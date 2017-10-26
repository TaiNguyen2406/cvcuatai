Imports BACSOFT.Db.SqlHelper
Public Class frmDuyetEmail

    Private URL_FILE_MAYCHU As String = ServerName & "\Data$\EmailKinhDoanh\"

    Private Sub frmDuyetEmail_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        gdvData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Dim _ThoiGian As DateTime = GetServerTime()
        txtTuNgay.EditValue = New DateTime(_ThoiGian.Year, _ThoiGian.Month, 1)
        txtDenNgay.EditValue = _ThoiGian.Date
        rcmbNhanVien.DataSource = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 ")
        rcmbTrangThai.DataSource = ExecuteSQLDataTable("SELECT N'Đã duyệt' as HienThi, 1 as TrangThai UNION SELECT N'Không duyệt' as HienThi, 0 as TrangThai UNION SELECT N'Chưa duyệt' as HienThi, -1 as TrangThai")
        TaiDuLieu()


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btCauHinhEmailChinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btDsEmailGui.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

    End Sub

    Private Sub TaiDuLieu()

        Dim sql As String = "SET DATEFORMAT DMY "
        sql &= " SELECT EMAILKD.Id,EMAILKD.TieuDe,EMAILKD.NguoiGui as IdNguoiGui,NGUOIGUI.Ten AS NguoiGui,EMAILKD.NgayGui,EMAILKD.TrangThai,EMAILKD.GhiChu, (CASE WHEN EMAILKD.NgayDuyet IS NOT NULL THEN NGUOIDUYET.Ten END) AS NguoiDuyet,EMAILKD.NgayDuyet,(CASE ISNULL(TrangThaiGui,0) WHEN 0 THEN N'' ELSE N'Đang gửi' END)TrangThaiGui FROM EMAILKD "
        sql &= " INNER JOIN NHANSU AS NGUOIGUI ON NGUOIGUI.ID=EMAILKD.NguoiGui "
        sql &= " LEFT JOIN NHANSU AS NGUOIDUYET ON NGUOIDUYET.ID=EMAILKD.NguoiDuyet "
        sql &= " WHERE CONVERT(NVARCHAR,EMAILKD.NgayGui,103) >= @TuNgay AND CONVERT(NVARCHAR,EMAILKD.NgayGui,103) <= @DenNgay "

        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)

        If Not cmbNhanVien.EditValue Is Nothing Then
            sql &= " AND EMAILKD.NguoiGui = @NguoiGui "
            AddParameter("@NguoiGui", cmbNhanVien.EditValue)
        End If

        If Not cmbTrangThai.EditValue Is Nothing Then
            Select Case cmbTrangThai.EditValue.ToString
                Case ""
                Case "-1"
                    sql &= " AND EMAILKD.TrangThai is NULL "
                Case "0"
                    sql &= " AND EMAILKD.TrangThai = 0 "
                Case "1"
                    sql &= " AND EMAILKD.TrangThai = 1 "
            End Select
        End If

        sql &= "ORDER BY EMAILKD.NgayGui DESC "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
        gdv.DataSource = dt

    End Sub

    Private Sub btnTaiDanhSach_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDanhSach.ItemClick
        TaiDuLieu()
    End Sub

    Private Sub rcmbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbNhanVien.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbNhanVien.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub rcmbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbTrangThai.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbTrangThai.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThemMoi.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        ShowWaiting("Đang tải nội dung ...")
        Dim f As New frmSoanEmail
        f.Tag = Me.Parent.Tag
        If f.ShowDialog() = DialogResult.OK Then
            TaiDuLieu()
        End If
    End Sub

    Private Sub btChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChep.ItemClick, mSaoChep.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isCopy = True
        ShowWaiting("Đang tải nội dung ...")
        MaTuDien = gdvData.GetFocusedRowCellValue("Id")
        Dim f As New frmSoanEmail
        f.Tag = Me.Parent.Tag
        If f.ShowDialog() = DialogResult.OK Then
            TaiDuLieu()
        End If
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick, mXoaNoiDung.ItemClick

        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not ShowCauHoi("Xoá " & gdvData.GetFocusedRowCellValue("TieuDe") & " ?") Then Exit Sub
        Try
            AddParameter("@Id", gdvData.GetFocusedRowCellValue("Id"))
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT Files FROM EMAILKD  WHERE Id = @Id")
            If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Dim strFile() As String = dt.Rows(0)("Files").ToString.Split(New Char() {";"c})
            For Each f As String In strFile
                If f.Trim = "" Then Continue For
                If System.IO.File.Exists(URL_FILE_MAYCHU & f) Then System.IO.File.Delete(URL_FILE_MAYCHU & f)
            Next
            AddParameter("@Id", gdvData.GetFocusedRowCellValue("Id"))
            If doDelete("EMAILKD", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ShowAlert("Xoá nội dung thành công !")
            TaiDuLieu()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Private Sub btnLocDuLieu_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocDuLieu.CheckedChanged
        gdvData.OptionsView.ShowAutoFilterRow = btnLocDuLieu.Checked
    End Sub

    Private Sub btSuaNoiDung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaNoiDung.ItemClick, mSuaNoiDung.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub


        'If gdvData.GetFocusedRowCellValue("TrangThaiGui").ToString.Trim <> "" Then
        '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '        ShowCanhBao("Email đang trong quá trình gửi, bạn cần có quyền TP Kinh doanh hoặc admin để thực hiện thao tác này !")
        '        Exit Sub
        '    End If
        'End If

        TrangThai.isUpdate = True
        ShowWaiting("Đang tải nội dung ...")
        MaTuDien = gdvData.GetFocusedRowCellValue("Id")
        Dim f As New frmSoanEmail
        f.Tag = Me.Parent.Tag

        If gdvData.GetFocusedRowCellValue("IdNguoiGui").ToString.ToLower.Trim <> TaiKhoan.ToString.ToLower Then
            f.isNguoiSoanEmail = False
        Else
            f.isNguoiSoanEmail = True
        End If

        If f.ShowDialog() = DialogResult.OK Then
            TaiDuLieu()
        End If
    End Sub

    Private Sub gdv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvData.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub btCauHinhEmailChinh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCauHinhEmailChinh.ItemClick
        Dim f As New frmCauHinhEmailGui
        f.ShowDialog()
    End Sub

    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        txtTuNgay.EditValue = New DateTime(Convert.ToDateTime(txtDenNgay.EditValue).Year, Convert.ToDateTime(txtDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub btDsEmailGui_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDsEmailGui.ItemClick
        Dim f As New frmDsEmailGui
        f.ShowDialog()
    End Sub

    'Private Sub gdvData_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvData.RowCellStyle
    '    If e.RowHandle < 0 Then Exit Sub
    '    Try
    '        If gdvData.GetRowCellValue(e.RowHandle, "TrangThai") = False Then
    '            e.Appearance.Font = New Font(Me.Font, FontStyle.Italic Or FontStyle.Strikeout)
    '            'e.Appearance.BackColor = Color.LightGray
    '            'ElseIf gdvData.GetRowCellValue(e.RowHandle, "TrangThai") = True Then
    '            '    e.Appearance.BackColor = Color.LightBlue
    '            'ElseIf gdvData.GetRowCellValue(e.RowHandle, "TrangThai") Is DBNull.Value Then
    '            '    e.Appearance.BackColor = Color.LightGoldenrodYellow
    '        End If
    '    Catch ex As Exception
    '    End Try

    'End Sub


    Private Sub mnuXemNhatKyGui_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXemNhatKyGui.ItemClick
        btXemNhatKyGui_ItemClick(sender, e)
    End Sub




    Private Sub btXemNhatKyGui_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemNhatKyGui.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmLogEmailKD
        f._IdEmail = gdvData.GetFocusedRowCellValue("Id")
        f.ShowDialog()
    End Sub


End Class