Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors

Public Class frmChaoGia
    Public _exit As Boolean = False
    Public index As Integer

    Private Sub frmChaoGia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 500"
        cbLoai.EditValue = "Tất cả"
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btNhanVien.Enabled = False
        End If
    End Sub

#Region "Load DS chào giá"

    Public Sub LoadDS()

        ShowWaiting("Đang tải danh sách chào giá ...")

        Dim sql As String = ""
        If cbTieuChi.EditValue = "Top 500" Then
            sql = " SELECT TOP 500 BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        Else
            sql = " SELECT BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        End If

        sql &= "    BANGCHAOGIA.DaTamUng,BANGCHAOGIA.TamUngConLai, "

        sql &= "	BANGCHAOGIA.Masodathang,BANGCHAOGIA.IDTakecare,BANGCHAOGIA.TenDuan,BANGCHAOGIA.txtKhac,(CASE NhanKS WHEN 0 THEN 'KD' WHEN 1 THEN 'KT' ELSE '' END)NhanKS,"
        sql &= "	BANGCHAOGIA.txtTkhac,BANGCHAOGIA.TienTruocthue,BANGCHAOGIA.Tienthue,"
        sql &= "	BANGCHAOGIA.TienChietkhau,BANGCHAOGIA.Tienthucthu,BANGCHAOGIA.TienLoiNhuan,"
        sql &= "	Ngaynhan,Ngaygiao,Ngayhuy,Tiente,NHANSU.Ten AS TenUser,BANGCHAOGIA.IDNgd,"
        sql &= "	NHANSU_1.Ten AS TenNgd,NHANSU_2.Ten AS TenTakeCare,tblTienTe.Ten AS TenTienTe,BANGCHAOGIA.IDtakecare,"
        sql &= "	Congtrinh,( SELECT NoiDung FROM tblTuDien WHERE Loai=2 AND Ma= BANGCHAOGIA.Trangthai) AS Trangthai,Khautru, BANGCHAOGIA.FileDinhKem,BANGCHAOGIA.Trangthai as IDTrangThai"
        sql &= "    ,SoPO,TGKDCanXuat,GhiChuKD,PHUTRACHHD.Ten as PhuTrachHD, PHUTRACHCT.Ten as PhuTrachCT"
        sql &= " FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT OUTER JOIN NHANSU ON BANGCHAOGIA.IDUSer=NHANSU.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_1 ON BANGCHAOGIA.IDNgd=NHANSU_1.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_2 ON BANGCHAOGIA.IDtakecare=NHANSU_2.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS PHUTRACHHD ON BANGCHAOGIA.IDPhuTrachKyHD=PHUTRACHHD.ID AND PHUTRACHHD.Noictac=74"
        sql &= "        LEFT OUTER JOIN NHANSU AS PHUTRACHCT ON BANGCHAOGIA.IDPhuTrachCT=PHUTRACHCT.ID AND PHUTRACHCT.Noictac=74"

        sql &= "        LEFT OUTER JOIN tblTienTe ON BANGCHAOGIA.Tiente=tblTienTe.ID"
        sql &= " WHERE 1=1 "

        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If cbLoai.EditValue = "Công trình" Then
            sql &= " AND Congtrinh=1 "
        ElseIf cbLoai.EditValue = "Thường" Then
            sql &= " AND Congtrinh=0 "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            If TaiKhoan <> btNhanVien.EditValue Then
                sql &= " AND BANGCHAOGIA.Pub=1 "
            End If
            sql &= " AND BANGCHAOGIA.IDTakecare= " & btNhanVien.EditValue
        Else
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                sql &= " AND (BANGCHAOGIA.Pub=1 or BANGCHAOGIA.IDTakeCare= " & TaiKhoan & ")"
            End If
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang= " & cbKH.EditValue
        End If

        If cbTieuChi.EditValue = "Top 500" Then
            sql &= " ORDER BY Sophieu DESC"
        End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        If Not dt Is Nothing Then
            gdv.DataSource = dt
            If Not gdvCT.FocusedRowHandle < 0 Then

                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Congtrinh"))
                LoadDSLSGiaoDich(gdvCT.GetFocusedRowCellValue("Sophieu"))
            Else
                gdvCG.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        If gdvCT.GetFocusedRowCellValue("Congtrinh") Then
            mXemChaoGiaChoKH.PerformClick()
        End If


        CloseWaiting()



    End Sub

    Public Sub LoadDS2(ByVal MaSoDatHang As Object)
        ShowWaiting("Đang tải danh sách chào giá ...")

        Dim sql As String = ""
        sql = " SELECT BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        sql &= "	BANGCHAOGIA.Masodathang,BANGCHAOGIA.TenDuan,BANGCHAOGIA.txtKhac,"
        sql &= "	BANGCHAOGIA.txtTkhac,BANGCHAOGIA.TienTruocthue,BANGCHAOGIA.Tienthue,"
        sql &= "	BANGCHAOGIA.TienChietkhau,BANGCHAOGIA.Tienthucthu,BANGCHAOGIA.TienLoiNhuan,"
        sql &= "	Ngaynhan,Ngaygiao,Ngayhuy,Tiente,NHANSU.Ten AS TenUser,"
        sql &= "	NHANSU_1.Ten AS TenNgd,NHANSU_2.Ten AS TenTakeCare,tblTienTe.Ten AS TenTienTe,"
        sql &= "	Congtrinh,( SELECT NoiDung FROM tblTuDien WHERE Loai=2 AND Ma= BANGCHAOGIA.Trangthai) AS Trangthai,Khautru, BANGCHAOGIA.FileDinhKem,BANGCHAOGIA.Trangthai as IDTrangThai,"
        sql &= "    SoPO,TGKDCanXuat,GhiChuKD"
        sql &= " FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT OUTER JOIN NHANSU ON BANGCHAOGIA.IDUSer=NHANSU.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_1 ON BANGCHAOGIA.IDNgd=NHANSU_1.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_2 ON BANGCHAOGIA.IDtakecare=NHANSU_2.ID"
        sql &= "        LEFT OUTER JOIN tblTienTe ON BANGCHAOGIA.Tiente=tblTienTe.ID"
        sql &= " WHERE BANGCHAOGIA.Masodathang=" & MaSoDatHang

        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        If Not dt Is Nothing Then
            gdv.DataSource = dt
            If Not gdvCT.FocusedRowHandle < 0 Then

                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Congtrinh"))
                LoadDSLSGiaoDich(gdvCT.GetFocusedRowCellValue("Sophieu"))
            Else
                gdvCG.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        If gdvCT.GetFocusedRowCellValue("Congtrinh") Then
            mXemChaoGiaChoKH.PerformClick()
        End If


        CloseWaiting()

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object, ByVal CongTrinh As Object)

        '    For i As Integer = 0 To pMenuPhu.ItemLinks.Count - 2
        If CongTrinh Then
            '  pMenuPhu.ItemLinks(i).Visible = True
            Dim FilterString, FilterDisplayText As String
            Dim FilterIf As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("SoPhieu", gdvCT.GetFocusedRowCellValue("Sophieu") & "CT", BinaryOperatorType.Equal))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "Chào giá cho khách"
            FilterIf = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvCGCT.Columns("SoPhieu").FilterInfo = FilterIf
            If pMenuPhu.ItemLinks.Count > 0 Then
                For i As Integer = 0 To pMenuPhu.ItemLinks.Count - 3
                    pMenuPhu.ItemLinks(i).Visible = True
                Next
                pMenuPhu.ItemLinks(pMenuPhu.ItemLinks.Count - 1).Visible = True
            End If

        Else
            If pMenuPhu.ItemLinks.Count > 0 Then
                For i As Integer = 0 To pMenuPhu.ItemLinks.Count - 3
                    pMenuPhu.ItemLinks(i).Visible = False
                Next
                pMenuPhu.ItemLinks(pMenuPhu.ItemLinks.Count - 1).Visible = False
            End If
            '   pMenuPhu.ItemLinks(i).Visible = False
            gdvCGCT.Columns("SoPhieu").ClearFilter()
        End If
        '  Next

        Dim sql As String = ""
        sql &= " DECLARE @tb table"
        sql &= " ("
        sql &= " 	SoPhieu nvarchar(20),"
        sql &= " 	IDVatTu int,"
        sql &= " 	NoiDung NVARCHAR(4000),"
        sql &= " 	TenHang NVARCHAR(50),"
        sql &= " 	Model NVARCHAR(50),"
        sql &= " 	ThongSo	NVARCHAR(1000),"
        sql &= " 	TenDVT NVARCHAR(50),"
        sql &= " 	SoLuong Float,"
        sql &= " 	DonGia float,"
        sql &= "    ChietKhauPT float,"
        sql &= "    ChietKhau float,"
        sql &= " 	ThanhTien Float,"
        sql &= " 	XuatThue bit,"
        sql &= " 	MucThue tinyint,"
        sql &= " 	slTon Float,"
        sql &= " 	TienTe nvarchar(20),"
        sql &= " 	TyGia float,"
        sql &= " 	HangTon bit,"
        sql &= " 	AZ int,"
        sql &= " 	Canxuat float,"
        sql &= " 	GhiChu nvarchar(500),"
        sql &= " 	ID int,"
        sql &= " 	IDXK int"
        sql &= " )"
        sql &= " INSERT INTO @tb(SoPhieu,IDVatTu,NoiDung,TenHang,Model,ThongSo,TenDVT,SoLuong,DonGia,ChietKhau,ThanhTien,XuatThue,MucThue,slTon,TienTe,TyGia,HangTon,AZ,Canxuat,GhiChu,ID,IDXK)"
        sql &= " SELECT CHAOGIA.Sophieu,CHAOGIA.IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso,"
        sql &= " TENDONVITINH.Ten AS TenDVT,CHAOGIA.Soluong,ISNULL(CHAOGIA.Dongia,0)DonGia,ISNULL(CHAOGIA.Chietkhau,0) ChietKhau,(CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,CHAOGIA.Xuatthue,CHAOGIA.Mucthue,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        sql &= " tblTienTe.Ten AS TenTienTe,CHAOGIA.TyGia, VATTU.HangTon, ISNULL(CHAOGIA.AZ,0)AZ,(select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS Canxuat,CHAOGIA.GhiChu,CHAOGIA.ID,XUATKHO.ID as IDXK"
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON CHAOGIA.Tiente=tblTienTe.ID"
        sql &= " LEFT JOIN XUATKHO ON XUATKHO.IDChaoGia=CHAOGIA.ID"
        sql &= " WHERE CHAOGIA.Sophieu=N'" & SoPhieu & "' OR CHAOGIA.Sophieu=N'" & SoPhieu & "CT'"
        sql &= " ORDER BY Sophieu,AZ "

        sql &= " INSERT INTO @tb(SoPhieu,NoiDung,TenHang,TenDVT,SoLuong,DonGia,ThanhTien,MucThue,XuatThue,ChietKhau,TienTe,TyGia,AZ,ID)"
        sql &= " SELECT CHAOGIAAUX.Sophieu,Noidung,HangSx, Donvi,Soluong, ISNULL(Dongia,0),(Dongia*Soluong)ThanhTien,Mucthue,Xuatthue,ISNULL(Chietkhau,0),tblTienTe.Ten AS TenTienTe,CHAOGIAAUX.TyGia, ISNULL(CHAOGIAAUX.AZ,0)AZ,0"
        sql &= " FROM CHAOGIAAUX LEFT OUTER JOIN tblTienTe ON CHAOGIAAUX.Tiente=tblTienTe.ID "
        sql &= " WHERE Sophieu=N'" & SoPhieu & "' OR Sophieu=N'" & SoPhieu & "CT'"
        sql &= " ORDER BY AZ "
        sql &= " SELECT * FROm @tb"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            With dt
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If .Rows(i)("DonGia") = 0 Then
                        .Rows(i)("ChietKhauPT") = 0
                    Else
                        .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100, 2)
                    End If
                Next
            End With
            gdvCG.DataSource = dt

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadDSLSGiaoDich(ByVal _SoPhieu As Object)
        AddParameterWhere("@SP", _SoPhieu)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ROW_NUMBER() OVER(ORDER BY ThoiGian,ID ) as STT,* FROM BANGCHAOGIA_LSGIAODICH WHERE SoPhieu=@SP ORDER BY ThoiGian DESC,ID DESC")
        If Not dt Is Nothing Then
            gdvLSGD.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadrCbNhanVien()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not dt Is Nothing Then
            rCbNhanVien.DataSource = dt
            btNhanVien.EditValue = Convert.ToInt32(TaiKhoan)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadrCbKH()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG order by ttcMa")
        If Not dt Is Nothing Then
            rcbKH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        'TrangThai.isAddNew = True
        'fCNChaoGia = New frmCNChaoGia
        'fCNChaoGia.Tag = Me.Tag
        'fCNChaoGia.cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        'fCNChaoGia.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "IDTrangThai") = TrangThaiChaoGia.DaXacNhan Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        End If

        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        index = gdvCT.FocusedRowHandle
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isUpdate = True
        fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("Congtrinh")
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")

        fCNChaoGia.Tag = Me.Parent.Tag
        fCNChaoGia.Show()
    End Sub

    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        gdvCT.ClearColumnsFilter()
        LoadDS()
    End Sub

    Private Sub gdvCT_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If gdvCT.FocusedRowHandle < 0 Then
            loadDSYCChiTiet("-1", True)
            LoadDSLSGiaoDich("-1")
            Exit Sub
        End If

        'If 
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Congtrinh"))
        LoadDSLSGiaoDich(gdvCT.GetFocusedRowCellValue("Sophieu"))
    End Sub

    Private Sub gdvCT_RowCellClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.Name = "Masodathang" Then
            If Not e.CellValue Is Nothing Then
                Dim psi As New ProcessStartInfo()
                With psi
                    .FileName = e.CellValue
                    .UseShellExecute = True
                End With
                Process.Start(psi)
            End If
        End If
    End Sub

    'Private Sub pMYCThemYC_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles pMYCThemYC.ItemClick
    '    If gdvCT.FocusedRowHandle < 0 Then Exit Sub
    '    gdvCGCT.AddNewRow()
    'End Sub

    'Private Sub pMYCBoYC_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles pMYCBoYC.ItemClick
    '    If gdvCGCT.FocusedRowHandle < 0 Then Exit Sub
    '    gdvCGCT.DeleteSelectedRows()

    'End Sub

    'Private Sub btLuuLai_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles pMYCLuuLai.ItemClick
    '    Try
    '        BeginTransaction()
    '        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
    '        If doDelete("YEUCAUDEN", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
    '        For i As Integer = 0 To gdvCGCT.RowCount - 1
    '            AddParameter("@Sophieu", gdvCGCT.GetRowCellValue(i, "Sophieu"))
    '            AddParameter("@Noidung", gdvCGCT.GetRowCellValue(i, "Noidung"))
    '            AddParameter("@Soluong", gdvCGCT.GetRowCellValue(i, "Soluong"))
    '            AddParameter("@Mucdocan", gdvCGCT.GetRowCellValue(i, "Mucdocan"))
    '            AddParameter("@IDVattu", gdvCGCT.GetRowCellValue(i, "IDVattu"))
    '            AddParameter("@IDHoithongtin", gdvCGCT.GetRowCellValue(i, "IDHoithongtin"))
    '            AddParameter("@IDChuyenma", gdvCGCT.GetRowCellValue(i, "IDChuyenma"))
    '            AddParameter("@IDHoigia", gdvCGCT.GetRowCellValue(i, "IDHoigia"))
    '            AddParameter("@Ngayhoithongtin", gdvCGCT.GetRowCellValue(i, "Ngayhoithongtin"))
    '            AddParameter("@Ngaychuyenma", gdvCGCT.GetRowCellValue(i, "Ngaychuyenma"))
    '            AddParameter("@Ngayhoigia", gdvCGCT.GetRowCellValue(i, "Ngayhoigia"))
    '            AddParameter("@Trangthai", gdvCGCT.GetRowCellValue(i, "Trangthai"))
    '            AddParameter("@Hoithongtin", gdvCGCT.GetRowCellValue(i, "Hoithongtin"))
    '            AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
    '            If doInsert("YEUCAUDEN") Is Nothing Then Throw New Exception(LoiNgoaiLe)
    '        Next

    '        ComitTransaction()
    '        ShowAlert("Đã lưu lại")

    '    Catch ex As Exception
    '        RollBackTransaction()
    '        ShowBaoLoi(ex.Message)
    '    End Try
    'End Sub

