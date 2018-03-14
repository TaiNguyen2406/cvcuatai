Imports BACSOFT.Db.SqlHelper
Public Class frmLichSuNhapXuatKhoTam


    Public idVatTu As Object

    Private dt As DataTable
    Private Sub frmLichSuNhapXuatKhoTam_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sql As String = txtSQL.Text
        sql = sql.Replace("-797879", idVatTu)
        dt = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            Dim XuatTam As Double = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                XuatTam += try2Object(dt.Rows(i)("YeuCauXuat"))
                XuatTam -= try2Object(dt.Rows(i)("YeuCauTra"))
                XuatTam -= try2Object(dt.Rows(i)("XuatThucCT"))
                dt.Rows(i)("XuatTam") = XuatTam
            Next
        End If
        gdv.DataSource = dt
    End Sub

    Public Function try2Object(obj As Object) As Double
        If obj Is DBNull.Value Then Return 0
        Try
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function obj2Double(obj As Object) As Double
        Try
            If obj Is DBNull.Value Then Return 0
            If obj Is Nothing Then Return 0
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub gdvData_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvData.CustomSummaryCalculate

        If TryCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName = "XuatTam" Then
            Try
                Dim xuattam As Double = obj2Double(dt.Compute("SUM(YeuCauXuat)", ""))
                xuattam -= obj2Double(dt.Compute("SUM(YeuCauTra)", ""))
                xuattam -= obj2Double(dt.Compute("SUM(XuatThucCT)", ""))
                e.TotalValue = xuattam
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
                e.TotalValue = 0
            End Try
            e.TotalValueReady = True
        End If
    End Sub

End Class