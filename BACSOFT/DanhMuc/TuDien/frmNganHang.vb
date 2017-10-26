Imports BACSOFT.Db.SqlHelper

Public Class frmNganHang

    Private Sub frmNganHang_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT *,Ma AS ID FROM tblNganHang")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("SuDung", True)
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xóa ngân hàng được chọn ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("tblNganHang", "Ma=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        ElseIf e.KeyCode = Keys.F5 Then
            LoadDS()
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        Try
            AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
            AddParameter("@Ten", gdvCT.GetFocusedRowCellValue("Ten"))
            AddParameter("@SuDung", gdvCT.GetFocusedRowCellValue("SuDung"))

            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then
                If doInsert("tblNganHang") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                gdvCT.SetFocusedRowCellValue("ID", gdvCT.GetFocusedRowCellValue("Ma"))
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("tblNganHang", "Ma=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvCT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvCT.DoubleClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        End If

        gdvCT.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gdvCT_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gdvCT.HiddenEditor
        gdvCT.OptionsBehavior.Editable = False
    End Sub
End Class
