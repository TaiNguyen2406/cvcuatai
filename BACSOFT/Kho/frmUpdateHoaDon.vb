Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT
Imports DevExpress.XtraEditors


Public Class frmUpdateHoaDon

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        loadKhachHang()
        loadTienTe()
        txtNgayHoaDon.EditValue = GetServerTime()

        'cmbHinhThucTT.SelectedIndex = 2 'Mặc định TM/CK
        Dim objHinhThucThanhToan As New HinhThucThanhToan
        objHinhThucThanhToan.KhoiTao()
        For Each obj As HinhThucThanhToan In objHinhThucThanhToan.DanhSach
            cmbHinhThucTT.Properties.Items.Add(obj)
        Next
        'Mặc định trạng thái hóa đơn
        cmbHinhThucTT.SelectedItem = cmbHinhThucTT.Properties.Items.Cast(Of HinhThucThanhToan).Where(Function(x) x.GiaTri = HinhThucThanhToan.TrangThai.TMCK).FirstOrDefault

        'Mặc định tiền tệ VNĐ
        cmbTienTe.SelectedItem = cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.Ten = "VNĐ").FirstOrDefault


        'Load ds trạng thái hóa đơn
        Dim objtrangThaiHoaDon As New TrangThaiHoaDon
        objtrangThaiHoaDon.KhoiTao()
        For Each obj As TrangThaiHoaDon In objtrangThaiHoaDon.DanhSach
            cmbTrangThai.Properties.Items.Add(obj)
        Next

        'Mặc định trạng thái hóa đơn
        cmbTrangThai.SelectedItem = cmbTrangThai.Properties.Items.Cast(Of TrangThaiHoaDon).Where(Function(x) x.GiaTri = TrangThaiHoaDon.TrangThai.HoaDonNhap).FirstOrDefault

        'Mặc định ký hiệu và số Hóa Đơn
        Dim sql As String = "SELECT TOP 1 KyHieuHD,(CONVERT(BIGINT,SoHD) + 1)SoHD FROM CHUNGTU WHERE LoaiCT = @LoaiCT  and KyHieuHD is not null and SoHD is not null ORDER BY NgayCT DESC, CONVERT(BIGINT,SoHD) DESC "
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        cmbSoTK.Properties.DataSource = ExecuteSQLDataTable("select ID, MaSo, Ten from taikhoan order by ten")

        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            txtKyHieuHoaDon.Text = dt.Rows(0)("KyHieuHD").ToString
            Dim sohd As String = dt.Rows(0)("SoHD").ToString
            Dim SoCT As String = ""
            For i As Integer = 1 To 7 - sohd.Length
                SoCT &= "0"
            Next
            txtSoHoaDon.Text = SoCT & sohd
        End If


        If TrangThai.isAddNew Or TrangThai.isCopy Then
            LoadChiTietHangTien(-1)
            LoadChiTietThue(-1)
        End If

        If HoaDonGTGT.CacheData.dataVatTu Is Nothing Then
            LoadDsVT()
        Else
            'gdvVT.DataSource = HoaDonGTGT.CacheData.dataVatTu
        End If

        'LoadDsNhomVT()
        'LoadDsTenVT()
        'LoadDsHangSX()
        'rcmbMaVT.DataSource = ExecuteSQLDataTable("SELECT N'' as Model")

        LoadDsTaiKhoan()



        'splHangHoa.Collapsed = True

    End Sub



    Public isDangXuatKho As Boolean = False
    Public idHoaDon As Object
    Public SoHoaDonCu As String = ""
    Public TagX As String = ""

    Public LoaiCT2 As ChungTu.LoaiCT2



    Private Sub frmUpdateHoaDon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        If LoaiCT2 = ChungTu.LoaiCT2.BanDichVu Then
            mnuThemDongVatTu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuThemMaVtMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuGopSoLuongVT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuHuyNoiDungGop.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gdvHangTienCT.Columns("IdVatTu").Visible = False
            gdvHangTienCT.Columns("SoLuong").Visible = False
            gdvHangTienCT.Columns("DonGia").Visible = False
            gdvHangTienCT.Columns("DVT").Visible = False
            gdvHangTienCT.Columns("GhiChuKhac").Visible = False
            tabGiaVon.PageEnabled = False
        End If

        If TrangThai.isUpdate Or TrangThai.isCopy Then

            Dim sql As String = "SELECT * FROM CHUNGTU WHERE Id = @Id"
            AddParameter("@Id", idHoaDon)
            Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

            cmbDoiTuong.EditValue = r("IdKH")
            txtTenDoiTuong.EditValue = r("TenKH")
            txtDiaChi.EditValue = r("DiaChi")
            txtMST.EditValue = r("MaSoThue")
            txtNguoiLienHe.EditValue = r("NguoiLienHe")

            cmbSoTK.EditValue = r("SoTkNganHang")
            txtTenTaiKhoan.EditValue = r("TenTkNganHang")

            cmbHinhThucTT.SelectedItem = cmbHinhThucTT.Properties.Items.Cast(Of HinhThucThanhToan).Where(Function(x) x.GiaTri = r("HtThanhToan")).FirstOrDefault
            cmbTienTe.SelectedItem = cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.ID = r("TienTe")).FirstOrDefault
            txtTyGia.EditValue = r("TyGia")

            If r("Thue") Is DBNull.Value Then
                chkKhongChiuThue.Checked = True
            Else
                chkKhongChiuThue.Checked = False
                txtThue.EditValue = r("Thue")
            End If

            chkKemBangKe.Checked = r("KemBangKe")

            txtDienGiaiChung.EditValue = r("DienGiai")
            txtNgayHoaDon.EditValue = r("NgayHD")
            txtKyHieuHoaDon.EditValue = r("KyHieuHD")
            txtSoHoaDon.EditValue = r("SoHD")
            SoHoaDonCu = r("SoHD")

            cmbTrangThai.SelectedItem = cmbTrangThai.Properties.Items.Cast(Of TrangThaiHoaDon).Where(Function(x) x.GiaTri = r("TrangThai")).FirstOrDefault

            txtTongTienHang.EditValue = r("ThanhTien")
            txtTongTienThue.EditValue = r("TienThue")
            txtTongThanhTien.EditValue = r("TongTien")

            cmbBanHang.Text = r("NguoiDaiDien").ToString
            cmbSoTaiKhoan.Text = r("TaiKhoanNganHang").ToString

            LoadChiTietHangTien(idHoaDon)
            LoadChiTietThue(idHoaDon)
            LoadChiTietGiaVon(idHoaDon)

            If TrangThai.isUpdate Then
                If Convert.ToBoolean(r("GhiSo")) Then
                    groupThongTinChung.AppearanceCaption.ForeColor = Color.Green
                    groupThongTinChung.Text = "Thông tin chung (Đã ghi sổ)"
                Else
                    groupThongTinChung.AppearanceCaption.ForeColor = Color.Navy
                    groupThongTinChung.Text = "Thông tin chung (Chưa ghi sổ)"
                End If
            End If

            btInHoaDon.Enabled = True


        End If


        If TrangThai.isCopy Then
            txtNgayHoaDon.EditValue = GetServerTime()
            'lay lai so hoa don mac dinh
            Dim sql As String = "SELECT TOP 1 KyHieuHD,(CONVERT(BIGINT,SoHD) + 1)SoHD FROM CHUNGTU WHERE LoaiCT = @LoaiCT ORDER BY NgayCT DESC, CONVERT(BIGINT,SoHD) DESC "
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                txtKyHieuHoaDon.Text = dt.Rows(0)("KyHieuHD").ToString
                Dim sohd As String = dt.Rows(0)("SoHD").ToString
                Dim SoCT As String = ""
                For i As Integer = 1 To 7 - sohd.Length
                    SoCT &= "0"
                Next
                txtSoHoaDon.Text = SoCT & sohd
            End If
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                Dim ref As String = ChungTu.getRef
                gdvHangTienCT.SetRowCellValue(i, "ref", ref)
                gdvThueCT.SetRowCellValue(i, "ref", ref)
            Next
            gdvHangTienCT.CloseEditor()
            gdvHangTienCT.UpdateCurrentRow()
            gdvThueCT.CloseEditor()
            gdvThueCT.UpdateCurrentRow()
        End If

        txtKyHieuHoaDon.Focus()



        cmbDoiTuong.Focus()


    End Sub


    Private Sub LoadDsTaiKhoan()
        Dim sql As String = "SELECT TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE ORDER BY TaiKhoan "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next
        rcmbTaiKhoan.DataSource = tb2
    End Sub

    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub

#Region "- In Hoa Don -"

