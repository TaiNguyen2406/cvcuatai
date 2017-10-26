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
Public Class frmLichSuMuonXe
    Private Sub loadData()
        riLueTrangThai.DataSource = tableTrangthai()
        riLueMucDo.DataSource = tableMucDo()
        Dim query As String ' = "select sudungxe.*, ten, tenxe+'('+bienso+')' as tenxe  from ((sudungxe inner join nhansu on id_nguoisudung=nhansu.id)  ) inner join xe on xe.id=sudungxe.id_xe order by case when ngaydi IS NULL then ngaydidukien else ngaydi end desc"
        ' gcXE.DataSource = ExecuteSQLDataSet(query).Tables(0)
        query = "select * from xe"

        lueXe.DataSource = ExecuteSQLDataSet(query).Tables(0)
        query = "select ID, Ten from nhansu where trangthai=1 and noictac=74"
        riGlueNSD.DataSource = ExecuteSQLDataTable(query)
        riGlueNSD.View.PopulateColumns(riGlueNSD.DataSource)
        riGlueNSD.View.Columns(riGlueNSD.ValueMember).Visible = False

        gvXe.HorzScrollVisibility = ScrollVisibility.Auto
        'gvXe.BestFitColumns()
        gcolNgaydi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        gcolNgaydi.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        gcolNgayve.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        gcolNgayve.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        gcolNgaydidukien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        gcolNgaydidukien.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        gcolNgayvedukien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        gcolNgayvedukien.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        timkiem()
    End Sub
    Private Sub timkiem()
        Dim query As String = " SELECT "
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " sudungxe.*, ten, tenxe+'('+bienso+')' as tenxe, tenmucdich  from ((sudungxe inner join nhansu on id_nguoisudung=nhansu.id)  ) inner join xe on xe.id=sudungxe.id_xe inner join MucDichSuDung on MucDichSuDung.id=id_mucdich where 1=1 "
        'AddParameter("@id_xe", barLueXe.EditValue)
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            barDeTuNgay.Enabled = True
            barDeDenNgay.Enabled = True
            AddParameterWhere("@TuNgay", barDeTuNgay.EditValue)
            AddParameterWhere("@DenNgay", barDeDenNgay.EditValue)
            query &= " AND Convert(datetime,CONVERT(nvarchar,ngaydi,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If barGlueNSD.EditValue IsNot Nothing Then
            query = query + " and sudungxe.id_nguoisudung=@id_nguoisudung"
            AddParameter("@id_nguoisudung", barGlueNSD.EditValue)
        End If
        If barLueXe.EditValue IsNot Nothing Then
            query = query + " and sudungxe.id_xe=@id_xe"
            AddParameter("@id_xe", barLueXe.EditValue)
        End If
        query &= " order by case when ngaydi IS NULL then ngaydidukien else ngaydi end desc"
        Dim row = gvXe.FocusedRowHandle
        gcXE.DataSource = ExecuteSQLDataTable(query)
        gvXe.FocusedRowHandle = row
    End Sub
    Private Sub frmQuanlychomuonxe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
        loadData()
    End Sub

    Private Sub xeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnThem_Click(sender As System.Object, e As System.EventArgs)
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        frm.Message = 0
        frm.Muontra = 2
        frm.ShowDialog()
        loadData()

    End Sub

    Private Sub btnSua_Click(sender As System.Object, e As System.EventArgs)
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
        frm.Message = id
        frm.Muontra = 2
        'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
        frm.ShowDialog()
        loadData()

    End Sub

    Private Sub btnXoa_Click(sender As System.Object, e As System.EventArgs)
        Dim query As String = "delete from sudungxe where id=@id"
        Dim xoacon As String = "delete from huhaixe where id_sudungxe=@id"
        Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
        AddParameter("@id", id)
        Try
            If ShowCauHoi("Có muốn xóa không ?") = DialogResult.No Then
                Return
            Else
                ExecuteSQLNonQuery(xoacon)
                AddParameter("@id", id)
                ExecuteSQLNonQuery(query)
                ShowAlert("Đã xóa !")
            End If
        Catch ex As Exception
            ShowCanhBao("Không xóa được !" + ex.Message)
        End Try
        loadData()

    End Sub


    Private Sub gcXE_Click(sender As System.Object, e As System.EventArgs) Handles gcXE.Click

    End Sub

    Private Sub ThôngTinHưHạiXeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ThôngTinHưHạiXeToolStripMenuItem.Click
        Dim frm As frmThemHuhaixe = New frmThemHuhaixe()
        Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
        frm.Message = id
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub cmsHuhaixe_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles cmsHuhaixe.Opening
        'Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
        'Dim query As String = "select id from huhaixe where id_sudungxe=@id"
        'AddParameter("@id", id)
        'Dim sodong As Integer = ExecuteSQLDataSet(query).Tables(0).Rows.Count
        'If sodong = 0 Then
        '    ThôngTinHưHạiXeToolStripMenuItem.Enabled = False
        'Else
        '    ThôngTinHưHạiXeToolStripMenuItem.Enabled = True
        'End If
    End Sub

    Private Sub ThêmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ThêmToolStripMenuItem.Click
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        frm.Message = 0
        frm.Muontra = 2
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub SửaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SửaToolStripMenuItem.Click
        If gvXe.RowCount > 0 Then
            Dim frm As frmThemsudungxe = New frmThemsudungxe()
            Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
            frm.Message = id
            frm.Muontra = 2
            'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
            frm.ShowDialog()
            loadData()
        End If
       
    End Sub

    Private Sub XóaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles XóaToolStripMenuItem.Click
        btnXoa.PerformClick()
    End Sub
    Private Sub btnXe_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim frm As frmXe = New frmXe()
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub btnNguoisudung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        'Dim frm As frmThemnguoidung = New frmThemnguoidung()
        'frm.ShowDialog()
        'loadData()
    End Sub


    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        frm.Message = 0
        frm.Muontra = 2
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
        frm.Message = id
        frm.Muontra = 2
        'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gvXe.RowCount > 0 Then
            Dim query As String = "delete from sudungxe where id=@id"
            Dim xoacon As String = "delete from huhaixe where id_sudungxe=@id_sudungxe"
            Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))

            'MsgBox(id)
            Try
                If ShowCauHoi("Có muốn xóa không ?") = DialogResult.No Then
                    Return
                Else
                    Dim check As String = "select id from huhaixe where id_sudungxe=@id_sudungxe"
                    AddParameter("@id_sudungxe", id)
                    If ExecuteSQLDataTable(check).Rows.Count > 0 Then
                        If ShowCauHoi("Lần sử dụng xe này có sửa chữa, có muốn xóa tiếp không?") = DialogResult.No Then
                            Return
                        Else
                            AddParameter("@id_sudungxe", id)
                            ExecuteSQLNonQuery(xoacon)
                        End If
                    End If
                    AddParameter("@id", id)
                    ExecuteSQLNonQuery(query)
                    ShowAlert("Đã xóa !")
                End If
            Catch ex As Exception
                ShowCanhBao("Không xóa được !" + ex.Message)
            End Try
            loadData()
        End If
       
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Dim query As String = "delete from sudungxe where id_trangthaixe=4"
        Try
            If ShowCauHoi("Có muốn xóa không ?") = DialogResult.No Then
                Return
            Else
                ExecuteSQLNonQuery(query)
                ShowAlert("Đã xóa !")
            End If
        Catch ex As Exception
            ShowCanhBao("Không xóa được !" + ex.Message)
        End Try
        loadData()
    End Sub


    Private Sub barLueXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueXe.EditValueChanged
        timkiem()

    End Sub

    Private Sub barLueNsd_EditValueChanged(sender As System.Object, e As System.EventArgs)
        timkiem()

    End Sub

    Private Sub gvXe_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gvXe.RowStyle
        Dim view As GridView = sender
        Dim query As String = "select id_sudungxe from huhaixe"
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If dt.Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To dt.Rows.Count - 1
                Dim id_text As String = view.GetRowCellDisplayText(e.RowHandle, view.Columns("id"))
                If id_text = dt.Rows(j).Item(0).ToString() Then
                    e.Appearance.BackColor = Color.Orange
                End If
            Next j


        End If
    End Sub

    Private Sub lueXe_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueXe.ButtonClick
        If e.Button.Index = 1 Then
            barLueXe.EditValue = Nothing
        End If
    End Sub

    Private Sub riGlueNSD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riGlueNSD.ButtonClick
        If e.Button.Index = 1 Then
            barGlueNSD.EditValue = Nothing
            riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        End If
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        loadData()
    End Sub

    Private Sub gvXe_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvXe.DoubleClick
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        Dim id As Integer = If(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvXe.GetRowCellValue(gvXe.FocusedRowHandle, "id").ToString()))
        frm.Message = id
        frm.Muontra = 2
        'AddHandler frm.FormClosed, AddressOf FormClosedEventHandler
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub gvXe_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvXe.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        frm.Message = 0
        frm.Muontra = 3
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub barGlueNSD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barGlueNSD.EditValueChanged
        timkiem()
    End Sub

    Private Sub riGlueNSD_Popup(sender As System.Object, e As System.EventArgs) Handles riGlueNSD.Popup
        riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Private Sub BarCheckItem1_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gvXe.OptionsView.ShowAutoFilterRow = True
        Else
            gvXe.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    Private Sub barCbbXem_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbXem.EditValueChanged
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            barDeTuNgay.Enabled = True
            barDeDenNgay.Enabled = True
        End If
    End Sub
End Class