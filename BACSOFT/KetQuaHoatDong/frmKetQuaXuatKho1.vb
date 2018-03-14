Imports BACSOFT.Db.SqlHelper
Imports DevExpress
Imports DevExpress.XtraGrid.Columns

Public Class frmKetQuaXuatKho1

    Private Sub frmKetQuaXuatKho_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        LoadPhongBan()
        Application.DoEvents()
        LoadTakeCare()
        Application.DoEvents()
        LoadCbVC()

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbTakeCare.Enabled = True
            cbPhong.Enabled = True
        Else
            cbTakeCare.Enabled = False
            cbPhong.Enabled = False
            'colTienGoc.Visible = False
            'colChietKhau.Visible = False
            'colPTCK.Visible = False
            colChotTienGoc.Visible = False
            colChotChiPhi.Visible = False
            colChotLoiNhuan.Visible = False
        End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            mChotSoLieu.Visibility = XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Public Sub LoadCbVC()
        Dim sql As String = "SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE ttcKhachHang=3 ORDER BY Ten "
        sql &= " SELECT ID,Ten,TyGia FROM tblTienTe ORDER BY ID "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbDVVC.DataSource = ds.Tables(0)
            cbDVVC.Properties.DataSource = ds.Tables(0)
            rcbTienTe.DataSource = ds.Tables(1)
            cbTienTe.Properties.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadTakeCare()
        Dim sql As String = ""
        If cbPhong.EditValue Is Nothing Then
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID"
        Else
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment= " & cbPhong.EditValue & " ORDER BY ID "
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbTakeCare.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbTakeCare.EditValue = TaiKhoan
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub


    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub

    'Load dùng proc
    Public Sub LoadDS2()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            If cbTakeCare.EditValue Is Nothing Then
                AddParameterWhere("@IDTakeCare", 1)
                AddParameterWhere("@TatCaTakeCare", True)
            Else
                AddParameterWhere("@IDTakeCare", cbTakeCare.EditValue)
                AddParameterWhere("@TatCaTakeCare", False)
            End If

            Dim tb As DataTable = ExecutePrcDataTable("prcKetQuaXuatKho")
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)

            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub
    'Load thông thường
    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            Dim tb As New DataTable
            If cbTieuChi.EditValue = "Xuất kho" Then
                colThuNH.Visible = False
                colPhieuThu.Visible = False
                sql &= " Select DISTINCT"
                sql &= "   KHACHHANG.ttcMa, "

                sql &= "   (CASE (SELECT Count(ID) FROM PHIEUXUATKHO AS B WHERE B.IDKhachHang=PHIEUXUATKHO.IDKhachHang AND "
                sql &= "    datediff(day,B.NgayThang,PHIEUXUATKHO.NgayThang)<365 AND  PHIEUXUATKHO.NgayThang > B.NgayThang "
                sql &= "    AND RIGHT(CONVERT(nvarchar, B.NgayThang,103),7) <> RIGHT(CONVERT(nvarchar, PHIEUXUATKHO.NgayThang,103),7)  )"
                sql &= "    WHEN 0 THEN Convert(bit, 1) ELSE convert(bit, 0) END) KHMoi,"
                sql &= "   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,BANGCHAOGIA.SoPhieu AS SoPhieuCG, PHIEUXUATKHO.CongTrinh, PHIEUXUATKHO.error, "
                sql &= "   PHIEUXUATKHO.TientruocThue * PHIEUXUATKHO.Tygia AS TienTruocThue, PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue, "
                sql &= "   ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) AS TienGoc, ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, "
                sql &= "   PHIEUXUATKHO.TienChietKhau, ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0) + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0) AS ChiCK, "
                If chkMoi.Checked = True Then
                    sql &= "   ISNULL(V_XuatkhoThuNH_TheoXK.ThuNH, 0) + ISNULL(V_XuatkhoThuTM_TheoXK.ThuTM, 0)+ PHIEUXUATKHO.PhanBoTamUng AS Thu,"
                Else
                    sql &= "   ISNULL(V_XuatkhoThuNH.ThuNH, 0) + ISNULL(V_XuatkhoThuTM.ThuTM, 0) AS Thu,"
                End If

                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0)  - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) /  (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * "
                sql &= "    (ISNULL(PhanBoLoiNhuan.TongConLai,0) / 100) "
                sql &= "     AS LoiNhuanKT, "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "    ( ISNULL(PhanBoLoiNhuan.PhuTrachHopDong,0) / 100 ) "
                sql &= "     AS LoiNhuanKD, "
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "    (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS, 0) = 1 "
                sql &= "    THEN (PhanBoLoiNhuan.PhuTrachHopDong + ("
                sql &= "    CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachKyHD, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= "    THEN PhanBoLoiNhuan.XucTienKyHD ELSE 0 END) + "
                sql &= "    (CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachCT, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= "    THEN PhanBoLoiNhuan.PhuTrachQuanLyCT ELSE 0 END)"
                sql &= "    +(CASE WHEN PHIEUXUATKHO.CongTrinh =0 THEN  PhanBoLoiNhuan.TongConLai ELSE 0 END)"
                sql &= "    )/ 100 "
                sql &= "    ELSE "
                sql &= "    (PhanBoLoiNhuan.PhuTrachHopDong + "
                sql &= "    PhanBoLoiNhuan.PhuTrachChaoGia + "
                sql &= "    (CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachKyHD, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= "    THEN PhanBoLoiNhuan.XucTienKyHD ELSE 0 END) + "
                sql &= "    (CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachCT, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= "    THEN PhanBoLoiNhuan.PhuTrachQuanLyCT ELSE 0 END)+"
                sql &= "    (CASE WHEN PHIEUXUATKHO.CongTrinh =0 THEN  PhanBoLoiNhuan.TongConLai ELSE 0 END)) / 100 END) END)"
                sql &= "    as TongLNKD,"
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "    (ISNULL(PhanBoLoiNhuan.XucTienKyHD,0)/100) AS LNXucTienHD,ISNULL(NGUOIKYHD.Ten,NHANSU.Ten) as NVKyHD,ISNULL( IDPhuTrachKyHD,BANGCHAOGIA.IDTakeCare)IDPhuTrachKyHD,"
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0)  - ISNULL(tbLNPTSP.LoiNhuanPTSP,0)"
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "    (PhanBoLoiNhuan.PhuTrachQuanLyCT / 100) AS LNPhuTrachCT, ISNULL(NVPHUTRACHCT.Ten,NhanSu.Ten) as NVPhuTrachCT,ISNULL(IDPhuTrachCT,BANGCHAOGIA.IDTakeCare) as IDPhuTrachCT,"
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "    (ISNULL(PhanBoLoiNhuan.PhuTrachChaoGia,0)/100) AS LNKSChaoGia, (CASE NhanKS WHEN 0 THEN 'KD' WHEN 1 THEN 'KT' ELSE '' END)NhanKS,"
                sql &= "     (CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0)  - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL((CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END), 0.15))) /((PHIEUXUATKHO.Tientruocthue +PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN, "
                sql &= "    (CASE PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tienchietkhau*PHIEUXUATKHO.TyGia)/(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END) AS PTCK,"
                sql &= "  PHIEUXUATKHO.IDTakeCare, LEFT(PHIEUXUATKHO.Sophieu, 4) "
                sql &= "   AS YearMonth, NHANSU.Ten AS TakeCare,DEPATMENT.Ten AS Phong,BANGCHAOGIA.KhauTru,BANGCHAOGIA.TrangThai AS TrangThaiCG,BANGCHAOGIA.IDTakeCare, '' AS CBTT, (CASE WHEN tbTT.SoTien is null then 0 ELSE (CASE WHEN tbTT.SoTien <> (PHIEUXUATKHO.TienTruocThue + PHIEUXUATKHO.TienThue) THEN 2 ELSE 1 END) END) AS TTTT,"
                sql &= "    tbVC.ID AS IDVC,tbVC.IDDVVC,tbVC.ThoiGian,tbVC.SoBill,tbVC.SoTien,tbVC.SoTienTC,tbVC.TienTe,tbVC.TyGia,tbVC.CanNang,tbVC.GhiChu,tbVC.SL,Convert(bit,0)Modify,NGUOINHAP.Ten as NguoiNhapCP,"
                sql &= " PHIEUXUATKHO.TienGoc as TienGocChot,PHIEUXUATKHO.ChiPhi as ChiPhiChot,PHIEUXUATKHO.LoiNhuanChot, "
                sql &= " ISNULL(tbLNPTSP.LoiNhuanPTSP,0)LoiNhuanPTSP"
                sql &= " FROM PHIEUXUATKHO "
                sql &= " LEFT JOIN (SELECT Sum(SoTien)SoTien, SoPhieu1 FROM tblCongNo WHERE Loai=0 GROUP BY SoPhieu1)tbTT ON tbTT.SoPhieu1=PHIEUXUATKHO.SoPhieu "
                sql &= " INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
                If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND NHANSU.IDDepatment= " & cbPhong.EditValue
                End If
                sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
                sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachhang"
                'sql &= " LEFT JOIN (SELECT DISTINCT IDKhachHang FROM PHIEUXUATKHO AS P WHERE datediff(day, P.NgayThang,PHIEUXUATKHO.NgayThang)<365 AND P.SoPhieu <> PHIEUXUATKHO.SoPhieu)tbKHMoi ON PHIEUXUATKHO.IDKhachHang=tbKHMoi.IDKhachHang "
                sql &= " LEFT JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"
                sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
                sql &= " LEFT JOIN NHANSU AS NGUOIKYHD ON NGUOIKYHD.ID=BANGCHAOGIA.IDPhuTrachKyHD"
                sql &= " LEFT JOIN NHANSU AS NVPHUTRACHCT ON NVPHUTRACHCT.ID=BANGCHAOGIA.IDPhuTrachCT"
                sql &= " LEFT JOIN BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang"
                'sql &= " LEFT JOIN tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID"
                'sql &= " LEFT JOIN PhanBoLoiNhuan ON BANGYEUCAU.IDLoaiYeuCau = PhanBoLoiNhuan.ID "
                'sql &= ""
                sql &= " LEFT JOIN View_XuatKhoTongGiaNhapTB ON PHIEUXUATKHO.Sophieu = View_XuatKhoTongGiaNhapTB.Sophieu "
                sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
                sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
                If chkMoi.Checked = True Then
                    sql &= " LEFT JOIN V_XuatkhoThuNH_TheoXK ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuNH_TheoXK.Sophieu "
                    sql &= " LEFT JOIN V_XuatkhoThuTM_TheoXK ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuTM_TheoXK.Sophieu "
                Else
                    sql &= " LEFT JOIN V_XuatkhoThuNH ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuNH.Sophieu "
                    sql &= " LEFT JOIN V_XuatkhoThuTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuTM.Sophieu "
                End If

                sql &= " LEFT JOIN V_XuatkhoChietkhauTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChietkhauTM.Sophieu "
                sql &= " LEFT JOIN V_XuatkhoChietkhauUNC ON PHIEUXUATKHO.Sophieu = V_XuatkhoChietkhauUNC.Sophieu"
                sql &= " LEFT JOIN (SELECT * FROM "
                sql &= " ("
                sql &= "    SELECT *,"
                sql &= "          ROW_NUMBER() OVER (PARTITION BY PhieuTC ORDER BY ThoiGian DESC) AS STT,"
                sql &= " 		Count(PhieuTC) over(PARTITION BY PhieuTC) AS SL"
                sql &= "    FROM ChiPhi WHERE Loai=1"
                sql &= " )tb WHERE STT=1)tbVC ON tbVC.PhieuTC = PHIEUXUATKHO.SoPhieu "
                sql &= " LEFT JOIN NHANSU as NGUOINHAP ON NGUOINHAP.ID=tbVC.IDUser"
                sql &= " LEFT JOIN"
                sql &= " ( SELECT SUM(PhatTrienSanPham_LoiNhuan.SoLuong*PhatTrienSanPham_LoiNhuan.LoiNhuan) as LoiNhuanPTSP,PhatTrienSanPham_LoiNhuan.SoPhieu FROM PhatTrienSanPham_LoiNhuan"
                sql &= "    INNER JOIN PHIEUXUATKHO as PX ON PX.SoPhieu=PhatTrienSanPham_LoiNhuan.SoPhieu"
                sql &= "    AND CONVERT(datetime,Convert(nvarchar,PX.NgayThang,103),103) between @TuNgay AND @DenNgay "
                sql &= "    GROUP BY PhatTrienSanPham_LoiNhuan.SoPhieu)tbLNPTSP ON tbLNPTSP.SoPhieu=PHIEUXUATKHO.SoPhieu"
                sql &= " LEFT JOIN PhanBoLoiNhuan ON PhanBoLoiNhuan.Thang=Right(Convert(nvarchar,PHIEUXUATKHO.NgayTHang,103),7) AND PhanBoLoiNhuan.CT=PHIEUXUATKHO.CongTrinh"

                sql &= " WHERE CONVERT(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) between @TuNgay AND @DenNgay "

                If Not cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND PHIEUXUATKHO.IDTakeCare =" & cbTakeCare.EditValue
                End If
                sql &= " ORDER BY PHIEUXUATKHO.SoPhieu"
                AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
                AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
                'AddParameterWhere("@IDTakeCare", cbTakeCare.EditValue)
                tb = ExecuteSQLDataTable(sql)
            Else
                colPhieuThu.Visible = True
                colPhieuThu.VisibleIndex = 1
                colThuNH.Visible = True
                colThuNH.VisibleIndex = 2
                sql &= " SELECT KHACHHANG.ttcMa,"
                sql &= "   (CASE (SELECT Count(ID) FROM PHIEUXUATKHO AS B WHERE B.IDKhachHang=PHIEUXUATKHO.IDKhachHang AND "
                sql &= "    datediff(day,B.NgayThang,PHIEUXUATKHO.NgayThang)<365 AND  PHIEUXUATKHO.NgayThang > B.NgayThang "
                sql &= "    AND RIGHT(CONVERT(nvarchar, B.NgayThang,103),7) <> RIGHT(CONVERT(nvarchar, PHIEUXUATKHO.NgayThang,103),7) )"
                sql &= "    WHEN 0 THEN Convert(bit, 1) ELSE convert(bit, 0) END) KHMoi,"
                sql &= "    tbThu.NgaythangCT AS Ngay,case when tbThu.Loai=0 then 'TM'+  tbThu.SoPhieu else 'NH' +tbThu.SoPhieu end SoPhieu,tbThu.Loai, PHIEUXUATKHO.error, PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
                sql &= " 	BANGCHAOGIA.MaSoDatHang,PHIEUXUATKHO.CongTrinh,PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia AS TienTruocThue, "
                sql &= " 	PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue,tbThu.Sotien AS TienThu, ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) AS TienGoc, "
                sql &= " 	ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, "
                sql &= " 	ISNULL(PHIEUXUATKHO.TienChietKhau,0)*PHIEUXUATKHO.TyGia AS TienChietKhau,"
                sql &= "     (ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0)   + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0))   AS ChiCK, tbThu.SoTien AS Thu,"
                sql &= "     PHIEUXUATKHO.tienchietkhau * PHIEUXUATKHO.Tygia AS TienChietKhau, "
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "    (((PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0)) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END)) ) "
                sql &= "     * (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia))) * "
                sql &= "    (ISNULL(PhanBoLoiNhuan.TongConLai,0) / 100) END)"
                sql &= "     AS LoiNhuanKT, "
                sql &= "   (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) WHEN 0 THEN 0 ELSE (((PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0)) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END)) ) "
                sql &= "     * (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia))) * "
                sql &= "    ( ISNULL(PhanBoLoiNhuan.PhuTrachHopDong,0) / 100 ) END)"
                sql &= "     AS LoiNhuanKD, "
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "      (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * "
                sql &= "    (ISNULL(PhanBoLoiNhuan.XucTienKyHD,0)/100) END) AS LNXucTienHD,ISNULL(NGUOIKYHD.Ten,NHANSU.Ten) as NVKyHD, IDPhuTrachKyHD,"
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "      (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * "
                sql &= "    (PhanBoLoiNhuan.PhuTrachQuanLyCT / 100 ) END) AS LNPhuTrachCT, ISNULL(NVPHUTRACHCT.Ten,NHANSU.Ten) as NVPhuTrachCT,ISNULL(IDPhuTrachCT,BANGCHAOGIA.IDTakeCare)IDPhuTrachCT,"
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "      (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * "
                sql &= "    (ISNULL(PhanBoLoiNhuan.PhuTrachChaoGia,0)/100) END) AS LNKSChaoGia, (CASE NhanKS WHEN 0 THEN 'KD' WHEN 1 THEN 'KT' ELSE '' END)NhanKS,"
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "      (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * "
                sql &= "    (PhanBoLoiNhuan.TongConLai / 100) END) AS LNThiCong,"
                sql &= "     (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) * PHIEUXUATKHO.Tygia) * "
                sql &= "      (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)) * "
                sql &= " (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS, 0) = 1 "
                sql &= " THEN (PhanBoLoiNhuan.PhuTrachHopDong + ("
                sql &= " CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachKyHD, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= " THEN PhanBoLoiNhuan.XucTienKyHD ELSE 0 END) + "
                sql &= " (CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachCT, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= " THEN PhanBoLoiNhuan.PhuTrachQuanLyCT ELSE 0 END)"
                sql &= " +(CASE WHEN PHIEUXUATKHO.CongTrinh =0 THEN  PhanBoLoiNhuan.TongConLai ELSE 0 END)"
                sql &= " )/ 100 "
                sql &= " ELSE "
                sql &= " (PhanBoLoiNhuan.PhuTrachHopDong + "
                sql &= " PhanBoLoiNhuan.PhuTrachChaoGia + "
                sql &= " (CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachKyHD, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= " THEN PhanBoLoiNhuan.XucTienKyHD ELSE 0 END) + "
                sql &= " (CASE WHEN ISNULL(BANGCHAOGIA.IDPhuTrachCT, BANGCHAOGIA.IDTakeCare) = BANGCHAOGIA.IDTakeCare "
                sql &= " THEN PhanBoLoiNhuan.PhuTrachQuanLyCT ELSE 0 END)+"
                sql &= " (CASE WHEN PHIEUXUATKHO.CongTrinh =0 THEN  PhanBoLoiNhuan.TongConLai ELSE 0 END)) / 100 END) END)"
                sql &= " as TongLNKD,"

                sql &= " 	 (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(tbLNPTSP.LoiNhuanPTSP,0) "
                sql &= "      - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END)) ) /((PHIEUXUATKHO.Tientruocthue+PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.Tygia)*100 END) AS PTLN, "
                sql &= "     (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tienchietkhau*PHIEUXUATKHO.TyGia)/(PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia)*100 END) AS PTCK,"
                sql &= " 	BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakeCare,NHANSU.Ten AS TakeCare,DEPATMENT.Ten AS Phong,BANGCHAOGIA.KhauTru,BANGCHAOGIA.TrangThai AS TrangThaiCG,BANGCHAOGIA.IDTakeCare,'' AS CBTT, (CASE WHEN tbTT.SoTien is null then 0 ELSE (CASE WHEN tbTT.SoTien <> (PHIEUXUATKHO.TienTruocThue + PHIEUXUATKHO.TienThue) THEN 2 ELSE 1 END) END) AS TTTT,"
                sql &= " PHIEUXUATKHO.TienGoc as TienGocChot,PHIEUXUATKHO.ChiPhi as ChiPhiChot,PHIEUXUATKHO.LoiNhuanChot, "
                sql &= " ISNULL(tbLNPTSP.LoiNhuanPTSP,0)LoiNhuanPTSP"
                sql &= " FROM  (SELECT NgayThangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1,convert(bit,0)Loai"
                sql &= "      FROM THU"
                sql &= "      UNION ALL"
                sql &= "      SELECT NgayThangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1,convert(bit,1)Loai"
                sql &= "      FROM THUNH) AS tbThu "
                sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(tbThu.NgaythangCT) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(tbThu.NgaythangCT)"
                sql &= " 	INNER JOIN KHACHHANG ON KHACHHANG.ID=tbThu.IDKH "
                sql &= " 	INNER JOIN"
                sql &= "     PHIEUXUATKHO ON tbThu.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tbThu.PhieuTC0=PHIEUXUATKHO.SoPhieuCG "
                sql &= "    LEFT JOIN (SELECT Sum(SoTien)SoTien, SoPhieu1 FROM tblCongNo WHERE Loai=0 GROUP BY SoPhieu1)tbTT ON tbTT.SoPhieu1=PHIEUXUATKHO.SoPhieu "

                sql &= "     INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID "
                If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND NHANSU.IDDepatment= " & cbPhong.EditValue
                End If
                sql &= "    LEFT JOIN (SELECT DISTINCT IDKhachHang FROM PHIEUXUATKHO WHERE datediff(day, PHIEUXUATKHO.NgayThang,@DenNgay)>365)tbKHMoi ON PHIEUXUATKHO.IDKhachHang=tbKHMoi.IDKhachHang "
                sql &= "     INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
                sql &= " LEFT JOIN NHANSU AS NGUOIKYHD ON NGUOIKYHD.ID=BANGCHAOGIA.IDPhuTrachKyHD"
                sql &= " LEFT JOIN NHANSU AS NVPHUTRACHCT ON NVPHUTRACHCT.ID=BANGCHAOGIA.IDPhuTrachCT"

                sql &= "   INNER JOIN  BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang "

                sql &= "     LEFT JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"
                'sql &= " LEFT JOIN View_XuatKhoTongGiaNhapTB ON PHIEUXUATKHO.Sophieu = View_XuatKhoTongGiaNhapTB.Sophieu "
                'sql &= "       LEFT JOIN   (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
                'sql &= "           FROM View_XuatKhoGiaNhapTB"
                'sql &= "           GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu "
                sql &= " LEFT JOIN View_XuatKhoTongGiaNhapTB ON PHIEUXUATKHO.Sophieu = View_XuatKhoTongGiaNhapTB.Sophieu "
                sql &= "    LEFT OUTER JOIN V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
                sql &= " LEFT JOIN"
                sql &= " ( SELECT SUM(PhatTrienSanPham_LoiNhuan.SoLuong*PhatTrienSanPham_LoiNhuan.LoiNhuan) as LoiNhuanPTSP,PhatTrienSanPham_LoiNhuan.SoPhieu FROM PhatTrienSanPham_LoiNhuan"
                sql &= "    INNER JOIN PHIEUXUATKHO as PX ON PX.SoPhieu=PhatTrienSanPham_LoiNhuan.SoPhieu"
                sql &= "    AND CONVERT(datetime,Convert(nvarchar,PX.NgayThang,103),103) between @TuNgay AND @DenNgay "
                sql &= "    GROUP BY PhatTrienSanPham_LoiNhuan.SoPhieu)tbLNPTSP ON tbLNPTSP.SoPhieu=PHIEUXUATKHO.SoPhieu"
                sql &= "    LEFT JOIN PhanBoLoiNhuan ON PhanBoLoiNhuan.Thang=Right(Convert(nvarchar,PHIEUXUATKHO.NgayTHang,103),7) AND PhanBoLoiNhuan.CT=PHIEUXUATKHO.CongTrinh"
                sql &= " WHERE CONVERT(datetime,Convert(nvarchar,tbThu.NgayThangCT,103),103) between @TuNgay AND @DenNgay"
                If Not cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND PHIEUXUATKHO.IDTakeCare =" & cbTakeCare.EditValue
                End If
                sql &= " ORDER BY tbThu.SoPhieu"

                If chkMoi.Checked = True Then
                    sql = meSql.Text
                    If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                        AddParameter("@IDDepatment", cbPhong.EditValue)
                    Else
                        sql = sql.Replace("AND NHANSU.IDDepatment= @IDDepatment", "")
                    End If
                    If Not cbTakeCare.EditValue Is Nothing Then
                        AddParameter("@IDTakeCare", cbTakeCare.EditValue)
                    Else
                        sql = sql.Replace("AND PHIEUXUATKHO.IDTakeCare =@IDTakeCare", "")
                    End If
                    AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
                    AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
                    'AddParameterWhere("@IDTakeCare", cbTakeCare.EditValue)
                    tb = ExecuteSQLDataTable(sql)
                    gcXe.DataSource = Nothing
                    If SplitContainerControl1.Collapsed = False Then
                        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
                        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
                        sql = meTaoBangPhanBo.Text
                        tblCommonTable = ExecuteSQLDataTable(sql)
                        tblAddedTempTable = tblCommonTable.Clone()
                        ' tblCommonTable = tbPhanBo
                        tblAddedTempTable.Rows.Clear()
                        ' Gọi xử lý dữ liệu
                        PhantichBang()

                        'Nhập những dòng đã thêm (khi tiền thu < giá xuất kho) vào chung bảng dữ liệu
                        For Each item In tblAddedTempTable.Rows
                            tblCommonTable.ImportRow(item)
                        Next

                        Dim view As DataView = tblCommonTable.DefaultView
                        view.Sort = "PhieuTC0 desc, PhieuRef ASC" ' Sắp xếp cột tăng dần
                        For i = 0 To tb.Rows.Count - 1
                            Dim tam = tb.Rows(i)("Thu")
                            tb.Rows(i)("Thu") = -1
                            For j = 0 To tblCommonTable.Rows.Count - 1
                                If tb.Rows(i)("SoPhieuCG") = NullToString(tblCommonTable.Rows(j)("PhieuTC0")) And "XK" + tb.Rows(i)("SoPhieuXK") = NullToString(tblCommonTable.Rows(j)("PhieuRef")) And tb.Rows(i)("SoPhieu") = NullToString(tblCommonTable.Rows(j)("SoPhieu")) Then
                                    tb.Rows(i)("Thu") = NullToDouble(tblCommonTable.Rows(j)("PhanBo"))
                                Else

                                    If "CG" + tb.Rows(i)("SoPhieuCG") = NullToString(tblCommonTable.Rows(j)("PhieuRef")) And tb.Rows(i)("SoPhieu") = NullToString(tblCommonTable.Rows(j)("SoPhieu")) Then
                                        tb.Rows(i)("Thu") = tam

                                    End If
                                End If
                            Next
                        Next
                        Dim sodong = tb.Rows.Count
                        For i = 0 To sodong - 1
                            If i > sodong - 1 Then Exit For
                            If tb.Rows(i)("Thu") = -1 Then
                                tb.Rows.Remove(tb.Rows(i))
                                i -= 1
                                sodong -= 1
                            End If
                        Next
                        gcXe.DataSource = view
                    Else

                        For i = 0 To tb.Rows.Count - 1
                            Dim SoCG = tb.Rows(i)("SophieuCG")
                            For j = i To tb.Rows.Count - 1
                                Dim tienthu = tb.Rows(j)("Thu")
                                If tb.Rows(j)("SophieuCG") = SoCG And tb.Rows(j)("PhieuTC0") <> "000000000" And tb.Rows(j)("GiaTriXK") <> 0 Then
                                    Dim SoPhieuXK = tb.Rows(j)("SoPhieuXK")
                                    Dim SoPhieuThu = tb.Rows(j)("SoPhieu")
                                    If tienthu >= tb.Rows(j)("GiaTriXK") Then
                                        tienthu = tienthu - tb.Rows(j)("GiaTriXK")
                                        tb.Rows(j)("Thu") = tb.Rows(j)("GiaTriXK")
                                        tb.Rows(j)("ConLai") = tienthu
                                        tb.Rows(j)("isPhanBo") = 1
                                        tb.Rows(j)("GiaTriXK") = 0
                                        tb.Rows(j)("GiaTriXK2") = tb.Rows(j)("TienTruocThue") + tb.Rows(j)("TienThue")
                                        '        tb.Rows(j)("TienTruocThue") = 0
                                        For k = j + 1 To tb.Rows.Count - 1
                                            If tb.Rows(k)("SoPhieuXK") = SoPhieuXK Then
                                                tb.Rows(k)("TienTruocThue") = 0
                                                tb.Rows(k)("GiaTriXK") = 0

                                            End If
                                            If tb.Rows(k)("SoPhieu") = SoPhieuThu Then
                                                tb.Rows(k)("Thu") = tienthu
                                            End If
                                        Next
                                    Else
                                        Dim GiaTriXK = tb.Rows(j)("GiaTriXK") - tienthu
                                        tb.Rows(j)("GiaTriXK2") = tienthu
                                        tb.Rows(j)("Thu") = tienthu
                                        tb.Rows(j)("ConLai") = 0
                                        tb.Rows(j)("isPhanBo") = 1
                                        tb.Rows(j)("GiaTriXK") = 0
                                        '     tb.Rows(j)("TienTruocThue") = 0
                                        tienthu = 0
                                        For k = j + 1 To tb.Rows.Count - 1
                                            If tb.Rows(k)("SoPhieuXK") = SoPhieuXK Then
                                                '  tb.Rows(k)("TienTruocThue") = GiaTriXK
                                                tb.Rows(k)("GiaTriXK") = GiaTriXK
                                            End If
                                            If tb.Rows(k)("SoPhieu") = SoPhieuThu Then
                                                tb.Rows(k)("Thu") = tienthu
                                            End If
                                        Next
                                    End If


                                End If


                            Next

                        Next
                        Dim d = tb.Rows.Count - 1
                        For i = 0 To tb.Rows.Count - 1
                            If tb.Rows(i)("Thu") = 0 Or tb.Rows(i)("TienTruocThue") = 0 Then
                                tb.Rows(i).Delete()
                            Else
                                If chkLNMoi.Checked = True Then
                                    tb.Rows(i)("HeSo") = NullToDouble(tb.Rows(i)("Thu") / (tb.Rows(i)("TienTruocThue") + tb.Rows(i)("Tienthue")))
                                    tb.Rows(i)("LoiNhuanKT") = NullToDouble(tb.Rows(i)("LoiNhuanKT2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("LoiNhuanKD") = NullToDouble(tb.Rows(i)("LoiNhuanKD2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("LNThiCong") = NullToDouble(tb.Rows(i)("LNThiCong2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("LNXucTienHD") = NullToDouble(tb.Rows(i)("LNXucTienHD2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("LNPhuTrachCT") = NullToDouble(tb.Rows(i)("LNPhuTrachCT2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("LNKSChaoGia") = NullToDouble(tb.Rows(i)("LNKSChaoGia2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("LNThiCong") = NullToDouble(tb.Rows(i)("LNThiCong2")) * tb.Rows(i)("HeSo")
                                    tb.Rows(i)("TongLNKD") = NullToDouble(tb.Rows(i)("TongLNKD2")) * tb.Rows(i)("HeSo")
                                End If

                            End If

                        Next
                        tb.AcceptChanges()
                    End If

                Else
                    AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
                    AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
                    'AddParameterWhere("@IDTakeCare", cbTakeCare.EditValue)
                    tb = ExecuteSQLDataTable(sql)
                End If

            End If



            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub
    Private Function NullToString(d As Object) As String

        If IsDBNull(d) Then
            Return ""
        End If

        Return d

    End Function
    Private Function NullToDouble(d As Object) As Double

        If IsDBNull(d) Then
            Return 0
        End If

        Return d

    End Function
    Private tblAddedTempTable As DataTable
    Private tblCommonTable As DataTable
    ' Xử lý dữ liệu bảng CSDL
    Private Sub PhantichBang2()
        Dim tb As DataTable = CType(gdv.DataSource, DataTable)
        For i = 0 To tb.Rows.Count - 1
            Dim tienthu = tb.Rows(i)("Thu")
            Dim SoCG = tb.Rows(i)("SophieuCG")
            For j = i To tb.Rows.Count - 1
                If tb.Rows(j)("SophieuCG") = SoCG And tb.Rows(j)("PhieuTC0") <> "000000000" Then
                    If tienthu >= tb.Rows(j)("GiaTriXK") Then
                        tienthu = tienthu - tb.Rows(j)("GiaTriXK")
                        Dim SoPhieuXK = tb.Rows(j)("SoPhieuXK")
                        For k = j + 1 To tb.Rows.Count - 1
                            If tb.Rows(k)("SoPhieuXK") = SoPhieuXK Then
                                tb.Rows(k)("GiaTriXK") = 0
                            End If
                        Next
                    End If
                End If

            Next
        Next

    End Sub
    Private Sub PhantichBang()
        ' Biến lưu số chào giá, số phiếu ref
        Dim strSoChaogia = "", strPhieuref = ""
        ' Biến lưu tiền thu, giá trị xuất kho
        Dim tienthu As Integer = 0, giatri As Integer = 0

        ' Biến mảng kiểu Enumerable, dùng để duyệt số phiếu
        Dim rowSoPhieuSet As DataRow()
        ' Biến mảng kiểu Enumerable, dùng để duyệt các bản ghi xuất kho
        Dim rowPhanBoSet As DataRow()
        ' Biến mảng kiểu Enumerable, dùng để duyệt các dòng của bảng tblAddedTempTable
        Dim rowPhanBoSetNewTable As DataRow()

        Dim temp = 0 ' Lưu giá trị tạm khi trừ: Giá trị - Tiền thu
        Dim runner = 0 ' Biến này để lưu công sai (+1) cho cột Id, để cột Id duy nhất. runner dùng để lưu vào giá trị Id khi thêm dòng mới.
        Dim positionRunner = 0 ' Biến này lưu vị trí khi duyệt bảng

        ' Duyệt số chào giá
        For Each rowSoChaogia As DataRow In tblCommonTable.Rows
            rowSoPhieuSet = tblCommonTable.Select("Sophieu is not null")
            strSoChaogia = rowSoChaogia("PhieuTC0").ToString()

            ' Duyệt số phiếu
            For Each rowSoPhieu As DataRow In rowSoPhieuSet
                tienthu = Integer.Parse(rowSoPhieu("TienThu").ToString())
                positionRunner = 0

                ' BẮT ĐẦU KHỐI XỬ LÝ VỚI BẢNG THÊM MỚI DÒNG, KHI BẢNG NÀY CÓ BẢN GHI
                If tblAddedTempTable.Rows.Count > 0 Then
                    rowPhanBoSetNewTable = tblAddedTempTable.Select("Sophieu is null And PhieuRef is not null And PhieuTC0 = '" & strSoChaogia & "' And PhanBo is null", "PhieuTC0 desc, PhieuRef  ASC")

                    ' Duyệt những giá trị xuất kho
                    For i = 0 To rowPhanBoSetNewTable.Length - 1

                        '  Next
                        'For Each rowPhanbo As DataRow In rowPhanBoSetNewTable
                        Dim rowPhanbo As DataRow = rowPhanBoSetNewTable(i)
                        giatri = Integer.Parse(rowPhanbo("GiaTri").ToString())
                        strPhieuref = rowPhanbo("PhieuRef").ToString()
                        If temp = 0 AndAlso positionRunner = 0 Then
                            If tienthu >= giatri Then

                                ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                                Dim rowsToUpdate = tblAddedTempTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                                For Each row In rowsToUpdate
                                    row("PhanBo") = giatri
                                    row("SoPhieu") = rowSoPhieu("SoPhieu")
                                Next

                                ' Lưu lại giá trị thừa vào biến temp
                                temp = tienthu - giatri
                            Else
                                ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                                ' Dim rowsToUpdate = tblAddedTempTable.Select(String.Format("Sophieu='{0}' and PhieuRef='{1}' and PhieuTC0='{2}'", "", strPhieuref, strSoChaogia))
                                Dim rowsToUpdate = tblAddedTempTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                                'tblAddedTempTable.AsEnumerable.Where(Function(customer) customer("Sophieu").ToString() = "" And customer("PhieuRef").ToString() = strPhieuref And customer("PhieuTC0").ToString() = strSoChaogia)
                                For Each row In rowsToUpdate
                                    row("PhanBo") = tienthu
                                    row("SoPhieu") = rowSoPhieu("SoPhieu")
                                Next

                                ' Nếu tiền thu < xuất kho thì thêm mới dòng và thoát vòng lặp
                                runner = runner + 1
                                Dim newRow As DataRow = tblAddedTempTable.NewRow()
                                ' newRow("SoPhieu") = rowSoPhieu("SoPhieu")
                                newRow("PhieuTC0") = strSoChaogia
                                newRow("PhieuRef") = strPhieuref
                                newRow("GiaTri") = giatri - tienthu
                                newRow("TienThu") = 0
                                newRow("Id") = 500000 + runner
                                tblAddedTempTable.Rows.Add(newRow)
                                Exit For
                            End If

                        ElseIf temp > 0 AndAlso positionRunner > 0 Then ' Nếu đã duyệt từ dòng thứ 2 trở đi

                            ' Khi Runtime, temp vẫn có thể có giá trị <= 0, cứ đặt kiểm tra vào.
                            If temp <= 0 Then
                                Exit For
                            End If

                            If temp >= giatri Then
                                Dim rowsToUpdate = tblAddedTempTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                                For Each row In rowsToUpdate
                                    row("PhanBo") = giatri
                                    row("SoPhieu") = rowSoPhieu("SoPhieu")
                                Next

                                temp = temp - giatri
                            Else
                                ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                                Dim rowsToUpdate = tblAddedTempTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                                For Each row In rowsToUpdate
                                    row("PhanBo") = temp
                                    row("SoPhieu") = rowSoPhieu("SoPhieu")
                                Next

                                ' Nếu tiền thu < xuất kho thì thêm mới dòng và thoát vòng lặp
                                runner = runner + 1
                                Dim newRow As DataRow = tblAddedTempTable.NewRow()
                                'newRow("SoPhieu") = rowSoPhieu("SoPhieu")
                                newRow("PhieuTC0") = strSoChaogia
                                newRow("PhieuRef") = strPhieuref
                                newRow("GiaTri") = giatri - tienthu
                                newRow("TienThu") = 0
                                newRow("Id") = 500000 + runner
                                tblAddedTempTable.Rows.Add(newRow)
                                Exit For
                            End If
                        End If

                        positionRunner = positionRunner + 1
                    Next

                    ' Nếu positionRunner chưa chạy đến dòng cuối cùng của Datatable thì để temp = 0 để khởi đầu chu trình lặp khác.
                    If positionRunner < (rowPhanBoSetNewTable.Length - 1) Then temp = 0
                End If


                ' BẮT ĐẦU KHỐI XỬ LÝ KHI KHÔNG CÓ BẢN GHI NÀO TRONG BẢNG THÊM MỚI tblAddedTempTable
                ' KHỐI CODE NÀY LẶP LẠI, CỨ ĐỂ CHUNG VÀO THÂN HÀM NÀY CŨNG ĐƯỢC.

                rowPhanBoSet = tblCommonTable.Select("Sophieu is null And PhieuRef is not null And PhieuTC0 = '" & strSoChaogia & "' And PhanBo is null")
                For Each rowPhanbo As DataRow In rowPhanBoSet
                    giatri = Integer.Parse(rowPhanbo("GiaTri").ToString())
                    strPhieuref = rowPhanbo("PhieuRef").ToString()
                    If temp = 0 AndAlso positionRunner = 0 Then
                        If tienthu >= giatri Then

                            ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                            Dim rowsToUpdate = tblCommonTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                            For Each row In rowsToUpdate
                                row("PhanBo") = giatri
                                row("SoPhieu") = rowSoPhieu("SoPhieu")
                            Next

                            temp = tienthu - giatri
                            positionRunner = positionRunner + 1
                            ' Khi đã cập nhật được phân bổ, nhảy tiếp sang xuất kho tiếp
                            Continue For
                        Else
                            ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                            Dim rowsToUpdate = tblCommonTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                            For Each row In rowsToUpdate
                                row("PhanBo") = tienthu
                                row("SoPhieu") = rowSoPhieu("SoPhieu")
                            Next

                            ' Nếu tiền thu < xuất kho thì thêm mới dòng và thoát vòng lặp
                            runner = runner + 1
                            Dim newRow As DataRow = tblAddedTempTable.NewRow()
                            'newRow("SoPhieu") = rowSoPhieu("SoPhieu")
                            newRow("PhieuTC0") = strSoChaogia
                            newRow("PhieuRef") = strPhieuref
                            newRow("GiaTri") = giatri - tienthu
                            newRow("TienThu") = 0
                            newRow("Id") = 500000 + runner
                            tblAddedTempTable.Rows.Add(newRow)
                            Exit For
                        End If
                    End If

                    ' Nếu temp thoát vòng lặp này
                    If temp < 0 Then
                        Exit For
                    End If

                    If temp > 0 AndAlso positionRunner > 0 Then
                        ' Nếu temp thoát vòng lặp này. Khi Runtime, temp vẫn có thể có giá trị <= 0, cứ đặt kiểm tra vào.
                        If temp <= 0 Then
                            Exit For
                        End If

                        If temp >= giatri Then
                            ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                            Dim rowsToUpdate = tblCommonTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                            For Each row In rowsToUpdate
                                row("PhanBo") = giatri
                                row("SoPhieu") = rowSoPhieu("SoPhieu")
                            Next

                            temp = temp - giatri
                        Else
                            ' Kiểm tra thoả mãn điều kiện để cập nhật Phân bổ
                            Dim rowsToUpdate = tblCommonTable.Select("Sophieu is null And PhieuRef = '" & strPhieuref & "' And PhieuTC0 = '" & strSoChaogia & "'")
                            For Each row In rowsToUpdate
                                row("PhanBo") = temp
                                row("SoPhieu") = rowSoPhieu("SoPhieu")
                            Next

                            ' Nếu tiền thu < xuất kho thì thêm mới dòng và thoát vòng lặp
                            runner = runner + 1
                            Dim newRow As DataRow = tblAddedTempTable.NewRow()
                            'newRow("SoPhieu") = rowSoPhieu("SoPhieu")
                            newRow("PhieuTC0") = strSoChaogia
                            newRow("PhieuRef") = strPhieuref
                            newRow("GiaTri") = giatri - temp
                            newRow("TienThu") = 0
                            newRow("Id") = 500000 + runner
                            tblAddedTempTable.Rows.Add(newRow)
                            Exit For
                        End If
                    End If

                    positionRunner = positionRunner + 1
                Next

                ' Temp = 0, để khởi đầu cho chu trình vòng lặp khác.
                temp = 0
            Next
        Next

    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub rcbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakeCare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakeCare.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        On Error Resume Next
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "Thu" Then
            If (gdvCT.GetRowCellValue(e.RowHandle, "TienTruocThue") + gdvCT.GetRowCellValue(e.RowHandle, "TienThue")) <> e.CellValue Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "CBTT" Then
            If gdvCT.GetRowCellValue(e.RowHandle, "TTTT") = 0 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvCT.GetRowCellValue(e.RowHandle, "TTTT") = 2 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "IDDVVC" Then

            If IsDBNull(e.CellValue) Then Exit Sub
            If gdvCT.GetRowCellValue(e.RowHandle, "SL") > 1 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "SoTien" Then
            If IsDBNull(e.CellValue) Then Exit Sub
            If gdvCT.GetRowCellValue(e.RowHandle, "SoTien") <> gdvCT.GetRowCellValue(e.RowHandle, "SoTienTC") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If

    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        LoadDS2()
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        If cbTieuChi.EditValue = "Xuất kho" Then
            saveFile.FileName = "Loi nhuan theo xuat kho.xls"
        Else
            saveFile.FileName = "Loi nhuan theo phieu thu.xls"
        End If

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

    Private Sub gdvCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCT.CustomSummaryCalculate
        On Error Resume Next
        If e.IsGroupSummary Then
            If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "PTLN" Then
                e.TotalValue = ((gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn12) + gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn13)) / (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn5) + gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn6))) * 100
            End If
            If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "PTCK" Then
                e.TotalValue = (gdvCT.GetGroupRowValue(e.GroupRowHandle, colChietKhau) / (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn5) + gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn6))) * 100
            End If
        End If

        If e.IsTotalSummary Then
            If e.IsTotalSummary And CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "PTLN" Then
                e.TotalValue = ((gdvCT.Columns("LoiNhuanKD").SummaryItem.SummaryValue + gdvCT.Columns("LoiNhuanKT").SummaryItem.SummaryValue) / (gdvCT.Columns("TienTruocThue").SummaryItem.SummaryValue + gdvCT.Columns("TienThue").SummaryItem.SummaryValue)) * 100
            End If
            If e.IsTotalSummary And CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "PTCK" Then
                e.TotalValue = (gdvCT.Columns("TienChietKhau").SummaryItem.SummaryValue / (gdvCT.Columns("TienTruocThue").SummaryItem.SummaryValue + gdvCT.Columns("TienThue").SummaryItem.SummaryValue)) * 100
            End If
        End If

    End Sub

    Private Sub btXemPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemPhieuThu.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim f As New frmCNThu2
        f.PhieuThu = gdvCT.GetFocusedRowCellValue("SoPhieu")
        f.ThuNH = gdvCT.GetFocusedRowCellValue("Loai")
        f.btThem.Visible = False
        f.btGhi.Visible = False
        f.ShowDialog()
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
        If gdvCT.Columns("Loai").Visible = False Then
            btXemPhieuThu.Visibility = XtraBars.BarItemVisibility.Never
        Else
            btXemPhieuThu.Visibility = XtraBars.BarItemVisibility.Always
        End If

        If colVCDVVC.Visible Then
            btLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mNhapThongTinVC.Caption = "Ẩn đề nghị chi vận chuyển"
            If IsDBNull(gdvCT.GetFocusedRowCellValue("IDDVVC")) Then
                mThemChiPhiMoi.Visibility = XtraBars.BarItemVisibility.Never
            Else
                mThemChiPhiMoi.Visibility = XtraBars.BarItemVisibility.Always
            End If
            mDuaVaoDSChungCP.Visibility = XtraBars.BarItemVisibility.Always
            If Not IsDBNull(gdvCT.GetFocusedRowCellValue("SoTienTC")) Then
                If gdvCT.GetFocusedRowCellValue("SoTienTC") <> gdvCT.GetFocusedRowCellValue("SoTien") Then
                    mSuaChiPhiVCChung.Visibility = XtraBars.BarItemVisibility.Always
                Else
                    mSuaChiPhiVCChung.Visibility = XtraBars.BarItemVisibility.Never
                End If
            End If

        Else
            btLuuLai.Visibility = XtraBars.BarItemVisibility.Never
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Never
            mNhapThongTinVC.Caption = "Đề nghị chi vận chuyển"
            mDuaVaoDSChungCP.Visibility = XtraBars.BarItemVisibility.Never
            mSuaChiPhiVCChung.Visibility = XtraBars.BarItemVisibility.Never
        End If

    End Sub

    Private Sub btXemCG_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemCG.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isUpdate = True
        fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNChaoGia.Tag = deskTop.mChaoGia.Name

        fCNChaoGia.Show()
    End Sub

    Private Sub btXemChiPhi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChiPhi.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT CHI.SoPhieu,Convert(bit,0)UNC,CHI.NgayThangCT AS NgayThang,DienGiai,SoTien,PhieuTC0,PhieuTC1,TamUng,"
        sql &= " KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich"
        sql &= " FROm CHI "
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=CHI.IDKH"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=CHI.MucDich"
        sql &= " WHERE ((CHI.MucDich=205 AND CHI.ChiPhiNhap=0) OR CHi.MucDich IN (235, 244)) AND (CHI.PhieuTC0='" & gdvCT.GetFocusedRowCellValue("SoPhieuCG").ToString & "' OR CHI.PhieuTC1='" & gdvCT.GetFocusedRowCellValue("SoPhieuXK").ToString & "')"
        sql &= " UNION ALL"
        sql &= " SELECT UNC.SoPhieu,Convert(bit,0)UNC,UNC.NgayThang,DienGiai,SoTien,PhieuTC0,PhieuTC1,TamUng,"
        sql &= " KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich"
        sql &= " FROm UNC "
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=UNC.IDKH"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=UNC.MucDich"
        sql &= " WHERE ((UNC.MucDich=205 AND UNC.ChiPhiNhap=0) OR UNC.MucDich IN (235, 244)) AND (UNC.PhieuTC0='" & gdvCT.GetFocusedRowCellValue("SoPhieuCG").ToString & "' OR UNC.PhieuTC1='" & gdvCT.GetFocusedRowCellValue("SoPhieuXK").ToString & "')"
        sql &= " ORDER BY NgayThang DESC"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
        Dim f As New frmTongHopChiPhi
        f.gdv.DataSource = tb
        f.ShowDialog()
    End Sub

    Private Sub btXemXK_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemXK.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.PhieuXK = gdvCT.GetFocusedRowCellValue("SoPhieuXK")
        fCNXuatKho.Tag = deskTop.mXuatKho.Name
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.QuyenSua) Then
            fCNXuatKho.btGhi.Enabled = False
            fCNXuatKho.btChuyenXK.Enabled = False
            fCNXuatKho.btCal.Enabled = False
            fCNXuatKho.btTichThue.Enabled = False
            fCNXuatKho.mChonBoChon.Enabled = False
        End If
        fCNXuatKho.ShowDialog()

    End Sub

    Private Sub btLocKHMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            If cbTieuChi.EditValue = "Xuất kho" Then
                colThuNH.Visible = False
                colPhieuThu.Visible = False
                sql &= " Select DISTINCT"
                sql &= "   KHACHHANG.ttcMa, PHIEUXUATKHO.SoPhieu AS SoPhieuXK,BANGCHAOGIA.SoPhieu AS SoPhieuCG, PHIEUXUATKHO.CongTrinh, PHIEUXUATKHO.error, "
                sql &= "   PHIEUXUATKHO.TientruocThue * PHIEUXUATKHO.Tygia AS TienTruocThue, PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue, "
                sql &= "   ISNULL(V_XuatkhoGianhap.tongnhap, 0) AS TienGoc, ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, "
                sql &= "   PHIEUXUATKHO.TienChietKhau, ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0) + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0) AS ChiCK, "
                If chkMoi.Checked = True Then
                    sql &= "   ISNULL(V_XuatkhoThuNH_TheoXK.ThuNH, 0) + ISNULL(V_XuatkhoThuTM_TheoXK.ThuTM, 0)+ PHIEUXUATKHO.PhanBoTamUng AS Thu,"
                Else
                    sql &= "   ISNULL(V_XuatkhoThuNH.ThuNH, 0) + ISNULL(V_XuatkhoThuTM.ThuTM, 0) AS Thu,"
                End If

                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) /  (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) * (ISNULL(tblTuDien.Diem, 0) / 100) "
                sql &= "     AS LoiNhuanKT, "
                sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - (ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) * PHIEUXUATKHO.Tygia) * ((100 - ISNULL(tblTuDien.Diem, 0)) / 100) "
                sql &= "     AS LoiNhuanKD, "
                sql &= "     (CASE (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN, "
                sql &= "    (CASE (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tienchietkhau*PHIEUXUATKHO.TyGia)/((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)*100 END) AS PTCK,"
                sql &= "  PHIEUXUATKHO.IDTakeCare, LEFT(PHIEUXUATKHO.Sophieu, 4) "
                sql &= "   AS YearMonth, NHANSU.Ten AS TakeCare,DEPATMENT.Ten AS Phong,BANGCHAOGIA.TrangThai AS TrangThaiCG,BANGCHAOGIA.IDTakeCare"
                sql &= " FROM PHIEUXUATKHO "
                sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
                sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachhang"
                sql &= " LEFT JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
                sql &= " INNER JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"
                If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND DEPATMENT.ID= " & cbPhong.EditValue
                End If
                sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
                sql &= " LEFT JOIN BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang "
                sql &= " LEFT JOIN tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID "
                sql &= " LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
                sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
                sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
                If chkMoi.Checked = True Then
                    sql &= " LEFT JOIN V_XuatkhoThuNH_TheoXK ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuNH_TheoXK.Sophieu "
                    sql &= " LEFT JOIN V_XuatkhoThuTM_TheoXK ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuTM_TheoXK.Sophieu "
                Else
                    sql &= " LEFT JOIN V_XuatkhoThuNH ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuNH.Sophieu "
                    sql &= " LEFT JOIN V_XuatkhoThuTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoThuTM.Sophieu "
                End If
                sql &= " LEFT JOIN V_XuatkhoChietkhauTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChietkhauTM.Sophieu "
                sql &= " LEFT JOIN V_XuatkhoChietkhauUNC ON PHIEUXUATKHO.Sophieu = V_XuatkhoChietkhauUNC.Sophieu "
                sql &= " WHERE CONVERT(datetime,PHIEUXUATKHO.NgayThang,103) between convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND Convert(datetime,'" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
                If Not cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND PHIEUXUATKHO.IDTakeCare =" & cbTakeCare.EditValue
                End If
                sql &= " AND PHIEUXUATKHO.IDKhachHang Not IN (SELECT DISTINCT IDKhachHang FROM PHIEUXUATKHO WHERE datediff(month, PHIEUXUATKHO.NgayThang,convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103))>3)"

                sql &= " ORDER BY PHIEUXUATKHO.SoPhieu"

            Else
                colPhieuThu.Visible = True
                colPhieuThu.VisibleIndex = 1
                colThuNH.Visible = True
                colThuNH.VisibleIndex = 2
                sql &= " SELECT KHACHHANG.ttcMa, tbThu.NgaythangCT AS Ngay, tbThu.SoPhieu,tbThu.Loai, PHIEUXUATKHO.error, PHIEUXUATKHO.SoPhieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
                sql &= " 	BANGCHAOGIA.MaSoDatHang,PHIEUXUATKHO.CongTrinh,PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia AS TienTruocThue, "
                sql &= " 	PHIEUXUATKHO.Tienthue * PHIEUXUATKHO.Tygia AS TienThue,tbThu.Sotien AS TienThu, ISNULL(tb.GiaNhap,0) AS TienGoc, "
                sql &= " 	ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) AS ChiPhi, "
                sql &= " 	ISNULL(PHIEUXUATKHO.TienChietKhau,0)*PHIEUXUATKHO.TyGia AS TienChietKhau,"
                sql &= "    ISNULL(V_XuatkhoChietkhauTM.ChietkhauTM, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))  + ISNULL(V_XuatkhoChietkhauUNC.ChietkhauUNC, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))  AS ChiCK, tbThu.SoTien AS Thu,"
                sql &= "     PHIEUXUATKHO.tienchietkhau * PHIEUXUATKHO.Tygia AS TienChietKhau, (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) "
                sql &= "     * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE (((PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0)) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)) ) "
                sql &= "     * (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia))) * (ISNULL(tblTuDien.Diem, 0) / 100) END) "
                sql &= "     AS LoiNhuanKT, (CASE ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia) "
                sql &= "     WHEN 0 THEN 0 ELSE (((PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0)) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) * ISNULL(PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)) ) "
                sql &= "     * (tbThu.Sotien / ((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia))) * ((100 - ISNULL(tblTuDien.Diem, 0)) / 100) "
                sql &= "     END) AS LoiNhuanKD, "
                sql &= " 	 (CASE (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
                sql &= "      - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)) ) /((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)*100 END) AS PTLN, "
                sql &= "     (CASE (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tienchietkhau*PHIEUXUATKHO.TyGia)/((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia)*100 END) AS PTCK,"
                sql &= " 	BANGYEUCAU.IDLoaiYeuCau, PHIEUXUATKHO.IDTakeCare,NHANSU.Ten AS TakeCare,DEPATMENT.Ten AS Phong,BANGCHAOGIA.TrangThai AS TrangThaiCG,BANGCHAOGIA.IDTakeCare"
                sql &= " FROM  (SELECT NgayThangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1,convert(bit,0)Loai"
                sql &= "      FROM          THU"
                sql &= "      UNION ALL"
                sql &= "      SELECT     NgayThangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1,convert(bit,1)Loai"
                sql &= "      FROM         THUNH) AS tbThu "
                sql &= " LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(tbThu.NgaythangCT) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4)) = YEAR(tbThu.NgaythangCT)"
                sql &= " 	INNER JOIN KHACHHANG ON KHACHHANG.ID=tbThu.IDKH "
                sql &= " 	INNER JOIN"
                sql &= "     PHIEUXUATKHO ON tbThu.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tbThu.PhieuTC0=PHIEUXUATKHO.SoPhieuCG "
                sql &= "     INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG INNER JOIN"
                sql &= "     BANGYEUCAU ON BANGYEUCAU.Sophieu = BANGCHAOGIA.Masodathang "
                sql &= "     INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID "
                sql &= "        INNER JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"
                If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND DEPATMENT.ID= " & cbPhong.EditValue
                End If
                sql &= "     LEFT OUTER JOIN tblTuDien ON BANGYEUCAU.IDLoaiYeuCau = tblTuDien.ID LEFT JOIN"
                sql &= "         (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
                sql &= "           FROM          V_XuatkhoCal"
                sql &= "           GROUP BY Sophieu) AS tb ON tb.Sophieu = PHIEUXUATKHO.Sophieu LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChiphiTM ON V_XuatkhoChiphiTM.Sophieu = tbThu.PhieuTC1 LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChiphiUnc ON V_XuatkhoChiphiUnc.Sophieu = tbThu.PhieuTC1 LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChietkhauTM ON V_XuatkhoChietkhauTM.Sophieu = tbThu.PhieuTC1 LEFT OUTER JOIN"
                sql &= "     V_XuatkhoChietkhauUNC ON V_XuatkhoChietkhauUNC.Sophieu = tbThu.PhieuTC1"
                sql &= " WHERE CONVERT(datetime,tbThu.NgayThangCT,103) between convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND Convert(datetime,'" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
                If Not cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND PHIEUXUATKHO.IDTakeCare =" & cbTakeCare.EditValue
                End If
                sql &= " AND PHIEUXUATKHO.IDKhachHang Not IN (SELECT DISTINCT IDKhachHang FROM PHIEUXUATKHO WHERE datediff(month, PHIEUXUATKHO.NgayThang,convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103))>3)"

                sql &= " ORDER BY tbThu.SoPhieu"

            End If
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Private Sub mDuKienTT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienTT.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieuCG")
        f._SoPhieuXNK = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieuXK")
        f._PhaiTra = False
        f._Buoc1 = False
        f.ShowDialog()
    End Sub

