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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTimKiemNhapXuatKho))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkLocTheoHD = New DevExpress.XtraEditors.CheckEdit()
        Me.txtNoiDung = New DevExpress.XtraEditors.TextEdit()
        Me.txtModel = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btTimKiem = New DevExpress.XtraEditors.SimpleButton()
        Me.txtDenNgay = New DevExpress.XtraEditors.DateEdit()
        Me.txtTuNgay = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkLocTheoHD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Red
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl1.CaptionImage = Global.BACSOFT.My.Resources.Resources.filter_18
        Me.GroupControl1.Controls.Add(Me.chkLocTheoHD)
        Me.GroupControl1.Controls.Add(Me.txtNoiDung)
        Me.GroupControl1.Controls.Add(Me.txtModel)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.btnDong)
        Me.GroupControl1.Controls.Add(Me.btTimKiem)
        Me.GroupControl1.Controls.Add(Me.txtDenNgay)
        Me.GroupControl1.Controls.Add(Me.txtTuNgay)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(422, 217)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Tiêu chí tìm kiếm"
        '
        'chkLocTheoHD
        '
        Me.chkLocTheoHD.Location = New System.Drawing.Point(79, 132)
        Me.chkLocTheoHD.Name = "chkLocTheoHD"
        Me.chkLocTheoHD.Properties.Caption = "Lọc những phiếu chưa có hóa đơn"
        Me.chkLocTheoHD.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.chkLocTheoHD.Properties.PictureChecked = CType(resources.GetObject("chkLocTheoHD.Properties.PictureChecked"), System.Drawing.Image)
        Me.chkLocTheoHD.Properties.PictureUnchecked = CType(resources.GetObject("chkLocTheoHD.Properties.PictureUnchecked"), System.Drawing.Image)
        Me.chkLocTheoHD.Size = New System.Drawing.Size(275, 20)
        Me.chkLocTheoHD.TabIndex = 4
        '
        'txtNoiDung
        '
        Me.txtNoiDung.Location = New System.Drawing.Point(81, 100)
        Me.txtNoiDung.Name = "txtNoiDung"
        Me.txtNoiDung.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNoiDung.Properties.Appearance.Options.UseBackColor = True
        Me.txtNoiDung.Size = New System.Drawing.Size(321, 20)
        Me.txtNoiDung.TabIndex = 3
        '
        'txtModel
        '
        Me.txtModel.Location = New System.Drawing.Point(81, 69)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtModel.Properties.Appearance.Options.UseBackColor = True
        Me.txtModel.Size = New System.Drawing.Size(321, 20)
        Me.txtModel.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(20, 104)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl3.TabIndex = 55
        Me.LabelControl3.Text = "Nội dung"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(20, 73)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl2.TabIndex = 54
        Me.LabelControl2.Text = "Model"
        '
        'btnDong
        '
        Me.btnDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btnDong.Location = New System.Drawing.Point(315, 169)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(82, 36)
        Me.btnDong.TabIndex = 6
        Me.btnDong.Text = "Đóng"
        '
        'btTimKiem
        '
        Me.btTimKiem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btTimKiem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btTimKiem.Appearance.Options.UseFont = True
        Me.btTimKiem.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btTimKiem.Image = Global.BACSOFT.My.Resources.Resources.Search_24
        Me.btTimKiem.Location = New System.Drawing.Point(215, 169)
        Me.btTimKiem.Name = "btTimKiem"
        Me.btTimKiem.Size = New System.Drawing.Size(94, 36)
        Me.btTimKiem.TabIndex = 5
        Me.btTimKiem.Text = "Tìm kiếm"
        '
        'txtDenNgay
        '
        Me.txtDenNgay.EditValue = Nothing
        Me.txtDenNgay.Location = New System.Drawing.Point(284, 36)
        Me.txtDenNgay.Name = "txtDenNgay"
        Me.txtDenNgay.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDenNgay.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtDenNgay.Properties.Appearance.Options.UseBackColor = True
        Me.txtDenNgay.Properties.Appearance.Options.UseFont = True
        Me.txtDenNgay.Properties.Appearance.Options.UseTextOptions = True
        Me.txtDenNgay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtDenNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.txtDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.txtDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.txtDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtDenNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtDenNgay.Size = New System.Drawing.Size(117, 20)
        Me.txtDenNgay.TabIndex = 1
        '
        'txtTuNgay
        '
        Me.txtTuNgay.EditValue = Nothing
        Me.txtTuNgay.Location = New System.Drawing.Point(81, 36)
        Me.txtTuNgay.Name = "txtTuNgay"
        Me.txtTuNgay.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTuNgay.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTuNgay.Properties.Appearance.Options.UseBackColor = True
        Me.txtTuNgay.Properties.Appearance.Options.UseFont = True
        Me.txtTuNgay.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTuNgay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtTuNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.txtTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.txtTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.txtTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtTuNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtTuNgay.Size = New System.Drawing.Size(117, 20)
        Me.txtTuNgay.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(219, 40)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 37
        Me.LabelControl1.Text = "Đến ngày"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(20, 40)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl7.TabIndex = 36
        Me.LabelControl7.Text = "Từ ngày"
        '
        'frmTimKiemNhapXuatKho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 217)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTimKiemNhapXuatKho"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmTimKiemNhapXuatKho"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkLocTheoHD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDenNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtTuNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btTimKiem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoiDung As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkLocTheoHD As DevExpress.XtraEditors.CheckEdit
End Class
