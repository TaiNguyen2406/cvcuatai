<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThongkeNguoiSudung
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
        Me.gcNguoiSudung = New DevExpress.XtraGrid.GridControl()
        Me.gvNguoiSudung = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolten = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolSolanMuon = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTongsokm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDeTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.barDeDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.BarEditItem2 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.barCbbXem = New DevExpress.XtraBars.BarEditItem()
        Me.riCbbXem = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.barGlueNguoiSd = New DevExpress.XtraBars.BarEditItem()
        Me.riGlueNSD = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.AlertControl1 = New DevExpress.XtraBars.Alerter.AlertControl(Me.components)
        CType(Me.gcNguoiSudung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvNguoiSudung, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbXem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcNguoiSudung
        '
        Me.gcNguoiSudung.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcNguoiSudung.Location = New System.Drawing.Point(0, 26)
        Me.gcNguoiSudung.MainView = Me.gvNguoiSudung
        Me.gcNguoiSudung.Name = "gcNguoiSudung"
        Me.gcNguoiSudung.Size = New System.Drawing.Size(1085, 317)
        Me.gcNguoiSudung.TabIndex = 70
        Me.gcNguoiSudung.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvNguoiSudung})
        '
        'gvNguoiSudung
        '
        Me.gvNguoiSudung.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvNguoiSudung.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvNguoiSudung.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvNguoiSudung.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvNguoiSudung.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvNguoiSudung.Appearance.GroupFooter.Options.UseFont = True
        Me.gvNguoiSudung.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvNguoiSudung.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvNguoiSudung.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvNguoiSudung.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvNguoiSudung.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvNguoiSudung.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvNguoiSudung.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolID, Me.gcolten, Me.gcolSolanMuon, Me.gcolTongsokm})
        Me.gvNguoiSudung.GridControl = Me.gcNguoiSudung
        Me.gvNguoiSudung.Name = "gvNguoiSudung"
        Me.gvNguoiSudung.OptionsBehavior.Editable = False
        Me.gvNguoiSudung.OptionsCustomization.AllowColumnMoving = False
        Me.gvNguoiSudung.OptionsCustomization.AllowGroup = False
        Me.gvNguoiSudung.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvNguoiSudung.OptionsView.EnableAppearanceEvenRow = True
        Me.gvNguoiSudung.OptionsView.ShowFooter = True
        Me.gvNguoiSudung.OptionsView.ShowGroupPanel = False
        Me.gvNguoiSudung.OptionsView.ShowIndicator = False
        Me.gvNguoiSudung.RowHeight = 23
        '
        'gcolID
        '
        Me.gcolID.Caption = "ID người sử dụng"
        Me.gcolID.FieldName = "id"
        Me.gcolID.Name = "gcolID"
        '
        'gcolten
        '
        Me.gcolten.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gcolten.AppearanceCell.Options.UseFont = True
        Me.gcolten.Caption = "Người sử dụng"
        Me.gcolten.FieldName = "ten"
        Me.gcolten.Name = "gcolten"
        Me.gcolten.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolten.Visible = True
        Me.gcolten.VisibleIndex = 0
        '
        'gcolSolanMuon
        '
        Me.gcolSolanMuon.AppearanceCell.Options.UseTextOptions = True
        Me.gcolSolanMuon.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolSolanMuon.Caption = "Số lần mượn"
        Me.gcolSolanMuon.FieldName = "solanmuon"
        Me.gcolSolanMuon.Name = "gcolSolanMuon"
        Me.gcolSolanMuon.Visible = True
        Me.gcolSolanMuon.VisibleIndex = 1
        '
        'gcolTongsokm
        '
        Me.gcolTongsokm.AppearanceCell.Options.UseTextOptions = True
        Me.gcolTongsokm.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gcolTongsokm.Caption = "Tổng số Km"
        Me.gcolTongsokm.FieldName = "tongsokm"
        Me.gcolTongsokm.Name = "gcolTongsokm"
        Me.gcolTongsokm.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.gcolTongsokm.Visible = True
        Me.gcolTongsokm.VisibleIndex = 2
        '
        'deTuNgay
        '
        Me.deTuNgay.AutoHeight = False
        Me.deTuNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deTuNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.deTuNgay.Mask.UseMaskAsDisplayFormat = True
        Me.deTuNgay.Name = "deTuNgay"
        Me.deTuNgay.NullText = "dd/MM/yyyy"
        Me.deTuNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barDeTuNgay
        '
        Me.barDeTuNgay.Caption = "Từ ngày"
        Me.barDeTuNgay.Edit = Me.deTuNgay
        Me.barDeTuNgay.Id = 13
        Me.barDeTuNgay.Name = "barDeTuNgay"
        Me.barDeTuNgay.Width = 109
        '
        'barDeDenNgay
        '
        Me.barDeDenNgay.Caption = "Đến ngày"
        Me.barDeDenNgay.Edit = Me.deDenNgay
        Me.barDeDenNgay.Id = 16
        Me.barDeDenNgay.Name = "barDeDenNgay"
        Me.barDeDenNgay.Width = 113
        '
        'deDenNgay
        '
        Me.deDenNgay.AutoHeight = False
        Me.deDenNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDenNgay.Mask.EditMask = "dd/MM/yyyy"
        Me.deDenNgay.Mask.UseMaskAsDisplayFormat = True
        Me.deDenNgay.Name = "deDenNgay"
        Me.deDenNgay.NullText = "dd/MM/yyyy"
        Me.deDenNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 317)
        '
        'BarEditItem2
        '
        Me.BarEditItem2.Caption = "BarEditItem2"
        Me.BarEditItem2.Edit = Me.RepositoryItemDateEdit2
        Me.BarEditItem2.Id = 15
        Me.BarEditItem2.Name = "BarEditItem2"
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1085, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 317)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 343)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1085, 0)
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1085, 26)
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCbbXem, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barGlueNguoiSd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, "", False, True, False, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'barCbbXem
        '
        Me.barCbbXem.Caption = "Xem"
        Me.barCbbXem.Edit = Me.riCbbXem
        Me.barCbbXem.Id = 23
        Me.barCbbXem.Name = "barCbbXem"
        Me.barCbbXem.Width = 75
        '
        'riCbbXem
        '
        Me.riCbbXem.AutoHeight = False
        Me.riCbbXem.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riCbbXem.Items.AddRange(New Object() {"Top 500", "Tất cả", "Tuỳ chỉnh"})
        Me.riCbbXem.Name = "riCbbXem"
        '
        'barGlueNguoiSd
        '
        Me.barGlueNguoiSd.Caption = "Người sd"
        Me.barGlueNguoiSd.Edit = Me.riGlueNSD
        Me.barGlueNguoiSd.Id = 22
        Me.barGlueNguoiSd.Name = "barGlueNguoiSd"
        Me.barGlueNguoiSd.Width = 163
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
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarButtonItem2.Appearance.Options.UseFont = True
        Me.BarButtonItem2.Caption = "Tải lại"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem2.Id = 20
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Excel"
        Me.BarButtonItem3.Id = 21
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barDeTuNgay, Me.BarEditItem2, Me.barDeDenNgay, Me.BarButtonItem2, Me.BarButtonItem3, Me.barGlueNguoiSd, Me.barCbbXem})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 24
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.deTuNgay, Me.deDenNgay, Me.riGlueNSD, Me.riCbbXem})
        '
        'frmThongkeNguoiSudung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1085, 343)
        Me.Controls.Add(Me.gcNguoiSudung)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmThongkeNguoiSudung"
        Me.Text = "Thống kê người sử dụng"
        CType(Me.gcNguoiSudung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvNguoiSudung, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbXem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riGlueNSD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcNguoiSudung As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvNguoiSudung As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcolID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolten As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolSolanMuon As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTongsokm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barDeTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents barDeDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarEditItem2 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents AlertControl1 As DevExpress.XtraBars.Alerter.AlertControl
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barGlueNguoiSd As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riGlueNSD As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents barCbbXem As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riCbbXem As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
End Class
