<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBangExeclInovice
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBangExeclInovice))
        Me.barMng = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.cmbQuy = New DevExpress.XtraBars.BarEditItem()
        Me.rcmbQuy = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.txtNam = New DevExpress.XtraBars.BarEditItem()
        Me.rtxtNam = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.chkGhiSo = New DevExpress.XtraBars.BarEditItem()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.btnTaiDuLieu = New DevExpress.XtraBars.BarButtonItem()
        Me.btnKetXuat = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.txtThang = New DevExpress.XtraBars.BarEditItem()
        Me.rTxtThang = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.btnDong = New DevExpress.XtraBars.BarButtonItem()
        Me.chkDaGhiSo = New DevExpress.XtraBars.BarCheckItem()
        Me.excelViewer = New SpreadsheetGear.Windows.Forms.WorkbookView()
        CType(Me.barMng, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcmbQuy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rtxtNam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rTxtThang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rTxtThang.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'barMng
        '
        Me.barMng.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.barMng.DockControls.Add(Me.barDockControlTop)
        Me.barMng.DockControls.Add(Me.barDockControlBottom)
        Me.barMng.DockControls.Add(Me.barDockControlLeft)
        Me.barMng.DockControls.Add(Me.barDockControlRight)
        Me.barMng.Form = Me
        Me.barMng.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.txtThang, Me.btnTaiDuLieu, Me.btnKetXuat, Me.btnDong, Me.cmbQuy, Me.txtNam, Me.chkDaGhiSo, Me.chkGhiSo})
        Me.barMng.MainMenu = Me.Bar2
        Me.barMng.MaxItemId = 8
        Me.barMng.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rTxtThang, Me.rcmbQuy, Me.rtxtNam, Me.RepositoryItemCheckEdit1})
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.cmbQuy), New DevExpress.XtraBars.LinkPersistInfo(Me.txtNam), New DevExpress.XtraBars.LinkPersistInfo(Me.chkGhiSo), New DevExpress.XtraBars.LinkPersistInfo(Me.btnTaiDuLieu), New DevExpress.XtraBars.LinkPersistInfo(Me.btnKetXuat, True)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'cmbQuy
        '
        Me.cmbQuy.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmbQuy.Appearance.Options.UseFont = True
        Me.cmbQuy.Caption = "Quý"
        Me.cmbQuy.Edit = Me.rcmbQuy
        Me.cmbQuy.Id = 4
        Me.cmbQuy.Name = "cmbQuy"
        Me.cmbQuy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'rcmbQuy
        '
        Me.rcmbQuy.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.rcmbQuy.Appearance.ForeColor = System.Drawing.Color.Red
        Me.rcmbQuy.Appearance.Options.UseFont = True
        Me.rcmbQuy.Appearance.Options.UseForeColor = True
        Me.rcmbQuy.Appearance.Options.UseTextOptions = True
        Me.rcmbQuy.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rcmbQuy.AutoHeight = False
        Me.rcmbQuy.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcmbQuy.DropDownItemHeight = 30
        Me.rcmbQuy.DropDownRows = 4
        Me.rcmbQuy.Items.AddRange(New Object() {"I", "II", "III", "IV"})
        Me.rcmbQuy.Name = "rcmbQuy"
        Me.rcmbQuy.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'txtNam
        '
        Me.txtNam.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtNam.Appearance.Options.UseFont = True
        Me.txtNam.Caption = "Năm"
        Me.txtNam.Edit = Me.rtxtNam
        Me.txtNam.Id = 5
        Me.txtNam.Name = "txtNam"
        Me.txtNam.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtNam.Width = 70
        '
        'rtxtNam
        '
        Me.rtxtNam.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.rtxtNam.Appearance.ForeColor = System.Drawing.Color.Red
        Me.rtxtNam.Appearance.Options.UseFont = True
        Me.rtxtNam.Appearance.Options.UseForeColor = True
        Me.rtxtNam.Appearance.Options.UseTextOptions = True
        Me.rtxtNam.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rtxtNam.AutoHeight = False
        Me.rtxtNam.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.rtxtNam.IsFloatValue = False
        Me.rtxtNam.Mask.EditMask = "N00"
        Me.rtxtNam.Name = "rtxtNam"
        '
        'chkGhiSo
        '
        Me.chkGhiSo.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkGhiSo.Appearance.Options.UseFont = True
        Me.chkGhiSo.Caption = "Đã ghi sổ"
        Me.chkGhiSo.Edit = Me.RepositoryItemCheckEdit1
        Me.chkGhiSo.Id = 7
        Me.chkGhiSo.Name = "chkGhiSo"
        Me.chkGhiSo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.chkGhiSo.Width = 27
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.RepositoryItemCheckEdit1.Appearance.Options.UseBackColor = True
        Me.RepositoryItemCheckEdit1.AutoWidth = True
        Me.RepositoryItemCheckEdit1.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.RepositoryItemCheckEdit1.PictureChecked = Global.BACSOFT.My.Resources.Resources.Checked
        Me.RepositoryItemCheckEdit1.PictureUnchecked = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.RepositoryItemCheckEdit1.ValueGrayed = "False"
        '
        'btnTaiDuLieu
        '
        Me.btnTaiDuLieu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnTaiDuLieu.Appearance.Options.UseFont = True
        Me.btnTaiDuLieu.Caption = "Tải dữ liệu"
        Me.btnTaiDuLieu.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btnTaiDuLieu.Id = 1
        Me.btnTaiDuLieu.Name = "btnTaiDuLieu"
        Me.btnTaiDuLieu.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btnKetXuat
        '
        Me.btnKetXuat.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnKetXuat.Appearance.Options.UseFont = True
        Me.btnKetXuat.Caption = "Kết xuất"
        Me.btnKetXuat.Glyph = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btnKetXuat.Id = 2
        Me.btnKetXuat.Name = "btnKetXuat"
        Me.btnKetXuat.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(1000, 30)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 689)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1000, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 30)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 659)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1000, 30)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 659)
        '
        'txtThang
        '
        Me.txtThang.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtThang.Appearance.Options.UseFont = True
        Me.txtThang.Caption = "Tháng"
        Me.txtThang.Edit = Me.rTxtThang
        Me.txtThang.Glyph = Global.BACSOFT.My.Resources.Resources.Preview_18
        Me.txtThang.Id = 0
        Me.txtThang.Name = "txtThang"
        Me.txtThang.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        Me.txtThang.Width = 101
        '
        'rTxtThang
        '
        Me.rTxtThang.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.rTxtThang.Appearance.ForeColor = System.Drawing.Color.Red
        Me.rTxtThang.Appearance.Options.UseFont = True
        Me.rTxtThang.Appearance.Options.UseForeColor = True
        Me.rTxtThang.Appearance.Options.UseTextOptions = True
        Me.rTxtThang.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rTxtThang.AutoHeight = False
        Me.rTxtThang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rTxtThang.DisplayFormat.FormatString = "MM/yyyy"
        Me.rTxtThang.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.rTxtThang.EditFormat.FormatString = "MM/yyyy"
        Me.rTxtThang.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.rTxtThang.Mask.EditMask = "MM/yyyy"
        Me.rTxtThang.Name = "rTxtThang"
        Me.rTxtThang.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'btnDong
        '
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Caption = "Đóng"
        Me.btnDong.Glyph = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btnDong.Id = 3
        Me.btnDong.Name = "btnDong"
        Me.btnDong.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'chkDaGhiSo
        '
        Me.chkDaGhiSo.Appearance.Font = New System.Drawing.Font("Tahoma", 8.24!, System.Drawing.FontStyle.Bold)
        Me.chkDaGhiSo.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkDaGhiSo.Appearance.Options.UseFont = True
        Me.chkDaGhiSo.Appearance.Options.UseForeColor = True
        Me.chkDaGhiSo.Caption = "Đã ghi sổ"
        Me.chkDaGhiSo.Id = 6
        Me.chkDaGhiSo.Name = "chkDaGhiSo"
        '
        'excelViewer
        '
        Me.excelViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.excelViewer.Location = New System.Drawing.Point(0, 30)
        Me.excelViewer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.excelViewer.Name = "excelViewer"
        Me.excelViewer.Size = New System.Drawing.Size(1000, 659)
        Me.excelViewer.TabIndex = 4
        Me.excelViewer.WorkbookSetState = resources.GetString("excelViewer.WorkbookSetState")
        '
        'frmBangKeHoaDonGTGT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.excelViewer)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmBangKeHoaDonGTGT"
        Me.Size = New System.Drawing.Size(1000, 689)
        CType(Me.barMng, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcmbQuy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rtxtNam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rTxtThang.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rTxtThang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents barMng As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents txtThang As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rTxtThang As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btnTaiDuLieu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnKetXuat As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDong As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents excelViewer As SpreadsheetGear.Windows.Forms.WorkbookView
    Friend WithEvents cmbQuy As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rcmbQuy As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents txtNam As DevExpress.XtraBars.BarEditItem
    Friend WithEvents rtxtNam As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents chkDaGhiSo As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents chkGhiSo As DevExpress.XtraBars.BarEditItem
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

End Class
