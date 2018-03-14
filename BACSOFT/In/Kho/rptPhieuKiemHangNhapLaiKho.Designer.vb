<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptPhieuKiemHangNhapLaiKho
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptPhieuKiemHangNhapLaiKho))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.tbDetail = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.cellSTTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellTenVTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.CellHangSXCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellMaVTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellDVTCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.cellSLCT = New DevExpress.XtraReports.UI.XRTableCell()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.lbTieuDe = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.lblGhiChu = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
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
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        CType(Me.tbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tbDetail.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.tbDetail.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.tbDetail.LocationFloat = New DevExpress.Utils.PointFloat(35.45829!, 0.0!)
        Me.tbDetail.Name = "tbDetail"
        Me.tbDetail.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.tbDetail.SizeF = New System.Drawing.SizeF(725.0002!, 25.0!)
        Me.tbDetail.StylePriority.UseBorders = False
        Me.tbDetail.StylePriority.UseFont = False
        Me.tbDetail.StylePriority.UseTextAlignment = False
        Me.tbDetail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.cellSTTCT, Me.cellTenVTCT, Me.CellHangSXCT, Me.cellMaVTCT, Me.cellDVTCT, Me.cellSLCT, Me.XrTableCell2})
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
        Me.cellTenVTCT.Weight = 0.91372900771208942R
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
        Me.CellHangSXCT.Weight = 0.53250280888571R
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
        Me.cellMaVTCT.Weight = 0.58350645240622068R
        '
        'cellDVTCT
        '
        Me.cellDVTCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.TenDVT")})
        Me.cellDVTCT.Name = "cellDVTCT"
        Me.cellDVTCT.Text = "ĐVT"
        Me.cellDVTCT.Weight = 0.23513151217908712R
        '
        'cellSLCT
        '
        Me.cellSLCT.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.SoLuong")})
        Me.cellSLCT.Name = "cellSLCT"
        Me.cellSLCT.Text = "Số lượng"
        Me.cellSLCT.Weight = 0.28699798644052832R
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
        Me.lbTieuDe.LocationFloat = New DevExpress.Utils.PointFloat(63.50835!, 111.8!)
        Me.lbTieuDe.Name = "lbTieuDe"
        Me.lbTieuDe.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbTieuDe.SizeF = New System.Drawing.SizeF(680.0001!, 30.29997!)
        Me.lbTieuDe.StylePriority.UseFont = False
        Me.lbTieuDe.StylePriority.UseTextAlignment = False
        Me.lbTieuDe.Text = "PHIẾU KIỂM HÀNG NHẬP LẠI KHO"
        Me.lbTieuDe.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1, Me.lblGhiChu})
        Me.ReportFooter.HeightF = 89.74997!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        '
        'lblGhiChu
        '
        Me.lblGhiChu.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblGhiChu.LocationFloat = New DevExpress.Utils.PointFloat(35.45828!, 33.0!)
        Me.lblGhiChu.Name = "lblGhiChu"
        Me.lblGhiChu.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblGhiChu.SizeF = New System.Drawing.SizeF(724.9831!, 27.0833!)
        Me.lblGhiChu.StylePriority.UseFont = False
        Me.lblGhiChu.StylePriority.UseTextAlignment = False
        Me.lblGhiChu.Text = "* Ghi chú:"
        Me.lblGhiChu.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel7, Me.XrLabel5, Me.XrLabel6, Me.XrLine3, Me.XrLabel14, Me.XrLabel1, Me.pLogo, Me.XrLine2, Me.lbTieuDe})
        Me.ReportHeader.HeightF = 195.3834!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.NgayXuat", "Ngày in {0:dd/MM/yyyy}")})
        Me.XrLabel7.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(544.5499!, 157.4!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(198.9586!, 23.0!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "[Table.NgayXuat]"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.MaKH", "Mã KH: {0}")})
        Me.XrLabel5.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(35.45828!, 157.4!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(298.7503!, 23.0!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.SoCG", "Số CG {0}")})
        Me.XrLabel6.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(351.6331!, 157.4!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(192.9169!, 23.0!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLine3
        '
        Me.XrLine3.LocationFloat = New DevExpress.Utils.PointFloat(35.4586!, 88.88335!)
        Me.XrLine3.Name = "XrLine3"
        Me.XrLine3.SizeF = New System.Drawing.SizeF(724.9998!, 2.0!)
        '
        'XrLabel14
        '
        Me.XrLabel14.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(270.7!, 38.20834!)
        Me.XrLabel14.Multiline = True
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(419.7584!, 50.675!)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "Địa chỉ: Vân Tra - An Đồng - An Dương - Hải Phòng" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tell: 02253.797877/ 78/ 79" & Global.Microsoft.VisualBasic.ChrW(9) & Global.Microsoft.VisualBasic.ChrW(9) & "Fa" & _
    "x: 0225.686182" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Website: www.baoanjsc.com.vn" & Global.Microsoft.VisualBasic.ChrW(9) & "Email: baoanjsc@gmail.com"
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
        Me.pLogo.LocationFloat = New DevExpress.Utils.PointFloat(35.45829!, 10.00001!)
        Me.pLogo.Name = "pLogo"
        Me.pLogo.SizeF = New System.Drawing.SizeF(159.2001!, 70.0!)
        Me.pLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage
        '
        'tbHeader
        '
        Me.tbHeader.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.tbHeader.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold)
        Me.tbHeader.LocationFloat = New DevExpress.Utils.PointFloat(35.45888!, 33.0!)
        Me.tbHeader.Name = "tbHeader"
        Me.tbHeader.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.tbHeader.SizeF = New System.Drawing.SizeF(725.0!, 25.0!)
        Me.tbHeader.StylePriority.UseBorders = False
        Me.tbHeader.StylePriority.UseFont = False
        Me.tbHeader.StylePriority.UseTextAlignment = False
        Me.tbHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.cellSTT, Me.cellTenVT, Me.cellHangSX, Me.cellMaVT, Me.cellDVT, Me.cellSL, Me.XrTableCell1})
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
        Me.cellTenVT.Weight = 0.91372786113763982R
        '
        'cellHangSX
        '
        Me.cellHangSX.Name = "cellHangSX"
        Me.cellHangSX.Text = "Hãng SX"
        Me.cellHangSX.Weight = 0.5325027484821615R
        '
        'cellMaVT
        '
        Me.cellMaVT.Name = "cellMaVT"
        Me.cellMaVT.Text = "Mã hàng/ vật tư"
        Me.cellMaVT.Weight = 0.58350624521500716R
        '
        'cellDVT
        '
        Me.cellDVT.Name = "cellDVT"
        Me.cellDVT.Text = "ĐVT"
        Me.cellDVT.Weight = 0.23513204744129673R
        '
        'cellSL
        '
        Me.cellSL.Name = "cellSL"
        Me.cellSL.Text = "Xuất tạm"
        Me.cellSL.Weight = 0.28699801420422671R
        '
        'XrLine2
        '
        Me.XrLine2.LocationFloat = New DevExpress.Utils.PointFloat(35.45829!, 189.4!)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.SizeF = New System.Drawing.SizeF(725.0001!, 2.0!)
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "Nhập lại"
        Me.XrTableCell1.Weight = 0.29045745481307916R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Weight = 0.29045736344365475R
        '
        'XrTable1
        '
        Me.XrTable1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable1.Font = New System.Drawing.Font("Times New Roman", 11.0!)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(35.4586!, 0.0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow3})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(725.0002!, 25.0!)
        Me.XrTable1.StylePriority.UseBorders = False
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell3, Me.XrTableCell7, Me.XrTableCell8, Me.XrTableCell9})
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1.0R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Weight = 0.16597514576895833R
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell7.StylePriority.UseFont = False
        Me.XrTableCell7.StylePriority.UsePadding = False
        Me.XrTableCell7.StylePriority.UseTextAlignment = False
        Me.XrTableCell7.Text = "Tổng"
        Me.XrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.XrTableCell7.Weight = 2.2648697811831071R
        '
        'XrTableCell8
        '
        Me.XrTableCell8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table1.SoLuong")})
        Me.XrTableCell8.Font = New System.Drawing.Font("Times New Roman", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.StylePriority.UseFont = False
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrTableCell8.Summary = XrSummary1
        Me.XrTableCell8.Text = "XrTableCell8"
        Me.XrTableCell8.Weight = 0.28699798644052832R
        '
        'XrTableCell9
        '
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.Weight = 0.29045736344365475R
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.tbHeader})
        Me.PageHeader.HeightF = 58.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'rptPhieuKiemHangNhapLaiKho
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportFooter, Me.ReportHeader, Me.PageHeader})
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
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents lbTieuDe As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
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
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pLogo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblGhiChu As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
End Class
