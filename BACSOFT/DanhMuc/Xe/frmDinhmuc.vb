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
Imports System.Runtime.Serialization
Public Class frmDinhmuc
    Private Shared da As SqlDataAdapter
    Private Shared ds As DataSet
    Private Shared conn As SqlConnection
    Private Sub loadData()
        Dim query = "select dinhmuc.*, tenxe, bienso, tennhienvatlieu from dinhmuc inner join xe on dinhmuc.id_xe=xe.id inner join nhienvatlieu on dinhmuc.id_nhienvatlieu=nhienvatlieu.id"
        gcDinhmuc.DataSource = ExecuteSQLDataTable(query)
        query = "select id, tenxe from xe"
        'cbbXe.DataSource = ExecuteSQLDataSet(query).Tables(0)
        lueXe.DataSource = ExecuteSQLDataSet(query).Tables(0)
        barLueXe.EditValue = 1
        query = "select * from nhienvatlieu"
        'cbbNhienvatlieu.DataSource = ExecuteSQLDataSet(query).Tables(0)
        lueNhienVatLieu.DataSource = ExecuteSQLDataTable(query)
        barLueNhienVatLieu.EditValue = 1
    End Sub
    Private Sub frmDinhmuc_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub cbbXe_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs)


    End Sub

    Private Sub cbbNhienvatlieu_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim kt = "select id from dinhmuc where id_xe=@id_xe and id_nhienvatlieu=@id_nhienvatlieu"
        AddParameter("@id_xe", barLueXe.EditValue)
        AddParameter("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
        If (ExecuteSQLDataTable(kt).Rows.Count = 0) Then
            ' Dim query = "insert into dinhmuc values(@id_xe, @id_nhienvatlieu, @dinhmuc)"
            AddParameter("@id_xe", barLueXe.EditValue)
            AddParameter("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
            AddParameter("@dinhmuc", barTxtDinhMuc.EditValue)
            If doInsert("DinhMuc") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công định mức mới")
            End If

        Else
            AddParameterWhere("@id_xe", barLueXe.EditValue)
            AddParameterWhere("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
            AddParameter("@dinhmuc", barTxtDinhMuc.EditValue)
            If doUpdate("DinhMuc", "id_xe=@id_xe and id_nhienvatlieu=@id_nhienvatlieu") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                For i As Integer = 0 To gvDinhmuc.RowCount - 1
                    gvDinhmuc.PostEditor()
                    gvDinhmuc.UpdateCurrentRow()
                    AddParameter("@chisohientai", gvDinhmuc.GetRowCellValue(i, "chisohientai"))
                    AddParameterWhere("@id", gvDinhmuc.GetRowCellValue(i, "id"))
                    If doUpdate("DinhMuc", "id=@id") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                Next
                ShowAlert("Đã cập nhật thành công định mức mới ")
            End If

        End If
        loadData()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim id As Integer
        Dim query As String = ""
        id = If(gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "id").ToString()))

        Dim tennhienvatlieu As String
        Dim tenxe As String
        tennhienvatlieu = gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "tennhienvatlieu").ToString()
        tenxe = gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "tenxe").ToString()
        'query = " delete from xe where id = " + id.ToString() + ""
        Try
            If ShowCauHoi("Có muốn xóa định mức '" + tennhienvatlieu + "' của xe " + tenxe + "  không ?") Then
                AddParameterWhere("@id", id)
                If doDelete("DinhMuc", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã xóa thành công ! ")
                End If

            End If
            loadData()
            ' conn.Close()
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try
    End Sub

    Private Sub barLueXe_EditValueChanged(sender As Object, e As EventArgs) Handles barLueXe.EditValueChanged

        Dim query = "select dinhmuc from dinhmuc where id_xe=@id_xe and id_nhienvatlieu=@id_nhienvatlieu"
        If barLueXe.EditValue Is Nothing And barLueNhienVatLieu.EditValue Is Nothing Then
            Return
        End If

        AddParameter("@id_xe", barLueXe.EditValue)
            AddParameter("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
            If (ExecuteSQLDataSet(query).Tables(0).Rows.Count > 0) Then
                AddParameter("@id_xe", barLueXe.EditValue)
                AddParameter("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
                barTxtDinhMuc.EditValue = ExecuteSQLScalar(query) 'ExecuteSQLDataSet(query).Tables(0).Rows(0).Item(0)
            Else
                barTxtDinhMuc.EditValue = 0
            End If

    End Sub

    Private Sub barLueNhienVatLieu_EditValueChanged(sender As Object, e As EventArgs) Handles barLueNhienVatLieu.EditValueChanged
        Dim query = "select dinhmuc from dinhmuc where id_xe=@id_xe and id_nhienvatlieu=@id_nhienvatlieu"
        If barLueXe.EditValue Is Nothing And barLueNhienVatLieu.EditValue Is Nothing Then
            Return
        End If
        AddParameter("@id_xe", barLueXe.EditValue)
            AddParameter("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
            If (ExecuteSQLDataSet(query).Tables(0).Rows.Count > 0) Then
                AddParameter("@id_xe", barLueXe.EditValue)
                AddParameter("@id_nhienvatlieu", barLueNhienVatLieu.EditValue)
                barTxtDinhMuc.EditValue = ExecuteSQLScalar(query)
            Else
                barTxtDinhMuc.EditValue = 0
            End If

    End Sub

    Private Sub gvDinhmuc_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gvDinhmuc.RowCellStyle
        Dim view As GridView = sender
        Dim query2 As String = "select id, chisohientai, dinhmuc from dinhmuc where chisohientai>=dinhmuc"
        If ExecuteSQLDataTable(query2).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ExecuteSQLDataTable(query2).Rows.Count - 1
                Dim id As String = view.GetRowCellDisplayText(e.RowHandle, view.Columns("id"))
                If id = ExecuteSQLDataTable(query2).Rows(j).Item(0).ToString() Then
                    If e.Column.FieldName = "tenxe" Then
                        e.Appearance.BackColor = Color.Yellow
                    End If
                End If
            Next j
        End If
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        If gvDinhmuc.RowCount > 0 Then
            Dim idxe As Integer
            Dim tenxe As String = gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "tenxe").ToString()
            idxe = If(gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "id_xe").ToString() = "", 0, Convert.ToInt32(gvDinhmuc.GetRowCellValue(gvDinhmuc.FocusedRowHandle, "id_xe").ToString()))
            Dim query As String = ""
            If ShowCauHoi("Có muốn bảo dưỡng xe '" + tenxe + "' không ?") Then
                Dim frm As frmThemBaoduongxe = New frmThemBaoduongxe()
                frm.Message2 = idxe
                frm.Message = 0
                frm.ShowDialog()

            End If
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTailai.ItemClick
        loadData()
    End Sub
End Class