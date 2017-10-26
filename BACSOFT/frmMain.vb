Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper

Public Class fMain
    Public _DangXuat As Boolean = False

    Private Sub fMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        If TaiKhoan <> 1898 Then
            btBaoCaoThuTien.Visibility = BarItemVisibility.Never
            tbThang.Visibility = BarItemVisibility.Never
            cbBCNhanVIen.Visibility = BarItemVisibility.Never
            CheckNewVersion()
        End If

        If TaiKhoan = 1898 Then
            LayTyGia()
        End If

        Application.DoEvents()
        loadDSPhongBan()
        Application.DoEvents()
        LoadDSNhanVien()
        Application.DoEvents()
        PhanQuyen()
        Application.DoEvents()
        LoadLichLamViec()
        SchLichLamViec.Start = DateAdd(DateInterval.Hour, 6, GetServerTime.Date)

        Dim index As Integer = 0
        For i As Integer = 0 To TreeNV.ItemCount - 1
            If CType(TreeNV.Items(i).Value, DevExpress.XtraScheduler.UI.ResourceCheckedListBoxItem).Resource.Id = TaiKhoan Then
                index = i
                Exit For
            End If
        Next
        SchLichLamViec.Views.TimelineView.FirstVisibleResourceIndex = index
        lbVer.Caption = "Ver." & Application.ProductVersion.ToString
        Dim tg As DateTime = GetServerTime()
        tbThang.EditValue = tg
        Dim GioLamViec As New TimeSpan(8, 0, 0)
        If tg.TimeOfDay <= GioLamViec And TaiKhoan <> 1898 Then
            BaoCaoTuDong()
        End If
        Application.DoEvents()
        ThongBaoVatTuMuonQuaHan()


        TimerLoadThongBao.Start()

        If My.Computer.Name = "BACBOSS" Then
            Application.DoEvents()
            UpdateTakeCare()
            Application.DoEvents()
            LayTyGia()
            Application.DoEvents()
            Threading.Thread.Sleep(2000)
            mXemManHinhLon.PerformClick()
        Else
            Application.DoEvents()
            Threading.Thread.Sleep(100)
            OpenTab("Chỉ tiêu", "frmChiTieu", New frmChiTieu, True, Nothing, mChiTieu.Name)
        End If

    End Sub

    Public Sub UpdateSoKH(ByVal _ThoiGian As DateTime)
        Dim sql As String = " SET DATEFORMAT DMY"
        sql &= " DECLARE @Ngay as datetime"
        sql &= " SET @Ngay = '" & _ThoiGian.Date.ToString("dd/MM/yyyy") & "'"

        sql &= " IF NOT EXISTS(SELECT ID FROM TONGKH WHERE Ngay=CONVERT(NVARCHAR(10),DATEADD(dd,-(DAY(@Ngay)-1),@Ngay),103)) "
        sql &= " 	BEGIN"
        sql &= " 		INSERT INTO TongKH"
        sql &= " 		SELECT CONVERT(NVARCHAR(10),DATEADD(dd,-(DAY(@Ngay)-1),@Ngay),103) AS Ngay ,"
        sql &= " 		IDTakeCare, count(IDTakeCare)SoKH FROM KHACHHANG"
        sql &= " 		WHERE IDTakeCare IS Not NULL"
        sql &= " 		GROUP BY IDTakeCare"
        sql &= " 	END"
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            'ShowAlert("Đã cập nhật số lượng KH !")
        End If
    End Sub

    Public Sub ThongBaoVatTuMuonQuaHan()
        Dim sql As String = ""
        sql &= " SELECT IDNguoiMuon,ThoiGianMuon,SoLuong,IDVatTu,VATTU.Model,(Datediff(day,thoigianmuon,getdate())-7) NgayQuaHan"
        sql &= " FROM tblXuatMuon"
        sql &= " INNER JOIN VATTU ON VATTU.ID=tblXuatMuon.IDVatTu"
        sql &= " WHERE TrangThai<>1 "
        sql &= " AND  Datediff(day,thoigianmuon,getdate()) >7"
        sql &= " AND IDNguoiMuon=@IdNgMuon"
        AddParameterWhere("@IdNgMuon", TaiKhoan)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If tb.Rows.Count = 0 Then Exit Sub
            sql = "Thông báo vật tư mượn quá hạn:" & vbCrLf
            For i As Integer = 0 To tb.Rows.Count - 1
                sql &= " - " & tb.Rows(i)("Model").ToString & "; SL: " & tb.Rows(i)("SoLuong").ToString & "; quá hạn " & tb.Rows(i)("NgayQuaHan").ToString & " ngày" & vbCrLf
            Next
            sql = sql.TrimEnd
            AddParameterWhere("@ND", sql)
            Dim tb2 As DataTable = ExecuteSQLDataTable("SELECT COUNT(ID) FROM tblThongBao WHERE NoiDung=@ND")
            If Not tb2 Is Nothing Then
                If tb2.Rows(0)(0) = 0 Then
                    ThemThongBaoChoNV(sql, TaiKhoan)
                End If
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = (Keys.Control Or Keys.Shift Or Keys.T) Then
            If TaiKhoan = 1 Or TaiKhoan = 1898 Then
                Dim f As New frmToolChucNang
                f.Show()
                Return True
            End If
        End If
        Return False
    End Function

    Public Sub UpdateTakeCare()
        Application.DoEvents()
        Dim tg As DateTime = GetServerTime()
        If tg.DayOfWeek = DayOfWeek.Monday And tg.Date > DateSerial(2014, 8, 20) Then
            Dim sql As String = ""
            sql &= " UPDATE KHACHHANG SET IDTakeCare = null "
            sql &= " WHERE ID IN ("
            sql &= " SELECT ID FROM"
            sql &= " ("
            sql &= " SELECT ID,ttcMa,Ngay,Datediff(day,Ngay,getdate())SoNgay FROM KHACHHANG WHERE ttcKhachHang=1 AND"
            sql &= " ID NOT IN (SELECT DISTINCT NoiCtac FROM NHANSU WHERE Noictac <>74)"
            sql &= " AND ID NOT IN (SELECT IDKH"
            sql &= " FROM"
            sql &= " ("
            sql &= " SELECT IDKH,Max(ThoiGian)ThoiGian FROM GIAODICHKH"
            sql &= " GROUP BY IDKH)tb WHERE datediff(day,ThoiGian,Getdate()) < 45 )"
            sql &= " AND ID <>74)tb WHERE SoNgay >=45)"

            sql &= " UPDATE KHACHHANG SET IDTakeCare = null "
            sql &= " WHERE ID IN ("
            sql &= " SELECT ID FROM"
            sql &= " ("
            sql &= " SELECT ID,ttcMa,Ngay,Datediff(day,Ngay,getdate())SoNgay FROM KHACHHANG WHERE ttcKhachHang=1 AND"
            sql &= " ID NOT IN (SELECT DISTINCT IDKhachHang FROM BANGYEUCAU)"
            sql &= " AND ID NOT IN (SELECT IDKH"
            sql &= " FROM"
            sql &= " ("
            sql &= " SELECT IDKH,Max(ThoiGian)ThoiGian FROM GIAODICHKH"
            sql &= " GROUP BY IDKH)tb WHERE datediff(day,ThoiGian,Getdate()) < 60 )"
            sql &= " AND ID <>74)tb WHERE SoNgay >=60)"

            sql &= " UPDATE KHACHHANG SET IDTakeCare = null "
            sql &= " WHERE ID IN ("
            sql &= " SELECT ID FROM"
            sql &= " ("
            sql &= " SELECT ID,ttcMa,Ngay,Datediff(day,Ngay,getdate())SoNgay FROM KHACHHANG WHERE ttcKhachHang=1 AND"
            sql &= " ID NOT IN (SELECT DISTINCT IDKhachHang FROM BANGCHAOGIA WHERE TrangThai=2)"
            sql &= " AND ID NOT IN (SELECT IDKH"
            sql &= " FROM"
            sql &= " ("
            sql &= " SELECT IDKH,Max(ThoiGian)ThoiGian FROM GIAODICHKH"
            sql &= " GROUP BY IDKH)tb WHERE datediff(day,ThoiGian,Getdate()) < 90 )"
            sql &= " AND ID <>74)tb WHERE SoNgay >=90)"
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thông tin khách hàng !")
            End If
        End If
    End Sub

    Public Sub BaoCaoTuDong()
        Application.DoEvents()
        Dim tg As DateTime = GetServerTime()
        Dim sql As String = ""
        sql &= " SELECT tbBaoCaoMoi.*,HDDanhSach.Diem  FROM (SELECT *,"
        sql &= " (Case WHEN datediff(day,NgayXK,Ngay) <-5 THEN"
        sql &= " 		39"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >=-5 AND datediff(day,NgayXK,Ngay) <=5 THEN"
        sql &= " 		35"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >5 AND datediff(day,NgayXK,Ngay) <=15 THEN"
        sql &= " 		36"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >15 AND datediff(day,NgayXK,Ngay) <=30 THEN"
        sql &= " 		37"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >30 AND datediff(day,NgayXK,Ngay) <=45 THEN"
        sql &= " 		38"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >45 THEN"
        sql &= " 		40 "
        sql &= " END)IDDanhSach "
        sql &= " FROM ("
        sql &= " SELECT tb.*,tbBaoCao.KhoiLuong FROM "
        sql &= " (SELECT tbThu.NgaythangCT AS Ngay,PHIEUXUATKHO.NgayThang AS NgayXK,KHACHHANG.ttcMa AS MaKH, tbThu.Sotien AS TienThu, PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK,"
        sql &= " dbo.PHIEUXUATKHO.IDTakecare"
        sql &= " FROM         "
        sql &= " (SELECT     NgaythangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "     FROM          dbo.THU"
        sql &= "     UNION ALL"
        sql &= "     SELECT     ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "     FROM         dbo.THUNH) AS tbThu "
        sql &= " INNER JOIN PHIEUXUATKHO ON tbThu.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tbThu.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachHang "
        sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(tbThu.NgaythangCT) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(tbThu.NgaythangCT) "
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
        sql &= " WHERE month(tbThu.NgaythangCT)= month(getDate()) AND year(tbThu.NgaythangCT)=year(getDate()) AND PHIEUXUATKHO.IDTakeCare=" & TaiKhoan & ")tb "
        sql &= " LEFT JOIN "
        sql &= " (SELECT HDNhanVien.ID,NgayBaoCao,KhoiLuong,ChiTiet,RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 9) SoPhieuXK,HDNhanVien.IDNhanVien FROM HDNhanVien "
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 9)"
        sql &= " WHERE IDDanhSach IN (35,36,37,38,39,40) AND IDNhanVien=" & TaiKhoan & "  AND month(NgayBaoCao)=month(getDate()) AND year(NgayBaoCao)=year(getDate()))tbBaoCao"
        sql &= " ON tb.SoPhieuXK=tbBaoCao.SoPhieuXK AND tb.TienThu=tbBaoCao.KhoiLuong)tblBaoCao2"
        sql &= " WHERE KhoiLuong IS NULL)tbBaoCaoMoi INNER JOIN HDDanhSach ON HDDanhSach.ID=tbBaoCaoMoi.IDDanhSach "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then

            Try
                For i As Integer = 0 To tb.Rows.Count - 1
                    AddParameter("@Ngaybaocao", tg.Date)
                    AddParameter("@Ngaynhaplieu", tg.Date)
                    AddParameter("@IDDanhsach", tb.Rows(i)("IDDanhSach"))
                    AddParameter("@Chitiet", "AT " & tb.Rows(i)("MaKH").ToString & " XK" & tb.Rows(i)("SoPhieuXK").ToString)
                    AddParameter("@Soluong", Math.Round(tb.Rows(i)("TienThu") / 1000000, 2))
                    AddParameter("@Khoiluong", tb.Rows(i)("TienThu"))
                    AddParameter("@Diem", tb.Rows(i)("Diem") * Math.Round(tb.Rows(i)("TienThu") / 1000000, 2))
                    AddParameter("@Duyet", False)
                    AddParameter("@IDNhanvien", TaiKhoan)
                    AddParameter("@IDUser", TaiKhoan)
                    AddParameter("@YearMonth", tg.ToString("yyyyMM"))
                    If doInsert("HDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Next
                If tb.Rows.Count > 0 Then
                    ShowAlert("Đã hoàn thành báo cáo tự động!")
                End If

            Catch ex As Exception
                ShowBaoLoi("Lỗi BC tự động: " & LoiNgoaiLe)
            End Try

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub BaoCao()
        If cbBCNhanVIen.EditValue Is Nothing Then
            cbBCNhanVIen.EditValue = Convert.ToInt32(TaiKhoan)
        End If
        If Not ShowCauHoi("Bạn đang lập báo cáo thu tiền " & Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy") & " cho nhân viên " & rcbBCNhanVien.GetDisplayText(cbBCNhanVIen.EditValue) & " ?") Then

            Exit Sub
        End If
        Application.DoEvents()
        Dim tg As DateTime = GetServerTime()
        Dim sql As String = ""
        sql &= " SELECT tbBaoCaoMoi.*,HDDanhSach.Diem  FROM (SELECT *,"
        sql &= " (Case WHEN datediff(day,NgayXK,Ngay) <-5 THEN"
        sql &= " 		39"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >=-5 AND datediff(day,NgayXK,Ngay) <=5 THEN"
        sql &= " 		35"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >5 AND datediff(day,NgayXK,Ngay) <=15 THEN"
        sql &= " 		36"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >15 AND datediff(day,NgayXK,Ngay) <=30 THEN"
        sql &= " 		37"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >30 AND datediff(day,NgayXK,Ngay) <=45 THEN"
        sql &= " 		38"
        sql &= " 	WHEN datediff(day,NgayXK,Ngay) >45 THEN"
        sql &= " 		40 "
        sql &= " END)IDDanhSach "
        sql &= " FROM ("
        sql &= " SELECT tb.*,tbBaoCao.KhoiLuong FROM "
        sql &= " (SELECT tbThu.NgaythangCT AS Ngay,PHIEUXUATKHO.NgayThang AS NgayXK,KHACHHANG.ttcMa AS MaKH, tbThu.Sotien AS TienThu, PHIEUXUATKHO.SophieuCG, PHIEUXUATKHO.Sophieu AS SoPhieuXK,"
        sql &= " dbo.PHIEUXUATKHO.IDTakecare"
        sql &= " FROM         "
        sql &= " (SELECT     NgaythangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "     FROM          dbo.THU"
        sql &= "     UNION ALL"
        sql &= "     SELECT     ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "     FROM         dbo.THUNH) AS tbThu "
        sql &= " INNER JOIN PHIEUXUATKHO ON tbThu.PhieuTC1 = PHIEUXUATKHO.Sophieu OR tbThu.PhieuTC0 = PHIEUXUATKHO.SophieuCG"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachHang "
        sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = " & Convert.ToDateTime(tbThang.EditValue).Month & " AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))=" & Convert.ToDateTime(tbThang.EditValue).Year
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = PHIEUXUATKHO.SophieuCG "
        sql &= " WHERE month(tbThu.NgaythangCT)= " & Convert.ToDateTime(tbThang.EditValue).Month & " AND year(tbThu.NgaythangCT)=" & Convert.ToDateTime(tbThang.EditValue).Year & " AND PHIEUXUATKHO.IDTakeCare=" & cbBCNhanVIen.EditValue & ")tb "
        sql &= " LEFT JOIN "
        sql &= " (SELECT HDNhanVien.ID,NgayBaoCao,KhoiLuong,ChiTiet,RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 9) SoPhieuXK,HDNhanVien.IDNhanVien FROM HDNhanVien "
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.Sophieu = RIGHT(RTRIM(dbo.HDNhanvien.Chitiet), 9)"
        sql &= " WHERE IDDanhSach IN (35,36,37,38,39,40) AND IDNhanVien=" & cbBCNhanVIen.EditValue & "  AND month(NgayBaoCao)=" & Convert.ToDateTime(tbThang.EditValue).Month & " AND year(NgayBaoCao)=" & Convert.ToDateTime(tbThang.EditValue).Year & ")tbBaoCao"
        sql &= " ON tb.SoPhieuXK=tbBaoCao.SoPhieuXK AND tb.TienThu=tbBaoCao.KhoiLuong)tblBaoCao2"
        sql &= " WHERE KhoiLuong IS NULL)tbBaoCaoMoi INNER JOIN HDDanhSach ON HDDanhSach.ID=tbBaoCaoMoi.IDDanhSach "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then

            Try
                For i As Integer = 0 To tb.Rows.Count - 1
                    AddParameter("@Ngaybaocao", tb.Rows(i)("Ngay"))
                    AddParameter("@Ngaynhaplieu", tg.Date)
                    AddParameter("@IDDanhsach", tb.Rows(i)("IDDanhSach"))
                    AddParameter("@Chitiet", "AT " & tb.Rows(i)("MaKH").ToString & " XK" & tb.Rows(i)("SoPhieuXK").ToString)
                    AddParameter("@Soluong", Math.Round(tb.Rows(i)("TienThu") / 1000000, 2))
                    AddParameter("@Khoiluong", tb.Rows(i)("TienThu"))
                    AddParameter("@Diem", tb.Rows(i)("Diem") * Math.Round(tb.Rows(i)("TienThu") / 1000000, 2))
                    AddParameter("@Duyet", False)
                    AddParameter("@IDNhanvien", cbBCNhanVIen.EditValue)
                    AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
                    AddParameter("@YearMonth", Convert.ToDateTime(tbThang.EditValue).ToString("yyyyMM"))
                    If doInsert("HDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Next
                If tb.Rows.Count > 0 Then
                    ShowAlert("Đã hoàn thành báo cáo tự động!")
                End If

            Catch ex As Exception
                ShowBaoLoi("Lỗi BC tự động: " & LoiNgoaiLe)
            End Try

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub CheckNewVersion()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT TOP 1 * FROM tblVersion ORDER BY ID DESC")
        If Not dt Is Nothing Then
            If dt.Rows(0)("Ver").ToString <> Application.ProductVersion.ToString Then
                If My.Computer.Name <> "BACBOSS" Then
                    ShowThongBao("Có phiên bản mới cần cập nhật !" & vbCrLf & "Phiên bản hiện tại: " _
                             & Application.ProductVersion.ToString & vbCrLf & "Phiên bản mới: " & dt.Rows(0)("Ver").ToString _
                             & vbCrLf & "Nội dung cập nhật: " & dt.Rows(0)("NoiDung").ToString)
                End If

                Dim psi As New ProcessStartInfo()
                With psi
                    .FileName = Application.StartupPath & "\UPDATE.exe"
                    .UseShellExecute = True
                End With
                Process.Start(psi)
            End If
        End If
    End Sub


#Region "Thay đổi con trỏ chuột trên menu"
    Private Sub BarManager1_HighlightedLinkChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.HighlightedLinkChangedEventArgs) Handles BarManager1.HighlightedLinkChanged
        If Not e.Link Is Nothing Then
            If TypeOf e.Link.Item Is BarSubItem Then
                Cursor = Cursors.Hand
            End If
            If TypeOf e.Link.Item Is BarButtonItem Then
                Cursor = Cursors.Hand
            End If
        Else
            Cursor = Cursors.Default
        End If
    End Sub
