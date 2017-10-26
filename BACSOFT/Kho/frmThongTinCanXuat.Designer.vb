<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThongTinCanXuat
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
        Me.gdvChaoGia = New DevExpress.XtraGrid.GridControl()
        Me.gdvChaoGiaCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.gdvChaoGia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChaoGiaCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvChaoGia
        '
        Me.gdvChaoGia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvChaoGia.Location = New System.Drawing.Point(0, 0)
        Me.gdvChaoGia.MainView = Me.gdvChaoGiaCT
        Me.gdvChaoGia.Name = "gdvChaoGia"
        Me.gdvChaoGia.Size = New System.Drawing.Size(457, 262)
        Me.gdvChaoGia.TabIndex = 1
        Me.gdvChaoGia.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvChaoGiaCT})
        '
        'gdvChaoGiaCT
        '
        Me.gdvChaoGiaCT.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.gdvChaoGiaCT.Appearance.FooterPanel.Options.UseFont = True
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvChaoGiaCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.gdvChaoGiaCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvChaoGiaCT.GridControl = Me.gdvChaoGia
        Me.gdvChaoGiaCT.Name = "gdvChaoGiaCT"
        Me.gdvChaoGiaCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvChaoGiaCT.OptionsBehavior.ReadOnly = True
        Me.gdvChaoGiaCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvChaoGiaCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvChaoGiaCT.OptionsView.ShowFooter = True
        Me.gdvChaoGiaCT.OptionsView.ShowGroupPanel = False
        Me.gdvChaoGiaCT.RowHeight = 22
        Me.gdvChaoGiaCT.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Ngày xác nhận"
        Me.GridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "NgayThang"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 102
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Mã KH"
        Me.GridColumn2.FieldName = "ttcMa"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 100
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "SL"
        Me.GridColumn3.FieldName = "SoLuong"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 46
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Phụ trách"
        Me.GridColumn4.FieldName = "PhuTrach"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 140
        '
        'frmThongTinCanXuat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 262)
        Me.Controls.Add(Me.gdvChaoGia)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmThongTinCanXuat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Thông tin cần xuất"
        CType(Me.gdvChaoGia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChaoGiaCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdvChaoGia As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvChaoGiaCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
