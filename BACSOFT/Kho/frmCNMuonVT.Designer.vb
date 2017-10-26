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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThoiGianMuon = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cbNguoiMuon = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.gdvVatTu = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTinhTrangMuon = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTinhTrangTra = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThoiGianTra = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.tbModel = New DevExpress.XtraEditors.TextEdit()
        Me.btTimVatTu = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemMoi = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.cbTrangThai = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.tbSoLuong = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.tbGhiChu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.tbSoChaoGia = New DevExpress.XtraEditors.ButtonEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.tbGhiChuKD = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.tbThoiGianMuon.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianMuon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNguoiMuon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvVatTu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTinhTrangMuon.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTinhTrangTra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianTra.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianTra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTrangThai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSoLuong.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGhiChu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSoChaoGia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGhiChuKD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 16)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(73, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Thời gian mượn"
        '
        'tbThoiGianMuon
        '
        Me.tbThoiGianMuon.EditValue = Nothing
        Me.tbThoiGianMuon.Location = New System.Drawing.Point(103, 13)
        Me.tbThoiGianMuon.Name = "tbThoiGianMuon"
        Me.tbThoiGianMuon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbThoiGianMuon.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianMuon.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianMuon.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianMuon.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianMuon.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianMuon.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGianMuon.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGianMuon.Size = New System.Drawing.Size(140, 20)
        Me.tbThoiGianMuon.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 42)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Người mượn"
        '
        'cbNguoiMuon
        '
        Me.cbNguoiMuon.Location = New System.Drawing.Point(103, 39)
        Me.cbNguoiMuon.Name = "cbNguoiMuon"
        Me.cbNguoiMuon.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbNguoiMuon.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên")})
        Me.cbNguoiMuon.Properties.DisplayMember = "Ten"
        Me.cbNguoiMuon.Properties.DropDownItemHeight = 22
        Me.cbNguoiMuon.Properties.NullText = ""
        Me.cbNguoiMuon.Properties.ShowHeader = False
        Me.cbNguoiMuon.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbNguoiMuon.Properties.ValueMember = "ID"
        Me.cbNguoiMuon.Size = New System.Drawing.Size(189, 20)
        Me.cbNguoiMuon.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 120)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Vật tư"
        '
        'gdvVatTu
        '
        Me.gdvVatTu.EditValue = ""
        Me.gdvVatTu.Location = New System.Drawing.Point(103, 117)
        Me.gdvVatTu.Name = "gdvVatTu"
        Me.gdvVatTu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.gdvVatTu.Properties.DisplayMember = "Model"
        Me.gdvVatTu.Properties.NullText = "[Chọn vật tư]"
        Me.gdvVatTu.Properties.PopupFormSize = New System.Drawing.Size(400, 350)
        Me.gdvVatTu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.gdvVatTu.Properties.ValueMember = "ID"
        Me.gdvVatTu.Properties.View = Me.GridLookUpEdit1View
        Me.gdvVatTu.Size = New System.Drawing.Size(357, 20)
        Me.gdvVatTu.TabIndex = 6
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.RowAutoHeight = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(12, 174)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(79, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Tình trạng mượn"
        '
        'tbTinhTrangMuon
        '
        Me.tbTinhTrangMuon.Location = New System.Drawing.Point(103, 171)
        Me.tbTinhTrangMuon.Name = "tbTinhTrangMuon"
        Me.tbTinhTrangMuon.Size = New System.Drawing.Size(357, 58)
        Me.tbTinhTrangMuon.TabIndex = 8
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(12, 238)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "Tình trạng trả"
        '
        'tbTinhTrangTra
        '
        Me.tbTinhTrangTra.Location = New System.Drawing.Point(103, 235)
        Me.tbTinhTrangTra.Name = "tbTinhTrangTra"
        Me.tbTinhTrangTra.Size = New System.Drawing.Size(357, 58)
        Me.tbTinhTrangTra.TabIndex = 9
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(254, 16)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl6.TabIndex = 0
        Me.LabelControl6.Text = "Thời gian trả"
        '
        'tbThoiGianTra
        '
        Me.tbThoiGianTra.EditValue = Nothing
        Me.tbThoiGianTra.Location = New System.Drawing.Point(320, 13)
        Me.tbThoiGianTra.Name = "tbThoiGianTra"
        Me.tbThoiGianTra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbThoiGianTra.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianTra.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianTra.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianTra.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianTra.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianTra.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGianTra.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGianTra.Size = New System.Drawing.Size(140, 20)
        Me.tbThoiGianTra.TabIndex = 1
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(12, 94)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl7.TabIndex = 0
        Me.LabelControl7.Text = "Model"
        '
        'tbModel
        '
        Me.tbModel.Location = New System.Drawing.Point(103, 91)
        Me.tbModel.Name = "tbModel"
        Me.tbModel.Size = New System.Drawing.Size(189, 20)
        Me.tbModel.TabIndex = 4
        '
        'btTimVatTu
        '
        Me.btTimVatTu.Location = New System.Drawing.Point(298, 88)
        Me.btTimVatTu.Name = "btTimVatTu"
        Me.btTimVatTu.Size = New System.Drawing.Size(75, 23)
        Me.btTimVatTu.TabIndex = 5
        Me.btTimVatTu.Text = "Tìm vật tư"
        '
        'btThemMoi
        '
        Me.btThemMoi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btThemMoi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btThemMoi.Appearance.Options.UseFont = True
        Me.btThemMoi.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThemMoi.Location = New System.Drawing.Point(198, 465)
        Me.btThemMoi.Name = "btThemMoi"
        Me.btThemMoi.Size = New System.Drawing.Size(94, 23)
        Me.btThemMoi.TabIndex = 13
        Me.btThemMoi.Text = "Thêm mới"
        '
        'btLuuLai
        '
        Me.btLuuLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(298, 465)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(75, 23)
        Me.btLuuLai.TabIndex = 12
        Me.btLuuLai.Text = "Lưu lại"
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(382, 465)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 23)
        Me.btDong.TabIndex = 14
        Me.btDong.Text = "Đóng"
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(12, 303)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl8.TabIndex = 0
        Me.LabelControl8.Text = "Tình trạng trả"
        '
        'cbTrangThai
        '
        Me.cbTrangThai.Location = New System.Drawing.Point(103, 300)
        Me.cbTrangThai.Name = "cbTrangThai"
        Me.cbTrangThai.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbTrangThai.Properties.DropDownItemHeight = 22
        Me.cbTrangThai.Properties.Items.AddRange(New Object() {"Đang mượn", "Đã trả", "Thất lạc"})
        Me.cbTrangThai.Size = New System.Drawing.Size(140, 20)
        Me.cbTrangThai.TabIndex = 10
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(12, 147)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl9.TabIndex = 0
        Me.LabelControl9.Text = "Số lượng"
        '
        'tbSoLuong
        '
        Me.tbSoLuong.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.tbSoLuong.Location = New System.Drawing.Point(103, 144)
        Me.tbSoLuong.Name = "tbSoLuong"
        Me.tbSoLuong.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbSoLuong.Size = New System.Drawing.Size(100, 20)
        Me.tbSoLuong.TabIndex = 7
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(12, 337)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl10.TabIndex = 0
        Me.LabelControl10.Text = "Ghi chú"
        '
        'tbGhiChu
        '
        Me.tbGhiChu.Location = New System.Drawing.Point(103, 326)
        Me.tbGhiChu.Name = "tbGhiChu"
        Me.tbGhiChu.Size = New System.Drawing.Size(357, 58)
        Me.tbGhiChu.TabIndex = 11
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(12, 70)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl11.TabIndex = 0
        Me.LabelControl11.Text = "Số CG"
        '
        'tbSoChaoGia
        '
        Me.tbSoChaoGia.Location = New System.Drawing.Point(103, 65)
        Me.tbSoChaoGia.Name = "tbSoChaoGia"
        Me.tbSoChaoGia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.tbSoChaoGia.Size = New System.Drawing.Size(100, 20)
        Me.tbSoChaoGia.TabIndex = 3
        '
        'LabelControl12
        '
        Me.LabelControl12.Location = New System.Drawing.Point(12, 403)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl12.TabIndex = 0
        Me.LabelControl12.Text = "Ghi chú KD"
        '
        'tbGhiChuKD
        '
        Me.tbGhiChuKD.Location = New System.Drawing.Point(103, 392)
        Me.tbGhiChuKD.Name = "tbGhiChuKD"
        Me.tbGhiChuKD.Size = New System.Drawing.Size(357, 58)
        Me.tbGhiChuKD.TabIndex = 11
        '
        'frmCNMuonVT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 500)
        Me.Controls.Add(Me.tbSoChaoGia)
        Me.Controls.Add(Me.tbSoLuong)
        Me.Controls.Add(Me.cbTrangThai)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.btThemMoi)
        Me.Controls.Add(Me.btTimVatTu)
        Me.Controls.Add(Me.tbModel)
        Me.Controls.Add(Me.tbGhiChuKD)
        Me.Controls.Add(Me.tbGhiChu)
        Me.Controls.Add(Me.tbTinhTrangTra)
        Me.Controls.Add(Me.tbTinhTrangMuon)
        Me.Controls.Add(Me.gdvVatTu)
        Me.Controls.Add(Me.cbNguoiMuon)
        Me.Controls.Add(Me.tbThoiGianTra)
        Me.Controls.Add(Me.LabelControl12)
        Me.Controls.Add(Me.tbThoiGianMuon)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCNMuonVT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật thông tin mượn vật tư"
        CType(Me.tbThoiGianMuon.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianMuon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNguoiMuon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvVatTu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTinhTrangMuon.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTinhTrangTra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianTra.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianTra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTrangThai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSoLuong.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGhiChu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSoChaoGia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGhiChuKD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGianMuon As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbNguoiMuon As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gdvVatTu As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbTinhTrangMuon As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbTinhTrangTra As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGianTra As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btTimVatTu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemMoi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbTrangThai As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbSoLuong As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbGhiChu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbSoChaoGia As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbGhiChuKD As DevExpress.XtraEditors.MemoEdit
End Class
