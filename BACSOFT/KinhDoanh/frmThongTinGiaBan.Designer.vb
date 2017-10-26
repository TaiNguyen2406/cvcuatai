<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmThongTinGiaBan
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
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lbVatTu = New DevExpress.XtraEditors.LabelControl()
        Me.lbMaVT = New DevExpress.XtraEditors.LabelControl()
        Me.lbHang = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.gdvGiaNhap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvGiaNhapCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gdvGiaNhap
        '
        Me.gdvGiaNhap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdvGiaNhap.Location = New System.Drawing.Point(2, 22)
        Me.gdvGiaNhap.MainView = Me.gdvGiaNhapCT
        Me.gdvGiaNhap.Name = "gdvGiaNhap"
        Me.gdvGiaNhap.Size = New System.Drawing.Size(643, 255)
        Me.gdvGiaNhap.TabIndex = 0
        Me.gdvGiaNhap.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvGiaNhapCT})
        '
        'gdvGiaNhapCT
        '
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvGiaNhapCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvGiaNhapCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7})
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
        Me.GridColumn1.Width = 96
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
        Me.GridColumn2.Width = 55
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
        Me.GridColumn3.Width = 121
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
        Me.GridColumn4.Width = 54
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "VAT"
        Me.GridColumn5.FieldName = "Xuatthue"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 51
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Mã KH"
        Me.GridColumn6.FieldName = "ttcMa"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        Me.GridColumn6.Width = 198
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "C Trình"
        Me.GridColumn7.FieldName = "CongTrinh"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 5
        Me.GridColumn7.Width = 50
        '
        'lbVatTu
        '
        Me.lbVatTu.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbVatTu.Appearance.ForeColor = System.Drawing.Color.Black
        Me.lbVatTu.Location = New System.Drawing.Point(12, 12)
        Me.lbVatTu.Name = "lbVatTu"
        Me.lbVatTu.Size = New System.Drawing.Size(59, 16)
        Me.lbVatTu.TabIndex = 1
        Me.lbVatTu.Text = "Tên hàng:"
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
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.gdvGiaNhap)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 69)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(647, 279)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "Thông tin xuất kho"
        '
        'frmThongTinGiaBan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 360)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.lbHang)
        Me.Controls.Add(Me.lbMaVT)
        Me.Controls.Add(Me.lbVatTu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmThongTinGiaBan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Thông tin giá bán"
        CType(Me.gdvGiaNhap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvGiaNhapCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
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
    Friend WithEvents lbMaVT As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lbHang As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
