<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogEmailKD
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
        Me.gdvEmail = New DevExpress.XtraGrid.GridControl()
        Me.gdvDataEmail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.gdvEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvDataEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdvEmail
        '
        Me.gdvEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvEmail.Location = New System.Drawing.Point(0, 0)
        Me.gdvEmail.MainView = Me.gdvDataEmail
        Me.gdvEmail.Name = "gdvEmail"
        Me.gdvEmail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEdit2, Me.RepositoryItemHyperLinkEdit2, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3})
        Me.gdvEmail.Size = New System.Drawing.Size(860, 498)
        Me.gdvEmail.TabIndex = 7
        Me.gdvEmail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvDataEmail})
        '
        'gdvDataEmail
        '
        Me.gdvDataEmail.Appearance.Empty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.gdvDataEmail.Appearance.Empty.Options.UseBackColor = True
        Me.gdvDataEmail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvDataEmail.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvDataEmail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12, Me.GridColumn8, Me.GridColumn4, Me.GridColumn5, Me.GridColumn2, Me.GridColumn1, Me.GridColumn3, Me.GridColumn6})
        Me.gdvDataEmail.GridControl = Me.gdvEmail
        Me.gdvDataEmail.GroupFormat = "{0}[#image]{1} {2}"
        Me.gdvDataEmail.GroupRowHeight = 25
        Me.gdvDataEmail.Name = "gdvDataEmail"
        Me.gdvDataEmail.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvDataEmail.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvDataEmail.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvDataEmail.OptionsBehavior.AutoPopulateColumns = False
        Me.gdvDataEmail.OptionsBehavior.Editable = False
        Me.gdvDataEmail.OptionsBehavior.ReadOnly = True
        Me.gdvDataEmail.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvDataEmail.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvDataEmail.OptionsView.EnableAppearanceOddRow = True
        Me.gdvDataEmail.OptionsView.ShowAutoFilterRow = True
        Me.gdvDataEmail.OptionsView.ShowFooter = True
        Me.gdvDataEmail.OptionsView.ShowGroupPanel = False
        Me.gdvDataEmail.OptionsView.ShowIndicator = False
        Me.gdvDataEmail.RowHeight = 25
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn12.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GridColumn12.Caption = "Đã gửi"
        Me.GridColumn12.ColumnEdit = Me.RepositoryItemCheckEdit3
        Me.GridColumn12.FieldName = "TrangThai"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.OptionsColumn.FixedWidth = True
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        Me.GridColumn12.Width = 52
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.PictureChecked = Global.BACSOFT.My.Resources.Resources.Checked
        Me.RepositoryItemCheckEdit3.PictureGrayed = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.RepositoryItemCheckEdit3.PictureUnchecked = Global.BACSOFT.My.Resources.Resources.Cancel_18
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "ID"
        Me.GridColumn8.FieldName = "ID"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Width = 58
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Tên"
        Me.GridColumn4.FieldName = "TenNguoiNhan"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 129
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Email nhận"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemHyperLinkEdit2
        Me.GridColumn5.FieldName = "EmailNguoiNhan"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 138
        '
        'RepositoryItemHyperLinkEdit2
        '
        Me.RepositoryItemHyperLinkEdit2.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit2.Name = "RepositoryItemHyperLinkEdit2"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Email gửi"
        Me.GridColumn2.FieldName = "EmailNguoiGui"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 5
        Me.GridColumn2.Width = 141
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Thông báo"
        Me.GridColumn1.FieldName = "GhiChu"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 4
        Me.GridColumn1.Width = 203
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Thời gian"
        Me.GridColumn3.DisplayFormat.FormatString = "HH:mm:ss dd/MM/yyyy"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn3.FieldName = "ThoiGian"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.FixedWidth = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 116
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Người gửi"
        Me.GridColumn6.FieldName = "IdNguoiGui"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowSize = False
        Me.GridColumn6.OptionsColumn.FixedWidth = True
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 79
        '
        'RepositoryItemPopupContainerEdit2
        '
        Me.RepositoryItemPopupContainerEdit2.AutoHeight = False
        Me.RepositoryItemPopupContainerEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit2.Name = "RepositoryItemPopupContainerEdit2"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.RepositoryItemCheckEdit2.PictureChecked = Global.BACSOFT.My.Resources.Resources.Checked
        Me.RepositoryItemCheckEdit2.PictureUnchecked = Global.BACSOFT.My.Resources.Resources.UnCheck
        '
        'frmLogEmailKD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 498)
        Me.Controls.Add(Me.gdvEmail)
        Me.Name = "frmLogEmailKD"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nhật ký gửi Email"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.gdvEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvDataEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdvEmail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvDataEmail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
End Class
