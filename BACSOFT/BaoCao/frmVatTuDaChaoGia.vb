Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports System.Runtime.InteropServices
Imports BACSOFT.TAI
Public Class frmVatTuDaChaoGia
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmBCChaoGia_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadcbHangSX(Nothing, Nothing)
        LoadDaChaoGiaNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btKetXuat.Visibility = XtraBars.BarItemVisibility.Never
            colLoaiDN.Visible = False
            gdvCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            colGiaGoc.Visible = False
        Else
            colLoaiDN.Visible = True
            gdvCT.GroupFooterShowMode = XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            colGiaGoc.Visible = True
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
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

    Private Sub LoadDaChaoGiaNhomVT(ByVal HangSX As Object, ByVal TenVT As Object)
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
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT where 1=1 "
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
        'Tai
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            If btfilterTakecare.EditValue <> TaiKhoan Then
                sql &= " and ID in (select IDHangSX from PhatTrienSanPham where Thang=CONVERT(char(2),getdate(), 101)+'/'+convert(nvarchar(5),datepart(year,getdate())) and IDPhuTrach =" & TaiKhoan & ")"
            End If

        End If
        'Tai
        sql &= " ORDER BY Ten"
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
            LoadDaChaoGiaNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
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
        LoadDaChaoGiaNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDaChaoGiaNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
    End Sub

