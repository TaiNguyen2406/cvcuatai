<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChiTietCCDC
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
        Me.gc = New DevExpress.XtraGrid.GridControl()
        Me.gv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.id = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gColTenTS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gColIdTinhTrang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTinhTrang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.riTxtMaVT = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.riDeTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.riDeDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.riCbbXem = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.riCbbTrangThai = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.riLueNhomVT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.riLueHang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.riLueTenVT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.riLueLoaiTS = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.ricbbTinhTrang = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.pMenu = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.gc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riTxtMaVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbXem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbTrangThai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueNhomVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueHang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTenVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueLoaiTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ricbbTinhTrang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gc
        '
        Me.gc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc.Location = New System.Drawing.Point(0, 0)
        Me.gc.MainView = Me.gv
        Me.gc.Name = "gc"
        Me.gc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riLueTinhTrang})
        Me.gc.Size = New System.Drawing.Size(531, 398)
        Me.gc.TabIndex = 1
        Me.gc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv})
        '
        'gv
        '
        Me.gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gv.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gv.Appearance.HeaderPanel.Options.UseFont = True
        Me.gv.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gv.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.id, Me.GridColumn1, Me.gColTenTS, Me.GridColumn2, Me.gColIdTinhTrang, Me.GridColumn3})
        Me.gv.GridControl = Me.gc
        Me.gv.Name = "gv"
        Me.gv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gv.OptionsView.AllowCellMerge = True
        Me.gv.OptionsView.EnableAppearanceEvenRow = True
        Me.gv.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gv.OptionsView.ShowFooter = True
        Me.gv.OptionsView.ShowGroupPanel = False
        Me.gv.OptionsView.ShowIndicator = False
        Me.gv.RowHeight = 23
        '
        'id
        '
        Me.id.Caption = "id"
        Me.id.FieldName = "id"
        Me.id.Name = "id"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Mã tài sản"
        Me.GridColumn1.FieldName = "idtaisan"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'gColTenTS
        '
        Me.gColTenTS.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gColTenTS.AppearanceCell.Options.UseFont = True
        Me.gColTenTS.AppearanceCell.Options.UseTextOptions = True
        Me.gColTenTS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gColTenTS.Caption = "Tên CCDC"
        Me.gColTenTS.FieldName = "TenVT"
        Me.gColTenTS.Name = "gColTenTS"
        Me.gColTenTS.OptionsColumn.AllowEdit = False
        Me.gColTenTS.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.gColTenTS.SummaryItem.DisplayFormat = "{0} "
        Me.gColTenTS.SummaryItem.FieldName = "tentaisan"
        Me.gColTenTS.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gColTenTS.SummaryItem.Tag = 1
        Me.gColTenTS.Visible = True
        Me.gColTenTS.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Chi tiết CCDC"
        Me.GridColumn2.FieldName = "tenchitietccdc"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'gColIdTinhTrang
        '
        Me.gColIdTinhTrang.Caption = "Tình trạng"
        Me.gColIdTinhTrang.ColumnEdit = Me.riLueTinhTrang
        Me.gColIdTinhTrang.FieldName = "idtinhtrang"
        Me.gColIdTinhTrang.Name = "gColIdTinhTrang"
        Me.gColIdTinhTrang.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.gColIdTinhTrang.Visible = True
        Me.gColIdTinhTrang.VisibleIndex = 2
        '
        'riLueTinhTrang
        '
        Me.riLueTinhTrang.AutoHeight = False
        Me.riLueTinhTrang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTinhTrang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentinhtrang", "Tên")})
        Me.riLueTinhTrang.DisplayMember = "tentinhtrang"
        Me.riLueTinhTrang.Name = "riLueTinhTrang"
        Me.riLueTinhTrang.NullText = ""
        Me.riLueTinhTrang.ShowHeader = False
        Me.riLueTinhTrang.ValueMember = "id"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Ngày thanh lý"
        Me.GridColumn3.FieldName = "ngaythanhly"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2, Me.btnXoa})
        Me.BarManager1.MaxItemId = 46
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riTxtMaVT, Me.riDeTuNgay, Me.riDeDenNgay, Me.riCbbXem, Me.riCbbTrangThai, Me.riLueNhomVT, Me.riLueHang, Me.riLueTenVT, Me.riLueLoaiTS, Me.ricbbTinhTrang})
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(531, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 398)
        Me.barDockControlBottom.Size = New System.Drawing.Size(531, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 398)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(531, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 398)
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
        'btnXoa
        '
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_24
        Me.btnXoa.Id = 44
        Me.btnXoa.Name = "btnXoa"
        '
        'riTxtMaVT
        '
        Me.riTxtMaVT.AutoHeight = False
        Me.riTxtMaVT.Name = "riTxtMaVT"
        '
        'riDeTuNgay
        '
        Me.riDeTuNgay.AutoHeight = False
        Me.riDeTuNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riDeTuNgay.EditFormat.FormatString = "yyyy/MM/dd"
        Me.riDeTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.riDeTuNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.riDeTuNgay.Mask.UseMaskAsDisplayFormat = True
        Me.riDeTuNgay.Name = "riDeTuNgay"
        Me.riDeTuNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'riDeDenNgay
        '
        Me.riDeDenNgay.AutoHeight = False
        Me.riDeDenNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riDeDenNgay.EditFormat.FormatString = "yyyy/MM/dd"
        Me.riDeDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.riDeDenNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.riDeDenNgay.Mask.UseMaskAsDisplayFormat = True
        Me.riDeDenNgay.Name = "riDeDenNgay"
        Me.riDeDenNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'riCbbXem
        '
        Me.riCbbXem.AutoHeight = False
        Me.riCbbXem.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riCbbXem.Items.AddRange(New Object() {"Top 500", "Tất cả", "Tuỳ chỉnh"})
        Me.riCbbXem.Name = "riCbbXem"
        '
        'riCbbTrangThai
        '
        Me.riCbbTrangThai.AutoHeight = False
        Me.riCbbTrangThai.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riCbbTrangThai.Items.AddRange(New Object() {"Tất cả", "Hàng hóa", "Công cụ, dụng cụ", "Tài sản"})
        Me.riCbbTrangThai.Name = "riCbbTrangThai"
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
        'riLueLoaiTS
        '
        Me.riLueLoaiTS.AutoHeight = False
        Me.riLueLoaiTS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueLoaiTS.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenloaitaisan", "Tên")})
        Me.riLueLoaiTS.DisplayMember = "tenloaitaisan"
        Me.riLueLoaiTS.Name = "riLueLoaiTS"
        Me.riLueLoaiTS.NullText = ""
        Me.riLueLoaiTS.ShowHeader = False
        Me.riLueLoaiTS.ValueMember = "id"
        '
        'ricbbTinhTrang
        '
        Me.ricbbTinhTrang.AutoHeight = False
        Me.ricbbTinhTrang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ricbbTinhTrang.Items.AddRange(New Object() {"Tất cả", "Còn khấu hao", "Khấu hao hết"})
        Me.ricbbTinhTrang.Name = "ricbbTinhTrang"
        '
        'pMenu
        '
        Me.pMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnXoa)})
        Me.pMenu.Manager = Me.BarManager1
        Me.pMenu.Name = "pMenu"
        '
        'frmChiTietCCDC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 398)
        Me.Controls.Add(Me.gc)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmChiTietCCDC"
        Me.Text = "Chi tiết công cụ, dụng cụ"
        CType(Me.gc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riTxtMaVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbXem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbTrangThai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueNhomVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueHang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTenVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueLoaiTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ricbbTinhTrang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents id As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gColTenTS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gColIdTinhTrang As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents riLueTinhTrang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents riTxtMaVT As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents riDeTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents riDeDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents riCbbXem As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents riCbbTrangThai As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents riLueNhomVT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents riLueHang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents riLueTenVT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents riLueLoaiTS As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents ricbbTinhTrang As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents pMenu As DevExpress.XtraBars.PopupMenu
End Class