#Region "- In hoa don - "
    Public Shared Sub InHoaDon(Id As Long)
        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim sql As String = ""
            sql &= "SELECT convert(float,0) SoTT, a.NgayHD,a.SoHD,a.KyHieuHD,a.NguoiLienHe,convert(nvarchar,a.HtThanhToan)HtThanhToan,a.NguoiDaiDien,a.TaiKhoanNganHang, "
            sql &= "a.TenKH,a.DiaChi,a.MaSoThue,convert(nvarchar,a.TienTe)TienTe,a.TyGia,a.Thue,a.KemBangKe,a.DienGiai DienGiaiChung, "
            'sql &= "a.ThanhTien TongTienHang,a.TienThue as TongTienThue,a.TongTien as TongThanhTien, "

            sql &= "(SELECT SUM(ThanhTien) FROM CHUNGTUCHITIET WHERE Id_CT = a.id and buttoan = 1)TongTienHang,"
            sql &= "(SELECT SUM(ThanhTien) FROM CHUNGTUCHITIET WHERE Id_CT = a.id and buttoan = 2)TongTienThue,"
            sql &= "(SELECT SUM(ThanhTien) FROM CHUNGTUCHITIET WHERE Id_CT = a.id and buttoan in (1,2))TongThanhTien,"

            sql &= "b.DienGiai,b.DVT,b.SoLuong,b.DonGia,b.ThanhTien,b.GhiChu "
            sql &= "FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "WHERE a.id = @Id And b.ButToan = @ButToan "
            AddParameter("@Id", Id)
            AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)

            Dim f As New frmIn("Xuất hóa đơn")
            Dim rpt As New rptHoaDonGTGT

            Dim dtX As DataTable = dt.Clone
            If Convert.ToBoolean(dt.Rows(0)("KemBangKe")) Then
                Dim r As DataRow = dt.Rows(0)
                r("SoTT") = 1
                r("DienGiai") = r("DienGiaiChung")
                r("DVT") = ""
                r("SoLuong") = DBNull.Value
                r("DonGia") = DBNull.Value
                r("ThanhTien") = r("TongTienHang")
                dtX.ImportRow(r)
                rpt.DataSource = dtX
            Else
                Dim strGhiChu As String = ""
                Dim arrRow As New List(Of DataRow)
                Dim arrGhiChu As New List(Of String)

                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("GhiChu").ToString.Trim <> "" Then
                        If arrGhiChu.IndexOf(dt.Rows(i)("GhiChu").ToString) >= 0 Then
                            'Xóa dòng đó đi
                            arrRow.Add(dt.Rows(i))
                        Else
                            'Lấy giá trị và gắn vào hàng đầu tiên
                            arrGhiChu.Add(dt.Rows(i)("GhiChu").ToString)
                            Dim reg As New System.Text.RegularExpressions.Regex("\[\<\((.*?)\)\>\]", System.Text.RegularExpressions.RegexOptions.Multiline)
                            Dim arrM = reg.Matches(dt.Rows(i)("GhiChu").ToString)
                            If arrM.Count = 6 Then
                                dt.Rows(i)("DienGiai") = reg.Match(arrM(0).Value.Trim).Groups(1).Value
                                dt.Rows(i)("DVT") = reg.Match(arrM(1).Value.Trim).Groups(1).Value
                                dt.Rows(i)("SoLuong") = Convert.ToDouble(reg.Match(arrM(2).Value.Trim).Groups(1).Value)
                                dt.Rows(i)("DonGia") = Convert.ToDouble(reg.Match(arrM(3).Value.Trim).Groups(1).Value)
                                dt.Rows(i)("ThanhTien") = Convert.ToDouble(reg.Match(arrM(4).Value.Trim).Groups(1).Value)
                            End If
                        End If
                    End If
                Next
                If arrRow.Count > 0 Then
                    For Each r As DataRow In arrRow
                        dt.Rows.Remove(r)
                    Next
                End If
                'So thu tu
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("SoTT") = i + 1
                Next
                rpt.DataSource = dt
            End If



            Dim mst As String = dt.Rows(0)("MaSoThue").ToString

            For i As Integer = 1 To mst.Length
                rpt.Parameters("prm" & i).Value = mst.Chars(i - 1)
            Next
            For i As Integer = mst.Length + 1 To 14
                rpt.Parameters("prm" & i).Value = "--"
            Next

            Dim objHTTT As New HoaDonGTGT.HinhThucThanhToan
            objHTTT.KhoiTao()

            If Convert.ToBoolean(dt.Rows(0)("KemBangKe")) Then
                rpt.Parameters("rpmDocSoTien").Value = Utils.StringHelper.VIE2String(dtX.Rows(0)("TongThanhTien"), False, "đồng", "lẻ", "phẩy", 2)
                rpt.Parameters("prmHTTT").Value = _
    objHTTT.DanhSach.Cast(Of HinhThucThanhToan).Where(Function(x) x.GiaTri = dtX.Rows(0)("HtThanhToan")).FirstOrDefault.TenHinhThuc
            Else
                rpt.Parameters("rpmDocSoTien").Value = Utils.StringHelper.VIE2String(dt.Rows(0)("TongThanhTien"), False, "đồng", "lẻ", "phẩy", 2)
                rpt.Parameters("prmHTTT").Value = _
    objHTTT.DanhSach.Cast(Of HinhThucThanhToan).Where(Function(x) x.GiaTri = dt.Rows(0)("HtThanhToan")).FirstOrDefault.TenHinhThuc
            End If


            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()

            f.rpt = rpt

            f.ShowDialog()
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub
#End Region

#Region "- In hoa don nhap -"
    Public Shared Sub InHoaDonNhap(Id As Long)

        Try

            ShowWaiting("Đang tải nội dung ...")
            Dim sql As String = ""

            sql &= "SELECT a.NgayHD,a.SoHD,a.KyHieuHD,a.NguoiLienHe,convert(nvarchar,a.HtThanhToan)HtThanhToan,a.NguoiDaiDien,a.TaiKhoanNganHang, "
            sql &= "a.TenKH,a.DiaChi,a.MaSoThue,convert(nvarchar,a.TienTe)TienTe,a.TyGia,a.Thue,a.KemBangKe,a.DienGiai DienGiaiChung, "
            sql &= "a.ThanhTien TongTienHang,a.TienThue as TongTienThue,a.TongTien as TongThanhTien, "
            sql &= "b.DienGiai,b.DVT,b.SoLuong,b.DonGia,b.ThanhTien,b.GhiChu "
            sql &= "FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "WHERE a.id = @Id And b.ButToan = @ButToan "
            AddParameter("@Id", Id)
            AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            Dim dtChinh As DataTable = dt.Clone
            dtChinh.ImportRow(dt.Rows(0))

            Dim f As New frmIn("Xuất hóa đơn nháp")
            Dim rpt As New rptHoaDonNhap
            rpt.DataSource = dtChinh

            Dim dtX As DataTable = dt.Clone
            If Convert.ToBoolean(dt.Rows(0)("KemBangKe")) Then
                Dim r As DataRow = dt.Rows(0)
                r("DienGiai") = r("DienGiaiChung")
                r("DVT") = ""
                r("SoLuong") = 0
                r("DonGia") = 0
                r("ThanhTien") = r("TongTienHang")
                dtX.ImportRow(r)
                'rpt.DataSource = dtX

                For i As Integer = 0 To 10 - dtX.Rows.Count
                    rpt.tableMain.DeleteRow(rpt.tableMain.Rows(0))
                Next
                For i As Integer = 0 To dtX.Rows.Count - 1
                    rpt.tableMain.Rows(i).Cells(0).Text = i + 1
                    rpt.tableMain.Rows(i).Cells(1).Text = dtX.Rows(i)("DienGiai")
                    rpt.tableMain.Rows(i).Cells(2).Text = dtX.Rows(i)("DVT")
                    If dtX.Rows(i)("SoLuong") = 0 Then
                        rpt.tableMain.Rows(i).Cells(3).Text = ""
                    Else
                        rpt.tableMain.Rows(i).Cells(3).Text = String.Format("{0:N2}", dtX.Rows(i)("SoLuong"))
                    End If
                    If dtX.Rows(i)("DonGia") = 0 Then
                        rpt.tableMain.Rows(i).Cells(4).Text = ""
                    Else
                        rpt.tableMain.Rows(i).Cells(4).Text = String.Format("{0:N0}", dtX.Rows(i)("DonGia"))
                    End If
                    rpt.tableMain.Rows(i).Cells(5).Text = String.Format("{0:N0}", dtX.Rows(i)("ThanhTien"))
                Next

            Else
                Dim strGhiChu As String = ""
                Dim arrRow As New List(Of DataRow)
                Dim arrGhiChu As New List(Of String)

                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("GhiChu").ToString.Trim <> "" Then

                        If arrGhiChu.IndexOf(dt.Rows(i)("GhiChu").ToString) >= 0 Then
                            'Xóa dòng đó đi
                            arrRow.Add(dt.Rows(i))
                        Else
                            'Lấy giá trị và gắn vào hàng đầu tiên
                            arrGhiChu.Add(dt.Rows(i)("GhiChu").ToString)
                            Dim reg As New System.Text.RegularExpressions.Regex("\[\<\((.*?)\)\>\]", System.Text.RegularExpressions.RegexOptions.Multiline)
                            Dim arrM = reg.Matches(dt.Rows(i)("GhiChu").ToString)
                            If arrM.Count = 6 Then
                                dt.Rows(i)("DienGiai") = reg.Match(arrM(0).Value.Trim).Groups(1).Value
                                dt.Rows(i)("DVT") = reg.Match(arrM(1).Value.Trim).Groups(1).Value
                                dt.Rows(i)("SoLuong") = Convert.ToDouble(reg.Match(arrM(2).Value.Trim).Groups(1).Value)
                                dt.Rows(i)("DonGia") = Convert.ToDouble(reg.Match(arrM(3).Value.Trim).Groups(1).Value)
                                dt.Rows(i)("ThanhTien") = Convert.ToDouble(reg.Match(arrM(4).Value.Trim).Groups(1).Value)
                            End If
                        End If
                    End If
                Next
                If arrRow.Count > 0 Then
                    For Each r As DataRow In arrRow
                        dt.Rows.Remove(r)
                    Next
                End If

                For i As Integer = 0 To 10 - dt.Rows.Count
                    rpt.tableMain.DeleteRow(rpt.tableMain.Rows(0))
                Next
                For i As Integer = 0 To dt.Rows.Count - 1
                    rpt.tableMain.Rows(i).Cells(0).Text = i + 1
                    rpt.tableMain.Rows(i).Cells(1).Text = dt.Rows(i)("DienGiai").ToString
                    rpt.tableMain.Rows(i).Cells(2).Text = dt.Rows(i)("DVT").ToString
                    If dt.Rows(i)("SoLuong") = 0 Then
                        rpt.tableMain.Rows(i).Cells(3).Text = ""
                    Else
                        rpt.tableMain.Rows(i).Cells(3).Text = String.Format("{0:N2}", dt.Rows(i)("SoLuong"))
                    End If
                    If dt.Rows(i)("DonGia") = 0 Then
                        rpt.tableMain.Rows(i).Cells(4).Text = ""
                    Else
                        rpt.tableMain.Rows(i).Cells(4).Text = String.Format("{0:N0}", dt.Rows(i)("DonGia"))
                    End If
                    rpt.tableMain.Rows(i).Cells(5).Text = String.Format("{0:N0}", dt.Rows(i)("ThanhTien"))
                Next

            End If



            Dim mst As String = dt.Rows(0)("MaSoThue").ToString

            For i As Integer = 1 To mst.Length
                rpt.Parameters("prm" & i).Value = mst.Chars(i - 1)
            Next
            For i As Integer = mst.Length + 1 To 14
                rpt.Parameters("prm" & i).Value = "--"
            Next

            Dim objHTTT As New HoaDonGTGT.HinhThucThanhToan
            objHTTT.KhoiTao()

            If Convert.ToBoolean(dt.Rows(0)("KemBangKe")) Then
                rpt.Parameters("rpmDocSoTien").Value = Utils.StringHelper.VIE2String(dtX.Rows(0)("TongThanhTien"), False, "đồng", "lẻ", "phẩy", 2)
                rpt.Parameters("prmHTTT").Value = _
    objHTTT.DanhSach.Cast(Of HinhThucThanhToan).Where(Function(x) x.GiaTri = dtX.Rows(0)("HtThanhToan")).FirstOrDefault.TenHinhThuc
            Else
                rpt.Parameters("rpmDocSoTien").Value = Utils.StringHelper.VIE2String(dt.Rows(0)("TongThanhTien"), False, "đồng", "lẻ", "phẩy", 2)
                rpt.Parameters("prmHTTT").Value = _
    objHTTT.DanhSach.Cast(Of HinhThucThanhToan).Where(Function(x) x.GiaTri = dt.Rows(0)("HtThanhToan")).FirstOrDefault.TenHinhThuc
            End If


            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub
