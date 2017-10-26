Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.Data

Public Class frmChiTietTaiSan
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
        query = "  select TaiSan_ChiTietTaiSan.*, TENVATTU .ten as TenVT from TaiSan_TaiSan inner join TaiSan_ChiTietTaiSan on TaiSan_TaiSan.id=idtaisan inner join XUATKHO on TaiSan_TaiSan.idxuatkho=XUATKHO.ID  INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID where idtaisan=@idtaisan "
        'TaiSan_TaiSan.sophieu=XUATKHO.Sophieu and TaiSan_TaiSan.idvattu=XUATKHO.IDvattu 
        AddParameterWhere("@idtaisan", _message)
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            gcChiTietTaiSan.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        Dim Item As New GridGroupSummaryItem
    End Sub

    Private Sub frmChiTietTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadGV()
       
    End Sub

    Private Sub gvChiTietTaiSan_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvChiTietTaiSan.DoubleClick
        gvChiTietTaiSan.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvChiTietTaiSan_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvChiTietTaiSan.HiddenEditor
        gvChiTietTaiSan.OptionsBehavior.Editable = False
    End Sub

    Private Sub gvChiTietTaiSan_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvChiTietTaiSan.KeyDown
        If e.KeyCode = Keys.Delete Then

        End If
    End Sub

    Private Sub gvChiTietTaiSan_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvChiTietTaiSan.RowUpdated
        gvChiTietTaiSan.UpdateCurrentRow()
        AddParameter("@tenchitiettaisan", gvChiTietTaiSan.GetFocusedRowCellValue("tenchitiettaisan"))
        AddParameter("@idtinhtrang", gvChiTietTaiSan.GetFocusedRowCellValue("idtinhtrang"))
        AddParameter("@ngaythanhly", gvChiTietTaiSan.GetFocusedRowCellValue("ngaythanhly"))
        AddParameterWhere("@id", gvChiTietTaiSan.GetFocusedRowCellValue("id"))
        If doUpdate("TaiSan_ChiTietTaiSan", "id=@id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gvChiTietTaiSan.SetFocusedRowCellValue("id", gvChiTietTaiSan.GetFocusedRowCellValue("id"))
            loadGV()
        End If
    End Sub
    Dim sudung As Integer
    Dim chuasudung As Integer
    Dim thanhly As Integer

    Private Sub gvChiTietTaiSan_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gvChiTietTaiSan.CustomSummaryCalculate
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