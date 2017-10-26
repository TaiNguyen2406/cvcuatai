<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptDiaChiGiaoHang
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.pLogo = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.lbDCNhan = New DevExpress.XtraReports.UI.XRLabel()
        Me.lbCtyNhan = New DevExpress.XtraReports.UI.XRLabel()
        Me.lbNguoiNhan = New DevExpress.XtraReports.UI.XRLabel()
        Me.lbNguoiGui = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lbDCGui = New DevExpress.XtraReports.UI.XRLabel()
        Me.lbTenCTyGui = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrCrossBandBox1 = New DevExpress.XtraReports.UI.XRCrossBandBox()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pLogo, Me.lbDCNhan, Me.lbCtyNhan, Me.lbNguoiNhan, Me.lbNguoiGui, Me.XrLabel4, Me.lbDCGui, Me.lbTenCTyGui, Me.XrLabel1})
        Me.Detail.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Detail.HeightF = 546.3334!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.StylePriority.UseFont = False
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'pLogo
        '
        Me.pLogo.LocationFloat = New DevExpress.Utils.PointFloat(22.50011!, 10.00001!)
        Me.pLogo.Name = "pLogo"
        Me.pLogo.SizeF = New System.Drawing.SizeF(117.7083!, 58.41667!)
        Me.pLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage
        '
        'lbDCNhan
        '
        Me.lbDCNhan.Font = New System.Drawing.Font("Times New Roman", 20.0!)
        Me.lbDCNhan.LocationFloat = New DevExpress.Utils.PointFloat(23.54172!, 336.625!)
        Me.lbDCNhan.Name = "lbDCNhan"
        Me.lbDCNhan.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbDCNhan.SizeF = New System.Drawing.SizeF(756.4582!, 36.54169!)
        Me.lbDCNhan.StylePriority.UseFont = False
        Me.lbDCNhan.StylePriority.UseTextAlignment = False
        Me.lbDCNhan.Text = "Đ/C: Địa chỉ nhận"
        Me.lbDCNhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'lbCtyNhan
        '
        Me.lbCtyNhan.Font = New System.Drawing.Font("Times New Roman", 20.25!)
        Me.lbCtyNhan.LocationFloat = New DevExpress.Utils.PointFloat(23.54167!, 286.5417!)
        Me.lbCtyNhan.Name = "lbCtyNhan"
        Me.lbCtyNhan.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbCtyNhan.SizeF = New System.Drawing.SizeF(756.4583!, 36.54166!)
        Me.lbCtyNhan.StylePriority.UseFont = False
        Me.lbCtyNhan.StylePriority.UseTextAlignment = False
        Me.lbCtyNhan.Text = "CÔNG TY NHẬN"
        Me.lbCtyNhan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'lbNguoiNhan
        '
        Me.lbNguoiNhan.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbNguoiNhan.LocationFloat = New DevExpress.Utils.PointFloat(178.7501!, 241.3333!)
        Me.lbNguoiNhan.Name = "lbNguoiNhan"
        Me.lbNguoiNhan.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbNguoiNhan.SizeF = New System.Drawing.SizeF(601.2498!, 30.29167!)
        Me.lbNguoiNhan.StylePriority.UseFont = False
        '
        'lbNguoiGui
        '
        Me.lbNguoiGui.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lbNguoiGui.LocationFloat = New DevExpress.Utils.PointFloat(165.2084!, 105.8333!)
        Me.lbNguoiGui.Name = "lbNguoiGui"
        Me.lbNguoiGui.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbNguoiGui.SizeF = New System.Drawing.SizeF(614.7916!, 30.29167!)
        Me.lbNguoiGui.StylePriority.UseFont = False
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(23.54167!, 241.3333!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(155.2084!, 30.29167!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "Người nhận:"
        '
        'lbDCGui
        '
        Me.lbDCGui.Font = New System.Drawing.Font("Times New Roman", 16.0!)
        Me.lbDCGui.LocationFloat = New DevExpress.Utils.PointFloat(151.6668!, 46.54166!)
        Me.lbDCGui.Name = "lbDCGui"
        Me.lbDCGui.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbDCGui.SizeF = New System.Drawing.SizeF(632.708!, 36.54169!)
        Me.lbDCGui.StylePriority.UseFont = False
        Me.lbDCGui.StylePriority.UseTextAlignment = False
        Me.lbDCGui.Text = "Đ/C: Vân Tra - An Đồng - An Dương - Hải Phòng"
        Me.lbDCGui.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'lbTenCTyGui
        '
        Me.lbTenCTyGui.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Bold)
        Me.lbTenCTyGui.LocationFloat = New DevExpress.Utils.PointFloat(151.6668!, 10.0!)
        Me.lbTenCTyGui.Name = "lbTenCTyGui"
        Me.lbTenCTyGui.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbTenCTyGui.SizeF = New System.Drawing.SizeF(632.7081!, 36.54167!)
        Me.lbTenCTyGui.StylePriority.UseFont = False
        Me.lbTenCTyGui.StylePriority.UseTextAlignment = False
        Me.lbTenCTyGui.Text = "CÔNG TY CP DỊCH VỤ KỸ THUẬT BẢO AN"
        Me.lbTenCTyGui.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(23.54172!, 105.8333!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(141.6667!, 30.29167!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "Người gửi:"
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 22.91667!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 17.70833!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrCrossBandBox1
        '
        Me.XrCrossBandBox1.EndBand = Me.Detail
        Me.XrCrossBandBox1.EndPointFloat = New DevExpress.Utils.PointFloat(10.00001!, 524.4584!)
        Me.XrCrossBandBox1.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.XrCrossBandBox1.Name = "XrCrossBandBox1"
        Me.XrCrossBandBox1.StartBand = Me.Detail
        Me.XrCrossBandBox1.StartPointFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.XrCrossBandBox1.WidthF = 776.4582!
        '
        'rptDiaChiGiaoHang
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.Bookmark = "DiaChiGiaoHang"
        Me.CrossBandControls.AddRange(New DevExpress.XtraReports.UI.XRCrossBandControl() {Me.XrCrossBandBox1})
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(15, 22, 23, 18)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.Version = "10.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents lbTenCTyGui As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrCrossBandBox1 As DevExpress.XtraReports.UI.XRCrossBandBox
    Friend WithEvents lbDCGui As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbNguoiGui As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbDCNhan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbCtyNhan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbNguoiNhan As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents pLogo As DevExpress.XtraReports.UI.XRPictureBox
End Class
