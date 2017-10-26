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
Public Class frmNhanvaTraxe

    Private Sub loadData()
        riLueTrangThai.DataSource = tableTrangthai()
        riLueTinhTrang.DataSource = tableTinhTrang()
        '  riLueTinhTrang.PopulateColumns()
        ' riLueTinhTrang.Columns(riLueTinhTrang.ValueMember).Visible = False
        Dim query As String = "select xe.* from xe "
        gcXeroi.DataSource = ExecuteSQLDataTable(query)
        query = "select  xe.id, tenxe, bienso , hanhtrinh, ten, sudungxe.id as id_sudungxe, ngaydidukien, ngaydi, id_trangthaixe from sudungxe inner join nhansu on id_nguoisudung=nhansu.id inner join xe on id_xe=xe.id where ngayve is null and id_trangthaixe<3 order by ngaydidukien desc"

        gcXeban.DataSource = ExecuteSQLDataTable(query)
    End Sub
    Private Shared Sub dongform(sender As System.Object, e As FormClosedEventArgs)
        'loadData()
        frmNhanvaTraxe.Show()
    End Sub
    Private Sub frmNhanvaTraxe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()

    End Sub

    Private Sub btnNhanxe_Click(sender As System.Object, e As System.EventArgs)
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        frm.Message = 0
        frm.Muontra = 0
        'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
        frm.ShowDialog()
        loadData()

    End Sub


    Private Sub btnTraxe_Click(sender As System.Object, e As System.EventArgs)
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        If gvXeban.RowCount > 0 Then
            Dim id As Integer = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString() = "", 0, Convert.ToInt32(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString()))
            frm.Message = id
            frm.Muontra = 1
            frm.ShowDialog()
            loadData()
        Else
            ShowCanhBao("không có xe nào đang hoạt động")
        End If


    End Sub

    Private Sub btnQuanly_Click(sender As System.Object, e As System.EventArgs)
        Dim frm As frmLichSuMuonXe = New frmLichSuMuonXe()
        Dim id As Integer = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString() = "", 0, Convert.ToInt32(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString()))
        ' frm.Message = id
        ' frm.Muontra = 1
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub NhậnXeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NhậnXeToolStripMenuItem.Click
        If gvXeban.RowCount > 0 Then
            Dim id As Integer = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString() = "", 0, Convert.ToInt32(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString()))
            Dim ngaydidukien As String = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "ngaydidukien").ToString() = "", 0, (gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "ngaydidukien").ToString()))
            Dim idxe As String = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id").ToString() = "", 0, (gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id").ToString()))
            AddParameter("@id_trangthaixe", 2)
            AddParameter("@ngaydi", DateTime.Now)
            AddParameter("@ngayvedukien", DateTime.Now.AddHours(1))
            AddParameterWhere("@id", id)

            If doUpdate("sudungxe", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã nhận xe")
            End If
            loadData()
        End If
      
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        frm.Message = 0
        frm.Muontra = 0
        'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        If gvXeban.RowCount > 0 Then
            Dim id As Integer = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString() = "", 0, Convert.ToInt32(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString()))
            frm.Message = id
            frm.Muontra = 1
            frm.ShowDialog()
            loadData()
        Else
            ShowCanhBao("không có xe nào đang hoạt động")
        End If


    End Sub

    Private Sub cmsNhanxe_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles cmsNhanxe.Opening
        If gvXeban.RowCount > 0 Then
            Dim query As String = ""
            Dim idxe As String = 0
            Dim ngaydidukien As String = ""
            Dim sodong As Integer = 0
            query = "select id from sudungxe where id_xe=@id_xe and id_trangthaixe=2"
            If gvXeban.RowCount > 0 Then
                idxe = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id").ToString() = "", 0, (gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id").ToString()))
                'ngaydidukien = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "ngaydidukien").ToString() = "", 0, (gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "ngaydidukien").ToString()))
                '  AddParameter("@ngaydidukien", Convert.ToDateTime(ngaydidukien))
                AddParameter("@id_xe", idxe)
                sodong = ExecuteSQLDataTable(query).Rows.Count
                If sodong > 0 Then
                    NhậnXeToolStripMenuItem.Enabled = False

                    HủyToolStripMenuItem.Enabled = True
                    If gvXeban.GetFocusedRowCellValue("id_trangthaixe") = 2 Then
                        TrảXeToolStripMenuItem.Enabled = True
                    Else
                        TrảXeToolStripMenuItem.Enabled = False
                    End If
                Else
                    NhậnXeToolStripMenuItem.Enabled = True
                    HủyToolStripMenuItem.Enabled = True
                    TrảXeToolStripMenuItem.Enabled = False
                    'End If
                End If
            Else
                NhậnXeToolStripMenuItem.Enabled = False
                TrảXeToolStripMenuItem.Enabled = False
                HủyToolStripMenuItem.Enabled = False
            End If
        End If
    End Sub

    Private Sub TrảXeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TrảXeToolStripMenuItem.Click
        If gvXeban.RowCount > 0 Then
            Dim frm As frmThemsudungxe = New frmThemsudungxe()
            If gvXeban.RowCount > 0 Then
                Dim id As Integer = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString() = "", 0, Convert.ToInt32(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString()))
                frm.Message = id
                frm.Muontra = 1
                frm.ShowDialog()
                loadData()
            Else
                ShowCanhBao("không có xe nào đang hoạt động")
            End If
        End If
      
    End Sub

    Private Sub HủyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HủyToolStripMenuItem.Click
        If gvXeban.RowCount > 0 Then
            Dim query As String = ""
            Dim id As Integer = 0
            Dim ngaydidukien As String = ""
            id = If(gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString() = "", 0, (gvXeban.GetRowCellValue(gvXeban.FocusedRowHandle, "id_sudungxe").ToString()))
            AddParameter("@id_trangthaixe", 4)
            AddParameterWhere("@id", id)
            If doUpdate("sudungxe", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã hủy thành công")
            End If
            loadData()
        End If
       
    End Sub

    Private Sub ĐăngKýMượnToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ĐăngKýMượnToolStripMenuItem.Click
        If gvXeroi.RowCount > 0 Then
            Dim id_xe As Integer = If(gvXeroi.GetRowCellValue(gvXeroi.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXeroi.GetRowCellValue(gvXeroi.FocusedRowHandle, "id").ToString()))
            Dim frm As frmThemsudungxe = New frmThemsudungxe()
            frm.Message = 0
            frm.Muontra = 0
            frm.MaXe = id_xe
            'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
            frm.ShowDialog()
            loadData()
        End If
      
    End Sub

    Private Sub gvXeroi_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvXeroi.DoubleClick
        Dim id_xe As Integer = If(gvXeroi.GetRowCellValue(gvXeroi.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXeroi.GetRowCellValue(gvXeroi.FocusedRowHandle, "id").ToString()))

        'Dim frm As frmThemsudungxe = New frmThemsudungxe()
        Dim frm As New frmThemsudungxe
        frm.Message = 0
        frm.Muontra = 0
        frm.MaXe = id_xe
        'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub gvXeroi_Click(sender As System.Object, e As System.EventArgs) Handles gvXeroi.Click
        'MsgBox(gvXeroi.FocusedColumn.Caption)
    End Sub
End Class