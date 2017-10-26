Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmGiayToXe
    Private Shared query As String
    Private Shared dt As DataTable
    Private Sub loadData()

        query = "select GiayToXe.*, tenxe from GiayToXe inner join Xe on idxe=Xe.id where 1=1"
        If barLueXe.EditValue IsNot Nothing Then
            If barLueXe.EditValue IsNot Nothing Then
                query = query + " and idxe=@idxe "
                AddParameter("@idxe", barLueXe.EditValue)
            End If
        End If
        If barDeTuNgay.EditValue IsNot Nothing Then
            query &= " and  ngayhethan >= @tungay "
            AddParameterWhere("@tungay", barDeTuNgay.EditValue)
        End If
        If barDeDenNgay.EditValue IsNot Nothing Then
            query &= " and  ngayhethan <= @denngay "
            AddParameterWhere("@denngay", barDeDenNgay.EditValue)
        End If
        If barLueLoaiGiayTo.EditValue IsNot Nothing Then
            query &= " and idloaigiayto=@idloaigiayto "
            AddParameterWhere("@idloaigiayto", barLueLoaiGiayTo.EditValue)
        End If
        If btnLoad.Caption = "Tất cả" Then
            query &= "and ngayhethan<=@ngayhethan"
            AddParameterWhere("@ngayhethan", Today)
        End If
        query &= " order by ngayhethan "
        gcGiayToXe.DataSource = ExecuteSQLDataTable(query)
    End Sub

    Private Sub frmGiayToXe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        riLueLoaiGiayTo.DataSource = tableLoaiGiayTo()
        barRiLoaiGiayTo.DataSource = tableLoaiGiayTo()
        lueXe.DataSource = ExecuteSQLDataTable("select * from xe order by id desc")
        loadData()
    End Sub
    Private Sub lueXe_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueXe.ButtonClick
        If e.Button.Index = 1 Then
            barLueXe.EditValue = Nothing
        End If
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

    Private Sub barRiLoaiGiayTo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles barRiLoaiGiayTo.ButtonClick
        If e.Button.Index = 1 Then
            barLueLoaiGiayTo.EditValue = Nothing
        End If
    End Sub
    Private Sub barLueXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueXe.EditValueChanged
        loadData()
    End Sub

    Private Sub barDeTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeTuNgay.EditValueChanged
        loadData()
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeDenNgay.EditValueChanged
        loadData()
    End Sub

    Private Sub barLoaiGiayTo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueLoaiGiayTo.EditValueChanged
        loadData()
    End Sub
    Private Sub btnThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
        Dim frm As frmThemGiayToXe = New frmThemGiayToXe()
        frm.Message = 0
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub btnSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        Dim frm As frmThemGiayToXe = New frmThemGiayToXe()
        Dim id As Integer = If(gvGiayToXe.GetFocusedRowCellValue("id") Is Nothing, 0, gvGiayToXe.GetFocusedRowCellValue("id"))
        Dim row = gvGiayToXe.FocusedRowHandle
        frm.Message = id
        frm.ShowDialog()
        gvGiayToXe.FocusedRowHandle = row
        loadData()
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gvGiayToXe.RowCount > 0 Then
            If ShowCauHoi("Xóa giấy tờ " & gvGiayToXe.GetFocusedRowCellDisplayText("idloaigiayto") & " của  " & gvGiayToXe.GetFocusedRowCellValue("tenxe") & " có ngày hết hạn " & gvGiayToXe.GetFocusedRowCellValue("ngayhethan") & " ?") Then
                AddParameterWhere("@id", gvGiayToXe.GetFocusedRowCellValue("id"))
                If doDelete("GiayToXe", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gvGiayToXe.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub
    Private Sub BarCheckItem1_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gvGiayToXe.OptionsView.ShowAutoFilterRow = True
        Else
            gvGiayToXe.OptionsView.ShowAutoFilterRow = False
        End If

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoad.ItemClick
        If btnLoad.Caption = "Hết hạn" Then
            btnLoad.Caption = "Tất cả"
        Else
            btnLoad.Caption = "Hết hạn"
        End If
        loadData()
    End Sub

    Private Sub gvGiayToXe_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvGiayToXe.DoubleClick
        btnSua.PerformClick()
    End Sub

    Private Sub gvGiayToXe_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvGiayToXe.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If

    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        If gvGiayToXe.RowCount < 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        btnThem.PerformClick()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        btnSua.PerformClick()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        btnXoa.PerformClick()
    End Sub

    Private Sub gvGiayToXe_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gvGiayToXe.RowCellStyle
        ' For j = 0 To gvGiayToXe.RowCount()
        Dim ngayhethan As Date = gvGiayToXe.GetRowCellDisplayText(e.RowHandle, gvGiayToXe.Columns("ngayhethan"))
        If ngayhethan <= Today Then
            If e.Column.FieldName = "ngayhethan" Then
                e.Appearance.BackColor = Color.Red
                e.Appearance.ForeColor = Color.White
            End If
        End If
        '   Next j
    End Sub
End Class