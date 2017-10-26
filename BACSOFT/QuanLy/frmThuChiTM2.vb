Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmThuChiTM2
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0

    Private Sub frmThuChi_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT THU.SoPhieuT, 0 AS STT, THU.ID,NgayThangVS,(N'TT ' + THU.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,THU.DienGiai,"
        sql &= " 	THU.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,NguoiNop,"
        sql &= " (CASE PhieuTC0 WHEN N'000000000' THEN N'' ELSE N'CG ' + PhieuTC0 END) PhieuTC0,"
        sql &= " (CASE PhieuTC1 WHEN N'000000000' THEN N'' ELSE N'XK ' + PhieuTC1 END) PhieuTC1,"
        sql &= " (SELECT SoCT FROM CHUNGTU WHERE ID = THU.IdChungTu)SoPhieuThu "
        sql &= " FROM THU"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=THU.IDKh"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=THU.TienTe"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=THU.MucDich"
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,THU.NgayThangCT,103),103) BETWEEN @TuNgay AND @DenNgay"
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

    Public Sub LoadChi()

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
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'ĐH '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'CG '+PhieuTC1 "
        sql &= "            WHEN MucDich = 205 THEN (CASE CHI.ChiPhiNhap WHEN 1 THEN N'NK '+PhieuTC1 ELSE N'XK '+PhieuTC1 END)"
        sql &= "        ELSE PhieuTC1 END) "
        sql &= "    END )PhieuTC1,"
        ' sql &= "    (Case PhieuTC1 WHEN '000000000' THEN '' ELSE (CASE WHEN MucDich IN (210, 228, 205) THEN N'NK '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 205, 230) THEN N'XK '+PhieuTC1 ELSE PhieuTC1 END) END )PhieuTC1,"
        sql &= "    (SELECT SoCT FROM CHUNGTU WHERE ID = CHI.IdChungTu)SoPhieuChi, "
        sql &= "    (SELECT TOP 1 a.SoHD FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.LoaiCT = 2 AND a.LoaiCT2 = 5 AND b.GhiChuKhac = CHI.SoPhieuT AND b.ButToan = 1)SoHD "
        sql &= " FROM CHI"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=CHI.IDKh"

        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=CHI.TienTe"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=CHI.MucDich"
        If chkLocChiPhi.Checked Then
            sql &= " AND MUCDICHTHUCHI.ChiPhiMatDi=1 "
        End If
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,CHI.NgayThangCT ,103),103) BETWEEN @TuNgay AND @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND CHI.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND ltrim(rtrim(CHI.MaTK))='" & cbSoTK.EditValue & "' "
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



    Public Sub LoadSoTM()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT (ISNULL((SELECT SUM(SoTien) FROM THU WHERE THU.NgayThangCT<@TuNgay),0) - ISNULL((SELECT SUM(SoTien) FROM Chi WHERE CHI.NgayThangCT<@TuNgay),0))AS DauKy"
        sql &= "   SELECT STT, tb.ID,NgayThangVS,SoPhieu,NgayThangCT,IDKh,DienGiai,"
        sql &= " 	(CASE THUCHI WHEN 0 THEN tb.SoTien ELSE NULL END)TienThu,(CASE THUCHI WHEN 1 THEN tb.SoTien ELSE NULL END)TienChi,0.0 AS ConLai,"
        sql &= " 	tblTienTe.Ten AS TienTe,NguoiGiaoDich,THUCHI,KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich"

        sql &= " FROM "
        sql &= " (SELECT 0 AS STT, THU.ID,NgayThangVS,(N'TT ' + THU.SoPhieu) AS SoPhieu,NgayThangCT,THU.IDKh,THU.DienGiai,"
        sql &= " 	THU.SoTien,TienTe,MucDich,NguoiNop AS NguoiGiaoDich,Convert(bit,0) AS THUCHI,MaTK"
        sql &= " FROM THU"
        sql &= " UNION ALL "
        sql &= " SELECT 0 AS STT, CHI.ID,NgayThangVS,(N'CT ' + CHI.SoPhieu) AS SoPhieu,NgayThangCT,CHI.IDKh,CHI.DienGiai,"
        sql &= " 	CHI.SoTien,TienTe,MucDich,NguoiNhan AS NguoiGiaoDich,Convert(bit,1) AS THUCHI,MaTK"
        sql &= " FROM CHI)tb"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=tb.IDKh"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=tb.MucDich"
        sql &= " INNER JOIN tblTienTe ON tblTienTe.ID = tb.TienTe"
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,tb.NgayThangCT ,103),103)  BETWEEN @TuNgay AND @DenNgay"
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND ltrim(rtrim(tb.MaTK))='" & cbSoTK.EditValue & "' "
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If Not ds Is Nothing Then
            gridBandSoTien.Caption = String.Format("Số tiền đầu kỳ:   {0:N0}", ds.Tables(0).Rows(0)(0))
            If ds.Tables(1).Rows.Count > 0 Then

                With ds.Tables(1)
                    .Rows(0)("STT") = 1
                    Dim _TienThu As Double = 0
                    Dim _TienChi As Double = 0
                    If IsDBNull(.Rows(0)("TienThu")) Then
                        _TienThu = 0
                    Else
                        _TienThu = .Rows(0)("TienThu")
                    End If
                    If IsDBNull(.Rows(0)("TienChi")) Then
                        _TienChi = 0
                    Else
                        _TienChi = .Rows(0)("TienChi")
                    End If
                    .Rows(0)("ConLai") = ds.Tables(0).Rows(0)(0) + _TienThu - _TienChi


                    For i As Integer = 1 To .Rows.Count - 1
                        .Rows(i)("STT") = i + 1
                        If IsDBNull(.Rows(i)("TienThu")) Then
                            _TienThu = 0
                        Else
                            _TienThu = .Rows(i)("TienThu")
                        End If
                        If IsDBNull(.Rows(i)("TienChi")) Then
                            _TienChi = 0
                        Else
                            _TienChi = .Rows(i)("TienChi")
                        End If

                        .Rows(i)("ConLai") = .Rows(i - 1)("ConLai") + _TienThu - _TienChi
                    Next
                    _ConLai = ds.Tables(1).Rows(ds.Tables(1).Rows.Count - 1)("ConLai")
                End With
            Else
                _ConLai = 0
            End If

            gdvSoTienMat.DataSource = ds.Tables(1)

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If xtraTab.SelectedTabPage Is tabChi Then
            LoadChi()
        ElseIf xtraTab.SelectedTabPage Is tabThu Then
            LoadThu()
        Else
            LoadSoTM()
        End If

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

    Private Sub xtraTab_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtraTab.SelectedPageChanged
        If e.Page Is tabChi Then
            chkLocChiPhi.Visibility = XtraBars.BarItemVisibility.Always
            btNhapChiPhiTuExcel.Visibility = XtraBars.BarItemVisibility.Always
        Else
            chkLocChiPhi.Visibility = XtraBars.BarItemVisibility.Never
            btNhapChiPhiTuExcel.Visibility = XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub chkLocChiPhi_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocChiPhi.CheckedChanged
        If chkLocChiPhi.Checked Then
            chkLocChiPhi.Glyph = My.Resources.Checked
        Else
            chkLocChiPhi.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub gdvSoTienMatCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvSoTienMatCT.CustomSummaryCalculate
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
        f.Text = "Cập nhật phiếu thu " & gdvThuCT.GetFocusedRowCellValue("SoPhieuT") '.ToString.Substring(3, 7)
        f.PhieuThu = gdvThuCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        f.PhieuThuCT = gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString
        f.ThuNH = False
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            f.btGhi.Enabled = False
            f.btThem.Enabled = False
        End If
        f.Show()
        gdvThuCT.FocusedRowHandle = _Index

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
End Class
