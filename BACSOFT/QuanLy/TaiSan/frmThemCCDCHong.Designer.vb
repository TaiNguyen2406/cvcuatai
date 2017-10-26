<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemCCDCHong
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
        Me.seChiphi = New DevExpress.XtraEditors.SpinEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhichuHongCCDC = New DevExpress.XtraEditors.MemoEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.deNgaySua = New DevExpress.XtraEditors.DateEdit()
        Me.glueNSD = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lueChiTietCCDC = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.lueCCDC = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.seChiphi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichuHongCCDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgaySua.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgaySua.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.glueNSD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueChiTietCCDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueCCDC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'seChiphi
        '
        Me.seChiphi.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seChiphi.Location = New System.Drawing.Point(91, 135)
        Me.seChiphi.MenuManager = Me.BarManager1
        Me.seChiphi.Name = "seChiphi"
        Me.seChiphi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seChiphi.Properties.Mask.EditMask = "c0"
        Me.seChiphi.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.seChiphi.Size = New System.Drawing.Size(100, 20)
        Me.seChiphi.TabIndex = 135
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
        Me.barDockControlTop.Size = New System.Drawing.Size(286, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 276)
        Me.barDockControlBottom.Size = New System.Drawing.Size(286, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 250)
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl1.Location = New System.Drawing.Point(286, 26)
        Me.BarDockControl1.Size = New System.Drawing.Size(0, 250)
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(7, 138)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl4.TabIndex = 134
        Me.LabelControl4.Text = "Chi phí"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(7, 164)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl3.TabIndex = 133
        Me.LabelControl3.Text = "Ghi chú"
        '
        'txtGhichuHongCCDC
        '
        Me.txtGhichuHongCCDC.Location = New System.Drawing.Point(48, 161)
        Me.txtGhichuHongCCDC.MenuManager = Me.BarManager1
        Me.txtGhichuHongCCDC.Name = "txtGhichuHongCCDC"
        Me.txtGhichuHongCCDC.Size = New System.Drawing.Size(226, 76)
        Me.txtGhichuHongCCDC.TabIndex = 132
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(199, 247)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 25)
        Me.SimpleButton1.TabIndex = 131
        Me.SimpleButton1.Text = "Lưu"
        '
        'deNgaySua
        '
        Me.deNgaySua.EditValue = Nothing
        Me.deNgaySua.Location = New System.Drawing.Point(91, 109)
        Me.deNgaySua.MenuManager = Me.BarManager1
        Me.deNgaySua.Name = "deNgaySua"
        Me.deNgaySua.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgaySua.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deNgaySua.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgaySua.Size = New System.Drawing.Size(100, 20)
        Me.deNgaySua.TabIndex = 129
        '
        'glueNSD
        '
        Me.glueNSD.EditValue = ""
        Me.glueNSD.Location = New System.Drawing.Point(91, 83)
        Me.glueNSD.Name = "glueNSD"
        Me.glueNSD.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.glueNSD.Properties.DisplayMember = "ten"
        Me.glueNSD.Properties.ImmediatePopup = True
        Me.glueNSD.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.glueNSD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.glueNSD.Properties.ValueMember = "id"
        Me.glueNSD.Properties.View = Me.GridView1
        Me.glueNSD.Size = New System.Drawing.Size(183, 20)
        Me.glueNSD.TabIndex = 128
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
        Me.LabelControl2.Location = New System.Drawing.Point(7, 86)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(74, 13)
        Me.LabelControl2.TabIndex = 127
        Me.LabelControl2.Text = "Người làm hỏng"
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(7, 112)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl6.TabIndex = 130
        Me.LabelControl6.Text = "Ngày sửa"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(7, 60)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl1.TabIndex = 126
        Me.LabelControl1.Text = "Chi tiết CCDC"
        '
        'lueChiTietCCDC
        '
        Me.lueChiTietCCDC.Location = New System.Drawing.Point(91, 57)
        Me.lueChiTietCCDC.Name = "lueChiTietCCDC"
        Me.lueChiTietCCDC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueChiTietCCDC.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenchitietccdc", "Tên")})
        Me.lueChiTietCCDC.Properties.DisplayMember = "tenchitietccdc"
        Me.lueChiTietCCDC.Properties.NullText = ""
        Me.lueChiTietCCDC.Properties.ShowHeader = False
        Me.lueChiTietCCDC.Properties.ValueMember = "id"
        Me.lueChiTietCCDC.Size = New System.Drawing.Size(183, 20)
        Me.lueChiTietCCDC.TabIndex = 125
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(7, 34)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl5.TabIndex = 141
        Me.LabelControl5.Text = "CCDC"
        '
        'lueCCDC
        '
        Me.lueCCDC.Location = New System.Drawing.Point(91, 31)
        Me.lueCCDC.Name = "lueCCDC"
        Me.lueCCDC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.lueCCDC.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ten", "Tên CCDC"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Model", "Mã CCDC")})
        Me.lueCCDC.Properties.DisplayMember = "ten"
        Me.lueCCDC.Properties.NullText = "Tất cả"
        Me.lueCCDC.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.lueCCDC.Properties.ValueMember = "id"
        Me.lueCCDC.Size = New System.Drawing.Size(183, 20)
        Me.lueCCDC.TabIndex = 140
        '
        'frmThemCCDCHong
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(286, 276)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.lueCCDC)
        Me.Controls.Add(Me.seChiphi)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.txtGhichuHongCCDC)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.deNgaySua)
        Me.Controls.Add(Me.glueNSD)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lueChiTietCCDC)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemCCDCHong"
        Me.Text = "Công cụ, dụng cụ hỏng"
        CType(Me.seChiphi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichuHongCCDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgaySua.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgaySua.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.glueNSD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueChiTietCCDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueCCDC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents seChiphi As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhichuHongCCDC As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents deNgaySua As DevExpress.XtraEditors.DateEdit
    Private WithEvents glueNSD As DevExpress.XtraEditors.GridLookUpEdit
    Private WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lueChiTietCCDC As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lueCCDC As DevExpress.XtraEditors.LookUpEdit
End Class
