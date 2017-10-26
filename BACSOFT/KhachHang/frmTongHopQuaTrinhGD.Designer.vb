<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTongHopQuaTrinhGD
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
        Me.cbXemTheo = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.tbTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.tbDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btTaiDS = New DevExpress.XtraBars.BarButtonItem()
        Me.btThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btXoaNgd = New DevExpress.XtraBars.BarButtonItem()
        Me.btXuatExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mSua = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.rcbLoaiKH = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.rcbKhachHang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbDoiTuongNhanEmail = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cbPhanHoi = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.chkMoi = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbLoaiKH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbKhachHang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbDoiTuongNhanEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbPhanHoi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkMoi, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btThem, Me.btSua, Me.btTaiDS, Me.btXuatExcel, Me.btXoaNgd, Me.mThem, Me.mSua, Me.mXoa, Me.tbTuNgay, Me.tbDenNgay, Me.cbXemTheo})
        Me.BarManager1.MaxItemId = 17
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbLoaiKH, Me.rcbKhachHang, Me.RepositoryItemDateEdit1, Me.RepositoryItemDateEdit2, Me.RepositoryItemComboBox1})
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
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cbXemTheo), New DevExpress.XtraBars.LinkPersistInfo(Me.tbTuNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.tbDenNgay), New DevExpress.XtraBars.LinkPersistInfo(Me.btTaiDS), New DevExpress.XtraBars.LinkPersistInfo(Me.btThem, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btSua), New DevExpress.XtraBars.LinkPersistInfo(Me.btXoaNgd), New DevExpress.XtraBars.LinkPersistInfo(Me.btXuatExcel)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'cbXemTheo
        '
        Me.cbXemTheo.Caption = "Xem theo"
        Me.cbXemTheo.Edit = Me.RepositoryItemComboBox1
        Me.cbXemTheo.EditValue = "Tất cả"
        Me.cbXemTheo.Id = 16
        Me.cbXemTheo.Name = "cbXemTheo"
        Me.cbXemTheo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbXemTheo.Width = 113
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Tất cả", "Khoảng thời gian"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'tbTuNgay
        '
        Me.tbTuNgay.Caption = "Từ ngày"
        Me.tbTuNgay.Edit = Me.RepositoryItemDateEdit1
        Me.tbTuNgay.Enabled = False
        Me.tbTuNgay.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.tbTuNgay.Id = 14
        Me.tbTuNgay.Name = "tbTuNgay"
        Me.tbTuNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbTuNgay.Width = 90
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'tbDenNgay
        '
        Me.tbDenNgay.Caption = "Đến ngày"
        Me.tbDenNgay.Edit = Me.RepositoryItemDateEdit2
        Me.tbDenNgay.Enabled = False
        Me.tbDenNgay.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.tbDenNgay.Id = 15
        Me.tbDenNgay.Name = "tbDenNgay"
        Me.tbDenNgay.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbDenNgay.Width = 90
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btTaiDS
        '
        Me.btTaiDS.Caption = "Tải DS"
        Me.btTaiDS.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btTaiDS.Id = 8
        Me.btTaiDS.Name = "btTaiDS"
        Me.btTaiDS.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btThem
        '
        Me.btThem.Caption = "Thêm"
        Me.btThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Id = 1
        Me.btThem.Name = "btThem"
        Me.btThem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btThem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'btSua
        '
        Me.btSua.Caption = "Sửa"
        Me.btSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btSua.Id = 2
        Me.btSua.Name = "btSua"
        Me.btSua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'btXoaNgd
        '
        Me.btXoaNgd.Caption = "Xóa"
        Me.btXoaNgd.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btXoaNgd.Id = 10
        Me.btXoaNgd.Name = "btXoaNgd"
        Me.btXoaNgd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.btXoaNgd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '
        'btXuatExcel
        '
        Me.btXuatExcel.Caption = "Xuất Excel"
        Me.btXuatExcel.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btXuatExcel.Id = 9
        Me.btXuatExcel.Name = "btXuatExcel"
        Me.btXuatExcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1061, 29)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 463)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1061, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 29)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 434)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1061, 29)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 434)
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm ngd"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 11
        Me.mThem.Name = "mThem"
        '
        'mSua
        '
        Me.mSua.Caption = "Sửa thông tin Ngd"
        Me.mSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSua.Id = 12
        Me.mSua.Name = "mSua"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xóa ngd"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 13
        Me.mXoa.Name = "mXoa"
        '
        'rcbLoaiKH
        '
        Me.rcbLoaiKH.AutoHeight = False
        Me.rcbLoaiKH.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbLoaiKH.DropDownItemHeight = 22
        Me.rcbLoaiKH.Items.AddRange(New Object() {"Tất cả", "Khách hàng", "Nhà cung cấp"})
        Me.rcbLoaiKH.Name = "rcbLoaiKH"
        Me.rcbLoaiKH.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'rcbKhachHang
        '
        Me.rcbKhachHang.AutoHeight = False
        Me.rcbKhachHang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbKhachHang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ttcMa", 10, "Mã KH"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên")})
        Me.rcbKhachHang.DisplayMember = "ttcMa"
        Me.rcbKhachHang.DropDownItemHeight = 22
        Me.rcbKhachHang.Name = "rcbKhachHang"
        Me.rcbKhachHang.NullText = "[Tất cả]"
        Me.rcbKhachHang.PopupWidth = 400
        Me.rcbKhachHang.ShowHeader = False
        Me.rcbKhachHang.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbKhachHang.ValueMember = "ID"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThem), New DevExpress.XtraBars.LinkPersistInfo(Me.mSua), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoa)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 29)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1, Me.rcbDoiTuongNhanEmail, Me.cbPhanHoi, Me.chkMoi})
        Me.gdv.Size = New System.Drawing.Size(1061, 434)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colID, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.GroupPanelText = "Kéo tả cột cần nhóm vào đây"
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsFind.AllowFindPanel = False
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowAutoFilterRow = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.RowHeight = 22
        '
        'colID
        '
        Me.colID.Caption = "ID"
        Me.colID.FieldName = "ID"
        Me.colID.Name = "colID"
        Me.colID.OptionsColumn.ReadOnly = True
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Thời gian"
        Me.GridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "ThoiGian"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 106
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Khách hàng"
        Me.GridColumn2.FieldName = "ttcMa"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 156
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Phụ trách"
        Me.GridColumn3.FieldName = "PhuTrach"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 178
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn4.Caption = "Nội dung"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn4.FieldName = "NoiDungGiaoDich"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 421
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Hình thức"
        Me.GridColumn5.FieldName = "HinhThuc"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 153
        '
        'rcbDoiTuongNhanEmail
        '
        Me.rcbDoiTuongNhanEmail.AutoHeight = False
        Me.rcbDoiTuongNhanEmail.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbDoiTuongNhanEmail.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name7", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name8")})
        Me.rcbDoiTuongNhanEmail.DisplayMember = "Ten"
        Me.rcbDoiTuongNhanEmail.DropDownItemHeight = 22
        Me.rcbDoiTuongNhanEmail.Name = "rcbDoiTuongNhanEmail"
        Me.rcbDoiTuongNhanEmail.NullText = ""
        Me.rcbDoiTuongNhanEmail.ShowHeader = False
        Me.rcbDoiTuongNhanEmail.ValueMember = "ID"
        '
        'cbPhanHoi
        '
        Me.cbPhanHoi.AutoHeight = False
        Me.cbPhanHoi.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbPhanHoi.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.cbPhanHoi.DisplayMember = "Ten"
        Me.cbPhanHoi.DropDownItemHeight = 22
        Me.cbPhanHoi.Name = "cbPhanHoi"
        Me.cbPhanHoi.NullText = ""
        Me.cbPhanHoi.ShowHeader = False
        Me.cbPhanHoi.ValueMember = "ID"
        '
        'chkMoi
        '
        Me.chkMoi.AutoHeight = False
        Me.chkMoi.Name = "chkMoi"
        Me.chkMoi.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkMoi.PictureChecked = Global.BACSOFT.My.Resources.Resources.Checked
        Me.chkMoi.PictureGrayed = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.chkMoi.PictureUnchecked = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.chkMoi.ValueGrayed = False
        '
        'frmTongHopQuaTrinhGD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmTongHopQuaTrinhGD"
        Me.Size = New System.Drawing.Size(1061, 463)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbLoaiKH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbKhachHang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbDoiTuongNhanEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbPhanHoi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkMoi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents rcbLoaiKH As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents colID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents rcbKhachHang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btTaiDS As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXuatExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btXoaNgd As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rcbDoiTuongNhanEmail As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents cbPhanHoi As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents chkMoi As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents tbTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents tbDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents cbXemTheo As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn

End Class
