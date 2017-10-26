<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemNsdCCDC
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
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhichuNSD = New DevExpress.XtraEditors.MemoEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.ceNgayTra = New DevExpress.XtraEditors.CheckEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.deNgayTra = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.deNgayNhan = New DevExpress.XtraEditors.DateEdit()
        Me.glueNSD = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lueChiTietCCDC = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lueCCDC = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.txtGhichuNSD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceNgayTra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayTra.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayTra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayNhan.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayNhan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.glueNSD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueChiTietCCDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueCCDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Thêm mới"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(284, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 261)
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(11, 164)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl3.TabIndex = 101
        Me.LabelControl3.Text = "Ghi chú"
        '
        'txtGhichuNSD
        '
        Me.txtGhichuNSD.Location = New System.Drawing.Point(54, 156)
        Me.txtGhichuNSD.MenuManager = Me.BarManager1
        Me.txtGhichuNSD.Name = "txtGhichuNSD"
        Me.txtGhichuNSD.Size = New System.Drawing.Size(219, 76)
        Me.txtGhichuNSD.TabIndex = 100
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.BarDockControl1)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem2})
        Me.BarManager1.MainMenu = Me.Bar1
        Me.BarManager1.MaxItemId = 1
        '
        'Bar1
        '
        Me.Bar1.BarName = "Main menu"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.MultiLine = True
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Main menu"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Thêm mới"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem2.Id = 0
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(285, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 273)
        Me.barDockControlBottom.Size = New System.Drawing.Size(285, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 247)
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl1.Location = New System.Drawing.Point(285, 26)
        Me.BarDockControl1.Size = New System.Drawing.Size(0, 247)
        '
        'ceNgayTra
        '
        Me.ceNgayTra.Location = New System.Drawing.Point(9, 131)
        Me.ceNgayTra.MenuManager = Me.BarManager1
        Me.ceNgayTra.Name = "ceNgayTra"
        Me.ceNgayTra.Properties.Caption = "Ngày trả"
        Me.ceNgayTra.Size = New System.Drawing.Size(75, 19)
        Me.ceNgayTra.TabIndex = 99
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(198, 243)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 25)
        Me.SimpleButton1.TabIndex = 98
        Me.SimpleButton1.Text = "Lưu"
        '
        'deNgayTra
        '
        Me.deNgayTra.EditValue = Nothing
        Me.deNgayTra.Enabled = False
        Me.deNgayTra.Location = New System.Drawing.Point(90, 131)
        Me.deNgayTra.MenuManager = Me.BarManager1
        Me.deNgayTra.Name = "deNgayTra"
        Me.deNgayTra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgayTra.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deNgayTra.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgayTra.Size = New System.Drawing.Size(100, 20)
        Me.deNgayTra.TabIndex = 97
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(14, 108)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl6.TabIndex = 96
        Me.LabelControl6.Text = "Ngày nhận"
        '
        'deNgayNhan
        '
        Me.deNgayNhan.EditValue = Nothing
        Me.deNgayNhan.Location = New System.Drawing.Point(90, 105)
        Me.deNgayNhan.MenuManager = Me.BarManager1
        Me.deNgayNhan.Name = "deNgayNhan"
        Me.deNgayNhan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgayNhan.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deNgayNhan.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgayNhan.Size = New System.Drawing.Size(100, 20)
        Me.deNgayNhan.TabIndex = 95
        '
        'glueNSD
        '
        Me.glueNSD.EditValue = ""
        Me.glueNSD.Location = New System.Drawing.Point(90, 79)
        Me.glueNSD.Name = "glueNSD"
        Me.glueNSD.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.glueNSD.Properties.DisplayMember = "ten"
        Me.glueNSD.Properties.ImmediatePopup = True
        Me.glueNSD.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.glueNSD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.glueNSD.Properties.ValueMember = "id"
        Me.glueNSD.Properties.View = Me.GridView1
        Me.glueNSD.Size = New System.Drawing.Size(183, 20)
        Me.glueNSD.TabIndex = 94
        '
        'GridView1
        '
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowColumnHeaders = False
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(14, 82)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(70, 13)
        Me.LabelControl2.TabIndex = 93
        Me.LabelControl2.Text = "Người sử dụng"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(14, 56)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl1.TabIndex = 92
        Me.LabelControl1.Text = "Chi tiết CCDC"
        '
        'lueChiTietCCDC
        '
        Me.lueChiTietCCDC.Location = New System.Drawing.Point(90, 53)
        Me.lueChiTietCCDC.Name = "lueChiTietCCDC"
        Me.lueChiTietCCDC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueChiTietCCDC.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenchitietccdc", "Tên")})
        Me.lueChiTietCCDC.Properties.DisplayMember = "tenchitietccdc"
        Me.lueChiTietCCDC.Properties.NullText = ""
        Me.lueChiTietCCDC.Properties.ShowHeader = False
        Me.lueChiTietCCDC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.lueChiTietCCDC.Properties.ValueMember = "id"
        Me.lueChiTietCCDC.Size = New System.Drawing.Size(183, 20)
        Me.lueChiTietCCDC.TabIndex = 91
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(14, 30)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl4.TabIndex = 107
        Me.LabelControl4.Text = "CCDC"
        '
        'lueCCDC
        '
        Me.lueCCDC.Location = New System.Drawing.Point(90, 27)
        Me.lueCCDC.Name = "lueCCDC"
        Me.lueCCDC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.lueCCDC.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ten", "Tên CCDC"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Mã CCDC")})
        Me.lueCCDC.Properties.DisplayMember = "ten"
        Me.lueCCDC.Properties.NullText = "Tất cả"
        Me.lueCCDC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.lueCCDC.Properties.ValueMember = "id"
        Me.lueCCDC.Size = New System.Drawing.Size(183, 20)
        Me.lueCCDC.TabIndex = 106
        '
        'frmThemNsdCCDC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 273)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.lueCCDC)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.txtGhichuNSD)
        Me.Controls.Add(Me.ceNgayTra)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.deNgayTra)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.deNgayNhan)
        Me.Controls.Add(Me.glueNSD)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lueChiTietCCDC)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemNsdCCDC"
        Me.Text = "Người sử dụng CCDC"
        CType(Me.txtGhichuNSD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceNgayTra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayTra.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayTra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayNhan.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayNhan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.glueNSD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueChiTietCCDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueCCDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhichuNSD As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents ceNgayTra As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents deNgayTra As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deNgayNhan As DevExpress.XtraEditors.DateEdit
    Private WithEvents glueNSD As DevExpress.XtraEditors.GridLookUpEdit
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lueChiTietCCDC As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lueCCDC As DevExpress.XtraEditors.LookUpEdit
End Class
