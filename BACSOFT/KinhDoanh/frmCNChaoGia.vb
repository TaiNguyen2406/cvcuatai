Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Xml
Imports System.Globalization
Imports BACSOFT.Utils

Public Class frmCNChaoGia
    Public TrangThaiCG As New Utils.TrangThai
    Public tmpTrangThai As New Utils.TrangThai
    Public tmpMaTuDien As Object
    Public SPYeuCau As Object
    Public SPYeuCau2 As Object
    Private EndSelect As Boolean
    Private Move_Next As Boolean
    Public SPChaoGia As Object
    Public IDYC As String = ""
    Private _exit As Boolean

    Private Sub frmCNChaoGia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        loadTienTe()
        loadDSDVT()
        loadCbHinhThucTT()
        loadKhachHang()
        loadNguoiGD()
        loadTakeCare()
        LoadCbTrangThai()
        loadDSThoiGianGH()
        loadTKNganHang()
        loadGdvVatTuChaoGia()
        loadDSKH()
        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        chkLocKhoBan.Checked = True


        If TrangThai.isAddNew Then
            Me.Text = "Lập chào giá "
            'tbSoNgayGiao.EditValue = 
            tbNgayNhan.EditValue = GetServerTime.Date
            chkPub.Checked = True
        ElseIf TrangThai.isUpdate Then
            Me.Text = "Cập nhật chào giá "
            btXuatExcel.Enabled = True
        Else
            Me.Text = "Tạo chào giá mới từ chào giá có sẵn "

            'tbNgayGiao.EditValue = DBNull.Value
            _exit = True
            tbSoNgayGiao.EditValue = 3
            _exit = False
            tbNgayHuy.EditValue = DBNull.Value

            tbNgayNhan.EditValue = GetServerTime.Date
            ' _exit = False
            For i As Integer = 0 To gdvVTCT.RowCount - 1
                gdvVTCT.SetRowCellValue(i, "TrangThai", TrangThaiChaoGia.ChoXacNhan)
            Next
        End If
        GroupControl2.Focus()
        isShowing = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            cbPhuTrachCT.Enabled = False
            'btCNDuLieuChung.Visible = False
        Else
            cbPhuTrachCT.Enabled = True
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.KeToan) Then
            btCNDuLieuChung.Visible = False
        End If

        If chkCongTrinh.Checked Then
            splitChiTiet.Collapsed = False
            tabHangMucKhac.PageVisible = True
        Else
            splitChiTiet.Collapsed = True
            tbTenCongTrinh.Enabled = False
            tabHangMucKhac.PageVisible = False
        End If

        ''Tai
        'If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '    cbHinhThucTT2.Enabled = True
        'Else
        '    cbHinhThucTT2.Enabled = False
        'End If





    End Sub

