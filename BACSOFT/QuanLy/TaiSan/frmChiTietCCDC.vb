Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.Data

Public Class frmChiTietCCDC
    Protected Shared _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property
    Private Shared query As String
    Private Sub loadGV()
        riLueTinhTrang.DataSource = tableTinhTrangTS()
        query = "  select TaiSan_ChiTietCCDC.*, TENVATTU .ten as TenVT  from TaiSan_CongCuDungCu inner join TaiSan_ChiTietCCDC on TaiSan_CongCuDungCu.id=idccdc inner join XUATKHO on TaiSan_CongCuDungCu.idxuatkho=XUATKHO.ID INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID where idccdc=@idccdc "
        'on TaiSan_CongCuDungCu.sophieu=XUATKHO.Sophieu and TaiSan_CongCuDungCu.idvattu=XUATKHO.IDvattu 
        AddParameterWhere("@idccdc", _message)
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            gcChiTietCCDC.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        Dim Item As New GridGroupSummaryItem
    End Sub

    Private Sub frmChiTietTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadGV()

    End Sub

    Private Sub gvChiTietCCDC_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvChiTietCCDC.DoubleClick
        gvChiTietCCDC.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvChiTietCCDC_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvChiTietCCDC.HiddenEditor
        gvChiTietCCDC.OptionsBehavior.Editable = False
    End Sub

    Private Sub gvChiTietCCDC_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvChiTietCCDC.KeyDown
        If e.KeyCode = Keys.Delete Then

        End If
    End Sub

    Private Sub gvChiTietCCDC_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvChiTietCCDC.RowUpdated
        gvChiTietCCDC.UpdateCurrentRow()
        AddParameter("@tenchitietccdc", gvChiTietCCDC.GetFocusedRowCellValue("tenchitietccdc"))
        AddParameter("@idtinhtrang", gvChiTietCCDC.GetFocusedRowCellValue("idtinhtrang"))
        AddParameter("@ngaythanhly", gvChiTietCCDC.GetFocusedRowCellValue("ngaythanhly"))
        AddParameterWhere("@id", gvChiTietCCDC.GetFocusedRowCellValue("id"))
        If doUpdate("TaiSan_ChiTietCCDC", "id=@id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gvChiTietCCDC.SetFocusedRowCellValue("id", gvChiTietCCDC.GetFocusedRowCellValue("id"))
            loadGV()
        End If
    End Sub
    Dim sudung As Integer
    Dim chuasudung As Integer
    Dim thanhly As Integer

    Private Sub gvChiTietCCDC_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gvChiTietCCDC.CustomSummaryCalculate
        ' Get the summary ID. 
        Dim summaryID As Integer = Convert.ToInt32(CType(e.Item, GridSummaryItem).Tag)
        Dim View As GridView = CType(sender, GridView)
        ' Initialization 
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            sudung = 0
            chuasudung = 0
            thanhly = 0
        End If
        ' Calculation 
        If e.SummaryProcess = CustomSummaryProcess.Calculate Then
            Dim check As Integer = CInt(View.GetRowCellValue(e.RowHandle, "idtinhtrang"))
            Select Case summaryID
                Case 1 ' The total summary calculated against the 'UnitPrice' column. 
                    If check = 1 Then sudung += 1
                Case 2 ' The group summary. 
                    If check = 2 Then chuasudung += 1
                Case 3 ' The group summary. 
                    If check = 3 Then thanhly += 1
            End Select
        End If
        ' Finalization 
        If e.SummaryProcess = CustomSummaryProcess.Finalize Then
            Select Case summaryID
                Case 1
                    e.TotalValue = sudung
                Case 2
                    e.TotalValue = chuasudung
                Case 3
                    e.TotalValue = thanhly
            End Select
        End If
    End Sub
End Class