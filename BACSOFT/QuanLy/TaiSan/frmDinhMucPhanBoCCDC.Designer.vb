<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDinhMucPhanBoCCDC
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
        Me.gcDinhMucPhanBo = New DevExpress.XtraGrid.GridControl()
        Me.gvDinhMucPhanBo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTSorCCDC = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.mXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.btTim = New DevExpress.XtraBars.BarButtonItem()
        Me.btAn = New DevExpress.XtraBars.BarButtonItem()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btnPhanQuyen = New DevExpress.XtraBars.BarButtonItem()
        Me.mThem = New DevExpress.XtraBars.BarButtonItem()
        Me.mPhanQuyen = New DevExpress.XtraBars.BarButtonItem()
        Me.mNgungSD = New DevExpress.XtraBars.BarButtonItem()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        CType(Me.gcDinhMucPhanBo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDinhMucPhanBo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTSorCCDC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcDinhMucPhanBo
        '
        Me.gcDinhMucPhanBo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDinhMucPhanBo.Location = New System.Drawing.Point(0, 26)
        Me.gcDinhMucPhanBo.MainView = Me.gvDinhMucPhanBo
        Me.gcDinhMucPhanBo.Name = "gcDinhMucPhanBo"
        Me.gcDinhMucPhanBo.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riLueTSorCCDC})
        Me.gcDinhMucPhanBo.Size = New System.Drawing.Size(593, 365)
        Me.gcDinhMucPhanBo.TabIndex = 6
        Me.gcDinhMucPhanBo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDinhMucPhanBo})
        '
        'gvDinhMucPhanBo
        '
        Me.gvDinhMucPhanBo.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvDinhMucPhanBo.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvDinhMucPhanBo.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvDinhMucPhanBo.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvDinhMucPhanBo.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvDinhMucPhanBo.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvDinhMucPhanBo.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvDinhMucPhanBo.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvDinhMucPhanBo.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvDinhMucPhanBo.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvDinhMucPhanBo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn8, Me.GridColumn5, Me.GridColumn3, Me.GridColumn6, Me.GridColumn1, Me.GridColumn4, Me.GridColumn7})
        Me.gvDinhMucPhanBo.FixedLineWidth = 1
        Me.gvDinhMucPhanBo.GridControl = Me.gcDinhMucPhanBo
        Me.gvDinhMucPhanBo.Name = "gvDinhMucPhanBo"
        Me.gvDinhMucPhanBo.OptionsCustomization.AllowColumnMoving = False
        Me.gvDinhMucPhanBo.OptionsCustomization.AllowGroup = False
        Me.gvDinhMucPhanBo.OptionsLayout.Columns.AddNewColumns = False
        Me.gvDinhMucPhanBo.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvDinhMucPhanBo.OptionsView.EnableAppearanceEvenRow = True
        Me.gvDinhMucPhanBo.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top
        Me.gvDinhMucPhanBo.OptionsView.ShowFooter = True
        Me.gvDinhMucPhanBo.OptionsView.ShowGroupPanel = False
        Me.gvDinhMucPhanBo.RowHeight = 23
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn2.AppearanceCell.Options.UseFont = True
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Mã"
        Me.GridColumn2.FieldName = "id"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.Width = 119
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nhóm CCDC"
        Me.GridColumn8.FieldName = "tennhomccdc"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Loại"
        Me.GridColumn5.ColumnEdit = Me.riLueTSorCCDC
        Me.GridColumn5.FieldName = "TSorCCDC"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Width = 111
        '
        'riLueTSorCCDC
        '
        Me.riLueTSorCCDC.AutoHeight = False
        Me.riLueTSorCCDC.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTSorCCDC.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ten", "Tên")})
        Me.riLueTSorCCDC.DisplayMember = "ten"
        Me.riLueTSorCCDC.Name = "riLueTSorCCDC"
        Me.riLueTSorCCDC.NullText = ""
        Me.riLueTSorCCDC.ShowHeader = False
        Me.riLueTSorCCDC.ValueMember = "id"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Từ"
        Me.GridColumn3.DisplayFormat.FormatString = "c0"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "mucdau"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Width = 79
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Đến"
        Me.GridColumn6.DisplayFormat.FormatString = "c0"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "muccuoi"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Width = 82
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn1.Caption = "Thời gian KH"
        Me.GridColumn1.DisplayFormat.FormatString = "{0} năm"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn1.FieldName = "thoigiankh"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 111
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Thời điểm áp dụng"
        Me.GridColumn4.FieldName = "ngayapdung"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 99
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Ghi chú"
        Me.GridColumn7.FieldName = "ghichu"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 108
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.mXoa)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'mXoa
        '
        Me.mXoa.Caption = "Xóa"
        Me.mXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.mXoa.Id = 15
        Me.mXoa.Name = "mXoa"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem2, Me.btTim, Me.btAn, Me.BarEditItem1, Me.btnPhanQuyen, Me.mThem, Me.mPhanQuyen, Me.mXoa, Me.mNgungSD, Me.BarButtonItem1})
        Me.BarManager1.MainMenu = Me.Bar1
        Me.BarManager1.MaxItemId = 19
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositoryItemLookUpEdit1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Custom 2"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.MultiLine = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Custom 2"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Tải lại"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem1.Id = 17
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(593, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 391)
        Me.barDockControlBottom.Size = New System.Drawing.Size(593, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 365)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(593, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 365)
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Sửa"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        Me.BarButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btTim
        '
        Me.btTim.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btTim.Appearance.Options.UseFont = True
        Me.btTim.Caption = "Tìm"
        Me.btTim.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btTim.Id = 8
        Me.btTim.Name = "btTim"
        Me.btTim.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btAn
        '
        Me.btAn.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btAn.Appearance.Options.UseFont = True
        Me.btAn.Caption = "Ẩn"
        Me.btAn.Glyph = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btAn.Id = 9
        Me.btAn.Name = "btAn"
        Me.btAn.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemLookUpEdit1
        Me.BarEditItem1.Id = 10
        Me.BarEditItem1.Name = "BarEditItem1"
        Me.BarEditItem1.Width = 183
        '
        'RepositoryItemLookUpEdit1
        '
        Me.RepositoryItemLookUpEdit1.AutoHeight = False
        Me.RepositoryItemLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemLookUpEdit1.Name = "RepositoryItemLookUpEdit1"
        '
        'btnPhanQuyen
        '
        Me.btnPhanQuyen.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnPhanQuyen.Appearance.Options.UseFont = True
        Me.btnPhanQuyen.Caption = "Phân quyền"
        Me.btnPhanQuyen.Id = 11
        Me.btnPhanQuyen.Name = "btnPhanQuyen"
        Me.btnPhanQuyen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'mThem
        '
        Me.mThem.Caption = "Thêm"
        Me.mThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.mThem.Id = 13
        Me.mThem.Name = "mThem"
        '
        'mPhanQuyen
        '
        Me.mPhanQuyen.Caption = "Phân quyền"
        Me.mPhanQuyen.Glyph = Global.BACSOFT.My.Resources.Resources.GroupKey_18
        Me.mPhanQuyen.Id = 14
        Me.mPhanQuyen.Name = "mPhanQuyen"
        '
        'mNgungSD
        '
        Me.mNgungSD.Caption = "Ngừng sử dụng"
        Me.mNgungSD.Glyph = Global.BACSOFT.My.Resources.Resources.warning_18
        Me.mNgungSD.Id = 16
        Me.mNgungSD.Name = "mNgungSD"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'frmDinhMucPhanBoCCDC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 391)
        Me.Controls.Add(Me.gcDinhMucPhanBo)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmDinhMucPhanBoCCDC"
        Me.Text = "Định mức công cụ, dụng cụ"
        CType(Me.gcDinhMucPhanBo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDinhMucPhanBo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTSorCCDC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcDinhMucPhanBo As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDinhMucPhanBo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents riLueTSorCCDC As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents mXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btTim As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btAn As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btnPhanQuyen As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mPhanQuyen As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mNgungSD As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
End Class
