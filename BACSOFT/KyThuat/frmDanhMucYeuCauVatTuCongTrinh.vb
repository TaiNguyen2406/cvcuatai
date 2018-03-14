Imports BACSOFT.Db.SqlHelper
Public Class frmDanhMucYeuCauVatTuCongTrinh

    Public SoCG As String = ""

    Private Sub frmDanhMucYeuCauVatTuCongTrinh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        lblSoCG.Caption = "Số CG: " & SoCG
        LoadDuLieu()
    End Sub

    Private Sub LoadDuLieu()
        LoadYcXuatTam()
        LoadYcTraXuatTam()
    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick
        LoadDuLieu()
    End Sub


    Private Sub LoadYcXuatTam()

        Dim sql As String = ""

        sql &= "SELECT "

        sql &= "PXKT.ID, SoYC as SoPhieu,ThoiGianCan,PXKT.GhiChu, PXKT.SoPhieu as SoPhieuXuatTam, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiLap)NguoiLap, "
        sql &= "BCG.TenDuAn CongTrinh,  "

        sql &= "(select Ten from nhansu where Noictac = 74 and ID = BCG.IdNgXuLy)NguoiXuLy, "
        sql &= "BCG.Masodathang SoYC, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = BCG.IDTakeCare)TakeCare,  "
        sql &= "SoCG,(select ttcMa from KHACHHANG where id = BCG.IdKhachhang)MaKH, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiSuaYC)TenNguoiSuaYC,ThoiGianSuaYC,  "

        sql &= "PXKT.NoiDungXL + ' - (' + "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiXL) + ' ' + "
        sql &= "(select left(convert(nvarchar,PXKT.ThoiGianXL,108),5) + ' ' + convert(nvarchar,PXKT.ThoiGianXL,103)) + "
        sql &= " ')' NoiDungXuLy, "

        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiXuat)PhuTrachKho  "

        sql &= " FROM PHIEUXUATKHOTAM PXKT LEFT JOIN BANGCHAOGIA BCG ON PXKT.SoCG = BCG.SoPhieu WHERE BCG.SoPhieu = @SoPhieu "

        sql &= " ORDER BY ThoiGianCan DESC "
        sql &= " OPTION ( FORCE ORDER ) "


        AddParameter("@SoPhieu", SoCG)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            tabXuatTam.Text = "YÊU CẦU KHO XUẤT TẠM (" & dt.Rows.Count & ")"
        End If

        gdv.DataSource = dt

        LoadDuLieuYCXuatChiTiet(gdvData.GetFocusedRowCellValue("ID"))

    End Sub


    Private Sub LoadDuLieuYCXuatChiTiet(id As Object)

        ShowWaiting("Đang tải chi tiết nội dung yêu cầu ...")

        Dim sql As String = ""

        sql &= " SELECT  XUATKHOTAM.IDVatTu, "
        sql &= " TENVATTU.Ten AS TenVT,  "
        sql &= " TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo, "
        sql &= " TENDONVITINH.Ten AS TenDVT, "
        sql &= " XUATKHOTAM.SlYeuCau,ChaoGia.SoLuong as SlCG, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon, "
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe, "

        sql &= " isnull(SlXuatKho,0) SlDaXuatTam, "
        sql &= "  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) "
        sql &= " as XuatTam,"
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

        AddParameter("@Id_Phieu", id)


        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If

        gdvVT.DataSource = dt

        CloseWaiting()

    End Sub


    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData.FocusedRowChanged
        LoadDuLieuYCXuatChiTiet(gdvData.GetFocusedRowCellValue("ID"))
    End Sub



    Private Sub LoadYcTraXuatTam()

        Dim sql As String = ""

        sql &= "SELECT "

        sql &= "PXKT.ID, SoYC as SoPhieu,ThoiGianTra,PXKT.GhiChu, PXKT.SoPhieu as SoPhieuNhapTam, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiLap)NguoiLap, "
        sql &= "BCG.TenDuAn CongTrinh,  "

        sql &= "(select Ten from nhansu where Noictac = 74 and ID = BCG.IdNgXuLy)NguoiXuLy, "
        sql &= "BCG.Masodathang SoYC, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = BCG.IDTakeCare)TakeCare,  "
        sql &= "SoCG,(select ttcMa from KHACHHANG where id = BCG.IdKhachhang)MaKH, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiSuaYC)TenNguoiSuaYC,ThoiGianSuaYC,  "

        sql &= "PXKT.NoiDungXL + ' - (' + "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiXL) + ' ' + "
        sql &= "(select left(convert(nvarchar,PXKT.ThoiGianXL,108),5) + ' ' + convert(nvarchar,PXKT.ThoiGianXL,103)) + "
        sql &= " ')' NoiDungXuLy, "

        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiNhap)PhuTrachKho  "

        sql &= " FROM PHIEUNHAPKHOTAM PXKT LEFT JOIN BANGCHAOGIA BCG ON PXKT.SoCG = BCG.SoPhieu WHERE BCG.SoPhieu = @SoPhieu "

        sql &= " ORDER BY ThoiGianTra DESC "
        sql &= " OPTION ( FORCE ORDER ) "

        AddParameter("@SoPhieu", SoCG)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            tabNhapTam.Text = "YÊU CẦU TRẢ LẠI XUẤT TẠM (" & dt.Rows.Count & ")"
        End If

        gdv2.DataSource = dt

        LoadDuLieuYCTraXuatChiTiet(gdvData2.GetFocusedRowCellValue("ID"))

    End Sub


    Private Sub LoadDuLieuYCTraXuatChiTiet(id As Object)

        Dim sql As String = ""
        ShowWaiting("Đang tải chi tiết nội dung yêu cầu ...")

        sql &= " SELECT  NHAPKHOTAM.IDVatTu, "
        sql &= " TENVATTU.Ten AS TenVT,  "
        sql &= " TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo, "
        sql &= " TENDONVITINH.Ten AS TenDVT, "
        sql &= " NHAPKHOTAM.SlYeuCau, "

        sql &= ""
        sql &= " (SELECT isnull(SUM(SlXuatKho),0) FROM XUATKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = CHAOGIA.SoPhieu)SlCG,  "

        sql &= " (SELECT isnull(SUM(SlYeuCau),0) FROM NHAPKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = CHAOGIA.SoPhieu)SlDaXuatTam,  "

        sql &= " (SELECT isnull(SUM(SlYeuCau),0) FROM XUATKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = CHAOGIA.SoPhieu)slTon,  "


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

        AddParameter("@Id_Phieu", id)


        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If

        gdvVT2.DataSource = dt

        CloseWaiting()

    End Sub

    Private Sub gdvData2_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData2.FocusedRowChanged
        LoadDuLieuYCTraXuatChiTiet(gdvData2.GetFocusedRowCellValue("ID"))
    End Sub


    Private Sub gdvData2_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData2.CustomDrawCell
        If e.RowHandle < 0 Then Exit Sub
        Try
            If Not gdvData2.GetRowCellValue(e.RowHandle, "SoPhieuNhapTam") Is DBNull.Value Then
                e.Appearance.BackColor = Color.LightPink
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        If e.RowHandle < 0 Then Exit Sub
        Try
            If Not gdvData.GetRowCellValue(e.RowHandle, "SoPhieuXuatTam") Is DBNull.Value Then
                e.Appearance.BackColor = Color.LightPink
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhat.ItemClick
        If tabMain.SelectedTabPage Is tabXuatTam Then
            If gdvData.FocusedRowHandle < 0 Then Exit Sub
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
                ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
                Exit Sub
            End If
            If Not gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") Is DBNull.Value Then
                ShowCanhBao("Phiếu yêu cầu này đã có xuất kho tạm số " & gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") & " nên không thể xóa ")
                Exit Sub
            End If
            Dim f As New frmUpdateYcXuatTam
            TrangThai.isUpdate = True
            f.idPhieu = gdvData.GetFocusedRowCellValue("ID")
            f.Text = "Lấy vật tư thi công cho chào giá " & gdvData.GetFocusedRowCellValue("SoCG")
            f.SoCG = gdvData.GetFocusedRowCellValue("SoCG")
            f.ShowDialog()
        ElseIf tabMain.SelectedTabPage Is tabNhapTam Then
            If gdvData2.FocusedRowHandle < 0 Then Exit Sub

            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
                ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
                Exit Sub
            End If

            If Not gdvData2.GetFocusedRowCellValue("SoPhieuNhapTam") Is DBNull.Value Then
                ShowCanhBao("Phiếu yêu cầu này đã có nhập kho tạm số " & gdvData2.GetFocusedRowCellValue("SoPhieuNhapTam") & " nên không thể xóa ")
                Exit Sub
            End If
            Dim f As New frmUpdateYcNhapTam
            TrangThai.isUpdate = True
            f.idPhieu = gdvData2.GetFocusedRowCellValue("ID")
            f.Text = "Trả vật tư thi công cho chào giá " & gdvData2.GetFocusedRowCellValue("SoCG")
            f.SoCG = gdvData2.GetFocusedRowCellValue("SoCG")
            f.ShowDialog()
        End If
    End Sub



    Private Sub btnXoaPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaPhieu.ItemClick
        If tabMain.SelectedTabPage Is tabXuatTam Then
            If gdvData.FocusedRowHandle < 0 Then Exit Sub
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
                ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
                Exit Sub
            End If
            If Not gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") Is DBNull.Value Then
                ShowCanhBao("Phiếu yêu cầu này đã có xuất kho tạm số " & gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") & " nên không thể xóa ")
                Exit Sub
            End If
            If Not ShowCauHoi("Xóa số phiếu " & gdvData.GetFocusedRowCellValue("SoPhieu") & " hay không?") Then Exit Sub
            AddParameter("@ID", gdvData.GetFocusedRowCellValue("ID"))
            If doDelete("PHIEUXUATKHOTAM", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
            LoadDuLieu()
        ElseIf tabMain.SelectedTabPage Is tabNhapTam Then
            If gdvData2.FocusedRowHandle < 0 Then Exit Sub
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
                ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
                Exit Sub
            End If
            If Not gdvData2.GetFocusedRowCellValue("SoPhieuNhapTam") Is DBNull.Value Then
                ShowCanhBao("Phiếu yêu cầu này đã có nhập kho tạm số " & gdvData2.GetFocusedRowCellValue("SoPhieuNhapTam") & " nên không thể xóa ")
                Exit Sub
            End If
            If Not ShowCauHoi("Xóa số phiếu " & gdvData2.GetFocusedRowCellValue("SoPhieu") & " hay không?") Then Exit Sub
            AddParameter("@ID", gdvData2.GetFocusedRowCellValue("ID"))
            If doDelete("PHIEUNHAPKHOTAM", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
            LoadDuLieu()
        End If
    End Sub


    Private Sub btnInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnInPhieu.ItemClick
        If tabMain.SelectedTabPage Is tabXuatTam Then
            If gdvData.FocusedRowHandle < 0 Then Exit Sub
            frmUpdateYcXuatTam.InPhieuXuaTam(gdvData.GetFocusedRowCellValue("ID"))
        ElseIf tabMain.SelectedTabPage Is tabNhapTam Then
            If gdvData2.FocusedRowHandle < 0 Then Exit Sub
            frmUpdateYcNhapTam.InPhieuXuaTam(gdvData2.GetFocusedRowCellValue("ID"))
        End If
    End Sub



    Private Sub gdvVTCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvVTCT.MouseDown
        Dim calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        calHitTestHoaDon = gdvVTCT.CalcHitInfo(e.Location)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If calHitTestHoaDon.InRowCell Then
                mnu.ShowPopup(gdvVT.PointToScreen(e.Location))
            End If
        End If
    End Sub

    Private Sub gdvVTCT2_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvVTCT2.MouseDown
        Dim calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        calHitTestHoaDon = gdvVTCT2.CalcHitInfo(e.Location)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If calHitTestHoaDon.InRowCell Then
                mnu.ShowPopup(gdvVT2.PointToScreen(e.Location))
            End If
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        If tabMain.SelectedTabPageIndex = 0 Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            Dim f As New frmLichSuNhapXuatKhoTam
            f.idVatTu = gdvVTCT.GetFocusedRowCellValue("IDVatTu")
            f.ShowDialog()
        Else
            If gdvVTCT2.FocusedRowHandle < 0 Then Exit Sub
            Dim f As New frmLichSuNhapXuatKhoTam
            f.idVatTu = gdvVTCT2.GetFocusedRowCellValue("IDVatTu")
            f.ShowDialog()
        End If
    End Sub
End Class



