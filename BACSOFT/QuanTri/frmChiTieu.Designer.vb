<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChiTieu
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.btCapNhatCT = New DevExpress.XtraBars.BarButtonItem()
        Me.cbLoaiChiTieu = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.cbPhong = New DevExpress.XtraBars.BarEditItem()
        Me.rcbPhong = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.cbNhanVien = New DevExpress.XtraBars.BarEditItem()
        Me.rcbNhanVien = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.tbNam = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.btXem = New DevExpress.XtraBars.BarButtonItem()
        Me.btKetXuat = New DevExpress.XtraBars.BarButtonItem()
        Me.btLuuKetQua = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btCapNhatBangGiaoChiTieu = New DevExpress.XtraBars.BarButtonItem()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.rMemoText = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.rcbNhomChiTieu = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.tbN0 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.tbPT = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.tabCT = New DevExpress.XtraTab.XtraTabControl()
        Me.tabGiaoChiTieu = New DevExpress.XtraTab.XtraTabPage()
        Me.tabCapNhatChiTieu = New DevExpress.XtraTab.XtraTabPage()
        Me.gdvChiTieu = New DevExpress.XtraGrid.GridControl()
        Me.gdvChiTieuCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemChiTieu = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNhanVien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNhomChiTieu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbN0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCT.SuspendLayout()
        Me.tabGiaoChiTieu.SuspendLayout()
        Me.tabCapNhatChiTieu.SuspendLayout()
        CType(Me.gdvChiTieu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChiTieuCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btKetXuat, Me.btCapNhatCT, Me.tbNam, Me.btXem, Me.cbPhong, Me.cbLoaiChiTieu, Me.cbNhanVien, Me.btCapNhatBangGiaoChiTieu, Me.btLuuKetQua})
        Me.BarManager1.MaxItemId = 20
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1, Me.rcbPhong, Me.RepositoryItemComboBox1, Me.rcbNhanVien})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btCapNhatCT, True), New DevExpress.XtraBars.LinkPersistInfo(Me.cbLoaiChiTieu, True), New DevExpress.XtraBars.LinkPersistInfo(Me.cbPhong), New DevExpress.XtraBars.LinkPersistInfo(Me.cbNhanVien), New DevExpress.XtraBars.LinkPersistInfo(Me.tbNam), New DevExpress.XtraBars.LinkPersistInfo(Me.btXem), New DevExpress.XtraBars.LinkPersistInfo(Me.btKetXuat, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btLuuKetQua, True)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'btCapNhatCT
        '
        Me.btCapNhatCT.Caption = "Cập nhật chỉ tiêu"
        Me.btCapNhatCT.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btCapNhatCT.Id = 4
        Me.btCapNhatCT.Name = "btCapNhatCT"
        Me.btCapNhatCT.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'cbLoaiChiTieu
        '
        Me.cbLoaiChiTieu.Caption = "Loại chỉ tiêu"
        Me.cbLoaiChiTieu.Edit = Me.RepositoryItemComboBox1
        Me.cbLoaiChiTieu.EditValue = "Phòng"
        Me.cbLoaiChiTieu.Id = 8
        Me.cbLoaiChiTieu.Name = "cbLoaiChiTieu"
        Me.cbLoaiChiTieu.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbLoaiChiTieu.Width = 103
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Phòng", "Nhân viên"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'cbPhong
        '
        Me.cbPhong.Caption = "Phòng"
        Me.cbPhong.Edit = Me.rcbPhong
        Me.cbPhong.Id = 7
        Me.cbPhong.Name = "cbPhong"
        Me.cbPhong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbPhong.Width = 170
        '
        'rcbPhong
        '
        Me.rcbPhong.AutoHeight = False
        Me.rcbPhong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbPhong.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.rcbPhong.DisplayMember = "Ten"
        Me.rcbPhong.DropDownItemHeight = 22
        Me.rcbPhong.Name = "rcbPhong"
        Me.rcbPhong.NullText = "[-Chọn phòng ban-]"
        Me.rcbPhong.ShowHeader = False
        Me.rcbPhong.ValueMember = "ID"
        '
        'cbNhanVien
        '
        Me.cbNhanVien.Caption = "Nhân viên"
        Me.cbNhanVien.Edit = Me.rcbNhanVien
        Me.cbNhanVien.Id = 10
        Me.cbNhanVien.Name = "cbNhanVien"
        Me.cbNhanVien.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Me.cbNhanVien.Width = 193
        '
        'rcbNhanVien
        '
        Me.rcbNhanVien.AutoHeight = False
        Me.rcbNhanVien.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbNhanVien.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name5", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name6")})
        Me.rcbNhanVien.DisplayMember = "Ten"
        Me.rcbNhanVien.DropDownItemHeight = 22
        Me.rcbNhanVien.Name = "rcbNhanVien"
        Me.rcbNhanVien.NullText = "[-Tất cả-]"
        Me.rcbNhanVien.ShowHeader = False
        Me.rcbNhanVien.ValueMember = "ID"
        '
        'tbNam
        '
        Me.tbNam.Caption = "Năm"
        Me.tbNam.Edit = Me.RepositoryItemSpinEdit1
        Me.tbNam.Id = 5
        Me.tbNam.Name = "tbNam"
        Me.tbNam.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'btXem
        '
        Me.btXem.Caption = "Xem"
        Me.btXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btXem.Id = 6
        Me.btXem.Name = "btXem"
        Me.btXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btKetXuat
        '
        Me.btKetXuat.Caption = "Kết xuất"
        Me.btKetXuat.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btKetXuat.Id = 2
        Me.btKetXuat.Name = "btKetXuat"
        Me.btKetXuat.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btLuuKetQua
        '
        Me.btLuuKetQua.Caption = "Lưu kết quả"
        Me.btLuuKetQua.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuKetQua.Id = 18
        Me.btLuuKetQua.Name = "btLuuKetQua"
        Me.btLuuKetQua.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1257, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 469)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1257, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 436)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1257, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 436)
        '
        'btCapNhatBangGiaoChiTieu
        '
        Me.btCapNhatBangGiaoChiTieu.Caption = "Cập nhật bảng giao chỉ tiêu"
        Me.btCapNhatBangGiaoChiTieu.Id = 15
        Me.btCapNhatBangGiaoChiTieu.Name = "btCapNhatBangGiaoChiTieu"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 0)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rMemoText, Me.rcbNhomChiTieu, Me.tbN0, Me.tbPT})
        Me.gdv.Size = New System.Drawing.Size(1251, 430)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.BandPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.BandPanel.Options.UseFont = True
        Me.gdvCT.Appearance.BandPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsCustomization.AllowFilter = False
        Me.gdvCT.OptionsFind.AllowFindPanel = False
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        '
        'rMemoText
        '
        Me.rMemoText.Name = "rMemoText"
        '
        'rcbNhomChiTieu
        '
        Me.rcbNhomChiTieu.AutoHeight = False
        Me.rcbNhomChiTieu.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbNhomChiTieu.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name4")})
        Me.rcbNhomChiTieu.DisplayMember = "NoiDung"
        Me.rcbNhomChiTieu.Name = "rcbNhomChiTieu"
        Me.rcbNhomChiTieu.ShowHeader = False
        Me.rcbNhomChiTieu.ValueMember = "ID"
        '
        'tbN0
        '
        Me.tbN0.AutoHeight = False
        Me.tbN0.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.tbN0.DisplayFormat.FormatString = "N0"
        Me.tbN0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbN0.EditFormat.FormatString = "N0"
        Me.tbN0.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbN0.Name = "tbN0"
        '
        'tbPT
        '
        Me.tbPT.AutoHeight = False
        Me.tbPT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.tbPT.DisplayFormat.FormatString = "{0:N2} %"
        Me.tbPT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.tbPT.EditFormat.FormatString = "{0:N2} %"
        Me.tbPT.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.tbPT.Name = "tbPT"
        '
        'tabCT
        '
        Me.tabCT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabCT.Location = New System.Drawing.Point(0, 33)
        Me.tabCT.Name = "tabCT"
        Me.tabCT.SelectedTabPage = Me.tabGiaoChiTieu
        Me.tabCT.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.tabCT.Size = New System.Drawing.Size(1257, 436)
        Me.tabCT.TabIndex = 9
        Me.tabCT.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabGiaoChiTieu, Me.tabCapNhatChiTieu})
        '
        'tabGiaoChiTieu
        '
        Me.tabGiaoChiTieu.Controls.Add(Me.gdv)
        Me.tabGiaoChiTieu.Name = "tabGiaoChiTieu"
        Me.tabGiaoChiTieu.Size = New System.Drawing.Size(1251, 430)
        Me.tabGiaoChiTieu.Text = "Giao chỉ tiêu"
        '
        'tabCapNhatChiTieu
        '
        Me.tabCapNhatChiTieu.Controls.Add(Me.gdvChiTieu)
        Me.tabCapNhatChiTieu.Controls.Add(Me.GroupControl1)
        Me.tabCapNhatChiTieu.Name = "tabCapNhatChiTieu"
        Me.tabCapNhatChiTieu.Size = New System.Drawing.Size(1251, 432)
        Me.tabCapNhatChiTieu.Text = "Cập nhật chỉ tiêu"
        '
        'gdvChiTieu
        '
        Me.gdvChiTieu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvChiTieu.Location = New System.Drawing.Point(0, 31)
        Me.gdvChiTieu.MainView = Me.gdvChiTieuCT
        Me.gdvChiTieu.Name = "gdvChiTieu"
        Me.gdvChiTieu.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1})
        Me.gdvChiTieu.Size = New System.Drawing.Size(1251, 401)
        Me.gdvChiTieu.TabIndex = 6
        Me.gdvChiTieu.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvChiTieuCT})
        '
        'gdvChiTieuCT
        '
        Me.gdvChiTieuCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvChiTieuCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvChiTieuCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvChiTieuCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvChiTieuCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvChiTieuCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvChiTieuCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvChiTieuCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvChiTieuCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvChiTieuCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvChiTieuCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvChiTieuCT.GridControl = Me.gdvChiTieu
        Me.gdvChiTieuCT.Name = "gdvChiTieuCT"
        Me.gdvChiTieuCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvChiTieuCT.OptionsFind.AllowFindPanel = False
        Me.gdvChiTieuCT.OptionsView.ColumnAutoWidth = False
        Me.gdvChiTieuCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvChiTieuCT.OptionsView.RowAutoHeight = True
        Me.gdvChiTieuCT.OptionsView.ShowFooter = True
        Me.gdvChiTieuCT.OptionsView.ShowGroupPanel = False
        Me.gdvChiTieuCT.RowHeight = 22
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.btDong)
        Me.GroupControl1.Controls.Add(Me.btThemChiTieu)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(1251, 31)
        Me.GroupControl1.TabIndex = 7
        Me.GroupControl1.Text = "GroupControl1"
        '
        'btDong
        '
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(127, 5)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 23)
        Me.btDong.TabIndex = 1
        Me.btDong.Text = "Đóng"
        '
        'btThemChiTieu
        '
        Me.btThemChiTieu.Image = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btThemChiTieu.Location = New System.Drawing.Point(5, 5)
        Me.btThemChiTieu.Name = "btThemChiTieu"
        Me.btThemChiTieu.Size = New System.Drawing.Size(116, 23)
        Me.btThemChiTieu.TabIndex = 0
        Me.btThemChiTieu.Text = "Cập nhật chỉ tiêu"
        '
        'frmChiTieu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tabCT)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmChiTieu"
        Me.Size = New System.Drawing.Size(1257, 469)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNhanVien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNhomChiTieu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbN0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCT.ResumeLayout(False)
        Me.tabGiaoChiTieu.ResumeLayout(False)
        Me.tabCapNhatChiTieu.ResumeLayout(False)
        CType(Me.gdvChiTieu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChiTieuCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents rMemoText As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents btKetXuat As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btCapNhatCT As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbNam As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents btXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cbPhong As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbPhong As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents rcbNhomChiTieu As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents tbN0 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents cbLoaiChiTieu As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents cbNhanVien As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbNhanVien As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents tabCT As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabGiaoChiTieu As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabCapNhatChiTieu As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gdvChiTieu As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvChiTieuCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btThemChiTieu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btCapNhatBangGiaoChiTieu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents btLuuKetQua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbPT As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

End Class
