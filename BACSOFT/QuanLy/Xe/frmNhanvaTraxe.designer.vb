<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNhanvaTraxe
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNhanvaTraxe))
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gcXeban = New DevExpress.XtraGrid.GridControl()
        Me.cmsNhanxe = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NhậnXeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TrảXeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HủyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvXeban = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolIDxeban = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxeban = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolBiensoxeban = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolHanhtrinh = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNguoisudung = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolNgaydidukien = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolIDsudungxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTrangthaixe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTrangThai = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.gcolIDTrangthaixe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemGridLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcXeroi = New DevExpress.XtraGrid.GridControl()
        Me.cmsDangKyXe = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ĐăngKýMượnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gvXeroi = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolIDxeroi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxeroi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gColbiensoxeroi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTinhtrang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTinhTrang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.gcolGhichu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rilueXe = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.repositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.AlertControl1 = New DevExpress.XtraBars.Alerter.AlertControl(Me.components)
        Me.tableLayoutPanel1.SuspendLayout()
        CType(Me.gcXeban, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsNhanxe.SuspendLayout()
        CType(Me.gvXeban, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTrangThai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcXeroi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsDangKyXe.SuspendLayout()
        CType(Me.gvXeroi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rilueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.ColumnCount = 2
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.85973!))
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.14027!))
        Me.tableLayoutPanel1.Controls.Add(Me.gcXeban, 1, 0)
        Me.tableLayoutPanel1.Controls.Add(Me.gcXeroi, 0, 0)
        Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 26)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.RowCount = 1
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 414.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(1179, 414)
        Me.tableLayoutPanel1.TabIndex = 11
        '
        'gcXeban
        '
        Me.gcXeban.ContextMenuStrip = Me.cmsNhanxe
        Me.gcXeban.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXeban.Location = New System.Drawing.Point(425, 3)
        Me.gcXeban.MainView = Me.gvXeban
        Me.gcXeban.Name = "gcXeban"
        Me.gcXeban.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemGridLookUpEdit1, Me.riLueTrangThai})
        Me.gcXeban.Size = New System.Drawing.Size(751, 408)
        Me.gcXeban.TabIndex = 11
        Me.gcXeban.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXeban})
        '
        'cmsNhanxe
        '
        Me.cmsNhanxe.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NhậnXeToolStripMenuItem, Me.TrảXeToolStripMenuItem, Me.HủyToolStripMenuItem})
        Me.cmsNhanxe.Name = "cmsNhanxe"
        Me.cmsNhanxe.Size = New System.Drawing.Size(142, 70)
        '
        'NhậnXeToolStripMenuItem
        '
        Me.NhậnXeToolStripMenuItem.Image = CType(resources.GetObject("NhậnXeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NhậnXeToolStripMenuItem.Name = "NhậnXeToolStripMenuItem"
        Me.NhậnXeToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.NhậnXeToolStripMenuItem.Text = "Nhận xe"
        '
        'TrảXeToolStripMenuItem
        '
        Me.TrảXeToolStripMenuItem.Image = CType(resources.GetObject("TrảXeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TrảXeToolStripMenuItem.Name = "TrảXeToolStripMenuItem"
        Me.TrảXeToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.TrảXeToolStripMenuItem.Text = "Trả xe"
        '
        'HủyToolStripMenuItem
        '
        Me.HủyToolStripMenuItem.Image = CType(resources.GetObject("HủyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.HủyToolStripMenuItem.Name = "HủyToolStripMenuItem"
        Me.HủyToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.HủyToolStripMenuItem.Text = "Hủy đăng ký"
        '
        'gvXeban
        '
        Me.gvXeban.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvXeban.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.gvXeban.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXeban.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvXeban.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvXeban.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvXeban.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvXeban.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvXeban.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXeban.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvXeban.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolIDxeban, Me.gcolTenxeban, Me.gcolBiensoxeban, Me.gcolHanhtrinh, Me.gcolNguoisudung, Me.gcolNgaydidukien, Me.gcolIDsudungxe, Me.gcolTrangthaixe, Me.gcolIDTrangthaixe, Me.GridColumn1})
        Me.gvXeban.GridControl = Me.gcXeban
        Me.gvXeban.Name = "gvXeban"
        Me.gvXeban.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXeban.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXeban.OptionsBehavior.Editable = False
        Me.gvXeban.OptionsCustomization.AllowColumnMoving = False
        Me.gvXeban.OptionsCustomization.AllowGroup = False
        Me.gvXeban.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvXeban.OptionsView.EnableAppearanceEvenRow = True
        Me.gvXeban.OptionsView.ShowGroupPanel = False
        Me.gvXeban.OptionsView.ShowIndicator = False
        Me.gvXeban.RowHeight = 23
        '
        'gcolIDxeban
        '
        Me.gcolIDxeban.Caption = "IDxe"
        Me.gcolIDxeban.FieldName = "id"
        Me.gcolIDxeban.Name = "gcolIDxeban"
        '
        'gcolTenxeban
        '
        Me.gcolTenxeban.Caption = "Tên xe"
        Me.gcolTenxeban.FieldName = "tenxe"
        Me.gcolTenxeban.Name = "gcolTenxeban"
        Me.gcolTenxeban.Visible = True
        Me.gcolTenxeban.VisibleIndex = 0
        Me.gcolTenxeban.Width = 101
        '
        'gcolBiensoxeban
        '
        Me.gcolBiensoxeban.Caption = "Biển số "
        Me.gcolBiensoxeban.FieldName = "bienso"
        Me.gcolBiensoxeban.Name = "gcolBiensoxeban"
        Me.gcolBiensoxeban.Visible = True
        Me.gcolBiensoxeban.VisibleIndex = 1
        Me.gcolBiensoxeban.Width = 101
        '
        'gcolHanhtrinh
        '
        Me.gcolHanhtrinh.Caption = "Hành trình"
        Me.gcolHanhtrinh.FieldName = "hanhtrinh"
        Me.gcolHanhtrinh.Name = "gcolHanhtrinh"
        Me.gcolHanhtrinh.Visible = True
        Me.gcolHanhtrinh.VisibleIndex = 2
        Me.gcolHanhtrinh.Width = 101
        '
        'gcolNguoisudung
        '
        Me.gcolNguoisudung.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNguoisudung.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNguoisudung.Caption = "Người sử dụng"
        Me.gcolNguoisudung.FieldName = "ten"
        Me.gcolNguoisudung.Name = "gcolNguoisudung"
        Me.gcolNguoisudung.Visible = True
        Me.gcolNguoisudung.VisibleIndex = 3
        Me.gcolNguoisudung.Width = 101
        '
        'gcolNgaydidukien
        '
        Me.gcolNgaydidukien.AppearanceCell.Options.UseTextOptions = True
        Me.gcolNgaydidukien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolNgaydidukien.Caption = "Dự kiến"
        Me.gcolNgaydidukien.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.gcolNgaydidukien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.gcolNgaydidukien.FieldName = "ngaydidukien"
        Me.gcolNgaydidukien.Name = "gcolNgaydidukien"
        Me.gcolNgaydidukien.Visible = True
        Me.gcolNgaydidukien.VisibleIndex = 4
        Me.gcolNgaydidukien.Width = 125
        '
        'gcolIDsudungxe
        '
        Me.gcolIDsudungxe.Caption = "Id sử dụng xe"
        Me.gcolIDsudungxe.FieldName = "id_sudungxe"
        Me.gcolIDsudungxe.Name = "gcolIDsudungxe"
        '
        'gcolTrangthaixe
        '
        Me.gcolTrangthaixe.Caption = "Trạng thái"
        Me.gcolTrangthaixe.ColumnEdit = Me.riLueTrangThai
        Me.gcolTrangthaixe.FieldName = "id_trangthaixe"
        Me.gcolTrangthaixe.Name = "gcolTrangthaixe"
        Me.gcolTrangthaixe.Visible = True
        Me.gcolTrangthaixe.VisibleIndex = 6
        Me.gcolTrangthaixe.Width = 64
        '
        'riLueTrangThai
        '
        Me.riLueTrangThai.AutoHeight = False
        Me.riLueTrangThai.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTrangThai.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentrangthai", "Ten")})
        Me.riLueTrangThai.DisplayMember = "tentrangthaixe"
        Me.riLueTrangThai.Name = "riLueTrangThai"
        Me.riLueTrangThai.NullText = ""
        Me.riLueTrangThai.ShowHeader = False
        Me.riLueTrangThai.ValueMember = "id"
        '
        'gcolIDTrangthaixe
        '
        Me.gcolIDTrangthaixe.Caption = "ID trạng thái xe"
        Me.gcolIDTrangthaixe.FieldName = "id_trangthaixe"
        Me.gcolIDTrangthaixe.Name = "gcolIDTrangthaixe"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Ngày đi"
        Me.GridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "ngaydi"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 5
        Me.GridColumn1.Width = 115
        '
        'RepositoryItemGridLookUpEdit1
        '
        Me.RepositoryItemGridLookUpEdit1.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit1.DisplayMember = "tentrangthai"
        Me.RepositoryItemGridLookUpEdit1.Name = "RepositoryItemGridLookUpEdit1"
        Me.RepositoryItemGridLookUpEdit1.ValueMember = "id"
        Me.RepositoryItemGridLookUpEdit1.View = Me.GridView3
        '
        'GridView3
        '
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'gcXeroi
        '
        Me.gcXeroi.ContextMenuStrip = Me.cmsDangKyXe
        Me.gcXeroi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcXeroi.Location = New System.Drawing.Point(3, 3)
        Me.gcXeroi.MainView = Me.gvXeroi
        Me.gcXeroi.Name = "gcXeroi"
        Me.gcXeroi.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rilueXe, Me.riLueTinhTrang})
        Me.gcXeroi.Size = New System.Drawing.Size(416, 408)
        Me.gcXeroi.TabIndex = 10
        Me.gcXeroi.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvXeroi})
        '
        'cmsDangKyXe
        '
        Me.cmsDangKyXe.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ĐăngKýMượnToolStripMenuItem})
        Me.cmsDangKyXe.Name = "cmsDangKyXe"
        Me.cmsDangKyXe.Size = New System.Drawing.Size(153, 26)
        '
        'ĐăngKýMượnToolStripMenuItem
        '
        Me.ĐăngKýMượnToolStripMenuItem.Image = CType(resources.GetObject("ĐăngKýMượnToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ĐăngKýMượnToolStripMenuItem.Name = "ĐăngKýMượnToolStripMenuItem"
        Me.ĐăngKýMượnToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ĐăngKýMượnToolStripMenuItem.Text = "Đăng ký mượn"
        '
        'gvXeroi
        '
        Me.gvXeroi.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvXeroi.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.gvXeroi.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXeroi.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvXeroi.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvXeroi.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvXeroi.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvXeroi.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvXeroi.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvXeroi.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvXeroi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolIDxeroi, Me.gcolTenxeroi, Me.gColbiensoxeroi, Me.gcolTinhtrang, Me.gcolGhichu})
        Me.gvXeroi.GridControl = Me.gcXeroi
        Me.gvXeroi.Name = "gvXeroi"
        Me.gvXeroi.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXeroi.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvXeroi.OptionsBehavior.Editable = False
        Me.gvXeroi.OptionsCustomization.AllowColumnMoving = False
        Me.gvXeroi.OptionsCustomization.AllowGroup = False
        Me.gvXeroi.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvXeroi.OptionsView.EnableAppearanceOddRow = True
        Me.gvXeroi.OptionsView.ShowGroupPanel = False
        Me.gvXeroi.OptionsView.ShowIndicator = False
        Me.gvXeroi.RowHeight = 23
        '
        'gcolIDxeroi
        '
        Me.gcolIDxeroi.Caption = "IDxe"
        Me.gcolIDxeroi.FieldName = "id"
        Me.gcolIDxeroi.Name = "gcolIDxeroi"
        '
        'gcolTenxeroi
        '
        Me.gcolTenxeroi.Caption = "Tên xe"
        Me.gcolTenxeroi.FieldName = "tenxe"
        Me.gcolTenxeroi.Name = "gcolTenxeroi"
        Me.gcolTenxeroi.Visible = True
        Me.gcolTenxeroi.VisibleIndex = 0
        '
        'gColbiensoxeroi
        '
        Me.gColbiensoxeroi.Caption = "Biển số "
        Me.gColbiensoxeroi.FieldName = "bienso"
        Me.gColbiensoxeroi.Name = "gColbiensoxeroi"
        Me.gColbiensoxeroi.Visible = True
        Me.gColbiensoxeroi.VisibleIndex = 1
        '
        'gcolTinhtrang
        '
        Me.gcolTinhtrang.Caption = "Tình trạng"
        Me.gcolTinhtrang.ColumnEdit = Me.riLueTinhTrang
        Me.gcolTinhtrang.FieldName = "id_tinhtrang"
        Me.gcolTinhtrang.Name = "gcolTinhtrang"
        Me.gcolTinhtrang.Visible = True
        Me.gcolTinhtrang.VisibleIndex = 2
        '
        'riLueTinhTrang
        '
        Me.riLueTinhTrang.AutoHeight = False
        Me.riLueTinhTrang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTinhTrang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentinhtrang", "Tên ")})
        Me.riLueTinhTrang.DisplayMember = "tentinhtrang"
        Me.riLueTinhTrang.Name = "riLueTinhTrang"
        Me.riLueTinhTrang.NullText = ""
        Me.riLueTinhTrang.ShowHeader = False
        Me.riLueTinhTrang.ValueMember = "id"
        '
        'gcolGhichu
        '
        Me.gcolGhichu.Caption = "Ghi chú"
        Me.gcolGhichu.FieldName = "ghichu"
        Me.gcolGhichu.Name = "gcolGhichu"
        '
        'rilueXe
        '
        Me.rilueXe.AutoHeight = False
        Me.rilueXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rilueXe.DisplayMember = "tentrangthai"
        Me.rilueXe.Name = "rilueXe"
        Me.rilueXe.ValueMember = "id"
        Me.rilueXe.View = Me.repositoryItemGridLookUpEdit1View
        '
        'repositoryItemGridLookUpEdit1View
        '
        Me.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View"
        Me.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 3
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Đăng ký mượn xe"
        Me.BarButtonItem2.Glyph = CType(resources.GetObject("BarButtonItem2.Glyph"), System.Drawing.Image)
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem3.Appearance.Options.UseFont = True
        Me.BarButtonItem3.Caption = "Trả xe"
        Me.BarButtonItem3.Glyph = CType(resources.GetObject("BarButtonItem3.Glyph"), System.Drawing.Image)
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1179, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 440)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1179, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 414)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1179, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 414)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Trả xe"
        Me.BarButtonItem1.Glyph = CType(resources.GetObject("BarButtonItem1.Glyph"), System.Drawing.Image)
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'frmNhanvaTraxe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 440)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.LookAndFeel.SkinName = "Blue"
        Me.Name = "frmNhanvaTraxe"
        Me.Text = "Nhận và trả xe"
        Me.tableLayoutPanel1.ResumeLayout(False)
        CType(Me.gcXeban, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsNhanxe.ResumeLayout(False)
        CType(Me.gvXeban, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTrangThai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcXeroi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsDangKyXe.ResumeLayout(False)
        CType(Me.gvXeroi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rilueXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private WithEvents gcXeban As DevExpress.XtraGrid.GridControl
    Private WithEvents gvXeban As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gcolIDxeban As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gcolTenxeban As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gcolBiensoxeban As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents RepositoryItemGridLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gcXeroi As DevExpress.XtraGrid.GridControl
    Private WithEvents gvXeroi As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gcolIDxeroi As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gcolTenxeroi As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gColbiensoxeroi As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents gcolTinhtrang As DevExpress.XtraGrid.Columns.GridColumn
    Private WithEvents rilueXe As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Private WithEvents repositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents gcolGhichu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolHanhtrinh As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolNguoisudung As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolIDsudungxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTrangthaixe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmsNhanxe As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NhậnXeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gcolNgaydidukien As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents AlertControl1 As DevExpress.XtraBars.Alerter.AlertControl
    Friend WithEvents TrảXeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gcolIDTrangthaixe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents HủyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsDangKyXe As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ĐăngKýMượnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents riLueTinhTrang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents riLueTrangThai As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