#Region "Xuất Excel, in bảng kê"

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        If chkDungFileCuaKhach.Checked Then
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT FileDinhKem FROM BANGYEUCAU WHERE Sophieu=" & gdvCT.GetFocusedRowCellValue("Masodathang"))
            If Not tb Is Nothing Then
                Dim tbFile As DataTable = DataSourceDSFile(tb.Rows(0)(0).ToString)
                Dim dr() As DataRow = tbFile.Select("File like '%" & "YC" & gdvCT.GetFocusedRowCellValue("Masodathang") & " FileYC " & "%'")
                If dr.Length = 1 Then
                    Utils.XuatExcel.CreateExcelFileChaoGiaFromYC(gdvCT.GetFocusedRowCellValue("Masodathang"), chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & dr(0)(0).ToString)
                    Exit Sub
                End If
            End If

            If ShowCauHoi("Không tìm thấy file yêu cầu trên hệ thống, bạn có muốn tìm file khác ?") Then
                Dim openFile As New OpenFileDialog
                openFile.Filter = "Excel File|*.xls;*.xlsx"
                If openFile.ShowDialog = DialogResult.OK Then
                    Utils.XuatExcel.CreateExcelFileChaoGiaFromYC(gdvCT.GetFocusedRowCellValue("Masodathang"), chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, openFile.FileName)
                End If
            End If

        Else
            Utils.XuatExcel.CreateExcelFileChaoGia(gdvCT.GetFocusedRowCellValue("Sophieu"), chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, chkN0.Checked, gdvCT.GetFocusedRowCellValue("Congtrinh"), chkGhiChu.Checked, gdvCT.GetFocusedRowCellValue("MaKH"))
            panelXuatExcel.Visible = False
        End If
    End Sub

    Private Sub btInBangKe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInBangKe.ItemClick
        If Not gdvCT.GetFocusedRowCellValue("Congtrinh") Then
            ShowCanhBao("Tính năng này chỉ áp dụng với chào giá công trình !")
            Exit Sub
        End If
        printFile.CongTrinh.BangKeVatTu(gdvCT.GetFocusedRowCellValue("Sophieu"))
    End Sub
