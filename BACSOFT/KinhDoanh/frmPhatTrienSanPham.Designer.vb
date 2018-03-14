<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhatTrienSanPham
    Inherits DevExpress.XtraEditors.XtraUserControl

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
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbHang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbPhuTrach = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tbN0 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.tbThang = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.tbNam = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemSpinEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.btXem = New DevExpress.XtraBars.BarButtonItem()
        Me.btLayDuLieuThangTruoc = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbHang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbPhuTrach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbN0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 33)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbHang, Me.tbN0, Me.rcbPhuTrach})
        Me.gdv.Size = New System.Drawing.Size(969, 475)
        Me.gdv.TabIndex = 1
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
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.GridColumn9, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.GroupCount = 1
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gdvCT.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gdvCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupedColumns = True
        Me.gdvCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn9, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "ID"
        Me.GridColumn5.FieldName = "ID"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Hãng sản phẩm"
        Me.GridColumn9.ColumnEdit = Me.rcbHang
        Me.GridColumn9.FieldName = "IDHangSX"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        Me.GridColumn9.Width = 181
        '
        'rcbHang
        '
        Me.rcbHang.AutoHeight = False
        Me.rcbHang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbHang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name2")})
        Me.rcbHang.DisplayMember = "Ten"
        Me.rcbHang.DropDownItemHeight = 22
        Me.rcbHang.Name = "rcbHang"
        Me.rcbHang.NullText = ""
        Me.rcbHang.ShowHeader = False
        Me.rcbHang.ValueMember = "ID"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Phụ trách"
        Me.GridColumn1.ColumnEdit = Me.rcbPhuTrach
        Me.GridColumn1.FieldName = "IDPhuTrach"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 150
        '
        'rcbPhuTrach
        '
        Me.rcbPhuTrach.AutoHeight = False
        Me.rcbPhuTrach.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbPhuTrach.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name5", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ten", "Name6")})
        Me.rcbPhuTrach.DisplayMember = "Ten"
        Me.rcbPhuTrach.DropDownItemHeight = 22
        Me.rcbPhuTrach.Name = "rcbPhuTrach"
        Me.rcbPhuTrach.NullText = ""
        Me.rcbPhuTrach.ShowHeader = False
        Me.rcbPhuTrach.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.rcbPhuTrach.ValueMember = "ID"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "% LN TM"
        Me.GridColumn2.FieldName = "PTLoiNhuanTM"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 63
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "% LN CT"
        Me.GridColumn3.FieldName = "PTLoiNhuanCT"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 61
        '
        'tbN0
        '
        Me.tbN0.AutoHeight = False
        Me.tbN0.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbN0.DisplayFormat.FormatString = "N0"
        Me.tbN0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbN0.EditFormat.FormatString = "N0"
        Me.tbN0.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.tbN0.Name = "tbN0"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tbThang, Me.tbNam, Me.btXem, Me.btLayDuLieuThangTruoc})
        Me.BarManager1.MaxItemId = 5
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1, Me.RepositoryItemSpinEdit2, Me.RepositoryItemComboBox1})
        '
        'Bar1
        '
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tbThang), New DevExpress.XtraBars.LinkPersistInfo(Me.tbNam), New DevExpress.XtraBars.LinkPersistInfo(Me.btXem), New DevExpress.XtraBars.LinkPersistInfo(Me.btLayDuLieuThangTruoc)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'tbThang
        '
        Me.tbThang.Caption = "Tháng"
        Me.tbThang.Edit = Me.RepositoryItemComboBox1
        Me.tbThang.Id = 0
        Me.tbThang.Name = "tbThang"
        Me.tbThang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.tbThang.Width = 40
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.DropDownItemHeight = 22
        Me.RepositoryItemComboBox1.DropDownRows = 12
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'tbNam
        '
        Me.tbNam.Caption = "Năm"
        Me.tbNam.Edit = Me.RepositoryItemSpinEdit2
        Me.tbNam.Id = 1
        Me.tbNam.Name = "tbNam"
        '
        'RepositoryItemSpinEdit2
        '
        Me.RepositoryItemSpinEdit2.AutoHeight = False
        Me.RepositoryItemSpinEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit2.Name = "RepositoryItemSpinEdit2"
        '
        'btXem
        '
        Me.btXem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btXem.Appearance.Options.UseFont = True
        Me.btXem.Caption = "Xem"
        Me.btXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btXem.Id = 2
        Me.btXem.Name = "btXem"
        Me.btXem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btLayDuLieuThangTruoc
        '
        Me.btLayDuLieuThangTruoc.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLayDuLieuThangTruoc.Appearance.Options.UseFont = True
        Me.btLayDuLieuThangTruoc.Caption = "Lấy dữ liệu tháng trước"
        Me.btLayDuLieuThangTruoc.Id = 4
        Me.btLayDuLieuThangTruoc.Name = "btLayDuLieuThangTruoc"
        Me.btLayDuLieuThangTruoc.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(969, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 508)
        Me.barDockControlBottom.Size = New System.Drawing.Size(969, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 475)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(969, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 475)
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit1.MaxValue = New Decimal(New Integer() {12, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'frmPhatTrienSanPham
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmPhatTrienSanPham"
        Me.Size = New System.Drawing.Size(969, 508)
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbHang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbPhuTrach, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbN0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbHang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents tbN0 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents tbThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents tbNam As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemSpinEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents btXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbPhuTrach As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents btLayDuLieuThangTruoc As DevExpress.XtraBars.BarButtonItem
End Class
