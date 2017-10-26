<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLichSuNhapXuat
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.gdvNhap = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhapCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.gdvXuat = New DevExpress.XtraGrid.GridControl()
        Me.gdvXuatCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTonKho = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTienTonKho = New DevExpress.XtraEditors.SpinEdit()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.gdvNhap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhapCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.gdvXuat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvXuatCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTonKho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTienTonKho.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainerControl1.Horizontal = False
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.GroupControl1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.GroupControl2)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(1057, 472)
        Me.SplitContainerControl1.SplitterPosition = 230
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.gdvNhap)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1057, 230)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Lịch sử nhập"
        '
        'gdvNhap
        '
        Me.gdvNhap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNhap.Location = New System.Drawing.Point(2, 21)
        Me.gdvNhap.MainView = Me.gdvNhapCT
        Me.gdvNhap.Name = "gdvNhap"
        Me.gdvNhap.Size = New System.Drawing.Size(1053, 207)
        Me.gdvNhap.TabIndex = 1
        Me.gdvNhap.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhapCT})
        '
        'gdvNhapCT
        '
        Me.gdvNhapCT.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.gdvNhapCT.Appearance.FooterPanel.Options.UseFont = True
        Me.gdvNhapCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhapCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhapCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhapCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhapCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6, Me.GridColumn7, Me.GridColumn1, Me.GridColumn8, Me.GridColumn2, Me.GridColumn3, Me.GridColumn20, Me.GridColumn17, Me.GridColumn4, Me.GridColumn5})
        Me.gdvNhapCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvNhapCT.GridControl = Me.gdvNhap
        Me.gdvNhapCT.Name = "gdvNhapCT"
        Me.gdvNhapCT.OptionsBehavior.ReadOnly = True
        Me.gdvNhapCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvNhapCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvNhapCT.OptionsView.ColumnAutoWidth = False
        Me.gdvNhapCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhapCT.OptionsView.ShowFooter = True
        Me.gdvNhapCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhapCT.RowHeight = 22
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Mã KH"
        Me.GridColumn6.FieldName = "ttcMa"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        Me.GridColumn6.Width = 158
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Số phiếu"
        Me.GridColumn7.FieldName = "SoPhieu"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Ngày"
        Me.GridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "NgayThang"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 81
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Mã hàng"
        Me.GridColumn8.FieldName = "Model"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 147
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn2.Caption = "SL"
        Me.GridColumn2.FieldName = "SoLuong"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 4
        Me.GridColumn2.Width = 42
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn3.Caption = "Đơn giá"
        Me.GridColumn3.DisplayFormat.FormatString = "N2"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "DonGia"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 5
        Me.GridColumn3.Width = 80
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Thành tiền"
        Me.GridColumn17.DisplayFormat.FormatString = "N2"
        Me.GridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn17.FieldName = "ThanhTien"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.SummaryItem.DisplayFormat = "{0:N2}"
        Me.GridColumn17.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 7
        Me.GridColumn17.Width = 107
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Người đặt hàng"
        Me.GridColumn4.FieldName = "TakeCare"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 8
        Me.GridColumn4.Width = 159
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Người nhập kho"
        Me.GridColumn5.FieldName = "NguoiNhapKho"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 9
        Me.GridColumn5.Width = 149
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.gdvXuat)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1057, 237)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Lịch sử xuất"
        '
        'gdvXuat
        '
        Me.gdvXuat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvXuat.Location = New System.Drawing.Point(2, 21)
        Me.gdvXuat.MainView = Me.gdvXuatCT
        Me.gdvXuat.Name = "gdvXuat"
        Me.gdvXuat.Size = New System.Drawing.Size(1053, 214)
        Me.gdvXuat.TabIndex = 2
        Me.gdvXuat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvXuatCT})
        '
        'gdvXuatCT
        '
        Me.gdvXuatCT.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvXuatCT.Appearance.FooterPanel.Options.UseFont = True
        Me.gdvXuatCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvXuatCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvXuatCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvXuatCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvXuatCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9, Me.GridColumn10, Me.GridColumn19, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13, Me.GridColumn14, Me.GridColumn18, Me.GridColumn15, Me.GridColumn16})
        Me.gdvXuatCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvXuatCT.GridControl = Me.gdvXuat
        Me.gdvXuatCT.Name = "gdvXuatCT"
        Me.gdvXuatCT.OptionsBehavior.ReadOnly = True
        Me.gdvXuatCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvXuatCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvXuatCT.OptionsView.ColumnAutoWidth = False
        Me.gdvXuatCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvXuatCT.OptionsView.ShowFooter = True
        Me.gdvXuatCT.OptionsView.ShowGroupPanel = False
        Me.gdvXuatCT.RowHeight = 22
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Mã KH"
        Me.GridColumn9.FieldName = "ttcMa"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 135
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.Caption = "Số phiếu"
        Me.GridColumn10.FieldName = "SoPhieu"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn10.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 1
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "CT"
        Me.GridColumn19.FieldName = "CongTrinh"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 2
        Me.GridColumn19.Width = 26
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn11.Caption = "Ngày"
        Me.GridColumn11.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn11.FieldName = "NgayThang"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 3
        Me.GridColumn11.Width = 81
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Mã hàng"
        Me.GridColumn12.FieldName = "Model"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 4
        Me.GridColumn12.Width = 145
        '
        'GridColumn13
        '
        Me.GridColumn13.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn13.Caption = "SL"
        Me.GridColumn13.FieldName = "SoLuong"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn13.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 5
        Me.GridColumn13.Width = 42
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn14.Caption = "Đơn giá"
        Me.GridColumn14.DisplayFormat.FormatString = "N2"
        Me.GridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn14.FieldName = "DonGia"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 6
        Me.GridColumn14.Width = 80
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Thành tiền"
        Me.GridColumn18.DisplayFormat.FormatString = "N2"
        Me.GridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn18.FieldName = "ThanhTien"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.SummaryItem.DisplayFormat = "{0:N2}"
        Me.GridColumn18.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 7
        Me.GridColumn18.Width = 107
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Kinh doanh"
        Me.GridColumn15.FieldName = "TakeCare"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 8
        Me.GridColumn15.Width = 160
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Người xuất kho"
        Me.GridColumn16.FieldName = "NguoiXuatKho"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 9
        Me.GridColumn16.Width = 147
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Location = New System.Drawing.Point(368, 481)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Tồn kho:"
        '
        'tbTonKho
        '
        Me.tbTonKho.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbTonKho.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.tbTonKho.Location = New System.Drawing.Point(421, 478)
        Me.tbTonKho.Name = "tbTonKho"
        Me.tbTonKho.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tbTonKho.Properties.Appearance.Options.UseFont = True
        Me.tbTonKho.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.tbTonKho.Size = New System.Drawing.Size(100, 20)
        Me.tbTonKho.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl2.Location = New System.Drawing.Point(525, 481)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Tiền tồn kho:"
        '
        'tbTienTonKho
        '
        Me.tbTienTonKho.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbTienTonKho.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.tbTienTonKho.Location = New System.Drawing.Point(594, 478)
        Me.tbTienTonKho.Name = "tbTienTonKho"
        Me.tbTienTonKho.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tbTienTonKho.Properties.Appearance.Options.UseFont = True
        Me.tbTienTonKho.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.tbTienTonKho.Properties.DisplayFormat.FormatString = "N2"
        Me.tbTienTonKho.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbTienTonKho.Properties.EditFormat.FormatString = "N2"
        Me.tbTienTonKho.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbTienTonKho.Size = New System.Drawing.Size(113, 20)
        Me.tbTienTonKho.TabIndex = 2
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Chi phí"
        Me.GridColumn20.DisplayFormat.FormatString = "N2"
        Me.GridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn20.FieldName = "ChiPhi"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Visible = True
        Me.GridColumn20.VisibleIndex = 6
        '
        'frmLichSuNhapXuat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1057, 506)
        Me.Controls.Add(Me.tbTienTonKho)
        Me.Controls.Add(Me.tbTonKho)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.SplitContainerControl1)
        Me.Name = "frmLichSuNhapXuat"
        Me.Text = "Lịch sử nhập xuất"
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.gdvNhap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhapCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.gdvXuat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvXuatCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTonKho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTienTonKho.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvNhap As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNhapCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gdvXuat As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvXuatCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbTonKho As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbTienTonKho As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
End Class
