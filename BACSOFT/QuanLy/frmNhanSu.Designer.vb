<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhanSu
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
        Me.cbPhongBan = New DevExpress.XtraBars.BarEditItem()
        Me.rcbPhongBan = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cbTrangThai = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ChkTaiAnh = New DevExpress.XtraBars.BarCheckItem()
        Me.btTaiDS = New DevExpress.XtraBars.BarButtonItem()
        Me.btThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mSua = New DevExpress.XtraBars.BarButtonItem()
        Me.mXemAnhLon = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.gdvNhanSu = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhanSuCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutViewColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbPhong = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.LayoutViewColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutViewColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rHinhAnh = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbFile = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.popupFile = New DevExpress.XtraEditors.PopupContainerControl()
        Me.gListFileCT = New DevExpress.XtraEditors.GroupControl()
        Me.gdvListFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvListFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMatKhau = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhongBan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhanSu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhanSuCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rHinhAnh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popupFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popupFile.SuspendLayout()
        CType(Me.gListFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFileCT.SuspendLayout()
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.cbPhongBan, Me.cbTrangThai, Me.btTaiDS, Me.btThem, Me.btSua, Me.btXoa, Me.mThem, Me.mSua, Me.mXemAnhLon, Me.ChkTaiAnh})
        Me.BarManager1.MaxItemId = 11
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbPhongBan, Me.RepositoryItemComboBox1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cbPhongBan), New DevExpress.XtraBars.LinkPersistInfo(Me.cbTrangThai), New DevExpress.XtraBars.LinkPersistInfo(Me.ChkTaiAnh), New DevExpress.XtraBars.LinkPersistInfo(Me.btTaiDS), New DevExpress.XtraBars.LinkPersistInfo(Me.btThem), New DevExpress.XtraBars.LinkPersistInfo(Me.btSua), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, False, Me.btXoa, False)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'cbPhongBan
        '
        Me.cbPhongBan.Caption = "Phòng ban"
        Me.cbPhongBan.Edit = Me.rcbPhongBan
        Me.cbPhongBan.Id = 1
        Me.cbPhongBan.Name = "cbPhongBan"
        Me.cbPhongBan.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbPhongBan.Width = 176
        '
        'rcbPhongBan
        '
        Me.rcbPhongBan.AutoHeight = False
        Me.rcbPhongBan.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbPhongBan.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.rcbPhongBan.DisplayMember = "Ten"
        Me.rcbPhongBan.DropDownItemHeight = 22
        Me.rcbPhongBan.Name = "rcbPhongBan"
        Me.rcbPhongBan.NullText = "[Tất cả]"
        Me.rcbPhongBan.ShowHeader = False
        Me.rcbPhongBan.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbPhongBan.ValueMember = "ID"
        '
        'cbTrangThai
        '
        Me.cbTrangThai.Caption = "Trạng thái"
        Me.cbTrangThai.Edit = Me.RepositoryItemComboBox1
        Me.cbTrangThai.EditValue = "Còn làm việc"
        Me.cbTrangThai.Id = 2
        Me.cbTrangThai.Name = "cbTrangThai"
        Me.cbTrangThai.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbTrangThai.Width = 94
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Đã nghỉ", "Còn làm việc", "Tất cả"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'ChkTaiAnh
        '
        Me.ChkTaiAnh.Caption = "Tải ảnh"
        Me.ChkTaiAnh.Glyph = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.ChkTaiAnh.Id = 10
        Me.ChkTaiAnh.Name = "ChkTaiAnh"
        Me.ChkTaiAnh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btTaiDS
        '
        Me.btTaiDS.Caption = "Tải danh sách"
        Me.btTaiDS.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btTaiDS.Id = 3
        Me.btTaiDS.Name = "btTaiDS"
        Me.btTaiDS.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btThem
        '
        Me.btThem.Caption = "Thêm"
        Me.btThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Id = 4
        Me.btThem.Name = "btThem"
        Me.btThem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btSua
        '
        Me.btSua.Caption = "Sửa"
        Me.btSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSua.Id = 5
        Me.btSua.Name = "btSua"
        Me.btSua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXoa
        '
        Me.btXoa.Caption = "Xoá"
        Me.btXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoa.Id = 6
        Me.btXoa.Name = "btXoa"
        Me.btXoa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(932, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 504)
        Me.barDockControlBottom.Size = New System.Drawing.Size(932, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 471)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(932, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 471)
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 7
        Me.mThem.Name = "mThem"
        '
        'mSua
        '
        Me.mSua.Caption = "Sửa"
        Me.mSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSua.Id = 8
        Me.mSua.Name = "mSua"
        '
        'mXemAnhLon
        '
        Me.mXemAnhLon.Caption = "Xem ảnh lớn"
        Me.mXemAnhLon.Glyph = Global.BACSOFT.My.Resources.Resources.Preview_18
        Me.mXemAnhLon.Id = 9
        Me.mXemAnhLon.Name = "mXemAnhLon"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThem), New DevExpress.XtraBars.LinkPersistInfo(Me.mSua), New DevExpress.XtraBars.LinkPersistInfo(Me.mXemAnhLon, True)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'gdvNhanSu
        '
        Me.gdvNhanSu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNhanSu.Location = New System.Drawing.Point(0, 33)
        Me.gdvNhanSu.MainView = Me.gdvNhanSuCT
        Me.gdvNhanSu.MenuManager = Me.BarManager1
        Me.gdvNhanSu.Name = "gdvNhanSu"
        Me.gdvNhanSu.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rHinhAnh, Me.rcbPhong, Me.rcbFile, Me.RepositoryItemMemoEdit1})
        Me.gdvNhanSu.Size = New System.Drawing.Size(932, 471)
        Me.gdvNhanSu.TabIndex = 0
        Me.gdvNhanSu.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhanSuCT})
        '
        'gdvNhanSuCT
        '
        Me.gdvNhanSuCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhanSuCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNhanSuCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhanSuCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNhanSuCT.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.gdvNhanSuCT.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhanSuCT.Appearance.GroupRow.Options.UseFont = True
        Me.gdvNhanSuCT.Appearance.GroupRow.Options.UseForeColor = True
        Me.gdvNhanSuCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhanSuCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhanSuCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhanSuCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhanSuCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhanSuCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvNhanSuCT.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.gdvNhanSuCT.Appearance.Row.Options.UseFont = True
        Me.gdvNhanSuCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.LayoutViewColumn1, Me.LayoutViewColumn2, Me.LayoutViewColumn3, Me.GridColumn5, Me.LayoutViewColumn4, Me.LayoutViewColumn5, Me.GridColumn2, Me.LayoutViewColumn6, Me.LayoutViewColumn7, Me.GridColumn4, Me.LayoutViewColumn8, Me.LayoutViewColumn9, Me.GridColumn1, Me.LayoutViewColumn10, Me.LayoutViewColumn11, Me.colFile, Me.GridColumn3, Me.colMatKhau})
        Me.gdvNhanSuCT.GridControl = Me.gdvNhanSu
        Me.gdvNhanSuCT.GroupCount = 1
        Me.gdvNhanSuCT.Name = "gdvNhanSuCT"
        Me.gdvNhanSuCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvNhanSuCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvNhanSuCT.OptionsFind.AllowFindPanel = False
        Me.gdvNhanSuCT.OptionsView.ColumnAutoWidth = False
        Me.gdvNhanSuCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhanSuCT.OptionsView.RowAutoHeight = True
        Me.gdvNhanSuCT.OptionsView.ShowFooter = True
        Me.gdvNhanSuCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhanSuCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.LayoutViewColumn4, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'LayoutViewColumn1
        '
        Me.LayoutViewColumn1.Caption = "ID"
        Me.LayoutViewColumn1.FieldName = "ID"
        Me.LayoutViewColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.LayoutViewColumn1.Name = "LayoutViewColumn1"
        Me.LayoutViewColumn1.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn1.Visible = True
        Me.LayoutViewColumn1.VisibleIndex = 0
        Me.LayoutViewColumn1.Width = 79
        '
        'LayoutViewColumn2
        '
        Me.LayoutViewColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.LayoutViewColumn2.AppearanceCell.Options.UseFont = True
        Me.LayoutViewColumn2.Caption = "Họ tên"
        Me.LayoutViewColumn2.FieldName = "Ten"
        Me.LayoutViewColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.LayoutViewColumn2.Name = "LayoutViewColumn2"
        Me.LayoutViewColumn2.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn2.SummaryItem.DisplayFormat = "{0:N0}"
        Me.LayoutViewColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.LayoutViewColumn2.Visible = True
        Me.LayoutViewColumn2.VisibleIndex = 1
        Me.LayoutViewColumn2.Width = 150
        '
        'LayoutViewColumn3
        '
        Me.LayoutViewColumn3.Caption = "Chức vụ"
        Me.LayoutViewColumn3.FieldName = "ChucVu"
        Me.LayoutViewColumn3.Name = "LayoutViewColumn3"
        Me.LayoutViewColumn3.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn3.Visible = True
        Me.LayoutViewColumn3.VisibleIndex = 2
        Me.LayoutViewColumn3.Width = 150
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Bộ phận"
        Me.GridColumn5.FieldName = "BoPhan"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 100
        '
        'LayoutViewColumn4
        '
        Me.LayoutViewColumn4.Caption = "Phòng ban"
        Me.LayoutViewColumn4.ColumnEdit = Me.rcbPhong
        Me.LayoutViewColumn4.FieldName = "IDDepatment"
        Me.LayoutViewColumn4.Name = "LayoutViewColumn4"
        Me.LayoutViewColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.LayoutViewColumn4.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.LayoutViewColumn4.Width = 120
        '
        'rcbPhong
        '
        Me.rcbPhong.AutoHeight = False
        Me.rcbPhong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbPhong.DisplayMember = "Ten"
        Me.rcbPhong.DropDownItemHeight = 22
        Me.rcbPhong.Name = "rcbPhong"
        Me.rcbPhong.NullText = ""
        Me.rcbPhong.ShowHeader = False
        Me.rcbPhong.ValueMember = "ID"
        '
        'LayoutViewColumn5
        '
        Me.LayoutViewColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.LayoutViewColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LayoutViewColumn5.Caption = "Ngày sinh"
        Me.LayoutViewColumn5.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.LayoutViewColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.LayoutViewColumn5.FieldName = "NgaySinh"
        Me.LayoutViewColumn5.Name = "LayoutViewColumn5"
        Me.LayoutViewColumn5.OptionsColumn.AllowEdit = False
        Me.LayoutViewColumn5.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn5.Visible = True
        Me.LayoutViewColumn5.VisibleIndex = 4
        Me.LayoutViewColumn5.Width = 80
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "CMND"
        Me.GridColumn2.FieldName = "SoCMT"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 5
        Me.GridColumn2.Width = 100
        '
        'LayoutViewColumn6
        '
        Me.LayoutViewColumn6.Caption = "Di động"
        Me.LayoutViewColumn6.FieldName = "Mobile"
        Me.LayoutViewColumn6.Name = "LayoutViewColumn6"
        Me.LayoutViewColumn6.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn6.Visible = True
        Me.LayoutViewColumn6.VisibleIndex = 6
        Me.LayoutViewColumn6.Width = 128
        '
        'LayoutViewColumn7
        '
        Me.LayoutViewColumn7.Caption = "Di động 2"
        Me.LayoutViewColumn7.FieldName = "Mobile1"
        Me.LayoutViewColumn7.Name = "LayoutViewColumn7"
        Me.LayoutViewColumn7.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn7.Visible = True
        Me.LayoutViewColumn7.VisibleIndex = 7
        Me.LayoutViewColumn7.Width = 123
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Số nội bộ"
        Me.GridColumn4.FieldName = "DienThoaiCQ"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 8
        '
        'LayoutViewColumn8
        '
        Me.LayoutViewColumn8.Caption = "Email"
        Me.LayoutViewColumn8.FieldName = "Email"
        Me.LayoutViewColumn8.Name = "LayoutViewColumn8"
        Me.LayoutViewColumn8.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn8.Visible = True
        Me.LayoutViewColumn8.VisibleIndex = 9
        Me.LayoutViewColumn8.Width = 200
        '
        'LayoutViewColumn9
        '
        Me.LayoutViewColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.LayoutViewColumn9.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.LayoutViewColumn9.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.LayoutViewColumn9.Caption = "Địa chỉ"
        Me.LayoutViewColumn9.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.LayoutViewColumn9.FieldName = "DiaChi"
        Me.LayoutViewColumn9.Name = "LayoutViewColumn9"
        Me.LayoutViewColumn9.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn9.Visible = True
        Me.LayoutViewColumn9.VisibleIndex = 10
        Me.LayoutViewColumn9.Width = 307
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Mã truy cập"
        Me.GridColumn1.FieldName = "MaTruyCap"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 11
        Me.GridColumn1.Width = 120
        '
        'LayoutViewColumn10
        '
        Me.LayoutViewColumn10.Caption = "HinhAnh"
        Me.LayoutViewColumn10.FieldName = "HinhAnh"
        Me.LayoutViewColumn10.Name = "LayoutViewColumn10"
        '
        'LayoutViewColumn11
        '
        Me.LayoutViewColumn11.Caption = "Hình ảnh"
        Me.LayoutViewColumn11.ColumnEdit = Me.rHinhAnh
        Me.LayoutViewColumn11.FieldName = "HienThi"
        Me.LayoutViewColumn11.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.LayoutViewColumn11.Name = "LayoutViewColumn11"
        Me.LayoutViewColumn11.OptionsColumn.ReadOnly = True
        Me.LayoutViewColumn11.Visible = True
        Me.LayoutViewColumn11.VisibleIndex = 13
        '
        'rHinhAnh
        '
        Me.rHinhAnh.CustomHeight = 50
        Me.rHinhAnh.Name = "rHinhAnh"
        Me.rHinhAnh.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        '
        'colFile
        '
        Me.colFile.Caption = "File"
        Me.colFile.ColumnEdit = Me.rcbFile
        Me.colFile.FieldName = "FileDinhKem"
        Me.colFile.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.colFile.Name = "colFile"
        Me.colFile.Visible = True
        Me.colFile.VisibleIndex = 14
        Me.colFile.Width = 41
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
        Me.popupFile.Location = New System.Drawing.Point(337, 137)
        Me.popupFile.Name = "popupFile"
        Me.popupFile.Size = New System.Drawing.Size(264, 230)
        Me.popupFile.TabIndex = 11
        '
        'gListFileCT
        '
        Me.gListFileCT.Controls.Add(Me.gdvListFile)
        Me.gListFileCT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gListFileCT.Location = New System.Drawing.Point(0, 0)
        Me.gListFileCT.Name = "gListFileCT"
        Me.gListFileCT.Size = New System.Drawing.Size(264, 230)
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
        Me.gdvListFile.Size = New System.Drawing.Size(260, 206)
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
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Ngày vào"
        Me.GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "NgayVaoCTy"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 12
        '
        'colMatKhau
        '
        Me.colMatKhau.Caption = "Mật khẩu"
        Me.colMatKhau.FieldName = "MatKhau"
        Me.colMatKhau.Name = "colMatKhau"
        '
        'frmNhanSu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.popupFile)
        Me.Controls.Add(Me.gdvNhanSu)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmNhanSu"
        Me.BarManager1.SetPopupContextMenu(Me, Me.PopupMenu1)
        Me.Size = New System.Drawing.Size(932, 504)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhongBan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhanSu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhanSuCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rHinhAnh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popupFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popupFile.ResumeLayout(False)
        CType(Me.gListFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFileCT.ResumeLayout(False)
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents cbPhongBan As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbPhongBan As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents cbTrangThai As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents btTaiDS As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents gdvNhanSu As DevExpress.XtraGrid.GridControl
    Friend WithEvents rHinhAnh As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents rcbPhong As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents gdvNhanSuCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutViewColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutViewColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbFile As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents popupFile As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents gListFileCT As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvListFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvListFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents mXemAnhLon As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ChkTaiAnh As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents colMatKhau As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn

End Class
