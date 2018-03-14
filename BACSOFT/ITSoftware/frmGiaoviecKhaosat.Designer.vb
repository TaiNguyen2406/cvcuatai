<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGiaoviecKhaosat
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grdGiaoviec = New DevExpress.XtraGrid.GridControl()
        Me.grdViewGiaoviec = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemLookUpEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckedComboBoxEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckedComboBoxEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.grdGiaoviec,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grdViewGiaoviec,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemLookUpEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemMemoEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemCheckedComboBoxEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemCheckedComboBoxEdit2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemMemoEdit3,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemPopupContainerEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grdGiaoviec
        '
        Me.grdGiaoviec.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdGiaoviec.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdGiaoviec.Location = New System.Drawing.Point(0, 0)
        Me.grdGiaoviec.MainView = Me.grdViewGiaoviec
        Me.grdGiaoviec.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdGiaoviec.Name = "grdGiaoviec"
        Me.grdGiaoviec.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit3, Me.RepositoryItemPopupContainerEdit1, Me.RepositoryItemMemoEdit1, Me.RepositoryItemDateEdit1, Me.RepositoryItemDateEdit2, Me.RepositoryItemCheckedComboBoxEdit1, Me.RepositoryItemCheckedComboBoxEdit2, Me.RepositoryItemLookUpEdit1})
        Me.grdGiaoviec.Size = New System.Drawing.Size(1534, 458)
        Me.grdGiaoviec.TabIndex = 8
        Me.grdGiaoviec.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewGiaoviec})
        '
        'grdViewGiaoviec
        '
        Me.grdViewGiaoviec.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewGiaoviec.Appearance.FocusedRow.Options.UseBackColor = true
        Me.grdViewGiaoviec.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewGiaoviec.Appearance.HideSelectionRow.Options.UseBackColor = true
        Me.grdViewGiaoviec.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewGiaoviec.Appearance.SelectedRow.Options.UseBackColor = true
        Me.grdViewGiaoviec.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn8})
        Me.grdViewGiaoviec.CustomizationFormBounds = New System.Drawing.Rectangle(909, 273, 216, 183)
        Me.grdViewGiaoviec.GridControl = Me.grdGiaoviec
        Me.grdViewGiaoviec.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.grdViewGiaoviec.Name = "grdViewGiaoviec"
        Me.grdViewGiaoviec.OptionsView.ColumnAutoWidth = false
        Me.grdViewGiaoviec.OptionsView.RowAutoHeight = true
        Me.grdViewGiaoviec.OptionsView.ShowGroupPanel = false
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "STT"
        Me.GridColumn7.MinWidth = 60
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Width = 63
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn1.AppearanceCell.Options.UseFont = true
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.GridColumn1.AppearanceHeader.Options.UseFont = true
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = true
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Nội dung công việc"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemLookUpEdit1
        Me.GridColumn1.FieldName = "IdLoaicongviec"
        Me.GridColumn1.MinWidth = 200
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = true
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 211
        '
        'RepositoryItemLookUpEdit1
        '
        Me.RepositoryItemLookUpEdit1.AutoHeight = false
        Me.RepositoryItemLookUpEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemLookUpEdit1.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdLoaicongviec", "IdLoaicongviec", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tencongviec", 140, "Tencongviec")})
        Me.RepositoryItemLookUpEdit1.Name = "RepositoryItemLookUpEdit1"
        Me.RepositoryItemLookUpEdit1.NullText = ""
        Me.RepositoryItemLookUpEdit1.ShowHeader = false
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn2.AppearanceCell.Options.UseFont = true
        Me.GridColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn2.AppearanceHeader.Options.UseFont = true
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = true
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Mô tả"
        Me.GridColumn2.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn2.FieldName = "Motacongviec"
        Me.GridColumn2.MinWidth = 300
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = true
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 318
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = true
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn3.AppearanceHeader.Options.UseFont = true
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = true
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Bắt đầu"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn3.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "Batdau"
        Me.GridColumn3.MinWidth = 130
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = true
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 130
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = false
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn4.AppearanceCell.Options.UseFont = true
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = true
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn4.AppearanceHeader.Options.UseFont = true
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = true
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Kết thúc"
        Me.GridColumn4.ColumnEdit = Me.RepositoryItemDateEdit2
        Me.GridColumn4.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn4.FieldName = "Kethuc"
        Me.GridColumn4.MinWidth = 130
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = true
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 130
        '
        'RepositoryItemDateEdit2
        '
        Me.RepositoryItemDateEdit2.AutoHeight = false
        Me.RepositoryItemDateEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit2.DisplayFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit2.EditFormat.FormatString = "HH:mm dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit2.Mask.EditMask = "HH:mm dd/MM/yyyy"
        Me.RepositoryItemDateEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit2.Name = "RepositoryItemDateEdit2"
        Me.RepositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn5.AppearanceCell.Options.UseFont = true
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = true
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = true
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "NV thực hiện"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemCheckedComboBoxEdit1
        Me.GridColumn5.FieldName = "IdDanhsachnguoiNhanviec"
        Me.GridColumn5.MinWidth = 250
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = true
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 250
        '
        'RepositoryItemCheckedComboBoxEdit1
        '
        Me.RepositoryItemCheckedComboBoxEdit1.AutoHeight = false
        Me.RepositoryItemCheckedComboBoxEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCheckedComboBoxEdit1.Name = "RepositoryItemCheckedComboBoxEdit1"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn6.AppearanceCell.Options.UseFont = true
        Me.GridColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn6.AppearanceHeader.Options.UseFont = true
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = true
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "NV thông báo"
        Me.GridColumn6.ColumnEdit = Me.RepositoryItemCheckedComboBoxEdit2
        Me.GridColumn6.FieldName = "IdDanhsachnguoiThongbao"
        Me.GridColumn6.MinWidth = 250
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = true
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 357
        '
        'RepositoryItemCheckedComboBoxEdit2
        '
        Me.RepositoryItemCheckedComboBoxEdit2.AutoHeight = false
        Me.RepositoryItemCheckedComboBoxEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCheckedComboBoxEdit2.Name = "RepositoryItemCheckedComboBoxEdit2"
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Id"
        Me.GridColumn8.FieldName = "Id"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'RepositoryItemMemoEdit3
        '
        Me.RepositoryItemMemoEdit3.Name = "RepositoryItemMemoEdit3"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.AutoHeight = false
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = true
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(1413, 466)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(114, 41)
        Me.btDong.TabIndex = 10
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = true
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(1265, 466)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(137, 41)
        Me.btLuuLai.TabIndex = 9
        Me.btLuuLai.Text = "Lưu lại"
        '
        'frmGiaoviecKhaosat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1534, 518)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.grdGiaoviec)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmGiaoviecKhaosat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Giao việc khảo sát phần mềm"
        CType(Me.grdGiaoviec,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grdViewGiaoviec,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemLookUpEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemMemoEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit2.VistaTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckedComboBoxEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemCheckedComboBoxEdit2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemMemoEdit3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemPopupContainerEdit1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents grdGiaoviec As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewGiaoviec As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemMemoEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemDateEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemCheckedComboBoxEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit
    Friend WithEvents RepositoryItemCheckedComboBoxEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemLookUpEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
End Class
