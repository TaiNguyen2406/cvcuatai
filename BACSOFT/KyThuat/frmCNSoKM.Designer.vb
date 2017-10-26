<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNSoKM
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
        Me.btLuuVaThem = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.deNgayApDung = New DevExpress.XtraEditors.DateEdit()
        Me.seSoKm = New DevExpress.XtraEditors.SpinEdit()
        Me.lueKhachHang = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.deNgayApDung.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayApDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seSoKm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueKhachHang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btLuuVaThem
        '
        Me.btLuuVaThem.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btLuuVaThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaThem.Appearance.Options.UseFont = True
        Me.btLuuVaThem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btLuuVaThem.Location = New System.Drawing.Point(57, 103)
        Me.btLuuVaThem.Name = "btLuuVaThem"
        Me.btLuuVaThem.Size = New System.Drawing.Size(87, 27)
        Me.btLuuVaThem.TabIndex = 11
        Me.btLuuVaThem.Text = "Thêm mới"
        '
        'btnLuu
        '
        Me.btnLuu.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLuu.Appearance.Options.UseFont = True
        Me.btnLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btnLuu.Location = New System.Drawing.Point(150, 103)
        Me.btnLuu.Name = "btnLuu"
        Me.btnLuu.Size = New System.Drawing.Size(87, 27)
        Me.btnLuu.TabIndex = 14
        Me.btnLuu.Text = "Lưu lại"
        '
        'LabelControl3
        '
        Me.LabelControl3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelControl3.Location = New System.Drawing.Point(12, 64)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(67, 13)
        Me.LabelControl3.TabIndex = 20
        Me.LabelControl3.Text = "Ngày áp dụng"
        '
        'LabelControl2
        '
        Me.LabelControl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelControl2.Location = New System.Drawing.Point(12, 38)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl2.TabIndex = 19
        Me.LabelControl2.Text = "Số Km"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl1.TabIndex = 18
        Me.LabelControl1.Text = "Khách hàng"
        '
        'deNgayApDung
        '
        Me.deNgayApDung.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.deNgayApDung.EditValue = Nothing
        Me.deNgayApDung.Location = New System.Drawing.Point(85, 61)
        Me.deNgayApDung.Name = "deNgayApDung"
        Me.deNgayApDung.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgayApDung.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deNgayApDung.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgayApDung.Size = New System.Drawing.Size(151, 20)
        Me.deNgayApDung.TabIndex = 17
        '
        'seSoKm
        '
        Me.seSoKm.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.seSoKm.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seSoKm.Location = New System.Drawing.Point(84, 35)
        Me.seSoKm.Name = "seSoKm"
        Me.seSoKm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seSoKm.Properties.Mask.EditMask = "n"
        Me.seSoKm.Size = New System.Drawing.Size(151, 20)
        Me.seSoKm.TabIndex = 16
        '
        'lueKhachHang
        '
        Me.lueKhachHang.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lueKhachHang.Location = New System.Drawing.Point(84, 9)
        Me.lueKhachHang.Name = "lueKhachHang"
        Me.lueKhachHang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueKhachHang.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ttcMa", "Name2")})
        Me.lueKhachHang.Properties.DisplayMember = "ttcMa"
        Me.lueKhachHang.Properties.NullText = ""
        Me.lueKhachHang.Properties.ShowFooter = False
        Me.lueKhachHang.Properties.ShowHeader = False
        Me.lueKhachHang.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.lueKhachHang.Properties.ValueMember = "ID"
        Me.lueKhachHang.Size = New System.Drawing.Size(151, 20)
        Me.lueKhachHang.TabIndex = 15
        '
        'frmCNSoKM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(260, 142)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.deNgayApDung)
        Me.Controls.Add(Me.seSoKm)
        Me.Controls.Add(Me.lueKhachHang)
        Me.Controls.Add(Me.btnLuu)
        Me.Controls.Add(Me.btLuuVaThem)
        Me.Name = "frmCNSoKM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật số Km"
        CType(Me.deNgayApDung.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayApDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seSoKm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueKhachHang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btLuuVaThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deNgayApDung As DevExpress.XtraEditors.DateEdit
    Friend WithEvents seSoKm As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents lueKhachHang As DevExpress.XtraEditors.LookUpEdit
End Class
