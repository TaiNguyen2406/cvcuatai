<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNQuaTrinhBaoGia
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
        Me.tbGia = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cbTienTe = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cbTGCungUng = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tbGhiChu = New DevExpress.XtraEditors.MemoEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbGia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTienTe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTGCungUng.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbGhiChu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Giá"
        '
        'tbGia
        '
        Me.tbGia.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.tbGia.Location = New System.Drawing.Point(58, 10)
        Me.tbGia.Name = "tbGia"
        Me.tbGia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbGia.Properties.DisplayFormat.FormatString = "N2"
        Me.tbGia.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbGia.Properties.EditFormat.FormatString = "N2"
        Me.tbGia.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbGia.Size = New System.Drawing.Size(126, 20)
        Me.tbGia.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(190, 13)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Tiền tệ"
        '
        'cbTienTe
        '
        Me.cbTienTe.Location = New System.Drawing.Point(229, 10)
        Me.cbTienTe.Name = "cbTienTe"
        Me.cbTienTe.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cbTienTe.Properties.Appearance.Options.UseFont = True
        Me.cbTienTe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbTienTe.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.cbTienTe.Properties.DisplayMember = "Ten"
        Me.cbTienTe.Properties.DropDownItemHeight = 22
        Me.cbTienTe.Properties.NullText = ""
        Me.cbTienTe.Properties.ShowHeader = False
        Me.cbTienTe.Properties.ValueMember = "ID"
        Me.cbTienTe.Size = New System.Drawing.Size(70, 20)
        Me.cbTienTe.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(310, 13)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(91, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Thời gian cung ứng"
        '
        'cbTGCungUng
        '
        Me.cbTGCungUng.Location = New System.Drawing.Point(407, 10)
        Me.cbTGCungUng.Name = "cbTGCungUng"
        Me.cbTGCungUng.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cbTGCungUng.Properties.Appearance.Options.UseFont = True
        Me.cbTGCungUng.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbTGCungUng.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default], DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name2")})
        Me.cbTGCungUng.Properties.DisplayMember = "NoiDung"
        Me.cbTGCungUng.Properties.DropDownItemHeight = 22
        Me.cbTGCungUng.Properties.NullText = ""
        Me.cbTGCungUng.Properties.ShowHeader = False
        Me.cbTGCungUng.Properties.ValueMember = "Ma"
        Me.cbTGCungUng.Size = New System.Drawing.Size(157, 20)
        Me.cbTGCungUng.TabIndex = 3
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(12, 45)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Ghi chú"
        '
        'tbGhiChu
        '
        Me.tbGhiChu.Location = New System.Drawing.Point(58, 36)
        Me.tbGhiChu.Name = "tbGhiChu"
        Me.tbGhiChu.Size = New System.Drawing.Size(506, 96)
        Me.tbGhiChu.TabIndex = 4
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(489, 145)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 23)
        Me.btDong.TabIndex = 5
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(407, 145)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(75, 23)
        Me.btLuuLai.TabIndex = 5
        Me.btLuuLai.Text = "Lưu lại"
        '
        'frmCNQuaTrinhBaoGia
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 180)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.tbGhiChu)
        Me.Controls.Add(Me.cbTGCungUng)
        Me.Controls.Add(Me.cbTienTe)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbGia)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmCNQuaTrinhBaoGia"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quá trình báo giá"
        CType(Me.tbGia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTienTe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTGCungUng.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbGhiChu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbGia As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbTienTe As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbTGCungUng As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbGhiChu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
End Class
