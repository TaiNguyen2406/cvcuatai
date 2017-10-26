Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors

Public Class frmNhapThueHangDaNhap
    Public _exit As Boolean = False
    Public index As Integer

    Private Sub frmNhapKho_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        ' tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month - 1, 1)
        'tbDenNgay.Enabled = False
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        ' LoadcbChiPhi()
    End Sub

#Region "Load DS nhập kho"

    Private sqlWhere As String = ""

    Public Sub LoadDS()

        ShowWaiting("Đang tải danh sách hàng nhập kho ...")

        Dim sql As String = " SET DATEFORMAT DMY"
        sql &= " SELECT NHAPKHO.ID,KHACHHANG.ttcMa,PHIEUNHAPKHO.IDKhachHang, NHAPKHO.IDVattu, row_number() over (order by NHAPKHO.SoPhieu, NHAPKHO.ID) AS AZ,NHAPKHO.SoPhieu, NHAPKHO.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,DonGia,NhapThue,MucThue,SoHDGTGT,NgayHDGTGT,SoHoaDon, convert(bit,0) as Chon "
        sql &= " FROM NHAPKHO "
        sql &= " INNER JOIN VATTU ON NHAPKHO.IDVatTu=VATTU.ID"
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu AND Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103)>='01/01/2016'"
        sql &= " AND (Convert(datetime,convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay)"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUNHAPKHO.IDKhachHang"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE SoHoaDon is null ORDER BY NHAPKHO.SoPhieu, NHAPKHO.ID "
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvVT.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


        '    AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        '    AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        '    sql &= " AND Convert(datetime,CONVERT(nvarchar,NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "

        'If Not btNhanVien.EditValue Is Nothing Then
        '    sql &= " AND PHIEUNHAPKHO.IDUser= " & btNhanVien.EditValue
        'End If

        'If Not cbKH.EditValue Is Nothing Then
        '    sql &= " AND PHIEUNHAPKHO.IDKhachhang= " & cbKH.EditValue
        'End If

        'If sqlWhere <> "" Then
        '    sql &= sqlWhere
        'End If

        'sql &= " ORDER BY PHIEUNHAPKHO.SoPhieu DESC"

        'Dim dt As DataTable = ExecuteSQLDataTable(sql)


        ' gdvVT.DataSource = Nothing


        CloseWaiting()

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)


    End Sub

    Private Sub LoadrCbNhanVien()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not dt Is Nothing Then
            rCbNhanVien.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadrCbKH()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG order by ttcMa")
        If Not dt Is Nothing Then
            rcbKH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub



