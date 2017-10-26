<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNHDHangNgay
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cbNhom = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbMoTa = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tbDiemChuan = New DevExpress.XtraEditors.SpinEdit()
        Me.btLuuVaThem = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.cbTen = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbMoTa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDiemChuan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 41)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(18, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Tên"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(13, 15)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(58, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Thuộc nhóm"
        '
        'cbNhom
        '
        Me.cbNhom.Location = New System.Drawing.Point(79, 12)
        Me.cbNhom.Name = "cbNhom"
        Me.cbNhom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbNhom.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "TenNhom")})
        Me.cbNhom.Properties.DisplayMember = "Ten"
        Me.cbNhom.Properties.DropDownItemHeight = 22
        Me.cbNhom.Properties.NullText = "[Chọn nhóm]"
        Me.cbNhom.Properties.ShowHeader = False
        Me.cbNhom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbNhom.Properties.ValueMember = "ID"
        Me.cbNhom.Size = New System.Drawing.Size(383, 20)
        Me.cbNhom.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 110)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Mô tả"
        '
        'tbMoTa
        '
        Me.tbMoTa.Location = New System.Drawing.Point(79, 90)
        Me.tbMoTa.Name = "tbMoTa"
        Me.tbMoTa.Size = New System.Drawing.Size(383, 63)
        Me.tbMoTa.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(13, 67)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Điểm chuẩn"
        '
        'tbDiemChuan
        '
        Me.tbDiemChuan.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.tbDiemChuan.Location = New System.Drawing.Point(79, 64)
        Me.tbDiemChuan.Name = "tbDiemChuan"
        Me.tbDiemChuan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbDiemChuan.Size = New System.Drawing.Size(100, 20)
        Me.tbDiemChuan.TabIndex = 2
        '
        'btLuuVaThem
        '
        Me.btLuuVaThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuVaThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaThem.Appearance.Options.UseFont = True
        Me.btLuuVaThem.Image = Global.BACSOFT.My.Resources.Resources.Save_AddNew_18
        Me.btLuuVaThem.Location = New System.Drawing.Point(155, 170)
        Me.btLuuVaThem.Name = "btLuuVaThem"
        Me.btLuuVaThem.Size = New System.Drawing.Size(108, 31)
        Me.btLuuVaThem.TabIndex = 4
        Me.btLuuVaThem.Text = "Lưu và thêm"
        '
        'btLuu
        '
        Me.btLuu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuu.Appearance.Options.UseFont = True
        Me.btLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuu.Location = New System.Drawing.Point(269, 170)
        Me.btLuu.Name = "btLuu"
        Me.btLuu.Size = New System.Drawing.Size(108, 31)
        Me.btLuu.TabIndex = 5
        Me.btLuu.Text = "Lưu và đóng"
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(383, 170)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(79, 31)
        Me.btDong.TabIndex = 6
        Me.btDong.Text = "Đóng"
        '
        'cbTen
        '
        Me.cbTen.Location = New System.Drawing.Point(79, 38)
        Me.cbTen.Name = "cbTen"
        Me.cbTen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbTen.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Ten")})
        Me.cbTen.Properties.DisplayMember = "Ten"
        Me.cbTen.Properties.DropDownItemHeight = 22
        Me.cbTen.Properties.NullText = "[Chọn tên]"
        Me.cbTen.Properties.ShowHeader = False
        Me.cbTen.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbTen.Properties.ValueMember = "ID"
        Me.cbTen.Size = New System.Drawing.Size(383, 20)
        Me.cbTen.TabIndex = 1
        '
        'frmCNHDHangNgay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 213)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.btLuuVaThem)
        Me.Controls.Add(Me.tbDiemChuan)
        Me.Controls.Add(Me.tbMoTa)
        Me.Controls.Add(Me.cbTen)
        Me.Controls.Add(Me.cbNhom)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmCNHDHangNgay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật nội dung báo cáo hàng ngày"
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbMoTa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDiemChuan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbNhom As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbMoTa As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbDiemChuan As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents btLuuVaThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cbTen As DevExpress.XtraEditors.LookUpEdit
End Class
