Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Xml
Imports System.Globalization
Imports BACSOFT.Utils

Public Class frmTachChaoGia
    Public tmpTrangThai As New Utils.TrangThai
    Public tmpMaTuDien As Object
    Public SPYeuCau As Object
    Public SPYeuCauMoi As Object
    Private EndSelect As Boolean
    Private Move_Next As Boolean
    Public SPChaoGia As Object
    Public sPChaoGiaMoi As Object
    Private _exit As Boolean

    Private Sub frmCNChaoGia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowWaiting("Đang tải nội dung chào giá ...")
        loadTienTe()
        loadCbHinhThucTT()
        loadKhachHang()
        loadNguoiGD()
        loadTakeCare()
        LoadCbTrangThai()
        LoadCbTrangThaiChiTiet()
        loadDSThoiGianGH()
        loadTKNganHang()
        loadGdvVatTuChaoGia()
        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)

        Me.Text = "Tách chào giá"
        CloseWaiting()
    End Sub

#Region "Load dữ liệu cho các combobox"

    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            rcbTienTe.DataSource = tb
            cbTienTe.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                _exit = True
                cbTienTe.EditValue = Convert.ToByte(TienTe.VND)
                _exit = False
                tbTyGia.EditValue = 1
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadTKNganHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM TAIKHOAN")
        If Not tb Is Nothing Then
            cbTKNganHang.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbTKNganHang.EditValue = tb.Rows(0)(0)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadKhachHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,IDHinhThucTT FROM KHACHHANG")
        If Not tb Is Nothing Then
            gdvMaKH.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadNguoiGD()
        AddParameterWhere("@IDCTY", gdvMaKH.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=@IDCTY")
        If Not tb Is Nothing Then
            cbNguoiGD.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadTakeCare()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not tb Is Nothing Then
            cbTakeCare.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbTrangThaiChiTiet()
        AddParameter("@Loai", LoaiTuDien.TrangThaiChaoGia)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            cbTrangThai.Properties.DataSource = tb
            If TrangThai.isAddNew Or TrangThai.isCopy Then
                cbTrangThai.EditValue = tb.Rows(0)("Ma")
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvMaKH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvMaKH.KeyPress
        If gdvMaKH.IsPopupOpen Then
            Exit Sub
        Else
            gdvMaKH.ShowPopup()
        End If
    End Sub

    Private Sub gdvMaKH_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        On Error Resume Next
        If gdvMaKH.IsPopupOpen Then Exit Sub
        loadNguoiGD()
        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbHinhThucTT.EditValue = dr("IDHinhThucTT")
        cbNguoiGD.Focus()
    End Sub

    Private Sub LoadCbTrangThai()
        AddParameter("@Loai", LoaiTuDien.TrangThaiChaoGia)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            rcbTrangThaiChiTiet.DataSource = tb
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
            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb) "
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
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT  IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb) "
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
            sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb) "
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadGdvVatTuChaoGia()
        Dim sql As String = ""
        sql &= " SELECT Ngaythang, IDKhachhang,TenDuan,TienTruocthue,Tienthue,TienChietkhau,Tienthucthu,Ngaynhan,Ngaygiao,IDHinhThucTT,MaSoDatHang,"
        sql &= " ISNULL(NgayGiaoDuKien,0)NgayGiaoDuKien,Ngayhuy,TyGia,IDUser,IDNgd,IDtakecare,Tiente,Congtrinh,Trangthai,Khautru,IDTaiKhoan,FileDinhkem"
        sql &= " FROM BANGCHAOGIA"
        sql &= " WHERE Sophieu=@SP"

        sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong, Convert(float,0)SLTach,CHAOGIA.IDYeucau AS IDYeuCau,(SELECT 0.0) TTGiaNhap,"
        sql &= " (CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = CHAOGIA.IDVatTu ORDER BY SoPhieu DESC),0) GiaNhapGanNhat, "
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        sql &= " CHAOGIA.Xuatthue AS XuatThue,CHAOGIA.Mucthue AS MucThue,VATTU.Gianhap1 AS GiaNhap,IDTGGiaoHang,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS CanXuat,TrangThai,CHAOGIA.Dongia AS DonGia,(0.0)GiaBanPT,"
        sql &= " CHAOGIA.Chietkhau AS ChietKhau,ROUND(((CHAOGIA.Chietkhau/ISNULL(CHAOGIA.Dongia,1))*100),2)ChietKhauPT,"
        sql &= " VATTU.Tiente1 AS TienTe,ISNULL(CHAOGIA.TyGia,tblTienTe.TyGia)TyGia, VATTU.HangTon , ISNULL(CHAOGIA.AZ,0)AZ, (SELECT 0) LoiGia"
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        If chkCongTrinh.Checked Then
            sql &= " WHERE CHAOGIA.Sophieu=N'" & SPChaoGia & "CT'"
        Else
            sql &= " WHERE CHAOGIA.Sophieu=N'" & SPChaoGia & "'"
        End If

        sql &= " ORDER BY AZ "

        AddParameter("@SP", SPChaoGia)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            _exit = True
            cbTienTe.EditValue = ds.Tables(0).Rows(0)("TienTe")
            _exit = False
            If Convert.ToInt32(cbTienTe.EditValue) = 0 Then
                tbTyGia.EditValue = 1
            Else
                tbTyGia.EditValue = ds.Tables(0).Rows(0)("TyGia")
            End If

            With ds.Tables(1)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If IsDBNull(.Rows(i)("GiaNhapGanNhat")) Then .Rows(i)("GiaNhapGanNhat") = 0
                    If .Rows(i)("GiaNhapGanNhat") = 0 Then
                        If .Rows(i)("GiaList") = 0 Then
                            .Rows(i)("GiaNhapGanNhat") = 0
                        Else
                            .Rows(i)("GiaNhapGanNhat") = (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue
                        End If

                    End If
                    .Rows(i)("TTGiaNhap") = .Rows(i)("SoLuong") * .Rows(i)("GiaNhapGanNhat")
                    If .Rows(i)("DonGia") < .Rows(i)("GiaNhapGanNhat") Then .Rows(i)("LoiGia") = 1
                    Dim _GiaBanPT As Double = 0
                    If .Rows(i)("GiaList") > 0 Then
                        If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(ds.Tables(0).Rows(0)("Tiente")) Then
                            _GiaBanPT = .Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)
                        Else
                            _GiaBanPT = .Rows(i)("DonGia") / .Rows(i)("GiaList")
                        End If
                    End If
                   
                    .Rows(i)("GiaBanPT") = Math.Round(_GiaBanPT * 100, 2)
                Next
            End With


            gdvVT.DataSource = ds.Tables(1)

            gdvMaKH.EditValue = ds.Tables(0).Rows(0)("IDKhachhang")
            If TrangThai.isUpdate Then
                tbSoPhieu.EditValue = SPChaoGia
                tbNgay.EditValue = ds.Tables(0).Rows(0)("Ngaythang")
            End If
            cbHinhThucTT.EditValue = ds.Tables(0).Rows(0)("IDHinhThucTT")
            SPYeuCau = ds.Tables(0).Rows(0)("MaSoDatHang")

            cbNguoiGD.EditValue = ds.Tables(0).Rows(0)("IDNgd")
            cbTakeCare.EditValue = ds.Tables(0).Rows(0)("IDtakecare")
            tbTenCongTrinh.EditValue = ds.Tables(0).Rows(0)("TenDuan")
            chkCongTrinh.Checked = ds.Tables(0).Rows(0)("Congtrinh")
            If TrangThai.isUpdate Then
                cbTrangThai.EditValue = ds.Tables(0).Rows(0)("Trangthai")
            End If
            _exit = True
            tbNgayNhan.EditValue = ds.Tables(0).Rows(0)("Ngaynhan")
            tbSoNgayGiao.EditValue = ds.Tables(0).Rows(0)("NgayGiaoDuKien")
            _exit = False
            tbNgayGiao.EditValue = ds.Tables(0).Rows(0)("Ngaygiao")
            tbNgayHuy.EditValue = ds.Tables(0).Rows(0)("Ngayhuy")
            gdvListFile.DataSource = DataSourceDSFile(ds.Tables(0).Rows(0)("FileDinhKem").ToString)

            cbTKNganHang.EditValue = ds.Tables(0).Rows(0)("IDTaiKhoan")



            tbThucCK.EditValue = ds.Tables(0).Rows(0)("TienChietkhau")
            tbKhauTruPT.EditValue = ds.Tables(0).Rows(0)("Khautru")
            tbTienTruocThue.EditValue = ds.Tables(0).Rows(0)("TienTruocthue")
            tbTienThue.EditValue = ds.Tables(0).Rows(0)("Tienthue")
            tbThucThu.EditValue = ds.Tables(0).Rows(0)("Tienthucthu")

            tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
            tbChietKhau.EditValue = Math.Round(tbThucCK.EditValue + (tbThucCK.EditValue * (tbKhauTruPT.EditValue / 100)), 2)
            tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSThoiGianGH()
        AddParameter("@Loai", LoaiTuDien.TGCungUng)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            cbThoiGianGH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub ktCoSan()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "slTon") - gdvVTCT.GetRowCellValue(i, "CanXuat") - gdvVTCT.GetRowCellValue(i, "SoLuong") >= 0 Then
                gdvVTCT.SetRowCellValue(i, "IDTGGiaoHang", 1)
            End If
        Next
    End Sub


    Private Sub loadCbHinhThucTT()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,HinhThucTT_VIE FROM tblHinhThucTTKH")
        If Not tb Is Nothing Then
            cbHinhThucTT.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbHinhThucTT.EditValue = tb.Rows(0)("ID")
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