#End Region

#Region "Đóng mở tab"
    Private Function checkOpenTabs(ByVal name As String) As Boolean
        For i As Integer = 0 To tabMain.TabPages.Count - 1
            If tabMain.TabPages(i).Text = name Then
                tabMain.SelectedTabPageIndex = i
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub tabMain_CloseButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabMain.CloseButtonClick
        Dim tabIndex As Integer = tabMain.SelectedTabPageIndex

        tabMain.SelectedTabPage.Dispose()
        tabMain.SelectedTabPageIndex = tabIndex - 1
    End Sub

    Public Sub OpenTab(ByVal TieuDe As String, ByVal Tag As String, ByVal frm As Object, ByVal isOnly As Boolean, Optional ByVal img As Image = Nothing, Optional ByVal buttonTag As String = "")
        Dim _strTieuDe As String = TieuDe
        If isOnly Then
            If checkOpenTabs(_strTieuDe) = True Then Exit Sub
        End If
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = _strTieuDe
        t.Tag = buttonTag
        If TypeOf (frm) Is DevExpress.XtraEditors.XtraForm Then
            With CType(frm, DevExpress.XtraEditors.XtraForm)
                .Hide()
                .Tag = Tag
                .Dock = DockStyle.Fill
                .FormBorderStyle = Windows.Forms.FormBorderStyle.None

                If Not img Is Nothing Then
                    t.Image = img
                End If
                .TopLevel = False

                t.Controls.Add(frm)

                .Show()
            End With

        Else
            With CType(frm, DevExpress.XtraEditors.XtraUserControl)
                .Hide()
                .Tag = Tag
                .Dock = DockStyle.Fill

                If Not img Is Nothing Then
                    t.Image = img
                End If
                t.Controls.Add(frm)

                .Show()
            End With
        End If

        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

#End Region

