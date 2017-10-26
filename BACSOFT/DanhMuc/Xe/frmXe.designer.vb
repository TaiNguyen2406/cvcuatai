<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmXe
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmXe))
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.btnXoa = New System.Windows.Forms.ToolStripMenuItem()
        Me.BảoDưỡngToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar5 = New DevExpress.XtraBars.Bar()
        Me.barRdgTinhTrangXe = New DevExpress.XtraBars.BarEditItem()
        Me.rdgTinhTrangxe = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnThem = New DevExpress.XtraBars.BarButtonItem()
        Me.gcXe = New DevExpress.XtraGrid.GridControl()
        Me.gvXe = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTinhTrang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmsMenu.SuspendLayout()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdgTinhTrangxe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmsMenu
        '
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnXoa, Me.BảoDưỡngToolStripMenuItem})
        Me.cmsMenu.Name = "cmsMenu"
        Me.cmsMenu.Size = New System.Drawing.Size(133, 48)
        '
        'btnXoa
        '
        Me.btnXoa.Image = CType(resources.GetObject("btnXoa.Image"), System.Drawing.Image)
        Me.btnXoa.Name = "btnXoa"
        Me.btnXoa.Size = New System.Drawing.Size(132, 22)
        Me.btnXoa.Text = "Xóa"
        '
        'BảoDưỡngToolStripMenuItem
        '
        Me.BảoDưỡngToolStripMenuItem.Image = CType(resources.GetObject("BảoDưỡngToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BảoDưỡngToolStripMenuItem.Name = "BảoDưỡngToolStripMenuItem"
        Me.BảoDưỡngToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.BảoDưỡngToolStripMenuItem.Text = "Bảo dưỡng"
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.DockCol = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.Text = "Status bar"
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.Text = "Main menu"
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.Text = "Tools"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar5})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem2, Me.barRdgTinhTrangXe, Me.BarButtonItem1, Me.btnThem, Me.BarButtonItem3})
        Me.BarManager1.MainMenu = Me.Bar5
        Me.BarManager1.MaxItemId = 7
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rdgTinhTrangxe})
        '
        'Bar5
        '
        Me.Bar5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar5.Appearance.Options.UseFont = True
        Me.Bar5.BarName = "Main menu"
        Me.Bar5.DockCol = 0
        Me.Bar5.DockRow = 0
        Me.Bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar5.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barRdgTinhTrangXe, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar5.OptionsBar.MultiLine = True
        Me.Bar5.OptionsBar.UseWholeRow = True
        Me.Bar5.Text = "Main menu"
        '
        'barRdgTinhTrangXe
        '
        Me.barRdgTinhTrangXe.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.barRdgTinhTrangXe.Appearance.Options.UseFont = True
        Me.barRdgTinhTrangXe.Caption = "Tình trạng xe"
        Me.barRdgTinhTrangXe.Edit = Me.rdgTinhTrangxe
        Me.barRdgTinhTrangXe.Glyph = CType(resources.GetObject("barRdgTinhTrangXe.Glyph"), System.Drawing.Image)
        Me.barRdgTinhTrangXe.Id = 2
        Me.barRdgTinhTrangXe.Name = "barRdgTinhTrangXe"
        Me.barRdgTinhTrangXe.Width = 433
        '
        'rdgTinhTrangxe
        '
        Me.rdgTinhTrangxe.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tất cả"), New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Đang sử dụng"), New DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Đang sửa chữa"), New DevExpress.XtraEditors.Controls.RadioGroupItem(3, "Bảo dưỡng")})
        Me.rdgTinhTrangxe.Name = "rdgTinhTrangxe"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Xóa"
        Me.BarButtonItem2.Glyph = CType(resources.GetObject("BarButtonItem2.Glyph"), System.Drawing.Image)
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Tải lại"
        Me.BarButtonItem1.Glyph = CType(resources.GetObject("BarButtonItem1.Glyph"), System.Drawing.Image)
        Me.BarButtonItem1.Id = 3
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Giấy tờ xe"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.ChaoGia_18
        Me.BarButtonItem3.Id = 5
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(913, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 370)
        Me.barDockControlBottom.Size = New System.Drawing.Size(913, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 344)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(913, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 344)
        '
        'btnThem
        '
        Me.btnThem.Caption = "Thêm mới"
        Me.btnThem.Id = 4
        Me.btnThem.Name = "btnThem"
        '
        'gcXe
        '
        Me.gcXe.ContextMenuStrip = Me.cmsMenu
        Me.gcXe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXe.Location = New System.Drawing.Point(0, 26)
        Me.gcXe.MainView = Me.gvXe
        Me.gcXe.MenuManager = Me.BarManager1
        Me.gcXe.Name = "gcXe"
        Me.gcXe.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riLueTinhTrang})
        Me.gcXe.Size = New System.Drawing.Size(913, 344)
        Me.gcXe.TabIndex = 10
        Me.gcXe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXe})
        '
        'gvXe
        '
        Me.gvXe.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvXe.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvXe.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXe.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvXe.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvXe.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvXe.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvXe.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvXe.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXe.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvXe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.gcolTenxe, Me.GridColumn1, Me.GridColumn7, Me.GridColumn3, Me.GridColumn5, Me.GridColumn6, Me.GridColumn4})
        Me.gvXe.FixedLineWidth = 1
        Me.gvXe.GridControl = Me.gcXe
        Me.gvXe.Name = "gvXe"
        Me.gvXe.OptionsBehavior.Editable = False
        Me.gvXe.OptionsCustomization.AllowColumnMoving = False
        Me.gvXe.OptionsCustomization.AllowGroup = False
        Me.gvXe.OptionsLayout.Columns.AddNewColumns = False
        Me.gvXe.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvXe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gvXe.OptionsView.ShowFooter = True
        Me.gvXe.OptionsView.ShowGroupPanel = False
        Me.gvXe.OptionsView.ShowIndicator = False
        Me.gvXe.RowHeight = 23
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn2.AppearanceCell.Options.UseFont = True
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Mã"
        Me.GridColumn2.FieldName = "id"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 119
        '
        'gcolTenxe
        '
        Me.gcolTenxe.Caption = "Tên xe"
        Me.gcolTenxe.FieldName = "tenxe"
        Me.gcolTenxe.Name = "gcolTenxe"
        Me.gcolTenxe.SummaryItem.DisplayFormat = "{0:N0}"
        Me.gcolTenxe.SummaryItem.FieldName = "Ten"
        Me.gcolTenxe.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolTenxe.Visible = True
        Me.gcolTenxe.VisibleIndex = 1
        Me.gcolTenxe.Width = 151
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Biển số"
        Me.GridColumn1.FieldName = "bienso"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 88
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tình trạng xe"
        Me.GridColumn7.ColumnEdit = Me.riLueTinhTrang
        Me.GridColumn7.FieldName = "id_tinhtrang"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 88
        '
        'riLueTinhTrang
        '
        Me.riLueTinhTrang.AutoHeight = False
        Me.riLueTinhTrang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTinhTrang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentinhtrang", "Tên")})
        Me.riLueTinhTrang.DisplayMember = "tentinhtrang"
        Me.riLueTinhTrang.Name = "riLueTinhTrang"
        Me.riLueTinhTrang.NullText = ""
        Me.riLueTinhTrang.ShowHeader = False
        Me.riLueTinhTrang.ValueMember = "id"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Đồng hồ km hiện tại"
        Me.GridColumn3.FieldName = "donghokmhientai"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Chỉ số hiện tại"
        Me.GridColumn5.FieldName = "chisohientai"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Width = 88
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Ghi chú"
        Me.GridColumn6.FieldName = "ghichu"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        Me.GridColumn6.Width = 88
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "id"
        Me.GridColumn4.FieldName = "id2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'frmXe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 370)
        Me.Controls.Add(Me.gcXe)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmXe"
        Me.Text = "Xe"
        Me.cmsMenu.ResumeLayout(False)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdgTinhTrangxe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents btnXoa As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BảoDưỡngToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar5 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barRdgTinhTrangXe As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rdgTinhTrangxe As DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Friend WithEvents gcXe As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvXe As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents riLueTinhTrang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
