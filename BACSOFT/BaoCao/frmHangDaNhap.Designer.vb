<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHangDaNhap
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
        Dim SuperToolTip3 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipTitleItem3 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.btfilterTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.rtbTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btfilterDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.rtbDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btfilterMaKH = New DevExpress.XtraBars.BarEditItem()
        Me.rcbMaKH = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btfilterTakecare = New DevExpress.XtraBars.BarEditItem()
        Me.rcbTakecare = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btFilterNhomVT = New DevExpress.XtraBars.BarEditItem()
        Me.rcbNhomVT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btfilterTenVT = New DevExpress.XtraBars.BarEditItem()
        Me.rcbTenVatTu = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btFilterHangSX = New DevExpress.XtraBars.BarEditItem()
        Me.rcbHangSX = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btFilterMaVT = New DevExpress.XtraBars.BarEditItem()
        Me.rtbMaVT = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.chkRutGon = New DevExpress.XtraBars.BarCheckItem()
        Me.btTaiLai = New DevExpress.XtraBars.BarButtonItem()
        Me.btKetXuat = New DevExpress.XtraBars.BarButtonItem()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl2 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl3 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl4 = New DevExpress.XtraBars.BarDockControl()
        Me.btCM = New DevExpress.XtraBars.BarSubItem()
        Me.btChuyenMaRieng = New DevExpress.XtraBars.BarButtonItem()
        Me.btChuyenMaChung = New DevExpress.XtraBars.BarButtonItem()
        Me.pCMChuyenMaChung = New DevExpress.XtraBars.BarButtonItem()
        Me.chkLocThongDung = New DevExpress.XtraBars.BarCheckItem()
        Me.chkLocTon = New DevExpress.XtraBars.BarCheckItem()
        Me.chkLocKhoBan = New DevExpress.XtraBars.BarCheckItem()
        Me.chkHienDongLocDuLieu = New DevExpress.XtraBars.BarCheckItem()
        Me.mSuaHinhAnh = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.mSaoChep = New DevExpress.XtraBars.BarButtonItem()
        Me.mLuuLai = New DevExpress.XtraBars.BarButtonItem()
        Me.mChuyenDenThuMuc = New DevExpress.XtraBars.BarSubItem()
        Me.mTaiAnhVeMay = New DevExpress.XtraBars.BarButtonItem()
        Me.mThuMucLuuTaiLieu = New DevExpress.XtraBars.BarButtonItem()
        Me.mDungChungTaiLieu = New DevExpress.XtraBars.BarButtonItem()
        Me.mDungChungHinhAnh = New DevExpress.XtraBars.BarButtonItem()
        Me.mBoHinhAnh = New DevExpress.XtraBars.BarButtonItem()
        Me.mMoThuMucLuuTruTaiLieu = New DevExpress.XtraBars.BarButtonItem()
        Me.mXemAnhLon = New DevExpress.XtraBars.BarButtonItem()
        Me.mBoSapXep = New DevExpress.XtraBars.BarButtonItem()
        Me.mSapXep = New DevExpress.XtraBars.BarButtonItem()
        Me.mXemChaoGia = New DevExpress.XtraBars.BarButtonItem()
        Me.mXemXuatKho = New DevExpress.XtraBars.BarButtonItem()
        Me.mTinhTrangVatTu = New DevExpress.XtraBars.BarButtonItem()
        Me.rcbMaVT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.tbThongSo = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.rtbThongSo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.rpQuaTrinh = New DevExpress.XtraEditors.Repository.RepositoryItemProgressBar()
        Me.menuDaXuat = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.gdvDaNhap = New DevExpress.XtraGrid.GridControl()
        Me.gdvDaNhapCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colDaNNCC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDNSoPhieu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDNSPDatHang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colLoaiDN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbMaKH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbTakecare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNhomVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbTenVatTu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbHangSX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbMaVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbMaVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThongSo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbThongSo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rpQuaTrinh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.menuDaXuat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvDaNhap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvDaNhapCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.DockControls.Add(Me.BarDockControl2)
        Me.BarManager1.DockControls.Add(Me.BarDockControl3)
        Me.BarManager1.DockControls.Add(Me.BarDockControl4)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btfilterTenVT, Me.btFilterNhomVT, Me.btTaiLai, Me.btCM, Me.btChuyenMaRieng, Me.btChuyenMaChung, Me.pCMChuyenMaChung, Me.btFilterHangSX, Me.chkLocThongDung, Me.chkLocTon, Me.chkLocKhoBan, Me.chkHienDongLocDuLieu, Me.btFilterMaVT, Me.mSuaHinhAnh, Me.BarButtonItem1, Me.mSaoChep, Me.mLuuLai, Me.mChuyenDenThuMuc, Me.mTaiAnhVeMay, Me.mThuMucLuuTaiLieu, Me.mDungChungTaiLieu, Me.mDungChungHinhAnh, Me.mBoHinhAnh, Me.mMoThuMucLuuTruTaiLieu, Me.mXemAnhLon, Me.mBoSapXep, Me.mSapXep, Me.btfilterMaKH, Me.btfilterTakecare, Me.chkRutGon, Me.btfilterTuNgay, Me.btfilterDenNgay, Me.mXemChaoGia, Me.mXemXuatKho, Me.btKetXuat, Me.mTinhTrangVatTu})
        Me.BarManager1.MaxItemId = 58
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbTenVatTu, Me.rcbMaVT, Me.rcbNhomVT, Me.RepositoryItemCheckEdit4, Me.rcbHangSX, Me.tbThongSo, Me.rtbMaVT, Me.rtbThongSo, Me.rpQuaTrinh, Me.rcbMaKH, Me.rcbTakecare, Me.rtbTuNgay, Me.rtbDenNgay})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarItemVertIndent = 0
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btfilterTuNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.btfilterDenNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.btfilterMaKH), New DevExpress.XtraBars.LinkPersistInfo(Me.btfilterTakecare), New DevExpress.XtraBars.LinkPersistInfo(Me.btFilterNhomVT, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btfilterTenVT), New DevExpress.XtraBars.LinkPersistInfo(Me.btFilterHangSX), New DevExpress.XtraBars.LinkPersistInfo(Me.btFilterMaVT), New DevExpress.XtraBars.LinkPersistInfo(Me.chkRutGon, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btTaiLai), New DevExpress.XtraBars.LinkPersistInfo(Me.btKetXuat)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.MultiLine = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'btfilterTuNgay
        '
        Me.btfilterTuNgay.Caption = "Từ ngày"
        Me.btfilterTuNgay.Edit = Me.rtbTuNgay
        Me.btfilterTuNgay.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.btfilterTuNgay.Id = 52
        Me.btfilterTuNgay.Name = "btfilterTuNgay"
        Me.btfilterTuNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btfilterTuNgay.Width = 90
        '
        'rtbTuNgay
        '
        Me.rtbTuNgay.AutoHeight = False
        Me.rtbTuNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rtbTuNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbTuNgay.EditFormat.FormatString = "dd/MM/yyyy"
        Me.rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbTuNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.rtbTuNgay.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.rtbTuNgay.Name = "rtbTuNgay"
        Me.rtbTuNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btfilterDenNgay
        '
        Me.btfilterDenNgay.Caption = "Đến ngày"
        Me.btfilterDenNgay.Edit = Me.rtbDenNgay
        Me.btfilterDenNgay.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.btfilterDenNgay.Id = 53
        Me.btfilterDenNgay.Name = "btfilterDenNgay"
        Me.btfilterDenNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btfilterDenNgay.Width = 90
        '
        'rtbDenNgay
        '
        Me.rtbDenNgay.AutoHeight = False
        Me.rtbDenNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rtbDenNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.rtbDenNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbDenNgay.EditFormat.FormatString = "dd/MM/yyyy"
        Me.rtbDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbDenNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.rtbDenNgay.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.rtbDenNgay.Name = "rtbDenNgay"
        Me.rtbDenNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btfilterMaKH
        '
        Me.btfilterMaKH.Caption = "Mã KH"
        Me.btfilterMaKH.Edit = Me.rcbMaKH
        Me.btfilterMaKH.Id = 48
        Me.btfilterMaKH.Name = "btfilterMaKH"
        Me.btfilterMaKH.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btfilterMaKH.Width = 176
        '
        'rcbMaKH
        '
        Me.rcbMaKH.AutoHeight = False
        Me.rcbMaKH.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbMaKH.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name5", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ttcMa", "Name6")})
        Me.rcbMaKH.DisplayMember = "ttcMa"
        Me.rcbMaKH.DropDownItemHeight = 22
        Me.rcbMaKH.Name = "rcbMaKH"
        Me.rcbMaKH.NullText = "[Tất cả]"
        Me.rcbMaKH.ShowHeader = False
        Me.rcbMaKH.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbMaKH.ValueMember = "ID"
        '
        'btfilterTakecare
        '
        Me.btfilterTakecare.Caption = "Takecare"
        Me.btfilterTakecare.Edit = Me.rcbTakecare
        Me.btfilterTakecare.Id = 49
        Me.btfilterTakecare.Name = "btfilterTakecare"
        Me.btfilterTakecare.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btfilterTakecare.Width = 185
        '
        'rcbTakecare
        '
        Me.rcbTakecare.AutoHeight = False
        Me.rcbTakecare.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbTakecare.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name2", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name4")})
        Me.rcbTakecare.DisplayMember = "Ten"
        Me.rcbTakecare.DropDownItemHeight = 22
        Me.rcbTakecare.Name = "rcbTakecare"
        Me.rcbTakecare.NullText = "[Tất cả]"
        Me.rcbTakecare.ShowHeader = False
        Me.rcbTakecare.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbTakecare.ValueMember = "ID"
        '
        'btFilterNhomVT
        '
        Me.btFilterNhomVT.Caption = "Nhóm"
        Me.btFilterNhomVT.Edit = Me.rcbNhomVT
        Me.btFilterNhomVT.Id = 4
        Me.btFilterNhomVT.Name = "btFilterNhomVT"
        Me.btFilterNhomVT.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btFilterNhomVT.Width = 135
        '
        'rcbNhomVT
        '
        Me.rcbNhomVT.AutoHeight = False
        Me.rcbNhomVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbNhomVT.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Ten")})
        Me.rcbNhomVT.DisplayMember = "Ten"
        Me.rcbNhomVT.DropDownItemHeight = 22
        Me.rcbNhomVT.Name = "rcbNhomVT"
        Me.rcbNhomVT.NullText = "[Tất cả nhóm]"
        Me.rcbNhomVT.ShowHeader = False
        Me.rcbNhomVT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbNhomVT.ValueMember = "ID"
        '
        'btfilterTenVT
        '
        Me.btfilterTenVT.Caption = "Tên hàng hóa"
        Me.btfilterTenVT.Edit = Me.rcbTenVatTu
        Me.btfilterTenVT.Id = 2
        Me.btfilterTenVT.Name = "btfilterTenVT"
        Me.btfilterTenVT.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btfilterTenVT.Width = 158
        '
        'rcbTenVatTu
        '
        Me.rcbTenVatTu.AutoHeight = False
        Me.rcbTenVatTu.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbTenVatTu.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên vật tư")})
        Me.rcbTenVatTu.DisplayMember = "Ten"
        Me.rcbTenVatTu.DropDownItemHeight = 22
        Me.rcbTenVatTu.Name = "rcbTenVatTu"
        Me.rcbTenVatTu.NullText = "[Tất cả]"
        Me.rcbTenVatTu.ShowHeader = False
        Me.rcbTenVatTu.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbTenVatTu.ValueMember = "ID"
        '
        'btFilterHangSX
        '
        Me.btFilterHangSX.Caption = "Hãng"
        Me.btFilterHangSX.Edit = Me.rcbHangSX
        Me.btFilterHangSX.Id = 16
        Me.btFilterHangSX.Name = "btFilterHangSX"
        Me.btFilterHangSX.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btFilterHangSX.Width = 120
        '
        'rcbHangSX
        '
        Me.rcbHangSX.AutoHeight = False
        Me.rcbHangSX.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbHangSX.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên")})
        Me.rcbHangSX.DisplayMember = "Ten"
        Me.rcbHangSX.DropDownItemHeight = 22
        Me.rcbHangSX.Name = "rcbHangSX"
        Me.rcbHangSX.NullText = "[Tất cả hãng]"
        Me.rcbHangSX.ShowHeader = False
        Me.rcbHangSX.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbHangSX.ValueMember = "ID"
        '
        'btFilterMaVT
        '
        Me.btFilterMaVT.Caption = "Mã"
        Me.btFilterMaVT.Edit = Me.rtbMaVT
        Me.btFilterMaVT.Id = 23
        Me.btFilterMaVT.Name = "btFilterMaVT"
        Me.btFilterMaVT.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btFilterMaVT.Width = 114
        '
        'rtbMaVT
        '
        Me.rtbMaVT.AutoHeight = False
        Me.rtbMaVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rtbMaVT.Name = "rtbMaVT"
        '
        'chkRutGon
        '
        Me.chkRutGon.Caption = "Rút gọn"
        Me.chkRutGon.Glyph = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.chkRutGon.Id = 51
        Me.chkRutGon.Name = "chkRutGon"
        Me.chkRutGon.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btTaiLai
        '
        Me.btTaiLai.Caption = "Tải DS"
        Me.btTaiLai.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btTaiLai.Id = 8
        Me.btTaiLai.Name = "btTaiLai"
        Me.btTaiLai.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        ToolTipTitleItem3.Text = "Tải lại danh sách vật tư "
        SuperToolTip3.Items.Add(ToolTipTitleItem3)
        Me.btTaiLai.SuperTip = SuperToolTip3
        '
        'btKetXuat
        '
        Me.btKetXuat.Caption = "Xuất Excel"
        Me.btKetXuat.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btKetXuat.Id = 56
        Me.btKetXuat.Name = "btKetXuat"
        Me.btKetXuat.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl1.Location = New System.Drawing.Point(0, 0)
        Me.BarDockControl1.Size = New System.Drawing.Size(1306, 65)
        '
        'BarDockControl2
        '
        Me.BarDockControl2.CausesValidation = False
        Me.BarDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl2.Location = New System.Drawing.Point(0, 663)
        Me.BarDockControl2.Size = New System.Drawing.Size(1306, 0)
        '
        'BarDockControl3
        '
        Me.BarDockControl3.CausesValidation = False
        Me.BarDockControl3.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl3.Location = New System.Drawing.Point(0, 65)
        Me.BarDockControl3.Size = New System.Drawing.Size(0, 598)
        '
        'BarDockControl4
        '
        Me.BarDockControl4.CausesValidation = False
        Me.BarDockControl4.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl4.Location = New System.Drawing.Point(1306, 65)
        Me.BarDockControl4.Size = New System.Drawing.Size(0, 598)
        '
        'btCM
        '
        Me.btCM.Caption = "Chuyển mã"
        Me.btCM.Glyph = Global.BACSOFT.My.Resources.Resources.back_18
        Me.btCM.Id = 10
        Me.btCM.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btChuyenMaRieng), New DevExpress.XtraBars.LinkPersistInfo(Me.btChuyenMaChung)})
        Me.btCM.Name = "btCM"
        Me.btCM.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btChuyenMaRieng
        '
        Me.btChuyenMaRieng.Caption = "Chuyển mã riêng"
        Me.btChuyenMaRieng.Id = 11
        Me.btChuyenMaRieng.Name = "btChuyenMaRieng"
        '
        'btChuyenMaChung
        '
        Me.btChuyenMaChung.Caption = "Chuyển mã chung"
        Me.btChuyenMaChung.Id = 12
        Me.btChuyenMaChung.Name = "btChuyenMaChung"
        '
        'pCMChuyenMaChung
        '
        Me.pCMChuyenMaChung.Caption = "Chuyển mã chung"
        Me.pCMChuyenMaChung.Glyph = Global.BACSOFT.My.Resources.Resources.back_18
        Me.pCMChuyenMaChung.Id = 13
        Me.pCMChuyenMaChung.Name = "pCMChuyenMaChung"
        Me.pCMChuyenMaChung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'chkLocThongDung
        '
        Me.chkLocThongDung.Caption = "Thông dụng"
        Me.chkLocThongDung.Id = 19
        Me.chkLocThongDung.Name = "chkLocThongDung"
        '
        'chkLocTon
        '
        Me.chkLocTon.Caption = "Tồn"
        Me.chkLocTon.Id = 20
        Me.chkLocTon.Name = "chkLocTon"
        '
        'chkLocKhoBan
        '
        Me.chkLocKhoBan.Caption = "Khó bán"
        Me.chkLocKhoBan.Id = 21
        Me.chkLocKhoBan.Name = "chkLocKhoBan"
        '
        'chkHienDongLocDuLieu
        '
        Me.chkHienDongLocDuLieu.Caption = "Dòng lọc dữ liệu"
        Me.chkHienDongLocDuLieu.Id = 22
        Me.chkHienDongLocDuLieu.Name = "chkHienDongLocDuLieu"
        '
        'mSuaHinhAnh
        '
        Me.mSuaHinhAnh.Caption = "Sửa hình ảnh"
        Me.mSuaHinhAnh.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSuaHinhAnh.Id = 26
        Me.mSuaHinhAnh.Name = "mSuaHinhAnh"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Chuyển đến thư mục lưu trữ"
        Me.BarButtonItem1.Id = 27
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'mSaoChep
        '
        Me.mSaoChep.Caption = "Tạo bản sao"
        Me.mSaoChep.Glyph = Global.BACSOFT.My.Resources.Resources.copy_18
        Me.mSaoChep.Id = 32
        Me.mSaoChep.Name = "mSaoChep"
        '
        'mLuuLai
        '
        Me.mLuuLai.Caption = "Lưu lại"
        Me.mLuuLai.Id = 33
        Me.mLuuLai.Name = "mLuuLai"
        '
        'mChuyenDenThuMuc
        '
        Me.mChuyenDenThuMuc.Caption = "Chuyển đến thư mục"
        Me.mChuyenDenThuMuc.Id = 34
        Me.mChuyenDenThuMuc.Name = "mChuyenDenThuMuc"
        '
        'mTaiAnhVeMay
        '
        Me.mTaiAnhVeMay.Caption = "Tải ảnh về máy"
        Me.mTaiAnhVeMay.Glyph = Global.BACSOFT.My.Resources.Resources.Download_18
        Me.mTaiAnhVeMay.Id = 35
        Me.mTaiAnhVeMay.Name = "mTaiAnhVeMay"
        '
        'mThuMucLuuTaiLieu
        '
        Me.mThuMucLuuTaiLieu.Caption = "Thư mục lưu tài liệu"
        Me.mThuMucLuuTaiLieu.Id = 36
        Me.mThuMucLuuTaiLieu.Name = "mThuMucLuuTaiLieu"
        '
        'mDungChungTaiLieu
        '
        Me.mDungChungTaiLieu.Caption = "Dùng chung tài liệu"
        Me.mDungChungTaiLieu.Id = 37
        Me.mDungChungTaiLieu.Name = "mDungChungTaiLieu"
        '
        'mDungChungHinhAnh
        '
        Me.mDungChungHinhAnh.Caption = "Dùng chung hình ảnh"
        Me.mDungChungHinhAnh.Id = 38
        Me.mDungChungHinhAnh.Name = "mDungChungHinhAnh"
        '
        'mBoHinhAnh
        '
        Me.mBoHinhAnh.Caption = "Bỏ hình ảnh"
        Me.mBoHinhAnh.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mBoHinhAnh.Id = 39
        Me.mBoHinhAnh.Name = "mBoHinhAnh"
        '
        'mMoThuMucLuuTruTaiLieu
        '
        Me.mMoThuMucLuuTruTaiLieu.Caption = "Tải tài liệu về máy"
        Me.mMoThuMucLuuTruTaiLieu.Glyph = Global.BACSOFT.My.Resources.Resources.Download_18
        Me.mMoThuMucLuuTruTaiLieu.Id = 40
        Me.mMoThuMucLuuTruTaiLieu.Name = "mMoThuMucLuuTruTaiLieu"
        '
        'mXemAnhLon
        '
        Me.mXemAnhLon.Caption = "Xem ảnh lớn"
        Me.mXemAnhLon.Glyph = Global.BACSOFT.My.Resources.Resources.Preview_18
        Me.mXemAnhLon.Id = 42
        Me.mXemAnhLon.Name = "mXemAnhLon"
        '
        'mBoSapXep
        '
        Me.mBoSapXep.Caption = "Bỏ chế độ sắp xếp"
        Me.mBoSapXep.Id = 43
        Me.mBoSapXep.Name = "mBoSapXep"
        '
        'mSapXep
        '
        Me.mSapXep.Caption = "Sắp xếp"
        Me.mSapXep.Id = 45
        Me.mSapXep.Name = "mSapXep"
        '
        'mXemChaoGia
        '
        Me.mXemChaoGia.Caption = "Xem chào giá"
        Me.mXemChaoGia.Id = 54
        Me.mXemChaoGia.Name = "mXemChaoGia"
        '
        'mXemXuatKho
        '
        Me.mXemXuatKho.Caption = "Xem xuất kho"
        Me.mXemXuatKho.Id = 55
        Me.mXemXuatKho.Name = "mXemXuatKho"
        '
        'mTinhTrangVatTu
        '
        Me.mTinhTrangVatTu.Caption = "Tình trạng vật tư"
        Me.mTinhTrangVatTu.Glyph = Global.BACSOFT.My.Resources.Resources.product_18
        Me.mTinhTrangVatTu.Id = 57
        Me.mTinhTrangVatTu.Name = "mTinhTrangVatTu"
        '
        'rcbMaVT
        '
        Me.rcbMaVT.AutoHeight = False
        Me.rcbMaVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbMaVT.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Mã VT")})
        Me.rcbMaVT.DisplayMember = "Model"
        Me.rcbMaVT.DropDownItemHeight = 22
        Me.rcbMaVT.Name = "rcbMaVT"
        Me.rcbMaVT.NullText = "[Tất cả mã VT]"
        Me.rcbMaVT.ShowHeader = False
        Me.rcbMaVT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbMaVT.ValueMember = "Model"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        '
        'tbThongSo
        '
        Me.tbThongSo.AutoHeight = False
        Me.tbThongSo.Name = "tbThongSo"
        '
        'rtbThongSo
        '
        Me.rtbThongSo.AutoHeight = False
        Me.rtbThongSo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rtbThongSo.Name = "rtbThongSo"
        '
        'rpQuaTrinh
        '
        Me.rpQuaTrinh.Name = "rpQuaTrinh"
        Me.rpQuaTrinh.Step = 1
        '
        'menuDaXuat
        '
        Me.menuDaXuat.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mTinhTrangVatTu, True)})
        Me.menuDaXuat.Manager = Me.BarManager1
        Me.menuDaXuat.Name = "menuDaXuat"
        '
        'gdvDaNhap
        '
        Me.gdvDaNhap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvDaNhap.Location = New System.Drawing.Point(0, 65)
        Me.gdvDaNhap.MainView = Me.gdvDaNhapCT
        Me.gdvDaNhap.MenuManager = Me.BarManager1
        Me.gdvDaNhap.Name = "gdvDaNhap"
        Me.gdvDaNhap.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1, Me.RepositoryItemPopupContainerEdit2, Me.RepositoryItemHyperLinkEdit1})
        Me.gdvDaNhap.Size = New System.Drawing.Size(1306, 598)
        Me.gdvDaNhap.TabIndex = 8
        Me.gdvDaNhap.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvDaNhapCT})
        '
        'gdvDaNhapCT
        '
        Me.gdvDaNhapCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvDaNhapCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvDaNhapCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvDaNhapCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvDaNhapCT.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvDaNhapCT.Appearance.FooterPanel.Options.UseFont = True
        Me.gdvDaNhapCT.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvDaNhapCT.Appearance.GroupFooter.Options.UseFont = True
        Me.gdvDaNhapCT.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gdvDaNhapCT.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.gdvDaNhapCT.Appearance.GroupRow.Options.UseFont = True
        Me.gdvDaNhapCT.Appearance.GroupRow.Options.UseForeColor = True
        Me.gdvDaNhapCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvDaNhapCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvDaNhapCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvDaNhapCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvDaNhapCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvDaNhapCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvDaNhapCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDaNNCC, Me.colDNSoPhieu, Me.GridColumn5, Me.GridColumn6, Me.GridColumn9, Me.GridColumn10, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn1, Me.GridColumn2, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn20, Me.GridColumn21, Me.GridColumn22, Me.GridColumn24, Me.GridColumn26, Me.colDNSPDatHang, Me.GridColumn29, Me.colLoaiDN})
        Me.gdvDaNhapCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvDaNhapCT.GridControl = Me.gdvDaNhap
        Me.gdvDaNhapCT.GroupCount = 1
        Me.gdvDaNhapCT.GroupPanelText = "Kéo thả cột cần nhóm vào vị trí này"
        Me.gdvDaNhapCT.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ThanhTien", Me.GridColumn16, "{0:N2}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SLXuat", Me.GridColumn14, "{0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TienThue", Me.GridColumn19, "{0:N2}")})
        Me.gdvDaNhapCT.Name = "gdvDaNhapCT"
        Me.gdvDaNhapCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvDaNhapCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvDaNhapCT.OptionsBehavior.ReadOnly = True
        Me.gdvDaNhapCT.OptionsBehavior.SummariesIgnoreNullValues = True
        Me.gdvDaNhapCT.OptionsFind.AllowFindPanel = False
        Me.gdvDaNhapCT.OptionsView.ColumnAutoWidth = False
        Me.gdvDaNhapCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvDaNhapCT.OptionsView.RowAutoHeight = True
        Me.gdvDaNhapCT.OptionsView.ShowAutoFilterRow = True
        Me.gdvDaNhapCT.OptionsView.ShowFooter = True
        Me.gdvDaNhapCT.RowHeight = 22
        Me.gdvDaNhapCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.colDNSoPhieu, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'colDaNNCC
        '
        Me.colDaNNCC.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.colDaNNCC.AppearanceCell.Options.UseFont = True
        Me.colDaNNCC.Caption = "Nhà cung cấp"
        Me.colDaNNCC.FieldName = "ttcMa"
        Me.colDaNNCC.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.colDaNNCC.Name = "colDaNNCC"
        Me.colDaNNCC.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom
        Me.colDaNNCC.Visible = True
        Me.colDaNNCC.VisibleIndex = 0
        Me.colDaNNCC.Width = 164
        '
        'colDNSoPhieu
        '
        Me.colDNSoPhieu.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.colDNSoPhieu.AppearanceCell.Options.UseFont = True
        Me.colDNSoPhieu.AppearanceCell.Options.UseTextOptions = True
        Me.colDNSoPhieu.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colDNSoPhieu.Caption = "Số phiếu"
        Me.colDNSoPhieu.FieldName = "SoPhieu"
        Me.colDNSoPhieu.Name = "colDNSoPhieu"
        Me.colDNSoPhieu.Width = 64
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Ngày tháng"
        Me.GridColumn5.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn5.FieldName = "NgayThang"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "STT"
        Me.GridColumn6.FieldName = "STT"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn6.OptionsFilter.AllowAutoFilter = False
        Me.GridColumn6.OptionsFilter.AllowFilter = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 2
        Me.GridColumn6.Width = 31
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Tên hàng hóa"
        Me.GridColumn9.FieldName = "TenVT"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
        Me.GridColumn9.Width = 160
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Hãng SX"
        Me.GridColumn10.FieldName = "HangSX"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 4
        Me.GridColumn10.Width = 100
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Mã hàng"
        Me.GridColumn12.FieldName = "Model"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 5
        Me.GridColumn12.Width = 140
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn13.Caption = "ĐVT"
        Me.GridColumn13.FieldName = "DVT"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.FixedWidth = True
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 6
        Me.GridColumn13.Width = 56
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "SL"
        Me.GridColumn14.FieldName = "SoLuong"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn14.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 7
        Me.GridColumn14.Width = 46
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Đơn giá"
        Me.GridColumn15.DisplayFormat.FormatString = "N2"
        Me.GridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn15.FieldName = "DonGia"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 8
        Me.GridColumn15.Width = 90
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Thành tiền"
        Me.GridColumn16.DisplayFormat.FormatString = "N2"
        Me.GridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn16.FieldName = "ThanhTien"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn16.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 9
        Me.GridColumn16.Width = 103
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "%VAT"
        Me.GridColumn17.FieldName = "MucThue"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 10
        Me.GridColumn17.Width = 45
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "N VAT"
        Me.GridColumn18.FieldName = "NhapThue"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 11
        Me.GridColumn18.Width = 45
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Tiền thuế"
        Me.GridColumn19.DisplayFormat.FormatString = "N2"
        Me.GridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn19.FieldName = "TienThue"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.SummaryItem.DisplayFormat = "{0:N2}"
        Me.GridColumn19.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 12
        Me.GridColumn19.Width = 99
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn20.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn20.Caption = "Tiền tệ"
        Me.GridColumn20.FieldName = "TienTe"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Width = 46
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "Người đặt hàng"
        Me.GridColumn21.FieldName = "TakeCare"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn21.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 13
        Me.GridColumn21.Width = 144
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "IDKhachHang"
        Me.GridColumn22.FieldName = "IDKhachHang"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "SoPhieu2"
        Me.GridColumn24.FieldName = "SoPhieu2"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "NgayXuat2"
        Me.GridColumn26.FieldName = "NgayXuat2"
        Me.GridColumn26.Name = "GridColumn26"
        '
        'colDNSPDatHang
        '
        Me.colDNSPDatHang.Caption = "SP Đặt hàng"
        Me.colDNSPDatHang.FieldName = "SoPhieuDH"
        Me.colDNSPDatHang.Name = "colDNSPDatHang"
        Me.colDNSPDatHang.Visible = True
        Me.colDNSPDatHang.VisibleIndex = 14
        Me.colDNSPDatHang.Width = 81
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "IDVatTu"
        Me.GridColumn29.FieldName = "IDVatTu"
        Me.GridColumn29.Name = "GridColumn29"
        '
        'colLoaiDN
        '
        Me.colLoaiDN.AppearanceCell.Options.UseTextOptions = True
        Me.colLoaiDN.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.colLoaiDN.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.colLoaiDN.Caption = "Lĩnh vực SX"
        Me.colLoaiDN.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.colLoaiDN.FieldName = "IDLinhVucSX"
        Me.colLoaiDN.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.colLoaiDN.Name = "colLoaiDN"
        Me.colLoaiDN.Visible = True
        Me.colLoaiDN.VisibleIndex = 17
        Me.colLoaiDN.Width = 127
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemMemoEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemHyperLinkEdit1
        '
        Me.RepositoryItemHyperLinkEdit1.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit1.Name = "RepositoryItemHyperLinkEdit1"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Chi phí"
        Me.GridColumn1.DisplayFormat.FormatString = "N0"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn1.FieldName = "ChiPhi"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 10
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tổng chi phí"
        Me.GridColumn2.DisplayFormat.FormatString = "N0"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.FieldName = "TongChiPhi"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 11
        Me.GridColumn2.Width = 80
        '
        'frmHangDaNhap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdvDaNhap)
        Me.Controls.Add(Me.BarDockControl3)
        Me.Controls.Add(Me.BarDockControl4)
        Me.Controls.Add(Me.BarDockControl2)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Name = "frmHangDaNhap"
        Me.Size = New System.Drawing.Size(1306, 663)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbMaKH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbTakecare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNhomVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbTenVatTu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbHangSX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbMaVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbMaVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThongSo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbThongSo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rpQuaTrinh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.menuDaXuat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvDaNhap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvDaNhapCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents btFilterNhomVT As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbNhomVT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btfilterTenVT As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbTenVatTu As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btFilterHangSX As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbHangSX As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btFilterMaVT As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rtbMaVT As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents rtbThongSo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents btTaiLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents chkLocThongDung As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkLocTon As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkLocKhoBan As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkHienDongLocDuLieu As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl2 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl3 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl4 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btCM As DevExpress.XtraBars.BarSubItem
    Friend WithEvents btChuyenMaRieng As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btChuyenMaChung As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pCMChuyenMaChung As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rcbMaVT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents tbThongSo As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents mSuaHinhAnh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSaoChep As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mLuuLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mChuyenDenThuMuc As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mTaiAnhVeMay As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mThuMucLuuTaiLieu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mDungChungTaiLieu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mDungChungHinhAnh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mBoHinhAnh As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mMoThuMucLuuTruTaiLieu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXemAnhLon As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mBoSapXep As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSapXep As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rpQuaTrinh As DevExpress.XtraEditors.Repository.RepositoryItemProgressBar
    Friend WithEvents btfilterMaKH As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbMaKH As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btfilterTakecare As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbTakecare As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents chkRutGon As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents btfilterTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rtbTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btfilterDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rtbDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents gdvDaNhap As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvDaNhapCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colDaNNCC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDNSoPhieu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDNSPDatHang As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents menuDaXuat As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mXemChaoGia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXemXuatKho As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btKetXuat As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mTinhTrangVatTu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colLoaiDN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn

End Class
