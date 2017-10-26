Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports BACSOFT.Utils
Imports BACSOFT.HoaDonGTGT
Imports BACSOFT.TAI
Imports System.Linq

Public Class frmYeuCauDi_DatHang
    Dim _exit As Boolean = False

    Private Sub frmYeuCauDi_DatHang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TrangThaiDH.isAddNew = True
        tbTuNgayTH.EditValue = DateAdd(DateInterval.Month, -6, New Date(Today.Year, Today.Month, 1))
        tbDenNgayTH.EditValue = Today.Date
        tbTuNgay.EditValue = New Date(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        tbNgayChot.EditValue = Today.Date
        tbTuNgay.Enabled = False
        tbDenNgay.Enabled = False
        loadLoaiDH()
        LoadDataFilterVT()
        gdvListFileDH.DataSource = DataSourceDSFile()
        loadCbHinhThucTT()
        loadTienTe()
        loadKhachHang()
        loadTakeCare()
        cbTieuChi.EditValue = "Top 500"
        loadDSTongHopVatTuDatHang()
        loadDSVatTuDatHang(-1)
        chkHienThongSo.Checked = False
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btNhanVien.Enabled = False
        End If

    End Sub


#Region "Các sự kiên liên quan tới các cb lọc dữ liệu"

    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            cbTienTe.Properties.DataSource = tb
            If cbTienTe.EditValue Is Nothing Then
                cbTienTe.EditValue = Convert.ToByte(TienTe.VND)
            End If
            cbTienTeNK.Properties.DataSource = tb
            If cbTienTeNK.EditValue Is Nothing Then
                cbTienTeNK.EditValue = Convert.ToByte(TienTe.USD)
            End If
            rcbTienTeDH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadLoaiDH()
        Dim sql As String = " SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=" & LoaiTuDien.LoaiDatHang & " ORDER BY Ma"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            cbLoaiDH.Properties.DataSource = dt
            If dt.Rows.Count > 0 Then
                cbLoaiDH.EditValue = dt.Rows(0)(0)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadKhachHang()
        Dim sql As String = " SELECT ID,ttcMa,Ten,IDTakeCare,IDHinhThucTT FROM KHACHHANG WHERE ttcKhachhang <> 1 "
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            sql &= " AND IDTakeCare=@IDNV"
            AddParameterWhere("@IDNV", TaiKhoan)
        End If
        sql &= " ORDER BY ttcMa"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvNhaCCDatHang.Properties.DataSource = dt
            rcbNCC.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDataFilterVT()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbTenVT1.Properties.DataSource = ds.Tables(0)
            cbNhomVT.Properties.DataSource = ds.Tables(1)
            cbHangSX.Properties.DataSource = ds.Tables(2)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub loadDSTenVT(ByVal _HangSX As Object, ByVal _NhomVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If _HangSX Is Nothing And _NhomVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTenvattu FROM TENVATTU WHERE ID=-1 "
            If Not _HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDHangSanxuat=" & _HangSX
            End If

            If Not _NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDTennhom=" & _NhomVT
            End If
            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbTenVT1.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhomVT(ByVal _HangSX As Object, ByVal _TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If _HangSX Is Nothing And _TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTennhom FROM TENNHOM WHERE ID=-1"

            If Not _HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & _HangSX
            End If

            If Not _TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT  IDTennhom FROM VATTU WHERE IDTenvattu=" & _TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            cbNhomVT.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbHangSX(ByVal _NhomVT As Object, ByVal _TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If _NhomVT Is Nothing And _TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Else
            sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not _NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & _NhomVT
            End If

            If Not _TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & _TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbHangSX.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rcbTenVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTenVT1.ButtonClick
        If e.Button.Index = 1 Then
            cbTenVT1.EditValue = Nothing
            loadDSTenVT(cbHangSX.EditValue, cbNhomVT.EditValue)
        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            cbHangSX.EditValue = Nothing
            If cbTenVT1.SelectedText.ToString.Trim = "" Then
                LoadcbHangSX(cbNhomVT.EditValue, Nothing)
            Else
                LoadcbHangSX(cbNhomVT.EditValue, cbTenVT1.EditValue)
            End If

        End If
    End Sub

    Private Sub rcbMaVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbMaVT.Properties.ButtonClick
        tbMaVT.EditValue = Nothing
    End Sub

    Private Sub rcbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            cbNhomVT.EditValue = Nothing
            If cbTenVT1.SelectedText.ToString.Trim = "" Then
                LoadDSNhomVT(cbHangSX.EditValue, Nothing)
            Else
                LoadDSNhomVT(cbHangSX.EditValue, cbTenVT1.EditValue)
            End If

        End If
    End Sub

    Private Sub tbThongSo_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbThongSo.Properties.ButtonClick
        tbThongSo.EditValue = Nothing
    End Sub

    Private Sub cbNhomVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNhomVT.EditValueChanged
        loadDSTenVT(cbHangSX.EditValue, cbNhomVT.EditValue)
        LoadcbHangSX(cbNhomVT.EditValue, cbTenVT1.EditValue)
    End Sub

    Private Sub cbTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTenVT1.EditValueChanged
        LoadcbHangSX(cbNhomVT.EditValue, cbTenVT1.EditValue)
        LoadDSNhomVT(cbHangSX.EditValue, cbTenVT1.EditValue)
    End Sub

    Private Sub cbHangSX_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbHangSX.EditValueChanged
        loadDSTenVT(cbHangSX.EditValue, cbNhomVT.EditValue)
        LoadDSNhomVT(cbHangSX.EditValue, cbTenVT1.EditValue)
    End Sub

    Public Sub loadTakeCare()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 ")
        If Not tb Is Nothing Then
            cbTakeCareDatHang.Properties.DataSource = tb
            cbNguoiLap.Properties.DataSource = tb
            cbNguoiLap.EditValue = Convert.ToInt32(TaiKhoan)
            rCbNhanVien.DataSource = tb
            btNhanVien.EditValue = Convert.ToInt32(TaiKhoan)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadCbNgd(ByVal cbNgd As LookUpEdit, ByVal IDKhachHang As Object)
        AddParameterWhere("@MaNCC", IDKhachHang)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,ChamSoc as IDTakeCare FROM NHANSU WHERE Noictac=@MaNCC order by Ten")
        If Not tb Is Nothing Then
            cbNgd.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadCbHinhThucTT()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,HinhThucTT_VIE FROM tblHinhThucTTKH")
        If Not tb Is Nothing Then
            cbHinhThucTT.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbHinhThucTT.EditValue = tb.Rows(0)("ID")
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

#Region "Tổng hợp"
    Private Sub LoadDSTongHop()
        ShowWaiting("Đang tải DS hàng hóa ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        Dim sqlWhere As String = " WHERE 1=1 "
        Dim sqlOrder As String = ""
        sql &= " SELECT NULL AS CanhBao,TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model AS MaVT,TENNHOM.Ten_ENG AS TenNhom_ENG,VATTU.HinhAnh,(convert(image,NULL))HienThi"
        sql &= " ,SLNhap=(select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanNhap= (select isnull(count(IDvattu),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLXuat=(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanXuat= (select isnull(count(IDvattu),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')"
        sql &= " ,XuatMax=(select isnull(Max(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,VATTU.DatToithieu AS DatToiThieu, VATTU.DMTon"
        sql &= " ,SLTon=((select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID) -(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID))"
        sql &= " ,CanNhap=(select isnull(SUM(Cannhap),0) from DATHANG where IDVattu=Vattu.ID) "
        sql &= " ,Convert(float,0)LuongDat "
        sql &= " ,KHSD=ISNULL(tbKHSD.KHSD,0)"
        sql &= " ,CanXuat=(select isnull(SUM(CanXuat),0) from CHAOGIA where CHAOGIA.IDVattu=Vattu.ID) "
        sql &= " ,ChaoGia=(select isnull(SUM(Soluong),0) from CHAOGIA inner join BANGCHAOGIA ON CHAOGIA.SOPHIEU = BANGCHAOGIA.SOPHIEU where IDVattu=Vattu.ID and CHAOGIA.Trangthai <> 2 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,YeuCau=(select isnull(SUM(Soluong),0) from YEUCAUDEN INNER JOIN BANGYEUCAU ON YEUCAUDEN.SOPHIEU = BANGYEUCAU.SOPHIEU where IDVattu=Vattu.ID and BANGYEUCAU.Trangthai <=3 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' ) "
        sql &= " ,VATTU.Ton,VATTU.TonNCC,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS TenDVT, VATTU.Thongso AS ThongSo, VATTU.IDTennuoc,TENNUOC.TEN AS NuocSX, VATTU.ID AS IDVatTu, VATTU.Thongdung AS ThongDung, VATTU.MaLoi, VATTU.HangTon,VATTU.TaiLieu, "
        sql &= " VATTU.Mucthue1 AS MucThue,VATTU.Xuatthue1 AS NhapThue,VATTU.Tiente1 AS TienTeDG,VATTU.Gianhap1 AS GiaNhapDG,VATTU.Mucthue1 AS MucThueDG,VATTU.Xuatthue1 AS NhapThueDG,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " VATTU.Khoiluong1 AS KhoiLuong,tblTienTe.TyGia,VATTU.GiaNK,VATTU.TNK1,VATTU.TienTeNK,VATTU.ConSX,ISNULL(VATTU.SLMOQ1,0)SLMOQ1,ISNULL(VATTU.SLMOQ2,0)SLMOQ2,ISNULL(VATTU.SLMOQ3,0)SLMOQ3,"
        sql &= " ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = VATTU.ID ORDER BY SoPhieu DESC),0) GiaNhap"
        sql &= " FROM VATTU"
        sql &= " LEFT JOIN TENVATTU on VATTU.IDTenvattu=TENVATTU.ID  "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh"
        sql &= " LEFT JOIN TENNHOM ON TENNHOM.ID=VATTU.IDTennhom"
        sql &= " LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= " LEFT JOIN "
        sql &= " (SELECT * FROM (SELECT COUNT(IDKhachHang)KHSD,IDVatTu"
        sql &= " FROM (SELECT DISTINCT PHIEUXUATKHO.IDKhachHang,XUATKHO.IDVatTu "
        sql &= " 	FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu WHERE PHIEUXUATKHO.NgayThang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')tb"
        sql &= " GROUP BY IDVatTu)tb2)tbKHSD ON tbKHSD.IDVatTu=VATTU.ID "

        If Not cbNhomVT.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDTennhom =" & cbNhomVT.EditValue
        If Not cbTenVT1.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDTenvattu =" & cbTenVT1.EditValue
        If Not cbHangSX.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDHangsanxuat =" & cbHangSX.EditValue
        If Not tbMaVT.EditValue Is Nothing Then sqlWhere &= " AND VATTU.Model like '" & tbMaVT.EditValue & "%'"
        If Not tbThongSo.EditValue Is Nothing Then sqlWhere &= " AND VATTU.Thongso like '%" & tbThongSo.EditValue & "%'"

        If rdbCtyNhap.Checked Then sql &= " inner join (SELECT DISTINCT IDVatTu FROM V_NHAPKHO)tbCTNhap on tbCTNhap.IDVattu=Vattu.ID "
        If rdbKHMua.Checked Then sql &= " inner join (SELECT DISTINCT IDVatTu FROM V_XUATKHO)tbKHMua ON tbKHMua.IDVatTu=Vattu.ID "
        If rdbKHSuDung.Checked Then sql &= " inner join (SELECT DISTINCT IDVatTu FROM KHVATTUSUDUNG)tbKHSD on VATTU.ID =tbKHSD.IDvattu "
        If rdbKHYeuCau.Checked Then sql &= " inner join (SELECT DISTINCT IDVatTu FROM YEUCAUDEN)tbYCDen on VATTU.ID =tbYCDen.IDvattu "
        If rdbThongDung.Checked Then sqlWhere &= " AND VATTU.Thongdung = 1 "
        If rdbCanTon.Checked Then sqlWhere &= " AND VATTU.Ton = 1 "

        If chkMaLoi.Checked Then sqlWhere &= " AND VATTU.MaLoi=" & Convert.ToInt16(chkMaLoi.Checked)
        If chkKhongSX.Checked Then sqlWhere &= " AND VATTU.ConSX =" & Convert.ToInt16(Not chkKhongSX.Checked)

        sql &= sqlWhere
        sql &= " ORDER BY TenVT,HangSX,MaVT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                colTHHinhAnh.Visible = True
                With tb

                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                ' .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next

                End With
            Else
                colTHHinhAnh.Visible = False
            End If

            gdvTongHop.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub btTimKiem_Click(sender As System.Object, e As System.EventArgs) Handles btTimKiem.Click
        LoadDSTongHop()
    End Sub

    Private Sub btThucHien_Click(sender As System.Object, e As System.EventArgs) Handles btThucHien.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If tbIDCu.EditValue Is Nothing Then
            ShowCanhBao("Chưa nhập ID cần thay thế !")
            Exit Sub
        End If
        If tbIDMoi.EditValue Is Nothing Then
            ShowCanhBao("Chưa nhập ID dùng để thay thế !")
            Exit Sub
        End If

        If ShowCauHoi("Lưu ý thao tác này không thể quay lại, bạn có chắc chắn muốn thay thế hay không ?") Then
            Dim sql As String = " Update NHAPKHO Set IDVattu = " & tbIDMoi.EditValue & " Where IDVattu = " & tbIDCu.EditValue
            sql &= " Update XUATKHO Set IDVattu = " & tbIDMoi.EditValue & " Where IDVattu = " & tbIDCu.EditValue
            sql &= " Update CHAOGIA Set IDVattu = " & tbIDMoi.EditValue & " Where IDVattu = " & tbIDCu.EditValue
            sql &= " Update DATHANG Set IDVattu = " & tbIDMoi.EditValue & " Where IDVattu = " & tbIDCu.EditValue
            sql &= " Update YEUCAUDEN Set IDVattu = " & tbIDMoi.EditValue & " Where IDVattu = " & tbIDCu.EditValue
            sql &= " DELETE VATTU  WHERE ID = " & tbIDCu.EditValue
            BeginTransaction()
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                RollBackTransaction()
                ShowBaoLoi(LoiNgoaiLe)
            End If
            ComitTransaction()
            tbIDMoi.EditValue = ""
            tbIDCu.EditValue = ""
            ShowAlert("Đã thay mã thành công !")
        End If

    End Sub

    Private Sub gdvTongHopCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvTongHopCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvTongHopCT.OptionsView.ShowAutoFilterRow = Not gdvTongHopCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

    Private Sub btDatHang_Click(sender As System.Object, e As System.EventArgs) Handles btDatHang.Click
        If tbSoPhieuDH.EditValue.ToString.Trim <> "" Then
            If Not ShowCauHoi("Bạn đang thêm mặt hàng cho đặt hàng DH " & gdvNhaCCDatHang.Text & " " & tbSoPhieuDH.EditValue) Then
                Exit Sub
            End If
        End If

        Dim _count As Integer = 0
        gdvTongHopCT.CloseEditor()
        gdvTongHopCT.UpdateCurrentRow()
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvTongHopCT.RowCount - 1
            If gdvTongHopCT.GetRowCellValue(i, "LuongDat") > 0 Then
                Dim _TonTai As Boolean = False
                With gdvDatHangCT
                    For j As Integer = 0 To .RowCount - 1
                        If .GetRowCellValue(j, "IDVatTu") = gdvTongHopCT.GetRowCellValue(i, "IDVatTu") Then
                            If ShowCauHoi("Mã: " & gdvTongHopCT.GetRowCellValue(i, "MaVT") & " đã có sẵn bạn có muốn thay thế số lượng hay không ?") Then
                                .SetRowCellValue(j, "SoLuong", gdvTongHopCT.GetRowCellValue(i, "LuongDat"))
                                _TonTai = True
                            End If
                        End If
                    Next
                End With
                If Not _TonTai Then
                    Try
                        gdvDatHangCT.AddNewRow()
                        Try
                            gdvDatHangCT.SetFocusedRowCellValue("AZ", gdvDatHangCT.GetRowCellValue(gdvDatHangCT.RowCount - 2, "AZ") + 1)
                        Catch ex As Exception
                            gdvDatHangCT.SetFocusedRowCellValue("AZ", 1)
                        End Try

                        gdvDatHangCT.SetFocusedRowCellValue("IDVatTu", gdvTongHopCT.GetRowCellValue(i, "IDVatTu"))
                        gdvDatHangCT.SetFocusedRowCellValue("TenVT", gdvTongHopCT.GetRowCellValue(i, "TenVT"))
                        gdvDatHangCT.SetFocusedRowCellValue("HangSX", gdvTongHopCT.GetRowCellValue(i, "HangSX"))
                        gdvDatHangCT.SetFocusedRowCellValue("Model", gdvTongHopCT.GetRowCellValue(i, "MaVT"))
                        gdvDatHangCT.SetFocusedRowCellValue("DVT", gdvTongHopCT.GetRowCellValue(i, "TenDVT"))
                        gdvDatHangCT.SetFocusedRowCellValue("SoLuong", gdvTongHopCT.GetRowCellValue(i, "LuongDat"))
                        gdvDatHangCT.SetFocusedRowCellValue("DonGia", 0.0)
                        gdvDatHangCT.SetFocusedRowCellValue("GiaNhapPT", 0.0)
                        gdvDatHangCT.SetFocusedRowCellValue("ThanhTien", 0.0)
                        gdvDatHangCT.SetFocusedRowCellValue("ThongSo", gdvTongHopCT.GetRowCellValue(i, "ThongSo"))
                        gdvDatHangCT.SetFocusedRowCellValue("MucThue", gdvTongHopCT.GetRowCellValue(i, "MucThue"))
                        gdvDatHangCT.SetFocusedRowCellValue("NhapThue", gdvTongHopCT.GetRowCellValue(i, "NhapThue"))
                        gdvDatHangCT.SetFocusedRowCellValue("GiaList", gdvTongHopCT.GetRowCellValue(i, "GiaList"))
                        gdvDatHangCT.SetFocusedRowCellValue("TienTe", gdvTongHopCT.GetRowCellValue(i, "TienTeDG"))
                        gdvDatHangCT.SetFocusedRowCellValue("GiaNhapDG", gdvTongHopCT.GetRowCellValue(i, "GiaNhapDG"))
                        gdvDatHangCT.SetFocusedRowCellValue("MucThueDG", gdvTongHopCT.GetRowCellValue(i, "MucThueDG"))
                        gdvDatHangCT.SetFocusedRowCellValue("NhapThueDG", gdvTongHopCT.GetRowCellValue(i, "NhapThueDG"))
                        gdvDatHangCT.SetFocusedRowCellValue("TyGia", gdvTongHopCT.GetRowCellValue(i, "TyGia"))
                        gdvDatHangCT.SetFocusedRowCellValue("GiaNK", gdvTongHopCT.GetRowCellValue(i, "GiaNK"))
                        gdvDatHangCT.SetFocusedRowCellValue("FOB", 0)
                        gdvDatHangCT.SetFocusedRowCellValue("TienTeNK", gdvTongHopCT.GetRowCellValue(i, "TienTeNK"))
                        gdvDatHangCT.SetFocusedRowCellValue("TNK1", gdvTongHopCT.GetRowCellValue(i, "TNK1"))
                        gdvDatHangCT.SetFocusedRowCellValue("TNK", gdvTongHopCT.GetRowCellValue(i, "TNK1"))
                        gdvDatHangCT.SetFocusedRowCellValue("DMTon", gdvTongHopCT.GetRowCellValue(i, "DMTon"))
                        gdvDatHangCT.SetFocusedRowCellValue("NgayVe", tbNgayNhanDatHang.EditValue)
                        gdvDatHangCT.SetFocusedRowCellValue("NgayVe2", tbNgayNhanDatHang.EditValue)
                        gdvDatHangCT.SetFocusedRowCellValue("CO", False)
                        gdvDatHangCT.SetFocusedRowCellValue("CQ", False)
                        gdvDatHangCT.CloseEditor()
                        gdvDatHangCT.UpdateCurrentRow()
                        '====================
                        gdvTongHopCT.SetRowCellValue(i, "LuongDat", 0)

                        _count += 1
                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try

                End If
            End If
        Next
        gdvTongHopCT.CloseEditor()
        gdvTongHopCT.UpdateCurrentRow()
        gdvDatHangCT.EndUpdate()
        If _count > 0 Then
            ShowAlert("Đã chuyển hàng hóa sang bảng đặt hàng !")
        End If

        'ShowThongBao(_count)

    End Sub

    Private Function addRowGdvDathang(ByVal IDVatTu As Object, ByVal SoLuong As Object) As Boolean
        With gdvDatHangCT
            For i As Integer = 0 To .RowCount - 1
                If .GetRowCellValue(i, "IDVatTu") = IDVatTu Then
                    .SetRowCellValue(i, "SoLuong", .GetRowCellValue(i, "SoLuong") + SoLuong)
                    Return True
                End If
            Next
        End With

        Dim sql As String = ""
        sql &= " Select TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,TENDONVITINH.Ten AS DVT,VATTU.ThongSo, "
        sql &= "     VATTU.Mucthue1 AS MucThue,VATTU.Xuatthue1 AS NhapThue,"
        sql &= "     (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= "     DATHANG.ID AS IDDatHang,VATTU.Tiente1 AS TienTeDG,VATTU.Gianhap1 AS GiaNhapDG,VATTU.Mucthue1 AS MucThueDG,VATTU.Xuatthue1 AS NhapThueDG,"
        sql &= "     VATTU.Khoiluong1 AS KhoiLuong,tblTienTe.TyGia,VATTU.GiaNK,VATTU.TNK1,VATTU.TienTeNK,VATTU.DMTon"
        sql &= " from VATTU"
        sql &= "     LEFT JOIN DATHANG on DATHANG.IDvattu =VATTU.ID "
        sql &= "     LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= "     LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= "     LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "     LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        sql &= " WHERE VATTU.ID=@IDVattu"
        AddParameterWhere("@IDVattu", IDVatTu)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            'CType(gdvDatHang.Views.Item(0).DataSource, DataView).Table.Merge(tb)
            gdvDatHangCT.AddNewRow()
            gdvDatHangCT.SetFocusedRowCellValue("IDVatTu", IDVatTu)
            gdvDatHangCT.SetFocusedRowCellValue("TenVT", tb.Rows(0)("TenVT"))
            gdvDatHangCT.SetFocusedRowCellValue("HangSX", tb.Rows(0)("HangSX"))
            gdvDatHangCT.SetFocusedRowCellValue("Model", tb.Rows(0)("Model"))
            gdvDatHangCT.SetFocusedRowCellValue("DVT", tb.Rows(0)("DVT"))
            gdvDatHangCT.SetFocusedRowCellValue("SoLuong", SoLuong)
            gdvDatHangCT.SetFocusedRowCellValue("DonGia", 0.0)
            gdvDatHangCT.SetFocusedRowCellValue("GiaNhapPT", 0.0)
            gdvDatHangCT.SetFocusedRowCellValue("ThanhTien", 0.0)
            gdvDatHangCT.SetFocusedRowCellValue("ThongSo", tb.Rows(0)("ThongSo"))
            gdvDatHangCT.SetFocusedRowCellValue("MucThue", tb.Rows(0)("MucThue"))
            gdvDatHangCT.SetFocusedRowCellValue("NhapThue", tb.Rows(0)("NhapThue"))
            gdvDatHangCT.SetFocusedRowCellValue("GiaList", tb.Rows(0)("GiaList"))
            gdvDatHangCT.SetFocusedRowCellValue("TienTe", tb.Rows(0)("TienTeDG"))
            gdvDatHangCT.SetFocusedRowCellValue("GiaNhapDG", tb.Rows(0)("GiaNhapDG"))
            gdvDatHangCT.SetFocusedRowCellValue("MucThueDG", tb.Rows(0)("MucThueDG"))
            gdvDatHangCT.SetFocusedRowCellValue("NhapThueDG", tb.Rows(0)("NhapThueDG"))
            gdvDatHangCT.SetFocusedRowCellValue("TyGia", tb.Rows(0)("TyGia"))
            gdvDatHangCT.SetFocusedRowCellValue("GiaNK", tb.Rows(0)("GiaNK"))
            gdvDatHangCT.SetFocusedRowCellValue("FOB", 0)
            gdvDatHangCT.SetFocusedRowCellValue("TienTeNK", tb.Rows(0)("TienTeNK"))
            gdvDatHangCT.SetFocusedRowCellValue("TNK1", tb.Rows(0)("TNK1"))
            gdvDatHangCT.SetFocusedRowCellValue("TNK", tb.Rows(0)("TNK1"))
            gdvDatHangCT.SetFocusedRowCellValue("DMTon", tb.Rows(0)("DMTon"))
            gdvDatHangCT.SetFocusedRowCellValue("NgayVe", tbNgayNhanDatHang.EditValue)
            gdvDatHangCT.SetFocusedRowCellValue("NgayVe2", tbNgayNhanDatHang.EditValue)
            gdvDatHangCT.SetFocusedRowCellValue("CO", False)
            gdvDatHangCT.SetFocusedRowCellValue("CQ", False)
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btTinhLuongDat_Click(sender As System.Object, e As System.EventArgs) Handles btTinhLuongDat.Click
        ShowWaiting("Đang tính lượng hàng cần đặt ...")
        'Dim _tmp As Integer
        tbChiPhi.EditValue = 0
        gdvTongHopCT.BeginUpdate()
        With gdvTongHopCT
            For i As Integer = 0 To .RowCount - 1
                If Not .GetRowCellValue(i, "Ton") Then
                    If .GetRowCellValue(i, "CanXuat") > 0 Then
                        If .GetRowCellValue(i, "CanXuat") > .GetRowCellValue(i, "SLTon") + .GetRowCellValue(i, "CanNhap") Then
                            .SetRowCellValue(i, "LuongDat", .GetRowCellValue(i, "CanXuat") - .GetRowCellValue(i, "SLTon") - .GetRowCellValue(i, "CanNhap"))
                        End If

                    End If
                    '_tmp = .GetRowCellValue(i, "DMTon") + .GetRowCellValue(i, "CanXuat")
                Else
                    If .GetRowCellValue(i, "CanXuat") <= 0 Then
                        If .GetRowCellValue(i, "SLTon") + .GetRowCellValue(i, "CanNhap") < .GetRowCellValue(i, "DMTon") Then
                            .SetRowCellValue(i, "LuongDat", .GetRowCellValue(i, "DatToiThieu"))
                        End If
                    Else
                        If .GetRowCellValue(i, "SLTon") + .GetRowCellValue(i, "CanNhap") - .GetRowCellValue(i, "CanXuat") < .GetRowCellValue(i, "DMTon") Then
                            If .GetRowCellValue(i, "CanXuat") + .GetRowCellValue(i, "DatToiThieu") - .GetRowCellValue(i, "SLTon") - .GetRowCellValue(i, "CanNhap") <= .GetRowCellValue(i, "DatToiThieu") Then
                                .SetRowCellValue(i, "LuongDat", .GetRowCellValue(i, "DatToiThieu"))
                            Else
                                .SetRowCellValue(i, "LuongDat", .GetRowCellValue(i, "CanXuat") + .GetRowCellValue(i, "DatToiThieu") - .GetRowCellValue(i, "SLTon") - .GetRowCellValue(i, "CanNhap"))
                            End If
                        End If
                        '    _tmp = .GetRowCellValue(i, "CanXuat") Then
                        'If _tmp > 0 Then
                        '    If .GetRowCellValue(i, "DatToiThieu") = 1 Then
                        '        .SetRowCellValue(i, "LuongDat", _tmp - .GetRowCellValue(i, "SLTon") - .GetRowCellValue(i, "CanNhap"))
                        '    Else
                        '        If (_tmp - .GetRowCellValue(i, "SLTon") - .GetRowCellValue(i, "CanNhap")) / .GetRowCellValue(i, "DatToiThieu") >= 0.4 Then
                        '            .SetRowCellValue(i, "LuongDat", Math.Round((_tmp - .GetRowCellValue(i, "SLTon") - .GetRowCellValue(i, "CanNhap")) / .GetRowCellValue(i, "DatToiThieu"), 0) * .GetRowCellValue(i, "DatToiThieu"))
                        '        Else
                        '            .SetRowCellValue(i, "LuongDat", 0)
                        '        End If
                        '    End If
                        'If .GetRowCellValue(i, "LuongDat") < 0 Then .SetRowCellValue(i, "LuongDat", 0)
                        'End If
                    End If
                End If
                If .GetRowCellValue(i, "LuongDat") > 0 Then tbChiPhi.EditValue += .GetRowCellValue(i, "LuongDat") * .GetRowCellValue(i, "GiaNhap")
            Next
        End With
        gdvTongHopCT.EndUpdate()

        CloseWaiting()
        ShowAlert("Đã tính xong !")

    End Sub

    Private Sub btLocMaConTon_Click(sender As System.Object, e As System.EventArgs) Handles btLocMaConTon.Click

        ShowWaiting("Đang lọc còn tồn ...")
        gdvTongHopCT.BeginUpdate()
        With gdvTongHopCT
            Dim i As Integer = 0
            While i < .RowCount
                If .GetRowCellValue(i, "SLTon") = 0 Then
                    .DeleteRow(i)
                Else
                    i += 1
                End If
            End While

        End With
        gdvTongHopCT.EndUpdate()
        CloseWaiting()

    End Sub

    Private Sub btLocMaLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btLocMaLienQuan.Click
        ShowWaiting("Đang lọc mã liên quan ...")
        gdvTongHopCT.BeginUpdate()
        With gdvTongHopCT
            Dim i As Integer = 0
            While i < .RowCount
                If .GetRowCellValue(i, "SLNhap") = 0 And .GetRowCellValue(i, "SLXuat") = 0 And .GetRowCellValue(i, "SLTon") = 0 And .GetRowCellValue(i, "CanXuat") = 0 And .GetRowCellValue(i, "CanNhap") = 0 And .GetRowCellValue(i, "LuongDat") = 0 Then
                    .DeleteRow(i)
                Else
                    i += 1
                End If
            End While
        End With
        gdvTongHopCT.EndUpdate()
        CloseWaiting()
    End Sub

    Private Sub btLuuCanTon_Click(sender As System.Object, e As System.EventArgs) Handles btLuuCanTon.Click, btLuuDatToiThieu.Click, btLuuDinhMucTon.Click, btLuuMaLoi.Click, btLuuThongDung.Click, btLuuKhoBan.Click, btLuuConSX.Click
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        CapNhatSoLieu(CType(CType(sender, SimpleButton).Tag, Integer))
    End Sub

    Public Sub CapNhatSoLieu(ByVal Index As Integer)
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        With gdvTongHopCT
            Dim Sql As String = ""
            Select Case Index
                Case 0 ' Ton
                    Dim _Ton As Int16
                    For i As Integer = 0 To .RowCount - 1
                        If .GetRowCellValue(i, "Ton") Then _Ton = 1 Else _Ton = 0
                        Sql = "Update VATTU Set Ton = " & _Ton & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                        If ExecuteSQLNonQuery(Sql) Is Nothing Then
                            ShowBaoLoi("Cập nhật tồn lỗi:" & LoiNgoaiLe)
                        End If
                    Next
                Case 1 ' Dinh muc ton
                    For i As Integer = 0 To .RowCount - 1
                        If IsNumeric(.GetRowCellValue(i, "DMTon")) Then
                            Sql = "Update VATTU Set DMTon = " & .GetRowCellValue(i, "DMTon") & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                            If ExecuteSQLNonQuery(Sql) Is Nothing Then
                                ShowBaoLoi("Cập nhật ĐM tồn lỗi:" & LoiNgoaiLe)
                            End If
                        End If
                    Next
                Case 2 ' Dat toi thieu
                    For i As Integer = 0 To .RowCount - 1
                        Sql = "Update VATTU Set DatToithieu = " & .GetRowCellValue(i, "DatToiThieu") & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                        If ExecuteSQLNonQuery(Sql) Is Nothing Then
                            ShowBaoLoi("Cập nhật đặt tối thiểu lỗi:" & LoiNgoaiLe)
                        End If
                    Next
                Case 3 ' Thong dung
                    Dim _ThongDung As Int16
                    For i As Integer = 0 To .RowCount - 1
                        If .GetRowCellValue(i, "ThongDung") Then _ThongDung = 1 Else _ThongDung = 0
                        Sql = "Update VATTU Set Thongdung = " & _ThongDung & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                        If ExecuteSQLNonQuery(Sql) Is Nothing Then
                            ShowBaoLoi("Cập nhật đặt tối thiểu lỗi:" & LoiNgoaiLe)
                        End If
                    Next
                Case 4 ' Ma loi
                    Dim _Maloi As Int16
                    For i As Integer = 0 To .RowCount - 1
                        If .GetRowCellValue(i, "MaLoi") Then _Maloi = 1 Else _Maloi = 0
                        Sql = "Update VATTU Set Maloi = " & _Maloi & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                        If ExecuteSQLNonQuery(Sql) Is Nothing Then
                            ShowBaoLoi("Cập nhật mã lỗi không thành công:" & LoiNgoaiLe)
                        End If
                    Next

                Case 5 ' Khó bán
                    Dim _KhoBan As Int16
                    For i As Integer = 0 To .RowCount - 1
                        If .GetRowCellValue(i, "HangTon") Then _KhoBan = 1 Else _KhoBan = 0
                        Sql = "Update VATTU Set HangTon = " & _KhoBan & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                        If ExecuteSQLNonQuery(Sql) Is Nothing Then
                            ShowBaoLoi("Cập nhật hàng khó bán không thành công:" & LoiNgoaiLe)
                        End If
                    Next
                Case 6 ' Còn SX
                    Dim _ConSX As Int16
                    For i As Integer = 0 To .RowCount - 1
                        If .GetRowCellValue(i, "ConSX") Then _ConSX = 1 Else _ConSX = 0
                        Sql = "Update VATTU Set ConSX = " & _ConSX & " Where ID = " & .GetRowCellValue(i, "IDVatTu")
                        If ExecuteSQLNonQuery(Sql) Is Nothing Then
                            ShowBaoLoi("Cập nhật hàng còn SX không thành công:" & LoiNgoaiLe)
                        End If
                    Next
            End Select
        End With
        ShowAlert("Đã cập nhật !")
    End Sub

#End Region

#Region "Đặt hàng"

    Private Sub gdvNhaCCDatHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvNhaCCDatHang.EditValueChanged
        On Error Resume Next
        loadCbNgd(cbDaiDienDatHang, gdvNhaCCDatHang.EditValue)

        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbHinhThucTT.EditValue = dr("IDHinhThucTT")
        cbTakeCareDatHang.EditValue = dr("IDTakecare")
    End Sub

    Private Sub gdvNhaCCDatHang_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvNhaCCDatHang.KeyPress
        If gdvNhaCCDatHang.IsPopupOpen Then
            Exit Sub
        Else
            gdvNhaCCDatHang.ShowPopup()
        End If
    End Sub

    Private Sub gdvNhaCCDatHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvNhaCCDatHang.ButtonClick
        If e.Button.Index = 1 Then
            gdvNhaCCDatHang.EditValue = Nothing
        End If
    End Sub

    Private Sub cbTakeCareDatHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTakeCareDatHang.ButtonClick, cbNguoiLap.ButtonClick
        If e.Button.Index = 1 Then
            cbTakeCareDatHang.EditValue = Nothing
        End If
    End Sub

    Private Sub cbDaiDienDatHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbDaiDienDatHang.ButtonClick
        If e.Button.Index = 1 Then
            cbDaiDienDatHang.EditValue = Nothing
        End If
    End Sub

    Private Sub cbTienTe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTienTe.EditValueChanged
        On Error Resume Next
        If cbTienTe.EditValue Is Nothing Then Exit Sub
        Dim edit As LookUpEdit = CType(sender, LookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        tbTyGia.EditValue = dr("TyGia")
    End Sub

    Private Sub btXuatExcelDatHang_ShowDropDownControl(sender As System.Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs) Handles btXuatExcelDatHang.ShowDropDownControl
        If cbTienTe.EditValue = TienTe.VND Then
            chkVIE.Checked = True
            chkN0.Checked = True
        Else
            chkENG.Checked = True
            chkN2.Checked = True
        End If
        If tbSoPhieuDH.Tag.ToString.Trim <> "" Then
            chkDHOmron.Checked = True
        Else
            chkDHOmron.Checked = False
        End If
    End Sub

    Private Sub tbTienTruocThue_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTienTruocThue.EditValueChanged, tbTienThue.EditValueChanged
        tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
    End Sub

    Private Sub btTichCO_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTichCO.ItemClick
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        Dim check As Boolean = Not gdvDatHangCT.GetRowCellValue(0, "CO")
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            gdvDatHangCT.SetRowCellValue(i, "CO", check)
        Next
        gdvDatHangCT.EndUpdate()
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
    End Sub

    Private Sub btTichCQ_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTichCQ.ItemClick
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        Dim check As Boolean = Not gdvDatHangCT.GetRowCellValue(0, "CQ")
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            gdvDatHangCT.SetRowCellValue(i, "CQ", check)
        Next
        gdvDatHangCT.EndUpdate()
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
    End Sub

    Private Sub btXuatETH_Click(sender As System.Object, e As System.EventArgs) Handles btXuatETH.Click
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If chkDatHangOmron.Checked Then
            '   XuatExcel.CreateExcelFileDatHangOmron(gdvCT.GetFocusedRowCellValue("SoPhieu"), gdvCT.GetFocusedRowCellValue("NgayDat"), gdvCT.GetFocusedRowCellValue("MaKH"))
        Else
            '   XuatExcel.CreateExcelFileDatHang(gdvCT.GetFocusedRowCellValue("SoPhieu"), chkXuatHangVTTH.Checked, chkXuatMaVTTH.Checked, chkXuatTSTH.Checked, chkXuatCOTH.Checked, chkVIETH.Checked, chkN0TH.Checked, chkTHNgayVe.Checked, "", gdvCT.GetFocusedRowCellValue("LoaiDH"))

        End If
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT PheDuyet FROM PHIEUDATHANG WHERE SoPhieu='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'")
        If Not tb Is Nothing Then
            If tb.Rows(0)(0) = True Then
                If Not gdvCT.GetFocusedRowCellValue("LoaiDH") = 2 And Not gdvCT.GetFocusedRowCellValue("IDTakecare") = Convert.ToInt32(TaiKhoan) Then
                    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                        ShowCanhBao("Bạn không có quyền sửa đặt hàng đã duyệt !")
                        Exit Sub
                    End If
                End If
            End If
            TrangThaiDH.isUpdate = True
            loadDSVatTuDatHang(gdvCT.GetFocusedRowCellValue("SoPhieu"), gdvCT.GetFocusedRowCellValue("LoaiDH"))
            tabYeuCauDi_DatHang.SelectedTabPage = tabDatHang
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btFileKemDatHang_Click(sender As System.Object, e As System.EventArgs) Handles btFileKemDatHang.Click
        gFileDinhKemDH.Visible = Not gFileDinhKemDH.Visible
    End Sub

    Private Sub btThemFileDH_Click(sender As System.Object, e As System.EventArgs) Handles btThemFileDH.Click
        If tbSoPhieuDH.EditValue = "" Then
            ShowCanhBao("Cần phải lưu lại để có số đặt hàng trước khi chọn file đính kèm")
            Exit Sub
        End If
        If gdvNhaCCDatHang.EditValue.ToString = "" Then
            ShowCanhBao("Chưa chọn nhà cung cấp !")
            Exit Sub
        End If

        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(tbNgayDatHang.EditValue).Year.ToString & "\" & UrlDatHang & gdvNhaCCDatHang.Text) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(tbNgayDatHang.EditValue).Year.ToString & "\" & UrlDatHang & gdvNhaCCDatHang.Text)
            End If
            For Each file In openFile.FileNames
                Try
                    path = "DH" & tbSoPhieuDH.EditValue & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)

                    System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(tbNgayDatHang.EditValue).Year.ToString & "\" & UrlDatHang & gdvNhaCCDatHang.Text & "\" & path)
                    gdvListFileDHCT.AddNewRow()
                    gdvListFileDHCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            Impersonator.EndImpersonation()
            CloseWaiting()
            gdvListFileDHCT.CloseEditor()
            gdvListFileDHCT.UpdateCurrentRow()

        End If
    End Sub

    Private Sub btXoaFileDH_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFileDH.Click
        If gdvListFileDHCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        Try
            If ShowCauHoi("Xoá file được chọn ?") Then
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(tbNgayDatHang.EditValue).Year.ToString & "\" & UrlDatHang & gdvNhaCCDatHang.Text & "\" & gdvListFileDHCT.GetFocusedRowCellValue("File"))
                gdvListFileDHCT.DeleteSelectedRows()
                Impersonator.EndImpersonation()
            End If

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btTinhTienDatHang_Click(sender As System.Object, e As System.EventArgs) Handles btTinhTienDatHang.Click
        gdvDatHangCT.ClearColumnsFilter()
        If cbLoaiDH.EditValue = 2 Then
            TinhToanLuongVTVe()
            Exit Sub
        End If

        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        gdvDatHangCT.BeginUpdate()
        _exit = True
        If cbLoaiDH.EditValue = 1 Then
            For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                Dim _GiaFOB As Double = 0
                If gdvDatHangCT.GetRowCellValue(i, "FOB") = 0 And gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") = 0 Then
                    gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", gdvDatHangCT.GetRowCellValue(i, "GiaNhapDG"))

                    _GiaFOB = gdvDatHangCT.GetRowCellValue(i, "GiaNK")

                    If chkLamTron.Checked Then
                        gdvDatHangCT.SetRowCellValue(i, "FOB", Math.Round(_GiaFOB, 2))
                    Else
                        gdvDatHangCT.SetRowCellValue(i, "FOB", _GiaFOB)
                    End If

                ElseIf gdvDatHangCT.GetRowCellValue(i, "FOB") = 0 And gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") <> 0 Then

                    _GiaFOB = gdvDatHangCT.GetRowCellValue(i, "GiaNK") * (gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") / 100)

                    If chkLamTron.Checked Then
                        gdvDatHangCT.SetRowCellValue(i, "FOB", Math.Round(_GiaFOB, 2))
                    Else
                        gdvDatHangCT.SetRowCellValue(i, "FOB", _GiaFOB)
                    End If

                Else
                    If gdvDatHangCT.GetRowCellValue(i, "GiaNK") = 0 Then
                        gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", 0)
                    Else
                        gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", Math.Round((gdvDatHangCT.GetRowCellValue(i, "FOB") / gdvDatHangCT.GetRowCellValue(i, "GiaNK")) * 100, 2))
                    End If
                End If

                ' gdvDatHangCT.SetRowCellValue(i, "TienTe", cbTienTe.EditValue)
            Next
        Else
            For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                Dim _DonGia As Double = 0
                If gdvDatHangCT.GetRowCellValue(i, "DonGia") = 0 And gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") = 0 Then
                    gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", gdvDatHangCT.GetRowCellValue(i, "GiaNhapDG"))
                    ' If Convert.ToByte(gdvVTCT.GetRowCellValue(i, "TienTe")) <> cbTienTe.EditValue Then
                    _DonGia = gdvDatHangCT.GetRowCellValue(i, "GiaList") * (gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") / 100)
                    _DonGia *= gdvDatHangCT.GetRowCellValue(i, "TyGia")
                    If cbTienTe.EditValue > TienTe.VND Then
                        _DonGia /= tbTyGia.EditValue
                    End If
                    'Else
                    '     _DonGia = gdvVTCT.GetRowCellValue(i, "GiaList") * (gdvVTCT.GetRowCellValue(i, "GiaBanPT") / 100)
                    'End If
                    If chkLamTron.Checked Then
                        If cbTienTe.EditValue = 0 Then
                            gdvDatHangCT.SetRowCellValue(i, "DonGia", Math.Ceiling(_DonGia / 100) * 100)
                        Else
                            gdvDatHangCT.SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))
                        End If
                    Else
                        gdvDatHangCT.SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))
                    End If


                ElseIf gdvDatHangCT.GetRowCellValue(i, "DonGia") = 0 And gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") <> 0 Then
                    'If Convert.ToByte(gdvVTCT.GetRowCellValue(i, "TienTe")) <> cbTienTe.EditValue Then
                    _DonGia = gdvDatHangCT.GetRowCellValue(i, "GiaList") * (gdvDatHangCT.GetRowCellValue(i, "GiaNhapPT") / 100)
                    _DonGia *= gdvDatHangCT.GetRowCellValue(i, "TyGia")
                    If cbTienTe.EditValue > TienTe.VND Then
                        _DonGia /= tbTyGia.EditValue
                    End If
                    'Else
                    ' _DonGia = gdvVTCT.GetRowCellValue(i, "GiaList") * (gdvVTCT.GetRowCellValue(i, "GiaBanPT") / 100)
                    ' End If
                    If chkLamTron.Checked Then
                        If cbTienTe.EditValue = 0 Then
                            gdvDatHangCT.SetRowCellValue(i, "DonGia", Math.Ceiling(_DonGia / 100) * 100)
                        Else
                            gdvDatHangCT.SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))
                        End If
                    Else
                        gdvDatHangCT.SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))
                    End If

                Else
                    If gdvDatHangCT.GetRowCellValue(i, "GiaList") = 0 Then
                        gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", 0)
                    Else
                        'gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", Math.Round((gdvDatHangCT.GetRowCellValue(i, "DonGia") / gdvDatHangCT.GetRowCellValue(i, "GiaList")) * 100, 2))
                        If Convert.ToInt32(gdvDatHangCT.GetRowCellValue(i, "TienTe")) > Convert.ToInt32(cbTienTe.EditValue) Then
                            gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", Math.Round((gdvDatHangCT.GetRowCellValue(i, "DonGia") / (gdvDatHangCT.GetRowCellValue(i, "GiaList") * gdvDatHangCT.GetRowCellValue(i, "TyGia") / tbTyGia.EditValue)) * 100, 2))
                        Else
                            gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", Math.Round((gdvDatHangCT.GetRowCellValue(i, "DonGia") / gdvDatHangCT.GetRowCellValue(i, "GiaList")) * 100, 2))
                        End If
                    End If
                End If

                'gdvDatHangCT.SetRowCellValue(i, "TienTe", cbTienTe.EditValue)
            Next
        End If

        _exit = False
        gdvDatHangCT.EndUpdate()
        TinhTienDatHang()
    End Sub

    Private Sub gdvDatHangCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvDatHangCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "GiaNhapPT"
                If Not IsNumeric(e.Value) Then
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "GiaNhapPT", 0)
                End If
                If _exit Then Exit Select
                _exit = True
                If cbLoaiDH.EditValue = 1 Then
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "FOB", 0)
                Else
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "DonGia", 0)
                End If

                _exit = False
            Case "DonGia"
                If cbLoaiDH.EditValue <> 1 Then
                    If Not IsNumeric(e.Value) Then
                        gdvDatHangCT.SetRowCellValue(e.RowHandle, "DonGia", 0)
                    End If
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvDatHangCT.GetRowCellValue(e.RowHandle, "DonGia"), 2))

                    If _exit Then Exit Select
                    _exit = True
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "GiaNhapPT", 0)
                    _exit = False
                End If
            Case "FOB"
                If cbLoaiDH.EditValue = 1 Then
                    If Not IsNumeric(e.Value) Then
                        gdvDatHangCT.SetRowCellValue(e.RowHandle, "FOB", 0)
                    End If
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvDatHangCT.GetRowCellValue(e.RowHandle, "FOB"), 2))

                    If _exit Then Exit Select
                    _exit = True
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "GiaNhapPT", 0)
                    _exit = False
                End If
                TinhFoB()
            Case "SoLuong"
                If Not IsNumeric(e.Value) Then
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "SoLuong", 0)
                End If
                If cbLoaiDH.EditValue = 1 Then
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvDatHangCT.GetRowCellValue(e.RowHandle, "FOB"), 2))
                Else
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvDatHangCT.GetRowCellValue(e.RowHandle, "DonGia"), 2))
                End If
                TinhFoB()
            Case "SLVe"
                If Not IsNumeric(e.Value) Then
                    gdvDatHangCT.SetRowCellValue(e.RowHandle, "SLVe", 0)
                End If
                If cbLoaiDH.EditValue = 2 Then
                    If e.Value > gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong") Then
                        ShowCanhBao("SL lớn hơn đã đặt !")
                        gdvDatHangCT.SetRowCellValue(e.RowHandle, "SLVe", gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong"))
                    End If
                End If
                TinhFoB()

        End Select
    End Sub

    Public Sub TinhTienDatHang()
        tbTienTruocThue.EditValue = 0
        tbTienThue.EditValue = 0
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            tbTienTruocThue.EditValue += gdvDatHangCT.GetRowCellValue(i, "ThanhTien")
            If gdvDatHangCT.GetRowCellValue(i, "NhapThue") Then
                tbTienThue.EditValue += (gdvDatHangCT.GetRowCellValue(i, "MucThue") / 100) * gdvDatHangCT.GetRowCellValue(i, "ThanhTien")
            End If
        Next
        tbTienSauThue.EditValue = tbTienTruocThue.EditValue + tbTienThue.EditValue
    End Sub

    Private Sub btTaoMoiDatHang_Click(sender As System.Object, e As System.EventArgs) Handles btTaoMoiDatHang.Click
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        TrangThaiDH.isAddNew = True
        loadDSVatTuDatHang(-1)
        tbSoPhieuDH.Tag = ""
    End Sub

    Private Sub btSuaDatHang_Click(sender As System.Object, e As System.EventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If chkDuyetDatHang.Checked Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        End If
        TrangThai.isUpdate = True
        tbNgayDatHang.Properties.ReadOnly = False
        tbNgayNhanDatHang.Properties.ReadOnly = False
        gdvDatHangCT.OptionsBehavior.ReadOnly = False
        gdvDatHangCT.OptionsBehavior.Editable = True
        chkDuyetDatHang.Properties.ReadOnly = False
        btTinhTienDatHang.Enabled = True
        btLuuDatHang.Enabled = True
    End Sub

    Function KiemTraDuKienThanhToan() As Boolean
        AddParameterWhere("@SP", tbSoPhieuDH.EditValue)
        AddParameterWhere("@PhaiTra", True)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT sum(SoTien) FROM tblCongNo WHERE Loai=@PhaiTra AND SoPhieu1=@SP")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If IsDBNull(tb.Rows(0)(0)) Then Return False
                If tb.Rows(0)(0) = tbTienSauThue.EditValue Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Sub btLuuDatHang_Click(sender As System.Object, e As System.EventArgs) Handles btLuuDatHang.Click
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If
        If cbLoaiDH.EditValue = 2 Then
            luuDHNhapKhau()
            Exit Sub
        End If




        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileDHCT.RowCount - 1
            _File &= gdvListFileDHCT.GetRowCellValue(i, "File")
            If i < gdvListFileDHCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next

        btTinhTienDatHang.PerformClick()

        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        If gdvDatHangCT.RowCount = 0 Then
            ShowCanhBao("Chưa có thông tin đặt hàng !")
            Exit Sub
        End If
        'Dim _SoPhieuDatHang As Object
        '_SoPhieuDatHang = tbSoPhieuDH.EditValue
        Dim _SoPhieuDHO As String = ""

        If TrangThaiDH.isAddNew Or TrangThaiDH.isCopy Then
            tbSoPhieuDH.EditValue = LaySoPhieu("PHIEUDATHANG")
            If gdvNhaCCDatHang.EditValue = 6874 Then
                _SoPhieuDHO = LaySoPhieuOmron(tbNgayDatHang.EditValue)
            End If
        End If

        Try
            BeginTransaction()
            AddParameter("@Sophieu", tbSoPhieuDH.EditValue)
            AddParameter("@Ngaydat", tbNgayDatHang.EditValue)
            AddParameter("@Ngaynhan", tbNgayNhanDatHang.EditValue)
            AddParameter("@IDKhachhang", gdvNhaCCDatHang.EditValue)
            AddParameter("@IDNgd", cbDaiDienDatHang.EditValue)
            AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
            AddParameter("@IDtakecare", cbTakeCareDatHang.EditValue)
            AddParameter("@tienTruocthue", tbTienTruocThue.EditValue)
            AddParameter("@tienthue", tbTienThue.EditValue)
            AddParameter("@Tiente", cbTienTe.EditValue)
            AddParameter("@Tygia", tbTyGia.EditValue)
            AddParameter("@IDHinhThucTT", cbHinhThucTT.EditValue)
            AddParameter("@FileDinhKem", _File)
            AddParameter("@LoaiDH", cbLoaiDH.EditValue)


            If TrangThaiDH.isAddNew Or TrangThaiDH.isCopy Then
                If gdvNhaCCDatHang.EditValue = 6874 Then
                    AddParameter("@SoPhieuO", _SoPhieuDHO)
                    tbSoPhieuDH.Tag = _SoPhieuDHO
                End If

                If doInsert("PHIEUDATHANG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@SP", tbSoPhieuDH.EditValue)
                If doUpdate("PHIEUDATHANG", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            With gdvDatHangCT
                For i As Integer = 0 To .RowCount - 1
                    If cbLoaiDH.EditValue = 1 Then
                        AddParameter("@SoPhieuPhu", tbSoPhieuDH.EditValue)
                        AddParameter("@FOB", .GetRowCellValue(i, "FOB"))
                    Else
                        AddParameter("@SoPhieu", tbSoPhieuDH.EditValue)
                        AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                    End If

                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))

                    AddParameter("@IDvattu", .GetRowCellValue(i, "IDVatTu"))
                    AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                    AddParameter("@TNK", .GetRowCellValue(i, "TNK"))
                    AddParameter("@Tiente", .GetRowCellValue(i, "TienTe"))
                    AddParameter("@TyGia", .GetRowCellValue(i, "TyGia"))
                    AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                    AddParameter("@Nhapthue", .GetRowCellValue(i, "NhapThue"))
                    AddParameter("@CO", .GetRowCellValue(i, "CO"))
                    AddParameter("@CQ", .GetRowCellValue(i, "CQ"))
                    AddParameter("@NgayVe", .GetRowCellValue(i, "NgayVe"))
                    AddParameter("@NgayVe2", .GetRowCellValue(i, "NgayVe2"))
                    'AddParameter("@CanNhap", gdvDatHangCT.GetRowCellValue(i, "CQ"))
                    If IsDBNull(.GetRowCellValue(i, "IDDatHang")) Or .GetRowCellValue(i, "IDDatHang") Is Nothing Then
                        Dim _IDDatHang As Object = doInsert("DATHANG")
                        If _IDDatHang Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDDatHang", _IDDatHang)
                    Else
                        AddParameterWhere("@IDDatHang", .GetRowCellValue(i, "IDDatHang"))
                        If doUpdate("DATHANG", "ID=@IDDatHang") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                Next
            End With


            ComitTransaction()

            AddParameterWhere("@SP", tbSoPhieuDH.EditValue)
            If cbLoaiDH.EditValue = 0 Then
                If doDelete("DATHANG", "Sophieu=@SP AND SoLuong = 0") Is Nothing Then
                    ShowBaoLoi("Không xóa được mặt hàng số lượng đặt = 0")
                End If
            ElseIf cbLoaiDH.EditValue = 1 Then
                If doDelete("DATHANG", "SoPhieuPhu=@SP AND SoLuong = 0") Is Nothing Then
                    ShowBaoLoi("Không xóa được mặt hàng số lượng đặt = 0")
                End If
            End If


            loadDSVatTuDatHang(tbSoPhieuDH.EditValue, cbLoaiDH.EditValue)

            ' Update số lượng cần nhập
            For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                Dim sql As String = ""
                If Not IsDBNull(gdvDatHangCT.GetRowCellValue(i, "IDDatHang")) Or Not gdvDatHangCT.GetRowCellValue(i, "IDDatHang") Is Nothing Then
                    If chkDuyetDatHang.Checked Then
                        sql = " Update DATHANG Set Cannhap = (Soluong - (select (isnull(sum(soluong),0)) from NHAPKHO where IDDathang = " & gdvDatHangCT.GetRowCellValue(i, "IDDatHang") & ")) Where ID = " & gdvDatHangCT.GetRowCellValue(i, "IDDatHang")
                    Else
                        sql = " Update DATHANG Set Cannhap = 0 Where ID = " & gdvDatHangCT.GetRowCellValue(i, "IDDatHang")
                    End If

                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        ShowBaoLoi(sql)
                    End If
                End If
            Next
            TrangThaiDH.isUpdate = True
            ShowAlert("Cập nhật đơn hàng thành công !")
            'If _exit = False Then
            Dim index As Object = gdvCT.FocusedRowHandle
            loadDSTongHopVatTuDatHang()
            gdvCT.FocusedRowHandle = index
            ' End If

            cbLoaiDH.Enabled = False
            If Not KiemTraDuKienThanhToan() Then
                'Dim f As New frmDuKienThanhToan
                'f._SoPhieuCGDH = tbSoPhieuDH.EditValue
                'f._PhaiTra = True
                'f._Buoc1 = True
                'f.ShowDialog()
            End If

        Catch ex As Exception
            If TrangThaiDH.isAddNew Or TrangThaiDH.isCopy Then
                tbSoPhieuDH.EditValue = ""
            End If
            RollBackTransaction()
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub

    Public Sub luuDHNhapKhau()
        chkDuyetDatHang.Checked = True
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileDHCT.RowCount - 1
            _File &= gdvListFileDHCT.GetRowCellValue(i, "File")
            If i < gdvListFileDHCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        btTinhTienDatHang.PerformClick()

        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        If gdvDatHangCT.RowCount = 0 Then
            ShowCanhBao("Chưa có thông tin đặt hàng !")
            Exit Sub
        End If
        'Dim _SoPhieuDatHang As Object
        '_SoPhieuDatHang = tbSoPhieuDH.EditValue

        If TrangThaiDH.isAddNew Then
            tbSoPhieuDH.EditValue = LaySoPhieu("PHIEUDATHANG")
        End If

        Try
            BeginTransaction()
            AddParameter("@Sophieu", tbSoPhieuDH.EditValue)
            AddParameter("@Ngaydat", tbNgayDatHang.EditValue)
            AddParameter("@Ngaynhan", tbNgayNhanDatHang.EditValue)
            AddParameter("@IDKhachhang", gdvNhaCCDatHang.EditValue)
            AddParameter("@IDNgd", cbDaiDienDatHang.EditValue)
            AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
            AddParameter("@IDtakecare", cbTakeCareDatHang.EditValue)
            AddParameter("@tienTruocthue", tbTienTruocThue.EditValue)
            AddParameter("@tienthue", tbTienThue.EditValue)
            AddParameter("@Tiente", cbTienTe.EditValue)
            AddParameter("@Tygia", tbTyGia.EditValue)
            AddParameter("@IDHinhThucTT", cbHinhThucTT.EditValue)
            AddParameter("@FileDinhKem", _File)
            AddParameter("@LoaiDH", cbLoaiDH.EditValue)
            AddParameter("@TienTeNK", cbTienTeNK.EditValue)
            AddParameter("@PheDuyet", True)

            If TrangThaiDH.isAddNew Then
                If doInsert("PHIEUDATHANG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@SP", tbSoPhieuDH.EditValue)
                If doUpdate("PHIEUDATHANG", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            With gdvDatHangCT
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "SLVe") = 0 Then
                        AddParameter("@SoPhieu", DBNull.Value)
                    Else
                        AddParameter("@SoPhieu", tbSoPhieuDH.EditValue)
                    End If
                    AddParameter("@SoPhieuPhu", .GetRowCellValue(i, "SoPhieuPhu"))
                    AddParameter("@FOB", .GetRowCellValue(i, "FOB"))
                    AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                    AddParameter("@ChiPhi", .GetRowCellValue(i, "ChiPhi"))
                    AddParameter("@IDvattu", .GetRowCellValue(i, "IDVatTu"))
                    AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                    AddParameter("@TNK", .GetRowCellValue(i, "TNK"))
                    AddParameter("@Tiente", .GetRowCellValue(i, "TienTe"))
                    AddParameter("@TyGia", .GetRowCellValue(i, "TyGia"))
                    AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                    AddParameter("@Nhapthue", .GetRowCellValue(i, "NhapThue"))
                    AddParameter("@CO", .GetRowCellValue(i, "CO"))
                    AddParameter("@CQ", .GetRowCellValue(i, "CQ"))
                    AddParameter("@NgayVe", .GetRowCellValue(i, "NgayVe"))
                    AddParameter("@NgayVe2", .GetRowCellValue(i, "NgayVe2"))
                    'AddParameter("@CanNhap", gdvDatHangCT.GetRowCellValue(i, "CQ"))
                    If IsDBNull(.GetRowCellValue(i, "IDDatHang")) Or .GetRowCellValue(i, "IDDatHang") Is Nothing Then
                        Dim _IDDatHang As Object = doInsert("DATHANG")
                        If _IDDatHang Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDDatHang", _IDDatHang)
                    Else
                        AddParameterWhere("@IDDatHang", .GetRowCellValue(i, "IDDatHang"))
                        If doUpdate("DATHANG", "ID=@IDDatHang") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                Next
            End With


            ComitTransaction()

            'AddParameterWhere("@SP", tbSoPhieuDH.EditValue)
            'If doDelete("DATHANG", "Sophieu=@SP AND SoLuong=0") Is Nothing Then
            '    ShowBaoLoi("Không xóa được vật tư số lượng đặt =0")
            'End If
            LoadDSHangCanVe(tbSoPhieuDH.EditValue)
            'loadDSVatTuDatHang(tbSoPhieuDH.EditValue, cbLoaiDH.EditValue)

            ' Update số lượng cần nhập
            For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                Dim sql As String = ""
                If Not IsDBNull(gdvDatHangCT.GetRowCellValue(i, "IDDatHang")) Or Not gdvDatHangCT.GetRowCellValue(i, "IDDatHang") Is Nothing Then
                    'If chkDuyetDatHang.Checked Then
                    sql = " Update DATHANG Set Cannhap = (Soluong - (select (isnull(sum(soluong),0)) from NHAPKHO where IDDathang = " & gdvDatHangCT.GetRowCellValue(i, "IDDatHang") & ")) Where ID = " & gdvDatHangCT.GetRowCellValue(i, "IDDatHang")
                    'Else
                    '    sql = " Update DATHANG Set Cannhap = 0 Where ID = " & gdvDatHangCT.GetRowCellValue(i, "IDDatHang")
                    'End If

                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        ShowBaoLoi(sql)
                    End If
                End If
            Next
            TrangThaiDH.isUpdate = True
            ShowAlert("Cập nhật đơn hàng thành công !")
            'If _exit = False Then
            Dim index As Object = gdvCT.FocusedRowHandle
            loadDSTongHopVatTuDatHang()
            gdvCT.FocusedRowHandle = index
            ' End If


        Catch ex As Exception
            If TrangThaiDH.isAddNew Or TrangThaiDH.isCopy Then
                tbSoPhieuDH.EditValue = ""
            End If
            RollBackTransaction()
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        'If tabYeuCauDi_DatHang.SelectedTabPage Is tabDatHang Then
        '    If tbSoPhieuDH.EditValue = "" Then Exit Sub
        '    If chkDHOmron.Checked Then
        '        XuatExcel.CreateExcelFileDatHangOmron(tbSoPhieuDH.EditValue, tbNgayDatHang.EditValue, gdvNhaCCDatHang.SelectedText)
        '    Else
        '        XuatExcel.CreateExcelFileDatHang(tbSoPhieuDH.EditValue, chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkCOCQ.Checked, chkVIE.Checked, chkN0.Checked, chkNgayGiao.Checked, "", cbLoaiDH.EditValue)
        '    End If
        'Else

        'End If
    End Sub

    Private Sub btXacNhanDatHang_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhanDatHang.Click
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            ShowBaoLoi("Bạn không có quyền thực hiện thao tác này !")
            Exit Sub
        End If

        Dim _PheDuyet As Int16
        If chkDuyetDatHang.Checked Then
            _PheDuyet = 1
        Else
            _PheDuyet = 0
        End If
        If tbSoPhieuDH.EditValue.ToString = "" Then
            btLuuDatHang.PerformClick()
        End If

        Try
            BeginTransaction()
            Dim sql As String = ""
            sql = "Update PHIEUDATHANG Set Pheduyet = " & _PheDuyet & " , IDNguoiduyet = " & TaiKhoan & " Where Sophieu = '" & tbSoPhieuDH.EditValue & "'"
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception("Không thực hiện được thao tác phê duyệt: " & LoiNgoaiLe)

            With gdvDatHangCT
                Dim _RowCount = .RowCount - 1
                For i As Integer = 0 To _RowCount
                    If _PheDuyet Then
                        sql = "Update DATHANG Set Cannhap = (Soluong - (select (isnull(sum(soluong),0)) from NHAPKHO where IDDathang = " & .GetRowCellValue(i, "IDDatHang") & ")) Where ID = " & .GetRowCellValue(i, "IDDatHang")
                    Else
                        sql = "Update DATHANG Set Cannhap = 0 Where ID = " & .GetRowCellValue(i, "IDDatHang")
                    End If
                    If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception("Lỗi update SL cần nhập:" & LoiNgoaiLe)
                Next
            End With

            ComitTransaction()
            chkDuyetDatHang.Checked = _PheDuyet
            Dim index As Object = gdvCT.FocusedRowHandle
            loadDSTongHopVatTuDatHang()
            gdvCT.FocusedRowHandle = index

            ShowAlert("Đã phê duyệt !")

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

#End Region

#Region "Tổng hợp đặt hàng"
    Public Sub loadDSTongHopVatTuDatHang()
        ShowWaiting("Đang tải danh sách đặt hàng ...")

        Dim sql As String = ""
        If cbTieuChi.EditValue = "Top 500" Then
            sql = " SELECT TOP 500 PHIEUDATHANG.ID,PHIEUDATHANG.SoPhieu,PHIEUNHAPKHO.SoPhieu as SoPhieuNK,PHIEUDATHANG.NgayDat, PHIEUDATHANG.NgayNhan, PHIEUDATHANG.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        Else
            sql = " SELECT PHIEUDATHANG.ID,PHIEUDATHANG.SoPhieu,PHIEUNHAPKHO.SoPhieu as SoPhieuNK,PHIEUDATHANG.NgayDat, PHIEUDATHANG.NgayNhan, PHIEUDATHANG.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        End If
        sql &= "    (Case when SoPhieuO is not null THEN 'BA ' + Convert(nvarchar, SoPhieuO) + '/'+ right(convert(nvarchar,PHIEUDATHANG.NgayDat,5),5) ELSE '' END) SoPhieuO, SoPhieuO as SoPhieuO2,"
        sql &= "	PHIEUDATHANG.TienTruocThue,PHIEUDATHANG.TienThue,PHIEUDATHANG.TyGia,"
        sql &= "	NHANSU.Ten AS TenNgLap,NHANSU_1.Ten AS TenNgd,NHANSU_2.Ten AS TenTakeCare,PHIEUDATHANG.Tiente, NHANSU_3.Ten AS TenNgDuyet,tblTienTe.Ten AS TenTienTe,"
        sql &= "	PHIEUDATHANG.FileDinhKem,PHIEUDATHANG.PheDuyet,PHIEUDATHANG.LoaiDH,tblTuDien.NoiDung AS TenLoaiDH, tblHinhThucTTKH.HinhThucTT_VIE AS HinhThucTT, '' AS CBTT, (CASE WHEN tb.SoTien is null then 0 ELSE (CASE WHEN tb.SoTien <> (PHIEUDATHANG.TienTruocThue + PHIEUDATHANG.TienThue) THEN 2 ELSE 1 END) END) AS TTTT "
        sql &= " FROM PHIEUDATHANG LEFT OUTER JOIN KHACHHANG ON PHIEUDATHANG.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT JOIN NHANSU ON PHIEUDATHANG.IDUSer=NHANSU.ID"
        sql &= "        LEFT JOIN NHANSU AS NHANSU_1 ON PHIEUDATHANG.IDNgd=NHANSU_1.ID"
        sql &= "        LEFT JOIN NHANSU AS NHANSU_2 ON PHIEUDATHANG.IDtakecare=NHANSU_2.ID"
        sql &= "        LEFT JOIN NHANSU AS NHANSU_3 ON PHIEUDATHANG.IDNguoiduyet=NHANSU_3.ID"
        sql &= "        LEFT JOIN tblTuDien ON PHIEUDATHANG.LoaiDH=tblTuDien.Ma AND tblTuDien.Loai=17"
        sql &= "        LEFT JOIN tblTienTe ON PHIEUDATHANG.Tiente=tblTienTe.ID"
        sql &= "        LEFT JOIN tblHinhThucTTKH ON tblHinhThucTTKH.ID = PHIEUDATHANG.IDHinhThucTT"
        sql &= "        LEFT JOIN (SELECT Sum(SoTien)SoTien, SoPhieu1 FROM tblCongNo WHERE Loai=1 GROUP BY SoPhieu1)tb ON tb.SoPhieu1=PHIEUDATHANG.SoPhieu "
        sql &= "        LEFT JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieuDH=PHIEUDATHANG.SoPhieu"
        sql &= " WHERE 1=1 "

        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,CONVERT(nvarchar,NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            '   If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            sql &= " AND (PHIEUDATHANG.IDTakeCare =@IDNV OR PHIEUDATHANG.IDUSer = @IDNV)"
            AddParameterWhere("@IDNV", TaiKhoan)
            '   End If
            '    sql &= " AND PHIEUDATHANG.IDTakecare= " & btNhanVien.EditValue
        End If

        If Not cbNCC.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDKhachhang= " & cbNCC.EditValue
        End If

        sql &= " ORDER BY Sophieu DESC"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        If Not dt Is Nothing Then
            gdv.DataSource = dt
            If Not gdvCT.FocusedRowHandle < 0 Then

                loadDSVatTuDatHangChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieu"), gdvCT.GetFocusedRowCellValue("LoaiDH"))
            Else
                gdvChiTietDH.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If



        CloseWaiting()
    End Sub

    Public Sub loadDSVatTuDatHang(ByVal SoPhieu As Object, Optional ByVal LoaiDH As Integer = 0)
        If SoPhieu = Nothing Then SoPhieu = "-1"
        Dim sql As String = ""
        sql &= " SELECT NgayDat,NgayNhan,IDKhachHang,SoPhieu,DieuKienKhac,TyGia,IDUser,TienTe, IDNgd,IDTakeCare,"
        sql &= " 		TienTruocThue,TienThue,PheDuyet,IDNguoiDuyet,FileDinhKem,IDHinhThucTT,LoaiDH,SoPhieuO"
        sql &= " FROM PHIEUDATHANG"
        sql &= " WHERE SoPhieu=" & SoPhieu

        sql &= " Select DATHANG.AZ, TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= "     VATTU.ThongSo, VATTU.ID AS IDVatTu,DATHANG.Soluong AS SoLuong,DATHANG.Dongia AS DonGia,DATHANG.FOB,"
        sql &= "     (CASE PHIEUDATHANG.LoaiDH WHEN 1 THEN DATHANG.Soluong*DATHANG.FOB ELSE DATHANG.Soluong*DATHANG.Dongia END )ThanhTien ,Convert(float,0) GiaNhapPT,"
        sql &= "     DATHANG.MucThue,DATHANG.NhapThue,DATHANG.ID AS IDDatHang,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList, "
        sql &= " VATTU.Tiente1 AS TienTe,ISNULL(VATTU.Gianhap1,0) AS GiaNhapDG,"
        sql &= "     VATTU.Mucthue1 AS MucThueDG, VATTU.Xuatthue1 AS NhapThueDG,VATTU.Khoiluong1 AS KhoiLuong,"
        sql &= "     ISNULL(DATHANG.TyGia,tblTienTe.TyGia)TyGia,ISNULL(VATTU.GiaNK,0)GiaNK,VATTU.DMTon,DATHANG.ChiPhi,"
        sql &= "     DATHANG.FOB,VATTU.TNK1,VATTU.TienTeNK,DATHANG.TNK,ISNULL(DATHANG.CO,Convert(bit,0))CO,ISNULL(DATHANG.CQ,Convert(bit,0))CQ,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan)NgayVe,ISNULL(NgayVe2,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan))NgayVe2 "
        sql &= " from DATHANG"

        sql &= "     LEFT JOIN VATTU on DATHANG.IDvattu =VATTU.ID "
        sql &= "     LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= "     LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= "     LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "     LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        If LoaiDH = 1 Then
            sql &= "     INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu "
            sql &= " WHERE DATHANG.SoPhieuPhu = " & SoPhieu
        Else
            sql &= "     INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieu "
            sql &= " WHERE DATHANG.SoPhieu = " & SoPhieu
        End If
        sql &= " ORDER BY DATHANG.AZ, DATHANG.ID"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            'If ds.Tables(1).Rows Then

            If LoaiDH = 2 Then
                bSTT.Visible = False
                LoadDSHangCanVe(SoPhieu)
            Else
                bSTT.Visible = True
                With ds.Tables(1)

                    If LoaiDH = 1 Then
                        For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                            .Rows(i)("AZ") = i + 1
                            If .Rows(i)("GiaNK") = 0 Then
                                .Rows(i)("GiaNhapPT") = 0
                            Else
                                .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("FOB") / .Rows(i)("GiaNK")) * 100, 2)
                            End If

                        Next
                    Else
                        For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                            .Rows(i)("AZ") = i + 1
                            If .Rows(i)("GiaList") = 0 Then
                                .Rows(i)("GiaNhapPT") = 0
                            Else
                                If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                                    .Rows(i)("GiaNhapPT") = 0
                                Else
                                    If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(ds.Tables(0).Rows(0)("Tiente")) Then
                                        .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / ds.Tables(0).Rows(0)("TyGia"))) * 100, 2)
                                    Else
                                        .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)

                                    End If
                                End If

                            End If

                        Next
                    End If

                End With
                gdvDatHang.DataSource = ds.Tables(1)
            End If
            If _exit = False Then
                If ds.Tables(0).Rows.Count > 0 Then
                    tbSoPhieuDH.EditValue = ds.Tables(0).Rows(0)("SoPhieu")
                    gdvNhaCCDatHang.EditValue = ds.Tables(0).Rows(0)("IDKhachHang")
                    cbDaiDienDatHang.EditValue = ds.Tables(0).Rows(0)("IDNgd")
                    cbTakeCareDatHang.EditValue = ds.Tables(0).Rows(0)("IDTakeCare")
                    tbNgayDatHang.EditValue = ds.Tables(0).Rows(0)("NgayDat")
                    _exit = True
                    tbNgayNhanDatHang.EditValue = ds.Tables(0).Rows(0)("NgayNhan")
                    _exit = False
                    cbTienTe.EditValue = ds.Tables(0).Rows(0)("TienTe")
                    tbTyGia.EditValue = ds.Tables(0).Rows(0)("TyGia")
                    cbNguoiLap.EditValue = ds.Tables(0).Rows(0)("IDUser")
                    chkDuyetDatHang.Checked = ds.Tables(0).Rows(0)("PheDuyet")
                    tbSoPhieuDH.Tag = ds.Tables(0).Rows(0)("SoPhieuO")
                    tbTienTruocThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue")
                    tbTienThue.EditValue = ds.Tables(0).Rows(0)("TienThue")
                    cbNguoiLap.EditValue = ds.Tables(0).Rows(0)("IDUser")
                    'tbTienSauThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue") + ds.Tables(0).Rows(0)("TienThue")
                    gdvListFileDH.DataSource = DataSourceDSFile(ds.Tables(0).Rows(0)("FileDinhKem").ToString)
                    cbLoaiDH.EditValue = ds.Tables(0).Rows(0)("LoaiDH")
                Else

                    tbSoPhieuDH.EditValue = ""
                    gdvNhaCCDatHang.EditValue = Nothing
                    cbDaiDienDatHang.EditValue = Nothing
                    cbTakeCareDatHang.EditValue = Nothing
                    tbNgayDatHang.EditValue = GetServerTime()
                    tbNgayNhanDatHang.EditValue = tbNgayDatHang.EditValue
                    cbTienTe.EditValue = 0
                    tbTyGia.EditValue = 1
                    cbNguoiLap.EditValue = Convert.ToUInt32(TaiKhoan)
                    chkDuyetDatHang.Checked = False
                    tbTienTruocThue.EditValue = 0
                    tbTienThue.EditValue = 0
                    'tbTienSauThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue") + ds.Tables(0).Rows(0)("TienThue")
                    gdvListFileDH.DataSource = DataSourceDSFile()
                    cbLoaiDH.EditValue = 0
                    tbSoPhieuDH.Tag = ""
                End If
            End If

            If TrangThaiDH.isAddNew Or TrangThaiDH.isCopy Then
                cbLoaiDH.Enabled = True
            Else
                cbLoaiDH.Enabled = False
            End If

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadDSVatTuDatHangChiTiet(ByVal SoPhieu As Object, Optional ByVal LoaiDH As Integer = 0)
        If SoPhieu = Nothing Then SoPhieu = "-1"

        Dim sql As String = ""

        sql &= " Select TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= "     VATTU.ThongSo, VATTU.ID AS IDVatTu,DATHANG.Soluong AS SoLuong,DATHANG.Dongia AS DonGia,DATHANG.FOB,"
        sql &= "     (CASE PHIEUDATHANG.LoaiDH WHEN 1 THEN DATHANG.Soluong*DATHANG.FOB ELSE DATHANG.Soluong*DATHANG.Dongia END )ThanhTien ,Convert(float,0) GiaNhapPT,"
        sql &= "     DATHANG.MucThue,DATHANG.NhapThue,DATHANG.ID AS IDDatHang,"
        sql &= "     (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= "     tblTienTe.Ten AS TenTienTe, DATHANG.TienTe,ISNULL(VATTU.Gianhap1,0) AS GiaNhapDG,"
        sql &= "     VATTU.Mucthue1 AS MucThueDG, VATTU.Xuatthue1 AS NhapThueDG,VATTU.Khoiluong1 AS KhoiLuong,"
        sql &= "     ISNULL(DATHANG.TyGia,tblTienTe.TyGia)TyGia,ISNULL(VATTU.GiaNK,0)GiaNK,VATTU.DMTon,DATHANG.ChiPhi,"
        sql &= "     DATHANG.FOB,VATTU.TNK1,tblTienTeNK.Ten AS TenTienTeNK,DATHANG.TNK,ISNULL(DATHANG.CO,Convert(bit,0))CO,ISNULL(DATHANG.CQ,Convert(bit,0))CQ,"
        sql &= "     (CASE WHEN PHIEUDATHANG.LoaiDH=2 THEN PHIEUDATHANG.NgayNhan ELSE ISNULL(NgayVe,PHIEUDATHANG.NgayNhan) END)NgayVe, "
        sql &= "    (CASE WHEN DATHANG.NgayVe2 <> DATHANG.NgayVe THEN DATHANG.NgayVe2 ELSE NULL END )NgayVe2"
        '---Tai
        sql &= ", CAST(case when DATHANG .ID  not in(select idchaogia from HaiQuan_ChiTietLamHaiQuan inner join DATHANG  on idchaogia =DATHANG .id where Soluong =(select SUM (SoLuongLamHQ ) from HaiQuan_ChiTietLamHaiQuan where idchaogia =DATHANG .id )) then 0 else 1 end as bit) as lamhaiquan"
        sql &= ", CAST(case when DATHANG .ID  not in(select idchaogia from HaiQuan_ChiTietLamHaiQuan inner join DATHANG  on idchaogia =DATHANG .id where Soluong =(select SUM (SoLuongLamHQ ) from HaiQuan_ChiTietLamHaiQuan where idchaogia =DATHANG .id )) then 0 else 1 end as bit) as lamhaiquan2"
        sql &= ", CAST(case when DATHANG .ID  not in(select idchaogia from HaiQuan_ChiTietLamHaiQuan) then 0 else 1 end as bit) as lamhaiquan3"
        '---Tai
        sql &= "    from DATHANG"

        sql &= "     LEFT JOIN VATTU on DATHANG.IDvattu =VATTU.ID "
        sql &= "     LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= "     LEFT JOIN tblTienTe AS tblTienTeNK ON VATTU.TienTeNK=tblTienTeNK.ID "
        sql &= "     LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= "     LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "     LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        If LoaiDH = 1 Then
            bCTTTVatTu.Visible = False
            bCTTTNhapKhau.Visible = True
            colCTDonGia.Visible = False
            colCTFOB.Visible = True
            colCTVAT.Visible = False
            colCTNhapVAT.Visible = False
            colCTTNK.Visible = True
            sql &= "     INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu "
            sql &= " WHERE DATHANG.SoPhieuPhu = " & SoPhieu
            'tai
            If _message <> 0 And hienthipopup = True Then
                sql &= " and DATHANG .ID  not in(select idchaogia from HaiQuan_ChiTietLamHaiQuan ) "
            End If
            'tài
            sql &= " ORDER BY DATHANG.AZ,DATHANG.ID"
        ElseIf LoaiDH = 0 Then
            bCTTTVatTu.Visible = True
            bCTTTNhapKhau.Visible = False
            colCTDonGia.Visible = True
            colCTFOB.Visible = False
            colCTVAT.Visible = True
            colCTNhapVAT.Visible = True
            colCTTNK.Visible = False
            sql &= "     INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieu "
            sql &= " WHERE DATHANG.SoPhieu = " & SoPhieu
            sql &= " ORDER BY DATHANG.AZ,DATHANG.ID"
        Else
            bCTTTVatTu.Visible = True
            bCTTTNhapKhau.Visible = True
            colCTDonGia.Visible = True
            colCTFOB.Visible = True
            colCTVAT.Visible = True
            colCTNhapVAT.Visible = True
            colCTTNK.Visible = True
            sql &= " INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieu "
            sql &= " WHERE DATHANG.SoPhieu = " & SoPhieu
            sql &= " ORDER BY DATHANG.SoPhieu DESC,DATHANG.SoPhieuPhu,DATHANG.AZ,HangSX,Model"
        End If


        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            With dt
                If LoaiDH = 1 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("GiaNK") = 0 Then
                            .Rows(i)("GiaNhapPT") = 0
                        Else

                            .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("FOB") / .Rows(i)("GiaNK")) * 100, 2)

                        End If
                    Next
                Else
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("GiaList") = 0 Then
                            .Rows(i)("GiaNhapPT") = 0
                        Else
                            If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                                .Rows(i)("GiaNhapPT") = 0
                            Else
                                If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(gdvCT.GetFocusedRowCellValue("Tiente")) Then
                                    .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / gdvCT.GetFocusedRowCellValue("Tiente"))) * 100, 2)
                                Else
                                    .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)
                                End If
                            End If
                        End If
                    Next
                End If
            End With
            gdvChiTietDH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        'tài 
        If _message <> 0 And hienthipopup = True Then
            tgidlamhaiquan = _message
            Dim ck = " select IDKhachhang  from HaiQuan_LamHaiQuan where id=" & tgidlamhaiquan.ToString()
            tgKH = ExecuteSQLScalar(ck)
            cbNCC.EditValue = tgKH
            barBtnLamHQ.Enabled = False
            btnBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        If _message = 0 Or hienthipopup = False Then
            tgidlamhaiquan = -1
            tgKH = -1
            barBtnLamHQ.Enabled = False
            btnBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            chaogia = Nothing
        End If
        '   ReDim Preserve chaogiaCT(0)
        d = 0
        'tai
        loadDSTongHopVatTuDatHang()
    End Sub

    Private Sub gdvCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If Not gdvCT.FocusedRowHandle < 0 Then
            loadDSVatTuDatHangChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieu"), gdvCT.GetFocusedRowCellValue("LoaiDH"))
        Else
            loadDSVatTuDatHangChiTiet("0")
        End If

    End Sub

    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        Else
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        End If
    End Sub
