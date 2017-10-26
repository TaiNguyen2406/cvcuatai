<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDoiMatKhau
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTaiKhoan = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbMatKhauCu = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbMatKhauMoi = New DevExpress.XtraEditors.TextEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tbMKLuongThuongCu = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.tbMKLuongThuongMoi = New DevExpress.XtraEditors.TextEdit()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.btnHienThiEmail = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDiaChiEmail = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtMatKhauEmail = New DevExpress.XtraEditors.TextEdit()
        Me.lblChuKyEmail = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tbTaiKhoan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMatKhauCu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMatKhauMoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.tbMKLuongThuongCu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMKLuongThuongMoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.txtDiaChiEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMatKhauEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(30, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tài khoản"
        '
        'tbTaiKhoan
        '
        Me.tbTaiKhoan.Location = New System.Drawing.Point(93, 12)
        Me.tbTaiKhoan.Name = "tbTaiKhoan"
        Me.tbTaiKhoan.Properties.ReadOnly = True
        Me.tbTaiKhoan.Size = New System.Drawing.Size(193, 20)
        Me.tbTaiKhoan.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(17, 28)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Mật khẩu cũ"
        '
        'tbMatKhauCu
        '
        Me.tbMatKhauCu.Location = New System.Drawing.Point(81, 25)
        Me.tbMatKhauCu.Name = "tbMatKhauCu"
        Me.tbMatKhauCu.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMatKhauCu.Size = New System.Drawing.Size(193, 20)
        Me.tbMatKhauCu.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 54)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Mật khẩu mới"
        '
        'tbMatKhauMoi
        '
        Me.tbMatKhauMoi.Location = New System.Drawing.Point(81, 51)
        Me.tbMatKhauMoi.Name = "tbMatKhauMoi"
        Me.tbMatKhauMoi.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMatKhauMoi.Size = New System.Drawing.Size(193, 20)
        Me.tbMatKhauMoi.TabIndex = 1
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Image = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.SimpleButton1.Location = New System.Drawing.Point(144, 365)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(91, 23)
        Me.SimpleButton1.TabIndex = 2
        Me.SimpleButton1.Text = "Xác nhận"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.SimpleButton2.Image = Global.BACSOFT.My.Resources.Resources.Cancel_18
        Me.SimpleButton2.Location = New System.Drawing.Point(241, 365)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton2.TabIndex = 3
        Me.SimpleButton2.Text = "Hủy bỏ"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.tbMatKhauCu)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.tbMatKhauMoi)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 38)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(308, 85)
        Me.GroupControl1.TabIndex = 5
        Me.GroupControl1.Text = "Mật khẩu đăng nhập"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.LabelControl6)
        Me.GroupControl2.Controls.Add(Me.LabelControl4)
        Me.GroupControl2.Controls.Add(Me.tbMKLuongThuongCu)
        Me.GroupControl2.Controls.Add(Me.LabelControl5)
        Me.GroupControl2.Controls.Add(Me.tbMKLuongThuongMoi)
        Me.GroupControl2.Location = New System.Drawing.Point(14, 248)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(306, 102)
        Me.GroupControl2.TabIndex = 5
        Me.GroupControl2.Text = "Mật khẩu xem lương thưởng"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.LabelControl6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LabelControl6.Location = New System.Drawing.Point(17, 77)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(209, 13)
        Me.LabelControl6.TabIndex = 2
        Me.LabelControl6.Text = "*Lấy lại mật khẩu xem lương thưởng"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(17, 28)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Mật khẩu cũ"
        '
        'tbMKLuongThuongCu
        '
        Me.tbMKLuongThuongCu.Location = New System.Drawing.Point(81, 25)
        Me.tbMKLuongThuongCu.Name = "tbMKLuongThuongCu"
        Me.tbMKLuongThuongCu.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMKLuongThuongCu.Size = New System.Drawing.Size(191, 20)
        Me.tbMKLuongThuongCu.TabIndex = 0
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(12, 54)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "Mật khẩu mới"
        '
        'tbMKLuongThuongMoi
        '
        Me.tbMKLuongThuongMoi.Location = New System.Drawing.Point(81, 51)
        Me.tbMKLuongThuongMoi.Name = "tbMKLuongThuongMoi"
        Me.tbMKLuongThuongMoi.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMKLuongThuongMoi.Size = New System.Drawing.Size(191, 20)
        Me.tbMKLuongThuongMoi.TabIndex = 1
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.Controls.Add(Me.lblChuKyEmail)
        Me.GroupControl3.Controls.Add(Me.btnHienThiEmail)
        Me.GroupControl3.Controls.Add(Me.LabelControl8)
        Me.GroupControl3.Controls.Add(Me.txtDiaChiEmail)
        Me.GroupControl3.Controls.Add(Me.LabelControl9)
        Me.GroupControl3.Controls.Add(Me.txtMatKhauEmail)
        Me.GroupControl3.Location = New System.Drawing.Point(14, 129)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(306, 106)
        Me.GroupControl3.TabIndex = 6
        Me.GroupControl3.Text = "Mật khẩu email"
        '
        'btnHienThiEmail
        '
        Me.btnHienThiEmail.CausesValidation = False
        Me.btnHienThiEmail.Image = Global.BACSOFT.My.Resources.Resources.Preview_18
        Me.btnHienThiEmail.Location = New System.Drawing.Point(276, 53)
        Me.btnHienThiEmail.Name = "btnHienThiEmail"
        Me.btnHienThiEmail.Size = New System.Drawing.Size(20, 20)
        Me.btnHienThiEmail.TabIndex = 2
        Me.btnHienThiEmail.ToolTip = "Hiển thị mật khẩu"
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(12, 31)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Địa chỉ"
        '
        'txtDiaChiEmail
        '
        Me.txtDiaChiEmail.Location = New System.Drawing.Point(81, 27)
        Me.txtDiaChiEmail.Name = "txtDiaChiEmail"
        Me.txtDiaChiEmail.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDiaChiEmail.Properties.Appearance.Options.UseBackColor = True
        Me.txtDiaChiEmail.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White
        Me.txtDiaChiEmail.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtDiaChiEmail.Properties.ReadOnly = True
        Me.txtDiaChiEmail.Size = New System.Drawing.Size(191, 20)
        Me.txtDiaChiEmail.TabIndex = 0
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(12, 57)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Mật khẩu"
        '
        'txtMatKhauEmail
        '
        Me.txtMatKhauEmail.Location = New System.Drawing.Point(81, 53)
        Me.txtMatKhauEmail.Name = "txtMatKhauEmail"
        Me.txtMatKhauEmail.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtMatKhauEmail.Size = New System.Drawing.Size(191, 20)
        Me.txtMatKhauEmail.TabIndex = 1
        '
        'lblChuKyEmail
        '
        Me.lblChuKyEmail.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.lblChuKyEmail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblChuKyEmail.Location = New System.Drawing.Point(16, 82)
        Me.lblChuKyEmail.Name = "lblChuKyEmail"
        Me.lblChuKyEmail.Size = New System.Drawing.Size(222, 13)
        Me.lblChuKyEmail.TabIndex = 3
        Me.lblChuKyEmail.Text = "* Thiết lập chữ ký khi gửi Email cá nhân"
        '
        'frmDoiMatKhau
        '
        Me.AcceptButton = Me.SimpleButton1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.SimpleButton2
        Me.ClientSize = New System.Drawing.Size(328, 400)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.tbTaiKhoan)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDoiMatKhau"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Đổi mật khảu"
        Me.TopMost = True
        CType(Me.tbTaiKhoan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMatKhauCu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMatKhauMoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.GroupControl2.PerformLayout()
        CType(Me.tbMKLuongThuongCu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMKLuongThuongMoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.GroupControl3.PerformLayout()
        CType(Me.txtDiaChiEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMatKhauEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbTaiKhoan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMatKhauCu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMatKhauMoi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMKLuongThuongCu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMKLuongThuongMoi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnHienThiEmail As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDiaChiEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtMatKhauEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblChuKyEmail As DevExpress.XtraEditors.LabelControl
End Class
