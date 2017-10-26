Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports BACSOFT.BAC
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO
Imports DevExpress.XtraPrinting
Imports System.Runtime.Serialization
Imports SpreadsheetGear
Imports System.Globalization

Public Class frmDSYCLamHQ
    Public tgmakh As String
    Protected Shared _message As Integer = 0

    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property
    'Protected Shared _kiemtra As Integer
    'Public Property Kiemtra() As Integer
    '    Get
    '        Return _kiemtra
    '    End Get
    '    Set(ByVal value As Integer)
    '        _kiemtra = value
    '    End Set
    'End Property
    Private Sub loadGVHaiQuan()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim query As String = " SELECT "
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " HaiQuan_LamHaiQuan.*, filehaiquan, NHANSU.Ten as tennguoixuly, (select Ten from NHANSU where ID =Idnguoilap) as tennguoilap, ttcMa,TenENG,ttcDienthoai, ttcFax, isnull(ttcDiachi_HQ,ttcDiachiHQ) ttcDiachiHQ, isnull(PhuongThucThanhToan_HQ,PhuongThucThanhToanHQ) PhuongThucThanhToanHQ"
        query &= " from HaiQuan_LamHaiQuan inner join KHACHHANG on IDKhachhang =KHACHHANG .id  "
        query &= "      LEFT OUTER JOIN HaiQuan_FileHaiQuan  ON HaiQuan_FileHaiQuan.idlamhaiquan   =HaiQuan_LamHaiQuan .id "
        query &= "   LEFT OUTER JOIN NHANSU ON NHANSU.ID  =idnguoinhanxuly "
        query &= " where 1=1"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then

            AddParameterWhere("@TuNgay", barDeTuNgay.EditValue)
            AddParameterWhere("@DenNgay", barDeDenNgay.EditValue)
            query &= " AND Convert(datetime,CONVERT(nvarchar,ngayht,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If barGlueNguoiLap.EditValue IsNot Nothing Then
            AddParameterWhere("@Idnguoilap", barGlueNguoiLap.EditValue)
            query &= " and Idnguoilap=@Idnguoilap"
        End If
        If barLueMaKH.EditValue IsNot Nothing Then
            AddParameterWhere("@IDKhachhang", barLueMaKH.EditValue)
            query &= " and IDKhachhang=@IDKhachhang"
        End If
        If barLueTinhTrang.EditValue IsNot Nothing Then
            AddParameterWhere("@idtinhtrang_haiquan", barLueTinhTrang.EditValue)
            query &= " and idtinhtrang_haiquan=@idtinhtrang_haiquan"
        End If
        'If _message <> 0 And (deskTop.tabMain.SelectedTabPage.Text = "Hàng đã chào giá" Or deskTop.tabMain.SelectedTabPage.Text = "Yêu cầu đi - Đặt hàng") Then
        '    query &= " and HaiQuan_LamHaiQuan.id=" & _message
        'End If
        If _message <> 0 And Me.Name = "frmDSYCLamHQpopup" Then
            query &= " and HaiQuan_LamHaiQuan.id=" & _message
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Làm hải quan" Then
            query &= " and idtinhtrang_haiquan>0 and idtinhtrang_haiquan<4"
        End If
        query &= " order by id desc"
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            Dim row = gvDsVatTuHaiQuan.FocusedRowHandle
            gcDsVatTuHaiQuan.DataSource = dt
            gvDsVatTuHaiQuan.FocusedRowHandle = row
            CloseWaiting()
            If dt.Rows.Count < 1 Then
                btnThemCT.Enabled = False
            Else
                btnThemCT.Enabled = True
            End If
            If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgnhanxuly")) Then
                btnNhanXuLy.Enabled = True
                btnCapNhat.Enabled = False
                mnu_NhanXuLy.Enabled = True
                mnu_HoanThanh.Enabled = False
            Else
                btnNhanXuLy.Enabled = False
                btnCapNhat.Enabled = True
                mnu_NhanXuLy.Enabled = False
                mnu_HoanThanh.Enabled = True
            End If
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub
    Private Sub loadGVHaiQuanCT(ByVal idlamhaiquan As Object)
        Dim query = " SELECT ttcMa,  CHAOGIA.Sophieu,CHAOGIA.IDVatTu as IDVatTuHQ,TENVATTU.Ten AS TenVT,VATTU.Thongso, VATTU.IdTenvattu "
        query &= " ,  Soluong,(ISNULL(DonGiaLamHQ, isnull(CHAOGIA.Dongia,0)) * ISNULL(SoLuongLamHQ,0)) AS ThanhTien,"
        query &= "   ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        query &= "   tblTienTe.Ten AS TenTienTe, VATTU.HangTon, ISNULL(CHAOGIA.AZ,0)AZ,"
        query &= "    HaiQuan_ChiTietLamHaiQuan.id, HaiQuan_ChiTietLamHaiQuan.idchaogia, isnull(TenTrongPO, TENVATTU.Ten) as TenTrongPO, tendexuat, SoLuongLamHQ, (Soluong-(select isnull(SUM(SoLuongLamHQ),0) from HaiQuan_ChiTietLamHaiQuan where idchaogia=CHAOGIA.ID)) as SoLuongConLai, ngaypo, TENVATTU.Ten_ENG "
        query &= " , isnull(HaiQuan_ChiTietLamHaiQuan.sopo,BANGCHAOGIA.SoPO) as SoPOHQ, CongTrinhHQ"
        query &= " ,  ISNULL(MaHangLamHQ,VATTU.Model) as Model"
        query &= " ,  ISNULL(HangSXLamHQ,TENHANGSANXUAT.Ten)  AS TenHang"
        query &= " ,  ISNULL(XuatSuLamHQ,TENNUOC.TEN)  AS Tennuoc"
        query &= " ,  case when XuatSuLamHQ is not null then (select top 1 MaNuoc from TENNUOC t where  LTRIM(RTRIM(t.TEN))= LTRIM(RTRIM(XuatSuLamHQ))) else '' end  AS TenVietTat"
        query &= " ,  ISNULL(DonGiaLamHQ, isnull(CHAOGIA.Dongia,0)) AS DonGia"
        query &= " ,  ISNULL(TenDVTLamHQ, TENDONVITINH.Ten) AS TenDVT"
        query &= " ,  (select top 1 Ten_ENG from TENDONVITINH where LTRIM(RTRIM(Ten))=LTRIM(RTRIM(TenDVTLamHQ))) AS TenDVT_ENG"
        query &= " ,  ISNULL(HaiQuan_ChiTietLamHaiQuan.MoTaHQ, VATTU.MoTaHQ) AS MoTaHQ"
        query &= " ,  ISNULL(HaiQuan_ChiTietLamHaiQuan.MaHSHQ, VATTU.MaHSHQ) AS MaHSHQ"
        query &= " ,  CAST(case when GopVTLamHQ IS NULL then 0 else 1 end as bit) AS GopVTLamHQ"
        query &= " ,  GopVTLamHQ as gop2, ThongTinDacBiet "
        ' query &= " ,  IDVatTuHQ"
        query &= "     FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        query &= "    LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        query &= "    LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        query &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        query &= "      LEFT OUTER JOIN tblTienTe ON CHAOGIA.Tiente=tblTienTe.ID"
        query &= "    LEFT OUTER JOIN TENNUOC  ON VATTU.IDTennuoc =TENNUOC.ID"
        query &= "      LEFT OUTER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu  =CHAOGIA .SoPhieu "
        ' query &= "      inner JOIN HaiQuan_ChiTietLamHaiQuan  ON idchaogia   =CHAOGIA .id "
        query &= "      Right OUTER JOIN HaiQuan_ChiTietLamHaiQuan  ON idchaogia   =CHAOGIA .id "
        query &= "     inner join HaiQuan_LamHaiQuan on HaiQuan_LamHaiQuan.id=idlamhaiquan"
        query &= "     inner join KHACHHANG on HaiQuan_LamHaiQuan.IDKhachhang =KHACHHANG .id"
        '    query &= "     left join HaiQuan_MaHS on HaiQuan_MaHS.Model =VATTU.Model and HaiQuan_MaHS.IDTenvattu =VATTU.IDTenvattu "
        '    query &= "     left join HaiQuan_MoTa on HaiQuan_MoTa.Model =VATTU.Model and HaiQuan_MoTa.IDTenvattu =VATTU.IDTenvattu"
        '  query &= "      LEFT OUTER JOIN HaiQuan_FileHaiQuan  ON HaiQuan_FileHaiQuan.idlamhaiquan   =HaiQuan_LamHaiQuan .id "
        query &= "      WHERE idlamhaiquan = @idlamhaiquan and ( CHAOGIA.Sophieu  is not null  or idchaogia =0  )  "

        If barChkHienVtDaGop.Checked = False Then
            query &= "  AND GopVTLamHQ IS NULL "
        End If
        '  query &= "      ORDER BY Sophieu,AZ desc   "

        query &= " union"

        query &= " SELECT ttcMa,  DATHANG.Sophieu,DATHANG.IDVatTu as IDVatTuHQ,TENVATTU.Ten AS TenVT,VATTU.Thongso, VATTU.IdTenvattu "
        query &= " , Soluong,(ISNULL(DonGiaLamHQ, isnull(DATHANG.Dongia,0)) * ISNULL(SoLuongLamHQ,0)) AS ThanhTien,"
        query &= "   ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=DATHANG.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=DATHANG.IDVattu)) AS slTon,"
        query &= "     tblTienTe.Ten AS TenTienTe, VATTU.HangTon, ISNULL(DATHANG.AZ,0)AZ,"
        query &= "     HaiQuan_ChiTietLamHaiQuan.id, HaiQuan_ChiTietLamHaiQuan.idchaogia, isnull(TenTrongPO, TENVATTU.Ten) as TenTrongPO, tendexuat, SoLuongLamHQ, (Soluong-(select isnull(SUM(SoLuongLamHQ),0) from HaiQuan_ChiTietLamHaiQuan where idchaogia=DATHANG.ID)) as SoLuongConLai, ngaypo, TENVATTU.Ten_ENG  "
        query &= " ,  HaiQuan_ChiTietLamHaiQuan.sopo as SoPOHQ, CongTrinhHQ"
        query &= " ,  ISNULL(MaHangLamHQ,VATTU.Model) as Model"
        query &= " ,  ISNULL(HangSXLamHQ,TENHANGSANXUAT.Ten)  AS TenHang"
        query &= " ,  ISNULL(XuatSuLamHQ,TENNUOC.TEN)  AS Tennuoc"
        query &= " ,  case when XuatSuLamHQ is not null then (select top 1 MaNuoc from TENNUOC t where  LTRIM(RTRIM(t.TEN))= LTRIM(RTRIM(XuatSuLamHQ))) else '' end  AS TenVietTat"
        query &= " ,  ISNULL(DonGiaLamHQ, isnull(DATHANG.Dongia,0)) AS DonGia"
        query &= " ,  ISNULL(TenDVTLamHQ, TENDONVITINH.Ten) AS TenDVT"
        query &= " ,  (select top 1 Ten_ENG from TENDONVITINH where LTRIM(RTRIM(Ten))=LTRIM(RTRIM(TenDVTLamHQ))) AS TenDVT_ENG"
        query &= " ,  ISNULL(HaiQuan_ChiTietLamHaiQuan.MoTaHQ, VATTU.MoTaHQ) AS MoTaHQ"
        query &= " ,  ISNULL(HaiQuan_ChiTietLamHaiQuan.MaHSHQ, VATTU.MaHSHQ) AS MaHSHQ"
        query &= " ,  CAST(case when GopVTLamHQ IS NULL then 0 else 1 end as bit) AS GopVTLamHQ"
        query &= " ,  GopVTLamHQ as gop2, ThongTinDacBiet"
        ' query &= " ,  IDVatTuHQ"
        query &= "     FROM DATHANG LEFT OUTER JOIN VATTU ON DATHANG.IDvattu=VATTU.ID"
        query &= "    LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        query &= "    LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        query &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        query &= "      LEFT OUTER JOIN tblTienTe ON DATHANG.Tiente=tblTienTe.ID"
        query &= "    LEFT OUTER JOIN TENNUOC  ON VATTU.IDTennuoc =TENNUOC.ID"
        query &= "      LEFT OUTER JOIN PHIEUDATHANG ON PHIEUDATHANG.Sophieu  =DATHANG .SoPhieu or PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu "
        query &= "      Right OUTER JOIN HaiQuan_ChiTietLamHaiQuan  ON idchaogia   =DATHANG .id "
        query &= "     inner join HaiQuan_LamHaiQuan on HaiQuan_LamHaiQuan.id=idlamhaiquan"
        query &= "     inner join KHACHHANG on HaiQuan_LamHaiQuan.IDKhachhang =KHACHHANG .id"
        '   query &= "     left join HaiQuan_MaHS on HaiQuan_MaHS.Model =VATTU.Model and HaiQuan_MaHS.IDTenvattu =VATTU.IDTenvattu "
        '  query &= "     left join HaiQuan_MoTa on HaiQuan_MoTa.Model =VATTU.Model and HaiQuan_MoTa.IDTenvattu =VATTU.IDTenvattu"
        '  query &= "   LEFT OUTER PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu "
        query &= "      WHERE idlamhaiquan = @idlamhaiquan and ( (DATHANG.Sophieu is not null or DATHANG.SoPhieuPhu is not null ) or idchaogia =0  ) "
        If barChkHienVtDaGop.Checked = False Then
            query &= "  AND GopVTLamHQ IS NULL "
        End If
        query &= "      ORDER BY HaiQuan_ChiTietLamHaiQuan.id asc  "
        AddParameterWhere("@idlamhaiquan", idlamhaiquan)
        'gcDsVatTuHaiQuanCT.DataSource = ExecuteSQLDataTable(query)
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            With dt
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1

                Next
            End With
            Dim row = gvDsVatTuHaiQuanCT.FocusedRowHandle
            gcDsVatTuHaiQuanCT.DataSource = dt
            gvDsVatTuHaiQuanCT.FocusedRowHandle = row
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        '    riSeSLHQ.MaxValue = ExecuteSQLScalar("select Soluong from CHAOGIA ")
    End Sub
    Private Sub loadFooter(ByVal idlamhaiquan As Object)
        If idlamhaiquan IsNot Nothing Then
            Dim query = "select thongtindonggoi, cantinh, cankhoi, soluongthung, loaithung from HaiQuan_LamHaiQuan where id=" & idlamhaiquan.ToString()
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            If Not dt Is Nothing Then
                barPCTTDongGoi.EditValue = dt.Rows(0).Item(0)
                MeTTDongGoi.EditValue = dt.Rows(0).Item(0)
                barSeCanTinh.EditValue = dt.Rows(0).Item(1)
                barSeCanKhoi.EditValue = dt.Rows(0).Item(2)
                barSeSLThung.EditValue = dt.Rows(0).Item(3)
                barTeLoaiThung.EditValue = dt.Rows(0).Item(4)
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If

    End Sub
    Private Sub loadData()
        barCbbXem.EditValue = "Top 500"
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
        barDeTuNgay.EditValue = New DateTime(Today.Year, 1, 1)
        barDeDenNgay.EditValue = Today.Date
        Dim query = "SELECT ID,Ten,GioiTinh FROM NHANSU WHERE Noictac=74 AND Trangthai=1 "
        Dim tb = ExecuteSQLDataTable(query)
        riGlueNguoiLap.DataSource = tb
        riGlueNguoiLap.View.PopulateColumns(riGlueNguoiLap.DataSource)
        riGlueNguoiLap.View.Columns(riGlueNguoiLap.ValueMember).Visible = False
        '   riGlueNguoiLap.View.Columns("GioiTinh").Visible = False
        griGlueNguoiLap.DataSource = tb
        griGlueNguoiLap.View.PopulateColumns(griGlueNguoiLap.DataSource)
        griGlueNguoiLap.View.Columns(griGlueNguoiLap.ValueMember).Visible = False
        riLueTinhTrang.DataSource = tableTinhTrangHaiQuan(1)
        If deskTop.tabMain.SelectedTabPage.Text = "Làm hải quan" Then
            riLueTinhTrang.DataSource = tableTinhTrangHaiQuan(2)
        End If
        riLueLuong.DataSource = tableLuongHaiQuan()
        barriLueTinhTrang.DataSource = tableTinhTrangHaiQuan(2)
        query = "select ID, ttcMa from KHACHHANG WHERE ID IN (select IDKhachhang from HaiQuan_LamHaiQuan)"
        riLueMaKh.DataSource = ExecuteSQLDataTable(query)

        gColMoTa.Visible = False
        gColMaHS.Visible = False

        If deskTop.tabMain.SelectedTabPage.Text = "Làm hải quan" Or deskTop.tabMain.SelectedTabPage.Text = "Danh sách yêu cầu làm hải quan" Then
            gColPhanHoi.Visible = True
            gColTgNhanXL.Visible = True
            '    gColSoTK.Visible = True
            '   gColLuong.Visible = True
            '   gColChiPhi.Visible = True
            gColTinhTrang.Visible = False
            '  gColNguoiXuLy.Visible = True
            gColFile.Visible = True
            gColGhiChu.Visible = True
            If deskTop.tabMain.SelectedTabPage.Text = "Làm hải quan" Then
                barLueTinhTrang.EditValue = 1
            End If
            If deskTop.tabMain.SelectedTabPage.Text = "Danh sách yêu cầu làm hải quan" Then
                barGlueNguoiLap.EditValue = CType(TaiKhoan, Int32)
            End If
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng đã chào giá" Or deskTop.tabMain.SelectedTabPage.Text = "Yêu cầu đi - Đặt hàng" Then
            barCbbXem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barDeTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barDeDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barLueMaKH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barLueTinhTrang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barCiLoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gColTinhTrang.Visible = False
            barGlueNguoiLap.EditValue = CType(TaiKhoan, Int32)
            If deskTop.tabMain.SelectedTabPage.Text = "Hàng đã chào giá" Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmVatTuDaChaoGia).barBtnLamHQ.Enabled = False
            End If
            If deskTop.tabMain.SelectedTabPage.Text = "Yêu cầu đi - Đặt hàng" Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).barBtnLamHQ.Enabled = False
            End If

        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Làm hải quan" Then
            btnGhiLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnXoaCT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnThemCT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnu_ThemCT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnu_XoaCT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barChkBoSungVTCT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnNhanXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnCapNhat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnPhanHoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnTraLaiKD.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            ' btnExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            gColPhanHoi.OptionsColumn.AllowEdit = True
            gcolNgayThongQuan.OptionsColumn.AllowEdit = True
            gcolDiaChiLamHQ.OptionsColumn.AllowEdit = True
            gColPTTT.OptionsColumn.AllowEdit = True
            gColMaKH.OptionsColumn.AllowEdit = False
            gColNgayHT.OptionsColumn.AllowEdit = False
            gColSoHD.OptionsColumn.AllowEdit = False
            gColSoHD.OptionsColumn.AllowEdit = False
            gColLan.OptionsColumn.AllowEdit = False
            gColDau.OptionsColumn.AllowEdit = False
            gColNgayHD.OptionsColumn.AllowEdit = False
            'gColNDYC.OptionsColumn.AllowEdit = False
            ' gColGhiChu.OptionsColumn.AllowEdit = False
            gColTinhTrang.OptionsColumn.AllowEdit = True
           
            gColNgayHT.VisibleIndex = 1
            gcolNgayThongQuan.VisibleIndex = 2
            gColSoHD.VisibleIndex = 3
            gColNgayHD.VisibleIndex = 4
            gColDau.VisibleIndex = 5
            gColtgchuyentuchaogia.VisibleIndex = 6
            gColtgchuyensanghaiquan.VisibleIndex = 7
            gColNDYC.VisibleIndex = 8
            gColFile.VisibleIndex = 9
            gColNguoiLap.VisibleIndex = 10
            gColNguoiXuLy.VisibleIndex = 11
            gColLan.VisibleIndex = 12
            gColPhanHoi.VisibleIndex = 13
            gColGhiChu.VisibleIndex = 14
            gColTgNhanXL.VisibleIndex = 15
            gColTinhTrang.VisibleIndex = 16
            gColSoTK.VisibleIndex = 17
            gColLuong.VisibleIndex = 18
            gColChiPhi.VisibleIndex = 19
            gColGTChiPhi.VisibleIndex = 20
            'ct
            gColTyGiaHQ.VisibleIndex = 21
            gColDonViTien.VisibleIndex = 22
            gColSLConLai.Visible = False
            gColSoLuong.Visible = False
            gColGopVT.Visible = False
            gColTenVT.Visible = False
            gColPhanHoi.AppearanceHeader.ForeColor = Color.Blue
            gColGTChiPhi.AppearanceHeader.ForeColor = Color.Blue
            gColDonViTien.AppearanceHeader.ForeColor = Color.Blue
            gColTyGiaHQ.AppearanceHeader.ForeColor = Color.Blue
            gColTinhTrang.AppearanceHeader.ForeColor = Color.Blue
            btThemFile.Enabled = False
            btXoaFile.Enabled = False
            barBtnGopVT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            barChkHienVtDaGop.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gColMoTa.VisibleIndex = 4
            gColMaHS.VisibleIndex = 5
            gColMoTa.OptionsColumn.AllowEdit = False
            gColMaHS.OptionsColumn.AllowEdit = False
            mnu_ChinhSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnExcelLamHQ.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            pcInovice.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Danh sách yêu cầu làm hải quan" Then
            ' gColNgayPO.AppearanceHeader.ForeColor = Color.Blue
            '  gColTenDeXuat.AppearanceHeader.ForeColor = Color.Blue
            '  gColSoPO.AppearanceHeader.ForeColor = Color.Blue
            gColSLHQ.AppearanceHeader.ForeColor = Color.Red
            gColTgNhanXL.VisibleIndex = 9
            gColTinhTrang.VisibleIndex = 10
            gColPhanHoi.VisibleIndex = 11
            gColGhiChu.VisibleIndex = 12
            gColFile.VisibleIndex = 13
            '  gColNguoiLap.VisibleIndex = 14
            If KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                gcolNguoiLap1.VisibleIndex = 14
            Else
                gColNguoiLap.VisibleIndex = 14
            End If
            gColNguoiXuLy.VisibleIndex = 15
            mnu_ChinhSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        cbbLoaiInovice.EditValue = "Chung"
        '  loadGVHaiQuan()
    End Sub

    Private Sub barCbbXem_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbXem.EditValueChanged
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            barDeTuNgay.Enabled = True
            barDeDenNgay.Enabled = True
            Exit Sub
        End If
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
    End Sub

    Private Sub frmDSVatTuLamHaiQuan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '    btnGhiLai.Enabled = False
        'End If
    End Sub


    Private Sub gvDsVatTuHaiQuan_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gvDsVatTuHaiQuan.FocusedRowChanged
        If mnu_ChinhSua.Caption <> "Chỉnh sửa" Then
            gvDsVatTuHaiQuan.FocusedRowHandle = focusrow
            Exit Sub
        End If
        loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        loadFooter(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        tgmakh = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa")
        '    Dim tgnhanxuly = gvDsVatTuHaiQuan.GetFocusedRowCellDisplayText("tgnhanxuly")
        If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgnhanxuly")) Then
            btnNhanXuLy.Enabled = True
            btnCapNhat.Enabled = False
            mnu_NhanXuLy.Enabled = True
            mnu_HoanThanh.Enabled = False
        Else
            btnNhanXuLy.Enabled = False
            btnCapNhat.Enabled = True
            mnu_NhanXuLy.Enabled = False
            If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")) Then
                mnu_HoanThanh.Enabled = True
            Else
                mnu_HoanThanh.Enabled = False
            End If

        End If
        If (gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 2 And gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 3) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            btnXoa.Enabled = True
            btnThemCT.Enabled = True
            btnXoaCT.Enabled = True
            barBtnGopVT.Enabled = True
            barChkBoSungVTCT.Enabled = True
        Else
            btnXoa.Enabled = False
            btnThemCT.Enabled = False
            btnXoaCT.Enabled = False
            barBtnGopVT.Enabled = False
            barChkBoSungVTCT.Checked = False
            barChkBoSungVTCT.Enabled = False
        End If
        AddParameterWhere("@IDKhachhang", gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDKhachhang"))
        gcSoCG.DataSource = ExecuteSQLDataTable("select distinct BANGCHAOGIA.SoPhieu from BANGCHAOGIA inner join CHAOGIA on BANGCHAOGIA.Sophieu =CHAOGIA .SoPhieu where Congtrinh =1 and IDKhachhang =@IDKhachhang")
    End Sub

    Private Sub btnGhiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGhiLai.ItemClick
        If _message = 0 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        End If
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không phải người lập")
        Else
            If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 3 Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                'If deskTop.tabMain.SelectedTabPage.Text = "Hàng đã chào giá" Or deskTop.tabMain.SelectedTabPage.Text = "Yêu cầu đi - Đặt hàng" Then
                '    AddParameter("@idtinhtrang_haiquan", 1)
                'End If
              
                gvDsVatTuHaiQuan.PostEditor()
                gvDsVatTuHaiQuan.UpdateCurrentRow()
                Dim hientai As DateTime = GetServerTime()
                If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")) Then
                    AddParameter("@tgchuyensanghaiquan", hientai)
                End If
                If KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                    AddParameter("@IDnguoilap", gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap"))
                End If
                If Me.Name = "frmDSYCLamHQpopup" Then
                    AddParameter("@idtinhtrang_haiquan", 4)
                End If
                AddParameter("@dau", gvDsVatTuHaiQuan.GetFocusedRowCellValue("dau"))
                AddParameter("@sohoadon", gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon"))
                AddParameter("@ngayhoadon", gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon"))
                '    AddParameter("@noidungyeucau", gvDsVatTuHaiQuan.GetFocusedRowCellValue("noidungyeucau"))
                '   AddParameter("@PhuongThucThanhToan_HQ", gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ"))
                ' AddParameter("@ttcDiachi_HQ", gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcDiachiHQ"))
                ' AddParameter("@NgayThongQuan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("NgayThongQuan"))
                '   AddParameter("@idtinhtrang_haiquan", 1)
                AddParameter("@thongtindonggoi", MeTTDongGoi.EditValue)
                AddParameter("@cantinh", barSeCanTinh.EditValue)
                AddParameter("@cankhoi", barSeCanKhoi.EditValue)
                AddParameter("@soluongthung", barSeSLThung.EditValue)
                AddParameter("@loaithung", barTeLoaiThung.EditValue)
                AddParameter("@ghichulamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("ghichulamhaiquan"))
                AddParameter("@noidungyeucau", gvDsVatTuHaiQuan.GetFocusedRowCellValue("noidungyeucau"))
                AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If

                Dim i As Integer
                For i = 0 To gvDsVatTuHaiQuanCT.RowCount - 1
                    If Not IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("id")) Then
                        gvDsVatTuHaiQuanCT.PostEditor()
                        gvDsVatTuHaiQuanCT.UpdateCurrentRow()
                        AddParameter("@tendexuat", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "tendexuat"))
                        AddParameter("@ngaypo", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "ngaypo"))
                        AddParameter("@sopo", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "SoPOHQ"))
                        If IsDBNull(gvDsVatTuHaiQuanCT.GetRowCellValue(i, "SoLuongLamHQ")) Then
                            AddParameter("@SoLuongLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "SoLuongConLai"))
                        Else
                            AddParameter("@SoLuongLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "SoLuongLamHQ"))
                        End If

                        AddParameter("@TenTrongPO", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "TenTrongPO"))
                        AddParameter("@MaHangLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "Model"))
                        AddParameter("@HangSXLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "TenHang"))
                        AddParameter("@XuatSuLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "Tennuoc"))
                        AddParameter("@DonGiaLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "DonGia"))
                        AddParameter("@TenDVTLamHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "TenDVT"))
                        AddParameter("@MoTaHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "MoTaHQ"))
                        AddParameter("@MaHSHQ", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "MaHSHQ"))
                        AddParameter("@ThongTinDacBiet", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "ThongTinDacBiet"))
                        'AddParameter("@tygiahaiquan", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "tygiahaiquan"))
                        AddParameterWhere("@id", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "id"))
                        If doUpdate("HaiQuan_ChiTietLamHaiQuan", "id=@id") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            Exit Sub
                        Else

                        End If
                    End If

                Next i
                ShowAlert("Thành công")
            Else
                ShowCanhBao("Đã hoàn thành làm hải quan không sửa được !")
            End If
        End If
        loadGVHaiQuan()
        loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        loadFooter(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
    End Sub

    Private Sub gvDsVatTuHaiQuanCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvDsVatTuHaiQuanCT.RowUpdated
        gvDsVatTuHaiQuanCT.PostEditor()
        gvDsVatTuHaiQuan.UpdateCurrentRow()
        If btnGhiLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never Then
            AddParameter("@MoTaHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ"))
            AddParameter("@MaHSHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ"))
            'AddParameter("@tygiahaiquan", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "tygiahaiquan"))
            AddParameterWhere("@id", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("id"))
            If doUpdate("HaiQuan_ChiTietLamHaiQuan", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            Else
                Dim d As Object
                '    AddParameterWhere("@IdTenvattu", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IdTenvattu"))
                AddParameterWhere("@ID", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IDVatTuHQ"))
                d = ExecuteSQLScalar("select MaHSHQ from VATTU where ID=@ID")
                If IsDBNull(d) And Not IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ")) And gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ") IsNot Nothing Then
                    AddParameter("@MaHSHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ"))
                    AddParameterWhere("@ID", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IDVatTuHQ"))
                    If doUpdate("VATTU", "ID=@ID") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)

                    End If

                End If
                AddParameterWhere("@ID", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IDVatTuHQ"))
                d = ExecuteSQLScalar("select MoTaHQ from VATTU where ID=@ID")
                If IsDBNull(d) And Not IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ")) And gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ") IsNot Nothing Then
                    AddParameter("@MoTaHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ"))
                    AddParameterWhere("@ID", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IDVatTuHQ"))
                    If doUpdate("VATTU", "ID=@ID") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)

                    End If

                End If
                Exit Sub
            End If

        End If
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then

            ShowCanhBao("Bạn không phải người lập")
        Else
            If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 3 Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                If IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("id")) Then
                    gvDsVatTuHaiQuanCT.PostEditor()
                    gvDsVatTuHaiQuanCT.UpdateCurrentRow()
                    AddParameter("@tendexuat", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("tendexuat"))
                    AddParameter("@ngaypo", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("ngaypo"))
                    AddParameter("@sopo", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoPOHQ"))
                    If IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ")) Then
                        AddParameter("@SoLuongLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongConLai"))
                    Else
                        AddParameter("@SoLuongLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ"))
                    End If

                    AddParameter("@TenTrongPO", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("TenTrongPO"))
                    AddParameter("@MaHangLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("Model"))
                    AddParameter("@HangSXLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("TenHang"))
                    AddParameter("@XuatSuLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("Tennuoc"))
                    AddParameter("@DonGiaLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("DonGia"))
                    AddParameter("@TenDVTLamHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("TenDVT"))
                    AddParameter("@ThongTinDacBiet", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("ThongTinDacBiet"))
                    AddParameter("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    ' AddParameter("@MoTaHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ"))
                    'AddParameter("@MaHSHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ"))
                    AddParameter("@CongTrinhHQ", 1)
                    AddParameter("@idchaogia", 0)
                    If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    Else
                        loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    End If
                End If
            End If

        End If
    End Sub
    Public _exit As Boolean = False
    '   Private RootUrlOld = "E:\\"
    Public UrlHaiQuan = "HAI QUAN\"
    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If _message = 0 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Or gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) Then Exit Sub
        End If
        Dim Impersonator As New Impersonator()
        Impersonator.BeginImpersonation()
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan").ToString = "" Then
            ShowCanhBao("nhập ngày chuyển")
        Else
            '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
            Dim path As String = ""
            Dim openFile As New OpenFileDialog
            openFile.Multiselect = True
            If openFile.ShowDialog = DialogResult.OK Then
                If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa")) Then
                    System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa"))
                End If
                ShowWaiting("Đang tải file lên máy chủ ...")
                For Each file In openFile.FileNames
                    Try
                        path = "HQ thang " & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Month.ToString & " lan " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("lan").ToString & " KD " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                        If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & path) Then
                            If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                                System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & path, 1)
                                gdvListFileCT.AddNewRow()
                                gdvListFileCT.SetFocusedRowCellValue("filehaiquan", path)

                            End If
                        Else
                            System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & path)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("filehaiquan", path)
                        End If

                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try
                Next
                CloseWaiting()
                gdvListFileCT.CloseEditor()
                gdvListFileCT.UpdateCurrentRow()
                _exit = True
                SendKeys.Send("{F4}")
            End If
            Impersonator.EndImpersonation()
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If _message = 0 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Or gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) Then Exit Sub
        End If
        Dim Impersonator As New Impersonator()
        Impersonator.BeginImpersonation()
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        ' If ShowCauHoi("Xoá file được chọn ?") Then
        Try

            Dim str = RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & gdvListFileCT.GetFocusedRowCellValue("filehaiquan")
            System.IO.File.Delete(str)

            gdvListFileCT.DeleteSelectedRows()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
        Impersonator.EndImpersonation()
    End Sub

    Private Sub btnTaiFileVeMay_Click(sender As System.Object, e As System.EventArgs) Handles btnTaiFileVeMay.Click
        If gdvListFileCT.RowCount > 0 Then
            If gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "filehaiquan").ToString <> "" Then
                Dim saveFile As New SaveFileDialog
                saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "filehaiquan"))
                saveFile.FileName = gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "filehaiquan")
                If saveFile.ShowDialog = DialogResult.OK Then
                    Try
                        ShowWaiting("Đang tải file về máy ...")
                        System.IO.File.Copy(RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "filehaiquan"), saveFile.FileName, True)
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
        End If

    End Sub
    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "filehaiquan" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & tgmakh & "\" & e.CellValue, e.CellValue, True)
            'Dim psi As New ProcessStartInfo()
            'With psi

            '    .FileName = RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & tgmakh & "\" & e.CellValue
            '    .UseShellExecute = True
            'End With
            'Try
            '    Process.Start(psi)
            'Catch ex As Exception
            '    ShowBaoLoi(ex.Message)
            'End Try

        End If
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("filehaiquan")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("filehaiquan", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()

    End Sub
    Private Sub riPCFileHaiQuan_Popup(sender As System.Object, e As System.EventArgs) Handles riPCFileHaiQuan.Popup
        If _exit Then
            _exit = False
            Exit Sub
        End If
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
        If gdvListFileCT.RowCount > 0 Then
            btnTaiFileVeMay.Enabled = True
        End If
    End Sub

    Private Sub riPCFileHaiQuan_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles riPCFileHaiQuan.Closed
        If deskTop.tabMain.SelectedTabPage.Text = "Danh sách yêu cầu làm hải quan" Or Me Is ActiveForm Then
            Dim dt As DataTable
            Dim query = "select id from HaiQuan_FileHaiQuan where idlamhaiquan=@idlamhaiquan"
            AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
            dt = ExecuteSQLDataTable(query)
            If gdvListFileCT.RowCount > 0 Then
                Dim _File As String = ""
                For i As Integer = 0 To gdvListFileCT.RowCount - 1
                    _File &= gdvListFileCT.GetRowCellValue(i, "filehaiquan")
                    If i < gdvListFileCT.RowCount - 1 Then
                        _File &= ";"
                    End If
                Next
                AddParameter("@filehaiquan", _File)
                If dt.Rows.Count > 0 Then
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doUpdate("HaiQuan_FileHaiQuan", "idlamhaiquan=@idlamhaiquan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        CType(sender, PopupContainerEdit).EditValue = _File

                    End If

                Else
                    AddParameter("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doInsert("HaiQuan_FileHaiQuan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        CType(sender, PopupContainerEdit).EditValue = _File

                    End If
                End If

            Else
                If dt.Rows.Count > 0 Then
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doDelete("HaiQuan_FileHaiQuan", "idlamhaiquan=@idlamhaiquan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ' CType(sender, PopupContainerEdit).EditValue = ""
                        loadGVHaiQuan()
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub btnTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiLai.ItemClick
        loadGVHaiQuan()
        loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        loadFooter(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không phải người lập")
        Else
            AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
            Dim kt = ExecuteSQLScalar("select idtinhtrang_haiquan from HaiQuan_LamHaiQuan where id=@id")
            If (kt <= 0 Or kt = 4) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                If ShowCauHoi("Có muốn xóa không ?") Then
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    Dim listFile = ExecuteSQLScalar("select filehaiquan from HaiQuan_FileHaiQuan where idlamhaiquan=@idlamhaiquan")
                    If listFile IsNot Nothing Then
                        Dim Impersonator As New Impersonator()
                        Impersonator.BeginImpersonation()
                        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
                        For Each _url In listUrl
                            If _url <> "" Then
                                Try
                                    Dim str = RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & _url
                                    System.IO.File.Delete(str)
                                    gdvListFileCT.DeleteSelectedRows()
                                Catch ex As Exception
                                    ShowBaoLoi(ex.Message)
                                End Try
                            End If
                        Next
                        Impersonator.EndImpersonation()
                    End If

                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doDelete("HaiQuan_FileHaiQuan", "idlamhaiquan=@idlamhaiquan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doDelete("HaiQuan_ChiTietLamHaiQuan", "idlamhaiquan=@idlamhaiquan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                        If doDelete("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            loadGVHaiQuan()
                            loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                        End If
                    End If
                End If

            Else
                ShowCanhBao("Đã làm hải quan không xóa được ")
            End If
        End If

    End Sub

    Private Sub btnXoaCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaCT.ItemClick
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không phải người lập")
        Else
            AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
            Dim kt = ExecuteSQLScalar("select idtinhtrang_haiquan from HaiQuan_LamHaiQuan where id=@id")
            If (kt <= 0 Or kt = 4) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                Dim id = gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("id")
                AddParameterWhere("@id", id)
                Dim gop = ExecuteSQLScalar("select GopVTLamHQ from HaiQuan_ChiTietLamHaiQuan where id=@id")
                If IsDBNull(gop) Then
                    If ShowCauHoi("Có muốn xóa không ?") Then

                        AddParameterWhere("@id", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("id"))
                        If doDelete("HaiQuan_ChiTietLamHaiQuan", "id=@id") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            AddParameter("@GopVTLamHQ", DBNull.Value)
                            AddParameterWhere("@idgop", id)
                            If doUpdate("HaiQuan_ChiTietLamHaiQuan", "GopVTLamHQ=@idgop") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        End If
                    End If
                    loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                Else
                    ShowCanhBao("Vật tư này đã gộp ")
                End If
            Else
                ShowCanhBao("Đã làm hải quan không xóa được ")
            End If
        End If

    End Sub

    Private Sub btnNhanXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNhanXuLy.ItemClick
        '  gvDsVatTuHaiQuan.PostEditor()
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        gvDsVatTuHaiQuan.UpdateCurrentRow()
        AddParameter("@tgnhanxuly", Now)
        AddParameter("@idtinhtrang_haiquan", 2)
        AddParameter("@idnguoinhanxuly", CType(TaiKhoan, Int32))
        AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ' Dim row = gvDsVatTuHaiQuan.FocusedRowHandle
            loadGVHaiQuan()
            '    gvDsVatTuHaiQuan.FocusedRowHandle = row
        End If

    End Sub


    Private Sub btnCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhat.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        gvDsVatTuHaiQuan.PostEditor()
        gvDsVatTuHaiQuan.UpdateCurrentRow()
        For i As Integer = 0 To gvDsVatTuHaiQuan.RowCount - 1
            If Not IsDBNull(gvDsVatTuHaiQuan.GetRowCellValue(i, "tgnhanxuly")) Then
                AddParameter("@phanhoi", gvDsVatTuHaiQuan.GetRowCellValue(i, "phanhoi"))
                AddParameter("@sotk", gvDsVatTuHaiQuan.GetRowCellValue(i, "sotk"))
                AddParameter("@luong", gvDsVatTuHaiQuan.GetRowCellValue(i, "luong"))
                AddParameter("@chiphi", gvDsVatTuHaiQuan.GetRowCellValue(i, "chiphi"))
                AddParameter("@giaithichchiphi", gvDsVatTuHaiQuan.GetRowCellValue(i, "giaithichchiphi"))
                AddParameter("@tygiahaiquan", gvDsVatTuHaiQuan.GetRowCellValue(i, "tygiahaiquan"))
                AddParameter("@donvitien", gvDsVatTuHaiQuan.GetRowCellValue(i, "donvitien"))
                AddParameter("@idtinhtrang_haiquan", gvDsVatTuHaiQuan.GetRowCellValue(i, "idtinhtrang_haiquan"))
                '   If gvDsVatTuHaiQuan.GetRowCellValue(i, "idtinhtrang_haiquan") = 3 And IsDBNull(gvDsVatTuHaiQuan.GetRowCellValue(i, "ngayht")) Then
                If gvDsVatTuHaiQuan.GetRowCellValue(i, "idtinhtrang_haiquan") = 3 And (IsDBNull(gvDsVatTuHaiQuan.GetRowCellValue(i, "ngayht")) Or gvDsVatTuHaiQuan.GetRowCellValue(i, "ngayht") Is Nothing) Then
                    AddParameter("@ngayht", Now)

                End If
                If gvDsVatTuHaiQuan.GetRowCellValue(i, "idtinhtrang_haiquan") = 1 Then
                    AddParameter("@idnguoinhanxuly", DBNull.Value)
                    AddParameter("@tgnhanxuly", DBNull.Value)
                    AddParameter("@ngayht", DBNull.Value)
                End If
                If gvDsVatTuHaiQuan.GetRowCellValue(i, "idtinhtrang_haiquan") = 2 Then
                    AddParameter("@ngayht", DBNull.Value)
                End If
                AddParameterWhere("@id", gvDsVatTuHaiQuan.GetRowCellValue(i, "id"))

                If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    '  ShowAlert("Thành công !")
                End If

            End If

        Next
        'Cong no
        'If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 3 And (IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")) And gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht") Is Nothing) Then
        '    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        '    Dim tbSoPhieuCG = ExecuteSQLDataTable("select SoPhieu ,IDVatTuHQ  from HaiQuan_ChiTietLamHaiQuan inner join CHAOGIA  ON idchaogia   =CHAOGIA .id  where idlamhaiquan=@idlamhaiquan")
        '    For j = 0 To tbSoPhieuCG.Rows.Count - 1
        '        AddParameterWhere("@SophieuCG", tbSoPhieuCG.Rows(j).Item("SoPhieu"))
        '        AddParameterWhere("@IDvattu", tbSoPhieuCG.Rows(j).Item("IDVatTuHQ"))
        '        Dim tbSoPhieuXK = ExecuteSQLDataTable("select PHIEUXUATKHO .Sophieu from PHIEUXUATKHO inner join XUATKHO on PHIEUXUATKHO .Sophieu =XUATKHO .Sophieu where SophieuCG =@SophieuCG and IDvattu =@IDvattu")
        '        For k = 0 To tbSoPhieuXK.Rows.Count - 1
        '            TAI.TinhNgayCongNo(tbSoPhieuCG.Rows(j).Item("Sophieu"), tbSoPhieuXK.Rows(k).Item("SoPhieu"), 3, Now)
        '        Next
        '    Next
        'End If
        'Cong no
        Dim row = gvDsVatTuHaiQuan.FocusedRowHandle
        loadGVHaiQuan()
        gvDsVatTuHaiQuan.FocusedRowHandle = row

    End Sub
    Dim hientai As DateTime = GetServerTime()
    Private Sub gvDsVatTuHaiQuan_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gvDsVatTuHaiQuan.RowCellStyle
        Dim view As GridView = sender
        If Not IsDBNull(view.GetRowCellValue(e.RowHandle, view.Columns("tgnhanxuly"))) Then

            Dim ngayht As DateTime = hientai
            Dim ngaythongquan As DateTime = hientai
            If Not IsDBNull(view.GetRowCellValue(e.RowHandle, view.Columns("ngayht"))) Then
                ngayht = view.GetRowCellValue(e.RowHandle, view.Columns("ngayht"))
            End If
            If Not IsDBNull(view.GetRowCellValue(e.RowHandle, view.Columns("NgayThongQuan"))) Then
                ngaythongquan = view.GetRowCellValue(e.RowHandle, view.Columns("NgayThongQuan"))
            End If
            Dim dau As String = view.GetRowCellDisplayText(e.RowHandle, view.Columns("dau"))

            Dim _tgnhanxuly As DateTime = view.GetRowCellValue(e.RowHandle, view.Columns("tgnhanxuly"))
            If e.Column.FieldName = "idtinhtrang_haiquan" Or e.Column.FieldName = "ngayht" Then
                Select Case dau
                    Case "1"
                        If (ngayht - _tgnhanxuly).TotalHours < 72 Then
                            e.Appearance.BackColor = Color.Green
                        End If
                        If (ngayht - _tgnhanxuly).TotalHours = 72 Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                        If (ngayht - _tgnhanxuly).TotalHours > 72 Then
                            e.Appearance.BackColor = Color.Red
                        End If
                    Case "2"
                        If (ngayht - _tgnhanxuly).TotalHours < 120 Then
                            e.Appearance.BackColor = Color.Green
                        End If
                        If (ngayht - _tgnhanxuly).TotalHours = 120 Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                        If (ngayht - _tgnhanxuly).TotalHours > 120 Then
                            e.Appearance.BackColor = Color.Red
                        End If
                    Case Else
                        e.Appearance.ForeColor = Color.Black
                End Select
            End If
            If e.Column.FieldName = "NgayThongQuan" Then
                Select Case dau
                    Case "1"
                        If (ngaythongquan - _tgnhanxuly).TotalHours < 72 Then
                            e.Appearance.BackColor = Color.Green
                        End If
                        If (ngaythongquan - _tgnhanxuly).TotalHours = 72 Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                        If (ngaythongquan - _tgnhanxuly).TotalHours > 72 Then
                            e.Appearance.BackColor = Color.Red
                        End If
                    Case "2"
                        If (ngaythongquan - _tgnhanxuly).TotalHours < 120 Then
                            e.Appearance.BackColor = Color.Green
                        End If
                        If (ngaythongquan - _tgnhanxuly).TotalHours = 120 Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                        If (ngaythongquan - _tgnhanxuly).TotalHours > 120 Then
                            e.Appearance.BackColor = Color.Red
                        End If
                    Case Else
                        e.Appearance.ForeColor = Color.Black
                End Select
            End If
        End If
    End Sub

    Private Sub barCiLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gvDsVatTuHaiQuan.OptionsView.ShowAutoFilterRow = True
        Else
            gvDsVatTuHaiQuan.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    Private Sub frmDSYCLamHQ_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If _message <> 0 Then
            _message = 0
            If isHienThiT = True Then

                AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                Dim id = gvDsVatTuHaiQuan.GetFocusedRowCellValue("id")
                Dim kt = ExecuteSQLDataTable("select idtinhtrang_haiquan from HaiQuan_LamHaiQuan inner join HaiQuan_ChiTietLamHaiQuan on idlamhaiquan=HaiQuan_LamHaiQuan.id where idtinhtrang_haiquan=0 and idlamhaiquan=@idlamhaiquan")
                If kt.Rows.Count > 0 Then
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    Dim listFile = ExecuteSQLScalar("select filehaiquan from HaiQuan_FileHaiQuan where idlamhaiquan=@idlamhaiquan")
                    If listFile IsNot Nothing Then
                        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
                        For Each _url In listUrl
                            If _url <> "" Then
                                Try
                                    Dim str = RootUrlOld & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).Year.ToString & "\" & UrlHaiQuan & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "\" & _url
                                    System.IO.File.Delete(str)
                                    gdvListFileCT.DeleteSelectedRows()
                                Catch ex As Exception
                                    ShowBaoLoi(ex.Message)
                                End Try
                            End If
                        Next
                    End If
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doDelete("HaiQuan_FileHaiQuan", "idlamhaiquan=@idlamhaiquan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    If doDelete("HaiQuan_ChiTietLamHaiQuan", "idlamhaiquan=@idlamhaiquan ") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                        If doDelete("HaiQuan_LamHaiQuan", "id=@id and idtinhtrang_haiquan=0") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        End If
                    End If
                End If
                For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                    If deskTop.tabMain.TabPages(i).Tag = "mVatTuDaChaoGia" Then
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmVatTuDaChaoGia).btnBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmVatTuDaChaoGia).barBtnLamHQ.Enabled = True
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmVatTuDaChaoGia).btTaiLai.PerformClick()

                    End If
                    If deskTop.tabMain.TabPages(i).Tag = "mYeuCauDi_DatHang" Then
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).btnBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).barBtnLamHQ.Enabled = True
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).tabYeuCauDi_DatHang.SelectedTabPage = CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).TabTongHopDH
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).btXem.PerformClick()
                    End If

                Next
                isHienThiT = False
            End If
            Me.Name = "frmDSYCLamHQ"
        End If

    End Sub

    Private Sub btnThemCT_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemCT.ItemClick
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không phải người lập")
        Else
            If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 3 Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                If deskTop.tabMain.SelectedTabPage.Text = "Hàng đã chào giá" Then
                    Me.WindowState = FormWindowState.Minimized
                    For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                        If deskTop.tabMain.TabPages(i).Tag = "mVatTuDaChaoGia" Then
                            CType(deskTop.tabMain.TabPages(i).Controls(0), frmVatTuDaChaoGia).btnBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                        End If
                    Next
                    hienthipopup = False
                End If
                If deskTop.tabMain.SelectedTabPage.Text = "Yêu cầu đi - Đặt hàng" Then
                    Me.WindowState = FormWindowState.Minimized
                    For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                        If deskTop.tabMain.TabPages(i).Tag = "mYeuCauDi_DatHang" Then
                            CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).btnBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                        End If
                    Next
                    hienthipopup = False
                End If
                If deskTop.tabMain.SelectedTabPage.Text = "Danh sách yêu cầu làm hải quan" Then
                    hienthipopup = True

                    If deskTop.mVatTuDaChaoGia.Enabled = True Then
                        Dim frm = New frmVatTuDaChaoGia
                        frm.Message = gvDsVatTuHaiQuan.GetFocusedRowCellValue("id")
                        OpenTab("Hàng đã chào giá", "frmVatTuDaChaoGia", frm, True, Nothing, "mVatTuDaChaoGia")
                        For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                            If deskTop.tabMain.TabPages(i).Tag = "mVatTuDaChaoGia" Then
                                'MsgBox(deskTop.tabMain.TabPages(i).Tag)
                                CType(deskTop.tabMain.TabPages(i).Controls(0), frmVatTuDaChaoGia).btTaiLai.PerformClick()
                            End If
                        Next
                    End If
                    If deskTop.mYeuCauDi_DatHang.Enabled = True Then
                        Dim frm = New frmYeuCauDi_DatHang
                        frm.Message = gvDsVatTuHaiQuan.GetFocusedRowCellValue("id")
                        OpenTab("Yêu cầu đi - Đặt hàng", "frmYeuCauDi_DatHang", frm, True, Nothing, "mYeuCauDi_DatHang")
                        For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                            If deskTop.tabMain.TabPages(i).Tag = "mYeuCauDi_DatHang" Then
                                CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).tabYeuCauDi_DatHang.SelectedTabPage = CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).TabTongHopDH
                                CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauDi_DatHang).btXem.PerformClick()
                            End If
                        Next
                    End If

                End If

                loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
            Else
                ShowCanhBao("Đã hoàn thành làm hải quan không sửa được !")
            End If

        End If

    End Sub

    Private Sub barGlueNguoiLap_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barGlueNguoiLap.EditValueChanged
        ' loadGVHaiQuan()
    End Sub

    Private Sub barLueMaKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueMaKH.EditValueChanged
        ' loadGVHaiQuan()
    End Sub

    Private Sub barLueTinhTrang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueTinhTrang.EditValueChanged
        'loadGVHaiQuan()
    End Sub
    Private Sub riLueTinhTrang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueTinhTrang.ButtonClick
        If e.Button.Index = 1 Then
            barLueTinhTrang.EditValue = Nothing
        End If
    End Sub

    Private Sub riLueMaKh_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueMaKh.ButtonClick
        If e.Button.Index = 1 Then
            barLueMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub riGlueNguoiLap_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riGlueNguoiLap.ButtonClick
        If e.Button.Index = 1 Then
            barGlueNguoiLap.EditValue = Nothing
            riGlueNguoiLap.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        End If
    End Sub

    Private Sub riGlueNguoiLap_Popup(sender As System.Object, e As System.EventArgs) Handles riGlueNguoiLap.Popup
        riGlueNguoiLap.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
    End Sub

    Private Sub barriLueTinhTrang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles barriLueTinhTrang.ButtonClick
        If e.Button.Index = 1 Then
            barLueTinhTrang.EditValue = Nothing
        End If
    End Sub

    Private Sub btnExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExcel.ItemClick
        If gvDsVatTuHaiQuanCT.RowCount > 0 Then
            If Not IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("dau")) Then
                Dim gvexprort As DevExpress.XtraGrid.Views.Grid.GridView = gvDsVatTuHaiQuanCT
                'gvexprort.OptionsView.ShowGroupPanel = True
                With gvexprort
                    .Columns("ttcMa").GroupIndex = 0
                    .OptionsPrint.AutoWidth = False
                    .OptionsView.ShowViewCaption = True
                    .ViewCaption = "Danh sách vật tư làm hải quan <" & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "> <tháng" & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyentuchaogia")).ToString("MM/yyyy") & "> <lần" & gvDsVatTuHaiQuan.GetFocusedRowCellValue("lan") & "> " & "Ngày chuyển: " & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).ToString("dd/MM") & " - Ngày hoàn thành " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("dau") & " đầu: " & If(IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")), "", Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")).ToString("dd/MM/yyyy HH:mm"))
                End With
                Dim saveDialog As SaveFileDialog = New SaveFileDialog() 'tgchuyentuchaogia
                Try
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx"
                    saveDialog.FileName = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "- Lần " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("lan") & " Ngày chuyển " & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).ToString("dd-MM-yyyy")
                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        ShowWaiting("Đang kết xuất ...")
                        Dim exportFilePath As String = saveDialog.FileName
                        Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
                        Dim str As String
                        Dim tuychon As XlsExportOptions = New XlsExportOptions
                        Dim tuychonx As XlsxExportOptions = New XlsxExportOptions

                        tuychon.ShowGridLines() = True
                        tuychonx.ShowGridLines() = True
                        Select Case fileExtenstion
                            Case ".xls"
                                Try
                                    gvexprort.ExportToXls(exportFilePath, tuychon)
                                Catch ex As Exception
                                    ShowBaoLoi(LoiNgoaiLe)
                                End Try

                            Case (".xlsx")
                                Try
                                    gvexprort.ExportToXlsx(exportFilePath, tuychonx)
                                Catch ex As Exception
                                    ShowBaoLoi(LoiNgoaiLe)
                                End Try

                        End Select

                        If ShowCauHoi("Mở file vừa kết xuất ?") Then
                            '                        System.Diagnostics.Process.Start(exportFilePath)
                            If File.Exists(exportFilePath) Then
                                Try
                                    System.Diagnostics.Process.Start(exportFilePath)
                                Catch ex As Exception
                                    str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                                    ShowBaoLoi(str)
                                End Try
                            Else
                                str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                                ShowBaoLoi(str)
                            End If
                        End If
                    End If

                Catch ex As Exception
                    ShowBaoLoi(LoiNgoaiLe)
                End Try
                gvexprort.Columns("ttcMa").GroupIndex = -1
                gvexprort.OptionsView.ShowViewCaption = False
            Else
                ShowCanhBao("nhập đầu ")
            End If
            CloseWaiting()
        End If

    End Sub
    Private Sub btnExcelLamHQ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnExcelLamHQ.ItemClick
        If gvDsVatTuHaiQuanCT.RowCount > 0 Then
            If Not IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("dau")) Then
                '   Dim vitri As Integer()
                Dim socot As Integer = gvDsVatTuHaiQuanCT.VisibleColumns.Count - 1
                Dim ten(socot) As String

                For i As Integer = 0 To socot
                    '   vitri(i) = gvDsVatTuHaiQuanCT.Columns(i).VisibleIndex
                    ten(i) = gvDsVatTuHaiQuanCT.VisibleColumns(i).Name
                Next
                For i = socot To 0 Step -1
                    gvDsVatTuHaiQuanCT.VisibleColumns(i).Visible = False
                Next
                gColSTT.VisibleIndex = 0
                gColMaHang.VisibleIndex = 1
                gColMoTa.VisibleIndex = 2
                gColMaHS.VisibleIndex = 3
                gColMaNuoc.VisibleIndex = 4
                gColSLHQ.VisibleIndex = 5
                gColDonViTinh.VisibleIndex = 6
                gColDonGia.VisibleIndex = 7
                gColThanhTien.VisibleIndex = 8
                Dim chieurong(gvDsVatTuHaiQuanCT.VisibleColumns.Count - 1) As Integer
                For i As Integer = 0 To gvDsVatTuHaiQuanCT.VisibleColumns.Count - 1
                    gvDsVatTuHaiQuanCT.VisibleColumns(i).Caption = gvDsVatTuHaiQuanCT.VisibleColumns(i).Tag
                    chieurong(i) = gvDsVatTuHaiQuanCT.VisibleColumns(i).Width
                    Select Case gvDsVatTuHaiQuanCT.VisibleColumns(i).Name
                        Case "gColMoTa"
                            gvDsVatTuHaiQuanCT.VisibleColumns(i).Width = 350
                        Case "gColSTT"
                            gvDsVatTuHaiQuanCT.VisibleColumns(i).Width = 40
                        Case "gColMaHS"
                            gvDsVatTuHaiQuanCT.VisibleColumns(i).Width = 110
                        Case "gColSLHQ"
                            gvDsVatTuHaiQuanCT.VisibleColumns(i).Width = 100
                        Case Else
                            gvDsVatTuHaiQuanCT.VisibleColumns(i).Width = 140
                    End Select


                Next
                Dim gvexprort As DevExpress.XtraGrid.Views.Grid.GridView = gvDsVatTuHaiQuanCT
                'gvexprort.OptionsView.ShowGroupPanel = True
                With gvexprort

                    .OptionsPrint.AutoWidth = False
                    .ViewCaption = "Danh sách vật tư làm hải quan <" & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "> <tháng" & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyentuchaogia")).ToString("MM/yyyy") & "> <lần" & gvDsVatTuHaiQuan.GetFocusedRowCellValue("lan") & "> " & "Ngày chuyển: " & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).ToString("dd/MM") & " - Ngày hoàn thành " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("dau") & " đầu: " & If(IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")), "", Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")).ToString("dd/MM/yyyy HH:mm"))
                End With
                Dim saveDialog As SaveFileDialog = New SaveFileDialog() 'tgchuyentuchaogia
                Try
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx"
                    saveDialog.FileName = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & "- Lần " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("lan") & " Ngày chuyển " & Convert.ToDateTime(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgchuyensanghaiquan")).ToString("dd-MM-yyyy")
                    If saveDialog.ShowDialog() = DialogResult.OK Then
                        ShowWaiting("Đang kết xuất ...")
                        Dim exportFilePath As String = saveDialog.FileName
                        Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
                        Dim str As String
                        Dim tuychon As XlsExportOptions = New XlsExportOptions
                        Dim tuychonx As XlsxExportOptions = New XlsxExportOptions
                        tuychon.ShowGridLines() = True
                        tuychonx.ShowGridLines() = True
                        Select Case fileExtenstion
                            Case ".xls"
                                Try
                                    gvexprort.ExportToXls(exportFilePath, tuychon)
                                Catch ex As Exception
                                    ShowBaoLoi(LoiNgoaiLe)
                                End Try

                            Case (".xlsx")
                                Try
                                    gvexprort.ExportToXlsx(exportFilePath, tuychonx)
                                Catch ex As Exception
                                    ShowBaoLoi(LoiNgoaiLe)
                                End Try

                        End Select

                        If ShowCauHoi("Mở file vừa kết xuất ?") Then
                            '                        System.Diagnostics.Process.Start(exportFilePath)
                            If File.Exists(exportFilePath) Then
                                Try
                                    System.Diagnostics.Process.Start(exportFilePath)
                                Catch ex As Exception
                                    str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                                    ShowBaoLoi(str)
                                End Try
                            Else
                                str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                                ShowBaoLoi(str)
                            End If
                        End If
                    End If

                Catch ex As Exception
                    ShowBaoLoi(LoiNgoaiLe)
                End Try
                gvexprort.Columns("ttcMa").GroupIndex = -1
                gvexprort.OptionsView.ShowViewCaption = False
                For i As Integer = 0 To gvDsVatTuHaiQuanCT.VisibleColumns.Count - 1
                    gvDsVatTuHaiQuanCT.VisibleColumns(i).Caption = gvDsVatTuHaiQuanCT.VisibleColumns(i).ToolTip
                    gvDsVatTuHaiQuanCT.VisibleColumns(i).Width = chieurong(i)


                Next
                For i As Integer = 0 To gvDsVatTuHaiQuanCT.Columns.Count - 1
                    For j As Integer = 0 To socot
                        If gvDsVatTuHaiQuanCT.Columns(i).Name = ten(j) Then
                            gvDsVatTuHaiQuanCT.Columns(i).VisibleIndex = j
                        End If
                    Next

                Next
            Else
                ShowCanhBao("nhập đầu ")
            End If
            CloseWaiting()

        End If
    End Sub
    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ThemCT.ItemClick
        btnThemCT.PerformClick()
    End Sub

    Private Sub BarButtonItem1_ItemClick_3(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_XoaCT.ItemClick
        btnXoaCT.PerformClick()
    End Sub

    Private Sub BarButtonItem5_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Me.Close()
    End Sub
    Private Function checkOpenTabs(ByVal name As String) As Boolean
        For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
            If deskTop.tabMain.TabPages(i).Text = name Then
                deskTop.tabMain.SelectedTabPageIndex = i
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub OpenTab(ByVal TieuDe As String, ByVal Tag As String, ByVal frm As Object, ByVal isOnly As Boolean, Optional ByVal img As Image = Nothing, Optional ByVal buttonTag As String = "")
        Dim _strTieuDe As String = TieuDe
        If isOnly Then
            If checkOpenTabs(_strTieuDe) = True Then Exit Sub
        End If
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = _strTieuDe
        t.Tag = buttonTag
        If TypeOf (frm) Is DevExpress.XtraEditors.XtraForm Then
            With CType(frm, DevExpress.XtraEditors.XtraForm)
                .Hide()
                .Tag = Tag
                .Dock = DockStyle.Fill
                .FormBorderStyle = System.Windows.Forms.FormBorderStyle.None

                If Not img Is Nothing Then
                    t.Image = img
                End If
                .TopLevel = False

                t.Controls.Add(frm)

                .Show()
            End With

        Else
            With CType(frm, DevExpress.XtraEditors.XtraUserControl)
                .Hide()
                .Tag = Tag
                .Dock = DockStyle.Fill

                If Not img Is Nothing Then
                    t.Image = img
                End If
                t.Controls.Add(frm)

                .Show()
            End With
        End If

        deskTop.tabMain.TabPages.Add(t)
        On Error Resume Next
        deskTop.tabMain.SelectedTabPageIndex = deskTop.tabMain.TabPages.Count - 1
    End Sub

    Private Sub frmDSYCLamHQ_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape And (deskTop.tabMain.SelectedTabPage.Text = "Hàng đã chào giá" Or deskTop.tabMain.SelectedTabPage.Text = "Yêu cầu đi - Đặt hàng") Then
            Me.Close()
        End If
    End Sub

    Private Sub gvDsVatTuHaiQuan_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles gvDsVatTuHaiQuan.PopupMenuShowing
        e.Allow = False
    End Sub

    'Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
    '    Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    '    HitInfo = gvDsVatTuHaiQuanCT.CalcHitInfo(gcDsVatTuHaiQuanCT.PointToClient(Cursor.Position))

    '    If HitInfo.InColumnPanel Then
    '        e.Cancel = True
    '    End If
    'End Sub

    Private Sub gvDsVatTuHaiQuanCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gvDsVatTuHaiQuanCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gvDsVatTuHaiQuanCT.CalcHitInfo(gcDsVatTuHaiQuanCT.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right And gvDsVatTuHaiQuanCT.RowCount > 0 Then
            pChiTiet.ShowPopup(gcDsVatTuHaiQuanCT.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub riPCTTDongGoi_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles riPCTTDongGoi.Closed
        barPCTTDongGoi.EditValue = MeTTDongGoi.EditValue
    End Sub
    Private Shared slcu As Integer = 0
    Private Sub gvDsVatTuHaiQuanCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvDsVatTuHaiQuanCT.CellValueChanged
        If e.Column.FieldName = "SoLuongLamHQ" Then
            If IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ")) Then
                If Convert.ToInt32(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ")) > gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongConLai") + slcu Then
                    ShowCanhBao("Số lượng làm hải quan đang lớn hơn số lượng thực")
                    'If Not IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ")) Then
                    '    gvDsVatTuHaiQuanCT.SetFocusedRowCellValue("SoLuongLamHQ", slcu)
                    'End If

                End If
            End If
        End If
    End Sub

    Private Sub gvDsVatTuHaiQuan_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvDsVatTuHaiQuan.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gvDsVatTuHaiQuan.OptionsView.ShowAutoFilterRow = Not gvDsVatTuHaiQuan.OptionsView.ShowAutoFilterRow
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnGhiLai.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnXoa.PerformClick()

        End If
        If e.KeyCode = Keys.F5 Then
            btnTaiLai.PerformClick()
        End If
    End Sub

    Private Sub gvDsVatTuHaiQuanCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvDsVatTuHaiQuanCT.RowCellClick
        ' If e.Column.FieldName = "SoLuongLamHQ" Then
        If Not IsDBNull(gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ")) Then
            slcu = gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("SoLuongLamHQ")
        End If
        If e.Column.FieldName = "GopVTLamHQ" Then
            If gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("idchaogia") <> 0 Then
                If gvDsVatTuHaiQuanCT.GetRowCellValue(e.RowHandle, "GopVTLamHQ") = True Then
                    gvDsVatTuHaiQuanCT.SetRowCellValue(e.RowHandle, "GopVTLamHQ", False)
                Else
                    gvDsVatTuHaiQuanCT.SetRowCellValue(e.RowHandle, "GopVTLamHQ", True)
                End If
            End If

        End If
    End Sub

    Private Sub gvDsVatTuHaiQuanCT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvDsVatTuHaiQuanCT.DoubleClick
        gColSLHQ.OptionsColumn.AllowEdit = True
    End Sub
    Private Sub gvDsVatTuHaiQuanCT_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvDsVatTuHaiQuanCT.HiddenEditor
        gColSLHQ.OptionsColumn.AllowEdit = False
    End Sub

    Private Sub barBtnGopVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnGopVT.ItemClick
        '  Dim tenvtgop As String = ""

        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không phải người lập")
        Else
            If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 3 Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                Dim tenpogop As String = ""
                Dim tendexuatgop As String = ""
                Dim mahanggop As String = ""
                Dim hangsxgop As String = ""
                Dim xuatsugop As String = ""
                Dim slgop As Integer = 0
                Dim dongiagop As Integer = 0
                Dim tendvtgop As String = ""
                Dim idchaogiagop As String = ""
                Dim d = 0
                For i As Integer = 0 To gvDsVatTuHaiQuanCT.RowCount - 1
                    If gvDsVatTuHaiQuanCT.GetRowCellValue(i, "GopVTLamHQ") = True And IsDBNull(gvDsVatTuHaiQuanCT.GetRowCellValue(i, "gop2")) Then
                        d += 1
                        ' tenvtgop &= gvDsVatTuHaiQuanCT.GetRowCellValue(i, "TenVT") & ";"
                        tenpogop &= gvDsVatTuHaiQuanCT.GetRowCellValue(i, "TenTrongPO") & ";"
                        tendexuatgop &= gvDsVatTuHaiQuanCT.GetRowCellValue(i, "tendexuat") & ";"
                        mahanggop &= gvDsVatTuHaiQuanCT.GetRowCellValue(i, "Model") & ";"
                        hangsxgop = gvDsVatTuHaiQuanCT.GetRowCellValue(i, "TenHang")
                        xuatsugop = gvDsVatTuHaiQuanCT.GetRowCellValue(i, "Tennuoc")
                        '  slgop = gvDsVatTuHaiQuanCT.GetRowCellValue(i, "Soluong")
                        tendvtgop = gvDsVatTuHaiQuanCT.GetRowCellValue(i, "Tennuoc")
                        dongiagop += gvDsVatTuHaiQuanCT.GetRowCellValue(i, "DonGia")

                    End If

                Next
                slgop = 1
                If d >= 2 Then
                    AddParameter("@TenTrongPO", tenpogop)
                    AddParameter("@tendexuat", tendexuatgop)
                    AddParameter("@MaHangLamHQ", mahanggop)
                    AddParameter("@HangSXLamHQ", hangsxgop)
                    AddParameter("@XuatSuLamHQ", xuatsugop)
                    AddParameter("@SoLuongLamHQ", slgop)
                    AddParameter("@DonGiaLamHQ", dongiagop)
                    AddParameter("@TenDVTLamHQ", "bộ")
                    AddParameter("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                    AddParameter("@idchaogia", 0)
                    If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    Else
                        For i As Integer = 0 To gvDsVatTuHaiQuanCT.RowCount - 1
                            If gvDsVatTuHaiQuanCT.GetRowCellValue(i, "GopVTLamHQ") = True Then
                                AddParameter("@GopVTLamHQ", ExecuteSQLScalar("select top 1 id from HaiQuan_ChiTietLamHaiQuan order by id desc"))
                                AddParameterWhere("@id", gvDsVatTuHaiQuanCT.GetRowCellValue(i, "id"))
                                If doUpdate("HaiQuan_ChiTietLamHaiQuan", "id=@id") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                End If
                            End If
                        Next

                    End If
                    loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                Else
                    ShowCanhBao("Không đủ số lượng nhập")
                End If
            Else
                ShowCanhBao("Đã hoàn thành làm hải quan không sửa được !")
            End If

        End If
    End Sub

    Private Sub barChkHienVtDaGop_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barChkHienVtDaGop.CheckedChanged
        If barChkHienVtDaGop.Checked = True Then
            barChkHienVtDaGop.Glyph = My.Resources.Checked
        Else
            barChkHienVtDaGop.Glyph = My.Resources.UnCheck
        End If
        loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
    End Sub

    Private Sub gvDsVatTuHaiQuanCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvDsVatTuHaiQuanCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoaCT.PerformClick()
        End If
    End Sub

    Private Sub btnPhanHoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPhanHoi.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        gvDsVatTuHaiQuan.PostEditor()
        gvDsVatTuHaiQuan.UpdateCurrentRow()
        For i As Integer = 0 To gvDsVatTuHaiQuan.RowCount - 1
            AddParameter("@phanhoi", gvDsVatTuHaiQuan.GetRowCellValue(i, "phanhoi"))
            AddParameterWhere("@id", gvDsVatTuHaiQuan.GetRowCellValue(i, "id"))
            If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If

        Next
        loadGVHaiQuan()
    End Sub

    Private Sub mnu_CanXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_CanXuLy.ItemClick
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 4 Or gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 0 Then
            AddParameter("@idtinhtrang_haiquan", 1)
            AddParameter("@tgchuyensanghaiquan", Now)
        End If
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 1 Then
            AddParameter("@idtinhtrang_haiquan", 4)
        End If
        AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        Else
            loadGVHaiQuan()
        End If
    End Sub
    Private Sub gvDsVatTuHaiQuan_MouseDown(sender As Object, e As MouseEventArgs) Handles gvDsVatTuHaiQuan.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gvDsVatTuHaiQuan.CalcHitInfo(gcDsVatTuHaiQuan.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If gvDsVatTuHaiQuan.RowCount > 0 And deskTop.tabMain.SelectedTabPage.Text = "Danh sách yêu cầu làm hải quan" Then
                mnu_CanXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnu_NhanXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnu_HoanThanh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 4 Or gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 0 Then
                    mnu_CanXuLy.Caption = "Chuyển thành cần xử lý"
                    ' pMain.ShowPopup(gcDsVatTuHaiQuan.PointToScreen(e.Location))
                Else
                    If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 1 Then
                        mnu_CanXuLy.Caption = "Chuyển thành chưa chuyển sang"
                        '  pMain.ShowPopup(gcDsVatTuHaiQuan.PointToScreen(e.Location))
                    End If
                End If
            Else
                mnu_CanXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnu_NhanXuLy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnu_HoanThanh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            End If
            pMain.ShowPopup(gcDsVatTuHaiQuan.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub btnTraLaiKD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTraLaiKD.ItemClick

        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") = 1 Then
            AddParameter("@ngayht", DBNull.Value)
            AddParameter("@idtinhtrang_haiquan", 4)
            AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
            If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            Else
                loadGVHaiQuan()
            End If
        Else
            ShowCanhBao("Đã làm HQ không trả lại được !")
        End If

    End Sub

    Private Sub barChkBoSungVTCT_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barChkBoSungVTCT.CheckedChanged
        If gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap") <> CType(TaiKhoan, Int32) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
            ShowCanhBao("Bạn không phải người lập")
        Else
            If gvDsVatTuHaiQuan.GetFocusedRowCellValue("idtinhtrang_haiquan") <> 3 Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.btnDSYCLamHQ.Name, DanhMucQuyen.TPKinhDoanh) Then
                If barChkBoSungVTCT.Checked = True Then
                    gvDsVatTuHaiQuanCT.OptionsView.NewItemRowPosition = NewItemRowPosition.Top
                Else
                    gvDsVatTuHaiQuanCT.OptionsView.NewItemRowPosition = NewItemRowPosition.None
                End If
            End If
        End If
    End Sub

    Private Sub gvSoCG_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvSoCG.RowCellClick
        '    MsgBox(e.CellValue())
        '  gvDsVatTuHaiQuanCT.SetFocusedRowCellValue("idchaogia", e.CellValue())
        AddParameter("@idchaogia", 0)
        AddParameter("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            loadGVHaiQuanCT(gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        End If
    End Sub

    Private Sub barChkBoSungVTCT_ItemPress(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barChkBoSungVTCT.ItemPress
        ToolTipController1.ShowHint("Chỉ dùng để thêm vật tư xuất cho công trình ", "Lưu ý")
    End Sub

    Private Sub mnu_NhanXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_NhanXuLy.ItemClick
        btnNhanXuLy_ItemClick(Nothing, Nothing)
    End Sub

    Private Sub mnu_HoanThanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_HoanThanh.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        gvDsVatTuHaiQuan.PostEditor()
        gvDsVatTuHaiQuan.UpdateCurrentRow()
        If Not IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("tgnhanxuly")) Then
            AddParameter("@phanhoi", gvDsVatTuHaiQuan.GetFocusedRowCellValue("phanhoi"))
            AddParameter("@sotk", gvDsVatTuHaiQuan.GetFocusedRowCellValue("sotk"))
            AddParameter("@luong", gvDsVatTuHaiQuan.GetFocusedRowCellValue("luong"))
            AddParameter("@chiphi", gvDsVatTuHaiQuan.GetFocusedRowCellValue("chiphi"))
            AddParameter("@giaithichchiphi", gvDsVatTuHaiQuan.GetFocusedRowCellValue("giaithichchiphi"))
            AddParameter("@tygiahaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("tygiahaiquan"))
            AddParameter("@donvitien", gvDsVatTuHaiQuan.GetFocusedRowCellValue("donvitien"))
            AddParameter("@idtinhtrang_haiquan", 3)
            '   If gvDsVatTuHaiQuan.GetRowCellValue(i, "idtinhtrang_haiquan") = 3 And IsDBNull(gvDsVatTuHaiQuan.GetRowCellValue(i, "ngayht")) Then
            If (IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")) Or gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht") Is Nothing) Then
                AddParameter("@ngayht", Now)

            End If
            AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
            If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Thành công !")
            End If
        End If
        'Cong no
        'If (IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht")) Or gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayht") Is Nothing) Then
        '    AddParameterWhere("@idlamhaiquan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
        '    Dim tbSoPhieuCG = ExecuteSQLDataTable("select SoPhieu ,IDvattu  from HaiQuan_ChiTietLamHaiQuan inner join CHAOGIA  ON idchaogia   =CHAOGIA .id  where idlamhaiquan=@idlamhaiquan")
        '    For j = 0 To tbSoPhieuCG.Rows.Count - 1
        '        AddParameterWhere("@SophieuCG", tbSoPhieuCG.Rows(j).Item("SoPhieu"))
        '        AddParameterWhere("@IDvattu", tbSoPhieuCG.Rows(j).Item("IDvattu"))
        '        Dim tbSoPhieuXK = ExecuteSQLDataTable("select PHIEUXUATKHO .Sophieu from PHIEUXUATKHO inner join XUATKHO on PHIEUXUATKHO .Sophieu =XUATKHO .Sophieu where SophieuCG =@SophieuCG and IDvattu =@IDvattu")
        '        For k = 0 To tbSoPhieuXK.Rows.Count - 1
        '            TAI.TinhNgayCongNo(tbSoPhieuCG.Rows(j).Item("Sophieu"), tbSoPhieuXK.Rows(k).Item("SoPhieu"), 3, Now)
        '        Next
        '    Next
        'End If
        'Cong no
        Dim row = gvDsVatTuHaiQuan.FocusedRowHandle
        loadGVHaiQuan()
        gvDsVatTuHaiQuan.FocusedRowHandle = row
    End Sub

    Private Sub riLueMaHS_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueMaHS.ButtonClick
        If e.Button.Index = 1 Then
            'AddParameter("MaHS", gvDsVatTuHaiQuanCT.GetFocusedRowCellDisplayText("IdMaHS"))
            ' If do
        End If
    End Sub
    Private Sub mnu_CnPTTT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_CnPTTT.ItemClick
        If ShowCauHoi("Bạn có muốn cập nhật phương thức thanh toán hải quan cho " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa")) Then
            AddParameter("@PhuongThucThanhToanHQ", gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ"))
            AddParameterWhere("@ID", gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDKhachhang"))
            If doUpdate("KHACHHANG", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật")
            End If
        End If


    End Sub
    Private Sub mnu_CnDiaChiLamHQ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_CnDiaChiLamHQ.ItemClick
        If ShowCauHoi("Bạn có muốn cập nhật địa chỉ làm hải quan cho " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa")) Then
            AddParameter("@ttcDiachiHQ", gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcDiachiHQ"))
            AddParameterWhere("@ID", gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDKhachhang"))
            If doUpdate("KHACHHANG", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật")
            End If
        End If


    End Sub
    Private Sub mnu_CapNhatLaiMoTa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_CapNhatLaiMoTa.ItemClick
        If ShowCauHoi("Bạn có muốn cập nhật lại mô tả cho " & gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("TenVT") & " mã " & gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("Model")) Then
            AddParameter("@MoTaHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ"))
            AddParameterWhere("@ID", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IDVatTuHQ"))
            If doUpdate("VATTU", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật")
            End If
        End If
        '    AddParameterWhere("@IdTenvattu", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IdTenvattu"))
        '    AddParameterWhere("@Model", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("Model"))
        '    AddParameter("@MoTa", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MoTaHQ"))
        '    If doUpdate("HaiQuan_MoTa", "IdTenvattu=@IdTenvattu and Model=@Model") Is Nothing Then
        '        ShowBaoLoi(LoiNgoaiLe)
        '    Else
        '        ShowAlert("Đã cập nhật")
        '    End If
        'End If

    End Sub
    Private Sub mnu_CapNhatLaiMaHS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_CapNhatLaiMaHS.ItemClick
        If ShowCauHoi("Bạn có muốn cập nhật lại mã HS cho " & gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("TenVT") & " mã " & gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("Model")) Then

            AddParameter("@MaHSHQ", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ"))
            AddParameterWhere("@ID", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IDVatTuHQ"))
            If doUpdate("VATTU", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật")
            End If
        End If
        'AddParameterWhere("@IdTenvattu", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("IdTenvattu"))
        'AddParameterWhere("@Model", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("Model"))
        'AddParameter("@MaHS", gvDsVatTuHaiQuanCT.GetFocusedRowCellValue("MaHSHQ"))
        'If doUpdate("HaiQuan_MaHS", "IdTenvattu=@IdTenvattu and Model=@Model") Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        'Else
        '    ShowAlert("Đã cập nhật")
        'End If


    End Sub
    Private Function VietHoaChuDau(ByVal str As String) As String
        If String.IsNullOrEmpty(str) Then
            Return str
        End If
        Dim array() As Char = str.ToCharArray
        array(0) = Char.ToUpper(array(0))
        For i As Integer = 1 To str.Length - 1
            array(i) = Char.ToLower(array(i))
        Next
        Return New String(array)
    End Function
    Private focusrow As Integer
    Private Sub mnu_ChinhSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ChinhSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If mnu_ChinhSua.Caption = "Chỉnh sửa" Then
            gColMoTa.OptionsColumn.AllowEdit = True
            gColMaHS.OptionsColumn.AllowEdit = True
            mnu_ChinhSua.Caption = "Hoàn thành chỉnh sửa"
            focusrow = gvDsVatTuHaiQuan.FocusedRowHandle()
        Else
            gColMoTa.OptionsColumn.AllowEdit = False
            gColMaHS.OptionsColumn.AllowEdit = False
            mnu_ChinhSua.Caption = "Chỉnh sửa"
        End If
    End Sub
    Private Function tryObj2Double(obj As Object) As Double
        Try
            If obj Is DBNull.Value Then Return 0
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub btXuat_Click(sender As Object, e As EventArgs) Handles btXuat.Click
        Dim fileKetXuat As String = ""
        Dim str As String = ""
        Dim tb As DataTable = CType(gcDsVatTuHaiQuan.DataSource, DataTable)
        Dim tb2 As DataTable = CType(gcDsVatTuHaiQuanCT.DataSource, DataTable)
        Dim wb As IWorkbook
        Dim ws As IWorksheet
        Dim _cells As IRange
        Dim usEng As CultureInfo = New CultureInfo("en-US")
        Dim englishInfo As DateTimeFormatInfo = usEng.DateTimeFormat
        Dim d As Integer = 0
        For i As Integer = 0 To tb2.Rows.Count - 2
            If Not IsDBNull(tb2.Rows(i)("SoPOHQ")) And Not IsDBNull(tb2.Rows(i + 1)("SoPOHQ")) Then
                If tb2.Rows(i)("SoPOHQ") <> tb2.Rows(i + 1)("SoPOHQ") Then
                    d += 1
                    Exit For
                End If
            End If
          
        Next
        ' áp dụng chung cho mọi khách hàng
        Try

            If cbbLoaiInovice.EditValue = "Chung" Then
                fileKetXuat = Application.StartupPath & "\Excel\HAIQUAN\INV_CHUNG.xls"
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
                ws = wb.Worksheets(0)
                _cells = ws.Cells
                If Not System.IO.File.Exists(fileKetXuat) Then
                    ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                    Exit Sub
                End If

                _cells(9, 0).Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("TenENG")
                _cells(10, 0).Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcDiaChiHQ")
                _cells(12, 0).Value = "Tel: " + gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcDienthoai") + "     Fax: " + gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcFax")
                _cells(4, 6).Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon")

                If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")) Then
                    _cells(5, 6).Value = ""
                Else
                    Dim ngay As DateTime = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")
                    _cells(5, 6).Value = ngay

                    _cells("F9").Value = "On or about " & ngay.ToString("MMM", usEng) & " " & ngay.Year()
                End If
                _cells("F11").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ") 'PhuongThucThanhToanHQ
                If d > 0 Then
                    _cells("G15").Value = "As listed below"
                Else
                    _cells("G15").Value = tb2.Rows(0)("SoPOHQ")
                End If

                Dim index As Integer = 18

                For i As Integer = 0 To tb2.Rows.Count - 1

                    If i < tb2.Rows.Count - 1 Then
                        _cells(index + 1, 0, index + 1, 8).Insert(InsertShiftDirection.Down)
                    End If
                    _cells(index, 0).Value = tb2.Rows(i)("AZ")
                    _cells(index, 1).Value = tb2.Rows(i)("SoPOHQ")
                    _cells(index, 2).Value = tb2.Rows(i)("Model")
                    _cells(index, 3).Value = tb2.Rows(i)("Ten_ENG")
                    _cells(index, 4).Value = tb2.Rows(i)("SoLuongLamHQ")
                    _cells(index, 5).Value = tb2.Rows(i)("TenDVT_ENG")
                    _cells(index, 6).Value = tb2.Rows(i)("DonGia")
                    _cells(index, 7).Value = tb2.Rows(i)("ThanhTien")
                    _cells(index, 8).Value = VietHoaChuDau(tb2.Rows(i)("TenHang")) + "/" + VietHoaChuDau(tb2.Rows(i)("Tennuoc"))
                    index += 1
                    '   _cells(index, 0, index, 8).Insert(InsertShiftDirection.Down)
                Next
                'index += 1
                _cells(index + 1, 3).RowHeight = _cells(index + 1, 3).RowHeight * 2
                _cells(index + 1, 3).WrapText = True
                _cells(index + 1, 3).Font.Bold = True
                _cells(index + 1, 3).Value = "DAP - Total AMOUNT"
                _cells(index + 1, 4).Value = tb2.Compute("SUM(SoLuongLamHQ)", "")
                _cells(index + 1, 6).Value = "VNĐ"
                _cells(index + 1, 7).Value = tb2.Compute("SUM(ThanhTien)", "")
                _cells(index + 6, 5).Value = barSeCanTinh.EditValue.ToString() + " Kgs"
                _cells(index + 7, 5).Value = barSeCanKhoi.EditValue.ToString() + " Kgs"
                _cells(index + 7, 7).Value = barSeSLThung.EditValue.ToString() + " " + barTeLoaiThung.EditValue.ToString()
            End If
            ' áp dụng với khách hàng GE
            If cbbLoaiInovice.EditValue = "GE" Then
                fileKetXuat = Application.StartupPath & "\Excel\HAIQUAN\INV_GE.xls"
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
                ws = wb.Worksheets(0)
                _cells = ws.Cells
                If Not System.IO.File.Exists(fileKetXuat) Then
                    ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                    Exit Sub
                End If

                _cells(10, 9).Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon")

                If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")) Then
                    _cells("J12").Value = ""
                Else
                    Dim ngay As DateTime = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")
                    _cells("J12").Value = ngay


                    _cells("C19").Value = "On or about " & ngay.ToString("MMM", usEng) & " " & ngay.Year()
                End If
                _cells("C20").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ")
                Dim row As DataRowView = TryCast(riGlueNguoiLap.GetRowByKeyValue(gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap")), DataRowView)
                If Not IsDBNull(row("GioiTinh")) Then
                    If row("GioiTinh") Then
                        _cells("B16").Value = "Mr. " + row("Ten")
                    Else
                        _cells("B16").Value = "Ms. " + row("Ten")
                    End If
                Else
                    _cells("B16").Value = row("Ten")
                End If
                _cells("B16").Value += " - Sales department"

                If d > 0 Then
                    _cells("J13").Value = "As listed below"
                Else
                    _cells("J13").Value = tb2.Rows(0)("SoPOHQ")
                End If

                Dim index As Integer = 24

                For i As Integer = 0 To tb2.Rows.Count - 1

                    If i < tb2.Rows.Count - 1 Then
                        _cells(index + 1, 0, index + 1, 10).Insert(InsertShiftDirection.Down)
                    End If

                    If Not IsDBNull(tb2.Rows(i)("ngaypo")) Then
                        Dim ngaypo As DateTime = tb2.Rows(i)("ngaypo")
                        _cells(index, 1).Value = tb2.Rows(i)("SoPOHQ") + vbCrLf + ngaypo.ToString("dd-MMM-yy", usEng)
                    Else
                        _cells(index, 1).Value = tb2.Rows(i)("SoPOHQ")
                    End If

                    _cells(index, 0).RowHeight = 50.2
                    _cells(index, 0).Value = tb2.Rows(i)("AZ")

                    _cells(index, 2).Value = tb2.Rows(i)("ThongTinDacBiet")
                    _cells(index, 3).Value = tb2.Rows(i)("Model")
                    _cells(index, 4).Value = tb2.Rows(i)("tendexuat")
                    _cells(index, 5).Value = tb2.Rows(i)("SoLuongLamHQ")
                    _cells(index, 6).Value = tb2.Rows(i)("TenDVT_ENG")
                    _cells(index, 7).Value = tb2.Rows(i)("DonGia")
                    _cells(index, 8).Value = tb2.Rows(i)("ThanhTien")
                    _cells(index, 9).Value = VietHoaChuDau(tb2.Rows(i)("TenHang")) + "/" + VietHoaChuDau(tb2.Rows(i)("Tennuoc"))
                    _cells(index, 10).Value = "Mới 100%"
                    index += 1

                Next
                '  index += 1
                _cells(index + 2, 2).Value = barSeCanTinh.EditValue.ToString() + " Kgs"
                _cells(index + 3, 2).Value = barSeCanKhoi.EditValue.ToString() + " Kgs"
                _cells(index + 3, 4).Value = barSeSLThung.EditValue.ToString() + " " + barTeLoaiThung.EditValue.ToString()
            End If

            'áp dụng với khách hàng KEFICO
            If cbbLoaiInovice.EditValue = "KEFICO" Then
                fileKetXuat = Application.StartupPath & "\Excel\HAIQUAN\INV_KEFICO.xls"
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
                ws = wb.Worksheets(0)
                _cells = ws.Cells
                If Not System.IO.File.Exists(fileKetXuat) Then
                    ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                    Exit Sub
                End If

                _cells("G12").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon")

                If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")) Then
                    _cells("G13").Value = ""
                Else
                    Dim ngay As DateTime = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")
                    _cells("G13").Value = ngay
                    _cells("C19").Value = "On or about " & ngay.ToString("MMM", usEng) & " " & ngay.Year()
                End If
                Dim row As DataRowView = TryCast(riGlueNguoiLap.GetRowByKeyValue(gvDsVatTuHaiQuan.GetFocusedRowCellValue("IDnguoilap")), DataRowView)
                If Not IsDBNull(row("GioiTinh")) Then
                    If row("GioiTinh") Then
                        _cells("B16").Value = "Mr. " + row("Ten")
                    Else
                        _cells("B16").Value = "Ms. " + row("Ten")
                    End If
                Else
                    _cells("B16").Value = row("Ten")
                End If
                _cells("B16").Value += " - Sales department"
                _cells("C20").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ")
                If d > 0 Then
                    _cells("G14").Value = "As listed below"
                    _cells("G14").Value = "As listed below"
                Else
                    _cells("G14").Value = tb2.Rows(0)("SoPOHQ")
                    _cells("G15").Value = tb2.Rows(0)("ngaypo")
                End If
                Dim index As Integer = 25

                For i As Integer = 0 To tb2.Rows.Count - 1

                    If i < tb2.Rows.Count - 1 Then
                        _cells(index + 1, 0, index + 1, 7).Insert(InsertShiftDirection.Down)
                    End If
                    _cells(index, 0).RowHeight = 48
                    _cells(index, 0).Value = tb2.Rows(i)("AZ")

                    _cells(index, 1).Value = tb2.Rows(i)("Ten_ENG") + vbCrLf + "(" + tb2.Rows(i)("tendexuat") + ")" + vbCrLf + "Model: " + tb2.Rows(i)("Model")
                    _cells(index, 2).Value = tb2.Rows(i)("SoLuongLamHQ")
                    _cells(index, 3).Value = tb2.Rows(i)("TenDVT_ENG")
                    _cells(index, 4).Value = tb2.Rows(i)("DonGia")
                    _cells(index, 5).Value = tb2.Rows(i)("ThanhTien")
                    _cells(index, 6).Value = VietHoaChuDau(tb2.Rows(i)("TenHang")) + "/" + VietHoaChuDau(tb2.Rows(i)("Tennuoc"))
                    _cells(index, 7).Value = "Mới 100%"
                    index += 1

                Next
                '  index += 1
                _cells(index + 2, 2).Value = barSeCanTinh.EditValue.ToString() + " Kgs"
                _cells(index + 3, 2).Value = barSeCanKhoi.EditValue.ToString() + " Kgs"
                _cells(index + 3, 4).Value = barSeSLThung.EditValue.ToString() + " " + barTeLoaiThung.EditValue.ToString()
            End If
            'áp dụng với khách hàng SUMIDENSO
            If cbbLoaiInovice.EditValue = "SUMIDENSO" Then
                fileKetXuat = Application.StartupPath & "\Excel\HAIQUAN\INV_SUMIDENSO.xls"
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
                ws = wb.Worksheets(0)
                _cells = ws.Cells
                If Not System.IO.File.Exists(fileKetXuat) Then
                    ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                    Exit Sub
                End If

                _cells("D6").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon") & vbCrLf

                If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")) Then
                    _cells("D6").Value &= ""
                Else
                    Dim ngay As DateTime = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")
                    _cells("D6").Value &= ngay.ToString("dd/MM/yyyy")
                    _cells("D25").Value = MonthName(ngay.Month) & ngay.Year()
                End If
                _cells("E23").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ")

                Dim index As Integer = 28
                Dim sopotong As String = ""

                For i As Integer = 0 To tb2.Rows.Count - 1

                    If i < tb2.Rows.Count - 1 Then
                        _cells(index + 1, 0, index + 1, 9).Insert(InsertShiftDirection.Down)
                    End If
                    _cells(index, 0).RowHeight = 34.5
                    _cells(index, 0).Value = tb2.Rows(i)("AZ")

                    _cells(index, 1).Value = tb2.Rows(i)("tendexuat")

                    _cells(index, 2).Value = tb2.Rows(i)("TenDVT")
                    _cells(index, 3).Value = tb2.Rows(i)("SoLuongLamHQ")
                    _cells(index, 4).Value = tb2.Rows(i)("DonGia")
                    _cells(index, 5).Value = tb2.Rows(i)("ThanhTien")
                    _cells(index, 9).Value = VietHoaChuDau(tb2.Rows(i)("TenHang")) + "/" + VietHoaChuDau(tb2.Rows(i)("Tennuoc"))
                    Dim dem As Integer = 0
                    If Not IsDBNull(tb2.Rows(i)("SoPOHQ").ToString()) Then

                        For j As Integer = i - 1 To 0 Step -1
                            If tb2.Rows(i)("SoPOHQ").ToString() = tb2.Rows(j)("SoPOHQ").ToString() Then
                                dem += 1
                                Exit For
                            End If
                        Next
                    End If
                    If dem = 0 Then
                        sopotong &= tb2.Rows(i)("SoPOHQ").ToString() & ","
                     
                    End If

                  
                    index += 1
                Next
                sopotong = sopotong.Trim().Remove(sopotong.Length - 1)
                _cells("C8").Value = sopotong
                '  index += 1
                _cells(index + 3, 1).Value = "Tổng số kiện : " & barSeSLThung.EditValue.ToString() & " kiện"
                _cells(index + 4, 1).Value = "Trọng lượng : " & barSeCanKhoi.EditValue.ToString() & " Kgs"
                _cells(index + 5, 1).Value = "Trọng lượng tịnh :" & barSeCanTinh.EditValue.ToString() & " Kgs"
            End If
            'áp dụng với khách hàng CHILISIN
            If cbbLoaiInovice.EditValue = "CHILISIN" Then
                fileKetXuat = Application.StartupPath & "\Excel\HAIQUAN\INV_CHILISIN.xls"
                wb = Factory.GetWorkbookSet().Workbooks.Open(fileKetXuat)
                ws = wb.Worksheets(0)

                _cells = ws.Cells
                If Not System.IO.File.Exists(fileKetXuat) Then
                    ShowBaoLoi("Không tìm thấy file chào giá mẫu : " & fileKetXuat)
                    Exit Sub
                End If

                _cells("H10").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon")

                If IsDBNull(gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")) Then
                    _cells("H11").Value &= ""
                Else
                    Dim ngay As DateTime = gvDsVatTuHaiQuan.GetFocusedRowCellValue("ngayhoadon")
                    _cells("H11").Value &= ngay.ToString("dd/MM/yyyy")
                    _cells("C17").Value = "On or about " & ngay.ToString("MMM", usEng) & " " & ngay.Year()
                End If
                _cells("C18").Value = gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ")
                If d > 0 Then
                    _cells("H12").Value = "As listed below"
                    _cells("H13").Value = "As listed below"
                Else
                    _cells("H12").Value = tb2.Rows(0)("SoPOHQ")
                    _cells("H13").Value = tb2.Rows(0)("ngaypo")
                End If

                Dim index As Integer = 23

                For i As Integer = 0 To tb2.Rows.Count - 1

                    If i < tb2.Rows.Count - 1 Then
                        _cells(index + 1, 0, index + 1, 8).Insert(InsertShiftDirection.Down)
                    End If
                    _cells(index, 0).RowHeight = 48
                    _cells(index, 0).Value = tb2.Rows(i)("AZ")

                    _cells(index, 1).Value = tb2.Rows(i)("SoPOHQ")
                    _cells(index, 2).Value = tb2.Rows(i)("Model") + vbCrLf + tb2.Rows(i)("tendexuat") + "/" + tb2.Rows(i)("Ten_ENG")
                    _cells(index, 3).Value = tb2.Rows(i)("SoLuongLamHQ")
                    _cells(index, 3).Value = tb2.Rows(i)("SoLuongLamHQ")
                    _cells(index, 4).Value = tb2.Rows(i)("TenDVT")
                    _cells(index, 5).Value = tb2.Rows(i)("DonGia")
                    _cells(index, 6).Value = tb2.Rows(i)("ThanhTien")
                    _cells(index, 7).Value = VietHoaChuDau(tb2.Rows(i)("TenHang")) + "/" + VietHoaChuDau(tb2.Rows(i)("Tennuoc"))
                    _cells(index, 8).Value = "Mới 100%"
                    index += 1
                Next
                '  index += 1
                _cells(index + 2, 2).Value = barSeCanTinh.EditValue.ToString() + " Kgs"
                _cells(index + 3, 2).Value = barSeCanKhoi.EditValue.ToString() + " Kgs"
                _cells(index + 3, 4).Value = barSeSLThung.EditValue.ToString() + " " + barTeLoaiThung.EditValue.ToString()
            End If
            Dim saveDialog As SaveFileDialog = New SaveFileDialog()
            Try
                saveDialog.Filter = "Excel(.xls)|*.xls"
                saveDialog.FileName = "CI&PL " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcMa") & " " & gvDsVatTuHaiQuan.GetFocusedRowCellValue("sohoadon")
                If saveDialog.ShowDialog() = DialogResult.OK Then
                    ShowWaiting("Đang kết xuất ...")
                    Dim exportFilePath As String = saveDialog.FileName
                    wb.SaveAs(exportFilePath, FileFormat.Excel8)

                    If ShowCauHoi("Mở file vừa kết xuất ?") Then
                        If File.Exists(exportFilePath) Then
                            Try
                                System.Diagnostics.Process.Start(exportFilePath)
                            Catch ex As Exception
                                str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                                ShowBaoLoi(str)
                            End Try
                        Else
                            str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                            ShowBaoLoi(str)
                        End If
                    End If
                End If


            Catch ex As Exception
                ShowBaoLoi(LoiNgoaiLe)
                CloseWaiting()
            End Try

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
        CloseWaiting()
    End Sub

    Private Sub gvDsVatTuHaiQuan_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvDsVatTuHaiQuan.RowUpdated
        If deskTop.tabMain.SelectedTabPage.Text = "Làm hải quan" Then
            If ShowCauHoi("Bạn có muốn lưu lại không ") Then
                gvDsVatTuHaiQuan.CloseEditor()
                gvDsVatTuHaiQuan.UpdateCurrentRow()
                AddParameter("@PhuongThucThanhToan_HQ", gvDsVatTuHaiQuan.GetFocusedRowCellValue("PhuongThucThanhToanHQ"))
                AddParameter("@ttcDiachi_HQ", gvDsVatTuHaiQuan.GetFocusedRowCellValue("ttcDiachiHQ"))
                AddParameter("@NgayThongQuan", gvDsVatTuHaiQuan.GetFocusedRowCellValue("NgayThongQuan"))
                AddParameterWhere("@id", gvDsVatTuHaiQuan.GetFocusedRowCellValue("id"))
                If doUpdate("HaiQuan_LamHaiQuan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                Else
                    Dim row = gvDsVatTuHaiQuan.FocusedRowHandle
                    loadGVHaiQuan()
                    gvDsVatTuHaiQuan.FocusedRowHandle = row
                End If
            Else
                Dim row = gvDsVatTuHaiQuan.FocusedRowHandle
                loadGVHaiQuan()
                gvDsVatTuHaiQuan.FocusedRowHandle = row
            End If
        End If
       

    End Sub

    Private Sub riDeNgayThongQuan_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riDeNgayThongQuan.ButtonClick
        If e.Button.Index = 1 Then

            gvDsVatTuHaiQuan.SetFocusedRowCellValue("NgayThongQuan", DBNull.Value)
        End If
    End Sub
End Class