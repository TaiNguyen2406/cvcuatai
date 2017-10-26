<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuyenTruyCap
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
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.gdvNhomNgDung = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhomNgDungCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.treePQ = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.TreeListColumn3 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn4 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn5 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn6 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn7 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn8 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn9 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn10 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.pMenu = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.mLuuLai = New DevExpress.XtraBars.BarButtonItem()
        Me.mChonToanBoHang = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.btChonHet = New DevExpress.XtraBars.BarButtonItem()
        Me.btLuuLai = New DevExpress.XtraBars.BarButtonItem()
        Me.btXemBangPhanQuyen = New DevExpress.XtraBars.BarButtonItem()
        Me.TreeListColumn11 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.gdvNhomNgDung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhomNgDungCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.treePQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1123, 628)
        Me.SplitContainerControl1.SplitterPosition = 257
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.gdvNhomNgDung)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(257, 628)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Nhóm người dùng"
        '
        'gdvNhomNgDung
        '
        Me.gdvNhomNgDung.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNhomNgDung.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gdvNhomNgDung.Location = New System.Drawing.Point(2, 25)
        Me.gdvNhomNgDung.MainView = Me.gdvNhomNgDungCT
        Me.gdvNhomNgDung.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gdvNhomNgDung.Name = "gdvNhomNgDung"
        Me.gdvNhomNgDung.Size = New System.Drawing.Size(253, 601)
        Me.gdvNhomNgDung.TabIndex = 0
        Me.gdvNhomNgDung.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhomNgDungCT})
        '
        'gdvNhomNgDungCT
        '
        Me.gdvNhomNgDungCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhomNgDungCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNhomNgDungCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhomNgDungCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNhomNgDungCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhomNgDungCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhomNgDungCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhomNgDungCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhomNgDungCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhomNgDungCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvNhomNgDungCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.gdvNhomNgDungCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvNhomNgDungCT.GridControl = Me.gdvNhomNgDung
        Me.gdvNhomNgDungCT.Name = "gdvNhomNgDungCT"
        Me.gdvNhomNgDungCT.OptionsBehavior.Editable = False
        Me.gdvNhomNgDungCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvNhomNgDungCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhomNgDungCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvNhomNgDungCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhomNgDungCT.RowHeight = 22
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Mã truy cập"
        Me.GridColumn1.FieldName = "MaTruyCap"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "ID"
        Me.GridColumn2.FieldName = "ID"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Quyền"
        Me.GridColumn3.FieldName = "Quyen"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.treePQ)
        Me.GroupControl2.Controls.Add(Me.barDockControlLeft)
        Me.GroupControl2.Controls.Add(Me.barDockControlRight)
        Me.GroupControl2.Controls.Add(Me.barDockControlBottom)
        Me.GroupControl2.Controls.Add(Me.barDockControlTop)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.BarManager1.SetPopupContextMenu(Me.GroupControl2, Me.pMenu)
        Me.GroupControl2.Size = New System.Drawing.Size(861, 628)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Quyền truy cập"
        '
        'treePQ
        '
        Me.treePQ.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.treePQ.Appearance.FocusedRow.Options.UseBackColor = True
        Me.treePQ.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn1, Me.TreeListColumn2, Me.TreeListColumn3, Me.TreeListColumn4, Me.TreeListColumn5, Me.TreeListColumn6, Me.TreeListColumn7, Me.TreeListColumn8, Me.TreeListColumn9, Me.TreeListColumn10, Me.TreeListColumn11})
        Me.treePQ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treePQ.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treePQ.Location = New System.Drawing.Point(2, 62)
        Me.treePQ.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.treePQ.Name = "treePQ"
        Me.treePQ.OptionsBehavior.PopulateServiceColumns = True
        Me.treePQ.OptionsSelection.MultiSelect = True
        Me.treePQ.OptionsView.AutoWidth = False
        Me.treePQ.OptionsView.EnableAppearanceEvenRow = True
        Me.treePQ.OptionsView.ShowCheckBoxes = True
        Me.treePQ.OptionsView.ShowHorzLines = False
        Me.treePQ.OptionsView.ShowIndicator = False
        Me.treePQ.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.treePQ.Size = New System.Drawing.Size(857, 564)
        Me.treePQ.TabIndex = 12
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn1.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn1.Caption = "Các chức năng chính"
        Me.TreeListColumn1.FieldName = "Phân quyền"
        Me.TreeListColumn1.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left
        Me.TreeListColumn1.MinWidth = 32
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.OptionsColumn.AllowEdit = False
        Me.TreeListColumn1.OptionsColumn.AllowSort = False
        Me.TreeListColumn1.OptionsColumn.FixedWidth = True
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        Me.TreeListColumn1.Width = 218
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn2.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn2.Caption = "Quyền Thêm"
        Me.TreeListColumn2.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn2.FieldName = "Quyền Sửa"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.OptionsColumn.AllowSort = False
        Me.TreeListColumn2.OptionsColumn.FixedWidth = True
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 1
        Me.TreeListColumn2.Width = 80
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.RepositoryItemCheckEdit1.ValueGrayed = False
        '
        'TreeListColumn3
        '
        Me.TreeListColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn3.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn3.Caption = "Quyền Sửa"
        Me.TreeListColumn3.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn3.FieldName = "Quyền sửa"
        Me.TreeListColumn3.Name = "TreeListColumn3"
        Me.TreeListColumn3.OptionsColumn.AllowSort = False
        Me.TreeListColumn3.OptionsColumn.FixedWidth = True
        Me.TreeListColumn3.Visible = True
        Me.TreeListColumn3.VisibleIndex = 2
        '
        'TreeListColumn4
        '
        Me.TreeListColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn4.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn4.Caption = "Quyền Xoá"
        Me.TreeListColumn4.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn4.FieldName = "Quyền xoá"
        Me.TreeListColumn4.Name = "TreeListColumn4"
        Me.TreeListColumn4.OptionsColumn.AllowSort = False
        Me.TreeListColumn4.OptionsColumn.FixedWidth = True
        Me.TreeListColumn4.Visible = True
        Me.TreeListColumn4.VisibleIndex = 3
        Me.TreeListColumn4.Width = 71
        '
        'TreeListColumn5
        '
        Me.TreeListColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn5.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn5.Caption = "TP Quản Lý"
        Me.TreeListColumn5.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn5.FieldName = "TP Quản Lý"
        Me.TreeListColumn5.Name = "TreeListColumn5"
        Me.TreeListColumn5.OptionsColumn.AllowSort = False
        Me.TreeListColumn5.Visible = True
        Me.TreeListColumn5.VisibleIndex = 4
        Me.TreeListColumn5.Width = 79
        '
        'TreeListColumn6
        '
        Me.TreeListColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn6.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn6.Caption = "TP Kinh Doanh"
        Me.TreeListColumn6.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn6.FieldName = "TP Kinh Doanh"
        Me.TreeListColumn6.Name = "TreeListColumn6"
        Me.TreeListColumn6.OptionsColumn.AllowSort = False
        Me.TreeListColumn6.Visible = True
        Me.TreeListColumn6.VisibleIndex = 5
        Me.TreeListColumn6.Width = 94
        '
        'TreeListColumn7
        '
        Me.TreeListColumn7.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn7.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn7.Caption = "TP Kỹ Thuật"
        Me.TreeListColumn7.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn7.FieldName = "TP Kỹ Thuật"
        Me.TreeListColumn7.Name = "TreeListColumn7"
        Me.TreeListColumn7.OptionsColumn.AllowSort = False
        Me.TreeListColumn7.Visible = True
        Me.TreeListColumn7.VisibleIndex = 6
        Me.TreeListColumn7.Width = 77
        '
        'TreeListColumn8
        '
        Me.TreeListColumn8.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn8.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn8.Caption = "Kiểm Duyệt"
        Me.TreeListColumn8.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn8.FieldName = "Kiểm Duyệt"
        Me.TreeListColumn8.Name = "TreeListColumn8"
        Me.TreeListColumn8.OptionsColumn.AllowSort = False
        Me.TreeListColumn8.Visible = True
        Me.TreeListColumn8.VisibleIndex = 7
        Me.TreeListColumn8.Width = 85
        '
        'TreeListColumn9
        '
        Me.TreeListColumn9.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn9.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn9.Caption = "Ban QT"
        Me.TreeListColumn9.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn9.FieldName = "Ban QT"
        Me.TreeListColumn9.Name = "TreeListColumn9"
        Me.TreeListColumn9.OptionsColumn.AllowSort = False
        Me.TreeListColumn9.Visible = True
        Me.TreeListColumn9.VisibleIndex = 8
        Me.TreeListColumn9.Width = 52
        '
        'TreeListColumn10
        '
        Me.TreeListColumn10.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn10.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn10.Caption = "Admin"
        Me.TreeListColumn10.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn10.FieldName = "Admin"
        Me.TreeListColumn10.Name = "TreeListColumn10"
        Me.TreeListColumn10.OptionsColumn.AllowSort = False
        Me.TreeListColumn10.Visible = True
        Me.TreeListColumn10.VisibleIndex = 9
        Me.TreeListColumn10.Width = 50
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(2, 62)
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 564)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(859, 62)
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 564)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(2, 626)
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(857, 0)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(2, 25)
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(857, 37)
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me.GroupControl2
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mLuuLai, Me.btLuuLai, Me.btChonHet, Me.mChonToanBoHang, Me.btXemBangPhanQuyen})
        Me.BarManager1.MaxItemId = 6
        '
        'pMenu
        '
        Me.pMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mLuuLai), New DevExpress.XtraBars.LinkPersistInfo(Me.mChonToanBoHang)})
        Me.pMenu.Manager = Me.BarManager1
        Me.pMenu.Name = "pMenu"
        '
        'mLuuLai
        '
        Me.mLuuLai.Caption = "Lưu lại"
        Me.mLuuLai.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.mLuuLai.Id = 0
        Me.mLuuLai.Name = "mLuuLai"
        '
        'mChonToanBoHang
        '
        Me.mChonToanBoHang.Caption = "Đánh dấu/bỏ toàn bộ hàng"
        Me.mChonToanBoHang.Glyph = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.mChonToanBoHang.Id = 3
        Me.mChonToanBoHang.Name = "mChonToanBoHang"
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btChonHet), New DevExpress.XtraBars.LinkPersistInfo(Me.btLuuLai), New DevExpress.XtraBars.LinkPersistInfo(Me.btXemBangPhanQuyen)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Custom 2"
        '
        'btChonHet
        '
        Me.btChonHet.Caption = "Chọn/Bỏ chọn toàn bộ"
        Me.btChonHet.Glyph = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.btChonHet.Id = 2
        Me.btChonHet.Name = "btChonHet"
        Me.btChonHet.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btLuuLai
        '
        Me.btLuuLai.Caption = "Lưu lại"
        Me.btLuuLai.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Id = 1
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXemBangPhanQuyen
        '
        Me.btXemBangPhanQuyen.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.btXemBangPhanQuyen.Caption = "Xem bảng phân quyền"
        Me.btXemBangPhanQuyen.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btXemBangPhanQuyen.Id = 5
        Me.btXemBangPhanQuyen.Name = "btXemBangPhanQuyen"
        Me.btXemBangPhanQuyen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'TreeListColumn11
        '
        Me.TreeListColumn11.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn11.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn11.Caption = "Kế toán"
        Me.TreeListColumn11.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.TreeListColumn11.FieldName = "KeToan"
        Me.TreeListColumn11.Name = "TreeListColumn11"
        Me.TreeListColumn11.Visible = True
        Me.TreeListColumn11.VisibleIndex = 10
        Me.TreeListColumn11.Width = 65
        '
        'frmQuyenTruyCap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmQuyenTruyCap"
        Me.Size = New System.Drawing.Size(1123, 628)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.gdvNhomNgDung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhomNgDungCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.treePQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents treePQ As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents gdvNhomNgDung As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNhomNgDungCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents TreeListColumn3 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn4 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn5 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents pMenu As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mLuuLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents btLuuLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents TreeListColumn6 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn7 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn8 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn9 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn10 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents btChonHet As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mChonToanBoHang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXemBangPhanQuyen As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents TreeListColumn11 As DevExpress.XtraTreeList.Columns.TreeListColumn

End Class
