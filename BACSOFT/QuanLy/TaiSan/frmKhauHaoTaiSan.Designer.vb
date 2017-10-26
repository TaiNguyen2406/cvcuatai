<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKhauHaoTaiSan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKhauHaoTaiSan))
        Me.gcKhauHaoTs = New DevExpress.XtraGrid.GridControl()
        Me.gvKhauHaoTs = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BandedGridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.barCbbThang = New DevExpress.XtraBars.BarEditItem()
        Me.riCbbThang = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.barSeNam = New DevExpress.XtraBars.BarEditItem()
        Me.riSeNam = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.barCiLoc = New DevExpress.XtraBars.BarCheckItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.chkXemHet = New DevExpress.XtraBars.BarCheckItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barTxtMaVT = New DevExpress.XtraBars.BarEditItem()
        Me.riTxtMaVT = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnChiTietTaiSan = New DevExpress.XtraBars.BarButtonItem()
        Me.barLueNhomVT = New DevExpress.XtraBars.BarEditItem()
        Me.riLueNhomVT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barLueHang = New DevExpress.XtraBars.BarEditItem()
        Me.riLueHang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barLueTenVT = New DevExpress.XtraBars.BarEditItem()
        Me.riLueTenVT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.riCbbTrangThai = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.meKhauHaoTS = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.gcKhauHaoTs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvKhauHaoTs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbThang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riSeNam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riTxtMaVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueNhomVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueHang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTenVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbTrangThai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.meKhauHaoTS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcKhauHaoTs
        '
        Me.gcKhauHaoTs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcKhauHaoTs.Location = New System.Drawing.Point(0, 26)
        Me.gcKhauHaoTs.MainView = Me.gvKhauHaoTs
        Me.gcKhauHaoTs.Name = "gcKhauHaoTs"
        Me.gcKhauHaoTs.Size = New System.Drawing.Size(1283, 318)
        Me.gcKhauHaoTs.TabIndex = 5
        Me.gcKhauHaoTs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvKhauHaoTs})
        '
        'gvKhauHaoTs
        '
        Me.gvKhauHaoTs.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.Black
        Me.gvKhauHaoTs.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvKhauHaoTs.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvKhauHaoTs.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvKhauHaoTs.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.gvKhauHaoTs.Appearance.FooterPanel.Options.UseFont = True
        Me.gvKhauHaoTs.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.gvKhauHaoTs.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.gvKhauHaoTs.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvKhauHaoTs.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvKhauHaoTs.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvKhauHaoTs.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvKhauHaoTs.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gvKhauHaoTs.ColumnPanelRowHeight = 40
        Me.gvKhauHaoTs.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn4, Me.GridColumn10, Me.GridColumn14, Me.GridColumn11, Me.BandedGridColumn2, Me.BandedGridColumn1, Me.GridColumn12, Me.GridColumn13, Me.GridColumn5, Me.GridColumn8, Me.GridColumn9, Me.GridColumn3, Me.GridColumn6, Me.GridColumn7})
        Me.gvKhauHaoTs.GridControl = Me.gcKhauHaoTs
        Me.gvKhauHaoTs.Name = "gvKhauHaoTs"
        Me.gvKhauHaoTs.OptionsBehavior.Editable = False
        Me.gvKhauHaoTs.OptionsCustomization.AllowGroup = False
        Me.gvKhauHaoTs.OptionsCustomization.AllowQuickHideColumns = False
        Me.gvKhauHaoTs.OptionsView.AllowHtmlDrawHeaders = True
        Me.gvKhauHaoTs.OptionsView.ColumnAutoWidth = False
        Me.gvKhauHaoTs.OptionsView.EnableAppearanceEvenRow = True
        Me.gvKhauHaoTs.OptionsView.ShowFooter = True
        Me.gvKhauHaoTs.OptionsView.ShowGroupPanel = False
        Me.gvKhauHaoTs.OptionsView.ShowIndicator = False
        Me.gvKhauHaoTs.RowHeight = 23
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Mã"
        Me.GridColumn1.FieldName = "id"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.SummaryItem.DisplayFormat = "Số tài sản: {0}"
        Me.GridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn1.Width = 139
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tên tài sản"
        Me.GridColumn2.FieldName = "TenVT"
        Me.GridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 124
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Đơn giá"
        Me.GridColumn4.DisplayFormat.FormatString = "c0"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "DonGia"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 108
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.Caption = "Số lượng"
        Me.GridColumn10.FieldName = "SoLuong"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 3
        Me.GridColumn10.Width = 47
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn14.Caption = "SL còn lại"
        Me.GridColumn14.FieldName = "SoLuongThuc"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 5
        Me.GridColumn14.Width = 48
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn11.Caption = "Tổng tiền"
        Me.GridColumn11.DisplayFormat.FormatString = "c0"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn11.FieldName = "tongtien"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.SummaryItem.DisplayFormat = "{0:c0}"
        Me.GridColumn11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 4
        Me.GridColumn11.Width = 127
        '
        'BandedGridColumn2
        '
        Me.BandedGridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.BandedGridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.BandedGridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.BandedGridColumn2.Caption = "Thời gian KH(tháng)"
        Me.BandedGridColumn2.FieldName = "thoigiankh"
        Me.BandedGridColumn2.Name = "BandedGridColumn2"
        Me.BandedGridColumn2.Visible = True
        Me.BandedGridColumn2.VisibleIndex = 6
        Me.BandedGridColumn2.Width = 74
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.BandedGridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BandedGridColumn1.Caption = "Mức KH(ngày)"
        Me.BandedGridColumn1.DisplayFormat.FormatString = "c0"
        Me.BandedGridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.BandedGridColumn1.FieldName = "muckh"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        Me.BandedGridColumn1.Visible = True
        Me.BandedGridColumn1.VisibleIndex = 7
        Me.BandedGridColumn1.Width = 91
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "KH tháng"
        Me.GridColumn12.DisplayFormat.FormatString = "c0"
        Me.GridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn12.FieldName = "khthang"
        Me.GridColumn12.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.SummaryItem.DisplayFormat = "{0:c0}"
        Me.GridColumn12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 8
        Me.GridColumn12.Width = 100
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "KH lũy kế"
        Me.GridColumn13.DisplayFormat.FormatString = "c0"
        Me.GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn13.FieldName = "khluyke"
        Me.GridColumn13.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.SummaryItem.DisplayFormat = "{0:c0}"
        Me.GridColumn13.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 9
        Me.GridColumn13.Width = 124
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Ngày nhập"
        Me.GridColumn5.FieldName = "NgayThang"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.SummaryItem.DisplayFormat = "Sau:  {0:c0}"
        Me.GridColumn5.SummaryItem.FieldName = "saukh"
        Me.GridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 111
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn8.Caption = "TG sử dụng(ngày)"
        Me.GridColumn8.FieldName = "thoigiansudung"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Width = 139
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn9.Caption = "TG còn lại"
        Me.GridColumn9.FieldName = "thoigianconkh"
        Me.GridColumn9.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 11
        Me.GridColumn9.Width = 143
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Giá trị còn lại"
        Me.GridColumn3.DisplayFormat.FormatString = "c0"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "saukh"
        Me.GridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.SummaryItem.DisplayFormat = "{0:c0}"
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 10
        Me.GridColumn3.Width = 113
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "ID Tình trạng"
        Me.GridColumn6.FieldName = "idtinhtrang"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Width = 83
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "ID Loại tài sản"
        Me.GridColumn7.FieldName = "idloaitaisan"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Width = 89
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barTxtMaVT, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.BarButtonItem5, Me.btnChiTietTaiSan, Me.barCiLoc, Me.barLueNhomVT, Me.barLueHang, Me.barLueTenVT, Me.barCbbThang, Me.barSeNam, Me.BarButtonItem4, Me.BarEditItem1, Me.chkXemHet})
        Me.BarManager1.MainMenu = Me.Bar1
        Me.BarManager1.MaxItemId = 37
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riTxtMaVT, Me.riCbbTrangThai, Me.riLueNhomVT, Me.riLueHang, Me.riLueTenVT, Me.riCbbThang, Me.riSeNam, Me.RepositoryItemLookUpEdit1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Main menu"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCbbThang, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barSeNam, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCiLoc, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem4, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.chkXemHet, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.MultiLine = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Main menu"
        '
        'barCbbThang
        '
        Me.barCbbThang.Caption = "Tháng"
        Me.barCbbThang.Edit = Me.riCbbThang
        Me.barCbbThang.Id = 31
        Me.barCbbThang.Name = "barCbbThang"
        Me.barCbbThang.Width = 62
        '
        'riCbbThang
        '
        Me.riCbbThang.AutoHeight = False
        Me.riCbbThang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riCbbThang.Name = "riCbbThang"
        '
        'barSeNam
        '
        Me.barSeNam.Caption = "Năm"
        Me.barSeNam.Edit = Me.riSeNam
        Me.barSeNam.Id = 33
        Me.barSeNam.Name = "barSeNam"
        Me.barSeNam.Width = 73
        '
        'riSeNam
        '
        Me.riSeNam.AutoHeight = False
        Me.riSeNam.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riSeNam.Mask.EditMask = "d"
        Me.riSeNam.Mask.UseMaskAsDisplayFormat = True
        Me.riSeNam.Name = "riSeNam"
        '
        'barCiLoc
        '
        Me.barCiLoc.Border = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.barCiLoc.Caption = "Lọc"
        Me.barCiLoc.Glyph = Global.BACSOFT.My.Resources.Resources.filter_18
        Me.barCiLoc.Id = 21
        Me.barCiLoc.Name = "barCiLoc"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Tải lại"
        Me.BarButtonItem4.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem4.Id = 34
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'chkXemHet
        '
        Me.chkXemHet.Caption = "Xem tài sản khấu hao hết"
        Me.chkXemHet.Glyph = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.chkXemHet.Id = 36
        Me.chkXemHet.Name = "chkXemHet"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1283, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 344)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1283, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 318)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1283, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 318)
        '
        'barTxtMaVT
        '
        Me.barTxtMaVT.Caption = "Mã"
        Me.barTxtMaVT.Edit = Me.riTxtMaVT
        Me.barTxtMaVT.Id = 8
        Me.barTxtMaVT.Name = "barTxtMaVT"
        Me.barTxtMaVT.Width = 156
        '
        'riTxtMaVT
        '
        Me.riTxtMaVT.AutoHeight = False
        Me.riTxtMaVT.Name = "riTxtMaVT"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Thêm"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.BarButtonItem1.Id = 12
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Sửa"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.BarButtonItem2.Id = 13
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Xóa"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem3.Id = 14
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "Thêm NSD"
        Me.BarButtonItem5.Glyph = Global.BACSOFT.My.Resources.Resources.User_18
        Me.BarButtonItem5.Id = 15
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'btnChiTietTaiSan
        '
        Me.btnChiTietTaiSan.Caption = "Chi tiết tài sản"
        Me.btnChiTietTaiSan.Id = 16
        Me.btnChiTietTaiSan.Name = "btnChiTietTaiSan"
        '
        'barLueNhomVT
        '
        Me.barLueNhomVT.Caption = "Nhóm"
        Me.barLueNhomVT.Edit = Me.riLueNhomVT
        Me.barLueNhomVT.Id = 26
        Me.barLueNhomVT.Name = "barLueNhomVT"
        Me.barLueNhomVT.Width = 156
        '
        'riLueNhomVT
        '
        Me.riLueNhomVT.AutoHeight = False
        Me.riLueNhomVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueNhomVT.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Ten")})
        Me.riLueNhomVT.DisplayMember = "Ten"
        Me.riLueNhomVT.Name = "riLueNhomVT"
        Me.riLueNhomVT.NullText = "Tất cả"
        Me.riLueNhomVT.ShowFooter = False
        Me.riLueNhomVT.ShowHeader = False
        Me.riLueNhomVT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueNhomVT.ValueMember = "ID"
        '
        'barLueHang
        '
        Me.barLueHang.Caption = "Hãng"
        Me.barLueHang.Edit = Me.riLueHang
        Me.barLueHang.Id = 28
        Me.barLueHang.Name = "barLueHang"
        Me.barLueHang.Width = 156
        '
        'riLueHang
        '
        Me.riLueHang.AutoHeight = False
        Me.riLueHang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueHang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN", "Tên")})
        Me.riLueHang.DisplayMember = "TEN"
        Me.riLueHang.Name = "riLueHang"
        Me.riLueHang.NullText = "Tất cả"
        Me.riLueHang.ShowHeader = False
        Me.riLueHang.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueHang.ValueMember = "ID"
        '
        'barLueTenVT
        '
        Me.barLueTenVT.Caption = "Tên"
        Me.barLueTenVT.Edit = Me.riLueTenVT
        Me.barLueTenVT.Id = 30
        Me.barLueTenVT.Name = "barLueTenVT"
        Me.barLueTenVT.Width = 156
        '
        'riLueTenVT
        '
        Me.riLueTenVT.AutoHeight = False
        Me.riLueTenVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueTenVT.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ten", "Tên"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.riLueTenVT.DisplayMember = "ten"
        Me.riLueTenVT.Name = "riLueTenVT"
        Me.riLueTenVT.NullText = "Tất cả"
        Me.riLueTenVT.ShowHeader = False
        Me.riLueTenVT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueTenVT.ValueMember = "ID"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemLookUpEdit1
        Me.BarEditItem1.Id = 35
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'RepositoryItemLookUpEdit1
        '
        Me.RepositoryItemLookUpEdit1.AutoHeight = False
        Me.RepositoryItemLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemLookUpEdit1.Name = "RepositoryItemLookUpEdit1"
        '
        'riCbbTrangThai
        '
        Me.riCbbTrangThai.AutoHeight = False
        Me.riCbbTrangThai.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riCbbTrangThai.Items.AddRange(New Object() {"Tất cả", "Hàng hóa", "Công cụ, dụng cụ", "Tài sản"})
        Me.riCbbTrangThai.Name = "riCbbTrangThai"
        '
        'meKhauHaoTS
        '
        Me.meKhauHaoTS.EditValue = resources.GetString("meKhauHaoTS.EditValue")
        Me.meKhauHaoTS.Location = New System.Drawing.Point(38, 117)
        Me.meKhauHaoTS.MenuManager = Me.BarManager1
        Me.meKhauHaoTS.Name = "meKhauHaoTS"
        Me.meKhauHaoTS.Size = New System.Drawing.Size(761, 227)
        Me.meKhauHaoTS.TabIndex = 10
        Me.meKhauHaoTS.Visible = False
        '
        'frmKhauHaoTaiSan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1283, 344)
        Me.Controls.Add(Me.meKhauHaoTS)
        Me.Controls.Add(Me.gcKhauHaoTs)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmKhauHaoTaiSan"
        Me.Text = "Khấu hao tài sản"
        CType(Me.gcKhauHaoTs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvKhauHaoTs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbThang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riSeNam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riTxtMaVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueNhomVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueHang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTenVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbTrangThai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.meKhauHaoTS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcKhauHaoTs As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvKhauHaoTs As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BandedGridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barLueNhomVT As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueNhomVT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barLueHang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueHang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barLueTenVT As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueTenVT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barTxtMaVT As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riTxtMaVT As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents riCbbTrangThai As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents barCiLoc As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents barCbbThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riCbbThang As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents barSeNam As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riSeNam As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnChiTietTaiSan As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents meKhauHaoTS As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents chkXemHet As DevExpress.XtraBars.BarCheckItem
End Class
