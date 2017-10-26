<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBaoCaoThueChiTietVatTuHangHoa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBaoCaoThueChiTietVatTuHangHoa))
        Me.chkGhiSo = New DevExpress.XtraEditors.CheckEdit()
        Me.btInHoaDon = New DevExpress.XtraEditors.DropDownButton()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.txtDenNgay = New DevExpress.XtraEditors.DateEdit()
        Me.txtTuNgay = New DevExpress.XtraEditors.DateEdit()
        Me.lblDenNgay = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTieuChi = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNam = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbDoiTuong = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtTongHopCongNoPhaiThu = New DevExpress.XtraEditors.MemoEdit()
        Me.txtTongHopCongNoPhaiTra = New DevExpress.XtraEditors.MemoEdit()
        Me.txtChiTietCongNoPhaiThu = New DevExpress.XtraEditors.MemoEdit()
        Me.txtChiTietCongNoPhaiTra = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTieuChi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbDoiTuong.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTongHopCongNoPhaiThu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTongHopCongNoPhaiTra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChiTietCongNoPhaiThu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChiTietCongNoPhaiTra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkGhiSo
        '
        Me.chkGhiSo.EditValue = True
        Me.chkGhiSo.Location = New System.Drawing.Point(12, 429)
        Me.chkGhiSo.Name = "chkGhiSo"
        Me.chkGhiSo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.chkGhiSo.Properties.Appearance.ForeColor = System.Drawing.Color.Green
        Me.chkGhiSo.Properties.Appearance.Options.UseFont = True
        Me.chkGhiSo.Properties.Appearance.Options.UseForeColor = True
        Me.chkGhiSo.Properties.Caption = "Lấy những bút toán đã ghi sổ"
        Me.chkGhiSo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.chkGhiSo.Properties.PictureChecked = CType(resources.GetObject("chkGhiSo.Properties.PictureChecked"), System.Drawing.Image)
        Me.chkGhiSo.Properties.PictureUnchecked = CType(resources.GetObject("chkGhiSo.Properties.PictureUnchecked"), System.Drawing.Image)
        Me.chkGhiSo.Size = New System.Drawing.Size(219, 21)
        Me.chkGhiSo.TabIndex = 65
        '
        'btInHoaDon
        '
        Me.btInHoaDon.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btInHoaDon.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btInHoaDon.Appearance.Image = CType(resources.GetObject("btInHoaDon.Appearance.Image"), System.Drawing.Image)
        Me.btInHoaDon.Appearance.Options.UseFont = True
        Me.btInHoaDon.Appearance.Options.UseImage = True
        Me.btInHoaDon.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btInHoaDon.Image = Global.BACSOFT.My.Resources.Resources.refresh_24
        Me.btInHoaDon.Location = New System.Drawing.Point(540, 483)
        Me.btInHoaDon.Name = "btInHoaDon"
        Me.btInHoaDon.ShowArrowButton = False
        Me.btInHoaDon.Size = New System.Drawing.Size(92, 36)
        Me.btInHoaDon.TabIndex = 63
        Me.btInHoaDon.Text = "Báo cáo"
        '
        'btnDong
        '
        Me.btnDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDong.Image = CType(resources.GetObject("btnDong.Image"), System.Drawing.Image)
        Me.btnDong.Location = New System.Drawing.Point(636, 483)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(87, 36)
        Me.btnDong.TabIndex = 64
        Me.btnDong.Text = "Đóng"
        Me.btnDong.ToolTipTitle = "Đóng"
        '
        'txtDenNgay
        '
        Me.txtDenNgay.EditValue = Nothing
        Me.txtDenNgay.Location = New System.Drawing.Point(293, 64)
        Me.txtDenNgay.Name = "txtDenNgay"
        Me.txtDenNgay.Properties.Appearance.Options.UseTextOptions = True
        Me.txtDenNgay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtDenNgay.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtDenNgay.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtDenNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.txtDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.txtDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.txtDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtDenNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtDenNgay.Size = New System.Drawing.Size(117, 20)
        Me.txtDenNgay.TabIndex = 62
        '
        'txtTuNgay
        '
        Me.txtTuNgay.EditValue = Nothing
        Me.txtTuNgay.Location = New System.Drawing.Point(95, 64)
        Me.txtTuNgay.Name = "txtTuNgay"
        Me.txtTuNgay.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTuNgay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtTuNgay.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTuNgay.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.txtTuNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.txtTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.txtTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.txtTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.txtTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.txtTuNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtTuNgay.Size = New System.Drawing.Size(117, 20)
        Me.txtTuNgay.TabIndex = 61
        '
        'lblDenNgay
        '
        Me.lblDenNgay.Location = New System.Drawing.Point(233, 67)
        Me.lblDenNgay.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lblDenNgay.Name = "lblDenNgay"
        Me.lblDenNgay.Size = New System.Drawing.Size(47, 13)
        Me.lblDenNgay.TabIndex = 60
        Me.lblDenNgay.Text = "Đến ngày"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(19, 67)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl3.TabIndex = 59
        Me.LabelControl3.Text = "Từ ngày"
        '
        'cmbTieuChi
        '
        Me.cmbTieuChi.Location = New System.Drawing.Point(95, 37)
        Me.cmbTieuChi.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.cmbTieuChi.Name = "cmbTieuChi"
        Me.cmbTieuChi.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.cmbTieuChi.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.cmbTieuChi.Properties.Appearance.Options.UseFont = True
        Me.cmbTieuChi.Properties.Appearance.Options.UseForeColor = True
        Me.cmbTieuChi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTieuChi.Properties.DropDownItemHeight = 25
        Me.cmbTieuChi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbTieuChi.Size = New System.Drawing.Size(315, 19)
        Me.cmbTieuChi.TabIndex = 58
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(19, 42)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl2.TabIndex = 57
        Me.LabelControl2.Text = "Thời gian"
        '
        'txtNam
        '
        Me.txtNam.EditValue = New Decimal(New Integer() {2016, 0, 0, 0})
        Me.txtNam.Location = New System.Drawing.Point(95, 11)
        Me.txtNam.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtNam.Name = "txtNam"
        Me.txtNam.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtNam.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtNam.Properties.Appearance.Options.UseFont = True
        Me.txtNam.Properties.Appearance.Options.UseForeColor = True
        Me.txtNam.Properties.Appearance.Options.UseTextOptions = True
        Me.txtNam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtNam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNam.Properties.IsFloatValue = False
        Me.txtNam.Properties.MaxValue = New Decimal(New Integer() {2020, 0, 0, 0})
        Me.txtNam.Properties.MinValue = New Decimal(New Integer() {2016, 0, 0, 0})
        Me.txtNam.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.txtNam.Size = New System.Drawing.Size(62, 19)
        Me.txtNam.TabIndex = 56
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(19, 14)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl1.TabIndex = 55
        Me.LabelControl1.Text = "Năm"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(19, 97)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl4.TabIndex = 67
        Me.LabelControl4.Text = "Đối tượng"
        '
        'cmbDoiTuong
        '
        Me.cmbDoiTuong.Location = New System.Drawing.Point(95, 93)
        Me.cmbDoiTuong.Name = "cmbDoiTuong"
        Me.cmbDoiTuong.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cmbDoiTuong.Properties.Appearance.Options.UseFont = True
        Me.cmbDoiTuong.Properties.Appearance.Options.UseTextOptions = True
        Me.cmbDoiTuong.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cmbDoiTuong.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmbDoiTuong.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.cmbDoiTuong.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbDoiTuong.Properties.DisplayMember = "ttcMa"
        Me.cmbDoiTuong.Properties.NullText = ""
        Me.cmbDoiTuong.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.cmbDoiTuong.Properties.PopupFormSize = New System.Drawing.Size(550, 300)
        Me.cmbDoiTuong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cmbDoiTuong.Properties.ValueMember = "ID"
        Me.cmbDoiTuong.Properties.View = Me.GridLookUpEdit1View
        Me.cmbDoiTuong.Size = New System.Drawing.Size(315, 20)
        Me.cmbDoiTuong.TabIndex = 66
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GridLookUpEdit1View.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridLookUpEdit1View.Appearance.HorzLine.BackColor = System.Drawing.Color.White
        Me.GridLookUpEdit1View.Appearance.HorzLine.Options.UseBackColor = True
        Me.GridLookUpEdit1View.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GridLookUpEdit1View.Appearance.VertLine.Options.UseBackColor = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridLookUpEdit1View.OptionsBehavior.Editable = False
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ColumnAutoWidth = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.GridLookUpEdit1View.OptionsView.ShowIndicator = False
        Me.GridLookUpEdit1View.RowHeight = 30
        Me.GridLookUpEdit1View.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn25, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn24.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridColumn24.Caption = "ID"
        Me.GridColumn24.FieldName = "ID"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn24.Width = 120
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Mã KH"
        Me.GridColumn25.FieldName = "ttcMa"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.FixedWidth = True
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 0
        Me.GridColumn25.Width = 150
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Tên KH"
        Me.GridColumn26.FieldName = "Ten"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.GridColumn26.Visible = True
        Me.GridColumn26.VisibleIndex = 1
        Me.GridColumn26.Width = 350
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "IDHinhThucTT"
        Me.GridColumn27.FieldName = "IDHinhThucTT"
        Me.GridColumn27.Name = "GridColumn27"
        '
        'txtTongHopCongNoPhaiThu
        '
        Me.txtTongHopCongNoPhaiThu.EditValue = resources.GetString("txtTongHopCongNoPhaiThu.EditValue")
        Me.txtTongHopCongNoPhaiThu.Location = New System.Drawing.Point(163, 12)
        Me.txtTongHopCongNoPhaiThu.Name = "txtTongHopCongNoPhaiThu"
        Me.txtTongHopCongNoPhaiThu.Size = New System.Drawing.Size(49, 20)
        Me.txtTongHopCongNoPhaiThu.TabIndex = 68
        Me.txtTongHopCongNoPhaiThu.Visible = False
        '
        'txtTongHopCongNoPhaiTra
        '
        Me.txtTongHopCongNoPhaiTra.EditValue = resources.GetString("txtTongHopCongNoPhaiTra.EditValue")
        Me.txtTongHopCongNoPhaiTra.Location = New System.Drawing.Point(218, 12)
        Me.txtTongHopCongNoPhaiTra.Name = "txtTongHopCongNoPhaiTra"
        Me.txtTongHopCongNoPhaiTra.Size = New System.Drawing.Size(49, 20)
        Me.txtTongHopCongNoPhaiTra.TabIndex = 69
        Me.txtTongHopCongNoPhaiTra.Visible = False
        '
        'txtChiTietCongNoPhaiThu
        '
        Me.txtChiTietCongNoPhaiThu.EditValue = resources.GetString("txtChiTietCongNoPhaiThu.EditValue")
        Me.txtChiTietCongNoPhaiThu.Location = New System.Drawing.Point(273, 12)
        Me.txtChiTietCongNoPhaiThu.Name = "txtChiTietCongNoPhaiThu"
        Me.txtChiTietCongNoPhaiThu.Size = New System.Drawing.Size(49, 20)
        Me.txtChiTietCongNoPhaiThu.TabIndex = 70
        Me.txtChiTietCongNoPhaiThu.Visible = False
        '
        'txtChiTietCongNoPhaiTra
        '
        Me.txtChiTietCongNoPhaiTra.EditValue = resources.GetString("txtChiTietCongNoPhaiTra.EditValue")
        Me.txtChiTietCongNoPhaiTra.Location = New System.Drawing.Point(328, 12)
        Me.txtChiTietCongNoPhaiTra.Name = "txtChiTietCongNoPhaiTra"
        Me.txtChiTietCongNoPhaiTra.Size = New System.Drawing.Size(49, 20)
        Me.txtChiTietCongNoPhaiTra.TabIndex = 71
        Me.txtChiTietCongNoPhaiTra.Visible = False
        '
        'frmBaoCaoThueChiTietVatTuHangHoa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 531)
        Me.Controls.Add(Me.txtChiTietCongNoPhaiTra)
        Me.Controls.Add(Me.txtChiTietCongNoPhaiThu)
        Me.Controls.Add(Me.txtTongHopCongNoPhaiTra)
        Me.Controls.Add(Me.txtTongHopCongNoPhaiThu)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.cmbDoiTuong)
        Me.Controls.Add(Me.chkGhiSo)
        Me.Controls.Add(Me.btInHoaDon)
        Me.Controls.Add(Me.btnDong)
        Me.Controls.Add(Me.txtDenNgay)
        Me.Controls.Add(Me.txtTuNgay)
        Me.Controls.Add(Me.lblDenNgay)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.cmbTieuChi)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtNam)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBaoCaoThueChiTietVatTuHangHoa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Báo cáo sổ chi tiết vật tư hàng hóa"
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTieuChi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbDoiTuong.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTongHopCongNoPhaiThu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTongHopCongNoPhaiTra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChiTietCongNoPhaiThu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChiTietCongNoPhaiTra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkGhiSo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btInHoaDon As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtDenNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtTuNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lblDenNgay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTieuChi As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNam As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbDoiTuong As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtTongHopCongNoPhaiThu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtTongHopCongNoPhaiTra As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtChiTietCongNoPhaiThu As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents txtChiTietCongNoPhaiTra As DevExpress.XtraEditors.MemoEdit
End Class
