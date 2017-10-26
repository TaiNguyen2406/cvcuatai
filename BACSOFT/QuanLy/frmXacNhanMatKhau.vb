Public Class frmXacNhanMatKhau 

    Private Sub tbMatKhau_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbMatKhau.KeyDown
        If e.KeyCode = Keys.Enter Then
            If tbMatKhau.EditValue = MatKhauLT Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmLuongThuong).SaiMatKhau = False
                Me.Close()
            Else
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmLuongThuong).SaiMatKhau = True
                ShowCanhBao("Mật khẩu không chính xác !")
            End If
        End If
        
    End Sub
End Class