#End Region

    Private Sub btSaoChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSaoChep.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If Not ShowCauHoi("Tạo chào giá mới với nội dung của chào giá được chọn ?") Then
            Exit Sub
        End If

        TrangThai.isCopy = True
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isCopy = True
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")
        fCNChaoGia.SPYeuCau = gdvCT.GetFocusedRowCellValue("Masodathang")
        fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("Congtrinh")
        fCNChaoGia.Tag = Me.Tag
        fCNChaoGia.ShowDialog()
    End Sub

    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            btNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKH.EditValue = Nothing
        End If
    End Sub

    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        Else
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        End If
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btThem.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btSua.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.C Then
            btSaoChep.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.Y Then
            XemYeuCauDen()
        End If
    End Sub

    Public Sub XemYeuCauDen()
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Masodathang").ToString <> "" Then
            deskTop.OpenTab("Yêu cầu đến", "KINHDOANH", New frmYeuCauDen, True, Nothing, "mYeuCauDen")
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDen).LoadDSYeuCau2(gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Masodathang"))
        End If

    End Sub

    Private Sub rcbXuatExcel_Popup(sender As System.Object, e As System.EventArgs) Handles rcbXuatExcel.Popup
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        If gdvCT.GetFocusedRowCellValue("Tiente") > 0 Then
            chkENG.Checked = True
            chkN2.Checked = True
        Else
            chkVIE.Checked = True
            chkN0.Checked = True
        End If

    End Sub

    Private Sub mXemChaoGiaChoKH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGiaChoKH.ItemClick
        Dim FilterString, FilterDisplayText As String
        Dim FilterIf As ColumnFilterInfo
        Dim BinaryFilter As CriteriaOperator
        BinaryFilter = New GroupOperator(New BinaryOperator("SoPhieu", gdvCT.GetFocusedRowCellValue("Sophieu") & "CT", BinaryOperatorType.Equal))
        FilterString = BinaryFilter.ToString()
        FilterDisplayText = "Chào giá cho khách"
        FilterIf = New ColumnFilterInfo(FilterString, FilterDisplayText)
        gdvCGCT.Columns("SoPhieu").FilterInfo = FilterIf
    End Sub

    Private Sub mXemChaoGiaDaXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGiaDaXuLy.ItemClick
        Dim FilterString, FilterDisplayText As String
        Dim FilterIf As ColumnFilterInfo
        Dim BinaryFilter As CriteriaOperator
        BinaryFilter = New GroupOperator(New BinaryOperator("SoPhieu", gdvCT.GetFocusedRowCellValue("Sophieu"), BinaryOperatorType.Equal))
        FilterString = BinaryFilter.ToString()
        FilterDisplayText = "Chào giá đã xử lý"
        FilterIf = New ColumnFilterInfo(FilterString, FilterDisplayText)
        gdvCGCT.Columns("SoPhieu").FilterInfo = FilterIf
    End Sub

    Private Sub mXemTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemTatCa.ItemClick
        gdvCGCT.Columns("SoPhieu").ClearFilter()
    End Sub

    Private Sub rcbFileDinhKem_Popup(sender As System.Object, e As System.EventArgs) Handles rcbFileDinhKem.Popup
        If _exit Then
            _exit = False
            Exit Sub
        End If
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()

    End Sub

    Private Sub rcbFileDinhKem_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rcbFileDinhKem.Closed
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next

        AddParameter("@FileDinhKem", _File)
        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
        If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CType(sender, PopupContainerEdit).EditValue = _File

        End If
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH")) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH"))
            End If
            ShowWaiting("Đang tải file lên máy chủ ...")
            For Each file In openFile.FileNames
                Try
                    path = "YC" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Masodathang") & " CG" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Sophieu") & " KD " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & path, 1)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)

                        End If
                    Else
                        System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            Impersonator.EndImpersonation()
            CloseWaiting()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()
            _exit = True
            SendKeys.Send("{F4}")

        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá file được chọn ?") Then
            Try
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Impersonator.EndImpersonation()
                gdvListFileCT.DeleteSelectedRows()
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If

    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub mXemThongTinYCDen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemThongTinYCDen.ItemClick
        XemYeuCauDen()
    End Sub

    Private Sub btFileLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btFileLienQuan.Click, mXemFileLienQuan.ItemClick
        Dim f As New frmFileLienQuan
        f.Tag = Me.Parent.Tag
        f.SoChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")
        f.SoYeuCau = gdvCT.GetFocusedRowCellValue("Masodathang")
        f.MaKH = gdvCT.GetFocusedRowCellValue("MaKH")
        f.ShowDialog()
    End Sub

    Private Sub mChuyenThanhCGKH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenThanhCGKH.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If Not gdvCT.GetFocusedRowCellValue("Congtrinh") Then
            ShowCanhBao("Chức năng này chỉ áp dụng đối với chào giá công trình lập bằng hệ thống cũ !")
            Exit Sub
        End If
        If ShowCauHoi("Chuyển toàn bộ mặt hàng của công trình này sang dạng chào giá cho khách, bạn có chắc là thao tác đúng không ?") Then
            Try
                BeginTransaction()
                AddParameter("@Canxuat", 0)
                AddParameter("@Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu") & "CT")
                AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
                If doUpdate("CHAOGIA", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                AddParameter("@Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu") & "CT")
                AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
                If doUpdate("CHAOGIAAUX", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                ComitTransaction()
                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Congtrinh"))
                ShowAlert("Đã chuyển thành công !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btTachChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTachChaoGia.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("Congtrinh") Then
            ShowCanhBao("Không sử dụng được tính năng này cho chào giá công trình !")
            Exit Sub
        End If

        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT COUNT(ID) FROM PHIEUXUATKHO WHERE SophieuCG=@SP")
        If Not dt Is Nothing Then
            If Convert.ToInt32(dt.Rows(0)(0)) > 0 Then
                ShowCanhBao("Chào giá đã lập xuất kho !")
                Exit Sub
            End If
        End If
        If Not ShowCauHoi("Tách chào giá ?") Then
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        fTachCG = New frmTachChaoGia
        'f       fTachCG.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("Congtrinh")
        fTachCG.SPChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")
        fTachCG.SPYeuCau = gdvCT.GetFocusedRowCellValue("Masodathang")
        'fCNChaoGia.Tag = Me.Parent.Tag
        fTachCG.ShowDialog()
        gdvCT.FocusedRowHandle = index
    End Sub

    'Private Sub mThuMucLuuTru_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuMucLuuTru.ItemClick
    '    If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
    '    Dim psi As New ProcessStartInfo()
    '    With psi
    '        .FileName = "explorer.exe"
    '        .Arguments = "/Select," & RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & gdvListFileCT.GetFocusedRowCellValue("File")
    '        .UseShellExecute = True
    '    End With
    '    Try
    '        Process.Start(psi)
    '    Catch ex As Exception
    '        ShowBaoLoi(ex.Message)
    '    End Try
    'End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvCGCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub

    Private Sub pMenuChinh_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChinh.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            mChuyenDoiYC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            mChuyenDoiYC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub mChuyenDoiYC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenDoiYC.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If Clipboard.GetText.ToString.Length <> 9 Then
            ShowCanhBao("Chưa chép số yêu cầu vào bộ nhớ đệm !")
            Exit Sub
        End If

        If ShowCauHoi("Bạn đang cập nhật số YC: " & Clipboard.GetText & " thay cho YC: " & gdvCT.GetFocusedRowCellValue("Masodathang") & " của chào giá: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " ? ") Then
            'ShowThongBao(Clipboard.GetText)
            BeginTransaction()
            Dim sql As String = ""
            sql &= " UPDATE BANGCHAOGIA SET MaSoDatHang='" & Clipboard.GetText.Trim & "'"
            sql &= " WHERE SoPhieu='" & gdvCT.GetFocusedRowCellValue("Sophieu").ToString.Trim & "'"

            sql &= " Update tblBaoCaoLichThiCong"
            sql &= " SET SoYC='" & Clipboard.GetText.Trim & "'"
            sql &= " WHERE SoYC='" & gdvCT.GetFocusedRowCellValue("Masodathang").ToString.Trim & "' AND SoCG='" & gdvCT.GetFocusedRowCellValue("Sophieu").ToString.Trim & "'"

            If ExecuteSQLNonQuery(sql) Is Nothing Then
                RollBackTransaction()
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ComitTransaction()
                ShowAlert("Đã cập nhật!")
                btXem.PerformClick()
            End If

        End If

    End Sub


    Private Sub gdvCGCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCGCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCGCT.CalcHitInfo(gdvCG.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenuPhu.ShowPopup(gdvCG.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub lbCNMauDH_Click(sender As System.Object, e As System.EventArgs) Handles lbCNMauDH.Click
        If ShowCauHoi("Khi cập nhật thì thông tin chữ ký cũ sẽ mất, bạn cần tạo lại chữ ký cho file chào giá." & vbCrLf & "Bạn có tiếp tục không ?") Then
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_ENG_N0.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N0.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_ENG_N0_L.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N0_L.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_ENG_N2_L.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N2_L.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_ENG_N0_L.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_ENG_N0_L.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_VIE_N0_L.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N0_L.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_VIE_N2_L.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N2_L.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_VIE_N0.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N0.xls", True)
            System.IO.File.Copy(ServerName & "\Excel$\BIEUMAU\CHAOGIA\_MAUCHAOGIA_VIE_N2.xls", Application.StartupPath & "\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N2.xls", True)
            ShowThongBao("Đã cập nhật mẫu file chào giá !")
        End If
    End Sub

    Private Sub mLapDuKienThanhToan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLapDuKienThanhToan.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Sophieu")
        f._PhaiTra = False
        f._Buoc1 = True
        f.ShowDialog()
    End Sub

    Private Sub mThongTinCanXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThongTinCanXuat.ItemClick

        If gdvCT.GetFocusedRowCellValue("IDTrangThai") = TrangThaiChaoGia.DaXacNhan Then
            pThoiGianXuat.Location = New Point(Cursor.Position.X, Cursor.Position.Y - 110)
            pThoiGianXuat.Visible = True
            AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT TGKDCanXuat,GhiChuKD FROM BANGCHAOGIA WHERE SoPhieu=@SP")
            If Not tb Is Nothing Then
                If IsDBNull(tb.Rows(0)(0)) Then
                    tbThoiGianXuat.EditValue = Now
                    tbGhiChu.EditValue = DBNull.Value
                Else
                    tbThoiGianXuat.EditValue = tb.Rows(0)(0)
                    tbGhiChu.EditValue = tb.Rows(0)(1)
                End If
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        Else
            ShowCanhBao("Chào giá chưa xác nhận ! ")
        End If

    End Sub

    Private Sub btHuy_Click(sender As System.Object, e As System.EventArgs) Handles btHuy.Click
        pThoiGianXuat.Visible = False
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        Dim tg As DateTime = GetServerTime()
        AddParameter("@GhiChuKD", tbGhiChu.EditValue)
        AddParameter("@TGKDCanXuat", tbThoiGianXuat.EditValue)
        AddParameter("@TGLapYCXuat", tg)
        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
        If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvCT.SetFocusedRowCellValue("TGKDCanXuat", tbThoiGianXuat.EditValue)
            gdvCT.SetFocusedRowCellValue("GhiChuKD", tbGhiChu.EditValue)
            'ThemThongBaoChoNV()
            ShowAlert("Đã cập nhật!")
            pThoiGianXuat.Visible = False
        End If
    End Sub

    Private Sub mChuyenThanhCGChoKhach_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenThanhCGChoKhach.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If Not gdvCT.GetFocusedRowCellValue("Congtrinh") Then
            ShowCanhBao("Chức năng này chỉ áp dụng đối với chào giá công trình!")
            Exit Sub
        End If
        If ShowCauHoi("Chuyển toàn bộ mặt hàng đã chọn của công trình này sang dạng chào giá đã XL, bạn có chắc là thao tác đúng không ?") Then
            Try
                BeginTransaction()
                For i As Integer = 0 To gdvCGCT.SelectedRowsCount - 1
                    If gdvCGCT.GetRowCellValue(gdvCGCT.GetSelectedRows(i), "ID") <> 0 Then
                        AddParameter("@Canxuat", 0)
                        AddParameter("@Sophieu", gdvCT.GetFocusedRowCellValue("Sophieu"))
                        AddParameterWhere("@IDCG", gdvCGCT.GetRowCellValue(gdvCGCT.GetSelectedRows(i), "ID"))
                        If doUpdate("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                Next


                ComitTransaction()
                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Congtrinh"))
                ShowAlert("Đã chuyển thành công !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btInDiaChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInDiaChi.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvCT.GetFocusedRowCellValue("IDTakecare")
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvCT.GetFocusedRowCellValue("IDNgd")
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=74 "
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=" & gdvCT.GetFocusedRowCellValue("IDKhachhang")
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmIn("In địa chỉ giao hàng")
            Dim rpt As New rptDiaChiGiaoHang
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenCTyGui.Text = ds.Tables(2).Rows(0)("Ten")
            rpt.lbDCGui.Text = "Đ/C: " & ds.Tables(2).Rows(0)("ttcDCGiaoHang").ToString
            rpt.lbCtyNhan.Text = ds.Tables(3).Rows(0)("Ten")
            rpt.lbDCNhan.Text = "Đ/C: " & ds.Tables(3).Rows(0)("ttcDCGiaoHang").ToString
            rpt.lbNguoiGui.Text = ds.Tables(0).Rows(0)("Ten") & " " & ds.Tables(0).Rows(0)("Mobile").ToString
            Try
                rpt.lbNguoiNhan.Text = ds.Tables(1).Rows(0)("Ten") & " " & ds.Tables(1).Rows(0)("Mobile").ToString
            Catch ex As Exception
                ShowBaoLoi("Không lấy được thông tin người nhận !")
            End Try

            rpt.CreateDocument()
            'f.PrintPreviewBarItem12.
            f.printControl.PrintingSystem = rpt.PrintingSystem

            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub mSuaQTGD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaQTGD.ItemClick
        If gdvLSGDCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim f As New frmChaoGia_LSGiaoDich

        f._ID = gdvLSGDCT.GetFocusedRowCellValue("ID")
        f._MaKH = gdvCT.GetFocusedRowCellValue("MaKH")
        f._SoPhieu = gdvCT.GetFocusedRowCellValue("Sophieu")
        f.tbThoiGian.EditValue = gdvLSGDCT.GetFocusedRowCellValue("ThoiGian")
        f.tbNoiDung.EditValue = gdvLSGDCT.GetFocusedRowCellValue("NoiDung")
        f.ShowDialog()
    End Sub

    Private Sub mThemQuaTrinhGD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemQuaTrinhGD.ItemClick, mThemQTGD.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmChaoGia_LSGiaoDich
        f._MaKH = gdvCT.GetFocusedRowCellValue("MaKH")
        f._SoPhieu = gdvCT.GetFocusedRowCellValue("Sophieu")
        f.ShowDialog()
    End Sub

    Private Sub gdvLSGDCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvLSGDCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvLSGDCT.CalcHitInfo(gdvLSGD.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenuLSGD.ShowPopup(gdvLSGD.PointToScreen(e.Location))
        End If
    End Sub
End Class
