Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Public Class frmTaiSanHong
    Private Shared query As String
    Private Shared dt As DataTable
    Private Sub loadGV()
        query = "select TaiSan_TaiSanHong.*, tenchitiettaisan, Ten from TaiSan_TaiSanHong inner join NHANSU on idnhansu= NHANSU.ID inner join TaiSan_ChiTietTaiSan on idchitiettaisan=TaiSan_ChiTietTaiSan.id where 1=1 "

        If barLueTaiSan.EditValue IsNot Nothing Then
            query &= " and idtaisan=@idtaisan"
            AddParameterWhere("@idtaisan", barLueTaiSan.EditValue)
        End If
        If barGlueNSD.EditValue IsNot Nothing Then
            query &= " and idnhansu=@idnhansu"
            AddParameterWhere("@idnhansu", barGlueNSD.EditValue)
        End If
        If barLueChiTietTS.EditValue IsNot Nothing Then
            query &= " and idchitiettaisan=@idchitiettaisan"
            AddParameterWhere("@idchitiettaisan", barLueChiTietTS.EditValue)
        End If
        query &= " order by ngaysua"
        gcTaiSanHong.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub loadData()
        loadGV()
        query = "select ID, Ten from nhansu where trangthai=1 and noictac=74"
        riGlueNSD.DataSource = ExecuteSQLDataTable(query)
        riGlueNSD.View.PopulateColumns(riGlueNSD.DataSource)
        riGlueNSD.View.Columns(riGlueNSD.ValueMember).Visible = False
        'barGlueNSD.EditValue = 1
        query = " select TaiSan_TaiSan.id,  ten, Model from Taisan_TaiSan inner join XUATKHO on XUATKHO.Sophieu=TaiSan_TaiSan.Sophieu inner join VATTU on VATTU.ID=TaiSan_TaiSan.idvattu inner join TENVATTU ON VATTU.IDTenvattu =TENVATTU.ID"
        riLueTaiSan.DataSource = ExecuteSQLDataTable(query)
        query = "select id, tenchitiettaisan from TaiSan_ChiTietTaiSan"
        riLueChiTietTS.DataSource = ExecuteSQLDataTable(query)
        'barLueTaiSan.EditValue = 1
    End Sub

    Private Sub frmHongTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub btnThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
        Dim frm = New frmThemTaiSanHong()
        frm.Message = 0
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub btnSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        Dim frm = New frmThemTaiSanHong
        Dim id = If(gvTaiSanHong.GetFocusedRowCellValue("id").ToString = "", "0", gvTaiSanHong.GetFocusedRowCellValue("id"))
        If id <> "0" Then
            frm.Message = id
            frm.ShowDialog()
            loadGV()
        End If
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        Dim id = If(gvTaiSanHong.GetFocusedRowCellValue("id").ToString = "", "0", gvTaiSanHong.GetFocusedRowCellValue("id"))
        If id <> "0" Then
            If ShowCauHoi("Bạn có muốn xóa hư hại của:" + gvTaiSanHong.GetFocusedRowCellValue("tentaisan").ToString + " do """ + gvTaiSanHong.GetFocusedRowCellValue("Ten").ToString + " làm hỏng không ?") Then
                AddParameterWhere("@id", gvTaiSanHong.GetFocusedRowCellValue("id"))
                If doDelete("TaiSan_TaiSanHong", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    loadGV()
                End If
            End If
        End If
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        loadGV()
    End Sub

   
    Private Sub barLueTaiSan_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
        query = "select id, tenchitiettaisan from TaiSan_ChiTietTaiSan"
        If barLueTaiSan.EditValue IsNot Nothing Then
            query &= " where idtaisan=@idtaisan"
            AddParameterWhere("@idtaisan", barLueTaiSan.EditValue)
        End If
        riLueChiTietTS.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub barGlueNSD_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub barLueChiTietTS_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        btnThem.PerformClick()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        btnSua.PerformClick()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        btnXoa.PerformClick()
    End Sub

    Private Sub gvTaiSanHong_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvTaiSanHong.DoubleClick
        btnSua.PerformClick()
    End Sub

   
    Private Sub riLueChiTietTS_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barLueChiTietTS.EditValue = Nothing
        End If
    End Sub

    Private Sub riLueTaiSan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barLueTaiSan.EditValue = Nothing
        End If
    End Sub

    Private Sub riGlueNSD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barGlueNSD.EditValue = Nothing
        End If
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        If gvTaiSanHong.RowCount < 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTaiSanHong_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvTaiSanHong.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub
End Class