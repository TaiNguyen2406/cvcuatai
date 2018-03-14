<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmThamgiaduan
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
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDS = New DevExpress.XtraGrid.GridControl()
        Me.grdViewDS = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemLookUpEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemLookUpEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.btnDel = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSoyc = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtTenpm = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtModel = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtCode = New DevExpress.XtraEditors.TextEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grdDS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewDS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemLookUpEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemLookUpEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSoyc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenpm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtModel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(948, 399)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 3
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btLuuLai.Location = New System.Drawing.Point(805, 399)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(131, 41)
        Me.btLuuLai.TabIndex = 2
        Me.btLuuLai.Text = "Lưu lại"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.grdDS)
        Me.GroupControl1.Location = New System.Drawing.Point(8, 53)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1044, 329)
        Me.GroupControl1.TabIndex = 65
        Me.GroupControl1.Text = "Danh sách nhân viên dự án"
        '
        'grdDS
        '
        Me.grdDS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDS.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDS.Location = New System.Drawing.Point(2, 25)
        Me.grdDS.MainView = Me.grdViewDS
        Me.grdDS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDS.Name = "grdDS"
        Me.grdDS.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemLookUpEdit2, Me.RepositoryItemLookUpEdit3, Me.RepositoryItemDateEdit1, Me.RepositoryItemMemoEdit1})
        Me.grdDS.Size = New System.Drawing.Size(1040, 302)
        Me.grdDS.TabIndex = 1
        Me.grdDS.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewDS})
        '
        'grdViewDS
        '
        Me.grdViewDS.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDS.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdViewDS.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDS.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdViewDS.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDS.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdViewDS.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn7, Me.GridColumn5, Me.GridColumn8})
        Me.grdViewDS.CustomizationFormBounds = New System.Drawing.Rectangle(909, 273, 216, 183)
        Me.grdViewDS.GridControl = Me.grdDS
        Me.grdViewDS.Name = "grdViewDS"
        Me.grdViewDS.OptionsView.RowAutoHeight = True
        Me.grdViewDS.OptionsView.ShowGroupPanel = False
        '
        'GridColumn10
        '
        Me.GridColumn10.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn10.AppearanceCell.Options.UseFont = True
        Me.GridColumn10.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn10.AppearanceHeader.Options.UseFont = True
        Me.GridColumn10.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn10.Caption = "Vai trò dự án"
        Me.GridColumn10.ColumnEdit = Me.RepositoryItemLookUpEdit2
        Me.GridColumn10.FieldName = "IdRole"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        Me.GridColumn10.Width = 207
        '
        'RepositoryItemLookUpEdit2
        '
        Me.RepositoryItemLookUpEdit2.AutoHeight = False
        Me.RepositoryItemLookUpEdit2.AutoSearchColumnIndex = 1
        Me.RepositoryItemLookUpEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemLookUpEdit2.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tenrole", "Role")})
        Me.RepositoryItemLookUpEdit2.DisplayMember = "Tenrole"
        Me.RepositoryItemLookUpEdit2.Name = "RepositoryItemLookUpEdit2"
        Me.RepositoryItemLookUpEdit2.NullText = ""
        Me.RepositoryItemLookUpEdit2.ShowHeader = False
        Me.RepositoryItemLookUpEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.RepositoryItemLookUpEdit2.ValueMember = "Id"
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn7.AppearanceHeader.Options.UseFont = True
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Họ và tên"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemLookUpEdit3
        Me.GridColumn7.FieldName = "IdNhansu"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsFilter.AllowFilter = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 230
        '
        'RepositoryItemLookUpEdit3
        '
        Me.RepositoryItemLookUpEdit3.AutoHeight = False
        Me.RepositoryItemLookUpEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemLookUpEdit3.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 40, "Id"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", 120, "Ten")})
        Me.RepositoryItemLookUpEdit3.DisplayMember = "Ten"
        Me.RepositoryItemLookUpEdit3.Name = "RepositoryItemLookUpEdit3"
        Me.RepositoryItemLookUpEdit3.NullText = ""
        Me.RepositoryItemLookUpEdit3.ShowHeader = False
        Me.RepositoryItemLookUpEdit3.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.RepositoryItemLookUpEdit3.ValueMember = "Id"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Ngày vào dự án"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn5.FieldName = "TuNgay"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 154
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn8.AppearanceHeader.Options.UseFont = True
        Me.GridColumn8.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn8.Caption = "Ghi chú"
        Me.GridColumn8.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn8.FieldName = "Ghichu"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        Me.GridColumn8.Width = 428
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.MaxLength = 100
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'btnDel
        '
        Me.btnDel.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDel.Appearance.Options.UseFont = True
        Me.btnDel.Image = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btnDel.Location = New System.Drawing.Point(10, 396)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(131, 41)
        Me.btnDel.TabIndex = 35
        Me.btnDel.Text = "Xoá dòng"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(9, 20)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(37, 17)
        Me.LabelControl1.TabIndex = 66
        Me.LabelControl1.Text = "Số YC"
        '
        'txtSoyc
        '
        Me.txtSoyc.Location = New System.Drawing.Point(52, 16)
        Me.txtSoyc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSoyc.Name = "txtSoyc"
        Me.txtSoyc.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtSoyc.Properties.Appearance.Options.UseBackColor = True
        Me.txtSoyc.Properties.ReadOnly = True
        Me.txtSoyc.Size = New System.Drawing.Size(152, 22)
        Me.txtSoyc.TabIndex = 68
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(220, 19)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(45, 17)
        Me.LabelControl4.TabIndex = 70
        Me.LabelControl4.Text = "Tên PM"
        '
        'txtTenpm
        '
        Me.txtTenpm.Location = New System.Drawing.Point(276, 16)
        Me.txtTenpm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTenpm.Name = "txtTenpm"
        Me.txtTenpm.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtTenpm.Properties.Appearance.Options.UseBackColor = True
        Me.txtTenpm.Properties.ReadOnly = True
        Me.txtTenpm.Size = New System.Drawing.Size(422, 22)
        Me.txtTenpm.TabIndex = 71
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(711, 18)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(35, 17)
        Me.LabelControl2.TabIndex = 72
        Me.LabelControl2.Text = "Model"
        '
        'txtModel
        '
        Me.txtModel.Location = New System.Drawing.Point(758, 15)
        Me.txtModel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtModel.Properties.Appearance.Options.UseBackColor = True
        Me.txtModel.Properties.ReadOnly = True
        Me.txtModel.Size = New System.Drawing.Size(131, 22)
        Me.txtModel.TabIndex = 73
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(905, 18)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(32, 17)
        Me.LabelControl3.TabIndex = 74
        Me.LabelControl3.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(943, 16)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtCode.Properties.Appearance.Options.UseBackColor = True
        Me.txtCode.Properties.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(109, 22)
        Me.txtCode.TabIndex = 75
        '
        'frmThamgiaduan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1060, 451)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.txtModel)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.txtTenpm)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtSoyc)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThamgiaduan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cập nhật NVDA"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grdDS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewDS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemLookUpEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemLookUpEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSoyc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenpm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtModel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSoyc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTenpm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdDS As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewDS As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemLookUpEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemLookUpEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents btnDel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtModel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtCode As DevExpress.XtraEditors.TextEdit
End Class
