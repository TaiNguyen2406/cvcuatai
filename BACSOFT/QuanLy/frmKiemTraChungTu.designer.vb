<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKiemTraChungTu
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
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
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.dtTuNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.dtDenNgay = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btnTaiDS = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarEditItem1 = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BarListItem1 = New DevExpress.XtraBars.BarListItem()
        Me.BarSubItem1 = New DevExpress.XtraBars.BarSubItem()
        Me.BarListItem2 = New DevExpress.XtraBars.BarListItem()
        Me.BarListItem3 = New DevExpress.XtraBars.BarListItem()
        Me.BarToolbarsListItem1 = New DevExpress.XtraBars.BarToolbarsListItem()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbNhanVien = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbNhanVien, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarEditItem1, Me.dtTuNgay, Me.dtDenNgay, Me.btnTaiDS, Me.BarListItem1, Me.BarSubItem1, Me.BarListItem2, Me.BarListItem3, Me.BarToolbarsListItem1})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 9
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositoryItemDateEdit1, Me.RepositoryItemDateEdit2})
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.dtTuNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.dtDenNgay, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnTaiDS, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'dtTuNgay
        '
        Me.dtTuNgay.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTuNgay.Appearance.Options.UseFont = True
        Me.dtTuNgay.Caption = "Từ ngày"
        Me.dtTuNgay.Edit = Me.RepositoryItemDateEdit1
        Me.dtTuNgay.Id = 1
        Me.dtTuNgay.Name = "dtTuNgay"
        Me.dtTuNgay.Width = 102
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'dtDenNgay
        '
        Me.dtDenNgay.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtDenNgay.Appearance.Options.UseFont = True
        Me.dtDenNgay.Caption = "Đến ngày"
        Me.dtDenNgay.Edit = Me.RepositoryItemDateEdit2
        Me.dtDenNgay.Id = 2
        Me.dtDenNgay.Name = "dtDenNgay"
        Me.dtDenNgay.Width = 110
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = False
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btnTaiDS
        '
        Me.btnTaiDS.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTaiDS.Appearance.Options.UseFont = True
        Me.btnTaiDS.Caption = "Tải danh sách"
        Me.btnTaiDS.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btnTaiDS.Id = 3
        Me.btnTaiDS.Name = "btnTaiDS"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1022, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 501)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1022, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 475)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1022, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 475)
        '
        'BarEditItem1
        '
        Me.BarEditItem1.Caption = "BarEditItem1"
        Me.BarEditItem1.Edit = Me.RepositoryItemTextEdit1
        Me.BarEditItem1.Id = 0
        Me.BarEditItem1.Name = "BarEditItem1"
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'BarListItem1
        '
        Me.BarListItem1.Caption = "BarListItem1"
        Me.BarListItem1.Id = 4
        Me.BarListItem1.Name = "BarListItem1"
        '
        'BarSubItem1
        '
        Me.BarSubItem1.Caption = "BarSubItem1"
        Me.BarSubItem1.Id = 5
        Me.BarSubItem1.Name = "BarSubItem1"
        '
        'BarListItem2
        '
        Me.BarListItem2.Caption = "BarListItem2"
        Me.BarListItem2.Id = 6
        Me.BarListItem2.Name = "BarListItem2"
        '
        'BarListItem3
        '
        Me.BarListItem3.Caption = "BarListItem3"
        Me.BarListItem3.Id = 7
        Me.BarListItem3.Name = "BarListItem3"
        '
        'BarToolbarsListItem1
        '
        Me.BarToolbarsListItem1.Caption = "BarToolbarsListItem1"
        Me.BarToolbarsListItem1.Id = 8
        Me.BarToolbarsListItem1.Name = "BarToolbarsListItem1"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 26)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbNhanVien})
        Me.gdv.Size = New System.Drawing.Size(1022, 475)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn3, Me.GridColumn5, Me.GridColumn1, Me.GridColumn2})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "STT"
        Me.GridColumn4.FieldName = "STT"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nhân viên"
        Me.GridColumn3.ColumnEdit = Me.rcbNhanVien
        Me.GridColumn3.FieldName = "IDNguoiDat"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 200
        '
        'rcbNhanVien
        '
        Me.rcbNhanVien.AutoHeight = False
        Me.rcbNhanVien.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbNhanVien.DisplayMember = "Ten"
        Me.rcbNhanVien.Name = "rcbNhanVien"
        Me.rcbNhanVien.NullText = ""
        Me.rcbNhanVien.ValueMember = "ID"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Hình thức CT"
        Me.GridColumn5.FieldName = "IDHinhThucCT"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 120
        '
        'Timer1
        '
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Số phiếu"
        Me.GridColumn1.FieldName = "Sophieu"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 100
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Sô CT"
        Me.GridColumn2.FieldName = "CoCT"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 4
        Me.GridColumn2.Width = 100
        '
        'frmKiemTraChungTu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmKiemTraChungTu"
        Me.Size = New System.Drawing.Size(1022, 501)
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbNhanVien, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents dtTuNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents dtDenNgay As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BarEditItem1 As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents btnTaiDS As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbNhanVien As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Timer1 As Timer
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarListItem2 As DevExpress.XtraBars.BarListItem
    Friend WithEvents BarListItem1 As DevExpress.XtraBars.BarListItem
    Friend WithEvents BarSubItem1 As DevExpress.XtraBars.BarSubItem
    Friend WithEvents BarListItem3 As DevExpress.XtraBars.BarListItem
    Friend WithEvents BarToolbarsListItem1 As DevExpress.XtraBars.BarToolbarsListItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
