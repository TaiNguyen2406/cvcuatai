Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmKetQuaXuatKhoCT

    Private Sub frmKetQuaXuatKhoCT_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            colTienTruocThue.Visible = False
            colLoiNhuan.Visible = False
        End If
    End Sub

    Public Sub LoadCbVC()
        Dim sql As String = "SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE ttcKhachHang=3 ORDER BY Ten "
        sql &= " SELECT ID,Ten,TyGia FROM tblTienTe ORDER BY ID "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbDVVC.DataSource = ds.Tables(0)
            rcbTienTe.DataSource = ds.Tables(1)
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
            sql &= "   KHACHHANG.ttcMa, "
            sql &= "   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,BANGCHAOGIA.SoPhieu AS SoPhieuCG,BANGCHAOGIA.TenDuAn,BANGCHAOGIA.IDTakeCare, PHIEUXUATKHO.TientruocThue * PHIEUXUATKHO.Tygia AS TienTruocThue,"
            sql &= "     (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(View_XuatKhoTongGiaNhapTB.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
            sql &= "     - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) /  (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) LoiNhuan, "
            sql &= "   NHANSU.Ten AS NVPhuTrachHD,ISNULL( XUCTIENHD.Ten,NHANSU.Ten) as NVXucTienHD,PTThiCong.Ten as NVPhuTrachThiCong,(CASE NhanKS WHEN 0 THEN 'KD' WHEN 1 THEN 'KT' ELSE '' END)NhanKS"
            sql &= " FROM PHIEUXUATKHO "
            sql &= " INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
            sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
            sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachhang"
            sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
            sql &= " LEFT JOIN NHANSU AS XUCTIENHD ON XUCTIENHD.ID= BANGCHAOGIA.IDPhuTrachKyHD"
            sql &= " LEFT JOIN NHANSU AS PTThiCong ON PTThiCong.ID= BANGCHAOGIA.IDPhuTrachCT"
            sql &= " LEFT JOIN View_XuatKhoTongGiaNhapTB ON PHIEUXUATKHO.Sophieu = View_XuatKhoTongGiaNhapTB.Sophieu "
            sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
            sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
            sql &= " WHERE PhieuXuatKho.CongTrinh=1 AND Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
            sql &= " ORDER BY PHIEUXUATKHO.SoPhieu"
           
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

        saveFile.FileName = "Loi nhuan theo xuat kho.xls"

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
        fCNChaoGia.chkCongTrinh.Checked = True
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

   


    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub


End Class
