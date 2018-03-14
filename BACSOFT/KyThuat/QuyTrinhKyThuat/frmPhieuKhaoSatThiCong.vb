Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Public Class frmPhieuKhaoSatThiCong
    Public _tag As Object
    Public _soyc As Object
    Public _id As Object

    Private Sub frmPhieuKhaoSatThiCong_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _tag = "mYeuCauDen" Then
            btnLuu.Enabled = False
            btThemFile.Enabled = False
            btXoaFile.Enabled = False
        End If
        Loaddulieu()
        loadcb()
    End Sub

    Public Sub Loaddulieu()
        If _soyc Is Nothing Or IsDBNull(_soyc) Then
            Exit Sub
        End If

        Dim dt As New DataTable
        Dim sql1 As String
        If _tag = "mYeuCauDen" Or _tag = "mXuLyYCCongTrinh" Then
            sql1 = "SELECT BYC.Sophieu as Masodathang,KH.Ten AS TenKH, NS.ID as NguoiGD, BYC.IDkhachhang as MaKH, BYC.NoiDung,  NS.Email, NS.Mobile, KH.IDLinhVucSX, BYC.IDTakecare, ISNULL(BYC.IDNhanXL,0) as IDNhanXL, NS.Chucvu, KH.ttcDiachi as DiaChi  FROM BANGYEUCAU BYC LEFT JOIN KHACHHANG KH ON KH.ID = BYC.IDkhachhang LEFT JOIN NHANSU NS ON NS.ID = BYC.IDNgd WHERE BYC.Sophieu = @soycden"
        Else
            sql1 = "SELECT ISNULL(BCG.Masodathang,0) as Masodathang ,KH.Ten AS TenKH, BCG.IDkhachhang as MaKH, BCG.TenDuan AS NoiDung, NS.ID as NguoiGD, NS.Email, NS.Mobile, KH.IDLinhVucSX, BCG.IDTakecare, ISNULL(BCG.IDNgNhanXL,0) as IDNhanXL, NS.Chucvu, KH.ttcDiachi as DiaChi  FROM BANGCHAOGIA BCG LEFT JOIN KHACHHANG KH ON KH.ID = BCG.IDkhachhang LEFT JOIN NHANSU NS ON NS.ID = BCG.IDNgd WHERE BCG.Sophieu = @soycden"
        End If
        AddParameterWhere("@soycden", _soyc.ToString)
        dt = ExecuteSQLDataTable(sql1)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        If _tag <> "mYeuCauDen" Then
            If dt.Rows(0)("IDNhanXL") Is Nothing Or IsDBNull(dt.Rows(0)("IDNhanXL")) Then
                btnLuu.Enabled = False
                btThemFile.Enabled = False
                btXoaFile.Enabled = False
            Else
                If dt.Rows(0)("IDNhanXL") <> Convert.ToInt32(TaiKhoan) Then
                    btnLuu.Enabled = False
                    btThemFile.Enabled = False
                    btXoaFile.Enabled = False
                End If
            End If
        End If

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim tb2 As DataTable = DataSourceDSFile(dt.Rows(i)("IDLinhVucSX").ToString)
                dt.Rows(i)("IDLinhVucSX") = ""
                For j As Integer = 0 To tb2.Rows.Count - 1
                    AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                    Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT NoiDung FROM tblTuDien WHERE Loai = 14 AND ID=@ID")
                    If Not tb3 Is Nothing Then
                        dt.Rows(i)("IDLinhVucSX") &= tb3.Rows(0)(0).ToString & "; "
                    End If
                Next
                dt.Rows(i)("IDLinhVucSX") = dt.Rows(i)("IDLinhVucSX").ToString.Trim
            Next
            txtMaKH.EditValue = dt.Rows(0)("MaKH")
            txtTenKH.EditValue = dt.Rows(0)("TenKH").ToString
            txtChucDanh.EditValue = dt.Rows(0)("Chucvu").ToString
            txtDiaChi.EditValue = dt.Rows(0)("DiaChi").ToString
            txtSDT.EditValue = dt.Rows(0)("Mobile").ToString
            txtEmail.EditValue = dt.Rows(0)("Email").ToString
            cbTakeCare.EditValue = dt.Rows(0)("IDTakecare")
            If _tag = "mCongTrinhCanXuLy" Then
                If dt.Rows(0)("Masodathang") <> 0 Then
                    txtSoYC.EditValue = dt.Rows(0)("Masodathang") & "-" & _soyc
                Else
                    txtSoYC.EditValue = "CG" & _soyc
                End If
            Else
                txtSoYC.EditValue = _soyc
            End If
            _soyc = dt.Rows(0)("Masodathang")
            txtLinhVucHD.EditValue = dt.Rows(0)("IDLinhVucSX")
            txtTenDA.EditValue = dt.Rows(0)("NoiDung")
            cbNguoiGD.EditValue = dt.Rows(0)("NguoiGD")

        End If

        'Kiểm tra đã tồn tại khảo sát thi công chưa?
        Dim dt_ks As New DataTable
        Dim sql As String
        sql = "SELECT * FROM KhaoSatThiCong WHERE SoYCDen = @soyc"
        AddParameterWhere("@soyc", _soyc)
        dt_ks = ExecuteSQLDataTable(sql)
        If dt_ks Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        If dt_ks.Rows.Count > 0 Then
            _id = dt_ks.Rows(0)("ID")
            memoNoiDung.EditValue = dt_ks.Rows(0)("NoiDungChung")
            txtTienDo.EditValue = dt_ks.Rows(0)("TienDo")
            memoViTriTC.EditValue = dt_ks.Rows(0)("ViTriTC")
            memoThoiGianTC.EditValue = dt_ks.Rows(0)("ThoiGianTC")
            memoDieuKienTC.EditValue = dt_ks.Rows(0)("DieuKienTC")
            txtNguonDienTC.EditValue = dt_ks.Rows(0)("NguonDienTC")
            txtTenDA.EditValue = dt_ks.Rows(0)("TenDuAn")
            txtThuTuc.EditValue = dt_ks.Rows(0)("ThuTuc")
            txtKhongGianLV.EditValue = dt_ks.Rows(0)("KhongGianLV")
            txtHoTroKhac.EditValue = dt_ks.Rows(0)("HoTroKhac")
            memoGhiChu.EditValue = dt_ks.Rows(0)("GhiChu")
            popupFileDinhKem.EditValue = dt_ks.Rows(0)("FileDinhKem")
        End If

    End Sub

    Public Sub loadcb()
        Dim dt As New DataTable
        dt = ExecuteSQLDataTable(" SELECT ID, Ten FROM NHANSU ")
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        cbTakeCare.Properties.DataSource = dt
        cbNguoiGD.Properties.DataSource = dt
    End Sub

    Private Sub btnLuu_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLuu.ItemClick
        Dim tg = GetServerTime()
        AddParameter("@TenDuAn", txtTenDA.EditValue)
        AddParameter("@NoiDungChung", memoNoiDung.EditValue)
        AddParameter("@TienDo", txtTienDo.EditValue)
        AddParameter("@ViTriTC", memoViTriTC.EditValue)
        AddParameter("@ThoiGianTC", memoThoiGianTC.EditValue)
        AddParameter("@DieuKienTC", memoDieuKienTC.EditValue)
        AddParameter("@NguonDienTC", txtNguonDienTC.EditValue)
        AddParameter("@ThuTuc", txtThuTuc.EditValue)
        AddParameter("@KhongGianLV", txtKhongGianLV.EditValue)
        AddParameter("@HoTroKhac", txtHoTroKhac.EditValue)
        AddParameter("@GhiChu", memoGhiChu.EditValue)
        AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
        If _id Is Nothing Or IsDBNull(_id) Then
            AddParameter("@SoYCDen", _soyc)
            _id = doInsert("KhaoSatThiCong")
            If _id Is Nothing Then
                ShowBaoLoi("Lỗi khi lưu dữ liệu")
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            ShowAlert("Lưu dữ liệu thành công")
        Else
            AddParameterWhere("@IDKS", _id)
            If doUpdate("KhaoSatThiCong", "ID = @IDKS") Is Nothing Then
                ShowBaoLoi("Lỗi khi lưu dữ liệu")
                Exit Sub
            End If
            ShowAlert("Lưu dữ liệu thành công")
        End If
        ' Lưu lịch sử thay đổi
        AddParameter("@TenDuAn", txtTenDA.EditValue)
        AddParameter("@NoiDungChung", memoNoiDung.EditValue)
        AddParameter("@TienDo", txtTienDo.EditValue)
        AddParameter("@ViTriTC", memoViTriTC.EditValue)
        AddParameter("@ThoiGianTC", memoThoiGianTC.EditValue)
        AddParameter("@DieuKienTC", memoDieuKienTC.EditValue)
        AddParameter("@NguonDienTC", txtNguonDienTC.EditValue)
        AddParameter("@ThuTuc", txtThuTuc.EditValue)
        AddParameter("@KhongGianLV", txtKhongGianLV.EditValue)
        AddParameter("@HoTroKhac", txtHoTroKhac.EditValue)
        AddParameter("@GhiChu", memoGhiChu.EditValue)
        AddParameter("@IDKhaoSatTC", _id)
        AddParameter("@NgaySua", tg)
        AddParameter("@IDNguoiSua", Convert.ToInt32(TaiKhoan))
        AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
        doInsert("KhaoSatThiCong_LichSu")
    End Sub
    Private Sub btThemFile_Click(sender As Object, e As EventArgs) Handles btThemFile.Click
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If _id Is Nothing Or IsDBNull(_id) Then
            ShowCanhBao("Chưa lưu bản ghi. Bạn không thể tải file đính kèm.")
            Exit Sub
        End If
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(UrlQuyTrinhKyThuat & _soyc) Then
                System.IO.Directory.CreateDirectory(UrlQuyTrinhKyThuat & _soyc)
            End If
            For Each file In openFile.FileNames
                Try
                    path = TaiKhoan.ToString & IO.Path.GetFileName(file)
                    If System.IO.File.Exists(UrlQuyTrinhKyThuat & _soyc & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            IO.File.Copy(file, UrlQuyTrinhKyThuat & _soyc & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                            popupFileDinhKem.EditValue &= path & ";"
                            AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
                            AddParameterWhere("@ID", _id)
                            If doUpdate("KhaoSatThiCong", "ID = @ID") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                                Exit Sub
                            End If
                        End If
                    Else
                        IO.File.Copy(file, UrlQuyTrinhKyThuat & _soyc & "\" & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                        popupFileDinhKem.EditValue &= path & ";"
                        AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
                        AddParameterWhere("@ID", _id)
                        If doUpdate("KhaoSatThiCong", "ID = @ID") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            Exit Sub
                        End If
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            CloseWaiting()
            Impersonator.EndImpersonation()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As Object, e As EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub

        If ShowCauHoi("Xoá file được chọn ?") Then

            Try
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                IO.File.Delete(UrlQuyTrinhKyThuat & _soyc & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Impersonator.EndImpersonation()
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
            gdvListFileCT.DeleteSelectedRows()
            popupFileDinhKem.EditValue = ""
            For i = 0 To gdvListFileCT.RowCount - 1
                popupFileDinhKem.EditValue &= gdvListFileCT.GetRowCellValue(i, "File") & ";"
            Next

            AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
            AddParameterWhere("@ID", _id)
            If doUpdate("KhaoSatThiCong", "ID = @ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            ShowAlert("Xóa file thành công!")
        End If
    End Sub

    Private Sub popupFileDinhKem_Popup(sender As System.Object, e As System.EventArgs) Handles popupFileDinhKem.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()

    End Sub

    Private Sub gdvListFileCT_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gdvListFileCT.ShowingEditor
        e.Cancel = True
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(UrlQuyTrinhKyThuat & _soyc & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnIn.ItemClick
        Try
            Dim tg = GetServerTime()

            Dim sql As String = "SELECT ISNULL(BCG.Masodathang,0) as Masodathang ,KH.Ten AS TenKH, BCG.IDkhachhang as MaKH, BCG.TenDuan AS NoiDung, NS1.Ten AS NguoiGD, NS1.Email, NS1.Mobile, KH.IDLinhVucSX, BCG.IDTakecare, ISNULL(BCG.IDNgNhanXL,0) as IDNhanXL, NS1.Chucvu, KH.ttcDiachi as DiaChi, NS2.Ten AS TakeCare, KSCG.* FROM BANGCHAOGIA BCG "
            sql &= " LEFT JOIN KHACHHANG KH ON KH.ID = BCG.IDkhachhang "
            sql &= " LEFT JOIN NHANSU NS1 ON NS1.ID = BCG.IDNgd "
            sql &= " LEFT JOIN NHANSU NS2 ON NS2.ID = BCG.IDtakecare"
            sql &= " INNER JOIN KhaoSatThiCong KSCG ON KSCG.SoYCDen = BCG.Masodathang WHERE BCG.Masodathang = @soycden "
            AddParameterWhere("@soycden", _soyc)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            If dt.Rows.Count = 0 Then
                ShowThongBao("Chưa có phiếu khảo sát thi công cho yêu cầu này")
                Exit Sub
            End If
            ShowWaiting("Đang tải nội dung ...")
            Dim f As New frmIn("Xuất hóa đơn")
            Dim rpt As New rptPhieuKhaoSatThiCong
            rpt.lblTenKH.Text = dt.Rows(0)("TenKH").ToString
            rpt.lblDiaChi.Text = dt.Rows(0)("DiaChi").ToString
            rpt.lblNguoiGD.Text = dt.Rows(0)("NguoiGD").ToString
            rpt.lblSDTGD.Text = dt.Rows(0)("Mobile").ToString
            rpt.lblChucVuGD.Text = dt.Rows(0)("Chucvu").ToString
            rpt.lblTenDuAn.Text = dt.Rows(0)("TenDuAn").ToString
            rpt.lblNoiDungChung.Text = dt.Rows(0)("NoiDungChung").ToString
            rpt.lblTienDo.Text = dt.Rows(0)("TienDo").ToString
            rpt.lblViTriTC.Text = dt.Rows(0)("ViTriTC").ToString
            rpt.lblThoiGianTC.Text = dt.Rows(0)("ThoiGianTC").ToString
            rpt.lblDieuKienTC.Text = dt.Rows(0)("DieuKienTC").ToString
            rpt.lblNguonDienTC.Text = dt.Rows(0)("NguonDienTC").ToString
            rpt.lblThuTuc.Text = dt.Rows(0)("ThuTuc").ToString
            rpt.lblKhongGianLV.Text = dt.Rows(0)("KhongGianLV").ToString
            rpt.lblHoTroKhac.Text = dt.Rows(0)("HoTroKhac").ToString
            rpt.lblGhiChu.Text = dt.Rows(0)("GhiChu").ToString

            rpt.lblNgaythang.Text = "Ngày " & tg.Day & " tháng " & tg.Month & " năm " & tg.Year
            rpt.lblKD.Text = dt.Rows(0)("TakeCare").ToString
            rpt.lblsoyeucau.Text &= _soyc
            rpt.RequestParameters = False
            rpt.CreateDocument()
            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

End Class