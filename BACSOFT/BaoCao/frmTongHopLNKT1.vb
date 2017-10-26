Imports BACSOFT.Db.SqlHelper

Public Class frmTongHopLNKT1


    Private Sub frmTongHopBCKT_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuThang.EditValue = New DateTime(Today.Year, 1, 1)
        tbDenThang.EditValue = Today.Date
        LoadPhongBan()
    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub



    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        If Convert.ToDateTime(tbDenThang.EditValue).Year = Convert.ToDateTime(tbTuThang.EditValue).Year Then
            If cbTieuChi.EditValue = "Phiếu thu" Then
                TheoPhieuThu()
            Else
                TheoXuatKho()
            End If
        Else
            ShowCanhBao("Hiện tại chỉ áp dụng cho khoảng thời gian trong cùng 1 năm !")
        End If

    End Sub

    Public Sub TheoPhieuThu()
        ShowWaiting("Đang tải dữ liệu...")
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY "
        sql &= " DECLARE @tblNhanVien AS TABLE (IdNhanVien INT) "
        sql &= " DECLARE @tblThoiGian AS TABLE (NgayThi DATETIME)"

        sql &= "  DECLARE @table sysname "
        sql &= "  SET @table = 'tblDinhMucDiem'"
        sql &= "  DECLARE @id_field sysname "
        sql &= "  SET @id_field = 'ID'"
        sql &= "  DECLARE @sql varchar(MAX)"
        sql &= "  SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '"
        sql &= "   +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'"
        sql &= "   +' CONVERT(sql_variant,N'''') AS [PTLoiNhuanKT] WHERE 1=0 '+CHAR(10)"
        sql &= "  SELECT @sql = @sql + 'UNION ALL SELECT '+@id_field+',IDNoiDungCV, N'''"
        sql &= "   +COLUMN_NAME+''',CONVERT(sql_variant, '"
        sql &= "    +'['+COLUMN_NAME+']) FROM ['+@table+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
        sql &= "    +CHAR(10)"
        sql &= "  FROM "
        sql &= "    INFORMATION_SCHEMA.COLUMNS"
        sql &= "  WHERE "
        sql &= "    TABLE_NAME = @table "
        sql &= "    AND COLUMN_NAME <> @id_field  AND COLUMN_NAME <> convert(sysname,'IDNoiDungCV') AND COLUMN_NAME <> convert(sysname,'HeSo')"
        sql &= "  ORDER BY COLUMN_NAME"
        sql &= "   SELECT @sql = @sql + ')tb'"
        sql &= "   EXEC(@sql)"

        sql &= "  SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet,tblTuDien.NoiDung AS TenLoaiYC"
        sql &= "  INTO #GTLD FROM"
        sql &= "  (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
        sql &= "  	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
        sql &= "                     'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,"
        sql &= "  		(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung)IDNhomCV,Duyet"
        sql &= "  FROM tblBaoCaoLichThiCong"
        sql &= "  INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
        sql &= "  WHERE GiaoViec=0 "

        sql &= "  AND Duyet=1 "

        sql &= "  )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
        sql &= "  	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"

        'Tính lợi nhuận cho các xuất kho đã thu đủ tiền"

        sql &= " declare @tblThuTien as TABLE("
        sql &= " 	Thang nvarchar(7),"
        sql &= " 	NgayThangCT Datetime,"
        sql &= " 	SoTien float,"
        sql &= " 	SoPhieuXK nvarchar(7),"
        sql &= " 	SoPhieuCG nvarchar(7),"
        sql &= " 	DaThu float,"
        sql &= " 	PhaiThu float"
        sql &= " )"

        sql &= " declare @tblTatCaThuTien as TABLE("
        sql &= " 	NgaythangCT Datetime,"
        sql &= " 	SoTien float,"
        sql &= " 	PhieuXuat nvarchar(7),"
        sql &= " 	DaThu float"
        sql &= " )"

        sql &= " insert into @tblThuTien(Thang,NgayThangCT,SoTien,SoPhieuXK,SoPhieuCG)"
        sql &= " SELECT NgaythangCT,"
        sql &= " DATEADD(dd,-1,DATEADD(mm, DATEDIFF(m,0,convert(datetime,'01/' + NgaythangCT,103))+1,0))NgayThangCT2,"
        sql &= " SUM(SoTien)SoTien,RTRIM(LTRIM(PhieuXuat))PhieuXuat,PhieuCG"
        sql &= " FROM"
        sql &= " ("
        sql &= " 	select SUBSTRING(CONVERT(NVARCHAR,NgayThangCT,103),4,8)as NgaythangCT,SoTien,"
        sql &= " 	(case when PhieuTC0 <> '0000000' then (select top 1 Sophieu from phieuxuatkho where SophieuCG = THU.PhieuTC0)"
        sql &= " 	else PhieuTC1 end)PhieuXuat,"
        sql &= " 	(case when PhieuTC0 = '0000000' then (select top 1 SophieuCG from phieuxuatkho where Sophieu = THU.PhieuTC1)"
        sql &= " 	else PhieuTC0 end)PhieuCG"
        sql &= " 	from THU WHERE (PhieuTC0 <> '0000000' OR PhieuTC1 <> '0000000') "
        sql &= " 	and NgayThangCT between @TuNgay and @DenNgay"
        sql &= " 	union "
        sql &= " 	select SUBSTRING(CONVERT(NVARCHAR,NgayThangCT,103),4,8)as NgaythangCT,SoTien,"
        sql &= " 	(case when PhieuTC0 <> '0000000' then (select top 1 Sophieu from phieuxuatkho where SophieuCG = THUNH.PhieuTC0)"
        sql &= " 	else PhieuTC1 end)PhieuXuat,"
        sql &= " 	(case when PhieuTC0 = '0000000' then (select top 1 SophieuCG from phieuxuatkho where Sophieu = THUNH.PhieuTC1)"
        sql &= " 	else PhieuTC0 end)PhieuCG"
        sql &= " 	from THUNH WHERE (PhieuTC0 <> '0000000' OR PhieuTC1 <> '0000000') "
        sql &= " 	and NgayThangCT between @TuNgay and @DenNgay"
        sql &= " )Tbltmp "
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=Tbltmp.PhieuXuat"
        sql &= " INNER JOIN BANGCHAOGIA ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu"
        sql &= " INNER JOIN BANGYEUCAU ON BANGCHAOGIA.MaSoDatHang=BANGYEUCAU.SoPhieu AND BANGYEUCAU.IDLoaiYeuCau IS not null "
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
        End If
        sql &= " GROUP BY NgaythangCT,DATEADD(dd,-1,DATEADD(mm, DATEDIFF(m,0,convert(datetime,'01/' + NgaythangCT,103))+1,0)),PhieuXuat,PhieuCG"
        sql &= " ORDER BY convert(datetime,'01/' + NgaythangCT,103)"

        sql &= " insert into @tblTatCaThuTien(ngaythangCT,SoTien,PhieuXuat)"
        sql &= " SELECT NgaythangCT,"
        sql &= " SoTien,RTRIM(LTRIM(PhieuXuat))PhieuXuat"
        sql &= " FROM"
        sql &= " ("
        sql &= " 	select NgayThangCT,SoTien,"
        sql &= " 	(case when PhieuTC0 <> '0000000' then (select top 1 Sophieu from phieuxuatkho where SophieuCG = THU.PhieuTC0)"
        sql &= " 	else PhieuTC1 end)PhieuXuat from THU WHERE (PhieuTC0 <> '0000000' OR PhieuTC1 <> '0000000') "
        sql &= " 	and NgayThangCT <= @DenNgay"
        sql &= " 	union "
        sql &= " 	select NgayThangCT,SoTien,"
        sql &= " 	(case when PhieuTC0 <> '0000000' then (select top 1 Sophieu from phieuxuatkho where SophieuCG = THUNH.PhieuTC0)"
        sql &= " 	else PhieuTC1 end)PhieuXuat from THUNH WHERE (PhieuTC0 <> '0000000' OR PhieuTC1 <> '0000000') "
        sql &= " 	and NgayThangCT <= @DenNgay"
        sql &= " )Tbltmp "
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=Tbltmp.PhieuXuat"
        sql &= " INNER JOIN BANGCHAOGIA ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu"
        sql &= " INNER JOIN BANGYEUCAU ON BANGCHAOGIA.MaSoDatHang=BANGYEUCAU.SoPhieu AND BANGYEUCAU.IDLoaiYeuCau IS not null "
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
        End If
        sql &= " ORDER BY NgaythangCT"

        'Tinh da thu den thoi diem hien tai"
        sql &= " UPDATE @tblThuTien SET DaThu = (SELECT SUM(SoTien) FROM @tblTatCaThuTien tbl "
        sql &= " WHERE [@tblThuTien].SoPhieuXK = tbl.PhieuXuat "
        sql &= " AND [@tblThuTien].NgayThangCT >= tbl.NgaythangCT)"

        'Tinh so tien phai thu"
        sql &= " UPDATE @tblThuTien SET PhaiThu = "
        sql &= " (SELECT (Tientruocthue + TienThue) * TyGia FROM PHIEUXUATKHO WHERE SoPhieu = [@tblThuTien].SoPhieuXK)"
        sql &= " select Thang,NgayThangCT,SoPhieuXK,SoPhieuCG,DaThu"
        sql &= " INTO #tbXKThuTienTrongThang"
        sql &= " from @tblThuTien "
        sql &= " where abs ([@tblThuTien].PhaiThu - [@tblThuTien].DaThu) <=50000"
        sql &= " order by Thang,SoPhieuXK"
        ' -- Tính điểm KN"
        sql &= "  INSERT INTO @tblThoiGian"
        sql &= "  SELECT dateadd(Day,-1, convert(datetime,'01/'+Thang,103))  "
        sql &= " FROM "
        sql &= "  ("
        sql &= "  SELECT DISTINCT right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7) AS Thang FROM tblBaoCaoLichThiCong "
        sql &= "  WHERE GiaoViec=0 AND Duyet=1 AND "
        sql &= "  	SoCG IN (SELECT DISTINCT SoPhieuCG FROM #tbXKThuTienTrongThang )"
        sql &= "  group BY right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7))tb"

        sql &= "  INSERT INTO @tblNhanVien"
        sql &= "  SELECT DISTINCT IDNgThucHien  FROM tblBaoCaoLichThiCong "
        sql &= "  WHERE GiaoViec=0 AND Duyet=1 AND "
        sql &= "  	SoCG IN (SELECT DISTINCT SoPhieuCG FROM #tbXKThuTienTrongThang )"

        sql &= "  DECLARE @tblDiemKyNang AS TABLE"
        sql &= "  ("
        sql &= "  	IDNhanVien INT,"
        sql &= "  	IDKyNang INT,"
        sql &= "  	IDNhomKN INT,"
        sql &= "  	Diem FLOAT DEFAULT(0),"
        sql &= "  	NgayThi DATETIME"
        sql &= "  )"
        sql &= "  INSERT INTO @tblDiemKyNang(IDNhanVien,IDKyNang,IDNhomKN,NgayThi)"
        sql &= "  SELECT "
        sql &= "  [@tblNhanVien].IdNhanVien,"
        sql &= "  tblKyNang.ID as IDKyNang, tblKyNang.IdNhomKN as IDNhomKN,"
        sql &= "  [@tblThoiGian].NgayThi"
        sql &= "  FROM @tblNhanVien "
        sql &= "  CROSS JOIN @tblThoiGian "
        sql &= "  CROSS JOIN (SELECT ID,IDNhomKN FROM NLDANHSACH WHERE Loai = 1)tblKyNang"
        sql &= "  ORDER BY [@tblNhanVien].IdNhanVien"

        sql &= "  UPDATE @tblDiemKyNang"
        sql &= "  SET Diem = ("
        sql &= "  	ISNULL("
        sql &= "  		(SELECT TOP 1 Diem FROM tblDiemThiKyNang "
        sql &= "  		WHERE IDNhanVien = [@tblDiemKyNang].IDNhanVien"
        sql &= "  		AND IDKyNang = [@tblDiemKyNang].IDKyNang"
        sql &= "  		AND NgayThi <= [@tblDiemKyNang].NgayThi ORDER BY NgayThi DESC),"
        sql &= "  		(SELECT ROUND(Diem/2.0,2) FROM NLDanhSach WHERE ID = [@tblDiemKyNang].IDKyNang)"
        sql &= "  	)"
        sql &= "  )"

        sql &= "  SELECT IDNhanVien,IDNhomKN,SUM(Diem) Diem, RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)  as Thang"
        sql &= "  INTO  #tbKyNang"
        sql &= "  FROM @tblDiemKyNang "
        sql &= "  GROUP BY IDNhanVien,IDNhomKN,RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)"
        sql &= "  ORDER By Thang,IdNhanVien"

        sql &= "  SELECT *,ISNULL((SELECT Diem FROM #tbKyNang "
        sql &= "  					WHERE #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien AND #GTLD.IDNhomCV=#tbKyNang.IDNhomKN "
        sql &= "  	AND #tbKyNang.Thang = right(Convert(nvarchar,#GTLD.Ngay,103),7)),0) TongDiemKN"
        sql &= "  INTO #DanhSachGTLDtmp"
        sql &= "  FROM #GTLD"

        ' Tính lợi nhuận kỹ thuật theo các xuất kho đã thu tiền trong bảng #tbXKThuTienTrongThang group theo tháng thu tiền */"

        sql &= "  SELECT  #tbXKThuTienTrongThang.DaThu AS TienThu, PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK,#tbXKThuTienTrongThang.Thang, "
        sql &= "     BANGCHAOGIA.Masodathang, "
        sql &= "     (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0)"
        sql &= "      - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "     * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
        sql &= "     * (#tbXKThuTienTrongThang.DaThu / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * (ISNULL(tblTuDien.Diem, 0) / 100) END) "
        sql &= "     AS LoiNhuanKT, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare"
        sql &= "  INTO #tbLoiNhuanKT"
        sql &= "  FROM  #tbXKThuTienTrongThang "
        sql &= "  INNER JOIN PHIEUXUATKHO ON #tbXKThuTienTrongThang.SoPhieuXK = PHIEUXUATKHO.Sophieu "
        sql &= "  LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) "
        sql &= "  		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= "  INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG INNER JOIN"
        sql &= "     BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null "
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
        End If

        sql &= "    LEFT OUTER JOIN tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID LEFT OUTER JOIN"
        sql &= "         (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
        sql &= "           FROM          V_XuatkhoCal"
        sql &= "           GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "     V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "     V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "     V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "     V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
        sql &= "  WHERE abs(DaThu - ((PHIEUXUATKHO.Tientruocthue+PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.TyGia))<=50000"

        sql &= "  SELECT #DanhSachGTLDtmp.* ,##tbDinhMuc.PTLoiNhuanKT,#tbLoiNhuanKT.Thang,#tbLoiNhuanKT.LoiNhuanKT,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
        sql &= "  INTO #TongHop"
        sql &= "  FROM #tbLoiNhuanKT"

        sql &= "  INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=#tbLoiNhuanKT.SoPhieuCG"
        sql &= "  INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=#DanhSachGTLDtmp.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=#DanhSachGTLDtmp.IDLoaiYeuCau"
        sql &= "  LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
        sql &= "  LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

        sql &= "  SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNoiDung"
        sql &= "  INTO #tbMauSo"
        sql &= "  FROM #TongHop"
        sql &= "  GROUP BY SoCG,IDNoiDung"

        sql &= "  SELECT ID,Ngay,SoGio,SoYC,SoCG,ttcMa,Thang,"
        sql &= "  	IDNgThucHien,IDNoiDung,TongDiemKN AS KNNhanVien,PTLoiNhuanKT AS PTLoiNhuanNhomCV,TongSoGio AS TongGioNhomThucHien,"
        sql &= "  	TyLe*ISNULL(LoiNhuanKT,0)LoiNhuan,ISNULL(LoiNhuanKT,0) AS TongLN,TenDuAn,Duyet"
        sql &= "  INTO #tbKetQua"
        sql &= "  FROM("
        sql &= "  SELECT #TongHop.*,tb.TongSoGio,"
        sql &= "  (CASE WHEN #tbMauSo.MauSo = 0 THEN 0 ELSE"
        sql &= "  ((#TongHop.SoGio*#TongHop.TongDiemKN) /#tbMauSo.MauSo) * (Convert(float,#TongHop.PTLoiNhuanKT)/100) END)  AS TyLe"
        sql &= "  FROM #TongHop"
        sql &= "  INNER JOIN "
        sql &= "  (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG"
        sql &= "  FROM "
        sql &= "   #TongHop"
        sql &= "  GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=#TongHop.IDNhomCV AND tb.SoCG=#TongHop.SoCG"
        sql &= "  INNER JOIN #tbMauSo ON #tbMauSo.SoCG=#TongHop.SoCG AND #tbMauSo.IDNoiDung=#TongHop.IDNoiDung"
        sql &= "  )tb"
        sql &= "  ORDER BY ID,SoCG,IDNoiDung"

        sql &= "  SELECT (SELECT Ten FROM DEPATMENT WHERE ID=(SELECT TOP 1 IDDepatment FROM LUONG WHERE IDNhanVien=tb2.IDNgThucHien AND convert(datetime,'01/' + [Month],103) <= '" & DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbDenThang.EditValue).Year, Convert.ToDateTime(tbDenThang.EditValue).Month + 1, 0)) & "' ORDER BY convert(datetime,'01/' + [Month],103) desc)) as Phong,"
        sql &= " NHANSU.Ten, tb2.* FROM ( SELECT IDNgThucHien, [01], [02], [03], [04], [05], [06], "
        sql &= "  	[07], [08], [09], [10], [11], [12],"
        sql &= "  (ISNULL([01],0)+ ISNULL([02],0)+ ISNULL([03],0)+ ISNULL([04],0)+ ISNULL([05],0)+ ISNULL([06],0)+ ISNULL([07],0)+ ISNULL([08],0)+ ISNULL([09],0)+ ISNULL([10],0)+ ISNULL([11],0)+ ISNULL([12],0))Tong"
        sql &= "  FROM "
        sql &= "  (SELECT TongLoiNhuan,LEFT(Thang,2) AS Thang,IDNgThucHien FROM (SELECT SUM(LoiNhuan) AS TongLoiNhuan, Thang AS Thang,IDNgThucHien"
        sql &= "  FROM #tbKetQua"
        sql &= "  GROUP BY IDNgThucHien,Thang)tb) p"
        sql &= "  PIVOT"
        sql &= "  ("
        sql &= "  SUM (TongLoiNhuan)"
        sql &= "  FOR Thang IN"
        sql &= "  ( [01], [02], [03], [04], [05], [06], [07], [08], [09], [10], [11], [12] )"
        sql &= "  ) AS  pvtLoiNhuan)tb2"
        sql &= "  INNER JOIN NHANSU ON NHANSU.ID=tb2.IDNgThucHien"
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " AND NHANSU.ID IN (SELECT DISTINCT IDNhanVien FROM LUONG WHERE IDDepatment = @Phong AND Convert(Datetime,'01/'+[Month],103) BETWEEN @TuNgay AND @DenNgay)"
            AddParameterWhere("@Phong", cbPhong.EditValue)
        End If
        sql &= " ORDER BY Phong,IDNgThucHien"
        sql &= "  DROP Table ##tbDinhMuc"
        sql &= "  DROP Table #GTLD"
        sql &= "  DROP Table #DanhSachGTLDtmp"
        sql &= "  DROP Table #TongHop"
        sql &= "  DROP table #tbXKThuTienTrongThang"
        sql &= "  DROP table #tbLoiNhuanKT"
        sql &= "  DROP Table #tbKyNang"
        sql &= "  DROP Table #tbMauSo"
        sql &= "  DROP Table #tbKetQua   "
        AddParameterWhere("@TuNgay", New DateTime(Convert.ToDateTime(tbTuThang.EditValue).Year, Convert.ToDateTime(tbTuThang.EditValue).Month, 1))
        AddParameterWhere("@DenNgay", DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbDenThang.EditValue).Year, Convert.ToDateTime(tbDenThang.EditValue).Month + 1, 0)))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TheoXuatKho()
        ShowWaiting("Đang tải dữ liệu...")
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY "
        sql &= " DECLARE @tblNhanVien AS TABLE (IdNhanVien INT) "
        sql &= " DECLARE @tblThoiGian AS TABLE (NgayThi DATETIME)"
        sql &= " DECLARE @table sysname "
        sql &= " SET @table = 'tblDinhMucDiem'"
        sql &= " DECLARE @id_field sysname "
        sql &= " SET @id_field = 'ID'"
        sql &= " DECLARE @sql varchar(MAX)"
        sql &= " SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '"
        sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'"
        sql &= "  +' CONVERT(sql_variant,N'''') AS [PTLoiNhuanKT] WHERE 1=0 '+CHAR(10)"
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

        sql &= " SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet,tblTuDien.NoiDung AS TenLoaiYC"
        sql &= " INTO #GTLD FROM"
        sql &= " (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
        sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
        sql &= "                     'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,"
        sql &= " 		(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung)IDNhomCV,Duyet"
        sql &= " FROM tblBaoCaoLichThiCong"
        sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
        sql &= " WHERE GiaoViec=0 "

        sql &= " AND Duyet=1 "

        sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
        sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"

        ' Tính điểm KN"
        sql &= " INSERT INTO @tblThoiGian"
        sql &= " SELECT dateadd(Day,-1, convert(datetime,'01/'+Thang,103))  FROM "
        sql &= " ("
        sql &= " SELECT DISTINCT right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7) AS Thang FROM tblBaoCaoLichThiCong "
        sql &= " WHERE GiaoViec=0 AND Duyet=1 AND "
        sql &= " 	SoCG IN (SELECT SoPhieuCG "
        sql &= " 				FROM PHIEUXUATKHO "
        sql &= " 				WHERE Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " )"
        sql &= " group BY right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7))tb"

        sql &= " INSERT INTO @tblNhanVien"
        sql &= " SELECT DISTINCT IDNgThucHien  FROM tblBaoCaoLichThiCong "
        sql &= " WHERE GiaoViec=0 AND Duyet=1 AND "
        sql &= " 	SoCG IN (SELECT SoPhieuCG "
        sql &= " 				FROM PHIEUXUATKHO "
        sql &= " 				WHERE Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " )"

        sql &= " DECLARE @tblDiemKyNang AS TABLE"
        sql &= " ("
        sql &= " 	IDNhanVien INT,"
        sql &= " 	IDKyNang INT,"
        sql &= " 	IDNhomKN INT,"
        sql &= " 	Diem FLOAT DEFAULT(0),"
        sql &= " 	NgayThi DATETIME"
        sql &= " )"
        sql &= " INSERT INTO @tblDiemKyNang(IDNhanVien,IDKyNang,IDNhomKN,NgayThi)"
        sql &= " SELECT "
        sql &= " [@tblNhanVien].IdNhanVien,"
        sql &= " tblKyNang.ID as IDKyNang, tblKyNang.IdNhomKN as IDNhomKN,"
        sql &= " [@tblThoiGian].NgayThi"
        sql &= " FROM @tblNhanVien "
        sql &= " CROSS JOIN @tblThoiGian "
        sql &= " CROSS JOIN (SELECT ID,IDNhomKN FROM NLDANHSACH WHERE Loai = 1)tblKyNang"
        sql &= " ORDER BY [@tblNhanVien].IdNhanVien"

        sql &= " UPDATE @tblDiemKyNang"
        sql &= " SET Diem = ("
        sql &= " 	ISNULL("
        sql &= " 		(SELECT TOP 1 Diem FROM tblDiemThiKyNang "
        sql &= " 		WHERE IDNhanVien = [@tblDiemKyNang].IDNhanVien"
        sql &= " 		AND IDKyNang = [@tblDiemKyNang].IDKyNang"
        sql &= " 		AND NgayThi <= [@tblDiemKyNang].NgayThi ORDER BY NgayThi DESC),"
        sql &= " 		(SELECT ROUND(Diem/2.0,2) FROM NLDanhSach WHERE ID = [@tblDiemKyNang].IDKyNang)"
        sql &= " 	)"
        sql &= " )"

        sql &= " SELECT IDNhanVien,IDNhomKN,SUM(Diem) Diem, RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)  as Thang"
        sql &= " INTO  #tbKyNang"
        sql &= " FROM @tblDiemKyNang "
        sql &= " GROUP BY IDNhanVien,IDNhomKN,RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)"
        sql &= " ORDER By Thang,IdNhanVien"

        sql &= " SELECT *,ISNULL((SELECT Diem FROM #tbKyNang "
        sql &= " 					WHERE #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien AND #GTLD.IDNhomCV=#tbKyNang.IDNhomKN "
        sql &= " 	AND #tbKyNang.Thang = right(Convert(nvarchar,#GTLD.Ngay,103),7)),0) TongDiemKN"
        sql &= " INTO #DanhSachGTLDtmp"
        sql &= " FROM #GTLD"

        sql &= " SELECT   PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK,PHIEUXUATKHO.NgayThang AS NgayXK, "
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0)"
        sql &= "     - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15)))* (ISNULL(tblTuDien.Diem, 0) / 100) END) "
        sql &= "    AS LoiNhuanKT,  BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare"
        sql &= " INTO #tbLoiNhuanKT "
        sql &= " FROM  PHIEUXUATKHO "
        sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG INNER JOIN"
        sql &= "    BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null "
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
        End If
        sql &= "  LEFT OUTER JOIN  tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID LEFT OUTER JOIN"
        sql &= "        (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
        sql &= "          FROM          V_XuatkhoCal"
        sql &= "          GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "    V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "    V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "    V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
        sql &= "    V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
        sql &= " WHERE Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "

        sql &= " SELECT #DanhSachGTLDtmp.* ,##tbDinhMuc.PTLoiNhuanKT,#tbLoiNhuanKT.NgayXK,#tbLoiNhuanKT.LoiNhuanKT,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
        sql &= " INTO #TongHop"
        sql &= " FROM #tbLoiNhuanKT"

        sql &= " INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=#tbLoiNhuanKT.SoPhieuCG"
        sql &= " INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=#DanhSachGTLDtmp.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=#DanhSachGTLDtmp.IDLoaiYeuCau"
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

        sql &= " SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNoiDung"
        sql &= " INTO #tbMauSo"
        sql &= " FROM #TongHop"
        sql &= " GROUP BY SoCG,IDNoiDung"

        sql &= " SELECT ID,Ngay,SoGio,SoYC,SoCG,ttcMa,NgayXK,"
        sql &= " 	IDNgThucHien,IDNoiDung,TongDiemKN AS KNNhanVien,PTLoiNhuanKT AS PTLoiNhuanNhomCV,TongSoGio AS TongGioNhomThucHien,"
        sql &= " 	TyLe*ISNULL(LoiNhuanKT,0)LoiNhuan,ISNULL(LoiNhuanKT,0) AS TongLN,TenDuAn,Duyet"
        sql &= " INTO #tbKetQua"
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
        sql &= " ORDER BY ID,SoCG,IDNoiDung"

        sql &= "  SELECT (SELECT Ten FROM DEPATMENT WHERE ID=(SELECT TOP 1 IDDepatment FROM LUONG WHERE IDNhanVien=tb2.IDNgThucHien AND convert(datetime,'01/' + [Month],103) <= '" & DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbDenThang.EditValue).Year, Convert.ToDateTime(tbDenThang.EditValue).Month + 1, 0)) & "' ORDER BY convert(datetime,'01/' + [Month],103) desc)) as Phong,"
        sql &= " NHANSU.Ten, tb2.* FROM ( SELECT IDNgThucHien, [01], [02], [03], [04], [05], [06], "
        sql &= " 	[07], [08], [09], [10], [11], [12],"
        sql &= " (ISNULL([01],0)+ ISNULL([02],0)+ ISNULL([03],0)+ ISNULL([04],0)+ ISNULL([05],0)+ ISNULL([06],0)+ ISNULL([07],0)+ ISNULL([08],0)+ ISNULL([09],0)+ ISNULL([10],0)+ ISNULL([11],0)+ ISNULL([12],0))Tong"
        sql &= " FROM "
        sql &= " (SELECT TongLoiNhuan,LEFT(Thang,2) AS Thang,IDNgThucHien FROM (SELECT SUM(LoiNhuan) AS TongLoiNhuan, right(Convert(nvarchar,#tbKetQua.NgayXK,103),7) AS Thang,IDNgThucHien"
        sql &= " FROM #tbKetQua"
        sql &= " GROUP BY IDNgThucHien,right(Convert(nvarchar,#tbKetQua.NgayXK,103),7))tb) p"
        sql &= " PIVOT"
        sql &= " ("
        sql &= " SUM (TongLoiNhuan)"
        sql &= " FOR Thang IN"
        sql &= " ( [01], [02], [03], [04], [05], [06], [07], [08], [09], [10], [11], [12] )"
        sql &= " ) AS  pvtLoiNhuan)tb2"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tb2.IDNgThucHien"
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " AND NHANSU.ID IN (SELECT DISTINCT IDNhanVien FROM LUONG WHERE IDDepatment = @Phong AND Convert(Datetime,'01/'+[Month],103) BETWEEN @TuNgay AND @DenNgay)"
            AddParameterWhere("@Phong", cbPhong.EditValue)
        End If
        sql &= " ORDER BY Phong,IDNgThucHien"
        sql &= " DROP Table ##tbDinhMuc"
        sql &= " DROP Table #GTLD"
        sql &= " DROP Table #DanhSachGTLDtmp"
        sql &= " DROP Table #TongHop"
        'sql &= " DROP table #tbXKThuTienTrongThang"
        'sql &= " DROP table #tbLoiNhuan"
        sql &= " DROP table #tbLoiNhuanKT"
        sql &= " DROP Table #tbKyNang"
        sql &= " DROP Table #tbMauSo"
        sql &= " DROP Table #tbKetQua   "
        AddParameterWhere("@TuNgay", New DateTime(Convert.ToDateTime(tbTuThang.EditValue).Year, Convert.ToDateTime(tbTuThang.EditValue).Month, 1))
        AddParameterWhere("@DenNgay", DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbDenThang.EditValue).Year, Convert.ToDateTime(tbDenThang.EditValue).Month + 1, 0)))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    'Private Sub gdvCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvCT.RowStyle
    '    On Error Resume Next
    '    If e.RowHandle < 0 Then Exit Sub
    '    If gdvCT.GetRowCellValue(e.RowHandle, "Ten").ToString.Length > 5 Then
    '        Select Case gdvCT.GetRowCellValue(e.RowHandle, "Ten").ToString.Substring(0, 5)
    '            Case "   DT", "   LN", "   CK"
    '                e.Appearance.Font = New Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold)
    '            Case "Tổng "
    '                e.Appearance.Font = New Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold)
    '                e.Appearance.ForeColor = Color.DarkSlateBlue
    '        End Select
    '    End If

    'End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub
End Class