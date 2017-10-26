<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateKetChuyenLaiLoThue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateKetChuyenLaiLoThue))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.txtThang = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.txtNam = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.btnTaiDuLieu = New DevExpress.XtraBars.BarButtonItem()
        Me.txtSoLuong = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.chkGhiSo = New DevExpress.XtraEditors.CheckEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btGhi = New DevExpress.XtraEditors.SimpleButton()
        Me.bg = New System.ComponentModel.BackgroundWorker()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcmbTaiKhoan = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.popupDsVT = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcmbTaiKhoan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popupDsVT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.txtThang, Me.txtNam, Me.btnTaiDuLieu, Me.BarEditItem1, Me.txtSoLuong})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 5
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1, Me.RepositoryItemSpinEdit2, Me.RepositoryItemComboBox1, Me.RepositoryItemSpinEdit3})
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.txtThang), New DevExpress.XtraBars.LinkPersistInfo(Me.txtNam), New DevExpress.XtraBars.LinkPersistInfo(Me.btnTaiDuLieu), New DevExpress.XtraBars.LinkPersistInfo(Me.txtSoLuong)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'txtThang
        '
        Me.txtThang.Caption = "Tháng"
        Me.txtThang.Edit = Me.RepositoryItemSpinEdit1
        Me.txtThang.EditValue = "1"
        Me.txtThang.Id = 0
        Me.txtThang.Name = "txtThang"
        Me.txtThang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtThang.Width = 62
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSpinEdit1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.RepositoryItemSpinEdit1.Appearance.Options.UseFont = True
        Me.RepositoryItemSpinEdit1.Appearance.Options.UseForeColor = True
        Me.RepositoryItemSpinEdit1.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemSpinEdit1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit1.MaxValue = New Decimal(New Integer() {12, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        Me.RepositoryItemSpinEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'txtNam
        '
        Me.txtNam.Caption = "Năm"
        Me.txtNam.Edit = Me.RepositoryItemSpinEdit2
        Me.txtNam.EditValue = "2016"
        Me.txtNam.Id = 1
        Me.txtNam.Name = "txtNam"
        Me.txtNam.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtNam.Width = 84
        '
        'RepositoryItemSpinEdit2
        '
        Me.RepositoryItemSpinEdit2.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSpinEdit2.Appearance.ForeColor = System.Drawing.Color.Red
        Me.RepositoryItemSpinEdit2.Appearance.Options.UseFont = True
        Me.RepositoryItemSpinEdit2.Appearance.Options.UseForeColor = True
        Me.RepositoryItemSpinEdit2.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemSpinEdit2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemSpinEdit2.AutoHeight = False
        Me.RepositoryItemSpinEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit2.MaxValue = New Decimal(New Integer() {2020, 0, 0, 0})
        Me.RepositoryItemSpinEdit2.MinValue = New Decimal(New Integer() {2015, 0, 0, 0})
        Me.RepositoryItemSpinEdit2.Name = "RepositoryItemSpinEdit2"
        '
        'btnTaiDuLieu
        '
        Me.btnTaiDuLieu.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.btnTaiDuLieu.Appearance.Options.UseForeColor = True
        Me.btnTaiDuLieu.Caption = "Tính giá trị kết chuyển"
        Me.btnTaiDuLieu.Glyph = Global.BACSOFT.My.Resources.Resources.next_18
        Me.btnTaiDuLieu.Id = 2
        Me.btnTaiDuLieu.Name = "btnTaiDuLieu"
        Me.btnTaiDuLieu.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'txtSoLuong
        '
        Me.txtSoLuong.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.txtSoLuong.Caption = "Số luồng"
        Me.txtSoLuong.Edit = Me.RepositoryItemSpinEdit3
        Me.txtSoLuong.EditValue = "1"
        Me.txtSoLuong.Glyph = Global.BACSOFT.My.Resources.Resources.Collapse_16
        Me.txtSoLuong.Id = 4
        Me.txtSoLuong.Name = "txtSoLuong"
        Me.txtSoLuong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtSoLuong.Width = 65
        '
        'RepositoryItemSpinEdit3
        '
        Me.RepositoryItemSpinEdit3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.4!, System.Drawing.FontStyle.Bold)
        Me.RepositoryItemSpinEdit3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.RepositoryItemSpinEdit3.Appearance.Options.UseFont = True
        Me.RepositoryItemSpinEdit3.Appearance.Options.UseForeColor = True
        Me.RepositoryItemSpinEdit3.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemSpinEdit3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemSpinEdit3.AutoHeight = False
        Me.RepositoryItemSpinEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit3.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemSpinEdit3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit3.EditFormat.FormatString = "N0"
        Me.RepositoryItemSpinEdit3.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit3.MaxValue = New Decimal(New Integer() {64, 0, 0, 0})
        Me.RepositoryItemSpinEdit3.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.RepositoryItemSpinEdit3.Name = "RepositoryItemSpinEdit3"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlTop.Size = New System.Drawing.Size(744, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 471)
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlBottom.Size = New System.Drawing.Size(744, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 445)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(744, 26)
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 445)
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemComboBox1
        Me.BarEditItem1.Id = 3
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.chkGhiSo)
        Me.PanelControl1.Controls.Add(Me.btDong)
        Me.PanelControl1.Controls.Add(Me.btGhi)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 422)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(744, 49)
        Me.PanelControl1.TabIndex = 7
        '
        'chkGhiSo
        '
        Me.chkGhiSo.EditValue = True
        Me.chkGhiSo.Location = New System.Drawing.Point(10, 14)
        Me.chkGhiSo.Name = "chkGhiSo"
        Me.chkGhiSo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkGhiSo.Properties.Appearance.ForeColor = System.Drawing.Color.Green
        Me.chkGhiSo.Properties.Appearance.Options.UseFont = True
        Me.chkGhiSo.Properties.Appearance.Options.UseForeColor = True
        Me.chkGhiSo.Properties.Caption = "Check chứng từ chưa ghi sổ"
        Me.chkGhiSo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.chkGhiSo.Properties.PictureChecked = CType(resources.GetObject("chkGhiSo.Properties.PictureChecked"), System.Drawing.Image)
        Me.chkGhiSo.Properties.PictureUnchecked = CType(resources.GetObject("chkGhiSo.Properties.PictureUnchecked"), System.Drawing.Image)
        Me.chkGhiSo.Size = New System.Drawing.Size(244, 22)
        Me.chkGhiSo.TabIndex = 33
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btDong.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(667, 10)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(65, 30)
        Me.btDong.TabIndex = 7
        Me.btDong.Text = "Đóng"
        '
        'btGhi
        '
        Me.btGhi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btGhi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btGhi.Appearance.Options.UseFont = True
        Me.btGhi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btGhi.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btGhi.Location = New System.Drawing.Point(508, 10)
        Me.btGhi.Name = "btGhi"
        Me.btGhi.Size = New System.Drawing.Size(154, 30)
        Me.btGhi.TabIndex = 5
        Me.btGhi.Text = "Cập nhật chứng từ"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 26)
        Me.gdv.MainView = Me.gdvData
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.RepositoryItemMemoEdit1, Me.popupDsVT, Me.rcmbTaiKhoan})
        Me.gdv.Size = New System.Drawing.Size(744, 396)
        Me.gdv.TabIndex = 12
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvData})
        '
        'gdvData
        '
        Me.gdvData.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvData.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvData.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvData.Appearance.HorzLine.BackColor = System.Drawing.Color.White
        Me.gdvData.Appearance.HorzLine.Options.UseBackColor = True
        Me.gdvData.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.gdvData.Appearance.VertLine.Options.UseBackColor = True
        Me.gdvData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn9, Me.GridColumn10, Me.GridColumn8})
        Me.gdvData.GridControl = Me.gdv
        Me.gdvData.Name = "gdvData"
        Me.gdvData.OptionsCustomization.AllowColumnMoving = False
        Me.gdvData.OptionsCustomization.AllowFilter = False
        Me.gdvData.OptionsCustomization.AllowGroup = False
        Me.gdvData.OptionsCustomization.AllowSort = False
        Me.gdvData.OptionsMenu.EnableColumnMenu = False
        Me.gdvData.OptionsView.ColumnAutoWidth = False
        Me.gdvData.OptionsView.RowAutoHeight = True
        Me.gdvData.OptionsView.ShowFooter = True
        Me.gdvData.OptionsView.ShowGroupPanel = False
        Me.gdvData.OptionsView.ShowIndicator = False
        Me.gdvData.RowHeight = 30
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn4.Caption = "Diễn giải"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn4.FieldName = "DienGiai"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        Me.GridColumn4.Width = 427
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn9.Caption = "Nợ"
        Me.GridColumn9.ColumnEdit = Me.rcmbTaiKhoan
        Me.GridColumn9.FieldName = "TaiKhoanNo"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        '
        'rcmbTaiKhoan
        '
        Me.rcmbTaiKhoan.AutoHeight = False
        Me.rcmbTaiKhoan.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
        Me.rcmbTaiKhoan.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcmbTaiKhoan.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TaiKhoan", "Name9"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenGoi", "Name10")})
        Me.rcmbTaiKhoan.DisplayMember = "TaiKhoan"
        Me.rcmbTaiKhoan.DropDownItemHeight = 30
        Me.rcmbTaiKhoan.DropDownRows = 10
        Me.rcmbTaiKhoan.ImmediatePopup = True
        Me.rcmbTaiKhoan.Name = "rcmbTaiKhoan"
        Me.rcmbTaiKhoan.NullText = ""
        Me.rcmbTaiKhoan.PopupWidth = 350
        Me.rcmbTaiKhoan.ShowHeader = False
        Me.rcmbTaiKhoan.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcmbTaiKhoan.ValueMember = "TaiKhoan"
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.Caption = "Có"
        Me.GridColumn10.ColumnEdit = Me.rcmbTaiKhoan
        Me.GridColumn10.FieldName = "TaiKhoanCo"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 2
        Me.GridColumn10.Width = 77
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn8.Caption = "Thành tiền"
        Me.GridColumn8.ColumnEdit = Me.RepositoryItemCalcEdit1
        Me.GridColumn8.DisplayFormat.FormatString = "N0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "ThanhTien"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 129
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        '
        'popupDsVT
        '
        Me.popupDsVT.AutoHeight = False
        Me.popupDsVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.popupDsVT.Name = "popupDsVT"
        '
        'frmKetChuyenLaiLoThue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 471)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmKetChuyenLaiLoThue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kết chuyển lãi lỗ"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcmbTaiKhoan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popupDsVT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents txtThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents txtNam As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents btnTaiDuLieu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btGhi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkGhiSo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents bg As System.ComponentModel.BackgroundWorker
    Friend WithEvents txtSoLuong As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcmbTaiKhoan As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents popupDsVT As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
