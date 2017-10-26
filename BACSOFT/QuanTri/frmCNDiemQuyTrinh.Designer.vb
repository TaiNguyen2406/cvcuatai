<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNDiemQuyTrinh
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
        Me.cbQuyTrinh = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btLuuVaThem = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.tbNgayThi = New DevExpress.XtraEditors.DateEdit()
        Me.gdvNhanVien = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhanVienCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rgdvHocVien = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.cbQuyTrinh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNgayThi.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNgayThi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhanVien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhanVienCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgdvHocVien, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 15)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Quy trình"
        '
        'cbQuyTrinh
        '
        Me.cbQuyTrinh.Location = New System.Drawing.Point(79, 12)
        Me.cbQuyTrinh.Name = "cbQuyTrinh"
        Me.cbQuyTrinh.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.cbQuyTrinh.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenQuyTrinh", "TenQuyTrinh")})
        Me.cbQuyTrinh.Properties.DisplayMember = "TenQuyTrinh"
        Me.cbQuyTrinh.Properties.DropDownItemHeight = 22
        Me.cbQuyTrinh.Properties.NullText = "[Chọn quy trình]"
        Me.cbQuyTrinh.Properties.ShowHeader = False
        Me.cbQuyTrinh.Properties.ValueMember = "ID"
        Me.cbQuyTrinh.Size = New System.Drawing.Size(454, 20)
        Me.cbQuyTrinh.TabIndex = 1
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(13, 42)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl4.TabIndex = 0
        Me.LabelControl4.Text = "Ngày bảo vệ"
        '
        'btLuuVaThem
        '
        Me.btLuuVaThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuVaThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaThem.Appearance.Options.UseFont = True
        Me.btLuuVaThem.Image = Global.BACSOFT.My.Resources.Resources.Save_AddNew_18
        Me.btLuuVaThem.Location = New System.Drawing.Point(226, 429)
        Me.btLuuVaThem.Name = "btLuuVaThem"
        Me.btLuuVaThem.Size = New System.Drawing.Size(108, 31)
        Me.btLuuVaThem.TabIndex = 6
        Me.btLuuVaThem.Text = "Lưu và thêm"
        '
        'btLuu
        '
        Me.btLuu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuu.Appearance.Options.UseFont = True
        Me.btLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuu.Location = New System.Drawing.Point(340, 429)
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
        Me.btDong.Location = New System.Drawing.Point(454, 429)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(79, 31)
        Me.btDong.TabIndex = 7
        Me.btDong.Text = "Đóng"
        '
        'tbNgayThi
        '
        Me.tbNgayThi.EditValue = Nothing
        Me.tbNgayThi.Location = New System.Drawing.Point(79, 39)
        Me.tbNgayThi.Name = "tbNgayThi"
        Me.tbNgayThi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbNgayThi.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgayThi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgayThi.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgayThi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgayThi.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.tbNgayThi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbNgayThi.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbNgayThi.Size = New System.Drawing.Size(100, 20)
        Me.tbNgayThi.TabIndex = 8
        '
        'gdvNhanVien
        '
        Me.gdvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNhanVien.Location = New System.Drawing.Point(2, 2)
        Me.gdvNhanVien.MainView = Me.gdvNhanVienCT
        Me.gdvNhanVien.Name = "gdvNhanVien"
        Me.gdvNhanVien.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rgdvHocVien})
        Me.gdvNhanVien.Size = New System.Drawing.Size(450, 353)
        Me.gdvNhanVien.TabIndex = 9
        Me.gdvNhanVien.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhanVienCT})
        '
        'gdvNhanVienCT
        '
        Me.gdvNhanVienCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhanVienCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNhanVienCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhanVienCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNhanVienCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhanVienCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhanVienCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhanVienCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhanVienCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhanVienCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvNhanVienCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn4, Me.GridColumn3, Me.GridColumn5})
        Me.gdvNhanVienCT.GridControl = Me.gdvNhanVien
        Me.gdvNhanVienCT.GroupCount = 1
        Me.gdvNhanVienCT.Name = "gdvNhanVienCT"
        Me.gdvNhanVienCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvNhanVienCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvNhanVienCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhanVienCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvNhanVienCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhanVienCT.RowHeight = 22
        Me.gdvNhanVienCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn5, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nhân viên"
        Me.GridColumn2.ColumnEdit = Me.rgdvHocVien
        Me.GridColumn2.FieldName = "IDNhanVien"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 243
        '
        'rgdvHocVien
        '
        Me.rgdvHocVien.AutoHeight = False
        Me.rgdvHocVien.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rgdvHocVien.DisplayMember = "Ten"
        Me.rgdvHocVien.Name = "rgdvHocVien"
        Me.rgdvHocVien.NullText = ""
        Me.rgdvHocVien.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.rgdvHocVien.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rgdvHocVien.ValueMember = "ID"
        Me.rgdvHocVien.View = Me.GridView2
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView2.OptionsBehavior.Editable = False
        Me.GridView2.OptionsBehavior.ReadOnly = True
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "% điểm"
        Me.GridColumn4.FieldName = "Diem"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "File"
        Me.GridColumn3.FieldName = "FileDinhKem"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 36
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Phòng"
        Me.GridColumn5.FieldName = "Phong"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.gdvNhanVien)
        Me.GroupControl1.Location = New System.Drawing.Point(79, 66)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(454, 357)
        Me.GroupControl1.TabIndex = 10
        Me.GroupControl1.Text = "GroupControl1"
        '
        'frmCNDiemQuyTrinh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 472)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.tbNgayThi)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.btLuuVaThem)
        Me.Controls.Add(Me.cbQuyTrinh)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmCNDiemQuyTrinh"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật điểm bảo vệ quy trình"
        CType(Me.cbQuyTrinh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNgayThi.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNgayThi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhanVien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhanVienCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgdvHocVien, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbQuyTrinh As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btLuuVaThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbNgayThi As DevExpress.XtraEditors.DateEdit
    Friend WithEvents gdvNhanVien As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNhanVienCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rgdvHocVien As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
