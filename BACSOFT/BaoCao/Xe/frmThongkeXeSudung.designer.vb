<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThongkeXeSudung
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
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.gcDanhsach = New DevExpress.XtraGrid.GridControl()
        Me.gvDanhsach = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcolID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTenxe = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBienso = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolSolan = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolTongsokm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.barCbbXem = New DevExpress.XtraBars.BarEditItem()
        Me.riCbbXe = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.barDeTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deTuNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barDeDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.deDenNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.barLueXe = New DevExpress.XtraBars.BarEditItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarEditItem2 = New DevExpress.XtraBars.BarEditItem()
        Me.AlertControl1 = New DevExpress.XtraBars.Alerter.AlertControl(Me.components)
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcDanhsach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvDanhsach, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riCbbXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'gcDanhsach
        '
        Me.gcDanhsach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcDanhsach.Location = New System.Drawing.Point(0, 26)
        Me.gcDanhsach.MainView = Me.gvDanhsach
        Me.gcDanhsach.Name = "gcDanhsach"
        Me.gcDanhsach.Size = New System.Drawing.Size(911, 350)
        Me.gcDanhsach.TabIndex = 69
        Me.gcDanhsach.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvDanhsach})
        '
        'gvDanhsach
        '
        Me.gvDanhsach.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvDanhsach.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvDanhsach.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvDanhsach.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvDanhsach.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvDanhsach.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvDanhsach.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvDanhsach.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvDanhsach.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcolID, Me.gcolTenxe, Me.colBienso, Me.gcolSolan, Me.gcolTongsokm})
        Me.gvDanhsach.GridControl = Me.gcDanhsach
        Me.gvDanhsach.Name = "gvDanhsach"
        Me.gvDanhsach.OptionsBehavior.Editable = False
        Me.gvDanhsach.OptionsCustomization.AllowColumnMoving = False
        Me.gvDanhsach.OptionsCustomization.AllowGroup = False
        Me.gvDanhsach.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvDanhsach.OptionsView.EnableAppearanceEvenRow = True
        Me.gvDanhsach.OptionsView.ShowFooter = True
        Me.gvDanhsach.OptionsView.ShowGroupPanel = False
        Me.gvDanhsach.RowHeight = 23
        '
        'gcolID
        '
        Me.gcolID.Caption = "ID xe"
        Me.gcolID.FieldName = "id"
        Me.gcolID.Name = "gcolID"
        '
        'gcolTenxe
        '
        Me.gcolTenxe.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gcolTenxe.AppearanceCell.Options.UseFont = True
        Me.gcolTenxe.Caption = "Tên xe"
        Me.gcolTenxe.FieldName = "tenxe"
        Me.gcolTenxe.Name = "gcolTenxe"
        Me.gcolTenxe.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolTenxe.Visible = True
        Me.gcolTenxe.VisibleIndex = 0
        '
        'colBienso
        '
        Me.colBienso.Caption = "Biển số"
        Me.colBienso.FieldName = "bienso"
        Me.colBienso.Name = "colBienso"
        Me.colBienso.Visible = True
        Me.colBienso.VisibleIndex = 1
        '
        'gcolSolan
        '
        Me.gcolSolan.Caption = "Số lần đi"
        Me.gcolSolan.FieldName = "solan"
        Me.gcolSolan.Name = "gcolSolan"
        Me.gcolSolan.Visible = True
        Me.gcolSolan.VisibleIndex = 2
        '
        'gcolTongsokm
        '
        Me.gcolTongsokm.Caption = "Tổng số Km"
        Me.gcolTongsokm.FieldName = "tongso"
        Me.gcolTongsokm.Name = "gcolTongsokm"
        Me.gcolTongsokm.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.gcolTongsokm.Visible = True
        Me.gcolTongsokm.VisibleIndex = 3
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.barLueXe, Me.barDeTuNgay, Me.BarEditItem2, Me.barDeDenNgay, Me.BarButtonItem2, Me.barCbbXem})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 22
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.deTuNgay, Me.deDenNgay, Me.riCbbXe})
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barCbbXem, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barDeDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.barLueXe, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'barCbbXem
        '
        Me.barCbbXem.Caption = "Xem"
        Me.barCbbXem.Edit = Me.riCbbXe
        Me.barCbbXem.Id = 21
        Me.barCbbXem.Name = "barCbbXem"
        Me.barCbbXem.Width = 75
        '
        'riCbbXe
        '
        Me.riCbbXe.AutoHeight = False
        Me.riCbbXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riCbbXe.Items.AddRange(New Object() {"Top 500", "Tất cả", "Tuỳ chỉnh"})
        Me.riCbbXe.Name = "riCbbXe"
        '
        'barDeTuNgay
        '
        Me.barDeTuNgay.Caption = "Từ ngày"
        Me.barDeTuNgay.Edit = Me.deTuNgay
        Me.barDeTuNgay.Id = 13
        Me.barDeTuNgay.Name = "barDeTuNgay"
        Me.barDeTuNgay.Width = 109
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
        'barLueXe
        '
        Me.barLueXe.Caption = "Xe"
        Me.barLueXe.Edit = Me.lueXe
        Me.barLueXe.Id = 12
        Me.barLueXe.Name = "barLueXe"
        Me.barLueXe.Width = 194
        '
        'lueXe
        '
        Me.lueXe.AutoHeight = False
        Me.lueXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.lueXe.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Tên xe"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Mã xe", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueXe.DisplayMember = "tenxe"
        Me.lueXe.Name = "lueXe"
        Me.lueXe.NullText = "[Tất cả]"
        Me.lueXe.ValueMember = "id"
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
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(911, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 376)
        Me.barDockControlBottom.Size = New System.Drawing.Size(911, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 350)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(911, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 350)
        '
        'BarEditItem2
        '
        Me.BarEditItem2.Caption = "BarEditItem2"
        Me.BarEditItem2.Edit = Me.RepositoryItemDateEdit2
        Me.BarEditItem2.Id = 15
        Me.BarEditItem2.Name = "BarEditItem2"
        '
        'frmThongkeXeSudung
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(911, 376)
        Me.Controls.Add(Me.gcDanhsach)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.LookAndFeel.SkinName = "Blue"
        Me.Name = "frmThongkeXeSudung"
        Me.Text = "Thống kê Xe sử dụng"
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcDanhsach, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvDanhsach, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riCbbXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTuNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDenNgay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcDanhsach As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvDanhsach As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcolID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTenxe As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBienso As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolSolan As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolTongsokm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents AlertControl1 As DevExpress.XtraBars.Alerter.AlertControl
    Friend WithEvents barLueXe As DevExpress.XtraBars.BarEditItem
    Friend WithEvents barDeTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deTuNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents barDeDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents deDenNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BarEditItem2 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barCbbXem As DevExpress.XtraBars.BarEditItem
    Friend WithEvents riCbbXe As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
End Class
