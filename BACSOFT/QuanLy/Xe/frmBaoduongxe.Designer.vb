<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBaoduongxe
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBaoduongxe))
        Me.gcBaoduongxe = New DevExpress.XtraGrid.GridControl()
        Me.gvBaoduongxe = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcoIdXe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolIDNTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenNTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolSoKmKhiBD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolChiphi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgaysua = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.barbtnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.barDeTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDeDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barLueXe = New DevExpress.XtraBars.BarEditItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btnThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.barCiLoc = New DevExpress.XtraBars.BarCheckItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarEditItem2 = New DevExpress.XtraBars.BarEditItem()
        Me.barCbxTuNgay = New DevExpress.XtraBars.BarCheckItem()
        Me.barCbxDenngay = New DevExpress.XtraBars.BarCheckItem()
        CType(Me.gcBaoduongxe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvBaoduongxe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcBaoduongxe
        '
        Me.gcBaoduongxe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcBaoduongxe.Location = New System.Drawing.Point(0, 59)
        Me.gcBaoduongxe.MainView = Me.gvBaoduongxe
        Me.gcBaoduongxe.Name = "gcBaoduongxe"
        Me.gcBaoduongxe.Size = New System.Drawing.Size(887, 288)
        Me.gcBaoduongxe.TabIndex = 71
        Me.gcBaoduongxe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvBaoduongxe})
        '
        'gvBaoduongxe
        '
        Me.gvBaoduongxe.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvBaoduongxe.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvBaoduongxe.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvBaoduongxe.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvBaoduongxe.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvBaoduongxe.Appearance.FooterPanel.Options.UseFont = True
        Me.gvBaoduongxe.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvBaoduongxe.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvBaoduongxe.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvBaoduongxe.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvBaoduongxe.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvBaoduongxe.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvBaoduongxe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolID, Me.gcoIdXe, Me.gcolIDNTH, Me.gcolTenNTH, Me.gcolTenxe, Me.GridColumn2, Me.gcolSoKmKhiBD, Me.gcolChiphi, Me.gcolNgaysua, Me.GridColumn1})
        Me.gvBaoduongxe.GridControl = Me.gcBaoduongxe
        Me.gvBaoduongxe.Name = "gvBaoduongxe"
        Me.gvBaoduongxe.OptionsBehavior.Editable = False
        Me.gvBaoduongxe.OptionsCustomization.AllowColumnMoving = False
        Me.gvBaoduongxe.OptionsCustomization.AllowGroup = False
        Me.gvBaoduongxe.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvBaoduongxe.OptionsView.EnableAppearanceEvenRow = True
        Me.gvBaoduongxe.OptionsView.ShowFooter = True
        Me.gvBaoduongxe.OptionsView.ShowGroupPanel = False
        Me.gvBaoduongxe.RowHeight = 23
        '
        'gcolID
        '
        Me.gcolID.Caption = "ID xe"
        Me.gcolID.FieldName = "id"
        Me.gcolID.Name = "gcolID"
        '
        'gcoIdXe
        '
        Me.gcoIdXe.Caption = "ID Hư hại xe"
        Me.gcoIdXe.FieldName = "idxe"
        Me.gcoIdXe.Name = "gcoIdXe"
        '
        'gcolIDNTH
        '
        Me.gcolIDNTH.Caption = "ID người thực hiện"
        Me.gcolIDNTH.FieldName = "idnguoithuchien"
        Me.gcolIDNTH.Name = "gcolIDNTH"
        '
        'gcolTenNTH
        '
        Me.gcolTenNTH.AppearanceCell.Options.UseTextOptions = True
        Me.gcolTenNTH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolTenNTH.Caption = "Người thực hiện"
        Me.gcolTenNTH.FieldName = "ten"
        Me.gcolTenNTH.Name = "gcolTenNTH"
        Me.gcolTenNTH.Visible = True
        Me.gcolTenNTH.VisibleIndex = 5
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
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Vị trí BD"
        Me.GridColumn2.FieldName = "tennvl"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'gcolSoKmKhiBD
        '
        Me.gcolSoKmKhiBD.Caption = "Số km khi BD"
        Me.gcolSoKmKhiBD.FieldName = "sokmkhibd"
        Me.gcolSoKmKhiBD.Name = "gcolSoKmKhiBD"
        Me.gcolSoKmKhiBD.Visible = True
        Me.gcolSoKmKhiBD.VisibleIndex = 2
        '
        'gcolChiphi
        '
        Me.gcolChiphi.AppearanceCell.Options.UseTextOptions = True
        Me.gcolChiphi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolChiphi.Caption = "Chi phí"
        Me.gcolChiphi.DisplayFormat.FormatString = "c0"
        Me.gcolChiphi.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gcolChiphi.FieldName = "chiphi"
        Me.gcolChiphi.Name = "gcolChiphi"
        Me.gcolChiphi.SummaryItem.DisplayFormat = "Tổng: {0:c0}"
        Me.gcolChiphi.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.gcolChiphi.Visible = True
        Me.gcolChiphi.VisibleIndex = 3
        '
        'gcolNgaysua
        '
        Me.gcolNgaysua.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgaysua.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgaysua.Caption = "Ngày sửa"
        Me.gcolNgaysua.FieldName = "ngaybaoduong"
        Me.gcolNgaysua.Name = "gcolNgaysua"
        Me.gcolNgaysua.Visible = True
        Me.gcolNgaysua.VisibleIndex = 4
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Ghi chú"
        Me.GridColumn1.FieldName = "ghichu"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 6
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem4), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barbtnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Thêm"
        Me.BarButtonItem4.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.BarButtonItem4.Id = 27
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Sửa"
        Me.BarButtonItem3.Glyph = CType(resources.GetObject("BarButtonItem3.Glyph"), System.Drawing.Image)
        Me.BarButtonItem3.Id = 21
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'barbtnXoa
        '
        Me.barbtnXoa.Caption = "Xóa"
        Me.barbtnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.barbtnXoa.Id = 22
        Me.barbtnXoa.Name = "barbtnXoa"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barLueXe, Me.barDeTuNgay, Me.BarEditItem2, Me.barDeDenNgay, Me.barCbxTuNgay, Me.barCbxDenngay, Me.BarButtonItem2, Me.BarButtonItem3, Me.barbtnXoa, Me.barCiLoc, Me.btnThem, Me.btnSua, Me.btnXoa, Me.BarButtonItem4})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 28
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.deTuNgay, Me.deDenNgay})
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueXe, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCiLoc, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
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
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Tải lại"
        Me.BarButtonItem2.Glyph = CType(resources.GetObject("BarButtonItem2.Glyph"), System.Drawing.Image)
        Me.BarButtonItem2.Id = 20
        Me.BarButtonItem2.Name = "BarButtonItem2"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(887, 59)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 347)
        Me.barDockControlBottom.Size = New System.Drawing.Size(887, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 59)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 288)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(887, 59)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 288)
        '
        'BarEditItem2
        '
        Me.BarEditItem2.Caption = "BarEditItem2"
        Me.BarEditItem2.Edit = Nothing
        Me.BarEditItem2.Id = 15
        Me.BarEditItem2.Name = "BarEditItem2"
        '
        'barCbxTuNgay
        '
        Me.barCbxTuNgay.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.barCbxTuNgay.Caption = "Từ ngày"
        Me.barCbxTuNgay.Id = 17
        Me.barCbxTuNgay.Name = "barCbxTuNgay"
        '
        'barCbxDenngay
        '
        Me.barCbxDenngay.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D
        Me.barCbxDenngay.Caption = "Đến ngày"
        Me.barCbxDenngay.Id = 18
        Me.barCbxDenngay.Name = "barCbxDenngay"
        '
        'frmBaoduongxe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 347)
        Me.Controls.Add(Me.gcBaoduongxe)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmBaoduongxe"
        Me.BarManager1.SetPopupContextMenu(Me, Me.PopupMenu1)
        Me.Text = "Bảo dưỡng xe"
        CType(Me.gcBaoduongxe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvBaoduongxe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcBaoduongxe As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvBaoduongxe As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcolID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcoIdXe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolSoKmKhiBD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolChiphi As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNgaysua As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenNTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolIDNTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents barLueXe As DevExpress.XtraBars.BarEditItem
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barCbxTuNgay As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents barDeTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barCbxDenngay As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents barDeDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarEditItem2 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents barbtnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barCiLoc As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents btnThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
End Class