#End Region

#Region "Sự kiện chung"
    Private Sub tabYeuCauDi_DatHang_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabYeuCauDi_DatHang.SelectedPageChanged
        If tabYeuCauDi_DatHang.SelectedTabPage Is tabDatHang Then
            chkCOCQ.Visible = True
            btXuatExcelDatHang.DropDownControl = pXuatExcel
        ElseIf tabYeuCauDi_DatHang.SelectedTabPage Is TabTongHopDH Then

        End If
    End Sub

#End Region

#Region "Tài liệu vật tư"
    Private Sub rcbTaiLieuTH_Popup(sender As System.Object, e As System.EventArgs) Handles rcbTaiLieuTH.Popup
        LoadDSTaiLieu(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub LoadDSTaiLieu(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvDSTaiLieu.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvDSTaiLieuCT.AddNewRow()
                gdvDSTaiLieuCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvDSTaiLieuCT.CloseEditor()
        gdvDSTaiLieuCT.UpdateCurrentRow()

    End Sub

    Private Sub gdvDSTaiLieuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvDSTaiLieuCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            Dim psi As New ProcessStartInfo()
            With psi
                Select Case tabYeuCauDi_DatHang.SelectedTabPage.Name
                    Case tabTongHop.Name
                        .FileName = UrlTaiLieuVatTu & gdvTongHopCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvTongHopCT.GetFocusedRowCellValue("HangSX") & "\" & e.CellValue
                End Select


                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub
#End Region

    Private Sub tbGiaBanPT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaNhapPT.ButtonClick
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            gdvDatHangCT.SetRowCellValue(i, "GiaNhapPT", tbGiaNhapPT.EditValue)
        Next
        gdvDatHangCT.EndUpdate()
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        TinhTienDatHang()
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick
        btTaoMoiDatHang.PerformClick()
        tabYeuCauDi_DatHang.SelectedTabPage = tabDatHang
    End Sub

    Private Sub rcbFileDinhKem_Popup(sender As System.Object, e As System.EventArgs) Handles rcbFileDinhKem.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvFileCT.AddNewRow()
                gdvFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()

    End Sub

    Private Sub gdvFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayDat")).Year.ToString & "\" & UrlDatHang & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & e.CellValue, e.CellValue)

        End If
    End Sub

    Private Sub btDongDH_Click(sender As System.Object, e As System.EventArgs) Handles btDongDH.Click
        gFileDinhKemDH.Visible = Not gFileDinhKemDH.Visible
    End Sub

    Private Sub gdvDatHangCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvDatHangCT.RowCellClick
        If e.Column.FieldName = "NhapThue" Then
            gdvDatHangCT.SetRowCellValue(e.Handled, "NhapThue", Not e.CellValue)
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub mTichThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTichThue.ItemClick
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
        gdvDatHangCT.BeginUpdate()
        Dim check = Not gdvDatHangCT.GetRowCellValue(0, "NhapThue")
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            gdvDatHangCT.SetRowCellValue(i, "NhapThue", check)
        Next
        gdvDatHangCT.EndUpdate()
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
    End Sub

    Private Sub mDuyetDH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuyetDH.ItemClick, btDuyetDH.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            ShowBaoLoi("Bạn không có quyền thực hiện thao tác này !")
            Exit Sub
        End If
        If gdvCT.GetFocusedRowCellValue("LoaiDH") = 2 Then
            ShowCanhBao("Tính năng này không áp dụng cho đặt hàng nước ngoài !")
            Exit Sub
        End If
        Try
            BeginTransaction()
            Dim sql As String = ""
            If gdvCT.GetFocusedRowCellValue("PheDuyet") Then
                sql = "Update PHIEUDATHANG Set Pheduyet = 0 , IDNguoiduyet = " & TaiKhoan & " Where Sophieu = '" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'"
            Else
                sql = "Update PHIEUDATHANG Set Pheduyet = 1 , IDNguoiduyet = " & TaiKhoan & " Where Sophieu = '" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'"
            End If

            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception("Không thực hiện được thao tác phê duyệt: " & LoiNgoaiLe)

            With gdvChiTietDHCT
                Dim _RowCount = .RowCount - 1
                For i As Integer = 0 To _RowCount
                    If Not gdvCT.GetFocusedRowCellValue("PheDuyet") Then
                        sql = "Update DATHANG Set Cannhap = (Soluong - (select (isnull(sum(soluong),0)) from NHAPKHO where IDDathang = " & .GetRowCellValue(i, "IDDatHang") & ")) Where ID = " & .GetRowCellValue(i, "IDDatHang")
                    Else
                        sql = "Update DATHANG Set Cannhap = 0 Where ID = " & .GetRowCellValue(i, "IDDatHang")
                    End If
                    If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception("Lỗi update SL cần nhập:" & LoiNgoaiLe)
                Next
            End With

            ComitTransaction()

            Dim index As Object = gdvCT.FocusedRowHandle
            loadDSTongHopVatTuDatHang()
            gdvCT.FocusedRowHandle = index

            ShowAlert("Đã phê duyệt !")

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try


    End Sub

    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        'Dim f As New frmTinhTrangVT
        'f.Tag = Me.Parent.Tag
        'f._IDVatTu = gdvDatHangCT.GetFocusedRowCellValue("IDVatTu")
        'f._HienThongTinNX = True
        'f._HienNCC = True

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '        '  f._HienNCC = False
        '        f._HienCGXK = False
        '    Else
        '        f._HienCGXK = True
        '    End If
        'Else
        '    f._HienCGXK = True
        '    ' f._HienNCC = True
        'End If
        'f.ShowDialog()
    End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVT.ItemClick
        'Dim f As New frmTinhTrangVT
        'f.Tag = Me.Parent.Tag
        'f._IDVatTu = gdvChiTietDHCT.GetFocusedRowCellValue("IDVatTu")
        'f._HienThongTinNX = True
        'f._HienNCC = True
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '        '      f._HienNCC = False
        '        f._HienCGXK = False
        '    Else

        '        f._HienCGXK = True
        '    End If
        'Else
        '    f._HienCGXK = True
        '    '   f._HienNCC = True
        'End If
        'f.ShowDialog()
    End Sub

    Private Sub tbNgayNhanDatHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbNgayNhanDatHang.EditValueChanged
        If cbLoaiDH.EditValue = 2 Then Exit Sub
        If _exit = True Then Exit Sub
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            gdvDatHangCT.SetRowCellValue(i, "NgayVe", tbNgayNhanDatHang.EditValue)
            gdvDatHangCT.SetRowCellValue(i, "NgayVe2", tbNgayNhanDatHang.EditValue)
        Next
        gdvDatHangCT.EndUpdate()
        gdvDatHangCT.CloseEditor()
        gdvDatHangCT.UpdateCurrentRow()
    End Sub

    Private Sub btCanXuat_Click(sender As System.Object, e As System.EventArgs) Handles btCanXuat.Click
        If listVTCX Is Nothing Then
            If listVTCX.Rows.Count = 0 Then
                ShowCanhBao("Chưa chọn hàng cần xuất !")
                Exit Sub
            End If
        End If
        Dim _strIDVT As String = "("

        For i As Integer = 0 To listVTCX.Rows.Count - 1
            _strIDVT &= listVTCX.Rows(i)("IDVatTu") & ","
        Next

        _strIDVT = _strIDVT.Substring(0, _strIDVT.Length - 1)
        _strIDVT &= ") "

        cbNhomVT.EditValue = _LocCXNhomVT
        cbTenVT1.EditValue = _LocCXTenVT
        cbHangSX.EditValue = _LocCXHangVT
        tbMaVT.EditValue = _LocCXModelVT
        tbThongSo.EditValue = Nothing


        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        Dim sqlWhere As String = " WHERE VATTU.ID IN " & _strIDVT
        Dim sqlOrder As String = ""

        sql &= " SELECT NULL AS CanhBao,TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model AS MaVT,TENNHOM.Ten_ENG AS TenNhom_ENG,VATTU.HinhAnh,(convert(image,NULL))HienThi"
        sql &= " ,SLNhap=(select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanNhap= (select isnull(count(IDvattu),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLXuat=(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanXuat= (select isnull(count(IDvattu),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')"
        sql &= " ,XuatMax=(select isnull(Max(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,VATTU.DatToithieu AS DatToiThieu, VATTU.DMTon"
        sql &= " ,SLTon=((select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID) -(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID))"
        sql &= " ,CanNhap=(select isnull(SUM(Cannhap),0) from DATHANG where IDVattu=Vattu.ID) "
        sql &= " ,Convert(float,0)LuongDat "
        sql &= " ,KHSD=ISNULL(tbKHSD.KHSD,0)"
        sql &= " ,CanXuat=(select isnull(SUM(CanXuat),0) from CHAOGIA where CHAOGIA.IDVattu=Vattu.ID) "
        sql &= " ,ChaoGia=(select isnull(SUM(Soluong),0) from CHAOGIA inner join BANGCHAOGIA ON CHAOGIA.SOPHIEU = BANGCHAOGIA.SOPHIEU where IDVattu=Vattu.ID and CHAOGIA.Trangthai <> 2 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,YeuCau=(select isnull(SUM(Soluong),0) from YEUCAUDEN INNER JOIN BANGYEUCAU ON YEUCAUDEN.SOPHIEU = BANGYEUCAU.SOPHIEU where IDVattu=Vattu.ID and BANGYEUCAU.Trangthai <=3 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' ) "
        sql &= " ,VATTU.Ton,VATTU.TonNCC,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS TenDVT, VATTU.Thongso AS ThongSo, VATTU.IDTennuoc,TENNUOC.TEN AS NuocSX, VATTU.ID AS IDVatTu, VATTU.Thongdung AS ThongDung, VATTU.MaLoi, VATTU.HangTon,VATTU.TaiLieu, "
        sql &= " VATTU.Mucthue1 AS MucThue,VATTU.Xuatthue1 AS NhapThue,VATTU.Tiente1 AS TienTeDG,VATTU.Gianhap1 AS GiaNhapDG,VATTU.Mucthue1 AS MucThueDG,VATTU.Xuatthue1 AS NhapThueDG,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " VATTU.Khoiluong1 AS KhoiLuong,tblTienTe.TyGia,VATTU.GiaNK,VATTU.TNK1,VATTU.TienTeNK,VATTU.ConSX,ISNULL(VATTU.SLMOQ1,0)SLMOQ1,ISNULL(VATTU.SLMOQ2,0)SLMOQ2,ISNULL(VATTU.SLMOQ3,0)SLMOQ3,"
        sql &= " ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = VATTU.ID ORDER BY SoPhieu DESC),0) GiaNhap"
        sql &= " FROM VATTU"
        sql &= " LEFT OUTER JOIN TENVATTU on VATTU.IDTenvattu=TENVATTU.ID  "
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh"
        sql &= " LEFT OUTER JOIN TENNHOM ON TENNHOM.ID=VATTU.IDTennhom"
        sql &= " LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= " LEFT JOIN "
        sql &= " (SELECT * FROM (SELECT COUNT(IDKhachHang)KHSD,IDVatTu"
        sql &= " FROM (SELECT DISTINCT PHIEUXUATKHO.IDKhachHang,XUATKHO.IDVatTu "
        sql &= " 	FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu WHERE PHIEUXUATKHO.NgayThang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')tb"
        sql &= " GROUP BY IDVatTu)tb2)tbKHSD ON tbKHSD.IDVatTu=VATTU.ID "

        If Not cbNhomVT.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDTennhom =" & cbNhomVT.EditValue
        If Not cbTenVT1.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDTenvattu =" & cbTenVT1.EditValue
        If Not cbHangSX.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDHangsanxuat =" & cbHangSX.EditValue
        If Not tbMaVT.EditValue Is Nothing Then sqlWhere &= " AND VATTU.Model like '" & tbMaVT.EditValue & "%'"
        If Not tbThongSo.EditValue Is Nothing Then sqlWhere &= " AND VATTU.Thongso like '%" & tbThongSo.EditValue & "%'"

        sql &= sqlWhere
        sql &= " ORDER BY TenVT,HangSX,MaVT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                colTHHinhAnh.Visible = True
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                '     .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            Else
                colTHHinhAnh.Visible = False
            End If

            gdvTongHop.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub btCanton_Click(sender As System.Object, e As System.EventArgs) Handles btCanton.Click
        If listVTCX Is Nothing Then
            If listVTCX.Rows.Count = 0 Then
                ShowCanhBao("Chưa chọn hàng cần xuất !")
                Exit Sub
            End If
        End If
        Dim _strIDVT As String = "("

        For i As Integer = 0 To listVTCX.Rows.Count - 1
            _strIDVT &= listVTCX.Rows(i)("IDVatTu") & ","
        Next

        _strIDVT = _strIDVT.Substring(0, _strIDVT.Length - 1)
        _strIDVT &= ") "

        cbNhomVT.EditValue = _LocCXNhomVT
        cbTenVT1.EditValue = _LocCXTenVT
        cbHangSX.EditValue = _LocCXHangVT
        tbMaVT.EditValue = _LocCXModelVT
        tbThongSo.EditValue = Nothing


        ShowWaiting("Đang tải DS hàng hóa ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        Dim sqlWhere As String = " WHERE (VATTU.Ton=1 OR VATTU.ID IN " & _strIDVT & ")"
        Dim sqlOrder As String = ""

        sql &= " SELECT NULL AS CanhBao, TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model AS MaVT,TENNHOM.Ten_ENG AS TenNhom_ENG,VATTU.HinhAnh,(convert(image,NULL))HienThi"
        sql &= " ,SLNhap=(select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanNhap= (select isnull(count(IDvattu),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLXuat=(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanXuat= (select isnull(count(IDvattu),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')"
        sql &= " ,XuatMax=(select isnull(Max(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,VATTU.DatToithieu AS DatToiThieu, VATTU.DMTon"
        sql &= " ,SLTon=((select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID) -(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID))"
        sql &= " ,CanNhap=(select isnull(SUM(Cannhap),0) from DATHANG where IDVattu=Vattu.ID) "
        sql &= " ,Convert(float,0)LuongDat "
        sql &= " ,KHSD=ISNULL(tbKHSD.KHSD,0)"
        sql &= " ,CanXuat=(select isnull(SUM(CanXuat),0) from CHAOGIA where CHAOGIA.IDVattu=Vattu.ID) "
        sql &= " ,ChaoGia=(select isnull(SUM(Soluong),0) from CHAOGIA inner join BANGCHAOGIA ON CHAOGIA.SOPHIEU = BANGCHAOGIA.SOPHIEU where IDVattu=Vattu.ID and CHAOGIA.Trangthai <> 2 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,YeuCau=(select isnull(SUM(Soluong),0) from YEUCAUDEN INNER JOIN BANGYEUCAU ON YEUCAUDEN.SOPHIEU = BANGYEUCAU.SOPHIEU where IDVattu=Vattu.ID and BANGYEUCAU.Trangthai <=3 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' ) "
        sql &= " ,VATTU.Ton,VATTU.TonNCC,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS TenDVT, VATTU.Thongso AS ThongSo, VATTU.IDTennuoc,TENNUOC.TEN AS NuocSX, VATTU.ID AS IDVatTu, VATTU.Thongdung AS ThongDung, VATTU.MaLoi, VATTU.HangTon,VATTU.TaiLieu, "
        sql &= " VATTU.Mucthue1 AS MucThue,VATTU.Xuatthue1 AS NhapThue,VATTU.Tiente1 AS TienTeDG,VATTU.Gianhap1 AS GiaNhapDG,VATTU.Mucthue1 AS MucThueDG,VATTU.Xuatthue1 AS NhapThueDG,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " VATTU.Khoiluong1 AS KhoiLuong,tblTienTe.TyGia,VATTU.GiaNK,VATTU.TNK1,VATTU.TienTeNK,VATTU.ConSX,ISNULL(VATTU.SLMOQ1,0)SLMOQ1,ISNULL(VATTU.SLMOQ2,0)SLMOQ2,ISNULL(VATTU.SLMOQ3,0)SLMOQ3,"
        sql &= " ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = VATTU.ID ORDER BY SoPhieu DESC),0) GiaNhap"
        sql &= " FROM VATTU"
        sql &= " LEFT OUTER JOIN TENVATTU on VATTU.IDTenvattu=TENVATTU.ID  "
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh"
        sql &= " LEFT OUTER JOIN TENNHOM ON TENNHOM.ID=VATTU.IDTennhom"
        sql &= " LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= " LEFT JOIN "
        sql &= " (SELECT * FROM (SELECT COUNT(IDKhachHang)KHSD,IDVatTu"
        sql &= " FROM (SELECT DISTINCT PHIEUXUATKHO.IDKhachHang,XUATKHO.IDVatTu "
        sql &= " 	FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu WHERE PHIEUXUATKHO.NgayThang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')tb"
        sql &= " GROUP BY IDVatTu)tb2)tbKHSD ON tbKHSD.IDVatTu=VATTU.ID "

        If Not cbNhomVT.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDTennhom =" & cbNhomVT.EditValue
        If Not cbTenVT1.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDTenvattu =" & cbTenVT1.EditValue
        If Not cbHangSX.EditValue Is Nothing Then sqlWhere &= " AND VATTU.IDHangsanxuat =" & cbHangSX.EditValue
        If Not tbMaVT.EditValue Is Nothing Then sqlWhere &= " AND VATTU.Model like '" & tbMaVT.EditValue & "%'"
        If Not tbThongSo.EditValue Is Nothing Then sqlWhere &= " AND VATTU.Thongso like '%" & tbThongSo.EditValue & "%'"

        sql &= sqlWhere
        sql &= " ORDER BY TenVT,HangSX,MaVT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                colTHHinhAnh.Visible = True
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                '  .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            Else
                colTHHinhAnh.Visible = False
            End If
            gdvTongHop.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub btChuyenTH_Click(sender As System.Object, e As System.EventArgs) Handles btChuyenTH.Click
        If gdvDatHangCT.RowCount = 0 Then
            ShowCanhBao("Không có mặt hàng nào được chọn !")
            Exit Sub
        End If

        Dim _strIDVT As String = ""

        If cbLoaiDH.EditValue = 2 Then
            For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                If gdvDatHangCT.GetRowCellValue(i, "SoPhieu").ToString <> "" Then
                    _strIDVT &= gdvDatHangCT.GetRowCellValue(i, "IDVatTu") & ","
                End If

            Next
        Else
            For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                _strIDVT &= gdvDatHangCT.GetRowCellValue(i, "IDVatTu") & ","
            Next
        End If


        _strIDVT = _strIDVT.Substring(0, _strIDVT.Length - 1)

        cbNhomVT.EditValue = _LocCXNhomVT
        cbTenVT1.EditValue = _LocCXTenVT
        cbHangSX.EditValue = _LocCXHangVT
        tbMaVT.EditValue = _LocCXModelVT
        tbThongSo.EditValue = Nothing


        ShowWaiting("Đang tải DS hàng hóa ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        Dim sqlWhere As String = " WHERE VATTU.ID IN (" & _strIDVT & ")"
        Dim sqlOrder As String = ""

        sql &= " SELECT NULL AS CanhBao,TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model AS MaVT,TENNHOM.Ten_ENG AS TenNhom_ENG,VATTU.HinhAnh,(convert(image,NULL))HienThi"
        sql &= " ,SLNhap=(select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanNhap= (select isnull(count(IDvattu),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLXuat=(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanXuat= (select isnull(count(IDvattu),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')"
        sql &= " ,XuatMax=(select isnull(Max(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,VATTU.DatToithieu AS DatToiThieu, VATTU.DMTon"
        sql &= " ,SLTon=((select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID) -(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID))"
        sql &= " ,CanNhap=(select isnull(SUM(Cannhap),0) from DATHANG where IDVattu=Vattu.ID) "
        sql &= " ,Convert(float,0)LuongDat "
        sql &= " ,KHSD=ISNULL(tbKHSD.KHSD,0)"
        sql &= " ,CanXuat=(select isnull(SUM(CanXuat),0) from CHAOGIA where CHAOGIA.IDVattu=Vattu.ID) "
        sql &= " ,ChaoGia=(select isnull(SUM(Soluong),0) from CHAOGIA inner join BANGCHAOGIA ON CHAOGIA.SOPHIEU = BANGCHAOGIA.SOPHIEU where IDVattu=Vattu.ID and CHAOGIA.Trangthai <> 2 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,YeuCau=(select isnull(SUM(Soluong),0) from YEUCAUDEN INNER JOIN BANGYEUCAU ON YEUCAUDEN.SOPHIEU = BANGYEUCAU.SOPHIEU where IDVattu=Vattu.ID and BANGYEUCAU.Trangthai <=3 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' ) "
        sql &= " ,VATTU.Ton,VATTU.TonNCC,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS TenDVT, VATTU.Thongso AS ThongSo, VATTU.IDTennuoc,TENNUOC.TEN AS NuocSX, VATTU.ID AS IDVatTu, VATTU.Thongdung AS ThongDung, VATTU.MaLoi, VATTU.HangTon,VATTU.TaiLieu, "
        sql &= " VATTU.Mucthue1 AS MucThue,VATTU.Xuatthue1 AS NhapThue,VATTU.Tiente1 AS TienTeDG,VATTU.Gianhap1 AS GiaNhapDG,VATTU.Mucthue1 AS MucThueDG,VATTU.Xuatthue1 AS NhapThueDG,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " VATTU.Khoiluong1 AS KhoiLuong,tblTienTe.TyGia,VATTU.GiaNK,VATTU.TNK1,VATTU.TienTeNK,VATTU.ConSX,ISNULL(VATTU.SLMOQ1,0)SLMOQ1,ISNULL(VATTU.SLMOQ2,0)SLMOQ2,ISNULL(VATTU.SLMOQ3,0)SLMOQ3,"
        sql &= " ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = VATTU.ID ORDER BY SoPhieu DESC),0) GiaNhap"
        sql &= " FROM VATTU"
        sql &= " LEFT JOIN TENVATTU on VATTU.IDTenvattu=TENVATTU.ID  "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh"
        sql &= " LEFT JOIN TENNHOM ON TENNHOM.ID=VATTU.IDTennhom"
        sql &= " LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= " LEFT JOIN "
        sql &= " (SELECT * FROM (SELECT COUNT(IDKhachHang)KHSD,IDVatTu"
        sql &= " FROM (SELECT DISTINCT PHIEUXUATKHO.IDKhachHang,XUATKHO.IDVatTu "
        sql &= " 	FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu WHERE PHIEUXUATKHO.NgayThang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')tb"
        sql &= " GROUP BY IDVatTu)tb2)tbKHSD ON tbKHSD.IDVatTu=VATTU.ID "

        sql &= sqlWhere
        sql &= " ORDER BY CHARINDEX(','+CONVERT(varchar, VATTU.id)+',', '," & _strIDVT & ",')"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                colTHHinhAnh.Visible = True
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                ' .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            Else
                colTHHinhAnh.Visible = False
            End If
            If gdvTongHop.DataSource Is Nothing Then
                gdvTongHop.DataSource = tb
            Else
                If ShowCauHoi("Bạn có muốn giữ lại thông tin vật tư trong form tổng hợp không ?") Then
                    Dim tbTH As DataTable = gdvTongHop.DataSource
                    tbTH.Merge(tb)
                    gdvTongHop.DataSource = tbTH
                Else
                    gdvTongHop.DataSource = tb
                End If
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        gdvTongHopCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            For j As Integer = 0 To gdvTongHopCT.RowCount - 1
                If gdvDatHangCT.GetRowCellValue(i, "IDVatTu") = gdvTongHopCT.GetRowCellValue(j, "IDVatTu") Then
                    gdvTongHopCT.SetRowCellValue(j, "LuongDat", gdvDatHangCT.GetRowCellValue(i, "SoLuong"))
                End If
            Next
        Next
        gdvTongHopCT.EndUpdate()

        CloseWaiting()
        tabYeuCauDi_DatHang.SelectedTabPage = tabTongHop



    End Sub

    Private Sub btXemNhapXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXemNhapXuat.Click
        If gdvTongHopCT.RowCount = 0 Then
            ShowCanhBao("Không có mặt hàng nào được chọn !")
            Exit Sub
        End If

        Dim _strIDVT As String = "("
        For i As Integer = 0 To gdvTongHopCT.RowCount - 1
            _strIDVT &= gdvTongHopCT.GetRowCellValue(i, "IDVatTu") & ","
        Next

        _strIDVT = _strIDVT.Substring(0, _strIDVT.Length - 1)
        _strIDVT &= ") "


        ShowWaiting("Đang tải DS hàng hóa ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        Dim sqlWhere As String = " WHERE VATTU.ID IN " & _strIDVT
        Dim sqlOrder As String = ""

        sql &= " SELECT NULL AS CanhBao,TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model AS MaVT,TENNHOM.Ten_ENG AS TenNhom_ENG,VATTU.HinhAnh,(convert(image,NULL))HienThi"
        sql &= " ,SLNhap=(select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanNhap= (select isnull(count(IDvattu),0) from V_NHAPKHO where IDVattu=Vattu.ID AND V_NHAPKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLXuat=(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' )"
        sql &= " ,SLanXuat= (select isnull(count(IDvattu),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')"
        sql &= " ,XuatMax=(select isnull(Max(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID AND V_XUATKHO.Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,VATTU.DatToithieu AS DatToiThieu, VATTU.DMTon"
        sql &= " ,SLTon=((select isnull(SUM(Soluong),0) from V_NHAPKHO where IDVattu=Vattu.ID) -(select isnull(SUM(Soluong),0) from V_XUATKHO where IDVattu=Vattu.ID))"
        sql &= " ,CanNhap=(select isnull(SUM(Cannhap),0) from DATHANG where IDVattu=Vattu.ID) "
        sql &= " ,Convert(float,0)LuongDat "
        sql &= " ,KHSD=ISNULL(tbKHSD.KHSD,0)"
        sql &= " ,CanXuat=(select isnull(SUM(CanXuat),0) from CHAOGIA where CHAOGIA.IDVattu=Vattu.ID) "
        sql &= " ,ChaoGia=(select isnull(SUM(Soluong),0) from CHAOGIA inner join BANGCHAOGIA ON CHAOGIA.SOPHIEU = BANGCHAOGIA.SOPHIEU where IDVattu=Vattu.ID and CHAOGIA.Trangthai <> 2 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "') "
        sql &= " ,YeuCau=(select isnull(SUM(Soluong),0) from YEUCAUDEN INNER JOIN BANGYEUCAU ON YEUCAUDEN.SOPHIEU = BANGYEUCAU.SOPHIEU where IDVattu=Vattu.ID and BANGYEUCAU.Trangthai <=3 AND Ngaythang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "' ) "
        sql &= " ,VATTU.Ton,VATTU.TonNCC,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS TenDVT, VATTU.Thongso AS ThongSo, VATTU.IDTennuoc,TENNUOC.TEN AS NuocSX, VATTU.ID AS IDVatTu, VATTU.Thongdung AS ThongDung, VATTU.MaLoi, VATTU.HangTon,VATTU.TaiLieu, "
        sql &= " VATTU.Mucthue1 AS MucThue,VATTU.Xuatthue1 AS NhapThue,VATTU.Tiente1 AS TienTeDG,VATTU.Gianhap1 AS GiaNhapDG,VATTU.Mucthue1 AS MucThueDG,VATTU.Xuatthue1 AS NhapThueDG,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " VATTU.Khoiluong1 AS KhoiLuong,tblTienTe.TyGia,VATTU.GiaNK,VATTU.TNK1,VATTU.TienTeNK,VATTU.ConSX,ISNULL(VATTU.SLMOQ1,0)SLMOQ1,ISNULL(VATTU.SLMOQ2,0)SLMOQ2,ISNULL(VATTU.SLMOQ3,0)SLMOQ3,"
        sql &= " ISNULL((SELECT TOP 1 DonGia * (SELECT TyGia FROM PHIEUNHAPKHO WHERE NHAPKHO.SoPhieu=PHIEUNHAPKHO.SoPhieu) FROm NHAPKHO  WHERE NHAPKHO.IDVatTu = VATTU.ID ORDER BY SoPhieu DESC),0) GiaNhap"
        sql &= " FROM VATTU"
        sql &= " LEFT JOIN TENVATTU on VATTU.IDTenvattu=TENVATTU.ID  "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh"
        sql &= " LEFT JOIN TENNHOM ON TENNHOM.ID=VATTU.IDTennhom"
        sql &= " LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= " LEFT JOIN "
        sql &= " (SELECT * FROM (SELECT COUNT(IDKhachHang)KHSD,IDVatTu"
        sql &= " FROM (SELECT DISTINCT PHIEUXUATKHO.IDKhachHang,XUATKHO.IDVatTu "
        sql &= " 	FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu WHERE PHIEUXUATKHO.NgayThang BETWEEN '" & tbTuNgayTH.EditValue & "' AND '" & tbDenNgayTH.EditValue & "')tb"
        sql &= " GROUP BY IDVatTu)tb2)tbKHSD ON tbKHSD.IDVatTu=VATTU.ID "
        sql &= sqlWhere
        sql &= " ORDER BY TenVT,HangSX,MaVT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                colTHHinhAnh.Visible = True
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                '   .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            Else
                colTHHinhAnh.Visible = False
            End If
            gdvTongHop.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub mXemAnhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemAnhLon.ItemClick
        'If gdvTongHopCT.FocusedRowHandle < 0 Then Exit Sub
        'If gdvTongHopCT.GetFocusedRowCellValue("HinhAnh").ToString <> "" Then
        '    Dim f As New frmXemAnh
        '    f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & gdvTongHopCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvTongHopCT.GetFocusedRowCellValue("HangSX") & "\" & gdvTongHopCT.GetFocusedRowCellValue("HinhAnh").ToString)
        '    f.Text = "Ảnh: " & gdvTongHopCT.GetFocusedRowCellValue("MaVT").ToString
        '    f.ShowDialog()
        'Else
        '    ShowCanhBao("Không tìm thấy ảnh hàng hóa!")
        'End If
    End Sub

    Private Sub mTaiTL_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiTL.ItemClick
        If gdvDSTaiLieuCT.GetFocusedRowCellValue("File").ToString <> "" Then
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvDSTaiLieuCT.GetFocusedRowCellValue("File"))
            saveFile.FileName = gdvDSTaiLieuCT.GetFocusedRowCellValue("File")
            If saveFile.ShowDialog = DialogResult.OK Then
                Try
                    ShowWaiting("Đang tải file về máy ...")
                    System.IO.File.Copy(UrlTaiLieuVatTu & gdvTongHopCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvTongHopCT.GetFocusedRowCellValue("HangSX") & "\" & gdvDSTaiLieuCT.GetFocusedRowCellValue("File"), saveFile.FileName, True)
                    CloseWaiting()
                    If ShowCauHoi("Đã tải file về máy, bạn có muốn mở file vừa tải không ?") Then
                        Dim psi As New ProcessStartInfo()
                        With psi
                            .FileName = saveFile.FileName
                            .UseShellExecute = True
                        End With
                        Try
                            Process.Start(psi)
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try
                    End If
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try
            End If


        End If
    End Sub

    Private Sub mSuaThongSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaThongSo.ItemClick
        'If Not KiemTraQuyenSuDung("Menu", "mThongSo", DanhMucQuyen.QuyenSua) Then Exit Sub
        'deskTop.OpenTab("Thông số", "THONGSO", New frmThongSo, True, Nothing, "mThongSo")
        'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterMaVT.EditValue = gdvTongHopCT.GetFocusedRowCellValue("MaVT")
        'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btTaiLai.PerformClick()
    End Sub

    Private Sub mTTVTTH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTTVTTH.ItemClick
        'Dim f As New frmTinhTrangVT
        'f.Tag = Me.Parent.Tag
        'f._IDVatTu = gdvTongHopCT.GetFocusedRowCellValue("IDVatTu")
        'f._HienThongTinNX = True
        'f._HienNCC = True
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '        '   f._HienNCC = False
        '        f._HienCGXK = False
        '    Else
        '        '    f._HienNCC = True
        '        f._HienCGXK = True
        '    End If
        'Else
        '    f._HienCGXK = True
        '    ' f._HienNCC = True
        'End If
        'f.ShowDialog()
    End Sub

    Private Sub chkThongSo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkThongSo.CheckedChanged
        colThongSo.Visible = chkThongSo.Checked
    End Sub

    Private Sub lbCNFileDH_Click(sender As System.Object, e As System.EventArgs) Handles lbCNFileDH.Click, lbCNMauDH.Click
        If ShowCauHoi("Khi cập nhật thì thông tin chữ ký cũ sẽ mất, bạn cần tạo lại chữ ký cho mẫu file đặt hàng." & vbCrLf & "Bạn có tiếp tục không ?") Then
            IO.File.Copy(ServerName & "\Excel$\BIEUMAU\DATHANG\_MAUDATHANG_ENG_N0.xls", Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_ENG_N0.xls", True)
            IO.File.Copy(ServerName & "\Excel$\BIEUMAU\DATHANG\_MAUDATHANG_ENG_N2.xls", Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_ENG_N2.xls", True)
            IO.File.Copy(ServerName & "\Excel$\BIEUMAU\DATHANG\_MAUDATHANG_VIE_N0.xls", Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_VIE_N0.xls", True)
            IO.File.Copy(ServerName & "\Excel$\BIEUMAU\DATHANG\_MAUDATHANG_VIE_N2.xls", Application.StartupPath & "\Excel\DATHANG\_MAUDATHANG_VIE_N2.xls", True)
            ShowThongBao("Đã cập nhật mẫu file đặt hàng !")
        End If
    End Sub

    Private Sub cbLoaiDH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbLoaiDH.EditValueChanged

        If cbLoaiDH.EditValue = 1 Then
            bTTVatTu.Visible = False
            bTTNhapKhau.Visible = True
            colDonGia.Visible = False
            colFOB.Visible = True
            colVAT.Visible = False
            colNhapVAT.Visible = False
            colTNKDH.Visible = True
            colSLVe.Visible = False
            lbTienTeNK.Visible = False
            cbTienTeNK.Visible = False
            btChiPhiHQ.Visible = False
            bSoPhieuPhu.Visible = False
            colSoLuong.OptionsColumn.ReadOnly = False
            gdvDatHangCT.OptionsSelection.MultiSelect = False
            mVTVe.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btBacklog.Visible = False
            lbTongFOB.Visible = False
            lbTongFOBChuaVe.Visible = False
            tbTongFOBVe.Visible = False
            tbTongFOBChuaVe.Visible = False
            If tbSoPhieuDH.EditValue Is Nothing Then
                For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                    gdvDatHangCT.BeginUpdate()
                    gdvDatHangCT.DeleteRow(i)
                    gdvDatHangCT.EndUpdate()
                Next
            Else
                _exit = True
                loadDSVatTuDatHang(tbSoPhieuDH.EditValue, 1)
                _exit = False
            End If
            bSTT.Visible = True
        ElseIf cbLoaiDH.EditValue = 0 Then
            bTTVatTu.Visible = True
            bTTNhapKhau.Visible = False
            colDonGia.Visible = True
            colFOB.Visible = False
            colVAT.Visible = True
            colNhapVAT.Visible = True
            colTNKDH.Visible = False
            lbTienTeNK.Visible = False
            cbTienTeNK.Visible = False
            btChiPhiHQ.Visible = False
            colSLVe.Visible = False
            bSoPhieuPhu.Visible = False
            colSoLuong.OptionsColumn.ReadOnly = False
            gdvDatHangCT.OptionsSelection.MultiSelect = False
            mVTVe.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btBacklog.Visible = False
            lbTongFOB.Visible = False
            lbTongFOBChuaVe.Visible = False
            tbTongFOBVe.Visible = False
            tbTongFOBChuaVe.Visible = False
            If tbSoPhieuDH.EditValue Is Nothing Then
                For i As Integer = 0 To gdvDatHangCT.RowCount - 1
                    gdvDatHangCT.BeginUpdate()
                    gdvDatHangCT.DeleteRow(i)
                    gdvDatHangCT.EndUpdate()
                Next
            Else
                _exit = True
                loadDSVatTuDatHang(tbSoPhieuDH.EditValue, 0)
                _exit = False
            End If
            bSTT.Visible = True
        Else
            bTTVatTu.Visible = True
            bTTNhapKhau.Visible = True
            colDonGia.Visible = True
            colFOB.Visible = True
            colVAT.Visible = True
            colNhapVAT.Visible = True
            colTNKDH.Visible = True
            lbTienTeNK.Visible = True
            cbTienTeNK.Visible = True
            btChiPhiHQ.Visible = True
            colSLVe.Visible = True
            bSoPhieuPhu.Visible = True
            gdvDatHangCT.OptionsSelection.MultiSelect = True
            colSoLuong.OptionsColumn.ReadOnly = True
            mVTVe.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            If TrangThaiDH.isAddNew Then
                LoadDSHangCanVe()
            Else
                LoadDSHangCanVe(tbSoPhieuDH.EditValue)
            End If

            chkDuyetDatHang.Checked = True
            btBacklog.Visible = True
            lbTongFOB.Visible = True
            lbTongFOBChuaVe.Visible = True
            tbTongFOBVe.Visible = True
            tbTongFOBChuaVe.Visible = True
            bSTT.Visible = False
        End If
    End Sub

    Public Sub LoadDSHangCanVe(Optional ByVal SoDH As Object = -1)
        ShowWaiting("Đang tải ds hàng cần nhập !")
        Dim sql As String = ""
        sql &= " Select (CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN NULL ELSE DATHANG.SoPhieu END)SoPhieu,"
        sql &= " 	DATHANG.SoPhieuPhu,TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= "     VATTU.ThongSo, VATTU.ID AS IDVatTu,DATHANG.Soluong AS SoLuong,"
        sql &= " 	(CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN 0 ELSE DATHANG.SoLuong END)SLVe,"
        sql &= " 	DATHANG.FOB,Convert(float,0) GiaNhapPT,"
        sql &= " 	(CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN 0 ELSE DATHANG.DonGia END)DonGia,"
        sql &= " 	(CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN 0 ELSE DATHANG.DonGia * DATHANG.SoLuong END)ThanhTien,"
        sql &= "     DATHANG.MucThue,DATHANG.NhapThue,DATHANG.ID AS IDDatHang,"
        sql &= "    (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= "     VATTU.Tiente1 AS TienTe,ISNULL(VATTU.Gianhap1,0) AS GiaNhapDG,"
        sql &= "     VATTU.Mucthue1 AS MucThueDG, VATTU.Xuatthue1 AS NhapThueDG,VATTU.Khoiluong1 AS KhoiLuong,"
        sql &= "     ISNULL(DATHANG.TyGia,tblTienTe.TyGia)TyGia,ISNULL(VATTU.GiaNK,0)GiaNK,VATTU.DMTon,DATHANG.ChiPhi,"
        sql &= "     DATHANG.FOB,VATTU.TNK1,VATTU.TienTeNK,DATHANG.TNK,ISNULL(DATHANG.CO,Convert(bit,0))CO,ISNULL(DATHANG.CQ,Convert(bit,0))CQ,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan)NgayVe,ISNULL(NgayVe2,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan))NgayVe2 "
        sql &= " from DATHANG"
        sql &= " 	INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu AND PHIEUDATHANG.PheDuyet=1"
        sql &= "     INNER JOIN VATTU on DATHANG.IDvattu =VATTU.ID "
        sql &= "     LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= "     LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= "     LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "     LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        sql &= " WHERE DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) OR DATHANG.SoPhieu= '" & SoDH & "'"
        sql &= " ORDER BY DATHANG.SoPhieu DESC,DATHANG.SoPhieuPhu,DATHANG.AZ,HangSX,Model"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            With tb
                For i As Integer = 0 To .Rows.Count - 1
                    If .Rows(i)("GiaList") = 0 Then
                        .Rows(i)("GiaNhapPT") = 0
                    Else
                        If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                            .Rows(i)("GiaNhapPT") = 0
                        Else
                            If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(cbTienTe.EditValue) Then
                                .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)) * 100, 2)
                            Else
                                .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)
                            End If
                        End If

                    End If

                Next
            End With
            gdvDatHang.DataSource = tb
            TinhFoB()
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub chkHienThongSo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkHienThongSo.CheckedChanged
        colThongSoVT.Visible = chkHienThongSo.Checked
    End Sub

    Private Sub rcbNCC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNCC.ButtonClick
        If e.Button.Index = 1 Then
            cbNCC.EditValue = Nothing
        End If
    End Sub

    Private Sub btChiPhiHQ_Click(sender As System.Object, e As System.EventArgs) Handles btChiPhiHQ.Click
        If tbSoPhieuDH.EditValue.ToString.Trim = "" Then
            ShowCanhBao("Bạn cần chốt thông tin hàng về trước !")
            Exit Sub
        End If
        ' Dim f As New frmThongTinHaiQuan2
        '  f.PhieuDatHang = tbSoPhieuDH.EditValue
        '  f.ShowDialog()
    End Sub


    Private Sub gdvDatHangCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvDatHangCT.RowCellStyle

        If e.RowHandle < 0 Then Exit Sub
        On Error Resume Next
        If e.Column.FieldName = "SLVe" Then
            If e.CellValue > 0 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "SoLuong" Then
            If gdvDatHangCT.GetRowCellValue(e.RowHandle, "SoLuong") > gdvDatHangCT.GetRowCellValue(e.RowHandle, "DMTon") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub mVTVe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mVTVe.ItemClick
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.SelectedRowsCount - 1
            gdvDatHangCT.SetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "SLVe", gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "SoLuong"))
        Next
        gdvDatHangCT.EndUpdate()

    End Sub


    Public Sub TinhToanLuongVTVe()

        Try
            gdvDatHangCT.BeginUpdate()
            Dim i As Integer = 0
            With gdvDatHangCT

                While i < .RowCount
                    If .GetRowCellValue(i, "SLVe") = 0 Then
                        .SetRowCellValue(i, "SoPhieu", Nothing)
                    Else
                        If .GetRowCellValue(i, "SLVe") < .GetRowCellValue(i, "SoLuong") Then
                            .AddNewRow()
                            .SetFocusedRowCellValue("SoPhieuPhu", .GetRowCellValue(i, "SoPhieuPhu"))
                            .SetFocusedRowCellValue("SoPhieu", Nothing)
                            .SetFocusedRowCellValue("IDVatTu", .GetRowCellValue(i, "IDVatTu"))
                            .SetFocusedRowCellValue("TenVT", .GetRowCellValue(i, "TenVT"))
                            .SetFocusedRowCellValue("HangSX", .GetRowCellValue(i, "HangSX"))
                            .SetFocusedRowCellValue("Model", .GetRowCellValue(i, "Model"))
                            .SetFocusedRowCellValue("DVT", .GetRowCellValue(i, "DVT"))
                            .SetFocusedRowCellValue("SoLuong", .GetRowCellValue(i, "SoLuong") - .GetRowCellValue(i, "SLVe"))
                            .SetFocusedRowCellValue("SLVe", 0)
                            .SetFocusedRowCellValue("DonGia", 0.0)
                            .SetFocusedRowCellValue("GiaNhapPT", 0.0)
                            .SetFocusedRowCellValue("ThanhTien", 0.0)
                            .SetFocusedRowCellValue("ThongSo", .GetRowCellValue(i, "ThongSo"))
                            .SetFocusedRowCellValue("MucThue", .GetRowCellValue(i, "MucThue"))
                            .SetFocusedRowCellValue("NhapThue", .GetRowCellValue(i, "NhapThue"))
                            .SetFocusedRowCellValue("GiaList", .GetRowCellValue(i, "GiaList"))
                            .SetFocusedRowCellValue("TienTe", .GetRowCellValue(i, "TienTe"))
                            .SetFocusedRowCellValue("GiaNhapDG", .GetRowCellValue(i, "GiaNhapDG"))
                            .SetFocusedRowCellValue("MucThueDG", .GetRowCellValue(i, "MucThueDG"))
                            .SetFocusedRowCellValue("NhapThueDG", .GetRowCellValue(i, "NhapThueDG"))
                            .SetFocusedRowCellValue("TyGia", .GetRowCellValue(i, "TyGia"))
                            .SetFocusedRowCellValue("GiaNK", .GetRowCellValue(i, "GiaNK"))
                            .SetFocusedRowCellValue("FOB", .GetRowCellValue(i, "FOB"))
                            .SetFocusedRowCellValue("TienTeNK", .GetRowCellValue(i, "TienTeNK"))
                            .SetFocusedRowCellValue("TNK1", .GetRowCellValue(i, "TNK1"))
                            .SetFocusedRowCellValue("TNK", .GetRowCellValue(i, "TNK"))
                            .SetFocusedRowCellValue("NgayVe", .GetRowCellValue(i, "NgayVe"))
                            .SetFocusedRowCellValue("NgayVe2", .GetRowCellValue(i, "NgayVe2"))
                            .SetFocusedRowCellValue("CO", .GetRowCellValue(i, "CO"))
                            .SetFocusedRowCellValue("CQ", .GetRowCellValue(i, "CQ"))
                            .CloseEditor()
                            .UpdateCurrentRow()

                            .SetRowCellValue(i, "SoLuong", .GetRowCellValue(i, "SLVe"))
                        End If

                    End If
                    i += 1
                End While
            End With

            Dim _TongTTFOB As Double = 0
            Dim _TongTTSauHQ As Double = 0
            Dim _TyGiaHQVCQT As Double = 0
            Dim _TyGiaNhapKho As Double = 0
            Dim _TyGiaHQHangHoa As Double = 0
            Dim _TongTienVCQT As Double = 0
            Dim _TongChiPhi As Double = 0

            Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM DATHANGQT WHERE SoPhieuDatHang=" & tbSoPhieuDH.EditValue)
            Dim tbCPVCQT As DataTable = ExecuteSQLDataTable("SELECT IsNULL(SoTien,0) FROM CHIPHI WHERE PHIEUCGDH='" & tbSoPhieuDH.EditValue & "' AND Loai=0 AND TienTE <>0 ")
            Dim tbCPTrongNuoc As DataTable = ExecuteSQLDataTable("SELECT IsNULL(SUM(SoTien),0) FROM CHIPHI WHERE PHIEUCGDH='" & tbSoPhieuDH.EditValue & "' AND Loai=0 AND TienTE =0 ")
            If tbCPVCQT Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                If tbCPVCQT.Rows.Count = 0 Then
                    _TongTienVCQT = 0
                Else
                    _TongTienVCQT = tbCPVCQT.Rows(0)(0)
                End If

            End If

            If tbCPTrongNuoc Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                If tbCPTrongNuoc.Rows.Count = 0 Then
                    _TongChiPhi = 0
                Else
                    _TongChiPhi = tbCPTrongNuoc.Rows(0)(0)
                End If

            End If

            If Not tb Is Nothing Then
                If tb.Rows.Count > 0 Then
                    '_TyGiaTTVCQT = tb.Rows(0)("TyGiaTTVC")
                    _TyGiaHQVCQT = tb.Rows(0)("TyGiaHQVC")
                    _TyGiaNhapKho = tb.Rows(0)("TyGiaNhapKho")
                    _TyGiaHQHangHoa = tb.Rows(0)("TyGiaHQHH")
                    '  _TongTienVCQT = tb.Rows(0)("TienVCQT") * (tb.Rows(0)("TyGiaHQVC") / tb.Rows(0)("TyGiaNhapKho"))
                    ' _TongChiPhi = tb.Rows(0)("PhiHaiQuan") + tb.Rows(0)("PhiLuuKho") + tb.Rows(0)("PhiVCMatDat") + tb.Rows(0)("PhiPSPhanBoChung") + tb.Rows(0)("TienVCNoiDia") + tb.Rows(0)("PhiDiLaiThongQuan") + tb.Rows(0)("ChiPhiKhac")
                    Dim tbtmp As New DataTable
                    tbtmp.Columns.Add("IDDatHang", GetType(Int32))
                    tbtmp.Columns.Add("IDVatTu", GetType(Int32))
                    tbtmp.Columns.Add("FOB", GetType(Double))
                    tbtmp.Columns.Add("SL", GetType(Double))
                    tbtmp.Columns.Add("ThanhTienFOB", GetType(Double))
                    tbtmp.Columns.Add("TienVCQT", GetType(Double))
                    tbtmp.Columns.Add("ThueSuat", GetType(Double))
                    tbtmp.Columns.Add("TienTNK", GetType(Double))
                    tbtmp.Columns.Add("ThanhTienSauHQ", GetType(Double))
                    tbtmp.Columns.Add("ChiPhiPSNoiDia", GetType(Double))
                    tbtmp.Columns.Add("DonGiaBAC", GetType(Double))

                    For j As Integer = 0 To gdvDatHangCT.RowCount - 1
                        If gdvDatHangCT.GetRowCellValue(j, "SoPhieu").ToString.Trim <> "" Then
                            Dim r As DataRow = tbtmp.NewRow
                            r("IDDatHang") = gdvDatHangCT.GetRowCellValue(j, "IDDatHang")
                            r("IDVatTu") = gdvDatHangCT.GetRowCellValue(j, "IDVatTu")
                            r("FOB") = gdvDatHangCT.GetRowCellValue(j, "FOB")
                            r("SL") = gdvDatHangCT.GetRowCellValue(j, "SoLuong")
                            r("ThanhTienFOB") = gdvDatHangCT.GetRowCellValue(j, "FOB") * gdvDatHangCT.GetRowCellValue(j, "SoLuong")
                            _TongTTFOB += gdvDatHangCT.GetRowCellValue(j, "FOB") * gdvDatHangCT.GetRowCellValue(j, "SoLuong")
                            r("TienVCQT") = 0
                            r("ThueSuat") = gdvDatHangCT.GetRowCellValue(j, "TNK")
                            r("TienTNK") = 0
                            r("ThanhTienSauHQ") = 0
                            r("ChiPhiPSNoiDia") = 0
                            r("DonGiaBAC") = 0
                            tbtmp.Rows.Add(r)
                        End If

                    Next

                    For j As Integer = 0 To tbtmp.Rows.Count - 1
                        tbtmp.Rows(j)("TienVCQT") = (_TongTienVCQT / _TongTTFOB) * tbtmp.Rows(j)("ThanhTienFOB")
                        tbtmp.Rows(j)("TienTNK") = (tbtmp.Rows(j)("ThanhTienFOB") + tbtmp.Rows(j)("TienVCQT")) * (tbtmp.Rows(j)("ThueSuat") / 100) * _TyGiaHQHangHoa
                        tbtmp.Rows(j)("ThanhTienSauHQ") = (tbtmp.Rows(j)("ThanhTienFOB") + tbtmp.Rows(j)("TienVCQT")) * _TyGiaNhapKho + tbtmp.Rows(j)("TienTNK")
                        _TongTTSauHQ += tbtmp.Rows(j)("ThanhTienSauHQ")
                    Next
                    For j As Integer = 0 To tbtmp.Rows.Count - 1
                        tbtmp.Rows(j)("ChiPhiPSNoiDia") = (_TongChiPhi / _TongTTSauHQ) * tbtmp.Rows(j)("ThanhTienSauHQ")
                        tbtmp.Rows(j)("DonGiaBAC") = (tbtmp.Rows(j)("ThanhTienSauHQ") + tbtmp.Rows(j)("ChiPhiPSNoiDia")) / tbtmp.Rows(j)("SL")
                    Next
                    For j As Integer = 0 To gdvDatHangCT.RowCount - 1
                        For k As Integer = 0 To tbtmp.Rows.Count - 1
                            If Not gdvDatHangCT.GetRowCellValue(j, "IDDatHang") Is Nothing And Not IsDBNull(gdvDatHangCT.GetRowCellValue(j, "IDDatHang")) Then
                                If gdvDatHangCT.GetRowCellValue(j, "IDDatHang") = tbtmp.Rows(k)("IDDatHang") Then
                                    If chkLamTron.Checked Then
                                        If cbTienTe.EditValue = 0 Then
                                            gdvDatHangCT.SetRowCellValue(j, "DonGia", Math.Ceiling(tbtmp.Rows(k)("FOB") * _TyGiaNhapKho / 100) * 100)
                                            gdvDatHangCT.SetRowCellValue(j, "ChiPhi", Math.Ceiling((tbtmp.Rows(k)("DonGiaBAC") - Math.Ceiling(tbtmp.Rows(k)("FOB") * _TyGiaNhapKho / 100) * 100) / 100) * 100)
                                        Else
                                            gdvDatHangCT.SetRowCellValue(j, "DonGia", Math.Round(tbtmp.Rows(k)("FOB") * _TyGiaNhapKho, 2))
                                            gdvDatHangCT.SetRowCellValue(j, "ChiPhi", Math.Round(tbtmp.Rows(k)("DonGiaBAC") - tbtmp.Rows(k)("FOB") * _TyGiaNhapKho, 2))
                                        End If
                                    Else
                                        gdvDatHangCT.SetRowCellValue(j, "DonGia", tbtmp.Rows(k)("FOB") * _TyGiaNhapKho)
                                        gdvDatHangCT.SetRowCellValue(j, "ChiPhi", tbtmp.Rows(k)("DonGiaBAC") - tbtmp.Rows(k)("FOB") * _TyGiaNhapKho)
                                    End If

                                    Exit For
                                End If
                            End If

                        Next
                    Next

                Else
                    ShowCanhBao("Chưa có thông tin chi phí !")
                End If
            End If

            TinhTienDatHang()
        Catch ex As Exception
            ShowCanhBao(ex.Message)
        Finally
            gdvDatHangCT.EndUpdate()
        End Try

    End Sub

    Private Sub mGopVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mGopVatTu.ItemClick
        Dim _SoLuong As Double = 0
        Dim _SoLuongVe As Double = 0
        Dim _ok As Boolean = True
        Dim _IDDatHang As String = "("
        gdvDatHangCT.BeginUpdate()
        'Dim _
        If gdvDatHangCT.SelectedRowsCount > 1 Then
            _SoLuong = gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(0), "SoLuong")
            _SoLuongVe = gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(0), "SLVe")
            For i As Integer = 1 To gdvDatHangCT.SelectedRowsCount - 1
                _SoLuong += gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "SoLuong")
                _SoLuongVe += gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "SLVe")
                _IDDatHang &= gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "IDDatHang") & ","
                If gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "SoPhieu").ToString <> gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i - 1), "SoPhieu").ToString _
                    Or gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "SoPhieuPhu") <> gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i - 1), "SoPhieuPhu") _
                    Or gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "FOB") <> gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i - 1), "FOB") _
                    Or gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i), "DonGia") <> gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(i - 1), "DonGia") Then
                    _ok = False
                End If
            Next
            If Not _ok Then
                gdvDatHangCT.EndUpdate()
                ShowCanhBao("Không thể ghép các mặt hàng đã chọn !")
                Exit Sub
            Else
                _IDDatHang = _IDDatHang.Substring(0, _IDDatHang.Length - 1) & ")"
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT COUNT (ID) FROM NHAPKHO WHERE IDDatHang IN " & _IDDatHang)
                If Not tb Is Nothing Then
                    If tb.Rows(0)(0) > 0 Then
                        gdvDatHangCT.EndUpdate()
                        ShowCanhBao("Đã có nhập kho được lập cho hàng hóa được chọn !")
                        Exit Sub
                    End If
                End If
                If ExecuteSQLNonQuery("UPDATE DATHANG SET SoLuong=" & _SoLuong & " WHERE ID =" & gdvDatHangCT.GetRowCellValue(gdvDatHangCT.GetSelectedRows(0), "IDDatHang")) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If

                If ExecuteSQLNonQuery("DELETE DATHANG WHERE ID IN " & _IDDatHang) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
                gdvDatHangCT.SetRowCellValue(gdvDatHangCT.GetSelectedRows(0), "SoLuong", _SoLuong)
                gdvDatHangCT.SetRowCellValue(gdvDatHangCT.GetSelectedRows(0), "SLVe", _SoLuongVe)
            End If

            For i As Integer = 1 To gdvDatHangCT.SelectedRowsCount - 1
                gdvDatHangCT.DeleteRow(gdvDatHangCT.GetSelectedRows(i))
            Next
            gdvDatHangCT.EndUpdate()
            ShowAlert("Đã gộp mặt hàng")
        End If

    End Sub

    Private Sub btChonFileBacklog_Click(sender As System.Object, e As System.EventArgs) Handles btChonFileBacklog.Click

        'Dim openfile As New OpenFileDialog
        'openfile.Filter = "Excel File|*.xls;*.xlsx"
        'If openfile.ShowDialog = DialogResult.OK Then
        '    Dim tb As DataTable = Utils.exportXLS2DataTable.getDataTableFromXLS(openfile.FileName, System.IO.Path.GetExtension(openfile.FileName), "Sheet1")
        '    If Not tb Is Nothing Then
        '        ShowWaiting("Đang so sánh backlog ...")
        '        gdvDatHangCT.BeginUpdate()
        '        For i As Integer = 0 To tb.Rows.Count - 1
        '            For j As Integer = 0 To gdvDatHangCT.RowCount - 1
        '                If gdvDatHangCT.GetRowCellValue(j, "Model") = tb.Rows(i)("Description") And gdvDatHangCT.GetRowCellValue(j, "FOB") = tb.Rows(i)("Unit Price") _
        '                    And gdvDatHangCT.GetRowCellValue(j, "SoLuong") = tb.Rows(i)("Order Qty") And Convert.ToInt32(Convert.ToDateTime(tbNgayChot.EditValue).ToString("yyyyMMdd")) >= Convert.ToInt32(tb.Rows(i)("Exp Delv Date")) Then
        '                    gdvDatHangCT.SetRowCellValue(j, "SLVe", tb.Rows(i)("Backlog Qty"))
        '                End If
        '            Next
        '        Next
        '        CloseWaiting()
        '        gdvDatHangCT.EndUpdate()
        '        ShowThongBao("Đã so sánh backlog !")
        '        pBackLog.Visible = False
        '    End If
        'End If
    End Sub

    Public Sub TinhFoB()
        tbTongFOBVe.EditValue = 0
        tbTongFOBChuaVe.EditValue = 0
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            tbTongFOBChuaVe.EditValue += gdvDatHangCT.GetRowCellValue(i, "FOB") * (gdvDatHangCT.GetRowCellValue(i, "SoLuong") - gdvDatHangCT.GetRowCellValue(i, "SLVe"))
            tbTongFOBVe.EditValue += gdvDatHangCT.GetRowCellValue(i, "FOB") * gdvDatHangCT.GetRowCellValue(i, "SLVe")
        Next
        gdvDatHangCT.EndUpdate()
    End Sub

    Private Sub btBacklog_Click(sender As System.Object, e As System.EventArgs) Handles btBacklog.Click
        pBackLog.Visible = Not pBackLog.Visible
    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        pBackLog.Visible = False
    End Sub

    Private Sub cbDaiDienDatHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbDaiDienDatHang.EditValueChanged
        On Error Resume Next
        If TrangThaiDH.isAddNew = True Then
            Dim edit As LookUpEdit = CType(sender, LookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            cbTakeCareDatHang.EditValue = dr("IDTakeCare")
        End If
    End Sub

    Private Sub mXemChiPhiHQ_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChiPhiHQ.ItemClick
        '  Dim f As New frmThongTinHaiQuan2
        '  f.PhieuDatHang = gdvCT.GetFocusedRowCellValue("SoPhieu")
        '  f.btLuuLai.Visible = False
        'If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.KeToan) Then
        '    f.btnDuaSangHoaDon.Enabled = True
        'End If
        'f.ShowDialog()
    End Sub

    Private Sub pMenuChinh_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChinh.BeforePopup
        If gdvCT.GetFocusedRowCellValue("LoaiDH") = 2 Then
            mXemChiPhiHQ.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mXemChiPhiHaiQuanCu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mChuyenDH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuLapHoaDonDauVao.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            mXemChiPhiHQ.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mXemChiPhiHaiQuanCu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mChuyenDH.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuLapHoaDonDauVao.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            If gdvCT.GetFocusedRowCellValue("LoaiDH") = 0 Then
                mChuyenDH.Caption = "Chuyển sang đặt hàng phụ"
            Else
                mChuyenDH.Caption = "Chuyển sang đặt hàng thường"
            End If
        End If
    End Sub

    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            btNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvTongHopCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvTongHopCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "CanhBao" Then
            If Not gdvTongHopCT.GetRowCellValue(e.RowHandle, "ConSX") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "LuongDat" Then
            If gdvTongHopCT.GetRowCellValue(e.RowHandle, "LuongDat") > gdvTongHopCT.GetRowCellValue(e.RowHandle, "DMTon") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub mChuyenDH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenDH.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            ShowCanhBao("Bạn cần liên hệ trưởng phòng kinh doanh để thực hiện thao tác này !")
            Exit Sub
        End If

        If gdvCT.GetFocusedRowCellValue("LoaiDH") = 0 Then
            If Not ShowCauHoi("Xác nhận chuyển đặt hàng thành đặt hàng phụ ?") Then Exit Sub
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT count(ID) FROM PHIEUNHAPKHO WHERE SoPhieuDH='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'")
            If tb Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            Else
                If tb.Rows(0)(0) > 0 Then
                    ShowCanhBao("Đặt hàng đã nhập kho không thể sửa đổi !")
                    Exit Sub
                Else
                    BeginTransaction()
                    If ExecuteSQLNonQuery("UPDATE DATHANG SET SoPhieuPhu=SoPhieu, SoPhieu=null WHERE SoPhieu='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "' UPDATE PHIEUDATHANG SET LoaiDH=1 WHERE SoPhieu='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'") Is Nothing Then
                        RollBackTransaction()
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ComitTransaction()
                        ShowAlert("Đã chuyển !")
                        Dim index As Integer = gdvCT.FocusedRowHandle
                        btXem.PerformClick()
                        gdvCT.ClearSelection()
                        gdvCT.FocusedRowHandle = index
                        gdvCT.SelectRow(index)
                    End If
                End If
            End If
        Else
            If Not ShowCauHoi("Xác nhận chuyển đặt hàng thành đặt hàng thường ?") Then Exit Sub

            Dim tb As DataTable = ExecuteSQLDataTable("SELECT count(ID) FROM DATHANG WHERE ISNULL(SoPhieu,SoPhieuPhu)<>SoPhieuPhu AND SoPhieuPhu='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'")
            If tb Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            Else
                If tb.Rows(0)(0) > 0 Then
                    ShowCanhBao("Đặt hàng phụ đã được dùng cho đặt hàng nước ngoài !")
                    Exit Sub
                Else
                    BeginTransaction()
                    If ExecuteSQLNonQuery("UPDATE DATHANG SET SoPhieu=SoPhieuPhu, SoPhieuPhu=null WHERE SoPhieuPhu='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "' UPDATE PHIEUDATHANG SET LoaiDH=0 WHERE SoPhieu='" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'") Is Nothing Then
                        RollBackTransaction()
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ComitTransaction()
                        ShowAlert("Đã chuyển !")
                        Dim index As Integer = gdvCT.FocusedRowHandle
                        btXem.PerformClick()
                        gdvCT.ClearSelection()
                        gdvCT.FocusedRowHandle = index
                        gdvCT.SelectRow(index)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub pMTongHop_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMTongHop.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvTongHopCT.CalcHitInfo(gdvTongHop.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
            'ElseIf HitInfo.RowHandle < 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'ElseIf HitInfo.RowHandle >= 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub btCapNhatCanTon_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatCanTon.Click
        If ShowCauHoi("Thay đổi trạng thái cần tồn của vật tư ?") Then
            gdvTongHopCT.BeginUpdate()
            For i As Integer = 0 To gdvTongHopCT.RowCount - 1
                gdvTongHopCT.SetRowCellValue(i, "Ton", chkTickCanTon.Checked)
            Next
            gdvTongHopCT.EndUpdate()
        End If
    End Sub

    Private Sub btCapNhatConSX_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatConSX.Click
        If ShowCauHoi("Thay đổi trạng thái còn sx của vật tư ?") Then
            gdvTongHopCT.BeginUpdate()
            For i As Integer = 0 To gdvTongHopCT.RowCount - 1
                gdvTongHopCT.SetRowCellValue(i, "ConSX", chkTickConSX.Checked)
            Next
            gdvTongHopCT.EndUpdate()
        End If
    End Sub

    Private Sub pMenuDH_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuDH.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvDatHangCT.CalcHitInfo(gdvDatHang.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
        If gdvDatHangCT.FocusedColumn.FieldName = "NgayVe" Then
            mCapNhatNgayVe.Caption = "Cập nhật ngày về ""từ ngày"" cho các mục bên dưới"
        ElseIf gdvDatHangCT.FocusedColumn.FieldName = "NgayVe2" Then

            mCapNhatNgayVe.Caption = "Cập nhật ngày về ""đến ngày"" cho các mục bên dưới"
        Else
            mCapNhatNgayVe.Caption = "Cập nhật ngày về cho các mục bên dưới"

        End If

    End Sub

    Private Sub pMenuPhu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuPhu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvChiTietDHCT.CalcHitInfo(gdvChiTietDH.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvChiTietDHCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvChiTietDHCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "SoLuong" Then
            If gdvChiTietDHCT.GetRowCellValue(e.RowHandle, "SoLuong") > gdvChiTietDHCT.GetRowCellValue(e.RowHandle, "DMTon") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
        If gdvChiTietDHCT.GetRowCellValue(e.RowHandle, "lamhaiquan3") = True Then
            If e.Column.FieldName = "lamhaiquan" Then
                e.Appearance.BackColor = Color.Pink
            End If
        End If
    End Sub

    Private Sub btMaCanTon_Click(sender As System.Object, e As System.EventArgs) Handles btMaCanTon.Click
        Dim sql As String = ""
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.CanTon_SoLanXuat
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.CanTon_SoKH
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        Dim _SoLanXuat As Double = ds.Tables(0).Rows(0)(0)
        Dim _SoKH As Double = ds.Tables(1).Rows(0)(0)
        With gdvTongHopCT
            .BeginUpdate()

            For i As Integer = 0 To .RowCount - 1
                If .GetRowCellValue(i, "SLanXuat") >= _SoLanXuat And .GetRowCellValue(i, "KHSD") >= _SoKH And .GetRowCellValue(i, "ConSX") And .GetRowCellValue(i, "ThongDung") Then
                    .SetRowCellValue(i, "Ton", True)
                Else
                    .SetRowCellValue(i, "Ton", False)
                End If
            Next

            .EndUpdate()
        End With


    End Sub

    Private Sub btTonToiThieu_Click(sender As System.Object, e As System.EventArgs) Handles btTonToiThieu.Click
        Dim sql As String = ""
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.TonMin_XXX
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.TonMin_YYY
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.TonMin_MauSoChia

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        Dim _XXX As Double = ds.Tables(0).Rows(0)(0)
        Dim _YYY As Double = ds.Tables(1).Rows(0)(0)
        Dim _MauSo As Double = ds.Tables(2).Rows(0)(0)
        Dim _tmp1 As Double = 0
        Dim _tmp2 As Double = 0
        With gdvTongHopCT
            .BeginUpdate()

            For i As Integer = 0 To .RowCount - 1
                If .GetRowCellValue(i, "Ton") Then
                    If ((.GetRowCellValue(i, "XuatMax") / .GetRowCellValue(i, "SLXuat")) * 100) <= _YYY Then
                        _tmp1 = .GetRowCellValue(i, "XuatMax")
                        _tmp2 = Math.Round(.GetRowCellValue(i, "SLXuat") / _MauSo, 0, MidpointRounding.AwayFromZero)
                        If _tmp2 = 0 Then _tmp2 = 1
                    Else
                        _tmp1 = 0
                        _tmp2 = Math.Round((.GetRowCellValue(i, "SLXuat") - .GetRowCellValue(i, "XuatMax")) / _MauSo, 0, MidpointRounding.AwayFromZero)
                        If _tmp2 = 0 Then _tmp2 = 1
                    End If
                    .SetRowCellValue(i, "DMTon", Math.Max(_tmp1, _tmp2))
                Else
                    .SetRowCellValue(i, "DMTon", 0)
                End If
            Next

            .EndUpdate()
        End With

    End Sub

    Private Sub btDatToiThieu_Click(sender As System.Object, e As System.EventArgs) Handles btDatToiThieu.Click
        Dim sql As String = ""
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_MauSoChia
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ3
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ3
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ2
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ2
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SLXuatMOQ1
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_SoKHMOQ1
        sql &= " SELECT Diem FROM tblTuDien WHERE Loai = " & LoaiTuDien.DatMin_XuatMax
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        Dim _MauSoChia As Double = ds.Tables(0).Rows(0)(0)
        Dim _SLXuatMOQ3 As Double = ds.Tables(1).Rows(0)(0)
        Dim _SoKHMOQ3 As Double = ds.Tables(2).Rows(0)(0)
        Dim _SLXuatMOQ2 As Double = ds.Tables(3).Rows(0)(0)
        Dim _SoKHMOQ2 As Double = ds.Tables(4).Rows(0)(0)
        Dim _SLXuatMOQ1 As Double = ds.Tables(5).Rows(0)(0)
        Dim _SoKHMOQ1 As Double = ds.Tables(6).Rows(0)(0)
        Dim _XuatMax As Double = ds.Tables(7).Rows(0)(0)

        Dim tmp0 As Double = 0
        Dim tmp1 As Double = 0
        Dim tmp2 As Double = 0
        Dim isMOQ3 As Boolean = False
        Dim isMOQ2 As Boolean = False
        With gdvTongHopCT
            .BeginUpdate()

            For i As Integer = 0 To .RowCount - 1
                If .GetRowCellValue(i, "Ton") Then
                    If .GetRowCellValue(i, "XuatMax") / .GetRowCellValue(i, "SLXuat") * 100 <= _XuatMax Then
                        tmp0 = .GetRowCellValue(i, "SLXuat")
                    Else
                        tmp0 = .GetRowCellValue(i, "SLXuat") - .GetRowCellValue(i, "XuatMax")
                    End If
                    tmp1 = Math.Round(tmp0 / _MauSoChia, 0, MidpointRounding.AwayFromZero)
                    tmp2 = 0

                    If IsNumeric(.GetRowCellValue(i, "SLMOQ3")) Then
                        If .GetRowCellValue(i, "SLMOQ3") > 0 Then
                            If tmp0 >= (_SLXuatMOQ3 / 100) * .GetRowCellValue(i, "SLMOQ3") And .GetRowCellValue(i, "KHSD") >= _SoKHMOQ3 Then
                                tmp2 = .GetRowCellValue(i, "SLMOQ3")
                                isMOQ3 = True
                            Else
                                isMOQ3 = False
                            End If
                        Else
                            isMOQ3 = False
                        End If
                    Else
                        isMOQ3 = False
                    End If

                    If Not isMOQ3 Then
                        If IsNumeric(.GetRowCellValue(i, "SLMOQ2")) Then
                            If .GetRowCellValue(i, "SLMOQ2") > 0 Then
                                If tmp0 >= (_SLXuatMOQ2 / 100) * .GetRowCellValue(i, "SLMOQ2") And .GetRowCellValue(i, "KHSD") >= _SoKHMOQ2 Then
                                    tmp2 = .GetRowCellValue(i, "SLMOQ2")
                                    isMOQ2 = True
                                Else
                                    isMOQ2 = False
                                End If
                            Else
                                isMOQ2 = False
                            End If
                        Else
                            isMOQ2 = False
                        End If
                    End If

                    If Not isMOQ2 Then
                        If IsNumeric(.GetRowCellValue(i, "SLMOQ1")) Then
                            If .GetRowCellValue(i, "SLMOQ1") > 0 Then
                                If tmp0 >= (_SLXuatMOQ1 / 100) * .GetRowCellValue(i, "SLMOQ1") And .GetRowCellValue(i, "KHSD") >= _SoKHMOQ1 Then
                                    tmp2 = .GetRowCellValue(i, "SLMOQ1")
                                End If
                            End If
                        End If
                    End If

                    .SetRowCellValue(i, "DatToiThieu", Math.Max(tmp1, tmp2))
                Else
                    .SetRowCellValue(i, "DatToiThieu", 1)
                End If
            Next

            .EndUpdate()
        End With

    End Sub

    Private Sub chkGiaNhap_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGiaNhap.CheckedChanged
        colGiaNhap.Visible = chkGiaNhap.Checked
    End Sub

    Private Sub btNCTinhToan_Click(sender As System.Object, e As System.EventArgs) Handles btNCTinhToan.Click
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        'Dim f As New frmCNThamSoTinhTon
        'f.ShowDialog()
    End Sub

    Private Sub gdvDatHangCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvDatHangCT.KeyDown
        If cbLoaiDH.EditValue = 2 Then Exit Sub
        If e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvDatHangCT.FocusedRowHandle = 0 Then Exit Sub
            Dim _tmp As Object = gdvDatHangCT.GetFocusedRowCellValue("AZ")
            gdvDatHangCT.SetFocusedRowCellValue("AZ", gdvDatHangCT.GetRowCellValue(gdvDatHangCT.FocusedRowHandle - 1, "AZ"))
            gdvDatHangCT.SetRowCellValue(gdvDatHangCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvDatHangCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvDatHangCT.FocusedRowHandle = gdvDatHangCT.RowCount - 1 Then Exit Sub
            Dim _tmp As Object = gdvDatHangCT.GetFocusedRowCellValue("AZ")
            gdvDatHangCT.SetFocusedRowCellValue("AZ", gdvDatHangCT.GetRowCellValue(gdvDatHangCT.FocusedRowHandle + 1, "AZ"))
            gdvDatHangCT.SetRowCellValue(gdvDatHangCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvDatHangCT.FocusedRowHandle -= 1

        End If
    End Sub

    Private Sub mDuKienThanhToan_Click(sender As System.Object, e As System.EventArgs) Handles mDuKienThanhToan.Click
        If tbSoPhieuDH.EditValue.ToString = "" Then
            ShowCanhBao("Phải lưu lại đặt hàng trước khi cập nhật thông tin dự kiến thanh toán !")
            Exit Sub
        End If
        'Dim f As New frmDuKienThanhToan
        'f._SoPhieuCGDH = tbSoPhieuDH.EditValue
        'f._PhaiTra = True
        'f._Buoc1 = True
        'f.ShowDialog()
    End Sub

    Private Sub mDuKienTT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienTT.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        'Dim f As New frmDuKienThanhToan
        'f._SoPhieuCGDH = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieu")
        'f._PhaiTra = True
        'f._Buoc1 = True
        'f.ShowDialog()
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.Column.FieldName = "CBTT" Then
            If gdvCT.GetRowCellValue(e.RowHandle, "TTTT") = 0 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvCT.GetRowCellValue(e.RowHandle, "TTTT") = 2 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub mCapNhatNgayVe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCapNhatNgayVe.ItemClick
        If gdvDatHangCT.FocusedColumn.FieldName = "NgayVe" Then
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
            gdvDatHangCT.BeginUpdate()
            If gdvDatHangCT.FocusedRowHandle < 0 Then Exit Sub
            Dim objNgayVe As Object = gdvDatHangCT.GetFocusedRowCellValue("NgayVe")
            For i As Integer = gdvDatHangCT.FocusedRowHandle + 1 To gdvDatHangCT.DataRowCount - 1
                gdvDatHangCT.SetRowCellValue(i, "NgayVe", objNgayVe)
            Next
            gdvDatHangCT.EndUpdate()
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
        ElseIf gdvDatHangCT.FocusedColumn.FieldName = "NgayVe2" Then
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
            gdvDatHangCT.BeginUpdate()
            If gdvDatHangCT.FocusedRowHandle < 0 Then Exit Sub
            Dim objNgayVe As Object = gdvDatHangCT.GetFocusedRowCellValue("NgayVe2")
            For i As Integer = gdvDatHangCT.FocusedRowHandle + 1 To gdvDatHangCT.DataRowCount - 1
                gdvDatHangCT.SetRowCellValue(i, "NgayVe2", objNgayVe)
            Next
            gdvDatHangCT.EndUpdate()
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
        Else
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
            gdvDatHangCT.BeginUpdate()
            If gdvDatHangCT.FocusedRowHandle < 0 Then Exit Sub
            Dim objNgayVe As Object = gdvDatHangCT.GetFocusedRowCellValue("NgayVe")
            Dim objNgayVe2 As Object = gdvDatHangCT.GetFocusedRowCellValue("NgayVe2")
            For i As Integer = gdvDatHangCT.FocusedRowHandle + 1 To gdvDatHangCT.DataRowCount - 1
                gdvDatHangCT.SetRowCellValue(i, "NgayVe", objNgayVe)
                gdvDatHangCT.SetRowCellValue(i, "NgayVe2", objNgayVe2)
            Next
            gdvDatHangCT.EndUpdate()
            gdvDatHangCT.CloseEditor()
            gdvDatHangCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        If gdvCT.GetFocusedRowCellDisplayText("SoPhieuO").ToString.Trim <> "" Then
            chkDatHangOmron.Checked = True
        Else
            chkDatHangOmron.Checked = False
        End If
    End Sub

    Private Sub mXemChiPhiHaiQuanCu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChiPhiHaiQuanCu.ItemClick
        'Dim f As New frmThongTinHaiQuan
        'f.PhieuDatHang = gdvCT.GetFocusedRowCellValue("SoPhieu")
        'f.btLuuLai.Visible = False
        'f.ShowDialog()
    End Sub


    Private Sub mnuLapHoaDonDauVao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLapHoaDonDauVao.ItemClick

        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        'If gdvCT.GetFocusedRowCellValue("LoaiDH") <> 2 Then
        '    ShowCanhBao("Vui lòng chọn đặt hàng nước ngoài để lập hóa đơn!")
        '    Exit Sub
        'End If


        'TrangThai.isAddNew = True

        'Dim fUpdateHdDauVao As New frmUpdateHdDauVao
        'fUpdateHdDauVao.LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai


        'fUpdateHdDauVao.Text = "Nhập hóa đơn mới (" & NguoiDung & ")"
        'fUpdateHdDauVao.txtNguoiLienHe.Focus()
        'fUpdateHdDauVao.isDangXuatKho = True


        ''Bind du lieu chung len hoa don
        'fUpdateHdDauVao.cmbDoiTuong.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachhang").ToString
        'fUpdateHdDauVao.cmbTienTe.SelectedItem = fUpdateHdDauVao.cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.Ten = "USD").FirstOrDefault
        'Dim sql As String = ""
        'Dim dt As DataTable
        'sql = "select TyGiaNhapKho,SoToKhai from dathangqt where SoPhieuDatHang = '" & gdvCT.GetFocusedRowCellValue("SoPhieu").ToString & "'"
        'dt = ExecuteSQLDataTable(sql)
        'fUpdateHdDauVao.txtTyGia.EditValue = dt.Rows(0)(0)
        'fUpdateHdDauVao.txtSoHoaDon.EditValue = dt.Rows(0)(1).ToString

        'For i As Integer = 0 To gdvChiTietDHCT.RowCount - 1
        '    Dim ref As String = ChungTu.getRef
        '    Dim diengiai As String

        '    'Thêm dòng cho hàng tiền
        '    With fUpdateHdDauVao.gdvHangTienCT
        '        .AddNewRow()
        '        .SetFocusedRowCellValue("ref", ref)
        '        .SetFocusedRowCellValue("IdVatTu", gdvChiTietDHCT.GetRowCellValue(i, "IDVatTu"))
        '        diengiai = gdvChiTietDHCT.GetRowCellValue(i, "TenVT") & " " & gdvChiTietDHCT.GetRowCellValue(i, "Model")
        '        .SetFocusedRowCellValue("DienGiai", diengiai)
        '        .SetFocusedRowCellValue("DVT", gdvChiTietDHCT.GetRowCellValue(i, "DVT"))
        '        .SetFocusedRowCellValue("SoLuong", gdvChiTietDHCT.GetRowCellValue(i, "SoLuong"))
        '        .SetFocusedRowCellValue("DonGia", gdvChiTietDHCT.GetRowCellValue(i, "FOB"))
        '        .SetFocusedRowCellValue("TaiKhoanCo", "331")
        '        .SetFocusedRowCellValue("TaiKhoanNo", "1561")
        '        .SetFocusedRowCellValue("IdChiTiet", gdvChiTietDHCT.GetRowCellValue(i, "IDDatHang"))
        '    End With


        '    '***** thue nhap khau ***************************
        '    fUpdateHdDauVao.gdvDataThueNK.AddNewRow()
        '    fUpdateHdDauVao.gdvDataThueNK.SetFocusedRowCellValue("ref", ref)
        '    fUpdateHdDauVao.gdvDataThueNK.SetFocusedRowCellValue("Thue", 0)
        '    fUpdateHdDauVao.gdvDataThueNK.SetFocusedRowCellValue("DienGiai", diengiai)
        '    fUpdateHdDauVao.gdvDataThueNK.SetFocusedRowCellValue("TaiKhoanNo", "1561")
        '    fUpdateHdDauVao.gdvDataThueNK.SetFocusedRowCellValue("TaiKhoanCo", "3333")
        '    fUpdateHdDauVao.gdvDataThueNK.SetFocusedRowCellValue("ThanhTien", 0)



        '    'Thêm dòng cho thuế
        '    With fUpdateHdDauVao.gdvThueCT
        '        .AddNewRow()
        '        .SetFocusedRowCellValue("ref", ref)
        '        .SetFocusedRowCellValue("IdVatTu", gdvChiTietDHCT.GetRowCellValue(i, "IDVattu"))
        '        .SetFocusedRowCellValue("DienGiai", diengiai)
        '        .SetFocusedRowCellValue("TaiKhoanNo", "1331")
        '        .SetFocusedRowCellValue("TaiKhoanCo", "331")
        '        .SetFocusedRowCellValue("IdChiTiet", gdvChiTietDHCT.GetRowCellValue(i, "IDDatHang"))

        '        Dim thanhtien = Math.Round(gdvChiTietDHCT.GetRowCellValue(i, "SoLuong") * gdvChiTietDHCT.GetRowCellValue(i, "GiaNK"), 2, MidpointRounding.AwayFromZero)
        '        Dim tienthue = Math.Round((thanhtien * 10) / 100, 2, MidpointRounding.AwayFromZero)
        '        .SetFocusedRowCellValue("ThanhTien", tienthue)
        '    End With



        'Next

        'ShowAlert("Nhập hóa đơn " & gdvCT.GetFocusedRowCellValue("TenKH").ToString)
        'fUpdateHdDauVao.ShowDialog()

    End Sub

    Private Sub btTHXuatExcel_Click(sender As System.Object, e As System.EventArgs) Handles btTHXuatExcel.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub

        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "TH_DatHang.xls"
        saveFile.OverwritePrompt = False
        If saveFile.ShowDialog = DialogResult.OK Then

            Try
                ShowWaiting("Đang kết xuất ...")
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvTongHopCT, False)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                CloseWaiting()
            End Try

        End If
    End Sub
#Region "Tài"
    Protected Shared _message As Integer = 0
    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property
    Public Structure _ChaoGia
        Public IDKhachhang As Integer
        Public IDnguoilap As Integer
        Public tgchuyentuchaogia As Date
        Public idtinhtrang_haiquan As Integer
    End Structure
    Public Structure _ChaoGiaCT
        '   Public idlamhaiquan As Integer
        Public idchaogia As Integer
        Public tendexuat As String
    End Structure
    Public tgKH As Integer = -1
    Public tgidlamhaiquan As Integer = -1
    Public chaogia As _ChaoGia = New _ChaoGia
    'Public chaogiaCT As _ChaoGiaCT() = New _ChaoGiaCT() {}
    Public d As Integer = 0
    Private Sub riCeHaiQuan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles riCeLamHaiQuan.EditValueChanged
        gdvChiTietDHCT.PostEditor()
        gdvChiTietDHCT.UpdateCurrentRow()
    End Sub

    Private Sub barBtnLamHQ_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnLamHQ.ItemClick

        If isHienThiT = False Then
            If tgidlamhaiquan = -1 Then
                tgKH = gdvCT.GetFocusedRowCellValue("IDKhachhang")
                Dim query = "select count(IDKhachhang) from HaiQuan_LamHaiQuan where  MONTH(tgchuyensanghaiquan )=MONTH (getdate())  and IDKhachhang=" & tgKH.ToString()
                Dim lan = ExecuteSQLScalar(query)
                AddParameter("@IDKhachhang", chaogia.IDKhachhang)
                AddParameter("@IDnguoilap", chaogia.IDnguoilap)
                AddParameter("@tgchuyentuchaogia", Now)
                AddParameter("@idtinhtrang_haiquan", chaogia.idtinhtrang_haiquan)
                '  AddParameter("@tgchuyensanghaiquan", Today)
                AddParameter("@lan", lan + 1)
                If doInsert("HaiQuan_LamHaiQuan") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If

            If tgKH <> -1 Then
                If tgKH <> gdvCT.GetFocusedRowCellValue("IDKhachhang") Then
                    ShowAlert("Chỉ được chọn một khách hàng")
                Else
                    ' gdvCGCT.UpdateCurrentRow()
                    ' Dim csgt As New _ChaoGiaCT in  
                    For i As Integer = 0 To gdvCT.DataRowCount - 1
                        If gdvChiTietDHCT.GetRowCellValue(i, "lamhaiquan") = True And gdvChiTietDHCT.GetRowCellValue(i, "lamhaiquan2") = False Then
                            If tgidlamhaiquan = -1 Then
                                AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                            Else
                                AddParameter("@idlamhaiquan", tgidlamhaiquan)
                            End If

                            AddParameter("@idchaogia", gdvChiTietDHCT.GetRowCellValue(i, "IDDatHang"))
                            AddParameter("@tendexuat", gdvChiTietDHCT.GetRowCellValue(i, "TenVT"))
                            If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        End If
                    Next i
                    'For Each cgct As _ChaoGiaCT In chaogiaCT
                    '    If tgidlamhaiquan = -1 Then
                    '        AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                    '    Else
                    '        AddParameter("@idlamhaiquan", tgidlamhaiquan)
                    '    End If

                    '    AddParameter("@idchaogia", cgct.idchaogia)
                    '    AddParameter("@tendexuat", cgct.tendexuat)
                    '    If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                    '        ShowBaoLoi(LoiNgoaiLe)
                    '    End If
                    'Next


                End If
            End If
            '  ReDim Preserve chaogiaCT(0)
            d = 0
            Dim frm = New frmDSYCLamHQ
            frm.Text = "Danh sách thiết bị làm hải quan"
            Dim id As Integer = 0
            If tgidlamhaiquan = -1 Then
                id = ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan where idtinhtrang_haiquan=0 order by id desc")
            Else
                id = tgidlamhaiquan
            End If
            If id = 0 Then
                frm.Message = 0
            Else
                frm.Message = id
            End If
            isHienThiT = True
            frm.Name = "frmDSYCLamHQpopup"
            frm.Show()
            frm.WindowState = FormWindowState.Maximized
            frm.btnTaiLai.PerformClick()
            '     btnBoSung.Visibility = XtraBars.BarItemVisibility.Always
        Else
            ShowCanhBao("Có hải quan đang làm, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If


        Dim row = gdvCT.FocusedRowHandle
        loadDSTongHopVatTuDatHang()
        gdvCT.FocusedRowHandle = row
    End Sub

    Private Sub btnBoSung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBoSung.ItemClick
        If tgKH <> -1 Then
            If tgKH <> gdvCT.GetFocusedRowCellValue("IDKhachhang") Then
                ShowAlert("Chỉ được chọn một khách hàng")
            Else
                If d > 0 Then
                    For i As Integer = 0 To gdvCT.DataRowCount - 1
                        If gdvChiTietDHCT.GetRowCellValue(i, "lamhaiquan") = True And gdvChiTietDHCT.GetRowCellValue(i, "lamhaiquan2") = False Then
                            If tgidlamhaiquan = -1 Then
                                AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                            Else
                                AddParameter("@idlamhaiquan", tgidlamhaiquan)
                            End If

                            AddParameter("@idchaogia", gdvCT.GetRowCellValue(i, "IDDatHang"))
                            AddParameter("@tendexuat", gdvCT.GetRowCellValue(i, "TenVT"))
                            If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        End If
                    Next i
                    'For Each cgct As _ChaoGiaCT In chaogiaCT
                    '    If tgidlamhaiquan = -1 Then
                    '        AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                    '    Else
                    '        AddParameter("@idlamhaiquan", tgidlamhaiquan)
                    '    End If

                    '    AddParameter("@idchaogia", cgct.idchaogia)
                    '    AddParameter("@tendexuat", cgct.tendexuat)
                    '    If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                    '        ShowBaoLoi(LoiNgoaiLe)

                    '    End If
                    'Next
                    'ReDim Preserve chaogiaCT(0)
                    d = 0
                    btnBoSung.Enabled = False
                End If

            End If
        End If
        If isHienThiT = True Then
            Application.OpenForms("frmDSYCLamHQpopup").WindowState = FormWindowState.Maximized
            CType(Application.OpenForms("frmDSYCLamHQpopup"), frmDSYCLamHQ).btnTaiLai.PerformClick()
        Else
            Application.OpenForms("frmDSYCLamHQ").WindowState = FormWindowState.Maximized
            'For Each tPage As DevExpress.XtraTab.XtraTabPage In deskTop.tabMain.TabPages
            '    MsgBox(tPage.Controls(0).Tag)
            '    If tPage.Controls(0).Tag = "frmDSYCLamHQ" Then
            '        deskTop.tabMain.SelectedTabPage = tPage
            '        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDSYCLamHQ).btnTaiLai.PerformClick()
            '    End If

            '    If tPage.Controls(0).Tag = "frmVatTuDaChaoGia" Then
            '        deskTop.tabMain.TabPages.Remove(tPage)
            '        hienthipopup = False
            '    End If
            'Next
            Dim d = deskTop.tabMain.TabPages.Count - 1
            Dim i As Integer
            For i = 0 To d
                If i > d Then
                    Exit For
                End If
                If deskTop.tabMain.TabPages(i).Tag = "btnDSYCLamHQ" Then
                    deskTop.tabMain.SelectedTabPage = deskTop.tabMain.TabPages(i)
                    CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDSYCLamHQ).btnTaiLai.PerformClick()
                End If

                If deskTop.tabMain.TabPages(i).Controls(0).Tag = "frmVatTuDaChaoGia" Then
                    deskTop.tabMain.TabPages.Remove(deskTop.tabMain.TabPages(i))
                    hienthipopup = False
                    i = i - 1
                    d = d - 1
                End If
            Next i
        End If

    End Sub


    Private Sub gdvChiTietDHCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvChiTietDHCT.RowCellClick
        If e.Column.Name = "gColLamHQ" Then
            If gdvChiTietDHCT.GetFocusedRowCellValue("lamhaiquan2") = False Then
                If tgKH = -1 Then
                    tgKH = gdvCT.GetFocusedRowCellValue("IDKhachhang")
                    chaogia.IDKhachhang = tgKH
                    chaogia.IDnguoilap = CType(TaiKhoan, Int32)
                    chaogia.tgchuyentuchaogia = Today
                    chaogia.idtinhtrang_haiquan = 0
                End If

                If tgKH <> -1 Then
                    If tgKH <> gdvCT.GetFocusedRowCellValue("IDKhachhang") Then
                        ShowCanhBao("Chỉ được chọn một khách hàng")
                    Else
                        If gdvChiTietDHCT.GetFocusedRowCellValue("lamhaiquan") = False Then
                            gdvChiTietDHCT.SetFocusedRowCellValue("lamhaiquan", True)
                            'ReDim Preserve chaogiaCT(d)
                            'chaogiaCT(d).idchaogia = gdvChiTietDHCT.GetFocusedRowCellValue("IDDatHang")
                            'chaogiaCT(d).tendexuat = gdvChiTietDHCT.GetFocusedRowCellValue("TenVT")
                            d += 1
                        Else
                            gdvChiTietDHCT.SetFocusedRowCellValue("lamhaiquan", False)
                            d -= 1
                        End If
                    End If
                End If
            Else
                ShowCanhBao("đã làm hải quan không hủy được nữa")
            End If

            If _message = 0 Or hienthipopup = False Then
                If d > 0 And isHienThiT = False Then
                    barBtnLamHQ.Enabled = True
                    btnBoSung.Enabled = False
                Else
                    If d > 0 Then
                        barBtnLamHQ.Enabled = False
                        btnBoSung.Enabled = True
                    Else
                        If isHienThiT = False Then
                            tgKH = -1
                        End If
                        barBtnLamHQ.Enabled = False
                        btnBoSung.Enabled = False
                    End If

                End If
            Else
                If d > 0 Then
                    btnBoSung.Enabled = True
                Else
                    btnBoSung.Enabled = False
                End If
            End If
        End If

    End Sub
    Private Sub Control1_HandleDestroyed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        If isHienThiT = True Then
            ShowCanhBao("Đóng form Danh sách thiết bị làm hải quan trước khi làm tiếp")
        End If
    End Sub
#End Region
End Class