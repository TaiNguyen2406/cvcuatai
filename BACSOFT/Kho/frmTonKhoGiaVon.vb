Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl

Public Class frmTonKhoGiaVon
    Public _Exit As Boolean = False

    Private Sub frmTonKho_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(Now.Year, Now.Month, 1)
        btfilterDenNgay.EditValue = Now.Date
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        tbNam.EditValue = Now.Year
        cbThang.EditValue = Now.ToString("MM")
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            barCapNhatTon.Visible = False
        End If
    End Sub

#Region "Lọc vật tư"

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

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
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


#End Region


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If chkXemTheoLanChot.Checked Then
            Dim sql As String = ""
            sql &= " IF EXISTS(SELECT TOP 1 IDVatTu FROM tblTonDauKy WHERE ThoiGian=N'" & DateAdd(DateInterval.Day, -1, Convert.ToDateTime(btfilterTuNgay.EditValue)).ToString("MM/yyyy") & "')"
            sql &= " BEGIN"
            sql &= "    Select 1"
            sql &= " End"
            sql &= " ELSE"
            sql &= " BEGIN"
            sql &= "    SELECT 0"
            sql &= " End"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                If tb.Rows(0)(0) = 1 Then
                    LoadDSTonKhoTheoThoiGianChot()
                Else
                    ShowCanhBao("Chưa có thông tin chốt tồn đầu kì cho khoảng thời gian được chọn !")
                End If

            End If

        Else
            LoadDS()
        End If

    End Sub

    Private Sub LoadDSTonKhoTheoThoiGianChot()
        Dim sql As String = ""
        ShowWaiting("Đang tải dữ liệu ...")
        sql &= " SET DATEFORMAT DMY"
        sql &= " DECLARE @TuNgay datetime"
        sql &= " DECLARE @DenNgay datetime"
        sql &= " DECLARE @DauKy	nvarchar(7)"

        sql &= " SET @TuNgay='" & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd/MM/yyyy") & "'"
        sql &= " SET @DenNgay='" & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd/MM/yyyy") & "'"
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
        sql &= " SELECT DISTINCT IDVatTu FROM"
        sql &= " ("
        sql &= " SELECT IDVatTu FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " UNION ALL "
        sql &= " SELECT IDVatTu FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " )tbl2"

        sql &= " INSERT INTO @tbMain(IDVatTu,SLDauKy,DonGiaDauKy)"
        sql &= " SELECT IDVatTu,SoLuong,DonGia"
        sql &= " FROM tblTonDauKy WHERE ThoiGian=@DauKy"

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

        sql &= " SELECT Row_number() Over (Order by TENVATTU.Ten,TENHANGSANXUAT.Ten,VATTU.Model) STT,"
        sql &= " TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= " Round([@tbMain].SLDauKy,3)SLDauKy,[@tbMain].DonGiaDauKy,((CASE WHEN [@tbMain].SLDauKy <0 then 0 ELSE [@tbMain].SLDauKy END) *[@tbMain].DonGiaDauKy)ThanhTienDauKy,"
        sql &= " Round([@tbMain].SLTrongKy,3)SLTrongKy,[@tbMain].SLNhapTrongKy,[@tbMain].SLXuatTrongKy,[@tbMain].DonGiaTrongKy,((CASE WHEN [@tbMain].SLTrongKy <0 THEN 0 ELSE [@tbMain].SLTrongKy END) *[@tbMain].DonGiaTrongKy)ThanhTienTrongKy,"
        sql &= " Round((ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0)),3)SLCuoiKy,"
        sql &= " (CASE ((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 then 0 else ISNULL([@tbMain].SLNhapTrongKy,0) END) ) WHEN 0 THEN [@tbMain].DonGiaDauKy ELSE"
        sql &= " Round((((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) * ISNULL([@tbMain].DonGiaDauKy,0)) + ((CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) < 0 then 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) *ISNULL([@tbMain].DonGiaTrongKy,0)))/((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) ),2) END) DonGiaCuoiKy,"
        sql &= " (CASE WHEN (ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0)) < 0 then 0 ELSE "
        sql &= " Round(((ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0))*"
        sql &= " (CASE ((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 then 0 else ISNULL([@tbMain].SLNhapTrongKy,0) END) ) WHEN 0 THEN [@tbMain].DonGiaDauKy ELSE"
        sql &= " (((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) * ISNULL([@tbMain].DonGiaDauKy,0)) + ((CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) < 0 then 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) *ISNULL([@tbMain].DonGiaTrongKy,0)))/((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) ) END)),2) END) ThanhTienCuoiKy,"
        sql &= " [@tbMain].IDVatTu"
        sql &= " FROM @tbMain"
        sql &= " INNER JOIN VATTU ON [@tbMain].IDVattu = VATTU.ID"
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh=TENDONVITINH.ID"
        sql &= " ORDER BY TENVATTU.Ten,TENHANGSANXUAT.Ten,VATTU.Model"
        'AddParameterWhere("@TuNgay", btfilterTuNgay.EditValue)
        'AddParameterWhere("@DenNgay", btfilterDenNgay.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        CloseWaiting()
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub LoadDS()
        Dim sql As String = ""
        ShowWaiting("Đang tải dữ liệu ...")
        sql &= " SET DATEFORMAT DMY"
        sql &= " DECLARE @TuNgay datetime"
        sql &= " DECLARE @DenNgay datetime"
        sql &= " DECLARE @NgayDauKy	Datetime"

        sql &= " SET @TuNgay='" & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd/MM/yyyy") & "'"
        sql &= " SET @DenNgay='" & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd/MM/yyyy") & "'"
        sql &= " SET @NgayDauKy = Dateadd(day,-1,@TuNgay)"

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

        sql &= " DECLARE @tbIDVatTuDauKy table"
        sql &= " ("
        sql &= "     IDVatTu Int"
        sql &= " )"

        sql &= " DECLARE @tbIDVatTuTrongKy table"
        sql &= " ("
        sql &= "     IDVatTu Int"
        sql &= " )"

        sql &= " INSERT INTO @tbIDVatTuDauKy(IDVatTu)"
        sql &= " SELECT DISTINCT IDVatTu FROM"
        sql &= " ("
        sql &= " SELECT IDVatTu FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103)<@TuNgay "
        sql &= " UNION ALL "
        sql &= " SELECT IDVatTu FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103)<@TuNgay"
        sql &= " )tbl"

        sql &= " INSERT INTO @tbIDVatTuTrongKy(IDVatTu)"
        sql &= " SELECT DISTINCT IDVatTu FROM"
        sql &= " ("
        sql &= " SELECT IDVatTu FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " UNION ALL "
        sql &= " SELECT IDVatTu FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103)BETWEEN @TuNgay AND @DenNgay"
        sql &= " )tbl2"

        sql &= "  INSERT INTO @tbMain(IDVatTu,SLDauKy,DonGiaDauKy)"
        sql &= "  SELECT IDVatTu,"
        sql &= "  (ISNULL((SELECT SUM(SoLuong) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103)<@TuNgay WHERE NHAPKHO.IDVatTu=tb.IDVatTu ),0)"
        sql &= "  -"
        sql &= "  ISNULL((SELECT SUM(SoLuong) FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103)<@TuNgay WHERE XUATKHO.IDVatTu=tb.IDVatTu),0))SLTon,"
        sql &= "  ISNULL(Round((TongDonGiaDauKy/SLDauKy),2),0)DonGiaTBDauKy"
        sql &= "  FROM"
        sql &= "  ("
        sql &= "  SELECT [@tbIDVatTuDauKy].IDVatTu,Sum(SoLuong) as SLDauKy,Sum(DonGia *SoLuong * TyGia)as TongDonGiaDauKy "
        sql &= " FROM @tbIDVatTuDauKy "
        sql &= "  LEFT JOIN "
        sql &= " (SELECT IDVatTu,SoLuong,(DonGia+ISNULL(ChiPhi,0))DonGia,PHIEUNHAPKHO.TyGia FROM NHAPKHO "
        sql &= "  INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) <@TuNgay"
        sql &= " )tbll ON tbll.IDVatTu=[@tbIDVatTuDauKy].IDVatTu"
        sql &= "  GROUP BY [@tbIDVatTuDauKy].IDVatTu"
        sql &= "  )tb"

        sql &= "  INSERT INTO @tbTonTrongKy(IDVatTu,SL,SLN,SLX,DonGia)"
        sql &= "  SELECT IDVatTu,"
        sql &= "  (ISNULL((SELECT SUM(SoLuong) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE NHAPKHO.IDVatTu=tb2.IDVatTu ),0)"
        sql &= "  -"
        sql &= "  ISNULL((SELECT SUM(SoLuong) FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE XUATKHO.IDVatTu=tb2.IDVatTu),0))SLTon,"
        sql &= "  ISNULL((SELECT SUM(SoLuong) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE NHAPKHO.IDVatTu=tb2.IDVatTu ),0)SLN,"
        sql &= "  ISNULL((SELECT SUM(SoLuong) FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay WHERE XUATKHO.IDVatTu=tb2.IDVatTu),0)SLX,"
        sql &= "  ISNULL(Round((TongDonGiaDauKy/SLDauKy),2),0)DonGiaTBDauKy"
        sql &= "  FROM"
        sql &= "  ("
        sql &= "  SELECT [@tbIDVatTuTrongKy].IDVatTu,Sum(SoLuong) as SLDauKy,Sum(DonGia * SoLuong*TyGia)as TongDonGiaDauKy "
        sql &= "  FROM  @tbIDVatTuTrongKy "
        sql &= " LEFT JOIN ("
        sql &= "  SELECT IDVatTu,SoLuong,(DonGia+ISNULL(ChiPhi,0))DonGia,PHIEUNHAPKHO.TyGia FROM NHAPKHO "
        sql &= "  INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        sql &= " )tbll2 ON [@tbIDVatTuTrongKy].IDVatTu=tbll2.IDVatTu"
        sql &= "  GROUP BY [@tbIDVatTuTrongKy].IDVatTu"
        sql &= "  )tb2"

        sql &= "  UPDATE @tbMain "
        sql &= "  SET [@tbMain].SLTrongKy = [@tbTonTrongKy].SL, "
        sql &= "  [@tbMain].SLNhapTrongKy = [@tbTonTrongKy].SLN, "
        sql &= "  [@tbMain].SLXuatTrongKy = [@tbTonTrongKy].SLX, "
        sql &= "  [@tbMain].DonGiaTrongKy = (CASE WHEN ISNULL([@tbTonTrongKy].DonGia,0) = 0 THEN [@tbMain].DonGiaDauKy ELSE [@tbTonTrongKy].DonGia END) "
        sql &= "  FROM @tbMain, @tbTonTrongKy "
        sql &= "  WHERE [@tbMain].IDVatTu = [@tbTonTrongKy].IDVatTu"

        sql &= "  INSERT INTO @tbMain(IDVatTu,SLDauKy,DonGiaDauKy,SLTrongKy,SLNhapTrongKy,SLXuatTrongKy,DonGiaTrongKy)"
        sql &= "  SELECT [@tbTonTrongKy].IDVatTu,0,0,[@tbTonTrongKy].SL,[@tbTonTrongKy].SLN,[@tbTonTrongKy].SLX,[@tbTonTrongKy].DonGia"
        sql &= "  FROM @tbTonTrongKy"
        sql &= "  WHERE [@tbTonTrongKy].IDVatTu Not IN (SELECT [@tbMain].IDVatTu FROM @tbMain)"

        sql &= " SELECT Row_number() Over (Order by TENVATTU.Ten,TENHANGSANXUAT.Ten,VATTU.Model) STT,"
        sql &= "  TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= " Round([@tbMain].SLDauKy,3)SLDauKy,[@tbMain].DonGiaDauKy,((CASE WHEN [@tbMain].SLDauKy<0 THEN 0 ELSE [@tbMain].SLDauKy END) *[@tbMain].DonGiaDauKy)ThanhTienDauKy,"
        sql &= " Round([@tbMain].SLTrongKy,3),[@tbMain].SLNhapTrongKy,[@tbMain].SLXuatTrongKy,[@tbMain].DonGiaTrongKy,((CASE WHEN [@tbMain].SLTrongKy<0 THEN 0 ELSE [@tbMain].SLTrongKy END)*[@tbMain].DonGiaTrongKy)ThanhTienTrongKy,"
        sql &= " Round((ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0)),3)SLCuoiKy,"
        sql &= " (CASE ((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 then 0 else ISNULL([@tbMain].SLNhapTrongKy,0) END) ) WHEN 0 THEN [@tbMain].DonGiaDauKy ELSE"
        sql &= " Round((((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) * ISNULL([@tbMain].DonGiaDauKy,0)) + ((CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) < 0 then 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) *ISNULL([@tbMain].DonGiaTrongKy,0)))/((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) ),2) END) DonGiaCuoiKy,"
        sql &= " (CASE WHEN (ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0)) < 0 THEN 0 ELSE "
        sql &= " Round(((ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0))*"
        sql &= " (CASE ((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 then 0 else ISNULL([@tbMain].SLNhapTrongKy,0) END) ) WHEN 0 THEN [@tbMain].DonGiaDauKy ELSE"
        sql &= " (((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) * ISNULL([@tbMain].DonGiaDauKy,0)) + ((CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) < 0 then 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) *ISNULL([@tbMain].DonGiaTrongKy,0)))/((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) ) END)),2) END) ThanhTienCuoiKy,"
        sql &= " [@tbMain].IDVatTu"
        sql &= "  FROM @tbMain"
        sql &= "  INNER JOIN VATTU ON [@tbMain].IDVattu = VATTU.ID"
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh=TENDONVITINH.ID"
        sql &= " ORDER BY TENVATTU.Ten,TENHANGSANXUAT.Ten,VATTU.Model"
        'AddParameterWhere("@TuNgay", btfilterTuNgay.EditValue)
        'AddParameterWhere("@DenNgay", btfilterDenNgay.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        CloseWaiting()
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT ton kho " & Today.Date.ToString("dd-MM-yyyy") & ".xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)
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


    Private Sub btXem_Click(sender As System.Object, e As System.EventArgs) Handles btXem.Click
        Dim sql As String = ""
        ShowWaiting("Đang tải dữ liệu tồn kho ...")
        sql &= " SELECT TENHANGSANXUAT.Ten AS TenHang,tb.* FROM "
        sql &= " (SELECT IDHangSanXuat, SUM(Ton) AS SoLuong, SUM(Ton * Gianhap) AS TongTien,SUM((CASE HangTon WHEN 1 THEN Ton*GiaNhap ELSE 0 END ))TienTonLau"
        sql &= " FROM  V_F_Tonkho"
        sql &= " GROUP BY IDHangSanxuat)tb"
        sql &= " INNER JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=tb.IDHangSanXuat"
        sql &= " ORDER BY TongTien DESC"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        CloseWaiting()
        If Not tb Is Nothing Then
            gdvTongHop.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT ton kho theo hang " & Today.Date.ToString("dd-MM-yyyy") & ".xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvTongHopCT, False)
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


    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        AddParameterWhere("@IDVatTu", gdvCT.GetFocusedRowCellValue("IDVatTu"))
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT KHACHHANG.ttcMa,PHIEUNHAPKHO.NgayThang,NHAPKHO.SoPhieu,VATTU.Model,SoLuong,"
        sql &= " 		(Dongia * PHIEUNHAPKHO.Tygia)DonGia,NHAPKHO.ChiPhi,(Dongia * PHIEUNHAPKHO.Tygia * SoLuong)ThanhTien,"
        sql &= " 		NGUOIDAT.Ten AS TakeCare,NGUOINHAP.Ten AS NguoiNhapKho"
        sql &= " FROM NHAPKHO "
        sql &= " 	INNER JOIN PHIEUNHAPKHO ON NHAPKHO.Sophieu=PHIEUNHAPKHO.Sophieu"
        sql &= " 	LEFT JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"
        sql &= "     INNER JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID=NHAPKHO.IDVatTu"
        sql &= " 	LEFT JOIN NHANSU AS NGUOIDAT ON NGUOIDAT.ID=PHIEUDATHANG.IDTakeCare"
        sql &= " 	LEFT JOIN NHANSU AS NGUOINHAP ON NGUOINHAP.ID=PHIEUNHAPKHO.IDUSer"
        sql &= " WHERE NHAPKHO.IDVattu=@IDVatTu"
        sql &= " ORDER BY PHIEUNHAPKHO.Ngaythang DESC"

        sql &= " SELECT KHACHHANG.ttcMa,PHIEUXUATKHO.NgayThang,PHIEUXUATKHO.CongTrinh,XUATKHO.SoPhieu,VATTU.Model,SoLuong,"
        sql &= " 		(Dongia * PHIEUXUATKHO.Tygia)DonGia,(Dongia * PHIEUXUATKHO.Tygia * SoLuong)ThanhTien,"
        sql &= " 		TAKECARE.Ten AS TakeCare,NGUOIXUAT.Ten AS NguoiXuatKho"
        sql &= " FROM XUATKHO "
        sql &= " 	INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu"
        sql &= "     INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu"
        sql &= " 	LEFT JOIN NHANSU AS TAKECARE ON TAKECARE.ID=PHIEUXUATKHO.IDTakeCare"
        sql &= " 	LEFT JOIN NHANSU AS NGUOIXUAT ON NGUOIXUAT.ID=PHIEUXUATKHO.IDUSer"
        sql &= " WHERE XUATKHO.IDVattu=@IDVatTu"
        sql &= " ORDER BY PHIEUXUATKHO.Ngaythang DESC"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmLichSuNhapXuat
            f.gdvNhap.DataSource = ds.Tables(0)
            f.gdvXuat.DataSource = ds.Tables(1)
            f.tbTonKho.EditValue = gdvCT.GetFocusedRowCellValue("Ton")
            f.tbTienTonKho.EditValue = gdvCT.GetFocusedRowCellValue("ThanhTien")
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

 
    Private Sub btCapNhatDauKy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatCuoiKyTheoNam.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Exit Sub
        End If
        If ShowCauHoi("Cập nhật thông tin tồn kho giá nhập của " & cbThang.EditValue & "/" & tbNam.EditValue.ToString & " ?") Then
            ShowWaiting("Đang cập nhật ...")
            Dim sql As String = ""
            sql &= " SET DATEFORMAT DMY"
            sql &= " DECLARE @TuNgay datetime"
            sql &= " DECLARE @DenNgay datetime"
            sql &= " DECLARE @DauKy	nvarchar(7)"
            sql &= " DECLARE @KyHienTai	nvarchar(7)"
            sql &= " SET @TuNgay='" & New DateTime(tbNam.EditValue, cbThang.EditValue, 1).ToString("dd/MM/yyyy") & "'"
            sql &= " SET @DenNgay='" & DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, New DateTime(tbNam.EditValue, cbThang.EditValue, 1))).ToString("dd/MM/yyyy") & "'"
            sql &= " SET @DauKy = right( Convert(nvarchar, Dateadd(day,-1,@TuNgay),103),7)"
            sql &= " SET @KyHienTai = right( Convert(nvarchar, @TuNgay,103),7)"
            If Not tbModel.EditValue Is Nothing Then
                sql &= " DECLARE @IDVatTu int"
                sql &= " SET @IDVatTu=" & tbModel.EditValue
            End If

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
            If tbModel.EditValue Is Nothing Then
                sql &= " SELECT DISTINCT IDVatTu FROM"
                sql &= " ("
                sql &= " SELECT IDVatTu FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
                sql &= " UNION ALL "
                sql &= " SELECT IDVatTu FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
                sql &= " )tbl2"
            Else
                sql &= " SELECT @IDVatTu"
            End If
           

            sql &= " INSERT INTO @tbMain(IDVatTu,SLDauKy,DonGiaDauKy)"
            sql &= " SELECT IDVatTu,SoLuong,DonGia"
            sql &= " FROM tblTonDauKy WHERE ThoiGian=@DauKy"
            If Not tbModel.EditValue Is Nothing Then
                sql &= " AND IDVatTu=@IDVatTu"
            End If

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

            sql &= " DELETE FROM tblTonDauKy WHERE ThoiGian=@KyHienTai "
            If Not tbModel.EditValue Is Nothing Then
                sql &= " AND IDVatTu=@IDVatTu"
            End If
            sql &= " INSERT INTO tblTonDauKy(IDVatTu,ThoiGian,SoLuong,DonGia)"
            sql &= " SELECT [@tbMain].IDVattu,@KyHienTai,"
            sql &= " Round((ISNULL([@tbMain].SLDauKy,0) + ISNULL([@tbMain].SLTrongKy,0)),3)SLCuoiKy,"
            sql &= " (CASE ((CASE WHEN ISNULL([@tbMain].SLDauKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0) <0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END)) WHEN 0 THEN ISNULL([@tbMain].DonGiaDauKy,0) ELSE"
            sql &= " Round((((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) * ISNULL([@tbMain].DonGiaDauKy,0)) + ((CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) *ISNULL([@tbMain].DonGiaTrongKy,0)))/((CASE WHEN ISNULL([@tbMain].SLDauKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLDauKy,0) END) + (CASE WHEN ISNULL([@tbMain].SLNhapTrongKy,0)<0 THEN 0 ELSE ISNULL([@tbMain].SLNhapTrongKy,0) END) ),2) END) DonGiaCuoiKy"
            sql &= " FROM @tbMain"

            If ExecuteSQLNonQuery(sql) Is Nothing Then
                CloseWaiting()

                ShowBaoLoi(LoiNgoaiLe)
            Else
                CloseWaiting()
                ShowAlert("Đã cập nhật !")
            End If
        End If
    End Sub

    Private Sub chkXemTheoLanChot_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkXemTheoLanChot.CheckedChanged
        If chkXemTheoLanChot.Checked Then
            chkXemTheoLanChot.Glyph = My.Resources.Checked
        Else
            chkXemTheoLanChot.Glyph = My.Resources.UnCheck
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

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(btfilterDenNgay.EditValue)
        btfilterTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub rtbModel_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbModel.ButtonClick
        tbModel.EditValue = Nothing
    End Sub


End Class
