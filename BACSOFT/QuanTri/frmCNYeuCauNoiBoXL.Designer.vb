<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNYeuCauNoiBoXL
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
        Me.tbThoiGianLap = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuu = New DevExpress.XtraEditors.SimpleButton()
        Me.btThem = New DevExpress.XtraEditors.SimpleButton()
        Me.chkHoanThanh = New DevExpress.XtraEditors.CheckEdit()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.gdvFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colFile = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.tbThoiGianLap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbThoiGianLap.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkHoanThanh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(60, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Thời gian lập"
        '
        'tbThoiGianLap
        '
        Me.tbThoiGianLap.EditValue = Nothing
        Me.tbThoiGianLap.Enabled = False
        Me.tbThoiGianLap.Location = New System.Drawing.Point(79, 10)
        Me.tbThoiGianLap.Name = "tbThoiGianLap"
        Me.tbThoiGianLap.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tbThoiGianLap.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianLap.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianLap.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianLap.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.tbThoiGianLap.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.tbThoiGianLap.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.tbThoiGianLap.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.tbThoiGianLap.Size = New System.Drawing.Size(128, 20)
        Me.tbThoiGianLap.TabIndex = 0
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 39)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "Nội dung"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(79, 36)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(470, 149)
        Me.tbNoiDung.TabIndex = 0
        '
        'btDong
        '
        Me.btDong.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(474, 441)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 27)
        Me.btDong.TabIndex = 4
        Me.btDong.Text = "Đóng"
        '
        'btLuu
        '
        Me.btLuu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLuu.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuu.Appearance.Options.UseFont = True
        Me.btLuu.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuu.Location = New System.Drawing.Point(366, 441)
        Me.btLuu.Name = "btLuu"
        Me.btLuu.Size = New System.Drawing.Size(102, 27)
        Me.btLuu.TabIndex = 3
        Me.btLuu.Text = "Lưu lại"
        '
        'btThem
        '
        Me.btThem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btThem.Appearance.Options.UseFont = True
        Me.btThem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btThem.Location = New System.Drawing.Point(253, 441)
        Me.btThem.Name = "btThem"
        Me.btThem.Size = New System.Drawing.Size(107, 27)
        Me.btThem.TabIndex = 2
        Me.btThem.Text = "Thêm mới"
        '
        'chkHoanThanh
        '
        Me.chkHoanThanh.Location = New System.Drawing.Point(77, 410)
        Me.chkHoanThanh.Name = "chkHoanThanh"
        Me.chkHoanThanh.Properties.Caption = "Xác nhận đã xử lý xong"
        Me.chkHoanThanh.Size = New System.Drawing.Size(157, 19)
        Me.chkHoanThanh.TabIndex = 1
        '
        'btXoaFile
        '
        Me.btXoaFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(474, 271)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(75, 23)
        Me.btXoaFile.TabIndex = 22
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(474, 242)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(75, 23)
        Me.btThemFile.TabIndex = 21
        Me.btThemFile.Text = "Thêm file"
        '
        'gdvFile
        '
        Me.gdvFile.Location = New System.Drawing.Point(79, 191)
        Me.gdvFile.MainView = Me.gdvFileCT
        Me.gdvFile.Name = "gdvFile"
        Me.gdvFile.Size = New System.Drawing.Size(389, 213)
        Me.gdvFile.TabIndex = 20
        Me.gdvFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvFileCT})
        '
        'gdvFileCT
        '
        Me.gdvFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colFile})
        Me.gdvFileCT.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.gdvFileCT.GridControl = Me.gdvFile
        Me.gdvFileCT.Name = "gdvFileCT"
        Me.gdvFileCT.OptionsBehavior.Editable = False
        Me.gdvFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvFileCT.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.gdvFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvFileCT.RowHeight = 22
        '
        'colFile
        '
        Me.colFile.Caption = "File đính kèm"
        Me.colFile.FieldName = "File"
        Me.colFile.Name = "colFile"
        Me.colFile.Visible = True
        Me.colFile.VisibleIndex = 0
        Me.colFile.Width = 500
        '
        'frmCNYeuCauNoiBoXL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(566, 480)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.gdvFile)
        Me.Controls.Add(Me.chkHoanThanh)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuu)
        Me.Controls.Add(Me.btThem)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.tbThoiGianLap)
        Me.Controls.Add(Me.LabelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCNYeuCauNoiBoXL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Xử lý YC nội bộ"
        CType(Me.tbThoiGianLap.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbThoiGianLap.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkHoanThanh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbThoiGianLap As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuu As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkHoanThanh As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gdvFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colFile As DevExpress.XtraGrid.Columns.GridColumn
End Class
