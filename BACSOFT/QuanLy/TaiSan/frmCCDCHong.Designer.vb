<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDCHong
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
        Me.gcCCDCHong = New DevExpress.XtraGrid.GridControl()
        Me.gvCCDCHong = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.btnThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.barLueCCDC = New DevExpress.XtraBars.BarEditItem()
        Me.riLueCCDC = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barLueChiTietCCDC = New DevExpress.XtraBars.BarEditItem()
        Me.riLueChiTietCCDC = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barGlueNSD = New DevExpress.XtraBars.BarEditItem()
        Me.riGlueNSD = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnTaiLai = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.RepositoryItemGridLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.gcCCDCHong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvCCDCHong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueCCDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueChiTietCCDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcCCDCHong
        '
        Me.gcCCDCHong.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcCCDCHong.Location = New System.Drawing.Point(0, 52)
        Me.gcCCDCHong.MainView = Me.gvCCDCHong
        Me.gcCCDCHong.Name = "gcCCDCHong"
        Me.gcCCDCHong.Size = New System.Drawing.Size(892, 302)
        Me.gcCCDCHong.TabIndex = 10
        Me.gcCCDCHong.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvCCDCHong})
        '
        'gvCCDCHong
        '
        Me.gvCCDCHong.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.Black
        Me.gvCCDCHong.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvCCDCHong.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvCCDCHong.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvCCDCHong.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCCDCHong.Appearance.FooterPanel.Options.UseFont = True
        Me.gvCCDCHong.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.gvCCDCHong.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvCCDCHong.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvCCDCHong.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvCCDCHong.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvCCDCHong.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvCCDCHong.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.gvCCDCHong.GridControl = Me.gcCCDCHong
        Me.gvCCDCHong.Name = "gvCCDCHong"
        Me.gvCCDCHong.OptionsBehavior.Editable = False
        Me.gvCCDCHong.OptionsCustomization.AllowColumnMoving = False
        Me.gvCCDCHong.OptionsCustomization.AllowGroup = False
        Me.gvCCDCHong.OptionsView.EnableAppearanceEvenRow = True
        Me.gvCCDCHong.OptionsView.ShowFooter = True
        Me.gvCCDCHong.OptionsView.ShowGroupPanel = False
        Me.gvCCDCHong.OptionsView.ShowIndicator = False
        Me.gvCCDCHong.RowHeight = 23
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "ID"
        Me.GridColumn4.FieldName = "id"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "ID người sử dụng"
        Me.GridColumn5.FieldName = "idnhansu"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "ID công cụ, dụng cụ"
        Me.GridColumn6.FieldName = "idccdc"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Người làm hỏng"
        Me.GridColumn1.FieldName = "Ten"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Công cụ, dụng cụ"
        Me.GridColumn2.FieldName = "tenchitietccdc"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Ngày hỏng"
        Me.GridColumn3.FieldName = "ngaysua"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Chi phí"
        Me.GridColumn7.DisplayFormat.FormatString = "c0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "chiphi"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.SummaryItem.DisplayFormat = "Tổng: {0:c0}"
        Me.GridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Ghi chú"
        Me.GridColumn8.FieldName = "ghichuhongccdc"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 4
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "id chi tiết ccdc"
        Me.GridColumn9.FieldName = "idchitieccdc"
        Me.GridColumn9.Name = "GridColumn9"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem3)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Thêm"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.BarButtonItem1.Id = 8
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Sửa"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.BarButtonItem2.Id = 9
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Xóa"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem3.Id = 10
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnThem, Me.btnSua, Me.btnXoa, Me.btnTaiLai, Me.barLueCCDC, Me.barGlueNSD, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.barLueChiTietCCDC})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 13
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riGlueNSD, Me.riLueCCDC, Me.RepositoryItemGridLookUpEdit1, Me.riLueChiTietCCDC})
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueCCDC, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueChiTietCCDC, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barGlueNSD, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnTaiLai, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'btnThem
        '
        Me.btnThem.Caption = "Thêm"
        Me.btnThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThem.Id = 0
        Me.btnThem.Name = "btnThem"
        '
        'btnSua
        '
        Me.btnSua.Caption = "Sửa"
        Me.btnSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btnSua.Id = 1
        Me.btnSua.Name = "btnSua"
        '
        'btnXoa
        '
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btnXoa.Id = 2
        Me.btnXoa.Name = "btnXoa"
        '
        'barLueCCDC
        '
        Me.barLueCCDC.Caption = "Công cụ, dụng cụ"
        Me.barLueCCDC.Edit = Me.riLueCCDC
        Me.barLueCCDC.Id = 6
        Me.barLueCCDC.Name = "barLueCCDC"
        Me.barLueCCDC.Width = 165
        '
        'riLueCCDC
        '
        Me.riLueCCDC.AutoHeight = False
        Me.riLueCCDC.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueCCDC.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ten", "Tên CCDC"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Mã CCDC")})
        Me.riLueCCDC.DisplayMember = "ten"
        Me.riLueCCDC.Name = "riLueCCDC"
        Me.riLueCCDC.NullText = "Tất cả"
        Me.riLueCCDC.ShowFooter = False
        Me.riLueCCDC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueCCDC.ValueMember = "id"
        '
        'barLueChiTietCCDC
        '
        Me.barLueChiTietCCDC.Caption = "Chi tiết CCDC"
        Me.barLueChiTietCCDC.Edit = Me.riLueChiTietCCDC
        Me.barLueChiTietCCDC.Id = 12
        Me.barLueChiTietCCDC.Name = "barLueChiTietCCDC"
        Me.barLueChiTietCCDC.Width = 174
        '
        'riLueChiTietCCDC
        '
        Me.riLueChiTietCCDC.AutoHeight = False
        Me.riLueChiTietCCDC.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueChiTietCCDC.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenchitietccdc", "Tên")})
        Me.riLueChiTietCCDC.DisplayMember = "tenchitietccdc"
        Me.riLueChiTietCCDC.Name = "riLueChiTietCCDC"
        Me.riLueChiTietCCDC.NullText = "Tất cả"
        Me.riLueChiTietCCDC.ShowHeader = False
        Me.riLueChiTietCCDC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueChiTietCCDC.ValueMember = "id"
        '
        'barGlueNSD
        '
        Me.barGlueNSD.Caption = "Người làm hỏng"
        Me.barGlueNSD.Edit = Me.riGlueNSD
        Me.barGlueNSD.Id = 7
        Me.barGlueNSD.Name = "barGlueNSD"
        Me.barGlueNSD.Width = 165
        '
        'riGlueNSD
        '
        Me.riGlueNSD.AutoHeight = False
        Me.riGlueNSD.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riGlueNSD.DisplayMember = "Ten"
        Me.riGlueNSD.Name = "riGlueNSD"
        Me.riGlueNSD.NullText = "Tất cả"
        Me.riGlueNSD.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riGlueNSD.ValueMember = "ID"
        Me.riGlueNSD.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = False
        '
        'btnTaiLai
        '
        Me.btnTaiLai.Caption = "Tải lại"
        Me.btnTaiLai.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btnTaiLai.Id = 3
        Me.btnTaiLai.Name = "btnTaiLai"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(892, 52)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 354)
        Me.barDockControlBottom.Size = New System.Drawing.Size(892, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 52)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 302)
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl1.Location = New System.Drawing.Point(892, 52)
        Me.BarDockControl1.Size = New System.Drawing.Size(0, 302)
        '
        'RepositoryItemGridLookUpEdit1
        '
        Me.RepositoryItemGridLookUpEdit1.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit1.Name = "RepositoryItemGridLookUpEdit1"
        Me.RepositoryItemGridLookUpEdit1.View = Me.GridView2
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'frmCCDCHong
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 354)
        Me.Controls.Add(Me.gcCCDCHong)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmCCDCHong"
        Me.BarManager1.SetPopupContextMenu(Me, Me.PopupMenu1)
        Me.Text = "Công cụ, dụng cụ hỏng"
        CType(Me.gcCCDCHong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvCCDCHong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueCCDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueChiTietCCDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcCCDCHong As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvCCDCHong As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents btnThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barLueCCDC As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueCCDC As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barLueChiTietCCDC As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueChiTietCCDC As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barGlueNSD As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riGlueNSD As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnTaiLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemGridLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
End Class
