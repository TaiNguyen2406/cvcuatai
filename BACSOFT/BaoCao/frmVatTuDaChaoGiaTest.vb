Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.Data

Public Class frmVatTuDaChaoGiaTest
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmBCChaoGia_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btKetXuat.Visibility = XtraBars.BarItemVisibility.Never
            colLoaiDN.Visible = False
            gdvCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            colGiaGoc.Visible = False
        Else
            colLoaiDN.Visible = True
            gdvCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            colGiaGoc.Visible = True
        End If

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '    btfilterTakecare.Enabled = False
        'End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            btfilterTakecare.Enabled = False
        End If
        btfilterTakecare.EditValue = Convert.ToInt32(TaiKhoan)
    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim str As String = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        str &= " SELECT KHACHHANG.ID,ttcMa,NHANSU.Ten as PhuTrach FROM KHACHHANG LEFT JOIN NHANSU ON KHACHHANG.IDTakeCare=NHANSU.ID "

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            str &= " WHERE IDTakecare is null or IDTakeCare = @TK"
            AddParameterWhere("@TK", TaiKhoan)
        End If
        str &= " ORDER BY ttcMa "

        Dim ds As DataSet = ExecuteSQLDataSet(str)
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
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
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT where 1=1"
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
        'Tai
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            If btfilterTakecare.EditValue <> TaiKhoan Then
                sql &= " and ID in (select IDHangSX from PhatTrienSanPham where Thang=CONVERT(char(2),getdate(), 101)+'/'+convert(nvarchar(5),datepart(year,getdate())) and IDPhuTrach =" & TaiKhoan & ")"
            End If

        End If
        'Tai
        sql &= " ORDER BY Ten"
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

    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbTakecare_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTakecare.EditValue = Nothing
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

    Private Sub LoadDaChaoGia()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT *,        "
        sql &= " (DonGia-GiaNhapTB - ChietKhau/(1-(KhauTru/100)))*SoLuong as LoiNhuan,"
        sql &= " Round(((DonGia-GiaNhapTB - ChietKhau/(1-(KhauTru/100)))/(CASE WHEN GiaNhapTB = 0 THEN 1 ELSE GiaNhapTB END))*100,2)PTLoiNhuan"
        sql &= " FROM ("
        sql &= " SELECT KHACHHANG.ttcMa,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.SoPhieu AS SoPhieu2,BANGCHAOGIA.NgayThang,0 AS STT,BANGCHAOGIA.CongTrinh,BANGCHAOGIA.TrangThai AS TrangThaiChinh,BANGCHAOGIA.IDTakeCare, "
        sql &= "        TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,TENDONVITINH.Ten AS DVT,CHAOGIA.SoLuong,(CHAOGIA.DonGia*BANGCHAOGIA.TyGia) AS DonGia,"
        sql &= " 		(CHAOGIA.DonGia*CHAOGIA.SoLuong*BANGCHAOGIA.TyGia)AS ThanhTien,(CASE CHAOGIA.XuatThue WHEN 1 THEN BANGCHAOGIA.TyGia*CHAOGIA.DonGia*CHAOGIA.SoLuong*CHAOGIA.MucThue/100 ELSE 0 END)TienThue,"
        sql &= " 		tblTienTe.Ten AS TienTe, BANGCHAOGIA.TyGia, CHAOGIA.MucThue,CHAOGIA.XuatThue,CHAOGIA.IDVatTu,KHACHHANG.IDLinhVucSX, "
        sql &= " 		CHAOGIA.ID AS IDCHAOGIA,NHANSU.Ten AS TakeCare,tblTuDien.NoiDung AS TrangThai, (CASE WHEN CHAOGIA.TrangThai >2 then BANGCHAOGIA.NgayHuy WHEN CHAOGIA.TrangThai=2 THEN NgayNhan ELSE NULL END)NgayNhanHuy,"
        sql &= "        ISNULL((SELECT DonGia FROM tblTonDauKy WHERE IDVatTu=CHAOGIA.IDVatTu AND tblTonDauKy.ThoiGian = Right(Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),7)),"
        'sql &= "        ISNULL(ISNULL("
        'sql &= "        (SELECT     TOP (1) Gianhap"
        'sql &= "            FROM V_GiaNhap "
        'sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        'sql &= "            ORDER BY Ngaythang DESC),"
        'sql &= "        (SELECT     TOP (1) Gianhap"
        'sql &= "            FROM V_GiaNhap"
        'sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        'sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia)) As GiaNhapTB, "
        sql &= "        ISNULL(GIANHAPTB.DonGia,ISNULL(GIANHAPTBTT.DonGia,"
        sql &= "        ISNULL(ISNULL("
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia))  "
        sql &= "        )) as GiaNhapTB,"
        sql &= " (CASE VATTU.XuatThue1 "
        sql &= "	WHEN 0 THEN "
        sql &= "		(CASE VATTU.DonGia1 "
        sql &= "			WHEN 0 THEN 0 "
        sql &= "				ELSE"
        sql &= "        ROUND(((CHAOGIA.DonGia - CHAOGIA.ChietKhau) / (VATTU.DonGia1 * TTVT.TyGia)) * 100, 2)"
        sql &= "			END) "
        sql &= "	ELSE"
        sql &= "		(CASE VATTU.DonGia1 "
        sql &= "			WHEN 0 THEN 0 "
        sql &= "				ELSE"
        sql &= "        ROUND(((CHAOGIA.DonGia - CHAOGIA.ChietKhau) / ((VATTU.DonGia1 * TTVT.TyGia) / (100 + VATTU.MucThue1))) * 100, 2)"
        sql &= "			END) "
        sql &= "	END) AS PTGiaBan,"
        sql &= "        (CASE KHACHHANG.NhomKH WHEN 1 THEN N'Thương mại, Chế tạo máy, Tích hợp …' WHEN 2 THEN  N'END User' ELSE '' END)NhomKH,"
        sql &= "        (CASE WHEN KHACHHANG.CapKH IS null THEN Convert(nvarchar,CapKH) ELSE N'Cấp ' +  Convert(nvarchar, KHACHHANG.CapKH) END)CapKH,"
        sql &= "    CHAOGIA.ChietKhau,BANGCHAOGIA.KhauTru,KHACHHANG.IDTakeCare as IDPhuTrachKH,PTKH.Ten as PhuTrachKH"
        sql &= "  , (select NoiDung from tblTuDien where ID=IDKhuCN) KhuCN,(select NoiDung from tblTuDien where ID=IDTinhThanh ) TinhThanh,(select NoiDung from tblTuDien where ID=IDChuSoHuu ) ChuSoHuu, (select NoiDung from tblTuDien where ID=IDLoaiHinhDN ) LoaiHinhDN"
        sql &= " FROM CHAOGIA "
        sql &= " 	INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = CHAOGIA.Sophieu "


        If cbTrangThai.EditValue = "Tất cả" Then
            sql &= " AND Convert(datetime,CONVERT(Nvarchar,BANGCHAOGIA.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay  "
        ElseIf cbTrangThai.EditValue = "Đã xác nhận" Then
            sql &= " AND CHAOGIA.TrangThai =2  AND Convert(datetime,CONVERT(Nvarchar,BANGCHAOGIA.NgayNhan,103),103) BETWEEN @TuNgay AND @DenNgay  "
        Else
            sql &= " AND CHAOGIA.TrangThai >2 AND Convert(datetime,CONVERT(Nvarchar,BANGCHAOGIA.NgayHuy,103),103) BETWEEN @TuNgay AND @DenNgay  "
        End If
        sql &= "    INNER JOIN tblTuDien ON CHAOGIA.TrangThai=tblTuDien.Ma AND Loai=2 "
        sql &= "    LEFT JOIN tblTonDauKy AS GIANHAPTB ON CHAOGIA.IDvattu = GIANHAPTB.IDVatTu AND RIGHT(CONVERT(nvarchar, BANGCHAOGIA.Ngaythang, 103), 7) = GIANHAPTB.ThoiGian "
        sql &= "    LEFT JOIN tblTonDauKy AS GIANHAPTBTT ON CHAOGIA.IDvattu = GIANHAPTBTT.IDVatTu AND RIGHT(CONVERT(nvarchar,Dateadd(month,-1, BANGCHAOGIA.Ngaythang), 103), 7) = GIANHAPTBTT.ThoiGian "
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = CHAOGIA.IDVatTu"
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        Else
            'Tai
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
                If btfilterTakecare.EditValue <> TaiKhoan Then
                    sql &= " and VATTU.IDHangSanxuat in (select IDHangSX from PhatTrienSanPham where Thang=CONVERT(char(2),getdate(), 101)+'/'+convert(nvarchar(5),datepart(year,getdate())) and IDPhuTrach =" & TaiKhoan & ")"
                End If

            End If
            'Tai
        End If
        sql &= " 	LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " 	LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " 	LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh=TENDONVITINH.ID"
        sql &= " 	INNER JOIN KHACHHANG ON BANGCHAOGIA.IDkhachhang = KHACHHANG.ID"

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            If chkBlank.Checked Then
                sql &= " AND (( BANGCHAOGIA.IDTakeCare=" & btfilterTakecare.EditValue & " OR  KHACHHANG.IDTakeCare is null ) OR KHACHHANG.IDTakeCare=" & btfilterTakecare.EditValue & " ) "
            Else
                sql &= " AND ( BANGCHAOGIA.IDTakeCare=" & btfilterTakecare.EditValue & " OR KHACHHANG.IDTakeCare=" & btfilterTakecare.EditValue & " ) "
            End If


        End If

        sql &= " 	INNER JOIN NHANSU ON BANGCHAOGIA.IDTakeCare = NHANSU.ID"
        sql &= " 	LEFT JOIN NHANSU as PTKH ON KHACHHANG.IDTakeCare = PTKH.ID"
        sql &= " 	LEFT JOIN tblTienTe ON BANGCHAOGIA.TienTe=tblTienTe.ID "
        sql &= " 	INNER JOIN tblTienTe AS TTVT ON VATTU.TienTe1=TTVT.ID "
        sql &= " )tbll"
        sql &= " ORDER BY SoPhieu "

        AddParameterWhere("@LinhVuc", LoaiTuDien.LinhVucSX)
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LinhVuc "

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If Not ds Is Nothing Then
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim _STT As Integer = 1
                    .Rows(0)("STT") = _STT
                    If .Rows(0)("IDLinhVucSX").ToString.Trim <> "" Then
                        Dim tb As DataTable = DataSourceDSFile(.Rows(0)("IDLinhVucSX").ToString, "IDLinhVucSX")
                        .Rows(0)("IDLinhVucSX") = ""
                        For j As Integer = 0 To tb.Rows.Count - 1
                            Dim rows() As DataRow = ds.Tables(1).Select("ID = " & tb.Rows(j)("IDLinhVucSX").ToString & "")
                            If Not rows Is Nothing And rows.Length > 0 Then
                                .Rows(0)("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                            End If
                        Next
                        .Rows(0)("IDLinhVucSX") = .Rows(0)("IDLinhVucSX").ToString.Trim
                    End If

                    For i As Integer = 1 To .Rows.Count - 1
                        If .Rows(i)("SoPhieu") <> .Rows(i - 1)("SoPhieu2") Then
                            _STT = 1
                        Else
                            _STT += 1
                        End If
                        .Rows(i)("STT") = _STT

                        If _STT <> 1 And chkRutGon.Checked Then
                            .Rows(i)("ttcMa") = DBNull.Value
                            .Rows(i)("SoPhieu") = DBNull.Value
                            .Rows(i)("TakeCare") = DBNull.Value
                            .Rows(i)("NgayThang") = DBNull.Value
                        End If
                        If .Rows(i)("IDLinhVucSX").ToString.Trim <> "" Then
                            Dim tb As DataTable = DataSourceDSFile(.Rows(i)("IDLinhVucSX").ToString, "IDLinhVucSX")
                            .Rows(i)("IDLinhVucSX") = ""
                            For j As Integer = 0 To tb.Rows.Count - 1
                                Dim rows() As DataRow = ds.Tables(1).Select("ID = " & tb.Rows(j)("IDLinhVucSX").ToString)
                                If Not rows Is Nothing And rows.Length > 0 Then
                                    .Rows(i)("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                                End If
                            Next
                            .Rows(i)("IDLinhVucSX") = .Rows(i)("IDLinhVucSX").ToString.Trim
                        End If
                    Next
                End If
            End With

            If chkRutGon.Checked Then
                colMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

                colSoPhieu.OptionsFilter.AllowAutoFilter = False
                colSoPhieu.OptionsFilter.AllowFilter = False
                colMaKH.OptionsFilter.AllowAutoFilter = False
                colMaKH.OptionsFilter.AllowFilter = False
            Else
                colMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsFilter.AllowAutoFilter = True
                colSoPhieu.OptionsFilter.AllowFilter = True
                colMaKH.OptionsFilter.AllowAutoFilter = True
                colMaKH.OptionsFilter.AllowFilter = True
            End If

            gdv.DataSource = ds.Tables(0)
            Dim summaryItemMaxOrderSum As GridSummaryItem = gdvCT.GroupSummary.Add(SummaryItemType.Sum, "LoiNhuan", Nothing, String.Empty)
            Dim firstcol As GridColumn = gdvCT.SortInfo(0).Column
            gdvCT.GroupSummarySortInfo.Add(summaryItemMaxOrderSum, ColumnSortOrder.Descending, firstcol)

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If chkRutGon.Checked Then
            gdvCT.ClearGrouping()
            gdvCT.ClearSorting()
        Else
            colSoPhieu.GroupIndex = 0
        End If
        LoadDaChaoGia()

    End Sub

    Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
        If chkRutGon.Checked = True Then
            chkRutGon.Glyph = My.Resources.Checked
        Else
            chkRutGon.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNChaoGia.Tag = Me.Parent.Tag
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            fCNChaoGia.gdvVTCT.OptionsBehavior.ReadOnly = True
            fCNChaoGia.gdvCongTrinhCT.OptionsBehavior.ReadOnly = True
            fCNChaoGia.btGhi.Enabled = False
            fCNChaoGia.tabChuyenMa.PageVisible = False
            fCNChaoGia.btTinhToan.Enabled = False
        End If

        fCNChaoGia.Show()
    End Sub

    Private Sub mXemXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemXuatKho.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        TrangThai.isUpdate = True
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.PhieuXK = gdvCT.GetFocusedRowCellValue("SoPhieu2")
        fCNXuatKho.Tag = Me.Parent.Tag
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            fCNXuatKho.btCal.Enabled = False
            fCNXuatKho.btGhi.Enabled = False
            fCNXuatKho.btChuyenXK.Enabled = False
            fCNXuatKho.mXuatKho.Enabled = False
        End If
        fCNXuatKho.ShowDialog()
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT DA CG " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

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

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub


        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mXemCG_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemCG.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If (gdvCT.GetFocusedRowCellValue("TrangThaiChinh") = TrangThaiChaoGia.DaXacNhan Or gdvCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan) And (Not IsDBNull(gdvCT.GetFocusedRowCellValue("IDPhuTrachKH")) And gdvCT.GetFocusedRowCellValue("IDPhuTrachKH").ToString <> TaiKhoan) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isUpdate = True
        fCNChaoGia.Tag = deskTop.mChaoGia.Name
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNChaoGia.Show()

    End Sub

    Private Sub mThuGon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuGon.ItemClick
        colMaKH.Width = 90
        colCXTenVT.Width = 70
        colCXHangSX.Width = 50
        colMaHang.Width = 60
        colCXPhuTrach.Width = 60
        colGiaGoc.Width = 60
        colPTBL.Width = 30
        colDVT.VisibleIndex = 14
    End Sub

    Private Sub chkBlank_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkBlank.CheckedChanged
        If chkBlank.Checked Then
            chkBlank.Glyph = My.Resources.Checked
        Else
            chkBlank.Glyph = My.Resources.UnCheck
        End If
    End Sub
End Class
