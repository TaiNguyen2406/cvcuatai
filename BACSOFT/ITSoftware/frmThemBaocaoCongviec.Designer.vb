<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmThemBaocaoCongviec
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmThemBaocaoCongviec))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNgaybatdau = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNgayketthuc = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDiadiem = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNoidung = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.gListFile = New DevExpress.XtraEditors.GroupControl()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.cboLoaicongviec = New DevExpress.XtraEditors.LookUpEdit()
        Me.cboDSNhanvien = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNgaybaocao = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNguoibaocao = New DevExpress.XtraEditors.TextEdit()
        Me.btnLuuthem = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txtNgaybatdau.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaybatdau.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgayketthuc.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgayketthuc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiadiem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoidung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFile.SuspendLayout()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLoaicongviec.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboDSNhanvien.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaybaocao.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaybaocao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNguoibaocao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(13, 50)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(83, 17)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Ngày bắt đầu"
        '
        'txtNgaybatdau
        '
        Me.txtNgaybatdau.EditValue = Nothing
        Me.txtNgaybatdau.Location = New System.Drawing.Point(136, 46)
        Me.txtNgaybatdau.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaybatdau.Name = "txtNgaybatdau"
        Me.txtNgaybatdau.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaybatdau.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaybatdau.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaybatdau.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaybatdau.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaybatdau.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgaybatdau.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgaybatdau.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaybatdau.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaybatdau.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(302, 50)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(87, 17)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "Ngày kết thúc"
        '
        'txtNgayketthuc
        '
        Me.txtNgayketthuc.EditValue = Nothing
        Me.txtNgayketthuc.Location = New System.Drawing.Point(397, 46)
        Me.txtNgayketthuc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgayketthuc.Name = "txtNgayketthuc"
        Me.txtNgayketthuc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgayketthuc.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgayketthuc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgayketthuc.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgayketthuc.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgayketthuc.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgayketthuc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgayketthuc.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgayketthuc.Size = New System.Drawing.Size(146, 22)
        Me.txtNgayketthuc.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(13, 85)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(114, 17)
        Me.LabelControl3.TabIndex = 8
        Me.LabelControl3.Text = "Địa điểm thực hiện"
        '
        'txtDiadiem
        '
        Me.txtDiadiem.Location = New System.Drawing.Point(136, 82)
        Me.txtDiadiem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDiadiem.Name = "txtDiadiem"
        Me.txtDiadiem.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtDiadiem.Properties.Appearance.Options.UseBackColor = True
        Me.txtDiadiem.Size = New System.Drawing.Size(519, 22)
        Me.txtDiadiem.TabIndex = 4
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(13, 122)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(123, 17)
        Me.LabelControl4.TabIndex = 16
        Me.LabelControl4.Text = "Nhân viên thực hiện"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(14, 158)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(87, 17)
        Me.LabelControl5.TabIndex = 18
        Me.LabelControl5.Text = "Loại công việc"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(14, 234)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(118, 17)
        Me.LabelControl6.TabIndex = 20
        Me.LabelControl6.Text = "Nội dung công việc"
        '
        'txtNoidung
        '
        Me.txtNoidung.Location = New System.Drawing.Point(136, 190)
        Me.txtNoidung.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoidung.Name = "txtNoidung"
        Me.txtNoidung.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoidung.Properties.Appearance.Options.UseFont = True
        Me.txtNoidung.Properties.MaxLength = 500
        Me.txtNoidung.Size = New System.Drawing.Size(520, 112)
        Me.txtNoidung.TabIndex = 7
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(14, 382)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(78, 17)
        Me.LabelControl7.TabIndex = 22
        Me.LabelControl7.Text = "File đính kèm"
        '
        'gListFile
        '
        Me.gListFile.Controls.Add(Me.gdvFile)
        Me.gListFile.Location = New System.Drawing.Point(136, 313)
        Me.gListFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gListFile.Name = "gListFile"
        Me.gListFile.Size = New System.Drawing.Size(429, 164)
        Me.gListFile.TabIndex = 23
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
        Me.gdvFile.Size = New System.Drawing.Size(425, 138)
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
        'btXoaFile
        '
        Me.btXoaFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(572, 446)
        Me.btXoaFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(87, 28)
        Me.btXoaFile.TabIndex = 25
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(572, 410)
        Me.btThemFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(87, 28)
        Me.btThemFile.TabIndex = 24
        Me.btThemFile.Text = "Thêm file"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(14, 513)
        Me.LabelControl8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(46, 17)
        Me.LabelControl8.TabIndex = 26
        Me.LabelControl8.Text = "Ghi chú"
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(136, 488)
        Me.txtGhichu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGhichu.Properties.Appearance.Options.UseFont = True
        Me.txtGhichu.Properties.MaxLength = 100
        Me.txtGhichu.Size = New System.Drawing.Size(524, 76)
        Me.txtGhichu.TabIndex = 8
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(555, 577)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 10
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(397, 577)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(146, 41)
        Me.btLuuLai.TabIndex = 9
        Me.btLuuLai.Text = "Lưu lại"
        '
        'cboLoaicongviec
        '
        Me.cboLoaicongviec.Location = New System.Drawing.Point(136, 154)
        Me.cboLoaicongviec.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboLoaicongviec.Name = "cboLoaicongviec"
        SerializableAppearanceObject1.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        SerializableAppearanceObject1.Options.UseBackColor = True
        SerializableAppearanceObject1.Options.UseImage = True
        Me.cboLoaicongviec.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.[Default], CType(resources.GetObject("cboLoaicongviec.Properties.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.cboLoaicongviec.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tencongviec", "Tên")})
        Me.cboLoaicongviec.Properties.DisplayMember = "Tencongviec"
        Me.cboLoaicongviec.Properties.DropDownItemHeight = 22
        Me.cboLoaicongviec.Properties.NullText = ""
        Me.cboLoaicongviec.Properties.ShowHeader = False
        Me.cboLoaicongviec.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cboLoaicongviec.Properties.ValueMember = "ID"
        Me.cboLoaicongviec.Size = New System.Drawing.Size(520, 22)
        Me.cboLoaicongviec.TabIndex = 6
        '
        'cboDSNhanvien
        '
        Me.cboDSNhanvien.Location = New System.Drawing.Point(136, 119)
        Me.cboDSNhanvien.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboDSNhanvien.Name = "cboDSNhanvien"
        Me.cboDSNhanvien.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboDSNhanvien.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", 50, "Tên")})
        Me.cboDSNhanvien.Properties.DisplayMember = "Ten"
        Me.cboDSNhanvien.Properties.DropDownItemHeight = 22
        Me.cboDSNhanvien.Properties.NullText = ""
        Me.cboDSNhanvien.Properties.ShowHeader = False
        Me.cboDSNhanvien.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cboDSNhanvien.Properties.ValueMember = "Id"
        Me.cboDSNhanvien.Size = New System.Drawing.Size(520, 22)
        Me.cboDSNhanvien.TabIndex = 5
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl9.Location = New System.Drawing.Point(14, 13)
        Me.LabelControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(85, 17)
        Me.LabelControl9.TabIndex = 32
        Me.LabelControl9.Text = "Ngày báo cáo"
        '
        'txtNgaybaocao
        '
        Me.txtNgaybaocao.EditValue = Nothing
        Me.txtNgaybaocao.Location = New System.Drawing.Point(136, 11)
        Me.txtNgaybaocao.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaybaocao.Name = "txtNgaybaocao"
        Me.txtNgaybaocao.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaybaocao.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaybaocao.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaybaocao.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaybaocao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaybaocao.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgaybaocao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgaybaocao.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaybaocao.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaybaocao.TabIndex = 1
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl10.Location = New System.Drawing.Point(302, 14)
        Me.LabelControl10.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(89, 17)
        Me.LabelControl10.TabIndex = 34
        Me.LabelControl10.Text = "Người báo cáo"
        '
        'txtNguoibaocao
        '
        Me.txtNguoibaocao.Location = New System.Drawing.Point(397, 11)
        Me.txtNguoibaocao.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNguoibaocao.Name = "txtNguoibaocao"
        Me.txtNguoibaocao.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNguoibaocao.Properties.Appearance.Options.UseBackColor = True
        Me.txtNguoibaocao.Properties.ReadOnly = True
        Me.txtNguoibaocao.Size = New System.Drawing.Size(259, 22)
        Me.txtNguoibaocao.TabIndex = 35
        '
        'btnLuuthem
        '
        Me.btnLuuthem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLuuthem.Appearance.Options.UseFont = True
        Me.btnLuuthem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnLuuthem.Location = New System.Drawing.Point(252, 577)
        Me.btnLuuthem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnLuuthem.Name = "btnLuuthem"
        Me.btnLuuthem.Size = New System.Drawing.Size(131, 41)
        Me.btnLuuthem.TabIndex = 36
        Me.btnLuuthem.Text = "Thêm"
        '
        'frmThemBaocaoCongviec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 628)
        Me.Controls.Add(Me.btnLuuthem)
        Me.Controls.Add(Me.txtNguoibaocao)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.txtNgaybaocao)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.cboDSNhanvien)
        Me.Controls.Add(Me.cboLoaicongviec)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gListFile)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.txtNoidung)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtDiadiem)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.txtNgayketthuc)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtNgaybatdau)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemBaocaoCongviec"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Báo cáo công việc"
        CType(Me.txtNgaybatdau.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaybatdau.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgayketthuc.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgayketthuc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiadiem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoidung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFile.ResumeLayout(False)
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLoaicongviec.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboDSNhanvien.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaybaocao.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaybaocao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNguoibaocao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgaybatdau As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgayketthuc As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDiadiem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNoidung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gListFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboLoaicongviec As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cboDSNhanvien As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgaybaocao As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNguoibaocao As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnLuuthem As DevExpress.XtraEditors.SimpleButton
End Class
