Imports BACSOFT.Db.SqlHelper
Imports DevExpress
Imports BACSOFT.TAI

Public Class frmKetQuaNhapKho

    Private Sub frmKetQuaNhapKho_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        Application.DoEvents()
        LoadPhongBan()
        Application.DoEvents()
        LoadTakeCare()
        Application.DoEvents()
        LoadCbVC()
        gdvDSCP.DataSource = LayDataSourceDSCP()
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbTakeCare.Enabled = True
            cbPhong.Enabled = True
        Else
            cbTakeCare.Enabled = False
            cbPhong.Enabled = False
            'colTienGoc.Visible = False
            'colChietKhau.Visible = False
            'colPTCK.Visible = False
        End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btPhanBoChiPhiNhap.Visibility = XtraBars.BarItemVisibility.Never
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

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub

    'Load thông thường
    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            If cbTieuChi.EditValue = "Nhập kho" Then
                colUNC.Visible = False
                colPhieuChi.Visible = False
                sql &= " Select DISTINCT"
                sql &= "   KHACHHANG.ttcMa, "
                sql &= "   PHIEUNHAPKHO.SoPhieu AS SoPhieuNK,PHIEUNHAPKHO.SoPhieuDH, PHIEUNHAPKHO.Ngaythang, "
                sql &= "   PHIEUNHAPKHO.TientruocThue * PHIEUNHAPKHO.Tygia AS TienTruocThue, PHIEUNHAPKHO.Tienthue * PHIEUNHAPKHO.Tygia AS TienThue, "
                sql &= "   (ISNULL((SELECT SUM (SoTien) FROM CHI WHERE ChiPhiNhap=1 AND MucDich IN (205,208) AND ( PhieuTC1=PHIEUNHAPKHO.SoPhieu OR PHIEUTC0=PHIEUNHAPKHO.SoPHieuDH)), 0) + "
                sql &= "   ISNULL((SELECT SUM (SoTien) FROM UNC WHERE ChiPhiNhap=1 AND MucDich IN (205,208) AND (PhieuTC1=PHIEUNHAPKHO.SoPhieu OR PHIEUTC0=PHIEUNHAPKHO.SoPHieuDH)), 0)) AS ChiPhi,"
                sql &= "   (ISNULL((SELECT SUM (SoTien) FROM CHI WHERE MucDich =210 AND ( PhieuTC1=PHIEUNHAPKHO.SoPhieu OR PHIEUTC0=PHIEUNHAPKHO.SoPHieuDH)), 0) + "
                sql &= "   ISNULL((SELECT SUM (SoTien) FROM UNC WHERE MucDich =210 AND (PhieuTC1=PHIEUNHAPKHO.SoPhieu OR PHIEUTC0=PHIEUNHAPKHO.SoPHieuDH)), 0)) AS Chi,"
                sql &= "   PHIEUNHAPKHO.IDNguoiDat, LEFT(PHIEUNHAPKHO.Sophieu, 4) AS YearMonth,"
                sql &= "    PHIEUDATHANG.PheDuyet,NHANSU.Ten AS TakeCare,DEPATMENT.Ten AS Phong,PHIEUNHAPKHO.SoPhieuDH, '' AS CBCT,PHIEUDATHANG.IDHinhThucCT, "
                sql &= "    (CASE WHEN tbCT.SoTien is null then 0 ELSE (CASE WHEN tbCT.SoTien <> (PHIEUNHAPKHO.TienTruocThue + PHIEUNHAPKHO.TienThue) THEN 2 ELSE 1 END) END) AS TTCT,"
                sql &= "    tbVC.ID AS IDVC,tbVC.IDDVVC,tbVC.ThoiGian,tbVC.SoBill,tbVC.SoTien,tbVC.SoTienTC,tbVC.TienTe,tbVC.TyGia,tbVC.CanNang,tbVC.GhiChu,tbVC.SL,Convert(bit,0)Modify,NGUOINHAP.Ten As NguoiNhapCP"
                sql &= " FROM PHIEUNHAPKHO "
                sql &= " LEFT JOIN (SELECT Sum(SoTien)SoTien, SoPhieu1 FROM tblCongNo WHERE Loai=1 GROUP BY SoPhieu1)tbCT ON tbCT.SoPhieu1=PHIEUNHAPKHO.SoPhieu "

                ' sql &= " LEFT JOIN CHI ON CHI.PHIEUTC1 = PHIEUNHAPKHO.SoPhieu"
                sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUNHAPKHO.IDKhachhang"


                sql &= " LEFT JOIN PHIEUDATHANG ON PHIEUDATHANG.Sophieu = PHIEUNHAPKHO.SoPhieuDH "
                sql &= " LEFT JOIN NHANSU ON NHANSU.ID=PHIEUNHAPKHO.IDNguoiDat"
                If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND NHANSU.IDDepatment= " & cbPhong.EditValue
                End If
                sql &= " LEFT JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"
                sql &= " LEFT JOIN (SELECT * FROM "
                sql &= " ("
                sql &= "    SELECT *,"
                sql &= "          ROW_NUMBER() OVER (PARTITION BY PhieuTC ORDER BY ThoiGian DESC) AS STT,"
                sql &= " 		Count(PhieuTC) over(PARTITION BY PhieuTC) AS SL"
                sql &= "    FROM ChiPhi WHERE Loai=0"
                sql &= " )tb WHERE STT=1)tbVC ON tbVC.PhieuTC = PHIEUNHAPKHO.SoPhieu "
                sql &= " LEFT JOIN NHANSU as NGUOINHAP ON NGUOINHAP.ID=tbVC.IDUser"

                sql &= " WHERE CONVERT(datetime,Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) between @TuNgay AND @DenNgay "

                If Not cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND PHIEUNHAPKHO.IDNguoiDat =" & cbTakeCare.EditValue
                End If
                sql &= " ORDER BY PHIEUNHAPKHO.SoPhieu"

            Else
                colPhieuChi.Visible = True
                colPhieuChi.VisibleIndex = 1
                colUNC.Visible = True
                colUNC.VisibleIndex = 2
                sql &= " SELECT KHACHHANG.ttcMa,"
                sql &= "    tbChi.NgaythangCT AS Ngay, tbChi.SoPhieu,tbChi.Loai, PHIEUNHAPKHO.SoPhieuDH, PHIEUNHAPKHO.Sophieu AS SoPhieuNK,  "
                sql &= " 	PHIEUNHAPKHO.Tientruocthue * PHIEUNHAPKHO.Tygia AS TienTruocThue, PHIEUNHAPKHO.Ngaythang, "
                sql &= " 	PHIEUNHAPKHO.Tienthue * PHIEUNHAPKHO.Tygia AS TienThue,tbChi.Sotien AS TienChi,  "
                sql &= " 	PHIEUNHAPKHO.IDNguoiDat,NHANSU.Ten AS TakeCare,DEPATMENT.Ten AS Phong,PHIEUNHAPKHO.IDNguoiDat,'' AS CBCT,"
                sql &= "    (CASE WHEN tbCT.SoTien is null then 0 ELSE (CASE WHEN tbCT.SoTien <> (PHIEUNHAPKHO.TienTruocThue + PHIEUNHAPKHO.TienThue) THEN 2 ELSE 1 END) END) AS TTCT"
                sql &= " FROM  (SELECT NgayThangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1,convert(bit,0)Loai"
                sql &= "      FROM CHI"
                sql &= "      UNION ALL"
                sql &= "      SELECT NgayThang, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1,convert(bit,1)Loai"
                sql &= "      FROM UNC) AS tbChi "
                sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=tbChi.IDKH "
                sql &= " INNER JOIN PHIEUNHAPKHO ON tbChi.PhieuTC1 = PHIEUNHAPKHO.Sophieu OR tbChi.PhieuTC0=PHIEUNHAPKHO.SoPhieuDH "
                sql &= " LEFT JOIN (SELECT Sum(SoTien)SoTien, SoPhieu1 FROM tblCongNo WHERE Loai=1 GROUP BY SoPhieu1)tbCT ON tbCT.SoPhieu1=PHIEUNHAPKHO.SoPhieu "

                sql &= " INNER JOIN NHANSU ON PHIEUNHAPKHO.IDNguoiDat=NHANSU.ID "
                If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND NHANSU.IDDepatment= " & cbPhong.EditValue
                End If
                sql &= " LEFT JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"

                sql &= " WHERE CONVERT(datetime,Convert(nvarchar,tbChi.NgayThangCT,103),103) between @TuNgay AND @DenNgay"
                If Not cbTakeCare.EditValue Is Nothing Then
                    sql &= " AND PHIEUNHAPKHO.IDNguoiDat =" & cbTakeCare.EditValue
                End If
                sql &= " ORDER BY tbChi.SoPhieu"

            End If
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb
            rcbHinhThucCT.DataSource = tableHinhThucCT()
            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)

        AddParameterWhere("@SP", SoPhieu)
        Dim sql As String = ""
        sql &= " SELECT NHAPKHO.ID, NHAPKHO.IDVattu, ISNULL(NHAPKHO.AZ,row_number() over (order by NHAPKHO.ID)) AS AZ,NHAPKHO.SoPhieu, NHAPKHO.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,DonGia,ChiPhi,NhapThue,MucThue,SoHDGTGT,NgayHDGTGT,SoHoaDon, convert(bit,0) as Chon "
        sql &= " FROM NHAPKHO "
        sql &= " INNER JOIN VATTU ON NHAPKHO.IDVatTu=VATTU.ID"
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE NHAPKHO.SoPhieu=@SP ORDER BY AZ,NHAPKHO.ID"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvVT.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

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
        If e.Column.FieldName = "Chi" Then
            If (gdvCT.GetRowCellValue(e.RowHandle, "TienTruocThue") + gdvCT.GetRowCellValue(e.RowHandle, "TienThue")) <> e.CellValue Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "CBCT" Then
            If gdvCT.GetRowCellValue(e.RowHandle, "TTCT") = 0 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvCT.GetRowCellValue(e.RowHandle, "TTCT") = 2 Then
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

        'Kiểm tra đã đủ chứng từ hay chưa? Chưa thì bôi đỏ cột số phiếu nhập hàng
        If e.Column.FieldName = "SoPhieuNK" Then
            Dim str As String = ""
            Dim dt As New DataTable

            If Year(gdvCT.GetRowCellValue(e.RowHandle, "Ngaythang")) >= 2018 Then
                If gdvCT.GetRowCellValue(e.RowHandle, "IDHinhThucCT") = 2 And gdvCT.GetRowCellValue(e.RowHandle, "TienTruocThue") > 0 Then
                    str = "SELECT count(Id_CT) from NHAPKHO where Sophieu = @SOPHIEU"
                ElseIf gdvCT.GetRowCellValue(e.RowHandle, "IDHinhThucCT") = 3 And gdvCT.GetRowCellValue(e.RowHandle, "TienTruocThue") > 0 Then
                    str = " select count(idlamhaiquan) from HaiQuan_ChiTietLamHaiQuan ct inner join HaiQuan_LamHaiQuan on idlamhaiquan =HaiQuan_LamHaiQuan.id"
                    str &= " right join  NHAPKHO  on ct.idchaogia =NHAPKHO .IDDathang where NHAPKHO.Sophieu = tbl.Sophieu And NgayThongQuan Is Not null"
                End If
                If str <> "" Then
                    AddParameterWhere("@SOPHIEU", gdvCT.GetRowCellValue(e.RowHandle, "SoPhieuNK"))
                    dt = ExecuteSQLDataTable(str)
                    If dt Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    End If
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0)(0) = 0 Then
                            e.Appearance.BackColor = Color.Red
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        If cbTieuChi.EditValue = "Xuất kho" Then
            saveFile.FileName = "Loi nhuan theo nhap kho.xls"
        Else
            saveFile.FileName = "Loi nhuan theo phieu chi.xls"
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


    Private Sub btXemPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemPhieuChi.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim f As New frmCNChi2
        f.PhieuChi = gdvCT.GetFocusedRowCellValue("SoPhieu")
        f.UNC = gdvCT.GetFocusedRowCellValue("Loai")
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
            btXemPhieuChi.Visibility = XtraBars.BarItemVisibility.Never
        Else
            btXemPhieuChi.Visibility = XtraBars.BarItemVisibility.Always
        End If

        If colVCDVVC.Visible Then
            btLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mNhapTTVC.Caption = "Ẩn đề nghị chi vận chuyển"
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
            mNhapTTVC.Caption = "Đề nghị chi vận chuyển"
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
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNChaoGia.Tag = deskTop.mChaoGia.Name

        fCNChaoGia.Show()
    End Sub

    Private Sub btXemChiPhi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChiPhi.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT CHI.SoPhieu,Convert(bit,0)UNC,CHI.NgayThangCT AS NgayThang,DienGiai,SoTien,(CASE PhieuTC0 WHEN N'000000000' THEN '' ELSE PhieuTC0 END)AS PhieuTC0,(CASE PhieuTC1 WHEN N'000000000' THEN '' ELSE PhieuTC1 END)AS PhieuTC1,TamUng,"
        sql &= " KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich"
        sql &= " FROm CHI "
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=CHI.IDKH"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=CHI.MucDich"
        sql &= " WHERE ((CHI.ChiPhiNhap=1 AND CHi.MucDich =205) OR (CHi.MucDich = 208)) AND (CHI.PhieuTC0='" & gdvCT.GetFocusedRowCellValue("SoPhieuDH").ToString & "' OR CHI.PhieuTC1='" & gdvCT.GetFocusedRowCellValue("SoPhieuNK").ToString & "')"
        sql &= " UNION ALL"
        sql &= " SELECT UNC.SoPhieu,Convert(bit,0)UNC,UNC.NgayThang,DienGiai,SoTien,PhieuTC0,PhieuTC1,TamUng,"
        sql &= " KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich"
        sql &= " FROm UNC "
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=UNC.IDKH"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=UNC.MucDich"
        sql &= " WHERE ((UNC.ChiPhiNhap=1 AND UNC.MucDich =205) OR (UNC.MucDich = 208)) AND (UNC.PhieuTC0='" & gdvCT.GetFocusedRowCellValue("SoPhieuDH").ToString & "' OR UNC.PhieuTC1='" & gdvCT.GetFocusedRowCellValue("SoPhieuNK").ToString & "')"
        sql &= " ORDER BY NgayThang DESC"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
        Dim f As New frmTongHopChiPhi
        f.colPhieuTC0.Caption = "Phiếu ĐH"
        f.colPhieuTC1.Caption = "Phiếu NK"
        f.gdv.DataSource = tb
        f.ShowDialog()
    End Sub

    Private Sub btXemXK_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemNK.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        fCNNhapKho = New frmCNNhapKho
        fCNNhapKho.PhieuNK = gdvCT.GetFocusedRowCellValue("SoPhieuNK")
        fCNNhapKho.Tag = deskTop.mNhapKho.Name
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.QuyenSua) Then
            fCNNhapKho.btGhi.Enabled = False
            fCNNhapKho.btChuyenXK.Enabled = False
            fCNNhapKho.btCal.Enabled = False
            fCNNhapKho.btTichThue.Enabled = False
            fCNNhapKho.mChonBoChon.Enabled = False
        End If
        fCNNhapKho.ShowDialog()
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

    Private Sub mNhapTTVC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhapTTVC.ItemClick
        If mNhapTTVC.Caption = "Đề nghị chi vận chuyển" Then
            colVCDVVC.VisibleIndex = 4
            colVCThoiGian.VisibleIndex = 5
            colVCSoBill.VisibleIndex = 6

            colVCSoTien.VisibleIndex = 7
            colVCCanNang.VisibleIndex = 8
            colVCGhiChu.VisibleIndex = 9
            colVCTienTe.VisibleIndex = 10
            colVCTyGia.VisibleIndex = 11
            colVCNguoiNhap.VisibleIndex = 12
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
            If gdvCT.FocusedColumn Is colTrcThue Then
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
                AddParameter("@PhieuTC", gdvCT.GetRowCellValue(i, "SoPhieuNK"))
                AddParameter("@SoBill", gdvCT.GetRowCellValue(i, "SoBill"))
                AddParameter("@SoTien", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@SoTienTC", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@TienTe", gdvCT.GetRowCellValue(i, "TienTe"))
                AddParameter("@TyGia", gdvCT.GetRowCellValue(i, "TyGia"))
                AddParameter("@MucDich", 205)
                If Not IsDBNull(gdvCT.GetRowCellValue(i, "CanNang")) Then
                    AddParameter("@CanNang", gdvCT.GetRowCellValue(i, "CanNang"))
                End If

                AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", False)
                If IsDBNull(gdvCT.GetRowCellValue(i, "IDVC")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        ShowBaoLoi("Không thêm được chi phí tại PN: " & gdvCT.GetRowCellValue(i, "SoPhieuNK") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "IDVC"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        ShowBaoLoi("Không cập nhật được chi phí tại PN: " & gdvCT.GetRowCellValue(i, "SoPhieuNK") & vbCrLf & LoiNgoaiLe)
                    End If
                End If
            End If
        Next

        gdvCT.EndUpdate()
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick, mLuuLai.ItemClick
        LuuLai()
    End Sub
#End Region

    Public Function LayDataSourceDSCP() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("SoPhieuNK", Type.GetType("System.String"))
        tb.Columns.Add("CanNang", Type.GetType("System.Double"))
        tb.Columns.Add("GhiChu", Type.GetType("System.String"))
        tb.Columns.Add("TienTruocThue", Type.GetType("System.Double"))
        tb.Columns.Add("ChiPhi", Type.GetType("System.Double"))
        tb.Columns.Add("ID", Type.GetType("System.Object"))
        tb.Columns.Add("IDNguoiDat", Type.GetType("System.Object"))
        Return tb
    End Function

    Private Sub mThemChiPhiMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemChiPhiMoi.ItemClick
        If ShowCauHoi("Thêm chi phí cho nhập kho " & gdvCT.GetFocusedRowCellValue("SoPhieuNK") & " ?") Then
            gdvCT.SetFocusedRowCellValue("IDDVVC", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("ThoiGian", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("SoBill", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("SoTien", 0)
            gdvCT.SetFocusedRowCellValue("TienTe", 0)
            gdvCT.SetFocusedRowCellValue("TyGia", 1)
            gdvCT.SetFocusedRowCellValue("CanNang", 0)
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
            If gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK") = gdvCT.GetFocusedRowCellValue("SoPhieuNK") Then
                ShowCanhBao("Số phiếu NK đã có sẵn trong danh sách!")
                Exit Sub
            End If
        Next
        gdvDSCPCT.AddNewRow()
        gdvDSCPCT.SetFocusedRowCellValue("SoPhieuNK", gdvCT.GetFocusedRowCellValue("SoPhieuNK"))
        gdvDSCPCT.SetFocusedRowCellValue("TienTruocThue", gdvCT.GetFocusedRowCellValue("TienTruocThue"))
        gdvDSCPCT.SetFocusedRowCellValue("CanNang", 0)
        gdvDSCPCT.SetFocusedRowCellValue("ChiPhi", gdvCT.GetFocusedRowCellValue("ChiPhi"))
        gdvDSCPCT.SetFocusedRowCellValue("IDNguoiDat", gdvCT.GetFocusedRowCellValue("IDNguoiDat"))
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
            ShowCanhBao("Chưa có thông tin phiếu nhập!")
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
                AddParameter("@PhieuTC", gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK"))
                AddParameter("@SoBill", tbSoBill.EditValue)
                AddParameter("@SoTien", gdvDSCPCT.GetRowCellValue(i, "ChiPhi"))
                AddParameter("@SoTienTC", tbSoTien.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@TyGia", tbTyGia.EditValue)
                AddParameter("@CanNang", gdvDSCPCT.GetRowCellValue(i, "CanNang"))
                AddParameter("@GhiChu", gdvDSCPCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", False)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvDSCPCT.GetRowCellValue(i, "ID")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvDSCPCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        Throw New Exception("Không thêm được chi phí tại PN: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvDSCPCT.GetRowCellValue(i, "ID"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        Throw New Exception("Không cập nhật được chi phí tại PN: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK") & vbCrLf & LoiNgoaiLe)
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
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT PhieuTC as SoPhieuNK,CanNang,GhiChu,ChiPhi.ID,SoTien As ChiPhi,PHIEUNHAPKHO.TienTruocThue*PHIEUNHAPKHO.TyGIa as TienTruocThue FROM ChiPhi INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=ChiPhi.PhieuTC WHERE Loai=0 AND IDDVVC=@IDVC AND SoTienTC=@STTC AND SoBill=@SB AND ThoiGian=@TG")
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

    Private Sub gdvCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieuNK"))
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown

        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub btPhanBoChiPhiNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btPhanBoChiPhiNhap.ItemClick
        Dim f As New frmPhanBoChiPhiNhap
        f.ShowDialog()
    End Sub

End Class
