<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGiaoXuLy
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSoYC = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.memoNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.btnHuy = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.btnGiao = New DevExpress.XtraEditors.SimpleButton()
        Me.gcbNhanVien = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lblLyDo = New DevExpress.XtraEditors.LabelControl()
        Me.txtLyDo = New DevExpress.XtraEditors.TextEdit()
        Me.lblNote = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtSoYC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memoNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gcbNhanVien.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLyDo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 14)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Số yêu cầu"
        '
        'txtSoYC
        '
        Me.txtSoYC.Enabled = False
        Me.txtSoYC.Location = New System.Drawing.Point(91, 7)
        Me.txtSoYC.Name = "txtSoYC"
        Me.txtSoYC.Size = New System.Drawing.Size(144, 20)
        Me.txtSoYC.TabIndex = 1
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(12, 35)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl5.TabIndex = 8
        Me.LabelControl5.Text = "Nội dung"
        '
        'memoNoiDung
        '
        Me.memoNoiDung.Location = New System.Drawing.Point(91, 33)
        Me.memoNoiDung.Name = "memoNoiDung"
        Me.memoNoiDung.Size = New System.Drawing.Size(542, 110)
        Me.memoNoiDung.TabIndex = 9
        '
        'btnHuy
        '
        Me.btnHuy.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHuy.Appearance.Options.UseFont = True
        Me.btnHuy.Image = Global.BACSOFT.My.Resources.Resources.block_24
        Me.btnHuy.Location = New System.Drawing.Point(567, 215)
        Me.btnHuy.Name = "btnHuy"
        Me.btnHuy.Size = New System.Drawing.Size(66, 36)
        Me.btnHuy.TabIndex = 12
        Me.btnHuy.Text = "Hủy"
        '
        'LabelControl7
        '
        Me.LabelControl7.Location = New System.Drawing.Point(12, 152)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl7.TabIndex = 13
        Me.LabelControl7.Text = "NV được giao"
        '
        'btnGiao
        '
        Me.btnGiao.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGiao.Appearance.Options.UseFont = True
        Me.btnGiao.Location = New System.Drawing.Point(448, 215)
        Me.btnGiao.Name = "btnGiao"
        Me.btnGiao.Size = New System.Drawing.Size(113, 36)
        Me.btnGiao.TabIndex = 15
        Me.btnGiao.Text = "Giao xử lý"
        '
        'gcbNhanVien
        '
        Me.gcbNhanVien.EditValue = ""
        Me.gcbNhanVien.Location = New System.Drawing.Point(91, 149)
        Me.gcbNhanVien.Name = "gcbNhanVien"
        Me.gcbNhanVien.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.gcbNhanVien.Properties.DisplayMember = "TEN"
        Me.gcbNhanVien.Properties.NullText = ""
        Me.gcbNhanVien.Properties.ValueMember = "ID"
        Me.gcbNhanVien.Properties.View = Me.GridLookUpEdit1View
        Me.gcbNhanVien.Size = New System.Drawing.Size(182, 20)
        Me.gcbNhanVien.TabIndex = 16
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'lblLyDo
        '
        Me.lblLyDo.Location = New System.Drawing.Point(12, 178)
        Me.lblLyDo.Name = "lblLyDo"
        Me.lblLyDo.Size = New System.Drawing.Size(26, 13)
        Me.lblLyDo.TabIndex = 18
        Me.lblLyDo.Text = "Lý do"
        '
        'txtLyDo
        '
        Me.txtLyDo.Location = New System.Drawing.Point(91, 175)
        Me.txtLyDo.Name = "txtLyDo"
        Me.txtLyDo.Size = New System.Drawing.Size(429, 20)
        Me.txtLyDo.TabIndex = 17
        '
        'lblNote
        '
        Me.lblNote.Location = New System.Drawing.Point(12, 258)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(27, 13)
        Me.lblNote.TabIndex = 19
        Me.lblNote.Text = "Note:"
        '
        'frmGiaoXuLy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 292)
        Me.Controls.Add(Me.lblNote)
        Me.Controls.Add(Me.lblLyDo)
        Me.Controls.Add(Me.txtLyDo)
        Me.Controls.Add(Me.gcbNhanVien)
        Me.Controls.Add(Me.btnGiao)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.btnHuy)
        Me.Controls.Add(Me.memoNoiDung)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.txtSoYC)
        Me.Controls.Add(Me.LabelControl1)
        Me.Name = "frmGiaoXuLy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Giao xử lý yêu cầu"
        CType(Me.txtSoYC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memoNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gcbNhanVien.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLyDo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSoYC As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents memoNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnHuy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnGiao As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcbNhanVien As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lblLyDo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtLyDo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblNote As DevExpress.XtraEditors.LabelControl
End Class
