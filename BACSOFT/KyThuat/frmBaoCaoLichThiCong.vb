Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmBaoCaoLichThiCong
    Dim _exit As Boolean = False

    Private Sub frmBaoCaoLichThiCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        _exit = True
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
        tbDenNgay.EditValue = tg
        _exit = False
        LoadDSPhongBan()
        LoadDSBoPhan()
        LoadDSNhanVien()
        LoadrcbNV()
        LoadDSSoYC()
        LoadDSNoiDungCV()
        btTaiBaoCao.PerformClick()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            gdvCT.Columns("Duyet").OptionsColumn.AllowEdit = False
            gdvCT.Columns("PhuongTien").OptionsColumn.AllowEdit = False
        End If
    End Sub
    Public Sub LoadDSBoPhan()
        Dim tb As DataTable = ExecuteSQLDataTable(" select * from NhanSu_BoPhan ")
        If Not tb Is Nothing Then
            riLueBoPhan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
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
        Dim sql As String = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and TrangThai=1 and 1=1 "
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " and IDDepatment= " & cbPhong.EditValue
        End If
        If Not lueBoPhan.EditValue Is Nothing Then
            sql &= " and IDBoPhan= " & lueBoPhan.EditValue
        End If
        sql &= " order by IDBoPhan,NhanSu.ChucVu, ID"
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
        Dim sql As String = " SET DATEFORMAT DMY SELECT Sophieu,KHACHHANG.ttcMa,BANGYEUCAU.NoiDung "
        sql &= " FROM BANGYEUCAU INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
        Dim sqlWhere As String = " WHERE IDLoaiYeuCau Is not null "
        Dim sqlOrder As String = " ORDER BY SoPhieu DESC "
        If tbTuNgay.EditValue Is Nothing And Not tbDenNgay.EditValue Is Nothing Then
            sqlWhere &= " AND BANGYEUCAU.Ngaythang <= @DenNgay "
        ElseIf Not tbTuNgay.EditValue Is Nothing And tbDenNgay.EditValue Is Nothing Then
            sqlWhere &= " AND BANGYEUCAU.Ngaythang >= @TuNgay "
        ElseIf Not tbTuNgay.EditValue Is Nothing And Not tbDenNgay Is Nothing Then
            sqlWhere &= " AND BANGYEUCAU.Ngaythang Between @TuNgay And @DenNgay "
        End If
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql & sqlWhere & sqlOrder)
        If Not tb Is Nothing Then
            rcbSoYC.View.Columns.Clear()
            rcbSoYC.DataSource = tb
            With rcbSoYC.View.Columns
                Dim colSP = .AddField("Sophieu")
                colSP.Caption = "Số phiếu"
                colSP.VisibleIndex = 0
                colSP.Width = 80
                colSP.OptionsColumn.FixedWidth = True
                colSP.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
                Dim colTenHang = .AddField("ttcMa")
                colTenHang.Caption = "Mã KH"
                colTenHang.VisibleIndex = 1
                colTenHang.Width = 120
                colTenHang.OptionsColumn.FixedWidth = True
                colTenHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Tên công trình"
                colNoiDung.VisibleIndex = 2
                colNoiDung.Width = 250
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

            End With
        End If
    End Sub

    Private Sub tbTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTuNgay.EditValueChanged, tbDenNgay.EditValueChanged
        If _exit Then Exit Sub
        LoadDSSoYC()
        If CType(sender, DevExpress.XtraBars.BarEditItem).Name = tbDenNgay.Name Then
            tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
        End If
    End Sub
    Private Sub riLueBoPhan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueBoPhan.ButtonClick
        If e.Button.Index = 1 Then
            lueBoPhan.EditValue = Nothing
        End If
    End Sub
    Private Sub rtbTuNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbTuNgay.ButtonClick
        If e.Button.Index = 1 Then
            tbTuNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub rtbDenNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbDenNgay.ButtonClick
        If e.Button.Index = 1 Then
            tbDenNgay.EditValue = Nothing
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

        Dim sql As String = " SET DATEFORMAT DMY " & vbCrLf
        sql &= " DECLARE @TyLeQD as float " & vbCrLf
        sql &= " DECLARE @tblNhanVien AS TABLE (IdNhanVien nvarchar(350)) " & vbCrLf
        sql &= " DECLARE @tblThoiGian AS TABLE (NgayThi DATETIME)" & vbCrLf
        'sql &= " DECLARE @ThangNam As Nvarchar(50)"
        'sql &= " DECLARE @TuNgay As datetime"
        'sql &= " DECLARE @DenNgay As datetime"

        ' sql &= " SET @ThangNam = '02/2014'"

        sql &= " DECLARE @table sysname " & vbCrLf
        sql &= " SET @table = 'tblDinhMucDiem'" & vbCrLf
        sql &= " DECLARE @id_field sysname " & vbCrLf
        sql &= " SET @id_field = 'ID'" & vbCrLf
        sql &= " DECLARE @sql varchar(MAX)" & vbCrLf
        sql &= " SET @sql = 'SELECT * INTO ##tbDinhMuc FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(int,0) AS [IDNoiDungCV], '" & vbCrLf
        sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDLoaiYC],'" & vbCrLf
        sql &= "  +' CONVERT(sql_variant,N'''') AS [PTLoiNhuanKT] WHERE 1=0 '+CHAR(10)" & vbCrLf
        sql &= " SELECT @sql = @sql + 'UNION ALL SELECT '+@id_field+',IDNoiDungCV, N'''" & vbCrLf
        sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '" & vbCrLf
        sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '" & vbCrLf
        sql &= "   +CHAR(10)" & vbCrLf
        sql &= " FROM " & vbCrLf
        sql &= "   INFORMATION_SCHEMA.COLUMNS" & vbCrLf
        sql &= " WHERE " & vbCrLf
        sql &= "   TABLE_NAME = @table " & vbCrLf
        sql &= "   AND COLUMN_NAME <> @id_field  AND COLUMN_NAME <> convert(sysname,'IDNoiDungCV') AND COLUMN_NAME <> convert(sysname,'HeSo')" & vbCrLf
        sql &= " ORDER BY COLUMN_NAME" & vbCrLf
        sql &= "  SELECT @sql = @sql + ')tb'" & vbCrLf
        sql &= "  EXEC(@sql)" & vbCrLf

        sql &= " SET @TyLeQD= (select TyLeQuyDoiDiem FROM tblQuyDoi WHERE ThangNam=@ThangNam)" & vbCrLf

        sql &= " DECLARE @tbLoiNhuanKT table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	SoPhieuCG  nvarchar(25)," & vbCrLf
        sql &= " 	SoPhieuXK  nvarchar(25)," & vbCrLf
        sql &= " 	Masodathang  nvarchar(25)," & vbCrLf
        sql &= " 	IDLoaiYeuCau  int," & vbCrLf
        sql &= " 	IDTakecare  nvarchar(350)," & vbCrLf
        sql &= " 	LoiNhuanKT float" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= " INSERT INTO @tbLoiNhuanKT(SoPhieuCG,SoPhieuXK,Masodathang,IDLoaiYeuCau,IDTakecare,LoiNhuanKT)" & vbCrLf
        sql &= " SELECT PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK,BANGCHAOGIA.Masodathang, BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakecare," & vbCrLf
        sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0)" & vbCrLf
        sql &= "      - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) " & vbCrLf
        sql &= "    * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END))) * (ISNULL(tblTuDien.Diem, 0) / 100) END) AS LoiNhuanKT" & vbCrLf

        sql &= " FROM PHIEUXUATKHO " & vbCrLf
        sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.Ngaythang) " & vbCrLf
        sql &= " 		AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(PHIEUXUATKHO.Ngaythang) " & vbCrLf
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG " & vbCrLf
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang AND BANGYEUCAU.IDLoaiYeuCau is not null " & vbCrLf
        sql &= " LEFT OUTER JOIN tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID " & vbCrLf
        sql &= " LEFT OUTER JOIN" & vbCrLf
        sql &= "        (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap" & vbCrLf
        sql &= "          FROM          V_XuatkhoCal" & vbCrLf
        sql &= "          GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu " & vbCrLf
        sql &= " LEFT OUTER JOIN V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu " & vbCrLf
        sql &= " LEFT OUTER JOIN V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu " & vbCrLf
        sql &= " LEFT OUTER JOIN V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu " & vbCrLf
        sql &= " LEFT OUTER JOIN V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu" & vbCrLf
        sql &= " WHERE " & vbCrLf

        sql &= " PHIEUXUATKHO.SoPhieu IN (" & vbCrLf
        sql &= " SELECT DISTINCT SoPhieu FROM " & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " SELECT PHIEUXUATKHO.SoPhieu" & vbCrLf
        sql &= " FROM tblBaoCaoLichThiCong " & vbCrLf
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieuCG=tblBaoCaoLichThiCong.SoCG" & vbCrLf
        sql &= " WHERE GiaoViec=0 " & vbCrLf
        sql &= " AND CONVERT(datetime,CONVERT(nvarchar,tblBaoCaoLichThiCong.Ngay,103),103) Between @TuNgay And @DenNgay " & vbCrLf
        sql &= " )tbb" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= " DECLARE @GTLD table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	ID int," & vbCrLf
        sql &= " 	Ngay datetime," & vbCrLf
        sql &= " 	SoGio float," & vbCrLf
        sql &= " 	SoYC nvarchar(25)," & vbCrLf
        sql &= " 	SoCG nvarchar(25)," & vbCrLf
        sql &= " 	IDNgThucHien nvarchar(350)," & vbCrLf
        sql &= " 	IDLoaiYeuCau nvarchar(25)," & vbCrLf
        sql &= " 	IDNoiDung int," & vbCrLf
        sql &= " 	IDNhomCV int," & vbCrLf
        sql &= " 	Duyet bit," & vbCrLf
        sql &= " 	PhuongTien bit," & vbCrLf
        sql &= " 	TenLoaiYC nvarchar(max)" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= "  INSERT INTO @GTLD(ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet,PhuongTien,TenLoaiYC)" & vbCrLf

        sql &= "  SELECT tbt.ID,Ngay,SoGio,SoYC,SoCG,IDNgThucHien,IDLoaiYeuCau,IDNoiDung,IDNhomCV,Duyet,PhuongTien,tblTuDien.NoiDung AS TenLoaiYC" & vbCrLf
        sql &= " FROM"
        sql &= "  (SELECT tblBaoCaoLichThiCong.ID,tblBaoCaoLichThiCong.SoCG,tblBaoCaoLichThiCong.Ngay,(Convert(float,datediff(minute,tblBaoCaoLichThiCong.BatDau,tblBaoCaoLichThiCong.KetThuc))/60)SoGio," & vbCrLf
        sql &= "  	tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.IDNgThucHien," & vbCrLf
        sql &= "                                 'C'+Convert(nvarchar,BANGYEUCAU.IDLoaiYeuCau)IDLoaiYeuCau,BANGYEUCAU.IDLoaiYeuCau AS IDLoaiYC2,IDNoiDung," & vbCrLf
        sql &= "  		(SELECT Ma FROM tblTuDien WHERE ID = (SELECT IDP FROM tblTuDien WHERE ID=tblBaoCaoLichThiCong.IDNoiDung))IDNhomCV,Duyet,PhuongTien" & vbCrLf
        sql &= "  FROM tblBaoCaoLichThiCong" & vbCrLf
        sql &= "  INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC=BANGYEUCAU.SoPhieu" & vbCrLf
        sql &= "  WHERE GiaoViec=0 AND tblBaoCaoLichThiCong.SoCG IN (SELECT DISTINCT [@tbLoiNhuanKT].SoPhieuCG FROM @tbLoiNhuanKT)" & vbCrLf

        sql &= " )tbt INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=tbt.IDNoiDung AND ##tbDinhMuc.IDLoaiYC=tbt.IDLoaiYeuCau" & vbCrLf
        sql &= " 	INNER JOIN tblTuDien ON tblTuDien.ID=tbt.IDLoaiYC2" & vbCrLf

        sql &= " INSERT INTO @tblThoiGian" & vbCrLf
        sql &= " SELECT dateadd(Day,-1, convert(datetime,'01/'+Thang,103))  FROM " & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " SELECT DISTINCT right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7) AS Thang FROM tblBaoCaoLichThiCong " & vbCrLf
        sql &= " WHERE GiaoViec=0 AND Duyet=1 AND " & vbCrLf
        sql &= " 	SoCG IN (SELECT SoPhieuCG " & vbCrLf
        sql &= " 				FROM PHIEUXUATKHO " & vbCrLf
        sql &= " 				WHERE Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay" & vbCrLf
        sql &= " )" & vbCrLf
        sql &= " group BY right(Convert(nvarchar,Dateadd(Month,1,Ngay),103),7))tb" & vbCrLf

        sql &= " INSERT INTO @tblNhanVien" & vbCrLf
        sql &= " SELECT DISTINCT IDNgThucHien  FROM tblBaoCaoLichThiCong " & vbCrLf
        sql &= " WHERE GiaoViec=0 AND Duyet=1 AND " & vbCrLf
        sql &= " 	SoCG IN (SELECT SoPhieuCG " & vbCrLf
        sql &= " 				FROM PHIEUXUATKHO " & vbCrLf
        sql &= " 				WHERE Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= " DECLARE @tblDiemKyNang AS TABLE" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	IDNhanVien nvarchar(350)," & vbCrLf
        sql &= " 	IDKyNang INT," & vbCrLf
        sql &= " 	IDNhomKN INT," & vbCrLf
        sql &= " 	Diem FLOAT DEFAULT(0)," & vbCrLf
        sql &= " 	NgayThi DATETIME" & vbCrLf
        sql &= " )" & vbCrLf
        sql &= " INSERT INTO @tblDiemKyNang(IDNhanVien,IDKyNang,IDNhomKN,NgayThi)" & vbCrLf
        sql &= " SELECT " & vbCrLf
        sql &= " [@tblNhanVien].IdNhanVien," & vbCrLf
        sql &= " tblKyNang.ID as IDKyNang, tblKyNang.IdNhomKN as IDNhomKN," & vbCrLf
        sql &= " [@tblThoiGian].NgayThi" & vbCrLf
        sql &= " FROM @tblNhanVien " & vbCrLf
        sql &= " CROSS JOIN @tblThoiGian " & vbCrLf
        sql &= " CROSS JOIN (SELECT ID,IDNhomKN FROM NLDANHSACH WHERE Loai = 1)tblKyNang" & vbCrLf
        sql &= " ORDER BY [@tblNhanVien].IdNhanVien" & vbCrLf

        sql &= " UPDATE @tblDiemKyNang" & vbCrLf
        sql &= " SET Diem = (" & vbCrLf
        sql &= " 	ISNULL(" & vbCrLf
        sql &= " 		(SELECT TOP 1 Diem FROM tblDiemThiKyNang " & vbCrLf
        sql &= " 		WHERE IDNhanVien = [@tblDiemKyNang].IDNhanVien" & vbCrLf
        sql &= " 		AND IDKyNang = [@tblDiemKyNang].IDKyNang" & vbCrLf
        sql &= " 		AND NgayThi <= [@tblDiemKyNang].NgayThi ORDER BY NgayThi DESC)," & vbCrLf
        sql &= " 		(SELECT ROUND(Diem/2.0,2) FROM NLDanhSach WHERE ID = [@tblDiemKyNang].IDKyNang)" & vbCrLf
        sql &= " 	)" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= " DECLARE @tbKyNang table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	IDNhanVien nvarchar(350)," & vbCrLf
        sql &= " 	IDNhomKN int," & vbCrLf
        sql &= " 	Diem float," & vbCrLf
        sql &= " 	Thang nvarchar(20)" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= " INSERT INTO @tbKyNang(IDNhanVien,IDNhomKN,Diem,Thang)" & vbCrLf
        sql &= " SELECT IDNhanVien,IDNhomKN,SUM(Diem) Diem, RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)  as Thang" & vbCrLf
        sql &= " FROM @tblDiemKyNang " & vbCrLf
        sql &= " GROUP BY IDNhanVien,IDNhomKN,RIGHT(CONVERT(NVARCHAR,NgayThi,103),7)" & vbCrLf

        sql &= " Declare @DanhSachGTLDtmp table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	IDNgThucHien nvarchar(350)," & vbCrLf
        sql &= " 	IDLoaiYeuCau nvarchar(25)," & vbCrLf
        sql &= " 	IDNhomCV int," & vbCrLf
        sql &= " 	Thang nvarchar(25)," & vbCrLf
        sql &= " 	SoCG nvarchar(25)," & vbCrLf
        sql &= " 	IDNoiDung int," & vbCrLf
        sql &= " 	SoGio float," & vbCrLf
        sql &= " 	ID int,	" & vbCrLf
        sql &= " 	Ngay datetime," & vbCrLf
        sql &= " 	TenLoaiYC nvarchar(max)," & vbCrLf
        sql &= " 	SoYC nvarchar(25)," & vbCrLf
        sql &= " 	Duyet bit," & vbCrLf
        sql &= " 	PhuongTien bit," & vbCrLf
        sql &= " 	TongDiemKN float" & vbCrLf
        sql &= " )" & vbCrLf
        sql &= " INSERT INTO @DanhSachGTLDtmp(IDNgThucHien,IDLoaiYeuCau,IDNhomCV,Thang,SoCG,IDNoiDung,SoGio,ID,Ngay,TenLoaiYC,SoYC,Duyet,PhuongTien,TongDiemKN)" & vbCrLf
        sql &= " select [@GTLD].IDNgThucHien,[@GTLD].IDLoaiYeuCau,[@GTLD].IDNhomCV, RIGHT(Convert(nvarchar,[@GTLD].Ngay,103),7)Thang," & vbCrLf
        sql &= " [@GTLD].SoCG,[@GTLD].IDNoiDung, [@GTLD].SoGio,[@GTLD].ID,[@GTLD].Ngay,[@GTLD].TenLoaiYC,[@GTLD].SoYC,[@GTLD].Duyet,PhuongTien, " & vbCrLf
        sql &= " (select SUM([@tbKyNang].Diem) " & vbCrLf
        sql &= " 	FROM @tbKyNang " & vbCrLf
        sql &= " 	WHERE [@tbKyNang].IDNhomKN=[@GTLD].IDNhomCV " & vbCrLf
        sql &= " 	AND RIGHT(Convert(nvarchar,[@GTLD].Ngay,103),7)=[@tbKyNang].Thang " & vbCrLf
        sql &= " 	AND [@GTLD].IDNgThucHien=[@tbKyNang].IDNhanVien)TongDiemKN " & vbCrLf
        sql &= " FROM @GTLD" & vbCrLf
        sql &= " INNER JOIN tblTuDien ON Loai=11 AND tblTuDien.Ma=[@GTLD].IDNhomCV" & vbCrLf

        sql &= " Declare @TongHop table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	IDNgThucHien nvarchar(350)," & vbCrLf
        sql &= " 	IDLoaiYeuCau nvarchar(25)," & vbCrLf
        sql &= " 	IDNhomCV int," & vbCrLf
        sql &= " 	Thang nvarchar(25)," & vbCrLf
        sql &= " 	SoCG nvarchar(25)," & vbCrLf
        sql &= " 	IDNoiDung int," & vbCrLf
        sql &= " 	SoGio float," & vbCrLf
        sql &= " 	ID int,	" & vbCrLf
        sql &= " 	Ngay datetime," & vbCrLf
        sql &= " 	SoYC nvarchar(25)," & vbCrLf
        sql &= " 	Duyet bit," & vbCrLf
        sql &= " 	PhuongTien bit," & vbCrLf
        sql &= " 	TongDiemKN float," & vbCrLf
        sql &= " 	PTLoiNhuanKT float," & vbCrLf
        sql &= "	LoiNhuanNhomCV float," & vbCrLf
        sql &= " 	LoiNhuanKT float," & vbCrLf
        sql &= " 	SoPhieu nvarchar(25)" & vbCrLf
        sql &= " )" & vbCrLf
        sql &= " INSERT INTO @TongHop(IDNgThucHien,IDLoaiYeuCau,IDNhomCV,Thang,SoCG,IDNoiDung,SoGio,ID,Ngay,SoYC,Duyet,PhuongTien,TongDiemKN,PTLoiNhuanKT,LoiNhuanNhomCV,LoiNhuanKT,SoPhieu)" & vbCrLf
        sql &= " SELECT " & vbCrLf
        sql &= " [@DanhSachGTLDtmp].IDNgThucHien,[@DanhSachGTLDtmp].IDLoaiYeuCau,[@DanhSachGTLDtmp].IDNhomCV,[@DanhSachGTLDtmp].Thang,[@DanhSachGTLDtmp].SoCG,[@DanhSachGTLDtmp].IDNoiDung,[@DanhSachGTLDtmp].SoGio,[@DanhSachGTLDtmp].ID,[@DanhSachGTLDtmp].Ngay,[@DanhSachGTLDtmp].SoYC,[@DanhSachGTLDtmp].Duyet,PhuongTien,[@DanhSachGTLDtmp].TongDiemKN," & vbCrLf

        sql &= " Convert(float,##tbDinhMuc.PTLoiNhuanKT)PTLoiNhuanKT," & vbCrLf
        sql &= " ([@tbLoiNhuanKT].LoiNhuanKT *((Case WHEN [@DanhSachGTLDtmp].IDNhomCV=1 THEN ISNULL(tblTuDien.HeSo1,50) ELSE 100- ISNULL(tblTuDien.HeSo1,50) END)/100)) as LoiNhuanNhomCV," & vbCrLf
        sql &= " [@tbLoiNhuanKT].LoiNhuanKT,BANGCHAOGIA.SoPhieu" & vbCrLf
        sql &= " FROM @tbLoiNhuanKT" & vbCrLf
        sql &= " INNER JOIN @DanhSachGTLDtmp ON [@DanhSachGTLDtmp].SoCG=[@tbLoiNhuanKT].SoPhieuCG" & vbCrLf
        sql &= " INNER JOIN ##tbDinhMuc ON ##tbDinhMuc.IDNoiDungCV=[@DanhSachGTLDtmp].IDNoiDung AND ##tbDinhMuc.IDLoaiYC=[@DanhSachGTLDtmp].IDLoaiYeuCau" & vbCrLf
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=[@DanhSachGTLDtmp].SoCG" & vbCrLf
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang" & vbCrLf
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.ID=[@tbLoiNhuanKT].IDLoaiYeuCau" & vbCrLf

        sql &= " Declare @tbMauSo table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	MauSo float," & vbCrLf
        sql &= " 	SoCG nvarchar(25)," & vbCrLf
        sql &= " 	IDNhomCV int" & vbCrLf
        sql &= " )" & vbCrLf

        sql &= " INSERT INTO @tbMauSo(MauSo,SoCG,IDNhomCV)" & vbCrLf
        sql &= " SELECT SUM([@TongHop].TongDiemKN*[@TongHop].SoGio)MauSo,[@TongHop].SoCG,[@TongHop].IDNhomCV" & vbCrLf
        sql &= " FROM @TongHop" & vbCrLf
        sql &= " GROUP BY [@TongHop].SoCG,[@TongHop].IDNhomCV" & vbCrLf

        sql &= " Declare @tbKetQua table" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " 	ID int," & vbCrLf
        sql &= " 	Ngay datetime," & vbCrLf
        sql &= " 	SoGio int," & vbCrLf
        sql &= " 	SoYC nvarchar(25)," & vbCrLf
        '   sql &= " 	IDNgThucHIen int,"
        sql &= " 	IDNoiDung int," & vbCrLf
        sql &= " 	IDNhomCV int," & vbCrLf
        sql &= " 	KNNhanVien float," & vbCrLf
        sql &= " 	PTLoiNhuanNhomCV float," & vbCrLf
        sql &= " 	TongGioNhomThucHien float," & vbCrLf
        sql &= " 	LoiNhuan float," & vbCrLf
        sql &= " 	Diem float," & vbCrLf
        sql &= " 	TongLN float," & vbCrLf
        sql &= " 	TongDiem float," & vbCrLf
        sql &= " 	Duyet bit," & vbCrLf
        sql &= " 	PhuongTien bit" & vbCrLf
        sql &= " )" & vbCrLf
        sql &= " INSERT INTO @tbKetQua(ID,Ngay,SoGio,SoYC,IDNoiDung,IDNhomCV,KNNhanVien,PTLoiNhuanNhomCV,TongGioNhomThucHien,LoiNhuan,Diem,TongLN,TongDiem,Duyet,PhuongTien)" & vbCrLf
        sql &= " SELECT ID,Ngay,SoGio,SoYC,IDNoiDung,IDNhomCV,TongDiemKN AS KNNhanVien,PTLoiNhuanKT AS PTLoiNhuanNhomCV,TongSoGio AS TongGioNhomThucHien," & vbCrLf
        sql &= " 	ISNULL(SoTien,0)LoiNhuan,(ISNULL(SoTien,0)* ISNULL(@TyLeQD,0)) AS Diem,ISNULL(LoiNhuanKT,0) AS TongLN,(ISNULL(LoiNhuanKT,0) * ISNULL(@TyLeQD,0)) AS TongDiem,Duyet,PhuongTien" & vbCrLf
        sql &= " FROM(" & vbCrLf
        sql &= " SELECT [@TongHop].*,tb.TongSoGio," & vbCrLf
        sql &= " (CASE WHEN [@tbMauSo].MauSo = 0 THEN 0 ELSE" & vbCrLf
        sql &= " (([@TongHop].SoGio*[@TongHop].TongDiemKN) /[@tbMauSo].MauSo) * Convert(float,[@TongHop].LoiNhuanNhomCV) END)  AS SoTien" & vbCrLf
        sql &= " FROM @TongHop" & vbCrLf
        sql &= " INNER JOIN " & vbCrLf
        sql &= " (SELECT SUM(SoGio)TongSoGio,IDNhomCV,SoCG" & vbCrLf
        sql &= " FROM " & vbCrLf
        sql &= "  @TongHop" & vbCrLf
        sql &= " GROUP BY IDNhomCV,SoCG)tb ON tb.IDNhomCV=[@TongHop].IDNhomCV AND tb.SoCG=[@TongHop].SoCG" & vbCrLf
        sql &= " INNER JOIN @tbMauSo ON [@tbMauSo].SoCG=[@TongHop].SoCG AND [@tbMauSo].IDNhomCV=[@TongHop].IDNhomCV" & vbCrLf
        sql &= " )tb" & vbCrLf
        sql &= " ORDER BY ID,SoCG,IDNoiDung" & vbCrLf
        sql &= " DROP Table ##tbDinhMuc" & vbCrLf

        sql &= " declare @temp table (ddate datetime);" & vbCrLf
        sql &= " insert @temp" & vbCrLf
        sql &= " select DATEADD(DAY,number,@TuNgay) as [Date]" & vbCrLf
        sql &= " from master..spt_values" & vbCrLf
        sql &= " where type='p'  AND DATEADD(DAY,number,@TuNgay) <= @DenNgay" & vbCrLf
        sql &= " SELECT IsNULL(tb.Ngay,[@temp].ddate) As ThoiGian, tb.* , Convert(bit,0) Modify,Convert(bit,0)CN" & vbCrLf
        sql &= " FROM @temp" & vbCrLf
        sql &= " LEFT JOIN" & vbCrLf
        sql &= " (" & vbCrLf
        sql &= " SELECT tblBaoCaoLichThiCong.*,PHIEUXUATKHO.SoPhieu AS PhieuXK,KHACHHANG.ttcMa AS MaKH,[@tbKetQua].Diem,[@tbKetQua].LoiNhuan, (CASE WHEN SoCG IS NULL THEN BANGYEUCAU.NoiDung ELSE BANGCHAOGIA.TenDuAn END)TenDuAn, tblTuDien.NoiDung AS NoiDungThiCong " & vbCrLf
        sql &= " FROM tblBaoCaoLichThiCong " & vbCrLf
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=tblBaoCaoLichThiCong.SoYC " & vbCrLf
        sql &= " LEFT JOIN @tbKetQua ON [@tbKetQua].ID=tblBaoCaoLichThiCong.ID" & vbCrLf
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGYEUCAU.IDKhachhang " & vbCrLf
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=tblBaoCaoLichThiCong.IDNgThucHien " & vbCrLf
        sql &= " LEFT JOIN tblTuDien ON tblBaoCaoLichThiCong.IDNoiDung=tblTuDien.ID AND tblTuDien.Loai=6 " & vbCrLf
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=tblBaoCaoLichThiCong.SoCG " & vbCrLf
        sql &= " LEFT JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieuCG=tblBaoCaoLichThiCong.SoCG" & vbCrLf
        sql &= " WHERE GiaoViec=0 " & vbCrLf
        sql &= " AND CONVERT(datetime,CONVERT(nvarchar,tblBaoCaoLichThiCong.Ngay,103),103) Between @TuNgay And @DenNgay " & vbCrLf
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        AddParameterWhere("@ThangNam", Convert.ToDateTime(tbDenNgay.EditValue).ToString("MM/yyyy"))

        If Not cbNhanVien.EditValue Is Nothing Then
            sql &= " AND IDNgThucHien =@NgThucHien " & vbCrLf
            AddParameterWhere("@NgThucHien", cbNhanVien.EditValue.ToString)
        Else
            If Not cbPhong.EditValue Is Nothing Then
                sql &= " AND NHANSU.IDDepatment =@Phong " & vbCrLf
                AddParameterWhere("@Phong", cbPhong.EditValue)
            End If
        End If

        If Not cbSoYC.EditValue Is Nothing Then
            sql &= " AND SoYC=@SoYC " & vbCrLf
            AddParameterWhere("@SoYC", CType(cbSoYC.EditValue, DataRowView).Item(0))
        End If

        sql &= " )tb ON tb.Ngay=[@temp].ddate ORDER BY ThoiGian,BatDau" & vbCrLf

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            If chkRutGon.Checked Then
                gdvCT.BeginUpdate()

                If gdvCT.RowCount > 1 Then
                    Dim _tmp = gdvCT.GetRowCellValue(0, "ThoiGian")
                    If CType(gdvCT.GetRowCellValue(0, "ThoiGian"), DateTime).DayOfWeek = DayOfWeek.Sunday Then
                        gdvCT.SetRowCellValue(0, "CN", True)
                    End If
                    For i As Integer = 1 To gdvCT.RowCount - 1
                        If CType(gdvCT.GetRowCellValue(i, "ThoiGian"), DateTime).DayOfWeek = DayOfWeek.Sunday Then
                            gdvCT.SetRowCellValue(i, "CN", True)
                        End If
                        If gdvCT.GetRowCellValue(i, "ThoiGian") = _tmp Then
                            gdvCT.SetRowCellValue(i, "ThoiGian", DBNull.Value)
                        Else
                            _tmp = gdvCT.GetRowCellValue(i, "ThoiGian")
                        End If
                    Next
                End If


                gdvCT.EndUpdate()
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rgdvNoiDung_Popup(sender As System.Object, e As System.EventArgs) Handles rgdvNoiDung.Popup
        CType(sender, GridLookUpEdit).Properties.View.ExpandAllGroups()
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetRowCellValue(e.RowHandle, "Ngay", tbDenNgay.EditValue)
        gdvCT.SetRowCellValue(e.RowHandle, "IDNgNhap", TaiKhoan)
        gdvCT.SetRowCellValue(e.RowHandle, "IDNgThucHien", cbNhanVien.EditValue)
        gdvCT.SetRowCellValue(e.RowHandle, "NgayNhap", GetServerTime)
        gdvCT.SetRowCellValue(e.RowHandle, "Duyet", False)
        gdvCT.SetRowCellValue(e.RowHandle, "PhuongTien", False)
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then Exit Sub
        If e.Column.FieldName = "Duyet" Then
            If IsDBNull(e.CellValue) Then
                gdvCT.SetRowCellValue(e.RowHandle, "Duyet", True)
                gdvCT.SetRowCellValue(e.RowHandle, "PhuongTien", True)
            Else
                Dim trangthai = e.CellValue
                gdvCT.SetRowCellValue(e.RowHandle, "Duyet", Not trangthai)
                gdvCT.SetRowCellValue(e.RowHandle, "PhuongTien", Not trangthai)
            End If
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
        End If
        If e.Column.FieldName = "PhuongTien" Then
            If IsDBNull(e.CellValue) And gdvCT.GetRowCellValue(e.RowHandle, "Duyet") = True Then
                gdvCT.SetRowCellValue(e.RowHandle, "PhuongTien", True)
            Else
                gdvCT.SetRowCellValue(e.RowHandle, "PhuongTien", Not e.CellValue)
            End If
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNLichThiCong
        f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
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
        If e.Column.FieldName = "PhuongTien" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btDuyetBC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDuyetBC.ItemClick, mDuyet.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này")
        End If
        Dim tg As DateTime = GetServerTime()
        Dim countErr As Integer = 0
        Dim count As Integer = 0
        For i As Integer = 0 To gdvCT.RowCount - 1
            If IsDBNull(gdvCT.GetRowCellValue(i, "Modify")) Then Continue For
            If gdvCT.GetRowCellValue(i, "Modify") Then
                AddParameter("@Duyet", gdvCT.GetRowCellValue(i, "Duyet"))
                AddParameter("@PhuongTien", gdvCT.GetRowCellValue(i, "PhuongTien"))
                If IsDBNull(gdvCT.GetRowCellValue(i, "Duyet")) Then

                    AddParameter("@IDNgDuyet", DBNull.Value)
                    AddParameter("@NgayDuyet", DBNull.Value)

                Else
                    If gdvCT.GetRowCellValue(i, "Duyet") Then
                        AddParameter("@IDNgDuyet", TaiKhoan)
                        AddParameter("@NgayDuyet", tg)
                    Else
                        AddParameter("@IDNgDuyet", DBNull.Value)
                        AddParameter("@NgayDuyet", DBNull.Value)
                    End If
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

    Private Sub mTichBoTich_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTichBoTich.ItemClick
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If gdvCT.RowCount <= 0 Then Exit Sub
        Dim _Check As Boolean = Not gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "Duyet")
        gdvCT.BeginUpdate()
        For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
            gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "Duyet", _Check)
        Next
        gdvCT.EndUpdate()
    End Sub


    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvCT_RowStyle_1(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvCT.RowStyle
        If gdvCT.GetRowCellValue(e.RowHandle, "CN") Then
            e.Appearance.BackColor = Color.LightSteelBlue
        End If
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub
End Class
