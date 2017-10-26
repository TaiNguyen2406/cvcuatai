Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo

Public Class frmNhienvatlieu
    Private Sub loadData()
        Dim query = "select *, id as id2 from nhienvatlieu"

        gcNhienvatlieu.DataSource = ExecuteSQLDataTable(query)
    End Sub
    Private Sub frmNhienvatlieu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()

    End Sub

    Private Sub gvNhienvatlieu_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvNhienvatlieu.DoubleClick
        gvNhienvatlieu.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvNhienvatlieu_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvNhienvatlieu.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub

    Private Sub gvNhienvatlieu_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvNhienvatlieu.HiddenEditor
        ' gvNhienvatlieu.OptionsBehavior.Editable = False
    End Sub

    Private Sub gvNhienvatlieu_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvNhienvatlieu.RowUpdated
        AddParameter("@id", gvNhienvatlieu.GetFocusedRowCellValue("id"))
        AddParameter("@tennhienvatlieu", gvNhienvatlieu.GetFocusedRowCellValue("tennhienvatlieu"))
        AddParameter("@ghichu_nhienvatlieu", gvNhienvatlieu.GetFocusedRowCellValue("ghichu_nhienvatlieu"))
        If Not IsDBNull(gvNhienvatlieu.GetFocusedRowCellValue("id2")) Then
            AddParameterWhere("@id2", gvNhienvatlieu.GetFocusedRowCellValue("id2"))
            If doUpdate("nhienvatlieu", "id=@id2") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadData()
            Else
                gvNhienvatlieu.SetFocusedRowCellValue("id2", gvNhienvatlieu.GetFocusedRowCellValue("id"))
            End If
        Else
            If doInsert("nhienvatlieu") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gvNhienvatlieu.DeleteSelectedRows()
                loadData()
            Else
                ShowAlert("Đã thêm nhiên vật liệu !")
                gvNhienvatlieu.SetFocusedRowCellValue("id2", gvNhienvatlieu.GetFocusedRowCellValue("id"))
            End If
        End If
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gvNhienvatlieu.RowCount > 1 Then
            If ShowCauHoi("Xóa nhiên vật liệu " & gvNhienvatlieu.GetFocusedRowCellValue("tennhienvatlieu") & " ?") Then
                AddParameterWhere("@id", gvNhienvatlieu.GetFocusedRowCellValue("id2"))
                If doDelete("nhienvatlieu", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gvNhienvatlieu.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
       
    End Sub

End Class