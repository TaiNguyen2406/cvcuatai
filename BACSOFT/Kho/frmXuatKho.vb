Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors

Public Class frmXuatKho
    Public _exit As Boolean = False
    Public index As Integer

    Private Sub frmXuatKho_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 500"

        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        LoadcbChiPhi()
        gdvDSCP.DataSource = LayDataSourceDSCP()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            colChietKhau.Visible = False

        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) Then
            mnuCapNhatNgayCT.Enabled = False
        End If

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", "frmHoaDonDauRa", DanhMucQuyen.QuyenThem) Then mnuLapHoaDon.Enabled = False

        gdvDSIn.DataSource = LayDataSourceDSIn()
    End Sub

#Region "Load DS xuất kho"

    Private sqlWhere As String = ""

    Public Sub LoadDS()
        ShowWaiting("Đang tải danh sách xuất kho ...")

        Dim sql As String = " SELECT "
        If cbTieuChi.EditValue = "Top 500" Then
            sql &= "  TOP 500 "
        End If

        sql &= " PHIEUXUATKHO.ID,PHIEUXUATKHO.NgayThang,PHIEUXUATKHO.SoPhieu,KHACHHANG.ttcMa,KHACHHANG.Ten AS TenKH, SoPhieuCG,BANGCHAOGIA.SoPO, PHIEUXUATKHO.CongTrinh, "
        'sql &= " TienTruocThue,TienThue,(TienTruocThue+TienThue) AS TongTien,TienChietKhau,"
        sql &= " (Case PHIEUXUATKHO.TienTe WHEN 0 then PHIEUXUATKHO.TienTruocThue ELSE PHIEUXUATKHO.TienTruocThue * PHIEUXUATKHO.TyGia END) TienTruocThue,"
        sql &= " (Case PHIEUXUATKHO.TienTe WHEN 0 then PHIEUXUATKHO.TienThue ELSE PHIEUXUATKHO.TienThue * PHIEUXUATKHO.TyGia END) TienThue,"
        sql &= " (Case PHIEUXUATKHO.TienTe WHEN 0 then (PHIEUXUATKHO.TienTruocThue+PHIEUXUATKHO.TienThue) ELSE (PHIEUXUATKHO.TienTruocThue+PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.TyGia END) TongTien,"
        sql &= " (Case PHIEUXUATKHO.TienTe WHEN 0 then PHIEUXUATKHO.TienChietKhau ELSE PHIEUXUATKHO.TienChietKhau * PHIEUXUATKHO.TyGia END) TienChietKhau,"
        sql &= " tblTienTe.Ten  AS TienTeXK,PHIEUXUATKHO.TyGia AS TyGiaXK,LyDoXuat,NGUOILAP.Ten AS NguoiLap, "
        sql &= " NGUOIGD.Ten AS NguoiGd,TAKECARE.Ten AS TakeCare,Kho,Error,Finish,PHIEUXUATKHO.IDKhachHang,PHIEUXUATKHO.IDNgd,PHIEUXUATKHO.IDTakeCare,"
        sql &= "    tbVC.ID AS IDVC,tbVC.IDDVVC,tbVC.ThoiGian,tbVC.SoBill,tbVC.SoTien,tbVC.SoTienTC,tbVC.TienTe,tbVC.TyGia,tbVC.CanNang,tbVC.GhiChu,tbVC.SL,NGUOINHAPCP.Ten AS NguoiNhapCP,Convert(bit,0)Modify"
        sql &= " FROM PHIEUXUATKHO"
        sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN BANGCHAOGIA ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu"
        sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUXUATKHO.IDUser=NGUOILAP.ID"
        sql &= " LEFT JOIN NHANSU AS NGUOIGD ON PHIEUXUATKHO.IDNgd=NGUOIGD.ID"
        sql &= " LEFT JOIN NHANSU AS TAKECARE ON PHIEUXUATKHO.IDTakeCare=TAKECARE.ID"
        sql &= " LEFT JOIN tblTienTe ON PHIEUXUATKHO.TienTe=tblTienTe.ID"
        sql &= " LEFT JOIN (SELECT * FROM "
        sql &= " ("
        sql &= "    SELECT *,"
        sql &= "          ROW_NUMBER() OVER (PARTITION BY PhieuTC ORDER BY ThoiGian DESC) AS STT,"
        sql &= " 		Count(PhieuTC) over(PARTITION BY PhieuTC) AS SL"
        sql &= "    FROM ChiPhi WHERE Loai=1"
        sql &= " )tb WHERE STT=1)tbVC ON tbVC.PhieuTC = PHIEUXUATKHO.SoPhieu "
        sql &= " LEFT JOIN NHANSU AS NGUOINHAPCP ON NGUOINHAPCP.ID=tbVC.IDUser "
        sql &= " WHERE 1=1 "

        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND PHIEUXUATKHO.IDUser= " & btNhanVien.EditValue
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND PHIEUXUATKHO.IDKhachhang= " & cbKH.EditValue
        End If

        If sqlWhere <> "" Then
            sql &= sqlWhere
        End If

        sql &= " ORDER BY SoPhieu DESC"

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
        sql &= " SELECT XUATKHO.AZ, XUATKHO.Id ,XUATKHO.SoPhieu, XUATKHO.IDvattu AS IDvattu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,DonGia,XuatThue,MucThue,VATTU.HangTon, XUATKHO.SoHoaDon, convert(bit,0) as Chon "
        sql &= " FROM XUATKHO "
        sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE XuatKho.SoPhieu=@SP"
        sql &= " UNION ALL"
        sql &= " SELECT 9999 AS AZ,XUATKHOAUX.Id,SoPhieu,NULL AS IDvattu,NoiDung AS TenVT,HangSX AS TenHang,Null AS Model,NULL AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,DonGia,XuatThue,MucThue,Null AS HangTon, XUATKHOAUX.SoHoaDon, convert(bit,0) as Chon "
        sql &= " FROM XUATKHOAUX"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=XUATKHOAUX.DonVi"
        sql &= " WHERE XUATKHOAUX.SoPhieu=@SP"
        sql &= " ORDER BY AZ "


        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("AZ") = i + 1
            Next
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
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.Tag = Me.Parent.Tag
        fCNXuatKho.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mpSua.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        index = gdvCT.FocusedRowHandle
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.PhieuXK = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNXuatKho.Tag = Me.Parent.Tag
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            fCNXuatKho.btCal.Enabled = False
            fCNXuatKho.btGhi.Enabled = False
            fCNXuatKho.btTichThue.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            fCNXuatKho.btChuyenXK.Enabled = False
            fCNXuatKho.mXuatKho.Enabled = False
        End If
        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "Finish") Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                fCNXuatKho.btCal.Enabled = False
                fCNXuatKho.btGhi.Enabled = False
                fCNXuatKho.btTichThue.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                fCNXuatKho.btChuyenXK.Enabled = False
                fCNXuatKho.mXuatKho.Enabled = False
            End If
        End If
        fCNXuatKho.ShowDialog()
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

    'Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown

    '    End If
    'End Sub

    Private Sub mXemTatCa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemTatCa.ItemClick
        gdvVTCT.Columns("SoPhieu").ClearFilter()
    End Sub


    Private Sub btIn_ItemClick(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If chkInTheoPO.Checked Then
            InPhieuXuatTheoPO()
        Else
            If gdvDSInCT.RowCount > 0 Then
                InPhieuXuatGop()
            Else
                InPhieuXuat()
            End If

        End If


    End Sub

    Public Sub InPhieuXuatGop()
        Dim _SoLuongPO As Integer = 0
        'Dim _SoPO() As String
        Dim _DSSoXK As String = "("

        For i As Integer = 0 To gdvDSInCT.RowCount - 1
            _DSSoXK &= "'" & gdvDSInCT.GetRowCellValue(i, "SoPhieu") & "'"
            If i < gdvDSInCT.RowCount - 1 Then
                _DSSoXK &= ","
            Else
                _DSSoXK &= ")"
            End If
        Next


        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim Sql As String = ""
            Sql &= " SELECT (N'Khách hàng: ' + ISNULL(KHACHHANG.Ten,'')) AS TenKH,(N'Ngày: ' + Convert(nvarchar,NgayThang,103))AS Ngay,"
            Sql &= " (N'Đại diện: ' + ISNULL(NGUOIGD.Ten,'')) AS DaiDien,(N'Xuất tại: ' + ISNULL(Kho,'')) AS Kho,"
            Sql &= " (N'Chức danh: '+ISNULL(NGUOIGD.ChucVu,'') + '  ĐT: ' + ISNULL(NGUOIGD.Mobile,'')) AS ChucDanh,"
            Sql &= " (N'Người xuất: ' + ISNULL(NGUOILAP.Ten,'')) AS NguoiXuat, (N'Chào giá: CG ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieuCG ) AS ChaoGia,"
            Sql &= " (N'Phiếu xuất: ' + 'XK ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieu) AS PhieuXuat,TAKECARE.Ten AS TakeCare,"
            Sql &= " (N'Lý do xuất kho: '+ ISNULL(LyDoXuat,'')) AS LyDoXuat"
            Sql &= " FROM PHIEUXUATKHO"
            Sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUXUATKHO.IDUser=NGUOILAP.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOIGD ON PHIEUXUATKHO.IDNgd=NGUOIGD.ID"
            Sql &= " LEFT JOIN NHANSU AS TAKECARE ON PHIEUXUATKHO.IDTakeCare=TAKECARE.ID"
            Sql &= " WHERE SoPhieu=@SP"

            Sql &= " SELECT (0) AS STT,"
            If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked = False Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " TENVATTU.Ten AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            Else
                If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Else
                    Sql &= " TENVATTU.Ten AS TenVT,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENHANGSANXUAT.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenHang,"
                Else
                    Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
                Else
                    Sql &= " VATTU.Model, "
                End If
            End If

            Sql &= " TENDONVITINH.Ten AS TenDVT,XUATKHO.SoLuong,XUATKHO.SoPhieu AS PhieuXK,XUATKHO.SoPhieu as SoPO"
            Sql &= " FROM XUATKHO "
            Sql &= " INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
            Sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
            Sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            Sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
            Sql &= " WHERE XUATKHO.SoPhieu IN " & _DSSoXK
            ' Sql &= " ORDER BY SoPhieu"


            Sql &= " UNION ALL "
            Sql &= " SELECT (0)AS STT, NoiDung AS TenVT,HangSX AS TenHang,('')MaVT,TENDONVITINH.Ten AS TenDVT, SoLuong,XUATKHOAUX.SoPhieu as PhieuXK,XUATKHOAUX.SoPhieu as SoPO"
            Sql &= " FROM XUATKHOAUX"
            Sql &= " INNER JOIN PHIEUXUATKHO ON XUATKHOAUX.SoPhieu=PHIEUXUATKHO.SoPhieu"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON XUATKHOAUX.Donvi=TENDONVITINH.ID"
            Sql &= " WHERE XUATKHOAUX.SoPhieu IN " & _DSSoXK
            '  Sql &= " )tb"
            Sql &= " ORDER BY PhieuXK"

            AddParameterWhere("@SP", gdvDSInCT.GetFocusedRowCellValue("SoPhieu"))


            '    AddParameterWhere("@SoPO", gdvCT.GetFocusedRowCellValue("SoPO"))
            Dim ds As DataSet = ExecuteSQLDataSet(Sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next
            End If

            Dim f As New frmIn("Phiếu xuất kho")
            Dim rpt As New rptPhieuXuatKhoGop
            rpt.pLogo.Image = My.Resources.Logo3


            rpt.DataSource = ds

            rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO"

            'rpt.tbHeader.DeleteColumn(rpt.cellPO)
            'rpt.tbDetail.DeleteColumn(rpt.cellPOCT)
            rpt.cellPO.Text = "Số XK"
            If chkBienBan.Checked Then
                rpt.lbTieuDe.Text = "BIÊN BẢN BÀN GIAO"
            End If

            If chkModel.Checked Then
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
            Else
                rpt.tbHeader.DeleteColumn(rpt.cellMaVT)
                rpt.tbDetail.DeleteColumn(rpt.cellMaVTCT)
                If chkTenVT.Checked = True And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)
                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellTenVT.WidthF = 560
                    rpt.cellTenVTCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = True Then

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellHangSX.WidthF = 560
                    rpt.CellHangSXCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                End If
            End If

            'If _SoLuongPO = 1 Then
            '    rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO - PO:" & gdvCT.GetFocusedRowCellValue("SoPO")
            '    rpt.tbHeader.DeleteColumn(rpt.cellPO)
            '    rpt.tbDetail.DeleteColumn(rpt.cellPO)
            'Else
            '    rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO"
            'End If

            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub


    Public Sub InPhieuXuatTheoPO()
        Dim _SoLuongPO As Integer = 0
        'Dim _SoPO() As String
        Dim _DSSoXK As String = "("
        Dim _DSSoPO As String = "("
        If gdvDSInCT.RowCount = 0 Then
            If IsDBNull(gdvCT.GetFocusedRowCellValue("SoPO")) Or gdvCT.GetFocusedRowCellDisplayText("SoPO").ToString = "" Then
                ShowCanhBao("Không có thông tin số PO")
                Exit Sub
            End If
            _SoLuongPO = 1
            _DSSoPO = "'" & gdvCT.GetFocusedRowCellDisplayText("SoPO").ToString & "'"

        Else
            For i As Integer = 0 To gdvDSInCT.RowCount - 1
                _DSSoXK &= "'" & gdvDSInCT.GetRowCellValue(i, "SoPhieu") & "'"
                _DSSoPO &= "'" & gdvDSInCT.GetRowCellValue(i, "SoPO") & "'"
                If i < gdvDSInCT.RowCount - 1 Then
                    _DSSoXK &= ","
                    _DSSoPO &= ","
                Else
                    _DSSoXK &= ")"
                    _DSSoPO &= ")"
                End If
            Next

            Dim tb As DataTable = ExecuteSQLDataTable("SELECT DISTINCT BANGCHAOGIA.SoPO FROM BANGCHAOGIA INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu AND PHIEUXUATKHO.SoPhieu IN " & _DSSoXK)
            If Not tb Is Nothing Then
                _SoLuongPO = tb.Rows.Count
            Else
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
        End If


        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim Sql As String = ""
            Sql &= " SELECT (N'Khách hàng: ' + ISNULL(KHACHHANG.Ten,'')) AS TenKH,(N'Ngày: ' + Convert(nvarchar,NgayThang,103))AS Ngay,"
            Sql &= " (N'Đại diện: ' + ISNULL(NGUOIGD.Ten,'')) AS DaiDien,(N'Xuất tại: ' + ISNULL(Kho,'')) AS Kho,"
            Sql &= " (N'Chức danh: '+ISNULL(NGUOIGD.ChucVu,'') + '  ĐT: ' + ISNULL(NGUOIGD.Mobile,'')) AS ChucDanh,"
            Sql &= " (N'Người xuất: ' + ISNULL(NGUOILAP.Ten,'')) AS NguoiXuat, (N'Chào giá: CG ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieuCG ) AS ChaoGia,"
            Sql &= " (N'Phiếu xuất: ' + 'XK ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieu) AS PhieuXuat,TAKECARE.Ten AS TakeCare,"
            Sql &= " (N'Lý do xuất kho: '+ ISNULL(LyDoXuat,'')) AS LyDoXuat"
            Sql &= " FROM PHIEUXUATKHO"
            Sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUXUATKHO.IDUser=NGUOILAP.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOIGD ON PHIEUXUATKHO.IDNgd=NGUOIGD.ID"
            Sql &= " LEFT JOIN NHANSU AS TAKECARE ON PHIEUXUATKHO.IDTakeCare=TAKECARE.ID"
            Sql &= " WHERE SoPhieu=@SP"

            Sql &= " SELECT (0) AS STT,"
            If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked = False Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " TENVATTU.Ten AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            Else
                If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Else
                    Sql &= " TENVATTU.Ten AS TenVT,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENHANGSANXUAT.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenHang,"
                Else
                    Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
                Else
                    Sql &= " VATTU.Model, "
                End If
            End If

            Sql &= " TENDONVITINH.Ten AS TenDVT,XUATKHO.SoLuong,BANGCHAOGIA.SoPO,XUATKHO.SoPhieu AS PhieuXK"
            Sql &= " FROM XUATKHO "
            Sql &= " INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
            Sql &= " INNER JOIN BANGCHAOGIA ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu "
            If chkInTheoPO.Checked Then
                If gdvDSInCT.RowCount = 0 Then
                    Sql &= " AND BANGCHAOGIA.SoPO = " & _DSSoPO
                Else
                    Sql &= " AND BANGCHAOGIA.SoPO IN " & _DSSoPO
                End If
            End If

            Sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
            Sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            Sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "




            Sql &= " UNION ALL "
            Sql &= " SELECT (0)AS STT, NoiDung AS TenVT,HangSX AS TenHang,('')MaVT,TENDONVITINH.Ten AS TenDVT, SoLuong,BANGCHAOGIA.SoPO,XUATKHOAUX.SoPhieu as PhieuXK"
            Sql &= " FROM XUATKHOAUX"
            Sql &= " INNER JOIN PHIEUXUATKHO ON XUATKHOAUX.SoPhieu=PHIEUXUATKHO.SoPhieu"
            Sql &= " INNER JOIN BANGCHAOGIA ON PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu "
            If chkInTheoPO.Checked Then
                If gdvDSInCT.RowCount = 0 Then
                    Sql &= " AND BANGCHAOGIA.SoPO = " & _DSSoPO
                Else
                    Sql &= " AND BANGCHAOGIA.SoPO IN " & _DSSoPO
                End If
            End If
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON XUATKHOAUX.Donvi=TENDONVITINH.ID"
            Sql &= " ORDER BY SoPO,PhieuXK"

            If chkInTheoPO.Checked Then
                If gdvDSInCT.RowCount = 0 Then
                    AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
                Else
                    AddParameterWhere("@SP", gdvDSInCT.GetFocusedRowCellValue("SoPhieu"))
                End If
            End If

            '    AddParameterWhere("@SoPO", gdvCT.GetFocusedRowCellValue("SoPO"))
            Dim ds As DataSet = ExecuteSQLDataSet(Sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next
            End If

            Dim f As New frmIn("Phiếu xuất kho")
            Dim rpt As New rptPhieuXuatKhoGop
            rpt.pLogo.Image = My.Resources.Logo3


            rpt.DataSource = ds
            If _SoLuongPO = 1 Then
                rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO - PO:" & gdvCT.GetFocusedRowCellValue("SoPO")
                rpt.tbHeader.DeleteColumn(rpt.cellPO)
                rpt.tbDetail.DeleteColumn(rpt.cellPOCT)
            Else
                rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO"
            End If
            If chkBienBan.Checked Then
                rpt.lbTieuDe.Text = "BIÊN BẢN BÀN GIAO"
            End If

            If chkModel.Checked Then
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
            Else
                rpt.tbHeader.DeleteColumn(rpt.cellMaVT)
                rpt.tbDetail.DeleteColumn(rpt.cellMaVTCT)
                If chkTenVT.Checked = True And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)
                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellTenVT.WidthF = 560
                    rpt.cellTenVTCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = True Then

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellHangSX.WidthF = 560
                    rpt.CellHangSXCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                End If
            End If

            'If _SoLuongPO = 1 Then
            '    rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO - PO:" & gdvCT.GetFocusedRowCellValue("SoPO")
            '    rpt.tbHeader.DeleteColumn(rpt.cellPO)
            '    rpt.tbDetail.DeleteColumn(rpt.cellPO)
            'Else
            '    rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO"
            'End If

            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Public Sub InPhieuXuat()
        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim Sql As String = ""
            Sql &= " SELECT (N'Khách hàng: ' + ISNULL(KHACHHANG.Ten,'')) AS TenKH,(N'Ngày: ' + Convert(nvarchar,NgayThang,103))AS Ngay,"
            Sql &= " (N'Đại diện: ' + ISNULL(NGUOIGD.Ten,'')) AS DaiDien,(N'Xuất tại: ' + ISNULL(Kho,'')) AS Kho,"
            Sql &= " (N'Chức danh: '+ISNULL(NGUOIGD.ChucVu,'') + '  ĐT: ' + ISNULL(NGUOIGD.Mobile,'')) AS ChucDanh,"
            Sql &= " (N'Người xuất: ' + ISNULL(NGUOILAP.Ten,'')) AS NguoiXuat, (N'Chào giá: CG ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieuCG ) AS ChaoGia,"
            Sql &= " (N'Phiếu xuất: ' + 'XK ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieu) AS PhieuXuat,TAKECARE.Ten AS TakeCare,"
            Sql &= " (N'Lý do xuất kho: '+ ISNULL(LyDoXuat,'')) AS LyDoXuat"
            Sql &= " FROM PHIEUXUATKHO"
            Sql &= " LEFT JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachHang=KHACHHANG.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOILAP ON PHIEUXUATKHO.IDUser=NGUOILAP.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOIGD ON PHIEUXUATKHO.IDNgd=NGUOIGD.ID"
            Sql &= " LEFT JOIN NHANSU AS TAKECARE ON PHIEUXUATKHO.IDTakeCare=TAKECARE.ID"
            Sql &= " WHERE SoPhieu=@SP"

            Sql &= " SELECT (0) AS STT,"
            If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked = False Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            ElseIf chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " TENVATTU.Ten AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
            ElseIf chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked And chkModel.Checked Then
                Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                Sql &= " VATTU.Model, "
            Else
                If chkThongSo.Checked And chkTenVT.Checked And chkHangSX.Checked = False And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENVATTU.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenVT,"
                Else
                    Sql &= " TENVATTU.Ten AS TenVT,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(TENHANGSANXUAT.Ten,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS TenHang,"
                Else
                    Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
                End If

                If chkThongSo.Checked And chkTenVT.Checked = False And chkHangSX.Checked And chkModel.Checked = False Then
                    Sql &= " (ISNULL(VATTU.Model,N'')+ (CASE WHEN ltrim(rtrim(VATTU.ThongSo))=N'' THEN N'' ELSE Char(10) + char(13) END)  + ISNULL(VATTU.ThongSo,N'')) AS Model,"
                Else
                    Sql &= " VATTU.Model, "
                End If
            End If

            Sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong"
            Sql &= " FROM XUATKHO "
            Sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
            Sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            Sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            Sql &= " WHERE SoPhieu=@SP"
            Sql &= " UNION ALL "
            Sql &= " SELECT (0)AS STT, NoiDung AS TenVT,HangSX AS TenHang,('')MaVT,TENDONVITINH.Ten AS TenDVT, SoLuong"
            Sql &= " FROM XUATKHOAUX"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON XUATKHOAUX.Donvi=TENDONVITINH.ID"
            Sql &= " WHERE SoPhieu=@SP"

            AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("SoPhieu"))
            Dim ds As DataSet = ExecuteSQLDataSet(Sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next
            End If

            Dim f As New frmIn("Phiếu xuất kho")
            Dim rpt As New rptPhieuXuatKho
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.DataSource = ds
            If chkBienBan.Checked Then
                rpt.lbTieuDe.Text = "BIÊN BẢN BÀN GIAO"
            End If

            If chkModel.Checked Then
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
            Else
                rpt.tbHeader.DeleteColumn(rpt.cellMaVT)
                rpt.tbDetail.DeleteColumn(rpt.cellMaVTCT)
                If chkTenVT.Checked = True And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)
                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellTenVT.WidthF = 560
                    rpt.cellTenVTCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = True Then

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellHangSX.WidthF = 560
                    rpt.CellHangSXCT.WidthF = 560
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 80
                    rpt.cellSLCT.WidthF = 80
                ElseIf chkTenVT.Checked = False And chkHangSX.Checked = False Then
                    rpt.tbHeader.DeleteColumn(rpt.cellHangSX)
                    rpt.tbDetail.DeleteColumn(rpt.CellHangSXCT)

                    rpt.tbHeader.DeleteColumn(rpt.cellTenVT)
                    rpt.tbDetail.DeleteColumn(rpt.cellTenVTCT)

                    rpt.cellSTT.WidthF = 40
                    rpt.cellSTTCT.WidthF = 40
                    rpt.cellDVT.WidthF = 65
                    rpt.cellDVTCT.WidthF = 65
                    rpt.cellSL.WidthF = 70
                    rpt.cellSLCT.WidthF = 70
                End If
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

    Private Sub btInDC_Click(sender As System.Object, e As System.EventArgs) Handles btInDC.Click
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvCT.GetFocusedRowCellValue("IDTakeCare")
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvCT.GetFocusedRowCellValue("IDNgd")
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=74 "
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=" & gdvCT.GetFocusedRowCellValue("IDKhachHang")
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



    Private fUpdateHoaDon As frmUpdateHoaDon

    Private Sub mnuLapHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLapHoaDon.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", fMain.mnuThueBanHangHoa.Name, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        'Dim f As New frmUpdateHoaDon
        'f.ShowDialog()

        gdvVTCT.Columns("Chon").Visible = True

        TrangThai.isAddNew = True
        fUpdateHoaDon = New frmUpdateHoaDon
        fUpdateHoaDon.TagX = fMain.mnuThueBanHangHoa.Name
        fUpdateHoaDon.LoaiCT2 = ChungTu.LoaiCT2.BanHangHoa
        fUpdateHoaDon.Text = "Lập hóa đơn mới (" & NguoiDung & ")"
        LapHoaDon()

        ShowAlert("Lập hóa đơn " & gdvCT.GetFocusedRowCellValue("TenKH").ToString)

    End Sub

    Private Sub LapHoaDon()


        fUpdateHoaDon.txtNguoiLienHe.Focus()
        fUpdateHoaDon.isDangXuatKho = True

        'Bin du lieu chung len hoa don
        fUpdateHoaDon.cmbDoiTuong.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachHang").ToString


        gHoaDon.Text = "Hóa đơn " & gdvCT.GetFocusedRowCellValue("TenKH").ToString
        gdvCT.Columns("ttcMa").FilterInfo = New ColumnFilterInfo("[ttcMa] = '" & gdvCT.GetFocusedRowCellValue("ttcMa").ToString & "'")

        txtTongTienHang.EditValue = 0
        txtTongTienThue.EditValue = 0
        txtTongThanhTien.EditValue = 0


        mThem.Enabled = False
        mSua.Enabled = False
        mnuLapHoaDon.Enabled = False
        gHoaDon.Visible = True

    End Sub

    Private Sub btnXemHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnXemHoaDon.Click
        fUpdateHoaDon.Show()
    End Sub

    Private Sub btnHuyThaoTacHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnHuyThaoTacHoaDon.Click
        gHoaDon.Visible = False
        gdvCT.Columns("ttcMa").FilterInfo = New ColumnFilterInfo()
        mThem.Enabled = True
        mSua.Enabled = True
        mnuLapHoaDon.Enabled = True
        fUpdateHoaDon = Nothing
        gdvVTCT.Columns("Chon").Visible = False
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

    Private Sub DuaVaoHoaDon(indexRow As Integer)

        'Dim idVatTu = gdvVTCT.GetRowCellValue(indexRow, "TenVT")

        For i As Integer = 0 To fUpdateHoaDon.gdvHangTienCT.RowCount - 1
            If Not fUpdateHoaDon.gdvHangTienCT.GetRowCellValue(i, "IdChiTiet") Is DBNull.Value _
                AndAlso gdvVTCT.GetRowCellValue(indexRow, "Id") = fUpdateHoaDon.gdvHangTienCT.GetRowCellValue(i, "IdChiTiet") _
                AndAlso Not gdvVTCT.GetRowCellValue(indexRow, "IdVatTu") Is DBNull.Value Then
                Exit Sub
            End If
        Next


        Dim ref As String = ChungTu.getRef
        Dim diengiai As String

        'Thêm dòng cho hàng tiền
        With fUpdateHoaDon.gdvHangTienCT

            .AddNewRow()
            .SetFocusedRowCellValue("Chon", False)
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDvattu"))
            diengiai = gdvVTCT.GetRowCellValue(indexRow, "TenVT") & " " & gdvVTCT.GetRowCellValue(indexRow, "Model")

            'Lấy mặc định diễn giải từ hóa đơn đầu vào
            Try
                Dim sql As String = "SELECT TOP 1 CHUNGTUCHITIET.DienGiai FROM CHUNGTU LEFT OUTER JOIN "
                sql &= "CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
                sql &= "WHERE CHUNGTU.LoaiCT = 2 AND CHUNGTUCHITIET.IdVatTu = @IdVatTu AND CHUNGTUCHITIET.ButToan = 1 ORDER BY NgayCT DESC "
                AddParameter("@IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDvattu"))
                Dim dtDienGiai As DataTable = ExecuteSQLDataTable(sql)
                If Not dtDienGiai Is Nothing AndAlso dtDienGiai.Rows.Count > 0 Then
                    diengiai = dtDienGiai.Rows(0)(0).ToString
                End If
            Catch ex As Exception
            End Try

            .SetFocusedRowCellValue("DienGiai", diengiai)
            .SetFocusedRowCellValue("DVT", gdvVTCT.GetRowCellValue(indexRow, "TenDVT"))
            .SetFocusedRowCellValue("SoLuong", gdvVTCT.GetRowCellValue(indexRow, "SoLuong"))
            .SetFocusedRowCellValue("DonGia", gdvVTCT.GetRowCellValue(indexRow, "DonGia"))
            .SetFocusedRowCellValue("ThanhTien", gdvVTCT.GetRowCellValue(indexRow, "DonGia") * gdvVTCT.GetRowCellValue(indexRow, "SoLuong"))
            .SetFocusedRowCellValue("TaiKhoanNo", "131")
            If gdvVTCT.GetRowCellValue(indexRow, "IDvattu") Is DBNull.Value Then
                '.SetFocusedRowCellValue("TaiKhoanCo", "5113")
                .SetFocusedRowCellValue("TaiKhoanCo", "5113")
            Else
                '.SetFocusedRowCellValue("TaiKhoanCo", "5111")
                .SetFocusedRowCellValue("TaiKhoanCo", "5111")
            End If

            .SetFocusedRowCellValue("IdChiTiet", gdvVTCT.GetRowCellValue(indexRow, "Id"))

        End With

        'Thêm dòng cho thuế
        With fUpdateHoaDon.gdvThueCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("IdVatTu", gdvVTCT.GetRowCellValue(indexRow, "IDvattu"))
            .SetFocusedRowCellValue("DienGiai", diengiai)

            .SetFocusedRowCellValue("TaiKhoanNo", "131")
            .SetFocusedRowCellValue("TaiKhoanCo", "3331")
            .SetFocusedRowCellValue("IdChiTiet", gdvVTCT.GetRowCellValue(indexRow, "Id"))

            Dim thanhtien = Math.Round(gdvVTCT.GetRowCellValue(indexRow, "SoLuong") * gdvVTCT.GetRowCellValue(indexRow, "DonGia"), 0, MidpointRounding.AwayFromZero)
            Dim tienthue = Math.Round((thanhtien * 10) / 100, 0, MidpointRounding.AwayFromZero)
            .SetFocusedRowCellValue("ThanhTien", tienthue)
        End With

        txtTongTienHang.EditValue = fUpdateHoaDon.txtTongTienHang.EditValue
        txtTongTienThue.EditValue = fUpdateHoaDon.txtTongTienThue.EditValue
        txtTongThanhTien.EditValue = fUpdateHoaDon.txtTongThanhTien.EditValue


    End Sub

    Private Sub mnuDuaVaoHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoHoaDon.ItemClick

        Dim indexRow = calHitTestHoaDon.RowHandle

        If gdvVTCT.GetRowCellValue(indexRow, "SoHoaDon").ToString <> "" Then
            ShowCanhBao("Vật tư này đã lập hóa đơn rồi!")
            Exit Sub
        End If

        DuaVaoHoaDon(indexRow)
        ShowAlert("Thêm vật tư " & gdvVTCT.GetRowCellValue(indexRow, "TenVT") & gdvVTCT.GetRowCellValue(indexRow, "Model") & " vào hóa đơn")

    End Sub

    Private Sub mnuDuaHetVaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaHetVaoHD.ItemClick
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "SoHoaDon").ToString <> "" Then Continue For
            DuaVaoHoaDon(i)
        Next
        fUpdateHoaDon.Show()
        ShowAlert("Đã thêm tất cả vào hóa đơn")
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        txtTongTienHang.EditValue = fUpdateHoaDon.txtTongTienHang.EditValue
        txtTongTienThue.EditValue = fUpdateHoaDon.txtTongTienThue.EditValue
        txtTongThanhTien.EditValue = fUpdateHoaDon.txtTongThanhTien.EditValue
    End Sub


    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvVTCT.RowCellStyle
        If gdvVTCT.GetRowCellValue(e.RowHandle, "SoHoaDon").ToString <> "" Then
            e.Appearance.BackColor = Color.LightPink
        End If
    End Sub

    Private Sub mnuDuaMucDaChonVaoHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaMucDaChonVaoHD.ItemClick
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If Convert.ToBoolean(gdvVTCT.GetRowCellValue(i, "Chon")) = True And gdvVTCT.GetRowCellValue(i, "SoHoaDon").ToString = "" Then
                DuaVaoHoaDon(i)
            End If
        Next
        fUpdateHoaDon.Show()
        ShowAlert("Đã đưa các mục được chọn tất cả vào hóa đơn")
    End Sub

    Private Sub gdvVTCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvVTCT.RowCellClick
        If gHoaDon.Visible = False Then Exit Sub
        If e.RowHandle < 0 Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If e.Column.FieldName = "Chon" And
                gdvVTCT.GetRowCellValue(e.RowHandle, "SoHoaDon").ToString = "" Then
                Dim st As Boolean = Convert.ToBoolean(CType(gdvVT.DataSource, DataTable).Rows(e.RowHandle)("Chon"))
                CType(gdvVT.DataSource, DataTable).Rows(e.RowHandle)("Chon") = Not st
                gdvVTCT.FocusedColumn = gdvVTCT.Columns(0)
            End If
        End If
    End Sub


    Private Sub mnuLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuLoc.ItemClick
        If mnuLoc.Appearance.ForeColor = Color.Red Then
            If ShowCauHoi("Hủy bỏ trạng thái đang lọc dữ liệu ?") Then
                mnuLoc.Appearance.ForeColor = mnuLapHoaDon.Appearance.ForeColor
                mnuLoc.Appearance.Font = New Font(Me.Font, FontStyle.Regular)
                mnuLoc.Glyph = My.Resources.UnCheck
                sqlWhere = ""
                cbTieuChi.EditValue = "Top 500"
                LoadDS()
            End If
        Else
            Dim f As New frmTimKiemNhapXuatKho
            f.Text = "Lọc phiếu xuất kho"
            If f.ShowDialog() = DialogResult.OK Then
                mnuLoc.Appearance.ForeColor = Color.Red
                mnuLoc.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
                mnuLoc.Glyph = My.Resources.Checked
                sqlWhere = f.sqlWhere
                cbTieuChi.EditValue = "Tuỳ chỉnh"
                tbTuNgay.EditValue = f.txtTuNgay.EditValue
                tbDenNgay.EditValue = f.txtDenNgay.EditValue
                LoadDS()
            End If
        End If
    End Sub


    Private Sub mnuCapNhatHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCapNhatHoaDon.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", fMain.mnuThueBanHangHoa.Name, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Width = 680
        f.Height = 65
        f.FormBorderStyle = FormBorderStyle.FixedDialog
        f.MinimizeBox = False
        f.MaximizeBox = False
        f.Text = "Cập nhật hóa đơn " & gdvCT.GetFocusedRowCellValue("TenKH")
        f.StartPosition = FormStartPosition.CenterParent

        Dim btnChon As New DevExpress.XtraEditors.SimpleButton
        btnChon.Dock = DockStyle.Right
        btnChon.Text = "Chọn"
        btnChon.Cursor = Cursors.Hand
        btnChon.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
        btnChon.Appearance.ForeColor = Color.Red
        btnChon.Image = My.Resources.Accept_24


        Dim btnHuy As New DevExpress.XtraEditors.SimpleButton
        btnHuy.Dock = DockStyle.Right
        btnHuy.Text = "Đóng"
        btnHuy.Cursor = Cursors.Hand
        btnHuy.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
        btnHuy.Appearance.ForeColor = Color.Navy
        btnHuy.Image = My.Resources.close_24
        AddHandler btnHuy.Click, Sub(senderX As System.Object, eX As System.EventArgs)
                                     f.Close()
                                 End Sub

        Dim gdv As New DevExpress.XtraEditors.LookUpEdit
        gdv.EditValue = DBNull.Value
        gdv.Properties.AutoHeight = False
        gdv.Dock = DockStyle.Fill
        gdv.BackColor = Color.WhiteSmoke
        gdv.Properties.NullText = "[Chọn hóa đơn nháp cần cập nhật]"
        gdv.Properties.ValueMember = "Id"
        gdv.Properties.DisplayMember = "NoiDung"
        gdv.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoiDung"))
        gdv.Properties.ShowHeader = False
        gdv.Properties.DropDownItemHeight = 25
        gdv.Properties.AppearanceDropDown.Font = New Font(Me.Font.Name, 10)


        Dim sql As String = "SELECT Id, N'Số HĐ: ' + SoHD + ': ' + convert(nvarchar,NgayHD,103) +  ' - '  + TenKH as NoiDung FROM CHUNGTU  WHERE LoaiCT = @LoaiCT AND TrangThai = @TrangThai and IdKH = @IdKH"
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
        AddParameter("@TrangThai", HoaDonGTGT.TrangThaiHoaDon.TrangThai.HoaDonNhap)
        AddParameter("@IdKH", gdvCT.GetFocusedRowCellValue("IDKhachHang"))

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdv.Properties.DataSource = dt


        AddHandler btnChon.Click,
            Sub(senderX As System.Object, eX As System.EventArgs)
                If gdv.EditValue Is DBNull.Value Then
                    ShowCanhBao("Chưa chọn hóa đơn nháp cần cập nhật!")
                    Exit Sub
                End If
                gdvVTCT.Columns("Chon").Visible = True
                fUpdateHoaDon = New frmUpdateHoaDon
                fUpdateHoaDon.LoaiCT2 = ChungTu.LoaiCT2.BanHangHoa
                fUpdateHoaDon.Text = "Cập nhật hóa đơn (" & NguoiDung & ")"
                fUpdateHoaDon.idHoaDon = gdv.EditValue
                TrangThai.isUpdate = True
                LapHoaDon()
                f.Close()
                fUpdateHoaDon.ShowDialog()
                SimpleButton1_Click(sender, e)
            End Sub

        f.Controls.Add(gdv)
        f.Controls.Add(btnChon)
        f.Controls.Add(btnHuy)

        Me.Enabled = False

        f.ShowDialog()

        Me.Enabled = True


    End Sub

    Public Function LayDataSourceDSIn() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("SoPhieu", Type.GetType("System.String"))
        tb.Columns.Add("SoPO", Type.GetType("System.String"))
        tb.Columns.Add("IDKhachHang", Type.GetType("System.Int32"))
        Return tb
    End Function

    Private Sub mDuaVaoDSIn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuaVaoDSIn.ItemClick


        'If gdvCT.GetFocusedRowCellDisplayText("SoPO").ToString.Trim = "" Then
        '    ShowCanhBao("Tính năng này tạm thời chỉ áp dụng với xuất kho theo PO !")
        '    Exit Sub
        'End If
        For i As Integer = 0 To gdvDSInCT.RowCount - 1
            If gdvDSInCT.GetRowCellValue(i, "SoPhieu") = gdvCT.GetFocusedRowCellValue("SoPhieu") Then
                ShowCanhBao("Số phiếu XK đã có sẵn trong danh sách!")
                Exit Sub
            End If
            If gdvDSInCT.GetRowCellValue(i, "IDKhachHang") <> gdvCT.GetFocusedRowCellValue("IDKhachHang") Then
                ShowCanhBao("Tính năng này chỉ áp dụng đối với 1 khách hàng duy nhất!")
                Exit Sub
            End If
        Next

        If pXuatGop.Visible = False Then
            pXuatGop.Visible = True
        End If
        gdvDSInCT.AddNewRow()
        gdvDSInCT.SetFocusedRowCellValue("SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu"))
        gdvDSInCT.SetFocusedRowCellValue("SoPO", gdvCT.GetFocusedRowCellValue("SoPO"))
        gdvDSInCT.SetFocusedRowCellValue("IDKhachHang", gdvCT.GetFocusedRowCellValue("IDKhachHang"))
        gdvDSInCT.CloseEditor()
        gdvDSInCT.UpdateCurrentRow()
    End Sub



    Private Sub ListXK_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        'If ListXK.SelectedItem Is Nothing Then Exit Sub
        'If e.KeyCode = Keys.Delete Then
        '    If ShowCauHoi("Xóa mục đang chọn ?") Then
        '        ListXK.Items.Remove(ListXK.SelectedItem)
        '    End If
        'End If
    End Sub

    Private Sub btClose_11Click(sender As System.Object, e As System.EventArgs) Handles btClose.Click
        gdvDSIn.DataSource = LayDataSourceDSIn()
        pXuatGop.Visible = False
    End Sub

    Private Sub mNhapThongTinVC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhapThongTinVC.ItemClick
        If mNhapThongTinVC.Caption = "Đề nghị chi vận chuyển" Then
            colVCDVVC.VisibleIndex = 5
            colVCThoiGian.VisibleIndex = 6
            colVCSoBill.VisibleIndex = 7

            colVCSoTien.VisibleIndex = 8
            colVCCanNang.VisibleIndex = 9
            colVCGhiChu.VisibleIndex = 10
            colVCTienTe.VisibleIndex = 11
            colVCTyGia.VisibleIndex = 12
            colVCNguoiNhapCP.VisibleIndex = 13

            gdvCT.Focus()
            gdvCT.FocusedColumn = colVCDVVC
        Else
            colVCDVVC.VisibleIndex = -1
            colVCThoiGian.VisibleIndex = -1
            colVCSoBill.VisibleIndex = -1

            colVCSoTien.VisibleIndex = -1
            colVCCanNang.VisibleIndex = -1
            colVCGhiChu.VisibleIndex = -1
            colVCTienTe.VisibleIndex = -1
            colVCTyGia.VisibleIndex = -1
            colVCNguoiNhapCP.VisibleIndex = -1
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Never
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


        If gdvCT.GetFocusedRowCellValue("CongTrinh") = True Then
            mnuNgayCTXuatKho.Visibility = XtraBars.BarItemVisibility.Always
            Dim sql As String = "SELECT top 1 NgayCT FROM PHIEUXUATKHO WHERE SoPhieu = @SoPhieu"
            AddParameter("@SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu"))
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing OrElse dt.Rows.Count = 0 OrElse dt.Rows(0)(0) Is DBNull.Value Then
                mnuNgayCTXuatKho.Caption = "Chưa có ngày CT xuất kho"
            Else
                mnuNgayCTXuatKho.Caption = "Ngày CT xuất kho: " & Convert.ToDateTime(dt.Rows(0)(0)).ToString("dd/MM/yyyy")
            End If
        Else
            mnuNgayCTXuatKho.Visibility = XtraBars.BarItemVisibility.Never
        End If


        If colVCDVVC.Visible Then
            ' btLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Always
            mNhapThongTinVC.Caption = "Ẩn đề nghị chi vận chuyển"
            If IsDBNull(gdvCT.GetFocusedRowCellValue("IDDVVC")) Then
                mThemChiPhiMoi.Visibility = XtraBars.BarItemVisibility.Never
            Else
                mThemChiPhiMoi.Visibility = XtraBars.BarItemVisibility.Always
            End If
            mDuaVaoDSDungChungCP.Visibility = XtraBars.BarItemVisibility.Always
            If Not IsDBNull(gdvCT.GetFocusedRowCellValue("SoTienTC")) Then
                If gdvCT.GetFocusedRowCellValue("SoTienTC") <> gdvCT.GetFocusedRowCellValue("SoTien") Then
                    mSuaCPVCDungChung.Visibility = XtraBars.BarItemVisibility.Always
                Else
                    mSuaCPVCDungChung.Visibility = XtraBars.BarItemVisibility.Never
                End If
            End If

        Else
            ' btLuuLai.Visibility = XtraBars.BarItemVisibility.Never
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Never
            mNhapThongTinVC.Caption = "Đề nghị chi vận chuyển"
            mDuaVaoDSDungChungCP.Visibility = XtraBars.BarItemVisibility.Never
            mSuaCPVCDungChung.Visibility = XtraBars.BarItemVisibility.Never
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
            mLuuLai.Visibility = XtraBars.BarItemVisibility.Always
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
            If colVCDVVC.Visible Then
                If Not gdvCT.IsEditing Then
                    gdvCT.Focus()
                    If gdvCT.FocusedRowHandle < gdvCT.RowCount Then
                        gdvCT.FocusedRowHandle = gdvCT.FocusedRowHandle + 1
                        gdvCT.FocusedColumn = colVCDVVC
                    End If
                End If
            End If
        End If
        If e.Control AndAlso e.KeyCode = Keys.S Then
            LuuLai()
        End If
    End Sub

    Private Sub gdvCT_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyUp
        If e.KeyCode = Keys.Tab Then
            If gdvCT.FocusedColumn Is colLoaiCT Then
                If gdvCT.FocusedRowHandle < gdvCT.RowCount Then
                    gdvCT.FocusedRowHandle = gdvCT.FocusedRowHandle + 1
                    gdvCT.FocusedColumn = colVCDVVC
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
                AddParameter("@Loai", True)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvCT.GetRowCellValue(i, "IDVC")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        ShowBaoLoi("Không thêm được chi phí tại PX: " & gdvCT.GetRowCellValue(i, "SoPhieu") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "IDVC"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        ShowBaoLoi("Không cập nhật được chi phí tại PX: " & gdvCT.GetRowCellValue(i, "SoPhieu") & vbCrLf & LoiNgoaiLe)
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
        tb.Columns.Add("SoPhieuXK", Type.GetType("System.String"))
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

    Private Sub btClose_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        gdvDSCP.DataSource = LayDataSourceDSCP()
        pVCGop.Visible = False
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
            If gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK") = gdvCT.GetFocusedRowCellValue("SoPhieu") Then
                ShowCanhBao("Số phiếu XK đã có sẵn trong danh sách!")
                Exit Sub
            End If
        Next
        gdvDSCPCT.AddNewRow()
        gdvDSCPCT.SetFocusedRowCellValue("SoPhieuXK", gdvCT.GetFocusedRowCellValue("SoPhieu"))
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
        If colVCDVVC.Visible Then
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
                AddParameter("@PhieuTC", gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK"))
                AddParameter("@SoBill", tbSoBill.EditValue)
                AddParameter("@SoTien", gdvDSCPCT.GetRowCellValue(i, "ChiPhi"))
                AddParameter("@SoTienTC", tbSoTien.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@TyGia", tbTyGia.EditValue)
                AddParameter("@CanNang", gdvDSCPCT.GetRowCellValue(i, "CanNang"))
                AddParameter("@GhiChu", gdvDSCPCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@Loai", True)
                AddParameter("@MucDich", 205)
                If IsDBNull(gdvDSCPCT.GetRowCellValue(i, "ID")) Then
                    Dim obj As Object = doInsert("ChiPhi")
                    If Not obj Is Nothing Then
                        gdvDSCPCT.SetRowCellValue(i, "IDVC", obj)
                    Else
                        Throw New Exception("Không thêm được chi phí tại PX: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK") & vbCrLf & LoiNgoaiLe)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvDSCPCT.GetRowCellValue(i, "ID"))
                    If doUpdate("ChiPhi", "ID=@IDD") Is Nothing Then
                        Throw New Exception("Không cập nhật được chi phí tại PX: " & gdvDSCPCT.GetRowCellValue(i, "SoPhieuXK") & vbCrLf & LoiNgoaiLe)
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
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT PhieuTC as SoPhieuXK,CanNang,GhiChu,ChiPhi.ID,SoTien As ChiPhi,PHIEUXUATKHO.TienTruocThue*PHIEUXUATKHO.TyGIa as TienTruocThue FROM ChiPhi INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=ChiPhi.PhieuTC WHERE Loai=1 AND IDDVVC=@IDVC AND SoTienTC=@STTC AND SoBill=@SB AND ThoiGian=@TG")
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


    Private Sub mnuThueGhepVatTuBoThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueGhepVatTuBoThue.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not ShowCauHoi("Ghép vật tư bộ từ danh sách vật tư trên xuất kho đang chọn?") Then Exit Sub

        TrangThai.isAddNew = True
        Dim f As New frmUpdateGhepVatTuBo

        f.Text = "Lập chứng từ ghép vật tư bộ"

        Dim nam As Integer = ChungTu.NamLamViec

        For i As Integer = 0 To gdvVTCT.RowCount - 1

            If gdvVTCT.GetRowCellValue(i, "IDvattu") Is DBNull.Value Then Continue For

            f.gdvData.AddNewRow()

            'f.gdvData.GetRowCellValue("Id_CT", 0)
            f.gdvData.SetFocusedRowCellValue("IDVatTuPhu", gdvVTCT.GetRowCellValue(i, "IDvattu"))

            Dim sql As String = "SELECT TenHoaDon,Model,Code, "
            sql &= "(select ten from tendonvitinh where id = vattu.iddonvitinh)DVT,  "
            sql &= "(select ten from tenhangsanxuat where id = vattu.idhangsanxuat)HangSX,  "
            sql &= "(select ten from tennhom where id = vattu.idhangsanxuat)TenNhom,  "
            sql &= "  "
            sql &= "isnull((SELECT TOP 1 a.ThanhTien FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT   "
            sql &= "WHERE b.IdVatTu = VATTU.ID AND b.ButToan = 3 ORDER BY a.NgayCT DESC),0)GiaNhap,   "
            sql &= "  "
            sql &= "(ISNULL((select DauKy from tonkhovattuthue where IdVatTu = VATTU.ID and Nam =  " & nam & " ),0)   "
            sql &= " +   "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT   "
            sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (2) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) =  " & nam & "   "
            sql &= "AND CHUNGTUCHITIET.ButToan = 1),0)   "
            sql &= "-  "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT   "
            sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (1) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) =  " & nam & "   "
            sql &= "AND CHUNGTUCHITIET.ButToan = 1),0)   "
            sql &= ")TonThue,  "

            sql &= "  "
            sql &= "( ISNULL((SELECT SUM(NHAPKHO.SoLuong) FROM NHAPKHO RIGHT OUTER JOIN PHIEUNHAPKHO    "
            sql &= "ON NHAPKHO.Sophieu = PHIEUNHAPKHO.Sophieu WHERE NHAPKHO.IdVattu = VATTU.ID),0)   "
            sql &= "-   "
            sql &= "ISNULL((SELECT SUM(XUATKHO.SoLuong) FROM XUATKHO RIGHT OUTER JOIN PHIEUXUATKHO    "
            sql &= "ON XUATKHO.Sophieu = PHIEUXUATKHO.Sophieu WHERE XUATKHO.IdVattu = VATTU.ID AND PHIEUXUATKHO.Congtrinh = 0   "
            sql &= "),0)  "
            sql &= ")TonKho   "
            sql &= " FROM VATTU WHERE ID =   " & gdvVTCT.GetRowCellValue(i, "IDvattu")
            sql &= "  "


            Dim dtX As DataTable = ExecuteSQLDataTable(sql)


            f.gdvData.SetFocusedRowCellValue("ID", 0)
            f.gdvData.SetFocusedRowCellValue("Id_CT", 0)

            f.gdvData.SetFocusedRowCellValue("TenVatTu", dtX.Rows(0)("TenHoaDon"))
            f.gdvData.SetFocusedRowCellValue("Model", dtX.Rows(0)("Model"))
            f.gdvData.SetFocusedRowCellValue("Code", dtX.Rows(0)("Code"))
            f.gdvData.SetFocusedRowCellValue("SoLuong", gdvVTCT.GetRowCellValue(i, "SoLuong"))
            f.gdvData.SetFocusedRowCellValue("GiaNhap", dtX.Rows(0)("GiaNhap"))
            f.gdvData.SetFocusedRowCellValue("TonKho", dtX.Rows(0)("TonKho"))
            f.gdvData.SetFocusedRowCellValue("TonThue", dtX.Rows(0)("TonThue"))
            f.gdvData.SetFocusedRowCellValue("DVT", dtX.Rows(0)("DVT"))
            f.gdvData.SetFocusedRowCellValue("HangSX", dtX.Rows(0)("HangSX"))
            f.gdvData.SetFocusedRowCellValue("NhomVT", dtX.Rows(0)("TenNhom"))

        Next

        f.ShowDialog()

    End Sub

    Private Sub mnuCapNhatNgayCT_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles mnuCapNhatNgayCT.ItemClick

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim txtNgay As New DevExpress.XtraEditors.DateEdit
        txtNgay.Dock = DockStyle.Top
        txtNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        txtNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        txtNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        txtNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        txtNgay.EditValue = GetServerTime()
        txtNgay.Properties.ShowClear = True
        txtNgay.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        txtNgay.Height = 25

        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Text = "Ngày CT XK"
        f.Height = 110
        f.Width = 140
        f.FormBorderStyle = FormBorderStyle.FixedSingle
        f.StartPosition = FormStartPosition.CenterScreen
        f.MaximizeBox = False
        f.MinimizeBox = False

        Dim btn As New DevExpress.XtraEditors.SimpleButton
        btn.Top = 25
        btn.Left = 28
        btn.Text = "Cập nhật"
        btn.Height = 35
        btn.Cursor = Cursors.Hand
        AddHandler btn.Click, AddressOf btnCapNhatNgayCT_Click

        f.Controls.Add(txtNgay)
        f.Controls.Add(btn)

        f.ShowDialog()

    End Sub

    Private Sub btnCapNhatNgayCT_Click(sender As System.Object, e As System.EventArgs)
        Dim d As Object = CType(CType(sender, SimpleButton).Parent.Controls(0), DateEdit).EditValue
        If d Is Nothing Then
            AddParameter("@NgayCT", DBNull.Value)
        Else
            AddParameter("@NgayCT", d)
        End If
        AddParameterWhere("@SoPhieu", gdvCT.GetFocusedRowCellValue("SoPhieu"))
        If Not doUpdate("PHIEUXUATKHO", "SoPhieu=@SoPhieu") Is Nothing Then
            ShowAlert("Đã cập nhật ngày CT XK  " & gdvCT.GetFocusedRowCellValue("SoPhieu") & "!")
            CType(CType(sender, SimpleButton).Parent, XtraForm).Close()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

End Class
