Imports BACSOFT.Db.SqlHelper
Public Class frmXuatKhoTam

    Private Sub frmHangHoaCongTrinhCanXuat_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        tg = New DateTime(tg.Year, tg.Month, tg.Day)
        txtDenNgay.EditValue = tg
        txtTuNgay.EditValue = tg.AddYears(-1)

        LoadDuLieuCongTrinh()
    End Sub


    Private Sub LoadDuLieuCongTrinh()

        ShowWaiting("Đang tải danh sách công trình ...")

        Dim sql As String = ""
        If cmbTieuChi.EditValue = "Top 500" Then
            sql = " SELECT TOP 500 BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu as SoCG,KHACHHANG.ttcMa AS MaKH, BANGCHAOGIA.IDKhachhang, "
        Else
            sql = " SELECT BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu as SoCG,KHACHHANG.ttcMa AS MaKH, BANGCHAOGIA.IDKhachhang, "
        End If
        sql &= "	BANGCHAOGIA.TenDuan CongTrinh,BANGCHAOGIA.masodathang SoYC,"
        sql &= "    (Case BANGCHAOGIA.XuLy WHEN 0 THEN N'Cần xử lý' WHEN 1 THEN N'Đã xử lý' END) AS XuLy, "
        sql &= "	(SELECT Ten from nhansu where id = bangchaogia.idtakecare)TakeCare,"
        sql &= "	(SELECT Ten from nhansu where id = bangchaogia.IdNgXuLy)NguoiXuLy, "
        sql &= "    (SELECT count(ID) FROM PHIEUXUATKHO WHERE SophieuCG=BANGCHAOGIA.SoPhieu)SoLuongXuatKho "
        sql &= "	FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID "
        sql &= " WHERE (SELECT COUNT(ID) FROM phieuxuatkhotam where SoCG = BANGCHAOGIA.SoPhieu and SoPhieu is not null) > 0 "

        If cmbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", txtTuNgay.EditValue)
            AddParameterWhere("@DenNgay", txtDenNgay.EditValue)
            sql &= " AND Convert(datetime,convert(nvarchar,BANGCHAOGIA.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        sql &= " ORDER BY BANGCHAOGIA.Ngaythang DESC "
        sql &= " OPTION ( FORCE ORDER ) "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If Not dt Is Nothing Then
            gdvCongTrinh.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        CloseWaiting()



    End Sub


    Private Sub LoadDuLieuVatTuXuatKho()
        Dim sql As String = ""

        sql &= " SELECT  "
        sql &= " ROW_NUMBER() OVER(ORDER BY CHAOGIA.AZ ASC) AS SoTT, "
        sql &= " XUATKHOTAM.IDVatTu, "
        sql &= " TENVATTU.Ten AS TenVT,  "
        sql &= " TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo, "
        sql &= " TENDONVITINH.Ten AS TenDVT, "
        sql &= " XUATKHOTAM.SlYeuCau,ChaoGia.SoLuong as SlCG, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon, "


        'sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = xuatkhotam.SoCG),'')),0) - "
        'sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = nhapkhotam.SoCG),'')),0) "
        'sql &= " as XuatTam, "
        sql &= "  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) "
        sql &= " as XuatTam,"
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe, "

        sql &= " XUATKHOTAM.SlXuatKho as SlDaXuatTam, "


        sql &= " ISNULL(CHAOGIA.AZ,0)AZ "
        sql &= " FROM XUATKHOTAM  "
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=XUATKHOTAM.SoCG "
        sql &= " LEFT OUTER JOIN VATTU ON XUATKHOTAM.IdVatTu=VATTU.ID "
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        sql &= " LEFT OUTER JOIN CHAOGIA ON CHAOGIA.SoPhieu=XUATKHOTAM.SoCG AND CHAOGIA.IdVatTu = XUATKHOTAM.IdVatTu "
        sql &= " where id_phieu = @Id_Phieu "
        sql &= " ORDER BY AZ "
        sql &= " OPTION ( FORCE ORDER ) "

        If gdvDataXuatKho.FocusedRowHandle < 0 Then
            AddParameter("@Id_Phieu", -1)
        Else
            AddParameter("@Id_Phieu", gdvDataXuatKho.GetFocusedRowCellValue("ID"))
        End If



        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        gdvVT.DataSource = dt
    End Sub

    Private Sub cmbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbTieuChi.EditValueChanged
        If cmbTieuChi.EditValue = "Top 500" Then
            txtTuNgay.Enabled = False
            txtDenNgay.Enabled = False
        Else
            txtTuNgay.Enabled = True
            txtDenNgay.Enabled = True
        End If
    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick
        LoadDuLieuCongTrinh()
        If gdvDataCongTrinh.RowCount > 0 Then
            LoadDuLieuXuatKhoTam(gdvDataCongTrinh.GetFocusedRowCellValue("SoCG"))
        Else
            LoadDuLieuXuatKhoTam(-1)
        End If
        LoadDuLieuVatTuXuatKho()
    End Sub


    Private Sub LoadDuLieuXuatKhoTam(SoCG As Object)

        Dim sql As String = ""

        sql &= "SELECT "
        sql &= "ROW_NUMBER() OVER(ORDER BY ThoiGianCan ASC) AS SoTT, "
        sql &= "ID, SoPhieu as SoPhieuXuatTam,ThoiGianXuat as ThoiGianXuatTam,GhiChu as NoiDungYeuCau, "
        sql &= "(select Ten from nhansu where ID = phieuxuatkhotam.IdNguoiLap)NguoiGiaoDich,   "
        sql &= "(select Ten from nhansu where ID = phieuxuatkhotam.IdNguoiXuat)NguoiPhuTrachKho, "
        sql &= "SoYC as SoPhieuYeuCau,ThoiGianCan as ThoiGianYeuCau "
        sql &= "FROM PHIEUXUATKHOTAM WHERE SoPhieu is not null  "
        sql &= "AND SoCG = @SoCG  "


        sql &= " ORDER BY ThoiGianCan ASC "
        sql &= " OPTION ( FORCE ORDER ) "

        AddParameter("@SoCG", SoCG)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If

        gdvXuatKho.DataSource = dt
    End Sub

    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvDataCongTrinh.FocusedRowChanged
        LoadDuLieuXuatKhoTam(gdvDataCongTrinh.GetFocusedRowCellValue("SoCG"))
        LoadDuLieuVatTuXuatKho()
    End Sub

    Private Sub btnXoaPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaPhieu.ItemClick

        If gdvDataXuatKho.FocusedRowHandle < 0 Then Exit Sub

        If gdvDataCongTrinh.GetFocusedRowCellValue("SoLuongXuatKho") > 0 Then
            ShowCanhBao("Không thể xóa phiếu xuất kho khi đã xuất hệ thống thực!")
            Exit Sub
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        'If Not gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") Is DBNull.Value Then
        '    ShowCanhBao("Phiếu yêu cầu này đã có xuất kho tạm số " & gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") & " nên không thể xóa ")
        '    Exit Sub
        'End If


        If Not ShowCauHoi("Xóa số phiếu " & gdvDataXuatKho.GetFocusedRowCellValue("SoPhieuXuatTam") & " hay không?") Then Exit Sub

        'AddParameter("@ID", gdvData.GetFocusedRowCellValue("ID"))
        'If doDelete("PHIEUXUATKHOTAM", "ID=@ID") Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        'End If

        AddParameter("@SlXuatKho", DBNull.Value)
        AddParameterWhere("@ID", gdvDataXuatKho.GetFocusedRowCellValue("ID"))
        doUpdate("XUATKHOTAM", "Id_Phieu=@ID")


        AddParameter("@SoPhieu", DBNull.Value)
        AddParameter("@IdNguoiXuat", DBNull.Value)
        AddParameter("@ThoiGianXuat", DBNull.Value)
        AddParameter("@GhiChuXK", DBNull.Value)
        AddParameterWhere("@ID", gdvDataXuatKho.GetFocusedRowCellValue("ID"))
        doUpdate("PHIEUXUATKHOTAM", "ID=@ID")

        LoadDuLieuXuatKhoTam(gdvDataCongTrinh.GetFocusedRowCellValue("SoCG"))

    End Sub

    Private Sub btnInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnInPhieu.ItemClick
        If gdvDataXuatKho.FocusedRowHandle < 0 Then Exit Sub

        frmUpdateXuatKhoTam.InPhieuXuaTam(gdvDataXuatKho.GetFocusedRowCellValue("ID"))
    End Sub



    Private Sub chkLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLoc.CheckedChanged
        gdvDataCongTrinh.OptionsView.ShowAutoFilterRow = chkLoc.Checked
    End Sub

    Private Sub gdvDataCongTrinh_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvDataCongTrinh.CustomDrawCell
        If e.RowHandle < 0 Then Exit Sub
        Try
            If gdvDataCongTrinh.GetRowCellValue(e.RowHandle, "SoLuongXuatKho") > 0 Then
                e.Appearance.BackColor = Color.LightPink
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gdvDataXuatKho_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvDataXuatKho.FocusedRowChanged
        LoadDuLieuVatTuXuatKho()
    End Sub


    Private Sub btnXuatKhoHeThong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXuatKhoHeThong.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        If gdvDataCongTrinh.GetFocusedRowCellValue("XuLy").ToString <> "Đã xử lý" Then
            ShowCanhBao("Không thể lập xuất kho hệ thống khi công trình chưa chuyển trạng thái Đã xử lý!")
            Exit Sub
        End If

        If gdvDataCongTrinh.GetFocusedRowCellValue("SoLuongXuatKho") > 0 Then
            ShowCanhBao("Công trình này đã thực hiện xuất kho rồi!")
            Exit Sub
        End If


        If Not ShowCauHoi("Thực hiện xuất kho với chào giá " & gdvDataCongTrinh.GetFocusedRowCellValue("SoCG") & "?") Then Exit Sub

        TrangThai.isAddNew = True
        Dim f As New frmCNXuatKho
        f.Tag = Me.Parent.Tag
        f.gdvMaKH.EditValue = gdvDataCongTrinh.GetFocusedRowCellValue("IDKhachhang")
        f.gdvPhieuCG.EditValue = gdvDataCongTrinh.GetFocusedRowCellValue("SoCG")
        Dim sql As String = "select IdVatTu,SLXuatKho from xuatkhotam WHERE SoCG = @SoCG AND SLXuatKho > 0"
        AddParameter("@SoCG", gdvDataCongTrinh.GetFocusedRowCellValue("SoCG"))
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        f.gdvThamChieuCT.CloseEditor()
        f.gdvThamChieuCT.UpdateCurrentRow()
        f.dtFromXuatKhoTam = dt
        f.ShowDialog()

        If f.tbSoPhieu.Text <> "" Then
            btnTaiDuLieu.PerformClick()
        End If

    End Sub


End Class
