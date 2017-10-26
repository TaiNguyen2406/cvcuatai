<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptPhieuXuatKhoGop
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptPhieuXuatKhoGop))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.tbDetail = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.cellSTTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellTenVTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.CellHangSXCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellMaVTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellDVTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellSLCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellPOCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.lbTieuDe = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLine3 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.pLogo = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.tbHeader = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.cellSTT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellTenVT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellHangSX = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellMaVT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellDVT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellSL = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellPO = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.tbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.tbDetail})
        Me.Detail.HeightF = 25.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'tbDetail
        '
        Me.tbDetail.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.tbDetail.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.tbDetail.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.tbDetail.Name = "tbDetail"
        Me.tbDetail.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.tbDetail.SizeF = New System.Drawing.SizeF(761.9999!, 25.0!)
        Me.tbDetail.StylePriority.UseBorders = False
        Me.tbDetail.StylePriority.UseFont = False
        Me.tbDetail.StylePriority.UseTextAlignment = False
        Me.tbDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.cellSTTCT, Me.cellTenVTCT, Me.CellHangSXCT, Me.cellMaVTCT, Me.cellDVTCT, Me.cellSLCT, Me.cellPOCT})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'cellSTTCT
        '
        Me.cellSTTCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.STT")})
        Me.cellSTTCT.Name = "cellSTTCT"
        Me.cellSTTCT.Text = "STT"
        Me.cellSTTCT.Weight = 0.16597514576895833R
        '
        'cellTenVTCT
        '
        Me.cellTenVTCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.TenVT")})
        Me.cellTenVTCT.Multiline = True
        Me.cellTenVTCT.Name = "cellTenVTCT"
        Me.cellTenVTCT.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.cellTenVTCT.StylePriority.UsePadding = False
        Me.cellTenVTCT.StylePriority.UseTextAlignment = False
        Me.cellTenVTCT.Text = "Tên vật tư"
        Me.cellTenVTCT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.cellTenVTCT.Weight = 0.82233150498305241R
        '
        'CellHangSXCT
        '
        Me.CellHangSXCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.TenHang")})
        Me.CellHangSXCT.Multiline = True
        Me.CellHangSXCT.Name = "CellHangSXCT"
        Me.CellHangSXCT.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.CellHangSXCT.StylePriority.UsePadding = False
        Me.CellHangSXCT.StylePriority.UseTextAlignment = False
        Me.CellHangSXCT.Text = "Hãng SX"
        Me.CellHangSXCT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.CellHangSXCT.Weight = 0.49038113615777479R
        '
        'cellMaVTCT
        '
        Me.cellMaVTCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.Model")})
        Me.cellMaVTCT.Multiline = True
        Me.cellMaVTCT.Name = "cellMaVTCT"
        Me.cellMaVTCT.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.cellMaVTCT.StylePriority.UsePadding = False
        Me.cellMaVTCT.StylePriority.UseTextAlignment = False
        Me.cellMaVTCT.Text = "Mã vật tư"
        Me.cellMaVTCT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.cellMaVTCT.Weight = 0.78460944359831708R
        '
        'cellDVTCT
        '
        Me.cellDVTCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.TenDVT")})
        Me.cellDVTCT.Name = "cellDVTCT"
        Me.cellDVTCT.Text = "ĐVT"
        Me.cellDVTCT.Weight = 0.26751079467308159R
        '
        'cellSLCT
        '
        Me.cellSLCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.SoLuong")})
        Me.cellSLCT.Name = "cellSLCT"
        Me.cellSLCT.Text = "Số lượng"
        Me.cellSLCT.Weight = 0.2926709645780905R
        '
        'cellPOCT
        '
        Me.cellPOCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.SoPO")})
        Me.cellPOCT.Name = "cellPOCT"
        Me.cellPOCT.Text = "cellPOCT"
        Me.cellPOCT.Weight = 0.33834701769860986R
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 15.625!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1})
        Me.BottomMargin.HeightF = 48.95833!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(660.4583!, 0.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(100.0!, 23.0!)
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'lbTieuDe
        '
        Me.lbTieuDe.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lbTieuDe.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 111.8!)
        Me.lbTieuDe.Name = "lbTieuDe"
        Me.lbTieuDe.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbTieuDe.SizeF = New System.Drawing.SizeF(761.9999!, 30.29996!)
        Me.lbTieuDe.StylePriority.UseFont = False
        Me.lbTieuDe.StylePriority.UseTextAlignment = False
        Me.lbTieuDe.Text = "PHIẾU XUẤT KHO"
        Me.lbTieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(62.25834!, 10.0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(119.7917!, 23.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "KHÁCH HÀNG"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(333.6364!, 10.0!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(118.75!, 23.0!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "KINH DOANH"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(543.3!, 10.0!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(198.9583!, 23.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "CTY CP DVKT BẢO AN"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel13, Me.XrLabel3, Me.XrLabel2, Me.XrLabel4, Me.XrLabel15, Me.XrLabel16})
        Me.ReportFooter.HeightF = 163.7083!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.TakeCare")})
        Me.XrLabel13.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(273.0485!, 97.18184!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(237.7704!, 23.00001!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel15
        '
        Me.XrLabel15.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(177.9917!, 33.0!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(467.3916!, 23.0!)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(177.9917!, 56.0!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(467.3916!, 23.0!)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine3, Me.XrLabel14, Me.XrLabel1, Me.pLogo, Me.tbHeader, Me.XrLabel10, Me.XrLabel9, Me.XrLabel8, Me.XrLabel7, Me.XrLabel6, Me.XrLabel5, Me.lbTieuDe})
        Me.ReportHeader.HeightF = 250.5999!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLine3
        '
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 88.88333!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(762.0!, 2.0!)
        '
        'XrLabel14
        '
        Me.XrLabel14.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(270.6999!, 38.20834!)
        Me.XrLabel14.Multiline = True
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(419.7584!, 50.675!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "Địa chỉ: Vân Tra - An Đồng - An Dương - Hải Phòng" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tell: 0313.797877/ 78/ 79" & Global.Microsoft.VisualBasic.ChrW(9) & Global.Microsoft.VisualBasic.ChrW(9) & "Fax" & _
    ": 0313.686182" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Website: www.baoanjsc.com.vn" & Global.Microsoft.VisualBasic.ChrW(9) & "Email: baoanjsc@gmail.com"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(194.6584!, 10.00001!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(547.5999!, 28.20834!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "CÔNG TY CỔ PHẦN DỊCH VỤ KỸ THUẬT BẢO AN"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'pLogo
        '
        Me.pLogo.LocationFloat = New DevExpress.Utils.PointFloat(35.45828!, 10.00001!)
        Me.pLogo.Name = "pLogo"
        Me.pLogo.SizeF = New System.Drawing.SizeF(159.2001!, 70.0!)
        Me.pLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage
        '
        'tbHeader
        '
        Me.tbHeader.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.tbHeader.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold)
        Me.tbHeader.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 225.5999!)
        Me.tbHeader.Name = "tbHeader"
        Me.tbHeader.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.tbHeader.SizeF = New System.Drawing.SizeF(762.0!, 25.0!)
        Me.tbHeader.StylePriority.UseBorders = False
        Me.tbHeader.StylePriority.UseFont = False
        Me.tbHeader.StylePriority.UseTextAlignment = False
        Me.tbHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.cellSTT, Me.cellTenVT, Me.cellHangSX, Me.cellMaVT, Me.cellDVT, Me.cellSL, Me.cellPO})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'cellSTT
        '
        Me.cellSTT.Name = "cellSTT"
        Me.cellSTT.Text = "STT"
        Me.cellSTT.Weight = 0.16597514576895833R
        '
        'cellTenVT
        '
        Me.cellTenVT.Name = "cellTenVT"
        Me.cellTenVT.Text = "Tên hàng hóa/ vật tư"
        Me.cellTenVT.Weight = 0.82233161700404434R
        '
        'cellHangSX
        '
        Me.cellHangSX.Name = "cellHangSX"
        Me.cellHangSX.Text = "Hãng SX"
        Me.cellHangSX.Weight = 0.49038107220815375R
        '
        'cellMaVT
        '
        Me.cellMaVT.Name = "cellMaVT"
        Me.cellMaVT.Text = "Mã hàng/ vật tư"
        Me.cellMaVT.Weight = 0.78460979151043808R
        '
        'cellDVT
        '
        Me.cellDVT.Name = "cellDVT"
        Me.cellDVT.Text = "ĐVT"
        Me.cellDVT.Weight = 0.26751057288726504R
        '
        'cellSL
        '
        Me.cellSL.Name = "cellSL"
        Me.cellSL.Text = "Số lượng"
        Me.cellSL.Weight = 0.29267137270633747R
        '
        'cellPO
        '
        Me.cellPO.Name = "cellPO"
        Me.cellPO.Text = "Số PO"
        Me.cellPO.Weight = 0.338346954813459R
        '
        'XrLabel10
        '
        Me.XrLabel10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.NguoiXuat")})
        Me.XrLabel10.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(447.9206!, 200.6!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(324.0795!, 23.0!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.StylePriority.UseTextAlignment = False
        Me.XrLabel10.Text = "XrLabel10"
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ChucDanh")})
        Me.XrLabel9.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 200.6!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(414.5833!, 23.0!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.StylePriority.UseTextAlignment = False
        Me.XrLabel9.Text = "XrLabel9"
        Me.XrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.Kho")})
        Me.XrLabel8.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(447.9206!, 177.5999!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(324.0793!, 23.0!)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "XrLabel8"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.DaiDien")})
        Me.XrLabel7.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 177.5999!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(414.5833!, 23.0!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.Ngay")})
        Me.XrLabel6.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(447.9206!, 154.5999!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(324.0793!, 23.0!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.TenKH")})
        Me.XrLabel5.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(10.0!, 154.5999!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(414.5833!, 23.0!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'rptPhieuXuatKhoGop
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportFooter, Me.ReportHeader})
        Me.Bookmark = "PhieuXuatKho"
        Me.DataMember = "Table1"
        Me.DataSourceSchema = resources.GetString("$this.DataSourceSchema")
        Me.DrawGrid = False
        Me.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Margins = New System.Drawing.Printing.Margins(18, 27, 16, 49)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.RequestParameters = False
        Me.ShowPrintMarginsWarning = False
        Me.SnapGridSize = 0.1!
        Me.Version = "10.2"
        CType(Me.tbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents lbTieuDe As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents tbHeader As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents cellSTT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellTenVT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellHangSX As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellMaVT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellDVT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellSL As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tbDetail As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents cellSTTCT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellTenVTCT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents CellHangSXCT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellMaVTCT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellDVTCT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellSLCT As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pLogo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents cellPO As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cellPOCT As DevExpress.XtraReports.UI.XRTableCell
End Class
