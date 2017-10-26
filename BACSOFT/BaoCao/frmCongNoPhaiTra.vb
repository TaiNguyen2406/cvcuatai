Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports DevExpress.XtraEditors.Repository

Public Class frmCongNoPhaiTra
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _DaThu As Double = 0


    Private emptyEditor As RepositoryItemButtonEdit
    Private countPhieuThu As List(Of DanhSachPhieu)
    Private countPhieuChi As List(Of DanhSachPhieu)

    Private Sub frmCongNo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btfilterTakecare.Enabled = False
            btKetXuat.Enabled = False
        End If

        countPhieuThu = New List(Of DanhSachPhieu)
        countPhieuChi = New List(Of DanhSachPhieu)
        emptyEditor = New RepositoryItemButtonEdit()
        emptyEditor.Buttons.Clear()
        emptyEditor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        gdvPhaiTra.RepositoryItems.Add(emptyEditor)

    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
            btfilterTakecare.EditValue = TaiKhoan
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


    Public Sub LoadPhaiTra()

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


                    r("ConNo") = tb.Rows(0)("ConNo")
                    tb2.Rows.Add(r)
                    Dim r2 As DataRow = tb2.NewRow
                    LuyKe += tb.Rows(0)("TienThu")
                    If Not chkXemRutGon.Checked Then
                        r2("ttcMa") = tb.Rows(0)("ttcMa")
                        r2("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                        r2("NgayNK") = tb.Rows(0)("NgayNK")
                    End If
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
                            If Not chkXemRutGon.Checked Then
                                r4("ttcMa") = tb.Rows(0)("ttcMa")
                                r4("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                                r4("NgayNK") = tb.Rows(0)("NgayNK")
                            End If
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
                            If Not chkXemRutGon.Checked Then
                                r4("ttcMa") = tb.Rows(0)("ttcMa")
                                r4("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                                r4("NgayNK") = tb.Rows(0)("NgayNK")
                            End If
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

            Dim col As New DataColumn("colChon", Type.GetType("System.Boolean"))
            col.DefaultValue = False
            tb2.Columns.Add(col)
            gdvPhaiTra.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        countPhieuChi = New List(Of DanhSachPhieu)
        LoadPhaiTra()

    End Sub


    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "No Phai Tra " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvPhaiTraCT, False)

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


    Private Sub gdvPhaiTraCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiTraCT.RowCellStyle
        If toBoolean(gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "colChon")) Then
            If e.Appearance.BackColor = Color.White Then
                e.Appearance.BackColor = Color.LightBlue
            End If
        Else
            If e.Appearance.BackColor = Color.LightBlue Then
                e.Appearance.BackColor = Color.White
            End If
        End If
    End Sub

    Private Sub btLocDo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocDo.ItemClick
        On Error Resume Next

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

    End Sub

    Private Sub btLocVang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocVang.ItemClick

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


    End Sub

    Private Sub btLocXanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocXanh.ItemClick

        gdvPhaiTraCT.ClearColumnsFilter()
        Dim FilterString As String
        Dim FilterConNo As ColumnFilterInfo
        Dim BinaryFilter As CriteriaOperator
        BinaryFilter = New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Less))
        FilterString = BinaryFilter.ToString()
        FilterConNo = New ColumnFilterInfo(FilterString, "Còn nợ <0")
        gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo

    End Sub

    Private Sub gdvPhaiTraCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvPhaiTraCT.RowCellClick

        If e.RowHandle >= 0 And e.Column.Name = "colChon2" And Not gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ttcMa") Is DBNull.Value Then
            If countPhieuChi.Count > 0 Then
                If countPhieuChi(0).IDkhachHang <> gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "IDkhachHang") Then
                    Exit Sub
                End If
            End If
            Dim st As Boolean = Not toBoolean(e.CellValue)
            gdvPhaiTraCT.SetRowCellValue(e.RowHandle, e.Column, st)
            If st Then
                countPhieuChi.Add(New DanhSachPhieu(gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "IDkhachHang"), gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "SoPhieuNK"), gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo")))
            Else
                For Each obj As DanhSachPhieu In countPhieuChi
                    If obj.SoPhieu = gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "SoPhieuNK") Then
                        countPhieuChi.Remove(obj)
                        Exit For
                    End If
                Next
            End If
            TinhTongTienPhaiTra()
        End If

    End Sub

    Private Sub TinhTongTienPhaiTra()
        Dim tongtien As Double = 0
        For Each obj As DanhSachPhieu In countPhieuChi
            tongtien += obj.SoTien
        Next
        gdvPhaiTraCT.Columns("ttcMa").SummaryItem.SetSummary(Data.SummaryItemType.Custom, String.Format("{0:N2}", tongtien))
    End Sub

    Private Function toBoolean(obj As Object) As Boolean
        Try
            Return Convert.ToBoolean(obj)
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Sub gdvPhaiTraCT_CustomRowCellEdit(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles gdvPhaiTraCT.CustomRowCellEdit
        If e.Column.Name = "colChon2" And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ttcMa") Is DBNull.Value Then
            e.RepositoryItem = emptyEditor
        End If
    End Sub


    Private Sub gdvPhaiTra_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvPhaiTra.MouseDown
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            Exit Sub
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If countPhieuChi.Count > 0 Then
                pMenu.ShowPopup(gdvPhaiTra.PointToScreen(e.Location))
                mnuThuTienMat.Visibility = XtraBars.BarItemVisibility.Never
                mnuThuQuaNganHang.Visibility = XtraBars.BarItemVisibility.Never
                mnuChiTienMat.Visibility = XtraBars.BarItemVisibility.Always
                mnuUyNhiemChi.Visibility = XtraBars.BarItemVisibility.Always
                mnuCapNhat.Visibility = XtraBars.BarItemVisibility.Never
                mnuInPhieu.Visibility = XtraBars.BarItemVisibility.Never
                mDuKienPhaiThuPhaiTra.Visibility = XtraBars.BarItemVisibility.Always
                mDuKienPhaiThuPhaiTra.Caption = "Dự kiến phải trả"
                mDuKienPhaiThuPhaiTra.Tag = "Tra"
            Else
                Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
                HitInfo = gdvPhaiTraCT.CalcHitInfo(gdvPhaiTra.PointToClient(Cursor.Position))
                If HitInfo.InRow And HitInfo.RowHandle >= 0 Then
                    If gdvPhaiTraCT.GetRowCellValue(HitInfo.RowHandle, "ttcMa") Is DBNull.Value Then
                        mnuThuTienMat.Visibility = XtraBars.BarItemVisibility.Never
                        mnuThuQuaNganHang.Visibility = XtraBars.BarItemVisibility.Never
                        mnuChiTienMat.Visibility = XtraBars.BarItemVisibility.Never
                        mnuUyNhiemChi.Visibility = XtraBars.BarItemVisibility.Never
                        mnuCapNhat.Visibility = XtraBars.BarItemVisibility.Always
                        mnuInPhieu.Visibility = XtraBars.BarItemVisibility.Always
                        mDuKienPhaiThuPhaiTra.Visibility = XtraBars.BarItemVisibility.Never
                        pMenu.ShowPopup(gdvPhaiTra.PointToScreen(e.Location))
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub mnuThuTienMat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThuTienMat.ItemClick
        TrangThai.isAddNew = True
        Dim f As New frmCNThu2
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu thu"
        f.arrPhieu = countPhieuThu
        f.ShowDialog()
    End Sub

    Private Sub mnuThuQuaNganHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThuQuaNganHang.ItemClick
        TrangThai.isAddNew = True
        Dim f As New frmCNThu2
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu thu"
        f.arrPhieu = countPhieuThu
        f.ThuNH = True
        f.ShowDialog()
    End Sub

    Private Sub mnuChiTienMat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChiTienMat.ItemClick
        TrangThai.isAddNew = True
        Dim f As New frmCNChi2
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu chi"
        f.arrPhieu = countPhieuChi
        f.UNC = False
        f.ShowDialog()
    End Sub

    Private Sub mnuUyNhiemChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuUyNhiemChi.ItemClick
        TrangThai.isAddNew = True
        Dim f As New frmCNChi2
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu chi"
        f.arrPhieu = countPhieuChi
        f.UNC = True
        f.ShowDialog()
    End Sub

    Private Sub mnuCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCapNhat.ItemClick


        Dim indexRow As Integer = -1
        Dim soPhieu As String = ""
        Dim soPhieuT As String = ""




        indexRow = gdvPhaiTraCT.FocusedRowHandle
        soPhieu = gdvPhaiTraCT.GetRowCellValue(indexRow, "SoPhieuChi").ToString
        If soPhieu = "" Then Return
        Dim f As New frmCNChi2
        f.Tag = Me.Parent.Tag
        If soPhieu.IndexOf("CT") = 0 Then 'Chi tien mat
            AddParameter("@SoPhieu", soPhieu.Substring(3))
            soPhieuT = ExecuteSQLDataTable("SELECT SoPhieuT FROM CHI WHERE SoPhieu = @SoPhieu").Rows(0)(0)
            f.Text = "Cập nhật phiếu chi " & soPhieuT
            f.UNC = False
            f.PhieuChi = soPhieuT
            TrangThai.isUpdate = True
            If f.ShowDialog() = DialogResult.OK Then
                LoadPhaiTra()
                gdvPhaiTraCT.ClearSelection()
                gdvPhaiTraCT.FocusedRowHandle = indexRow
                gdvPhaiTraCT.MakeRowVisible(gdvPhaiTraCT.FocusedRowHandle, False)
                gdvPhaiTraCT.SelectRow(gdvPhaiTraCT.FocusedRowHandle)
            End If
        ElseIf soPhieu.IndexOf("UNC") = 0 Then 'Chi uy nhiem chi
            AddParameter("@SoPhieu", soPhieu.Substring(4))
            soPhieuT = ExecuteSQLDataTable("SELECT SoPhieuT FROM UNC WHERE SoPhieu = @SoPhieu").Rows(0)(0)
            f.Text = "Cập nhật UNC " & soPhieuT
            f.UNC = True
            f.PhieuChi = soPhieuT
            TrangThai.isUpdate = True
            If f.ShowDialog() = DialogResult.OK Then
                LoadPhaiTra()
                gdvPhaiTraCT.ClearSelection()
                gdvPhaiTraCT.FocusedRowHandle = indexRow
                gdvPhaiTraCT.MakeRowVisible(gdvPhaiTraCT.FocusedRowHandle, False)
                gdvPhaiTraCT.SelectRow(gdvPhaiTraCT.FocusedRowHandle)
            End If
        End If

    End Sub

    Private Sub mnuInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInPhieu.ItemClick

        Dim soPhieu As String = gdvPhaiTraCT.GetFocusedRowCellValue("SoPhieuChi")
        If soPhieu.IndexOf("CT") = 0 Then 'Chi tien mat
            Dim sql As String = ""
            sql = "SELECT SoPhieu,NgayThangCT,NguoiNhan,DienGiai,SoTien,ChungTuGoc, "
            sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=CHI.IDKh)DiaChi, "
            sql &= "(SELECT Ten FROM NHANSU WHERE ID=CHI.IDUser)NguoiLap FROM CHI WHERE SoPhieu=@SoPhieu "
            AddParameterWhere("@SoPhieu", soPhieu.Substring(3))
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                Dim f As New frmIn("In phiếu chi")
                Dim rpt As New rptPhieuThuChi
                rpt.pLogo.Image = My.Resources.Logo3
                rpt.lbTenPhieu.Text = "PHIẾU CHI"
                rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
                rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
                rpt.lbHoTen.Text = "Người nhận tiền: "
                rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNhan")
                rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
                rpt.lbLyDo.Text = "Lý do chi: "
                rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
                rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
                rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
                rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
                rpt.lbNguoiGd.Text = "Người nhận tiền"
                rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNhan")
                rpt.lbKTNguoiLap.Text = tb.Rows(0)("NguoiLap")
                rpt.CreateDocument()
                f.printControl.PrintingSystem = rpt.PrintingSystem
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf soPhieu.IndexOf("UNC") = 0 Then 'Chi uy nhiem chi
            Dim sql As String = ""
            sql &= " SELECT NgayThang,SoPhieu,TaiKhoanDi,NganHangDi,(SELECT Ten FROm KHACHHANG WHERE ID=74)DonViTra, "
            sql &= "TaiKhoanDen,NganHangDen,(SELECT Ten FROm KHACHHANG WHERE ID=UNC.IDKh)DonViNhan,DienGiaiNH AS DienGiai,SoTien,N'' AS BangChu "
            sql &= "FROM UNC "
            sql &= "WHERE SoPhieu=@SoPhieu "
            AddParameterWhere("@SoPhieu", soPhieu.Substring(4))
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tb.Rows(0)("BangChu") = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
                Dim f As New frmIn("In ủy nhiệm chi")
                Dim rpt As New rptUNC
                rpt.pLogo.Image = My.Resources.Logo3
                rpt.pAnhLien2.Image = My.Resources.Logo3
                rpt.DataSource = tb
                rpt.CreateDocument()
                f.printControl.PrintingSystem = rpt.PrintingSystem
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If

    End Sub


    Private Sub chkXemRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkXemRutGon.CheckedChanged
        If chkXemRutGon.Checked Then
            chkXemRutGon.Glyph = My.Resources.Checked
        Else
            chkXemRutGon.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub btTaiDS2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS2.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY "

        sql &= " SELECT PHIEUNHAPKHO.IDkhachHang,KHACHHANG.ttcMa,('NK' + PHIEUNHAPKHO.Sophieu )AS SoPhieuNK, PHIEUNHAPKHO.Ngaythang AS NgayNK,dateadd(day,  " & tbHanThu.EditValue & " ,PHIEUNHAPKHO.NgayThang)HanChi, "
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
        sql &= " SELECT * FROM ("
        sql &= "  SELECT #tbPhaiChi.*,(#tbPhaiChi.PhaiChi-tbDaChi.DaChi)ConNo,Convert(float,0) as LuyKe,(#tbPhaiChi.PhaiChi-tbDaChi.DaChi)SoTienNo,(CASE (#tbPhaiChi.PhaiChi-tbDaChi.DaChi) WHEN 0 THEN 0 ELSE ISNULL(Datediff(day,HanChi,Getdate()),0) END )NgayQH,ISNULL(Datediff(day,HanChi,NgayChiTien),0)NgayQH2,"
        sql &= "  ((#tbPhaiChi.PhaiChi-tbDaChi.DaChi) *  Datediff(day,HanChi,Getdate()) * @LaiSuat)TienLai"
        sql &= "  FROM #tbPhaiChi"
        sql &= "  INNER JOIN (SELECT SUM(TienThu)DaChi,SoPhieuNK FROM #tbPhaiChi GROUP BY SoPhieuNK)tbDaChi ON #tbPhaiChi.SoPhieuNK = tbDaChi.SoPhieuNK"
        sql &= " UNION ALL"

        sql &= " SELECT CHIPHI.IDDVVC as IDKhachHang,KHACHHANG.ttcMa,"
        sql &= " (CASE WHEN (CASE CHIPHI.Loai WHEN 0 then 'NK' + CHIPHI.PhieuTC WHEN 1 THEN 'XK' + CHIPHI.PhieuTC END) IS NULL THEN "
        sql &= "    (CASE CHIPHI.Loai WHEN 0 then 'DH' + CHIPHI.PhieuCGDH WHEN 1 THEN 'CG' + CHIPHI.PhieuCGDH END) ELSE"
        sql &= " (CASE CHIPHI.Loai WHEN 0 then 'NK' + CHIPHI.PhieuTC WHEN 1 THEN 'XK' + CHIPHI.PhieuTC END) END)SoPhieuNK,"
        sql &= " CHIPHI.ThoiGian as NgayNK,dateadd(day,  30 ,CHIPHI.ThoiGian)HanChi, "
        sql &= " CHIPHI.SoTien * CHIPHI.TyGia as PhaiChi,(CASE WHEN CHI.SoTien is null THEN ISNULL(UNC.SoTien,0) ELSE ISNULL(CHI.SoTien,0) END)TienThu,"
        sql &= " (CASE WHEN CHI.SoTien is null THEN (N'UNC ' + UNC.SoPhieu) ELSE (N'CT ' + CHI.SoPhieu) END)SoPhieuChi,"
        sql &= " (CASE WHEN CHI.SoTien is null THEN NgayThang ELSE CHI.NgayThangVS END)NgayChiTien,"
        sql &= " CHIPHI.IDUser as IDTakeCare,NHANSU.Ten as KinhDoanh,convert(float,0)ConNo,"
        sql &= " Convert(float,0) as LuyKe,(CHIPHI.SoTien - (CASE WHEN CHI.SoTien is null THEN ISNULL(UNC.SoTien,0) ELSE ISNULL(CHI.SoTien,0) END))SoTienNo,0 as NgayQH,0 as NgayQH2,"
        sql &= " Convert(float,0) TienLai"

        sql &= " FROM CHIPHI"
        sql &= " INNER JOIN MUCDICHTHUCHI ON CHIPHI.MucDich = MUCDICHTHUCHI.ID"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=CHIPHI.IDUser"
        sql &= " INNER JOIN KHACHHANG ON CHIPHI.IDDVVC = KHACHHANG.ID"
        sql &= " INNER JOIN tblTienTe ON CHIPHI.TienTe = tblTienTe.ID"
        sql &= " LEFT JOIN CHI ON CHI.SoTien=CHIPHI.SoTien*CHIPHI.TyGia AND ChiPhi.Loai<>CHI.ChiPhiNhap AND (CHIPHI.PhieuTC=CHI.PhieuTC1 OR CHIPHI.PhieuCGDH=CHI.PHIEUTC0) AND CHI.MucDich=CHIPHI.MucDich"
        sql &= " LEFT JOIN UNC ON UNC.SoTien=CHIPHI.SoTien*CHIPHI.TyGia AND ChiPhi.Loai<>UNC.ChiPhiNhap AND (CHIPHI.PhieuTC=UNC.PhieuTC1 OR CHIPHI.PHieuCGDH=CHI.PHIEUTC0) AND UNC.MucDich=CHIPHI.MucDich"
        sql &= " WHERE convert(datetime,convert(nvarchar, CHIPHI.ThoiGian,103),103) BETWEEN @TuNgay AND @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND CHIPHI.IDDVVC=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND CHIPHI.IDUser=" & btfilterTakecare.EditValue
        End If
        sql &= " )tbll WHERE SoPhieuNK is not null "
        sql &= "  ORDER BY SoPhieuNK,NgayChiTien"
        sql &= "  DROP table #tbPhaiChi "
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


                    r("ConNo") = tb.Rows(0)("ConNo")
                    tb2.Rows.Add(r)
                    Dim r2 As DataRow = tb2.NewRow
                    LuyKe += tb.Rows(0)("TienThu")
                    If Not chkXemRutGon.Checked Then
                        r2("ttcMa") = tb.Rows(0)("ttcMa")
                        r2("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                        r2("NgayNK") = tb.Rows(0)("NgayNK")
                    End If
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
                            If Not chkXemRutGon.Checked Then
                                r4("ttcMa") = tb.Rows(0)("ttcMa")
                                r4("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                                r4("NgayNK") = tb.Rows(0)("NgayNK")
                            End If
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
                            If Not chkXemRutGon.Checked Then
                                r4("ttcMa") = tb.Rows(0)("ttcMa")
                                r4("SoPhieuNK") = tb.Rows(0)("SoPhieuNK")
                                r4("NgayNK") = tb.Rows(0)("NgayNK")
                            End If
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

            Dim col As New DataColumn("colChon", Type.GetType("System.Boolean"))
            col.DefaultValue = False
            tb2.Columns.Add(col)
            gdvPhaiTra.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
End Class

Public Class DanhSachPhieu2

    Private _IDkhachHang As Integer
    Public Property IDkhachHang() As Integer
        Get
            Return _IDkhachHang
        End Get
        Set(ByVal value As Integer)
            _IDkhachHang = value
        End Set
    End Property


    Private _SoPhieu As String
    Public Property SoPhieu() As String
        Get
            Return _SoPhieu
        End Get
        Set(ByVal value As String)
            _SoPhieu = value
        End Set
    End Property

    Private _SoTien As Double
    Public Property SoTien() As Double
        Get
            Return _SoTien
        End Get
        Set(ByVal value As Double)
            _SoTien = value
        End Set
    End Property

    Public Sub New(__IDkhachHang As Integer, __SoPhieu As String, __SoTien As Double)
        _IDkhachHang = __IDkhachHang
        _SoPhieu = __SoPhieu
        _SoTien = __SoTien
    End Sub

End Class