#End Region

#Region "- In bang ke -"
    Public Shared Sub InBangKe(Id As Long)


        Try

            ShowWaiting("Đang tải nội dung ...")
            Dim sql As String = ""

            sql &= "SELECT *, "
            sql &= "(SELECT Top 1 ID FROM CHUNGTUCHITIET WHERE Id_CT = tt.ID AND DonGia = tt.DonGia and DienGiai = tt.DienGiai order by ID)stt "
            sql &= "FROM "
            sql &= "( "


            sql &= "SELECT a.ID,a.NgayHD,a.SoHD,a.KyHieuHD,a.NguoiLienHe,convert(nvarchar,a.HtThanhToan)HtThanhToan,a.NguoiDaiDien, "
            sql &= "a.TenKH,a.DiaChi,a.MaSoThue,convert(nvarchar,a.TienTe)TienTe,a.TyGia,a.Thue,a.KemBangKe,a.DienGiai DienGiaiChung, "
            sql &= "a.ThanhTien TongTienHang,a.TienThue as TongTienThue,a.TongTien as TongThanhTien, "
            sql &= "b.DienGiai,b.DVT,SUM(b.SoLuong)SoLuong,b.DonGia,SUM(b.ThanhTien)ThanhTien,b.GhiChu,b.GhiChuKhac as PO "
            sql &= "FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "WHERE a.id = @Id And b.ButToan = @ButToan "


            sql &= "GROUP BY a.ID,a.NgayHD,a.SoHD,a.KyHieuHD,a.NguoiLienHe,HtThanhToan,a.NguoiDaiDien, "
            sql &= "a.TenKH,a.DiaChi,a.MaSoThue,TienTe,a.TyGia,a.Thue,a.KemBangKe,a.DienGiai, "
            sql &= "a.ThanhTien,a.TienThue,a.TongTien, b.DonGia, b.DienGiai,b.DVT,b.GhiChu, b.GhiChuKhac, a.Thue "

            sql &= ")tt "
            sql &= "ORDER BY stt "

            AddParameter("@Id", Id)
            AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)

            If dt Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            Dim f As New frmIn("In bảng kê")
            Dim rpt As New rptBangKeKemHoaDon
            rpt.DataSource = dt

            rpt.txtThue.Text = "Thuế suất GTGT: " & dt.Rows(0)("Thue") & "%"

            Dim r As DataRow = dt.Rows(0)
            rpt.Parameters("prmBangChu").Value = Utils.StringHelper.VIE2String(r("TongThanhTien"), False, "đồng", "lẻ", "phẩy", 2)

            Dim ngayhd As DateTime = Convert.ToDateTime(r("NgayHD"))
            rpt.Parameters("prmKemTheoHoaDon").Value = String.Format("( Kèm theo hóa đơn GTGT mẫu số 01GTKT3/001;ký hiệu {0} số: {1} ngày {2} tháng {3} năm {4} )", r("KyHieuHD").ToString.Trim, r("SoHD"), ngayhd.ToString("dd"), ngayhd.ToString("MM"), ngayhd.ToString("yyyy"))

            Dim strSoBangKe As String = r("DienGiaiChung").ToString.ToLower
            Dim reg As New System.Text.RegularExpressions.Regex("số(.*)ngày(.*)tháng(.*)năm(.*)\d{4}")
            Dim m As System.Text.RegularExpressions.Match = reg.Match(strSoBangKe)
            If m.Success Then
                rpt.Parameters("prmSoBangKe").Value = m.Value
            Else
                rpt.Parameters("prmSoBangKe").Value = ""
            End If

            Dim kt As Boolean = False
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("PO").ToString.Trim <> "" Then
                    kt = True
                    Exit For
                End If
            Next


            If Not kt Then

                rpt.tbl1.DeleteColumn(rpt.tbl1.Rows.FirstRow.Cells(2))
                rpt.tbl2.DeleteColumn(rpt.tbl2.Rows.FirstRow.Cells(2))

                rpt.tbl1.Rows.FirstRow.Cells(0).WidthF = rpt.tbl3.Rows.FirstRow.Cells(0).WidthF
                rpt.tbl2.Rows.FirstRow.Cells(0).WidthF = rpt.tbl3.Rows.FirstRow.Cells(0).WidthF


                rpt.tbl3.Rows(0).Cells(1).WidthF -= 28
                rpt.tbl3.Rows(1).Cells(2).WidthF -= 28
                rpt.tbl3.Rows(2).Cells(1).WidthF -= 28


            End If

            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub
#End Region

