<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToolChucNang
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
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnCapNhatCSDL = New DevExpress.XtraEditors.SimpleButton()
        Me.prc = New DevExpress.XtraEditors.ProgressBarControl()
        Me.btnTinhDiemThiKyNang = New DevExpress.XtraEditors.SimpleButton()
        Me.lstDiemKyNang = New DevExpress.XtraEditors.ListBoxControl()
        Me.gdvDiemKyNang = New DevExpress.XtraGrid.GridControl()
        Me.gdvDataDiemKyNang = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnLayDiemGocVaKyNang = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.btCapNhapSP = New DevExpress.XtraEditors.SimpleButton()
        Me.btTaiDS7So = New DevExpress.XtraEditors.SimpleButton()
        Me.gdvSoYC = New DevExpress.XtraGrid.GridControl()
        Me.gdvSoYCCT = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.prc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lstDiemKyNang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvDiemKyNang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvDataDiemKyNang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.gdvSoYC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gdvSoYCCT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1054, 519)
        Me.XtraTabControl1.TabIndex = 0
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.PanelControl1)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1048, 493)
        Me.XtraTabPage1.Text = "Tính điểm thi kỹ năng"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnCapNhatCSDL)
        Me.PanelControl1.Controls.Add(Me.prc)
        Me.PanelControl1.Controls.Add(Me.btnTinhDiemThiKyNang)
        Me.PanelControl1.Controls.Add(Me.lstDiemKyNang)
        Me.PanelControl1.Controls.Add(Me.gdvDiemKyNang)
        Me.PanelControl1.Controls.Add(Me.btnLayDiemGocVaKyNang)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1048, 493)
        Me.PanelControl1.TabIndex = 0
        '
        'btnCapNhatCSDL
        '
        Me.btnCapNhatCSDL.Enabled = False
        Me.btnCapNhatCSDL.Location = New System.Drawing.Point(394, 15)
        Me.btnCapNhatCSDL.Name = "btnCapNhatCSDL"
        Me.btnCapNhatCSDL.Size = New System.Drawing.Size(115, 23)
        Me.btnCapNhatCSDL.TabIndex = 5
        Me.btnCapNhatCSDL.Text = "Cập nhật vào CSDL"
        '
        'prc
        '
        Me.prc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.prc.Location = New System.Drawing.Point(11, 44)
        Me.prc.Name = "prc"
        Me.prc.Properties.ShowTitle = True
        Me.prc.Size = New System.Drawing.Size(847, 18)
        Me.prc.TabIndex = 4
        Me.prc.Visible = False
        '
        'btnTinhDiemThiKyNang
        '
        Me.btnTinhDiemThiKyNang.Enabled = False
        Me.btnTinhDiemThiKyNang.Location = New System.Drawing.Point(262, 15)
        Me.btnTinhDiemThiKyNang.Name = "btnTinhDiemThiKyNang"
        Me.btnTinhDiemThiKyNang.Size = New System.Drawing.Size(115, 23)
        Me.btnTinhDiemThiKyNang.TabIndex = 3
        Me.btnTinhDiemThiKyNang.Text = "Bắt đầu tính toán"
        '
        'lstDiemKyNang
        '
        Me.lstDiemKyNang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstDiemKyNang.Location = New System.Drawing.Point(877, 61)
        Me.lstDiemKyNang.Name = "lstDiemKyNang"
        Me.lstDiemKyNang.Size = New System.Drawing.Size(164, 416)
        Me.lstDiemKyNang.TabIndex = 2
        '
        'gdvDiemKyNang
        '
        Me.gdvDiemKyNang.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gdvDiemKyNang.Location = New System.Drawing.Point(11, 61)
        Me.gdvDiemKyNang.MainView = Me.gdvDataDiemKyNang
        Me.gdvDiemKyNang.Name = "gdvDiemKyNang"
        Me.gdvDiemKyNang.Size = New System.Drawing.Size(847, 416)
        Me.gdvDiemKyNang.TabIndex = 1
        Me.gdvDiemKyNang.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvDataDiemKyNang})
        '
        'gdvDataDiemKyNang
        '
        Me.gdvDataDiemKyNang.GridControl = Me.gdvDiemKyNang
        Me.gdvDataDiemKyNang.Name = "gdvDataDiemKyNang"
        Me.gdvDataDiemKyNang.OptionsBehavior.Editable = False
        Me.gdvDataDiemKyNang.OptionsBehavior.ReadOnly = True
        Me.gdvDataDiemKyNang.OptionsSelection.MultiSelect = True
        Me.gdvDataDiemKyNang.OptionsView.ColumnAutoWidth = False
        Me.gdvDataDiemKyNang.OptionsView.ShowFooter = True
        Me.gdvDataDiemKyNang.OptionsView.ShowGroupPanel = False
        '
        'btnLayDiemGocVaKyNang
        '
        Me.btnLayDiemGocVaKyNang.Location = New System.Drawing.Point(11, 15)
        Me.btnLayDiemGocVaKyNang.Name = "btnLayDiemGocVaKyNang"
        Me.btnLayDiemGocVaKyNang.Size = New System.Drawing.Size(245, 23)
        Me.btnLayDiemGocVaKyNang.TabIndex = 0
        Me.btnLayDiemGocVaKyNang.Text = "Lấy dữ liệu gốc và danh sách ngày thi kỹ năng"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.btCapNhapSP)
        Me.XtraTabPage2.Controls.Add(Me.btTaiDS7So)
        Me.XtraTabPage2.Controls.Add(Me.gdvSoYC)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1048, 493)
        Me.XtraTabPage2.Text = "Cập nhật số phiếu"
        '
        'btCapNhapSP
        '
        Me.btCapNhapSP.Location = New System.Drawing.Point(335, 38)
        Me.btCapNhapSP.Name = "btCapNhapSP"
        Me.btCapNhapSP.Size = New System.Drawing.Size(115, 23)
        Me.btCapNhapSP.TabIndex = 5
        Me.btCapNhapSP.Text = "Cập nhật số phiếu"
        '
        'btTaiDS7So
        '
        Me.btTaiDS7So.Location = New System.Drawing.Point(335, 9)
        Me.btTaiDS7So.Name = "btTaiDS7So"
        Me.btTaiDS7So.Size = New System.Drawing.Size(115, 23)
        Me.btTaiDS7So.TabIndex = 4
        Me.btTaiDS7So.Text = "Tải DS YC 7 số"
        '
        'gdvSoYC
        '
        Me.gdvSoYC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gdvSoYC.Location = New System.Drawing.Point(3, 9)
        Me.gdvSoYC.MainView = Me.gdvSoYCCT
        Me.gdvSoYC.Name = "gdvSoYC"
        Me.gdvSoYC.Size = New System.Drawing.Size(309, 477)
        Me.gdvSoYC.TabIndex = 2
        Me.gdvSoYC.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gdvSoYCCT})
        '
        'gdvSoYCCT
        '
        Me.gdvSoYCCT.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3})
        Me.gdvSoYCCT.GridControl = Me.gdvSoYC
        Me.gdvSoYCCT.Name = "gdvSoYCCT"
        Me.gdvSoYCCT.OptionsBehavior.Editable = False
        Me.gdvSoYCCT.OptionsBehavior.ReadOnly = True
        Me.gdvSoYCCT.OptionsSelection.MultiSelect = True
        Me.gdvSoYCCT.OptionsView.ColumnAutoWidth = False
        Me.gdvSoYCCT.OptionsView.ShowFooter = True
        Me.gdvSoYCCT.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Số YC"
        Me.GridColumn1.FieldName = "SoPhieu"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.SummaryItem.DisplayFormat = "{0:N0}"
        Me.GridColumn1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Ngày"
        Me.GridColumn2.FieldName = "NgayThang"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Số YC mới"
        Me.GridColumn3.FieldName = "SoYCMoi"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 99
        '
        'frmToolChucNang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1054, 519)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Name = "frmToolChucNang"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmToolChucNang"
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.prc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lstDiemKyNang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvDiemKyNang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvDataDiemKyNang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.gdvSoYC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gdvSoYCCT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnLayDiemGocVaKyNang As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lstDiemKyNang As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents gdvDiemKyNang As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvDataDiemKyNang As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnTinhDiemThiKyNang As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents prc As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents btnCapNhatCSDL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btTaiDS7So As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gdvSoYC As DevExpress.XtraGrid.GridControl
    Friend WithEvents gdvSoYCCT As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btCapNhapSP As DevExpress.XtraEditors.SimpleButton
End Class
