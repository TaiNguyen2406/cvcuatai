<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDatTon_PhanHoi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDatTon_PhanHoi))
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        Me.lbThongTin = New DevExpress.XtraEditors.LabelControl()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.meSql = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.meSql.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btDong
        '
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(401, 141)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 23)
        Me.btDong.TabIndex = 5
        Me.btDong.Text = "Đóng"
        '
        'btXacNhan
        '
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Checked
        Me.btXacNhan.Location = New System.Drawing.Point(312, 141)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(83, 23)
        Me.btXacNhan.TabIndex = 6
        Me.btXacNhan.Text = "Xác nhận"
        '
        'lbThongTin
        '
        Me.lbThongTin.Location = New System.Drawing.Point(12, 14)
        Me.lbThongTin.Name = "lbThongTin"
        Me.lbThongTin.Size = New System.Drawing.Size(41, 13)
        Me.lbThongTin.TabIndex = 4
        Me.lbThongTin.Text = "Phản hồi"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(12, 33)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(464, 96)
        Me.tbNoiDung.TabIndex = 3
        '
        'meSql
        '
        Me.meSql.EditValue = resources.GetString("meSql.EditValue")
        Me.meSql.Enabled = False
        Me.meSql.Location = New System.Drawing.Point(33, 12)
        Me.meSql.Name = "meSql"
        Me.meSql.Size = New System.Drawing.Size(464, 96)
        Me.meSql.TabIndex = 7
        Me.meSql.Visible = False
        '
        'frmDatTon_PhanHoi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 173)
        Me.Controls.Add(Me.meSql)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.lbThongTin)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Name = "frmDatTon_PhanHoi"
        Me.Text = "Đặt tồn phản hồi"
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.meSql.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lbThongTin As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents meSql As DevExpress.XtraEditors.MemoEdit
End Class
