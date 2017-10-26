Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmThaoGhepVatTuBo


    Public isBoXung As Boolean = False


    Private Sub frmThaoGhepVatTuBo_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim tg As DateTime = GetServerTime()


        txtDenNgay.EditValue = tg
        txtTuNgay.EditValue = tg.AddDays(-30)

        LoadDuLieu()

        If isBoXung Then
            btnGhiSo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnDuaVaoBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnGhiSo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnDuaVaoBoSung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If



    End Sub


    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        'gdvData.OptionsView.ShowAutoFilterRow = Not gdvData.OptionsView.ShowAutoFilterRow
    End Sub

    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Dim tg As DateTime = txtDenNgay.EditValue
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub btnThemHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemHoaDon.ItemClick


        TrangThai.isAddNew = True
        Dim f As New frmUpdateGhepVatTuBo
        f.Text = "Lập chứng từ ghép vật tư bộ"
        f.ShowDialog()
        LoadDuLieu()


    End Sub

    Public Sub LoadDuLieu()

        Dim sql As String = "SELECT Id,NgayCT,SoCT,DienGiai,GhiSo,ThanhTien, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID = CHUNGTU.NguoiLap)NguoiLap,refId "
        sql &= "FROM CHUNGTU WHERE Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "AND LoaiCT = @LoaiCT "


        If isBoXung Then sql &= "AND GhiSo = 0 "


        sql &= "ORDER BY NgayCT DESC"

        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.GhepVatTu)
        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdv.DataSource = dt
        End If

    End Sub


    Private Sub btnTaiHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiHoaDon.ItemClick

        LoadDuLieu()

        Try
            LoadDuLieuChiTiet()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData.FocusedRowChanged




        LoadDuLieuChiTiet()


    End Sub


    Private Sub LoadDuLieuChiTiet()

        If gdvData.FocusedRowHandle < 0 Then
            'gdvChiTiet.DataSource = Nothing
            Exit Sub
        End If



        Dim id As Object = gdvData.GetFocusedRowCellValue("Id")
        Dim sql As String = ""


        'Nhập kho
        sql = "SELECT convert(int,0)SoTT,b.DienGiai TenHoaDon,b.DVT TenDVT,b.SoLuong,b.DonGia,b.ThanhTien,b.GhiChu,b.TaiKhoanNo,b.TaiKhoanCo "
        sql &= "FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
        sql &= "WHERE a.refId = @refId AND a.LoaiCT = @LoaiCT AND a.LoaiCT2 = @LoaiCT2  AND ButToan = 1  "
        AddParameter("@refId", id)
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
        AddParameter("@LoaiCT2", ChungTu.LoaiCT2.NhapKho)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("SoTT") = i + 1
        Next
        gdv1.DataSource = dt


        'Xuất kho
        sql = "SELECT convert(int,0)SoTT,b.DienGiai TenHoaDon,b.DVT TenDVT,b.SoLuong,b.TaiKhoanNo,b.TaiKhoanCo,"
        sql &= "b.DonGia,b.ThanhTien,"

        'sql &= "(select top 1 dongia from chungtuchitiet where id_ct = a.ID and IdVatTu = b.IdVatTu and buttoan = 3  and SoLuong = b.SoLuong)DonGia,"
        'sql &= "(select top 1 thanhtien from chungtuchitiet where id_ct = a.ID and IdVatTu = b.IdVatTu and buttoan = 3  and SoLuong = b.SoLuong)ThanhTien,"

        sql &= "b.GhiChu,b.TaiKhoanNo,b.TaiKhoanCo "
        sql &= "FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
        sql &= "WHERE a.refId = @refId AND a.LoaiCT = @LoaiCT AND a.LoaiCT2 = @LoaiCT2 AND ButToan = @ButToan "
        AddParameter("@refId", id)
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
        AddParameter("@LoaiCT2", ChungTu.LoaiCT2.XuatKho)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt2 As DataTable = ExecuteSQLDataTable(sql)
        For i As Integer = 0 To dt2.Rows.Count - 1
            dt2.Rows(i)("SoTT") = i + 1
        Next

        gdv2.DataSource = dt2


        If gdvData.GetRowCellValue(gdvData.FocusedRowHandle, "GhiSo") = True Then
            mnuGhiSo.Caption = "Bỏ sổ"
            mnuGhiSo.Glyph = My.Resources.Stop_16
            mnuGhiSo.Appearance.ForeColor = Color.Red
        Else
            mnuGhiSo.Caption = "Ghi sổ"
            mnuGhiSo.Glyph = My.Resources.Start_16
            mnuGhiSo.Appearance.ForeColor = Color.Blue
        End If
    End Sub

    Private Sub btnSuaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSuaHoaDon.ItemClick


        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
        '    ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
        '    Exit Sub
        'End If


        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Chứng từ đã ghi sổ không thể sửa!")
            Exit Sub
        End If


        'Kiểm tra đã phát sinh chứng từ phân bổ hay chưa.
        'For i As Integer = 0 To gdvDataChiTiet.RowCount - 1
        '    Dim sql As String = "SELECT COUNT(Id) FROM CHUNGTUCHITIET WHERE IdChiTiet = " & gdvDataChiTiet.GetRowCellValue(i, "Id") & " "
        '    sql &= "AND GiaTriKhac = 0 "
        '    Dim dt As DataTable = ExecuteSQLDataTable(sql)
        '    If dt.Rows(0)(0) > 0 Then
        '        ShowCanhBao(gdvDataChiTiet.GetRowCellValue(i, "DienGiai") & " đã phát sinh " & dt.Rows(0)(0) & " chứng từ phân bổ.")
        '        Exit Sub
        '    End If
        'Next

        TrangThai.isUpdate = True
        Dim f As New frmUpdateGhepVatTuBo
        f.Text = "Cập nhật chứng từ ghép vật tư bộ"
        f.idGhepVatTuBo = gdvData.GetFocusedRowCellValue("Id")
        f.ShowDialog()
        LoadDuLieu()

    End Sub

    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs)
        Try
            If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True Then
                e.Appearance.ForeColor = Color.Green
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)

        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)

        mnuSuaCT.Enabled = calTest.InRowCell
        mnuXoaCT.Enabled = calTest.InRowCell
        mnuGhiSoCT.Enabled = calTest.InRowCell
        If calTest.InRowCell Then
            If calTest.RowHandle >= 0 Then
                If gdvData.GetRowCellValue(calTest.RowHandle, "GhiSo") = True Then
                    mnuGhiSoCT.Caption = "Bỏ sổ"
                    mnuGhiSoCT.Glyph = My.Resources.Stop_16
                    mnuGhiSoCT.Appearance.ForeColor = Color.Red
                Else
                    mnuGhiSoCT.Caption = "Ghi sổ"
                    mnuGhiSoCT.Glyph = My.Resources.Start_16
                    mnuGhiSoCT.Appearance.ForeColor = Color.Blue
                End If
            End If
        End If
        pMnu.ShowPopup(gdv.PointToScreen(e.Location))

    End Sub

    Private Sub mnuThemCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemCT.ItemClick
        btnThemHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mnuSuaCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSuaCT.ItemClick
        btnSuaHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mnuXoaCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaCT.ItemClick
        btnXoaHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mnuGhiSoCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGhiSoCT.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền sửa chứng từ!")
            Exit Sub
        End If

        'Dim sql As String = "SELECT COUNT(Id) FROM CHUNGTUCHITIET WHERE "

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
        ShowAlert(str & " chứng từ thành công!")

    End Sub

    Private Sub btnXoaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaHoaDon.ItemClick


        If gdvData.FocusedRowHandle < 0 Then Exit Sub


        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Chứng từ đã ghi sổ không thể xóa!")
            Exit Sub
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        If Not ShowCauHoi("Xóa chứng từ bạn vừa chọn hay không?") Then Exit Sub

        Try
            BeginTransaction()
            'Xóa chứng từ
            Dim sql As String = "DELETE FROM CHUNGTU WHERE ID = " & gdvData.GetFocusedRowCellValue("Id")
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            sql = "DELETE FROM CHUNGTU WHERE refId = " & gdvData.GetFocusedRowCellValue("Id")
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            ComitTransaction()
            LoadDuLieu()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        End Try

        Try
            If gdvData.FocusedRowHandle < 0 Then
                gdv1.DataSource = Nothing
                gdv2.DataSource = Nothing
            End If
            LoadDuLieuChiTiet()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gdvData_CustomDrawCell_1(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        Try
            If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True Then
                e.Appearance.ForeColor = Color.Green
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub btnGhiSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGhiSo.ItemClick


        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        Dim st As Boolean = gdvData.GetFocusedRowCellValue("GhiSo")


        If st = True Then

            AddParameter("@GhiSo", 0)
            AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
            doUpdate("CHUNGTU", "Id=@dk_Id")

            AddParameter("@GhiSo", 0)
            AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
            doUpdate("CHUNGTU", "refId=@dk_Id")
            ShowAlert("Bỏ sổ chứng từ thành công")

            gdvData.SetFocusedRowCellValue("GhiSo", 0)

            Exit Sub
        End If


        sql = " SELECT b.IdVatTu, b.SoLuong, b.DienGiai "
        sql &= "FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
        sql &= "WHERE a.refId = @refId AND a.LoaiCT = @LoaiCT AND a.LoaiCT2 = @LoaiCT2 AND ButToan = 1 "
        AddParameter("@refId", gdvData.GetFocusedRowCellValue("Id"))
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
        AddParameter("@LoaiCT2", ChungTu.LoaiCT2.XuatKho)

        Dim dtVT As DataTable = ExecuteSQLDataTable(sql)


        Dim ngay As DateTime = gdvData.GetFocusedRowCellValue("NgayCT")
        ngay = New DateTime(ngay.Year, ngay.Month, ngay.Day)
        Dim nam As Integer = Convert.ToDateTime(gdvData.GetFocusedRowCellValue("NgayCT")).Year

        'Check tồn
        Dim strThongBao As String = ""
        Dim kt As Boolean = True

        For i As Integer = 0 To dtVT.Rows.Count - 1
            If dtVT.Rows(i)("IdVatTu") Is DBNull.Value Then Continue For
            sql = "SELECT "
            sql &= "(ISNULL((select DauKy from tonkhovattuthue where IdVatTu = " & dtVT.Rows(i)("IdVatTu") & " and Nam =  " & nam & " ),0)   "
            sql &= " +   "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT   "
            sql &= "WHERE CHUNGTUCHITIET.IdVatTu = " & dtVT.Rows(i)("IdVatTu") & " AND CHUNGTU.LOAICT IN (2) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) =  " & nam & "   "
            sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay1 "
            sql &= "AND CHUNGTUCHITIET.ButToan = 1),0)   "
            sql &= "-  "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT   "
            sql &= "WHERE CHUNGTUCHITIET.IdVatTu = " & dtVT.Rows(i)("IdVatTu") & " AND CHUNGTU.LOAICT IN (1) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) =  " & nam & "   "
            sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay2 "
            sql &= "AND CHUNGTUCHITIET.ButToan = 1),0)   "
            sql &= ")  "

            AddParameter("@DenNgay1", ngay)
            AddParameter("@DenNgay2", ngay)

            Dim dtTon As DataTable = ExecuteSQLDataTable(sql)
            If dtVT.Rows(i)("SoLuong") > dtTon.Rows(0)(0) Then
                kt = False
                strThongBao &= dtVT.Rows(i)("DienGiai").ToString & " tồn " & dtTon.Rows(0)(0) & " không đủ xuất " & dtVT.Rows(i)("SoLuong") & " ." & vbCrLf
            End If

        Next


        If Not kt Then
            ShowCanhBao(strThongBao)
            Exit Sub
        Else

            AddParameter("@GhiSo", 1)
            AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
            doUpdate("CHUNGTU", "Id=@dk_Id")

            AddParameter("@GhiSo", 1)
            AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
            doUpdate("CHUNGTU", "refId=@dk_Id")
            ShowAlert("Ghi sổ chứng từ thành công")

            gdvData.SetFocusedRowCellValue("GhiSo", 1)

        End If

    End Sub


    Public isOK As Boolean = False
    Public arrVatTu As List(Of DictionaryEntry)

    Private Sub btnDuaVaoBoSung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDuaVaoBoSung.ItemClick

        If gdvData.FocusedRowHandle < 0 Then
            ShowCanhBao("Chưa chọn vật tư bộ cần bổ xung!")
            Exit Sub
        End If

        If Not ShowCauHoi("Bổ xung vật tư vào vật tư bộ đã chọn hay không?") Then Exit Sub

        Dim idCT As Object = gdvData.GetFocusedRowCellValue("Id")

        Try

            Dim sql As String = ""

            'lấy id chứng từ xuất kho
            sql = "SELECT ID FROM CHUNGTU WHERE refID = " & idCT & " AND LoaiCT =  " & ChungTu.LoaiChungTu.HoaDonDauRa & " AND LoaiCT2 = " & ChungTu.LoaiCT2.XuatKho

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Dim idXK As Object = dt.Rows(0)(0)

            For i As Integer = 0 To arrVatTu.Count - 1

                Dim o As DictionaryEntry = arrVatTu(i)
                sql = "SELECT ID, Model,Code, "
                sql &= "(SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu)TenVatTu, "
                sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT "
                sql &= "FROM VATTU WHERE ID = " & o.Key

                Dim dtVT As DataTable = ExecuteSQLDataTable(sql)
                If dtVT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Dim r As DataRow = dtVT.Rows(0)

                AddParameter("@Id_CT", idXK)
                AddParameter("@IdVatTu", arrVatTu(i).Key)

                AddParameter("@DienGiai", r("TenVatTu") & " " & r("Model"))
                AddParameter("@DVT", r("DVT"))
                AddParameter("@SoLuong", o.Value)
                AddParameter("@DonGia", 0)
                AddParameter("@ThanhTien", 0)
                AddParameter("@ThanhTienQD", 0)
                AddParameter("@TaiKhoanNo", "")
                AddParameter("@TaiKhoanCo", "")
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

                Dim idXK_CT As Object = doInsert("CHUNGTUCHITIET")

                If idXK_CT Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Next
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

        ShowAlert("Cập nhật thao tác ghép vật tư bộ thành công!")

        Me.ParentForm.Close()

    End Sub

    Private Sub gdvData_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvData.RowCellClick
        Try
            LoadDuLieuChiTiet()
        Catch ex As Exception

        End Try
    End Sub
End Class
