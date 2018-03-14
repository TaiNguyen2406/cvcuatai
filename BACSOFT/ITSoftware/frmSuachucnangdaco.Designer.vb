<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSuachucnangdaco
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDSChucnang = New DevExpress.XtraGrid.GridControl()
        Me.grdViewDSChucnang = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDulieubaotri = New DevExpress.XtraGrid.GridControl()
        Me.grdViewDulieubaotri = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.grdDSChucnang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewDSChucnang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grdDulieubaotri, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewDulieubaotri, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btLuuLai.Location = New System.Drawing.Point(932, 464)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(131, 41)
        Me.btLuuLai.TabIndex = 2
        Me.btLuuLai.Text = "OK"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(1076, 464)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 3
        Me.btDong.Text = "Đóng"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.grdDSChucnang)
        Me.GroupControl2.Location = New System.Drawing.Point(9, 10)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1171, 446)
        Me.GroupControl2.TabIndex = 23
        Me.GroupControl2.Text = "Chọn một hoặc nhiều chức năng cần sửa"
        '
        'grdDSChucnang
        '
        Me.grdDSChucnang.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDSChucnang.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDSChucnang.Location = New System.Drawing.Point(2, 25)
        Me.grdDSChucnang.MainView = Me.grdViewDSChucnang
        Me.grdDSChucnang.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDSChucnang.Name = "grdDSChucnang"
        Me.grdDSChucnang.Size = New System.Drawing.Size(1167, 419)
        Me.grdDSChucnang.TabIndex = 1
        Me.grdDSChucnang.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewDSChucnang})
        '
        'grdViewDSChucnang
        '
        Me.grdViewDSChucnang.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSChucnang.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdViewDSChucnang.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSChucnang.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdViewDSChucnang.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSChucnang.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdViewDSChucnang.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13, Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn31, Me.GridColumn32, Me.GridColumn33, Me.GridColumn35, Me.GridColumn36, Me.GridColumn37, Me.GridColumn38, Me.GridColumn39, Me.GridColumn40, Me.GridColumn41, Me.GridColumn42, Me.GridColumn43, Me.GridColumn44, Me.GridColumn45, Me.GridColumn46})
        Me.grdViewDSChucnang.CustomizationFormBounds = New System.Drawing.Rectangle(909, 273, 216, 183)
        Me.grdViewDSChucnang.GridControl = Me.grdDSChucnang
        Me.grdViewDSChucnang.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.grdViewDSChucnang.Name = "grdViewDSChucnang"
        Me.grdViewDSChucnang.OptionsView.ColumnAutoWidth = False
        Me.grdViewDSChucnang.OptionsView.ShowAutoFilterRow = True
        Me.grdViewDSChucnang.OptionsView.ShowGroupPanel = False
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn13.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn13.AppearanceHeader.Options.UseFont = True
        Me.GridColumn13.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn13.Caption = "STT"
        Me.GridColumn13.FieldName = "Stt"
        Me.GridColumn13.MaxWidth = 50
        Me.GridColumn13.MinWidth = 50
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        Me.GridColumn13.Width = 50
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn24.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn24.AppearanceHeader.Options.UseFont = True
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn24.Caption = "Mã vật tư"
        Me.GridColumn24.FieldName = "IdVatTu"
        Me.GridColumn24.MaxWidth = 80
        Me.GridColumn24.MinWidth = 80
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsFilter.AllowFilter = False
        Me.GridColumn24.Width = 80
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn25.AppearanceHeader.Options.UseFont = True
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn25.Caption = "Mô tả chức năng"
        Me.GridColumn25.FieldName = "Motachucnang"
        Me.GridColumn25.MaxWidth = 360
        Me.GridColumn25.MinWidth = 360
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 2
        Me.GridColumn25.Width = 360
        '
        'GridColumn26
        '
        Me.GridColumn26.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn26.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn26.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn26.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn26.Caption = "[x]"
        Me.GridColumn26.FieldName = "checkbox"
        Me.GridColumn26.MaxWidth = 30
        Me.GridColumn26.MinWidth = 30
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 1
        Me.GridColumn26.Width = 30
        '
        'GridColumn27
        '
        Me.GridColumn27.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn27.AppearanceHeader.Options.UseFont = True
        Me.GridColumn27.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn27.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn27.Caption = "Trạng thái"
        Me.GridColumn27.FieldName = "Trangthaichucnang"
        Me.GridColumn27.MaxWidth = 140
        Me.GridColumn27.MinWidth = 140
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.OptionsColumn.AllowEdit = False
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 3
        Me.GridColumn27.Width = 140
        '
        'GridColumn31
        '
        Me.GridColumn31.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn31.AppearanceHeader.Options.UseFont = True
        Me.GridColumn31.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn31.Caption = "File đính kèm"
        Me.GridColumn31.FieldName = "Filedinhkem"
        Me.GridColumn31.MaxWidth = 150
        Me.GridColumn31.MinWidth = 150
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.Visible = True
        Me.GridColumn31.VisibleIndex = 4
        Me.GridColumn31.Width = 150
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn32.AppearanceHeader.Options.UseFont = True
        Me.GridColumn32.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn32.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn32.Caption = "Coder"
        Me.GridColumn32.FieldName = "Nguoinhanviec"
        Me.GridColumn32.MaxWidth = 150
        Me.GridColumn32.MinWidth = 150
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.OptionsColumn.AllowEdit = False
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 5
        Me.GridColumn32.Width = 150
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn33.AppearanceHeader.Options.UseFont = True
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn33.Caption = "Tester"
        Me.GridColumn33.FieldName = "DanhsachNguoiTest"
        Me.GridColumn33.MaxWidth = 170
        Me.GridColumn33.MinWidth = 170
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Visible = True
        Me.GridColumn33.VisibleIndex = 6
        Me.GridColumn33.Width = 170
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn35.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn35.AppearanceHeader.Options.UseFont = True
        Me.GridColumn35.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn35.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn35.Caption = "Thời hạn HT"
        Me.GridColumn35.FieldName = "Thoihanhoanthanh"
        Me.GridColumn35.MaxWidth = 80
        Me.GridColumn35.MinWidth = 80
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.OptionsColumn.AllowEdit = False
        Me.GridColumn35.Width = 80
        '
        'GridColumn36
        '
        Me.GridColumn36.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn36.AppearanceHeader.Options.UseFont = True
        Me.GridColumn36.Caption = "Ngày NV thực tế"
        Me.GridColumn36.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn36.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn36.FieldName = "NgaynhanviecThucte"
        Me.GridColumn36.MaxWidth = 100
        Me.GridColumn36.MinWidth = 100
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.OptionsColumn.AllowEdit = False
        Me.GridColumn36.Visible = True
        Me.GridColumn36.VisibleIndex = 7
        Me.GridColumn36.Width = 100
        '
        'GridColumn37
        '
        Me.GridColumn37.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn37.AppearanceHeader.Options.UseFont = True
        Me.GridColumn37.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn37.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn37.Caption = "Ngày HT thực tế"
        Me.GridColumn37.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn37.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn37.FieldName = "NgayketthucThucte"
        Me.GridColumn37.MaxWidth = 100
        Me.GridColumn37.MinWidth = 100
        Me.GridColumn37.Name = "GridColumn37"
        Me.GridColumn37.OptionsColumn.AllowEdit = False
        Me.GridColumn37.Visible = True
        Me.GridColumn37.VisibleIndex = 8
        Me.GridColumn37.Width = 100
        '
        'GridColumn38
        '
        Me.GridColumn38.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn38.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn38.AppearanceHeader.Options.UseFont = True
        Me.GridColumn38.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn38.Caption = "Ngày nhận việc"
        Me.GridColumn38.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn38.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn38.FieldName = "Ngaynhanviec"
        Me.GridColumn38.MaxWidth = 110
        Me.GridColumn38.MinWidth = 110
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.OptionsColumn.AllowEdit = False
        Me.GridColumn38.Visible = True
        Me.GridColumn38.VisibleIndex = 9
        Me.GridColumn38.Width = 110
        '
        'GridColumn39
        '
        Me.GridColumn39.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn39.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn39.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn39.AppearanceHeader.Options.UseFont = True
        Me.GridColumn39.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn39.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn39.Caption = "Ngày hoàn thành"
        Me.GridColumn39.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn39.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn39.FieldName = "Ngayketthuc"
        Me.GridColumn39.MaxWidth = 110
        Me.GridColumn39.MinWidth = 110
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.OptionsColumn.AllowEdit = False
        Me.GridColumn39.Visible = True
        Me.GridColumn39.VisibleIndex = 10
        Me.GridColumn39.Width = 110
        '
        'GridColumn40
        '
        Me.GridColumn40.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn40.AppearanceHeader.Options.UseFont = True
        Me.GridColumn40.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn40.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn40.Caption = "Tình trạng bảo trì"
        Me.GridColumn40.FieldName = "Tinhtrang"
        Me.GridColumn40.MaxWidth = 150
        Me.GridColumn40.MinWidth = 150
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.Visible = True
        Me.GridColumn40.VisibleIndex = 11
        Me.GridColumn40.Width = 150
        '
        'GridColumn41
        '
        Me.GridColumn41.Caption = "RecordId"
        Me.GridColumn41.FieldName = "Id"
        Me.GridColumn41.Name = "GridColumn41"
        '
        'GridColumn42
        '
        Me.GridColumn42.Caption = "IdGiaoviec"
        Me.GridColumn42.FieldName = "IdGiaoviec"
        Me.GridColumn42.Name = "GridColumn42"
        '
        'GridColumn43
        '
        Me.GridColumn43.Caption = "Người hỗ trợ"
        Me.GridColumn43.FieldName = "DanhsachNguoiPhoihop"
        Me.GridColumn43.Name = "GridColumn43"
        '
        'GridColumn44
        '
        Me.GridColumn44.Caption = "Ghi chú GV"
        Me.GridColumn44.FieldName = "GhichuGV"
        Me.GridColumn44.Name = "GridColumn44"
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "TesterIds"
        Me.GridColumn45.FieldName = "IdDanhsachNguoiTest"
        Me.GridColumn45.Name = "GridColumn45"
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "NguoihotroIds"
        Me.GridColumn46.FieldName = "IdDanhsachNguoiPhoihop"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.grdDulieubaotri)
        Me.GroupControl1.Location = New System.Drawing.Point(9, 10)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1171, 291)
        Me.GroupControl1.TabIndex = 24
        Me.GroupControl1.Text = "Chọn một yêu cầu bảo trì"
        Me.GroupControl1.Visible = False
        '
        'grdDulieubaotri
        '
        Me.grdDulieubaotri.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDulieubaotri.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDulieubaotri.Location = New System.Drawing.Point(2, 25)
        Me.grdDulieubaotri.MainView = Me.grdViewDulieubaotri
        Me.grdDulieubaotri.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDulieubaotri.Name = "grdDulieubaotri"
        Me.grdDulieubaotri.Size = New System.Drawing.Size(1167, 264)
        Me.grdDulieubaotri.TabIndex = 9
        Me.grdDulieubaotri.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewDulieubaotri})
        '
        'grdViewDulieubaotri
        '
        Me.grdViewDulieubaotri.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDulieubaotri.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdViewDulieubaotri.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDulieubaotri.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdViewDulieubaotri.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDulieubaotri.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdViewDulieubaotri.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn10})
        Me.grdViewDulieubaotri.GridControl = Me.grdDulieubaotri
        Me.grdViewDulieubaotri.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.grdViewDulieubaotri.Name = "grdViewDulieubaotri"
        Me.grdViewDulieubaotri.OptionsView.ColumnAutoWidth = False
        Me.grdViewDulieubaotri.OptionsView.ShowAutoFilterRow = True
        Me.grdViewDulieubaotri.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "STT"
        Me.GridColumn1.FieldName = "Stt"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 43
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn2.AppearanceHeader.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Mã vật tư"
        Me.GridColumn2.FieldName = "IdVatTu"
        Me.GridColumn2.MaxWidth = 90
        Me.GridColumn2.MinWidth = 90
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 90
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Ngày yêu cầu"
        Me.GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "Ngaythongbao"
        Me.GridColumn3.MaxWidth = 100
        Me.GridColumn3.MinWidth = 100
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 100
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn4.AppearanceHeader.Options.UseFont = True
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Người yêu cầu"
        Me.GridColumn4.FieldName = "Nguoithongbao"
        Me.GridColumn4.MaxWidth = 100
        Me.GridColumn4.MinWidth = 100
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 100
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Nội dung yêu cầu"
        Me.GridColumn5.FieldName = "Noidungthongbao"
        Me.GridColumn5.MaxWidth = 400
        Me.GridColumn5.MinWidth = 400
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 400
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn6.AppearanceHeader.Options.UseFont = True
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "Trạng thái xử lý"
        Me.GridColumn6.FieldName = "Trangthaixuly"
        Me.GridColumn6.MaxWidth = 100
        Me.GridColumn6.MinWidth = 100
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 100
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn21.AppearanceHeader.Options.UseFont = True
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn21.Caption = "Người nhận xử lý"
        Me.GridColumn21.FieldName = "Nguoinhanxuly"
        Me.GridColumn21.MaxWidth = 140
        Me.GridColumn21.MinWidth = 140
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 6
        Me.GridColumn21.Width = 140
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn22.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn22.AppearanceHeader.Options.UseFont = True
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn22.Caption = "Ngày nhận xử lý"
        Me.GridColumn22.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn22.FieldName = "Ngaynhanxuly"
        Me.GridColumn22.MaxWidth = 100
        Me.GridColumn22.MinWidth = 100
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 7
        Me.GridColumn22.Width = 100
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn23.AppearanceHeader.Options.UseFont = True
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn23.Caption = "Ghi chú nhận xử lý"
        Me.GridColumn23.FieldName = "Ghichu"
        Me.GridColumn23.MaxWidth = 450
        Me.GridColumn23.MinWidth = 450
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 8
        Me.GridColumn23.Width = 450
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "RecordId"
        Me.GridColumn10.FieldName = "Id"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'frmSuachucnangdaco
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 516)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSuachucnangdaco"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Chọn chức năng cần sửa, nâng cấp"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.grdDSChucnang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewDSChucnang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grdDulieubaotri, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewDulieubaotri, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdDulieubaotri As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewDulieubaotri As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdDSChucnang As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewDSChucnang As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
End Class
