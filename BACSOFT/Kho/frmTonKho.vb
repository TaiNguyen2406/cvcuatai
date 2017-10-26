Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl

Public Class frmTonKho
    Public _Exit As Boolean = False

    Private Sub frmTonKho_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
            tabTonTH.PageVisible = False
            colGiaNhap.Visible = False
            colThanhTien.Visible = False
            btXuatExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        Try
            tbNgayBan.EditValue = New DateTime(Today.Date.Year - 1, Today.Date.Month, Today.Date.Day)
        Catch ex As Exception
            tbNgayBan.EditValue = New DateTime(Today.Date.Year - 1, Today.Date.Month, Today.Date.Day - 1)
        End Try
    End Sub

#Region "Lọc vật tư"

    Private Sub loadDSTenVT(ByVal HangSX As Object, ByVal NhomVT As Object)
        Dim sqltb As String = ""

        Dim sql As String = ""
        If HangSX Is Nothing And NhomVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTenvattu FROM TENVATTU WHERE ID=-1 "

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbTenVatTu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhomVT(ByVal HangSX As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If HangSX Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTennhom FROM TENNHOM WHERE ID=-1"

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbNhomVT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbHangSX(ByVal NhomVT As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If NhomVT Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub cbTenVatTu_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTenVatTu.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTenVT.EditValue = Nothing
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            btFilterHangSX.EditValue = Nothing
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub rtbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub rtbThongSo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThongSo.ButtonClick
        btFilterThongSo.EditValue = Nothing
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)

    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)

    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)

    End Sub

