<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTieuChiBaoCaoThue
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTieuChiBaoCaoThue))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNam = New DevExpress.XtraEditors.SpinEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.cmbTieuChi = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.lblDenNgay = New DevExpress.XtraEditors.LabelControl()
        Me.txtTuNgay = New DevExpress.XtraEditors.DateEdit()
        Me.txtDenNgay = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btInHoaDon = New DevExpress.XtraEditors.DropDownButton()
        Me.btnDong = New DevExpress.XtraEditors.SimpleButton()
        Me.chkGhiSo = New DevExpress.XtraEditors.CheckEdit()
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvData = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.txtNam.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTieuChi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(22, 22)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(26, 16)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Năm"
        '
        'txtNam
        '
        Me.txtNam.EditValue = New Decimal(New Integer() {2016, 0, 0, 0})
        Me.txtNam.Location = New System.Drawing.Point(108, 19)
        Me.txtNam.Name = "txtNam"
        Me.txtNam.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtNam.Properties.Appearance.ForeColor = System.Drawing.Color.Red
        Me.txtNam.Properties.Appearance.Options.UseFont = True
        Me.txtNam.Properties.Appearance.Options.UseForeColor = True
        Me.txtNam.Properties.Appearance.Options.UseTextOptions = True
        Me.txtNam.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.txtNam.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.txtNam.Properties.MaxValue = New Decimal(New Integer() {2020, 0, 0, 0})
        Me.txtNam.Properties.MinValue = New Decimal(New Integer() {2016, 0, 0, 0})
        Me.txtNam.Size = New System.Drawing.Size(87, 22)
        Me.txtNam.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(22, 57)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(53, 16)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Thời gian"
        '
        'cmbTieuChi
        '
        Me.cmbTieuChi.Location = New System.Drawing.Point(108, 51)
        Me.cmbTieuChi.Name = "cmbTieuChi"
        Me.cmbTieuChi.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Bold)
        Me.cmbTieuChi.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
        Me.cmbTieuChi.Properties.Appearance.Options.UseFont = True
        Me.cmbTieuChi.Properties.Appearance.Options.UseForeColor = True
        Me.cmbTieuChi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cmbTieuChi.Properties.DropDownItemHeight = 25
        Me.cmbTieuChi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cmbTieuChi.Size = New System.Drawing.Size(368, 22)
        Me.cmbTieuChi.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(22, 87)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(47, 16)
        Me.LabelControl3.TabIndex = 4
        Me.LabelControl3.Text = "Từ ngày"
        '
        'lblDenNgay
        '
        Me.lblDenNgay.Location = New System.Drawing.Point(269, 87)
        Me.lblDenNgay.Name = "lblDenNgay"
        Me.lblDenNgay.Size = New System.Drawing.Size(54, 16)
        Me.lblDenNgay.TabIndex = 5
        Me.lblDenNgay.Text = "Đến ngày"
        '
        'txtTuNgay
        '
        Me.txtTuNgay.EditValue = Nothing
        Me.txtTuNgay.Location = New System.Drawing.Point(108, 84)
        Me.txtTuNgay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.txtTuNgay.Size = New System.Drawing.Size(136, 22)
        Me.txtTuNgay.TabIndex = 6
        '
        'txtDenNgay
        '
        Me.txtDenNgay.EditValue = Nothing
        Me.txtDenNgay.Location = New System.Drawing.Point(340, 84)
        Me.txtDenNgay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.txtDenNgay.Size = New System.Drawing.Size(136, 22)
        Me.txtDenNgay.TabIndex = 7
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(22, 131)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(56, 16)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "Tài khoản"
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
        Me.btInHoaDon.Location = New System.Drawing.Point(339, 395)
        Me.btInHoaDon.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btInHoaDon.Name = "btInHoaDon"
        Me.btInHoaDon.ShowArrowButton = False
        Me.btInHoaDon.Size = New System.Drawing.Size(107, 44)
        Me.btInHoaDon.TabIndex = 52
        Me.btInHoaDon.Text = "Báo cáo"
        '
        'btnDong
        '
        Me.btnDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnDong.Appearance.Options.UseFont = True
        Me.btnDong.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDong.Image = CType(resources.GetObject("btnDong.Image"), System.Drawing.Image)
        Me.btnDong.Location = New System.Drawing.Point(452, 395)
        Me.btnDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDong.Name = "btnDong"
        Me.btnDong.Size = New System.Drawing.Size(101, 44)
        Me.btnDong.TabIndex = 53
        Me.btnDong.Text = "Đóng"
        Me.btnDong.ToolTipTitle = "Đóng"
        '
        'chkGhiSo
        '
        Me.chkGhiSo.EditValue = True
        Me.chkGhiSo.Location = New System.Drawing.Point(33, 404)
        Me.chkGhiSo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkGhiSo.Name = "chkGhiSo"
        Me.chkGhiSo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.chkGhiSo.Properties.Appearance.ForeColor = System.Drawing.Color.Green
        Me.chkGhiSo.Properties.Appearance.Options.UseFont = True
        Me.chkGhiSo.Properties.Appearance.Options.UseForeColor = True
        Me.chkGhiSo.Properties.Caption = "Lấy những bút toán đã ghi sổ"
        Me.chkGhiSo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.chkGhiSo.Properties.PictureChecked = CType(resources.GetObject("chkGhiSo.Properties.PictureChecked"), System.Drawing.Image)
        Me.chkGhiSo.Properties.PictureUnchecked = CType(resources.GetObject("chkGhiSo.Properties.PictureUnchecked"), System.Drawing.Image)
        Me.chkGhiSo.Size = New System.Drawing.Size(255, 26)
        Me.chkGhiSo.TabIndex = 54
        '
        'gdv
        '
        Me.gdv.Location = New System.Drawing.Point(108, 119)
        Me.gdv.MainView = Me.gdvData
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gdv.Size = New System.Drawing.Size(445, 257)
        Me.gdv.TabIndex = 55
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvData})
        '
        'gdvData
        '
        Me.gdvData.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn1, Me.GridColumn2, Me.GridColumn4})
        Me.gdvData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvData.GridControl = Me.gdv
        Me.gdvData.Name = "gdvData"
        Me.gdvData.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvData.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.gdvData.OptionsView.ShowColumnHeaders = False
        Me.gdvData.OptionsView.ShowGroupPanel = False
        Me.gdvData.OptionsView.ShowHorzLines = False
        Me.gdvData.OptionsView.ShowIndicator = False
        Me.gdvData.RowHeight = 25
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "GridColumn3"
        Me.GridColumn3.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn3.FieldName = "Chon"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.FixedWidth = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 50
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.PictureChecked = Global.BACSOFT.My.Resources.Resources.Checked
        Me.RepositoryItemCheckEdit1.PictureUnchecked = Global.BACSOFT.My.Resources.Resources.UnCheck
        Me.RepositoryItemCheckEdit1.ValueGrayed = "False"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GridColumn1.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceCell.Options.UseForeColor = True
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.FieldName = "TaiKhoan"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 80
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.GridColumn2.AppearanceCell.ForeColor = System.Drawing.Color.Navy
        Me.GridColumn2.AppearanceCell.Options.UseFont = True
        Me.GridColumn2.AppearanceCell.Options.UseForeColor = True
        Me.GridColumn2.Caption = "GridColumn2"
        Me.GridColumn2.FieldName = "TenGoi"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 488
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "GridColumn4"
        Me.GridColumn4.FieldName = "TaiKhoanCha"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'frmTieuChiBaoCaoThue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 453)
        Me.Controls.Add(Me.gdv)
        Me.Controls.Add(Me.chkGhiSo)
        Me.Controls.Add(Me.btInHoaDon)
        Me.Controls.Add(Me.btnDong)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.txtDenNgay)
        Me.Controls.Add(Me.txtTuNgay)
        Me.Controls.Add(Me.lblDenNgay)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.cmbTieuChi)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtNam)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTieuChiBaoCaoThue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTieuChiBaoCaoThue"
        CType(Me.txtNam.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTieuChi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTuNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTuNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDenNgay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGhiSo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtNam As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cmbTieuChi As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblDenNgay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtTuNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtDenNgay As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btInHoaDon As DevExpress.XtraEditors.DropDownButton
    Friend WithEvents btnDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkGhiSo As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvData As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
