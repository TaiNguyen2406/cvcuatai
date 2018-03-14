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
        query = "  select TaiSan_ChiTietTaiSan.*, isnull(TENVATTU .ten,TenTaiSan) as TenVT from TaiSan_TaiSan inner join TaiSan_ChiTietTaiSan on TaiSan_TaiSan.id=idtaisan left join XUATKHO on TaiSan_TaiSan.idxuatkho=XUATKHO.ID  left JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID where idtaisan=@idtaisan "
        'TaiSan_TaiSan.sophieu=XUATKHO.Sophieu and TaiSan_TaiSan.idvattu=XUATKHO.IDvattu 
        AddParameterWhere("@idtaisan", _message)
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

    Private Sub gvChiTietTaiSan_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gv.DoubleClick
        gv.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gvChiTietTaiSan_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gv.HiddenEditor
        gv.OptionsBehavior.Editable = False
    End Sub

    Private Sub gvChiTietTaiSan_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.Delete Then

        End If
    End Sub

    Private Sub gvChiTietTaiSan_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv.RowUpdated
        If KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.KiemDuyet) Then
            gv.PostEditor()
            gv.UpdateCurrentRow()
            AddParameter("@tenchitiettaisan", gv.GetFocusedRowCellValue("tenchitiettaisan"))
            AddParameter("@idtinhtrang", gv.GetFocusedRowCellValue("idtinhtrang"))
            If gv.GetFocusedRowCellValue("idtinhtrang") <> 3 Then
                AddParameter("@ngaythanhly", DBNull.Value)
            Else
                AddParameter("@ngaythanhly", gv.GetFocusedRowCellValue("ngaythanhly"))
            End If

            AddParameter("@idtaisan", _message)
            If Not IsDBNull(gv.GetFocusedRowCellValue("id")) Then
                AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
                If doUpdate("TaiSan_ChiTietTaiSan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gv.SetFocusedRowCellValue("id", gv.GetFocusedRowCellValue("id"))
                    loadGV()
                End If
            Else
                If doInsert("TaiSan_ChiTietTaiSan") Is Nothing Then
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

    Private Sub gvChiTietTaiSan_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) 'Handles gv.CustomSummaryCalculate
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