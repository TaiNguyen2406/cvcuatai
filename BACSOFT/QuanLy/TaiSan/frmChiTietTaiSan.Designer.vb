<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChiTietTaiSan
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
        Me.gcChiTietTaiSan = New DevExpress.XtraGrid.GridControl()
        Me.gvChiTietTaiSan = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.id = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gColTenTS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gColIdTinhTrang = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.riLueTinhTrang = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.gcChiTietTaiSan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvChiTietTaiSan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gcChiTietTaiSan
        '
        Me.gcChiTietTaiSan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcChiTietTaiSan.Location = New System.Drawing.Point(0, 0)
        Me.gcChiTietTaiSan.MainView = Me.gvChiTietTaiSan
        Me.gcChiTietTaiSan.Name = "gcChiTietTaiSan"
        Me.gcChiTietTaiSan.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.riLueTinhTrang})
        Me.gcChiTietTaiSan.Size = New System.Drawing.Size(551, 349)
        Me.gcChiTietTaiSan.TabIndex = 0
        Me.gcChiTietTaiSan.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvChiTietTaiSan})
        '
        'gvChiTietTaiSan
        '
        Me.gvChiTietTaiSan.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gvChiTietTaiSan.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gvChiTietTaiSan.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvChiTietTaiSan.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gvChiTietTaiSan.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvChiTietTaiSan.Appearance.HeaderPanel.Options.UseFont = True
        Me.gvChiTietTaiSan.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gvChiTietTaiSan.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gvChiTietTaiSan.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gvChiTietTaiSan.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gvChiTietTaiSan.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.id, Me.GridColumn1, Me.gColTenTS, Me.GridColumn2, Me.gColIdTinhTrang, Me.GridColumn3})
        Me.gvChiTietTaiSan.GridControl = Me.gcChiTietTaiSan
        Me.gvChiTietTaiSan.Name = "gvChiTietTaiSan"
        Me.gvChiTietTaiSan.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gvChiTietTaiSan.OptionsView.AllowCellMerge = True
        Me.gvChiTietTaiSan.OptionsView.EnableAppearanceEvenRow = True
        Me.gvChiTietTaiSan.OptionsView.ShowFooter = True
        Me.gvChiTietTaiSan.OptionsView.ShowGroupPanel = False
        Me.gvChiTietTaiSan.OptionsView.ShowIndicator = False
        Me.gvChiTietTaiSan.RowHeight = 23
        '
        'id
        '
        Me.id.Caption = "id"
        Me.id.FieldName = "id"
        Me.id.Name = "id"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Mã tài sản"
        Me.GridColumn1.FieldName = "idtaisan"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        '
        'gColTenTS
        '
        Me.gColTenTS.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gColTenTS.AppearanceCell.Options.UseFont = True
        Me.gColTenTS.AppearanceCell.Options.UseTextOptions = True
        Me.gColTenTS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gColTenTS.Caption = "Tên tài sản"
        Me.gColTenTS.FieldName = "TenVT"
        Me.gColTenTS.Name = "gColTenTS"
        Me.gColTenTS.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.gColTenTS.SummaryItem.DisplayFormat = "Đang sử dụng = {0} "
        Me.gColTenTS.SummaryItem.FieldName = "tentaisan"
        Me.gColTenTS.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.gColTenTS.SummaryItem.Tag = 1
        Me.gColTenTS.Visible = True
        Me.gColTenTS.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Chi tiết tài sản"
        Me.GridColumn2.FieldName = "tenchitiettaisan"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn2.SummaryItem.DisplayFormat = "Chưa sử dụng= {0} "
        Me.GridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.GridColumn2.SummaryItem.Tag = 2
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'gColIdTinhTrang
        '
        Me.gColIdTinhTrang.Caption = "Tình trạng"
        Me.gColIdTinhTrang.ColumnEdit = Me.riLueTinhTrang
        Me.gColIdTinhTrang.FieldName = "idtinhtrang"
        Me.gColIdTinhTrang.Name = "gColIdTinhTrang"
        Me.gColIdTinhTrang.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.gColIdTinhTrang.SummaryItem.DisplayFormat = "Thanh lý = {0}"
        Me.gColIdTinhTrang.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        Me.gColIdTinhTrang.SummaryItem.Tag = 3
        Me.gColIdTinhTrang.Visible = True
        Me.gColIdTinhTrang.VisibleIndex = 2
        '
        'riLueTinhTrang
        '
        Me.riLueTinhTrang.AutoHeight = False
        Me.riLueTinhTrang.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.riLueTinhTrang.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("id", "ID", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("tentinhtrang", "Tên")})
        Me.riLueTinhTrang.DisplayMember = "tentinhtrang"
        Me.riLueTinhTrang.Name = "riLueTinhTrang"
        Me.riLueTinhTrang.NullText = ""
        Me.riLueTinhTrang.ShowHeader = False
        Me.riLueTinhTrang.ValueMember = "id"
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Ngày thanh lý"
        Me.GridColumn3.FieldName = "ngaythanhly"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        '
        'frmChiTietTaiSan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(551, 349)
        Me.Controls.Add(Me.gcChiTietTaiSan)
        Me.Name = "frmChiTietTaiSan"
        Me.Text = "Chi tiết tài sản"
        CType(Me.gcChiTietTaiSan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvChiTietTaiSan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.riLueTinhTrang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gcChiTietTaiSan As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvChiTietTaiSan As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents id As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gColIdTinhTrang As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents riLueTinhTrang As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents gColTenTS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
End Class
