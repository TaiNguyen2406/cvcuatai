Imports SpreadsheetGear
Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Utils.StringHelper
Namespace Utils
    Public Class XuatExcel
        Public Shared Sub CreateExcelFileChaoGia(ByVal SoPhieu As Object, ByVal HangSX As Boolean, ByVal MaVT As Boolean, ByVal ThongSo As Boolean, ByVal TinhTrang As Boolean, ByVal VIE As Boolean, ByVal N0 As Boolean, ByVal CongTrinh As Boolean, ByVal GhiChu As Boolean, Optional ByVal MaKH As String = "")
            Dim fileKetXuat As String = ""
            If HangSX And MaVT And TinhTrang Then
                If VIE And N0 Then
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N0_L.xls"
                ElseIf VIE And N0 = False Then
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N2_L.xls"
                ElseIf VIE = False And N0 Then
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N0_L.xls"
                Else
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N2_L.xls"
                End If
            Else
                If VIE And N0 Then
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N0.xls"
                ElseIf VIE And N0 = False Then
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N2.xls"
                ElseIf VIE = False And N0 Then
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N0.xls"
                Else
                    fileKetXuat = Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N2.xls"
                End If
            End If

            If Not System.IO.File.Exists(fileKetXuat) Then
                ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                Exit Sub
            End If
            ShowWaiting("Đang xuất ra Excel...")
            Dim sql As String = ""

            sql &= " SELECT Ngaythang, KHACHHANG.Ten AS TenKH, ISNULL(KHACHHANG.ttcFax,'...') AS FaxKH,ISNULL(KHACHHANG.ttcDienthoai,'...') AS DienThoaiKH,tblHinhThucTTKH.HinhThucTT_VIE,tblHinhThucTTKH.HinhThucTT_ENG,"
            sql &= " 	    NHANSU_Ngd.ten AS NguoiGD,ISNULL(NHANSU_Ngd.Mobile,'...') AS DienThoaiNgd,NHANSU_Ngd.Email AS EmailNgd,"
            sql &= " 	    NHANSU.Ten AS NhanVien,ISNULL(NHANSU.Mobile,'...') AS DienThoaiNV,NHANSU.Email AS EmailNV,"
            sql &= " 	    (KHACHHANG.ttcMa + N' ' + Sophieu) AS ChaoGia,(N'YC' + ISNULL(BANGCHAOGIA.Masodathang,'') + N' CG' + Sophieu + N' KD') FileCG,"
            sql &= " 	    (BANGCHAOGIA.Masodathang + N'/YC/' +KHACHHANG.ttcMa ) AS YeuCau, TenDuan,"
            sql &= " 	    TienTruocthue AS TienTruocThue,Tienthue AS TienThue,"
            sql &= " 	    Tiente AS TienTe, TAIKHOAN.MaSo AS MaSoTK,TAIKHOAN.Ten AS TenTK, TAIKHOAN.Ten_ENG AS TenTK_ENG,tblTienTe.Ten AS TenTienTe,ISNULL(NgayGiaoDuKien,N' ')NgayGiaoDuKien"
            sql &= " FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
            sql &= "     LEFT OUTER JOIN NHANSU ON BANGCHAOGIA.IDTakecare=NHANSU.ID"
            sql &= "     LEFT OUTER JOIN NHANSU AS NHANSU_Ngd ON BANGCHAOGIA.IDNgd= NHANSU_Ngd.ID"
            sql &= "     LEFT OUTER JOIN TAIKHOAN ON BANGCHAOGIA.IDTaiKhoan=TAIKHOAN.ID"
            sql &= "     LEFT OUTER JOIN tblTienTe ON BANGCHAOGIA.Tiente=tblTienTe.ID"
            sql &= "     LEFT OUTER JOIN tblHinhThucTTKH ON BANGCHAOGIA.IDHinhThucTT=tblHinhThucTTKH.ID"
            sql &= " WHERE BANGCHAOGIA.Sophieu=@SP"

            sql &= " DECLARE @tmpTable table"
            sql &= " ("
            sql &= "     TenVT NVARCHAR(4000),"
            sql &= "     MaVT NVARCHAR(500),"
            sql &= "     HangSX NVARCHAR(100),"
            sql &= "     TenDVT NVARCHAR(30),"
            sql &= "     SoLuong float,"
            sql &= "     DonGia Float,"
            sql &= "     ThanhTien Float,"
            sql &= "     TinhTrang Nvarchar(250),"
            sql &= "     TinhTrang_ENG NVARCHAR(250)"
            sql &= " )"

            sql &= " INSERT INTO @tmpTable(TenVT,TenDVT,MaVT,HangSX,SoLuong,DonGia,ThanhTien,TinhTrang,TinhTrang_ENG)"
            If VIE Then
                sql &= " SELECT (CASE @XuatGhiChu WHEN 1 THEN (CASE @XuatThongSo WHEN 1 THEN ISNULL(TENVATTU.Ten,'') + N' ' + ISNULL(VATTU.Thongso,'') ELSE TENVATTU.Ten END) + (CASE when CHAOGIA.GhiChu is not null THEN  char(10) ELSE N'' END) + isnull(CHAOGIA.GhiChu,'') ELSE "
                sql &= " (CASE @XuatThongSo WHEN 1 THEN ISNULL(TENVATTU.Ten,'') + N' ' + ISNULL(VATTU.Thongso,'') ELSE TENVATTU.Ten END) END) TenVT, "
                sql &= " TENDONVITINH.Ten, "
            Else
                sql &= " SELECT (CASE @XuatGhiChu WHEN 1 THEN (CASE @XuatThongSo WHEN 1 THEN ISNULL(TENVATTU.Ten_ENG,'') + N' ' + ISNULL(VATTU.Thongso_ENG,'') ELSE TENVATTU.Ten_ENG END) + (CASE when CHAOGIA.GhiChu is not null THEN  char(10) ELSE N'' END) + isnull(CHAOGIA.GhiChu,'') ELSE "
                sql &= " (CASE @XuatThongSo WHEN 1 THEN ISNULL(TENVATTU.Ten_ENG,'') + N' ' + ISNULL(VATTU.Thongso_ENG,'') ELSE TENVATTU.Ten_ENG END) END) TenVT, "
                sql &= " TENDONVITINH.Ten_ENG, "
            End If
            sql &= " 	    (CASE @XuatMaVT WHEN 1 THEN VATTU.Model ELSE N' ' END)MaVT,"
            sql &= " 	    (CASE @XuatHangSX WHEN 1 THEN TENHANGSANXUAT.Ten ELSE N' ' END )HangSX,"
            sql &= " 	    Soluong AS SoLuong,Dongia AS DonGia,(Soluong*Dongia) AS ThanhTien,"
            sql &= " 	    ISNULL((SELECT NoiDung FROm tblTuDien WHERE Loai=4 AND Ma=IDTGGiaoHang),N' ')TinhTrang,"
            sql &= " 	    ISNULL((SELECT NoiDung_ENG FROM tblTuDien WHERE Loai=4 AND Ma=IDTGGiaoHang),N' ')TinhTrang_ENG"
            sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
            sql &= "     LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            sql &= "     LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            sql &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            If CongTrinh Then
                sql &= " WHERE  CHAOGIA.Sophieu=N'" & SoPhieu & "CT' AND CHAOGIA.SoLuong<>0"
            Else
                sql &= " WHERE  CHAOGIA.Sophieu=@SP AND CHAOGIA.SoLuong<>0 "
            End If

            sql &= " ORDER BY CHAOGIA.AZ "

            sql &= " INSERT INTO @tmpTable(TenVT,HangSX,SoLuong,DonGia,ThanhTien,TenDVT)"
            sql &= " SELECT Noidung,HangSx,Soluong,Dongia,(Soluong*Dongia)ThanhTien,"
            If VIE Then
                sql &= " TENDONVITINH.Ten"
            Else
                sql &= " TENDONVITINH.Ten_ENG"
            End If

            sql &= " FROM CHAOGIAAUX LEFT OUTER JOIN TENDONVITINH ON CHAOGIAAUX.Donvi=TENDONVITINH.ID"
            If CongTrinh Then
                sql &= " WHERE CHAOGIAAUX.Sophieu=N'" & SoPhieu & "CT' AND CHAOGIAAUX.SoLuong<>0"
            Else
                sql &= " WHERE CHAOGIAAUX.Sophieu=@SP AND CHAOGIAAUX.SoLuong<>0"
            End If

            sql &= " ORDER BY CHAOGIAAUX.AZ "

            sql &= " SELECT * FROM @tmpTable"

            AddParameter("@SP", SoPhieu)
            AddParameter("@XuatHangSX", HangSX)
            AddParameter("@XuatMaVT", MaVT)
            AddParameter("@XuatThongSo", ThongSo)
            AddParameter("@XuatGhiChu", GhiChu)
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If ds Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)

                Exit Sub
            End If

            Dim tb0 As DataTable = ds.Tables(0)
            Dim tb1 As DataTable = ds.Tables(1)
            Dim wb As IWorkbook
            Try
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
                Exit Sub
            End Try

            Dim ws As IWorksheet = wb.Worksheets(0)

            Dim _cells As IRange = ws.Cells

            Dim _RowIndex As Integer = 15
            Dim _ColumnCount As Integer = 9

            _cells(5, 0).Value &= tb0.Rows(0)("ChaoGia").ToString
            If tb0.Rows(0)("TenDuan").ToString <> "" Then
                _cells(5, 0).Value &= " - " & tb0.Rows(0)("Tenduan").ToString
            End If
            _cells(6, 0).Value &= tb0.Rows(0)("NguoiGD").ToString
            _cells(7, 0).Value &= tb0.Rows(0)("TenKH").ToString
            _cells(8, 0).Value &= tb0.Rows(0)("DienThoaiNgd") & "/" & tb0.Rows(0)("FaxKH")
            _cells(9, 0).Value &= tb0.Rows(0)("EmailNgd").ToString

            _cells(6, 4).Value &= Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).ToString("dd/MM/yyyy")
            _cells(7, 4).Value &= tb0.Rows(0)("NhanVien").ToString
            _cells(8, 4).Value &= tb0.Rows(0)("DienThoaiNV") & "/02253.686182"
            _cells(9, 4).Value &= tb0.Rows(0)("EmailNV").ToString

            Dim stt As Integer = 1
            For i As Integer = 0 To tb1.Rows.Count - 1
                If i < tb1.Rows.Count - 1 Then
                    _cells(_RowIndex - 1, 0, _RowIndex - 1, 9).Insert(InsertShiftDirection.Down)
                End If

                _cells(_RowIndex - 1, 0).Value = stt
                _cells(_RowIndex - 1, 1).Value = tb1.Rows(i)("TenVT").ToString
                If MaVT Then _cells(_RowIndex - 1, 2).Value = tb1.Rows(i)("MaVT").ToString
                If HangSX Then _cells(_RowIndex - 1, 3).Value = tb1.Rows(i)("HangSX").ToString
                _cells(_RowIndex - 1, 4).Value = tb1.Rows(i)("TenDVT").ToString
                _cells(_RowIndex - 1, 5).Value = tb1.Rows(i)("SoLuong")
                _cells(_RowIndex - 1, 6).Value = tb1.Rows(i)("DonGia")
                _cells(_RowIndex - 1, 7).Value = tb1.Rows(i)("ThanhTien")
                If TinhTrang Then _cells(_RowIndex - 1, 8).Value = tb1.Rows(i)("TinhTrang").ToString

                stt += 1
                _RowIndex += 1

            Next

            If MaVT = False Then
                _cells(0, 1).ColumnWidth += _cells(0, 2).ColumnWidth
                _cells(0, 2).ColumnWidth = 0
            End If
            If HangSX = False Then
                _cells(0, 1).ColumnWidth += _cells(0, 3).ColumnWidth
                _cells(0, 3).ColumnWidth = 0
            End If

            Dim _ThanhToan As String = ""
            Dim _PhanHoi As String = ""
            _cells(_RowIndex - 1, 7).Value = tb0.Rows(0)("TienTruocThue")
            _cells(_RowIndex, 7).Value = tb0.Rows(0)("TienThue")
            _RowIndex += 6
            If VIE = True Then
                _cells(_RowIndex - 4, 0).Value &= Utils.StringHelper.VIE2String(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue")), False, "đồng", "lẻ", "phẩy", 2)

                _cells(_RowIndex, 0, _RowIndex, 9).Insert(InsertShiftDirection.Down)
                _cells(_RowIndex, 0).Value = "'- Giao hàng: tại kho Quý công ty trong vòng " & tb0.Rows(0)("NgayGiaoDuKien").ToString & " ngày làm việc kể từ khi có xác nhận đặt hàng và thanh toán theo quy định."

                _cells(_RowIndex + 1, 0, _RowIndex + 1, 9).Insert(InsertShiftDirection.Down)

                If Convert.ToInt16(tb0.Rows(0)("TienTe")) = 0 Then
                    _cells(_RowIndex + 1, 0).Value = "'- Thanh toán: bằng tiền Việt Nam"
                Else
                    _cells(_RowIndex + 1, 0).Value = "'- Thanh toán: bằng tiền " & tb0.Rows(0)("TenTienTe").ToString & ", theo tỷ giá bán ra của ngân hàng" & tb0.Rows(0)("TenTK").ToString
                End If
                _cells(_RowIndex + 2, 0, _RowIndex + 2, 9).Insert(InsertShiftDirection.Down)
                _cells(_RowIndex + 2, 0).Value = "  bằng tiền mặt hoặc chuyển khoản số " & tb0.Rows(0)("MaSoTK").ToString & " tại " & tb0.Rows(0)("TenTK").ToString

                _cells(_RowIndex + 3, 0, _RowIndex + 3, 9).Insert(InsertShiftDirection.Down)
                If tb0.Rows(0)("HinhThucTT_VIE").ToString = "" Then
                    _ThanhToan = "  " & "theo thoả thuận giữa hai bên."
                Else
                    _ThanhToan = "  " & tb0.Rows(0)("HinhThucTT_VIE")
                End If
                _cells(_RowIndex + 5, 0, _RowIndex + 5, 9).Insert(InsertShiftDirection.Down)
                _PhanHoi = "'- Email phản hồi chất lượng sản phẩm - dịch vụ: baoanjsc@gmail.com"
            Else
                If Convert.ToInt16(tb0.Rows(0)("TienTe")) = 0 Then
                    _cells(_RowIndex - 4, 0).Value &= Utils.StringHelper.Number2String_Eng(Math.Round(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue"))))
                Else
                    _cells(_RowIndex - 4, 0).Value &= Utils.StringHelper.USD2String(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue")))
                End If
                _cells(_RowIndex, 0, _RowIndex, 9).Insert(InsertShiftDirection.Down)
                _cells(_RowIndex, 0).Value = "'- Execution time: Within " & tb0.Rows(0)("NgayGiaoDuKien").ToString & " working days (excluding Sundays and holidays) after confirmation by contract"

                _cells(_RowIndex + 1, 0, _RowIndex + 1, 9).Insert(InsertShiftDirection.Down)

                If Convert.ToInt16(tb0.Rows(0)("TienTe")) = 0 Then
                    _cells(_RowIndex + 1, 0).Value = "'- Payment: By VND"
                Else
                    _cells(_RowIndex + 1, 0).Value = "'- Payment: By " & tb0.Rows(0)("TenTienTe").ToString & ", by rate's " & tb0.Rows(0)("TenTK_ENG").ToString
                End If
                _cells(_RowIndex + 2, 0, _RowIndex + 2, 9).Insert(InsertShiftDirection.Down)
                _cells(_RowIndex + 2, 0).Value = "  by cash or bank transfer to " & tb0.Rows(0)("MaSoTK").ToString & " at " & tb0.Rows(0)("TenTK_ENG").ToString

                _cells(_RowIndex + 3, 0, _RowIndex + 3, 3).Insert(InsertShiftDirection.Down)
                If tb0.Rows(0)("HinhThucTT_ENG").ToString = "" Then
                    _ThanhToan = "  " & "under the agreement between the two parties."
                Else
                    _ThanhToan = "  " & tb0.Rows(0)("HinhThucTT_ENG")
                End If
                _cells(_RowIndex + 5, 0, _RowIndex + 5, 9).Insert(InsertShiftDirection.Down)
                _PhanHoi = "'- Email feedback product quality - service: baoanjsc@gmail.com"
            End If

            _cells(_RowIndex + 3, 0).Value = _ThanhToan
            _cells(_RowIndex + 5, 0).Value = _PhanHoi
            _cells(_RowIndex + 5, 0).Font.Bold = True
            CloseWaiting()
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH)
            End If
            If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH & "\" & tb0.Rows(0)("FileCG") & ".xls") Then
                If Not ShowCauHoi("File : " & tb0.Rows(0)("FileCG") & ".xls" & " đã có sẵn, bạn có muốn mở file chào giá cũ không ?") Then
                    wb.SaveAs(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH & "\" & tb0.Rows(0)("FileCG") & ".xls", FileFormat.Excel8)
                End If
            Else
                wb.SaveAs(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH & "\" & tb0.Rows(0)("FileCG") & ".xls", FileFormat.Excel8)
            End If

            Impersonator.EndImpersonation()
            'Dim p As New System.Diagnostics.Process
            'p.StartInfo.FileName = RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH & "\" & tb0.Rows(0)("FileCG") & ".xls"
            'p.Start()

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & MaKH & "\" & tb0.Rows(0)("FileCG") & ".xls", tb0.Rows(0)("FileCG") & ".xls", True)

        End Sub

        Public Shared Sub CreateExcelFileChaoGiaFromYC(ByVal SoPhieu As Object, ByVal HangSX As Boolean, ByVal MaVT As Boolean, ByVal ThongSo As Boolean, ByVal TinhTrang As Boolean, ByVal VIE As Boolean, ByVal FileKetXuat As String)
            If Not System.IO.File.Exists(FileKetXuat) Then
                ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & FileKetXuat)
                Exit Sub
            End If
            ShowWaiting("Đang xuất ra Excel...")
            Dim sql As String = ""

            If VIE Then
                sql &= " SELECT YEUCAUDEN.NoiDung, (CASE @XuatThongSo WHEN 1 THEN TENVATTU.Ten + N' ' + VATTU.Thongso ELSE TENVATTU.Ten END) TenVT,TENDONVITINH.Ten AS TenDVT,"
            Else
                sql &= " SELECT YEUCAUDEN.NoiDung, (CASE @XuatThongSo WHEN 1 THEN TENVATTU.Ten_ENG + N' ' + VATTU.ThongSo_ENG ELSE TENVATTU.Ten_ENG END) TenVT,TENDONVITINH.Ten_ENG AS TenDVT,"
            End If
            sql &= " 	    (CASE @XuatMaVT WHEN 1 THEN VATTU.Model ELSE N' ' END)MaVT,"
            sql &= " 	    (CASE @XuatHangSX WHEN 1 THEN TENHANGSANXUAT.Ten ELSE N' ' END )HangSX,"
            sql &= " 	    CHAOGIA.SoLuong,Dongia AS DonGia,(CHAOGIA.Soluong*Dongia) AS ThanhTien,"
            sql &= " 	    ISNULL((SELECT NoiDung FROm tblTuDien WHERE Loai=4 AND Ma=IDTGGiaoHang),N' ')TinhTrang,"
            sql &= " 	    ISNULL((SELECT NoiDung_ENG FROM tblTuDien WHERE Loai=4 AND Ma=IDTGGiaoHang),N' ')TinhTrang_ENG"
            sql &= " FROM YEUCAUDEN LEFT JOIN BANGCHAOGIA ON YEUCAUDEN.SoPhieu=BANGCHAOGIA.MaSoDatHang"
            sql &= " 	LEFT JOIN CHAOGIA ON YEUCAUDEN.ID=CHAOGIA.IDYeuCau"
            sql &= " 	LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
            sql &= "     LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            sql &= "     LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            sql &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            sql &= " WHERE YEUCAUDEN.SoPhieu=@SP "
            sql &= " ORDER By YEUCAUDEN.ID "
            '
            AddParameter("@SP", SoPhieu)
            AddParameter("@XuatHangSX", HangSX)
            AddParameter("@XuatMaVT", MaVT)
            AddParameter("@XuatThongSo", ThongSo)

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)

                Exit Sub
            End If

            Dim wb As IWorkbook = Factory.GetWorkbookSet().Workbooks.Open(FileKetXuat)
            Dim ws As IWorksheet = wb.Worksheets(0)

            Dim _cells As IRange = ws.Cells

            Dim _RowIndex As Integer = 13
            Dim _ColumnCount As Integer = 17

            Dim _FirstCol As Integer = 7


            ' _RowIndex += 1
            '  Dim stt As Integer = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                If i < dt.Rows.Count - 1 Then
                    _cells(_RowIndex - 1, _FirstCol, _RowIndex - 1, _FirstCol + 9).Insert(InsertShiftDirection.Down)
                End If

                ' _cells(_RowIndex - 1, 0).Value = stt
                _cells(_RowIndex - 1, _FirstCol + 1).Value = dt.Rows(i)("TenVT").ToString
                If MaVT Then _cells(_RowIndex - 1, _FirstCol + 2).Value = dt.Rows(i)("MaVT").ToString
                If HangSX Then _cells(_RowIndex - 1, _FirstCol + 3).Value = dt.Rows(i)("HangSX").ToString
                _cells(_RowIndex - 1, _FirstCol + 4).Value = dt.Rows(i)("TenDVT").ToString
                _cells(_RowIndex - 1, _FirstCol + 5).Value = dt.Rows(i)("SoLuong")
                _cells(_RowIndex - 1, _FirstCol + 6).Value = dt.Rows(i)("DonGia")
                _cells(_RowIndex - 1, _FirstCol + 7).Value = dt.Rows(i)("ThanhTien")
                If TinhTrang Then _cells(_RowIndex - 1, _FirstCol + 8).Value = dt.Rows(i)("TinhTrang").ToString

                'stt += 1
                _RowIndex += 1

            Next
            If TinhTrang = False Then
                _cells(0, _FirstCol + 8).ColumnWidth = 0
            End If
            If MaVT = False Then
                _cells(0, _FirstCol + 1).ColumnWidth += _cells(0, 2).ColumnWidth
                _cells(0, _FirstCol + 2).ColumnWidth = 0
            End If
            If HangSX = False Then
                _cells(0, _FirstCol + 1).ColumnWidth += _cells(0, 3).ColumnWidth
                _cells(0, _FirstCol + 3).ColumnWidth = 0
            End If

            _cells(11, _FirstCol + 1).Value = "Tên, thông số"

            _cells(11, _FirstCol + 2).Value = "Mã vật tư"
            _cells(11, _FirstCol + 3).Value = "Hãng sản xuất"
            _cells(11, _FirstCol + 4).Value = "ĐVT"
            _cells(11, _FirstCol + 5).Value = "Số lượng"
            _cells(11, _FirstCol + 6).Value = "Đơn giá"
            _cells(11, _FirstCol + 7).Value = "Thành tiền"

            CloseWaiting()

            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "Excel File|*.xls"
            saveFile.FileName = System.IO.Path.GetFileNameWithoutExtension(FileKetXuat) & ".xls"
            If saveFile.ShowDialog = DialogResult.OK Then

                wb.SaveAs(saveFile.FileName, FileFormat.Excel8)

                Dim p As New System.Diagnostics.Process
                p.StartInfo.FileName = saveFile.FileName
                p.Start()
            End If
        End Sub

        Public Shared Sub CreateExcelFileDatHang(ByVal SoPhieu As Object, ByVal HangSX As Boolean, ByVal MaVT As Boolean, ByVal ThongSo As Boolean, ByVal COCQ As Boolean, ByVal VIE As Boolean, ByVal N0 As Boolean, ByVal NgayVe As Boolean, Optional ByVal MaKH As String = "", Optional ByVal LoaiDH As Integer = 0)
            Dim fileKetXuat As String = ""

            If VIE And N0 Then
                fileKetXuat = Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_VIE_N0.xls"
            ElseIf VIE And N0 = False Then
                fileKetXuat = Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_VIE_N2.xls"
            ElseIf VIE = False And N0 Then
                fileKetXuat = Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_ENG_N0.xls"
            Else
                fileKetXuat = Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_ENG_N2.xls"
            End If

            If Not System.IO.File.Exists(fileKetXuat) Then
                ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                Exit Sub
            End If
            ShowWaiting("Đang xuất ra Excel...")
            Dim sql As String = ""

            sql &= " SELECT NgayDat, NgayNhan , KHACHHANG.Ten AS TenKH, KHACHHANG.ttcFax AS FaxKH,KHACHHANG.ttcDienthoai AS DienThoaiKH,"
            sql &= " 	    NHANSU_Ngd.ten AS NguoiGD,NHANSU_Ngd.Mobile AS DienThoaiNgd,NHANSU_Ngd.Email AS EmailNgd,"
            sql &= " 	    NHANSU.Ten AS NhanVien,NHANSU.Mobile AS DienThoaiNV,NHANSU.Email AS EmailNV,"
            sql &= " 	    (N'DH ' + KHACHHANG.ttcMa + ' ' + Sophieu) AS DatHang,"
            sql &= " 	    TienTruocthue AS TienTruocThue,Tienthue AS TienThue,PHIEUDATHANG.TienTe,DM_HINH_THUC_TT.GiaiThich HinhThucTT_VIE, '' HinhThucTT_ENG,tblTienTe.Ten AS TenTienTe"
            sql &= " FROM PHIEUDATHANG LEFT OUTER JOIN KHACHHANG ON PHIEUDATHANG.IDKhachhang=KHACHHANG.ID"
            sql &= "     LEFT OUTER JOIN NHANSU ON PHIEUDATHANG.IDTakecare=NHANSU.ID"
            sql &= "     LEFT OUTER JOIN NHANSU AS NHANSU_Ngd ON PHIEUDATHANG.IDNgd= NHANSU_Ngd.ID"
            sql &= "     LEFT OUTER JOIN DM_HINH_THUC_TT ON PHIEUDATHANG.IDHinhThucTT2=DM_HINH_THUC_TT.ID"
            sql &= "     LEFT OUTER JOIN tblTienTe ON PHIEUDATHANG.Tiente=tblTienTe.ID"
            sql &= " WHERE PHIEUDATHANG.Sophieu=@SP"

            If VIE Then
                sql &= " SELECT (CASE @XuatThongSo WHEN 1 THEN TENVATTU.Ten + N' ' + VATTU.Thongso ELSE TENVATTU.Ten END) TenVT,TENDONVITINH.Ten AS TenDVT,"
            Else
                sql &= " SELECT (CASE @XuatThongSo WHEN 1 THEN TENVATTU.Ten_ENG + N' ' + VATTU.ThongSo_ENG ELSE TENVATTU.Ten_ENG END) TenVT,TENDONVITINH.Ten_ENG AS TenDVT,"
            End If
            sql &= " 	    (CASE @COCQ WHEN 1 THEN (CASE(DATHANG.CO) WHEN 1 THEN N'x' ELSE N' ' END) ELSE N' ' END)CO,"
            sql &= " 	    (CASE @COCQ WHEN 1 THEN (CASE(DATHANG.CQ) WHEN 1 THEN N'x' ELSE N' ' END) ELSE N' ' END)CQ,"
            sql &= " 	    (CASE @XuatMaVT WHEN 1 THEN VATTU.Model ELSE N' ' END)MaVT,"
            sql &= " 	    (CASE @XuatHangSX WHEN 1 THEN TENHANGSANXUAT.Ten ELSE N' ' END )HangSX,SoLuong,"
            If LoaiDH = 1 Then
                sql &= " 	    FOB AS DonGia,(Soluong*FOB) AS ThanhTien, "
            Else
                sql &= " 	    Dongia AS DonGia,(Soluong*Dongia) AS ThanhTien, "
            End If

            sql &= "       (CASE @XuatNgayVe WHEN 1 THEN DatHang.NgayVe ELSE N'' END)NgayVe"
            sql &= " FROM DATHANG LEFT OUTER JOIN VATTU ON DATHANG.IDvattu=VATTU.ID"
            sql &= "     LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            sql &= "     LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            sql &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            If LoaiDH = 1 Then
                sql &= " WHERE DATHANG.SoPhieuPhu=@SP"
            Else
                sql &= " WHERE DATHANG.Sophieu=@SP"
            End If
            If LoaiDH = 2 Then
                sql &= " ORDER BY DATHANG.SoPhieu DESC,DATHANG.SoPhieuPhu,DATHANG.AZ,HangSX,Model"
            Else
                sql &= " ORDER BY DATHANG.AZ,DATHANG.ID"
            End If


            AddParameter("@SP", SoPhieu)
            AddParameter("@XuatHangSX", HangSX)
            AddParameter("@XuatMaVT", MaVT)
            AddParameter("@XuatThongSo", ThongSo)
            AddParameter("@COCQ", COCQ)
            AddParameter("@XuatNgayVe", NgayVe)

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If ds Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)

                Exit Sub
            End If

            Dim tb0 As DataTable = ds.Tables(0)
            Dim tb1 As DataTable = ds.Tables(1)


            Dim wb As IWorkbook = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
            Dim ws As IWorksheet = wb.Worksheets(0)

            Dim _cells As IRange = ws.Cells

            Dim _RowIndex As Integer = 20
            Dim _ColumnCount As Integer = 11
            _cells(9, 1).Value = tb0.Rows(0)("TenKH")
            _cells(9, 7).Value = tb0.Rows(0)("NgayDat")
            _cells(10, 1).Value = tb0.Rows(0)("NguoiGD")
            _cells(10, 7).Value = tb0.Rows(0)("NhanVien")
            _cells(11, 1).Value = tb0.Rows(0)("FaxKH")
            _cells(12, 1).Value = tb0.Rows(0)("DienThoaiNgd")
            _cells(12, 6).Value = "Mobile: " & tb0.Rows(0)("DienThoaiNV")
            _cells(13, 1).Value = tb0.Rows(0)("EmailNgd")
            _cells(13, 7).Value = tb0.Rows(0)("EmailNV")

            Dim stt As Integer = 1
            For i As Integer = 0 To tb1.Rows.Count - 1
                If i < tb1.Rows.Count - 1 Then
                    _cells(_RowIndex - 1, 0, _RowIndex - 1, 10).Insert(InsertShiftDirection.Down)
                End If

                _cells(_RowIndex - 1, 0).Value = stt
                _cells(_RowIndex - 1, 1).Value = tb1.Rows(i)("TenVT").ToString
                If MaVT Then _cells(_RowIndex - 1, 2).Value = tb1.Rows(i)("MaVT").ToString
                If HangSX Then _cells(_RowIndex - 1, 3).Value = tb1.Rows(i)("HangSX").ToString
                If COCQ Then _cells(_RowIndex - 1, 4).Value = tb1.Rows(i)("CO").ToString
                If COCQ Then _cells(_RowIndex - 1, 5).Value = tb1.Rows(i)("CQ").ToString
                _cells(_RowIndex - 1, 6).Value = tb1.Rows(i)("TenDVT").ToString
                _cells(_RowIndex - 1, 7).Value = tb1.Rows(i)("SoLuong")
                _cells(_RowIndex - 1, 8).Value = tb1.Rows(i)("DonGia")
                _cells(_RowIndex - 1, 9).Value = tb1.Rows(i)("ThanhTien")
                _cells(_RowIndex - 1, 10).Value = tb1.Rows(i)("NgayVe")
                stt += 1
                _RowIndex += 1

            Next
            If NgayVe = False Then
                _cells(0, 1).ColumnWidth += 0.3 * _cells(0, 10).ColumnWidth
                _cells(0, 2).ColumnWidth += 0.7 * _cells(0, 10).ColumnWidth + 1

                _cells(0, 10).ColumnWidth = 0
            End If

            If COCQ = False Then
                _cells(0, 1).ColumnWidth += _cells(0, 4).ColumnWidth
                _cells(0, 2).ColumnWidth += _cells(0, 5).ColumnWidth + 1
                _cells(0, 4).ColumnWidth = 0
                _cells(0, 5).ColumnWidth = 0
            End If

            If MaVT = False Then
                _cells(0, 1).ColumnWidth += _cells(0, 2).ColumnWidth + 1
                _cells(0, 2).ColumnWidth = 0
            End If
            If HangSX = False Then
                _cells(0, 1).ColumnWidth += _cells(0, 3).ColumnWidth + 1
                _cells(0, 3).ColumnWidth = 0
            End If




            Dim _ThanhToan As String = ""
            _cells(_RowIndex - 1, 9).Value = tb0.Rows(0)("TienTruocThue")
            _cells(_RowIndex, 9).Value = tb0.Rows(0)("TienThue")
            If VIE = True Then
                _cells(14, 0).Value = "Đặt hàng: " & tb0.Rows(0)("DatHang")
                _cells(14, 6).Value = "Ngày nhận hàng: " & Convert.ToDateTime(tb0.Rows(0)("NgayNhan")).ToString("dd/MM/yyyy")
                _cells(_RowIndex + 2, 0).Value = "Bằng chữ: " & Utils.StringHelper.VIE2String(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue")), False, "đồng", "lẻ", "phẩy", 2)


                _cells(_RowIndex + 7, 0, _RowIndex + 7, 10).Insert(InsertShiftDirection.Down)
                If tb0.Rows(0)("HinhThucTT_VIE").ToString = "" Then
                    _ThanhToan = "'- Thanh toán: theo thoả thuận giữa hai bên."
                Else
                    _ThanhToan = "'- " & tb0.Rows(0)("HinhThucTT_VIE")
                End If

                _cells(_RowIndex + 8, 0, _RowIndex + 8, 10).Insert(InsertShiftDirection.Down)
                If Convert.ToInt16(tb0.Rows(0)("TienTe")) = 0 Then
                    _cells(_RowIndex + 8, 0).Value = "  bằng tiền mặt hoặc chuyển khoản tiền VND"
                Else
                    _cells(_RowIndex + 8, 0).Value = "  bằng tiền " & tb0.Rows(0)("TenTienTe").ToString & ", theo tỷ giá bán ra của ngân hàng VietComBank, hoặc chuyển khoản"
                End If

            Else
                _cells(14, 0).Value = "Orders: " & tb0.Rows(0)("DatHang")
                _cells(14, 6).Value = "Delivery: " & tb0.Rows(0)("NgayNhan")
                ' _cells(_RowIndex + 2, 0).Value = "In words: " & Utils.StringHelper.USD2String(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue")))

                If Convert.ToInt16(tb0.Rows(0)("TienTe")) = 0 Then
                    _cells(_RowIndex + 2, 0).Value &= Utils.StringHelper.Number2String_Eng(Math.Round(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue"))))
                Else
                    _cells(_RowIndex + 2, 0).Value &= Utils.StringHelper.USD2String(Convert.ToDouble(tb0.Rows(0)("TienTruocThue") + tb0.Rows(0)("TienThue")))
                End If

                _cells(_RowIndex + 7, 0, _RowIndex + 7, 10).Insert(InsertShiftDirection.Down)
                If tb0.Rows(0)("HinhThucTT_ENG").ToString = "" Then
                    _ThanhToan = "'- Payment: under the agreement between the two parties."
                Else
                    _ThanhToan = "'- Payment: " & tb0.Rows(0)("HinhThucTT_ENG")
                End If
                _cells(_RowIndex + 8, 0, _RowIndex + 8, 10).Insert(InsertShiftDirection.Down)
                _cells(_RowIndex + 8, 0).Value = "  by cash or bank transfer (" & tb0.Rows(0)("TenTienTe").ToString & ")"

            End If

            _cells(_RowIndex + 7, 0).Value = _ThanhToan
            '_cells(11, 4).Value = "Pages including this page: "

            CloseWaiting()
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH)
            End If
            If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH & "\" & tb0.Rows(0)("DatHang") & ".xls") Then
                If Not ShowCauHoi("File : " & tb0.Rows(0)("DatHang") & ".xls" & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                    wb.Close()
                    Dim p1 As New System.Diagnostics.Process
                    p1.StartInfo.FileName = RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH & "\" & tb0.Rows(0)("DatHang") & ".xls"
                    p1.Start()

                    Exit Sub
                End If
            Else

            End If
            wb.SaveAs(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH & "\" & tb0.Rows(0)("DatHang") & ".xls", FileFormat.Excel8)

            Impersonator.EndImpersonation()
            'Dim p As New System.Diagnostics.Process
            'p.StartInfo.FileName = RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH & "\" & tb0.Rows(0)("DatHang") & ".xls"
            'p.Start()
            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(tb0.Rows(0)("NgayDat")).Year.ToString & "\" & UrlDatHang & MaKH & "\" & tb0.Rows(0)("DatHang") & ".xls", tb0.Rows(0)("DatHang") & ".xls")

        End Sub

        Public Shared Sub CreateExcelFileDatHangOmron(ByVal SoPhieu As Object, ByVal Ngay As DateTime, ByVal MaKH As String)
            Dim fileKetXuat As String = ServerName & "\excel$\BIEUMAU\Mau_Dat_Hang_Omron.xls"

            If Not System.IO.File.Exists(fileKetXuat) Then
                ShowBaoLoi("Không tìm thấy file mẫu đặt hàng : " & fileKetXuat)
                Exit Sub
            End If
            ShowWaiting("Đang xuất ra Excel...")
            Dim sql As String = ""
            sql &= " SELECT (Case when SoPhieuO is not null THEN 'BA ' + Convert(nvarchar,SoPhieuO) + '/'+ right(convert(nvarchar,PHIEUDATHANG.NgayDat,5),5) ELSE '' END) SoPhieuO FROM PHIEUDATHANG WHERE SoPhieu=@SP "
            sql &= " SELECT VATTU.Model,VATTU.Code,SoLuong,DonGia,(Soluong*Dongia) AS ThanhTien"
            sql &= " FROM DATHANG LEFT OUTER JOIN VATTU ON DATHANG.IDvattu=VATTU.ID"
            sql &= " WHERE DATHANG.SoPhieu=@SP"
            sql &= " ORDER BY DATHANG.AZ,DATHANG.ID"
            AddParameter("@SP", SoPhieu)

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If ds Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            Dim tb As DataTable = ds.Tables(1)
            Dim wb As IWorkbook = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
            Dim ws As IWorksheet = wb.Worksheets(0)

            Dim _cells As IRange = ws.Cells

            Dim _RowIndex As Integer = 9
            Dim _ColumnCount As Integer = 9
            _cells(2, 0).Value = "PO#: " & ds.Tables(0).Rows(0)(0)
            _cells(3, 0).Value = "PO date: " & Ngay.ToString("dd/MM/yyyy")
            Dim TongTien As Double = 0
            Dim stt As Integer = 1
            For i As Integer = 0 To tb.Rows.Count - 1
                If i < tb.Rows.Count - 1 Then
                    _cells(_RowIndex, 0, _RowIndex, 10).Insert(InsertShiftDirection.Down)
                End If

                _cells(_RowIndex - 1, 0).Value = stt
                _cells(_RowIndex - 1, 1).Value = tb.Rows(i)("Model").ToString
                _cells(_RowIndex - 1, 2).Value = tb.Rows(i)("Code").ToString
                _cells(_RowIndex - 1, 3).Value = tb.Rows(i)("DonGia")
                _cells(_RowIndex - 1, 5).Value = tb.Rows(i)("SoLuong")
                _cells(_RowIndex - 1, 7).Value = tb.Rows(i)("ThanhTien")
                stt += 1
                _RowIndex += 1
                TongTien += tb.Rows(i)("ThanhTien")
            Next

            _cells(_RowIndex - 1, 7).Value = TongTien

            CloseWaiting()
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()

            If Not System.IO.Directory.Exists(RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH)
            End If
            If System.IO.File.Exists(RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH & "\" & ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls") Then
                If Not ShowCauHoi("File : " & ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls" & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                    wb.Close()
                    Dim p1 As New System.Diagnostics.Process
                    p1.StartInfo.FileName = RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH & "\" & ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls"
                    p1.Start()

                    Exit Sub
                End If
            Else

            End If
            wb.SaveAs(RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH & "\" & ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls", FileFormat.Excel8)
            Impersonator.EndImpersonation()
            'Dim p As New System.Diagnostics.Process
            'p.StartInfo.FileName = RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH & "\" & ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls"
            'p.Start()
            OpenFileOnLocal(RootUrlOld & Ngay.Year.ToString & "\" & UrlDatHang & MaKH & "\" & ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls", ds.Tables(0).Rows(0)(0).ToString.Replace("/", " ") & ".xls")
        End Sub

        Public Shared Sub TH_LuongThuong(ByVal FileName As String, ByVal data As DataTable, ByVal ThoiGian As String)
            Dim BieuMau As String = ServerName & "\excel$\BIEUMAU\TH_LUONG.xls"

            If Not System.IO.File.Exists(BieuMau) Then
                ShowBaoLoi("Không tìm thấy file mẫu : " & BieuMau)
                Exit Sub
            End If

            Dim wb As IWorkbook
            Try
                wb = Factory.GetWorkbookSet().Workbooks.Open(BieuMau)
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
                Exit Sub
            End Try

            Dim ws As IWorksheet = wb.Worksheets(0)

            Dim _cells As IRange = ws.Cells

            Dim _RowIndex As Integer = 5
            Dim _ColumnCount As Integer = 29

            Dim stt As Integer = 1
            With data
                _cells(0, 9).Value = "BẢNG TỔNG HỢP LƯƠNG THÁNG " & ThoiGian
                Dim phong As String = ""
                For i As Integer = 0 To .Rows.Count - 1
                    If i < .Rows.Count - 1 Then
                        _cells(_RowIndex - 1, 0, _RowIndex - 1, 28).Insert(InsertShiftDirection.Down)
                    End If
                    If phong.ToUpper <> .Rows(i)("PhongBan").ToString.ToUpper Then
                        _cells(_RowIndex - 1, 0, _RowIndex - 1, 28).Insert(InsertShiftDirection.Down)
                        _cells(_RowIndex - 1, 0).Value = .Rows(i)("PhongBan").ToString
                        _cells(_RowIndex - 1, 0, _RowIndex - 1, 1).Merge()
                        _cells(_RowIndex - 1, 0, _RowIndex - 1, 1).Font.Bold = True
                        _cells(_RowIndex - 1, 0, _RowIndex - 1, 1).HorizontalAlignment = HAlign.Left
                        phong = .Rows(i)("PhongBan").ToString
                        _RowIndex += 1
                    End If

                    _cells(_RowIndex - 1, 0).Value = stt
                    _cells(_RowIndex - 1, 1).Value = .Rows(i)("NhanVien").ToString
                    _cells(_RowIndex - 1, 2).Value = .Rows(i)("LuongCB")
                    _cells(_RowIndex - 1, 3).Value = .Rows(i)("LuongBH")
                    _cells(_RowIndex - 1, 4).Value = .Rows(i)("CongThuong")
                    _cells(_RowIndex - 1, 5).Value = .Rows(i)("CongNghi")
                    _cells(_RowIndex - 1, 6).Value = .Rows(i)("CNLe1")
                    _cells(_RowIndex - 1, 7).Value = .Rows(i)("CNLe2")
                    _cells(_RowIndex - 1, 8).Value = .Rows(i)("ThemGio1")
                    _cells(_RowIndex - 1, 9).Value = .Rows(i)("LuongCong2")
                    _cells(_RowIndex - 1, 10).Value = .Rows(i)("LuongThemGio")
                    _cells(_RowIndex - 1, 11).Value = .Rows(i)("Thuong2")
                    _cells(_RowIndex - 1, 12).Value = .Rows(i)("ThuongDotXuat")
                    _cells(_RowIndex - 1, 13).Value = .Rows(i)("PCTrachNhiem")
                    _cells(_RowIndex - 1, 14).Value = .Rows(i)("PCXangAn")
                    _cells(_RowIndex - 1, 15).Value = .Rows(i)("PCAnCa")
                    _cells(_RowIndex - 1, 16).Value = .Rows(i)("PC_CT_Xang")
                    _cells(_RowIndex - 1, 17).Value = .Rows(i)("PCDVKTLuong")
                    _cells(_RowIndex - 1, 18).Value = .Rows(i)("PCDVKTXang")
                    _cells(_RowIndex - 1, 19).Value = .Rows(i)("PC_DVKT_TrachNhiem")
                    _cells(_RowIndex - 1, 20).Value = .Rows(i)("Tong2")
                    _cells(_RowIndex - 1, 21).Value = .Rows(i)("TienAnDaChi")
                    _cells(_RowIndex - 1, 22).Value = .Rows(i)("BHCongTyTra")
                    _cells(_RowIndex - 1, 23).Value = .Rows(i)("BHNhanVienTra")
                    _cells(_RowIndex - 1, 24).Value = .Rows(i)("QuyCongDoan")
                    _cells(_RowIndex - 1, 25).Value = .Rows(i)("ThucLinh2")
                    _cells(_RowIndex - 1, 26).Value = .Rows(i)("ThucChi")
                    _cells(_RowIndex - 1, 27).Value = .Rows(i)("TaiKhoan")
                    _cells(_RowIndex - 1, 28).Value = .Rows(i)("SoCMT")
                    _RowIndex += 1
                    stt += 1
                Next
            End With



            If System.IO.File.Exists(FileName) Then
                If Not ShowCauHoi("File : " & FileName & " đã có sẵn, bạn có muốn mở file cũ không ?") Then
                    wb.SaveAs(FileName, FileFormat.Excel8)
                End If
            Else
                wb.SaveAs(FileName, FileFormat.Excel8)
            End If


            Dim p As New System.Diagnostics.Process
            p.StartInfo.FileName = FileName
            p.Start()


        End Sub

        Public Shared Function CreateExcelFileYeuCauGuiNCC(ByVal _SoPhieu As String, ByVal _data As DataTable, ByVal dr As DataRow, ByVal _url As String, ByVal _PhuTrach As String, ByVal _SoDiDong As String, ByVal _EmailPhuTrach As String) As String
            Dim fileKetXuat As String = Application.StartupPath & "\Excel\HOIHANG\_MAUHOIHANG_VIE.xls"

            If Not System.IO.File.Exists(fileKetXuat) Then
                ShowBaoLoi("Không tìm thấy file mẫu : " & fileKetXuat)
                Return ""
                Exit Function
            End If
            ShowWaiting("Đang xuất ra Excel...")
            Dim sql As String = ""


            Dim wb As IWorkbook
            Try
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
                Return ""
                Exit Function
            End Try

            Dim ws As IWorksheet = wb.Worksheets(0)

            Dim _cells As IRange = ws.Cells

            Dim _RowIndex As Integer = 15
            Dim _ColumnCount As Integer = 9

            _cells(5, 1).Value &= _SoPhieu
            _cells(6, 1).Value &= dr("TenNgd").ToString
            _cells(7, 1).Value &= dr("TenNCC").ToString
            _cells(8, 1).Value &= dr("Mobile") & "/" & dr("ttcFax")
            _cells(9, 1).Value &= dr("Email").ToString

            _cells(6, 5).Value &= GetServerTime.ToString("dd/MM/yyyy")
            _cells(7, 5).Value &= _PhuTrach
            _cells(8, 5).Value &= _SoDiDong & "/0313.686182"
            _cells(9, 5).Value &= _EmailPhuTrach

            For i As Integer = 0 To _data.Rows.Count - 1
                If i < _data.Rows.Count - 1 Then
                    _cells(_RowIndex - 1, 0, _RowIndex - 1, 10).Insert(InsertShiftDirection.Down)
                End If
                _cells(_RowIndex - 1, 0).Value = _data.Rows(i)("ID")
                _cells(_RowIndex - 1, 1).Value = _data.Rows(i)("AZ")
                _cells(_RowIndex - 1, 2).Value = CType(_data.Rows(i)("TenVT").ToString & vbCrLf & _data.Rows(i)("NoiDung").ToString, String).Trim
                _cells(_RowIndex - 1, 3).Value = _data.Rows(i)("Model")
                _cells(_RowIndex - 1, 4).Value = _data.Rows(i)("HangSX")
                _cells(_RowIndex - 1, 5).Value = _data.Rows(i)("DVT")
                _cells(_RowIndex - 1, 6).Value = _data.Rows(i)("SoLuong")

                _RowIndex += 1

            Next

            CloseWaiting()
            If Not System.IO.Directory.Exists(_url) Then
                System.IO.Directory.CreateDirectory(_url)
            End If
            wb.SaveAs(_url & "HH" & _SoPhieu & " " & dr("ttcMa").ToString & ".xls", FileFormat.Excel8)
            wb.Close()

            Return _url & "HH" & _SoPhieu & " " & dr("ttcMa").ToString & ".xls"
            'RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang
        End Function


    End Class
End Namespace
