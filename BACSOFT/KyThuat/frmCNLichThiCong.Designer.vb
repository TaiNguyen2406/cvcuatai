<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNLichThiCong
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
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbTuNgay = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.cbSoYeuCau = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.rMemoText = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.gdvSoYC = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cbNoiDungThiCong = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.gdvNoiDungThiCong = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuVaThem = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.tbBatDauTu = New DevExpress.XtraEditors.TimeEdit()
        Me.tbKetThucTu = New DevExpress.XtraEditors.TimeEdit()
        Me.tbBatDauDen = New DevExpress.XtraEditors.TimeEdit()
        Me.tbKetThucDen = New DevExpress.XtraEditors.TimeEdit()
        Me.tbDenNgay = New DevExpress.XtraEditors.DateEdit()
        Me.chkSang = New DevExpress.XtraEditors.CheckEdit()
        Me.chkChieu = New DevExpress.XtraEditors.CheckEdit()
        Me.treeNV = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.chkDenNgay = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.tbSoCG = New DevExpress.XtraEditors.TextEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.tbTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbTuNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbSoYeuCau.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvSoYC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbNoiDungThiCong.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvNoiDungThiCong, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBatDauTu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbKetThucTu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbBatDauDen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbKetThucDen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkSang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkChieu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbSoCG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 67)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Số yêu cầu"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 15)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Từ ngày"
        '
        'tbTuNgay
        '
        Me.tbTuNgay.EditValue = Nothing
        Me.tbTuNgay.Location = New System.Drawing.Point(91, 12)
        Me.tbTuNgay.Name = "tbTuNgay"
        Me.tbTuNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.tbTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbTuNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbTuNgay.Size = New System.Drawing.Size(100, 20)
        Me.tbTuNgay.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(251, 15)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Bắt đầu"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(356, 15)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl4.TabIndex = 2
        Me.LabelControl4.Text = "Kết thúc"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(12, 93)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl5.TabIndex = 0
        Me.LabelControl5.Text = "Nội dung"
        '
        'cbSoYeuCau
        '
        Me.cbSoYeuCau.Location = New System.Drawing.Point(91, 64)
        Me.cbSoYeuCau.Name = "cbSoYeuCau"
        Me.cbSoYeuCau.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cbSoYeuCau.Properties.Appearance.Options.UseFont = True
        Me.cbSoYeuCau.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbSoYeuCau.Properties.DisplayMember = "Sophieu"
        Me.cbSoYeuCau.Properties.HideSelection = False
        Me.cbSoYeuCau.Properties.ImmediatePopup = True
        Me.cbSoYeuCau.Properties.NullText = "[Chọn yêu cầu]"
        Me.cbSoYeuCau.Properties.NullValuePromptShowForEmptyValue = True
        Me.cbSoYeuCau.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.cbSoYeuCau.Properties.PopupFormSize = New System.Drawing.Size(800, 300)
        Me.cbSoYeuCau.Properties.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rMemoText})
        Me.cbSoYeuCau.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbSoYeuCau.Properties.ValidateOnEnterKey = True
        Me.cbSoYeuCau.Properties.ValueMember = "Sophieu"
        Me.cbSoYeuCau.Properties.View = Me.gdvSoYC
        Me.cbSoYeuCau.Size = New System.Drawing.Size(100, 20)
        Me.cbSoYeuCau.TabIndex = 7
        '
        'rMemoText
        '
        Me.rMemoText.Name = "rMemoText"
        '
        'gdvSoYC
        '
        Me.gdvSoYC.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gdvSoYC.Appearance.HeaderPanel.Options.UseFont = True
        Me.gdvSoYC.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.gdvSoYC.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.gdvSoYC.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.gdvSoYC.Name = "gdvSoYC"
        Me.gdvSoYC.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvSoYC.OptionsBehavior.ReadOnly = True
        Me.gdvSoYC.OptionsFind.AllowFindPanel = False
        Me.gdvSoYC.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvSoYC.OptionsView.RowAutoHeight = True
        Me.gdvSoYC.OptionsView.ShowAutoFilterRow = True
        Me.gdvSoYC.OptionsView.ShowGroupPanel = False
        '
        'cbNoiDungThiCong
        '
        Me.cbNoiDungThiCong.Location = New System.Drawing.Point(91, 90)
        Me.cbNoiDungThiCong.Name = "cbNoiDungThiCong"
        Me.cbNoiDungThiCong.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cbNoiDungThiCong.Properties.DisplayMember = "NoiDung"
        Me.cbNoiDungThiCong.Properties.NullText = "[Chọn nội dung thi công]"
        Me.cbNoiDungThiCong.Properties.PopupFormSize = New System.Drawing.Size(500, 300)
        Me.cbNoiDungThiCong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.cbNoiDungThiCong.Properties.ValueMember = "ID"
        Me.cbNoiDungThiCong.Properties.View = Me.gdvNoiDungThiCong
        Me.cbNoiDungThiCong.Size = New System.Drawing.Size(367, 20)
        Me.cbNoiDungThiCong.TabIndex = 8
        '
        'gdvNoiDungThiCong
        '
        Me.gdvNoiDungThiCong.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.gdvNoiDungThiCong.Name = "gdvNoiDungThiCong"
        Me.gdvNoiDungThiCong.OptionsBehavior.AutoExpandAllGroups = True
        Me.gdvNoiDungThiCong.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvNoiDungThiCong.OptionsBehavior.ReadOnly = True
        Me.gdvNoiDungThiCong.OptionsFind.AllowFindPanel = False
        Me.gdvNoiDungThiCong.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvNoiDungThiCong.OptionsView.ShowAutoFilterRow = True
        Me.gdvNoiDungThiCong.OptionsView.ShowGroupPanel = False
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(382, 422)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 27)
        Me.btDong.TabIndex = 12
        Me.btDong.Text = "Đóng"
        '
        'btLuu
        '
        Me.btLuu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuu.Appearance.Options.UseFont = True
        Me.btLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuu.Location = New System.Drawing.Point(250, 422)
        Me.btLuu.Name = "btLuu"
        Me.btLuu.Size = New System.Drawing.Size(125, 27)
        Me.btLuu.TabIndex = 10
        Me.btLuu.Text = "Lưu lại và đóng"
        '
        'btLuuVaThem
        '
        Me.btLuuVaThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuuVaThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuVaThem.Appearance.Options.UseFont = True
        Me.btLuuVaThem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btLuuVaThem.Location = New System.Drawing.Point(60, 422)
        Me.btLuuVaThem.Name = "btLuuVaThem"
        Me.btLuuVaThem.Size = New System.Drawing.Size(96, 27)
        Me.btLuuVaThem.TabIndex = 11
        Me.btLuuVaThem.Text = "Thêm mới"
        '
        'LabelControl8
        '
        Me.LabelControl8.Location = New System.Drawing.Point(251, 41)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl8.TabIndex = 2
        Me.LabelControl8.Text = "Bắt đầu"
        '
        'LabelControl9
        '
        Me.LabelControl9.Location = New System.Drawing.Point(356, 41)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl9.TabIndex = 2
        Me.LabelControl9.Text = "Kết thúc"
        '
        'tbBatDauTu
        '
        Me.tbBatDauTu.EditValue = New Date(2014, 2, 7, 0, 0, 0, 0)
        Me.tbBatDauTu.Location = New System.Drawing.Point(294, 12)
        Me.tbBatDauTu.Name = "tbBatDauTu"
        Me.tbBatDauTu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbBatDauTu.Properties.DisplayFormat.FormatString = "HH:mm"
        Me.tbBatDauTu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbBatDauTu.Properties.Mask.EditMask = "HH:mm"
        Me.tbBatDauTu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbBatDauTu.Size = New System.Drawing.Size(56, 20)
        Me.tbBatDauTu.TabIndex = 1
        '
        'tbKetThucTu
        '
        Me.tbKetThucTu.EditValue = New Date(2014, 2, 7, 0, 0, 0, 0)
        Me.tbKetThucTu.Location = New System.Drawing.Point(402, 12)
        Me.tbKetThucTu.Name = "tbKetThucTu"
        Me.tbKetThucTu.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbKetThucTu.Properties.DisplayFormat.FormatString = "HH:mm"
        Me.tbKetThucTu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbKetThucTu.Properties.Mask.EditMask = "HH:mm"
        Me.tbKetThucTu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbKetThucTu.Size = New System.Drawing.Size(56, 20)
        Me.tbKetThucTu.TabIndex = 2
        '
        'tbBatDauDen
        '
        Me.tbBatDauDen.EditValue = New Date(2014, 2, 7, 0, 0, 0, 0)
        Me.tbBatDauDen.Enabled = False
        Me.tbBatDauDen.Location = New System.Drawing.Point(294, 38)
        Me.tbBatDauDen.Name = "tbBatDauDen"
        Me.tbBatDauDen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbBatDauDen.Properties.DisplayFormat.FormatString = "HH:mm"
        Me.tbBatDauDen.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbBatDauDen.Properties.Mask.EditMask = "HH:mm"
        Me.tbBatDauDen.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbBatDauDen.Size = New System.Drawing.Size(56, 20)
        Me.tbBatDauDen.TabIndex = 5
        '
        'tbKetThucDen
        '
        Me.tbKetThucDen.EditValue = New Date(2014, 2, 7, 0, 0, 0, 0)
        Me.tbKetThucDen.Enabled = False
        Me.tbKetThucDen.Location = New System.Drawing.Point(402, 38)
        Me.tbKetThucDen.Name = "tbKetThucDen"
        Me.tbKetThucDen.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbKetThucDen.Properties.DisplayFormat.FormatString = "HH:mm"
        Me.tbKetThucDen.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbKetThucDen.Properties.Mask.EditMask = "HH:mm"
        Me.tbKetThucDen.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbKetThucDen.Size = New System.Drawing.Size(56, 20)
        Me.tbKetThucDen.TabIndex = 6
        '
        'tbDenNgay
        '
        Me.tbDenNgay.EditValue = Nothing
        Me.tbDenNgay.Enabled = False
        Me.tbDenNgay.Location = New System.Drawing.Point(91, 38)
        Me.tbDenNgay.Name = "tbDenNgay"
        Me.tbDenNgay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.tbDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.tbDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.tbDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbDenNgay.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbDenNgay.Size = New System.Drawing.Size(100, 20)
        Me.tbDenNgay.TabIndex = 4
        '
        'chkSang
        '
        Me.chkSang.EditValue = True
        Me.chkSang.Location = New System.Drawing.Point(200, 12)
        Me.chkSang.Name = "chkSang"
        Me.chkSang.Properties.Caption = "Ca 1"
        Me.chkSang.Size = New System.Drawing.Size(45, 19)
        Me.chkSang.TabIndex = 12
        '
        'chkChieu
        '
        Me.chkChieu.Location = New System.Drawing.Point(200, 38)
        Me.chkChieu.Name = "chkChieu"
        Me.chkChieu.Properties.Caption = "Ca 2"
        Me.chkChieu.Size = New System.Drawing.Size(45, 19)
        Me.chkChieu.TabIndex = 12
        '
        'treeNV
        '
        Me.treeNV.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn2})
        Me.treeNV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeNV.Location = New System.Drawing.Point(91, 116)
        Me.treeNV.Name = "treeNV"
        Me.treeNV.OptionsBehavior.Editable = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.treeNV.OptionsView.ShowCheckBoxes = True
        Me.treeNV.OptionsView.ShowHorzLines = False
        Me.treeNV.OptionsView.ShowIndicator = False
        Me.treeNV.OptionsView.ShowVertLines = False
        Me.treeNV.ParentFieldName = "PhongBan"
        Me.treeNV.Size = New System.Drawing.Size(367, 300)
        Me.treeNV.TabIndex = 9
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Nhân viên thực hiện"
        Me.TreeListColumn2.FieldName = "Nhân viên"
        Me.TreeListColumn2.MinWidth = 32
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 0
        Me.TreeListColumn2.Width = 372
        '
        'chkDenNgay
        '
        Me.chkDenNgay.Location = New System.Drawing.Point(10, 38)
        Me.chkDenNgay.Name = "chkDenNgay"
        Me.chkDenNgay.Properties.Caption = "Đến ngày"
        Me.chkDenNgay.Size = New System.Drawing.Size(75, 19)
        Me.chkDenNgay.TabIndex = 3
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(202, 67)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(55, 13)
        Me.LabelControl7.TabIndex = 0
        Me.LabelControl7.Text = "Số chào giá"
        '
        'tbSoCG
        '
        Me.tbSoCG.Location = New System.Drawing.Point(294, 64)
        Me.tbSoCG.Name = "tbSoCG"
        Me.tbSoCG.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tbSoCG.Properties.Appearance.Options.UseFont = True
        Me.tbSoCG.Properties.Appearance.Options.UseTextOptions = True
        Me.tbSoCG.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tbSoCG.Size = New System.Drawing.Size(100, 20)
        Me.tbSoCG.TabIndex = 13
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.SimpleButton1.Location = New System.Drawing.Point(162, 422)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(82, 27)
        Me.SimpleButton1.TabIndex = 14
        Me.SimpleButton1.Text = "Lưu lại"
        '
        'frmCNLichThiCong
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 461)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.tbSoCG)
        Me.Controls.Add(Me.chkDenNgay)
        Me.Controls.Add(Me.treeNV)
        Me.Controls.Add(Me.chkChieu)
        Me.Controls.Add(Me.chkSang)
        Me.Controls.Add(Me.tbKetThucDen)
        Me.Controls.Add(Me.tbBatDauDen)
        Me.Controls.Add(Me.tbKetThucTu)
        Me.Controls.Add(Me.tbBatDauTu)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.btLuuVaThem)
        Me.Controls.Add(Me.cbNoiDungThiCong)
        Me.Controls.Add(Me.cbSoYeuCau)
        Me.Controls.Add(Me.LabelControl9)
        Me.Controls.Add(Me.tbDenNgay)
        Me.Controls.Add(Me.tbTuNgay)
        Me.Controls.Add(Me.LabelControl8)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmCNLichThiCong"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật lịch thi công"
        CType(Me.tbTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbTuNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbSoYeuCau.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rMemoText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvSoYC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbNoiDungThiCong.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvNoiDungThiCong, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBatDauTu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbKetThucTu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbBatDauDen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbKetThucDen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkSang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkChieu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbSoCG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbTuNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cbSoYeuCau As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents gdvSoYC As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cbNoiDungThiCong As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents gdvNoiDungThiCong As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuVaThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbBatDauTu As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents tbKetThucTu As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents tbBatDauDen As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents tbKetThucDen As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents tbDenNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents chkSang As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkChieu As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents treeNV As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents chkDenNgay As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbSoCG As DevExpress.XtraEditors.TextEdit
    Friend WithEvents rMemoText As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
End Class
