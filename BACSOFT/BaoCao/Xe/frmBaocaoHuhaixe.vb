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
Imports System.Globalization
Imports System.Threading
Imports System.IO
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports DevExpress.XtraGrid

Public Class frmBaocaoHuhaixe
    Private Shared query As String
    Private Sub loadData()

        query = "select xe.id, tenxe, bienso, count(huhaixe.id) as solan, sum(chiphi) as tongchiphi from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe inner join xe on xe.id=id_xe group by xe.id, tenxe, bienso  order by xe.id"
        gcDanhsachXe.DataSource = ExecuteSQLDataTable(query)
        query = "select nhansu.id, ten, solanmuon, count(huhaixe.id) as solanhong, sum(chiphi) as tongchiphi"
        query &= " from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe"
        query &= " inner join xe on xe.id=id_xe inner join nhansu on nhansu.id=id_nguoisudung"
        query &= " left join (select nhansu.id, count(id_nguoisudung) as solanmuon from nhansu inner join sudungxe"
        query &= " on id_nguoisudung = nhansu.id group by nhansu.id) as slm on slm.id=nhansu.id"
        query &= " group by  nhansu.id,ten,solanmuon  order by nhansu.id"
        gcNguoisSuDung.DataSource = ExecuteSQLDataTable(query)
        query = "select nhansu.id,ten, xe.id, tenxe, vitrihuhai, chiphi,thaythe, ngaysua, huhaixe.id as id_huhaixe from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe inner join xe on xe.id=id_xe inner join nhansu on nhansu.id=id_nguoisudung order by ngaysua"
        gcBaocaoHuhaixe.DataSource = ExecuteSQLDataTable(query)
        query = "select id, tenxe from xe"
        riLueXe.DataSource = ExecuteSQLDataTable(query)
        query = "select ID, Ten from nhansu where trangthai=1 and noictac=74"
        riGlueNSD.DataSource = ExecuteSQLDataTable(query)
        riGlueNSD.View.PopulateColumns(riGlueNSD.DataSource)
        riGlueNSD.View.Columns(riGlueNSD.ValueMember).Visible = False
        ' barDeNgaySua.EditValue = Today
    End Sub
    Private Sub frmBaocaoHuhaixe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub gvDanhsachXe_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvDanhsachXe.FocusedRowChanged
        Dim id As Integer = If(gvDanhsachXe.GetRowCellValue(gvDanhsachXe.FocusedRowHandle, "id") Is Nothing, 0, Convert.ToInt32(gvDanhsachXe.GetRowCellValue(gvDanhsachXe.FocusedRowHandle, "id").ToString()))
        query = "select nhansu.id,ten, xe.id, tenxe, vitrihuhai, chiphi,thaythe, ngaysua, huhaixe.id as id_huhaixe from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe inner join xe on xe.id=id_xe inner join nhansu on nhansu.id=id_nguoisudung where id_xe=@id_xe"
        AddParameterWhere("@id_xe", id)
        gcBaocaoHuhaixe.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub gvBaocaoHuhaixe_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        Dim view As GridView = sender
        Dim query2 As String = "select id from huhaixe where trangthaihuhai=0 "
        If ExecuteSQLDataTable(query2).Rows.Count > 0 Then
            Dim j As Integer
            For j = 0 To ExecuteSQLDataSet(query2).Tables(0).Rows.Count - 1
                Dim id As String = view.GetRowCellDisplayText(e.RowHandle, view.Columns("id_huhaixe"))
                If id = ExecuteSQLDataSet(query2).Tables(0).Rows(j).Item(0).ToString() Then
                    e.Appearance.Options.UseForeColor = True
                    e.Appearance.BackColor = Color.Red
                    e.Appearance.ForeColor = Color.White

                End If
            Next j


        End If
    End Sub

    Private Sub gvNguoiSuDung_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvNguoiSuDung.FocusedRowChanged
        Dim id As Integer = If(gvNguoiSuDung.GetRowCellValue(gvNguoiSuDung.FocusedRowHandle, "id") Is Nothing, 0, Convert.ToInt32(gvNguoiSuDung.GetRowCellValue(gvNguoiSuDung.FocusedRowHandle, "id").ToString()))
        query = "select nhansu.id, ten, xe.id, tenxe, vitrihuhai, chiphi,thaythe, ngaysua, huhaixe.id as id_huhaixe from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe inner join xe on xe.id=id_xe inner join nhansu on nhansu.id=id_nguoisudung where id_nguoisudung=@id_nguoisudung"
        AddParameterWhere("@id_nguoisudung", id)
        gcBaocaoHuhaixe.DataSource = ExecuteSQLDataTable(query)

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        loadData()
    End Sub

    Private Sub gvDanhsachXe_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim saveDialog As SaveFileDialog = New SaveFileDialog()
        'Dim str As String
        saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html"
        If saveDialog.ShowDialog() = DialogResult.OK Then
            Dim exportFilePath As String = saveDialog.FileName
            Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
            Dim printingSystem = New PrintingSystemBase()
            Dim compositeLink = New CompositeLink()
            compositeLink.PrintingSystemBase = printingSystem
            Dim link1 As PrintableComponentLinkBase = New PrintableComponentLinkBase()
            link1.Component = gcDanhsachXe
            Dim link2 As PrintableComponentLinkBase = New PrintableComponentLinkBase()
            link2.Component = gcNguoisSuDung
            Dim link3 As PrintableComponentLinkBase = New PrintableComponentLinkBase()
            link3.Component = gcBaocaoHuhaixe
            compositeLink.Links.Add(link1)
            compositeLink.Links.Add(link2)
            compositeLink.Links.Add(link3)
            Dim options As XlsxExportOptions = New XlsxExportOptions()
            options.ExportMode = XlsxExportMode.SingleFilePageByPage
            compositeLink.CreateDocument()
            compositeLink.ShowPreview()
        End If
    End Sub

    Private Sub barLueXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueXe.EditValueChanged
        query = "select xe.id, tenxe, bienso, count(huhaixe.id) as solan, sum(chiphi) as tongchiphi from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe inner join xe on xe.id=id_xe"
        If barLueXe.EditValue IsNot Nothing Then
            query &= " where xe.id=@idxe"
            AddParameterWhere("@idxe", barLueXe.EditValue)
        End If
        query &= " group by xe.id, tenxe, bienso order by xe.id"
        gcDanhsachXe.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub barGlueNSD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barGlueNSD.EditValueChanged
        query = "select nhansu.id, ten, solanmuon, count(huhaixe.id) as solanhong, sum(chiphi) as tongchiphi"
        query &= " from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe"
        query &= " inner join xe on xe.id=id_xe inner join nhansu on nhansu.id=id_nguoisudung"
        query &= " left join (select nhansu.id, count(id_nguoisudung) as solanmuon from nhansu inner join sudungxe"
        query &= " on id_nguoisudung = nhansu.id group by nhansu.id) as slm on slm.id=nhansu.id"

        If barGlueNSD.EditValue IsNot Nothing Then
            query &= " where nhansu.id=@idnhansu"
            AddParameterWhere("@idnhansu", barGlueNSD.EditValue)
        End If
        query &= " group by nhansu.id,ten,solanmuon order by nhansu.id desc"
        gcNguoisSuDung.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub BarEditItem2_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeNgaySua.EditValueChanged
        query = "select nhansu.id,ten, xe.id, tenxe, vitrihuhai, chiphi,thaythe, ngaysua, huhaixe.id as id_huhaixe from sudungxe inner join huhaixe on sudungxe.id=id_sudungxe inner join xe on xe.id=id_xe inner join nhansu on nhansu.id=id_nguoisudung where ngaysua=@ngaysua order by ngaysua"
        AddParameterWhere("@ngaysua", barDeNgaySua.EditValue)
        gcBaocaoHuhaixe.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub riLueXe_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueXe.ButtonClick
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

    Private Sub riDeNgaySua_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riDeNgaySua.ButtonClick
        If e.Button.Index = 1 Then
            barDeNgaySua.EditValue = Today
        End If
    End Sub

    Private Sub riGlueNSD_Popup(sender As System.Object, e As System.EventArgs) Handles riGlueNSD.Popup
        riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub
End Class