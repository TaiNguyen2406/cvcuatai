<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNKhoaDaoTao
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
        Me.gdvKhoaDT = New DevExpress.XtraGrid.GridControl()
        Me.gdvKhoaDTCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tbNgay = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.gdvKhoaDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvKhoaDTCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNgay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvKhoaDT
        '
        Me.gdvKhoaDT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvKhoaDT.Location = New System.Drawing.Point(0, 0)
        Me.gdvKhoaDT.MainView = Me.gdvKhoaDTCT
        Me.gdvKhoaDT.Name = "gdvKhoaDT"
        Me.gdvKhoaDT.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.tbNgay})
        Me.gdvKhoaDT.Size = New System.Drawing.Size(595, 479)
        Me.gdvKhoaDT.TabIndex = 5
        Me.gdvKhoaDT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvKhoaDTCT})
        '
        'gdvKhoaDTCT
        '
        Me.gdvKhoaDTCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvKhoaDTCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvKhoaDTCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvKhoaDTCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvKhoaDTCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvKhoaDTCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvKhoaDTCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvKhoaDTCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvKhoaDTCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvKhoaDTCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvKhoaDTCT.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gdvKhoaDTCT.Appearance.Row.Options.UseFont = True
        Me.gdvKhoaDTCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4, Me.GridColumn3, Me.GridColumn1, Me.GridColumn2})
        Me.gdvKhoaDTCT.FixedLineWidth = 1
        Me.gdvKhoaDTCT.GridControl = Me.gdvKhoaDT
        Me.gdvKhoaDTCT.Name = "gdvKhoaDTCT"
        Me.gdvKhoaDTCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvKhoaDTCT.OptionsCustomization.AllowColumnMoving = False
        Me.gdvKhoaDTCT.OptionsCustomization.AllowGroup = False
        Me.gdvKhoaDTCT.OptionsLayout.Columns.AddNewColumns = False
        Me.gdvKhoaDTCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvKhoaDTCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvKhoaDTCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvKhoaDTCT.OptionsView.ShowFooter = True
        Me.gdvKhoaDTCT.OptionsView.ShowGroupPanel = False
        Me.gdvKhoaDTCT.RowHeight = 23
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "ID"
        Me.GridColumn4.FieldName = "ID"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Khoá học"
        Me.GridColumn3.FieldName = "Ten"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn3.SummaryItem.FieldName = "NoiDung"
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 307
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Từ ngày"
        Me.GridColumn1.ColumnEdit = Me.tbNgay
        Me.GridColumn1.FieldName = "TuNgay"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 134
        '
        'tbNgay
        '
        Me.tbNgay.AutoHeight = False
        Me.tbNgay.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgay.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbNgay.Name = "tbNgay"
        Me.tbNgay.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Đến ngày"
        Me.GridColumn2.ColumnEdit = Me.tbNgay
        Me.GridColumn2.FieldName = "DenNgay"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 136
        '
        'frmCNKhoaDaoTao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(595, 479)
        Me.Controls.Add(Me.gdvKhoaDT)
        Me.Name = "frmCNKhoaDaoTao"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật khoá đào tạo"
        CType(Me.gdvKhoaDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvKhoaDTCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNgay.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNgay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdvKhoaDT As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvKhoaDTCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents tbNgay As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
