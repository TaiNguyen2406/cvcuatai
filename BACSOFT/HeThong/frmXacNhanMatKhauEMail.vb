Public Class frmXacNhanMatKhauEmail


    Private Sub btXemMK_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btXemMK.MouseDown
        tbMK.Properties.PasswordChar = Nothing
    End Sub

    Private Sub btXemMK_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btXemMK.MouseUp
        tbMK.Properties.PasswordChar = "*"
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If tbMK.EditValue = "" Then
            ShowBaoLoi("Mật khẩu không được để trống !")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        fCNYeuCauHoiGia._MatKhauEmailGui = tbMK.EditValue
        Me.Close()
    End Sub
End Class