Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base

Public Class frmXuLyYCCongTrinh
    Private KTChaoGia As Boolean
    Private _exit As Boolean
    Private _countKH As Int32

    Private Sub frmYeuCauDen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _exit = False
        cbTieuChi.EditValue = "Top 500"
        KTChaoGia = False
        Dim _ThoiGian As DateTime = GetServerTime()
        tbTuNgay.EditValue = New DateTime(_ThoiGian.Year, _ThoiGian.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = _ThoiGian.Date
        tbDenNgay.Enabled = False
        ' KiemTraNguoiDung()

        loadCbTuDien()
        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        LoadCbTGCapHang()
        LoadDSYeuCau()

    End Sub

#Region "Load DS dữ liệu vào các combobox và grid"

    'Private Sub KiemTraNguoiDung()
    '    Select Case Me.Tag
    '        Case "KINHDOANH"
    '            mNhanChuyenMa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    '            'btNhanChuyenMa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    '        Case "KYTHUAT"
    '            colTrangThai.OptionsColumn.AllowEdit = False
    '            colGiaCungUng.OptionsColumn.AllowEdit = False
    '        Case "MUAHANG"
    '            colTrangThai.OptionsColumn.AllowEdit = False
    '    End Select
    'End Sub

    Public Sub LoadDSYeuCau()
        Try
            If Not chkTuDong.Checked Then
                ShowWaiting("Đang tải dữ liệu ...")
            End If

            Dim sql As String = " SET DATEFORMAT DMY "
            If Not cbTrangThaiYC.EditValue Is Nothing Then
                sql &= "SELECT Sophieu INTO #tblSoPhieu FROM YEUCAUDEN WHERE TrangThai = " & cbTrangThaiYC.EditValue
            End If

            If cbTieuChi.EditValue = "Top 500" Then
                sql &= " SELECT Top 500 BANGYEUCAU.ID,Ngaythang,Sophieu,IDKhachhang,KHACHHANG.ttcMa, "
            Else
                sql &= " SELECT BANGYEUCAU.ID,Ngaythang,Sophieu,IDKhachhang,KHACHHANG.ttcMa, "
            End If

            sql &= "	KHACHHANG.Ten AS TenKH,BANGYEUCAU.IDUser,BANGYEUCAU.IDNgd,NHANSU1.Ten as TakeCare,NGUOILAP.Ten AS NguoiLap,BANGYEUCAU.IDTakecare, "
            sql &= "    NHANSU2.Ten AS NguoiGD,NHANSU2.Mobile AS DienThoaiNgd,NHANSU2.Email as EmailNgd,ISNULL(CongTrinh,CONVERT(Bit,0))CongTrinh, "
            sql &= " BANGYEUCAU.NoiDung, BANGYEUCAU.FileDinhKem,BANGYEUCAU.TrangThai,BANGYEUCAU.IDLoaiYeuCau, BANGYEUCAU.IDNhanXL,NGUOIXL.Ten AS NguoiNhanXL,BANGYEUCAU.ThoiGianNhanXL, "
            'Tai
            sql &= " GhiChuGV,"
            'Tai
            sql &= " round( Convert(float,datediff(hour,NgayThang,isnull(ThoiGianDaXL,getdate())))/24,2)GioXL"
            sql &= " INTO #tb"
            sql &= " FROM BANGYEUCAU "
            sql &= " LEFT OUTER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
            sql &= " LEFT OUTER JOIN NHANSU AS NHANSU1 ON BANGYEUCAU.IDTakecare=NHANSU1.ID "
            sql &= " LEFT OUTER JOIN NHANSU AS NHANSU2 ON BANGYEUCAU.IDNgd=NHANSU2.ID "
            sql &= " LEFT OUTER JOIN NHANSU AS NGUOILAP ON BANGYEUCAU.IDUser=NGUOILAP.ID "
            sql &= " LEFT OUTER JOIN NHANSU AS NGUOIXL ON BANGYEUCAU.IDNhanXL=NGUOIXL.ID "
            sql &= " WHERE Congtrinh=1 "

            If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
                AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
                AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
                sql &= "	AND CONVERT(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
            End If

            If Not cbTrangThaiYC.EditValue Is Nothing Then
                sql &= "  AND (Sophieu IN (SELECT SoPhieu FROM #tblSoPhieu) OR BANGYEUCAU.TrangThai = " & cbTrangThaiYC.EditValue & ")"
            End If

            If Not cbNVKD.EditValue Is Nothing Then
                sql &= " AND BANGYEUCAU.IDTakecare= " & cbNVKD.EditValue
            End If

            If Not cbKH.EditValue Is Nothing Then
                sql &= " AND BANGYEUCAU.IDKhachhang= " & cbKH.EditValue
            End If

            If cbTieuChi.EditValue = "Top 500" Then
                sql &= " ORDER BY Sophieu DESC"
            End If

            sql &= " SELECT * FROm #tb "

            sql &= " SELECT tblXuLyYeuCau.ID,tblXuLyYeuCau.Ngay,IDYeuCau,NoiDungXuLy,tblXuLyYeuCau.FileDinhKem,NHANSU.Ten AS NguoiXuLy"
            sql &= " FROM tblXuLyYeuCau LEFT OUTER JOIN NHANSU ON tblXuLyYeuCau.IDNgXuLy=NHANSU.ID"
            sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.ID=tblXuLyYeuCau.IDYeuCau WHERE IDYeuCau IN (SELECT ID FROM #tb) "

            sql &= " DROP TABLE #tb "
            If Not cbTrangThaiYC.EditValue Is Nothing Then sql &= "	DROP TABLE #tblSoPhieu"

            Dim dt As DataSet = ExecuteSQLDataSet(sql)
            If Not dt Is Nothing Then
                Dim View As New DataView(dt.Tables(0))
                _countKH = View.ToTable(True, "ttcMa").Rows.Count
                gdvCT.Columns("Sophieu").Caption = dt.Tables(0).Rows.Count.ToString & " YC"
                gdvCT.Columns("ttcMa").Caption = _countKH & " KH"
                ' Try
                dt.Relations.Add(dt.Tables(0).Columns("ID"), dt.Tables(1).Columns("IDYeuCau"))
                dt.Relations.Item(0).RelationName = "Quá trình xử lý"

                gdv.DataSource = dt.Tables(0)


                If Not gdvCT.FocusedRowHandle < 0 Then
                    loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
                Else
                    gdvYC.DataSource = Nothing
                End If
            Else
                Throw New Exception(LoiNgoaiLe)
            End If

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        Finally
            If Not chkTuDong.Checked Then
                CloseWaiting()
            End If
        End Try


    End Sub

    Public Sub LoadDSYeuCau2(ByVal _SoPhieu As Object)
        If Not chkTuDong.Checked Then
            ShowWaiting("Đang tải dữ liệu ...")
        End If
        Dim _trangThai As String = ""

        Dim sql As String = " SET DATEFORMAT DMY "

        sql &= " SELECT BANGYEUCAU.ID,Ngaythang,Sophieu,IDKhachhang,KHACHHANG.ttcMa, "
        sql &= "	KHACHHANG.Ten AS TenKH,BANGYEUCAU.IDUser,BANGYEUCAU.IDNgd,NHANSU1.Ten as TakeCare,BANGYEUCAU.IDTakecare, "
        sql &= "    NHANSU2.Ten AS NguoiGD,ISNULL(CongTrinh,CONVERT(Bit,0))CongTrinh, "
        sql &= " BANGYEUCAU.NoiDung, BANGYEUCAU.FileDinhKem,BANGYEUCAU.TrangThai,BANGYEUCAU.IDLoaiYeuCau, BANGYEUCAU.IDNhanXL,NGUOIXL.Ten AS NguoiNhanXL,BANGYEUCAU.ThoiGianNhanXL "
        sql &= " FROM BANGYEUCAU "
        sql &= " LEFT OUTER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
        sql &= " LEFT OUTER JOIN NHANSU AS NHANSU1 ON BANGYEUCAU.IDTakecare=NHANSU1.ID "
        sql &= " LEFT OUTER JOIN NHANSU AS NHANSU2 ON BANGYEUCAU.IDNgd=NHANSU2.ID "
        sql &= " LEFT OUTER JOIN NHANSU AS NGUOIXL ON BANGYEUCAU.IDNhanXL=NGUOIXL.ID "
        sql &= " WHERE BANGYEUCAU.SoPhieu=" & _SoPhieu

        sql &= " SELECT tblXuLyYeuCau.ID,tblXuLyYeuCau.Ngay,IDYeuCau,NoiDungXuLy,tblXuLyYeuCau.FileDinhKem,NHANSU.Ten AS NguoiXuLy"
        sql &= " FROM tblXuLyYeuCau LEFT OUTER JOIN NHANSU ON tblXuLyYeuCau.IDNgXuLy=NHANSU.ID"
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.ID=tblXuLyYeuCau.IDYeuCau AND BANGYEUCAU.Sophieu= " & _SoPhieu

        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            ' Try
            dt.Relations.Add(dt.Tables(0).Columns("ID"), dt.Tables(1).Columns("IDYeuCau"))
            dt.Relations.Item(0).RelationName = "Quá trình xử lý"

            gdv.DataSource = dt.Tables(0)

            If Not gdvCT.FocusedRowHandle < 0 Then
                loadDSYCChiTiet(_SoPhieu)
            Else
                gdvYC.DataSource = Nothing
            End If
        Else
            If Not chkTuDong.Checked Then
                CloseWaiting()
            End If
            ShowBaoLoi(LoiNgoaiLe)
            ShowBaoLoi(sql)
        End If
        If Not chkTuDong.Checked Then
            CloseWaiting()
        End If

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)
        chkChonYC.Checked = False
        'Dim sql As String = " SELECT *,Convert(bit,0)Chon,(0)STT FROM View_YeuCauDen WHERE Sophieu= " & SoPhieu
        Dim sql As String = ""
        sql &= " DECLARE @tb table"
        sql &= " ("
        sql &= " 	Chon bit,"
        sql &= " 	STT int,"
        sql &= " 	TenVT nvarchar(500),"
        sql &= " 	TenHang nvarchar(500),"
        sql &= " 	Model nvarchar(500),"
        sql &= " 	DVT nvarchar(100),"
        sql &= " 	Thongso nvarchar(MAX), "
        sql &= " 	TenNuoc nvarchar(500),"
        sql &= " 	TenTaiLieu nvarchar(MAX),"
        sql &= " 	ConSX bit,"
        sql &= " 	TenNhom nvarchar(500),"
        sql &= " 	ID int, "
        sql &= " 	slTon float, "
        sql &= " 	Sophieu nvarchar(15),"
        sql &= " 	Noidung nvarchar(MAX),"
        sql &= " 	Soluong float,"
        sql &= " 	Mucdocan int,"
        sql &= " 	IDVattu int, "
        sql &= " 	IDHoithongtin int,"
        sql &= " 	IDChuyenma int,"
        sql &= " 	IDHoigia int,"
        sql &= " 	NgayNhanYeucau datetime,"
        sql &= " 	NgayHoithongtin datetime, "
        sql &= " 	NgayNhanChuyenma datetime,"
        sql &= " 	NgayChuyenma datetime,"
        sql &= " 	NgayNhanHoiGia datetime,"
        sql &= " 	NgayHoigia datetime, "
        sql &= " 	Trangthai tinyint,"
        sql &= " 	Hoithongtin nvarchar(MAX),"
        sql &= " 	IDNhom int,"
        sql &= " 	IDNuoc int,"
        sql &= " 	MaLoi bit, "
        sql &= " 	IDDVT int,"
        sql &= " 	GiaCungUng float,"
        sql &= " 	TGCungUng int,"
        sql &= " 	FileDinhKem nvarchar(MAX), "
        sql &= " 	IDkhachhang int, "
        sql &= " 	IDTienTeCungUng int,"
        sql &= " 	IDUser int, "
        sql &= " 	IDNgd int, "
        sql &= " 	IDTakecare int,"
        sql &= " 	ttcMa nvarchar(250), "
        sql &= " 	HangTon bit,"
        sql &= " 	NguoiNhanBaoGia  nvarchar(250)"
        sql &= " )"
        sql &= " INSERT INTO @tb"
        sql &= " SELECT Convert(bit,0)Chon,ROW_NUMBER() OVER(ORDER BY BANGYEUCAU.ID ) AS STT, TENVATTU.ten AS TenVT, TENHANGSANXUAT.TEN AS TenHang, VATTU.Model, TENDONVITINH.TEN AS DVT, VATTU.Thongso, "
        sql &= "      TENNUOC.TEN AS TenNuoc, TENTAILIEU.TEN AS TenTaiLieu, VATTU.ConSX, TENNHOM.Ten AS TenNhom, YEUCAUDEN.ID, "
        sql &= "      ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=YEUCAUDEN.IDVatTu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=YEUCAUDEN.IDVatTu)) AS slTon, "
        sql &= "      YEUCAUDEN.Sophieu, YEUCAUDEN.Noidung, YEUCAUDEN.Soluong, YEUCAUDEN.Mucdocan, YEUCAUDEN.IDVattu, "
        sql &= "      YEUCAUDEN.IDHoithongtin, YEUCAUDEN.IDChuyenma, YEUCAUDEN.IDHoigia, YEUCAUDEN.NgayNhanYeucau, YEUCAUDEN.NgayHoithongtin, "
        sql &= "      YEUCAUDEN.NgayNhanChuyenma, YEUCAUDEN.NgayChuyenma, YEUCAUDEN.NgayNhanHoigia, YEUCAUDEN.NgayHoigia, "
        sql &= "      YEUCAUDEN.Trangthai, YEUCAUDEN.Hoithongtin, TENNHOM.ID AS IDNhom, TENNUOC.ID AS IDNuoc, VATTU.MaLoi, "
        sql &= "      TENDONVITINH.ID AS IDDVT, YEUCAUDEN.GiaCungUng, YEUCAUDEN.TGCungUng, YEUCAUDEN.FileDinhKem, BANGYEUCAU.IDkhachhang, "
        sql &= "      YEUCAUDEN.IDTienTeCungUng, BANGYEUCAU.IDUser, BANGYEUCAU.IDNgd, BANGYEUCAU.IDTakecare, KHACHHANG.ttcMa, "
        sql &= "      VATTU.HangTon,NGUOINHANBAOGIA.Ten AS NguoiNhanBaoGia"
        sql &= " FROM    BANGYEUCAU LEFT OUTER JOIN"
        sql &= "      KHACHHANG ON BANGYEUCAU.IDkhachhang = KHACHHANG.ID RIGHT OUTER JOIN"
        sql &= "      YEUCAUDEN ON BANGYEUCAU.Sophieu = YEUCAUDEN.Sophieu LEFT OUTER JOIN"
        sql &= "      VATTU ON YEUCAUDEN.IDVattu = VATTU.ID LEFT OUTER JOIN"
        sql &= "      TENNHOM ON VATTU.IDTennhom = TENNHOM.ID LEFT OUTER JOIN"
        sql &= "      TENTAILIEU ON VATTU.IDTentailieu = TENTAILIEU.ID LEFT OUTER JOIN"
        sql &= "      TENNUOC ON VATTU.IDTennuoc = TENNUOC.ID LEFT OUTER JOIN"
        sql &= "      TENDONVITINH ON VATTU.IDDonvitinh = TENDONVITINH.ID LEFT OUTER JOIN"
        sql &= "      TENVATTU ON VATTU.IDTenvattu = TENVATTU.ID LEFT OUTER JOIN"
        sql &= "      TENHANGSANXUAT ON VATTU.IDHangSanxuat = TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOINHANBAOGIA ON YEUCAUDEN.IDNhanBaoGia=NGUOINHANBAOGIA.ID"
        sql &= " WHERE YEUCAUDEN.Sophieu=N'" & SoPhieu & "'"

        sql &= " SELECT * FROM @tb"

        'sql &= " SELECT  tblQuaTrinhBaoGia.ID,IDYeuCau,ThoiGianBaoGia, NHANSU.Ten AS NguoiBaoGia,"
        'sql &= " Gia,tblTuDien.NoiDung AS ThoiGianCungUng,tblTienTe.Ten AS TienTe,GhiChu,ChaoGia,DatHang,ThoiGianChaoGia,ThoiGianDatHang,IDTienTe,IDTGCungUng,IDCungUng"
        'sql &= " FROM tblQuaTrinhBaoGia"
        'sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblQuaTrinhBaoGia.IDCungUng AND NHANSU.Noictac=74"
        'sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=tblQuaTrinhBaoGia.IDTGCungUng AND tblTuDien.Loai=4"
        'sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=tblQuaTrinhBaoGia.IDTienTe"
        'sql &= " WHERE tblQuaTrinhBaoGia.IDYeuCau IN (SELECT DISTINCT ID FROM @tb)"

        Dim ds As DataTable = ExecuteSQLDataTable(sql)

        If Not ds Is Nothing Then
            'gdvYCCT.Columns("Noidung").Caption = "Nội dung (" & ds.Tables(0).Rows.Count.ToString & ")"
            'ds.Relations.Add(ds.Tables(0).Columns("ID"), ds.Tables(1).Columns("IDYeuCau"))
            'ds.Relations.Item(0).RelationName = "Quá trình báo giá"

            gdvYC.DataSource = ds

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub gdvYCCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvYCCT.CustomDrawCell
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.VisibleIndex = 0 And view.IsMasterRowEmptyEx(e.RowHandle, 0) And view.IsMasterRowEmptyEx(e.RowHandle, 1) Then
            CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty
        End If
    End Sub

    Private Sub gdvCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvCT.CustomDrawCell
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.VisibleIndex = 0 And view.IsMasterRowEmptyEx(e.RowHandle, 0) And view.IsMasterRowEmptyEx(e.RowHandle, 1) Then
            CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty
        End If
    End Sub

    Public Sub loadCbTuDien()
        Dim sql As String = ""
        sql &= " SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=0 ORDER BY Ma"
        sql &= " SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=1 ORDER BY Ma"
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        sql &= " SELECT ID,TEN FROM TENDONVITINH "
        sql &= " SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa "
        sql &= " SELECT ID,Ten,TyGia FROM tblTienTe  "
        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            cbTrangThai.DataSource = dt.Tables(0)
            rcbTrangThaiYC.DataSource = dt.Tables(0)

            cbTrangThaiYC.EditValue = TrangThaiYeuCau.CanChuyenMa

            cbMucDoCan.DataSource = dt.Tables(1)
            cbNhanVien.DataSource = dt.Tables(2)
            cbDVT.DataSource = dt.Tables(3)
            rCbNhanVien.DataSource = dt.Tables(2)
            rcbTrangThai.DataSource = dt.Tables(0)

            rcbKH.DataSource = dt.Tables(4)
            rbtFilterMaKH.DataSource = dt.Tables(4)
            rcbTienTe.DataSource = dt.Tables(5)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadDSVatTuDungChuyenMa()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sqlWhere As String = " WHERE Maloi=0 "

        Dim sql As String = " Select NULL AS CanhBao,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS slTon, "
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
        sql &= " Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
        sql &= " Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"

        sql &= " VATTU.Gianhap1 AS Gianhap, tblTienTe.Ten AS Tiente,TonNCC,"
        sql &= " TENNUOC.Ten AS Xuatxu,convert(float,0) AS SLYC, VATTU.ThongDung,VATTU.HangTon,VATTU.HinhAnh,(convert(image,NULL))HienThi,VATTU.TaiLieu,VATTU.ConSX "
        sql &= " from VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "

        If Not btFilterMaKH.EditValue Is Nothing Then
            sql &= " INNER JOIN (SELECT XUATKHO.ID,XUATKHO.IDVattu,PHIEUXUATKHO.IDKhachhang FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu)tbXK ON VATTU.ID=tbXK.IDVattu AND tbXK.IDKhachhang = " & btFilterMaKH.EditValue
        End If

        If Not btFilterMaVT.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If Not btFilterThongSo.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If

        sql &= sqlWhere

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        Application.DoEvents()
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\thumb\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            End If

            gdvChuyenMa.DataSource = tb
            gdvChuyenMaCT.Columns("TenVT").Caption = "Tên hàng hóa (" & tb.Rows.Count.ToString & ")"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub loadDSTenVT(ByVal HangSX As Object, ByVal NhomVT As Object)
        Dim sqltb As String = ""

        Dim sql As String = ""
        If HangSX Is Nothing And NhomVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTenvattu FROM TENVATTU WHERE ID=-1 "

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbTenVatTu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhomVT(ByVal HangSX As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If HangSX Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTennhom FROM TENNHOM WHERE ID=-1"

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbNhomVT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbHangSX(ByVal NhomVT As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If NhomVT Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub timerAutoLoad_Tick(sender As System.Object, e As System.EventArgs) Handles timerAutoLoad.Tick
        LoadDSYeuCau()
    End Sub

    Private Sub LoadCbTGCapHang()
        AddParameterWhere("@Loai", LoaiTuDien.TGCungUng)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If Not tb Is Nothing Then
            cbTGCungUng.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNVKD.EditValue = Nothing
        End If
    End Sub

    Private Sub rCbKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKH.EditValue = Nothing
        End If
    End Sub

#End Region

#Region "Các sự kiện của thanh công cụ và menu chính"

    Private Sub btNhanChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhanChuyenMa.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        AddParameterWhere("@ID", gdvYCCT.GetFocusedRowCellValue("ID"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT IDNhanXL FROM BANGYEUCAU WHERE ID=@ID")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If Not ShowCauHoi("Đã có người nhận xử lý yêu cầu này, bạn có muốn nhận xử lý thay hay không ?") Then
                    Exit Sub
                End If

            End If
        End If

        Dim tg As DateTime = GetServerTime()
        AddParameter("@IDNhanXL", Convert.ToInt32(TaiKhoan))
        AddParameter("@ThoiGianNhanXL", tg)
        AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
        If doUpdate("BANGYEUCAU", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvCT.SetFocusedRowCellValue("NguoiNhanXL", NguoiDung)
            gdvCT.SetFocusedRowCellValue("ThoiGianNhanXL", tg)
            'loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))

            Dim str As String = NguoiDung & " đã nhận xử lý YC: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " KH: " & gdvCT.GetFocusedRowCellValue("ttcMa") & " của bạn"
            ThemThongBaoChoNV(str, gdvCT.GetFocusedRowCellValue("IDTakecare"))
            ShowAlert("Đã nhận chuyển mã thành công !")
            'ThemThongBaoChoNV(
        End If
    End Sub

    Private Sub chkTuDong_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkTuDong.CheckedChanged
        If chkTuDong.Checked Then
            chkTuDong.Glyph = My.Resources.Checked
            timerAutoLoad.Start()
        Else
            chkTuDong.Glyph = My.Resources.UnCheck
            timerAutoLoad.Stop()
        End If
    End Sub

    Private Sub btThem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        MaTuDien = -1
        fCNYeuCau = New frmThemYeuCau
        fCNYeuCau.Tag = Me.Tag
        fCNYeuCau.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If Convert.ToInt16(gdvCT.GetFocusedRowCellValue("TrangThai")) = Convert.ToInt16(TrangThaiYeuCau.DaChaoGia) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
                ShowCanhBao("Bạn không có quyền thực hiện thao tác này !")
                Exit Sub
            End If

        End If

        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        MaTuDien = gdvCT.GetFocusedRowCellValue("ID")
        fCNYeuCau = New frmThemYeuCau
        fCNYeuCau.Tag = Me.Tag
        fCNYeuCau.ShowDialog()
        gdvCT.FocusedRowHandle = index
    End Sub

    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDSYeuCau()
    End Sub

    Private Sub ChaoGia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        gdvYCCT.CloseEditor()
        gdvYCCT.UpdateCurrentRow()
        Dim count As Integer = 0
        Dim str As String = "("
        For i As Integer = 0 To gdvYCCT.RowCount - 1
            If gdvYCCT.GetRowCellValue(i, "Chon") And Not IsDBNull(gdvYCCT.GetRowCellValue(i, "IDVattu")) Then
                str &= gdvYCCT.GetRowCellValue(i, "ID") & ","
                count += 1
            End If
        Next
        str = str.TrimEnd(New Char() {","c}) & ")"

        If isShowing Then
            If ShowCauHoi("Thêm hàng vào chào giá đang được mở ?") Then
                If count = 0 Then
                    ShowCanhBao("Chưa chọn mặt hàng cần chào giá !")
                    Exit Sub
                End If
                fCNChaoGia.SPYeuCau2 = gdvCT.GetFocusedRowCellValue("Sophieu")
                fCNChaoGia.IDYC = str
                fCNChaoGia.ThemVatTuChaoGia()
                fCNChaoGia.WindowState = FormWindowState.Maximized
            End If

        Else
            If Not gdvCT.GetFocusedRowCellValue("CongTrinh") Then
                If count = 0 Then
                    ShowCanhBao("Chưa chọn mặt hàng cần chào giá !")
                    Exit Sub
                End If
            End If

            TrangThai.isAddNew = True
            fCNChaoGia = New frmCNChaoGia
            fCNChaoGia.TrangThaiCG.isAddNew = True
            fCNChaoGia.SPYeuCau = gdvCT.GetFocusedRowCellValue("Sophieu")
            fCNChaoGia.gdvMaKH.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachhang")
            fCNChaoGia.cbNguoiGD.EditValue = gdvCT.GetFocusedRowCellValue("IDNgd")
            fCNChaoGia.cbTakeCare.EditValue = gdvCT.GetFocusedRowCellValue("IDTakecare")
            fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
            fCNChaoGia.IDYC = str
            If gdvCT.GetFocusedRowCellValue("CongTrinh") Then
                fCNChaoGia.tbTenCongTrinh.EditValue = gdvCT.GetFocusedRowCellValue("NoiDung")
            End If
            isShowing = True
            fCNChaoGia.Show()
        End If
    End Sub

    Private Sub chkLocDuLieu_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocDuLieu.CheckedChanged
        gdvCT.OptionsView.ShowAutoFilterRow = chkLocDuLieu.Checked
    End Sub


    Private Sub mQuaTrinhXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mQuaTrinhXuLy.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("TrangThai") = TrangThaiYeuCau.DaChaoGia Then
            ShowCanhBao("Yêu cầu đã được chào giá !")
            Exit Sub
        End If
        Dim index As Integer = gdvCT.FocusedRowHandle
        TrangThai.isAddNew = True
        objID = gdvCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNQuaTrinhXuLy
        f._MaKH = gdvCT.GetFocusedRowCellValue("ttcMa")
        f.ThoiGian = gdvCT.GetFocusedRowCellValue("Ngaythang")
        f.ShowDialog()

        gdvCT.FocusedRowHandle = index
        gdvCT.ExpandMasterRow(index, "Quá trình xử lý")
    End Sub

    Private Sub gdvXuLyCT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvXuLyCT.DoubleClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        MaTuDien = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
        objID = gdvCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNQuaTrinhXuLy
        f.ShowDialog()
        gdvCT.FocusedRowHandle = index
        gdvCT.ExpandMasterRow(index, "Quá trình xử lý")
    End Sub

#End Region

#Region "Các sự kiện liên quan đến grid chính gdvCT"
    Private Sub gdvCT_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
        MaTuDien = gdvCT.GetFocusedRowCellValue("ID")
        gdvListFileCT.Tag = "BANGYEUCAU"
    End Sub
#End Region

#Region "Các sự kiện liên quan đến thanh công cụ yêu cầu "
    Private Sub pmCmFilter_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles pmCmFilter.ItemClick
        gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = Not gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
    End Sub

    Private Sub pMYCThemYC_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles pMYCThemYC.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        gdvYCCT.AddNewRow()
    End Sub

    Private Sub pMYCBoYC_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles pMYCBoYC.ItemClick
        If gdvYCCT.FocusedRowHandle < 0 Then Exit Sub
        gdvYCCT.DeleteSelectedRows()
    End Sub

#End Region

#Region "Các sự kiện liên quan đến grid yêu cầu"
    Private Sub gdvYCCT_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvYCCT.InitNewRow
        Dim tg As DateTime = GetServerTime()
        AddParameter("@Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu"))
        AddParameter("@NgayNhanYeucau", tg)
        Dim objIDYC As Object = doInsert("YEUCAUDEN")
        If objIDYC Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        gdvYCCT.SetRowCellValue(e.RowHandle, "Chon", False)
        gdvYCCT.SetRowCellValue(e.RowHandle, "ID", objIDYC)
        gdvYCCT.SetRowCellValue(e.RowHandle, "Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu"))
        gdvYCCT.SetRowCellValue(e.RowHandle, "STT", gdvYCCT.GetRowCellValue(gdvYCCT.RowCount - 2, "STT") + 1)
        gdvYCCT.SetRowCellValue(e.RowHandle, "Soluong", 1)
        gdvYCCT.SetRowCellValue(e.RowHandle, "Mucdocan", 3)
        gdvYCCT.SetRowCellValue(e.RowHandle, "Trangthai", TrangThaiYeuCau.CanChaoGia)
    End Sub
#End Region

#Region "Các sự kiện liên quan đến thanh công cụ chuyển mã"

    Private Sub cbTenVatTu_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTenVatTu.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTenVT.EditValue = Nothing
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            btFilterHangSX.EditValue = Nothing
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub rtbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub rtbThongSo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThongSo.ButtonClick
        btFilterThongSo.EditValue = Nothing
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub btTaiLai_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If chkTaiAnh.Checked Then
            colHinhAnh.Visible = True
        Else
            colHinhAnh.Visible = False
        End If
        LoadDSVatTuDungChuyenMa()
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChuyenMa.ItemClick
        gdvChuyenMaCT.CloseEditor()
        gdvChuyenMaCT.UpdateCurrentRow()

        For i As Integer = 0 To gdvChuyenMaCT.RowCount - 1
            If gdvChuyenMaCT.GetRowCellValue(i, "SLYC") > 0 Then

                If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                    gdvYCCT.AddNewRow()
                Else
                    If Not ShowCauHoi("Thay thế mã: " & gdvCT.GetFocusedRowCellValue("Model") & " bằng: " & gdvChuyenMaCT.GetRowCellValue(i, "Model")) Then Exit Sub
                End If
                Dim tg As DateTime = GetServerTime()

                gdvYCCT.SetFocusedRowCellValue("Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu"))
                gdvYCCT.SetFocusedRowCellValue("Mucdocan", LoaiTuDien.MucDoCan)
                gdvYCCT.SetFocusedRowCellValue("Trangthai", TrangThaiYeuCau.CanChaoGia)
                gdvYCCT.SetFocusedRowCellValue("IDVattu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                gdvYCCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetRowCellValue(i, "TenVT"))
                gdvYCCT.SetFocusedRowCellValue("Thongso", gdvChuyenMaCT.GetRowCellValue(i, "Thongso"))
                gdvYCCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetRowCellValue(i, "Model"))
                gdvYCCT.SetFocusedRowCellValue("Soluong", gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                gdvYCCT.SetFocusedRowCellValue("IDDVT", gdvChuyenMaCT.GetRowCellValue(i, "IDDVT"))
                gdvYCCT.SetFocusedRowCellValue("TenHang", gdvChuyenMaCT.GetRowCellValue(i, "HangSX"))
                gdvYCCT.SetFocusedRowCellValue("HangTon", gdvChuyenMaCT.GetRowCellValue(i, "HangTon"))
                gdvYCCT.SetFocusedRowCellValue("slTon", gdvChuyenMaCT.GetRowCellValue(i, "slTon"))
                gdvYCCT.SetFocusedRowCellValue("NgayChuyenma", tg)
                gdvYCCT.SetFocusedRowCellValue("IDChuyenma", Convert.ToInt32(TaiKhoan))
                gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
                ' gdvChuyenMaCT.GetFocusedRowCellValue("HangTon")
                gdvYCCT.CloseEditor()
                gdvYCCT.UpdateCurrentRow()
            End If


        Next

        ShowAlert("Đã chuyển mã")
    End Sub

    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then
            'SendKeys.Send("{F4}")
            pNhapSoLuong.Visible = True
            pNhapSoLuong.Focus()

            If gdvYCCT.FocusedRowHandle >= 0 Then
                tbSL.EditValue = Convert.ToDouble(gdvYCCT.GetFocusedRowCellValue("Soluong"))
            Else
                tbSL.EditValue = 1.0
            End If
            tbSL.Focus()
        ElseIf e.Control AndAlso (e.KeyCode = Keys.N Or e.KeyCode = Keys.G) Then
            Dim f As New frmTinhTrangVT
            f.Tag = Me.Parent.Tag
            f._IDVatTu = gdvYCCT.GetFocusedRowCellValue("IDVatTu")
            ' If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            f._HienThongTinNX = False
            ' End If
            f.ShowDialog()

        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = Not gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
            chkHienDongLocDuLieu.Checked = gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

#End Region

#Region "Các sự kiện liên quan đến grid yêu cầu gdvYCCT"

    Private Sub gdvYCCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvYCCT.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.N Then
            If IsDBNull(gdvYCCT.GetFocusedRowCellValue("IDVattu")) Or gdvYCCT.GetFocusedRowCellValue("IDVattu") Is Nothing Then Exit Sub

            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvYCCT.GetFocusedRowCellValue("IDVattu"), gdvCT.GetFocusedRowCellValue("Sophieu"), -1, gdvCT.GetFocusedRowCellValue("IDKhachhang"))
            If Not ds Is Nothing Then
                Dim f As New frmThongTinGiaNhap
                f.lbVatTu.Text &= gdvYCCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvYCCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvYCCT.GetFocusedRowCellValue("HangSX")
                f.lbGiaCungUng.Text &= Convert.ToDouble(ds.Tables(0).Rows(0)(0)).ToString("N2")
                f.gdvGiaNhap.DataSource = ds.Tables(1)
                f.gdvChaoGia.DataSource = ds.Tables(2)
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.E Then
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "Excel File|*.xls"
            saveFile.FileName = "YC" & gdvCT.GetFocusedRowCellValue("Sophieu") & ".xls"
            If saveFile.ShowDialog = DialogResult.OK Then
                ShowWaiting("Đang kết xuất ...")
                Try
                    Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvYCCT)
                    CloseWaiting()
                    If ShowCauHoi("Mở file vừa kết xuất ?") Then
                        Dim psi As New ProcessStartInfo()
                        psi.FileName = saveFile.FileName
                        psi.UseShellExecute = True
                        Process.Start(psi)
                    End If
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try


            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.G Then
            If IsDBNull(gdvYCCT.GetFocusedRowCellValue("IDVattu")) Or gdvYCCT.GetFocusedRowCellValue("IDVattu") Is Nothing Then Exit Sub
            'Keys.Space
            Dim dt As DataTable = SqlSelect.Select_ThongTinBanHang(gdvYCCT.GetFocusedRowCellValue("IDVattu"), gdvCT.GetFocusedRowCellValue("IDKhachhang"))
            If Not dt Is Nothing Then
                Dim f As New frmThongTinGiaBan
                f.lbVatTu.Text &= gdvYCCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvYCCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvYCCT.GetFocusedRowCellValue("HangSX")
                f.gdvGiaNhap.DataSource = dt
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xoá nội dung được chọn ?") Then
                AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("ID"))
                If doDelete("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    CType(gdvYC.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvYCCT.GetFocusedDataSourceRowIndex)
                End If
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            gdvYCCT.OptionsView.ShowAutoFilterRow = Not gdvYCCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

#End Region

#Region "Lưu lại"

    Private Sub gdvYCCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvYCCT.RowUpdated
        AddParameter("@Sophieu", gdvYCCT.GetFocusedRowCellValue("Sophieu"))
        AddParameter("@Noidung", gdvYCCT.GetFocusedRowCellValue("Noidung"))
        AddParameter("@Soluong", gdvYCCT.GetFocusedRowCellValue("Soluong"))
        AddParameter("@Mucdocan", gdvYCCT.GetFocusedRowCellValue("Mucdocan"))
        AddParameter("@IDVattu", gdvYCCT.GetFocusedRowCellValue("IDVattu"))
        AddParameter("@IDHoithongtin", gdvYCCT.GetFocusedRowCellValue("IDHoithongtin"))
        AddParameter("@IDChuyenma", gdvYCCT.GetFocusedRowCellValue("IDChuyenma"))
        AddParameter("@NgayHoithongtin", gdvYCCT.GetFocusedRowCellValue("NgayHoithongtin"))
        AddParameter("@NgayChuyenma", gdvYCCT.GetFocusedRowCellValue("NgayChuyenma"))
        AddParameter("@NgayHoigia", gdvYCCT.GetFocusedRowCellValue("NgayHoigia"))
        AddParameter("@TGCungUng", gdvYCCT.GetFocusedRowCellValue("TGCungUng"))
        AddParameter("@Hoithongtin", gdvYCCT.GetFocusedRowCellValue("Hoithongtin"))
        AddParameter("@FileDinhKem", gdvYCCT.GetFocusedRowCellValue("FileDinhKem"))
        'If Me.Tag = "MUAHANG" Then
        '    AddParameter("@IDHoigia", Convert.ToInt32(TaiKhoan))
        '    AddParameter("@Trangthai", TrangThaiYeuCau.CanChaoGia)
        'Else
        AddParameter("@IDHoigia", gdvYCCT.GetFocusedRowCellValue("IDHoigia"))
        AddParameter("@Trangthai", gdvYCCT.GetFocusedRowCellValue("Trangthai"))
        'End If
        AddParameter("@GiaCungUng", gdvYCCT.GetFocusedRowCellValue("GiaCungUng"))
        AddParameter("@IDTienTeCungUng", gdvYCCT.GetFocusedRowCellValue("IDTienTeCungUng"))
        AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("ID"))
        If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        Else
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        End If
    End Sub

    Private Sub tbSL_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbSL.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            gdvChuyenMaCT.SetFocusedRowCellValue("SLYC", Convert.ToDouble(tbSL.EditValue))
            btChuyenMa.PerformClick()
            If gdvYCCT.FocusedRowHandle < 0 Then

                If gdvYCCT.FocusedRowHandle < gdvYCCT.RowCount - 1 Then
                    gdvYCCT.FocusedRowHandle += 1
                Else
                    gdvYCCT.FocusedRowHandle = -1
                End If
            End If
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        ElseIf e.KeyChar = Convert.ToChar(Keys.Escape) Then
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        End If
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
            chkLocDuLieu.Checked = gdvCT.OptionsView.ShowAutoFilterRow
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btThem.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btSua.PerformClick()

        End If
    End Sub

#Region "File đính kèm"
    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            If gdvListFileCT.Tag = "TaiLieu" Then
                OpenFileOnLocal(UrlTaiLieuVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & e.CellValue, e.CellValue)
            Else
                OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & e.CellValue, e.CellValue, True)
            End If

        End If
    End Sub

    Private Sub rPopupFileCT_Popup(sender As System.Object, e As System.EventArgs) Handles rPopupFileCT.Popup
        btThemFile.Enabled = True
        btXoaFile.Enabled = True
        If _exit Then
            _exit = False
            Exit Sub
        End If
        gListFileCT.Text = "Danh sách file đính kèm"
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)

    End Sub

    Private Sub rpopupTaiLieu_Popup(sender As System.Object, e As System.EventArgs) Handles rpopupTaiLieu.Popup
        btThemFile.Enabled = False
        btXoaFile.Enabled = False
        gListFileCT.Text = "Danh sách tài liệu"
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub rPopup_Popup(sender As System.Object, e As System.EventArgs) Handles rPopup.Popup
        btThemFile.Enabled = True
        btXoaFile.Enabled = True
        If _exit Then
            _exit = False
            Exit Sub
        End If
        gListFileCT.Text = "Danh sách file đính kèm"
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub rPopup_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rPopup.Closed, rPopupFileCT.Closed
        If gdvListFileCT.Tag = "tblXyLyYeuCau" Then Exit Sub
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        AddParameter("@FileDinhKem", _File)
        AddParameterWhere("@ID", MaTuDien)
        If doUpdate(gdvListFileCT.Tag, "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CType(sender, PopupContainerEdit).EditValue = _File
        End If
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
    End Sub

    Private Sub gdvCT_GotFocus(sender As System.Object, e As System.EventArgs) Handles gdvCT.GotFocus
        rPopup.PopupControl = popupFile
        gdvListFileCT.Tag = "BANGYEUCAU"
        btThemFile.Visible = True
        btXoaFile.Visible = True
        MaTuDien = gdvCT.GetFocusedRowCellValue("ID")
    End Sub

    Private Sub gdvYCCT_GotFocus(sender As System.Object, e As System.EventArgs) Handles gdvYCCT.GotFocus
        rPopupFileCT.PopupControl = popupFile

        gdvListFileCT.Tag = "YEUCAUDEN"
        btThemFile.Visible = True
        btXoaFile.Visible = True
        MaTuDien = gdvYCCT.GetFocusedRowCellValue("ID")

    End Sub

    Private Sub gdvChuyenMaCT_GotFocus(sender As System.Object, e As System.EventArgs) Handles gdvChuyenMaCT.GotFocus
        rpopupTaiLieu.PopupControl = popupFile
        gdvListFileCT.Tag = "TaiLieu"
        btThemFile.Visible = False
        btXoaFile.Visible = False
        MaTuDien = Nothing
    End Sub

    Private Sub gdvXuLyCT_GotFocus(sender As System.Object, e As System.EventArgs) Handles gdvXuLyCT.GotFocus
        gdvListFileCT.Tag = "tblXuLyYeuCau"
        btThemFile.Visible = False
        btXoaFile.Visible = False
        MaTuDien = Nothing
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        ' On Error Resume Next
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Dim path As String = ""

        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\") Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa"))
            End If
            For Each file In openFile.FileNames
                Try
                    path = "YC" & gdvCT.GetFocusedRowCellValue("Sophieu") & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            Impersonator.EndImpersonation()
            CloseWaiting()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()
            _exit = True
            SendKeys.Send("{F4}")

        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If Convert.ToInt16(gdvCT.GetFocusedRowCellValue("TrangThai")) = Convert.ToInt16(TrangThaiYeuCau.DaChaoGia) Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        End If

        If ShowCauHoi("Xoá file được chọn ?") Then
            gdvListFileCT.DeleteSelectedRows()
            If ShowCauHoi("Xoá luôn file trong hệ thống ?") Then
                Try
                    IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Catch ex As Exception
                    'If Not IO.File.Exists(ServerName & RootUrl & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & gdvListFileCT.GetFocusedRowCellValue("File")) Then
                    'End If
                    ShowBaoLoi(ex.Message)
                End Try
            End If

        End If

    End Sub

