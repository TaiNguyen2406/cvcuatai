Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class frmXe

    Private Sub loadData()
        Dim query = "select id, tenxe, bienso, id_tinhtrang, ghichu, chiso, id as id2 from xe order by id"
        riLueTinhTrang.DataSource = tableTinhTrang()
        barRdgTinhTrangXe.EditValue = 1
        gcXe.DataSource = ExecuteSQLDataTable(query)
        query = "select xe.id, chiso, dinhmuc from xe inner join dinhmuc on xe.id=dinhmuc.id_xe where chiso>=dinhmuc"
        'deskTop = New fMain
        If ExecuteSQLDataTable(query).Rows.Count > 0 Then
            deskTop.bsiTinhTrangXe.Enabled = True
            deskTop.bsiTinhTrangXe.Caption = "có xe cần bảo dưỡng"
        Else
            deskTop.bsiTinhTrangXe.Enabled = False
            deskTop.bsiTinhTrangXe.Caption = "."
        End If
    End Sub
    Private Sub frmXe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub gvXe_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvXe.RowUpdated
        AddParameter("@id", gvXe.GetFocusedRowCellValue("id"))
        AddParameter("@tenxe", gvXe.GetFocusedRowCellValue("tenxe"))
        AddParameter("@bienso", gvXe.GetFocusedRowCellValue("bienso"))
        AddParameter("@id_tinhtrang", 1)
        AddParameter("@chiso", gvXe.GetFocusedRowCellValue("chiso"))
        AddParameter("@ghichu", gvXe.GetFocusedRowCellValue("ghichu"))
        If Not IsDBNull(gvXe.GetFocusedRowCellValue("id2")) Then
            AddParameterWhere("@id2", gvXe.GetFocusedRowCellValue("id2"))
            If doUpdate("xe", "id=@id2") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)

            Else
                ShowAlert("Đã cập nhật !")
                gvXe.SetFocusedRowCellValue("id2", gvXe.GetFocusedRowCellValue("id"))
            End If
        Else
            If doInsert("xe") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gvXe.DeleteSelectedRows()

            Else
                ShowAlert("Đã thêm Xe !")
                gvXe.SetFocusedRowCellValue("id2", gvXe.GetFocusedRowCellValue("id"))
            End If
        End If
        loadData()
        Dim query = "select xe.id, chisohientai, dinhmuc from xe inner join dinhmuc on xe.id=dinhmuc.id_xe where chisohientai>=dinhmuc"
        If ExecuteSQLDataTable(query).Rows.Count > 0 Then
            deskTop.bsiTinhTrangXe.Enabled = True
            deskTop.bsiTinhTrangXe.Caption = "có xe cần bảo dưỡng"
        Else
            deskTop.bsiTinhTrangXe.Enabled = False
            deskTop.bsiTinhTrangXe.Caption = "."
        End If
    End Sub

    Private Sub gvXe_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvXe.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
        
    End Sub

    Private Sub gvXe_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvXe.DoubleClick
        gvXe.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvXe_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvXe.HiddenEditor
        ' gvXe.OptionsBehavior.Editable = False
    End Sub

    Private Sub XóaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles btnXoa.Click
        If gvXe.RowCount > 1 Then
            If ShowCauHoi("Xóa xe " & gvXe.GetFocusedRowCellValue("tenxe") & " ?") Then
                AddParameterWhere("@id", gvXe.GetFocusedRowCellValue("id2"))
                If doDelete("xe", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gvXe.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
       
    End Sub


    Private Sub BảoDưỡngToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BảoDưỡngToolStripMenuItem.Click
        If gvXe.RowCount > 1 Then
            Dim id As Integer
            Dim tenxe As String = gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "tenxe").ToString()

            id = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
            Dim query As String = ""
            Dim id_tinhtrang As Integer
            id_tinhtrang = gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id_tinhtrang").ToString()
            If id_tinhtrang = 1 Then
                Try

                    If ShowCauHoi("Có muốn bảo dưỡng xe '" + tenxe + "' không ?") Then
                        Dim frm As frmThemBaoduongxe = New frmThemBaoduongxe()
                        frm.Message2 = id
                        frm.ShowDialog()
                        loadData()
                    End If
                Catch ex As Exception
                    ShowBaoLoi("Lỗi" + ex.Message)
                End Try
            Else
                If id_tinhtrang = 3 Or id_tinhtrang = 2 Then
                    query = " update xe set  id_tinhtrang=1 where id=" + id.ToString()
                    If ShowCauHoi("Có muốn đưa xe '" + tenxe + "' vào sử dụng không ?") = False Then
                        Return
                    Else
                        ExecuteSQLNonQuery(query)
                        ShowAlert("Thành công")
                        loadData()
                    End If

                End If
            End If
        End If
      

    End Sub

    Private Sub cmsMenu_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles cmsMenu.Opening
        Dim cms As ContextMenuStrip = sender
        Dim id As Integer
        Dim query As String = ""
        If gvXe.FocusedRowHandle >= 0 And gvXe.FocusedRowHandle < gvXe.RowCount - 1 Then
            id = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
            Dim kiemtra As String
            kiemtra = "select * from sudungxe where id_xe = " + id.ToString() + ""
            Dim id_tinhtrang As Integer
            id_tinhtrang = gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id_tinhtrang").ToString()
            If id_tinhtrang = 2 Or id_tinhtrang = 3 Then
                BảoDưỡngToolStripMenuItem.Text = "Đưa vào sử dụng"
            Else
                If (id_tinhtrang = 1) Then
                    BảoDưỡngToolStripMenuItem.Text = "Bảo dưỡng"
                End If
            End If
        Else
            e.Cancel = True


        End If

    End Sub


    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        If ShowCauHoi("Xóa xe " & gvXe.GetFocusedRowCellValue("tenxe") & " ?") Then
            AddParameterWhere("@id", gvXe.GetFocusedRowCellValue("id"))
            If doDelete("xe", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gvXe.DeleteSelectedRows()
                ShowAlert("Đã xoá!")
            End If
        End If
    End Sub


    Private Sub rdgTinhTrangxe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles rdgTinhTrangxe.EditValueChanged
        Bar5.Manager.ActiveEditItemLink.PostEditor()
        Dim query As String = ""
        query = "select id, tenxe, bienso, chiso, id_tinhtrang, ghichu, id as id2 from xe"
        Select Case barRdgTinhTrangXe.EditValue
            Case 1
                query &= " where id_tinhtrang=1"
            Case 2
                query &= " where id_tinhtrang=2"
            Case 3
                query &= " where id_tinhtrang=3"
        End Select

        gcXe.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub gvXe_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gvXe.RowCellStyle
        Dim view As GridView = sender
        Dim query2 As String = "select xe.id from xe  inner join dinhmuc on xe.id=dinhmuc.id_xe where chisohientai>=dinhmuc"
        Dim kt = ExecuteSQLDataTable(query2)
        If kt.Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To kt.Rows.Count - 1
                Dim id As String = view.GetRowCellDisplayText(e.RowHandle, view.Columns("id"))
                If id = kt.Rows(j).Item(0).ToString() Then
                    If e.Column.FieldName = "tenxe" Then
                        e.Appearance.BackColor = Color.Yellow
                    End If
                End If
            Next j
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        loadData()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Dim frm = New frmGiayToXe()
        frm.ShowDialog()
    End Sub
End Class