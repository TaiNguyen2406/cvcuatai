<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDanhMucNangLuc
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.lbTaiLaiNhomNL = New DevExpress.XtraEditors.LabelControl()
        Me.gdvNhomNL = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhomNLCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.lbTaiLaiTenNL = New DevExpress.XtraEditors.LabelControl()
        Me.gdvTenNL = New DevExpress.XtraGrid.GridControl()
        Me.gdvTenNLCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.btXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.btSua = New DevExpress.XtraBars.BarButtonItem()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.tabKyNang = New DevExpress.XtraTab.XtraTabPage()
        Me.gdvKyNang = New DevExpress.XtraGrid.GridControl()
        Me.gdvKyNangCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.rMemoText = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.XtraTabPage5 = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.btCapNhatNhomKyNang = New DevExpress.XtraBars.BarButtonItem()
        Me.btThemKyNang = New DevExpress.XtraBars.BarButtonItem()
        Me.btSuaKyNang = New DevExpress.XtraBars.BarButtonItem()
        Me.btXoaKyNang = New DevExpress.XtraBars.BarButtonItem()
        Me.mThemKyNang = New DevExpress.XtraBars.BarButtonItem()
        Me.mSuaKN = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoaKN = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        CType(Me.gdvNhomNL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhomNLCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.gdvTenNL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvTenNLCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.tabKyNang.SuspendLayout()
        CType(Me.gdvKyNang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvKyNangCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage5.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl3
        '
        Me.GroupControl3.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl3.AppearanceCaption.Options.UseFont = True
        Me.GroupControl3.Controls.Add(Me.lbTaiLaiNhomNL)
        Me.GroupControl3.Controls.Add(Me.gdvNhomNL)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(405, 440)
        Me.GroupControl3.TabIndex = 0
        Me.GroupControl3.Text = "Nhóm năng lực"
        '
        'lbTaiLaiNhomNL
        '
        Me.lbTaiLaiNhomNL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTaiLaiNhomNL.Appearance.Image = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.lbTaiLaiNhomNL.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbTaiLaiNhomNL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbTaiLaiNhomNL.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lbTaiLaiNhomNL.Location = New System.Drawing.Point(374, 0)
        Me.lbTaiLaiNhomNL.Name = "lbTaiLaiNhomNL"
        Me.lbTaiLaiNhomNL.Size = New System.Drawing.Size(26, 22)
        Me.lbTaiLaiNhomNL.TabIndex = 6
        '
        'gdvNhomNL
        '
        Me.gdvNhomNL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNhomNL.Location = New System.Drawing.Point(2, 22)
        Me.gdvNhomNL.MainView = Me.gdvNhomNLCT
        Me.gdvNhomNL.Name = "gdvNhomNL"
        Me.gdvNhomNL.Size = New System.Drawing.Size(401, 416)
        Me.gdvNhomNL.TabIndex = 5
        Me.gdvNhomNL.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhomNLCT})
        '
        'gdvNhomNLCT
        '
        Me.gdvNhomNLCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhomNLCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNhomNLCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhomNLCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNhomNLCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhomNLCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhomNLCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhomNLCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhomNLCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7})
        Me.gdvNhomNLCT.FixedLineWidth = 1
        Me.gdvNhomNLCT.GridControl = Me.gdvNhomNL
        Me.gdvNhomNLCT.Name = "gdvNhomNLCT"
        Me.gdvNhomNLCT.OptionsBehavior.Editable = False
        Me.gdvNhomNLCT.OptionsCustomization.AllowColumnMoving = False
        Me.gdvNhomNLCT.OptionsCustomization.AllowGroup = False
        Me.gdvNhomNLCT.OptionsFind.AllowFindPanel = False
        Me.gdvNhomNLCT.OptionsLayout.Columns.AddNewColumns = False
        Me.gdvNhomNLCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvNhomNLCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhomNLCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvNhomNLCT.OptionsView.ShowFooter = True
        Me.gdvNhomNLCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhomNLCT.RowHeight = 23
        Me.gdvNhomNLCT.Tag = "tên nhóm vật tư"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn6.AppearanceCell.Options.UseFont = True
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "Mã"
        Me.GridColumn6.FieldName = "ID"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.OptionsColumn.FixedWidth = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 50
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Nhóm năng lực"
        Me.GridColumn7.FieldName = "Ten"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 166
        '
        'GroupControl4
        '
        Me.GroupControl4.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl4.AppearanceCaption.Options.UseFont = True
        Me.GroupControl4.Controls.Add(Me.lbTaiLaiTenNL)
        Me.GroupControl4.Controls.Add(Me.gdvTenNL)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(459, 440)
        Me.GroupControl4.TabIndex = 1
        Me.GroupControl4.Text = "Tên năng lực"
        '
        'lbTaiLaiTenNL
        '
        Me.lbTaiLaiTenNL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbTaiLaiTenNL.Appearance.Image = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.lbTaiLaiTenNL.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.lbTaiLaiTenNL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbTaiLaiTenNL.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.lbTaiLaiTenNL.Location = New System.Drawing.Point(428, 0)
        Me.lbTaiLaiTenNL.Name = "lbTaiLaiTenNL"
        Me.lbTaiLaiTenNL.Size = New System.Drawing.Size(26, 22)
        Me.lbTaiLaiTenNL.TabIndex = 6
        '
        'gdvTenNL
        '
        Me.gdvTenNL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvTenNL.Location = New System.Drawing.Point(2, 22)
        Me.gdvTenNL.MainView = Me.gdvTenNLCT
        Me.gdvTenNL.Name = "gdvTenNL"
        Me.gdvTenNL.Size = New System.Drawing.Size(455, 416)
        Me.gdvTenNL.TabIndex = 5
        Me.gdvTenNL.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvTenNLCT})
        '
        'gdvTenNLCT
        '
        Me.gdvTenNLCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvTenNLCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvTenNLCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvTenNLCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvTenNLCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvTenNLCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvTenNLCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvTenNLCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvTenNLCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8, Me.GridColumn9})
        Me.gdvTenNLCT.FixedLineWidth = 1
        Me.gdvTenNLCT.GridControl = Me.gdvTenNL
        Me.gdvTenNLCT.Name = "gdvTenNLCT"
        Me.gdvTenNLCT.OptionsBehavior.Editable = False
        Me.gdvTenNLCT.OptionsCustomization.AllowColumnMoving = False
        Me.gdvTenNLCT.OptionsCustomization.AllowGroup = False
        Me.gdvTenNLCT.OptionsFind.AllowFindPanel = False
        Me.gdvTenNLCT.OptionsLayout.Columns.AddNewColumns = False
        Me.gdvTenNLCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvTenNLCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvTenNLCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvTenNLCT.OptionsView.ShowFooter = True
        Me.gdvTenNLCT.OptionsView.ShowGroupPanel = False
        Me.gdvTenNLCT.RowHeight = 23
        Me.gdvTenNLCT.Tag = "tên vật tư"
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn8.AppearanceCell.Options.UseFont = True
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn8.Caption = "Mã"
        Me.GridColumn8.FieldName = "ID"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.FixedWidth = True
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        Me.GridColumn8.Width = 50
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Tên năng lực"
        Me.GridColumn9.FieldName = "Ten"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 193
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'btXoa
        '
        Me.btXoa.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btXoa.Appearance.Options.UseFont = True
        Me.btXoa.Caption = "Xóa"
        Me.btXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoa.Id = 6
        Me.btXoa.Name = "btXoa"
        Me.btXoa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btSua
        '
        Me.btSua.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btSua.Appearance.Options.UseFont = True
        Me.btSua.Caption = "Phân quyền"
        Me.btSua.Glyph = Global.BACSOFT.My.Resources.Resources.GroupKey_18
        Me.btSua.Id = 2
        Me.btSua.Name = "btSua"
        Me.btSua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.AppearancePage.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XtraTabControl1.AppearancePage.Header.Options.UseFont = True
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.tabKyNang
        Me.XtraTabControl1.Size = New System.Drawing.Size(875, 466)
        Me.XtraTabControl1.TabIndex = 10
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabKyNang, Me.XtraTabPage5})
        '
        'tabKyNang
        '
        Me.tabKyNang.Controls.Add(Me.gdvKyNang)
        Me.tabKyNang.Controls.Add(Me.barDockControlLeft)
        Me.tabKyNang.Controls.Add(Me.barDockControlRight)
        Me.tabKyNang.Controls.Add(Me.barDockControlBottom)
        Me.tabKyNang.Controls.Add(Me.barDockControlTop)
        Me.tabKyNang.Name = "tabKyNang"
        Me.tabKyNang.Size = New System.Drawing.Size(869, 440)
        Me.tabKyNang.Text = "Kỹ năng"
        '
        'gdvKyNang
        '
        Me.gdvKyNang.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvKyNang.Location = New System.Drawing.Point(0, 33)
        Me.gdvKyNang.MainView = Me.gdvKyNangCT
        Me.gdvKyNang.Name = "gdvKyNang"
        Me.gdvKyNang.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rMemoText})
        Me.gdvKyNang.Size = New System.Drawing.Size(869, 407)
        Me.gdvKyNang.TabIndex = 5
        Me.gdvKyNang.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvKyNangCT})
        '
        'gdvKyNangCT
        '
        Me.gdvKyNangCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvKyNangCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvKyNangCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvKyNangCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvKyNangCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvKyNangCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvKyNangCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvKyNangCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvKyNangCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvKyNangCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvKyNangCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvKyNangCT.GridControl = Me.gdvKyNang
        Me.gdvKyNangCT.Name = "gdvKyNangCT"
        Me.gdvKyNangCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvKyNangCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvKyNangCT.OptionsBehavior.ReadOnly = True
        Me.gdvKyNangCT.OptionsFind.AllowFindPanel = False
        Me.gdvKyNangCT.OptionsView.ColumnAutoWidth = False
        Me.gdvKyNangCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvKyNangCT.OptionsView.RowAutoHeight = True
        Me.gdvKyNangCT.OptionsView.ShowFooter = True
        Me.gdvKyNangCT.OptionsView.ShowGroupPanel = False
        Me.gdvKyNangCT.RowHeight = 22
        '
        'rMemoText
        '
        Me.rMemoText.Name = "rMemoText"
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 407)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(869, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 407)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 440)
        Me.barDockControlBottom.Size = New System.Drawing.Size(869, 0)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(869, 33)
        '
        'XtraTabPage5
        '
        Me.XtraTabPage5.Controls.Add(Me.SplitContainerControl2)
        Me.XtraTabPage5.Name = "XtraTabPage5"
        Me.XtraTabPage5.Size = New System.Drawing.Size(869, 440)
        Me.XtraTabPage5.Text = "Nhóm năng lực - Tên năng lực"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.GroupControl3)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.GroupControl4)
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(869, 440)
        Me.SplitContainerControl2.SplitterPosition = 405
        Me.SplitContainerControl2.TabIndex = 1
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me.tabKyNang
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btCapNhatNhomKyNang, Me.btThemKyNang, Me.btSuaKyNang, Me.btXoaKyNang, Me.mThemKyNang, Me.mSuaKN, Me.mXoaKN})
        Me.BarManager1.MaxItemId = 8
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Tools"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btCapNhatNhomKyNang), New DevExpress.XtraBars.LinkPersistInfo(Me.btThemKyNang), New DevExpress.XtraBars.LinkPersistInfo(Me.btSuaKyNang), New DevExpress.XtraBars.LinkPersistInfo(Me.btXoaKyNang)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Tools"
        '
        'btCapNhatNhomKyNang
        '
        Me.btCapNhatNhomKyNang.Caption = "Cập nhật nhóm kỹ năng"
        Me.btCapNhatNhomKyNang.Id = 0
        Me.btCapNhatNhomKyNang.Name = "btCapNhatNhomKyNang"
        '
        'btThemKyNang
        '
        Me.btThemKyNang.Caption = "Thêm kỹ năng"
        Me.btThemKyNang.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThemKyNang.Id = 1
        Me.btThemKyNang.Name = "btThemKyNang"
        Me.btThemKyNang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btSuaKyNang
        '
        Me.btSuaKyNang.Caption = "Sửa thông tin kỹ năng"
        Me.btSuaKyNang.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSuaKyNang.Id = 2
        Me.btSuaKyNang.Name = "btSuaKyNang"
        Me.btSuaKyNang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXoaKyNang
        '
        Me.btXoaKyNang.Caption = "Xoá kỹ năng"
        Me.btXoaKyNang.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoaKyNang.Id = 4
        Me.btXoaKyNang.Name = "btXoaKyNang"
        Me.btXoaKyNang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mThemKyNang
        '
        Me.mThemKyNang.Caption = "Thêm kỹ năng"
        Me.mThemKyNang.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThemKyNang.Id = 5
        Me.mThemKyNang.Name = "mThemKyNang"
        Me.mThemKyNang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mSuaKN
        '
        Me.mSuaKN.Caption = "Sửa thông tin kỹ năng"
        Me.mSuaKN.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSuaKN.Id = 6
        Me.mSuaKN.Name = "mSuaKN"
        '
        'mXoaKN
        '
        Me.mXoaKN.Caption = "Xoá kỹ năng"
        Me.mXoaKN.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoaKN.Id = 7
        Me.mXoaKN.Name = "mXoaKN"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThemKyNang), New DevExpress.XtraBars.LinkPersistInfo(Me.mSuaKN), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoaKN)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'frmDanhMucNangLuc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Name = "frmDanhMucNangLuc"
        Me.Size = New System.Drawing.Size(875, 466)
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        CType(Me.gdvNhomNL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhomNLCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.gdvTenNL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvTenNLCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.tabKyNang.ResumeLayout(False)
        CType(Me.gdvKyNang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvKyNangCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage5.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents btXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage5 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvNhomNL As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNhomNLCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gdvTenNL As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvTenNLCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbTaiLaiNhomNL As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbTaiLaiTenNL As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tabKyNang As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gdvKyNang As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvKyNangCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rMemoText As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents btCapNhatNhomKyNang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btThemKyNang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSuaKyNang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXoaKyNang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mThemKyNang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSuaKN As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoaKN As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu

End Class
