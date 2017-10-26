Public Class frmXuatTam

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim frm = New frmLichSuXuatTam
        frm.ShowDialog()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim frm = New frmThemXuatTam
        frm.ShowDialog()
    End Sub

    Private Sub frmXuatTam_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    Private Sub gvChiTietXuatTam_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gcChiTietXuatTam.PointToScreen(e.Location))
        End If
    End Sub
    Private Sub gvXuatTam_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gvXuatTam.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            PopupMenu1.HidePopup()
        End If
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
       
        If gvChiTietXuatTam.RowCount < 1 Then
            '   e.Cancel = True
        End If
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Dim frm = New frmThemNhapLai
        frm.ShowDialog()

    End Sub
End Class