#End Region

#Region "Lưu lại"
    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        Dim _count As Integer = 0

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "SLTach") > 0 Then
                _count += 1
            End If
        Next
        If _count = 0 Then
            ShowCanhBao("Chưa có số lượng cần tách")
            Exit Sub
        End If
        Dim tmp As Double = 0
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            tmp = gdvVTCT.GetRowCellValue(i, "SoLuong")
            gdvVTCT.SetRowCellValue(i, "SoLuong", gdvVTCT.GetRowCellValue(i, "SLTach"))
            _exit = True
            gdvVTCT.SetRowCellValue(i, "SLTach", tmp)
            _exit = False
        Next

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()

        tbTienTruocThue.EditValue = 0
        tbTienThue.EditValue = 0
        tbChietKhau.EditValue = 0

        With gdvVTCT
            For i As Integer = 0 To gdvVTCT.RowCount - 1
                tbTienTruocThue.EditValue += .GetRowCellValue(i, "ThanhTien")
                tbChietKhau.EditValue += (.GetRowCellValue(i, "ChietKhau") * .GetRowCellValue(i, "SoLuong"))
                If Convert.ToBoolean(.GetRowCellValue(i, "XuatThue")) Then
                    tbTienThue.EditValue += (.GetRowCellValue(i, "MucThue") / 100) * .GetRowCellValue(i, "ThanhTien")
                End If
            Next
        End With

        tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
        tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
        tbThucCK.EditValue = tbChietKhau.EditValue - tbKhauTru.EditValue
        tbThucThu.EditValue = tbTienSauThue.EditValue - tbThucCK.EditValue


        Dim SoPhieu As Object = LaySoPhieu2("BANGCHAOGIA", Convert.ToDateTime(tbNgay.EditValue).Date)



        Try
            BeginTransaction()
            AddParameter("@Sophieu", SoPhieu)
            AddParameter("@Ngaythang", tbNgay.EditValue)
            AddParameter("@IDKhachhang", gdvMaKH.EditValue)
            AddParameter("@IDNgd", cbNguoiGD.EditValue)
            AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
            AddParameter("@IDtakecare", cbTakeCare.EditValue)
            AddParameter("@Congtrinh", chkCongTrinh.Checked)
            AddParameter("@TenDuan", tbTenCongTrinh.EditValue)
            AddParameter("@Trangthai", cbTrangThai.EditValue)
            AddParameter("@Ngaynhan", tbNgayNhan.EditValue)
            AddParameter("@Ngaygiao", tbNgayGiao.EditValue)
            AddParameter("@Ngayhuy", tbNgayHuy.EditValue)
            AddParameter("@NgayGiaoDuKien", tbSoNgayGiao.EditValue)
            AddParameter("@TienTruocthue", tbTienTruocThue.EditValue)
            AddParameter("@TienChietkhau", tbThucCK.EditValue)
            AddParameter("@Khautru", tbKhauTruPT.EditValue)
            AddParameter("@Tienthue", tbTienThue.EditValue)
            AddParameter("@Tienthucthu", tbThucThu.EditValue)
            AddParameter("@Tiente", cbTienTe.EditValue)
            AddParameter("@Tygia", tbTyGia.EditValue)
            AddParameter("@IDTaiKhoan", cbTKNganHang.EditValue)
            AddParameter("@IDHinhThucTT", cbHinhThucTT.EditValue)
            AddParameter("@FileDinhKem", StrDSFile(gdvListFileCT))
            ' If TrangThai.isAddNew Or TrangThai.isCopy Then
            AddParameter("@Masodathang", SPYeuCau)
            If doInsert("BANGCHAOGIA") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            With gdvVTCT
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "SoLuong") > 0 Then
                        If chkCongTrinh.Checked Then
                            AddParameter("@SoPhieu", SoPhieu.ToString & "CT")
                        Else
                            AddParameter("@SoPhieu", SoPhieu)
                        End If
                        AddParameter("@IDvattu", .GetRowCellValue(i, "IDVatTu"))
                        AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                        AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                        AddParameter("@Tiente", cbTienTe.EditValue)
                        AddParameter("@TyGia", .GetRowCellValue(i, "TyGia"))
                        AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                        AddParameter("@Xuatthue", .GetRowCellValue(i, "XuatThue"))
                        AddParameter("@Chietkhau", .GetRowCellValue(i, "ChietKhau"))
                        If .GetRowCellValue(i, "TrangThai") = TrangThaiChaoGia.DaXacNhan Then
                            AddParameter("@Canxuat", .GetRowCellValue(i, "SoLuong"))
                        Else
                            AddParameter("@Canxuat", 0)
                        End If

                        AddParameter("@Trangthai", .GetRowCellValue(i, "TrangThai"))
                        AddParameter("@IDYeucau", .GetRowCellValue(i, "IDYeuCau"))
                        AddParameter("@IDTGGiaoHang", .GetRowCellValue(i, "IDTGGiaoHang"))
                        AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                        If doInsert("CHAOGIA") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                    AddParameter("@Trangthai", TrangThaiYeuCau.DaChaoGia)
                    AddParameterWhere("@IDYeucau", .GetRowCellValue(i, "IDYeuCau"))
                    If doUpdate("YEUCAUDEN", "ID=@IDYeucau") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Next
            End With


            For i As Integer = 0 To gdvVTCT.RowCount - 1
                tmp = gdvVTCT.GetRowCellValue(i, "SoLuong")
                gdvVTCT.SetRowCellValue(i, "SoLuong", gdvVTCT.GetRowCellValue(i, "SLTach") - tmp)
                _exit = True
                gdvVTCT.SetRowCellValue(i, "SLTach", 0)
                _exit = False
            Next
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()

            tbTienTruocThue.EditValue = 0
            tbTienThue.EditValue = 0
            tbChietKhau.EditValue = 0

            With gdvVTCT
                For i As Integer = 0 To gdvVTCT.RowCount - 1
                    tbTienTruocThue.EditValue += .GetRowCellValue(i, "ThanhTien")
                    tbChietKhau.EditValue += (.GetRowCellValue(i, "ChietKhau") * .GetRowCellValue(i, "SoLuong"))
                    If Convert.ToBoolean(.GetRowCellValue(i, "XuatThue")) Then
                        tbTienThue.EditValue += (.GetRowCellValue(i, "MucThue") / 100) * .GetRowCellValue(i, "ThanhTien")
                    End If
                Next
            End With

            tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
            tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
            tbThucCK.EditValue = tbChietKhau.EditValue - tbKhauTru.EditValue
            tbThucThu.EditValue = tbTienSauThue.EditValue - tbThucCK.EditValue

            AddParameter("@Sophieu", tbSoPhieu.EditValue)
            AddParameter("@Ngaythang", tbNgay.EditValue)
            AddParameter("@IDKhachhang", gdvMaKH.EditValue)
            AddParameter("@IDNgd", cbNguoiGD.EditValue)
            AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
            AddParameter("@IDtakecare", cbTakeCare.EditValue)
            AddParameter("@Congtrinh", chkCongTrinh.Checked)
            AddParameter("@TenDuan", tbTenCongTrinh.EditValue)
            AddParameter("@Trangthai", cbTrangThai.EditValue)
            AddParameter("@Ngaynhan", tbNgayNhan.EditValue)
            AddParameter("@Ngaygiao", tbNgayGiao.EditValue)
            AddParameter("@Ngayhuy", tbNgayHuy.EditValue)
            AddParameter("@NgayGiaoDuKien", tbSoNgayGiao.EditValue)
            AddParameter("@TienTruocthue", tbTienTruocThue.EditValue)
            AddParameter("@TienChietkhau", tbThucCK.EditValue)
            AddParameter("@Khautru", tbKhauTruPT.EditValue)
            AddParameter("@Tienthue", tbTienThue.EditValue)
            AddParameter("@Tienthucthu", tbThucThu.EditValue)
            AddParameter("@Tiente", cbTienTe.EditValue)
            AddParameter("@Tygia", tbTyGia.EditValue)
            AddParameter("@IDTaiKhoan", cbTKNganHang.EditValue)
            AddParameter("@IDHinhThucTT", cbHinhThucTT.EditValue)
            AddParameter("@FileDinhKem", StrDSFile(gdvListFileCT))
            AddParameterWhere("@SP", tbSoPhieu.EditValue)
            If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)


            With gdvVTCT
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "SoLuong") > 0 Then

                        If chkCongTrinh.Checked Then
                            AddParameter("@SoPhieu", tbSoPhieu.EditValue & "CT")
                        Else
                            AddParameter("@SoPhieu", tbSoPhieu.EditValue)
                        End If
                        AddParameter("@IDvattu", .GetRowCellValue(i, "IDVatTu"))
                        AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                        AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                        AddParameter("@Tiente", cbTienTe.EditValue)
                        AddParameter("@TyGia", .GetRowCellValue(i, "TyGia"))
                        AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                        AddParameter("@Xuatthue", .GetRowCellValue(i, "XuatThue"))
                        AddParameter("@Chietkhau", .GetRowCellValue(i, "ChietKhau"))
                        If .GetRowCellValue(i, "TrangThai") = TrangThaiChaoGia.DaXacNhan Then
                            AddParameter("@Canxuat", .GetRowCellValue(i, "SoLuong"))
                        Else
                            AddParameter("@Canxuat", 0)
                        End If

                        AddParameter("@Trangthai", .GetRowCellValue(i, "TrangThai"))
                        AddParameter("@IDYeucau", .GetRowCellValue(i, "IDYeuCau"))
                        AddParameter("@IDTGGiaoHang", .GetRowCellValue(i, "IDTGGiaoHang"))
                        AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                        AddParameterWhere("@IDCG", .GetRowCellValue(i, "IDCG"))
                        If doUpdate("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next
            End With


            For i = 0 To gdvVTCT.RowCount - 1
                If gdvVTCT.GetRowCellValue(i, "SoLuong") = 0 Then
                    AddParameterWhere("@IDCG", gdvVTCT.GetRowCellValue(i, "IDCG"))
                    If doDelete("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next


            ComitTransaction()

            ShowAlert("Tách chào giá thành công !")
            ShowThongBao("Chào giá sau khi tách là:" & tbSoPhieu.EditValue.ToString & " và " & SoPhieu.ToString)
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmChaoGia).LoadDS()
            Me.Close()

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

#End Region

#Region "Sự kiện của các nút lệnh chính"
    '========================= Chức năng nâng cao =====================================

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        colGiaNhapNew.VisibleIndex = 12
        colGiaNhapNew.Visible = chkHienGiaNhap.Checked
        colTTGiaNhap.VisibleIndex = 16
        colTTGiaNhap.Visible = chkHienGiaNhap.Checked
        colThongSo.VisibleIndex = 17
        colThongSo.Visible = chkHienThongSo.Checked
        colGiaList.VisibleIndex = 18
        colGiaList.Visible = chkGiaList.Checked
        colGiaBanBuon.VisibleIndex = 19
        colGiaBanBuon.Visible = chkGiaBanBuon.Checked
        colGiaBanLe.VisibleIndex = 20
        colGiaBanLe.Visible = chkHienGiaBanLe.Checked
        colGiaNhap.VisibleIndex = 21
        colGiaNhap.Visible = chkGiaNhap.Checked

    End Sub

    '============================================================================================

#End Region

#Region "Các sự kiện của grid vật tư chào giá gdvVTCT"

    Private Sub gdvVTCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvVTCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvVTCT.GetFocusedRowCellValue("IDVatTu"), gdvVTCT.GetFocusedRowCellValue("Sophieu"), gdvVTCT.GetFocusedRowCellValue("IDYeuCau"))
            If Not ds Is Nothing Then
                Dim f As New frmThongTinGiaNhap
                f.lbVatTu.Text &= gdvVTCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvVTCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvVTCT.GetFocusedRowCellValue("TenHang")
                f.lbGiaCungUng.Text &= Convert.ToDouble(ds.Tables(0).Rows(0)(0)).ToString("N2")
                f.gdvGiaNhap.DataSource = ds.Tables(1)
                f.gdvChaoGia.DataSource = ds.Tables(2)
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            If ShowCauHoi("Xoá dòng hiện tại ?") Then
                If TrangThai.isUpdate Then
                    Try
                        BeginTransaction()
                        AddParameterWhere("@IDCG", gdvVTCT.GetFocusedRowCellValue("IDCG"))
                        If doDelete("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                        AddParameterWhere("@IDYC", gdvVTCT.GetFocusedRowCellValue("IDYeuCau"))
                        AddParameter("@Trangthai", TrangThaiYeuCau.KHHuy)
                        If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                        ComitTransaction()
                        CType(gdvVT.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvVTCT.FocusedRowHandle)
                        'gdvVTCT.DeleteSelectedRows()
                    Catch ex As Exception
                        RollBackTransaction()
                        ShowBaoLoi(ex.Message)
                    End Try
                Else
                    CType(gdvVT.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvVTCT.FocusedRowHandle)
                    'gdvVTCT.DeleteSelectedRows()
                End If
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvVTCT.FocusedRowHandle = 0 Then Exit Sub
            Dim _tmp As Object = gdvVTCT.GetFocusedRowCellValue("AZ")
            gdvVTCT.SetFocusedRowCellValue("AZ", gdvVTCT.GetRowCellValue(gdvVTCT.FocusedRowHandle - 1, "AZ"))
            gdvVTCT.SetRowCellValue(gdvVTCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvVTCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvVTCT.FocusedRowHandle = gdvVTCT.RowCount - 1 Then Exit Sub
            Dim _tmp As Object = gdvVTCT.GetFocusedRowCellValue("AZ")
            gdvVTCT.SetFocusedRowCellValue("AZ", gdvVTCT.GetRowCellValue(gdvVTCT.FocusedRowHandle + 1, "AZ"))
            gdvVTCT.SetRowCellValue(gdvVTCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvVTCT.FocusedRowHandle -= 1

        End If
    End Sub

    Private Sub gdvVTCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvVTCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "GiaBanPT"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                If _exit Then Exit Select
                _exit = True
                gdvVTCT.SetRowCellValue(e.RowHandle, "DonGia", 0)
                _exit = False
            Case "ChietKhauPT"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                If _exit Then Exit Select
                _exit = True
                gdvVTCT.SetRowCellValue(e.RowHandle, "ChietKhau", 0)
                _exit = False
            Case "ChietKhau"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                If _exit Then Exit Select
                _exit = True
                gdvVTCT.SetRowCellValue(e.RowHandle, "ChietKhauPT", 0)
                _exit = False
            Case "DonGia"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                gdvVTCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvVTCT.GetRowCellValue(e.RowHandle, "DonGia") * gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong"), 2))

                If _exit Then Exit Select
                _exit = True
                gdvVTCT.SetRowCellValue(e.RowHandle, "GiaBanPT", 0)
                _exit = False

            Case "SoLuong"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                gdvVTCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvVTCT.GetRowCellValue(e.RowHandle, "DonGia") * gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong"), 2))
                gdvVTCT.SetRowCellValue(e.RowHandle, "TTGiaNhap", Math.Round(gdvVTCT.GetRowCellValue(e.RowHandle, "GiaNhapGanNhat") * gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong"), 2))
            Case "SLTach"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                If _exit Then Exit Select
                If e.Value > gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong") Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong"))
                End If

        End Select
    End Sub

#End Region

#Region "Tính giá"

    Private Sub btGiaBanBuon_Click(sender As System.Object, e As System.EventArgs)
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "GiaBanPT", gdvVTCT.GetRowCellValue(i, "GiaNhap") + gdvVTCT.GetRowCellValue(i, "GiaBanBuon"))
            gdvVTCT.SetRowCellValue(i, "DonGia", 0)
        Next
    End Sub

    Private Sub btGiaBanLe_Click(sender As System.Object, e As System.EventArgs)
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "GiaBanPT", gdvVTCT.GetRowCellValue(i, "GiaNhap") + gdvVTCT.GetRowCellValue(i, "GiaBanLe"))
            gdvVTCT.SetRowCellValue(i, "DonGia", 0)
        Next
    End Sub

    Private Sub cbTienTe_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTienTe.EditValueChanged
        If _exit = True Then Exit Sub
        If cbTienTe.EditValue Is Nothing Then Exit Sub
        Dim edit As LookUpEdit = CType(sender, LookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow

        If ShowCauHoi("Tỷ tệ chào giá đã thay đổi, bạn có muốn tính lại đơn giá theo tỷ giá mới ?") Then
            _exit = True
            With gdvVTCT
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "DonGia") <> 0 Then
                        Dim _DonGia As Double = 0
                        _DonGia = (.GetRowCellValue(i, "DonGia") * tbTyGia.EditValue) / CType(dr("TyGia"), Double)
                        .SetRowCellValue(i, "DonGia", _DonGia)
                    End If

                    If .GetRowCellValue(i, "ChietKhauPT") = 0 And .GetRowCellValue(i, "ChietKhau") > 0 Then
                        .SetRowCellValue(i, "ChietKhauPT", Math.Round((.GetRowCellValue(i, "ChietKhau") / .GetRowCellValue(i, "DonGia")) * 100, 2))
                    ElseIf .GetRowCellValue(i, "ChietKhauPT") > 0 And .GetRowCellValue(i, "ChietKhau") = 0 Then
                        .SetRowCellValue(i, "ChietKhau", Math.Round((.GetRowCellValue(i, "ChietKhauPT") / 100) * .GetRowCellValue(i, "DonGia"), 2))
                    Else
                        .SetRowCellValue(i, "ChietKhauPT", Math.Round((.GetRowCellValue(i, "ChietKhau") / .GetRowCellValue(i, "DonGia")) * 100, 2))
                    End If

                Next
            End With

            _exit = False
            tbTyGia.EditValue = dr("TyGia")
        End If
    End Sub

    Private Sub btTinhToan_Click(sender As System.Object, e As System.EventArgs)
        '===========Tính đơn giá và Chiết khấu
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        _exit = True


        With CType(gdvVT.Views.Item(0).DataSource, DataView).Table
            For i As Integer = 0 To .Rows.Count - 1

                Dim _DonGia As Double = 0
                If .Rows(i)("DonGia") = 0 And .Rows(i)("GiaBanPT") = 0 Then

                    .Rows(i)("GiaBanPT") = .Rows(i)("GiaNhap") + .Rows(i)("GiaBanLe")
                    _DonGia = .Rows(i)("GiaList") * (.Rows(i)("GiaBanPT") / 100)
                    _DonGia *= .Rows(i)("TyGia")
                    If cbTienTe.EditValue > TienTe.VND Then
                        _DonGia /= tbTyGia.EditValue
                    End If
                    .Rows(i)("DonGia") = Math.Round(_DonGia, 2)

                ElseIf .Rows(i)("DonGia") = 0 And .Rows(i)("GiaBanPT") <> 0 Then
                    _DonGia = .Rows(i)("GiaList") * (.Rows(i)("GiaBanPT") / 100)
                    _DonGia *= .Rows(i)("TyGia")
                    If cbTienTe.EditValue > TienTe.VND Then
                        _DonGia /= tbTyGia.EditValue
                    End If
                    .Rows(i)("DonGia") = Math.Round(_DonGia, 2)
                Else
                    If .Rows(i)("GiaList") = 0 Then
                        .Rows(i)("GiaBanPT") = 0
                    Else
                        Dim _GiaBanPT As Double = 0
                        If Convert.ToByte(.Rows(i)("TienTe")) > Convert.ToByte(cbTienTe.EditValue) Then
                            _GiaBanPT = .Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)
                        Else
                            _GiaBanPT = .Rows(i)("DonGia") / .Rows(i)("GiaList")
                        End If
                        .Rows(i)("GiaBanPT") = Math.Round((_GiaBanPT) * 100, 2)
                    End If
                End If

                If .Rows(i)("DonGia") < .Rows(i)("GiaNhapGanNhat") Then
                    .Rows(i)("LoiGia") = 1
                Else
                    .Rows(i)("LoiGia") = 0
                End If

                ' End If

                .Rows(i)("ThanhTien") = Math.Round(.Rows(i)("SoLuong") * .Rows(i)("DonGia"), 2)

                If .Rows(i)("ChietKhauPT") = 0 And .Rows(i)("ChietKhau") > 0 Then
                    .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100, 2)
                ElseIf .Rows(i)("ChietKhauPT") > 0 And .Rows(i)("ChietKhau") = 0 Then
                    .Rows(i)("ChietKhau") = Math.Round((.Rows(i)("ChietKhauPT") / 100) * .Rows(i)("DonGia"), 2)
                Else
                    .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100, 2)
                End If

            Next
        End With


        _exit = False


        tbTienTruocThue.EditValue = 0
        tbTienThue.EditValue = 0
        tbChietKhau.EditValue = 0

        With gdvVTCT
            For i As Integer = 0 To gdvVTCT.RowCount - 1
                tbTienTruocThue.EditValue += .GetRowCellValue(i, "ThanhTien")
                tbChietKhau.EditValue += (.GetRowCellValue(i, "ChietKhau") * .GetRowCellValue(i, "SoLuong"))
                If Convert.ToBoolean(.GetRowCellValue(i, "XuatThue")) Then
                    tbTienThue.EditValue += (.GetRowCellValue(i, "MucThue") / 100) * .GetRowCellValue(i, "ThanhTien")
                End If
            Next
        End With


        tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
        tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
        tbThucCK.EditValue = tbChietKhau.EditValue - tbKhauTru.EditValue
        tbThucThu.EditValue = tbTienSauThue.EditValue - tbThucCK.EditValue
    End Sub