#End Region

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mpThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        fCNNhapKho = New frmCNNhapKho
        fCNNhapKho.Tag = Me.Parent.Tag
        fCNNhapKho.ShowDialog()
    End Sub

    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            btNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKH.EditValue = Nothing
        End If
    End Sub

    Private Sub mXemTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemTatCa.ItemClick
        gdvVTCT.Columns("SoPhieu").ClearFilter()
    End Sub


    Private fUpdateHdDauVao As frmUpdateHdDauVao

    Private Sub mnuNhapHoaDonDauVao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNhapHoaDonDauVao.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", fMain.mnuThueMuaHangTrongNuoc.Name, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        'Dim f As New frmUpdateHoaDon
        'f.ShowDialog()

        gdvVTCT.Columns("Chon").Visible = True

        TrangThai.isAddNew = True

        fUpdateHdDauVao = New frmUpdateHdDauVao
        fUpdateHdDauVao.LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc

        fUpdateHdDauVao.Text = "Nhập hóa đơn mới (" & NguoiDung & ")"
        fUpdateHdDauVao.txtNguoiLienHe.Focus()
        fUpdateHdDauVao.isDangXuatKho = True


        'Bin du lieu chung len hoa don
        ' fUpdateHdDauVao.cmbDoiTuong.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachHang").ToString

        mThem.Enabled = False
        mSua.Enabled = False
        mnuNhapHoaDonDauVao.Enabled = False

    End Sub



    Private Sub btnXemHoaDon_Click(sender As System.Object, e As System.EventArgs)
        fUpdateHdDauVao.Show()
    End Sub

    Private Sub btnHuyThaoTacHoaDon_Click(sender As System.Object, e As System.EventArgs)
        mThem.Enabled = True
        mSua.Enabled = True
        mnuNhapHoaDonDauVao.Enabled = True
        fUpdateHdDauVao = Nothing
        gdvVTCT.Columns("Chon").Visible = False
    End Sub


    'Hiển thị menu đưa nội dung vào hóa đơn
    Private calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    Private Sub gdvVTCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvVTCT.MouseDown
        On Error Resume Next
        calHitTestHoaDon = gdvVTCT.CalcHitInfo(e.Location)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If calHitTestHoaDon.InRowCell Then
                pMenuHoaDon.ShowPopup(gdvVT.PointToScreen(e.Location))
            End If
        End If
    End Sub

    Public _SoLuong As Integer = 0
    Public _SoTien1 As Integer = 0
    Public _SoTien2 As Integer = 0

    Private Sub gdvVTCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvVTCT.RowCellClick
        If e.RowHandle < 0 Then Exit Sub

        If e.Column.FieldName = "Chon" Then
            'gdvVTCT.CloseEditor()
            ' gdvVTCT.UpdateCurrentRow()
            'Dim st As Boolean = Convert.ToBoolean(gdvVTCT.GetFocusedRowCellValue("Chon"))
            'gdvVTCT.SetFocusedRowCellValue("Chon", Not st)
            gdvVTCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
            gdvVTCT.FocusedColumn = gdvVTCT.Columns(0)
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()

            If gdvVTCT.GetFocusedRowCellValue("Chon") Then
                _SoTien2 += gdvVTCT.GetFocusedRowCellValue("DonGia") * gdvVTCT.GetFocusedRowCellValue("SoLuong")
                _SoLuong += 1
            Else
                If _SoTien2 > 0 Then
                    _SoTien2 -= gdvVTCT.GetFocusedRowCellValue("DonGia") * gdvVTCT.GetFocusedRowCellValue("SoLuong")
                End If
                _SoLuong -= 1
            End If
        End If

    End Sub

    Private Sub gdvVTCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvVTCT.CustomSummaryCalculate
        On Error Resume Next
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "Chon" Then
                e.TotalValue = _SoLuong
            End If
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "Model" Then
                e.TotalValue = _SoTien2
            End If
        End If
    End Sub


    Private Sub DuaVaoHoaDon(indexRow As Integer)

        'Dim idVatTu = gdvVTCT.GetRowCellValue(indexRow, "TenVT")
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", fMain.mnuThueMuaHangTrongNuoc.Name, DanhMucQuyen.QuyenThem) Then
        '    ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
        '    Exit Sub
        'End If

        'Dim f As New frmUpdateHoaDon
        'f.ShowDialog()

        ' gdvVTCT.Columns("Chon").Visible = True



        Dim ref As String = ChungTu.getRef
        Dim diengiai As String

        'Thêm dòng cho hàng tiền
        With fUpdateHdDauVao.gdvHangTienCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDVattu"))
            diengiai = gdvVTCT.GetRowCellValue(indexRow, "TenVT") & " " & gdvVTCT.GetRowCellValue(indexRow, "Model")
            .SetFocusedRowCellValue("DienGiai", diengiai)
            .SetFocusedRowCellValue("DVT", gdvVTCT.GetRowCellValue(indexRow, "TenDVT"))

            .SetFocusedRowCellValue("SoLuong", gdvVTCT.GetRowCellValue(indexRow, "SoLuong"))
            .SetFocusedRowCellValue("DonGia", gdvVTCT.GetRowCellValue(indexRow, "DonGia"))
            .SetFocusedRowCellValue("TaiKhoanCo", "331")

            If gdvVTCT.GetRowCellValue(indexRow, "IDvattu") Is DBNull.Value Then
                .SetFocusedRowCellValue("TaiKhoanNo", "")
            Else
                .SetFocusedRowCellValue("TaiKhoanNo", "1561")
            End If

            .SetFocusedRowCellValue("IdChiTiet", gdvVTCT.GetRowCellValue(indexRow, "ID"))


        End With

        'Thêm dòng cho thuế
        With fUpdateHdDauVao.gdvThueCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDVattu"))
            .SetFocusedRowCellValue("DienGiai", diengiai)
            .SetFocusedRowCellValue("TaiKhoanNo", "1331")
            .SetFocusedRowCellValue("TaiKhoanCo", "331")
            .SetFocusedRowCellValue("IdChiTiet", gdvVTCT.GetRowCellValue(indexRow, "ID"))

            Dim thanhtien = Math.Round(gdvVTCT.GetRowCellValue(indexRow, "SoLuong") * gdvVTCT.GetRowCellValue(indexRow, "DonGia"), 2, MidpointRounding.AwayFromZero)
            Dim tienthue = Math.Round((thanhtien * 10) / 100, 2, MidpointRounding.AwayFromZero)
            .SetFocusedRowCellValue("ThanhTien", tienthue)
        End With

    End Sub

    Private Sub mnuDuaVaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoHD.ItemClick

        Dim indexRow = calHitTestHoaDon.RowHandle
        If gdvVTCT.GetRowCellValue(indexRow, "SoHoaDon").ToString <> "" Then
            ShowCanhBao("Vật tư này đã lập hóa đơn rồi!")
            Exit Sub
        End If
        CType(gdvVT.DataSource, DataTable).Rows(indexRow)("NhapThue") = True
        DuaVaoHoaDon(indexRow)
        ShowAlert("Thêm vật tư " & gdvVTCT.GetRowCellValue(indexRow, "TenVT") & gdvVTCT.GetRowCellValue(indexRow, "Model") & " vào hóa đơn")
    End Sub

    Private Sub mnuDuaVaoHetHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoHetHD.ItemClick
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "SoHoaDon").ToString <> "" Then Continue For
            DuaVaoHoaDon(i)
        Next
        fUpdateHdDauVao.Show()
        ShowAlert("Đã thêm tất cả vào hóa đơn")
    End Sub

    Private Sub mnuDuaMucDaChonVaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaMucDaChonVaoHD.ItemClick
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        Dim _IDKhachHang As Integer = 0
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If Convert.ToBoolean(gdvVTCT.GetRowCellValue(i, "Chon")) = True Then
                If _IDKhachHang = 0 Then
                    _IDKhachHang = gdvVTCT.GetRowCellValue(i, "IDKhachHang")
                Else
                    If _IDKhachHang <> gdvVTCT.GetRowCellValue(i, "IDKhachHang") Then
                        ShowCanhBao("Chỉ áp dụng đối với cùng 1 nhà cung cấp !")
                        Exit Sub
                    End If
                End If

            End If
        Next
        If _IDKhachHang = 0 Then
            ShowCanhBao("Chưa chọn vật tư nào !")
            Exit Sub
        End If

        TrangThai.isAddNew = True

        fUpdateHdDauVao = New frmUpdateHdDauVao
        fUpdateHdDauVao.LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc
        fUpdateHdDauVao.Tag = "NK"
        fUpdateHdDauVao.Text = "Nhập hóa đơn mới (" & NguoiDung & ")"
        fUpdateHdDauVao.txtNguoiLienHe.Focus()
        fUpdateHdDauVao.isDangXuatKho = True
        fUpdateHdDauVao.cmbDoiTuong.EditValue = _IDKhachHang
        fUpdateHdDauVao.cmbTienTe.SelectedIndex = 0

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If Convert.ToBoolean(gdvVTCT.GetRowCellValue(i, "Chon")) = True And gdvVTCT.GetRowCellValue(i, "SoHoaDon").ToString = "" Then
                DuaVaoHoaDon(i)
            End If
        Next
        fUpdateHdDauVao.ShowDialog()
        ' ShowAlert("Đã đưa các mục được chọn tất cả vào hóa đơn")
    End Sub


    Private Sub mnuLocPhieuNhapKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLocPhieuNhapKho.ItemClick
        If mnuLocPhieuNhapKho.Appearance.ForeColor = Color.Red Then
            If ShowCauHoi("Hủy bỏ trạng thái đang lọc dữ liệu ?") Then
                mnuLocPhieuNhapKho.Appearance.ForeColor = mnuNhapHoaDonDauVao.Appearance.ForeColor
                mnuLocPhieuNhapKho.Appearance.Font = New Font(Me.Font, FontStyle.Regular)
                mnuLocPhieuNhapKho.Glyph = My.Resources.UnCheck
                sqlWhere = ""
                ' cbTieuChi.EditValue = "Top 5000"
                LoadDS()
            End If
        Else
            Dim f As New frmTimKiemNhapXuatKho
            f.Text = "Lọc phiếu nhập kho theo model"
            f.txtNoiDung.Enabled = False
            If f.ShowDialog() = DialogResult.OK Then
                mnuLocPhieuNhapKho.Appearance.ForeColor = Color.Red
                mnuLocPhieuNhapKho.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
                mnuLocPhieuNhapKho.Glyph = My.Resources.Checked
                sqlWhere = f.sqlWhere
                'cbTieuChi.EditValue = "Tuỳ chỉnh"
                tbTuNgay.EditValue = f.txtTuNgay.EditValue
                tbDenNgay.EditValue = f.txtDenNgay.EditValue
                LoadDS()
            End If
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As Object, e As EventArgs) Handles tbDenNgay.EditValueChanged
        tbTuNgay.EditValue = DateAdd(DateInterval.Month, -1, Convert.ToDateTime(tbDenNgay.EditValue))
    End Sub

    Private Sub gdvVTCT_KeyDown(sender As Object, e As KeyEventArgs) Handles gdvVTCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvVTCT.OptionsView.ShowAutoFilterRow = Not gdvVTCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub
End Class
