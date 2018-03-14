<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBaogiaVatTu
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
        Me.txtNgaynhap = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtMavattu = New DevExpress.XtraEditors.TextEdit()
        Me.txtGia = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cboTiente = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNguoinhap = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSoluong = New DevExpress.XtraEditors.TextEdit()
        Me.gListFile = New DevExpress.XtraEditors.GroupControl()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtNgaynhap.Properties.VistaTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtNgaynhap.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtMavattu.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtGia.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cboTiente.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtGhichu.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtNguoinhap.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.txtSoluong.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.gListFile,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gListFile.SuspendLayout
        CType(Me.gdvFile,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.gdvFileCT,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemHyperLinkEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(14, 18)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(67, 17)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "Ngày nhập"
        '
        'txtNgaynhap
        '
        Me.txtNgaynhap.EditValue = Nothing
        Me.txtNgaynhap.Location = New System.Drawing.Point(119, 14)
        Me.txtNgaynhap.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaynhap.Name = "txtNgaynhap"
        Me.txtNgaynhap.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNgaynhap.Properties.Appearance.Options.UseBackColor = true
        Me.txtNgaynhap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaynhap.Properties.ReadOnly = true
        Me.txtNgaynhap.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaynhap.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaynhap.TabIndex = 9
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(301, 17)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(71, 17)
        Me.LabelControl2.TabIndex = 10
        Me.LabelControl2.Text = "Người nhập"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(14, 57)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(59, 17)
        Me.LabelControl3.TabIndex = 11
        Me.LabelControl3.Text = "Mã vật tư"
        '
        'txtMavattu
        '
        Me.txtMavattu.Location = New System.Drawing.Point(119, 53)
        Me.txtMavattu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtMavattu.Name = "txtMavattu"
        Me.txtMavattu.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtMavattu.Properties.Appearance.Options.UseBackColor = true
        Me.txtMavattu.Properties.ReadOnly = true
        Me.txtMavattu.Size = New System.Drawing.Size(146, 22)
        Me.txtMavattu.TabIndex = 16
        '
        'txtGia
        '
        Me.txtGia.Location = New System.Drawing.Point(406, 53)
        Me.txtGia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGia.Name = "txtGia"
        Me.txtGia.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtGia.Properties.Appearance.Options.UseBackColor = true
        Me.txtGia.Properties.DisplayFormat.FormatString = "N2"
        Me.txtGia.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtGia.Properties.EditFormat.FormatString = "N2"
        Me.txtGia.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.txtGia.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtGia.Size = New System.Drawing.Size(127, 22)
        Me.txtGia.TabIndex = 1
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(300, 57)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(98, 17)
        Me.LabelControl4.TabIndex = 18
        Me.LabelControl4.Text = "Đơn giá / tiền tệ"
        '
        'cboTiente
        '
        Me.cboTiente.Location = New System.Drawing.Point(544, 53)
        Me.cboTiente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboTiente.Name = "cboTiente"
        Me.cboTiente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTiente.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", 50, "Tên")})
        Me.cboTiente.Properties.DisplayMember = "Ten"
        Me.cboTiente.Properties.DropDownItemHeight = 22
        Me.cboTiente.Properties.NullText = ""
        Me.cboTiente.Properties.ShowHeader = false
        Me.cboTiente.Properties.ValueMember = "ID"
        Me.cboTiente.Size = New System.Drawing.Size(66, 22)
        Me.cboTiente.TabIndex = 2
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(117, 433)
        Me.txtGhichu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtGhichu.Properties.Appearance.Options.UseFont = true
        Me.txtGhichu.Properties.MaxLength = 100
        Me.txtGhichu.Size = New System.Drawing.Size(660, 70)
        Me.txtGhichu.TabIndex = 33
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(12, 452)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(46, 17)
        Me.LabelControl5.TabIndex = 34
        Me.LabelControl5.Text = "Ghi chú"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = true
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(676, 264)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 4
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = true
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(541, 264)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(122, 41)
        Me.btLuuLai.TabIndex = 3
        Me.btLuuLai.Text = "Lưu lại"
        '
        'txtNguoinhap
        '
        Me.txtNguoinhap.Location = New System.Drawing.Point(407, 14)
        Me.txtNguoinhap.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNguoinhap.Name = "txtNguoinhap"
        Me.txtNguoinhap.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNguoinhap.Properties.Appearance.Options.UseBackColor = true
        Me.txtNguoinhap.Properties.ReadOnly = true
        Me.txtNguoinhap.Size = New System.Drawing.Size(372, 22)
        Me.txtNguoinhap.TabIndex = 37
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(642, 57)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(55, 17)
        Me.LabelControl6.TabIndex = 38
        Me.LabelControl6.Text = "Số lượng"
        '
        'txtSoluong
        '
        Me.txtSoluong.Location = New System.Drawing.Point(698, 53)
        Me.txtSoluong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSoluong.Name = "txtSoluong"
        Me.txtSoluong.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtSoluong.Properties.Appearance.Options.UseBackColor = true
        Me.txtSoluong.Properties.ReadOnly = true
        Me.txtSoluong.Size = New System.Drawing.Size(82, 22)
        Me.txtSoluong.TabIndex = 39
        '
        'gListFile
        '
        Me.gListFile.Controls.Add(Me.gdvFile)
        Me.gListFile.Location = New System.Drawing.Point(119, 92)
        Me.gListFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gListFile.Name = "gListFile"
        Me.gListFile.Size = New System.Drawing.Size(568, 164)
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
        Me.gdvFile.Size = New System.Drawing.Size(564, 138)
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
        Me.gdvFileCT.OptionsBehavior.Editable = false
        Me.gdvFileCT.OptionsBehavior.ReadOnly = true
        Me.gdvFileCT.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.gdvFileCT.OptionsView.ShowGroupPanel = false
        Me.gdvFileCT.RowHeight = 22
        '
        'colFile
        '
        Me.colFile.ColumnEdit = Me.RepositoryItemHyperLinkEdit1
        Me.colFile.FieldName = "File"
        Me.colFile.Name = "colFile"
        Me.colFile.Visible = true
        Me.colFile.VisibleIndex = 0
        Me.colFile.Width = 500
        '
        'RepositoryItemHyperLinkEdit1
        '
        Me.RepositoryItemHyperLinkEdit1.AutoHeight = false
        Me.RepositoryItemHyperLinkEdit1.Name = "RepositoryItemHyperLinkEdit1"
        '
        'btXoaFile
        '
        Me.btXoaFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(693, 226)
        Me.btXoaFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(87, 28)
        Me.btXoaFile.TabIndex = 48
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(693, 190)
        Me.btThemFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(87, 28)
        Me.btThemFile.TabIndex = 47
        Me.btThemFile.Text = "Thêm file"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(14, 166)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(66, 17)
        Me.LabelControl7.TabIndex = 49
        Me.LabelControl7.Text = "File báo giá"
        '
        'frmBaogiaVatTu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 313)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gListFile)
        Me.Controls.Add(Me.txtSoluong)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.txtNguoinhap)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.cboTiente)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtGia)
        Me.Controls.Add(Me.txtMavattu)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtNgaynhap)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmBaogiaVatTu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Báo giá vật tư"
        CType(Me.txtNgaynhap.Properties.VistaTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNgaynhap.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtMavattu.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtGia.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cboTiente.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtGhichu.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtNguoinhap.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.txtSoluong.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.gListFile,System.ComponentModel.ISupportInitialize).EndInit
        Me.gListFile.ResumeLayout(false)
        CType(Me.gdvFile,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.gdvFileCT,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemHyperLinkEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgaynhap As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtMavattu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtGia As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboTiente As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNguoinhap As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSoluong As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gListFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
End Class
