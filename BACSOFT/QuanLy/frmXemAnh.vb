Public Class frmXemAnh 

    Private Sub frmXemAnh_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub pAnh_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles pAnh.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class