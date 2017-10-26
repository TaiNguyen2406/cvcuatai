Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors

Public Class frmXuatKhoCT
    Public _exit As Boolean = False
    Public index As Integer

    Private Sub frmXuatKhoCT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 100"

        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btNhanVien.Enabled = False
        End If
    End Sub

#Region "Load DS xuất kho công trình"

    Public Sub LoadDS()
        ShowWaiting("Đang tải danh sách xuất kho công trình ...")

        Dim sql As String = " SELECT "
        If cbTieuChi.EditValue = "Top 100" Then
            sql &= "  TOP 100 "
        End If

        sql &= " NgayThang,SoPhieu,KHACHHANG.ttcMa,KHACHHANG.Ten AS TenKH,SoPhieuCG,LyDoXuat,"
        sql &= " NGUOILAP.Ten AS NguoiLap,NGUOINHAN.Ten AS NguoiNhan,NGUOIXN.Ten AS NguoiXN,XacNhan,ThoiGianXN "
        sql &= " FROM tblPhieuXKCT"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID = tblPhieuXKCT.IDKH"
        sql &= " LEFT JOIN NHANSU AS NGUOILAP ON NGUOILAP.ID=tblPhieuXKCT.IDNgLap"
        sql &= " LEFT JOIN NHANSU AS NGUOINHAN ON NGUOINHAN.ID=tblPhieuXKCT.IDNgNhan"
        sql &= " LEFT JOIN NHANSU AS NGUOIXN ON NGUOIXN.ID = tblPhieuXKCT.IDNgXacNhan"
        sql &= " WHERE 1=1 "
        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,CONVERT(nvarchar,NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND tblPhieuXKCT.IDNgLap= " & btNhanVien.EditValue
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND tblPhieuXKCT.IDKH= " & cbKH.EditValue
        End If

        If cbTieuChi.EditValue = "Top 100" Then
            sql &= " ORDER BY SoPhieu DESC"
        End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        If Not dt Is Nothing Then
            gdv.DataSource = dt
            If Not gdvCT.FocusedRowHandle < 0 Then

                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieu"))
            Else
                gdvVT.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


        CloseWaiting()

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)
        AddParameterWhere("@SP", SoPhieu)
        Dim sql As String = ""
        sql &= " SELECT tblXKCT.ID AS IDXKCT,tblXKCT.SoPhieu, tblXKCT.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SLYC,SLXuatKho,tblXKCT.NgayCan,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=tblXKCT.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=tblXKCT.IDVattu)) AS slTon,"
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= tblXKCT.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= tblXKCT.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA where CHAOGIA.IDVattu= tblXKCT.IDVattu) AS CanXuat, VATTU.HangTon , tblXKCT.NgayCan,"
        sql &= " ISNULL(tblXKCT.AZ,0)AZ "
        sql &= " FROM tblXKCT "
        sql &= " INNER JOIN VATTU ON tblXKCT.IDVatTu=VATTU.ID"
        sql &= " INNER JOIN tblPhieuXKCT ON tblPhieuXKCT.SoPhieu=tblXKCT.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE tblXKCT.SoPhieu=@SP"
        sql &= " ORDER BY AZ "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvVT.DataSource = dt

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadrCbNhanVien()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not dt Is Nothing Then
            rCbNhanVien.DataSource = dt
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
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        fCNXuatKhoCT = New frmCNXuatKhoCT
        fCNXuatKhoCT.Tag = Me.Tag
        fCNXuatKhoCT.cbNgLap.EditValue = Convert.ToInt32(TaiKhoan)
        fCNXuatKhoCT.cbNgNhan.EditValue = Convert.ToInt32(TaiKhoan)
        fCNXuatKhoCT.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        'If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "IDTrangThai") = TrangThaiChaoGia.DaXacNhan Then
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        'End If


        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        index = gdvCT.FocusedRowHandle
        fCNXuatKhoCT = New frmCNXuatKhoCT
        'fCNXuatKhoCT.TrangThaiCG.isUpdate = True
        fCNXuatKhoCT.SoPhieuPXKCT = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNXuatKhoCT.SoPhieuCG = gdvCT.GetFocusedRowCellValue("SoPhieuCG")
        fCNXuatKhoCT.Tag = Me.Parent.Tag
        fCNXuatKhoCT.Show()
    End Sub

    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        gdvCT.ClearColumnsFilter()
        LoadDS()
    End Sub

    Private Sub gdvCT_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If gdvCT.FocusedRowHandle < 0 Then
            loadDSYCChiTiet("-1")
            Exit Sub
        End If

        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieu"))
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
    '        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
    '        If doDelete("YEUCAUDEN", "SoPhieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
    '        For i As Integer = 0 To gdvCGCT.RowCount - 1
    '            AddParameter("@SoPhieu", gdvCGCT.GetRowCellValue(i, "SoPhieu"))
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
    '            AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
    '            If doInsert("YEUCAUDEN") Is Nothing Then Throw New Exception(LoiNgoaiLe)
    '        Next

    '        ComitTransaction()
    '        ShowAlert("Đã lưu lại")

    '    Catch ex As Exception
    '        RollBackTransaction()
    '        ShowBaoLoi(ex.Message)
    '    End Try
    'End Sub

    Private Sub btSaoChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSaoChep.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        If Not ShowCauHoi("Tạo phiếu xuất kho công trình theo phiếu đang được chọn ?") Then
            Exit Sub
        End If

        TrangThai.isCopy = True
        fCNXuatKhoCT = New frmCNXuatKhoCT
        fCNXuatKhoCT.SoPhieuPXKCT = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNXuatKhoCT.Tag = Me.Parent.Tag
        fCNXuatKhoCT.ShowDialog()
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
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
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


    Private Sub mXemChaoGiaChoKH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGiaChoKH.ItemClick
        Dim FilterString, FilterDisplayText As String
        Dim FilterIf As ColumnFilterInfo
        Dim BinaryFilter As CriteriaOperator
        BinaryFilter = New GroupOperator(New BinaryOperator("SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu") & "CT", BinaryOperatorType.Equal))
        FilterString = BinaryFilter.ToString()
        FilterDisplayText = "Chào giá cho khách"
        FilterIf = New ColumnFilterInfo(FilterString, FilterDisplayText)
        gdvVTCT.Columns("SoPhieu").FilterInfo = FilterIf
    End Sub

    Private Sub mXemChaoGiaDaXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGiaDaXuLy.ItemClick
        Dim FilterString, FilterDisplayText As String
        Dim FilterIf As ColumnFilterInfo
        Dim BinaryFilter As CriteriaOperator
        BinaryFilter = New GroupOperator(New BinaryOperator("SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu"), BinaryOperatorType.Equal))
        FilterString = BinaryFilter.ToString()
        FilterDisplayText = "Chào giá đã xử lý"
        FilterIf = New ColumnFilterInfo(FilterString, FilterDisplayText)
        gdvVTCT.Columns("SoPhieu").FilterInfo = FilterIf
    End Sub

    Private Sub mXemTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemTatCa.ItemClick
        gdvVTCT.Columns("SoPhieu").ClearFilter()
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
        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
        If doUpdate("BANGCHAOGIA", "SoPhieu=@SP") Is Nothing Then
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
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH")) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH"))
            End If
            ShowWaiting("Đang tải file lên máy chủ ...")
            For Each file In openFile.FileNames
                Try
                    path = "YC" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Masodathang") & " CG" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieu") & " KD " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
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
                System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
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
            Dim psi As New ProcessStartInfo()
            With psi
                .FileName = RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & e.CellValue
                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub

    Private Sub mXemThongTinYCDen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemThongTinYCDen.ItemClick
        XemYeuCauDen()
    End Sub

    Private Sub btFileLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btFileLienQuan.Click, mXemFileLienQuan.ItemClick
        Dim f As New frmFileLienQuan
        f.Tag = Me.Parent.Tag
        f.SoChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieu")
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
        If ShowCauHoi("Chuyển toàn bộ vật tư của công trình này sang dạng chào giá cho khách, bạn có chắc là thao tác đúng không ?") Then
            Try
                BeginTransaction()
                AddParameter("@Canxuat", 0)
                AddParameter("@SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu") & "CT")
                AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
                If doUpdate("CHAOGIA", "SoPhieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                AddParameter("@SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu") & "CT")
                AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
                If doUpdate("CHAOGIAAUX", "SoPhieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                ComitTransaction()
                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieu"))
                ShowAlert("Đã chuyển thành công !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub mThuMucLuuTru_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuMucLuuTru.ItemClick
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        Dim psi As New ProcessStartInfo()
        With psi
            .FileName = "explorer.exe"
            .Arguments = "/Select," & RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("Ngaythang")).Year.ToString & "\" & UrlKinhDoanh & gdvCT.GetFocusedRowCellValue("MaKH") & "\" & gdvListFileCT.GetFocusedRowCellValue("File")
            .UseShellExecute = True
        End With
        Try
            Process.Start(psi)
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class
