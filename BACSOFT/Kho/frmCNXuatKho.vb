Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmCNXuatKho
    Public PhieuXK As Object
    Public dtFromXuatKhoTam As DataTable = Nothing

    Private Sub frmCNXuatKho_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            chkFinish.Enabled = False
            lbChietKhau.Visible = False
            lbChietKhauCG.Visible = False
            tbChietKhau.Visible = False
            tbChietKhauCG.Visible = False
            lbTienGoc.Visible = False
            tbTienGoc.Visible = False
            colChietKhau.Visible = False
            colTCChietKhau.Visible = False
            colGiaDB.Visible = False
        Else
            colGiaDB.Visible = True
            colGiaNhap.OptionsColumn.ReadOnly = False
        End If
        loadKhachHang()
        loadDSDVT()
        loadNguoiGD()
        loadTakeCare()
        loadTienTe()
        If TrangThai.isAddNew Then
            Dim sql As String = ""
            sql &= " SELECT row_number() over (order by XUATKHO.ID) AS AZ,TENNUOC.Ten AS XuatXu, XUATKHO.ID,XUATKHO.SoPhieu,IDVatTu,VATTU.Model,VATTU.HangTon,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.ThongSo,TENDONVITINH.Ten AS DVT,SoLuong,DonGia,(SoLuong*DonGia)ThanhTien,XUATKHO.TienTe,XUATKHO.MucThue,XUATKHO.XuatThue,IDChaoGia,XUATKHO.ModifyID,XUATKHO.ModifyDate, "
            sql &= " Convert(float,0) AS slTon,Convert(float,0)SLTonThuc,0.0 AS GiaNhap, 0.0 AS ChietKhau,0 AS LoiGia,isGiaDacBiet"
            sql &= " FROM XUATKHO "
            sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            sql &= " INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu "
            sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu "
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
            sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonViTinh"
            sql &= " LEFT JOIN TENNUOC ON TENNUOC.ID=VATTU.IDTenNuoc"
            sql &= " WHERE XUATKHO.SoPhieu=-1"
            sql &= " SELECT row_number() over (order by ID) AS AZ, ID,XUATKHOAUX.SoPhieu,NoiDung,HangSX,DonVi,SoLuong,DonGia,(DonGia*SoLuong)ThanhTien,MucThue,XuatThue,TienTe FROM XUATKHOAUX WHERE SoPhieu=-1"

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                gdvVT.DataSource = ds.Tables(0)
                gdvCongTrinh.DataSource = ds.Tables(1)
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

            tabXK.SelectedTabPage = tabThamChieuCG
            cbNguoiXuat.EditValue = Convert.ToInt32(TaiKhoan)
            tbNgay.EditValue = GetServerTime()

            ' tbNguoiLap.EditValue = 
        Else
            Dim sql As String = " SET DATEFORMAT DMY"
            sql &= " SELECT NgayThang,PHIEUXUATKHO.SoPhieu,Kho,IDKhachHang,SoPhieuCG,LyDoXuat,IDUser,IDNgd,PHIEUXUATKHO.IDTakeCare,CongTrinh,TienTe,TyGia,TienTruocThue,TienThue,Finish,TienGoc,TienChietKhau,ThanhToan FROM PHIEUXUATKHO "
            sql &= " WHERE SoPhieu=@PhieuXK"

            sql &= " SELECT *,(CASE WHEN GiaNhap-DonGia >0  THEN 1 ELSE 0 END)LoiGia FROM "
            sql &= " (SELECT row_number() over (order by XUATKHO.ID) AS AZ,TENNUOC.Ten AS XuatXu, XUATKHO.ID,XUATKHO.SoPhieu,XUATKHO.IDVatTu,VATTU.Model,VATTU.HangTon,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.ThongSo,TENDONVITINH.Ten AS DVT,XUATKHO.SoLuong,XUATKHO.DonGia,(XUATKHO.SoLuong*XUATKHO.DonGia)ThanhTien,XUATKHO.TienTe,XUATKHO.MucThue,XUATKHO.XuatThue,IDChaoGia,XUATKHO.ModifyID,XUATKHO.ModifyDate, "
            sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu where IDVattu=XUATKHO.IDVattu AND Convert(datetime,Convert(nvarchar, PHIEUNHAPKHO.NgayThang,103),103) <=Convert(datetime,Convert(nvarchar, PHIEUNHAPKHO.NgayThang,103),103) )-(select isnull(SUM(Soluong),0) from XUATKHO AS tb INNER JOIN PHIEUXUATKHO as PXK ON PXK.SoPhieu=tb.SoPhieu  where tb.IDVattu=XUATKHO.IDVattu AND PXK.NgayThang <PHIEUXUATKHO.NgayThang)) AS slTon,"
            sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=XUATKHO.IDVattu )-(select isnull(SUM(Soluong),0) from XUATKHO AS tb  where tb.IDVattu=XUATKHO.IDVattu)) AS SLTonThuc,isnull(XUATKHO.isGiaDacBiet,Convert(bit,0))isGiaDacBiet,"
            sql &= " ISNULL(XUATKHO.GiaNhap,ISNULL(GIANHAPTB.DonGia,ISNULL(ISNULL("
            sql &= "        (SELECT     TOP (1) Gianhap"
            sql &= "            FROM V_GiaNhap "
            sql &= "            WHERE IDvattu = dbo.XUATKHO.IDvattu AND Ngaythang <= PHIEUXUATKHO.Ngaythang"
            sql &= "            ORDER BY Ngaythang DESC),"
            sql &= "        (SELECT     TOP (1) Gianhap"
            sql &= "            FROM V_GiaNhap"
            sql &= "            WHERE IDvattu = dbo.XUATKHO.IDvattu AND Ngaythang > PHIEUXUATKHO.Ngaythang"
            sql &= "                 ORDER BY Ngaythang)),0))) AS GiaNhap,ISNULL(CHAOGIA.ChietKhau,0)ChietKhau,ISNULL(GIANHAPTB.DonGia,0) as GiaNhapTB"
            'Tai
            sql &= " ,SoHoaDon, NgayHD"
            'Tai
            sql &= " FROM XUATKHO "
            sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            sql &= " INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu "
            sql &= " LEFT JOIN tblTonDauKy AS GIANHAPTB ON XUATKHO.IDvattu = GIANHAPTB.IDVatTu AND RIGHT(CONVERT(nvarchar, PHIEUXUATKHO.Ngaythang, 103), 7) = GIANHAPTB.ThoiGian "
            sql &= " LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
            sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu "
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
            sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonViTinh"
            sql &= " LEFT JOIN TENNUOC ON TENNUOC.ID=VATTU.IDTenNuoc"
            sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
            'Tai
            sql &= " LEFT JOIN CHUNGTU ON CHUNGTU.Id=Id_CT"
            'Tai
            sql &= " WHERE XUATKHO.SoPhieu=@PhieuXK)tb ORDER BY AZ"

            sql &= " SELECT row_number() over (order by ID) AS AZ, ID,XUATKHOAUX.SoPhieu,NoiDung,HangSX,DonVi,SoLuong,DonGia,(DonGia*SoLuong)ThanhTien,MucThue,XuatThue,TienTe,0 AS ChietKhau "
            sql &= " FROM XUATKHOAUX WHERE SoPhieu=@PhieuXK"

            'sql &= " SELECT BANGCHAOGIA.SoPhieu,BANGCHAOGIA.TienTruocThue,BANGCHAOGIA.TienThue,BANGCHAOGIA.TienChietKhau,BANGCHAOGIA.KhauTru,BANGCHAOGIA.TienTe,BANGCHAOGIA.TyGia,KHACHHANG.ttcMa,BANGCHAOGIA.TenDuAn,BANGCHAOGIA.CongTrinh AS ThanhTien FROM BANGCHAOGIA INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang "
            'sql &= " WHERE BANGCHAOGIA.SoPhieu=(SELECT DISTINCT SoPhieuCG FROM PHIEUXUATKHO WHERE PHIEUXUATKHO.SoPhieu=@PhieuXK)"
            AddParameterWhere("@PhieuXK", PhieuXK)
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                gdvVT.DataSource = ds.Tables(1)
                gdvCongTrinh.DataSource = ds.Tables(2)
                gdvMaKH.EditValue = ds.Tables(0).Rows(0)("IDKhachHang")


                chkFinish.Checked = ds.Tables(0).Rows(0)("Finish")
                tbKhoXuat.EditValue = ds.Tables(0).Rows(0)("Kho")
                tbSoPhieu.EditValue = ds.Tables(0).Rows(0)("SoPhieu")
                tbNgay.EditValue = ds.Tables(0).Rows(0)("NgayThang")

                tbTienTruocThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue")
                tbTienThue.EditValue = ds.Tables(0).Rows(0)("TienThue")
                tbChietKhau.EditValue = ds.Tables(0).Rows(0)("TienChietKhau")
                tbTienGoc.EditValue = ds.Tables(0).Rows(0)("TienGoc")
                LoadPhieuCG(gdvMaKH.EditValue)
                gdvPhieuCG.EditValue = ds.Tables(0).Rows(0)("SoPhieuCG")

                cbTienTe.EditValue = ds.Tables(0).Rows(0)("TienTe")
                tbTyGia.EditValue = ds.Tables(0).Rows(0)("TyGia")
                cbNguoiXuat.EditValue = ds.Tables(0).Rows(0)("IDUser")
                chkCongTrinh.Checked = ds.Tables(0).Rows(0)("CongTrinh")

                If chkCongTrinh.Checked Then
                    spliter.Collapsed = False
                Else
                    spliter.Collapsed = True
                End If

                LoadThamChieuCG(ds.Tables(0).Rows(0)("SoPhieuCG"))
                cbTakeCare.EditValue = ds.Tables(0).Rows(0)("IDTakeCare")
                cbNguoiGD.EditValue = ds.Tables(0).Rows(0)("IDNgd")

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If


            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                colSoLuong.OptionsColumn.ReadOnly = True
                colSLAUX.OptionsColumn.ReadOnly = True
                mXuatKho.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btChuyenXK.Visible = False
                gdvMaKH.Enabled = False
                gdvPhieuCG.Enabled = False
                btCapNhatGia.Visible = False
            Else
                colDonGiaVT.OptionsColumn.ReadOnly = False
                colDonGiaChiPhi.OptionsColumn.ReadOnly = False
            End If

        End If


        If Not dtFromXuatKhoTam Is Nothing Then

            For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
                Dim idVT As Object = gdvThamChieuCT.GetRowCellValue(i, "IDVatTu")
                If idVT Is DBNull.Value Then Continue For
                If dtFromXuatKhoTam.Select("IDVatTu = " & idVT).Length > 0 Then
                    gdvThamChieuCT.SetRowCellValue(i, "Chon", True)
                End If
            Next

            btChuyenXK_Click(sender, e)

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                Dim idVT As Object = gdvVTCT.GetRowCellValue(i, "IDVatTu")
                If idVT Is DBNull.Value Then Continue For
                Dim arrSL = dtFromXuatKhoTam.Select("IDVatTu = " & idVT)
                If arrSL.Length > 0 Then
                    gdvVTCT.SetRowCellValue(i, "SoLuong", arrSL(0)("SLXuatKho"))
                End If
            Next

        End If

    End Sub

    Public Sub LoadPhieuCG(ByVal IDKhachHang As Object)
        AddParameterWhere("@IDKH", IDKhachHang)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,CongTrinh,TenDuAn,TienTruocThue,TienThue,TienChietKhau,KhauTru,BANGCHAOGIA.IDTakeCare,BANGCHAOGIA.IDNgd, TAKECARE.Ten AS TakeCare,TienTe,TyGia FROM BANGCHAOGIA INNER JOIN NHANSU TAKECARE ON BANGCHAOGIA.IDTakeCare=TAKECARE.ID WHERE IDKhachHang=@IDKH AND BANGCHAOGIA.TrangThai=2 ORDER BY SoPhieu DESC")
        If Not tb Is Nothing Then
            gdvPhieuCG.Properties.DataSource = tb
        End If
    End Sub

    Public Sub LoadThamChieuCG(ByVal SoPhieuCG As Object)
        Dim sql As String = ""
        Dim tg As DateTime = GetServerTime()

        AddParameterWhere("@SP", SoPhieuCG)
        sql &= "  SELECT CHAOGIA.ID AS IDCG,Convert(bit,0)Chon, CHAOGIA.IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,CanXuat AS SoLuong,CHAOGIA.SoLuong AS SLChao,CHAOGIA.DonGia,CHAOGIA.ChietKhau,CHAOGIA.MucThue,CHAOGIA.XuatThue,"
        sql &= " (Round((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu ),4)-Round((select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu ),4)) AS slTon,"
        sql &= " VATTU.HangTon, ISNULL(CHAOGIA.AZ,0)AZ,"
        sql &= " ISNULL(GIANHAPTB.DonGia,ISNULL(ISNULL("
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia)) AS GiaNhap,VATTU.IDDonViTinh,TENNUOC.Ten AS XuatXu"
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=CHAOGIA.SoPhieu"
        Dim _thoiGian As String = ""
        If TrangThai.isAddNew Then
            _thoiGian = tg.ToString("MM/yyyy")
        Else
            _thoiGian = Convert.ToDateTime(tbNgay.EditValue).ToString("MM/yyyy")
        End If
        sql &= " LEFT JOIN tblTonDauKy AS GIANHAPTB ON CHAOGIA.IDvattu = GIANHAPTB.IDVatTu AND GIANHAPTB.ThoiGian = '" & _thoiGian & "'"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENNUOC ON VATTU.IDTenNuoc=TENNUOC.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " WHERE CHAOGIA.Sophieu=@SP"

        sql &= " UNION ALL"

        sql &= " SELECT  CHAOGIAAUX.ID AS IDCG, Convert(bit,0)Chon, NULL AS IDVatTu,NoiDung AS TenVT,HangSX AS TenHang,"
        sql &= " NULL AS Model,NULL AS ThongSo,TENDONVITINH.Ten AS TenDVT,SoLuong,SoLuong AS SLChao,DonGia,CHAOGIAAUX.ChietKhau,MucThue,XuatThue,"
        sql &= " NULL AS slTon, NULL AS HangTon,9999 AS AZ,NULL as GiaNhap,DonVi AS IDDonViTinh,NULL AS XuatXu"
        sql &= " FROM CHAOGIAAUX"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON CHAOGIAAUX.DonVi=TENDONVITINH.ID"
        sql &= " WHERE CHAOGIAAUX.Sophieu=@SP"

        sql &= " ORDER BY AZ"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("AZ") = i + 1
            Next
            gdvThamChieu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub

    Public Sub loadXuatKho(ByVal _PhieuXK As Object)
        Dim sql As String = " SET DATEFORMAT DMY"
        sql &= " SELECT *,(CASE WHEN GiaNhap-DonGia >0  THEN 1 ELSE 0 END)LoiGia FROM (SELECT row_number() over (order by XUATKHO.ID) AS AZ,TENNUOC.Ten AS XuatXu, XUATKHO.ID,XUATKHO.SoPhieu,XUATKHO.IDVatTu,VATTU.Model,VATTU.HangTon,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.ThongSo,TENDONVITINH.Ten AS DVT,XUATKHO.SoLuong,XUATKHO.DonGia,(XUATKHO.SoLuong*XUATKHO.DonGia)ThanhTien,XUATKHO.TienTe,XUATKHO.MucThue,XUATKHO.XuatThue,IDChaoGia,XUATKHO.ModifyID,XUATKHO.ModifyDate, "
        ' sql &= " (Round((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=XUATKHO.IDVattu),4)-Round((select isnull(SUM(Soluong),0) from XUATKHO AS tb where tb.IDVattu=XUATKHO.IDVattu),4)) AS slTon,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu where IDVattu=XUATKHO.IDVattu AND Convert(datetime,Convert(nvarchar, PHIEUNHAPKHO.NgayThang,103),103) <=Convert(datetime,Convert(nvarchar, PHIEUNHAPKHO.NgayThang,103),103) )-(select isnull(SUM(Soluong),0) from XUATKHO AS tb INNER JOIN PHIEUXUATKHO as PXK ON PXK.SoPhieu=tb.SoPhieu  where tb.IDVattu=XUATKHO.IDVattu AND PXK.NgayThang <PHIEUXUATKHO.NgayThang)) AS slTon,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=XUATKHO.IDVattu )-(select isnull(SUM(Soluong),0) from XUATKHO AS tb  where tb.IDVattu=XUATKHO.IDVattu)) AS SLTonThuc,isnull(XUATKHO.isGiaDacBiet,Convert(bit,0))isGiaDacBiet,"
        sql &= " ISNULL(XUATKHO.GiaNhap,ISNULL(GIANHAPTB.DonGia,ISNULL(ISNULL("
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = dbo.XUATKHO.IDvattu AND Ngaythang <= PHIEUXUATKHO.Ngaythang"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = dbo.XUATKHO.IDvattu AND Ngaythang > PHIEUXUATKHO.Ngaythang"
        sql &= "                 ORDER BY Ngaythang)),0))) AS GiaNhap,ISNULL(CHAOGIA.ChietKhau,0)ChietKhau"

        sql &= " FROM XUATKHO "
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
        sql &= " INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu "
        sql &= " LEFT JOIN tblTonDauKy AS GIANHAPTB ON XUATKHO.IDvattu = GIANHAPTB.IDVatTu AND RIGHT(CONVERT(nvarchar, PHIEUXUATKHO.Ngaythang, 103), 7) = GIANHAPTB.ThoiGian "
        sql &= " LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu "
        sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonViTinh"
        sql &= " LEFT JOIN TENNUOC ON TENNUOC.ID=VATTU.IDTenNuoc"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"

        sql &= " WHERE XUATKHO.SoPhieu=@PhieuXK)tb ORDER BY AZ"

        sql &= " SELECT row_number() over (order by ID) AS AZ, ID,XUATKHOAUX.SoPhieu,NoiDung,HangSX,DonVi,SoLuong,DonGia,(DonGia*SoLuong)ThanhTien,MucThue,XuatThue,TienTe,0 AS ChietKhau "
        sql &= " FROM XUATKHOAUX WHERE SoPhieu=@PhieuXK"

        AddParameterWhere("@PhieuXK", _PhieuXK)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvVT.DataSource = ds.Tables(0)
            gdvCongTrinh.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            rcbTienTe.DataSource = tb
            rcbTienTeCT.DataSource = tb
            cbTienTe.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                '     _exit = True
                cbTienTe.EditValue = Convert.ToByte(TienTe.VND)
                '      _exit = False
                tbTyGia.EditValue = 1
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub loadDSDVT()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM TENDONVITINH")
        If Not tb Is Nothing Then
            cbDVTAUX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    'Public Sub loadTKNganHang()
    '    Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM TAIKHOAN")
    '    If Not tb Is Nothing Then
    '        cbTKNganHang.Properties.DataSource = tb
    '        If tb.Rows.Count > 0 Then
    '            cbTKNganHang.EditValue = tb.Rows(0)(0)
    '        End If
    '    Else
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub

    Public Sub loadKhachHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,IDHinhThucTT,IDTakecare FROM KHACHHANG")
        If Not tb Is Nothing Then
            gdvMaKH.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadNguoiGD()
        AddParameterWhere("@IDCTY", gdvMaKH.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,ChamSoc FROM NHANSU WHERE Noictac=@IDCTY")
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
            cbNguoiXuat.Properties.DataSource = tb
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

    Private Sub gdvPhieuCG_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvPhieuCG.KeyPress
        If gdvPhieuCG.IsPopupOpen Then
            Exit Sub
        Else
            gdvPhieuCG.ShowPopup()
        End If
    End Sub

    Private Sub gdvMaKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        On Error Resume Next
        LoadPhieuCG(gdvMaKH.EditValue)
        loadNguoiGD()
        loadTakeCare()
    End Sub

    'Private Sub cbNguoiGD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNguoiGD.EditValueChanged
    '    On Error Resume Next
    '    'If _Exit Then Exit Sub
    '    Dim edit As LookUpEdit = CType(sender, LookUpEdit)
    '    Dim dr As DataRowView = edit.GetSelectedDataRow
    '    cbTakeCare.EditValue = dr("Chamsoc")
    'End Sub

    Private Sub btIn_Click(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If tbSoPhieu.EditValue Is Nothing Then Exit Sub
        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim Sql As String = ""
            Sql &= " SELECT (N'Khách hàng: ' + ISNULL(KHACHHANG.Ten,'')) AS TenKH,(N'Ngày: ' + Convert(nvarchar,NgayThang,103))AS Ngay,"
            Sql &= " (N'Đại diện: ' + ISNULL(NGUOIGD.Ten,'')) AS DaiDien,(N'Xuất tại: ' + ISNULL(Kho,'')) AS Kho,"
            Sql &= " (N'Chức danh: '+ISNULL(NGUOIGD.ChucVu,'') + '  ĐT: ' + ISNULL(NGUOIGD.Mobile,'')) AS ChucDanh,"
            Sql &= " (N'Người xuất: ' + ISNULL(NGUOILAP.Ten,'')) AS NguoiXuat, (N'Chào giá: CG ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieuCG ) AS ChaoGia,"
            Sql &= " (N'Phiếu xuất: ' + 'XK ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieu) AS PhieuXuat,TAKECARE.Ten AS TakeCare,"
            Sql &= " (N'Lý do xuất kho: '+ ISNULL(LyDoXuat,'')) AS LyDoXuat"
            Sql &= " FROM PHIEUXUATKHO"
            Sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUXUATKHO.IDUser=NGUOILAP.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOIGD ON PHIEUXUATKHO.IDNgd=NGUOIGD.ID"
            Sql &= " LEFT JOIN NHANSU AS TAKECARE ON PHIEUXUATKHO.IDTakeCare=TAKECARE.ID"
            Sql &= " WHERE SoPhieu=@SP"

            Sql &= " SELECT (0) AS STT,"
            If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked = False Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " TENVATTU.Ten AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            Else
                If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Else
                    Sql &= " TENVATTU.Ten AS TenVT,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENHANGSANXUAT.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenHang,"
                Else
                    Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
                Else
                    Sql &= " VATTU.Model, "
                End If
            End If

            Sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong"
            Sql &= " FROM XUATKHO "
            Sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
            Sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            Sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            Sql &= " WHERE SoPhieu=@SP"
            Sql &= " UNION ALL "
            Sql &= " SELECT (0)AS STT, NoiDung AS TenVT,HangSX AS TenHang,('')MaVT,TENDONVITINH.Ten AS TenDVT, SoLuong"
            Sql &= " FROM XUATKHOAUX"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON XUATKHOAUX.Donvi=TENDONVITINH.ID"
            Sql &= " WHERE SoPhieu=@SP"

            AddParameterWhere("@SP", tbSoPhieu.EditValue)
            Dim ds As DataSet = ExecuteSQLDataSet(Sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next
            End If

            Dim f As New frmIn("Phiếu xuất kho")
            Dim rpt As New rptPhieuXuatKho
            rpt.pLogo.Image = My.Resources.Logo2
            rpt.DataSource = ds
            If chkBienBan.Checked Then
                rpt.lbTieuDe.Text = "BIÊN BẢN BÀN GIAO"
            End If

            If chkModel.Checked Then
                If chkTenVT.Checked = True And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellTenVT.WidthF = 300
                    rpt.cellTenVTCT.WidthF = 300
                    rpt.cellMaVT.WidthF = 250
                    rpt.cellMaVTCT.WidthF = 250
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = True Then

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellHangSX.WidthF = 200
                    rpt.CellHangSXCT.WidthF = 200
                    rpt.cellMaVT.WidthF = 350
                    rpt.cellMaVTCT.WidthF = 350
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellMaVT.WidthF = 550
                    rpt.cellMaVTCT.WidthF = 550
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                End If
            Else
                rpt.tbHeader.DeleteColumn(rpt.cellMaVT)
                rpt.tbDetail.DeleteColumn(rpt.cellMaVTCT)
                If chkTenVT.Checked = True And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)
                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellTenVT.WidthF = 560
                    rpt.cellTenVTCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = True Then

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellHangSX.WidthF = 560
                    rpt.CellHangSXCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                End If
            End If

            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            pIn.Visible = False
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btInPhieu_Click(sender As System.Object, e As System.EventArgs) Handles btInPhieu.Click
        pIn.Visible = True
    End Sub

    Private Sub gdvPhieuCG_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhieuCG.EditValueChanged
        On Error Resume Next
        LoadThamChieuCG(gdvPhieuCG.EditValue)
        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbTakeCare.EditValue = dr("IDTakeCare")
        cbNguoiGD.EditValue = dr("IDNgd")
        chkCongTrinh.Checked = dr("CongTrinh")
        cbTienTe.EditValue = dr("TienTe")
        tbTyGia.EditValue = dr("TyGia")
        tbTruocThueCG.EditValue = dr("TienTruocThue")
        tbTienThueCG.EditValue = dr("TienThue")
        tbChietKhauCG.EditValue = dr("TienChietKhau")
        tbKhauTru.EditValue = dr("KhauTru")
        'lbThongTinCG.Text = "*Thông tin chào giá: " & String.Format("Trước thuế: {0:N2}, tiền thuế: {1:N2}, chiết khấu: {2:N2}", dr("TienTruocThue"), dr("TienThue"), dr("TienChietKhau"))
    End Sub

    Private Sub chkChonHet_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChonHet.CheckedChanged
        gdvThamChieuCT.BeginUpdate()
        For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
            gdvThamChieuCT.SetRowCellValue(i, "Chon", chkChonHet.Checked)
        Next
        gdvThamChieuCT.EndUpdate()
    End Sub

    Private Sub gdvThamChieuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvThamChieuCT.RowCellClick
        If e.Column.FieldName = "Chon" Then
            gdvThamChieuCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
        End If
    End Sub


    Private Sub XtraTabControl1_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabXK.SelectedPageChanged
        If e.Page Is tabThamChieuCG Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And TrangThai.isUpdate = True Then
                mXuatKho.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btChuyenXK.Visible = False
            Else
                mXuatKho.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btChuyenXK.Visible = True
            End If

        Else
            btChuyenXK.Visible = False
        End If
    End Sub

    Public Sub btChuyenXK_Click(sender As System.Object, e As System.EventArgs) Handles btChuyenXK.Click, mXuatKho.ItemClick

        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
            If gdvThamChieuCT.GetRowCellValue(i, "Chon") Then
                If gdvThamChieuCT.GetRowCellValue(i, "SoLuong") > 0 Then
                    If Not IsDBNull(gdvThamChieuCT.GetRowCellValue(i, "IDVatTu")) Then
                        gdvVTCT.AddNewRow()
                        gdvVTCT.SetFocusedRowCellValue("IDVatTu", gdvThamChieuCT.GetRowCellValue(i, "IDVatTu"))
                        gdvVTCT.SetFocusedRowCellValue("TenVT", gdvThamChieuCT.GetRowCellValue(i, "TenVT"))
                        gdvVTCT.SetFocusedRowCellValue("HangSX", gdvThamChieuCT.GetRowCellValue(i, "TenHang"))
                        gdvVTCT.SetFocusedRowCellValue("Model", gdvThamChieuCT.GetRowCellValue(i, "Model"))
                        gdvVTCT.SetFocusedRowCellValue("ThongSo", gdvThamChieuCT.GetRowCellValue(i, "ThongSo"))
                        gdvVTCT.SetFocusedRowCellValue("DVT", gdvThamChieuCT.GetRowCellValue(i, "TenDVT"))
                        gdvVTCT.SetFocusedRowCellValue("SoLuong", gdvThamChieuCT.GetRowCellValue(i, "SoLuong"))
                        gdvVTCT.SetFocusedRowCellValue("DonGia", gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                        gdvVTCT.SetFocusedRowCellValue("ThanhTien", gdvThamChieuCT.GetRowCellValue(i, "SoLuong") * gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                        gdvVTCT.SetFocusedRowCellValue("MucThue", gdvThamChieuCT.GetRowCellValue(i, "MucThue"))
                        gdvVTCT.SetFocusedRowCellValue("XuatThue", False)
                        gdvVTCT.SetFocusedRowCellValue("GiaNhap", gdvThamChieuCT.GetRowCellValue(i, "GiaNhap"))
                        gdvVTCT.SetFocusedRowCellValue("slTon", gdvThamChieuCT.GetRowCellValue(i, "slTon"))
                        gdvVTCT.SetFocusedRowCellValue("ChietKhau", gdvThamChieuCT.GetRowCellValue(i, "ChietKhau"))
                        gdvVTCT.SetFocusedRowCellValue("XuatXu", gdvThamChieuCT.GetRowCellValue(i, "XuatXu"))
                        gdvVTCT.SetFocusedRowCellValue("IDChaoGia", gdvThamChieuCT.GetRowCellValue(i, "IDCG"))
                        gdvVTCT.SetFocusedRowCellValue("LoiGia", 0)
                        gdvVTCT.SetFocusedRowCellValue("isGiaDacBiet", False)
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        'gdvVTCT.SetFocusedRowCellValue("", gdvThamChieuCT.GetRowCellValue(i, ""))
                        gdvVTCT.CloseEditor()
                        gdvVTCT.UpdateCurrentRow()
                    Else
                        gdvCongTrinhCT.AddNewRow()

                        gdvCongTrinhCT.SetFocusedRowCellValue("NoiDung", gdvThamChieuCT.GetRowCellValue(i, "TenVT"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("HangSX", gdvThamChieuCT.GetRowCellValue(i, "TenHang"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("Model", gdvThamChieuCT.GetRowCellValue(i, "Model"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("ThongSo", gdvThamChieuCT.GetRowCellValue(i, "ThongSo"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("DonVi", gdvThamChieuCT.GetRowCellValue(i, "IDDonViTinh"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("SoLuong", gdvThamChieuCT.GetRowCellValue(i, "SoLuong"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("DonGia", gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("ThanhTien", gdvThamChieuCT.GetRowCellValue(i, "SoLuong") * gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("MucThue", gdvThamChieuCT.GetRowCellValue(i, "MucThue"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("XuatThue", gdvThamChieuCT.GetRowCellValue(i, "XuatThue"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("GiaNhap", gdvThamChieuCT.GetRowCellValue(i, "GiaNhap"))
                        gdvCongTrinhCT.SetFocusedRowCellValue("slTon", gdvThamChieuCT.GetRowCellValue(i, "slTon"))
                        gdvCongTrinhCT.CloseEditor()
                        gdvCongTrinhCT.UpdateCurrentRow()
                    End If


                End If
            End If
        Next
        gdvVTCT.EndUpdate()
        tabXK.SelectedTabPage = tabXuatKho

    End Sub

    Private Sub gdvVTCT_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvVTCT.InitNewRow
        gdvVTCT.SetRowCellValue(e.RowHandle, "AZ", gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 2, "AZ") + 1)
    End Sub

    Private Sub gdvCongTrinhCT_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCongTrinhCT.InitNewRow
        gdvCongTrinhCT.SetRowCellValue(e.RowHandle, "AZ", gdvCongTrinhCT.GetRowCellValue(gdvCongTrinhCT.RowCount - 2, "AZ") + 1)
    End Sub

    Public Function KiemTraTrung(ByVal MaVT As Object) As Boolean
        Dim count As Integer = 0
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "Model") = MaVT Then
                count += 1
                If count > 1 Then
                    gdvVTCT.SetRowCellValue(i, "LoiGia", 1)
                    For j As Integer = 0 To i - 1
                        If gdvVTCT.GetRowCellValue(j, "Model") = MaVT Then
                            gdvVTCT.SetRowCellValue(j, "LoiGia", 1)
                        End If
                    Next
                End If
            End If
        Next
        If count > 1 Then

            Return True
        Else
            Return False
        End If
    End Function

    'Private _TienGoc As Double = 0

    Private Sub btCal_Click(sender As System.Object, e As System.EventArgs) Handles btCal.Click
        If gdvPhieuCG.EditValue Is Nothing Then
            ShowCanhBao("Chưa có thông tin chào giá !")
            Exit Sub
        End If
        ' tbTienGoc = 0
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        gdvCongTrinhCT.CloseEditor()
        gdvCongTrinhCT.UpdateCurrentRow()

        If gdvVTCT.RowCount = 0 And gdvCongTrinhCT.RowCount = 0 Then
            ShowCanhBao("Chưa có hàng hóa và chi phí xuất kho !")
            Exit Sub
        End If
        Dim _MaTrung As String = "Mã trùng: "
        ' Kiểm tra trùng mã khi xuất kho
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If KiemTraTrung(gdvVTCT.GetRowCellValue(i, "Model")) Then
                _MaTrung &= vbCrLf & " - " & gdvVTCT.GetRowCellValue(i, "Model")
            End If
        Next

        If _MaTrung <> "Mã trùng: " Then
            If Not ShowCauHoi(_MaTrung & vbCrLf & " bạn có muốn tiếp tục không ?") Then
                Exit Sub
            End If
        End If

        tbTienTruocThue.EditValue = 0
        tbTienThue.EditValue = 0
        tbChietKhau.EditValue = 0
        tbTienGoc.EditValue = 0

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            tbTienTruocThue.EditValue += gdvVTCT.GetRowCellValue(i, "ThanhTien")
            If gdvVTCT.GetRowCellValue(i, "XuatThue") Then
                tbTienThue.EditValue += Math.Round((gdvVTCT.GetRowCellValue(i, "ThanhTien") * gdvVTCT.GetRowCellValue(i, "MucThue")) / 100, 2)
            End If
            tbChietKhau.EditValue += Math.Round(gdvVTCT.GetRowCellValue(i, "ChietKhau") * gdvVTCT.GetRowCellValue(i, "SoLuong"), 2)
            If gdvVTCT.GetRowCellValue(i, "DonGia") < gdvVTCT.GetRowCellValue(i, "GiaNhap") Then
                gdvVTCT.SetRowCellValue(i, "LoiGia", 1)
            Else
                gdvVTCT.SetRowCellValue(i, "LoiGia", 0)
            End If
            tbTienGoc.EditValue += Math.Round(gdvVTCT.GetRowCellValue(i, "GiaNhap") * gdvVTCT.GetRowCellValue(i, "SoLuong"), 2)
        Next

        For i As Integer = 0 To gdvCongTrinhCT.RowCount - 1
            tbTienTruocThue.EditValue += gdvCongTrinhCT.GetRowCellValue(i, "ThanhTien")
            If gdvCongTrinhCT.GetRowCellValue(i, "XuatThue") Then
                tbTienThue.EditValue += Math.Round((gdvCongTrinhCT.GetRowCellValue(i, "ThanhTien") * gdvCongTrinhCT.GetRowCellValue(i, "MucThue")) / 100, 2)
            End If
        Next

        If chkCongTrinh.Checked Then
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT (TienChietKhau/((100-KhauTru)/100)) AS TienChietKhau FROM BANGCHAOGIA WHERE SoPhieu='" & gdvPhieuCG.EditValue & "'")
            If Not tb Is Nothing Then
                If tb.Rows.Count = 0 Then
                    ShowCanhBao("Không lấy được thông tin tiền chiết khấu !")
                Else
                    tbChietKhau.EditValue = tb.Rows(0)(0)
                End If

            Else
                ShowCanhBao("Không lấy được thông tin tiền chiết khấu !")
            End If

        End If

        tbChietKhau.EditValue = Math.Round(tbChietKhau.EditValue - tbChietKhau.EditValue * (tbKhauTru.EditValue / 100), 2)


    End Sub

    Private Sub gdvVTCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvVTCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "SoLuong", "DonGia"
                gdvVTCT.SetRowCellValue(e.RowHandle, "ThanhTien", gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvVTCT.GetRowCellValue(e.RowHandle, "DonGia"))
        End Select
    End Sub

    Private Sub gdvCongTrinhCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCongTrinhCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "SoLuong", "DonGia"
                gdvCongTrinhCT.SetRowCellValue(e.RowHandle, "ThanhTien", gdvCongTrinhCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvCongTrinhCT.GetRowCellValue(e.RowHandle, "DonGia"))
        End Select
    End Sub

    Public Function CheckTonAm() As Boolean
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "SoLuong") > gdvVTCT.GetRowCellValue(i, "slTon") Then
                ShowCanhBao("Số lượng vật tư " & gdvVTCT.GetRowCellValue(i, "Model") & " không đủ để xuất hàng !")
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click
        btCal.PerformClick()
        If CheckTonAm() Then Exit Sub

        If tbTienTruocThue.EditValue <> tbTruocThueCG.EditValue Or tbTienThue.EditValue <> tbTienThueCG.EditValue Or tbChietKhau.EditValue <> tbChietKhauCG.EditValue Then
            If Not ShowCauHoi("Số tiền xuất kho chưa khớp chào giá, bạn có muốn tiếp tục lưu lại ?") Then Exit Sub
        End If


        Try
            ' BeginTransaction()

            If TrangThai.isAddNew Then
                tbSoPhieu.EditValue = LaySoPhieu("PHIEUXUATKHO")
                tbNgay.EditValue = GetServerTime()
            End If
            Dim modifyDate As DateTime = GetServerTime()

            AddParameter("@Ngaythang", tbNgay.EditValue)

            AddParameter("@Kho", tbKhoXuat.EditValue)
            AddParameter("@IDkhachhang", gdvMaKH.EditValue)
            AddParameter("@SophieuCG", gdvPhieuCG.EditValue)
            AddParameter("@IDUser", cbNguoiXuat.EditValue)
            AddParameter("@IDNgd", cbNguoiGD.EditValue)
            AddParameter("@IDTakecare", cbTakeCare.EditValue)
            AddParameter("@Congtrinh", chkCongTrinh.Checked)
            AddParameter("@Tiente", cbTienTe.EditValue)
            AddParameter("@Tygia", tbTyGia.EditValue)
            AddParameter("@Tientruocthue", tbTienTruocThue.EditValue)
            AddParameter("@Tienthue", tbTienThue.EditValue)
            AddParameter("@Finish", chkFinish.Checked)
            AddParameter("@tiengoc", tbTienGoc.EditValue)
            AddParameter("@tienchietkhau", tbChietKhau.EditValue)
            AddParameter("@ModifyID", TaiKhoan)
            AddParameter("@ModifyDate", modifyDate)
            If colLoiGia.SummaryItem.SummaryValue > 0 Then
                AddParameter("@error", True)
            Else
                AddParameter("@error", False)
            End If

            If TrangThai.isAddNew Then
                AddParameter("@Sophieu", tbSoPhieu.EditValue)
                If doInsert("PHIEUXUATKHO") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@SoPhieu", tbSoPhieu.EditValue)
                If doUpdate("PHIEUXUATKHO", "Sophieu=@SoPhieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                AddParameter("@Sophieu", tbSoPhieu.EditValue)
                AddParameter("@IDvattu", gdvVTCT.GetRowCellValue(i, "IDVatTu"))
                AddParameter("@Soluong", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                AddParameter("@Dongia", gdvVTCT.GetRowCellValue(i, "DonGia"))
                AddParameter("@GiaNhap", gdvVTCT.GetRowCellValue(i, "GiaNhap"))
                AddParameter("@Tiente", cbTienTe.EditValue)
                AddParameter("@Mucthue", gdvVTCT.GetRowCellValue(i, "MucThue"))
                AddParameter("@Xuatthue", gdvVTCT.GetRowCellValue(i, "XuatThue"))
                AddParameter("@IDChaogia", gdvVTCT.GetRowCellValue(i, "IDChaoGia"))
                AddParameter("@ModifyID", TaiKhoan)
                AddParameter("@ModifyDate", modifyDate)
                AddParameter("@AZ", gdvVTCT.GetRowCellValue(i, "AZ"))
                AddParameter("@isGiaDacBiet", gdvVTCT.GetRowCellValue(i, "isGiaDacBiet"))
                If IsDBNull(gdvVTCT.GetRowCellValue(i, "ID")) Or gdvVTCT.GetRowCellValue(i, "ID") Is Nothing Then
                    Dim IDXK As Integer = doInsert("XUATKHO")
                    If IDXK = Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvVTCT.SetRowCellValue(i, "ID", IDXK)
                    gdvVTCT.CloseEditor()
                    gdvVTCT.UpdateCurrentRow()
                Else
                    AddParameterWhere("@ID", gdvVTCT.GetRowCellValue(i, "ID"))
                    If doUpdate("XUATKHO", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next
            ' TAI.TinhNgayCongNo(gdvPhieuCG.EditValue, tbSoPhieu.EditValue, 1, tbNgay.EditValue, gdvVTCT.Columns("ThanhTien").SummaryItem.SummaryValue)
            For i As Integer = 0 To gdvCongTrinhCT.RowCount - 1
                AddParameter("@Sophieu", tbSoPhieu.EditValue)
                AddParameter("@Noidung", gdvCongTrinhCT.GetRowCellValue(i, "NoiDung"))
                AddParameter("@HangSx", gdvCongTrinhCT.GetRowCellValue(i, "HangSX"))
                AddParameter("@Donvi", gdvCongTrinhCT.GetRowCellValue(i, "DonVi"))
                AddParameter("@Soluong", gdvCongTrinhCT.GetRowCellValue(i, "SoLuong"))
                AddParameter("@Dongia", gdvCongTrinhCT.GetRowCellValue(i, "DonGia"))
                AddParameter("@Tiente", cbTienTe.EditValue)
                AddParameter("@Mucthue", gdvCongTrinhCT.GetRowCellValue(i, "MucThue"))
                AddParameter("@Xuatthue", gdvCongTrinhCT.GetRowCellValue(i, "XuatThue"))
                If IsDBNull(gdvCongTrinhCT.GetRowCellValue(i, "ID")) Or gdvCongTrinhCT.GetRowCellValue(i, "ID") Is Nothing Then
                    Dim IDXKAUX As Integer = doInsert("XUATKHOAUX")
                    If IDXKAUX = Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvCongTrinhCT.SetRowCellValue(i, "ID", IDXKAUX)
                    gdvCongTrinhCT.CloseEditor()
                    gdvCongTrinhCT.UpdateCurrentRow()
                Else
                    AddParameterWhere("@ID", gdvCongTrinhCT.GetRowCellValue(i, "ID"))
                    If doUpdate("XUATKHOAUX", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            If ExecuteSQLNonQuery("DELETE FROM XUATKHO WHERE SoLuong=0 AND SoPhieu='" & tbSoPhieu.EditValue & "'") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If ExecuteSQLNonQuery("DELETE FROM XUATKHOAUX WHERE SoLuong=0 AND SoPhieu='" & tbSoPhieu.EditValue & "'") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If ExecuteSQLNonQuery("UPDATE THU SET TamUng=0 WHERE THU.MucDich=100 AND THU.PhieuTC0='" & gdvPhieuCG.EditValue & "' UPDATE THUNH SET TamUng=0 WHERE THUNH.MucDich=100 AND THUNH.PhieuTC0='" & gdvPhieuCG.EditValue & "'") Is Nothing Then Throw New Exception(LoiNgoaiLe)


            ' ComitTransaction()
            TrangThai.isUpdate = True
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                gdvPhieuCG.Enabled = False
                gdvMaKH.Enabled = False
            End If

            For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
                Dim sql As String = ""
                If Not IsDBNull(gdvThamChieuCT.GetRowCellValue(i, "IDVatTu")) And Not gdvThamChieuCT.GetRowCellValue(i, "IDVatTu") Is Nothing Then
                    sql = " Update CHAOGIA Set CanXuat = (Case TrangThai WHEN 2 then (Soluong - (select (isnull(sum(soluong),0)) from XUATKHO where IDChaogia = " & gdvThamChieuCT.GetRowCellValue(i, "IDCG") & ")) ELSE 0 END) Where ID = " & gdvThamChieuCT.GetRowCellValue(i, "IDCG")

                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        ShowBaoLoi(sql)
                    End If
                End If
            Next
            If TrangThai.isAddNew Then
                ShowAlert("Đã tạo xuất kho !")
            Else
                ShowAlert("Đã cập nhật xuất kho !")
            End If

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                AddParameter("@ThoiGianTra", modifyDate)
                AddParameter("@GhiChu", "Xuất kho: " & tbSoPhieu.EditValue)
                AddParameter("@TrangThai", 1)
                AddParameterWhere("@SP", gdvPhieuCG.EditValue)
                AddParameterWhere("@IDVT", gdvVTCT.GetRowCellValue(i, "IDVatTu"))
                AddParameterWhere("@SL", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                If doUpdate("tblXuatMuon", "IDVatTu=@IDVT AND SoLuong=@SL AND SoPhieuCG=@SP AND TrangThai=0") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            Next

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mXuatKho" Then
                    Dim Index As Object = CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKho).gdvCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKho).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKho).gdvCT.FocusedRowHandle = Index
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKho).gdvCT.SelectRow(Index)
                ElseIf deskTop.tabMain.TabPages(i).Tag = "mChaoGiaCanXuat" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmCGCanXuatKho).LoadDS()
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mTheoXuatKho.Name Then
                    Dim Index As Object = CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaXuatKho).gdvCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaXuatKho).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaXuatKho).gdvCT.FocusedRowHandle = Index
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKetQuaXuatKho).gdvCT.SelectRow(Index)
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mXuatMuon.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatMuon).btTaiDS.PerformClick()
                End If
            Next
            LoadThamChieuCG(gdvPhieuCG.EditValue)
            loadXuatKho(tbSoPhieu.EditValue)

            Dim tinhphanbo As String = CapNhatPhanBoTamUng(gdvPhieuCG.EditValue)
            If tinhphanbo <> "OK" Then
                ShowBaoLoi(tinhphanbo)
            End If

        Catch ex As Exception
            '  RollBackTransaction()
            ShowBaoLoi(ex.Message)
            If TrangThai.isAddNew Then
                tbSoPhieu.EditValue = Nothing
            End If
        End Try

    End Sub


    ''' <summary>
    ''' Tính giá trị phân bổ tạm ứng cho xuất kho
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CapNhatPhanBoTamUng(SoChaoGia As String) As String

        Dim kq As String = "OK"
        Dim sql As String = ""

        Try

            'Lấy mức phần trăm phân bổ
            sql = "select TraTruoc1,TraTruoc2,TraSau1 FROM DM_HINH_THUC_TT WHERE ID = (select IDHinhThucTT2 from bangchaogia where SoPhieu = @SoCG) "
            AddParameter("@SoCG", SoChaoGia)
            Dim dtMucPhanBo As DataTable = ExecuteSQLDataTable(sql)
            If dtMucPhanBo Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim TyLePhanBo As Double = 1

            If dtMucPhanBo.Rows.Count > 0 Then
                Dim r As DataRow = dtMucPhanBo.Rows(0)
                If r("TraTruoc1") > 0 And r("TraTruoc2") > 0 And r("TraSau1") >= 0 Then
                    TyLePhanBo = (r("TraTruoc1") + r("TraTruoc2")) / 100.0
                Else
                    TyLePhanBo = r("TraTruoc1") / 100.0
                End If
            End If

            'Lấy tổng tiền tạm ứng cho chào giá
            'Mục đích 100 là bán hàng
            sql = "select "
            sql &= "(select isnull(sum(Sotien),0) FROM THU where MucDich = 100 AND PhieuTC0 = @SoCG) "
            sql &= " + "
            sql &= "(select isnull(sum(sotien),0) from THUNH WHERE MucDich = 100 AND PhieuTC0 = @SoCG) "
            AddParameter("@SoCG", SoChaoGia)
            Dim TienTamUng As Double = ExecuteSQLDataTable(sql).Rows(0)(0)

            'Lấy danh sách xuất kho tương ứng với chào giá này
            AddParameter("@SoCG", SoChaoGia)
            Dim dtXuatKho As DataTable = ExecuteSQLDataTable("SELECT SoPhieu, (Tientruocthue+Tienthue)ThanhTien FROM PHIEUXUATKHO WHERE SophieuCG = @SoCG ORDER BY NgayThang ASC")

            Dim SoDuTamUng As Double = TienTamUng

            For Each r As DataRow In dtXuatKho.Rows

                Dim tienPhanBo As Double = r("ThanhTien") * TyLePhanBo

                If TienTamUng <> 0 Then
                    SoDuTamUng = SoDuTamUng - tienPhanBo
                    AddParameter("@PhanBoTamUng", tienPhanBo)
                    AddParameter("@SoDuTamUng", SoDuTamUng)
                    AddParameterWhere("@Sophieu", r("SoPhieu"))
                    If doUpdate("PHIEUXUATKHO", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else

                    AddParameter("@PhanBoTamUng", 0)
                    AddParameter("@SoDuTamUng", 0)
                    AddParameterWhere("@Sophieu", r("SoPhieu"))
                    If doUpdate("PHIEUXUATKHO", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If


            Next

            AddParameter("@DaTamUng", TienTamUng)
            AddParameter("@TamUngConLai", SoDuTamUng)
            AddParameterWhere("@Sophieu", SoChaoGia)
            If doUpdate("BANGCHAOGIA", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Return "OK"

        Catch ex As Exception
            kq = ex.Message
        End Try

        Return kq

    End Function

    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvVTCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "LoiGia" Then
            If e.CellValue = 1 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub

    Private Sub btTichThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTichThue.ItemClick
        Dim check As Boolean = gdvVTCT.GetRowCellValue(0, "XuatThue")
        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "XuatThue", Not check)
        Next
        gdvVTCT.EndUpdate()
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
    End Sub

    Private Sub mChonBoChon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChonBoChon.ItemClick
        gdvThamChieuCT.BeginUpdate()
        chkChonHet.Checked = Not chkChonHet.Checked
        For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
            gdvThamChieuCT.SetRowCellValue(i, "Chon", chkChonHet.Checked)
        Next
        gdvThamChieuCT.EndUpdate()
    End Sub

    Private Sub gdvVTCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvVTCT.RowCellClick
        If e.Column.FieldName = "XuatThue" Then
            gdvVTCT.SetRowCellValue(e.RowHandle, "XuatThue", Not e.CellValue)
        ElseIf e.Column.FieldName = "isGiaDacBiet" Then
            If IsDBNull(e.CellValue) Then
                gdvVTCT.SetRowCellValue(e.RowHandle, "isGiaDacBiet", True)
            Else
                gdvVTCT.SetRowCellValue(e.RowHandle, "isGiaDacBiet", Not e.CellValue)
            End If
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub tbTienTruocThue_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTienTruocThue.EditValueChanged, tbTienThue.EditValueChanged
        tbTongXK.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
    End Sub

    Private Sub tbTruocThueCG_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTruocThueCG.EditValueChanged, tbTienThueCG.EditValueChanged
        tbTongCG.EditValue = tbTruocThueCG.EditValue + tbTienThueCG.EditValue
    End Sub

    Private Sub btCapNhatGia_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatGia.Click
        If ShowCauHoi("Cập nhật giá xuất kho ? ") Then
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()
            For i As Integer = 0 To gdvThamChieuCT.DataRowCount - 1
                For j As Integer = 0 To gdvVTCT.DataRowCount - 1
                    If IsDBNull(gdvThamChieuCT.GetRowCellValue(i, "IDVatTu")) Then Continue For
                    If gdvThamChieuCT.GetRowCellValue(i, "IDVatTu") = gdvVTCT.GetRowCellValue(j, "IDVatTu") And gdvThamChieuCT.GetRowCellValue(i, "IDCG") = gdvVTCT.GetRowCellValue(j, "IDChaoGia") Then
                        gdvVTCT.SetRowCellValue(j, "DonGia", gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                    End If
                Next
            Next
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()
            btCal.PerformClick()
            ShowAlert("Đã cập nhật!")
        End If
    End Sub


    Private Sub mnuSaoChepDongHangHoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSaoChepDongHangHoa.ItemClick
        If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
        'If Not ShowCauHoi("Tách số lượng hàng hóa này?") Then Exit Sub
        If gdvVTCT.GetFocusedRowCellValue("SoLuong") <= 1 Then
            ShowCanhBao("Số lượng không đủ tách!")
            Exit Sub
        End If


        Dim str As String = XtraInputBox.Show("Nhập số lượng cần tách:", "Chú ý!", "")
        If str = "" Then Exit Sub

        Dim sl As Double = 0
        Try
            sl = Convert.ToDouble(str)
        Catch ex As Exception
            ShowCanhBao("Số lượng không hợp lệ!")
            Exit Sub
        End Try

        If sl < 1 Or sl >= gdvVTCT.GetFocusedRowCellValue("SoLuong") Then
            ShowCanhBao("Số lượng phải >=1 và < " & gdvVTCT.GetFocusedRowCellValue("SoLuong"))
            Exit Sub
        End If

        Dim dt As DataTable = CType(gdvVT.DataSource, DataTable)
        Dim index As Integer = gdvVTCT.FocusedRowHandle
        gdvVTCT.AddNewRow()
        For i As Integer = 0 To gdvVTCT.Columns.Count - 1
            If gdvVTCT.Columns(i).FieldName = "ID" Or gdvVTCT.Columns(i).FieldName = "AZ" Then Continue For
            gdvVTCT.SetFocusedRowCellValue(gdvVTCT.Columns(i), gdvVTCT.GetRowCellValue(index, gdvVTCT.Columns(i)))
        Next
        gdvVTCT.SetFocusedRowCellValue("SoLuong", sl)
        gdvVTCT.SetRowCellValue(index, "SoLuong", gdvVTCT.GetRowCellValue(index, "SoLuong") - sl)
    End Sub


End Class