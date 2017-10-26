Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmCongNo
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _DaThu As Double = 0

    Private Sub frmCongNo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()

    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbTakecare_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTakecare.EditValue = Nothing
        End If
    End Sub

#End Region

    Private Sub LoadPhaiThu()
        gdvPhaiThuCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT PHIEUXUATKHO.IDkhachHang,KHACHHANG.ttcMa, PHIEUXUATKHO.Sophieu AS SoPhieuXK, PHIEUXUATKHO.Ngaythang AS NgayXK,dateadd(day, " & tbHanThu.EditValue & ",PHIEUXUATKHO.NgayThang)HanThu,ISNULL(PHIEUXUATKHO.HanThu,dateadd(day, " & tbHanThu.EditValue & ",PHIEUXUATKHO.NgayThang)) as HanThuThuc, "
        sql &= "   (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia AS PhaiThu, ISNULL(tbThu.Sotien, 0) AS TienThu,ISNULL(SoTien2,0)SoTien2, "
        sql &= "   tbThu.SoPhieu AS SoPhieuThu, tbThu.NgayThangVS AS NgayThuTien, PHIEUXUATKHO.IDTakecare,NHANSU.Ten AS KinhDoanh,PHIEUXUATKHO.SoPhieuCG"
        sql &= " INTO #tbPhaiThu"
        sql &= " FROM PHIEUXUATKHO "
        sql &= " LEFT JOIN (SELECT SoTien,SoTien AS SoTien2,(N'TT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THU"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,SoTien as SoTien2,(N'CK ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THUNH"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich=230"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich=230"
        sql &= " )tbThu ON PHIEUXUATKHO.Sophieu = tbThu.PhieuTC1 OR PHIEUXUATKHO.SophieuCG = tbThu.PhieuTC0 "
        sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN NHANSU ON PHIEUXUATKHO.IDTakeCare=NHANSU.ID"

        sql &= " WHERE  CONVERT(datetime,Convert(Nvarchar,PHIEUXUATKHO.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUXUATKHO.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND PHIEUXUATKHO.IDTakeCare=" & btfilterTakecare.EditValue
        End If
        ' 
        sql &= " SELECT #tbPhaiThu.*,(#tbPhaiThu.PhaiThu-tbDaThu.DaThu)ConNo,0.0 as LuyKe,(#tbPhaiThu.PhaiThu-tbDaThu.DaThu)SoTienNo,(CASE (#tbPhaiThu.PhaiThu-tbDaThu.DaThu) WHEN 0 THEN 0 ELSE Datediff(day,HanThuThuc,Getdate()) END )NgayQH,Datediff(day,HanThuThuc,NgayThuTien)NgayQH2,"
        sql &= " ((#tbPhaiThu.PhaiThu-tbDaThu.DaThu) *  Datediff(day,HanThu,Getdate()) * @LaiSuat)TienLai"
        sql &= " FROM #tbPhaiThu"
        sql &= " INNER JOIN (SELECT SUM(SoTien2)DaThu,SoPhieuXK FROM #tbPhaiThu GROUP BY SoPhieuXK)tbDaThu ON #tbPhaiThu.SoPhieuXK = tbDaThu.SoPhieuXK"
        sql &= " ORDER BY SoPhieuXK,NgayThuTien"
        sql &= " DROP table #tbPhaiThu "

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        AddParameter("@LaiSuat", tbLaiSuat.EditValue / 12 / 30 / 100)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        Dim tb2 As New DataTable
        Dim LuyKe As Double = 0
        _DaThu = 0
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                tb2 = tb.Clone
                Dim r As DataRow = tb2.NewRow
                r("IDKhachHang") = tb.Rows(0)("IDKhachHang")
                r("ttcMa") = tb.Rows(0)("ttcMa")
                r("SoPhieuXK") = tb.Rows(0)("SoPhieuXK")
                r("SoPhieuCG") = tb.Rows(0)("SoPhieuCG")
                r("NgayXK") = tb.Rows(0)("NgayXK")
                r("HanThu") = tb.Rows(0)("HanThu")
                r("HanThuThuc") = tb.Rows(0)("HanThuThuc")
                r("PhaiThu") = tb.Rows(0)("PhaiThu")
                r("NgayQH") = tb.Rows(0)("NgayQH")

                r("KinhDoanh") = tb.Rows(0)("KinhDoanh")


                If tb.Rows(0)("SoPhieuThu").ToString <> "" Then
                    tb2.Rows.Add(r)
                    Dim r2 As DataRow = tb2.NewRow
                    'If tb.Rows(0)("SoPhieuThu").ToString.Substring(0, 3) = "CT " Or tb.Rows(0)("SoPhieuThu").ToString.Substring(0, 3) = "UNC" Then
                    '    LuyKe -= tb.Rows(0)("TienThu")
                    '    _DaThu -= tb.Rows(0)("TienThu")
                    'Else
                    '    LuyKe += tb.Rows(0)("TienThu")
                    '    _DaThu += tb.Rows(0)("TienThu")
                    'End If
                    LuyKe += tb.Rows(0)("SoTien2")
                    _DaThu += tb.Rows(0)("SoTien2")

                    r2("TienThu") = tb.Rows(0)("TienThu")
                    r2("SoPhieuThu") = tb.Rows(0)("SoPhieuThu")
                    r2("NgayThuTien") = tb.Rows(0)("NgayThuTien")
                    'r2("ConNo") = tb.Rows(0)("ConNo")
                    r2("LuyKe") = tb.Rows(0)("TienThu")
                    ' r2("SoTienNo") = tb.Rows(0)("SoTienNo")
                    tb2.Rows.Add(r2)
                Else
                    r("ConNo") = tb.Rows(0)("ConNo")
                    If tb.Rows(0)("NgayQH") > 0 Then
                        r("SoTienNo") = tb.Rows(0)("SoTienNo")
                        r("TienLai") = tb.Rows(0)("TienLai")
                    End If

                    tb2.Rows.Add(r)
                End If

                Dim i As Integer = 1


                While i < tb.Rows.Count
                    'If tb.Rows(i)("SoPhieuXK") = "1307038" Then
                    '    ShowAlert(i)
                    'End If

                    If tb.Rows(i)("SoPhieuXK") <> tb.Rows(i - 1)("SoPhieuXK") Then
                        Dim r3 As DataRow = tb2.NewRow
                        r3("SoPhieuCG") = tb.Rows(i)("SoPhieuCG")
                        r3("IDKhachHang") = tb.Rows(i)("IDKhachHang")
                        r3("ttcMa") = tb.Rows(i)("ttcMa")
                        r3("SoPhieuXK") = tb.Rows(i)("SoPhieuXK")
                        r3("NgayXK") = tb.Rows(i)("NgayXK")
                        r3("HanThu") = tb.Rows(i)("HanThu")
                        r3("HanThuThuc") = tb.Rows(i)("HanThuThuc")
                        r3("PhaiThu") = tb.Rows(i)("PhaiThu")
                        r3("NgayQH") = tb.Rows(i)("NgayQH")

                        r3("KinhDoanh") = tb.Rows(i)("KinhDoanh")
                        'If IsDBNull(tb.Rows(i)("SoPhieuThu")) Then
                        r3("ConNo") = tb.Rows(i)("ConNo")
                        If tb.Rows(i)("NgayQH") > 0 Then
                            r3("SoTienNo") = tb.Rows(i)("SoTienNo")
                            r3("TienLai") = tb.Rows(i)("TienLai")
                        End If

                        'End If
                        LuyKe = 0
                        tb2.Rows.Add(r3)
                        If Not IsDBNull(tb.Rows(i)("SoPhieuThu")) Then
                            Dim r4 As DataRow = tb2.NewRow
                            'If tb.Rows(i)("SoPhieuThu").ToString.Substring(0, 3) = "CT " Or tb.Rows(i)("SoPhieuThu").ToString.Substring(0, 3) = "UNC" Then
                            '    LuyKe -= tb.Rows(i)("TienThu")
                            '    _DaThu -= tb.Rows(i)("TienThu")
                            'Else
                            '    LuyKe += tb.Rows(i)("TienThu")
                            '    _DaThu += tb.Rows(i)("TienThu")
                            'End If
                            LuyKe += tb.Rows(i)("SoTien2")
                            _DaThu += tb.Rows(i)("SoTien2")

                            r4("TienThu") = tb.Rows(i)("TienThu")
                            r4("SoPhieuThu") = tb.Rows(i)("SoPhieuThu")
                            r4("NgayThuTien") = tb.Rows(i)("NgayThuTien")
                            'r4("ConNo") = tb.Rows(i)("ConNo")
                            r4("LuyKe") = tb.Rows(i)("TienThu")
                            '  r4("SoTienNo") = tb.Rows(i)("PhaiThu") - LuyKe
                            r4("NgayQH") = tb.Rows(i)("NgayQH2")
                            tb2.Rows.Add(r4)
                        End If
                    Else
                        If Not IsDBNull(tb.Rows(i)("SoPhieuThu")) Then
                            Dim r4 As DataRow = tb2.NewRow
                            'If tb.Rows(i)("SoPhieuThu").ToString.Substring(0, 3) = "CT " Or tb.Rows(i)("SoPhieuThu").ToString.Substring(0, 3) = "UNC" Then
                            '    LuyKe -= tb.Rows(i)("TienThu")
                            '    _DaThu -= tb.Rows(i)("TienThu")
                            'Else
                            '    LuyKe += tb.Rows(i)("TienThu")
                            '    _DaThu += tb.Rows(i)("TienThu")
                            'End If
                            LuyKe += tb.Rows(i)("SoTien2")
                            _DaThu += tb.Rows(i)("SoTien2")

                            r4("TienThu") = tb.Rows(i)("TienThu")
                            r4("SoPhieuThu") = tb.Rows(i)("SoPhieuThu")
                            r4("NgayThuTien") = tb.Rows(i)("NgayThuTien")
                            ' r4("ConNo") = tb.Rows(i)("ConNo")
                            r4("LuyKe") = LuyKe
                            '  r4("SoTienNo") = tb.Rows(i)("PhaiThu") - LuyKe
                            r4("NgayQH") = tb.Rows(i)("NgayQH2")
                            tb2.Rows.Add(r4)
                        End If
                    End If
                    i += 1
                End While
            End If

            gdvPhaiThu.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadPhaiTra()

        gdvPhaiTraCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT PHIEUNHAPKHO.IDkhachHang,KHACHHANG.ttcMa, PHIEUNHAPKHO.Sophieu AS SoPhieuNK, PHIEUNHAPKHO.Ngaythang AS NgayNK,dateadd(day, " & tbHanThu.EditValue & ",PHIEUNHAPKHO.NgayThang)HanChi, "
        sql &= "   (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia AS PhaiChi, ISNULL(tbChi.Sotien, 0) AS TienThu, "
        sql &= "   tbChi.SoPhieu AS SoPhieuChi, tbChi.NgayThangVS AS NgayChiTien, PHIEUDATHANG.IDTakeCare,NHANSU.Ten AS KinhDoanh"
        sql &= " INTO #tbPhaiChi"
        sql &= " FROM PHIEUNHAPKHO "
        sql &= " LEFT JOIN (SELECT SoTien,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich IN (210,228)"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich IN (210,228))tbChi ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SophieuDH = tbChi.PhieuTC0 "
        sql &= " LEFT JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN PHIEUDATHANG ON PHIEUNHAPKHO.SoPhieuDH=PHIEUDATHANG.SoPhieu"
        sql &= " LEFT JOIN NHANSU ON PHIEUDATHANG.IDTakeCare=NHANSU.ID"

        sql &= " WHERE  CONVERT(datetime,Convert(Nvarchar,PHIEUNHAPKHO.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDUser=" & btfilterTakecare.EditValue
        End If
        ' 
        sql &= " SELECT #tbPhaiChi.*,(#tbPhaiChi.PhaiChi-tbDaChi.DaChi)ConNo,0.0 as LuyKe,(#tbPhaiChi.PhaiChi-tbDaChi.DaChi)SoTienNo,(CASE (#tbPhaiChi.PhaiChi-tbDaChi.DaChi) WHEN 0 THEN 0 ELSE ISNULL(Datediff(day,HanChi,Getdate()),0) END )NgayQH,ISNULL(Datediff(day,HanChi,NgayChiTien),0)NgayQH2,"
        sql &= " ((#tbPhaiChi.PhaiChi-tbDaChi.DaChi) *  Datediff(day,HanChi,Getdate()) * @LaiSuat)TienLai"
        sql &= " FROM #tbPhaiChi"
        sql &= " INNER JOIN (SELECT SUM(TienThu)DaChi,SoPhieuNK FROM #tbPhaiChi GROUP BY SoPhieuNK)tbDaChi ON #tbPhaiChi.SoPhieuNK = tbDaChi.SoPhieuNK"
        sql &= " ORDER BY SoPhieuNK,NgayChiTien"
        sql &= " DROP table #tbPhaiChi "

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        AddParameter("@LaiSuat", tbLaiSuat.EditValue / 12 / 30 / 100)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        Dim tb2 As New DataTable
        Dim LuyKe As Double = 0
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                tb2 = tb.Clone
                Dim r As DataRow = tb2.NewRow
                r("IDKhachHang") = tb.Rows(0)("IDKhachHang")
                r("ttcMa") = tb.Rows(0)("ttcMa")
                r("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                r("NgayNK") = tb.Rows(0)("NgayNK")
                r("HanChi") = tb.Rows(0)("HanChi")
                r("PhaiChi") = tb.Rows(0)("PhaiChi")
                r("NgayQH") = tb.Rows(0)("NgayQH")
                r("TienLai") = tb.Rows(0)("TienLai")
                r("KinhDoanh") = tb.Rows(0)("KinhDoanh")


                If tb.Rows(0)("SoPhieuChi").ToString <> "" Then
                    tb2.Rows.Add(r)
                    Dim r2 As DataRow = tb2.NewRow
                    LuyKe += tb.Rows(0)("TienThu")
                    r2("TienThu") = tb.Rows(0)("TienThu")
                    r2("SoPhieuChi") = tb.Rows(0)("SoPhieuChi")
                    r2("NgayChiTien") = tb.Rows(0)("NgayChiTien")
                    'r2("ConNo") = tb.Rows(0)("ConNo")
                    r2("LuyKe") = tb.Rows(0)("TienThu")
                    ' r2("SoTienNo") = tb.Rows(0)("SoTienNo")
                    tb2.Rows.Add(r2)
                Else
                    r("ConNo") = tb.Rows(0)("ConNo")
                    r("SoTienNo") = tb.Rows(0)("SoTienNo")
                    tb2.Rows.Add(r)
                End If

                Dim i As Integer = 1


                While i < tb.Rows.Count

                    If tb.Rows(i)("SoPhieuNK") <> tb.Rows(i - 1)("SoPhieuNK") Then
                        Dim r3 As DataRow = tb2.NewRow
                        r3("IDKhachHang") = tb.Rows(i)("IDKhachHang")
                        r3("ttcMa") = tb.Rows(i)("ttcMa")
                        r3("SoPhieuNK") = tb.Rows(i)("SoPhieuNK")
                        r3("NgayNK") = tb.Rows(i)("NgayNK")
                        r3("HanChi") = tb.Rows(i)("HanChi")
                        r3("PhaiChi") = tb.Rows(i)("PhaiChi")
                        r3("NgayQH") = tb.Rows(i)("NgayQH")
                        r3("TienLai") = tb.Rows(i)("TienLai")
                        r3("KinhDoanh") = tb.Rows(i)("KinhDoanh")
                        'If IsDBNull(tb.Rows(i)("SoPhieuThu")) Then
                        r3("ConNo") = tb.Rows(i)("ConNo")
                        r3("SoTienNo") = tb.Rows(i)("SoTienNo")
                        'End If
                        LuyKe = 0
                        tb2.Rows.Add(r3)
                        If Not IsDBNull(tb.Rows(i)("SoPhieuChi")) Then
                            Dim r4 As DataRow = tb2.NewRow
                            LuyKe += tb.Rows(i)("TienThu")
                            r4("TienThu") = tb.Rows(i)("TienThu")
                            r4("SoPhieuChi") = tb.Rows(i)("SoPhieuChi")
                            r4("NgayChiTien") = tb.Rows(i)("NgayChiTien")
                            'r4("ConNo") = tb.Rows(i)("ConNo")
                            r4("LuyKe") = tb.Rows(i)("TienThu")
                            '  r4("SoTienNo") = tb.Rows(i)("PhaiChi") - LuyKe
                            r4("NgayQH") = tb.Rows(i)("NgayQH2")
                            tb2.Rows.Add(r4)
                        End If
                    Else
                        If Not IsDBNull(tb.Rows(i)("SoPhieuChi")) Then
                            Dim r4 As DataRow = tb2.NewRow
                            LuyKe += tb.Rows(i)("TienThu")
                            r4("TienThu") = tb.Rows(i)("TienThu")
                            r4("SoPhieuChi") = tb.Rows(i)("SoPhieuChi")
                            r4("NgayChiTien") = tb.Rows(i)("NgayChiTien")
                            'r4("ConNo") = tb.Rows(i)("ConNo")
                            r4("LuyKe") = LuyKe
                            'r4("SoTienNo") = tb.Rows(i)("PhaiChi") - LuyKe
                            r4("NgayQH") = tb.Rows(i)("NgayQH2")
                            tb2.Rows.Add(r4)
                        End If
                    End If

                    i += 1
                End While


            End If

            gdvPhaiTra.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If tabCongNo.SelectedTabPage Is tabPhaiTra Then
            LoadPhaiTra()
        Else
            LoadPhaiThu()
        End If
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            saveFile.FileName = "No Phai Thu " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"
        Else
            saveFile.FileName = "No Phai Tra " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"
        End If

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                If tabCongNo.SelectedTabPage Is tabPhaiThu Then
                    Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvPhaiThuCT, False)
                Else
                    Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvPhaiTraCT, False)
                End If

                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub gdvPhaiThuCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiThuCT.RowCellStyle, gdvPhaiTraCT.RowCellStyle
        On Error Resume Next
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            If e.Column.FieldName = "ConNo" Then
                If Not IsDBNull(e.CellValue) Then
                    If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
                        e.Appearance.BackColor = Color.Red
                    ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
                        e.Appearance.BackColor = Color.Cyan
                    ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") <= 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") >= -10 Then
                        e.Appearance.BackColor = Color.Yellow
                    End If
                End If
                
            End If
        Else
            If e.Column.FieldName = "ConNo" Then
                If Not IsDBNull(e.CellValue) Then
                    If gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
                        e.Appearance.BackColor = Color.Red
                    ElseIf gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
                        e.Appearance.BackColor = Color.Cyan
                    ElseIf gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") <= 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") >= -10 Then
                        e.Appearance.BackColor = Color.Yellow
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btLocDo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocDo.ItemClick
        On Error Resume Next
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            gdvPhaiThuCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As New ColumnFilterInfoCollection
            Dim BinaryFilter As New CriteriaOperatorCollection
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Greater)))
            FilterString = BinaryFilter.ToString()
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đã quá hạn"))
            gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            gdvPhaiThuCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        Else
            gdvPhaiTraCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As New ColumnFilterInfoCollection
            Dim BinaryFilter As New CriteriaOperatorCollection
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Greater)))
            FilterString = BinaryFilter.ToString()
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đã quá hạn"))
            gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            gdvPhaiTraCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        End If
    End Sub

    Private Sub btLocVang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocVang.ItemClick
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            gdvPhaiThuCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As New ColumnFilterInfoCollection
            Dim BinaryFilter As New CriteriaOperatorCollection
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.LessOrEqual)))
            FilterString = BinaryFilter.ToString()
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đến hạn"))
            gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            gdvPhaiThuCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        Else
            gdvPhaiTraCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As New ColumnFilterInfoCollection
            Dim BinaryFilter As New CriteriaOperatorCollection
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Equal)))
            FilterString = BinaryFilter.ToString()
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đến hạn"))
            gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            gdvPhaiTraCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        End If
       
    End Sub

    Private Sub btLocXanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocXanh.ItemClick
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            gdvPhaiThuCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Less))
            FilterString = BinaryFilter.ToString()
            FilterConNo = New ColumnFilterInfo(FilterString, "Còn nợ <0")
            gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo
        Else
            gdvPhaiTraCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Less))
            FilterString = BinaryFilter.ToString()
            FilterConNo = New ColumnFilterInfo(FilterString, "Còn nợ <0")
            gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo
        End If
    End Sub

    Private Sub gdvPhaiThuCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvPhaiThuCT.CustomSummaryCalculate
        'On Error Resume Next
        If e.IsTotalSummary Then
            If CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "TienThu" Then
                e.TotalValue = _DaThu
            End If
        End If
    End Sub

    Private Sub mDuKienThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienThu.ItemClick
        If gdvPhaiThuCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.FocusedRowHandle, "SoPhieuCG")
        f._SoPhieuXNK = gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.FocusedRowHandle, "SoPhieuXK")
        f._PhaiTra = False
        f._Buoc1 = False
        f.ShowDialog()
    End Sub

    Private Sub gdvPhaiThu_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvPhaiThu.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvPhaiThuCT.CalcHitInfo(gdvPhaiThu.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            mPhaiThu.ShowPopup(gdvPhaiThu.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvPhaiThuCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvPhaiThuCT.CellValueChanged
        On Error Resume Next
        If e.Column.FieldName = "HanThuThuc" Then
            AddParameter("@HanThu", e.Value)
            AddParameterWhere("@SP", gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoPhieuXK"))
            If doUpdate("PHIEUXUATKHO", "SoPhieu=@SP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã lưu !")
            End If
        End If
    End Sub
End Class