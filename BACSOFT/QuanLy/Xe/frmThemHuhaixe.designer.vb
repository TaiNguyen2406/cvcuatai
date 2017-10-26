<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThemHuhaixe
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
        Me.Bar1 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.lueXe = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.lueNhienVatLieu = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.AlertControl1 = New DevExpress.XtraBars.Alerter.AlertControl(Me.components)
        Me.btnThem = New DevExpress.XtraEditors.SimpleButton()
        Me.nudChiphi = New DevExpress.XtraEditors.SpinEdit()
        Me.cbxSuachua = New DevExpress.XtraEditors.CheckEdit()
        Me.txtThaythe = New DevExpress.XtraEditors.TextEdit()
        Me.txtVitri = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.dtpNgaySuaChua = New DevExpress.XtraEditors.DateEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lueNhienVatLieu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudChiphi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbxSuachua.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtThaythe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtVitri.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpNgaySuaChua.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpNgaySuaChua.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3})
        Me.BarManager1.MaxItemId = 12
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.lueXe, Me.lueNhienVatLieu, Me.RepositoryItemTextEdit1})
        '
        'Bar1
        '
        Me.Bar1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bar1.Appearance.Options.UseFont = True
        Me.Bar1.BarName = "Custom 1"
        Me.Bar1.DockCol = 0
        Me.Bar1.DockRow = 0
        Me.Bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem3, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar1.OptionsBar.AllowQuickCustomization = False
        Me.Bar1.OptionsBar.DrawDragBorder = False
        Me.Bar1.OptionsBar.UseWholeRow = True
        Me.Bar1.Text = "Custom 1"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Xóa"
        Me.BarButtonItem3.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem3.Id = 11
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(309, 33)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 211)
        Me.barDockControlBottom.Size = New System.Drawing.Size(309, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 33)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 178)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(309, 33)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 178)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "Lưu"
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Xóa"
        Me.BarButtonItem2.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
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
        'btnThem
        '
        Me.btnThem.Location = New System.Drawing.Point(222, 165)
        Me.btnThem.Name = "btnThem"
        Me.btnThem.Size = New System.Drawing.Size(75, 33)
        Me.btnThem.TabIndex = 66
        Me.btnThem.Text = "Lưu"
        '
        'nudChiphi
        '
        Me.nudChiphi.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.nudChiphi.Enabled = False
        Me.nudChiphi.Location = New System.Drawing.Point(56, 142)
        Me.nudChiphi.MenuManager = Me.BarManager1
        Me.nudChiphi.Name = "nudChiphi"
        Me.nudChiphi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.nudChiphi.Size = New System.Drawing.Size(100, 20)
        Me.nudChiphi.TabIndex = 133
        '
        'cbxSuachua
        '
        Me.cbxSuachua.Location = New System.Drawing.Point(8, 65)
        Me.cbxSuachua.MenuManager = Me.BarManager1
        Me.cbxSuachua.Name = "cbxSuachua"
        Me.cbxSuachua.Properties.Caption = "Đưa vào sửa chữa"
        Me.cbxSuachua.Size = New System.Drawing.Size(124, 19)
        Me.cbxSuachua.TabIndex = 132
        '
        'txtThaythe
        '
        Me.txtThaythe.Enabled = False
        Me.txtThaythe.Location = New System.Drawing.Point(56, 116)
        Me.txtThaythe.MenuManager = Me.BarManager1
        Me.txtThaythe.Name = "txtThaythe"
        Me.txtThaythe.Size = New System.Drawing.Size(241, 20)
        Me.txtThaythe.TabIndex = 131
        '
        'txtVitri
        '
        Me.txtVitri.Location = New System.Drawing.Point(56, 39)
        Me.txtVitri.MenuManager = Me.BarManager1
        Me.txtVitri.Name = "txtVitri"
        Me.txtVitri.Size = New System.Drawing.Size(241, 20)
        Me.txtVitri.TabIndex = 130
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(10, 144)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl4.TabIndex = 129
        Me.LabelControl4.Text = "Chi phí"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(10, 119)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl3.TabIndex = 128
        Me.LabelControl3.Text = "Thay thế"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(10, 92)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(73, 13)
        Me.LabelControl2.TabIndex = 127
        Me.LabelControl2.Text = "Ngày sửa chữa"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 42)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl1.TabIndex = 126
        Me.LabelControl1.Text = "Vị trí"
        '
        'dtpNgaySuaChua
        '
        Me.dtpNgaySuaChua.EditValue = Nothing
        Me.dtpNgaySuaChua.Enabled = False
        Me.dtpNgaySuaChua.Location = New System.Drawing.Point(92, 89)
        Me.dtpNgaySuaChua.MenuManager = Me.BarManager1
        Me.dtpNgaySuaChua.Name = "dtpNgaySuaChua"
        Me.dtpNgaySuaChua.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dtpNgaySuaChua.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.dtpNgaySuaChua.Properties.Mask.IgnoreMaskBlank = False
        Me.dtpNgaySuaChua.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.dtpNgaySuaChua.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpNgaySuaChua.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.dtpNgaySuaChua.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dtpNgaySuaChua.Properties.VistaTimeProperties.Mask.EditMask = "HH:mm"
        Me.dtpNgaySuaChua.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = True
        Me.dtpNgaySuaChua.Size = New System.Drawing.Size(124, 20)
        Me.dtpNgaySuaChua.TabIndex = 125
        '
        'frmThemHuhaixe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 211)
        Me.Controls.Add(Me.nudChiphi)
        Me.Controls.Add(Me.cbxSuachua)
        Me.Controls.Add(Me.txtThaythe)
        Me.Controls.Add(Me.txtVitri)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.dtpNgaySuaChua)
        Me.Controls.Add(Me.btnThem)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.LookAndFeel.SkinName = "Blue"
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmThemHuhaixe"
        Me.Text = "Thông tin hư hại xe"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueXe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lueNhienVatLieu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudChiphi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbxSuachua.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtThaythe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtVitri.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpNgaySuaChua.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpNgaySuaChua.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents lueXe As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents lueNhienVatLieu As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents AlertControl1 As DevExpress.XtraBars.Alerter.AlertControl
    Friend WithEvents btnThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Bar1 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents nudChiphi As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents cbxSuachua As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtThaythe As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtVitri As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtpNgaySuaChua As DevExpress.XtraEditors.DateEdit
End Class
