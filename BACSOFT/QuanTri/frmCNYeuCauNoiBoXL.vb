Imports DevExpress.XtraTreeList.Nodes
Imports BACSOFT.Db.SqlHelper

Public Class frmCNYeuCauNoiBoXL
    Public _SP As Object
    Private _stateChange As Boolean = False
    Public _IDNgYC As Object

    Private Sub frmCNYeuCauNoiBoXL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If TrangThai.isAddNew Then
            Me.Text = "Thêm xử lý yêu cầu " & _SP
        Else
            Me.Text = "Cập nhật nội dung xử lý yêu cầu " & _SP
            Dim sql As String = ""
            AddParameterWhere("@ID", objID)

            sql = "SELECT * FROM tblYeuCauNoiBoXL WHERE ID=@ID"

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbThoiGianLap.EditValue = tb.Rows(0)("ThoiGian")
                tbNoiDung.EditValue = tb.Rows(0)("NoiDung")
                gdvFile.DataSource = DataSourceDSFile(tb.Rows(0)("FileDinhKem").ToString)
            End If
        End If

    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click

        Dim tg As DateTime = GetServerTime()
        AddParameter("@ThoiGian", tg)
        AddParameter("@NoiDung", tbNoiDung.EditValue)
        AddParameter("@IDThucHien", TaiKhoan)
        AddParameter("@SoPhieu", _SP)
        AddParameter("@FileDinhKem", StrDSFile(gdvFileCT, "File"))
        If TrangThai.isAddNew Then
            objID = doInsert("tblYeuCauNoiBoXL")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)

            Else
                TrangThai.isUpdate = True
                Me.Text = "Cập nhật nội dung xử lý yêu cầu " & _SP
                tbThoiGianLap.EditValue = tg
                ShowAlert("Đã thêm xử lý !")
                If ExecuteSQLNonQuery("UPDATE tblYeuCauNoiBo SET TrangThai=1 WHERE SoPhieu=N'" & _SP & "'") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauNoiBo).LoadDS()
                _stateChange = True
            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("tblYeuCauNoiBoXL", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật xử lý yêu cầu !")
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauNoiBo).LoadDS()
                _stateChange = True
            End If
        End If
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        tbThoiGianLap.EditValue = Nothing
        tbNoiDung.EditValue = ""
        Me.Text = "Thêm xử lý yêu cầu " & _SP
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub frmCNYeuCauNoiBoXL_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _stateChange Then
            Dim tg As DateTime = GetServerTime()
            Dim str As String = NguoiDung & " đã thêm quá trình xử lý YC nội bộ SP: " & _SP & vbCrLf & " - nội dung: " & vbCrLf & tbNoiDung.EditValue
            If chkHoanThanh.Checked Then
                str &= vbCrLf & " - Yêu cầu đã xử lý xong."
            End If
            AddParameter("@NoiDung", str)
            AddParameter("@ThoiGian", tg)
            AddParameter("@IDNhanVien", _IDNgYC)
            If doInsert("tblThongBao") Is Nothing Then
                ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
            End If
            If chkHoanThanh.Checked Then
                str = "UPDATE tblYeuCauNoiBo SET TrangThai=2 WHERE SoPhieu=N'" & _SP & "'"
            Else
                str = "UPDATE tblYeuCauNoiBo SET TrangThai=1 WHERE SoPhieu=N'" & _SP & "'"
            End If
            If ExecuteSQLNonQuery(str) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        Dim _NamThang As String = GetServerTime.ToString("yyyyMM") & "\"
        If TrangThai.isUpdate Then
            _NamThang = Convert.ToDateTime(tbThoiGianLap.EditValue).ToString("yyyyMM") & "\"
        End If


        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(RootUrl & UrlYeuCauNoiBo & _NamThang) Then
                System.IO.Directory.CreateDirectory(RootUrl & UrlYeuCauNoiBo & _NamThang)
            End If
            For Each file In OpenFile.FileNames
                ShowWaiting("Đang chuyển file lên server ...")
                path = IO.Path.GetFileNameWithoutExtension(file) & " " & _SP & " " & TaiKhoan.ToString & IO.Path.GetExtension(file)
                Try
                    IO.File.Copy(file, RootUrl & UrlYeuCauNoiBo & _NamThang & path)
                    gdvFileCT.AddNewRow()
                    gdvFileCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
                CloseWaiting()
            Next
        End If
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xóa file được chọn ?") Then
            Try
                IO.File.Delete(RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(tbThoiGianLap.EditValue).Year.ToString("yyyyMM") & "\" & gdvFileCT.GetFocusedRowCellValue("File"))
                gdvFileCT.DeleteSelectedRows()
            Catch ex As Exception
                If Not IO.File.Exists(RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(tbThoiGianLap.EditValue).Year.ToString("yyyyMM") & "\" & gdvFileCT.GetFocusedRowCellValue("File")) Then
                    gdvFileCT.DeleteSelectedRows()
                End If
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub gdvFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvFileCT.RowCellClick
        If e.Column.Name = "colFile" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            Dim psi As New ProcessStartInfo()
            With psi
                .FileName = RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(tbThoiGianLap.EditValue).Year.ToString("yyyyMM") & "\" & e.CellValue
                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub
End Class