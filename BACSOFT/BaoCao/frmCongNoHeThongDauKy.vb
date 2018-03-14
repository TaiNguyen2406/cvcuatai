Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid


Public Class frmCongNoHeThongDauKy

    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _DaThu As Double = 0

    Private Sub frmCongNo_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim tgNgayHienTai As Date = GetServerTime()
        txtNgayTinhQuaHan.EditValue = tgNgayHienTai
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btfilterTakecare.Enabled = False
            btKetXuat.Enabled = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gdvPhaiThuCT.Columns("NgayGiaHan").OptionsColumn.ReadOnly = True
            gdvPhaiThuCT.Columns("NgayGiaHan").OptionsColumn.AllowEdit = False
        End If

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

    Private Sub LoadPhaiThu()

        ShowWaiting("Đang tải dữ liệu ...")

        Dim sql As String = txtSQL_PhaiThu.Text
        sql = sql.Replace("01/01/2017", Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("31/12/2017", Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("set @NgayXem = getdate()", "set @NgayXem = '" & Convert.ToDateTime(txtNgayTinhQuaHan.EditValue).ToString("dd/MM/yyyy") & "'")
        sql = sql.Replace("set @LaiSuat = 12/100.0", "set @LaiSuat = " & tbLaiSuat.EditValue & "/100.0")
        Dim _strDK As String = ""

        If Not btfilterMaKH.EditValue Is Nothing Then
            _strDK &= " AND phieuxuatkho.IDkhachhang=" & btfilterMaKH.EditValue & " "
        End If
        If Not btfilterTakecare.EditValue Is Nothing Then
            _strDK &= " AND phieuxuatkho.IDTakecare=" & btfilterTakecare.EditValue & " "
        End If
        sql = sql.Replace(" AND 1 = 1 ", _strDK)

        Dim sqlLocMau As String = "select * from @tblKQ order by convert(bigint,SoPhieuGroup),SoTT,NgayThuTien"
        Dim sqlLoc As String = ""
        If chkLocdo.Checked Then
            sqlLoc = " WHERE ConNo > 0 AND NgayQH > 0 "
        ElseIf chkLocXanh.Checked Then
            sqlLoc = " WHERE ConNo < 0  "
        ElseIf chkLocVang.Checked Then
            sqlLoc = " WHERE ConNo > 0  AND NgayQH <= 0 AND NgayQH >= -10 "
        End If
        If sqlLoc.Trim <> "" Then
            sql = sql.Replace(sqlLocMau, "select * from @tblKQ where SoPhieuGroup in (select SoPhieuGroup from @tblKQ " & sqlLoc & ") order by convert(bigint,SoPhieuGroup),SoTT,NgayThuTien")
        End If


        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        If Not dt Is Nothing Then
            'tính lũy kế
            Dim _luyke As Double = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("LoaiPhieu") = "Phiếu xuất kho" Then
                    _luyke = 0
                    'dt.Rows(i)("ConNo") = tryDouble(dt.Rows(i)("PhaiThu")) - tryDouble(dt.Compute("SUM(TienThu)", "SoPhieuGroup='" & dt.Rows(i)("SoPhieuGroup") & "'"))
                Else
                    Select Case dt.Rows(i)("ThuChi")
                        Case -1
                            _luyke -= tryDouble(dt.Rows(i)("TienThu"))
                        Case Else
                            _luyke += tryDouble(dt.Rows(i)("TienThu"))
                    End Select
                    dt.Rows(i)("LuyKe") = _luyke
                End If
            Next
        End If


        gdvPhaiThu.DataSource = dt

        CloseWaiting()

    End Sub

    Private Function tryDouble(obj As Object) As Double
        If obj Is DBNull.Value Then Return 0
        Try
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Private Sub LoadPhaiTra()

        ShowWaiting("Đang tải dữ liệu ...")

        Dim sql As String = txtSQL_PhaiTra.Text
        sql = sql.Replace("01/01/2017", Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("31/12/2017", Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("set @LaiSuat = 12/100.0", "set @LaiSuat = " & tbLaiSuat.EditValue & "/100.0")
        Dim _strDK As String = ""

        If Not btfilterMaKH.EditValue Is Nothing Then
            _strDK &= " AND phieunhapkho.IDkhachhang=" & btfilterMaKH.EditValue & " "
        End If
        If Not btfilterTakecare.EditValue Is Nothing Then
            _strDK &= " AND phieunhapkho.IDTakecare=" & btfilterTakecare.EditValue & " "
        End If
        sql = sql.Replace(" AND 1 = 1 ", _strDK)

        Dim sqlLocMau As String = "select * from @tblKQ order by convert(bigint,SoPhieuGroup),SoTT,NgayChiTien"
        Dim sqlLoc As String = ""
        If chkLocdo.Checked Then
            sqlLoc = " WHERE ConNo > 0 AND NgayQH > 0 "
        ElseIf chkLocXanh.Checked Then
            sqlLoc = " WHERE ConNo < 0  "
        ElseIf chkLocVang.Checked Then
            sqlLoc = " WHERE ConNo > 0  AND NgayQH <= 0 AND NgayQH >= -10 "
        End If
        If sqlLoc.Trim <> "" Then
            sql = sql.Replace(sqlLocMau, "select * from @tblKQ where SoPhieuGroup in (select SoPhieuGroup from @tblKQ " & sqlLoc & ") order by convert(bigint,SoPhieuGroup),SoTT,NgayChiTien")
        End If


        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If Not dt Is Nothing Then
            'tính lũy kế
            Dim _luyke As Double = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("LoaiPhieu") = "Phiếu nhập kho" Then
                    _luyke = 0
                    'dt.Rows(i)("ConNo") = tryDouble(dt.Rows(i)("PhaiThu")) - tryDouble(dt.Compute("SUM(TienThu)", "SoPhieuGroup='" & dt.Rows(i)("SoPhieuGroup") & "'"))
                Else
                    Select Case dt.Rows(i)("LoaiPhieu")
                        Case "Hoàn tạm ứng tiền mặt", "Hoàn tạm ứng ngân hàng"
                            _luyke -= tryDouble(dt.Rows(i)("TienTra"))
                        Case Else
                            _luyke += tryDouble(dt.Rows(i)("TienTra"))
                    End Select

                    dt.Rows(i)("LuyKe") = _luyke
                End If
            Next
        End If


        gdvPhaiTra.DataSource = dt

        CloseWaiting()

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

    Private Sub gdvPhaiThuCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiThuCT.RowCellStyle
        On Error Resume Next

        If e.Column.FieldName = "ConNo" Then
            If Not IsDBNull(e.CellValue) Then
                If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
                    e.Appearance.BackColor = Color.Red
                    e.Appearance.ForeColor = ColorTranslator.FromHtml("#f2f2f2")
                ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
                    e.Appearance.BackColor = Color.Cyan
                ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") <= 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") >= -10 Then
                    e.Appearance.BackColor = Color.Yellow
                Else
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#f2f2f2")
                End If
            End If

        End If

    End Sub

    Private Sub gdvPhaiTraCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiTraCT.RowCellStyle

        On Error Resume Next

        If e.Column.FieldName = "ConNo" Then
            If Not IsDBNull(e.CellValue) Then
                If gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
                    e.Appearance.BackColor = Color.Red
                    e.Appearance.ForeColor = ColorTranslator.FromHtml("#f2f2f2")
                ElseIf gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
                    e.Appearance.BackColor = Color.Cyan
                ElseIf gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") <= 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") >= -10 Then
                    e.Appearance.BackColor = Color.Yellow
                Else
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#f2f2f2")
                End If
            End If

        End If

    End Sub

    Private Sub btLocDo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocDo.ItemClick

        If btLocDo.Appearance.Font.Bold Then
            btLocDo.Appearance.Font = New Font(btLocDo.Font, FontStyle.Regular)
        Else
            btLocDo.Appearance.Font = New Font(btLocDo.Font, FontStyle.Bold Or FontStyle.Underline)
        End If
        btLocVang.Appearance.Font = New Font(Me.Font, FontStyle.Regular)
        btLocXanh.Appearance.Font = New Font(Me.Font, FontStyle.Regular)

    End Sub

    Private Sub btLocVang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocVang.ItemClick

        btLocDo.Appearance.BorderColor = Color.White
        btLocVang.Appearance.BorderColor = Color.Yellow
        btLocXanh.Appearance.BorderColor = Color.White

    End Sub

    Private Sub btLocXanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocXanh.ItemClick


        btLocDo.Appearance.BorderColor = Color.White
        btLocVang.Appearance.BorderColor = Color.White
        btLocXanh.Appearance.BorderColor = Color.Green

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
        If gdvPhaiThuCT.FocusedRowHandle < 0 Then
            ShowCanhBao("Vui lòng chọn dòng có mã khách hàng!")
            Exit Sub
        End If
        If gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.FocusedRowHandle, "SoPhieuCG") Is DBNull.Value Then Exit Sub
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
            If gdvPhaiThuCT.GetRowCellValue(HitInfo.RowHandle, "LoaiPhieu").ToString = "Phiếu xuất kho" Then
                mPhaiThu.ShowPopup(gdvPhaiThu.PointToScreen(e.Location))
            End If
        End If

    End Sub

    Private Sub gdvPhaiThuCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvPhaiThuCT.CellValueChanged
        On Error Resume Next

        If e.Column.FieldName = "NgayGiaHan" Then
            AddParameter("@HanThu", e.Value)
            AddParameterWhere("@SP", gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoPhieuXK"))
            If doUpdate("PHIEUXUATKHO", "SoPhieu=@SP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã lưu ngày gia hạn xuất kho " & gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoPhieuXK") & " !")
            End If
        End If
    End Sub

    'Private Sub gdvPhaiThuCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvPhaiThuCT.CustomDrawCell
    '    On Error Resume Next
    '    'If e.RowHandle < 0 Then Exit Sub
    '    'If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "LoaiPhieu") = "Phiếu xuất kho" Then

    '    'Else
    '    '    'e.Appearance.BackColor = ColorTranslator.FromHtml("#f2f2f2")
    '    'End If

    '    'If gdvPhaiThuCT.FocusedColumn Is e.Column Then
    '    Dim rect As Rectangle = e.Bounds
    '    rect.Inflate(New Size(-1, -1))
    '    e.Graphics.DrawRectangle(New Pen(Color.Red), rect)

    '    e.Appearance.DrawString(e.Cache, e.DisplayText, rect)

    '    'End If

    '    e.Handled = True


    'End Sub



    Private Sub gdvPhaiThu_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles gdvPhaiThu.Paint
        Dim viewInfo As GridViewInfo = CType(gdvPhaiThuCT.GetViewInfo(), GridViewInfo)


        Dim handl As Integer = gdvPhaiThuCT.FocusedRowHandle


        Dim _Top As Integer = handl
        If _Top >= 0 Then
            While gdvPhaiThuCT.GetRowCellValue(_Top, "LoaiPhieu") <> "Phiếu xuất kho" And _Top > 0
                _Top = _Top - 1
            End While

            Dim rowInfo2 As GridRowInfo = viewInfo.GetGridRowInfo(_Top)
            If rowInfo2 Is Nothing Then Exit Sub
            Dim rect2 As System.Drawing.Rectangle = rowInfo2.DataBounds
            Dim pen2 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 2)
            e.Graphics.DrawLine(pen2, rect2.X, rect2.Y, rect2.X + rect2.Width, rect2.Y)
        End If



        Dim _Bottom As Integer = handl + 1

        If _Bottom >= 0 Then

            While gdvPhaiThuCT.GetRowCellValue(_Bottom, "LoaiPhieu") <> "Phiếu xuất kho" And _Bottom < gdvPhaiThuCT.RowCount - 1
                _Bottom = _Bottom + 1
            End While

            Dim rowInfo3 As GridRowInfo = viewInfo.GetGridRowInfo(_Bottom)
            If rowInfo3 Is Nothing Then Exit Sub
            Dim rect3 As System.Drawing.Rectangle = rowInfo3.DataBounds
            Dim pen3 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 2)
            e.Graphics.DrawLine(pen3, rect3.X, rect3.Y, rect3.X + rect3.Width, rect3.Y)

        End If


    End Sub

    Private Sub gdvPhaiThuCT_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvPhaiThuCT.FocusedRowChanged
        gdvPhaiThuCT.Invalidate()
    End Sub

    Private Sub gdvPhaiThuCT_LeftCoordChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhaiThuCT.LeftCoordChanged
        gdvPhaiThuCT.Invalidate()
    End Sub


    'Private Sub gdvPhaiThuCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvPhaiThuCT.RowStyle
    '    Dim viewInfo As GridViewInfo = CType(gdvPhaiThuCT.GetViewInfo(), GridViewInfo)
    '    Dim rowInfo As GridRowInfo = viewInfo.GetGridRowInfo(e.RowHandle)
    '    If rowInfo Is Nothing Then Exit Sub
    '    Dim rect As System.Drawing.Rectangle = rowInfo.DataBounds
    '    Dim pen As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 1)
    '    'e.Graphics.DrawRectangle(pen, rect)
    '    gdvPhaiThu.CreateGraphics().DrawRectangle(pen, rect)
    'End Sub



    'Private Sub gdvPhaiThuCT_CustomDrawRowPreview(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles gdvPhaiThuCT.CustomDrawRowPreview
    '    Dim grid As GridControl = TryCast(sender, GridControl)
    '    Dim view As GridView = TryCast(grid.FocusedView, GridView)
    '    Dim info As GridViewInfo = TryCast(view.GetViewInfo(), GridViewInfo)
    '    Dim colRect As Rectangle = info.RowsInfo(e.RowHandle).Bounds
    '    Dim lineRect As New Rectangle(colRect.Right - 1, colRect.Y, 1, info.ViewRects.Rows.Height + colRect.Height)
    '    e.Graphics.FillRectangle(Brushes.Red, lineRect)
    'End Sub

    'Private Sub gdvPhaiThuCT_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles gdvPhaiThuCT.CellMerge
    '    On Error Resume Next
    '    Select Case e.Column.FieldName
    '        Case "ttcMa", "SoPhieuXK", "NgayXK", "PhaiThu", "KinhDoanh"
    '            If e.CellValue1.ToString <> "" And e.CellValue2.ToString = "" Then
    '                e.Merge = True
    '            Else
    '                e.Merge = False
    '            End If
    '            e.Handled = True
    '    End Select

    'End Sub


    Private Sub gdvPhaiTra_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles gdvPhaiTra.Paint
        Dim viewInfo As GridViewInfo = CType(gdvPhaiTraCT.GetViewInfo(), GridViewInfo)


        Dim handl As Integer = gdvPhaiTraCT.FocusedRowHandle


        Dim _Top As Integer = handl
        If _Top >= 0 Then
            While gdvPhaiTraCT.GetRowCellValue(_Top, "LoaiPhieu") <> "Phiếu nhập kho" And _Top > 0
                _Top = _Top - 1
            End While

            Dim rowInfo2 As GridRowInfo = viewInfo.GetGridRowInfo(_Top)
            If rowInfo2 Is Nothing Then Exit Sub
            Dim rect2 As System.Drawing.Rectangle = rowInfo2.DataBounds
            Dim pen2 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 2)
            e.Graphics.DrawLine(pen2, rect2.X, rect2.Y, rect2.X + rect2.Width, rect2.Y)
        End If



        Dim _Bottom As Integer = handl + 1

        If _Bottom >= 0 Then

            While gdvPhaiTraCT.GetRowCellValue(_Bottom, "LoaiPhieu") <> "Phiếu nhập kho" And _Bottom < gdvPhaiTraCT.RowCount - 1
                _Bottom = _Bottom + 1
            End While

            Dim rowInfo3 As GridRowInfo = viewInfo.GetGridRowInfo(_Bottom)
            If rowInfo3 Is Nothing Then Exit Sub
            Dim rect3 As System.Drawing.Rectangle = rowInfo3.DataBounds
            Dim pen3 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 2)
            e.Graphics.DrawLine(pen3, rect3.X, rect3.Y, rect3.X + rect3.Width, rect3.Y)

        End If


    End Sub

    Private Sub gdvPhaiTraCT_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvPhaiTraCT.FocusedRowChanged
        gdvPhaiTraCT.Invalidate()
    End Sub

    Private Sub gdvPhaiTraCT_LeftCoordChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhaiTraCT.LeftCoordChanged
        gdvPhaiTraCT.Invalidate()
    End Sub



    Private Sub chkLocdo_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocdo.CheckedChanged
        If chkLocdo.Checked Then
            chkLocXanh.Checked = False
            chkLocVang.Checked = False
        End If
        btTaiLai.PerformClick()
    End Sub

    Private Sub chkLocVang_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocVang.CheckedChanged
        If chkLocVang.Checked Then
            chkLocdo.Checked = False
            chkLocXanh.Checked = False
        End If
        btTaiLai.PerformClick()
    End Sub

    Private Sub chkLocXanh_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocXanh.CheckedChanged
        If chkLocXanh.Checked Then
            chkLocVang.Checked = False
            chkLocdo.Checked = False
        End If
        btTaiLai.PerformClick()
    End Sub


    Private Sub gdvPhaiThuCT_ShowingEditor(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles gdvPhaiThuCT.ShowingEditor
        If gdvPhaiThuCT.FocusedColumn.FieldName = "NgayGiaHan" And gdvPhaiThuCT.FocusedRowHandle >= 0 Then
            If gdvPhaiThuCT.GetFocusedRowCellValue("SoPhieuXK").ToString = "" Then
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub mnuLapPhieuThuTienLaiQuaHan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLapPhieuThuTienLaiQuaHan.ItemClick
        'If gdvPhaiThuCT.FocusedRowHandle < 0 Then
        '    ShowCanhBao("Vui lòng chọn dòng có mã khách hàng!")
        '    Exit Sub
        'End If
        'Dim f As New frmCNThu2
        'f._TrangThai.isAddNew = True
        'f.Tag = Me.Parent.Tag
        'f.Text = "Thêm phiếu thu"
        'f.SoPhieuCG = gdvPhaiThuCT.GetFocusedRowCellValue("SoPhieuCG").ToString
        'f.SoPhieuXK = gdvPhaiThuCT.GetFocusedRowCellValue("SoPhieuXK").ToString
        'f.NgayThuTienLai = txtNgayTinhQuaHan.EditValue
        'f.NguoiNop = gdvPhaiThuCT.GetFocusedRowCellValue("KinhDoanh").ToString
        'f.IdKhachHang = gdvPhaiThuCT.GetFocusedRowCellValue("IdKH")
        'f.TienThuLaiQuaHan = gdvPhaiThuCT.GetFocusedRowCellValue("ConThuTienLai")
        'f.Show()
    End Sub
End Class