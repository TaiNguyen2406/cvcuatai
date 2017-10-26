<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Me.gdv = New DevExpress.XtraGrid.GridControl()
        Me.gdvCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rcbCotExcel = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        Me.chkBaoGomCode = New DevExpress.XtraEditors.CheckEdit()
        Me.rdModelVT = New DevExpress.XtraEditors.CheckEdit()
        Me.rdCodeVT = New DevExpress.XtraEditors.CheckEdit()
        Me.rdModelE = New DevExpress.XtraEditors.CheckEdit()
        Me.rdCodeE = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rcbCotExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkBaoGomCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdModelVT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdCodeVT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdModelE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdCodeE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gdv
        '
        Me.gdv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gdv.Location = New System.Drawing.Point(0, 0)
        Me.gdv.MainView = Me.gdvCT
        Me.gdv.Name = "gdv"
        Me.gdv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rcbCotExcel})
        Me.gdv.Size = New System.Drawing.Size(391, 422)
        Me.gdv.TabIndex = 1
        Me.gdv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvCT})
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
        Me.gdvCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
        Me.gdvCT.GridControl = Me.gdv
        Me.gdvCT.Name = "gdvCT"
        Me.gdvCT.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvCT.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gdvCT.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused
        Me.gdvCT.OptionsBehavior.SummariesIgnoreNullValues = True
        Me.gdvCT.OptionsView.EnableAppearanceEvenRow = True
        Me.gdvCT.OptionsView.RowAutoHeight = True
        Me.gdvCT.OptionsView.ShowFooter = True
        Me.gdvCT.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Bảng VT"
        Me.GridColumn2.FieldName = "col1"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.FixedWidth = True
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.SummaryItem.DisplayFormat = "{0}"
        Me.GridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 134
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cột cần nhập"
        Me.GridColumn3.ColumnEdit = Me.rcbCotExcel
        Me.GridColumn3.FieldName = "col2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.FixedWidth = True
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 152
        '
        'rcbCotExcel
        '
        Me.rcbCotExcel.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.rcbCotExcel.AutoHeight = False
        Me.rcbCotExcel.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)})
        Me.rcbCotExcel.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "Name1")})
        Me.rcbCotExcel.DisplayMember = "ID"
        Me.rcbCotExcel.DropDownItemHeight = 22
        Me.rcbCotExcel.Name = "rcbCotExcel"
        Me.rcbCotExcel.NullText = ""
        Me.rcbCotExcel.ShowHeader = False
        Me.rcbCotExcel.ValueMember = "ID"
        '
        'btXacNhan
        '
        Me.btXacNhan.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btXacNhan.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btXacNhan.Appearance.Options.UseFont = True
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Accept_18
        Me.btXacNhan.Location = New System.Drawing.Point(285, 460)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(94, 23)
        Me.btXacNhan.TabIndex = 2
        Me.btXacNhan.Text = "Xác nhận"
        '
        'chkBaoGomCode
        '
        Me.chkBaoGomCode.Location = New System.Drawing.Point(13, 428)
        Me.chkBaoGomCode.Name = "chkBaoGomCode"
        Me.chkBaoGomCode.Properties.Caption = "Hàng hóa có mã code"
        Me.chkBaoGomCode.Size = New System.Drawing.Size(140, 19)
        Me.chkBaoGomCode.TabIndex = 3
        '
        'rdModelVT
        '
        Me.rdModelVT.Enabled = False
        Me.rdModelVT.Location = New System.Drawing.Point(12, 453)
        Me.rdModelVT.Name = "rdModelVT"
        Me.rdModelVT.Properties.Caption = "Model hàng hóa"
        Me.rdModelVT.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdModelVT.Properties.RadioGroupIndex = 0
        Me.rdModelVT.Size = New System.Drawing.Size(100, 19)
        Me.rdModelVT.TabIndex = 3
        Me.rdModelVT.TabStop = False
        Me.rdModelVT.Tag = "Model"
        '
        'rdCodeVT
        '
        Me.rdCodeVT.EditValue = True
        Me.rdCodeVT.Enabled = False
        Me.rdCodeVT.Location = New System.Drawing.Point(12, 472)
        Me.rdCodeVT.Name = "rdCodeVT"
        Me.rdCodeVT.Properties.Caption = "Code hàng hóa"
        Me.rdCodeVT.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdCodeVT.Properties.RadioGroupIndex = 0
        Me.rdCodeVT.Size = New System.Drawing.Size(100, 19)
        Me.rdCodeVT.TabIndex = 3
        Me.rdCodeVT.Tag = "Code"
        '
        'rdModelE
        '
        Me.rdModelE.Enabled = False
        Me.rdModelE.Location = New System.Drawing.Point(145, 453)
        Me.rdModelE.Name = "rdModelE"
        Me.rdModelE.Properties.Caption = "Model Excel"
        Me.rdModelE.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdModelE.Properties.RadioGroupIndex = 1
        Me.rdModelE.Size = New System.Drawing.Size(86, 19)
        Me.rdModelE.TabIndex = 3
        Me.rdModelE.TabStop = False
        Me.rdModelE.Tag = "Model"
        '
        'rdCodeE
        '
        Me.rdCodeE.EditValue = True
        Me.rdCodeE.Enabled = False
        Me.rdCodeE.Location = New System.Drawing.Point(145, 472)
        Me.rdCodeE.Name = "rdCodeE"
        Me.rdCodeE.Properties.Caption = "Code Excel"
        Me.rdCodeE.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.rdCodeE.Properties.RadioGroupIndex = 1
        Me.rdCodeE.Size = New System.Drawing.Size(86, 19)
        Me.rdCodeE.TabIndex = 3
        Me.rdCodeE.Tag = "Code"
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 495)
        Me.Controls.Add(Me.rdCodeE)
        Me.Controls.Add(Me.rdCodeVT)
        Me.Controls.Add(Me.rdModelE)
        Me.Controls.Add(Me.rdModelVT)
        Me.Controls.Add(Me.chkBaoGomCode)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.gdv)
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lựa chọn các tiêu chí để cập nhật"
        CType(Me.gdv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rcbCotExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkBaoGomCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdModelVT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdCodeVT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdModelE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdCodeE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gdv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rcbCotExcel As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkBaoGomCode As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdModelVT As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdCodeVT As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdModelE As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents rdCodeE As DevExpress.XtraEditors.CheckEdit
End Class
