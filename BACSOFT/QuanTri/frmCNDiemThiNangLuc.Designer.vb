<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNDiemThiNangLuc
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.tbNgayThi = New DevExpress.XtraEditors.DateEdit()
        Me.gdvNhanVien = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhanVienCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colNhanVien = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rgdvHocVien = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cbNangLuc = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cbNhom = New DevExpress.XtraEditors.LookUpEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbGhiChu = New DevExpress.XtraEditors.MemoEdit()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.pMenuNV = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.mThemNV = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThemFile = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoaFile = New DevExpress.XtraBars.BarButtonItem()
        Me.pMenuFile = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.tbNgayThi.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNgayThi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhanVien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhanVienCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgdvHocVien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNangLuc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGhiChu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pMenuNV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pMenuFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 41)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tên môn thi"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(13, 67)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Ngày thi"
        '
        'btLuu
        '
        Me.btLuu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuu.Appearance.Options.UseFont = True
        Me.btLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuu.Location = New System.Drawing.Point(572, 474)
        Me.btLuu.Name = "btLuu"
        Me.btLuu.Size = New System.Drawing.Size(76, 31)
        Me.btLuu.TabIndex = 5
        Me.btLuu.Text = "Lưu"
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(654, 474)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(79, 31)
        Me.btDong.TabIndex = 7
        Me.btDong.Text = "Đóng"
        '
        'tbNgayThi
        '
        Me.tbNgayThi.EditValue = Nothing
        Me.tbNgayThi.Location = New System.Drawing.Point(77, 64)
        Me.tbNgayThi.Name = "tbNgayThi"
        Me.tbNgayThi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbNgayThi.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgayThi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgayThi.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgayThi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgayThi.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.tbNgayThi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbNgayThi.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbNgayThi.Size = New System.Drawing.Size(100, 20)
        Me.tbNgayThi.TabIndex = 8
        '
        'gdvNhanVien
        '
        Me.gdvNhanVien.Location = New System.Drawing.Point(474, 12)
        Me.gdvNhanVien.MainView = Me.gdvNhanVienCT
        Me.gdvNhanVien.Name = "gdvNhanVien"
        Me.gdvNhanVien.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rgdvHocVien})
        Me.gdvNhanVien.Size = New System.Drawing.Size(259, 444)
        Me.gdvNhanVien.TabIndex = 9
        Me.gdvNhanVien.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhanVienCT})
        '
        'gdvNhanVienCT
        '
        Me.gdvNhanVienCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhanVienCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNhanVienCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhanVienCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNhanVienCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhanVienCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhanVienCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhanVienCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhanVienCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhanVienCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvNhanVienCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ID, Me.colNhanVien, Me.GridColumn4, Me.GridColumn1})
        Me.gdvNhanVienCT.GridControl = Me.gdvNhanVien
        Me.gdvNhanVienCT.Name = "gdvNhanVienCT"
        Me.gdvNhanVienCT.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gdvNhanVienCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvNhanVienCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvNhanVienCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhanVienCT.OptionsView.ShowFooter = True
        Me.gdvNhanVienCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhanVienCT.RowHeight = 22
        '
        'ID
        '
        Me.ID.Caption = "GridColumn1"
        Me.ID.FieldName = "ID"
        Me.ID.Name = "ID"
        '
        'colNhanVien
        '
        Me.colNhanVien.Caption = "Nhân viên"
        Me.colNhanVien.ColumnEdit = Me.rgdvHocVien
        Me.colNhanVien.FieldName = "IDNhanVien"
        Me.colNhanVien.Name = "colNhanVien"
        Me.colNhanVien.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.colNhanVien.Visible = True
        Me.colNhanVien.VisibleIndex = 0
        Me.colNhanVien.Width = 243
        '
        'rgdvHocVien
        '
        Me.rgdvHocVien.AutoHeight = False
        Me.rgdvHocVien.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rgdvHocVien.DisplayMember = "Ten"
        Me.rgdvHocVien.Name = "rgdvHocVien"
        Me.rgdvHocVien.NullText = ""
        Me.rgdvHocVien.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.rgdvHocVien.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rgdvHocVien.ValueMember = "ID"
        Me.rgdvHocVien.View = Me.GridView2
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Điểm"
        Me.GridColumn4.FieldName = "Diem"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Modify"
        Me.GridColumn1.FieldName = "Modify"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'cbNangLuc
        '
        Me.cbNangLuc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbNangLuc.Location = New System.Drawing.Point(77, 38)
        Me.cbNangLuc.Name = "cbNangLuc"
        Me.cbNangLuc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbNangLuc.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Ten")})
        Me.cbNangLuc.Properties.DisplayMember = "Ten"
        Me.cbNangLuc.Properties.DropDownItemHeight = 22
        Me.cbNangLuc.Properties.NullText = "[Chọn năng lực]"
        Me.cbNangLuc.Properties.ShowHeader = False
        Me.cbNangLuc.Properties.ValueMember = "ID"
        Me.cbNangLuc.Size = New System.Drawing.Size(391, 20)
        Me.cbNangLuc.TabIndex = 11
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(13, 15)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Nhóm"
        '
        'cbNhom
        '
        Me.cbNhom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbNhom.Location = New System.Drawing.Point(77, 12)
        Me.cbNhom.Name = "cbNhom"
        Me.cbNhom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbNhom.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "NoiDung")})
        Me.cbNhom.Properties.DisplayMember = "NoiDung"
        Me.cbNhom.Properties.DropDownItemHeight = 22
        Me.cbNhom.Properties.NullText = "[Chọn nhóm]"
        Me.cbNhom.Properties.ShowHeader = False
        Me.cbNhom.Properties.ValueMember = "Ma"
        Me.cbNhom.Size = New System.Drawing.Size(391, 20)
        Me.cbNhom.TabIndex = 11
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.SimpleButton1.Location = New System.Drawing.Point(458, 474)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(108, 31)
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.Text = "Thêm mới"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 106)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Ghi chú"
        '
        'tbGhiChu
        '
        Me.tbGhiChu.Location = New System.Drawing.Point(77, 91)
        Me.tbGhiChu.Name = "tbGhiChu"
        Me.tbGhiChu.Size = New System.Drawing.Size(391, 96)
        Me.tbGhiChu.TabIndex = 12
        '
        'gdvFile
        '
        Me.gdvFile.Location = New System.Drawing.Point(77, 193)
        Me.gdvFile.MainView = Me.gdvFileCT
        Me.gdvFile.Name = "gdvFile"
        Me.gdvFile.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHyperLinkEdit1})
        Me.gdvFile.Size = New System.Drawing.Size(391, 263)
        Me.gdvFile.TabIndex = 13
        Me.gdvFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvFileCT})
        '
        'gdvFileCT
        '
        Me.gdvFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFile})
        Me.gdvFileCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvFileCT.GridControl = Me.gdvFile
        Me.gdvFileCT.Name = "gdvFileCT"
        Me.gdvFileCT.OptionsBehavior.Editable = False
        Me.gdvFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvFileCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvFileCT.RowHeight = 22
        '
        'colFile
        '
        Me.colFile.Caption = "File"
        Me.colFile.ColumnEdit = Me.RepositoryItemHyperLinkEdit1
        Me.colFile.FieldName = "File"
        Me.colFile.Name = "colFile"
        Me.colFile.Visible = True
        Me.colFile.VisibleIndex = 0
        Me.colFile.Width = 500
        '
        'RepositoryItemHyperLinkEdit1
        '
        Me.RepositoryItemHyperLinkEdit1.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit1.Name = "RepositoryItemHyperLinkEdit1"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(12, 210)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(16, 13)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "File"
        '
        'pMenuNV
        '
        Me.pMenuNV.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThemNV), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoa)})
        Me.pMenuNV.Manager = Me.BarManager1
        Me.pMenuNV.Name = "pMenuNV"
        '
        'mThemNV
        '
        Me.mThemNV.Caption = "Thêm"
        Me.mThemNV.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThemNV.Id = 0
        Me.mThemNV.Name = "mThemNV"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xóa"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 1
        Me.mXoa.Name = "mXoa"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mThemNV, Me.mXoa, Me.mThemFile, Me.mXoaFile})
        Me.BarManager1.MaxItemId = 4
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(745, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 517)
        Me.barDockControlBottom.Size = New System.Drawing.Size(745, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 517)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(745, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 517)
        '
        'mThemFile
        '
        Me.mThemFile.Caption = "Thêm file"
        Me.mThemFile.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThemFile.Id = 2
        Me.mThemFile.Name = "mThemFile"
        '
        'mXoaFile
        '
        Me.mXoaFile.Caption = "Xóa file"
        Me.mXoaFile.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoaFile.Id = 3
        Me.mXoaFile.Name = "mXoaFile"
        '
        'pMenuFile
        '
        Me.pMenuFile.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThemFile), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoaFile)})
        Me.pMenuFile.Manager = Me.BarManager1
        Me.pMenuFile.Name = "pMenuFile"
        '
        'frmCNDiemThiNangLuc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 517)
        Me.Controls.Add(Me.gdvFile)
        Me.Controls.Add(Me.tbGhiChu)
        Me.Controls.Add(Me.gdvNhanVien)
        Me.Controls.Add(Me.cbNhom)
        Me.Controls.Add(Me.cbNangLuc)
        Me.Controls.Add(Me.tbNgayThi)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmCNDiemThiNangLuc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật điểm thi năng lực"
        CType(Me.tbNgayThi.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNgayThi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhanVien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhanVienCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgdvHocVien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNangLuc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGhiChu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pMenuNV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pMenuFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbNgayThi As DevExpress.XtraEditors.DateEdit
    Friend WithEvents gdvNhanVien As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNhanVienCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colNhanVien As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rgdvHocVien As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cbNangLuc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbNhom As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbGhiChu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pMenuNV As DevExpress.XtraBars.PopupMenu
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents mThemNV As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mThemFile As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoaFile As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pMenuFile As DevExpress.XtraBars.PopupMenu
End Class
