<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCauHinhEmailGui
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
        Me.tbEmail = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbMatKhau = New DevExpress.XtraEditors.TextEdit()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        Me.btHienMK = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMatKhau.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbEmail
        '
        Me.tbEmail.Location = New System.Drawing.Point(77, 12)
        Me.tbEmail.Name = "tbEmail"
        Me.tbEmail.Size = New System.Drawing.Size(228, 20)
        Me.tbEmail.TabIndex = 4
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 15)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(24, 13)
        Me.LabelControl3.TabIndex = 3
        Me.LabelControl3.Text = "Email"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 41)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Mật khẩu"
        '
        'tbMatKhau
        '
        Me.tbMatKhau.Location = New System.Drawing.Point(77, 38)
        Me.tbMatKhau.Name = "tbMatKhau"
        Me.tbMatKhau.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMatKhau.Size = New System.Drawing.Size(228, 20)
        Me.tbMatKhau.TabIndex = 4
        '
        'btXacNhan
        '
        Me.btXacNhan.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btXacNhan.Appearance.Options.UseFont = True
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.btXacNhan.Location = New System.Drawing.Point(219, 64)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(86, 23)
        Me.btXacNhan.TabIndex = 5
        Me.btXacNhan.Text = "Xác nhận"
        '
        'btHienMK
        '
        Me.btHienMK.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btHienMK.Appearance.Options.UseFont = True
        Me.btHienMK.Image = Global.BACSOFT.My.Resources.Resources.Preview_18
        Me.btHienMK.Location = New System.Drawing.Point(311, 35)
        Me.btHienMK.Name = "btHienMK"
        Me.btHienMK.Size = New System.Drawing.Size(28, 23)
        Me.btHienMK.TabIndex = 6
        Me.btHienMK.ToolTip = "Hiển thị mật khẩu"
        '
        'frmCauHinhEmailGui
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 101)
        Me.Controls.Add(Me.btHienMK)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.tbMatKhau)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tbEmail)
        Me.Controls.Add(Me.LabelControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCauHinhEmailGui"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cấu hình Email"
        CType(Me.tbEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMatKhau.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMatKhau As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btHienMK As DevExpress.XtraEditors.SimpleButton
End Class
