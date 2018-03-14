<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNYeuCauTuWeb
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
        Me.btnThemMoi = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.gListFile = New DevExpress.XtraEditors.GroupControl()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtDiaChi = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNguoiGD = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSDT = New DevExpress.XtraEditors.TextEdit()
        Me.txtTenKH = New DevExpress.XtraEditors.TextEdit()
        Me.txtSoYC = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.memoNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.dtThoiGianNhan = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtEmail = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gListFile.SuspendLayout()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDiaChi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNguoiGD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSDT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenKH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSoYC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memoNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtThoiGianNhan.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtThoiGianNhan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnThemMoi
        '
        Me.btnThemMoi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnThemMoi.Appearance.Options.UseFont = True
        Me.btnThemMoi.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThemMoi.Location = New System.Drawing.Point(512, 378)
        Me.btnThemMoi.Name = "btnThemMoi"
        Me.btnThemMoi.Size = New System.Drawing.Size(87, 32)
        Me.btnThemMoi.TabIndex = 22
        Me.btnThemMoi.Text = "Thêm mới"
        '
        'btnLuu
        '
        Me.btnLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLuu.Appearance.Options.UseFont = True
        Me.btnLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btnLuu.Location = New System.Drawing.Point(605, 378)
        Me.btnLuu.Name = "btnLuu"
        Me.btnLuu.Size = New System.Drawing.Size(68, 32)
        Me.btnLuu.TabIndex = 21
        Me.btnLuu.Text = "Lưu"
        '
        'btnDong
        '
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btnDong.Location = New System.Drawing.Point(679, 378)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(70, 32)
        Me.btnDong.TabIndex = 20
        Me.btnDong.Text = "Đóng"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.txtEmail)
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.btXoaFile)
        Me.PanelControl1.Controls.Add(Me.btThemFile)
        Me.PanelControl1.Controls.Add(Me.gListFile)
        Me.PanelControl1.Controls.Add(Me.LabelControl9)
        Me.PanelControl1.Controls.Add(Me.txtDiaChi)
        Me.PanelControl1.Controls.Add(Me.LabelControl8)
        Me.PanelControl1.Controls.Add(Me.txtNguoiGD)
        Me.PanelControl1.Controls.Add(Me.LabelControl7)
        Me.PanelControl1.Controls.Add(Me.txtSDT)
        Me.PanelControl1.Controls.Add(Me.txtTenKH)
        Me.PanelControl1.Controls.Add(Me.txtSoYC)
        Me.PanelControl1.Controls.Add(Me.LabelControl5)
        Me.PanelControl1.Controls.Add(Me.memoNoiDung)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.dtThoiGianNhan)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Location = New System.Drawing.Point(12, 13)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(754, 349)
        Me.PanelControl1.TabIndex = 23
        '
        'btXoaFile
        '
        Me.btXoaFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(662, 303)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(75, 23)
        Me.btXoaFile.TabIndex = 38
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(662, 274)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(75, 23)
        Me.btThemFile.TabIndex = 37
        Me.btThemFile.Text = "Thêm file"
        '
        'gListFile
        '
        Me.gListFile.Controls.Add(Me.gdvFile)
        Me.gListFile.Location = New System.Drawing.Point(57, 189)
        Me.gListFile.Name = "gListFile"
        Me.gListFile.Size = New System.Drawing.Size(599, 138)
        Me.gListFile.TabIndex = 36
        Me.gListFile.Text = "Danh sách file đính kèm"
        '
        'gdvFile
        '
        Me.gdvFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvFile.Location = New System.Drawing.Point(2, 22)
        Me.gdvFile.MainView = Me.gdvFileCT
        Me.gdvFile.Name = "gdvFile"
        Me.gdvFile.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHyperLinkEdit1})
        Me.gdvFile.Size = New System.Drawing.Size(595, 114)
        Me.gdvFile.TabIndex = 0
        Me.gdvFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvFileCT})
        '
        'gdvFileCT
        '
        Me.gdvFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFile})
        Me.gdvFileCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvFileCT.GridControl = Me.gdvFile
        Me.gdvFileCT.Name = "gdvFileCT"
        Me.gdvFileCT.OptionsBehavior.Editable = False
        Me.gdvFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvFileCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvFileCT.RowHeight = 22
        '
        'colFile
        '
        Me.colFile.Caption = "File"
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
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(9, 189)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(16, 13)
        Me.LabelControl9.TabIndex = 35
        Me.LabelControl9.Text = "File"
        '
        'txtDiaChi
        '
        Me.txtDiaChi.Location = New System.Drawing.Point(427, 61)
        Me.txtDiaChi.Name = "txtDiaChi"
        Me.txtDiaChi.Size = New System.Drawing.Size(312, 20)
        Me.txtDiaChi.TabIndex = 34
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl8.Location = New System.Drawing.Point(379, 64)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl8.TabIndex = 33
        Me.LabelControl8.Text = "Địa chỉ"
        '
        'txtNguoiGD
        '
        Me.txtNguoiGD.Location = New System.Drawing.Point(59, 35)
        Me.txtNguoiGD.Name = "txtNguoiGD"
        Me.txtNguoiGD.Size = New System.Drawing.Size(305, 20)
        Me.txtNguoiGD.TabIndex = 32
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(8, 38)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl7.TabIndex = 31
        Me.LabelControl7.Text = "Người GD"
        '
        'txtSDT
        '
        Me.txtSDT.Location = New System.Drawing.Point(59, 61)
        Me.txtSDT.Name = "txtSDT"
        Me.txtSDT.Size = New System.Drawing.Size(305, 20)
        Me.txtSDT.TabIndex = 29
        '
        'txtTenKH
        '
        Me.txtTenKH.Location = New System.Drawing.Point(427, 9)
        Me.txtTenKH.Name = "txtTenKH"
        Me.txtTenKH.Size = New System.Drawing.Size(276, 20)
        Me.txtTenKH.TabIndex = 28
        '
        'txtSoYC
        '
        Me.txtSoYC.Enabled = False
        Me.txtSoYC.Location = New System.Drawing.Point(59, 9)
        Me.txtSoYC.Name = "txtSoYC"
        Me.txtSoYC.Size = New System.Drawing.Size(87, 20)
        Me.txtSoYC.TabIndex = 27
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Location = New System.Drawing.Point(8, 66)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(20, 13)
        Me.LabelControl5.TabIndex = 25
        Me.LabelControl5.Text = "SĐT"
        '
        'memoNoiDung
        '
        Me.memoNoiDung.Location = New System.Drawing.Point(59, 89)
        Me.memoNoiDung.Name = "memoNoiDung"
        Me.memoNoiDung.Size = New System.Drawing.Size(682, 94)
        Me.memoNoiDung.TabIndex = 24
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Location = New System.Drawing.Point(11, 117)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl4.TabIndex = 23
        Me.LabelControl4.Text = "Nội dung"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Location = New System.Drawing.Point(379, 12)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl3.TabIndex = 22
        Me.LabelControl3.Text = "Tên KH"
        '
        'dtThoiGianNhan
        '
        Me.dtThoiGianNhan.EditValue = Nothing
        Me.dtThoiGianNhan.Location = New System.Drawing.Point(247, 9)
        Me.dtThoiGianNhan.Name = "dtThoiGianNhan"
        Me.dtThoiGianNhan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtThoiGianNhan.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtThoiGianNhan.Size = New System.Drawing.Size(117, 20)
        Me.dtThoiGianNhan.TabIndex = 21
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(155, 12)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(86, 13)
        Me.LabelControl2.TabIndex = 20
        Me.LabelControl2.Text = "Thời gian nhận YC"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(9, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl1.TabIndex = 19
        Me.LabelControl1.Text = "Số YC"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(427, 35)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(312, 20)
        Me.txtEmail.TabIndex = 40
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(379, 38)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(24, 13)
        Me.LabelControl6.TabIndex = 39
        Me.LabelControl6.Text = "Email"
        '
        'frmCNYeuCauTuWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(778, 427)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.btnThemMoi)
        Me.Controls.Add(Me.btnLuu)
        Me.Controls.Add(Me.btnDong)
        Me.MaximizeBox = False
        Me.Name = "frmCNYeuCauTuWeb"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật yêu cầu từ Webmail"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.gListFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gListFile.ResumeLayout(False)
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDiaChi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNguoiGD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSDT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenKH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSoYC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memoNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtThoiGianNhan.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtThoiGianNhan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnThemMoi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents txtSDT As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTenKH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSoYC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents memoNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtThoiGianNhan As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNguoiGD As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtDiaChi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gListFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
End Class