#Region "Thông tin vận chuyển"

    Private Sub mNhapThongTinVC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhapThongTinVC.ItemClick
        If mNhapThongTinVC.Caption = "Đề nghị chi vận chuyển" Then
            colVCDVVC.VisibleIndex = 5
            colVCThoiGian.VisibleIndex = 6
            colVCSoBill.VisibleIndex = 7

            colVCSoTien.VisibleIndex = 8
            colVCCanNang.VisibleIndex = 9
            colVCGhiChu.VisibleIndex = 10
            colVCTienTe.VisibleIndex = 11
            colVCTyGia.VisibleIndex = 12
            colVCNguoiNhap.VisibleIndex = 13
            gdvCT.Focus()
            gdvCT.FocusedColumn = colVCDVVC
        Else
            colVCDVVC.VisibleIndex = -1
            colVCThoiGian.VisibleIndex = -1
            colVCSoBill.VisibleIndex = -1

            colVCSoTien.VisibleIndex = -1
            colVCCanNang.VisibleIndex = -1
            colVCGhiChu.VisibleIndex = -1
            colVCTienTe.VisibleIndex = -1
            colVCTyGia.VisibleIndex = -1
            colVCNguoiNhap.VisibleIndex = -1
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Never
            btLuuLai.Visibility = XtraBars.BarItemVisibility.Never

        End If
    End Sub



    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "IDDVVC" Then
            gdvCT.SetFocusedRowCellValue("ThoiGian", Now)
        ElseIf e.Column.FieldName = "SoTien" Then
            gdvCT.SetFocusedRowCellValue("TienTe", 0)
        ElseIf e.Column.FieldName = "SoBill" Then
            gdvCT.SetFocusedRowCellValue("CanNang", 0)
        ElseIf e.Column.FieldName = "TienTe" Then
            If IsDBNull(e.Value) Then
                gdvCT.SetFocusedRowCellValue("TyGia", DBNull.Value)
            Else
                Dim r() As DataRow = CType(rcbTienTe.DataSource, DataTable).Select("ID=" & e.Value)
                gdvCT.SetFocusedRowCellValue("TyGia", r(0)("TyGia"))
            End If

        End If

        If e.Column.FieldName <> "Modify" And e.Column.FieldName <> "IDVC" Then
            gdvCT.SetFocusedRowCellValue("Modify", True)
            btLuuLai.Visibility = XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown

        If e.KeyCode = Keys.Enter Then
            If colVCDVVC.Visible Then
                If Not gdvCT.IsEditing Then
                    gdvCT.Focus()
                    If gdvCT.FocusedRowHandle < gdvCT.RowCount Then
                        gdvCT.FocusedRowHandle = gdvCT.FocusedRowHandle + 1
                        gdvCT.FocusedColumn = colVCDVVC
                    End If
                End If
            End If
        End If
        If e.Control AndAlso e.KeyCode = Keys.S Then
            LuuLai()
        End If
    End Sub

    Private Sub gdvCT_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyUp
        If e.KeyCode = Keys.Tab Then
            If gdvCT.FocusedColumn Is colLoaiCT Then
                If gdvCT.FocusedRowHandle < gdvCT.RowCount Then
                    gdvCT.FocusedRowHandle = gdvCT.FocusedRowHandle + 1
                    gdvCT.FocusedColumn = colVCDVVC
                End If
            End If
        End If
    End Sub

    Public Sub LuuLai()
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        gdvCT.BeginUpdate()
        For i As Integer = 0 To gdvCT.RowCount - 1
            If gdvCT.GetRowCellValue(i, "Modify") Then
                AddParameter("@IDDVVC", gdvCT.GetRowCellValue(i, "IDDVVC"))
                AddParameter("@ThoiGian", gdvCT.GetRowCellValue(i, "ThoiGian"))
                AddParameter("@PhieuTC", gdvCT.GetRowCellValue(i, "SoPhieuXK"))
                AddParameter("@SoBill", gdvCT.GetRowCellValue(i, "SoBill"))
                AddParameter("@SoTien", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@SoTienTC", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@TienTe", gdvCT.GetRowCellValue(i, "TienTe"))
                AddParameter("@TyGia", gdvCT.GetRowCellValue(i, "TyGia"))
                AddParameter("@CanNang", gdvCT.GetRowCellValue(i, "CanNang"))
                AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", True)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvCT.GetRowCellValue(i, "IDVC")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        ShowBaoLoi("Không thêm được chi phí tại PX: " & gdvCT.GetRowCellValue(i, "SoPhieuXK") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "IDVC"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        ShowBaoLoi("Không cập nhật được chi phí tại PX: " & gdvCT.GetRowCellValue(i, "SoPhieuXK") & vbCrLf & LoiNgoaiLe)
                    End If
                End If

            End If
        Next

        gdvCT.EndUpdate()
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemPress
        LuuLai()
    End Sub
