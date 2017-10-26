<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLichSuMuonXe
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLichSuMuonXe))
        Me.gcXE = New DevExpress.XtraGrid.GridControl()
        Me.cmsHuhaixe = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ThôngTinHưHạiXeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ThêmToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SửaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XóaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvXe = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gclIDxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolID_nguoisudung = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgaydi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgaydidukien = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgayve = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgayvedukien = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNguoisudung = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolHanhtrinh = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riMeHanhTrinh = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.gcolDonghokm_truoc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolDonghokm_sau = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolSokmdachay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolID_mucdo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueMucDo = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.gcolID_mucdich = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenmucdich = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolGhichu_muon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.gcolGhichu_tra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.gcoltrangthai = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTrangThai = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.CardView1 = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarMenu = New DevExpress.XtraBars.Bar()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.barCbbXem = New DevExpress.XtraBars.BarEditItem()
        Me.riCbbXem = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.barDeTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.riDeTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDeDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.riDeDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barLueXe = New DevExpress.XtraBars.BarEditItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barGlueNSD = New DevExpress.XtraBars.BarEditItem()
        Me.riGlueNSD = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.barCiLoc = New DevExpress.XtraBars.BarCheckItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.loi = New DevExpress.XtraBars.BarEditItem()
        Me.rcbxXe = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarEditItem4 = New DevExpress.XtraBars.BarEditItem()
        Me.BarEditItem5 = New DevExpress.XtraBars.BarEditItem()
        Me.BarEditItem6 = New DevExpress.XtraBars.BarEditItem()
        Me.rcbxNguoisudung = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.gcXE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsHuhaixe.SuspendLayout()
        CType(Me.gvXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riMeHanhTrinh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueMucDo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTrangThai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbXem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riDeDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbxXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbxNguoisudung, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcXE
        '
        Me.gcXE.ContextMenuStrip = Me.cmsHuhaixe
        Me.gcXE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXE.Location = New System.Drawing.Point(0, 55)
        Me.gcXE.MainView = Me.gvXe
        Me.gcXE.Name = "gcXE"
        Me.gcXE.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riMeHanhTrinh, Me.RepositoryItemMemoEdit1, Me.RepositoryItemMemoEdit2, Me.riLueTrangThai, Me.riLueMucDo})
        Me.gcXE.Size = New System.Drawing.Size(1344, 316)
        Me.gcXE.TabIndex = 20
        Me.gcXE.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXe, Me.CardView1, Me.GridView1})
        '
        'cmsHuhaixe
        '
        Me.cmsHuhaixe.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ThôngTinHưHạiXeToolStripMenuItem, Me.ThêmToolStripMenuItem, Me.SửaToolStripMenuItem, Me.XóaToolStripMenuItem})
        Me.cmsHuhaixe.Name = "cmsHuhaixe"
        Me.cmsHuhaixe.Size = New System.Drawing.Size(177, 92)
        '
        'ThôngTinHưHạiXeToolStripMenuItem
        '
        Me.ThôngTinHưHạiXeToolStripMenuItem.Name = "ThôngTinHưHạiXeToolStripMenuItem"
        Me.ThôngTinHưHạiXeToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ThôngTinHưHạiXeToolStripMenuItem.Text = "Thông tin hư hại xe"
        '
        'ThêmToolStripMenuItem
        '
        Me.ThêmToolStripMenuItem.Image = CType(resources.GetObject("ThêmToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ThêmToolStripMenuItem.Name = "ThêmToolStripMenuItem"
        Me.ThêmToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ThêmToolStripMenuItem.Text = "Thêm"
        '
        'SửaToolStripMenuItem
        '
        Me.SửaToolStripMenuItem.Image = CType(resources.GetObject("SửaToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SửaToolStripMenuItem.Name = "SửaToolStripMenuItem"
        Me.SửaToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.SửaToolStripMenuItem.Text = "Sửa"
        '
        'XóaToolStripMenuItem
        '
        Me.XóaToolStripMenuItem.Image = CType(resources.GetObject("XóaToolStripMenuItem.Image"), System.Drawing.Image)
        Me.XóaToolStripMenuItem.Name = "XóaToolStripMenuItem"
        Me.XóaToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.XóaToolStripMenuItem.Text = "Xóa"
        '
        'gvXe
        '
        Me.gvXe.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvXe.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.gvXe.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXe.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvXe.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvXe.Appearance.FooterPanel.Options.UseFont = True
        Me.gvXe.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvXe.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvXe.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvXe.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvXe.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXe.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvXe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolID, Me.gclIDxe, Me.gcolTenxe, Me.gcolID_nguoisudung, Me.gcolNgaydi, Me.gcolNgaydidukien, Me.gcolNgayve, Me.gcolNgayvedukien, Me.gcolNguoisudung, Me.gcolHanhtrinh, Me.gcolDonghokm_truoc, Me.gcolDonghokm_sau, Me.gcolSokmdachay, Me.gcolID_mucdo, Me.gcolID_mucdich, Me.gcolTenmucdich, Me.gcolGhichu_muon, Me.gcolGhichu_tra, Me.gcoltrangthai})
        Me.gvXe.GridControl = Me.gcXE
        Me.gvXe.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.gvXe.Name = "gvXe"
        Me.gvXe.OptionsBehavior.Editable = False
        Me.gvXe.OptionsCustomization.AllowColumnMoving = False
        Me.gvXe.OptionsCustomization.AllowGroup = False
        Me.gvXe.OptionsCustomization.AllowRowSizing = True
        Me.gvXe.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvXe.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.gvXe.OptionsView.RowAutoHeight = True
        Me.gvXe.OptionsView.ShowFooter = True
        Me.gvXe.OptionsView.ShowGroupPanel = False
        Me.gvXe.OptionsView.ShowIndicator = False
        Me.gvXe.RowHeight = 23
        '
        'gcolID
        '
        Me.gcolID.Caption = "ID sử dụng xe"
        Me.gcolID.FieldName = "id"
        Me.gcolID.Name = "gcolID"
        Me.gcolID.Width = 80
        '
        'gclIDxe
        '
        Me.gclIDxe.Caption = "ID xe"
        Me.gclIDxe.FieldName = "id_xe"
        Me.gclIDxe.Name = "gclIDxe"
        '
        'gcolTenxe
        '
        Me.gcolTenxe.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gcolTenxe.AppearanceCell.Options.UseFont = True
        Me.gcolTenxe.AppearanceCell.Options.UseTextOptions = True
        Me.gcolTenxe.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolTenxe.Caption = "Tên xe"
        Me.gcolTenxe.FieldName = "tenxe"
        Me.gcolTenxe.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.gcolTenxe.Name = "gcolTenxe"
        Me.gcolTenxe.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolTenxe.Visible = True
        Me.gcolTenxe.VisibleIndex = 0
        Me.gcolTenxe.Width = 129
        '
        'gcolID_nguoisudung
        '
        Me.gcolID_nguoisudung.Caption = "ID người sử dụng"
        Me.gcolID_nguoisudung.FieldName = "id_nguoisudung"
        Me.gcolID_nguoisudung.Name = "gcolID_nguoisudung"
        Me.gcolID_nguoisudung.Width = 95
        '
        'gcolNgaydi
        '
        Me.gcolNgaydi.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgaydi.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgaydi.Caption = "Ngày đi"
        Me.gcolNgaydi.FieldName = "ngaydi"
        Me.gcolNgaydi.Name = "gcolNgaydi"
        Me.gcolNgaydi.Visible = True
        Me.gcolNgaydi.VisibleIndex = 3
        Me.gcolNgaydi.Width = 95
        '
        'gcolNgaydidukien
        '
        Me.gcolNgaydidukien.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgaydidukien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgaydidukien.Caption = "Ngày đi DK"
        Me.gcolNgaydidukien.FieldName = "ngaydidukien"
        Me.gcolNgaydidukien.Name = "gcolNgaydidukien"
        Me.gcolNgaydidukien.Visible = True
        Me.gcolNgaydidukien.VisibleIndex = 2
        Me.gcolNgaydidukien.Width = 95
        '
        'gcolNgayve
        '
        Me.gcolNgayve.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgayve.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgayve.Caption = "Ngày về"
        Me.gcolNgayve.FieldName = "ngayve"
        Me.gcolNgayve.Name = "gcolNgayve"
        Me.gcolNgayve.Visible = True
        Me.gcolNgayve.VisibleIndex = 5
        Me.gcolNgayve.Width = 95
        '
        'gcolNgayvedukien
        '
        Me.gcolNgayvedukien.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgayvedukien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgayvedukien.Caption = "Ngày về DK"
        Me.gcolNgayvedukien.FieldName = "ngayvedukien"
        Me.gcolNgayvedukien.Name = "gcolNgayvedukien"
        Me.gcolNgayvedukien.Visible = True
        Me.gcolNgayvedukien.VisibleIndex = 4
        Me.gcolNgayvedukien.Width = 95
        '
        'gcolNguoisudung
        '
        Me.gcolNguoisudung.Caption = "Người sử dụng"
        Me.gcolNguoisudung.FieldName = "ten"
        Me.gcolNguoisudung.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.gcolNguoisudung.Name = "gcolNguoisudung"
        Me.gcolNguoisudung.Visible = True
        Me.gcolNguoisudung.VisibleIndex = 1
        Me.gcolNguoisudung.Width = 113
        '
        'gcolHanhtrinh
        '
        Me.gcolHanhtrinh.Caption = "Hành trình"
        Me.gcolHanhtrinh.ColumnEdit = Me.riMeHanhTrinh
        Me.gcolHanhtrinh.FieldName = "hanhtrinh"
        Me.gcolHanhtrinh.Name = "gcolHanhtrinh"
        Me.gcolHanhtrinh.Visible = True
        Me.gcolHanhtrinh.VisibleIndex = 6
        Me.gcolHanhtrinh.Width = 122
        '
        'riMeHanhTrinh
        '
        Me.riMeHanhTrinh.Name = "riMeHanhTrinh"
        '
        'gcolDonghokm_truoc
        '
        Me.gcolDonghokm_truoc.Caption = "ĐH Km trước"
        Me.gcolDonghokm_truoc.FieldName = "donghokm_truoc"
        Me.gcolDonghokm_truoc.Name = "gcolDonghokm_truoc"
        Me.gcolDonghokm_truoc.Visible = True
        Me.gcolDonghokm_truoc.VisibleIndex = 7
        Me.gcolDonghokm_truoc.Width = 85
        '
        'gcolDonghokm_sau
        '
        Me.gcolDonghokm_sau.Caption = "ĐH Km sau"
        Me.gcolDonghokm_sau.FieldName = "donghokm_sau"
        Me.gcolDonghokm_sau.Name = "gcolDonghokm_sau"
        Me.gcolDonghokm_sau.Visible = True
        Me.gcolDonghokm_sau.VisibleIndex = 8
        Me.gcolDonghokm_sau.Width = 81
        '
        'gcolSokmdachay
        '
        Me.gcolSokmdachay.Caption = "Đã chạy(km)"
        Me.gcolSokmdachay.FieldName = "sokmdachay"
        Me.gcolSokmdachay.Name = "gcolSokmdachay"
        Me.gcolSokmdachay.Visible = True
        Me.gcolSokmdachay.VisibleIndex = 9
        Me.gcolSokmdachay.Width = 81
        '
        'gcolID_mucdo
        '
        Me.gcolID_mucdo.Caption = "Mức độ"
        Me.gcolID_mucdo.ColumnEdit = Me.riLueMucDo
        Me.gcolID_mucdo.FieldName = "id_mucdo"
        Me.gcolID_mucdo.Name = "gcolID_mucdo"
        Me.gcolID_mucdo.Visible = True
        Me.gcolID_mucdo.VisibleIndex = 11
        '
        'riLueMucDo
        '
        Me.riLueMucDo.AutoHeight = False
        Me.riLueMucDo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueMucDo.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenmucdo", "Tên")})
        Me.riLueMucDo.DisplayMember = "tenmucdo"
        Me.riLueMucDo.Name = "riLueMucDo"
        Me.riLueMucDo.NullText = ""
        Me.riLueMucDo.ShowHeader = False
        Me.riLueMucDo.ValueMember = "id"
        '
        'gcolID_mucdich
        '
        Me.gcolID_mucdich.Caption = "ID mục đích"
        Me.gcolID_mucdich.FieldName = "id_mucdich"
        Me.gcolID_mucdich.Name = "gcolID_mucdich"
        '
        'gcolTenmucdich
        '
        Me.gcolTenmucdich.Caption = "Mục đích "
        Me.gcolTenmucdich.FieldName = "tenmucdich"
        Me.gcolTenmucdich.Name = "gcolTenmucdich"
        Me.gcolTenmucdich.Visible = True
        Me.gcolTenmucdich.VisibleIndex = 10
        Me.gcolTenmucdich.Width = 67
        '
        'gcolGhichu_muon
        '
        Me.gcolGhichu_muon.Caption = "Gc mượn"
        Me.gcolGhichu_muon.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.gcolGhichu_muon.FieldName = "ghichu_muon"
        Me.gcolGhichu_muon.Name = "gcolGhichu_muon"
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'gcolGhichu_tra
        '
        Me.gcolGhichu_tra.Caption = "Gc trả"
        Me.gcolGhichu_tra.ColumnEdit = Me.RepositoryItemMemoEdit2
        Me.gcolGhichu_tra.FieldName = "ghichu_tra"
        Me.gcolGhichu_tra.Name = "gcolGhichu_tra"
        Me.gcolGhichu_tra.Width = 81
        '
        'RepositoryItemMemoEdit2
        '
        Me.RepositoryItemMemoEdit2.Name = "RepositoryItemMemoEdit2"
        '
        'gcoltrangthai
        '
        Me.gcoltrangthai.AppearanceCell.Options.UseTextOptions = True
        Me.gcoltrangthai.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcoltrangthai.Caption = "Trạng thái xe"
        Me.gcoltrangthai.ColumnEdit = Me.riLueTrangThai
        Me.gcoltrangthai.FieldName = "id_trangthaixe"
        Me.gcoltrangthai.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.gcoltrangthai.Name = "gcoltrangthai"
        Me.gcoltrangthai.Visible = True
        Me.gcoltrangthai.VisibleIndex = 12
        Me.gcoltrangthai.Width = 100
        '
        'riLueTrangThai
        '
        Me.riLueTrangThai.AutoHeight = False
        Me.riLueTrangThai.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTrangThai.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentrangthaixe", "Tên")})
        Me.riLueTrangThai.DisplayMember = "tentrangthaixe"
        Me.riLueTrangThai.Name = "riLueTrangThai"
        Me.riLueTrangThai.NullText = ""
        Me.riLueTrangThai.ShowHeader = False
        Me.riLueTrangThai.ValueMember = "id"
        '
        'CardView1
        '
        Me.CardView1.FocusedCardTopFieldIndex = 0
        Me.CardView1.GridControl = Me.gcXE
        Me.CardView1.Name = "CardView1"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.gcXE
        Me.GridView1.Name = "GridView1"
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.BarMenu})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.loi, Me.BarStaticItem1, Me.BarButtonItem1, Me.BarButtonItem2, Me.btnXoa, Me.BarButtonItem4, Me.barLueXe, Me.BarButtonItem5, Me.BarButtonItem6, Me.barGlueNSD, Me.barCiLoc, Me.barCbbXem, Me.barDeTuNgay, Me.barDeDenNgay})
        Me.BarManager1.MainMenu = Me.BarMenu
        Me.BarManager1.MaxItemId = 37
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.rcbxXe, Me.riGlueNSD, Me.riCbbXem, Me.RepositoryItemTextEdit1, Me.RepositoryItemTextEdit2, Me.riDeTuNgay, Me.riDeDenNgay})
        '
        'BarMenu
        '
        Me.BarMenu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarMenu.Appearance.Options.UseFont = True
        Me.BarMenu.BarName = "Main menu"
        Me.BarMenu.DockCol = 0
        Me.BarMenu.DockRow = 0
        Me.BarMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.BarMenu.FloatLocation = New System.Drawing.Point(126, 195)
        Me.BarMenu.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCbbXem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueXe, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barGlueNSD, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem4, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem5, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCiLoc, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem6, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.BarMenu.OptionsBar.AllowQuickCustomization = False
        Me.BarMenu.OptionsBar.DrawDragBorder = False
        Me.BarMenu.OptionsBar.MultiLine = True
        Me.BarMenu.OptionsBar.UseWholeRow = True
        Me.BarMenu.Text = "Main menu"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Id = 8
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'barCbbXem
        '
        Me.barCbbXem.Caption = "Xem"
        Me.barCbbXem.Edit = Me.riCbbXem
        Me.barCbbXem.Id = 32
        Me.barCbbXem.Name = "barCbbXem"
        Me.barCbbXem.Width = 70
        '
        'riCbbXem
        '
        Me.riCbbXem.AutoHeight = False
        Me.riCbbXem.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riCbbXem.Items.AddRange(New Object() {"Top 500", "Tất cả", "Tuỳ chỉnh"})
        Me.riCbbXem.Name = "riCbbXem"
        '
        'barDeTuNgay
        '
        Me.barDeTuNgay.Caption = "Từ"
        Me.barDeTuNgay.Edit = Me.riDeTuNgay
        Me.barDeTuNgay.Id = 35
        Me.barDeTuNgay.Name = "barDeTuNgay"
        Me.barDeTuNgay.Width = 83
        '
        'riDeTuNgay
        '
        Me.riDeTuNgay.AutoHeight = False
        Me.riDeTuNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riDeTuNgay.EditFormat.FormatString = "yyyy/MM/dd"
        Me.riDeTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.riDeTuNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.riDeTuNgay.Mask.UseMaskAsDisplayFormat = True
        Me.riDeTuNgay.Name = "riDeTuNgay"
        Me.riDeTuNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barDeDenNgay
        '
        Me.barDeDenNgay.Caption = "Đến"
        Me.barDeDenNgay.Edit = Me.riDeDenNgay
        Me.barDeDenNgay.Id = 36
        Me.barDeDenNgay.Name = "barDeDenNgay"
        Me.barDeDenNgay.Width = 83
        '
        'riDeDenNgay
        '
        Me.riDeDenNgay.AutoHeight = False
        Me.riDeDenNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riDeDenNgay.EditFormat.FormatString = "yyyy/MM/dd"
        Me.riDeDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.riDeDenNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.riDeDenNgay.Mask.UseMaskAsDisplayFormat = True
        Me.riDeDenNgay.Name = "riDeDenNgay"
        Me.riDeDenNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barLueXe
        '
        Me.barLueXe.Caption = "Xe"
        Me.barLueXe.Edit = Me.lueXe
        Me.barLueXe.Id = 22
        Me.barLueXe.Name = "barLueXe"
        Me.barLueXe.Width = 203
        '
        'lueXe
        '
        Me.lueXe.AutoHeight = False
        Me.lueXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.lueXe.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Xe"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "maxe", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueXe.DisplayMember = "tenxe"
        Me.lueXe.Name = "lueXe"
        Me.lueXe.NullText = "[Tất cả]"
        Me.lueXe.ValueMember = "id"
        '
        'barGlueNSD
        '
        Me.barGlueNSD.Caption = "NSD"
        Me.barGlueNSD.Edit = Me.riGlueNSD
        Me.barGlueNSD.Id = 30
        Me.barGlueNSD.Name = "barGlueNSD"
        Me.barGlueNSD.Width = 185
        '
        'riGlueNSD
        '
        Me.riGlueNSD.AutoHeight = False
        Me.riGlueNSD.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riGlueNSD.DisplayMember = "Ten"
        Me.riGlueNSD.Name = "riGlueNSD"
        Me.riGlueNSD.NullText = "Tất cả"
        Me.riGlueNSD.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riGlueNSD.ValueMember = "ID"
        Me.riGlueNSD.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem1.Appearance.Options.UseFont = True
        Me.BarButtonItem1.Caption = "Thêm"
        Me.BarButtonItem1.Glyph = CType(resources.GetObject("BarButtonItem1.Glyph"), System.Drawing.Image)
        Me.BarButtonItem1.Id = 15
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Sửa"
        Me.BarButtonItem2.Glyph = CType(resources.GetObject("BarButtonItem2.Glyph"), System.Drawing.Image)
        Me.BarButtonItem2.Id = 16
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'btnXoa
        '
        Me.btnXoa.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnXoa.Appearance.Options.UseFont = True
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = CType(resources.GetObject("btnXoa.Glyph"), System.Drawing.Image)
        Me.btnXoa.Id = 17
        Me.btnXoa.Name = "btnXoa"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem4.Appearance.Options.UseFont = True
        Me.BarButtonItem4.Caption = "Xóa đăng ký bị hủy"
        Me.BarButtonItem4.Glyph = CType(resources.GetObject("BarButtonItem4.Glyph"), System.Drawing.Image)
        Me.BarButtonItem4.Id = 18
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem5.Appearance.Options.UseFont = True
        Me.BarButtonItem5.Caption = "Tải lại"
        Me.BarButtonItem5.Glyph = CType(resources.GetObject("BarButtonItem5.Glyph"), System.Drawing.Image)
        Me.BarButtonItem5.Id = 27
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'barCiLoc
        '
        Me.barCiLoc.Caption = "Lọc"
        Me.barCiLoc.Glyph = Global.BACSOFT.My.Resources.Resources.filter_18
        Me.barCiLoc.Id = 31
        Me.barCiLoc.Name = "barCiLoc"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem6.Appearance.Options.UseFont = True
        Me.BarButtonItem6.Caption = "Nhập cũ"
        Me.BarButtonItem6.Id = 29
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1344, 55)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 371)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1344, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 55)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 316)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1344, 55)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 316)
        '
        'loi
        '
        Me.loi.Caption = "Chọn xe"
        Me.loi.Edit = Me.lueXe
        Me.loi.EditValue = ""
        Me.loi.Enabled = False
        Me.loi.Id = 4
        Me.loi.Name = "loi"
        Me.loi.Width = 148
        '
        'rcbxXe
        '
        Me.rcbxXe.AutoHeight = False
        Me.rcbxXe.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rcbxXe.Name = "rcbxXe"
        Me.rcbxXe.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.AutoHeight = False
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        '
        'BarEditItem4
        '
        Me.BarEditItem4.Caption = "BarEditItem3"
        Me.BarEditItem4.Edit = Me.rcbxXe
        Me.BarEditItem4.Id = 10
        Me.BarEditItem4.Name = "BarEditItem4"
        Me.BarEditItem4.Width = 20
        '
        'BarEditItem5
        '
        Me.BarEditItem5.Caption = "BarEditItem3"
        Me.BarEditItem5.Edit = Me.rcbxXe
        Me.BarEditItem5.Id = 10
        Me.BarEditItem5.Name = "BarEditItem5"
        Me.BarEditItem5.Width = 20
        '
        'BarEditItem6
        '
        Me.BarEditItem6.Caption = "BarEditItem3"
        Me.BarEditItem6.Edit = Me.rcbxXe
        Me.BarEditItem6.Id = 10
        Me.BarEditItem6.Name = "BarEditItem6"
        Me.BarEditItem6.Width = 20
        '
        'rcbxNguoisudung
        '
        Me.rcbxNguoisudung.AutoHeight = False
        Me.rcbxNguoisudung.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rcbxNguoisudung.Name = "rcbxNguoisudung"
        Me.rcbxNguoisudung.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'frmLichSuMuonXe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1344, 371)
        Me.Controls.Add(Me.gcXE)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmLichSuMuonXe"
        Me.Text = "Lịch sử mượn xe"
        CType(Me.gcXE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsHuhaixe.ResumeLayout(False)
        CType(Me.gvXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riMeHanhTrinh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueMucDo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTrangThai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbXem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riDeDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbxXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbxNguoisudung, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcXE As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvXe As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcolID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gclIDxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolID_nguoisudung As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNgaydi As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNgaydidukien As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNgayve As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNgayvedukien As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNguoisudung As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolHanhtrinh As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolDonghokm_truoc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolDonghokm_sau As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolSokmdachay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolID_mucdo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolID_mucdich As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenmucdich As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolGhichu_muon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolGhichu_tra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcoltrangthai As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmsHuhaixe As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ThôngTinHưHạiXeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ThêmToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SửaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents XóaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarMenu As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents loi As DevExpress.XtraBars.BarEditItem
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents rcbxXe As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BarEditItem4 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarEditItem5 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarEditItem6 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbxNguoisudung As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents CardView1 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents barLueXe As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents riMeHanhTrinh As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemMemoEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barGlueNSD As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riGlueNSD As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents riLueTrangThai As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents riLueMucDo As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barCiLoc As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents barCbbXem As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riCbbXem As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents barDeTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riDeTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barDeDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riDeDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
