<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptThongTinDuAn_2

    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptThongTinDuAn_2))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.FormattingRule3 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.FormattingRule2 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.FormattingRule1 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.FormattingRule4 = New DevExpress.XtraReports.UI.FormattingRule()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.prmHTTT = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm14 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm13 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm12 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm11 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm10 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm9 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm8 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm7 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm6 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm5 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm4 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm3 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm2 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.prm1 = New DevExpress.XtraReports.Parameters.Parameter()
        Me.rpmDocSoTien = New DevExpress.XtraReports.Parameters.Parameter()
        Me.styleMST = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.tbl1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.LBLSTT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.lblDoiThu = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 63.5!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable1.Dpi = 254.0!
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(18.00033!, 0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(1841.103!, 63.5!)
        Me.XrTable1.StylePriority.UseBorders = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell4, Me.XrTableCell3})
        Me.XrTableRow1.Dpi = 254.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TableDTCT.STT")})
        Me.XrTableCell1.Dpi = 254.0!
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.Text = "XrTableCell1"
        Me.XrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell1.Weight = 0.44095365268977604R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TableDTCT.DoiThu")})
        Me.XrTableCell2.Dpi = 254.0!
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254.0!)
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell2.Weight = 1.7828608235036296R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TableDTCT.GiaTri")})
        Me.XrTableCell4.Dpi = 254.0!
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 254.0!)
        Me.XrTableCell4.StylePriority.UseBorders = False
        Me.XrTableCell4.StylePriority.UsePadding = False
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell4.Weight = 2.3368487020177167R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TableDTCT.DanhGia")})
        Me.XrTableCell3.Dpi = 254.0!
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 254.0!)
        Me.XrTableCell3.StylePriority.UsePadding = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell3.Weight = 2.6877729761318903R
        '
        'FormattingRule3
        '
        Me.FormattingRule3.Condition = "[ThanhTien]=0"
        '
        '
        '
        Me.FormattingRule3.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.FormattingRule3.Name = "FormattingRule3"
        '
        'FormattingRule2
        '
        Me.FormattingRule2.Condition = "[DonGia]=0"
        '
        '
        '
        Me.FormattingRule2.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.FormattingRule2.Name = "FormattingRule2"
        '
        'FormattingRule1
        '
        Me.FormattingRule1.Condition = "[SoLuong]=0"
        '
        '
        '
        Me.FormattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.FormattingRule1.Name = "FormattingRule1"
        '
        'FormattingRule4
        '
        Me.FormattingRule4.Condition = "[DataSource.CurrentRowIndex]>=1"
        '
        '
        '
        Me.FormattingRule4.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.FormattingRule4.Name = "FormattingRule4"
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.HeightF = 0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'prmHTTT
        '
        Me.prmHTTT.Name = "prmHTTT"
        Me.prmHTTT.Value = ""
        '
        'prm14
        '
        Me.prm14.Name = "prm14"
        Me.prm14.Value = ""
        '
        'prm13
        '
        Me.prm13.Name = "prm13"
        Me.prm13.Value = ""
        '
        'prm12
        '
        Me.prm12.Name = "prm12"
        Me.prm12.Value = ""
        '
        'prm11
        '
        Me.prm11.Name = "prm11"
        Me.prm11.Value = ""
        '
        'prm10
        '
        Me.prm10.Name = "prm10"
        Me.prm10.Value = ""
        '
        'prm9
        '
        Me.prm9.Name = "prm9"
        Me.prm9.Value = ""
        '
        'prm8
        '
        Me.prm8.Name = "prm8"
        Me.prm8.Value = ""
        '
        'prm7
        '
        Me.prm7.Name = "prm7"
        Me.prm7.Value = ""
        '
        'prm6
        '
        Me.prm6.Name = "prm6"
        Me.prm6.Value = ""
        '
        'prm5
        '
        Me.prm5.Name = "prm5"
        Me.prm5.Value = ""
        '
        'prm4
        '
        Me.prm4.Name = "prm4"
        Me.prm4.Value = ""
        '
        'prm3
        '
        Me.prm3.Name = "prm3"
        Me.prm3.Value = ""
        '
        'prm2
        '
        Me.prm2.Name = "prm2"
        Me.prm2.Value = ""
        '
        'prm1
        '
        Me.prm1.Name = "prm1"
        Me.prm1.Value = ""
        '
        'rpmDocSoTien
        '
        Me.rpmDocSoTien.Name = "rpmDocSoTien"
        Me.rpmDocSoTien.Value = ""
        '
        'styleMST
        '
        Me.styleMST.Name = "styleMST"
        Me.styleMST.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.tbl1})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 120.0!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'tbl1
        '
        Me.tbl1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.tbl1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.tbl1.Dpi = 254.0!
        Me.tbl1.LocationFloat = New DevExpress.Utils.PointFloat(19.00001!, 0!)
        Me.tbl1.Name = "tbl1"
        Me.tbl1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.tbl1.SizeF = New System.Drawing.SizeF(1841.103!, 120.0!)
        Me.tbl1.StylePriority.UseBorders = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.LBLSTT, Me.lblDoiThu, Me.XrTableCell16, Me.XrTableCell22})
        Me.XrTableRow2.Dpi = 254.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'LBLSTT
        '
        Me.LBLSTT.CanGrow = False
        Me.LBLSTT.Dpi = 254.0!
        Me.LBLSTT.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LBLSTT.Name = "LBLSTT"
        Me.LBLSTT.StylePriority.UseFont = False
        Me.LBLSTT.StylePriority.UseTextAlignment = False
        Me.LBLSTT.Text = "STT"
        Me.LBLSTT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.LBLSTT.Weight = 0.18207775670480769R
        '
        'lblDoiThu
        '
        Me.lblDoiThu.CanGrow = False
        Me.lblDoiThu.Dpi = 254.0!
        Me.lblDoiThu.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblDoiThu.Name = "lblDoiThu"
        Me.lblDoiThu.StylePriority.UseFont = False
        Me.lblDoiThu.StylePriority.UseTextAlignment = False
        Me.lblDoiThu.Text = "ĐỐI THỦ"
        Me.lblDoiThu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.lblDoiThu.Weight = 0.73617526793536125R
        '
        'XrTableCell16
        '
        Me.XrTableCell16.CanGrow = False
        Me.XrTableCell16.Dpi = 254.0!
        Me.XrTableCell16.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.StylePriority.UseFont = False
        Me.XrTableCell16.StylePriority.UseTextAlignment = False
        Me.XrTableCell16.Text = "GIÁ ĐÃ CHÀO"
        Me.XrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell16.Weight = 0.9649269493022582R
        '
        'XrTableCell22
        '
        Me.XrTableCell22.CanGrow = False
        Me.XrTableCell22.Dpi = 254.0!
        Me.XrTableCell22.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.StylePriority.UseFont = False
        Me.XrTableCell22.StylePriority.UseTextAlignment = False
        Me.XrTableCell22.Text = "ĐÁNH GIÁ CỦA KHÁCH HÀNG VỀ ĐỐI THỦ"
        Me.XrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell22.Weight = 1.10983012628645R
        '
        'rptThongTinDuAn_2
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader})
        Me.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.DataMember = "TableDTCT"
        Me.DataSourceSchema = resources.GetString("$this.DataSourceSchema")
        Me.DesignerOptions.ShowDesignerHints = False
        Me.DesignerOptions.ShowExportWarnings = False
        Me.DesignerOptions.ShowPrintingWarnings = False
        Me.DetailPrintCountOnEmptyDataSource = 0
        Me.Dpi = 254.0!
        Me.DrawGrid = False
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.FormattingRule1, Me.FormattingRule2, Me.FormattingRule3, Me.FormattingRule4})
        Me.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.prm1, Me.prm2, Me.prm3, Me.prm4, Me.prm5, Me.prm6, Me.prm7, Me.prm8, Me.prm9, Me.prm10, Me.prm11, Me.prm12, Me.prm13, Me.prm14, Me.rpmDocSoTien, Me.prmHTTT})
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.ScriptsSource = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ShowPrintMarginsWarning = False
        Me.SnapGridSize = 1.0!
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.styleMST})
        Me.Version = "10.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents prm1 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm2 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm3 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm4 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm5 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm6 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm7 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm8 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm9 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm10 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm11 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm12 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm13 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents prm14 As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents rpmDocSoTien As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents styleMST As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents prmHTTT As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents FormattingRule2 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents FormattingRule1 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents FormattingRule3 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents FormattingRule4 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents tbl1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents LBLSTT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents lblDoiThu As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
End Class
