Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmHangDaXuatTest
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
            gdvDaXuatCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            colDXLoaiDN.Visible = False
        Else
            gdvDaXuatCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            colDXLoaiDN.Visible = True
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

    Private Sub LoadDaXuat()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT *,(DonGia- ISNULL(GiaNhapTB,0)- ISNULL(ChiPhi,0) - ISNULL(ChietKhau/(1-KhauTru/100),0)) * SoLuong as LoiNhuan,Round(((DonGia-ISNULL(GiaNhapTB,0) - ISNULL(ChiPhi,0) - ISNULL(ChietKhau/(1-KhauTru/100),0))/(CASE ISNULL(GiaNhapTB,0) WHEN 0 THEN 1 ELSE ISNULL(GiaNhapTB,0) END))*100,2) as PTLoiNhuan  FROM"
        sql &= " (SELECT KHACHHANG.ttcMa,PHIEUXUATKHO.SoPhieu,PHIEUXUATKHO.SoPhieu AS SoPhieu2,PHIEUXUATKHO.NgayThang,0 AS STT,PHIEUXUATKHO.CongTrinh, PHIEUXUATKHO.Finish,  "
        sql &= "        TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,TENDONVITINH.Ten AS DVT,XUATKHO.SoLuong,(XUATKHO.DonGia*PHIEUXUATKHO.TyGia) AS DonGia,"
        sql &= " 		(XUATKHO.DonGia*XUATKHO.SoLuong*PHIEUXUATKHO.TyGia)AS ThanhTien,(CASE XUATKHO.XuatThue WHEN 1 THEN PHIEUXUATKHO.TyGia*XUATKHO.DonGia*XUATKHO.SoLuong*XuatKho.MucThue/100 ELSE 0 END)TienThue,"
        sql &= " 		tblTienTe.Ten AS TienTe, PHIEUXUATKHO.TyGia, XUATKHO.MucThue,XUATKHO.XuatThue,XUATKHO.IDVatTu, "
        sql &= " 		XUATKHO.ID AS IDXuatkho,NHANSU.Ten AS TakeCare,PHIEUXUATKHO.SoPhieuCG,KHACHHANG.IDLinhVucSX, "
        sql &= "        View_XuatKhoGiaNhapTB.GiaNhap as GiaNhapTB,(SELECT KhauTru FROM BANGCHAOGIA WHERE BANGCHAOGIA.SoPhieu=PHIEUXUATKHO.SoPhieuCG)KhauTru,"
        sql &= "        (SELECT ChietKhau FROM CHAOGIA WHERE CHAOGIA.ID=XUATKHO.IDChaoGia)ChietKhau,"
        sql &= "        (DonGia*XUATKHO.SoLuong*(ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0))/PHIEUXUATKHO.TienTruocThue)/XUATKHO.SoLuong as ChiPhi,"
        sql &= "        (CASE KHACHHANG.NhomKH WHEN 1 THEN N'Thương mại, Chế tạo máy, Tích hợp …' WHEN 2 THEN  N'END User' ELSE '' END)NhomKH,"
        sql &= "        (CASE WHEN KHACHHANG.CapKH IS null THEN Convert(nvarchar,CapKH) ELSE N'Cấp ' +  Convert(nvarchar, KHACHHANG.CapKH) END)CapKH"
        sql &= " FROM XUATKHO "
        sql &= " 	INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = XUATKHO.Sophieu AND Convert(datetime, CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUXUATKHO.IDKhachhang=" & btfilterMaKH.EditValue
        End If
        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND PHIEUXUATKHO.IDTakeCare=" & btfilterTakecare.EditValue
        End If
        sql &= "    LEFT JOIN View_XuatKhoGiaNhapTB ON View_XuatKhoGiaNhapTB.IDVatTu=XUATKHO.IDVatTu AND XUATKHO.SoPhieu=View_XuatKhoGiaNhapTB.SoPhieu "

        sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = XUATKHO.IDVatTu"
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
        sql &= " 	INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDkhachhang = KHACHHANG.ID"

        'If Not btfilterTakecare.EditValue Is Nothing Then
        '    sql &= " AND PHIEUXUATKHO.IDTakeCare=" & btfilterTakecare.EditValue
        'End If
        sql &= " 	INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakeCare = NHANSU.ID"
        sql &= " 	LEFT JOIN tblTienTe ON PHIEUXUATKHO.TienTe=tblTienTe.ID "
        sql &= " )tbl"
        sql &= " ORDER BY SoPhieu "

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
                            .Rows(i)("SoPhieuCG") = DBNull.Value
                        End If

                        If .Rows(i)("IDLinhVucSX").ToString.Trim <> "" Then
                            Dim tb As DataTable = DataSourceDSFile(.Rows(i)("IDLinhVucSX").ToString, "IDLinhVucSX")
                            .Rows(i)("IDLinhVucSX") = ""
                            For j As Integer = 0 To tb.Rows.Count - 1
                                Dim rows() As DataRow = ds.Tables(1).Select("ID = " & tb.Rows(j)("IDLinhVucSX").ToString & "")
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
                colDXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

                colDXMaKH.OptionsFilter.AllowAutoFilter = False
                colDXMaKH.OptionsFilter.AllowFilter = False
                colDXSoPhieu.OptionsFilter.AllowAutoFilter = False
                colDXSoPhieu.OptionsFilter.AllowFilter = False
                colDXSPChaoGia.OptionsFilter.AllowAutoFilter = False
                colDXSPChaoGia.OptionsFilter.AllowFilter = False
            Else
                colDXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True

                colDXMaKH.OptionsFilter.AllowAutoFilter = True
                colDXMaKH.OptionsFilter.AllowFilter = True
                colDXSoPhieu.OptionsFilter.AllowAutoFilter = True
                colDXSoPhieu.OptionsFilter.AllowFilter = True
                colDXSPChaoGia.OptionsFilter.AllowAutoFilter = True
                colDXSPChaoGia.OptionsFilter.AllowFilter = True
            End If

            gdvDaXuat.DataSource = ds.Tables(0)


            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        'If chkRutGon.Checked Then
        '    gdvDaXuatCT.ClearGrouping()
        '    gdvDaXuatCT.ClearSorting()

        'Else
        '    colDXSoPhieu.GroupIndex = 0

        'End If
        LoadDaXuat()


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

        If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
        fCNChaoGia.SPChaoGia = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieuCG")
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
        If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

        TrangThai.isUpdate = True
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.PhieuXK = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieu2")
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

        saveFile.FileName = "VT DA XUAT " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvDaXuatCT, False)

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

        f._IDVatTu = gdvDaXuatCT.GetFocusedRowCellValue("IDVatTu")
        f._HienThongTinNX = True
        f._HienCGXK = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienNCC = False
            Else
                f._HienNCC = True
            End If
        Else
            f._HienNCC = True

        End If
        f.ShowDialog()
    End Sub

    Private Sub gdvDaXuatCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDaXuatCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvDaXuatCT.CalcHitInfo(gdvDaXuat.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menuDaXuat.ShowPopup(gdvDaXuat.PointToScreen(e.Location))
        End If
    End Sub


End Class
