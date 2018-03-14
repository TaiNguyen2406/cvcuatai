<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhanxulyBaotri
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
        Me.txtNguoinhanxuly = New DevExpress.XtraEditors.TextEdit()
        Me.txtNgaynhanxuly = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txtNguoinhanxuly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaynhanxuly.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaynhanxuly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtNguoinhanxuly
        '
        Me.txtNguoinhanxuly.Location = New System.Drawing.Point(402, 15)
        Me.txtNguoinhanxuly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNguoinhanxuly.Name = "txtNguoinhanxuly"
        Me.txtNguoinhanxuly.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNguoinhanxuly.Properties.Appearance.Options.UseBackColor = True
        Me.txtNguoinhanxuly.Properties.ReadOnly = True
        Me.txtNguoinhanxuly.Size = New System.Drawing.Size(240, 22)
        Me.txtNguoinhanxuly.TabIndex = 9
        '
        'txtNgaynhanxuly
        '
        Me.txtNgaynhanxuly.EditValue = Nothing
        Me.txtNgaynhanxuly.Location = New System.Drawing.Point(121, 15)
        Me.txtNgaynhanxuly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaynhanxuly.Name = "txtNgaynhanxuly"
        Me.txtNgaynhanxuly.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNgaynhanxuly.Properties.Appearance.Options.UseBackColor = True
        Me.txtNgaynhanxuly.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaynhanxuly.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaynhanxuly.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaynhanxuly.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaynhanxuly.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaynhanxuly.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgaynhanxuly.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgaynhanxuly.Properties.ReadOnly = True
        Me.txtNgaynhanxuly.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaynhanxuly.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaynhanxuly.TabIndex = 8
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(286, 18)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(106, 17)
        Me.LabelControl2.TabIndex = 7
        Me.LabelControl2.Text = "Người nhận xử lý"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(13, 18)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(102, 17)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Ngày nhận xử lý"
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(122, 53)
        Me.txtGhichu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGhichu.Properties.Appearance.Options.UseFont = True
        Me.txtGhichu.Properties.MaxLength = 100
        Me.txtGhichu.Size = New System.Drawing.Size(520, 102)
        Me.txtGhichu.TabIndex = 1
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(13, 91)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(46, 17)
        Me.LabelControl4.TabIndex = 12
        Me.LabelControl4.Text = "Ghi chú"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(538, 165)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 3
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(402, 165)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(122, 41)
        Me.btLuuLai.TabIndex = 2
        Me.btLuuLai.Text = "Lưu lại"
        '
        'frmNhanxulyBaotri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(651, 214)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtNguoinhanxuly)
        Me.Controls.Add(Me.txtNgaynhanxuly)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNhanxulyBaotri"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nhận xử lý yêu cầu bảo trì, nâng cấp"
        CType(Me.txtNguoinhanxuly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaynhanxuly.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaynhanxuly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtNguoinhanxuly As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNgaynhanxuly As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
End Class
