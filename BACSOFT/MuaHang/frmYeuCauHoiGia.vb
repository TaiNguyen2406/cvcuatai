Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors

Public Class frmYeuCauHoiGia
    Public _exit As Boolean = False
    Public index As Integer

    Private Sub frmYeuCauHoiGia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 500"
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btNhanVien.Enabled = False
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


#Region "Load DS YC gửi NCC"

    Public Sub LoadDS()
        ShowWaiting("Đang tải danh sách yêu cầu ...")

        Dim sql As String = ""
        If cbTieuChi.EditValue = "Top 500" Then
            sql = " SELECT TOP 500  "
        Else
            sql = " SELECT  "
        End If

        sql &= "   tblYeuCauHoiGia.SoPhieu,tblYeuCauHoiGia.NgayLap,tblYeuCauHoiGia.NgayGui,tblYeuCauHoiGia.NgayPhanHoi,tblYeuCauHoiGia.NgayHuy,"
        sql &= " 	(Case DaGui when 0 then N'Chưa gửi yêu cầu' ELSE tblTuDien.NoiDung END)TrangThai,tblYeuCauHoiGia.TieuDe,tblYeuCauHoiGia.NoiDung,"
        sql &= " 	PHUTRACH.Ten AS PhuTrach,NGUOILAP.Ten As NguoiLap,KHACHHANG.Ten AS TenNCC,KHACHHANG.ttcMa,NGUOIGD.Ten AS NguoiGD,tblYeuCauHoiGia.FileDinhKem"
        sql &= " FROm tblYeuCauHoiGia"
        sql &= " LEFT JOIN tblNCCHoiGia ON tblNCCHoiGia.SoPhieu=tblYeuCauHoiGia.SoPhieu"
        sql &= " INNER JOIN NHANSU AS PHUTRACH ON PHUTRACH.Noictac=74 AND PHUTRACH.ID=tblYeuCauHoiGia.IDPhuTrach"
        sql &= " INNER JOIN NHANSU AS NGUOILAP ON NGUOILAP.Noictac=74 AND NGUOILAP.ID=tblYeuCauHoiGia.IDNguoiLap"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=tblNCCHoiGia.IDNCC"
        sql &= " LEFT JOIN NHANSU AS NGUOIGD ON NGUOIGD.ID=tblNCCHoiGia.IDNgd"
        sql &= " LEFT JOIN tblTuDien ON tblYeuCauHoiGia.TrangThai=tblTuDien.Ma AND tblTuDien.Loai=@LoaiTrangThai"
        sql &= " WHERE 1=1 "

        AddParameter("@LoaiTrangThai", LoaiTuDien.YeuCauHoiGia)
        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,CONVERT(nvarchar,tblYeuCauHoiGia.NgayLap,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND tblYeuCauHoiGia.IDPhuTrach= " & btNhanVien.EditValue
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND tblNCCHoiGia.IDNCC= " & cbKH.EditValue
        End If


        sql &= " ORDER BY Sophieu DESC"


        Dim dt As DataTable = ExecuteSQLDataTable(sql)


        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        CloseWaiting()



    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)
        Dim sql As String = ""
        sql &= " SELECT tblYeuCauHoiGiaCT.ID, tblYeuCauHoiGiaCT.SoPhieu, tblYeuCauHoiGiaCT.NoiDung, tblYeuCauHoiGiaCT.SoLuong, tblYeuCauHoiGiaCT.IDVatTu, tblYeuCauHoiGiaCT.AZ,"
        sql &= " VATTU.Model, TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, TENDONVITINH.Ten AS DVT,tblYeuCauHoiGiaCT.IDYeuCau"
        sql &= " FROM tblYeuCauHoiGiaCT"
        sql &= " LEFT OUTER JOIN VATTU ON tblYeuCauHoiGiaCT.IDVatTu = VATTU.ID "
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu = TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonViTinh = TENDONVITINH.ID "
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat = TENHANGSANXUAT.ID"
        sql &= " WHERE tblYeuCauHoiGiaCT.SoPhieu=" & SoPhieu
        sql &= " ORDER BY tblYeuCauHoiGiaCT.AZ "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvYC.DataSource = tb
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
        'If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "IDTrangThai") = TrangThaiChaoGia.DaXacNhan Then
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        'End If

        'If isShowing Then
        '    ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
        '    Exit Sub
        'End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        index = gdvCT.FocusedRowHandle
        fCNYeuCauHoiGia = New frmCNYeuCauHoiGia
        fCNYeuCauHoiGia.TrangThai.isUpdate = True
        fCNYeuCauHoiGia.SPHoiGia = gdvCT.GetFocusedRowCellValue("SoPhieu")

        fCNYeuCauHoiGia.Tag = Me.Parent.Tag
        fCNYeuCauHoiGia.Show()
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

        'If 
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("SoPhieu"))
    End Sub

    Private Sub gdvCT_RowCellClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    'sql &= " SELECT tblYeuCauHoiGia.SoPhieu,tblYeuCauHoiGia.NgayLap,tblYeuCauHoiGia.NgayGui,tblYeuCauHoiGia.NgayPhanHoi,tblYeuCauHoiGia.NgayHuy,
    'sql &= " 	--(Case DaGui when 0 then N'Chưa gửi yêu cầu' ELSE tblTuDien.NoiDung END)TrangThai,
    'sql &= " 	tblYeuCauHoiGia.TieuDe,tblYeuCauHoiGia.NoiDung,
    'sql &= " 	PHUTRACH.Ten AS PhuTrach,NGUOILAP.Ten As NguoiLap,NGUOILAP.Email as EmailNguoiLap,
    'sql &= " 	KHACHHANG.Ten AS TenNCC,KHACHHANG.ttcMa,KHACHHANG.ttcFax,
    'sql &= " 	NGUOIGD.Ten AS TenNgd,NGUOIGD.Email as EmailNgd,tblYeuCauHoiGia.FileDinhKem
    'sql &= " FROm tblYeuCauHoiGia
    'sql &= " LEFT JOIN tblNCCHoiGia ON tblNCCHoiGia.SoPhieu=tblYeuCauHoiGia.SoPhieu
    'sql &= " INNER JOIN NHANSU AS PHUTRACH ON PHUTRACH.Noictac=74 AND PHUTRACH.ID=tblYeuCauHoiGia.IDPhuTrach
    'sql &= " INNER JOIN NHANSU AS NGUOILAP ON NGUOILAP.Noictac=74 AND NGUOILAP.ID=tblYeuCauHoiGia.IDNguoiLap
    'sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=tblNCCHoiGia.IDNCC
    'sql &= " LEFT JOIN NHANSU AS NGUOIGD ON NGUOIGD.ID=tblNCCHoiGia.IDNgd
    'sql &= " --LEFT JOIN tblTuDien ON tblYeuCauHoiGia.TrangThai=tblTuDien.Ma AND tblTuDien.Loai=@LoaiTrangThai
    'sql &= " 
    'sql &= " SELECT * FROM tblYeuCauHoiGia
    'sql &= " 

