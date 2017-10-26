Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors

Public Class frmNhapKho
    Public _exit As Boolean = False
    Public index As Integer

    Private Sub frmNhapKho_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 5000"

        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        LoadcbChiPhi()
        gdvDSCP.DataSource = LayDataSourceDSCP()
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '    btNhanVien.Enabled = False
        'End If
    End Sub

#Region "Load DS nhập kho"

    Private sqlWhere As String = ""

    Public Sub LoadDS()

        ShowWaiting("Đang tải danh sách nhập kho ...")

        Dim sql As String = " SELECT "
        If cbTieuChi.EditValue = "Top 5000" Then
            sql &= "  TOP 5000 "
        End If

        sql &= " PHIEUNHAPKHO.ID,NgayThang,PHIEUNHAPKHO.SoPhieu,KHACHHANG.ttcMa,KHACHHANG.Ten AS TenKH, SoPhieuDH,"
        '  sql &= " PHIEUNHAPKHO.TienTruocThue,PHIEUNHAPKHO.TienThue,(PHIEUNHAPKHO.TienTruocThue+PHIEUNHAPKHO.TienThue) AS TongTien,"
        sql &= " (Case PHIEUNHAPKHO.TienTe WHEN 0 then PHIEUNHAPKHO.TienTruocThue ELSE PHIEUNHAPKHO.TienTruocThue * PHIEUNHAPKHO.TyGia END) TienTruocThue,"
        sql &= " (Case PHIEUNHAPKHO.TienTe WHEN 0 then PHIEUNHAPKHO.TienThue ELSE PHIEUNHAPKHO.TienThue * PHIEUNHAPKHO.TyGia END) TienThue,"
        sql &= " (Case PHIEUNHAPKHO.TienTe WHEN 0 then (PHIEUNHAPKHO.TienTruocThue+PHIEUNHAPKHO.TienThue) ELSE (PHIEUNHAPKHO.TienTruocThue+PHIEUNHAPKHO.TienThue) * PHIEUNHAPKHO.TyGia END) TongTien,"
        sql &= " tblTienTe.Ten AS TienTeNK,PHIEUNHAPKHO.TyGia as TyGiaNK,NGUOILAP.Ten AS NguoiLap, "
        sql &= " TAKECARE.Ten AS TakeCare,Kho,PHIEUNHAPKHO.IDKhachHang,PHIEUNHAPKHO.IDNguoiDat as IDTakeCare,"
        sql &= "    tbVC.ID AS IDVC,tbVC.IDDVVC,tbVC.ThoiGian,tbVC.SoBill,tbVC.SoTien,tbVC.SoTienTC,tbVC.TienTe,tbVC.TyGia,tbVC.CanNang,tbVC.GhiChu,tbVC.SL,NGUOINHAPCP.Ten AS NguoiNhapCP,Convert(bit,0)Modify"
        sql &= " FROM PHIEUNHAPKHO"
        sql &= " LEFT JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUNHAPKHO.IDUser=NGUOILAP.ID"
        sql &= " LEFT JOIN NHANSU AS TAKECARE ON PHIEUNHAPKHO.IDNguoiDat=TAKECARE.ID"
        sql &= " LEFT JOIN tblTienTe ON PHIEUNHAPKHO.TienTe=tblTienTe.ID"
        sql &= " LEFT JOIN (SELECT * FROM "
        sql &= " ("
        sql &= "    SELECT *,"
        sql &= "          ROW_NUMBER() OVER (PARTITION BY PhieuTC ORDER BY ThoiGian DESC) AS STT,"
        sql &= " 		Count(PhieuTC) over(PARTITION BY PhieuTC) AS SL"
        sql &= "    FROM ChiPhi WHERE Loai=0"
        sql &= " )tb WHERE STT=1)tbVC ON tbVC.PhieuTC = PHIEUNHAPKHO.SoPhieu "
        sql &= " LEFT JOIN NHANSU AS NGUOINHAPCP ON NGUOINHAPCP.ID=tbVC.IDUser "
        sql &= " WHERE 1=1 "

        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,CONVERT(nvarchar,NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDUser= " & btNhanVien.EditValue
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDKhachhang= " & cbKH.EditValue
        End If

        If sqlWhere <> "" Then
            sql &= sqlWhere
        End If

        sql &= " ORDER BY PHIEUNHAPKHO.SoPhieu DESC"

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
        sql &= " SELECT NHAPKHO.ID, NHAPKHO.IDVattu, ISNULL(NHAPKHO.AZ,row_number() over (order by NHAPKHO.ID)) AS AZ,NHAPKHO.SoPhieu, NHAPKHO.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,DonGia,NhapThue,MucThue,SoHDGTGT,NgayHDGTGT,SoHoaDon, convert(bit,0) as Chon,NHAPKHO.IDDatHang "
        sql &= " FROM NHAPKHO "
        sql &= " INNER JOIN VATTU ON NHAPKHO.IDVatTu=VATTU.ID"
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE NHAPKHO.SoPhieu=@SP ORDER BY AZ,NHAPKHO.ID"

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

    Private Sub LoadcbChiPhi()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE ttcKhachHang=3 order by ttcMa SELECT ID,Ten,TyGia FROM tblTienTe ORDER BY ID")
        If Not ds Is Nothing Then
            rcbDVVC.DataSource = ds.Tables(0)
            cbDVVC.Properties.DataSource = ds.Tables(0)
            rcbTienTe.DataSource = ds.Tables(1)
            cbTienTe.Properties.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mpThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        fCNNhapKho = New frmCNNhapKho
        fCNNhapKho.Tag = Me.Parent.Tag
        fCNNhapKho.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mpSua.ItemClick


        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        index = gdvCT.FocusedRowHandle
        fCNNhapKho = New frmCNNhapKho
        fCNNhapKho.PhieuNK = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNNhapKho.Tag = Me.Parent.Tag
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            fCNNhapKho.gdvVTCT.OptionsBehavior.ReadOnly = True
            fCNNhapKho.mNhapKho.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            fCNNhapKho.btChuyenXK.Visible = False
            fCNNhapKho.btCal.Enabled = False
            fCNNhapKho.btGhi.Enabled = False
            fCNNhapKho.btTichThue.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        fCNNhapKho.ShowDialog()
        gdvCT.FocusedRowHandle = index


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
        ElseIf e.KeyCode = Keys.Enter Then
            If colVCIDDVVC.Visible Then
                If Not gdvCT.IsEditing Then
                    gdvCT.Focus()
                    If gdvCT.FocusedRowHandle < gdvCT.RowCount Then
                        gdvCT.FocusedRowHandle = gdvCT.FocusedRowHandle + 1
                        gdvCT.FocusedColumn = colVCIDDVVC
                    End If
                End If
            End If
        End If
        If e.Control AndAlso e.KeyCode = Keys.S Then
            LuuLai()
        End If
    End Sub

    Private Sub mXemTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemTatCa.ItemClick
        gdvVTCT.Columns("SoPhieu").ClearFilter()
    End Sub


    Private Sub btIn_ItemClick(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim Sql As String = ""
            Sql &= " SELECT (N'Nhà CC: ' + ISNULL(KHACHHANG.Ten,'')) AS TenNCC,(N'Ngày: ' + Convert(nvarchar,NgayThang,103))AS Ngay,"
            Sql &= " (N'Đại diện: ' + ISNULL(NGUOIGD.Ten,'')) AS DaiDien,(N'Nhập tại: ' + ISNULL(Kho,'')) AS Kho,"
            Sql &= " (N'Chức danh: '+ISNULL(NGUOIGD.ChucVu,'') + '  ĐT: ' + ISNULL(NGUOIGD.Mobile,'')) AS ChucDanh,"
            Sql &= " (N'Người nhập: ' + ISNULL(NGUOILAP.Ten,'')) AS NguoiNhap, (N'Đặt hàng: ĐH ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieuDH ) AS DatHang,"
            Sql &= " (N'Phiếu nhập: ' + 'NK ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + PHIEUNHAPKHO.SoPhieu) AS PhieuNhap"
            Sql &= " FROM PHIEUNHAPKHO"
            Sql &= " LEFT JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachHang=KHACHHANG.ID"
            Sql &= " LEFT JOIN PHIEUDATHANG ON PHIEUNHAPKHO.SoPhieuDH=PHIEUDATHANG.SoPhieu"
            Sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUNHAPKHO.IDUser=NGUOILAP.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOIGD ON PHIEUDATHANG.IDNgd=NGUOIGD.ID"
            Sql &= " WHERE PHIEUNHAPKHO.SoPhieu=@SP"

            Sql &= " SELECT (0) AS STT, TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,"
            Sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong"
            Sql &= " FROM NHAPKHO "
            Sql &= " INNER JOIN VATTU ON NHAPKHO.IDVatTu=VATTU.ID"
            Sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            Sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            Sql &= " WHERE SoPhieu=@SP ORDER BY NHAPKHO.AZ,NHAPKHO.ID"

            AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
            Dim ds As DataSet = ExecuteSQLDataSet(Sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next
            End If

            Dim f As New frmIn("Phiếu nhập kho")
            Dim rpt As New rptPhieuNhapKho
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.DataSource = ds

            If chkTenVT.Checked = True And chkHangSX.Checked = False Then
                rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                rpt.cellSTT.WidthF = 40
                rpt.cellSTTCT.WidthF = 40
                rpt.cellTenVT.WidthF = 300
                rpt.cellTenVTCT.WidthF = 300
                rpt.cellMaVT.WidthF = 250
                rpt.cellMaVTCT.WidthF = 250
                rpt.cellDVT.WidthF = 65
                rpt.cellDVTCT.WidthF = 65
                rpt.cellSL.WidthF = 70
                rpt.cellSLCT.WidthF = 70
            ElseIf chkTenVT.Checked = False And chkHangSX.Checked = True Then

                rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                rpt.cellSTT.WidthF = 40
                rpt.cellSTTCT.WidthF = 40
                rpt.cellHangSX.WidthF = 200
                rpt.CellHangSXCT.WidthF = 200
                rpt.cellMaVT.WidthF = 350
                rpt.cellMaVTCT.WidthF = 350
                rpt.cellDVT.WidthF = 65
                rpt.cellDVTCT.WidthF = 65
                rpt.cellSL.WidthF = 70
                rpt.cellSLCT.WidthF = 70
            ElseIf chkTenVT.Checked = False And chkHangSX.Checked = False Then
                rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                rpt.cellSTT.WidthF = 40
                rpt.cellSTTCT.WidthF = 40
                rpt.cellMaVT.WidthF = 550
                rpt.cellMaVTCT.WidthF = 550
                rpt.cellDVT.WidthF = 65
                rpt.cellDVTCT.WidthF = 65
                rpt.cellSL.WidthF = 70
                rpt.cellSLCT.WidthF = 70
            End If

            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub


    Private Sub mDuKienPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienPhaiTra.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieuDH")
        f._SoPhieuXNK = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieu")
        f._PhaiTra = True
        f._Buoc1 = False
        f.ShowDialog()
    End Sub

    Private fUpdateHdDauVao As frmUpdateHdDauVao

    Private Sub mnuNhapHoaDonDauVao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNhapHoaDonDauVao.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", fMain.mnuThueMuaHangTrongNuoc.Name, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        'Dim f As New frmUpdateHoaDon
        'f.ShowDialog()

        gdvVTCT.Columns("Chon").Visible = True

        TrangThai.isAddNew = True

        fUpdateHdDauVao = New frmUpdateHdDauVao
        fUpdateHdDauVao.LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc

        fUpdateHdDauVao.Text = "Nhập hóa đơn mới (" & NguoiDung & ")"
        fUpdateHdDauVao.txtNguoiLienHe.Focus()
        fUpdateHdDauVao.isDangXuatKho = True


        'Bin du lieu chung len hoa don
        fUpdateHdDauVao.cmbDoiTuong.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachHang").ToString



        gHoaDon.Text = "Hóa đơn NCC: " & gdvCT.GetFocusedRowCellValue("TenKH").ToString
        gdvCT.Columns("ttcMa").FilterInfo = New ColumnFilterInfo("[ttcMa] = '" & gdvCT.GetFocusedRowCellValue("ttcMa").ToString & "'")

        txtTongTienHang.EditValue = 0
        txtTongTienThue.EditValue = 0
        txtTongThanhTien.EditValue = 0


        mThem.Enabled = False
        mSua.Enabled = False
        mnuNhapHoaDonDauVao.Enabled = False

        gHoaDon.Visible = True

        ShowAlert("Nhập hóa đơn " & gdvCT.GetFocusedRowCellValue("TenKH").ToString)

    End Sub



    Private Sub btnXemHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnXemHoaDon.Click
        fUpdateHdDauVao.Show()
    End Sub

    Private Sub btnHuyThaoTacHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnHuyThaoTacHoaDon.Click
        gHoaDon.Visible = False
        gdvCT.Columns("ttcMa").FilterInfo = New ColumnFilterInfo()
        mThem.Enabled = True
        mSua.Enabled = True
        mnuNhapHoaDonDauVao.Enabled = True
        fUpdateHdDauVao = Nothing
        gdvVTCT.Columns("Chon").Visible = False
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        txtTongTienHang.EditValue = fUpdateHdDauVao.txtTongTienHang.EditValue
        txtTongTienThue.EditValue = fUpdateHdDauVao.txtTongTienThue.EditValue
        txtTongThanhTien.EditValue = fUpdateHdDauVao.txtTongThanhTien.EditValue
    End Sub


    'Hiển thị menu đưa nội dung vào hóa đơn
    Private calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    Private Sub gdvVTCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvVTCT.MouseDown
        If gHoaDon.Visible = False Then Exit Sub
        On Error Resume Next
        calHitTestHoaDon = gdvVTCT.CalcHitInfo(e.Location)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If calHitTestHoaDon.InRowCell Then
                pMenuHoaDon.ShowPopup(gdvVT.PointToScreen(e.Location))
            End If
        End If
    End Sub

    Private Sub gdvVTCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvVTCT.RowCellClick
        If gHoaDon.Visible = False Then Exit Sub
        If e.RowHandle < 0 Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If e.Column.FieldName = "Chon" And _
                gdvVTCT.GetRowCellValue(e.RowHandle, "SoHoaDon").ToString = "" Then
                Dim st As Boolean = Convert.ToBoolean(CType(gdvVT.DataSource, DataTable).Rows(e.RowHandle)("Chon"))
                CType(gdvVT.DataSource, DataTable).Rows(e.RowHandle)("Chon") = Not st
                gdvVTCT.FocusedColumn = gdvVTCT.Columns(0)
            End If
        End If
    End Sub

    Private Sub DuaVaoHoaDon(indexRow As Integer)

        'Dim idVatTu = gdvVTCT.GetRowCellValue(indexRow, "TenVT")

        Dim ref As String = ChungTu.getRef
        Dim diengiai As String

        'Thêm dòng cho hàng tiền
        With fUpdateHdDauVao.gdvHangTienCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDVattu"))
            diengiai = gdvVTCT.GetRowCellValue(indexRow, "TenVT") & " " & gdvVTCT.GetRowCellValue(indexRow, "Model")
            .SetFocusedRowCellValue("DienGiai", diengiai)
            .SetFocusedRowCellValue("DVT", gdvVTCT.GetRowCellValue(indexRow, "TenDVT"))

            .SetFocusedRowCellValue("SoLuong", gdvVTCT.GetRowCellValue(indexRow, "SoLuong"))
            .SetFocusedRowCellValue("DonGia", gdvVTCT.GetRowCellValue(indexRow, "DonGia"))
            .SetFocusedRowCellValue("TaiKhoanCo", "331")

            If gdvVTCT.GetRowCellValue(indexRow, "IDvattu") Is DBNull.Value Then
                .SetFocusedRowCellValue("TaiKhoanNo", "")
            Else
                .SetFocusedRowCellValue("TaiKhoanNo", "1561")
            End If

            .SetFocusedRowCellValue("IdChiTiet", gdvVTCT.GetRowCellValue(indexRow, "ID"))


        End With


        'Thêm dòng cho thuế
        With fUpdateHdDauVao.gdvThueCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDVattu"))
            .SetFocusedRowCellValue("DienGiai", diengiai)
            .SetFocusedRowCellValue("TaiKhoanNo", "1331")
            .SetFocusedRowCellValue("TaiKhoanCo", "331")
            .SetFocusedRowCellValue("IdChiTiet", gdvVTCT.GetRowCellValue(indexRow, "ID"))

            Dim thanhtien = Math.Round(gdvVTCT.GetRowCellValue(indexRow, "SoLuong") * gdvVTCT.GetRowCellValue(indexRow, "DonGia"), 2, MidpointRounding.AwayFromZero)
            Dim tienthue = Math.Round((thanhtien * 10) / 100, 2, MidpointRounding.AwayFromZero)
            .SetFocusedRowCellValue("ThanhTien", tienthue)
        End With

        txtTongTienHang.EditValue = fUpdateHdDauVao.txtTongTienHang.EditValue
        txtTongTienThue.EditValue = fUpdateHdDauVao.txtTongTienThue.EditValue
        txtTongThanhTien.EditValue = fUpdateHdDauVao.txtTongThanhTien.EditValue

    End Sub

    Private Sub mnuDuaVaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoHD.ItemClick
        Dim indexRow = calHitTestHoaDon.RowHandle
        If gdvVTCT.GetRowCellValue(indexRow, "SoHoaDon").ToString <> "" Then
            ShowCanhBao("Vật tư này đã lập hóa đơn rồi!")
            Exit Sub
        End If
        CType(gdvVT.DataSource, DataTable).Rows(indexRow)("NhapThue") = True
        DuaVaoHoaDon(indexRow)
        ShowAlert("Thêm vật tư " & gdvVTCT.GetRowCellValue(indexRow, "TenVT") & gdvVTCT.GetRowCellValue(indexRow, "Model") & " vào hóa đơn")
    End Sub

    Private Sub mnuDuaVaoHetHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoHetHD.ItemClick
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "SoHoaDon").ToString <> "" Then Continue For
            DuaVaoHoaDon(i)
        Next
        fUpdateHdDauVao.Show()
        ShowAlert("Đã thêm tất cả vào hóa đơn")
    End Sub

    Private Sub mnuDuaMucDaChonVaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaMucDaChonVaoHD.ItemClick
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If Convert.ToBoolean(gdvVTCT.GetRowCellValue(i, "Chon")) = True And gdvVTCT.GetRowCellValue(i, "SoHoaDon").ToString = "" Then
                DuaVaoHoaDon(i)
            End If
        Next
        fUpdateHdDauVao.Show()
        ShowAlert("Đã đưa các mục được chọn tất cả vào hóa đơn")
    End Sub

    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvVTCT.RowCellStyle
        If gdvVTCT.GetRowCellValue(e.RowHandle, "SoHoaDon").ToString <> "" And gdvVTCT.GetRowCellValue(e.RowHandle, "NhapThue") = True Then
            e.Appearance.BackColor = Color.LightPink
            'ElseIf gdvVTCT.GetRowCellValue(e.RowHandle, "SoHoaDon").ToString = "" And gdvVTCT.GetRowCellValue(e.RowHandle, "NhapThue") = True Then
            '    e.Appearance.BackColor = Color.LightSteelBlue
        End If
    End Sub



    Private Sub mnuLocPhieuNhapKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLocPhieuNhapKho.ItemClick
        If mnuLocPhieuNhapKho.Appearance.ForeColor = Color.Red Then
            If ShowCauHoi("Hủy bỏ trạng thái đang lọc dữ liệu ?") Then
                mnuLocPhieuNhapKho.Appearance.ForeColor = mnuNhapHoaDonDauVao.Appearance.ForeColor
                mnuLocPhieuNhapKho.Appearance.Font = New Font(Me.Font, FontStyle.Regular)
                mnuLocPhieuNhapKho.Glyph = My.Resources.UnCheck
                sqlWhere = ""
                cbTieuChi.EditValue = "Top 5000"
                LoadDS()
            End If
        Else
            Dim f As New frmTimKiemNhapXuatKho
            f.Text = "Lọc phiếu nhập kho theo model"
            f.txtNoiDung.Enabled = False
            If f.ShowDialog() = DialogResult.OK Then
                mnuLocPhieuNhapKho.Appearance.ForeColor = Color.Red
                mnuLocPhieuNhapKho.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
                mnuLocPhieuNhapKho.Glyph = My.Resources.Checked
                sqlWhere = f.sqlWhere
                cbTieuChi.EditValue = "Tuỳ chỉnh"
                tbTuNgay.EditValue = f.txtTuNgay.EditValue
                tbDenNgay.EditValue = f.txtDenNgay.EditValue
                LoadDS()
            End If
        End If
    End Sub

    Private Sub mDeNghiVanChuyen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDeNghiVanChuyen.ItemClick
        If mDeNghiVanChuyen.Caption = "Đề nghị chi vận chuyển" Then
            colVCIDDVVC.VisibleIndex = 5
            colVCThoiGian.VisibleIndex = 6
            colVCSoBill.VisibleIndex = 7

            colVCSoTien.VisibleIndex = 8
            colVCCanNang.VisibleIndex = 9
            colVCGhiChu.VisibleIndex = 10
            colVCTienTe.VisibleIndex = 11
            colVCTyGia.VisibleIndex = 12
            colVCNguoiNhapCP.VisibleIndex = 13

            gdvCT.Focus()
            gdvCT.FocusedColumn = colVCIDDVVC
        Else
            colVCIDDVVC.VisibleIndex = -1
            colVCThoiGian.VisibleIndex = -1
            colVCSoBill.VisibleIndex = -1

            colVCSoTien.VisibleIndex = -1
            colVCCanNang.VisibleIndex = -1
            colVCGhiChu.VisibleIndex = -1
            colVCTienTe.VisibleIndex = -1
            colVCTyGia.VisibleIndex = -1
            colVCNguoiNhapCP.VisibleIndex = -1
            mLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'btLuuLai.Visibility = XtraBars.BarItemVisibility.Never

        End If
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        On Error Resume Next
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "IDDVVC" Then
            If IsDBNull(e.CellValue) Then Exit Sub
            If gdvCT.GetRowCellValue(e.RowHandle, "SL") > 1 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        ElseIf e.Column.FieldName = "SoTien" Then
            If IsDBNull(e.CellValue) Then Exit Sub
            If gdvCT.GetRowCellValue(e.RowHandle, "SoTien") <> gdvCT.GetRowCellValue(e.RowHandle, "SoTienTC") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub pMenuChinh_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuChinh.BeforePopup
        If colVCIDDVVC.Visible Then
            ' btLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mDeNghiVanChuyen.Caption = "Ẩn đề nghị chi vận chuyển"
            If IsDBNull(gdvCT.GetFocusedRowCellValue("IDDVVC")) Then
                mThemChiPhiMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                mThemChiPhiMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
            mDuaVaoDSDungChungCP.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            If Not IsDBNull(gdvCT.GetFocusedRowCellValue("SoTienTC")) Then
                If gdvCT.GetFocusedRowCellValue("SoTienTC") <> gdvCT.GetFocusedRowCellValue("SoTien") Then
                    mSuaCPVCDungChung.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Else
                    mSuaCPVCDungChung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            End If
            mThemChiPhiMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Else
            ' btLuuLai.Visibility = XtraBars.BarItemVisibility.Never
            mLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mDeNghiVanChuyen.Caption = "Đề nghị chi vận chuyển"
            mDuaVaoDSDungChungCP.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mSuaCPVCDungChung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mThemChiPhiMoi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "IDDVVC" Then
            gdvCT.SetFocusedRowCellValue("ThoiGian", Now)
        ElseIf e.Column.FieldName = "SoTien" Then
            gdvCT.SetFocusedRowCellValue("TienTe", 0)
        ElseIf e.Column.FieldName = "SoBill" Then
            gdvCT.SetFocusedRowCellValue("CanNang", 0)
        ElseIf e.Column.FieldName = "TienTe" Then
            If IsDBNull(e.Value) Then
                gdvCT.SetFocusedRowCellValue("TyGia", DBNull.Value)
            Else
                Dim r() As DataRow = CType(rcbTienTe.DataSource, DataTable).Select("ID=" & e.Value)
                gdvCT.SetFocusedRowCellValue("TyGia", r(0)("TyGia"))
            End If

        End If

        If e.Column.FieldName <> "Modify" And e.Column.FieldName <> "IDVC" Then
            gdvCT.SetFocusedRowCellValue("Modify", True)
            mLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub gdvCT_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyUp
        If e.KeyCode = Keys.Tab Then
            If gdvCT.FocusedColumn Is colTienTruocThue Then
                If gdvCT.FocusedRowHandle < gdvCT.RowCount Then
                    gdvCT.FocusedRowHandle = gdvCT.FocusedRowHandle + 1
                    gdvCT.FocusedColumn = colVCIDDVVC
                End If
            End If
        End If
    End Sub

    Public Sub LuuLai()
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        gdvCT.BeginUpdate()
        For i As Integer = 0 To gdvCT.RowCount - 1
            If gdvCT.GetRowCellValue(i, "Modify") Then
                AddParameter("@IDDVVC", gdvCT.GetRowCellValue(i, "IDDVVC"))
                AddParameter("@ThoiGian", gdvCT.GetRowCellValue(i, "ThoiGian"))
                AddParameter("@PhieuTC", gdvCT.GetRowCellValue(i, "SoPhieu"))
                AddParameter("@SoBill", gdvCT.GetRowCellValue(i, "SoBill"))
                AddParameter("@SoTien", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@SoTienTC", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@TienTe", gdvCT.GetRowCellValue(i, "TienTe"))
                AddParameter("@TyGia", gdvCT.GetRowCellValue(i, "TyGia"))
                AddParameter("@CanNang", gdvCT.GetRowCellValue(i, "CanNang"))
                AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", False)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvCT.GetRowCellValue(i, "IDVC")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        ShowBaoLoi("Không thêm được chi phí tại PN: " & gdvCT.GetRowCellValue(i, "SoPhieu") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "IDVC"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        ShowBaoLoi("Không cập nhật được chi phí tại PN: " & gdvCT.GetRowCellValue(i, "SoPhieu") & vbCrLf & LoiNgoaiLe)
                    End If
                End If

            End If
        Next

        gdvCT.EndUpdate()
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLuuLai.ItemClick
        LuuLai()
    End Sub

    Public Function LayDataSourceDSCP() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("SoPhieuNK", Type.GetType("System.String"))
        tb.Columns.Add("CanNang", Type.GetType("System.Double"))
        tb.Columns.Add("GhiChu", Type.GetType("System.String"))
        tb.Columns.Add("TienTruocThue", Type.GetType("System.Double"))
        tb.Columns.Add("ChiPhi", Type.GetType("System.Double"))
        tb.Columns.Add("ID", Type.GetType("System.Object"))
        tb.Columns.Add("IDTakeCare", Type.GetType("System.Object"))
        Return tb
    End Function

    Private Sub mThemChiPhiMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemChiPhiMoi.ItemClick
        If ShowCauHoi("Thêm chi phí cho xuất kho " & gdvCT.GetFocusedRowCellValue("SoPhieu") & " ?") Then
            gdvCT.SetFocusedRowCellValue("IDDVVC", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("ThoiGian", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("SoBill", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("SoTien", 0)
            gdvCT.SetFocusedRowCellValue("TienTe", 0)
            gdvCT.SetFocusedRowCellValue("TyGia", 1)
            gdvCT.SetFocusedRowCellValue("CanNang", 0)
            gdvCT.SetFocusedRowCellValue("TienTC", 0)
            gdvCT.SetFocusedRowCellValue("GhiChu", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("IDVC", DBNull.Value)
            gdvCT.SetFocusedRowCellValue("Modify", 0)
        End If
    End Sub

    Private Sub mDuaVaoDSDungChungCP_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuaVaoDSDungChungCP.ItemClick
        If pVCGop.Visible = False Then
            pVCGop.Visible = True
            tbSoBill.EditValue = DBNull.Value
            tbSoTien.EditValue = 0
            cbTienTe.EditValue = CType(0, Byte)
            tbThoiGian.EditValue = Now
        End If

        For i As Integer = 0 To gdvDSCPCT.RowCount - 1
            If gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK") = gdvCT.GetFocusedRowCellValue("SoPhieu") Then
                ShowCanhBao("Số phiếu NK đã có sẵn trong danh sách!")
                Exit Sub
            End If
        Next
        gdvDSCPCT.AddNewRow()
        gdvDSCPCT.SetFocusedRowCellValue("SoPhieuNK", gdvCT.GetFocusedRowCellValue("SoPhieu"))
        gdvDSCPCT.SetFocusedRowCellValue("TienTruocThue", gdvCT.GetFocusedRowCellValue("TienTruocThue"))
        gdvDSCPCT.SetFocusedRowCellValue("CanNang", 0)
        gdvDSCPCT.SetFocusedRowCellValue("ChiPhi", gdvCT.GetFocusedRowCellValue("ChiPhi"))
        gdvDSCPCT.SetFocusedRowCellValue("IDTakeCare", gdvCT.GetFocusedRowCellValue("IDTakeCare"))
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
    End Sub

    Private Sub cbTienTe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTienTe.EditValueChanged
        On Error Resume Next
        Dim r As DataRowView = cbTienTe.GetSelectedDataRow
        tbTyGia.EditValue = r("TyGia")
    End Sub

    Private Sub gdvCT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvCT.DoubleClick
        If colVCIDDVVC.Visible Then
            mDuaVaoDSDungChungCP.PerformClick()
        End If

    End Sub

    Private Sub cbDVVC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbDVVC.EditValueChanged
        tbSoBill.Focus()
    End Sub

    Private Sub gdvDSCPCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvDSCPCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng đang chọn ?") Then
                gdvDSCPCT.DeleteSelectedRows()
                gdvDSCPCT.CloseEditor()
                gdvDSCPCT.UpdateCurrentRow()
            End If
        End If
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If tbSoTien.EditValue Is Nothing Then
            ShowCanhBao("Chưa có thông tin tiền vận chuyển!")
            Exit Sub
        End If
        If gdvDSCPCT.RowCount = 0 Then
            ShowCanhBao("Chưa có thông tin phiếu xuất!")
            Exit Sub
        End If
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
        gdvDSCPCT.BeginUpdate()

        For i As Integer = 0 To gdvDSCPCT.RowCount - 1
            gdvDSCPCT.SetRowCellValue(i, "ChiPhi", Math.Round((gdvDSCPCT.GetRowCellValue(i, "TienTruocThue") / gdvDSCPCT.Columns("TienTruocThue").SummaryItem.SummaryValue * tbSoTien.EditValue), 0))
        Next
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()
        gdvDSCPCT.EndUpdate()


    End Sub


    Private Sub btLuuVCGop_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVCGop.Click
        gdvDSCPCT.CloseEditor()
        gdvDSCPCT.UpdateCurrentRow()

        If gdvDSCPCT.Columns("ChiPhi").SummaryItem.SummaryValue <> tbSoTien.EditValue Then
            ShowCanhBao("Chi phí chưa khớp!")
            Exit Sub
        End If
        gdvDSCPCT.BeginUpdate()
        BeginTransaction()
        Try
            For i As Integer = 0 To gdvDSCPCT.RowCount - 1
                AddParameter("@IDDVVC", cbDVVC.EditValue)
                AddParameter("@ThoiGian", tbThoiGian.EditValue)
                AddParameter("@PhieuTC", gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK"))
                AddParameter("@SoBill", tbSoBill.EditValue)
                AddParameter("@SoTien", gdvDSCPCT.GetRowCellValue(i, "ChiPhi"))
                AddParameter("@SoTienTC", tbSoTien.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@TyGia", tbTyGia.EditValue)
                AddParameter("@CanNang", gdvDSCPCT.GetRowCellValue(i, "CanNang"))
                AddParameter("@GhiChu", gdvDSCPCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", False)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvDSCPCT.GetRowCellValue(i, "ID")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvDSCPCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        Throw New Exception("Không thêm được chi phí tại PN: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvDSCPCT.GetRowCellValue(i, "ID"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        Throw New Exception("Không cập nhật được chi phí tại PN: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuNK") & vbCrLf & LoiNgoaiLe)
                    End If
                End If
            Next
            ComitTransaction()
            gdvDSCPCT.CloseEditor()
            gdvDSCPCT.UpdateCurrentRow()
            gdvDSCPCT.EndUpdate()
            ShowAlert("Đã thực hiện !")
            btXem.PerformClick()
        Catch ex As Exception
            RollBackTransaction()
            gdvDSCPCT.CloseEditor()
            gdvDSCPCT.UpdateCurrentRow()
            gdvDSCPCT.EndUpdate()
            ShowBaoLoi(LoiNgoaiLe)
        End Try


    End Sub

    Private Sub mSuaChiPhiVCChung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaCPVCDungChung.ItemClick
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("SoTien")) Then
            If gdvCT.GetFocusedRowCellValue("SoTienTC") <> gdvCT.GetFocusedRowCellValue("SoTien") Then
                AddParameterWhere("@IDVC", gdvCT.GetFocusedRowCellValue("IDDVVC"))
                AddParameterWhere("@STTC", gdvCT.GetFocusedRowCellValue("SoTienTC"))
                AddParameterWhere("@TG", gdvCT.GetFocusedRowCellValue("ThoiGian"))
                AddParameterWhere("@SB", gdvCT.GetFocusedRowCellValue("SoBill"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT PhieuTC as SoPhieuNK,CanNang,GhiChu,ChiPhi.ID,SoTien As ChiPhi,PHIEUNHAPKHO.TienTruocThue*PHIEUNHAPKHO.TyGIa as TienTruocThue FROM ChiPhi INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=ChiPhi.PhieuTC WHERE Loai=0 AND IDDVVC=@IDVC AND SoTienTC=@STTC AND SoBill=@SB AND ThoiGian=@TG")
                If tb Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvDSCP.DataSource = tb
                    If pVCGop.Visible = False Then
                        pVCGop.Visible = True
                    End If
                    cbDVVC.EditValue = gdvCT.GetFocusedRowCellValue("IDDVVC")
                    tbSoBill.EditValue = gdvCT.GetFocusedRowCellValue("SoBill")
                    tbThoiGian.EditValue = gdvCT.GetFocusedRowCellValue("ThoiGian")
                    tbSoTien.EditValue = gdvDSCPCT.Columns("ChiPhi").SummaryItem.SummaryValue
                    cbTienTe.EditValue = gdvCT.GetFocusedRowCellValue("TienTe")
                    tbTyGia.EditValue = gdvCT.GetFocusedRowCellValue("TyGia")
                End If
            End If
        End If

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        gdvDSCP.DataSource = LayDataSourceDSCP()
        pVCGop.Visible = False
    End Sub

End Class
