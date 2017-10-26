Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Public Class frmCCDCHong
    Private Shared query As String
    Private Shared dt As DataTable
    Private Sub loadGV()
        query = "select Taisan_CCDCHong.*, tenchitietccdc, Ten from Taisan_CCDCHong inner join NHANSU on idnhansu= NHANSU.ID inner join Taisan_ChiTIetCCDC on idchitietccdc=Taisan_ChiTIetCCDC.id where 1=1 "

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
        query &= " order by ngaysua"
        gcCCDCHong.DataSource = ExecuteSQLDataTable(query)
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
    End Sub

    Private Sub frmHongTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub btnThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
        Dim frm = New frmThemCCDCHong()
        frm.Message = 0
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub btnSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        Dim frm = New frmThemCCDCHong()
        Dim id = If(gvCCDCHong.GetFocusedRowCellValue("id").ToString = "", "0", gvCCDCHong.GetFocusedRowCellValue("id"))
        If id <> "0" Then
            frm.Message = id
            frm.ShowDialog()
            loadGV()
        End If
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        Dim id = If(gvCCDCHong.GetFocusedRowCellValue("id").ToString = "", "0", gvCCDCHong.GetFocusedRowCellValue("id"))
        If id <> "0" Then
            If ShowCauHoi("Bạn có muốn xóa hư hại của:" + gvCCDCHong.GetFocusedRowCellValue("tentaisan").ToString + " do """ + gvCCDCHong.GetFocusedRowCellValue("Ten").ToString + " làm hỏng không ?") Then
                AddParameterWhere("@id", gvCCDCHong.GetFocusedRowCellValue("id"))
                If doDelete("Taisan_CCDCHong", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    loadGV()
                End If
            End If
        End If
    End Sub

    Private Sub btnTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiLai.ItemClick
        loadGV()
    End Sub


    Private Sub barLueCCDC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueCCDC.EditValueChanged
        loadGV()
        query = "select id, tenchitiettaisan from Taisan_ChiTIetCCDC"
        If barLueCCDC.EditValue IsNot Nothing Then
            query &= " where idccdc=@idccdc"
            AddParameterWhere("@idccdc", barLueCCDC.EditValue)
        End If
        riLueChiTietCCDC.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub barGlueNSD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barGlueNSD.EditValueChanged
        loadGV()
    End Sub

    Private Sub barLueChiTietCCDC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueChiTietCCDC.EditValueChanged
        loadGV()
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

    Private Sub gvCCDCHong_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvCCDCHong.DoubleClick
        btnSua.PerformClick()
    End Sub


    Private Sub riLueChiTietCCDC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueChiTietCCDC.ButtonClick
        If e.Button.Index = 1 Then
            barLueChiTietCCDC.EditValue = Nothing
        End If
    End Sub

    Private Sub riLueCCDC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueCCDC.ButtonClick
        If e.Button.Index = 1 Then
            barLueCCDC.EditValue = Nothing
        End If
    End Sub

    Private Sub riGlueNSD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riGlueNSD.ButtonClick
        If e.Button.Index = 1 Then
            barGlueNSD.EditValue = Nothing
        End If
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        If gvCCDCHong.RowCount < 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvCCDCHong_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvCCDCHong.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub
End Class