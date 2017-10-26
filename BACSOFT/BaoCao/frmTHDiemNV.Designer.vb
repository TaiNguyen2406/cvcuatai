<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTHDiemNV
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
        Me.tbTuThang = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.tbDenThang = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.cbPhong = New DevExpress.XtraBars.BarEditItem()
        Me.rcbPhong = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btXem = New DevExpress.XtraBars.BarButtonItem()
        Me.btXuatExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.rtbTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.rtbDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemMonth1 = New DevExpress.XtraScheduler.UI.RepositoryItemMonth()
        Me.rcbThang = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.rtbNam = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.rcbTieuChi = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.RepositoryItemComboBox2 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.tbN2 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMonth1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbThang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbNam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbTieuChi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbN2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btXem, Me.cbPhong, Me.tbTuThang, Me.tbDenThang, Me.btXuatExcel})
        Me.BarManager1.MaxItemId = 19
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rtbTuNgay, Me.rtbDenNgay, Me.RepositoryItemMonth1, Me.rcbThang, Me.rtbNam, Me.rcbTieuChi, Me.RepositoryItemComboBox1, Me.rcbPhong, Me.RepositoryItemSpinEdit1, Me.RepositoryItemDateEdit1, Me.RepositoryItemDateEdit2, Me.RepositoryItemComboBox2})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tbTuThang), New DevExpress.XtraBars.LinkPersistInfo(Me.tbDenThang), New DevExpress.XtraBars.LinkPersistInfo(Me.cbPhong), New DevExpress.XtraBars.LinkPersistInfo(Me.btXem), New DevExpress.XtraBars.LinkPersistInfo(Me.btXuatExcel)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'tbTuThang
        '
        Me.tbTuThang.Caption = "Từ tháng"
        Me.tbTuThang.Edit = Me.RepositoryItemDateEdit1
        Me.tbTuThang.Id = 15
        Me.tbTuThang.Name = "tbTuThang"
        Me.tbTuThang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbTuThang.Width = 79
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "MM/yyyy"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'tbDenThang
        '
        Me.tbDenThang.Caption = "Đến tháng"
        Me.tbDenThang.Edit = Me.RepositoryItemDateEdit2
        Me.tbDenThang.Id = 16
        Me.tbDenThang.Name = "tbDenThang"
        Me.tbDenThang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbDenThang.Width = 73
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.DisplayFormat.FormatString = "MM/yyyy"
        Me.RepositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit2.EditFormat.FormatString = "MM/yyyy"
        Me.RepositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit2.Mask.EditMask = "MM/yyyy"
        Me.RepositoryItemDateEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'cbPhong
        '
        Me.cbPhong.Caption = "Phòng"
        Me.cbPhong.Edit = Me.rcbPhong
        Me.cbPhong.Id = 13
        Me.cbPhong.Name = "cbPhong"
        Me.cbPhong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbPhong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Me.cbPhong.Width = 206
        '
        'rcbPhong
        '
        Me.rcbPhong.AutoHeight = False
        Me.rcbPhong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbPhong.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name9", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name10")})
        Me.rcbPhong.DisplayMember = "Ten"
        Me.rcbPhong.DropDownItemHeight = 22
        Me.rcbPhong.Name = "rcbPhong"
        Me.rcbPhong.NullText = "[Tất cả]"
        Me.rcbPhong.ShowHeader = False
        Me.rcbPhong.ValueMember = "ID"
        '
        'btXem
        '
        Me.btXem.Caption = "Xem"
        Me.btXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btXem.Id = 5
        Me.btXem.Name = "btXem"
        Me.btXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btXuatExcel
        '
        Me.btXuatExcel.Caption = "Xuất Excel"
        Me.btXuatExcel.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btXuatExcel.Id = 18
        Me.btXuatExcel.Name = "btXuatExcel"
        Me.btXuatExcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
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
        'rcbThang
        '
        Me.rcbThang.AutoHeight = False
        Me.rcbThang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbThang.DropDownItemHeight = 22
        Me.rcbThang.DropDownRows = 12
        Me.rcbThang.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.rcbThang.Name = "rcbThang"
        '
        'rtbNam
        '
        Me.rtbNam.AutoHeight = False
        Me.rtbNam.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.rtbNam.IsFloatValue = False
        Me.rtbNam.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.rtbNam.Name = "rtbNam"
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
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit1.DisplayFormat.FormatString = "MM/yyyy"
        Me.RepositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemSpinEdit1.EditFormat.FormatString = "MM/yyyy"
        Me.RepositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemSpinEdit1.Mask.EditMask = "{0:MM/yyyy}"
        Me.RepositoryItemSpinEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'RepositoryItemComboBox2
        '
        Me.RepositoryItemComboBox2.AutoHeight = False
        Me.RepositoryItemComboBox2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox2.DropDownItemHeight = 22
        Me.RepositoryItemComboBox2.Items.AddRange(New Object() {"Phiếu thu", "Xuất kho"})
        Me.RepositoryItemComboBox2.Name = "RepositoryItemComboBox2"
        Me.RepositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 33)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.tbN2})
        Me.gdv.Size = New System.Drawing.Size(1189, 439)
        Me.gdv.TabIndex = 6
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.GroupFooter.Options.UseFont = True
        Me.gdvCT.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.gdvCT.Appearance.GroupRow.Options.UseFont = True
        Me.gdvCT.Appearance.GroupRow.Options.UseForeColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsBehavior.ReadOnly = True
        Me.gdvCT.OptionsFind.AllowFindPanel = False
        Me.gdvCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        Me.gdvCT.RowHeight = 22
        '
        'tbN2
        '
        Me.tbN2.AutoHeight = False
        Me.tbN2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.tbN2.DisplayFormat.FormatString = "N0"
        Me.tbN2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbN2.EditFormat.FormatString = "N0"
        Me.tbN2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbN2.Name = "tbN2"
        '
        'frmTHDiemNV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmTHDiemNV"
        Me.Size = New System.Drawing.Size(1189, 472)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMonth1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbThang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbNam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbTieuChi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbN2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rtbTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents rtbDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemMonth1 As DevExpress.XtraScheduler.UI.RepositoryItemMonth
    Friend WithEvents rcbThang As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents rtbNam As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents rcbTieuChi As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents cbPhong As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbPhong As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents tbTuThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents tbDenThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemComboBox2 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents btXuatExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbN2 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit

End Class
