Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports BACSOFT.TAI
Public Class frmCNDatTon
    Public _SoPhieu As Object
    Public _Exit As Boolean = False
    Private Sub frmCNDatTon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim hientai As DateTime = GetServerTime()
        deTuNgay.EditValue = hientai.AddYears(-2)
        deDenNgay.EditValue = hientai
        loadTakeCare()
        If TrangThai.isAddNew Then
            cbTakeCare.EditValue = CType(TaiKhoan, Int32)
        End If

        loadDSKH()
        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        riLueTrangThai.DataSource = tableDatTon()
        If TrangThai.isUpdate = True Then
            loadThongTinChung()
        Else
            deTGLap.EditValue = hientai
        End If
        loadGv()
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbTakeCare.Enabled = True
            deTGLap.Enabled = True
        End If
    End Sub
    Private Sub loadTakeCare()
        Dim sql As String = ""
        sql = "select ID,Ten from NHANSU where Noictac=74"
        cbTakeCare.Properties.DataSource = ExecuteSQLDataTable(sql)
    End Sub
    Private Sub loadDSKH()
        Dim sql As String = ""
        sql &= " SELECT distinct KHACHHANG. * from KHACHHANG left join NHANSU on KHACHHANG .ID=NHANSU .Noictac left join NHANSU TAKECARE on TAKECARE .ID =NHANSU .Chamsoc where 1=1"
        If cbTakeCare.EditValue IsNot Nothing Then ' And TrangThai.isAddNew = True
            sql &= " and (TAKECARE .ID=@Chamsoc  OR KHACHHANG.IDTakecare is null  )"
            AddParameterWhere("@Chamsoc", cbTakeCare.EditValue)
        End If
        sql &= " order by ttcMa"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt IsNot Nothing Then
            gdvMaKH.Properties.DataSource = dt
            rbtFilterMaKH.DataSource = dt
        End If

    End Sub
    Private Sub GridLookUpEdit1View_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridLookUpEdit1View.RowCellStyle
        Dim view = GridLookUpEdit1View
        If (IsDBNull(view.GetRowCellValue(e.RowHandle, "IDTakecare")) Or view.GetRowCellValue(e.RowHandle, "IDTakecare") Is Nothing) And Not view.IsFilterRow(e.RowHandle) Then
            e.Appearance.BackColor = Color.FromArgb(255, 192, 192)
        End If
    End Sub
    Public Sub loadNguoiGD()
        AddParameterWhere("@IDCTY", gdvMaKH.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,Chamsoc, (select Ten from NHANSU CS where CS.ID=NhanSu.Chamsoc) TenChamSoc FROM NHANSU WHERE Noictac=@IDCTY")
        If Not tb Is Nothing Then
            cbNguoiGD.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
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
    Private Sub LoadDSVatTuDungChuyenMa()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sqlWhere As String = " WHERE Maloi=0 "

        Dim sql As String = " Select NULL AS CanhBao,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= " (Round((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID),4)-Round((select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID),4)) AS slTon, "


        sql &= "  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = VATTU.ID),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = VATTU.ID),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = VATTU.ID AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = VATTU.ID and SlXuatKho > 0)),0) "
        sql &= " as XuatTam, "

        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
        sql &= " Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
        sql &= " Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"

        sql &= " VATTU.Gianhap1 AS Gianhap, tblTienTe.Ten AS Tiente,TonNCC,TENNUOC.Ten AS Xuatxu,convert(float,0) AS SLYC, VATTU.ThongDung,"
        sql &= " VATTU.HangTon,VATTU.HinhAnh,(convert(image,NULL))HienThi,VATTU.TaiLieu,VATTU.ConSX,VATTU.Bo "
        If Not btFilterMaKH.EditValue Is Nothing Then
            sql &= "   ,  (select isnull(Sum(SoLuong),0) from CHAOGIA inner join BANGCHAOGIA on BANGCHAOGIA .Sophieu =CHAOGIA .SoPhieu  where IDvattu =Vattu.ID and  Ngaythang  between convert(datetime,@TuNgayXK,103) and convert(datetime,@DenNgayXK ,103) and BANGCHAOGIA .IDKhachhang =tbXK .IdKhachHang ) SLCGChoKH,"
            sql &= " (select isnull(Sum(SoLuong),0) from xuatkho inner join PHIEUXUATKHO on xuatkho.Sophieu = PHIEUXUATKHO.Sophieu   where IDvattu =Vattu.ID and Ngaythang between convert(datetime,@TuNgayXK,103) and convert(datetime,@DenNgayXK ,103)  and PHIEUXUATKHO .IDkhachhang =tbXK .IdKhachHang ) SLXKChoKH,"
            sql &= " (select isnull(Sum(SoLuong),0) from CHAOGIA inner join BANGCHAOGIA on BANGCHAOGIA .Sophieu =CHAOGIA .SoPhieu  where IDvattu =Vattu.ID and  Ngaythang  between convert(datetime,@TuNgayXK,103) and convert(datetime,@DenNgayXK ,103)  )SLCGChoCTy,"
            sql &= " (select isnull(Sum(SoLuong),0) from xuatkho inner join PHIEUXUATKHO on xuatkho.Sophieu = PHIEUXUATKHO.Sophieu   where IDvattu =Vattu.ID and Ngaythang between convert(datetime,@TuNgayXK,103) and convert(datetime,@DenNgayXK ,103) ) SLXKChoCTy"
            AddParameter("@TuNgayXK", deTuNgay.EditValue)
            AddParameter("@DenNgayXK", deDenNgay.EditValue)
        End If
        sql &= " from VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        '  sql &= " inner join V_TonKhoTongHop ON V_TonkhoTonghop.IDVattu = VATTU.ID"
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
    Private Sub loadThongTinChung()
        Dim sql As String
        sql = "select * from DAT_TON where SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", _SoPhieu)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        Else
            tbSoPhieu.EditValue = dt.Rows(0)("SoPhieu")
            cbTakeCare.EditValue = dt.Rows(0)("IdTakeCare")
            deTGLap.EditValue = dt.Rows(0)("ThoiGianLap")
            gdvMaKH.EditValue = dt.Rows(0)("IdKhachHang")
            cbNguoiGD.EditValue = dt.Rows(0)("IdNguoiGiaoDich")

        End If

    End Sub
    Private Sub loadGv()
        Dim sql As String
        sql = " select *, DonGia*SoLuong ThanhTien from ("
        sql &= " select row_number() over(PARTITION by DAT_TON_CHI_TIET.SoPhieu order by NgayChuyenMa asc) STT, DAT_TON_CHI_TIET.*, (Select Ten from TENDONVITINH where id=IDDonvitinh ) TenDVT, (Select ten from TENVATTU where id=IDTenvattu )TenVT, (select ten from TENHANGSANXUAT where id=IDHangSanxuat ) HangSX  ,Model,ThongSo, isnull(DonGIaDatTon ,(CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) ) DonGia from DAT_TON_CHI_TIET inner join VATTU on IdVatTu =VATTU .id  "
        sql &= " )tb where SoPhieu=@SoPhieu order by  STT "
        AddParameterWhere("@SoPhieu", tbSoPhieu.EditValue)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        gc.DataSource = dt
    End Sub

    Private Sub btTaiLai_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If chkTaiAnh.Checked Then
            colHinhAnh.Visible = True
        Else
            colHinhAnh.Visible = False
        End If
        LoadDSVatTuDungChuyenMa()
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

    Private Sub btnLuu_Click(sender As Object, e As EventArgs) Handles btnLuu.Click
        Dim hientai As DateTime = GetServerTime()
        If TrangThai.isAddNew = True Then
            tbSoPhieu.EditValue = LaySoPhieu("DAT_TON")
            deTGLap.EditValue = hientai
        End If
        Dim _IdDatTon As Object = Nothing

        AddParameter("@IdTakeCare", cbTakeCare.EditValue)
        AddParameter("@IdKhachHang", gdvMaKH.EditValue)
        AddParameter("@IdNguoiGiaoDich", cbNguoiGD.EditValue)
        AddParameter("@ThoiGianLap", deTGLap.EditValue)
        If TrangThai.isAddNew = True Then
            AddParameter("@SoPhieu", tbSoPhieu.EditValue)
            _IdDatTon = doInsert("DAT_TON")
        Else
            AddParameterWhere("@SoPhieu", _SoPhieu)
            _IdDatTon = doUpdate("DAT_TON", "SoPhieu=@SoPhieu")
        End If

        If _IdDatTon Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        Else
            For i = 0 To gv.RowCount - 1

                If IsDBNull(gv.GetRowCellValue(i, "Id")) Then
                    AddParameter("@SoPhieu", tbSoPhieu.EditValue)
                    AddParameter("@IdVatTu", gv.GetRowCellValue(i, "IdVatTu"))
                    AddParameter("@SoLuong", gv.GetRowCellValue(i, "SoLuong"))
                    AddParameter("@DonGiaDatTon", gv.GetRowCellValue(i, "DonGia"))
                    AddParameter("@LyDo", gv.GetRowCellValue(i, "LyDo"))
                    AddParameter("@DuKienXuat", gv.GetRowCellValue(i, "DuKienXuat"))
                    AddParameter("@DuKienXuatDen", gv.GetRowCellValue(i, "DuKienXuatDen"))
                    AddParameter("@NgayChuyenMa", gv.GetRowCellValue(i, "NgayChuyenMa"))
                    If doInsert("DAT_TON_CHI_TIET") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    Else
                        TrangThai.isAddNew = False
                        TrangThai.isUpdate = True
                    End If
                Else
                    If gv.GetRowCellValue(i, "isDuyet") = 0 Then
                        AddParameter("@SoPhieu", tbSoPhieu.EditValue)
                        AddParameter("@IdVatTu", gv.GetRowCellValue(i, "IdVatTu"))
                        AddParameter("@SoLuong", gv.GetRowCellValue(i, "SoLuong"))
                        AddParameter("@DonGiaDatTon", gv.GetRowCellValue(i, "DonGia"))
                        AddParameter("@LyDo", gv.GetRowCellValue(i, "LyDo"))
                        AddParameter("@DuKienXuat", gv.GetRowCellValue(i, "DuKienXuat"))
                        AddParameter("@DuKienXuatDen", gv.GetRowCellValue(i, "DuKienXuatDen"))
                        AddParameter("@NgayChuyenMa", gv.GetRowCellValue(i, "NgayChuyenMa"))
                        AddParameterWhere("@Id", gv.GetRowCellValue(i, "Id"))
                        If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            Exit Sub
                        End If
                    End If

                End If
            Next
            AddParameterWhere("@isXoa", 1)
            If doDelete("DAT_TON_CHI_TIET", "isXoa=@isXoa") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
            ShowAlert("Đã cập nhật !")
            Dim row = gv.FocusedRowHandle
            loadGv()
            gv.FocusedRowHandle = row
        End If
    End Sub
    Public Function LayGiaNhapTB(_IDVatTu As Integer) As DataTable
        Dim tg As DateTime = GetServerTime()
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"

        sql &= " DECLARE @TuNgay datetime"
        sql &= " DECLARE @DenNgay datetime"
        sql &= " DECLARE @DauKy	nvarchar(7)"

        sql &= " SET @TuNgay='" & New DateTime(tg.Year, tg.Month, 1).ToString("dd/MM/yyyy") & "'"
        sql &= " SET @DenNgay='" & tg.ToString("dd/MM/yyyy") & "'"
        sql &= " SET @DauKy = right( Convert(nvarchar, Dateadd(day,-1,@TuNgay),103),7)"

        sql &= " DECLARE @tbMain table"
        sql &= " ("
        sql &= " 	IDVatTu int,"
        sql &= " 	SLDauKy float,"
        sql &= " 	DonGiaDauKy float,"
        sql &= " 	SLTrongKy	float,"
        sql &= "    SLNhapTrongKy float,"
        sql &= "    SLXuatTrongKy float,"
        sql &= " 	DonGiaTrongKy float"
        sql &= " )"

        sql &= " DECLARE @tbTonTrongKy table"
        sql &= " ("
        sql &= " 	IDVatTu int,"
        sql &= " 	SL float,"
        sql &= "    SLN float,"
        sql &= "    SLX float,"
        sql &= " 	DonGia float"
        sql &= " )"

        sql &= " DECLARE @tbIDVatTuTrongKy table"
        sql &= " ("
        sql &= "     IDVatTu Int"
        sql &= " )"

        sql &= " INSERT INTO @tbIDVatTuTrongKy(IDVatTu)"
        sql &= " SELECT " & _IDVatTu

        sql &= " INSERT INTO @tbMain(IDVatTu,SLDauKy,DonGiaDauKy)"
        sql &= " SELECT IDVatTu,SoLuong,DonGia"
        sql &= " FROM tblTonDauKy WHERE ThoiGian=@DauKy AND IDVatTu=" & _IDVatTu

        sql &= " INSERT INTO @tbTonTrongKy(IDVatTu,SL,SLN,SLX,DonGia)"
        sql &= " SELECT IDVatTu,"
        sql &= " (ISNULL((SELECT SUM(SoLuong) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE NHAPKHO.IDVatTu=tb2.IDVatTu ),0)"
        sql &= " -"
        sql &= " ISNULL((SELECT SUM(SoLuong) FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE XUATKHO.IDVatTu=tb2.IDVatTu),0))SLTon,"
        sql &= " ISNULL((SELECT SUM(SoLuong) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE NHAPKHO.IDVatTu=tb2.IDVatTu ),0)SLN,"
        sql &= " ISNULL((SELECT SUM(SoLuong) FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE XUATKHO.IDVatTu=tb2.IDVatTu),0)SLX,"
        sql &= " Round((TongDonGiaDauKy/SLDauKy),2)DonGiaTBDauKy"
        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT [@tbIDVatTuTrongKy].IDVatTu,Sum(SoLuong) as SLDauKy,Sum(DonGia * SoLuong*TyGia)as TongDonGiaDauKy "
        sql &= " FROM @tbIDVatTuTrongKy "
        sql &= " LEFT JOIN (SELECT IDVatTu,SoLuong,(DonGia + isnull(ChiPhi,0))DonGia,PHIEUNHAPKHO.TyGia FROM NHAPKHO"
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
        sql &= " WHERE Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " )tbll ON tbll.IDVatTu=[@tbIDVatTuTrongKy].IDVatTu"
        sql &= " GROUP BY [@tbIDVatTuTrongKy].IDVatTu"
        sql &= " )tb2"

        sql &= " UPDATE @tbMain "
        sql &= " SET [@tbMain].SLTrongKy = [@tbTonTrongKy].SL, "
        sql &= " [@tbMain].SLNhapTrongKy = [@tbTonTrongKy].SLN, "
        sql &= " [@tbMain].SLXuatTrongKy = [@tbTonTrongKy].SLX, "
        sql &= " [@tbMain].DonGiaTrongKy = (CASE WHEN ISNULL([@tbTonTrongKy].DonGia,0) = 0 THEN [@tbMain].DonGiaDauKy ELSE [@tbTonTrongKy].DonGia END) "
        sql &= " FROM @tbMain, @tbTonTrongKy "
        sql &= " WHERE [@tbMain].IDVatTu = [@tbTonTrongKy].IDVatTu"

        sql &= " INSERT INTO @tbMain(IDVatTu,SLDauKy,DonGiaDauKy,SLTrongKy,SLNhapTrongKy,SLXuatTrongKy,DonGiaTrongKy)"
        sql &= " SELECT [@tbTonTrongKy].IDVatTu,0,0,[@tbTonTrongKy].SL,[@tbTonTrongKy].SLN,[@tbTonTrongKy].SLX,[@tbTonTrongKy].DonGia"
        sql &= " FROM @tbTonTrongKy"
        sql &= " WHERE [@tbTonTrongKy].IDVatTu Not IN (SELECT [@tbMain].IDVatTu FROM @tbMain)"

        sql &= " SELECT * FROM "
        sql &= " (SELECT Right(Convert(nvarchar,@TuNgay,103),7) as ThoiGian, Round((ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0)),3)SoLuong,"
        sql &= " (CASE ((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 then 0 else ISNULL([@tbMain].SLNhapTrongKy,0) END) ) WHEN 0 THEN [@tbMain].DonGiaDauKy ELSE"
        sql &= " Round((((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) * ISNULL([@tbMain].DonGiaDauKy,0)) + ((CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) < 0 then 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) *ISNULL([@tbMain].DonGiaTrongKy,0)))/((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) ),2) END) DonGia"
        sql &= " FROM @tbMain"
        sql &= " UNION ALL "

        sql &= " SELECT ThoiGian,SoLuong,DonGia FROM tblTonDauKy WHERE IDVatTu=@IDVT)tb "
        sql &= " ORDER BY Convert(datetime,Convert(nvarchar,'01/' + ThoiGian,103),103) DESC"
        AddParameterWhere("@IDVT", _IDVatTu)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Return tb
        Else
            Return Nothing
        End If
    End Function
    Private Sub btChuyenMa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChuyenMa.ItemClick
        gdvChuyenMaCT.CloseEditor()
        gdvChuyenMaCT.UpdateCurrentRow()
        Dim tg As DateTime = GetServerTime()
        For i As Integer = 0 To gdvChuyenMaCT.RowCount - 1
            If gdvChuyenMaCT.GetRowCellValue(i, "SLYC") > 0 Then
                Dim _SL As Double = gdvChuyenMaCT.GetRowCellValue(i, "SLYC")
                Dim tb As DataTable
                If gdvChuyenMaCT.GetRowCellValue(i, "Bo") Then
                    tb = SqlSelect.Select_BoVatTu(gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                Else
                    tb = Nothing
                End If

                If Not tb Is Nothing Then
                    ShowCanhBao(gdvChuyenMaCT.GetRowCellValue(i, "Model") & " là một bộ vật tư, vật tư này sẽ được chuyển thành các vật tư chi tiết.")
                    If Convert.ToBoolean(chkThayThe.EditValue) Then
                        If Not ShowCauHoi("Thay thế mã: " & gv.GetFocusedRowCellValue("Model") & " bằng bộ vật tư: " & gdvChuyenMaCT.GetRowCellValue(i, "Model")) Then Exit Sub
                    Else
                        For j As Integer = 0 To tb.Rows.Count - 1
                            gv.AddNewRow()
                            gv.SetFocusedRowCellValue("SoPhieu", tbSoPhieu.EditValue)
                            gv.SetFocusedRowCellValue("IDVatTu", tb.Rows(j)("ID"))
                            gv.SetFocusedRowCellValue("TenVT", tb.Rows(j)("TenVT"))
                            gv.SetFocusedRowCellValue("ThongSo", tb.Rows(j)("Thongso"))
                            gv.SetFocusedRowCellValue("Model", tb.Rows(j)("Model"))
                            gv.SetFocusedRowCellValue("SoLuong", tb.Rows(j)("SLTrongBo") * _SL)
                            gv.SetFocusedRowCellValue("TenDVT", tb.Rows(j)("DVT"))
                            gv.SetFocusedRowCellValue("HangSX", tb.Rows(j)("HangSX"))
                            gv.SetFocusedRowCellValue("DonGia", LayGiaNhapTB(tb.Rows(j)("ID")).Rows(1)("DonGia"))
                            gv.SetFocusedRowCellValue("ThanhTien", LayGiaNhapTB(tb.Rows(j)("ID")).Rows(1)("DonGia") * tb.Rows(j)("SLTrongBo") * _SL)
                            gv.SetFocusedRowCellValue("NgayChuyenMa", tg)
                            gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
                        Next
                    End If

                Else
                    If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                        gv.AddNewRow()
                    Else
                        If Not ShowCauHoi("Thay thế mã: " & gv.GetFocusedRowCellValue("Model") & " bằng: " & gdvChuyenMaCT.GetRowCellValue(i, "Model")) Then Exit Sub
                    End If



                    gv.SetFocusedRowCellValue("SoPhieu", tbSoPhieu.EditValue)
                    gv.SetFocusedRowCellValue("IdVatTu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                    gv.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetRowCellValue(i, "TenVT"))
                    gv.SetFocusedRowCellValue("ThongSo", gdvChuyenMaCT.GetRowCellValue(i, "Thongso"))
                    gv.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetRowCellValue(i, "Model"))
                    gv.SetFocusedRowCellValue("SoLuong", gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                    gv.SetFocusedRowCellValue("TenDVT", gdvChuyenMaCT.GetRowCellValue(i, "DVT"))
                    gv.SetFocusedRowCellValue("HangSX", gdvChuyenMaCT.GetRowCellValue(i, "HangSX"))
                    gv.SetFocusedRowCellValue("DonGia", LayGiaNhapTB(gdvChuyenMaCT.GetRowCellValue(i, "ID")).Rows(1)("DonGia"))
                    gv.SetFocusedRowCellValue("ThanhTien", LayGiaNhapTB(gdvChuyenMaCT.GetRowCellValue(i, "ID")).Rows(1)("DonGia") * gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                    gv.SetFocusedRowCellValue("NgayChuyenMa", tg)
                    gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
                End If



                ' gdvChuyenMaCT.GetFocusedRowCellValue("HangTon")
                gv.CloseEditor()
                gv.UpdateCurrentRow()
            End If


        Next

        ShowAlert("Đã chuyển mã")
    End Sub
    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then
            'SendKeys.Send("{F4}")
            pNhapSoLuong.Visible = True
            pNhapSoLuong.Focus()

            If gv.FocusedRowHandle >= 0 Then
                tbSL.EditValue = Convert.ToDouble(gv.GetFocusedRowCellValue("Soluong"))
            Else
                tbSL.EditValue = 1.0
            End If
            tbSL.Focus()
        ElseIf e.Control AndAlso (e.KeyCode = Keys.N Or e.KeyCode = Keys.G) Then

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
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = Not gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
            chkHienDongLocDuLieu.Checked = gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub
    Private Sub tbSL_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbSL.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            gdvChuyenMaCT.SetFocusedRowCellValue("SLYC", Convert.ToDouble(tbSL.EditValue))
            btChuyenMa.PerformClick()
            If gv.FocusedRowHandle < 0 Then

                If gv.FocusedRowHandle < gv.RowCount - 1 Then
                    gv.FocusedRowHandle += 1
                Else
                    gv.FocusedRowHandle = -1
                End If
            End If
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        ElseIf e.KeyChar = Convert.ToChar(Keys.Escape) Then
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        End If
    End Sub
    Private Sub gdvMaKH_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        On Error Resume Next
        If gdvMaKH.IsPopupOpen Then Exit Sub
        loadNguoiGD()
        '    cbNguoiGD.Focus()

    End Sub
    
    Private Sub mnu_TinhTrangVT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_TinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
        f._HienThongTinNX = True
        f._HienNCC = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienCGXK = False
            Else
                f._HienCGXK = True
            End If
        Else
            f._HienCGXK = True
        End If
        f.ShowDialog()
    End Sub

    Private Sub rcbNhomVT_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
        End If
    End Sub
    Private Sub rcbTenVatTu_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTenVatTu.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTenVT.EditValue = Nothing
        End If
    End Sub
    Private Sub rcbHangSX_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            btFilterHangSX.EditValue = Nothing
        End If
    End Sub
    Private Sub rtbMaVT_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterMaVT.EditValue = Nothing
        End If
    End Sub
    Private Sub rtbThongSo_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThongSo.ButtonClick
        If e.Button.Index = 1 Then
            btFilterThongSo.EditValue = Nothing
        End If
    End Sub
    Private Sub rbtFilterMaKH_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rbtFilterMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btFilterMaKH.EditValue = Nothing
        End If
    End Sub
    Private Sub gv_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gv.ShowingEditor
        If Not IsDBNull(gv.GetFocusedRowCellValue("isDuyet")) Then
            If gv.GetFocusedRowCellValue("isDuyet") <> 0 Then
                e.Cancel = True
            End If
        End If
     
    End Sub
    Private Sub gv_MouseDown(sender As Object, e As MouseEventArgs) Handles gv.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gv.CalcHitInfo(gc.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gc.PointToScreen(e.Location))
        Else

        End If
    End Sub
    Private Sub gdvChuyenMaCT_MouseDown(sender As Object, e As MouseEventArgs) Handles gdvChuyenMaCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvChuyenMaCT.CalcHitInfo(gdvChuyenMa.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdvChuyenMa.PointToScreen(e.Location))
        Else

        End If
    End Sub

    Private Sub mnu_Xoa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Xoa.ItemClick
        btnXoa.PerformClick()
    End Sub

    Private Sub btnXoa_Click(sender As Object, e As EventArgs) Handles btnXoa.Click
        If ShowCauHoi("Bạn có muốn xóa không ?") Then
            If Not IsDBNull(gv.GetFocusedRowCellValue("isDuyet")) Then
                If gv.GetFocusedRowCellValue("isDuyet") <> 1 Then
                    AddParameterWhere("@Id", gv.GetFocusedRowCellValue("Id"))
                    AddParameter("@isXoa", 1)
                    If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ShowAlert("Đã xóa")
                        gv.DeleteSelectedRows()
                    End If
                    'If doDelete("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                    '    ShowBaoLoi(LoiNgoaiLe)
                    'Else
                    '    ShowAlert("Đã xóa")
                    '    gv.DeleteSelectedRows()
                    'End If
                Else
                    ShowAlert("Đã xác nhận không xóa được ")
                End If
            Else
                gv.DeleteSelectedRows()
            End If
           

        End If
     
    End Sub
    Private Sub gv_RowStyle(sender As Object, e As Views.Grid.RowStyleEventArgs) 'Handles gv.RowStyle
        If gv.GetRowCellValue(e.RowHandle, "isDuyet") = 1 Then
            e.Appearance.BackColor = Color.FromArgb(255, 160, 122)
        End If
    End Sub

    Private Sub frmCNDatTon_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        AddParameter("@isXoa", 0)
        AddParameterWhere("isXoa2", 1)
        If doUpdate("DAT_TON_CHI_TIET", "isXoa=@isXoa2") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
End Class