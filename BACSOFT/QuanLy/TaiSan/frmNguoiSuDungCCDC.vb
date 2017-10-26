Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Public Class frmNguoiSuDungCCDC
    Private Shared query As String
    Private Shared dt As DataTable

    Private Sub loadGV()
        query = "select TaiSan_NguoiSuDungCCDC.*, NHANSU.Ten, tenchitietccdc, DEPATMENT.Ten as TenPB from TaiSan_NguoiSuDungCCDC inner join NHANSU on idnhansu=NHANSU.ID inner join TaiSan_ChiTietCCDC on idchitietccdc=TaiSan_ChiTietCCDC.id inner join TaiSan_CongCuDungCu on TaiSan_CongCuDungCu.id=idccdc inner join DEPATMENT on DEPATMENT.id= IDDepatment where 1=1 "
        If barLueCCDC.EditValue IsNot Nothing Then
            query &= " and idccdc=@idccdc"
            AddParameterWhere("@idccdc", barLueCCDC.EditValue)
        End If
        If barGlueNSD.EditValue IsNot Nothing Then
            query &= " and idnhansu=@idnhansu"
            AddParameterWhere("@idnhansu", barGlueNSD.EditValue)
        End If
        If barLueChiTietCCDC.EditValue IsNot Nothing Then
            query &= " and idchitietccdc=@idchitietccdc"
            AddParameterWhere("@idchitietccdc", barLueChiTietCCDC.EditValue)
        End If

        query &= " order by NHANSU.Ten"
        gcNguoiSuDung.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub loadData()
        loadGV()
        query = "select ID, Ten from nhansu where trangthai=1 and noictac=74"
        riGlueNSD.DataSource = ExecuteSQLDataTable(query)
        riGlueNSD.View.PopulateColumns(riGlueNSD.DataSource)
        riGlueNSD.View.Columns(riGlueNSD.ValueMember).Visible = False
        'barGlueNSD.EditValue = 1
        query = " select TaiSan_CongCuDungCu.id,ten, Model from TaiSan_CongCuDungCu inner join XUATKHO on XUATKHO.Sophieu=TaiSan_CongCuDungCu.Sophieu inner join VATTU on VATTU.ID=TaiSan_CongCuDungCu.idvattu inner join TENVATTU ON VATTU.IDTenvattu =TENVATTU.ID"
        riLueCCDC.DataSource = ExecuteSQLDataTable(query)
        query = "select id, tenchitiettaisan from TaiSan_ChiTietCCDC"
        riLueChiTietCCDC.DataSource = ExecuteSQLDataTable(query)
        'barLueCCDC.EditValue = 1
    End Sub
    Private Sub frmNguoiSuDung_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub btnThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
        Dim frm = New frmThemNsdCCDC()
        frm.Message = 0
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub btnSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        Dim frm = New frmThemNsdCCDC
        Dim id = If(gvNguoiSuDung.GetFocusedRowCellValue("id") Is Nothing, 0, gvNguoiSuDung.GetFocusedRowCellValue("id"))
        If id <> 0 Then
            frm.Message = id
            frm.ShowDialog()
            loadGV()
        End If
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        Dim id = If(gvNguoiSuDung.GetFocusedRowCellValue("id").ToString = "", "0", gvNguoiSuDung.GetFocusedRowCellValue("id"))
        If id <> "0" Then
            If ShowCauHoi("Bạn có muốn xóa sử dụng của:" + gvNguoiSuDung.GetFocusedRowCellValue("Ten").ToString + " với : """ + gvNguoiSuDung.GetFocusedRowCellValue("tenchitiettaisan").ToString + " không ?") Then
                AddParameterWhere("@id", gvNguoiSuDung.GetFocusedRowCellValue("id"))
                If doDelete("TaiSan_NguoiSuDungCCDC", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    loadGV()
                End If
            End If
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        btnThem.PerformClick()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        btnSua.PerformClick()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        btnXoa.PerformClick()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        loadData()
    End Sub

    Private Sub gvNguoiSuDung_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvNguoiSuDung.DoubleClick
        btnSua.PerformClick()
    End Sub

    Private Sub gvNguoiSuDung_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvNguoiSuDung.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub

    Private Sub barLueCCDC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueCCDC.EditValueChanged
        loadGV()
        query = "select id, tenchitietccdc from TaiSan_ChiTietCCDC where 1=1"
        If barLueCCDC.EditValue IsNot Nothing Then
            query &= " and idccdc=@idccdc"
            AddParameterWhere("@idccdc", barLueCCDC.EditValue)
        End If
        riLueChiTietCCDC.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub barLueChiTietCCDC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueChiTietCCDC.EditValueChanged
        loadGV()
    End Sub

    Private Sub barGlueNSD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barGlueNSD.EditValueChanged
        loadGV()
    End Sub

    Private Sub riLueTaiSan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueCCDC.ButtonClick
        If e.Button.Index = 1 Then
            barLueCCDC.EditValue = Nothing
        End If
    End Sub

    Private Sub riGlueNSD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riGlueNSD.ButtonClick
        If e.Button.Index = 1 Then
            barGlueNSD.EditValue = Nothing
            riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        End If
    End Sub


    Private Sub riGlueNSD_Popup(sender As System.Object, e As System.EventArgs) Handles riGlueNSD.Popup
        riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Private Sub riLueChiTietTS_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueChiTietCCDC.ButtonClick
        If e.Button.Index = 1 Then
            barLueChiTietCCDC.EditValue = Nothing
        End If
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        If gvNguoiSuDung.RowCount < 1 Then
            e.Cancel = True
        End If
    End Sub


End Class