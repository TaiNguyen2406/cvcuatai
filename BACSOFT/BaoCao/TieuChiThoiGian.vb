Public Class TieuChiThoiGian

    Private _HienThi As String
    Public Property HienThi() As String
        Get
            Return _HienThi
        End Get
        Set(ByVal value As String)
            _HienThi = value
        End Set
    End Property

    Private _Nam As Integer
    Public Property Nam() As Integer
        Get
            Return _Nam
        End Get
        Set(ByVal value As Integer)
            _Nam = value
        End Set
    End Property

    Private _TuNgay As DateTime
    Public Property TuNgay() As DateTime
        Get
            Return _TuNgay
        End Get
        Set(ByVal value As DateTime)
            _TuNgay = value
        End Set
    End Property

    Private _DenNgay As DateTime
    Public Property DenNgay() As DateTime
        Get
            Return _DenNgay
        End Get
        Set(ByVal value As DateTime)
            _DenNgay = value
        End Set
    End Property

    Private _HienThiBaoCao As String
    Public Property HienThiBaoCao() As String
        Get
            Return _HienThiBaoCao
        End Get
        Set(ByVal value As String)
            _HienThiBaoCao = value
        End Set
    End Property


    Public Sub New(__Nam As Integer, __HienThi As String)
        _Nam = __Nam
        _HienThi = __HienThi
        Dim tg As DateTime = GetServerTime()
        Select Case _HienThi
            Case "Tháng này"
                _TuNgay = New DateTime(__Nam, tg.Month, 1)
                _DenNgay = New DateTime(__Nam, tg.Month, DateTime.DaysInMonth(__Nam, tg.Month))
                _HienThiBaoCao = "Tháng " & tg.Month & " năm " & Nam
            Case "Tháng trước"
                Dim thang As Integer = tg.Month
                If thang > 1 Then thang = thang - 1
                _TuNgay = New DateTime(__Nam, thang, 1)
                _DenNgay = New DateTime(__Nam, thang, DateTime.DaysInMonth(__Nam, thang))
                _HienThiBaoCao = "Tháng " & thang & " năm " & Nam
            Case "Tuần này"
                Dim ngaydautuan = tg.AddDays((-1 * tg.DayOfWeek) + 1)
                _TuNgay = ngaydautuan
                Dim ngaycuoituan = ngaydautuan
                While ngaycuoituan.DayOfWeek <> DayOfWeek.Saturday Or ngaycuoituan.Day < DateTime.DaysInMonth(ngaydautuan.Year, ngaydautuan.Month)
                    ngaycuoituan = ngaycuoituan.AddDays(1)
                End While
                _DenNgay = ngaycuoituan
                HienThiBaoCao = "Từ ngày " & _TuNgay.ToString("dd/MM/yyyy") & " đến ngày " & _DenNgay.ToString("dd/MM/yyyy")
                'Case "Tuần trước"
            Case "Tháng 1"
                _TuNgay = New DateTime(__Nam, 1, 1)
                _DenNgay = New DateTime(__Nam, 1, DateTime.DaysInMonth(__Nam, 1))
                HienThiBaoCao = "Tháng 1 năm " & Nam
            Case "Tháng 2"
                _TuNgay = New DateTime(__Nam, 2, 1)
                _DenNgay = New DateTime(__Nam, 2, DateTime.DaysInMonth(__Nam, 2))
                HienThiBaoCao = "Tháng 2 năm " & Nam
            Case "Tháng 3"
                _TuNgay = New DateTime(__Nam, 3, 1)
                _DenNgay = New DateTime(__Nam, 3, DateTime.DaysInMonth(__Nam, 3))
                HienThiBaoCao = "Tháng 3 năm " & Nam
            Case "Tháng 4"
                _TuNgay = New DateTime(__Nam, 4, 1)
                _DenNgay = New DateTime(__Nam, 4, DateTime.DaysInMonth(__Nam, 4))
                HienThiBaoCao = "Tháng 4 năm " & Nam
            Case "Tháng 5"
                _TuNgay = New DateTime(__Nam, 5, 1)
                _DenNgay = New DateTime(__Nam, 5, DateTime.DaysInMonth(__Nam, 5))
                HienThiBaoCao = "Tháng 5 năm " & Nam
            Case "Tháng 6"
                _TuNgay = New DateTime(__Nam, 6, 1)
                _DenNgay = New DateTime(__Nam, 6, DateTime.DaysInMonth(__Nam, 6))
                HienThiBaoCao = "Tháng 6 năm " & Nam
            Case "Tháng 7"
                _TuNgay = New DateTime(__Nam, 7, 1)
                _DenNgay = New DateTime(__Nam, 7, DateTime.DaysInMonth(__Nam, 7))
                HienThiBaoCao = "Tháng 7 năm " & Nam
            Case "Tháng 8"
                _TuNgay = New DateTime(__Nam, 8, 1)
                _DenNgay = New DateTime(__Nam, 8, DateTime.DaysInMonth(__Nam, 8))
                HienThiBaoCao = "Tháng 8 năm " & Nam
            Case "Tháng 9"
                _TuNgay = New DateTime(__Nam, 9, 1)
                _DenNgay = New DateTime(__Nam, 9, DateTime.DaysInMonth(__Nam, 9))
                HienThiBaoCao = "Tháng 9 năm " & Nam
            Case "Tháng 10"
                _TuNgay = New DateTime(__Nam, 10, 1)
                _DenNgay = New DateTime(__Nam, 10, DateTime.DaysInMonth(__Nam, 10))
                HienThiBaoCao = "Tháng 10 năm " & Nam
            Case "Tháng 11"
                _TuNgay = New DateTime(__Nam, 11, 1)
                _DenNgay = New DateTime(__Nam, 11, DateTime.DaysInMonth(__Nam, 11))
                HienThiBaoCao = "Tháng 11 năm " & Nam
            Case "Tháng 12"
                _TuNgay = New DateTime(__Nam, 12, 1)
                _DenNgay = New DateTime(__Nam, 12, DateTime.DaysInMonth(__Nam, 12))
                HienThiBaoCao = "Tháng 12 năm " & Nam
            Case "Quý I"
                _TuNgay = New DateTime(__Nam, 1, 1)
                _DenNgay = New DateTime(__Nam, 3, DateTime.DaysInMonth(__Nam, 3))
                HienThiBaoCao = "Quý I năm " & Nam
            Case "Quý II"
                _TuNgay = New DateTime(__Nam, 4, 1)
                _DenNgay = New DateTime(__Nam, 6, DateTime.DaysInMonth(__Nam, 6))
                HienThiBaoCao = "Quý II năm " & Nam
            Case "Quý III"
                _TuNgay = New DateTime(__Nam, 7, 1)
                _DenNgay = New DateTime(__Nam, 9, DateTime.DaysInMonth(__Nam, 9))
                HienThiBaoCao = "Quý III năm " & Nam
            Case "Quý IV"
                _TuNgay = New DateTime(__Nam, 10, 1)
                _DenNgay = New DateTime(__Nam, 12, DateTime.DaysInMonth(__Nam, 12))
                HienThiBaoCao = "Quý IV năm " & Nam
            Case "Cả năm"
                _TuNgay = New DateTime(__Nam, 1, 1)
                _DenNgay = New DateTime(__Nam, 12, DateTime.DaysInMonth(__Nam, 12))
                HienThiBaoCao = "Năm " & Nam
            Case "Tùy chỉnh"
                _TuNgay = New DateTime(__Nam, tg.Month, tg.Day).AddDays(-30)
                _DenNgay = New DateTime(__Nam, tg.Month, tg.Day)
        End Select
    End Sub

    Public Overrides Function ToString() As String
        Return _HienThi
    End Function


End Class

Public Class TieuChiThoiGianCollection
    Private _DsTieuChi As List(Of TieuChiThoiGian)
    Public Property DsTieuChi() As List(Of TieuChiThoiGian)
        Get
            Return _DsTieuChi
        End Get
        Set(ByVal value As List(Of TieuChiThoiGian))
            _DsTieuChi = value
        End Set
    End Property
    Public Sub New(_Nam As Integer)
        _DsTieuChi = New List(Of TieuChiThoiGian)
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng này"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng trước"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tuần này"))
        '_DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tuần trước"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 1"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 2"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 3"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 4"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 5"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 6"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 7"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 8"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 9"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 10"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 11"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tháng 12"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Quý I"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Quý II"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Quý III"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Quý IV"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Cả năm"))
        _DsTieuChi.Add(New TieuChiThoiGian(_Nam, "Tùy chỉnh"))
    End Sub
End Class
