<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNHinhThucTT
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.tbHinhThucTT_ENG = New DevExpress.XtraEditors.TextEdit()
        Me.tbHinhThucTT_VIE = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.gdvTT = New DevExpress.XtraGrid.GridControl()
        Me.gdvTTCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbThoiDiemTT = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btLuuVaThem = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuVaDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.tbHinhThucTT_ENG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbHinhThucTT_VIE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.gdvTT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvTTCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbThoiDiemTT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 34)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(87, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Hình thức TT - VIE"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.tbHinhThucTT_ENG)
        Me.GroupControl1.Controls.Add(Me.tbHinhThucTT_VIE)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(650, 89)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Thông tin chung"
        '
        'tbHinhThucTT_ENG
        '
        Me.tbHinhThucTT_ENG.Location = New System.Drawing.Point(109, 57)
        Me.tbHinhThucTT_ENG.Name = "tbHinhThucTT_ENG"
        Me.tbHinhThucTT_ENG.Size = New System.Drawing.Size(530, 20)
        Me.tbHinhThucTT_ENG.TabIndex = 1
        '
        'tbHinhThucTT_VIE
        '
        Me.tbHinhThucTT_VIE.Location = New System.Drawing.Point(109, 31)
        Me.tbHinhThucTT_VIE.Name = "tbHinhThucTT_VIE"
        Me.tbHinhThucTT_VIE.Size = New System.Drawing.Size(530, 20)
        Me.tbHinhThucTT_VIE.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(10, 60)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(91, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Hình thức TT - ENG"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.gdvTT)
        Me.GroupControl2.Location = New System.Drawing.Point(2, 97)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(650, 203)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "Chi tiết"
        '
        'gdvTT
        '
        Me.gdvTT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvTT.Location = New System.Drawing.Point(2, 22)
        Me.gdvTT.MainView = Me.gdvTTCT
        Me.gdvTT.Name = "gdvTT"
        Me.gdvTT.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbThoiDiemTT})
        Me.gdvTT.Size = New System.Drawing.Size(646, 179)
        Me.gdvTT.TabIndex = 22
        Me.gdvTT.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvTTCT})
        '
        'gdvTTCT
        '
        Me.gdvTTCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvTTCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvTTCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvTTCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvTTCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn34, Me.GridColumn35, Me.GridColumn50, Me.GridColumn49})
        Me.gdvTTCT.GridControl = Me.gdvTT
        Me.gdvTTCT.Name = "gdvTTCT"
        Me.gdvTTCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.gdvTTCT.OptionsView.ShowGroupPanel = False
        Me.gdvTTCT.RowHeight = 22
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "ID"
        Me.GridColumn34.FieldName = "ID"
        Me.GridColumn34.Name = "GridColumn34"
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn35.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn35.Caption = "Thanh toán (%)"
        Me.GridColumn35.FieldName = "ThanhToanPT"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Visible = True
        Me.GridColumn35.VisibleIndex = 0
        Me.GridColumn35.Width = 107
        '
        'GridColumn50
        '
        Me.GridColumn50.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn50.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn50.Caption = "Số ngày"
        Me.GridColumn50.FieldName = "Ngay"
        Me.GridColumn50.Name = "GridColumn50"
        Me.GridColumn50.Visible = True
        Me.GridColumn50.VisibleIndex = 1
        Me.GridColumn50.Width = 81
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "Thời điểm"
        Me.GridColumn49.ColumnEdit = Me.rcbThoiDiemTT
        Me.GridColumn49.FieldName = "IDThoiDiemTT"
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.Visible = True
        Me.GridColumn49.VisibleIndex = 2
        Me.GridColumn49.Width = 440
        '
        'rcbThoiDiemTT
        '
        Me.rcbThoiDiemTT.AutoHeight = False
        Me.rcbThoiDiemTT.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.rcbThoiDiemTT.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ma", "Ma", 20, DevExpress.Utils.FormatType.None, "", False, DevExpress.Utils.HorzAlignment.[Default]), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung", "NoiDung")})
        Me.rcbThoiDiemTT.DisplayMember = "NoiDung"
        Me.rcbThoiDiemTT.DropDownItemHeight = 22
        Me.rcbThoiDiemTT.Name = "rcbThoiDiemTT"
        Me.rcbThoiDiemTT.NullText = ""
        Me.rcbThoiDiemTT.ShowHeader = False
        Me.rcbThoiDiemTT.ValueMember = "Ma"
        '
        'btLuuVaThem
        '
        Me.btLuuVaThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuVaThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaThem.Appearance.Options.UseFont = True
        Me.btLuuVaThem.Image = Global.BACSOFT.My.Resources.Resources.Save_AddNew_18
        Me.btLuuVaThem.Location = New System.Drawing.Point(339, 313)
        Me.btLuuVaThem.Name = "btLuuVaThem"
        Me.btLuuVaThem.Size = New System.Drawing.Size(112, 31)
        Me.btLuuVaThem.TabIndex = 3
        Me.btLuuVaThem.Text = "Lưu và thêm"
        '
        'btLuuVaDong
        '
        Me.btLuuVaDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuVaDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaDong.Appearance.Options.UseFont = True
        Me.btLuuVaDong.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuVaDong.Location = New System.Drawing.Point(457, 313)
        Me.btLuuVaDong.Name = "btLuuVaDong"
        Me.btLuuVaDong.Size = New System.Drawing.Size(108, 31)
        Me.btLuuVaDong.TabIndex = 2
        Me.btLuuVaDong.Text = "Lưu và đóng"
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(571, 313)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(70, 31)
        Me.btDong.TabIndex = 4
        Me.btDong.Text = "Đóng"
        '
        'frmCNHinhThucTT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 356)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuVaDong)
        Me.Controls.Add(Me.btLuuVaThem)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmCNHinhThucTT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật hình thức thanh toán"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.tbHinhThucTT_ENG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbHinhThucTT_VIE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.gdvTT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvTTCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbThoiDiemTT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbHinhThucTT_ENG As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tbHinhThucTT_VIE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents gdvTT As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvTTCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbThoiDiemTT As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btLuuVaThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuVaDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
End Class
