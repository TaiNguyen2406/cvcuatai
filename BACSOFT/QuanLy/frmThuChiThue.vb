Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Public Class frmThuChiThue

    Public LoaiCT As ChungTu.LoaiChungTu

    Private Sub frmThuChiThue_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim tg As DateTime = GetServerTime()
        txtDenNgay.EditValue = New DateTime(tg.Year, tg.Month, Date.DaysInMonth(tg.Year, tg.Month))
        txtTuNgay.EditValue = tg.AddDays(-30)

        LoadDuLieu()
    End Sub


    Private Sub txtDenNgayXK_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Try
            txtTuNgay.EditValue = Convert.ToDateTime(txtDenNgay.EditValue).AddDays(-30)
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadDuLieu()

        Dim sql As String = "SELECT a.Id,a.SoCT,a.NgayCT,a.TenKH,a.DienGiai,a.ThanhTien,b.TaiKhoanNo,b.TaiKhoanCo,b.GhiChuKhac SoPhieu,a.SoHD SoHD,a.GhiSo,a.LoaiCT,a.LoaiCT2 "
        sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) BETWEEN @TuNgay AND @DenNgay "

        Select Case LoaiCT
            Case ChungTu.LoaiChungTu.PhieuThuTienMat
                sql &= "AND b.TaiKhoanNo like '111%' "
            Case ChungTu.LoaiChungTu.PhieuChiTienMat
                sql &= "AND b.TaiKhoanCo like '111%' "
            Case ChungTu.LoaiChungTu.NopTienNganHang
                sql &= "AND b.TaiKhoanNo like '112%' "
            Case ChungTu.LoaiChungTu.UyNhiemChi
                sql &= "AND b.TaiKhoanCo like '112%' "
        End Select

        sql &= "ORDER BY a.NgayCT DESC, Id DESC "

        'AddParameter("@LoaiCT", LoaiCT)

        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)


        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        dt.Columns.Add("TenLoaiCT", Type.GetType("System.String"))
        dt.Columns.Add("SoCTx", Type.GetType("System.String"))
        For Each r As DataRow In dt.Rows
            r("TenLoaiCT") = ChungTu.TenLoaiCT(r("LoaiCT"), r("LoaiCT2"))
            r("SoCTx") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
        Next

        gdv.DataSource = dt


    End Sub


    Private Sub btnTaiHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiHoaDon.ItemClick
        LoadDuLieu()
    End Sub


    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        gdvData.OptionsView.ShowAutoFilterRow = Not gdvData.OptionsView.ShowAutoFilterRow
    End Sub

    Private Sub btnXoaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoaHoaDon.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Chứng từ đã ghi sổ không thể xóa!")
            Exit Sub
        End If

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
        '    ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
        '    Exit Sub
        'End If

        Dim isChungThuThuChi As Boolean
        Select Case gdvData.GetFocusedRowCellValue("LoaiCT")
            Case ChungTu.LoaiChungTu.NopTienNganHang, _
                ChungTu.LoaiChungTu.UyNhiemChi, _
                ChungTu.LoaiChungTu.PhieuChiTienMat, _
                ChungTu.LoaiChungTu.PhieuThuTienMat
                isChungThuThuChi = True
            Case Else
                isChungThuThuChi = False
        End Select


        If Not isChungThuThuChi Then
            ShowCanhBao("Vui lòng qua tác nghiệp " & gdvData.GetFocusedRowCellValue("TenLoaiCT") & " để thực hiện thao tác này!")
            Exit Sub
        End If


        If Not ShowCauHoi("Xóa chứng từ " & gdvData.GetFocusedRowCellValue("SoCT") & " hay không?") Then Exit Sub

        Try
            Dim idCT As Object = gdvData.GetFocusedRowCellValue("Id")
            Dim SoPhieu As Object = gdvData.GetFocusedRowCellValue("SoPhieu")
            AddParameterWhere("@Id", idCT)
            doDelete("CHUNGTU", "Id=@Id")
            AddParameter("@IdChungTu", DBNull.Value)
            AddParameterWhere("@SoPhieuT", SoPhieu)
            Select Case LoaiCT
                Case ChungTu.LoaiChungTu.PhieuThuTienMat
                    doUpdate("THU", "SoPhieuT=@SoPhieuT")
                Case ChungTu.LoaiChungTu.NopTienNganHang
                    doUpdate("THUNH", "SoPhieuT=@SoPhieuT")
            End Select

            LoadDuLieu()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception

            RollBackTransaction()
        End Try

    End Sub

    Private Sub btnThemHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemHoaDon.ItemClick
        TrangThai.isAddNew = True
        Dim f As New frmUpdateThuChiThue
        f.LoaiCT = LoaiCT
        f.ShowDialog()
        LoadDuLieu()
    End Sub

    Private Sub btnSuaHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSuaHoaDon.ItemClick

        TrangThai.isUpdate = True
        If gdvData.FocusedRowHandle < 0 Then Exit Sub


        Dim isChungThuThuChi As Boolean
        Select Case gdvData.GetFocusedRowCellValue("LoaiCT")
            Case ChungTu.LoaiChungTu.NopTienNganHang, _
                ChungTu.LoaiChungTu.UyNhiemChi, _
                ChungTu.LoaiChungTu.PhieuChiTienMat, _
                ChungTu.LoaiChungTu.PhieuThuTienMat
                isChungThuThuChi = True
            Case Else
                isChungThuThuChi = False
        End Select


        If Not isChungThuThuChi Then
            ShowCanhBao("Vui lòng qua tác nghiệp " & gdvData.GetFocusedRowCellValue("TenLoaiCT") & " để thực hiện thao tác này!")
            Exit Sub
        End If

        If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            ShowCanhBao("Hóa đơn đã ghi sổ không thể sửa!")
            Exit Sub
        End If

        Select Case gdvData.GetFocusedRowCellValue("LoaiCT")
            'Case ChungTu.LoaiChungTu.HoaDonDauRa, ChungTu.LoaiChungTu.HoaDonDauVao
            '    If gdvData.GetFocusedRowCellValue("GhiSo") = True Then
            '        ShowCanhBao("Hóa đơn đã ghi sổ không thể sửa!")
            '        Exit Sub
            '    End If

            '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            '        ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            '        Exit Sub
            '    End If

            '    Dim f As New frmUpdateHdDauVao
            '    f.isDangXuatKho = False
            '    f.LoaiCT2 = LoaiCT2
            '    f.Text = "Cập nhật hóa đơn (" & NguoiDung & ")"
            '    f.idHoaDon = gdvData.GetFocusedRowCellValue("Id")
            '    TrangThai.isUpdate = True

            '    f.ShowDialog()
        End Select

        Dim f As New frmUpdateThuChiThue
        f.LoaiCT = LoaiCT
        f.idChungTu = gdvData.GetFocusedRowCellValue("Id")
        f.ShowDialog()
        LoadDuLieu()


    End Sub

    Private Sub btnChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChep.ItemClick
        TrangThai.isCopy = True
        Dim f As New frmUpdateThuChiThue
        f.LoaiCT = LoaiCT
        f.idChungTu = gdvData.GetFocusedRowCellValue("Id")
        f.ShowDialog()
        LoadDuLieu()
    End Sub

    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown

        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)

        mSua.Enabled = calTest.InRowCell
        mXoa.Enabled = calTest.InRowCell
        mChep.Enabled = calTest.InRowCell

        If calTest.InRowCell Then
            If calTest.RowHandle >= 0 Then
                If gdvData.GetRowCellValue(calTest.RowHandle, "GhiSo") = True Then
                    mnuGhiSo2.Caption = "Bỏ sổ"
                    mnuGhiSo2.Glyph = My.Resources.Stop_16
                    mnuGhiSo2.Appearance.ForeColor = Color.Red
                Else
                    mnuGhiSo2.Caption = "Ghi sổ"
                    mnuGhiSo2.Glyph = My.Resources.Start_16
                    mnuGhiSo2.Appearance.ForeColor = Color.Blue
                End If
            End If
        End If

        pMnu.ShowPopup(gdv.PointToScreen(e.Location))

    End Sub

    Private Sub mThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThem.ItemClick
        btnThemHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChep.ItemClick
        btnChep_ItemClick(sender, e)
    End Sub

    Private Sub mSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSua.ItemClick
        btnSuaHoaDon_ItemClick(sender, e)
    End Sub

    Private Sub mXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoa.ItemClick
        btnXoaHoaDon_ItemClick(sender, e)
    End Sub


    Private Sub mnuGhiSo2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGhiSo2.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
        '    ShowCanhBao("Bạn không có quyền sửa hóa đơn!")
        '    Exit Sub
        'End If

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
        ShowAlert(str & " chứng từ " & gdvData.GetFocusedRowCellValue("SoCT") & " thành công!")

    End Sub


    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        Try
            If gdvData.GetRowCellValue(e.RowHandle, "GhiSo") = True And e.Column.FieldName <> "SoCT" And e.Column.FieldName <> "NgayCT" Then
                e.Appearance.ForeColor = Color.Green
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
