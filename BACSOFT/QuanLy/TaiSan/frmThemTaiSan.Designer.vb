<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemTaiSan
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
        Me.txtTenTS = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.lueLoaiTS = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.seDonGia = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhiChuTS = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.deNgayNhap = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.lueTinhTrangTS = New DevExpress.XtraEditors.LookUpEdit()
        Me.seSoLuong = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.seMucKH = New DevExpress.XtraEditors.SpinEdit()
        Me.seTGKH = New DevExpress.XtraEditors.SpinEdit()
        Me.seTongGia = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl1 = New DevExpress.XtraBars.BarDockControl()
        CType(Me.txtTenTS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueLoaiTS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seDonGia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhiChuTS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayNhap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deNgayNhap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueTinhTrangTS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seSoLuong.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seMucKH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seTGKH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.seTongGia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTenTS
        '
        Me.txtTenTS.Location = New System.Drawing.Point(67, 31)
        Me.txtTenTS.Name = "txtTenTS"
        Me.txtTenTS.Size = New System.Drawing.Size(207, 20)
        Me.txtTenTS.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(7, 35)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Tên tài sản"
        '
        'lueLoaiTS
        '
        Me.lueLoaiTS.Location = New System.Drawing.Point(67, 57)
        Me.lueLoaiTS.Name = "lueLoaiTS"
        Me.lueLoaiTS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueLoaiTS.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenloaitaisan", "Tên")})
        Me.lueLoaiTS.Properties.DisplayMember = "tenloaitaisan"
        Me.lueLoaiTS.Properties.NullText = ""
        Me.lueLoaiTS.Properties.ShowHeader = False
        Me.lueLoaiTS.Properties.ValueMember = "id"
        Me.lueLoaiTS.Size = New System.Drawing.Size(121, 20)
        Me.lueLoaiTS.TabIndex = 4
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(7, 60)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "Loại tài sản"
        '
        'seDonGia
        '
        Me.seDonGia.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seDonGia.Location = New System.Drawing.Point(67, 83)
        Me.seDonGia.Name = "seDonGia"
        Me.seDonGia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seDonGia.Properties.Mask.EditMask = "c0"
        Me.seDonGia.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.seDonGia.Size = New System.Drawing.Size(121, 20)
        Me.seDonGia.TabIndex = 6
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(7, 86)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Đơn giá"
        '
        'txtGhiChuTS
        '
        Me.txtGhiChuTS.Location = New System.Drawing.Point(62, 161)
        Me.txtGhiChuTS.Name = "txtGhiChuTS"
        Me.txtGhiChuTS.Size = New System.Drawing.Size(291, 46)
        Me.txtGhiChuTS.TabIndex = 8
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(9, 164)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl5.TabIndex = 9
        Me.LabelControl5.Text = "Ghi chú"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(275, 213)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 32)
        Me.SimpleButton1.TabIndex = 10
        Me.SimpleButton1.Text = "Lưu"
        '
        'deNgayNhap
        '
        Me.deNgayNhap.EditValue = Nothing
        Me.deNgayNhap.Location = New System.Drawing.Point(253, 57)
        Me.deNgayNhap.Name = "deNgayNhap"
        Me.deNgayNhap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deNgayNhap.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deNgayNhap.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deNgayNhap.Size = New System.Drawing.Size(100, 20)
        Me.deNgayNhap.TabIndex = 15
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(194, 60)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl6.TabIndex = 16
        Me.LabelControl6.Text = "Ngày nhập"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(194, 112)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(49, 13)
        Me.LabelControl7.TabIndex = 22
        Me.LabelControl7.Text = "Tình trạng"
        '
        'lueTinhTrangTS
        '
        Me.lueTinhTrangTS.Location = New System.Drawing.Point(253, 109)
        Me.lueTinhTrangTS.Name = "lueTinhTrangTS"
        Me.lueTinhTrangTS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lueTinhTrangTS.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentinhtrang", "Tên")})
        Me.lueTinhTrangTS.Properties.DisplayMember = "tentinhtrang"
        Me.lueTinhTrangTS.Properties.NullText = ""
        Me.lueTinhTrangTS.Properties.ShowHeader = False
        Me.lueTinhTrangTS.Properties.ValueMember = "id"
        Me.lueTinhTrangTS.Size = New System.Drawing.Size(100, 20)
        Me.lueTinhTrangTS.TabIndex = 23
        '
        'seSoLuong
        '
        Me.seSoLuong.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seSoLuong.Location = New System.Drawing.Point(253, 83)
        Me.seSoLuong.Name = "seSoLuong"
        Me.seSoLuong.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seSoLuong.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.seSoLuong.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seSoLuong.Size = New System.Drawing.Size(100, 20)
        Me.seSoLuong.TabIndex = 28
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(194, 86)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl8.TabIndex = 29
        Me.LabelControl8.Text = "Số lượng"
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(7, 138)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(68, 13)
        Me.LabelControl9.TabIndex = 35
        Me.LabelControl9.Text = "Mức KH(ngày)"
        '
        'seMucKH
        '
        Me.seMucKH.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seMucKH.Enabled = False
        Me.seMucKH.Location = New System.Drawing.Point(81, 135)
        Me.seMucKH.Name = "seMucKH"
        Me.seMucKH.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seMucKH.Properties.Mask.EditMask = "c0"
        Me.seMucKH.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.seMucKH.Size = New System.Drawing.Size(107, 20)
        Me.seMucKH.TabIndex = 34
        '
        'seTGKH
        '
        Me.seTGKH.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seTGKH.Location = New System.Drawing.Point(259, 135)
        Me.seTGKH.Name = "seTGKH"
        Me.seTGKH.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seTGKH.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.seTGKH.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.seTGKH.Size = New System.Drawing.Size(94, 20)
        Me.seTGKH.TabIndex = 36
        '
        'seTongGia
        '
        Me.seTongGia.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.seTongGia.Enabled = False
        Me.seTongGia.Location = New System.Drawing.Point(67, 109)
        Me.seTongGia.Name = "seTongGia"
        Me.seTongGia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seTongGia.Properties.Mask.EditMask = "c0"
        Me.seTongGia.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.seTongGia.Size = New System.Drawing.Size(121, 20)
        Me.seTongGia.TabIndex = 38
        '
        'LabelControl10
        '
        Me.LabelControl10.Location = New System.Drawing.Point(7, 112)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl10.TabIndex = 39
        Me.LabelControl10.Text = "Tổng giá"
        '
        'LabelControl11
        '
        Me.LabelControl11.Location = New System.Drawing.Point(194, 138)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl11.TabIndex = 44
        Me.LabelControl11.Text = "TG KH(năm)"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(375, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 253)
        Me.barDockControlBottom.Size = New System.Drawing.Size(375, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 227)
        '
        'BarDockControl1
        '
        Me.BarDockControl1.CausesValidation = False
        Me.BarDockControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl1.Location = New System.Drawing.Point(375, 26)
        Me.BarDockControl1.Size = New System.Drawing.Size(0, 227)
        '
        'frmThemTaiSan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 253)
        Me.Controls.Add(Me.LabelControl11)
        Me.Controls.Add(Me.LabelControl10)
        Me.Controls.Add(Me.seTongGia)
        Me.Controls.Add(Me.seTGKH)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.seMucKH)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.seSoLuong)
        Me.Controls.Add(Me.lueTinhTrangTS)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.deNgayNhap)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.txtGhiChuTS)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.seDonGia)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.lueLoaiTS)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtTenTS)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.BarDockControl1)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemTaiSan"
        Me.Text = "Thêm tài sản"
        CType(Me.txtTenTS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueLoaiTS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seDonGia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhiChuTS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayNhap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deNgayNhap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueTinhTrangTS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seSoLuong.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seMucKH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seTGKH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.seTongGia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTenTS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lueLoaiTS As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seDonGia As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhiChuTS As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deNgayNhap As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lueTinhTrangTS As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seSoLuong As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents seTGKH As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seMucKH As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents seTongGia As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl1 As DevExpress.XtraBars.BarDockControl
End Class
