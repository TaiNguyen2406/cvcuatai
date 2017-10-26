<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNguonKhachMoi
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
        Me.gdvXuLyYC = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rtbNoiDung = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rpFile = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.tbTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.tbDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.cbNhom = New DevExpress.XtraBars.BarEditItem()
        Me.rcbNhom = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cbNguoiLap = New DevExpress.XtraBars.BarEditItem()
        Me.rcbNguoiLap = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btTaiDS = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mChuaXem = New DevExpress.XtraBars.BarButtonItem()
        Me.mDaXem = New DevExpress.XtraBars.BarButtonItem()
        Me.btThemLienHe = New DevExpress.XtraBars.BarButtonItem()
        Me.btSuaLienHe = New DevExpress.XtraBars.BarButtonItem()
        Me.btThemQuaTrinhXL = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.mSuaQuaTrinhXL = New DevExpress.XtraBars.BarButtonItem()
        Me.btXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.rcbThucHien = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.pMenu = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.gdvXuLyYC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbNoiDung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rpFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNhom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNguoiLap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbThucHien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvXuLyYC
        '
        Me.gdvXuLyYC.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvXuLyYC.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvXuLyYC.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvXuLyYC.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvXuLyYC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16})
        Me.gdvXuLyYC.GridControl = Me.gdv
        Me.gdvXuLyYC.Name = "gdvXuLyYC"
        Me.gdvXuLyYC.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvXuLyYC.OptionsView.ColumnAutoWidth = False
        Me.gdvXuLyYC.OptionsView.RowAutoHeight = True
        Me.gdvXuLyYC.OptionsView.ShowGroupPanel = False
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.Caption = "Thời gian"
        Me.GridColumn10.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn10.FieldName = "ThoiGian"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.ReadOnly = True
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        Me.GridColumn10.Width = 112
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn11.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn11.Caption = "Nội dung"
        Me.GridColumn11.ColumnEdit = Me.rtbNoiDung
        Me.GridColumn11.FieldName = "NoiDung"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.ReadOnly = True
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 500
        '
        'rtbNoiDung
        '
        Me.rtbNoiDung.Name = "rtbNoiDung"
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Thực hiện"
        Me.GridColumn13.FieldName = "ThucHien"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.ReadOnly = True
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        Me.GridColumn13.Width = 150
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "File"
        Me.GridColumn14.ColumnEdit = Me.rpFile
        Me.GridColumn14.FieldName = "FileDinhKem"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 3
        Me.GridColumn14.Width = 40
        '
        'rpFile
        '
        Me.rpFile.AutoHeight = False
        Me.rpFile.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rpFile.Name = "rpFile"
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "ID"
        Me.GridColumn15.FieldName = "ID"
        Me.GridColumn15.Name = "GridColumn15"
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "IDThucHien"
        Me.GridColumn16.FieldName = "IDThucHien"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 29)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rtbNoiDung, Me.rpFile})
        Me.gdv.Size = New System.Drawing.Size(1277, 418)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT, Me.gdvXuLyYC})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn2, Me.GridColumn3, Me.GridColumn7, Me.GridColumn12, Me.GridColumn1, Me.GridColumn4, Me.GridColumn6})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsSelection.MultiSelect = True
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowAutoFilterRow = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        Me.gdvCT.RowHeight = 22
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "ID"
        Me.GridColumn5.FieldName = "ID"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Thời gian"
        Me.GridColumn2.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "ThoiGian"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 112
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn3.Caption = "Nội dung"
        Me.GridColumn3.ColumnEdit = Me.rtbNoiDung
        Me.GridColumn3.FieldName = "NoiDung"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 4
        Me.GridColumn3.Width = 541
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Nhân viên"
        Me.GridColumn7.FieldName = "NhanVien"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.FixedWidth = True
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        Me.GridColumn7.Width = 150
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "IDNguoiLap"
        Me.GridColumn12.FieldName = "IDNhanVien"
        Me.GridColumn12.Name = "GridColumn12"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nguồn"
        Me.GridColumn1.FieldName = "Nguon"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 3
        Me.GridColumn1.Width = 174
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mChuaXem, Me.mDaXem, Me.tbTuNgay, Me.tbDenNgay, Me.btTaiDS, Me.cbNguoiLap, Me.btThemLienHe, Me.btSuaLienHe, Me.btThemQuaTrinhXL, Me.BarButtonItem4, Me.cbNhom, Me.mSuaQuaTrinhXL, Me.btXoa})
        Me.BarManager1.MaxItemId = 15
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemComboBox1, Me.RepositoryItemDateEdit1, Me.RepositoryItemDateEdit2, Me.rcbNguoiLap, Me.rcbThucHien, Me.rcbNhom})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarItemVertIndent = 0
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tbTuNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.tbDenNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.cbNhom), New DevExpress.XtraBars.LinkPersistInfo(Me.cbNguoiLap), New DevExpress.XtraBars.LinkPersistInfo(Me.btTaiDS)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DisableCustomization = True
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Custom 2"
        '
        'tbTuNgay
        '
        Me.tbTuNgay.Caption = "Từ ngày"
        Me.tbTuNgay.Edit = Me.RepositoryItemDateEdit1
        Me.tbTuNgay.Id = 3
        Me.tbTuNgay.Name = "tbTuNgay"
        Me.tbTuNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbTuNgay.Width = 85
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'tbDenNgay
        '
        Me.tbDenNgay.Caption = "Đến ngày"
        Me.tbDenNgay.Edit = Me.RepositoryItemDateEdit2
        Me.tbDenNgay.Id = 4
        Me.tbDenNgay.Name = "tbDenNgay"
        Me.tbDenNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbDenNgay.Width = 85
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit2.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit2.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'cbNhom
        '
        Me.cbNhom.Caption = "Nguồn"
        Me.cbNhom.Edit = Me.rcbNhom
        Me.cbNhom.Id = 12
        Me.cbNhom.Name = "cbNhom"
        Me.cbNhom.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbNhom.Width = 191
        '
        'rcbNhom
        '
        Me.rcbNhom.AutoHeight = False
        Me.rcbNhom.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbNhom.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name2")})
        Me.rcbNhom.DisplayMember = "NoiDung"
        Me.rcbNhom.DropDownItemHeight = 22
        Me.rcbNhom.Name = "rcbNhom"
        Me.rcbNhom.NullText = "[Tất cả]"
        Me.rcbNhom.ShowHeader = False
        Me.rcbNhom.ValueMember = "Ma"
        '
        'cbNguoiLap
        '
        Me.cbNguoiLap.Caption = "Nhân viên"
        Me.cbNguoiLap.Edit = Me.rcbNguoiLap
        Me.cbNguoiLap.Id = 6
        Me.cbNguoiLap.Name = "cbNguoiLap"
        Me.cbNguoiLap.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbNguoiLap.Width = 170
        '
        'rcbNguoiLap
        '
        Me.rcbNguoiLap.AutoHeight = False
        Me.rcbNguoiLap.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbNguoiLap.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.rcbNguoiLap.DisplayMember = "Ten"
        Me.rcbNguoiLap.DropDownItemHeight = 22
        Me.rcbNguoiLap.Name = "rcbNguoiLap"
        Me.rcbNguoiLap.NullText = "[-Tất cả-]"
        Me.rcbNguoiLap.ShowHeader = False
        Me.rcbNguoiLap.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbNguoiLap.ValueMember = "ID"
        '
        'btTaiDS
        '
        Me.btTaiDS.Caption = "Tải DS"
        Me.btTaiDS.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btTaiDS.Id = 5
        Me.btTaiDS.Name = "btTaiDS"
        Me.btTaiDS.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1277, 29)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 447)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1277, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 29)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 418)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1277, 29)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 418)
        '
        'mChuaXem
        '
        Me.mChuaXem.Caption = "Đánh dấu chưa xem"
        Me.mChuaXem.Id = 0
        Me.mChuaXem.Name = "mChuaXem"
        '
        'mDaXem
        '
        Me.mDaXem.Caption = "Đánh dấu đã xem"
        Me.mDaXem.Id = 1
        Me.mDaXem.Name = "mDaXem"
        '
        'btThemLienHe
        '
        Me.btThemLienHe.Caption = "Thêm liên hệ"
        Me.btThemLienHe.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThemLienHe.Id = 8
        Me.btThemLienHe.Name = "btThemLienHe"
        '
        'btSuaLienHe
        '
        Me.btSuaLienHe.Caption = "Sửa liên hệ"
        Me.btSuaLienHe.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSuaLienHe.Id = 9
        Me.btSuaLienHe.Name = "btSuaLienHe"
        '
        'btThemQuaTrinhXL
        '
        Me.btThemQuaTrinhXL.Caption = "Thêm quá trình xử lý"
        Me.btThemQuaTrinhXL.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThemQuaTrinhXL.Id = 10
        Me.btThemQuaTrinhXL.Name = "btThemQuaTrinhXL"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Sửa quá trình xử lý"
        Me.BarButtonItem4.Id = 11
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'mSuaQuaTrinhXL
        '
        Me.mSuaQuaTrinhXL.Caption = "Sửa quá trình xử lý"
        Me.mSuaQuaTrinhXL.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSuaQuaTrinhXL.Id = 13
        Me.mSuaQuaTrinhXL.Name = "mSuaQuaTrinhXL"
        '
        'btXoa
        '
        Me.btXoa.Caption = "Xóa"
        Me.btXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoa.Id = 14
        Me.btXoa.Name = "btXoa"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Nội dung mới", "Theo thời gian", "Tất cả"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'rcbThucHien
        '
        Me.rcbThucHien.AutoHeight = False
        Me.rcbThucHien.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbThucHien.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name4")})
        Me.rcbThucHien.DisplayMember = "Ten"
        Me.rcbThucHien.DropDownItemHeight = 22
        Me.rcbThucHien.Name = "rcbThucHien"
        Me.rcbThucHien.NullText = "[-Tất cả-]"
        Me.rcbThucHien.ShowHeader = False
        Me.rcbThucHien.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbThucHien.ValueMember = "ID"
        '
        'pMenu
        '
        Me.pMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btThemLienHe), New DevExpress.XtraBars.LinkPersistInfo(Me.btSuaLienHe), New DevExpress.XtraBars.LinkPersistInfo(Me.btXoa)})
        Me.pMenu.Manager = Me.BarManager1
        Me.pMenu.Name = "pMenu"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Khách hàng"
        Me.GridColumn4.FieldName = "ttcMa"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 146
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Người GD"
        Me.GridColumn6.FieldName = "NguoiGiaoDich"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 180
        '
        'frmNguonKhachMoi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmNguonKhachMoi"
        Me.Size = New System.Drawing.Size(1277, 447)
        CType(Me.gdvXuLyYC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbNoiDung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rpFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNhom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNguoiLap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbThucHien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rtbNoiDung As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents mChuaXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pMenu As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mDaXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents tbTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents tbDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btTaiDS As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cbNguoiLap As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbNguoiLap As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents rcbThucHien As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btThemLienHe As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSuaLienHe As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btThemQuaTrinhXL As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cbNhom As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbNhom As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents gdvXuLyYC As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mSuaQuaTrinhXL As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rpFile As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn

End Class