#Region "Load dữ liệu cho các combobox"


    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            rcbTienTe.DataSource = tb
            cbTienTeCT.DataSource = tb
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
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM TAIKHOAN WHERE LoaiTK <> N'CN'")
        If Not tb Is Nothing Then
            cbTKNganHang.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbTKNganHang.EditValue = tb.Rows(0)(0)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSKH()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa")
        If Not tb Is Nothing Then
            rcbFilterKH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadKhachHang()
        'Tai
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,IDHinhThucTT,IDTakecare,IDHinhThucTT2 FROM KHACHHANG")
        'Tai
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
        Dim sql As String = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        If TrangThaiCG.isAddNew Then
            sql &= " AND TrangThai=1"
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbTakeCare.Properties.DataSource = tb
            cbNVKyHD.Properties.DataSource = tb
            cbPhuTrachCT.Properties.DataSource = tb
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
        'Tai
        cbHinhThucTT2.EditValue = dr("IDHinhThucTT2")
        'Tai
        cbTakeCare.EditValue = dr("IDTakecare")
        cbNguoiGD.Focus()
    End Sub

    Private Sub LoadCbTrangThai()
        AddParameter("@Loai", LoaiTuDien.TrangThaiChaoGia)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            rcbTrangThaiChiTiet.DataSource = tb
            cbTrangThai.Properties.DataSource = tb
            If TrangThai.isAddNew Or TrangThai.isCopy Then
                cbTrangThai.EditValue = tb.Rows(0)("Ma")
            End If
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
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT  IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
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
            sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE ID=-1"

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

        Dim sql As String = "Select TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= "((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS slTon, "
        sql &= "(select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
        sql &= "Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
        sql &= "Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        sql &= "ISNULL(VATTU.Xuatthue1,0)Xuatthue,ISNULL(VATTU.Mucthue1,0)Mucthue, TonNCC,"
        sql &= "ISNULL(VATTU.Gianhap1,0)Gianhap, ISNULL(tblTienTe.Ten,'VND') AS TenTienTe,ISNULL(tblTienTe.TyGia,1)TyGia,ISNULL(VATTU.Tiente1,0) AS TienTe,"
        sql &= "TENNUOC.Ten AS Xuatxu,Convert(float,0) AS SLYC, VATTU.HangTon, VATTU.ThongDung,(convert(image,NULL))HienThi,VATTU.HinhAnh,VATTU.TaiLieu,VATTU.ConSX,VATTU.Bo "
        sql &= "FROM VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        sql &= "LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
        sql &= "LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "

        If Not btFilterKH.EditValue Is Nothing Then
            sql &= " INNER JOIN (SELECT XUATKHO.ID,XUATKHO.IDVattu,PHIEUXUATKHO.IDKhachhang FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu)tbXK ON VATTU.ID=tbXK.IDVattu AND tbXK.IDKhachhang = " & btFilterKH.EditValue
        End If

        If Not btFilterMaVT.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
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

        If btFilterThongSo.EditValue <> "" Then
            sqlWhere &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%'"
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
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub


    Public Sub ThemVatTuChaoGia()
        Dim sql As String = ""
        sql &= " SELECT (0)AZ, (SELECT NULL)IDCG, YEUCAUDEN.IDVattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,YEUCAUDEN.Soluong AS SoLuong,YEUCAUDEN.ID AS IDYeuCau,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        sql &= " Convert(bit,0) AS XuatThue,ISNULL(VATTU.Mucthue1,10) AS MucThue,ISNULL(VATTU.Gianhap1,100) AS GiaNhap,(SELECT NULL)IDTGGiaoHang,(SELECT 0.0) TTGiaNhap,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=YEUCAUDEN.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=YEUCAUDEN.IDVattu)) AS slTon,"
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= YEUCAUDEN.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= YEUCAUDEN.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA where IDVattu= YEUCAUDEN.IDVattu) AS CanXuat,"
        sql &= " (SELECT 1)TrangThai,"
        sql &= " (SELECT 0.0)DonGiaGoc,(SELECT 0.0)DonGia,ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = YEUCAUDEN.IDVatTu ORDER BY SoPhieu DESC),0) GiaNhapGanNhat, "
        sql &= " (SELECT 0.0)ThanhTien,(SELECT 100.0)GiaBanPT,(SELECT 0.0)ChietKhauPT,(SELECT 0.0)ChietKhau,"
        sql &= " VATTU.Tiente1 AS TienTe,ISNULL(tblTienTe.TyGia,1)TyGia,VATTU.HangTon, (SELECT 0) LoiGia,(Case ISNULL(YEUCAUDEN.IDTienTeCungUng,0) WHEN 0 THEN YEUCAUDEN.GiaCungUng ELSE YEUCAUDEN.GiaCungUng * TIENTECUNGUNG.TyGia END)GiaCungUng,TIENTECUNGUNG.Ten AS TienTeCungUng,THOIGIANCUNGUNG.NoiDung AS TGCungUng"
        sql &= " ,YEUCAUDEN.IDBo,YEUCAUDEN.SLBo,tblGhepVatTu.SoLuong AS SLTrongBo,"
        sql &= " (SELECT VATTU.Model FROM VATTU WHERE VATTU.ID=YEUCAUDEN.IDBo) AS TenBoVT"
        sql &= " FROM YEUCAUDEN LEFT OUTER JOIN VATTU ON YEUCAUDEN.IDVattu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " LEFT OUTER JOIN tblTienTe AS TIENTECUNGUNG ON YEUCAUDEN.IDTienTeCungUng=TIENTECUNGUNG.ID"
        sql &= " LEFT OUTER JOIN tblTuDien AS THOIGIANCUNGUNG ON YEUCAUDEN.TGCungUng=THOIGIANCUNGUNG.Ma AND Loai=4"
        sql &= " LEFT JOIN tblGhepVatTu ON YEUCAUDEN.IDBo= tblGhepVatTu.IDVatTu AND YEUCAUDEN.IDVatTu=tblGhepVatTu.IDVatTuPhu"
        sql &= " WHERE YEUCAUDEN.Sophieu=@SP AND YEUCAUDEN.ID IN " & IDYC
        sql &= " ORDER BY YEUCAUDEN.ID "
        AddParameterWhere("@SP", SPYeuCau2)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            With dt
                For i As Integer = 0 To .Rows.Count - 1
                    gdvVTCT.AddNewRow()
                    gdvVTCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 2, "AZ")) + 1)
                    gdvVTCT.SetFocusedRowCellValue("IDYeuCau", .Rows(i)("IDYeuCau"))

                    gdvVTCT.SetFocusedRowCellValue("TrangThai", cbTrangThai.EditValue)
                    gdvVTCT.SetFocusedRowCellValue("IDVatTu", .Rows(i)("IDVatTu"))
                    gdvVTCT.SetFocusedRowCellValue("DonGia", 0)
                    gdvVTCT.SetFocusedRowCellValue("GiaBanPT", 0)
                    If IsDBNull(tbNgay.EditValue) Then
                        tbNgay.EditValue = Nothing
                    End If
                    Dim GiaGanNhat As Double = 0
                    'LayGiaNhapGanNhat(.Rows(i)("IDVatTu"), tbNgay.EditValue)
                    AddParameterWhere("@VatTu", .Rows(i)("IDVatTu"))
                    AddParameterWhere("@ThoiGian", Convert.ToDateTime(tbNgay.EditValue).Date)
                    Dim tb As DataTable = ExecuteSQLDataTable("SELECT dbo.LayGiaNhap(@VatTu,@ThoiGian)")
                    If Not tb Is Nothing Then
                        GiaGanNhat = tb.Rows(0)(0)
                    End If
                    If GiaGanNhat = 0 Then
                        GiaGanNhat = (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue
                    End If
                    gdvVTCT.SetFocusedRowCellValue("GiaNhapGanNhat", GiaGanNhat)
                    gdvVTCT.SetFocusedRowCellValue("TTGiaNhap", 0)
                    gdvVTCT.SetFocusedRowCellValue("ChietKhau", 0)
                    gdvVTCT.SetFocusedRowCellValue("ChietKhauPT", 0)
                    gdvVTCT.SetFocusedRowCellValue("ThanhTien", 0)
                    gdvVTCT.SetFocusedRowCellValue("TenVT", .Rows(i)("TenVT"))
                    gdvVTCT.SetFocusedRowCellValue("ThongSo", .Rows(i)("Thongso"))
                    gdvVTCT.SetFocusedRowCellValue("Model", .Rows(i)("Model"))
                    gdvVTCT.SetFocusedRowCellValue("SoLuong", .Rows(i)("SoLuong"))
                    gdvVTCT.SetFocusedRowCellValue("TenDVT", .Rows(i)("TenDVT"))
                    gdvVTCT.SetFocusedRowCellValue("TenHang", .Rows(i)("TenHang"))
                    gdvVTCT.SetFocusedRowCellValue("slTon", .Rows(i)("slTon"))
                    gdvVTCT.SetFocusedRowCellValue("DangVe", .Rows(i)("DangVe"))
                    gdvVTCT.SetFocusedRowCellValue("NgayVe", .Rows(i)("NgayVe"))
                    gdvVTCT.SetFocusedRowCellValue("CanXuat", .Rows(i)("CanXuat"))
                    gdvVTCT.SetFocusedRowCellValue("GiaList", .Rows(i)("GiaList"))
                    gdvVTCT.SetFocusedRowCellValue("GiaBanBuon", .Rows(i)("GiaBanBuon"))
                    gdvVTCT.SetFocusedRowCellValue("GiaBanLe", .Rows(i)("GiaBanLe"))
                    gdvVTCT.SetFocusedRowCellValue("GiaNhap", .Rows(i)("GiaNhap"))
                    gdvVTCT.SetFocusedRowCellValue("TienTe", .Rows(i)("TienTe"))
                    gdvVTCT.SetFocusedRowCellValue("TyGia", .Rows(i)("TyGia"))
                    gdvVTCT.SetFocusedRowCellValue("XuatThue", .Rows(i)("XuatThue"))
                    gdvVTCT.SetFocusedRowCellValue("MucThue", .Rows(i)("MucThue"))
                    gdvVTCT.SetFocusedRowCellValue("HangTon", .Rows(i)("HangTon"))
                    gdvVTCT.SetFocusedRowCellValue("GiaCungUng", .Rows(i)("GiaCungUng"))
                    gdvVTCT.SetFocusedRowCellValue("TienTeCungUng", .Rows(i)("TienTeCungUng"))
                    gdvVTCT.SetFocusedRowCellValue("TGCungUng", .Rows(i)("TGCungUng"))
                    gdvVTCT.SetFocusedRowCellValue("LoiGia", 0)
                    gdvVTCT.CloseEditor()
                    gdvVTCT.UpdateCurrentRow()
                Next
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadGdvVatTuChaoGia()
        Dim sql As String = ""
        If TrangThai.isAddNew Then
            sql &= " SELECT (0)AZ, (SELECT NULL)IDCG, YEUCAUDEN.IDVattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
            sql &= " TENDONVITINH.Ten AS TenDVT,YEUCAUDEN.Soluong AS SoLuong,YEUCAUDEN.ID AS IDYeuCau,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
            sql &= " Convert(bit,0) AS XuatThue,ISNULL(VATTU.Mucthue1,10) AS MucThue,ISNULL(VATTU.Gianhap1,100) AS GiaNhap,(SELECT NULL)IDTGGiaoHang,(SELECT 0.0) TTGiaNhap,"
            sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=YEUCAUDEN.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=YEUCAUDEN.IDVattu)) AS slTon,"
            sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= YEUCAUDEN.IDVattu) AS DangVe,"
            sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= YEUCAUDEN.IDVattu) AS NgayVe,"
            sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA where IDVattu= YEUCAUDEN.IDVattu) AS CanXuat,"
            sql &= " (SELECT 1)TrangThai,"
            sql &= " (SELECT 0.0)DonGiaGoc,(SELECT 0.0)DonGia,ISNULL((SELECT TOP 1 NHAPKHO.DonGia*PHIEUNHAPKHO.TyGia FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu WHERE NHAPKHO.IDVatTu=YEUCAUDEN.IDVattu ORDER BY PHIEUNHAPKHO.Ngaythang DESC),0) GiaNhapGanNhat, "
            sql &= " (SELECT 0.0)ThanhTien,(SELECT 100.0)GiaBanPT,(SELECT 0.0)ChietKhauPT,(SELECT 0.0)ChietKhau,"
            sql &= " ISNULL(VATTU.Tiente1,0) AS TienTe,ISNULL(tblTienTe.TyGia,1)TyGia,VATTU.HangTon, (SELECT 0) LoiGia,(Case ISNULL(YEUCAUDEN.IDTienTeCungUng,0) WHEN 0 THEN YEUCAUDEN.GiaCungUng ELSE YEUCAUDEN.GiaCungUng * TIENTECUNGUNG.TyGia END)GiaCungUng,TIENTECUNGUNG.Ten AS TienTeCungUng,THOIGIANCUNGUNG.NoiDung AS TGCungUng,VATTU.GiaBan1 AS PTBL,VATTU.GiaNCC1 AS PTBB"
            sql &= " ,YEUCAUDEN.IDBo,YEUCAUDEN.SLBo,tblGhepVatTu.SoLuong AS SLTrongBo,"
            sql &= " (SELECT VATTU.Model FROM VATTU WHERE VATTU.ID=YEUCAUDEN.IDBo) AS TenBoVT,N'' as GhiChu"
            sql &= " FROM YEUCAUDEN LEFT OUTER JOIN VATTU ON YEUCAUDEN.IDVattu=VATTU.ID"
            sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
            sql &= " LEFT OUTER JOIN tblTienTe AS TIENTECUNGUNG ON YEUCAUDEN.IDTienTeCungUng=TIENTECUNGUNG.ID"
            sql &= " LEFT JOIN tblGhepVatTu ON YEUCAUDEN.IDBo= tblGhepVatTu.IDVatTu AND YEUCAUDEN.IDVatTu=tblGhepVatTu.IDVatTuPhu AND YEUCAUDEN.IDVatTu=tblGhepVatTu.IDVatTuPhu"
            sql &= " LEFT OUTER JOIN tblTuDien AS THOIGIANCUNGUNG ON YEUCAUDEN.TGCungUng=THOIGIANCUNGUNG.Ma AND Loai=4"
            If IDYC <> "()" Then
                sql &= " WHERE YEUCAUDEN.Sophieu=@SP AND YEUCAUDEN.ID IN " & IDYC
            Else
                sql &= " WHERE YEUCAUDEN.Sophieu='-1' "
            End If

            sql &= " ORDER BY YEUCAUDEN.ID "

            sql &= " SELECT ID AS IDCGAUX,(0)AZ,Sophieu AS SoPhieu,Noidung AS NoiDung,HangSx AS HangSX, Donvi AS MaDVT,"
            sql &= " SoLuong, DonGia, MucThue, XuatThue,ChietKhau,(SELECT 0.0)ChietKhauPT, (SELECT 0.0)ThanhTien"
            sql &= " FROM CHAOGIAAUX"
            sql &= " WHERE Sophieu='-1'"

            If IDYC <> "()" Then
                AddParameterWhere("@SP", SPYeuCau)
            End If

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                With ds.Tables(0)
                    For i As Integer = 0 To .Rows.Count - 1
                        .Rows(i)("AZ") = i + 1
                        If IsDBNull(.Rows(i)("GiaNhapGanNhat")) Then .Rows(i)("GiaNhapGanNhat") = 0
                        If .Rows(i)("GiaNhapGanNhat") = 0 Then
                            '  if
                            .Rows(i)("GiaNhapGanNhat") = .Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")
                        End If
                        .Rows(i)("TTGiaNhap") = .Rows(i)("SoLuong") * .Rows(i)("GiaNhapGanNhat")
                        'If .Rows(i)("DonGia") < .Rows(i)("GiaNhapGanNhat") Then .Rows(i)("LoiGia") = 1
                    Next
                End With

                With ds.Tables(1)
                    For i As Integer = 0 To .Rows.Count - 1
                        .Rows(i)("AZ") = i + 1
                    Next
                End With

                gdvVT.DataSource = ds.Tables(0)
                gdvCongTrinh.DataSource = ds.Tables(1)
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
            ktCoSan()
            gdvListFile.DataSource = DataSourceDSFile("")
        Else
            sql &= " SELECT Ngaythang, IDKhachhang,TenDuan,TienTruocthue,Tienthue,TienChietkhau,Tienthucthu,Ngaynhan,Ngaygiao,IDHinhThucTT,MaSoDatHang,NhanKS,IDPhuTrachKyHD,IDPhuTrachCT,"
            sql &= " ISNULL(NgayGiaoDuKien,0)NgayGiaoDuKien,Ngayhuy,TyGia,IDUser,IDNgd,IDtakecare,Tiente,Congtrinh,Trangthai,Khautru,IDTaiKhoan,FileDinhkem,Pub,SoPO"
            'Tai
            sql &= " , IDHinhThucTT2"
            'Tai
            sql &= " FROM BANGCHAOGIA"
            sql &= " WHERE Sophieu=@SP"

            sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
            sql &= " TENDONVITINH.Ten AS TenDVT,CHAOGIA.SoLuong,CHAOGIA.IDYeucau AS IDYeuCau,(SELECT 0.0) TTGiaNhap,"
            sql &= " (CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien, "
            sql &= " ISNULL(ISNULL("
            sql &= "        (SELECT     TOP (1) Gianhap"
            sql &= "            FROM V_GiaNhap "
            sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
            sql &= "            ORDER BY Ngaythang DESC),"
            sql &= "        (SELECT     TOP (1) Gianhap"
            sql &= "            FROM V_GiaNhap"
            sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
            sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia) AS GiaNhapGanNhat,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
            sql &= " CHAOGIA.Xuatthue AS XuatThue,CHAOGIA.Mucthue AS MucThue,ISNULL(VATTU.Gianhap1,100) AS GiaNhap,IDTGGiaoHang,"
            sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
            sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
            sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"
            sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS CanXuat,CHAOGIA.TrangThai,CHAOGIA.Dongia AS DonGia,Convert(float,0) as GiaBanPT,"
            sql &= " CHAOGIA.Chietkhau AS ChietKhau,Convert(float,0) as ChietKhauPT,"
            sql &= " ISNULL(VATTU.Tiente1,0) AS TienTe, (CASE ISNULL(VATTU.Tiente1,0) WHEN 0 THEN 1 ELSE ISNULL(CHAOGIA.TyGia,ISNULL(tblTienTe.TyGia,1)) END ) TyGia, VATTU.HangTon , ISNULL(CHAOGIA.AZ,0)AZ, (SELECT 0) LoiGia,(Case ISNULL(YEUCAUDEN.IDTienTeCungUng,0) WHEN 0 THEN YEUCAUDEN.GiaCungUng ELSE YEUCAUDEN.GiaCungUng * TIENTECUNGUNG.TyGia END)GiaCungUng,TIENTECUNGUNG.Ten AS TienTeCungUng,THOIGIANCUNGUNG.NoiDung AS TGCungUng,VATTU.GiaBan1 AS PTBL,VATTU.GiaNCC1 AS PTBB"
            sql &= " ,CHAOGIA.IDBo,CHAOGIA.SLBo,tblGhepVatTu.SoLuong as SLTrongBo,"
            sql &= " (SELECT VATTU.Model FROM VATTU WHERE VATTU.ID=CHAOGIA.IDBo) AS TenBoVT,CHAOGIA.GhiChu"
            sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
            sql &= " LEFT OUTER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=@SP"
            sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
            sql &= " LEFT JOIN YEUCAUDEN ON CHAOGIA.IDYeuCau=YEUCAUDEN.ID"
            sql &= " LEFT JOIN tblGhepVatTu ON CHAOGIA.IDBo= tblGhepVatTu.IDVatTu AND CHAOGIA.IDVatTu=tblGhepVatTu.IDVatTuPhu"
            sql &= " LEFT OUTER JOIN tblTienTe AS TIENTECUNGUNG ON YEUCAUDEN.IDTienTeCungUng=TIENTECUNGUNG.ID"
            sql &= " LEFT OUTER JOIN tblTuDien AS THOIGIANCUNGUNG ON YEUCAUDEN.TGCungUng=THOIGIANCUNGUNG.Ma AND Loai=4"
            sql &= " WHERE CHAOGIA.Sophieu= (CASE WHEN BANGCHAOGIA.CongTrinh=1 THEN convert(nvarchar, @SP) + 'CT' ELSE @SP END)  "

            'If chkCongTrinh.Checked Then
            '    sql &= " WHERE CHAOGIA.Sophieu=N'" & SPChaoGia & "CT'"
            'Else
            '    sql &= " WHERE CHAOGIA.Sophieu=N'" & SPChaoGia & "'"
            'End If

            sql &= " ORDER BY AZ "

            sql &= " SELECT CHAOGIAAUX.ID AS IDCGAUX,CHAOGIAAUX.Sophieu AS SoPhieu,Noidung AS NoiDung,HangSx AS HangSX, Donvi AS MaDVT,"
            sql &= " Soluong AS SoLuong, ISNULL(Dongia,0) AS DonGia, "
            sql &= " Mucthue AS MucThue,ISNULL(Xuatthue,0) AS XuatThue,Chietkhau AS ChietKhau,Convert(float,0) as ChietKhauPT,"
            sql &= " (ISNULL(Dongia,0)*ISNULL(Soluong,0))ThanhTien, ISNULL(CHAOGIAAUX.AZ,0)AZ "
            sql &= " FROM CHAOGIAAUX "
            sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=@SP"
            sql &= " WHERE CHAOGIAAUX.Sophieu= (CASE WHEN BANGCHAOGIA.CongTrinh=1 THEN convert(nvarchar, @SP) + 'CT' ELSE @SP END)"
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
                        If TrangThai.isCopy = True Then .Rows(i)("IDCG") = DBNull.Value

                        .Rows(i)("AZ") = i + 1
                        If IsDBNull(.Rows(i)("GiaNhapGanNhat")) Then .Rows(i)("GiaNhapGanNhat") = 0
                        If .Rows(i)("GiaNhapGanNhat") = 0 Then
                            .Rows(i)("GiaNhapGanNhat") = (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue
                        End If
                        .Rows(i)("TTGiaNhap") = .Rows(i)("SoLuong") * .Rows(i)("GiaNhapGanNhat")
                        If .Rows(i)("DonGia") < .Rows(i)("GiaNhapGanNhat") Then .Rows(i)("LoiGia") = 1

                        If .Rows(i)("GiaList") = 0 Then
                            .Rows(i)("GiaBanPT") = 0
                        Else
                            If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                                .Rows(i)("GiaBanPT") = 0
                            Else
                                If .Rows(i)("GiaList") = 0 Then
                                    .Rows(i)("GiaBanPT") = 0
                                Else
                                    If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(ds.Tables(0).Rows(0)("Tiente")) Then
                                        .Rows(i)("GiaBanPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)) * 100, 2)
                                    Else
                                        .Rows(i)("GiaBanPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)
                                    End If
                                End If

                            End If

                        End If


                        If .Rows(i)("DonGia") <> 0 Then
                            .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100, 2)
                        End If
                    Next
                End With

                With ds.Tables(2)
                    For i As Integer = 0 To .Rows.Count - 1
                        If TrangThai.isCopy = True Then .Rows(i)("IDCGAUX") = DBNull.Value
                        .Rows(i)("AZ") = i + 1
                        If .Rows(i)("DonGia") = 0 Then
                            .Rows(i)("ChietKhauPT") = 0
                        Else
                            .Rows(i)("ChietKhauPT") = (.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100
                        End If
                    Next
                End With

                gdvVT.DataSource = ds.Tables(1)

                gdvMaKH.EditValue = ds.Tables(0).Rows(0)("IDKhachhang")
                If TrangThai.isUpdate Then
                    tbSoPhieu.EditValue = SPChaoGia
                    tbNgay.EditValue = ds.Tables(0).Rows(0)("Ngaythang")
                End If
                cbHinhThucTT.EditValue = ds.Tables(0).Rows(0)("IDHinhThucTT")
                'Tai
                cbHinhThucTT2.EditValue = ds.Tables(0).Rows(0)("IDHinhThucTT2")
                'Tai
                SPYeuCau = ds.Tables(0).Rows(0)("MaSoDatHang")

                cbNguoiGD.EditValue = ds.Tables(0).Rows(0)("IDNgd")
                cbTakeCare.EditValue = ds.Tables(0).Rows(0)("IDtakecare")
                tbTenCongTrinh.EditValue = ds.Tables(0).Rows(0)("TenDuan")
                chkCongTrinh.Checked = ds.Tables(0).Rows(0)("Congtrinh")
                chkPub.Checked = ds.Tables(0).Rows(0)("Pub")
                If TrangThai.isUpdate Then
                    _exit = True
                    cbTrangThai.EditValue = ds.Tables(0).Rows(0)("Trangthai")
                    _exit = False
                End If
                _exit = True
                tbNgayNhan.EditValue = ds.Tables(0).Rows(0)("Ngaynhan")
                tbSoNgayGiao.EditValue = ds.Tables(0).Rows(0)("NgayGiaoDuKien")
                tbNgayGiao.EditValue = ds.Tables(0).Rows(0)("Ngaygiao")
                tbNgayHuy.EditValue = ds.Tables(0).Rows(0)("Ngayhuy")
                _exit = False
                If IsDBNull(ds.Tables(0).Rows(0)("NhanKS")) Then
                    cbNhanKS.SelectedIndex = -1
                Else
                    cbNhanKS.SelectedIndex = CType(ds.Tables(0).Rows(0)("NhanKS"), Integer)
                End If

                If IsDBNull(ds.Tables(0).Rows(0)("IDPhuTrachKyHD")) Then
                    cbNVKyHD.EditValue = cbTakeCare.EditValue
                Else
                    cbNVKyHD.EditValue = ds.Tables(0).Rows(0)("IDPhuTrachKyHD")
                End If
                cbPhuTrachCT.EditValue = ds.Tables(0).Rows(0)("IDPhuTrachCT")

                gdvListFile.DataSource = DataSourceDSFile(ds.Tables(0).Rows(0)("FileDinhKem").ToString)

                cbTKNganHang.EditValue = ds.Tables(0).Rows(0)("IDTaiKhoan")

                tbThucCK.EditValue = ds.Tables(0).Rows(0)("TienChietkhau")
                tbKhauTruPT.EditValue = ds.Tables(0).Rows(0)("Khautru")
                tbTienTruocThue.EditValue = ds.Tables(0).Rows(0)("TienTruocthue")
                tbTienThue.EditValue = ds.Tables(0).Rows(0)("Tienthue")
                tbThucThu.EditValue = ds.Tables(0).Rows(0)("Tienthucthu")
                tbSoPO.EditValue = ds.Tables(0).Rows(0)("SoPO")

                gdvCongTrinh.DataSource = ds.Tables(2)
                tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
                tbChietKhau.EditValue = Math.Round(tbThucCK.EditValue / ((100 - tbKhauTruPT.EditValue) / 100), 2)
                tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Public Sub LoadVTChaoGia(ByVal SoPhieu As Object)
        Dim Sql As String = ""
        Sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        Sql &= " TENDONVITINH.Ten AS TenDVT,CHAOGIA.SoLuong,CHAOGIA.IDYeucau AS IDYeuCau,(SELECT 0.0) TTGiaNhap,"
        Sql &= " (CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien, "
        Sql &= " ISNULL(ISNULL("
        Sql &= "        (SELECT     TOP (1) Gianhap"
        Sql &= "            FROM V_GiaNhap "
        Sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        Sql &= "            ORDER BY Ngaythang DESC),"
        Sql &= "        (SELECT     TOP (1) Gianhap"
        Sql &= "            FROM V_GiaNhap"
        Sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        Sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia) AS GiaNhapGanNhat,"
        Sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        Sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        Sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        Sql &= " CHAOGIA.Xuatthue AS XuatThue,CHAOGIA.Mucthue AS MucThue,ISNULL(VATTU.Gianhap1,100) AS GiaNhap,IDTGGiaoHang,"
        Sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        Sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        Sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"
        Sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS CanXuat,CHAOGIA.TrangThai,CHAOGIA.Dongia AS DonGia,Convert(float,0) as GiaBanPT,"
        Sql &= " CHAOGIA.Chietkhau AS ChietKhau,Convert(float,0) as ChietKhauPT,"
        Sql &= " ISNULL(VATTU.Tiente1,0) AS TienTe, (CASE ISNULL(VATTU.Tiente1,0) WHEN 0 THEN 1 ELSE ISNULL(CHAOGIA.TyGia,ISNULL(tblTienTe.TyGia,1)) END )TyGia, VATTU.HangTon , ISNULL(CHAOGIA.AZ,0)AZ, (SELECT 0) LoiGia,(Case ISNULL(YEUCAUDEN.IDTienTeCungUng,0) WHEN 0 THEN YEUCAUDEN.GiaCungUng ELSE YEUCAUDEN.GiaCungUng * TIENTECUNGUNG.TyGia END)GiaCungUng,TIENTECUNGUNG.Ten AS TienTeCungUng,THOIGIANCUNGUNG.NoiDung AS TGCungUng,VATTU.GiaBan1 AS PTBL,VATTU.GiaNCC1 AS PTBB"
        Sql &= " ,CHAOGIA.IDBo,CHAOGIA.SLBo,tblGhepVatTu.SoLuong as SLTrongBo,"
        Sql &= " (SELECT VATTU.Model FROM VATTU WHERE VATTU.ID=CHAOGIA.IDBo) AS TenBoVT,CHAOGIA.GhiChu"
        Sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        Sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=CHAOGIA.Sophieu"
        Sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        Sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        Sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        Sql &= " LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        Sql &= " LEFT JOIN YEUCAUDEN ON CHAOGIA.IDYeuCau=YEUCAUDEN.ID"
        Sql &= " LEFT JOIN tblGhepVatTu ON CHAOGIA.IDBo= tblGhepVatTu.IDVatTu AND CHAOGIA.IDVatTu=tblGhepVatTu.IDVatTuPhu"
        Sql &= " LEFT JOIN tblTienTe AS TIENTECUNGUNG ON YEUCAUDEN.IDTienTeCungUng=TIENTECUNGUNG.ID"
        Sql &= " LEFT JOIN tblTuDien AS THOIGIANCUNGUNG ON YEUCAUDEN.TGCungUng=THOIGIANCUNGUNG.Ma AND Loai=4"

        If chkCongTrinh.Checked Then
            Sql &= " WHERE CHAOGIA.Sophieu=N'" & SoPhieu & "CT'"
        Else
            Sql &= " WHERE CHAOGIA.Sophieu=N'" & SoPhieu & "'"
        End If

        Sql &= " ORDER BY AZ "

        Sql &= " SELECT CHAOGIAAUX.ID AS IDCGAUX,SoPhieu,NoiDung,HangSx AS HangSX, Donvi AS MaDVT,"
        Sql &= " SoLuong, ISNULL(Dongia,0) AS DonGia, "
        Sql &= " Mucthue AS MucThue,ISNULL(Xuatthue,0) AS XuatThue,Chietkhau AS ChietKhau,Convert(float,0) as ChietKhauPT,"
        Sql &= " (ISNULL(Dongia,0)*ISNULL(Soluong,0))ThanhTien, ISNULL(CHAOGIAAUX.AZ,0)AZ "
        Sql &= " FROM CHAOGIAAUX "
        Sql &= " WHERE Sophieu=N'" & SoPhieu & "CT'"
        Sql &= " ORDER BY AZ "

        AddParameter("@SP", SoPhieu)
        Dim ds As DataSet = ExecuteSQLDataSet(Sql)
        If Not ds Is Nothing Then
            With ds.Tables(0)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If IsDBNull(.Rows(i)("GiaNhapGanNhat")) Then .Rows(i)("GiaNhapGanNhat") = 0
                    If .Rows(i)("GiaNhapGanNhat") = 0 Then
                        .Rows(i)("GiaNhapGanNhat") = (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue
                    End If
                    .Rows(i)("TTGiaNhap") = .Rows(i)("SoLuong") * .Rows(i)("GiaNhapGanNhat")
                    If .Rows(i)("DonGia") < .Rows(i)("GiaNhapGanNhat") Then .Rows(i)("LoiGia") = 1

                    If .Rows(i)("GiaList") = 0 Then
                        .Rows(i)("GiaBanPT") = 0
                    Else
                        If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                            .Rows(i)("GiaBanPT") = 0
                        Else
                            If .Rows(i)("GiaList") = 0 Then
                                .Rows(i)("GiaBanPT") = 0
                            Else
                                If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(ds.Tables(0).Rows(0)("Tiente")) Then
                                    .Rows(i)("GiaBanPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)) * 100, 2)
                                Else
                                    .Rows(i)("GiaBanPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)
                                End If
                            End If

                        End If

                    End If


                    If .Rows(i)("DonGia") <> 0 Then
                        .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100, 2)
                    End If
                Next
            End With

            With ds.Tables(1)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If .Rows(i)("DonGia") = 0 Then
                        .Rows(i)("ChietKhauPT") = 0
                    Else
                        .Rows(i)("ChietKhauPT") = (.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100
                    End If
                Next
            End With

            gdvVT.DataSource = ds.Tables(0)
            gdvCongTrinh.DataSource = ds.Tables(1)

            'tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
            'tbChietKhau.EditValue = Math.Round(tbThucCK.EditValue / ((100 - tbKhauTruPT.EditValue) / 100), 2)
            'tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
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

    Private Sub loadDSDVT()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM TENDONVITINH")
        If Not tb Is Nothing Then
            cbDVTAUX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
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
        'Tai
        riLueNhom.DataSource = TAI.tableNhomHinhThucTT()
        tb = ExecuteSQLDataTable("SELECT ID,SoTT,GiaiThich,Nhom FROM DM_HINH_THUC_TT where TrangThai=1 ORDER BY Nhom asc, SoTT, GiaiThich asc")
        If Not tb Is Nothing Then
            cbHinhThucTT2.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbHinhThucTT2.EditValue = tb.Rows(0)("ID")
                AddParameterWhere("@ID", gdvMaKH.EditValue)
                Dim HTTT_TheoKH = ExecuteSQLScalar("select IDHinhThucTT2  from KHACHHANG where ID=@ID ")
                If Not IsDBNull(HTTT_TheoKH) And HTTT_TheoKH IsNot Nothing Then
                    cbHinhThucTT2.EditValue = HTTT_TheoKH
                End If
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        'Tai
    End Sub
#End Region

#Region "Lưu lại"
    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click

        If cbHinhThucTT2.EditValue Is Nothing OrElse cbHinhThucTT2.EditValue Is DBNull.Value Then
            ShowBaoLoi("Chưa chọn hình thức thanh toán!")
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            If Not CheckHinhThucThanhToanMoiCoLoi() Then
                ShowCanhBao("Chỉ được chọn hình thức thanh toán có lợi hơn !")
                Exit Sub
            End If
        End If


        'Nếu không có quyền admin phải check xem đã sửa chào giá hay chưa
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            AddParameter("@SoPhieu", tbSoPhieu.Text)
            Dim sql As String = "select TrangThai from BANGCHAOGIA where Sophieu = @SoPhieu"
            Dim dtTrangThai As DataTable = ExecuteSQLDataTable(sql)
            If Not dtTrangThai Is Nothing AndAlso dtTrangThai.Rows.Count > 0 Then
                If dtTrangThai.Rows(0)(0) = TrangThaiChaoGia.DaXacNhan Then
                    ShowCanhBao("Không thể cập nhật nội dung chào giá trạng thái đã xác nhận !")
                    Exit Sub
                End If
            End If
        End If


        If chkPub.Checked = False Then
            If Not ShowCauHoi("Chào giá đang ở trạng thái ẩn, bạn có muốn lưu lại hay không ?") Then Exit Sub
        End If

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        gdvCongTrinhCT.CloseEditor()
        gdvCongTrinhCT.UpdateCurrentRow()
        btTinhToan.PerformClick()

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "TrangThai") = TrangThaiChaoGia.DaXacNhan Then
                If cbTrangThai.EditValue <> TrangThaiChaoGia.DaXacNhan Then
                    ShowCanhBao("Bạn phải chuyển trạng thái chào giá của toàn bộ đơn hàng sang đã xác nhận")
                    Exit Sub
                End If
            End If
        Next

        If TrangThaiCG.isAddNew Or TrangThaiCG.isCopy Then
            tbSoPhieu.EditValue = LaySoPhieu("BANGCHAOGIA")
            tbNgay.EditValue = GetServerTime()
        End If

        Try
            BeginTransaction()
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
            AddParameter("@IDHinhThucTT2", cbHinhThucTT2.EditValue)
            AddParameter("@SoPO", tbSoPO.EditValue)
            AddParameter("@FileDinhKem", StrDSFile(gdvListFileCT))

            ' AddParameter("@IDPhuTrachKyHD", cbNVKyHD.EditValue)
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                AddParameter("@IDPhuTrachCT", cbPhuTrachCT.EditValue)
            End If

            If cbNhanKS.SelectedIndex = -1 Then
                AddParameter("@NhanKS", DBNull.Value)
            Else
                AddParameter("@NhanKS", cbNhanKS.SelectedIndex)
            End If

            If cbNVKyHD.EditValue Is Nothing Then
                AddParameter("@IDPhuTrachKyHD", cbTakeCare.EditValue)
            Else
                AddParameter("@IDPhuTrachKyHD", cbNVKyHD.EditValue)
            End If

            AddParameter("@Pub", chkPub.Checked)
            If TrangThaiCG.isAddNew Or TrangThaiCG.isCopy Then
                AddParameter("@Masodathang", SPYeuCau)
                If doInsert("BANGCHAOGIA") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                'AddParameter("@SoCG", DBNull.Value)
                'AddParameterWhere("@SoYC", SPYeuCau)
                'If doUpdate("tblBaoCaoLichThiCong", "SoYC=@SoYC") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'cập nhật lại số chào giá vào bảng báo cáo lịch thi công theo số yêu cầu
                If cbTrangThai.EditValue = 2 And chkCongTrinh.Checked = True Then
                    AddParameter("@SoCG", tbSoPhieu.EditValue)
                    AddParameterWhere("@SoYC", SPYeuCau)
                    If doUpdate("tblBaoCaoLichThiCong", "SoYC=@SoYC and SoCG is null") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
               
                If TrangThaiCG.isCopy Then
                    Dim sql As String = ""
                    'sql &= " DECLARE @DoDai AS NVARCHAR(4) "
                    'sql &= " SET @DoDai = N'0000'"
                    'sql &= " DECLARE @SP AS INT"
                    'sql &= " DECLARE @SoPhieu AS NVARCHAR(8) "
                    'sql &= " DECLARE @Thang AS NVARCHAR(2)"
                    'sql &= " DECLARE @Nam AS NVARCHAR(2)"
                    'sql &= " SET @Thang = LEFT('00',2-LEN(CONVERT(NVARCHAR,DATEPART(mm,getdate())))) +  CONVERT(NVARCHAR,DATEPART(mm,getdate()))"
                    'sql &= " SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
                    'sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(Sophieu,4),0))),0)+1  "
                    'sql &= "             FROM BANGYEUCAU "
                    'sql &= "             WHERE"
                    'sql &= "		            LEFT(Sophieu,2)=@Nam AND "
                    'sql &= "		            SUBSTRING(Sophieu,3,2)=@Thang AND LEN(Sophieu)=8"
                    'sql &= "	            )"
                    'sql &= " SET @SoPhieu = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
                    'sql &= " SELECT @SoPhieu"
                    'Dim tbSP As DataTable = ExecuteSQLDataTable(sql)
                    'If tbSP Is Nothing Then
                    '    ShowBaoLoi(LoiNgoaiLe)
                    '    RollBackTransaction()
                    '    Exit Sub
                    'End If

                    'If tbSP.Rows.Count = 0 Then
                    '    ShowBaoLoi("Không lấy được số phiếu yêu cầu !")
                    '    RollBackTransaction()
                    '    Exit Sub
                    'End If

                    Dim SPYCCopy As Object = LaySoPhieu("BANGYEUCAU")

                    If SPYCCopy Is Nothing Then
                        ShowBaoLoi("Không lấy được số phiếu yêu cầu !")
                        RollBackTransaction()
                        Exit Sub
                    End If


                    sql &= " INSERT INTO BANGYEUCAU(NgayThang,SoPhieu,IDKhachHang,IDUser,IDNgd,IDTakeCare,IDModify,NoiDung,CongTrinh,TrangThai,IDLoaiYeuCau)"
                    sql &= " SELECT getdate(),'" & SPYCCopy & "',IDKhachHang," & TaiKhoan & ",IDNgd,IDTakeCare," & TaiKhoan & ",NoiDung,CongTrinh,TrangThai,IDLoaiYeuCau"
                    sql &= " FROM BANGYEUCAU WHERE SoPhieu='" & SPYeuCau & "'"
                    If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    SPYeuCau = SPYCCopy
                    AddParameter("@Masodathang", SPYeuCau)
                    AddParameterWhere("@SP", tbSoPhieu.EditValue)
                    If doUpdate("BANGCHAOGIA", "SoPhieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Else
                AddParameterWhere("@SP", tbSoPhieu.EditValue)
                If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                'cập nhật lại số chào giá vào bảng báo cáo lịch thi công theo số yêu cầu
                If cbTrangThai.EditValue = 2 And chkCongTrinh.Checked = True Then
                    AddParameter("@SoCG", tbSoPhieu.EditValue)
                    AddParameterWhere("@SoYC", SPYeuCau)
                    If doUpdate("tblBaoCaoLichThiCong", "SoYC=@SoYC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
              
            End If

            With gdvVTCT
                For i As Integer = 0 To .RowCount - 1
                    If IsDBNull(.GetRowCellValue(i, "IDVatTu")) Or .GetRowCellValue(i, "IDVatTu") Is Nothing Then Continue For
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
                        If chkCongTrinh.Checked Then
                            AddParameter("@Canxuat", 0)
                        Else
                            AddParameter("@Canxuat", .GetRowCellValue(i, "SoLuong"))
                        End If

                    Else
                        AddParameter("@Canxuat", 0)
                    End If

                    AddParameter("@Trangthai", .GetRowCellValue(i, "TrangThai"))
                    AddParameter("@IDYeucau", .GetRowCellValue(i, "IDYeuCau"))
                    AddParameter("@IDTGGiaoHang", .GetRowCellValue(i, "IDTGGiaoHang"))
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                    AddParameter("@IDBo", .GetRowCellValue(i, "IDBo"))
                    AddParameter("@SLBo", .GetRowCellValue(i, "SLBo"))
                    AddParameter("@GhiChu", .GetRowCellValue(i, "GhiChu"))
                    If IsDBNull(.GetRowCellValue(i, "IDCG")) Or .GetRowCellValue(i, "IDCG") Is Nothing Then
                        Dim _IDCG As Object = doInsert("CHAOGIA")
                        If _IDCG Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDCG", _IDCG)
                    Else
                        AddParameterWhere("@IDCG", .GetRowCellValue(i, "IDCG"))
                        If doUpdate("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                    If .GetRowCellValue(i, "SoLuong") = 0 Then
                        AddParameter("@Trangthai", TrangThaiYeuCau.CanChaoGia)
                    Else
                        AddParameter("@Trangthai", TrangThaiYeuCau.DaChaoGia)
                    End If

                    AddParameterWhere("@IDYeucau", .GetRowCellValue(i, "IDYeuCau"))

                    If doUpdate("YEUCAUDEN", "ID=@IDYeucau") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Next
            End With


            AddParameter("@TrangThai", TrangThaiYeuCau.DaChaoGia)
            AddParameterWhere("@SPYeuCau", SPYeuCau)
            If doUpdate("BANGYEUCAU", "Sophieu=@SPYeuCau") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            With gdvCongTrinhCT
                For i As Integer = 0 To .RowCount - 2
                    AddParameter("@Sophieu", tbSoPhieu.EditValue & "CT")
                    AddParameter("@Noidung", .GetRowCellValue(i, "NoiDung"))
                    AddParameter("@HangSx", .GetRowCellDisplayText(i, "HangSX"))
                    AddParameter("@Donvi", .GetRowCellValue(i, "MaDVT"))
                    AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                    AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                    AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                    AddParameter("@Xuatthue", .GetRowCellValue(i, "XuatThue"))
                    AddParameter("@Chietkhau", .GetRowCellValue(i, "ChietKhau"))
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                    If IsDBNull(.GetRowCellValue(i, "IDCGAUX")) Or .GetRowCellValue(i, "IDCGAUX") Is Nothing Then
                        Dim _IDCGAUX As Object = doInsert("CHAOGIAAUX")
                        If _IDCGAUX Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDCGAUX", _IDCGAUX)
                    Else
                        AddParameterWhere("@IDCGAUX", .GetRowCellValue(i, "IDCGAUX"))
                        If doUpdate("CHAOGIAAUX", "ID=@IDCGAUX") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next
            End With

            ComitTransaction()

            If Not chkCongTrinh.Checked Then
                For i As Integer = 0 To gdvVTCT.RowCount - 1
                    Dim sql As String = ""
                    If Not IsDBNull(gdvVTCT.GetRowCellValue(i, "IDCG")) And Not gdvVTCT.GetRowCellValue(i, "IDCG") Is Nothing Then
                        If Convert.ToByte(gdvVTCT.GetRowCellValue(i, "TrangThai")) = TrangThaiChaoGia.DaXacNhan Then
                            sql = " Update CHAOGIA Set CanXuat = (Soluong - (select (isnull(sum(soluong),0)) from XUATKHO where IDChaogia = " & gdvVTCT.GetRowCellValue(i, "IDCG") & ")) Where ID = " & gdvVTCT.GetRowCellValue(i, "IDCG")
                        Else
                            sql = " Update CHAOGIA Set CanXuat = 0 Where ID = " & gdvVTCT.GetRowCellValue(i, "IDCG")
                        End If

                        If ExecuteSQLNonQuery(sql) Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            ShowBaoLoi(sql)
                        End If
                    End If
                Next
            Else
                Dim sql As String = ""
                If Convert.ToByte(cbTrangThai.EditValue) = TrangThaiChaoGia.DaXacNhan Then
                    sql = " Update CHAOGIA Set TrangThai=2, CanXuat = (Soluong - (select (isnull(sum(soluong),0)) from XUATKHO where IDChaogia = CHAOGIA.ID)) Where CHAOGIA.SoPhieu='" & tbSoPhieu.EditValue & "'"
                Else
                    sql = " Update CHAOGIA Set TrangThai=" & cbTrangThai.EditValue & ", CanXuat = 0 Where SoPhieu = '" & tbSoPhieu.EditValue & "'"
                End If
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    ShowBaoLoi(sql)
                End If
            End If

            If chkCongTrinh.Checked Then
                If ExecuteSQLNonQuery("DELETE FROM CHAOGIA WHERE SoPhieu='" & tbSoPhieu.EditValue & "CT' AND SoLuong=0 DELETE FROM CHAOGIAAUX WHERE SoPhieu='" & tbSoPhieu.EditValue & "CT' AND SoLuong=0 ") Is Nothing Then
                    ShowCanhBao("Lỗi xóa VT số lượng = 0 " & LoiNgoaiLe)
                End If
            Else
                If ExecuteSQLNonQuery("DELETE FROM CHAOGIA WHERE SoPhieu='" & tbSoPhieu.EditValue & "' AND SoLuong=0") Is Nothing Then
                    ShowCanhBao("Lỗi xóa VT số lượng = 0 " & LoiNgoaiLe)
                End If
            End If

            LoadVTChaoGia(tbSoPhieu.EditValue)
            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mChaoGia" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChaoGia).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChaoGia).gdvCT.FocusedRowHandle = CType(deskTop.tabMain.TabPages(i).Controls(0), frmChaoGia).index
                ElseIf deskTop.tabMain.TabPages(i).Tag = "mYeuCauDen" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDen).LoadDSYeuCau()
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mKetQuaChaoGia.Name Then
                    Dim _Index As Integer = CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaChaoGia).gdvCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaChaoGia).LoadDS()
                    'CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaChaoGia).gdvCT.ClearSelection()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaChaoGia).gdvCT.FocusedRowHandle = _Index
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaChaoGia).gdvCT.SelectRow(_Index)
                End If
            Next

            '************* PHAN BO TAM UNG ************************'
            frmCNXuatKho.CapNhatPhanBoTamUng(tbSoPhieu.EditValue)

            ShowAlert(Me.Text & " thành công!")
            TrangThaiCG.isUpdate = True
            Me.Text = "Cập nhật chào giá "
        Catch ex As Exception
            RollBackTransaction()
            If TrangThai.isAddNew Then
                tbSoPhieu.EditValue = ""
                tbNgay.EditValue = DBNull.Value
            End If
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

    Private Sub chkCongTrinh_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCongTrinh.CheckedChanged
        If chkCongTrinh.Checked Then
            tbTenCongTrinh.Enabled = True
            splitChiTiet.Collapsed = False
            tabHangMucKhac.PageVisible = True
        Else
            tbTenCongTrinh.Enabled = False
            splitChiTiet.Collapsed = True
            tabHangMucKhac.PageVisible = False
        End If
    End Sub

#End Region

#Region "Các sự kiện của grid vật tư chào giá gdvVTCT"

    Private Sub gdvVTCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvVTCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvVTCT.GetFocusedRowCellValue("IDVatTu"), gdvVTCT.GetFocusedRowCellValue("Sophieu"), gdvVTCT.GetFocusedRowCellValue("IDYeuCau"), gdvMaKH.EditValue)
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
        ElseIf e.Control AndAlso e.KeyCode = Keys.G Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            Dim dt As DataTable = SqlSelect.Select_ThongTinBanHang(gdvVTCT.GetFocusedRowCellValue("IDVatTu"), gdvMaKH.EditValue)
            If Not dt Is Nothing Then
                Dim f As New frmThongTinGiaBan
                f.lbVatTu.Text &= gdvVTCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvVTCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvVTCT.GetFocusedRowCellValue("TenHang")
                f.gdvGiaNhap.DataSource = dt
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Q Then
            splitChiTiet.Collapsed = False
            XtraTabControl1.SelectedTabPageIndex = 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.W Then
            splitChiTiet.Collapsed = True
            XtraTabControl1.SelectedTabPageIndex = 0
            'ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            '    If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            '    If ShowCauHoi("Xoá dòng hiện tại ?") Then
            '        If TrangThai.isUpdate Then
            '            Try
            '                BeginTransaction()
            '                AddParameterWhere("@IDCG", gdvVTCT.GetFocusedRowCellValue("IDCG"))
            '                If doDelete("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            '                AddParameterWhere("@IDYC", gdvVTCT.GetFocusedRowCellValue("IDYeuCau"))
            '                AddParameter("@Trangthai", TrangThaiYeuCau.CanChaoGia)
            '                If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            '                ComitTransaction()
            '                CType(gdvVT.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvVTCT.GetFocusedDataSourceRowIndex)
            '                'gdvVTCT.DeleteSelectedRows()
            '            Catch ex As Exception
            '                RollBackTransaction()
            '                ShowBaoLoi(ex.Message)
            '            End Try
            '        Else
            '            CType(gdvVT.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvVTCT.FocusedRowHandle)
            '            'gdvVTCT.DeleteSelectedRows()
            '        End If
            '    End If
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
        End Select
    End Sub

#End Region

#Region "Các sự kiện của grid công trình"
    Private Sub gdvCongTrinhCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCongTrinhCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            'If ShowCauHoi("Xoá dòng hiện tại ?") Then
            '    If gdvCongTrinhCT.FocusedRowHandle < 0 Then Exit Sub
            '    If Not gdvCongTrinhCT.GetFocusedRowCellValue("IDCGAUX") Is Nothing Then
            '        AddParameterWhere("@IDCGAUX", gdvCongTrinhCT.GetFocusedRowCellValue("IDCGAUX"))
            '        If doDelete("CHAOGIAAUX", "ID=@IDCGAUX") Is Nothing Then
            '            ShowBaoLoi(LoiNgoaiLe)
            '            Exit Sub
            '        End If
            '    End If
            '    gdvCongTrinhCT.DeleteSelectedRows()
            'End If

        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvCongTrinhCT.FocusedRowHandle = 0 Or gdvCongTrinhCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvCongTrinhCT.GetFocusedRowCellValue("AZ")
            gdvCongTrinhCT.SetFocusedRowCellValue("AZ", gdvCongTrinhCT.GetRowCellValue(gdvCongTrinhCT.FocusedRowHandle - 1, "AZ"))
            gdvCongTrinhCT.SetRowCellValue(gdvCongTrinhCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvCongTrinhCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then

            If gdvCongTrinhCT.FocusedRowHandle = gdvCongTrinhCT.RowCount - 2 Or gdvCongTrinhCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvCongTrinhCT.GetFocusedRowCellValue("AZ")
            gdvCongTrinhCT.SetFocusedRowCellValue("AZ", gdvCongTrinhCT.GetRowCellValue(gdvCongTrinhCT.FocusedRowHandle + 1, "AZ"))
            gdvCongTrinhCT.SetRowCellValue(gdvCongTrinhCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvCongTrinhCT.FocusedRowHandle -= 1
        End If
    End Sub

    Private Sub gdvCongTrinhCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCongTrinhCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "ChietKhauPT"
                If Not IsNumeric(e.Value) Then
                    gdvCongTrinhCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                If _exit Then Exit Select
                _exit = True
                gdvCongTrinhCT.SetRowCellValue(e.RowHandle, "ChietKhau", 0)
                _exit = False
            Case "ChietKhau"
                If Not IsNumeric(e.Value) Then
                    gdvCongTrinhCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                If _exit Then Exit Select
                _exit = True
                gdvCongTrinhCT.SetRowCellValue(e.RowHandle, "ChietKhauPT", 0)
                _exit = False
            Case "DonGia", "SoLuong"
                If Not IsNumeric(e.Value) Then
                    gdvCongTrinhCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                gdvCongTrinhCT.SetRowCellValue(e.RowHandle, "ThanhTien", gdvCongTrinhCT.GetRowCellValue(e.RowHandle, "DonGia") * gdvCongTrinhCT.GetRowCellValue(e.RowHandle, "SoLuong"))

        End Select
    End Sub

    Private Sub gdvCongTrinhCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCongTrinhCT.InitNewRow
        gdvCongTrinhCT.SetFocusedRowCellValue("SoLuong", 1)
        gdvCongTrinhCT.SetFocusedRowCellValue("DonGia", 0)
        gdvCongTrinhCT.SetFocusedRowCellValue("ChietKhauPT", 0)
        gdvCongTrinhCT.SetFocusedRowCellValue("ChietKhau", 0)
        gdvCongTrinhCT.SetFocusedRowCellValue("MucThue", 10)
        gdvCongTrinhCT.SetFocusedRowCellValue("XuatThue", True)
        gdvCongTrinhCT.SetFocusedRowCellValue("TienTe", cbTienTe.EditValue)
        gdvCongTrinhCT.SetFocusedRowCellValue("TyGia", tbTyGia.EditValue)
        gdvCongTrinhCT.SetFocusedRowCellValue("ThanhTien", 0)
        gdvCongTrinhCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvCongTrinhCT.GetRowCellValue(gdvCongTrinhCT.RowCount - 2, "AZ")) + 1)
    End Sub
#End Region

#Region "Tính giá"

    Private Sub btGiaBanBuon_Click(sender As System.Object, e As System.EventArgs) Handles btGiaBanBuon.Click
        If btGhi.Enabled = False Then Exit Sub
        _exit = True
        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "DonGia", gdvVTCT.GetRowCellValue(i, "GiaBanBuon"))
            gdvVTCT.SetRowCellValue(i, "GiaBanPT", 0)
        Next
        _exit = False
        gdvVTCT.EndUpdate()
    End Sub

    Private Sub btGiaBanLe_Click(sender As System.Object, e As System.EventArgs) Handles btGiaBanLe.Click
        If btGhi.Enabled = False Then Exit Sub
        gdvVTCT.BeginUpdate()
        _exit = True
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "DonGia", gdvVTCT.GetRowCellValue(i, "GiaBanLe"))
            gdvVTCT.SetRowCellValue(i, "GiaBanPT", 0)
        Next
        _exit = False
        gdvVTCT.EndUpdate()
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

            With gdvCongTrinhCT
                For i As Integer = 0 To .RowCount - 2
                    Dim _DonGia As Double = 0
                    _DonGia = (.GetRowCellValue(i, "DonGia") * tbTyGia.EditValue) / CType(dr("TyGia"), Double)
                    .SetRowCellValue(i, "DonGia", _DonGia)
                    If .GetRowCellValue(i, "ChietKhauPT") = 0 And .GetRowCellValue(i, "ChietKhau") > 0 Then
                        .SetRowCellValue(i, "ChietKhauPT", Math.Round((.GetRowCellValue(i, "ChietKhau") / .GetRowCellValue(i, "DonGia")) * 100, 2))
                    ElseIf .GetRowCellValue(i, "ChietKhauPT") > 0 And .GetRowCellValue(i, "ChietKhau") = 0 Then
                        .SetRowCellValue(i, "ChietKhau", Math.Round((.GetRowCellValue(i, "ChietKhauPT") / 100) * .GetRowCellValue(i, "DonGia"), 2))
                    End If
                Next
            End With

            _exit = False
            tbTyGia.EditValue = dr("TyGia")
        End If
    End Sub

    Private Sub btTinhToan_Click(sender As System.Object, e As System.EventArgs) Handles btTinhToan.Click
        If btGhi.Enabled = False Then Exit Sub
        '===========Tính đơn giá và Chiết khấu
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        gdvCongTrinhCT.CloseEditor()
        gdvCongTrinhCT.UpdateCurrentRow()
        _exit = True
        'Dim tmp As DataTable = 

        With CType(gdvVT.Views.Item(0).DataSource, DataView).Table
            For i As Integer = 0 To .Rows.Count - 1

                Dim _DonGia As Double = 0
                If .Rows(i)("DonGia") = 0 And .Rows(i)("GiaBanPT") = 0 Then

                    _DonGia = .Rows(i)("GiaBanLe")
                    _DonGia *= .Rows(i)("TyGia")
                    If cbTienTe.EditValue > TienTe.VND Then
                        _DonGia /= tbTyGia.EditValue
                    End If
                    .Rows(i)("DonGia") = Math.Round(_DonGia, 2)


                    If .Rows(i)("GiaList") = 0 Then
                        .Rows(i)("GiaBanPT") = 0
                    Else
                        .Rows(i)("GiaBanPT") = (_DonGia / (.Rows(i)("GiaList") * .Rows(i)("TyGia"))) * 100
                        If cbTienTe.EditValue > TienTe.VND Then
                            _DonGia /= tbTyGia.EditValue
                            .Rows(i)("GiaBanPT") = ((_DonGia / (.Rows(i)("GiaList") * .Rows(i)("TyGia"))) / tbTyGia.EditValue) * 100
                        End If

                    End If
                ElseIf .Rows(i)("DonGia") = 0 And .Rows(i)("GiaBanPT") <> 0 Then
                    _DonGia = .Rows(i)("GiaList") * (.Rows(i)("GiaBanPT") / 100)
                    _DonGia *= .Rows(i)("TyGia")
                    If cbTienTe.EditValue > TienTe.VND Then
                        _DonGia /= tbTyGia.EditValue
                    End If
                    .Rows(i)("DonGia") = Math.Round(_DonGia, 2)
                Else
                    If IsDBNull(.Rows(i)("GiaList")) Or .Rows(i)("GiaList") Is Nothing Then
                        .Rows(i)("GiaBanPT") = 0
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

                End If

                'Dim _giaNhap As Double = LayGiaNhapGanNhat(.Rows(i)("IDVatTu"))
                'If _giaNhap = 0 Then
                '    If .Rows(i)("DonGia") < (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue Then
                '        .Rows(i)("LoiGia") = 1
                '    Else
                '        .Rows(i)("LoiGia") = 0
                '    End If

                'Else
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

        'With gdvVTCT
        '    For i As Integer = 0 To .RowCount - 1

        '        Dim _DonGia As Double = 0
        '        If .GetRowCellValue(i, "DonGia") = 0 And .GetRowCellValue(i, "GiaBanPT") = 0 Then
        '            .SetRowCellValue(i, "GiaBanPT", .GetRowCellValue(i, "GiaNhap") + .GetRowCellValue(i, "GiaBanLe"))
        '            _DonGia = .GetRowCellValue(i, "GiaList") * (.GetRowCellValue(i, "GiaBanPT") / 100)
        '            _DonGia *= .GetRowCellValue(i, "TyGia")
        '            If cbTienTe.EditValue > TienTe.VND Then
        '                _DonGia /= tbTyGia.EditValue
        '            End If
        '            .SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))

        '        ElseIf .GetRowCellValue(i, "DonGia") = 0 And .GetRowCellValue(i, "GiaBanPT") <> 0 Then
        '            _DonGia = .GetRowCellValue(i, "GiaList") * (.GetRowCellValue(i, "GiaBanPT") / 100)
        '            _DonGia *= .GetRowCellValue(i, "TyGia")
        '            If cbTienTe.EditValue > TienTe.VND Then
        '                _DonGia /= tbTyGia.EditValue
        '            End If
        '            .SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))
        '        Else
        '            If .GetRowCellValue(i, "GiaList") = 0 Then
        '                .SetRowCellValue(i, "GiaBanPT", 0)
        '            Else
        '                Dim _GiaBanPT As Double = 0
        '                If Convert.ToByte(.GetRowCellValue(i, "TienTe")) > Convert.ToByte(cbTienTe.EditValue) Then
        '                    _GiaBanPT = .GetRowCellValue(i, "DonGia") / (.GetRowCellValue(i, "GiaList") * .GetRowCellValue(i, "TyGia") / tbTyGia.EditValue)
        '                Else
        '                    _GiaBanPT = .GetRowCellValue(i, "DonGia") / .GetRowCellValue(i, "GiaList")
        '                End If
        '                .SetRowCellValue(i, "GiaBanPT", Math.Round((_GiaBanPT) * 100, 2))
        '            End If
        '        End If

        '        If .GetRowCellValue(i, "ChietKhauPT") = 0 And .GetRowCellValue(i, "ChietKhau") > 0 Then
        '            .SetRowCellValue(i, "ChietKhauPT", Math.Round((.GetRowCellValue(i, "ChietKhau") / .GetRowCellValue(i, "DonGia")) * 100, 2))
        '        ElseIf .GetRowCellValue(i, "ChietKhauPT") > 0 And .GetRowCellValue(i, "ChietKhau") = 0 Then
        '            .SetRowCellValue(i, "ChietKhau", Math.Round((.GetRowCellValue(i, "ChietKhauPT") / 100) * .GetRowCellValue(i, "DonGia"), 2))
        '        Else
        '            .SetRowCellValue(i, "ChietKhauPT", Math.Round((.GetRowCellValue(i, "ChietKhau") / .GetRowCellValue(i, "DonGia")) * 100, 2))
        '        End If

        '    Next
        'End With




        For i As Integer = 0 To gdvCongTrinhCT.RowCount - 2
            If gdvCongTrinhCT.GetRowCellValue(i, "ChietKhauPT") = 0 And gdvCongTrinhCT.GetRowCellValue(i, "ChietKhau") > 0 Then
                gdvCongTrinhCT.SetRowCellValue(i, "ChietKhauPT", Math.Round((gdvCongTrinhCT.GetRowCellValue(i, "ChietKhau") / gdvCongTrinhCT.GetRowCellValue(i, "DonGia")) * 100, 2))
            ElseIf gdvCongTrinhCT.GetRowCellValue(i, "ChietKhauPT") > 0 And gdvCongTrinhCT.GetRowCellValue(i, "ChietKhau") = 0 Then
                gdvCongTrinhCT.SetRowCellValue(i, "ChietKhau", Math.Round((gdvCongTrinhCT.GetRowCellValue(i, "ChietKhauPT") / 100) * gdvCongTrinhCT.GetRowCellValue(i, "DonGia"), 2))
            End If
        Next
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

        With gdvCongTrinhCT
            For i As Integer = 0 To .RowCount - 2
                tbTienTruocThue.EditValue += .GetRowCellValue(i, "ThanhTien")
                tbChietKhau.EditValue += (.GetRowCellValue(i, "ChietKhau") * .GetRowCellValue(i, "SoLuong"))
                If Convert.ToBoolean(.GetRowCellValue(i, "XuatThue")) Then
                    tbTienThue.EditValue += (.GetRowCellValue(i, "MucThue") / 100) * .GetRowCellValue(i, "ThanhTien")
                End If
            Next
        End With


        tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue

        tbThucCK.EditValue = Math.Round(tbChietKhau.EditValue - tbChietKhau.EditValue * (tbKhauTruPT.EditValue / 100), 2)
        tbKhauTru.EditValue = Math.Round((tbKhauTruPT.EditValue / 100) * tbChietKhau.EditValue)
        tbThucThu.EditValue = tbTienSauThue.EditValue - tbThucCK.EditValue
    End Sub

