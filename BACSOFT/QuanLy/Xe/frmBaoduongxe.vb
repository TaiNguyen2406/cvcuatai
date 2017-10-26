Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Imports System.Runtime.Serialization
Public Class frmBaoduongxe
    Private Sub loadData()
        Dim query As String = "select baoduongxe.*, tenxe, ten,  convert(varchar(10),dinhmuc)+' km thay '+tennhienvatlieu as tennvl  from baoduongxe inner join xe on idxe=xe.id inner join nhansu on idnguoithuchien=nhansu.id inner join dinhmuc on baoduongxe .iddinhmuc=dinhmuc.id  inner join nhienvatlieu on nhienvatlieu.id=id_nhienvatlieu order by ngaybaoduong desc"
        gcBaoduongxe.DataSource = ExecuteSQLDataTable(query)
        query = "select xe.id, tenxe, bienso, xe.ghichu  from xe order by id"
        lueXe.DataSource = ExecuteSQLDataTable(query)
        'barDeTuNgay.EditValue = DateTime.Today
        'barDeDenNgay.EditValue = DateTime.Today
        ' barCbxXe.Checked = True
    End Sub
    Private Sub timkiem()
        Dim query As String = "select baoduongxe.*, tenxe, ten,  convert(varchar(10),dinhmuc)+' km thay '+tennhienvatlieu as tennvl  from baoduongxe inner join xe on idxe=xe.id inner join nhansu on idnguoithuchien=nhansu.id inner join dinhmuc on baoduongxe .iddinhmuc=dinhmuc.id  inner join nhienvatlieu on nhienvatlieu.id=id_nhienvatlieu  where 1=1 "

        If barLueXe.EditValue IsNot Nothing Then
            If barLueXe.EditValue IsNot Nothing Then
                query = query + " and xe.id=@xe_id "
                AddParameter("@xe_id", barLueXe.EditValue)
            End If
        End If
        If barDeTuNgay.EditValue IsNot Nothing Then
            query &= " and  ngaybaoduong >= @tungay "
            AddParameterWhere("@tungay", barDeTuNgay.EditValue)
        End If
        If barDeDenNgay.EditValue IsNot Nothing Then
            query &= " and  ngaybaoduong <= @denngay "
            AddParameterWhere("@denngay", barDeDenNgay.EditValue)
        End If
        query &= " order by ngaybaoduong desc"
        gcBaoduongxe.DataSource = ExecuteSQLDataTable(query)
    End Sub
    Private Sub frmBaoduongxe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub
    Private Sub btnThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
            Dim frm As frmThemBaoduongxe = New frmThemBaoduongxe()
            frm.Message = 0
            frm.ShowDialog()
            loadData()
    End Sub

    Private Sub btnSua_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        If gvBaoduongxe.RowCount > 0 Then
            Dim frm As frmThemBaoduongxe = New frmThemBaoduongxe()
            Dim id As Integer = If(gvBaoduongxe.GetRowCellValue(gvBaoduongxe.FocusedRowHandle, "id").ToString() = "", 0, Convert.ToInt32(gvBaoduongxe.GetRowCellValue(gvBaoduongxe.FocusedRowHandle, "id").ToString()))
            frm.Message = id
            frm.ShowDialog()
            loadData()
        End If
    End Sub

    Private Sub btnXoa_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gvBaoduongxe.RowCount > 0 Then
            If ShowCauHoi("Xóa lần bảo dưỡng của  " & gvBaoduongxe.GetFocusedRowCellValue("tenxe") & " vào ngày " & gvBaoduongxe.GetFocusedRowCellValue("ngaysua") & " ?") Then
                AddParameterWhere("@ID", gvBaoduongxe.GetFocusedRowCellValue("id"))
                If doDelete("baoduongxe", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gvBaoduongxe.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub
    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        btnThem.PerformClick()
    End Sub
    Private Sub btnSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        btnSua.PerformClick()
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barbtnXoa.ItemClick
        btnXoa.PerformClick()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        loadData()

    End Sub

    Private Sub barLueXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueXe.EditValueChanged
        timkiem()
    End Sub

    Private Sub BarButtonItem1_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        timkiem()
    End Sub

    Private Sub lueXe_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueXe.ButtonClick
        If e.Button.Index = 1 Then
            barLueXe.EditValue = Nothing
        End If
    End Sub

    Private Sub barDeTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeTuNgay.EditValueChanged
        timkiem()
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeDenNgay.EditValueChanged
        timkiem()
    End Sub

    Private Sub gvBaoduongxe_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvBaoduongxe.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
    End Sub

    Private Sub BarCheckItem1_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gvBaoduongxe.OptionsView.ShowAutoFilterRow = True
        Else
            gvBaoduongxe.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    
    Private Sub gvBaoduongxe_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvBaoduongxe.DoubleClick
        btnSua.PerformClick()
    End Sub

    Private Sub deTuNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles deTuNgay.ButtonClick
        If e.Button.Index = 1 Then
            barDeTuNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub deDenNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles deDenNgay.ButtonClick
        If e.Button.Index = 1 Then
            barDeDenNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        If gvBaoduongxe.RowCount <= 0 Then
            e.Cancel = True
        End If
    End Sub
End Class