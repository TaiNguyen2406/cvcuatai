<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHinhThucTT2
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
        Me.btXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.barChkHuy = New DevExpress.XtraBars.BarCheckItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btThem = New DevExpress.XtraBars.BarButtonItem()
        Me.pmThem = New DevExpress.XtraBars.BarButtonItem()
        Me.pmXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.pmLuuLai = New DevExpress.XtraBars.BarButtonItem()
        Me.btSua = New DevExpress.XtraBars.BarButtonItem()
        Me.mnu_Xoa = New DevExpress.XtraBars.BarButtonItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.gc = New DevExpress.XtraGrid.GridControl()
        Me.gv = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.gridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.BandedGridColumn6 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.riLueNhom = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridBand3 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand4 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn2 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand5 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn3 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand6 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.GridBand7 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn4 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand8 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn5 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand9 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn9 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand10 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn8 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.GridBand11 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.BandedGridColumn7 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.riChkTrangThai = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueNhom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riChkTrangThai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btThem, Me.pmThem, Me.pmXoa, Me.pmLuuLai, Me.btSua, Me.btXoa, Me.mnu_Xoa, Me.barChkHuy})
        Me.BarManager1.MaxItemId = 15
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1, Me.RepositoryItemCheckEdit1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btXoa), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barChkHuy, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'btXoa
        '
        Me.btXoa.Caption = "Xoá"
        Me.btXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoa.Id = 10
        Me.btXoa.Name = "btXoa"
        Me.btXoa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barChkHuy
        '
        Me.barChkHuy.Caption = "Xem HTTT hủy"
        Me.barChkHuy.Glyph = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.barChkHuy.Id = 14
        Me.barChkHuy.Name = "barChkHuy"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(870, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 383)
        Me.barDockControlBottom.Size = New System.Drawing.Size(870, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 350)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(870, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 350)
        '
        'btThem
        '
        Me.btThem.Caption = "Thêm"
        Me.btThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Id = 3
        Me.btThem.Name = "btThem"
        Me.btThem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'pmThem
        '
        Me.pmThem.Caption = "Thêm"
        Me.pmThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.pmThem.Id = 6
        Me.pmThem.Name = "pmThem"
        '
        'pmXoa
        '
        Me.pmXoa.Caption = "Xóa"
        Me.pmXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.pmXoa.Id = 7
        Me.pmXoa.Name = "pmXoa"
        '
        'pmLuuLai
        '
        Me.pmLuuLai.Caption = "Lưu lại"
        Me.pmLuuLai.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.pmLuuLai.Id = 8
        Me.pmLuuLai.Name = "pmLuuLai"
        '
        'btSua
        '
        Me.btSua.Caption = "Sửa"
        Me.btSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSua.Id = 9
        Me.btSua.Name = "btSua"
        Me.btSua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mnu_Xoa
        '
        Me.mnu_Xoa.Caption = "Xóa"
        Me.mnu_Xoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_24
        Me.mnu_Xoa.Id = 12
        Me.mnu_Xoa.Name = "mnu_Xoa"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'gc
        '
        Me.gc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc.Location = New System.Drawing.Point(0, 33)
        Me.gc.MainView = Me.gv
        Me.gc.MenuManager = Me.BarManager1
        Me.gc.Name = "gc"
        Me.gc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riLueNhom, Me.riChkTrangThai})
        Me.gc.Size = New System.Drawing.Size(870, 350)
        Me.gc.TabIndex = 4
        Me.gc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv})
        '
        'gv
        '
        Me.gv.Appearance.BandPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gv.Appearance.BandPanel.Options.UseFont = True
        Me.gv.Appearance.BandPanel.Options.UseTextOptions = True
        Me.gv.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gv.Appearance.BandPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.gv.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gv.Appearance.HeaderPanel.Options.UseFont = True
        Me.gv.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gridBand1, Me.GridBand2, Me.GridBand3, Me.GridBand6, Me.GridBand9, Me.GridBand10, Me.GridBand11})
        Me.gv.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.BandedGridColumn1, Me.BandedGridColumn2, Me.BandedGridColumn3, Me.BandedGridColumn4, Me.BandedGridColumn5, Me.BandedGridColumn6, Me.BandedGridColumn7, Me.BandedGridColumn8, Me.BandedGridColumn9})
        Me.gv.GridControl = Me.gc
        Me.gv.GroupCount = 1
        Me.gv.GroupFormat = "[#image]{1} {2}"
        Me.gv.GroupRowHeight = 25
        Me.gv.Name = "gv"
        Me.gv.NewItemRowText = "THÊM MỚI HÌNH THỨC THANH TOÁN"
        Me.gv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gv.OptionsBehavior.AutoExpandAllGroups = True
        Me.gv.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gv.OptionsNavigation.EnterMoveNextColumn = True
        Me.gv.OptionsView.ColumnAutoWidth = False
        Me.gv.OptionsView.EnableAppearanceEvenRow = True
        Me.gv.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
        Me.gv.OptionsView.ShowColumnHeaders = False
        Me.gv.OptionsView.ShowFooter = True
        Me.gv.OptionsView.ShowGroupPanel = False
        Me.gv.OptionsView.ShowIndicator = False
        Me.gv.RowHeight = 25
        Me.gv.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.BandedGridColumn6, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'gridBand1
        '
        Me.gridBand1.Caption = "ID"
        Me.gridBand1.Name = "gridBand1"
        Me.gridBand1.Visible = False
        Me.gridBand1.Width = 75
        '
        'GridBand2
        '
        Me.GridBand2.Caption = "Nhóm"
        Me.GridBand2.Columns.Add(Me.BandedGridColumn1)
        Me.GridBand2.Columns.Add(Me.BandedGridColumn6)
        Me.GridBand2.MinWidth = 20
        Me.GridBand2.Name = "GridBand2"
        Me.GridBand2.Width = 307
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.Caption = "ID"
        Me.BandedGridColumn1.FieldName = "ID"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        '
        'BandedGridColumn6
        '
        Me.BandedGridColumn6.Caption = "Nhóm"
        Me.BandedGridColumn6.ColumnEdit = Me.riLueNhom
        Me.BandedGridColumn6.FieldName = "Nhom"
        Me.BandedGridColumn6.GroupFormat.FormatString = "{0}"
        Me.BandedGridColumn6.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.BandedGridColumn6.Name = "BandedGridColumn6"
        Me.BandedGridColumn6.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.BandedGridColumn6.Visible = True
        Me.BandedGridColumn6.Width = 307
        '
        'riLueNhom
        '
        Me.riLueNhom.AutoHeight = False
        Me.riLueNhom.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueNhom.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenNhom", "Name2")})
        Me.riLueNhom.DisplayMember = "TenNhom"
        Me.riLueNhom.Name = "riLueNhom"
        Me.riLueNhom.NullText = "Chọn nhóm"
        Me.riLueNhom.ShowFooter = False
        Me.riLueNhom.ShowHeader = False
        Me.riLueNhom.ValueMember = "ID"
        '
        'GridBand3
        '
        Me.GridBand3.Caption = "Trả trước"
        Me.GridBand3.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand4, Me.GridBand5})
        Me.GridBand3.MinWidth = 20
        Me.GridBand3.Name = "GridBand3"
        Me.GridBand3.Width = 150
        '
        'GridBand4
        '
        Me.GridBand4.Caption = "Lần 1"
        Me.GridBand4.Columns.Add(Me.BandedGridColumn2)
        Me.GridBand4.Name = "GridBand4"
        Me.GridBand4.Width = 75
        '
        'BandedGridColumn2
        '
        Me.BandedGridColumn2.Caption = "Trước 1"
        Me.BandedGridColumn2.DisplayFormat.FormatString = "{0} %"
        Me.BandedGridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BandedGridColumn2.FieldName = "TraTruoc1"
        Me.BandedGridColumn2.Name = "BandedGridColumn2"
        Me.BandedGridColumn2.Visible = True
        '
        'GridBand5
        '
        Me.GridBand5.Caption = "Lần 2"
        Me.GridBand5.Columns.Add(Me.BandedGridColumn3)
        Me.GridBand5.Name = "GridBand5"
        Me.GridBand5.Width = 75
        '
        'BandedGridColumn3
        '
        Me.BandedGridColumn3.Caption = "Trước 2"
        Me.BandedGridColumn3.DisplayFormat.FormatString = "{0} %"
        Me.BandedGridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BandedGridColumn3.FieldName = "TraTruoc2"
        Me.BandedGridColumn3.Name = "BandedGridColumn3"
        Me.BandedGridColumn3.Visible = True
        '
        'GridBand6
        '
        Me.GridBand6.Caption = "Trả sau"
        Me.GridBand6.Children.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand7, Me.GridBand8})
        Me.GridBand6.MinWidth = 20
        Me.GridBand6.Name = "GridBand6"
        Me.GridBand6.Width = 150
        '
        'GridBand7
        '
        Me.GridBand7.Caption = "Lần 1"
        Me.GridBand7.Columns.Add(Me.BandedGridColumn4)
        Me.GridBand7.Name = "GridBand7"
        Me.GridBand7.Width = 75
        '
        'BandedGridColumn4
        '
        Me.BandedGridColumn4.Caption = "Sau 1"
        Me.BandedGridColumn4.DisplayFormat.FormatString = "{0} %"
        Me.BandedGridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BandedGridColumn4.FieldName = "TraSau1"
        Me.BandedGridColumn4.Name = "BandedGridColumn4"
        Me.BandedGridColumn4.Visible = True
        '
        'GridBand8
        '
        Me.GridBand8.Caption = "Lần 2"
        Me.GridBand8.Columns.Add(Me.BandedGridColumn5)
        Me.GridBand8.Name = "GridBand8"
        Me.GridBand8.Width = 75
        '
        'BandedGridColumn5
        '
        Me.BandedGridColumn5.Caption = "Sau 2"
        Me.BandedGridColumn5.DisplayFormat.FormatString = "{0} %"
        Me.BandedGridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BandedGridColumn5.FieldName = "TraSau2"
        Me.BandedGridColumn5.Name = "BandedGridColumn5"
        Me.BandedGridColumn5.Visible = True
        '
        'GridBand9
        '
        Me.GridBand9.Caption = "Số ngày HT"
        Me.GridBand9.Columns.Add(Me.BandedGridColumn9)
        Me.GridBand9.MinWidth = 20
        Me.GridBand9.Name = "GridBand9"
        Me.GridBand9.Width = 58
        '
        'BandedGridColumn9
        '
        Me.BandedGridColumn9.Caption = "Số Ngày HT"
        Me.BandedGridColumn9.FieldName = "SoNgayHT"
        Me.BandedGridColumn9.Name = "BandedGridColumn9"
        Me.BandedGridColumn9.Visible = True
        Me.BandedGridColumn9.Width = 58
        '
        'GridBand10
        '
        Me.GridBand10.Caption = "Giải thích"
        Me.GridBand10.Columns.Add(Me.BandedGridColumn8)
        Me.GridBand10.MinWidth = 20
        Me.GridBand10.Name = "GridBand10"
        Me.GridBand10.Width = 595
        '
        'BandedGridColumn8
        '
        Me.BandedGridColumn8.Caption = "Giải thích"
        Me.BandedGridColumn8.FieldName = "GiaiThich"
        Me.BandedGridColumn8.Name = "BandedGridColumn8"
        Me.BandedGridColumn8.Visible = True
        Me.BandedGridColumn8.Width = 595
        '
        'GridBand11
        '
        Me.GridBand11.Caption = "Áp dụng"
        Me.GridBand11.Columns.Add(Me.BandedGridColumn7)
        Me.GridBand11.MinWidth = 20
        Me.GridBand11.Name = "GridBand11"
        Me.GridBand11.Width = 39
        '
        'BandedGridColumn7
        '
        Me.BandedGridColumn7.Caption = "Trạng thái"
        Me.BandedGridColumn7.ColumnEdit = Me.riChkTrangThai
        Me.BandedGridColumn7.FieldName = "TrangThai"
        Me.BandedGridColumn7.Name = "BandedGridColumn7"
        Me.BandedGridColumn7.Visible = True
        Me.BandedGridColumn7.Width = 39
        '
        'riChkTrangThai
        '
        Me.riChkTrangThai.AutoHeight = False
        Me.riChkTrangThai.Name = "riChkTrangThai"
        Me.riChkTrangThai.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.mnu_Xoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'frmHinhThucTT2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gc)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmHinhThucTT2"
        Me.Size = New System.Drawing.Size(870, 383)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueNhom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riChkTrangThai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents btThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents pmThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pmXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pmLuuLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents gc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents BandedGridColumn6 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents riLueNhom As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn3 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn4 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn5 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn9 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn8 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn7 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents riChkTrangThai As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents mnu_Xoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents barChkHuy As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand3 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand4 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand5 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand6 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand7 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand8 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand9 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand10 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents GridBand11 As DevExpress.XtraGrid.Views.BandedGrid.GridBand

End Class
