<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChamCong
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
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.cbThang = New DevExpress.XtraBars.BarEditItem()
        Me.rcbThang = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.tbNam = New DevExpress.XtraBars.BarEditItem()
        Me.rtbNam = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.btXem = New DevExpress.XtraBars.BarButtonItem()
        Me.mKetXuat = New DevExpress.XtraBars.BarButtonItem()
        Me.btDongBoDuLieu = New DevExpress.XtraBars.BarSubItem()
        Me.mDongBoTenNhanVien = New DevExpress.XtraBars.BarButtonItem()
        Me.mDongBoDuLieuChamCong = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.rtbTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.rtbDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemMonth1 = New DevExpress.XtraScheduler.UI.RepositoryItemMonth()
        Me.rcbTieuChi = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.gdvChamCong = New DevExpress.XtraGrid.GridControl()
        Me.gdvChamCongCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbThang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbNam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMonth1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbTieuChi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChamCong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChamCongCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btXem, Me.cbThang, Me.tbNam, Me.btDongBoDuLieu, Me.mDongBoTenNhanVien, Me.mDongBoDuLieuChamCong, Me.mKetXuat})
        Me.BarManager1.MaxItemId = 20
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rtbTuNgay, Me.rtbDenNgay, Me.RepositoryItemMonth1, Me.rcbThang, Me.rtbNam, Me.rcbTieuChi, Me.RepositoryItemComboBox1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cbThang), New DevExpress.XtraBars.LinkPersistInfo(Me.tbNam), New DevExpress.XtraBars.LinkPersistInfo(Me.btXem), New DevExpress.XtraBars.LinkPersistInfo(Me.mKetXuat), New DevExpress.XtraBars.LinkPersistInfo(Me.btDongBoDuLieu)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'cbThang
        '
        Me.cbThang.Caption = "Tháng"
        Me.cbThang.Edit = Me.rcbThang
        Me.cbThang.Id = 7
        Me.cbThang.Name = "cbThang"
        Me.cbThang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'rcbThang
        '
        Me.rcbThang.AutoHeight = False
        Me.rcbThang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbThang.DropDownItemHeight = 22
        Me.rcbThang.DropDownRows = 12
        Me.rcbThang.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.rcbThang.Name = "rcbThang"
        '
        'tbNam
        '
        Me.tbNam.Caption = "Năm"
        Me.tbNam.Edit = Me.rtbNam
        Me.tbNam.Id = 8
        Me.tbNam.Name = "tbNam"
        Me.tbNam.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbNam.Width = 65
        '
        'rtbNam
        '
        Me.rtbNam.AutoHeight = False
        Me.rtbNam.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.rtbNam.IsFloatValue = False
        Me.rtbNam.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.rtbNam.Name = "rtbNam"
        '
        'btXem
        '
        Me.btXem.Caption = "Xem"
        Me.btXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btXem.Id = 5
        Me.btXem.Name = "btXem"
        Me.btXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mKetXuat
        '
        Me.mKetXuat.Caption = "Kết xuất"
        Me.mKetXuat.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.mKetXuat.Id = 19
        Me.mKetXuat.Name = "mKetXuat"
        Me.mKetXuat.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btDongBoDuLieu
        '
        Me.btDongBoDuLieu.Caption = "Đồng bộ dữ liệu"
        Me.btDongBoDuLieu.Id = 16
        Me.btDongBoDuLieu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mDongBoTenNhanVien), New DevExpress.XtraBars.LinkPersistInfo(Me.mDongBoDuLieuChamCong)})
        Me.btDongBoDuLieu.Name = "btDongBoDuLieu"
        Me.btDongBoDuLieu.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mDongBoTenNhanVien
        '
        Me.mDongBoTenNhanVien.Caption = "Tên nhân viên"
        Me.mDongBoTenNhanVien.Id = 17
        Me.mDongBoTenNhanVien.Name = "mDongBoTenNhanVien"
        '
        'mDongBoDuLieuChamCong
        '
        Me.mDongBoDuLieuChamCong.Caption = "Dữ liệu chấm công"
        Me.mDongBoDuLieuChamCong.Id = 18
        Me.mDongBoDuLieuChamCong.Name = "mDongBoDuLieuChamCong"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1189, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 472)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1189, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 439)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1189, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 439)
        '
        'rtbTuNgay
        '
        Me.rtbTuNgay.AutoHeight = False
        Me.rtbTuNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rtbTuNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbTuNgay.EditFormat.FormatString = "dd/MM/yyyy"
        Me.rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbTuNgay.Name = "rtbTuNgay"
        Me.rtbTuNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'rtbDenNgay
        '
        Me.rtbDenNgay.AutoHeight = False
        Me.rtbDenNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rtbDenNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.rtbDenNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbDenNgay.EditFormat.FormatString = "dd/MM/yyyy"
        Me.rtbDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbDenNgay.Name = "rtbDenNgay"
        Me.rtbDenNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'RepositoryItemMonth1
        '
        Me.RepositoryItemMonth1.AutoHeight = False
        Me.RepositoryItemMonth1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMonth1.Name = "RepositoryItemMonth1"
        '
        'rcbTieuChi
        '
        Me.rcbTieuChi.AutoHeight = False
        Me.rcbTieuChi.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbTieuChi.DropDownItemHeight = 22
        Me.rcbTieuChi.Items.AddRange(New Object() {"Xuất kho", "Đã thu tiền"})
        Me.rcbTieuChi.Name = "rcbTieuChi"
        Me.rcbTieuChi.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.DropDownRows = 2
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"Đã duyệt", "Đã báo cáo"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'gdvChamCong
        '
        Me.gdvChamCong.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvChamCong.Location = New System.Drawing.Point(0, 33)
        Me.gdvChamCong.MainView = Me.gdvChamCongCT
        Me.gdvChamCong.Name = "gdvChamCong"
        Me.gdvChamCong.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateEdit1})
        Me.gdvChamCong.Size = New System.Drawing.Size(1189, 439)
        Me.gdvChamCong.TabIndex = 11
        Me.gdvChamCong.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvChamCongCT})
        '
        'gdvChamCongCT
        '
        Me.gdvChamCongCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvChamCongCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvChamCongCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvChamCongCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvChamCongCT.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.gdvChamCongCT.Appearance.GroupFooter.Options.UseFont = True
        Me.gdvChamCongCT.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gdvChamCongCT.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.gdvChamCongCT.Appearance.GroupRow.Options.UseFont = True
        Me.gdvChamCongCT.Appearance.GroupRow.Options.UseForeColor = True
        Me.gdvChamCongCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvChamCongCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvChamCongCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvChamCongCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvChamCongCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvChamCongCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvChamCongCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvChamCongCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8, Me.GridColumn4, Me.GridColumn1, Me.GridColumn2, Me.GridColumn5, Me.GridColumn3, Me.GridColumn6, Me.GridColumn7, Me.GridColumn9, Me.GridColumn10})
        Me.gdvChamCongCT.GridControl = Me.gdvChamCong
        Me.gdvChamCongCT.GroupCount = 2
        Me.gdvChamCongCT.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "CheckTime", Me.GridColumn2, "{0:N0}")})
        Me.gdvChamCongCT.Name = "gdvChamCongCT"
        Me.gdvChamCongCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvChamCongCT.OptionsBehavior.ReadOnly = True
        Me.gdvChamCongCT.OptionsView.ColumnAutoWidth = False
        Me.gdvChamCongCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvChamCongCT.OptionsView.ShowFooter = True
        Me.gdvChamCongCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn8, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn4, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Phòng"
        Me.GridColumn8.FieldName = "Phong"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nhân viên"
        Me.GridColumn4.FieldName = "Ten"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "UserID"
        Me.GridColumn1.FieldName = "ID"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Ngày"
        Me.GridColumn2.DisplayFormat.FormatString = "dd/MM/yyyy - ddd"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "ThoiGian"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn2.SummaryItem.FieldName = "CheckTime"
        Me.GridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 160
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Lần 1"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn5.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn5.FieldName = "Lan1"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 52
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "HH:mm"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "HH:mm"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "HH:mm"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Lần 2"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn3.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "Lan2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 54
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Lần 3"
        Me.GridColumn6.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn6.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn6.FieldName = "Lan3"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 54
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Lần 4"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn7.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn7.FieldName = "Lan4"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 4
        Me.GridColumn7.Width = 56
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Lần 5"
        Me.GridColumn9.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn9.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "Lan5"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 5
        Me.GridColumn9.Width = 56
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Lần 6"
        Me.GridColumn10.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn10.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn10.FieldName = "Lan6"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 6
        Me.GridColumn10.Width = 59
        '
        'frmChamCong
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdvChamCong)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmChamCong"
        Me.Size = New System.Drawing.Size(1189, 472)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbThang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbNam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMonth1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbTieuChi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChamCong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChamCongCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents rtbTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents rtbDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemMonth1 As DevExpress.XtraScheduler.UI.RepositoryItemMonth
    Friend WithEvents cbThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbThang As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents tbNam As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rtbNam As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents rcbTieuChi As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents btDongBoDuLieu As DevExpress.XtraBars.BarSubItem
    Friend WithEvents mDongBoTenNhanVien As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mDongBoDuLieuChamCong As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents gdvChamCong As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvChamCongCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mKetXuat As DevExpress.XtraBars.BarButtonItem

End Class
