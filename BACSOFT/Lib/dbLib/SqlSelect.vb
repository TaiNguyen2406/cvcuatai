Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class SqlSelect
    Private Shared sql As String = ""

    Public Shared Function Select_ThongTinNhapHang(ByVal IDVatTu As Object, ByVal SoYeuCau As Object, ByVal IDYeuCau As Object, Optional ByVal MaKH As Object = -1) As DataSet
        If IsDBNull(MaKH) Or MaKH Is Nothing Then
            MaKH = -1
        End If
        sql = ""
        sql &= " SELECT ISNULL(( SELECT TOP 1 ISNULL(GiaCungUng,0) FROM YEUCAUDEN"
        sql &= " WHERE (Sophieu=@SoYeuCau AND IDVattu=@IDVattu) OR  (Sophieu=@SoYeuCau AND ID=@IDYeuCau)),0)"
        sql &= " "
        sql &= " SELECT TOP 5 NHAPKHO.Sophieu,IDVattu,Soluong,(Dongia * PHIEUNHAPKHO.Tygia)Dongia,NHAPKHO.Tiente,Mucthue,Nhapthue,PHIEUNHAPKHO.Ngaythang,KHACHHANG.ttcMa"
        sql &= " FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON NHAPKHO.Sophieu=PHIEUNHAPKHO.Sophieu"
        sql &= "         INNER JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " WHERE IDVattu=@IDVatTu"
        sql &= " ORDER BY PHIEUNHAPKHO.Ngaythang DESC"

        sql &= " SELECT * FROM ( SELECT TOP 5 BANGCHAOGIA.Ngaythang,BANGCHAOGIA.Tiente AS TienTeCG,CHAOGIA.TyGia, ISNULL(BANGCHAOGIA.TyGia,1) AS TyGiaCG,CHAOGIA.Tiente,CHAOGIA.Soluong,(0.0)GiaBanPT,CHAOGIA.Dongia,VATTU.Dongia1 AS GiaList,CHAOGIA.Mucthue,CHAOGIA.Xuatthue,CHAOGIA.Chietkhau as ChietKhau,(0.0)ChietKhauPT,KHACHHANG.ttcMa,BANGCHAOGIA.CongTrinh"
        sql &= " FROM CHAOGIA INNER JOIN BANGCHAOGIA ON CHAOGIA.Sophieu=BANGCHAOGIA.Sophieu"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachhang"
        sql &= " INNER JOIN VATTU ON VATTU.ID=CHAOGIA.IDVattu"
        sql &= " WHERE CHAOGIA.IDVattu=@IDVatTu AND BANGCHAOGIA.IDKhachhang= @KH "
        sql &= " ORDER BY BANGCHAOGIA.Ngaythang DESC)tb "
        sql &= " UNION ALL "
        sql &= " SELECT * FROM (SELECT TOP 5 BANGCHAOGIA.Ngaythang,BANGCHAOGIA.Tiente AS TienTeCG,CHAOGIA.TyGia,ISNULL(BANGCHAOGIA.TyGia,1) AS TyGiaCG,CHAOGIA.Tiente,CHAOGIA.Soluong,(0.0)GiaBanPT,CHAOGIA.Dongia,VATTU.Dongia1 AS GiaList,CHAOGIA.Mucthue,CHAOGIA.Xuatthue,CHAOGIA.Chietkhau as ChietKhau,(0.0)ChietKhauPT,KHACHHANG.ttcMa,BANGCHAOGIA.CongTrinh"
        sql &= " FROM CHAOGIA INNER JOIN BANGCHAOGIA ON CHAOGIA.Sophieu=BANGCHAOGIA.Sophieu"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachhang"
        sql &= " INNER JOIN VATTU ON VATTU.ID=CHAOGIA.IDVattu"
        sql &= " WHERE CHAOGIA.IDVattu=@IDVatTu AND BANGCHAOGIA.IDKhachhang<>@KH"
        sql &= " ORDER BY BANGCHAOGIA.Ngaythang DESC)tb2 "

        AddParameter("@IDVatTu", IDVatTu)
        AddParameter("@SoYeuCau", SoYeuCau)
        AddParameter("@IDYeuCau", IDYeuCau)
        AddParameter("@KH", MaKH)

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            With ds.Tables(2)
                For i As Integer = 0 To .Rows.Count - 1
                    Dim _GiaBanPT As Double = 0
                    If .Rows(i)("GiaList") > 0 Then
                        If Convert.ToInt32(.Rows(i)("Tiente")) > Convert.ToInt32(.Rows(0)("TienTeCG")) Then
                            _GiaBanPT = .Rows(i)("Dongia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / .Rows(i)("TyGiaCG"))
                        Else
                            _GiaBanPT = .Rows(i)("Dongia") / .Rows(i)("GiaList")
                        End If
                    End If

                    .Rows(i)("GiaBanPT") = Math.Round(_GiaBanPT * 100, 2)
                    If .Rows(i)("Dongia") <> 0 Then
                        .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("Dongia")) * 100, 2)
                    End If
                Next
            End With
        End If

        Return ds
    End Function

    Public Shared Function Select_ThongTinBanHang(ByVal IDVatTu As Object, Optional ByVal MaKH As Object = -1) As DataTable
        If IsDBNull(MaKH) Or MaKH Is Nothing Then
            MaKH = -1
        End If
        sql = ""
        sql &= " SELECT * FROM (SELECT TOP 5 XUATKHO.Sophieu,IDVattu,Soluong,(Dongia * PHIEUXUATKHO.Tygia)Dongia,PHIEUXUATKHO.Tiente,Mucthue,Xuatthue,PHIEUXUATKHO.Ngaythang,KHACHHANG.ttcMa,BANGCHAOGIA.CongTrinh"
        sql &= " FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu"
        sql &= "         INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=XUATKHO.SoPhieu "
        sql &= " WHERE IDVattu=@IDVatTu AND PHIEUXUATKHO.IDKhachhang=@KH "
        sql &= " ORDER BY PHIEUXUATKHO.Ngaythang DESC)tb"

        sql &= " UNION ALL "

        sql &= " SELECT * FROM ( SELECT TOP 5 XUATKHO.Sophieu,IDVattu,Soluong,(Dongia * PHIEUXUATKHO.Tygia)Dongia,PHIEUXUATKHO.Tiente,Mucthue,Xuatthue,PHIEUXUATKHO.Ngaythang,KHACHHANG.ttcMa,BANGCHAOGIA.CongTrinh"
        sql &= " FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu"
        sql &= "         INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=XUATKHO.SoPhieu "
        sql &= " WHERE IDVattu=@IDVatTu AND PHIEUXUATKHO.IDKhachhang<>@KH"
        sql &= " ORDER BY PHIEUXUATKHO.Ngaythang DESC)tb2 "
        AddParameter("@IDVatTu", IDVatTu)
        AddParameter("@KH", MaKH)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        Return dt
    End Function

    Public Shared Function Select_KetQuaBaoGia(ByVal TuNgay As DateTime, ByVal DenNgay As DateTime, Optional ByVal HienDayDu As Boolean = False, Optional ByVal LayDiemTong As Boolean = False) As DataTable
        '   ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " SELECT * FROM("
        sql &= " SELECT ROW_NUMBER() OVER (ORDER BY SoPhieu,IDVatTu,ID,NgayNhap DESC) STT,"
        sql &= "             ROW_NUMBER() OVER (PARTITION BY SoPhieu,ID,IDVatTu"
        sql &= "                                 ORDER BY SoPhieu,ID,NgayNhap DESC) STTNhap,Convert(float,0) as LN_GN,Convert(float,0) as TongGiaNhap,Convert(float,0) as LoiNhuanTmp,Convert(float,0) as Diem,"
        sql &= " * FROM"
        sql &= " ("
        sql &= " SELECT  VATTU.Model,TENHANGSANXUAT.Ten as HangSX,PHUTRACH.Ten As PhuTrach,MUAHANG.Ten As NguoiNhap, "
        sql &= " 	XK.ID,PX.NgayThang,XK.SoPhieu,PX.CongTrinh,PX.IDTakeCare,XK.IDVatTu,XK.SoLuong,Convert(float,0) As SoLuong2,"
        sql &= " 	(CASE PX.TienTe WHEN 0 THEN XK.DonGia ELSE XK.DonGia*PX.TyGia END) AS GiaBan,"
        sql &= " 	ISNULL((CASE PX.TienTe WHEN 0 THEN CHAOGIA.ChietKhau ELSE ISNULL(CHAOGIA.ChietKhau,0)*PX.TyGia END),0) AS TienChietKhau,(CASE WHEN PX.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE tbCK.KhauTru END) as HSThuCK,"
        sql &= " 	ISNULL((ISNULL((SELECT SUM(SoLuong) FROM NHAPKHO as tbNK "
        sql &= " 		INNER JOIN PHIEUNHAPKHO As tbPNK ON tbNK.SoPhieu = tbPNK.SoPhieu"
        sql &= " 	 WHERE tbNK.IDVatTu=XK.IDVatTu AND tbPNK.NgayThang <= PX.NgayThang),0)"
        sql &= " 	-"
        sql &= " 	ISNULL((SELECT SUM(SoLuong) FROM XUATKHO as tbXK "
        sql &= " 		INNER JOIN PHIEUXUATKHO As tbPXK ON tbXK.SoPhieu = tbPXK.SoPhieu"
        sql &= " 	 WHERE tbXK.IDVatTu=XK.IDVatTu AND tbPXK.NgayThang < PX.NgayThang),0)),0)SLTon,"
        sql &= " 	tbN.NgayThang as NgayNhap,tbN.IDNguoiDat,ISNULL(tbN.SoLuong,0) AS SLNhap,ISNULL(tbN.DonGia,0) as GiaNhap,"
        sql &= " 	((CASE PX.TienTe WHEN 0 THEN XK.DonGia ELSE XK.DonGia*PX.TyGia END)"
        sql &= " 	- ISNULL(tbN.DonGia,0)) As LoiNhuan,Convert(float,0)LoiNhuanMH, tbCK.TongChietKhau,tbCK.TienGoc,tbCK.LoiNhuanXK,tbCK.PTLN,tbCK.PTCK,ISNULL(tbCK.ChiPhi,0) AS ChiPhi,tbCK.TienTruocThue,"
        sql &= " 	ISNULL(tblQuyDoi.HSLNThuongMaiToMH,0) HSLNThuongMaiToMH, ISNULL(tblQuyDoi.HSLNCongTrinhToMH,0) HSLNCongTrinhToMH,ISNULL(tblQuyDoi.TyLeQuyDoiDiem,0) TyLeQuyDoiDiem,tbCK.KhauTru"
        sql &= " FROM XUATKHO As XK"
        sql &= " INNER JOIN PHIEUXUATKHO as PX ON PX.SoPhieu = XK.SoPhieu"
        sql &= " LEFT JOIN tblQuyDoi ON Right(CONVERT(nvarchar,PX.NgayThang,103),7) = tblQuyDoi.ThangNam"
        sql &= " LEFT JOIN"
        sql &= " ("
        sql &= " SELECT  PN.NgayThang,NK.SoPhieu,PN.IDNguoiDat,NK.IDVatTu,NK.SoLuong,"
        sql &= " 	(CASE PN.TienTe WHEN 0 THEN NK.DonGia + ISNULL(NK.ChiPhi,0) ELSE NK.DonGia*PN.TyGia + ISNULL(NK.ChiPhi,0)  END) AS DonGia"
        sql &= " FROM NHAPKHO As NK"
        sql &= " INNER JOIN PHIEUNHAPKHO as PN ON NK.SoPhieu = PN.SoPhieu"
        sql &= " WHERE Convert(datetime,Convert(nvarchar, PN.NgayThang,103),103)<=@DenNgay "
        sql &= " ) tbN ON XK.IDVatTu=tbN.IDVatTu  AND PX.NgayThang>=tbN.NgayThang"

        sql &= " LEFT JOIN (Select  PHIEUXUATKHO.SoPhieu ,ISNULL(BANGCHAOGIA.KhauTru,15)/100 as KhauTru,(PHIEUXUATKHO.TienTruocThue*PHIEUXUATKHO.TyGia)AS TienTruocThue,PHIEUXUATKHO.TienChietKhau* PHIEUXUATKHO.Tygia  as TongChietKhau,ISNULL(V_XuatkhoGianhap.tongnhap, 0) AS TienGoc,"
        sql &= "  (ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0)) AS ChiPhi, "
        sql &= "            (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= "                 - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) /  (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) AS LoiNhuanXK,"
        sql &= "     (CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 	WHEN 0 THEN 0 "
        sql &= " 	ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) "
        sql &= " 	- ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL((CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END), 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN, "
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tienchietkhau*PHIEUXUATKHO.TyGia)/(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END) AS PTCK"
        sql &= " FROM PHIEUXUATKHO  "
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
        sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu"
        sql &= " WHERE Convert(datetime,Convert(nvarchar, PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay  AND @DenNgay   "
        sql &= " )tbCK ON tbCK.SoPhieu=XK.SoPhieu "

        sql &= " LEFT JOIN CHAOGIA ON CHAOGIA.ID = XK.IDChaoGia"
        sql &= " INNER JOIN NHANSU AS PHUTRACH ON PHUTRACH.ID=PX.IDTakeCare"
        sql &= " LEFT JOIN NHANSU AS MUAHANG ON MUAHANG.ID=tbN.IDNguoiDat"
        sql &= " INNER JOIN VATTU ON VATTU.ID=XK.IDVatTu"
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " WHERE Convert(datetime,Convert(nvarchar, PX.NgayThang,103),103)  BETWEEN @TuNgay  AND @DenNgay   "
        sql &= " )tbl)tbl2 WHERE STTNhap <=5"
        sql &= " ORDER By SoPhieu,IDVatTu,ID,NgayNhap DESC"
        AddParameterWhere("@TuNgay", TuNgay)
        AddParameterWhere("@DenNgay", DenNgay)

        Dim tbTmp As DataTable = ExecuteSQLDataTable(sql)
        If Not tbTmp Is Nothing Then
            CloseWaiting()
            Dim tb As DataTable = tbTmp.Clone
            Dim _SP As Object
            Dim _IDVatTu As Object
            Dim _IDXK As Object

            Dim _SoLuongT As Double = 0
            Dim _SLTonT As Double = 0
            Dim _TLQDDiem As Double = 0

            If tbTmp.Rows.Count > 0 Then
                _SP = tbTmp.Rows(0)("SoPhieu")
                _IDVatTu = tbTmp.Rows(0)("IDVatTu")
                _IDXK = tbTmp.Rows(0)("ID")
                If tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") >= tbTmp.Rows(0)("SLNhap") Then
                    tbTmp.Rows(0)("SoLuong2") = 0
                    _SoLuongT = 0
                    _SLTonT = tbTmp.Rows(0)("SLNhap")
                ElseIf tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") > 0 And tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") < tbTmp.Rows(0)("SLNhap") Then
                    If tbTmp.Rows(0)("SoLuong") < tbTmp.Rows(0)("SLNhap") And tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") > tbTmp.Rows(0)("SLNhap") - tbTmp.Rows(0)("SoLuong") Then
                        tbTmp.Rows(0)("SoLuong2") = tbTmp.Rows(0)("SLNhap") - (tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong"))
                        _SoLuongT = tbTmp.Rows(0)("SoLuong2")

                    Else
                        tbTmp.Rows(0)("SoLuong2") = tbTmp.Rows(0)("SoLuong")
                        _SoLuongT = tbTmp.Rows(0)("SoLuong")

                    End If
                ElseIf tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") = 0 And tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") < tbTmp.Rows(0)("SLNhap") Then
                    If tbTmp.Rows(0)("SoLuong") < tbTmp.Rows(0)("SLNhap") And tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong") > tbTmp.Rows(0)("SLNhap") - tbTmp.Rows(0)("SoLuong") Then
                        tbTmp.Rows(0)("SoLuong2") = tbTmp.Rows(0)("SLNhap") - (tbTmp.Rows(0)("SLTon") - tbTmp.Rows(0)("SoLuong"))
                        _SoLuongT = tbTmp.Rows(0)("SoLuong2")

                    ElseIf tbTmp.Rows(0)("SoLuong") > tbTmp.Rows(0)("SLNhap") Then
                        tbTmp.Rows(0)("SoLuong2") = tbTmp.Rows(0)("SoLuong") - tbTmp.Rows(0)("SLNhap")
                        _SoLuongT = tbTmp.Rows(0)("SoLuong2")
                    Else
                        tbTmp.Rows(0)("SoLuong2") = tbTmp.Rows(0)("SoLuong")
                        _SoLuongT = tbTmp.Rows(0)("SoLuong")

                    End If
                Else
                    tbTmp.Rows(0)("SoLuong2") = tbTmp.Rows(0)("SoLuong")
                    _SoLuongT = tbTmp.Rows(0)("SoLuong")

                End If
                If tbTmp.Rows(0)("CongTrinh") Then
                    tbTmp.Rows(0)("LoiNhuanMH") = (tbTmp.Rows(0)("GiaNhap") / tbTmp.Rows(0)("TienGoc")) * tbTmp.Rows(0)("LoiNhuanXK") * tbTmp.Rows(0)("SoLuong2")
                    tbTmp.Rows(0)("LoiNhuanTmp") = tbTmp.Rows(0)("LoiNhuanMH") * tbTmp.Rows(0)("HSLNCongTrinhToMH")
                    tbTmp.Rows(0)("Diem") = tbTmp.Rows(0)("LoiNhuanMH") * tbTmp.Rows(0)("HSLNCongTrinhToMH") * tbTmp.Rows(0)("TyLeQuyDoiDiem")
                Else
                    tbTmp.Rows(0)("LoiNhuanMH") = (tbTmp.Rows(0)("LoiNhuan") - (tbTmp.Rows(0)("TienChietKhau") - tbTmp.Rows(0)("TienChietKhau") * tbTmp.Rows(0)("KhauTru")) / (1 - tbTmp.Rows(0)("HSThuCK")) - (tbTmp.Rows(0)("GiaBan") / tbTmp.Rows(0)("TienTruocThue")) * tbTmp.Rows(0)("ChiPhi")) * tbTmp.Rows(0)("SoLuong2")
                    tbTmp.Rows(0)("LoiNhuanTmp") = tbTmp.Rows(0)("LoiNhuanMH") * tbTmp.Rows(0)("HSLNThuongMaiToMH")
                    tbTmp.Rows(0)("Diem") = tbTmp.Rows(0)("LoiNhuanMH") * tbTmp.Rows(0)("HSLNThuongMaiToMH") * tbTmp.Rows(0)("TyLeQuyDoiDiem")
                End If
                tbTmp.Rows(0)("TongGiaNhap") = tbTmp.Rows(0)("SoLuong2") * tbTmp.Rows(0)("GiaNhap")
                If tbTmp.Rows(0)("TongGiaNhap") <> 0 Then
                    tbTmp.Rows(0)("LN_GN") = tbTmp.Rows(0)("LoiNhuanMH") / tbTmp.Rows(0)("TongGiaNhap")
                End If
                _TLQDDiem = tbTmp.Rows(0)("TyLeQuyDoiDiem")
                Dim i As Integer = 1
                If _SoLuongT = 0 Then
                    If Not HienDayDu Then
                        tbTmp.Rows.RemoveAt(0)
                        i = 0
                    End If
                End If

                While i < tbTmp.Rows.Count
                    If _SP = tbTmp.Rows(i)("SoPhieu") And _IDVatTu = tbTmp.Rows(i)("IDVatTu") And _IDXK = tbTmp.Rows(i)("ID") Then
                        If _SoLuongT = tbTmp.Rows(i)("SoLuong") Then
                            If Not HienDayDu Then
                                tbTmp.Rows.RemoveAt(i)
                                Continue While
                            End If


                        ElseIf _SoLuongT > 0 And _SoLuongT < tbTmp.Rows(i)("SoLuong") Then
                            If tbTmp.Rows(i)("SoLuong") - _SoLuongT > tbTmp.Rows(i)("SLNhap") Then
                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SLNhap")
                                _SoLuongT += tbTmp.Rows(i)("SLNhap")
                            Else
                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SoLuong") - _SoLuongT
                                _SoLuongT += tbTmp.Rows(i)("SoLuong2")
                            End If
                        ElseIf _SoLuongT = 0 Then
                            If tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") - _SLTonT > tbTmp.Rows(i)("SLNhap") Then
                                _SLTonT += tbTmp.Rows(i)("SLNhap")
                                If Not HienDayDu Then
                                    tbTmp.Rows.RemoveAt(i)
                                    Continue While
                                End If
                            Else
                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SoLuong")
                                _SLTonT = 0
                                _SoLuongT = tbTmp.Rows(i)("SoLuong")
                            End If
                        End If
                    Else
                        _SP = tbTmp.Rows(i)("SoPhieu")
                        _IDVatTu = tbTmp.Rows(i)("IDVatTu")
                        _IDXK = tbTmp.Rows(i)("ID")
                        _SoLuongT = 0
                        _SLTonT = 0
                        If tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") >= tbTmp.Rows(i)("SLNhap") Then
                            tbTmp.Rows(i)("SoLuong2") = 0
                            _SoLuongT = 0
                            _SLTonT = tbTmp.Rows(i)("SLNhap")
                            If Not HienDayDu Then
                                tbTmp.Rows.RemoveAt(i)
                                Continue While
                            End If
                        ElseIf tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") > 0 And tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") < tbTmp.Rows(i)("SLNhap") Then
                            If tbTmp.Rows(i)("SoLuong") < tbTmp.Rows(i)("SLNhap") And tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") > tbTmp.Rows(i)("SLNhap") - tbTmp.Rows(i)("SoLuong") Then

                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SLNhap") - (tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong"))
                                _SoLuongT = tbTmp.Rows(i)("SoLuong2")
                            Else
                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SoLuong")
                                _SoLuongT = tbTmp.Rows(i)("SoLuong")
                            End If

                        ElseIf tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") = 0 And tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") < tbTmp.Rows(i)("SLNhap") Then
                            If tbTmp.Rows(i)("SoLuong") < tbTmp.Rows(i)("SLNhap") And tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong") > tbTmp.Rows(i)("SLNhap") - tbTmp.Rows(i)("SoLuong") Then

                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SLNhap") - (tbTmp.Rows(i)("SLTon") - tbTmp.Rows(i)("SoLuong"))
                                _SoLuongT = tbTmp.Rows(i)("SoLuong2")
                            ElseIf tbTmp.Rows(i)("SoLuong") > tbTmp.Rows(i)("SLNhap") Then

                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SoLuong") - tbTmp.Rows(i)("SLNhap")
                                _SoLuongT = tbTmp.Rows(i)("SoLuong2")
                            Else
                                tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SoLuong")
                                _SoLuongT = tbTmp.Rows(i)("SoLuong")
                            End If
                        Else
                            tbTmp.Rows(i)("SoLuong2") = tbTmp.Rows(i)("SoLuong")
                            _SoLuongT = tbTmp.Rows(i)("SoLuong")
                        End If
                    End If

                    '  tbTmp.Rows(i)("LoiNhuan") = tbTmp.Rows(i)("LoiNhuan") * tbTmp.Rows(i)("SoLuong2")
                    If tbTmp.Rows(i)("CongTrinh") Then
                        tbTmp.Rows(i)("LoiNhuanMH") = (tbTmp.Rows(i)("GiaNhap") / tbTmp.Rows(i)("TienGoc")) * tbTmp.Rows(i)("LoiNhuanXK") * tbTmp.Rows(i)("SoLuong2")
                        tbTmp.Rows(i)("LoiNhuanTmp") = tbTmp.Rows(i)("LoiNhuanMH") * tbTmp.Rows(i)("HSLNCongTrinhToMH")
                        tbTmp.Rows(i)("Diem") = tbTmp.Rows(i)("LoiNhuanMH") * tbTmp.Rows(i)("HSLNCongTrinhToMH") * tbTmp.Rows(i)("TyLeQuyDoiDiem")
                    Else
                        tbTmp.Rows(i)("LoiNhuanMH") = (tbTmp.Rows(i)("LoiNhuan") - (tbTmp.Rows(i)("TienChietKhau") - tbTmp.Rows(i)("TienChietKhau") * tbTmp.Rows(i)("KhauTru")) / (1 - tbTmp.Rows(i)("HSThuCK")) - (tbTmp.Rows(i)("GiaBan") / tbTmp.Rows(i)("TienTruocThue")) * tbTmp.Rows(i)("ChiPhi")) * tbTmp.Rows(i)("SoLuong2")
                        tbTmp.Rows(i)("LoiNhuanTmp") = tbTmp.Rows(i)("LoiNhuanMH") * tbTmp.Rows(i)("HSLNThuongMaiToMH")
                        tbTmp.Rows(i)("Diem") = tbTmp.Rows(i)("LoiNhuanMH") * tbTmp.Rows(i)("HSLNThuongMaiToMH") * tbTmp.Rows(i)("TyLeQuyDoiDiem")
                    End If
                    tbTmp.Rows(i)("TongGiaNhap") = tbTmp.Rows(i)("SoLuong2") * tbTmp.Rows(i)("GiaNhap")
                    If tbTmp.Rows(i)("TongGiaNhap") <> 0 Then
                        tbTmp.Rows(i)("LN_GN") = tbTmp.Rows(i)("LoiNhuanMH") / tbTmp.Rows(i)("TongGiaNhap")
                    End If
                    tbTmp.Rows(i)("STT") = i + 1
                    i += 1

                End While

            End If

            If LayDiemTong Then
                'Dim q = From p As DataRow In tbTmp.Rows
                'Group By IDCungUng = p("IDNguoiDat")
                'Into LoiNhuan = Sum(CType(p("LoiNhuanMH"), Double)),
                'Diem = Sum(CType(p("LoiNhuanMH"), Double))

                'Select IDCungUng, LoiNhuan,Diem

                Dim query = From row As DataRow In tbTmp.Rows
                Group row By IDCungUng = row("IDNguoiDat") Into IDCungUngGroup = Group
                Select New With {
                    Key IDCungUng,
                    .LoiNhuan = IDCungUngGroup.Sum(Function(r) r("LoiNhuanTmp")),
                    .TyLeQuyDoiDiem = _TLQDDiem
               }

                ' Dim query1 = From row As DataRow In tbTmp.Rows
                ' Group row By IDCungUng = row("IDNguoiDat") Into IDCUGroup = Group
                ' Select New With {
                '     Key IDCungUng,
                '     .LoiNhuan = IDCungUng.Sum(Function(r) r.Field(Of Double)("LoiNhuanTmp")),
                '     .TyLeQuyDoiDiem = _TLQDDiem
                '}



                Dim tb1 As New DataTable
                tb1.Columns.Add("IDCungUng", Type.GetType("System.Int32"))
                tb1.Columns.Add("LoiNhuan", Type.GetType("System.Double"))
                tb1.Columns.Add("TyLeQuyDoiDiem", Type.GetType("System.Double"))
                For Each x In query
                    Dim r As DataRow = tb1.NewRow()
                    r("IDCungUng") = x.IDCungUng
                    r("LoiNhuan") = x.LoiNhuan
                    r("TyLeQuyDoiDiem") = x.TyLeQuyDoiDiem
                    tb1.Rows.Add(r)
                Next
                '  CloseWaiting()
                Return tb1
            Else
                '  CloseWaiting()
                Return tbTmp
            End If



        Else
            ' CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
            Return Nothing
        End If
    End Function

    Public Shared Function Select_BoVatTu(ByVal IDVatTu As Object) As DataTable
        Dim sql As String = ""
        sql &= " Select tblGhepVatTu.IDVatTu as IDBo, TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,"
        sql &= " TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= " TENNUOC.Ten AS Xuatxu,convert(float,0) AS SLYC, VATTU.ThongDung,VATTU.HangTon,"
        sql &= " (Round((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=tblGhepVatTu.IDVatTu),4)-Round((select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=tblGhepVatTu.IDVatTu),4)) AS slTon, "
        sql &= " tblGhepVatTu.SoLuong as SLTrongBo "
        sql &= " FROM tblGhepVatTu "
        sql &= " INNER JOIN VATTU ON tblGhepVatTu.IDVatTu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
        sql &= " WHERE tblGhepVatTu.IDVatTu = " & IDVatTu
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                Return tb
            Else
                Return Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
            Return Nothing
        End If

    End Function
End Class
