Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraEditors
Imports BACSOFT.Utils

Public Class frmCongTrinhCanXuLy

    Public _exit As Boolean = False
    Public _FileCGKinhDoanh As String = ""
    Public _SPSaoChep As Object
    Private _countKH As Int32

    Private Sub frmChaoGiaCanXuLy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 500"
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
    End Sub

#Region "Load DS chào giá công trình cần xử lý"

    Public Sub LoadDS()
        ShowWaiting("Đang tải danh sách công trình ...")

        Dim sql As String = ""
        If cbTieuChi.EditValue = "Top 500" Then
            sql = " SELECT TOP 500 BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        Else
            sql = " SELECT BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        End If
        sql &= "	BANGCHAOGIA.Masodathang,BANGCHAOGIA.TenDuan,BANGCHAOGIA.txtKhac,TGThiCong,Ngaygiao,NgayNhan,"
        sql &= "	BANGCHAOGIA.txtTkhac,BANGCHAOGIA.TienTruocthue,BANGCHAOGIA.Tienthue,"
        sql &= "	BANGCHAOGIA.TienChietkhau,BANGCHAOGIA.Tienthucthu,BANGCHAOGIA.TienLoiNhuan,"
        sql &= "	Ngaynhan,Ngaygiao,Ngayhuy,BANGCHAOGIA.IDUser, IDNgXuLy,NgayXuLy, IDNgDuyet,NgayDuyet, "
        sql &= "	NHANSU_1.Ten AS TenNgd,BANGCHAOGIA.IDTakeCare,tblTienTe.Ten AS TienTe,(CASE NhanKS WHEN 0 THEN 'KD' WHEN 1 THEN 'KT' ELSE '' END)NhanKS,"
        sql &= "	Congtrinh,(Case XuLy WHEN 0 THEN N'Cần xử lý' WHEN 1 THEN N'Đã xử lý' END) AS Trangthai,Khautru,BANGCHAOGIA.FileDinhKem,"
        sql &= "    tblTuDien.NoiDung AS TrangThaiCG,BANGCHAOGIA.TrangThai as TTCG, BANGCHAOGIA.TienDo,NGUOINHAN.Ten AS NguoiNhanXL,ThoiGianNhanXL,PHUTRACHCT.Ten as PhuTrachCT"
        sql &= " FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_1 ON BANGCHAOGIA.IDNgd=NHANSU_1.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NGUOINHAN ON BANGCHAOGIA.IDNgNhanXL=NGUOINHAN.ID"
        sql &= "        LEFT JOIN NHANSU as PHUTRACHCT  ON PHUTRACHCT.ID=BANGCHAOGIA.IDPhuTrachCT"
        sql &= "        LEFT OUTER JOIN tblTienTe ON BANGCHAOGIA.Tiente=tblTienTe.ID"
        sql &= "        LEFT OUTER JOIN tblTuDien ON BANGCHAOGIA.Trangthai=tblTuDien.Ma AND Loai=2"
        sql &= " WHERE 1=1 "

        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,convert(nvarchar,BANGCHAOGIA.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        sql &= " AND Congtrinh=1 "

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDTakeCare= " & btNhanVien.EditValue
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
            Dim View As New DataView(dt)
            _countKH = View.ToTable(True, "MaKH").Rows.Count
            gdvCT.Columns("Sophieu").Caption = dt.Rows.Count.ToString & " CG"
            gdvCT.Columns("MaKH").Caption = _countKH & " KH"
            If Not gdvCT.FocusedRowHandle < 0 Then

                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Masodathang"))
            Else
                gdvCG.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        CloseWaiting()

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object, ByVal SoYC As Object)
        Dim sql As String = ""
        sql &= " DECLARE @tb table"
        sql &= " ("
        sql &= " 	SoPhieu nvarchar(15),"
        sql &= " 	IDVatTu int,"
        sql &= " 	TenVT NVARCHAR(4000),"
        sql &= " 	HangSX NVARCHAR(50),"
        sql &= " 	Model NVARCHAR(50),"
        sql &= " 	ThongSo	NVARCHAR(1000),"
        sql &= " 	DVT NVARCHAR(50),"
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
        sql &= " 	Canxuat float"
        sql &= " )"
        sql &= " INSERT INTO @tb(SoPhieu,IDVatTu,TenVT,HangSX,Model,ThongSo,DVT,SoLuong,DonGia,ChietKhau,ThanhTien,XuatThue,MucThue,slTon,TienTe,TyGia,HangTon,AZ,Canxuat)"
        sql &= " SELECT CHAOGIA.Sophieu,CHAOGIA.IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso,"
        sql &= " TENDONVITINH.Ten AS TenDVT,Soluong,ISNULL(CHAOGIA.Dongia,0)DonGia,ISNULL(CHAOGIA.Chietkhau,0) ChietKhau,(CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,CHAOGIA.Xuatthue,CHAOGIA.Mucthue,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        sql &= " tblTienTe.Ten AS TenTienTe,CHAOGIA.TyGia, VATTU.HangTon, ISNULL(CHAOGIA.AZ,0)AZ,(select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS Canxuat"
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON CHAOGIA.Tiente=tblTienTe.ID"
        sql &= " WHERE CHAOGIA.Sophieu=N'" & SoPhieu & "'"
        sql &= " ORDER BY AZ "

        sql &= " INSERT INTO @tb(SoPhieu,TenVT,HangSX,DVT,SoLuong,DonGia,ThanhTien,MucThue,XuatThue,ChietKhau,TienTe,TyGia,AZ)"
        sql &= " SELECT CHAOGIAAUX.Sophieu,Noidung,HangSx, TENDONVITINH.Ten,Soluong, ISNULL(Dongia,0),(Dongia*Soluong)ThanhTien,Mucthue,Xuatthue,ISNULL(Chietkhau,0),tblTienTe.Ten AS TenTienTe,CHAOGIAAUX.TyGia, ISNULL(CHAOGIAAUX.AZ,0)AZ"
        sql &= " FROM CHAOGIAAUX LEFT OUTER JOIN tblTienTe ON CHAOGIAAUX.Tiente=tblTienTe.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON CHAOGIAAUX.Donvi=TENDONVITINH.ID "
        sql &= " WHERE Sophieu=N'" & SoPhieu & "'"
        sql &= " ORDER BY AZ "
        sql &= " SELECT * FROm @tb"

        sql &= " SELECT ('')NVThucHien,('')NVKiemDuyetLan1,('')NVKiemDuyetLan2,tblBaoCaoLichThiCong.*,tblTuDien.NoiDung FROM tblBaoCaoLichThiCong "
        sql &= " LEFT OUTER JOIN tblTuDien ON tblTuDien.ID=tblBaoCaoLichThiCong.IDNoiDung AND tblTuDien.Loai= " & LoaiTuDien.NoiDungThiCong
        sql &= " WHERE GiaoViec=1 AND SoYC=N'" & SoYC & "'"
        sql &= " ORDER BY AZ "



        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            With dt.Tables(0)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If .Rows(i)("DonGia") = 0 Then
                        .Rows(i)("ChietKhauPT") = 0
                    Else
                        .Rows(i)("ChietKhauPT") = Math.Round((.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100, 2)
                    End If
                Next
            End With
            With dt.Tables(1)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1

                    Dim tb2 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThucHien").ToString, , ",")
                    .Rows(i)("NVThucHien") = ""
                    For j As Integer = 0 To tb2.Rows.Count - 1
                        AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                        Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb3 Is Nothing Then
                            .Rows(i)("NVThucHien") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NVThucHien") = .Rows(i)("NVThucHien").ToString.Trim

                    Dim tb4 As DataTable = DataSourceDSFile(.Rows(i)("IDNgKiemDuyet1").ToString, , ",")
                    .Rows(i)("NVKiemDuyetLan1") = ""
                    For j As Integer = 0 To tb4.Rows.Count - 1
                        AddParameterWhere("@ID", tb4.Rows(j)("File").ToString)
                        Dim tb5 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb5 Is Nothing Then
                            .Rows(i)("NVKiemDuyetLan1") &= "- " & tb5.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NVKiemDuyetLan1") = .Rows(i)("NVKiemDuyetLan1").ToString.Trim

                    Dim tb6 As DataTable = DataSourceDSFile(.Rows(i)("IDNgKiemDuyet2").ToString, , ",")
                    .Rows(i)("NVKiemDuyetLan2") = ""
                    For j As Integer = 0 To tb6.Rows.Count - 1
                        AddParameterWhere("@ID", tb6.Rows(j)("File").ToString)
                        Dim tb7 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb7 Is Nothing Then
                            .Rows(i)("NVKiemDuyetLan2") &= "- " & tb7.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NVKiemDuyetLan2") = .Rows(i)("NVKiemDuyetLan2").ToString.Trim

                Next


            End With

            gdvCG.DataSource = dt.Tables(0)
            gdvThiCong.DataSource = dt.Tables(1)

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadrCbNhanVien()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not dt Is Nothing Then
            rCbNhanVien.DataSource = dt
            ' rcbNhanVienXuLy.DataSource = dt
            rcbNV.DataSource = dt
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

    Private Sub btSua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub


        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("Trangthai").ToString = "Đã xử lý" Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
                ShowCanhBao("Công trình đã được xử lý, để sửa bạn cần phải có quyền Admin hoặc trưởng phòng kỹ thuật để thao tác !")
                Exit Sub
            End If
        End If

        If IsDBNull(gdvCT.GetFocusedRowCellValue("NguoiNhanXL")) Then
            ShowCanhBao("Bạn cần phải nhận xử lý công trình này trước!")
            Exit Sub
        End If

        If Convert.ToInt32(gdvCT.GetFocusedRowCellDisplayText("TTCG")) <> Convert.ToInt32(TrangThaiChaoGia.DaXacNhan) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
                ShowCanhBao("Công trình chưa được xác nhận, để sửa bạn cần phải có quyền Admin hoặc quyền trưởng phòng kĩ thuật để giao việc !")
                Exit Sub
            End If
        End If

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM PHIEUXUATKHO WHERE SoPhieuCG='" & gdvCT.GetFocusedRowCellValue("Sophieu") & "'")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If Not ShowCauHoi("Đã có xuất kho cho công trình này, bạn có muốn tiếp tục xử lý không ?") Then Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        fCNCongTrinh = New frmCNCongTrinhCanXuLy
        fCNCongTrinh.SPChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")
        fCNCongTrinh.SPDatHang = gdvCT.GetFocusedRowCellValue("Masodathang")
        fCNCongTrinh._MaKH = gdvCT.GetFocusedRowCellValue("MaKH")
        fCNCongTrinh.SPYeuCau = gdvCT.GetFocusedRowCellValue("Masodathang")
        fCNCongTrinh.TrangThaiCG = gdvCT.GetFocusedRowCellValue("TTCG")
        fCNCongTrinh._SPDaSaoChep = _SPSaoChep
        If Convert.ToInt32(gdvCT.GetFocusedRowCellDisplayText("TTCG")) <> Convert.ToInt32(TrangThaiChaoGia.DaXacNhan) Then
            fCNCongTrinh.TrangThaiCT = False
        Else
            fCNCongTrinh.TrangThaiCT = True
        End If

        fCNCongTrinh.Tag = Me.Parent.Tag
        fCNCongTrinh.ShowDialog()
        gdvCT.FocusedRowHandle = index
    End Sub

    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        gdvCT.ClearColumnsFilter()
        gdvCT.ClearSorting()
        LoadDS()
        _SPSaoChep = Nothing
    End Sub

    Private Sub gdvCT_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Masodathang"))
    End Sub

#Region "Xuất Excel"
    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        XuatExcel.CreateExcelFileChaoGia(gdvCT.GetFocusedRowCellValue("Sophieu"), chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, chkN0.Checked, chkXuatKH.Checked, True)
        panelXuatExcel.Visible = False
    End Sub
#End Region

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
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
            chkLoc.Checked = gdvCT.OptionsView.ShowAutoFilterRow
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btSua.PerformClick()
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

    Private Sub chkLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLoc.CheckedChanged
        gdvCT.OptionsView.ShowAutoFilterRow = chkLoc.Checked
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

    Private Sub rcbNangCao_QueryPopUp(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles rcbNangCao.QueryPopUp
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
    End Sub


    Private Sub rcbFileDinhKem_Popup(sender As System.Object, e As System.EventArgs) Handles rcbFileDinhKem.Popup
        If _exit Then
            _exit = False
            Exit Sub
        End If
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        _FileCGKinhDoanh = ""
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url.Trim = "" Then Continue For
            If _url.Substring(0, 23) = "YC" & gdvCT.GetFocusedRowCellValue("Masodathang") & " CG" & gdvCT.GetFocusedRowCellValue("Sophieu") & " KT " Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            Else
                _FileCGKinhDoanh &= _url & ";"
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()

    End Sub

    Private Sub rcbFileDinhKem_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rcbFileDinhKem.Closed
        Dim _File As String = _FileCGKinhDoanh
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        _File = _File.TrimEnd(New Char() {";c"})
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
        If gdvCT.GetFocusedRowCellDisplayText("IDNgDuyet").ToString <> "" Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                ShowCanhBao("Công trình đã được xử lý, để thêm file bạn cần phải có quyền Admin !")
                Exit Sub
            End If

        End If
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH")) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH"))
            End If
            For Each file In openFile.FileNames
                Try
                    path = "YC" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Masodathang") & " CG" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Sophieu") & " KT " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH") & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH") & "\" & path, 1)

                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH") & "\" & path)

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
        Try
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
            Impersonator.EndImpersonation()
            gdvListFileCT.DeleteSelectedRows()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year & "\" & UrlKyThuat & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "MaKH") & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub mChuyenSangCGChoKhach_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenSangCGChoKhach.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If ShowCauHoi("Chuyển toàn bộ vật tư của công trình này sang dạng chào giá cho khách, bạn có chắc là thao tác đúng không ?") Then
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
                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Masodathang"))
                ShowAlert("Đã chuyển thành công !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        colDonGiaCG.Visible = chkHienGia.Checked
        colCKCG.Visible = chkHienGia.Checked
        colXuatThueCG.Visible = chkHienGia.Checked
        colThueCG.Visible = chkHienGia.Checked

        colTienTeCG.Visible = chkHienGia.Checked

        colTrcThue.Visible = chkHienGia.Checked
        colThue.Visible = chkHienGia.Checked
        colCK.Visible = chkHienGia.Checked
        colTienTe.Visible = chkHienGia.Checked
        colThucThu.Visible = chkHienGia.Checked
    End Sub

    Private Sub btFileLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btFileLienQuan.Click, mXemFileLienQuan.ItemClick
        Dim f As New frmFileLienQuan
        f.Tag = Me.Parent.Tag
        f.SoChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")
        f.SoYeuCau = gdvCT.GetFocusedRowCellValue("Masodathang")
        f.MaKH = gdvCT.GetFocusedRowCellValue("MaKH")
        f.ShowDialog()
    End Sub

    Private Sub btIn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

    End Sub

    Private Sub chkXuatKH_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkXuatKH.CheckedChanged
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.BanQuanTri) Then Exit Sub
    End Sub

    Private Sub btXacNhanIn_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhanIn.Click
        If chkInBangKe.Checked And chkInKeHoachThiCong.Checked = False Then
            printFile.CongTrinh.BangKeVatTu(gdvCT.GetFocusedRowCellValue("Sophieu"))
        ElseIf chkInBangKe.Checked = False And chkInKeHoachThiCong.Checked Then
            printFile.CongTrinh.KeHoachThiCong(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Masodathang"))
        Else
            printFile.CongTrinh.BangKeVatTuVaKeHoachThiCong(gdvCT.GetFocusedRowCellValue("Sophieu"), gdvCT.GetFocusedRowCellValue("Masodathang"))
        End If

    End Sub

    Private Sub btGiaoViec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btGiaoViec.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmGiaoViec
        f.SoYC = gdvCT.GetFocusedRowCellValue("Masodathang")
        f.ShowDialog()
    End Sub

    Private Sub btLapYCVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLapYCVatTu.ItemClick

        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        'Dim f As New frmUpdateYcXuatTam
        'TrangThai.isAddNew = True
        'f.Text = "Lấy vật tư thi công cho chào giá " & gdvCT.GetFocusedRowCellValue("Sophieu")
        'f.SoCG = gdvCT.GetFocusedRowCellValue("Sophieu")
        'f.ShowDialog()

        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        ''If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        ''If gdvCT.GetFocusedRowCellDisplayText("IDNgDuyet").ToString <> "" Then
        ''    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
        ''        ShowCanhBao("Công trình đã được xử lý, để sửa bạn cần phải có quyền Admin hoặc quyền trưởng phòng kĩ thuật để giao việc !")
        ''        Exit Sub
        ''    End If
        ''End If

        ''If Convert.ToInt32(gdvCT.GetFocusedRowCellDisplayText("TTCG")) <> Convert.ToInt32(TrangThaiChaoGia.DaXacNhan) Then
        ''    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
        ''        ShowCanhBao("Công trình chưa được xác nhận, để sửa bạn cần phải có quyền Admin hoặc quyền trưởng phòng kĩ thuật để giao việc !")
        ''        Exit Sub
        ''    End If
        ''End If

        'TrangThai.isAddNew = True
        'Dim index As Integer = gdvCT.FocusedRowHandle
        'fCNXuatKhoCT = New frmCNXuatKhoCT
        'fCNXuatKhoCT.SoPhieuCG = gdvCT.GetFocusedRowCellValue("Sophieu")
        'fCNXuatKhoCT._MaKH = gdvCT.GetFocusedRowCellValue("IDKhachhang")
        'fCNXuatKhoCT.Tag = Me.Parent.Tag
        'fCNXuatKhoCT.ShowDialog()
        'gdvCT.FocusedRowHandle = index
    End Sub

    Private Sub btTTVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTTVatTu.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvCGCT.GetFocusedRowCellValue("IDVatTu")
        f._HienThongTinNX = False
        f.ShowDialog()
    End Sub


    Private Sub mSaoChepNoiDung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSaoChepNoiDung.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Sao chép nội dung xử lý của công trình: " & gdvCT.GetFocusedRowCellValue("Sophieu")) Then
            _SPSaoChep = gdvCT.GetFocusedRowCellValue("Sophieu")
            ShowAlert("Đã sao chép!")
        End If
    End Sub

    Private Sub pMenuChinh_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChinh.BeforePopup
        If gdvCGCT.RowCount = 0 Then
            mSaoChepNoiDung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            mSaoChepNoiDung.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub


    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "TienDo" Then
            If Not IsDBNull(e.CellValue) Then
                If e.CellValue >= 60 And e.CellValue < 100 Then
                    e.Appearance.BackColor = Color.Yellow
                ElseIf e.CellValue = 100 Then
                    e.Appearance.BackColor = Color.Chartreuse
                Else
                    e.Appearance.BackColor = Color.Red
                End If
            End If
        End If
    End Sub


    Private Sub mNhanXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhanXuLy.ItemClick

        Dim tg As DateTime = GetServerTime()
        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT TEN FROM NHANSU WHERE ID=(SELECT IDNgNhanXL FROM BANGCHAOGIA WHERE SoPhieu=@SP)")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If ShowCauHoi(tb.Rows(0)(0).ToString & " đã nhận xử lý công trình này, bạn có muốn nhận xử lý thay hay không ?") Then

                    AddParameter("@IDNgNhanXL", TaiKhoan)
                    AddParameter("@ThoiGianNhanXL", tg)
                    AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
                    If Not doUpdate("BANGCHAOGIA", "SoPhieu=@SP") Is Nothing Then
                        gdvCT.SetFocusedRowCellValue("NguoiNhanXL", NguoiDung)
                        gdvCT.SetFocusedRowCellValue("ThoiGianNhanXL", tg)
                        ShowAlert("Đã nhận xử lý")
                    Else
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                Else
                    Exit Sub
                End If
            Else
                If ShowCauHoi("Nhận xử lý công trình: " & gdvCT.GetFocusedRowCellValue("Sophieu")) Then
                    AddParameter("@IDNgNhanXL", TaiKhoan)
                    AddParameter("@ThoiGianNhanXL", tg)
                    AddParameter("@TienDo", 0)
                    AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
                    If Not doUpdate("BANGCHAOGIA", "SoPhieu=@SP") Is Nothing Then
                        gdvCT.SetFocusedRowCellValue("NguoiNhanXL", NguoiDung)
                        gdvCT.SetFocusedRowCellValue("ThoiGianNhanXL", tg)
                        ShowAlert("Đã nhận xử lý")
                    Else
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If

            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub



    Private Sub mnuLayVatTuThiCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLayVatTuThiCong.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        'Dim f As New frmUpdateYcXuatTam
        'TrangThai.isAddNew = True
        'f.Text = "Lấy vật tư thi công cho chào giá " & gdvCT.GetFocusedRowCellValue("Sophieu")
        'f.SoCG = gdvCT.GetFocusedRowCellValue("Sophieu")
        'f.ShowDialog()
    End Sub


    Private Sub mnuLichSuXuatTam_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLichSuXuatTam.ItemClick

    End Sub


    Private Sub btnTraLaiVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTraLaiVatTu.ItemClick
        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        'Dim f As New frmUpdateYcNhapTam
        'TrangThai.isAddNew = True
        'f.Text = "Trảvật tư thi công cho chào giá " & gdvCT.GetFocusedRowCellValue("Sophieu")
        'f.SoCG = gdvCT.GetFocusedRowCellValue("Sophieu")
        'f.ShowDialog()
    End Sub



End Class
