Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmKetQuaChaoGiaCT

    Private Sub frmKetQuaChaoGiaCT_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        LoadCbTrangThai()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
            colTienTruocThue.Visible = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            colTienCK.Visible = False
            colPTCK.Visible = False
            If MaPhongBan = 2 Then
                mXemChaoGia.Visibility = XtraBars.BarItemVisibility.Never
            Else
                mVatTuHangHoaThiCong.Visibility = XtraBars.BarItemVisibility.Never
            End If
        End If
    End Sub

    Public Sub LoadCbTrangThai()
        Dim sql As String = "SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=2 And (Ma=1 OR Ma=2)"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbTrangThai.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    'Load thông thường
    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            sql &= " Select DISTINCT"
            sql &= "   KHACHHANG.ttcMa, BANGCHAOGIA.SoPhieu AS SoPhieuCG,BANGCHAOGIA.TenDuAn, BANGCHAOGIA.TientruocThue * BANGCHAOGIA.Tygia AS TienTruocThue,BANGCHAOGIA.TienChietKhau,BANGCHAOGIA.NgayThang as NgayCG, BANGCHAOGIA.NgayNhan as NgayXN,"
            sql &= "   NHANSU.Ten AS NVPhuTrachHD,ISNULL( XUCTIENHD.Ten,NHANSU.Ten) as NVXucTienHD,PTThiCong.Ten as NVPhuTrachThiCong,(CASE NhanKS WHEN 0 THEN NHANSU.Ten WHEN 1 THEN NHANXL.Ten ELSE '' END)NhanKS,"
            sql &= "    (CASE BANGCHAOGIA.TienTruocThue WHEN 0 THEN 0 ELSE Round(((ISNULL(BANGCHAOGIA.tienchietkhau, 0) /  (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END)))/BANGCHAOGIA.TienTruocThue),2)*100 END)PTCK,"
            sql &= "   tblTuDien.NoiDung AS TrangThai,BANGCHAOGIA.TrangThai as TrangThaiCG,BANGCHAOGIA.IDTakeCare,BANGCHAOGIA.MaSoDatHang"
            sql &= " FROM BANGCHAOGIA "
            sql &= " INNER JOIN NHANSU ON NHANSU.ID=BANGCHAOGIA.IDTakecare"
            sql &= " LEFT JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=BANGCHAOGIA.MaSoDatHang"
            sql &= " LEFT JOIN NHANSU as NHANXL ON NHANSU.ID=BANGYEUCAU.IDNhanXL"
            sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachhang"
            sql &= " LEFT JOIN NHANSU AS XUCTIENHD ON XUCTIENHD.ID= BANGCHAOGIA.IDPhuTrachKyHD"
            sql &= " LEFT JOIN NHANSU AS PTThiCong ON PTThiCong.ID= BANGCHAOGIA.IDPhuTrachCT"
            sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=BANGCHAOGIA.TrangThai AND tblTuDien.Loai=2 "
            sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(BANGCHAOGIA.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(BANGCHAOGIA.Ngaythang) "
            sql &= " WHERE BANGCHAOGIA.CongTrinh=1 AND BANGCHAOGIA.TrangThai IN (1,2) "
            If cbTrangThai.EditValue = 2 Then
                sql &= " AND Convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) BETWEEN @TuNgay AND @DenNgay"
            Else
                sql &= " AND Convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
            End If


            If Not cbTrangThai.EditValue Is Nothing Then
                sql &= " AND BANGCHAOGIA.TRangThai=@TT"
                AddParameterWhere("@TT", cbTrangThai.EditValue)
            End If

            sql &= " ORDER BY BANGCHAOGIA.SoPhieu"

            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb



            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "ket qua chao gia CT.xls"

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


   

    Private Sub rcbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTrangThai.ButtonClick
        If e.Button.Index = 1 Then
            cbTrangThai.EditValue = Nothing
        End If
    End Sub

    Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("TrangThaiCG") = TrangThaiChaoGia.DaXacNhan Or gdvCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isUpdate = True
        fCNChaoGia.chkCongTrinh.Checked = True
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNChaoGia.Tag = deskTop.mChaoGia.Name

        fCNChaoGia.Show()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub


        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub


    Private Sub mVatTuHangHoaThiCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mVatTuHangHoaThiCong.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub


        If Convert.ToInt32(gdvCT.GetFocusedRowCellValue("TrangThaiCG")) <> Convert.ToInt32(TrangThaiChaoGia.DaXacNhan) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mCongTrinhCanXuLy.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mCongTrinhCanXuLy.Name, DanhMucQuyen.TPKyThuat) Then
                ShowCanhBao("Công trình chưa được xác nhận !")
                Exit Sub
            End If
        End If

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM PHIEUXUATKHO WHERE SoPhieuCG='" & gdvCT.GetFocusedRowCellValue("Sophieu") & "'")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If Not ShowCauHoi("Đã có xuất kho cho công trình này, bạn có muốn tiếp tục xử lý không ?") Then Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        fCNCongTrinh = New frmCNCongTrinhCanXuLy
        fCNCongTrinh.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNCongTrinh.SPDatHang = gdvCT.GetFocusedRowCellValue("MaSoDatHang")
        fCNCongTrinh._MaKH = gdvCT.GetFocusedRowCellValue("ttcMa")
        fCNCongTrinh.SPYeuCau = gdvCT.GetFocusedRowCellValue("MaSoDatHang")
        fCNCongTrinh.TrangThaiCG = gdvCT.GetFocusedRowCellValue("TrangThaiCG")
        ' fCNCongTrinh._SPDaSaoChep = _SPSaoChep
        If Convert.ToInt32(gdvCT.GetFocusedRowCellValue("TrangThaiCG")) <> Convert.ToInt32(TrangThaiChaoGia.DaXacNhan) Then
            fCNCongTrinh.TrangThaiCT = False
        Else
            fCNCongTrinh.TrangThaiCT = True
        End If

        fCNCongTrinh.Tag = deskTop.mCongTrinhCanXuLy.Name
        fCNCongTrinh.ShowDialog()
        gdvCT.FocusedRowHandle = index
    End Sub
End Class
