Imports BACSOFT.Db.SqlHelper

Public Class frmCNBoPhan
    Dim _exit As Boolean = False
    Private Sub frmCNBoPhan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        rcbNhom.DataSource = DataTableNhomBoPhan()
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT *,Ma as ID FROM NhanSu_BoPhan ORDER BY STT")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown

        'gdvCT.CloseEditor()
        'gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng được chọn ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("NhanSu_BoPhan", "Ma=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If _exit Then Exit Sub
        Try
            AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
            AddParameter("@MaBP", gdvCT.GetFocusedRowCellValue("MaBP"))
            AddParameter("@Ten", gdvCT.GetFocusedRowCellValue("Ten"))
            AddParameter("@STT", gdvCT.GetFocusedRowCellValue("STT"))
            AddParameter("@IDNhom", gdvCT.GetFocusedRowCellValue("IDNhom"))
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then

                If doInsert("NhanSu_BoPhan") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                ' Dim objID As Object =
                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", gdvCT.GetFocusedRowCellValue("Ma"))
                gdvCT.CloseEditor()
                gdvCT.UpdateCurrentRow()
                _exit = False
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("NhanSu_BoPhan", "Ma=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class