#Region "Xuất Excel"

    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        Try
            'OpenFileOnLocal(Utils.XuatExcel.CreateExcelFileYeuCauGuiNCC(gdvCT.GetFocusedRowCellValue("SoPhieu"), CType(gdvYC.DataSource, DataTable), ds.Tables(0).Rows(i), RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang, ds.Tables(1).Rows(0)("Ten").ToString, ds.Tables(1).Rows(0)("Mobile").ToString, ds.Tables(1).Rows(0)("Email").ToString))
        Catch ex As Exception

        End Try

    End Sub


#End Region

    Private Sub btSaoChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSaoChep.ItemClick
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        'If isShowing Then
        '    ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
        '    Exit Sub
        'End If

        'If Not ShowCauHoi("Tạo chào giá mới với nội dung của chào giá được chọn ?") Then
        '    Exit Sub
        'End If

        'TrangThai.isCopy = True
        'fCNChaoGia = New frmCNChaoGia
        'fCNChaoGia.TrangThaiCG.isCopy = True
        'fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("Sophieu")
        'fCNChaoGia.SPYeuCau = gdvCT.GetFocusedRowCellValue("Masodathang")
        'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("Congtrinh")
        'fCNChaoGia.Tag = Me.Tag
        'fCNChaoGia.ShowDialog()
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

    Private Sub rcbFileDinhKem_Popup(sender As System.Object, e As System.EventArgs) Handles rcbFileDinhKem.Popup
        If _exit Then
            _exit = False
            Exit Sub
        End If
        gdvListFile.DataSource = DataSourceDSFile2(CType(sender, PopupContainerEdit).EditValue.ToString)
        'LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
    End Sub

    'Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
    '    Dim tb As New DataTable
    '    tb.Columns.Add("File")
    '    gdvListFile.DataSource = tb
    '    If listFile Is Nothing Then Exit Sub
    '    Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
    '    For Each _url In listUrl
    '        If _url <> "" Then
    '            gdvListFileCT.AddNewRow()
    '            gdvListFileCT.SetFocusedRowCellValue("File", _url)
    '        End If
    '    Next
    '    gdvListFileCT.CloseEditor()
    '    gdvListFileCT.UpdateCurrentRow()

    'End Sub

    Private Sub rcbFileDinhKem_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rcbFileDinhKem.Closed
        'Dim _File As String = ""
        'For i As Integer = 0 To gdvListFileCT.RowCount - 1
        '    _File &= gdvListFileCT.GetRowCellValue(i, "File")
        '    If i < gdvListFileCT.RowCount - 1 Then
        '        _File &= ";"
        '    End If
        'Next

        'AddParameter("@FileDinhKem", _File)
        'AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
        'If doUpdate("tblYeuCauHoiGia", "SoPhieu=@SP") Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        'Else
        '    CType(sender, PopupContainerEdit).EditValue = _File

        'End If
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang)
            End If
            For Each file In openFile.FileNames
                Try
                    path = gdvCT.GetFocusedRowCellValue("SoPhieu") & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang & path)
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

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá file được chọn ?") Then
            Try
                System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang & gdvListFileCT.GetFocusedRowCellValue("File"))
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
            Dim urlfile As String = RootUrlOld & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("NgayLap")).Year.ToString & "\" & UrlHoiHang & e.CellValue

            OpenFileOnLocal(urlfile, e.CellValue, True)

        End If
    End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvYCCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub

    Private Sub pMenuChinh_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChinh.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If

     
    End Sub



    Private Sub gdvCGCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs)
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvYCCT.CalcHitInfo(gdvYC.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenuPhu.ShowPopup(gdvYC.PointToScreen(e.Location))
        End If
    End Sub
End Class
