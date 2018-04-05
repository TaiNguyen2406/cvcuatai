Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports System.Linq
Imports BACSOFT.HoaDonGTGT

Public Class frmChiNganHang
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0
    'Duong
    Private _Hsql = ""
    Private Sub frmChiNganHang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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


    Public Sub LoadUNC()
        BandLichSuSua.Visible = False
        BandSTT2.Visible = False
        If Me.Parent.Name = "frmLichSuPhieu" Then
            LoadLog()
            Exit Sub
        End If
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT convert(bit,0)chon, UNC.SoPhieuT, 0 AS STT, UNC.ID,NgayThang AS NgayThangVS,(N'UNC ' + UNC.SoPhieu) AS SoPhieu,NgayThang AS NgayThangCT,KHACHHANG.ttcMa,UNC.DienGiai,"
        sql &= " 	UNC.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,MUCDICHTHUCHI.ChiPhiMatDi,TaiKhoanDi AS TaiKhoan,"
        'sql &= "    (Case PhieuTC0 WHEN '000000000' THEN '' ELSE (CASE WHEN MucDich IN (210, 228, 205) THEN N'ĐH '+PhieuTC0 WHEN MucDich IN (200, 224, 244, 235, 205, 230) THEN N'CG '+PhieuTC0 ELSE PhieuTC0 END) END )PhieuTC0,"
        'sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE (CASE WHEN MucDich IN (210, 228, 205) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 205, 230) THEN N'XK '+PhieuTC1 ELSE PhieuTC1 END) END )PhieuTC1,"
        sql &= "    (Case PhieuTC0 WHEN '000000000' THEN '' ELSE "
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'ĐH '+PhieuTC0 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'CG '+PhieuTC0 "
        sql &= "            WHEN MucDich = 205 THEN (CASE UNC.ChiPhiNhap WHEN 1 THEN N'ĐH '+PhieuTC0 ELSE N'CG '+PhieuTC0 END)"
        sql &= "        ELSE PhieuTC0 END) "
        sql &= "    END )PhieuTC0,"
        sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE "
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'XK '+PhieuTC1 "
        sql &= "            WHEN MucDich = 205 THEN (CASE UNC.ChiPhiNhap WHEN 1 THEN N'NK '+PhieuTC1 ELSE N'XK '+PhieuTC1 END)"
        sql &= "        ELSE PhieuTC1 END) "
        sql &= "    END )PhieuTC1,"
        sql &= "    (SELECT SoCT FROM CHUNGTU WHERE ID = UNC.IdChungTu)PhieuUNC, "
        sql &= "    (SELECT TOP 1 a.SoHD FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.LoaiCT = 2 AND a.LoaiCT2 = 5 AND b.GhiChuKhac = UNC.SoPhieuT AND b.ButToan = 1)SoHD,NHANSU.Ten as NguoiLap, UNC.DeNghiSua "
        sql &= " FROM UNC"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=UNC.IDKh"
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=UNC.IDUSer"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=UNC.TienTe"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=UNC.MucDich"
        If chkLocChiPhi.Checked Then
            sql &= " AND MUCDICHTHUCHI.ChiPhiMatDi=1 "
        End If
        sql &= " WHERE "
        'Duong
        _Hsql = sql
        sql &= " CONVERT(datetime,CONVERT(nvarchar,UNC.NgayThang,103),103)  BETWEEN @TuNgay And @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " And UNC.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " And rtrim(ltrim(UNC.TaiKhoanDi))='" & cbSoTK.EditValue & "'"
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next
            gdvUNC.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        LoadUNC()

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

    Private Sub gdvChiCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvUNCCT.RowCellStyle
        On Error Resume Next
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "MucDich" Then
            If gdvUNCCT.GetRowCellValue(e.RowHandle, "ChiPhiMatDi") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub


    Private Sub chkLocChiPhi_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocChiPhi.CheckedChanged
        If chkLocChiPhi.Checked Then
            chkLocChiPhi.Glyph = My.Resources.Checked
        Else
            chkLocChiPhi.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub gdvSoTienMatCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs)
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ConLai" Then
                e.TotalValue = _ConLai
            End If
        End If
    End Sub

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



    Private Sub btThemPhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThemPhieuChi.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        ' TrangThai.isAddNew = True
        Dim f As New frmCNChi2
        f._TrangThai.isAddNew = True
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm UNC"
        f.UNC = True
        f.Show()
    End Sub

    Private Sub btSuaPhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaPhieuChi.ItemClick

        If gdvUNCCT.FocusedRowHandle < 0 Then Exit Sub
        'TrangThai.isUpdate = True
        Dim _Index As Integer = gdvUNCCT.FocusedRowHandle
        Dim f As New frmCNChi2
        f._TrangThai.isUpdate = True
        f.Tag = Me.Parent.Tag
        f.Text = "Cập nhật UNC " & gdvUNCCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(4, 7)
        f.UNC = True
        f.PhieuChi = gdvUNCCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(4, 7)
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
        f.btGhi.Enabled = False
        f.btThem.Enabled = False
        'End If
        f.Show()
        gdvUNCCT.FocusedRowHandle = _Index
    End Sub

    Private Sub btInUNC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInUNC.ItemClick
        If gdvUNCCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT NgayThang,SoPhieu,TaiKhoanDi,NganHangDi,(SELECT Ten FROm KHACHHANG WHERE ID=74)DonViTra,"
        sql &= " 	TaiKhoanDen,NganHangDen,(SELECT Ten FROm KHACHHANG WHERE ID=UNC.IDKh)DonViNhan,DienGiaiNH AS DienGiai,SoTien,N'' AS BangChu"
        sql &= " FROM UNC"
        sql &= " WHERE SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", gdvUNCCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(4, 7))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            tb.Rows(0)("BangChu") = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            Dim f As New frmIn("In ủy nhiệm chi")
            Dim rpt As New rptUNC
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.pAnhLien2.Image = My.Resources.Logo3
            rpt.DataSource = tb
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

    Private Sub mInUNCTong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mInUNCTong.ItemClick
        If gdvUNCCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT NgayThang,SoPhieuT as SoPhieu,TaiKhoanDi,NganHangDi,(SELECT Ten FROm KHACHHANG WHERE ID=74)DonViTra, "
        sql &= "TaiKhoanDen,NganHangDen,(SELECT Ten FROm KHACHHANG WHERE ID=UNC.IDKh)DonViNhan,DienGiaiNH AS DienGiai,SUM(SoTien) as SoTien,N'' AS BangChu "
        sql &= "FROM UNC "
        sql &= "WHERE SoPhieuT=@SoPhieu "
        sql &= "GROUP BY NgayThang,SoPhieuT,TaiKhoanDi,NganHangDi,TaiKhoanDen,NganHangDen,IDKh,DienGiaiNH "
        AddParameterWhere("@SoPhieu", gdvUNCCT.GetFocusedRowCellValue("SoPhieuT"))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            tb.Rows(0)("BangChu") = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            Dim f As New frmIn("In ủy nhiệm chi")
            Dim rpt As New rptUNC
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.pAnhLien2.Image = My.Resources.Logo3
            rpt.DataSource = tb
            rpt.CreateDocument()
            f.printControl.PrintingSystem = rpt.PrintingSystem
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btNhapChiPhiTuExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapChiPhiTuExcel.ItemClick
        Dim f As New frmNhapChiPhi
        f.chkUNC.Checked = True
        f.ShowDialog()
    End Sub

    Private Sub pMenuChi_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChi.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvUNCCT.CalcHitInfo(gdvUNC.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvUNCCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvUNCCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuChi.ShowPopup(gdvUNC.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvUNCCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvUNCCT.RowCellClick
        If e.Column.FieldName = "chon" Then
            gdvUNCCT.SetRowCellValue(e.RowHandle, "chon", Not gdvUNCCT.GetRowCellValue(e.RowHandle, "chon"))
        End If
    End Sub


    Private Sub mnuChuyenSangBenThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenSangBenThue.ItemClick
        If gdvUNCCT.FocusedRowHandle < 0 Then Exit Sub

        Dim f As New frmCapNhatThuChiNganHangThue
        TrangThai.isAddNew = True
        f.LoaiCT = ChungTu.LoaiChungTu.UyNhiemChi
        f.Text = "Thêm mới chứng từ ủy nhiệm chi"
        f.idChungTu = gdvUNCCT.GetFocusedRowCellValue("ID")

        Dim sql As String = "SELECT * FROM UNC WHERE ID = " & f.idChungTu
        Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

        Dim dt1 As DataTable = CType(f.cmbSoTK.Properties.DataSource, DataTable)
        Dim dr1 As DataRow = dt1.NewRow
        dr1("MaSo") = r("TaiKhoandi").ToString.Trim
        dr1("Ten") = r("Nganhangdi").ToString.Trim
        dr1("ID") = DBNull.Value
        dt1.Rows.InsertAt(dr1, dt1.Rows.Count)

        Dim dt2 As DataTable = CType(f.cmbSoTaiKhoanDoiUng.Properties.DataSource, DataTable)
        Dim dr2 As DataRow = dt2.NewRow
        dr2("MaSo") = r("Taikhoanden").ToString.Trim
        dr2("Ten") = r("Nganhangden").ToString.Trim
        dr2("ID") = DBNull.Value
        dt2.Rows.InsertAt(dr2, dt2.Rows.Count)


        f.txtNgayCT.EditValue = r("Ngaythang")
        f.cmbTienTe.SelectedItem = f.cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.ID = r("TienTe")).FirstOrDefault
        f.cmbDoiTuong.EditValue = r("IDKh")
        f.txtNoiDung.EditValue = "Thanh toán tiền hàng " & f.txtTenDoiTuong.Text
        f.cmbSoTK.EditValue = r("TaiKhoandi").ToString.Trim
        f.txtTenTaiKhoan.EditValue = r("Nganhangdi").ToString.Trim
        f.cmbSoTaiKhoanDoiUng.EditValue = r("Taikhoanden").ToString.Trim
        f.txtTenTaiKhoanDoiUng.EditValue = r("Nganhangden").ToString.Trim
        'f.txtDaiDien.EditValue = r("")
        f.gdvData.AddNewRow()

        f.gdvData.SetFocusedRowCellValue("ThanhTien", r("sotien"))

        sql = "SELECT SUM(sotien) FROM UNC WHERE SoPhieuT = @SoPhieuT"
        AddParameter("@SoPhieuT", r("SoPhieuT"))
        Dim tt As Object = ExecuteSQLDataTable(sql).Rows(0)(0)
        If tt > r("sotien") Then
            If ShowCauHoi("Chứng từ này gồm nhiều phiếu có tổng giá trị " & tt & ", chọn số tiền này thay cho " & r("sotien") & "?") Then
                f.gdvData.SetFocusedRowCellValue("ThanhTien", tt)
            End If
        End If

        f.gdvData.SetFocusedRowCellValue("DienGiai", "Thanh toán tiền hàng " & f.txtTenDoiTuong.Text)
        f.gdvData.SetFocusedRowCellValue("TaiKhoanNo", "331")
        f.gdvData.SetFocusedRowCellValue("TaiKhoanCo", "1121")
        f.gdvData.Focus()
        f.gdvData.FocusedColumn = f.gdvData.Columns("DienGiai")
        f.gdvData.ShowEditor()


        f.ShowDialog()


        If Not f.idChungTu Is Nothing Then
            gdvUNCCT.SetFocusedRowCellValue("SoCT", f.tbSoPhieu.EditValue)
            gdvUNCCT.SetFocusedRowCellValue("IdCT", f.idHoaDon)
        End If
    End Sub

    Private Sub mnuChuyenCacPhieuDaChon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenCacPhieuDaChon.ItemClick
        gdvUNCCT.CloseEditor()
        gdvUNCCT.UpdateCurrentRow()
        Dim arrPhieu As New List(Of Integer)
        For i As Integer = 0 To gdvUNCCT.RowCount - 1
            If gdvUNCCT.GetRowCellValue(i, "chon") = True Then
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


                Dim sql As String = "SELECT * FROM UNC WHERE ID = " & gdvUNCCT.GetRowCellValue(arrPhieu(i), "ID")
                Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG WHERE ID = @ID"
                AddParameter("@ID", r("IDKh"))
                Dim rKH As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ISNULL((SELECT SUM(sotien) FROM UNC WHERE SoPhieuT =  @SoPhieuT),0)"
                AddParameter("@SoPhieuT", r("SoPhieuT"))
                Dim tt As Object = ExecuteSQLDataTable(sql).Rows(0)(0)
                If tt <= r("sotien") Then tt = r("sotien")


                'chung tu
                AddParameter("@LoaiCT", ChungTu.LoaiChungTu.UyNhiemChi)
                AddParameter("@NgayCT", r("ngaythang"))
                AddParameter("@TienTe", 0)
                AddParameter("@TyGia", 1)
                AddParameter("@GhiSo", 0)
                AddParameter("@IdKH", r("IDKh"))
                AddParameter("@TenKH", rKH("Ten"))
                AddParameter("@DiaChi", rKH("ttcDiachi"))
                AddParameter("@MaSoThue", rKH("ttcMasothue"))
                AddParameter("@NguoiLienHe", DBNull.Value)
                AddParameter("@DienGiai", "Thanh toán tiền hàng " & rKH("Ten"))
                AddParameter("@SoTkNganHang", r("taikhoandi"))
                AddParameter("@TenTkNganHang", r("nganhangdi"))
                AddParameter("@SoTkNganHangDoiUng", r("taikhoanden"))
                AddParameter("@TenTkNganHangDoiUng", r("nganhangden"))
                AddParameter("@ThanhTien", tt)

                AddParameter("@refId", gdvUNCCT.GetRowCellValue(arrPhieu(i), "ID"))
                Dim idHoaDon As Object
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Hàng tiền
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@DienGiai", "Thanh toán tiền hàng " & rKH("Ten"))
                AddParameter("@ThanhTien", tt)
                AddParameter("@TaiKhoanNo", "331")
                AddParameter("@TaiKhoanCo", "1121")
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

                Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)


                gdvUNCCT.SetRowCellValue(arrPhieu(i), "IdCT", idHoaDon)

            Next
            For i As Integer = 0 To gdvUNCCT.RowCount - 1
                gdvUNCCT.SetRowCellValue(i, "chon", False)
            Next
            mnuChonBoChonTatCa.Tag = False
            ShowAlert("Đã chuyển thành công " & arrPhieu.Count & " phiếu sang bên thuế!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadUNC()
        End Try
    End Sub

    Private Sub mnuChonBoChonTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChonBoChonTatCa.ItemClick
        Dim arrPhieu As New List(Of String)
        If mnuChonBoChonTatCa.Tag Then
            For i As Integer = 0 To gdvUNCCT.RowCount - 1
                If arrPhieu.IndexOf(gdvUNCCT.GetRowCellValue(i, "SoPhieuT")) >= 0 Then
                    gdvUNCCT.SetRowCellValue(i, "chon", False)
                Else
                    arrPhieu.Add(gdvUNCCT.GetRowCellValue(i, "SoPhieuT"))
                    gdvUNCCT.SetRowCellValue(i, "chon", True)
                End If
            Next
        Else
            For i As Integer = 0 To gdvUNCCT.RowCount - 1
                gdvUNCCT.SetRowCellValue(i, "chon", False)
            Next
        End If
        mnuChonBoChonTatCa.Tag = Not mnuChonBoChonTatCa.Tag
    End Sub

    Private Sub mXemPhieuTC0_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemPhieuTC0.ItemClick
        If gdvUNCCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvUNCCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Length > 0 Then
            If gdvUNCCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(0, 2) = "CG" Then
                'If gdvUNCCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvUNCCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
                '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                '        ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                '        Exit Sub
                '    End If
                'End If

                TrangThai.isUpdate = True
                fCNChaoGia = New frmCNChaoGia
                fCNChaoGia.TrangThaiCG.isUpdate = True
                'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
                fCNChaoGia.SPChaoGia = gdvUNCCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(3, 9)
                fCNChaoGia.Tag = deskTop.mChaoGia.Name
                fCNChaoGia.btGhi.Enabled = False
                fCNChaoGia.Show()
            ElseIf gdvUNCCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(0, 2) = "ĐH" Then

                Dim f As New frmCNDatHang
                f._SoPhieu = gdvUNCCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(3, 9)
                f.Tag = Me.Parent.Tag
                f.ShowDialog()
            End If
        End If
    End Sub

    Private Sub mXemPhieuTC1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemPhieuTC1.ItemClick

        If gdvUNCCT.FocusedRowHandle < 0 Then Exit Sub

        If gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Length > 0 Then
            If gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "XK" Then
                TrangThai.isUpdate = True
                fCNXuatKho = New frmCNXuatKho
                fCNXuatKho.PhieuXK = gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
                fCNXuatKho.Tag = deskTop.mXuatKho.Name
                If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.QuyenSua) Then
                    fCNXuatKho.btGhi.Enabled = False
                    fCNXuatKho.btChuyenXK.Enabled = False
                    fCNXuatKho.btCal.Enabled = False
                    fCNXuatKho.btTichThue.Enabled = False
                    fCNXuatKho.mChonBoChon.Enabled = False
                End If
                fCNXuatKho.ShowDialog()
            ElseIf gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "CG" Then
                'If gdvUNCCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvUNCCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
                '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                '        ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                '        Exit Sub
                '    End If
                'End If

                TrangThai.isUpdate = True
                fCNChaoGia = New frmCNChaoGia
                fCNChaoGia.TrangThaiCG.isUpdate = True
                'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
                fCNChaoGia.SPChaoGia = gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
                fCNChaoGia.Tag = deskTop.mChaoGia.Name
                fCNChaoGia.btGhi.Enabled = False
                fCNChaoGia.Show()

            ElseIf gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "NK" Then
                TrangThai.isUpdate = True

                fCNNhapKho = New frmCNNhapKho
                fCNNhapKho.PhieuNK = gdvUNCCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
                fCNNhapKho.Tag = Me.Parent.Tag
                If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mNhapKho.Name, DanhMucQuyen.QuyenSua) Then
                    fCNNhapKho.gdvVTCT.OptionsBehavior.ReadOnly = True
                    fCNNhapKho.mNhapKho.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    fCNNhapKho.btChuyenXK.Visible = False
                    fCNNhapKho.btCal.Enabled = False
                    fCNNhapKho.btGhi.Enabled = False
                    fCNNhapKho.btTichThue.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
                fCNNhapKho.ShowDialog()
            End If
        End If

    End Sub

    Private Sub mmDeNghiSua_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mmDeNghiSua.ItemClick
        If CType(gdvUNCCT.GetFocusedRowCellValue("DeNghiSua"), Boolean) Then
            ShowCanhBao("Phiếu này đang được đề nghị sửa!")
            Exit Sub
        End If
        Dim dns = New frmDeNghiSua.DENGHISUA_DTO()
        dns.LoaiPhieu = "UNC"
        dns.SoPhieu = gdvUNCCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(4, 9)
        dns.TenNguoiLapPhieu = gdvUNCCT.GetFocusedRowCellValue("NguoiLap").ToString
        dns.ttcMa = gdvUNCCT.GetFocusedRowCellValue("ttcMa")
        dns.SoPhieuT = gdvUNCCT.GetFocusedRowCellValue("SoPhieuT")
        dns._TagForm = Me.Parent.Tag
        Dim f = New frmDeNghiSua
        f._FormCall = Me
        f._RowSelectedIndex = gdvUNCCT.FocusedRowHandle
        f._LogData = LuuDuLieuLichSu(gdvUNCCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(4, 9))
        f._DeNghi = dns
        f.ShowDialog()
    End Sub
    Private Function LuuDuLieuLichSu(SoPhieu As String) As DataSet
        _Hsql &= " SoPhieu = '" & SoPhieu & "' "
        Dim ds = New DataSet
        ds = ExecuteSQLDataSet(_Hsql)
        Return ds
    End Function

    Private Sub LoadLog()
        Bar1.Visible = False
        pMenuChi.Dispose()
        BandSTT.Visible = False
        BandDNS.Visible = False
        BandLichSuSua.Visible = True
        BandSTT2.Visible = True

        Dim sql = "Select (Select TenNguoiSua from DENGHISUA WHERE DENGHISUA.ID = IDDeNghi) as NguoiSua, CauTrucBang, PhienBan,"
        sql &= " DuLieu, NgaySua from DeNghiSua_Log Where SoPhieu = '" & frmLichSuPhieu.SoPhieu & "' And TenBang = 'UNC'"
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
        ds.Tables(0).Columns("STT2").DataType = GetType(Integer)
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
        gdvUNC.DataSource = ds.Tables(0)
    End Sub

    Private Sub mXemLichSuPhieu_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mXemLichSuPhieu.ItemClick
        Dim f = New frmLichSuPhieu
        f._PhieuChiNH = New frmChiNganHang
        frmLichSuPhieu.SoPhieu = gdvUNCCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(4, 9)
        f.Text = "Lịch sử phiếu " & gdvUNCCT.GetFocusedRowCellValue("SoPhieu")
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
    End Sub
End Class
