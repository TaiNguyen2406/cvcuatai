<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateGhiTangCCDC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateGhiTangCCDC))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.cmbPhongBan = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btnChungTuKhac = New DevExpress.XtraEditors.SimpleButton()
        Me.chkGhiSo = New DevExpress.XtraEditors.CheckEdit()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btnThemMoi = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNgayVaoSo = New DevExpress.XtraEditors.DateEdit()
        Me.btGhiLai = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDienGiaiChung = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.tabNoiDung = New DevExpress.XtraTab.XtraTabControl()
        Me.tabHangTien = New DevExpress.XtraTab.XtraTabPage()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcmbTaiKhoan = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.popupDsVT = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.cmbPhongBan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgayVaoSo.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgayVaoSo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDienGiaiChung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabNoiDung, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabNoiDung.SuspendLayout()
        Me.tabHangTien.SuspendLayout()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcmbTaiKhoan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popupDsVT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.cmbPhongBan)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.btnChungTuKhac)
        Me.GroupControl1.Controls.Add(Me.chkGhiSo)
        Me.GroupControl1.Controls.Add(Me.btnDong)
        Me.GroupControl1.Controls.Add(Me.btnThemMoi)
        Me.GroupControl1.Controls.Add(Me.txtNgayVaoSo)
        Me.GroupControl1.Controls.Add(Me.btGhiLai)
        Me.GroupControl1.Controls.Add(Me.LabelControl7)
        Me.GroupControl1.Controls.Add(Me.txtDienGiaiChung)
        Me.GroupControl1.Controls.Add(Me.LabelControl6)
        Me.GroupControl1.Location = New System.Drawing.Point(9, 8)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(795, 104)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Thông tin chung"
        '
        'cmbPhongBan
        '
        Me.cmbPhongBan.Location = New System.Drawing.Point(91, 68)
        Me.cmbPhongBan.Name = "cmbPhongBan"
        Me.cmbPhongBan.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cmbPhongBan.Properties.Appearance.Options.UseBackColor = True
        Me.cmbPhongBan.Properties.Appearance.Options.UseTextOptions = True
        Me.cmbPhongBan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cmbPhongBan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbPhongBan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbPhongBan.Size = New System.Drawing.Size(183, 20)
        Me.cmbPhongBan.TabIndex = 68
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(19, 72)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl2.TabIndex = 67
        Me.LabelControl2.Text = "Phòng ban"
        '
        'btnChungTuKhac
        '
        Me.btnChungTuKhac.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnChungTuKhac.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.btnChungTuKhac.Appearance.Options.UseFont = True
        Me.btnChungTuKhac.Appearance.Options.UseForeColor = True
        Me.btnChungTuKhac.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnChungTuKhac.Image = Global.BACSOFT.My.Resources.Resources.invoice_181
        Me.btnChungTuKhac.Location = New System.Drawing.Point(319, 33)
        Me.btnChungTuKhac.Name = "btnChungTuKhac"
        Me.btnChungTuKhac.Size = New System.Drawing.Size(229, 25)
        Me.btnChungTuKhac.TabIndex = 53
        Me.btnChungTuKhac.Text = "Lấy chứng từ hóa đơn mua CCDC"
        '
        'chkGhiSo
        '
        Me.chkGhiSo.EditValue = True
        Me.chkGhiSo.Location = New System.Drawing.Point(227, 35)
        Me.chkGhiSo.Name = "chkGhiSo"
        Me.chkGhiSo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkGhiSo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.chkGhiSo.Properties.Appearance.Options.UseFont = True
        Me.chkGhiSo.Properties.Appearance.Options.UseForeColor = True
        Me.chkGhiSo.Properties.Caption = "Ghi sổ"
        Me.chkGhiSo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.chkGhiSo.Properties.PictureChecked = CType(resources.GetObject("chkGhiSo.Properties.PictureChecked"), System.Drawing.Image)
        Me.chkGhiSo.Properties.PictureUnchecked = CType(resources.GetObject("chkGhiSo.Properties.PictureUnchecked"), System.Drawing.Image)
        Me.chkGhiSo.Size = New System.Drawing.Size(77, 21)
        Me.chkGhiSo.TabIndex = 55
        '
        'btnDong
        '
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btnDong.Location = New System.Drawing.Point(720, 28)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(63, 36)
        Me.btnDong.TabIndex = 54
        Me.btnDong.Text = "Đóng"
        '
        'btnThemMoi
        '
        Me.btnThemMoi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnThemMoi.Appearance.Options.UseFont = True
        Me.btnThemMoi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnThemMoi.Enabled = False
        Me.btnThemMoi.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThemMoi.Location = New System.Drawing.Point(580, 28)
        Me.btnThemMoi.Name = "btnThemMoi"
        Me.btnThemMoi.Size = New System.Drawing.Size(66, 36)
        Me.btnThemMoi.TabIndex = 53
        Me.btnThemMoi.Text = "Thêm"
        '
        'txtNgayVaoSo
        '
        Me.txtNgayVaoSo.EditValue = Nothing
        Me.txtNgayVaoSo.Location = New System.Drawing.Point(91, 35)
        Me.txtNgayVaoSo.Name = "txtNgayVaoSo"
        Me.txtNgayVaoSo.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNgayVaoSo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtNgayVaoSo.Properties.Appearance.Options.UseBackColor = True
        Me.txtNgayVaoSo.Properties.Appearance.Options.UseFont = True
        Me.txtNgayVaoSo.Properties.Appearance.Options.UseTextOptions = True
        Me.txtNgayVaoSo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtNgayVaoSo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgayVaoSo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.txtNgayVaoSo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgayVaoSo.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.txtNgayVaoSo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgayVaoSo.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.txtNgayVaoSo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgayVaoSo.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgayVaoSo.Size = New System.Drawing.Size(130, 20)
        Me.txtNgayVaoSo.TabIndex = 40
        '
        'btGhiLai
        '
        Me.btGhiLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btGhiLai.Appearance.Options.UseFont = True
        Me.btGhiLai.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btGhiLai.Image = CType(resources.GetObject("btGhiLai.Image"), System.Drawing.Image)
        Me.btGhiLai.Location = New System.Drawing.Point(651, 28)
        Me.btGhiLai.Name = "btGhiLai"
        Me.btGhiLai.Size = New System.Drawing.Size(63, 36)
        Me.btGhiLai.TabIndex = 52
        Me.btGhiLai.Text = "Ghi"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(19, 39)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl7.TabIndex = 41
        Me.LabelControl7.Text = "Ngày vào sổ"
        '
        'txtDienGiaiChung
        '
        Me.txtDienGiaiChung.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDienGiaiChung.Location = New System.Drawing.Point(374, 68)
        Me.txtDienGiaiChung.Name = "txtDienGiaiChung"
        Me.txtDienGiaiChung.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDienGiaiChung.Properties.Appearance.Options.UseBackColor = True
        Me.txtDienGiaiChung.Size = New System.Drawing.Size(408, 20)
        Me.txtDienGiaiChung.TabIndex = 11
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(288, 72)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(72, 13)
        Me.LabelControl6.TabIndex = 39
        Me.LabelControl6.Text = "Diễn giải chung"
        '
        'tabNoiDung
        '
        Me.tabNoiDung.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabNoiDung.Location = New System.Drawing.Point(12, 118)
        Me.tabNoiDung.Name = "tabNoiDung"
        Me.tabNoiDung.SelectedTabPage = Me.tabHangTien
        Me.tabNoiDung.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.tabNoiDung.Size = New System.Drawing.Size(792, 424)
        Me.tabNoiDung.TabIndex = 36
        Me.tabNoiDung.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tabHangTien})
        '
        'tabHangTien
        '
        Me.tabHangTien.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tabHangTien.Appearance.Header.ForeColor = System.Drawing.Color.Blue
        Me.tabHangTien.Appearance.Header.Options.UseFont = True
        Me.tabHangTien.Appearance.Header.Options.UseForeColor = True
        Me.tabHangTien.Controls.Add(Me.gdv)
        Me.tabHangTien.Name = "tabHangTien"
        Me.tabHangTien.Size = New System.Drawing.Size(786, 418)
        Me.tabHangTien.Text = "Chi tiết"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 0)
        Me.gdv.MainView = Me.gdvData
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.RepositoryItemMemoEdit1, Me.popupDsVT, Me.rcmbTaiKhoan})
        Me.gdv.Size = New System.Drawing.Size(786, 418)
        Me.gdv.TabIndex = 0
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
        Me.gdvData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn18, Me.GridColumn3, Me.GridColumn4, Me.GridColumn2, Me.GridColumn9, Me.GridColumn10, Me.GridColumn6, Me.GridColumn5, Me.GridColumn7, Me.GridColumn8, Me.GridColumn11, Me.GridColumn12})
        Me.gdvData.GridControl = Me.gdv
        Me.gdvData.Name = "gdvData"
        Me.gdvData.OptionsCustomization.AllowColumnMoving = False
        Me.gdvData.OptionsCustomization.AllowGroup = False
        Me.gdvData.OptionsCustomization.AllowSort = False
        Me.gdvData.OptionsView.ColumnAutoWidth = False
        Me.gdvData.OptionsView.RowAutoHeight = True
        Me.gdvData.OptionsView.ShowGroupPanel = False
        Me.gdvData.OptionsView.ShowIndicator = False
        Me.gdvData.RowHeight = 30
        Me.gdvData.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn18, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Id"
        Me.GridColumn1.FieldName = "Id"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.GridColumn18.AppearanceCell.Options.UseFont = True
        Me.GridColumn18.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn18.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn18.Caption = "Stt"
        Me.GridColumn18.FieldName = "STT"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.OptionsColumn.ReadOnly = True
        Me.GridColumn18.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        Me.GridColumn18.Width = 43
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "ID VT"
        Me.GridColumn3.FieldName = "IdVatTu"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Width = 76
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn4.Caption = "Nội dung"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn4.FieldName = "DienGiai"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        Me.GridColumn4.Width = 261
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Số kỳ"
        Me.GridColumn2.ColumnEdit = Me.RepositoryItemCalcEdit1
        Me.GridColumn2.DisplayFormat.FormatString = "N0"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn2.FieldName = "GiaTriKhac"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn9.Caption = "Nợ"
        Me.GridColumn9.ColumnEdit = Me.rcmbTaiKhoan
        Me.GridColumn9.FieldName = "TaiKhoanNo"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
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
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 4
        Me.GridColumn10.Width = 77
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "ĐVT"
        Me.GridColumn6.FieldName = "DVT"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.ReadOnly = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 80
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.Caption = "SL"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemCalcEdit1
        Me.GridColumn5.DisplayFormat.FormatString = "N2"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "SoLuong"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.ReadOnly = True
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 6
        Me.GridColumn5.Width = 81
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn7.Caption = "Đơn giá"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemCalcEdit1
        Me.GridColumn7.DisplayFormat.FormatString = "N2"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn7.FieldName = "DonGia"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 7
        Me.GridColumn7.Width = 123
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn8.Caption = "Thành tiền"
        Me.GridColumn8.ColumnEdit = Me.RepositoryItemCalcEdit1
        Me.GridColumn8.DisplayFormat.FormatString = "N2"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "ThanhTien"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.ReadOnly = True
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 8
        Me.GridColumn8.Width = 129
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn11.Caption = "Ghi chú"
        Me.GridColumn11.FieldName = "GhiChu"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Width = 500
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "IdChiTiet"
        Me.GridColumn12.FieldName = "IdChiTiet"
        Me.GridColumn12.Name = "GridColumn12"
        '
        'popupDsVT
        '
        Me.popupDsVT.AutoHeight = False
        Me.popupDsVT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.popupDsVT.Name = "popupDsVT"
        '
        'frmUpdateGhiTangCCDC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(816, 554)
        Me.Controls.Add(Me.tabNoiDung)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "frmUpdateGhiTangCCDC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmUpdateGhiTangTSCD"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.cmbPhongBan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgayVaoSo.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgayVaoSo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDienGiaiChung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabNoiDung, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabNoiDung.ResumeLayout(False)
        Me.tabHangTien.ResumeLayout(False)
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcmbTaiKhoan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popupDsVT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents txtDienGiaiChung As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgayVaoSo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnChungTuKhac As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tabNoiDung As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tabHangTien As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents popupDsVT As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcmbTaiKhoan As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnThemMoi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btGhiLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkGhiSo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmbPhongBan As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
