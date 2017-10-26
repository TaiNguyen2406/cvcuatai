Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Public Class frmHoaDonDauRa

    Private objHinhThucTT As HoaDonGTGT.HinhThucThanhToan
    Private objTrangThaiHD As HoaDonGTGT.TrangThaiHoaDon

    Public LoaiCT2 As ChungTu.LoaiCT2



    Private Sub frmHoaDonDauRa_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim tg As DateTime = GetServerTime()
        'txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
        txtDenNgay.EditValue = New DateTime(tg.Year, tg.Month, Date.DaysInMonth(tg.Year, tg.Month))
        txtTuNgay.EditValue = tg.AddDays(-30)

        objHinhThucTT = New HoaDonGTGT.HinhThucThanhToan
        objHinhThucTT.KhoiTao()

        objTrangThaiHD = New HoaDonGTGT.TrangThaiHoaDon
        objTrangThaiHD.KhoiTao()
        rCmbTrangThai.Items.Add(New HoaDonGTGT.TrangThaiHoaDon("Tất cả hóa đơn", -1))
        For Each obj As HoaDonGTGT.TrangThaiHoaDon In objTrangThaiHD.DanhSach
            rCmbTrangThai.Items.Add(obj)
        Next
        cmbTrangThai.EditValue = rCmbTrangThai.Items(0)
        LoadDsHoaDon()

        btnChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        mnuChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If LoaiCT2 = ChungTu.LoaiCT2.BanDichVu Then
            gdvHangTienCT.Columns("IdVatTu").Visible = False
            gdvHangTienCT.Columns("SoLuong").Visible = False
            gdvHangTienCT.Columns("DonGia").Visible = False
            gdvHangTienCT.Columns("DVT").Visible = False
            gdvHangTienCT.Columns("SoPhieu").Visible = False
            gdvHangTienCT.Columns("GhiChuKhac").Visible = False

            btnChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            tabGiaVon.PageVisible = False
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.XuLyCongTrinh Then
            gdvHangTienCT.Columns("SoPhieu").Visible = False
            gdvHangTienCT.Columns("GhiChuKhac").Visible = False
        End If

        chkThuGon.Checked = True

    End Sub

    Private Sub LoadDsHoaDon()

        Dim sql As String = "SELECT Id,IdKH,TenKH,DiaChi,MaSoThue,NguoiLienHe,HtThanhToan,TyGia,Thue,KemBangKe, "
        sql &= "DienGiai,NgayHD,SoHD,KyHieuHD,TrangThai,ThanhTien,TienThue,TongTien, GhiSo, "
        sql &= "(SELECT Ten FROM tblTienTe WHERE ID = CHUNGTU.TienTe)TienTe, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID = CHUNGTU.NguoiLap)NguoiLap "
        sql &= "FROM CHUNGTU WHERE Convert(datetime,CONVERT(nvarchar,NgayHD,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "AND LoaiCT = @LoaiCT AND LoaiCT2 = " & LoaiCT2 & " "

        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)

        Dim objTrangThai = CType(cmbTrangThai.EditValue, HoaDonGTGT.TrangThaiHoaDon)
        If objTrangThai.GiaTri <> -1 Then
            AddParameter("@TrangThai", objTrangThai.GiaTri)
            sql &= "AND TrangThai = @TrangThai "
        End If

        sql &= "ORDER BY CONVERT(INT,SOHD) DESC, NgayHD DESC "

        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)

        gdv.DataSource = ExecuteSQLDataTable(sql)


        



    End Sub


    Private Sub btnTaiHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiHoaDon.ItemClick
        LoadDsHoaDon()
    End Sub



    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell

        Try
            Select Case e.Column.FieldName
                Case "HtThanhToan"
                    e.DisplayText = objHinhThucTT.DanhSach.Where(Function(x) x.GiaTri = e.CellValue).FirstOrDefault().TenHinhThuc
                Case "TrangThai"
                    e.DisplayText = objTrangThaiHD.DanhSach.Where(Function(x) x.GiaTri = e.CellValue).FirstOrDefault().TenTrangThai
                Case "Thue"
                    If e.CellValue Is DBNull.Value Then
                        e.DisplayText = "///"
                    Else
                        e.DisplayText = e.CellValue & " %"
                    End If
            End Select
        Catch ex As Exception
        End Try

        Try
            If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True And e.Column.FieldName <> "KyHieuHD" And e.Column.FieldName <> "SoHD" Then
                e.Appearance.ForeColor = Color.Green
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData.FocusedRowChanged
        LoadDsChiTiet()
    End Sub

    Private Sub LoadDsChiTiet()
        If gdvData.FocusedRowHandle < 0 Then
            gdvHangTien.DataSource = Nothing
            gdvThue.DataSource = Nothing
            Exit Sub
        End If

        Dim id As Object = gdvData.GetFocusedRowCellValue("Id")

        Dim sql As String = ""
        sql = "SELECT convert(int,0)SoTT,Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,GhiChuKhac,TaiKhoanNo,TaiKhoanCo, "

        sql &= "(CASE isnull(IdVatTu,'')  "
        sql &= "WHEN '' THEN (SELECT Sophieu FROM XUATKHOAUX WHERE ID = CHUNGTUCHITIET.IdChiTiet) "
        sql &= "ELSE (SELECT Sophieu FROM XUATKHO WHERE ID = CHUNGTUCHITIET.IdChiTiet) "
        sql &= "END)SoPhieu "

        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If chkXemTheoHoaDon.Checked Then

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
                        Dim arrM = reg.Matches(strGhiChu)
                        If arrM.Count = 6 Then
                            dt.Rows(i)("DienGiai") = reg.Match(arrM(0).Value.Trim).Groups(1).Value
                            dt.Rows(i)("DVT") = reg.Match(arrM(1).Value.Trim).Groups(1).Value
                            dt.Rows(i)("SoLuong") = Convert.ToDouble(reg.Match(arrM(2).Value.Trim).Groups(1).Value)
                            dt.Rows(i)("DonGia") = Convert.ToDouble(reg.Match(arrM(3).Value.Trim).Groups(1).Value)
                            dt.Rows(i)("ThanhTien") = Convert.ToDouble(reg.Match(arrM(4).Value.Trim).Groups(1).Value)
                            dt.Rows(i)("GhiChu") = "Nội dung đã được gộp"
                        End If
                    End If


                End If
            Next

            If arrRow.Count > 0 Then
                For Each r As DataRow In arrRow
                    dt.Rows.Remove(r)
                Next
            End If

        End If
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("SoTT") = i + 1
        Next

        gdvHangTien.DataSource = dt

        sql = "SELECT Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.ThueGTGT)
        gdvThue.DataSource = ExecuteSQLDataTable(sql)


        sql = "SELECT Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.GiaVon)
        gdvGiaVon.DataSource = ExecuteSQLDataTable(sql)

    End Sub


    Private Sub chkXemTheoHoaDon_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkXemTheoHoaDon.CheckedChanged
        LoadDsChiTiet()
    End Sub

    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        gdvData.OptionsView.ShowAutoFilterRow = Not gdvData.OptionsView.ShowAutoFilterRow
    End Sub

    Private Sub btnSuaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSuaHoaDon.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Hóa đơn đã ghi sổ không thể sửa!")
            Exit Sub
        End If

        If gdvData.GetFocusedRowCellValue("TrangThai") = HoaDonGTGT.TrangThaiHoaDon.TrangThai.HoaDonDaIn Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
                ShowCanhBao("Bạn không có quyền sửa hóa đơn đã được in!")
                Exit Sub
            End If
        End If


        Dim f As New frmUpdateHoaDon
        f.isDangXuatKho = False
        f.LoaiCT2 = LoaiCT2
        f.Text = "Cập nhật hóa đơn (" & NguoiDung & ")"
        f.idHoaDon = gdvData.GetFocusedRowCellValue("Id")
        TrangThai.isUpdate = True
        f.ShowDialog()

    End Sub

    Private Sub btnThemHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemHoaDon.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If
        TrangThai.isAddNew = True
        Dim f As New frmUpdateHoaDon
        f.LoaiCT2 = LoaiCT2
        f.isDangXuatKho = False
        f.Text = "Lập hóa đơn (" & NguoiDung & ")"
        f.ShowDialog()
    End Sub

    Private Sub btnXoaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaHoaDon.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Hóa đơn đã ghi sổ không thể xóa!")
            Exit Sub
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        If Not ShowCauHoi("Xóa hóa đơn " & gdvData.GetFocusedRowCellValue("SoHD") & " hay không?") Then Exit Sub

        Try

            BeginTransaction()
            Dim sql As String = ""
            sql &= "select ID,SoPhieu,convert(nvarchar,'VT') as isVatTu from xuatkho WITH (NOLOCK) where ID in "
            sql &= "(select IdChiTiet from chungtuchitiet WITH (NOLOCK) where Id_CT = " & gdvData.GetFocusedRowCellValue("Id") & " And ButToan = " & ChungTu.LoaiButToan.HangTien & ") "
            sql &= "UNION ALL "
            sql &= "select ID,SoPhieu,convert(nvarchar,'DV') as isVatTu from xuatkhoaux WITH (NOLOCK) where ID in "
            sql &= "(select IdChiTiet from chungtuchitiet WITH (NOLOCK) where Id_CT = " & gdvData.GetFocusedRowCellValue("Id") & " And ButToan = " & ChungTu.LoaiButToan.HangTien & ") "

            Dim dtId As DataTable = ExecuteSQLDataTable(sql)
            If dtId Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim strIdVatTu As String = "-1"
            Dim strIdDV As String = "-1"
            For i As Integer = 0 To dtId.Rows.Count - 1
                If dtId.Rows(i)("isVatTu") = "VT" Then
                    strIdVatTu &= "," & dtId.Rows(i)("ID")
                ElseIf dtId.Rows(i)("isVatTu") = "DV" Then
                    strIdDV &= "," & dtId.Rows(i)("ID")
                End If
            Next

            'Cập nhật lại trạng thái xuất kho
            sql = "UPDATE xuatkho set Id_CT = null, SoHoaDon = null, Xuatthue = 0 WHERE ID in (" & strIdVatTu & ") "
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            'Cập nhật lại trạng thái xuất kho dịch vụ
            sql = "UPDATE xuatkhoaux set Id_CT = null, SoHoaDon = null, Xuatthue = 0 WHERE ID in (" & strIdDV & ") "
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            'Tính lại tiền thuế phiếu xuất kho
            Dim dtSoPhieu = dtId.DefaultView.ToTable(True, New String() {"SoPhieu"})
            For i As Integer = 0 To dtSoPhieu.Rows.Count - 1
                sql = "update PHIEUXUATKHO set "
                sql &= "TienThue = ( "
                sql &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkho WITH (NOLOCK) where SoPhieu = '" & dtSoPhieu.Rows(i)(0) & "' and Xuatthue = 1), 0) + "
                sql &= "isnull((select round(SUM(DonGia*SoLuong*Mucthue/100.0),2) from xuatkhoaux WITH (NOLOCK) where SoPhieu = '" & dtSoPhieu.Rows(i)(0) & "' and Xuatthue = 1), 0) ) "
                sql &= "where Sophieu = '" & dtSoPhieu.Rows(i)(0) & "'"
                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

            'Xóa chứng từ
            sql = "DELETE FROM CHUNGTU WHERE ID = " & gdvData.GetFocusedRowCellValue("Id")
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            ComitTransaction()
            LoadDsHoaDon()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        End Try


    End Sub


    Private Sub mnuInHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInHoaDon.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        frmUpdateHoaDon.InHoaDon(gdvData.GetFocusedRowCellValue("Id"))
    End Sub

    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Dim tg As DateTime = txtDenNgay.EditValue
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub mnuInNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInNhap.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        frmUpdateHoaDon.InHoaDonNhap(gdvData.GetFocusedRowCellValue("Id"))
    End Sub

    Private Sub mnuInBangKe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInBangKe.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        frmUpdateHoaDon.InBangKe(gdvData.GetFocusedRowCellValue("Id"))
    End Sub


    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown

        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)

        mnuSua.Enabled = calTest.InRowCell
        mnuIn.Enabled = calTest.InRowCell
        mnuXoa.Enabled = calTest.InRowCell
        mnuGhiSo.Enabled = calTest.InRowCell
        mnuDuaVaoXuatKho.Enabled = calTest.InRowCell
        mnuDiChuyen.Enabled = calTest.InRowCell
        mnuXuatBangKe.Enabled = calTest.InRowCell

        If calTest.InRowCell Then
            If calTest.RowHandle >= 0 Then
                If gdvData.GetRowCellValue(calTest.RowHandle, "GhiSo") = True Then
                    mnuGhiSo.Caption = "Bỏ sổ"
                    mnuGhiSo.Glyph = My.Resources.Stop_16
                    mnuGhiSo.Appearance.ForeColor = Color.Red
                Else
                    mnuGhiSo.Caption = "Ghi sổ"
                    mnuGhiSo.Glyph = My.Resources.Start_16
                    mnuGhiSo.Appearance.ForeColor = Color.Blue
                End If
            End If
        End If

        pMnHD.ShowPopup(gdv.PointToScreen(e.Location))

    End Sub


    Private Sub mnuThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThem.ItemClick
        btnThemHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mnuSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSua.ItemClick
        btnSuaHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mnuGhiSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGhiSo.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền sửa hóa đơn!")
            Exit Sub
        End If

        Me.Enabled = False

        If gdvData.GetFocusedRowCellValue("GhiSo") = False Then

            Dim isHopLeTonKho As Boolean = True
            Dim thongBao As String = ""

            'Cảnh báo tồn đầu vào khi chốt sổ
            Dim sql As String = ""
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                If gdvHangTienCT.GetRowCellValue(i, "IdVatTu") Is DBNull.Value Then Continue For
                Dim ngayHD As DateTime = Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayHD"))
                sql = "SET DATEFORMAT DMY "
                sql &= "Select "
                sql &= "ISNULL((SELECT DauKy FROM TONKHOVATTUTHUE WHERE Nam = " & ngayHD.Year & " AND IdVatTu =  " & gdvHangTienCT.GetRowCellValue(i, "IdVatTu") & "),0) "
                sql &= " + "
                sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
                sql &= "WHERE YEAR(CHUNGTU.NgayHD) = " & ngayHD.Year & " AND CHUNGTU.LoaiCT = " & ChungTu.LoaiChungTu.HoaDonDauVao & " AND CHUNGTU.GhiSo = 1 AND CHUNGTUCHITIET.ButToan = 1 "
                sql &= "AND CHUNGTUCHITIET.IdVatTu = " & gdvHangTienCT.GetRowCellValue(i, "IdVatTu") & " AND Convert(datetime,CONVERT(nvarchar,CHUNGTU.NgayHD,103),103) <= Convert(datetime,'" & ngayHD.ToString("dd/MM/yyyy") & "',103) ),0) "
                sql &= " - "
                sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
                sql &= "WHERE YEAR(CHUNGTU.NgayHD) = " & ngayHD.Year & " AND CHUNGTU.LoaiCT = " & ChungTu.LoaiChungTu.HoaDonDauRa & " AND CHUNGTU.GhiSo = 1 AND CHUNGTU.TrangThai = " & HoaDonGTGT.TrangThaiHoaDon.TrangThai.HoaDonDaIn & " AND CHUNGTUCHITIET.ButToan = 1 "
                sql &= "AND CHUNGTUCHITIET.IdVatTu = " & gdvHangTienCT.GetRowCellValue(i, "IdVatTu") & " AND Convert(datetime,CONVERT(nvarchar,CHUNGTU.NgayHD,103),103) <= Convert(datetime,'" & ngayHD.ToString("dd/MM/yyyy") & "',103) ),0) "

                Dim dtKiemTraTon As DataTable = ExecuteSQLDataTable(sql)
                If dtKiemTraTon Is Nothing Then
                    Me.Enabled = True
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If

                If gdvHangTienCT.GetRowCellValue(i, "SoLuong") > dtKiemTraTon.Rows(0)(0) Then
                    isHopLeTonKho = False
                    thongBao &= gdvHangTienCT.GetRowCellValue(i, "DienGiai") & " chỉ còn tồn " & dtKiemTraTon.Rows(0)(0) & " nên không đủ xuất " & gdvHangTienCT.GetRowCellValue(i, "SoLuong") & "!" & vbCrLf
                End If

            Next

            If Not isHopLeTonKho Then
                ShowCanhBao(thongBao)
                Clipboard.SetText(thongBao)
                Me.Enabled = True
                Exit Sub
            End If

        End If

        Dim str As String = ""
        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            AddParameter("@GhiSo", 0)
            str = "Bỏ sổ"
        Else
            AddParameter("@GhiSo", 1)
            AddParameter("@TrangThai", HoaDonGTGT.TrangThaiHoaDon.TrangThai.HoaDonDaIn)
            str = "Ghi sổ"
        End If

        AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
        doUpdate("CHUNGTU", "Id=@dk_Id")

        gdvData.SetFocusedRowCellValue("GhiSo", Not gdvData.GetFocusedRowCellValue("GhiSo"))
        ShowAlert(str & " hóa đơn " & gdvData.GetFocusedRowCellValue("SoHD") & " thành công!")
        Me.Enabled = True

    End Sub

    Private Sub mnuXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoa.ItemClick
        btnXoaHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        mnuInNhap_ItemClick(sender, e)
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        mnuInHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        mnuInBangKe_ItemClick(sender, e)
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick

    End Sub

    Private Sub gdvData_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvData.RowStyle
        'If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True Then
        '    e.Appearance.ForeColor = Color.Green

        'End If
    End Sub


    Private Sub LoadDsLinkXuatKho()
        Dim sqlXuatKho As String = "SET DATEFORMAT DMY "

        sqlXuatKho &= "SELECT * FROM "

        sqlXuatKho &= " ( "

        sqlXuatKho &= "SELECT xk.SoPhieu, pxk.SophieuCG, pxk.Ngaythang, pxk.Congtrinh, xk.IDvattu, "
        sqlXuatKho &= "(select ten from tenvattu where ID = (select IDTenvattu from VATTU WHERE ID = xk.IDvattu))TenVT, "
        sqlXuatKho &= "(select ten from tenhangsanxuat where ID = (select IDHangSanXuat from VATTU WHERE ID = xk.IDvattu))HangSX, "
        sqlXuatKho &= "(select Model from VATTU WHERE ID = xk.IDvattu)Model, "
        sqlXuatKho &= "(select ten from tendonvitinh where ID = (select IDDonvitinh from VATTU WHERE ID = xk.IDvattu))DVT, "
        sqlXuatKho &= "SoLuong,DonGia, Xuatthue, mucthue, SoHoaDon, Id_CT "
        sqlXuatKho &= "FROM  XUATKHO xk RIGHT OUTER JOIN PHIEUXUATKHO pxk ON xk.Sophieu = xk.sophieu "
        sqlXuatKho &= "WHERE Convert(datetime,CONVERT(nvarchar,pxk.Ngaythang,103),103) >=  Convert(datetime,'" & Convert.ToDateTime(txtTuNgayXK.EditValue).ToString("dd/MM/yyyy") & "',103) "
        sqlXuatKho &= "AND Convert(datetime,CONVERT(nvarchar,pxk.Ngaythang,103),103) <= Convert(datetime,'" & Convert.ToDateTime(txtDenNgayXK.EditValue).ToString("dd/MM/yyyy") & "',103) "
        sqlXuatKho &= "AND pxk.IDkhachhang =  " & gdvData.GetFocusedRowCellValue("IdKH") & " "


        sqlXuatKho &= "UNION ALL "
        sqlXuatKho &= "SELECT xk.SoPhieu, pxk.SophieuCG, pxk.Ngaythang, pxk.Congtrinh, null IDvattu, "
        sqlXuatKho &= "xk.NoiDung as TenVT,xk.HangSX,null Model, "
        sqlXuatKho &= "(select ten from tendonvitinh where ID = (select IDDonvitinh from VATTU WHERE ID = xk.Donvi))DVT, "
        sqlXuatKho &= "SoLuong,DonGia, Xuatthue, mucthue, SoHoaDon, Id_CT "
        sqlXuatKho &= "FROM  XUATKHOAUX xk RIGHT OUTER JOIN PHIEUXUATKHO pxk ON xk.Sophieu = xk.sophieu "
        sqlXuatKho &= "WHERE Convert(datetime,CONVERT(nvarchar,pxk.Ngaythang,103),103) >=  Convert(datetime,'" & Convert.ToDateTime(txtTuNgayXK.EditValue).ToString("dd/MM/yyyy") & "',103) "
        sqlXuatKho &= "AND Convert(datetime,CONVERT(nvarchar,pxk.Ngaythang,103),103) <= Convert(datetime,'" & Convert.ToDateTime(txtDenNgayXK.EditValue).ToString("dd/MM/yyyy") & "',103) "
        sqlXuatKho &= "AND pxk.IDkhachhang =  " & gdvData.GetFocusedRowCellValue("IdKH") & " "

        sqlXuatKho &= ")tbl "
        sqlXuatKho &= "order by Ngaythang desc, SoPhieu desc "

        gdvXuatKho.DataSource = ExecuteSQLDataTable(sqlXuatKho)


    End Sub

    Private Sub mnuDuaVaoXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoXuatKho.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        Dim tg As DateTime = GetServerTime()
        txtDenNgayXK.EditValue = tg
        'txtTuNgayXK.EditValue = tg.AddDays(-30)

        LoadDsLinkXuatKho()

        gXuatKho.Text = "Xuất kho: " & gdvData.GetFocusedRowCellValue("TenKH")

        chkXemTheoHoaDon.Checked = False
        chkXemTheoHoaDon.Enabled = False
        tabHoaDon.SelectedTabPageIndex = 1
        Bar1.Visible = False


    End Sub

    Private Sub gHoaDon_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles gXuatKho.Paint

    End Sub

    Private Sub btnTaiLai_Click(sender As System.Object, e As System.EventArgs) Handles btnTaiLai.Click
        LoadDsLinkXuatKho()
    End Sub

    Private Sub txtDenNgayXK_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgayXK.EditValueChanged
        Try
            txtTuNgayXK.EditValue = Convert.ToDateTime(txtDenNgayXK.EditValue).AddDays(-30)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub gdvDataXuatKho_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDataXuatKho.MouseDown

    End Sub

    Private Sub gdvHangTienCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvHangTienCT.MouseDown

        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvHangTienCT.CalcHitInfo(e.Location)


        If calTest.InRowCell Then
            If calTest.RowHandle >= 0 Then
                If Not gdvData.GetRowCellValue(calTest.RowHandle, "IdVatTu") Is DBNull.Value Then
                    mnuLichSuNhapXuat.Enabled = True
                Else
                    mnuLichSuNhapXuat.Enabled = False
                End If
                pLsNhapXuat.ShowPopup(gdvHangTien.PointToScreen(e.Location))

            End If

        End If



    End Sub



    Private Sub btnHuyThaoTacHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnHuyThaoTacHoaDon.Click
        chkXemTheoHoaDon.Checked = True
        chkXemTheoHoaDon.Enabled = True
        tabHoaDon.SelectedTabPageIndex = 0
        Bar1.Visible = True
    End Sub



    Private Sub mnuHoaDonBanHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuHoaDonBanHang.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        AddParameter("@LoaiCT2", ChungTu.LoaiCT2.BanHangHoa)
        AddParameterWhere("@ID", gdvData.GetFocusedRowCellValue("Id"))
        doUpdate("CHUNGTU", "Id=@ID")
        ShowAlert("Chuyển loại chứng từ thành công, bấm Tải để load lại danh sách!")
    End Sub

    Private Sub mnuHoaDonDichVu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuHoaDonDichVu.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        AddParameter("@LoaiCT2", ChungTu.LoaiCT2.BanDichVu)
        AddParameterWhere("@ID", gdvData.GetFocusedRowCellValue("Id"))
        doUpdate("CHUNGTU", "Id=@ID")
        ShowAlert("Chuyển loại chứng từ thành công, bấm Tải để load lại danh sách!")
    End Sub

    Private Sub mnuXuLyCongTrinh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXuLyCongTrinh.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        AddParameter("@LoaiCT2", ChungTu.LoaiCT2.XuLyCongTrinh)
        AddParameterWhere("@ID", gdvData.GetFocusedRowCellValue("Id"))
        doUpdate("CHUNGTU", "Id=@ID")
        ShowAlert("Chuyển loại chứng từ thành công, bấm Tải để load lại danh sách!")
    End Sub


    Private Sub mnuLichSuNhapXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLichSuNhapXuat.ItemClick
        If gdvHangTienCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmLichSuNhapXuatKhoThue
        f.Text = "Lịch sử nhập xuất kho " & gdvHangTienCT.GetFocusedRowCellValue("DienGiai") & " năm " & Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayHD")).Year
        f.idVatTu = gdvHangTienCT.GetFocusedRowCellValue("IdVatTu")
        f.Nam = Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayHD")).Year
        f.idChungTu = gdvHangTienCT.GetFocusedRowCellValue("Id")
        f.ShowDialog()
    End Sub




    Private Sub btnChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChep.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        Dim f As New frmUpdateHoaDon
        f.isDangXuatKho = False
        f.LoaiCT2 = LoaiCT2
        f.Text = "Lập hóa đơn (" & NguoiDung & ")"
        f.idHoaDon = gdvData.GetFocusedRowCellValue("Id")
        TrangThai.isCopy = True

        f.ShowDialog()
    End Sub

    Private Sub mnuChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChep.ItemClick
        btnChep_ItemClick(sender, e)
    End Sub

    Private Sub mnuXuatBangKe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXuatBangKe.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub


        Dim f As New frmHienThiFileExcel

        f.workbook = SpreadsheetGear.Factory.GetWorkbook("\\bacboss\Data$\thue\BangKeChiTietHangHoa.xls", System.Globalization.CultureInfo.CurrentCulture)
        f.excelViewer.ActiveWorkbook = f.workbook


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

        AddParameter("@Id", gdvData.GetFocusedRowCellValue("Id"))
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        f.excelViewer.GetLock()
        Dim ws As SpreadsheetGear.IWorksheet = f.excelViewer.ActiveWorkbook.Sheets(0)


        Dim _cells As SpreadsheetGear.IRange = ws.Cells

        Dim ngayhd As DateTime = Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayHD"))

        _cells("A4").Value = String.Format("(Kèm theo hóa đơn GTGT mẫu số  ... ; ký hiệu {0} số: {1} ngày {2} tháng {3} năm {4} )", _
                                           gdvData.GetFocusedRowCellValue("KyHieuHD").ToString.Trim, _
                                           gdvData.GetFocusedRowCellValue("SoHD"), _
                                           ngayhd.Day, ngayhd.Month, ngayhd.Year)

        _cells("D6").Value = "Công ty Cổ Phần Dịch Vụ Kỹ Thuật Bảo An"
        _cells("D7").Value = "Số 3A phố Lý Tự Trọng- P. Minh khai- Q.Hồng Bàng-TP. Hải Phòng, Việt Nam"
        _cells("D8").Value = "'0200682529"

        _cells("D9").Value = gdvData.GetFocusedRowCellValue("TenKH")
        _cells("D10").Value = gdvData.GetFocusedRowCellValue("DiaChi")
        _cells("D11").Value = "'" & gdvData.GetFocusedRowCellValue("MaSoThue")

        Dim rowIndex As Integer = 14
        Dim sttKT As Integer = 1

        For Each r As DataRow In dt.Rows
            _cells(rowIndex, 1, rowIndex, 9).Insert(SpreadsheetGear.InsertShiftDirection.Down)
        Next

        _cells(rowIndex + dt.Rows.Count, 1, rowIndex + dt.Rows.Count, 9).Delete(SpreadsheetGear.DeleteShiftDirection.Up)
        _cells(rowIndex - 1, 1, rowIndex - 1, 9).Delete(SpreadsheetGear.DeleteShiftDirection.Up)

        For Each r As DataRow In dt.Rows

            _cells("C" & rowIndex & ":F" & rowIndex).Merge()
            _cells("B" & rowIndex).Value = sttKT
            _cells("C" & rowIndex).Value = r("DienGiai")
            _cells("G" & rowIndex).Value = r("DVT")
            _cells("H" & rowIndex).Value = r("SoLuong")
            _cells("I" & rowIndex).Value = r("DonGia")
            _cells("J" & rowIndex).Value = r("ThanhTien")

            Dim border = _cells(rowIndex - 1, 1, rowIndex - 1, 9).Range.Borders
            border(SpreadsheetGear.BordersIndex.EdgeBottom).LineStyle = SpreadsheetGear.LineStyle.Dot

            sttKT += 1
            rowIndex += 1

            '    '_cells(rowIndex, 1, rowIndex, 9).Borders(SpreadsheetGear.BordersIndex.DiagonalDown).LineStyle = SpreadsheetGear.LineStyle.Dot


            '    'Dim style = _cells(rowIndex, 1, rowIndex, 9).Style
            '    'style.Borders(SpreadsheetGear.BordersIndex.DiagonalDown).LineStyle = SpreadsheetGear.LineStyle.Dot

        Next

        Dim border2 = _cells(rowIndex - 1, 1, rowIndex - 1, 9).Range.Borders
        border2(SpreadsheetGear.BordersIndex.EdgeTop).LineStyle = SpreadsheetGear.LineStyle.Continuous

        _cells("C" & rowIndex + 1).Value = String.Format("Thuế suất GTGT: {0} %", dt.Rows(0)("Thue"))

        _cells("J" & rowIndex + 0).Value = dt.Rows(0)("TongTienHang")
        _cells("J" & rowIndex + 1).Value = dt.Rows(0)("TongTienThue")
        _cells("J" & rowIndex + 2).Value = dt.Rows(0)("TongThanhTien")

        _cells("D" & rowIndex + 4).Value = Utils.StringHelper.VIE2String(dt.Rows(0)("TongThanhTien"), False, "đồng", "lẻ", "phẩy", 2)

        f.excelViewer.ReleaseLock()

        f.Tag = "BangKeHoaDonBanRa_" & gdvData.GetFocusedRowCellValue("SoHD")

        f.Text = "Bảng kê chi tiết hàng hóa bán ra"
        f.ShowDialog()


    End Sub


    Private Sub chkThuGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkThuGon.CheckedChanged

        Try
            gdvData.Columns("DiaChi").Visible = Not chkThuGon.Checked
            gdvData.Columns("MaSoThue").Visible = Not chkThuGon.Checked
            gdvData.Columns("NguoiLienHe").Visible = Not chkThuGon.Checked
            gdvData.Columns("HtThanhToan").Visible = Not chkThuGon.Checked
            gdvData.Columns("TienTe").Visible = Not chkThuGon.Checked
            gdvData.Columns("TyGia").Visible = Not chkThuGon.Checked
            gdvData.Columns("Thue").Visible = Not chkThuGon.Checked
            gdvData.Columns("KemBangKe").Visible = Not chkThuGon.Checked
            gdvData.Columns("NguoiDaiDien").Visible = Not chkThuGon.Checked
            gdvData.Columns("TaiKhoanNganHang").Visible = Not chkThuGon.Checked
            gdvData.Columns("TrangThai").Visible = Not chkThuGon.Checked
            gdvData.Columns("DienGiai").Visible = Not chkThuGon.Checked
            gdvData.Columns("NguoiLap").Visible = Not chkThuGon.Checked
        Catch ex As Exception

        End Try
        

    End Sub



End Class
