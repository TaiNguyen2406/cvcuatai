<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTHDiemQuyTrinh
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.rMemoText = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.rcbPhongBan = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.cbPhong = New DevExpress.XtraBars.BarEditItem()
        Me.rcbPhong = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.tbThoiGian = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btXem = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThemKyNang = New DevExpress.XtraBars.BarButtonItem()
        Me.mSuaKN = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoaKN = New DevExpress.XtraBars.BarButtonItem()
        Me.rcbNhom = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.rcbTen = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.rtbDiem = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhongBan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNhom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbTen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbDiem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Tools"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Tools"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 33)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rMemoText, Me.rcbPhongBan})
        Me.gdv.Size = New System.Drawing.Size(951, 397)
        Me.gdv.TabIndex = 6
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsFind.AllowFindPanel = False
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        Me.gdvCT.RowHeight = 22
        '
        'rMemoText
        '
        Me.rMemoText.Name = "rMemoText"
        '
        'rcbPhongBan
        '
        Me.rcbPhongBan.AutoHeight = False
        Me.rcbPhongBan.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbPhongBan.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.rcbPhongBan.DisplayMember = "Ten"
        Me.rcbPhongBan.DropDownItemHeight = 22
        Me.rcbPhongBan.Name = "rcbPhongBan"
        Me.rcbPhongBan.NullText = ""
        Me.rcbPhongBan.ShowHeader = False
        Me.rcbPhongBan.ValueMember = "ID"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mThemKyNang, Me.mSuaKN, Me.mXoaKN, Me.cbPhong, Me.btXem, Me.tbThoiGian})
        Me.BarManager1.MaxItemId = 14
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbPhong, Me.rcbNhom, Me.rcbTen, Me.rtbDiem, Me.RepositoryItemDateEdit1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cbPhong), New DevExpress.XtraBars.LinkPersistInfo(Me.tbThoiGian), New DevExpress.XtraBars.LinkPersistInfo(Me.btXem)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'cbPhong
        '
        Me.cbPhong.Caption = "Phòng"
        Me.cbPhong.Edit = Me.rcbPhong
        Me.cbPhong.Id = 8
        Me.cbPhong.Name = "cbPhong"
        Me.cbPhong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbPhong.Width = 212
        '
        'rcbPhong
        '
        Me.rcbPhong.AutoHeight = False
        Me.rcbPhong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbPhong.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Ten")})
        Me.rcbPhong.DisplayMember = "Ten"
        Me.rcbPhong.DropDownItemHeight = 22
        Me.rcbPhong.Name = "rcbPhong"
        Me.rcbPhong.NullText = "[Tất cả]"
        Me.rcbPhong.ShowHeader = False
        Me.rcbPhong.ValueMember = "ID"
        '
        'tbThoiGian
        '
        Me.tbThoiGian.Caption = "Đến ngày"
        Me.tbThoiGian.Edit = Me.RepositoryItemDateEdit1
        Me.tbThoiGian.Id = 13
        Me.tbThoiGian.Name = "tbThoiGian"
        Me.tbThoiGian.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbThoiGian.Width = 102
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btXem
        '
        Me.btXem.Caption = "Xem"
        Me.btXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btXem.Id = 12
        Me.btXem.Name = "btXem"
        Me.btXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(951, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 430)
        Me.barDockControlBottom.Size = New System.Drawing.Size(951, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 397)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(951, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 397)
        '
        'mThemKyNang
        '
        Me.mThemKyNang.Caption = "Thêm kỹ năng"
        Me.mThemKyNang.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThemKyNang.Id = 5
        Me.mThemKyNang.Name = "mThemKyNang"
        Me.mThemKyNang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mSuaKN
        '
        Me.mSuaKN.Caption = "Sửa thông tin kỹ năng"
        Me.mSuaKN.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSuaKN.Id = 6
        Me.mSuaKN.Name = "mSuaKN"
        '
        'mXoaKN
        '
        Me.mXoaKN.Caption = "Xoá kỹ năng"
        Me.mXoaKN.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoaKN.Id = 7
        Me.mXoaKN.Name = "mXoaKN"
        '
        'rcbNhom
        '
        Me.rcbNhom.AutoHeight = False
        Me.rcbNhom.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbNhom.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name4")})
        Me.rcbNhom.DisplayMember = "Ten"
        Me.rcbNhom.DropDownItemHeight = 22
        Me.rcbNhom.Name = "rcbNhom"
        Me.rcbNhom.NullText = "[Tất cả]"
        Me.rcbNhom.ShowHeader = False
        Me.rcbNhom.ValueMember = "ID"
        '
        'rcbTen
        '
        Me.rcbTen.AutoHeight = False
        Me.rcbTen.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbTen.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name5"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name6", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.rcbTen.DisplayMember = "Ten"
        Me.rcbTen.DropDownItemHeight = 22
        Me.rcbTen.Name = "rcbTen"
        Me.rcbTen.NullText = "[Tất cả]"
        Me.rcbTen.ShowHeader = False
        Me.rcbTen.ValueMember = "ID"
        '
        'rtbDiem
        '
        Me.rtbDiem.AutoHeight = False
        Me.rtbDiem.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.rtbDiem.Name = "rtbDiem"
        '
        'frmTHDiemQuyTrinh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmTHDiemQuyTrinh"
        Me.Size = New System.Drawing.Size(951, 430)
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhongBan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNhom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbTen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbDiem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rMemoText As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents mThemKyNang As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSuaKN As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoaKN As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents cbPhong As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbPhong As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents rcbNhom As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents rcbTen As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents rtbDiem As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents btXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents tbThoiGian As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents rcbPhongBan As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit

End Class
