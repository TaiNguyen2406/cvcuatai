Imports BACSOFT.Db.SqlHelper
Imports DevExpress.CodeParser

Public Class frmThemBaocaoCongviec
    ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable
    ' Biến này lưu ID của vật tư phần mềm đã chọn
    Public _idVattuPhanmem As String
    Public _recordId As String
    Public _Ngaybaocao As String
    Public _Nguoibaocao As String
    Public _Ngaybatdau As String
    Public _Ngayketthuc As String
    Public _Diadiemthuchien As String
    Public _Thuchien As String
    Public _Loaicongviec As String
    Public _Noidung As String
    Public _Files As String
    Public _Ghichu As String
    ' _flagForm = false thì form làm nhiệm vụ thêm mới. _flagForm = true thì form  làm nhiệm vụ cập nhật
    Public _flagForm As Boolean = False
    Dim _idTempSaved As Object

    ' Khởi tạo dữ liệu cho form khi form làm nhiệm vụ Update
    Sub InitUpdateData()
        txtNgaybaocao.EditValue = DateTime.Parse(_Ngaybaocao)
        txtNguoibaocao.Text = _Nguoibaocao
        txtNgaybatdau.EditValue = DateTime.Parse(_Ngaybatdau)
        txtNgayketthuc.EditValue = DateTime.Parse(_Ngayketthuc)

        txtDiadiem.Text = _Diadiemthuchien
        cboDSNhanvien.SelectedText = _Thuchien
        cboLoaicongviec.EditValue = _Loaicongviec

        txtNoidung.Text = _Noidung
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

    ' Select danh sách nhân viên, và fill vào combobox cboDSNhanvien
    Sub SelectDSNhanvien()
        Dim tb As DataTable = ExecuteSQLDataTable("exec [sp_Thanhvienduan_IT] @activity = 'xem' , @idvattu = " & _idVattuPhanmem)

        If Not tb Is Nothing Then
            cboDSNhanvien.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select danh sachs loại công việc và fill vào combobox cboLoaicongviec
    Sub SelectLoaicongviec()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Tencongviec FROM tblLoaicongviec_IT  WHERE Active = 1")
        If Not tb Is Nothing Then
            cboLoaicongviec.Properties.DataSource = tb
            cboLoaicongviec.ItemIndex = 0
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub frmThemBaocaoCongviec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm cột vào Grid File đính kèm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        txtNgaybatdau.EditValue = GetServerTime()
        txtNgayketthuc.EditValue = GetServerTime()
        txtNgaybaocao.EditValue = GetServerTime()
        txtNguoibaocao.Text = NguoiDung
        txtDiadiem.Select()

        ShowWaiting("Đang tải dữ liệu ...")
        SelectLoaicongviec()
        SelectDSNhanvien()
        CloseWaiting()

        ' Nếu form làm nhiệm vụ update
        If _flagForm = True Then
            InitUpdateData()
            btLuuLai.Text = "Lưu lại"
            btnLuuthem.Visible = False
        Else
            Dim temp = cboDSNhanvien.Properties.GetKeyValueByDisplayText(NguoiDung)
            If Not temp Is Nothing Then
                cboDSNhanvien.SelectedText = NguoiDung
            End If
        End If
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
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

    Function ValidateForm() As Boolean

        If txtNgaybaocao.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày báo cáo.")
            Return False
        End If

        If txtNgaybatdau.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày bắt đầu.")
            Return False
        End If

        If txtNgayketthuc.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày kết thúc.")
            Return False
        End If

        If DateTime.Compare(txtNgaybatdau.DateTime, txtNgayketthuc.DateTime) > 0 Then
            ShowCanhBao("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")
            Return False
        Else
            If TimeSpan.Compare(txtNgaybatdau.DateTime.TimeOfDay, txtNgayketthuc.DateTime.TimeOfDay) > 0 Then
                ShowCanhBao("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")
                Return False
            End If
        End If

        If txtDiadiem.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập địa điểm.")
            Return False
        End If

        If cboDSNhanvien.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa chọn nhân viên thực hiện.")
            Return False
        End If

        If txtNoidung.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập nội dung công việc.")
            Return False
        End If

        Return True
    End Function

    Function SaveForm() As Boolean
        If ValidateForm() = False Then
            Return False
        End If

        If _flagForm = False Then
            ' Nếu Form thêm mới
            Dim files = ""
            For i = 0 To gdvFileCT.DataRowCount - 1
                files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
            Next i

            ' Truyền tham số và giá trị để insert/update dữ liệu
            AddParameter("@IdVattu", _idVattuPhanmem)
            AddParameter("@Ngaybatdau", txtNgaybatdau.EditValue)
            AddParameter("@Ngayketthuc", txtNgayketthuc.EditValue)
            AddParameter("@Diadiemthuchien", txtDiadiem.EditValue)
            AddParameter("@DanhsachNguoithuchien", cboDSNhanvien.EditValue)
            AddParameter("@IdLoaicongviec", cboLoaicongviec.EditValue)
            AddParameter("@Noidungcongviec", txtNoidung.Text)
            AddParameter("@Filedinhkem", files)
            AddParameter("@Ghichu", txtGhichu.Text)
            AddParameter("@IdUser", TaiKhoan)
            AddParameter("@Ngaybaocao", txtNgaybaocao.EditValue)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object

            If _idTempSaved Is Nothing Then
                ' Insert bản ghi vào bảng CSDL tblBaocaocongviec_IT
                _iD = doInsert("tblBaocaocongviec_IT")
            Else
                ' Update
                AddParameterWhere("@Id", _idTempSaved)
                _iD = doUpdate("tblBaocaocongviec_IT", "Id = @Id")
            End If

            If _iD Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")

                ' Xác nhận phiên
                ComitTransaction()

                If _idTempSaved Is Nothing Then
                    _idTempSaved = _iD
                End If

                ' Tải lại dữ liệu của gridview sau khi thêm mới
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachBaocaoCongviec).SelectKhaosat(_idVattuPhanmem)
            End If
        Else
            'Dim ngaybaocao, ngaybatdau, ngayketthuc As String
            'If txtNgaybaocao.Text.Contains("SA") Or txtNgaybaocao.Text.Contains("CH") Then
            '    ngaybaocao = txtNgaybaocao.EditValue.ToString().Substring(0, txtNgaybaocao.EditValue.ToString().Length - 6)
            'Else
            '    ngaybaocao = txtNgaybaocao.EditValue
            'End If
            'If txtNgaybatdau.Text.Contains("SA") Or txtNgaybatdau.Text.Contains("CH") Then
            '    ngaybatdau = txtNgaybatdau.EditValue.ToString().Substring(0, txtNgaybatdau.EditValue.ToString().Length - 6)
            'Else
            '    ngaybatdau = txtNgaybatdau.EditValue
            'End If
            'If txtNgayketthuc.Text.Contains("SA") Or txtNgayketthuc.Text.Contains("CH") Then
            '    ngayketthuc = txtNgayketthuc.EditValue.ToString().Substring(0, txtNgayketthuc.EditValue.ToString().Length - 6)
            'Else
            '    ngayketthuc = txtNgayketthuc.EditValue
            'End If

            ' Nếu form cập nhật
            Dim files = ""
            For i = 0 To gdvFileCT.DataRowCount - 1
                files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
            Next i

            ' Truyền tham số và giá trị để update dữ liệu
            AddParameter("@Ngaybatdau", txtNgaybatdau.EditValue)
            AddParameter("@Ngayketthuc", txtNgayketthuc.EditValue)

            AddParameter("@Diadiemthuchien", txtDiadiem.EditValue)
            AddParameter("@DanhsachNguoithuchien", cboDSNhanvien.EditValue)
            AddParameter("@IdLoaicongviec", cboLoaicongviec.EditValue)
            AddParameter("@Noidungcongviec", txtNoidung.Text)
            AddParameter("@Filedinhkem", files)
            AddParameter("@Ghichu", txtGhichu.Text)
            AddParameter("@IdUser", TaiKhoan)
            AddParameter("@Ngaybaocao", txtNgaybaocao.EditValue)
            AddParameterWhere("@Id", _recordId)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object

            ' Insert bản ghi vào bảng CSDL tblBaocaocongviec_IT
            _iD = doUpdate("tblBaocaocongviec_IT", "Id = @Id")

            If _iD Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()
                ' Tải lại dữ liệu của gridview sau khi thêm mới
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachBaocaoCongviec).SelectKhaosat(_idVattuPhanmem)
            End If
        End If

        Return True
    End Function

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        If _flagForm = False Then
            If SaveForm() Then
                'Close()
            End If
        Else
            SaveForm()
        End If
    End Sub

    Private Sub cboLoaicongviec_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cboLoaicongviec.ButtonClick
        If e.Button.Index = 1 Then
            Dim frm As New frmLoaicongviec
            frm.ShowDialog()
        End If
    End Sub

    Private Sub btnLuuthem_Click(sender As Object, e As EventArgs) Handles btnLuuthem.Click
        'SaveForm()

        ' Xoá trắng
        txtDiadiem.EditValue = Nothing
        txtNoidung.EditValue = Nothing
        txtGhichu.Text = ""
        cboDSNhanvien.EditValue = Nothing
        txtNgaybatdau.EditValue = GetServerTime()
        txtNgayketthuc.EditValue = GetServerTime()
        txtNgaybaocao.EditValue = GetServerTime()
        dt.Rows.Clear()
        gdvFile.DataSource = dt
        _idTempSaved = Nothing
    End Sub
End Class