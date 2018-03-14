Imports BACSOFT.Db.SqlHelper
Imports DevExpress.CodeParser

Public Class frmThemBaotri

    ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable

    ' _flagForm = false thì form làm nhiệm vụ thêm mới. _flagForm = true thì form  làm nhiệm vụ cập nhật
    Public _flagForm As Boolean = False

    ' Những biến này lưu giá trị nhận về từ gridview Dữ liệu bảo trì
    Public _ngaythongbao As String
    Public _nguoithongbao As String
    Public _trangthaixuly As String
    Public _noidungthongbao As String
    Public _idVattuPhanmem As String
    Public _recordId As String
    Public _Files As String
    Dim _idTempSaved As Object

    ' Hàm này khởi tạo dữ liệu khi cập nhật bản ghi
    Public Sub InitUpdateData()
        ' Nếu form làm nhiệm vụ cập nhật thì điền dữ liệu vào Form.
        If _flagForm Then

            If _ngaythongbao IsNot Nothing Then
                txtNgaythongbao.EditValue = DateTime.Parse(_ngaythongbao)
            Else
                txtNgaythongbao.EditValue = GetServerTime()
            End If

            txtNguoithongbao.Text = _nguoithongbao

                If _trangthaixuly.Contains("Đã nhận xử lý") Then
                    cboTrangthaixuly.EditValue = "Đã nhận xử lý"
                Else
                    cboTrangthaixuly.EditValue = "Chờ nhận xử lý"
                End If

                Text = "Sửa yêu cầu bảo trì, nâng cấp"
                txtNoidungbaotri.Text = _noidungthongbao

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
            Else
                Text = "Thêm yêu cầu bảo trì, nâng cấp"
        End If
    End Sub

    ' Bấm nút đóng form
    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    ' Form load
    Private Sub frmThemBaotri_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm cột vào table chứa file tạm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        ' Hiển thị mặc định ngày hôm nay.
        txtNgaythongbao.EditValue = GetServerTime()
        txtNguoithongbao.Text = NguoiDung
        If _flagForm = False Then
            cboTrangthaixuly.EditValue = "Chờ nhận xử lý"
        Else
            btLuuLai.Text = " Lưu lại"
            btnLuuthem.Visible = False
        End If
        ' Focus con trỏ vào control Nội dung bảo trì
        txtNoidungbaotri.Select()

        ' Gọi hàm Init để điền dữ liệu vào Form khi form làm nhiệm vụ cập nhật
        InitUpdateData()
    End Sub

    Function ValidateForm() As Boolean
        If txtNgaythongbao.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày tạo.")
            Return False
        End If

        If txtNoidungbaotri.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập nội dung bảo trì.")
            Return False
        End If
        Return True
    End Function

    Function SaveForm() As Boolean
        If ValidateForm() = False Then
            Return False
        End If
        Dim files = ""
        For i = 0 To gdvFileCT.DataRowCount - 1
            files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
        Next i

        ' Truyền tham số và giá trị để insert/update dữ liệu
        AddParameter("@IdVatTu", _idVattuPhanmem)
        AddParameter("@Nguoithongbao", TaiKhoan)
        AddParameter("@Noidungthongbao", txtNoidungbaotri.Text)
        AddParameter("@Filedinhkem", files)

        If cboTrangthaixuly.SelectedText = "Chờ nhận xử lý" Then
            AddParameter("@Trangthaixuly", 0)
        End If

        AddParameter("@Ghichu", "")

        ' Bắt đầu phiên
        BeginTransaction()
        Dim _iD As Object

        If _flagForm Then
            ' Update bảng CSDL tblDulieubaotri_IT
            'Dim ngaytao As String
            'If txtNgaythongbao.Text.Contains("SA") Or txtNgaythongbao.Text.Contains("CH") Then
            '    ngaytao = txtNgaythongbao.EditValue.ToString().Substring(0, txtNgaythongbao.EditValue.ToString().Length - 6)
            'Else
            '    ngaytao = txtNgaythongbao.EditValue
            'End If

            AddParameter("@Ngaythongbao", txtNgaythongbao.EditValue)
            AddParameterWhere("@Id", _recordId)
            _iD = doUpdate("tblDulieubaotri_IT", "Id = @Id")
        Else

            If _idTempSaved Is Nothing Then
                ' Insert bản ghi vào bảng CSDL tblDulieubaotri_IT
                AddParameter("@Ngaythongbao", txtNgaythongbao.EditValue)
                _iD = doInsert("tblDulieubaotri_IT")
            Else
                ' Update
                AddParameterWhere("@Id", _idTempSaved)
                _iD = doUpdate("tblDulieubaotri_IT", "Id = @Id")
            End If

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
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachBaotri).SelectDulieubaotri(_idVattuPhanmem)
        End If
        Return True
    End Function

    ' Bấm nút lưu lại
    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        If _flagForm = False Then
            If SaveForm() Then
                'Close()
            End If
        Else
            SaveForm()
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

    Private Sub btnLuuthem_Click(sender As Object, e As EventArgs) Handles btnLuuthem.Click
        'SaveForm()

        ' Xoá trắng
        txtNgaythongbao.EditValue = GetServerTime()
        txtNoidungbaotri.EditValue = Nothing
        dt.Rows.Clear()
        gdvFile.DataSource = dt
        _idTempSaved = Nothing
    End Sub
End Class