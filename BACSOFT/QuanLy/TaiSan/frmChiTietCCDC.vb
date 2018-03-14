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
        query = "  select TaiSan_ChiTietCCDC.*,  isnull(TENVATTU .ten,TenCCDC) as TenVT  from TaiSan_CongCuDungCu inner join TaiSan_ChiTietCCDC on TaiSan_CongCuDungCu.id=idccdc left join XUATKHO on TaiSan_CongCuDungCu.idxuatkho=XUATKHO.ID left JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID where idccdc=@idccdc "
        AddParameterWhere("@idccdc", _message)
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            gc.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        Dim Item As New GridGroupSummaryItem
    End Sub

    Private Sub frmChiTietTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadGV()

    End Sub

    Private Sub gvChiTietCCDC_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gv.DoubleClick
        gv.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvChiTietCCDC_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gv.HiddenEditor
        gv.OptionsBehavior.Editable = False
    End Sub

    Private Sub gvChiTietCCDC_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.Delete Then

        End If
    End Sub

    Private Sub gvChiTietCCDC_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv.RowUpdated
        If KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.KiemDuyet) Then
            gv.PostEditor()
            gv.UpdateCurrentRow()
            AddParameter("@tenchitietccdc", gv.GetFocusedRowCellValue("tenchitietccdc"))
            AddParameter("@idtinhtrang", gv.GetFocusedRowCellValue("idtinhtrang"))
            If gv.GetFocusedRowCellValue("idtinhtrang") <> 3 Then
                AddParameter("@ngaythanhly", DBNull.Value)
            Else
                AddParameter("@ngaythanhly", gv.GetFocusedRowCellValue("ngaythanhly"))
            End If
            AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
            If Not IsDBNull(gv.GetFocusedRowCellValue("id")) Then
                If doUpdate("TaiSan_ChiTietCCDC", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gv.SetFocusedRowCellValue("id", gv.GetFocusedRowCellValue("id"))
                    loadGV()
                End If
            Else
                If doInsert("TaiSan_ChiTietCCDC") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    loadGV()
                End If
            End If
        End If
    End Sub
    Dim sudung As Integer
    Dim chuasudung As Integer
    Dim thanhly As Integer

    Private Sub gv_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gv.CustomSummaryCalculate
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
            Dim check As Object = (View.GetRowCellValue(e.RowHandle, "idtinhtrang"))
            If IsDBNull(check) Then
                check = 0
            End If
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
    Private Sub btnXoa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
            If doDelete("TaiSan_ChiTietTaiSan", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gv.DeleteSelectedRows()
            End If
        End If

    End Sub
    Private Sub gv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gv.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gv.CalcHitInfo(gc.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gc.PointToScreen(e.Location))
        End If
    End Sub
End Class