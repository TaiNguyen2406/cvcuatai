Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress
Imports System.Threading

Public Class frmDiemSo3

    Private Sub frmDiemSo3_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbThang.EditValue = Now.ToString("MM")
        tbNam.EditValue = Today.Year
        'colTamUngDiem.VisibleIndex = -1
        'colTamUngThangTrc.VisibleIndex = -1
        'colLoiNhuanTU.VisibleIndex = -1
        'colLNTamUngThangTrc.VisibleIndex = -1
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btNangCao.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btTyLeQuyDoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            cbTieuChi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            colLoiNhuan.Visible = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
            gdvCT.OptionsBehavior.ReadOnly = True
            colLoiNhuanTU.Visible = False
        End If
        Application.DoEvents()
        kiemTraHeSo()
        If CType(cbThang.EditValue, Integer) = Now.Month And tbNam.EditValue = Now.Year Then
            Application.DoEvents()
            TaoDuLieuMacDinhChoBangLuong()
            Application.DoEvents()
            TaoDuLieuMacDinhChoTHChamCong()
            Application.DoEvents()
        End If

        loadPhong()
        'If CType(TaiKhoan, Int32) <> 1 Then
        cbCachXem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        ' End If
        'LoadDS()
        'colTamUngDiem.VisibleIndex = -1
        'colTamUngThangTrc.VisibleIndex = -1
        'colLoiNhuanTU.VisibleIndex = -1
        'colLNTamUngThangTrc.VisibleIndex = -1
    End Sub

    Public Sub loadPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            rcbPhongBan.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbPhong.EditValue = tb.Rows(0)("ID")
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TaoDuLieuMacDinhChoTHChamCong()
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " INSERT INTO tblTHChamCong ("
        sql &= " IDNhanVien,CongThuong,CongPhep,CongNghiLe,CNLe1,CNLe2,ThemGio1,ThemGio2,ThemGio3,PC_CT_Xang,KT_DVKT_An,PCAnCa,DiemVH,DiemKN,DiemNLDV,"
        sql &= " Diem,Diem1,DoanhThu,LoiNhuan,DoanhThu1,LoiNhuan1,LoiNhuanTU,PTDiemTamUng,Month)"
        sql &= " SELECT IDNhanVien,CongThuong,CongPhep,CongNghiLe,CNLe1,CNLe2,ThemGio1,ThemGio2,ThemGio3,PC_CT_Xang,KT_DVKT_An,PCAnCa,DiemVH,DiemKN,DiemNLDV,"
        sql &= " Diem,Diem1,DoanhThu,LoiNhuan,DoanhThu1,LoiNhuan1,LoiNhuanTU,PTDiemTamUng,Thang "
        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT NHANSU.ID AS IDNhanVien,tblTHChamCong.ID,"
        sql &= " ISNULL(tblTHChamCong.CongThuong,26)CongThuong,ISNULL(tblTHChamCong.CongPhep,0)CongPhep,ISNULL(tblTHChamCong.CongNghiLe,0)CongNghiLe,"
        sql &= " ISNULL(tblTHChamCong.CNLe1,0)CNLe1,ISNULL(tblTHChamCong.CNLe2,0)CNLe2,ISNULL(tblTHChamCong.ThemGio1,0)ThemGio1,"
        sql &= " ISNULL(tblTHChamCong.ThemGio2,0)ThemGio2,ISNULL(tblTHChamCong.ThemGio3,0)ThemGio3,ISNULL(tblTHChamCong.PC_CT_Xang,0)PC_CT_Xang,"
        sql &= " ISNULL(tblTHChamCong.KT_DVKT_An,0)KT_DVKT_An,ISNULL(tblTHChamCong.PCAnCa,0)PCAnCa,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,"
        sql &= " ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,ISNULL(tblTHChamCong.Diem,0)Diem,ISNULL(tblTHChamCong.Diem1,0)Diem1,"
        sql &= " ISNULL(tblTHChamCong.DoanhThu,0)DoanhThu,ISNULL(tblTHChamCong.LoiNhuan,0)LoiNhuan,ISNULL(tblTHChamCong.DoanhThu1,0)DoanhThu1,"
        sql &= " ISNULL(tblTHChamCong.LoiNhuan1,0)LoiNhuan1,ISNULL(tblTHChamCong.LoiNhuanTU,0)LoiNhuanTU,ISNULL(tblTHChamCong.PTDiemTamUng,0)PTDiemTamUng,Right(convert(nvarchar,getdate(),103),7) as Thang"
        sql &= " FROM LUONG INNER JOIN NHANSU ON NHANSU.ID=LUONG.IDNhanVien AND NHANSU.NoiCtac=74 "
        sql &= " LEFT JOIN tblTHChamCong ON LUONG.IDNhanVien = tblTHChamCong.IDNhanVien "
        sql &= " AND tblTHChamCong.[Month]=LUONG.[Month]"
        sql &= " WHERE LUONG.[Month] =Right(convert(nvarchar,getdate(),103),7))tb"
        sql &= " WHERE ID is null"
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TaoDuLieuMacDinhChoBangLuong()
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " DECLARE @NgayCuoiThang as datetime"
        sql &= " SET @NgayCuoiThang = (SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(Datetime,'01/'+Right(convert(nvarchar,getdate(),103),7),103))+1,0)))"
        sql &= " DECLARE @ThangTruoc	as nvarchar(10)"
        sql &= " SET @ThangTruoc = (SELECT right(convert(varchar(10),Dateadd(m,-1,@NgayCuoiThang),103),7))"

        sql &= " SELECT LUONG.ID,LUONG.IDNhanVien, "
        sql &= " DateDiff(Month,NgayVaoCty,@NgayCuoiThang)ThamNien,LUONG.IDDepatment, "
        sql &= " ISNULL(LUONG.HSThuong,0)HSThuong,ISNULL(LUONG.DMThuong,0)DMThuong,Convert(float,0)ThuongDotXuat,ISNULL(LUONG.MaThuong,0)MaThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " Convert(tinyint,0) AS CTKhongHT, LUONG.PhatChiTieu INTO #tbtmp2"
        sql &= " FROM NHANSU INNER JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien "
        sql &= " 	AND LUONG.[month]=Right(convert(nvarchar,getdate(),103),7)"
        sql &= " WHERE NHANSU.NoiCtac=74"
        sql &= " SELECT * INTO #tbtmp"
        sql &= " FROM #tbtmp2"
        sql &= " UNION ALL"
        sql &= " SELECT LUONG.ID,NHANSU.ID AS IDNhanVien,"
        sql &= " DateDiff(Month,NgayVaoCty,@NgayCuoiThang)ThamNien,NHANSU.IDDepatment, "
        sql &= " ISNULL(LUONG.HSThuong,0)HSThuong,ISNULL(LUONG.DMThuong,0)DMThuong,Convert(float,0)ThuongDotXuat,ISNULL(LUONG.MaThuong,0)MaThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " Convert(tinyint,0) AS CTKhongHT, LUONG.PhatChiTieu"
        sql &= " FROM NHANSU LEFT JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien AND LUONG.ID=-1"
        sql &= " WHERE NHANSU.NoiCtac=74 AND NHANSU.ID NOT IN (SELECT IDNhanVien FROM #tbtmp2) "
        sql &= " 	AND  ((month(NgayVaoCty)= month(getdate())   AND Year(NgayVaoCty)= year(getdate()) ) OR Convert(datetime,NgayVaoCty,103)<@NgayCuoiThang)"
        sql &= " 	AND  ((month(NgayRoiCty)=  month(getdate())   AND Year(NgayRoiCty)= year(getdate()) ) OR Convert(datetime,ISNULL(NgayRoiCty,Convert(Datetime,'01/12/2060',103)),103)>@NgayCuoiThang)"

        sql &= " INSERT INTO LUONG(ThamNien,IDNhanVien,IDDepatment,LuongCB,LuongThuong,LuongBH,PCXang,PCAn,PCDienThoai,PCTrachNhiem,"
        sql &= " PC_DVKT_Luong,PC_DVKT_Xang,PC_DVKT_TrachNhiem,HSVanHoa,HSNangLuc,HSHoatDong,HSSangTao,HSThamNien,HS_BH_CongTy,"
        sql &= " HS_BH_NhanVien,HSThuong,DMThuong,ThuongDotXuat,MaThuong,ChucVu,NhomKN,CTKhongHT,PhatChiTieu,Month)"
        sql &= " SELECT "
        sql &= " ThamNien,IDNhanVien,IDDepatment,LuongCB,LuongThuong,LuongBH,PCXang,PCAn,PCDienThoai,PCTrachNhiem,"
        sql &= " PC_DVKT_Luong,PC_DVKT_Xang,PC_DVKT_TrachNhiem,HSVanHoa,HSNangLuc,HSHoatDong,HSSangTao,HSThamNien,HS_BH_CongTy,"
        sql &= " HS_BH_NhanVien,HSThuong,DMThuong,ThuongDotXuat,MaThuong,ChucVu,NhomKN,CTKhongHT,ISNULL(PhatChiTieu,0), Right(convert(nvarchar,getdate(),103),7)"
        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT #tbtmp.ID,#tbtmp.ThamNien,#tbtmp.IDNhanVien,#tbtmp.IDDepatment, "
        sql &= " 	ISNULL(tb2.LuongCB,0)LuongCB,ISNULL(tb2.LuongThuong,0)LuongThuong,ISNULL(tb2.LuongBH,0)LuongBH,ISNULL(tb2.PCXang,0)PCXang,ISNULL(tb2.PCAn,0)PCAn,ISNULL(tb2.PCDienThoai,0)PCDienThoai,ISNULL(tb2.PCTrachNhiem,0)PCTrachNhiem,"
        sql &= " 	ISNULL(tb2.PC_DVKT_Luong,0)PC_DVKT_Luong,ISNULL(tb2.PC_DVKT_Xang,0)PC_DVKT_Xang,ISNULL(tb2.PC_DVKT_TrachNhiem,0)PC_DVKT_TrachNhiem,ISNULL(tb2.HSVanHoa,0)HSVanHoa,ISNULL(tb2.HSNangLuc,0)HSNangLuc,ISNULL(tb2.HSHoatDong,0)HSHoatDong,ISNULL(tb2.HSSangTao,0)HSSangTao,ISNULL(tb2.HSThamNien,0)HSThamNien,"
        sql &= " 	ISNULL(tb2.HS_BH_CongTy,0)HS_BH_CongTy,ISNULL(tb2.HS_BH_NhanVien,0)HS_BH_NhanVien, "
        sql &= " ISNULL(tb2.HSThuong,0)HSThuong,ISNULL(tb2.DMThuong,0)DMThuong,ISNULL(tb2.ThuongDotXuat,0)ThuongDotXuat,ISNULL(tb2.MaThuong,0)MaThuong,tb2.ChucVu,tb2.NhomKN,0 as CTKhongHT,tb2.PhatChiTieu"
        sql &= " FROM #tbtmp"
        sql &= " LEFT JOIN (SELECT NHANSU.Ten AS NhanVien, DateDiff(Month,NgayVaoCty,@NgayCuoiThang)ThamNien, "
        sql &= " 	LUONG.ID,LUONG.IDNhanVien,LUONG.LuongCB,LUONG.LuongThuong,LUONG.LuongBH,LUONG.PCXang,LUONG.PCAn,LUONG.PCDienThoai,LUONG.PCTrachNhiem,"
        sql &= " 	LUONG.PC_DVKT_Luong,LUONG.PC_DVKT_Xang,LUONG.PC_DVKT_TrachNhiem,LUONG.HSVanHoa,LUONG.HSNangLuc,LUONG.HSHoatDong,LUONG.HSSangTao,LUONG.HSThamNien,"
        sql &= " 	LUONG.[Month],LUONG.HS_BH_CongTy,LUONG.HS_BH_NhanVien,LUONG.IDDepatment,"
        sql &= " LUONG.HSThuong,LUONG.DMThuong, Convert(float,0)ThuongDotXuat,LUONG.MaThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " convert(bit,0)Modify,Convert(tinyint,0) AS CTKhongHT, LUONG.PhatChiTieu "
        sql &= " FROM NHANSU INNER JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien "
        sql &= " AND LUONG.[Month]=@ThangTruoc"
        sql &= " WHERE NHANSU.NoiCtac=74 AND NHANSU.ID IN (SELECT IDNhanVien FROM #tbtmp2))tb2 ON #tbtmp.IDNhanVien=tb2.IDNhanVien"
        sql &= " )tbll WHERE ID is null"
        sql &= " DROP table #tbtmp2"
        sql &= " DROP table #tbtmp"

        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub kiemTraHeSo()
        Dim tg As DateTime = GetServerTime()
        AddParameterWhere("@Thang", tg.ToString("MM"))
        AddParameterWhere("@Nam", tg.ToString("yyyy"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam)")
        If Not tb Is Nothing Then
            If tb.Rows.Count = 0 Then
                Dim tb2 As DataTable = ExecuteSQLDataTable("SELECT TOP 1 * FROM (SELECT *,('1/'+ThangNam)ThoiGian FROM tblQuyDoi)tb ORDER BY Convert(datetime,ThoiGian,103) DESC")
                If tb2 Is Nothing Or tb2.Rows.Count = 0 Then
                    AddParameter("@ThangNam", tg.ToString("MM/yyyy"))
                    AddParameter("@TyLeQuyDoiDiem", 1)
                    AddParameter("@TyLeChuyenMa", 0.15)
                    AddParameter("@HeSoLoiNhuan", 0.21)
                    AddParameter("@HSThuCK", 0.15)
                    AddParameter("@HSLNThuongMaiToMH", 0.3)
                    AddParameter("@HSLNCongTrinhToMH", 0.06)
                Else
                    AddParameter("@ThangNam", tg.ToString("MM/yyyy"))
                    AddParameter("@TyLeQuyDoiDiem", tb2.Rows(0)("TyLeQuyDoiDiem"))
                    AddParameter("@TyLeChuyenMa", tb2.Rows(0)("TyLeChuyenMa"))
                    AddParameter("@HeSoLoiNhuan", tb2.Rows(0)("HeSoLoiNhuan"))
                    AddParameter("@HSThuCK", tb2.Rows(0)("HSThuCK"))
                    AddParameter("@HSLNThuongMaiToMH", tb2.Rows(0)("HSLNThuongMaiToMH"))
                    AddParameter("@HSLNCongTrinhToMH", tb2.Rows(0)("HSLNCongTrinhToMH"))
                End If

                If doInsert("tblQuyDoi") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub
    ' Xem theo cách xem cũ
    Public Sub LoadDiemCachCu()
        Try
            ShowWaiting("Đang tính điểm ...")
            Dim sql As String = ""

            sql &= " DECLARE @TyLeQD as float"
            sql &= " DECLARE @TyLeCM as Float"
            sql &= " DECLARE @HeSoLoiNhuan as Float"
            sql &= " DECLARE @Thang AS nvarchar(02)"
            sql &= " DECLARE @Nam AS int"
            sql &= " SET @Thang='" & cbThang.EditValue & "'"
            sql &= " SET @Nam=" & tbNam.EditValue
            sql &= " SET @TyLeQD= (SELECT TyLeQuyDoiDiem FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @TyLeCM=(SELECT TyLeChuyenMa FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @HeSoLoiNhuan=(SELECT ISNULL(HeSoLoiNhuan,0.21) FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"

            sql &= " DECLARE @table2 sysname"
            sql &= " SET @table2 = 'NLNhanVienE'"
            sql &= "  DECLARE @id_field2 sysname "
            sql &= "  SET @id_field2 = 'IDNangLuc'"
            sql &= "  DECLARE @sql2 varchar(MAX)"
            sql &= "  SET @sql2 = 'SELECT IDNhanVien,SUM(DiemNL*NLDanhSach.Diem/100)DiemNL INTO ##tbDiemNLE FROM (SELECT TOP 0 CONVERT(int,0) AS [IDNangLuc], '"
            sql &= "   +'CAST(0 AS nvarchar(10)) AS [IDNhanVien],'"
            sql &= "   +' CONVERT(Float,0) AS [DiemNL] WHERE 1=0 '+CHAR(10)"
            sql &= "  SELECT @sql2 = @sql2 + 'UNION ALL SELECT '+@id_field2+', N'''"
            sql &= "   +COLUMN_NAME+''',CONVERT(Float, '"
            sql &= "    +'['+COLUMN_NAME+']) FROM ['+@table2+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "    +CHAR(10)"
            sql &= "  FROM "
            sql &= "    INFORMATION_SCHEMA.COLUMNS"
            sql &= "  WHERE "
            sql &= "    TABLE_NAME = @table2 "
            sql &= "    AND COLUMN_NAME <> @id_field2  AND COLUMN_NAME <> convert(sysname,'ID')"
            sql &= "  ORDER BY COLUMN_NAME"
            sql &= "   SELECT @sql2 = @sql2 +')tb INNER JOIN NLDanhSach ON NLDanhSach.ID=tb.IDNangLuc where DiemNL <>0 GROUP BY IDNhanVien'"
            sql &= "  EXEC(@sql2)"

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
                Else
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= " GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If


            '-- Tính k hiệu chỉnh
            sql &= " SELECT IDTakeCare,((TienLai/TienThu)/@HeSoLoiNhuan)KHC INTO #tbKHC FROM"
            sql &= " ("

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                    sql &= " FROM View_LoiNhuan"
                Else
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(TienThu*TySuatLN)TienLai,IDTakeCare "
                    sql &= " FROM View_BCThuTien"

                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                sql &= " FROM View_LoiNhuanXK"
            End If

            sql &= " WHERE Month(Ngay)=@Thang AND Year(Ngay)=@Nam"
            If cbTinhTheo.EditValue <> "Phiếu thu" Then
                If cbTrangThai.EditValue = "Đã duyệt" Then
                    sql &= " 	AND Duyet=1 "
                End If
            End If
            sql &= " GROUP BY IDTakeCare)tbK"

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(LoiNhuanKD)LoiNhuan,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam"
                    'If cbTrangThai.EditValue = "Đã duyệt" Then
                    '    sql &= " 		AND Duyet=1 "
                    'End If
                    sql &= " GROUP BY IDTakeCare "
                Else
                    sql &= " SELECT SUM(TienThu*TySuatLN*LoiNhuanKD)LoiNhuan,IDTakeCare INTO #tbLoiNhuan FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= " GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT SUM(LoiNhuanKD)LoiNhuan,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If
            ''-- Tính HTKHC
            sql &= " SELECT ISNULL(SUM(HDNhanVien.Diem),0)DiemHTKHC,IDNhanVien INTO #tbHTKHC"
            sql &= " 		FROM HDNhanVien WHERE IDDanhSach NOT IN (35,36,37,38,39,40) "
            sql &= " AND month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " group by IDNhanVien "

            '-- Tính điểm văn hoá
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemVH,IDNhanVien INTO #tbDiemVH FROM VHNhanVien WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam GROUP BY IDNhanVien "
            '-- Tính điểm năng lực
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNL FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) > convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "
            '-- Truy vấn chính
            sql &= " SELECT *,(HTHC*KHC)DTT FROM "
            sql &= " (SELECT (0)STT, LUONG.IDNhanVien,NHANSU.ID,NHANSU.Ten,((ISNULL(LUONG.ThamNien,0)/150)*(ISNULL(LUONG.HSThamNien,0)/100))*4000 AS ThamNien,"
            sql &= " (((ISNULL(LUONG.HSVanHoa,5)/100)*4000)+ ISNULL(#tbDiemVH.DiemVH,0))DiemVH,"
            sql &= " (ISNULL(NLNVE.DiemNL,0) - ISNULL(#tbDiemNL.DiemNL,0)) AS DiemNL,"
            sql &= " LUONG.IDDepatment,(ISNULL(LUONG.HSNangLuc,5)/100)HeSoNL,"
            sql &= " ISNULL(#tbHTKHC.DiemHTKHC,0) HTKHC,"
            sql &= " Round((SELECT ISNULL(SUM(HDNhanVien.Diem),0) "
            sql &= " 		FROM HDNhanVien INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 7) "
            sql &= " 		WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " AND IDNhanVien=LUONG.IDNhanVien AND HDNhanVien.IDDanhSach IN (35,36,37,38,39,40)),0) HTHC,"
            sql &= " ISNULL(#tbKHC.KHC,1) AS KHC,Convert(float,0)HoanThanh,convert(float,0)SangTao,convert(float,0)PhanTram,Convert(float,0)Tong,(SELECT '') AS Hang,ISNULL(#tbLoiNhuan.LoiNhuan,0)LoiNhuan,ISNULL(#tbDaThu.DaThu,0)DaThu"
            sql &= " FROM LUONG INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID  "
            sql &= " 		LEFT JOIN #tbHTKHC ON #tbHTKHC.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemVH ON #tbDiemVH.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemNL ON #tbDiemNL.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbKHC ON #tbKHC.IDTakeCare=NHANSU.ID"
            sql &= " 		LEFT JOIN (SELECT Convert(int,Replace(IDNhanVien,'C',''))IDNhanVien,DiemNL FROM ##tbDiemNLE)NLNVE ON NLNVE.IDNhanVien=LUONG.IDNhanVien "
            sql &= "        LEFT JOIN #tbLoiNhuan ON #tbLoiNhuan.IDTakeCare=NHANSU.ID"
            sql &= "        LEFT JOIN #tbDaThu ON #tbDaThu.IDTakeCare=LUONG.IDNhanVien"
            sql &= " WHERE LUONG.Month= Convert(nvarchar,@Thang) + '/' + Convert(nvarchar,@Nam) ) tbDiem"
            sql &= " ORDER BY ThamNien DESC,ID"
            sql &= " DROP table #tbDaThu"
            sql &= " DROP table #tbKHC"
            sql &= " DROP table #tbLoiNhuan "
            sql &= " DROP table #DiemHTKHC "
            sql &= " DROP table #tbDiemVH "
            sql &= " DROP table #tbDiemNL "
            sql &= " DROP table ##tbDiemNLE "
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1

                tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                tb.Rows(i)("Tong") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                tb.Rows(i)("PhanTram") = Math.Round((tb.Rows(i)("Tong") / 4000) * 100, 2)
                If tb.Rows(i)("PhanTram") < 60 Then
                    tb.Rows(i)("Hang") = "A3"
                ElseIf tb.Rows(i)("PhanTram") >= 60 And tb.Rows(i)("PhanTram") < 70 Then
                    tb.Rows(i)("Hang") = "A2"
                ElseIf tb.Rows(i)("PhanTram") >= 70 And tb.Rows(i)("PhanTram") < 90 Then
                    tb.Rows(i)("Hang") = "A1"
                ElseIf tb.Rows(i)("PhanTram") >= 90 And tb.Rows(i)("PhanTram") < 100 Then
                    tb.Rows(i)("Hang") = "A"
                Else
                    tb.Rows(i)("Hang") = "A*"
                End If
            Next
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

