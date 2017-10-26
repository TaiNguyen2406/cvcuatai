Imports BACSOFT.Db.SqlHelper
Public Class frmHangHoaCongTrinhCanXuat

    Private Sub frmHangHoaCongTrinhCanXuat_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        Select Case Me.Tag
            Case "KHOHANGCANXUAT"
                'btnCapNhat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                'btnXoaPhieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Case "KYTHUATYEUCAUVATTU"
                btnLapPhieuXuatKhoTam.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End Select

        Dim tg As DateTime = GetServerTime()
        tg = New DateTime(tg.Year, tg.Month, tg.Day)
        txtDenNgay.EditValue = tg
        txtTuNgay.EditValue = tg.AddYears(-1)

        LoadDuLieu()
    End Sub


    Private Sub LoadDuLieu()


        Dim sql As String = ""

        sql &= "SELECT "

        If cmbTieuChi.EditValue = "Top 500" Then
            sql &= " TOP 500 "
        End If


        sql &= "PXKT.ID, SoYC as SoPhieu,ThoiGianCan,GhiChu, PXKT.SoPhieu as SoPhieuXuatTam, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiLap)NguoiLap, "
        sql &= "BCG.TenDuAn CongTrinh,  "

        sql &= "(select Ten from nhansu where Noictac = 74 and ID = BCG.IdNgXuLy)NguoiXuLy, "
        sql &= "BCG.Masodathang SoYC, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = BCG.IDTakeCare)TakeCare,  "
        sql &= "SoCG,(select ttcMa from KHACHHANG where id = BCG.IdKhachhang)MaKH, "
        sql &= "(select Ten from nhansu where Noictac = 74 and ID = PXKT.IdNguoiSuaYC)TenNguoiSuaYC,ThoiGianSuaYC  "

        sql &= " FROM PHIEUXUATKHOTAM PXKT LEFT JOIN BANGCHAOGIA BCG ON PXKT.SoCG = BCG.SoPhieu WHERE 1=1 "


        If cmbTieuChi.EditValue <> "Top 500" Then
            sql &= " AND Convert(datetime,Convert(nvarchar,ThoiGianCan,103),103) >= @TuNgay  "
            sql &= " AND Convert(datetime,Convert(nvarchar,ThoiGianCan,103),103) <= @DenNgay  "
            AddParameter("@TuNgay", txtTuNgay.EditValue)
            AddParameter("@DenNgay", txtDenNgay.EditValue)
        End If


        sql &= " ORDER BY ThoiGianCan DESC "
        sql &= " OPTION ( FORCE ORDER ) "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If

        gdv.DataSource = dt

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
        LoadDuLieu()
        If gdvData.RowCount > 0 Then
            LoadDuLieuChiTiet(gdvData.GetRowCellValue(0, "ID"))
        Else
            LoadDuLieuChiTiet(-1)
        End If
    End Sub


    Private Sub LoadDuLieuChiTiet(id As Object)
        Dim sql As String = ""


        sql &= " SELECT  XUATKHOTAM.IDVatTu, "
        sql &= " TENVATTU.Ten AS TenVT,  "
        sql &= " TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo, "
        sql &= " TENDONVITINH.Ten AS TenDVT, "
        sql &= " XUATKHOTAM.SlYeuCau,ChaoGia.SoLuong as SlCG, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon, "
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe, "

        sql &= " isnull(SlXuatKho,0) SlDaXuatTam, "

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
    End Sub

    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData.FocusedRowChanged
        LoadDuLieuChiTiet(gdvData.GetFocusedRowCellValue("ID"))
    End Sub

    Private Sub btnXoaPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaPhieu.ItemClick

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

    End Sub

    Private Sub btnInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnInPhieu.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        frmUpdateYcXuatTam.InPhieuXuaTam(gdvData.GetFocusedRowCellValue("ID"))
    End Sub

    Private Sub btnLapPhieuXuatKhoTam_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLapPhieuXuatKhoTam.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        If Not gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") Is DBNull.Value Then
            ShowCanhBao("Phiếu yêu cầu này đã có xuất kho tạm số " & gdvData.GetFocusedRowCellValue("SoPhieuXuatTam") & " nên không thể cập nhật ")
            Exit Sub
        End If

        Dim f As New frmUpdateXuatKhoTam
        TrangThai.isAddNew = True
        f.Text = "Xuất kho tạm cho số yêu cầu " & gdvData.GetFocusedRowCellValue("SoPhieu")
        f.IdPhieu = gdvData.GetFocusedRowCellValue("ID")
        f.ShowDialog()

        Try
            If f.txtSoPhieuXuatTam.Text.Trim <> "" Then
                gdvData.SetFocusedRowCellValue("SoPhieuXuatTam", f.txtSoPhieuXuatTam.Text)
            End If
        Catch ex As Exception
        End Try
 



    End Sub

    Private Sub chkLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLoc.CheckedChanged
        gdvData.OptionsView.ShowAutoFilterRow = chkLoc.Checked
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

    End Sub



End Class
