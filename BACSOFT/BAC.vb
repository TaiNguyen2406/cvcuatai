Imports BACSOFT.Db.SqlHelper
Imports System.Xml
Imports System.Globalization
Imports BACSOFT.Utils

Module BAC
    '---------- Form --------------
    Public deskTop As fMain
    Public fCNYeuCau As frmThemYeuCau
    Public fCNChaoGia As frmCNChaoGia
    Public fTachCG As frmTachChaoGia
    Public fCNCongTrinh As frmCNCongTrinhCanXuLy
    Public fCNMonHoc As frmCNNoiDungDaoTao
    Public fCNLichDaoTao As frmCNLichDaoTao
    Public fCongViec As frmCongViec
    Public fNoiDungThiCong As frmNoiDungThiCong
    Public fCNKhachHang As frmCNKhachHang
    Public fCNXuatKhoCT As frmCNXuatKhoCT
    Public fCNXuatKho As frmCNXuatKho
    Public fCNNhapKho As frmCNNhapKho
    Public fCNChiTieu As frmCNChiTieu
    Public fCNYeuCauHoiGia As frmCNYeuCauHoiGia
    Public fCNDiemThiNangLuc As frmCNDiemThiNangLuc
    Public fCNNangLuc As frmCNNangLuc

    '---------- Biến sử dụng ------
    Public key As String = "p@ssw0rd"
    Public TaiKhoan As Object
    Public MaPhongBan As Object
    Public NguoiDung As String = ""
    Public MatKhau As String = ""
    Public MatKhauLT As String = ""
    Public TenMayChu As String = ""
    Public TenCSDL As String = ""
    Public EmailNguoiDung As String = ""
    Public MaTruyCap As String = ""


    Public IPServer As String = "192.168.1.109"
    Public ServerName As String = "\\192.168.1.109"
    Public RootUrl As String = ""
    Public RootUrlOld As String = ServerName & "\DATA$\BAC"
    Public UrlKinhDoanh As String = "KINH DOANH\"
    Public UrlDatHang As String = "DAT HANG\"
    Public UrlKyThuat As String = "KY THUAT\"
    Public UrlHoiHang As String = "HOI HANG\"
    Public UrlNhaCC As String = ServerName & "\DATA$\NHA CUNG CAP\"

    Public UrlAnhVatTu As String = ServerName & "\BAC VAT TU$\HINH ANH\"
    Public UrlTaiLieuVatTu As String = ServerName & "\BAC VAT TU$\TAI LIEU\"
    Public UrlAnhNhanSu As String = ServerName & "\BAC NHAN SU$\HINH ANH\"
    Public UrlTaiLieuNhanSu As String = ServerName & "\BAC NHAN SU$\File\"
    Public UrlDaoTao As String = ServerName & "\BAC DAO TAO$\"
    Public UrlKhachHang As String = ServerName & "\BAC KHACH HANG$\"
    Public UrlYeuCauNoiBo As String = ServerName & "\DATA$\YEU CAU NOI BO\"
    Public UrlThiNangLuc As String = ServerName & "\DATA$\THI NANG LUC\"
    Public UrlQuyTrinhKyThuat As String = ServerName & "\DATA$\THI NANG LUC\"
    Public DbChamCong As New DbLibAccess.Database.AccessDBProvider("D:\standard\Att2003.mdb", "", "", "")


    Public Kho As Object
    Public TrangThai As New Utils.TrangThai
    Public TrangThaiDH As New Utils.TrangThai
    Public MaTuDien As Object
    Public objID As Object
    Public isShowing As Boolean = False
    Public isShowingHoiGia As Boolean = False
    Public wDlg As DevExpress.Utils.WaitDialogForm
    Public alert As DevExpress.XtraBars.Alerter.AlertControl
    Public QuyenSD As DataTable
    Public listVTCX As DataTable

    Public _LocCXNhomVT As Object
    Public _LocCXTenVT As Object
    Public _LocCXHangVT As Object
    Public _LocCXModelVT As Object

    Public Enum LoaiTuDien
        YeuCauDen = 0
        MucDoCan = 1
        TrangThaiChaoGia = 2
        HinhThucTT = 3
        TGCungUng = 4
        ThoiDiemTT = 5
        NoiDungThiCong = 6
        NhomMonHoc = 7
        HangMucCongViec = 8
        NhomCongViec = 9
        NoiDungCongViec = 10
        NhomNoiDungThiCong = 11
        TinhThanh = 12
        KhuCN = 13
        LinhVucSX = 14
        NhomKyNang = 15
        KyNang = 16
        LoaiDatHang = 17
        LoaiHinhDN = 18
        LoaiHinhChuSoHuu = 19
        TinhTrangKH = 20
        CanTon_SoLanXuat = 21
        CanTon_SoKH = 22
        TonMin_XXX = 23
        TonMin_YYY = 24
        TonMin_MauSoChia = 25
        DatMin_MauSoChia = 26
        DatMin_SLXuatMOQ3 = 27
        DatMin_SoKHMOQ3 = 28
        DatMin_XuatMax = 29
        DatMin_SLXuatMOQ2 = 30
        DatMin_SoKHMOQ2 = 31
        DatMin_SLXuatMOQ1 = 32
        DatMin_SoKHMOQ1 = 33
        LoaiChiTieu = 34
        EmailMarketing = 35
        NhomChiTieu = 36
        ChiTieu = 37
        YeuCauHoiGia = 38
        GiaoDichKH = 39
        NhomYCNoiBo = 40
        NguonKhachMoi = 41
        NhomNangLuc = 42
    End Enum

    Public Enum LoaiNangLuc
        QuyTrinh = 0
        KyNang = 1
    End Enum

    Public Enum TrangThaiYeuCau
        CanChuyenMa = 1
        CanChaoGia = 2
        CanHoiGiaHang = 3
        DaChaoGia = 4
        DaDuocKhaoSat = 5
        KHHuy = 6
    End Enum

    Public Enum TrangThaiYeuCauHoiGia
        ChoPhanHoi = 0
        DaBaoGia = 1
        HuyGiaCao = 2
        HuyKhongCoSan = 3
        HuyKhongCanNua = 4
        HuyDoNCC = 5
    End Enum

    Public Enum TrangThaiChaoGia
        ChoXacNhan = 1
        DaXacNhan = 2
        HuyGiaCao = 3
        HuyCoCheThanhToan = 4
        HuyYeuHonDTCT = 5
        HuyKhongCoSanHang = 6
        HuyKhongLayNua = 7
        HuyKhongLyDo = 8
    End Enum

    Public Enum TienTe
        VND = 0
        USD = 1
        EUR = 2
        JPY = 3
    End Enum

    Public Enum DanhMucQuyen
        Menu = 0
        QuyenThem = 1
        QuyenSua = 2
        QuyenXoa = 3
        TPQuanLy = 4
        TPKinhDoanh = 5
        TPKyThuat = 6
        KiemDuyet = 7
        BanQuanTri = 8
        Admin = 9
        KeToan = 10
    End Enum

    Public Function CauTrucQuyenTruyCap() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("Menu", Type.GetType("System.String"))
        tb.Columns.Add("QuyenThem", Type.GetType("System.Boolean"))
        tb.Columns.Add("QuyenSua", Type.GetType("System.Boolean"))
        tb.Columns.Add("QuyenXoa", Type.GetType("System.Boolean"))
        tb.Columns.Add("TPQuanLy", Type.GetType("System.Boolean"))
        tb.Columns.Add("TPKinhDoanh", Type.GetType("System.Boolean"))
        tb.Columns.Add("TPKyThuat", Type.GetType("System.Boolean"))
        tb.Columns.Add("KiemDuyet", Type.GetType("System.Boolean"))
        tb.Columns.Add("BanQuanTri", Type.GetType("System.Boolean"))
        tb.Columns.Add("Admin", Type.GetType("System.Boolean"))
        tb.Columns.Add("KeToan", Type.GetType("System.Boolean"))
        Return tb
    End Function

    Public Function TrangThaiDatHang() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Byte"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r As DataRow = tb.NewRow
        r("ID") = 0
        r("Ten") = "Soạn thảo"

        Dim r1 As DataRow = tb.NewRow
        r1("ID") = 1
        r1("Ten") = "Đã duyệt"

        Dim r2 As DataRow = tb.NewRow
        r2("ID") = 2
        r2("Ten") = "Chờ duyệt"
        tb.Rows.Add(r)
        tb.Rows.Add(r2)
        tb.Rows.Add(r1)
        Return tb
    End Function

    Public Function KiemTraQuyenMenu(ByVal _QuyenTruyCap As String, ByVal _TenMenu As Object) As Boolean
        Dim dr() As DataRow = QuyenSD.Select(String.Format("{0} = '{1}'", _QuyenTruyCap, _TenMenu))
        If dr.Length > 0 And Not dr Is Nothing Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function KiemTraQuyenSuDung(ByVal _QuyenTruyCap As String, ByVal _TenMenu As String, ByVal _QuyenSuDung As Integer) As Boolean
        Dim dr() As DataRow = QuyenSD.Select(String.Format("{0} = '{1}'", _QuyenTruyCap, _TenMenu))
        If dr.Length > 0 And Not dr Is Nothing Then
            If CType(dr(0)(_QuyenSuDung), Boolean) Then
                Return True
            Else
                ShowCanhBao("Bạn không có quyền thực hiện thao tác này !")
                Return False
            End If

        Else
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này !")
            Return False
        End If
    End Function

    Public Function KiemTraQuyenSuDungKhongCanhBao(ByVal _QuyenTruyCap As String, ByVal _TenMenu As String, ByVal _QuyenSuDung As Integer) As Boolean
        Dim dr() As DataRow = QuyenSD.Select(String.Format("{0} = '{1}'", _QuyenTruyCap, _TenMenu))
        If dr.Length > 0 And Not dr Is Nothing Then
            If CType(dr(0)(_QuyenSuDung), Boolean) Then
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If
    End Function

    Public Sub OpenFileOnLocal(ByVal _FileUrl As String, ByVal _TargetFileName As String, Optional ByVal _Overwrite As Boolean = True)
        ShowWaiting("Đang mở file...")
        Dim Impersonator As New Impersonator()
        Impersonator.BeginImpersonation()

        If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
        End If
        Application.DoEvents()
        Try
            System.IO.File.Copy(_FileUrl, Application.StartupPath & "\tmp\" & _TargetFileName, _Overwrite)
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
            Exit Sub
        End Try

        Impersonator.EndImpersonation()

        CloseWaiting()

        Dim psi As New ProcessStartInfo()
        With psi
            .FileName = Application.StartupPath & "\tmp\" & _TargetFileName
            .UseShellExecute = True
        End With
        Try
            Process.Start(psi)
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Public Sub OpenFile(ByVal _FileUrl As String)
        Dim psi As New ProcessStartInfo()
        With psi
            .FileName = _FileUrl
            .UseShellExecute = True
        End With
        Try
            Process.Start(psi)
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Public Sub LayTyGia()
        If Not System.IO.File.Exists(Application.StartupPath & "\Config.xml") Then
            ShowBaoLoi("Không lấy được link tải tỷ giá trong file cấu hình! ")
            Exit Sub
        End If
        ShowWaiting("Đang tải tỷ giá ...")
        Dim xmlUrlExRate As New XmlDocument
        xmlUrlExRate.Load(Application.StartupPath & "\Config.xml")

        Dim fileDownload As New FileDownloader()
        Try
            If fileDownload.DownloadFileWithProgress(CType(xmlUrlExRate.DocumentElement.SelectSingleNode("UrlExrate"), XmlElement).GetAttribute("Value"), Application.StartupPath & "\ExRate.xml") Then

                Dim xmlDoc As New XmlDocument
                xmlDoc.Load(Application.StartupPath & "\ExRate.xml")
                Dim root As XmlElement = xmlDoc.DocumentElement

                For Each ExrateNode As XmlElement In root.SelectNodes("Exrate")
                    Application.DoEvents()
                    AddParameter("@TyGia", ExrateNode.GetAttribute("Sell"))
                    AddParameter("@Ngay", DateTime.Parse(root.SelectSingleNode("DateTime").InnerText, CultureInfo.InvariantCulture))
                    AddParameterWhere("@TienTe", ExrateNode.GetAttribute("CurrencyCode"))
                    If doUpdate("tblTienTe", "Ten=@TienTe") Is Nothing Then ShowBaoLoi(LoiNgoaiLe)
                Next
            End If
        Catch ex As Exception
        Finally

            CloseWaiting()
        End Try


    End Sub

    Public Function LayGiaNhapGanNhat(ByVal _IDVatTu As Object, Optional ByVal _ThoiGian As DateTime = Nothing) As Double
        Dim sqlWhere As String = ""
        Dim sqlOrder As String = ""
        Dim sql As String = "SELECT TOP 1 (NHAPKHO.DonGia * PHIEUNHAPKHO.TyGia) FROM NHAPKHO  "
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.Sophieu=NHAPKHO.Sophieu "
        sql &= " WHERE IDVatTu = @IDVatTu "

        AddParameterWhere("@IDVatTu", _IDVatTu)
        If Not _ThoiGian = Nothing Then
            AddParameterWhere("@ThoiGian", _ThoiGian)
            sqlWhere = " AND PHIEUNHAPKHO.Ngaythang <= @ThoiGian "
        End If
        sqlOrder = " ORDER BY NHAPKHO.Ngaythang DESC"

        Dim tb As DataTable = ExecuteSQLDataTable(sql & sqlWhere & sqlOrder)
        If Not tb Is Nothing Then
            If tb.Rows.Count = 0 Then

                AddParameterWhere("@IDVatTu", _IDVatTu)
                If Not _ThoiGian = Nothing Then
                    AddParameterWhere("@ThoiGian", _ThoiGian)
                    sqlWhere = " AND PHIEUNHAPKHO.Ngaythang >= @ThoiGian "
                End If
                sqlOrder &= " ORDER BY NHAPKHO.Ngaythang "
                Dim tb1 As DataTable = ExecuteSQLDataTable(sql & sqlWhere & sqlOrder)
                If Not tb1 Is Nothing Then
                    If tb1.Rows.Count = 0 Then
                        Return 0
                    Else
                        Return tb1.Rows(0)(0)
                    End If
                End If
            Else
                Return tb.Rows(0)(0)
            End If
        Else
            Return 0
        End If
    End Function

    Public Function DataSourceDSFile(Optional ByVal DSFile As String = "", Optional ByVal FieldName As String = "File", Optional ByVal _char As String = ";") As DataTable
        Dim tb As New DataTable
        tb.Columns.Add(FieldName)
        If DSFile <> "" Then
            Dim listFile() As String = DSFile.ToString.Split(New Char() {_char})
            For Each file In listFile
                If file <> "" Then
                    tb.Rows.Add(tb.NewRow)
                    tb.Rows(tb.Rows.Count - 1)(FieldName) = file
                End If
            Next
        End If
        Return tb
    End Function

    Public Function DataSourceDSFile2(Optional ByVal DSFile As String = "", Optional ByVal FieldName1 As String = "File", Optional ByVal _char1 As String = ";", Optional ByVal FieldName2 As String = "Check", Optional ByVal _char2 As String = ",") As DataTable
        Dim tb As New DataTable
        tb.Columns.Add(FieldName1, Type.GetType("System.String"))
        tb.Columns.Add(FieldName2, Type.GetType("System.Boolean"))
        If DSFile <> "" Then
            Dim listFile() As String = DSFile.ToString.Split(New Char() {_char1})
            For Each file In listFile
                If file <> "" Then
                    Dim _strFileName() As String = file.ToString.Split(New Char() {_char2})
                    tb.Rows.Add(tb.NewRow)
                    tb.Rows(tb.Rows.Count - 1)(FieldName1) = _strFileName(0)
                    Try
                        tb.Rows(tb.Rows.Count - 1)(FieldName2) = CType(_strFileName(1), Boolean)
                    Catch ex As Exception
                        tb.Rows(tb.Rows.Count - 1)(FieldName2) = False
                    End Try

                End If
            Next
        End If
        Return tb
    End Function

    Public Function StrDSFile(ByVal gdv As DevExpress.XtraGrid.Views.Grid.GridView, Optional ByVal FieldName As String = "File") As String
        Dim DSFile As String = ""
        For i As Integer = 0 To gdv.RowCount - 1
            DSFile &= gdv.GetRowCellValue(i, FieldName)
            If i < gdv.RowCount - 1 Then
                DSFile &= ";"
            End If
        Next
        Return DSFile
    End Function

    Public Function gdv2String(ByVal gdv As DevExpress.XtraGrid.Views.Grid.GridView, Optional ByVal FieldName1 As String = "File", Optional ByVal FieldName2 As String = "Check") As String
        Dim DSFile As String = ""
        For i As Integer = 0 To gdv.RowCount - 1
            DSFile &= gdv.GetRowCellValue(i, FieldName1) & "," & gdv.GetRowCellValue(i, FieldName2)
            If i < gdv.RowCount - 1 Then
                DSFile &= ";"
            End If
        Next
        Return DSFile
    End Function

    Public Function Datatable2Str(ByVal tb As DataTable, Optional ByVal FieldName As String = "File") As String
        Dim DSFile As String = ""
        For i As Integer = 0 To tb.Rows.Count - 1
            DSFile &= tb.Rows(i)(FieldName)
            If i < tb.Rows.Count - 1 Then
                DSFile &= ";"
            End If
        Next
        Return DSFile
    End Function

    Public Function GetServerTime() As DateTime
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT getdate()")
        If Not tb Is Nothing Then
            Return Convert.ToDateTime(tb.Rows(0)(0))
        Else
            ShowCanhBao("Không lấy được giờ máy chủ, dữ liệu thời gian sẽ được lấy trên client !")
            Return Now
        End If
    End Function

    Public Function LaySoPhieu(ByVal _TenBang As String) As Object
        Dim sql As String = ""
        sql &= " DECLARE @DoDai AS NVARCHAR(5) "
        sql &= " SET @DoDai = N'00000'"
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoPhieu AS CHAR(9)"
        sql &= " DECLARE @Thang AS NVARCHAR(2)"
        sql &= " DECLARE @Nam AS NVARCHAR(2)"

        sql &= " SET @Thang = LEFT('00',2-LEN(CONVERT(NVARCHAR,DATEPART(mm,getdate())))) +  CONVERT(NVARCHAR,DATEPART(mm,getdate()))"
        sql &= " SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
        sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(LEFT(Sophieu,9),5),0))),0)+1  "
        sql &= "            FROM " & _TenBang
        sql &= "            WHERE"
        sql &= " 	       LEFT(Sophieu,2)=@Nam AND "
        sql &= " 	       SUBSTRING(Sophieu,3,2)=@Thang AND LEN(Sophieu)>=9 "
        sql &= "            )"

        sql &= " SET @SoPhieu = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
        sql &= " SELECT @SoPhieu"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)
    End Function

    Public Function LaySoPhieu2(ByVal _TenBang As String, ByVal NgayThang As DateTime) As Object
        Dim sql As String = ""
        sql &= " DECLARE @DoDai AS NVARCHAR(5) "
        sql &= " SET @DoDai = N'00000'"
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoPhieu AS CHAR(9)"
        sql &= " DECLARE @Thang AS NVARCHAR(2)"
        sql &= " DECLARE @Nam AS NVARCHAR(2)"

        sql &= " SET @Thang = '" & NgayThang.ToString("MM") & "'"
        sql &= " SET @Nam = '" & NgayThang.ToString("yy") & "'"
        sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(LEFT(Sophieu,9),5),0))),0)+1  "
        sql &= "            FROM " & _TenBang
        sql &= "            WHERE"
        sql &= " 	       LEFT(Sophieu,2)=@Nam AND "
        sql &= " 	       SUBSTRING(Sophieu,3,2)=@Thang AND LEN(Sophieu)>=9 "
        sql &= "            )"

        sql &= " SET @SoPhieu = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
        sql &= " SELECT @SoPhieu"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)
        
    End Function

    Public Function LaySoPhieuOmron(ByVal NgayThang As DateTime) As Object
        Dim sql As String = ""
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoPhieu AS CHAR(9)"

        sql &= " SELECT ISNULL(MAX(SoPhieuO),0)+1 FROM PHIEUDATHANG "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)

    End Function

    Public Function LaySoPhieuT(ByVal _TenBang As String) As Object

        Dim sql As String = ""
        sql &= " DECLARE @SoPhieu AS NVARCHAR(10) "
        sql &= " SET @SoPhieu = 'T' + SUBSTRING(CONVERT(NVARCHAR,GETDATE(),112),3,4) + '00000'"
        sql &= " DECLARE @SoMax AS NVARCHAR(5)"
        sql &= " SET @SoMax = (SELECT CONVERT(NVARCHAR,ISNULL(MAX(RIGHT(SoPhieuT,5)),0) + 1) FROM " & _TenBang & " WHERE LEFT(@SoPhieu,5) = LEFT(SoPhieuT,5))"
        sql &= " SELECT LEFT(@SoPhieu,10-LEN(@SoMax)) + @SoMax"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)
    End Function

    Public Function LaySoPhieuNhapKhau(ByVal _TenBang As String) As Object
        Dim sql As String = ""
        sql &= " DECLARE @DoDai AS NVARCHAR(3) "
        sql &= " SET @DoDai = N'000'"
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoPhieu AS CHAR(5)"
        sql &= " DECLARE @Nam AS NVARCHAR(2)"

        sql &= " SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
        sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(LEFT(SoPhieuNhapKhau,5),3),0))),0)+1  "
        sql &= "            FROM " & _TenBang
        sql &= "            WHERE"
        sql &= " 	        LEFT(SoPhieuNhapKhau,2)=@Nam "
        sql &= " 	        AND LEN(LEFT(SoPhieuNhapKhau,5))=5"
        sql &= "            )"
        sql &= " SET @SoPhieu = @Nam + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
        sql &= " SELECT @SoPhieu"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)
    End Function

    Public Function GetFileNameFromURL(ByVal URL As String) As String
        If URL.IndexOf("/"c) = -1 Then Return String.Empty

        Return "\" & URL.Substring(URL.LastIndexOf("/"c) + 1)
    End Function

    Public Function KiemTraDuyetLuong(ByVal ThangNam As String) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ISNULL((SELECT Duyet FROM tblDuyetLuong WHERE [Month]='" & ThangNam & "'),convert(bit,0))")
        If Not tb Is Nothing Then
            Return tb.Rows(0)(0)
        Else
            ShowBaoLoi(LoiNgoaiLe)
            Return False
        End If
    End Function

    Public Sub ThemThongBaoChoNV(ByVal _NoiDung As String, ByVal _IDNhanVien As Object)
        Dim tg As DateTime = GetServerTime()
        AddParameter("@NoiDung", _NoiDung)
        AddParameter("@ThoiGian", tg)
        AddParameter("@IDNhanVien", _IDNhanVien)
        If doInsert("tblThongBao") Is Nothing Then
            ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
        End If
    End Sub

    Public Sub PhanBoChiPhiNhap(ByVal _SoPhieuNK As String)
        Dim sql As String = ""
        sql &= " DECLARE @TongChiPhi float"
        sql &= " DECLARE @TongTienHang float"
        sql &= " DECLARE @SoPhieuDH nvarchar(15)"
        sql &= " SET @SoPhieuDH = (SELECT SoPhieuDH FROM PHIEUNHAPKHO WHERE SoPhieu=@SoPhieu)"
        sql &= " SET @TongChiPhi= (SELECT ISNULL((SELECT Sum(SoTien) FROM CHI WHERE MucDich IN (205,208) AND ChiPhiNhap=1 AND (PhieuTC0=@SoPhieuDH OR PhieuTC1=@SoPhieu)),0)"
        sql &= " 	+ ISNULL((SELECT Sum(SoTien) FROM UNC WHERE MucDich IN (205,208) AND ChiPhiNhap=1 AND (PhieuTC0=@SoPhieuDH OR PhieuTC1=@SoPhieu)),0) )"
        sql &= " SET @TongTienHang = (SELECT SUM(SoLuong*DonGia) FROM NHAPKHO WHERE SoPhieu=@SoPhieu)"
        sql &= " UPDATE NHAPKHO"
        sql &= " SET ChiPhi=round((DonGia/@TongTienHang)* @TongChiPhi,0)"
        sql &= " WHERE SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", _SoPhieuNK)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function PhanBoChiPhiNhapR(ByVal _SoPhieuNK As String) As String
        Dim sql As String = ""

        sql &= " DECLARE @TongChiPhi float"
        sql &= " DECLARE @TongTienHang float"
        sql &= " DECLARE @SoPhieuDH nvarchar(15)"
        sql &= " SET @SoPhieuDH = (SELECT SoPhieuDH FROM PHIEUNHAPKHO WHERE SoPhieu=@SoPhieu)"
        sql &= " SET @TongChiPhi= (SELECT ISNULL((SELECT Sum(SoTien) FROM CHI WHERE MucDich IN (205,208) AND ChiPhiNhap=1 AND (PhieuTC0=@SoPhieuDH OR PhieuTC1=@SoPhieu)),0)"
        sql &= " 	+ ISNULL((SELECT Sum(SoTien) FROM UNC WHERE MucDich IN (205,208) AND ChiPhiNhap=1 AND (PhieuTC0=@SoPhieuDH OR PhieuTC1=@SoPhieu)),0) )"
        sql &= " SET @TongTienHang = (SELECT SUM(SoLuong*DonGia) FROM NHAPKHO WHERE SoPhieu=@SoPhieu)"
        sql &= " UPDATE NHAPKHO"
        sql &= " SET ChiPhi=(DonGia/@TongTienHang)* @TongChiPhi"
        sql &= " WHERE SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", _SoPhieuNK)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            Return LoiNgoaiLe
        Else
            Return ""
        End If
    End Function

    Public Function DataTableNhomKH() As DataTable
        Dim tb As DataTable = New DataTable()
        tb.Columns.Add("Ma", GetType(Integer))
        tb.Columns.Add("MoTa", GetType(String))
        tb.Rows.Add(1, "Thương mại, Chế tạo máy, Tích hợp …")
        tb.Rows.Add(2, "End user")
        Return tb
    End Function

    Public Function DataTableCapKH() As DataTable
        Dim tb As DataTable = New DataTable()
        tb.Columns.Add("Ma", GetType(Integer))
        tb.Columns.Add("MoTa", GetType(String))
        tb.Rows.Add(1, "Cấp 1")
        tb.Rows.Add(2, "Cấp 2")
        tb.Rows.Add(3, "Cấp 3")
        Return tb
    End Function

    Public Function DataTableNhomBoPhan() As DataTable
        Dim tb As DataTable = New DataTable()
        tb.Columns.Add("Ma", GetType(Integer))
        tb.Columns.Add("Ten", GetType(String))
        tb.Rows.Add(1, "Tạo ra lợi nhuận")
        tb.Rows.Add(2, "Hậu trường")
        Return tb
    End Function

