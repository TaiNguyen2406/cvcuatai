Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmLoiNhuanNhanVienPT

    Private Sub frmLoiNhuanNhanVien_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        LoadPhongBan()
        LoadTakeCare()
        'If tb.Rows.Count > 0 Then
        'cbTakeCare.EditValue = TaiKhoan
        LoadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btChotSoLieu.Visibility = XtraBars.BarItemVisibility.Never
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

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""

            sql &= " SELECT PhatTrienSanPham_LoiNhuan.SoPhieu,PHIEUXUATKHO.CongTrinh,VATTU.Model,TenHangSanXuat.Ten as HangSX,"
            sql &= " PhatTrienSanPham_LoiNhuan.SoLuong,PhatTrienSanPham_LoiNhuan.GiaBan,PhatTrienSanPham_LoiNhuan.GiaNhap,PhatTrienSanPham_LoiNhuan.LoiNhuan,(PhatTrienSanPham_LoiNhuan.SoLuong * PhatTrienSanPham_LoiNhuan.LoiNhuan) as TongLN,NhanSu.Ten as TenNV "
            sql &= " FROm PhatTrienSanPham_LoiNhuan"
            sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=PhatTrienSanPham_LoiNhuan.SoPhieu"
            sql &= " INNER JOIN VATTU ON VATTU.ID=PhatTrienSanPham_LoiNhuan.IDVatTu"
            sql &= " INNER JOIn TenHangSanXuat ON VATTU.IDHangSanXuat=TenHangSanXuat.ID"
            sql &= " INNEr JOIN NHANSU ON NHANSU.ID=PhatTrienSanPham_LoiNhuan.IdNhanVien"
            If cbTakeCare.EditValue Is Nothing Then
                If Not cbPhong.EditValue Is Nothing Then
                    AddParameterWhere("@IDPhong", cbPhong.EditValue)
                    sql &= " AND NHANSU.IDDepatment =@IDPhong"
                End If
            Else
                AddParameterWhere("@IDNV", cbTakeCare.EditValue)
                sql &= " AND NHANSU.ID =@IDNV"
            End If
            sql &= " WHERE LEFT(PhatTrienSanPham_loiNhuan.SoPhieu,4)=@Thang"

            AddParameterWhere("@Thang", Convert.ToDateTime(tbTuNgay.EditValue).ToString("yyMM"))

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

    Private Sub rcbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakeCare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakeCare.EditValue = Nothing
        End If
    End Sub


    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Loi nhuan theo nhan vien phat trien.xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
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

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub

    Private Sub btChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChotSL.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Dim sql As String = ""
        sql &= " UPDATE tblTHChamCong SET LoiNhuan1=tb.TongLN"
        sql &= " FROM tblTHChamCong,"
        sql &= " (SELECT IDNhanVien,SUM(SoLuong * LoiNhuan) as TongLN"
        sql &= "  FROm PhatTrienSanPham_LoiNhuan"
        sql &= " WHERE LEFT(PhatTrienSanPham_loiNhuan.SoPhieu,4)=@ThangSP"
        sql &= " GROUP BY IDNhanVien)tb"
        sql &= " WHERE tblTHChamCong.IDNhanVien=tb.IDNhanVien AND tblTHChamCong.[Month]=@Thang"
        AddParameterWhere("@Thang", Convert.ToDateTime(tbTuNgay.EditValue).ToString("MM/yyyy"))
        AddParameterWhere("@ThangSP", Convert.ToDateTime(tbTuNgay.EditValue).ToString("yyMM"))
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật!")
        End If
        'CloseWaiting()
    End Sub

    Private Sub btTinhLoiNhuan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhLoiNhuan.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub

        If ShowCauHoi("Cập nhật lợi nhuận NVPT tháng " & Convert.ToDateTime(tbTuNgay.EditValue).ToString("MM/yyyy")) Then
            ShowWaiting("Cập nhật lợi nhuận nhân viên phát triển...")
            Dim sql As String = ""
            sql = " DELETE FROM PhatTrienSanPham_LoiNhuan WHERE left(SoPhieu,4) = '" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("yyMM") & "'"

            sql &= " INSERT INTO PhatTrienSanPham_LoiNhuan(SoPhieu,IDVatTu,SoLuong,GiaBan,GiaNhap,LoiNhuan,IDNhanVien)"
            sql &= " SELECT XUATKHO.SoPhieu,XUATKHO.IDVatTu,XUATKHO.SoLuong,XUATKHO.DonGia,XUATKHO.GiaNhap,"
            sql &= " Round((CASE PHIEUXUATKHO.CongTrinh WHEN 0 "
            sql &= " THEN (XUATKHO.DonGia-ISNULL(XUATKHO.GiaNhap,0) - (XUATKHO.DonGia/PHIEUXUATKHO.TienTruocThue)*"
            sql &= " (ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0))- "
            sql &= " (ISNULL(CHAOGIA.ChietKhau,0) - ISNULL(CHAOGIA.ChietKhau,0)*ISNULL(BANGCHAOGIA.KhauTru,0)/100)/(1-ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)) )"
            sql &= " * PhatTrienSanPham.PTLoiNhuanTM/100"
            sql &= " ELSE  XUATKHO.GiaNhap*PTLoiNhuanCT/100 END),2)LoiNhuan,PhatTrienSanPham.IDPhuTrach"
            sql &= " FROM XUATKHO"
            sql &= " LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
            sql &= " LEFT JOIN BANGCHAOGIA ON CHAOGIA.SoPhieu=BANGCHAOGIA.SoPhieu"
            sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            sql &= " AND Right(Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),7)=@Thang "
            sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu  "
            sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
            sql &= " INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu "
            sql &= " INNER JOIN PhatTrienSanPham ON VATTU.IDHangSanXuat=PhatTrienSanPham.IDHangSX"
            sql &= " AND PhatTrienSanPham.Thang=@Thang "
            sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) "
            sql &= " AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "

            AddParameterWhere("@Thang", Convert.ToDateTime(tbTuNgay.EditValue).ToString("MM/yyyy"))
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
            CloseWaiting()

        End If
    End Sub
End Class
