Imports BACSOFT.Db.SqlHelper
Public Class frmCNYeuCauTuWeb
    Public objTrangThai As New Utils.TrangThai
    Public _id As Object

    Private Sub frmCNYeuCauTuWeb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setSourceGdvFileDinhKem()
        If objTrangThai.isAddNew = True Then
            dtThoiGianNhan.EditValue = GetServerTime()
            txtSoYC.EditValue = LaySoPhieu("YeuCauTuWeb")
        Else
            Dim dt As New DataTable
            dt = ExecuteSQLDataTable("SELECT * FROM YeuCauTuWeb WHERE ID = " & _id)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    txtSoYC.EditValue = dt.Rows(0)("Sophieu")
                    txtSDT.EditValue = dt.Rows(0)("SDT")
                    txtTenKH.EditValue = dt.Rows(0)("TenKH")
                    txtNguoiGD.EditValue = dt.Rows(0)("NguoiGD")
                    txtDiaChi.EditValue = dt.Rows(0)("DiaChi")
                    txtEmail.EditValue = dt.Rows(0)("Email")
                    memoNoiDung.EditValue = dt.Rows(0)("NoiDung")
                    dtThoiGianNhan.EditValue = dt.Rows(0)("ThoiGianNhan")
                    If dt.Rows(0)("FileDinhKem").ToString <> "" Then
                        Dim str() As String = dt.Rows(0)("FileDinhKem").ToString.Split(New Char() {";c"})
                        For Each _St In str
                            gdvFileCT.AddNewRow()
                            gdvFileCT.SetFocusedRowCellValue("File", _St)
                        Next
                        gdvFileCT.CloseEditor()
                        gdvFileCT.UpdateCurrentRow()
                    End If
                    'SoYeuCau =
                End If
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Private Sub btnDong_Click(sender As Object, e As EventArgs) Handles btnDong.Click
        Close()
    End Sub

    Private Sub btnLuu_Click(sender As Object, e As EventArgs) Handles btnLuu.Click
       
        Dim tg = GetServerTime()
        If (IsDBNull(txtEmail.EditValue) Or txtEmail.EditValue Is Nothing Or txtEmail.EditValue = "") Then
            ShowCanhBao("Email không được để trống")
            Exit Sub
        End If
        If (IsDBNull(txtNguoiGD.EditValue) Or txtNguoiGD.EditValue Is Nothing Or txtNguoiGD.EditValue = "") Then
            ShowCanhBao("Người giao dịch không được để trống")
            Exit Sub
        End If
        If (IsDBNull(txtSDT.EditValue) Or txtSDT.EditValue Is Nothing Or txtSDT.EditValue = "") Then
            ShowCanhBao("SDT không được để trống")
            Exit Sub
        End If
        If dtThoiGianNhan.EditValue > tg Then
            ShowCanhBao("Thời gian nhận không đúng. Vui lòng nhập lại.")
            Exit Sub
        End If
        Dim _listFile As String = ""
        For i As Integer = 0 To gdvFileCT.RowCount - 1
            _listFile &= gdvFileCT.GetRowCellValue(i, "File")
            If i < gdvFileCT.RowCount - 1 Then
                _listFile &= ";"
            End If
        Next
        Dim path As String = ""

        AddParameter("@Sophieu", txtSoYC.EditValue)
        AddParameter("@TenKH", txtTenKH.EditValue)
        AddParameter("@NguoiGD", txtNguoiGD.EditValue)
        AddParameter("@DiaChi", txtDiaChi.EditValue)
        AddParameter("@Email", txtEmail.EditValue)
        AddParameter("@SDT", txtSDT.EditValue)
        AddParameter("@ThoiGianNhan", dtThoiGianNhan.EditValue)
        AddParameter("@NoiDung", memoNoiDung.EditValue)
        AddParameter("@NguonYC", "Webmail")

        If objTrangThai.isUpdate Then
            AddParameter("@FileDinhKem", _listFile)
        Else
            AddParameter("@FileDinhKem", "")
        End If

        If (Not _id Is Nothing And Not IsDBNull(_id)) Then
            AddParameterWhere("@ID", _id)
            If doUpdate("YeuCauTuWeb", "ID = @ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowThongBao("Update thành công")
            End If
        Else
            _id = doInsert("YeuCauTuWeb")
            If _id Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
        End If

        If objTrangThai.isAddNew Then
            _listFile = ""
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString)
            End If
            ShowWaiting("Đang chuyển file lên server ...")
            For i As Integer = 0 To gdvFileCT.RowCount - 1
                path = "YC" & txtSoYC.EditValue & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(gdvFileCT.GetRowCellValue(i, "File"))
                'ShowCanhBao(path)
                IO.File.Copy(gdvFileCT.GetRowCellValue(i, "File"), RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString & "\" & path)
                _listFile &= path & ";"
            Next
            _listFile = _listFile.TrimEnd(New Char() {";"c})
            Impersonator.EndImpersonation()
            CloseWaiting()
            AddParameter("@FileDinhKem", _listFile)
            AddParameterWhere("@ID", _id)
            If doUpdate("YEUCAUTUWEB", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
        objTrangThai.isAddNew = False
        objTrangThai.isUpdate = True
        ShowThongBao("Lưu dữ liệu thành công")

        'Dim dt As New DataTable
        'dt = ExecuteSQLDataTable("select ID from NHANSU WHERE Noictac <> 74 and REPLACE(lower(Email), ' ', '') = '" & txtEmail.EditValue & "'")
        'If dt Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        '    Exit Sub
        'End If
        'If dt.Rows.Count > 0 Then
        '    AddParameter("@NguoiGD", dt.Rows(0)(0))
        'Else
        '    AddParameter("@NguoiGD", Nothing)

        'End If
        'AddParameterWhere("@ID", _id)
        'If doUpdate("YeuCauTuWeb", "ID = @ID") Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        '    Exit Sub
        'End If

    End Sub

    Private Sub btnThemMoi_Click(sender As Object, e As EventArgs) Handles btnThemMoi.Click
        objTrangThai.isAddNew = True
        objTrangThai.isUpdate = False
        txtEmail.EditValue = ""
        txtSDT.EditValue = ""
        txtDiaChi.EditValue = Nothing
        txtTenKH.EditValue = ""
        memoNoiDung.EditValue = ""
        dtThoiGianNhan.EditValue = GetServerTime()
        txtSoYC.EditValue = LaySoPhieu("YeuCauTuWeb")
    End Sub
    Public Sub setSourceGdvFileDinhKem()
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvFile.DataSource = tb
    End Sub
    Private Sub btThemFile_Click(sender As Object, e As EventArgs) Handles btThemFile.Click
        Dim path As String = ""

        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString)
            End If
            For Each file In OpenFile.FileNames
                If objTrangThai.isUpdate Then
                    ShowWaiting("Đang chuyển file lên server ...")
                    path = "YC" & txtSoYC.EditValue & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(file)
                    Try
                        IO.File.Copy(file, RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString & "\" & path)
                        gdvFileCT.AddNewRow()
                        gdvFileCT.SetFocusedRowCellValue("File", path)
                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try
                    CloseWaiting()
                ElseIf objTrangThai.isAddNew Then
                    gdvFileCT.AddNewRow()
                    gdvFileCT.SetFocusedRowCellValue("File", file)
                End If
            Next
            Impersonator.EndImpersonation()
        End If
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
    End Sub

    Private Sub btXoaFile_Click(sender As Object, e As EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        Try
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            IO.File.Delete(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString & "\" & gdvFileCT.GetFocusedRowCellValue("File"))
            Impersonator.EndImpersonation()
            gdvFileCT.DeleteSelectedRows()
        Catch ex As Exception
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not IO.File.Exists(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(dtThoiGianNhan.EditValue).Year.ToString & "\" & gdvFileCT.GetFocusedRowCellValue("File")) Then
                gdvFileCT.DeleteSelectedRows()
            End If
            Impersonator.EndImpersonation()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class