Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering

Public Class frmKhachHang

    Private Sub frmKhachHang_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        'cbLoaiKH.EditValue = "Tất cả"
        tbNgayNhap.EditValue = Today.Date



        rgdbTakecare.DataSource = ExecuteSQLDataTable("select ID,Ten from nhansu where Noictac = 74 union all select null,N'-- Tất cả --' ORDER BY Ten ")
        cmbTakecare.EditValue = TaiKhoan

        rcmbHinhThucChungTu.DataSource = TAI.tableHinhThucCT()


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cmbTakeCare.Enabled = False
            cmbBaoCao.Enabled = False
        End If
        LoadLoc()
        loadDS()

        LoadDsHinhThucTT2()


        For i As Integer = 0 To gdvKHCT.Columns.Count - 1
            gdvKHCT.Columns(i).OptionsColumn.ReadOnly = True
        Next

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            gdvKHCT.Columns("IDHinhThucTT2").OptionsColumn.ReadOnly = False
            gdvKHCT.Columns("HinhThucChungTu").OptionsColumn.ReadOnly = False
        End If


    End Sub


    Private Sub LoadDsHinhThucTT2()
        riLueNhom.DataSource = TAI.tableNhomHinhThucTT
        Dim sql As String = "SELECT ID,Nhom, TraTruoc1,TraTruoc2,TraSau1,TraSau2,SoNgayHT,GiaiThich FROM DM_HINH_THUC_TT where TrangThai=1 ORDER BY Nhom asc, GiaiThich asc  "
        rgdvHTTT2.DataSource = ExecuteSQLDataTable(sql)

        ' rgdvHTTT2.View.Columns("Nhom").GroupIndex = 0
    End Sub

    Private Sub LoadLoc()
        Dim str As String = "select ID, NoiDung from  tblTuDien"
        Dim str2 As String = ""
        str2 = str + "  where loai=19"
        riLueChuSoHuu.DataSource = ExecuteSQLDataTable(str2)
        str2 = str + " where loai=12"
        riLueTinhThanh.DataSource = ExecuteSQLDataTable(str2)
        str2 = str + " where loai=13"
        riLueKhuCN.DataSource = ExecuteSQLDataTable(str2)
    End Sub
    Public Sub LoadDSById(IdKH As Object)


        Dim sql As String = ""

        Try
            sql = " Select KHACHHANG.*,NHANSU.Ten AS TakeCare,NGUOINHAP.Ten AS NguoiNhap,THANHPHO.NoiDung AS ThanhPho,KHUCN.NoiDung AS KhuCN,"
            sql &= " LOAIDN.NoiDung AS LoaiDN,TINHTRANG.NoiDung AS TinhTrang,CHUSH.NoiDung AS ChuSoHuu, tblHinhThucTTKH.HinhThucTT_VIE AS HinhThucTT, "
            'Tai
            sql &= " DM_HINH_THUC_TT.GiaiThich as HinhThucTT2, HinhThucChungTu, IDHinhThucTT2,  "
            'Tai
            sql &= " (Case KHACHHANG.ttcKhachHang WHEN 1 THEN N'Khách hàng' ELSE N'KH/NCC' END) LoaiKH, (CASE KHACHHANG.NhomKH WHEN 1 THEN N'Thương mại, Chế tạo máy, Tích hợp …' WHEN 2 THEN  N'END User' ELSE '' END)NhomKH2,"
            sql &= " (CASE WHEN KHACHHANG.CapKH IS null THEN Convert(nvarchar,CapKH) ELSE N'Cấp ' +  Convert(nvarchar, KHACHHANG.CapKH) END)CapKH2"
            sql &= " FROM KHACHHANG LEFT OUTER JOIN NHANSU AS NHANSU ON KHACHHANG.IDTakecare=NHANSU.ID "
            sql &= " LEFT OUTER JOIN tblHinhThucTTKH ON KHACHHANG.IDHinhThucTT=tblHinhThucTTKH.ID "
            sql &= " LEFT OUTER JOIN NHANSU AS NGUOINHAP ON KHACHHANG.IDUser=NGUOINHAP.ID "
            sql &= " LEFT OUTER JOIN tblTuDien AS THANHPHO ON KHACHHANG.IDTinhThanh=THANHPHO.ID  "
            sql &= " LEFT OUTER JOIN tblTuDien AS KHUCN ON KHACHHANG.IDKhuCN=KHUCN.ID  "
            sql &= " LEFT OUTER JOIN tblTuDien AS LOAIDN ON KHACHHANG.IDLoaiHinhDN=LOAIDN.ID  "
            sql &= " LEFT OUTER JOIN tblTuDien AS CHUSH ON KHACHHANG.IDChuSoHuu=CHUSH.ID  "
            sql &= " LEFT OUTER JOIN tblTuDien AS TINHTRANG ON KHACHHANG.IDTinhTrang=TINHTRANG.ID  "
            'Tai
            sql &= "        LEFT OUTER JOIN DM_HINH_THUC_TT ON KHACHHANG.IDHinhThucTT2=DM_HINH_THUC_TT.ID "
            'Tai
            sql &= " WHERE KHACHHANG.ID = N'" & IdKH & "' "

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub

            Dim dtX As DataTable = CType(gdvKHCT.DataSource, DataView).Table

            Dim arrId() = dtX.Select("ID='" & dt.Rows(0)("ID") & "'")
            Dim r As DataRow

            If arrId.Length > 0 Then
                r = arrId(0)
            Else
                r = dtX.NewRow
            End If

            For i As Integer = 0 To dtX.Columns.Count - 1
                r(i) = dt.Rows(0)(i)
            Next

            Dim tb As DataTable = DataSourceDSFile(r("IDLinhVucSX").ToString, "IDLinhVucSX")
            r("IDLinhVucSX") = ""
            For j As Integer = 0 To tb.Rows.Count - 1
                Dim rows() As DataRow = dtLinhVucHoatDong.Select("ID = '" & tb.Rows(j)("IDLinhVucSX").ToString & "'")
                If Not rows Is Nothing And rows.Length > 0 Then
                    r("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                End If
            Next
            r("IDLinhVucSX") = r("IDLinhVucSX").ToString.Trim

            If arrId.Length = 0 Then
                dtX.Rows.InsertAt(r, dtX.Rows.Count)
            End If

        Catch ex As Exception

        End Try


    End Sub

    Public Sub loadDS()

        ShowWaiting("Đang tải dữ liệu !")
        Dim sql As String = ""

        sql = " Select KHACHHANG.*,NHANSU.Ten AS TakeCare,NGUOINHAP.Ten AS NguoiNhap,THANHPHO.NoiDung AS ThanhPho,KHUCN.NoiDung AS KhuCN,"
        sql &= " LOAIDN.NoiDung AS LoaiDN,TINHTRANG.NoiDung AS TinhTrang,CHUSH.NoiDung AS ChuSoHuu, tblHinhThucTTKH.HinhThucTT_VIE AS HinhThucTT, "
        'Tai
        sql &= " DM_HINH_THUC_TT.GiaiThich as HinhThucTT2, HinhThucChungTu, IDHinhThucTT2,  "
        'Tai
        sql &= " (Case KHACHHANG.ttcKhachHang WHEN 1 THEN N'Khách hàng' ELSE N'KH/NCC' END) LoaiKH, (CASE KHACHHANG.NhomKH WHEN 1 THEN N'Thương mại, Chế tạo máy, Tích hợp …' WHEN 2 THEN  N'END User' ELSE '' END)NhomKH2,"
        sql &= " (CASE WHEN KHACHHANG.CapKH IS null THEN Convert(nvarchar,CapKH) ELSE N'Cấp ' +  Convert(nvarchar, KHACHHANG.CapKH) END)CapKH2"
        sql &= " FROM KHACHHANG LEFT OUTER JOIN NHANSU AS NHANSU ON KHACHHANG.IDTakecare=NHANSU.ID "
        sql &= " LEFT OUTER JOIN tblHinhThucTTKH ON KHACHHANG.IDHinhThucTT=tblHinhThucTTKH.ID "
        sql &= " LEFT OUTER JOIN NHANSU AS NGUOINHAP ON KHACHHANG.IDUser=NGUOINHAP.ID "
        sql &= " LEFT OUTER JOIN tblTuDien AS THANHPHO ON KHACHHANG.IDTinhThanh=THANHPHO.ID  "
        sql &= " LEFT OUTER JOIN tblTuDien AS KHUCN ON KHACHHANG.IDKhuCN=KHUCN.ID  "
        sql &= " LEFT OUTER JOIN tblTuDien AS LOAIDN ON KHACHHANG.IDLoaiHinhDN=LOAIDN.ID  "
        sql &= " LEFT OUTER JOIN tblTuDien AS CHUSH ON KHACHHANG.IDChuSoHuu=CHUSH.ID  "
        sql &= " LEFT OUTER JOIN tblTuDien AS TINHTRANG ON KHACHHANG.IDTinhTrang=TINHTRANG.ID  "
        'Tai
        sql &= "        LEFT OUTER JOIN DM_HINH_THUC_TT ON KHACHHANG.IDHinhThucTT2=DM_HINH_THUC_TT.ID "
        'Tai
        sql &= " WHERE 1=1" 'KHACHHANG.ttcKhachHang <>0 "

        If Not cmbTakeCare.EditValue Is DBNull.Value Then
            If chkTaiCaBlank.EditValue = False Then
                sql &= " AND KHACHHANG.IDTakecare = " & cmbTakeCare.EditValue & " "
            Else
                sql &= " AND (KHACHHANG.IDTakecare = " & cmbTakeCare.EditValue & " OR KHACHHANG.IDTakecare is null) "
            End If
        Else
            If chkTaiCaBlank.EditValue = False Then
                sql &= " AND KHACHHANG.IDTakecare is not null "
            End If
        End If
        If lueChuSoHuu.EditValue IsNot Nothing Then
            sql &= " AND IDChuSoHuu =@IDChuSoHuu "
        End If
        If lueTinhThanh.EditValue IsNot Nothing Then
            sql &= " AND IDTinhThanh =@IDTinhThanh "
        End If
        If lueKhuCN.EditValue IsNot Nothing Then
            sql &= " AND IDKhuCN =@IDKhuCN "
        End If
        If txtMaKH.EditValue <> "" Then
            sql &= " and ttcMa like @ttcMa"
        End If
        If btnXacNhanKhachMoi.Checked Then
            sql &= " AND Convert(datetime,convert(nvarchar, KHACHHANG.Ngay,103),103)>=@Ngay "
        End If



        sql &= " SELECT NHANSU.ID,NHANSU.Ten,NHANSU.ChucVu,NHANSU.NgaySinh,NHANSU.SoCMT,NHANSU.NgayCap,NHANSU.NoiCap,NHANSU.DiaChi,NHANSU.NguyenQuan,"
        sql &= " NHANSU.DienThoaiNR,NHANSU.DienThoaiCQ,NHANSU.Mobile,NHANSU.Mobile1,NHANSU.Fax,NHANSU.Email,NHANSU.TaiKhoan,NHANSU.NganHang,"
        sql &= " TAKECARE.Ten AS TakeCare,NHANSU.NoiCTac,NHANSU.Email,NHANSU.TrangThai"
        sql &= " FROM NHANSU "
        sql &= " LEFT OUTER JOIN NHANSU AS TAKECARE ON TAKECARE.ID=NHANSU.Chamsoc "
        sql &= " WHERE NHANSU.Noictac <> 74 AND NHANSU.Noictac in (SELECT ID FROM KHACHHANG"

        sql &= " WHERE KHACHHANG.ttcKhachHang <> 0 "

        If Not cmbTakeCare.EditValue Is DBNull.Value Then
            If chkTaiCaBlank.EditValue = False Then
                sql &= " AND KHACHHANG.IDTakecare = " & cmbTakeCare.EditValue & " "
            Else
                sql &= " AND (KHACHHANG.IDTakecare = " & cmbTakeCare.EditValue & " OR KHACHHANG.IDTakecare is null) "
            End If
        Else
            If chkTaiCaBlank.EditValue = False Then
                sql &= " AND KHACHHANG.IDTakecare is not null "
            End If
        End If
        If lueChuSoHuu.EditValue IsNot Nothing Then
            sql &= " AND IDChuSoHuu =@IDChuSoHuu "
        End If
        If lueTinhThanh.EditValue IsNot Nothing Then
            sql &= " AND IDTinhThanh =@IDTinhThanh "
        End If
        If lueKhuCN.EditValue IsNot Nothing Then
            sql &= " AND IDKhuCN =@IDKhuCN "
        End If
        If txtMaKH.EditValue <> "" Then
            sql &= " and ttcMa like @ttcMa"
        End If
        If btnXacNhanKhachMoi.Checked Then
            sql &= " AND Convert(datetime,convert(nvarchar, KHACHHANG.Ngay,103),103)>=@Ngay "
        End If

        sql &= " ) "
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            sql &= " AND (NHANSU.Chamsoc = " & TaiKhoan & " OR NHANSU.Chamsoc is null )"
        End If
        sql &= " ORDER BY NHANSU.Ten"

        sql &= " SELECT GIAODICHKH.ID,GIAODICHKH.IDKH,GIAODICHKH.ThoiGian,GIAODICHKH.IDTakeCare,GIAODICHKH.NoiDungGiaoDich,GIAODICHKH.ChuyenGiao,GIAODICHKH.IDNgChuyen,"
        sql &= " tblTuDien.NoiDung as HinhThuc,NHANSU.Ten AS TakeCare "
        sql &= " FROM GIAODICHKH "
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=GIAODICHKH.HinhThuc AND tblTuDien.Loai=@LoaiTuDien"
        AddParameterWhere("@LoaiTuDien", LoaiTuDien.GiaoDichKH)
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=GIAODICHKH.IDTakeCare AND NHANSU.Noictac=74 WHERE GIAODICHKH.IDKH IN (SELECT ID FROM KHACHHANG "

        sql &= " WHERE KHACHHANG.ttcKhachHang <>0 "


        If Not cmbTakeCare.EditValue Is DBNull.Value Then
            If chkTaiCaBlank.EditValue = False Then
                sql &= " AND KHACHHANG.IDTakecare = " & cmbTakeCare.EditValue & " "
            Else
                sql &= " AND (KHACHHANG.IDTakecare = " & cmbTakeCare.EditValue & " OR KHACHHANG.IDTakecare is null) "
            End If
        Else
            If chkTaiCaBlank.EditValue = False Then
                sql &= " AND KHACHHANG.IDTakecare is not null "
            End If
        End If
        If lueChuSoHuu.EditValue IsNot Nothing Then
            sql &= " AND IDChuSoHuu =@IDChuSoHuu "
        End If
        If lueTinhThanh.EditValue IsNot Nothing Then
            sql &= " AND IDTinhThanh =@IDTinhThanh "
        End If
        If lueKhuCN.EditValue IsNot Nothing Then
            sql &= " AND IDKhuCN =@IDKhuCN "
        End If
        If txtMaKH.EditValue <> "" Then
            sql &= " and ttcMa like @ttcMa"
        End If
        If btnXacNhanKhachMoi.Checked Then
            sql &= " AND Convert(datetime,convert(nvarchar, KHACHHANG.Ngay,103),103)>=@Ngay "
        End If

       
        sql &= " )"
        sql &= " ORDER BY ThoiGian"

        AddParameterWhere("@LinhVuc", LoaiTuDien.LinhVucSX)
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LinhVuc "

        If btnXacNhanKhachMoi.Checked Then
            AddParameter("@Ngay", tbNgayNhap.EditValue)
        End If
        AddParameterWhere("@IDChuSoHuu", lueChuSoHuu.EditValue)
        AddParameterWhere("@IDTinhThanh", lueTinhThanh.EditValue)
        AddParameterWhere("@IDKhuCN", lueKhuCN.EditValue)
        AddParameterWhere("@ttcMa", "%" + txtMaKH.EditValue + "%")
        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        dtLinhVucHoatDong = dt.Tables(3)
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Tables(0).Rows.Count - 1
                If dt.Tables(0).Rows(i)("IDLinhVucSX").ToString.Trim <> "" Then
                    Dim tb As DataTable = DataSourceDSFile(dt.Tables(0).Rows(i)("IDLinhVucSX").ToString, "IDLinhVucSX")
                    dt.Tables(0).Rows(i)("IDLinhVucSX") = ""
                    For j As Integer = 0 To tb.Rows.Count - 1
                        Dim rows() As DataRow = dt.Tables(3).Select("ID = '" & tb.Rows(j)("IDLinhVucSX").ToString & "'")
                        If Not rows Is Nothing And rows.Length > 0 Then
                            dt.Tables(0).Rows(i)("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                        End If
                    Next
                    dt.Tables(0).Rows(i)("IDLinhVucSX") = dt.Tables(0).Rows(i)("IDLinhVucSX").ToString.Trim
                End If
            Next

            dt.Relations.Add(dt.Tables(0).Columns("ID"), dt.Tables(1).Columns("NoiCTac"))
            dt.Relations.Add(dt.Tables(0).Columns("ID"), dt.Tables(2).Columns("IDKH"))
            dt.Relations.Item(0).RelationName = "Người giao dịch"
            dt.Relations.Item(1).RelationName = "Thông tin giao dịch"
            gdvKH.DataSource = dt.Tables(0)
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private dtLinhVucHoatDong As DataTable


    Private Sub gdvKHCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvKHCT.CustomDrawCell
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.VisibleIndex = 0 And view.IsMasterRowEmptyEx(e.RowHandle, 0) And view.IsMasterRowEmptyEx(e.RowHandle, 1) Then
            CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty
        End If
    End Sub

    Private Sub cbLoaiKH_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadDS()
    End Sub

    Private Sub gdvKHCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvKHCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btThem.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btSua.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.I Then
            ColID.Visible = Not ColID.Visible
            colIDNgd.Visible = Not colIDNgd.Visible
            If ColID.Visible Then
                ColID.Fixed = FixedStyle.Left
                colIDNgd.Fixed = FixedStyle.Left
            End If
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThemKH.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        fCNKhachHang = New frmCNKhachHang
        fCNKhachHang.Tag = Me.Parent.Tag
        fCNKhachHang.cbTakecare.EditValue = Convert.ToInt32(TaiKhoan)
        fCNKhachHang.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSuaKH.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            If TaiKhoan <> gdvKHCT.GetFocusedRowCellValue("IDTakecare").ToString And Not IsDBNull(gdvKHCT.GetFocusedRowCellValue("IDTakecare")) Then
                ShowCanhBao("Bạn không được phép sửa thông tin khách hàng của người khác !")
                Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        Dim Index As Integer = gdvKHCT.FocusedRowHandle
        MaTuDien = gdvKHCT.GetFocusedRowCellValue("ID")
        fCNKhachHang = New frmCNKhachHang
        fCNKhachHang.Tag = Me.Parent.Tag
        fCNKhachHang.ShowDialog()
        gdvKHCT.FocusedRowHandle = Index
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

    Private Sub rcbFile_Popup(sender As System.Object, e As System.EventArgs) Handles rpopupFile.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(UrlKhachHang & gdvKHCT.GetFocusedRowCellValue("ttcMa") & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub btThemNgd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThemNgd.ItemClick, mThemNgd.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim Index As Integer = gdvKHCT.FocusedRowHandle
        Dim f As New frmCNNguoiGiaoDich
        f.Tag = Me.Parent.Tag
        f.cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        f.cbNoiCongTac.EditValue = Convert.ToInt32(gdvKHCT.GetFocusedRowCellValue("ID"))
        f.ShowDialog()
        gdvKHCT.FocusedRowHandle = Index
        gdvKHCT.ExpandMasterRow(Index, "Người giao dịch")
    End Sub

    Private Sub mThemGiaoDich_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemGiaoDich.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim Index As Integer = gdvKHCT.FocusedRowHandle
        Dim f As New frmLichSuGiaoDich
        f.Tag = Me.Parent.Tag
        f.cbTakecare.EditValue = Convert.ToInt32(TaiKhoan)
        f._IDKH = Convert.ToInt32(gdvKHCT.GetFocusedRowCellValue("ID"))
        f.ShowDialog()
        gdvKHCT.FocusedRowHandle = Index
        gdvKHCT.ExpandMasterRow(Index, "Thông tin giao dịch")
    End Sub

    Private Sub btSuaNgd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaTTNgd.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If Not gdvKHCT.GetMasterRowExpanded(gdvKHCT.FocusedRowHandle) Then Exit Sub
        TrangThai.isUpdate = True
        Dim Index As Integer = gdvKHCT.FocusedRowHandle
        Dim indexNgd As Integer = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle
        MaTuDien = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
        Dim f As New frmCNNguoiGiaoDich
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
        gdvKHCT.FocusedRowHandle = Index
        gdvKHCT.ExpandMasterRow(Index, "Người giao dịch")
        CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle = indexNgd
    End Sub

    Private Sub mSuaGiaoDich_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaQTDG.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If Not gdvKHCT.GetMasterRowExpanded(gdvKHCT.FocusedRowHandle) Then Exit Sub
        TrangThai.isUpdate = True
        Dim Index As Integer = gdvKHCT.FocusedRowHandle
        Dim indexGD As Integer = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Thông tin giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle
        objID = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Thông tin giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
        If CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Thông tin giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ChuyenGiao") Then
            Dim f As New frmChuyenGiaoKH
            f.Tag = Me.Parent.Tag
            f._IDKH = Convert.ToInt32(gdvKHCT.GetFocusedRowCellValue("ID"))
            f.cbNguoiChuyen.Enabled = False
            f.ShowDialog()
        Else
            Dim f As New frmLichSuGiaoDich
            f.Tag = Me.Parent.Tag
            f._IDKH = Convert.ToInt32(gdvKHCT.GetFocusedRowCellValue("ID"))
            f.ShowDialog()
        End If
        gdvKHCT.FocusedRowHandle = Index
        gdvKHCT.ExpandMasterRow(Index, "Thông tin giao dịch")
        CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Thông tin giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle = indexGD
    End Sub

    Private Sub btXoaKH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoaKH.ItemClick, mXoaKH.ItemClick


        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvKHCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá khách hàng " & gdvKHCT.GetFocusedRowCellValue("ttcMa") & " ?") Then
            AddParameterWhere("@MaKH", gdvKHCT.GetFocusedRowCellValue("ID"))
            Dim sql As String = ""
            sql &= " SELECT COUNT(IDKhachhang) FROM BANGCHAOGIA WHERE IDKhachhang=@MaKH"
            sql &= " SELECT COUNT(IDkhachhang) FROM BANGYEUCAU WHERE IDkhachhang=@MaKH"
            sql &= " SELECT COUNT(IDKh) FROM CHI WHERE IDKh=@MaKH"
            sql &= " SELECT COUNT(IDKhachhang) FROM KHVATTUSUDUNG WHERE IDKhachhang=@MaKH"
            sql &= " SELECT COUNT(IDkh) FROM NODAUKY WHERE IDkh=@MaKH"
            sql &= " SELECT COUNT(IDKH) FROM NOPHAITHU WHERE IDKH=@MaKH"
            sql &= " SELECT COUNT(IDKH) FROM NOPHAITRA WHERE IDKH=@MaKH"
            sql &= " SELECT COUNT(IDKhachhang) FROM PHIEUDATHANG WHERE IDKhachhang=@MaKH"
            sql &= " SELECT COUNT(IDKhachhang) FROM PHIEUNHAPKHO WHERE IDKhachhang=@MaKH"
            sql &= " SELECT COUNT(IDkhachhang) FROM PHIEUXUATKHO WHERE IDkhachhang=@MaKH"
            sql &= " SELECT COUNT(IDKhachhang) FROM PHIEUYEUCAUDI WHERE IDKhachhang=@MaKH"
            sql &= " SELECT COUNT(IDkh) FROM THU WHERE IDkh=@MaKH"
            sql &= " SELECT COUNT(IDKh) FROM THUNH WHERE IDKh=@MaKH"
            sql &= " SELECT COUNT(IDKh) FROM UNC WHERE IDKh=@MaKH"
            sql &= " SELECT COUNT(Noictac) FROM NHANSU WHERE Noictac=@MaKH"
            sql &= " SELECT COUNT(Id) FROM CHUNGTU WHERE IdKH=@MaKH"
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            Dim count As Integer = 0
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables.Count - 1
                    count += ds.Tables(i).Rows(0)(0)
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            Dim index As Integer = gdvKHCT.FocusedRowHandle
            If count > 0 Then

                Dim f As New frmThayIDKH
                f.Tag = "KH"
                f.Text = "Thay thế ID của KH: " & gdvKHCT.GetFocusedRowCellValue("ttcMa")
                f._oldID = gdvKHCT.GetFocusedRowCellValue("ID")
                f._oldttcMa = gdvKHCT.GetFocusedRowCellValue("ttcMa")
                f.ShowDialog()

            Else
                AddParameterWhere("@ID", gdvKHCT.GetFocusedRowCellValue("ID"))
                If doDelete("KHACHHANG", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã xoá !")
                    loadDS()
                End If
            End If
            gdvKHCT.FocusedRowHandle = index
        End If


    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs)
        AddParameterWhere("@Ngay", tbNgayNhap.EditValue)
        ShowWaiting("Đang tải dữ liệu !")
        Dim sql As String = ""

        sql = " Select KHACHHANG.*,NHANSU.Ten AS TakeCare,NGUOINHAP.Ten AS NguoiNhap,THANHPHO.NoiDung AS ThanhPho,KHUCN.NoiDung AS KhuCN,"
        sql &= " LOAIDN.NoiDung AS LoaiDN,TINHTRANG.NoiDung AS TinhTrang,CHUSH.NoiDung AS ChuSoHuu, tblHinhThucTTKH.HinhThucTT_VIE AS HinhThucTT, "

        sql &= " (Case KHACHHANG.ttcKhachHang WHEN 1 THEN N'Khách hàng' ELSE N'KH/NCC' END) LoaiKH, (CASE KHACHHANG.NhomKH WHEN 1 THEN N'Thương mại, Chế tạo máy, Tích hợp …' WHEN 2 THEN  N'END User' ELSE '' END)NhomKH2,"
        sql &= " (CASE WHEN KHACHHANG.CapKH IS null THEN Convert(nvarchar,CapKH) ELSE N'Cấp ' +  Convert(nvarchar, KHACHHANG.CapKH) END)CapKH2"
        sql &= " FROM KHACHHANG LEFT OUTER JOIN NHANSU AS NHANSU ON KHACHHANG.IDTakecare=NHANSU.ID "
        sql &= "        LEFT OUTER JOIN tblHinhThucTTKH ON KHACHHANG.IDHinhThucTT=tblHinhThucTTKH.ID "
        sql &= "        LEFT OUTER JOIN NHANSU AS NGUOINHAP ON KHACHHANG.IDUser=NGUOINHAP.ID "
        sql &= "        LEFT OUTER JOIN tblTuDien AS THANHPHO ON KHACHHANG.IDTinhThanh=THANHPHO.ID  "
        sql &= "        LEFT OUTER JOIN tblTuDien AS KHUCN ON KHACHHANG.IDKhuCN=KHUCN.ID  "
        sql &= "        LEFT OUTER JOIN tblTuDien AS LOAIDN ON KHACHHANG.IDLoaiHinhDN=LOAIDN.ID  "
        sql &= "        LEFT OUTER JOIN tblTuDien AS CHUSH ON KHACHHANG.IDChuSoHuu=CHUSH.ID  "
        sql &= "        LEFT OUTER JOIN tblTuDien AS TINHTRANG ON KHACHHANG.IDTinhTrang=TINHTRANG.ID  "

        sql &= " WHERE (KHACHHANG.ttcKhachHang = 1 OR KHACHHANG.ttcKhachHang =2) AND Convert(datetime,convert(nvarchar, KHACHHANG.Ngay,103),103)>=@Ngay "


        sql &= " SELECT NHANSU.ID,NHANSU.Ten,NHANSU.ChucVu,NHANSU.NgaySinh,NHANSU.SoCMT,NHANSU.NgayCap,NHANSU.NoiCap,NHANSU.DiaChi,NHANSU.NguyenQuan,"
        sql &= " NHANSU.DienThoaiNR,NHANSU.DienThoaiCQ,NHANSU.Mobile,NHANSU.Mobile1,NHANSU.Fax,NHANSU.Email,NHANSU.TaiKhoan,NHANSU.NganHang,"
        sql &= " TAKECARE.Ten AS TakeCare,NHANSU.NoiCTac,NHANSU.Email,NHANSU.TrangThai"
        sql &= " FROM NHANSU "
        sql &= " LEFT OUTER JOIN NHANSU AS TAKECARE ON TAKECARE.ID=NHANSU.Chamsoc "
        sql &= " WHERE NHANSU.Noictac <> 74 AND NHANSU.Noictac in (SELECT ID FROM KHACHHANG"

        sql &= " WHERE (KHACHHANG.ttcKhachHang = 1 OR KHACHHANG.ttcKhachHang =2) AND Convert(datetime,convert(nvarchar, KHACHHANG.Ngay,103),103)>=@Ngay "

        sql &= " ) "

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            sql &= " AND (NHANSU.Chamsoc = " & TaiKhoan & " OR NHANSU.Chamsoc is null )"
        End If

        sql &= " ORDER BY NHANSU.Ten"

        sql &= " SELECT GIAODICHKH.ID,GIAODICHKH.IDKH,GIAODICHKH.ThoiGian,GIAODICHKH.IDTakeCare,GIAODICHKH.NoiDungGiaoDich,GIAODICHKH.ChuyenGiao,GIAODICHKH.IDNgChuyen,"
        sql &= " tblTuDien.NoiDung as HinhThuc,NHANSU.Ten AS TakeCare "
        sql &= " FROM GIAODICHKH "
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=GIAODICHKH.HinhThuc AND tblTuDien.Loai=@LoaiTuDien"
        AddParameterWhere("@LoaiTuDien", LoaiTuDien.GiaoDichKH)
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=GIAODICHKH.IDTakeCare AND NHANSU.Noictac=74 WHERE GIAODICHKH.IDKH IN (SELECT ID FROM KHACHHANG "

        sql &= " WHERE (KHACHHANG.ttcKhachHang = 1 OR KHACHHANG.ttcKhachHang =2) AND Convert(datetime,convert(nvarchar, KHACHHANG.Ngay,103),103)>=@Ngay "


        sql &= " )"
        sql &= "  ORDER BY ThoiGian"

        AddParameterWhere("@LinhVuc", LoaiTuDien.LinhVucSX)
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LinhVuc "

        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Tables(0).Rows.Count - 1
                If dt.Tables(0).Rows(i)("IDLinhVucSX").ToString.Trim <> "" Then
                    Dim tb As DataTable = DataSourceDSFile(dt.Tables(0).Rows(i)("IDLinhVucSX").ToString, "IDLinhVucSX")
                    dt.Tables(0).Rows(i)("IDLinhVucSX") = ""
                    For j As Integer = 0 To tb.Rows.Count - 1
                        Dim rows() As DataRow = dt.Tables(3).Select("ID = '" & tb.Rows(j)("IDLinhVucSX").ToString & "'")
                        If Not rows Is Nothing And rows.Length > 0 Then
                            dt.Tables(0).Rows(i)("IDLinhVucSX") &= "- " & rows(0)("NoiDung").ToString & vbCrLf
                        End If
                    Next
                    dt.Tables(0).Rows(i)("IDLinhVucSX") = dt.Tables(0).Rows(i)("IDLinhVucSX").ToString.Trim
                End If
            Next

            dt.Relations.Add(dt.Tables(0).Columns("ID"), dt.Tables(1).Columns("NoiCTac"))
            dt.Relations.Add(dt.Tables(0).Columns("ID"), dt.Tables(2).Columns("IDKH"))
            dt.Relations.Item(0).RelationName = "Người giao dịch"
            dt.Relations.Item(1).RelationName = "Thông tin giao dịch"
            gdvKH.DataSource = dt.Tables(0)
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXoaNgd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaTTNgd.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If Not gdvKHCT.GetMasterRowExpanded(gdvKHCT.FocusedRowHandle) Then Exit Sub
        If ShowCauHoi("Xoá người giao dịch " & CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten") & " ?") Then
            AddParameterWhere("@IDNgd", CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
            Dim sql As String = ""
            sql &= " SELECT COUNT(IDNgd) FROM BANGCHAOGIA WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM BANGYEUCAU WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM PHIEUDATHANG WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM PHIEUXUATKHO WHERE IDNgd=@IDNgd"
            sql &= " SELECT COUNT(IDNgd) FROM PHIEUYEUCAUDI WHERE IDNgd=@IDNgd"
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            Dim count As Integer = 0
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables.Count - 1
                    count += ds.Tables(i).Rows(0)(0)
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            Dim Index As Integer = gdvKHCT.FocusedRowHandle
            Dim indexNgd As Integer = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle

            If count > 0 Then

                Dim f As New frmThayIDKH
                f.Tag = "NGD"
                f.Text = "Thay thế ID của người gd: " & CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten")
                f._oldID = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
                f._oldttcMa = CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten")
                f.ShowDialog()

            Else
                AddParameterWhere("@ID", CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
                If doDelete("NHANSU", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã xoá !")
                    loadDS()
                End If
            End If

            gdvKHCT.FocusedRowHandle = Index
            gdvKHCT.ExpandMasterRow(Index, "Người giao dịch")
            CType(gdvKHCT.GetDetailView(gdvKHCT.FocusedRowHandle, gdvKHCT.GetRelationIndex(gdvKHCT.FocusedRowHandle, "Người giao dịch")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle = indexNgd

        End If
    End Sub

    Private Sub mChuyenKH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenKH.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này !")
            Exit Sub
        End If

        TrangThai.isAddNew = True
        Dim Index As Integer = gdvKHCT.FocusedRowHandle
        Dim f As New frmChuyenGiaoKH
        f.Tag = Me.Parent.Tag
        f.cbNguoiChuyen.EditValue = Convert.ToInt32(gdvKHCT.GetFocusedRowCellValue("IDTakecare"))
        f.cbNguoiChuyen.Enabled = False
        f._IDKH = Convert.ToInt32(gdvKHCT.GetFocusedRowCellValue("ID"))
        f.ShowDialog()
        gdvKHCT.FocusedRowHandle = Index
        gdvKHCT.ExpandMasterRow(Index, "Thông tin giao dịch")
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        loadDS()
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvKHCT.CalcHitInfo(gdvKH.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
            'ElseIf HitInfo.RowHandle < 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'ElseIf HitInfo.RowHandle >= 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub gdvKH_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvKH.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gdvKH.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvNgd_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvNgd.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuNgd.ShowPopup(gdvKH.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenuNgd_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuNgd.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvNgd.CalcHitInfo(gdvKH.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
            'ElseIf HitInfo.RowHandle < 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'ElseIf HitInfo.RowHandle >= 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub gdvThongTinGiaoDich_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvThongTinGiaoDich.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuSuaQTGD.ShowPopup(gdvKH.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenuSuaQTGD_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuSuaQTGD.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvThongTinGiaoDich.CalcHitInfo(gdvKH.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
            'ElseIf HitInfo.RowHandle < 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'ElseIf HitInfo.RowHandle >= 0 Then
            '    mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub
    'Tai
    Private Sub gdvKHCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvKHCT.RowUpdated
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            AddParameter("@IDHinhThucTT2", gdvKHCT.GetFocusedRowCellValue("IDHinhThucTT2"))
            AddParameter("@HinhThucChungTu", gdvKHCT.GetFocusedRowCellValue("HinhThucChungTu"))
            AddParameterWhere("@ID", gdvKHCT.GetFocusedRowCellValue("ID"))
            If doUpdate("KHACHHANG", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ShowAlert("Đã cập nhật thông tin khách hàng " & gdvKHCT.GetFocusedRowCellValue("ttcMa"))
        Else
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
        End If

    End Sub

    Private Sub rgdbTakecare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rgdbTakecare.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbTakeCare.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub btnXacNhanKhachMoi_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles btnXacNhanKhachMoi.CheckedChanged
        If btnXacNhanKhachMoi.Checked Then
            btnXacNhanKhachMoi.Font = New Font(Me.Font, FontStyle.Bold)
        Else
            btnXacNhanKhachMoi.Font = New Font(Me.Font, FontStyle.Regular)
        End If
    End Sub



    Private Sub btnXacNhanBaoCao_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles btnXacNhanBaoCao.CheckedChanged
        If btnXacNhanBaoCao.Checked Then
            btnXacNhanBaoCao.Font = New Font(Me.Font, FontStyle.Bold)
        Else
            btnXacNhanBaoCao.Font = New Font(Me.Font, FontStyle.Regular)
        End If
    End Sub


    Private Sub riLueChuSoHuu_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueChuSoHuu.ButtonClick
        If e.Button.Index = 1 Then
            lueChuSoHuu.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueTinhThanh_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueTinhThanh.ButtonClick
        If e.Button.Index = 1 Then
            lueTinhThanh.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueKhuCN_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueKhuCN.ButtonClick
        If e.Button.Index = 1 Then
            lueKhuCN.EditValue = Nothing
        End If
    End Sub
End Class
'Tai
Public Class HinhThucChungTu


    Public Shared Function GetDataTable() As DataTable

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("Id", Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("Ten", Type.GetType("System.String")))
        Dim r As DataRow

        r = dt.NewRow
        r("Id") = 1
        r("Ten") = "Không lấy VAT"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 2
        r("Ten") = "Xuất VAT"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 3
        r("Ten") = "Hải Quan"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        Return dt

    End Function



End Class
