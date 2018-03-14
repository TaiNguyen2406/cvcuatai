Imports BACSOFT.Db.SqlHelper

Public Class frmThemPhanhe

    ' _flagForm = false thì form làm nhiệm vụ thêm mới. _flagForm = true thì form  làm nhiệm vụ cập nhật
    Public _flagForm As Boolean = False

    ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable

    ' Các thuộc tính truy cập bên ngoài form.
    Public _idVattuPhanmem As String
    Public _recordId As String
    Public _Ngaytao As String
    Public _Nguoitao As String
    Public _Tenphanhe As String
    Public _Motaphanhe As String
    Public _Files As String
    Public _Ghichu As String
    Public _isBaotri As Boolean
    Public _isThemoiphanhe As Boolean
    Public _isSuaphanhe As Boolean
    Dim _idTempSaved As Object

    Public Sub SetThongtinYeucaubaotri(idYeucaubaotri As String, noidungYeucau As String)
        txtYeucaubaotri.Text = idYeucaubaotri
        txtMota.Text = txtMota.Text & "; " & noidungYeucau
    End Sub

    ' Khởi tạo dữ liệu cho form khi form làm nhiệm vụ update
    Sub InitUpdateData()
        If _Ngaytao IsNot Nothing Then
            txtNgaytao.EditValue = DateTime.Parse(_Ngaytao)
        Else
            txtNgaytao.EditValue = GetServerTime()
        End If

        txtNguoitao.Text = _Nguoitao
        txtTenphanhe.Text = _Tenphanhe
        txtMota.Text = _Motaphanhe
        txtGhichu.Text = _Ghichu
        chkLaBaotri.Checked = _isBaotri
        chkThemphanhemoi.Checked = _isThemoiphanhe
        chkSuaphanhedaco.Checked = _isSuaphanhe

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

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Function ValidateForm() As Boolean
        If txtNgaytao.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày tạo.")
            Return False
        End If

        If txtTenphanhe.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập tên phân hệ.")
            Return False
        End If

        Return True
    End Function

    Function SaveForm() As Boolean
        If ValidateForm() = False Then
            Return False
        End If

        If _flagForm = False Then
            Dim files = ""
            For i = 0 To gdvFileCT.DataRowCount - 1
                files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
            Next i

            ' Truyền tham số và giá trị để insert/update dữ liệu
            AddParameter("@IdVatTu", _idVattuPhanmem)
            AddParameter("@Ngaylap", txtNgaytao.EditValue)
            AddParameter("@Nguoilap", TaiKhoan)
            AddParameter("@Tenphanhe", txtTenphanhe.Text)
            AddParameter("@Filedinhkem", files)
            AddParameter("@_Nangcapbaotri", chkLaBaotri.Checked)

            If chkLaBaotri.Checked Then
                AddParameter("@_Themphanhe", chkThemphanhemoi.Checked)
                AddParameter("@_Suaphanhe", chkSuaphanhedaco.Checked)
                AddParameter("@IdYeucaubaotri", txtYeucaubaotri.Text)
            End If

            AddParameter("@Motaphanhe", txtMota.Text)
            AddParameter("@Ghichu", txtGhichu.Text)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object

            If _idTempSaved Is Nothing Then
                ' Insert bản ghi vào bảng CSDL tblPhanhe_IT
                _iD = doInsert("tblPhanhe_IT")
            Else
                ' Update
                AddParameterWhere("@Id", _idTempSaved)
                _iD = doUpdate("tblPhanhe_IT", "Id = @Id")
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
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmPhantichChucnang).SelectDanhmucphanhe(_idVattuPhanmem)
            End If
        Else
            'Dim ngaytao As String
            'If _txtNgaytao.Text.Contains("SA") Or _txtNgaytao.Text.Contains("CH") Then
            '    ngaytao = _txtNgaytao.EditValue.ToString().Substring(0, _txtNgaytao.EditValue.ToString().Length - 6)
            'Else
            '    ngaytao = _txtNgaytao.EditValue
            'End If
            Dim files = ""
            For i = 0 To gdvFileCT.DataRowCount - 1
                files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
            Next i

            ' Truyền tham số và giá trị để update dữ liệu
            AddParameter("@Ngaylap", txtNgaytao.EditValue)
            AddParameter("@Tenphanhe", txtTenphanhe.Text)
            AddParameter("@Filedinhkem", files)
            AddParameter("@_Nangcapbaotri", chkLaBaotri.Checked)

            If chkLaBaotri.Checked Then
                AddParameter("@_Themphanhe", chkThemphanhemoi.Checked)
                AddParameter("@_Suaphanhe", chkSuaphanhedaco.Checked)
            End If

            AddParameter("@Motaphanhe", txtMota.Text)
            AddParameter("@Ghichu", txtGhichu.Text)

            AddParameterWhere("@Id", _recordId)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object

            ' Insert bản ghi vào bảng CSDL tblPhanhe_IT
            _iD = doUpdate("tblPhanhe_IT", "Id = @Id")

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
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmPhantichChucnang).SelectDanhmucphanhe(_idVattuPhanmem)
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

    Private Sub frmThemPhanhe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm cột vào table chứa file tạm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        txtNgaytao.EditValue = GetServerTime()
        txtNguoitao.Text = NguoiDung

        ' Focus con trỏ vào control txtTenphanhe
        txtTenphanhe.Select()

        If _flagForm = True Then
            InitUpdateData()
            btLuuLai.Text = "Lưu lại"
            btnLuuthem.Visible = False
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

    Private Sub chkLaBaotri_CheckedChanged(sender As Object, e As EventArgs) Handles chkLaBaotri.CheckedChanged
        If chkLaBaotri.Checked Then
            chkSuaphanhedaco.Enabled = True
            chkThemphanhemoi.Enabled = True
            btnChon.Enabled = True
        Else
            chkSuaphanhedaco.Enabled = False
            chkThemphanhemoi.Enabled = False
            btnChon.Enabled = False
        End If
    End Sub

    Private Sub btnChon_Click(sender As Object, e As EventArgs) Handles btnChon.Click
        Dim formChonyeucaubaotri As frmChonyeucaubaotri
        formChonyeucaubaotri = New frmChonyeucaubaotri
        formChonyeucaubaotri._idVattuPhanmem = _idVattuPhanmem
        formChonyeucaubaotri._flagPhanhe = True
        formChonyeucaubaotri.ShowDialog(Me)
    End Sub

    Private Sub btnLuuthem_Click(sender As Object, e As EventArgs) Handles btnLuuthem.Click
        'SaveForm()

        ' Xoá trắng
        txtNgaytao.EditValue = GetServerTime()
        txtTenphanhe.EditValue = Nothing
        txtGhichu.Text = ""
        txtMota.Text = Nothing
        chkLaBaotri.Checked = False
        txtYeucaubaotri.EditValue = Nothing
        _idTempSaved = Nothing
        dt.Rows.Clear()
        gdvFile.DataSource = dt
    End Sub
End Class