#Region "Cách tính điểm mới"
    ' Bỏ
    Public Sub LoadDSDiemSo()
        Try
            ShowWaiting("Đang tính điểm ...")
            Dim sql As String = ""

            sql &= " DECLARE @TyLeQD as float"
            sql &= " DECLARE @TyLeCM as Float"
            sql &= " DECLARE @HeSoLoiNhuan as Float"
            sql &= " DECLARE @Thang AS nvarchar(02)"
            sql &= " DECLARE @Nam AS int"
            sql &= " SET @Thang='" & cbThang.EditValue & "'"
            sql &= " SET @Nam=" & tbNam.EditValue
            sql &= " SET @TyLeQD= (SELECT TyLeQuyDoiDiem FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @TyLeCM=(SELECT TyLeChuyenMa FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @HeSoLoiNhuan=(SELECT ISNULL(HeSoLoiNhuan,0.21) FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"

            sql &= " DECLARE @table3 sysname"
            sql &= " SET @table3 = 'NLNhanVienE'"
            sql &= "  DECLARE @id_field3 sysname "
            sql &= "  SET @id_field3 = 'IDNangLuc'"
            sql &= "  DECLARE @sql3 varchar(MAX)"
            sql &= "  SET @sql3 = 'SELECT IDNhanVien,SUM(DiemNL*NLDanhSach.Diem/100)DiemNL INTO ##tbDiemNLE FROM (SELECT TOP 0 CONVERT(int,0) AS [IDNangLuc], '"
            sql &= "   +'CAST(0 AS nvarchar(10)) AS [IDNhanVien],'"
            sql &= "   +' CONVERT(Float,0) AS [DiemNL] WHERE 1=0 '+CHAR(10)"
            sql &= "  SELECT @sql3 = @sql3 + 'UNION ALL SELECT '+@id_field3+', N'''"
            sql &= "   +COLUMN_NAME+''',CONVERT(Float, '"
            sql &= "    +'['+COLUMN_NAME+']) FROM ['+@table3+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "    +CHAR(10)"
            sql &= "  FROM "
            sql &= "    INFORMATION_SCHEMA.COLUMNS"
            sql &= "  WHERE "
            sql &= "    TABLE_NAME = @table3 "
            sql &= "    AND COLUMN_NAME <> @id_field3  AND COLUMN_NAME <> convert(sysname,'ID')"
            sql &= "  ORDER BY COLUMN_NAME"
            sql &= "   SELECT @sql3 = @sql3 +')tb INNER JOIN NLDanhSach ON NLDanhSach.ID=tb.IDNangLuc where DiemNL <>0 GROUP BY IDNhanVien'"
            sql &= "  EXEC(@sql3)"

            sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNLBCMoi FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) > convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "

            sql &= " SELECT replace(##tbDiemNLE.IDNhanVien,'C','')IDNhanVien,(##tbDiemNLE.DiemNL-ISNULL(#tbDiemNLBCMoi.DiemNL,0))DiemNL INTO #tbDiemNL"
            sql &= " FROM ##tbDiemNLE LEFT JOIN #tbDiemNLBCMoi ON ##tbDiemNLE.IDNhanVien='C'+convert(nvarchar, #tbDiemNLBCMoi.IDNhanVien)"


            sql &= " DECLARE @table sysname "
            sql &= " SET @table = 'tblDinhMucDiem'"
            sql &= " DECLARE @id_field sysname "
            sql &= " SET @id_field = 'ID'"
            sql &= " DECLARE @sql varchar(MAX)"
            sql &= " SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [GiaTri] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql = @sql + 'UNION ALL SELECT '+@id_field+',IDNoiDungCV, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table "
            sql &= "   AND COLUMN_NAME <> @id_field  AND COLUMN_NAME <> convert(sysname,'IDNoiDungCV') AND COLUMN_NAME <> convert(sysname,'HeSo')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql = @sql + ')tb'"
            sql &= "  EXEC(@sql)"
            '-- Tính điểm năng lực phòng
            sql &= " DECLARE @table2 sysname "
            sql &= " SET @table2 = 'NLDanhSach'"
            sql &= " DECLARE @id_field2 sysname "
            sql &= " SET @id_field2 = 'ID'"
            sql &= " DECLARE @sql2 varchar(MAX)"
            sql &= " SET @sql2 = 'SELECT IDPhong,SUM(Diem)DiemNL INTO ##tbNLPhong FROM  (SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(float,0) AS [Diem], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDPhong],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [TrangThai] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql2 = @sql2 + 'UNION ALL SELECT '+@id_field2+',Diem, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table2+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table2 "
            sql &= "   AND COLUMN_NAME <> @id_field2  AND COLUMN_NAME <> convert(sysname,'IDPhong') AND COLUMN_NAME <> convert(sysname,'IDNhom')"
            sql &= " 	 AND COLUMN_NAME <> convert(sysname,'IDTen')  AND COLUMN_NAME <> convert(sysname,'MoTa') AND COLUMN_NAME <> convert(sysname,'Diem')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql2 = @sql2 + ')tbl WHERE TrangThai=1 GROUP BY IDPhong'"
            sql &= " EXEC(@sql2)"

            '--Tạo bảng tạm điểm kỹ năng mới nhất của nhân viên"
            sql &= " SELECT tmpTb.*,tb2.ID,tb2.Diem "
            sql &= " INTO #tbKyNang FROM ("
            sql &= " select"
            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
            sql &= "     max(tb.NgayThi) as Ngay"
            sql &= " from"
            sql &= "     tblDiemThiKyNang tb"
            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
            sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
            sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"

            '-- Tổng hợp giá trị lao động của kỹ thuật

            sql &= " SELECT Ngay,(SoGio*(Convert(float,##tbDinhMuc.GiaTri)/100) * (Convert(float,tblTuDien.Diem)/100))SoGio,SoYC,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV "
            sql &= " INTO #GTLD FROM"
            sql &= " (SELECT tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
            sql &= "                         'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung)IDNhomCV"
            sql &= " FROM tblBaoCaoLichThiCong"
            sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
            sql &= " WHERE GiaoViec=0 "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " AND Duyet=1 "
            End If
            sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
            sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"
            '-- Tổng hợp giá trị lao động của kỹ thuật đã bao gồm điểm kỹ năng
            sql &= " SELECT *,ISNULL((SELECT SUM(Diem) FROM "
            sql &= " 		(SELECT *,(SELECT IDNhomKN FROM NLDanhSach WHERE ID=IDKyNang)IDNhomKN FROM tblDiemThiKyNang)tb2 "
            sql &= " 		WHERE tb.IDNgThucHien=tb2.IDNhanVien AND tb2.IDNhomKN=tb.IDNhomCV ),1) TongDiemKN"
            sql &= " INTO #DanhSachGTLDtmp"
            sql &= " FROM "
            sql &= " ("
            sql &= " 	SELECT SUM(SoGio)AS SoGio,IDNgThucHien,IDNhomCV,SoYC "
            sql &= " 	FROM #GTLD"
            sql &= " 	GROUP BY IDNgThucHien,IDNhomCV,SoYC) tb"
            '-- Tổng giá trị lao động của KT theo từng yêu cầu
            sql &= " SELECT Sum(Diem)Diem,SoYC "
            sql &= " INTO #TongGTLD FROM"
            sql &= "  (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
            sql &= " FROM #DanhSachGTLDtmp"
            sql &= " GROUP BY IDNgThucHien,SoYC)tbl"
            sql &= " GROUP BY SoYC"

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
                Else
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= " GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If

            '-- Tính k hiệu chỉnh
            sql &= " SELECT IDTakeCare,((TienLai/TienThu)/@HeSoLoiNhuan)KHC INTO #tbKHC FROM"
            sql &= " ("

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                    sql &= " FROM View_LoiNhuan"
                Else
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD*TienThu*TySuatLN)TienLai,IDTakeCare "
                    sql &= " FROM View_BCThuTien"

                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                sql &= " FROM View_LoiNhuanXK"
            End If

            sql &= " WHERE Month(Ngay)=@Thang AND Year(Ngay)=@Nam"
            If cbTinhTheo.EditValue <> "Phiếu thu" Then
                If cbTrangThai.EditValue = "Đã duyệt" Then
                    sql &= " 	AND Duyet=1 "
                End If
            End If

            sql &= " GROUP BY IDTakeCare)tbK"
            '-- Tính điểm chuyển mã cho kỹ thuật
            sql &= " SELECT YEUCAUDEN.SoPhieu,YEUCAUDEN.IDVatTu,V_XuatKhoCal.IDVatTu AS VTXK,IDChuyenMa,BANGYEUCAU.IDTakeCare,BANGYEUCAU.NgayThang,"
            sql &= " (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))*@TyLeQD*@TyLeCM AS Diem INTO #tbDiemCM"
            sql &= " FROM YEUCAUDEN INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=YEUCAUDEN.SoPhieu"
            sql &= " INNER JOIN View_LoiNhuan ON View_LoiNhuan.MaSoDatHang=BANGYEUCAU.SoPhieu AND Month(View_LoiNhuan.Ngay)=@Thang AND Month(View_LoiNhuan.Ngay)=@Nam"
            sql &= " INNER JOIN V_XuatKhoCal ON YEUCAUDEN.IDVatTu=V_XuatKhoCal.IDVattu AND V_XuatKhoCal.SoPhieu = View_LoiNhuan.SoPhieuXK"
            sql &= " WHERE IDChuyenMa<>BANGYEUCAU.IDTakeCare AND (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))>0 "
            '-- Tính lợi nhuận
            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT tbTmp.*,View_LoiNhuan.LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuan ON View_LoiNhuan.Masodathang=tbTmp.SoYC AND View_LoiNhuan.LoiNhuanKT >0 AND month(View_LoiNhuan.Ngay)=@Thang AND Year(View_LoiNhuan.Ngay)=@Nam"

                    sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "

                Else
                    sql &= " SELECT tbTmp.*,(View_BCThuTien.LoiNhuanKT*View_BCThuTien.TySuatLN*View_BCThuTien.TienThu)LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_BCThuTien ON View_BCThuTien.Masodathang=tbTmp.SoYC AND View_BCThuTien.LoiNhuanKT >0 AND month(View_BCThuTien.Ngay)=@Thang AND Year(View_BCThuTien.Ngay)=@Nam"
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND View_BCThuTien.Duyet=1 "
                    End If

                    sql &= " SELECT SUM(TienThu*TySuatLN*LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= "  GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT tbTmp.*,View_LoiNhuanXK.LoiNhuanKT "
                sql &= " INTO #DanhSachGTLD FROM"
                sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                sql &= " FROM #DanhSachGTLDtmp "
                sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuanXK ON View_LoiNhuanXK.Masodathang=tbTmp.SoYC AND View_LoiNhuanXK.LoiNhuanKT >0 AND month(View_LoiNhuanXK.Ngay)=@Thang AND Year(View_LoiNhuanXK.Ngay)=@Nam"

                sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If
            '-- Tính điểm KT
            sql &= " SELECT IDNgThucHien,ROUND(SUM(Diem),2) AS LoiNhuanKT, ROUND(SUM(Diem * @TyLeQD),0)Diem"
            sql &= " INTO #tbDiemKT"
            sql &= " FROM(SELECT (LoiNhuanKT*Diem)/(SELECT Diem FROM #TongGTLD WHERE #TongGTLD.SoYC=#DanhSachGTLD.SoYC)Diem,IDNgThucHien FROM #DanhSachGTLD)tb"
            sql &= " GROUP BY IDNgThucHien"
            '-- Tính HTKHC
            sql &= " SELECT ISNULL(SUM(HDNhanVien.Diem),0)DiemHTKHC,IDNhanVien INTO #tbHTKHC"
            sql &= " 		FROM HDNhanVien WHERE IDDanhSach NOT IN (35,36,37,38,39,40) "
            sql &= " AND month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " group by IDNhanVien "

            '-- Tính điểm văn hoá
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemVH,IDNhanVien INTO #tbDiemVH FROM VHNhanVien WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam GROUP BY IDNhanVien "
            '-- Tính điểm năng lực
            'sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNL FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "
            '-- Tính điểm kỹ năng
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemKN,IDNhanVien INTO #tbDiemKN FROM #tbKyNang WHERE convert(datetime,Ngay,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) GROUP BY IDNhanVien "
            '-- Truy vấn chính
            sql &= " SELECT *,(HTHC*KHC)DTT,(ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))LoiNhuan FROM "
            sql &= " (SELECT (0)STT, LUONG.IDNhanVien,NHANSU.ID,NHANSU.Ten,((ISNULL(LUONG.ThamNien,0)/300)*(ISNULL(LUONG.HSThamNien,0)/100))*4000 AS ThamNien,"
            sql &= " (((ISNULL(LUONG.HSVanHoa,5)/100)*4000)+ ISNULL(#tbDiemVH.DiemVH,0))DiemVH,"
            sql &= " ((ISNULL(#tbDiemNL.DiemNL,0)+ ISNULL(#tbDiemKN.DiemKN,0))"
            sql &= " /ISNULL((SELECT ISNULL(DiemNL,1) FROM ##tbNLPhong WHERE ##tbNLPhong.IDPhong='P'+Convert(nvarchar,LUONG.IDDepatment)),1))*ISNULL(LUONG.HSNangLuc,5)*0.01*4000 AS DiemNL,"
            sql &= " LUONG.IDDepatment,(ISNULL(LUONG.HSNangLuc,5)/100)HeSoNL,"
            sql &= " (ISNULL(#tbHTKHC.DiemHTKHC,0) + ISNULL(#tbDiemCM.Diem,0) + ISNULL(#tbDiemKT.Diem,0)) HTKHC,"
            sql &= " Round((SELECT ISNULL(SUM(HDNhanVien.Diem),0) "
            sql &= " 		FROM HDNhanVien INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 7) "
            sql &= " 		WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam AND IDNhanVien=LUONG.IDNhanVien"
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " AND HDNhanVien.IDDanhSach IN (35,36,37,38,39,40)),0) HTHC,"
            sql &= " ISNULL(#tbKHC.KHC,1) AS KHC,Convert(float,0)HoanThanh,convert(float,0)SangTao,convert(float,0)PhanTram,Convert(float,0)Tong,(SELECT '') AS Hang,#tbDiemKT.LoiNhuanKT,#tbLoiNhuan.LoiNhuanKD,ISNULL(#tbDaThu.DaThu,0)DaThu"
            sql &= " FROM LUONG INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID "
            sql &= " 		LEFT JOIN #tbDiemKT ON #tbDiemKT.IDNgThucHien=NHANSU.ID AND NHANSU.IDDepatment=2"
            sql &= " 		LEFT JOIN #tbHTKHC ON #tbHTKHC.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemVH ON #tbDiemVH.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemNL ON #tbDiemNL.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemKN ON #tbDiemKN.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbKHC ON #tbKHC.IDTakeCare=NHANSU.ID"
            sql &= " 		LEFT JOIN #tbDiemCM ON #tbDiemCM.IDChuyenMa=NHANSU.ID AND NHANSU.IDDepatment =2"
            sql &= "        LEFT JOIN #tbLoiNhuan ON #tbLoiNhuan.IDTakeCare=NHANSU.ID"
            sql &= "        LEFT JOIN #tbDaThu ON #tbDaThu.IDTakeCare=LUONG.IDNhanVien"
            sql &= " WHERE LUONG.Month= Convert(nvarchar,@Thang) + '/' + Convert(nvarchar,@Nam) ) tbDiem"
            sql &= " ORDER BY ThamNien DESC,ID"
            sql &= " DROP table #GTLD"
            sql &= " DROP table #DanhSachGTLD"
            sql &= " DROP table #DanhSachGTLDtmp"
            sql &= " DROP table #TongGTLD"
            sql &= " DROP table #tbDaThu"
            sql &= " DROP table #tbDiemKT"
            sql &= " DROP table #tbKHC"
            sql &= " DROP table #tbDiemCM"
            sql &= " DROP table ##tbDinhMuc"
            sql &= " DROP table ##tbNLPhong"
            sql &= " DROP table #tbKyNang "
            sql &= " DROP table #tbLoiNhuan "
            sql &= " DROP table #tbHTKHC "
            sql &= " DROP table #tbDiemVH "
            sql &= " DROP table #tbDiemNL "
            sql &= " DROP table #tbDiemKN "
            sql &= " DROP table ##tbDiemNLE"
            sql &= " DROP table #tbDiemNLBCMoi"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1

                tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                tb.Rows(i)("Tong") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                tb.Rows(i)("PhanTram") = Math.Round((tb.Rows(i)("Tong") / 4000) * 100, 2)
                If tb.Rows(i)("PhanTram") < 60 Then
                    tb.Rows(i)("Hang") = "D"
                ElseIf tb.Rows(i)("PhanTram") >= 60 And tb.Rows(i)("PhanTram") < 70 Then
                    tb.Rows(i)("Hang") = "C"
                ElseIf tb.Rows(i)("PhanTram") >= 70 And tb.Rows(i)("PhanTram") < 90 Then
                    tb.Rows(i)("Hang") = "B"
                ElseIf tb.Rows(i)("PhanTram") >= 90 And tb.Rows(i)("PhanTram") < 100 Then
                    tb.Rows(i)("Hang") = "A"
                Else
                    tb.Rows(i)("Hang") = "A*"
                End If
            Next
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub
    ' Bỏ
    Public Sub LoadDSDiemSoDiemKT()
        Try
            ShowWaiting("Đang tính điểm ...")
            Dim sql As String = ""

            sql &= " DECLARE @TyLeQD as float"
            sql &= " DECLARE @TyLeCM as Float"
            sql &= " DECLARE @HeSoLoiNhuan as Float"
            sql &= " DECLARE @Thang AS nvarchar(02)"
            sql &= " DECLARE @Nam AS int"
            sql &= " SET @Thang='" & cbThang.EditValue & "'"
            sql &= " SET @Nam=" & tbNam.EditValue
            sql &= " SET @TyLeQD= (SELECT TyLeQuyDoiDiem FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @TyLeCM=(SELECT TyLeChuyenMa FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @HeSoLoiNhuan=(SELECT ISNULL(HeSoLoiNhuan,0.21) FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"

            sql &= " DECLARE @table3 sysname"
            sql &= " SET @table3 = 'NLNhanVienE'"
            sql &= "  DECLARE @id_field3 sysname "
            sql &= "  SET @id_field3 = 'IDNangLuc'"
            sql &= "  DECLARE @sql3 varchar(MAX)"
            sql &= "  SET @sql3 = 'SELECT IDNhanVien,SUM(DiemNL*NLDanhSach.Diem/100)DiemNL INTO ##tbDiemNLE FROM (SELECT TOP 0 CONVERT(int,0) AS [IDNangLuc], '"
            sql &= "   +'CAST(0 AS nvarchar(10)) AS [IDNhanVien],'"
            sql &= "   +' CONVERT(Float,0) AS [DiemNL] WHERE 1=0 '+CHAR(10)"
            sql &= "  SELECT @sql3 = @sql3 + 'UNION ALL SELECT '+@id_field3+', N'''"
            sql &= "   +COLUMN_NAME+''',CONVERT(Float, '"
            sql &= "    +'['+COLUMN_NAME+']) FROM ['+@table3+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "    +CHAR(10)"
            sql &= "  FROM "
            sql &= "    INFORMATION_SCHEMA.COLUMNS"
            sql &= "  WHERE "
            sql &= "    TABLE_NAME = @table3 "
            sql &= "    AND COLUMN_NAME <> @id_field3  AND COLUMN_NAME <> convert(sysname,'ID')"
            sql &= "  ORDER BY COLUMN_NAME"
            sql &= "   SELECT @sql3 = @sql3 +')tb INNER JOIN NLDanhSach ON NLDanhSach.ID=tb.IDNangLuc where DiemNL <>0 GROUP BY IDNhanVien'"
            sql &= "  EXEC(@sql3)"

            sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNLBCMoi FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) > convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "
            sql &= " SELECT replace(##tbDiemNLE.IDNhanVien,'C','')IDNhanVien,(##tbDiemNLE.DiemNL-ISNULL(#tbDiemNLBCMoi.DiemNL,0))DiemNL INTO #tbDiemNL"
            sql &= " FROM ##tbDiemNLE LEFT JOIN #tbDiemNLBCMoi ON ##tbDiemNLE.IDNhanVien='C'+convert(nvarchar, #tbDiemNLBCMoi.IDNhanVien)"

            sql &= " DECLARE @table sysname "
            sql &= " SET @table = 'tblDinhMucDiem'"
            sql &= " DECLARE @id_field sysname "
            sql &= " SET @id_field = 'ID'"
            sql &= " DECLARE @sql varchar(MAX)"
            sql &= " SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [GiaTri] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql = @sql + 'UNION ALL SELECT '+@id_field+',IDNoiDungCV, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table "
            sql &= "   AND COLUMN_NAME <> @id_field  AND COLUMN_NAME <> convert(sysname,'IDNoiDungCV') AND COLUMN_NAME <> convert(sysname,'HeSo')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql = @sql + ')tb'"
            sql &= "  EXEC(@sql)"
            '-- Tính điểm năng lực phòng
            sql &= " DECLARE @table2 sysname "
            sql &= " SET @table2 = 'NLDanhSach'"
            sql &= " DECLARE @id_field2 sysname "
            sql &= " SET @id_field2 = 'ID'"
            sql &= " DECLARE @sql2 varchar(MAX)"
            sql &= " SET @sql2 = 'SELECT IDPhong,SUM(Diem)DiemNL INTO ##tbNLPhong FROM  (SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(float,0) AS [Diem], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDPhong],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [TrangThai] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql2 = @sql2 + 'UNION ALL SELECT '+@id_field2+',Diem, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table2+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table2 "
            sql &= "   AND COLUMN_NAME <> @id_field2  AND COLUMN_NAME <> convert(sysname,'IDPhong') AND COLUMN_NAME <> convert(sysname,'IDNhom')"
            sql &= " 	 AND COLUMN_NAME <> convert(sysname,'IDTen')  AND COLUMN_NAME <> convert(sysname,'MoTa') AND COLUMN_NAME <> convert(sysname,'Diem')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql2 = @sql2 + ')tbl WHERE TrangThai=1 GROUP BY IDPhong'"
            sql &= " EXEC(@sql2)"

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
                Else
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= " GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If

            '-- Tính k hiệu chỉnh
            sql &= " SELECT IDTakeCare,((TienLai/TienThu)/@HeSoLoiNhuan)KHC INTO #tbKHC FROM"
            sql &= " ("

            If cbTieuChi.EditValue = "Đã thu tiền" Then

                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                    sql &= " FROM View_LoiNhuan"
                Else
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD*TienThu*TySuatLN)TienLai,IDTakeCare "
                    sql &= " FROM View_BCThuTien"

                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                sql &= " FROM View_LoiNhuanXK"
            End If

            sql &= " WHERE Month(Ngay)=@Thang AND Year(Ngay)=@Nam"
            If cbTinhTheo.EditValue <> "Phiếu thu" Then
                If cbTrangThai.EditValue = "Đã duyệt" Then
                    sql &= " 	AND Duyet=1 "
                End If
            End If

            sql &= " GROUP BY IDTakeCare)tbK"
            '-- Tính điểm chuyển mã cho kỹ thuật
            sql &= " SELECT YEUCAUDEN.SoPhieu,YEUCAUDEN.IDVatTu,V_XuatKhoCal.IDVatTu AS VTXK,IDChuyenMa,BANGYEUCAU.IDTakeCare,BANGYEUCAU.NgayThang,"
            sql &= " (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))*@TyLeQD*@TyLeCM AS Diem INTO #tbDiemCM"
            sql &= " FROM YEUCAUDEN INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=YEUCAUDEN.SoPhieu"
            sql &= " INNER JOIN View_LoiNhuan ON View_LoiNhuan.MaSoDatHang=BANGYEUCAU.SoPhieu AND Month(View_LoiNhuan.Ngay)=@Thang AND Month(View_LoiNhuan.Ngay)=@Nam"
            sql &= " INNER JOIN V_XuatKhoCal ON YEUCAUDEN.IDVatTu=V_XuatKhoCal.IDVattu AND V_XuatKhoCal.SoPhieu = View_LoiNhuan.SoPhieuXK"
            sql &= " WHERE IDChuyenMa<>BANGYEUCAU.IDTakeCare AND (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))>0 "


            '-- Tính điểm kỹ thuật theo doanh thu
            sql &= " SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet"
            sql &= " INTO #GTLD FROM"
            sql &= " (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
            sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
            sql &= "                     'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,"
            sql &= " 		(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung)IDNhomCV,Duyet"
            sql &= " FROM tblBaoCaoLichThiCong"
            sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
            sql &= " WHERE GiaoViec=0 "

            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " AND Duyet=1 "
            End If

            sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
            sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"

            sql &= " DECLARE @tbKyNang table "
            sql &= " (IDNhanVien int,"
            sql &= " IDKyNang int,"
            sql &= " Ngay datetime,"
            sql &= " ID int,"
            sql &= " Diem int,"
            sql &= " IDNhomKN int,"
            sql &= " DiemChuan int)"

            sql &= " SELECT tmpTb.*,tb2.ID,tb2.Diem,NLDanhSach.IDNhomKN"
            sql &= " INTO #tbKyNang FROM ("
            sql &= " select"
            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
            sql &= "     max(tb.NgayThi) as Ngay"
            sql &= " from"
            sql &= "     tblDiemThiKyNang tb"
            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
            sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
            sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"
            sql &= " INNER JOIN NLDanhSach ON tb2.IDKyNang = NLDanhSach.ID"

            sql &= " SELECT *,ISNULL((SELECT SUM(Diem) FROM #tbKyNang "
            sql &= " 					WHERE #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien AND #GTLD.IDNhomCV=#tbKyNang.IDNhomKN ),0) TongDiemKN"
            sql &= " INTO #DanhSachGTLDtmp"
            sql &= " FROM #GTLD"

            sql &= " SELECT #DanhSachGTLDtmp.* ,##tbDinhMuc.GiaTri AS PTLoiNhuanKT,tbLoiNhuan.LoiNhuanKT,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
            sql &= " INTO #TongHop"
            sql &= " FROM "
            sql &= " (SELECT Sum(LoiNhuanKT)LoiNhuanKT,SoPhieuCG FROM View_LoiNhuan "
            sql &= "	WHERE "

            sql &= "  Month(View_LoiNhuan.Ngay)=@Thang And Year(View_LoiNhuan.Ngay)=@Nam "

            sql &= "	GROUP BY SoPhieuCG"
            sql &= "	)tbLoiNhuan"
            sql &= "  INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=tbLoiNhuan.SoPhieuCG"
            sql &= " INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=#DanhSachGTLDtmp.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=#DanhSachGTLDtmp.IDLoaiYeuCau"
            sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
            sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

            sql &= " SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNoiDung"
            sql &= "  INTO #tbMauSo"
            sql &= " FROM #TongHop"
            sql &= " GROUP BY SoCG,IDNoiDung"

            sql &= " SELECT IDNgThucHien,sum(TyLe*ISNULL(LoiNhuanKT,0))LoiNhuanKT,sum((TyLe*ISNULL(LoiNhuanKT,0)* ISNULL(@TyLeQD,0))) AS Diem"
            sql &= " INTO #tbDiemKT "
            sql &= " FROM("
            sql &= " SELECT #TongHop.*,tb.TongSoGio,"
            sql &= " (CASE WHEN #tbMauSo.MauSo = 0 THEN 0 ELSE"
            sql &= " ((#TongHop.SoGio*#TongHop.TongDiemKN) /#tbMauSo.MauSo) * (Convert(float,#TongHop.PTLoiNhuanKT)/100) END)  AS TyLe"
            sql &= " FROM #TongHop"
            sql &= " INNER JOIN "
            sql &= " (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG"
            sql &= " FROM "
            sql &= "  #TongHop"
            sql &= " GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=#TongHop.IDNhomCV AND tb.SoCG=#TongHop.SoCG"
            sql &= " INNER JOIN #tbMauSo ON #tbMauSo.SoCG=#TongHop.SoCG AND #tbMauSo.IDNoiDung=#TongHop.IDNoiDung"
            sql &= " )tb"
            sql &= " GROUP BY IDNgThucHien"



            '-- Tính lợi nhuận
            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT tbTmp.*,View_LoiNhuan.LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuan ON View_LoiNhuan.Masodathang=tbTmp.SoYC AND View_LoiNhuan.LoiNhuanKT >0 AND month(View_LoiNhuan.Ngay)=@Thang AND Year(View_LoiNhuan.Ngay)=@Nam"

                    sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "

                Else
                    sql &= " SELECT tbTmp.*,(View_BCThuTien.LoiNhuanKT*View_BCThuTien.TySuatLN*View_BCThuTien.TienThu)LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_BCThuTien ON View_BCThuTien.Masodathang=tbTmp.SoYC AND View_BCThuTien.LoiNhuanKT >0 AND month(View_BCThuTien.Ngay)=@Thang AND Year(View_BCThuTien.Ngay)=@Nam"
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND View_BCThuTien.Duyet=1 "
                    End If

                    sql &= " SELECT SUM(TienThu*TySuatLN*LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= "  GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT tbTmp.*,View_LoiNhuanXK.LoiNhuanKT "
                sql &= " INTO #DanhSachGTLD FROM"
                sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                sql &= " FROM #DanhSachGTLDtmp "
                sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuanXK ON View_LoiNhuanXK.Masodathang=tbTmp.SoYC AND View_LoiNhuanXK.LoiNhuanKT >0 AND month(View_LoiNhuanXK.Ngay)=@Thang AND Year(View_LoiNhuanXK.Ngay)=@Nam"

                sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If

            '-- Tính HTKHC
            sql &= " SELECT ISNULL(SUM(HDNhanVien.Diem),0)DiemHTKHC,IDNhanVien INTO #tbHTKHC"
            sql &= " 		FROM HDNhanVien WHERE IDDanhSach NOT IN (35,36,37,38,39,40) "
            sql &= " AND month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " group by IDNhanVien "

            '-- Tính điểm văn hoá
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemVH,IDNhanVien INTO #tbDiemVH FROM VHNhanVien WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam GROUP BY IDNhanVien "
            '-- Tính điểm năng lực
            'sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNL FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "
            '-- Tính điểm kỹ năng
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemKN,IDNhanVien INTO #tbDiemKN FROM #tbKyNang WHERE convert(datetime,Ngay,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) GROUP BY IDNhanVien "
            '-- Truy vấn chính
            sql &= " SELECT *,(HTHC*KHC)DTT,(ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))LoiNhuan FROM "
            sql &= " (SELECT (0)STT, LUONG.IDNhanVien,NHANSU.ID,NHANSU.Ten,((ISNULL(LUONG.ThamNien,0)/300)*(ISNULL(LUONG.HSThamNien,0)/100))*4000 AS ThamNien,"
            sql &= " (((ISNULL(LUONG.HSVanHoa,5)/100)*4000)+ ISNULL(#tbDiemVH.DiemVH,0))DiemVH,"
            sql &= " ((ISNULL(#tbDiemNL.DiemNL,0)+ ISNULL(#tbDiemKN.DiemKN,0))"
            sql &= " /ISNULL((SELECT ISNULL(DiemNL,1) FROM ##tbNLPhong WHERE ##tbNLPhong.IDPhong='P'+Convert(nvarchar,LUONG.IDDepatment)),1))*ISNULL(LUONG.HSNangLuc,5)*0.01*4000 AS DiemNL,"
            sql &= " LUONG.IDDepatment,(ISNULL(LUONG.HSNangLuc,5)/100)HeSoNL,"
            sql &= " (ISNULL(#tbHTKHC.DiemHTKHC,0) + ISNULL(#tbDiemCM.Diem,0) + ISNULL(#tbDiemKT.Diem,0)) HTKHC,"
            sql &= " Round((SELECT ISNULL(SUM(HDNhanVien.Diem),0) "
            sql &= " 		FROM HDNhanVien INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 7) "
            sql &= " 		WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam AND IDNhanVien=LUONG.IDNhanVien"
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " AND HDNhanVien.IDDanhSach IN (35,36,37,38,39,40)),0) HTHC,"
            sql &= " ISNULL(#tbKHC.KHC,1) AS KHC,Convert(float,0)HoanThanh,convert(float,0)SangTao,convert(float,0)PhanTram,Convert(float,0)Tong,(SELECT '') AS Hang,#tbDiemKT.LoiNhuanKT,#tbLoiNhuan.LoiNhuanKD,ISNULL(#tbDaThu.DaThu,0)DaThu"
            sql &= " FROM LUONG INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID "
            sql &= " 		LEFT JOIN #tbDiemKT ON #tbDiemKT.IDNgThucHien=NHANSU.ID AND (NHANSU.IDDepatment=2 OR NHANSU.IDDepatment=6)"
            sql &= " 		LEFT JOIN #tbHTKHC ON #tbHTKHC.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemVH ON #tbDiemVH.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemNL ON #tbDiemNL.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemKN ON #tbDiemKN.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbKHC ON #tbKHC.IDTakeCare=NHANSU.ID"
            sql &= " 		LEFT JOIN #tbDiemCM ON #tbDiemCM.IDChuyenMa=NHANSU.ID AND NHANSU.IDDepatment =2"
            sql &= "        LEFT JOIN #tbLoiNhuan ON #tbLoiNhuan.IDTakeCare=NHANSU.ID"
            sql &= "        LEFT JOIN #tbDaThu ON #tbDaThu.IDTakeCare=LUONG.IDNhanVien"
            sql &= " WHERE LUONG.Month= Convert(nvarchar,@Thang) + '/' + Convert(nvarchar,@Nam) ) tbDiem"
            sql &= " ORDER BY ThamNien DESC,ID"
            sql &= " DROP table #GTLD"
            sql &= " DROP table #DanhSachGTLD"
            sql &= " DROP table #DanhSachGTLDtmp"
            sql &= " DROP table #TongHop"
            sql &= " DROP table #MauSo"
            sql &= " DROP table #tbDaThu"
            sql &= " DROP table #tbDiemKT"
            sql &= " DROP table #tbKHC"
            sql &= " DROP table #tbDiemCM"
            sql &= " DROP table ##tbDinhMuc"
            sql &= " DROP table ##tbNLPhong"
            sql &= " DROP table #tbKyNang "
            sql &= " DROP table #tbLoiNhuan "
            sql &= " DROP table #tbHTKHC "
            sql &= " DROP table #tbDiemVH "
            sql &= " DROP table #tbDiemNL "
            sql &= " DROP table #tbDiemKN "
            sql &= " DROP table ##tbDiemNLE"
            sql &= " DROP table #tbDiemNLBCMoi"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1

                tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                tb.Rows(i)("Tong") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                tb.Rows(i)("PhanTram") = Math.Round((tb.Rows(i)("Tong") / 4000) * 100, 2)
                If tb.Rows(i)("PhanTram") < 60 Then
                    tb.Rows(i)("Hang") = "A3"
                ElseIf tb.Rows(i)("PhanTram") >= 60 And tb.Rows(i)("PhanTram") < 70 Then
                    tb.Rows(i)("Hang") = "A2"
                ElseIf tb.Rows(i)("PhanTram") >= 70 And tb.Rows(i)("PhanTram") < 90 Then
                    tb.Rows(i)("Hang") = "A1"
                ElseIf tb.Rows(i)("PhanTram") >= 90 And tb.Rows(i)("PhanTram") < 100 Then
                    tb.Rows(i)("Hang") = "A"
                Else
                    tb.Rows(i)("Hang") = "A*"
                End If
            Next
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub
    ' Đang sử dụng
    Public Sub LoadDSDiemSoDiemKTDaThuDuTien()
        Try
            ShowWaiting("Đang tính điểm ...")
            Dim sql As String = ""

            sql &= " DECLARE @TyLeQD as float"
            sql &= " DECLARE @TyLeCM as Float"
            sql &= " DECLARE @HeSoLoiNhuan as Float"
            sql &= " DECLARE @Thang AS nvarchar(02)"
            sql &= " DECLARE @Nam AS int"
            sql &= " SET @Thang='" & cbThang.EditValue & "'"
            sql &= " SET @Nam=" & tbNam.EditValue
            sql &= " SET @TyLeQD= (SELECT TyLeQuyDoiDiem FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @TyLeCM=(SELECT TyLeChuyenMa FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @HeSoLoiNhuan=(SELECT ISNULL(HeSoLoiNhuan,0.21) FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"

            sql &= " DECLARE @table3 sysname"
            sql &= " SET @table3 = 'NLNhanVienE'"
            sql &= "  DECLARE @id_field3 sysname "
            sql &= "  SET @id_field3 = 'IDNangLuc'"
            sql &= "  DECLARE @sql3 varchar(MAX)"
            sql &= "  SET @sql3 = 'SELECT IDNhanVien,SUM(DiemNL*NLDanhSach.Diem/100)DiemNL INTO ##tbDiemNLE FROM (SELECT TOP 0 CONVERT(int,0) AS [IDNangLuc], '"
            sql &= "   +'CAST(0 AS nvarchar(10)) AS [IDNhanVien],'"
            sql &= "   +' CONVERT(Float,0) AS [DiemNL] WHERE 1=0 '+CHAR(10)"
            sql &= "  SELECT @sql3 = @sql3 + 'UNION ALL SELECT '+@id_field3+', N'''"
            sql &= "   +COLUMN_NAME+''',CONVERT(Float, '"
            sql &= "    +'['+COLUMN_NAME+']) FROM ['+@table3+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "    +CHAR(10)"
            sql &= "  FROM "
            sql &= "    INFORMATION_SCHEMA.COLUMNS"
            sql &= "  WHERE "
            sql &= "    TABLE_NAME = @table3 "
            sql &= "    AND COLUMN_NAME <> @id_field3  AND COLUMN_NAME <> convert(sysname,'ID')"
            sql &= "  ORDER BY COLUMN_NAME"
            sql &= "   SELECT @sql3 = @sql3 +')tb INNER JOIN NLDanhSach ON NLDanhSach.ID=tb.IDNangLuc where DiemNL <>0 GROUP BY IDNhanVien'"
            sql &= "  EXEC(@sql3)"

            sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNLBCMoi FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) > convert(datetime,'" & DateSerial(tbNam.EditValue, cbThang.EditValue, 1) & "',103) AND Duyet=1 GROUP BY IDNhanVien "

            sql &= " SELECT replace(##tbDiemNLE.IDNhanVien,'C','')IDNhanVien,(##tbDiemNLE.DiemNL-ISNULL(#tbDiemNLBCMoi.DiemNL,0))DiemNL INTO #tbDiemNL"
            sql &= " FROM ##tbDiemNLE LEFT JOIN #tbDiemNLBCMoi ON ##tbDiemNLE.IDNhanVien='C'+convert(nvarchar, #tbDiemNLBCMoi.IDNhanVien)"


            sql &= " DECLARE @table sysname "
            sql &= " SET @table = 'tblDinhMucDiem'"
            sql &= " DECLARE @id_field sysname "
            sql &= " SET @id_field = 'ID'"
            sql &= " DECLARE @sql varchar(MAX)"
            sql &= " SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [GiaTri] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql = @sql + 'UNION ALL SELECT '+@id_field+',IDNoiDungCV, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table "
            sql &= "   AND COLUMN_NAME <> @id_field  AND COLUMN_NAME <> convert(sysname,'IDNoiDungCV') AND COLUMN_NAME <> convert(sysname,'HeSo')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql = @sql + ')tb'"
            sql &= "  EXEC(@sql)"
            '-- Tính điểm năng lực phòng
            sql &= " DECLARE @table2 sysname "
            sql &= " SET @table2 = 'NLDanhSach'"
            sql &= " DECLARE @id_field2 sysname "
            sql &= " SET @id_field2 = 'ID'"
            sql &= " DECLARE @sql2 varchar(MAX)"
            sql &= " SET @sql2 = 'SELECT IDPhong,SUM(Diem)DiemNL INTO ##tbNLPhong FROM  (SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(float,0) AS [Diem], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDPhong],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [TrangThai] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql2 = @sql2 + 'UNION ALL SELECT '+@id_field2+',Diem, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table2+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table2 "
            sql &= "   AND COLUMN_NAME <> @id_field2  AND COLUMN_NAME <> convert(sysname,'IDPhong') AND COLUMN_NAME <> convert(sysname,'IDNhom')"
            sql &= " 	 AND COLUMN_NAME <> convert(sysname,'IDTen')  AND COLUMN_NAME <> convert(sysname,'MoTa') AND COLUMN_NAME <> convert(sysname,'Diem')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql2 = @sql2 + ')tbl WHERE TrangThai=1 GROUP BY IDPhong'"
            sql &= " EXEC(@sql2)"

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
                Else
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare INTO #tbDaThu FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= " GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))DaThu,IDTakeCare INTO #tbDaThu FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If

            '-- Tính k hiệu chỉnh
            sql &= " SELECT IDTakeCare,((TienLai/TienThu)/@HeSoLoiNhuan)KHC INTO #tbKHC FROM"
            sql &= " ("

            If cbTieuChi.EditValue = "Đã thu tiền" Then

                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                    sql &= " FROM View_LoiNhuan"
                Else
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD*TienThu*TySuatLN)TienLai,IDTakeCare "
                    sql &= " FROM View_BCThuTien"

                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                sql &= " FROM View_LoiNhuanXK"
            End If

            sql &= " WHERE Month(Ngay)=@Thang AND Year(Ngay)=@Nam AND TienThu<>0 "
            If cbTinhTheo.EditValue <> "Phiếu thu" Then
                If cbTrangThai.EditValue = "Đã duyệt" Then
                    sql &= " 	AND Duyet=1 "
                End If
            End If

            sql &= " GROUP BY IDTakeCare)tbK"
            '-- Tính điểm chuyển mã cho kỹ thuật
            sql &= " SELECT YEUCAUDEN.SoPhieu,YEUCAUDEN.IDVatTu,V_XuatKhoCal.IDVatTu AS VTXK,IDChuyenMa,BANGYEUCAU.IDTakeCare,BANGYEUCAU.NgayThang,"
            sql &= " (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))*@TyLeQD*@TyLeCM AS Diem INTO #tbDiemCM"
            sql &= " FROM YEUCAUDEN INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=YEUCAUDEN.SoPhieu"
            sql &= " INNER JOIN View_LoiNhuan ON View_LoiNhuan.MaSoDatHang=BANGYEUCAU.SoPhieu AND Month(View_LoiNhuan.Ngay)=@Thang AND Month(View_LoiNhuan.Ngay)=@Nam"
            sql &= " INNER JOIN V_XuatKhoCal ON YEUCAUDEN.IDVatTu=V_XuatKhoCal.IDVattu AND V_XuatKhoCal.SoPhieu = View_LoiNhuan.SoPhieuXK"
            sql &= " WHERE IDChuyenMa<>BANGYEUCAU.IDTakeCare AND (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))>0 "


            '-- Tính điểm kỹ thuật theo doanh thu
            sql &= " SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet"
            sql &= " INTO #GTLD FROM"
            sql &= " (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
            sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
            sql &= "                     'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,"
            sql &= " 		(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung)IDNhomCV,Duyet"
            sql &= " FROM tblBaoCaoLichThiCong"
            sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
            sql &= " WHERE GiaoViec=0 "

            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " AND Duyet=1 "
            End If

            sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
            sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"

            'sql &= " INSERT INTO @tbKyNang "
            sql &= " SELECT tmpTb.*,tb2.ID,tb2.Diem,NLDanhSach.IDNhomKN"
            sql &= " INTO #tbKyNang2 FROM ("
            sql &= " select"
            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
            sql &= "     max(tb.NgayThi) as Ngay"
            sql &= " from"
            sql &= "     tblDiemThiKyNang tb"
            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
            sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
            sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"
            sql &= " INNER JOIN NLDanhSach ON tb2.IDKyNang = NLDanhSach.ID"

            sql &= " SELECT tb.IDNhanVien,tb.IDKyNang,DiemChuan,IDNhomKN,(CASE WHEN tb2.Diem IS NULL THEN tb.DiemChuan*0.5 ELSE tb2.Diem END)Diem"
            sql &= " INTO #tbKyNang FROM("
            sql &= " SELECT NHANSU.ID AS IDNhanVien,NLDanhSach.ID AS IDKyNang,NULL AS Diem,NLDanhSach.Diem AS DiemChuan,NLDanhSach.IDNhomKN"
            sql &= " FROM NHANSU LEFT JOIN NLDanhsach ON 1=1 AND NLDanhSach.Loai=1"
            sql &= " WHERE NHANSU.NoiCtac=74)tb"
            sql &= " LEFT JOIN "
            sql &= " (select"
            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
            sql &= "     max(tb.NgayThi) as Ngay"
            sql &= " from"
            sql &= "     tblDiemThiKyNang tb"
            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
            sql &= " group by IDNhanVien,IDKyNang) tmpTb ON tb.IDNhanVien=tmpTb.IDNhanVien AND tmpTb.IDKyNang=tb.IDKyNang"
            sql &= " LEFT JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"

            sql &= " SELECT *,ISNULL((SELECT SUM(Diem) FROM #tbKyNang "
            sql &= " WHERE #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien AND #GTLD.IDNhomCV=#tbKyNang.IDNhomKN ),0) TongDiemKN"
            sql &= " INTO #DanhSachGTLDtmp"
            sql &= " FROM #GTLD"

            sql &= " SELECT tb.*,PHIEUXUATKHO.SoPhieu AS SoPhieuXK"
            sql &= " INTO #tbXKThuTienTrongThang"
            sql &= " FROM"
            sql &= " ("
            sql &= " SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THU"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= "  month(NgayThangCT)=@Thang AND Year(NgayThangCT)=@Nam"
            sql &= " UNION ALL"
            sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THUNH"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " month(NgayThangCT)=@Thang AND Year(NgayThangCT)=@Nam"
            sql &= " )tb"
            sql &= " INNER JOIN PHIEUXUATKHO ON tb.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb.PhieuTC0 = PHIEUXUATKHO.SophieuCG"

            sql &= " SELECT SUM(SoTien)SoTien,PHIEUXUATKHO.SoPhieu"
            sql &= " INTO #tbLoiNhuanKTTemp"
            sql &= " FROM"
            sql &= " (SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THU"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103)"
            sql &= " UNION ALL"
            sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THUNH"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103)"
            sql &= " )tb2"
            sql &= " INNER JOIN PHIEUXUATKHO ON tb2.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb2.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
            sql &= " WHERE PHIEUXUATKHO.SoPhieu IN ( SELECT DISTINCT SoPhieuXK FROM #tbXKThuTienTrongThang)"
            sql &= " GROUP BY PHIEUXUATKHO.SoPhieu"

            sql &= " SELECT  #tbLoiNhuanKTTemp.Sotien AS TienThu, PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
            sql &= "    BANGCHAOGIA.Masodathang, PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia AS TruocThue, "
            sql &= "    PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue, #tbLoiNhuanKTTemp.Sotien, ISNULL(tb.GiaNhap, 0) AS GiaNhap, "
            sql &= "    ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0) "
            sql &= "    + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0) AS ChiChietKhau, PHIEUXUATKHO.tienchietkhau * PHIEUXUATKHO.Tygia AS TienChietKhau, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
            sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (#tbLoiNhuanKTTemp.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * (ISNULL(tblTuDien.Diem, 0) / 100) END) "
            sql &= "    AS LoiNhuanKT, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
            sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (#tbLoiNhuanKTTemp.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * ((100 - ISNULL(tblTuDien.Diem, 0)) / 100) "
            sql &= "    END) AS LoiNhuanKD, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare"
            sql &= " INTO #tbLoiNhuanKT"
            sql &= " FROM  #tbLoiNhuanKTTemp "
            sql &= " INNER JOIN PHIEUXUATKHO ON #tbLoiNhuanKTTemp.SoPhieu = PHIEUXUATKHO.Sophieu AND PHIEUXUATKHO.TienTruocThue <>0 "
            sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) "
            sql &= " 		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) "
            sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG INNER JOIN"
            sql &= "    BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null LEFT OUTER JOIN"
            sql &= "    tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID LEFT OUTER JOIN"
            sql &= "        (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
            sql &= "          FROM          V_XuatkhoCal"
            sql &= "          GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
            sql &= " WHERE abs(SoTien - (PHIEUXUATKHO.Tientruocthue+PHIEUXUATKHO.TienThue * PHIEUXUATKHO.TyGia))<=50000"

            sql &= " SELECT #DanhSachGTLDtmp.* ,##tbDinhMuc.GiaTri AS PTLoiNhuanKT,#tbLoiNhuanKT.LoiNhuanKT,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
            sql &= " INTO #TongHop"
            sql &= " FROM #tbLoiNhuanKT"
            sql &= "  INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=#tbLoiNhuanKT.SoPhieuCG"
            sql &= " INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=#DanhSachGTLDtmp.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=#DanhSachGTLDtmp.IDLoaiYeuCau"
            sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
            sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

            sql &= " SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNoiDung"
            sql &= "  INTO #tbMauSo"
            sql &= " FROM #TongHop"
            sql &= " GROUP BY SoCG,IDNoiDung"

            sql &= " SELECT IDNgThucHien,sum(TyLe*ISNULL(LoiNhuanKT,0))LoiNhuanKT,sum((TyLe*ISNULL(LoiNhuanKT,0)* ISNULL(@TyLeQD,0))) AS Diem"
            sql &= " INTO #tbDiemKT "
            sql &= " FROM("
            sql &= " SELECT #TongHop.*,tb.TongSoGio,"
            sql &= " (CASE WHEN #tbMauSo.MauSo = 0 THEN 0 ELSE"
            sql &= " ((#TongHop.SoGio*#TongHop.TongDiemKN) /#tbMauSo.MauSo) * (Convert(float,#TongHop.PTLoiNhuanKT)/100) END)  AS TyLe"
            sql &= " FROM #TongHop"
            sql &= " INNER JOIN "
            sql &= " (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG"
            sql &= " FROM "
            sql &= "  #TongHop"
            sql &= " GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=#TongHop.IDNhomCV AND tb.SoCG=#TongHop.SoCG"
            sql &= " INNER JOIN #tbMauSo ON #tbMauSo.SoCG=#TongHop.SoCG AND #tbMauSo.IDNoiDung=#TongHop.IDNoiDung"
            sql &= " )tb"
            sql &= " GROUP BY IDNgThucHien"

            '-- Tính lợi nhuận
            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT tbTmp.*,View_LoiNhuan.LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuan ON View_LoiNhuan.Masodathang=tbTmp.SoYC AND View_LoiNhuan.LoiNhuanKT >0 AND month(View_LoiNhuan.Ngay)=@Thang AND Year(View_LoiNhuan.Ngay)=@Nam"

                    sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "

                Else
                    sql &= " SELECT tbTmp.*,(View_BCThuTien.LoiNhuanKT*View_BCThuTien.TySuatLN*View_BCThuTien.TienThu)LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_BCThuTien ON View_BCThuTien.Masodathang=tbTmp.SoYC AND View_BCThuTien.LoiNhuanKT >0 AND month(View_BCThuTien.Ngay)=@Thang AND Year(View_BCThuTien.Ngay)=@Nam"
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND View_BCThuTien.Duyet=1 "
                    End If

                    sql &= " SELECT SUM(TienThu*TySuatLN*LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= "  GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT tbTmp.*,View_LoiNhuanXK.LoiNhuanKT "
                sql &= " INTO #DanhSachGTLD FROM"
                sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                sql &= " FROM #DanhSachGTLDtmp "
                sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuanXK ON View_LoiNhuanXK.Masodathang=tbTmp.SoYC AND View_LoiNhuanXK.LoiNhuanKT >0 AND month(View_LoiNhuanXK.Ngay)=@Thang AND Year(View_LoiNhuanXK.Ngay)=@Nam"

                sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If

            '-- Tính HTKHC
            sql &= " SELECT ISNULL(SUM(HDNhanVien.Diem),0)DiemHTKHC,IDNhanVien INTO #tbHTKHC"
            sql &= " 		FROM HDNhanVien WHERE IDDanhSach NOT IN (35,36,37,38,39,40) "
            sql &= " AND month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " group by IDNhanVien "

            '-- Tính điểm văn hoá
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemVH,IDNhanVien INTO #tbDiemVH FROM VHNhanVien WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam GROUP BY IDNhanVien "
            '-- Tính điểm năng lực
            'sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNL FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "
            '-- Tính điểm kỹ năng
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemKN,IDNhanVien INTO #tbDiemKN FROM #tbKyNang2 WHERE convert(datetime,Ngay,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) GROUP BY IDNhanVien "
            '-- Truy vấn chính
            sql &= " SELECT *,(HTHC*KHC)DTT,(ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))LoiNhuan, "
            sql &= " (CASE WHEN IDTC IS NULL THEN 0 ELSE (CASE WHEN LuongNV = 0 THEN 0 ELSE (((ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))*DiemThamChieu/LoiNhuanGop)+DiemVH+DiemNL+ThamNien) * (LuongThamChieu/LuongNV) END) END) AS DiemMoi,"
            sql &= " (CASE WHEN IDTC IS NULL THEN 0 ELSE (CASE WHEN LuongNV = 0 THEN 0 ELSE ((((ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))*DiemThamChieu/LoiNhuanGop)+DiemVH+DiemNL+ThamNien)/4000)*100 * (LuongThamChieu/LuongNV) END) END) AS PhanTramMoi"

            sql &= " FROM "
            sql &= " (SELECT (0)STT, LUONG.IDNhanVien,NHANSU.ID,NHANSU.Ten,((ISNULL(LUONG.ThamNien,0)/300)*(ISNULL(LUONG.HSThamNien,0)/100))*4000 AS ThamNien,"
            sql &= " (((ISNULL(LUONG.HSVanHoa,5)/100)*4000)+ ISNULL(#tbDiemVH.DiemVH,0))DiemVH,"
            sql &= " ((ISNULL(#tbDiemNL.DiemNL,0)+ ISNULL(#tbDiemKN.DiemKN,0))"
            sql &= " /ISNULL((SELECT ISNULL(DiemNL,1) FROM ##tbNLPhong WHERE ##tbNLPhong.IDPhong='P'+Convert(nvarchar,LUONG.IDDepatment)),1))*ISNULL(LUONG.HSNangLuc,5)*0.01*4000 AS DiemNL,"
            sql &= " LUONG.IDDepatment,(ISNULL(LUONG.HSNangLuc,5)/100)HeSoNL,"
            sql &= " (ISNULL(#tbHTKHC.DiemHTKHC,0) + ISNULL(#tbDiemCM.Diem,0) + ISNULL(#tbDiemKT.Diem,0)) HTKHC,"
            sql &= " Round((SELECT ISNULL(SUM(HDNhanVien.Diem),0) "
            sql &= " 		FROM HDNhanVien INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 7) AND PHIEUXUATKHO.TienTruocThue<>0 "
            sql &= " 		WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam AND IDNhanVien=LUONG.IDNhanVien"
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " AND HDNhanVien.IDDanhSach IN (35,36,37,38,39,40)),0) HTHC,"
            sql &= " ISNULL(#tbKHC.KHC,1) AS KHC,Convert(float,0)HoanThanh,convert(float,0)SangTao,convert(float,0)PhanTram,Convert(float,0)Tong,(SELECT '') AS Hang,#tbDiemKT.LoiNhuanKT,#tbLoiNhuan.LoiNhuanKD,ISNULL(#tbDaThu.DaThu,0)DaThu,"
            sql &= " ISNULL((LUONG.LuongCB + (LUONG.LuongBH*LUONG.HS_BH_CongTy) + 26*(PCXang+PCAn) + PCDienthoai),0) AS LuongNV,MaThuong,"
            sql &= " ISNULL(tblDMThuongPhong.DiemThamChieu,3500)DiemThamChieu,ISNULL(tblDMThuongPhong.LoiNhuanGop,15000000)LoiNhuanGop,ISNULL(tblDMThuongPhong.LuongThamChieu,3000000)LuongThamChieu,tblDMThuongPhong.ID AS IDTC"
            sql &= " FROM LUONG INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID "
            sql &= "        LEFT JOIN tblDMThuongPhong on tblDMThuongPhong.IDPhong= Luong.IDDepatment and Luong.[Month] = tblDMThuongPhong.[Thang]"
            sql &= " 		LEFT JOIN #tbDiemKT ON #tbDiemKT.IDNgThucHien=NHANSU.ID AND (NHANSU.IDDepatment=2 OR NHANSU.IDDepatment=6)"
            sql &= " 		LEFT JOIN #tbHTKHC ON #tbHTKHC.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemVH ON #tbDiemVH.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemNL ON #tbDiemNL.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemKN ON #tbDiemKN.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbKHC ON #tbKHC.IDTakeCare=NHANSU.ID"
            sql &= " 		LEFT JOIN #tbDiemCM ON #tbDiemCM.IDChuyenMa=NHANSU.ID AND NHANSU.IDDepatment =2"
            sql &= "        LEFT JOIN #tbLoiNhuan ON #tbLoiNhuan.IDTakeCare=NHANSU.ID"
            sql &= "        LEFT JOIN #tbDaThu ON #tbDaThu.IDTakeCare=LUONG.IDNhanVien"
            sql &= " WHERE LUONG.Month= Convert(nvarchar,@Thang) + '/' + Convert(nvarchar,@Nam) ) tbDiem"
            sql &= " ORDER BY IDDepatment, ThamNien DESC,ID"
            sql &= " DROP table #GTLD"
            sql &= " DROP table #DanhSachGTLD"
            sql &= " DROP table #DanhSachGTLDtmp"
            sql &= " DROP table #TongHop"
            sql &= " DROP table #tbMauSo"
            sql &= " DROP table #tbDaThu"
            sql &= " DROP table #tbDiemKT"
            sql &= " DROP table #tbKHC"
            sql &= " DROP table #tbDiemCM"
            sql &= " DROP table ##tbDinhMuc"
            sql &= " DROP table ##tbNLPhong"
            sql &= " DROP table #tbKyNang "
            sql &= " DROP table #tbKyNang2 "
            sql &= " DROP table #tbLoiNhuan "
            sql &= " DROP table #tbHTKHC "
            sql &= " DROP table #tbDiemVH "
            sql &= " DROP table #tbDiemNL "
            sql &= " DROP table #tbDiemKN "
            sql &= " DROP table ##tbDiemNLE"
            sql &= " DROP table #tbDiemNLBCMoi"
            sql &= " DROP table #tbXKThuTienTrongThang"
            sql &= " DROP table #tbLoiNhuanKT"
            sql &= " DROP table #tbLoiNhuanKTTemp"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1

                tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                tb.Rows(i)("Tong") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                tb.Rows(i)("PhanTram") = Math.Round((tb.Rows(i)("Tong") / 4000) * 100, 2)
                Select Case tb.Rows(i)("MaThuong")
                    Case 204, 305, 501, 2002, 3001, 4001, 400, 6002, 604
                    Case Else
                        tb.Rows(i)("DiemMoi") = tb.Rows(i)("Tong")
                        tb.Rows(i)("PhanTramMoi") = tb.Rows(i)("PhanTram")
                End Select

                If tb.Rows(i)("PhanTram") < 60 Then
                    tb.Rows(i)("Hang") = "A3"
                ElseIf tb.Rows(i)("PhanTram") >= 60 And tb.Rows(i)("PhanTram") < 70 Then
                    tb.Rows(i)("Hang") = "A2"
                ElseIf tb.Rows(i)("PhanTram") >= 70 And tb.Rows(i)("PhanTram") < 90 Then
                    tb.Rows(i)("Hang") = "A1"
                ElseIf tb.Rows(i)("PhanTram") >= 90 And tb.Rows(i)("PhanTram") < 100 Then
                    tb.Rows(i)("Hang") = "A"
                Else
                    tb.Rows(i)("Hang") = "A*"
                End If
            Next
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub
    ' Đang sửa đổi
    Public Sub LoadDSDiemSoFixNL()
        Try
            ShowWaiting("Đang tính điểm ...")
            Dim sql As String = ""

            sql &= " DECLARE @TyLeQD as float"
            sql &= " DECLARE @TyLeCM as Float"
            sql &= " DECLARE @HeSoLoiNhuan as Float"
            sql &= " DECLARE @Thang AS nvarchar(02)"
            sql &= " DECLARE @Nam AS int"
            sql &= " SET @Thang='" & cbThang.EditValue & "'"
            sql &= " SET @Nam=" & tbNam.EditValue
            sql &= " SET @TyLeQD= (SELECT TyLeQuyDoiDiem FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @TyLeCM=(SELECT TyLeChuyenMa FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
            sql &= " SET @HeSoLoiNhuan=(SELECT ISNULL(HeSoLoiNhuan,0.21) FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"

            sql &= " DECLARE @table3 sysname"
            sql &= " SET @table3 = 'NLNhanVienE'"
            sql &= "  DECLARE @id_field3 sysname "
            sql &= "  SET @id_field3 = 'IDNangLuc'"
            sql &= "  DECLARE @sql3 varchar(MAX)"
            sql &= "  SET @sql3 = 'SELECT IDNhanVien,SUM(DiemNL*NLDanhSach.Diem/100)DiemNL INTO ##tbDiemNLE FROM (SELECT TOP 0 CONVERT(int,0) AS [IDNangLuc], '"
            sql &= "   +'CAST(0 AS nvarchar(10)) AS [IDNhanVien],'"
            sql &= "   +' CONVERT(Float,0) AS [DiemNL] WHERE 1=0 '+CHAR(10)"
            sql &= "  SELECT @sql3 = @sql3 + 'UNION ALL SELECT '+@id_field3+', N'''"
            sql &= "   +COLUMN_NAME+''',CONVERT(Float, '"
            sql &= "    +'['+COLUMN_NAME+']) FROM ['+@table3+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "    +CHAR(10)"
            sql &= "  FROM "
            sql &= "    INFORMATION_SCHEMA.COLUMNS"
            sql &= "  WHERE "
            sql &= "    TABLE_NAME = @table3 "
            sql &= "    AND COLUMN_NAME <> @id_field3  AND COLUMN_NAME <> convert(sysname,'ID')"
            sql &= "  ORDER BY COLUMN_NAME"
            sql &= "   SELECT @sql3 = @sql3 +')tb INNER JOIN NLDanhSach ON NLDanhSach.ID=tb.IDNangLuc where DiemNL <>0 GROUP BY IDNhanVien'"
            sql &= "  EXEC(@sql3)"

            sql &= " DECLARE @table sysname "
            sql &= " SET @table = 'tblDinhMucDiem'"
            sql &= " DECLARE @id_field sysname "
            sql &= " SET @id_field = 'ID'"
            sql &= " DECLARE @sql varchar(MAX)"
            sql &= " SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [GiaTri] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql = @sql + 'UNION ALL SELECT '+@id_field+',IDNoiDungCV, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table "
            sql &= "   AND COLUMN_NAME <> @id_field  AND COLUMN_NAME <> convert(sysname,'IDNoiDungCV') AND COLUMN_NAME <> convert(sysname,'HeSo')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql = @sql + ')tb'"
            sql &= "  EXEC(@sql)"
            '-- Tính điểm năng lực phòng
            sql &= " DECLARE @table2 sysname "
            sql &= " SET @table2 = 'NLDanhSach'"
            sql &= " DECLARE @id_field2 sysname "
            sql &= " SET @id_field2 = 'ID'"
            sql &= " DECLARE @sql2 varchar(MAX)"
            sql &= " SET @sql2 = 'SELECT IDPhong,SUM(Diem)DiemNL INTO ##tbNLPhong FROM  (SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(float,0) AS [Diem], '"
            sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDPhong],'"
            sql &= "  +' CONVERT(sql_variant,N'''') AS [TrangThai] WHERE 1=0 '+CHAR(10)"
            sql &= " SELECT @sql2 = @sql2 + 'UNION ALL SELECT '+@id_field2+',Diem, N'''"
            sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
            sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table2+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
            sql &= "   +CHAR(10)"
            sql &= " FROM "
            sql &= "   INFORMATION_SCHEMA.COLUMNS"
            sql &= " WHERE "
            sql &= "   TABLE_NAME = @table2 "
            sql &= "   AND COLUMN_NAME <> @id_field2 AND COLUMN_NAME <> convert(sysname,'Loai')  AND COLUMN_NAME <> convert(sysname,'IDPhong') AND COLUMN_NAME <> convert(sysname,'IDNhom')"
            sql &= " 	 AND COLUMN_NAME <> convert(sysname,'IDTen')  AND COLUMN_NAME <> convert(sysname,'MoTa') AND COLUMN_NAME <> convert(sysname,'Diem')"
            sql &= " ORDER BY COLUMN_NAME"
            sql &= "  SELECT @sql2 = @sql2 + ')tbl WHERE TrangThai=1 GROUP BY IDPhong'"
            sql &= " EXEC(@sql2)"

            sql &= " DECLARE @tbDiemNL table"
            sql &= " (IDNhanVien int,DiemNL float)"


            sql &= " INSERT INTO @tbDiemNL"
            sql &= " SELECT convert(int,replace(##tbDiemNLE.IDNhanVien,'C',''))IDNhanVien,(##tbDiemNLE.DiemNL-"
            sql &= " ISNULL((SELECT ISNULL(SUM(Diem),0) FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) > convert(datetime,'" & DateSerial(tbNam.EditValue, cbThang.EditValue, 1) & "',103) AND Duyet=1 "
            sql &= " AND ##tbDiemNLE.IDNhanVien='C'+convert(nvarchar, NLNhanVien.IDNhanVien)),0)"
            sql &= " )DiemNL "
            sql &= " FROM ##tbDiemNLE "

            sql &= " DECLARE @tbDaThu table"
            sql &= " (DaThu float,IDTakeCare int)"

            sql &= " INSERT INTO @tbDaThu"
            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    'sql &= " INSERT INTO @tbDaThu"
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare  FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
                Else
                    'sql &= " INSERT INTO @tbDaThu"
                    sql &= " SELECT SUM(TienThu)DaThu,IDTakeCare  FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= " GROUP BY IDTakeCare "
                End If
            Else
                'sql &= " INSERT INTO @tbDaThu"
                sql &= " SELECT SUM((TruocThue+TienThue))DaThu,IDTakeCare FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If
            'sql &= " SELECT * INTO #tbDaThu FROM @tbDaThu"
            '-- Tính k hiệu chỉnh
            sql &= " DECLARE @tbKHC table"
            sql &= " (IDTakeCare int, KHC float)"

            sql &= " INSERT INTO @tbKHC "
            sql &= " SELECT IDTakeCare,((TienLai/TienThu)/@HeSoLoiNhuan)KHC FROM"
            sql &= " ("

            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                    sql &= " FROM View_LoiNhuan"
                Else
                    sql &= " SELECT SUM(TienThu)TienThu,SUM(LoiNhuanKD*TienThu*TySuatLN)TienLai,IDTakeCare "
                    sql &= " FROM View_BCThuTien"
                End If
            Else
                sql &= " SELECT SUM((TruocThue+TienThue))TienThu,SUM(LoiNhuanKD)TienLai,IDTakeCare "
                sql &= " FROM View_LoiNhuanXK"
            End If

            sql &= " WHERE Month(Ngay)=@Thang AND Year(Ngay)=@Nam AND TienThu<>0 "
            If cbTinhTheo.EditValue <> "Phiếu thu" Then
                If cbTrangThai.EditValue = "Đã duyệt" Then
                    sql &= " 	AND Duyet=1 "
                End If
            End If

            sql &= " GROUP BY IDTakeCare)tbK"
            '-- Tính điểm chuyển mã cho kỹ thuật

            sql &= " SELECT YEUCAUDEN.SoPhieu,YEUCAUDEN.IDVatTu,V_XuatKhoCal.IDVatTu AS VTXK,IDChuyenMa,BANGYEUCAU.IDTakeCare,BANGYEUCAU.NgayThang,"
            sql &= " (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))*@TyLeQD*@TyLeCM AS Diem INTO #tbDiemCM"
            sql &= " FROM YEUCAUDEN INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=YEUCAUDEN.SoPhieu"
            sql &= " INNER JOIN View_LoiNhuan ON View_LoiNhuan.MaSoDatHang=BANGYEUCAU.SoPhieu AND Month(View_LoiNhuan.Ngay)=@Thang AND Month(View_LoiNhuan.Ngay)=@Nam"
            sql &= " INNER JOIN V_XuatKhoCal ON YEUCAUDEN.IDVatTu=V_XuatKhoCal.IDVattu AND V_XuatKhoCal.SoPhieu = View_LoiNhuan.SoPhieuXK"
            sql &= " WHERE IDChuyenMa<>BANGYEUCAU.IDTakeCare AND (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))>0 "

            '-- Tính điểm kỹ thuật theo doanh thu
            sql &= " SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet"
            sql &= " INTO #GTLD FROM"
            sql &= " (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
            sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
            sql &= "                     'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,"
            sql &= " 		(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung)IDNhomCV,Duyet"
            sql &= " FROM tblBaoCaoLichThiCong"
            sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
            sql &= " WHERE GiaoViec=0 "

            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " AND Duyet=1 "
            End If

            sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
            sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"

            sql &= " DECLARE @tbKyNang table "
            sql &= " (IDNhanVien int,"
            sql &= " IDKyNang int,"
            sql &= " Ngay datetime,"
            sql &= " ID int,"
            sql &= " Diem int,"
            sql &= " IDNhomKN int,"
            sql &= " DiemChuan int)"

            sql &= " INSERT INTO @tbKyNang"
            sql &= " SELECT tmpTb.*,tb2.ID,tb2.Diem,NLDanhSach.IDNhomKN,NLDanhSach.Diem AS DiemChuan"
            sql &= " FROM ("
            sql &= " select"
            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
            sql &= "     max(tb.NgayThi) as Ngay"
            sql &= " from"
            sql &= "     tblDiemThiKyNang tb"
            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
            sql &= " group by IDNhanVien,IDKyNang) tmpTb  "
            sql &= " INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"
            sql &= " INNER JOIN NLDanhSach ON tb2.IDKyNang = NLDanhSach.ID"

            sql &= " SELECT tb.IDNhanVien,tb.IDKyNang,DiemChuan,IDNhomKN,(CASE WHEN tb2.Diem IS NULL THEN tb.DiemChuan*0.5 ELSE tb2.Diem END)Diem"
            sql &= " INTO #tbKyNang FROM("
            sql &= " SELECT NHANSU.ID AS IDNhanVien,NLDanhSach.ID AS IDKyNang,NULL AS Diem,NLDanhSach.Diem AS DiemChuan,NLDanhSach.IDNhomKN"
            sql &= " FROM NHANSU LEFT JOIN NLDanhsach ON 1=1 AND NLDanhSach.Loai=1"
            sql &= " WHERE NHANSU.NoiCtac=74)tb"
            sql &= " LEFT JOIN "
            sql &= " (select"
            sql &= "     tb.IDNhanVien,tb.IDKyNang,"
            sql &= "     max(tb.NgayThi) as Ngay"
            sql &= " from"
            sql &= "     tblDiemThiKyNang tb"
            sql &= " where Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) "
            sql &= " group by IDNhanVien,IDKyNang) tmpTb ON tb.IDNhanVien=tmpTb.IDNhanVien AND tmpTb.IDKyNang=tb.IDKyNang"
            sql &= " LEFT JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"


            sql &= " SELECT *,ISNULL((SELECT SUM(Diem) FROM #tbKyNang "
            sql &= " WHERE #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien AND #GTLD.IDNhomCV=#tbKyNang.IDNhomKN ),0) TongDiemKN"
            sql &= " INTO #DanhSachGTLDtmp"
            sql &= " FROM #GTLD"

            sql &= " SELECT tb.*,PHIEUXUATKHO.SoPhieu AS SoPhieuXK"
            sql &= " INTO #tbXKThuTienTrongThang"
            sql &= " FROM"
            sql &= " ("
            sql &= " SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THU"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= "  month(NgayThangCT)=@Thang AND Year(NgayThangCT)=@Nam"
            sql &= " UNION ALL"
            sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THUNH"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " month(NgayThangCT)=@Thang AND Year(NgayThangCT)=@Nam"
            sql &= " )tb"
            sql &= " INNER JOIN PHIEUXUATKHO ON tb.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb.PhieuTC0 = PHIEUXUATKHO.SophieuCG"

            sql &= " SELECT SUM(SoTien)SoTien,PHIEUXUATKHO.SoPhieu"
            sql &= " INTO #tbLoiNhuanKTTemp"
            sql &= " FROM"
            sql &= " (SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THU"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103)"
            sql &= " UNION ALL"
            sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THUNH"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103)"
            sql &= " )tb2"
            sql &= " INNER JOIN PHIEUXUATKHO ON tb2.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb2.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
            sql &= " WHERE PHIEUXUATKHO.SoPhieu IN ( SELECT DISTINCT SoPhieuXK FROM #tbXKThuTienTrongThang)"
            sql &= " GROUP BY PHIEUXUATKHO.SoPhieu"

            sql &= " SELECT  #tbLoiNhuanKTTemp.Sotien AS TienThu, PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
            sql &= "    BANGCHAOGIA.Masodathang, PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia AS TruocThue, "
            sql &= "    PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue, #tbLoiNhuanKTTemp.Sotien, ISNULL(tb.GiaNhap, 0) AS GiaNhap, "
            sql &= "    ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0) "
            sql &= "    + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0) AS ChiChietKhau, PHIEUXUATKHO.tienchietkhau * PHIEUXUATKHO.Tygia AS TienChietKhau, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
            sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (#tbLoiNhuanKTTemp.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * (ISNULL(tblTuDien.Diem, 0) / 100) END) "
            sql &= "    AS LoiNhuanKT, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
            sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (#tbLoiNhuanKTTemp.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * ((100 - ISNULL(tblTuDien.Diem, 0)) / 100) "
            sql &= "    END) AS LoiNhuanKD, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare"
            sql &= " INTO #tbLoiNhuanKT"
            sql &= " FROM  #tbLoiNhuanKTTemp "
            sql &= " INNER JOIN PHIEUXUATKHO ON #tbLoiNhuanKTTemp.SoPhieu = PHIEUXUATKHO.Sophieu AND PHIEUXUATKHO.TienTruocThue <>0 "
            sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) "
            sql &= " 		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) "
            sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG INNER JOIN"
            sql &= "    BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null LEFT OUTER JOIN"
            sql &= "    tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID LEFT OUTER JOIN"
            sql &= "        (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
            sql &= "          FROM          V_XuatkhoCal"
            sql &= "          GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
            sql &= " WHERE abs(SoTien - (PHIEUXUATKHO.Tientruocthue+PHIEUXUATKHO.TienThue * PHIEUXUATKHO.TyGia))<=50000"

            sql &= " SELECT #DanhSachGTLDtmp.* ,##tbDinhMuc.GiaTri AS PTLoiNhuanKT,#tbLoiNhuanKT.LoiNhuanKT,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
            sql &= " INTO #TongHop"
            sql &= " FROM #tbLoiNhuanKT"
            sql &= "  INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=#tbLoiNhuanKT.SoPhieuCG"
            sql &= " INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=#DanhSachGTLDtmp.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=#DanhSachGTLDtmp.IDLoaiYeuCau"
            sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
            sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

            sql &= " SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNoiDung"
            sql &= "  INTO #tbMauSo"
            sql &= " FROM #TongHop"
            sql &= " GROUP BY SoCG,IDNoiDung"

            sql &= " SELECT IDNgThucHien,sum(TyLe*ISNULL(LoiNhuanKT,0))LoiNhuanKT,sum((TyLe*ISNULL(LoiNhuanKT,0)* ISNULL(@TyLeQD,0))) AS Diem"
            sql &= " INTO #tbDiemKT "
            sql &= " FROM("
            sql &= " SELECT #TongHop.*,tb.TongSoGio,"
            sql &= " (CASE WHEN #tbMauSo.MauSo = 0 THEN 0 ELSE"
            sql &= " ((#TongHop.SoGio*#TongHop.TongDiemKN) /#tbMauSo.MauSo) * (Convert(float,#TongHop.PTLoiNhuanKT)/100) END)  AS TyLe"
            sql &= " FROM #TongHop"
            sql &= " INNER JOIN "
            sql &= " (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG"
            sql &= " FROM "
            sql &= "  #TongHop"
            sql &= " GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=#TongHop.IDNhomCV AND tb.SoCG=#TongHop.SoCG"
            sql &= " INNER JOIN #tbMauSo ON #tbMauSo.SoCG=#TongHop.SoCG AND #tbMauSo.IDNoiDung=#TongHop.IDNoiDung"
            sql &= " )tb"
            sql &= " GROUP BY IDNgThucHien"



            '-- Tính lợi nhuận
            If cbTieuChi.EditValue = "Đã thu tiền" Then
                If cbTinhTheo.EditValue = "Phiếu thu" Then
                    sql &= " SELECT tbTmp.*,View_LoiNhuan.LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuan ON View_LoiNhuan.Masodathang=tbTmp.SoYC AND View_LoiNhuan.LoiNhuanKT >0 AND month(View_LoiNhuan.Ngay)=@Thang AND Year(View_LoiNhuan.Ngay)=@Nam"

                    sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuan WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "

                Else
                    sql &= " SELECT tbTmp.*,(View_BCThuTien.LoiNhuanKT*View_BCThuTien.TySuatLN*View_BCThuTien.TienThu)LoiNhuanKT "
                    sql &= " INTO #DanhSachGTLD FROM"
                    sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                    sql &= " FROM #DanhSachGTLDtmp "
                    sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_BCThuTien ON View_BCThuTien.Masodathang=tbTmp.SoYC AND View_BCThuTien.LoiNhuanKT >0 AND month(View_BCThuTien.Ngay)=@Thang AND Year(View_BCThuTien.Ngay)=@Nam"
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND View_BCThuTien.Duyet=1 "
                    End If

                    sql &= " SELECT SUM(TienThu*TySuatLN*LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_BCThuTien WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam "
                    If cbTrangThai.EditValue = "Đã duyệt" Then
                        sql &= " 		AND Duyet=1 "
                    End If
                    sql &= "  GROUP BY IDTakeCare "
                End If
            Else
                sql &= " SELECT tbTmp.*,View_LoiNhuanXK.LoiNhuanKT "
                sql &= " INTO #DanhSachGTLD FROM"
                sql &= " (SELECT SUM(SoGio*TongDiemKN)Diem,IDNgThucHien,SoYC"
                sql &= " FROM #DanhSachGTLDtmp "
                sql &= " GROUP BY IDNgThucHien,SoYC)tbTmp INNER JOIN View_LoiNhuanXK ON View_LoiNhuanXK.Masodathang=tbTmp.SoYC AND View_LoiNhuanXK.LoiNhuanKT >0 AND month(View_LoiNhuanXK.Ngay)=@Thang AND Year(View_LoiNhuanXK.Ngay)=@Nam"

                sql &= " SELECT SUM(LoiNhuanKD)LoiNhuanKD,IDTakeCare INTO #tbLoiNhuan FROM View_LoiNhuanXK WHERE month(Ngay)=@Thang AND year(Ngay)=@Nam GROUP BY IDTakeCare "
            End If

            '-- Tính HTKHC
            sql &= " SELECT ISNULL(SUM(HDNhanVien.Diem),0)DiemHTKHC,IDNhanVien INTO #tbHTKHC"
            sql &= " 		FROM HDNhanVien WHERE IDDanhSach NOT IN (35,36,37,38,39,40) "
            sql &= " AND month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam "
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " group by IDNhanVien "

            '-- Tính điểm văn hoá
            sql &= " SELECT ISNULL(SUM(Diem),0)DiemVH,IDNhanVien INTO #tbDiemVH FROM VHNhanVien WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam GROUP BY IDNhanVien "
            '-- Tính điểm năng lực
            'sql &= " SELECT ISNULL(SUM(Diem),0)DiemNL,IDNhanVien INTO #tbDiemNL FROM NLNhanVien WHERE convert(datetime,NgayBaoCao,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) AND Duyet=1 GROUP BY IDNhanVien "
            '-- Tính điểm kỹ năng
            'sql &= " SELECT ISNULL(SUM(Diem),0)DiemKN,IDNhanVien INTO #tbDiemKN FROM @tbKyNang WHERE convert(datetime,Ngay,103) <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103) GROUP BY IDNhanVien "
            '-- Truy vấn chính

            ' ((LoiNhuan*DiemThamChieu/LoiNhuanGop)+VH+NL+Thâm niên)/4000*100*LuongThamChieu/LuongNV

            sql &= " SELECT *,(HTHC*KHC)DTT,(ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))LoiNhuan, "
            sql &= " (CASE WHEN IDTC IS NULL THEN 0 ELSE (CASE WHEN LuongNV = 0 THEN 0 ELSE (((ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))*DiemThamChieu/LoiNhuanGop)+DiemVH+DiemNL+ThamNien) * (LuongThamChieu/LuongNV) END) END) AS DiemMoi,"
            sql &= " (CASE WHEN IDTC IS NULL THEN 0 ELSE (CASE WHEN LuongNV = 0 THEN 0 ELSE ((((ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))*DiemThamChieu/LoiNhuanGop)+DiemVH+DiemNL+ThamNien)/4000)*100 * (LuongThamChieu/LuongNV) END) END) AS PhanTramMoi"
            sql &= " FROM "
            sql &= " (SELECT (0)STT, LUONG.IDNhanVien,NHANSU.ID,NHANSU.Ten,((ISNULL(LUONG.ThamNien,0)/300)*(ISNULL(LUONG.HSThamNien,0)/100))*4000 AS ThamNien,"
            sql &= " (((ISNULL(LUONG.HSVanHoa,5)/100)*4000)+ ISNULL(#tbDiemVH.DiemVH,0))DiemVH,"
            sql &= " (CASE WHEN LUONG.IDDepatment =2 or LUONG.IDDepatment =6 THEN ISNULL((SELECT SUM(Diem)/(SUM(DiemChuan)*1.0) FROM @tbKyNang WHERE [@tbKyNang].IDNhanVien=LUONG.IDNhanVien),0)*(ISNULL(LUONG.HSNangLuc,5)/100)*4000 ELSE   "
            sql &= " ISNULL((SELECT SUM(DiemNL) FROM @tbDiemNL WHERE [@tbDiemNL].IDNhanVien=LUONG.IDNhanVien),0) END) AS DiemNL,"
            sql &= " LUONG.IDDepatment,(ISNULL(LUONG.HSNangLuc,5)/100)HeSoNL,"
            sql &= " (ISNULL(#tbHTKHC.DiemHTKHC,0) + ISNULL(#tbDiemCM.Diem,0) + ISNULL(#tbDiemKT.Diem,0)) HTKHC,"
            sql &= " Round((SELECT ISNULL(SUM(HDNhanVien.Diem),0) "
            sql &= " 		FROM HDNhanVien INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 7) AND PHIEUXUATKHO.TienTruocThue<>0 "
            sql &= " 		WHERE month(NgayBaoCao)=@Thang AND year(NgayBaoCao)=@Nam AND IDNhanVien=LUONG.IDNhanVien"
            If cbTrangThai.EditValue = "Đã duyệt" Then
                sql &= " 		AND duyet=1 "
            End If
            sql &= " AND HDNhanVien.IDDanhSach IN (35,36,37,38,39,40)),0) HTHC,"
            sql &= " ISNULL((SELECT KHC FROM @tbKHC WHERE [@tbKHC].IDTakeCare = LUONG.IDNhanVien),1) KHC,"
            sql &= " Convert(float,0)HoanThanh,convert(float,0)SangTao,convert(float,0)PhanTram,Convert(float,0)Tong,(SELECT '') AS Hang,#tbDiemKT.LoiNhuanKT,#tbLoiNhuan.LoiNhuanKD,"
            sql &= " ISNULL((SELECT DaThu FROM @tbDaThu WHERE [@tbDaThu].IDTakeCare = LUONG.IDNhanVien),1) DaThu,"
            sql &= " ISNULL((LUONG.LuongCB + (LUONG.LuongBH*LUONG.HS_BH_CongTy) + 26*(PCXang+PCAn) + PCDienthoai),0) AS LuongNV,MaThuong,"
            sql &= " ISNULL(tblDMThuongPhong.DiemThamChieu,3500)DiemThamChieu,ISNULL(tblDMThuongPhong.LoiNhuanGop,15000000)LoiNhuanGop,ISNULL(tblDMThuongPhong.LuongThamChieu,3000000)LuongThamChieu,tblDMThuongPhong.ID AS IDTC"

            sql &= " FROM LUONG INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID "
            sql &= "        LEFT JOIN tblDMThuongPhong on tblDMThuongPhong.IDPhong= Luong.IDDepatment and Luong.[Month] = tblDMThuongPhong.[Thang]"
            sql &= " 		LEFT JOIN #tbDiemKT ON #tbDiemKT.IDNgThucHien=NHANSU.ID AND (NHANSU.IDDepatment=2 OR NHANSU.IDDepatment=6)"
            sql &= " 		LEFT JOIN #tbHTKHC ON #tbHTKHC.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemVH ON #tbDiemVH.IDNhanVien=LUONG.IDNhanVien "
            sql &= " 		LEFT JOIN #tbDiemCM ON #tbDiemCM.IDChuyenMa=NHANSU.ID AND NHANSU.IDDepatment =2"
            sql &= "        LEFT JOIN #tbLoiNhuan ON #tbLoiNhuan.IDTakeCare=NHANSU.ID"
            sql &= " WHERE LUONG.Month= Convert(nvarchar,@Thang) + '/' + Convert(nvarchar,@Nam) ) tbDiem"
            sql &= " ORDER BY IDDepatment, ThamNien DESC,ID"

            'sql &= " SELECT * FROM @tbKyNang"

            sql &= " DROP table #GTLD"
            sql &= " DROP table #DanhSachGTLD"
            sql &= " DROP table #DanhSachGTLDtmp"
            sql &= " DROP table #TongHop"
            sql &= " DROP table #tbMauSo"
            sql &= " DROP table #tbDiemKT"
            sql &= " DROP table #tbDiemCM"
            'sql &= " DROP table #tbDaThu"
            sql &= " DROP table ##tbDinhMuc"
            sql &= " DROP table ##tbNLPhong"
            sql &= " DROP table #tbKyNang "
            sql &= " DROP table #tbLoiNhuan "
            sql &= " DROP table #tbHTKHC "
            sql &= " DROP table #tbDiemVH "
            sql &= " DROP table ##tbDiemNLE"
            sql &= " DROP table #tbXKThuTienTrongThang"
            sql &= " DROP table #tbLoiNhuanKT"
            sql &= " DROP table #tbLoiNhuanKTTemp"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1

                tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                tb.Rows(i)("Tong") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                tb.Rows(i)("PhanTram") = Math.Round((tb.Rows(i)("Tong") / 4000) * 100, 2)
                Select Case tb.Rows(i)("MaThuong")
                    Case 204, 305, 501, 2002, 3001, 4001, 400, 6002, 604
                    Case Else
                        tb.Rows(i)("DiemMoi") = tb.Rows(i)("Tong")
                        tb.Rows(i)("PhanTramMoi") = tb.Rows(i)("PhanTram")
                End Select
                If tb.Rows(i)("PhanTramMoi") < 60 Then
                    tb.Rows(i)("Hang") = "A3"
                ElseIf tb.Rows(i)("PhanTramMoi") >= 60 And tb.Rows(i)("PhanTramMoi") < 70 Then
                    tb.Rows(i)("Hang") = "A2"
                ElseIf tb.Rows(i)("PhanTramMoi") >= 70 And tb.Rows(i)("PhanTramMoi") < 90 Then
                    tb.Rows(i)("Hang") = "A1"
                ElseIf tb.Rows(i)("PhanTramMoi") >= 90 And tb.Rows(i)("PhanTramMoi") < 100 Then
                    tb.Rows(i)("Hang") = "A"
                Else
                    tb.Rows(i)("Hang") = "A*"
                End If
            Next
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Public Sub LoadDSDiemSoSuaDiemNL()
        ShowWaiting("Đang tính điểm")
        AddParameterWhere("@Thang", cbThang.EditValue)
        AddParameterWhere("@Nam", tbNam.EditValue)
        AddParameterWhere("@TieuChi", cbTieuChi.EditValue)
        AddParameterWhere("@TinhTheo", cbTinhTheo.EditValue)
        If cbTrangThai.EditValue = "Đã duyệt" Then
            AddParameterWhere("@DaDuyet", 1)
        Else
            AddParameterWhere("@DaDuyet", 0)
        End If
        Dim tb As DataTable = ExecutePrcDataTable("prc_DiemSo")
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1

                tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                tb.Rows(i)("Tong") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                tb.Rows(i)("PhanTram") = Math.Round((tb.Rows(i)("Tong") / 4000) * 100, 2)
                Select Case tb.Rows(i)("MaThuong")
                    Case 204, 305, 501, 2002, 3001, 4001, 400, 6002, 604
                    Case Else
                        tb.Rows(i)("DiemMoi") = tb.Rows(i)("Tong")
                        tb.Rows(i)("PhanTramMoi") = tb.Rows(i)("PhanTram")
                End Select

                If tb.Rows(i)("PhanTram") < 60 Then
                    tb.Rows(i)("Hang") = "A3"
                ElseIf tb.Rows(i)("PhanTram") >= 60 And tb.Rows(i)("PhanTram") < 70 Then
                    tb.Rows(i)("Hang") = "A2"
                ElseIf tb.Rows(i)("PhanTram") >= 70 And tb.Rows(i)("PhanTram") < 90 Then
                    tb.Rows(i)("Hang") = "A1"
                ElseIf tb.Rows(i)("PhanTram") >= 90 And tb.Rows(i)("PhanTram") < 100 Then
                    tb.Rows(i)("Hang") = "A"
                Else
                    tb.Rows(i)("Hang") = "A*"
                End If
            Next
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub
#End Region

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        LoadDiemCachCu()
    End Sub

    Private Sub gdvCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvCT.RowStyle
        If gdvCT.GetRowCellValue(e.RowHandle, "IDNhanVien") = CType(TaiKhoan, Int32) Then
            Select Case gdvCT.GetRowCellValue(e.RowHandle, "Mau")
                Case "R"
                    e.Appearance.BackColor = Color.Red
                Case "Y"
                    e.Appearance.BackColor = Color.Yellow
                Case "G"
                    e.Appearance.BackColor = Color.SpringGreen
                Case "S"
                    e.Appearance.BackColor = Color.Chartreuse
            End Select
        End If
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "PhanTramLN" Then
            Select Case gdvCT.GetRowCellValue(e.RowHandle, "Mau")
                Case "R"
                    e.Appearance.BackColor = Color.Red
                Case "Y"
                    e.Appearance.BackColor = Color.Yellow
                Case "G"
                    e.Appearance.BackColor = Color.SpringGreen
                Case "S"
                    e.Appearance.BackColor = Color.Chartreuse
            End Select
        ElseIf e.Column.FieldName = "PTDiemTamUng" Or e.Column.FieldName = "PTDiemTamUngThangTruoc" Then
            If e.CellValue > 0 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        If cbTieuChi.EditValue = "Đã thu tiền" Then
            cbTinhTheo.Enabled = True
        Else
            cbTinhTheo.Enabled = False
        End If
    End Sub

    Private Sub btUpdateDiem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1
                AddParameter("@Diem", gdvCT.GetRowCellValue(i, "Tong"))
                AddParameter("@DoanhThu", gdvCT.GetRowCellValue(i, "DaThu"))
                AddParameter("@LoiNhuan", gdvCT.GetRowCellValue(i, "LoiNhuan"))
                AddParameterWhere("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next
            ShowAlert("Đã cập nhật !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Function CheckValue(ByVal _obj As Object) As Double
        If IsDBNull(_obj) Or _obj Is Nothing Then
            Return 0
        Else
            If IsNumeric(_obj) Then
                Return _obj
            Else
                Return 0
            End If
        End If
    End Function

    Private Sub btUpdateDiem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

    End Sub

    Function CheckThangLuongDuyet() As Boolean
        AddParameterWhere("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Duyet FROM tblDuyetLuong WHERE [Month]=@Month")
        If tb IsNot Nothing Then
            If tb.Rows.Count = 1 Then
                Return tb.Rows(0)(0)
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub btXemCachMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemCachMoi.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            chkXemTheoSLChot.Checked = CheckThangLuongDuyet()
        End If


        Dim _prc As String = ""
        ShowWaiting("Đang tính điểm")
        If chkXemTheoSLChot.Checked Then
            AddParameterWhere("@Thang", cbThang.EditValue)
            AddParameterWhere("@Nam", tbNam.EditValue)
            'AddParameterWhere("@TieuChi", cbTieuChi.EditValue)
            'AddParameterWhere("@TinhTheo", cbTinhTheo.EditValue)
            AddParameterWhere("@IDPhong", cbPhong.EditValue)
            Dim tb As DataTable = ExecutePrcDataTable("prc_DiemSo_Chot")
            If Not tb Is Nothing Then
                gdv.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        Else
            Dim tb As New DataTable
            If IsDBNull(cbPhong.EditValue) Or cbPhong.EditValue Is Nothing Then
                CloseWaiting()
                ShowCanhBao("Tính năng đang được xây dựng ...")
                Exit Sub

            Else

                AddParameterWhere("@Thang", cbThang.EditValue)
                AddParameterWhere("@Nam", tbNam.EditValue)

                If cbTrangThai.EditValue = "Đã duyệt" Then
                    AddParameterWhere("@DaDuyet", 1)
                Else
                    AddParameterWhere("@DaDuyet", 0)
                End If
                Select Case cbPhong.EditValue
                    Case 1
                        _prc = "prc_DiemSoPQL_T3"

                    Case 2, 6
                        If chkXemTheoSLChot.Checked Then
                            _prc = "prc_DiemSoPKT_T3_Chot"
                        Else
                            _prc = "prc_DiemSoPKT_T3"

                        End If
                        AddParameterWhere("@IDPhong", cbPhong.EditValue)

                    Case 5
                        If chkXemTheoSLChot.Checked Then
                            _prc = "prc_DiemSoTH_T3_Chot"
                        Else
                            _prc = "prc_DiemSoTH_T3"

                        End If
                        'AddParameterWhere("@TieuChi", cbTieuChi.EditValue)
                        'AddParameterWhere("@TinhTheo", cbTinhTheo.EditValue)
                        AddParameterWhere("@IDPhong", cbPhong.EditValue)
                    Case Else
                        _prc = "prc_DiemSoPKD_T3"
                        AddParameterWhere("@TieuChi", cbTieuChi.EditValue)
                        AddParameterWhere("@TinhTheo", cbTinhTheo.EditValue)
                        AddParameterWhere("@IDPhong", cbPhong.EditValue)

                End Select
                tb = ExecutePrcDataTable(_prc)
            End If


            Application.DoEvents()
            Thread.Sleep(50)
            Dim tbDiemCungUng As New DataTable
            ' CloseWaiting
            Select Case cbPhong.EditValue
                Case 1, 2, 5, 6
                Case Else
                    '(CASE WHEN IDTC IS NULL THEN 0 ELSE (CASE WHEN LuongNV = 0 THEN 0 ELSE (((ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))*DiemThamChieu/LoiNhuanGop)+DiemVH+DiemNL+ThamNien) * (LuongThamChieu/LuongNV) END) END) AS DiemMoi,
                    '(CASE WHEN IDTC IS NULL THEN 0 ELSE (CASE WHEN LuongNV = 0 THEN 0 ELSE ((((ISNULL(LoiNhuanKT,0)+ISNULL(LoiNhuanKD,0))*DiemThamChieu/LoiNhuanGop)+DiemVH+DiemNL+ThamNien)/4000)*100 * (LuongThamChieu/LuongNV) END) END) AS PhanTramMoi


                    tbDiemCungUng = SqlSelect.Select_KetQuaBaoGia(New DateTime(tbNam.EditValue, cbThang.EditValue, 1), DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, New DateTime(tbNam.EditValue, cbThang.EditValue, 1))), False, True)

            End Select


            If Not tb Is Nothing Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    tb.Rows(i)("STT") = i + 1
                    Select Case tb.Rows(i)("MaThuong")
                        Case 307
                            Dim rows() As DataRow = tbDiemCungUng.Select("IDCungUng = " & tb.Rows(i)("IDNhanVien"))
                            If rows.Length > 0 Then
                                If Not IsDBNull(tb.Rows(i)("IDTC")) Then
                                    If tb.Rows(i)("LuongNV") = 0 Then
                                        tb.Rows(i)("DiemMoi") = 0
                                    Else
                                        tb.Rows(i)("LoiNhuan") = rows(0)("LoiNhuan")
                                        tb.Rows(i)("TongLN") = ((rows(0)("LoiNhuan") * tb.Rows(i)("DiemThamChieu") / tb.Rows(i)("LoiNhuanGop")) + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("ThamNien")) * (tb.Rows(i)("LuongThamChieu") / tb.Rows(i)("LuongNV"))
                                        tb.Rows(i)("PhanTramLN") = tb.Rows(i)("TongLN") * 100 / 4000
                                    End If

                                End If
                            End If
                            'tb.Rows(i)("HTKHC") = tbDiemCungUng.Select(
                    End Select
                    tb.Rows(i)("HoanThanh") = tb.Rows(i)("HTKHC") + tb.Rows(i)("DTT")
                    tb.Rows(i)("TongHT") = tb.Rows(i)("ThamNien") + tb.Rows(i)("DiemVH") + tb.Rows(i)("DiemNL") + tb.Rows(i)("HoanThanh") + tb.Rows(i)("SangTao")
                    tb.Rows(i)("PhanTramHT") = Math.Round((tb.Rows(i)("TongHT") / 4000) * 100, 2)
                    If tb.Rows(i)("IDDepatment") <> 2 And tb.Rows(i)("IDDepatment") <> 6 Then
                        Select Case tb.Rows(i)("MaThuong")
                            Case 204, 305, 307, 501, 502, 2002, 3001, 4001, 400, 6002, 604, 100, 101, 102, 103, 104
                            Case Else
                                tb.Rows(i)("TongLN") = tb.Rows(i)("TongHT")
                                tb.Rows(i)("PhanTramLN") = tb.Rows(i)("PhanTramHT")
                        End Select
                    End If


                    If tb.Rows(i)("PhanTramLN") < 60 Then
                        tb.Rows(i)("Hang") = "A3"
                    ElseIf tb.Rows(i)("PhanTramLN") >= 60 And tb.Rows(i)("PhanTramLN") < 70 Then
                        tb.Rows(i)("Hang") = "A2"
                    ElseIf tb.Rows(i)("PhanTramLN") >= 70 And tb.Rows(i)("PhanTramLN") < 90 Then
                        tb.Rows(i)("Hang") = "A1"
                    ElseIf tb.Rows(i)("PhanTramLN") >= 90 And tb.Rows(i)("PhanTramLN") < 100 Then
                        tb.Rows(i)("Hang") = "A"
                    Else
                        tb.Rows(i)("Hang") = "A*"
                    End If

                    If tb.Rows(i)("PhanTramLN") <= tb.Rows(i)("DiemDo") Then
                        tb.Rows(i)("Mau") = "R"
                    ElseIf tb.Rows(i)("PhanTramLN") > tb.Rows(i)("DiemDo") And tb.Rows(i)("PhanTramLN") <= tb.Rows(i)("DiemVang") Then
                        tb.Rows(i)("Mau") = "Y"
                    ElseIf tb.Rows(i)("PhanTramLN") > tb.Rows(i)("DiemVang") And tb.Rows(i)("PhanTramLN") <= tb.Rows(i)("DiemXanh") Then
                        tb.Rows(i)("Mau") = "G"
                    Else
                        tb.Rows(i)("Mau") = "S"
                    End If

                Next
                gdv.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
        CloseWaiting()
        Select Case cbPhong.EditValue
            Case 2, 6
                colHTKHC.VisibleIndex = -1
                colTamUngDiem.VisibleIndex = 9
                colTamUngThangTrc.VisibleIndex = 10
                colLoiNhuanTU.VisibleIndex = 15
                colLNTamUngThangTrc.VisibleIndex = 16
                colHoanThanh.VisibleIndex = -1
            Case Else
                colHTKHC.VisibleIndex = 5
                colHoanThanh.VisibleIndex = 6
                colTamUngDiem.VisibleIndex = -1
                colTamUngDiem.VisibleIndex = -1
                colTamUngThangTrc.VisibleIndex = -1
                colLoiNhuanTU.VisibleIndex = -1
                colLNTamUngThangTrc.VisibleIndex = -1
        End Select

        Select Case cbPhong.EditValue
            Case 1
                colTongHT.VisibleIndex = 8
                colPTHT.VisibleIndex = 9
            Case Else
                colTongHT.VisibleIndex = -1
                colPTHT.VisibleIndex = -1
        End Select
    End Sub

    Private Sub btXemChiTiet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChiTiet.ItemClick
        Dim sql As String = ""
        sql &= " SELECT HDNhanVIen.ID,NgayBaoCao,NHANSU.Ten AS NhanVien, NgayNhapLieu,IdDanhSach,HDTen.Ten AS NoiDungBC,ChiTiet,SoLuong,KhoiLuong,HDNhanVien.Diem,BaoCao,HDNhanVien.PhanHoi,Duyet,IDNhanVien,HDNhanVien.IDUser,YearMonth"
        sql &= " FROM HDNhanVien"
        sql &= " INNER JOIN NHANSU ON HDNhanVien.IDNhanVien=NHANSU.ID"
        sql &= " INNER JOIN HDDanhSach ON HDDanhSach.ID=HDNhanVien.IDDanhSach"
        sql &= " INNER JOIN HDTen ON HDDanhSach.IDTen=HDTen.ID"
        sql &= " WHERE month(HDNhanVien.NgayBaoCao)=" & cbThang.EditValue & " AND Year(HDNhanVien.NgayBaoCao)= " & tbNam.EditValue.ToString
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmChiTietBaoCao
            f.gdv.DataSource = tb
            f.Show()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXemChiTietTheoPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChiTietTheoPhieuThu.ItemClick
        ShowWaiting("Đang tải nội dung...")
        Dim sql As String = ""
        sql &= " SELECT *,(LN*HSLNKT) AS LoiNhuanKT,(LN*HSLNKD) AS LoiNhuanKD FROM "
        sql &= " (SELECT    KHACHHANG.ttcMa, tbThu.NgaythangCT AS Ngay,Datediff(day,PHIEUXUATKHO.NgayThang,tbThu.NgayThangCT)SoNgay,dbo.PHIEUXUATKHO.Sophieu AS SoPhieuXK, dbo.PHIEUXUATKHO.SoPhieuCG,  "
        sql &= "     dbo.BANGCHAOGIA.MaSoDatHang,PHIEUXUATKHO.CongTrinh,tbThu.Sotien AS TienThu, "
        sql &= "     dbo.PHIEUXUATKHO.Tienthue * dbo.PHIEUXUATKHO.Tygia AS TienThue, tbThu.Sotien, ISNULL(tb.GiaNhap, 0) AS GiaNhap,"
        sql &= "     dbo.PHIEUXUATKHO.tienchietkhau * dbo.PHIEUXUATKHO.Tygia AS TienChietKhau, "
        'sql &= "     (CASE PHIEUXUATKHO.TienTruocThue WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * dbo.PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0) "
        'sql &= " 		- ISNULL(dbo.V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(dbo.V_XuatkhoChiphiUnc.ChiUNC, 0) "
        'sql &= " 		- (ISNULL(dbo.PHIEUXUATKHO.tienchietkhau, 0) * ISNULL(dbo.PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)))) "
        'sql &= "      / ((dbo.PHIEUXUATKHO.Tientruocthue + dbo.PHIEUXUATKHO.Tienthue) * dbo.PHIEUXUATKHO.Tygia) END )AS TySuatLN,"
        sql &= " 	 (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= "      - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END)) ) /((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.Tygia) END) AS TySuatLN, "

        sql &= " 	 tbThu.Sotien * (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= "      - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END)) ) /((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.Tygia) END) AS LN, "


        sql &= "    (CASE WHEN BANGCHAOGIA.CongTrinh = 1 THEN (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS, 1) = 1 "
        sql &= "    THEN KTPhanBoLoiNhuanCT.PhuTrachHopDong / 100 ELSE (KTPhanBoLoiNhuanCT.PhuTrachHopDong + KTPhanBoLoiNhuanCT.PhuTrachChaoGia) / 100 END) "
        sql &= "    ELSE 1 END) As HSLNKD,"
        sql &= "    (CASE WHEN BANGCHAOGIA.CongTrinh = 1 THEN (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS, 1) = 1 "
        sql &= "    THEN (100 - ISNULL(KTPhanBoLoiNhuanCT.PhuTrachHopDong, 0)) / 100 ELSE (100 - ISNULL(KTPhanBoLoiNhuanCT.PhuTrachHopDong, 0) "
        sql &= "    - ISNULL(KTPhanBoLoiNhuanCT.PhuTrachChaoGia, 0)) / 100 END) ELSE 0 END) AS HSLNKT,"
        sql &= " 	PHIEUXUATKHO.IDTakeCare,NHANSU.Ten AS TakeCare"
        sql &= " FROM  (SELECT     NgaythangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THU"
        sql &= "        UNION ALL"
        sql &= "        SELECT     ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THUNH) AS tbThu "
        sql &= " 	INNER JOIN PHIEUXUATKHO ON tbThu.PhieuTC1 = dbo.PHIEUXUATKHO.Sophieu OR tbThu.PhieuTC0 = dbo.PHIEUXUATKHO.SophieuCG "
        sql &= "    LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachhang "
        sql &= " 	LEFT JOIN NHANSU ON NHANSU.ID= PHIEUXUATKHO.IDTakeCare"
        sql &= " 	LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(dbo.tblQuyDoi.ThangNam, 2)) = MONTH(tbThu.NgaythangCT) AND CONVERT(int, RIGHT(dbo.tblQuyDoi.ThangNam, 4)) = YEAR(tbThu.NgaythangCT) "
        sql &= " 	INNER JOIN BANGCHAOGIA ON dbo.BANGCHAOGIA.Sophieu = dbo.PHIEUXUATKHO.SophieuCG "
        sql &= " 	INNER JOIN BANGYEUCAU ON dbo.BANGYEUCAU.Sophieu = dbo.BANGCHAOGIA.Masodathang "
        sql &= " 	LEFT OUTER JOIN dbo.KTPhanBoLoiNhuanCT ON dbo.BANGYEUCAU.IDLoaiYeuCau = dbo.KTPhanBoLoiNhuanCT.ID "
        sql &= " 	LEFT OUTER JOIN (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
        sql &= "                    FROM dbo.View_XuatKhoGiaNhapTB"
        sql &= "                    GROUP BY Sophieu) AS tb ON tb.Sophieu = dbo.PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChiphiTM ON dbo.V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChiphiUnc ON dbo.V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChietkhauTM ON dbo.V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChietkhauUNC ON dbo.V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
        sql &= " WHERE (month(tbThu.NgayThangCT)=" & cbThang.EditValue & " AND Year(tbThu.NgayThangCT)= " & tbNam.EditValue.ToString & "))tb ORDER BY SoPhieuXK"

        sql &= " SELECT * FROM ("
        sql &= " SELECT ROW_NUMBER() OVER (PARTITION BY HDNhanVIen.ID ORDER BY right(HDNhanVien.ChiTiet,9),tbThu.NgayThangCT DESC) AS STT, HDNhanVIen.ID,NgayBaoCao,NHANSU.Ten AS NhanVien,Datediff(day,PHIEUXUATKHO.NgayThang,tbThu.NgayThangCT)SoNgay, PHIEUXUATKHO.SoPhieu, NgayNhapLieu,IdDanhSach,HDTen.Ten AS NoiDungBC,ChiTiet,SoLuong,KhoiLuong,HDNhanVien.Diem,BaoCao,HDNhanVien.PhanHoi,Duyet,IDNhanVien,HDNhanVien.IDUser,YearMonth,Convert(bit,0)Modify"
        sql &= " FROM HDNhanVien"
        sql &= " INNER JOIN NHANSU ON HDNhanVien.IDNhanVien=NHANSU.ID"
        sql &= " INNER JOIN HDDanhSach ON HDDanhSach.ID=HDNhanVien.IDDanhSach"
        sql &= " INNER JOIN HDTen ON HDDanhSach.IDTen=HDTen.ID"
        sql &= " LEFT JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=right(HDNhanVien.ChiTiet,9)"
        sql &= " LEFT JOIN  (SELECT     NgaythangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THU"
        'sql &= "        WHERE month(NgayThangCT)=" & _Thang & " AND Year(NgayThangCT)= " & _Nam
        sql &= "        UNION ALL"
        sql &= "        SELECT     ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THUNH"
        ' sql &= "        WHERE month(NgayThangCT)=" & _Thang & " AND Year(NgayThangCT)= " & _Nam
        sql &= "        ) AS tbThu ON (tbThu.PhieuTC1 = PHIEUXUATKHO.SoPhieu OR tbThu.PhieuTC0=PHIEUXUATKHO.SoPhieuCG) AND HDNhanVien.KhoiLuong=tbThu.SoTien "
        sql &= " WHERE IDDanhSach IN (35,36,37,38,39,40)"
        sql &= " AND month(HDNhanVien.NgayBaoCao)=" & cbThang.EditValue & " AND Year(HDNhanVien.NgayBaoCao)= " & tbNam.EditValue.ToString
        sql &= " )tbl"
        sql &= " WHERE STT =1"
        sql &= " ORDER BY SoPhieu"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmChiTietLoiNhuan
            f.Tag = Me.Parent.Tag
            f._Thang = cbThang.EditValue
            f._Nam = tbNam.EditValue.ToString
            f.gdv.DataSource = ds.Tables(0)
            f.gdvBCThuTien.DataSource = ds.Tables(1)
            f.Text = "Chi tiết phiếu thu - báo cáo thu tiền"
            f.Show()
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXemChiTietBCThuTien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChiTietBCThuTien.ItemClick
        Dim sql As String = ""
        sql &= " SELECT HDNhanVIen.ID,NgayBaoCao,NHANSU.Ten AS NhanVien, PHIEUXUATKHO.SoPhieu, NgayNhapLieu,IdDanhSach,HDTen.Ten AS NoiDungBC,ChiTiet,SoLuong,KhoiLuong,HDNhanVien.Diem,BaoCao,HDNhanVien.PhanHoi,Duyet,IDNhanVien,HDNhanVien.IDUser,YearMonth"
        sql &= " FROM HDNhanVien"
        sql &= " INNER JOIN NHANSU ON HDNhanVien.IDNhanVien=NHANSU.ID"
        sql &= " INNER JOIN HDDanhSach ON HDDanhSach.ID=HDNhanVien.IDDanhSach"
        sql &= " INNER JOIN HDTen ON HDDanhSach.IDTen=HDTen.ID"
        sql &= " LEFT JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=right(HDNhanVien.ChiTiet,9)"
        sql &= " WHERE IDDanhSach IN (35,36,37,38,39,40)"
        sql &= " AND month(HDNhanVien.NgayBaoCao)=" & cbThang.EditValue & " AND Year(HDNhanVien.NgayBaoCao)= " & tbNam.EditValue.ToString
        sql &= " ORDER BY right(HDNhanVien.ChiTiet,7)"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmChiTietBaoCaoThuTien
            f.gdv.DataSource = tb
            f.SetDesktopLocation(0, 370)
            f.Show()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btUpdateDiemVH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1
                AddParameter("@DiemVH", gdvCT.GetRowCellValue(i, "DiemVH"))
                AddParameterWhere("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next
            ShowAlert("Đã cập nhật !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btCapNhatDMDiem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

    End Sub

    Private Sub btUpdateDiemCu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btUpdateDiemCu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1
                AddParameter("@Diem1", gdvCT.GetRowCellValue(i, "Tong"))
                AddParameter("@DoanhThu1", gdvCT.GetRowCellValue(i, "DaThu"))
                AddParameter("@LoiNhuan1", gdvCT.GetRowCellValue(i, "LoiNhuan"))
                AddParameterWhere("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next
            ShowAlert("Đã cập nhật !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    'Private Sub gdvCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvCT.CustomDrawCell
    '    If e.Column.FieldName = "PhanTramMoi" Then
    '        If e.RowHandle = 6 Then
    '            ShowAlert(1)

    '        End If
    '    End If
    'End Sub

    Private Sub gdvCT_CustomDrawRowFooterCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs) Handles gdvCT.CustomDrawRowFooterCell
        If e.Column.FieldName = "PhanTramLN" Then
            'MsgBox(gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)))
            If gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 30 Then
                e.Appearance.BackColor = Color.Red

                e.Appearance.DrawBackground(e.Graphics, e.Cache, e.Bounds)
                e.Appearance.ForeColor = Color.Black
                e.Appearance.DrawString(e.Cache, gdvCT.GetGroupSummaryDisplayText(e.RowHandle, gdvCT.GroupSummary.Item(15)), e.Bounds)
                e.Handled = True
            End If
        ElseIf e.Column.FieldName = "Hang" Then
            If gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 60 Then
                e.Appearance.DrawString(e.Cache, "A3", e.Bounds)
                'e.Appearance.ForeColor = Color.Red
            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 60 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 70 Then
                e.Appearance.DrawString(e.Cache, "A2", e.Bounds)
            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 70 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 90 Then
                e.Appearance.DrawString(e.Cache, "A1", e.Bounds)
            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 90 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 100 Then
                e.Appearance.DrawString(e.Cache, "A", e.Bounds)
            Else
                e.Appearance.DrawString(e.Cache, "A*", e.Bounds)
            End If
            e.Handled = True
        End If
    End Sub


    'Private Sub gdvCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCT.CustomSummaryCalculate

    '    If e.IsGroupSummary Then
    '        If CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "Hang" Then

    '            If gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 60 Then
    '                e.TotalValue = "A3"
    '            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 60 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 70 Then
    '                e.TotalValue = "A2"
    '            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 70 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 90 Then
    '                e.TotalValue = "A1"
    '            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 90 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 100 Then
    '                e.TotalValue = "A"
    '            Else
    '                e.TotalValue = "A*"
    '            End If
    '        End If

    '    End If
    'End Sub

    'Private Sub gdvCT_CustomSummaryExists(sender As System.Object, e As DevExpress.Data.CustomSummaryExistEventArgs) Handles gdvCT.CustomSummaryExists
    '    If e.IsGroupSummary Then
    '        If CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "Hang" Then

    '            If gdvCT.GetGroupSummaryValue(e.RowHan, gdvCT.GroupSummary.Item(15)) < 60 Then
    '                e.TotalValue = "A3"
    '            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 60 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 70 Then
    '                e.TotalValue = "A2"
    '            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 70 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 90 Then
    '                e.TotalValue = "A1"
    '            ElseIf gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) >= 90 And gdvCT.GetGroupSummaryValue(e.RowHandle, gdvCT.GroupSummary.Item(15)) < 100 Then
    '                e.TotalValue = "A"
    '            Else
    '                e.TotalValue = "A*"
    '            End If
    '        End If

    '    End If
    'End Sub

    Private Sub rcbPhongBan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhongBan.ButtonClick
        'If e.Button.Index = 1 Then
        '    cbPhong.EditValue = Nothing
        'End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        'colTamUngDiem.VisibleIndex = -1
        Select Case cbPhong.EditValue
            Case 1, 2, 6
                cbTinhTheo.Enabled = False
                If cbPhong.EditValue <> 1 Then
                    ' colTamUngDiem.VisibleIndex = 9
                End If
            Case Else
                cbTinhTheo.Enabled = True
        End Select


    End Sub


    Private Sub mTinhDiemTamUngKT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhDiemTamUngKT.ItemClick
        If ShowCauHoi("Tính điểm tạm ứng cho kỹ thuật") Then
            Dim HS As Integer = 75
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT DiemThuong FROm tblDMThuongPhong WHERE Thang='" & cbThang.EditValue & "/" & tbNam.EditValue & "' AND IDPhong=" & cbPhong.EditValue)
            If Not tb Is Nothing Then
                If tb.Rows.Count > 0 Then
                    HS = tb.Rows(0)(0)
                End If
            End If

            gdvCT.BeginUpdate()
            For i As Integer = 0 To gdvCT.RowCount - 1
                If gdvCT.GetRowCellValue(i, "PhanTramLN") < HS Then
                    gdvCT.SetRowCellValue(i, "PTDiemTamUng", HS - gdvCT.GetRowCellValue(i, "PhanTramLN"))
                End If
            Next
            gdvCT.EndUpdate()
        End If
    End Sub

    Private Sub mCapNhatDiemTamUng_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCapNhatDiemTamUng.ItemClick
        If ShowCauHoi("Lưu lại thông tin điểm tạm ứng cho " & cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString & "?") Then
            gdvCT.BeginUpdate()
            For i As Integer = 0 To gdvCT.RowCount - 1
                AddParameter("@PTDiemTamUng", gdvCT.GetRowCellValue(i, "PTDiemTamUng"))
                AddParameterWhere("@IDNV", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                AddParameterWhere("@Thang", cbThang.EditValue & "/" & tbNam.EditValue.ToString)
                If doUpdate("tblTHChamCong", "[Month]=@Thang AND IDNhanVien=@IDNV") Is Nothing Then
                    ShowBaoLoi("Lỗi dòng " & i + 1 & vbCrLf & LoiNgoaiLe)
                End If
            Next
            gdvCT.EndUpdate()
            ShowAlert("Đã thực hiện !")
        End If
    End Sub

    Private Sub btXem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

    End Sub

    Private Sub chkXemTheoSLChot_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkXemTheoSLChot.CheckedChanged
        If chkXemTheoSLChot.Checked Then
            chkXemTheoSLChot.Glyph = My.Resources.Checked
        Else
            chkXemTheoSLChot.Glyph = My.Resources.UnCheck
        End If
    End Sub


    Private Sub mChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChotSoLieu.ItemClick

        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Dim sql As String = ""
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1
                'PTDiemTamUng
                If gdvCT.GetRowCellValue(i, "IDNhanVien") Is Nothing Then Continue For
                If Not IsDBNull(gdvCT.GetRowCellValue(i, "PTDiemTamUng")) Then
                    If gdvCT.GetRowCellValue(i, "PTDiemTamUng") > 0 Then
                        AddParameter("@Diem1", gdvCT.GetRowCellValue(i, "PhanTramLN") * 40)
                    Else
                        AddParameter("@Diem1", gdvCT.GetRowCellValue(i, "TongLN"))
                    End If
                Else
                    AddParameter("@Diem1", gdvCT.GetRowCellValue(i, "TongLN"))
                End If

                AddParameter("@LoiNhuanTU", gdvCT.GetRowCellValue(i, "LNTamUng"))

                AddParameter("@DiemBC", CheckValue(gdvCT.GetRowCellValue(i, "TongHT")))
                AddParameter("@DiemVH", gdvCT.GetRowCellValue(i, "DiemVH"))
                AddParameter("@DoanhThu1", gdvCT.GetRowCellValue(i, "DaThu"))
                AddParameter("@LoiNhuan1", gdvCT.GetRowCellValue(i, "LoiNhuan"))
                AddParameter("@DiemNL", gdvCT.GetRowCellValue(i, "DiemNL"))
                AddParameter("@DiemHTKHC", gdvCT.GetRowCellValue(i, "HTKHC"))
                AddParameter("@DiemHT", gdvCT.GetRowCellValue(i, "HoanThanh"))
                AddParameter("@DiemST", gdvCT.GetRowCellValue(i, "SangTao"))
                AddParameterWhere("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Next
            ShowAlert("Đã cập nhật !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try


    End Sub


    Private Sub btChotSL2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChotSL2.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Dim sql As String = ""
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1
                'PTDiemTamUng
                If gdvCT.GetRowCellValue(i, "IDNhanVien") Is Nothing Then Continue For
                AddParameter("@DiemNL", gdvCT.GetRowCellValue(i, "DiemNL"))
                AddParameter("@DiemHTKHC", gdvCT.GetRowCellValue(i, "HTKHC"))
                AddParameter("@DiemHT", gdvCT.GetRowCellValue(i, "HoanThanh"))
                AddParameter("@DiemST", gdvCT.GetRowCellValue(i, "SangTao"))
                AddParameterWhere("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Next
            ShowAlert("Đã cập nhật !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btnChiTietLoiNhuan_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles btnChiTietLoiNhuan.ItemClick
        Dim f As frmChiTietLoiNhuan2 = New frmChiTietLoiNhuan2
        f.Dock = DockStyle.Fill
        f.Tag = f.Name
        f._Thang = cbThang.EditValue
        f._Nam = tbNam.EditValue
        Dim f2 As New DevExpress.XtraEditors.XtraForm
        f2.WindowState = FormWindowState.Maximized
        f2.Controls.Add(f)
        f2.Show()
    End Sub
End Class