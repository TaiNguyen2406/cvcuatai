<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhaCCTheoHangSX
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
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.cbXem = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.tbTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.tbDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btTaiLai = New DevExpress.XtraBars.BarButtonItem()
        Me.btThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mSua = New DevExpress.XtraBars.BarButtonItem()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rtbNoiDung = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbFile = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.popupFile = New DevExpress.XtraEditors.PopupContainerControl()
        Me.gListFileCT = New DevExpress.XtraEditors.GroupControl()
        Me.gdvListFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvListFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.pMenu = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbNoiDung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popupFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popupFile.SuspendLayout()
        CType(Me.gListFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFileCT.SuspendLayout()
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mSua, Me.mThem, Me.mXoa, Me.btThem, Me.btSua, Me.btXoa, Me.btTaiLai, Me.tbTuNgay, Me.tbDenNgay, Me.cbXem})
        Me.BarManager1.MaxItemId = 11
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateEdit1, Me.RepositoryItemDateEdit2, Me.RepositoryItemComboBox1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cbXem), New DevExpress.XtraBars.LinkPersistInfo(Me.tbTuNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.tbDenNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.btTaiLai), New DevExpress.XtraBars.LinkPersistInfo(Me.btThem, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btSua), New DevExpress.XtraBars.LinkPersistInfo(Me.btXoa)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DisableCustomization = True
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Custom 2"
        '
        'cbXem
        '
        Me.cbXem.Caption = "Xem"
        Me.cbXem.Edit = Me.RepositoryItemComboBox1
        Me.cbXem.EditValue = "Tất cả"
        Me.cbXem.Id = 10
        Me.cbXem.Name = "cbXem"
        Me.cbXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbXem.Width = 81
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Tất cả", "Theo ngày"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'tbTuNgay
        '
        Me.tbTuNgay.Caption = "Từ ngày"
        Me.tbTuNgay.Edit = Me.RepositoryItemDateEdit1
        Me.tbTuNgay.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.tbTuNgay.Id = 8
        Me.tbTuNgay.Name = "tbTuNgay"
        Me.tbTuNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbTuNgay.Width = 82
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'tbDenNgay
        '
        Me.tbDenNgay.Caption = "Đến ngày"
        Me.tbDenNgay.Edit = Me.RepositoryItemDateEdit2
        Me.tbDenNgay.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.tbDenNgay.Id = 9
        Me.tbDenNgay.Name = "tbDenNgay"
        Me.tbDenNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbDenNgay.Width = 82
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btTaiLai
        '
        Me.btTaiLai.Caption = "Tải lại"
        Me.btTaiLai.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btTaiLai.Id = 6
        Me.btTaiLai.Name = "btTaiLai"
        Me.btTaiLai.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btThem
        '
        Me.btThem.Caption = "Thêm"
        Me.btThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Id = 3
        Me.btThem.Name = "btThem"
        Me.btThem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btSua
        '
        Me.btSua.Caption = "Sửa"
        Me.btSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSua.Id = 4
        Me.btSua.Name = "btSua"
        Me.btSua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXoa
        '
        Me.btXoa.Caption = "Xóa"
        Me.btXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoa.Id = 5
        Me.btXoa.Name = "btXoa"
        Me.btXoa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1022, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 447)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1022, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 414)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1022, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 414)
        '
        'mSua
        '
        Me.mSua.Caption = "Sửa"
        Me.mSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSua.Id = 0
        Me.mSua.Name = "mSua"
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 1
        Me.mThem.Name = "mThem"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xóa"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 2
        Me.mXoa.Name = "mXoa"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 33)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rtbNoiDung, Me.rcbFile})
        Me.gdv.Size = New System.Drawing.Size(1022, 414)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn7, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gdvCT.GroupPanelText = "Kéo thả cột cần nhóm nội dung vào đây"
        Me.gdvCT.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "TenNhaCC", Me.GridColumn3, "{0:N0}")})
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowAutoFilterRow = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.RowHeight = 22
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "ID"
        Me.GridColumn1.FieldName = "ID"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Hãng SX"
        Me.GridColumn2.FieldName = "HangSX"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 215
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nhà cung cấp"
        Me.GridColumn3.FieldName = "TenNhaCC"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Ngày"
        Me.GridColumn7.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn7.FieldName = "NgayNhap"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.FixedWidth = True
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        Me.GridColumn7.Width = 80
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Mô tả"
        Me.GridColumn4.ColumnEdit = Me.rtbNoiDung
        Me.GridColumn4.FieldName = "MoTa"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'rtbNoiDung
        '
        Me.rtbNoiDung.Name = "rtbNoiDung"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "File"
        Me.GridColumn5.ColumnEdit = Me.rcbFile
        Me.GridColumn5.FieldName = "FileDinhKem"
        Me.GridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.FixedWidth = True
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 50
        '
        'rcbFile
        '
        Me.rcbFile.AutoHeight = False
        Me.rcbFile.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbFile.Name = "rcbFile"
        Me.rcbFile.PopupControl = Me.popupFile
        '
        'popupFile
        '
        Me.popupFile.Controls.Add(Me.gListFileCT)
        Me.popupFile.Location = New System.Drawing.Point(379, 108)
        Me.popupFile.Name = "popupFile"
        Me.popupFile.Size = New System.Drawing.Size(340, 230)
        Me.popupFile.TabIndex = 9
        '
        'gListFileCT
        '
        Me.gListFileCT.Controls.Add(Me.gdvListFile)
        Me.gListFileCT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gListFileCT.Location = New System.Drawing.Point(0, 0)
        Me.gListFileCT.Name = "gListFileCT"
        Me.gListFileCT.Size = New System.Drawing.Size(340, 230)
        Me.gListFileCT.TabIndex = 16
        Me.gListFileCT.Text = "Danh sách file đính kèm"
        '
        'gdvListFile
        '
        Me.gdvListFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvListFile.Location = New System.Drawing.Point(2, 22)
        Me.gdvListFile.MainView = Me.gdvListFileCT
        Me.gdvListFile.Name = "gdvListFile"
        Me.gdvListFile.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHyperLinkEdit4})
        Me.gdvListFile.Size = New System.Drawing.Size(336, 206)
        Me.gdvListFile.TabIndex = 0
        Me.gdvListFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvListFileCT})
        '
        'gdvListFileCT
        '
        Me.gdvListFileCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvListFileCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvListFileCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvListFileCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvListFileCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvListFileCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvListFileCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvListFileCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvListFileCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvListFileCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvListFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn52})
        Me.gdvListFileCT.GridControl = Me.gdvListFile
        Me.gdvListFileCT.Name = "gdvListFileCT"
        Me.gdvListFileCT.OptionsBehavior.Editable = False
        Me.gdvListFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvListFileCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvListFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvListFileCT.RowHeight = 22
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "File"
        Me.GridColumn52.ColumnEdit = Me.RepositoryItemHyperLinkEdit4
        Me.GridColumn52.FieldName = "File"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.OptionsColumn.AllowEdit = False
        Me.GridColumn52.OptionsColumn.ReadOnly = True
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 0
        Me.GridColumn52.Width = 500
        '
        'RepositoryItemHyperLinkEdit4
        '
        Me.RepositoryItemHyperLinkEdit4.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit4.Name = "RepositoryItemHyperLinkEdit4"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Phụ trách"
        Me.GridColumn6.FieldName = "PhuTrach"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.FixedWidth = True
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        Me.GridColumn6.Width = 150
        '
        'pMenu
        '
        Me.pMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThem), New DevExpress.XtraBars.LinkPersistInfo(Me.mSua, True), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoa, True)})
        Me.pMenu.Manager = Me.BarManager1
        Me.pMenu.Name = "pMenu"
        '
        'frmNhaCCTheoHangSX
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.popupFile)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmNhaCCTheoHangSX"
        Me.Size = New System.Drawing.Size(1022, 447)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbNoiDung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popupFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popupFile.ResumeLayout(False)
        CType(Me.gListFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFileCT.ResumeLayout(False)
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rtbNoiDung As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents mSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pMenu As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbFile As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents popupFile As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents gListFileCT As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvListFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvListFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents btThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btTaiLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tbTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents tbDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents cbXem As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox

End Class
