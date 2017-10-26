<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMucdich
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
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.lueNhienVatLieu = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.PopupMenu1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.gcMucdich = New DevExpress.XtraGrid.GridControl()
        Me.gvMucdich = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolMucdich = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcolGhichu = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueNhienVatLieu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcMucdich, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvMucdich, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnXoa})
        Me.BarManager1.MaxItemId = 13
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.lueNhienVatLieu, Me.RepositoryItemTextEdit1})
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(415, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 308)
        Me.barDockControlBottom.Size = New System.Drawing.Size(415, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 308)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(415, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 308)
        '
        'btnXoa
        '
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btnXoa.Id = 12
        Me.btnXoa.Name = "btnXoa"
        '
        'lueXe
        '
        Me.lueXe.AutoHeight = False
        Me.lueXe.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueXe.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Tên xe"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Mã xe", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueXe.DisplayMember = "tenxe"
        Me.lueXe.Name = "lueXe"
        Me.lueXe.ValueMember = "id"
        '
        'lueNhienVatLieu
        '
        Me.lueNhienVatLieu.AutoHeight = False
        Me.lueNhienVatLieu.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueNhienVatLieu.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tennhienvatlieu", "Thay thế"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "Mã nvl", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default])})
        Me.lueNhienVatLieu.DisplayMember = "tennhienvatlieu"
        Me.lueNhienVatLieu.Name = "lueNhienVatLieu"
        Me.lueNhienVatLieu.ValueMember = "id"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'PopupMenu1
        '
        Me.PopupMenu1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXoa, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.PopupMenu1.Manager = Me.BarManager1
        Me.PopupMenu1.Name = "PopupMenu1"
        '
        'gcMucdich
        '
        Me.gcMucdich.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcMucdich.Location = New System.Drawing.Point(0, 0)
        Me.gcMucdich.MainView = Me.gvMucdich
        Me.gcMucdich.MenuManager = Me.BarManager1
        Me.gcMucdich.Name = "gcMucdich"
        Me.gcMucdich.Size = New System.Drawing.Size(415, 308)
        Me.gcMucdich.TabIndex = 8
        Me.gcMucdich.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvMucdich})
        '
        'gvMucdich
        '
        Me.gvMucdich.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvMucdich.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvMucdich.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvMucdich.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvMucdich.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gvMucdich.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvMucdich.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvMucdich.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvMucdich.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvMucdich.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvMucdich.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.gcolMucdich, Me.gcolGhichu, Me.GridColumn4})
        Me.gvMucdich.FixedLineWidth = 1
        Me.gvMucdich.GridControl = Me.gcMucdich
        Me.gvMucdich.Name = "gvMucdich"
        Me.gvMucdich.OptionsCustomization.AllowColumnMoving = False
        Me.gvMucdich.OptionsCustomization.AllowGroup = False
        Me.gvMucdich.OptionsLayout.Columns.AddNewColumns = False
        Me.gvMucdich.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvMucdich.OptionsView.EnableAppearanceEvenRow = True
        Me.gvMucdich.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gvMucdich.OptionsView.ShowFooter = True
        Me.gvMucdich.OptionsView.ShowGroupPanel = False
        Me.gvMucdich.RowHeight = 23
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
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 108
        '
        'gcolMucdich
        '
        Me.gcolMucdich.Caption = "Mục đích"
        Me.gcolMucdich.FieldName = "tenmucdich"
        Me.gcolMucdich.Name = "gcolMucdich"
        Me.gcolMucdich.SummaryItem.DisplayFormat = "{0:N0}"
        Me.gcolMucdich.SummaryItem.FieldName = "Ten"
        Me.gcolMucdich.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gcolMucdich.Visible = True
        Me.gcolMucdich.VisibleIndex = 1
        Me.gcolMucdich.Width = 174
        '
        'gcolGhichu
        '
        Me.gcolGhichu.Caption = "Ghi chú"
        Me.gcolGhichu.FieldName = "ghichu"
        Me.gcolGhichu.Name = "gcolGhichu"
        Me.gcolGhichu.Visible = True
        Me.gcolGhichu.VisibleIndex = 2
        Me.gcolGhichu.Width = 115
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "ID"
        Me.GridColumn4.FieldName = "id2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'frmMucdich
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 308)
        Me.Controls.Add(Me.gcMucdich)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.LookAndFeel.SkinName = "Blue"
        Me.Name = "frmMucdich"
        Me.BarManager1.SetPopupContextMenu(Me, Me.PopupMenu1)
        Me.Text = "Mục đích sử dụng"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueNhienVatLieu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcMucdich, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvMucdich, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents lueNhienVatLieu As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents gcMucdich As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvMucdich As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolMucdich As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcolGhichu As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupMenu1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
End Class
