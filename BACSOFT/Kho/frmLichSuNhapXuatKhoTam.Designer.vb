<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLichSuNhapXuatKhoTam
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLichSuNhapXuatKhoTam))
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtSQL = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSQL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 0
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(874, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 521)
        Me.barDockControlBottom.Size = New System.Drawing.Size(874, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 521)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(874, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 521)
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 0)
        Me.gdv.MainView = Me.gdvData
        Me.gdv.MenuManager = Me.BarManager1
        Me.gdv.Name = "gdv"
        Me.gdv.Size = New System.Drawing.Size(874, 521)
        Me.gdv.TabIndex = 4
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvData})
        '
        'gdvData
        '
        Me.gdvData.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvData.Appearance.FooterPanel.Options.UseFont = True
        Me.gdvData.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvData.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvData.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvData.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvData.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.gdvData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11})
        Me.gdvData.GridControl = Me.gdv
        Me.gdvData.Name = "gdvData"
        Me.gdvData.OptionsView.ColumnAutoWidth = False
        Me.gdvData.OptionsView.ShowAutoFilterRow = True
        Me.gdvData.OptionsView.ShowFooter = True
        Me.gdvData.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Ngày"
        Me.GridColumn1.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn1.FieldName = "Ngay"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 123
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Số CG"
        Me.GridColumn2.FieldName = "SoCG"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 78
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Số Phiếu"
        Me.GridColumn3.FieldName = "SoPhieu"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 84
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "XK Tạm"
        Me.GridColumn4.FieldName = "YeuCauXuat"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 60
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "NK Tạm"
        Me.GridColumn5.FieldName = "YeuCauTra"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 66
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Xuất CT"
        Me.GridColumn6.FieldName = "XuatThucCT"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 63
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Xuất Tạm"
        Me.GridColumn7.FieldName = "XuatTam"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 64
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "NV Yêu cầu"
        Me.GridColumn8.FieldName = "NhanVienYeuCau"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 111
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "PT Kho"
        Me.GridColumn9.FieldName = "PhuTrachKho"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 106
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Mã KH"
        Me.GridColumn10.FieldName = "MaKH"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 9
        Me.GridColumn10.Width = 94
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Tên CT"
        Me.GridColumn11.FieldName = "TenCT"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 10
        Me.GridColumn11.Width = 667
        '
        'txtSQL
        '
        Me.txtSQL.EditValue = resources.GetString("txtSQL.EditValue")
        Me.txtSQL.Location = New System.Drawing.Point(37, 124)
        Me.txtSQL.MenuManager = Me.BarManager1
        Me.txtSQL.Name = "txtSQL"
        Me.txtSQL.Size = New System.Drawing.Size(455, 296)
        Me.txtSQL.TabIndex = 9
        Me.txtSQL.Visible = False
        '
        'frmLichSuNhapXuatKhoTam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 521)
        Me.Controls.Add(Me.txtSQL)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmLichSuNhapXuatKhoTam"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lịch sử nhập xuất tạm"
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSQL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtSQL As DevExpress.XtraEditors.MemoEdit
End Class
