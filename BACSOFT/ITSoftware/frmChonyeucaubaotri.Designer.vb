<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChonyeucaubaotri
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDulieubaotri = New DevExpress.XtraGrid.GridControl()
        Me.grdViewDulieubaotri = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grdDulieubaotri, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewDulieubaotri, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.grdDulieubaotri)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 3)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1025, 361)
        Me.GroupControl1.TabIndex = 25
        Me.GroupControl1.Text = "Yêu cầu bảo trì"
        '
        'grdDulieubaotri
        '
        Me.grdDulieubaotri.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDulieubaotri.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDulieubaotri.Location = New System.Drawing.Point(2, 25)
        Me.grdDulieubaotri.MainView = Me.grdViewDulieubaotri
        Me.grdDulieubaotri.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDulieubaotri.Name = "grdDulieubaotri"
        Me.grdDulieubaotri.Size = New System.Drawing.Size(1021, 334)
        Me.grdDulieubaotri.TabIndex = 1
        Me.grdDulieubaotri.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewDulieubaotri})
        '
        'grdViewDulieubaotri
        '
        Me.grdViewDulieubaotri.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDulieubaotri.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdViewDulieubaotri.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDulieubaotri.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdViewDulieubaotri.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDulieubaotri.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdViewDulieubaotri.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn7, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn10})
        Me.grdViewDulieubaotri.GridControl = Me.grdDulieubaotri
        Me.grdViewDulieubaotri.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.grdViewDulieubaotri.Name = "grdViewDulieubaotri"
        Me.grdViewDulieubaotri.OptionsView.ColumnAutoWidth = False
        Me.grdViewDulieubaotri.OptionsView.ShowAutoFilterRow = True
        Me.grdViewDulieubaotri.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "STT"
        Me.GridColumn1.FieldName = "Stt"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 43
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "[x]"
        Me.GridColumn7.FieldName = "checkbox"
        Me.GridColumn7.MaxWidth = 30
        Me.GridColumn7.MinWidth = 30
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        Me.GridColumn7.Width = 30
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn2.AppearanceHeader.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Mã vật tư"
        Me.GridColumn2.FieldName = "IdVatTu"
        Me.GridColumn2.MaxWidth = 90
        Me.GridColumn2.MinWidth = 90
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Width = 90
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Ngày yêu cầu"
        Me.GridColumn3.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn3.FieldName = "Ngaythongbao"
        Me.GridColumn3.MaxWidth = 100
        Me.GridColumn3.MinWidth = 100
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 100
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn4.AppearanceHeader.Options.UseFont = True
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn4.Caption = "Người yêu cầu"
        Me.GridColumn4.FieldName = "Nguoithongbao"
        Me.GridColumn4.MaxWidth = 100
        Me.GridColumn4.MinWidth = 100
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 100
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.GridColumn5.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn5.AppearanceHeader.Options.UseFont = True
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn5.Caption = "Nội dung yêu cầu"
        Me.GridColumn5.FieldName = "Noidungthongbao"
        Me.GridColumn5.MaxWidth = 400
        Me.GridColumn5.MinWidth = 400
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 400
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn6.AppearanceHeader.Options.UseFont = True
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn6.Caption = "Trạng thái xử lý"
        Me.GridColumn6.FieldName = "Trangthaixuly"
        Me.GridColumn6.MaxWidth = 100
        Me.GridColumn6.MinWidth = 100
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.OptionsColumn.AllowEdit = False
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 100
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn21.AppearanceHeader.Options.UseFont = True
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn21.Caption = "Người nhận xử lý"
        Me.GridColumn21.FieldName = "Nguoinhanxuly"
        Me.GridColumn21.MaxWidth = 140
        Me.GridColumn21.MinWidth = 140
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.OptionsColumn.AllowEdit = False
        Me.GridColumn21.Visible = True
        Me.GridColumn21.VisibleIndex = 6
        Me.GridColumn21.Width = 140
        '
        'GridColumn22
        '
        Me.GridColumn22.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn22.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn22.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn22.AppearanceHeader.Options.UseFont = True
        Me.GridColumn22.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn22.Caption = "Ngày nhận xử lý"
        Me.GridColumn22.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn22.FieldName = "Ngaynhanxuly"
        Me.GridColumn22.MaxWidth = 100
        Me.GridColumn22.MinWidth = 100
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.OptionsColumn.AllowEdit = False
        Me.GridColumn22.Visible = True
        Me.GridColumn22.VisibleIndex = 7
        Me.GridColumn22.Width = 100
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridColumn23.AppearanceHeader.Options.UseFont = True
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn23.Caption = "Ghi chú nhận xử lý"
        Me.GridColumn23.FieldName = "Ghichu"
        Me.GridColumn23.MaxWidth = 450
        Me.GridColumn23.MinWidth = 450
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.OptionsColumn.AllowEdit = False
        Me.GridColumn23.Visible = True
        Me.GridColumn23.VisibleIndex = 8
        Me.GridColumn23.Width = 450
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "RecordId"
        Me.GridColumn10.FieldName = "Id"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_24
        Me.btDong.Location = New System.Drawing.Point(935, 372)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(91, 41)
        Me.btDong.TabIndex = 3
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_24
        Me.btLuuLai.Location = New System.Drawing.Point(809, 372)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(115, 41)
        Me.btLuuLai.TabIndex = 2
        Me.btLuuLai.Text = "OK"
        '
        'frmChonyeucaubaotri
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1032, 419)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChonyeucaubaotri"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Chọn yêu cầu bảo trì"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grdDulieubaotri, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewDulieubaotri, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdDulieubaotri As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewDulieubaotri As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
