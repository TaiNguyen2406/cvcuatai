<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDatHang_PhanHoi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDatHang_PhanHoi))
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.lbThongTin = New DevExpress.XtraEditors.LabelControl()
        Me.btXacNhan = New DevExpress.XtraEditors.SimpleButton()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.meSql = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.meSql.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(13, 38)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(464, 96)
        Me.tbNoiDung.TabIndex = 0
        '
        'lbThongTin
        '
        Me.lbThongTin.Location = New System.Drawing.Point(13, 13)
        Me.lbThongTin.Name = "lbThongTin"
        Me.lbThongTin.Size = New System.Drawing.Size(157, 13)
        Me.lbThongTin.TabIndex = 1
        Me.lbThongTin.Text = "Thông tin đề nghị duyệt chào giá"
        '
        'btXacNhan
        '
        Me.btXacNhan.Image = Global.BACSOFT.My.Resources.Resources.Checked
        Me.btXacNhan.Location = New System.Drawing.Point(313, 140)
        Me.btXacNhan.Name = "btXacNhan"
        Me.btXacNhan.Size = New System.Drawing.Size(83, 23)
        Me.btXacNhan.TabIndex = 2
        Me.btXacNhan.Text = "Xác nhận"
        '
        'btDong
        '
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(402, 140)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(75, 23)
        Me.btDong.TabIndex = 2
        Me.btDong.Text = "Đóng"
        '
        'meSql
        '
        Me.meSql.EditValue = resources.GetString("meSql.EditValue")
        Me.meSql.Enabled = False
        Me.meSql.Location = New System.Drawing.Point(28, 0)
        Me.meSql.Name = "meSql"
        Me.meSql.Size = New System.Drawing.Size(464, 96)
        Me.meSql.TabIndex = 3
        Me.meSql.Visible = False
        '
        'frmDatHang_PhanHoi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 175)
        Me.Controls.Add(Me.meSql)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btXacNhan)
        Me.Controls.Add(Me.lbThongTin)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Name = "frmDatHang_PhanHoi"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmDatHang_PhanHoi"
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.meSql.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents lbThongTin As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btXacNhan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents meSql As DevExpress.XtraEditors.MemoEdit
End Class
