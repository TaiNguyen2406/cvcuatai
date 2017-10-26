<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLichSuGiaoDich
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
        Me.components = New System.ComponentModel.Container()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btGhi = New DevExpress.XtraEditors.SimpleButton()
        Me.btThem = New DevExpress.XtraEditors.SimpleButton()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.cbTakecare = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThoiGian = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cbHinhThuc = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbTakecare.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGian.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGian.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbHinhThuc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.CausesValidation = False
        Me.btDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btDong.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(501, 145)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 28)
        Me.btDong.TabIndex = 6
        Me.btDong.Text = "Đóng"
        '
        'btGhi
        '
        Me.btGhi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btGhi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btGhi.Appearance.Options.UseFont = True
        Me.btGhi.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btGhi.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btGhi.Location = New System.Drawing.Point(385, 145)
        Me.btGhi.Name = "btGhi"
        Me.btGhi.Size = New System.Drawing.Size(110, 28)
        Me.btGhi.TabIndex = 5
        Me.btGhi.Text = "Lưu và đóng"
        '
        'btThem
        '
        Me.btThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btThem.Appearance.Options.UseFont = True
        Me.btThem.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btThem.Image = Global.BACSOFT.My.Resources.Resources.Save_AddNew_18
        Me.btThem.Location = New System.Drawing.Point(274, 145)
        Me.btThem.Name = "btThem"
        Me.btThem.Size = New System.Drawing.Size(105, 28)
        Me.btThem.TabIndex = 4
        Me.btThem.Text = "Lưu và thêm"
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mThem, Me.mXoa})
        Me.BarManager1.MaxItemId = 2
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(588, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 185)
        Me.barDockControlBottom.Size = New System.Drawing.Size(588, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 185)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(588, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 185)
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm file"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 0
        Me.mThem.Name = "mThem"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xoá file"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 1
        Me.mXoa.Name = "mXoa"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mThem), New DevExpress.XtraBars.LinkPersistInfo(Me.mXoa)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'cbTakecare
        '
        Me.cbTakecare.Location = New System.Drawing.Point(73, 114)
        Me.cbTakecare.Name = "cbTakecare"
        Me.cbTakecare.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbTakecare.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Tên")})
        Me.cbTakecare.Properties.DisplayMember = "Ten"
        Me.cbTakecare.Properties.DropDownItemHeight = 22
        Me.cbTakecare.Properties.NullText = ""
        Me.cbTakecare.Properties.ShowHeader = False
        Me.cbTakecare.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbTakecare.Properties.ValueMember = "ID"
        Me.cbTakecare.Size = New System.Drawing.Size(216, 20)
        Me.cbTakecare.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 117)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl3.TabIndex = 17
        Me.LabelControl3.Text = "Người nhập"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.EditValue = ""
        Me.tbNoiDung.Location = New System.Drawing.Point(73, 39)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(508, 69)
        Me.tbNoiDung.TabIndex = 2
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 66)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl1.TabIndex = 17
        Me.LabelControl1.Text = "Nội dung"
        '
        'tbThoiGian
        '
        Me.tbThoiGian.EditValue = Nothing
        Me.tbThoiGian.Location = New System.Drawing.Point(73, 13)
        Me.tbThoiGian.MenuManager = Me.BarManager1
        Me.tbThoiGian.Name = "tbThoiGian"
        Me.tbThoiGian.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbThoiGian.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGian.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGian.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGian.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGian.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.tbThoiGian.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGian.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGian.Size = New System.Drawing.Size(135, 20)
        Me.tbThoiGian.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 16)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl2.TabIndex = 17
        Me.LabelControl2.Text = "Thời gian"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(214, 16)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl4.TabIndex = 17
        Me.LabelControl4.Text = "Hình thức"
        '
        'cbHinhThuc
        '
        Me.cbHinhThuc.Location = New System.Drawing.Point(266, 13)
        Me.cbHinhThuc.MenuManager = Me.BarManager1
        Me.cbHinhThuc.Name = "cbHinhThuc"
        Me.cbHinhThuc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbHinhThuc.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name2")})
        Me.cbHinhThuc.Properties.DisplayMember = "NoiDung"
        Me.cbHinhThuc.Properties.DropDownItemHeight = 22
        Me.cbHinhThuc.Properties.NullText = ""
        Me.cbHinhThuc.Properties.ShowHeader = False
        Me.cbHinhThuc.Properties.ValueMember = "Ma"
        Me.cbHinhThuc.Size = New System.Drawing.Size(100, 20)
        Me.cbHinhThuc.TabIndex = 22
        '
        'frmLichSuGiaoDich
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btDong
        Me.ClientSize = New System.Drawing.Size(588, 185)
        Me.Controls.Add(Me.cbHinhThuc)
        Me.Controls.Add(Me.tbThoiGian)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btGhi)
        Me.Controls.Add(Me.cbTakecare)
        Me.Controls.Add(Me.btThem)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLichSuGiaoDich"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật lịch sử giao dịch"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbTakecare.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGian.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGian.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbHinhThuc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btGhi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents cbTakecare As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGian As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbHinhThuc As DevExpress.XtraEditors.LookUpEdit
End Class
