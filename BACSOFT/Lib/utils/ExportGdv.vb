Namespace Utils
    Public Class ExportGdv
        Public Shared Sub ExportToExcel(ByVal fileName As String, ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView, Optional ByVal exDisplay As Boolean = True)
            Dim provider As New DevExpress.XtraExport.ExportXlsProviderInternal(fileName)
            'DevExpress.XtraExport.exp()
            Dim currentCursor As Cursor = System.Windows.Forms.Cursor.Current
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
            Dim link As DevExpress.XtraGrid.Export.BaseExportLink = grid.CreateExportLink(provider)
            'TryCast(link, DevExpress.XtraGrid.Export.GridViewExportLink).ExportAll = True
            'TryCast(link, DevExpress.XtraGrid.Export.GridViewExportLink).ExportDetails = False
            'TryCast(link, DevExpress.XtraGrid.Export.GridViewExportLink).ExportCellsAsDisplayText = False
            link.ExportAll = True
            link.ExportCellsAsDisplayText = exDisplay
            link.ExportTo(True)
            provider.Dispose()
            System.Windows.Forms.Cursor.Current = currentCursor
        End Sub
    End Class
End Namespace
