<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhieuDanhGiaNV
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cbBoPhan = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThoiGian = New DevExpress.XtraEditors.DateEdit()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rtbN0 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btTaiLai = New DevExpress.XtraEditors.SimpleButton()
        Me.cbNguoiDanhGia = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbCapQuanLy = New DevExpress.XtraEditors.ComboBoxEdit()
        CType(Me.cbBoPhan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGian.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGian.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbN0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNguoiDanhGia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbCapQuanLy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 16)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(72, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Người đánh giá"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(274, 16)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Bộ phận"
        '
        'cbBoPhan
        '
        Me.cbBoPhan.Location = New System.Drawing.Point(319, 13)
        Me.cbBoPhan.Name = "cbBoPhan"
        Me.cbBoPhan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbBoPhan.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name2")})
        Me.cbBoPhan.Properties.DisplayMember = "NoiDung"
        Me.cbBoPhan.Properties.DropDownItemHeight = 22
        Me.cbBoPhan.Properties.NullText = ""
        Me.cbBoPhan.Properties.ShowHeader = False
        Me.cbBoPhan.Properties.ValueMember = "Ma"
        Me.cbBoPhan.Size = New System.Drawing.Size(100, 20)
        Me.cbBoPhan.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(425, 16)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Thời gian"
        '
        'tbThoiGian
        '
        Me.tbThoiGian.EditValue = Nothing
        Me.tbThoiGian.Location = New System.Drawing.Point(474, 13)
        Me.tbThoiGian.Name = "tbThoiGian"
        Me.tbThoiGian.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.tbThoiGian.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.tbThoiGian.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.tbThoiGian.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.tbThoiGian.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.tbThoiGian.Properties.Mask.EditMask = "MM/yyyy"
        Me.tbThoiGian.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGian.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        Me.tbThoiGian.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGian.Size = New System.Drawing.Size(100, 20)
        Me.tbThoiGian.TabIndex = 2
        '
        'gdv
        '
        Me.gdv.Location = New System.Drawing.Point(12, 72)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1, Me.rtbN0})
        Me.gdv.Size = New System.Drawing.Size(705, 485)
        Me.gdv.TabIndex = 7
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.gdvCT.Appearance.Row.Options.UseFont = True
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8})
        Me.gdvCT.FixedLineWidth = 1
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsCustomization.AllowColumnMoving = False
        Me.gdvCT.OptionsCustomization.AllowGroup = False
        Me.gdvCT.OptionsLayout.Columns.AddNewColumns = False
        Me.gdvCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        Me.gdvCT.RowHeight = 23
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nhân viên"
        Me.GridColumn1.FieldName = "TenNV"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 204
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Chủ động hoàn thành cv"
        Me.GridColumn2.ColumnEdit = Me.rtbN0
        Me.GridColumn2.FieldName = "ChuDongHTCongViec"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 159
        '
        'rtbN0
        '
        Me.rtbN0.AutoHeight = False
        Me.rtbN0.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.rtbN0.MaxValue = New Decimal(New Integer() {4, 0, 0, 0})
        Me.rtbN0.Name = "rtbN0"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Chủ động tìm cv để làm"
        Me.GridColumn3.ColumnEdit = Me.rtbN0
        Me.GridColumn3.FieldName = "ChuDongTimViecDeLam"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 159
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Linh hoạt biết việc để làm"
        Me.GridColumn4.ColumnEdit = Me.rtbN0
        Me.GridColumn4.FieldName = "LinhHoatBietViecDeLam"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 165
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "isQuanLy"
        Me.GridColumn5.FieldName = "isQuanLy"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Modify"
        Me.GridColumn6.FieldName = "Modify"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "IDNhanVien"
        Me.GridColumn7.FieldName = "IDNhanVien"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "GridColumn8"
        Me.GridColumn8.FieldName = "ID"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        Me.RepositoryItemMemoEdit1.ValidateOnEnterKey = True
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(535, 563)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(81, 33)
        Me.btLuuLai.TabIndex = 4
        Me.btLuuLai.Text = "Lưu lại"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(622, 563)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 33)
        Me.btDong.TabIndex = 5
        Me.btDong.Text = "Đóng"
        '
        'btTaiLai
        '
        Me.btTaiLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btTaiLai.Appearance.Options.UseFont = True
        Me.btTaiLai.Image = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btTaiLai.Location = New System.Drawing.Point(580, 12)
        Me.btTaiLai.Name = "btTaiLai"
        Me.btTaiLai.Size = New System.Drawing.Size(81, 21)
        Me.btTaiLai.TabIndex = 3
        Me.btTaiLai.Text = "Tải lại"
        '
        'cbNguoiDanhGia
        '
        Me.cbNguoiDanhGia.Location = New System.Drawing.Point(92, 13)
        Me.cbNguoiDanhGia.Name = "cbNguoiDanhGia"
        Me.cbNguoiDanhGia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbNguoiDanhGia.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NhomKN", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.cbNguoiDanhGia.Properties.DisplayMember = "Ten"
        Me.cbNguoiDanhGia.Properties.DropDownItemHeight = 22
        Me.cbNguoiDanhGia.Properties.NullText = ""
        Me.cbNguoiDanhGia.Properties.ShowHeader = False
        Me.cbNguoiDanhGia.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbNguoiDanhGia.Properties.ValueMember = "ID"
        Me.cbNguoiDanhGia.Size = New System.Drawing.Size(176, 20)
        Me.cbNguoiDanhGia.TabIndex = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(27, 43)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Cấp quản lý"
        '
        'cbCapQuanLy
        '
        Me.cbCapQuanLy.EditValue = "Nhân viên"
        Me.cbCapQuanLy.Location = New System.Drawing.Point(92, 40)
        Me.cbCapQuanLy.Name = "cbCapQuanLy"
        Me.cbCapQuanLy.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbCapQuanLy.Properties.DropDownItemHeight = 22
        Me.cbCapQuanLy.Properties.Items.AddRange(New Object() {"Nhân viên", "Phó phòng", "Trưởng phòng"})
        Me.cbCapQuanLy.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cbCapQuanLy.Size = New System.Drawing.Size(176, 20)
        Me.cbCapQuanLy.TabIndex = 8
        '
        'frmPhieuDanhGiaNV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 608)
        Me.Controls.Add(Me.cbCapQuanLy)
        Me.Controls.Add(Me.cbNguoiDanhGia)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btTaiLai)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.tbThoiGian)
        Me.Controls.Add(Me.cbBoPhan)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmPhieuDanhGiaNV"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Phiếu đánh giá nhân viên"
        CType(Me.cbBoPhan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGian.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGian.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbN0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNguoiDanhGia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbCapQuanLy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbBoPhan As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGian As DevExpress.XtraEditors.DateEdit
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btTaiLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rtbN0 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents cbNguoiDanhGia As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbCapQuanLy As DevExpress.XtraEditors.ComboBoxEdit
End Class