#End Region

#Region "Thay đổi trạng thái"
    Private Sub cbTrangThai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTrangThai.EditValueChanged
        If cbTrangThai.EditValue = TrangThaiChaoGia.DaXacNhan Then
            tbNgayNhan.Enabled = True
            tbNgayGiao.Enabled = True
            tbNgayHuy.Enabled = False
        ElseIf cbTrangThai.EditValue = TrangThaiChaoGia.ChoXacNhan Then
            tbNgayNhan.Enabled = False
            tbNgayGiao.Enabled = False
            tbNgayHuy.Enabled = False
        ElseIf cbTrangThai.EditValue > TrangThaiChaoGia.DaXacNhan Then
            tbNgayHuy.Enabled = True
            tbNgayNhan.Enabled = False
            tbNgayGiao.Enabled = False
        End If

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "TrangThai", cbTrangThai.EditValue)
        Next
    End Sub

    Private Sub tbNgayNhan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbNgayNhan.EditValueChanged
        If _exit Then Exit Sub
        If Not tbNgayNhan.EditValue Is Nothing Then
            tbNgayGiao.EditValue = DateAdd(DateInterval.Day, tbSoNgayGiao.EditValue, tbNgayNhan.EditValue)
        End If
    End Sub

    Private Sub tbSoNgayGiao_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbSoNgayGiao.EditValueChanged
        If _exit Then Exit Sub
        If Not tbNgayNhan.EditValue Is Nothing Then
            tbNgayGiao.EditValue = DateAdd(DateInterval.Day, tbSoNgayGiao.EditValue, tbNgayNhan.EditValue)
        End If
    End Sub
