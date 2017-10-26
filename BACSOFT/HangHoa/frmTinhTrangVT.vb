Imports BACSOFT.Db.SqlHelper

Public Class frmTinhTrangVT

    Public _IDVatTu As Int32 = 0
    Public _Count1 As Integer = 0
    Public _Count2 As Integer = 0
    Public _Count3 As Integer = 0

    Public _HienThongTinNX As Boolean = True
    Public _HienNCC As Boolean = False
    Public _HienCGXK As Boolean = True

    Private Sub frmTinhTrangVT_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not _HienNCC Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
                colNhaCC.Visible = False
                colNCCDH.Visible = False
            Else
                _HienNCC = True
            End If
        End If

        If _IDVatTu <> 0 Then
            Dim sql As String = ""
            sql &= " SELECT Model,Code,"
            sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS TonBAC,"
            sql &= " TonNCC,(SELECT ISNULL(SUM(SoLuong),0) FROM NHAPKHO WHERE IDVatTu=VATTU.ID)TongNhap,(SELECT ISNULL(SUM(SoLuong),0) FROM XUATKHO WHERE IDVatTu=VATTU.ID)TongXuat,"
            sql &= " ((CASE ConSX WHEN 1 THEN N'Còn SX, ' ELSE N'Ngừng SX, ' END) +(CASE MaLoi WHEN 1 THEN N'Mã lỗi, ' ELSE N'' END)+(CASE ThongDung WHEN 1 THEN N'Thông dụng, ' ELSE N'' END)+(CASE HangTon WHEN 1 THEN N'Khó bán,' ELSE N'' END))TinhTrang,"
            sql &= " SLMOQ1,GiaMOQ1,SLMOQ2,GiaMOQ2,SLMOQ3,GiaMOQ3,ISNULL(GiaNKBAC,0)GiaNKBAC,HinhAnh,TaiLieu,ThongSo,TENNHOM.Ten_ENG AS TenNhom_ENG, TENHANGSANXUAT.Ten AS TenHang,"
            'sql &= " (CASE TienTe1 WHEN 0 Then DonGia1 ELSE DonGia1 * (SELECT TyGia FROM tblTienTe WHERE ID=VATTU.TienTe1) END)GiaList,"
            'sql &= " (CASE TienTe1 WHEN 0 Then DonGia1*GiaBan1/100 ELSE DonGia1 * (SELECT TyGia FROM tblTienTe WHERE ID=VATTU.TienTe1) * GiaBan1/100 END)GiaBL,"
            'sql &= " (CASE TienTe1 WHEN 0 Then DonGia1*GiaNCC1/100 ELSE DonGia1 * (SELECT TyGia FROM tblTienTe WHERE ID=VATTU.TienTe1)* GiaNCC1/100 END)GiaBB"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBL,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBB"
            sql &= " ,GhiChu"
            sql &= " FROM VATTU "
            sql &= " LEFT JOIN TENNHOM ON TENNHOM.ID= VATTU.IDTennhom"
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangsanxuat "
            sql &= " WHERE VATTU.ID=" & _IDVatTu

            sql &= " SELECT TOP 5 NHAPKHO.Sophieu,IDVattu,Soluong,(Dongia * PHIEUNHAPKHO.Tygia)Dongia,NHAPKHO.ChiPhi,NHAPKHO.Tiente,Mucthue,Nhapthue,PHIEUNHAPKHO.Ngaythang,KHACHHANG.ttcMa,"
            sql &= " (CASE VATTU.DonGia1 WHEN 0 THEN 0 ELSE "
            sql &= " (CASE VATTU.TienTe1 WHEN 0 THEN round(((Dongia * PHIEUNHAPKHO.Tygia)/VATTU.DonGia1)*100,2) ELSE round(((Dongia * PHIEUNHAPKHO.Tygia)/(VATTU.DonGia1 * tblTienTe.TyGia))*100,2) END)END )GiaNhapPT"
            sql &= " FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON NHAPKHO.Sophieu=PHIEUNHAPKHO.Sophieu"
            sql &= "         INNER JOIN VATTU ON NHAPKHO.IDVatTu= VATTU.ID "
            sql &= "         INNER JOIN tblTienTe ON tblTienTe.ID= VATTU.TienTe1 "
            sql &= "         INNER JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachhang=KHACHHANG.ID"
            sql &= " WHERE IDVattu=" & _IDVatTu
            sql &= " ORDER BY PHIEUNHAPKHO.Ngaythang DESC"

            sql &= " SELECT XUATKHO.Sophieu,IDVattu,Soluong,(Dongia * PHIEUXUATKHO.Tygia)Dongia,PHIEUXUATKHO.Tiente,Mucthue,Xuatthue,PHIEUXUATKHO.Ngaythang,KHACHHANG.ttcMa,PHIEUXUATKHO.CongTrinh,"
            sql &= " (CASE VATTU.DonGia1 WHEN 0 THEN 0 ELSE "
            sql &= " (CASE VATTU.TienTe1 WHEN 0 THEN round(((Dongia * PHIEUXUATKHO.Tygia)/VATTU.DonGia1)*100,2) ELSE round(((Dongia * PHIEUXUATKHO.Tygia)/(VATTU.DonGia1 * tblTienTe.TyGia))*100,2) END )END)GiaBanPT"
            sql &= " FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu"
            sql &= "         INNER JOIN VATTU ON XUATKHO.IDVatTu= VATTU.ID "
            sql &= "         INNER JOIN tblTienTe ON tblTienTe.ID= VATTU.TienTe1 "
            sql &= "         INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachhang=KHACHHANG.ID"
            'sql &= "        LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=XUATKHO.SoPhieu "
            sql &= " WHERE IDVattu=" & _IDVatTu
            sql &= " ORDER BY PHIEUXUATKHO.Ngaythang DESC"

            sql &= " SELECT CHAOGIA.Sophieu,IDVattu,Soluong,(Dongia * BANGCHAOGIA.Tygia)Dongia,BANGCHAOGIA.Tiente,Mucthue,Xuatthue,"
            sql &= " BANGCHAOGIA.Ngaythang,KHACHHANG.ttcMa,BANGCHAOGIA.CongTrinh,"
            sql &= " (CASE VATTU.DonGia1 WHEN 0 THEN 0 ELSE "
            sql &= " (CASE VATTU.TienTe1 WHEN 0 THEN round(((Dongia * BANGCHAOGIA.Tygia)/VATTU.DonGia1)*100,2) "
            sql &= " 	ELSE round(((Dongia * BANGCHAOGIA.Tygia)/(VATTU.DonGia1 * tblTienTe.TyGia))*100,2) END )END)GiaBanPT,"
            sql &= " (CASE CHAOGIA.CanXuat WHEN 0 THEN Convert(bit,0) ELSE Convert(bit,1) END)CX"
            sql &= " FROM CHAOGIA INNER JOIN BANGCHAOGIA ON CHAOGIA.Sophieu=BANGCHAOGIA.Sophieu"
            sql &= "         INNER JOIN VATTU ON CHAOGIA.IDVatTu= VATTU.ID "
            sql &= "         INNER JOIN tblTienTe ON tblTienTe.ID= VATTU.TienTe1 "
            sql &= "         INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
            sql &= " WHERE (CHAOGIA.TrangThai <>2 OR CHAOGIA.CanXuat <> 0) AND CHAOGIA.IDVattu=" & _IDVatTu
            sql &= " ORDER BY BANGCHAOGIA.Ngaythang DESC"



            sql &= " SELECT ISNULL( (SELECT SUM(SoLuong) FROM tblXuatMuon WHERE TrangThai <>1 AND IDVatTu=" & _IDVatTu & "),0)"

            sql &= " SELECT DATHANG.Sophieu,IDVattu,CanNhap as SoLuong,(Dongia * PHIEUDATHANG.Tygia)Dongia,Mucthue,Nhapthue,DATHANG.NgayVe,DATHANG.NgayVe2,KHACHHANG.ttcMa,"
            sql &= " (CASE VATTU.DonGia1 WHEN 0 THEN 0 ELSE "
            sql &= " (CASE VATTU.TienTe1 WHEN 0 THEN round(((Dongia * PHIEUDATHANG.Tygia)/VATTU.DonGia1)*100,2) ELSE round(((Dongia * PHIEUDATHANG.Tygia)/(VATTU.DonGia1 * tblTienTe.TyGia))*100,2) END)END )GiaNhapPT"
            sql &= " FROM DATHANG INNER JOIN PHIEUDATHANG ON DATHANG.Sophieu=PHIEUDATHANG.Sophieu"
            sql &= "         INNER JOIN VATTU ON DATHANG.IDVatTu= VATTU.ID "
            sql &= "         INNER JOIN tblTienTe ON tblTienTe.ID= VATTU.TienTe1 "
            sql &= "         INNER JOIN KHACHHANG ON PHIEUDATHANG.IDKhachhang=KHACHHANG.ID"
            sql &= " WHERE DATHANG.CanNhap <> 0 AND IDVattu=" & _IDVatTu
            sql &= " ORDER BY PHIEUDATHANG.NgayNhan DESC"

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                With ds.Tables(0)
                    tbModel.EditValue = .Rows(0)("Model")
                    tbCode.EditValue = .Rows(0)("Code")
                    tbTonBAC.EditValue = .Rows(0)("TonBAC")
                    tbTonNCC.EditValue = .Rows(0)("TonNCC")
                    tbTongNhap.EditValue = .Rows(0)("TongNhap")
                    tbTongXuat.EditValue = .Rows(0)("TongXuat")
                    tbTinhTrang.EditValue = .Rows(0)("TinhTrang")
                    If _HienThongTinNX Then
                        tbSLMOQ1.EditValue = .Rows(0)("SLMOQ1")
                        tbSLMOQ2.EditValue = .Rows(0)("SLMOQ2")
                        tbSLMOQ3.EditValue = .Rows(0)("SLMOQ3")
                        tbGiaMOQ1.EditValue = .Rows(0)("GiaMOQ1")
                        tbGiaMOQ2.EditValue = .Rows(0)("GiaMOQ2")
                        tbGiaMOQ3.EditValue = .Rows(0)("GiaMOQ3")
                        tbGiaBB.EditValue = .Rows(0)("GiaBB")
                        tbGiaBL.EditValue = .Rows(0)("GiaBL")
                        tbGiaNKBAC.EditValue = .Rows(0)("GiaNKBAC")
                    Else
                        tabNhapXuat.PageVisible = False
                    End If

                    tbGiaList.EditValue = .Rows(0)("GiaList")


                    tbAnh.ImageLocation = UrlAnhVatTu & .Rows(0)("TenNhom_ENG") & "\" & .Rows(0)("TenHang") & "\" & .Rows(0)("HinhAnh")

                    gdvListFile.DataSource = DataSourceDSFile(.Rows(0)("TaiLieu").ToString)
                    gdvListFileCT.Tag = UrlTaiLieuVatTu & .Rows(0)("TenNhom_ENG") & "\" & .Rows(0)("TenHang") & "\"
                    tbThongSo.EditValue = .Rows(0)("ThongSo")
                    tbGhiChu.EditValue = .Rows(0)("GhiChu")
                End With
                Dim View As New DataView(ds.Tables(1))
                _Count1 = View.ToTable(True, "ttcMa").Rows.Count
                Dim View2 As New DataView(ds.Tables(2))
                _Count2 = View2.ToTable(True, "ttcMa").Rows.Count

                Dim View3 As New DataView(ds.Tables(3))
                _Count3 = View3.ToTable(True, "ttcMa").Rows.Count

                gdvGiaNhap.DataSource = ds.Tables(1)
                gdvGiaBan.DataSource = ds.Tables(2)
                gdvChaoGia.DataSource = ds.Tables(3)

                

                If ds.Tables(4).Rows(0)(0) > 0 Then
                    lbMuon.Text = "KD đang mượn: " & ds.Tables(4).Rows(0)(0).ToString
                End If

                gdvDH.DataSource = ds.Tables(5)

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
        If Not _HienCGXK Then
            spliterMain.Panel2.Visible = False
            spliterMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2
            spliterMain.Collapsed = True
        End If
        If Not _HienNCC Then
            colNhaCC.Visible = False
            colNCCDH.Visible = False
        End If
        If Not _HienThongTinNX Then
            tabNhapXuat.PageVisible = False
        End If
        LayGiaNhapTB()
    End Sub

    Public Sub LayGiaNhapTB()
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
            gdvGiaNhapTB.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvGiaNhapCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvGiaNhapCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ttcMa" Then
                e.TotalValue = _Count1
            End If
        End If
    End Sub

    Private Sub gdvGiaBanCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvGiaBanCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ttcMa" Then
                e.TotalValue = _Count2
            End If
        End If
    End Sub

    Private Sub gdvChaoGiaCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvChaoGiaCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ttcMa" Then
                e.TotalValue = _Count3
            End If
        End If
    End Sub

    Private Sub tbGiaBB_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbGiaBB.EditValueChanged
        If tbGiaNKBAC.EditValue = 0 Then Exit Sub
        If (tbGiaBB.EditValue - tbGiaNKBAC.EditValue) / tbGiaNKBAC.EditValue < 0.05 Then
            tbGiaBB.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub tbGiaBL_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbGiaBL.EditValueChanged
        If tbGiaNKBAC.EditValue = 0 Then Exit Sub
        If (tbGiaBL.EditValue - tbGiaNKBAC.EditValue) / tbGiaNKBAC.EditValue < 0.05 Then
            tbGiaBL.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub tbAnh_DoubleClick(sender As System.Object, e As System.EventArgs) Handles tbAnh.DoubleClick
        OpenFileOnLocal(tbAnh.ImageLocation, IO.Path.GetFileName(tbAnh.ImageLocation))
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(gdvListFileCT.Tag & e.CellValue, e.CellValue)
        End If
    End Sub

    'Private Sub cbTrangThaiCG_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
    '    Dim sql As String = ""

    '    sql &= " SELECT CHAOGIA.Sophieu,IDVattu,Soluong,(Dongia * BANGCHAOGIA.Tygia)Dongia,BANGCHAOGIA.Tiente,Mucthue,Xuatthue,"
    '    sql &= " BANGCHAOGIA.Ngaythang,KHACHHANG.ttcMa,BANGCHAOGIA.CongTrinh,"
    '    sql &= " (CASE VATTU.DonGia1 WHEN 0 THEN 0 ELSE "
    '    sql &= " (CASE VATTU.TienTe1 WHEN 0 THEN round(((Dongia * BANGCHAOGIA.Tygia)/VATTU.DonGia1)*100,2) "
    '    sql &= " 	ELSE round(((Dongia * BANGCHAOGIA.Tygia)/(VATTU.DonGia1 * tblTienTe.TyGia))*100,2) END )END)GiaBanPT,"
    '    sql &= " (CASE CHAOGIA.CanXuat WHEN 0 THEN 0 ELSE 1 END)CX "
    '    sql &= " FROM CHAOGIA INNER JOIN BANGCHAOGIA ON CHAOGIA.Sophieu=BANGCHAOGIA.Sophieu"
    '    sql &= "         INNER JOIN VATTU ON CHAOGIA.IDVatTu= VATTU.ID "
    '    sql &= "         INNER JOIN tblTienTe ON tblTienTe.ID= VATTU.TienTe1 "
    '    sql &= "         INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
    '    sql &= " WHERE (CHAOGIA.TrangThai <>2 OR CHAOGIA.CanXuat <> 0) AND CHAOGIA.IDVattu=" & _IDVatTu
    '    sql &= " ORDER BY BANGCHAOGIA.Ngaythang DESC"

    '    Dim dt As DataTable = ExecuteSQLDataTable(sql)
    '    If Not dt Is Nothing Then
    '        gdvChaoGia.DataSource = dt
    '    Else
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub
End Class