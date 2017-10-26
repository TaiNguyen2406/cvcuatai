Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT

Public Class frmUpdateGhiTangTSCD

    Public idChungTu As Object


    Private Sub frmUpdateGhiTangTSCD_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        LoadDsPhongBan()
        cmbPhongBan.EditValue = DBNull.Value

        txtNgayVaoSo.EditValue = GetServerTime()

        If TrangThai.isAddNew Then
            LoadChiTietNoiDung(-1)
        Else
            Dim sql As String = "SELECT NgayCT,DienGiai,GhiSo,IdPhongBan FROM CHUNGTU WHERE Id = " & idChungTu
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            txtNgayVaoSo.EditValue = dt.Rows(0)("NgayCT")
            txtDienGiaiChung.EditValue = dt.Rows(0)("DienGiai")
            chkGhiSo.Checked = Convert.ToBoolean(dt.Rows(0)("GhiSo"))
            cmbPhongBan.SelectedItem = cmbPhongBan.Properties.Items.Cast(Of ObjectItemCmb).Where(Function(x) x.GiaTri = dt.Rows(0)("IdPhongBan")).FirstOrDefault
            LoadChiTietNoiDung(idChungTu)
        End If


        LoadDsTaiKhoan()
        txtDienGiaiChung.Focus()

    End Sub

    Public Sub LoadDsPhongBan()
        Dim sql As String = "SELECT Id, (MaPhongBan + ' - ' + TenPhongBan) PhongBan FROM PhongBanThue order by TenPhongBan"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For Each r As DataRow In tb.Rows
                cmbPhongBan.Properties.Items.Add(New ObjectItemCmb(r("Id"), r("PhongBan")))
            Next
        End If
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



    Public Sub LoadChiTietNoiDung(IdCT As Long)
        Dim sql As String = "SELECT convert(int,0)STT, Id,IdVatTu,IdChiTiet,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo,GiaTriKhac "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", IdCT)
        AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("STT") = i + 1
            Next
        End If
        gdv.DataSource = dt
    End Sub

    Dim idChungTuCCDC As Object

    Private Sub btnChungTuKhac_Click(sender As System.Object, e As System.EventArgs) Handles btnChungTuKhac.Click

        If gdvData.RowCount > 0 Then
            ShowCanhBao("Đã chọn hóa đơn tài sản cố định!")
            Exit Sub
        End If

        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Text = "Danh sách hóa đơn mua tài sản cố định"
        f.WindowState = FormWindowState.Maximized
        f.Tag = fMain.mnuThue_GhiTangCCDC.Name
        Dim c As New frmHoaDonDauVao
        c.isLayChungTuCCDC = True
        c.LoaiCT2 = ChungTu.LoaiCT2.MuaTaiSanCoDinh
        c.Dock = DockStyle.Fill
        f.Controls.Add(c)
        f.ShowDialog()

        idChungTuCCDC = c.IdHoaDonCCDC
        txtDienGiaiChung.Text = c.strNoiDungDienGiai

        Dim sql As String = ""
        sql = "SELECT convert(int,0)STT,Id,IdVatTu,DienGiai,DVT,SoLuong,DonGia,ThanhTien,GiaTriKhac FROM CHUNGTUCHITIET WHERE Id_CT = @IdCT And ButToan = @ButToan"
        AddParameter("@IdCT", idChungTuCCDC)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then Exit Sub

        Dim drx As DataTable = CType(gdv.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim r As DataRow = drx.NewRow
            r("STT") = i + 1
            r("IdVatTu") = dt.Rows(i)("IdVatTu")
            r("DienGiai") = dt.Rows(i)("DienGiai")
            r("GiaTriKhac") = 12
            r("DVT") = dt.Rows(i)("DVT")
            r("SoLuong") = dt.Rows(i)("SoLuong")
            r("DonGia") = dt.Rows(i)("DonGia")
            r("ThanhTien") = dt.Rows(i)("ThanhTien")
            r("IdChiTiet") = dt.Rows(i)("Id")
            drx.Rows.InsertAt(r, drx.Rows.Count)
        Next


    End Sub

    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        If gdvData.RowCount <= 0 Then
            ShowCanhBao("Chưa có nội dung tài sản cố định!")
            Exit Sub
        End If

        If cmbPhongBan.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn phòng ban!")
            Exit Sub
        End If

        If chkGhiSo.Checked Then
            For i As Integer = 0 To gdvData.RowCount - 1
                If gdvData.GetRowCellValue(i, "TaiKhoanNo") Is DBNull.Value Or _
                    gdvData.GetRowCellValue(i, "TaiKhoanCo") Is DBNull.Value Then
                    ShowCanhBao("Chưa đầy đủ bút toán nên không thể ghi sổ chứng từ này được!")
                    Exit Sub
                End If
            Next
        End If

        If TrangThai.isAddNew Then
            If Not ShowCauHoi("Bạn có chắc chắn với thao tác này không?") Then Exit Sub
        End If

        Try
            AddParameter("@NgayCT", txtNgayVaoSo.EditValue)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@GhiSo", Convert.ToByte(chkGhiSo.Checked))
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.GhiTangTSCD)
            AddParameter("@IdPhongBan", CType(cmbPhongBan.EditValue, ObjectItemCmb).GiaTri)
            Dim tongthanhtien As Double = 0
            For i As Integer = 0 To gdvData.RowCount - 1
                tongthanhtien += gdvData.GetRowCellValue(i, "ThanhTien")
            Next
            AddParameter("@ThanhTien", tongthanhtien)
            AddParameter("@NguoiLap", TaiKhoan)

            If TrangThai.isAddNew Then
                idChungTu = doInsert("CHUNGTU")
                If idChungTu Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@dk_Id", idChungTu)
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If


            If TrangThai.isAddNew Then
                AddParameter("@refid", idChungTu)
                AddParameterWhere("@dk_Id", idChungTuCCDC)
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            For i As Integer = 0 To gdvData.RowCount - 1
                AddParameter("@Id_CT", idChungTu)
                AddParameter("@IdVatTu", gdvData.GetRowCellValue(i, "IdVatTu"))
                AddParameter("@DienGiai", gdvData.GetRowCellValue(i, "DienGiai"))
                AddParameter("@DVT", gdvData.GetRowCellValue(i, "DVT"))
                AddParameter("@SoLuong", gdvData.GetRowCellValue(i, "SoLuong"))
                AddParameter("@DonGia", gdvData.GetRowCellValue(i, "DonGia"))
                AddParameter("@ThanhTien", gdvData.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@ThanhTienQD", gdvData.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@GhiChu", gdvData.GetRowCellValue(i, "GhiChu"))
                AddParameter("@TaiKhoanNo", gdvData.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvData.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@GiaTriKhac", gdvData.GetRowCellValue(i, "GiaTriKhac"))
                AddParameter("@GiaTriKhac2", 0)
                AddParameter("@IdChiTiet", gdvData.GetRowCellValue(i, "IdChiTiet"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
                If TrangThai.isAddNew Then
                    Dim idchitiet As Object = doInsert("CHUNGTUCHITIET")
                    If idchitiet Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvData.SetRowCellValue(i, "Id", idchitiet)
                Else
                    AddParameterWhere("@dk_Id", gdvData.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            If TrangThai.isAddNew Then
                TrangThai.isUpdate = True
                Me.Text = "Cập nhật chứng từ ghi tăng TSCĐ"
                ShowAlert("Đã thêm mới chứng từ ghi tăng TSCĐ thành công")
            Else
                ShowAlert("Đã cập nhật chứng từ ghi tăng TSCĐ thành công")
            End If

            btnThemMoi.Enabled = True

        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub


    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub


    Private Sub btnThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btnThemMoi.Click
        TrangThai.isAddNew = True
        txtNgayVaoSo.EditValue = GetServerTime()
        txtDienGiaiChung.Text = ""
        LoadChiTietNoiDung(-1)
        btnThemMoi.Enabled = False
        Me.Text = "Thêm mới chứng từ ghi tăng TSCĐ"
    End Sub

End Class