#End Region

    Private Sub LoadDaChaoGia()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " Set DATEFORMAT DMY "
        sql &= " Select KHACHHANG.ttcMa,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.SoPhieu As SoPhieu2,BANGCHAOGIA.NgayThang,0 As STT,BANGCHAOGIA.CongTrinh,BANGCHAOGIA.TrangThai As TrangThaiChinh,BANGCHAOGIA.IDTakeCare, "
        sql &= "        TENVATTU.Ten As TenVT,TENHANGSANXUAT.Ten As HangSX,VATTU.Model,TENDONVITINH.Ten As DVT,CHAOGIA.SoLuong,(CHAOGIA.DonGia*BANGCHAOGIA.TyGia) As DonGia,"
        sql &= " 		(CHAOGIA.DonGia*CHAOGIA.SoLuong*BANGCHAOGIA.TyGia)As ThanhTien,(Case CHAOGIA.XuatThue When 1 Then BANGCHAOGIA.TyGia*CHAOGIA.DonGia*CHAOGIA.SoLuong*CHAOGIA.MucThue/100 Else 0 End)TienThue,"
        sql &= " 		tblTienTe.Ten As TienTe, BANGCHAOGIA.TyGia, CHAOGIA.MucThue,CHAOGIA.XuatThue,CHAOGIA.IDVatTu,KHACHHANG.IDLinhVucSX,CHAOGIA.ChietKhau, "
        sql &= " 		CHAOGIA.ID As IDCHAOGIA,NHANSU.Ten As TakeCare,tblTuDien.NoiDung As TrangThai, (Case When CHAOGIA.TrangThai >2 Then BANGCHAOGIA.NgayHuy When CHAOGIA.TrangThai=2 Then NgayNhan Else NULL End)NgayNhanHuy,"
        sql &= "        ISNULL(ISNULL("
        sql &= "        (Select     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu And Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((Select TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (Select     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu And Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((Select TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia) * CHAOGIA.SoLuong As GiaGoc, "
        sql &= " (Case VATTU.XuatThue1 "
        sql &= "	When 0 Then "
        sql &= "		(Case VATTU.DonGia1 "
        sql &= "			When 0 Then 0 "
        sql &= "				Else"
        sql &= "        ROUND(((CHAOGIA.DonGia - CHAOGIA.ChietKhau) / (VATTU.DonGia1 * TTVT.TyGia)) * 100, 2)"
        sql &= "			End) "
        sql &= "	Else"
        sql &= "		(Case VATTU.DonGia1 "
        sql &= "			When 0 Then 0 "
        sql &= "				Else"
        sql &= "        ROUND(((CHAOGIA.DonGia - CHAOGIA.ChietKhau) / ((VATTU.DonGia1 * TTVT.TyGia) / (100 + VATTU.MucThue1))) * 100, 2)"
        sql &= "			End) "
        sql &= "	End) As PTGiaBan"
        '---tai
        sql &= " , KHACHHANG.ID As IDKhachHang"
        sql &= ", CAST(Case When CHAOGIA .ID  Not In(Select idchaogia from HaiQuan_ChiTietLamHaiQuan inner join CHAOGIA  On idchaogia =CHAOGIA .id where Soluong =(Select SUM (SoLuongLamHQ ) from HaiQuan_ChiTietLamHaiQuan where idchaogia =CHAOGIA .id )) Then 0 Else 1 End As bit) As lamhaiquan"
        sql &= ", CAST(Case When CHAOGIA .ID  Not In(Select idchaogia from HaiQuan_ChiTietLamHaiQuan inner join CHAOGIA  On idchaogia =CHAOGIA .id where Soluong =(Select SUM (SoLuongLamHQ ) from HaiQuan_ChiTietLamHaiQuan where idchaogia =CHAOGIA .id )) Then 0 Else 1 End As bit) As lamhaiquan2"
        sql &= ", CAST(Case When CHAOGIA .ID  Not In(Select idchaogia from HaiQuan_ChiTietLamHaiQuan) Then 0 Else 1 End As bit) As lamhaiquan3"
        '---tai
        sql &= " FROM CHAOGIA "
        sql &= " 	INNER JOIN BANGCHAOGIA On BANGCHAOGIA.Sophieu = CHAOGIA.Sophieu "

        If cbTrangThai.EditValue = "Tất cả" Then
            sql &= " And Convert(datetime,CONVERT(Nvarchar,BANGCHAOGIA.NgayThang,103),103) BETWEEN @TuNgay And @DenNgay  "
        ElseIf cbTrangThai.EditValue = "Đã xác nhận" Then
            sql &= " And CHAOGIA.TrangThai =2  And Convert(datetime,CONVERT(Nvarchar,BANGCHAOGIA.NgayNhan,103),103) BETWEEN @TuNgay And @DenNgay  "
        Else
            sql &= " And CHAOGIA.TrangThai >2 And Convert(datetime,CONVERT(Nvarchar,BANGCHAOGIA.NgayHuy,103),103) BETWEEN @TuNgay And @DenNgay  "
        End If
        sql &= "    INNER JOIN tblTuDien On CHAOGIA.TrangThai=tblTuDien.Ma And Loai=2 "
        sql &= " 	INNER JOIN VATTU On VATTU.ID = CHAOGIA.IDVatTu"
        If btFilterMaVT.EditValue <> "" Then
            sql &= " And VATTU.Model Like '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        Else
            'Tai
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
                If btfilterTakecare.EditValue <> TaiKhoan Then
                    sql &= " and VATTU.IDHangSanxuat in (select IDHangSX from PhatTrienSanPham where Thang=CONVERT(char(2),getdate(), 101)+'/'+convert(nvarchar(5),datepart(year,getdate())) and IDPhuTrach =" & TaiKhoan & ")"
                End If

            End If
            'Tai
        End If
        sql &= " 	LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " 	LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " 	LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh=TENDONVITINH.ID"
        sql &= " 	INNER JOIN KHACHHANG ON BANGCHAOGIA.IDkhachhang = KHACHHANG.ID"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDTakeCare=" & btfilterTakecare.EditValue
        End If

        sql &= " 	INNER JOIN NHANSU ON BANGCHAOGIA.IDTakeCare = NHANSU.ID"
        sql &= " 	LEFT JOIN tblTienTe ON BANGCHAOGIA.TienTe=tblTienTe.ID "
        sql &= " 	INNER JOIN tblTienTe AS TTVT ON VATTU.TienTe1=TTVT.ID "
        'tai
        If _message <> 0 And hienthipopup = True Then
            sql &= " where CHAOGIA .ID  not in(select idchaogia from HaiQuan_ChiTietLamHaiQuan ) "
        End If
        'tài
        sql &= " ORDER BY BANGCHAOGIA.SoPhieu "

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
                colMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

                colSoPhieu.OptionsFilter.AllowAutoFilter = False
                colSoPhieu.OptionsFilter.AllowFilter = False
                colMaKH.OptionsFilter.AllowAutoFilter = False
                colMaKH.OptionsFilter.AllowFilter = False
            Else
                colMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDXNgayThang.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colSoPhieu.OptionsFilter.AllowAutoFilter = True
                colSoPhieu.OptionsFilter.AllowFilter = True
                colMaKH.OptionsFilter.AllowAutoFilter = True
                colMaKH.OptionsFilter.AllowFilter = True
            End If

            gdv.DataSource = ds.Tables(0)


            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        'If chkRutGon.Checked Then
        '    gdvCT.ClearGrouping()
        '    gdvCT.ClearSorting()
        'Else
        '    colSoPhieu.GroupIndex = 0
        'End If
        'tài 
        If _message <> 0 And hienthipopup = True Then
            tgidlamhaiquan = _message
            Dim ck = " select IDKhachhang  from HaiQuan_LamHaiQuan where id=" & tgidlamhaiquan.ToString()
            tgKH = ExecuteSQLScalar(ck)
            btfilterMaKH.EditValue = tgKH
            cbTrangThai.EditValue = "Đã xác nhận"
            barBtnLamHQ.Enabled = False
            btnBoSung.Visibility = XtraBars.BarItemVisibility.Always
        End If

        If _message = 0 Or hienthipopup = False Then
            tgidlamhaiquan = -1
            tgKH = -1
            barBtnLamHQ.Enabled = False
            btnBoSung.Visibility = XtraBars.BarItemVisibility.Never
            chaogia = Nothing
        End If
        '   ReDim Preserve chaogiaCT(0)
        d = 0
        'tai
        LoadDaChaoGia()
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
        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        'TrangThai.isUpdate = True
        'fCNXuatKho = New frmCNXuatKho
        'fCNXuatKho.PhieuXK = gdvCT.GetFocusedRowCellValue("SoPhieu2")
        'fCNXuatKho.Tag = Me.Parent.Tag
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '    fCNXuatKho.btCal.Enabled = False
        '    fCNXuatKho.btGhi.Enabled = False
        '    fCNXuatKho.btChuyenXK.Enabled = False
        '    fCNXuatKho.mXuatKho.Enabled = False
        'End If
        'fCNXuatKho.ShowDialog()
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "VT DA CG " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

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
        ' btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, 1, 1)
    End Sub

    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        'Dim f As New frmTinhTrangVT
        'f.Tag = Me.Parent.Tag
        'f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        'f.ShowDialog()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub


        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mXemCG_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemCG.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("TrangThaiChinh") = TrangThaiChaoGia.DaXacNhan Or gdvCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isUpdate = True
        fCNChaoGia.Tag = deskTop.mChaoGia.Name
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNChaoGia.Show()

    End Sub

    Private Sub mThuGon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuGon.ItemClick
        colMaKH.Width = 90
        colCXTenVT.Width = 70
        colCXHangSX.Width = 50
        colMaHang.Width = 60
        colCXPhuTrach.Width = 60
        colGiaGoc.Width = 60
        colPTBL.Width = 30
        colDVT.VisibleIndex = 14
    End Sub
