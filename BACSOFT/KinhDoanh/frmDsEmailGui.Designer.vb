<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDsEmailGui
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
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.btnThemMoi = New DevExpress.XtraBars.BarButtonItem()
        Me.btnCapNhat = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXoa = New DevExpress.XtraBars.BarButtonItem()
        Me.btnDong = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTen = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colEmail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colMatKhau = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtMatKhau = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.txtMatKhau2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMatKhau, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMatKhau2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2, Me.Bar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnThemMoi, Me.btnXoa, Me.btnCapNhat, Me.btnDong})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 4
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnThemMoi), New DevExpress.XtraBars.LinkPersistInfo(Me.btnCapNhat), New DevExpress.XtraBars.LinkPersistInfo(Me.btnXoa), New DevExpress.XtraBars.LinkPersistInfo(Me.btnDong, True)})
        Me.Bar2.OptionsBar.AllowQuickCustomization = False
        Me.Bar2.OptionsBar.DrawDragBorder = False
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'btnThemMoi
        '
        Me.btnThemMoi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnThemMoi.Appearance.Options.UseFont = True
        Me.btnThemMoi.Caption = "Thêm mới"
        Me.btnThemMoi.Glyph = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThemMoi.Id = 0
        Me.btnThemMoi.Name = "btnThemMoi"
        Me.btnThemMoi.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btnCapNhat
        '
        Me.btnCapNhat.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnCapNhat.Appearance.Options.UseFont = True
        Me.btnCapNhat.Caption = "Cập nhật"
        Me.btnCapNhat.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btnCapNhat.Id = 2
        Me.btnCapNhat.Name = "btnCapNhat"
        Me.btnCapNhat.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
        '
        'btnXoa
        '
        Me.btnXoa.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnXoa.Appearance.Options.UseFont = True
        Me.btnXoa.Caption = "Xóa"
        Me.btnXoa.Glyph = Global.BACSOFT.My.Resources.Resources.Delete_18
        Me.btnXoa.Id = 1
        Me.btnXoa.Name = "btnXoa"
        Me.btnXoa.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph
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
        'Bar3
        '
        Me.Bar3.BarName = "Status bar"
        Me.Bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom
        Me.Bar3.DockCol = 0
        Me.Bar3.DockRow = 0
        Me.Bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.Bar3.OptionsBar.AllowQuickCustomization = False
        Me.Bar3.OptionsBar.DrawDragBorder = False
        Me.Bar3.OptionsBar.UseWholeRow = True
        Me.Bar3.Text = "Status bar"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(824, 26)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 506)
        Me.barDockControlBottom.Size = New System.Drawing.Size(824, 23)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 26)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 480)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(824, 26)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 480)
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 26)
        Me.gdv.MainView = Me.gdvData
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtMatKhau, Me.txtMatKhau2})
        Me.gdv.Size = New System.Drawing.Size(824, 480)
        Me.gdv.TabIndex = 6
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvData})
        '
        'gdvData
        '
        Me.gdvData.Appearance.Empty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.gdvData.Appearance.Empty.Options.UseBackColor = True
        Me.gdvData.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvData.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvData.ColumnPanelRowHeight = 30
        Me.gdvData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colId, Me.colTen, Me.colEmail, Me.colMatKhau})
        Me.gdvData.GridControl = Me.gdv
        Me.gdvData.GroupFormat = "{0}[#image]{1} {2}"
        Me.gdvData.GroupRowHeight = 25
        Me.gdvData.Name = "gdvData"
        Me.gdvData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvData.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvData.OptionsBehavior.AutoPopulateColumns = False
        Me.gdvData.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.gdvData.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvData.OptionsView.EnableAppearanceOddRow = True
        Me.gdvData.OptionsView.ShowAutoFilterRow = True
        Me.gdvData.OptionsView.ShowGroupPanel = False
        Me.gdvData.OptionsView.ShowIndicator = False
        Me.gdvData.RowHeight = 25
        '
        'colId
        '
        Me.colId.Caption = "Id"
        Me.colId.FieldName = "Id"
        Me.colId.Name = "colId"
        '
        'colTen
        '
        Me.colTen.Caption = "Tên"
        Me.colTen.FieldName = "Ten"
        Me.colTen.Name = "colTen"
        Me.colTen.SummaryItem.DisplayFormat = "{0:N0}"
        Me.colTen.SummaryItem.FieldName = "NguoiGui"
        Me.colTen.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.colTen.Visible = True
        Me.colTen.VisibleIndex = 0
        Me.colTen.Width = 144
        '
        'colEmail
        '
        Me.colEmail.AppearanceCell.ForeColor = System.Drawing.Color.Navy
        Me.colEmail.AppearanceCell.Options.UseForeColor = True
        Me.colEmail.Caption = "Email"
        Me.colEmail.FieldName = "Email"
        Me.colEmail.Name = "colEmail"
        Me.colEmail.Visible = True
        Me.colEmail.VisibleIndex = 1
        Me.colEmail.Width = 299
        '
        'colMatKhau
        '
        Me.colMatKhau.Caption = "Mật khẩu"
        Me.colMatKhau.ColumnEdit = Me.txtMatKhau
        Me.colMatKhau.FieldName = "MatKhau"
        Me.colMatKhau.Name = "colMatKhau"
        Me.colMatKhau.Visible = True
        Me.colMatKhau.VisibleIndex = 2
        Me.colMatKhau.Width = 379
        '
        'txtMatKhau
        '
        Me.txtMatKhau.AutoHeight = False
        Me.txtMatKhau.Name = "txtMatKhau"
        Me.txtMatKhau.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'txtMatKhau2
        '
        Me.txtMatKhau2.AutoHeight = False
        Me.txtMatKhau2.Name = "txtMatKhau2"
        '
        'frmDsEmailGui
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 529)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmDsEmailGui"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Danh sách Email gửi marketing"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMatKhau, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMatKhau2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents btnThemMoi As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnCapNhat As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnXoa As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnDong As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTen As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colEmail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colMatKhau As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtMatKhau As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents txtMatKhau2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
