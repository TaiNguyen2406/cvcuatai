<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemBaoduongxe
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
        Me.btnThem = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.lueXebd = New DevExpress.XtraEditors.LookUpEdit()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.glueNSD = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.gridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.nudChiphi = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpNgaySuaChua = New DevExpress.XtraEditors.DateEdit()
        Me.seSoKmKhiBD = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.lueViTri = New DevExpress.XtraEditors.LookUpEdit()
        Me.ckeNhapCu = New DevExpress.XtraEditors.CheckEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.lueXebd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.glueNSD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudChiphi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpNgaySuaChua.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpNgaySuaChua.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seSoKmKhiBD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueViTri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ckeNhapCu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnThem
        '
        Me.btnThem.Location = New System.Drawing.Point(293, 234)
        Me.btnThem.Name = "btnThem"
        Me.btnThem.Size = New System.Drawing.Size(75, 33)
        Me.btnThem.TabIndex = 69
        Me.btnThem.Text = "Lưu"
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(9, 42)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(12, 13)
        Me.LabelControl6.TabIndex = 141
        Me.LabelControl6.Text = "Xe"
        '
        'lueXebd
        '
        Me.lueXebd.Location = New System.Drawing.Point(52, 39)
        Me.lueXebd.Name = "lueXebd"
        Me.lueXebd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueXebd.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Tên")})
        Me.lueXebd.Properties.DisplayMember = "tenxe"
        Me.lueXebd.Properties.ShowHeader = False
        Me.lueXebd.Properties.ValueMember = "id"
        Me.lueXebd.Size = New System.Drawing.Size(209, 20)
        Me.lueXebd.TabIndex = 140
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(55, 169)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Size = New System.Drawing.Size(316, 59)
        Me.txtGhichu.TabIndex = 139
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(229, 120)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl4.TabIndex = 134
        Me.LabelControl4.Text = "Chi phí"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(6, 146)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(76, 13)
        Me.LabelControl5.TabIndex = 138
        Me.LabelControl5.Text = "Người thực hiện"
        '
        'glueNSD
        '
        Me.glueNSD.Location = New System.Drawing.Point(88, 143)
        Me.glueNSD.Name = "glueNSD"
        Me.glueNSD.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.glueNSD.Properties.DisplayMember = "ten"
        Me.glueNSD.Properties.ImmediatePopup = True
        Me.glueNSD.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.glueNSD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.glueNSD.Properties.ValueMember = "id"
        Me.glueNSD.Properties.View = Me.gridLookUpEdit1View
        Me.glueNSD.Size = New System.Drawing.Size(201, 20)
        Me.glueNSD.TabIndex = 137
        '
        'gridLookUpEdit1View
        '
        Me.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.gridLookUpEdit1View.Name = "gridLookUpEdit1View"
        Me.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = False
        Me.gridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'nudChiphi
        '
        Me.nudChiphi.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.nudChiphi.Location = New System.Drawing.Point(268, 117)
        Me.nudChiphi.Name = "nudChiphi"
        Me.nudChiphi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.nudChiphi.Properties.Mask.EditMask = "c0"
        Me.nudChiphi.Size = New System.Drawing.Size(100, 20)
        Me.nudChiphi.TabIndex = 136
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(9, 171)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl3.TabIndex = 133
        Me.LabelControl3.Text = "Ghi chú"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(6, 120)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(73, 13)
        Me.LabelControl2.TabIndex = 132
        Me.LabelControl2.Text = "Ngày thực hiện"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(9, 94)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl1.TabIndex = 131
        Me.LabelControl1.Text = "Vị trí"
        '
        'dtpNgaySuaChua
        '
        Me.dtpNgaySuaChua.EditValue = Nothing
        Me.dtpNgaySuaChua.Location = New System.Drawing.Point(88, 117)
        Me.dtpNgaySuaChua.Name = "dtpNgaySuaChua"
        Me.dtpNgaySuaChua.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpNgaySuaChua.Properties.EditFormat.FormatString = "yyyy/MM/dd"
        Me.dtpNgaySuaChua.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.dtpNgaySuaChua.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.dtpNgaySuaChua.Properties.Mask.IgnoreMaskBlank = False
        Me.dtpNgaySuaChua.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dtpNgaySuaChua.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpNgaySuaChua.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpNgaySuaChua.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtpNgaySuaChua.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm"
        Me.dtpNgaySuaChua.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = True
        Me.dtpNgaySuaChua.Size = New System.Drawing.Size(124, 20)
        Me.dtpNgaySuaChua.TabIndex = 130
        '
        'seSoKmKhiBD
        '
        Me.seSoKmKhiBD.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seSoKmKhiBD.Location = New System.Drawing.Point(81, 65)
        Me.seSoKmKhiBD.Name = "seSoKmKhiBD"
        Me.seSoKmKhiBD.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seSoKmKhiBD.Size = New System.Drawing.Size(100, 20)
        Me.seSoKmKhiBD.TabIndex = 146
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(9, 68)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl7.TabIndex = 147
        Me.LabelControl7.Text = "Số Km khi BD"
        '
        'lueViTri
        '
        Me.lueViTri.Location = New System.Drawing.Point(52, 91)
        Me.lueViTri.Name = "lueViTri"
        Me.lueViTri.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueViTri.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tennvl", "Tên")})
        Me.lueViTri.Properties.DisplayMember = "tennvl"
        Me.lueViTri.Properties.NullText = "Chưa có vị trí nào cần bảo dưỡng"
        Me.lueViTri.Properties.ShowHeader = False
        Me.lueViTri.Properties.ValueMember = "id"
        Me.lueViTri.Size = New System.Drawing.Size(209, 20)
        Me.lueViTri.TabIndex = 152
        '
        'ckeNhapCu
        '
        Me.ckeNhapCu.Location = New System.Drawing.Point(266, 91)
        Me.ckeNhapCu.Name = "ckeNhapCu"
        Me.ckeNhapCu.Properties.Caption = "Nhập cũ"
        Me.ckeNhapCu.Size = New System.Drawing.Size(75, 19)
        Me.ckeNhapCu.TabIndex = 157
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 1
        '
        'Bar2
        '
        Me.Bar2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar2.Appearance.Options.UseFont = True
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
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Thêm mới"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(380, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 276)
        Me.barDockControlBottom.Size = New System.Drawing.Size(380, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 250)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(380, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 250)
        '
        'frmThemBaoduongxe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 276)
        Me.Controls.Add(Me.ckeNhapCu)
        Me.Controls.Add(Me.lueViTri)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.seSoKmKhiBD)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.lueXebd)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.glueNSD)
        Me.Controls.Add(Me.nudChiphi)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.dtpNgaySuaChua)
        Me.Controls.Add(Me.btnThem)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemBaoduongxe"
        Me.Text = "Thêm bảo dưỡng xe"
        CType(Me.lueXebd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.glueNSD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudChiphi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpNgaySuaChua.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpNgaySuaChua.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seSoKmKhiBD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueViTri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ckeNhapCu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lueXebd As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Private WithEvents glueNSD As DevExpress.XtraEditors.GridLookUpEdit
    Private WithEvents gridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents nudChiphi As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpNgaySuaChua As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seSoKmKhiBD As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents lueViTri As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents ckeNhapCu As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
