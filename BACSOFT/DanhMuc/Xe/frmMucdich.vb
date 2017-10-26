Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class frmMucdich
    Private Sub loadData()
        Dim query = "select *, id as id2 from mucdichsudung"
        gcMucdich.DataSource = ExecuteSQLDataTable(query)
    End Sub
    Private Sub frmMucdich_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub


    Private Sub gdvChiTiet_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvMucdich.RowUpdated
        AddParameter("@id", gvMucdich.GetFocusedRowCellValue("id"))
        AddParameter("@tenmucdich", gvMucdich.GetFocusedRowCellValue("tenmucdich"))
        If Not IsDBNull(gvMucdich.GetFocusedRowCellValue("id2")) Then
            AddParameterWhere("@id2", gvMucdich.GetFocusedRowCellValue("id2"))
            If doUpdate("mucdichsudung", "id=@id2") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadData()
            Else
                gvMucdich.SetFocusedRowCellValue("id2", gvMucdich.GetFocusedRowCellValue("id"))
            End If
        Else
            If doInsert("mucdichsudung") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gvMucdich.DeleteSelectedRows()
                loadData()
            Else
                ShowAlert("Đã thêm mục đích sử dụng !")
                gvMucdich.SetFocusedRowCellValue("id2", gvMucdich.GetFocusedRowCellValue("id"))
            End If
        End If
    End Sub

    Private Sub gvMucdich_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvMucdich.DoubleClick
        gvMucdich.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvMucdich_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvMucdich.HiddenEditor
        'gvMucdich.OptionsBehavior.Editable = False
    End Sub

    Private Sub gvMucdich_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvMucdich.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gvMucdich.RowCount > 1 Then
            If ShowCauHoi("Xóa mục đích " & gvMucdich.GetFocusedRowCellValue("tenmucdich") & " ?") Then
                AddParameterWhere("@id", gvMucdich.GetFocusedRowCellValue("id2"))
                If doDelete("mucdichsudung", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gvMucdich.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
       
    End Sub
End Class