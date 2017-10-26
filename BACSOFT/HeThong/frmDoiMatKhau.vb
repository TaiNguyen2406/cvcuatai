Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Utils.BaoMat
Imports System.Net.Mail
Imports DevExpress.XtraEditors

Public Class frmDoiMatKhau

    Private Sub frmDoiMatKhau_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tbTaiKhoan.EditValue = NguoiDung
        txtDiaChiEmail.EditValue = EmailNguoiDung
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        If tbMatKhauCu.EditValue <> "" Then
            If tbMatKhauCu.EditValue = MatKhau Then
                If tbMatKhauMoi.EditValue <> "" Then

                    AddParameter("@Matkhau", tbMatKhauMoi.EditValue)
                    AddParameterWhere("@Tk", TaiKhoan)
                    If doUpdate("NHANSU", "ID=@Tk") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ShowAlert("Đổi mật khẩu đăng nhập thành công !")
                        Me.Close()
                    End If
                Else
                    ShowCanhBao("Mật khẩu đăng nhập mới không được để trống !")
                End If
            End If
        End If
        If tbMKLuongThuongCu.EditValue <> "" Then
            If tbMKLuongThuongCu.EditValue = MatKhauLT Then
                If tbMKLuongThuongMoi.EditValue <> "" Then
                    AddParameter("@MatKhauLT", tbMKLuongThuongMoi.EditValue)
                    AddParameterWhere("@Tk", TaiKhoan)
                    If doUpdate("NHANSU", "ID=@Tk") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ShowAlert("Đổi mật khẩu xem lương thưởng thành công !")
                        Me.Close()
                    End If
                Else
                    ShowCanhBao("Mật khẩu xem lương thưởng mới không được để trống !")
                End If
            End If
        End If
        If txtMatKhauEmail.Text <> "" Then
            AddParameter("@MatKhauEmail", Utils.BaoMat.MaHoaStr(txtMatKhauEmail.Text, key))
            AddParameterWhere("@Tk", TaiKhoan)
            If doUpdate("NHANSU", "ID=@Tk") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đổi mật khẩu email thành công !")
                Me.Close()
            End If
        End If
    End Sub

    Private Sub LabelControl6_Click(sender As System.Object, e As System.EventArgs) Handles LabelControl6.Click
        If tbMatKhauCu.EditValue = "" Then
            ShowCanhBao("Bạn phải điền mật khẩu đăng nhập để thực hiện thao tác này")
            Exit Sub
        End If
        If ShowCauHoi("Bạn có chắc là muốn lấy lại mất khẩu lương thưởng ?") Then
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,Email,MatKhau,MatKhauLT FROM NHANSU WHERE ID=" & TaiKhoan)
            If Not tb Is Nothing Then
                If tbMatKhauCu.EditValue.ToString <> tb.Rows(0)("MatKhau").ToString Then
                    ShowCanhBao("Mật khẩu không chính xác !")
                    Exit Sub
                End If
                Utils.Email.Send(tb.Rows(0)("Email"), "Mật khẩu xem lương thưởng", "Mật khẩu lương thưởng của bạn là: " & tb.Rows(0)("MatKhauLT").ToString)
            End If
        End If

    End Sub

    Private Sub btnHienThiEmail_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnHienThiEmail.MouseDown
        txtMatKhauEmail.Properties.PasswordChar = ""
    End Sub

    Private Sub btnHienThiEmail_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnHienThiEmail.MouseUp
        txtMatKhauEmail.Properties.PasswordChar = "*"
    End Sub

    Private Sub lblChuKyEmail_Click(sender As System.Object, e As System.EventArgs) Handles lblChuKyEmail.Click
        

        Dim f As New frmHtmlEditor
        f.Height = 500 : f.Width = 970
        f.TopMost = True
        f.Text = "Thiết lập chữ ký khi gửi Email"

        f.StartPosition = FormStartPosition.CenterScreen
        AddHandler f.FormClosed, AddressOf DongFormChuKy
        AddHandler f.Load, AddressOf ShowFormChuKy
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then ShowAlert("Thiết lập nội dung chữ ký Email thành công !")
    End Sub

    Private Sub DongFormChuKy(sender As Object, e As FormClosedEventArgs)
        If ShowCauHoi("Bạn có muốn lưu lại nội dung chữ ký này không ?") Then
            AddParameter("@ChuKyEmail", CType(sender, frmHtmlEditor).txtNoiDungEmail.DocumentText)
            AddParameterWhere("@id_tk", TaiKhoan)
            If doUpdate("NHANSU", "Id=@id_tk") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                CType(sender, XtraForm).DialogResult = Windows.Forms.DialogResult.Cancel
            Else
                CType(sender, XtraForm).DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    Private Sub ShowFormChuKy(sender As Object, e As EventArgs)

        'For Each f As FontFamily In FontFamily.Families
        '    CType(sender, frmHtmlEditor).rcmbFont.Items.Add(f.Name)
        'Next
        'CType(sender, frmHtmlEditor).cmbFont.EditValue = "Times New Roman"
        'CType(sender, frmHtmlEditor).cmbSize.EditValue = 3
        'txtNoiDungEmail.Visible = True

        AddParameter("@ID", TaiKhoan)
        Dim dt As DataTable = ExecuteSQLDataTable("select ChuKyEmail from NHANSU where Id = @ID")
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

            CType(sender, frmHtmlEditor).txtNoiDungEmail.Document.Write(dt.Rows(0)(0).ToString)
        End If
    End Sub


End Class