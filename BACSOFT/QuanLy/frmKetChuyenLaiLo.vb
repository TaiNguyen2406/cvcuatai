Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmKetChuyenLaiLo


    Private Sub frmChungTuThueKhac_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
        Dim f As New frmUpdateKetChuyenLaiLoThue
        f.Text = "Lập chứng từ kết chuyển lãi lỗ"
        f.ShowDialog()
        LoadDuLieu()


    End Sub

    Public Sub LoadDuLieu()
        Dim sql As String = "SELECT Id,NgayCT,SoCT,DienGiai,GhiSo,ThanhTien, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID = CHUNGTU.NguoiLap)NguoiLap "
        sql &= "FROM CHUNGTU WHERE Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "AND LoaiCT = @LoaiCT "
        sql &= "ORDER BY NgayCT DESC"

        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.KetChuyenLaiLo)
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

        sql = "SELECT convert(int,0)SoTT,Id,DienGiai,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("SoTT") = i + 1
        Next

        gdvChiTiet.DataSource = dt


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
            ComitTransaction()
            LoadDuLieu()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        End Try


    End Sub

End Class
