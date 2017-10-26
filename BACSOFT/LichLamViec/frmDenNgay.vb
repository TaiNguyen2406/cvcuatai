Public Class frmDenNgay 

    Private Sub frmDenNgay_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbNgay.EditValue = Today.Date
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If Me.Tag = "BC" Then
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmLichThiCongCongTrinh).SchLichThiCong.Start = tbNgay.EditValue
        Else
            deskTop.SchLichLamViec.Start = tbNgay.EditValue
        End If
        Me.Close()
    End Sub
End Class