#Region "Danh mục"

    Private Sub mQuanTriNguoiDung_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhongBan.ItemClick
        OpenTab("Cập nhật phòng ban", "frmPhongBan", New frmPhongBan, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mNganHang_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNganHang.ItemClick
        OpenTab("Ngân hàng", "frmNganHang", New frmNganHang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHinhThucTT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHinhThucTT.ItemClick
        OpenTab("Hình thức thanh toán", "frmHinhThucTT", New frmHinhThucTT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTienTe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTienTe.ItemClick
        OpenTab("Tiền tệ - Tỷ giá", "frmTienTe", New frmTienTe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTGGiaoHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTGGiaoHang.ItemClick
        OpenTab("Thời gian giao hàng", "frmTGGiaoHang", New frmCNTrangThai, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTaiKhoanNganHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiKhoanNganHang.ItemClick
        OpenTab("Tài khoản ngân hàng", "frmTaiKhoanNganHang", New frmTaiKhoanNganHang, True, Nothing, e.Item.Name)
    End Sub
#End Region

#Region "Khách hàng"

    Private Sub mDSKhachHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDSKhachHang.ItemClick
        OpenTab("Danh sách khách hàng", "frmKhachHang", New frmKhachHang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDSNguoiGiaoDich_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDSNguoiGiaoDich.ItemClick
        OpenTab("Danh sách người giao dịch", "frmNguoiGiaoDich", New frmNguoiGiaoDich, True, Nothing, e.Item.Name)
    End Sub
#End Region

#Region "Hệ thống"
    Private Sub mDoiMatKhau_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDoiMatKhau.ItemClick
        Dim f As New frmDoiMatKhau
        f.ShowDialog()
    End Sub

    Private Sub mXemManHinhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemManHinhLon.ItemClick
        Me.Hide()
        Dim f As New frmXemManHinhLon
        f.ShowDialog()
    End Sub

    Private Sub mQuyenTruyCap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mQuyenTruyCap.ItemClick
        OpenTab("Quyền truy cập", "QUYENTRUYCAP", New frmQuyenTruyCap, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDangXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDangXuat.ItemClick, btDangXuat.ItemClick
        _DangXuat = True
        Me.Close()
    End Sub

    Private Sub mThoat_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThoat.ItemClick
        Me.Close()
    End Sub

    Private Sub fMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If ShowCauHoi("Thoát chương trình ?") Then

            If _DangXuat Then
                _DangXuat = False
                frmDangNhap.Show()
                If Not frmDangNhap.chkNhoMK.Checked Then
                    frmDangNhap.tbMatKhau.EditValue = Nothing
                End If
            Else
                'Dim files() As String
                'files = IO.Directory.GetFiles(Application.StartupPath, "*.exe")

                ''IO.Directory.


                If System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                    ShowWaiting("Đang xoá cache ...")
                    Try
                        System.IO.Directory.Delete(Application.StartupPath & "\tmp", True)
                    Catch ex As Exception

                    End Try
                    CloseWaiting()
                End If

                frmDangNhap.Close()
            End If
        Else
            _DangXuat = False
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "Kinh doanh"
    Private Sub mChaoGia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChaoGia.ItemClick
        OpenTab("Chào giá", "CHAOGIA", New frmChaoGia, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mYeuCauDen_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mYeuCauDen.ItemClick
        OpenTab("Yêu cầu đến", "KINHDOANH", New frmYeuCauDen, True, Nothing, e.Item.Name)
    End Sub
#End Region

#Region "Cung ứng"
    Private Sub mYeuCauCanHoiGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mYeuCauCanHoiGia.ItemClick
        OpenTab("Yêu cầu cần hỏi giá", "MUAHANG", New frmYeuCauDen, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mYeuCauDi_DatHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mYeuCauDi_DatHang.ItemClick
        OpenTab("Yêu cầu đi - Đặt hàng", mYeuCauDi_DatHang.Tag, New frmYeuCauDi_DatHang, True, Nothing, e.Item.Name)
    End Sub
#End Region

#Region "Kỹ thuật"
    Private Sub mChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        OpenTab("Yêu cầu đền cần xử lý", "KYTHUAT", New frmYeuCauDen, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChaoGiaCanXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCongTrinhCanXuLy.ItemClick
        OpenTab("Lập vật tư, kế hoạch thi công", "CONGTRINHCANXULY", New frmCongTrinhCanXuLy, True, Nothing, e.Item.Name)
    End Sub
#End Region

#Region "Vật tư"

    Private Sub mThongTinPhu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThongTinPhu.ItemClick
        OpenTab("Thông tin phụ", "THONGTINPHU", New frmThongTinPhu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mThongSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThongSo.ItemClick
        OpenTab("Thông số", "THONGSO", New frmThongSo, True, Nothing, e.Item.Name)
    End Sub
#End Region

#Region "Phân quyền"
    Private Sub PhanQuyen()
        For i As Integer = 0 To BarMenu.ItemLinks.Count - 1
            If Not KiemTraQuyenMenu("Menu", BarMenu.ItemLinks(i).Item.Name) Then BarMenu.ItemLinks(i).Item.Enabled = False
            DeQuyPhanQuyen(BarMenu.ItemLinks(i))
        Next
    End Sub
    Public Sub DeQuyPhanQuyen(item As DevExpress.XtraBars.BarItemLink)
        If item.ToString = "DevExpress.XtraBars.BarSubItemLink" Then
            For j As Integer = 0 To CType(item, BarSubItemLink).Item.ItemLinks.Count - 1
                If Not KiemTraQuyenMenu("Menu", CType(item, BarSubItemLink).Item.ItemLinks(j).Item.Name) Then
                    CType(item, BarSubItemLink).Item.ItemLinks(j).Item.Enabled = False
                End If
                If CType(item, BarSubItemLink).Item.ItemLinks.Count > 0 Then
                    DeQuyPhanQuyen(CType(item, BarSubItemLink).Item.ItemLinks(j))
                End If
            Next
        End If
    End Sub
#End Region

#Region "Lịch làm việc"

    Public Sub LoadLichLamViec()
        Dim _startDate As DateTime = SchLichLamViec.Start
        Dim sql As String = ""
        sql &= " SELECT (Convert(bit,0))CongTrinh, tblLichLamViec.ID,TieuDe,DiaDiem,DoKhanCap,DoQuanTrong,BatDau,KetThuc,NoiDung,IDUser,NguoiThucHien,NguoiThucHien AS IDNgThucHien,"
        sql &= "     (ISNULL(NoiDung,'') + Char(10) + N'_______' + Char(10) + N'Ng lập: '+ NHANSU.Ten )NoiDung2,NhanSu.Ten AS NguoiLap "
        sql &= " FROM tblLichLamViec LEFT OUTER JOIN NHANSU ON tblLichLamViec.IDUser=NHANSU.ID"
        sql &= " WHERE 1=1 "

        If cbTieuChi.EditValue = "Từ đầu tháng" Then
            sql &= " AND (Convert(datetime,Convert(nvarchar,BatDau,103),103)>=@NgayDauThang OR Convert(datetime,Convert(nvarchar,KetThuc,103),103)>=@NgayDauThang)"
            AddParameterWhere("@NgayDauThang", New DateTime(Today.Year, Today.Month, 1))
        ElseIf cbTieuChi.EditValue = "Thời gian" Then
            sql &= " AND ((Convert(datetime,Convert(nvarchar,BatDau,103),103)>=@TuNgay AND Convert(datetime,Convert(nvarchar,BatDau,103),103)<=@DenNgay)"
            sql &= "  OR (Convert(datetime,Convert(nvarchar,KetThuc,103),103)>=@TuNgay AND Convert(datetime,Convert(nvarchar,KetThuc,103),103)<=@DenNgay))"
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        Dim _KhanCap As Integer = -1
        Select Case cbKhanCap.EditValue
            Case "Rất khẩn cấp"
                _KhanCap = 0
            Case "Khẩn cấp"
                _KhanCap = 1
            Case "Bình thường"
                _KhanCap = 2
            Case "Không khẩn cấp"
                _KhanCap = 3
        End Select

        Dim _QuanTrong As Integer = -1
        Select Case cbKhanCap.EditValue
            Case "Rất khẩn cấp"
                _QuanTrong = 0
            Case "Khẩn cấp"
                _QuanTrong = 1
            Case "Bình thường"
                _QuanTrong = 2
            Case "Không khẩn cấp"
                _QuanTrong = 3
        End Select
        If _KhanCap >= 0 Then
            sql &= " AND DoKhanCap = " & _KhanCap
        End If

        If _QuanTrong >= 0 Then
            sql &= " AND DoQuanTrong = " & _QuanTrong
        End If



        sql &= " UNION ALL"
        sql &= " SELECT (Convert(bit,1))CongTrinh, CHAOGIAAUX.ID,BANGCHAOGIA.Tenduan AS TieuDe, KHACHHANG.ttcMa AS DiaDiem,(0)DoQuanTrong,(0)DoKhanCap,"
        sql &= "     TGBatDau AS BatDau,TGKetThuc AS KetThuc,NoiDung, BANGCHAOGIA.IDNgXuLy AS IDUser, NVThucHien AS NguoiThucHien,NVThucHien AS IDNgThucHien,"
        sql &= "     (ISNULL(NoiDung,'') + Char(10) + N'_______' + Char(10) + N'Ng lập: '+ NHANSU.Ten )NoiDung2,NhanSu.Ten AS NguoiLap"
        sql &= " FROM CHAOGIAAUX INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=CHAOGIAAUX.Sophieu"
        sql &= "     INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
        sql &= "     LEFT OUTER JOIN NHANSU ON BANGCHAOGIA.IDNgXuLy=NHANSU.ID"
        sql &= " WHERE NVThucHien is not null"

        If cbTieuChi.EditValue = "Từ đầu tháng" Then
            sql &= " AND (Convert(datetime,Convert(nvarchar,TGBatDau,103),103)>=@NgayDauThang OR Convert(datetime,Convert(nvarchar,TGKetThuc,103),103)>=@NgayDauThang)"
            'AddParameterWhere("@NgayDauThang", New DateTime(Today.Year, Today.Month, 1))
        ElseIf cbTieuChi.EditValue = "Thời gian" Then
            sql &= " AND ((Convert(datetime,Convert(nvarchar,TGBatDau,103),103)>=@TuNgay AND Convert(datetime,Convert(nvarchar,TGBatDau,103),103)<=@DenNgay)"
            sql &= "  OR (Convert(datetime,Convert(nvarchar,TGKetThuc,103),103)>=@TuNgay AND Convert(datetime,Convert(nvarchar,TGKetThuc,103),103)<=@DenNgay))"

        End If


        If cbNhanVien.EditValue Is Nothing Then
            If Not cbPhongBan.EditValue Is Nothing Then
                sql &= " AND NVThucHien Like '%;" & "PB" & cbPhongBan.EditValue & ";%' "
            End If
        Else
            sql &= " AND NVThucHien Like '%;" & cbNhanVien.EditValue & ";%' "
        End If

        sql &= " UNION ALL"
        sql &= " SELECT (Convert(bit,1)) CongTrinh, tblBaoCaoLichThiCong.ID,(ISNULL(tblTuDien.NoiDung,'') + char(10) + ISNULL(tblBaoCaoLichThiCong.MoTa,'')) AS TieuDe,KHACHHANG.ttcMa AS DiaDiem,(0)DoQuanTrong,(0)DoKhanCap,"
        sql &= "   NgayBatDau,NgayKetThuc, tblTuDien.NoiDung, tblBaoCaoLichThiCong.IDNgNhap AS IDUser, IDNgThucHien AS NguoiThucHien, IDNgThucHien, "
        sql &= "     (ISNULL(tblTuDien.NoiDung,'') + Char(10) + N'_______' + Char(10) + N'Ng lập: '+ NHANSU.Ten )NoiDung2,NHANSU.Ten AS NguoiLap"
        sql &= "   FROM tblBaoCaoLichThiCong "
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=tblBaoCaoLichThiCong.SoYC "
        sql &= " LEFT OUTER JOIN NHANSU ON tblBaoCaoLichThiCong.IDNgNhap=NHANSU.ID"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGYEUCAU.IDKhachhang "
        sql &= " LEFT JOIN tblTuDien ON tblBaoCaoLichThiCong.IDNoiDung=tblTuDien.ID AND tblTuDien.Loai=6 "
        sql &= " WHERE GiaoViec=1 "

        If cbTieuChi.EditValue = "Từ đầu tháng" Then
            sql &= " AND (Convert(datetime,Convert(nvarchar,NgayBatDau,103),103)>=@NgayDauThang OR Convert(datetime,Convert(nvarchar,NgayKetThuc,103),103)>=@NgayDauThang)"
            ' AddParameterWhere("@NgayDauThang3", New DateTime(Today.Year, Today.Month, 1))
        ElseIf cbTieuChi.EditValue = "Thời gian" Then
            sql &= " AND ((Convert(datetime,Convert(nvarchar,NgayBatDau,103),103)>=@TuNgay AND Convert(datetime,Convert(nvarchar,NgayBatDau,103),103)<=@DenNgay)"
            sql &= "  OR (Convert(datetime,Convert(nvarchar,NgayKetThuc,103),103)>=@TuNgay AND Convert(datetime,Convert(nvarchar,NgayKetThuc,103),103)<=@DenNgay))"

        End If


        If Not cbPhongBan.EditValue Is Nothing Then
            sql &= " SELECT NHANSU.ID,NHANSU.Ten FROM NHANSU LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan WHERE NHANSU.Noictac=74 AND NHANSU.TrangThai=1 AND NHANSU.IDDepatment = " & cbPhongBan.EditValue & " ORDER BY NHANSU.IDDepatment,NhanSu_BoPhan.MaBP,NHANSU.ChucVu,NHANSU.ID "
        Else
            sql &= " SELECT NHANSU.ID,NHANSU.Ten FROM NHANSU LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan WHERE NHANSU.Noictac=74 AND NHANSU.TrangThai=1 ORDER BY NHANSU.IDDepatment,NhanSu_BoPhan.MaBP,NHANSU.ChucVu,NHANSU.ID "
        End If

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            'For i As Integer = 0 To tb.Rows.Count - 1
            '    Dim tb2 As DataTable = DataSourceDSFile(tb.Rows(i)("NguoiThucHien").ToString, , ",")
            '    'tb.Rows(i)("IDNgThucHien") = ""
            '    For j As Integer = 0 To tb2.Rows.Count - 1
            '        AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
            '        Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
            '        If Not tb3 Is Nothing Then
            '            tb.Rows(i)("NoiDung2") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
            '        End If
            '    Next
            '    tb.Rows(i)("NoiDung2") = tb.Rows(i)("NoiDung2").ToString.Trim
            'Next

            Dim tb2 As DataTable = ds.Tables(0).Clone
            tb2.Columns("NguoiThucHien").DataType = System.Type.GetType("System.Int32")
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim tb3 As DataTable = DataSourceDSFile(ds.Tables(0).Rows(i)("NguoiThucHien").ToString, , ",")
                For j As Integer = 0 To tb3.Rows.Count - 1
                    Try
                        Convert.ToInt32(tb3.Rows(j)(0))
                        tb2.Rows.Add(tb2.NewRow())
                        tb2.Rows(tb2.Rows.Count - 1)("CongTrinh") = ds.Tables(0).Rows(i)("CongTrinh")
                        tb2.Rows(tb2.Rows.Count - 1)("ID") = ds.Tables(0).Rows(i)("ID")
                        tb2.Rows(tb2.Rows.Count - 1)("TieuDe") = ds.Tables(0).Rows(i)("TieuDe")
                        tb2.Rows(tb2.Rows.Count - 1)("DiaDiem") = ds.Tables(0).Rows(i)("DiaDiem")
                        tb2.Rows(tb2.Rows.Count - 1)("DoQuanTrong") = ds.Tables(0).Rows(i)("DoQuanTrong")
                        tb2.Rows(tb2.Rows.Count - 1)("DoKhanCap") = ds.Tables(0).Rows(i)("DoKhanCap")
                        tb2.Rows(tb2.Rows.Count - 1)("BatDau") = ds.Tables(0).Rows(i)("BatDau")
                        tb2.Rows(tb2.Rows.Count - 1)("KetThuc") = ds.Tables(0).Rows(i)("KetThuc")
                        tb2.Rows(tb2.Rows.Count - 1)("NoiDung") = ds.Tables(0).Rows(i)("NoiDung")
                        tb2.Rows(tb2.Rows.Count - 1)("IDUser") = ds.Tables(0).Rows(i)("IDUser")
                        tb2.Rows(tb2.Rows.Count - 1)("IDNgThucHien") = ds.Tables(0).Rows(i)("IDNgThucHien")
                        tb2.Rows(tb2.Rows.Count - 1)("NguoiThucHien") = tb3.Rows(j)(0)
                        tb2.Rows(tb2.Rows.Count - 1)("NoiDung2") = ds.Tables(0).Rows(i)("NoiDung2")
                        tb2.Rows(tb2.Rows.Count - 1)("NguoiLap") = ds.Tables(0).Rows(i)("NguoiLap")
                    Catch ex As Exception

                    End Try

                Next
            Next
            SchLichLamViec.Storage.Resources.DataSource = ds.Tables(1)
            SchLichLamViec.Storage.Appointments.DataSource = tb2
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        SchLichLamViec.Start = _startDate
    End Sub

    Private Sub btChonToanBo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChonToanBo.ItemClick
        Dim check As CheckState
        If TreeNV.Items(0).CheckState = CheckState.Checked Then
            check = CheckState.Unchecked
        Else
            check = CheckState.Checked
        End If

        For i As Integer = 0 To TreeNV.ItemCount - 1
            TreeNV.Items(i).CheckState = check
        Next
    End Sub

    Private Sub SchLichLamViec_EditAppointmentFormShowing(sender As System.Object, e As DevExpress.XtraScheduler.AppointmentFormEventArgs) Handles SchLichLamViec.EditAppointmentFormShowing
        e.Handled = True
        If SchLichLamViec.SelectedAppointments.Count > 0 Then
            TrangThai.isUpdate = True
            objID = SchLichLamViec.SelectedAppointments(0).CustomFields("ID")
            If SchLichLamViec.SelectedAppointments(0).CustomFields("CongTrinh") Then
                Dim sql As String = ""

                sql &= " SELECT KHACHHANG.ttcMa,tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.SoCG,NGD.Ten AS NguoiGD, NGD.Mobile AS DTNgd,"
                sql &= " KHACHHANG.Ten AS TenKH, KHACHHANG.ttcDiaChi,tblTuDien.NoiDung AS NhomCV,tblBaoCaoLichThiCong.NgayBatDau,tblBaoCaoLichThiCong.NgayKetThuc,"
                sql &= " tblBaoCaoLichThiCong.MoTa,KD.Ten AS PhuTrach"
                sql &= " FROM tblBaoCaoLichThiCong "
                sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC =BANGYEUCAU.SoPhieu"
                sql &= " INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachHang=KHACHHANG.ID"
                sql &= " LEFT JOIN NHANSU AS NGD ON NGD.ID=BANGYEUCAU.IDNgd"
                sql &= " LEFT JOIN NHANSU AS KD ON KD.ID=BANGYEUCAU.IDTakeCare"
                sql &= " LEFT JOIN tblTuDien ON tblBaoCaoLichThiCong.IDNoiDung=tblTuDien.ID AND tblTuDien.Loai=6 "
                sql &= " WHERE GIaoViec=1 AND tblBaoCaoLichThiCong.ID=" & objID
                Dim tb As DataTable = ExecuteSQLDataTable(sql)
                Dim str As String = ""
                If Not tb Is Nothing Then

                    str &= "- Mã KH:  " & tb.Rows(0)("ttcMa") & " - YC: " & tb.Rows(0)("SoYC") & " - CG: " & tb.Rows(0)("SoCG") & " - Người GD: " & tb.Rows(0)("NguoiGD") & " " & tb.Rows(0)("DTNgd")
                    str &= vbCrLf & "- Tên KH: " & tb.Rows(0)("TenKH")
                    str &= vbCrLf & "- Đ/c KH: " & tb.Rows(0)("ttcDiaChi")
                    str &= vbCrLf & "- Công việc: " & tb.Rows(0)("NhomCV")
                    str &= vbCrLf & "- Thời gian: " & Convert.ToDateTime(tb.Rows(0)("NgayBatDau")).ToString("HH:mm dd/MM/yyyy") & " -> " & Convert.ToDateTime(tb.Rows(0)("NgayKetThuc")).ToString("HH:mm dd/MM/yyyy")
                    str &= vbCrLf & "- Nội dung chi tiết: " & tb.Rows(0)("Mota")
                    str &= vbCrLf & "- NV Kinh doanh: " & tb.Rows(0)("PhuTrach")
                    str &= vbCrLf & "- NV Thực hiện: "
                    Dim tb2 As DataTable = DataSourceDSFile(SchLichLamViec.SelectedAppointments(0).CustomFields("IDNgThucHien").ToString, , ",")

                    For j As Integer = 0 To tb2.Rows.Count - 1
                        AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                        Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb3 Is Nothing Then
                            str &= vbCrLf & "  . " & tb3.Rows(0)(0).ToString
                        End If
                    Next
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If


                tbChiTiet.EditValue = str
                pChiTiet.Visible = True
                Exit Sub
            End If
        Else
            TrangThai.isAddNew = True
        End If

        Dim f As New frmCNLichLamViec
        If TrangThai.isUpdate Then
            f.CongTrinh = SchLichLamViec.SelectedAppointments(0).CustomFields("CongTrinh")
            f.IDUser = Convert.ToInt32(SchLichLamViec.SelectedAppointments(0).CustomFields("IDUser"))
        End If

        f.ShowDialog()
    End Sub

    Private Sub rcbPhongBan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhongBan.ButtonClick
        If e.Button.Index = 1 Then
            cbPhongBan.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub loadDSPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhongBan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhanVien()
        Dim sql As String = "SELECT ID,Ten FROM NHANSU WHERE Noictac=74 AND Trangthai=1 "
        'If Not cbPhongBan.EditValue Is Nothing Then
        '    sql &= " AND IDDepatment=" & cbPhongBan.EditValue
        'End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            'rcbNhanVien.DataSource = tb
            'cbNhanVien.EditValue = CType(TaiKhoan, Int32)
            rcbBCNhanVien.DataSource = tb
            cbBCNhanVIen.EditValue = CType(TaiKhoan, Int32)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub cbPhongBan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhongBan.EditValueChanged
        LoadLichLamViec()
    End Sub

    Private Sub btTaiLichLamViec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLichLamViec.ItemClick
        LoadLichLamViec()
        If SchLichLamViec.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Day Then
            SchLichLamViec.Views.DayView.DayCount = cbSoNgay.EditValue
        End If
    End Sub

    Private Sub SchedulerStorage1_AppointmentDeleting(sender As System.Object, e As DevExpress.XtraScheduler.PersistentObjectCancelEventArgs) Handles SchedulerStorage1.AppointmentDeleting
        If Convert.ToInt32(TaiKhoan) <> Convert.ToInt32(SchLichLamViec.SelectedAppointments(0).CustomFields("IDUser")) Then
            ShowCanhBao("Bạn không có quyền xoá lịch làm việc của người khác")
            e.Cancel = True
            Exit Sub
        End If
        If SchLichLamViec.SelectedAppointments(0).CustomFields("CongTrinh") Then
            ShowCanhBao("Bạn không thể xoá nội dung giao việc")
            e.Cancel = True
            Exit Sub
        End If

        If SchLichLamViec.SelectedAppointments(0).Start < GetServerTime() Then
            ShowCanhBao("Bạn không thể xoá nội dung đã qua")
            e.Cancel = True
            Exit Sub
        End If

        If ShowCauHoi("Xoá công việc này ?") Then
            AddParameterWhere("@IDCongViec", SchLichLamViec.SelectedAppointments(0).CustomFields("ID"))
            If doDelete("tblLichLamViec", "ID=@IDCongViec") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                e.Cancel = True
            End If
            ShowAlert("Đã xoá !")
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub SchLichLamViec_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraScheduler.PopupMenuShowingEventArgs) Handles SchLichLamViec.PopupMenuShowing
        If e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then
            e.Menu.Items(0).Caption = "Xem"
            e.Menu.Items(1).Visible = False
            e.Menu.Items(2).Visible = False
            e.Menu.Items(3).Visible = False
            e.Menu.Items(4).Visible = False
            e.Menu.Items(5).Caption = "Xoá"
        Else
            Select Case SchLichLamViec.ActiveViewType
                Case DevExpress.XtraScheduler.SchedulerViewType.Day
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Ngày hôm nay"
                    e.Menu.Items(3).Caption = "Chuyển đến ngày"
                    e.Menu.Items(4).Caption = "Chế độ xem"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.Month
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Chuyển tới ngày này"
                    e.Menu.Items(3).Caption = "Ngày hôm nay"
                    e.Menu.Items(4).Caption = "Chuyển đến ngày"
                    e.Menu.Items(5).Caption = "Chế độ xem"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.Timeline
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Ngày hôm nay"
                    e.Menu.Items(3).Caption = "Chuyển đến ngày"
                    e.Menu.Items(4).Caption = "Tuỳ chọn hiển thị"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Năm"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Quý"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Tháng"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Ngày"
                    'CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(5).Caption = "Giờ"
                    e.Menu.Items(5).Visible = False
                    e.Menu.Items(6).Caption = "Chế độ xem"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.Week
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Chuyển tới ngày này"
                    e.Menu.Items(3).Caption = "Ngày hôm nay"
                    e.Menu.Items(4).Caption = "Chuyển đến ngày"
                    e.Menu.Items(5).Caption = "Chế độ xem"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.WorkWeek
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Ngày hôm nay"
                    e.Menu.Items(3).Caption = "Chuyển đến ngày"
                    e.Menu.Items(4).Caption = "Chế độ xem"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
            End Select
        End If

    End Sub

    Private Sub SchLichLamViec_GotoDateFormShowing(sender As System.Object, e As DevExpress.XtraScheduler.GotoDateFormEventArgs) Handles SchLichLamViec.GotoDateFormShowing
        e.Handled = True
        Dim f As New frmDenNgay
        f.ShowDialog()
    End Sub

#End Region

    Private Sub mPhienBan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhienBan.ItemClick
        Dim f As New frmPhienBan
        f.ShowDialog()
    End Sub

    Private Sub mNhanSu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhanSu.ItemClick
        OpenTab("Nhân sự", "frmNhanSu", New frmNhanSu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub TimeAutoLoad_Tick(sender As System.Object, e As System.EventArgs) Handles TimeAutoLoad.Tick
        LoadLichLamViec()
    End Sub

    Private Sub mLichHoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLichDaoTao.ItemClick
        OpenTab("Lịch học", "frmLichHoc", New frmLichDaoTao, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mMonHoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNoiDungDaotao.ItemClick
        OpenTab("Đào tạo", "frmDaoTao", New frmNoiDungDaoTao, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDinhMucDiemCongTrinh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDinhMucDiemCongTrinh.ItemClick
        OpenTab("Định mức điểm công trình", "frmDinhMucDiemCongTrinh", New frmDinhMucDiemCongTrinh, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBaoCaoCongViec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuyetBaoCaoThiCong.ItemClick
        OpenTab("Báo cáo, duyệt lịch thi công", "frmBaoCaoLichThiCong", New frmBaoCaoLichThiCong, True, Nothing, e.Item.Name)
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBaoCaoLichThiCong.ItemClick
        OpenTab("Báo cáo lịch thi công", "frmLichThiCongCongTrinh", New frmLichThiCongCongTrinh, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btAn_Click(sender As System.Object, e As System.EventArgs) Handles btAn.Click
        pChiTiet.Visible = False
    End Sub

    Private Sub mXuatMuon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuatMuon.ItemClick
        OpenTab("Xuất mượn", "frmXuatMuon", New frmXuatMuon, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDanhMucNangLuc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDanhMucNangLuc.ItemClick
        OpenTab("Danh mục năng lực, kỹ năng", "frmDanhMucNangLuc", New frmDanhMucNangLuc, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemThiKyNang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDiemThiKyNang.ItemClick
        OpenTab("Điểm thi kỹ năng", "frmDiemThiKyNang", New frmDiemThiKyNang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDiemSo.ItemClick
        OpenTab("Điểm số", "frmDiemSo", New frmDiemSo1, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTheoXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTheoXuatKho.ItemClick
        OpenTab("Kết quả xuất kho, phiếu thu", "frmKetQuaXuatKho", New frmKetQuaXuatKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaChaoGia.ItemClick
        OpenTab("Kết quả chào giá", "frmKetQuaChaoGia", New frmKetQuaChaoGia, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopChamCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopChamCong.ItemClick
        OpenTab("Tổng hợp chấm công", "frmTHChamCong", New frmTHChamCong, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDinhMucLuongThuong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDinhMucLuongThuong.ItemClick
        OpenTab("Định mức lương thưởng", "frmDMLuongThuong", New frmDMLuongThuong, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mLuongThuong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLuongThuong.ItemClick
        OpenTab("Lương thưởng", "frmLuongThuong", New frmLuongThuong, True, Nothing, e.Item.Name)
    End Sub

    Private Sub lbVer_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles lbVer.ItemClick

        Dim dt As DataTable = ExecuteSQLDataTable("SELECT TOP 1 * FROM tblVersion ORDER BY ID DESC")
        If Not dt Is Nothing Then
            If dt.Rows(0)("Ver").ToString <> Application.ProductVersion.ToString Then
                ShowThongBao("Có phiên bản mới cần cập nhật !" & vbCrLf & "Phiên bản hiện tại: " _
                             & Application.ProductVersion.ToString & vbCrLf & "Phiên bản mới: " & dt.Rows(0)("Ver").ToString _
                             & vbCrLf & "Nội dung cập nhật: " & dt.Rows(0)("NoiDung").ToString)
                Dim psi As New ProcessStartInfo()
                With psi
                    .FileName = Application.StartupPath & "\UPDATE.exe"
                    .UseShellExecute = True
                End With
                Process.Start(psi)
            Else
                ShowAlert("Bạn đang ở phiên bản mới nhất !")
            End If
        End If

    End Sub

    Private Sub mTongHopDiemKN_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopDiemKN.ItemClick
        OpenTab("Tổng hợp điểm kỹ năng", "frmDiemThiKyNang2", New frmTHDiemThiKyNang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btBaoCaoThuTien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btBaoCaoThuTien.ItemClick
        ' ShowAlert(cbNhanVien.EditValue)
        BaoCao()
    End Sub

    Private Sub btChamCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim f As New frmTestChamCong
        f.ShowDialog()
    End Sub

    Private Sub mTongHopDiemQuyTrinhCu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopDiemQuyTrinhCu.ItemClick
        OpenTab("Tổng hợp điểm quy trình", "frmTHDiemQuyTrinhCu", New frmTHDiemQuyTrinhCu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaTongHopXK_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaTongHopXK.ItemClick
        OpenTab("Tổng hợp xuất kho", "frmTongHopXK", New frmTongHopXK, True, Nothing, e.Item.Name)

    End Sub

    Private Sub mXuatKhoCongTrinh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuatKhoCongTrinh.ItemClick
        'OpenTab("Xuất kho công trình", "frmXuatKhoCT", New frmXuatKhoCT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuatKho.ItemClick
        OpenTab("Xuất kho", "frmXuatKho", New frmXuatKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChaoGiaCanXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChaoGiaCanXuat.ItemClick
        OpenTab("Chào giá cần xuất kho", "frmCGCanXuatKho", New frmCGCanXuatKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTonKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTonKho.ItemClick
        OpenTab("Tồn kho", "frmTonKho", New frmTonKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mNhapKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhapKho.ItemClick
        OpenTab("Nhập kho", "frmNhapKho", New frmNhapKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDHCanNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDHCanNhap.ItemClick
        OpenTab("Đặt hàng cần nhập kho", "frmDHCanNhapKho", New frmDHCanNhapKho, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mMucDichThuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mMucDichThuChi.ItemClick
        OpenTab("Mục đích thu chi", "frmMucDichThuChi", New frmMucDichThuChi, True, Nothing, e.Item.Name)
    End Sub


    Private Sub rcbBCNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbBCNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbBCNhanVIen.EditValue = Nothing
        End If
    End Sub

    Private Sub mVatTuDaChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mVatTuDaChaoGia.ItemClick
        OpenTab("Hàng đã chào giá", "frmVatTuDaChaoGia", New frmVatTuDaChaoGia, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mVatTuDaDatHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mVatTuDaDatHang.ItemClick
        OpenTab("Hàng đã đặt", "frmVatTuDaDatHang", New frmVatTuDaDatHang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBaoCaoHangNgay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBaoCaoHangNgay.ItemClick
        OpenTab("Báo cáo hàng ngày", "frmBaoCaoHangNgay", New frmBaoCaoHangNgay, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTieuChiBaoCao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTieuChiBaoCao.ItemClick
        OpenTab("Tiêu chí báo cáo", "frmTieuChiBaoCao", New frmTieuChiBaoCao, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKiemTraBaoCaoKT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKiemTraBaoCaoKT.ItemClick
        OpenTab("Kiểm quả làm việc của kỹ thuật", "frmKiemTraBaoCaoKT", New frmKiemTraBaoCaoKTTest, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mCongNoPhaiThuPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCongNoPhaiThuPhaiTra.ItemClick
        OpenTab("Công nợ phải thu - phải trả", "frmCongNo", New frmCongNo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTHCongNo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTHCongNo.ItemClick
        OpenTab("Tổng hợp công nợ", "frmTHCongNo", New frmTHCongNo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBCTongHop_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBCTongHop.ItemClick
        OpenTab("BC Tổng hợp", "frmBCTongHop", New frmBCTongHop, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTruSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        'OpenTab("Trụ sở", "frmTruSo", New frmTruSo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopLNKT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopLNKT.ItemClick
        OpenTab("Tổng hợp lợi nhuận KT", "frmTongHopLNKT", New frmTongHopLNKT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopDiemNV_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopDiemNV.ItemClick
        OpenTab("Tổng hợp điểm nhân viên", "frmTHDiemNV", New frmTHDiemNV, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChiTieuKhachHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChiTieuKhachHang.ItemClick
        OpenTab("Chỉ tiêu khách hàng", "frmChiTieuKhachHang", New frmChiTieu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBaoCaoKinhDoanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBaoCaoKinhDoanh.ItemClick
        OpenTab("Báo cáo kinh doanh", "frmBaoCaoKinhDoanh", New frmBaoCaoKinhDoanh, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mEmailMarketing_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mEmailMarketing.ItemClick
        OpenTab("Email Marketing", "frmDuyetEmail", New frmDuyetEmail, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBCDoanhThuTheoHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBCDoanhThuTheoHang.ItemClick
        OpenTab("Báo cáo doanh thu theo hãng", "frmBCKDTheoDongSP", New frmBCKDTheoDongSP, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBaoCaoOnline_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBaoCaoOnline.ItemClick
        'OpenTab("Báo cáo Online", "frmBCOnline", New frmBCOnline, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mThuongCuoiNam_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuongCuoiNam.ItemClick
        OpenTab("Thưởng cuối năm", "frmThuongCuoiNam", New frmThuongCuoiNam, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChiTieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChiTieu.ItemClick
        OpenTab("Chỉ tiêu", "frmChiTieu", New frmChiTieu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mXuLyYeuCauHoiGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuLyYeuCauHoiGia.ItemClick
        OpenTab("Xử lý yêu cầu", "frmXuLyYeuCau", New frmXuLyYeuCau, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mYeuCauHoiGiaNCC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mYeuCauHoiGiaNCC.ItemClick
        OpenTab("Yêu cầu hỏi giá NCC", "frmYeuCauHoiGia", New frmYeuCauHoiGia, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mXuLyYeuCauCanChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuLyYeuCauCanChuyenMa.ItemClick
        OpenTab("Chuyển mã", "frmChuyenMa", New frmChuyenMa, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDanhMucTrangThai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDanhMucTrangThai.ItemClick
        OpenTab("Danh mục trạng thái", "frmCNTrangThai", New frmCNTrangThai, True, Nothing, e.Item.Name)
    End Sub

    Private Sub TimerLoadThongBao_Tick(sender As System.Object, e As System.EventArgs) Handles TimerLoadThongBao.Tick
        Try
            Dim str As String = " SET DATEFORMAT DMY SELECT count(ID) FROM tblThongBao WHERE IDNhanVien=" & TaiKhoan & " AND DaXem = 0;"
            str &= " SELECT Count(ID) FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND month(NgaySinh)=Month(getdate()) AND day(NgaySinh)=Day(GetDate());"
            str &= " select chisohientai, dinhmuc from dinhmuc  where chisohientai>=dinhmuc"

            Dim ds As DataSet = ExecuteSQLDataSet(str)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows(0)(0) = 0 Then
                    btThongBaoMoi.Visibility = BarItemVisibility.Never
                Else
                    If btThongBaoMoi.Visibility = BarItemVisibility.Never Then
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                    End If
                    btThongBaoMoi.Visibility = BarItemVisibility.Always
                    btThongBaoMoi.Caption = ds.Tables(0).Rows(0)(0).ToString
                End If

                If ds.Tables(1).Rows(0)(0) = 0 Then
                    btSinhNhat.Visibility = BarItemVisibility.Never
                Else
                    If btSinhNhat.Visibility = BarItemVisibility.Never Then
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
                    End If
                    btSinhNhat.Visibility = BarItemVisibility.Always
                    btSinhNhat.Caption = ds.Tables(1).Rows(0)(0).ToString
                End If
                If ds.Tables(2).Rows.Count > 0 Then
                    bsiTinhTrangXe.Enabled = True
                    bsiTinhTrangXe.Visibility = BarItemVisibility.Always
                    bsiTinhTrangXe.Caption = "Có xe cần bảo dưỡng"
                Else
                    bsiTinhTrangXe.Enabled = False
                    bsiTinhTrangXe.Visibility = BarItemVisibility.Never
                    bsiTinhTrangXe.Caption = "."
                End If
            Else
                btThongBaoMoi.Visibility = BarItemVisibility.Always
                btThongBaoMoi.Caption = "Lỗi phát sinh !"
            End If
        Catch ex As Exception
            btThongBaoMoi.Visibility = BarItemVisibility.Always
            btThongBaoMoi.Caption = "Lỗi phát sinh !"
        End Try

    End Sub

    Private Sub btThongBaoMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThongBaoMoi.ItemClick
        OpenTab("Thông báo", "frmThongBao", New frmThongBao, True, Nothing, e.Item.Name)
        CType(tabMain.SelectedTabPage.Controls(0), frmThongBao).LoadDS()
    End Sub

    Private Sub mNhaCCTheoHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhaCCTheoHang.ItemClick
        OpenTab("Nhà cung cấp theo hãng SX", "frmNhaCCTheoHangSX", New frmNhaCCTheoHangSX, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopQuaTrinhGiaoDich_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopQuaTrinhGiaoDich.ItemClick
        OpenTab("Tổng hợp quá trình giao dịch", "frmTongHopQuaTrinhGD", New frmTongHopQuaTrinhGD, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mGopY_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mYeuCauNoiBo.ItemClick
        OpenTab("Yêu cầu nội bộ", "frmYeuCauNoiBo", New frmYeuCauNoiBo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mNguonKhachMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNguonKhachMoi.ItemClick
        OpenTab("Nguồn khách mới", "frmNguonKhachMoi", New frmNguonKhachMoi, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaXuLyYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaXuLyYeuCau.ItemClick
        OpenTab("Kết quả xử lý yêu cầu", "frmKetQuaXuLyYeuCau", New frmKetQuaXuLyYeuCau, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopDiemNangLuc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopDiemNangLuc.ItemClick
        OpenTab("Tổng hợp điểm năng lực", "frmTHDiemThiNangLuc", New frmTHDiemThiNangLuc, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopYeuCau.ItemClick
        OpenTab("Tổng hợp yêu cầu", "frmTongHopYeuCau", New frmTongHopYeuCau, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mXuLyYCCongTrinh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXuLyYCCongTrinh.ItemClick
        OpenTab("Lập vật tư chào giá", "frmXuLyYCCongTrinh", New frmXuLyYCCongTrinh, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDiemChuyenMa.ItemClick
        OpenTab("Điểm chuyển mã", "frmDiemChuyenMa", New frmDiemChuyenMa, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaXuLyYCCungUng_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaXuLyYCCungUng.ItemClick
        OpenTab("Kết quả xử lý yêu cầu hỏi giá", "frmKetQuaXuLyYCCungUng", New frmKetQuaXuLyYCCungUng, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChamCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChamCong.ItemClick
        OpenTab("Chấm công", "frmChamCong", New frmChamCong, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDuKienCongNo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienCongNo.ItemClick
        OpenTab("Dự kiến công nợ", "frmDuKienCongNo", New frmDuKienCongNo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDatHangCanThanhToan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDatHangCanThanhToan.ItemClick
        OpenTab("Đặt hàng cần thanh toán", "frmDatHangCanThanhToan", New frmDatHangCanThanhToan, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHoTroOnline_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHoTroOnline.ItemClick
        OpenTab("Hỗ trợ online", "frmHoTroKhachHangOnline", New frmHoTroKhachHangOnline, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mEmail_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mEmail.ItemClick
        OpenTab("Email", "frmDsEmailCongTy", New frmDsEmailCongTy, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mThuChiTienMatTest_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuChiTienMatTest.ItemClick
        OpenTab("Thu chi tiền mặt", "frmThuChiTM2", New frmThuChiTM2, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mThuChiNganHangTest_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuChiNganHangTest.ItemClick
        OpenTab("Thu chi ngân hàng", "frmThuChiNH2", New frmThuChiNH2, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mCongNoPhaiThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCongNoPhaiThu.ItemClick
        OpenTab("Công nợ phải thu", "frmCongNoPhaiThu", New frmCongNoPhaiThu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mCongNoPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCongNoPhaiTra.ItemClick
        OpenTab("Công nợ phải trả", "frmCongNoPhaiTra", New frmCongNoPhaiTra, True, Nothing, e.Item.Name)
    End Sub
    Private Sub mCongNoPhaiTraMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCongNoPhaiTraMoi.ItemClick
        OpenTab("Công nợ phải trả mới", "frmCongNoTraMoi", New frmCongNoTraMoi, True, Nothing, e.Item.Name)
    End Sub
    Private Sub mCongNoPhaiThuPhaiTraTest_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCongNoPhaiThuPhaiTraTest.ItemClick
        ' OpenTab("Công nợ phải thu - phải trả (test)", "frmCongNo2", New frmCongNo2, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTHDuKienPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTHDuKienPhaiTra.ItemClick
        OpenTab("Tổng hợp dự kiến phải trả", "frmDuKienPhaiTra", New frmDuKienPhaiTra, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTHDuKienPhaiThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTHDuKienPhaiThu.ItemClick
        OpenTab("Tổng hợp dự kiến phải thu", "frmDuKienPhaiThu", New frmDuKienPhaiThu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHangDangVe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangDangVe.ItemClick
        OpenTab("Hàng đang về", "frmDangVe", New frmDangVe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHangCanXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangCanXuat.ItemClick
        OpenTab("Hàng cần xuất", "frmCanXuat", New frmCanXuat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHangDaNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangDaNhap.ItemClick
        OpenTab("Hàng đã nhập", "frmHangDaNhap", New frmHangDaNhap, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHangDaXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangDaXuat.ItemClick
        OpenTab("Hàng đã xuất", "frmHangDaXuat", New frmHangDaXuat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDanhSachNhaCungCap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDanhSachNhaCungCap.ItemClick
        OpenTab("Danh sách nhà cung cấp", "frmNhaCungCap", New frmNhaCungCap, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDanhSachNguoiGGNCC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDanhSachNguoiGGNCC.ItemClick
        OpenTab("Danh sách người giao dịch NCC", "frmNguoiGiaoDichNCC", New frmNguoiGiaoDichNCC, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemSoThuNghiem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        ' OpenTab("Điểm số (Thử nghiệm)", "frmDiemSo2", New frmDiemSo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mMHDangVe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mMHDangVe.ItemClick
        OpenTab("Hàng đang về (Nhóm mua)", "frmMHDangVe", New frmMHDangVe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mMHCanXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mMHCanXuat.ItemClick
        OpenTab("Hàng cần xuất (Nhóm mua)", "frmMHCanXuat", New frmMHCanXuat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btSinhNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSinhNhat.ItemClick
        OpenTab("Nhân sự", "frmNhanSu", New frmNhanSu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaBaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaBaoGia.ItemClick
        OpenTab("Kết quả báo giá", "frmKetQuaBaoGia", New frmKetQuaBaoGia, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuHoaDonDauRa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuHoaDonDauRa.ItemClick
        OpenTab("Hóa đơn đầu ra", "frmHoaDonDauRa", New frmHoaDonDauRa, True, Nothing, e.Item.Name)
    End Sub




    Private Sub mnuHoaDonDauVao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuHoaDonDauVao.ItemClick
        OpenTab("Hóa đơn đầu vào", "frmHoaDonDauVao", New frmHoaDonDauVao, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuTonKhoThueDauKy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTonKhoThueDauKy.ItemClick
        OpenTab("Tồn kho thuế đầu kỳ", "frmTonKhoThueDauKy", New frmTonKhoThueDauKy, True, Nothing, e.Item.Name)
    End Sub

#Region "-- MENU THUE --"

    Private Sub mnuThueMuaHangTrongNuoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueMuaHangTrongNuoc.ItemClick
        If checkOpenTabs("Hóa đơn mua hàng trong nước") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThueMuaHangTrongNuoc.Glyph
        t.Text = "Hóa đơn mua hàng trong nước"
        t.Tag = mnuThueMuaHangTrongNuoc.Name
        Dim f As New frmHoaDonDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaHangTrongNuoc
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThueBanHangHoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueBanHangHoa.ItemClick
        If checkOpenTabs("Hóa đơn bán hàng") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = "Hóa đơn bán hàng"
        t.Tag = mnuThueBanHangHoa.Name
        t.Image = mnuThueBanHangHoa.Glyph
        Dim f As New frmHoaDonDauRa
        f.LoaiCT2 = ChungTu.LoaiCT2.BanHangHoa
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuBanHangDichVu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuBanHangDichVu.ItemClick
        If checkOpenTabs("Hóa đơn bán dịch vụ") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = "Hóa đơn bán dịch vụ"
        t.Tag = mnuBanHangDichVu.Name
        t.Image = mnuBanHangDichVu.Glyph
        Dim f As New frmHoaDonDauRa
        f.LoaiCT2 = ChungTu.LoaiCT2.BanDichVu
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThueXuLyCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueXuLyCT.ItemClick
        If checkOpenTabs("Hóa đơn công trình") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = "Hóa đơn công trình"
        t.Tag = mnuThueXuLyCT.Name
        t.Image = mnuThueXuLyCT.Glyph
        Dim f As New frmHoaDonDauRa
        f.LoaiCT2 = ChungTu.LoaiCT2.XuLyCongTrinh
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub


    Private Sub mnuThueMuaDichVu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueMuaDichVu.ItemClick
        If checkOpenTabs("Hóa đơn mua dịch vụ") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = "Hóa đơn mua dịch vụ"
        t.Tag = mnuThueMuaDichVu.Name
        t.Image = mnuThueMuaDichVu.Glyph
        Dim f As New frmHoaDonDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThueHoaDonCongCuDungCu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueHoaDonCongCuDungCu.ItemClick
        If checkOpenTabs("Hóa đơn công cụ dụng cụ") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = "Hóa đơn công cụ dụng cụ"
        t.Image = mnuHoaDonGTGT_DauVao.Glyph
        t.Tag = mnuThueCongCuDungCu.Name
        Dim f As New frmHoaDonDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaCongCuDungCu
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

#End Region





    Private Sub mnuThueNhapXuatTon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueNhapXuatTon.ItemClick
        OpenTab("Bảng nhập xuất tồn kho thuế", "frmThue_BangNhapXuatTon", New frmThue_BangNhapXuatTon, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueCongCuDungCu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueCongCuDungCu.ItemClick
        OpenTab("Công cụ dụng cụ", "frmDsCongCuDungCu", New frmDsCongCuDungCu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuHeThongTaiKhoanThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuHeThongTaiKhoanThue.ItemClick
        OpenTab("Hệ thống tài khoản", "frmHeThongTaiKhoanThue", New frmHeThongTaiKhoanThue, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueBangKeHoaDonBanRa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueBangKeHoaDonBanRa.ItemClick
        OpenTab("Bảng kê hóa đơn bán ra", "BangKeHoaDonBanRa", New frmBangExeclInovice, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueBangKeHoaDonMuaVao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueBangKeHoaDonMuaVao.ItemClick
        OpenTab("Bảng kê hóa đơn mua vào", "BangKeHoaDonMuaVao", New frmBangExeclInovice, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaNhapKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaNhapKho.ItemClick
        OpenTab("Kết quả nhập kho", "frmKetQuaNhapKho", New frmKetQuaNhapKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChiPhi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChiPhi.ItemClick
        OpenTab("Chi phí", "frmChiPhi", New frmChiPhi, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThue_PhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_PhieuThu.ItemClick
        If checkOpenTabs("Phiếu thu") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThue_PhieuThu.Glyph
        t.Text = "Phiếu thu"
        t.Tag = mnuThue_PhieuThu.Name
        Dim f As New frmThuChiThue
        f.LoaiCT = ChungTu.LoaiChungTu.PhieuThuTienMat
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThue_PhieuChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_PhieuChi.ItemClick
        If checkOpenTabs("Phiếu chi") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThue_PhieuChi.Glyph
        t.Text = "Phiếu chi"
        t.Tag = mnuThue_PhieuChi.Name
        Dim f As New frmThuChiThue
        f.LoaiCT = ChungTu.LoaiChungTu.PhieuChiTienMat
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThue_GuiTien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_GuiTien.ItemClick
        If checkOpenTabs("Gửi tiền") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThue_GuiTien.Glyph
        t.Text = "Gửi tiền"
        t.Tag = mnuThue_GuiTien.Name
        Dim f As New frmThuChiThue
        f.LoaiCT = ChungTu.LoaiChungTu.NopTienNganHang
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThue_UyNhiemChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_UyNhiemChi.ItemClick
        If checkOpenTabs("Ủy nhiệm chi") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThue_UyNhiemChi.Glyph
        t.Text = "Ủy nhiệm chi"
        t.Tag = mnuThue_UyNhiemChi.Name
        Dim f As New frmThuChiThue
        f.LoaiCT = ChungTu.LoaiChungTu.UyNhiemChi
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThue_SoQuyTienMat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_SoQuyTienMat.ItemClick
        Dim f As New frmTieuChiBaoCaoThue
        f.Tag = "SoQuyTienMat"
        f.ShowDialog()
    End Sub

    Private Sub mnuThue_SoQuyTienGui_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_SoQuyTienGui.ItemClick
        Dim f As New frmBaoCaoSoQuyTienGui
        f.Tag = "SoQuyTienGui"
        f.ShowDialog()
    End Sub

    Private Sub mnuThue_DanhLaiSoCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_DanhLaiSoCT.ItemClick
        Dim f As New frmDanhLaiSoChungTu
        f.ShowDialog()
    End Sub

    Private Sub mnuThue_TinhGiaVon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_TinhGiaVon.ItemClick
        Dim f As New frmTinhGiaXuatKhoThue
        f.ShowDialog()
    End Sub

    Private Sub mnuThueMuaHangNuocNgoai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueMuaHangNuocNgoai.ItemClick
        If checkOpenTabs("Hóa đơn mua hàng nước ngoài") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThueMuaHangNuocNgoai.Glyph
        t.Text = "Hóa đơn mua hàng nước ngoài"
        t.Tag = mnuThueMuaHangNuocNgoai.Name
        Dim f As New frmHoaDonDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaHangNuocNgoai
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThue_TonDauKyCCDC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_TonDauKyCCDC.ItemClick
        OpenTab("Tồn kho thuế CCDC", "frmTonKhoThueCCDC", New frmTonKhoThueCCDC, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThue_GhiTangCCDC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_GhiTangCCDC.ItemClick
        OpenTab("Ghi tăng công cụ dụng cụ", "frmGhiTangCCDC", New frmGhiTangCCDC, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThue_PhanBoCCDC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_PhanBoCCDC.ItemClick
        OpenTab("Phân bổ công cụ dụng cụ", "frmPhanBoCCDC", New frmPhanBoCCDC, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueChungTuKhac_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueChungTuKhac.ItemClick
        OpenTab("Chứng từ nghiệp vụ khác", "frmChungTuThueKhac", New frmChungTuThueKhac, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueHoaDonTSCD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueHoaDonTSCD.ItemClick
        If checkOpenTabs("Hóa đơn mua tài sản cố định") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Image = mnuThueHoaDonTSCD.Glyph
        t.Text = "Hóa đơn mua tài sản cố định"
        t.Tag = mnuThueHoaDonTSCD.Name
        Dim f As New frmHoaDonDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaTaiSanCoDinh
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mnuThueDanhSachTSCD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueDanhSachTSCD.ItemClick
        OpenTab("Tài sản cố định", "frmDanhSachTSCD", New frmDanhSachTSCD, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueCongNoDauKy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueCongNoDauKy.ItemClick
        OpenTab("Công nợ thuế Phải Thu đầu kỳ", "CONGNOTHUEPHAITHUDAUKY", New frmCongNoThueDauKy, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueCongNoPhaiTraDauKy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueCongNoPhaiTraDauKy.ItemClick
        OpenTab("Công nợ thuế Phải Trả đầu kỳ", "CONGNOTHUEPHAITRADAUKY", New frmCongNoThueDauKy, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueTongHopCongNoPhaiThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueTongHopCongNoPhaiThu.ItemClick
        Dim f As New frmTieuChiBaoCaoCongNoThue(frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo.TongHopCongNoPhaiThu)
        f.ShowDialog()
    End Sub

    Private Sub mnuThueTongHopCongNoPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueTongHopCongNoPhaiTra.ItemClick
        Dim f As New frmTieuChiBaoCaoCongNoThue(frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo.TonngHopCongNoPhaiTra)
        f.ShowDialog()
    End Sub

    Private Sub mnuThueChiTietCongNoPhaiThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueChiTietCongNoPhaiThu.ItemClick
        Dim f As New frmTieuChiBaoCaoCongNoThue(frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo.ChiTietCongNoPhaiThu)
        f.ShowDialog()
    End Sub

    Private Sub mnuThueChiTietCongNoPhaiTra_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueChiTietCongNoPhaiTra.ItemClick
        Dim f As New frmTieuChiBaoCaoCongNoThue(frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo.ChiTietCongNoPhaiTra)
        f.ShowDialog()
    End Sub

    Private Sub mHangCanNhapKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangCanNhapKho.ItemClick
        OpenTab("Hàng cần nhập kho", "frmHangCanNhapKho", New frmHangCanNhapKho, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuDanhMucPhongBanThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDanhMucPhongBanThue.ItemClick
        OpenTab("Danh mục phòng ban thuế", "frmPhongBanThue", New frmPhongBanThue, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueTonDauKyTSCD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueTonDauKyTSCD.ItemClick
        OpenTab("Tồn đầu kỳ TSCĐ", "frmTonKhoThueTSCD", New frmTonKhoThueTSCD, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mNhapThueChoHangDaNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mNhapThueChoHangDaNhap.ItemClick
        OpenTab("Nhập thuế cho hàng đã nhập", "frmNhapThueHangDaNhap", New frmNhapThueHangDaNhap, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueThaoGhepVatTuBo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueThaoGhepVatTuBo.ItemClick
        'TrangThai.isAddNew = True
        'Dim f As New frmUpdateGhepVatTuBo
        'f.ShowDialog()
    End Sub

    Private Sub mTonKhoGiaNhap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTonKhoGiaVon.ItemClick
        OpenTab("Tồn kho giá vốn", "frmTonKhoGiaVon", New frmTonKhoGiaVon, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueNganHangDauKy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueNganHangDauKy.ItemClick
        OpenTab("Số dư ngân hàng đầu kỳ", "frmSoDuNganHangDauKy", New frmSoDuNganHangDauKy, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemSo1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        OpenTab("Điểm số (test)", "frmDiemSo1", New frmDiemSo1, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKQXKPT1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKQXKPT1.ItemClick
        OpenTab("Kết quả xuất kho, phiếu thu (test)", "frmKetQuaXuatKho1", New frmKetQuaXuatKho1, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKQChaoGia1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKQChaoGia1.ItemClick
        OpenTab("Kết quả chào giá (test)", "frmKetQuaChaoGia1", New frmKetQuaChaoGia1, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueKetChuyenLaiLo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueKetChuyenLaiLo.ItemClick
        OpenTab("Kết chuyển lãi lỗ", "frmKetChuyenLaiLo", New frmKetChuyenLaiLo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueChungTuChuaGhiSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueChungTuChuaGhiSo.ItemClick
        OpenTab("Chứng từ chưa ghi sổ", "frmChungTuThueChuaGhiSo", New frmChungTuThueChuaGhiSo, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueSoChiTietVatTuHangHoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueSoChiTietVatTuHangHoa.ItemClick
        Dim f As New frmBaoCaoThueChiTietVatTuHangHoa
        f.ShowDialog()
    End Sub


    Private Sub mnuThuePhieuNhapXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThuePhieuNhapXuatKho.ItemClick
        Dim f As New frmInPhieuNXkhoTheoLo
        f.Tag = "NHAPKHO"
        f.ShowDialog()
    End Sub


    Private Sub mnuThue_InPhieuChiTheoLo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_InPhieuChiTheoLo.ItemClick
        Dim f As New frmInPhieuThuChiThueTheoLo
        f.Tag = "PHIEUCHI"
        f.ShowDialog()
    End Sub



    Private Sub mnuThueSoCaiTaiKhoan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueSoCaiTaiKhoan.ItemClick

    End Sub


    Private Sub mnuThueSoNhatKyChung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueSoNhatKyChung.ItemClick

    End Sub


    Private Sub mnuThueHoaDonVanChuyen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueHoaDonVanChuyen.ItemClick
        If checkOpenTabs("Hóa đơn vận chuyển QT") = True Then Exit Sub
        Dim t As New DevExpress.XtraTab.XtraTabPage
        t.Text = "Hóa đơn vận chuyển QT"
        t.Tag = mnuThueHoaDonVanChuyen.Name
        t.Image = mnuThueHoaDonVanChuyen.Glyph
        Dim f As New frmHoaDonDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaDichVuVanChuyen
        f.Dock = DockStyle.Fill
        t.Controls.Add(f)
        f.Show()
        tabMain.TabPages.Add(t)
        On Error Resume Next
        tabMain.SelectedTabPageIndex = tabMain.TabPages.Count - 1
    End Sub

    Private Sub mHangYCXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangYCXuat.ItemClick
        OpenTab("Hàng yêu cầu xuất", "frmHangYCXuat", New frmHangYCXuat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnMucDichSd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnMucDichSd.ItemClick
        OpenTab("Mục đích sử dụng", "frmMucdich", New frmMucdich, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnDinhMucHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDinhMucHD.ItemClick
        OpenTab("Định mức hoạt động", "frmDinhmuc", New frmDinhmuc, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnNhienVatLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNhienVatLieu.ItemClick
        OpenTab("Nhiên vật liệu", "frmNhienvatlieu", New frmNhienvatlieu, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnBCHuHaiXe_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBCHuHaiXe.ItemClick
        OpenTab("Báo cáo hư hại xe", "frmBaocaoHuhaixe", New frmBaocaoHuhaixe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnTkeNSD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTkeNSD.ItemClick
        OpenTab("Thống kê người sử dụng xe", "frmThongkeNguoiSudung", New frmThongkeNguoiSudung, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnTKXeSD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTKXeSD.ItemClick
        OpenTab("Thống kê xe được sử dụng", "frmThongkeXeSudung", New frmThongkeXeSudung, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnDKMuonXe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDKMuonXe.ItemClick
        OpenTab("Nhận và trả xe", "frmNhanvaTraxe", New frmNhanvaTraxe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnLichMuonXe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLichMuonXe.ItemClick
        OpenTab("Lịch mượn xe", "frmLichMuonXe", New frmLichMuonXe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnLichSuMuonXe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLichSuMuonXe.ItemClick
        OpenTab("Lịch sử mượn xe", "frmLichSuMuonXe", New frmLichSuMuonXe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnThongTinBDXE_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThongTinBDXE.ItemClick
        OpenTab("Bảo dưỡng Xe", "frmBaoduongxe", New frmBaoduongxe, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnThongTinXe_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThongTinXe.ItemClick
        OpenTab("Thông tin Xe", "frmXe", New frmXe, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueTinhThueHangNhapKhau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueTinhThueHangNhapKhau.ItemClick

        Dim sql As String = ""
        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
        frmDoi.Width = 450
        frmDoi.Height = 75
        frmDoi.TopLevel = True
        frmDoi.TopMost = True
        frmDoi.MaximizeBox = False
        frmDoi.MinimizeBox = False
        Dim prc As New DevExpress.XtraEditors.ProgressBarControl
        prc.Properties.ShowTitle = True
        prc.Properties.Appearance.Font = New Font(Me.Font.Name, 10, FontStyle.Bold)
        prc.Properties.Appearance.ForeColor = Color.Red
        prc.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Broken
        prc.Dock = DockStyle.Fill
        frmDoi.Controls.Add(prc)

        Try
            frmDoi.Show()
            Application.DoEvents()
            ''xóa các chứng từ cũ hóa đơn mua hàng đi
            frmDoi.Text = "Xóa các bút toán thuế của chứng từ mua hàng nước ngoài"
            If ExecuteSQLDataTable("delete from chungtuchitiet where Id_CT in (select Id from chungtu where LoaiCT = 2 AND LoaiCT2 = 7) and buttoan <> 1") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Application.DoEvents()
            frmDoi.Text = "Đang lấy danh sách các bút toán hàng tiền liên quan"
            Dim dt As DataTable
            sql = "select b.Id_CT,b.ID,a.TyGia,a.Thue,b.ref,b.DienGiai,b.ThanhTien "
            sql &= "from CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "where a.LoaiCT = 2 and a.LoaiCT2 = 7 "
            dt = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
            prc.Properties.Maximum = dt.Rows.Count
            prc.Properties.Minimum = 0
            prc.EditValue = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                Application.DoEvents()
                prc.EditValue = i + 1
                'Thuế nhập khẩu
                frmDoi.Text = "Đang cập nhật bút toán thuế (" & (i + 1) & "/" & dt.Rows.Count & ") ..."
                Dim r As DataRow = dt.Rows(i)
                AddParameter("@Id_CT", r("Id_CT"))
                AddParameter("@ref", r("ref"))
                AddParameter("@DienGiai", r("DienGiai"))
                AddParameter("@ThanhTien", 0)
                AddParameter("@TaiKhoanNo", "1561")
                AddParameter("@TaiKhoanCo", "3333")
                AddParameter("@ButToan", ChungTu.LoaiButToan.ThueNK)
                AddParameter("@GiaTriKhac", 0)
                If doInsert("CHUNGTUCHITIET") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                'Thuế GTGT
                AddParameter("@Id_CT", r("Id_CT"))
                AddParameter("@ref", r("ref"))
                AddParameter("@DienGiai", r("DienGiai"))
                AddParameter("@ThanhTien", Math.Round((isNullThen0(r("ThanhTien")) * isNullThen1(r("Thue"))) / 100.0F, 2, MidpointRounding.AwayFromZero))
                AddParameter("@TaiKhoanNo", "1331")
                AddParameter("@TaiKhoanCo", "33312")
                AddParameter("@ButToan", ChungTu.LoaiButToan.ThueGTGT)
                AddParameter("@GiaTriKhac", 0)
                If doInsert("CHUNGTUCHITIET") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

        Catch ex As Exception
            'frmDoi.Close()
            ShowBaoLoi(ex.Message)
        End Try

        ShowThongBao("Đã cập nhật các bút toán thuế cho chứng từ mua hàng nước ngoài thành công!")
        'frmDoi.Close()

    End Sub

    Private Function isNullThen1(obj As Object) As Double
        If obj Is DBNull.Value Then Return 0
        Return obj
    End Function
    Private Function isNullThen0(obj As Object) As Double
        If obj Is DBNull.Value Then Return 0
        Return obj
    End Function


    Private Sub mnuGhepVatTuBo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGhepVatTuBo.ItemClick
        OpenTab("Ghép vật tư bộ", "GhepVatTuBo", New frmThaoGhepVatTuBo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueThaoVatTuBo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueThaoVatTuBo.ItemClick
        OpenTab("Tháo vật tư bộ", "ThaoVatTuBo", New frmThaoGhepVatTuBo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueGhiTangTSCD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueGhiTangTSCD.ItemClick
        OpenTab("Ghi tăng TSCĐ", "frmGhiTangTSCD", New frmGhiTangTSCD, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mnuThueSoDuTaiKhoanThueDauKy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueSoDuTaiKhoanThueDauKy.ItemClick
        OpenTab("Số dư tài khoản thuế đầu kỳ", "frmHeThongTaiKhoanThueDauKy", New frmHeThongTaiKhoanThueDauKy, True, Nothing, e.Item.Name)
    End Sub


    Private Sub TimerLoadCanhBao_Tick(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub mnuDoiChieuVatTuTon2Kho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDoiChieuVatTuTon2Kho.ItemClick
        OpenTab("Đối chiếu lượng tồn 2 kho", "frmThue_DoiChieu2Kho", New frmThue_DoiChieu2Kho, True, Nothing, e.Item.Name)
    End Sub


    Private Sub mHangDaXuatTest_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangDaXuatTest.ItemClick
        OpenTab("Hàng đã xuất (Test)", "frmHangDaXuatTest", New frmHangDaXuatTest, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHangDaChaoGiaTest_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHangDaChaoGiaTest.ItemClick
        OpenTab("Hàng đã chào giá (Test)", "frmVatTuDaChaoGiaTest", New frmVatTuDaChaoGiaTest, True, Nothing, e.Item.Name)
    End Sub

    'Thông tin tài sản:
    Private Sub btnThongTinTS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        OpenTab("Thông tin tài sản", "frmThongTinTaiSan", New frmThongTinTaiSan, True, Nothing, e.Item.Name)
    End Sub

    'Khấu hao tài sản
    Private Sub btnKhauHaoTS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKhauHaoTS.ItemClick
        OpenTab("Khấu hao tài sản", "frmKhauHaoTaiSan", New frmKhauHaoTaiSan, True, Nothing, e.Item.Name)
    End Sub

    'Tài sản hỏng
    Private Sub btnTShong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTShong.ItemClick
        OpenTab("Tài sản hỏng", "frmTaiSanHong", New frmTaiSanHong, True, Nothing, e.Item.Name)
    End Sub

    'Thông tin công cụ, dụng cụ

    Private Sub mDiemDanhGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDiemDanhGia.ItemClick
        OpenTab("Tổng hợp điểm đánh giá", "frmTHDiemDanhGia", New frmTHDiemDanhGia, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mKetQuaLamViecCuaKTTest_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        OpenTab("Kiểm quả làm việc của kỹ thuật (Test)", "frmKiemTraBaoCaoKTTest", New frmKiemTraBaoCaoKTTest, True, Nothing, e.Item.Name)
    End Sub
    Private Sub mTHThemGioThemCong_ItemClick_1(sender As Object, e As ItemClickEventArgs) Handles mTHThemGioThemCong.ItemClick
        OpenTab("Tổng hợp thêm giờ, thêm công của kỹ thuật", "frmTongHopThemGioThemCongKT", New frmTongHopThemGioThemCongKT, True, Nothing, e.Item.Name)
    End Sub

    'Hàng hóa xuất cho BAC
    Private Sub btnHangHoaXuatChoBA_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnHangHoaXuatChoBA.ItemClick
        OpenTab("Hàng hóa xuất cho Bảo An", "frmHangHoaXuatChoBac", New frmHangHoaXuatChoBac, True, Nothing, e.Item.Name)
    End Sub

    'Tài sản:
    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThongTinTS.ItemClick
        OpenTab("Thông tin tài sản", "frmThongTinTaiSan", New frmThongTinTaiSan, True, Nothing, e.Item.Name)
    End Sub

    'Công cụ, dụng cụ:
    Private Sub btnThongTinCCDC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThongTinCCDC.ItemClick
        OpenTab("Thông tin công cụ, dụng cụ", "frmThongTinCCDC", New frmThongTinCCDC, True, Nothing, e.Item.Name)
    End Sub
    'Chi phí chung:
    Private Sub btnThongTinCPC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThongTinCPC.ItemClick
        OpenTab("Thông tin chi phí chung", "frmThongTinCCDC", New frmThongTinChiPhiChung, True, Nothing, e.Item.Name)
    End Sub
    Private Sub mThuTienMat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuTienMat.ItemClick
        OpenTab("Thu tiền mặt", "frmThuTienMat", New frmThuTienMat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChiTienMat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChiTienMat.ItemClick
        OpenTab("Chi tiền mặt", "frmChiTienMat", New frmChiTienMat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mSoTienMat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSoTienMat.ItemClick
        OpenTab("Sổ tiền mặt", "frmSoTienMat", New frmSoTienMat, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mThuNganHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThuNganHang.ItemClick
        OpenTab("Thu ngân hàng", "frmThuNganHang", New frmThuNganHang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mUNC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mUNC.ItemClick
        OpenTab("Ủy nhiệm chi", "frmChiNganHang", New frmChiNganHang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mSoTienGui_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSoTienGui.ItemClick
        OpenTab("Sổ tiền gửi", "frmSoTienGui", New frmSoTienGui, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemSoTest2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDiemSoTest2.ItemClick
        OpenTab("Điểm số (test 2)", "frmDiemSo2", New frmDiemSo2, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnDSYCLamHQ_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDSYCLamHQ.ItemClick
        OpenTab("Danh sách yêu cầu làm hải quan", "frmDSYCLamHQ", New frmDSYCLamHQ, True, Nothing, e.Item.Name)
    End Sub

    Private Sub btnLamHaiQuan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLamHaiQuan.ItemClick
        OpenTab("Làm hải quan", "frmDSYCLamHQ", New frmDSYCLamHQ, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mBoPhanTrongSo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBoPhanTrongSo.ItemClick
        OpenTab("Bộ phận - trọng số", "frmBoPhanTrongSo", New frmBoPhanTrongSo, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mPhatTrienSanPham_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhatTrienSanPham.ItemClick
        OpenTab("Phát triển sản phẩm", "frmPhatTrienSanPham", New frmPhatTrienSanPham, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDanhGiaNhanVien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDanhGiaNhanVien.ItemClick
        OpenTab("Điểm đánh giá nhân viên", "frmDiemDanhGiaNhanVien", New frmDiemDanhGiaNhanVien, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mPhanBoLoiNhuan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhanBoLoiNhuan.ItemClick
        OpenTab("Phân bổ lợi nhuận", "frmPhanBoLoiNhuan", New frmPhanBoLoiNhuan, True, Nothing, e.Item.Name)
    End Sub

    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        If cbTieuChi.EditValue = "Thời gian" Then
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        Else
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = tbDenNgay.EditValue
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub mLNNhanVien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLNNhanVienHT.ItemClick
        OpenTab("Lợi nhuận nhân viên", "frmLoiNhuanNhanVien", New frmLoiNhuanNhanVienHT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mLoiNhuanNVPTSP_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLoiNhuanNVPTSP.ItemClick
        OpenTab("Lợi nhuận nhóm phát triển SP", "frmLoiNhuanNhanVienPT", New frmLoiNhuanNhanVienPT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChotSoLieu.ItemClick
        Dim f As New frmChotSoLieu
        f.ShowDialog()
    End Sub

    Private Sub mKetQuaXuatKhoCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetQuaXuatKhoCT.ItemClick
        OpenTab("Kết quả xuất kho công trình", "frmKetQuaXuatKhoCT", New frmKetQuaXuatKhoCT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopDiemKyNangTheoNhom_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTongHopDiemKyNang.ItemClick
        OpenTab("Tổng hợp điểm ký năng", "frmTongHopDiemKyNang", New frmTongHopDiemKyNang, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mLNPhanBoTheoXK_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLNPhanBoTheoXK.ItemClick
        OpenTab("Lợi nhuận phân bổ theo xuất kho", "frmLNPhanBoTheoXK", New frmLNPhanBoTheoXK, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTinhLNNhomHT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhanBoLNNhomHT.ItemClick
        OpenTab("Phân bổ lợi nhuận nhóm hậu trường", "frmPhanBoLoiNhuanNhomHT", New frmPhanBoLoiNhuanNhomHT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDiemSo3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDiemSo3.ItemClick
        OpenTab("Điểm số (test 3)", "frmDiemSo3", New frmDiemSo3, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mDinhMucDiemThuong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDinhMucTinhDiem.ItemClick
        OpenTab("Định mức tính điểm", "frmDinhMucTinhDiem", New frmDinhMucTinhDiem, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThue_KetQuaHoatDongKinhDoanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThue_KetQuaHoatDongKinhDoanh.ItemClick
        Dim f As New frmBaoCaoKetQuaHoatDongKinhDoanh
        f.ShowDialog()
    End Sub

    Private Sub mHSLNMuaHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHSLNMuaHang.ItemClick
        Dim f As New frmQuyDoi
        f.ShowDialog()
    End Sub

    Private Sub mKQChaoGiaCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKQChaoGiaCT.ItemClick
        OpenTab("Kết quả chào giá công trình", "frmKetQuaChaoGiaCT", New frmKetQuaChaoGiaCT, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mnuThueSoChiTietTaiKhoan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThueSoChiTietTaiKhoan.ItemClick
        OpenTab("Sổ chi tiết tài khoản thuế", "frmBaoCaoChiTietTaiKhoanThue", New frmBaoCaoChiTietTaiKhoanThue, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mHinhThucTT2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHinhThucTT2.ItemClick
        OpenTab("Hình thức thanh toán 2", "frmHinhThucTT2", New frmHinhThucTT2, True, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopCongNoTra_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mTongHopCongNoTra.ItemClick
        OpenTab("Tổng hợp công nợ phải trả", "TONGHOPCONGNOPHAITRA", New frmTongHopCongNoPhaiThuPhaiTraCu, False, Nothing, e.Item.Name)
    End Sub
    Private Sub mTongHopCongNoThu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mTongHopCongNoThu.ItemClick
        OpenTab("Tổng hợp công nợ phải thu", "TONGHOPCONGNOPHAITHU", New frmTongHopCongNoPhaiThuPhaiTraCu, False, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopCongNoTra2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mTongHopCongNoTra2.ItemClick
        OpenTab("Tổng hợp công nợ phải trả", "TONGHOPCONGNOPHAITRA", New frmTongHopCongNoPhaiTraMoi, False, Nothing, e.Item.Name)
    End Sub

    Private Sub mTongHopCongNoThu2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles mTongHopCongNoThu2.ItemClick
        OpenTab("Tổng hợp công nợ phải thu 2", "TONGHOPCONGNOPHAITHU", New frmTongHopCongNoPhaiThuPhaiTra, False, Nothing, e.Item.Name)
    End Sub

  
End Class