#End Region

    Public Function LayDataSourceDSCP() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("SoPhieuXK", Type.GetType("System.String"))
        tb.Columns.Add("CanNang", Type.GetType("System.Double"))
        tb.Columns.Add("GhiChu", Type.GetType("System.String"))
        tb.Columns.Add("TienTruocThue", Type.GetType("System.Double"))
        tb.Columns.Add("ChiPhi", Type.GetType("System.Double"))
        tb.Columns.Add("ID", Type.GetType("System.Object"))
        tb.Columns.Add("IDTakeCare", Type.GetType("System.Object"))
        Return tb
    End Function

    Private Sub mThemChiPhiMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemChiPhiMoi.ItemClick
        If ShowCauHoi("Thêm chi phí mới cho xuất kho " & gdvCT.GetFocusedRowCellValue("SoPhieuXK") & " ?") Then
            gdvCT.SetFocusedRowCellValue("IDDVVC", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("ThoiGian", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("SoBill", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("SoTien", 0)
            gdvCT.SetFocusedRowCellValue("TienTe", 0)
            gdvCT.SetFocusedRowCellValue("TyGia", 1)
            gdvCT.SetFocusedRowCellValue("CanNang", 0)
            gdvCT.SetFocusedRowCellValue("TienTC", 0)
            gdvCT.SetFocusedRowCellValue("GhiChu", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("IDVC", DBNull.Value)
        End If
    End Sub

    Private Sub btClose_Click(sender As System.Object, e As System.EventArgs) Handles btClose.Click
        gdvDSCP.DataSource = LayDataSourceDSCP()
        pVCGop.Visible = False
    End Sub

    Private Sub mDuaVaoDSChungCP_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuaVaoDSChungCP.ItemClick
        If pVCGop.Visible = False Then
            pVCGop.Visible = True
            tbSoBill.EditValue = DBNull.Value
            tbSoTien.EditValue = 0
            cbTienTe.EditValue = CType(0, Byte)
            tbThoiGian.EditValue = Now
        End If

        For i As Integer = 0 To gdvDSCPCT.RowCount - 1
            If gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK") = gdvCT.GetFocusedRowCellValue("SoPhieuXK") Then
                ShowCanhBao("Số phiếu XK đã có sẵn trong danh sách!")
                Exit Sub
            End If
        Next
        gdvDSCPCT.AddNewRow()
        gdvDSCPCT.SetFocusedRowCellValue("SoPhieuXK", gdvCT.GetFocusedRowCellValue("SoPhieuXK"))
        gdvDSCPCT.SetFocusedRowCellValue("TienTruocThue", gdvCT.GetFocusedRowCellValue("TienTruocThue"))
        gdvDSCPCT.SetFocusedRowCellValue("CanNang", 0)
        gdvDSCPCT.SetFocusedRowCellValue("ChiPhi", gdvCT.GetFocusedRowCellValue("ChiPhi"))
        gdvDSCPCT.SetFocusedRowCellValue("IDTakeCare", gdvCT.GetFocusedRowCellValue("IDTakeCare"))
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
    End Sub

    Private Sub cbTienTe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTienTe.EditValueChanged
        On Error Resume Next
        Dim r As DataRowView = cbTienTe.GetSelectedDataRow
        tbTyGia.EditValue = r("TyGia")
    End Sub

    Private Sub gdvCT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvCT.DoubleClick
        If colVCDVVC.Visible Then
            mDuaVaoDSChungCP.PerformClick()
        End If

    End Sub

    Private Sub cbDVVC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbDVVC.EditValueChanged
        tbSoBill.Focus()
    End Sub

    Private Sub gdvDSCPCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvDSCPCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng đang chọn ?") Then
                gdvDSCPCT.DeleteSelectedRows()
                gdvDSCPCT.CloseEditor()
                gdvDSCPCT.UpdateCurrentRow()
            End If
        End If
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If tbSoTien.EditValue Is Nothing Then
            ShowCanhBao("Chưa có thông tin tiền vận chuyển!")
            Exit Sub
        End If
        If gdvDSCPCT.RowCount = 0 Then
            ShowCanhBao("Chưa có thông tin phiếu xuất!")
            Exit Sub
        End If
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
        gdvDSCPCT.BeginUpdate()

        For i As Integer = 0 To gdvDSCPCT.RowCount - 1
            gdvDSCPCT.SetRowCellValue(i, "ChiPhi", Math.Round((gdvDSCPCT.GetRowCellValue(i, "TienTruocThue") / gdvDSCPCT.Columns("TienTruocThue").SummaryItem.SummaryValue * tbSoTien.EditValue), 0))
        Next
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
        gdvDSCPCT.EndUpdate()


    End Sub


    Private Sub btLuuVCGop_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVCGop.Click
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()

        If gdvDSCPCT.Columns("ChiPhi").SummaryItem.SummaryValue <> tbSoTien.EditValue Then
            ShowCanhBao("Chi phí chưa khớp!")
            Exit Sub
        End If
        gdvDSCPCT.BeginUpdate()
        BeginTransaction()
        Try
            For i As Integer = 0 To gdvDSCPCT.RowCount - 1
                AddParameter("@IDDVVC", cbDVVC.EditValue)
                AddParameter("@ThoiGian", tbThoiGian.EditValue)
                AddParameter("@PhieuTC", gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK"))
                AddParameter("@SoBill", tbSoBill.EditValue)
                AddParameter("@SoTien", gdvDSCPCT.GetRowCellValue(i, "ChiPhi"))
                AddParameter("@SoTienTC", tbSoTien.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@TyGia", tbTyGia.EditValue)
                AddParameter("@CanNang", gdvDSCPCT.GetRowCellValue(i, "CanNang"))
                AddParameter("@GhiChu", gdvDSCPCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", True)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvDSCPCT.GetRowCellValue(i, "ID")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvDSCPCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        Throw New Exception("Không thêm được chi phí tại PX: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvDSCPCT.GetRowCellValue(i, "ID"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        Throw New Exception("Không cập nhật được chi phí tại PX: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK") & vbCrLf & LoiNgoaiLe)
                    End If
                End If
            Next
            ComitTransaction()
        Catch ex As Exception
            RollBackTransaction()
        End Try

        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
        gdvDSCPCT.EndUpdate()
        ShowAlert("Đã thực hiện !")
        btXem.PerformClick()
    End Sub

    Private Sub mSuaChiPhiVCChung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaChiPhiVCChung.ItemClick
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("SoTien")) Then
            If gdvCT.GetFocusedRowCellValue("SoTienTC") <> gdvCT.GetFocusedRowCellValue("SoTien") Then
                AddParameterWhere("@IDVC", gdvCT.GetFocusedRowCellValue("IDDVVC"))
                AddParameterWhere("@STTC", gdvCT.GetFocusedRowCellValue("SoTienTC"))
                AddParameterWhere("@TG", gdvCT.GetFocusedRowCellValue("ThoiGian"))
                AddParameterWhere("@SB", gdvCT.GetFocusedRowCellValue("SoBill"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT PhieuTC as SoPhieuXK,CanNang,GhiChu,ChiPhi.ID,SoTien As ChiPhi,PHIEUXUATKHO.TienTruocThue*PHIEUXUATKHO.TyGIa as TienTruocThue FROM ChiPhi INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=ChiPhi.PhieuTC WHERE Loai=1 AND IDDVVC=@IDVC AND SoTienTC=@STTC AND SoBill=@SB AND ThoiGian=@TG")
                If tb Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvDSCP.DataSource = tb
                    If pVCGop.Visible = False Then
                        pVCGop.Visible = True
                    End If
                    cbDVVC.EditValue = gdvCT.GetFocusedRowCellValue("IDDVVC")
                    tbSoBill.EditValue = gdvCT.GetFocusedRowCellValue("SoBill")
                    tbThoiGian.EditValue = gdvCT.GetFocusedRowCellValue("ThoiGian")
                    tbSoTien.EditValue = gdvDSCPCT.Columns("ChiPhi").SummaryItem.SummaryValue
                    cbTienTe.EditValue = gdvCT.GetFocusedRowCellValue("TienTe")
                    tbTyGia.EditValue = gdvCT.GetFocusedRowCellValue("TyGia")
                End If
            End If
        End If

    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub btLuuLai_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick, mLuuLai.ItemClick
        LuuLai()
    End Sub

    Function CheckValue(ByVal _Value As Object) As Double
        If IsDBNull(_Value) Then
            Return 0
        Else
            Return _Value
        End If
    End Function

    Private Sub mChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChotSoLieu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Dim sql As String = ""
        Dim c As Integer = 0
        For i As Integer = 0 To gdvCT.RowCount - 1
            sql = " "
            sql &= " DECLARE  @tbTmp table"
            sql &= " ("
            sql &= " SoPhieu	nvarchar(9),"
            sql &= " IDNhanVien	int,"
            sql &= " PhuTrachHD	float,"
            sql &= " KhaoSatCG	float,"
            sql &= " XucTienHD	float,"
            sql &= " PhuTrachTC	float,"
            sql &= " ThiCong	float,"
            sql &= " PhatTrienSP	float"
            sql &= " )"



            If gdvCT.GetRowCellValue(i, "NhanKS").ToString <> "KT" Then
                sql &= " INSERT INTO @tbTmp(Sophieu,IDNhanVien,PhuTrachHD,KhaoSatCG)"
                sql &= " VALUES(@SP,@IDNV1,@PhuTrachHD,@KhaoSatCG)"
                AddParameterWhere("@PhuTrachHD", CheckValue(gdvCT.GetRowCellValue(i, "LoiNhuanKD")))
                AddParameterWhere("@KhaoSatCG", CheckValue(gdvCT.GetRowCellValue(i, "LNKSChaoGia")))
            Else
                sql &= " INSERT INTO @tbTmp(Sophieu,IDNhanVien,PhuTrachHD)"
                sql &= " VALUES(@SP,@IDNV1,@PhuTrachHD)"
                AddParameterWhere("@PhuTrachHD", CheckValue(gdvCT.GetRowCellValue(i, "LoiNhuanKD")))
            End If
            AddParameterWhere("@IDNV1", gdvCT.GetRowCellValue(i, "IDTakeCare"))

            If gdvCT.GetRowCellValue(i, "CongTrinh") = False Then
                sql &= " INSERT INTO @tbTmp(Sophieu,IDNhanVien,ThiCong)"
                sql &= " VALUES(@SP,@IDNV1,@ThiCong)"
                AddParameterWhere("@ThiCong", CheckValue(gdvCT.GetRowCellValue(i, "LoiNhuanKT")))
            End If

            sql &= " INSERT INTO @tbTmp(Sophieu,IDNhanVien,XucTienHD)"
            sql &= " VALUES(@SP,@IDNV2,@XucTienHD)"
            AddParameterWhere("@IDNV2", gdvCT.GetRowCellValue(i, "IDPhuTrachKyHD"))
            AddParameterWhere("@XucTienHD", gdvCT.GetRowCellValue(i, "LNXucTienHD"))

            sql &= " INSERT INTO @tbTmp(Sophieu,IDNhanVien,PhuTrachTC)"
            sql &= " VALUES(@SP,@IDNV3,@PhuTrachTC)"
            AddParameterWhere("@IDNV3", gdvCT.GetRowCellValue(i, "IDPhuTrachCT"))
            AddParameterWhere("@PhuTrachTC", gdvCT.GetRowCellValue(i, "LNPhuTrachCT"))

            sql &= " DELETE FROM PhieuXuatKho_LoiNhuan WHERE SoPhieu=@SP"

            sql &= " INSERT INTO PhieuXuatKho_LoiNhuan(Sophieu,IDNhanVien,PhuTrachHD,KhaoSatCG,XucTienHD,PhuTrachTC,ThiCong)"
            sql &= " SELECT [@tbTmp].SoPhieu,[@tbTmp].IDNhanVien,ISNULL(SUM([@tbTmp].PhuTrachHD),0),ISNULL(SUM([@tbTmp].KhaoSatCG),0),ISNULL(Sum([@tbTmp].XucTienHD),0),ISNULL(SUM([@tbTmp].PhuTrachTC),0),ISNULL(SUM([@tbTmp].ThiCong),0)"
            sql &= " FROM @tbTmp"
            sql &= " GROUP BY [@tbTmp].SoPhieu,[@tbTmp].IDNhanVien"

            sql &= " INSERT INTO PhieuXuatKho_LoiNhuan(Sophieu,IDNhanVien,PhatTrienSP)"
            sql &= " SELECT SoPhieu,IDNhanVien,Sum(SoLuong*LoiNhuan)TongLN"
            sql &= " FROM PhatTrienSanPham_LoiNhuan"
            sql &= " WHERE SoPhieu=@SP"
            sql &= " GROUP By SoPhieu,IDNhanVien"
            AddParameterWhere("@SP", gdvCT.GetRowCellValue(i, "SoPhieuXK"))
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                c += 1
            End If
        Next
        ShowAlert("Đã cập nhật lợi nhuận " & c.ToString & " phiếu xuất !")
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Alt Or Keys.O) Then
            If meSql.Visible = True Then
                meSql.Visible = False
                meTaoBangPhanBo.Visible = False
                '  panelPhanBo.Visible = False
            Else
                meSql.Visible = True
                meTaoBangPhanBo.Visible = True
                '  panelPhanBo.Visible = True

            End If
            Return True
        End If
        Return False
    End Function

    Private Sub BarButtonItem1_ItemClick_1(sender As Object, e As XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        Dim makh As String = gdvCT.GetFocusedRowCellValue("ttcMa")
        Dim filterString As String = "[ttcMa] = '" & makh & "'"
        gdvCT.ActiveFilter.Clear()
        '  gdvCT.ActiveFilter.NonColumnFilter = filterString
        gdvCT.Columns("ttcMa").FilterInfo = New ColumnFilterInfo(filterString)
        On Error Resume Next
    End Sub
End Class
