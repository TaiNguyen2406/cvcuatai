Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmVatTuDaDatHang
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmVatTuDaDatHang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btKetXuat.Visibility = XtraBars.BarItemVisibility.Never
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btfilterTakecare.Enabled = False
        End If
        btfilterTakecare.EditValue = Convert.ToInt32(TaiKhoan)
    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
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

    Private Sub LoadDaDatHang()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT KHACHHANG.ttcMa,PHIEUDATHANG.SoPhieu,PHIEUDATHANG.SoPhieu AS SoPhieu2,PHIEUDATHANG.NgayDat AS NgayThang,0 AS STT,"
        sql &= "        TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,TENDONVITINH.Ten AS DVT,DATHANG.SoLuong,(DATHANG.DonGia*PHIEUDATHANG.TyGia) AS DonGia,"
        sql &= " 		(DATHANG.DonGia*DATHANG.SoLuong*PHIEUDATHANG.TyGia)AS ThanhTien,(CASE DATHANG.NhapThue WHEN 1 THEN PHIEUDATHANG.TyGia*DATHANG.DonGia*DATHANG.SoLuong*DATHANG.MucThue/100 ELSE 0 END)TienThue,"
        sql &= " 		tblTienTe.Ten AS TienTe, PHIEUDATHANG.TyGia, DATHANG.MucThue,DATHANG.NhapThue,DATHANG.IDVatTu, "
        sql &= " 		DATHANG.ID AS IDDATHANG,NHANSU.Ten AS TakeCare,"
        sql &= "        (CASE VATTU.DONGIA1 WHEN 0 THEN 0 ELSE ((DATHANG.DonGia/VATTU.DonGia1)*(PHIEUDATHANG.TyGia/TyGiaVT.TyGia))*100 END) PTGiaNhap,"
        sql &= "        (CASE PHIEUDATHANG.PheDuyet WHEN 0 THEN N'Chưa duyệt' ELSE N'Đã duyệt' END) TrangThai,"
        sql &= " (CASE VATTU.XuatThue1 "
        sql &= "	WHEN 0 THEN "
        sql &= "		(CASE VATTU.DonGia1 "
        sql &= "			WHEN 0 THEN 0 "
        sql &= "				ELSE"
        sql &= "        ROUND((DATHANG.DonGia / (VATTU.DonGia1 * TTVT.TyGia)) * 100, 2)"
        sql &= "			END) "
        sql &= "	ELSE"
        sql &= "		(CASE VATTU.DonGia1 "
        sql &= "			WHEN 0 THEN 0 "
        sql &= "				ELSE"
        sql &= "        ROUND((DATHANG.DonGia / ((VATTU.DonGia1 * TTVT.TyGia) / (100 + VATTU.MucThue1))) * 100, 2)"
        sql &= "			END) "
        sql &= "	END) AS PTGiaNhap"
        sql &= " FROM DATHANG "
        sql &= " 	INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.Sophieu = DATHANG.Sophieu AND convert(datetime,Convert(nvarchar,PHIEUDATHANG.NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay "
        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDTakeCare=" & btfilterTakecare.EditValue
        End If
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = DATHANG.IDVatTu"
        sql &= " 	INNER JOIN tblTienTe AS TyGiaVT ON VATTU.TienTe1=TyGiaVT.ID "
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
        sql &= " 	INNER JOIN KHACHHANG ON PHIEUDATHANG.IDkhachhang = KHACHHANG.ID"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        sql &= " 	INNER JOIN NHANSU ON PHIEUDATHANG.IDTakeCare = NHANSU.ID"
        sql &= " 	LEFT JOIN tblTienTe ON PHIEUDATHANG.TienTe=tblTienTe.ID "
        sql &= " 	INNER JOIN tblTienTe AS TTVT ON VATTU.TienTe1=TTVT.ID "
        sql &= " ORDER BY PHIEUDATHANG.SoPhieu "

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                Dim _STT As Integer = 1
                tb.Rows(0)("STT") = _STT
                For i As Integer = 1 To tb.Rows.Count - 1
                    If tb.Rows(i)("SoPhieu") <> tb.Rows(i - 1)("SoPhieu2") Then
                        _STT = 1
                    Else
                        _STT += 1
                    End If
                    tb.Rows(i)("STT") = _STT

                    If _STT <> 1 And chkRutGon.Checked Then
                        tb.Rows(i)("ttcMa") = DBNull.Value
                        tb.Rows(i)("SoPhieu") = DBNull.Value
                        tb.Rows(i)("TakeCare") = DBNull.Value
                        tb.Rows(i)("NgayThang") = DBNull.Value
                    End If
                Next
            End If
            If Not chkRutGon.Checked Then
                colMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

                colMaKH.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                colMaKH.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.False
            Else
                colMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True

                colMaKH.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True
                colMaKH.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsFilter.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsFilter.AllowFilter = DevExpress.Utils.DefaultBoolean.True
            End If

            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If chkRutGon.Checked Then
            gdvCT.ClearGrouping()
            gdvCT.ClearSorting()
        Else
            colSoPhieu.GroupIndex = 0
        End If
        LoadDaDatHang()
    End Sub

    Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
        If chkRutGon.Checked = True Then
            chkRutGon.Glyph = My.Resources.Checked
        Else
            chkRutGon.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNChaoGia.Tag = Me.Parent.Tag
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            fCNChaoGia.gdvVTCT.OptionsBehavior.ReadOnly = True
            fCNChaoGia.gdvCongTrinhCT.OptionsBehavior.ReadOnly = True
            fCNChaoGia.btGhi.Enabled = False
            fCNChaoGia.tabChuyenMa.PageVisible = False
            fCNChaoGia.btTinhToan.Enabled = False
        End If

        fCNChaoGia.Show()
    End Sub

    Private Sub mXemXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemXuatKho.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        TrangThai.isUpdate = True
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.PhieuXK = gdvCT.GetFocusedRowCellValue("SoPhieu2")
        fCNXuatKho.Tag = Me.Parent.Tag
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            fCNXuatKho.btCal.Enabled = False
            fCNXuatKho.btGhi.Enabled = False
            fCNXuatKho.btChuyenXK.Enabled = False
            fCNXuatKho.mXuatKho.Enabled = False
        End If
        fCNXuatKho.ShowDialog()
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT DA DH " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)

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
        f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub
End Class
