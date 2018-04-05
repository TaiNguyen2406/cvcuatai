Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports System.IO

Public Class frmThuTienMat
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0
    'Duong
    Private _Hsql = ""
    Private Sub frmThuTienMat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()
        btTaiLai.PerformClick()
    End Sub

#Region "Load dữ liệu phụ"

    Public Sub LoadTuDien()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        sql &= " SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa "
        sql &= " SELECT ltrim(rtrim(MaSo))MaSo,Ten FROM TAIKHOAN "
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


#End Region

    Public Sub LoadThu()
        If Me.Parent.Name = "frmLichSuPhieu" Then
            LoadLog()
            Exit Sub
        End If
        BanSTTLichSu.Visible = False
        BandLichSuSuaPhieu.Visible = False
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT convert(bit,0)chon,THU.SoPhieuT, 0 AS STT, THU.ID,NgayThangVS,(N'TT ' + THU.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,THU.DienGiai,THU.TyGia,"
        sql &= " 	THU.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,NguoiNop,"
        sql &= " (CASE PhieuTC0 WHEN N'000000000' THEN N'' ELSE case when Thu.MucDich = 109 then N'ĐH ' else N'CG ' end + PhieuTC0 END) PhieuTC0, "
        sql &= " (CASE PhieuTC1 WHEN N'000000000' THEN N'' ELSE case when Thu.MucDich = 109 then N'NK ' else N'XK ' end + PhieuTC1 END) PhieuTC1, "
        sql &= " (SELECT SoCT FROM CHUNGTU WHERE ID = THU.IdChungTu)SoPhieuThu,NHANSU.Ten as NguoiLap "
        sql &= " , DeNghiSua"
        sql &= " FROM THU"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=THU.IDKh"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=THU.TienTe"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=THU.MucDich"
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=THU.IDUser"
        sql &= " WHERE "
        _Hsql = sql
        sql &= " Convert(datetime,CONVERT(nvarchar,THU.NgayThangCT,103),103) BETWEEN @TuNgay AND @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND THU.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND ltrim(rtrim(THU.MaTK))='" & cbSoTK.EditValue & "' "
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next

            gdvThu.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadThu()

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
        f._frmThuTienMat = Me
        f.Text = "Thêm phiếu thu"
        f.Show()
    End Sub

    Private Sub mSuaPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaPhieuThu.ItemClick

        If gdvThuCT.FocusedRowHandle < 0 Then Exit Sub
        'TrangThai.isUpdate = True
        Dim _Index As Integer = gdvThuCT.FocusedRowHandle
        Dim f As New frmCNThu2
        f.Tag = Me.Parent.Tag
        f._TrangThai.isUpdate = True
        f._frmThuTienMat = Me
        f._focusedRowIndex = gdvThuCT.FocusedRowHandle
        f.Text = "Cập nhật phiếu thu " & gdvThuCT.GetFocusedRowCellValue("SoPhieuT") '.ToString.Substring(3, 7)
        f.PhieuThu = gdvThuCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        f.PhieuThuCT = gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString
        f.ThuNH = False
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Or CType(gdvThuCT.GetFocusedRowCellValue("HoanThanh"), Boolean) Or Not CType(gdvThuCT.GetFocusedRowCellValue("HoanThanh"), Boolean) Then

        f.btGhi.Enabled = False
        f.btThem.Enabled = False
        'End If
        f.ShowDialog()
        gdvThuCT.FocusedRowHandle = _Index
    End Sub



    Private Sub btInPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInPhieuThu.ItemClick
        If gdvThuCT.FocusedRowHandle < 0 Then Exit Sub

        Dim sql As String = ""
        sql = "SELECT SoPhieu,NgayThangCT,NguoiNop,DienGiai,SoTien,ChungTuGoc,(SELECT Ten FROM KHACHHANG WHERE ID=THU.IDKh)DiaChi,(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap FROM THU WHERE SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmIn("In phiếu thu")
            Dim rpt As New rptPhieuThuChi
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenPhieu.Text = "PHIẾU THU"
            rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
            rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
            rpt.lbHoTen.Text = "Người nộp tiền: "
            rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNop")
            rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
            rpt.lbLyDo.Text = "Lý do nộp: "
            rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
            rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
            rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
            rpt.lbNguoiGd.Text = "Người nộp tiền"
            rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNop")
            rpt.lbKTNguoiLap.Text = tb.Rows(0)("NguoiLap")
            rpt.CreateDocument()
            f.printControl.PrintingSystem = rpt.PrintingSystem
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub rcbSoTK_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbSoTK.ButtonClick
        If e.Button.Index = 1 Then
            cbSoTK.EditValue = Nothing
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub




    Private Sub btNhapChiPhiTuExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapChiPhiTuExcel.ItemClick
        Dim f As New frmNhapChiPhi
        f.ShowDialog()
        'f.btChonFileExcel.PerformClick()
    End Sub


    Private Sub pMenuThu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuThu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvThuCT.CalcHitInfo(gdvThu.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvThuCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvThuCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuThu.ShowPopup(gdvThu.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvThuCT_RowCellClick(sender As Object, e As XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvThuCT.RowCellClick
        If e.Column.FieldName = "chon" Then
            gdvThuCT.SetRowCellValue(e.RowHandle, "chon", Not gdvThuCT.GetRowCellValue(e.RowHandle, "chon"))
        End If
    End Sub

    Private Sub mnuChuyenCacSoDaChon_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mnuChuyenCacSoDaChon.ItemClick
        gdvThuCT.CloseEditor()
        gdvThuCT.UpdateCurrentRow()
        Dim arrPhieu As New List(Of Integer)
        For i As Integer = 0 To gdvThuCT.RowCount - 1
            If gdvThuCT.GetRowCellValue(i, "chon") = True Then
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


                Dim sql As String = "SELECT * FROM THU WHERE ID = " & gdvThuCT.GetRowCellValue(arrPhieu(i), "ID")
                Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG WHERE ID = @ID"
                AddParameter("@ID", r("IDKh"))
                Dim rKH As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ISNULL((SELECT SUM(sotien) FROM THU WHERE SoPhieuT =  @SoPhieuT),0)"
                AddParameter("@SoPhieuT", r("SoPhieuT"))
                Dim tt As Object = ExecuteSQLDataTable(sql).Rows(0)(0)
                If tt <= r("sotien") Then tt = r("sotien")


                'chung tu
                AddParameter("@LoaiCT", ChungTu.LoaiChungTu.PhieuThuTienMat)
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
                'AddParameter("@SoTkNganHang", r("taikhoanden"))
                'AddParameter("@TenTkNganHang", r("nganhangden"))
                'AddParameter("@SoTkNganHangDoiUng", r("taikhoandi"))
                'AddParameter("@TenTkNganHangDoiUng", r("nganhangdi"))
                AddParameter("@ThanhTien", tt)

                AddParameter("@refId", gdvThuCT.GetRowCellValue(arrPhieu(i), "ID"))
                Dim idHoaDon As Object
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Hàng tiền
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@DienGiai", rKH("Ten") & " thanh toán tiền hàng")
                AddParameter("@ThanhTien", tt)
                AddParameter("@TaiKhoanNo", "1111")
                AddParameter("@TaiKhoanCo", "131")
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

                Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)

                gdvThuCT.SetRowCellValue(arrPhieu(i), "IdCT", idHoaDon)

            Next
            gdvThuCT.BeginUpdate()
            For i As Integer = 0 To gdvThuCT.RowCount - 1
                If gdvThuCT.GetRowCellValue(i, "chon") = True Then
                    gdvThuCT.SetRowCellValue(i, "chon", False)
                End If
            Next
            gdvThuCT.EndUpdate()
            mnuChonBoChonTatCa.Tag = False
            ShowAlert("Đã chuyển thành công " & arrPhieu.Count & " phiếu sang bên thuế!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadThu()
        End Try
    End Sub


    Private Sub mnuChonBoChonTatCa_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mnuChonBoChonTatCa.ItemClick
        Dim arrPhieu As New List(Of String)
        If mnuChonBoChonTatCa.Tag Then
            For i As Integer = 0 To gdvThuCT.RowCount - 1
                If arrPhieu.IndexOf(gdvThuCT.GetRowCellValue(i, "SoPhieuT")) >= 0 Then
                    gdvThuCT.SetRowCellValue(i, "chon", False)
                Else
                    arrPhieu.Add(gdvThuCT.GetRowCellValue(i, "SoPhieuT"))
                    gdvThuCT.SetRowCellValue(i, "chon", True)
                End If
            Next
        Else
            For i As Integer = 0 To gdvThuCT.RowCount - 1
                gdvThuCT.SetRowCellValue(i, "chon", False)
            Next
        End If
        mnuChonBoChonTatCa.Tag = Not mnuChonBoChonTatCa.Tag
    End Sub

    ' Scroll grid và Focus vào dòng index
    Public Sub ScrollToRow(rowIndexToScroll As Integer)
        gdvThuCT.FocusedRowHandle = rowIndexToScroll
    End Sub

    Private Sub mDeNghiSua_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mDeNghiSua.ItemClick
        If CType(gdvThuCT.GetFocusedRowCellValue("DeNghiSua"), Boolean) Then
            ShowCanhBao("Phiếu này đang được đề nghị sửa!")
            Exit Sub
        End If
        Dim dns = New frmDeNghiSua.DENGHISUA_DTO()
        dns.LoaiPhieu = "TT"
        dns.SoPhieu = gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9)
        dns.TenNguoiLapPhieu = gdvThuCT.GetFocusedRowCellValue("NguoiLap").ToString
        dns.ttcMa = gdvThuCT.GetFocusedRowCellValue("ttcMa")
        dns.SoPhieuT = gdvThuCT.GetFocusedRowCellValue("SoPhieuT")
        dns._TagForm = Me.Parent.Tag
        Dim f = New frmDeNghiSua
        f._FormCall = Me
        f._RowSelectedIndex = gdvThuCT.FocusedRowHandle
        f._LogData = LuuDuLieuLichSu(gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9))
        f._DeNghi = dns
        f.ShowDialog()
    End Sub

    Private Sub mXemLichSuPhieu_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mXemLichSuPhieu.ItemClick
        Dim f = New frmLichSuPhieu
        f._PhieuThuTM = New frmThuTienMat
        frmLichSuPhieu.SoPhieu = gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9)
        f.Text = "Lịch sử phiếu " & gdvThuCT.GetFocusedRowCellValue("SoPhieu")
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
    End Sub
    Private Function LuuDuLieuLichSu(SoPhieu As String) As DataSet
        _Hsql &= " SoPhieu = '" & SoPhieu & "' "
        Dim ds = New DataSet
        ds = ExecuteSQLDataSet(_Hsql)
        Return ds
    End Function
    Private Sub LoadLog()
        BanSTTLichSu.Visible = True
        BandLichSuSuaPhieu.Visible = True
        GridBand1.Visible = False
        GridBand11.Visible = False
        Bar1.Visible = False
        pMenuThu.Dispose()
        Dim sql = "Select (Select TenNguoiSua from DENGHISUA WHERE DENGHISUA.ID = IDDeNghi) as NguoiSua, CauTrucBang, PhienBan,"
        sql &= " DuLieu, NgaySua from DeNghiSua_Log Where SoPhieu = '" & frmLichSuPhieu.SoPhieu & "' And TenBang = 'THU'"
        Dim dt = New DataTable
        Dim ds = New DataSet()
        dt = ExecuteSQLDataSet(sql).Tables(0)
        Dim cautrucbang = ""
        Try
            cautrucbang = dt.Rows(0)("CauTrucBang").ToString()
        Catch
            ShowAlert("Phiếu này chưa phải sửa lần nào!")
            Exit Sub
        End Try
        frmLichSuPhieu.XMLSChemaToDataSet(ds, cautrucbang) 'tao kieu du lieu cho bang
        ds.Tables(0).Columns.Add("NguoiSua")
        ds.Tables(0).Columns.Add("PhienBan")
        ds.Tables(0).Columns("PhienBan").DataType = GetType(Integer)
        ds.Tables(0).Columns.Add("NgaySua")
        ds.Tables(0).Columns("NgaySua").DataType = GetType(DateTime)
        ds.Tables(0).Columns.Add("STT2")
        Dim i = 0
        For Each row As DataRow In dt.Rows
            Dim dulieu = row("DuLieu").ToString
            frmLichSuPhieu.XMLToDataSet(ds, dulieu) 'tao du lieu cho bang
            ds.Tables(0).Rows(i)("NguoiSua") = row("NguoiSua")
            ds.Tables(0).Rows(i)("PhienBan") = row("PhienBan")
            ds.Tables(0).Rows(i)("NgaySua") = row("NgaySua")
            ds.Tables(0).Rows(i)("STT2") = i + 1
            i += 1
        Next
        gdvThu.DataSource = ds.Tables(0)
    End Sub
End Class
