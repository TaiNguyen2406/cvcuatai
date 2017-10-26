<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXacNhanMatKhauEmail
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
        Me.tbMK = New DevExpress.XtraEditors.TextEdit()
        Me.btXemMK = New DevExpress.XtraEditors.SimpleButton()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbMK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbMK
        '
        Me.tbMK.Location = New System.Drawing.Point(13, 12)
        Me.tbMK.Name = "tbMK"
        Me.tbMK.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbMK.Size = New System.Drawing.Size(259, 20)
        Me.tbMK.TabIndex = 0
        '
        'btXemMK
        '
        Me.btXemMK.Image = Global.BACSOFT.My.Resources.Resources.Preview_18
        Me.btXemMK.Location = New System.Drawing.Point(278, 9)
        Me.btXemMK.Name = "btXemMK"
        Me.btXemMK.Size = New System.Drawing.Size(26, 23)
        Me.btXemMK.TabIndex = 1
        '
        'btXacNhan
        '
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.btXacNhan.Location = New System.Drawing.Point(118, 39)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(82, 23)
        Me.btXacNhan.TabIndex = 2
        Me.btXacNhan.Text = "Xác nhận"
        '
        'frmXacNhanMatKhauEmail
        '
        Me.AcceptButton = Me.btXacNhan
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(312, 70)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.btXemMK)
        Me.Controls.Add(Me.tbMK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmXacNhanMatKhauEmail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmXacNhanMatKhau"
        CType(Me.tbMK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbMK As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btXemMK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
End Class
