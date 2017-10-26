<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNChi2
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
        Me.btnHoaDon = New DevExpress.XtraEditors.SimpleButton()
        Me.tbSoPhieu = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.tbSoPhieu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnHoaDon
        '
        Me.btnHoaDon.Location = New System.Drawing.Point(155, 102)
        Me.btnHoaDon.Name = "btnHoaDon"
        Me.btnHoaDon.Size = New System.Drawing.Size(75, 23)
        Me.btnHoaDon.TabIndex = 0
        Me.btnHoaDon.Text = "SimpleButton1"
        '
        'tbSoPhieu
        '
        Me.tbSoPhieu.Location = New System.Drawing.Point(51, 139)
        Me.tbSoPhieu.Name = "tbSoPhieu"
        Me.tbSoPhieu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbSoPhieu.Size = New System.Drawing.Size(100, 20)
        Me.tbSoPhieu.TabIndex = 1
        '
        'frmCNChi2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.tbSoPhieu)
        Me.Controls.Add(Me.btnHoaDon)
        Me.Name = "frmCNChi2"
        Me.Text = "frmCNChi2"
        CType(Me.tbSoPhieu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnHoaDon As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbSoPhieu As DevExpress.XtraEditors.LookUpEdit
End Class
