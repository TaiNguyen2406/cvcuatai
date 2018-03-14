<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGiaoViec
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
        Me.gdvThiCong = New DevExpress.XtraGrid.GridControl()
        Me.gdvThiCongCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rgdvHangMucThiCong = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rtbThoiGian = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rPopUpDSThiCong = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.popupDSThiCong = New DevExpress.XtraEditors.PopupContainerControl()
        Me.treeNV = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rtbTime = New DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.gdvThiCong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvThiCongCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgdvHangMucThiCong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbThoiGian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbThoiGian.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rPopUpDSThiCong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.popupDSThiCong, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.popupDSThiCong.SuspendLayout()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvThiCong
        '
        Me.gdvThiCong.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gdvThiCong.Location = New System.Drawing.Point(0, 0)
        Me.gdvThiCong.MainView = Me.gdvThiCongCT
        Me.gdvThiCong.Name = "gdvThiCong"
        Me.gdvThiCong.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rtbThoiGian, Me.RepositoryItemMemoEdit3, Me.rPopUpDSThiCong, Me.rgdvHangMucThiCong, Me.rtbTime})
        Me.gdvThiCong.Size = New System.Drawing.Size(1338, 465)
        Me.gdvThiCong.TabIndex = 2
        Me.gdvThiCong.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvThiCongCT})
        '
        'gdvThiCongCT
        '
        Me.gdvThiCongCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvThiCongCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvThiCongCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvThiCongCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvThiCongCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvThiCongCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvThiCongCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvThiCongCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvThiCongCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn82, Me.GridColumn117, Me.GridColumn85, Me.GridColumn1, Me.GridColumn86, Me.GridColumn88, Me.GridColumn89, Me.GridColumn92, Me.GridColumn3, Me.GridColumn2, Me.GridColumn95, Me.GridColumn90, Me.GridColumn96, Me.GridColumn93})
        Me.gdvThiCongCT.GridControl = Me.gdvThiCong
        Me.gdvThiCongCT.Name = "gdvThiCongCT"
        Me.gdvThiCongCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvThiCongCT.OptionsCustomization.AllowFilter = False
        Me.gdvThiCongCT.OptionsCustomization.AllowSort = False
        Me.gdvThiCongCT.OptionsFilter.AllowColumnMRUFilterList = False
        Me.gdvThiCongCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvThiCongCT.OptionsFilter.AllowMRUFilterList = False
        Me.gdvThiCongCT.OptionsView.ColumnAutoWidth = False
        Me.gdvThiCongCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvThiCongCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvThiCongCT.OptionsView.RowAutoHeight = True
        Me.gdvThiCongCT.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gdvThiCongCT.OptionsView.ShowFooter = True
        Me.gdvThiCongCT.OptionsView.ShowGroupPanel = False
        Me.gdvThiCongCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn82, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn82
        '
        Me.GridColumn82.Caption = "AZ"
        Me.GridColumn82.FieldName = "AZ"
        Me.GridColumn82.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridColumn82.Name = "GridColumn82"
        Me.GridColumn82.OptionsColumn.FixedWidth = True
        Me.GridColumn82.Visible = True
        Me.GridColumn82.VisibleIndex = 0
        Me.GridColumn82.Width = 36
        '
        'GridColumn117
        '
        Me.GridColumn117.Caption = "ID"
        Me.GridColumn117.FieldName = "ID"
        Me.GridColumn117.Name = "GridColumn117"
        '
        'GridColumn85
        '
        Me.GridColumn85.Caption = "Nội dung thi công"
        Me.GridColumn85.ColumnEdit = Me.rgdvHangMucThiCong
        Me.GridColumn85.FieldName = "IDNoiDung"
        Me.GridColumn85.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridColumn85.Name = "GridColumn85"
        Me.GridColumn85.Visible = True
        Me.GridColumn85.VisibleIndex = 1
        Me.GridColumn85.Width = 232
        '
        'rgdvHangMucThiCong
        '
        Me.rgdvHangMucThiCong.AutoHeight = False
        Me.rgdvHangMucThiCong.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
        Me.rgdvHangMucThiCong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rgdvHangMucThiCong.DisplayMember = "NoiDung"
        Me.rgdvHangMucThiCong.Name = "rgdvHangMucThiCong"
        Me.rgdvHangMucThiCong.NullText = ""
        Me.rgdvHangMucThiCong.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rgdvHangMucThiCong.ValueMember = "ID"
        Me.rgdvHangMucThiCong.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsBehavior.Editable = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn1.Caption = "Mô tả"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemMemoEdit3
        Me.GridColumn1.FieldName = "MoTa"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 290
        '
        'RepositoryItemMemoEdit3
        '
        Me.RepositoryItemMemoEdit3.Name = "RepositoryItemMemoEdit3"
        '
        'GridColumn86
        '
        Me.GridColumn86.Caption = "Bắt đầu"
        Me.GridColumn86.ColumnEdit = Me.rtbThoiGian
        Me.GridColumn86.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn86.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn86.FieldName = "NgayBatDau"
        Me.GridColumn86.Name = "GridColumn86"
        Me.GridColumn86.OptionsColumn.FixedWidth = True
        Me.GridColumn86.Visible = True
        Me.GridColumn86.VisibleIndex = 3
        Me.GridColumn86.Width = 110
        '
        'rtbThoiGian
        '
        Me.rtbThoiGian.AutoHeight = False
        Me.rtbThoiGian.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rtbThoiGian.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.rtbThoiGian.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbThoiGian.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.rtbThoiGian.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbThoiGian.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.rtbThoiGian.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.rtbThoiGian.Name = "rtbThoiGian"
        Me.rtbThoiGian.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn88
        '
        Me.GridColumn88.Caption = "Kết thúc"
        Me.GridColumn88.ColumnEdit = Me.rtbThoiGian
        Me.GridColumn88.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn88.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn88.FieldName = "NgayKetThuc"
        Me.GridColumn88.Name = "GridColumn88"
        Me.GridColumn88.OptionsColumn.FixedWidth = True
        Me.GridColumn88.Visible = True
        Me.GridColumn88.VisibleIndex = 4
        Me.GridColumn88.Width = 110
        '
        'GridColumn89
        '
        Me.GridColumn89.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn89.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn89.Caption = "NV Thực hiện"
        Me.GridColumn89.ColumnEdit = Me.RepositoryItemMemoEdit3
        Me.GridColumn89.FieldName = "NgThucHien"
        Me.GridColumn89.Name = "GridColumn89"
        Me.GridColumn89.OptionsColumn.FixedWidth = True
        Me.GridColumn89.Visible = True
        Me.GridColumn89.VisibleIndex = 5
        Me.GridColumn89.Width = 160
        '
        'GridColumn92
        '
        Me.GridColumn92.ColumnEdit = Me.rPopUpDSThiCong
        Me.GridColumn92.FieldName = "IDNgThucHien"
        Me.GridColumn92.Name = "GridColumn92"
        Me.GridColumn92.OptionsColumn.ShowCaption = False
        Me.GridColumn92.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn92.Visible = True
        Me.GridColumn92.VisibleIndex = 6
        Me.GridColumn92.Width = 20
        '
        'rPopUpDSThiCong
        '
        Me.rPopUpDSThiCong.AutoHeight = False
        Me.rPopUpDSThiCong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rPopUpDSThiCong.Name = "rPopUpDSThiCong"
        Me.rPopUpDSThiCong.PopupControl = Me.popupDSThiCong
        '
        'popupDSThiCong
        '
        Me.popupDSThiCong.Controls.Add(Me.treeNV)
        Me.popupDSThiCong.Location = New System.Drawing.Point(345, 54)
        Me.popupDSThiCong.Name = "popupDSThiCong"
        Me.popupDSThiCong.Size = New System.Drawing.Size(264, 273)
        Me.popupDSThiCong.TabIndex = 8
        '
        'treeNV
        '
        Me.treeNV.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn2})
        Me.treeNV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeNV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeNV.Location = New System.Drawing.Point(0, 0)
        Me.treeNV.Name = "treeNV"
        Me.treeNV.OptionsBehavior.Editable = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.treeNV.OptionsView.ShowCheckBoxes = True
        Me.treeNV.OptionsView.ShowHorzLines = False
        Me.treeNV.OptionsView.ShowIndicator = False
        Me.treeNV.OptionsView.ShowVertLines = False
        Me.treeNV.ParentFieldName = "PhongBan"
        Me.treeNV.Size = New System.Drawing.Size(264, 273)
        Me.treeNV.TabIndex = 6
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Nhân viên"
        Me.TreeListColumn2.FieldName = "Nhân viên"
        Me.TreeListColumn2.MinWidth = 32
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 0
        Me.TreeListColumn2.Width = 372
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "NV Thông báo"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemMemoEdit3
        Me.GridColumn3.FieldName = "NgThongBao"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 7
        Me.GridColumn3.Width = 130
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.ColumnEdit = Me.rPopUpDSThiCong
        Me.GridColumn2.FieldName = "IDNgThongBao"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.ShowCaption = False
        Me.GridColumn2.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 8
        Me.GridColumn2.Width = 20
        '
        'GridColumn95
        '
        Me.GridColumn95.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn95.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn95.Caption = "Kiểm duyệt 1"
        Me.GridColumn95.ColumnEdit = Me.RepositoryItemMemoEdit3
        Me.GridColumn95.FieldName = "NVKiemDuyetLan1"
        Me.GridColumn95.Name = "GridColumn95"
        Me.GridColumn95.OptionsColumn.FixedWidth = True
        Me.GridColumn95.Visible = True
        Me.GridColumn95.VisibleIndex = 9
        Me.GridColumn95.Width = 156
        '
        'GridColumn90
        '
        Me.GridColumn90.Caption = "GridColumn90"
        Me.GridColumn90.ColumnEdit = Me.rPopUpDSThiCong
        Me.GridColumn90.FieldName = "IDNgKiemDuyet1"
        Me.GridColumn90.Name = "GridColumn90"
        Me.GridColumn90.OptionsColumn.ShowCaption = False
        Me.GridColumn90.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn90.Visible = True
        Me.GridColumn90.VisibleIndex = 10
        Me.GridColumn90.Width = 20
        '
        'GridColumn96
        '
        Me.GridColumn96.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn96.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn96.Caption = "Kiểm duyệt 2"
        Me.GridColumn96.ColumnEdit = Me.RepositoryItemMemoEdit3
        Me.GridColumn96.FieldName = "NVKiemDuyetLan2"
        Me.GridColumn96.Name = "GridColumn96"
        Me.GridColumn96.OptionsColumn.FixedWidth = True
        Me.GridColumn96.Visible = True
        Me.GridColumn96.VisibleIndex = 11
        Me.GridColumn96.Width = 162
        '
        'GridColumn93
        '
        Me.GridColumn93.Caption = "GridColumn93"
        Me.GridColumn93.ColumnEdit = Me.rPopUpDSThiCong
        Me.GridColumn93.FieldName = "IDNgKiemDuyet2"
        Me.GridColumn93.Name = "GridColumn93"
        Me.GridColumn93.OptionsColumn.ShowCaption = False
        Me.GridColumn93.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways
        Me.GridColumn93.Visible = True
        Me.GridColumn93.VisibleIndex = 12
        Me.GridColumn93.Width = 20
        '
        'rtbTime
        '
        Me.rtbTime.AutoHeight = False
        Me.rtbTime.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.rtbTime.DisplayFormat.FormatString = "HH:mm"
        Me.rtbTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbTime.EditFormat.FormatString = "HH:mm"
        Me.rtbTime.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbTime.Mask.EditMask = "HH:mm"
        Me.rtbTime.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.rtbTime.Name = "rtbTime"
        '
        'btLuuLai
        '
        Me.btLuuLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(1163, 471)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(75, 27)
        Me.btLuuLai.TabIndex = 9
        Me.btLuuLai.Text = "Lưu lại"
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(1244, 471)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 27)
        Me.btDong.TabIndex = 9
        Me.btDong.Text = "Đóng"
        '
        'frmGiaoViec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1338, 504)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.popupDSThiCong)
        Me.Controls.Add(Me.gdvThiCong)
        Me.MaximizeBox = False
        Me.Name = "frmGiaoViec"
        Me.Text = "Giao việc kỹ thuật"
        CType(Me.gdvThiCong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvThiCongCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgdvHangMucThiCong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbThoiGian.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbThoiGian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rPopUpDSThiCong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.popupDSThiCong, System.ComponentModel.ISupportInitialize).EndInit()
        Me.popupDSThiCong.ResumeLayout(False)
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdvThiCong As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvThiCongCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn117 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rgdvHangMucThiCong As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rtbThoiGian As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents GridColumn92 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rPopUpDSThiCong As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GridColumn95 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn96 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn93 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents popupDSThiCong As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents treeNV As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rtbTime As DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
