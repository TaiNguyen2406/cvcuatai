<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemBaotri
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
        Me.txtNgaythongbao = New DevExpress.XtraEditors.DateEdit()
        Me.txtNguoithongbao = New DevExpress.XtraEditors.TextEdit()
        Me.cboTrangthaixuly = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.txtNoidungbaotri = New DevExpress.XtraEditors.MemoEdit()
        Me.gListFile = New DevExpress.XtraEditors.GroupControl()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btnLuuthem = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.txtNgaythongbao.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNgaythongbao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNguoithongbao.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTrangthaixuly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoidungbaotri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFile.SuspendLayout()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(12, 17)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(100, 17)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Ngày thông báo"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(287, 16)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(104, 17)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Người thông báo"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(12, 49)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(97, 17)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Trạng thái xử lý"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(11, 407)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(98, 17)
        Me.LabelControl4.TabIndex = 3
        Me.LabelControl4.Text = "Nội dung bảo trì"
        '
        'txtNgaythongbao
        '
        Me.txtNgaythongbao.EditValue = Nothing
        Me.txtNgaythongbao.Location = New System.Drawing.Point(120, 12)
        Me.txtNgaythongbao.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNgaythongbao.Name = "txtNgaythongbao"
        Me.txtNgaythongbao.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtNgaythongbao.Properties.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaythongbao.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaythongbao.Properties.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.txtNgaythongbao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtNgaythongbao.Properties.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.txtNgaythongbao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtNgaythongbao.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNgaythongbao.Size = New System.Drawing.Size(146, 22)
        Me.txtNgaythongbao.TabIndex = 1
        '
        'txtNguoithongbao
        '
        Me.txtNguoithongbao.Location = New System.Drawing.Point(401, 12)
        Me.txtNguoithongbao.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNguoithongbao.Name = "txtNguoithongbao"
        Me.txtNguoithongbao.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtNguoithongbao.Properties.Appearance.Options.UseBackColor = True
        Me.txtNguoithongbao.Properties.ReadOnly = True
        Me.txtNguoithongbao.Size = New System.Drawing.Size(240, 22)
        Me.txtNguoithongbao.TabIndex = 5
        '
        'cboTrangthaixuly
        '
        Me.cboTrangthaixuly.EditValue = ""
        Me.cboTrangthaixuly.Location = New System.Drawing.Point(120, 47)
        Me.cboTrangthaixuly.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cboTrangthaixuly.Name = "cboTrangthaixuly"
        Me.cboTrangthaixuly.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.cboTrangthaixuly.Properties.Appearance.Options.UseBackColor = True
        Me.cboTrangthaixuly.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTrangthaixuly.Properties.Items.AddRange(New Object() {"Chờ nhận xử lý", "Đã nhận xử lý"})
        Me.cboTrangthaixuly.Properties.ReadOnly = True
        Me.cboTrangthaixuly.Size = New System.Drawing.Size(521, 22)
        Me.cboTrangthaixuly.TabIndex = 7
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btLuuLai.Location = New System.Drawing.Point(671, 590)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(151, 41)
        Me.btLuuLai.TabIndex = 3
        Me.btLuuLai.Text = "Lưu lại"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(835, 590)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 4
        Me.btDong.Text = "Đóng"
        '
        'txtNoidungbaotri
        '
        Me.txtNoidungbaotri.Location = New System.Drawing.Point(122, 256)
        Me.txtNoidungbaotri.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNoidungbaotri.Name = "txtNoidungbaotri"
        Me.txtNoidungbaotri.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoidungbaotri.Properties.Appearance.Options.UseFont = True
        Me.txtNoidungbaotri.Properties.MaxLength = 2000
        Me.txtNoidungbaotri.Size = New System.Drawing.Size(818, 326)
        Me.txtNoidungbaotri.TabIndex = 2
        '
        'gListFile
        '
        Me.gListFile.Controls.Add(Me.gdvFile)
        Me.gListFile.Location = New System.Drawing.Point(120, 84)
        Me.gListFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.gListFile.Name = "gListFile"
        Me.gListFile.Size = New System.Drawing.Size(724, 164)
        Me.gListFile.TabIndex = 27
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
        Me.gdvFile.Size = New System.Drawing.Size(720, 138)
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
        Me.btXoaFile.Location = New System.Drawing.Point(853, 218)
        Me.btXoaFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(87, 28)
        Me.btXoaFile.TabIndex = 30
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(853, 182)
        Me.btThemFile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(87, 28)
        Me.btThemFile.TabIndex = 29
        Me.btThemFile.Text = "Thêm file"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(12, 158)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(78, 17)
        Me.LabelControl5.TabIndex = 31
        Me.LabelControl5.Text = "File đính kèm"
        '
        'btnLuuthem
        '
        Me.btnLuuthem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLuuthem.Appearance.Options.UseFont = True
        Me.btnLuuthem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnLuuthem.Location = New System.Drawing.Point(525, 590)
        Me.btnLuuthem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnLuuthem.Name = "btnLuuthem"
        Me.btnLuuthem.Size = New System.Drawing.Size(133, 41)
        Me.btnLuuthem.TabIndex = 60
        Me.btnLuuthem.Text = "Thêm"
        '
        'frmThemBaotri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(946, 636)
        Me.Controls.Add(Me.btnLuuthem)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gListFile)
        Me.Controls.Add(Me.txtNoidungbaotri)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.cboTrangthaixuly)
        Me.Controls.Add(Me.txtNguoithongbao)
        Me.Controls.Add(Me.txtNgaythongbao)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemBaotri"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Thêm Bảo trì, nâng cấp"
        CType(Me.txtNgaythongbao.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNgaythongbao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNguoithongbao.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTrangthaixuly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoidungbaotri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFile.ResumeLayout(False)
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNgaythongbao As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtNguoithongbao As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboTrangthaixuly As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtNoidungbaotri As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents gListFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnLuuthem As DevExpress.XtraEditors.SimpleButton
End Class
