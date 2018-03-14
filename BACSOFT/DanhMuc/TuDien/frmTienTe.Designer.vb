<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTienTe
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
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSpinEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem()
        Me.btCapNhatTyGia = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.pmThem = New DevExpress.XtraBars.BarButtonItem()
        Me.pmXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.pmLuuLai = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 33)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemSpinEdit1, Me.RepositoryItemDateEdit1})
        Me.gdv.Size = New System.Drawing.Size(809, 450)
        Me.gdv.TabIndex = 0
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
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvCT.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsFilter.AllowColumnMRUFilterList = False
        Me.gdvCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvCT.OptionsFind.AllowFindPanel = False
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        Me.gdvCT.RowHeight = 22
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Mã"
        Me.GridColumn4.FieldName = "ID"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.FixedWidth = True
        Me.GridColumn4.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Tiền tệ"
        Me.GridColumn1.FieldName = "Ten"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 100
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "ID"
        Me.GridColumn2.FieldName = "ID2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn3.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GridColumn3.AppearanceCell.Options.UseFont = True
        Me.GridColumn3.AppearanceCell.Options.UseForeColor = True
        Me.GridColumn3.Caption = "Tỷ giá hàng ngày"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemSpinEdit1
        Me.GridColumn3.DisplayFormat.FormatString = "N2"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "TyGia"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.FixedWidth = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 135
        '
        'RepositoryItemSpinEdit1
        '
        Me.RepositoryItemSpinEdit1.AutoHeight = False
        Me.RepositoryItemSpinEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.RepositoryItemSpinEdit1.DisplayFormat.FormatString = "N2"
        Me.RepositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit1.EditFormat.FormatString = "N2"
        Me.RepositoryItemSpinEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemSpinEdit1.Mask.EditMask = "N2"
        Me.RepositoryItemSpinEdit1.Name = "RepositoryItemSpinEdit1"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn5.AppearanceCell.Options.UseFont = True
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Ngày"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn5.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn5.FieldName = "Ngay"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.FixedWidth = True
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 139
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Tỷ giá BAC"
        Me.GridColumn6.DisplayFormat.FormatString = "N2"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn6.FieldName = "TyGiaBAC"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 4
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Tỷ giá HQ"
        Me.GridColumn7.DisplayFormat.FormatString = "N2"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "TyGiaHQ"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.pmThem, Me.pmXoa, Me.pmLuuLai, Me.btCapNhatTyGia, Me.BarStaticItem1})
        Me.BarManager1.MaxItemId = 11
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Tools"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.BarStaticItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.btCapNhatTyGia)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Tools"
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BarStaticItem1.Appearance.Options.UseForeColor = True
        Me.BarStaticItem1.Caption = "Tỷ giá được lấy theo tỷ giá của ngân hàng VietCombank"
        Me.BarStaticItem1.Id = 10
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'btCapNhatTyGia
        '
        Me.btCapNhatTyGia.Caption = "Cập nhật tỷ giá"
        Me.btCapNhatTyGia.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btCapNhatTyGia.Id = 9
        Me.btCapNhatTyGia.Name = "btCapNhatTyGia"
        Me.btCapNhatTyGia.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(809, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 483)
        Me.barDockControlBottom.Size = New System.Drawing.Size(809, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 450)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(809, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 450)
        '
        'pmThem
        '
        Me.pmThem.Caption = "Thêm"
        Me.pmThem.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.pmThem.Id = 6
        Me.pmThem.Name = "pmThem"
        '
        'pmXoa
        '
        Me.pmXoa.Caption = "Xóa"
        Me.pmXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.pmXoa.Id = 7
        Me.pmXoa.Name = "pmXoa"
        '
        'pmLuuLai
        '
        Me.pmLuuLai.Caption = "Lưu lại"
        Me.pmLuuLai.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.pmLuuLai.Id = 8
        Me.pmLuuLai.Name = "pmLuuLai"
        '
        'frmTienTe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmTienTe"
        Me.Size = New System.Drawing.Size(809, 483)
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSpinEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents pmThem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pmXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents pmLuuLai As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSpinEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btCapNhatTyGia As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn

End Class
