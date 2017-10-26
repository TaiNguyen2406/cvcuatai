<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateHeThongTkThue
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.cmbTaiKhoanCha = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtTaiKhoan = New DevExpress.XtraEditors.TextEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuVaDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuVaThem = New DevExpress.XtraEditors.SimpleButton()
        Me.txtTenGoi = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.cmbTaiKhoanCha.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaiKhoan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenGoi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.cmbTaiKhoanCha)
        Me.PanelControl1.Controls.Add(Me.txtTaiKhoan)
        Me.PanelControl1.Controls.Add(Me.btDong)
        Me.PanelControl1.Controls.Add(Me.btLuuVaDong)
        Me.PanelControl1.Controls.Add(Me.btLuuVaThem)
        Me.PanelControl1.Controls.Add(Me.txtTenGoi)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(576, 164)
        Me.PanelControl1.TabIndex = 0
        '
        'cmbTaiKhoanCha
        '
        Me.cmbTaiKhoanCha.Location = New System.Drawing.Point(371, 25)
        Me.cmbTaiKhoanCha.Name = "cmbTaiKhoanCha"
        Me.cmbTaiKhoanCha.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.cmbTaiKhoanCha.Properties.Appearance.ForeColor = System.Drawing.Color.Maroon
        Me.cmbTaiKhoanCha.Properties.Appearance.Options.UseFont = True
        Me.cmbTaiKhoanCha.Properties.Appearance.Options.UseForeColor = True
        Me.cmbTaiKhoanCha.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
        Me.cmbTaiKhoanCha.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cmbTaiKhoanCha.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TaiKhoan", "Name1"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenGoi", "Name2")})
        Me.cmbTaiKhoanCha.Properties.DisplayMember = "TaiKhoan"
        Me.cmbTaiKhoanCha.Properties.DropDownItemHeight = 30
        Me.cmbTaiKhoanCha.Properties.DropDownRows = 12
        Me.cmbTaiKhoanCha.Properties.NullText = "[Chọn nếu có]"
        Me.cmbTaiKhoanCha.Properties.PopupWidth = 350
        Me.cmbTaiKhoanCha.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete
        Me.cmbTaiKhoanCha.Properties.ShowHeader = False
        Me.cmbTaiKhoanCha.Properties.ShowLines = False
        Me.cmbTaiKhoanCha.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbTaiKhoanCha.Properties.ValueMember = "TaiKhoan"
        Me.cmbTaiKhoanCha.Size = New System.Drawing.Size(180, 22)
        Me.cmbTaiKhoanCha.TabIndex = 1
        '
        'txtTaiKhoan
        '
        Me.txtTaiKhoan.EditValue = ""
        Me.txtTaiKhoan.Location = New System.Drawing.Point(102, 25)
        Me.txtTaiKhoan.Name = "txtTaiKhoan"
        Me.txtTaiKhoan.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtTaiKhoan.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.txtTaiKhoan.Properties.Appearance.Options.UseFont = True
        Me.txtTaiKhoan.Properties.Appearance.Options.UseForeColor = True
        Me.txtTaiKhoan.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTaiKhoan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtTaiKhoan.Size = New System.Drawing.Size(135, 22)
        Me.txtTaiKhoan.TabIndex = 0
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(469, 109)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(82, 38)
        Me.btDong.TabIndex = 5
        Me.btDong.Text = "Đóng"
        '
        'btLuuVaDong
        '
        Me.btLuuVaDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaDong.Appearance.Options.UseFont = True
        Me.btLuuVaDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btLuuVaDong.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuVaDong.Location = New System.Drawing.Point(336, 109)
        Me.btLuuVaDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuVaDong.Name = "btLuuVaDong"
        Me.btLuuVaDong.Size = New System.Drawing.Size(126, 38)
        Me.btLuuVaDong.TabIndex = 3
        Me.btLuuVaDong.Text = "Lưu và đóng"
        '
        'btLuuVaThem
        '
        Me.btLuuVaThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaThem.Appearance.Options.UseFont = True
        Me.btLuuVaThem.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btLuuVaThem.Image = Global.BACSOFT.My.Resources.Resources.Save_AddNew_18
        Me.btLuuVaThem.Location = New System.Drawing.Point(198, 109)
        Me.btLuuVaThem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuVaThem.Name = "btLuuVaThem"
        Me.btLuuVaThem.Size = New System.Drawing.Size(131, 38)
        Me.btLuuVaThem.TabIndex = 4
        Me.btLuuVaThem.Text = "Lưu và thêm"
        '
        'txtTenGoi
        '
        Me.txtTenGoi.Location = New System.Drawing.Point(102, 67)
        Me.txtTenGoi.Name = "txtTenGoi"
        Me.txtTenGoi.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtTenGoi.Properties.Appearance.Options.UseFont = True
        Me.txtTenGoi.Size = New System.Drawing.Size(449, 22)
        Me.txtTenGoi.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(22, 70)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(45, 17)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Tên gọi"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(261, 28)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(85, 17)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Tài khoản cha"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(22, 28)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(59, 17)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tài khoản"
        '
        'frmUpdateHeThongTkThue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 164)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateHeThongTkThue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hệ thống tài khoản thuế"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.cmbTaiKhoanCha.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaiKhoan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenGoi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTenGoi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuVaDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuVaThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cmbTaiKhoanCha As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtTaiKhoan As DevExpress.XtraEditors.TextEdit
End Class
