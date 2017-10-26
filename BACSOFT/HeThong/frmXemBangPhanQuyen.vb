Imports SpreadsheetGear
Public Class frmXemBangPhanQuyen
    Dim workbook As IWorkbook



    Private Sub frmXemBangPhanQuyen_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        workbook = Factory.GetWorkbook("\\bacboss\Data$\Quyen Su Dung.xls", System.Globalization.CultureInfo.CurrentCulture)
        ' Get a reference to the first worksheet.
        ' Dim worksheet As IWorksheet = workbook.Worksheets("Sheet1")
        ExcelViewer.ActiveWorkbook = workbook

    End Sub

    Private Sub btluu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btluu.ItemClick
        ExcelViewer.GetLock()
        workbook.Save()
        ExcelViewer.ReleaseLock()
        ShowAlert("Đã lưu !")
    End Sub

    Private Sub ExcelViewer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles ExcelViewer.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btluu.PerformClick()
        End If
    End Sub

    Private Sub frmXemBangPhanQuyen_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ExcelViewer.GetLock()
        workbook.Close()
        ExcelViewer.ReleaseLock()
    End Sub
End Class