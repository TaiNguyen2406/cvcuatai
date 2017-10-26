<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNNguonKhachMoi
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
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThoiGianLap = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btThem = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.cbNhom = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianLap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianLap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemHyperLinkEdit1
        '
        Me.RepositoryItemHyperLinkEdit1.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit1.Name = "RepositoryItemHyperLinkEdit1"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Thời gian"
        '
        'tbThoiGianLap
        '
        Me.tbThoiGianLap.EditValue = Nothing
        Me.tbThoiGianLap.Enabled = False
        Me.tbThoiGianLap.Location = New System.Drawing.Point(79, 10)
        Me.tbThoiGianLap.Name = "tbThoiGianLap"
        Me.tbThoiGianLap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbThoiGianLap.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianLap.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianLap.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianLap.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianLap.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianLap.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGianLap.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGianLap.Size = New System.Drawing.Size(128, 20)
        Me.tbThoiGianLap.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 39)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Nội dung"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(79, 36)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(470, 149)
        Me.tbNoiDung.TabIndex = 2
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(467, 195)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 27)
        Me.btDong.TabIndex = 6
        Me.btDong.Text = "Đóng"
        '
        'btLuu
        '
        Me.btLuu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuu.Appearance.Options.UseFont = True
        Me.btLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuu.Location = New System.Drawing.Point(359, 195)
        Me.btLuu.Name = "btLuu"
        Me.btLuu.Size = New System.Drawing.Size(102, 27)
        Me.btLuu.TabIndex = 5
        Me.btLuu.Text = "Lưu lại"
        '
        'btThem
        '
        Me.btThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btThem.Appearance.Options.UseFont = True
        Me.btThem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Location = New System.Drawing.Point(246, 195)
        Me.btThem.Name = "btThem"
        Me.btThem.Size = New System.Drawing.Size(107, 27)
        Me.btThem.TabIndex = 4
        Me.btThem.Text = "Thêm mới"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(225, 13)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl5.TabIndex = 16
        Me.LabelControl5.Text = "Nguồn"
        '
        'cbNhom
        '
        Me.cbNhom.Location = New System.Drawing.Point(273, 10)
        Me.cbNhom.Name = "cbNhom"
        Me.cbNhom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbNhom.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name4")})
        Me.cbNhom.Properties.DisplayMember = "NoiDung"
        Me.cbNhom.Properties.DropDownItemHeight = 22
        Me.cbNhom.Properties.NullText = "[Không thuộc nguồn nào]"
        Me.cbNhom.Properties.ShowHeader = False
        Me.cbNhom.Properties.ValueMember = "Ma"
        Me.cbNhom.Size = New System.Drawing.Size(188, 20)
        Me.cbNhom.TabIndex = 1
        '
        'frmCNNguonKhachMoi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 234)
        Me.Controls.Add(Me.cbNhom)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.btThem)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbThoiGianLap)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCNNguonKhachMoi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật yêu cầu nội bộ"
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianLap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianLap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGianLap As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbNhom As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
End Class
