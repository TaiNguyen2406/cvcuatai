Imports BACSOFT.Db.SqlHelper
Public Class frmTraLaiXuatKhoTam

    Private Sub frmTraLaiXuatKhoTam_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
        sql &= "	(SELECT Ten from nhansu where id = bangchaogia.idtakecare)TakeCare,"
        sql &= "	(SELECT Ten from nhansu where id = bangchaogia.IdNgXuLy)NguoiXuLy "
        'sql &= "    (SELECT count(ID) FROM PHIEUXUATKHO WHERE SophieuCG=BANGCHAOGIA.SoPhieu)SoLuongNhapKho "
        sql &= "	FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID "
        sql &= " WHERE (SELECT COUNT(ID) FROM phieunhapkhotam where SoCG = BANGCHAOGIA.SoPhieu and SoPhieu is not null) > 0 "

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
        sql &= " NHAPKHOTAM.IDVatTu, "
        sql &= " TENVATTU.Ten AS TenVT,  "
        sql &= " TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo, "
        sql &= " TENDONVITINH.Ten AS TenDVT, "
        sql &= " NHAPKHOTAM.SlYeuCau, "

        sql &= " (SELECT isnull(SUM(SlXuatKho),0) FROM XUATKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = CHAOGIA.Sophieu)SlCG,  "

        sql &= "  NHAPKHOTAM.SlNhapKho SlDaXuatTam, "

        'sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon, "
        'sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe, "

        'sql &= " NHAPKHOTAM.SlXuatKho as SlDaXuatTam, "

        sql &= " ISNULL(CHAOGIA.AZ,0)AZ "
        sql &= " FROM NHAPKHOTAM  "
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=NHAPKHOTAM.SoCG "
        sql &= " LEFT OUTER JOIN VATTU ON NHAPKHOTAM.IdVatTu=VATTU.ID "
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        sql &= " LEFT OUTER JOIN CHAOGIA ON CHAOGIA.SoPhieu=NHAPKHOTAM.SoCG AND CHAOGIA.IdVatTu = NHAPKHOTAM.IdVatTu "
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
        sql &= "ROW_NUMBER() OVER(ORDER BY ThoiGianTra ASC) AS SoTT, "
        sql &= "ID, SoPhieu as SoPhieuNhapTam,ThoiGianNhap as ThoiGianNhapTam,GhiChu as NoiDungYeuCau, "
        sql &= "(select Ten from nhansu where ID = phieunhapkhotam.IdNguoiLap)NguoiGiaoDich,   "
        sql &= "(select Ten from nhansu where ID = phieunhapkhotam.IdNguoiNhap)NguoiPhuTrachKho, "
        sql &= "SoYC as SoPhieuYeuCau,ThoiGianTra as ThoiGianYeuCau "
        sql &= "FROM PHIEUNHAPKHOTAM WHERE SoPhieu is not null  "
        sql &= "AND SoCG = @SoCG  "


        sql &= " ORDER BY ThoiGianTra ASC "
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

        'If gdvDataCongTrinh.GetFocusedRowCellValue("SoLuongXuatKho") > 0 Then
        '    ShowCanhBao("Không thể xóa phiếu xuất kho khi đã xuất hệ thống thực!")
        '    Exit Sub
        'End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        'If Not gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") Is DBNull.Value Then
        '    ShowCanhBao("Phiếu yêu cầu này đã có xuất kho tạm số " & gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") & " nên không thể xóa ")
        '    Exit Sub
        'End If


        If Not ShowCauHoi("Xóa số phiếu " & gdvDataXuatKho.GetFocusedRowCellValue("SoPhieuNhapTam") & " hay không?") Then Exit Sub

        'AddParameter("@ID", gdvData.GetFocusedRowCellValue("ID"))
        'If doDelete("PHIEUXUATKHOTAM", "ID=@ID") Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        'End If

        AddParameter("@SlNhapKho", DBNull.Value)
        AddParameterWhere("@ID", gdvDataXuatKho.GetFocusedRowCellValue("ID"))
        doUpdate("NHAPKHOTAM", "Id_Phieu=@ID")


        AddParameter("@SoPhieu", DBNull.Value)
        AddParameter("@IdNguoiNhap", DBNull.Value)
        AddParameter("@ThoiGianNhap", DBNull.Value)
        AddParameter("@GhiChuNK", DBNull.Value)
        AddParameterWhere("@ID", gdvDataXuatKho.GetFocusedRowCellValue("ID"))
        doUpdate("PHIEUNHAPKHOTAM", "ID=@ID")

        LoadDuLieuXuatKhoTam(gdvDataCongTrinh.GetFocusedRowCellValue("SoCG"))

    End Sub

    Private Sub btnInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnInPhieu.ItemClick
        If gdvDataXuatKho.FocusedRowHandle < 0 Then Exit Sub
        frmUpdateTraXuatKhoTam.InPhieuXuaTam(gdvDataXuatKho.GetFocusedRowCellValue("ID"))
    End Sub



    Private Sub chkLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLoc.CheckedChanged
        gdvDataCongTrinh.OptionsView.ShowAutoFilterRow = chkLoc.Checked
    End Sub

    Private Sub gdvDataCongTrinh_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvDataCongTrinh.CustomDrawCell
        'If e.RowHandle < 0 Then Exit Sub
        'Try
        '    If gdvDataCongTrinh.GetRowCellValue(e.RowHandle, "SoLuongXuatKho") > 0 Then
        '        e.Appearance.BackColor = Color.LightPink
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub gdvDataXuatKho_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvDataXuatKho.FocusedRowChanged
        LoadDuLieuVatTuXuatKho()
    End Sub


    Private Sub btnXuatKhoHeThong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXuatKhoHeThong.ItemClick
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
        '    ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
        '    Exit Sub
        'End If

        'If gdvDataCongTrinh.GetFocusedRowCellValue("SoLuongXuatKho") > 0 Then
        '    ShowCanhBao("Công trình này đã thực hiện xuất kho rồi!")
        '    Exit Sub
        'End If


        'If Not ShowCauHoi("Thực hiện xuất kho với chào giá " & gdvDataCongTrinh.GetFocusedRowCellValue("SoCG") & "?") Then Exit Sub

        'TrangThai.isAddNew = True
        'Dim f As New frmCNXuatKho
        'f.Tag = Me.Parent.Tag
        'f.gdvMaKH.EditValue = gdvDataCongTrinh.GetFocusedRowCellValue("IDKhachhang")
        'f.gdvPhieuCG.EditValue = gdvDataCongTrinh.GetFocusedRowCellValue("SoCG")
        'Dim sql As String = "select IdVatTu,SLXuatKho from xuatkhotam WHERE SoCG = @SoCG AND SLXuatKho > 0"
        'AddParameter("@SoCG", gdvDataCongTrinh.GetFocusedRowCellValue("SoCG"))
        'Dim dt As DataTable = ExecuteSQLDataTable(sql)
        'f.gdvThamChieuCT.CloseEditor()
        'f.gdvThamChieuCT.UpdateCurrentRow()
        'f.dtFromXuatKhoTam = dt
        'f.ShowDialog()

        'If f.tbSoPhieu.Text <> "" Then
        '    btnTaiDuLieu.PerformClick()
        'End If

    End Sub


End Class
