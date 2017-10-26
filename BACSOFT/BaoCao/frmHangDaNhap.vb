Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmHangDaNhap
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmDangVeCanXuat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btfilterTakecare.Enabled = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btKetXuat.Visibility = XtraBars.BarItemVisibility.Never
            gdvDaNhapCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            colLoaiDN.Visible = False

        Else
            gdvDaNhapCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            colLoaiDN.Visible = True

        End If

    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
            btfilterTakecare.EditValue = Convert.ToInt32(TaiKhoan)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub loadDSTenVT(ByVal HangSX As Object, ByVal NhomVT As Object)
        Dim sqltb As String = ""

        Dim sql As String = ""
        If HangSX Is Nothing And NhomVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTenvattu FROM TENVATTU WHERE ID=-1 "

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            rcbTenVatTu.DataSource = tb

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhomVT(ByVal HangSX As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If HangSX Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTennhom FROM TENNHOM WHERE ID=-1"

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbNhomVT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbHangSX(ByVal NhomVT As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If NhomVT Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub cbTenVatTu_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTenVatTu.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTenVT.EditValue = Nothing
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            btFilterHangSX.EditValue = Nothing
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub rtbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbTakecare_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTakecare.EditValue = Nothing
        End If
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
    End Sub

#End Region

    Private Sub LoadDaNhap()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "

        sql &= " SELECT KHACHHANG.ttcMa,PHIEUNHAPKHO.SoPhieu,PHIEUNHAPKHO.SoPhieu AS SoPhieu2,PHIEUNHAPKHO.NgayThang,0 AS STT,  "
        sql &= "        TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,TENDONVITINH.Ten AS DVT,NHAPKHO.SoLuong,(NHAPKHO.DonGia * PHIEUNHAPKHO.TyGia) AS DonGia,"
        sql &= " 		(NHAPKHO.DonGia*NHAPKHO.SoLuong*PHIEUNHAPKHO.TyGia)AS ThanhTien,NHAPKHO.ChiPhi,(NHAPKHO.ChiPhi*NHAPKHO.SoLuong) AS TongChiPhi,(CASE NHAPKHO.NhapThue WHEN 1 THEN PHIEUNHAPKHO.TyGia*NHAPKHO.DonGia*NHAPKHO.SoLuong*NHAPKHO.MucThue/100 ELSE 0 END)TienThue,"
        sql &= " 		tblTienTe.Ten AS TienTe, PHIEUNHAPKHO.TyGia, NHAPKHO.MucThue,NHAPKHO.NhapThue,NHAPKHO.IDVatTu,"
        sql &= " 		NHAPKHO.ID AS IDNHAPKHO,NGDAT.Ten AS TakeCare,PHIEUNHAPKHO.SoPhieuDH,KHACHHANG.IDLinhVucSX"
        sql &= " FROM NHAPKHO "
        sql &= " 	INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.Sophieu = NHAPKHO.Sophieu AND CONVERT(Datetime,CONVERT(Nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " 	INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"
            sql &= " AND PHIEUDATHANG.IDTakeCare=" & btfilterTakecare.EditValue
        Else
            sql &= " 	LEFT JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"
        End If
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = NHAPKHO.IDVatTu"
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If
        sql &= " 	LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " 	LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " 	LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh=TENDONVITINH.ID"
        sql &= " 	INNER JOIN KHACHHANG ON PHIEUNHAPKHO.IDkhachhang = KHACHHANG.ID"

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        sql &= " 	LEFT JOIN NHANSU AS NGDAT ON PHIEUDATHANG.IDTakeCare = NGDAT.ID"
        sql &= " 	LEFT JOIN tblTienTe ON PHIEUNHAPKHO.TienTe=tblTienTe.ID"
        sql &= " ORDER BY PHIEUNHAPKHO.SoPhieu "



        AddParameterWhere("@LinhVuc", LoaiTuDien.LinhVucSX)
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LinhVuc "

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If Not ds Is Nothing Then
            With ds.Tables(0)
                If .Rows.Count > 0 Then
                    Dim _STT As Integer = 1
                    .Rows(0)("STT") = _STT
                    If .Rows(0)("IDLinhVucSX").ToString.Trim <> "" Then
                        Dim tb As DataTable = DataSourceDSFile(.Rows(0)("IDLinhVucSX").ToString, "IDLinhVucSX")
                        .Rows(0)("IDLinhVucSX") = ""
                        For j As Integer = 0 To tb.Rows.Count - 1
                            Dim rows() As DataRow = ds.Tables(1).Select("ID = " & tb.Rows(j)("IDLinhVucSX").ToString & "")
                            If Not rows Is Nothing And rows.Length > 0 Then
                                .Rows(0)("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                            End If
                        Next
                        .Rows(0)("IDLinhVucSX") = .Rows(0)("IDLinhVucSX").ToString.Trim
                    End If

                    For i As Integer = 1 To .Rows.Count - 1
                        If .Rows(i)("SoPhieu") <> .Rows(i - 1)("SoPhieu2") Then
                            _STT = 1
                        Else
                            _STT += 1
                        End If
                        .Rows(i)("STT") = _STT


                        If _STT <> 1 And chkRutGon.Checked Then
                            .Rows(i)("ttcMa") = DBNull.Value
                            .Rows(i)("SoPhieu") = DBNull.Value
                            .Rows(i)("TakeCare") = DBNull.Value
                            .Rows(i)("NgayThang") = DBNull.Value
                            .Rows(i)("SoPhieuDH") = DBNull.Value
                        End If

                        If .Rows(i)("IDLinhVucSX").ToString.Trim <> "" Then
                            Dim tb As DataTable = DataSourceDSFile(.Rows(i)("IDLinhVucSX").ToString, "IDLinhVucSX")
                            .Rows(i)("IDLinhVucSX") = ""
                            For j As Integer = 0 To tb.Rows.Count - 1
                                Dim rows() As DataRow = ds.Tables(1).Select("ID = " & tb.Rows(j)("IDLinhVucSX").ToString)
                                If Not rows Is Nothing And rows.Length > 0 Then
                                    .Rows(i)("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                                End If
                            Next
                            .Rows(i)("IDLinhVucSX") = .Rows(i)("IDLinhVucSX").ToString.Trim
                        End If

                    Next
                End If
            End With

            If chkRutGon.Checked Then
                colDaNNCC.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDaNNCC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDNSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDNSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDNSPDatHang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDNSPDatHang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False


                colDaNNCC.OptionsFilter.AllowAutoFilter = False
                colDaNNCC.OptionsFilter.AllowFilter = False
                colDNSoPhieu.OptionsFilter.AllowAutoFilter = False
                colDNSoPhieu.OptionsFilter.AllowFilter = False
                colDNSPDatHang.OptionsFilter.AllowAutoFilter = False
                colDNSPDatHang.OptionsFilter.AllowFilter = False
            Else
                colDaNNCC.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDaNNCC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True

                colDNSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDNSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDNSPDatHang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDNSPDatHang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True

                colDaNNCC.OptionsFilter.AllowAutoFilter = True
                colDaNNCC.OptionsFilter.AllowFilter = True
                colDNSoPhieu.OptionsFilter.AllowAutoFilter = True
                colDNSoPhieu.OptionsFilter.AllowFilter = True
                colDNSPDatHang.OptionsFilter.AllowAutoFilter = True
                colDNSPDatHang.OptionsFilter.AllowFilter = True
            End If

            gdvDaNhap.DataSource = ds.Tables(0)

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        If chkRutGon.Checked Then
            gdvDaNhapCT.ClearGrouping()
            gdvDaNhapCT.ClearSorting()
            colDaNNCC.OptionsFilter.AllowAutoFilter = False
            colDaNNCC.OptionsFilter.AllowFilter = False
            colDNSoPhieu.OptionsFilter.AllowAutoFilter = False
            colDNSoPhieu.OptionsFilter.AllowFilter = False
            colDNSPDatHang.OptionsFilter.AllowAutoFilter = False
            colDNSPDatHang.OptionsFilter.AllowFilter = False
        Else
            colDaNNCC.OptionsFilter.AllowAutoFilter = True
            colDaNNCC.OptionsFilter.AllowFilter = True
            colDNSoPhieu.OptionsFilter.AllowAutoFilter = True
            colDNSoPhieu.OptionsFilter.AllowFilter = True
            colDNSPDatHang.OptionsFilter.AllowAutoFilter = True
            colDNSPDatHang.OptionsFilter.AllowFilter = True
        End If

        LoadDaNhap()


    End Sub

    Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
        If chkRutGon.Checked = True Then
            chkRutGon.Glyph = My.Resources.Checked
        Else
            chkRutGon.Glyph = My.Resources.UnCheck
        End If
    End Sub


    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "VT DA NHAP " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"


        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvDaNhapCT, False)

                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub mTinhTrangVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVatTu.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag

        f._IDVatTu = gdvDaNhapCT.GetFocusedRowCellValue("IDVatTu")

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienNCC = False
                f._HienCGXK = False
            Else
                f._HienNCC = True
                f._HienCGXK = True
            End If
        Else
            f._HienCGXK = True
            f._HienNCC = True
            f._HienThongTinNX = True
        End If

        f.ShowDialog()
    End Sub


    Private Sub gdvDaNhapCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDaNhapCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvDaNhapCT.CalcHitInfo(gdvDaNhap.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menuDaXuat.ShowPopup(gdvDaNhap.PointToScreen(e.Location))
        End If
    End Sub


End Class
