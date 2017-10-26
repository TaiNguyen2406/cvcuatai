Imports BACSOFT.Db.SqlHelper

Public Class frmLuongThuong
    Public SaiMatKhau As Boolean = True

    Private Sub frmLuongThuong_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbThang.EditValue = Now.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadDSPhong()
        LoadDSNhanVien()
        chkSoSanh.PerformClick()

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            chkDuyet.Enabled = True
            btXacNhan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btUpdateDoanhSo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
            'rcbNhanVien.Buttons.Item(1).Visible = False
            chkSoSanh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btKetXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btXemChiTiet.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btCNBangTinhThuong.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            colGhiChu.OptionsColumn.ReadOnly = True
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbNhanVien.Enabled = False

        End If

        'LoadDS()
    End Sub

    Public Sub LoadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNhanVien()
        On Error Resume Next
        Dim sql As String = ""
        If TaiKhoan = 602 Or TaiKhoan = 971 Or TaiKhoan = 1291 Or TaiKhoan = 1340 Then
            sql &= "DECLARE @Depatment AS int "
            sql &= "SET @Depatment = (SELECT IDDepatment FROM LUONG WHERE [Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND IDNhanVien=" & TaiKhoan & ") "
            sql &= "SELECT IDNhanVien AS ID,NHANSU.Ten FROM LUONG INNER JOIN NHANSU ON NHANSU.ID=LUONG.IDNhanVien WHERE Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND LUONG.IDDepatment=@Depatment "
        Else
            sql = "SELECT IDNhanVien AS ID,NHANSU.Ten FROM LUONG INNER JOIN NHANSU ON NHANSU.ID=LUONG.IDNhanVien WHERE Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
            cbNhanVien.EditValue = TaiKhoan

        End If
    End Sub

    Friend Sub XemLuongChuaDuyet()
        Dim tbD As DataTable = ExecuteSQLDataTable("SELECT IDDepatment FROM LUONG WHERE [Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND IDNhanVien=" & TaiKhoan)
        If tbD Is Nothing Then
            ShowCanhBao("Không lấy được thông tin phòng ban !")
            Exit Sub
        End If
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " DECLARE @DoanhSoNhomMua AS float"
        sql &= " DECLARE @TongDiem3 AS Float"
        sql &= " DECLARE @Thang AS int"
        sql &= " DECLARE @Nam AS int"
        sql &= " DECLARE @ThuongThangPKT AS float"
        sql &= " DECLARE @ThuongThangPKTDien AS float"
        sql &= " DECLARE @ThuongThangPKTCo AS float"
        sql &= " DECLARE @HSKyNang AS Float"
        sql &= " DECLARE @ThuongThangPKTDVDien AS Float"
        sql &= " DECLARE @ThuongThangPKTDVCo AS Float"
        sql &= " DECLARE @SoKS AS int"
        sql &= " DECLARE @SoKT AS int"
        sql &= " DECLARE @Diem1QuyNhomMua AS float"
        sql &= " DECLARE @ThangTruoc AS nvarchar(10)"

        sql &= " SET @Thang=" & cbThang.EditValue
        sql &= " SET @Nam=" & tbNam.EditValue.ToString
        sql &= " SET @ThangTruoc='" & DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, cbThang.EditValue, 1)).ToString("MM/yyyy") & "'"
        sql &= " SET @ThuongThangPKT=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=2) "
        sql &= " SET @ThuongThangPKTDVDien=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=6 AND MaThuong=600)"
        sql &= " SET @ThuongThangPKTDVCo=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=6 AND MaThuong=602)"
        sql &= " SET @ThuongThangPKTDien=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=2 AND MaThuong=202) "
        sql &= " SET @ThuongThangPKTCo=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=2 AND MaThuong=203) "
        'sql &= " SET @TongDiemNangLucPKTDV=1"

        '========Tính doanh số nhóm mua theo quý

        sql &= " SET @DoanhSoNhomMua = ISNULL((SELECT DSNhomMua FROM tblDuyetLuong WHERE Month='" & cbThang.EditValue & "/" & tbNam.EditValue & "'),0)"

        '========Tổng điểm nhoms mua

        sql &= " SET @Diem1QuyNhomMua = (SELECT SUM(Diem1)Diem "
        sql &= " FROM(SELECT SUM(tblTHChamCong.Diem1)Diem1,IDNhanVien"
        sql &= " 	FROM tblTHChamCong"
        sql &= " 	WHERE Right(tblTHChamCong.[month],4) = @Nam  AND Left(tblTHChamCong.[month],2) IN (" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "," & (Convert.ToInt32(cbThang.EditValue) - 1).ToString & "," & (Convert.ToInt32(cbThang.EditValue).ToString) & ")    "
        sql &= " 	GROUP BY IDNhanVien)tb"
        sql &= " WHERE tb.IDNhanVien IN (SELECT DISTINCT IDNhanVien FROM LUONG WHERE Right(LUONG.[month],4) = @Nam  AND Left(LUONG.[month],2) IN (" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "," & (Convert.ToInt32(cbThang.EditValue) - 1).ToString & "," & (Convert.ToInt32(cbThang.EditValue).ToString) & ") AND (LUONG.MaThuong=300 OR LUONG.MaThuong=301)))"

        '========Tổng điểm tính theo quý của nhân viên

        sql &= " SELECT SUM(ISNULL(tblTHChamCong.Diem,0))Diem,SUM(ISNULL(tblTHChamCong.Diem1,0))Diem1,tblTHChamCong.IDNhanVien INTO #DiemNVQuy FROM tblTHChamCong INNER JOIN LUONG ON LUONG.[Month]=tblTHChamCong.[Month] AND LUONG.IDNhanVien=tblTHChamCong.IDNhanVien "
        sql &= "     WHERE Right(tblTHChamCong.[month],4) = @Nam"
        If Convert.ToInt32(cbThang.EditValue) = 3 Or Convert.ToInt32(cbThang.EditValue) = 6 Or Convert.ToInt32(cbThang.EditValue) = 9 Or Convert.ToInt32(cbThang.EditValue) = 12 Then
            sql &= " AND Left(tblTHChamCong.[month],2) IN (" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "," & (Convert.ToInt32(cbThang.EditValue) - 1).ToString & "," & (Convert.ToInt32(cbThang.EditValue).ToString) & ")"
        End If
        sql &= " GROUP BY tblTHChamCong.IDNhanVien"


        '========== Tính số kỹ thuật và số kỹ sư
        sql &= " SET @SoKS= ( SELECT COUNT(ID) FROM LUONG WHERE ChucVu=0 AND Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam)"
        sql &= " SET @SoKT= ( SELECT COUNT(ID) FROM LUONG WHERE ChucVu=1 AND Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam)"

        '========== HS kỹ năng của phòng"

        sql &= " SET @HSKyNang= (SELECT SUM(Diem)/Sum(DiemChuan)Diem"
        sql &= " FROM (SELECT tmpTb.IDNhanVien,tb2.Diem,NLDanhSach.Diem AS DiemChuan FROM ("
        sql &= " select"
        sql &= "     tb.IDNhanVien,tb.IDKyNang,"
        sql &= "     max(tb.NgayThi) as Ngay"
        sql &= " from"
        sql &= "     tblDiemThiKyNang tb"
        sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
        sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
        sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=tmpTb.IDNhanVien AND LUONG.[Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND LUONG.IDDepatment=6"
        sql &= " INNER JOIN NLDanhSach ON NLDanhSach.ID=tb2.IDKyNang"
        sql &= " )tb1)"

        '=========== Tạo bảng tạm điểm kỹ năng theo thời điểm của nhân viên"

        sql &= " SELECT IDNhanVien,SUM(Diem)Diem"
        sql &= " INTO #KNNhanVien FROM (SELECT tmpTb.IDNhanVien,tb2.Diem FROM ("
        sql &= " select"
        sql &= "     tb.IDNhanVien,tb.IDKyNang,"
        sql &= "     max(tb.NgayThi) as Ngay"
        sql &= " from"
        sql &= "     tblDiemThiKyNang tb"
        sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
        sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
        sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi)tb1"
        sql &= " Group By IDNhanVien"

        '=========== Tạo bảng tạm tính điểm kỹ năng của phòng
        sql &= " SELECT IDDepatment,SUM(Diem)Diem INTO #KNPhong FROM ("
        sql &= " SELECT #KNNhanVien.IDNhanVien,#KNNhanVien.Diem,LUONG.IDDepatment FROM #KNNhanVien INNER JOIN LUONG ON LUONG.IDNhanVien=#KNNhanVien.IDNhanVien AND Left(Luong.[month],2) = " & cbThang.EditValue & " AND Right(Luong.[month],4) = " & tbNam.EditValue.ToString
        sql &= " AND LUONG.LuongThuong <> 0)tb group by IDDepatment "

        '=========== Tạo bảng tạm tính điểm kỹ năng của phòng KT nhóm điện
        sql &= " SELECT IDDepatment,SUM(Diem)Diem INTO #KNPhongKTDien FROM ("
        sql &= " SELECT #KNNhanVien.IDNhanVien,#KNNhanVien.Diem,LUONG.IDDepatment FROM #KNNhanVien INNER JOIN LUONG ON LUONG.IDNhanVien=#KNNhanVien.IDNhanVien AND Left(Luong.[month],2) = " & cbThang.EditValue & " AND Right(Luong.[month],4) = " & tbNam.EditValue.ToString
        sql &= " AND LUONG.LuongThuong <> 0 AND LUONG.MaThuong=202)tb group by IDDepatment "

        '=========== Tạo bảng tạm tính điểm kỹ năng của phòng KT nhóm cơ
        sql &= " SELECT IDDepatment,SUM(Diem)Diem INTO #KNPhongKTCo FROM ("
        sql &= " SELECT #KNNhanVien.IDNhanVien,#KNNhanVien.Diem,LUONG.IDDepatment FROM #KNNhanVien INNER JOIN LUONG ON LUONG.IDNhanVien=#KNNhanVien.IDNhanVien AND Left(Luong.[month],2) = " & cbThang.EditValue & " AND Right(Luong.[month],4) = " & tbNam.EditValue.ToString
        sql &= " AND LUONG.LuongThuong <> 0 AND LUONG.MaThuong=203)tb group by IDDepatment "

        '============ Điểm văn hóa nhân viên trong tháng"
        sql &= " select Luong.IDNhanvien, (200+ sum(isnull(VHNhanvien.Diem,0))) as Diem into #VHNhanVien from Luong "
        sql &= " left join VHNhanvien on Luong.IDNhanvien= VHNhanvien.IDNhanvien and month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
        sql &= " where convert(int,left([Month],2)) = @Thang and convert(int,right([Month],4)) = @Nam group by Luong.IDNhanvien"

        '============ Tổng điểm văn hóa của phòng theo từng tháng"
        sql &= " SELECT SUM(Diem)Diem,IDDepatment INTO #VHPhong FROM ("
        sql &= " SELECT #VHNhanVien.Diem,LUONG.IDDepatment FROM #VHNhanVien"
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=#VHNhanVien.IDNhanVien AND convert(int,left([Month],2)) = @Thang and convert(int,right([Month],4)) = @Nam "
        sql &= " AND LUONG.LuongThuong <> 0 )tb GROUP BY IDDepatment"

        '============ Tổng điểm văn hóa của phòng kt nhóm điện theo từng tháng"
        sql &= " SELECT SUM(Diem)Diem,IDDepatment INTO #VHPhongKTDien FROM ("
        sql &= " SELECT #VHNhanVien.Diem,LUONG.IDDepatment FROM #VHNhanVien"
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=#VHNhanVien.IDNhanVien AND convert(int,left([Month],2)) = @Thang and convert(int,right([Month],4)) = @Nam "
        sql &= " AND LUONG.LuongThuong <> 0 AND LUONG.MaThuong=202 )tb GROUP BY IDDepatment"

        '============ Tổng điểm văn hóa của phòng kt nhóm cơ theo từng tháng"
        sql &= " SELECT SUM(Diem)Diem,IDDepatment INTO #VHPhongKTCo FROM ("
        sql &= " SELECT #VHNhanVien.Diem,LUONG.IDDepatment FROM #VHNhanVien"
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=#VHNhanVien.IDNhanVien AND convert(int,left([Month],2)) = @Thang and convert(int,right([Month],4)) = @Nam "
        sql &= " AND LUONG.LuongThuong <> 0 AND LUONG.MaThuong=203 )tb GROUP BY IDDepatment"

        '=========== Tạo bảng tạm tính điểm văn hóa, kỹ năng, NLDV của phòng --- KTDV
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,SUM(DiemNLDV)DiemNLDV,Count(IDNhanVien)SoNguoi INTO #DiemPhongKTDV FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"
        '=========== Tạo bảng tạm tính điểm văn hóa, kỹ năng của phòng kỹ thuật dịch vụ (nhóm điện) trừ NV thử việc trừ TP 
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,SUM(DiemNLDV)DiemNLDV,Count(IDNhanVien)SoNguoi INTO #DiemNVPhongKTDVDien FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien AND LUONG.LUONGTHUONG <> 0 AND LUONG.IDDepatment=6 AND LUONG.MaThuong=600 "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '=========== Tạo bảng tạm tính điểm văn hóa, kỹ năng của phòng kỹ thuật dịch vụ (nhóm cơ) trừ NV thử việc trừ TP
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,SUM(DiemNLDV)DiemNLDV,Count(IDNhanVien)SoNguoi INTO #DiemNVPhongKTDVCo FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien AND LUONG.LUONGTHUONG <> 0 AND LUONG.IDDepatment=6 AND LUONG.MaThuong=602  "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '============ Tạo bảng tạm tính điểm văn hóa nhóm điện theo hệ thống (Phòng KTDV)
        sql &= " SELECT SUM(#VHNhanVien.Diem)DiemVH,IDDepatment INTO #tbDiemVHNhomDien FROM #VHNhanVien "
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=#VHNhanVien.IDNhanVien AND LUONG.LUONGTHUONG <> 0 AND LUONG.IDDepatment=6 AND LUONG.MaThuong=600 "
        sql &= " 			AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " group by IDDepatment "

        '============ Tạo bảng tạm tính điểm văn hóa nhóm cơ theo hệ thống (Phòng KTDV)
        sql &= " SELECT SUM(#VHNhanVien.Diem)DiemVH,IDDepatment INTO #tbDiemVHNhomCo FROM #VHNhanVien "
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=#VHNhanVien.IDNhanVien AND LUONG.LUONGTHUONG <> 0 AND LUONG.IDDepatment=6 AND LUONG.MaThuong=602 "
        sql &= " 			AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " group by IDDepatment "

        '======================= Truy vấn chính"

        sql &= " SELECT tb2.*,(Tong2+BHCongTyTra+QuyCongDoan)ThucChi,(Tong2-BHNhanVienTra-TienAnDaChi-QuyCongDoan)ThucLinh2"
        sql &= " FROM (SELECT tb.*,"
        sql &= " (LuongCong2+LuongThemGio+Thuong2+ISNULL(ThuongDotXuat,0)+PCTrachNhiem+PCXangAn+PCAnCa+PC_CT_Xang+PCDVKTLuong+PCDVKTXang+PC_DVKT_TrachNhiem)Tong2, "
        sql &= " (LuongCong2+LuongThemGio+Thuong2+ISNULL(ThuongDotXuat,0)+PCTrachNhiem+PCXangAn+PCAnCa+PC_CT_Xang+PCDVKTLuong+PCDVKTXang+PC_DVKT_TrachNhiem)*0.01 AS QuyCongDoan"
        sql &= " FROM (Select Luong.[Month],LUONG.IDDepatment,LUONG.IDNhanVien,Nhansu.Ten AS NhanVien, LuongCB, LuongBH, tblTHChamCong.CongThuong, "
        sql &= " (tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) as CongNghi,tblTHChamCong.CNLe1, tblTHChamCong.CNLe2, tblTHChamCong.ThemGio1,tblTHChamCong.ThemGio2,tblTHChamCong.ThemGio3, "

        '==========sTính lương công cách mớ
        'sql &= " (case  "
        'sql &= " 	when LUONG.MaThuong IN(302,303) then"
        'sql &= " 		Case WHEN tblTHChamCong.Diem1/4000<DMThuong THEN"
        'sql &= " 				LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.7 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        ELSE LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) END "
        'sql &= " 	WHEN LUONG.MaThuong=300 THEN "
        'sql &= "        CASE WHEN tblTHChamCong.Diem1/4000>=0.35 THEN "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.5 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        ELSE 0 END"
        'sql &= "    WHEN LUONG.MaThuong=304 THEN"
        'sql &= "        CASE WHEN tblTHChamCong.Diem1/4000>=0.35 THEN "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.5 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "            WHEN tblTHChamCong.Diem1/4000<0.35 AND tblTHChamCong.Diem1/4000>=0.25 THEN"
        'sql &= "                1000000 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) ELSE 0 END"
        'sql &= " 	WHEN LUONG.MaThuong IN (204,305,307,400) THEN "
        'sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)>=0.5 THEN "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        ELSE "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.7 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100)"
        'sql &= "        END"
        'sql &= " 	when LUONG.MaThuong IN (501,2002,4001,3001)  then"
        'sql &= " 		LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100)"
        'sql &= " 	ELSE"
        'sql &= " 		LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100)"
        'sql &= " end ) as LuongCong2 ,"
        sql &= " (CASE WHEN ( tblTHChamCong.Diem1/4000)*100 <= ISNULL(LUONG.DiemDo,DMThuong)"
        sql &= "    THEN LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*(ISNULL(LUONG.PTLuong,70)/100) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        sql &= " ELSE "
        sql &= "     LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100)"
        sql &= " END) as LuongCong2,"

        '====== Lương thêm giờ"
        sql &= " (LuongCB/26*(tblTHChamCong.CNLe1+tblTHChamCong.CNLe2*2+(tblTHChamCong.ThemGio1+tblTHChamCong.ThemGio2*2+tblTHChamCong.ThemGio3*3)/8)) as LuongThemGio,"

        '=== Tính thưởng theo cách mới"
        '--
        sql &= " ((CASE"
        'THưởng điểm

        'Nhân viên kỹ thuật
        sql &= " 	WHEN LUONG.MaThuong = 200 THEN"
        sql &= " 		((ISNULL(#KNNhanVien.Diem,0)*0.95/ISNULL(#KNPhong.Diem,1)) + (#VHNhanVien.Diem*0.05/#VHPhong.Diem))*@ThuongThangPKT"
        sql &= " 	WHEN LUONG.MaThuong = 202 THEN"
        sql &= " 		((ISNULL(#KNNhanVien.Diem,0)*0.95/ISNULL(#KNPhongKTDien.Diem,1)) + (#VHNhanVien.Diem*0.05/#VHPhongKTDien.Diem))*@ThuongThangPKTDien"
        sql &= " 	WHEN LUONG.MaThuong = 203 THEN"
        sql &= " 		((ISNULL(#KNNhanVien.Diem,0)*0.95/ISNULL(#KNPhongKTCo.Diem,1)) + (#VHNhanVien.Diem*0.05/#VHPhongKTCo.Diem))*@ThuongThangPKTCo"
        'Trưởng phòng KT
        sql &= " 	WHEN LUONG.MaThuong = 2001 THEN"
        sql &= " 		((ISNULL(#KNNhanVien.Diem,0)*0.95/ISNULL(#KNPhong.Diem,1)) + (#VHNhanVien.Diem*0.05/#VHPhong.Diem))*@ThuongThangPKT"
        'Nhân viên KT DV nhóm điện
        sql &= " 	WHEN LUONG.MaThuong =600 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#tbDiemVHNhomDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)) WHEN 0 THEN 0 ELSE ((ISNULL(#VHNhanVien.Diem,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#tbDiemVHNhomDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)))*@ThuongThangPKTDVDien END) END) "
        sql &= " 	WHEN LUONG.MaThuong =600 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#tbDiemVHNhomDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)) WHEN 0 THEN 0 ELSE ((ISNULL(#VHNhanVien.Diem,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#tbDiemVHNhomDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)))*@ThuongThangPKTDVDien END) END) "

        'Nhân viên KT DV nhóm cơ
        sql &= " 	WHEN LUONG.MaThuong =602 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#tbDiemVHNhomCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)) WHEN 0 THEN 0 ELSE  ((ISNULL(#VHNhanVien.Diem,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#tbDiemVHNhomCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)))*@ThuongThangPKTDVCo END) END) "
        sql &= " 	WHEN LUONG.MaThuong =603 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#tbDiemVHNhomCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)) WHEN 0 THEN 0 ELSE  ((ISNULL(#VHNhanVien.Diem,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#tbDiemVHNhomCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)))*@ThuongThangPKTDVCo END) END) "
        sql &= " 	WHEN LUONG.MaThuong IN (100,101,201,300,301) THEN"
        sql &= " 		tblTHChamCong.Diem1/4000 * LuongThuong"
        sql &= "     ELSE "
        sql &= " 		0"
        sql &= " END) + "
        'Thưởng doanh thu
        sql &= " (CASE "
        sql &= " 	WHEN LUONG.MaThuong= 201 THEN"
        sql &= " 		CASE WHEN tblTHChamCong.Diem1>DMThuong THEN"
        sql &= " 			tblTHChamCong.LoiNhuan1*HSThuong*tblTHChamCong.Diem1/4000"
        sql &= " 	ELSE 0"
        sql &= " 	END"
        '===== Mã thưởng bằng 5: Của phòng KTDV nhóm điện áp dụng từ ngày 1/7/2014-31/12/2014 - Thưởng thêm theo điểm KN
        sql &= " 	WHEN LUONG.MaThuong=  601 THEN"
        sql &= " 		CASE WHEN LUONG.LuongThuong <> 0 THEN"
        sql &= "                (CASE WHEN #DiemNVPhongKTDVDien.DiemKN/#DiemNVPhongKTDVDien.SoNguoi <=0.65 THEN 0 ELSE "
        sql &= " 				(ISNULL(tblTHChamCong.DiemKN,0)/#DiemNVPhongKTDVDien.DiemKN)*LUONG.LuongThuong END)"
        sql &= " 	    ELSE"
        sql &= " 		    0"
        sql &= " 	    END"
        '===== Mã thưởng bằng 21: Của phòng KTDV nhóm cơ áp dụng từ ngày 1/7/2014-31/12/2014 - Thưởng thêm theo điểm KN
        sql &= " 	WHEN LUONG.MaThuong=  603 THEN"
        sql &= " 		CASE WHEN LUONG.LuongThuong <> 0 THEN"
        sql &= "                (CASE WHEN #DiemNVPhongKTDVCo.DiemKN/#DiemNVPhongKTDVCo.SoNguoi <=0.65 THEN 0 ELSE "
        sql &= " 				(ISNULL(tblTHChamCong.DiemKN,0)/#DiemNVPhongKTDVCo.DiemKN)*LUONG.LuongThuong END)"
        sql &= " 		ELSE"
        sql &= " 			0"
        sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=  300 THEN"
        sql &= " 		CASE WHEN @Thang IN (3,6,9,12) THEN"
        sql &= "                (CASE @Diem1QuyNhomMua WHEN 0 THEN 0 ELSE "
        sql &= " 				((((@DoanhSoNhomMua/(DMThuong/4)) * HSThuong*@DoanhSoNhomMua) /@Diem1QuyNhomMua)*#DiemNVQuy.Diem1)/2 END )"
        sql &= " 		ELSE"
        sql &= " 			0"
        sql &= " 		END"
        'sql &= " 	WHEN LUONG.MaThuong=  301 THEN"
        'sql &= " 		CASE WHEN @Thang IN (3,6,9,12) THEN"
        'sql &= "                (CASE @Diem1QuyNhomMua WHEN 0 THEN 0 ELSE "
        'sql &= " 				((((@DoanhSoNhomMua/(DMThuong/4)) * HSThuong*@DoanhSoNhomMua) /@Diem1QuyNhomMua)*#DiemNVQuy.Diem1)/2 END )"
        'sql &= " 		ELSE"
        'sql &= " 			0"
        'sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=  302 THEN"
        sql &= " 		CASE WHEN tblTHChamCong.Diem1/4000>(DMThuong+0.15) THEN"
        sql &= " 				 CASE WHEN tblTHChamCong.Diem1/4000>2 THEN"
        sql &= " 						2*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 					ELSE  (tblTHChamCong.Diem1/4000)*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 				END"
        sql &= " 			ELSE 0"
        sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=  303 THEN"
        sql &= " 		CASE WHEN tblTHChamCong.Diem1/4000>(DMThuong+0.15) THEN"
        sql &= " 				 CASE WHEN tblTHChamCong.Diem1/4000>1.5 THEN"
        sql &= " 						1.5*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 					ELSE  (tblTHChamCong.Diem1/4000)*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 				END"
        sql &= " 			ELSE 0"
        sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=  6000 THEN"
        sql &= " 		(@SoKS*400000+@SoKT*200000)*((#DiemPhongKTDV.DiemNLDV/(#DiemPhongKTDV.SoNguoi*2000))*0.5 + (#DiemPhongKTDV.DiemVH/((LUONG.HSVanHoa*4000/100)*#DiemPhongKTDV.SoNguoi))*0.05 + ISNULL(@HSKyNang,0)*0.45 )"
        sql &= "    WHEN LUONG.MaThuong=  500 THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)>DMThuong THEN"
        sql &= "            tblTHChamCong.LoiNhuan1*HSThuong"
        sql &= "            ELSE 0 "
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong=  304 THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)>DMThuong THEN"
        sql &= " 				 CASE WHEN tblTHChamCong.Diem1/4000>1.5 THEN"
        sql &= " 						1.5*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 					ELSE  (tblTHChamCong.Diem1/4000)*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 				END"
        sql &= "            ELSE 0"
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong IN(  305,307, 501, 2002, 3001, 4001, 400, 6002,604,104) THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong),75) THEN"
        sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)/100) THEN"
        sql &= " 						((ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 				END"
        sql &= "            ELSE 0"
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong IN (102,103) THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong),75) THEN"
        sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)/100) THEN"
        sql &= " 						((ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 		               + ISNULL(tblTHChamCong.DiemBC,0)/4000 * LuongThuong"
        sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 		               + ISNULL(tblTHChamCong.DiemBC,0)/4000 * LuongThuong"
        sql &= " 				END"
        sql &= "        ELSE ISNULL(tblTHChamCong.DiemBC,0)/4000 * LuongThuong"
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong IN ( 204,502) THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong),75) THEN"
        sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)/100) THEN"
        sql &= " 						((ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*(tblTHChamCong.LoiNhuan1 - isnull(LNTU.LoiNhuanTU,0))*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*(tblTHChamCong.LoiNhuan1 - isnull(LNTU.LoiNhuanTU,0))*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 				END"
        sql &= "            ELSE 0"
        sql &= "        END"

        'sql &= "    WHEN LUONG.MaThuong =307 THEN"
        'sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(tblDMThuongMaThuong.DiemThuong,75) THEN"
        'sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (tblDMThuongMaThuong.DiemPTMax/100) THEN"
        'sql &= " 						((tblDMThuongMaThuong.DiemPTMax-tblDMThuongMaThuong.DiemThuong)*tblTHChamCong.LoiNhuan1*tblDMThuongMaThuong.HeSoThuong)/100"
        'sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-tblDMThuongMaThuong.DiemThuong)*tblTHChamCong.LoiNhuan1*tblDMThuongMaThuong.HeSoThuong)/100"
        'sql &= " 				END"
        'sql &= "            ELSE 0"
        'sql &= "        END"
        sql &= " 	ELSE"
        sql &= " 		0"
        sql &= " END)) Thuong2,ISNULL(ThuongDotXuat,0)ThuongDotXuat,"
        sql &= " PCTrachNhiem, "
        sql &= " ((tblTHChamCong.CongThuong+ tblTHChamCong.CNLe1+tblTHChamCong.CNLe2)*(PCXang+PCAn)) as PCXangAn,"
        sql &= " (tblTHChamCong.PCAnCa* PCAn) as PCAnCa,tblTHChamCong.PC_CT_Xang,"
        sql &= " (PC_DVKT_Luong/26*tblTHChamCong.CongThuong) as PCDVKTLuong,"
        sql &= " (PC_DVKT_Xang/26*tblTHChamCong.CongThuong) as PCDVKTXang,"
        sql &= " PC_DVKT_TrachNhiem,tblTHChamCong.KT_DVKT_An as TienAnDaChi,"
        sql &= " (HS_BH_Congty * LuongBH) as BHCongTyTra,"
        sql &= " (HS_BH_Nhanvien * LuongBH) as BHNhanVienTra,DEPATMENT.Ten AS PhongBan,TaiKhoan,SoCMT,LUONG.ID,LUONG.GhiChu,LUONG.PhanHoi,LUONG.MaThuong,tblTHChamCong.Diem1,LUONG.LuongThuong,NhanSu_BoPhan.MaBP,NhanSu.ChucVu"
        sql &= " from NHANSU"
        sql &= " inner join Luong on Luong.IDNhanvien = Nhansu.ID"
        sql &= " inner join tblTHChamCong on tblTHChamCong.IDNhanvien= Luong.IDNhanvien and Luong.[Month] = tblTHChamCong.[Month]"
        sql &= " LEFT join tblTHChamCong as LNTU on LNTU.IDNhanvien= Luong.IDNhanvien and LNTU.[Month] = @ThangTruoc"
        'sql &= " LEFT join DiemSo_DinhMuc on DiemSo_DinhMuc.IDPhong= Luong.IDDepatment and Luong.[Month] = DiemSo_DinhMuc.Thang AND DiemSo_DinhMuc.MaThuong is NULL"
        'sql &= " LEFT join DiemSo_DinhMuc AS tblDMThuongMaThuong on tblDMThuongMaThuong.MaThuong= Luong.MaThuong and Luong.[Month] = tblDMThuongMaThuong.Thang "
        sql &= " LEFT JOIN DiemSo_DinhMuc ON DiemSo_DinhMuc.ID=LUONG.IDDinhMucTinhDiem"
        sql &= " LEFT JOIN DEPATMENT ON DEPATMENT.ID=LUONG.IDDepatment"
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=LUONG.IDBoPhan"
        'sql &= " LEFT JOIN #tbLoiNhuanTU ON NHANSU.ID=#tbLoiNhuanTU.IDNhanVien"
        sql &= " LEFT JOIN #KNPhong ON LUONG.IDDepatment=#KNPhong.IDDepatment"
        sql &= " LEFT JOIN #KNPhongKTDien ON LUONG.IDDepatment=#KNPhongKTDien.IDDepatment"
        sql &= " LEFT JOIN #KNPhongKTCo ON LUONG.IDDepatment=#KNPhongKTCo.IDDepatment"
        sql &= " LEFT JOIN #tbDiemVHNhomDien ON LUONG.IDDepatment=#tbDiemVHNhomDien.IDDepatment"
        sql &= " LEFT JOIN #tbDiemVHNhomCo ON LUONG.IDDepatment=#tbDiemVHNhomCo.IDDepatment"
        sql &= " LEFT JOIN #VHNhanVien ON #VHNhanVien.IDNhanVien=LUONG.IDNhanVien"
        sql &= " LEFT JOIN #KNNhanVien ON #KNNhanVien.IDNhanVien=LUONG.IDNhanVien"
        sql &= " LEFT JOIN #VHPhong ON #VHPhong.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #VHPhongKTDien ON #VHPhongKTDien.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #VHPhongKTCo ON #VHPhongKTCo.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemPhongKTDV ON #DiemPhongKTDV.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemNVPhongKTDVDien ON #DiemNVPhongKTDVDien.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemNVPhongKTDVCo ON #DiemNVPhongKTDVCo.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemNVQuy ON #DiemNVQuy.IDNhanVien=LUONG.IDNhanVien"
        sql &= " where Left(Luong.[month],2) = " & cbThang.EditValue & " AND Right(Luong.[month],4) = " & tbNam.EditValue.ToString
        If Not cbNhanVien.EditValue Is Nothing Then
            sql &= " AND LUONG.IDNhanVien=" & cbNhanVien.EditValue
        Else
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
                sql &= " AND LUONG.IDDepatment=" & tbD.Rows(0)(0)
            End If
        End If
        sql &= " )tb)tb2 order by [Month],IDDepatment,MaBP,ChucVu, IDNhanVien"

        sql &= " DROP table #KNPhong"
        sql &= " DROP table #KNPhongKTDien"
        sql &= " DROP table #KNPhongKTCo"
        sql &= " DROP table #KNNhanVien"
        sql &= " DROP table #VHNhanVien"
        sql &= " DROP table #VHPhong"
        sql &= " DROP table #VHPhongKTDien"
        sql &= " DROP table #VHPhongKTCo"
        sql &= " DROP table #tbDiemVHNhomDien"
        sql &= " DROP table #tbDiemVHNhomCo"
        sql &= " DROP table #DiemPhongKTDV"
        sql &= " DROP table #DiemNVPhongKTDVDien"
        sql &= " DROP table #DiemNVPhongKTDVCo"
        sql &= " DROP table #DiemNVQuy"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TinhLuongDaDuyet()

        Dim tbD As DataTable = ExecuteSQLDataTable("SELECT IDDepatment FROM LUONG WHERE [Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND IDNhanVien=" & TaiKhoan)
        If tbD Is Nothing Then
            ShowCanhBao("Không lấy được thông tin phòng ban !")
            Exit Sub
        End If
        Dim sql As String = ""
        'sql &= " SET DATEFORMAT DMY"
        'sql &= " EXEC getLoiNhuanTU '" & DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, cbThang.EditValue, 1)).ToString("dd/MM/yyyy") & "'"
        'sql &= " IF OBJECT_ID('tempdb..#tbLoiNhuanTU') IS NULL "
        'sql &= " BEGIN"
        'sql &= " 	DECLARE @tbt table"
        'sql &= " 	("
        'sql &= " 		IDNhanVien int,"
        'sql &= " 		LoiNhuanTU float"
        'sql &= " 	)"
        'sql &= " SELECT * "
        'sql &= " INTO #tbLoiNhuanTU"
        'sql &= " FROM "
        'sql &= " @tbt"
        'sql &= " End"
        sql &= " SET DATEFORMAT DMY"
        sql &= " DECLARE @DoanhSoNhomMua AS float"
        sql &= " DECLARE @TongDiem3 AS Float"
        sql &= " DECLARE @Thang AS int"
        sql &= " DECLARE @Nam AS int"
        sql &= " DECLARE @ThuongThangPKT AS float"
        sql &= " DECLARE @ThuongThangPKTDien AS float"
        sql &= " DECLARE @ThuongThangPKTCo AS float"
        sql &= " DECLARE @HSKyNang AS Float"
        sql &= " DECLARE @ThuongThangPKTDVDien AS Float"
        sql &= " DECLARE @ThuongThangPKTDVCo AS Float"
        sql &= " DECLARE @SoKS AS int"
        sql &= " DECLARE @SoKT AS int"
        sql &= " DECLARE @Diem1QuyNhomMua AS float"

        sql &= " DECLARE @ThangTruoc AS nvarchar(10)"

        sql &= " SET @Thang=" & cbThang.EditValue
        sql &= " SET @Nam=" & tbNam.EditValue.ToString
        sql &= " SET @ThangTruoc='" & DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, cbThang.EditValue, 1)).ToString("MM/yyyy") & "'"

        sql &= " SET @ThuongThangPKT=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=2) "
        sql &= " SET @ThuongThangPKTDVDien=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=6 AND MaThuong=600)"
        sql &= " SET @ThuongThangPKTDVCo=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=6 AND MaThuong=602)"
        sql &= " SET @ThuongThangPKTDien=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=2 AND MaThuong=202) "
        sql &= " SET @ThuongThangPKTCo=(SELECT SUM(LuongThuong) FROM LUONG WHERE Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam AND IDDepatment=2 AND MaThuong=203) "
        'sql &= " SET @TongDiemNangLucPKTDV=1"

        '========Tính doanh số nhóm mua theo quý

        sql &= " SET @DoanhSoNhomMua = ISNULL((SELECT DSNhomMua FROM tblDuyetLuong WHERE Month='" & cbThang.EditValue & "/" & tbNam.EditValue & "'),0)"

        '========Tổng điểm theo nhóm mã thưởng (tính theo quý)

        sql &= " SET @Diem1QuyNhomMua = (SELECT SUM(Diem1)Diem "
        sql &= " FROM(SELECT SUM(tblTHChamCong.Diem1)Diem1,IDNhanVien"
        sql &= " 	FROM tblTHChamCong"
        sql &= " 	WHERE Right(tblTHChamCong.[month],4) = @Nam  AND Left(tblTHChamCong.[month],2) IN (" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "," & (Convert.ToInt32(cbThang.EditValue) - 1).ToString & "," & (Convert.ToInt32(cbThang.EditValue).ToString) & ")    "
        sql &= " 	GROUP BY IDNhanVien)tb"
        sql &= " WHERE tb.IDNhanVien IN (SELECT DISTINCT IDNhanVien FROM LUONG WHERE Right(LUONG.[month],4) = @Nam  AND Left(LUONG.[month],2) IN (" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "," & (Convert.ToInt32(cbThang.EditValue) - 1).ToString & "," & (Convert.ToInt32(cbThang.EditValue).ToString) & ") AND (LUONG.MaThuong=300 OR LUONG.MaThuong=301)))"


        '========Tổng điểm tính theo quý của nhân viên

        sql &= " SELECT SUM(ISNULL(tblTHChamCong.Diem,0))Diem,SUM(ISNULL(tblTHChamCong.Diem1,0))Diem1,tblTHChamCong.IDNhanVien INTO #DiemNVQuy FROM tblTHChamCong INNER JOIN LUONG ON LUONG.[Month]=tblTHChamCong.[Month] AND LUONG.IDNhanVien=tblTHChamCong.IDNhanVien "
        sql &= "     WHERE Right(tblTHChamCong.[month],4) = @Nam"
        If Convert.ToInt32(cbThang.EditValue) = 3 Or Convert.ToInt32(cbThang.EditValue) = 6 Or Convert.ToInt32(cbThang.EditValue) = 9 Or Convert.ToInt32(cbThang.EditValue) = 12 Then
            sql &= " AND Left(tblTHChamCong.[month],2) IN (" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "," & (Convert.ToInt32(cbThang.EditValue) - 1).ToString & "," & (Convert.ToInt32(cbThang.EditValue).ToString) & ")"
        End If
        sql &= " GROUP BY tblTHChamCong.IDNhanVien"


        '========== Tính số kỹ thuật và số kỹ sư
        sql &= " SET @SoKS= ( SELECT COUNT(ID) FROM LUONG WHERE ChucVu=0 AND Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam)"
        sql &= " SET @SoKT= ( SELECT COUNT(ID) FROM LUONG WHERE ChucVu=1 AND Left(Luong.[month],2) = @Thang  AND Right(Luong.[month],4) = @Nam)"

        '========== HS kỹ năng của phòng"

        sql &= " SET @HSKyNang=ISNULL((SELECT HSKyNang FROM tblDuyetLuong WHERE Month='" & cbThang.EditValue & "/" & tbNam.EditValue & "'),0)"

        '=========== Tạo bảng tạm tính điểm văn hóa, kỹ năng, NLDV của phòng --- KTDV
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,SUM(DiemNLDV)DiemNLDV,Count(IDNhanVien)SoNguoi INTO #DiemPhong FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '==========Bảng tạm tính điểm của phòng KT (ĐK: Lương thưởng <>0)
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,Count(IDNhanVien)SoNguoi INTO #DiemPhongKT FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien  AND LUONG.LUONGTHUONG<>0 AND LUONG.IDDepatment=200 "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '==========Bảng tạm tính điểm của phòng KT nhóm điện (ĐK: Lương thưởng <>0)
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,Count(IDNhanVien)SoNguoi INTO #DiemPhongKTDien FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien  AND LUONG.LUONGTHUONG<>0 AND LUONG.IDDepatment=2 AND LUONG.MaThuong=202 "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '==========Bảng tạm tính điểm của phòng KT nhóm cơ (ĐK: Lương thưởng <>0)
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,Count(IDNhanVien)SoNguoi INTO #DiemPhongKTCo FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien  AND LUONG.LUONGTHUONG<>0 AND LUONG.IDDepatment=2 AND LUONG.MaThuong=203 "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '=========== Tạo bảng tạm tính điểm văn hóa, kỹ năng của phòng trừ NV thử việc (Nhóm điện)
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,SUM(DiemNLDV)DiemNLDV,Count(IDNhanVien)SoNguoi INTO #DiemNVPhongKTDVDien FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien AND LUONG.LUONGTHUONG<>0 AND LUONG.IDDepatment=6 AND LUONG.MaThuong=600 "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"

        '=========== Tạo bảng tạm tính điểm văn hóa, kỹ năng của phòng kỹ thuật dịch vụ (nhóm cơ) trừ NV thử việc trừ TP
        sql &= " SELECT IDDepatment,SUM(DiemVH)DiemVH,SUM(DiemKN)DiemKN,SUM(DiemNLDV)DiemNLDV,Count(IDNhanVien)SoNguoi INTO #DiemNVPhongKTDVCo FROM ("
        sql &= " 	SELECT tblTHChamCong.IDNhanVien,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,LUONG.IDDepatment "
        sql &= " 	FROM tblTHChamCong INNER JOIN LUONG ON LUONG.IDNhanVien=tblTHChamCong.IDNhanVien AND LUONG.LUONGTHUONG<>0 AND LUONG.IDDepatment=6 AND LUONG.MaThuong=602  "
        sql &= " 				AND Luong.[month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " 				AND  tblTHChamCong.[Month] = '" & cbThang.EditValue & "/" & tbNam.EditValue & "'"
        sql &= " )tb group by IDDepatment"
        '======================= Truy vấn chính"

        sql &= " SELECT tb2.*,(Tong2+BHCongTyTra+QuyCongDoan )ThucChi,(Tong2-BHNhanVienTra-TienAnDaChi-QuyCongDoan)ThucLinh2"
        sql &= " FROM (SELECT tb.*,"
        sql &= " (LuongCong2+LuongThemGio+Thuong2+ISNULL(ThuongDotXuat,0)+PCTrachNhiem+PCXangAn+PCAnCa+PC_CT_Xang+PCDVKTLuong+PCDVKTXang+PC_DVKT_TrachNhiem)Tong2, "
        sql &= " (LuongCong2+LuongThemGio+Thuong2+ISNULL(ThuongDotXuat,0)+PCTrachNhiem+PCXangAn+PCAnCa+PC_CT_Xang+PCDVKTLuong+PCDVKTXang+PC_DVKT_TrachNhiem)*0.01 AS QuyCongDoan"
        sql &= " FROM (Select Luong.[Month],LUONG.IDDepatment,LUONG.IDNhanVien,Nhansu.Ten AS NhanVien, LuongCB, LuongBH, tblTHChamCong.CongThuong, "
        sql &= " (tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) as CongNghi,tblTHChamCong.CNLe1, tblTHChamCong.CNLe2, tblTHChamCong.ThemGio1,tblTHChamCong.ThemGio2,tblTHChamCong.ThemGio3, "

        '==========sTính lương công cách mới
        '--Theo điểm 2
        'sql &= " (case  "
        'sql &= " 	when LUONG.MaThuong IN (302,303) then"
        'sql &= " 		Case WHEN tblTHChamCong.Diem1/4000<DMThuong THEN"
        'sql &= " 				LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.7 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        ELSE LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) END "
        'sql &= " 	WHEN LUONG.MaThuong=300 THEN "
        'sql &= "        CASE WHEN tblTHChamCong.Diem1/4000>=0.35 THEN "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.5 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        ELSE 0 END"
        'sql &= "    WHEN LUONG.MaThuong=304 THEN"
        'sql &= "        CASE WHEN tblTHChamCong.Diem1/4000>=0.35 THEN "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.5 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "            WHEN tblTHChamCong.Diem1/4000<0.35 AND tblTHChamCong.Diem1/4000>=0.25 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) THEN"
        'sql &= "                1000000 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) ELSE 0 END"
        'sql &= " 	WHEN LUONG.MaThuong IN (204,305,307,400) THEN "
        'sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)>=0.5 THEN "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)- LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        ELSE "
        'sql &= "               LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*0.7 - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= "        END"
        'sql &= " 	when LUONG.MaThuong IN (2002,501,4001,3001)  then"
        'sql &= " 		LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100)"
        'sql &= " 	ELSE"
        'sql &= " 		LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        'sql &= " end ) as LuongCong2 ,"

        sql &= " (CASE WHEN (tblTHChamCong.Diem1/4000)*100 <= ISNULL(LUONG.DiemDo,DMThuong)"
        sql &= "    THEN LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe)*(ISNULL(LUONG.PTLuong,70)/100) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100) "
        sql &= " ELSE "
        sql &= "     LuongCB/26*(tblTHChamCong.CongThuong+tblTHChamCong.CongPhep+tblTHChamCong.CongNghiLe) - LuongCB*LUONG.CTKhongHT*(Convert(float,LUONG.PhatChiTieu)/100)"
        sql &= " END) as LuongCong2,"
        '====== Lương thêm giờ"
        sql &= " (LuongCB/26*(tblTHChamCong.CNLe1+tblTHChamCong.CNLe2*2+(tblTHChamCong.ThemGio1+tblTHChamCong.ThemGio2*2+tblTHChamCong.ThemGio3*3)/8)) as LuongThemGio,"

        '=== Tính thưởng theo cách mới"
        '--Theo điểm 2
        sql &= " ((CASE"
        'Nhân viên kỹ thuật
        sql &= " 	WHEN LUONG.MaThuong = 200 THEN"
        sql &= " 		((ISNULL(tblTHChamCong.DiemKN,0)*0.95/ISNULL(#DiemPhongKT.DiemKN,1)) + (tblTHChamCong.DiemVH*0.05/#DiemPhongKT.DiemVH))*@ThuongThangPKT"
        sql &= " 	WHEN LUONG.MaThuong = 202 THEN"
        sql &= " 		((ISNULL(tblTHChamCong.DiemKN,0)*0.95/ISNULL(#DiemPhongKTDien.DiemKN,1)) + (tblTHChamCong.DiemVH*0.05/#DiemPhongKTDien.DiemVH))*@ThuongThangPKTDien"
        sql &= " 	WHEN LUONG.MaThuong = 203 THEN"
        sql &= " 		((ISNULL(tblTHChamCong.DiemKN,0)*0.95/ISNULL(#DiemPhongKTCo.DiemKN,1)) + (tblTHChamCong.DiemVH*0.05/#DiemPhongKTCo.DiemVH))*@ThuongThangPKTCo"
        'Trưởng phòng KT
        sql &= " 	WHEN LUONG.MaThuong = 2001 THEN"
        sql &= " 		((ISNULL(tblTHChamCong.DiemKN,0)*0.95/ISNULL(#DiemPhongKT.DiemKN,1)) + (tblTHChamCong.DiemVH*0.05/#DiemPhongKT.DiemVH))*@ThuongThangPKT"
        sql &= " 	WHEN LUONG.MaThuong =600 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#DiemNVPhongKTDVDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)) WHEN 0 THEN 0 ELSE ((ISNULL(tblTHChamCong.DiemVH,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#DiemNVPhongKTDVDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)))*@ThuongThangPKTDVDien END) END) "
        'Nhân viên KT DV nhóm điện - áp dụng từ 1/7/2014 - 31/12/2014
        sql &= " 	WHEN LUONG.MaThuong =601 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#DiemNVPhongKTDVDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)) WHEN 0 THEN 0 ELSE ((ISNULL(tblTHChamCong.DiemVH,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#DiemNVPhongKTDVDien.DiemVH,0)+ISNULL(#DiemNVPhongKTDVDien.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVDien.DiemKN,0)))*@ThuongThangPKTDVDien END) END) "
        'Nhân viên KT DV nhóm cơ- áp dụng từ 1/1/2014 - 30/06/2014
        sql &= " 	WHEN LUONG.MaThuong =602 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#DiemNVPhongKTDVCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)) WHEN 0 THEN 0 ELSE  ((ISNULL(tblTHChamCong.DiemVH,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#DiemNVPhongKTDVCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)))*@ThuongThangPKTDVCo END) END) "
        'Nhân viên KT DV nhóm cơ- áp dụng từ 1/7/2014 - 31/12/2014
        sql &= " 	WHEN LUONG.MaThuong =603 THEN"
        sql &= " 		(CASE LUONG.LuongThuong WHEN 0 THEN 0 ELSE (CASE (ISNULL(#DiemNVPhongKTDVCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)) WHEN 0 THEN 0 ELSE  ((ISNULL(tblTHChamCong.DiemVH,0)+ISNULL(tblTHChamCong.DiemNLDV,0)+ISNULL(tblTHChamCong.DiemKN,0))/(ISNULL(#DiemNVPhongKTDVCo.DiemVH,0)+ISNULL(#DiemNVPhongKTDVCo.DiemNLDV,0)+ISNULL(#DiemNVPhongKTDVCo.DiemKN,0)))*@ThuongThangPKTDVCo END) END) "
        sql &= " 	WHEN LUONG.MaThuong IN (100,101,201,300,301) THEN"
        sql &= " 		tblTHChamCong.Diem1/4000 * LuongThuong"
        sql &= "     ELSE "
        sql &= " 		0"
        sql &= " END) + "

        sql &= " (CASE "
        sql &= " 	WHEN LUONG.MaThuong= 201 THEN"
        sql &= " 		CASE WHEN tblTHChamCong.Diem1>DMThuong THEN"
        sql &= " 			tblTHChamCong.LoiNhuan1*HSThuong*tblTHChamCong.Diem1/4000"
        sql &= " 		ELSE 0"
        sql &= " 		END"
        '===== Mã thưởng bằng 5: Của phòng KTDV nhóm điện áp dụng từ ngày 1/7/2014-31/12/2014v - Thưởng thêm theo điểm KN
        sql &= " 	WHEN LUONG.MaThuong= 601 THEN"
        sql &= " 		CASE WHEN LUONG.LuongThuong <> 0 THEN"
        sql &= "                (CASE WHEN #DiemNVPhongKTDVDien.DiemKN/#DiemNVPhongKTDVDien.SoNguoi <=0.65 THEN 0 ELSE "
        sql &= " 				(ISNULL(tblTHChamCong.DiemKN,0)/#DiemNVPhongKTDVDien.DiemKN)*LUONG.LuongThuong END)"
        sql &= " 		ELSE"
        sql &= " 				0"
        sql &= " 		END"
        '===== Mã thưởng bằng 21: Của phòng KTDV nhóm cơ áp dụng từ ngày 1/7/2014-31/12/2014v - Thưởng thêm theo điểm KN
        sql &= " 	WHEN LUONG.MaThuong=603 THEN"
        sql &= " 		CASE WHEN LUONG.LuongThuong <> 0 THEN"
        sql &= "                (CASE WHEN #DiemNVPhongKTDVCo.DiemKN/#DiemNVPhongKTDVCo.SoNguoi <=0.65 THEN 0 ELSE "
        sql &= " 				(ISNULL(tblTHChamCong.DiemKN,0)/#DiemNVPhongKTDVCo.DiemKN)*LUONG.LuongThuong END)"
        sql &= " 		ELSE"
        sql &= " 				0"
        sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=300 THEN"
        sql &= " 		CASE WHEN @Thang IN (3,6,9,12) THEN"
        sql &= "                (CASE @Diem1QuyNhomMua WHEN 0 THEN 0 ELSE "
        sql &= " 				((((@DoanhSoNhomMua/(DMThuong/4)) * HSThuong*@DoanhSoNhomMua) /@Diem1QuyNhomMua)*#DiemNVQuy.Diem1)/2 END)"
        sql &= " 			ELSE"
        sql &= " 				0"
        sql &= " 		END"
        'sql &= " 	WHEN LUONG.MaThuong=301 THEN"
        'sql &= " 		CASE WHEN @Thang IN (3,6,9,12) THEN"
        'sql &= "                (CASE @Diem1QuyNhomMua WHEN 0 THEN 0 ELSE "
        'sql &= " 				((((@DoanhSoNhomMua/(DMThuong/4)) * HSThuong*@DoanhSoNhomMua) /@Diem1QuyNhomMua)*#DiemNVQuy.Diem1)/2 END)"
        'sql &= " 			ELSE"
        'sql &= " 				0"
        'sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=302 THEN"
        sql &= " 		CASE WHEN tblTHChamCong.Diem1/4000>(DMThuong+0.15) THEN"
        sql &= " 				 CASE WHEN tblTHChamCong.Diem1/4000>2 THEN"
        sql &= " 						2*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 					ELSE  (tblTHChamCong.Diem1/4000)*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 				END"
        sql &= " 			ELSE 0"
        sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=303 THEN"
        sql &= " 		CASE WHEN tblTHChamCong.Diem1/4000>(DMThuong+0.15) THEN"
        sql &= " 				 CASE WHEN tblTHChamCong.Diem1/4000>1.5 THEN"
        sql &= " 						1.5*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 					ELSE  (tblTHChamCong.Diem1/4000)*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 				END"
        sql &= " 			ELSE 0"
        sql &= " 		END"
        sql &= " 	WHEN LUONG.MaThuong=6000 THEN"
        sql &= " 		(@SoKS*400000+@SoKT*200000)*((#DiemPhong.DiemNLDV/(#DiemPhong.SoNguoi*2000))*0.5 + (#DiemPhong.DiemVH/((LUONG.HSVanHoa*4000/100)*#DiemPhong.SoNguoi))*0.05 + ISNULL(@HSKyNang,0)*0.45 )"
        sql &= "    WHEN LUONG.MaThuong= 500 THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)>DMThuong THEN"
        sql &= "            tblTHChamCong.LoiNhuan1*HSThuong"
        sql &= "            ELSE 0 "
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong=304 THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)>DMThuong THEN"
        sql &= " 				 CASE WHEN tblTHChamCong.Diem1/4000>1.5 THEN"
        sql &= " 						1.5*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 					ELSE  (tblTHChamCong.Diem1/4000)*tblTHChamCong.DoanhThu1*HSThuong"
        sql &= " 				END"
        sql &= "            ELSE 0"
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong IN (305,307, 501, 2002, 3001, 4001, 400, 6002,604,104) THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong),75) THEN"
        sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)/100) THEN"
        sql &= " 						((ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 				END"
        sql &= "            ELSE 0"
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong IN (102,103) THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong),75) THEN"
        sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)/100) THEN"
        sql &= " 						((ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 		               + ISNULL(tblTHChamCong.DiemBC,0)/4000 * LuongThuong"
        sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*tblTHChamCong.LoiNhuan1*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 		               + ISNULL(tblTHChamCong.DiemBC,0)/4000 * LuongThuong"
        sql &= " 				END"
        sql &= "        ELSE ISNULL(tblTHChamCong.DiemBC,0)/4000 * LuongThuong"
        sql &= "        END"
        sql &= "    WHEN LUONG.MaThuong IN (204,502) THEN"
        sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong),75) THEN"
        sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) > (ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)/100) THEN"
        sql &= " 						((ISNULL(LUONG.DiemPTMax,DiemSo_DinhMuc.DiemPTMax)-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*(tblTHChamCong.LoiNhuan1 - isnull(LNTU.LoiNhuanTU,0))*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-ISNULL(LUONG.DiemThuong,DiemSo_DinhMuc.DiemThuong))*(tblTHChamCong.LoiNhuan1 - isnull(LNTU.LoiNhuanTU,0))*ISNULL(LUONG.HeSoThuong,DiemSo_DinhMuc.HeSoThuong))/100"
        sql &= " 				END"
        sql &= "            ELSE 0"
        sql &= "        END"
        'sql &= "    WHEN LUONG.MaThuong =307 THEN"
        'sql &= "        CASE WHEN (tblTHChamCong.Diem1/4000)*100 > ISNULL(tblDMThuongMaThuong.DiemThuong,75) THEN"
        'sql &= " 				 CASE WHEN (tblTHChamCong.Diem1/4000) >(tblDMThuongMaThuong.DiemPTMax/100) THEN"
        'sql &= " 						((tblDMThuongMaThuong.DiemPTMax-tblDMThuongMaThuong.DiemThuong)*tblTHChamCong.LoiNhuan1*tblDMThuongMaThuong.HeSoThuong)/100"
        'sql &= " 					ELSE  (((tblTHChamCong.Diem1/4000)*100-tblDMThuongMaThuong.DiemThuong)*tblTHChamCong.LoiNhuan1*tblDMThuongMaThuong.HeSoThuong)/100"
        'sql &= " 				END"
        'sql &= "            ELSE 0"
        'sql &= "        END"
        sql &= " 	ELSE"
        sql &= " 		0"
        sql &= " END)) Thuong2,ISNULL(ThuongDotXuat,0)ThuongDotXuat,"
        sql &= " PCTrachNhiem, "
        sql &= " ((tblTHChamCong.CongThuong+ tblTHChamCong.CNLe1+tblTHChamCong.CNLe2)*(PCXang+PCAn)) as PCXangAn,"
        sql &= " (tblTHChamCong.PCAnCa* PCAn) as PCAnCa,tblTHChamCong.PC_CT_Xang,"
        sql &= " (PC_DVKT_Luong/26*tblTHChamCong.CongThuong) as PCDVKTLuong,"
        sql &= " (PC_DVKT_Xang/26*tblTHChamCong.CongThuong) as PCDVKTXang,"
        sql &= " PC_DVKT_TrachNhiem,tblTHChamCong.KT_DVKT_An as TienAnDaChi,"
        sql &= " (HS_BH_Congty * LuongBH) as BHCongTyTra,"
        sql &= " (HS_BH_Nhanvien * LuongBH) as BHNhanVienTra,DEPATMENT.Ten AS PhongBan,TaiKhoan,SoCMT,LUONG.ID,LUONG.GhiChu,LUONG.PhanHoi,LUONG.MaThuong,tblTHChamCong.Diem1,LUONG.LuongThuong,NHANSU_BoPhan.MaBP,NhanSu.ChucVu"
        sql &= " from NHANSU"
        sql &= " inner join Luong on Luong.IDNhanvien = Nhansu.ID"
        sql &= " inner join tblTHChamCong on tblTHChamCong.IDNhanvien= Luong.IDNhanvien and Luong.[Month] = tblTHChamCong.[Month]"
        sql &= " LEFT join tblTHChamCong as LNTU on LNTU.IDNhanvien= Luong.IDNhanvien and LNTU.[Month] = @ThangTruoc"
        sql &= " LEFT JOIN DiemSo_DinhMuc ON DiemSo_DinhMuc.ID=LUONG.IDDinhMucTinhDiem"
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=LUONG.IDBoPhan"
        sql &= " LEFT JOIN DEPATMENT ON DEPATMENT.ID=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemPhong ON #DiemPhong.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemPhongKT ON #DiemPhongKT.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemPhongKTDien ON #DiemPhongKTDien.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemPhongKTCo ON #DiemPhongKTCo.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemNVPhongKTDVDien ON #DiemNVPhongKTDVDien.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemNVPhongKTDVCo ON #DiemNVPhongKTDVCo.IDDepatment=LUONG.IDDepatment"
        sql &= " LEFT JOIN #DiemNVQuy ON #DiemNVQuy.IDNhanVien=LUONG.IDNhanVien"
        sql &= " where Left(Luong.[month],2) = " & cbThang.EditValue & " AND Right(Luong.[month],4) = " & tbNam.EditValue.ToString
        If Not cbNhanVien.EditValue Is Nothing Then
            sql &= " AND LUONG.IDNhanVien=" & cbNhanVien.EditValue
        Else
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
                sql &= " AND LUONG.IDDepatment=" & tbD.Rows(0)(0)
            End If
        End If

        sql &= " )tb)tb2 order by [Month],IDDepatment,MaBP,ChucVu, IDNhanVien"

        sql &= " DROP table #DiemPhong"
        sql &= " DROP table #DiemPhongKT"
        sql &= " DROP table #DiemPhongKTDien"
        sql &= " DROP table #DiemPhongKTCo"
        sql &= " DROP table #DiemNVPhongKTDVDien"
        sql &= " DROP table #DiemNVPhongKTDVCo"
        sql &= " DROP table #DiemNVQuy"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        chkDuyet.Checked = KiemTraDuyetLuong(cbThang.EditValue & "/" & tbNam.EditValue.ToString)
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
            SaiMatKhau = True
            Dim f As New frmXacNhanMatKhau
            f.ShowDialog()
            If SaiMatKhau = False Then
                If chkDuyet.Checked = True Then
                    TinhLuongDaDuyet()
                Else
                    XemLuongChuaDuyet()
                End If

            Else
                gdv.DataSource = Nothing
            End If
        Else
            If chkDuyet.Checked = True Then
                TinhLuongDaDuyet()
            Else
                XemLuongChuaDuyet()
            End If
        End If

    End Sub


    Private Sub rcbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbThang.ButtonClick
        If e.Button.Index = 1 Then
            cbThang.EditValue = Nothing
        End If
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "TH_LUONG_" & tbNam.EditValue.ToString & cbThang.EditValue & ".xls"
        saveFile.OverwritePrompt = False
        If saveFile.ShowDialog = DialogResult.OK Then

            Try
                ShowWaiting("Đang kết xuất ...")
                Utils.XuatExcel.TH_LuongThuong(saveFile.FileName, gdv.DataSource, cbThang.EditValue & "/" & tbNam.EditValue.ToString)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                CloseWaiting()
            End Try

        End If
    End Sub

    Private Sub chkSoSanh_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSoSanh.CheckedChanged
        If chkSoSanh.Checked Then
            chkSoSanh.Glyph = My.Resources.Checked
            colLuongCongCu.VisibleIndex = 8
            colLuongCong1.VisibleIndex = 9
            colLuongCong2.Caption = "Lương công 2"
            colThuongCu.VisibleIndex = 12
            colThuong1.VisibleIndex = 13
            colThuong2.Caption = "Thưởng 2"
            colTongCu.VisibleIndex = 23
            colTongMoi1.VisibleIndex = 24
            colTongMoi2.Caption = "Tổng 2"
            colThucLinhCu.VisibleIndex = 30
            colThucLinh1.VisibleIndex = 31
            colThucLinh2.Caption = "Còn lĩnh 2"
        Else
            chkSoSanh.Glyph = My.Resources.UnCheck
            colLuongCongCu.Visible = False
            colLuongCong1.Visible = False
            colLuongCong2.Caption = "Lương công"
            colThuongCu.Visible = False
            colThuong1.Visible = False
            colThuong2.Caption = "Thưởng"
            colTongCu.Visible = False
            colTongMoi1.Visible = False
            colTongMoi2.Caption = "Tổng"
            colThucLinhCu.Visible = False
            colThucLinh1.Visible = False
            colThucLinh2.Caption = "Còn lĩnh"
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub cbThang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbThang.EditValueChanged, tbNam.EditValueChanged
        LoadDSNhanVien()
    End Sub

    Private Sub chkDuyet_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkDuyet.CheckedChanged
        If chkDuyet.Checked Then
            chkDuyet.Glyph = My.Resources.Checked
        Else
            chkDuyet.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub btXacNhan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXacNhan.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvCT.RowCount = 0 Then
            ShowCanhBao("Cần tải số liệu trước khi thực hiện thao tác này !")
            Exit Sub
        End If

        Dim tbkt As DataTable = ExecuteSQLDataTable("SELECT Count([Month]) FROM tblDuyetLuong WHERE [Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "'")
        If tbkt Is Nothing Then
            ShowBaoLoi("Không tìm thấy thông tin tháng lương " & cbThang.EditValue & "/" & tbNam.EditValue)
            Exit Sub
        Else
            If tbkt.Rows(0)(0) = 0 Then
                AddParameter("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
                AddParameter("@Duyet", False)
                If doInsert("tblDuyetLuong") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
            End If
        End If

        Dim strCauHoi As String = ""
        If chkDuyet.Checked Then
            strCauHoi = "Duyệt thông tin lương tháng " & cbThang.EditValue & "/" & tbNam.EditValue & "?"
        Else
            strCauHoi = "Bỏ xác nhận duyệt thông tin lương tháng " & cbThang.EditValue & "/" & tbNam.EditValue & "?"
        End If

        If ShowCauHoi(strCauHoi) Then
            AddParameter("@Duyet", chkDuyet.Checked)
            AddParameterWhere("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
            If doUpdate("tblDuyetLuong", "Month=@Month") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                If chkDuyet.Checked Then

                    If ShowCauHoi("Bạn có muốn lưu lại số liệu tính toán lương thưởng tháng " & cbThang.EditValue & "/" & tbNam.EditValue & " không ?") Then
                        Try
                            Dim sql As String = "SET DATEFORMAT DMY"

                            sql &= " SELECT ISNULL( SUM(Diem)/Sum(DiemChuan),0)Diem"
                            sql &= " FROM (SELECT tmpTb.IDNhanVien,tb2.Diem,NLDanhSach.Diem AS DiemChuan FROM ("
                            sql &= " select"
                            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
                            sql &= "     max(tb.NgayThi) as Ngay"
                            sql &= " from"
                            sql &= "     tblDiemThiKyNang tb"
                            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
                            sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
                            sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"
                            sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=tmpTb.IDNhanVien AND LUONG.[Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND LUONG.IDDepatment=6"
                            sql &= " INNER JOIN NLDanhSach ON NLDanhSach.ID=tb2.IDKyNang"
                            sql &= " )tb1"
                            Dim tbHSKN As DataTable = ExecuteSQLDataTable(sql)
                            If tbHSKN Is Nothing Then Throw New Exception(LoiNgoaiLe)

                            sql = " SET DATEFORMAT DMY"
                            If Convert.ToInt32(cbThang.EditValue) = 3 Or Convert.ToInt32(cbThang.EditValue) = 6 Or Convert.ToInt32(cbThang.EditValue) = 9 Or Convert.ToInt32(cbThang.EditValue) = 12 Then
                                sql &= " DECLARE @TongDoanhSo as Float"
                                sql &= " DECLARE @TongDoanhSoSMCOmron as Float"
                                sql &= " DECLARE @ChiPhiNhanCong	AS Float"
                                ' -- Tổng Doanh số
                                sql &= " SELECT * INTO #tbXuatKhoChinh FROM("
                                sql &= " Select DISTINCT"
                                sql &= "   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
                                sql &= "     (PHIEUXUATKHO.Tientruocthue - PHIEUXUATKHO.TienChietKhau)* PHIEUXUATKHO.Tygia as DoanhThu, "
                                sql &= "       (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia "
                                sql &= " 			WHEN 0 THEN 0 "
                                sql &= " 			ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) "
                                sql &= " 					- ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                                sql &= " 					- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) "
                                sql &= " 					- ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / "
                                sql &= " 			(1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"

                                sql &= " FROM PHIEUXUATKHO "
                                sql &= "  LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang)  "
                                sql &= "  LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
                                sql &= "  LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
                                sql &= "  LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
                                sql &= "  WHERE Convert(datetime,PHIEUXUATKHO.NgayThang,103) >= convert(datetime,'01/" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "/" & tbNam.EditValue & "',103)"
                                sql &= " AND Convert(datetime,PHIEUXUATKHO.NgayThang,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103)  )tb WHERE PTLN >5"

                                sql &= " SET @TongDoanhSo = (SELECT SUM(DoanhThu)TongDanhSo FROM #tbXuatKhoChinh)"
                                sql &= " SET @TongDoanhSoSMCOmron = (SELECT SUM(TongGia)TienSMCOmron"
                                sql &= " FROM"
                                sql &= " (SELECT PHIEUXUATKHO.SoPhieu,(XUATKHO.Soluong * XUATKHO.Dongia* PHIEUXUATKHO.Tygia) AS TongGia"
                                sql &= "     FROM XUATKHO"
                                sql &= " 	INNER JOIN #tbXuatKhoChinh ON #tbXuatKhoChinh.SophieuXK = dbo.XUATKHO.Sophieu "
                                sql &= " 	INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
                                sql &= " 	INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu AND VATTU.IDHangSanXuat IN (15,265,378,70)"
                                sql &= " )tb)"

                                sql &= " SET @ChiPhiNhanCong = (SELECT SUM(TongTien)TienNhanCong"
                                sql &= " FROM"
                                sql &= " (SELECT PHIEUXUATKHO.SoPhieu,(XUATKHOAUX.Soluong * XUATKHOAUX.Dongia* PHIEUXUATKHO.Tygia) AS TongTien"
                                sql &= " FROM XUATKHOAUX"
                                sql &= " 	INNER JOIN #tbXuatKhoChinh ON #tbXuatKhoChinh.SophieuXK = XUATKHOAUX.Sophieu "
                                sql &= " 	INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHOAUX.SoPhieu"
                                sql &= " )tb)"

                                sql &= " SELECT ISNULL(( @TongDoanhSo- ISNULL(@TongDoanhSoSMCOmron,0) - ISNULL(@ChiPhiNhanCong,0)),0)"

                                sql &= " DROP table #tbXuatKhoChinh"
                                Dim tb As DataTable = ExecuteSQLDataTable(sql)
                                If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
                                AddParameter("@DSNhomMua", tb.Rows(0)(0))
                            Else
                                AddParameter("@DSNhomMua", 0)
                            End If
                            AddParameter("@HSKyNang", tbHSKN.Rows(0)(0))
                            AddParameterWhere("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
                            If doUpdate("tblDuyetLuong", "Month=@Month") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                            sql = "SET DATEFORMAT DMY"
                            sql &= " SELECT (200+ISNULL(SUM(VHNhanVien.Diem),0))DiemVH,LUONG.IDNhanVien FROM LUONG "
                            sql &= " LEFT JOIN VHNhanVien ON LUONG.IDNhanVien=VHNhanVien.IDNhanVien AND month(NgayBaoCao)=" & Convert.ToInt32(cbThang.EditValue).ToString & " AND year(NgayBaoCao)=" & tbNam.EditValue
                            sql &= " WHERE LUONG.[Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "'"

                            ' If Not ShowCauHoi("Bạn có muốn cập nhật cả điểm văn hóa của phòng kỹ thuật dịch vụ hay không ?") Then
                            If Convert.ToInt32(cbThang.EditValue) < 6 And Convert.ToInt32(tbNam.EditValue) < 2014 Then
                                If Not ShowCauHoi("Thời điểm " & cbThang.EditValue & "/" & tbNam.EditValue & " sử dụng cách tính khác, bạn có muốn cập nhật điểm văn hóa cho phòng KTDV hay không ?") Then
                                    sql &= " AND LUONG.IDDepatment<>6 "
                                End If
                            End If

                            'End If

                            sql &= " GROUP BY LUONG.IDNhanVien"

                            Dim tbVH As DataTable = ExecuteSQLDataTable(sql)
                            If tbVH Is Nothing Then Throw New Exception(LoiNgoaiLe)
                            For i As Integer = 0 To tbVH.Rows.Count - 1
                                AddParameter("@DiemVH", tbVH.Rows(i)("DiemVH"))
                                AddParameterWhere("@IDNhanVien", tbVH.Rows(i)("IDNhanVien"))
                                AddParameterWhere("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
                                If doUpdate("tblTHChamCong", "IDNhanVien=@IDNhanVien AND Month=@Month") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                            Next

                            sql = " SET DATEFORMAT DMY "
                            sql &= " SELECT IDNhanVien,ISNULL(SUM(tb1.Diem),0)Diem  "
                            sql &= " FROM (SELECT LUONG.IDNhanVien,tb2.Diem "
                            sql &= " 		FROM ( select  tb.IDNhanVien,tb.IDKyNang,max(tb.NgayThi) as Ngay "
                            sql &= " 				from tblDiemThiKyNang tb "
                            sql &= " 				where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
                            sql &= " 				group by IDNhanVien,IDKyNang ) tmpTb   "
                            sql &= " 		INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"
                            sql &= " 		INNER JOIN LUONG ON LUONG.IDNhanVien=tmpTb.IDNhanVien AND LUONG.[Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND LUONG.IDDepatment<>6	"
                            sql &= " 		)tb1  "
                            sql &= " Group By IDNhanVien"

                            Dim tbKNNV As DataTable = ExecuteSQLDataTable(sql)
                            If tbKNNV Is Nothing Then Throw New Exception(LoiNgoaiLe)
                            For i As Integer = 0 To tbKNNV.Rows.Count - 1
                                AddParameter("@DiemKN", tbKNNV.Rows(i)("Diem"))
                                AddParameterWhere("@IDNhanVien", tbKNNV.Rows(i)("IDNhanVien"))
                                AddParameterWhere("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
                                If doUpdate("tblTHChamCong", "IDNhanVien=@IDNhanVien AND Month=@Month") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                            Next
                            ShowAlert("Đã lưu lại số liệu !")
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try


                    End If
                Else
                    Dim sql As String = ""
                    sql &= " Update tblTHChamCong SET tblTHChamCong.DiemVH=0,tblTHChamCong.DiemKN=0 "
                    sql &= " FROM tblTHChamCong, LUONG "
                    sql &= " WHERE tblTHChamCong.[Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND LUONG.[Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND tblTHChamCong.IDNhanVien=LUONG.IDNhanVien AND LUONG.IDDepatment<>6"

                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub btXemChiTiet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChiTiet.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then Exit Sub
        Dim f As New frmThongTinDuyetLuong
        f.ShowDialog()
    End Sub

    Private Sub btUpdateDoanhSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btUpdateDoanhSo.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If Not ShowCauHoi("Cập nhật doanh số nhóm mua ?") Then Exit Sub
        Dim sql As String = ""

        If Convert.ToInt32(cbThang.EditValue) = 3 Or Convert.ToInt32(cbThang.EditValue) = 6 Or Convert.ToInt32(cbThang.EditValue) = 9 Or Convert.ToInt32(cbThang.EditValue) = 12 Then
            sql = " SET DATEFORMAT DMY"
            sql &= " DECLARE @TongDoanhSo as Float"
            sql &= " DECLARE @TongDoanhSoSMCOmron as Float"
            sql &= " DECLARE @ChiPhiNhanCong	AS Float"
            ' -- Tổng Doanh số
            sql &= " SELECT * INTO #tbXuatKhoChinh FROM("
            sql &= " Select DISTINCT"
            sql &= "   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
            sql &= "     (PHIEUXUATKHO.Tientruocthue - PHIEUXUATKHO.TienChietKhau)* PHIEUXUATKHO.Tygia as DoanhThu, "
            sql &= "       (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia "
            sql &= " 			WHEN 0 THEN 0 "
            sql &= " 			ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) "
            sql &= " 					- ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
            sql &= " 					- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) "
            sql &= " 					- ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / "
            sql &= " 			(1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"

            sql &= " FROM PHIEUXUATKHO "
            sql &= "  LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang)  "
            sql &= "  LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
            sql &= "  LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
            sql &= "  LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
            sql &= "  WHERE Convert(datetime,PHIEUXUATKHO.NgayThang,103) >= convert(datetime,'01/" & (Convert.ToInt32(cbThang.EditValue) - 2).ToString & "/" & tbNam.EditValue & "',103)"
            sql &= " AND Convert(datetime,PHIEUXUATKHO.NgayThang,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103)  )tb WHERE PTLN >5"

            sql &= " SET @TongDoanhSo = (SELECT SUM(DoanhThu)TongDanhSo FROM #tbXuatKhoChinh)"
            sql &= " SET @TongDoanhSoSMCOmron = (SELECT SUM(TongGia)TienSMCOmron"
            sql &= " FROM"
            sql &= " (SELECT PHIEUXUATKHO.SoPhieu,(XUATKHO.Soluong * XUATKHO.Dongia* PHIEUXUATKHO.Tygia) AS TongGia"
            sql &= "     FROM XUATKHO"
            sql &= " 	INNER JOIN #tbXuatKhoChinh ON #tbXuatKhoChinh.SophieuXK = dbo.XUATKHO.Sophieu "
            sql &= " 	INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            sql &= " 	INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu AND VATTU.IDHangSanXuat IN (15,265,378,70)"
            sql &= " )tb)"

            sql &= " SET @ChiPhiNhanCong = (SELECT SUM(TongTien)TienNhanCong"
            sql &= " FROM"
            sql &= " (SELECT PHIEUXUATKHO.SoPhieu,(XUATKHOAUX.Soluong * XUATKHOAUX.Dongia* PHIEUXUATKHO.Tygia) AS TongTien"
            sql &= " FROM XUATKHOAUX"
            sql &= " 	INNER JOIN #tbXuatKhoChinh ON #tbXuatKhoChinh.SophieuXK = XUATKHOAUX.Sophieu "
            sql &= " 	INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHOAUX.SoPhieu"
            sql &= " )tb)"

            sql &= " SELECT ISNULL(( @TongDoanhSo- ISNULL(@TongDoanhSoSMCOmron,0) - ISNULL(@ChiPhiNhanCong,0)),0)"

            sql &= " DROP table #tbXuatKhoChinh"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            AddParameter("@DSNhomMua", tb.Rows(0)(0))
        Else
            AddParameter("@DSNhomMua", 0)
        End If
        AddParameterWhere("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
        If doUpdate("tblDuyetLuong", "Month=@Month") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã update doanh số nhóm mua")
        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "GhiChu" Then
            AddParameter("@GhiChu", e.Value)
            AddParameterWhere("@IDLuong", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
            If doUpdate("LUONG", "ID=@IDLuong") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thông tin ghi chú !")
            End If
        ElseIf e.Column.FieldName = "PhanHoi" Then
            AddParameter("@PhanHoi", e.Value)
            AddParameterWhere("@IDLuong", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
            If doUpdate("LUONG", "ID=@IDLuong") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thông tin phản hồi !")
            End If
        End If
    End Sub

    Private Sub btCNBangTinhThuong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCNBangTinhThuong.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If
        If ShowCauHoi("Cập nhật thông tin lương Tháng" & Convert.ToInt16(cbThang.EditValue).ToString & " vào bảng tính thưởng tết hay không ?") Then
            Dim sql As String = ""
            Dim count = 0
            Dim countEr = 0
            For i As Integer = 0 To gdvCT.RowCount - 1
                If Not gdvCT.GetRowCellValue(i, "IDNhanVien") Is Nothing Then

                    sql = ""
                    sql &= " If exists(SELECT Id FROM tblBangTinhThuong WHERE Nam=@Nam AND IdNhanVien=@IdNV)"
                    sql &= " BEGIN"
                    sql &= " 	UPDATE tblBangTinhThuong"
                    sql &= " 	Set LuongT" & Convert.ToInt16(cbThang.EditValue).ToString & "=@Luong,"
                    sql &= " 	 ThuongT" & Convert.ToInt16(cbThang.EditValue).ToString & "=@Thuong"
                    sql &= " 	WHERE Nam=@Nam AND IdNhanVien=@IdNV"
                    sql &= " END"
                    sql &= " ELSE"
                    sql &= " BEGIN"
                    sql &= " 	INSERT INTO tblBangTinhThuong(LuongT" & Convert.ToInt16(cbThang.EditValue).ToString & ",ThuongT" & Convert.ToInt16(cbThang.EditValue).ToString & ",IdNhanVien,Nam)"
                    sql &= " 	Values(@Luong,@Thuong,@IdNV,@Nam)"
                    sql &= " END"
                    AddParameter("@Nam", tbNam.EditValue)
                    AddParameter("@IdNV", gdvCT.GetRowCellValue(i, "IDNhanVien"))

                    Select Case gdvCT.GetRowCellValue(i, "MaThuong")
                        Case 8, 9, 10, 11, 17
                            AddParameter("@Luong", gdvCT.GetRowCellValue(i, "ThucChi") - gdvCT.GetRowCellValue(i, "Thuong2"))
                            AddParameter("@Thuong", gdvCT.GetRowCellValue(i, "Thuong2"))
                        Case 6, 7
                            Select Case Convert.ToInt16(cbThang.EditValue)
                                Case 3, 6, 9, 12
                                    AddParameter("@Luong", gdvCT.GetRowCellValue(i, "ThucChi") - gdvCT.GetRowCellValue(i, "Thuong2") + ((gdvCT.GetRowCellValue(i, "Diem1") / 4000) * gdvCT.GetRowCellValue(i, "LuongThuong")))
                                    AddParameter("@Thuong", gdvCT.GetRowCellValue(i, "Thuong2") - ((gdvCT.GetRowCellValue(i, "Diem1") / 4000) * gdvCT.GetRowCellValue(i, "LuongThuong")))
                                Case Else
                                    AddParameter("@Luong", gdvCT.GetRowCellValue(i, "ThucChi"))
                                    AddParameter("@Thuong", 0)
                            End Select
                        Case Else
                            AddParameter("@Luong", gdvCT.GetRowCellValue(i, "ThucChi"))
                            AddParameter("@Thuong", 0)
                    End Select

                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        countEr += 1
                    Else
                        count += 1
                    End If
                End If
            Next
            ShowThongBao("Update thành công: " & count.ToString & ", lỗi: " & countEr.ToString)
        End If
    End Sub

End Class