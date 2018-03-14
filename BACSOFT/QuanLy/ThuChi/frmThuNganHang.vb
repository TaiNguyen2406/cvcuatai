Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Imports System.Linq
Imports BACSOFT.HoaDonGTGT


Public Class frmThuNganHang
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0

    Private Sub frmThuNganHang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()
        btTaiLai.PerformClick()
    End Sub

    Public Sub LoadTuDien()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        sql &= " SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa "
        sql &= " SELECT rtrim(lTrim(MaSo))MaSo,Ten FROM TAIKHOAN "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
            rcbSoTK.DataSource = ds.Tables(2)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub



    Public Sub LoadThuNH()



        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT convert(bit,0)chon, THUNH.SoPhieuT, 0 AS STT, THUNH.ID,NgayThangVS,(N'CK ' + THUNH.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,THUNH.DienGiai,THUNH.TyGia,"
        sql &= " 	THUNH.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,TaiKhoanDen AS TaiKhoan,"
        sql &= " (CASE PhieuTC0 WHEN N'000000000' THEN N'' ELSE case when THUNH.MucDich = 109 then N'ĐH ' else N'CG ' end + PhieuTC0 END) PhieuTC0, "
        sql &= " (CASE PhieuTC1 WHEN N'000000000' THEN N'' ELSE case when THUNH.MucDich = 109 then N'NK ' else N'XK ' end + PhieuTC1 END) PhieuTC1, "
        sql &= " (SELECT SoCT FROM CHUNGTU WHERE ID = THUNH.IdChungTu)PhieuNopTien,NHANSU.Ten as NguoiLap"
        sql &= " FROM THUNH"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=THUNH.IDKh"
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=THUNH.IDUser "
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=THUNH.TienTe"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=THUNH.MucDich"
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,THUNH.NgayThangCT,103),103) BETWEEN @TuNgay AND @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND THUNH.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND rtrim(ltrim(THUNH.TaiKhoanDen))='" & cbSoTK.EditValue & "'"
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next

            gdvThuNH.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        LoadThuNH()

    End Sub



    'Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
    '    If chkRutGon.Checked = True Then
    '        chkRutGon.Glyph = My.Resources.Checked
    '    Else
    '        chkRutGon.Glyph = My.Resources.UnCheck
    '    End If
    'End Sub

    'Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
    '    If isShowing Then
    '        ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
    '        Exit Sub
    '    End If

    '    If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

    '    TrangThai.isUpdate = True
    '    fCNChaoGia = New frmCNChaoGia
    '    'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
    '    fCNChaoGia.SPChaoGia = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieuCG")
    '    fCNChaoGia.Tag = Me.Parent.Tag
    '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
    '        fCNChaoGia.gdvVTCT.OptionsBehavior.ReadOnly = True
    '        fCNChaoGia.gdvCongTrinhCT.OptionsBehavior.ReadOnly = True
    '        fCNChaoGia.btGhi.Enabled = False
    '        fCNChaoGia.tabChuyenMa.PageVisible = False
    '        fCNChaoGia.btTinhToan.Enabled = False
    '    End If

    '    fCNChaoGia.Show()
    'End Sub

    'Private Sub mXemXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemXuatKho.ItemClick
    '    If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

    '    TrangThai.isUpdate = True
    '    fCNXuatKho = New frmCNXuatKho
    '    fCNXuatKho.PhieuXK = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieu2")
    '    fCNXuatKho.Tag = Me.Parent.Tag
    '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
    '        fCNXuatKho.btCal.Enabled = False
    '        fCNXuatKho.btGhi.Enabled = False
    '        fCNXuatKho.btChuyenXK.Enabled = False
    '        fCNXuatKho.mXuatKho.Enabled = False
    '    End If
    '    fCNXuatKho.ShowDialog()
    'End Sub

  

    Private Sub mThemPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemPhieuThu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        ' TrangThai.isAddNew = True
        Dim f As New frmCNThu2
        f._TrangThai.isAddNew = True
        f.Tag = Me.Parent.Tag
        f.ThuNH = True
        f.Text = "Thêm phiếu thu vào tài khoản"
        f.Show()
    End Sub

    Private Sub mSuaPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaPhieuThu.ItemClick

        If gdvThuNHCT.FocusedRowHandle < 0 Then Exit Sub
        'TrangThai.isUpdate = True
        Dim _Index As Integer = gdvThuNHCT.FocusedRowHandle
        Dim f As New frmCNThu2
        f.Tag = Me.Parent.Tag
        f._TrangThai.isUpdate = True
        f.Text = "Cập nhật phiếu thu NH " & gdvThuNHCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        f.ThuNH = True
        f.PhieuThu = gdvThuNHCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        f.PhieuThuCT = gdvThuNHCT.GetFocusedRowCellValue("SoPhieu").ToString
        f.ThuNH = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            f.btGhi.Enabled = False
            f.btThem.Enabled = False
        End If
        f.Show()

        gdvThuNHCT.FocusedRowHandle = _Index
    End Sub

    

    Private Sub rcbSoTK_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbSoTK.ButtonClick
        If e.Button.Index = 1 Then
            cbSoTK.EditValue = Nothing
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub btNhapChiPhiTuExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim f As New frmNhapChiPhi
        f.chkUNC.Checked = True
        f.ShowDialog()
    End Sub

    Private Sub pMenuThu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuThu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvThuNHCT.CalcHitInfo(gdvThuNH.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvChuyenMaCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvThuNHCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuThu.ShowPopup(gdvThuNH.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvThuNHCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvThuNHCT.RowCellClick
        If e.Column.FieldName = "chon" Then
            gdvThuNHCT.SetRowCellValue(e.RowHandle, "chon", Not gdvThuNHCT.GetRowCellValue(e.RowHandle, "chon"))
        End If
    End Sub

    Private Sub mnuChuyenSangBenThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenSangBenThue.ItemClick

        If gdvThuNHCT.FocusedRowHandle < 0 Then Exit Sub

        Dim f As New frmCapNhatThuChiNganHangThue
        TrangThai.isAddNew = True
        f.LoaiCT = ChungTu.LoaiChungTu.NopTienNganHang
        f.Text = "Thêm mới chứng từ nộp tiền ngân hàng"
        f.idChungTu = gdvThuNHCT.GetFocusedRowCellValue("ID")

        Dim sql As String = "SELECT * FROM THUNH WHERE ID = " & f.idChungTu
        Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)



        Dim dt1 As DataTable = CType(f.cmbSoTK.Properties.DataSource, DataTable)
        Dim dr1 As DataRow = dt1.NewRow
        dr1("MaSo") = r("TaiKhoanden").ToString.Trim
        dr1("Ten") = r("Nganhangden").ToString.Trim
        dr1("ID") = DBNull.Value
        dt1.Rows.InsertAt(dr1, dt1.Rows.Count)

        Dim dt2 As DataTable = CType(f.cmbSoTaiKhoanDoiUng.Properties.DataSource, DataTable)
        Dim dr2 As DataRow = dt2.NewRow
        dr2("MaSo") = r("Taikhoandi").ToString.Trim
        dr2("Ten") = r("Nganhangdi").ToString.Trim
        dr2("ID") = DBNull.Value
        dt2.Rows.InsertAt(dr2, dt2.Rows.Count)

        f.txtNgayCT.EditValue = r("ngaythangCT")
        f.cmbTienTe.SelectedItem = f.cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.ID = r("TienTe")).FirstOrDefault
        f.cmbDoiTuong.EditValue = r("IDKh")
        f.txtNoiDung.EditValue = f.txtTenDoiTuong.Text & " thanh toán tiền hàng"

        f.cmbSoTK.EditValue = r("taikhoanden").ToString.Trim
        f.txtTenTaiKhoan.EditValue = r("nganhangden").ToString.Trim

        f.cmbSoTaiKhoanDoiUng.EditValue = r("taikhoandi").ToString.Trim
        f.txtTenTaiKhoanDoiUng.EditValue = r("nganhangdi").ToString.Trim
        'f.txtDaiDien.EditValue = r("")
        f.gdvData.AddNewRow()

        f.gdvData.SetFocusedRowCellValue("ThanhTien", r("sotien"))

        sql = "SELECT SUM(sotien) FROM THUNH WHERE SoPhieuT = @SoPhieuT"
        AddParameter("@SoPhieuT", r("SoPhieuT"))
        Dim tt As Object = ExecuteSQLDataTable(sql).Rows(0)(0)
        If tt > r("sotien") Then
            If ShowCauHoi("Chứng từ này gồm nhiều phiếu có tổng giá trị " & tt & ", chọn số tiền này thay cho " & r("sotien") & "?") Then
                f.gdvData.SetFocusedRowCellValue("ThanhTien", tt)
            End If
        End If




        f.gdvData.SetFocusedRowCellValue("DienGiai", f.txtTenDoiTuong.Text & " thanh toán tiền hàng")
        f.gdvData.SetFocusedRowCellValue("TaiKhoanNo", "1121")
        f.gdvData.SetFocusedRowCellValue("TaiKhoanCo", "131")
        f.gdvData.Focus()
        f.gdvData.FocusedColumn = f.gdvData.Columns("DienGiai")
        f.gdvData.ShowEditor()


        f.ShowDialog()


        If Not f.idChungTu Is Nothing Then
            gdvThuNHCT.SetFocusedRowCellValue("SoCT", f.tbSoPhieu.EditValue)
            gdvThuNHCT.SetFocusedRowCellValue("IdCT", f.idHoaDon)
        End If

    End Sub

    Private Sub mnuChuyenCacPhieuDaChon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenCacPhieuDaChon.ItemClick
        gdvThuNHCT.CloseEditor()
        gdvThuNHCT.UpdateCurrentRow()
        Dim arrPhieu As New List(Of Integer)
        For i As Integer = 0 To gdvThuNHCT.RowCount - 1
            If gdvThuNHCT.GetRowCellValue(i, "chon") = True Then
                arrPhieu.Add(i)
            End If
        Next
        If arrPhieu.Count = 0 Then
            ShowCanhBao("Chưa chọn phiếu nào cả!")
            Exit Sub
        End If
        If Not ShowCauHoi("Chuyển " & arrPhieu.Count & " phiếu đã chọn sang bên thuế ?") Then Exit Sub

        Try
            For i As Integer = 0 To arrPhieu.Count - 1


                Dim sql As String = "SELECT * FROM THUNH WHERE ID = " & gdvThuNHCT.GetRowCellValue(arrPhieu(i), "ID")
                Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG WHERE ID = @ID"
                AddParameter("@ID", r("IDKh"))
                Dim rKH As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ISNULL((SELECT SUM(sotien) FROM THUNH WHERE SoPhieuT =  @SoPhieuT),0)"
                AddParameter("@SoPhieuT", r("SoPhieuT"))
                Dim tt As Object = ExecuteSQLDataTable(sql).Rows(0)(0)
                If tt <= r("sotien") Then tt = r("sotien")


                'chung tu
                AddParameter("@LoaiCT", ChungTu.LoaiChungTu.NopTienNganHang)
                AddParameter("@NgayCT", r("ngaythangCT"))
                AddParameter("@TienTe", 0)
                AddParameter("@TyGia", 1)
                AddParameter("@GhiSo", 0)
                AddParameter("@IdKH", r("IDKh"))
                AddParameter("@TenKH", rKH("Ten"))
                AddParameter("@DiaChi", rKH("ttcDiachi"))
                AddParameter("@MaSoThue", rKH("ttcMasothue"))
                AddParameter("@NguoiLienHe", DBNull.Value)
                AddParameter("@DienGiai", rKH("Ten") & " thanh toán tiền hàng")
                AddParameter("@SoTkNganHang", r("taikhoanden"))
                AddParameter("@TenTkNganHang", r("nganhangden"))
                AddParameter("@SoTkNganHangDoiUng", r("taikhoandi"))
                AddParameter("@TenTkNganHangDoiUng", r("nganhangdi"))
                AddParameter("@ThanhTien", tt)

                AddParameter("@refId", gdvThuNHCT.GetRowCellValue(arrPhieu(i), "ID"))
                Dim idHoaDon As Object
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Hàng tiền
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@DienGiai", rKH("Ten") & " thanh toán tiền hàng")
                AddParameter("@ThanhTien", tt)
                AddParameter("@TaiKhoanNo", "1121")
                AddParameter("@TaiKhoanCo", "131")
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

                Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)

                gdvThuNHCT.SetRowCellValue(arrPhieu(i), "IdCT", idHoaDon)

            Next
            gdvThuNHCT.BeginUpdate()
            For i As Integer = 0 To gdvThuNHCT.RowCount - 1
                If gdvThuNHCT.GetRowCellValue(i, "chon") = True Then
                    gdvThuNHCT.SetRowCellValue(i, "chon", False)
                End If
            Next
            gdvThuNHCT.EndUpdate()
            mnuChonBoChonTatCa.Tag = False
            ShowAlert("Đã chuyển thành công " & arrPhieu.Count & " phiếu sang bên thuế!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadThuNH()
        End Try
    End Sub

    Private Sub mnuChonBoChonTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChonBoChonTatCa.ItemClick
        Dim arrPhieu As New List(Of String)
        If mnuChonBoChonTatCa.Tag Then
            For i As Integer = 0 To gdvThuNHCT.RowCount - 1
                If arrPhieu.IndexOf(gdvThuNHCT.GetRowCellValue(i, "SoPhieuT")) >= 0 Then
                    gdvThuNHCT.SetRowCellValue(i, "chon", False)
                Else
                    arrPhieu.Add(gdvThuNHCT.GetRowCellValue(i, "SoPhieuT"))
                    gdvThuNHCT.SetRowCellValue(i, "chon", True)
                End If
            Next
        Else
            For i As Integer = 0 To gdvThuNHCT.RowCount - 1
                gdvThuNHCT.SetRowCellValue(i, "chon", False)
            Next
        End If
        mnuChonBoChonTatCa.Tag = Not mnuChonBoChonTatCa.Tag
    End Sub


End Class
