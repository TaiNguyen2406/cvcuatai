Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports DevExpress.XtraEditors.Repository

Public Class frmCongNoPhaiThu
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
        gdvPhaiThu.RepositoryItems.Add(emptyEditor)

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

    'CG 

    Public Sub LoadPhaiThu()
        gdvPhaiThuCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT  PHIEUXUATKHO.IDkhachHang,KHACHHANG.ttcMa, PHIEUXUATKHO.Sophieu AS SoPhieuXK,PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Ngaythang AS NgayXK,dateadd(day, " & tbHanThu.EditValue & ",PHIEUXUATKHO.NgayThang)HanThu, ISNULL(PHIEUXUATKHO.HanThu,dateadd(day, " & tbHanThu.EditValue & ",PHIEUXUATKHO.NgayThang)) as HanThuThuc,"
        sql &= "   (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia AS PhaiThu, ISNULL(tbThu.Sotien, 0) AS TienThu,ISNULL(SoTien2,0)SoTien2, "
        sql &= "   tbThu.SoPhieu AS SoPhieuThu, tbThu.NgayThangVS AS NgayThuTien, PHIEUXUATKHO.IDTakecare,NHANSU.Ten AS KinhDoanh"
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
                r("PhaiThu") = tb.Rows(0)("PhaiThu")
                r("HanThuThuc") = tb.Rows(0)("HanThuThuc")
                r("NgayQH") = tb.Rows(0)("NgayQH")

                r("KinhDoanh") = tb.Rows(0)("KinhDoanh")



                If tb.Rows(0)("SoPhieuThu").ToString <> "" Then
                    r("ConNo") = tb.Rows(0)("ConNo")
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
                    If Not chkXemRutGon.Checked Then
                        r2("ttcMa") = tb.Rows(0)("ttcMa")
                        r2("SoPhieuXK") = tb.Rows(0)("SoPhieuXK")
                        r2("SoPhieuCG") = tb.Rows(0)("SoPhieuCG")
                        r2("NgayXK") = tb.Rows(0)("NgayXK")
                    End If

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
                        r3("IDKhachHang") = tb.Rows(i)("IDKhachHang")
                        r3("ttcMa") = tb.Rows(i)("ttcMa")
                        r3("SoPhieuXK") = tb.Rows(i)("SoPhieuXK")
                        r3("SoPhieuCG") = tb.Rows(0)("SoPhieuCG")
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

                            If Not chkXemRutGon.Checked Then
                                r4("ttcMa") = tb.Rows(0)("ttcMa")
                                r4("SoPhieuXK") = tb.Rows(0)("SoPhieuXK")
                                r4("SoPhieuCG") = tb.Rows(0)("SoPhieuCG")
                                r4("NgayXK") = tb.Rows(0)("NgayXK")
                            End If
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
                            If Not chkXemRutGon.Checked Then
                                r4("ttcMa") = tb.Rows(0)("ttcMa")
                                r4("SoPhieuXK") = tb.Rows(0)("SoPhieuXK")
                                r4("SoPhieuCG") = tb.Rows(0)("SoPhieuCG")
                                r4("NgayXK") = tb.Rows(0)("NgayXK")
                            End If

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

            Dim col As New DataColumn("colChon", Type.GetType("System.Boolean"))
            col.DefaultValue = False
            tb2.Columns.Add(col)
            gdvPhaiThu.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        countPhieuThu = New List(Of DanhSachPhieu)
        LoadPhaiThu()

    End Sub


    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "No Phai Thu " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvPhaiThuCT, False)

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


    Private Sub gdvPhaiThuCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiThuCT.RowCellStyle
        On Error Resume Next

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
       
    End Sub

    Private Sub gdvPhaiThuCT2_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiThuCT.RowCellStyle
        If toBoolean(gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "colChon")) Then
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
       
    End Sub

    Private Sub btLocVang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocVang.ItemClick

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
     
    End Sub

    Private Sub btLocXanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocXanh.ItemClick

        gdvPhaiThuCT.ClearColumnsFilter()
        Dim FilterString As String
        Dim FilterConNo As ColumnFilterInfo
        Dim BinaryFilter As CriteriaOperator
        BinaryFilter = New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Less))
        FilterString = BinaryFilter.ToString()
        FilterConNo = New ColumnFilterInfo(FilterString, "Còn nợ <0")
        gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo

    End Sub

    Private Sub gdvPhaiThuCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvPhaiThuCT.CustomSummaryCalculate
        'On Error Resume Next
        If e.IsTotalSummary Then
            If CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "TienThu" Then
                e.TotalValue = _DaThu
            End If
        End If
    End Sub

    Private Sub gdvPhaiThuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvPhaiThuCT.RowCellClick
        If e.RowHandle >= 0 And e.Column.Name = "colChon" And Not gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ttcMa") Is DBNull.Value Then
            If countPhieuThu.Count > 0 Then
                If countPhieuThu(0).IDkhachHang <> gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "IDkhachHang") Then
                    Exit Sub
                End If
            End If
            Dim st As Boolean = Not toBoolean(e.CellValue)
            gdvPhaiThuCT.SetRowCellValue(e.RowHandle, e.Column, st)
            If st Then
                countPhieuThu.Add(New DanhSachPhieu(gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "IDkhachHang"), gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoPhieuXK"), gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo")))
            Else
                For Each obj As DanhSachPhieu In countPhieuThu
                    If obj.SoPhieu = gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoPhieuXK") Then
                        countPhieuThu.Remove(obj)
                        Exit For
                    End If
                Next
            End If
            TinhTongTienPhaiThu()
        End If
    End Sub

    Private Sub TinhTongTienPhaiThu()
        Dim tongtien As Double = 0
        For Each obj As DanhSachPhieu In countPhieuThu
            tongtien += obj.SoTien
        Next
        gdvPhaiThuCT.Columns("ttcMa").SummaryItem.SetSummary(Data.SummaryItemType.Custom, String.Format("{0:N2}", tongtien))
    End Sub



    Private Function toBoolean(obj As Object) As Boolean
        Try
            Return Convert.ToBoolean(obj)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub gdvPhaiThuCT_CustomRowCellEdit(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles gdvPhaiThuCT.CustomRowCellEdit
        If e.Column.Name = "colChon" And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ttcMa") Is DBNull.Value Then
            e.RepositoryItem = emptyEditor
        End If
    End Sub



    Private Sub gdvPhaiThu_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvPhaiThu.MouseDown
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            Exit Sub
        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If countPhieuThu.Count > 0 Then
                pMenu.ShowPopup(gdvPhaiThu.PointToScreen(e.Location))
                mnuThuTienMat.Visibility = XtraBars.BarItemVisibility.Always
                mnuThuQuaNganHang.Visibility = XtraBars.BarItemVisibility.Always
                mnuChiTienMat.Visibility = XtraBars.BarItemVisibility.Never
                mnuUyNhiemChi.Visibility = XtraBars.BarItemVisibility.Never
                mnuCapNhat.Visibility = XtraBars.BarItemVisibility.Never
                mnuInPhieu.Visibility = XtraBars.BarItemVisibility.Never
                mDuKienPhaiThuPhaiTra.Visibility = XtraBars.BarItemVisibility.Always
                mDuKienPhaiThuPhaiTra.Caption = "Dự kiến phải thu"
                mDuKienPhaiThuPhaiTra.Tag = "Thu"

            Else
                Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
                HitInfo = gdvPhaiThuCT.CalcHitInfo(gdvPhaiThu.PointToClient(Cursor.Position))
                If HitInfo.InRow And HitInfo.RowHandle >= 0 Then
                    If gdvPhaiThuCT.GetRowCellValue(HitInfo.RowHandle, "ttcMa") Is DBNull.Value Then
                        mnuThuTienMat.Visibility = XtraBars.BarItemVisibility.Never
                        mnuThuQuaNganHang.Visibility = XtraBars.BarItemVisibility.Never
                        mnuChiTienMat.Visibility = XtraBars.BarItemVisibility.Never
                        mnuUyNhiemChi.Visibility = XtraBars.BarItemVisibility.Never
                        mnuCapNhat.Visibility = XtraBars.BarItemVisibility.Always
                        mDuKienPhaiThuPhaiTra.Visibility = XtraBars.BarItemVisibility.Never
                        If gdvPhaiThuCT.GetRowCellValue(HitInfo.RowHandle, "SoPhieuThu").ToString.IndexOf("TT") = 0 Then
                            mnuInPhieu.Visibility = XtraBars.BarItemVisibility.Always
                        Else
                            mnuInPhieu.Visibility = XtraBars.BarItemVisibility.Never
                        End If
                        pMenu.ShowPopup(gdvPhaiThu.PointToScreen(e.Location))
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

        If gdvPhaiThuCT.FocusedRowHandle < 0 Then Exit Sub

        Dim indexRow As Integer = -1
        Dim soPhieu As String = ""
        Dim soPhieuT As String = ""



            indexRow = gdvPhaiThuCT.FocusedRowHandle
            soPhieu = gdvPhaiThuCT.GetRowCellValue(indexRow, "SoPhieuThu").ToString
            If soPhieu = "" Then Return
            Dim f As New frmCNThu2
            f.Tag = Me.Parent.Tag
            If soPhieu.IndexOf("TT") = 0 Then 'Thu tien mat
                AddParameter("@SoPhieu", soPhieu.Substring(3))
                soPhieuT = ExecuteSQLDataTable("SELECT SoPhieuT FROM THU WHERE SoPhieu = @SoPhieu").Rows(0)(0)
                f.Text = "Cập nhật phiếu thu " & soPhieuT
                f.PhieuThu = soPhieuT
                f.ThuNH = False
                TrangThai.isUpdate = True
                If f.ShowDialog() = DialogResult.OK Then
                    LoadPhaiThu()
                    gdvPhaiThuCT.ClearSelection()
                    gdvPhaiThuCT.FocusedRowHandle = indexRow
                    gdvPhaiThuCT.MakeRowVisible(gdvPhaiThuCT.FocusedRowHandle, False)
                    gdvPhaiThuCT.SelectRow(gdvPhaiThuCT.FocusedRowHandle)
                End If
            ElseIf soPhieu.IndexOf("CK") = 0 Then 'Thu ngan hang
                AddParameter("@SoPhieu", soPhieu.Substring(3))
                soPhieuT = ExecuteSQLDataTable("SELECT SoPhieuT FROM THUNH WHERE SoPhieu = @SoPhieu").Rows(0)(0)
                f.Text = "Cập nhật phiếu thu NH " & soPhieuT
                f.PhieuThu = soPhieuT
                f.ThuNH = True
                TrangThai.isUpdate = True
                If f.ShowDialog() = DialogResult.OK Then
                    LoadPhaiThu()
                    gdvPhaiThuCT.ClearSelection()
                    gdvPhaiThuCT.FocusedRowHandle = indexRow
                    gdvPhaiThuCT.MakeRowVisible(gdvPhaiThuCT.FocusedRowHandle, False)
                    gdvPhaiThuCT.SelectRow(gdvPhaiThuCT.FocusedRowHandle)
                End If
            End If
       
    End Sub

    Private Sub mnuInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInPhieu.ItemClick
        If gdvPhaiThuCT.FocusedRowHandle < 0 Then Exit Sub


        'In phieu thu tin mat
        Dim sql As String = ""
        sql = "SELECT SoPhieu,NgayThangCT,NguoiNop,DienGiai,SoTien,ChungTuGoc, "
        sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=THU.IDKh)DiaChi, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap "
        sql &= "FROM THU WHERE SoPhieu = @SoPhieu "
        AddParameterWhere("@SoPhieu", gdvPhaiThuCT.GetFocusedRowCellValue("SoPhieuThu").ToString.Substring(3))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmIn("In phiếu thu")
            Dim rpt As New rptPhieuThuChi
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenPhieu.Text = "PHIẾU THU"
            rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
            rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
            rpt.lbHoTen.Text = "Người nộp tiền: "
            rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNop")
            rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
            rpt.lbLyDo.Text = "Lý do nộp: "
            rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
            rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
            rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
            rpt.lbNguoiGd.Text = "Người nộp tiền"
            rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNop")
            rpt.lbKTNguoiLap.Text = tb.Rows(0)("NguoiLap")
            rpt.CreateDocument()
            f.printControl.PrintingSystem = rpt.PrintingSystem
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
     
    End Sub


    Private Sub chkXemRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkXemRutGon.CheckedChanged
        If chkXemRutGon.Checked Then
            chkXemRutGon.Glyph = My.Resources.Checked
        Else
            chkXemRutGon.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub mDuKienPhaiThuPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienPhaiThuPhaiTra.ItemClick
        If mDuKienPhaiThuPhaiTra.Tag = "Thu" Then
            If gdvPhaiThuCT.FocusedRowHandle < 0 Then Exit Sub
            Dim f As New frmDuKienThanhToan
            f._SoPhieuCGDH = gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.FocusedRowHandle, "SoPhieuCG")
            f._SoPhieuXNK = gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.FocusedRowHandle, "SoPhieuXK")
            f._PhaiTra = False
            f._Buoc1 = False
            f.ShowDialog()
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

Public Class DanhSachPhieu

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

