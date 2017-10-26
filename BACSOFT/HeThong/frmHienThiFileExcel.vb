Imports SpreadsheetGear

Public Class frmHienThiFileExcel

    Public workbook As IWorkbook
    Private Sub frmHienThiFileExcel_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnIn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnIn.ItemClick
        excelViewer.PrintPreview()
    End Sub

    Private Sub btnKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick
        Dim saveDlg As New SaveFileDialog
        saveDlg.Filter = "Excel files (*.xls)|*.xls"

        saveDlg.FileName = Me.Tag


        If saveDlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            excelViewer.GetLock()
            workbook.SaveAs(saveDlg.FileName, FileFormat.Excel8)
            excelViewer.ReleaseLock()
            If ShowCauHoi("Bạn có muốn mở file vừa lưu xuất không ?") Then
                Dim p As New System.Diagnostics.Process
                p.StartInfo.FileName = saveDlg.FileName
                p.Start()
            End If
        End If
    End Sub

    Private Sub btnDongLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDongLai.ItemClick
        Me.Close()
    End Sub

End Class