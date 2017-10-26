<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNYeuCauNoiBo
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThoiGianYCHoanThanh = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.treeNV = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btThem = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.cbNhom = New DevExpress.XtraEditors.LookUpEdit()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianLap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianLap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianYCHoanThanh.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianYCHoanThanh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Thời gian lập"
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
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(225, 15)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(96, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Thời y/c hoàn thành"
        '
        'tbThoiGianYCHoanThanh
        '
        Me.tbThoiGianYCHoanThanh.EditValue = Nothing
        Me.tbThoiGianYCHoanThanh.Location = New System.Drawing.Point(327, 10)
        Me.tbThoiGianYCHoanThanh.Name = "tbThoiGianYCHoanThanh"
        Me.tbThoiGianYCHoanThanh.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.tbThoiGianYCHoanThanh.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianYCHoanThanh.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianYCHoanThanh.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianYCHoanThanh.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianYCHoanThanh.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianYCHoanThanh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGianYCHoanThanh.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGianYCHoanThanh.Size = New System.Drawing.Size(149, 20)
        Me.tbThoiGianYCHoanThanh.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 66)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Nội dung"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(79, 63)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(470, 149)
        Me.tbNoiDung.TabIndex = 2
        '
        'treeNV
        '
        Me.treeNV.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn2})
        Me.treeNV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeNV.Location = New System.Drawing.Point(79, 218)
        Me.treeNV.Name = "treeNV"
        Me.treeNV.OptionsBehavior.Editable = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.treeNV.OptionsView.ShowCheckBoxes = True
        Me.treeNV.OptionsView.ShowHorzLines = False
        Me.treeNV.OptionsView.ShowIndicator = False
        Me.treeNV.OptionsView.ShowVertLines = False
        Me.treeNV.ParentFieldName = "PhongBan"
        Me.treeNV.Size = New System.Drawing.Size(252, 273)
        Me.treeNV.TabIndex = 3
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TreeListColumn2.AppearanceHeader.Options.UseFont = True
        Me.TreeListColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.TreeListColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.TreeListColumn2.Caption = "Người thực hiện"
        Me.TreeListColumn2.FieldName = "Nhân viên"
        Me.TreeListColumn2.MinWidth = 32
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 0
        Me.TreeListColumn2.Width = 372
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(467, 500)
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
        Me.btLuu.Location = New System.Drawing.Point(359, 500)
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
        Me.btThem.Location = New System.Drawing.Point(246, 500)
        Me.btThem.Name = "btThem"
        Me.btThem.Size = New System.Drawing.Size(107, 27)
        Me.btThem.TabIndex = 4
        Me.btThem.Text = "Thêm mới"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(13, 40)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl5.TabIndex = 16
        Me.LabelControl5.Text = "Nhóm"
        '
        'cbNhom
        '
        Me.cbNhom.Location = New System.Drawing.Point(79, 37)
        Me.cbNhom.Name = "cbNhom"
        Me.cbNhom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbNhom.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "Name4")})
        Me.cbNhom.Properties.DisplayMember = "NoiDung"
        Me.cbNhom.Properties.DropDownItemHeight = 22
        Me.cbNhom.Properties.NullText = "[Không thuộc nhóm nào]"
        Me.cbNhom.Properties.ValueMember = "Ma"
        Me.cbNhom.Size = New System.Drawing.Size(188, 20)
        Me.cbNhom.TabIndex = 1
        '
        'gdvFile
        '
        Me.gdvFile.Location = New System.Drawing.Point(337, 218)
        Me.gdvFile.MainView = Me.gdvFileCT
        Me.gdvFile.Name = "gdvFile"
        Me.gdvFile.Size = New System.Drawing.Size(212, 213)
        Me.gdvFile.TabIndex = 17
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
        'btXoaFile
        '
        Me.btXoaFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(449, 440)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(75, 23)
        Me.btXoaFile.TabIndex = 19
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(368, 440)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(75, 23)
        Me.btThemFile.TabIndex = 18
        Me.btThemFile.Text = "Thêm file"
        '
        'frmCNYeuCauNoiBo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 539)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gdvFile)
        Me.Controls.Add(Me.cbNhom)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.btThem)
        Me.Controls.Add(Me.treeNV)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbThoiGianYCHoanThanh)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.tbThoiGianLap)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCNYeuCauNoiBo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật yêu cầu nội bộ"
        CType(Me.RepositoryItemHyperLinkEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianLap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianLap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianYCHoanThanh.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianYCHoanThanh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNhom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGianLap As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGianYCHoanThanh As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents treeNV As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbNhom As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemHyperLinkEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
End Class
