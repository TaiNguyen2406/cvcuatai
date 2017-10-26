<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThongTinGiaNhap
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
        Me.gdvGiaNhap = New DevExpress.XtraGrid.GridControl()
        Me.gdvGiaNhapCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lbVatTu = New DevExpress.XtraEditors.LabelControl()
        Me.lbGiaCungUng = New DevExpress.XtraEditors.LabelControl()
        Me.lbMaVT = New DevExpress.XtraEditors.LabelControl()
        Me.lbHang = New DevExpress.XtraEditors.LabelControl()
        Me.gdvChaoGia = New DevExpress.XtraGrid.GridControl()
        Me.gdvChaoGiaCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.gdvGiaNhap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvGiaNhapCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChaoGia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvChaoGiaCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'gdvGiaNhap
        '
        Me.gdvGiaNhap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvGiaNhap.Location = New System.Drawing.Point(2, 22)
        Me.gdvGiaNhap.MainView = Me.gdvGiaNhapCT
        Me.gdvGiaNhap.Name = "gdvGiaNhap"
        Me.gdvGiaNhap.Size = New System.Drawing.Size(643, 161)
        Me.gdvGiaNhap.TabIndex = 0
        Me.gdvGiaNhap.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvGiaNhapCT})
        '
        'gdvGiaNhapCT
        '
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvGiaNhapCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6})
        Me.gdvGiaNhapCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvGiaNhapCT.GridControl = Me.gdvGiaNhap
        Me.gdvGiaNhapCT.Name = "gdvGiaNhapCT"
        Me.gdvGiaNhapCT.OptionsBehavior.Editable = False
        Me.gdvGiaNhapCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvGiaNhapCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvGiaNhapCT.OptionsView.ShowGroupPanel = False
        Me.gdvGiaNhapCT.RowHeight = 22
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Ngày"
        Me.GridColumn1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn1.FieldName = "Ngaythang"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 81
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn2.Caption = "SL"
        Me.GridColumn2.FieldName = "Soluong"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 47
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn3.Caption = "Đơn giá"
        Me.GridColumn3.DisplayFormat.FormatString = "N2"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "Dongia"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 102
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "VAT"
        Me.GridColumn4.DisplayFormat.FormatString = "{0}%"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn4.FieldName = "Mucthue"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 46
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "VAT"
        Me.GridColumn5.FieldName = "Nhapthue"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 44
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Mã KH"
        Me.GridColumn6.FieldName = "ttcMa"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 165
        '
        'lbVatTu
        '
        Me.lbVatTu.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbVatTu.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lbVatTu.Location = New System.Drawing.Point(12, 12)
        Me.lbVatTu.Name = "lbVatTu"
        Me.lbVatTu.Size = New System.Drawing.Size(63, 16)
        Me.lbVatTu.TabIndex = 1
        Me.lbVatTu.Text = "Tên hàng: "
        '
        'lbGiaCungUng
        '
        Me.lbGiaCungUng.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbGiaCungUng.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lbGiaCungUng.Location = New System.Drawing.Point(12, 56)
        Me.lbGiaCungUng.Name = "lbGiaCungUng"
        Me.lbGiaCungUng.Size = New System.Drawing.Size(88, 16)
        Me.lbGiaCungUng.TabIndex = 1
        Me.lbGiaCungUng.Text = "Giá cung ứng:  "
        '
        'lbMaVT
        '
        Me.lbMaVT.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbMaVT.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lbMaVT.Location = New System.Drawing.Point(12, 34)
        Me.lbMaVT.Name = "lbMaVT"
        Me.lbMaVT.Size = New System.Drawing.Size(62, 16)
        Me.lbMaVT.TabIndex = 1
        Me.lbMaVT.Text = "Mã hàng:  "
        '
        'lbHang
        '
        Me.lbHang.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbHang.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lbHang.Location = New System.Drawing.Point(363, 34)
        Me.lbHang.Name = "lbHang"
        Me.lbHang.Size = New System.Drawing.Size(42, 16)
        Me.lbHang.TabIndex = 1
        Me.lbHang.Text = "Hãng:  "
        '
        'gdvChaoGia
        '
        Me.gdvChaoGia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvChaoGia.Location = New System.Drawing.Point(2, 22)
        Me.gdvChaoGia.MainView = Me.gdvChaoGiaCT
        Me.gdvChaoGia.Name = "gdvChaoGia"
        Me.gdvChaoGia.Size = New System.Drawing.Size(645, 161)
        Me.gdvChaoGia.TabIndex = 2
        Me.gdvChaoGia.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvChaoGiaCT})
        '
        'gdvChaoGiaCT
        '
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvChaoGiaCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvChaoGiaCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn13, Me.GridColumn9, Me.GridColumn14, Me.GridColumn15, Me.GridColumn10, Me.GridColumn11, Me.GridColumn16, Me.GridColumn12})
        Me.gdvChaoGiaCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvChaoGiaCT.GridControl = Me.gdvChaoGia
        Me.gdvChaoGiaCT.Name = "gdvChaoGiaCT"
        Me.gdvChaoGiaCT.OptionsBehavior.Editable = False
        Me.gdvChaoGiaCT.OptionsFilter.AllowFilterEditor = False
        Me.gdvChaoGiaCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvChaoGiaCT.OptionsView.ColumnAutoWidth = False
        Me.gdvChaoGiaCT.OptionsView.ShowGroupPanel = False
        Me.gdvChaoGiaCT.RowHeight = 22
        '
        'GridColumn7
        '
        Me.GridColumn7.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn7.Caption = "Ngày"
        Me.GridColumn7.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn7.FieldName = "Ngaythang"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 0
        Me.GridColumn7.Width = 96
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn8.Caption = "SL"
        Me.GridColumn8.FieldName = "Soluong"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 1
        Me.GridColumn8.Width = 53
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "GB (%)"
        Me.GridColumn13.DisplayFormat.FormatString = "N2"
        Me.GridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn13.FieldName = "GiaBanPT"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 2
        Me.GridColumn13.Width = 58
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn9.Caption = "Đơn giá"
        Me.GridColumn9.DisplayFormat.FormatString = "N2"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn9.FieldName = "Dongia"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 3
        Me.GridColumn9.Width = 97
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "CK (%)"
        Me.GridColumn14.DisplayFormat.FormatString = "N2"
        Me.GridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn14.FieldName = "ChietKhauPT"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 4
        Me.GridColumn14.Width = 52
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "CK"
        Me.GridColumn15.DisplayFormat.FormatString = "N2"
        Me.GridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn15.FieldName = "ChietKhau"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 5
        Me.GridColumn15.Width = 85
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "VAT"
        Me.GridColumn10.DisplayFormat.FormatString = "{0}%"
        Me.GridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.GridColumn10.FieldName = "Mucthue"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 6
        Me.GridColumn10.Width = 37
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "VAT"
        Me.GridColumn11.FieldName = "Xuatthue"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 7
        Me.GridColumn11.Width = 32
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "C Trình"
        Me.GridColumn16.FieldName = "CongTrinh"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 8
        Me.GridColumn16.Width = 50
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Mã KH"
        Me.GridColumn12.FieldName = "ttcMa"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 9
        Me.GridColumn12.Width = 155
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.gdvGiaNhap)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 78)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(647, 185)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "Thông tin nhập hàng"
        '
        'GroupControl2
        '
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.gdvChaoGia)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 267)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(649, 185)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Thông tin chào giá"
        '
        'frmThongTinGiaNhap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 464)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.lbHang)
        Me.Controls.Add(Me.lbMaVT)
        Me.Controls.Add(Me.lbGiaCungUng)
        Me.Controls.Add(Me.lbVatTu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmThongTinGiaNhap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thông tin giá nhập, chào giá"
        CType(Me.gdvGiaNhap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvGiaNhapCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChaoGia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvChaoGiaCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gdvGiaNhap As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvGiaNhapCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbVatTu As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbGiaCungUng As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbMaVT As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbHang As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gdvChaoGia As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvChaoGiaCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
End Class
