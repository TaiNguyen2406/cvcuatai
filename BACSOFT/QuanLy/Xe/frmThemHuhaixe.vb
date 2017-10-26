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
Public Class frmThemHuhaixe
    Protected Shared _message As Integer

    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property
    Public id_xe As Integer = 0
    Private Sub loadData()
        'Dim conn As SqlConnection = New SqlConnection(Str)
        'conn.Open()
        Dim query = "select * from huhaixe where id_sudungxe=@id_sudungxe"
        AddParameter("@id_sudungxe", _message)
        Dim dt As DataTable = ExecuteSQLDataSet(query).Tables(0)
        If dt.Rows.Count > 0 Then
            txtVitri.Text = dt.Rows(0).Item(1).ToString()
            txtThaythe.Text = dt.Rows(0).Item(2).ToString()
            nudChiphi.Text = dt.Rows(0).Item(3).ToString()
            If dt.Rows(0).Item(6).ToString <> "" Then
                dtpNgaySuaChua.EditValue = dt.Rows(0).Item(6)
            End If

            If dt.Rows(0).Item(4).ToString() = 1 Then
                'rdbSuachua.Checked = True
                cbxSuachua.Checked = True
            Else
                'rdbSuachua.Checked = False
                cbxSuachua.Checked = False
            End If
            query = "select  tenxe, id_xe from sudungxe inner join xe on xe.id=sudungxe.id_xe where sudungxe.id=@id_sudungxe"
            AddParameter("@id_sudungxe", _message)
            dt = ExecuteSQLDataSet(query).Tables(0)
            id_xe = dt.Rows(0).Item(1)
        End If
        
    End Sub
    Private Sub frmHuhaixe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick

    End Sub

    Private Sub cbxSuachua_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxSuachua.CheckedChanged
        If cbxSuachua.Checked = True Then
            dtpNgaySuaChua.Enabled = True
            txtThaythe.Enabled = True
            nudChiphi.Enabled = True
        Else
            dtpNgaySuaChua.Enabled = False
            txtThaythe.Enabled = False
            nudChiphi.Enabled = False
        End If
    End Sub

    Private Sub btnThem_Click(sender As System.Object, e As System.EventArgs) Handles btnThem.Click
        Dim _date As System.Data.SqlTypes.SqlDateTime = System.Data.SqlTypes.SqlDateTime.Null
        Dim query = "select * from huhaixe where id_sudungxe=@id_sudungxe"
        AddParameter("@id_sudungxe", _message)
        Dim sodong As Integer = ExecuteSQLDataSet(query).Tables(0).Rows.Count
        If sodong = 0 Then
            query = "insert into huhaixe(vitrihuhai, thaythe, chiphi, trangthaihuhai, id_sudungxe, ngaysua) values(@vitrihuhai, @thaythe, @chiphi, @trangthaihuhai, @id_sudungxe, @ngaysua)"
        Else
            query = "update huhaixe set vitrihuhai=@vitrihuhai, thaythe=@thaythe, chiphi=@chiphi, trangthaihuhai=@trangthaihuhai, ngaysua=@ngaysua where id_sudungxe=@id_sudungxe"
        End If

        If cbxSuachua.Checked = True Then
            Dim suachua As String = "update xe set id_tinhtrang=2 where id_xe=@id_xe"
            AddParameterWhere("@id_xe", id_xe)
            ExecuteSQLNonQuery(suachua)
            AddParameter("@trangthaihuhai", 1)
            AddParameter("@ngaysua", Convert.ToDateTime(dtpNgaySuaChua.EditValue).ToString("yyyy/MM/dd HH:mm"))
        Else
            AddParameter("@trangthaihuhai", 0)
            AddParameter("@ngaysua", _date)
        End If
        AddParameter("@vitrihuhai", txtVitri.Text)
        AddParameter("@thaythe", txtThaythe.Text)
        AddParameter("@chiphi", nudChiphi.Value)
        AddParameter("@id_sudungxe", _message)
        Try
            ExecuteSQLNonQuery(query)

            ShowAlert("Đã sửa chữa")
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try
    End Sub

 
    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Dim query = "delete from huhaixe where id_sudungxe=@id_sudungxe"
        AddParameter("@id_sudungxe", _message)
        ExecuteSQLNonQuery(query)
        Me.Close()
    End Sub


End Class