#End Region

    Private Sub chkLocThongDung_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocThongDung.CheckedChanged
        If chkLocThongDung.Checked Then
            Dim FilterString, FilterDisplayText As String
            Dim FilterThongDung As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("ThongDung", True, BinaryOperatorType.Equal))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "Thông dụng = 1"
            FilterThongDung = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvChuyenMaCT.Columns("ThongDung").FilterInfo = FilterThongDung
        Else
            gdvChuyenMaCT.Columns("ThongDung").ClearFilter()
        End If
    End Sub

    Private Sub chkLocTon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocTon.CheckedChanged
        If chkLocTon.Checked Then
            Dim FilterString, FilterDisplayText As String
            Dim FilterSLTon As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("slTon", 0, BinaryOperatorType.Greater))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "SL tồn > 0"
            FilterSLTon = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvChuyenMaCT.Columns("slTon").FilterInfo = FilterSLTon
        Else
            gdvChuyenMaCT.Columns("slTon").ClearFilter()
        End If
    End Sub

    Private Sub chkLocKhoBan_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocKhoBan.CheckedChanged
        If chkLocKhoBan.Checked Then
            Dim FilterString, FilterDisplayText As String
            Dim FilterKhoBan As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("HangTon", True, BinaryOperatorType.Equal))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "Hàng tồn = 1"
            FilterKhoBan = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvChuyenMaCT.Columns("HangTon").FilterInfo = FilterKhoBan
        Else
            gdvChuyenMaCT.Columns("HangTon").ClearFilter()
        End If
    End Sub

    Private Sub chkHienDongLocDuLieu_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkHienDongLocDuLieu.CheckedChanged
        gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = chkHienDongLocDuLieu.Checked
    End Sub

    Private Sub mXemAnhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemAnhLon.ItemClick
        If gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString <> "" Then
            Dim f As New frmXemAnh
            f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString)
            f.Text = "Ảnh: " & gdvChuyenMaCT.GetFocusedRowCellValue("Model").ToString
            f.ShowDialog()

        End If
    End Sub

    Private Sub mTaiTaiLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiTaiLieu.ItemClick
        If gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File").ToString <> "" Then
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File"))
            saveFile.FileName = gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File")
            If saveFile.ShowDialog = DialogResult.OK Then
                Try
                    ShowWaiting("Đang tải file về máy ...")
                    System.IO.File.Copy(UrlTaiLieuVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File"), saveFile.FileName, True)
                    CloseWaiting()
                    If ShowCauHoi("Đã tải file về máy, bạn có muốn mở file vừa tải không ?") Then
                        Dim psi As New ProcessStartInfo()
                        With psi
                            .FileName = saveFile.FileName
                            .UseShellExecute = True
                        End With
                        Try
                            Process.Start(psi)
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try
                    End If
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try
            End If


        End If
    End Sub

    Private Sub mTaiAnhVeMay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiAnhVeMay.ItemClick
        If gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString = "" Then Exit Sub
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString)
        saveFile.FileName = gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString
        If saveFile.ShowDialog = DialogResult.OK Then
            Try
                ShowWaiting("Đang tải file về máy ...")
                System.IO.File.Copy(UrlAnhVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString, saveFile.FileName, True)
                CloseWaiting()
                If ShowCauHoi("Đã tải file về máy, bạn có muốn mở file vừa tải không ?") Then
                    OpenFile(saveFile.FileName)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub pMenuXemTaiLieu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuXemTaiLieu.BeforePopup
        If gdvListFileCT.Tag = "TaiLieu" Then
            mTaiTaiLieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            mTaiTaiLieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub gdvYCCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvYCCT.CellValueChanged
        If e.Column.FieldName = "GiaCungUng" Then
            If IsNumeric(e.Value) Then
                gdvYCCT.SetFocusedRowCellValue("IDHoigia", Convert.ToInt32(TaiKhoan))
                gdvYCCT.SetFocusedRowCellValue("NgayHoigia", GetServerTime)
                gdvYCCT.CloseEditor()
                gdvYCCT.UpdateCurrentRow()
            End If
        End If
    End Sub

    Private Sub btSuaThongTinVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaThongTinVT.ItemClick
        If Not KiemTraQuyenSuDung("Menu", "mThongSo", DanhMucQuyen.QuyenSua) Then Exit Sub
        deskTop.OpenTab("Thông số", "THONGSO", New frmThongSo, True, Nothing, "mThongSo")
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterMaVT.EditValue = gdvChuyenMaCT.GetFocusedRowCellValue("Model")
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btTaiLai.PerformClick()
    End Sub

    Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Convert.ToInt16(gdvCT.GetFocusedRowCellValue("TrangThai")) <> Convert.ToInt16(TrangThaiYeuCau.DaChaoGia) Then
            ShowCanhBao("Chưa có chào giá nào lập cho yêu cầu này !")
            Exit Sub
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And TaiKhoan <> gdvCT.GetFocusedRowCellValue("IDTakecare") Then
            ShowCanhBao("Bạn không có quyền xem chào giá của nhân viên khác !")
            Exit Sub
        End If

        deskTop.OpenTab("Chào giá", "CHAOGIA", New frmChaoGia, True, Nothing, "mChaoGia")
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmChaoGia).LoadDS2(gdvCT.GetFocusedRowCellValue("Sophieu"))

    End Sub

    Private Sub gdvYCCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvYCCT.FocusedRowChanged
        MaTuDien = gdvYCCT.GetFocusedRowCellValue("ID")
        gdvListFileCT.Tag = "YEUCAUDEN"
    End Sub

    Private Sub btLayYeuCauTuFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLayYeuCauTuFile.ItemClick
        If ShowCauHoi("Bạn có muốn xem file mẫu trước khi thực hiện thao tác này không ?") Then
            OpenFileOnLocal("\\bacboss\excel$\BIEUMAU\Mau File YC KH.xls", "Mau File YC KH.xls", True)
            Exit Sub
        End If
        Dim openfile As New OpenFileDialog
        openfile.Filter = "Excel File|*.xls;*.xlsx"
        If openfile.ShowDialog = DialogResult.OK Then
            Dim tb As DataTable = Utils.exportXLS2DataTable.getDataTableFromXLS(openfile.FileName, System.IO.Path.GetExtension(openfile.FileName), "1")
            If Not tb Is Nothing Then

                ShowWaiting("Đang tải file lên server ...")
                If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\") Then
                    System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa"))
                End If
                Dim path = "YC" & gdvCT.GetFocusedRowCellValue("Sophieu") & " FileYC " & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(openfile.FileName)

                Try
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            IO.File.Copy(openfile.FileName, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & path, True)
                        End If
                    Else
                        IO.File.Copy(openfile.FileName, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("ttcMa") & "\" & path)
                    End If
                    Dim tbFile As DataTable = ExecuteSQLDataTable("SELECT FileDinhKem FROM BANGYEUCAU WHERE SoPhieu=" & gdvCT.GetFocusedRowCellValue("Sophieu"))
                    If tbFile Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Dim strFile As String = tbFile.Rows(0)(0).ToString & ";" & path
                    AddParameter("@FileDinhKem", strFile.TrimStart(New Char() {";"c}))
                    AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
                    If doUpdate("BANGYEUCAU", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
                CloseWaiting()
                Application.DoEvents()

                ShowWaiting("Đang tải yêu cầu ...")

                For i As Integer = 0 To tb.Rows.Count - 1
                    Application.DoEvents()
                    gdvYCCT.AddNewRow()
                    gdvYCCT.SetFocusedRowCellValue("Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu"))
                    gdvYCCT.SetFocusedRowCellValue("Noidung", tb.Rows(i)(1).ToString + vbCrLf + tb.Rows(i)(2).ToString + vbCrLf + tb.Rows(i)(3).ToString)
                    gdvYCCT.SetFocusedRowCellValue("Trangthai", TrangThaiYeuCau.CanChaoGia)
                    gdvYCCT.SetFocusedRowCellValue("Soluong", tb.Rows(i)(4))
                    gdvYCCT.SetFocusedRowCellValue("NgayChuyenma", GetServerTime)
                    gdvYCCT.SetFocusedRowCellValue("IDChuyenma", Convert.ToInt32(TaiKhoan))
                    gdvYCCT.UpdateCurrentRow()
                Next
                CloseWaiting()
                Dim index As Integer = gdvCT.FocusedRowHandle
                LoadDSYeuCau()
                gdvCT.FocusedRowHandle = index
            End If
        End If
    End Sub

    Private Sub chkChonYC_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChonYC.CheckedChanged
        'Dim tb As DataTable = CType(gdvYC.Views.Item(0).DataSource, DataView).Table
        'With tb
        '    For i As Integer = 0 To .Rows.Count - 1
        '        .Rows(i)("Chon") = chkChonYC.Checked
        '    Next
        'End With
        'gdvYC.DataSource = tb
        gdvYCCT.BeginUpdate()
        For i As Integer = 0 To gdvYCCT.RowCount - 2
            gdvYCCT.SetRowCellValue(i, "Chon", chkChonYC.Checked)
        Next
        gdvYCCT.EndUpdate()
    End Sub


    Private Sub rcbTrangThaiYC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTrangThaiYC.ButtonClick
        cbTrangThaiYC.EditValue = Nothing
    End Sub

    Private Sub gdvYCCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvYCCT.RowCellClick
        If e.Column.FieldName = "Chon" Then
            gdvYCCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
        End If
    End Sub

    Private Sub rbtFilterMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rbtFilterMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btFilterMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub mGiaoViec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mGiaoViec.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmGiaoViec
        f.SoYC = gdvCT.GetFocusedRowCellValue("Sophieu")
        f.ShowDialog()
    End Sub


    Private Sub btMauFileYC_Click(sender As System.Object, e As System.EventArgs)
        OpenFileOnLocal("\\192.168.1.109\excel$\BIEUMAU\Mau File YC KH.xls", "Mau File YC KH.xls", True)
    End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
        f._HienThongTinNX = False
        f.ShowDialog()
    End Sub

    Private Sub btXemTTVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemTTVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvYCCT.GetFocusedRowCellValue("IDVattu")
        f._HienThongTinNX = False
        f.ShowDialog()
    End Sub

    Private Sub gdvChuyenMaCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvChuyenMaCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "CanhBao" Then
            If Not gdvChuyenMaCT.GetRowCellValue(e.RowHandle, "ConSX") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub pMenuChinh_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChinh.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            mTaoBanSao.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            mTaoBanSao.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub mChuyenYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaoBanSao.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub

        If Not ShowCauHoi("Tạo bản sao cho yêu cầu được chọn ?") Then Exit Sub
        'BeginTransaction()
        Dim _SoYCMoi As New Object
        If ShowCauHoi("Tạo bản sao cùng thời điểm yêu cầu cũ ?") Then
            _SoYCMoi = LaySoPhieu2("BANGYEUCAU", gdvCT.GetFocusedRowCellValue("Ngaythang"))
        Else
            _SoYCMoi = LaySoPhieu("BANGYEUCAU")
        End If

        BeginTransaction()

        Dim sql As String = ""
        sql &= " INSERT INTO BANGYEUCAU (NgayThang,SoPhieu,IDKhachHang,IDUser,IDNgd,IDTakeCare,IDModify,NoiDung,FileDinhKem,CongTrinh,TrangThai,IDLoaiYeuCau)"
        sql &= " SELECT NgayThang,'" & _SoYCMoi & "',IDKhachHang,IDUser,IDNgd,IDTakeCare,IDModify,NoiDung,FileDinhKem,CongTrinh," & TrangThaiYeuCau.CanChaoGia & ",IDLoaiYeuCau"
        sql &= " FROM BANGYEUCAU "
        sql &= " WHERE sophieu='" & gdvCT.GetFocusedRowCellValue("Sophieu") & "'"

        sql &= " INSERT INTO YEUCAUDEN"
        sql &= " (SoPhieu,Noidung,Soluong,Mucdocan,IDVattu,IDHoithongtin,IDChuyenma,IDHoigia,NgayNhanYeucau,NgayHoithongtin,"
        sql &= "         NgayNhanChuyenma, NgayChuyenma, NgayNhanHoigia, NgayHoigia, TrangThai, Hoithongtin, GiaCungUng"
        sql &= " ,TGCungUng,FileDinhKem,IDTienTeCungUng)"
        sql &= " SELECT "
        sql &= " '" & _SoYCMoi & "',Noidung,Soluong,Mucdocan,IDVattu,IDHoithongtin,IDChuyenma,IDHoigia,NgayNhanYeucau,NgayHoithongtin,"
        sql &= " NgayNhanChuyenma,NgayChuyenma,NgayNhanHoigia,NgayHoigia," & TrangThaiYeuCau.CanChaoGia & ",Hoithongtin,GiaCungUng"
        sql &= " ,TGCungUng,FileDinhKem,IDTienTeCungUng"
        sql &= " FROM YEUCAUDEN"
        sql &= " WHERE Sophieu='" & gdvCT.GetFocusedRowCellValue("Sophieu") & "'"


        If ExecuteSQLNonQuery(sql) Is Nothing Then
            RollBackTransaction()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ComitTransaction()
            ShowThongBao("Số yêu cầu mới là: " & _SoYCMoi)
            btXem.PerformClick()
        End If
    End Sub

    Private Sub pMenuYCCT_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuYCCT.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvYCCT.CalcHitInfo(gdvYC.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub chkTaiAnh_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkTaiAnh.CheckedChanged

    End Sub

    Private Sub gdvQuaTrinhBaoGia_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvQuaTrinhBaoGia.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            'pQuaTrinhBaoGia.ShowPopup(gdvYC.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvYCCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvYCCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuYCCT.ShowPopup(gdvYC.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mSuDungDeChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuDungDeChaoGia.ItemClick
        If ShowCauHoi("Lấy báo giá được chọn để chào giá ?") Then
            If Not gdvYCCT.GetMasterRowExpanded(gdvYCCT.FocusedRowHandle) Then Exit Sub
            Try
                BeginTransaction()
                Dim Index As Integer = gdvYCCT.FocusedRowHandle
                Dim indexGD As Integer = CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle
                objID = CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
                AddParameter("@GiaCungUng", CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Gia"))
                AddParameter("@IDTienTeCungUng", CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("IDTienTe"))
                AddParameter("@TGCungUng", CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("IDTGCungUng"))
                AddParameter("@IDHoigia", CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("IDCungUng"))
                AddParameter("@NgayHoigia", CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ThoiGianBaoGia"))
                AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("ID"))
                If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                AddParameter("@ChaoGia", False)
                AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("ID"))
                If doUpdate("tblQuaTrinhBaoGia", "IDYeuCau=@IDYC") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                AddParameter("@ChaoGia", True)
                AddParameterWhere("@IDBG", objID)
                If doUpdate("tblQuaTrinhBaoGia", "ID=@IDBG") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                ComitTransaction()

                Dim str As String = "Báo giá được chọn để chào giá" & vbCrLf
                str &= " - YC: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " KH: " & gdvCT.GetFocusedRowCellValue("ttcMa") & vbCrLf
                str &= " - Nội dung YC: " & gdvYCCT.GetFocusedRowCellValue("Noidung") & vbCrLf
                str &= " - Model: " & gdvYCCT.GetFocusedRowCellValue("Model") & vbCrLf
                str &= " - Kinh doanh: " & NguoiDung

                ThemThongBaoChoNV(str, CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("IDCungUng"))
                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))

                gdvYCCT.ClearSelection()
                gdvYCCT.FocusedRowHandle = Index

                gdvYCCT.SelectRow(Index)
                gdvYCCT.ExpandMasterRow(Index, "Quá trình báo giá")
                ShowAlert("Đã thực hiện !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub

    Private Sub gdvCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ttcMa" Then
                e.TotalValue = _countKH
            End If
        End If
    End Sub

    Public Function GetDataView(gc As GridControl) As DataView
        Dim dv As DataView = Nothing

        If gc.FocusedView IsNot Nothing AndAlso gc.FocusedView.DataSource IsNot Nothing Then
            Dim view = DirectCast(gc.FocusedView, ColumnView)
            Dim currentList = gc.DefaultView.DataSource
            '(DataView)
            Dim filterExpression = GetFilterExpression(view)

            Dim currentFilter = currentList.RowFilter

            'create a new data view
            dv = New DataView(currentList.Table)

            If filterExpression <> [String].Empty Then
                If currentFilter <> [String].Empty Then
                    currentFilter += " AND "
                End If
                currentFilter += filterExpression
            End If
            dv.RowFilter = currentFilter
        End If
        Return dv
    End Function

    Public Function GetFilterExpression(view As ColumnView) As String
        Dim expression = [String].Empty

        If view.ActiveFilter IsNot Nothing AndAlso view.ActiveFilterEnabled AndAlso view.ActiveFilter.Expression <> [String].Empty Then
            expression = view.ActiveFilter.Expression
        End If
        Return expression
    End Function

    Private Sub gdvCT_ColumnFilterChanged(sender As System.Object, e As System.EventArgs) Handles gdvCT.ColumnFilterChanged
        gdvCT.Columns("Sophieu").Caption = gdvCT.RowCount & " YC"
        gdvCT.Columns("ttcMa").Caption = GetDataView(gdv).ToTable(True, "ttcMa").Rows.Count & " KH"
    End Sub

    Private Sub mPhanHoiChoCungUng_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhanHoiChoCungUng.ItemClick
        If IsDBNull(gdvYCCT.GetFocusedRowCellValue("IDHoigia")) Then
            ShowCanhBao("Chưa có người báo giá cho yêu cầu được chọn !")
            Exit Sub
        Else
            If ShowCauHoi("Gửi thông báo lại cho nhân viên báo giá yêu cầu này ?") Then
                Dim str As String = "Phản hồi lại từ kinh doanh " & NguoiDung & vbCrLf
                str &= "Sô YC: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " KH: " & gdvCT.GetFocusedRowCellValue("ttcMa") & vbCrLf
                str &= "Nội dung chính: " & vbCrLf
                str &= gdvYCCT.GetFocusedRowCellValue("Hoithongtin")
                ThemThongBaoChoNV(str, gdvYCCT.GetFocusedRowCellValue("IDHoigia"))
                gdvYCCT.SetFocusedRowCellValue("TrangThai", TrangThaiYeuCau.CanHoiGiaHang)
                gdvYCCT.UpdateCurrentRow()

            End If
        End If
    End Sub

    Private Sub mHoanTatXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHoanTatXuLy.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        If Not ShowCauHoi("Hoàn tất xử lý yêu cầu này?") Then
            Exit Sub
        End If


        Dim tg As DateTime = GetServerTime()
        AddParameter("@IDDaXL", Convert.ToInt32(TaiKhoan))
        AddParameter("@ThoiGianDaXL", tg)
        AddParameter("@TrangThai", TrangThaiYeuCau.CanChaoGia)
        AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
        If doUpdate("BANGYEUCAU", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            'gdvCT.SetFocusedRowCellValue("NguoiNhanXL", NguoiDung)
            'gdvCT.SetFocusedRowCellValue("ThoiGianNhanXL", tg)
            'loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))

            Dim str As String = NguoiDung & " đã xử lý xong YC: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " KH: " & gdvCT.GetFocusedRowCellValue("ttcMa") & " của bạn"
            ThemThongBaoChoNV(str, gdvCT.GetFocusedRowCellValue("IDTakecare"))
            LoadDSYeuCau()
            ShowAlert("Đã hoàn tất xử lý YC !")
            'ThemThongBaoChoNV(
        End If
    End Sub

    Private Sub btLoadTop10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLoadTop10.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sqlWhere As String = " WHERE Maloi=0 "

        Dim sql As String = " Select TOP 10 NULL AS CanhBao,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= " (Round((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID),4)-Round((select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID),4)) AS slTon, "
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
        sql &= " Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
        sql &= " Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"

        sql &= " VATTU.Gianhap1 AS Gianhap, tblTienTe.Ten AS Tiente,TonNCC,"
        sql &= " TENNUOC.Ten AS Xuatxu,convert(float,0) AS SLYC, VATTU.ThongDung,VATTU.HangTon,VATTU.HinhAnh,(convert(image,NULL))HienThi,VATTU.TaiLieu,VATTU.ConSX "
        sql &= " from VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "

        'If Not btFilterMaKH.EditValue Is Nothing Then
        '    sql &= " INNER JOIN (SELECT XUATKHO.ID,XUATKHO.IDVattu,PHIEUXUATKHO.IDKhachhang FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu)tbXK ON VATTU.ID=tbXK.IDVattu AND tbXK.IDKhachhang = " & btFilterMaKH.EditValue
        'End If


        sql &= sqlWhere
        sql &= " ORDER BY ID DESC "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        Application.DoEvents()
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\thumb\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            End If

            gdvChuyenMa.DataSource = tb

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenuChinh.ShowPopup(gdv.PointToScreen(e.Location))
            locationM = (gdv.PointToScreen(e.Location))
        End If
    End Sub
    'Tai
    Private locationM As New Object
    Private Sub mGiaoXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mGiaoXuLy.ItemClick
        pcGiaoXuLy.Location = locationM
        If pcGiaoXuLy.Visible = False Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                gLueNhanSu.Enabled = False
                meGhiChu.Enabled = False
                btnXacNhan.Enabled = False
            Else
                gLueNhanSu.Enabled = True
                meGhiChu.Enabled = True
                btnXacNhan.Enabled = True
            End If
            pcGiaoXuLy.Enabled = True
            pcGiaoXuLy.Visible = True

            gLueNhanSu.Properties.DataSource = ExecuteSQLDataTable("select ID, Ten from NHANSU Where (IDDepatment=2 or IDDepatment=6) AND Trangthai=1 order by ID ")
            gLueNhanSu.EditValue = gdvCT.GetFocusedRowCellValue("IDNhanXL")
            '  AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            meGhiChu.EditValue = gdvCT.GetFocusedRowCellValue("GhiChuGV")
            gLueNhanSu.Focus()
        End If

    End Sub

    Private Sub btnXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btnXacNhan.Click
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        AddParameterWhere("@ID", gdvYCCT.GetFocusedRowCellValue("ID"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT IDNhanXL FROM BANGYEUCAU WHERE ID=@ID")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If Not ShowCauHoi("Đã có người nhận xử lý yêu cầu này, bạn có muốn nhận xử lý thay hay không ?") Then
                    Exit Sub
                End If

            End If
        End If

        Dim tg As DateTime = GetServerTime()
        Dim tgns As Object = gLueNhanSu.EditValue
        AddParameterWhere("@ID", tgns)
        Dim tentg As Object = ExecuteSQLScalar("select  Ten from NHANSU where ID=@ID")
        AddParameter("@IDNhanXL", tgns)
        AddParameter("@GhiChuGV", meGhiChu.EditValue)
        AddParameter("@ThoiGianNhanXL", tg)
        AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
        If doUpdate("BANGYEUCAU", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvCT.SetFocusedRowCellValue("NguoiNhanXL", tentg)
            gdvCT.SetFocusedRowCellValue("ThoiGianNhanXL", tg)
            gdvCT.SetFocusedRowCellValue("GhiChuGV", meGhiChu.EditValue)
            'loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
            If tgns IsNot Nothing Then
                Dim str As String = NguoiDung & " đã nhận xử lý YC: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " KH: " & gdvCT.GetFocusedRowCellValue("ttcMa") & " của bạn"
                ThemThongBaoChoNV(str, gdvCT.GetFocusedRowCellValue("IDTakecare"))
                str = NguoiDung & " đã giao việc cho " & tentg & " xử lý YC: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " KH: " & gdvCT.GetFocusedRowCellValue("ttcMa")
                ThemThongBaoChoNV(str, tgns)
                ShowAlert("Đã giao việc thành công !")
            End If
        End If

    End Sub

    Private Sub gLueNhanSu_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gLueNhanSu.Properties.ButtonClick
        If e.Button.Index = 1 Then
            gLueNhanSu.EditValue = DBNull.Value
        End If

    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        pcGiaoXuLy.Visible = False
        pcGiaoXuLy.Enabled = False
    End Sub
    'Tai
End Class