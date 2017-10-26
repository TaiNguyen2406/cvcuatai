Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmKetQuaXuLyYeuCau
    Private tbtmp As DataTable
    Private _exit As Boolean = False

    Private Sub frmKetQuaXuLyYeuCau_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Application.DoEvents()
        LoadCbKhachHang()
        LoadCbPhuTrach()
        LoadCbTrangThai()
        LoadCbPhu()

        Application.DoEvents()
        ShowWaiting("Đang tải dữ liệu chuyển mã ...")

        CloseWaiting()
        'tbTuNgay.Enabled = False
        'tbDenNgay.Enabled = False
        'cbTieuChiLoc.Enabled = False
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date

        'Threading.Thread.Sleep(2000)
        Application.DoEvents()
        btTaiDS.PerformClick()
    End Sub

    Public Sub LoadCbPhu()
        Dim sql As String = ""
        sql &= " SELECT Ma,NoiDung FROm tblTuDien WHERE Loai=@MucDoCan "
        sql &= " SELECT Ma,NoiDung FROm tblTuDien WHERE Loai=@ThoiGianCungUng "
        sql &= " SELECT ID,Ten FROm TENDONVITINH "
        sql &= " SELECT ID,Ten FROM tblTienTe"
        AddParameterWhere("@MucDoCan", LoaiTuDien.MucDoCan)
        AddParameterWhere("@ThoiGianCungUng", LoaiTuDien.TGCungUng)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbMucDoCan.DataSource = ds.Tables(0)
            cbTGCungUng.DataSource = ds.Tables(1)
            cbDVT.DataSource = ds.Tables(2)
            rcbTienTe.DataSource = ds.Tables(3)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbKhachHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa ")
        If Not tb Is Nothing Then
            rcbMaKH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbPhuTrach()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment=2 ORDER BY ID ")
        If Not tb Is Nothing Then
            rcbPhuTrach.DataSource = tb
            rcbNguoiXuLy.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCbTrangThai()
        AddParameterWhere("@Loai", LoaiTuDien.YeuCauDen)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma ")
        If Not tb Is Nothing Then
            rcbTrangThai.DataSource = tb
            rcbTrangThaiCT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rcbMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKhachHang.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbPhuTrach_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhuTrach.ButtonClick
        If e.Button.Index = 1 Then
            cbPhuTrach.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbNguoiNhanXuLy_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNguoiXuLy.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiXuLy.EditValue = Nothing
        End If
    End Sub

    Public Sub LoadYeuCau()
        ShowWaiting("Đang tải yêu cầu ...")
        Dim sql As String = ""
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY NgayNhanYeuCau ) AS STT,BANGYEUCAU.NgayThang as NgayThangYCChinh, YEUCAUDEN.ID AS IDYC, YEUCAUDEN.SoPhieu, YEUCAUDEN.NgayNhanYeuCau,KHACHHANG.ttcMa,YEUCAUDEN.NoiDung,NGUOIXULY.Ten AS NguoiXuLy,PHUTRACH.Ten AS PhuTrach,"
        sql &= " YEUCAUDEN.IDVatTu,YEUCAUDEN.SoLuong,VATTU.Model,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,YEUCAUDEN.FileDinhKem,YEUCAUDEN.MucDoCan, (CASE WHEN YEUCAUDEN.IDNhanChuyenMa IS NULL THEN NULL ELSE YEUCAUDEN.NgayNhanChuyenMa END) AS NgayNhanChuyenMa,"
        sql &= " TENDONVITINH.Ten AS DVT,BANGYEUCAU.IDTakeCare, BANGYEUCAU.CongTrinh,"
        sql &= " (CASE WHEN YEUCAUDEN.NgayChuyenMa IS NULL THEN NULL ELSE NGUOICHUYENMA.Ten END) AS NguoiChuyenMa, YEUCAUDEN.NgayChuyenMa, YEUCAUDEN.TrangThai,"
        sql &= " TRANGTHAICG.NoiDung AS TrangThaiCG"
        sql &= " FROM YEUCAUDEN "
        sql &= " INNER JOIN BANGYEUCAU ON YEUCAUDEN.Sophieu=BANGYEUCAU.Sophieu"
        If cbLoaiYC.EditValue = "Công trình" Then
            sql &= " AND BANGYEUCAU.CongTrinh = 1 "
        ElseIf cbLoaiYC.EditValue = "Thương mại" Then
            sql &= " AND BANGYEUCAU.CongTrinh = 0 "
        End If
        sql &= " LEFT JOIN CHAOGIA ON YEUCAUDEN.ID=CHAOGIA.IDYeuCau"
        If cbTieuChiLoc.EditValue = "Thời gian chào giá" Then

            sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=CHAOGIA.SoPhieu"
            If cbTieuChiTG.EditValue <> "Tất cả" Then
                sql &= " AND convert(datetime, Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) BETWEEN @TuNgay AND @DenNgay "
            End If
        ElseIf cbTieuChiLoc.EditValue = "Thời gian xuất kho" Then

            sql &= " INNER JOIN XUATKHO ON XUATKHO.IDChaoGia=CHAOGIA.ID"
            sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            If cbTieuChiTG.EditValue <> "Tất cả" Then
                sql &= " AND convert(datetime, Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
            End If
        ElseIf cbTieuChiLoc.EditValue = "Thời gian thu tiền" Then
            sql &= " INNER JOIN View_LoiNhuan ON View_LoiNhuan.MaSoDatHang=BANGYEUCAU.SoPhieu "
            If cbTieuChiTG.EditValue <> "Tất cả" Then
                sql &= " AND convert(datetime, Convert(nvarchar,View_LoiNhuan.Ngay,103),103) BETWEEN @TuNgay AND @DenNgay "
            End If
        End If
        sql &= " LEFT JOIN KHACHHANG ON BANGYEUCAU.IDkhachhang=KHACHHANG.ID"

        sql &= " LEFT JOIN tblTuDien AS TRANGTHAICG ON TRANGTHAICG.Ma=CHAOGIA.TrangThai AND TRANGTHAICG.Loai=2"
        sql &= " LEFT JOIN NHANSU AS PHUTRACH ON BANGYEUCAU.IDTakecare=PHUTRACH.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOIXULY ON YEUCAUDEN.IDNhanChuyenMa=NGUOIXULY.ID"
        sql &= " INNER JOIN NHANSU AS NGUOICHUYENMA ON YEUCAUDEN.IDChuyenMa=NGUOICHUYENMA.ID AND NGUOICHUYENMA.IDDepatment=2"
        sql &= " LEFT JOIN VATTU ON YEUCAUDEN.IDVattu = VATTU.ID "
        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenvattu = TENVATTU.ID "
        sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh = TENDONVITINH.ID "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat = TENHANGSANXUAT.ID"
        sql &= " WHERE 1=1 "

        If Not cbTrangThai.EditValue Is Nothing Then
            sql &= " AND YEUCAUDEN.TrangThai=@TrangThai"
            AddParameterWhere("@TrangThai", cbTrangThai.EditValue)
        End If



        If Not cbPhuTrach.EditValue Is Nothing Then
            sql &= " AND BANGYEUCAU.IDTakeCare=@PhuTrach"
            AddParameterWhere("@PhuTrach", cbPhuTrach.EditValue)
        End If

        If Not cbNguoiXuLy.EditValue Is Nothing Then

            sql &= " AND YEUCAUDEN.IDChuyenMa=@IDChuyenMa"
            AddParameterWhere("@IDChuyenMa", cbNguoiXuLy.EditValue)
        End If

        If Not cbKhachHang.EditValue Is Nothing Then
            sql &= " AND BANGYEUCAU.IDKhachHang=@MaKH"
            AddParameterWhere("@MaKH", cbKhachHang.EditValue)
            colMaKH.Visible = False
        Else
            colMaKH.Visible = True
            colMaKH.VisibleIndex = 1
        End If

        If cbTieuChiTG.EditValue <> "Tất cả" Then
            If cbTieuChiLoc.EditValue = "Thời gian gửi yêu cầu" Then
                sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayNhanYeuCau,103),103) BETWEEN @TuNgay AND @DenNgay "
            ElseIf cbTieuChiLoc.EditValue = "Thời gian nhận xử lý" Then
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayNhanHoiGia,103),103) BETWEEN @TuNgay AND @DenNgay "
                Else
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayNhanChuyenMa,103),103) BETWEEN @TuNgay AND @DenNgay "
                End If

            ElseIf cbTieuChiLoc.EditValue = "Thời gian xử lý" Then
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayHoiGia,103),103) BETWEEN @TuNgay AND @DenNgay "
                Else
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayChuyenMa,103),103) BETWEEN @TuNgay AND @DenNgay "
                End If
            ElseIf cbTieuChiLoc.EditValue = "Thời gian chào giá" Then

            End If

            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If


        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then

            gdvYC.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub gdvYCCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvYCCT.CustomDrawCell
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.VisibleIndex = 0 And view.IsMasterRowEmptyEx(e.RowHandle, 0) And view.IsMasterRowEmptyEx(e.RowHandle, 1) Then
            CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty
        End If
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        LoadYeuCau()
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs)
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvYCCT.CalcHitInfo(gdvYC.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If

        If gdvYCCT.GetMasterRowExpanded(gdvYCCT.FocusedRowHandle) Then
            mSuaQuaTrinhBaoGia.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            mSuaQuaTrinhBaoGia.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub



    Private Sub rcbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTrangThai.ButtonClick
        If e.Button.Index = 1 Then
            cbTrangThai.EditValue = Nothing
        End If
    End Sub


    Private Sub gdvYCCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvYCCT.RowCellClick

        If e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa") & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub rPopupFileCT_Popup(sender As System.Object, e As System.EventArgs) Handles rPopupFileCT.Popup
        gListFileCT.Text = "Danh sách file đính kèm"
        If _exit = True Then Exit Sub
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)

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

    Private Sub tbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChiTG.EditValueChanged
        If cbTieuChiTG.EditValue = "Tất cả" Then
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
            cbTieuChiLoc.Enabled = False
        Else
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
            cbTieuChiLoc.Enabled = True
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(tbDenNgay.EditValue)
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub


    Private Sub gdvListFileCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvListFileCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuFile.ShowPopup(gdvListFile.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenuFile_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuFile.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvListFileCT.CalcHitInfo(gdvListFile.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub mThemFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemFile.ItemClick
        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa")) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa"))
            End If
            For Each file In OpenFile.FileNames
                ShowWaiting("Đang chuyển file lên server ...")
                path = "XL YC" & gdvYCCT.GetFocusedRowCellValue("SoPhieu") & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(file)
                Try
                    IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa") & "\" & path, True)
                    gdvListFileCT.AddNewRow()
                    gdvListFileCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
                CloseWaiting()
            Next
        End If
        _exit = True
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
        SendKeys.Send("{F4}")
    End Sub


    Private Sub rPopupFileCT_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rPopupFileCT.Closed
        Dim _file As String = StrDSFile(gdvListFileCT)
        If _file.Trim = "" Then Exit Sub
        AddParameter("@FileDinhKem", _file)
        AddParameterWhere("@ID", gdvYCCT.GetFocusedRowCellValue("IDYC"))
        If doUpdate("YEUCAUDEN", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CType(sender, PopupContainerEdit).EditValue = _file
        End If
        _exit = False
    End Sub

    Private Sub mXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaFile.ItemClick
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        'If Convert.ToInt16(gdvCT.GetFocusedRowCellValue("TrangThai")) = Convert.ToInt16(TrangThaiYeuCau.DaChaoGia) Then
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then Exit Sub
        'End If
        Dim str() As String = gdvListFileCT.GetFocusedRowCellValue("File").ToString.Split(" ")
        If str(str.Length - 1) = TaiKhoan Then
            ShowAlert("1")
        End If

        If ShowCauHoi("Xoá file được chọn ?") Then
            gdvListFileCT.DeleteSelectedRows()
            If ShowCauHoi("Xoá luôn file trong hệ thống ?") Then
                Try
                    IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvYCCT.GetFocusedRowCellValue("NgayThangYCChinh")).Year.ToString & "\" & UrlKinhDoanh & gdvYCCT.GetFocusedRowCellValue("ttcMa") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If

        End If
    End Sub

    Private Sub pMenuKD_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs)
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvQuaTrinhBaoGia.CalcHitInfo(gdvYC.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

End Class