#Region "Tài"
    Protected Shared _message As Integer = 0
    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property
    Public Structure _ChaoGia
        Public IDKhachhang As Integer
        Public IDnguoilap As Integer
        Public tgchuyentuchaogia As Date
        Public idtinhtrang_haiquan As Integer
    End Structure
    Public Structure _ChaoGiaCT
        '   Public idlamhaiquan As Integer
        Public idchaogia As Integer
        Public tendexuat As String
    End Structure
    Public tgKH As Integer = -1
    Public tgidlamhaiquan As Integer = -1
    Public chaogia As _ChaoGia = New _ChaoGia
    '   Public chaogiaCT As _ChaoGiaCT() = New _ChaoGiaCT() {}
    Public d As Integer = 0
    Private Sub riCeHaiQuan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles riCeLamHaiQuan.EditValueChanged
        gdvCT.PostEditor()
        gdvCT.UpdateCurrentRow()
    End Sub

    Private Sub barBtnLamHQ_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barBtnLamHQ.ItemClick

        If isHienThiT = False Then
            If tgidlamhaiquan = -1 Then
                tgKH = gdvCT.GetFocusedRowCellValue("IDKhachHang")
                Dim query = "select count(IDKhachhang) from HaiQuan_LamHaiQuan where  MONTH(tgchuyensanghaiquan )=MONTH (getdate())  and IDKhachhang=" & tgKH.ToString()
                Dim lan = ExecuteSQLScalar(query)
                AddParameter("@IDKhachhang", chaogia.IDKhachhang)
                AddParameter("@IDnguoilap", chaogia.IDnguoilap)
                AddParameter("@tgchuyentuchaogia", Now)
                AddParameter("@idtinhtrang_haiquan", chaogia.idtinhtrang_haiquan)
                '  AddParameter("@tgchuyensanghaiquan", Today)
                AddParameter("@lan", lan + 1)
                If doInsert("HaiQuan_LamHaiQuan") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If

            If tgKH <> -1 Then
                If tgKH <> gdvCT.GetFocusedRowCellValue("IDKhachHang") Then
                    ShowAlert("Chỉ được chọn một khách hàng")
                Else
                    ' gdvCGCT.UpdateCurrentRow()
                    ' Dim csgt As New _ChaoGiaCT in  
                    'For Each cgct As _ChaoGiaCT In chaogiaCT
                    '    If tgidlamhaiquan = -1 Then
                    '        AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                    '    Else
                    '        AddParameter("@idlamhaiquan", tgidlamhaiquan)
                    '    End If

                    '    AddParameter("@idchaogia", cgct.idchaogia)
                    '    AddParameter("@tendexuat", cgct.tendexuat)
                    '    If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                    '        ShowBaoLoi(LoiNgoaiLe)
                    '    End If
                    'Next
                    For i As Integer = 0 To gdvCT.DataRowCount - 1
                        If gdvCT.GetRowCellValue(i, "lamhaiquan") = True And gdvCT.GetRowCellValue(i, "lamhaiquan2") = False Then
                            If tgidlamhaiquan = -1 Then
                                AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                            Else
                                AddParameter("@idlamhaiquan", tgidlamhaiquan)
                            End If

                            AddParameter("@idchaogia", gdvCT.GetRowCellValue(i, "IDCHAOGIA"))
                            AddParameter("@tendexuat", gdvCT.GetRowCellValue(i, "TenVT")) 'IDVatTu
                            ' AddParameter("@IDVatTuHQ", gdvCT.GetRowCellValue(i, "IDVatTu"))
                            If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        End If
                    Next i


                End If
            End If
            '     ReDim Preserve chaogiaCT(0)
            d = 0
            Dim frm = New frmDSYCLamHQ
            frm.Text = "Danh sách thiết bị làm hải quan"
            Dim id As Integer = 0
            If tgidlamhaiquan = -1 Then
                id = ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan where idtinhtrang_haiquan=0 order by id desc")
            Else
                id = tgidlamhaiquan
            End If
            If id = 0 Then
                frm.Message = 0
            Else
                frm.Message = id
            End If
            isHienThiT = True
            frm.Name = "frmDSYCLamHQpopup"
            frm.Show()
            frm.WindowState = FormWindowState.Maximized
            frm.btnTaiLai.PerformClick()
            '     btnBoSung.Visibility = XtraBars.BarItemVisibility.Always
        Else
            ShowCanhBao("Có hải quan đang làm, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If


        Dim row = gdvCT.FocusedRowHandle
        LoadDaChaoGia()
        gdvCT.FocusedRowHandle = row
    End Sub

    Private Sub btnBoSung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBoSung.ItemClick
        If tgKH <> -1 Then
            If tgKH <> gdvCT.GetFocusedRowCellValue("IDKhachHang") Then
                ShowAlert("Chỉ được chọn một khách hàng")
            Else
                If d > 0 Then
                    'For Each cgct As _ChaoGiaCT In chaogiaCT
                    '    If tgidlamhaiquan = -1 Then
                    '        AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                    '    Else
                    '        AddParameter("@idlamhaiquan", tgidlamhaiquan)
                    '    End If

                    '    AddParameter("@idchaogia", cgct.idchaogia)
                    '    AddParameter("@tendexuat", cgct.tendexuat)
                    '    If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                    '        ShowBaoLoi(LoiNgoaiLe)

                    '    End If
                    'Next
                    For i As Integer = 0 To gdvCT.DataRowCount - 1
                        If gdvCT.GetRowCellValue(i, "lamhaiquan") = True And gdvCT.GetRowCellValue(i, "lamhaiquan2") = False Then
                            If tgidlamhaiquan = -1 Then
                                AddParameter("@idlamhaiquan", ExecuteSQLScalar("select top 1 id from HaiQuan_LamHaiQuan order by id desc"))
                            Else
                                AddParameter("@idlamhaiquan", tgidlamhaiquan)
                            End If

                            AddParameter("@idchaogia", gdvCT.GetRowCellValue(i, "IDCHAOGIA"))
                            AddParameter("@tendexuat", gdvCT.GetRowCellValue(i, "TenVT"))
                            If doInsert("HaiQuan_ChiTietLamHaiQuan") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        End If
                    Next i
                    '   ReDim Preserve chaogiaCT(0)
                    d = 0
                    btnBoSung.Enabled = False
                End If

            End If
        End If
        If isHienThiT = True Then
            Application.OpenForms("frmDSYCLamHQpopup").WindowState = FormWindowState.Maximized
            CType(Application.OpenForms("frmDSYCLamHQpopup"), frmDSYCLamHQ).btnTaiLai.PerformClick()
        Else
            Application.OpenForms("frmDSYCLamHQ").WindowState = FormWindowState.Maximized
            Dim d = deskTop.tabMain.TabPages.Count - 1
            Dim i As Integer
            For i = 0 To d
                If i > d Then
                    Exit For
                End If
                If deskTop.tabMain.TabPages(i).Tag = "btnDSYCLamHQ" Then
                    deskTop.tabMain.SelectedTabPage = deskTop.tabMain.TabPages(i)
                    CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDSYCLamHQ).btnTaiLai.PerformClick()
                End If

                If deskTop.tabMain.TabPages(i).Controls(0).Tag = "frmVatTuDaChaoGia" Then
                    deskTop.tabMain.TabPages.Remove(deskTop.tabMain.TabPages(i))
                    hienthipopup = False
                    i = i - 1
                    d = d - 1
                End If
            Next i
        End If

    End Sub
    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle

        If gdvCT.GetRowCellValue(e.RowHandle, "lamhaiquan3") = True Then
            If e.Column.FieldName = "lamhaiquan" Then
                e.Appearance.BackColor = Color.Pink
            End If
        End If
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.Name = "gColLamHQ" Then
            If gdvCT.GetFocusedRowCellValue("lamhaiquan2") = False Then
                If tgKH = -1 Then
                    tgKH = gdvCT.GetFocusedRowCellValue("IDKhachHang")
                    chaogia.IDKhachhang = tgKH
                    chaogia.IDnguoilap = CType(TaiKhoan, Int32)
                    chaogia.tgchuyentuchaogia = Today
                    chaogia.idtinhtrang_haiquan = 0
                End If

                If tgKH <> -1 Then
                    If tgKH <> gdvCT.GetFocusedRowCellValue("IDKhachHang") Then
                        ShowCanhBao("Chỉ được chọn một khách hàng")
                    Else
                        If gdvCT.GetFocusedRowCellValue("lamhaiquan") = False Then
                            gdvCT.SetFocusedRowCellValue("lamhaiquan", True)
                            '    ReDim Preserve chaogiaCT(d)
                            '     chaogiaCT(d).idchaogia = gdvCT.GetFocusedRowCellValue("IDCHAOGIA")
                            '      chaogiaCT(d).tendexuat = gdvCT.GetFocusedRowCellValue("TenVT")
                            d += 1
                        Else
                            gdvCT.SetFocusedRowCellValue("lamhaiquan", False)
                            d -= 1
                            '    ReDim Preserve chaogiaCT(d - 1)
                        End If
                    End If
                End If
            Else
                ShowCanhBao("đã làm hải quan không hủy được nữa")
            End If

            If _message = 0 Or hienthipopup = False Then
                If d > 0 And isHienThiT = False Then
                    barBtnLamHQ.Enabled = True
                    btnBoSung.Enabled = False
                Else
                    If d > 0 Then
                        barBtnLamHQ.Enabled = False
                        btnBoSung.Enabled = True
                    Else
                        If isHienThiT = False Then
                            tgKH = -1
                        End If
                        barBtnLamHQ.Enabled = False
                        btnBoSung.Enabled = False
                    End If

                End If
            Else
                If d > 0 Then
                    btnBoSung.Enabled = True
                Else
                    btnBoSung.Enabled = False
                End If
            End If
        End If

    End Sub
    Private Sub Control1_HandleDestroyed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        If isHienThiT = True Then
            ShowCanhBao("Đóng form Danh sách thiết bị làm hải quan trước khi làm tiếp")
        End If
        _message = 0
    End Sub
    'Tai
    Private Sub btfilterTakecare_EditValueChanged(sender As Object, e As EventArgs) Handles btfilterTakecare.EditValueChanged
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub
    'Tai
#End Region
End Class
