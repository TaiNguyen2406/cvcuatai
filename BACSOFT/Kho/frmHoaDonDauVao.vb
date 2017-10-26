Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Public Class frmHoaDonDauVao

    Private objHinhThucTT As HoaDonGTGT.HinhThucThanhToan

    Public LoaiCT2 As ChungTu.LoaiCT2


    Public isLayChungTuChiPhi As Boolean = False
    Public isLayChungTuCCDC As Boolean = False


    Private Sub frmHoaDonDauVao_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        If isLayChungTuChiPhi Then
            gdvData.OptionsSelection.MultiSelect = True
            gdvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        ElseIf isLayChungTuCCDC Then
            gdvData.OptionsSelection.MultiSelect = False
            gdvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End If

        Dim tg As DateTime = GetServerTime()

        txtDenNgay.EditValue = tg
        txtTuNgay.EditValue = tg.AddDays(-30)

        objHinhThucTT = New HoaDonGTGT.HinhThucThanhToan
        objHinhThucTT.KhoiTao()

        LoadDsHoaDon()

        btnChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        mnuChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu Then
            gdvHangTienCT.Columns("IdVatTu").Visible = False
            gdvHangTienCT.Columns("SoLuong").Visible = False
            gdvHangTienCT.Columns("DonGia").Visible = False
            gdvHangTienCT.Columns("DVT").Visible = False
            gdvHangTienCT.Columns("SoPhieu").Visible = False
            btnChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mnuChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            gdvHangTienCT.Columns("ChiPhi").Visible = False
            tabChiPhi.PageVisible = False
        ElseIf LoaiCT2 = ChungTu.LoaiCT2.MuaCongCuDungCu _
            Or LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai _
            Or LoaiCT2 = ChungTu.LoaiCT2.MuaTaiSanCoDinh Then
            gdvHangTienCT.Columns("SoPhieu").Visible = False
        End If

    End Sub

    Private Sub LoadDsHoaDon()

        Dim sql As String = "SELECT Id,(SELECT ttcMa FROM KHACHHANG WHERE ID = CHUNGTU.IdKH)MaNhaCungCap,TenKH,DiaChi,MaSoThue,NguoiLienHe,HtThanhToan,TyGia,Thue,KemBangKe, "
        sql &= "DienGiai,NgayHD,SoHD,KyHieuHD,TrangThai,ThanhTien,TienThue,TongTien,GhiSo, "
        sql &= "(SELECT Ten FROM tblTienTe WHERE ID = CHUNGTU.TienTe)TienTe, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID = CHUNGTU.NguoiLap)NguoiLap "

        'If LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu Then
        '    gdvData.Columns("SoDH").Visible = True
        '    sql &= ", (SELECT Top 1 GhiChuKhac FROM CHUNGTUCHITIET WHERE Id_CT = CHUNGTU.ID AND ButToan = 1)SoDH "
        'Else
        '    gdvData.Columns("SoDH").Visible = False
        'End If

        sql &= "FROM CHUNGTU WHERE Convert(datetime,CONVERT(nvarchar,NgayHD,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "AND LoaiCT = @LoaiCT "
        sql &= "AND LoaiCT2 = " & LoaiCT2 & " "

        If isLayChungTuChiPhi Or isLayChungTuCCDC Then
            sql &= " AND refId is null "
        End If


        sql &= "ORDER BY NgayHD DESC"

        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)

        gdv.DataSource = ExecuteSQLDataTable(sql)

    End Sub


    Private Sub btnTaiHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiHoaDon.ItemClick
        LoadDsHoaDon()
    End Sub


    Private Sub gdvData_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvData.RowCellStyle

    End Sub

    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        Try
            Select Case e.Column.FieldName
                Case "HtThanhToan"
                    e.DisplayText = objHinhThucTT.DanhSach.Where(Function(x) x.GiaTri = e.CellValue).FirstOrDefault().TenHinhThuc
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

        If gdvData.FocusedRowHandle < 0 Then
            gdvHangTien.DataSource = Nothing
            gdvThue.DataSource = Nothing
            Exit Sub
        End If

        Dim id As Object = gdvData.GetFocusedRowCellValue("Id")

        Dim sql As String = ""

        sql = "SELECT convert(int,0)SoTT,Id,ref,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo,GhiChuKhac,ChiPhi, "

        sql &= "(SELECT Sophieu FROM NHAPKHO WHERE ID = CHUNGTUCHITIET.IdChiTiet)SoPhieu "

        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
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


        sql = "select Id,NgayHD,SoHD,KyHieuHD,DienGiai,ThanhTien,TienThue,TongTien from chungtu  "
        sql &= "WHERE refid = @Id "
        AddParameter("@Id", id)
        gdvChiPhi.DataSource = ExecuteSQLDataTable(sql)

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

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        Dim f As New frmUpdateHdDauVao
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
        Dim f As New frmUpdateHdDauVao
        f.LoaiCT2 = LoaiCT2
        f.isDangXuatKho = False
        f.Text = "Nhập hóa đơn (" & NguoiDung & ")"
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
            Dim sql As String = "select ID,SoPhieu from nhapkho WITH (NOLOCK) where ID in "
            sql &= "(select IdChiTiet from chungtuchitiet WITH (NOLOCK) where Id_CT = " & gdvData.GetFocusedRowCellValue("Id") & " And ButToan = " & ChungTu.LoaiButToan.HangTien & ") "
            Dim dtId As DataTable = ExecuteSQLDataTable(sql)
            If dtId Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim strId As String = "-1"
            For i As Integer = 0 To dtId.Rows.Count - 1
                strId &= "," & dtId.Rows(i)("ID")
            Next

            'Cập nhật lại trạng thái nhập kho
            sql = "UPDATE nhapkho set Id_CT = null, SoHoaDon = null, Nhapthue = 0 WHERE ID in (" & strId & ") "
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            'Tính lại tiền thuế phiếu nhập kho
            Dim dtSoPhieu = dtId.DefaultView.ToTable(True, New String() {"SoPhieu"})
            For i As Integer = 0 To dtSoPhieu.Rows.Count - 1
                sql = "update PHIEUNHAPKHO set "
                sql &= "TienThue = (select isnull(round(SUM(DonGia*SoLuong*Mucthue/100.0),2),0) from nhapkho WITH (NOLOCK) where SoPhieu = '" & dtSoPhieu.Rows(i)(0) & "' and Nhapthue = 1) "
                sql &= "where Sophieu = '" & dtSoPhieu.Rows(i)(0) & "' "
                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

            'Xóa chứng từ
            sql = "DELETE FROM CHUNGTU WHERE ID = " & gdvData.GetFocusedRowCellValue("Id")
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)


            'Check hoa don dịch vụ bên chi phí
            If LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu Then
                sql = "UPDATE CHIPHI SET IdChungTu = null WHERE IdChungTu = " & gdvData.GetFocusedRowCellValue("Id")
                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            ComitTransaction()
            LoadDsHoaDon()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        End Try


    End Sub


    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Dim tg As DateTime = txtDenNgay.EditValue
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown

        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)


        If isLayChungTuChiPhi Then
            pCP.ShowPopup(gdv.PointToScreen(e.Location))
        ElseIf isLayChungTuCCDC Then
            pCC.ShowPopup(gdv.PointToScreen(e.Location))
        Else
            mnuSua.Enabled = calTest.InRowCell
            mnuChep.Enabled = calTest.InRowCell
            mnuXoa.Enabled = calTest.InRowCell
            mnuGhiSo.Enabled = calTest.InRowCell
            mnuXuatBangKe.Enabled = calTest.InRowCell
            mnuDiChuyen.Enabled = calTest.InRowCell
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
        End If


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

        Dim str As String = ""
        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            AddParameter("@GhiSo", 0)
            str = "Bỏ sổ"
        Else
            AddParameter("@GhiSo", 1)
            str = "Ghi sổ"
        End If

        AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
        doUpdate("CHUNGTU", "Id=@dk_Id")


        gdvData.SetFocusedRowCellValue("GhiSo", Not gdvData.GetFocusedRowCellValue("GhiSo"))
        ShowAlert(str & " hóa đơn " & gdvData.GetFocusedRowCellValue("SoHD") & " thành công!")

    End Sub


    Private Sub mnuXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoa.ItemClick
        btnXoaHoaDon_ItemClick(sender, e)
    End Sub


    'Private Sub gdvData_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvData.RowStyle
    '    If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True Then
    '        e.Appearance.ForeColor = Color.Green
    '    End If
    'End Sub


    Private Sub btnLichSuNhapXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLichSuNhapXuat.ItemClick
        If gdvHangTienCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmLichSuNhapXuatKhoThue
        f.Text = "Lịch sử nhập xuất kho " & gdvHangTienCT.GetFocusedRowCellValue("DienGiai") & " năm " & Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayHD")).Year
        f.idVatTu = gdvHangTienCT.GetFocusedRowCellValue("IdVatTu")
        f.Nam = Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayHD")).Year
        f.idChungTu = gdvHangTienCT.GetFocusedRowCellValue("Id")
        f.ShowDialog()
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


    Private Sub btnChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChep.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        Dim f As New frmUpdateHdDauVao
        f.isDangXuatKho = False
        f.LoaiCT2 = LoaiCT2
        f.Text = "Nhập hóa đơn (" & NguoiDung & ")"
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

        _cells("D9").Value = "Công ty Cổ Phần Dịch Vụ Kỹ Thuật Bảo An"
        _cells("D10").Value = "Số 3A phố Lý Tự Trọng- P. Minh khai- Q.Hồng Bàng-TP. Hải Phòng, Việt Nam"
        _cells("D11").Value = "'0200682529"

        _cells("D6").Value = gdvData.GetFocusedRowCellValue("TenKH")
        _cells("D7").Value = gdvData.GetFocusedRowCellValue("DiaChi")
        _cells("D8").Value = "'" & gdvData.GetFocusedRowCellValue("MaSoThue")

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

        f.Tag = "BangKeHoaDonMuaVao_" & gdvData.GetFocusedRowCellValue("SoHD")

        f.Text = "Bảng kê chi tiết hàng hóa mua vào"
        f.ShowDialog()
    End Sub

    Public arrListHoaDonChiPhi As List(Of Long)
    Private Sub mnuDuaVaoChiPhiHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoChiPhiHoaDon.ItemClick
        arrListHoaDonChiPhi = New List(Of Long)
        For Each r As Integer In gdvData.GetSelectedRows
            arrListHoaDonChiPhi.Add(gdvData.GetRowCellValue(r, "Id"))
        Next
        CType(Me.Parent, Form).Close()
    End Sub

    Public strNoiDungDienGiai As String = ""
    Public IdHoaDonCCDC As Long
    Private Sub mnuChonGhiTangCCDC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChonGhiTangCCDC.ItemClick
        IdHoaDonCCDC = gdvData.GetFocusedRowCellValue("Id")
        strNoiDungDienGiai = gdvData.GetFocusedRowCellValue("DienGiai")
        CType(Me.Parent, Form).Close()
    End Sub


End Class
