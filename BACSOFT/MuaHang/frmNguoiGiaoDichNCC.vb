Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid

Public Class frmNguoiGiaoDichNCC

    Private Sub frmNguoiGiaoDichNCC_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        rcbDoiTuongNhanEmail.DataSource = getDataCbDoiTuongNhanEmail()
        cbPhanHoi.DataSource = getDataCbPhanHoi()
        loadDSKhachHang()
        loadDS()
    End Sub

    Public Sub loadDS()
        ShowWaiting("Đang tải danh sách người giao dịch ...")
        Dim sql As String = ""
        sql &= " SELECT NHANSU.ID,NHANSU.Ten,KHACHHANG.ttcMa,NHANSU.Chucvu,NHANSU.Ngaysinh,NHANSU.SoCMT,NHANSU.Ngaycap,NHANSU.Noicap,NHANSU.Diachi,NHANSU.Nguyenquan,"
        sql &= " NHANSU.DienthoaiNR,NHANSU.DienthoaiCQ,NHANSU.Mobile,NHANSU.Mobile1,NHANSU.Fax,NHANSU.Email,NHANSU.Taikhoan,NHANSU.Nganhang,NHANSU.Moi,NHANSU.PhanHoi,"
        sql &= " NHANSU.Chucvu,KHACHHANG.Ten AS KhachHang,TAKECARE.Ten AS TakeCare,NHANSU.Noictac,NHANSU.Email,NHANSU.Trangthai,NHANSU.DoiTuongNhanEmail,NHANSU.XungHo,NHANSU.Chamsoc as IDTakeCare"
        sql &= " FROM NHANSU "
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=NHANSU.Noictac AND (KHACHHANG.ttcKhachHang = 0 OR KHACHHANG.ttcKhachHang =2) "
        sql &= " LEFT OUTER JOIN NHANSU AS TAKECARE ON TAKECARE.ID=NHANSU.Chamsoc "
        sql &= " WHERE NHANSU.Noictac <> 74 "
        If Not cbKhachHang.EditValue Is Nothing Then
            sql &= " AND NHANSU.Noictac= " & cbKhachHang.EditValue
        End If

        sql &= " ORDER BY KhachHang,NHANSU.Ten"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvNgd.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Function getDataCbDoiTuongNhanEmail() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r1 As DataRow
        r1 = tb.NewRow
        r1("ID") = 0
        r1("Ten") = "Chính hãng"
        Dim r2 As DataRow
        r2 = tb.NewRow
        r2("ID") = 1
        r2("Ten") = "Đăng ký không làm phiền"
        Dim r3 As DataRow
        r3 = tb.NewRow
        r3("ID") = 2
        r3("Ten") = "End user - Kỹ thuật"
        Dim r4 As DataRow
        r4 = tb.NewRow
        r4("ID") = 3
        r4("Ten") = "End user - Mua hàng, Lãnh đạo"
        Dim r5 As DataRow
        r5 = tb.NewRow
        r5("ID") = 4
        r5("Ten") = "Thương mại"
        Dim r6 As DataRow
        r6 = tb.NewRow
        r6("ID") = 5
        r6("Ten") = "Trường học"
        Dim r7 As DataRow
        r7 = tb.NewRow
        r7("ID") = 6
        r7("Ten") = "Email lỗi"
        tb.Rows.Add(r1)
        tb.Rows.Add(r2)
        tb.Rows.Add(r3)
        tb.Rows.Add(r4)
        tb.Rows.Add(r5)
        tb.Rows.Add(r6)
        tb.Rows.Add(r7)
        Return tb
    End Function

    Function getDataCbPhanHoi() As DataTable

        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r1 As DataRow
        r1 = tb.NewRow
        r1("ID") = 0
        r1("Ten") = "Tham gia"
        Dim r2 As DataRow
        r2 = tb.NewRow
        r2("ID") = 1
        r2("Ten") = "Tham gia có dùng bữa"
        Dim r3 As DataRow
        r3 = tb.NewRow
        r3("ID") = 2
        r3("Ten") = "Tham gia không dùng bữa"
        Dim r4 As DataRow
        r4 = tb.NewRow
        r4("ID") = 3
        r4("Ten") = "Không tham gia"
        Dim r5 As DataRow
        r5 = tb.NewRow
        r5("ID") = 4
        r5("Ten") = "Xem xét"

        tb.Rows.Add(r1)
        tb.Rows.Add(r2)
        tb.Rows.Add(r3)
        tb.Rows.Add(r4)
        tb.Rows.Add(r5)
        Return tb
    End Function

    Private Sub loadDSKhachHang()
        'ShowWaiting("Đang tải danh sách khách hàng ...")
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE KHACHHANG.ttcKhachHang = 0 OR KHACHHANG.ttcKhachHang =2 ORDER BY ttcMa")
        If Not dt Is Nothing Then
            rcbKhachHang.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        '  CloseWaiting()
    End Sub

    Private Sub cbLoaiKH_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadDS()
    End Sub

    Private Sub gdvKHCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNgdCT.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.N Then
            btThem.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btSua.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.I Then
            colID.Visible = Not colID.Visible
            If colID.Visible Then
                colID.VisibleIndex = 0
            End If
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNNguoiGiaoDich
        f.Tag = Me.Parent.Tag
        f.cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        f.cbNoiCongTac.EditValue = Convert.ToInt32(gdvNgdCT.GetFocusedRowCellValue("Noictac"))
        f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvNgdCT.GetFocusedRowCellValue("Noictac") = 74 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        End If
        TrangThai.isUpdate = True
        Dim Index As Integer = gdvNgdCT.FocusedRowHandle
        MaTuDien = gdvNgdCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNNguoiGiaoDich
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
        gdvNgdCT.FocusedRowHandle = Index
    End Sub

    Private Sub rcbKhachHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbKhachHang.ButtonClick
        If e.Button.Index = 1 Then
            cbKhachHang.EditValue = Nothing
        End If
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        loadDS()
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "DS Nguoi giao dich.xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvNgdCT)
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


    Private Sub btXoaNgd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoaNgd.ItemClick, mXoa.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvNgdCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá người giao dịch " & gdvNgdCT.GetFocusedRowCellValue("Ten") & " ?") Then
            AddParameterWhere("@IDNgd", gdvNgdCT.GetFocusedRowCellValue("ID"))
            Dim sql As String = ""
            sql &= " SELECT COUNT(IDNgd) FROM BANGCHAOGIA WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM BANGYEUCAU WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM PHIEUDATHANG WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM PHIEUXUATKHO WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM PHIEUYEUCAUDI WHERE IDNgd=@IDNgd"
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            Dim count As Integer = 0
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables.Count - 1
                    count += ds.Tables(i).Rows(0)(0)
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            Dim index As Integer = gdvNgdCT.FocusedRowHandle
            If count > 0 Then

                Dim f As New frmThayIDKH
                f.Tag = "NGD"
                f.Text = "Thay thế ID của người gd: " & gdvNgdCT.GetFocusedRowCellValue("Ten")
                f._oldID = gdvNgdCT.GetFocusedRowCellValue("ID")
                f._oldttcMa = gdvNgdCT.GetFocusedRowCellValue("Ten")
                f.ShowDialog()

            Else
                AddParameterWhere("@ID", gdvNgdCT.GetFocusedRowCellValue("ID"))
                If doDelete("NHANSU", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã xoá !")
                    loadDS()
                End If
            End If
            gdvNgdCT.FocusedRowHandle = index
        End If
    End Sub


    Private Sub gdvNgdCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvNgdCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvNgdCT.CalcHitInfo(gdvNgd.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gdvNgd.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvNgdCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvNgdCT.CellValueChanged
        'If e.Column.FieldName = "DoiTuongNhanEmail" Then
        '    AddParameter("@DoiTuongNhanEmail", gdvNgdCT.GetRowCellValue(e.RowHandle, "DoiTuongNhanEmail"))
        '    AddParameterWhere("@IDNV", gdvNgdCT.GetRowCellValue(e.RowHandle, "ID"))
        '    If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
        '        ShowBaoLoi(LoiNgoaiLe)
        '    Else
        '        ShowAlert("Đã cập nhật !")
        '    End If
        'End If
    End Sub

    Private Sub rcbDoiTuongNhanEmail_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles rcbDoiTuongNhanEmail.EditValueChanged
        AddParameter("@DoiTuongNhanEmail", CType(sender, DevExpress.XtraEditors.LookUpEdit).EditValue)
        AddParameterWhere("@IDNV", gdvNgdCT.GetFocusedRowCellValue("ID"))
        If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật !")
        End If

    End Sub

    Private Sub chkMoi_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkMoi.CheckedChanged
        AddParameter("@Moi", CType(sender, DevExpress.XtraEditors.CheckEdit).Checked)
        AddParameterWhere("@IDNV", gdvNgdCT.GetFocusedRowCellValue("ID"))
        If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật !")
        End If
    End Sub

    Private Sub cbPhanHoi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhanHoi.EditValueChanged
        AddParameter("@PhanHoi", CType(sender, DevExpress.XtraEditors.LookUpEdit).EditValue)
        AddParameterWhere("@IDNV", gdvNgdCT.GetFocusedRowCellValue("ID"))
        If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật !")
        End If
    End Sub

    Private Sub cbPhanHoi_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbPhanHoi.ButtonClick
        If e.Button.Index = 1 Then
            AddParameter("@PhanHoi", DBNull.Value)
            AddParameterWhere("@IDNV", gdvNgdCT.GetFocusedRowCellValue("ID"))
            If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật !")
                gdvNgdCT.SetFocusedRowCellValue("PhanHoi", DBNull.Value)
            End If
        End If
    End Sub

    Private Sub btInDiaChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInDiaChi.ItemClick
        If gdvNgdCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvNgdCT.GetFocusedRowCellValue("IDTakeCare")
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvNgdCT.GetFocusedRowCellValue("ID")
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=74 "
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=" & gdvNgdCT.GetFocusedRowCellValue("Noictac")
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmIn("In địa chỉ giao hàng")
            Dim rpt As New rptDiaChiGiaoHang
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenCTyGui.Text = ds.Tables(2).Rows(0)("Ten")
            rpt.lbDCGui.Text = "Đ/C: " & ds.Tables(2).Rows(0)("ttcDCGiaoHang").ToString
            rpt.lbCtyNhan.Text = ds.Tables(3).Rows(0)("Ten")
            rpt.lbDCNhan.Text = "Đ/C: " & ds.Tables(3).Rows(0)("ttcDCGiaoHang").ToString
            rpt.lbNguoiGui.Text = ds.Tables(0).Rows(0)("Ten") & " " & ds.Tables(0).Rows(0)("Mobile").ToString
            Try
                rpt.lbNguoiNhan.Text = ds.Tables(1).Rows(0)("Ten") & " " & ds.Tables(1).Rows(0)("Mobile").ToString
            Catch ex As Exception
                ShowBaoLoi("Không lấy được thông tin người nhận !")
            End Try

            rpt.CreateDocument()
            'f.PrintPreviewBarItem12.
            f.printControl.PrintingSystem = rpt.PrintingSystem

            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Public _SLDT1 As Integer = 0
    Public _SLDT2 As Integer = 0
    Public _Email As Integer = 0
    Public _TongDT1 As Integer = 0
    Public _TongDT2 As Integer = 0
    Public _TongEmail As Integer = 0
    ' Public _SLDT1 As Integer = 0

    Private Sub gdvNgdCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvNgdCT.CustomSummaryCalculate
        'GridView view = sender as GridView;
        Dim view As GridView = CType(sender, GridView)
        ' Get the summary ID.  
        Dim summaryField = CType(e.Item, GridSummaryItem).FieldName

        ' // Initialization.  
        'if (e.SummaryProcess == CustomSummaryProcess.Start) {
        '    discontinuedProductsCount = 0;
        '    totalPrice = 0;
        '}
        'Dim _SL As Integer
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            _SLDT1 = 0
            _SLDT2 = 0
            _Email = 0
            '_TongDT1 = 0
            '_TongDT2 = 0
            '_TongEmail = 0
        End If


        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
            Select Case summaryField
                Case "Mobile"
                    If view.GetRowCellValue(e.RowHandle, "Mobile").ToString.Trim <> "" Then
                        _SLDT1 += 1
                        _TongDT1 += 1
                        Exit Select
                    End If
                Case "Mobile1"
                    If view.GetRowCellValue(e.RowHandle, "Mobile1").ToString.Trim <> "" Then
                        _SLDT2 += 1
                        _TongDT2 += 1
                        Exit Select
                    End If
                Case "Email"
                    If view.GetRowCellValue(e.RowHandle, "Email").ToString.Trim <> "" Then
                        _Email += 1
                        _TongEmail += 1
                        Exit Select
                    End If
            End Select
        End If
        '// Finalization.  

        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
            Select Case summaryField
                Case "Mobile"
                    e.TotalValue = _SLDT1
                    'e.IsTotalSummary = _TongDT1
                    Exit Select
                Case "Mobile1"
                    e.TotalValue = _SLDT2
                    Exit Select
                Case "Email"
                    e.TotalValue = _Email
                    Exit Select
            End Select
        End If
    End Sub
End Class
