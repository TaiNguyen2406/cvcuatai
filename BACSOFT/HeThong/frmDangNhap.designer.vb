<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDangNhap
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
        Me.components = New System.ComponentModel.Container()
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDangNhap))
        Me.g1 = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.rdLocal = New DevExpress.XtraEditors.CheckEdit()
        Me.rdLan = New DevExpress.XtraEditors.CheckEdit()
        Me.rdInternet = New DevExpress.XtraEditors.CheckEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.tbCSDL = New DevExpress.XtraEditors.TextEdit()
        Me.tbMayChu = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.g2 = New DevExpress.XtraEditors.GroupControl()
        Me.chkNhoMK = New DevExpress.XtraEditors.CheckEdit()
        Me.tbMatKhau = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTaiKhoan = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDangNhap = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCauHinh = New DevExpress.XtraEditors.SimpleButton()
        Me.DefaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
        Me.bgW = New System.ComponentModel.BackgroundWorker()
        Me.prc = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.chkRunStartup = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.g1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.g1.SuspendLayout()
        CType(Me.rdLocal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdLan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdInternet.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbCSDL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMayChu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.g2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.g2.SuspendLayout()
        CType(Me.chkNhoMK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMatKhau.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTaiKhoan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRunStartup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'g1
        '
        Me.g1.Controls.Add(Me.LabelControl5)
        Me.g1.Controls.Add(Me.rdLocal)
        Me.g1.Controls.Add(Me.rdLan)
        Me.g1.Controls.Add(Me.rdInternet)
        Me.g1.Controls.Add(Me.tbCSDL)
        Me.g1.Controls.Add(Me.tbMayChu)
        Me.g1.Controls.Add(Me.LabelControl2)
        Me.g1.Controls.Add(Me.LabelControl1)
        Me.g1.Location = New System.Drawing.Point(13, 13)
        Me.g1.Margin = New System.Windows.Forms.Padding(4)
        Me.g1.Name = "g1"
        Me.g1.Size = New System.Drawing.Size(324, 118)
        Me.g1.TabIndex = 0
        Me.g1.Text = "Thông tin máy chủ"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(7, 92)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(115, 14)
        Me.LabelControl5.TabIndex = 3
        Me.LabelControl5.Text = "Phương thức kết nối"
        '
        'rdLocal
        '
        Me.rdLocal.Location = New System.Drawing.Point(261, 89)
        Me.rdLocal.Name = "rdLocal"
        Me.rdLocal.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.rdLocal.Properties.Appearance.Options.UseFont = True
        Me.rdLocal.Properties.Caption = "Local"
        Me.rdLocal.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdLocal.Properties.RadioGroupIndex = 0
        Me.rdLocal.Size = New System.Drawing.Size(50, 19)
        Me.rdLocal.TabIndex = 2
        Me.rdLocal.TabStop = False
        '
        'rdLan
        '
        Me.rdLan.EditValue = True
        Me.rdLan.Location = New System.Drawing.Point(205, 89)
        Me.rdLan.Name = "rdLan"
        Me.rdLan.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.rdLan.Properties.Appearance.Options.UseFont = True
        Me.rdLan.Properties.Caption = "Lan"
        Me.rdLan.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdLan.Properties.RadioGroupIndex = 0
        Me.rdLan.Size = New System.Drawing.Size(50, 19)
        Me.rdLan.TabIndex = 2
        '
        'rdInternet
        '
        Me.rdInternet.Location = New System.Drawing.Point(128, 89)
        Me.rdInternet.MenuManager = Me.BarManager1
        Me.rdInternet.Name = "rdInternet"
        Me.rdInternet.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.rdInternet.Properties.Appearance.Options.UseFont = True
        Me.rdInternet.Properties.Caption = "Internet"
        Me.rdInternet.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdInternet.Properties.RadioGroupIndex = 0
        Me.rdInternet.Size = New System.Drawing.Size(77, 19)
        Me.rdInternet.TabIndex = 2
        Me.rdInternet.TabStop = False
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(350, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 334)
        Me.barDockControlBottom.Size = New System.Drawing.Size(350, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 334)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(350, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 334)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "BarButtonItem2"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'tbCSDL
        '
        Me.tbCSDL.EnterMoveNextControl = True
        Me.tbCSDL.Location = New System.Drawing.Point(107, 59)
        Me.tbCSDL.Margin = New System.Windows.Forms.Padding(4)
        Me.tbCSDL.Name = "tbCSDL"
        Me.tbCSDL.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.tbCSDL.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.tbCSDL.Properties.Appearance.Options.UseFont = True
        Me.tbCSDL.Properties.Appearance.Options.UseForeColor = True
        Me.tbCSDL.Size = New System.Drawing.Size(207, 21)
        Me.tbCSDL.TabIndex = 1
        '
        'tbMayChu
        '
        Me.tbMayChu.EnterMoveNextControl = True
        Me.tbMayChu.Location = New System.Drawing.Point(107, 31)
        Me.tbMayChu.Margin = New System.Windows.Forms.Padding(4)
        Me.tbMayChu.Name = "tbMayChu"
        Me.tbMayChu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.tbMayChu.Properties.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.tbMayChu.Properties.Appearance.Options.UseFont = True
        Me.tbMayChu.Properties.Appearance.Options.UseForeColor = True
        Me.tbMayChu.Size = New System.Drawing.Size(207, 21)
        Me.tbMayChu.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.LabelControl2.Appearance.Image = Global.BACSOFT.My.Resources.Resources.database_24
        Me.LabelControl2.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl2.Location = New System.Drawing.Point(6, 55)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(61, 28)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "CSDL"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.LabelControl1.Appearance.Image = Global.BACSOFT.My.Resources.Resources.computer_24
        Me.LabelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl1.Location = New System.Drawing.Point(7, 27)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(80, 28)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Máy chủ"
        '
        'g2
        '
        Me.g2.Controls.Add(Me.chkNhoMK)
        Me.g2.Controls.Add(Me.tbMatKhau)
        Me.g2.Controls.Add(Me.LabelControl3)
        Me.g2.Controls.Add(Me.tbTaiKhoan)
        Me.g2.Controls.Add(Me.LabelControl4)
        Me.g2.Location = New System.Drawing.Point(13, 139)
        Me.g2.Margin = New System.Windows.Forms.Padding(4)
        Me.g2.Name = "g2"
        Me.g2.Size = New System.Drawing.Size(324, 110)
        Me.g2.TabIndex = 3
        Me.g2.Text = "Tài khoản"
        '
        'chkNhoMK
        '
        Me.chkNhoMK.Location = New System.Drawing.Point(105, 87)
        Me.chkNhoMK.Name = "chkNhoMK"
        Me.chkNhoMK.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.chkNhoMK.Properties.Appearance.Options.UseFont = True
        Me.chkNhoMK.Properties.Caption = "Ghi nhớ mật khẩu"
        Me.chkNhoMK.Size = New System.Drawing.Size(154, 19)
        Me.chkNhoMK.TabIndex = 2
        '
        'tbMatKhau
        '
        Me.tbMatKhau.EnterMoveNextControl = True
        Me.tbMatKhau.Location = New System.Drawing.Point(107, 59)
        Me.tbMatKhau.Margin = New System.Windows.Forms.Padding(4)
        Me.tbMatKhau.Name = "tbMatKhau"
        Me.tbMatKhau.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.tbMatKhau.Properties.Appearance.Options.UseFont = True
        Me.tbMatKhau.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMatKhau.Size = New System.Drawing.Size(207, 21)
        Me.tbMatKhau.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.LabelControl3.Appearance.Image = Global.BACSOFT.My.Resources.Resources.User_24
        Me.LabelControl3.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl3.Location = New System.Drawing.Point(7, 26)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(88, 28)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Tài khoản"
        '
        'tbTaiKhoan
        '
        Me.tbTaiKhoan.EnterMoveNextControl = True
        Me.tbTaiKhoan.Location = New System.Drawing.Point(107, 30)
        Me.tbTaiKhoan.Margin = New System.Windows.Forms.Padding(4)
        Me.tbTaiKhoan.Name = "tbTaiKhoan"
        Me.tbTaiKhoan.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.tbTaiKhoan.Properties.Appearance.ForeColor = System.Drawing.Color.MidnightBlue
        Me.tbTaiKhoan.Properties.Appearance.Options.UseFont = True
        Me.tbTaiKhoan.Properties.Appearance.Options.UseForeColor = True
        Me.tbTaiKhoan.Size = New System.Drawing.Size(207, 21)
        Me.tbTaiKhoan.TabIndex = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.LabelControl4.Appearance.Image = Global.BACSOFT.My.Resources.Resources.Key_24
        Me.LabelControl4.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl4.Location = New System.Drawing.Point(7, 55)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(87, 28)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Mật khẩu"
        '
        'btnDangNhap
        '
        Me.btnDangNhap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDangNhap.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDangNhap.Appearance.Options.UseFont = True
        Me.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDangNhap.Image = Global.BACSOFT.My.Resources.Resources.login_24
        Me.btnDangNhap.Location = New System.Drawing.Point(121, 285)
        Me.btnDangNhap.Margin = New System.Windows.Forms.Padding(5)
        Me.btnDangNhap.Name = "btnDangNhap"
        Me.btnDangNhap.Size = New System.Drawing.Size(116, 35)
        Me.btnDangNhap.TabIndex = 0
        Me.btnDangNhap.Text = "Đăng nhập"
        '
        'btnDong
        '
        Me.btnDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btnDong.Location = New System.Drawing.Point(247, 285)
        Me.btnDong.Margin = New System.Windows.Forms.Padding(5)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(90, 35)
        Me.btnDong.TabIndex = 2
        Me.btnDong.Text = "Đóng"
        '
        'btnCauHinh
        '
        Me.btnCauHinh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCauHinh.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCauHinh.Appearance.Options.UseFont = True
        Me.btnCauHinh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCauHinh.Image = Global.BACSOFT.My.Resources.Resources.Config_24
        Me.btnCauHinh.Location = New System.Drawing.Point(13, 285)
        Me.btnCauHinh.Margin = New System.Windows.Forms.Padding(5)
        Me.btnCauHinh.Name = "btnCauHinh"
        Me.btnCauHinh.Size = New System.Drawing.Size(34, 35)
        Me.btnCauHinh.TabIndex = 1
        '
        'DefaultLookAndFeel1
        '
        Me.DefaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue"
        '
        'bgW
        '
        Me.bgW.WorkerSupportsCancellation = True
        '
        'prc
        '
        Me.prc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.prc.EditValue = "Đang kết nối tới máy chủ, click  đây để đóng kết nối ..."
        Me.prc.Location = New System.Drawing.Point(37, 124)
        Me.prc.Name = "prc"
        Me.prc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.prc.Properties.MarqueeAnimationSpeed = 50
        Me.prc.Properties.ShowTitle = True
        Me.prc.Size = New System.Drawing.Size(286, 18)
        Me.prc.TabIndex = 3
        Me.prc.Visible = False
        '
        'chkRunStartup
        '
        Me.chkRunStartup.Location = New System.Drawing.Point(18, 256)
        Me.chkRunStartup.MenuManager = Me.BarManager1
        Me.chkRunStartup.Name = "chkRunStartup"
        Me.chkRunStartup.Properties.Caption = "Chạy ứng dụng khi khởi động máy tính"
        Me.chkRunStartup.Size = New System.Drawing.Size(219, 19)
        Me.chkRunStartup.TabIndex = 8
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Image = Global.BACSOFT.My.Resources.Resources.noti_Warning_18
        Me.LabelControl6.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl6.Location = New System.Drawing.Point(243, 255)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(23, 22)
        ToolTipTitleItem1.Text = "Click vào đây khi không đăng nhập được chương trình"
        ToolTipItem1.LeftIndent = 6
        ToolTipItem1.Text = "- Sửa lỗi khi báo không thể kết nối khi đăng nhập với tài khoản baoan01"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.LabelControl6.SuperTip = SuperToolTip1
        Me.LabelControl6.TabIndex = 9
        '
        'frmDangNhap
        '
        Me.AcceptButton = Me.btnDangNhap
        Me.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnDong
        Me.ClientSize = New System.Drawing.Size(350, 334)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.chkRunStartup)
        Me.Controls.Add(Me.prc)
        Me.Controls.Add(Me.btnDong)
        Me.Controls.Add(Me.btnCauHinh)
        Me.Controls.Add(Me.btnDangNhap)
        Me.Controls.Add(Me.g2)
        Me.Controls.Add(Me.g1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDangNhap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Đăng nhập hệ thống"
        CType(Me.g1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.g1.ResumeLayout(False)
        Me.g1.PerformLayout()
        CType(Me.rdLocal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdLan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdInternet.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbCSDL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMayChu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.g2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.g2.ResumeLayout(False)
        Me.g2.PerformLayout()
        CType(Me.chkNhoMK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMatKhau.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTaiKhoan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRunStartup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents g1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tbCSDL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tbMayChu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents g2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMatKhau As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tbTaiKhoan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnDangNhap As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCauHinh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkNhoMK As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents rdLocal As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdLan As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdInternet As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents DefaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
    Friend WithEvents bgW As System.ComponentModel.BackgroundWorker
    Friend WithEvents prc As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents chkRunStartup As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
End Class
