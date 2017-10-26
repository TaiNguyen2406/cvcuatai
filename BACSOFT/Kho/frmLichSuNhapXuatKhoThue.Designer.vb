<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLichSuNhapXuatKhoThue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLichSuNhapXuatKhoThue))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.txtSoLuongCuoiKy = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.txtSoLuongDauKy = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.txtThanhTienTonDauKy = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.txtThanhTienTonCuoiKy = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoExEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.txtSQL = New DevExpress.XtraEditors.MemoEdit()
        Me.tabMain = New DevExpress.XtraTab.XtraTabControl()
        Me.tabHeThongThue = New DevExpress.XtraTab.XtraTabPage()
        Me.tabHeThongThuc = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSQL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.tabHeThongThue.SuspendLayout()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2, Me.Bar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.txtSoLuongCuoiKy, Me.txtThanhTienTonDauKy, Me.BarButtonItem1, Me.txtSoLuongDauKy, Me.txtThanhTienTonCuoiKy, Me.BarEditItem1})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 6
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1, Me.RepositoryItemSpinEdit2, Me.RepositoryItemSpinEdit3, Me.RepositoryItemSpinEdit4, Me.RepositoryItemSpinEdit5})
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.txtSoLuongCuoiKy), New DevExpress.XtraBars.LinkPersistInfo(Me.BarButtonItem1)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'txtSoLuongCuoiKy
        '
        Me.txtSoLuongCuoiKy.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtSoLuongCuoiKy.Appearance.ForeColor = System.Drawing.Color.Teal
        Me.txtSoLuongCuoiKy.Appearance.Options.UseFont = True
        Me.txtSoLuongCuoiKy.Appearance.Options.UseForeColor = True
        Me.txtSoLuongCuoiKy.Caption = "Tồn cuối  kỳ:"
        Me.txtSoLuongCuoiKy.Edit = Me.RepositoryItemSpinEdit1
        Me.txtSoLuongCuoiKy.EditValue = "0"
        Me.txtSoLuongCuoiKy.Glyph = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.txtSoLuongCuoiKy.Id = 0
        Me.txtSoLuongCuoiKy.Name = "txtSoLuongCuoiKy"
        Me.txtSoLuongCuoiKy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtSoLuongCuoiKy.Width = 97
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AppearanceReadOnly.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSpinEdit1.AppearanceReadOnly.Options.UseFont = True
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit1.DisplayFormat.FormatString = "N2"
        Me.RepositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit1.EditFormat.FormatString = "N2"
        Me.RepositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit1.Mask.EditMask = "N2"
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        Me.RepositoryItemSpinEdit1.ReadOnly = True
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.BarButtonItem1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BarButtonItem1.Appearance.Options.UseFont = True
        Me.BarButtonItem1.Appearance.Options.UseForeColor = True
        Me.BarButtonItem1.Caption = "Đóng"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.close_18
        Me.BarButtonItem1.Id = 2
        Me.BarButtonItem1.Name = "BarButtonItem1"
        Me.BarButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.txtSoLuongDauKy)})
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'txtSoLuongDauKy
        '
        Me.txtSoLuongDauKy.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtSoLuongDauKy.Appearance.Options.UseFont = True
        Me.txtSoLuongDauKy.Caption = "Tồn đầu kỳ"
        Me.txtSoLuongDauKy.Edit = Me.RepositoryItemSpinEdit3
        Me.txtSoLuongDauKy.EditValue = "0"
        Me.txtSoLuongDauKy.Glyph = Global.BACSOFT.My.Resources.Resources.calendar_18
        Me.txtSoLuongDauKy.Id = 3
        Me.txtSoLuongDauKy.Name = "txtSoLuongDauKy"
        Me.txtSoLuongDauKy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtSoLuongDauKy.Width = 89
        '
        'RepositoryItemSpinEdit3
        '
        Me.RepositoryItemSpinEdit3.AppearanceReadOnly.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSpinEdit3.AppearanceReadOnly.Options.UseFont = True
        Me.RepositoryItemSpinEdit3.AutoHeight = False
        Me.RepositoryItemSpinEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit3.DisplayFormat.FormatString = "N2"
        Me.RepositoryItemSpinEdit3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit3.EditFormat.FormatString = "N2"
        Me.RepositoryItemSpinEdit3.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit3.Mask.EditMask = "N2"
        Me.RepositoryItemSpinEdit3.Name = "RepositoryItemSpinEdit3"
        Me.RepositoryItemSpinEdit3.ReadOnly = True
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1188, 30)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 689)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1188, 33)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 30)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 659)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1188, 30)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 659)
        '
        'txtThanhTienTonDauKy
        '
        Me.txtThanhTienTonDauKy.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtThanhTienTonDauKy.Appearance.ForeColor = System.Drawing.Color.Teal
        Me.txtThanhTienTonDauKy.Appearance.Options.UseFont = True
        Me.txtThanhTienTonDauKy.Appearance.Options.UseForeColor = True
        Me.txtThanhTienTonDauKy.Caption = "Giá trị:"
        Me.txtThanhTienTonDauKy.Edit = Me.RepositoryItemSpinEdit2
        Me.txtThanhTienTonDauKy.EditValue = "0"
        Me.txtThanhTienTonDauKy.Id = 1
        Me.txtThanhTienTonDauKy.Name = "txtThanhTienTonDauKy"
        Me.txtThanhTienTonDauKy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtThanhTienTonDauKy.Width = 128
        '
        'RepositoryItemSpinEdit2
        '
        Me.RepositoryItemSpinEdit2.AutoHeight = False
        Me.RepositoryItemSpinEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit2.Name = "RepositoryItemSpinEdit2"
        '
        'txtThanhTienTonCuoiKy
        '
        Me.txtThanhTienTonCuoiKy.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtThanhTienTonCuoiKy.Appearance.Options.UseFont = True
        Me.txtThanhTienTonCuoiKy.Caption = "Giá trị"
        Me.txtThanhTienTonCuoiKy.Edit = Me.RepositoryItemSpinEdit4
        Me.txtThanhTienTonCuoiKy.EditValue = "0"
        Me.txtThanhTienTonCuoiKy.Id = 4
        Me.txtThanhTienTonCuoiKy.Name = "txtThanhTienTonCuoiKy"
        Me.txtThanhTienTonCuoiKy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtThanhTienTonCuoiKy.Width = 118
        '
        'RepositoryItemSpinEdit4
        '
        Me.RepositoryItemSpinEdit4.AutoHeight = False
        Me.RepositoryItemSpinEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit4.Name = "RepositoryItemSpinEdit4"
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "Giá trị"
        Me.BarEditItem1.Edit = Me.RepositoryItemSpinEdit5
        Me.BarEditItem1.Id = 5
        Me.BarEditItem1.Name = "BarEditItem1"
        Me.BarEditItem1.Width = 129
        '
        'RepositoryItemSpinEdit5
        '
        Me.RepositoryItemSpinEdit5.AutoHeight = False
        Me.RepositoryItemSpinEdit5.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit5.Name = "RepositoryItemSpinEdit5"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 0)
        Me.gdv.MainView = Me.gdvData
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoExEdit1, Me.RepositoryItemMemoEdit1})
        Me.gdv.Size = New System.Drawing.Size(1182, 628)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvData})
        '
        'gdvData
        '
        Me.gdvData.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.gdvData.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvData.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvData.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.gdvData.Appearance.HorzLine.Options.UseBackColor = True
        Me.gdvData.ColumnPanelRowHeight = 30
        Me.gdvData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn1, Me.GridColumn9, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8})
        Me.gdvData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvData.GridControl = Me.gdv
        Me.gdvData.Name = "gdvData"
        Me.gdvData.OptionsBehavior.Editable = False
        Me.gdvData.OptionsBehavior.ReadOnly = True
        Me.gdvData.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvData.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.gdvData.OptionsView.RowAutoHeight = True
        Me.gdvData.OptionsView.ShowGroupPanel = False
        Me.gdvData.OptionsView.ShowIndicator = False
        Me.gdvData.RowHeight = 30
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Navy
        Me.GridColumn2.AppearanceCell.Options.UseForeColor = True
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn2.Caption = "Ngày CT"
        Me.GridColumn2.FieldName = "NgayCT"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 95
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GridColumn1.AppearanceCell.Options.UseForeColor = True
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Số CT"
        Me.GridColumn1.FieldName = "SoCT"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 90
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn9.Caption = "Đơn giá"
        Me.GridColumn9.DisplayFormat.FormatString = "N0"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "DonGia"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.FixedWidth = True
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        Me.GridColumn9.Width = 85
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn3.Caption = "Đối tượng"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn3.FieldName = "DoiTuong"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 337
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn4.Caption = "Loại CT"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn4.FieldName = "LoaiCT2"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.FixedWidth = True
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 138
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.Caption = "Nhập"
        Me.GridColumn5.DisplayFormat.FormatString = "N2"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "Nhap"
        Me.GridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 5
        Me.GridColumn5.Width = 72
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn6.Caption = "Xuất"
        Me.GridColumn6.DisplayFormat.FormatString = "N2"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "Xuat"
        Me.GridColumn6.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 71
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.GridColumn7.AppearanceCell.Options.UseFont = True
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn7.Caption = "Tồn"
        Me.GridColumn7.DisplayFormat.FormatString = "N2"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "Ton"
        Me.GridColumn7.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 7
        Me.GridColumn7.Width = 89
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "GridColumn8"
        Me.GridColumn8.FieldName = "ID"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'RepositoryItemMemoExEdit1
        '
        Me.RepositoryItemMemoExEdit1.AutoHeight = False
        Me.RepositoryItemMemoExEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit1.Name = "RepositoryItemMemoExEdit1"
        '
        'txtSQL
        '
        Me.txtSQL.EditValue = resources.GetString("txtSQL.EditValue")
        Me.txtSQL.Location = New System.Drawing.Point(29, 172)
        Me.txtSQL.MenuManager = Me.BarManager1
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.Size = New System.Drawing.Size(218, 145)
        Me.txtSQL.TabIndex = 9
        Me.txtSQL.Visible = False
        '
        'tabMain
        '
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 30)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedTabPage = Me.tabHeThongThue
        Me.tabMain.Size = New System.Drawing.Size(1188, 659)
        Me.tabMain.TabIndex = 14
        Me.tabMain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabHeThongThue, Me.tabHeThongThuc})
        '
        'tabHeThongThue
        '
        Me.tabHeThongThue.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tabHeThongThue.Appearance.Header.ForeColor = System.Drawing.Color.Red
        Me.tabHeThongThue.Appearance.Header.Options.UseFont = True
        Me.tabHeThongThue.Appearance.Header.Options.UseForeColor = True
        Me.tabHeThongThue.Controls.Add(Me.txtSQL)
        Me.tabHeThongThue.Controls.Add(Me.gdv)
        Me.tabHeThongThue.Image = Global.BACSOFT.My.Resources.Resources.invoice_18
        Me.tabHeThongThue.Name = "tabHeThongThue"
        Me.tabHeThongThue.Size = New System.Drawing.Size(1182, 628)
        Me.tabHeThongThue.Text = "Hệ thống thuế"
        '
        'tabHeThongThuc
        '
        Me.tabHeThongThuc.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tabHeThongThuc.Appearance.Header.ForeColor = System.Drawing.Color.Green
        Me.tabHeThongThuc.Appearance.Header.Options.UseFont = True
        Me.tabHeThongThuc.Appearance.Header.Options.UseForeColor = True
        Me.tabHeThongThuc.Image = Global.BACSOFT.My.Resources.Resources.Attachment_18
        Me.tabHeThongThuc.Name = "tabHeThongThuc"
        Me.tabHeThongThuc.Size = New System.Drawing.Size(1182, 634)
        Me.tabHeThongThuc.Text = "Hệ thống thực"
        '
        'frmLichSuNhapXuatKhoThue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1188, 722)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmLichSuNhapXuatKhoThue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLichSuNhapXuatKhoThue"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSQL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.tabHeThongThue.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents txtSoLuongCuoiKy As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents txtThanhTienTonDauKy As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents txtSoLuongDauKy As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents txtThanhTienTonCuoiKy As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtSQL As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents tabMain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabHeThongThue As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tabHeThongThuc As DevExpress.XtraTab.XtraTabPage
End Class
