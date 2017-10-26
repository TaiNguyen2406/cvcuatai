<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDenNgay
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
        Me.tbNgay = New DevExpress.XtraEditors.DateEdit()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Image = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.LabelControl1.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LabelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(108, 22)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Chuyển đến ngày"
        '
        'tbNgay
        '
        Me.tbNgay.EditValue = Nothing
        Me.tbNgay.Location = New System.Drawing.Point(127, 13)
        Me.tbNgay.Name = "tbNgay"
        Me.tbNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbNgay.Size = New System.Drawing.Size(100, 20)
        Me.tbNgay.TabIndex = 1
        '
        'btXacNhan
        '
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.btXacNhan.Location = New System.Drawing.Point(233, 10)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(93, 23)
        Me.btXacNhan.TabIndex = 2
        Me.btXacNhan.Text = "Xác nhận"
        '
        'frmDenNgay
        '
        Me.AcceptButton = Me.btXacNhan
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 45)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.tbNgay)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmDenNgay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chuyển đến ngày"
        CType(Me.tbNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
End Class
