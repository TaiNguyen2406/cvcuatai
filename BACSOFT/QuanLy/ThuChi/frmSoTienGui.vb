Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmSoTienGui
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0

    Private Sub frmSoTienGui_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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
        LoadSoTienGui()
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


    Private Sub gdvSoTienMatCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvSoTienGuiCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ConLai" Then
                e.TotalValue = _ConLai
            End If
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


End Class
