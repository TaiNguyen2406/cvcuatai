<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemPhanhe
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNgaytao = New DevExpress.XtraEditors.DateEdit()
        Me.txtNguoitao = New DevExpress.XtraEditors.TextEdit()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.gListFile = New DevExpress.XtraEditors.GroupControl()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.txtMota = New DevExpress.XtraEditors.MemoEdit()
        Me.txtTenphanhe = New DevExpress.XtraEditors.TextEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.chkLaBaotri = New DevExpress.XtraEditors.CheckEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.chkSuaphanhedaco = New DevExpress.XtraEditors.CheckEdit()
        Me.chkThemphanhemoi = New DevExpress.XtraEditors.CheckEdit()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.btnChon = New DevExpress.XtraEditors.SimpleButton()
        Me.txtYeucaubaotri = New DevExpress.XtraEditors.TextEdit()
        Me.btnLuuthem = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txtNgaytao.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaytao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNguoitao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFile.SuspendLayout()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMota.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenphanhe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLaBaotri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkSuaphanhedaco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkThemphanhemoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtYeucaubaotri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(282, 15)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 17)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Người tạo"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(14, 15)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(56, 17)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Ngày tạo"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(14, 48)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(77, 17)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Tên phân hệ"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(14, 231)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(78, 17)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "File đính kèm"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(14, 107)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(88, 17)
        Me.LabelControl5.TabIndex = 6
        Me.LabelControl5.Text = "Mô tả phân hệ"
        '
        'txtNgaytao
        '
        Me.txtNgaytao.EditValue = Nothing
        Me.txtNgaytao.Location = New System.Drawing.Point(113, 11)
        Me.txtNgaytao.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaytao.Name = "txtNgaytao"
        Me.txtNgaytao.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaytao.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.txtNgaytao.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaytao.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.txtNgaytao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaytao.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.txtNgaytao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgaytao.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaytao.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaytao.TabIndex = 1
        '
        'txtNguoitao
        '
        Me.txtNguoitao.Location = New System.Drawing.Point(350, 11)
        Me.txtNguoitao.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNguoitao.Name = "txtNguoitao"
        Me.txtNguoitao.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNguoitao.Properties.Appearance.Options.UseBackColor = True
        Me.txtNguoitao.Properties.ReadOnly = True
        Me.txtNguoitao.Size = New System.Drawing.Size(310, 22)
        Me.txtNguoitao.TabIndex = 10
        '
        'btXoaFile
        '
        Me.btXoaFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(573, 301)
        Me.btXoaFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(87, 28)
        Me.btXoaFile.TabIndex = 28
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(573, 265)
        Me.btThemFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(87, 28)
        Me.btThemFile.TabIndex = 27
        Me.btThemFile.Text = "Thêm file"
        '
        'gListFile
        '
        Me.gListFile.Controls.Add(Me.gdvFile)
        Me.gListFile.Location = New System.Drawing.Point(113, 167)
        Me.gListFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gListFile.Name = "gListFile"
        Me.gListFile.Size = New System.Drawing.Size(453, 164)
        Me.gListFile.TabIndex = 26
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
        Me.gdvFile.Size = New System.Drawing.Size(449, 138)
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
        'txtMota
        '
        Me.txtMota.Location = New System.Drawing.Point(113, 79)
        Me.txtMota.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMota.Name = "txtMota"
        Me.txtMota.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMota.Properties.Appearance.Options.UseFont = True
        Me.txtMota.Properties.MaxLength = 200
        Me.txtMota.Size = New System.Drawing.Size(547, 76)
        Me.txtMota.TabIndex = 3
        '
        'txtTenphanhe
        '
        Me.txtTenphanhe.Location = New System.Drawing.Point(113, 44)
        Me.txtTenphanhe.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTenphanhe.Name = "txtTenphanhe"
        Me.txtTenphanhe.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtTenphanhe.Properties.Appearance.Options.UseBackColor = True
        Me.txtTenphanhe.Properties.MaxLength = 50
        Me.txtTenphanhe.Size = New System.Drawing.Size(547, 22)
        Me.txtTenphanhe.TabIndex = 2
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(556, 514)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 6
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btLuuLai.Location = New System.Drawing.Point(405, 514)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(139, 41)
        Me.btLuuLai.TabIndex = 5
        Me.btLuuLai.Text = "Lưu lại"
        '
        'chkLaBaotri
        '
        Me.chkLaBaotri.Location = New System.Drawing.Point(111, 350)
        Me.chkLaBaotri.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkLaBaotri.Name = "chkLaBaotri"
        Me.chkLaBaotri.Properties.Caption = "Bảo trì / nâng cấp"
        Me.chkLaBaotri.Size = New System.Drawing.Size(148, 22)
        Me.chkLaBaotri.TabIndex = 33
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.chkSuaphanhedaco)
        Me.GroupControl1.Controls.Add(Me.chkThemphanhemoi)
        Me.GroupControl1.Location = New System.Drawing.Point(364, 343)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(296, 70)
        Me.GroupControl1.TabIndex = 34
        Me.GroupControl1.Text = "Danh sách file"
        '
        'chkSuaphanhedaco
        '
        Me.chkSuaphanhedaco.Enabled = False
        Me.chkSuaphanhedaco.Location = New System.Drawing.Point(14, 37)
        Me.chkSuaphanhedaco.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkSuaphanhedaco.Name = "chkSuaphanhedaco"
        Me.chkSuaphanhedaco.Properties.Caption = "Sửa phân hệ đã có"
        Me.chkSuaphanhedaco.Size = New System.Drawing.Size(148, 22)
        Me.chkSuaphanhedaco.TabIndex = 35
        '
        'chkThemphanhemoi
        '
        Me.chkThemphanhemoi.Enabled = False
        Me.chkThemphanhemoi.Location = New System.Drawing.Point(14, 6)
        Me.chkThemphanhemoi.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkThemphanhemoi.Name = "chkThemphanhemoi"
        Me.chkThemphanhemoi.Properties.Caption = "Thêm phân hệ mới"
        Me.chkThemphanhemoi.Size = New System.Drawing.Size(148, 22)
        Me.chkThemphanhemoi.TabIndex = 34
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(113, 428)
        Me.txtGhichu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGhichu.Properties.Appearance.Options.UseFont = True
        Me.txtGhichu.Properties.MaxLength = 100
        Me.txtGhichu.Size = New System.Drawing.Size(547, 76)
        Me.txtGhichu.TabIndex = 4
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(14, 457)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(46, 17)
        Me.LabelControl6.TabIndex = 36
        Me.LabelControl6.Text = "Ghi chú"
        '
        'btnChon
        '
        Me.btnChon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChon.Enabled = False
        Me.btnChon.Location = New System.Drawing.Point(259, 346)
        Me.btnChon.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnChon.Name = "btnChon"
        Me.btnChon.Size = New System.Drawing.Size(87, 28)
        Me.btnChon.TabIndex = 57
        Me.btnChon.Text = "Chọn..."
        '
        'txtYeucaubaotri
        '
        Me.txtYeucaubaotri.Location = New System.Drawing.Point(259, 569)
        Me.txtYeucaubaotri.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtYeucaubaotri.Name = "txtYeucaubaotri"
        Me.txtYeucaubaotri.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtYeucaubaotri.Properties.Appearance.Options.UseBackColor = True
        Me.txtYeucaubaotri.Size = New System.Drawing.Size(139, 22)
        Me.txtYeucaubaotri.TabIndex = 58
        Me.txtYeucaubaotri.Visible = False
        '
        'btnLuuthem
        '
        Me.btnLuuthem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLuuthem.Appearance.Options.UseFont = True
        Me.btnLuuthem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnLuuthem.Location = New System.Drawing.Point(259, 513)
        Me.btnLuuthem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnLuuthem.Name = "btnLuuthem"
        Me.btnLuuthem.Size = New System.Drawing.Size(131, 41)
        Me.btnLuuthem.TabIndex = 59
        Me.btnLuuthem.Text = "Thêm"
        '
        'frmThemPhanhe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 561)
        Me.Controls.Add(Me.btnLuuthem)
        Me.Controls.Add(Me.txtYeucaubaotri)
        Me.Controls.Add(Me.btnChon)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.chkLaBaotri)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.txtTenphanhe)
        Me.Controls.Add(Me.txtMota)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gListFile)
        Me.Controls.Add(Me.txtNguoitao)
        Me.Controls.Add(Me.txtNgaytao)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemPhanhe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Thêm phân hệ"
        CType(Me.txtNgaytao.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaytao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNguoitao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFile.ResumeLayout(False)
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMota.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenphanhe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLaBaotri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.chkSuaphanhedaco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkThemphanhemoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtYeucaubaotri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgaytao As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtNguoitao As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gListFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents txtMota As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtTenphanhe As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkLaBaotri As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents chkSuaphanhedaco As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkThemphanhemoi As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnChon As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtYeucaubaotri As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnLuuthem As DevExpress.XtraEditors.SimpleButton
End Class