#End Region

    Private Sub LoadDS()
        Dim sql As String = ""
        ShowWaiting("Đang tải dữ liệu tồn kho ...")
        sql &= " SELECT  TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,V_TonKhoTongHop.IDVatTu, VATTU.Model,"
        sql &= " round(V_TonkhoTonghop.Ton,2)Ton, TENDONVITINH.Ten AS DVT, VATTU.ThongSo,VATTU.HangTon, TENNUOC.Ten AS TenNuoc, V_TonkhoTonghop.GiaNhap,"
        sql &= " (V_TonkhoTonghop.Ton*V_TonkhoTonghop.GiaNhap)ThanhTien,VATTU.KhoiLuong1"
        Sql &= " FROM  V_TonkhoTonghop "
        Sql &= " INNER JOIN VATTU ON V_TonkhoTonghop.IDVattu = VATTU.ID"
        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh=TENDONVITINH.ID"
        sql &= " LEFT JOIN TENNUOC ON VATTU.IDTenNuoc=TENNUOC.ID"
        sql &= " WHERE 1=1 "

        If chkChiHienConTon.Checked Then
            sql &= " AND V_TonkhoTonghop.Ton >0 "
        End If

        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        sql &= " ORDER BY TenVT,TenHang,Model"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        CloseWaiting()
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadDS()
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT ton kho " & Today.Date.ToString("dd-MM-yyyy") & ".xls"
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

   
    Private Sub btXem_Click(sender As System.Object, e As System.EventArgs) Handles btXem.Click
        Dim sql As String = ""
        ShowWaiting("Đang tải dữ liệu tồn kho ...")
        sql &= " SELECT TENHANGSANXUAT.Ten AS TenHang,tb.* FROM "
        sql &= " (SELECT IDHangSanXuat, SUM(Ton) AS SoLuong, SUM(Ton * Gianhap) AS TongTien,SUM((CASE HangTon WHEN 1 THEN Ton*GiaNhap ELSE 0 END ))TienTonLau"
        sql &= " FROM  V_F_Tonkho"
        sql &= " GROUP BY IDHangSanxuat)tb"
        sql &= " INNER JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=tb.IDHangSanXuat"
        sql &= " ORDER BY TongTien DESC"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        CloseWaiting()
        If Not tb Is Nothing Then
            gdvTongHop.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT ton kho theo hang " & Today.Date.ToString("dd-MM-yyyy") & ".xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvTongHopCT, False)
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

    Private Sub mXemChiTiet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChiTiet.ItemClick
        btFilterNhomVT.EditValue = Nothing
        btfilterTenVT.EditValue = Nothing
        btFilterHangSX.EditValue = gdvTongHopCT.GetFocusedRowCellValue("IDHangSanXuat")
        btFilterMaVT.EditValue = Nothing
        btFilterThongSo.EditValue = Nothing
        btTaiLai.PerformClick()
        XtraTabControl1.SelectedTabPage = XtraTabPage1
    End Sub

    Private Sub btLichSuXuatNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLichSuXuatNhap.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        AddParameterWhere("@IDVatTu", gdvCT.GetFocusedRowCellValue("IDVatTu"))
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT KHACHHANG.ttcMa,PHIEUNHAPKHO.NgayThang,NHAPKHO.SoPhieu,VATTU.Model,SoLuong,"
        sql &= " 		(Dongia * PHIEUNHAPKHO.Tygia)DonGia,NHAPKHO.ChiPhi,(Dongia * PHIEUNHAPKHO.Tygia * SoLuong)ThanhTien,"
        sql &= " 		NGUOIDAT.Ten AS TakeCare,NGUOINHAP.Ten AS NguoiNhapKho"
        sql &= " FROM NHAPKHO "
        sql &= " 	INNER JOIN PHIEUNHAPKHO ON NHAPKHO.Sophieu=PHIEUNHAPKHO.Sophieu"
        sql &= " 	LEFT JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"
        sql &= "     INNER JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID=NHAPKHO.IDVatTu"
        sql &= " 	LEFT JOIN NHANSU AS NGUOIDAT ON NGUOIDAT.ID=PHIEUDATHANG.IDTakeCare"
        sql &= " 	LEFT JOIN NHANSU AS NGUOINHAP ON NGUOINHAP.ID=PHIEUNHAPKHO.IDUSer"
        sql &= " WHERE NHAPKHO.IDVattu=@IDVatTu"
        sql &= " ORDER BY PHIEUNHAPKHO.Ngaythang DESC"

        sql &= " SELECT KHACHHANG.ttcMa,PHIEUXUATKHO.NgayThang,PHIEUXUATKHO.CongTrinh,XUATKHO.SoPhieu,VATTU.Model,SoLuong,"
        sql &= " 		(Dongia * PHIEUXUATKHO.Tygia)DonGia,(Dongia * PHIEUXUATKHO.Tygia * SoLuong)ThanhTien,"
        sql &= " 		TAKECARE.Ten AS TakeCare,NGUOIXUAT.Ten AS NguoiXuatKho"
        sql &= " FROM XUATKHO "
        sql &= " 	INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu"
        sql &= "     INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu"
        sql &= " 	LEFT JOIN NHANSU AS TAKECARE ON TAKECARE.ID=PHIEUXUATKHO.IDTakeCare"
        sql &= " 	LEFT JOIN NHANSU AS NGUOIXUAT ON NGUOIXUAT.ID=PHIEUXUATKHO.IDUSer"
        sql &= " WHERE XUATKHO.IDVattu=@IDVatTu"
        sql &= " ORDER BY PHIEUXUATKHO.Ngaythang DESC"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmLichSuNhapXuat
            f.gdvNhap.DataSource = ds.Tables(0)
            f.gdvXuat.DataSource = ds.Tables(1)
            f.tbTonKho.EditValue = gdvCT.GetFocusedRowCellValue("Ton")
            f.tbTienTonKho.EditValue = gdvCT.GetFocusedRowCellValue("ThanhTien")
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btCNKhoBan_Click(sender As System.Object, e As System.EventArgs) Handles btCNKhoBan.Click
        If ShowCauHoi("Cập nhật hàng tồn kho khó bán ?") Then
            Dim sql As String = ""
            sql &= " SET DATEFORMAT DMY"
            sql &= "  UPDATE VATTU SET HangTon=0"
            sql &= "  UPDATE VATTU SET HangTon=1 "
            sql &= "  WHERE ID NOT IN ("
            sql &= "  SELECT DISTINCT IDVatTu FROM"
            sql &= "  (SELECT  IDVatTu "
            sql &= "  FROM XUATKHO "
            sql &= "  INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            sql &= "  WHERE PHIEUXUATKHO.NgayThang> '" & tbNgayBan.EditValue & "'"
            sql &= "  UNION ALL"
            sql &= "  SELECT  IDVatTu "
            sql &= "  FROM NHAPKHO "
            sql &= "  INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
            sql &= "  WHERE PHIEUNHAPKHO.NgayThang> '" & tbNgayBan.EditValue & "')tb"
            sql &= " )"
            sql &= " AND ID IN ("
            sql &= " SELECT ID FROM "
            sql &= " (SELECT ID,((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID))slTon"
            sql &= " FROM VATTU)tb1 WHERE slTon <>0)"

            ShowWaiting("Đang cập nhật ...")
            If ExecuteSQLDataTable(sql) Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            Else
                CloseWaiting()
                ShowThongBao("Đã cập nhật thành công !")
            End If
        End If
       
    End Sub

    Private Sub chkChiHienConTon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkChiHienConTon.CheckedChanged
        If chkChiHienConTon.Checked Then
            chkChiHienConTon.Glyph = My.Resources.Checked
        Else
            chkChiHienConTon.Glyph = My.Resources.UnCheck
        End If
    End Sub
End Class
