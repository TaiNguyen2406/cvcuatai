<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNMuonVT
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
        Me.btTimVatTu = New DevExpress.XtraEditors.SimpleButton()
        Me.cbNguoiMuon = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.cbTrangThai = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.cbNguoiMuon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTrangThai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btTimVatTu
        '
        Me.btTimVatTu.Location = New System.Drawing.Point(59, 26)
        Me.btTimVatTu.Name = "btTimVatTu"
        Me.btTimVatTu.Size = New System.Drawing.Size(75, 23)
        Me.btTimVatTu.TabIndex = 0
        Me.btTimVatTu.Text = "SimpleButton1"
        '
        'cbNguoiMuon
        '
        Me.cbNguoiMuon.Location = New System.Drawing.Point(59, 55)
        Me.cbNguoiMuon.Name = "cbNguoiMuon"
        Me.cbNguoiMuon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbNguoiMuon.Size = New System.Drawing.Size(100, 20)
        Me.cbNguoiMuon.TabIndex = 1
        '
        'cbTrangThai
        '
        Me.cbTrangThai.Location = New System.Drawing.Point(59, 81)
        Me.cbTrangThai.Name = "cbTrangThai"
        Me.cbTrangThai.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbTrangThai.Size = New System.Drawing.Size(100, 20)
        Me.cbTrangThai.TabIndex = 2
        '
        'frmCNMuonVT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.cbTrangThai)
        Me.Controls.Add(Me.cbNguoiMuon)
        Me.Controls.Add(Me.btTimVatTu)
        Me.Name = "frmCNMuonVT"
        Me.Text = "frmCNMuonVT"
        CType(Me.cbNguoiMuon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTrangThai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btTimVatTu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbNguoiMuon As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents cbTrangThai As DevExpress.XtraEditors.ComboBoxEdit
End Class
