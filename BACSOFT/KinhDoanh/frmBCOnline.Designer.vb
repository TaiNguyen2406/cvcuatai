<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBCOnline
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
        Me.BarManager1 = New DevExpress.XtraBars.BarManager()
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.cbPhong = New DevExpress.XtraBars.BarEditItem()
        Me.rcbPhong = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.tbThoiGian = New DevExpress.XtraBars.BarEditItem()
        Me.rtbThoiGian = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btXem = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mSua = New DevExpress.XtraBars.BarButtonItem()
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.rcbPhongCT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbThoiGian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtbThoiGian.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhongCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.mThem, Me.mSua, Me.mXoa, Me.cbPhong, Me.btXem, Me.tbThoiGian})
        Me.BarManager1.MaxItemId = 9
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbPhong, Me.rtbThoiGian})
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
        Me.cbPhong.Id = 6
        Me.cbPhong.Name = "cbPhong"
        Me.cbPhong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.cbPhong.Width = 190
        '
        'rcbPhong
        '
        Me.rcbPhong.AutoHeight = False
        Me.rcbPhong.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbPhong.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name3", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name4")})
        Me.rcbPhong.DisplayMember = "Ten"
        Me.rcbPhong.DropDownItemHeight = 22
        Me.rcbPhong.Name = "rcbPhong"
        Me.rcbPhong.NullText = "[Tất cả]"
        Me.rcbPhong.ValueMember = "ID"
        '
        'tbThoiGian
        '
        Me.tbThoiGian.Caption = "Đến ngày"
        Me.tbThoiGian.Edit = Me.rtbThoiGian
        Me.tbThoiGian.Id = 8
        Me.tbThoiGian.Name = "tbThoiGian"
        Me.tbThoiGian.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbThoiGian.Width = 92
        '
        'rtbThoiGian
        '
        Me.rtbThoiGian.AutoHeight = False
        Me.rtbThoiGian.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rtbThoiGian.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.rtbThoiGian.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbThoiGian.EditFormat.FormatString = "dd/MM/yyyy"
        Me.rtbThoiGian.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.rtbThoiGian.Mask.EditMask = "dd/MM/yyyy"
        Me.rtbThoiGian.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.rtbThoiGian.Name = "rtbThoiGian"
        Me.rtbThoiGian.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btXem
        '
        Me.btXem.Caption = "Xem"
        Me.btXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btXem.Id = 7
        Me.btXem.Name = "btXem"
        Me.btXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(906, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 489)
        Me.barDockControlBottom.Size = New System.Drawing.Size(906, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 456)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(906, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 456)
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 3
        Me.mThem.Name = "mThem"
        '
        'mSua
        '
        Me.mSua.Caption = "Sửa"
        Me.mSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.mSua.Id = 4
        Me.mSua.Name = "mSua"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xoá"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 5
        Me.mXoa.Name = "mXoa"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 33)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbPhongCT})
        Me.gdv.Size = New System.Drawing.Size(906, 456)
        Me.gdv.TabIndex = 10
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.BandPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.BandPanel.Options.UseFont = True
        Me.gdvCT.Appearance.BandPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.BandPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.gdvCT.Appearance.GroupFooter.Options.UseFont = True
        Me.gdvCT.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.gdvCT.Appearance.GroupRow.Options.UseFont = True
        Me.gdvCT.Appearance.GroupRow.Options.UseForeColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.BandPanelRowHeight = 50
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsBehavior.ReadOnly = True
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        '
        'rcbPhongCT
        '
        Me.rcbPhongCT.AutoHeight = False
        Me.rcbPhongCT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbPhongCT.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name5"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name6")})
        Me.rcbPhongCT.DisplayMember = "Ten"
        Me.rcbPhongCT.Name = "rcbPhongCT"
        Me.rcbPhongCT.ValueMember = "ID"
        '
        'frmTHDiemThiKyNang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmTHDiemThiKyNang"
        Me.Size = New System.Drawing.Size(906, 489)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbThoiGian.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtbThoiGian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhongCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents cbPhong As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcbPhong As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents rcbPhongCT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents tbThoiGian As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rtbThoiGian As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit

End Class
