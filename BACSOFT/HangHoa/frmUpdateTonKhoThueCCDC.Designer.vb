<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateTonKhoThueCCDC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateTonKhoThueCCDC))
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.gdvVT = New DevExpress.XtraGrid.GridControl()
        Me.gdvDataVT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btnThemMoi = New DevExpress.XtraEditors.SimpleButton()
        Me.btGhiLai = New DevExpress.XtraEditors.SimpleButton()
        Me.txtSoKyConLai = New DevExpress.XtraEditors.SpinEdit()
        Me.txtSoKyPhanBo = New DevExpress.XtraEditors.SpinEdit()
        Me.txtSoLuong = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGiaTriConLai = New DevExpress.XtraEditors.SpinEdit()
        Me.txtGhiChu = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbPhongBan = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.gdvVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvDataVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSoKyConLai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSoKyPhanBo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSoLuong.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGiaTriConLai.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhiChu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPhongBan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GroupControl3.AppearanceCaption.ForeColor = System.Drawing.Color.Green
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.AppearanceCaption.Options.UseForeColor = True
        Me.GroupControl3.Controls.Add(Me.gdvVT)
        Me.GroupControl3.Location = New System.Drawing.Point(9, 9)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(751, 350)
        Me.GroupControl3.TabIndex = 5
        Me.GroupControl3.Text = "Danh sách công cụ dụng cụ"
        '
        'gdvVT
        '
        Me.gdvVT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvVT.Location = New System.Drawing.Point(2, 23)
        Me.gdvVT.MainView = Me.gdvDataVT
        Me.gdvVT.Name = "gdvVT"
        Me.gdvVT.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit3})
        Me.gdvVT.Size = New System.Drawing.Size(747, 325)
        Me.gdvVT.TabIndex = 1
        Me.gdvVT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvDataVT})
        '
        'gdvDataVT
        '
        Me.gdvDataVT.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gdvDataVT.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvDataVT.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Blue
        Me.gdvDataVT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvDataVT.Appearance.FocusedRow.Options.UseFont = True
        Me.gdvDataVT.Appearance.FocusedRow.Options.UseForeColor = True
        Me.gdvDataVT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvDataVT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvDataVT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvDataVT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvDataVT.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gdvDataVT.Appearance.HorzLine.Options.UseBackColor = True
        Me.gdvDataVT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.gdvDataVT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn20, Me.GridColumn29, Me.GridColumn32})
        Me.gdvDataVT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvDataVT.GridControl = Me.gdvVT
        Me.gdvDataVT.Name = "gdvDataVT"
        Me.gdvDataVT.OptionsBehavior.Editable = False
        Me.gdvDataVT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvDataVT.OptionsView.RowAutoHeight = True
        Me.gdvDataVT.OptionsView.ShowAutoFilterRow = True
        Me.gdvDataVT.OptionsView.ShowColumnHeaders = False
        Me.gdvDataVT.OptionsView.ShowGroupPanel = False
        Me.gdvDataVT.OptionsView.ShowIndicator = False
        Me.gdvDataVT.RowHeight = 30
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "ID"
        Me.GridColumn20.FieldName = "ID"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Width = 65
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn29.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn29.Caption = "Tên vật tư"
        Me.GridColumn29.ColumnEdit = Me.RepositoryItemMemoEdit3
        Me.GridColumn29.FieldName = "TenVatTu"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.ReadOnly = True
        Me.GridColumn29.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 0
        Me.GridColumn29.Width = 687
        '
        'RepositoryItemMemoEdit3
        '
        Me.RepositoryItemMemoEdit3.Name = "RepositoryItemMemoEdit3"
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn32.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn32.Caption = "ĐVT"
        Me.GridColumn32.FieldName = "DVT"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 1
        Me.GridColumn32.Width = 78
        '
        'btnDong
        '
        Me.btnDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btnDong.Location = New System.Drawing.Point(691, 437)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(63, 36)
        Me.btnDong.TabIndex = 54
        Me.btnDong.Text = "Đóng"
        '
        'btnThemMoi
        '
        Me.btnThemMoi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnThemMoi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnThemMoi.Appearance.Options.UseFont = True
        Me.btnThemMoi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnThemMoi.Enabled = False
        Me.btnThemMoi.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThemMoi.Location = New System.Drawing.Point(550, 437)
        Me.btnThemMoi.Name = "btnThemMoi"
        Me.btnThemMoi.Size = New System.Drawing.Size(66, 36)
        Me.btnThemMoi.TabIndex = 53
        Me.btnThemMoi.Text = "Thêm"
        '
        'btGhiLai
        '
        Me.btGhiLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btGhiLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btGhiLai.Appearance.Options.UseFont = True
        Me.btGhiLai.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btGhiLai.Image = CType(resources.GetObject("btGhiLai.Image"), System.Drawing.Image)
        Me.btGhiLai.Location = New System.Drawing.Point(622, 437)
        Me.btGhiLai.Name = "btGhiLai"
        Me.btGhiLai.Size = New System.Drawing.Size(63, 36)
        Me.btGhiLai.TabIndex = 52
        Me.btGhiLai.Text = "Ghi"
        '
        'txtSoKyConLai
        '
        Me.txtSoKyConLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSoKyConLai.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtSoKyConLai.Location = New System.Drawing.Point(450, 377)
        Me.txtSoKyConLai.Name = "txtSoKyConLai"
        Me.txtSoKyConLai.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSoKyConLai.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtSoKyConLai.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtSoKyConLai.Properties.Appearance.Options.UseBackColor = True
        Me.txtSoKyConLai.Properties.Appearance.Options.UseFont = True
        Me.txtSoKyConLai.Properties.Appearance.Options.UseForeColor = True
        Me.txtSoKyConLai.Properties.DisplayFormat.FormatString = "N0"
        Me.txtSoKyConLai.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSoKyConLai.Properties.EditFormat.FormatString = "N2"
        Me.txtSoKyConLai.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSoKyConLai.Properties.IsFloatValue = False
        Me.txtSoKyConLai.Properties.Mask.EditMask = "N0"
        Me.txtSoKyConLai.Size = New System.Drawing.Size(88, 20)
        Me.txtSoKyConLai.TabIndex = 2
        '
        'txtSoKyPhanBo
        '
        Me.txtSoKyPhanBo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSoKyPhanBo.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtSoKyPhanBo.Location = New System.Drawing.Point(269, 377)
        Me.txtSoKyPhanBo.Name = "txtSoKyPhanBo"
        Me.txtSoKyPhanBo.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSoKyPhanBo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtSoKyPhanBo.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtSoKyPhanBo.Properties.Appearance.Options.UseBackColor = True
        Me.txtSoKyPhanBo.Properties.Appearance.Options.UseFont = True
        Me.txtSoKyPhanBo.Properties.Appearance.Options.UseForeColor = True
        Me.txtSoKyPhanBo.Properties.DisplayFormat.FormatString = "N0"
        Me.txtSoKyPhanBo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSoKyPhanBo.Properties.EditFormat.FormatString = "N2"
        Me.txtSoKyPhanBo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSoKyPhanBo.Properties.IsFloatValue = False
        Me.txtSoKyPhanBo.Properties.Mask.EditMask = "N0"
        Me.txtSoKyPhanBo.Size = New System.Drawing.Size(91, 20)
        Me.txtSoKyPhanBo.TabIndex = 1
        '
        'txtSoLuong
        '
        Me.txtSoLuong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSoLuong.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtSoLuong.Location = New System.Drawing.Point(77, 377)
        Me.txtSoLuong.Name = "txtSoLuong"
        Me.txtSoLuong.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSoLuong.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtSoLuong.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtSoLuong.Properties.Appearance.Options.UseBackColor = True
        Me.txtSoLuong.Properties.Appearance.Options.UseFont = True
        Me.txtSoLuong.Properties.Appearance.Options.UseForeColor = True
        Me.txtSoLuong.Properties.DisplayFormat.FormatString = "N2"
        Me.txtSoLuong.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSoLuong.Properties.EditFormat.FormatString = "N2"
        Me.txtSoLuong.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtSoLuong.Properties.IsFloatValue = False
        Me.txtSoLuong.Properties.Mask.EditMask = "N00"
        Me.txtSoLuong.Size = New System.Drawing.Size(86, 20)
        Me.txtSoLuong.TabIndex = 0
        '
        'LabelControl17
        '
        Me.LabelControl17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl17.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl17.Location = New System.Drawing.Point(371, 381)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl17.TabIndex = 57
        Me.LabelControl17.Text = "Số kỳ còn lại"
        '
        'LabelControl16
        '
        Me.LabelControl16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl16.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl16.Location = New System.Drawing.Point(181, 381)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(79, 13)
        Me.LabelControl16.TabIndex = 56
        Me.LabelControl16.Text = "Số kỳ phân bổ"
        '
        'LabelControl15
        '
        Me.LabelControl15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl15.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl15.Location = New System.Drawing.Point(14, 381)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(49, 13)
        Me.LabelControl15.TabIndex = 55
        Me.LabelControl15.Text = "Số lượng"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(552, 381)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(73, 13)
        Me.LabelControl1.TabIndex = 61
        Me.LabelControl1.Text = "Giá trị còn lại"
        '
        'txtGiaTriConLai
        '
        Me.txtGiaTriConLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtGiaTriConLai.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtGiaTriConLai.Location = New System.Drawing.Point(639, 377)
        Me.txtGiaTriConLai.Name = "txtGiaTriConLai"
        Me.txtGiaTriConLai.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtGiaTriConLai.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtGiaTriConLai.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtGiaTriConLai.Properties.Appearance.Options.UseBackColor = True
        Me.txtGiaTriConLai.Properties.Appearance.Options.UseFont = True
        Me.txtGiaTriConLai.Properties.Appearance.Options.UseForeColor = True
        Me.txtGiaTriConLai.Properties.DisplayFormat.FormatString = "N0"
        Me.txtGiaTriConLai.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtGiaTriConLai.Properties.EditFormat.FormatString = "N2"
        Me.txtGiaTriConLai.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtGiaTriConLai.Properties.IsFloatValue = False
        Me.txtGiaTriConLai.Properties.Mask.EditMask = "N0"
        Me.txtGiaTriConLai.Size = New System.Drawing.Size(115, 20)
        Me.txtGiaTriConLai.TabIndex = 3
        '
        'txtGhiChu
        '
        Me.txtGhiChu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGhiChu.Location = New System.Drawing.Point(330, 410)
        Me.txtGhiChu.Name = "txtGhiChu"
        Me.txtGhiChu.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtGhiChu.Properties.Appearance.Options.UseBackColor = True
        Me.txtGhiChu.Size = New System.Drawing.Size(424, 20)
        Me.txtGhiChu.TabIndex = 4
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(269, 414)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl6.TabIndex = 64
        Me.LabelControl6.Text = "Ghi chú"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(14, 414)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl2.TabIndex = 65
        Me.LabelControl2.Text = "Phòng ban"
        '
        'cmbPhongBan
        '
        Me.cmbPhongBan.Location = New System.Drawing.Point(77, 410)
        Me.cmbPhongBan.Name = "cmbPhongBan"
        Me.cmbPhongBan.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cmbPhongBan.Properties.Appearance.Options.UseBackColor = True
        Me.cmbPhongBan.Properties.Appearance.Options.UseTextOptions = True
        Me.cmbPhongBan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cmbPhongBan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPhongBan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbPhongBan.Size = New System.Drawing.Size(183, 20)
        Me.cmbPhongBan.TabIndex = 66
        '
        'frmUpdateTonKhoThueCCDC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(771, 485)
        Me.Controls.Add(Me.cmbPhongBan)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtGhiChu)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.txtGiaTriConLai)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.txtSoKyConLai)
        Me.Controls.Add(Me.txtSoKyPhanBo)
        Me.Controls.Add(Me.txtSoLuong)
        Me.Controls.Add(Me.LabelControl17)
        Me.Controls.Add(Me.LabelControl16)
        Me.Controls.Add(Me.LabelControl15)
        Me.Controls.Add(Me.btnDong)
        Me.Controls.Add(Me.btnThemMoi)
        Me.Controls.Add(Me.btGhiLai)
        Me.Controls.Add(Me.GroupControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateTonKhoThueCCDC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmUpdateTonKhoThueCCDC"
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.gdvVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvDataVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSoKyConLai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSoKyPhanBo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSoLuong.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGiaTriConLai.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhiChu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPhongBan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvVT As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvDataVT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnThemMoi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btGhiLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSoKyConLai As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtSoKyPhanBo As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtSoLuong As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGiaTriConLai As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtGhiChu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbPhongBan As DevExpress.XtraEditors.ComboBoxEdit
End Class
