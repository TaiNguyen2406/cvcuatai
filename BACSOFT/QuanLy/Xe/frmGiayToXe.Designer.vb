<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGiayToXe
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGiayToXe))
        Me.gcGiayToXe = New DevExpress.XtraGrid.GridControl()
        Me.gvGiayToXe = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcoIdXe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgaysua = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueLoaiGiayTo = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.barDeTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDeDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barLueXe = New DevExpress.XtraBars.BarEditItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barLueLoaiGiayTo = New DevExpress.XtraBars.BarEditItem()
        Me.barRiLoaiGiayTo = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btnThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.btnLoad = New DevExpress.XtraBars.BarButtonItem()
        Me.barCiLoc = New DevExpress.XtraBars.BarCheckItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.gcGiayToXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvGiayToXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueLoaiGiayTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barRiLoaiGiayTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcGiayToXe
        '
        Me.gcGiayToXe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcGiayToXe.Location = New System.Drawing.Point(0, 59)
        Me.gcGiayToXe.MainView = Me.gvGiayToXe
        Me.gcGiayToXe.Name = "gcGiayToXe"
        Me.gcGiayToXe.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riLueLoaiGiayTo})
        Me.gcGiayToXe.Size = New System.Drawing.Size(1164, 285)
        Me.gcGiayToXe.TabIndex = 72
        Me.gcGiayToXe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvGiayToXe})
        '
        'gvGiayToXe
        '
        Me.gvGiayToXe.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvGiayToXe.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvGiayToXe.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvGiayToXe.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvGiayToXe.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvGiayToXe.Appearance.FooterPanel.Options.UseFont = True
        Me.gvGiayToXe.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvGiayToXe.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvGiayToXe.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvGiayToXe.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvGiayToXe.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvGiayToXe.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvGiayToXe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolID, Me.gcoIdXe, Me.gcolTenxe, Me.gcolNgaysua, Me.GridColumn2, Me.GridColumn1, Me.GridColumn3})
        Me.gvGiayToXe.GridControl = Me.gcGiayToXe
        Me.gvGiayToXe.Name = "gvGiayToXe"
        Me.gvGiayToXe.OptionsBehavior.Editable = False
        Me.gvGiayToXe.OptionsCustomization.AllowColumnMoving = False
        Me.gvGiayToXe.OptionsCustomization.AllowGroup = False
        Me.gvGiayToXe.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvGiayToXe.OptionsView.EnableAppearanceEvenRow = True
        Me.gvGiayToXe.OptionsView.ShowFooter = True
        Me.gvGiayToXe.OptionsView.ShowGroupPanel = False
        Me.gvGiayToXe.OptionsView.ShowIndicator = False
        Me.gvGiayToXe.RowHeight = 23
        '
        'gcolID
        '
        Me.gcolID.Caption = "ID xe"
        Me.gcolID.FieldName = "id"
        Me.gcolID.Name = "gcolID"
        '
        'gcoIdXe
        '
        Me.gcoIdXe.Caption = "ID xe"
        Me.gcoIdXe.FieldName = "idxe"
        Me.gcoIdXe.Name = "gcoIdXe"
        '
        'gcolTenxe
        '
        Me.gcolTenxe.AppearanceCell.Options.UseTextOptions = True
        Me.gcolTenxe.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolTenxe.Caption = "Tên xe"
        Me.gcolTenxe.FieldName = "tenxe"
        Me.gcolTenxe.Name = "gcolTenxe"
        Me.gcolTenxe.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolTenxe.Visible = True
        Me.gcolTenxe.VisibleIndex = 0
        '
        'gcolNgaysua
        '
        Me.gcolNgaysua.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgaysua.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgaysua.Caption = "Ngày bắt đầu"
        Me.gcolNgaysua.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.gcolNgaysua.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gcolNgaysua.FieldName = "ngaybatdau"
        Me.gcolNgaysua.Name = "gcolNgaysua"
        Me.gcolNgaysua.Visible = True
        Me.gcolNgaysua.VisibleIndex = 2
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Ngày hết hạn"
        Me.GridColumn2.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "ngayhethan"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 3
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Ghi chú"
        Me.GridColumn1.FieldName = "ghichu"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 4
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Loại giấy tờ"
        Me.GridColumn3.ColumnEdit = Me.riLueLoaiGiayTo
        Me.GridColumn3.FieldName = "idloaigiayto"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'riLueLoaiGiayTo
        '
        Me.riLueLoaiGiayTo.AutoHeight = False
        Me.riLueLoaiGiayTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueLoaiGiayTo.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenloaigiayto", "Tên ")})
        Me.riLueLoaiGiayTo.DisplayMember = "tenloaigiayto"
        Me.riLueLoaiGiayTo.Name = "riLueLoaiGiayTo"
        Me.riLueLoaiGiayTo.NullText = ""
        Me.riLueLoaiGiayTo.ShowHeader = False
        Me.riLueLoaiGiayTo.ValueMember = "id"
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barLueXe, Me.barDeTuNgay, Me.barDeDenNgay, Me.btnLoad, Me.barCiLoc, Me.btnThem, Me.btnSua, Me.btnXoa, Me.BarStaticItem1, Me.barLueLoaiGiayTo, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.BarButtonItem4})
        Me.BarManager1.MainMenu = Me.Bar1
        Me.BarManager1.MaxItemId = 35
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.deTuNgay, Me.deDenNgay, Me.barRiLoaiGiayTo})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Main menu"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarStaticItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueXe, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueLoaiGiayTo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnLoad, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCiLoc, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.MultiLine = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Main menu"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "Ngày hết hạn:"
        Me.BarStaticItem1.Id = 28
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barDeTuNgay
        '
        Me.barDeTuNgay.Caption = "Từ ngày"
        Me.barDeTuNgay.Edit = Me.deTuNgay
        Me.barDeTuNgay.Id = 13
        Me.barDeTuNgay.Name = "barDeTuNgay"
        Me.barDeTuNgay.Width = 109
        '
        'deTuNgay
        '
        Me.deTuNgay.AutoHeight = False
        Me.deTuNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.deTuNgay.EditFormat.FormatString = "yyyy/MM/dd"
        Me.deTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deTuNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.deTuNgay.Mask.UseMaskAsDisplayFormat = True
        Me.deTuNgay.Name = "deTuNgay"
        Me.deTuNgay.NullText = "Tất cả"
        Me.deTuNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barDeDenNgay
        '
        Me.barDeDenNgay.Caption = "Đến ngày"
        Me.barDeDenNgay.Edit = Me.deDenNgay
        Me.barDeDenNgay.Id = 16
        Me.barDeDenNgay.Name = "barDeDenNgay"
        Me.barDeDenNgay.Width = 113
        '
        'deDenNgay
        '
        Me.deDenNgay.AutoHeight = False
        Me.deDenNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.deDenNgay.EditFormat.FormatString = "yyyy/MM/dd"
        Me.deDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deDenNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.deDenNgay.Mask.UseMaskAsDisplayFormat = True
        Me.deDenNgay.Name = "deDenNgay"
        Me.deDenNgay.NullText = "Tất cả"
        Me.deDenNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barLueXe
        '
        Me.barLueXe.Caption = "Xe"
        Me.barLueXe.Edit = Me.lueXe
        Me.barLueXe.Id = 12
        Me.barLueXe.Name = "barLueXe"
        Me.barLueXe.Width = 189
        '
        'lueXe
        '
        Me.lueXe.AutoHeight = False
        Me.lueXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.lueXe.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Tên xe"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Mã xe", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueXe.DisplayMember = "tenxe"
        Me.lueXe.Name = "lueXe"
        Me.lueXe.NullText = "[Tất cả]"
        Me.lueXe.ValueMember = "id"
        '
        'barLueLoaiGiayTo
        '
        Me.barLueLoaiGiayTo.Caption = "Loại giấy tờ"
        Me.barLueLoaiGiayTo.Edit = Me.barRiLoaiGiayTo
        Me.barLueLoaiGiayTo.Id = 30
        Me.barLueLoaiGiayTo.Name = "barLueLoaiGiayTo"
        Me.barLueLoaiGiayTo.Width = 166
        '
        'barRiLoaiGiayTo
        '
        Me.barRiLoaiGiayTo.AutoHeight = False
        Me.barRiLoaiGiayTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.barRiLoaiGiayTo.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenloaigiayto", "Tên")})
        Me.barRiLoaiGiayTo.DisplayMember = "tenloaigiayto"
        Me.barRiLoaiGiayTo.Name = "barRiLoaiGiayTo"
        Me.barRiLoaiGiayTo.NullText = "Tất cả"
        Me.barRiLoaiGiayTo.ShowHeader = False
        Me.barRiLoaiGiayTo.ValueMember = "id"
        '
        'btnThem
        '
        Me.btnThem.Caption = "Thêm"
        Me.btnThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThem.Id = 24
        Me.btnThem.Name = "btnThem"
        '
        'btnSua
        '
        Me.btnSua.Caption = "Sửa"
        Me.btnSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btnSua.Id = 25
        Me.btnSua.Name = "btnSua"
        '
        'btnXoa
        '
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btnXoa.Id = 26
        Me.btnXoa.Name = "btnXoa"
        '
        'btnLoad
        '
        Me.btnLoad.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.Appearance.Options.UseFont = True
        Me.btnLoad.Caption = "Tải lại"
        Me.btnLoad.Glyph = CType(resources.GetObject("btnLoad.Glyph"), System.Drawing.Image)
        Me.btnLoad.Id = 20
        Me.btnLoad.Name = "btnLoad"
        '
        'barCiLoc
        '
        Me.barCiLoc.Caption = "Lọc"
        Me.barCiLoc.Glyph = Global.BACSOFT.My.Resources.Resources.filter_18
        Me.barCiLoc.Id = 23
        Me.barCiLoc.Name = "barCiLoc"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1164, 59)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 344)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1164, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 59)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 285)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1164, 59)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 285)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "BarButtonItem1"
        Me.BarButtonItem1.Id = 31
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Thêm"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.BarButtonItem2.Id = 32
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Sửa"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.BarButtonItem3.Id = 33
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Xóa"
        Me.BarButtonItem4.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem4.Id = 34
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem4, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'frmGiayToXe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1164, 344)
        Me.Controls.Add(Me.gcGiayToXe)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmGiayToXe"
        Me.BarManager1.SetPopupContextMenu(Me, Me.PopupMenu1)
        Me.Text = "Giấy tờ xe"
        CType(Me.gcGiayToXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvGiayToXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueLoaiGiayTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barRiLoaiGiayTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcGiayToXe As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvGiayToXe As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcolID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcoIdXe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNgaysua As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents riLueLoaiGiayTo As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents btnThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barLueXe As DevExpress.XtraBars.BarEditItem
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barDeTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barDeDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btnLoad As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barCiLoc As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents barLueLoaiGiayTo As DevExpress.XtraBars.BarEditItem
    Friend WithEvents barRiLoaiGiayTo As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
End Class