#End Region




    Public Sub loadKhachHang()
        Dim sql As String = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cmbDoiTuong.Properties.DataSource = tb
        End If
    End Sub

    Public Sub LoadChiTietHangTien(IdCT As Long)
        Dim sql As String = "SELECT convert(int,0) as STT, convert(bit,0) as Chon,Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,GhiChuKhac,TaiKhoanNo,TaiKhoanCo "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", IdCT)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("STT") = i + 1
            Next
            If TrangThai.isCopy Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("Id") = DBNull.Value
                    dt.Rows(i)("ref") = DBNull.Value
                    dt.Rows(i)("IdVatTu") = DBNull.Value
                    dt.Rows(i)("IdChiTiet") = DBNull.Value
                Next
            End If
        End If
        gdvHangTien.DataSource = dt
    End Sub


    Public Sub LoadChiTietThue(IdCT As Long)
        Dim sql As String = "SELECT Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", IdCT)
        AddParameter("@ButToan", ChungTu.LoaiButToan.ThueGTGT)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            If TrangThai.isCopy Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("Id") = DBNull.Value
                    dt.Rows(i)("ref") = DBNull.Value
                    dt.Rows(i)("IdVatTu") = DBNull.Value
                    dt.Rows(i)("IdChiTiet") = DBNull.Value
                Next
            End If
        End If
        gdvThue.DataSource = dt
    End Sub

    Public Sub LoadChiTietGiaVon(IdCT As Long)
        If TrangThai.isUpdate Then
            Dim sql As String = "SELECT Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo "
            sql &= "FROM CHUNGTUCHITIET "
            sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
            AddParameter("@Id_CT", IdCT)
            AddParameter("@ButToan", ChungTu.LoaiButToan.GiaVon)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            gdvGiaVon.DataSource = dt
        End If
    End Sub

    Public Sub loadTienTe()
        Dim sql As String = "SELECT ID,Ten,TyGia FROM tblTienTe ORDER BY ID"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For Each r As DataRow In tb.Rows
                cmbTienTe.Properties.Items.Add(New LoaiTienTe(r("ID"), r("Ten"), r("TyGia")))
            Next
        End If
    End Sub

    Private Sub cmbDoiTuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbDoiTuong.EditValueChanged

        On Error Resume Next
        If cmbDoiTuong.IsPopupOpen Then Exit Sub

        cmbBanHang.Properties.Items.Clear()
        cmbSoTaiKhoan.Properties.Items.Clear()

        AddParameter("@ID", cmbDoiTuong.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachiTs,IDTakecare,ttcTaikhoan,ttcNoimo FROM KHACHHANG WHERE ID = @ID")


        If tb Is Nothing OrElse tb.Rows.Count = 0 Then
            txtTenDoiTuong.EditValue = ""
            txtDiaChi.EditValue = ""
            txtMST.EditValue = ""
        Else
            txtTenDoiTuong.EditValue = tb.Rows(0)("Ten").ToString
            txtDiaChi.EditValue = tb.Rows(0)("ttcDiachiTs").ToString
            txtMST.EditValue = tb.Rows(0)("ttcMasothue").ToString

            Dim noiMo As String = tb.Rows(0)("ttcTaikhoan").ToString & " tại " & tb.Rows(0)("ttcNoimo").ToString

            cmbSoTaiKhoan.Properties.Items.Add(noiMo)
            If cmbHinhThucTT.EditValue = "TM/CK" Or cmbHinhThucTT.EditValue = "Chuyển khoản" Then
                If noiMo.ToString.Trim <> "tại" Then
                    cmbSoTaiKhoan.Text = noiMo
                End If
            End If
            txtDienGiaiChung.Focus()

            AddParameter("@id", tb.Rows(0)("IDTakecare"))
            tb = ExecuteSQLDataTable("select Ten from nhansu where id = @id")
            If Not tb Is Nothing AndAlso tb.Rows.Count > 0 Then
                cmbBanHang.Properties.Items.Add(tb.Rows(0)("Ten").ToString)
            End If


        End If

    End Sub

    Private Sub cmbTienTe_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTienTe.SelectedIndexChanged
        txtTyGia.EditValue = CType(cmbTienTe.SelectedItem, LoaiTienTe).TyGia
    End Sub

    Private Sub frmUpdateHoaDon_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isDangXuatKho Then
            Me.Hide()
            e.Cancel = True
        Else
            Me.Dispose()
        End If
    End Sub

    Private Sub chkKhongChiuThue_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkKhongChiuThue.CheckedChanged
        If chkKhongChiuThue.Checked Then
            txtThue.EditValue = 0
        Else
            txtThue.EditValue = 10
        End If
    End Sub


    Private Sub gdvHangTienCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvHangTienCT.CellValueChanged

        Select Case e.Column.FieldName
            Case "DonGia"
                If gdvHangTienCT.FocusedColumn Is gdvHangTienCT.Columns("DonGia") Then

                    With gdvHangTienCT
                        Try
                            .SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(.GetRowCellValue(e.RowHandle, "SoLuong") * .GetRowCellValue(e.RowHandle, "DonGia"), 2, MidpointRounding.AwayFromZero))
                        Catch ex As Exception
                            .SetRowCellValue(e.RowHandle, "ThanhTien", 0)
                        End Try
                    End With

                End If
            Case "SoLuong"

                With gdvHangTienCT
                    Try
                        .SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(.GetRowCellValue(e.RowHandle, "SoLuong") * .GetRowCellValue(e.RowHandle, "DonGia"), 2, MidpointRounding.AwayFromZero))
                    Catch ex As Exception
                        .SetRowCellValue(e.RowHandle, "ThanhTien", 0)
                    End Try
                End With
            Case "DienGiai"
                With gdvThueCT
                    Try
                        If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                            Dim row = CType(.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            row("DienGiai") = gdvHangTienCT.GetRowCellValue(e.RowHandle, "DienGiai")
                        End If
                    Catch ex As Exception
                    End Try
                End With
            Case "ThanhTien"
                With gdvThueCT

                    Try
                        Dim thanhtien = gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien")
                        Dim tienthue = Math.Round((thanhtien * txtThue.EditValue) / 100, 0, MidpointRounding.AwayFromZero)
                        Dim row = CType(.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        row("ThanhTien") = tienthue
                    Catch ex As Exception
                    End Try

                    If gdvHangTienCT.FocusedColumn Is gdvHangTienCT.Columns("ThanhTien") Then
                        With gdvHangTienCT
                            Try
                                .SetRowCellValue(e.RowHandle, "DonGia", Math.Round(.GetRowCellValue(e.RowHandle, "ThanhTien") / .GetRowCellValue(e.RowHandle, "SoLuong"), 2, MidpointRounding.AwayFromZero))
                            Catch ex As Exception
                                .SetRowCellValue(e.RowHandle, "DonGia", 0)
                            End Try
                        End With
                    End If

                End With
                TinhTongTien()
                'Case "Chon"
                '    Try
                '        If e.RowHandle < 0 Then Exit Sub
                '        If gdvHangTienCT.GetRowCellValue(e.RowHandle, "GhiChu").ToString.Trim = "" Then
                '            gdvHangTienCT.SetRowCellValue(e.RowHandle, "Chon", False)
                '        End If
                '    Catch ex As Exception
                '    End Try
        End Select


    End Sub

    Private Sub gdvThueCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvThueCT.CellValueChanged
        Select Case e.Column.FieldName
            Case "ThanhTien"
                TinhTongTien()
        End Select
    End Sub

    'Cập nhật lại tiền thuế
    Private Sub txtThue_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtThue.EditValueChanged
        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            Try
                Dim thanhtien = gdvHangTienCT.GetRowCellValue(i, "ThanhTien")
                Dim tienthue = Math.Round((thanhtien * txtThue.EditValue) / 100, 0, MidpointRounding.AwayFromZero)
                Dim row = CType(gdvThueCT.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(i, "ref") & "'")(0)
                row("ThanhTien") = tienthue
            Catch ex As Exception
            End Try
        Next
        TinhTongTien()
    End Sub

    Private Sub TinhTongTien()
        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()
        Dim tongtienhang As Double = 0
        Dim tongtienthue As Double = 0
        Dim tongthanhtien As Double = 0
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            tongtienhang += gdvHangTienCT.GetRowCellValue(i, "ThanhTien")
        Next
        For i As Integer = 0 To gdvThueCT.RowCount - 1
            tongtienthue += gdvThueCT.GetRowCellValue(i, "ThanhTien")
        Next
        tongthanhtien = tongtienhang + tongtienthue
        txtTongTienHang.EditValue = tongtienhang
        txtTongTienThue.EditValue = tongtienthue
        txtTongThanhTien.EditValue = tongthanhtien
    End Sub

    Private calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    Private Sub gdvHangTienCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvHangTienCT.MouseDown
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calHitTestHoaDon = gdvHangTienCT.CalcHitInfo(e.Location)
        'If calHitTestHoaDon.InRowCell Then
        '    mnuXoaDongHoaDon.Enabled = True
        '    If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is DBNull.Value Then
        '        mnuXoaDongHoaDon.Enabled = True
        '    Else
        '        mnuXoaDongHoaDon.Enabled = False
        '    End If
        'Else
        '    mnuXoaDongHoaDon.Enabled = False
        'End If
        pMenuHoaDon.ShowPopup(gdvHangTien.PointToScreen(e.Location))
    End Sub

    Private Sub mnuXoaDongHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaDongHoaDon.ItemClick
        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()
        Dim ref As String = gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "ref")

        If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is Nothing Then 'Truong hop hang moi chua them gi
            gdvHangTienCT.DeleteRow(calHitTestHoaDon.RowHandle)
            For i As Integer = 0 To gdvThueCT.RowCount - 1
                If gdvThueCT.GetRowCellValue(i, "ref") = ref Then
                    gdvThueCT.DeleteRow(i)
                    Exit Sub
                End If
            Next
        Else
            If Not ShowCauHoi("Bạn có chắc muốn xóa dòng này không?") Then Exit Sub
            Try
                BeginTransaction()
                'gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id")
                Dim idXuatKho As Object = gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "IdChiTiet")
                'Xóa chứng từ bút toán bên thuế
                AddParameter("@Id", gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id"))
                If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                For i As Integer = 0 To gdvThueCT.RowCount - 1
                    If gdvThueCT.GetRowCellValue(i, "ref") = ref Then
                        AddParameter("@Id", gdvThueCT.GetRowCellValue(i, "Id"))
                        If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        Exit For
                    End If
                Next
                'Xóa chứng từ bút toán bên giá vốn
                For i As Integer = 0 To gdvDataGiaVon.RowCount - 1
                    If gdvDataGiaVon.GetRowCellValue(i, "ref") = ref Then
                        AddParameter("@Id", gdvDataGiaVon.GetRowCellValue(i, "Id"))
                        If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        Exit For
                    End If
                Next

                If Not idXuatKho Is DBNull.Value Then

                    'update xuat kho
                    AddParameter("@Id_CT", DBNull.Value)
                    AddParameter("@SoHoaDon", DBNull.Value)
                    AddParameter("@Xuatthue", 0)
                    AddParameterWhere("@dk_Id", idXuatKho)
                    If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "IdVatTu") Is DBNull.Value Then
                        If doUpdate("XUATKHOAUX", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Else
                        If doUpdate("XUATKHO", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                    'Tinh lai tien thue
                    Dim sqlX As String = ""
                    sqlX &= "select ID,SoPhieu,convert(nvarchar,'VT') as isVatTu from xuatkho WITH (NOLOCK) where ID = " & idXuatKho & " "
                    sqlX &= "UNION ALL "
                    sqlX &= "select ID,SoPhieu,convert(nvarchar,'DV') as isVatTu from xuatkhoaux WITH (NOLOCK) where ID = " & idXuatKho & " "

                    Dim dtId As DataTable = ExecuteSQLDataTable(sqlX)
                    If dtId Is Nothing Then Throw New Exception(LoiNgoaiLe)

                    For i As Integer = 0 To dtId.Rows.Count - 1
                        sqlX = "update PHIEUXUATKHO set "
                        sqlX &= "TienThue = ( "
                        sqlX &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkho WITH (NOLOCK) where SoPhieu = '" & dtId.Rows(i)("SoPhieu") & "' and Xuatthue = 1), 0) + "
                        sqlX &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkhoaux WITH (NOLOCK) where SoPhieu = '" & dtId.Rows(i)("SoPhieu") & "' and Xuatthue = 1), 0) ) "
                        sqlX &= "where Sophieu = '" & dtId.Rows(i)("SoPhieu") & "'"
                        If ExecuteSQLNonQuery(sqlX) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Next

                End If


                gdvHangTienCT.DeleteRow(calHitTestHoaDon.RowHandle)
                For i As Integer = 0 To gdvThueCT.RowCount - 1
                    If gdvThueCT.GetRowCellValue(i, "ref") = ref Then
                        gdvThueCT.DeleteRow(i)
                        Exit For
                    End If
                Next



                ComitTransaction()
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If

        TinhTongTien()

    End Sub


    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub

    Private Function checkSoHoaDonDaCo() As Boolean
        Dim sqlKT = "SELECT NgayHD FROM CHUNGTU WHERE SoHD = @SoHD AND KyHieuHD = @KyHieuHD And LoaiCT = @LoaiCT "
        AddParameter("@SoHD", txtSoHoaDon.Text)
        AddParameter("KyHieuHD", txtKyHieuHoaDon.Text)
        AddParameter("LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
        Dim dtKT As DataTable = ExecuteSQLDataTable(sqlKT)
        If dtKT.Rows.Count > 0 Then
            Try
                ShowCanhBao("Số hóa đơn " & txtSoHoaDon.Text & " đã được lập ngày " & Convert.ToDateTime(dtKT.Rows(0)("NgayHD")).ToString("dd/MM/yyyy"))
            Catch ex As Exception
                ShowCanhBao("Số hóa đơn " & txtSoHoaDon.Text & " đã được lập !")
            End Try
            Return True
        End If
        Return False
    End Function

    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        Try

            gdvHangTienCT.CloseEditor()
            gdvHangTienCT.UpdateCurrentRow()
            gdvThueCT.CloseEditor()
            gdvThueCT.UpdateCurrentRow()

            TinhTongTien()


            btGhiLai.Enabled = False

            Dim sqlKT As String = ""
            If TrangThai.isAddNew Or TrangThai.isCopy Then
                If checkSoHoaDonDaCo() Then
                    Exit Sub
                End If
            ElseIf TrangThai.isUpdate Then
                If SoHoaDonCu <> txtSoHoaDon.Text.Trim Then
                    If checkSoHoaDonDaCo() Then
                        Exit Sub
                    End If
                End If
            End If

            'Check luong ton dau vao
            Dim isHopLeTonKho As Boolean = True
            Dim thongBao As String = ""
            Dim sqlX As String = ""
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
                Dim ngayHD As DateTime = txtNgayHoaDon.EditValue
                sqlX = "SET DATEFORMAT DMY "
                sqlX &= "Select "
                sqlX &= "ISNULL((SELECT DauKy FROM TONKHOVATTUTHUE WHERE Nam = " & ngayHD.Year & " AND IdVatTu =  " & gdvHangTienCT.GetRowCellValue(i, "IdVatTu") & "),0) "
                sqlX &= " + "
                sqlX &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
                sqlX &= "WHERE YEAR(CHUNGTU.NgayHD) = " & ngayHD.Year & " AND CHUNGTU.LoaiCT = " & ChungTu.LoaiChungTu.HoaDonDauVao & " AND CHUNGTU.GhiSo = 1 AND CHUNGTUCHITIET.ButToan = 1 "
                sqlX &= "AND CHUNGTUCHITIET.IdVatTu = " & gdvHangTienCT.GetRowCellValue(i, "IdVatTu") & " AND Convert(datetime,CONVERT(nvarchar,CHUNGTU.NgayHD,103),103) <= Convert(datetime,'" & ngayHD.ToString("dd/MM/yyyy") & "',103) ),0) "
                sqlX &= " - "
                sqlX &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
                sqlX &= "WHERE YEAR(CHUNGTU.NgayHD) = " & ngayHD.Year & " AND CHUNGTU.LoaiCT = " & ChungTu.LoaiChungTu.HoaDonDauRa & " AND CHUNGTU.GhiSo = 1 AND CHUNGTU.TrangThai = " & HoaDonGTGT.TrangThaiHoaDon.TrangThai.HoaDonDaIn & " AND CHUNGTUCHITIET.ButToan = 1 "
                sqlX &= "AND CHUNGTUCHITIET.IdVatTu = " & gdvHangTienCT.GetRowCellValue(i, "IdVatTu") & " AND Convert(datetime,CONVERT(nvarchar,CHUNGTU.NgayHD,103),103) <= Convert(datetime,'" & ngayHD.ToString("dd/MM/yyyy") & "',103) ),0) "
                Dim dtKiemTraTon As DataTable = ExecuteSQLDataTable(sqlX)
                If dtKiemTraTon Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
                If gdvHangTienCT.GetRowCellValue(i, "SoLuong") > dtKiemTraTon.Rows(0)(0) Then
                    isHopLeTonKho = False
                    thongBao &= gdvHangTienCT.GetRowCellValue(i, "DienGiai") & " chỉ còn tồn " & dtKiemTraTon.Rows(0)(0) & " nên không đủ xuất " & gdvHangTienCT.GetRowCellValue(i, "SoLuong") & "!" & vbCrLf
                End If
            Next

            If Not isHopLeTonKho Then
                Clipboard.SetText(thongBao)
                If Not ShowCauHoi(thongBao & vbCrLf & "Vẫn lưu hóa đơn này hay không?") Then Exit Sub
            End If


            BeginTransaction()

            'If TrangThai.isAddNew Or TrangThai.isCopy Then
            '    AddParameter("@NgayCT", GetServerTime)
            'End If

            AddParameter("@NgayCT", txtNgayHoaDon.EditValue)

            'AddParameter("@SoCT", )
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
            AddParameter("@LoaiCT2", LoaiCT2)
            AddParameter("@IdKH", cmbDoiTuong.EditValue)
            AddParameter("@TenKH", txtTenDoiTuong.EditValue)
            AddParameter("@DiaChi", txtDiaChi.EditValue)
            AddParameter("@MaSoThue", txtMST.EditValue)            AddParameter("@NguoiLienHe", txtNguoiLienHe.EditValue)
            AddParameter("@HtThanhToan", CType(cmbHinhThucTT.SelectedItem, HinhThucThanhToan).GiaTri)
            AddParameter("@TienTe", CType(cmbTienTe.SelectedItem, LoaiTienTe).ID)
            AddParameter("@TyGia", txtTyGia.EditValue)
            If chkKhongChiuThue.Checked Then
                AddParameter("@Thue", DBNull.Value)
            Else
                AddParameter("@Thue", txtThue.EditValue)
            End If
            AddParameter("@KemBangKe", Convert.ToByte(chkKemBangKe.Checked))
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@NgayHD", txtNgayHoaDon.EditValue)
            AddParameter("@SoHD", txtSoHoaDon.EditValue)
            AddParameter("@KyHieuHD", txtKyHieuHoaDon.EditValue)

            AddParameter("@ThanhTien", txtTongTienHang.EditValue)
            AddParameter("@TienThue", txtTongTienThue.EditValue)
            AddParameter("@TongTien", txtTongThanhTien.EditValue)
            AddParameter("@NguoiLap", TaiKhoan)
            AddParameter("@NguoiDaiDien", cmbBanHang.Text)
            AddParameter("@TaiKhoanNganHang", cmbSoTaiKhoan.Text)

            AddParameter("@SoTkNganHang", cmbSoTK.EditValue)
            AddParameter("@TenTkNganHang", txtTenTaiKhoan.EditValue)


            If KiemTraQuyenSuDungKhongCanhBao("Menu", TagX, DanhMucQuyen.KeToan) Then
                If isHopLeTonKho Then
                    AddParameter("@GhiSo", 1)
                    AddParameter("@TrangThai", HoaDonGTGT.TrangThaiHoaDon.TrangThai.HoaDonDaIn)
                Else
                    AddParameter("@GhiSo", 0)
                    AddParameter("@TrangThai", CType(cmbTrangThai.SelectedItem, TrangThaiHoaDon).GiaTri)
                End If
            Else
                AddParameter("@GhiSo", 0)
                AddParameter("@TrangThai", CType(cmbTrangThai.SelectedItem, TrangThaiHoaDon).GiaTri)
            End If


            If TrangThai.isAddNew Or TrangThai.isCopy Then
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@dk_Id", idHoaDon)
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Hàng tiền
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@ref", gdvHangTienCT.GetRowCellValue(i, "ref"))
                AddParameter("@IdVatTu", gdvHangTienCT.GetRowCellValue(i, "IdVatTu"))
                AddParameter("@DienGiai", gdvHangTienCT.GetRowCellValue(i, "DienGiai"))
                AddParameter("@DVT", gdvHangTienCT.GetRowCellValue(i, "DVT"))
                AddParameter("@SoLuong", gdvHangTienCT.GetRowCellValue(i, "SoLuong"))
                AddParameter("@DonGia", gdvHangTienCT.GetRowCellValue(i, "DonGia"))
                AddParameter("@ThanhTien", gdvHangTienCT.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@ThanhTienQD", gdvHangTienCT.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@GhiChu", gdvHangTienCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@GhiChuKhac", gdvHangTienCT.GetRowCellValue(i, "GhiChuKhac"))
                AddParameter("@TaiKhoanNo", gdvHangTienCT.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvHangTienCT.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                AddParameter("@IdChiTiet", gdvHangTienCT.GetRowCellValue(i, "IdChiTiet"))
                If gdvHangTienCT.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                    If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvHangTienCT.SetRowCellValue(i, "Id", idHdCT)
                Else
                    AddParameterWhere("@dk_Id", gdvHangTienCT.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            'Thuế
            For i As Integer = 0 To gdvThueCT.RowCount - 1
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@ref", gdvThueCT.GetRowCellValue(i, "ref"))
                AddParameter("@IdVatTu", gdvThueCT.GetRowCellValue(i, "IdVatTu"))
                AddParameter("@DienGiai", gdvThueCT.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvThueCT.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@ThanhTienQD", gdvThueCT.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@GhiChu", gdvThueCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@TaiKhoanNo", gdvThueCT.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvThueCT.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.ThueGTGT)
                AddParameter("@IdChiTiet", gdvThueCT.GetRowCellValue(i, "IdChiTiet"))
                If gdvThueCT.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                    If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvThueCT.SetRowCellValue(i, "Id", idHdCT)
                Else
                    AddParameterWhere("@dk_Id", gdvThueCT.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            'Cập nhật IdCT và số HD lên xuất kho
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                If gdvHangTienCT.GetRowCellValue(i, "IdChiTiet") Is DBNull.Value Then Continue For
                If idHoaDon Is Nothing Then Throw New Exception("Chưa lưu được hóa đơn, vui lòng thử lại thao tác!")
                AddParameter("@Id_CT", idHoaDon)
                'Nếu hóa đơn hủy quay lại cập nhật phiếu xuất kho
                If CType(cmbTrangThai.SelectedItem, TrangThaiHoaDon).GiaTri = TrangThaiHoaDon.TrangThai.HoaDonHuy Then
                    AddParameter("@Xuatthue", 0)
                    AddParameter("@Mucthue", 0)
                    AddParameter("@SoHoaDon", DBNull.Value)
                Else
                    AddParameter("@Xuatthue", 1)
                    AddParameter("@Mucthue", txtThue.EditValue)
                    AddParameter("@SoHoaDon", txtSoHoaDon.Text)
                End If
                AddParameterWhere("@dk_ID", gdvHangTienCT.GetRowCellValue(i, "IdChiTiet"))
                If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then
                    If doUpdate("XUATKHOAUX", "ID = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else
                    If doUpdate("XUATKHO", "ID = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            'Cập nhật tiền thuế phiếu xuất kho
            Dim dsIdVT As String = "-1"
            Dim dsIdDV As String = "-1"
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                If gdvHangTienCT.GetRowCellValue(i, "IdChiTiet") Is DBNull.Value Then Continue For
                If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then
                    dsIdDV &= "," & gdvHangTienCT.GetRowCellValue(i, "IdChiTiet")
                Else
                    dsIdVT &= "," & gdvHangTienCT.GetRowCellValue(i, "IdChiTiet")
                End If
            Next

            'lấy phiếu bên xuất kho vật tư
            Dim dataSoPhieuVT As New DataTable
            Dim sql As String = "select distinct Sophieu from xuatkho WITH (NOLOCK) where Id in (" & dsIdVT & ")"
            Dim _sqlCmd As New SqlClient.SqlCommand
            Dim _objSqlConn As New SqlClient.SqlConnection(getSQLConnectionString)
            _sqlCmd.CommandText = sql
            _sqlCmd.Connection = _objSqlConn
            Dim da As New SqlClient.SqlDataAdapter(_sqlCmd)
            da.Fill(dataSoPhieuVT)
            _sqlCmd.Dispose()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
            End If
            If dataSoPhieuVT Is Nothing Then Throw New Exception(LoiNgoaiLe)
            'lấy phiếu bên xuất kho dịch vụ
            Dim dataSoPhieuDV As New DataTable
            sql = "select distinct Sophieu from xuatkhoaux WITH (NOLOCK) where Id in (" & dsIdDV & ")"
            _sqlCmd = New SqlClient.SqlCommand
            _objSqlConn = New SqlClient.SqlConnection(getSQLConnectionString)
            _sqlCmd.CommandText = sql
            _sqlCmd.Connection = _objSqlConn
            da = New SqlClient.SqlDataAdapter(_sqlCmd)
            da.Fill(dataSoPhieuDV)
            _sqlCmd.Dispose()
            If _objSqlConn.State <> ConnectionState.Closed Then
                _objSqlConn.Close()
            End If
            If dataSoPhieuDV Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim dataSoPhieu As DataTable = dataSoPhieuVT.Copy
            For i As Integer = 0 To dataSoPhieuDV.Rows.Count - 1
                Dim _sophieu As String = dataSoPhieuDV.Rows(i)("Sophieu")
                If dataSoPhieu.Rows.Cast(Of DataRow).Where(Function(x) x("Sophieu") = _sophieu).Count() = 0 Then
                    Dim r As DataRow = dataSoPhieu.NewRow
                    r("Sophieu") = _sophieu
                    dataSoPhieu.Rows.InsertAt(r, dataSoPhieu.Rows.Count)
                End If
            Next

            For i As Integer = 0 To dataSoPhieu.Rows.Count - 1
                sql = "update PHIEUXUATKHO set "
                sql &= "TienThue = ( "
                sql &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkho WITH (NOLOCK) where SoPhieu = '" & dataSoPhieu.Rows(i)(0) & "' and Xuatthue = 1), 0) + "
                sql &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkhoaux WITH (NOLOCK) where SoPhieu = '" & dataSoPhieu.Rows(i)(0) & "' and Xuatthue = 1), 0) ) "
                sql &= "where Sophieu = '" & dataSoPhieu.Rows(i)(0) & "'"
                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Next


            'For i As Integer = 0 To dataSoPhieuVT.Rows.Count - 1
            '    sql = "update PHIEUXUATKHO set "
            '    sql &= "TienThue = (select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkho where SoPhieu = '" & dataSoPhieuVT.Rows(i)(0) & "' and Nhapthue = 1) "
            '    sql &= "where Sophieu = '" & dataSoPhieuVT.Rows(i)(0) & "'"
            'Next
            'If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            SoHoaDonCu = txtSoHoaDon.Text

            ComitTransaction()

            For i As Integer = 0 To dataSoPhieu.Rows.Count - 1
                'Cập nhật giá trị phân bổ xuất kho
                AddParameter("@sophieu", dataSoPhieu.Rows(i)(0))
                sql = "select SophieuCG from phieuxuatkho where sophieu = @sophieu "
                Dim dtSoCG As DataTable = ExecuteSQLDataTable(sql)
                If Not dtSoCG Is Nothing AndAlso dtSoCG.Rows.Count > 0 Then
                    frmCNXuatKho.CapNhatPhanBoTamUng(dtSoCG.Rows(0)(0))
                End If
            Next

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                TrangThai.isUpdate = True
                ShowAlert("Lập mới hóa đơn thành công !")
            Else
                ShowAlert("Cập nhật hóa đơn thành công !")
            End If






            If Not isHopLeTonKho Then
                groupThongTinChung.AppearanceCaption.ForeColor = Color.Navy
                groupThongTinChung.Text = "Thông tin chung (Chưa ghi sổ)"
            Else
                groupThongTinChung.AppearanceCaption.ForeColor = Color.Green
                groupThongTinChung.Text = "Thông tin chung (Đã ghi sổ)"
            End If


            btnThemMoi.Enabled = True
            btInHoaDon.Enabled = True

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        Finally
            btGhiLai.Enabled = True
        End Try

    End Sub

    Private Sub mnuThemDongHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemDongHD.ItemClick
        Dim ref As String = ChungTu.getRef
        gdvHangTienCT.AddNewRow()
        gdvHangTienCT.SetFocusedRowCellValue("ref", ref)
        gdvHangTienCT.SetFocusedRowCellValue("Chon", 0)
        gdvHangTienCT.SetFocusedRowCellValue("SoLuong", 0)
        gdvHangTienCT.SetFocusedRowCellValue("DonGia", 0)
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "131")
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "5113")
        gdvThueCT.AddNewRow()
        gdvThueCT.SetFocusedRowCellValue("ref", ref)
        gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "131")
        gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "3331")
        gdvThueCT.SetFocusedRowCellValue("ThanhTien", 0)
        gdvHangTienCT.Focus()
        gdvHangTienCT.FocusedColumn = gdvHangTienCT.Columns("DienGiai")
        gdvHangTienCT.ShowEditor()
        SendKeys.Send("{F4}")
    End Sub

    Private Sub txtTenDoiTuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTenDoiTuong.EditValueChanged
        txtDienGiaiChung.EditValue = "Bán hàng cho " & txtTenDoiTuong.Text
    End Sub

    Private Sub mnuInHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInHoaDon.ItemClick
        InHoaDon(idHoaDon)
    End Sub


    Private lstIndexRowChon As List(Of Integer)
    Private Sub mnuGopSoLuongVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGopSoLuongVT.ItemClick


        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()

        lstIndexRowChon = New List(Of Integer)
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            If Convert.ToBoolean(gdvHangTienCT.GetRowCellValue(i, "Chon")) = True Then
                If gdvHangTienCT.GetRowCellValue(i, "GhiChu").ToString.Trim <> "" Then
                    ShowCanhBao("Chỉ gộp được các dòng có ghi chú rỗng!")
                    Exit Sub
                End If
                lstIndexRowChon.Add(i)
            End If
        Next


        If lstIndexRowChon.Count <= 1 Then
            ShowCanhBao("Phải chọn ít nhất 2 dòng chi tiết!")
            Exit Sub
        End If

        'Đặt mặc định
        txtDienGiaiGop.Text = gdvHangTienCT.GetRowCellValue(lstIndexRowChon(0), "DienGiai")
        txtDvtGop.Text = gdvHangTienCT.GetRowCellValue(lstIndexRowChon(0), "DVT")
        txtSoLuongGop.Value = gdvHangTienCT.GetRowCellValue(lstIndexRowChon(0), "SoLuong")
        txtDonGiaGop.Value = gdvHangTienCT.GetRowCellValue(lstIndexRowChon(0), "DonGia")

        For i As Integer = 1 To lstIndexRowChon.Count - 1
            If txtDienGiaiGop.Text = gdvHangTienCT.GetRowCellValue(lstIndexRowChon(i), "DienGiai") And txtDienGiaiGop.Text.Trim <> "" Then
                txtSoLuongGop.Value += gdvHangTienCT.GetRowCellValue(lstIndexRowChon(i), "SoLuong")
            Else
                txtDienGiaiGop.Text = ""
                txtDvtGop.Text = ""
                txtSoLuongGop.Value = 1
                txtDonGiaGop.Value = 0
            End If
        Next

        trangthaigop(True)

        txtDienGiaiGop.Focus()

    End Sub

    Private Sub trangthaigop(st As Boolean)
        groupThongTinChung.Enabled = Not st
        groupChungTu.Enabled = Not st
        tabNoiDung.Enabled = Not st
        btGhiLai.Enabled = Not st
        txtTongTienHang.Enabled = Not st
        txtTongTienThue.Enabled = Not st
        txtTongThanhTien.Enabled = Not st
        groupGopNoiDung.Visible = st
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            gdvHangTienCT.SetRowCellValue(i, "Chon", False)
        Next
    End Sub

    Private Sub btnHuyThaoTac_Click(sender As System.Object, e As System.EventArgs) Handles btnHuyThaoTac.Click
        trangthaigop(False)
    End Sub

    Private Sub txtSoLuongGop_ValueChanged(sender As System.Object, e As System.EventArgs) Handles txtSoLuongGop.ValueChanged, txtDonGiaGop.ValueChanged
        txtThanhTienGop.Value = Math.Round(txtSoLuongGop.Value * txtDonGiaGop.Value, 2, MidpointRounding.AwayFromZero)
    End Sub

    Private Sub txtThanhTienGop_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtThanhTienGop.EditValueChanged
        txtTienThueGop.Value = Math.Round(((txtThanhTienGop.Value * txtThue.Value) / 100.0F), 2, MidpointRounding.AwayFromZero)
    End Sub

    Private Sub btnGopLai_Click(sender As System.Object, e As System.EventArgs) Handles btnGopLai.Click

        Dim strGop As String = "[<({0})>];[<({1})>];[<({2})>];[<({3})>];[<({4})>];[<({5})>]" 'DienGiai;DVT;SoLuong;DonGia;ThanhTien;TienThue
        strGop = String.Format(strGop, txtDienGiaiGop.Text, txtDvtGop.Text, txtSoLuongGop.Value, txtDonGiaGop.Value, txtThanhTienGop.Value, txtTienThueGop.Value)

        For i As Integer = 0 To lstIndexRowChon.Count - 1
            gdvHangTienCT.SetRowCellValue(lstIndexRowChon(i), "GhiChu", strGop)
        Next

        trangthaigop(False)

    End Sub

    Private Sub mnuHuyNoiDungGop_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuHuyNoiDungGop.ItemClick
        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()
        If gdvHangTienCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvHangTienCT.GetFocusedRowCellValue("GhiChu").ToString.Trim = "" Then Exit Sub
        Dim strGhiChu = gdvHangTienCT.GetFocusedRowCellValue("GhiChu").ToString
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            If gdvHangTienCT.GetRowCellValue(i, "GhiChu").ToString = strGhiChu Then
                gdvHangTienCT.SetRowCellValue(i, "GhiChu", DBNull.Value)
            End If
        Next
    End Sub

    Private Sub mnuInNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInNhap.ItemClick
        InHoaDonNhap(idHoaDon)
    End Sub

    Private Sub mnuInBangKe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInBangKe.ItemClick
        InBangKe(idHoaDon)
    End Sub

    Private Sub LoadDsVT()


        Dim sql As String = "SELECT ID, (case when rtrim(ltrim(isnull(TenHoaDon,''))) = '' then RTRIM(LTRIM(ISNULL((SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu),'') + ' ' + ISNULL(Model,''))) else TenHoaDon end) as TenVatTu, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT, convert(float,null) Ton, isCongCuDungCu,isTaiSanCoDinh  "
        sql &= "FROM VATTU ORDER BY TenHoaDon "
        HoaDonGTGT.CacheData.dataVatTu = New DataView(ExecuteSQLDataTable(sql))
        'gdvVT.DataSource = HoaDonGTGT.CacheData.dataVatTu

    End Sub




    Private Sub mnuThemDongVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemDongVatTu.ItemClick
        Dim ref As String = ChungTu.getRef
        gdvHangTienCT.AddNewRow()
        gdvHangTienCT.SetFocusedRowCellValue("ref", ref)
        gdvHangTienCT.SetFocusedRowCellValue("Chon", 0)
        gdvHangTienCT.SetFocusedRowCellValue("SoLuong", 1)
        gdvHangTienCT.SetFocusedRowCellValue("DonGia", 0)
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "131")
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "5111")
        gdvThueCT.AddNewRow()
        gdvThueCT.SetFocusedRowCellValue("ref", ref)
        gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "131")
        gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "3331")
        gdvThueCT.SetFocusedRowCellValue("ThanhTien", 0)
        gdvHangTienCT.Focus()
        gdvHangTienCT.FocusedColumn = gdvHangTienCT.Columns("IdVatTu")
        gdvHangTienCT.ShowEditor()
        SendKeys.Send("{F4}")
        'cmbMaVT.Links(0).Focus()
        'CType(cmbMaVT.Links(0), DevExpress.XtraBars.BarEditItemLink).ShowEditor()
    End Sub


    Private Sub btnAn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAn.ItemClick
        'splHangHoa.Collapsed = True
    End Sub


    Private Sub mnuDuaVTvaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVTvaoHD.ItemClick


        If gdvDataVT.FocusedRowHandle < 0 Then Exit Sub

        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
            If gdvDataVT.GetFocusedRowCellValue("ID") = gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Then
                If Not ShowCauHoi("Vật tư này đã tồn tại trong hóa đơn, có muốn đưa tiếp không?") Then Exit Sub
            End If
        Next

        gdvHangTienCT.SetFocusedRowCellValue("IdVatTu", gdvDataVT.GetFocusedRowCellValue("ID"))
        gdvHangTienCT.SetFocusedRowCellValue("DienGiai", gdvDataVT.GetFocusedRowCellValue("TenVatTu"))
        gdvHangTienCT.SetFocusedRowCellValue("DVT", gdvDataVT.GetFocusedRowCellValue("DVT"))

        TinhTongTien()

        gdvDataVT.SetFocusedRowCellValue("Ton", DBNull.Value)
        ShowAlert("Đã thêm vật tư vào hóa đơn!")

        gdvHangTienCT.Focus()
        gdvHangTienCT.FocusedColumn = gdvHangTienCT.Columns("DienGiai")
        gdvHangTienCT.ShowEditor()
        'SendKeys.Send("{F4}")


    End Sub

    Private Sub gdvDataVT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDataVT.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvDataVT.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            pMnuVT.ShowPopup(gdvVT.PointToScreen(e.Location))
        End If
    End Sub


    Private Sub gdvHangTienCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvHangTienCT.InitNewRow
        gdvHangTienCT.SetRowCellValue(e.RowHandle, "STT", gdvHangTienCT.RowCount)
    End Sub

    Private Sub gdvDataVT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvDataVT.DoubleClick
        mnuDuaVTvaoHD_ItemClick(mnuDuaVTvaoHD, New DevExpress.XtraBars.ItemClickEventArgs(mnuDuaVTvaoHD, mnuDuaVTvaoHD.Links(0)))
    End Sub

    Private Sub mnuThemMaVtMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemMaVtMoi.ItemClick
        Dim f As New frmThemVatTuNhanh
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim ref As String = ChungTu.getRef
            gdvHangTienCT.AddNewRow()
            gdvHangTienCT.SetFocusedRowCellValue("ref", ref)
            gdvHangTienCT.SetFocusedRowCellValue("Chon", 0)
            gdvHangTienCT.SetFocusedRowCellValue("SoLuong", 1)
            gdvHangTienCT.SetFocusedRowCellValue("DonGia", 0)
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "131")
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "5111")
            gdvThueCT.AddNewRow()
            gdvThueCT.SetFocusedRowCellValue("ref", ref)
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "131")
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "3331")
            gdvThueCT.SetFocusedRowCellValue("ThanhTien", 0)

            gdvHangTienCT.SetFocusedRowCellValue("IdVatTu", f.IdVT)
            gdvHangTienCT.SetFocusedRowCellValue("DienGiai", f.TenHoaDon)
            gdvHangTienCT.SetFocusedRowCellValue("DVT", f.DVT)
        End If
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean

        Select Case keyData
            Case Keys.F2
                If mnuThemDongVatTu.Enabled And mnuThemDongVatTu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    mnuThemDongVatTu_ItemClick(mnuThemDongVatTu, New DevExpress.XtraBars.ItemClickEventArgs(mnuThemDongVatTu, mnuThemDongVatTu.Links(0)))
                End If
            Case Keys.F3
                If mnuThemDongHD.Enabled And mnuThemDongHD.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    mnuThemDongHD_ItemClick(mnuThemDongHD, New DevExpress.XtraBars.ItemClickEventArgs(mnuThemDongHD, mnuThemDongHD.Links(0)))
                End If
                'Case Keys.Delete
                '    If mnuXoaDongHoaDon.Enabled And mnuXoaDongHoaDon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                '        mnuXoaDongHoaDon_ItemClick(mnuXoaDongHoaDon, New DevExpress.XtraBars.ItemClickEventArgs(mnuXoaDongHoaDon, mnuXoaDongHoaDon.Links(0)))
                '    End If
            Case Keys.Control Or Keys.S
                btGhiLai_Click(btGhiLai, New System.EventArgs())
            Case Keys.Control Or Keys.N
                If btnThemMoi.Enabled Then
                    btnThemMoi_Click(btnThemMoi, New System.EventArgs())
                End If
            Case Keys.Control Or Keys.P
                If btInHoaDon.Enabled Then
                    mnuInHoaDon_ItemClick(mnuInHoaDon, New DevExpress.XtraBars.ItemClickEventArgs(mnuInHoaDon, mnuInHoaDon.Links(0)))
                End If
        End Select

    End Function

    Private Sub cmbDoiTuong_Enter(sender As System.Object, e As System.EventArgs) Handles cmbDoiTuong.Enter
        If Not isDangXuatKho And TrangThai.isAddNew Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private isPopupDsVtShowing As Boolean = False
    Private Sub popupDsVT_Popup(sender As System.Object, e As System.EventArgs) Handles popupDsVT.Popup
        isPopupDsVtShowing = True
        If isPopupDsVtShowing Then
            If gdvHangTienCT.FocusedRowHandle >= 0 AndAlso Not gdvHangTienCT.GetFocusedRowCellValue("IdVatTu") Is DBNull.Value Then
                txtTimVT.Text = ""
                HoaDonGTGT.CacheData.dataVatTu.RowFilter = "ID=" & gdvHangTienCT.GetFocusedRowCellValue("IdVatTu")
                gdvVT.DataSource = HoaDonGTGT.CacheData.dataVatTu
            Else
                txtTimVT.Focus()
                txtTimVT.SelectAll()
            End If
        End If
    End Sub

    Private Sub popupDsVT_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles popupDsVT.Closed
        isPopupDsVtShowing = False
    End Sub

    Private Sub gdvDataVT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvDataVT.KeyDown

        If e.KeyCode = Keys.Enter And gdvDataVT.FocusedRowHandle >= 0 Then
            mnuDuaVTvaoHD_ItemClick(mnuDuaVTvaoHD, New DevExpress.XtraBars.ItemClickEventArgs(mnuDuaVTvaoHD, mnuDuaVTvaoHD.Links(0)))
        ElseIf e.KeyCode = Keys.F3 Then
            txtTimVT.Focus()
            txtTimVT.SelectAll()
        ElseIf e.KeyCode = Keys.Space And gdvDataVT.FocusedRowHandle >= 0 Then

            Dim nam As Integer = ChungTu.NamLamViec
            Dim idVT As Object = gdvDataVT.GetFocusedRowCellValue("ID")

            Dim sql As String = "SELECT "
            sql &= "ISNULL((SELECT DauKy FROM TONKHOVATTUTHUE WHERE IdVatTu = " & idVT & " AND Nam=" & nam & "),0)"
            sql &= " + "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
            sql &= "WHERE YEAR(CHUNGTU.NgayHD) = " & nam & " AND CHUNGTU.LoaiCT = 2 AND CHUNGTU.GhiSo = 1 AND CHUNGTUCHITIET.ButToan = 1 "
            sql &= "AND CHUNGTUCHITIET.IdVatTu = " & idVT & "),0)"
            sql &= " - "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
            sql &= "WHERE YEAR(CHUNGTU.NgayHD) = " & nam & " AND CHUNGTU.LoaiCT = 1 AND CHUNGTU.GhiSo = 1 AND CHUNGTU.TrangThai = 3 AND CHUNGTUCHITIET.ButToan = 1 "
            sql &= "AND CHUNGTUCHITIET.IdVatTu = " & idVT & "),0) "

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing And dt.Rows.Count > 0 Then
                gdvDataVT.SetFocusedRowCellValue("Ton", dt.Rows(0)(0))
            End If



        End If
    End Sub

    Private Sub gdvHangTienCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvHangTienCT.KeyDown
        If e.KeyCode = Keys.Right And gdvHangTienCT.FocusedColumn.FieldName = "GhiChu" Then
            tabNoiDung.SelectedTabPage = tabTienThue
            gdvThueCT.FocusedRowHandle = gdvHangTienCT.FocusedRowHandle
            gdvThueCT.Focus()
            gdvThueCT.FocusedColumn = gdvThueCT.Columns("DienGiai")
            gdvThueCT.ShowEditor()
        End If
    End Sub

    Private Sub gdvThueCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvThueCT.KeyDown
        If e.KeyCode = Keys.Right And gdvThueCT.FocusedColumn.FieldName = "GhiChu" Then
            tabNoiDung.SelectedTabPage = tabHangTien
            gdvHangTienCT.Focus()
            gdvHangTienCT.FocusedColumn = gdvThueCT.Columns("DienGiai")
            gdvHangTienCT.ShowEditor()
        End If
    End Sub



    Private Sub btnThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btnThemMoi.Click

        TrangThai.isAddNew = True

        cmbDoiTuong.EditValue = DBNull.Value
        cmbTrangThai.SelectedItem = cmbTrangThai.Properties.Items.Cast(Of TrangThaiHoaDon).Where(Function(x) x.GiaTri = TrangThaiHoaDon.TrangThai.HoaDonNhap).FirstOrDefault

        'Mặc định ký hiệu và số Hóa Đơn
        Dim sql As String = "SELECT TOP 1 KyHieuHD,(CONVERT(BIGINT,SoHD) + 1)SoHD FROM CHUNGTU WHERE LoaiCT = @LoaiCT ORDER BY NgayCT DESC, CONVERT(BIGINT,SoHD) DESC"
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            txtKyHieuHoaDon.Text = dt.Rows(0)("KyHieuHD").ToString
            Dim sohd As String = dt.Rows(0)("SoHD").ToString
            Dim SoCT As String = ""
            For i As Integer = 1 To 7 - sohd.Length
                SoCT &= "0"
            Next
            txtSoHoaDon.Text = SoCT & sohd
        End If

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            LoadChiTietHangTien(-1)
            LoadChiTietThue(-1)
        End If



        btnThemMoi.Enabled = False
        btInHoaDon.Enabled = False


        cmbDoiTuong.Focus()
        SendKeys.Send("{F4}")

    End Sub



    Private Sub txtTimVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTimVT.EditValueChanged

        If txtTimVT.EditValue.ToString <> "" Then
            tmVT.Stop()
            tmVT.Start()
        Else
            gdvVT.DataSource = Nothing
        End If

    End Sub

    Private Sub tmVT_Tick(sender As System.Object, e As System.EventArgs) Handles tmVT.Tick
        If txtTimVT.EditValue.ToString <> "" Then
            HoaDonGTGT.CacheData.dataVatTu.RowFilter = "TenVatTu Like '*" & txtTimVT.EditValue & "*'"
            gdvVT.DataSource = HoaDonGTGT.CacheData.dataVatTu
        End If
    End Sub

    Private Sub txtTimVT_Properties_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTimVT.Properties.KeyDown
        If e.KeyCode = Keys.Down Then
            gdvDataVT.Focus()
        End If
    End Sub



    Private Sub cmbSoTK_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbSoTK.EditValueChanged
        Try
            If cmbSoTK.EditValue Is Nothing Then Exit Sub
            Dim edit As LookUpEdit = CType(sender, LookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            txtTenTaiKhoan.EditValue = dr("Ten")
        Catch ex As Exception
            txtTenTaiKhoan.EditValue = DBNull.Value
        End Try
    End Sub

    Private Sub cmbSoTK_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbSoTK.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbSoTK.EditValue = DBNull.Value
            txtTenTaiKhoan.EditValue = DBNull.Value
        End If
    End Sub



End Class