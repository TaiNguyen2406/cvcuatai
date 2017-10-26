<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCNQuaTrinhXuLy
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
        Me.btGhi = New DevExpress.XtraEditors.SimpleButton()
        Me.tbNoiDung = New DevExpress.XtraEditors.MemoEdit()
        Me.btXoaFile = New DevExpress.XtraEditors.SimpleButton()
        Me.btThemFile = New DevExpress.XtraEditors.SimpleButton()
        Me.gdvListFile = New DevExpress.XtraGrid.GridControl()
        Me.gdvListFileCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemHyperLinkEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btGhi
        '
        Me.btGhi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btGhi.Appearance.Options.UseFont = True
        Me.btGhi.Image = Global.BACSOFT.My.Resources.Resources.Save_18
        Me.btGhi.Location = New System.Drawing.Point(581, 432)
        Me.btGhi.Name = "btGhi"
        Me.btGhi.Size = New System.Drawing.Size(75, 23)
        Me.btGhi.TabIndex = 2
        Me.btGhi.Text = "Lưu lại"
        '
        'tbNoiDung
        '
        Me.tbNoiDung.Location = New System.Drawing.Point(12, 32)
        Me.tbNoiDung.Name = "tbNoiDung"
        Me.tbNoiDung.Size = New System.Drawing.Size(647, 212)
        Me.tbNoiDung.TabIndex = 0
        '
        'btXoaFile
        '
        Me.btXoaFile.Image = Global.BACSOFT.My.Resources.Resources.Delete_File_16
        Me.btXoaFile.Location = New System.Drawing.Point(94, 432)
        Me.btXoaFile.Name = "btXoaFile"
        Me.btXoaFile.Size = New System.Drawing.Size(75, 23)
        Me.btXoaFile.TabIndex = 5
        Me.btXoaFile.Text = "Xoá file"
        '
        'btThemFile
        '
        Me.btThemFile.Image = Global.BACSOFT.My.Resources.Resources.attachment_16
        Me.btThemFile.Location = New System.Drawing.Point(13, 432)
        Me.btThemFile.Name = "btThemFile"
        Me.btThemFile.Size = New System.Drawing.Size(75, 23)
        Me.btThemFile.TabIndex = 4
        Me.btThemFile.Text = "Thêm file"
        '
        'gdvListFile
        '
        Me.gdvListFile.Location = New System.Drawing.Point(12, 250)
        Me.gdvListFile.MainView = Me.gdvListFileCT
        Me.gdvListFile.Name = "gdvListFile"
        Me.gdvListFile.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemHyperLinkEdit4})
        Me.gdvListFile.Size = New System.Drawing.Size(647, 176)
        Me.gdvListFile.TabIndex = 1
        Me.gdvListFile.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvListFileCT})
        '
        'gdvListFileCT
        '
        Me.gdvListFileCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn52})
        Me.gdvListFileCT.GridControl = Me.gdvListFile
        Me.gdvListFileCT.Name = "gdvListFileCT"
        Me.gdvListFileCT.OptionsBehavior.Editable = False
        Me.gdvListFileCT.OptionsBehavior.ReadOnly = True
        Me.gdvListFileCT.OptionsView.ShowGroupPanel = False
        Me.gdvListFileCT.RowHeight = 22
        '
        'GridColumn52
        '
        Me.GridColumn52.Caption = "File"
        Me.GridColumn52.ColumnEdit = Me.RepositoryItemHyperLinkEdit4
        Me.GridColumn52.FieldName = "File"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.OptionsColumn.AllowEdit = False
        Me.GridColumn52.OptionsColumn.ReadOnly = True
        Me.GridColumn52.Visible = True
        Me.GridColumn52.VisibleIndex = 0
        Me.GridColumn52.Width = 500
        '
        'RepositoryItemHyperLinkEdit4
        '
        Me.RepositoryItemHyperLinkEdit4.AutoHeight = False
        Me.RepositoryItemHyperLinkEdit4.Name = "RepositoryItemHyperLinkEdit4"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "Nội dung"
        '
        'frmCNQuaTrinhXuLy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 468)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tbNoiDung)
        Me.Controls.Add(Me.btXoaFile)
        Me.Controls.Add(Me.btThemFile)
        Me.Controls.Add(Me.btGhi)
        Me.Controls.Add(Me.gdvListFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmCNQuaTrinhXuLy"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cập nhật quá trình xử lý yêu cầu đến"
        CType(Me.tbNoiDung.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvListFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvListFileCT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemHyperLinkEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btGhi As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gdvListFile As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvListFileCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemHyperLinkEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit
    Friend WithEvents btXoaFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btThemFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tbNoiDung As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