#End Region

#Region "Xuất Excel"
    Private Sub btXuatExcel_Click(sender As System.Object, e As System.EventArgs) Handles btXuatExcel.Click
        btXuatExcel.ShowDropDown()
    End Sub

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        XuatExcel.CreateExcelFileChaoGia(tbSoPhieu.EditValue, chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, chkN0.Checked, chkCongTrinh.Checked, gdvMaKH.Text)
    End Sub
#End Region

    Private Sub tbGiaBanPT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaBanPT.ButtonClick
        _exit = True
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "GiaBanPT", tbGiaBanPT.EditValue)
            gdvVTCT.SetRowCellValue(i, "DonGia", 0)
        Next
        _exit = False
    End Sub

    Private Sub tbChietKhauPT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbChietKhauPT.ButtonClick
        _exit = True
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "ChietKhauPT", tbChietKhauPT.EditValue)
            gdvVTCT.SetRowCellValue(i, "ChietKhau", 0)
        Next
        _exit = False
    End Sub



    Private Sub cbTienTe_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTienTe.Properties.ButtonClick
        If e.Button.Index = 1 Then
            LayTyGia()
            loadTienTe()
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

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If tbSoPhieu.EditValue = "" Then
            ShowCanhBao("Cần phải lưu lại chào giá trước khi chọn file đính kèm")
            Exit Sub
        End If
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(RootUrl & UrlKinhDoanh & gdvMaKH.Text) Then
                System.IO.Directory.CreateDirectory(RootUrl & UrlKinhDoanh & gdvMaKH.Text)
            End If
            For Each file In openFile.FileNames
                Try
                    path = "YC" & SPYeuCau & " CG" & tbSoPhieu.EditValue & " KD " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            CloseWaiting()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        Try
            If ShowCauHoi("Xoá file được chọn ?") Then
                System.IO.File.Delete(RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                gdvListFileCT.DeleteSelectedRows()
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            Dim psi As New ProcessStartInfo()
            With psi
                .FileName = RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & e.CellValue
                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btFileDinhKem_Click(sender As System.Object, e As System.EventArgs) Handles btFileDinhKem.Click
        gListFileCT.Visible = True
    End Sub


    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvVTCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "LoiGia" Then
            If e.CellValue = 1 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub


    Private Sub btInBangKe_Click(sender As System.Object, e As System.EventArgs)
        If tbSoPhieu.EditValue = "" Then
            ShowCanhBao("Chào giá chưa được lưu !")
            Exit Sub
        End If
        printFile.CongTrinh.BangKeVatTu(tbSoPhieu.EditValue)
    End Sub

    Private Sub btAnDSFile_Click(sender As System.Object, e As System.EventArgs) Handles btAnDSFile.Click
        gListFileCT.Visible = False
    End Sub

    Private Sub btFileLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btFileLienQuan.Click
        Dim f As New frmFileLienQuan
        f.Tag = Me.Tag
        f.SoChaoGia = SPChaoGia
        f.SoYeuCau = SPYeuCau
        f.MaKH = gdvMaKH.Text
        f.ShowDialog()
    End Sub
End Class