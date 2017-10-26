<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileLienQuan
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
        Me.gdvListFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvListFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvListFile
        '
        Me.gdvListFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvListFile.Location = New System.Drawing.Point(0, 0)
        Me.gdvListFile.MainView = Me.gdvListFileCT
        Me.gdvListFile.Name = "gdvListFile"
        Me.gdvListFile.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHyperLinkEdit4})
        Me.gdvListFile.Size = New System.Drawing.Size(411, 358)
        Me.gdvListFile.TabIndex = 1
        Me.gdvListFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvListFileCT})
        '
        'gdvListFileCT
        '
        Me.gdvListFileCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvListFileCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvListFileCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvListFileCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvListFileCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvListFileCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvListFileCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvListFileCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvListFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn52, Me.GridColumn1, Me.GridColumn3})
        Me.gdvListFileCT.GridControl = Me.gdvListFile
        Me.gdvListFileCT.GroupCount = 1
        Me.gdvListFileCT.Name = "gdvListFileCT"
        Me.gdvListFileCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvListFileCT.OptionsBehavior.Editable = False
        Me.gdvListFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvListFileCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvListFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvListFileCT.RowHeight = 22
        Me.gdvListFileCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Quá trình"
        Me.GridColumn2.FieldName = "QuaTrinh"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "File"
        Me.GridColumn52.ColumnEdit = Me.RepositoryItemHyperLinkEdit4
        Me.GridColumn52.FieldName = "File"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.OptionsColumn.AllowEdit = False
        Me.GridColumn52.OptionsColumn.ReadOnly = True
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 0
        Me.GridColumn52.Width = 500
        '
        'RepositoryItemHyperLinkEdit4
        '
        Me.RepositoryItemHyperLinkEdit4.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit4.Name = "RepositoryItemHyperLinkEdit4"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "KD"
        Me.GridColumn1.FieldName = "KD"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Thời gian"
        Me.GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "NgayThang"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'frmFileLienQuan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 358)
        Me.Controls.Add(Me.gdvListFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileLienQuan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tổng hợp file yêu cầu đến, chào giá"
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdvListFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvListFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
