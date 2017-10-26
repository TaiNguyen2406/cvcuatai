Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress
Imports System.Linq

Public Class frmKiemTraBaoCaoKTTest
    Dim _exit As Boolean = False
    Dim tbData As DataTable
    Private Sub frmBaoCaoLichThiCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        _exit = True
        tbThang.EditValue = New DateTime(tg.Year, tg.Month, 1)
        _exit = False
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btChotSoLieu.Visibility = XtraBars.BarItemVisibility.Never
        End If

        LoadDSPhongBan()
        LoadDSNhanVien()
        LoadrcbNV()
        LoadDSSoYC()
        LoadDSNoiDungCV()
        btTaiBaoCao.PerformClick()
    End Sub

    Public Sub LoadDSPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT ID,Ten FROM DEPATMENT ")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNhanVien()
        On Error Resume Next
        Dim sql As String = ""
        If Not cbPhong.EditValue Is Nothing Then
            sql = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and IDDepatment= " & cbPhong.EditValue
        Else
            sql = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
            cbNhanVien.EditValue = TaiKhoan

            ' rcbNV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadrcbNV()
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT ID,Ten FROM NHANSU WHERE Noictac=74 ")
        If Not tb Is Nothing Then
            rcbNV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNoiDungCV()
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        AddParameterWhere("@Loai2", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT tblTuDien.ID,tblTuDien.NoiDung,tbTmp.NoiDung AS Nhom FROM tblTuDien LEFT JOIN tblTuDien as tbTmp ON tblTuDien.IDP=tbTmp.ID and tbTmp.Loai=@loai2 WHERE tblTuDien.Loai=@Loai ORDER BY tbTmp.Ma,tblTuDien.Ma ")
        If Not tb Is Nothing Then
            rgdvNoiDung.DataSource = tb

            With rgdvNoiDung.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Nội dung"
                colNoiDung.VisibleIndex = 1
                colNoiDung.Width = 120
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colNhom = .AddField("Nhom")
                colNhom.Caption = "Nhóm"
                colNhom.VisibleIndex = 2
                colNhom.Width = 250
                colNhom.GroupIndex = 0
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSSoYC()
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT BANGYEUCAU.Sophieu,KHACHHANG.ttcMa,BANGYEUCAU.NoiDung,BANGYEUCAU.IDLoaiYeuCau,tblTuDien.NoiDung as LoaiYeuCau,"
        sql &= " BANGCHAOGIA.SoPhieu AS SoCG, BANGCHAOGIA.TenDuAn	 "
        sql &= " FROM BANGYEUCAU "
        sql &= " INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.MaSoDatHang=BANGYEUCAU.SoPhieu"
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.ID=BANGYEUCAU.IDLoaiYeuCau AND tblTuDien.Loai = 10 "
        Dim sqlWhere As String = " WHERE IDLoaiYeuCau Is not null "
        Dim sqlOrder As String = " ORDER BY SoPhieu DESC "
        'If tbTuNgay.EditValue Is Nothing And Not tbDenNgay.EditValue Is Nothing Then
        '    sqlWhere &= " AND BANGYEUCAU.Ngaythang <= @DenNgay "
        'ElseIf Not tbTuNgay.EditValue Is Nothing And tbDenNgay.EditValue Is Nothing Then
        '    sqlWhere &= " AND BANGYEUCAU.Ngaythang >= @TuNgay "
        'ElseIf Not tbTuNgay.EditValue Is Nothing And Not tbDenNgay Is Nothing Then
        '    sqlWhere &= " AND BANGYEUCAU.Ngaythang Between @TuNgay And @DenNgay "
        'End If
        'AddParameterWhere("@Thang", tbThang.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql & sqlWhere & sqlOrder)
        If Not tb Is Nothing Then
            rcbSoYC.View.Columns.Clear()

            rcbSoYC.DataSource = tb
            With rcbSoYC.View.Columns
                Dim colSP = .AddField("Sophieu")
                colSP.Caption = "Số YC"
                colSP.VisibleIndex = 0
                colSP.Width = 60
                colSP.OptionsColumn.FixedWidth = True
                colSP.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colSPCG = .AddField("SoCG")
                colSPCG.Caption = "Số CG"
                colSPCG.VisibleIndex = 1
                colSPCG.Width = 60
                colSPCG.OptionsColumn.FixedWidth = True
                colSPCG.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colTenHang = .AddField("ttcMa")
                colTenHang.Caption = "Mã KH"
                colTenHang.VisibleIndex = 2
                colTenHang.Width = 120
                colTenHang.OptionsColumn.FixedWidth = True
                colTenHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Nội dung yêu cầu"
                colNoiDung.VisibleIndex = 3
                colNoiDung.Width = 250
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                colNoiDung.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                colNoiDung.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                colNoiDung.ColumnEdit = rMemoText

                Dim colTenCG = .AddField("TenDuAn")
                colTenCG.Caption = "Nội dung chào giá"
                colTenCG.VisibleIndex = 4
                colTenCG.Width = 250
                colTenCG.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                colTenCG.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                colTenCG.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                colTenCG.ColumnEdit = rMemoText

                Dim colLoaiYeuCau = .AddField("LoaiYeuCau")
                colLoaiYeuCau.Caption = "Loại yêu cầu"
                colLoaiYeuCau.VisibleIndex = 5
                colLoaiYeuCau.Width = 200
                colLoaiYeuCau.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                colLoaiYeuCau.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                colLoaiYeuCau.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                colLoaiYeuCau.ColumnEdit = rMemoText
                Dim colIDLoai = .AddField("IDLoaiYeuCau")
                colIDLoai.Caption = "Loại yêu cầu"
                colIDLoai.VisibleIndex = -1

            End With
        End If
    End Sub



    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbSoYC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbSoYC.ButtonClick
        If e.Button.Index = 1 Then
            cbSoYC.EditValue = Nothing
        End If
    End Sub

    Private Sub btTaiBaoCao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiBaoCao.ItemClick
        If cbTieuChi.EditValue = "Phiếu thu" Then
            TheoPhieuThu()
        Else
            TheoXuatKho()
        End If

    End Sub

    Public Sub TheoPhieuThu()
        ShowWaiting("Đang tải dữ liệu ...")
        AddParameterWhere("@TuNgay", New DateTime(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month, 1))
        AddParameterWhere("@DenNgay", DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month + 1, 0)))
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " DECLARE @TyLeQD as float "
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

        sql &= " SET @TyLeQD= (select TyLeQuyDoiDiem FROM tblQuyDoi WHERE ThangNam='" & Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy") & "')"

        sql &= " SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet,tblTuDien.NoiDung AS TenLoaiYC"
        sql &= " INTO #GTLD FROM"
        sql &= " (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
        sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,"
        sql &= "                     'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung,"
        sql &= " 		(SELECT Ma FROM tblTuDien WHERE ID=(SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung))IDNhomCV,Duyet"
        sql &= " FROM tblBaoCaoLichThiCong"
        sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
        sql &= " WHERE GiaoViec=0 "

        If cbTrangThai.EditValue = "Đã duyệt" Then
            sql &= " AND Duyet=1 "
        End If

        If Not cbSoYC.EditValue Is Nothing Then
            sql &= " AND SoYC=@SoYC "
            AddParameterWhere("@SoYC", cbSoYC.EditValue)
        End If

        sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau"
        sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2"


        'Tính lợi nhuận cho các xuất kho đã thu đủ tiền
        sql &= " SELECT tb.*,PHIEUXUATKHO.SoPhieu AS SoPhieuXK, PHIEUXUATKHO.SoPhieuCG"
        sql &= " INTO #tbXKThuTienTrongThang"
        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
        sql &= " FROM THU"
        sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
        sql &= "  CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) Between @TuNgay And @DenNgay"
        sql &= " UNION ALL"
        sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= " FROM THUNH"
        sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
        sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) Between @TuNgay And @DenNgay"
        sql &= " )tb"
        sql &= " INNER JOIN PHIEUXUATKHO ON tb.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
        'Tính điểm kỹ năng
        sql &= " INSERT INTO @tblThoiGian"
        sql &= " SELECT dateadd(Day,-1, convert(datetime,'01/'+Thang,103))  FROM "
        sql &= " ("
        sql &= " SELECT DISTINCT right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7) AS Thang FROM tblBaoCaoLichThiCong "
        sql &= " WHERE GiaoViec=0 AND Duyet=1 AND "
        sql &= " 	SoCG IN (SELECT SoPhieuCG FROM #tbXKThuTienTrongThang )"
        sql &= " group BY right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7))tb"

        sql &= " INSERT INTO @tblNhanVien"
        sql &= " SELECT DISTINCT IDNgThucHien  FROM tblBaoCaoLichThiCong "
        sql &= " WHERE GiaoViec=0 AND Duyet=1 AND "
        sql &= " 	SoCG IN (SELECT SoPhieuCG FROM #tbXKThuTienTrongThang)"



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


        sql &= " SELECT tb.IDNhanVien,tb.IDNhomKN,tb.Diem*ISNULL(tblTHChamCong.HSDanhGia,0) as Diem,tb.Thang"
        sql &= " INTO  #tbKyNang"
        sql &= " FROM "
        sql &= " (SELECT IDNhanVien,IDNhomKN,SUM(Diem) Diem, RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)  as Thang"
        sql &= " FROM @tblDiemKyNang "
        sql &= " GROUP BY IDNhanVien,IDNhomKN,RIGHT(CONVERT(NVARCHAR,NgayThi,103),7))tb"
        sql &= " LEFT JOIN tblTHChamCong ON tblTHChamCong.IDNhanVien=tb.IDNhanVien AND tblTHChamCong.[Month]=tb.Thang"

        'sql &= " SELECT *,ISNULL((SELECT Diem FROM #tbKyNang "
        'sql &= " 					WHERE #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien AND #GTLD.IDNhomCV=#tbKyNang.IDNhomKN ),0) TongDiemKN"
        'sql &= " INTO #DanhSachGTLDtmp"
        'sql &= " FROM #GTLD"

        sql &= " select IDNgThucHien,IDLoaiYeuCau,IDNhomCV,tblTuDien.Diem as PTLoiNhuanNhomCV, RIGHT(Convert(nvarchar,Ngay,103),7)Thang,SoCG,IDNoiDung, SoGio,#GTLD.ID,Ngay,TenLoaiYC,SoYC,Duyet, "
        sql &= " (select SUM(#tbKyNang.Diem) FROM #tbKyNang WHERE #tbKyNang.IDNhomKN=#GTLD.IDNhomCV AND RIGHT(Convert(nvarchar,#GTLD.Ngay,103),7)=#tbKyNang.Thang AND #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien)TongDiemKN "
        sql &= " INTO #DanhSachGTLDtmp"
        sql &= " FROM #GTLD"
        sql &= " INNER JOIN tblTuDien ON Loai=11 AND tblTuDien.Ma=#GTLD.IDNhomCV"

        sql &= " SELECT SUM(SoTien)SoTien,PHIEUXUATKHO.SoPhieu"
        sql &= " INTO #tbLoiNhuan"
        sql &= " FROM"
        sql &= " (SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
        sql &= " FROM THU"
        sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
        sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= @DenNgay"
        sql &= " UNION ALL"
        sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= " FROM THUNH"
        sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
        sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= @DenNgay"
        sql &= " )tb2"
        sql &= " INNER JOIN PHIEUXUATKHO ON tb2.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb2.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
        sql &= " WHERE PHIEUXUATKHO.SoPhieu IN ( SELECT DISTINCT SoPhieuXK FROM #tbXKThuTienTrongThang)"
        sql &= " GROUP BY PHIEUXUATKHO.SoPhieu"

        sql &= " SELECT  #tbLoiNhuan.Sotien AS TienThu, PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
        sql &= "    BANGCHAOGIA.Masodathang, PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia AS TruocThue, "
        sql &= "    PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue, #tbLoiNhuan.Sotien, ISNULL(tb.GiaNhap, 0) AS GiaNhap, "
        sql &= "    ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0) "
        sql &= "    + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0) AS ChiChietKhau, PHIEUXUATKHO.tienchietkhau * PHIEUXUATKHO.Tygia AS TienChietKhau, "
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
        sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END))) "
        sql &= "    * (#tbLoiNhuan.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * (ISNULL(tblTuDien.Diem, 0) / 100) END) "
        sql &= "    AS LoiNhuanKT, "
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
        sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END))) "
        sql &= "    * (#tbLoiNhuan.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * ((100 - ISNULL(tblTuDien.Diem, 0)) / 100) "
        sql &= "    END) AS LoiNhuanKD, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare,"
        sql &= "    tblTuDien.HeSo1 as PTLoiNhuanThietKe"
        sql &= " INTO #tbLoiNhuanKT"
        sql &= " FROM  #tbLoiNhuan "
        sql &= " INNER JOIN PHIEUXUATKHO ON #tbLoiNhuan.SoPhieu = PHIEUXUATKHO.Sophieu "
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
        sql &= " WHERE abs(SoTien - ((PHIEUXUATKHO.Tientruocthue+PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.TyGia))<=50000"
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
        End If

        sql &= " SELECT #DanhSachGTLDtmp.* ,##tbDinhMuc.PTLoiNhuanKT,#tbLoiNhuanKT.LoiNhuanKT,"
        sql &= " (#tbLoiNhuanKT.LoiNhuanKT *((Case WHEN #DanhSachGTLDtmp.IDNhomCV=1 THEN ISNULL(#tbLoiNhuanKT.PTLoiNhuanThietKe,50) ELSE 100- ISNULL(#tbLoiNhuanKT.PTLoiNhuanThietKe,50) END)/100)) as LoiNhuanNhomCV,"
        sql &= " BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
        sql &= " INTO #TongHop"
        sql &= " FROM #tbLoiNhuanKT"

        sql &= "  INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=#tbLoiNhuanKT.SoPhieuCG"
        sql &= " INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=#DanhSachGTLDtmp.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=#DanhSachGTLDtmp.IDLoaiYeuCau"
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

        sql &= " SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNhomCV"
        sql &= "  INTO #tbMauSo"
        sql &= " FROM #TongHop"
        sql &= " GROUP BY SoCG,IDNhomCV"

        sql &= " SELECT ID,Ngay,SoGio,SoYC,(SoCG + N'  ' + ttcMa +N'  '+ ISNULL(TenDuAn,N'') + N'  ' + replace(replace(replace(convert(varchar,convert(Money, round(ISNULL(LoiNhuanKT,0),2)),1),'.','-'),',','.'),'-',',') + N'  ' + ISNULL(TenLoaiYC,N'') )SoCG,ttcMa,IDNgThucHien,IDNoiDung,TongDiemKN AS KNNhanVien,PTLoiNhuanKT AS PTLoiNhuanNhomCV,TongSoGio AS TongGioNhomThucHien,SoTien as LoiNhuan,(SoTien* ISNULL(@TyLeQD,0)) AS Diem,ISNULL(LoiNhuanKT,0) AS TongLN,(ISNULL(LoiNhuanKT,0) * ISNULL(@TyLeQD,0)) AS TongDiem,TenDuAn,Duyet"
        sql &= " INTO #tbKetQua"
        sql &= " FROM("
        sql &= " SELECT #TongHop.*,tb.TongSoGio,"
        sql &= " (CASE WHEN #tbMauSo.MauSo = 0 THEN 0 ELSE"
        sql &= " ((#TongHop.SoGio*#TongHop.TongDiemKN) /#tbMauSo.MauSo) * #TongHop.LoiNhuanNhomCV END)  AS SoTien"
        sql &= " FROM #TongHop"
        sql &= " INNER JOIN "
        sql &= " (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG"
        sql &= " FROM "
        sql &= "  #TongHop"
        sql &= " GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=#TongHop.IDNhomCV AND tb.SoCG=#TongHop.SoCG"
        sql &= " INNER JOIN #tbMauSo ON #tbMauSo.SoCG=#TongHop.SoCG AND #tbMauSo.IDNhomCV=#TongHop.IDNhomCV"
        sql &= " )tb"

        sql &= " ORDER BY ID,SoCG,IDNoiDung"

        sql &= " SELECT * FROM #tbKetQua "
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " where IDNgThucHien IN (SELECT DISTINCT IDNhanVien FROM LUONG WHERE IDDepatment = @Phong AND Convert(Datetime,'01/'+[Month],103) BETWEEN @TuNgay AND @DenNgay)"
            AddParameterWhere("@Phong", cbPhong.EditValue)

            If Not cbNhanVien.EditValue Is Nothing Then
                sql &= " AND IDNgThucHien =@NgThucHien "
                AddParameterWhere("@NgThucHien", cbNhanVien.EditValue)
            End If
        Else
            If Not cbNhanVien.EditValue Is Nothing Then
                sql &= " WHERE IDNgThucHien =@NgThucHien "
                AddParameterWhere("@NgThucHien", cbNhanVien.EditValue)
            End If
        End If



        sql &= " ORDER BY SoCG,Ngay"

        sql &= " SELECT ISNULL(SUM(LoiNhuan),0) FROM #tbKetQua"
        sql &= " SELECT ISNULL(SUM(TongLN),0) FROM (SELECT DISTINCT SoCG,TongLN FROM #tbKetQua)tb "

        sql &= " SELECT ISNULL(Sum(LoiNhuanKT),0)LoiNhuanKT FROM #tbLoiNhuanKT "

        sql &= " DROP Table ##tbDinhMuc"
        sql &= " DROP Table #GTLD"
        sql &= " DROP Table #DanhSachGTLDtmp"
        sql &= " DROP Table #TongHop"
        sql &= " DROP table #tbXKThuTienTrongThang"
        sql &= " DROP table #tbLoiNhuan"
        sql &= " DROP table #tbLoiNhuanKT"
        sql &= " DROP Table #tbKyNang"
        sql &= " DROP Table #tbMauSo"
        sql &= " DROP Table #tbKetQua"


        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            CloseWaiting()
            gdv.DataSource = ds.Tables(0)
            tbTongLoiNhuan.EditValue = ds.Tables(2).Rows(0)(0)
            tbTongLoiNhuanBC.EditValue = ds.Tables(1).Rows(0)(0)
            tbTongLN.EditValue = ds.Tables(3).Rows(0)(0)
            tbTongLN.Caption = "Kết quả theo phiếu thu"
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TheoXuatKho()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim _ThangTinhKN As New DateTime
        Dim sql As String = " SET DATEFORMAT DMY "
        AddParameterWhere("@TuNgay", New DateTime(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month, 1))
        AddParameterWhere("@DenNgay", DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month + 1, 0)))
        AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
        AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Month, -1, Convert.ToDateTime(tbThang.EditValue)).ToString("MM/yyyy"))
        sql &= " DECLARE @TyLeQD as float "
        sql &= " SET @TyLeQD= (select TyLeQuyDoiDiem FROM tblQuyDoi WHERE ThangNam=@Thang)"

        sql &= " SELECT PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK,BANGCHAOGIA.Masodathang, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare,"
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - "
        If chkTheoGiaNhapTB.Checked Then
            sql &= " ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) "
        Else
            sql &= " ISNULL(tb.GiaNhap,0)"
        End If
        sql &= "    - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
        sql &= "      - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) "
        sql &= " ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END))) "
        sql &= " * "
        sql &= "    (ISNULL(PhanBoLoiNhuan.TongConLai,0)/100) "

        sql &= "  END) AS LoiNhuanKT,"
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - "
        If chkTheoGiaNhapTB.Checked Then
            sql &= " ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) "
        Else
            sql &= " ISNULL(tb.GiaNhap,0)"
        End If
        sql &= "    - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
        sql &= "    - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) "
        sql &= " ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END))) END)as TongLoiNhuan, "

        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - "
        If chkTheoGiaNhapTB.Checked Then
            sql &= " ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) "
        Else
            sql &= " ISNULL(tb.GiaNhap,0)"
        End If
        sql &= "    - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
        sql &= "      - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) "
        sql &= " ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END))) *"
        sql &= "    (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS,0) = 1 THEN (100 - ISNULL(PhanBoLoiNhuan.PhuTrachHopDong,0) - ISNULL(PhanBoLoiNhuan.XucTienKyHD,0)) / 100 ELSE (ISNULL(PhanBoLoiNhuan.TongConLai, 0) + ISNULL(PhanBoLoiNhuan.PhuTrachQuanLyCT, 0)) / 100 END) "
        sql &= "  END) as  TongLoiNhuanKT, "
        sql &= "    LoaiCongTrinh_LNThietKe.LNThietKe as PTLoiNhuanThietKe,"
        sql &= " (CASE ISNULL(BANGCHAOGIA.NhanKS,1) WHEN 1 THEN PhanBoLoiNhuan.PhuTrachChaoGia ELSE 0 END)PTLNChaoGia,"
        sql &= " PhanBoLoiNhuan.PhuTrachQuanLyCT as PTLNQuanLyCT"
        sql &= " INTO #tbLoiNhuanKT "
        sql &= " FROM PHIEUXUATKHO "
        sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null "
        sql &= " LEFT OUTER JOIN LoaiCongTrinh_LNThietKe ON BANGYEUCAU.IDLoaiYeuCau = LoaiCongTrinh_LNThietKe.IDLoaiCT AND LoaiCongTrinh_LNThietKe.Thang=@Thang "
        If chkTheoGiaNhapTB.Checked Then
            sql &= " LEFT JOIN View_XuatKhoTongGiaNhapTB ON PHIEUXUATKHO.Sophieu = View_XuatKhoTongGiaNhapTB.Sophieu "
        Else
            sql &= " LEFT OUTER JOIN"
            sql &= "        (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
            sql &= "          FROM          V_XuatkhoCal"
            sql &= "          GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu "
        End If
        sql &= " LEFT OUTER JOIN V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " LEFT OUTER JOIN V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " LEFT OUTER JOIN V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " LEFT OUTER JOIN V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"

        sql &= " LEFT JOIN PhanBoLoiNhuan ON PhanBoLoiNhuan.Thang=Right(Convert(nvarchar,PHIEUXUATKHO.NgayTHang,103),7) AND PhanBoLoiNhuan.CT=PHIEUXUATKHO.CongTrinh"
        sql &= " LEFT JOIN"
        sql &= " ( SELECT SUM(PhatTrienSanPham_LoiNhuan.SoLuong*PhatTrienSanPham_LoiNhuan.LoiNhuan) as LoiNhuanPTSP,PhatTrienSanPham_LoiNhuan.SoPhieu FROM PhatTrienSanPham_LoiNhuan"
        sql &= "    INNER JOIN PHIEUXUATKHO as PX ON PX.SoPhieu=PhatTrienSanPham_LoiNhuan.SoPhieu"
        sql &= "    AND CONVERT(datetime,Convert(nvarchar,PX.NgayThang,103),103) between @TuNgay AND @DenNgay "
        sql &= "    GROUP BY PhatTrienSanPham_LoiNhuan.SoPhieu)tbLNPTSP ON tbLNPTSP.SoPhieu=PHIEUXUATKHO.SoPhieu"

        sql &= " WHERE CONVERT(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
        End If

        sql &= " SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet,tblTuDien.NoiDung AS TenLoaiYC"
        sql &= " INTO #GTLD FROM"
        sql &= " (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,"
        sql &= " (Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
        sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,BANGYEUCAU.IDLoaiYeuCau,IDNoiDung,"
        sql &= " 		(SELECT Ma FROM tblTuDien WHERE ID = (SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung))IDNhomCV,Duyet"
        sql &= " FROM tblBaoCaoLichThiCong"
        sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
        sql &= " WHERE GiaoViec=0 AND tblBaoCaoLichThiCong.SoCG IN (SELECT DISTINCT SoPhieuCG FROM #tbLoiNhuanKT)"
        If cbTrangThai.EditValue = "Đã duyệt" Then
            sql &= " AND Duyet=1 "
        End If

        If Not cbSoYC.EditValue Is Nothing Then
            sql &= " AND SoYC=@SoYC "
            AddParameterWhere("@SoYC", cbSoYC.EditValue)
        End If

        sql &= " UNION ALL"
        sql &= " SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,"
        sql &= " (Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio,"
        sql &= " 	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien,BANGYEUCAU.IDLoaiYeuCau,IDNoiDung,"
        sql &= " 		Convert(int,1) as IDNhomCV,Duyet"
        sql &= " FROM tblBaoCaoLichThiCong"
        sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu"
        sql &= " WHERE GiaoViec=0 AND tblBaoCaoLichThiCong.IDNoiDung=62 AND tblBaoCaoLichThiCong.SoCG IN (SELECT DISTINCT SoPhieuCG FROM #tbLoiNhuanKT)"
        If cbTrangThai.EditValue = "Đã duyệt" Then
            sql &= " AND Duyet=1 "
        End If

        If Not cbSoYC.EditValue Is Nothing Then
            sql &= " AND SoYC=@SoYC "
            AddParameterWhere("@SoYC", cbSoYC.EditValue)
        End If

        sql &= " UNION ALL "

        sql &= " SELECT Convert(int,0) as ID,BANGCHAOGIA.SoPhieu as SoCG,PHIEUXUATKHO.NgayThang as Ngay,Convert(float,1) as SoGio,"
        sql &= " BANGCHAOGIA.Masodathang as SoYC,BANGCHAOGIA.IDPhuTrachCT as IDNgThucHien,BANGYEUCAU.IDLoaiYeuCau"
        sql &= " ,Convert(int,635) as IDNoiDung,Convert(int,4) as IDNhomCV,Convert(bit,1)Duyet"
        sql &= " FROM BANGCHAOGIA"
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=BANGCHAOGIA.MaSoDatHang"
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu"
        sql &= " AND CONVERT(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= " WHERE IDPhuTrachCT is not null"
        sql &= " )tbt INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYeuCau"

        

        sql &= " SELECT tb.IDNhanVien,tb.IDNhomKN,tb.Diem*ISNULL(tblTHChamCong.HSDanhGia,"
        sql &= " ISNULL((SELECT tbCC.HSDanhGia FROM tblTHChamCong as tbCC WHERE tbCC.IDNhanVien=tb.IDNhanVien "
        sql &= " AND tbCC.[Month]= Right(Convert(nvarchar, Dateadd(month,-1,Convert(Datetime,'01/'+ tblTHChamCong.[Month],103)),103),7)),0))"
        sql &= " as Diem,tb.Thang"
        sql &= " INTO  #tbKyNang"
        sql &= " FROM "
        sql &= " (SELECT IDNhanVien,IDNhomKN,SUM(Diem) Diem, Thang"
        sql &= " FROM NhanSu_DiemKyNang "
        sql &= " GROUP BY IDNhanVien,IDNhomKN,Thang)tb"
        sql &= " LEFT JOIN tblTHChamCong ON tblTHChamCong.IDNhanVien=tb.IDNhanVien AND tblTHChamCong.[Month]=tb.Thang"

        sql &= " select IDNgThucHien,IDLoaiYeuCau,IDNhomCV, RIGHT(Convert(nvarchar,Ngay,103),7)Thang,"
        sql &= " SoCG,IDNoiDung, SoGio,#GTLD.ID,Ngay,TenLoaiYC,SoYC,Duyet, "
        sql &= " (CASE #GTLD.IDNhomCV WHEN 4 THEN 1 ELSE "
        sql &= " (select SUM(#tbKyNang.Diem) FROM #tbKyNang WHERE #tbKyNang.IDNhomKN=(Case #GTLD.IDNhomCV WHEN 3 THEN 1 WHEN 4 THEN 2 ELSE #GTLD.IDNhomCV END) "
        sql &= " AND RIGHT(Convert(nvarchar,#GTLD.Ngay,103),7)=#tbKyNang.Thang AND #GTLD.IDNgThucHien=#tbKyNang.IDNhanVien) END)TongDiemKN "
        sql &= " INTO #DanhSachGTLDtmp"
        sql &= " FROM #GTLD"

        sql &= " SELECT #DanhSachGTLDtmp.* ,#tbLoiNhuanKT.SoPhieuXK,"
        sql &= " #tbLoiNhuanKT.LoiNhuanKT,#tbLoiNhuanKT.TongLoiNhuanKT,#tbLoiNhuanKT.TongLoiNhuan,"
        sql &= " (Case #DanhSachGTLDtmp.IDNhomCV WHEN 3 THEN #tbLoiNhuanKT.PTLNChaoGia * #tbLoiNhuanKT.TongLoiNhuan/100"
        sql &= "    WHEN 4 THEN #tbLoiNhuanKT.PTLNQuanLyCT * #tbLoiNhuanKT.TongLoiNhuan/100 "
        sql &= "    WHEN 1 THEN #tbLoiNhuanKT.LoiNhuanKT* ISNULL(#tbLoiNhuanKT.PTLoiNhuanThietKe,50)/100"
        sql &= "    WHEN 2 THEN #tbLoiNhuanKT.LoiNhuanKT*(100- ISNULL(#tbLoiNhuanKT.PTLoiNhuanThietKe,50))/100 END) as LoiNhuanNhomCV,"
        'sql &= " (#tbLoiNhuanKT.LoiNhuanKT *((Case #DanhSachGTLDtmp.IDNhomCV WHEN 1 THEN ISNULL(#tbLoiNhuanKT.PTLoiNhuanThietKe,50) "
        'sql &= " WHEN 2 THEN 100- ISNULL(#tbLoiNhuanKT.PTLoiNhuanThietKe,50) WHEN 3 THEN #tbLoiNhuanKT.PTLNChaoGia WHEN 4 THEN #tbLoiNhuanKT.PTLNQuanLyCT END)/100)) as LoiNhuanNhomCV,"
        sql &= " BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TenDuAn,KHACHHANG.ttcMa"
        sql &= " INTO #TongHop"
        sql &= " FROM #tbLoiNhuanKT"
        sql &= " INNER JOIN #DanhSachGTLDtmp ON #DanhSachGTLDtmp.SoCG=#tbLoiNhuanKT.SoPhieuCG"
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=#DanhSachGTLDtmp.SoCG"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"

        sql &= " SELECT SUM(TongDiemKN*SoGio)MauSo,SoCG,IDNhomCV"
        sql &= "  INTO #tbMauSo"
        sql &= " FROM #TongHop"
        sql &= " GROUP BY SoCG,IDNhomCV"

        sql &= " SELECT ID,Ngay,SoGio,SoYC,IDNhomCV,SoPhieuXK,"
        sql &= " (SoCG + N'  ' + ttcMa +N'  '+ ISNULL(TenDuAn,N'') + N'Tổng LN:  ' "
        sql &= " + replace(replace(replace(convert(varchar,convert(Money, round(ISNULL(TongLoiNhuan,0),2)),1),'.','-'),',','.'),'-',',') + N'  (KT: ' "
        sql &= " + replace(replace(replace(convert(varchar,convert(Money, round(ISNULL(TongLoiNhuanKT,0),2)),1),'.','-'),',','.'),'-',',') + N') '"
        sql &= " + ISNULL(TenLoaiYC,N'') )SoCG,"

        sql &= " ttcMa,IDNgThucHien,IDNoiDung,TongDiemKN AS KNNhanVien,"
        sql &= " TongSoGio AS TongGioNhomThucHien,(SoTien) as LoiNhuan,(SoTien* ISNULL(@TyLeQD,0)) AS Diem,"
        sql &= " ISNULL(TongLoiNhuanKT,0) AS TongLN,(ISNULL(LoiNhuanKT,0) * ISNULL(@TyLeQD,0)) AS TongDiem,TenDuAn,Duyet"
        sql &= " INTO #tbKetQua"
        sql &= " FROM("
        sql &= " SELECT #TongHop.*,tb.TongSoGio,"
        sql &= " (CASE WHEN #tbMauSo.MauSo = 0 THEN 0 ELSE"
        sql &= " ((#TongHop.SoGio*#TongHop.TongDiemKN) /#tbMauSo.MauSo) * #TongHop.LoiNhuanNhomCV END)  AS SoTien"
        sql &= " FROM #TongHop"
        sql &= " INNER JOIN "
        sql &= " (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG"
        sql &= " FROM "
        sql &= "  #TongHop"
        sql &= " GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=#TongHop.IDNhomCV AND tb.SoCG=#TongHop.SoCG"
        sql &= " INNER JOIN #tbMauSo ON #tbMauSo.SoCG=#TongHop.SoCG AND #tbMauSo.IDNhomCV=#TongHop.IDNhomCV"
        sql &= " )tb"
        sql &= " ORDER BY ID,SoCG,IDNoiDung"

        sql &= " SELECT * FROM #tbKetQua "
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " where IDNgThucHien IN (SELECT DISTINCT IDNhanVien FROM LUONG WHERE IDDepatment = @Phong AND Convert(Datetime,'01/'+[Month],103) BETWEEN @TuNgay AND @DenNgay)"
            AddParameterWhere("@Phong", cbPhong.EditValue)

            If Not cbNhanVien.EditValue Is Nothing Then
                sql &= " AND IDNgThucHien =@NgThucHien "
                AddParameterWhere("@NgThucHien", cbNhanVien.EditValue)
            End If
        Else
            If Not cbNhanVien.EditValue Is Nothing Then
                sql &= " WHERE IDNgThucHien =@NgThucHien "
                AddParameterWhere("@NgThucHien", cbNhanVien.EditValue)
            End If
        End If
        sql &= " ORDER BY SoCG,Ngay"
        sql &= " SELECT ISNULL(SUM(LoiNhuan),0) FROM #tbKetQua"
        sql &= " SELECT ISNULL(SUM(TongLN),0) FROM (SELECT DISTINCT SoCG,TongLN FROM #tbKetQua)tb "
        sql &= " SELECT ISNULL(Sum(TongLoiNhuanKT),0)LoiNhuanKT FROM #tbLoiNhuanKT "

        sql &= " DROP Table #GTLD"
        sql &= " DROP Table #DanhSachGTLDtmp"
        sql &= " DROP Table #TongHop"
        sql &= " DROP table #tbLoiNhuanKT"
        sql &= " DROP Table #tbKyNang"
        sql &= " DROP Table #tbMauSo"
        sql &= " DROP Table #tbKetQua"


        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            CloseWaiting()
            gdv.DataSource = ds.Tables(0)
            tbData = ds.Tables(0)
            tbTongLoiNhuan.EditValue = ds.Tables(2).Rows(0)(0)
            tbTongLoiNhuanBC.EditValue = ds.Tables(1).Rows(0)(0)
            tbTongLN.EditValue = ds.Tables(3).Rows(0)(0)
            tbTongLN.Caption = "Kết quả theo xuất kho"
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rgdvNoiDung_Popup(sender As System.Object, e As System.EventArgs) Handles rgdvNoiDung.Popup
        CType(sender, GridLookUpEdit).Properties.View.ExpandAllGroups()
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then Exit Sub
        If e.Column.FieldName = "Duyet" Then
            If IsDBNull(e.CellValue) Then
                gdvCT.SetRowCellValue(e.RowHandle, "Duyet", True)
            Else
                gdvCT.SetRowCellValue(e.RowHandle, "Duyet", Not e.CellValue)
            End If
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNLichThiCong
        f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If TaiKhoan <> gdvCT.GetFocusedRowCellValue("IDNguoiNhap") And TaiKhoan <> gdvCT.GetFocusedRowCellValue("IDNgThucHien") Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này !")
            Exit Sub
        End If
        AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Duyet FROM tblBaoCaoLichThiCong WHERE ID=@ID")
        If Not tb Is Nothing Then
            If tb.Rows(0)(0) Then
                ShowCanhBao("Nội dung này đã được duyệt, không thể sửa đổi !")
                Exit Sub
            End If
        End If
        TrangThai.isUpdate = True
        objID = gdvCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNLichThiCong
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "Duyet" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btDuyetBC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then Exit Sub
        Dim tg As DateTime = GetServerTime()
        Dim countErr As Integer = 0
        Dim count As Integer = 0
        For i As Integer = 0 To gdvCT.RowCount - 1
            If gdvCT.GetRowCellValue(i, "Modify") Then
                AddParameter("@Duyet", gdvCT.GetRowCellValue(i, "Duyet"))
                If gdvCT.GetRowCellValue(i, "Duyet") Then
                    AddParameter("@IDNgDuyet", TaiKhoan)
                    AddParameter("@NgayDuyet", tg)
                Else
                    AddParameter("@IDNgDuyet", DBNull.Value)
                    AddParameter("@NgayDuyet", DBNull.Value)
                End If

                AddParameterWhere("@ID", gdvCT.GetRowCellValue(i, "ID"))
                If doUpdate("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then
                    countErr += 1
                    gdvCT_RowStyle(gdvCT, New DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs(i, DevExpress.XtraGrid.Views.Base.GridRowCellState.Default, New DevExpress.Utils.AppearanceObject()))
                Else
                    gdvCT.SetRowCellValue(i, "IDNgDuyet", TaiKhoan)
                    gdvCT.SetRowCellValue(i, "NgayDuyet", tg)
                    gdvCT.SetRowCellValue(i, "Modify", False)
                    count += 1
                End If
            End If

        Next
        If countErr > 0 Then
            ShowCanhBao("Có: " & countErr & " lỗi xảy ra !")
        ElseIf count > 0 Then
            ShowAlert("Đã duyệt!")
        End If
    End Sub

    Private Sub gdvCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        e.Appearance.BackColor = Color.MistyRose
    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadDSNhanVien()
        cbNhanVien.EditValue = Nothing
    End Sub

    Private Sub tbTongLoiNhuanBC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTongLoiNhuanBC.EditValueChanged, tbTongLoiNhuan.EditValueChanged
        On Error Resume Next
        If tbTongLoiNhuan.EditValue = 0 Then Exit Sub
        tbPTLoiNhuan.EditValue = (tbTongLoiNhuanBC.EditValue / tbTongLoiNhuan.EditValue) * 100
    End Sub

    Private Sub btChiTiet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChiTiet.ItemClick
        If cbTieuChi.EditValue = "Phiếu thu" Then
            ShowWaiting("Đang tải dữ liệu ...")

            AddParameterWhere("@TuNgay", New DateTime(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month, 1))
            AddParameterWhere("@DenNgay", DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month + 1, 0)))
            Dim sql As String = " SET DATEFORMAT DMY "

            sql &= " SELECT tb.*,PHIEUXUATKHO.SoPhieu AS SoPhieuXK"
            sql &= " INTO #tbXKThuTienTrongThang"
            sql &= " FROM"
            sql &= " ("
            sql &= " SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THU"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= "  CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) Between @TuNgay And @DenNgay"
            sql &= " UNION ALL"
            sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THUNH"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) Between @TuNgay And @DenNgay"
            sql &= " )tb"
            sql &= " INNER JOIN PHIEUXUATKHO ON tb.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb.PhieuTC0 = PHIEUXUATKHO.SophieuCG"

            sql &= " SELECT SUM(SoTien)SoTien,PHIEUXUATKHO.SoPhieu"
            sql &= " INTO #tbLoiNhuan"
            sql &= " FROM"
            sql &= " (SELECT NgayThangCT, SoPhieu, IDkh, SoTien, MucDich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THU"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= @DenNgay"
            sql &= " UNION ALL"
            sql &= " SELECT ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
            sql &= " FROM THUNH"
            sql &= " WHERE (PhieuTC0 <> N'0000000' OR PhieuTC1 <> N'0000000') AND"
            sql &= " CONVERT(datetime,CONVERT(nvarchar,NgayThangCT,103),103) <= @DenNgay"
            sql &= " )tb2"
            sql &= " INNER JOIN PHIEUXUATKHO ON tb2.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tb2.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
            sql &= " WHERE PHIEUXUATKHO.SoPhieu IN ( SELECT DISTINCT SoPhieuXK FROM #tbXKThuTienTrongThang)"
            sql &= " GROUP BY PHIEUXUATKHO.SoPhieu"

            sql &= " SELECT  PHIEUXUATKHO.NgayThang AS Ngay,#tbLoiNhuan.Sotien AS TienThu, PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
            sql &= "    BANGCHAOGIA.MaSoDatHang, ISNULL(tb.GiaNhap, 0) AS GiaNhap, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0)"
            sql &= "      - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (#tbLoiNhuan.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) "
            sql &= " * (CASE ISNULL(BANGCHAOGIA.NhanKS, 1) "
            sql &= "  WHEN 1 THEN (100-KTPhanBoLoiNhuanCT.PhuTrachHopDong)/100 "
            sql &= " ELSE (100-KTPhanBoLoiNhuanCT.PhuTrachHopDong-KTPhanBoLoiNhuanCT.PhuTrachChaoGia)/100 END)"
            sql &= "     END) AS LoiNhuanKT, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0)"
            sql &= "     - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (#tbLoiNhuan.SoTien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) *  "
            sql &= " (CASE ISNULL(BANGCHAOGIA.NhanKS, 1) "
            sql &= "           WHEN 1 THEN KTPhanBoLoiNhuanCT.PhuTrachHopDong / 100 ELSE (KTPhanBoLoiNhuanCT.PhuTrachHopDong + KTPhanBoLoiNhuanCT.PhuTrachChaoGia) "
            sql &= "           / 100 END)"
            sql &= "    END) AS LoiNhuanKD, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare,tblTuDien.NoiDung AS LoaiYC,(CASE WHEN tbBC.SoCG IS NULL Then N'BC thiếu' ELSE N'' END)KT"
            sql &= "    ,KHACHHANG.ttcMa,BANGCHAOGIA.TenDuAn"
            sql &= " FROM  #tbLoiNhuan "
            sql &= " INNER JOIN PHIEUXUATKHO ON #tbLoiNhuan.SoPhieu = PHIEUXUATKHO.Sophieu "
            sql &= " LEFT JOIN (SELECT DISTINCT SoCG FROM tblBaoCaoLichThiCong WHERE Duyet=1 AND SoCG IS not null)tbBC ON PHIEUXUATKHO.SoPhieuCG=tbBC.SoCG"
            sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
            sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) "
            sql &= " 		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) "
            sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
            sql &= " INNER JOIN   BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null "
            sql &= " LEFT OUTER JOIN KTPhanBoLoiNhuanCT ON BANGYEUCAU.IDLoaiYeuCau = KTPhanBoLoiNhuanCT.ID "
            sql &= " LEFT JOIN View_XuatKhoTongGiaNhapTB ON PHIEUXUATKHO.Sophieu = View_XuatKhoTongGiaNhapTB.Sophieu"
            sql &= " LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
            sql &= "    V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
            sql &= " WHERE abs(SoTien - ((PHIEUXUATKHO.Tientruocthue+PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.TyGia))<=50000"
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
            End If
            sql &= " ORDER BY PHIEUXUATKHO.SoPhieu"

            sql &= " DROP table #tbXKThuTienTrongThang"
            sql &= " DROP table #tbLoiNhuan"
            sql &= " DROP table #tbLoiNhuanKT"

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                CloseWaiting()
                Dim f As New frmDanhSachXK
                f.gdv.DataSource = tb
                f.Text = "Danh sách các phiếu xuất kho thu được tiền trong tháng"
                f.Show()

            Else
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)

            End If
        Else
            ShowWaiting("Đang tải dữ liệu ...")

            AddParameterWhere("@TuNgay", New DateTime(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month, 1))
            AddParameterWhere("@DenNgay", DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbThang.EditValue).Year, Convert.ToDateTime(tbThang.EditValue).Month + 1, 0)))
            Dim sql As String = " SET DATEFORMAT DMY "

            sql &= " SELECT  PHIEUXUATKHO.NgayThang AS Ngay,(PHIEUXUATKHO.TienTruocThue + PHIEUXUATKHO.TienThue)*PHIEUXUATKHO.TyGia AS TienThu, PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
            sql &= "    BANGCHAOGIA.MaSoDatHang, ISNULL(tb.GiaNhap, 0) AS GiaNhap, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
            sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * (ISNULL(tblTuDien.Diem, 0) / 100) END) "
            sql &= "    AS LoiNhuanKT, "
            sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,"
            sql &= "     0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) "
            sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(tblQuyDoi.HSThuCK, 0.15))) "
            sql &= "    * ((100 - ISNULL(tblTuDien.Diem, 0)) / 100) "
            sql &= "    END) AS LoiNhuanKD, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare,tblTuDien.NoiDung AS LoaiYC,(CASE WHEN tbBC.SoCG IS NULL Then N'BC thiếu' ELSE N'' END)KT"
            sql &= "    ,KHACHHANG.ttcMa,BANGCHAOGIA.TenDuAn"
            sql &= " FROM  PHIEUXUATKHO "
            sql &= " LEFT JOIN (SELECT DISTINCT SoCG FROM tblBaoCaoLichThiCong WHERE duyet=1 AND SoCG IS not null)tbBC ON PHIEUXUATKHO.SoPhieuCG=tbBC.SoCG"
            sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
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
            sql &= " WHERE Convert(datetime, Convert(nvarchar, PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                sql &= " AND BANGYEUCAU.IDLoaiYeuCau NOT IN (100,101) "
            End If
            sql &= " ORDER BY PHIEUXUATKHO.SoPhieu"


            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                CloseWaiting()
                Dim f As New frmDanhSachXK
                f.gdv.DataSource = tb
                f.Text = "Danh sách các phiếu xuất kho trong tháng"
                f.Show()

            Else
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)

            End If
        End If


    End Sub

    Private Sub gdvCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCT.CustomSummaryCalculate
        On Error Resume Next
        If e.IsGroupSummary Then
            If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "IDNgThucHien" Then
                e.TotalValue = (gdvCT.GetGroupSummaryValue(e.GroupRowHandle, gdvCT.GroupSummary.Item(3)) / gdvCT.GetGroupSummaryValue(e.GroupRowHandle, gdvCT.GroupSummary.Item(2))) * 8

            End If
        End If
    End Sub

    'Private Sub gdvCT_CustomSummaryExists(sender As System.Object, e As DevExpress.Data.CustomSummaryExistEventArgs) Handles gdvCT.CustomSummaryExists
    '    On Error Resume Next
    '    If e.IsGroupSummary Then
    '        If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "IDNgThucHien" Then
    '            e.Item = (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn21) / gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn3)) * 8
    '        End If
    '    End If
    'End Sub

    Private Sub chkTheoGiaNhapTB_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkTheoGiaNhapTB.CheckedChanged
        If chkTheoGiaNhapTB.Checked Then
            chkTheoGiaNhapTB.Glyph = My.Resources.Checked
        Else
            chkTheoGiaNhapTB.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub mXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuatExcel.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub

        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "KetQuaLamViecKT_" & Convert.ToDateTime(tbThang.EditValue).ToString("yyyyMM") & ".xls"
        saveFile.OverwritePrompt = False
        If saveFile.ShowDialog = DialogResult.OK Then

            Try
                ShowWaiting("Đang kết xuất ...")
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                CloseWaiting()
            End Try

        End If
    End Sub

    Private Sub btChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChotSoLieu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If ShowCauHoi("Chốt số liệu ?") Then
            Try
                ShowWaiting("Đang chốt số liệu ...")
                ' Dim tb As DataTable = gdv.DataSource

                Dim tbNV As DataTable = New DataView(tbData).ToTable(True, "IDNgThucHien")
                Dim tbXK As DataTable = New DataView(tbData).ToTable(True, "SoPhieuXK")
                Dim c As Integer = 0
                For i As Integer = 0 To tbNV.Rows.Count - 1
                    For j As Integer = 0 To tbXK.Rows.Count - 1
                        Dim LNKS As Object = tbData.Compute("SUM(LoiNhuan)", "IDNgThucHien=" & tbNV.Rows(i)("IDNgThucHien") & " AND SoPhieuXK='" & tbXK.Rows(j)("SoPhieuXK") & "' AND IDNhomCV=3")
                        If Not IsDBNull(LNKS) Then
                            If LNKS > 0 Then

                                AddParameter("@KhaoSatCG", LNKS)
                                AddParameter("@SoPhieu", tbXK.Rows(j)("SoPhieuXK"))
                                AddParameter("@IDNhanVien", tbNV.Rows(i)("IDNgThucHien"))
                                If doInsert("PhieuXuatKho_LoiNhuan") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                Else
                                    c += 1
                                End If
                            End If
                        End If

                      
                        Dim LNTC As Object = tbData.Compute("SUM(LoiNhuan)", "IDNgThucHien=" & tbNV.Rows(i)("IDNgThucHien") & " AND SoPhieuXK='" & tbXK.Rows(j)("SoPhieuXK") & "' AND IDNhomCV<>3 AND IDNhomCV<>4")
                        If Not IsDBNull(LNTC) Then
                            If LNTC > 0 Then

                                AddParameter("@ThiCong", LNTC)
                                AddParameter("@SoPhieu", tbXK.Rows(j)("SoPhieuXK"))
                                AddParameter("@IDNhanVien", tbNV.Rows(i)("IDNgThucHien"))
                                If doInsert("PhieuXuatKho_LoiNhuan") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                Else
                                    c += 1
                                End If
                            End If
                        End If
                    Next

                Next
                CloseWaiting()
                ShowAlert("Đã cập nhật " & c.ToString & " thông tin lợi nhuận!")
            Catch ex As Exception
                CloseWaiting()
            End Try
            
        End If
        
    End Sub
End Class
