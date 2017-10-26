<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThayIDKH
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
        Me.tbIDThayThe = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbIDThayThe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbIDThayThe
        '
        Me.tbIDThayThe.Location = New System.Drawing.Point(84, 10)
        Me.tbIDThayThe.Name = "tbIDThayThe"
        Me.tbIDThayThe.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbIDThayThe.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbIDThayThe.Size = New System.Drawing.Size(100, 20)
        Me.tbIDThayThe.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Thay bằng ID"
        '
        'btXacNhan
        '
        Me.btXacNhan.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btXacNhan.Appearance.Options.UseFont = True
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.btXacNhan.Location = New System.Drawing.Point(190, 7)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(86, 23)
        Me.btXacNhan.TabIndex = 2
        Me.btXacNhan.Text = "Xác nhận"
        '
        'frmDoiID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(290, 41)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tbIDThayThe)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDoiID"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Đổi ID"
        CType(Me.tbIDThayThe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbIDThayThe As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
End Class
