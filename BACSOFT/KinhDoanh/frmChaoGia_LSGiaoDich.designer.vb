<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChaoGia_LSGiaoDich
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
        Me.tbThoiGian = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.btClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btGhi = New DevExpress.XtraEditors.SimpleButton()
        Me.btThem = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.treeNV = New DevExpress.XtraTreeList.TreeList()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        CType(Me.tbThoiGian.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGian.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 16)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Thời gian"
        '
        'tbThoiGian
        '
        Me.tbThoiGian.EditValue = Nothing
        Me.tbThoiGian.Location = New System.Drawing.Point(89, 13)
        Me.tbThoiGian.Name = "tbThoiGian"
        Me.tbThoiGian.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbThoiGian.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGian.Size = New System.Drawing.Size(114, 20)
        Me.tbThoiGian.TabIndex = 0
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(13, 78)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl2.TabIndex = 0
        Me.LabelControl2.Text = "Nội dung"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(89, 39)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(382, 96)
        Me.tbNoiDung.TabIndex = 1
        '
        'btClose
        '
        Me.btClose.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btClose.Location = New System.Drawing.Point(396, 393)
        Me.btClose.Name = "btClose"
        Me.btClose.Size = New System.Drawing.Size(75, 23)
        Me.btClose.TabIndex = 4
        Me.btClose.Text = "Đóng"
        '
        'btGhi
        '
        Me.btGhi.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btGhi.Location = New System.Drawing.Point(315, 393)
        Me.btGhi.Name = "btGhi"
        Me.btGhi.Size = New System.Drawing.Size(75, 23)
        Me.btGhi.TabIndex = 2
        Me.btGhi.Text = "Ghi lại"
        '
        'btThem
        '
        Me.btThem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Location = New System.Drawing.Point(234, 393)
        Me.btThem.Name = "btThem"
        Me.btThem.Size = New System.Drawing.Size(75, 23)
        Me.btThem.TabIndex = 3
        Me.btThem.Text = "Thêm"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(12, 145)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(71, 13)
        Me.LabelControl3.TabIndex = 0
        Me.LabelControl3.Text = "Thông báo cho"
        '
        'treeNV
        '
        Me.treeNV.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.TreeListColumn2})
        Me.treeNV.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeNV.Location = New System.Drawing.Point(89, 141)
        Me.treeNV.Name = "treeNV"
        Me.treeNV.OptionsBehavior.Editable = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.treeNV.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.treeNV.OptionsView.ShowCheckBoxes = True
        Me.treeNV.OptionsView.ShowHorzLines = False
        Me.treeNV.OptionsView.ShowIndicator = False
        Me.treeNV.OptionsView.ShowVertLines = False
        Me.treeNV.ParentFieldName = "PhongBan"
        Me.treeNV.Size = New System.Drawing.Size(382, 246)
        Me.treeNV.TabIndex = 10
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Nhân viên "
        Me.TreeListColumn2.FieldName = "Nhân viên"
        Me.TreeListColumn2.MinWidth = 32
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 0
        Me.TreeListColumn2.Width = 372
        '
        'frmChaoGia_LSGiaoDich
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(477, 428)
        Me.Controls.Add(Me.treeNV)
        Me.Controls.Add(Me.btThem)
        Me.Controls.Add(Me.btGhi)
        Me.Controls.Add(Me.btClose)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Controls.Add(Me.tbThoiGian)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmChaoGia_LSGiaoDich"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật quá trình giao dịch"
        CType(Me.tbThoiGian.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGian.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.treeNV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGian As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btGhi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents treeNV As DevExpress.XtraTreeList.TreeList
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
End Class
