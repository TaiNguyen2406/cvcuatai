<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNPhanHoi
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mmoPhanHoi = New DevExpress.XtraEditors.MemoEdit()
        Me.btnLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSoYC = New DevExpress.XtraEditors.TextEdit()
        CType(Me.mmoPhanHoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSoYC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mmoPhanHoi
        '
        Me.mmoPhanHoi.Location = New System.Drawing.Point(13, 37)
        Me.mmoPhanHoi.Name = "mmoPhanHoi"
        Me.mmoPhanHoi.Size = New System.Drawing.Size(393, 96)
        Me.mmoPhanHoi.TabIndex = 0
        '
        'btnLuu
        '
        Me.btnLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLuu.Appearance.Options.UseFont = True
        Me.btnLuu.Location = New System.Drawing.Point(246, 142)
        Me.btnLuu.Name = "btnLuu"
        Me.btnLuu.Size = New System.Drawing.Size(70, 28)
        Me.btnLuu.TabIndex = 1
        Me.btnLuu.Text = "Lưu"
        '
        'btnDong
        '
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Location = New System.Drawing.Point(322, 142)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(66, 28)
        Me.btnDong.TabIndex = 2
        Me.btnDong.Text = "Đóng"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Số yêu cầu"
        '
        'txtSoYC
        '
        Me.txtSoYC.Enabled = False
        Me.txtSoYC.Location = New System.Drawing.Point(80, 9)
        Me.txtSoYC.Name = "txtSoYC"
        Me.txtSoYC.Size = New System.Drawing.Size(125, 20)
        Me.txtSoYC.TabIndex = 4
        '
        'frmCNPhanHoi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(418, 186)
        Me.Controls.Add(Me.txtSoYC)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btnDong)
        Me.Controls.Add(Me.btnLuu)
        Me.Controls.Add(Me.mmoPhanHoi)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCNPhanHoi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Phản hồi yêu cầu"
        CType(Me.mmoPhanHoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSoYC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mmoPhanHoi As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSoYC As DevExpress.XtraEditors.TextEdit
End Class
