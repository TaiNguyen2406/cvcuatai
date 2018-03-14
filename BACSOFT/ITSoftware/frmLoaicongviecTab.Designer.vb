<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLoaicongviecTab
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDSCV = New DevExpress.XtraGrid.GridControl()
        Me.grdViewDSCV = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.btnLuu = New DevExpress.XtraBars.BarButtonItem()
        Me.btnXem = New DevExpress.XtraBars.BarButtonItem()
        Me.Bar3 = New DevExpress.XtraBars.Bar()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grdDSCV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewDSCV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.grdDSCV)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 30)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1122, 617)
        Me.GroupControl1.TabIndex = 41
        Me.GroupControl1.Text = "Danh mục loại công việc"
        '
        'grdDSCV
        '
        Me.grdDSCV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDSCV.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDSCV.Location = New System.Drawing.Point(2, 25)
        Me.grdDSCV.MainView = Me.grdViewDSCV
        Me.grdDSCV.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDSCV.Name = "grdDSCV"
        Me.grdDSCV.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.grdDSCV.Size = New System.Drawing.Size(1118, 590)
        Me.grdDSCV.TabIndex = 5
        Me.grdDSCV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewDSCV})
        '
        'grdViewDSCV
        '
        Me.grdViewDSCV.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSCV.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdViewDSCV.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSCV.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdViewDSCV.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSCV.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdViewDSCV.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn1, Me.GridColumn2, Me.GridColumn4})
        Me.grdViewDSCV.GridControl = Me.grdDSCV
        Me.grdViewDSCV.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.grdViewDSCV.Name = "grdViewDSCV"
        Me.grdViewDSCV.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn3.AppearanceCell.Options.UseFont = True
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Kích hoạt"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn3.FieldName = "Active"
        Me.GridColumn3.MaxWidth = 100
        Me.GridColumn3.MinWidth = 100
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 100
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Loại công việc"
        Me.GridColumn1.FieldName = "Tencongviec"
        Me.GridColumn1.MaxWidth = 400
        Me.GridColumn1.MinWidth = 400
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 400
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn2.AppearanceCell.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn2.AppearanceHeader.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Ghi chú"
        Me.GridColumn2.FieldName = "Ghichu"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 702
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Recordid"
        Me.GridColumn4.FieldName = "Id"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2, Me.Bar3})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnLuu, Me.btnXem})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 3
        Me.BarManager1.StatusBar = Me.Bar3
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnLuu, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph), New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.btnXem, "", True, True, True, 0, Nothing, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'btnLuu
        '
        Me.btnLuu.Caption = "Lưu lại"
        Me.btnLuu.Glyph = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btnLuu.Id = 1
        Me.btnLuu.Name = "btnLuu"
        '
        'btnXem
        '
        Me.btnXem.Caption = "Tải DS"
        Me.btnXem.Glyph = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btnXem.Id = 2
        Me.btnXem.Name = "btnXem"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(1122, 30)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 647)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1122, 23)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 30)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 617)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1122, 30)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 617)
        '
        'frmLoaicongviecTab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmLoaicongviecTab"
        Me.Size = New System.Drawing.Size(1122, 670)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grdDSCV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewDSCV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdDSCV As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewDSCV As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents btnLuu As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents Bar3 As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnXem As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
