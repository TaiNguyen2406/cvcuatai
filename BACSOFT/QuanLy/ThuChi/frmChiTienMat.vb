Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmChiTienMat

    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0
    'Duong
    Private _Hsql = ""
    Private Sub frmChiTienMat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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

    Public Sub LoadChi()
        BandLichSuSua.Visible = False
        BandSTT2.Visible = False
        If Me.Parent.Name = "frmLichSuPhieu" Then
            LoadLog()
            Exit Sub
        End If
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT CHI.SoPhieuT, 0 AS STT, CHI.ID,NgayThangVS,(N'CT ' + CHI.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,CHI.DienGiai,"
        sql &= " 	CHI.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,NguoiNhan,MUCDICHTHUCHI.ChiPhiMatDi,"
        sql &= "    (Case PhieuTC0 WHEN '000000000' THEN '' ELSE "
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'ĐH '+PhieuTC0 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'CG '+PhieuTC0 "
        sql &= "            WHEN MucDich = 205 THEN (CASE CHI.ChiPhiNhap WHEN 1 THEN N'ĐH '+PhieuTC0 ELSE N'CG '+PhieuTC0 END)"
        sql &= "        ELSE PhieuTC0 END) "
        sql &= "    END )PhieuTC0,"
        sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE "
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'XK '+PhieuTC1 "
        sql &= "            WHEN MucDich = 205 THEN (CASE CHI.ChiPhiNhap WHEN 1 THEN N'NK '+PhieuTC1 ELSE N'XK '+PhieuTC1 END)"
        sql &= "        ELSE PhieuTC1 END) "
        sql &= "    END )PhieuTC1,"
        ' sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE (CASE WHEN MucDich IN (210, 228, 205) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 205, 230) THEN N'XK '+PhieuTC1 ELSE PhieuTC1 END) END )PhieuTC1,"
        sql &= "    (SELECT SoCT FROM CHUNGTU WHERE ID = CHI.IdChungTu)SoPhieuChi, "
        sql &= "    (SELECT TOP 1 a.SoHD FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.LoaiCT = 2 AND a.LoaiCT2 = 5 AND b.GhiChuKhac = CHI.SoPhieuT AND b.ButToan = 1)SoHD,NHANSU.Ten as NguoiLap, CHI.DeNghiSua "
        sql &= " FROM CHI"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=CHI.IDKh"
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=CHI.IDUser"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=CHI.TienTe"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=CHI.MucDich"
        If chkLocChiPhi.Checked Then
            sql &= " AND MUCDICHTHUCHI.ChiPhiMatDi=1 "
        End If
        sql &= " WHERE "
        'Duong
        _Hsql = sql
        sql &= " CONVERT(datetime,CONVERT(nvarchar,CHI.NgayThangCT ,103),103) BETWEEN @TuNgay And @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " And CHI.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " And ltrim(rtrim(CHI.MaTK))='" & cbSoTK.EditValue & "' "
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next
            gdvChi.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadChi()
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

    Private Sub gdvChiCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvChiCT.RowCellStyle
        On Error Resume Next
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "MucDich" Then
            If gdvChiCT.GetRowCellValue(e.RowHandle, "ChiPhiMatDi") Then
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


    Private Sub btThemPhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThemPhieuChi.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        'TrangThai.isAddNew = True
        Dim f As New frmCNChi2
        f._TrangThai.isAddNew = True
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu chi"
        f.Show()
    End Sub

    Private Sub btSuaPhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaPhieuChi.ItemClick

        If gdvChiCT.FocusedRowHandle < 0 Then Exit Sub
        ' TrangThai.isUpdate = True
        Dim _Index As Integer = gdvChiCT.FocusedRowHandle
        Dim f As New frmCNChi2
        f._TrangThai.isUpdate = True
        f.Tag = Me.Parent.Tag
        f.Text = "Cập nhật phiếu chi " & gdvChiCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        f.UNC = False
        f.PhieuChi = gdvChiCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
        f.btGhi.Enabled = False
        f.btThem.Enabled = False
        'End If

        f.Show()
        gdvChiCT.FocusedRowHandle = _Index
    End Sub

    Private Sub btInPhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInPhieuChi.ItemClick
        If gdvChiCT.FocusedRowHandle < 0 Then Exit Sub

        Dim sql As String = ""
        sql = "SELECT SoPhieu,NgayThangCT,NguoiNhan,DienGiai,SoTien,ChungTuGoc,(SELECT Ten FROM KHACHHANG WHERE ID=CHI.IDKh)DiaChi,(SELECT Ten FROM NHANSU WHERE ID=CHI.IDUser)NguoiLap FROM CHI WHERE SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", gdvChiCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmIn("In phiếu chi")
            Dim rpt As New rptPhieuThuChi
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenPhieu.Text = "PHIẾU CHI"
            rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
            rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
            rpt.lbHoTen.Text = "Người nhận tiền: "
            rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNhan")
            rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
            rpt.lbLyDo.Text = "Lý do chi: "
            rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
            rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
            rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
            rpt.lbNguoiGd.Text = "Người nhận tiền"
            rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNhan")
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

    Private Sub mInPhieuChiTong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mInPhieuChiTong.ItemClick
        If gdvChiCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql = "SELECT SoPhieuT as SoPhieu,NgayThangCT,NguoiNhan,DienGiaiChung as DienGiai,SUM(SoTien) as SoTien,ChungTuGoc, "
        sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=CHI.IDKh)DiaChi, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID=CHI.IDUser)NguoiLap FROM CHI WHERE SoPhieuT=@SoPhieu "
        sql &= "GROUP BY SoPhieuT,NgayThangCT,NguoiNhan,DienGiaiChung,ChungTuGoc,IDKh,IDUser "
        AddParameterWhere("@SoPhieu", gdvChiCT.GetFocusedRowCellValue("SoPhieuT"))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmIn("In phiếu chi")
            Dim rpt As New rptPhieuThuChi
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenPhieu.Text = "PHIẾU CHI"
            rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
            rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
            rpt.lbHoTen.Text = "Người nhận tiền: "
            rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNhan")
            rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
            rpt.lbLyDo.Text = "Lý do chi: "
            rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
            rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
            rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
            rpt.lbNguoiGd.Text = "Người nhận tiền"
            rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNhan")
            rpt.lbKTNguoiLap.Text = tb.Rows(0)("NguoiLap")
            rpt.CreateDocument()
            f.printControl.PrintingSystem = rpt.PrintingSystem
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub



    Private Sub btNhapChiPhiTuExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapChiPhiTuExcel.ItemClick
        Dim f As New frmNhapChiPhi
        f.ShowDialog()
        'f.btChonFileExcel.PerformClick()
    End Sub

    Private Sub pMenuChi_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChi.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvChiCT.CalcHitInfo(gdvChi.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvChiCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvChiCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuChi.ShowPopup(gdvChi.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mXemPhieuTC0_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemPhieuTC0.ItemClick
        If gdvChiCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvChiCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Length > 0 Then
            If gdvChiCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(0, 2) = "CG" Then
                'If gdvChiCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvChiCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
                '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                '        ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                '        Exit Sub
                '    End If
                'End If

                TrangThai.isUpdate = True
                fCNChaoGia = New frmCNChaoGia
                fCNChaoGia.TrangThaiCG.isUpdate = True
                'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
                fCNChaoGia.SPChaoGia = gdvChiCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(3, 9)
                fCNChaoGia.Tag = deskTop.mChaoGia.Name
                fCNChaoGia.btGhi.Enabled = False
                fCNChaoGia.Show()
            ElseIf gdvChiCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(0, 2) = "ĐH" Then

                Dim f As New frmCNDatHang
                f._SoPhieu = gdvChiCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(3, 9)
                f.Tag = Me.Parent.Tag
                f.ShowDialog()
            End If
        End If
    End Sub

    Private Sub mXemPhieuTC1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemPhieuTC1.ItemClick

        If gdvChiCT.FocusedRowHandle < 0 Then Exit Sub

        If gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Length > 0 Then
            If gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "XK" Then
                TrangThai.isUpdate = True
                fCNXuatKho = New frmCNXuatKho
                fCNXuatKho.PhieuXK = gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
                fCNXuatKho.Tag = deskTop.mXuatKho.Name
                If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.QuyenSua) Then
                    fCNXuatKho.btGhi.Enabled = False
                    fCNXuatKho.btChuyenXK.Enabled = False
                    fCNXuatKho.btCal.Enabled = False
                    fCNXuatKho.btTichThue.Enabled = False
                    fCNXuatKho.mChonBoChon.Enabled = False
                End If
                fCNXuatKho.ShowDialog()
            ElseIf gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "CG" Then
                'If gdvChiCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvChiCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
                '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                '        ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                '        Exit Sub
                '    End If
                'End If

                TrangThai.isUpdate = True
                fCNChaoGia = New frmCNChaoGia
                fCNChaoGia.TrangThaiCG.isUpdate = True
                'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
                fCNChaoGia.SPChaoGia = gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
                fCNChaoGia.Tag = deskTop.mChaoGia.Name
                fCNChaoGia.btGhi.Enabled = False
                fCNChaoGia.Show()

            ElseIf gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "NK" Then
                TrangThai.isUpdate = True

                fCNNhapKho = New frmCNNhapKho
                fCNNhapKho.PhieuNK = gdvChiCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
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

    Private Sub mDeNghiSua_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mDeNghiSua.ItemClick
        If CType(gdvChiCT.GetFocusedRowCellValue("DeNghiSua"), Boolean) Then
            ShowCanhBao("Phiếu này đang được đề nghị sửa!")
            Exit Sub
        End If
        Dim dns = New frmDeNghiSua.DENGHISUA_DTO()
        dns.LoaiPhieu = "CT"
        dns.SoPhieu = gdvChiCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9)
        dns.TenNguoiLapPhieu = gdvChiCT.GetFocusedRowCellValue("NguoiLap").ToString
        dns.ttcMa = gdvChiCT.GetFocusedRowCellValue("ttcMa")
        dns.SoPhieuT = gdvChiCT.GetFocusedRowCellValue("SoPhieuT")
        dns._TagForm = Me.Parent.Tag
        Dim f = New frmDeNghiSua
        f._FormCall = Me
        f._LogData = LuuDuLieuLichSu(gdvChiCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9))
        f._RowSelectedIndex = gdvChiCT.FocusedRowHandle
        f._DeNghi = dns
        f.ShowDialog()
    End Sub

    Private Sub mXemLichSuPhieuChi_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mXemLichSuPhieuChi.ItemClick
        Dim f = New frmLichSuPhieu
        f._PhieuChiTM = New frmChiTienMat
        frmLichSuPhieu.SoPhieu = gdvChiCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9)
        f.Text = "Lịch sử phiếu " & gdvChiCT.GetFocusedRowCellValue("SoPhieu")
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
        Bar1.Visible = False
        pMenuChi.Dispose()
        BandSTT.Visible = False
        BandDNS.Visible = False
        BandLichSuSua.Visible = True
        BandSTT2.Visible = True

        Dim sql = "Select (Select TenNguoiSua from DENGHISUA WHERE DENGHISUA.ID = IDDeNghi) as NguoiSua, CauTrucBang, PhienBan,"
        sql &= " DuLieu, NgaySua from DeNghiSua_Log Where SoPhieu = '" & frmLichSuPhieu.SoPhieu & "' And TenBang = 'CHI'"
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
        gdvChi.DataSource = ds.Tables(0)
    End Sub
End Class
