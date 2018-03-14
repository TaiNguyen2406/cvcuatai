Imports BACSOFT.Db.SqlHelper

Public Class frmThemChucnang

    ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable
    ' Biến này lưu ID của phân hệ đã chọn
    Public _idPhanhe As String
    ' Biến này lưu ID của vật tư phần mềm đã chọn
    Public _idVattuPhanmem As String

    ' _flagForm = false thì form làm nhiệm vụ thêm mới. _flagForm = true thì form  làm nhiệm vụ cập nhật
    Public _flagForm As Boolean = False

    Public _recordId As String
    Public _Ngaytao As String
    Public _Nguoitao As String
    Public _Trangthai As String
    Public _Motachucnang As String
    Public _Tenchucnang As String
    Public _Files As String
    Public _Ghichu As String
    Public _isBaotri As Boolean
    Public _isThemoichucnang As Boolean
    Public _isSuachucnang As Boolean
    Dim _idTempSaved As Object

    Public Sub SetChucnangcu(listChucnang As String, idYeucaubaotri As String)
        txtChucnangcu.Text = listChucnang
        'txtYeucaubaotri.Text = idYeucaubaotri
    End Sub
    Public Sub SetThongtinYeucaubaotri(idYeucaubaotri As String, noidungYeucaubaotri As String)
        txtYeucaubaotri.Text = idYeucaubaotri
        txtGhichu.Text = txtGhichu.Text & "; " & noidungYeucaubaotri
    End Sub

    ' Method này Khởi tạo dữ liệu cho form khi form làm nhiệm vụ Update
    Sub InitUpdateData()
        If _Ngaytao IsNot Nothing Then
            txtNgaytao.EditValue = DateTime.Parse(_Ngaytao)
        Else
            txtNgaytao.EditValue = GetServerTime()
        End If
        txtNguoitao.Text = _Nguoitao
        txtMota.Text = _Motachucnang
        txtTenchucnang.Text = _Tenchucnang 
        txtGhichu.Text = _Ghichu
        chkLaBaotri.Checked = _isBaotri
        chkChucnangmoi.Checked = _isThemoichucnang
        chkSuachucnangdaco.Checked = _isSuachucnang

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

    ' Select bảng CSDL tblTrangthaichucnang_IT vào combobox cboTrangthaichucnang
    Sub SelectTrangthaichucnang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM tblTrangthaichucnang_IT")
        If Not tb Is Nothing Then
            cboTrangthaichucnang.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub frmThemChucnang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm cột vào table chứa file tạm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        SelectTrangthaichucnang()

        cboTrangthaichucnang.ItemIndex = 0

        txtNgaytao.EditValue = GetServerTime()
        txtNguoitao.Text = NguoiDung

        ' Focus con trỏ vào control txtMota
        txtTenchucnang.Select()

        If _flagForm = True Then
            InitUpdateData()
            btLuuLai.Text = "Lưu lại"
            btnLuuthem.Visible = False
        End If
    End Sub

    Private Sub chkLaBaotri_CheckedChanged(sender As Object, e As EventArgs) Handles chkLaBaotri.CheckedChanged
        If chkLaBaotri.Checked Then
            chkChucnangmoi.Enabled = True
            chkSuachucnangdaco.Enabled = True
            btnChon.Enabled = True
        Else
            chkChucnangmoi.Enabled = False
            chkSuachucnangdaco.Enabled = False
            btnChon.Enabled = False
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

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Function ValidateForm() As Boolean
        If txtNgaytao.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập ngày tạo.")
            Return False
        End If

         If txtTenchucnang.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập tên chức năng.")
            Return False
        End If

        If txtMota.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa nhập mô tả chức năng.")
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
            AddParameter("@IdPhanhe", _idPhanhe)
            AddParameter("@Tenchucnang", txtTenchucnang.Text)
            AddParameter("@Motachucnang", txtMota.Text)
            AddParameter("@Filedinhkem", files)
            AddParameter("@Ngaylap", txtNgaytao.EditValue)
            AddParameter("@Nguoilap", TaiKhoan)

            If chkSuachucnangdaco.Checked Then
                AddParameter("@IdChucnangcu", txtChucnangcu.Text)
                AddParameter("@IdYeucaubaotri", txtYeucaubaotri.Text)
            End If

            AddParameter("@Ghichu", txtGhichu.Text)
            AddParameter("@TrangthaiChucnang", cboTrangthaichucnang.EditValue)
            AddParameter("@_Nangcapbaotri", chkLaBaotri.Checked)

            If chkLaBaotri.Checked Then
                AddParameter("@_Themchucnang", chkChucnangmoi.Checked)
                AddParameter("@_Suachucnag", chkSuachucnangdaco.Checked)
            End If

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD, _iDYC As Object

            If _idTempSaved Is Nothing Then
                ' Insert bản ghi vào bảng CSDL tblDSChucnang_IT
                _iD = doInsert("tblDSChucnang_IT")
            Else
                ' Update
                AddParameterWhere("@Id", _idTempSaved)
                _iD = doUpdate("tblDSChucnang_IT", "Id = @Id")
            End If

            If _iD Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else

                If txtYeucaubaotri.Text.Length > 0 Then
                    _iDYC = ExecuteSQLNonQuery("Update tblDulieubaotri_IT set _Dabaotri = 1 " & " where Id = " & txtYeucaubaotri.Text)
                End If

                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()
                If _idTempSaved Is Nothing Then
                    _idTempSaved = _iD
                End If

                ' Tải lại dữ liệu của gridview sau khi thêm mới
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmPhantichChucnang).SelectDanhmucchucnang(_idPhanhe)
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

            ' Truyền tham số và giá trị để insert/update dữ liệu
            AddParameter("@Motachucnang", txtMota.Text)
            AddParameter("@Tenchucnang", txtTenchucnang.Text)
            AddParameter("@Filedinhkem", files)
            AddParameter("@Ngaylap", txtNgaytao.EditValue)
            AddParameter("@Nguoilap", TaiKhoan)

            AddParameter("@Ghichu", txtGhichu.Text)
            AddParameter("@TrangthaiChucnang", cboTrangthaichucnang.EditValue)
            AddParameter("@_Nangcapbaotri", chkLaBaotri.Checked)

            If chkSuachucnangdaco.Checked Then
                AddParameter("@IdChucnangcu", txtChucnangcu.Text)
            End If

            If chkLaBaotri.Checked Then
                AddParameter("@_Themchucnang", chkChucnangmoi.Checked)
                AddParameter("@_Suachucnag", chkSuachucnangdaco.Checked)
            End If

            AddParameterWhere("@Id", _recordId)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object

            ' Insert bản ghi vào bảng CSDL tblDSChucnang_IT
            _iD = doUpdate("tblDSChucnang_IT", "Id = @Id")

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
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmPhantichChucnang).SelectDanhmucchucnang(_idPhanhe)
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

    Private Sub chkSuachucnangdaco_CheckedChanged(sender As Object, e As EventArgs) Handles chkSuachucnangdaco.CheckedChanged
        If chkSuachucnangdaco.Checked Then
            btnChon2.Enabled = True
        Else
            btnChon2.Enabled = False
        End If
    End Sub

    Private Sub btnChon_Click(sender As Object, e As EventArgs) Handles btnChon.Click
        Dim formChonyeucaubaotri As frmChonyeucaubaotri
        formChonyeucaubaotri = New frmChonyeucaubaotri
        formChonyeucaubaotri._maPhanhe = _idPhanhe
        formChonyeucaubaotri._idVattuPhanmem = _idVattuPhanmem
        formChonyeucaubaotri.ShowDialog(Me)
    End Sub

    Private Sub btnChon2_Click(sender As Object, e As EventArgs) Handles btnChon2.Click
        Dim formSuachucnangdaco As frmSuachucnangdaco
        formSuachucnangdaco = New frmSuachucnangdaco
        formSuachucnangdaco._maPhanhe = _idPhanhe
        formSuachucnangdaco._idVattuPhanmem = _idVattuPhanmem
        formSuachucnangdaco.ShowDialog(Me)
    End Sub

    Private Sub btnLuuthem_Click(sender As Object, e As EventArgs) Handles btnLuuthem.Click
        'SaveForm()

        ' Xoá trắng
        txtTenchucnang.EditValue = nothing
        txtNgaytao.EditValue = GetServerTime()
        txtMota.EditValue = Nothing
        txtGhichu.Text = ""
        chkLaBaotri.Checked = False
        txtYeucaubaotri.EditValue = Nothing
        txtChucnangcu.EditValue = Nothing
        _idTempSaved = Nothing
        dt.Rows.Clear()
        gdvFile.DataSource = dt
    End Sub
End Class