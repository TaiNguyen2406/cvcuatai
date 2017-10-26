<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNNhomDaoTao
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
        Me.gdvNhomMH = New DevExpress.XtraGrid.GridControl()
        Me.gdvNhomMHCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.gdvNhomMH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNhomMHCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvNhomMH
        '
        Me.gdvNhomMH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvNhomMH.Location = New System.Drawing.Point(0, 0)
        Me.gdvNhomMH.MainView = Me.gdvNhomMHCT
        Me.gdvNhomMH.Name = "gdvNhomMH"
        Me.gdvNhomMH.Size = New System.Drawing.Size(276, 376)
        Me.gdvNhomMH.TabIndex = 5
        Me.gdvNhomMH.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvNhomMHCT})
        '
        'gdvNhomMHCT
        '
        Me.gdvNhomMHCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvNhomMHCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvNhomMHCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhomMHCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvNhomMHCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvNhomMHCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvNhomMHCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvNhomMHCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvNhomMHCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvNhomMHCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvNhomMHCT.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.gdvNhomMHCT.Appearance.Row.Options.UseFont = True
        Me.gdvNhomMHCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.gdvNhomMHCT.FixedLineWidth = 1
        Me.gdvNhomMHCT.GridControl = Me.gdvNhomMH
        Me.gdvNhomMHCT.Name = "gdvNhomMHCT"
        Me.gdvNhomMHCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvNhomMHCT.OptionsCustomization.AllowColumnMoving = False
        Me.gdvNhomMHCT.OptionsCustomization.AllowGroup = False
        Me.gdvNhomMHCT.OptionsLayout.Columns.AddNewColumns = False
        Me.gdvNhomMHCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvNhomMHCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvNhomMHCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvNhomMHCT.OptionsView.ShowFooter = True
        Me.gdvNhomMHCT.OptionsView.ShowGroupPanel = False
        Me.gdvNhomMHCT.RowHeight = 23
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nhóm đào tạo"
        Me.GridColumn3.FieldName = "NoiDung"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 250
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "ID"
        Me.GridColumn4.FieldName = "ID"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'frmCNNhomDaoTao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(276, 376)
        Me.Controls.Add(Me.gdvNhomMH)
        Me.Name = "frmCNNhomDaoTao"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật nhóm đào tạo"
        CType(Me.gdvNhomMH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNhomMHCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdvNhomMH As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvNhomMHCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