#Region "Hiển thị thông báo"

    Public Sub ShowAlert(ByVal noiDung As String, Optional ByVal tieuDe As String = "")
        alert = New DevExpress.XtraBars.Alerter.AlertControl()
        alert.AutoHeight = True
        alert.ShowPinButton = False
        alert.AutoFormDelay = 5000
        alert.AllowHotTrack = False
        alert.FormDisplaySpeed = DevExpress.XtraBars.Alerter.AlertFormDisplaySpeed.Fast
        alert.AppearanceCaption.BackColor = Color.Black
        alert.AppearanceCaption.Font = New Font("Time New Roman", 10, FontStyle.Bold)
        alert.AppearanceText.BackColor = Color.Navy
        alert.AppearanceText.Font = New Font("Time New Roman", 12, FontStyle.Bold)
        alert.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.BottomLeft
        alert.Show(New Form, tieuDe, noiDung, My.Resources.noti_Success_24)
    End Sub

    Public Sub ShowWaiting(ByVal _Noi_Dung As String, Optional ByVal _Tieu_De As String = "")

        wDlg = New DevExpress.Utils.WaitDialogForm(_Noi_Dung, _Tieu_De)

        wDlg.AutoSize = True
        wDlg.Show()
    End Sub

    Public Sub CloseWaiting()
        wDlg.Close()
    End Sub

    Public Sub ShowBaoLoi(ByVal _Noi_Dung As String)
        DevExpress.XtraEditors.XtraMessageBox.Show(_Noi_Dung, "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Public Sub ShowCanhBao(ByVal _Noi_Dung As String)
        DevExpress.XtraEditors.XtraMessageBox.Show(_Noi_Dung, "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Public Sub ShowThongBao(ByVal _Noi_Dung As String)
        DevExpress.XtraEditors.XtraMessageBox.Show(_Noi_Dung, "Chú ý!", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function ShowCauHoi(ByVal _Noi_Dung As String) As Boolean
        If DevExpress.XtraEditors.XtraMessageBox.Show(_Noi_Dung, "Chú ý!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then Return True
        Return False
    End Function

#End Region

End Module
