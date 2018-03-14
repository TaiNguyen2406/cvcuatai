<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGiaoNhanViec
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNgaynhanviec = New DevExpress.XtraEditors.DateEdit()
        Me.cboNguoinhanviec = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtThoihan = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNgayhoanthanh = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.gListFile = New DevExpress.XtraEditors.GroupControl()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.cboNguoitest = New DevExpress.XtraEditors.CheckedComboBoxEdit()
        Me.cboNguoiphoihop = New DevExpress.XtraEditors.CheckedComboBoxEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.DateEdit2 = New DevExpress.XtraEditors.DateEdit()
        CType(Me.txtNgaynhanviec.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaynhanviec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNguoinhanviec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtThoihan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgayhoanthanh.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgayhoanthanh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFile.SuspendLayout()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNguoitest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboNguoiphoihop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(14, 16)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(95, 17)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Ngày nhận việc"
        '
        'txtNgaynhanviec
        '
        Me.txtNgaynhanviec.EditValue = Nothing
        Me.txtNgaynhanviec.Location = New System.Drawing.Point(129, 11)
        Me.txtNgaynhanviec.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaynhanviec.Name = "txtNgaynhanviec"
        Me.txtNgaynhanviec.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaynhanviec.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaynhanviec.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaynhanviec.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaynhanviec.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaynhanviec.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgaynhanviec.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgaynhanviec.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaynhanviec.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaynhanviec.TabIndex = 1
        '
        'cboNguoinhanviec
        '
        Me.cboNguoinhanviec.Location = New System.Drawing.Point(129, 48)
        Me.cboNguoinhanviec.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboNguoinhanviec.Name = "cboNguoinhanviec"
        Me.cboNguoinhanviec.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboNguoinhanviec.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", 50, "Tên")})
        Me.cboNguoinhanviec.Properties.DisplayMember = "Ten"
        Me.cboNguoinhanviec.Properties.DropDownItemHeight = 22
        Me.cboNguoinhanviec.Properties.NullText = ""
        Me.cboNguoinhanviec.Properties.ShowHeader = False
        Me.cboNguoinhanviec.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cboNguoinhanviec.Properties.ValueMember = "Id"
        Me.cboNguoinhanviec.Size = New System.Drawing.Size(559, 22)
        Me.cboNguoinhanviec.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(13, 53)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(37, 17)
        Me.LabelControl2.TabIndex = 33
        Me.LabelControl2.Text = "Coder"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(13, 493)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(128, 17)
        Me.LabelControl3.TabIndex = 34
        Me.LabelControl3.Text = "Thời hạn hoàn thành"
        '
        'txtThoihan
        '
        Me.txtThoihan.EditValue = "1"
        Me.txtThoihan.Location = New System.Drawing.Point(129, 489)
        Me.txtThoihan.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtThoihan.Name = "txtThoihan"
        Me.txtThoihan.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtThoihan.Properties.Appearance.Options.UseBackColor = True
        Me.txtThoihan.Properties.DisplayFormat.FormatString = "d"
        Me.txtThoihan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtThoihan.Properties.EditFormat.FormatString = "d"
        Me.txtThoihan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtThoihan.Properties.Mask.EditMask = "d"
        Me.txtThoihan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtThoihan.Size = New System.Drawing.Size(146, 22)
        Me.txtThoihan.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(298, 17)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(107, 17)
        Me.LabelControl4.TabIndex = 36
        Me.LabelControl4.Text = "Ngày hoàn thành"
        '
        'txtNgayhoanthanh
        '
        Me.txtNgayhoanthanh.EditValue = Nothing
        Me.txtNgayhoanthanh.Location = New System.Drawing.Point(412, 12)
        Me.txtNgayhoanthanh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgayhoanthanh.Name = "txtNgayhoanthanh"
        Me.txtNgayhoanthanh.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtNgayhoanthanh.Properties.Appearance.Options.UseBackColor = True
        Me.txtNgayhoanthanh.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgayhoanthanh.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgayhoanthanh.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgayhoanthanh.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgayhoanthanh.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgayhoanthanh.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgayhoanthanh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgayhoanthanh.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgayhoanthanh.Size = New System.Drawing.Size(146, 22)
        Me.txtNgayhoanthanh.TabIndex = 37
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(13, 87)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(38, 17)
        Me.LabelControl5.TabIndex = 38
        Me.LabelControl5.Text = "Tester"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(14, 121)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(78, 17)
        Me.LabelControl6.TabIndex = 39
        Me.LabelControl6.Text = "Người hỗ trợ"
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(129, 335)
        Me.txtGhichu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGhichu.Properties.Appearance.Options.UseFont = True
        Me.txtGhichu.Properties.MaxLength = 100
        Me.txtGhichu.Size = New System.Drawing.Size(559, 80)
        Me.txtGhichu.TabIndex = 6
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(14, 365)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(46, 17)
        Me.LabelControl7.TabIndex = 41
        Me.LabelControl7.Text = "Ghi chú"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(12, 231)
        Me.LabelControl8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(78, 17)
        Me.LabelControl8.TabIndex = 44
        Me.LabelControl8.Text = "File đính kèm"
        '
        'btXoaFile
        '
        Me.btXoaFile.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(600, 290)
        Me.btXoaFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(87, 28)
        Me.btXoaFile.TabIndex = 47
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(600, 254)
        Me.btThemFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(87, 28)
        Me.btThemFile.TabIndex = 46
        Me.btThemFile.Text = "Thêm file"
        '
        'gListFile
        '
        Me.gListFile.Controls.Add(Me.gdvFile)
        Me.gListFile.Location = New System.Drawing.Point(129, 154)
        Me.gListFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gListFile.Name = "gListFile"
        Me.gListFile.Size = New System.Drawing.Size(464, 164)
        Me.gListFile.TabIndex = 45
        Me.gListFile.Text = "Danh sách file"
        '
        'gdvFile
        '
        Me.gdvFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvFile.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gdvFile.Location = New System.Drawing.Point(2, 24)
        Me.gdvFile.MainView = Me.gdvFileCT
        Me.gdvFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gdvFile.Name = "gdvFile"
        Me.gdvFile.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHyperLinkEdit1})
        Me.gdvFile.Size = New System.Drawing.Size(460, 138)
        Me.gdvFile.TabIndex = 0
        Me.gdvFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvFileCT})
        '
        'gdvFileCT
        '
        Me.gdvFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFile})
        Me.gdvFileCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvFileCT.GridControl = Me.gdvFile
        Me.gdvFileCT.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.gdvFileCT.Name = "gdvFileCT"
        Me.gdvFileCT.OptionsBehavior.Editable = False
        Me.gdvFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvFileCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvFileCT.RowHeight = 22
        '
        'colFile
        '
        Me.colFile.ColumnEdit = Me.RepositoryItemHyperLinkEdit1
        Me.colFile.FieldName = "File"
        Me.colFile.Name = "colFile"
        Me.colFile.Visible = True
        Me.colFile.VisibleIndex = 0
        Me.colFile.Width = 500
        '
        'RepositoryItemHyperLinkEdit1
        '
        Me.RepositoryItemHyperLinkEdit1.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit1.Name = "RepositoryItemHyperLinkEdit1"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(584, 427)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 8
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btLuuLai.Location = New System.Drawing.Point(415, 427)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(156, 41)
        Me.btLuuLai.TabIndex = 7
        Me.btLuuLai.Text = "Lưu và đóng"
        '
        'cboNguoitest
        '
        Me.cboNguoitest.EditValue = ""
        Me.cboNguoitest.Location = New System.Drawing.Point(129, 81)
        Me.cboNguoitest.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboNguoitest.Name = "cboNguoitest"
        Me.cboNguoitest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboNguoitest.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cboNguoitest.Size = New System.Drawing.Size(559, 22)
        Me.cboNguoitest.TabIndex = 4
        '
        'cboNguoiphoihop
        '
        Me.cboNguoiphoihop.EditValue = ""
        Me.cboNguoiphoihop.Location = New System.Drawing.Point(129, 117)
        Me.cboNguoiphoihop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboNguoiphoihop.Name = "cboNguoiphoihop"
        Me.cboNguoiphoihop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboNguoiphoihop.Size = New System.Drawing.Size(559, 22)
        Me.cboNguoiphoihop.TabIndex = 5
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl9.Location = New System.Drawing.Point(568, 16)
        Me.LabelControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(63, 17)
        Me.LabelControl9.TabIndex = 61
        Me.LabelControl9.Text = "( dự kiến )"
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Location = New System.Drawing.Point(81, 568)
        Me.LabelControl10.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(144, 17)
        Me.LabelControl10.TabIndex = 62
        Me.LabelControl10.Text = "Ngày nhận việc thực tế"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl11.Location = New System.Drawing.Point(410, 568)
        Me.LabelControl11.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(156, 17)
        Me.LabelControl11.TabIndex = 63
        Me.LabelControl11.Text = "Ngày hoàn thành thực tế"
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Location = New System.Drawing.Point(235, 566)
        Me.DateEdit1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit1.Size = New System.Drawing.Size(146, 22)
        Me.DateEdit1.TabIndex = 64
        '
        'DateEdit2
        '
        Me.DateEdit2.EditValue = Nothing
        Me.DateEdit2.Location = New System.Drawing.Point(580, 566)
        Me.DateEdit2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateEdit2.Name = "DateEdit2"
        Me.DateEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit2.Size = New System.Drawing.Size(146, 22)
        Me.DateEdit2.TabIndex = 65
        '
        'frmGiaoNhanViec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 478)
        Me.Controls.Add(Me.DateEdit2)
        Me.Controls.Add(Me.DateEdit1)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.cboNguoiphoihop)
        Me.Controls.Add(Me.cboNguoitest)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gListFile)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.txtNgayhoanthanh)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtThoihan)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.cboNguoinhanviec)
        Me.Controls.Add(Me.txtNgaynhanviec)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGiaoNhanViec"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Giao việc"
        CType(Me.txtNgaynhanviec.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaynhanviec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNguoinhanviec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtThoihan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgayhoanthanh.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgayhoanthanh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFile.ResumeLayout(False)
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNguoitest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboNguoiphoihop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgaynhanviec As DevExpress.XtraEditors.DateEdit
    Friend WithEvents cboNguoinhanviec As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtThoihan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgayhoanthanh As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gListFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboNguoitest As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents cboNguoiphoihop As DevExpress.XtraEditors.CheckedComboBoxEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents DateEdit2 As DevExpress.XtraEditors.DateEdit
End Class
