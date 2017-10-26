Imports BACSOFT.Db.SqlHelper

Public Class frmDoiID
    Public _TenCot As String = ""
    Public _oldID As Object
    Public _IsVatTu As Boolean = False

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If tbIDThayThe.EditValue = "" Then
            ShowBaoLoi("Phải điền ID thay thế trước khi xác nhận !")
            Exit Sub
        End If
        If _IsVatTu Then
            If ShowCauHoi("Lưu ý thao tác này không thể quay lại, bạn có chắc chắn muốn thay thế hay không ?") Then
                If tbIDThayThe.EditValue = _oldID Then
                    ShowCanhBao("Bạn cần nhập ID vật tư khác để thay thế !")
                    Exit Sub
                End If
                Dim sql As String = " Update NHAPKHO Set IDVattu = " & tbIDThayThe.EditValue & " Where IDVattu = " & _oldID
                sql &= " Update XUATKHO Set IDVattu = " & tbIDThayThe.EditValue & " Where IDVattu = " & _oldID
                sql &= " Update CHAOGIA Set IDVattu = " & tbIDThayThe.EditValue & " Where IDVattu = " & _oldID
                sql &= " Update DATHANG Set IDVattu = " & tbIDThayThe.EditValue & " Where IDVattu = " & _oldID
                sql &= " Update YEUCAUDEN Set IDVattu = " & tbIDThayThe.EditValue & " Where IDVattu = " & _oldID
                sql &= " DELETE VATTU  WHERE ID = " & _oldID
                BeginTransaction()
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    RollBackTransaction()
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ComitTransaction()
                    ShowThongBao("Đã thay thế ID và xóa vật tư vừa chọn!")
                    Dim index As Object = CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).gdvCT.FocusedRowHandle

                    CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).gdvCT.DeleteSelectedRows()

                    ' CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).gdvCT.ClearSelection()

                    'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).gdvCT.FocusedRowHandle = index
                    'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).gdvCT.SelectRow(index)
                    Me.Close()
                End If



            End If
        Else
            If ShowCauHoi("Bạn có chắc bạn thao tác đúng không? nếu sai sẽ không thể khôi phục lại. ") Then
                If ExecuteSQLNonQuery("UPDATE VATTU SET " & _TenCot & "=" & tbIDThayThe.EditValue & " WHERE " & _TenCot & " = " & _oldID) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongTinPhu)._exit = True
                Else
                    ShowThongBao("Đã thay thế ID giờ bạn đã có thể xoá đối tượng vừa chọn !")
                    Me.Close()
                End If
            Else
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongTinPhu)._exit = True
                Me.Close()
            End If
        End If



    End Sub
End Class