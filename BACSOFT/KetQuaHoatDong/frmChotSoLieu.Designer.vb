<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChotSoLieu
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
        Me.tbThang = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.chkCNGiaNhapTBTrongXK = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCNChiPhiXK = New DevExpress.XtraEditors.CheckEdit()
        Me.chkCNLoiNhuanXK = New DevExpress.XtraEditors.CheckEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbThang.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCNGiaNhapTBTrongXK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCNChiPhiXK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCNLoiNhuanXK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbThang
        '
        Me.tbThang.EditValue = Nothing
        Me.tbThang.Location = New System.Drawing.Point(48, 12)
        Me.tbThang.Name = "tbThang"
        Me.tbThang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.tbThang.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.tbThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThang.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.tbThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThang.Properties.Mask.EditMask = "MM/yyyy"
        Me.tbThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThang.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThang.Size = New System.Drawing.Size(92, 20)
        Me.tbThang.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Tháng"
        '
        'chkCNGiaNhapTBTrongXK
        '
        Me.chkCNGiaNhapTBTrongXK.EditValue = True
        Me.chkCNGiaNhapTBTrongXK.Location = New System.Drawing.Point(46, 38)
        Me.chkCNGiaNhapTBTrongXK.Name = "chkCNGiaNhapTBTrongXK"
        Me.chkCNGiaNhapTBTrongXK.Properties.Caption = "Giá nhập trung bình trong xuất kho"
        Me.chkCNGiaNhapTBTrongXK.Size = New System.Drawing.Size(203, 19)
        Me.chkCNGiaNhapTBTrongXK.TabIndex = 2
        '
        'chkCNChiPhiXK
        '
        Me.chkCNChiPhiXK.EditValue = True
        Me.chkCNChiPhiXK.Location = New System.Drawing.Point(46, 63)
        Me.chkCNChiPhiXK.Name = "chkCNChiPhiXK"
        Me.chkCNChiPhiXK.Properties.Caption = "Chi phí của xuất kho"
        Me.chkCNChiPhiXK.Size = New System.Drawing.Size(203, 19)
        Me.chkCNChiPhiXK.TabIndex = 2
        '
        'chkCNLoiNhuanXK
        '
        Me.chkCNLoiNhuanXK.EditValue = True
        Me.chkCNLoiNhuanXK.Location = New System.Drawing.Point(46, 88)
        Me.chkCNLoiNhuanXK.Name = "chkCNLoiNhuanXK"
        Me.chkCNLoiNhuanXK.Properties.Caption = "Lợi nhuận của xuất kho"
        Me.chkCNLoiNhuanXK.Size = New System.Drawing.Size(203, 19)
        Me.chkCNLoiNhuanXK.TabIndex = 2
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Image = Global.BACSOFT.My.Resources.Resources.Checked
        Me.SimpleButton1.Location = New System.Drawing.Point(191, 116)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(79, 23)
        Me.SimpleButton1.TabIndex = 3
        Me.SimpleButton1.Text = "Cập nhật"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton2.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.SimpleButton2.Location = New System.Drawing.Point(276, 116)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(62, 23)
        Me.SimpleButton2.TabIndex = 3
        Me.SimpleButton2.Text = "Đóng"
        '
        'frmChotSoLieu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 151)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.chkCNGiaNhapTBTrongXK)
        Me.Controls.Add(Me.chkCNLoiNhuanXK)
        Me.Controls.Add(Me.chkCNChiPhiXK)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tbThang)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmChotSoLieu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chốt số liệu"
        CType(Me.tbThang.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCNGiaNhapTBTrongXK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCNChiPhiXK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCNLoiNhuanXK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbThang As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkCNGiaNhapTBTrongXK As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCNChiPhiXK As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkCNLoiNhuanXK As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
End Class
