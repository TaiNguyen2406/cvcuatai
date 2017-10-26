Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering

Public Class frmXuLyYeuCau
    Private tbtmp As DataTable
    Private _exit As Boolean = False

    Private Sub frmXuLyYeuCau_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Application.DoEvents()
        LoadCbKhachHang()
        LoadCbPhuTrach()
        LoadCbTrangThai()
        LoadCbPhu()
        If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
            colNhanChuyenMa.Visible = False
            colTGNhanChuyenMa.Visible = False
        Else
            colNhanBaoGia.Visible = False
            colTGNhanBaoGia.Visible = False
            colGiaCungUng.Visible = False
            colTienTeCungUng.Visible = False
            colTGCungUng.Visible = False
        End If
        Application.DoEvents()
        ShowWaiting("Đang tải dữ liệu chuyển mã ...")
        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        CloseWaiting()
        tbTuNgay.Enabled = False
        tbDenNgay.Enabled = False
        cbTieuChiLoc.Enabled = False
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date

        'Threading.Thread.Sleep(2000)
        Application.DoEvents()
        btTaiDS.PerformClick()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbTrangThai.Enabled = False
        End If

    End Sub

    Public Sub LoadCbPhu()
        Dim sql As String = ""
        sql &= " SELECT Ma,NoiDung FROm tblTuDien WHERE Loai=@MucDoCan "
        sql &= " SELECT Ma,NoiDung FROm tblTuDien WHERE Loai=@ThoiGianCungUng "
        sql &= " SELECT ID,Ten FROm TENDONVITINH "
        sql &= " SELECT ID,Ten FROM tblTienTe"
        sql &= " SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa "
        AddParameterWhere("@MucDoCan", LoaiTuDien.MucDoCan)
        AddParameterWhere("@ThoiGianCungUng", LoaiTuDien.TGCungUng)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbMucDoCan.DataSource = ds.Tables(0)
            cbTGCungUng.DataSource = ds.Tables(1)
            cbDVT.DataSource = ds.Tables(2)
            rcbTienTe.DataSource = ds.Tables(3)
            rbtFilterMaKH.DataSource = ds.Tables(4)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbKhachHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa ")
        If Not tb Is Nothing Then
            rcbMaKH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbPhuTrach()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID ")
        If Not tb Is Nothing Then
            rcbPhuTrach.DataSource = tb
            rcbNguoiNhanXuLy.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbTrangThai()
        AddParameterWhere("@Loai", LoaiTuDien.YeuCauDen)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma ")
        If Not tb Is Nothing Then
            rcbTrangThai.DataSource = tb
            rcbTrangThaiCT.DataSource = tb
            If tb.Rows.Count > 0 Then
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    cbTrangThai.EditValue = TrangThaiYeuCau.CanHoiGiaHang
                Else
                    cbTrangThai.EditValue = TrangThaiYeuCau.CanChuyenMa
                End If
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rcbMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKhachHang.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbPhuTrach_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhuTrach.ButtonClick
        If e.Button.Index = 1 Then
            cbPhuTrach.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbNguoiNhanXuLy_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNguoiNhanXuLy.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiNhanXuLy.EditValue = Nothing
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

    Public Sub LoadYeuCau()
        ShowWaiting("Đang tải yêu cầu ...")
        Dim sql As String = ""
        sql &= " DECLARE @tb  table"
        sql &= " ("
        sql &= " 	STT  int,"
        sql &= " 	NgayThangYCChinh  datetime,"
        sql &= " 	IDYC int,"
        sql &= " 	Chon  bit,"
        sql &= " 	SoPhieu  nvarchar(10),"
        sql &= " 	NgayNhanYeuCau  datetime,"
        sql &= " 	ttcMa  nvarchar(MAX),"
        sql &= " 	NoiDung  nvarchar(MAX),"
        sql &= " 	NguoiXuLy  nvarchar(MAX),"
        sql &= " 	PhuTrach  nvarchar(MAX),"
        sql &= " 	NguoiNhanBaoGia  nvarchar(MAX),"
        sql &= " 	IDVatTu  int,"
        sql &= " 	SoLuong  float,"
        sql &= " 	Model  nvarchar(500),"
        sql &= " 	TenVT  Nvarchar(MAX),"
        sql &= " 	HangSX  nvarchar(MAX),"
        sql &= " 	FileDinhKem  Nvarchar(Max),"
        sql &= " 	MucDoCan  int,"
        sql &= " 	NgayNhanChuyenMa  datetime,"
        sql &= " 	NgayNhanHoiGia  datetime,"
        sql &= " 	GiaCungUng  float,"
        sql &= " 	TGCungUng  Float,"
        sql &= " 	IDTienTeCungUng  int,"
        sql &= " 	HoiThongTin  nvarchar(MAX),"
        sql &= " 	DVT  nvarchar(50),"
        sql &= " 	IDTakeCare  int,"
        sql &= " 	NgayHoiGia  Datetime,"
        sql &= " 	NguoiBaoGia  nvarchar(MAX),"
        sql &= " 	NguoiChuyenMa  Nvarchar(MAX),"
        sql &= " 	NgayChuyenMa  datetime,"
        sql &= " 	TrangThai  int,"
        sql &= "    AZ int,"
        sql &= "    SoPhut float"
        sql &= " )"
        sql &= " INSERT INTO @tb "
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY NgayNhanYeuCau ) AS STT,BANGYEUCAU.NgayThang as NgayThangYCChinh, YEUCAUDEN.ID AS IDYC, COnvert(bit,0)Chon, YEUCAUDEN.SoPhieu, YEUCAUDEN.NgayNhanYeuCau,KHACHHANG.ttcMa,YEUCAUDEN.NoiDung,NGUOIXULY.Ten AS NguoiXuLy,PHUTRACH.Ten AS PhuTrach,NGUOINHANBAOGIA.Ten AS NguoiNhanBaoGia,"
        sql &= " YEUCAUDEN.IDVatTu,YEUCAUDEN.SoLuong,VATTU.Model,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,YEUCAUDEN.FileDinhKem,YEUCAUDEN.MucDoCan, (CASE WHEN YEUCAUDEN.IDNhanChuyenMa IS NULL THEN NULL ELSE YEUCAUDEN.NgayNhanChuyenMa END) AS NgayNhanChuyenMa,YEUCAUDEN.NgayNhanHoiGia,"
        sql &= " YEUCAUDEN.GiaCungUng,YEUCAUDEN.TGCungUng,YEUCAUDEN.IDTienTeCungUng,YEUCAUDEN.HoiThongTin,TENDONVITINH.Ten AS DVT,BANGYEUCAU.IDTakeCare,YEUCAUDEN.NgayHoiGia,(CASE WHEN YEUCAUDEN.NgayHoiGia IS NULL THEN NULL ELSE NGUOIBAOGIA.Ten END)AS NguoiBaoGia, "
        sql &= " (CASE WHEN YEUCAUDEN.NgayChuyenMa IS NULL THEN NULL ELSE NGUOICHUYENMA.Ten END) AS NguoiChuyenMa, YEUCAUDEN.NgayChuyenMa, YEUCAUDEN.TrangThai,YEUCAUDEN.AZ,"
        sql &= " Datediff(minute,NgayNhanYeuCau,(CASE WHEN YEUCAUDEN.TrangThai=3 THEN GETDATE()  ELSE NgayHoiGia END))SoPhut"
        sql &= " FROM YEUCAUDEN "
        sql &= " INNER JOIN BANGYEUCAU ON YEUCAUDEN.Sophieu=BANGYEUCAU.Sophieu"
        sql &= " LEFT JOIN KHACHHANG ON BANGYEUCAU.IDkhachhang=KHACHHANG.ID"
        sql &= " LEFT JOIN NHANSU AS PHUTRACH ON BANGYEUCAU.IDTakecare=PHUTRACH.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOIXULY ON YEUCAUDEN.IDNhanChuyenMa=NGUOIXULY.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOINHANBAOGIA ON YEUCAUDEN.IDNhanBaoGia=NGUOINHANBAOGIA.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOIBAOGIA ON YEUCAUDEN.IDHoiGia=NGUOIBAOGIA.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOICHUYENMA ON YEUCAUDEN.IDChuyenMa=NGUOICHUYENMA.ID"
        sql &= " LEFT JOIN VATTU ON YEUCAUDEN.IDVattu = VATTU.ID "
        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenvattu = TENVATTU.ID "
        sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh = TENDONVITINH.ID "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat = TENHANGSANXUAT.ID"
        sql &= " WHERE 1=1 "

        If Not cbTrangThai.EditValue Is Nothing Then
            sql &= " AND YEUCAUDEN.TrangThai=@TrangThai"
            AddParameterWhere("@TrangThai", cbTrangThai.EditValue)
        End If

        If Not cbPhuTrach.EditValue Is Nothing Then
            sql &= " AND BANGYEUCAU.IDTakeCare=@PhuTrach"
            AddParameterWhere("@PhuTrach", cbPhuTrach.EditValue)
        End If

        If Not cbNguoiNhanXuLy.EditValue Is Nothing Then
            If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                sql &= " AND YEUCAUDEN.IDNhanBaoGia=@IDNhanXuLy"
            Else
                sql &= " AND YEUCAUDEN.IDNhanChuyenMa=@IDNhanXuLy"
            End If

            AddParameterWhere("@IDNhanXuLy", cbNguoiNhanXuLy.EditValue)
        End If

        If Not cbKhachHang.EditValue Is Nothing Then
            sql &= " AND BANGYEUCAU.IDKhachHang=@MaKH"
            AddParameterWhere("@MaKH", cbKhachHang.EditValue)
            colMaKH.Visible = False
        Else
            colMaKH.Visible = True
            colMaKH.VisibleIndex = 1
        End If

        If cbTieuChiTG.EditValue <> "Tất cả" Then
            If cbTieuChiLoc.EditValue = "Thời gian gửi yêu cầu" Then
                sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayNhanYeuCau,103),103) BETWEEN @TuNgay AND @DenNgay "
            ElseIf cbTieuChiLoc.EditValue = "Thời gian nhận xử lý" Then
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayNhanHoiGia,103),103) BETWEEN @TuNgay AND @DenNgay "
                Else
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayNhanChuyenMa,103),103) BETWEEN @TuNgay AND @DenNgay "
                End If

            Else
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayHoiGia,103),103) BETWEEN @TuNgay AND @DenNgay "
                Else
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayChuyenMa,103),103) BETWEEN @TuNgay AND @DenNgay "
                End If
            End If

            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        sql &= " SELECT (CASE WHEN SoPhut is not null then Round(Convert(float,SoPhut)/60,1) END)SoGio,* FROM @tb ORDER BY NgayNhanYeuCau "

        sql &= " SELECT  tblQuaTrinhBaoGia.ID,IDYeuCau,ThoiGianBaoGia, NHANSU.Ten AS NguoiBaoGia,"
        sql &= " Gia,tblTuDien.NoiDung AS ThoiGianCungUng,tblTienTe.Ten AS TienTe,GhiChu,ChaoGia,DatHang,ThoiGianChaoGia,ThoiGianDatHang,IDCungUng"
        sql &= " FROM tblQuaTrinhBaoGia"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblQuaTrinhBaoGia.IDCungUng AND NHANSU.Noictac=74"
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=tblQuaTrinhBaoGia.IDTGCungUng AND tblTuDien.Loai=4"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=tblQuaTrinhBaoGia.IDTienTe"
        sql &= "  WHERE tblQuaTrinhBaoGia.IDYeuCau IN (SELECT DISTINCT IDYC FROM @tb)"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            ds.Relations.Add(ds.Tables(0).Columns("IDYC"), ds.Tables(1).Columns("IDYeuCau"))
            ds.Relations.Item(0).RelationName = "Quá trình báo giá"

            gdvYC.DataSource = ds.Tables(0)
            tbtmp = ds.Tables(0).Clone
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub gdvYCCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvYCCT.CustomDrawCell
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.VisibleIndex = 0 And view.IsMasterRowEmptyEx(e.RowHandle, 0) And view.IsMasterRowEmptyEx(e.RowHandle, 1) Then
            CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty
        End If
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        LoadYeuCau()
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvYCCT.CalcHitInfo(gdvYC.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If

        If gdvYCCT.GetMasterRowExpanded(gdvYCCT.FocusedRowHandle) Then
            mSuaQuaTrinhBaoGia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            mSuaQuaTrinhBaoGia.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub gdvYC_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvYC.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdvYC.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mNhanXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhanXuLy.ItemClick
        If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
            Dim _countChecked As Int32 = 0
            Dim _strIDYeuCau As String = ""
            For i As Integer = 0 To gdvYCCT.DataRowCount - 1
                If gdvYCCT.GetRowCellValue(i, "Chon") Then
                    _countChecked += 1
                    _strIDYeuCau &= gdvYCCT.GetRowCellValue(i, "IDYC")
                    _strIDYeuCau &= ","
                End If
            Next


            If _countChecked = 0 Then
                AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT (SELECT Ten FROM NHANSU WHERE ID=YEUCAUDEN.IDNhanBaoGia) Ten FROM YEUCAUDEN WHERE ID=@IDYC")
                If Not tb Is Nothing Then
                    If tb.Rows(0)(0).ToString.Trim = "" Then
                        If Not ShowCauHoi("Bạn có chắc muốn nhận xử lý yêu cầu này hay không ?") Then Exit Sub

                    Else
                        If Not ShowCauHoi(tb.Rows(0)(0).ToString & " đã nhận xử lý yêu cầu này, bạn có muốn nhận xử lý thay khổng ?") Then Exit Sub
                    End If
                    Dim tg As DateTime = GetServerTime()
                    AddParameter("@IDNhanBaoGia", TaiKhoan)
                    AddParameter("@NgayNhanHoiGia", tg)
                    AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
                    If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        gdvYCCT.SetFocusedRowCellValue("NgayNhanHoiGia", tg)
                        gdvYCCT.SetFocusedRowCellValue("NguoiNhanBaoGia", NguoiDung)

                        ShowAlert("Đã nhận xử lý yêu cầu !")
                    End If

                End If

            Else
                _strIDYeuCau = _strIDYeuCau.Substring(0, _strIDYeuCau.Length - 1)
                If ShowCauHoi("Nhận xử lý cho " & _countChecked.ToString & " yêu cầu đã chọn ?") Then
                    Dim tg As DateTime = GetServerTime()
                    AddParameter("@IDNhanBaoGia", TaiKhoan)
                    AddParameter("@NgayNhanHoiGia", tg)
                    'AddParameterWhere("@IDYC", "(" & _strIDYeuCau & ")")
                    If doUpdate("YEUCAUDEN", "ID IN (" & _strIDYeuCau & ")") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        btTaiDS.PerformClick()
                        ShowAlert("Đã nhận xử lý yêu cầu !")
                    End If
                End If
            End If
        Else
            Dim _countChecked As Int32 = 0
            Dim _strIDYeuCau As String = ""
            For i As Integer = 0 To gdvYCCT.DataRowCount - 1
                If gdvYCCT.GetRowCellValue(i, "Chon") Then
                    _countChecked += 1
                    _strIDYeuCau &= gdvYCCT.GetRowCellValue(i, "IDYC")
                    _strIDYeuCau &= ","
                End If
            Next


            If _countChecked = 0 Then
                AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT (SELECT Ten FROM NHANSU WHERE ID=YEUCAUDEN.IDNhanChuyenMa) Ten FROM YEUCAUDEN WHERE ID=@IDYC")
                If Not tb Is Nothing Then
                    If tb.Rows(0)(0).ToString.Trim = "" Then
                        If ShowCauHoi("Bạn có chắc muốn nhận xử lý yêu cầu này hay không ?") Then
                            Dim tg As DateTime = GetServerTime()
                            AddParameter("@IDNhanChuyenMa", TaiKhoan)
                            AddParameter("@NgayNhanChuyenMa", tg)
                            AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
                            If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            Else
                                gdvYCCT.SetFocusedRowCellValue("NgayNhanChuyenMa", tg)
                                gdvYCCT.SetFocusedRowCellValue("NguoiXuLy", NguoiDung)
                                ShowAlert("Đã nhận xử lý yêu cầu !")
                            End If
                        End If
                    Else
                        ShowThongBao(tb.Rows(0)(0).ToString & " đã nhận xử lý yêu cầu này !")
                    End If
                End If
            Else
                _strIDYeuCau = _strIDYeuCau.Substring(0, _strIDYeuCau.Length - 1)
                If ShowCauHoi("Nhận xử lý cho " & _countChecked.ToString & " yêu cầu đã chọn ?") Then
                    Dim tg As DateTime = GetServerTime()
                    AddParameter("@IDNhanChuyenMa", TaiKhoan)
                    AddParameter("@NgayNhanChuyenMa", tg)
                    'AddParameterWhere("@IDYC", "(" & _strIDYeuCau & ")")
                    If doUpdate("YEUCAUDEN", "ID IN (" & _strIDYeuCau & ")") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        btTaiDS.PerformClick()
                        ShowAlert("Đã nhận xử lý yêu cầu !")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then
            If ShowCauHoi("Chuyển mã vật tư " & gdvChuyenMaCT.GetFocusedRowCellValue("Model") & " cho yêu cầu đang được chọn ?") Then

                Dim tg As DateTime = GetServerTime()
                AddParameter("@IDVatTu", gdvChuyenMaCT.GetFocusedRowCellValue("ID"))
                AddParameter("@IDChuyenMa", TaiKhoan)
                AddParameter("@NgayChuyenMa", tg)
                AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
                If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvYCCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetFocusedRowCellValue("TenVT"))
                    gdvYCCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetFocusedRowCellValue("Model"))
                    gdvYCCT.SetFocusedRowCellValue("HangSX", gdvChuyenMaCT.GetFocusedRowCellValue("HangSX"))
                    gdvYCCT.SetFocusedRowCellValue("NgayChuyenma", tg)
                    gdvYCCT.SetFocusedRowCellValue("NguoiXuLy", NguoiDung)
                    gdvYCCT.CloseEditor()
                    gdvYCCT.UpdateCurrentRow()
                    ShowAlert("Đã thực hiện !")
                End If
                'gdvYCCT.SetFocusedRowCellValue("IDVattu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                'gdvYCCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetRowCellValue(i, "TenVT"))
                'gdvYCCT.SetFocusedRowCellValue("Thongso", gdvChuyenMaCT.GetRowCellValue(i, "Thongso"))
                'gdvYCCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetRowCellValue(i, "Model"))
                'gdvYCCT.SetFocusedRowCellValue("Soluong", gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                'gdvYCCT.SetFocusedRowCellValue("IDDVT", gdvChuyenMaCT.GetRowCellValue(i, "IDDVT"))
                'gdvYCCT.SetFocusedRowCellValue("TenHang", gdvChuyenMaCT.GetRowCellValue(i, "HangSX"))
                'gdvYCCT.SetFocusedRowCellValue("HangTon", gdvChuyenMaCT.GetRowCellValue(i, "HangTon"))
                'gdvYCCT.SetFocusedRowCellValue("slTon", gdvChuyenMaCT.GetRowCellValue(i, "slTon"))
                'gdvYCCT.SetFocusedRowCellValue("NgayChuyenma", GetServerTime)
                'gdvYCCT.SetFocusedRowCellValue("IDChuyenma", Convert.ToInt32(TaiKhoan))
                'gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
                ' gdvChuyenMaCT.GetFocusedRowCellValue("HangTon")
                'gdvYCCT.CloseEditor()
                'gdvYCCT.UpdateCurrentRow()
            End If
        End If
    End Sub

    'Private Sub tbSL_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbSL.KeyDown
    '    If e.KeyCode = Keys.Escape Then
    '        pNhapSoLuong.Visible = False
    '    ElseIf e.KeyCode = Keys.Enter Then

    '    End If
    'End Sub

    'Private Sub gdvYCCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvYCCT.RowUpdated
    '    If Not gdvYCCT.GetRowCellValue(e.RowHandle, "GiaCungUng") Is Nothing Then
    '        AddParameter("@GiaCungUng", gdvYCCT.GetRowCellValue(e.RowHandle, "GiaCungUng"))
    '        AddParameter("@GiaCungUng", gdvYCCT.GetRowCellValue(e.RowHandle, "GiaCungUng"))
    '        AddParameter("@GiaCungUng", gdvYCCT.GetRowCellValue(e.RowHandle, "GiaCungUng"))
    '        AddParameter("@GiaCungUng", gdvYCCT.GetRowCellValue(e.RowHandle, "GiaCungUng"))
    '    End If
    'End Sub

    Private Sub gdvYCCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvYCCT.CellValueChanged
        'If e.Column.FieldName = "GiaCungUng" Or e.Column.FieldName = "IDTienTeCungUng" Then
        Select Case e.Column.FieldName
            Case "HoiThongTin"
                AddParameter("@HoiThongTin", e.Value)
                AddParameterWhere("@IDYC", gdvYCCT.GetRowCellValue(e.RowHandle, "IDYC"))
                If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
        End Select
    End Sub

    Private Sub mHoanTat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHoanTat.ItemClick
        ' If gdvYCCT.GetFocusedRowCellDisplayText("GiaCungUng").ToString.Trim <> "" Or gdvYCCT.GetFocusedRowCellDisplayText("IDTienTeCungUng").ToString.Trim <> "" Or gdvYCCT.GetFocusedRowCellDisplayText("TGCungUng").ToString.Trim <> "" Then
        If ShowCauHoi("Bạn có chắc là đã xử lý xong hay không ?") Then
            Dim index As Integer = 0
            index = gdvYCCT.FocusedRowHandle
            Dim tg As DateTime = GetServerTime()
            If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                AddParameter("@GiaCungUng", gdvYCCT.GetFocusedRowCellValue("GiaCungUng"))
                AddParameter("@IDTienTeCungUng", gdvYCCT.GetFocusedRowCellValue("IDTienTeCungUng"))
                AddParameter("@TGCungUng", gdvYCCT.GetFocusedRowCellValue("TGCungUng"))
                AddParameter("@IDHoiGia", TaiKhoan)
                AddParameter("@NgayHoiGia", tg)
            End If

            AddParameter("@TrangThai", TrangThaiYeuCau.CanChaoGia)
            AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
            If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    AddParameter("@NoiDung", "Đã báo giá yêu cầu: " & gdvYCCT.GetFocusedRowCellValue("SoPhieu") & " KH: " & gdvYCCT.GetFocusedRowCellValue("ttcMa") & vbCrLf & " - nội dung yêu cầu: " & gdvYCCT.GetFocusedRowCellValue("NoiDung"))
                Else
                    AddParameter("@NoiDung", "Đã xử lý yêu cầu: " & gdvYCCT.GetFocusedRowCellValue("SoPhieu") & " KH: " & gdvYCCT.GetFocusedRowCellValue("ttcMa") & vbCrLf & " - nội dung yêu cầu: " & gdvYCCT.GetFocusedRowCellValue("NoiDung"))
                End If
                AddParameter("@ThoiGian", tg)
                AddParameter("@IDNhanVien", gdvYCCT.GetFocusedRowCellValue("IDTakeCare"))
                If doInsert("tblThongBao") Is Nothing Then
                    ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
                End If

                btTaiDS.PerformClick()
                gdvYCCT.ClearSelection()
                gdvYCCT.FocusedRowHandle = index - 1
                gdvYCCT.SelectRow(index - 1)
                ShowAlert("Đã thực hiện !")
            End If
        End If

        'Else
        'ShowCanhBao("Chưa có thông tin báo giá !")
        'End If

    End Sub

    Private Sub rcbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTrangThai.ButtonClick
        If e.Button.Index = 1 Then
            cbTrangThai.EditValue = Nothing
        End If
    End Sub

    Private Sub btPhanBoCVBoPhanMua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btPhanBoCVBoPhanMua.ItemClick

    End Sub

    Private Sub btTinhTrangVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVatTu.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvYCCT.GetFocusedRowCellValue("IDVatTu")
        f._HienThongTinNX = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienNCC = False
                f._HienCGXK = False
            Else
                f._HienNCC = True
                f._HienCGXK = True
            End If
        Else
            f._HienCGXK = True
            f._HienNCC = True
        End If
        f.ShowDialog()
    End Sub

    Private Sub mChonBoChon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChonBoChon.ItemClick
        gdvYCCT.CloseEditor()
        gdvYCCT.UpdateCurrentRow()
        gdvYCCT.BeginUpdate()
        Dim _isCheck As Boolean = False
        If gdvYCCT.SelectedRowsCount > 0 Then
            _isCheck = Not gdvYCCT.GetRowCellValue(gdvYCCT.GetSelectedRows(0), "Chon")
        Else
            Exit Sub
        End If
        For i As Integer = 0 To gdvYCCT.SelectedRowsCount - 1
            gdvYCCT.SetRowCellValue(gdvYCCT.GetSelectedRows(i), "Chon", _isCheck)
        Next
        gdvYCCT.EndUpdate()
        gdvYCCT.CloseEditor()
        gdvYCCT.UpdateCurrentRow()
    End Sub

    Private Sub gdvYCCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvYCCT.RowCellClick

        If e.Column.FieldName = "Chon" Then
            gdvYCCT.CloseEditor()
            gdvYCCT.UpdateCurrentRow()
            gdvYCCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
            gdvYCCT.CloseEditor()
            gdvYCCT.UpdateCurrentRow()
        ElseIf e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa") & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub rPopupFileCT_Popup(sender As System.Object, e As System.EventArgs) Handles rPopupFileCT.Popup
        gListFileCT.Text = "Danh sách file đính kèm"
        If _exit = True Then Exit Sub
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)

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

    Private Sub tbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChiTG.EditValueChanged
        If cbTieuChiTG.EditValue = "Tất cả" Then
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
            cbTieuChiLoc.Enabled = False
        Else
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
            cbTieuChiLoc.Enabled = True
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(tbDenNgay.EditValue)
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub


    Private Sub btLapYeuCauHoiGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLapYeuCauHoiGia.ItemClick
        Dim _countChecked As Int32 = 0
        'Dim tbtmp As DataTable = CType(gdvYC.DataSource, DataTable).Clone
        For i As Integer = 0 To gdvYCCT.DataRowCount - 1
            If gdvYCCT.GetRowCellValue(i, "Chon") Then
                _countChecked += 1
                Dim r As DataRow = tbtmp.NewRow()
                For j As Integer = 0 To r.Table.Columns.Count - 1
                    r(tbtmp.Columns(j).ColumnName) = gdvYCCT.GetRowCellValue(i, tbtmp.Columns(j).ColumnName)
                Next


                tbtmp.Rows.Add(r)

            End If
        Next
        If _countChecked = 0 Then
            ShowCanhBao("Chưa chọn yêu cầu hỏi giá !")
            Exit Sub
        End If

        fCNYeuCauHoiGia = New frmCNYeuCauHoiGia
        fCNYeuCauHoiGia.Tag = Me.Parent.Tag
        fCNYeuCauHoiGia.tbtmpYC = tbtmp
        fCNYeuCauHoiGia.TrangThai.isAddNew = True
        fCNYeuCauHoiGia.ShowDialog()
    End Sub

    Private Sub gdvListFileCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvListFileCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuFile.ShowPopup(gdvListFile.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenuFile_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuFile.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvListFileCT.CalcHitInfo(gdvListFile.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub mThemFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemFile.ItemClick
        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()

            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa")) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa"))
            End If
            For Each file In OpenFile.FileNames
                ShowWaiting("Đang chuyển file lên server ...")
                path = "XL YC" & gdvYCCT.GetFocusedRowCellValue("SoPhieu") & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(file)
                Try
                    IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa") & "\" & path, True)
                    gdvListFileCT.AddNewRow()
                    gdvListFileCT.SetFocusedRowCellValue("File", path)

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
                CloseWaiting()
            Next
            Impersonator.EndImpersonation()

        End If
        _exit = True
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
        SendKeys.Send("{F4}")
    End Sub


    Private Sub rPopupFileCT_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rPopupFileCT.Closed
        Dim _file As String = StrDSFile(gdvListFileCT)
        If _file.Trim = "" Then Exit Sub
        AddParameter("@FileDinhKem", _file)
        AddParameterWhere("@ID", gdvYCCT.GetFocusedRowCellValue("IDYC"))
        If doUpdate("YEUCAUDEN", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CType(sender, PopupContainerEdit).EditValue = _file
        End If
        _exit = False
    End Sub

    Private Sub mXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaFile.ItemClick
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        'If Convert.ToInt16(gdvCT.GetFocusedRowCellValue("TrangThai")) = Convert.ToInt16(TrangThaiYeuCau.DaChaoGia) Then
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        'End If
        Dim str() As String = gdvListFileCT.GetFocusedRowCellValue("File").ToString.Split(" ")
        If str(str.Length - 1) = TaiKhoan Then
            ShowAlert("1")
        End If

        If ShowCauHoi("Xoá file được chọn ?") Then
            gdvListFileCT.DeleteSelectedRows()
            If ShowCauHoi("Xoá luôn file trong hệ thống ?") Then
                Try
                    IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If

        End If
    End Sub

    Private Sub mThemQuaTrinhBaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemQuaTrinhBaoGia.ItemClick
        'TrangThai.isAddNew = True
        'Dim f As New frmCNQuaTrinhBaoGia
        'f._SoYC = gdvYCCT.GetFocusedRowCellValue("SoPhieu") & " KH: " & gdvYCCT.GetFocusedRowCellValue("ttcMa")
        'f._IDYC = gdvYCCT.GetFocusedRowCellValue("IDYC")
        'f._IdPhuTrach = gdvYCCT.GetFocusedRowCellValue("IDTakeCare")
        'f.ShowDialog()
    End Sub

    Private Sub gdvQuaTrinhBaoGia_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvQuaTrinhBaoGia.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuKD.ShowPopup(gdvYC.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mSuaQuaTrinhBG_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaQuaTrinhBG.ItemClick
        'If Not gdvYCCT.GetMasterRowExpanded(gdvYCCT.FocusedRowHandle) Then Exit Sub
        'TrangThai.isUpdate = True
        'Dim Index As Integer = gdvYCCT.FocusedRowHandle
        'Dim indexGD As Integer = CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle
        'objID = CType(gdvYCCT.GetDetailView(gdvYCCT.FocusedRowHandle, gdvYCCT.GetRelationIndex(gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
        'Dim f As New frmCNQuaTrinhBaoGia
        'f.Tag = Me.Parent.Tag
        'f._SoYC = gdvYCCT.GetFocusedRowCellValue("SoPhieu") & " KH: " & gdvYCCT.GetFocusedRowCellValue("ttcMa")
        'f._IDYC = gdvYCCT.GetFocusedRowCellValue("IDYC")
        'f._IdPhuTrach = gdvYCCT.GetFocusedRowCellValue("IDTakeCare")
        'f.ShowDialog()
    End Sub

    Private Sub pMenuKD_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuKD.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvQuaTrinhBaoGia.CalcHitInfo(gdvYC.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvChuyenMaCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvChuyenMaCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pmenuVT.ShowPopup(gdvChuyenMa.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
        f._HienThongTinNX = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienNCC = False
                f._HienCGXK = False
            Else
                f._HienNCC = True
                f._HienCGXK = True
            End If
        Else
            f._HienCGXK = True
            f._HienNCC = True
        End If
        f.ShowDialog()
    End Sub

    Private Sub btSuaThongTinVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaThongTinVT.ItemClick
        If Not KiemTraQuyenSuDung("Menu", "mThongSo", DanhMucQuyen.QuyenSua) Then Exit Sub
        deskTop.OpenTab("Thông số", "THONGSO", New frmThongSo, True, Nothing, "mThongSo")
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterMaVT.EditValue = gdvChuyenMaCT.GetFocusedRowCellValue("Model")
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btTaiLai.PerformClick()
    End Sub

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

    Private Sub btTaiTop10_Click(sender As System.Object, e As System.EventArgs) Handles btTop10.ItemClick
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

    Private Sub mThemDongMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemDongMoi.ItemClick
        Dim tg As DateTime = GetServerTime()
        If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
            AddParameter("@NgayNhanHoiGia", tg)
            AddParameter("@IDNhanBaoGia", TaiKhoan)

        Else
            AddParameter("@NgayNhanChuyenMa", tg)
            AddParameter("@IDNhanChuyenMa", TaiKhoan)
        End If

        AddParameter("@SoPhieu", gdvYCCT.GetFocusedRowCellValue("SoPhieu"))
        AddParameter("@NgayNhanYeuCau", gdvYCCT.GetFocusedRowCellValue("NgayNhanYeuCau"))
        AddParameter("@NoiDung", gdvYCCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@SoLuong", gdvYCCT.GetFocusedRowCellValue("SoLuong"))
        AddParameter("@MucDoCan", gdvYCCT.GetFocusedRowCellValue("MucDoCan"))
        AddParameter("@TrangThai", gdvYCCT.GetFocusedRowCellValue("TrangThai"))
        AddParameter("@AZ", gdvYCCT.GetFocusedRowCellValue("AZ"))
        Dim _id As Object = doInsert("YEUCAUDEN")
        If _id Is Nothing Then
            ShowCanhBao(LoiNgoaiLe)
            Exit Sub
        End If


        Dim dr As DataRow = CType(gdvYC.DataSource, DataTable).NewRow
        dr("STT") = gdvYCCT.GetFocusedRowCellValue("STT")
        dr("NgayThangYCChinh") = gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")
        dr("IDYC") = _id
        dr("Chon") = gdvYCCT.GetFocusedRowCellValue("Chon")
        dr("SoPhieu") = gdvYCCT.GetFocusedRowCellValue("SoPhieu")
        dr("NgayNhanYeuCau") = gdvYCCT.GetFocusedRowCellValue("NgayNhanYeuCau")
        dr("ttcMa") = gdvYCCT.GetFocusedRowCellValue("ttcMa")
        dr("NoiDung") = gdvYCCT.GetFocusedRowCellValue("NoiDung")
        'dr("NguoiXuLy") = NguoiDung
        dr("PhuTrach") = gdvYCCT.GetFocusedRowCellValue("PhuTrach")

        'dr("NguoiNhanBaoGia") = TaiKhoan
        dr("SoLuong") = gdvYCCT.GetFocusedRowCellValue("SoLuong")
        dr("MucDoCan") = gdvYCCT.GetFocusedRowCellValue("MucDoCan")
        dr("NgayNhanChuyenMa") = gdvYCCT.GetFocusedRowCellValue("NgayNhanChuyenMa")
        dr("NgayNhanHoiGia") = gdvYCCT.GetFocusedRowCellValue("NgayNhanHoiGia")
        dr("IDTakeCare") = gdvYCCT.GetFocusedRowCellValue("IDTakeCare")
        dr("TrangThai") = gdvYCCT.GetFocusedRowCellValue("TrangThai")

        If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
            dr("NgayNhanHoiGia") = tg
            'dr("IDNhanBaoGia") = TaiKhoan
            dr("NguoiNhanBaoGia") = NguoiDung

        Else
            'dr("IDNhanChuyenMa") = TaiKhoan
            dr("NgayNhanChuyenMa") = tg
            dr("NguoiXuLy") = NguoiDung
        End If


        CType(gdvYC.DataSource, DataTable).Rows.InsertAt(dr, gdvYCCT.FocusedRowHandle + 1)
        CType(gdvYC.DataSource, DataTable).AcceptChanges()
    End Sub

    Private Sub gdvYCCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvYCCT.RowCellStyle
        If e.Column.FieldName = "SoGio" AndAlso Not IsDBNull(e.CellValue) Then
            If e.CellValue >= 2 And e.CellValue < 4 Then
                e.Appearance.BackColor = Color.Yellow
            ElseIf e.CellValue >= 4 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub

    Private Sub mXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Yeu cau can xu ly.xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvYCCT, False)

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
    End Sub
End Class
