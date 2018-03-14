Imports BACSOFT.Db.SqlHelper
Imports DevExpress.CodeParser

Public Class frmGiaoNhanViec
    Public _idVattuPhanmem As String
    Public _idChucnang As String
    Public _idPhanhe As String
    Public _recordId As String

    ' Biến này true thì là form giao việc. False thì là form nhận việc.
    Public _giaoViec As Boolean

    ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable
    Public _IdGiaoviec As String

    Public _NgayNhanviec As String
    Public _Coder As String
    Public _Thoihan As String
    Public _NgayHoanthanh As String
    Public _Testers As String
    Public _IdTesters As String
    Public _NguoiHotro As String
    Public _IdNguoiHotro As String
    Public _Files As String
    Public _Ghichu As String

    ' Init dữ liệu cập nhật khi mở form giao việc lại
    Sub KhoitaoDulieuForm()
        If _NgayNhanviec IsNot Nothing Then
            txtNgaynhanviec.EditValue = DateTime.Parse(_NgayNhanviec)
        End If

        txtThoihan.Text = _Thoihan

        If _NgayNhanviec IsNot Nothing Then
            txtNgayhoanthanh.EditValue = DateTime.Parse(_NgayHoanthanh)
        End If

        cboNguoinhanviec.SelectedText = _Coder
        cboNguoitest.EditValue = _IdTesters
        cboNguoiphoihop.EditValue = _IdNguoiHotro
        txtGhichu.Text = _Ghichu

        If Not _Files Is Nothing Then

            For Each file In _Files.Split(";")
                If file.Trim().Length > 0 Then

                    Dim dr As DataRow = dt.NewRow()
                    dr(0) = file
                    dt.Rows.Add(dr)

                End If
            Next

            gdvFile.DataSource = dt
        End If
    End Sub

    Enum Trangthaichucnang
        Choxuly = 1
        DoiQuantrixacnhan = 2
        DoiCoderxacnhan = 3
        Dangxuly = 4
        Chuyensangtest = 5
        Testcoloi = 6
        Hoanthanh = 7
    End Enum

    ' Mặc định Coder nhận việc
    Public Sub DefaultNhanviec()
        cboNguoinhanviec.EditValue = cboNguoinhanviec.Properties.GetKeyValueByDisplayText(NguoiDung)
        cboNguoinhanviec.Properties.ReadOnly = True
    End Sub

    ' Fill dữ liệu vào combobox Coder, tester, người hỗ trợ
    Sub SelectDSNhanvien()

        Dim tb As DataTable = ExecuteSQLDataTable("exec [sp_Thanhvienduan_IT] @activity = 'xem' , @IdRole = 2, @idvattu = " & _idVattuPhanmem)
        'If Not tb Is Nothing Then

        cboNguoinhanviec.Properties.DataSource = tb

        tb = ExecuteSQLDataTable("exec [sp_Thanhvienduan_IT] @activity = 'xem' , @IdRole = 3, @idvattu = " & _idVattuPhanmem)
        cboNguoitest.Properties.DataSource = tb
        cboNguoitest.Properties.DisplayMember = "Ten"
        cboNguoitest.Properties.ValueMember = "Id"

        tb = ExecuteSQLDataTable("exec [sp_Thanhvienduan_IT] @activity = 'xem' , @IdRole = 5, @idvattu = " & _idVattuPhanmem)
        cboNguoiphoihop.Properties.DataSource = tb
        cboNguoiphoihop.Properties.DisplayMember = "Ten"
        cboNguoiphoihop.Properties.ValueMember = "Id"
        'Else
        'ShowBaoLoi(LoiNgoaiLe)
        'End If
    End Sub

    Private Sub frmGiaoNhanViec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm cột vào table chứa file tạm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        ' Hiển thị mặc định ngày hôm nay.
        txtNgaynhanviec.EditValue = GetServerTime()
        txtNgayhoanthanh.EditValue = GetServerTime()
        '' Focus con trỏ vào textbox txtThoihan
        'txtThoihan.Select()

        ShowWaiting("Đang tải dữ liệu ...")
        SelectDSNhanvien()
        CloseWaiting()

        If _IdGiaoviec <> "" Then
            KhoitaoDulieuForm()
        End If

        If _giaoViec = False Then
            DefaultNhanviec()
        End If
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Function KiemtraDaRaoviec(idChucnang As String) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblGiaoNhanViec_IT where IdChucnang = " & idChucnang & " and _Active = 1")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        On Error Resume Next

        If txtNgaynhanviec.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày nhận việc.")
            Exit Sub
        End If

        If txtNgayhoanthanh.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày hoàn thành.")
            Exit Sub
        End If

        If DateTime.Compare(txtNgaynhanviec.DateTime, txtNgayhoanthanh.DateTime) > 0 Then
            ShowCanhBao("Ngày nhận việc phải nhỏ hơn ngày hoàn thành.")
            Exit Sub
        Else
            If TimeSpan.Compare(txtNgaynhanviec.DateTime.TimeOfDay, txtNgayhoanthanh.DateTime.TimeOfDay) > 0 Then
                ShowCanhBao("Ngày nhận việc phải nhỏ hơn ngày hoàn thành.")
                Exit Sub
            End If
        End If

        If cboNguoinhanviec.EditValue Is Nothing Then
            ShowCanhBao("Bạn phải chọn Coder.")
            Exit Sub
        End If

        'If txtThoihan.EditValue <= 0 Then
        '    MessageBox.Show(Me, "Bạn phải nhập thời hạn hoàn thành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        Dim files = ""
        For i = 0 To gdvFileCT.DataRowCount - 1
            files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
        Next i

        If KiemtraDaRaoviec(_idChucnang) Then
            ' Truyền tham số và giá trị để insert/update dữ liệu
            AddParameter("@Ngaygiaoviec", GetServerTime().ToString("dd/MM/yyyy"))
            AddParameter("@Nguoigiaoviec", TaiKhoan)
            AddParameter("@Ngaynhanviec", txtNgaynhanviec.EditValue)
            AddParameter("@Nguoinhanviec", cboNguoinhanviec.EditValue)
            AddParameter("@Thoihanhoanthanh", txtThoihan.Text)

            AddParameter("@Ngayketthuc", txtNgayhoanthanh.EditValue)
            AddParameter("@IdDanhsachNguoiTest", cboNguoitest.EditValue)
            AddParameter("@IdDanhsachNguoiPhoihop", cboNguoiphoihop.EditValue)
            AddParameter("@DanhsachNguoiTest", cboNguoitest.Text)
            AddParameter("@DanhsachNguoiPhoihop", cboNguoiphoihop.Text)
            AddParameter("@_Active", 1)
            AddParameter("@Filedinhkem", files)
            AddParameter("@Ghichu", txtGhichu.Text)
            AddParameter("@IdChucnang", _idChucnang)
            AddParameterWhere("@Id", _IdGiaoviec)
            'AddParameter("@_Giaoviec", 1)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iDUpdate As Object

            ' Rao việc lại
            _iDUpdate = doUpdate("tblGiaoNhanViec_IT", "Id = @Id")

            ' Báo Email và chỉ báo tại đây

            If _iDUpdate Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()
                ' Tải lại dữ liệu của gridview sau khi thêm mới
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXulyCongviec).SelectDanhmucchucnang(_idPhanhe)
            End If
        Else
            ' Insert data
            ' Truyền tham số và giá trị để insert/update dữ liệu

            AddParameter("@Ngaygiaoviec", GetServerTime().ToString("dd/MM/yyyy"))
            AddParameter("@Nguoigiaoviec", TaiKhoan)
            AddParameter("@Nguoinhanviec", cboNguoinhanviec.EditValue)
            AddParameter("@Ngaynhanviec", txtNgaynhanviec.EditValue)
            AddParameter("@Thoihanhoanthanh", txtThoihan.Text)
            AddParameter("@Ngayketthuc", txtNgayhoanthanh.EditValue)
            AddParameter("@IdDanhsachNguoiTest", cboNguoitest.EditValue)
            AddParameter("@IdDanhsachNguoiPhoihop", cboNguoiphoihop.EditValue)
            AddParameter("@DanhsachNguoiTest", cboNguoitest.Text)
            AddParameter("@DanhsachNguoiPhoihop", cboNguoiphoihop.Text)
            AddParameter("@_Active", 1)
            AddParameter("@Filedinhkem", files)
            AddParameter("@Ghichu", txtGhichu.Text)
            AddParameter("@IdChucnang", _idChucnang)

            If _giaoViec Then
                AddParameter("@_Giaoviec", 1) ' Bản ghi Giao việc
            Else
                AddParameter("@_Giaoviec", 0) ' Bản ghi nhận việc
            End If

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object
            Dim _iDUpdate As Object

            ' Insert bản ghi vào bảng CSDL tblGiaoNhanViec_IT
            _iD = doInsert("tblGiaoNhanViec_IT")

            ' Báo Email và chỉ báo tại đây

            ' Xoá trước đã
            If _giaoViec Then
                _iDUpdate = ExecuteSQLNonQuery("Update tblDSChucnang_IT set TrangthaiChucnang = " & Trangthaichucnang.DoiCoderxacnhan & " where Id = " & _idChucnang.ToString())
            Else
                _iDUpdate = ExecuteSQLNonQuery("Update tblDSChucnang_IT set TrangthaiChucnang = " & Trangthaichucnang.DoiQuantrixacnhan & " where Id = " & _idChucnang.ToString())
            End If

            If _iD Is Nothing Or _iDUpdate Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()

                If _giaoViec Then
                    ' Gửi người nhận việc
                    Dim tb As DataTable = ExecuteSQLDataTable("exec sp_QuanlyVattu_Phanmem_IT @activity = 'email', @recordid = '" & _idVattuPhanmem & "', @idNhansu = '" & cboNguoinhanviec.EditValue & "'")
                    Dim strEmail = ""
                    If tb.Rows.Count > 0 Then
                        For Each item As DataRow In tb.Rows
                            strEmail = item(3).ToString()
                        Next
                    End If

                    Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), "Bộ phận IT-Phần mềm BAC thông báo giao việc", "Bộ phận IT-Phần mềm BAC thông báo cho bạn biết: Đã có công việc được giao cho bạn. Bạn truy cập ngay phần mềm BAC để xem chi tiết công việc.".ToString.Replace(Chr(10), " <BR /> "), EmailNguoiDung)
                End If

                ' Tải lại dữ liệu của gridview sau khi thêm mới
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXulyCongviec).SelectDanhmucchucnang(_idPhanhe)
            End If
        End If

        Close()
    End Sub

    Private Sub txtThoihan_EditValueChanged(sender As Object, e As EventArgs) Handles txtThoihan.EditValueChanged
        If Not txtThoihan.Text Is Nothing Then
            If txtThoihan.Text.Trim() <> "" Then
                Dim temp = txtNgaynhanviec.DateTime.AddDays(txtThoihan.Text)
                txtNgayhoanthanh.DateTime = temp
            Else
                txtNgayhoanthanh.EditValue = ""
            End If
        Else
            txtNgayhoanthanh.EditValue = ""
        End If
    End Sub

    Private Sub btThemFile_Click(sender As Object, e As EventArgs) Handles btThemFile.Click
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()

            If Not IO.Directory.Exists(UrlITPhanmem) Then
                IO.Directory.CreateDirectory(UrlITPhanmem)
            End If

            For Each file In openFile.FileNames
                    ShowWaiting("Đang chuyển file lên server ...")
                    Try
                        IO.File.Copy(file, UrlITPhanmem & IO.Path.GetFileName(file), true)
                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try
                    CloseWaiting()

                Dim dr As DataRow = dt.NewRow()
                dr(0) = IO.Path.GetFileName(file)
                dt.Rows.Add(dr)
            Next

            Impersonator.EndImpersonation()
            gdvFile.DataSource = dt
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As Object, e As EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        gdvFileCT.DeleteSelectedRows()
    End Sub

    Private Sub cboNguoitest_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles cboNguoitest.CustomDisplayText
        If _Testers <> "" Then
            e.DisplayText = _Testers
        End If
    End Sub

    Private Sub cboNguoiphoihop_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles cboNguoiphoihop.CustomDisplayText
        If _NguoiHotro <> "" Then
            e.DisplayText = _NguoiHotro
        End If
    End Sub
End Class