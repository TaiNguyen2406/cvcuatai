<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNguoiGiaoDich
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
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem1 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.cbKhachHang = New DevExpress.XtraBars.BarEditItem()
        Me.rcbKhachHang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btTaiDS = New DevExpress.XtraBars.BarButtonItem()
        Me.btThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btXoaNgd = New DevExpress.XtraBars.BarButtonItem()
        Me.btXuatExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.btInDiaChi = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mSua = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.rcbLoaiKH = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.gdvNgd = New DevExpress.XtraGrid.GridControl()
        Me.gdvNgdCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbDoiTuongNhanEmail = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkMoi = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cbPhanHoi = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbKhachHang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbLoaiKH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNgd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNgdCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbDoiTuongNhanEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMoi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbPhanHoi, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btThem, Me.btSua, Me.cbKhachHang, Me.btTaiDS, Me.btXuatExcel, Me.btXoaNgd, Me.mThem, Me.mSua, Me.mXoa, Me.btInDiaChi})
        Me.BarManager1.MaxItemId = 15
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbLoaiKH, Me.rcbKhachHang})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarItemVertIndent = 0
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cbKhachHang), New DevExpress.XtraBars.LinkPersistInfo(Me.btTaiDS), New DevExpress.XtraBars.LinkPersistInfo(Me.btThem, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btSua), New DevExpress.XtraBars.LinkPersistInfo(Me.btXoaNgd), New DevExpress.XtraBars.LinkPersistInfo(Me.btXuatExcel), New DevExpress.XtraBars.LinkPersistInfo(Me.btInDiaChi)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'cbKhachHang
        '
        Me.cbKhachHang.Caption = "Nơi công tác"
        Me.cbKhachHang.Edit = Me.rcbKhachHang
        Me.cbKhachHang.Id = 7
        Me.cbKhachHang.Name = "cbKhachHang"
        Me.cbKhachHang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbKhachHang.Width = 160
        '
        'rcbKhachHang
        '
        Me.rcbKhachHang.AutoHeight = False
        Me.rcbKhachHang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbKhachHang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ttcMa", 10, "Mã KH"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên")})
        Me.rcbKhachHang.DisplayMember = "ttcMa"
        Me.rcbKhachHang.DropDownItemHeight = 22
        Me.rcbKhachHang.Name = "rcbKhachHang"
        Me.rcbKhachHang.NullText = "[Tất cả]"
        Me.rcbKhachHang.PopupWidth = 400
        Me.rcbKhachHang.ShowHeader = False
        Me.rcbKhachHang.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbKhachHang.ValueMember = "ID"
        '
        'btTaiDS
        '
        Me.btTaiDS.Caption = "Tải DS"
        Me.btTaiDS.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btTaiDS.Id = 8
        Me.btTaiDS.Name = "btTaiDS"
        Me.btTaiDS.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btThem
        '
        Me.btThem.Caption = "Thêm Ngd"
        Me.btThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Id = 1
        Me.btThem.Name = "btThem"
        Me.btThem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btSua
        '
        Me.btSua.Caption = "Sửa thông tin Ngd"
        Me.btSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSua.Id = 2
        Me.btSua.Name = "btSua"
        Me.btSua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXoaNgd
        '
        Me.btXoaNgd.Caption = "Xóa Ngd"
        Me.btXoaNgd.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoaNgd.Id = 10
        Me.btXoaNgd.Name = "btXoaNgd"
        Me.btXoaNgd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXuatExcel
        '
        Me.btXuatExcel.Caption = "Xuất Excel"
        Me.btXuatExcel.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btXuatExcel.Id = 9
        Me.btXuatExcel.Name = "btXuatExcel"
        Me.btXuatExcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btInDiaChi
        '
        Me.btInDiaChi.Caption = "In địa chỉ"
        Me.btInDiaChi.Glyph = Global.BACSOFT.My.Resources.Resources.print_18
        Me.btInDiaChi.Id = 14
        Me.btInDiaChi.Name = "btInDiaChi"
        Me.btInDiaChi.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        ToolTipTitleItem1.Text = "In địa chỉ giao hàng"
        SuperToolTip1.Items.Add(ToolTipTitleItem1)
        Me.btInDiaChi.SuperTip = SuperToolTip1
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(1144, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 678)
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1144, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 645)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1144, 33)
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 645)
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm ngd"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 11
        Me.mThem.Name = "mThem"
        '
        'mSua
        '
        Me.mSua.Caption = "Sửa thông tin Ngd"
        Me.mSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSua.Id = 12
        Me.mSua.Name = "mSua"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xóa ngd"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 13
        Me.mXoa.Name = "mXoa"
        '
        'rcbLoaiKH
        '
        Me.rcbLoaiKH.AutoHeight = False
        Me.rcbLoaiKH.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbLoaiKH.DropDownItemHeight = 22
        Me.rcbLoaiKH.Items.AddRange(New Object() {"Tất cả", "Khách hàng", "Nhà cung cấp"})
        Me.rcbLoaiKH.Name = "rcbLoaiKH"
        Me.rcbLoaiKH.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThem), New DevExpress.XtraBars.LinkPersistInfo(Me.mSua), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoa)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'gdvNgd
        '
        Me.gdvNgd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNgd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gdvNgd.Location = New System.Drawing.Point(0, 33)
        Me.gdvNgd.MainView = Me.gdvNgdCT
        Me.gdvNgd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gdvNgd.MenuManager = Me.BarManager1
        Me.gdvNgd.Name = "gdvNgd"
        Me.gdvNgd.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1, Me.rcbDoiTuongNhanEmail, Me.cbPhanHoi, Me.chkMoi})
        Me.gdvNgd.Size = New System.Drawing.Size(1144, 645)
        Me.gdvNgd.TabIndex = 4
        Me.gdvNgd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNgdCT})
        '
        'gdvNgdCT
        '
        Me.gdvNgdCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNgdCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNgdCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNgdCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNgdCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNgdCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNgdCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNgdCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNgdCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNgdCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvNgdCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.GridColumn25, Me.GridColumn3, Me.GridColumn20, Me.GridColumn2, Me.GridColumn21, Me.GridColumn13, Me.GridColumn14, Me.GridColumn11, Me.GridColumn10, Me.GridColumn19, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn12, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn22, Me.GridColumn1, Me.GridColumn23, Me.GridColumn24, Me.GridColumn26})
        Me.gdvNgdCT.GridControl = Me.gdvNgd
        Me.gdvNgdCT.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gdvNgdCT.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Ten", Me.GridColumn3, "{0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Mobile", Me.GridColumn13, "{0:N0}", "0"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Mobile1", Me.GridColumn14, "{0:N0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Custom, "Email", Me.GridColumn19, "{0:N0}")})
        Me.gdvNgdCT.Name = "gdvNgdCT"
        Me.gdvNgdCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvNgdCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvNgdCT.OptionsFind.AllowFindPanel = False
        Me.gdvNgdCT.OptionsView.ColumnAutoWidth = False
        Me.gdvNgdCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNgdCT.OptionsView.RowAutoHeight = True
        Me.gdvNgdCT.OptionsView.ShowAutoFilterRow = True
        Me.gdvNgdCT.OptionsView.ShowFooter = True
        Me.gdvNgdCT.RowHeight = 22
        '
        'colID
        '
        Me.colID.Caption = "ID"
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        Me.colID.OptionsColumn.ReadOnly = True
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Xưng hô"
        Me.GridColumn25.FieldName = "XungHo"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 3
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Người giao dịch"
        Me.GridColumn3.FieldName = "Ten"
        Me.GridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 150
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Nơi công tác"
        Me.GridColumn20.FieldName = "ttcMa"
        Me.GridColumn20.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.OptionsColumn.ReadOnly = True
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 1
        Me.GridColumn20.Width = 100
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tên công ty"
        Me.GridColumn2.FieldName = "KhachHang"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 100
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Chức vụ"
        Me.GridColumn21.FieldName = "Chucvu"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.ReadOnly = True
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 4
        Me.GridColumn21.Width = 100
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Di động 1"
        Me.GridColumn13.FieldName = "Mobile"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.ReadOnly = True
        Me.GridColumn13.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn13.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 5
        Me.GridColumn13.Width = 100
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Di động 2"
        Me.GridColumn14.FieldName = "Mobile1"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.ReadOnly = True
        Me.GridColumn14.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn14.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 6
        Me.GridColumn14.Width = 100
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "ĐT cơ quan"
        Me.GridColumn11.FieldName = "DienthoaiCQ"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.ReadOnly = True
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 7
        Me.GridColumn11.Width = 100
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "ĐT nhà riêng"
        Me.GridColumn10.FieldName = "DienthoaiNR"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.ReadOnly = True
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 8
        Me.GridColumn10.Width = 100
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Email"
        Me.GridColumn19.FieldName = "Email"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.OptionsColumn.ReadOnly = True
        Me.GridColumn19.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn19.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 9
        Me.GridColumn19.Width = 150
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Ngày sinh"
        Me.GridColumn4.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn4.FieldName = "Ngaysinh"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 11
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "CMND"
        Me.GridColumn5.FieldName = "SoCMT"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.ReadOnly = True
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 12
        Me.GridColumn5.Width = 100
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Ngày cấp"
        Me.GridColumn6.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn6.FieldName = "Ngaycap"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 13
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Nơi cấp"
        Me.GridColumn7.FieldName = "Noicap"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 14
        Me.GridColumn7.Width = 100
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn8.Caption = "Nguyên quán"
        Me.GridColumn8.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn8.FieldName = "Nguyenquan"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.ReadOnly = True
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 15
        Me.GridColumn8.Width = 200
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn9.Caption = "Địa chỉ"
        Me.GridColumn9.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn9.FieldName = "Diachi"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.ReadOnly = True
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 16
        Me.GridColumn9.Width = 200
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Fax"
        Me.GridColumn12.FieldName = "Fax"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.ReadOnly = True
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 17
        Me.GridColumn12.Width = 100
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Tài khoản"
        Me.GridColumn15.FieldName = "Taikhoan"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.ReadOnly = True
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 18
        Me.GridColumn15.Width = 100
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Ngân hàng"
        Me.GridColumn16.FieldName = "Nganhang"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.ReadOnly = True
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 19
        Me.GridColumn16.Width = 200
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Take care"
        Me.GridColumn17.FieldName = "TakeCare"
        Me.GridColumn17.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.ReadOnly = True
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 23
        Me.GridColumn17.Width = 120
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "IDNoiCtac"
        Me.GridColumn18.FieldName = "Noictac"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.ReadOnly = True
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Còn làm"
        Me.GridColumn22.FieldName = "Trangthai"
        Me.GridColumn22.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.FixedWidth = True
        Me.GridColumn22.OptionsColumn.ReadOnly = True
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 22
        Me.GridColumn22.Width = 52
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Đối tượng nhận Email"
        Me.GridColumn1.ColumnEdit = Me.rcbDoiTuongNhanEmail
        Me.GridColumn1.FieldName = "DoiTuongNhanEmail"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 10
        Me.GridColumn1.Width = 178
        '
        'rcbDoiTuongNhanEmail
        '
        Me.rcbDoiTuongNhanEmail.AutoHeight = False
        Me.rcbDoiTuongNhanEmail.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbDoiTuongNhanEmail.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name7", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name8")})
        Me.rcbDoiTuongNhanEmail.DisplayMember = "Ten"
        Me.rcbDoiTuongNhanEmail.DropDownItemHeight = 22
        Me.rcbDoiTuongNhanEmail.Name = "rcbDoiTuongNhanEmail"
        Me.rcbDoiTuongNhanEmail.NullText = ""
        Me.rcbDoiTuongNhanEmail.ShowHeader = False
        Me.rcbDoiTuongNhanEmail.ValueMember = "ID"
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Mời"
        Me.GridColumn23.ColumnEdit = Me.chkMoi
        Me.GridColumn23.FieldName = "Moi"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 20
        Me.GridColumn23.Width = 38
        '
        'chkMoi
        '
        Me.chkMoi.AutoHeight = False
        Me.chkMoi.Name = "chkMoi"
        Me.chkMoi.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkMoi.PictureChecked = Global.BACSOFT.My.Resources.Resources.Checked
        Me.chkMoi.PictureGrayed = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.chkMoi.PictureUnchecked = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.chkMoi.ValueGrayed = False
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Phản hồi"
        Me.GridColumn24.ColumnEdit = Me.cbPhanHoi
        Me.GridColumn24.FieldName = "PhanHoi"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 21
        Me.GridColumn24.Width = 160
        '
        'cbPhanHoi
        '
        Me.cbPhanHoi.AutoHeight = False
        Me.cbPhanHoi.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbPhanHoi.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.cbPhanHoi.DisplayMember = "Ten"
        Me.cbPhanHoi.DropDownItemHeight = 22
        Me.cbPhanHoi.Name = "cbPhanHoi"
        Me.cbPhanHoi.NullText = ""
        Me.cbPhanHoi.ShowHeader = False
        Me.cbPhanHoi.ValueMember = "ID"
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "GridColumn26"
        Me.GridColumn26.FieldName = "IDTakeCare"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'frmNguoiGiaoDich
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdvNgd)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmNguoiGiaoDich"
        Me.Size = New System.Drawing.Size(1144, 678)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbKhachHang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbLoaiKH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNgd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNgdCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbDoiTuongNhanEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMoi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbPhanHoi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents rcbLoaiKH As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents gdvNgd As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNgdCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cbKhachHang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbKhachHang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btTaiDS As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btXuatExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXoaNgd As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbDoiTuongNhanEmail As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cbPhanHoi As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents chkMoi As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btInDiaChi As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn

End Class
