Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmChiPhi

    Private Sub frmChiPhi_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        LoadPhongBan()
        Application.DoEvents()
        LoadTakeCare()
        Application.DoEvents()
        'LoadCbVC()
        LoadcbDVVC()
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbTakeCare.Enabled = True
            cbPhong.Enabled = True
        Else
            cbTakeCare.Enabled = False
            cbPhong.Enabled = False
            'colTienGoc.Visible = False
            'colChietKhau.Visible = False
            'colPTCK.Visible = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            gdvCT.OptionsBehavior.ReadOnly = True
            btLuuLai.Visibility = XtraBars.BarItemVisibility.Never
        End If
        btXem.PerformClick()
    End Sub

    Public Sub LoadcbDVVC()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE ttcKhachHang=3 OR ID IN (SELECT DISTINCT IDDVVC FROM CHIPHI) ORDER BY ttcMa")
        If Not tb Is Nothing Then
            rcbDoiTuong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTakeCare()
        Dim sql As String = ""
        If cbPhong.EditValue Is Nothing Then
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID"
        Else
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment= " & cbPhong.EditValue & " ORDER BY ID "
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbTakeCare.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbTakeCare.EditValue = TaiKhoan
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakeCare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakeCare.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        Dim sql As String = ""
        sql &= " SELECT DISTINCT CHIPHI.ID,SoBill,CHIPHI.IDDVVC,ThoiGian,CHIPHI.SoTien,SoTienTC,tblTienTe.Ten AS TienTe,CHIPHI.TyGia,"
        sql &= " (CASE WHEN (CASE CHIPHI.Loai WHEN 0 then 'NK' + CHIPHI.PhieuTC WHEN 1 THEN 'XK' + CHIPHI.PhieuTC END) IS NULL THEN "
        sql &= "    (CASE CHIPHI.Loai WHEN 0 then 'DH' + CHIPHI.PhieuCGDH WHEN 1 THEN 'CG' + CHIPHI.PhieuCGDH END) ELSE"
        sql &= " (CASE CHIPHI.Loai WHEN 0 then 'NK' + CHIPHI.PhieuTC WHEN 1 THEN 'XK' + CHIPHI.PhieuTC END) END)PhieuTC,"
        sql &= " CanNang,GhiChu,CHIPHI.MucDich,KHACHHANG.ttcMa,CHIPHI.IDDVVC,"
        sql &= " MUCDICHTHUCHI.Ten AS TenMucDich,NHANSU.Ten as PhuTrach,CHIPHI.Loai,CHIPHI.PhieuTC as PhieuTC2,"
        sql &= " (CASE WHEN CHI.ID IS NULL THEN (CASE WHEN UNC.ID IS NULL THEN Convert(bit,0) ELSE Convert(bit,1) END) ELSE Convert(bit,1) END) as DaChi,PhieuCGDH,PhieuTC as PhieuTC2,Convert(bit,0)Modify"
        sql &= " FROM CHIPHI"
        sql &= " INNER JOIN MUCDICHTHUCHI ON CHIPHI.MucDich = MUCDICHTHUCHI.ID"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=CHIPHI.IDUser"
        If Not cbTakeCare.EditValue Is Nothing Then
            AddParameterWhere("@TK", cbTakeCare.EditValue)
            sql &= " AND CHIPHI.IDUser=@TK "
        Else
            If Not cbPhong.EditValue Is Nothing Then
                AddParameterWhere("@IDP", cbPhong.EditValue)
                sql &= " AND NHANSU.IDDepatment=@IDP"
            End If
        End If
        sql &= " INNER JOIN KHACHHANG ON CHIPHI.IDDVVC = KHACHHANG.ID"
        sql &= " INNER JOIN tblTienTe ON CHIPHI.TienTe = tblTienTe.ID"
        sql &= " LEFT JOIN CHI ON CHI.SoTien=CHIPHI.SoTien*CHIPHI.TyGia AND ChiPhi.Loai<>CHI.ChiPhiNhap AND (CHIPHI.PhieuTC=CHI.PhieuTC1 OR CHIPHI.PhieuCGDH=CHI.PHIEUTC0) AND CHI.MucDich=CHIPHI.MucDich"
        sql &= " LEFT JOIN UNC ON UNC.SoTien=CHIPHI.SoTien*CHIPHI.TyGia AND ChiPhi.Loai<>UNC.ChiPhiNhap AND (CHIPHI.PhieuTC=UNC.PhieuTC1 OR CHIPHI.PHieuCGDH=CHI.PHIEUTC0) AND UNC.MucDich=CHIPHI.MucDich"
        sql &= " WHERE 1=1 "

        sql &= " AND convert(datetime,convert(nvarchar, CHIPHI.ThoiGian,103),103) BETWEEN @TuNgay AND @DenNgay"
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        Select Case cbTieuChi.EditValue
            Case "Xuất kho"
                sql &= " AND CHIPHI.Loai=1 "
            Case "Nhập kho"
                sql &= " AND CHIPHI.Loai=0 "
            Case "Khác"
                sql &= " AND CHIPHI.Loai=NULL "
        End Select
        sql &= " ORDER BY CHIPHI.ThoiGian DESC"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "SoTien" Then
            If gdvCT.GetRowCellValue(e.RowHandle, "SoTienTC") <> e.CellValue Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If e.Control And e.KeyCode = Keys.Delete Then
                If gdvCT.GetFocusedRowCellValue("SoTienTC") <> gdvCT.GetFocusedRowCellValue("SoTien") Then
                    If ShowCauHoi("Đây là chi phí gộp nên sẽ xóa toàn bộ chi phí liên quan, bạn có xóa không ?") Then
                        AddParameterWhere("@Bill", gdvCT.GetFocusedRowCellValue("SoBill"))
                        AddParameterWhere("@IDDV", gdvCT.GetFocusedRowCellValue("IDDVVC"))
                        AddParameterWhere("@TG", gdvCT.GetFocusedRowCellValue("ThoiGian"))
                        If doDelete("CHIPHI", "SoBill=@Bill AND IDDVVC=@IDDV AND ThoiGian=@TG") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            ShowAlert("Đã xóa !")
                            btXem.PerformClick()
                        End If
                    End If
                Else
                    If ShowCauHoi("Xóa chi phí cho " & gdvCT.GetFocusedRowCellValue("ttcMa") & " của " & gdvCT.GetFocusedRowCellValue("PhuTrach") & " ?") Then
                        AddParameterWhere("@UID", gdvCT.GetFocusedRowCellValue("ID"))
                        If doDelete("CHIPHI", "ID=@UID") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            ShowAlert("Đã xóa !")
                            btXem.PerformClick()
                        End If
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Chi phi.xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
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

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mLapPhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLapPhieuChi.ItemClick, mLapUNC.ItemClick
        If gdvCT.SelectedRowsCount = 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) Then Exit Sub
        'TrangThai.isAddNew = True
        Dim _Loai As Object = gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "Loai")
        Dim _ttcMa As Object = gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "ttcMa")
        Dim _mucDich As Object = gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "MucDich")
        For i As Integer = 1 To gdvCT.SelectedRowsCount - 1
            If _Loai <> gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "Loai") Then
                ShowCanhBao("Chỉ áp dụng với cùng một mục đích chi cho nhập hoặc xuất hàng !")
                Exit Sub
            End If

            If _ttcMa <> gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ttcMa") Then
                ShowCanhBao("Chỉ áp dụng với cùng một đơn vị vận chuyển !")
                Exit Sub
            End If

            If _mucDich <> gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "MucDich") Then
                ShowCanhBao("Chỉ áp dụng với cùng một mục đích chi trả !")
                Exit Sub
            End If
        Next
        ShowWaiting("Đang tải dữ liệu ...")
        Dim f As New frmCNChi2
        f._TrangThai.isAddNew = True
        If e.Item.Name = mLapUNC.Name Then
            f.UNC = True
        End If
        f.chkChiPhiNhap.Checked = Not gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "Loai")
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu chi"
        f.Show()
        f.gdvMaKH.EditValue = gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "IDDVVC")
        f.cbNguoiNhan.EditValue = gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "PhuTrach")
        f.cbMucDich.EditValue = 205
        f.tbDienGiai.EditValue = "Chi phí vận chuyển"
        For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
            f.gdvData.AddNewRow()
            If gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "PhieuTC2").ToString.Trim = "" Then
                f.gdvData.SetFocusedRowCellValue("PhieuDH", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "PhieuCGDH"))
                f.gdvData.SetFocusedRowCellValue("PhieuNK", "000000000")
            Else
                f.gdvData.SetFocusedRowCellValue("PhieuDH", "000000000")
                f.gdvData.SetFocusedRowCellValue("PhieuNK", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "PhieuTC2"))
            End If

            f.gdvData.SetFocusedRowCellValue("SoTien", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "SoTien") * gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TyGia"))
            f.gdvData.SetFocusedRowCellValue("Tamung", False)
            f.gdvData.SetFocusedRowCellValue("NoiDung", "Chi phí vận chuyển cho " & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "PhieuTC").ToString & " " & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "GhiChu"))

            ' f.gdvData.SetFocusedRowCellValue("ChiPhiNhap", Not gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "Loai"))
        Next
        CloseWaiting()

    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(tbDenNgay.EditValue)
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName <> "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Exit Sub
        End If
        If ShowCauHoi("Lưu lại nội dung thay đổi ?") Then

            Try
                gdvCT.CloseEditor()
                gdvCT.BeginUpdate()
                For i As Integer = 0 To gdvCT.RowCount - 1
                    If gdvCT.GetRowCellValue(i, "Modify") Then
                        AddParameter("@IDDVVC", gdvCT.GetRowCellValue(i, "IDDVVC"))
                        AddParameter("@SoBill", gdvCT.GetRowCellValue(i, "SoBill"))
                        AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                        ' AddParameter("@PhieuTC", gdvCT.GetRowCellValue(i, "PhieuTC"))
                        AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "ID"))
                        If doUpdate("CHIPHI", "ID=@IDD") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvCT.SetRowCellValue(i, "Modify", False)
                        End If
                    End If
                Next
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                gdvCT.CloseEditor()
                gdvCT.EndUpdate()
            End Try

            ShowAlert("Đã thực hiện !")

        End If
    End Sub
End Class
