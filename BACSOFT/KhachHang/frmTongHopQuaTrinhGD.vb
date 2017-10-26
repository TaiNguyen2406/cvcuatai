Imports BACSOFT.Db.SqlHelper

Public Class frmTongHopQuaTrinhGD

    Private Sub frmTongHopQuaTrinhGD_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        loadDS()

    End Sub

    Public Sub loadDS()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SELECT GIAODICHKH.ID,KHACHHANG.ttcMa,GIAODICHKH.ThoiGian,GIAODICHKH.NoiDungGiaoDich,"
        sql &= " NHANSU.Ten AS PhuTrach,"
        sql &= " tblTuDien.NoiDung AS HinhThuc"
        sql &= " FROM GIAODICHKH "
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=GIAODICHKH.IDTakeCare AND NHANSU.Noictac=74"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=GIAODICHKH.IDKH"
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=GIAODICHKH.HinhThuc AND tblTuDien.Loai=39"
        sql &= " WHERE GIAODICHKH.ChuyenGiao=0"
        If cbXemTheo.EditValue <> "Tất cả" Then
            sql &= " AND Convert(datetime,convert(nvarchar,GIAODICHKH.ThoiGian,103),103) BETWEEN @TuNgay AND @DenNgay"
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If
        sql &= " order by ThoiGian DESC"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        'Dim f As New frmCNNguoiGiaoDich
        'f.Tag = Me.Parent.Tag
        'f.cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        'f.cbNoiCongTac.EditValue = Convert.ToInt32(gdvCT.GetFocusedRowCellValue("Noictac"))
        'f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        'If gdvCT.GetFocusedRowCellValue("Noictac") = 74 Then
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        'End If
        'TrangThai.isUpdate = True
        'Dim Index As Integer = gdvCT.FocusedRowHandle
        'MaTuDien = gdvCT.GetFocusedRowCellValue("ID")
        'Dim f As New frmCNNguoiGiaoDich
        'f.Tag = Me.Parent.Tag
        'f.ShowDialog()
        'gdvCT.FocusedRowHandle = Index
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        loadDS()
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Tong hop giao dich KH.xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub


    Private Sub btXoaNgd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoaNgd.ItemClick, mXoa.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
       
    End Sub


    Private Sub gdvNgdCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        'Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        'HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        'If HitInfo.InColumnPanel Then Exit Sub

        'If e.Button = System.Windows.Forms.MouseButtons.Right Then
        '    PopupMenu1.ShowPopup(gdv.PointToScreen(e.Location))
        'End If
    End Sub

    Private Sub gdvNgdCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        'If e.Column.FieldName = "DoiTuongNhanEmail" Then
        '    AddParameter("@DoiTuongNhanEmail", gdvNgdCT.GetRowCellValue(e.RowHandle, "DoiTuongNhanEmail"))
        '    AddParameterWhere("@IDNV", gdvNgdCT.GetRowCellValue(e.RowHandle, "ID"))
        '    If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
        '        ShowBaoLoi(LoiNgoaiLe)
        '    Else
        '        ShowAlert("Đã cập nhật !")
        '    End If
        'End If
    End Sub

    Private Sub cbXemTheo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbXemTheo.EditValueChanged
        If cbXemTheo.EditValue = "Tất cả" Then
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        Else
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(tbDenNgay.EditValue)
        tbDenNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub
End Class
