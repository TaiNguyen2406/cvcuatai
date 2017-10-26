Imports BACSOFT.Db.SqlHelper

Public Class frmThayIDKH

    Public _oldID As Object
    Public _oldttcMa As String = ""

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If tbIDThayThe.EditValue = "" Then
            ShowBaoLoi("Phải điền ID thay thế trước khi xác nhận !")
            Exit Sub
        End If
        Dim sql As String = ""
        If Me.Tag = "KH" Then
            sql = "SELECT ttcMa FROM KHACHHANG WHERE ID=" & tbIDThayThe.EditValue
        ElseIf Me.Tag = "NGD" Then
            sql = "SELECT Ten FROM NHANSU WHERE ID=" & tbIDThayThe.EditValue
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If tb.Rows.Count = 0 Then
                ShowCanhBao("ID này không tồn tại !")
                Exit Sub
            End If
        Else
            ShowCanhBao(LoiNgoaiLe)
        End If

        If ShowCauHoi("Bạn đang thay thế KH " & _oldttcMa & " bằng KH: " & tb.Rows(0)(0).ToString & ", bạn có muốn tiếp tục không ? ") Then
            sql = ""
            If Me.Tag = "KH" Then
                BeginTransaction()
                AddParameterWhere("@IDKH", _oldID)

                sql &= " UPDATE BANGCHAOGIA SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDKhachhang=@IDKH"
                sql &= " UPDATE BANGYEUCAU SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDkhachhang=@IDKH"
                sql &= " UPDATE CHI  SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDKh=@IDKH"
                sql &= " UPDATE KHVATTUSUDUNG SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDKhachhang=@IDKH"
                sql &= " UPDATE NODAUKY SET IDKh=" & tbIDThayThe.EditValue & " WHERE IDkh=@IDKH"
                sql &= " UPDATE NOPHAITHU SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDKH=@IDKH"
                sql &= " UPDATE NOPHAITRA SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDKH=@IDKH"
                sql &= " UPDATE PHIEUDATHANG SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDKhachhang=@IDKH"
                sql &= " UPDATE PHIEUNHAPKHO SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDKhachhang=@IDKH"
                sql &= " UPDATE PHIEUXUATKHO SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDkhachhang=@IDKH"
                sql &= " UPDATE PHIEUYEUCAUDI SET IDKhachHang=" & tbIDThayThe.EditValue & " WHERE IDKhachhang=@IDKH"
                sql &= " UPDATE THU SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDkh=@IDKH"
                sql &= " UPDATE THUNH SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDKh=@IDKH"
                sql &= " UPDATE UNC SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDKh=@IDKH"
                sql &= " UPDATE NHANSU SET Noictac=" & tbIDThayThe.EditValue & " WHERE Noictac=@IDKH"
                sql &= " UPDATE GIAODICHKH SET IDKH=" & tbIDThayThe.EditValue & " WHERE IDKH=@IDKH"
                sql &= " DELETE KHACHHANG WHERE ID=@IDKH"
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    RollBackTransaction()
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ComitTransaction()
                    CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmKhachHang).loadDS()
                    ShowAlert("Đã thay thế !")
                    Me.Close()
                End If
            ElseIf Me.Tag = "NGD" Then
                BeginTransaction()
                AddParameterWhere("@IDNgd", _oldID)
                sql &= " UPDATE BANGCHAOGIA SET IDNgd=" & tbIDThayThe.EditValue & " WHERE IDNgd=@IDNgd"
                sql &= " UPDATE BANGYEUCAU SET IDNgd=" & tbIDThayThe.EditValue & " WHERE IDNgd=@IDNgd"
                sql &= " UPDATE PHIEUDATHANG  SET IDNgd=" & tbIDThayThe.EditValue & " WHERE IDNgd=@IDNgd"
                sql &= " UPDATE PHIEUXUATKHO SET IDNgd=" & tbIDThayThe.EditValue & " WHERE IDNgd=@IDNgd"
                sql &= " UPDATE PHIEUYEUCAUDI SET IDNgd=" & tbIDThayThe.EditValue & " WHERE IDNgd=@IDNgd"
                
                sql &= " DELETE NHANSU WHERE ID=@IDNgd"
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    RollBackTransaction()
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ComitTransaction()
                    For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                        If deskTop.tabMain.TabPages(i).Tag = "mDSKhachHang" Then
                            CType(deskTop.tabMain.TabPages(i).Controls(0), frmKhachHang).loadDS()
                        ElseIf deskTop.tabMain.TabPages(i).Tag = "mDSNguoiGiaoDich" Then
                            CType(deskTop.tabMain.TabPages(i).Controls(0), frmNguoiGiaoDich).loadDS()
                        End If
                    Next
                    ShowAlert("Đã thay thế !")
                    Me.Close()
                End If
            End If

        End If

    End Sub
End Class