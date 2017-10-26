Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT
Imports DevExpress.XtraEditors


Public Class frmUpdateHdDauVao


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

        cmbSoTK.Properties.DataSource = ExecuteSQLDataTable("select ID, MaSo, Ten from taikhoan order by ten")


        If TrangThai.isAddNew Or TrangThai.isCopy Then
            LoadChiTietHangTien(-1)
            LoadChiTietThue(-1)
            LoadChiTietThueNhapKhau(-1)
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

    End Sub



    Public isDangXuatKho As Boolean = False
    Public idHoaDon As Object

    Public LoaiCT2 As ChungTu.LoaiCT2

    Private Sub frmUpdateHoaDon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        If LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu Or LoaiCT2 = ChungTu.LoaiCT2.MuaDichVuVanChuyen Then
            mnuThemDongVT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuThemMaVtMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gdvHangTienCT.Columns("IdVatTu").Visible = False
            gdvHangTienCT.Columns("SoLuong").Visible = False
            gdvHangTienCT.Columns("DonGia").Visible = False
            gdvHangTienCT.Columns("DVT").Visible = False
            btnChungTuKhac.Visible = False
            btnChiPhiVC.Visible = False
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaCongCuDungCu Then
            mnuThemMaVtMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuThemDongVT.Caption = "Thêm dòng công cụ, dụng cụ"
            GroupControl3.Text = "Danh sách công cụ, dụng cụ"
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaTaiSanCoDinh Then
            mnuThemMaVtMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnuThemDongVT.Caption = "Thêm dòng tài sản cố định"
            GroupControl3.Text = "Danh sách tài sản cố định"
        End If

        '***************************************************************************************************
        '************** Mua hang nuoc ngoai ****************************************************************
        If LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
            gdvHangTienCT.Columns("VanChuyenNT").Visible = True
            gdvHangTienCT.Columns("ChiPhiNT").Visible = True
            gdvHangTienCT.Columns("DonGiaQD").Visible = True
            gdvHangTienCT.Columns("ThanhTienQD").Visible = True
            tabNhapKhau.PageVisible = True
        Else
            gdvHangTienCT.Columns("VanChuyenNT").Visible = False
            gdvHangTienCT.Columns("ChiPhiNT").Visible = False
            gdvHangTienCT.Columns("DonGiaQD").Visible = False
            gdvHangTienCT.Columns("ThanhTienQD").Visible = False
            tabNhapKhau.PageVisible = False
        End If
        '***************************************************************************************************

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
            txtNgayCT.EditValue = r("NgayCT")

     

            LoadChiTietHangTien(idHoaDon)
            LoadChiTietThue(idHoaDon)
            LoadChiTietThueNhapKhau(idHoaDon)

            txtTongTienHang.EditValue = r("ThanhTien")
            txtTongTienThue.EditValue = r("TienThue")
            txtTongThanhTien.EditValue = r("TongTien")

            If TrangThai.isUpdate Then
                btInHoaDon.Enabled = True
                If Convert.ToBoolean(r("GhiSo")) Then
                    chkGhiSo.Checked = True
                Else
                    chkGhiSo.Checked = False
                End If
            End If

        End If

        If TrangThai.isCopy Then
            txtNgayHoaDon.EditValue = DBNull.Value
            txtSoHoaDon.EditValue = ""
            txtSoHoaDon.Focus()
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                Dim ref As String = ChungTu.getRef
                gdvHangTienCT.SetRowCellValue(i, "ref", ref)
                gdvThueCT.SetRowCellValue(i, "ref", ref)
                If LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
                    gdvDataThueNK.SetRowCellValue(i, "ref", ref)
                End If
            Next
            gdvHangTienCT.CloseEditor()
            gdvHangTienCT.UpdateCurrentRow()
            gdvThueCT.CloseEditor()
            gdvThueCT.UpdateCurrentRow()
            gdvDataThueNK.CloseEditor()
            gdvDataThueNK.UpdateCurrentRow()
        End If

        txtKyHieuHoaDon.Focus()

        If LoaiCT2 <> ChungTu.LoaiCT2.MuaDichVu Then
            LoadChiTietChiPhi(idHoaDon)
            Dim tongchiphi As Double = 0
            For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
                tongchiphi += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            Next
            txtTongChiPhi.EditValue = Math.Round(tongchiphi / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)
        End If



        'updates su kien thay doi thue nhap khau
        'gdvDataThueNK.BeginUpdate()
        'For i As Integer = 0 To gdvDataThueNK.RowCount - 1
        '    gdvDataThueNK.SetRowCellValue(i, "ThanhTien", gdvDataThueNK.GetRowCellValue(i, "ThanhTien"))
        'Next
        'gdvDataThueNK.EndUpdate()

        'updates su kien thay doi thue GTGT
        'gdvThueCT.BeginUpdate()
        'For i As Integer = 0 To gdvThueCT.RowCount - 1
        '    gdvThueCT.SetRowCellValue(i, "ThanhTien", gdvThueCT.GetRowCellValue(i, "ThanhTien"))
        'Next
        'gdvThueCT.EndUpdate()

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

    Public Sub loadKhachHang()
        Dim sql As String = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cmbDoiTuong.Properties.DataSource = tb
        End If
    End Sub


    Public Sub LoadChiTietHangTien(IdCT As Long)
        Dim sql As String = "SELECT convert(int,0)STT, Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo, "
        sql &= "VanChuyen,ChiPhi,convert(float,0)NhapKho, "
        sql &= "convert(float,0)DonGiaQD,ThanhTienQD,convert(float,0)VanChuyenNT,convert(float,0)ChiPhiNT "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", IdCT)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        Dim dtx As DataTable = dt.Copy
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
        'updates su kien thay doi hang tien
        gdvHangTienCT.BeginUpdate()
        For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            gdvHangTienCT.SetRowCellValue(i, "SoLuong", dtx.Rows(i)("SoLuong"))
            gdvHangTienCT.SetRowCellValue(i, "DonGia", dtx.Rows(i)("DonGia"))
            gdvHangTienCT.SetRowCellValue(i, "ThanhTien", dtx.Rows(i)("ThanhTien"))
            gdvHangTienCT.SetRowCellValue(i, "ThanhTienQD", dtx.Rows(i)("ThanhTienQD"))
        Next
        gdvHangTienCT.EndUpdate()
    End Sub


    Public Sub LoadChiTietThue(IdCT As Long)
        Dim sql As String = "SELECT Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo,ThanhTienQD "
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

    Public Sub LoadChiTietThueNhapKhau(IdCT As Long)
        Dim sql As String = "SELECT Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo,GiaTriKhac as Thue,ThanhTienQD "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", IdCT)
        AddParameter("@ButToan", ChungTu.LoaiButToan.ThueNK)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            If TrangThai.isCopy Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("Id") = DBNull.Value
                    dt.Rows(i)("ref") = DBNull.Value
                    dt.Rows(i)("IdVatTu") = DBNull.Value
                    dt.Rows(i)("IdChiTiet") = DBNull.Value
                    dt.Rows(i)("Thue") = 0
                Next
            End If
        End If
        gdvThueNK.DataSource = dt
    End Sub

    Public Sub LoadChiTietChiPhi(IdCT As Long)
        Dim sql As String = "select Id,NgayHD,SoHD,KyHieuHD,DienGiai,ThanhTien,TienThue,TongTien,LoaiCT2 from chungtu  "
        sql &= "WHERE refid = @Id "
        AddParameter("@Id", IdCT)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdvChiPhi.DataSource = dt
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

        AddParameter("@ID", cmbDoiTuong.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachiTs FROM KHACHHANG WHERE ID = @ID")

        If tb Is Nothing OrElse tb.Rows.Count = 0 Then
            txtTenDoiTuong.EditValue = ""
            txtDiaChi.EditValue = ""
            txtMST.EditValue = ""
        Else
            txtTenDoiTuong.EditValue = tb.Rows(0)("Ten").ToString
            txtDiaChi.EditValue = tb.Rows(0)("ttcDiachiTs").ToString
            txtMST.EditValue = tb.Rows(0)("ttcMasothue").ToString
            'txtNguoiLienHe.Focus()
        End If


    End Sub

    Private Sub cmbTienTe_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTienTe.SelectedIndexChanged
        txtTyGia.EditValue = CType(cmbTienTe.SelectedItem, LoaiTienTe).TyGia
    End Sub

    Private Sub frmUpdateHoaDon_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isDangXuatKho Then
            If Me.Tag <> "NK" Then
                Me.Hide()
                e.Cancel = True
            End If

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

        gdvDataThueNK.CloseEditor()
        gdvDataThueNK.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()

        Select Case e.Column.FieldName
            Case "DonGia", "SoLuong"
                With gdvHangTienCT
                    Try
                        .SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(.GetRowCellValue(e.RowHandle, "SoLuong") * .GetRowCellValue(e.RowHandle, "DonGia"), 2, MidpointRounding.AwayFromZero))
                    Catch ex As Exception
                        .SetRowCellValue(e.RowHandle, "ThanhTien", 0)
                    End Try
                End With

                Try
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "DonGiaQD", Math.Round(gdvHangTienCT.GetRowCellValue(e.RowHandle, "DonGia") * txtTyGia.EditValue, 0, MidpointRounding.AwayFromZero))
                Catch ex As Exception
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "DonGiaQD", 0)
                End Try

            Case "DienGiai"
                Try
                    If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                        Dim row = CType(gdvDataThueNK.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        row("DienGiai") = gdvHangTienCT.GetRowCellValue(e.RowHandle, "DienGiai")
                    End If
                Catch ex As Exception
                End Try
                Try
                    If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                        Dim row = CType(gdvThueCT.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        row("DienGiai") = gdvHangTienCT.GetRowCellValue(e.RowHandle, "DienGiai")
                    End If
                Catch ex As Exception
                End Try

            Case "ThanhTien"

                Try 'Tinh thanh tien quy doi
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "ThanhTienQD", Math.Round(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien") * txtTyGia.EditValue, 0, MidpointRounding.AwayFromZero))
                Catch ex As Exception
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "ThanhTienQD", 0)
                End Try

                Try 'Tinh thue nhap khau
                    If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                        Dim row = CType(gdvDataThueNK.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        Dim tiennhapkhau As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien"))
                        tiennhapkhau += isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyenNT"))
                        row("ThanhTien") = Math.Round((tiennhapkhau * isNullThen(row("Thue"))) / 100.0F, 2, MidpointRounding.AwayFromZero)
                    End If
                Catch ex As Exception
                End Try

                'Tinh thue GTGT
                If LoaiCT2 <> ChungTu.LoaiCT2.MuaHangNuocNgoai Then
                    With gdvThueCT
                        Try
                            Dim thanhtien = gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien")
                            Dim tienthue = Math.Round((thanhtien * txtThue.EditValue) / 100, 0, MidpointRounding.AwayFromZero)
                            Dim row = CType(.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            row("ThanhTien") = tienthue
                        Catch ex As Exception
                        End Try
                    End With
                Else
                    Try
                        If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                            Dim rowThue = CType(gdvThueCT.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            Dim rowThueNK = CType(gdvDataThueNK.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            Dim tienhang As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien"))
                            Dim tienvc As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyenNT"))
                            Dim tienthuenk As Double = isNullThen(rowThueNK("ThanhTien"))
                            Dim tienthueGTGT = Math.Round(((tienhang + tienvc + tienthuenk) * txtThue.EditValue) / 100.0F, 2, MidpointRounding.AwayFromZero)

                            rowThue("ThanhTien") = tienthueGTGT
                            rowThue("ThanhTienQD") = Math.Round(tienthueGTGT * txtTyGia.EditValue, 0, MidpointRounding.AwayFromZero)
                        End If
                    Catch ex As Exception

                    End Try
                End If

                Try 'Tinh gia tri nhap kho
                    Dim thanhtien As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTienQD"))
                    Dim vanchuyen As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyen"))
                    Dim chiphi As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ChiPhi"))
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "NhapKho", thanhtien + vanchuyen + chiphi)
                Catch ex As Exception
                End Try

                TinhPhanBoChiPhi()

            Case "VanChuyenNT"

                'Tinh thue NK
                Try 'Tinh thue nhap khau
                    If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                        Dim row = CType(gdvDataThueNK.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        Dim tiennhapkhau As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien"))
                        tiennhapkhau += isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyenNT"))
                        row("ThanhTien") = Math.Round((tiennhapkhau * isNullThen(row("Thue"))) / 100.0F, 2, MidpointRounding.AwayFromZero)
                    End If
                Catch ex As Exception
                End Try

                'Tinh thue GTGT
                If LoaiCT2 <> ChungTu.LoaiCT2.MuaHangNuocNgoai Then
                    With gdvThueCT
                        Try
                            Dim thanhtien = gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien")
                            Dim tienthue = Math.Round((thanhtien * txtThue.EditValue) / 100, 0, MidpointRounding.AwayFromZero)
                            Dim row = CType(.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            row("ThanhTien") = tienthue
                        Catch ex As Exception
                        End Try
                    End With
                Else
                    Try
                        If Not gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                            Dim rowThue = CType(gdvThueCT.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            Dim rowThueNK = CType(gdvDataThueNK.DataSource, DataView).Table.Select("ref='" & gdvHangTienCT.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                            Dim tienhang As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTien"))
                            Dim tienvc As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyenNT"))
                            Dim tienthuenk As Double = isNullThen(rowThueNK("ThanhTien"))
                            Dim tienthueGTGT = Math.Round(((tienhang + tienvc + tienthuenk) * txtThue.EditValue) / 100.0F, 2, MidpointRounding.AwayFromZero)

                            rowThue("ThanhTien") = tienthueGTGT
                        End If
                    Catch ex As Exception

                    End Try
                End If



                Try 'Tinh gia tri nhap kho
                    Dim thanhtien As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTienQD"))
                    Dim vanchuyen As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyen"))
                    Dim chiphi As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ChiPhi"))
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "NhapKho", thanhtien + vanchuyen + chiphi)
                Catch ex As Exception
                End Try

            Case "ChiPhi"

                Try 'Tinh gia tri nhap kho
                    Dim thanhtien As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ThanhTienQD"))
                    Dim vanchuyen As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "VanChuyen"))
                    Dim chiphi As Double = isNullThen(gdvHangTienCT.GetRowCellValue(e.RowHandle, "ChiPhi"))
                    gdvHangTienCT.SetRowCellValue(e.RowHandle, "NhapKho", thanhtien + vanchuyen + chiphi)
                Catch ex As Exception
                End Try

            Case "NhapKho"

                TinhTongTien()
        End Select


    End Sub

    Private Function isNullThen(obj As Object) As Double
        If obj Is DBNull.Value Then Return 0
        Return obj
    End Function

    Private Sub gdvThueCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvThueCT.CellValueChanged
        Select Case e.Column.FieldName
            Case "ThanhTien"
                TinhTongTien()
                Dim tienthue As Integer = Math.Round(e.Value * txtTyGia.EditValue, 0, MidpointRounding.AwayFromZero)
                gdvThueCT.SetRowCellValue(e.RowHandle, "ThanhTienQD", tienthue)
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
        If calHitTestHoaDon.InRowCell Then
            mnuXoaDongHoaDon.Enabled = True
            'If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is DBNull.Value Then
            '    mnuXoaDongHoaDon.Enabled = True
            'Else
            '    mnuXoaDongHoaDon.Enabled = False
            'End If
        Else
            mnuXoaDongHoaDon.Enabled = False
        End If
        pMenuHoaDon.ShowPopup(gdvHangTien.PointToScreen(e.Location))
    End Sub

    Private Sub mnuXoaDongHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaDongHoaDon.ItemClick

        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()
        gdvDataThueNK.CloseEditor()
        gdvDataThueNK.UpdateCurrentRow()


        Dim ref As String = gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "ref")

        If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is DBNull.Value Then 'Truong hop hang moi chua them gi
            gdvHangTienCT.DeleteRow(calHitTestHoaDon.RowHandle)
            ' xoa dong thue GTGT
            For i As Integer = 0 To gdvThueCT.RowCount - 1
                If gdvThueCT.GetRowCellValue(i, "ref") = ref Then
                    gdvThueCT.DeleteRow(i)
                    Exit For
                End If
            Next
            ' xoa dong thue nhap khau
            For i As Integer = 0 To gdvDataThueNK.RowCount - 1
                If gdvDataThueNK.GetRowCellValue(i, "ref") = ref Then
                    gdvDataThueNK.DeleteRow(i)
                    Exit For
                End If
            Next
        Else
            If Not ShowCauHoi("Bạn có chắc muốn xóa dòng này không?") Then Exit Sub
            Try
                BeginTransaction()
                'gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id")
                Dim idNhapKho As Object = gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "IdChiTiet")
                'dod
                AddParameter("@Id", gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id"))
                If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ' xoa chung tu thue GTGT
                For i As Integer = 0 To gdvThueCT.RowCount - 1
                    If gdvThueCT.GetRowCellValue(i, "ref") = ref Then
                        AddParameter("@Id", gdvThueCT.GetRowCellValue(i, "Id"))
                        If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        Exit For
                    End If
                Next
                ' xoa chung tu thue nhap khau
                For i As Integer = 0 To gdvDataThueNK.RowCount - 1
                    If gdvDataThueNK.GetRowCellValue(i, "ref") = ref Then
                        AddParameter("@Id", gdvDataThueNK.GetRowCellValue(i, "Id"))
                        If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        Exit For
                    End If
                Next

                If Not idNhapKho Is DBNull.Value Then

                    'update xuat kho
                    AddParameter("@Id_CT", DBNull.Value)
                    AddParameter("@SoHoaDon", DBNull.Value)
                    AddParameter("@Nhapthue", 0)
                    AddParameterWhere("@dk_Id", idNhapKho)

                    If doUpdate("NHAPKHO", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                    'Tinh lai tien thue
                    Dim sqlX As String = ""
                    sqlX &= "select ID,SoPhieu,convert(nvarchar,'VT') as isVatTu from NhapKho WITH (NOLOCK) where ID = " & idNhapKho & " "

                    Dim dtId As DataTable = ExecuteSQLDataTable(sqlX)
                    If dtId Is Nothing Then Throw New Exception(LoiNgoaiLe)

                    For i As Integer = 0 To dtId.Rows.Count - 1
                        sqlX = "update PHIEUNHAPKHO set "
                        sqlX &= "TienThue = ( "
                        sqlX &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkho WITH (NOLOCK) where SoPhieu = '" & dtId.Rows(i)("SoPhieu") & "' and Xuatthue = 1), 0)  ) "
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

        'gdvHangTienCT.DeleteRow(calHitTestHoaDon.RowHandle)
        'For i As Integer = 0 To gdvThueCT.RowCount - 1
        '    If gdvThueCT.GetRowCellValue(i, "ref") = ref Then
        '        gdvThueCT.DeleteRow(i)
        '        Exit Sub
        '    End If
        'Next

        TinhPhanBoChiPhi()
        TinhTongTien()

    End Sub


    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub


    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click


        Try

            'If txtKyHieuHoaDon.Text.Trim = "" Then
            '    ShowCanhBao("Chưa nhập ký hiệu hóa đơn!")
            '    txtKyHieuHoaDon.Focus()
            '    Exit Sub
            'End If

            If txtSoHoaDon.Text.Trim = "" Then
                ShowCanhBao("Chưa nhập số hóa đơn!")
                txtSoHoaDon.Focus()
                Exit Sub
            End If

            btGhiLai.Enabled = False

            gdvHangTienCT.CloseEditor()
            gdvHangTienCT.UpdateCurrentRow()
            gdvThueCT.CloseEditor()
            gdvThueCT.UpdateCurrentRow()


            BeginTransaction()
            'If TrangThai.isAddNew Or TrangThai.isCopy Then
            '    AddParameter("@NgayCT", GetServerTime)
            'End If
            'AddParameter("@SoCT", )

            'AddParameter("@NgayCT", txtNgayHoaDon.EditValue)

            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
            AddParameter("@LoaiCT2", LoaiCT2)
            AddParameter("@IdKH", cmbDoiTuong.EditValue)
            AddParameter("@TenKH", txtTenDoiTuong.EditValue)
            AddParameter("@DiaChi", txtDiaChi.EditValue)
            AddParameter("@MaSoThue", txtMST.EditValue)
            AddParameter("@NguoiLienHe", txtNguoiLienHe.EditValue)
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
            AddParameter("@NgayCT", txtNgayCT.EditValue)
            AddParameter("@ThanhTien", txtTongTienHang.EditValue)
            AddParameter("@TienThue", txtTongTienThue.EditValue)
            AddParameter("@TongTien", txtTongThanhTien.EditValue)

            AddParameter("@SoTkNganHang", cmbSoTK.EditValue)
            AddParameter("@TenTkNganHang", txtTenTaiKhoan.EditValue)

            AddParameter("@GhiSo", chkGhiSo.Checked)
            AddParameter("@NguoiLap", TaiKhoan)

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
                AddParameter("@ThanhTienQD", gdvHangTienCT.GetRowCellValue(i, "ThanhTienQD"))
                'AddParameter("@ChiPhi", gdvHangTienCT.GetRowCellValue(i, "ChiPhi"))
                'AddParameter("@VanChuyen", gdvHangTienCT.GetRowCellValue(i, "VanChuyen"))
                AddParameter("@GhiChu", gdvHangTienCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@TaiKhoanNo", gdvHangTienCT.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvHangTienCT.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                AddParameter("@IdChiTiet", gdvHangTienCT.GetRowCellValue(i, "IdChiTiet"))

                If Not Me.Owner Is Nothing AndAlso Me.Owner.Name = "frmCNChi2" Then
                    AddParameter("@GhiChuKhac", CType(Me.Owner, frmCNChi2).tbSoPhieu.EditValue)
                End If

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
                'AddParameter("@IdVatTu", gdvThueCT.GetRowCellValue(i, "IdVatTu"))
                AddParameter("@DienGiai", gdvThueCT.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvThueCT.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@ThanhTienQD", gdvThueCT.GetRowCellValue(i, "ThanhTienQD"))
                AddParameter("@GhiChu", gdvThueCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@TaiKhoanNo", gdvThueCT.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvThueCT.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.ThueGTGT)
                AddParameter("@IdChiTiet", gdvThueCT.GetRowCellValue(i, "IdChiTiet"))

                If Not Me.Owner Is Nothing AndAlso Me.Owner.Name = "frmCNChi2" Then
                    AddParameter("@GhiChuKhac", CType(Me.Owner, frmCNChi2).tbSoPhieu.EditValue)
                End If

                If gdvThueCT.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                    If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvThueCT.SetRowCellValue(i, "Id", idHdCT)
                Else
                    AddParameterWhere("@dk_Id", gdvThueCT.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next


            'Thuế nhap khau
            For i As Integer = 0 To gdvDataThueNK.RowCount - 1
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@ref", gdvDataThueNK.GetRowCellValue(i, "ref"))
                'AddParameter("@IdVatTu", gdvThueCT.GetRowCellValue(i, "IdVatTu"))
                AddParameter("@DienGiai", gdvDataThueNK.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvDataThueNK.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@ThanhTienQD", gdvDataThueNK.GetRowCellValue(i, "ThanhTienQD"))
                AddParameter("@GiaTriKhac", gdvDataThueNK.GetRowCellValue(i, "Thue"))
                AddParameter("@GhiChu", gdvDataThueNK.GetRowCellValue(i, "GhiChu"))
                AddParameter("@TaiKhoanNo", gdvDataThueNK.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvDataThueNK.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.ThueNK)
                AddParameter("@IdChiTiet", gdvDataThueNK.GetRowCellValue(i, "IdChiTiet"))

                If Not Me.Owner Is Nothing AndAlso Me.Owner.Name = "frmCNChi2" Then
                    AddParameter("@GhiChuKhac", CType(Me.Owner, frmCNChi2).tbSoPhieu.EditValue)
                End If

                If gdvDataThueNK.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                    If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvDataThueNK.SetRowCellValue(i, "Id", idHdCT)
                Else
                    AddParameterWhere("@dk_Id", gdvDataThueNK.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next



            'Chi phí liên quan
            For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
                AddParameter("@refid", idHoaDon)
                AddParameterWhere("@dk_Id", gdvDataChiPhi.GetRowCellValue(i, "Id"))
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

            'Phân bổ chi phí liên quan
            'Dim _tongchiphi As Long = 0
            'For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
            '    _tongchiphi += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            'Next
            'Dim _tongthanhtien As Long = 0
            'For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            '    If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
            '    _tongthanhtien += gdvHangTienCT.GetRowCellValue(i, "ThanhTien")
            'Next
            'Dim _tongchiPhiX As Integer = 0
            'For i As Integer = 0 To gdvHangTienCT.RowCount - 1
            '    If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
            '    Dim _tyle As Double = 0
            '    Dim _chiphi As Integer = 0
            '    If i = gdvHangTienCT.RowCount - 1 Then
            '        _chiphi = _tongchiphi - _tongchiPhiX
            '    Else
            '        _tyle = Math.Round(gdvHangTienCT.GetRowCellValue(i, "ThanhTien") / _tongthanhtien, 2, MidpointRounding.AwayFromZero)
            '        _chiphi = Math.Round(_tyle * _tongchiphi, 0, MidpointRounding.AwayFromZero)
            '        _tongchiPhiX += _chiphi
            '    End If
            '    AddParameter("@ChiPhi", _chiphi)
            '    AddParameterWhere("@dk_id", gdvHangTienCT.GetRowCellValue(i, "Id"))
            '    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            'Next


            If LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc Then
                'Cập nhật IdCT và số HD, NhapThue lên xuất kho
                For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                    If gdvHangTienCT.GetRowCellValue(i, "IdChiTiet") Is DBNull.Value Then Continue For
                    If idHoaDon Is Nothing Then Throw New Exception("Chưa lưu được hóa đơn, vui lòng thử lại thao tác!")
                    AddParameter("@Id_CT", idHoaDon)
                    AddParameter("@NhapThue", 1)
                    AddParameter("@MucThue", txtThue.EditValue)
                    AddParameter("@SoHoaDon", txtSoHoaDon.Text)
                    AddParameterWhere("@dk_ID", gdvHangTienCT.GetRowCellValue(i, "IdChiTiet"))
                    If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then
                    Else
                        If doUpdate("NHAPKHO", "ID = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next

                'Cập nhật tiền thuế phiếu nhập kho
                Dim dsId As String = "-1"
                For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                    If gdvHangTienCT.GetRowCellValue(i, "IdChiTiet") Is DBNull.Value Then Continue For
                    If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
                    dsId &= "," & gdvHangTienCT.GetRowCellValue(i, "IdChiTiet")
                Next
                Dim dataSoPhieu As New DataTable
                Dim sql As String = "select distinct Sophieu from nhapkho  WITH (NOLOCK)  where Id in (" & dsId & ")"

                Dim _sqlCmd As New SqlClient.SqlCommand
                Dim _objSqlConn As New SqlClient.SqlConnection(getSQLConnectionString)
                _sqlCmd.CommandText = sql
                _sqlCmd.Connection = _objSqlConn
                Dim da As New SqlClient.SqlDataAdapter(_sqlCmd)
                da.Fill(dataSoPhieu)
                _sqlCmd.Dispose()
                If _objSqlConn.State <> ConnectionState.Closed Then
                    _objSqlConn.Close()
                End If

                If dataSoPhieu Is Nothing Then Throw New Exception(LoiNgoaiLe)
                For i As Integer = 0 To dataSoPhieu.Rows.Count - 1
                    sql = "update PHIEUNHAPKHO set "
                    sql &= "TienThue = (select isnull(round(SUM(DonGia*SoLuong*Mucthue/100.0),2),0) from nhapkho WITH (NOLOCK) where SoPhieu = '" & dataSoPhieu.Rows(i)(0) & "' and Nhapthue = 1) "
                    sql &= "where Sophieu = '" & dataSoPhieu.Rows(i)(0) & "' "
                    If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Next
            End If


            ComitTransaction()

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                TrangThai.isUpdate = True
                ShowAlert("Nhập hóa đơn thành công !")
            Else
                ShowAlert("Cập nhật hóa đơn thành công !")
            End If

            btnThemMoi.Enabled = True
            btInHoaDon.Enabled = True

            If Not Me.Owner Is Nothing AndAlso Me.Owner.Name = "frmCNChi2" Then
                CType(Me.Owner, frmCNChi2).btnHoaDon.Enabled = False
                CType(Me.Owner, frmCNChi2).btnHoaDon.Text = "HĐ: " & txtSoHoaDon.Text
                CType(Me.Owner, frmCNChi2).btnHoaDon.Appearance.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)
                CType(Me.Owner, frmCNChi2).btnHoaDon.Appearance.ForeColor = Color.Red
            End If
            If Me.Tag = "NK" Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNhapThueHangDaNhap).LoadDS()
            End If

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        Finally
            btGhiLai.Enabled = True
        End Try


    End Sub


    Private Sub mnuThemDongHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemDongDV.ItemClick
        Dim ref As String = ChungTu.getRef
        gdvHangTienCT.AddNewRow()
        gdvHangTienCT.SetFocusedRowCellValue("ref", ref)
        gdvHangTienCT.SetFocusedRowCellValue("SoLuong", 0)
        gdvHangTienCT.SetFocusedRowCellValue("DonGia", 0)
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "")
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        gdvThueCT.AddNewRow()
        gdvThueCT.SetFocusedRowCellValue("ref", ref)
        gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "1331")
        gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        gdvThueCT.SetFocusedRowCellValue("ThanhTien", 0)

        gdvHangTienCT.Focus()
        gdvHangTienCT.FocusedColumn = gdvHangTienCT.Columns("DienGiai")
        gdvHangTienCT.ShowEditor()
        SendKeys.Send("{F4}")
    End Sub

    Private Sub txtTenDoiTuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTenDoiTuong.EditValueChanged
        txtDienGiaiChung.EditValue = "Mua hàng của " & txtTenDoiTuong.Text
    End Sub


    Private Sub gdvHangTienCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvHangTienCT.InitNewRow
        gdvHangTienCT.SetRowCellValue(e.RowHandle, "STT", gdvHangTienCT.RowCount)
    End Sub




    Private Sub LoadDsVT()
        Dim sql As String = "SELECT ID, (case when rtrim(ltrim(isnull(TenHoaDon,''))) = '' then RTRIM(LTRIM(ISNULL((SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu),'') + ' ' + ISNULL(Model,''))) else TenHoaDon end) as TenVatTu, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT, convert(float,null) Ton, isCongCuDungCu,isTaiSanCoDinh "
        sql &= "FROM VATTU ORDER BY TenHoaDon "
        HoaDonGTGT.CacheData.dataVatTu = New DataView(ExecuteSQLDataTable(sql))
    End Sub





    Private Sub gdvDataVT_DoubleClick(sender As System.Object, e As System.EventArgs)

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

        ShowAlert("Đã thêm vật tư vào hóa đơn!")

    End Sub


    Private Sub mnuThemDongVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemDongVT.ItemClick

        Dim ref As String = ChungTu.getRef
        gdvHangTienCT.AddNewRow()
        gdvHangTienCT.SetFocusedRowCellValue("ref", ref)
        gdvHangTienCT.SetFocusedRowCellValue("Chon", 0)
        gdvHangTienCT.SetFocusedRowCellValue("SoLuong", 1)
        gdvHangTienCT.SetFocusedRowCellValue("DonGia", 0)

        If LoaiCT2 = ChungTu.LoaiCT2.MuaCongCuDungCu Then
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "242")
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc Then
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "1561")
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "1561")
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        End If


        '***** thue nhap khau ***************************
        If LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
            gdvDataThueNK.AddNewRow()
            gdvDataThueNK.SetFocusedRowCellValue("ref", ref)
            gdvDataThueNK.SetFocusedRowCellValue("Thue", 0)
            gdvDataThueNK.SetFocusedRowCellValue("TaiKhoanNo", "1561")
            gdvDataThueNK.SetFocusedRowCellValue("TaiKhoanCo", "3333")
        End If
        gdvDataThueNK.SetFocusedRowCellValue("ThanhTien", 0)

        '***** thue GTGT ********************************
        gdvThueCT.AddNewRow()
        gdvThueCT.SetFocusedRowCellValue("ref", ref)
        If LoaiCT2 = ChungTu.LoaiCT2.MuaCongCuDungCu Then
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "1331")
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc Then
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "1331")
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "1331")
            gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "33312")
        End If
        gdvThueCT.SetFocusedRowCellValue("ThanhTien", 0)



        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvDataThueNK.CloseEditor()
        gdvDataThueNK.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()

        TinhPhanBoChiPhi()

        gdvHangTienCT.Focus()
        gdvHangTienCT.FocusedColumn = gdvHangTienCT.Columns("IdVatTu")
        gdvHangTienCT.ShowEditor()
        SendKeys.Send("{F4}")


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
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "1561")
            gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "331")


            '***** thue nhap khau ***************************
            If LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
                gdvDataThueNK.AddNewRow()
                gdvDataThueNK.SetFocusedRowCellValue("ref", ref)
                gdvDataThueNK.SetFocusedRowCellValue("Thue", 0)
                gdvDataThueNK.SetFocusedRowCellValue("TaiKhoanNo", "1561")
                gdvDataThueNK.SetFocusedRowCellValue("TaiKhoanCo", "3333")
            End If
            gdvDataThueNK.SetFocusedRowCellValue("ThanhTien", 0)

            gdvThueCT.AddNewRow()
            gdvThueCT.SetFocusedRowCellValue("ref", ref)
            If LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai Then
                gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "1331")
                gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "33312")
            Else
                gdvThueCT.SetFocusedRowCellValue("TaiKhoanNo", "1331")
                gdvThueCT.SetFocusedRowCellValue("TaiKhoanCo", "331")
            End If
            gdvThueCT.SetFocusedRowCellValue("ThanhTien", 0)

            gdvHangTienCT.SetFocusedRowCellValue("IdVatTu", f.IdVT)
            gdvHangTienCT.SetFocusedRowCellValue("DienGiai", f.TenHoaDon)
            gdvHangTienCT.SetFocusedRowCellValue("DVT", f.DVT)

            gdvHangTienCT.CloseEditor()
            gdvHangTienCT.UpdateCurrentRow()
            gdvDataThueNK.CloseEditor()
            gdvDataThueNK.UpdateCurrentRow()
            gdvThueCT.CloseEditor()
            gdvThueCT.UpdateCurrentRow()

        End If
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean

        Select Case keyData
            Case Keys.F2
                If mnuThemDongVT.Enabled And mnuThemDongVT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    mnuThemDongVT_ItemClick(mnuThemDongVatTu, New DevExpress.XtraBars.ItemClickEventArgs(mnuThemDongVT, mnuThemDongVT.Links(0)))
                End If
            Case Keys.F3
                If mnuThemDongDV.Enabled And mnuThemDongDV.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    mnuThemDongHD_ItemClick(mnuThemDongHD, New DevExpress.XtraBars.ItemClickEventArgs(mnuThemDongDV, mnuThemDongDV.Links(0)))
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
        txtSoHoaDon.Text = ""
        txtKyHieuHoaDon.Text = ""
        txtDienGiaiChung.Text = ""
        chkGhiSo.Checked = False

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            LoadChiTietHangTien(-1)
            LoadChiTietThue(-1)
            LoadChiTietChiPhi(-1)
            LoadChiTietThueNhapKhau(-1)
        End If

        btnThemMoi.Enabled = False


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
            If LoaiCT2 = ChungTu.LoaiCT2.MuaCongCuDungCu Then
                HoaDonGTGT.CacheData.dataVatTu.RowFilter = "TenVatTu Like '*" & txtTimVT.EditValue & "*' AND isCongCuDungCu = 1"
            ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaTaiSanCoDinh Then
                HoaDonGTGT.CacheData.dataVatTu.RowFilter = "TenVatTu Like '*" & txtTimVT.EditValue & "*' AND isTaiSanCoDinh = 1"
            Else
                HoaDonGTGT.CacheData.dataVatTu.RowFilter = "TenVatTu Like '*" & txtTimVT.EditValue & "*'"
            End If
            gdvVT.DataSource = HoaDonGTGT.CacheData.dataVatTu
        End If
    End Sub

    Private Sub txtTimVT_Properties_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtTimVT.Properties.KeyDown
        If e.KeyCode = Keys.Down Then
            gdvDataVT.Focus()
        End If
    End Sub

    Private Sub chkGhiSo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGhiSo.CheckedChanged
        If chkGhiSo.Checked Then
            chkGhiSo.ForeColor = Color.Green
        Else
            chkGhiSo.ForeColor = Color.Black
        End If
        chkGhiSo.Refresh()
    End Sub


    Private Sub gdvDataVT_DoubleClick_1(sender As System.Object, e As System.EventArgs) Handles gdvDataVT.DoubleClick

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


    End Sub


    Private Sub btnChungTuKhac_Click(sender As System.Object, e As System.EventArgs) Handles btnChungTuKhac.Click

        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Text = "Danh sách hóa đơn dịch vụ"
        f.WindowState = FormWindowState.Maximized
        f.Tag = fMain.mnuThueMuaDichVu.Name
        Dim c As New frmHoaDonDauVao
        c.isLayChungTuChiPhi = True
        c.arrListHoaDonChiPhi = New List(Of Long)
        c.LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu
        c.Dock = DockStyle.Fill
        f.Controls.Add(c)
        f.ShowDialog()

        Dim sql As String = ""
        For Each _id As Long In c.arrListHoaDonChiPhi
            sql = "select Id,NgayHD,SoHD,KyHieuHD,DienGiai,ThanhTien,TienThue,TongTien,LoaiCT2 from chungtu  "
            sql &= "WHERE Id = @Id "
            AddParameter("@Id", _id)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)

            For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
                If gdvDataChiPhi.GetRowCellValue(i, "Id") = _id Then
                    'cap nhat tien chi phi
                    gdvDataChiPhi.SetRowCellValue(i, "NgayHD", dt.Rows(0)("NgayHD"))
                    gdvDataChiPhi.SetRowCellValue(i, "SoHD", dt.Rows(0)("SoHD"))
                    gdvDataChiPhi.SetRowCellValue(i, "KyHieuHD", dt.Rows(0)("KyHieuHD"))
                    gdvDataChiPhi.SetRowCellValue(i, "DienGiai", dt.Rows(0)("DienGiai"))
                    gdvDataChiPhi.SetRowCellValue(i, "ThanhTien", dt.Rows(0)("ThanhTien"))
                    gdvDataChiPhi.SetRowCellValue(i, "TienThue", dt.Rows(0)("TienThue"))
                    gdvDataChiPhi.SetRowCellValue(i, "TongTien", dt.Rows(0)("TongTien"))
                    gdvDataChiPhi.SetRowCellValue(i, "LoaiCT2", dt.Rows(0)("LoaiCT2"))
                    Continue For
                End If
            Next

            'them moi chi phi
            Dim drx As DataTable = CType(gdvChiPhi.DataSource, DataTable)
            Dim r As DataRow = drx.NewRow
            r("Id") = dt.Rows(0)("Id")
            r("NgayHD") = dt.Rows(0)("NgayHD")
            r("SoHD") = dt.Rows(0)("SoHD")
            r("KyHieuHD") = dt.Rows(0)("KyHieuHD")
            r("DienGiai") = dt.Rows(0)("DienGiai")
            r("ThanhTien") = dt.Rows(0)("ThanhTien")
            r("TienThue") = dt.Rows(0)("TienThue")
            r("TongTien") = dt.Rows(0)("TongTien")
            r("LoaiCT2") = dt.Rows(0)("LoaiCT2")
            drx.Rows.InsertAt(r, drx.Rows.Count)
        Next


        Dim tongchiphi As Double = 0
        Dim tongvanchuyen As Double = 0

        For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
            If gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVu Then
                tongchiphi += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            ElseIf gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVuVanChuyen Then
                tongvanchuyen += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            End If

        Next

        txtTongVanChuyenQT.EditValue = Math.Round(tongvanchuyen / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)
        txtTongChiPhi.EditValue = Math.Round(tongchiphi / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)
        tabNoiDung.SelectedTabPage = tabChiPhi

        TinhPhanBoChiPhi()

    End Sub

    Private Sub btnChiPhiVC_Click(sender As System.Object, e As System.EventArgs) Handles btnChiPhiVC.Click
        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Text = "Danh sách hóa đơn vận chuyển QT"
        f.WindowState = FormWindowState.Maximized
        f.Tag = fMain.mnuThueMuaDichVu.Name
        Dim c As New frmHoaDonDauVao
        c.isLayChungTuChiPhi = True
        c.arrListHoaDonChiPhi = New List(Of Long)
        c.LoaiCT2 = ChungTu.LoaiCT2.MuaDichVuVanChuyen
        c.Dock = DockStyle.Fill
        f.Controls.Add(c)
        f.ShowDialog()

        Dim sql As String = ""
        For Each _id As Long In c.arrListHoaDonChiPhi
            sql = "select Id,NgayHD,SoHD,KyHieuHD,DienGiai,ThanhTien,TienThue,TongTien,LoaiCT2 from chungtu  "
            sql &= "WHERE Id = @Id "
            AddParameter("@Id", _id)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)

            For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
                If gdvDataChiPhi.GetRowCellValue(i, "Id") = _id Then
                    'cap nhat tien chi phi
                    gdvDataChiPhi.SetRowCellValue(i, "NgayHD", dt.Rows(0)("NgayHD"))
                    gdvDataChiPhi.SetRowCellValue(i, "SoHD", dt.Rows(0)("SoHD"))
                    gdvDataChiPhi.SetRowCellValue(i, "KyHieuHD", dt.Rows(0)("KyHieuHD"))
                    gdvDataChiPhi.SetRowCellValue(i, "DienGiai", dt.Rows(0)("DienGiai"))
                    gdvDataChiPhi.SetRowCellValue(i, "ThanhTien", dt.Rows(0)("ThanhTien"))
                    gdvDataChiPhi.SetRowCellValue(i, "TienThue", dt.Rows(0)("TienThue"))
                    gdvDataChiPhi.SetRowCellValue(i, "TongTien", dt.Rows(0)("TongTien"))
                    gdvDataChiPhi.SetRowCellValue(i, "LoaiCT2", dt.Rows(0)("LoaiCT2"))
                    Continue For
                End If
            Next

            'them moi chi phi
            Dim drx As DataTable = CType(gdvChiPhi.DataSource, DataTable)
            Dim r As DataRow = drx.NewRow
            r("Id") = dt.Rows(0)("Id")
            r("NgayHD") = dt.Rows(0)("NgayHD")
            r("SoHD") = dt.Rows(0)("SoHD")
            r("KyHieuHD") = dt.Rows(0)("KyHieuHD")
            r("DienGiai") = dt.Rows(0)("DienGiai")
            r("ThanhTien") = dt.Rows(0)("ThanhTien")
            r("TienThue") = dt.Rows(0)("TienThue")
            r("TongTien") = dt.Rows(0)("TongTien")
            r("LoaiCT2") = dt.Rows(0)("LoaiCT2")
            drx.Rows.InsertAt(r, drx.Rows.Count)
        Next

        Dim tongchiphi As Double = 0
        Dim tongvanchuyen As Double = 0

        For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
            If gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVu Then
                tongchiphi += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            ElseIf gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVuVanChuyen Then
                tongvanchuyen += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            End If

        Next

        txtTongVanChuyenQT.EditValue = Math.Round(tongvanchuyen / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)
        txtTongChiPhi.EditValue = Math.Round(tongchiphi / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)
        tabNoiDung.SelectedTabPage = tabChiPhi

        TinhPhanBoChiPhi()
    End Sub

    'Tính phân bổ chi phí vận chuyển và chi phí khác
    Private Sub TinhPhanBoChiPhi()
        'Try
        '    Dim _tongchiphiVanChuyen As Long = 0
        '    Dim _tongchiphiDichVu As Long = 0
        '    For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
        '        If gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVu Then
        '            _tongchiphiDichVu += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
        '        ElseIf gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVuVanChuyen Then
        '            _tongchiphiVanChuyen += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
        '        End If
        '    Next
        '    Dim _tongthanhtien As Long = 0
        '    For i As Integer = 0 To gdvHangTienCT.RowCount - 1
        '        If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
        '        _tongthanhtien += gdvHangTienCT.GetRowCellValue(i, "ThanhTien")
        '    Next
        '    Dim _tongchiphiVanChuyenX As Integer = 0
        '    Dim _tongchiphiDichVuX As Integer = 0
        '    For i As Integer = 0 To gdvHangTienCT.RowCount - 1
        '        If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
        '        Dim _tyle As Double = 0
        '        Dim _chiphiVanChuyen As Integer = 0
        '        Dim _chiphiDichVu As Integer = 0
        '        If i = gdvHangTienCT.RowCount - 1 Then
        '            _chiphiVanChuyen = _tongchiphiVanChuyen - _tongchiphiVanChuyenX
        '            _chiphiDichVu = _tongchiphiDichVu - _tongchiphiDichVuX
        '        Else
        '            _tyle = gdvHangTienCT.GetRowCellValue(i, "ThanhTien") / _tongthanhtien
        '            _chiphiVanChuyen = Math.Round(_tyle * _tongchiphiVanChuyen, 0, MidpointRounding.AwayFromZero)
        '            _tongchiphiVanChuyenX += _chiphiVanChuyen
        '            _chiphiDichVu = Math.Round(_tyle * _tongchiphiDichVu, 0, MidpointRounding.AwayFromZero)
        '            _tongchiphiDichVuX += _chiphiDichVu
        '        End If

        '        gdvHangTienCT.SetRowCellValue(i, "VanChuyen", _chiphiVanChuyen)
        '        gdvHangTienCT.SetRowCellValue(i, "VanChuyenNT", Math.Round(_chiphiVanChuyen / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero))

        '        gdvHangTienCT.SetRowCellValue(i, "ChiPhi", _chiphiDichVu)
        '        gdvHangTienCT.SetRowCellValue(i, "ChiPhiNT", Math.Round(_chiphiDichVu / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero))

        '    Next

        '    txtTongNhapKho.EditValue = txtTongTienHang.EditValue + txtTongChiPhi.EditValue + txtTongVanChuyenQT.EditValue

        'Catch ex As Exception
        'End Try
    End Sub




    Private Sub gdvDataChiPhi_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDataChiPhi.MouseDown
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calHitTestHoaDon = gdvDataChiPhi.CalcHitInfo(e.Location)
        If calHitTestHoaDon.InRowCell Then
            mnuXoaLienKetCP.Enabled = True
        Else
            mnuXoaLienKetCP.Enabled = False
        End If
        pChiPhi.ShowPopup(gdvChiPhi.PointToScreen(e.Location))
    End Sub

    Private Sub mnuXoaLienKetCP_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaLienKetCP.ItemClick
        If gdvDataChiPhi.FocusedRowHandle < 0 Then Exit Sub
        If Not ShowCauHoi("Xóa liên kết chi phí vừa chọn?") Then Exit Sub
        AddParameter("@refid", DBNull.Value)
        AddParameterWhere("@dk_Id", gdvDataChiPhi.GetFocusedRowCellValue("Id"))
        If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        gdvDataChiPhi.DeleteRow(gdvDataChiPhi.FocusedRowHandle)

        Dim tongchiphi As Double = 0
        Dim tongvanchuyen As Double = 0

        For i As Integer = 0 To gdvDataChiPhi.RowCount - 1
            If gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVu Then
                tongchiphi += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            ElseIf gdvDataChiPhi.GetRowCellValue(i, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVuVanChuyen Then
                tongvanchuyen += gdvDataChiPhi.GetRowCellValue(i, "ThanhTien")
            End If

        Next

        txtTongVanChuyenQT.EditValue = Math.Round(tongvanchuyen / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)
        txtTongChiPhi.EditValue = Math.Round(tongchiphi / txtTyGia.EditValue, 2, MidpointRounding.AwayFromZero)

        TinhPhanBoChiPhi()


        ShowAlert("Đã xóa liên kết chi phí")
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


    Private Sub txtTongThanhTien_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTongThanhTien.EditValueChanged, txtTongChiPhi.EditValueChanged, txtTongVanChuyenQT.EditValueChanged
        txtTongNhapKho.EditValue = txtTongTienHang.EditValue + txtTongChiPhi.EditValue + txtTongVanChuyenQT.EditValue
    End Sub




    Private Sub btInHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btInHoaDon.Click
        'Try
        '    ShowWaiting("Đang tải nội dung ...")
        '    Dim sql As String = ""
        '    sql &= "SELECT LoaiCT,LoaiCT2,NgayCT,SoCT,NgayHD,SoHD,TenKH,b.DienGiai, "
        '    sql &= "(select Model from vattu where id = b.IdVatTu)Model,b.DVT,b.SoLuong,b.DonGia,b.ThanhTien "
        '    sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
        '    sql &= "WHERE a.LoaiCT = @LoaiCT and b.ButToan = @ButToan AND a.ID = @Id "
        '    AddParameter("@Id", idHoaDon)
        '    AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        '    AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
        '    Dim dt As DataTable = ExecuteSQLDataTable(sql)

        '    Dim f As New frmIn("Xuất hóa đơn")
        '    Dim rpt As New rptPhieuNhapKhoThue

        '    rpt.DataSource = dt

        '    Dim tt As Double = 0
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        tt += dt.Rows(i)("ThanhTien")
        '    Next
        '    rpt.lblSoTienBangChu.Text = Utils.StringHelper.VIE2String(tt, False, "đồng", "lẻ", "phẩy", 2)


        '    Dim ngayct As DateTime = Convert.ToDateTime(dt.Rows(0)("NgayCT"))
        '    rpt.lblNgayCT.Text = String.Format("Ngày {0} tháng {1} năm {2}", ngayct.Day, ngayct.Month, ngayct.Year)
        '    rpt.lblSoCT.Text = "Số: " & ChungTu.TienToCT(dt.Rows(0)("LoaiCT"), dt.Rows(0)("LoaiCT2")) & dt.Rows(0)("SoCT")
        '    rpt.lblNguoiGiao.Text = "- Họ và tên người giao: " & dt.Rows(0)("TenKH").ToString
        '    Dim ngayhd As DateTime = Convert.ToDateTime(dt.Rows(0)("NgayHD"))
        '    rpt.lblTheoSoHD.Text = "- Theo hóa đơn số " & dt.Rows(0)("SoHD").ToString _
        '        & " ngày " & ngayhd.Day & " tháng " & ngayhd.Month & " năm " & ngayhd.Year _
        '        & " của " & dt.Rows(0)("TenKH").ToString

        '    rpt.RequestParameters = False
        '    rpt.CreateDocument()

        '    f.printControl.PrintingSystem = rpt.PrintingSystem
        '    CloseWaiting()
        '    f.ShowDialog()
        'Catch ex As Exception
        '    CloseWaiting()
        '    ShowBaoLoi(ex.Message)
        'End Try
    End Sub





    Private Sub gdvDataThueNK_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvDataThueNK.CellValueChanged
        Select Case e.Column.FieldName
            Case "Thue"
                Try 'Tinh thue nhap khau
                    If Not gdvDataThueNK.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                        Dim row = CType(gdvHangTienCT.DataSource, DataView).Table.Select("ref='" & gdvDataThueNK.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        Dim tiennhapkhau As Double = isNullThen(row("ThanhTien"))
                        tiennhapkhau += isNullThen(row("VanChuyenNT"))
                        gdvDataThueNK.SetRowCellValue(e.RowHandle, "ThanhTien", _
                                                      Math.Round((tiennhapkhau * isNullThen(gdvDataThueNK.GetRowCellValue(e.RowHandle, "Thue"))) / 100.0F, 2, MidpointRounding.AwayFromZero))
                    End If
                Catch ex As Exception
                End Try
            Case "ThanhTien"
                Try 'Tinh thanh tien quy doi
                    gdvDataThueNK.SetRowCellValue(e.RowHandle, "ThanhTienQD", Math.Round(gdvDataThueNK.GetRowCellValue(e.RowHandle, "ThanhTien") * txtTyGia.EditValue, 0, MidpointRounding.AwayFromZero))
                Catch ex As Exception
                    gdvDataThueNK.SetRowCellValue(e.RowHandle, "ThanhTienQD", 0)
                End Try
                Try 'Tinh tien thue GTGT
                    If Not gdvDataThueNK.GetRowCellValue(e.RowHandle, "ref") Is DBNull.Value Then
                        Dim rowThue = CType(gdvThueCT.DataSource, DataView).Table.Select("ref='" & gdvDataThueNK.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        Dim rowHangTien = CType(gdvHangTienCT.DataSource, DataView).Table.Select("ref='" & gdvDataThueNK.GetRowCellValue(e.RowHandle, "ref") & "'")(0)
                        Dim tienhang As Double = isNullThen(rowHangTien("ThanhTien"))
                        Dim tienvc As Double = isNullThen(rowHangTien("VanChuyenNT"))
                        Dim tienthuenk As Double = gdvDataThueNK.GetRowCellValue(e.RowHandle, "ThanhTien")
                        Dim tienthueGTGT = Math.Round(((tienhang + tienvc + tienthuenk) * txtThue.EditValue) / 100.0F, 2, MidpointRounding.AwayFromZero)
                        rowThue("ThanhTien") = tienthueGTGT
                        rowThue("ThanhTienQD") = Math.Round(tienthueGTGT * txtTyGia.EditValue, 0, MidpointRounding.AwayFromZero)
                    End If
                Catch ex As Exception

                End Try
        End Select
    End Sub


    Private Sub gdvDataChiPhi_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvDataChiPhi.CustomDrawCell


        Try
            If gdvDataChiPhi.GetRowCellValue(e.RowHandle, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVu Then
                e.Appearance.BackColor = Color.LightGoldenrodYellow
            ElseIf gdvDataChiPhi.GetRowCellValue(e.RowHandle, "LoaiCT2") = ChungTu.LoaiCT2.MuaDichVuVanChuyen Then
                e.Appearance.BackColor = Color.LightPink
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtNgayHoaDon_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtNgayHoaDon.EditValueChanged
        txtNgayCT.EditValue = txtNgayHoaDon.EditValue
    End Sub


    Private Sub mnuGopVatTuTrung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGopVatTuTrung.ItemClick

        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()
        gdvThueCT.CloseEditor()
        gdvThueCT.UpdateCurrentRow()
        gdvDataThueNK.CloseEditor()
        gdvDataThueNK.UpdateCurrentRow()

        Dim dt As DataTable = CType(gdvHangTien.DataSource, DataTable).Copy

        Dim hTable As New Hashtable()
        Dim duplicateList As New ArrayList()

        For Each r As DataRow In dt.Rows
            If hTable.Contains(r("IdVatTu")) Then
                duplicateList.Add(r)
            Else
                hTable.Add(r("IdVatTu"), String.Empty)
            End If
        Next


        If duplicateList.Count > 0 Then

            Dim index As Integer = 0
            Dim idvVatTu As Object = CType(duplicateList(0), DataRow)("IdVatTu")
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") = idvVatTu Then
                    index = i
                    Exit For
                End If
            Next
            gdvHangTienCT.FocusedRowHandle = index
            Dim lstIndexTrung As New List(Of Object)
            Dim _SoLuong As Double = gdvHangTienCT.GetRowCellValue(index, "SoLuong")
            For i As Integer = index + 1 To gdvHangTienCT.RowCount - 1
                If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") = idvVatTu Then
                    _SoLuong += gdvHangTienCT.GetRowCellValue(i, "SoLuong")
                    lstIndexTrung.Add(gdvHangTienCT.GetRowCellValue(i, "ref"))
                End If
            Next


            If Not ShowCauHoi("Gộp vật tư " & gdvHangTienCT.GetRowCellValue(index, "DienGiai") & " từ " & gdvHangTienCT.GetRowCellValue(index, "SoLuong") & " thành " & _SoLuong & " lại ?") Then
                Exit Sub
            End If

            gdvHangTienCT.SetRowCellValue(index, "SoLuong", _SoLuong)

            'xoa dong cu di
            For Each st As Object In lstIndexTrung
                For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                    If st = gdvHangTienCT.GetRowCellValue(i, "ref") Then
                        gdvHangTienCT.SetRowCellValue(i, "SoLuong", 0)
                        gdvHangTienCT.FocusedRowHandle = i
                    End If
                Next
            Next

        End If


    End Sub



    Private Sub gdvHangTienCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvHangTienCT.CustomDrawCell
        On Error Resume Next
        If e.Column.FieldName = "SoLuong" AndAlso e.CellValue = 0 Then
            e.Appearance.BackColor = Color.Red
            e.Appearance.ForeColor = Color.White
        End If
    End Sub

End Class