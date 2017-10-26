<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaiSanHong
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
        Me.gcTaiSanHong = New DevExpress.XtraGrid.GridControl()
        Me.gvTaiSanHong = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.loi = New DevExpress.XtraBars.BarEditItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.btnThem = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSua = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.barLueTaiSan = New DevExpress.XtraBars.BarEditItem()
        Me.riLueTaiSan = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barLueChiTietTS = New DevExpress.XtraBars.BarEditItem()
        Me.riLueChiTietTS = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.barGlueNSD = New DevExpress.XtraBars.BarEditItem()
        Me.riGlueNSD = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarButtonItem8 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.RepositoryItemGridLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.gcTaiSanHong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTaiSanHong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTaiSan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueChiTietTS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcTaiSanHong
        '
        Me.gcTaiSanHong.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcTaiSanHong.Location = New System.Drawing.Point(0, 59)
        Me.gcTaiSanHong.MainView = Me.gvTaiSanHong
        Me.gcTaiSanHong.Name = "gcTaiSanHong"
        Me.gcTaiSanHong.Size = New System.Drawing.Size(1032, 202)
        Me.gcTaiSanHong.TabIndex = 9
        Me.gcTaiSanHong.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvTaiSanHong})
        '
        'gvTaiSanHong
        '
        Me.gvTaiSanHong.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.Black
        Me.gvTaiSanHong.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvTaiSanHong.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvTaiSanHong.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvTaiSanHong.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTaiSanHong.Appearance.FooterPanel.Options.UseFont = True
        Me.gvTaiSanHong.Appearance.FooterPanel.Options.UseTextOptions = True
        Me.gvTaiSanHong.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvTaiSanHong.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvTaiSanHong.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvTaiSanHong.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvTaiSanHong.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvTaiSanHong.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn7, Me.GridColumn8})
        Me.gvTaiSanHong.GridControl = Me.gcTaiSanHong
        Me.gvTaiSanHong.Name = "gvTaiSanHong"
        Me.gvTaiSanHong.OptionsBehavior.Editable = False
        Me.gvTaiSanHong.OptionsCustomization.AllowColumnMoving = False
        Me.gvTaiSanHong.OptionsCustomization.AllowGroup = False
        Me.gvTaiSanHong.OptionsView.EnableAppearanceEvenRow = True
        Me.gvTaiSanHong.OptionsView.ShowFooter = True
        Me.gvTaiSanHong.OptionsView.ShowGroupPanel = False
        Me.gvTaiSanHong.OptionsView.ShowIndicator = False
        Me.gvTaiSanHong.RowHeight = 23
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "ID"
        Me.GridColumn4.FieldName = "id"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "ID người sử dụng"
        Me.GridColumn5.FieldName = "idnhansu"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "ID tài sản"
        Me.GridColumn6.FieldName = "idtaisan"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Người làm hỏng"
        Me.GridColumn1.FieldName = "Ten"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tài sản"
        Me.GridColumn2.FieldName = "tenchitiettaisan"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Ngày hỏng"
        Me.GridColumn3.FieldName = "ngaysua"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Chi phí"
        Me.GridColumn7.DisplayFormat.FormatString = "c0"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "chiphi"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.SummaryItem.DisplayFormat = "Tổng: {0:c0}"
        Me.GridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Ghi chú"
        Me.GridColumn8.FieldName = "ghichuhongts"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 4
        '
        'PopupMenu1
        '
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'loi
        '
        Me.loi.Caption = "Chi tiết T2"
        Me.loi.Edit = Nothing
        Me.loi.Id = 12
        Me.loi.Name = "loi"
        Me.loi.Width = 174
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnThem, Me.btnSua, Me.btnXoa, Me.BarButtonItem8, Me.barLueTaiSan, Me.barGlueNSD, Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.barLueChiTietTS})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 13
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riGlueNSD, Me.riLueTaiSan, Me.RepositoryItemGridLookUpEdit1, Me.riLueChiTietTS})
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnThem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnSua, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueTaiSan, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueChiTietTS, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barGlueNSD, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem8, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'btnThem
        '
        Me.btnThem.Caption = "Thêm"
        Me.btnThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThem.Id = 0
        Me.btnThem.Name = "btnThem"
        '
        'btnSua
        '
        Me.btnSua.Caption = "Sửa"
        Me.btnSua.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.btnSua.Id = 1
        Me.btnSua.Name = "btnSua"
        '
        'btnXoa
        '
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btnXoa.Id = 2
        Me.btnXoa.Name = "btnXoa"
        '
        'barLueTaiSan
        '
        Me.barLueTaiSan.Caption = "Tài sản"
        Me.barLueTaiSan.Edit = Me.riLueTaiSan
        Me.barLueTaiSan.Id = 6
        Me.barLueTaiSan.Name = "barLueTaiSan"
        Me.barLueTaiSan.Width = 165
        '
        'riLueTaiSan
        '
        Me.riLueTaiSan.AutoHeight = False
        Me.riLueTaiSan.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueTaiSan.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ten", "Tên tài sản"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Mã TS")})
        Me.riLueTaiSan.DisplayMember = "Model"
        Me.riLueTaiSan.Name = "riLueTaiSan"
        Me.riLueTaiSan.NullText = "Tất cả"
        Me.riLueTaiSan.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueTaiSan.ValueMember = "id"
        '
        'barLueChiTietTS
        '
        Me.barLueChiTietTS.Caption = "Chi tiết TS"
        Me.barLueChiTietTS.Edit = Me.riLueChiTietTS
        Me.barLueChiTietTS.Id = 12
        Me.barLueChiTietTS.Name = "barLueChiTietTS"
        Me.barLueChiTietTS.Width = 174
        '
        'riLueChiTietTS
        '
        Me.riLueChiTietTS.AutoHeight = False
        Me.riLueChiTietTS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riLueChiTietTS.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenchitiettaisan", "Tên")})
        Me.riLueChiTietTS.DisplayMember = "tenchitiettaisan"
        Me.riLueChiTietTS.Name = "riLueChiTietTS"
        Me.riLueChiTietTS.NullText = "Tất cả"
        Me.riLueChiTietTS.ShowHeader = False
        Me.riLueChiTietTS.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riLueChiTietTS.ValueMember = "id"
        '
        'barGlueNSD
        '
        Me.barGlueNSD.Caption = "Người làm hỏng"
        Me.barGlueNSD.Edit = Me.riGlueNSD
        Me.barGlueNSD.Id = 7
        Me.barGlueNSD.Name = "barGlueNSD"
        Me.barGlueNSD.Width = 165
        '
        'riGlueNSD
        '
        Me.riGlueNSD.AutoHeight = False
        Me.riGlueNSD.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.riGlueNSD.DisplayMember = "Ten"
        Me.riGlueNSD.Name = "riGlueNSD"
        Me.riGlueNSD.NullText = "Tất cả"
        Me.riGlueNSD.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.riGlueNSD.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.riGlueNSD.ValueMember = "ID"
        Me.riGlueNSD.View = Me.RepositoryItemGridLookUpEdit1View
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = False
        '
        'BarButtonItem8
        '
        Me.BarButtonItem8.Caption = "Tải lại"
        Me.BarButtonItem8.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem8.Id = 3
        Me.BarButtonItem8.Name = "BarButtonItem8"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1032, 59)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 261)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1032, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 59)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 202)
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl1.Location = New System.Drawing.Point(1032, 59)
        Me.BarDockControl1.Size = New System.Drawing.Size(0, 202)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Thêm"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.BarButtonItem1.Id = 8
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Sửa"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.Edit_18
        Me.BarButtonItem2.Id = 9
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Xóa"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem3.Id = 10
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'RepositoryItemGridLookUpEdit1
        '
        Me.RepositoryItemGridLookUpEdit1.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit1.Name = "RepositoryItemGridLookUpEdit1"
        Me.RepositoryItemGridLookUpEdit1.View = Me.GridView2
        '
        'GridView2
        '
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'frmTaiSanHong
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1032, 261)
        Me.Controls.Add(Me.gcTaiSanHong)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmTaiSanHong"
        Me.Text = "Tài sản hỏng"
        CType(Me.gcTaiSanHong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTaiSanHong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTaiSan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueChiTietTS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcTaiSanHong As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvTaiSanHong As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents loi As DevExpress.XtraBars.BarEditItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents btnThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSua As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barLueTaiSan As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueTaiSan As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barLueChiTietTS As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riLueChiTietTS As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents barGlueNSD As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riGlueNSD As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem8 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemGridLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