#End Region

#Region "Thay đổi trạng thái"
    Private Sub cbTrangThai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTrangThai.EditValueChanged
        If cbTrangThai.EditValue = TrangThaiChaoGia.DaXacNhan Then
            tbNgayNhan.Enabled = True
            tbNgayGiao.Enabled = True
            tbNgayHuy.Enabled = False
            tbNgayNhan.EditValue = Today.Date
        ElseIf cbTrangThai.EditValue = TrangThaiChaoGia.ChoXacNhan Then
            tbNgayNhan.Enabled = False
            tbNgayGiao.Enabled = False
            tbNgayHuy.Enabled = False
        ElseIf cbTrangThai.EditValue > TrangThaiChaoGia.DaXacNhan Then
            tbNgayHuy.Enabled = True
            tbNgayNhan.Enabled = False
            tbNgayGiao.Enabled = False
            tbNgayHuy.EditValue = Today.Date
        End If
        If _exit Then Exit Sub
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

#Region "Chuyển mã"
    Private Sub chkLocVatTu_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkDongLocDuLieu.CheckedChanged
        gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = chkDongLocDuLieu.Checked
    End Sub

    Private Sub rtbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub rtbThongSo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThongSo.ButtonClick
        btFilterThongSo.EditValue = Nothing
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

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        'If Not btfilterTenVT.EditValue Is Nothing Then
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        ' End If
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        ' If Not btFilterHangSX.EditValue Is Nothing Then
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        ' End If
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        ' If Not btFilterNhomVT.EditValue Is Nothing Then
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        ' End If
    End Sub

    Private Sub chkThongDung_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkThongDung.CheckedChanged
        If chkThongDung.Checked Then
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
        On Error Resume Next
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

    Private Sub btChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChuyenMa.ItemClick
        If btGhi.Enabled = False Then Exit Sub
        Application.DoEvents()
        gdvChuyenMaCT.CloseEditor()
        gdvChuyenMaCT.UpdateCurrentRow()

        For i As Integer = 0 To gdvChuyenMaCT.RowCount - 1
            If gdvChuyenMaCT.GetRowCellValue(i, "SLYC") > 0 Then
                If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                    gdvVTCT.AddNewRow()
                    gdvVTCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 2, "AZ")) + 1)
                    gdvVTCT.SetFocusedRowCellValue("IDYeuCau", 1)
                Else
                    If Not ShowCauHoi("Thay thế mã: " & gdvVTCT.GetFocusedRowCellValue("Model") & " bằng: " & gdvChuyenMaCT.GetRowCellValue(i, "Model") & " ?") Then Exit Sub
                End If

                gdvVTCT.SetFocusedRowCellValue("TrangThai", cbTrangThai.EditValue)
                gdvVTCT.SetFocusedRowCellValue("IDVatTu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                gdvVTCT.SetFocusedRowCellValue("DonGia", 0)
                gdvVTCT.SetFocusedRowCellValue("GiaBanPT", 0)
                If IsDBNull(tbNgay.EditValue) Then
                    tbNgay.EditValue = Nothing
                End If
                Dim GiaGanNhat As Double = 0
                AddParameterWhere("@VatTu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                AddParameterWhere("@ThoiGian", Convert.ToDateTime(tbNgay.EditValue).Date)
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT ISNULL(dbo.LayGiaNhap(@VatTu,@ThoiGian),0)")
                If Not tb Is Nothing Then
                    GiaGanNhat = tb.Rows(0)(0)
                End If
                If GiaGanNhat = 0 Then
                    If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Gianhap")) Or gdvChuyenMaCT.GetRowCellValue(i, "Gianhap") Is Nothing Then
                        GiaGanNhat = 0
                    Else
                        GiaGanNhat = (gdvChuyenMaCT.GetRowCellValue(i, "GiaList") * (gdvChuyenMaCT.GetRowCellValue(i, "Gianhap") / 100) * gdvChuyenMaCT.GetRowCellValue(i, "TyGia")) / tbTyGia.EditValue

                    End If
                End If
                gdvVTCT.SetFocusedRowCellValue("GiaNhapGanNhat", GiaGanNhat)
                gdvVTCT.SetFocusedRowCellValue("TTGiaNhap", 0)
                gdvVTCT.SetFocusedRowCellValue("ChietKhau", 0)
                gdvVTCT.SetFocusedRowCellValue("ChietKhauPT", 0)
                gdvVTCT.SetFocusedRowCellValue("ThanhTien", 0)
                gdvVTCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetRowCellValue(i, "TenVT"))
                gdvVTCT.SetFocusedRowCellValue("ThongSo", gdvChuyenMaCT.GetRowCellValue(i, "Thongso"))
                gdvVTCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetRowCellValue(i, "Model"))
                gdvVTCT.SetFocusedRowCellValue("SoLuong", gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                gdvVTCT.SetFocusedRowCellValue("TenDVT", gdvChuyenMaCT.GetRowCellValue(i, "DVT"))
                gdvVTCT.SetFocusedRowCellValue("TenHang", gdvChuyenMaCT.GetRowCellValue(i, "HangSX"))
                gdvVTCT.SetFocusedRowCellValue("slTon", gdvChuyenMaCT.GetRowCellValue(i, "slTon"))
                gdvVTCT.SetFocusedRowCellValue("DangVe", gdvChuyenMaCT.GetRowCellValue(i, "Dangve"))
                gdvVTCT.SetFocusedRowCellValue("NgayVe", gdvChuyenMaCT.GetRowCellValue(i, "Ngayve"))
                gdvVTCT.SetFocusedRowCellValue("CanXuat", gdvChuyenMaCT.GetRowCellValue(i, "Canxuat"))
                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "GiaList")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaList", 0)
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaList", gdvChuyenMaCT.GetRowCellValue(i, "GiaList"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "GiaBanBuon")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaBanBuon", 5)
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaBanBuon", gdvChuyenMaCT.GetRowCellValue(i, "GiaBanBuon"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "GiaBanLe")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaBanLe", 5)
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaBanLe", gdvChuyenMaCT.GetRowCellValue(i, "GiaBanLe"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Gianhap")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaNhap", 100)
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaNhap", gdvChuyenMaCT.GetRowCellValue(i, "Gianhap"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "TienTe")) Then
                    gdvVTCT.SetFocusedRowCellValue("TienTe", 0)
                Else
                    gdvVTCT.SetFocusedRowCellValue("TienTe", gdvChuyenMaCT.GetRowCellValue(i, "TienTe"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "TyGia")) Then
                    gdvVTCT.SetFocusedRowCellValue("TyGia", 1)
                Else
                    gdvVTCT.SetFocusedRowCellValue("TyGia", gdvChuyenMaCT.GetRowCellValue(i, "TyGia"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Xuatthue")) Then
                    gdvVTCT.SetFocusedRowCellValue("XuatThue", True)
                Else
                    gdvVTCT.SetFocusedRowCellValue("XuatThue", gdvChuyenMaCT.GetRowCellValue(i, "Xuatthue"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Mucthue")) Then
                    gdvVTCT.SetFocusedRowCellValue("MucThue", 10)
                Else
                    gdvVTCT.SetFocusedRowCellValue("MucThue", gdvChuyenMaCT.GetRowCellValue(i, "Mucthue"))
                End If


                gdvVTCT.SetFocusedRowCellValue("HangTon", gdvChuyenMaCT.GetRowCellValue(i, "HangTon"))
                gdvVTCT.SetFocusedRowCellValue("LoiGia", 0)
                gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
                gdvVTCT.CloseEditor()
                gdvVTCT.UpdateCurrentRow()
            End If
        Next


        ShowAlert("Đã chuyển mã !")
    End Sub

    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then
            If btGhi.Enabled = False Then Exit Sub
            pNhapSoLuong.Visible = True
            pNhapSoLuong.Focus()
            If gdvVTCT.FocusedRowHandle >= 0 Then
                tbSL.EditValue = Convert.ToDouble(gdvVTCT.GetFocusedRowCellValue("SoLuong"))
            Else
                tbSL.EditValue = 1.0
            End If
            tbSL.Focus()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvChuyenMaCT.GetFocusedRowCellValue("ID"), -1, -1, gdvMaKH.EditValue)
            If Not ds Is Nothing Then
                Dim f As New frmThongTinGiaNhap
                f.lbVatTu.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("HangSX")
                f.lbGiaCungUng.Text &= Convert.ToDouble(ds.Tables(0).Rows(0)(0)).ToString("N2")
                f.gdvGiaNhap.DataSource = ds.Tables(1)
                f.gdvChaoGia.DataSource = ds.Tables(2)
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.G Then
            Dim dt As DataTable = SqlSelect.Select_ThongTinBanHang(gdvChuyenMaCT.GetFocusedRowCellValue("ID"), gdvMaKH.EditValue)
            If Not dt Is Nothing Then
                Dim f As New frmThongTinGiaBan
                f.lbVatTu.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("HangSX")
                f.gdvGiaNhap.DataSource = dt
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = Not gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
            chkDongLocDuLieu.Checked = gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub
#End Region

#Region "Xuất Excel"
    Private Sub btXuatExcel_Click(sender As System.Object, e As System.EventArgs) Handles btXuatExcel.Click
        btXuatExcel.ShowDropDown()
    End Sub

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        If chkDungFileCuaKhach.Checked Then
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT FileDinhKem FROM BANGYEUCAU WHERE Sophieu=" & SPYeuCau)
            If Not tb Is Nothing Then
                Dim tbFile As DataTable = DataSourceDSFile(tb.Rows(0)(0).ToString)
                Dim dr() As DataRow = tbFile.Select("File like '%" & "YC" & SPYeuCau & " FileYC " & "%'")
                If dr.Length = 1 Then
                    Utils.XuatExcel.CreateExcelFileChaoGiaFromYC(SPYeuCau, chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.EditValue & "\" & dr(0)(0).ToString)
                    Exit Sub
                End If
            End If

            If ShowCauHoi("Không tìm thấy file yêu cầu trên hệ thống, bạn có muốn tìm file khác ?") Then
                Dim openFile As New OpenFileDialog
                openFile.Filter = "Excel File|*.xls;*.xlsx"
                If openFile.ShowDialog = DialogResult.OK Then
                    XuatExcel.CreateExcelFileChaoGiaFromYC(SPYeuCau, chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, openFile.FileName)
                End If
            End If

        Else
            XuatExcel.CreateExcelFileChaoGia(tbSoPhieu.EditValue, chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, chkN0.Checked, chkCongTrinh.Checked, chkGhiChu.Checked, gdvMaKH.Text)
        End If

    End Sub
#End Region

    Private Sub tbGiaBanPT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaBanPT.ButtonClick
        If btGhi.Enabled = False Then Exit Sub
        _exit = True
        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "GiaBanPT", tbGiaBanPT.EditValue)
            gdvVTCT.SetRowCellValue(i, "DonGia", 0)
        Next
        gdvVTCT.EndUpdate()
        _exit = False
    End Sub

    Private Sub tbChietKhauPT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbChietKhauPT.ButtonClick
        If btGhi.Enabled = False Then Exit Sub
        _exit = True
        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "ChietKhauPT", tbChietKhauPT.EditValue)
            gdvVTCT.SetRowCellValue(i, "ChietKhau", 0)
        Next
        gdvVTCT.EndUpdate()
        For i As Integer = 0 To gdvCongTrinhCT.RowCount - 2
            gdvCongTrinhCT.SetRowCellValue(i, "ChietKhauPT", tbChietKhauPT.EditValue)
            gdvCongTrinhCT.SetRowCellValue(i, "ChietKhau", 0)
        Next
        _exit = False
    End Sub


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If chkTaiAnh.Checked Then
            colHinhAnh.Visible = True
        Else
            colHinhAnh.Visible = False
        End If
        LoadDSVatTuDungChuyenMa()
    End Sub

    Private Sub tbSL_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbSL.KeyPress
        If btGhi.Enabled = False Then Exit Sub
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            gdvChuyenMaCT.SetFocusedRowCellValue("SLYC", Convert.ToDouble(tbSL.EditValue))
            Application.DoEvents()
            btChuyenMa.PerformClick()
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        ElseIf e.KeyChar = Convert.ToChar(Keys.Escape) Then
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        End If
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
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text)
            End If
            For Each file In openFile.FileNames
                Try
                    path = "YC" & SPYeuCau & " CG" & tbSoPhieu.EditValue & " KD " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & path)
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

        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        Try
            If ShowCauHoi("Xoá file được chọn ?") Then
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Impersonator.EndImpersonation()
                gdvListFileCT.DeleteSelectedRows()
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            'If e.CellValue Is Nothing Then Exit Sub
            'If e.CellValue.ToString = "" Then Exit Sub
            'Dim psi As New ProcessStartInfo()
            'With psi
            '    .FileName = RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & e.CellValue
            '    .UseShellExecute = True
            'End With
            'Try
            '    Process.Start(psi)
            'Catch ex As Exception
            '    ShowBaoLoi(ex.Message)
            'End Try
            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub btFileDinhKem_Click(sender As System.Object, e As System.EventArgs) Handles btFileDinhKem.Click
        gListFileCT.Visible = True
    End Sub

    Private Sub gdvTaiLieuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvTaiLieuCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            ShowWaiting("Đang mở file...")
            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
            End If
            Application.DoEvents()
            Try
                System.IO.File.Copy(UrlTaiLieuVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & e.CellValue, Application.StartupPath & "\tmp\" & e.CellValue, True)
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
                Exit Sub
            End Try

            CloseWaiting()

            Dim psi As New ProcessStartInfo()
            With psi
                .FileName = Application.StartupPath & "\tmp\" & e.CellValue
                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub

    Private Sub LoadDSTaiLieu(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvTaiLieu.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvTaiLieuCT.AddNewRow()
                gdvTaiLieuCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvTaiLieuCT.CloseEditor()
        gdvTaiLieuCT.UpdateCurrentRow()

    End Sub

    Private Sub rcbTaiLieu_Popup(sender As System.Object, e As System.EventArgs) Handles rcbTaiLieu.Popup
        LoadDSTaiLieu(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub frmCNChaoGia_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        isShowing = False
        If btGhi.Enabled = False Then Exit Sub
        If TrangThai.isAddNew Then
            For i As Integer = 0 To gdvListFileCT.RowCount - 1
                Try
                    System.IO.File.Delete(RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & gdvListFileCT.GetRowCellValue(i, "File"))
                Catch ex As Exception
                    ShowCanhBao(ex.Message)
                End Try
            Next
        Else
            Dim _File As String = ""
            For i As Integer = 0 To gdvListFileCT.RowCount - 1
                _File &= gdvListFileCT.GetRowCellValue(i, "File")
                If i < gdvListFileCT.RowCount - 1 Then
                    _File &= ";"
                End If
            Next

            AddParameter("@FileDinhKem", _File)
            AddParameterWhere("@SP", tbSoPhieu.EditValue)
            If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If

    End Sub

    Private Sub mTaiHinhAnh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiHinhAnh.ItemClick
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

    Private Sub mTaiTaiLieuVeMay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiTaiLieuVeMay.ItemClick
        If gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File").ToString <> "" Then
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File"))
            saveFile.FileName = gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File")
            If saveFile.ShowDialog = DialogResult.OK Then
                Try
                    ShowWaiting("Đang tải file về máy ...")
                    System.IO.File.Copy(UrlTaiLieuVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File"), saveFile.FileName, True)
                    CloseWaiting()
                    If ShowCauHoi("Đã tải file về máy, bạn có muốn mở file vừa tải không ?") Then
                        OpenFile(saveFile.FileName)
                    End If
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvVTCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "LoiGia" Then
            If e.CellValue = 1 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub


    Private Sub btInBangKe_Click(sender As System.Object, e As System.EventArgs) Handles btInBangKe.Click
        If Not chkCongTrinh.Checked Then
            ShowCanhBao("Chỉ áp dụng đối với chào giá công trình !")
            Exit Sub
        End If

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

    Private Sub mXemAnhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemAnhLon.ItemClick
        If gdvChuyenMaCT.GetFocusedRowCellValue("HinhAnh").ToString = "" Then Exit Sub
        Dim f As New frmXemAnh
        f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HinhAnh").ToString)
        f.Text = "Ảnh: " & gdvChuyenMaCT.GetFocusedRowCellValue("Model").ToString
        f.ShowDialog()

    End Sub

    Private Sub tbNgayGiao_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbNgayGiao.EditValueChanged
        If _exit Then Exit Sub
        If Not tbNgayGiao.EditValue Is Nothing Then
            _exit = True
            tbSoNgayGiao.EditValue = DateDiff(DateInterval.Day, tbNgayNhan.EditValue, tbNgayGiao.EditValue)
            _exit = False
        End If
    End Sub

    Private Sub rcbFilterKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbFilterKH.ButtonClick
        If e.Button.Index = 1 Then
            btFilterKH.EditValue = Nothing
        End If
    End Sub

    Private Sub btTichThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTichThue.ItemClick
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        gdvCongTrinhCT.CloseEditor()
        gdvCongTrinhCT.UpdateCurrentRow()
        Dim check As Boolean = gdvVTCT.GetRowCellValue(0, "XuatThue")
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "XuatThue", Not check)
        Next

        For i As Integer = 0 To gdvCongTrinhCT.RowCount - 1
            gdvCongTrinhCT.SetRowCellValue(i, "XuatThue", Not check)
        Next
    End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        f._IDVatTu = gdvVTCT.GetFocusedRowCellValue("IDVatTu")
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

    Private Sub mTinhGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhGia.ItemClick
        pTinhGia.Location = Cursor.Position
        pTinhGia.Visible = True
        tbHSChiaGiaCU.EditValue = 0.85
        tbHSChiaGiaNhap.EditValue = 0.85

        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("GiaCungUng")) Then
            tbGiaCU.EditValue = 0
            tbGiaCungUng.EditValue = 0
        Else
            tbGiaCungUng.EditValue = gdvVTCT.GetFocusedRowCellValue("GiaCungUng")
            tbGiaCU.EditValue = Math.Round((gdvVTCT.GetFocusedRowCellValue("GiaCungUng") / tbHSChiaGiaCU.EditValue) / 100, 0) * 100
        End If

        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("GiaList")) Then
            tbGiaList.EditValue = 0
        Else
            tbGiaList.EditValue = gdvVTCT.GetFocusedRowCellValue("GiaList")
        End If

        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("GiaNhapGanNhat")) Then
            tbGiaNhap.EditValue = 0
            tbLayGiaNhap.EditValue = 0
        Else
            tbGiaNhap.EditValue = gdvVTCT.GetFocusedRowCellValue("GiaNhapGanNhat")
            tbLayGiaNhap.EditValue = Math.Round((gdvVTCT.GetFocusedRowCellValue("GiaNhapGanNhat") / tbHSChiaGiaNhap.EditValue) / 100, 0) * 100
        End If

        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("PTBB")) Then
            tbHSNhanGiaBB.EditValue = 0
            tbGiaBB.EditValue = 0
        Else
            tbHSNhanGiaBB.EditValue = Math.Round(gdvVTCT.GetFocusedRowCellValue("PTBB"), 2)
            tbGiaBB.EditValue = Math.Round((tbGiaList.EditValue * (tbHSNhanGiaBB.EditValue / 100)) / 100, 0) * 100
        End If

        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("PTBL")) Then
            tbHSNhanGiaBL.EditValue = 0
            tbGiaBL.EditValue = 0
        Else
            tbHSNhanGiaBL.EditValue = Math.Round(gdvVTCT.GetFocusedRowCellValue("PTBL"), 2)
            tbGiaBL.EditValue = Math.Round((tbGiaList.EditValue * (tbHSNhanGiaBL.EditValue / 100)) / 100, 0) * 100
        End If




        ' tbGiaCungUng.EditValue = 

    End Sub

    Private Sub btDongPTinhGia_Click(sender As System.Object, e As System.EventArgs) Handles btDongPTinhGia.Click
        pTinhGia.Visible = False
    End Sub

    Private Sub tbHSChiaGiaCU_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbHSChiaGiaCU.ButtonClick
        If e.Button.Index = 1 Then
            tbHSChiaGiaCU.EditValue = 0.85
        End If
    End Sub

    Private Sub tbHSNhanGiaBB_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbHSNhanGiaBB.ButtonClick
        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("PTBB")) Then
            tbHSNhanGiaBB.EditValue = 0
            tbGiaBB.EditValue = 0
        Else
            Dim _e As Boolean = _exit
            _exit = True
            tbHSNhanGiaBB.EditValue = Math.Round(gdvVTCT.GetFocusedRowCellValue("PTBB"), 2)
            tbGiaBB.EditValue = Math.Round((tbGiaList.EditValue * (tbHSNhanGiaBB.EditValue / 100)) / 100, 0) * 100
            _exit = _e
        End If
    End Sub

    Private Sub tbHSNhanGiaBL_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbHSNhanGiaBL.ButtonClick
        If IsDBNull(gdvVTCT.GetFocusedRowCellValue("PTBL")) Then
            tbHSNhanGiaBL.EditValue = 0
            tbGiaBL.EditValue = 0
        Else
            Dim _e As Boolean = _exit
            _exit = True
            tbHSNhanGiaBL.EditValue = Math.Round(gdvVTCT.GetFocusedRowCellValue("PTBL"), 2)
            tbGiaBL.EditValue = Math.Round((tbGiaList.EditValue * (tbHSNhanGiaBL.EditValue / 100)) / 100, 0) * 100
            _exit = _e
        End If
    End Sub

    Private Sub tbHSChiaGiaNhap_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbHSChiaGiaNhap.ButtonClick
        If e.Button.Index = 1 Then
            tbHSChiaGiaNhap.EditValue = 0.85
        End If
    End Sub

    Private Sub tbHSChiaGiaCU_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbHSChiaGiaCU.EditValueChanged
        If tbGiaCungUng.EditValue = 0 Then
            tbGiaCU.EditValue = 0
        Else
            If Not _exit Then
                If tbHSChiaGiaCU.EditValue = 0 Then
                    tbGiaCU.EditValue = 0
                Else
                    tbGiaCU.EditValue = Math.Round((tbGiaCungUng.EditValue / tbHSChiaGiaCU.EditValue) / 100, 0) * 100
                End If

            End If

        End If
    End Sub

    Private Sub tbHSNhanGiaBB_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbHSNhanGiaBB.EditValueChanged
        If tbGiaList.EditValue = 0 Then
            tbGiaBB.EditValue = 0
        Else
            If Not _exit Then
                tbGiaBB.EditValue = Math.Round((tbGiaList.EditValue * (tbHSNhanGiaBB.EditValue / 100)) / 100, 0) * 100
            End If

        End If
    End Sub

    Private Sub tbHSNhanGiaBL_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbHSNhanGiaBL.EditValueChanged
        If tbGiaList.EditValue = 0 Then
            tbGiaBL.EditValue = 0
        Else
            If Not _exit Then
                tbGiaBL.EditValue = Math.Round((tbGiaList.EditValue * (tbHSNhanGiaBL.EditValue / 100)) / 100, 0) * 100
            End If

        End If
    End Sub

    Private Sub tbHSChiaGiaNhap_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbHSChiaGiaNhap.EditValueChanged
        If tbGiaNhap.EditValue = 0 Then
            tbLayGiaNhap.EditValue = 0
        Else
            If Not _exit Then
                If tbHSChiaGiaNhap.EditValue = 0 Then
                    tbLayGiaNhap.EditValue = 0
                Else
                    If tbHSChiaGiaNhap.EditValue = 0 Then
                        tbLayGiaNhap.EditValue = 0
                    Else
                        tbLayGiaNhap.EditValue = Math.Round((tbGiaNhap.EditValue / tbHSChiaGiaNhap.EditValue) / 100, 0) * 100
                    End If

                End If

            End If
        End If
    End Sub

    Private Sub tbGiaCU_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaCU.ButtonClick
        If tbSoDong.EditValue Is Nothing Then
            gdvVTCT.SetFocusedRowCellValue("DonGia", tbGiaCU.EditValue)
        Else
            If ShowCauHoi("Tính giá cho số dòng đã chọn ?") Then
                Dim tb As DataTable = LaySoDong(tbSoDong.EditValue)
                gdvVTCT.BeginUpdate()
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim _DonGia As Double = 0
                    If Not IsDBNull(gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaCungUng")) Then
                        _DonGia = Math.Round((gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaCungUng") / tbHSChiaGiaCU.EditValue) / 100, 0) * 100
                    End If
                    gdvVTCT.SetRowCellValue(tb.Rows(i)(0), "DonGia", _DonGia)
                Next
                gdvVTCT.EndUpdate()
            End If
        End If

    End Sub

    Private Sub tbGiaBB_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaBB.ButtonClick

        If tbSoDong.EditValue Is Nothing Then
            gdvVTCT.SetFocusedRowCellValue("DonGia", tbGiaBB.EditValue)
        Else
            If ShowCauHoi("Tính giá cho số dòng đã chọn ?") Then
                Dim tb As DataTable = LaySoDong(tbSoDong.EditValue)
                gdvVTCT.BeginUpdate()
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim _DonGia As Double = 0
                    If Not IsDBNull(gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaList")) Then
                        _DonGia = Math.Round((gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaList") * (tbHSNhanGiaBB.EditValue / 100)) / 100, 0) * 100
                    End If
                    gdvVTCT.SetRowCellValue(tb.Rows(i)(0), "DonGia", _DonGia)
                Next
                gdvVTCT.EndUpdate()
            End If
        End If
    End Sub

    Private Sub tbGiaBL_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaBL.ButtonClick

        If tbSoDong.EditValue Is Nothing Then
            gdvVTCT.SetFocusedRowCellValue("DonGia", tbGiaBL.EditValue)
        Else
            If ShowCauHoi("Tính giá cho số dòng đã chọn ?") Then
                Dim tb As DataTable = LaySoDong(tbSoDong.EditValue)
                gdvVTCT.BeginUpdate()
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim _DonGia As Double = 0
                    If Not IsDBNull(gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaList")) Then
                        _DonGia = Math.Round((gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaList") * (tbHSNhanGiaBL.EditValue / 100)) / 100, 0) * 100
                    End If
                    gdvVTCT.SetRowCellValue(tb.Rows(i)(0), "DonGia", _DonGia)
                Next
                gdvVTCT.EndUpdate()
            End If
        End If
    End Sub

    Private Sub tbLayGiaNhap_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbLayGiaNhap.ButtonClick

        If tbSoDong.EditValue Is Nothing Then
            gdvVTCT.SetFocusedRowCellValue("DonGia", tbLayGiaNhap.EditValue)
        Else
            If ShowCauHoi("Tính giá cho số dòng đã chọn ?") Then
                Dim tb As DataTable = LaySoDong(tbSoDong.EditValue)
                gdvVTCT.BeginUpdate()
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim _DonGia As Double = 0
                    If Not IsDBNull(gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaNhapGanNhat")) Then
                        _DonGia = Math.Round((gdvVTCT.GetRowCellValue(tb.Rows(i)(0), "GiaNhapGanNhat") / tbHSChiaGiaNhap.EditValue) / 100, 0) * 100
                    End If
                    gdvVTCT.SetRowCellValue(tb.Rows(i)(0), "DonGia", _DonGia)
                Next
                gdvVTCT.EndUpdate()
            End If
        End If
    End Sub

    Private Sub tbGiaCU_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbGiaCU.EditValueChanged
        If tbGiaCungUng.EditValue <> 0 Then
            Dim _e As Boolean = _exit
            _exit = True
            If tbGiaCU.EditValue = 0 Then
                tbHSChiaGiaCU.EditValue = 0
            Else
                tbHSChiaGiaCU.EditValue = Math.Round(tbGiaCungUng.EditValue / tbGiaCU.EditValue, 2)
            End If

            _exit = _e
        End If
    End Sub

    Private Sub tbGiaBB_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbGiaBB.EditValueChanged
        Dim _e As Boolean = _exit
        _exit = True
        If tbGiaList.EditValue = 0 Then
            tbHSNhanGiaBB.EditValue = 0
        Else
            tbHSNhanGiaBB.EditValue = Math.Round((tbGiaBB.EditValue / tbGiaList.EditValue) * 100, 2)
        End If

        _exit = _e
    End Sub

    Private Sub tbGiaBL_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbGiaBL.EditValueChanged
        Dim _e As Boolean = _exit
        _exit = True
        If tbGiaList.EditValue = 0 Then
            tbHSNhanGiaBL.EditValue = 0
        Else
            tbHSNhanGiaBL.EditValue = Math.Round((tbGiaBL.EditValue / tbGiaList.EditValue) * 100, 2)
        End If

        _exit = _e
    End Sub

    Private Sub tbLayGiaNhap_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbLayGiaNhap.EditValueChanged
        If tbGiaNhap.EditValue <> 0 Then
            Dim _e As Boolean = _exit
            _exit = True
            If tbLayGiaNhap.EditValue = 0 Then
                tbHSChiaGiaNhap.EditValue = 0
            Else
                tbHSChiaGiaNhap.EditValue = Math.Round(tbGiaNhap.EditValue / tbLayGiaNhap.EditValue, 2)
            End If

            _exit = _e
        End If
    End Sub

    Private Sub tbSoDong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbSoDong.ButtonClick
        tbSoDong.EditValue = Nothing
    End Sub

    Function LaySoDong(ByVal _str As String) As DataTable
        Dim tb As New DataTable

        tb.Columns.Add("Index")
        If _str <> "" Then
            Dim listFile() As String = _str.ToString.Split(New Char() {","})
            For Each file In listFile
                If file <> "" Then
                    Dim _strFileName() As String = file.ToString.Split(New Char() {"-"})
                    If _strFileName.Length = 2 Then
                        Dim _i As Integer = CType(_strFileName(0), Integer)
                        While _i <= CType(_strFileName(1), Integer)
                            tb.Rows.Add(tb.NewRow)
                            tb.Rows(tb.Rows.Count - 1)(0) = _i - 1
                            _i += 1
                        End While
                    Else
                        tb.Rows.Add(tb.NewRow)
                        tb.Rows(tb.Rows.Count - 1)(0) = _strFileName(0) - 1
                    End If

                End If
            Next
        End If
        Return tb
    End Function

    Private Sub mDuKienThanhToan_Click(sender As System.Object, e As System.EventArgs) Handles mDuKienThanhToan.Click
        If tbSoPhieu.EditValue.ToString = "" Then
            ShowCanhBao("Phải lưu lại chào giá trước khi cập nhật thông tin dự kiến thanh toán !")
            Exit Sub
        End If
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = tbSoPhieu.EditValue
        f._PhaiTra = False
        f._Buoc1 = True
        f.ShowDialog()
    End Sub

    Private Sub mTinhTrangHangHoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangHangHoa.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
        f.ShowDialog()
    End Sub

    Private Sub mNhomTheoBoVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhomTheoBoVT.ItemClick
        If colTenBoVT.GroupIndex = 0 Then
            colTenBoVT.GroupIndex = -1
            gdvVTCT.ClearGrouping()
        Else
            colTenBoVT.GroupIndex = 0
            gdvVTCT.ExpandAllGroups()
        End If

    End Sub


    Private Sub cbNhanKS_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhanKS.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanKS.SelectedIndex = -1
        End If
    End Sub

    Private Sub cbNVKyHD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNVKyHD.ButtonClick
        If e.Button.Index = 1 Then
            cbNVKyHD.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhuTrachCT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbPhuTrachCT.ButtonClick
        If e.Button.Index = 1 Then
            cbPhuTrachCT.EditValue = Nothing
        End If
    End Sub


    Private Function CheckHinhThucThanhToanMoiCoLoi()
        Try
            Dim sql As String = "select SoTT from DM_HINH_THUC_TT where id = (select IDHinhThucTT2 from KHACHHANG where ID = @IdKH); "
            sql &= "select SoTT from DM_HINH_THUC_TT where id = @IDHinhThuc"
            AddParameter("@IdKH", gdvMaKH.EditValue)
            AddParameter("@IDHinhThuc", cbHinhThucTT2.EditValue)
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If ds.Tables(0).Rows.Count = 0 Then Return False
            If ds.Tables(1).Rows(0)(0) <= ds.Tables(0).Rows(0)(0) Then Return True
            Return False
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btCNDuLieuChung_Click(sender As System.Object, e As System.EventArgs) Handles btCNDuLieuChung.Click

        If cbHinhThucTT2.EditValue Is Nothing OrElse cbHinhThucTT2.EditValue Is DBNull.Value Then
            ShowBaoLoi("Chưa chọn hình thức thanh toán!")
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            If Not CheckHinhThucThanhToanMoiCoLoi() Then
                ShowCanhBao("Chỉ được chọn hình thức thanh toán có lợi hơn !")
                Exit Sub
            End If
        End If

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            AddParameter("@IDPhuTrachCT", cbPhuTrachCT.EditValue)
        End If



        If cbNhanKS.SelectedIndex = -1 Then
            AddParameter("@NhanKS", DBNull.Value)
        Else
            AddParameter("@NhanKS", cbNhanKS.SelectedIndex)
        End If

        If cbNVKyHD.EditValue Is Nothing Then
            AddParameter("@IDPhuTrachKyHD", cbTakeCare.EditValue)
        Else
            AddParameter("@IDPhuTrachKyHD", cbNVKyHD.EditValue)
        End If
        AddParameter("@IDHinhThucTT", cbHinhThucTT.EditValue)
        AddParameter("@IDHinhThucTT2", cbHinhThucTT2.EditValue)
        AddParameterWhere("@SP", tbSoPhieu.EditValue)
        If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật !")
            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mChaoGia" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChaoGia).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChaoGia).gdvCT.FocusedRowHandle = CType(deskTop.tabMain.TabPages(i).Controls(0), frmChaoGia).index
                End If
            Next
        End If
    End Sub
End Class

