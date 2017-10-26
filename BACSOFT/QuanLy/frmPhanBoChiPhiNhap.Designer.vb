<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhanBoChiPhiNhap
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.btPhanBoChiPhi = New DevExpress.XtraEditors.SimpleButton()
        Me.btTaiDS = New DevExpress.XtraEditors.SimpleButton()
        Me.chkThoiGianNhap = New DevExpress.XtraEditors.CheckEdit()
        Me.chkPhieuChi = New DevExpress.XtraEditors.CheckEdit()
        Me.tbDenNgay = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTuNgay = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.gdvQuaTrinhBaoGia = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn49 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn50 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.chkThoiGianNhap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPhieuChi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTuNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvQuaTrinhBaoGia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.btPhanBoChiPhi)
        Me.GroupControl1.Controls.Add(Me.btTaiDS)
        Me.GroupControl1.Controls.Add(Me.chkThoiGianNhap)
        Me.GroupControl1.Controls.Add(Me.chkPhieuChi)
        Me.GroupControl1.Controls.Add(Me.tbDenNgay)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.tbTuNgay)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(537, 55)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Tiêu chí"
        '
        'btPhanBoChiPhi
        '
        Me.btPhanBoChiPhi.Image = Global.BACSOFT.My.Resources.Resources.Start_16
        Me.btPhanBoChiPhi.Location = New System.Drawing.Point(267, 33)
        Me.btPhanBoChiPhi.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btPhanBoChiPhi.Name = "btPhanBoChiPhi"
        Me.btPhanBoChiPhi.Size = New System.Drawing.Size(111, 19)
        Me.btPhanBoChiPhi.TabIndex = 6
        Me.btPhanBoChiPhi.Text = "Phân bổ chi phí"
        '
        'btTaiDS
        '
        Me.btTaiDS.Image = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.btTaiDS.Location = New System.Drawing.Point(267, 10)
        Me.btTaiDS.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btTaiDS.Name = "btTaiDS"
        Me.btTaiDS.Size = New System.Drawing.Size(111, 19)
        Me.btTaiDS.TabIndex = 5
        Me.btTaiDS.Text = "Tải danh sách"
        '
        'chkThoiGianNhap
        '
        Me.chkThoiGianNhap.Location = New System.Drawing.Point(151, 32)
        Me.chkThoiGianNhap.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkThoiGianNhap.Name = "chkThoiGianNhap"
        Me.chkThoiGianNhap.Properties.Caption = "Thời gian nhập"
        Me.chkThoiGianNhap.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.chkThoiGianNhap.Properties.RadioGroupIndex = 0
        Me.chkThoiGianNhap.Size = New System.Drawing.Size(102, 19)
        Me.chkThoiGianNhap.TabIndex = 3
        Me.chkThoiGianNhap.TabStop = False
        '
        'chkPhieuChi
        '
        Me.chkPhieuChi.EditValue = True
        Me.chkPhieuChi.Location = New System.Drawing.Point(57, 32)
        Me.chkPhieuChi.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.chkPhieuChi.Name = "chkPhieuChi"
        Me.chkPhieuChi.Properties.Caption = "Thời gian chi"
        Me.chkPhieuChi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.chkPhieuChi.Properties.RadioGroupIndex = 0
        Me.chkPhieuChi.Size = New System.Drawing.Size(89, 19)
        Me.chkPhieuChi.TabIndex = 2
        '
        'tbDenNgay
        '
        Me.tbDenNgay.EditValue = Nothing
        Me.tbDenNgay.Location = New System.Drawing.Point(168, 10)
        Me.tbDenNgay.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbDenNgay.Name = "tbDenNgay"
        Me.tbDenNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.tbDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbDenNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbDenNgay.Size = New System.Drawing.Size(86, 20)
        Me.tbDenNgay.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(151, 12)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(12, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "->"
        '
        'tbTuNgay
        '
        Me.tbTuNgay.EditValue = Nothing
        Me.tbTuNgay.Location = New System.Drawing.Point(60, 10)
        Me.tbTuNgay.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tbTuNgay.Name = "tbTuNgay"
        Me.tbTuNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.tbTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbTuNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbTuNgay.Size = New System.Drawing.Size(86, 20)
        Me.tbTuNgay.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 12)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Từ ngày"
        '
        'gdv
        '
        Me.gdv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdv.Location = New System.Drawing.Point(0, 55)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoEdit1})
        Me.gdv.Size = New System.Drawing.Size(537, 518)
        Me.gdv.TabIndex = 7
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT, Me.gdvQuaTrinhBaoGia})
        '
        'gdvCT
        '
        Me.gdvCT.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gdvCT.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.gdvCT.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.FocusedRow.Options.UseBackColor = True
        Me.gdvCT.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvCT.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvCT.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvCT.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvCT.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.gdvCT.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsFind.AllowFindPanel = False
        Me.gdvCT.OptionsSelection.MultiSelect = True
        Me.gdvCT.OptionsView.ColumnAutoWidth = False
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowAutoFilterRow = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        Me.gdvCT.RowHeight = 22
        Me.gdvCT.Tag = "GridChiTiet"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "STT"
        Me.GridColumn1.FieldName = "STT"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Phiếu nhập"
        Me.GridColumn2.FieldName = "SoPhieuNK"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 90
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Trạng thái"
        Me.GridColumn3.FieldName = "TrangThai"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 3
        Me.GridColumn3.Width = 90
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Ghi chú"
        Me.GridColumn4.FieldName = "GhiChu"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        Me.GridColumn4.Width = 182
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemMemoEdit1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'gdvQuaTrinhBaoGia
        '
        Me.gdvQuaTrinhBaoGia.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvQuaTrinhBaoGia.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvQuaTrinhBaoGia.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvQuaTrinhBaoGia.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvQuaTrinhBaoGia.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn24, Me.GridColumn40, Me.GridColumn48, Me.GridColumn49, Me.GridColumn50, Me.GridColumn51, Me.GridColumn53})
        Me.gdvQuaTrinhBaoGia.GridControl = Me.gdv
        Me.gdvQuaTrinhBaoGia.Name = "gdvQuaTrinhBaoGia"
        Me.gdvQuaTrinhBaoGia.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvQuaTrinhBaoGia.OptionsBehavior.ReadOnly = True
        Me.gdvQuaTrinhBaoGia.OptionsView.ColumnAutoWidth = False
        Me.gdvQuaTrinhBaoGia.OptionsView.RowAutoHeight = True
        Me.gdvQuaTrinhBaoGia.OptionsView.ShowGroupPanel = False
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Thời gian"
        Me.GridColumn14.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.GridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn14.FieldName = "ThoiGianBaoGia"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 0
        Me.GridColumn14.Width = 130
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Người báo giá"
        Me.GridColumn17.FieldName = "NguoiBaoGia"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 1
        Me.GridColumn17.Width = 169
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Giá"
        Me.GridColumn18.DisplayFormat.FormatString = "N2"
        Me.GridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn18.FieldName = "Gia"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 2
        Me.GridColumn18.Width = 101
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Tiền tệ"
        Me.GridColumn19.FieldName = "TienTe"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 3
        Me.GridColumn19.Width = 50
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Thời gian cung ứng"
        Me.GridColumn24.FieldName = "ThoiGianCungUng"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 4
        Me.GridColumn24.Width = 132
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "Ghi chú"
        Me.GridColumn40.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.GridColumn40.FieldName = "GhiChu"
        Me.GridColumn40.Name = "GridColumn40"
        Me.GridColumn40.Visible = True
        Me.GridColumn40.VisibleIndex = 5
        Me.GridColumn40.Width = 262
        '
        'GridColumn48
        '
        Me.GridColumn48.Caption = "Chào giá"
        Me.GridColumn48.FieldName = "ChaoGia"
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.Visible = True
        Me.GridColumn48.VisibleIndex = 6
        Me.GridColumn48.Width = 60
        '
        'GridColumn49
        '
        Me.GridColumn49.Caption = "Đặt hàng"
        Me.GridColumn49.FieldName = "DatHang"
        Me.GridColumn49.Name = "GridColumn49"
        Me.GridColumn49.Visible = True
        Me.GridColumn49.VisibleIndex = 7
        Me.GridColumn49.Width = 61
        '
        'GridColumn50
        '
        Me.GridColumn50.Caption = "IDYeuCau"
        Me.GridColumn50.FieldName = "IDYeuCau"
        Me.GridColumn50.Name = "GridColumn50"
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "IDCungUng"
        Me.GridColumn51.FieldName = "IDCungUng"
        Me.GridColumn51.Name = "GridColumn51"
        '
        'GridColumn53
        '
        Me.GridColumn53.Caption = "ID"
        Me.GridColumn53.FieldName = "ID"
        Me.GridColumn53.Name = "GridColumn53"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Nhập khẩu"
        Me.GridColumn5.FieldName = "NhapKhau"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        '
        'frmPhanBoChiPhiNhap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 573)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.GroupControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPhanBoChiPhiNhap"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Phân bổ chi phí nhập"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.chkThoiGianNhap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPhieuChi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTuNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvQuaTrinhBaoGia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents tbTuNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbDenNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents chkPhieuChi As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkThoiGianNhap As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btTaiDS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btPhanBoChiPhi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents gdvQuaTrinhBaoGia As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn49 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn50 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
