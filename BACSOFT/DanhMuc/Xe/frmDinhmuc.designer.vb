<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDinhmuc
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
        Me.gcDinhmuc = New DevExpress.XtraGrid.GridControl()
        Me.gvDinhmuc = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolID_xe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolID_nhienlieu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolBienSo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolDinhmuc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTennhienvatlieu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLeuXe = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolGhichu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.barLueXe = New DevExpress.XtraBars.BarEditItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.BarStaticItem2 = New DevExpress.XtraBars.BarStaticItem()
        Me.barTxtDinhMuc = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarStaticItem4 = New DevExpress.XtraBars.BarStaticItem()
        Me.BarStaticItem3 = New DevExpress.XtraBars.BarStaticItem()
        Me.barLueNhienVatLieu = New DevExpress.XtraBars.BarEditItem()
        Me.lueNhienVatLieu = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnTailai = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.gcDinhmuc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDinhmuc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.colLeuXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueNhienVatLieu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcDinhmuc
        '
        Me.gcDinhmuc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDinhmuc.Location = New System.Drawing.Point(0, 26)
        Me.gcDinhmuc.MainView = Me.gvDinhmuc
        Me.gcDinhmuc.Name = "gcDinhmuc"
        Me.gcDinhmuc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.colLeuXe})
        Me.gcDinhmuc.Size = New System.Drawing.Size(877, 359)
        Me.gcDinhmuc.TabIndex = 60
        Me.gcDinhmuc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDinhmuc})
        '
        'gvDinhmuc
        '
        Me.gvDinhmuc.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvDinhmuc.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvDinhmuc.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvDinhmuc.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvDinhmuc.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDinhmuc.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvDinhmuc.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvDinhmuc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvDinhmuc.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvDinhmuc.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvDinhmuc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolID, Me.gcolID_xe, Me.gcolID_nhienlieu, Me.gcolTenxe, Me.gcolBienSo, Me.gcolDinhmuc, Me.gcolTennhienvatlieu, Me.GridColumn1})
        Me.gvDinhmuc.FixedLineWidth = 1
        Me.gvDinhmuc.GridControl = Me.gcDinhmuc
        Me.gvDinhmuc.Name = "gvDinhmuc"
        Me.gvDinhmuc.OptionsCustomization.AllowColumnMoving = False
        Me.gvDinhmuc.OptionsCustomization.AllowGroup = False
        Me.gvDinhmuc.OptionsView.EnableAppearanceEvenRow = True
        Me.gvDinhmuc.OptionsView.ShowFooter = True
        Me.gvDinhmuc.OptionsView.ShowGroupPanel = False
        Me.gvDinhmuc.OptionsView.ShowIndicator = False
        Me.gvDinhmuc.RowHeight = 23
        '
        'gcolID
        '
        Me.gcolID.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gcolID.AppearanceCell.Options.UseFont = True
        Me.gcolID.AppearanceCell.Options.UseTextOptions = True
        Me.gcolID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolID.Caption = "ID"
        Me.gcolID.FieldName = "id"
        Me.gcolID.Name = "gcolID"
        '
        'gcolID_xe
        '
        Me.gcolID_xe.Caption = "ID xe"
        Me.gcolID_xe.FieldName = "id_xe"
        Me.gcolID_xe.Name = "gcolID_xe"
        '
        'gcolID_nhienlieu
        '
        Me.gcolID_nhienlieu.Caption = "Nhiên liệu"
        Me.gcolID_nhienlieu.FieldName = "id_nhienvatlieu"
        Me.gcolID_nhienlieu.Name = "gcolID_nhienlieu"
        '
        'gcolTenxe
        '
        Me.gcolTenxe.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gcolTenxe.AppearanceCell.Options.UseFont = True
        Me.gcolTenxe.Caption = "Xe"
        Me.gcolTenxe.FieldName = "tenxe"
        Me.gcolTenxe.Name = "gcolTenxe"
        Me.gcolTenxe.OptionsColumn.AllowEdit = False
        Me.gcolTenxe.SummaryItem.DisplayFormat = "{0:N0}"
        Me.gcolTenxe.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolTenxe.Visible = True
        Me.gcolTenxe.VisibleIndex = 0
        Me.gcolTenxe.Width = 145
        '
        'gcolBienSo
        '
        Me.gcolBienSo.AppearanceCell.Options.UseTextOptions = True
        Me.gcolBienSo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolBienSo.Caption = "Biển số"
        Me.gcolBienSo.FieldName = "bienso"
        Me.gcolBienSo.Name = "gcolBienSo"
        Me.gcolBienSo.OptionsColumn.AllowEdit = False
        Me.gcolBienSo.Visible = True
        Me.gcolBienSo.VisibleIndex = 1
        Me.gcolBienSo.Width = 190
        '
        'gcolDinhmuc
        '
        Me.gcolDinhmuc.AppearanceCell.Options.UseTextOptions = True
        Me.gcolDinhmuc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolDinhmuc.Caption = "Định mức (đơn vị Km)"
        Me.gcolDinhmuc.FieldName = "dinhmuc"
        Me.gcolDinhmuc.Name = "gcolDinhmuc"
        Me.gcolDinhmuc.OptionsColumn.AllowEdit = False
        Me.gcolDinhmuc.Visible = True
        Me.gcolDinhmuc.VisibleIndex = 2
        Me.gcolDinhmuc.Width = 185
        '
        'gcolTennhienvatlieu
        '
        Me.gcolTennhienvatlieu.AppearanceCell.Options.UseTextOptions = True
        Me.gcolTennhienvatlieu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolTennhienvatlieu.Caption = "Thay"
        Me.gcolTennhienvatlieu.FieldName = "tennhienvatlieu"
        Me.gcolTennhienvatlieu.Name = "gcolTennhienvatlieu"
        Me.gcolTennhienvatlieu.OptionsColumn.AllowEdit = False
        Me.gcolTennhienvatlieu.Visible = True
        Me.gcolTennhienvatlieu.VisibleIndex = 3
        Me.gcolTennhienvatlieu.Width = 169
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Chỉ số theo dõi"
        Me.GridColumn1.FieldName = "chisohientai"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 4
        Me.GridColumn1.Width = 170
        '
        'colLeuXe
        '
        Me.colLeuXe.AutoHeight = False
        Me.colLeuXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.colLeuXe.DisplayMember = "tenxe"
        Me.colLeuXe.Name = "colLeuXe"
        Me.colLeuXe.ValueMember = "id"
        Me.colLeuXe.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'gcolGhichu
        '
        Me.gcolGhichu.Caption = "Ghi chú"
        Me.gcolGhichu.FieldName = "ghichu_tinhtrang"
        Me.gcolGhichu.Name = "gcolGhichu"
        Me.gcolGhichu.Visible = True
        Me.gcolGhichu.VisibleIndex = 0
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2, Me.barLueXe, Me.barLueNhienVatLieu, Me.barTxtDinhMuc, Me.BarStaticItem1, Me.BarStaticItem2, Me.BarStaticItem3, Me.BarStaticItem4, Me.BarButtonItem3, Me.btnTailai})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 14
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.lueNhienVatLieu, Me.RepositoryItemTextEdit1})
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1, True), New DevExpress.XtraBars.LinkPersistInfo(Me.barLueXe), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.barTxtDinhMuc), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.barLueNhienVatLieu), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem1, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnTailai, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarStaticItem1.Appearance.Options.UseFont = True
        Me.BarStaticItem1.Caption = "Chọn xe"
        Me.BarStaticItem1.Id = 8
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barLueXe
        '
        Me.barLueXe.Caption = "Chọn xe"
        Me.barLueXe.Edit = Me.lueXe
        Me.barLueXe.EditValue = ""
        Me.barLueXe.Id = 5
        Me.barLueXe.Name = "barLueXe"
        Me.barLueXe.Width = 138
        '
        'lueXe
        '
        Me.lueXe.AutoHeight = False
        Me.lueXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueXe.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Tên xe"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Mã xe", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueXe.DisplayMember = "tenxe"
        Me.lueXe.Name = "lueXe"
        Me.lueXe.ValueMember = "id"
        '
        'BarStaticItem2
        '
        Me.BarStaticItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarStaticItem2.Appearance.Options.UseFont = True
        Me.BarStaticItem2.Caption = "Định mức"
        Me.BarStaticItem2.Id = 9
        Me.BarStaticItem2.Name = "BarStaticItem2"
        Me.BarStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barTxtDinhMuc
        '
        Me.barTxtDinhMuc.Caption = "Định mức"
        Me.barTxtDinhMuc.Edit = Me.RepositoryItemTextEdit1
        Me.barTxtDinhMuc.Id = 7
        Me.barTxtDinhMuc.Name = "barTxtDinhMuc"
        Me.barTxtDinhMuc.Width = 78
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'BarStaticItem4
        '
        Me.BarStaticItem4.Caption = "(Km)   "
        Me.BarStaticItem4.Id = 11
        Me.BarStaticItem4.Name = "BarStaticItem4"
        Me.BarStaticItem4.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'BarStaticItem3
        '
        Me.BarStaticItem3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarStaticItem3.Appearance.Options.UseFont = True
        Me.BarStaticItem3.Caption = "Thay thế"
        Me.BarStaticItem3.Id = 10
        Me.BarStaticItem3.Name = "BarStaticItem3"
        Me.BarStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barLueNhienVatLieu
        '
        Me.barLueNhienVatLieu.Caption = "Nhiên vật liệu thay thế"
        Me.barLueNhienVatLieu.Edit = Me.lueNhienVatLieu
        Me.barLueNhienVatLieu.EditValue = ""
        Me.barLueNhienVatLieu.Id = 6
        Me.barLueNhienVatLieu.Name = "barLueNhienVatLieu"
        Me.barLueNhienVatLieu.Width = 136
        '
        'lueNhienVatLieu
        '
        Me.lueNhienVatLieu.AutoHeight = False
        Me.lueNhienVatLieu.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueNhienVatLieu.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tennhienvatlieu", "Thay thế"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Mã nvl", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueNhienVatLieu.DisplayMember = "tennhienvatlieu"
        Me.lueNhienVatLieu.Name = "lueNhienVatLieu"
        Me.lueNhienVatLieu.ValueMember = "id"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem1.Appearance.Options.UseFont = True
        Me.BarButtonItem1.Caption = "Lưu"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Xóa"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'btnTailai
        '
        Me.btnTailai.Caption = "Tải lại"
        Me.btnTailai.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btnTailai.Id = 13
        Me.btnTailai.Name = "btnTailai"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(877, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 385)
        Me.barDockControlBottom.Size = New System.Drawing.Size(877, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 359)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(877, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 359)
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Bảo dưỡng"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.Config_18
        Me.BarButtonItem3.Id = 12
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'frmDinhmuc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 385)
        Me.Controls.Add(Me.gcDinhmuc)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmDinhmuc"
        Me.BarManager1.SetPopupContextMenu(Me, Me.PopupMenu1)
        Me.Text = "Định mức xe"
        CType(Me.gcDinhmuc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDinhmuc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.colLeuXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueNhienVatLieu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcDinhmuc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDinhmuc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcolID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolID_xe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolID_nhienlieu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTennhienvatlieu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolDinhmuc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolGhichu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents colLeuXe As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents barLueXe As DevExpress.XtraBars.BarEditItem
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barTxtDinhMuc As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents barLueNhienVatLieu As DevExpress.XtraBars.BarEditItem
    Friend WithEvents lueNhienVatLieu As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticItem2 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarStaticItem3 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents gcolBienSo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarStaticItem4 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents btnTailai As DevExpress.XtraBars.BarButtonItem
End Class
