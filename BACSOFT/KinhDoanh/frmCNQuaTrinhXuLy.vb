Imports BACSOFT.Db.SqlHelper

Public Class frmCNQuaTrinhXuLy
    Public _MaKH As String = ""
    Public SoYeuCau As Object
    Public ThoiGian As DateTime

    Private Sub frmCNQuaTrinhXuLy_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        'gdvListFileCT.
        If TrangThai.isAddNew Then
            Me.Text = "Thêm quá trình xử lý yêu cầu "
        Else
            Me.Text = "Cập nhật quá trình xử lý yêu cầu"
            AddParameterWhere("@IDXuLy", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM tblXuLyYeuCau WHERE ID=@IDXuLy")
            If Not dt Is Nothing Then
                tbNoiDung.EditValue = dt.Rows(0)("NoiDungXuLy")
                Dim listUrl() As String = dt.Rows(0)("FileDinhKem").ToString.Split(New Char() {";"c})
                For Each _url In listUrl
                    If Not _url = "" Then
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", _url)
                    End If

                Next
            End If

        End If
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên server...")
            If Not System.IO.Directory.Exists(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKinhDoanh & _MaKH & "\") Then
                System.IO.Directory.CreateDirectory(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKinhDoanh & _MaKH)
            End If
            For Each file In openFile.FileNames
                path = "YC" & SoYeuCau & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(file)
                Try
                    IO.File.Copy(file, RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKinhDoanh & _MaKH & "\" & path)
                    gdvListFileCT.AddNewRow()
                    gdvListFileCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception

                    ShowBaoLoi(ex.Message)
                End Try
            Next
            CloseWaiting()
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá file được chọn ?") Then
            Try
                IO.File.Delete(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKinhDoanh & _MaKH & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                gdvListFileCT.DeleteSelectedRows()
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub gdvListFileCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvListFileCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            btXoaFile.PerformClick()
        End If

    End Sub

    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
        If tbNoiDung.EditValue Is Nothing And gdvListFileCT.RowCount = 0 Then
            ShowCanhBao("Chưa có thông tin cần lưu")
            Exit Sub
        End If

        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        If TrangThai.isAddNew Then
            Dim sql As String = " INSERT INTO tblXuLyYeuCau (Ngay,IDYeuCau,IDNgXuLy,NoiDungXuLy,FileDinhKem)"

            sql &= " VALUES (getdate()," & objID & "," & TaiKhoan & ",N'" & tbNoiDung.EditValue & "'" & ",N'" & _File & "'); SELECT SCOPE_IDENTITY();"

            If ExecuteSQLScalar(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
        Else
            AddParameter("@NoiDungXuLy", tbNoiDung.EditValue)
            AddParameter("@FileDinhKem", _File)
            AddParameterWhere("@ID", MaTuDien)
            If doUpdate("tblXuLyYeuCau", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

        End If
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDen).LoadDSYeuCau()
        ShowAlert(Me.Text & " thành công!")
        Me.Close()
    End Sub


End Class