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

        ShowWaiting("Đang tải dữ liệu ...")

        Dim sql As String = " SET DATEFORMAT DMY " & vbCrLf
        sql &= " SELECT convert(bit,0)chon, CHI.SoPhieuT, 0 AS STT, CHI.ID,NgayThangVS,(N'CT ' + CHI.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,CHI.DienGiai,CHI.TyGia," & vbCrLf
        sql &= " 	CHI.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,NguoiNhan,MUCDICHTHUCHI.ChiPhiMatDi," & vbCrLf
        sql &= "    (Case PhieuTC0 WHEN '000000000' THEN '' ELSE " & vbCrLf
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'ĐH '+PhieuTC0 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'CG '+PhieuTC0 " & vbCrLf
        sql &= "            WHEN MucDich = 205 THEN (CASE CHI.ChiPhiNhap WHEN 1 THEN N'ĐH '+PhieuTC0 ELSE N'CG '+PhieuTC0 END)" & vbCrLf
        sql &= "        ELSE PhieuTC0 END) " & vbCrLf
        sql &= "    END )PhieuTC0," & vbCrLf
        sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE " & vbCrLf
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'XK '+PhieuTC1 " & vbCrLf
        sql &= "            WHEN MucDich = 205 THEN (CASE CHI.ChiPhiNhap WHEN 1 THEN N'NK '+PhieuTC1 ELSE N'XK '+PhieuTC1 END)" & vbCrLf
        sql &= "        ELSE PhieuTC1 END) " & vbCrLf
        sql &= "    END )PhieuTC1" & vbCrLf
        ' sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE (CASE WHEN MucDich IN (210, 228, 205) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 205, 230) THEN N'XK '+PhieuTC1 ELSE PhieuTC1 END) END )PhieuTC1,"
        'sql &= "    (SELECT SoCT FROM CHUNGTU WHERE ID = CHI.IdChungTu)SoPhieuChi, " & vbCrLf
        'sql &= "    (SELECT TOP 1 a.SoHD FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.LoaiCT = 2 AND a.LoaiCT2 = 5 AND b.GhiChuKhac = CHI.SoPhieuT AND b.ButToan = 1)SoHD,NHANSU.Ten as NguoiLap " & vbCrLf
        sql &= " ,NHANSU.Ten as NguoiLap " & vbCrLf
        sql &= " FROM CHI" & vbCrLf
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=CHI.IDKh" & vbCrLf
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=CHI.IDUser" & vbCrLf
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=CHI.TienTe" & vbCrLf
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=CHI.MucDich" & vbCrLf
        If chkLocChiPhi.Checked Then
            sql &= " AND MUCDICHTHUCHI.ChiPhiMatDi=1 " & vbCrLf
        End If
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,CHI.NgayThangCT ,103),103) BETWEEN @TuNgay AND @DenNgay" & vbCrLf
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND CHI.IDKh=" & btfilterMaKH.EditValue & vbCrLf
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND ltrim(rtrim(CHI.MaTK))='" & cbSoTK.EditValue & "' " & vbCrLf
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu" & vbCrLf

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
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            f.btGhi.Enabled = False
            f.btThem.Enabled = False
        End If

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

    Private Sub gdvChiCT_RowCellClick(sender As Object, e As XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvChiCT.RowCellClick
        If e.Column.FieldName = "chon" Then
            gdvChiCT.SetRowCellValue(e.RowHandle, "chon", Not gdvChiCT.GetRowCellValue(e.RowHandle, "chon"))
        End If
    End Sub

    Private Sub mnuChuyenCacPhieuDaChon_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mnuChuyenCacPhieuDaChon.ItemClick
        gdvChiCT.CloseEditor()
        gdvChiCT.UpdateCurrentRow()
        Dim arrPhieu As New List(Of Integer)
        For i As Integer = 0 To gdvChiCT.RowCount - 1
            If gdvChiCT.GetRowCellValue(i, "chon") = True Then
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


                Dim sql As String = "SELECT * FROM CHI WHERE ID = " & gdvChiCT.GetRowCellValue(arrPhieu(i), "ID")
                Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG WHERE ID = @ID"
                AddParameter("@ID", r("IDKh"))
                Dim rKH As DataRow = ExecuteSQLDataTable(sql).Rows(0)

                sql = "SELECT ISNULL((SELECT SUM(sotien) FROM CHI WHERE SoPhieuT =  @SoPhieuT),0)"
                AddParameter("@SoPhieuT", r("SoPhieuT"))
                Dim tt As Object = ExecuteSQLDataTable(sql).Rows(0)(0)
                If tt <= r("sotien") Then tt = r("sotien")


                'chung tu
                AddParameter("@LoaiCT", ChungTu.LoaiChungTu.PhieuChiTienMat)
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

                AddParameter("@refId", gdvChiCT.GetRowCellValue(arrPhieu(i), "ID"))
                Dim idHoaDon As Object
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Hàng tiền
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@DienGiai", rKH("Ten") & " thanh toán tiền hàng")
                AddParameter("@ThanhTien", tt)
                AddParameter("@TaiKhoanNo", "331")
                AddParameter("@TaiKhoanCo", "1111")
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

                Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)

                gdvChiCT.SetRowCellValue(arrPhieu(i), "IdCT", idHoaDon)

            Next
            gdvChiCT.BeginUpdate()
            For i As Integer = 0 To gdvChiCT.RowCount - 1
                If gdvChiCT.GetRowCellValue(i, "chon") = True Then
                    gdvChiCT.SetRowCellValue(i, "chon", False)
                End If
            Next
            gdvChiCT.EndUpdate()
            mnuChonBoChonTatCa.Tag = False
            ShowAlert("Đã chuyển thành công " & arrPhieu.Count & " phiếu sang bên thuế!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadChi()
        End Try
    End Sub

    Private Sub mnuChonBoChonTatCa_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mnuChonBoChonTatCa.ItemClick
        Dim arrPhieu As New List(Of String)
        If mnuChonBoChonTatCa.Tag Then
            For i As Integer = 0 To gdvChiCT.RowCount - 1
                If arrPhieu.IndexOf(gdvChiCT.GetRowCellValue(i, "SoPhieuT")) >= 0 Then
                    gdvChiCT.SetRowCellValue(i, "chon", False)
                Else
                    arrPhieu.Add(gdvChiCT.GetRowCellValue(i, "SoPhieuT"))
                    gdvChiCT.SetRowCellValue(i, "chon", True)
                End If
            Next
        Else
            For i As Integer = 0 To gdvChiCT.RowCount - 1
                gdvChiCT.SetRowCellValue(i, "chon", False)
            Next
        End If
        mnuChonBoChonTatCa.Tag = Not mnuChonBoChonTatCa.Tag
    End Sub

End Class
