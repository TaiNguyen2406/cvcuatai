Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmPhanBoCCDC


    Private Sub frmGhiTangCCDC_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim tg As DateTime = GetServerTime()

        txtDenNgay.EditValue = tg
        txtTuNgay.EditValue = tg.AddDays(-30)

        LoadDuLieu()
    End Sub


    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        'gdvData.OptionsView.ShowAutoFilterRow = Not gdvData.OptionsView.ShowAutoFilterRow
    End Sub

    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Dim tg As DateTime = txtDenNgay.EditValue
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub btnThemHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemHoaDon.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isAddNew = True
        Dim f As New frmUpdatePhanBoCCDC
        f.Text = "Lập chứng từ phân bổ CCDC"
        f.ShowDialog()
        LoadDuLieu()

    End Sub

    Public Sub LoadDuLieu()

        Dim sql As String = "SELECT Id,NgayCT,SoCT,DienGiai,GhiSo,ThanhTien, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID = CHUNGTU.NguoiLap)NguoiLap, "
        sql &= "(SELECT MaPhongBan + ' - ' + TenPhongBan FROM PhongBanThue WHERE Id = CHUNGTU.IdPhongBan)PhongBan "
        sql &= "FROM CHUNGTU WHERE Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "AND LoaiCT = @LoaiCT "
        sql &= "ORDER BY NgayCT DESC"

        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.PhanBoCCDC)
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
    End Sub

    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData.FocusedRowChanged

        If gdvData.FocusedRowHandle < 0 Then
            gdvChiTiet.DataSource = Nothing
            Exit Sub
        End If

        Dim id As Object = gdvData.GetFocusedRowCellValue("Id")

        Dim sql As String = ""

        sql = "SELECT convert(int,0)STT,Id,IdVatTu,IdChiTiet,DienGiai,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo,GiaTriKhac, "
        sql &= "convert(int,0)SoKyPhanBo,convert(int,0)SoKyDaPhanBo,convert(int,0)GiaTriBanDau,convert(nvarchar,N'')SoCT "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("STT") = i + 1
            If dt.Rows(i)("GiaTriKhac") Is DBNull.Value Then
                dt.Rows(i)("SoCT") = "Đầu kỳ"
                sql = "SELECT  SoKyPhanBo, (SoKyPhanBo-SoKyConLai+SoKyPhanBoTrongNam)SoKyDaPhanBo, round((GiaTriConLai/SoKyConLai),0)TienPhanBo "
                sql &= "FROM TONKHOTHUECCDC WHERE ID = " & dt.Rows(i)("IdChiTiet")
                Dim dtx As DataTable = ExecuteSQLDataTable(sql)
                dt.Rows(i)("SoKyPhanBo") = dtx.Rows(0)("SoKyPhanBo")
                'dt.Rows(i)("SoKyDaPhanBo") = dtx.Rows(0)("SoKyDaPhanBo")
                dt.Rows(i)("GiaTriBanDau") = Math.Round(dtx.Rows(0)("SoKyPhanBo") * dtx.Rows(0)("TienPhanBo"), 0, MidpointRounding.AwayFromZero)
            Else
                sql = "SELECT a.SoCT,b.IdVatTu,b.DienGiai,b.ThanhTien,b.GiaTriKhac,b.GiaTriKhac2,b.Id FROM CHUNGTUCHITIET b RIGHT OUTER JOIN CHUNGTU a ON b.Id_CT = a.Id "
                sql &= "WHERE b.Id = " & dt.Rows(i)("IdChiTiet")
                Dim dtx As DataTable = ExecuteSQLDataTable(sql)
                dt.Rows(i)("SoKyPhanBo") = dtx.Rows(0)("GiaTriKhac")
                'dt.Rows(i)("SoKyDaPhanBo") = dtx.Rows(0)("GiaTriKhac2")
                dt.Rows(i)("GiaTriBanDau") = dtx.Rows(0)("ThanhTien")
            End If
        Next

        gdvChiTiet.DataSource = dt


    End Sub

    Private Sub btnSuaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSuaHoaDon.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Chứng từ đã ghi sổ không thể sửa!")
            Exit Sub
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isUpdate = True
        Dim f As New frmUpdateGhiTangCCDC
        f.idChungTu = gdvData.GetFocusedRowCellValue("Id")
        f.Text = "Cập nhật chứng từ ghi tăng CCDC"
        f.ShowDialog()
        LoadDuLieu()
    End Sub

    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        Try
            If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True Then
                e.Appearance.ForeColor = Color.Green
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown

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

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If
        If Not ShowCauHoi("Xóa chứng từ bạn vừa chọn hay không?") Then Exit Sub


        Try
            Dim sql As String = "SELECT Id,IdVatTu,GiaTriKhac,GiaTriKhac2,IdChiTiet FROM CHUNGTUCHITIET WHERE Id_CT = " & gdvData.GetFocusedRowCellValue("Id")
            Dim dt As DataTable = ExecuteSQLDataTable(sql)

            BeginTransaction()

            'Cập nhật lại số kỳ đã phân bổ

            For i As Integer = 0 To dt.Rows.Count - 1
                AddParameter("@Id", dt.Rows(i)("IdChiTiet"))
                If dt.Rows(i)("GiaTriKhac") Is DBNull.Value Then
                    If ExecuteSQLNonQuery("UPDATE TONKHOTHUECCDC SET SoKyPhanBoTrongNam = SoKyPhanBoTrongNam - 1 WHERE ID = @Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else
                    If ExecuteSQLNonQuery("UPDATE CHUNGTUCHITIET SET GiaTriKhac2 = GiaTriKhac2 - 1 WHERE ID = @Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            ''Xóa chứng từ
            sql = "DELETE FROM CHUNGTU WHERE ID = " & gdvData.GetFocusedRowCellValue("Id")
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            ComitTransaction()
            LoadDuLieu()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        End Try


    End Sub

End Class
