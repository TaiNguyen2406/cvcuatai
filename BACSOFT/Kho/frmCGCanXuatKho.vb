Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraEditors
Imports BACSOFT.Utils

Public Class frmCGCanXuatKho
    Public _exit As Boolean = False
    Public _FileCGKinhDoanh As String = ""

    Private Sub frmCGCanXuatKho_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 5000"
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
        tAutoLoadChuaXL.Start()
    End Sub

#Region "Load DS chào giá cần xuất kho"

    Public Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu...")

        Dim sql As String = ""
        If cbTieuChi.EditValue = "Top 5000" Then
            sql = " SELECT TOP 5000 BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        Else
            sql = " SELECT BANGCHAOGIA.ID,BANGCHAOGIA.Sophieu,BANGCHAOGIA.Ngaythang,BANGCHAOGIA.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenKH, "
        End If
        sql &= "	Null as CanhBao,Datediff(hour,getdate(),TGKDCanXuat)SoPhut,BANGCHAOGIA.Masodathang,BANGCHAOGIA.TenDuan,BANGCHAOGIA.txtKhac,Ngaygiao,NgayNhan,"
        sql &= "	BANGCHAOGIA.txtTkhac,BANGCHAOGIA.TienTruocthue,BANGCHAOGIA.Tienthue,"
        sql &= "	BANGCHAOGIA.TienChietkhau,BANGCHAOGIA.Tienthucthu,BANGCHAOGIA.TienLoiNhuan,"
        sql &= "	Ngaynhan,Ngaygiao,Ngayhuy,BANGCHAOGIA.IDUser, IDNgXuLy,NgayXuLy, IDNgDuyet,NgayDuyet, "
        sql &= "	NHANSU_1.Ten AS TenNgd,BANGCHAOGIA.IDTakeCare,tblTienTe.Ten AS TienTe,"
        sql &= "	Congtrinh,(CASE CongTrinh WHEN 1 THEN (Case XuLy WHEN 0 THEN N'Cần xử lý' WHEN 1 THEN N'Đã xử lý' END) ELSE N'' END) AS Trangthai,Khautru,BANGCHAOGIA.FileDinhKem,tblTuDien.NoiDung AS TrangThaiCG,BANGCHAOGIA.TrangThai as TTCG,TGKDCanXuat,TGKhoHoanThanh,NoiDungXLCuaKho,GhiChuKD,BANGCHAOGIA.TGLapYCXuat"
        sql &= " FROM BANGCHAOGIA LEFT OUTER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_1 ON BANGCHAOGIA.IDNgd=NHANSU_1.ID"
        sql &= "        LEFT OUTER JOIN tblTienTe ON BANGCHAOGIA.Tiente=tblTienTe.ID"
        sql &= "        LEFT OUTER JOIN tblTuDien ON BANGCHAOGIA.Trangthai=tblTuDien.Ma AND Loai=2"
        sql &= "        INNER JOIN (SELECT DISTINCT SoPhieu FROM CHAOGIA WHERE CanXuat <>0)tbCX ON tbCX.SoPhieu=BANGCHAOGIA.SoPhieu "
        sql &= " WHERE BANGCHAOGIA.TrangThai=2 "


        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,convert(nvarchar,BANGCHAOGIA.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDUser= " & btNhanVien.EditValue
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang= " & cbKH.EditValue
        End If

        'If cbTieuChi.EditValue = "Top 100" Then
        sql &= " ORDER BY BANGCHAOGIA.TGKDCanXuat DESC, BANGCHAOGIA.NgayGiao "
        ' End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If Not dt Is Nothing Then
            gdv.DataSource = dt
            If Not gdvCT.FocusedRowHandle < 0 Then

                loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
            Else
                gdvCG.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        CloseWaiting()

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)
        Dim sql As String = ""
        sql &= " DECLARE @tb table"
        sql &= " ("
        sql &= " 	SoPhieu nvarchar(15),"
        sql &= " 	TenVT NVARCHAR(4000),"
        sql &= " 	HangSX NVARCHAR(50),"
        sql &= " 	Model NVARCHAR(50),"
        sql &= " 	ThongSo	NVARCHAR(1000),"
        sql &= " 	DVT NVARCHAR(50),"
        sql &= " 	SoLuong Float,"
        sql &= " 	SLDaXuat Float,"
        sql &= " 	SLCanXuat Float,"
        sql &= " 	DonGia float,"
        sql &= "    ChietKhauPT float,"
        sql &= "    ChietKhau float,"
        sql &= " 	ThanhTien Float,"
        sql &= " 	XuatThue bit,"
        sql &= " 	MucThue tinyint,"
        sql &= " 	slTon Float,"
        sql &= " 	DangVe Float,"
        sql &= " 	TienTe nvarchar(20),"
        sql &= " 	TyGia float,"
        sql &= " 	HangTon bit,"
        sql &= "    IDVatTu int,"
        sql &= " 	AZ int"
        sql &= " )"
        sql &= " INSERT INTO @tb(SoPhieu,TenVT,HangSX,Model,ThongSo,DVT,SoLuong,SLDaXuat,SLCanXuat,DonGia,ChietKhau,ThanhTien,XuatThue,MucThue,slTon,DangVe,TienTe,TyGia,HangTon,IDVatTu,AZ)"
        sql &= " SELECT CHAOGIA.Sophieu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso,"
        sql &= " TENDONVITINH.Ten AS TenDVT,Soluong,(Soluong-CanXuat)AS SLDaXuat,CanXuat AS SLCanXuat,ISNULL(CHAOGIA.Dongia,0)DonGia,ISNULL(CHAOGIA.Chietkhau,0) ChietKhau,(CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,CHAOGIA.Xuatthue,CHAOGIA.Mucthue,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        sql &= "  (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVatTu) AS DangVe,"
        sql &= " tblTienTe.Ten AS TenTienTe,CHAOGIA.TyGia, VATTU.HangTon, CHAOGIA.IDVatTu,ISNULL(CHAOGIA.AZ,0)AZ"
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


    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        'gdvCT.ClearColumnsFilter()
        'gdvCT.ClearSorting()
        LoadDS()
    End Sub

    Private Sub gdvCT_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
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

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.Column.FieldName = "CanhBao" Then
            If IsDBNull(gdvCT.GetRowCellValue(e.RowHandle, "SoPhut")) Then Exit Sub
            If Not IsDBNull(gdvCT.GetRowCellValue(e.RowHandle, "TGKhoHoanThanh")) Then
                If Convert.ToDateTime(gdvCT.GetRowCellValue(e.RowHandle, "TGKhoHoanThanh")) > Convert.ToDateTime(gdvCT.GetRowCellValue(e.RowHandle, "TGKDCanXuat")) Then
                    Exit Sub
                End If
            End If

            If e.RowHandle < 0 Then Exit Sub
            If gdvCT.GetRowCellValue(e.RowHandle, "SoPhut") <= 30 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvCT.GetRowCellValue(e.RowHandle, "SoPhut") > 30 And gdvCT.GetRowCellValue(e.RowHandle, "SoPhut") <= 120 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub btXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatKho.ItemClick, mLapXuatKho.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("Congtrinh") And gdvCT.GetFocusedRowCellValue("Trangthai") = "Cần xử lý" Then
            If Not ShowCauHoi("Công trình này vẫn chưa được xử lý xong bạn có muốn lập xuất kho hay không ?") Then Exit Sub
        End If

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG='" & gdvCT.GetFocusedRowCellValue("Sophieu") & "' ORDER BY SoPhieu DESC")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If ShowCauHoi("Đã có xuất kho cho chào giá này, bạn có muốn mở phiếu xuất kho đã có không ?") Then
                    If tb.Rows.Count = 1 Then
                        TrangThai.isUpdate = True
                        fCNXuatKho = New frmCNXuatKho
                        fCNXuatKho.PhieuXK = tb.Rows(0)(0)
                        fCNXuatKho.Tag = Me.Parent.Tag
                        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                            fCNXuatKho.btCal.Enabled = False
                            fCNXuatKho.btGhi.Enabled = False
                            fCNXuatKho.btChuyenXK.Enabled = False
                            fCNXuatKho.mXuatKho.Enabled = False
                        End If
                        fCNXuatKho.ShowDialog()
                    Else
                        Dim f As New frmChonSoPhieu
                        f.gdv.DataSource = tb
                        f.Tag = Me.Parent.Tag
                        f.ShowDialog()
                    End If

                    Exit Sub
                End If

            End If
        End If


        TrangThai.isAddNew = True
        fCNXuatKho = New frmCNXuatKho
        fCNXuatKho.Tag = Me.Parent.Tag
        fCNXuatKho.gdvMaKH.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachhang")
        fCNXuatKho.gdvPhieuCG.EditValue = gdvCT.GetFocusedRowCellValue("Sophieu")
        fCNXuatKho.ShowDialog()

    End Sub

    Private Sub mXemCanXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemCanXuat.ItemClick
        Dim f As New frmThongTinCanXuat
        f._IDVatTu = gdvCGCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub

    Private Sub gdvCGCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCGCT.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If gdvCGCT.CalcHitInfo(e.Location).InRowCell Then
                pMenuPhu.ShowPopup(gdvCG.PointToScreen(e.Location))
            End If
        End If
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If gdvCT.CalcHitInfo(e.Location).InRowCell Then
                pMenuChinh.ShowPopup(gdv.PointToScreen(e.Location))
            End If
        End If
    End Sub

    Private Sub mThongBaoChoKD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThongBaoChoKD.ItemClick
        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT TGKhoHoanThanh,NoiDungXLCuaKho FROM BANGCHAOGIA WHERE SoPhieu=@SP")
        If Not tb Is Nothing Then
            If IsDBNull(tb.Rows(0)(0)) Then
                lbThoiGianHT.Text = ""
                tbNoiDungThongBao.EditValue = "Đã chuẩn bị xong hàng cần xuất cho chào giá: " & gdvCT.GetFocusedRowCellValue("Sophieu") & " - " & gdvCT.GetFocusedRowCellValue("MaKH")
            Else
                lbThoiGianHT.Text = Convert.ToDateTime(tb.Rows(0)(0)).ToString("dd/MM/yyyy HH:mm")
                tbNoiDungThongBao.EditValue = tb.Rows(0)(1)

            End If
        End If

        pThongBao.Location = New Point(Cursor.Position.X, Cursor.Position.Y - 110)
        pThongBao.Visible = True

    End Sub

    Private Sub btHuy_Click(sender As System.Object, e As System.EventArgs) Handles btHuy.Click
        tbNoiDungThongBao.EditValue = DBNull.Value
        pThongBao.Visible = False
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        lbThoiGianHT.Text = Now.ToString("dd/MM/yyyy HH:mm")
        AddParameter("@TGKhoHoanThanh", Now)
        AddParameter("@NoiDungXLCuaKho", tbNoiDungThongBao.EditValue)
        AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
        If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then
            lbThoiGianHT.Text = ""
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvCT.SetFocusedRowCellValue("TGKhoHoanThanh", Now)
            gdvCT.SetFocusedRowCellValue("NoiDungXLCuaKho", tbNoiDungThongBao.EditValue)
            ' Dim str As String = tbNoiDungThongBao.EditValue
            ThemThongBaoChoNV(tbNoiDungThongBao.EditValue & vbCrLf & "(" & NguoiDung & " - " & lbThoiGianHT.Text & ")", gdvCT.GetFocusedRowCellValue("IDTakeCare"))
            ShowAlert("Đã thông báo cho kinh doanh !")
            pThongBao.Visible = False
        End If
    End Sub

    Private Sub tAutoLoadChuaXL_Tick(sender As System.Object, e As System.EventArgs) Handles tAutoLoadChuaXL.Tick
        Dim sql As String = " SELECT count(ID) FROM BANGCHAOGIA "
        sql &= " INNER JOIN (SELECT DISTINCT SoPhieu FROM CHAOGIA WHERE CanXuat <>0)tbCX ON tbCX.SoPhieu=BANGCHAOGIA.SoPhieu "
        sql &= " WHERE Datediff(minute,TGKhoHoanThanh,TGKDCanXuat)>0 OR (TGKDCanXuat is not null and TGKhoHoanThanh is null) "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If tb.Rows(0)(0) = 0 Then
                lbChuaXL.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                lbChuaXL.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                lbChuaXL.Caption = String.Format(lbChuaXL.Tag, tb.Rows(0)(0))
            End If
        Else
            lbChuaXL.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            lbChuaXL.Caption = "Lỗi tải dữ liệu!"
        End If
    End Sub

    Private Sub btInBienBanGiaoNhan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInBBGiaoNhanConTon.ItemClick, btInBienBanGiaoNhan.ItemClick, mInBBGiaoNhan.ItemClick, mInBBGiaoNhanConTon.ItemClick
        Try
            ShowWaiting("Đang tải nội dung ...")
            Dim Sql As String = ""
            Sql &= " SELECT (N'Khách hàng: ' + ISNULL(KHACHHANG.Ten,'')) AS TenKH,(N'Ngày: ' + Convert(nvarchar,NgayThang,103))AS Ngay,"
            Sql &= " (N'Đại diện: ' + ISNULL(NGUOIGD.Ten,'')) AS DaiDien,(N'Xuất tại: ' + N'Kho Cty Bảo An') AS Kho,"
            Sql &= " (N'Chức danh: '+ISNULL(NGUOIGD.ChucVu,'') + '  ĐT: ' + ISNULL(NGUOIGD.Mobile,'')) AS ChucDanh,"
            Sql &= " (N'Người xuất: ' + N'" & NguoiDung.ToString() & "') AS NguoiXuat, "
            Sql &= "    (N'Chào giá: CG ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + SoPhieu ) AS ChaoGia,"
            Sql &= " (N'Phiếu xuất: ' + 'XK ' + ISNULL(KHACHHANG.ttcMa,'') + ' ' + '" & Now.ToString("yyMM") & "' +'........') AS PhieuXuat,TAKECARE.Ten AS TakeCare,"
            Sql &= " (N'Lý do xuất kho: '+ '') AS LyDoXuat"
            Sql &= " FROM BANGCHAOGIA"
            Sql &= " LEFT JOIN KHACHHANG ON BANGCHAOGIA.IDKhachHang=KHACHHANG.ID"
            Sql &= " LEFT JOIN NHANSU AS NGUOIGD ON BANGCHAOGIA.IDNgd=NGUOIGD.ID"
            Sql &= " LEFT JOIN NHANSU AS TAKECARE ON BANGCHAOGIA.IDTakeCare=TAKECARE.ID"
            Sql &= " WHERE SoPhieu=@SP"

            Sql &= " SELECT * FROM ("

            Sql &= " SELECT (0) AS STT,"
            Sql &= " ISNULL(TENVATTU.Ten,N'') AS TenVT,"
            Sql &= " TENHANGSANXUAT.Ten AS TenHang,"
            Sql &= " VATTU.Model,TENDONVITINH.Ten AS TenDVT,SoLuong,"
            Sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS SLTon"
            Sql &= " FROM CHAOGIA "
            Sql &= " INNER JOIN VATTU ON CHAOGIA.IDVatTu=VATTU.ID"
            Sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
            Sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
            Sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
            Sql &= " WHERE SoPhieu=@SP AND CanXuat >0"
            Sql &= " )tbl "
            If e.Item.Name = btInBBGiaoNhanConTon.Name Or e.Item.Name = mInBBGiaoNhanConTon.Name Then
                Sql &= " WHERE SLTon>0  "
            End If


            'Sql &= " UNION ALL "
            'Sql &= " SELECT (0)AS STT, NoiDung AS TenVT,HangSX AS TenHang,('')MaVT,TENDONVITINH.Ten AS TenDVT, SoLuong,NULL as SLTon"
            'Sql &= " FROM CHAOGIAAUX"
            'Sql &= " LEFT OUTER JOIN TENDONVITINH ON CHAOGIAAUX.Donvi=TENDONVITINH.ID"
            'Sql &= " WHERE SoPhieu=@SP"

            AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
            Dim ds As DataSet = ExecuteSQLDataSet(Sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
            If Not ds Is Nothing Then
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next
            End If

            Dim f As New frmIn("Biên bản giao nhận")
            'Dim rpt As New rptBienBanGiaoNhan

            'rpt.pLogo.Image = My.Resources.Logo3
            'rpt.DataSource = ds



            'rpt.CreateDocument()

            '  f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class
