Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports System.Linq
Imports BACSOFT.HoaDonGTGT

Public Class frmThuChiNH2
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0

    Private Sub frmThuChiNH_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
        sql &= " SELECT convert(bit,0)chon, THUNH.SoPhieuT, 0 AS STT, THUNH.ID,NgayThangVS,(N'CK ' + THUNH.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,THUNH.DienGiai,"
        sql &= " 	THUNH.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,TaiKhoanDen AS TaiKhoan,"
        sql &= " (CASE PhieuTC0 WHEN N'000000000' THEN N'' ELSE N'CG ' + PhieuTC0 END) PhieuTC0,"
        sql &= " (CASE PhieuTC1 WHEN N'000000000' THEN N'' ELSE N'XK ' + PhieuTC1 END) PhieuTC1,"
        sql &= " (SELECT SoCT FROM CHUNGTU WHERE ID = THUNH.IdChungTu)PhieuNopTien, "

        sql &= " (SELECT Top 1 SoCT FROM CHUNGTU WHERE refId = THUNH.ID)SoCT, "
        sql &= " (SELECT Top 1 Id FROM CHUNGTU WHERE refId = THUNH.ID)IdCT "

        sql &= " FROM THUNH"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=THUNH.IDKh"

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

    Public Sub LoadUNC()
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
        sql &= "        (CASE WHEN MucDich IN (210, 228) THEN N'ĐH '+PhieuTC1 WHEN MucDich IN (200, 224, 244, 235, 230) THEN N'CG '+PhieuTC1 "
        sql &= "            WHEN MucDich = 205 THEN (CASE UNC.ChiPhiNhap WHEN 1 THEN N'NK '+PhieuTC1 ELSE N'XK '+PhieuTC1 END)"
        sql &= "        ELSE PhieuTC1 END) "
        sql &= "    END )PhieuTC1,"
        sql &= "    (SELECT SoCT FROM CHUNGTU WHERE ID = UNC.IdChungTu)PhieuUNC, "
        sql &= "    (SELECT TOP 1 a.SoHD FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.LoaiCT = 2 AND a.LoaiCT2 = 5 AND b.GhiChuKhac = UNC.SoPhieuT AND b.ButToan = 1)SoHD, "
        sql &= " (SELECT Top 1 SoCT FROM CHUNGTU WHERE refId = UNC.ID)SoCT, "
        sql &= " (SELECT Top 1 Id FROM CHUNGTU WHERE refId = UNC.ID)IdCT "
        sql &= " FROM UNC"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=UNC.IDKh"

        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=UNC.TienTe"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=UNC.MucDich"
        If chkLocChiPhi.Checked Then
            sql &= " AND MUCDICHTHUCHI.ChiPhiMatDi=1 "
        End If
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,UNC.NgayThang,103),103)  BETWEEN @TuNgay AND @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND UNC.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND rtrim(ltrim(UNC.TaiKhoanDi))='" & cbSoTK.EditValue & "'"
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

    Public Sub LoadSoTienGui()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " SELECT (ISNULL((SELECT SUM(SoTien) FROM THUNH WHERE THUNH.NgayThangCT<@TuNgay "
            sql &= " AND rtrim(ltrim(TaiKhoanDen))='" & cbSoTK.EditValue & "'"
            sql &= " ),0) "
            sql &= " + ISNULL((SELECT SUM(SoTien) FROM CHI WHERE CHI.NgayThangCT<@TuNgay AND MucDich=211"
            sql &= " AND rtrim(ltrim(MaTK))='" & cbSoTK.EditValue & "'"
            sql &= " ),0)"
            sql &= " 		- ISNULL((SELECT SUM(SoTien) FROM UNC WHERE UNC.NgayThang<@TuNgay"
            sql &= " AND rtrim(ltrim(TaiKhoanDi))='" & cbSoTK.EditValue & "'"
            sql &= " ),0)"
            sql &= " 		- ISNULL((SELECT SUM(SoTien) FROM THU WHERE THU.NgayThangCT<@TuNgay AND MucDich=101"
            sql &= " AND rtrim(ltrim(MaTK))='" & cbSoTK.EditValue & "'"
            sql &= " ),0)"
            sql &= " )AS DauKy"
        Else
            sql &= " SELECT (ISNULL((SELECT SUM(SoTien) FROM THUNH WHERE THUNH.NgayThangCT<@TuNgay),0) "
            sql &= " 		+ ISNULL((SELECT SUM(SoTien) FROM CHI WHERE CHI.NgayThangCT<@TuNgay AND MucDich=211),0)"
            sql &= " 		- ISNULL((SELECT SUM(SoTien) FROM UNC WHERE UNC.NgayThang<@TuNgay),0)"
            sql &= " 		- ISNULL((SELECT SUM(SoTien) FROM THU WHERE THU.NgayThangCT<@TuNgay AND MucDich=101),0)"
            sql &= " )AS DauKy"
        End If

        sql &= "   SELECT STT, tb.ID,NgayThangVS,SoPhieu,NgayThangCT,IDKh,DienGiai,"
        sql &= " 	(CASE THUCHI WHEN 0 THEN tb.SoTien ELSE NULL END)TienThu,(CASE THUCHI WHEN 1 THEN tb.SoTien ELSE NULL END)TienChi,0.0 AS ConLai,"
        sql &= " 	tblTienTe.Ten AS TienTe,THUCHI,KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich,TaiKhoan"
        sql &= " FROM "
        sql &= " (SELECT 0 AS STT, THUNH.ID,NgayThangVS,(N'CK ' + THUNH.SoPhieu) AS SoPhieu,NgayThangCT,THUNH.IDKh,THUNH.DienGiai,"
        sql &= " 	THUNH.SoTien,TienTe,MucDich,Convert(bit,0) AS THUCHI,TaiKhoanDen AS TaiKhoan"
        sql &= " FROM THUNH"
        sql &= " UNION ALL "
        sql &= " SELECT 0 AS STT, UNC.ID,NgayThang AS NgayThangVS,(N'UNC ' + UNC.SoPhieu) AS SoPhieu,NgayThang AS NgayThangCT,UNC.IDKh,UNC.DienGiai,"
        sql &= " 	UNC.SoTien,TienTe,MucDich,Convert(bit,1) AS THUCHI,TaiKhoanDi AS TaiKhoan"
        sql &= " FROM UNC"
        sql &= " UNION ALL"
        sql &= " SELECT 0 AS STT, THU.ID,NgayThangVS,(N'RT ' + THU.SoPhieu) AS SoPhieu,NgayThangCT,THU.IDKh,THU.DienGiai,"
        sql &= " 	THU.SoTien,TienTe,MucDich,Convert(bit,1) AS THUCHI,MaTK AS TaiKhoan"
        sql &= " FROM THU"
        sql &= " WHERE THU.MucDich=101"
        sql &= " UNION ALL"
        sql &= " SELECT 0 AS STT, CHI.ID,NgayThangVS,(N'NT ' + CHI.SoPhieu) AS SoPhieu,NgayThangCT,CHI.IDKh,CHI.DienGiai,"
        sql &= " 	CHI.SoTien,TienTe,MucDich,Convert(bit,0) AS THUCHI,MaTK AS TaiKhoan"
        sql &= " FROM CHI"
        sql &= " WHERE CHI.MucDich=211"
        sql &= " )tb"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=tb.IDKh"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=tb.MucDich"
        sql &= " INNER JOIN tblTienTe ON tblTienTe.ID = tb.TienTe"
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,tb.NgayThangCT,103),103)  BETWEEN @TuNgay AND @DenNgay"
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND rtrim(ltrim(tb.TaiKhoan))='" & cbSoTK.EditValue & "'"
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

            gdvSoTienGui.DataSource = ds.Tables(1)

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If xtraTab.SelectedTabPage Is tabChi Then
            LoadUNC()
        ElseIf xtraTab.SelectedTabPage Is tabThu Then
            LoadThuNH()
        Else
            LoadSoTienGui()
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

    Private Sub gdvChiCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvUNCCT.RowCellStyle
        On Error Resume Next
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "MucDich" Then
            If gdvUNCCT.GetRowCellValue(e.RowHandle, "ChiPhiMatDi") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub xtraTab_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtraTab.SelectedPageChanged
        If e.Page Is tabChi Then
            chkLocChiPhi.Visibility = XtraBars.BarItemVisibility.Always
        Else
            chkLocChiPhi.Visibility = XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub chkLocChiPhi_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocChiPhi.CheckedChanged
        If chkLocChiPhi.Checked Then
            chkLocChiPhi.Glyph = My.Resources.Checked
        Else
            chkLocChiPhi.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub gdvSoTienMatCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvSoTienGuiCT.CustomSummaryCalculate
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
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            f.btGhi.Enabled = False
            f.btThem.Enabled = False
        End If
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

    Private Sub mnuChuyenBenThueNopTien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenBenThueNopTien.ItemClick

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



    Private Sub gdvThuNHCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvThuNHCT.CustomDrawCell
        Try
            If Not gdvThuNHCT.GetRowCellValue(e.RowHandle, "IdCT") Is DBNull.Value And e.Column.FieldName = "SoCT" And e.RowHandle >= 0 Then
                e.Appearance.BackColor = Color.LightPink
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gdvUNCCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvUNCCT.CustomDrawCell
        Try
            If Not gdvUNCCT.GetRowCellValue(e.RowHandle, "IdCT") Is DBNull.Value And e.Column.FieldName = "SoCT" And e.RowHandle >= 0 Then
                e.Appearance.BackColor = Color.LightPink
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub mnuChuyenSangBenThueUNC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenSangBenThueUNC.ItemClick
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


    Private Sub mnuChuyenCacPhieuDaChon1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenCacPhieuDaChon1.ItemClick
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
            For i As Integer = 0 To gdvThuNHCT.RowCount - 1
                gdvThuNHCT.SetRowCellValue(i, "chon", False)
            Next
            mnuChonBoChonTatCa1.Tag = False
            ShowAlert("Đã chuyển thành công " & arrPhieu.Count & " phiếu sang bên thuế!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadThuNH()
        End Try


    End Sub

    Private Sub mnuChonBoChonTatCa1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChonBoChonTatCa1.ItemClick
        Dim arrPhieu As New List(Of String)
        If mnuChonBoChonTatCa1.Tag Then
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
        mnuChonBoChonTatCa1.Tag = Not mnuChonBoChonTatCa1.Tag
    End Sub

    Private Sub gdvThuNHCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvThuNHCT.RowCellClick
        If e.Column.FieldName = "chon" Then
            gdvThuNHCT.SetRowCellValue(e.RowHandle, "chon", Not gdvThuNHCT.GetRowCellValue(e.RowHandle, "chon"))
        End If
    End Sub


    Private Sub mnuChuyenCacPhieuDaChon2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenCacPhieuDaChon2.ItemClick
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
            mnuChonBoChonTatCa1.Tag = False
            ShowAlert("Đã chuyển thành công " & arrPhieu.Count & " phiếu sang bên thuế!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadThuNH()
        End Try
    End Sub

    Private Sub mnuChonBoChonTatCa2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChonBoChonTatCa2.ItemClick
        Dim arrPhieu As New List(Of String)
        If mnuChonBoChonTatCa2.Tag Then
            For i As Integer = 0 To gdvUNCCT.RowCount - 1
                If arrPhieu.IndexOf(gdvUNCCT.GetRowCellValue(i, "SoPhieuT")) >= 0 Then
                    gdvUNCCT.SetRowCellValue(i, "chon", False)
                Else
                    arrPhieu.Add(gdvThuNHCT.GetRowCellValue(i, "SoPhieuT"))
                    gdvUNCCT.SetRowCellValue(i, "chon", True)
                End If
            Next
        Else
            For i As Integer = 0 To gdvUNCCT.RowCount - 1
                gdvUNCCT.SetRowCellValue(i, "chon", False)
            Next
        End If
        mnuChonBoChonTatCa2.Tag = Not mnuChonBoChonTatCa2.Tag
    End Sub


    Private Sub gdvUNCCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvUNCCT.RowCellClick
        If e.Column.FieldName = "chon" Then
            gdvUNCCT.SetRowCellValue(e.RowHandle, "chon", Not gdvUNCCT.GetRowCellValue(e.RowHandle, "chon"))
        End If
        '5,6 
    End Sub


End Class
