<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemGiayToXe
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.lueLoaiGiayTo = New DevExpress.XtraEditors.LookUpEdit()
        Me.lueXe = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.deNgayBatDau = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.deNgayHetHan = New DevExpress.XtraEditors.DateEdit()
        Me.txtGhichu = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.btnThem = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueLoaiGiayTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayBatDau.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayBatDau.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayHetHan.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayHetHan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(263, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 238)
        Me.barDockControlBottom.Size = New System.Drawing.Size(263, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 212)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(263, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 212)
        '
        'lueLoaiGiayTo
        '
        Me.lueLoaiGiayTo.Location = New System.Drawing.Point(73, 58)
        Me.lueLoaiGiayTo.MenuManager = Me.BarManager1
        Me.lueLoaiGiayTo.Name = "lueLoaiGiayTo"
        Me.lueLoaiGiayTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueLoaiGiayTo.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenloaigiayto", "Tên")})
        Me.lueLoaiGiayTo.Properties.DisplayMember = "tenloaigiayto"
        Me.lueLoaiGiayTo.Properties.ShowHeader = False
        Me.lueLoaiGiayTo.Properties.ValueMember = "id"
        Me.lueLoaiGiayTo.Size = New System.Drawing.Size(179, 20)
        Me.lueLoaiGiayTo.TabIndex = 4
        '
        'lueXe
        '
        Me.lueXe.Location = New System.Drawing.Point(43, 32)
        Me.lueXe.Name = "lueXe"
        Me.lueXe.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueXe.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "id", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenxe", "Tên")})
        Me.lueXe.Properties.DisplayMember = "tenxe"
        Me.lueXe.Properties.ShowHeader = False
        Me.lueXe.Properties.ValueMember = "id"
        Me.lueXe.Size = New System.Drawing.Size(209, 20)
        Me.lueXe.TabIndex = 141
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(12, 35)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(12, 13)
        Me.LabelControl6.TabIndex = 142
        Me.LabelControl6.Text = "Xe"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 61)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl1.TabIndex = 143
        Me.LabelControl1.Text = "Loại giấy tờ"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 87)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl2.TabIndex = 145
        Me.LabelControl2.Text = "Ngày bắt đầu"
        '
        'deNgayBatDau
        '
        Me.deNgayBatDau.EditValue = Nothing
        Me.deNgayBatDau.Location = New System.Drawing.Point(91, 84)
        Me.deNgayBatDau.Name = "deNgayBatDau"
        Me.deNgayBatDau.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgayBatDau.Properties.EditFormat.FormatString = "yyyy/MM/dd"
        Me.deNgayBatDau.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deNgayBatDau.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deNgayBatDau.Properties.Mask.IgnoreMaskBlank = False
        Me.deNgayBatDau.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deNgayBatDau.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgayBatDau.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm"
        Me.deNgayBatDau.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = True
        Me.deNgayBatDau.Size = New System.Drawing.Size(161, 20)
        Me.deNgayBatDau.TabIndex = 144
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 113)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl3.TabIndex = 147
        Me.LabelControl3.Text = "Ngày hết hạn"
        '
        'deNgayHetHan
        '
        Me.deNgayHetHan.EditValue = Nothing
        Me.deNgayHetHan.Location = New System.Drawing.Point(91, 110)
        Me.deNgayHetHan.Name = "deNgayHetHan"
        Me.deNgayHetHan.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgayHetHan.Properties.EditFormat.FormatString = "yyyy/MM/dd"
        Me.deNgayHetHan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deNgayHetHan.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.deNgayHetHan.Properties.Mask.IgnoreMaskBlank = False
        Me.deNgayHetHan.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deNgayHetHan.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.deNgayHetHan.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.deNgayHetHan.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgayHetHan.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm"
        Me.deNgayHetHan.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = True
        Me.deNgayHetHan.Size = New System.Drawing.Size(161, 20)
        Me.deNgayHetHan.TabIndex = 146
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(55, 136)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Size = New System.Drawing.Size(197, 56)
        Me.txtGhichu.TabIndex = 149
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(12, 139)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl4.TabIndex = 148
        Me.LabelControl4.Text = "Ghi chú"
        '
        'btnThem
        '
        Me.btnThem.Location = New System.Drawing.Point(176, 198)
        Me.btnThem.Name = "btnThem"
        Me.btnThem.Size = New System.Drawing.Size(75, 33)
        Me.btnThem.TabIndex = 150
        Me.btnThem.Text = "Lưu"
        '
        'frmThemGiayToXe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(263, 238)
        Me.Controls.Add(Me.btnThem)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.deNgayHetHan)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.deNgayBatDau)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.lueXe)
        Me.Controls.Add(Me.lueLoaiGiayTo)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmThemGiayToXe"
        Me.Text = "Thêm giấy tờ xe"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueLoaiGiayTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayBatDau.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayBatDau.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayHetHan.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayHetHan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents lueLoaiGiayTo As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lueXe As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deNgayHetHan As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deNgayBatDau As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnThem As DevExpress.XtraEditors.SimpleButton
End Class
