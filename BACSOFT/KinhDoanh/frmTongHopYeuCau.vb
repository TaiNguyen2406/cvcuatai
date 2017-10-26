Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmTongHopYeuCau
    Private tbtmp As DataTable
    Private _exit As Boolean = False

    Private Sub frmTongHopYeuCau_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Application.DoEvents()
        LoadCbKhachHang()
        LoadCbPhuTrach()
        LoadCbTrangThai()
        LoadCbPhu()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            cbPhuTrach.Enabled = False
        End If
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
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID ")
        If Not tb Is Nothing Then
            rcbPhuTrach.DataSource = tb
            rcbNguoiNhanXuLy.DataSource = tb
            cbPhuTrach.EditValue = TaiKhoan
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

    Private Sub rcbNguoiNhanXuLy_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNguoiNhanXuLy.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiNhanXuLy.EditValue = Nothing
        End If
    End Sub

    Public Sub LoadYeuCau()
        ShowWaiting("Đang tải yêu cầu ...")
        Dim sql As String = ""
        sql &= " DECLARE @tb  table"
        sql &= " ("
        sql &= " 	STT  int,"
        sql &= " 	NgayThangYCChinh  datetime,"
        sql &= " 	IDYC int,"
        sql &= " 	Chon  bit,"
        sql &= " 	SoPhieu  nvarchar(15),"
        sql &= " 	NgayNhanYeuCau  datetime,"
        sql &= " 	ttcMa  nvarchar(250),"
        sql &= " 	NoiDung  nvarchar(MAX),"
        sql &= " 	NguoiXuLy  nvarchar(250),"
        sql &= " 	PhuTrach  nvarchar(250),"
        sql &= " 	NguoiNhanBaoGia  nvarchar(250),"
        sql &= " 	IDVatTu  int,"
        sql &= " 	SoLuong  float,"
        sql &= " 	Model  nvarchar(500),"
        sql &= " 	TenVT  Nvarchar(500),"
        sql &= " 	HangSX  nvarchar(250),"
        sql &= " 	FileDinhKem  Nvarchar(Max),"
        sql &= " 	MucDoCan  int,"
        sql &= " 	NgayNhanChuyenMa  datetime,"
        sql &= " 	NgayNhanHoiGia  datetime,"
        sql &= " 	GiaCungUng  float,"
        sql &= " 	TGCungUng  Float,"
        sql &= " 	IDTienTeCungUng  int,"
        sql &= " 	HoiThongTin  nvarchar(MAX),"
        sql &= " 	DVT  nvarchar(50),"
        sql &= " 	IDTakeCare  int,"
        sql &= " 	NgayHoiGia  Datetime,"
        sql &= " 	NguoiBaoGia  nvarchar(MAX),"
        sql &= " 	NguoiChuyenMa  Nvarchar(250),"
        sql &= " 	NgayChuyenMa  datetime,"
        sql &= " 	TrangThai  int"
        sql &= " )"
        sql &= " INSERT INTO @tb "
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY NgayNhanYeuCau ) AS STT,BANGYEUCAU.NgayThang as NgayThangYCChinh, YEUCAUDEN.ID AS IDYC, COnvert(bit,0)Chon, YEUCAUDEN.SoPhieu, YEUCAUDEN.NgayNhanYeuCau,KHACHHANG.ttcMa,YEUCAUDEN.NoiDung,NGUOIXULY.Ten AS NguoiXuLy,PHUTRACH.Ten AS PhuTrach,NGUOINHANBAOGIA.Ten AS NguoiNhanBaoGia,"
        sql &= " YEUCAUDEN.IDVatTu,YEUCAUDEN.SoLuong,VATTU.Model,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,YEUCAUDEN.FileDinhKem,YEUCAUDEN.MucDoCan, (CASE WHEN YEUCAUDEN.IDNhanChuyenMa IS NULL THEN NULL ELSE YEUCAUDEN.NgayNhanChuyenMa END) AS NgayNhanChuyenMa,YEUCAUDEN.NgayNhanHoiGia,"
        sql &= " YEUCAUDEN.GiaCungUng,YEUCAUDEN.TGCungUng,YEUCAUDEN.IDTienTeCungUng,YEUCAUDEN.HoiThongTin,TENDONVITINH.Ten AS DVT,BANGYEUCAU.IDTakeCare,YEUCAUDEN.NgayHoiGia,(CASE WHEN YEUCAUDEN.NgayHoiGia IS NULL THEN NULL ELSE NGUOIBAOGIA.Ten END)AS NguoiBaoGia, "
        sql &= " (CASE WHEN YEUCAUDEN.NgayChuyenMa IS NULL THEN NULL ELSE NGUOICHUYENMA.Ten END) AS NguoiChuyenMa, YEUCAUDEN.NgayChuyenMa, YEUCAUDEN.TrangThai"
        sql &= " FROM YEUCAUDEN "
        sql &= " INNER JOIN BANGYEUCAU ON YEUCAUDEN.Sophieu=BANGYEUCAU.Sophieu"
        sql &= " LEFT JOIN KHACHHANG ON BANGYEUCAU.IDkhachhang=KHACHHANG.ID"
        sql &= " LEFT JOIN NHANSU AS PHUTRACH ON BANGYEUCAU.IDTakecare=PHUTRACH.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOIXULY ON YEUCAUDEN.IDNhanChuyenMa=NGUOIXULY.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOINHANBAOGIA ON YEUCAUDEN.IDNhanBaoGia=NGUOINHANBAOGIA.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOIBAOGIA ON YEUCAUDEN.IDHoiGia=NGUOIBAOGIA.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOICHUYENMA ON YEUCAUDEN.IDChuyenMa=NGUOICHUYENMA.ID"
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

        If Not cbNguoiNhanXuLy.EditValue Is Nothing Then
            If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                sql &= " AND YEUCAUDEN.IDNhanBaoGia=@IDNhanXuLy"
            Else
                sql &= " AND YEUCAUDEN.IDNhanChuyenMa=@IDNhanXuLy"
            End If

            AddParameterWhere("@IDNhanXuLy", cbNguoiNhanXuLy.EditValue)
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

            Else
                If Me.Parent.Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayHoiGia,103),103) BETWEEN @TuNgay AND @DenNgay "
                Else
                    sql &= " AND convert(datetime, Convert(nvarchar,YEUCAUDEN.NgayChuyenMa,103),103) BETWEEN @TuNgay AND @DenNgay "
                End If
            End If

            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        sql &= " SELECT * FROM @tb ORDER BY NgayNhanYeuCau "

        sql &= " SELECT  tblQuaTrinhBaoGia.ID,IDYeuCau,ThoiGianBaoGia, NHANSU.Ten AS NguoiBaoGia,"
        sql &= " Gia,tblTuDien.NoiDung AS ThoiGianCungUng,tblTienTe.Ten AS TienTe,GhiChu,ChaoGia,DatHang,ThoiGianChaoGia,ThoiGianDatHang,IDCungUng"
        sql &= " FROM tblQuaTrinhBaoGia"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblQuaTrinhBaoGia.IDCungUng AND NHANSU.Noictac=74"
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=tblQuaTrinhBaoGia.IDTGCungUng AND tblTuDien.Loai=4"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=tblQuaTrinhBaoGia.IDTienTe"
        sql &= "  WHERE tblQuaTrinhBaoGia.IDYeuCau IN (SELECT DISTINCT IDYC FROM @tb)"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            ds.Relations.Add(ds.Tables(0).Columns("IDYC"), ds.Tables(1).Columns("IDYeuCau"))
            ds.Relations.Item(0).RelationName = "Quá trình báo giá"

            gdvYC.DataSource = ds.Tables(0)
            tbtmp = ds.Tables(0).Clone
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

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
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

    Private Sub gdvYC_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvYC.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdvYC.PointToScreen(e.Location))
        End If
    End Sub

  
    Private Sub rcbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTrangThai.ButtonClick
        If e.Button.Index = 1 Then
            cbTrangThai.EditValue = Nothing
        End If
    End Sub

    Private Sub btPhanBoCVBoPhanMua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btPhanBoCVBoPhanMua.ItemClick

    End Sub

    Private Sub btTinhTrangVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVatTu.ItemClick

        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag

        f._IDVatTu = gdvYCCT.GetFocusedRowCellValue("IDVatTu")
        f._HienThongTinNX = True
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
        End If

        f.ShowDialog()
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

End Class
