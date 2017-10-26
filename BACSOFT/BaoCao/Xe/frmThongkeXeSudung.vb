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
Imports System.Globalization
Imports System.Threading

Public Class frmThongkeXeSudung
    Private Sub loadData()
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
        Dim query As String = "select"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            barDeTuNgay.Enabled = True
            barDeDenNgay.Enabled = True
        End If
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " xe.id, tenxe, bienso, count(id_xe) as solan, sum(sokmdachay) as tongso from xe inner join sudungxe on xe.id=sudungxe.id_xe where 1=1"
        If barLueXe.EditValue IsNot Nothing Then
            query &= " and xe.id=@xe_id"
            AddParameter("@xe_id", barLueXe.EditValue)
        End If

        If barDeTuNgay.Enabled = True Then
            query = query + " and ngaydi >= @detungay "
            AddParameterWhere("@detungay", barDeTuNgay.EditValue)
        End If
        If barDeDenNgay.Enabled = True Then
            query = query + " and ngayve <= @dedenngay "
            AddParameterWhere("@dedenngay", barDeDenNgay.EditValue)
        End If
        query = query + " group by xe.id, tenxe, bienso "
        ' MsgBox(query)
        gcDanhsach.DataSource = ExecuteSQLDataTable(query)

        ' barCbxXe.Checked = True
    End Sub


    Private Sub frmDanhsach_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        barCbbXem.EditValue = "Top 500"
        loadData()
        Dim query = "select xe.id, tenxe, bienso, xe.ghichu  from xe"
        lueXe.DataSource = ExecuteSQLDataSet(query).Tables(0)
        barDeTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        barDeDenNgay.EditValue = DateTime.Today
    End Sub

    Private Sub barLueXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueXe.EditValueChanged
        loadData()
    End Sub


    Private Sub lueXe_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueXe.ButtonClick
        If e.Button.Index = 1 Then
            barLueXe.EditValue = Nothing
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        loadData()
    End Sub

    Private Sub barDeTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeTuNgay.EditValueChanged
        loadData()
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeDenNgay.EditValueChanged
        loadData()
    End Sub

    Private Sub barCbbXem_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbXem.EditValueChanged
        loadData()
    End Sub
End Class