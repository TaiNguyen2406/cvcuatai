
Namespace HoaDonGTGT

    Public Class CacheData
        Public Shared dataVatTu As DataView = Nothing

    End Class

#Region " -- Class TienTe -- "

    Public Class LoaiTienTe
        Private _ID As Integer
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Private _Ten As String
        Public Property Ten() As String
            Get
                Return _Ten
            End Get
            Set(ByVal value As String)
                _Ten = value
            End Set
        End Property

        Private _TyGia As Double
        Public Property TyGia() As Double
            Get
                Return _TyGia
            End Get
            Set(ByVal value As Double)
                _TyGia = value
            End Set
        End Property

        Public Sub New(__Id As Integer, __Ten As String, __TyGia As Double)
            _ID = __Id
            _Ten = __Ten
            _TyGia = __TyGia
        End Sub

        Public Overrides Function ToString() As String
            Return _Ten
        End Function



    End Class
#End Region

#Region "-- Class TrangThaiHoaDon --"
    Public Class TrangThaiHoaDon

        Private _TenTrangThai As String
        Public Property TenTrangThai() As String
            Get
                Return _TenTrangThai
            End Get
            Set(ByVal value As String)
                _TenTrangThai = value
            End Set
        End Property

        Private _GiaTri As Int16
        Public Property GiaTri() As Int16
            Get
                Return _GiaTri
            End Get
            Set(ByVal value As Int16)
                _GiaTri = value
            End Set
        End Property

        Private _DanhSach As List(Of TrangThaiHoaDon)
        Public Property DanhSach() As List(Of TrangThaiHoaDon)
            Get
                Return _DanhSach
            End Get
            Set(ByVal value As List(Of TrangThaiHoaDon))
                _DanhSach = value
            End Set
        End Property
        Public Sub New()

        End Sub
        Public Sub New(__TenTrangThai As String, __GiaTri As Int16)
            _TenTrangThai = __TenTrangThai
            _GiaTri = __GiaTri
        End Sub

        Enum TrangThai
            HoaDonNhap = 1
            HoaDonTreo = 2
            HoaDonDaIn = 3
            HoaDonHuy = 4
        End Enum

        Public Sub KhoiTao()
            _DanhSach = New List(Of TrangThaiHoaDon)
            _DanhSach.Add(New TrangThaiHoaDon("Hoá đơn nháp", TrangThai.HoaDonNhap))
            _DanhSach.Add(New TrangThaiHoaDon("Hoá đơn treo", TrangThai.HoaDonTreo))
            _DanhSach.Add(New TrangThaiHoaDon("Hoá đơn đã in", TrangThai.HoaDonDaIn))
            _DanhSach.Add(New TrangThaiHoaDon("Hoá đơn hủy", TrangThai.HoaDonHuy))
        End Sub

        Public Overrides Function ToString() As String
            Return _TenTrangThai
        End Function

    End Class
#End Region

#Region "-- Class HinhThucThanhToan --"
    Public Class HinhThucThanhToan

        Private _TenHinhThuc As String
        Public Property TenHinhThuc() As String
            Get
                Return _TenHinhThuc
            End Get
            Set(ByVal value As String)
                _TenHinhThuc = value
            End Set
        End Property

        Private _GiaTri As Int16
        Public Property GiaTri() As Int16
            Get
                Return _GiaTri
            End Get
            Set(ByVal value As Int16)
                _GiaTri = value
            End Set
        End Property

        Private _DanhSach As List(Of HinhThucThanhToan)
        Public Property DanhSach() As List(Of HinhThucThanhToan)
            Get
                Return _DanhSach
            End Get
            Set(ByVal value As List(Of HinhThucThanhToan))
                _DanhSach = value
            End Set
        End Property
        Public Sub New()

        End Sub
        Public Sub New(__TenHinhThuc As String, __GiaTri As Int16)
            _TenHinhThuc = __TenHinhThuc
            _GiaTri = __GiaTri
        End Sub

        Enum TrangThai
            TienMat = 1
            ChuyenKhoan = 2
            TMCK = 3
        End Enum

        Public Sub KhoiTao()
            _DanhSach = New List(Of HinhThucThanhToan)
            _DanhSach.Add(New HinhThucThanhToan("Tiền mặt", TrangThai.TienMat))
            _DanhSach.Add(New HinhThucThanhToan("Chuyển khoản", TrangThai.ChuyenKhoan))
            _DanhSach.Add(New HinhThucThanhToan("TM/CK", TrangThai.TMCK))
        End Sub

        Public Overrides Function ToString() As String
            Return _TenHinhThuc
        End Function

    End Class

#End Region


End Namespace


