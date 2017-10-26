<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTimKiemNhapXuatKho
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
        Me.txtTuNgay = New DevExpress.XtraEditors.TextEdit()
        Me.txtDenNgay = New DevExpress.XtraEditors.TextEdit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTuNgay
        '
        Me.txtTuNgay.Location = New System.Drawing.Point(45, 91)
        Me.txtTuNgay.Name = "txtTuNgay"
        Me.txtTuNgay.Size = New System.Drawing.Size(100, 20)
        Me.txtTuNgay.TabIndex = 0
        '
        'txtDenNgay
        '
        Me.txtDenNgay.Location = New System.Drawing.Point(45, 117)
        Me.txtDenNgay.Name = "txtDenNgay"
        Me.txtDenNgay.Size = New System.Drawing.Size(100, 20)
        Me.txtDenNgay.TabIndex = 1
        '
        'frmTimKiemNhapXuatKho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.txtDenNgay)
        Me.Controls.Add(Me.txtTuNgay)
        Me.Name = "frmTimKiemNhapXuatKho"
        Me.Text = "frmTimKiemNhapXuatKho"
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTuNgay As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDenNgay As DevExpress.XtraEditors.TextEdit
End Class
