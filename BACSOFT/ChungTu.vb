Imports BACSOFT.Db.SqlHelper
Public Class ChungTu

    Enum LoaiButToan
        Khac = 0
        HangTien = 1
        ThueGTGT = 2
        GiaVon = 3
        ThueNK = 4
    End Enum

    Enum LoaiChungTu
        KhongButToan = 0
        HoaDonDauRa = 1
        HoaDonDauVao = 2
        PhieuThuTienMat = 3
        PhieuChiTienMat = 4
        NopTienNganHang = 5
        UyNhiemChi = 6
        GhiTangCCDC = 7
        PhanBoCCDC = 8
        TongHop = 9
        GhepVatTu = 10
        ThaoVatTu = 11
        KetChuyenLaiLo = 12
        GhiTangTSCD = 13
        'XuatKho = 10
        'NhapKho = 11
    End Enum

    Enum LoaiCT2
        MuaHangTrongNuoc = 1
        BanHangHoa = 2
        BanDichVu = 3
        XuLyCongTrinh = 4
        MuaDichVu = 5

        MuaCongCuDungCu = 6
        MuaHangNuocNgoai = 7
        MuaTaiSanCoDinh = 8
        XuatKho = 9
        NhapKho = 10

        MuaDichVuVanChuyen = 11
    End Enum

    Shared Function getRef() As String
        Return BACSOFT.Db.SqlHelper.ExecuteSQLDataTable("select replace(newid(),'-','')").Rows(0)(0).ToString
    End Function

    Shared Function TienToCT(_loaicT As LoaiChungTu, _loaiCt2 As Object) As String
        Select Case _loaicT
            Case LoaiChungTu.HoaDonDauRa
                Select Case _loaiCt2
                    Case LoaiCT2.BanHangHoa, LoaiCT2.XuLyCongTrinh
                        Return "BHH"
                    Case LoaiCT2.BanDichVu
                        Return "BDV"
                End Select
            Case LoaiChungTu.HoaDonDauVao
                Select Case _loaiCt2
                    Case LoaiCT2.MuaHangTrongNuoc
                        Return "MHH"
                    Case LoaiCT2.MuaDichVu
                        Return "MDV"
                    Case LoaiCT2.MuaCongCuDungCu
                        Return "MDC"
                    Case LoaiCT2.MuaHangTrongNuoc
                        Return "MNN"
                    Case LoaiCT2.MuaTaiSanCoDinh
                        Return "MTS"
                End Select
            Case LoaiChungTu.PhieuChiTienMat
                Return "PC"
            Case LoaiChungTu.PhieuThuTienMat
                Return "PT"
            Case LoaiChungTu.NopTienNganHang
                Return "NT"
            Case LoaiChungTu.UyNhiemChi
                Return "UNC"
            Case LoaiChungTu.GhiTangCCDC
                Return "TDC"
            Case LoaiChungTu.PhanBoCCDC
                Return "PB"
            Case LoaiChungTu.TongHop
                Return "TH"
            Case LoaiChungTu.KetChuyenLaiLo
                Return "KC"
        End Select
        Return ""
    End Function

    Shared Function TenLoaiCT(_loaicT As LoaiChungTu, _loaiCT2 As Object) As String
        Select Case _loaicT
            Case LoaiChungTu.HoaDonDauRa
                Select Case _loaiCT2
                    Case LoaiCT2.BanHangHoa
                        Return "Hóa đơn bán hàng trong nước"
                    Case LoaiCT2.XuLyCongTrinh
                        Return "Hóa đơn xử lý công trình"
                    Case LoaiCT2.BanDichVu
                        Return "Hóa đơn bán dịch vụ"
                    Case Else
                        Return "Hóa đơn đầu ra"
                End Select
            Case LoaiChungTu.HoaDonDauVao
                Select Case _loaiCT2
                    Case LoaiCT2.MuaHangTrongNuoc
                        Return "Hóa đơn mua hàng trong nước"
                    Case LoaiCT2.MuaDichVu
                        Return "Hóa đơn mua dịch vụ"
                    Case LoaiCT2.MuaCongCuDungCu
                        Return "Hóa đơn mua công cụ dụng cụ"
                    Case LoaiCT2.MuaHangNuocNgoai
                        Return "Hóa đơn mua hàng nước ngoài"
                    Case LoaiCT2.MuaTaiSanCoDinh
                        Return "Hóa đơn mua tài sản cố định"
                    Case Else
                        Return "Hóa đơn đầu vào"
                End Select
            Case LoaiChungTu.PhieuChiTienMat
                Return "Phiếu chi tiền mặt"
            Case LoaiChungTu.PhieuThuTienMat
                Return "Phiếu thu tiền mặt"
            Case LoaiChungTu.NopTienNganHang
                Return "Nộp tiền ngân hàng"
            Case LoaiChungTu.UyNhiemChi
                Return "Ủy nhiệm chi"
            Case LoaiChungTu.GhiTangCCDC
                Return "Ghi tăng công cụ dụng cụ"
            Case LoaiChungTu.PhanBoCCDC
                Return "Phân bổ công cụ dụng cụ"
            Case LoaiChungTu.TongHop
                Return "Nghiệp vụ tổng hợp"
            Case LoaiChungTu.KetChuyenLaiLo
                Return "Kết chuyển lãi lỗ"
        End Select
        Return ""
    End Function


    Shared Function LaySoPhieu(loaiCT As ChungTu.LoaiChungTu) As Object
        Dim sql As String = ""
        sql &= " DECLARE @DoDai AS NVARCHAR(3) "
        sql &= " SET @DoDai = N'000'"
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoPhieu AS CHAR(7)"
        sql &= " DECLARE @Thang AS NVARCHAR(2)"
        sql &= " DECLARE @Nam AS NVARCHAR(2)"

        sql &= " SET @Thang = LEFT('00',2-LEN(CONVERT(NVARCHAR,DATEPART(mm,getdate())))) +  CONVERT(NVARCHAR,DATEPART(mm,getdate()))"
        sql &= " SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
        sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(LEFT(SoCT,7),3),0))),0)+1  "
        sql &= "            FROM CHUNGTU "
        sql &= "            WHERE"
        sql &= " 	        LEFT(SoCT,2)=@Nam AND "
        sql &= " 	        SUBSTRING(SoCT,3,2)=@Thang AND LEN(LEFT(SoCT,7))=7 AND LoaiCT =  " & loaiCT & " "
        sql &= "            )"

        sql &= " SET @SoPhieu = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
        sql &= " SELECT @SoPhieu"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)
    End Function


    Public Shared Function NamLamViec() As Integer
        Return Convert.ToInt32(ExecuteSQLDataTable(" SELECT FloatValue FROM CAIDATHETHONGTHUE WHERE Module = N'NamLamViec' ").Rows(0)(0))
    End Function



End Class




Public Class ObjectItemCmb
    Private _HienThi As String
    Public Property HienThi() As String
        Get
            Return _HienThi
        End Get
        Set(ByVal value As String)
            _HienThi = value
        End Set
    End Property
    Private _GiaTri As Object
    Public Property GiaTri() As Object
        Get
            Return _GiaTri
        End Get
        Set(ByVal value As Object)
            _GiaTri = value
        End Set
    End Property

    Public Sub New(__GiaTri As Object, __HienThi As String)
        _HienThi = __HienThi
        _GiaTri = __GiaTri
    End Sub

    Public Overrides Function ToString() As String
        Return _HienThi
    End Function

End Class

