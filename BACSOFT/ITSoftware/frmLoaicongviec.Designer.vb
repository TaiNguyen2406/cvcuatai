<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoaicongviec
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
        Me.txtTen = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btDong = New DevExpress.XtraEditors.SimpleButton()
        Me.btLuuLai = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grdDSCV = New DevExpress.XtraGrid.GridControl()
        Me.grdViewDSCV = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.chkActive = New DevExpress.XtraEditors.CheckEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtGhichu = New DevExpress.XtraEditors.TextEdit()
        Me.btnThem = New DevExpress.XtraEditors.SimpleButton()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.txtTen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grdDSCV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdViewDSCV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTen
        '
        Me.txtTen.Location = New System.Drawing.Point(117, 11)
        Me.txtTen.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTen.Name = "txtTen"
        Me.txtTen.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtTen.Properties.Appearance.Options.UseBackColor = True
        Me.txtTen.Size = New System.Drawing.Size(293, 22)
        Me.txtTen.TabIndex = 18
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(11, 14)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(87, 17)
        Me.LabelControl1.TabIndex = 19
        Me.LabelControl1.Text = "Loại công việc"
        '
        'btDong
        '
        Me.btDong.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btDong.Appearance.Options.UseFont = True
        Me.btDong.Image = Global.BACSOFT.My.Resources.Resources.close_18
        Me.btDong.Location = New System.Drawing.Point(708, 388)
        Me.btDong.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btDong.Name = "btDong"
        Me.btDong.Size = New System.Drawing.Size(104, 41)
        Me.btDong.TabIndex = 38
        Me.btDong.Text = "Đóng"
        '
        'btLuuLai
        '
        Me.btLuuLai.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btLuuLai.Appearance.Options.UseFont = True
        Me.btLuuLai.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btLuuLai.Location = New System.Drawing.Point(570, 388)
        Me.btLuuLai.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btLuuLai.Name = "btLuuLai"
        Me.btLuuLai.Size = New System.Drawing.Size(122, 41)
        Me.btLuuLai.TabIndex = 37
        Me.btLuuLai.Text = "Lưu lại"
        '
        'GroupControl1
        '
        Me.GroupControl1.AppearanceCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl1.AppearanceCaption.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.grdDSCV)
        Me.GroupControl1.Location = New System.Drawing.Point(12, 115)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(802, 257)
        Me.GroupControl1.TabIndex = 39
        Me.GroupControl1.Text = "Danh mục loại công việc"
        '
        'grdDSCV
        '
        Me.grdDSCV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDSCV.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDSCV.Location = New System.Drawing.Point(2, 25)
        Me.grdDSCV.MainView = Me.grdViewDSCV
        Me.grdDSCV.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdDSCV.Name = "grdDSCV"
        Me.grdDSCV.Size = New System.Drawing.Size(798, 230)
        Me.grdDSCV.TabIndex = 5
        Me.grdDSCV.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdViewDSCV})
        '
        'grdViewDSCV
        '
        Me.grdViewDSCV.Appearance.FocusedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSCV.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grdViewDSCV.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSCV.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grdViewDSCV.Appearance.SelectedRow.BackColor = System.Drawing.Color.PowderBlue
        Me.grdViewDSCV.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grdViewDSCV.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn1, Me.GridColumn2, Me.GridColumn4})
        Me.grdViewDSCV.GridControl = Me.grdDSCV
        Me.grdViewDSCV.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.grdViewDSCV.Name = "grdViewDSCV"
        Me.grdViewDSCV.OptionsView.ShowGroupPanel = False
        '
        'chkActive
        '
        Me.chkActive.EditValue = True
        Me.chkActive.Location = New System.Drawing.Point(115, 80)
        Me.chkActive.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Properties.Caption = "Kích hoạt"
        Me.chkActive.Size = New System.Drawing.Size(148, 22)
        Me.chkActive.TabIndex = 52
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn1.AppearanceCell.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn1.AppearanceHeader.Options.UseFont = True
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn1.Caption = "Loại công việc"
        Me.GridColumn1.FieldName = "Tencongviec"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        Me.GridColumn1.Width = 238
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn2.AppearanceCell.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn2.AppearanceHeader.Options.UseFont = True
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn2.Caption = "Ghi chú"
        Me.GridColumn2.FieldName = "Ghichu"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        Me.GridColumn2.Width = 242
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.GridColumn3.AppearanceCell.Options.UseFont = True
        Me.GridColumn3.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridColumn3.AppearanceHeader.Options.UseFont = True
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn3.Caption = "Kích hoạt"
        Me.GridColumn3.FieldName = "Active"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        Me.GridColumn3.Width = 80
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Location = New System.Drawing.Point(12, 47)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 17)
        Me.LabelControl2.TabIndex = 53
        Me.LabelControl2.Text = "Ghi chú"
        '
        'txtGhichu
        '
        Me.txtGhichu.Location = New System.Drawing.Point(117, 45)
        Me.txtGhichu.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtGhichu.Name = "txtGhichu"
        Me.txtGhichu.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.txtGhichu.Properties.Appearance.Options.UseBackColor = True
        Me.txtGhichu.Size = New System.Drawing.Size(518, 22)
        Me.txtGhichu.TabIndex = 54
        '
        'btnThem
        '
        Me.btnThem.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnThem.Appearance.Options.UseFont = True
        Me.btnThem.Image = Global.BACSOFT.My.Resources.Resources.AddNew_18
        Me.btnThem.Location = New System.Drawing.Point(14, 388)
        Me.btnThem.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnThem.Name = "btnThem"
        Me.btnThem.Size = New System.Drawing.Size(122, 41)
        Me.btnThem.TabIndex = 55
        Me.btnThem.Text = "Thêm mới"
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Recordid"
        Me.GridColumn4.FieldName = "Id"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'frmLoaicongviec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 442)
        Me.Controls.Add(Me.btnThem)
        Me.Controls.Add(Me.txtGhichu)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.btDong)
        Me.Controls.Add(Me.btLuuLai)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.txtTen)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoaicongviec"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Loại công việc"
        CType(Me.txtTen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grdDSCV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdViewDSCV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGhichu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtTen As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btDong As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btLuuLai As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grdDSCV As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdViewDSCV As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtGhichu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnThem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
