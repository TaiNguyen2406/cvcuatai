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

Public Class frmThongkeNguoiSudung

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
        query &= " nhansu.id, ten, count(id_nguoisudung) as solanmuon, sum(sokmdachay) as tongsokm from nhansu inner join sudungxe on id_nguoisudung = nhansu.id where 1=1"
        If barGlueNguoiSd.EditValue IsNot Nothing Then
            query &= " and id_nguoisudung=@id_nguoisudung"
            AddParameterWhere("@id_nguoisudung", barGlueNguoiSd.EditValue)
        End If

        If barDeTuNgay.Enabled = True Then
            query = query + " and ngaydi >= @detungay "
            AddParameterWhere("@detungay", barDeTuNgay.EditValue)
        End If
        If barDeDenNgay.Enabled = True Then
            query = query + " and ngayve <= @dedenngay "
            AddParameterWhere("@dedenngay", barDeDenNgay.EditValue)
        End If
        query = query + " group by nhansu.id, ten "
        gcNguoiSudung.DataSource = ExecuteSQLDataTable(query)

        'barCbxNSD.Checked = True
    End Sub
    Private Sub frmThongkeNguoiSudung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        barCbbXem.EditValue = "Top 500"
        loadData()
        Dim query = "select ID, Ten from nhansu where trangthai=1 and noictac=74"
        riGlueNSD.DataSource = ExecuteSQLDataTable(query)
        riGlueNSD.View.PopulateColumns(riGlueNSD.DataSource)
        riGlueNSD.View.Columns(riGlueNSD.ValueMember).Visible = False
        barDeTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        barDeDenNgay.EditValue = DateTime.Today
    End Sub
    

    Private Sub barLueNSD_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadData()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        loadData()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Dim saveDialog As SaveFileDialog = New SaveFileDialog()
        saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html"
        If saveDialog.ShowDialog() = DialogResult.OK Then
            Dim exportFilePath As String = saveDialog.FileName
            Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
            Dim str As String
            Select Case fileExtenstion
                Case ".xls"
                    gcNguoiSudung.ExportToXls(exportFilePath)
                Case(".xlsx")
                    gcNguoiSudung.ExportToXlsx(exportFilePath)
                Case ".rtf"
                    gcNguoiSudung.ExportToRtf(exportFilePath)
                Case ".pdf"
                    gcNguoiSudung.ExportToPdf(exportFilePath)
            End Select
            System.Diagnostics.Process.Start(exportFilePath)
            If File.Exists(exportFilePath) Then
                Try
                    System.Diagnostics.Process.Start(exportFilePath)
                Catch ex As Exception
                    str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                    ShowBaoLoi(str)
                End Try
            Else
                str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                ShowBaoLoi(str)
            End If
        End If
    End Sub

    Private Sub barDeTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeTuNgay.EditValueChanged
        loadData()
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeDenNgay.EditValueChanged
        loadData()
    End Sub

    Private Sub barGlueNguoiSd_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barGlueNguoiSd.EditValueChanged
        loadData()
    End Sub

    Private Sub riGlueNSD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riGlueNSD.ButtonClick
        If e.Button.Index = 1 Then
            barGlueNguoiSd.EditValue = Nothing
            riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        End If
    End Sub

    Private Sub riGlueNSD_Popup(sender As System.Object, e As System.EventArgs) Handles riGlueNSD.Popup
        riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Private Sub barCbbXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbXem.EditValueChanged
        loadData()
    End Sub
End Class