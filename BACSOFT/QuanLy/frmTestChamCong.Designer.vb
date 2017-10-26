<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestChamCong
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.btLayDuLieu = New DevExpress.XtraEditors.SimpleButton()
        Me.gdvChamCong = New DevExpress.XtraGrid.GridControl()
        Me.gdvChamCongCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btKetXuat = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbThang = New DevExpress.XtraEditors.SpinEdit()
        Me.tbNam = New DevExpress.XtraEditors.SpinEdit()
        Me.btXoaCC = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.gdvChamCong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChamCongCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btLayDuLieu
        '
        Me.btLayDuLieu.Image = Global.BACSOFT.My.Resources.Resources.Search_18
        Me.btLayDuLieu.Location = New System.Drawing.Point(199, 12)
        Me.btLayDuLieu.Name = "btLayDuLieu"
        Me.btLayDuLieu.Size = New System.Drawing.Size(92, 23)
        Me.btLayDuLieu.TabIndex = 0
        Me.btLayDuLieu.Text = "Lấy dữ liệu"
        '
        'gdvChamCong
        '
        Me.gdvChamCong.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gdvChamCong.Location = New System.Drawing.Point(13, 41)
        Me.gdvChamCong.MainView = Me.gdvChamCongCT
        Me.gdvChamCong.Name = "gdvChamCong"
        Me.gdvChamCong.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateEdit1})
        Me.gdvChamCong.Size = New System.Drawing.Size(509, 440)
        Me.gdvChamCong.TabIndex = 1
        Me.gdvChamCong.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvChamCongCT})
        '
        'gdvChamCongCT
        '
        Me.gdvChamCongCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvChamCongCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvChamCongCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvChamCongCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvChamCongCT.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 6.75!)
        Me.gdvChamCongCT.Appearance.GroupFooter.Options.UseFont = True
        Me.gdvChamCongCT.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.gdvChamCongCT.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.gdvChamCongCT.Appearance.GroupRow.Options.UseFont = True
        Me.gdvChamCongCT.Appearance.GroupRow.Options.UseForeColor = True
        Me.gdvChamCongCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvChamCongCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvChamCongCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvChamCongCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvChamCongCT.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.gdvChamCongCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvChamCongCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvChamCongCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8, Me.GridColumn4, Me.GridColumn1, Me.GridColumn2, Me.GridColumn5, Me.GridColumn3, Me.GridColumn6, Me.GridColumn7, Me.GridColumn9, Me.GridColumn10})
        Me.gdvChamCongCT.GridControl = Me.gdvChamCong
        Me.gdvChamCongCT.GroupCount = 2
        Me.gdvChamCongCT.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "CheckTime", Me.GridColumn2, "{0:N0}")})
        Me.gdvChamCongCT.Name = "gdvChamCongCT"
        Me.gdvChamCongCT.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvChamCongCT.OptionsBehavior.ReadOnly = True
        Me.gdvChamCongCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvChamCongCT.OptionsView.ShowFooter = True
        Me.gdvChamCongCT.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn8, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn4, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Phòng"
        Me.GridColumn8.FieldName = "DeptName"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nhân viên"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "UserID"
        Me.GridColumn1.FieldName = "Userid"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Ngày"
        Me.GridColumn2.DisplayFormat.FormatString = "dd/MM/yyyy - ddd"
        Me.GridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn2.FieldName = "CheckTime"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 160
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Lần 1"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn5.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn5.FieldName = "Lan1"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 1
        Me.GridColumn5.Width = 52
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "HH:mm"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "HH:mm"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Mask.EditMask = "HH:mm"
        Me.RepositoryItemDateEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        Me.RepositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Lần 2"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn3.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "Lan2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 54
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Lần 3"
        Me.GridColumn6.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn6.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn6.FieldName = "Lan3"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 3
        Me.GridColumn6.Width = 54
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Lần 4"
        Me.GridColumn7.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn7.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn7.FieldName = "Lan4"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 4
        Me.GridColumn7.Width = 56
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Lần 5"
        Me.GridColumn9.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn9.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "Lan5"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 5
        Me.GridColumn9.Width = 56
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Lần 6"
        Me.GridColumn10.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.GridColumn10.DisplayFormat.FormatString = "HH:mm"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn10.FieldName = "Lan6"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 6
        Me.GridColumn10.Width = 59
        '
        'btKetXuat
        '
        Me.btKetXuat.Image = Global.BACSOFT.My.Resources.Resources.Excel_18
        Me.btKetXuat.Location = New System.Drawing.Point(297, 12)
        Me.btKetXuat.Name = "btKetXuat"
        Me.btKetXuat.Size = New System.Drawing.Size(75, 23)
        Me.btKetXuat.TabIndex = 2
        Me.btKetXuat.Text = "Kết xuất"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 17)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl1.TabIndex = 3
        Me.LabelControl1.Text = "Tháng"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(107, 17)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Năm"
        '
        'tbThang
        '
        Me.tbThang.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.tbThang.Location = New System.Drawing.Point(50, 14)
        Me.tbThang.Name = "tbThang"
        Me.tbThang.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThang.Properties.MaxValue = New Decimal(New Integer() {12, 0, 0, 0})
        Me.tbThang.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.tbThang.Size = New System.Drawing.Size(51, 20)
        Me.tbThang.TabIndex = 4
        '
        'tbNam
        '
        Me.tbNam.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.tbNam.Location = New System.Drawing.Point(134, 14)
        Me.tbNam.Name = "tbNam"
        Me.tbNam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbNam.Properties.IsFloatValue = False
        Me.tbNam.Properties.Mask.EditMask = "N00"
        Me.tbNam.Size = New System.Drawing.Size(59, 20)
        Me.tbNam.TabIndex = 4
        '
        'btXoaCC
        '
        Me.btXoaCC.Location = New System.Drawing.Point(378, 12)
        Me.btXoaCC.Name = "btXoaCC"
        Me.btXoaCC.Size = New System.Drawing.Size(102, 23)
        Me.btXoaCC.TabIndex = 5
        Me.btXoaCC.Text = "Xóa chấm công cũ"
        '
        'frmTestChamCong
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 493)
        Me.Controls.Add(Me.btXoaCC)
        Me.Controls.Add(Me.tbNam)
        Me.Controls.Add(Me.tbThang)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btKetXuat)
        Me.Controls.Add(Me.gdvChamCong)
        Me.Controls.Add(Me.btLayDuLieu)
        Me.Name = "frmTestChamCong"
        Me.Text = "Dữ liệu chấm công"
        CType(Me.gdvChamCong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChamCongCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btLayDuLieu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gdvChamCong As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvChamCongCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents btKetXuat As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThang As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents tbNam As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents btXoaCC As DevExpress.XtraEditors.SimpleButton
End Class
