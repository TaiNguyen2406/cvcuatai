Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Xml
Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports BACSOFT.Utils
Imports DevExpress.XtraTreeList.Nodes

Public Class frmCNCongTrinhCanXuLy

    Public tmpTrangThai As New Utils.TrangThai
    Public tmpMaTuDien As Object
    Public SPYeuCau As Object
    Public SPDatHang As Object
    Private EndSelect As Boolean
    Private Move_Next As Boolean
    Public SPChaoGia As Object
    Private _exit As Boolean
    Public _MaKH As String
    Public ThoiGian As DateTime
    Public _FileCGKinhDoanh As String
    Public NVThamGia As New ArrayList
    Public TrangThaiCT As Boolean = False
    Public TrangThaiCG As Object
    Public objIDYC As Object
    Public _SPDaSaoChep As Object

    Private Sub frmCNCongTrinhCanXuLy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ShowWaiting("Đang tải nội dung công trình cần xử lý ...")

        tbTGThiCong.EditValue = DBNull.Value
        loadTienTe()
        loadDSDVT()
        loadDSMaKH()
        loadPhuTrachCT()
        loadDSLoaiCongTrinh()
        LoadCbTrangThai()
        loadDSThoiGianGH()
        loadDSNoiDungThiCong()
        LoadThongTinYC()
        loadGdvVatTuChaoGia()

        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        TinhTienDieuChinh()
        chkLocKhoBan.Checked = True
        Me.Text = "Xử lý công trình"
        ' CloseWaiting()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            'cbTrangThai.Properties.ReadOnly = True
            btNangCao.Enabled = False
            btXuatExcel.Enabled = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.KiemDuyet) Then
            cbTrangThai.Properties.ReadOnly = True
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            cbPhuTrachCT.Enabled = False
            luePhuTrachTC.Enabled = False
            gdvVTCT.Columns("TrangThai").OptionsColumn.ReadOnly = True
            mChuyenSangDaXN.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            cbPhuTrachCT.Enabled = True
            luePhuTrachTC.Enabled = True
        End If

        If TrangThaiCT = False Then
            btDuyet.Enabled = False
            'gdvChiPhiKhacCT.OptionsBehavior.Editable = False
        End If

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKyThuat) Then
            btnCapNhatTTchung.Enabled = True
        Else
            btnCapNhatTTchung.Enabled = False
        End If

    End Sub

    Private Sub frmCNCongTrinhCanXuLy_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '     CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmCongTrinhCanXuLy).LoadDS()

        For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
            If deskTop.tabMain.TabPages(i).Tag = deskTop.mCongTrinhCanXuLy.Name Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmCongTrinhCanXuLy).LoadDS()
            End If
        Next
    End Sub

#Region "Load dữ liệu cho các combobox"

    Public Sub loadPhuTrachCT()
        Dim sql As String = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        'If TrangThai.isAddNew Then
        '    sql &= " AND TrangThai=1"
        'End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbPhuTrachCT.Properties.DataSource = tb
            luePhuTrachTC.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadDSDVT()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM TENDONVITINH")
        If Not tb Is Nothing Then
            rcbDVTChiPhi.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadDSMaKH()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KhachHang ORDER BY ttcMa")
        If Not tb Is Nothing Then
            rcbMaKH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSLoaiCongTrinh()
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungCongViec)
        AddParameterWhere("@Loai2", LoaiTuDien.NhomCongViec)
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT tblTuDien.ID,tblTuDien.NoiDung,tbTmp.NoiDung AS Nhom FROM tblTuDien LEFT JOIN tblTuDien as tbTmp ON tblTuDien.IDP=tbTmp.ID and tbTmp.Loai=@loai2 WHERE tblTuDien.Loai=@Loai ORDER BY tbTmp.Ma,tblTuDien.Ma ")
        If Not tb Is Nothing Then
            cbLoaiYeuCau.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadThongTinYC()

        AddParameterWhere("@SoYeuCau", SPYeuCau)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,IDLoaiYeuCau FROM BANGYEUCAU WHERE Sophieu=@SoYeuCau")
        If Not tb Is Nothing And tb.Rows.Count > 0 Then
            cbLoaiYeuCau.EditValue = tb.Rows(0)("IDLoaiYeuCau")
            objIDYC = tb.Rows(0)("ID")
        End If
    End Sub

    Public Sub loadDSNoiDungThiCong()
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        AddParameterWhere("@Loai2", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT tblTuDien.ID,tblTuDien.NoiDung,tbTmp.NoiDung AS Nhom FROM tblTuDien LEFT JOIN tblTuDien as tbTmp ON tblTuDien.IDP=tbTmp.ID and tbTmp.Loai=@loai2 WHERE tblTuDien.Loai=@Loai ORDER BY tbTmp.Ma,tblTuDien.Ma ")
        If Not tb Is Nothing Then
            rgdvHangMucThiCong.DataSource = tb

            With rgdvHangMucThiCong.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Nội dung"
                colNoiDung.VisibleIndex = 1
                colNoiDung.Width = 120
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colNhom = .AddField("Nhom")
                colNhom.Caption = "Nhóm"
                colNhom.VisibleIndex = 2
                colNhom.Width = 250
                colNhom.GroupIndex = 0
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rgdvHangMucThiCong_Popup(sender As System.Object, e As System.EventArgs) Handles rgdvHangMucThiCong.Popup
        CType(sender, GridLookUpEdit).Properties.View.ExpandAllGroups()
    End Sub

    Private Sub LoadDSNVThiCong()
        Dim sql As String = ""
        sql &= " SELECT 'PB'+Convert(nvarchar, ID)ID,Ten FROM DEPATMENT "
        sql &= " SELECT ID,Ten,'PB'+Convert(nvarchar, IDDepatment)IDDepatment"
        sql &= " FROM NHANSU WHERE NHANSU.Noictac=74 AND NHANSU.Trangthai=1 ORDER BY IDDepatment,IDBoPhan,ChucVu"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        treeNV.Nodes.Clear()
        If Not ds Is Nothing Then

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim item As New Utils.ItemObject(ds.Tables(0).Rows(i)("ID"), ds.Tables(0).Rows(i)("Ten"))
                Dim nodeCha As TreeListNode = Nothing
                Dim node As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
                If NVThamGia.Contains(item.Value) Then node.Checked = True
                'If deskTop.BarMenu.ItemLinks.Item(i).ToString = "DevExpress.XtraBars.BarSubItemLink" Then
                For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(j)("IDDepatment") = ds.Tables(0).Rows(i)("ID") Then
                        Dim item2 As New Utils.ItemObject(ds.Tables(1).Rows(j)("ID"), ds.Tables(1).Rows(j)("Ten"))
                        Dim nodecon As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item2.Value, item2.Name)}, node)
                        If NVThamGia.Contains(item2.Value.ToString) Then nodecon.Checked = True
                    End If
                Next
                'End If
            Next
            treeNV.ExpandAll()
        End If

    End Sub

    Private Sub rPopUpDSThiCong_Popup(sender As System.Object, e As System.EventArgs) Handles rPopUpDSThiCong.Popup
        NVThamGia.Clear()
        If CType(sender, PopupContainerEdit).EditValue.ToString <> "" Then
            NVThamGia.AddRange(CType(sender, PopupContainerEdit).EditValue.ToString.Split(","))
        End If
        LoadDSNVThiCong()
    End Sub

    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            rcbTienTe.DataSource = tb
            rcbTienTeGoc.DataSource = tb
            cbTienTe.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadCbTrangThai()
        AddParameter("@Loai", LoaiTuDien.TrangThaiChaoGia)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            rcbTrangThaiChiTiet.DataSource = tb
            rcbTrangThaiCG.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadDSTenVT(ByVal HangSX As Object, ByVal NhomVT As Object)
        Dim sqltb As String = ""

        Dim sql As String = ""
        If HangSX Is Nothing And NhomVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTenvattu FROM TENVATTU WHERE ID=-1 "
            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb) "
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbTenVatTu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhomVT(ByVal HangSX As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If HangSX Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTennhom FROM TENNHOM WHERE ID=-1"

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT  IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb) "
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbNhomVT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbHangSX(ByVal NhomVT As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If NhomVT Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Else
            sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb) "
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSVatTuDungChuyenMa()
        Try
            ShowWaiting("Đang tải vật tư, hàng hóa ...")
            Dim sqlWhere As String = " WHERE Maloi=0 "

            Dim sql As String = "Select NULL AS CanhBao,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT, "
            sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS slTon, "

            'sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = VATTU.ID AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = xuatkhotam.SoCG),'')),0) - "
            'sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = VATTU.ID AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = nhapkhotam.SoCG),'')),0) "
            'sql &= " as XuatTam, "

            sql &= "  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu =  VATTU.ID),0)  "
            sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu =  VATTU.ID),0) "
            sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu =  VATTU.ID AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu =  VATTU.ID and SlXuatKho > 0)),0) "
            sql &= " as XuatTam,"

            sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
            sql &= " Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
            sql &= " Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
            sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
            sql &= " VATTU.Xuatthue1 AS Xuatthue,VATTU.Mucthue1 AS Mucthue, "
            sql &= " VATTU.Gianhap1 AS Gianhap, tblTienTe.Ten AS TenTienTe,tblTienTe.TyGia,VATTU.Tiente1 AS TienTe,VATTU.ConSX, "
            sql &= " TENNUOC.Ten AS Xuatxu, VATTU.TaiLieu,convert(float,0) AS SLYC, VATTU.HangTon,(convert(image,NULL))HienThi,VATTU.HinhAnh, VATTU.ThongDung, TENNHOM.Ten_ENG AS TenNhom_ENG "
            sql &= " FROM VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
            sql &= " LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
            sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
            sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "

            If Not cbMaKH.EditValue Is Nothing Then
                sql &= " INNER JOIN (SELECT XUATKHO.ID,XUATKHO.IDVattu,PHIEUXUATKHO.IDKhachhang FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu)tbXK ON VATTU.ID=tbXK.IDVattu AND tbXK.IDKhachhang = " & cbMaKH.EditValue
            End If

            If Not btFilterMaVT.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sqlWhere &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%'"
            End If

            sql &= sqlWhere

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                If chkTaiAnh.Checked Then
                    With tb
                        For i As Integer = 0 To .Rows.Count - 1
                            Application.DoEvents()
                            If .Rows(i)("HinhAnh").ToString <> "" Then
                                Try
                                    .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\thumb\" & .Rows(i)("HinhAnh"))
                                Catch ex As Exception

                                End Try
                            End If
                        Next
                    End With
                End If

                gdvChuyenMa.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        Catch ex As Exception
        Finally
            CloseWaiting()
        End Try


    End Sub

    Private TrangThaiDaXuLy As Integer = 0

    Public Sub loadGdvVatTuChaoGia()
        Dim sql As String = " SET DATEFORMAT DMY"
        sql &= " SELECT Tiente,TyGia,TenDuAn,XuLy,TienTruocThue,TienChietKhau,FileDinhKem,NgayThang,TienDo,TGThiCong,NhanKS,IDPhuTrachCT, IDPhuTrachTC"
        sql &= " FROM BANGCHAOGIA"
        sql &= " WHERE Sophieu=@SP"

        sql &= " DECLARE @tb table"
        sql &= " ("
        sql &= " 	IDCG int,"
        sql &= " 	IDVatTu int,"
        sql &= " 	TenVT NVARCHAR(4000),"
        sql &= " 	TenHang NVARCHAR(50),"
        sql &= " 	Model NVARCHAR(50),"
        sql &= " 	ThongSo	NVARCHAR(1000),"
        sql &= " 	TenDVT NVARCHAR(50),"
        sql &= " 	SoLuong Float,"
        sql &= " 	SoLuongGoc Float,"
        sql &= " 	DuToan Float,"
        sql &= " 	IDYeuCau int,"
        sql &= " 	ThanhTien Float,"
        sql &= " 	GiaList	Float,"
        sql &= " 	GiaBanBuon float,"
        sql &= " 	GiaBanLe float,"
        sql &= " 	XuatThue bit,"
        sql &= " 	MucThue tinyint,"
        sql &= " 	GiaNhap float,"
        sql &= " 	IDTGGiaoHang int,"
        sql &= " 	slTon float,"
        sql &= " 	XuatTam float,"
        sql &= " 	DangVe float,"
        sql &= " 	NgayVe Datetime,"
        sql &= " 	CanXuat float,"
        sql &= " 	TrangThai tinyint,"
        sql &= " 	DonGia float,"
        sql &= " 	DonGiaGoc float,"
        sql &= " 	GiaBanPT float,"
        sql &= " 	ChietKhau float,"
        sql &= " 	ChietKhauPT float,"
        sql &= " 	TienTe tinyint,"
        sql &= " 	TyGia float,"
        sql &= " 	HangTon bit,"
        sql &= " 	AZ int,"
        sql &= "    NguoiNhap nvarchar(250) "
        sql &= " )"
        sql &= " INSERT INTO @tb(IDCG,IDVatTu,TenVT,TenHang,Model,ThongSo,TenDVT,SoLuong,SoLuongGoc,DuToan,IDYeuCau,ThanhTien,GiaList,GiaBanBuon,GiaBanLe,XuatThue,MucThue,GiaNhap,IDTGGiaoHang,slTon,XuatTam,DangVe,NgayVe,CanXuat,TrangThai,DonGia,GiaBanPT,DonGiaGoc,ChietKhau,ChietKhauPT,TienTe,TyGia,HangTon,AZ,NguoiNhap)"
        sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,SoLuongGoc,DuToan,CHAOGIA.IDYeuCau,"
        sql &= " (CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        sql &= " CHAOGIA.XuatThue,CHAOGIA.MucThue,VATTU.GiaNhap1 AS GiaNhap,IDTGGiaoHang,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"

        'sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = xuatkhotam.SoCG),'')),0) - "
        'sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = nhapkhotam.SoCG),'')),0) "
        'sql &= " as XuatTam, "

        'sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG = '" & SPChaoGia & "'),0) - "
        'sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG = '" & SPChaoGia & "'),0) "
        'sql &= " as XuatTam, "

        sql &= "  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) "
        sql &= " as XuatTam,"

        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS CanXuat,CHAOGIA.TrangThai,CHAOGIA.DonGia,(0.0)GiaBanPT,"
        sql &= " ISNULL(ISNULL("
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia) AS DonGiaGoc,"
        sql &= " CHAOGIA.ChietKhau,Convert(float,0) AS ChietKhauPT,"
        sql &= " ISNULL(VATTU.TienTe1,0)TienTe,ISNULL(ISNULL(CHAOGIA.TyGia,tblTienTe.TyGia),1)TyGia, VATTU.HangTon, ISNULL(CHAOGIA.AZ,0)AZ,"
        sql &= " (SELECT Ten FROM NHANSU WHERE ID = CHAOGIA.IdNguoiLap)NguoiNhap "
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " INNER JOIN BANGCHAOGIA ON (BANGCHAOGIA.SoPhieu+'CT')=CHAOGIA.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " WHERE CHAOGIA.Sophieu=N'" & SPChaoGia & "CT'"
        sql &= " ORDER BY AZ "


        sql &= " INSERT INTO @tb(IDCG,TenVT,TenHang,TenDVT,SoLuong,DonGia,MucThue,XuatThue,ChietKhau,ChietKhauPT,ThanhTien,TienTe,TyGia,AZ)"
        sql &= " SELECT CHAOGIAAUX.ID AS IDCGAUX,Noidung AS NoiDung,HangSx AS HangSX, Donvi,SoLuong, DonGia, "
        sql &= " MucThue,XuatThue,Chietkhau,Convert(float,0) AS ChietKhauPT,"
        sql &= " (Dongia*Soluong)ThanhTien,TienTe,ISNULL(CHAOGIAAUX.TyGia,tblTienTe.TyGia)TyGia, ISNULL(CHAOGIAAUX.AZ,0)AZ "
        sql &= " FROM CHAOGIAAUX LEFT OUTER JOIN tblTienTe ON CHAOGIAAUX.Tiente=tblTienTe.ID "
        sql &= " WHERE Sophieu=N'" & SPChaoGia & "CT'"
        sql &= " ORDER BY AZ "
        sql &= " SELECT * FROm @tb"

        sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,Soluong AS SoLuong,CHAOGIA.SoLuongGoc,CHAOGIA.DuToan,CHAOGIA.IDYeucau AS IDYeuCau,CHAOGIA.NgayCan,"
        sql &= " (CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,"
        sql &= " ISNULL(ISNULL("
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia) AS DonGiaGoc,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        sql &= " (SELECT 0.0)TTGiaNhap,"
        sql &= " CHAOGIA.Xuatthue AS XuatThue,CHAOGIA.Mucthue AS MucThue,VATTU.Gianhap1 AS GiaNhap,IDTGGiaoHang,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"

        'sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = xuatkhotam.SoCG),'')),0) - "
        'sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = nhapkhotam.SoCG),'')),0) "
        'sql &= " as XuatTam, "


        sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG = '" & SPChaoGia & "'),0) - "
        sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG = '" & SPChaoGia & "'),0) "
        sql &= "  as XuatTam, "


        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS CanXuat,CHAOGIA.TrangThai, CHAOGIA.TrangThai as TrangThai2,CHAOGIA.Dongia AS DonGia,(0.0)GiaBanPT,"
        sql &= " CHAOGIA.Chietkhau AS ChietKhau,(0.0)ChietKhauPT, "
        sql &= " ISNULL(VATTU.Tiente1,0) AS TienTe,ISNULL(ISNULL(CHAOGIA.TyGia,tblTienTe.TyGia),1)TyGia, VATTU.HangTon , ISNULL(CHAOGIA.AZ,0)AZ, (SELECT 0) LoiGia, "
        sql &= " (SELECT Ten FROM NHANSU WHERE ID = CHAOGIA.IdNguoiLap)NguoiNhap "
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=CHAOGIA.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " WHERE CHAOGIA.Sophieu=@SP"
        sql &= " ORDER BY CHAOGIA.Id asc, AZ "

        sql &= " SELECT ID AS IDCGAUX,(0)AZ,SoPhieu,NoiDung, Donvi AS DVT,SoLuong, DonGia,HangSX, ChietKhau,(0.0)ChietKhauPT, IDNoiDungThiCong, "
        sql &= " (Soluong*Dongia)ThanhTien,MucThue,XuatThue"
        sql &= " FROM CHAOGIAAUX"
        sql &= " WHERE Sophieu=@SP"
        sql &= " ORDER BY AZ "

        sql &= " SELECT ('')NgThucHien,('')NgThongBao,('')NVKiemDuyetLan1,('')NVKiemDuyetLan2,ID,NgayBatDau,NgayKetThuc,SoYC,IDNoiDung,MoTa,IDNgThucHien,IDNgThongBao,IDNgNhap,NgayNhap,IDNgDuyet,Duyet,NgayDuyet,IDNgKiemDuyet1,IDNgKiemDuyet2,GiaoViec,AZ FROM tblBaoCaoLichThiCong "
        sql &= " WHERE GiaoViec=1 AND SoYC=@SoYC"
        sql &= " ORDER BY AZ "


        AddParameter("@SP", SPChaoGia)
        AddParameter("@SoYC", SPYeuCau)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbTienTe.EditValue = ds.Tables(0).Rows(0)("TienTe")
            tbTyGia.EditValue = ds.Tables(0).Rows(0)("TyGia")
            With ds.Tables(1)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1

                    Dim _GiaBanPT As Double = 0
                    If Not .Rows(i)("GiaList") Is Nothing And Not IsDBNull(.Rows(i)("GiaList")) Then
                        If Convert.ToByte(.Rows(i)("TienTe")) > Convert.ToByte(cbTienTe.EditValue) Then
                            _GiaBanPT = .Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)
                        Else
                            _GiaBanPT = .Rows(i)("DonGia") / .Rows(i)("GiaList")
                        End If
                        .Rows(i)("GiaBanPT") = Math.Round(_GiaBanPT * 100, 2)
                    End If
                    If .Rows(i)("DonGia") > 0 Then
                        .Rows(i)("ChietKhauPT") = (.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100
                    End If
                Next
            End With

            With ds.Tables(2)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1

                    Dim _GiaBanPT As Double = 0
                    If Not .Rows(i)("GiaList") Is Nothing And Not IsDBNull(.Rows(i)("GiaList")) Then
                        If .Rows(i)("GiaList") <> 0 Then
                            If Convert.ToByte(.Rows(i)("TienTe")) > Convert.ToByte(cbTienTe.EditValue) Then
                                _GiaBanPT = .Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)
                            Else
                                _GiaBanPT = .Rows(i)("DonGia") / .Rows(i)("GiaList")
                            End If
                        End If
                        .Rows(i)("GiaBanPT") = Math.Round(_GiaBanPT * 100, 2)
                    End If

                    If .Rows(i)("DonGia") > 0 Then
                        .Rows(i)("ChietKhauPT") = (.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100
                    Else
                        .Rows(i)("ChietKhauPT") = 0
                    End If

                    If IsDBNull(.Rows(i)("DonGiaGoc")) Then .Rows(i)("DonGiaGoc") = 0
                    If .Rows(i)("DonGiaGoc") = 0 Then
                        .Rows(i)("DonGiaGoc") = (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue
                    End If
                    .Rows(i)("TTGiaNhap") = .Rows(i)("SoLuong") * .Rows(i)("DonGiaGoc")
                    If .Rows(i)("DonGia") < .Rows(i)("DonGiaGoc") Then .Rows(i)("LoiGia") = 1

                Next
            End With

            With ds.Tables(3)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If .Rows(i)("DonGia") > 0 Then
                        .Rows(i)("ChietKhauPT") = (.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100
                    End If
                Next
            End With

            gdvThamChieu.DataSource = ds.Tables(1)
            gdvVT.DataSource = ds.Tables(2)
            gdvChiPhiKhac.DataSource = ds.Tables(3)
            ThoiGian = ds.Tables(0).Rows(0)("NgayThang")
            tbTenCongTrinh.EditValue = ds.Tables(0).Rows(0)("TenDuAn")
            cbTrangThai.SelectedIndex = Convert.ToInt16(ds.Tables(0).Rows(0)("XuLy"))
            TrangThaiDaXuLy = Convert.ToInt16(ds.Tables(0).Rows(0)("XuLy"))
            tbTongTienCG.EditValue = ds.Tables(0).Rows(0)("TienTruocThue")
            tbChietKhauCG.EditValue = ds.Tables(0).Rows(0)("TienChietKhau")
            tbTienDo.EditValue = ds.Tables(0).Rows(0)("TienDo")
            tbTGThiCong.EditValue = ds.Tables(0).Rows(0)("TGThiCong")
            If IsDBNull(ds.Tables(0).Rows(0)("NhanKS")) Then
                cbNhanKS.SelectedIndex = -1
            Else
                cbNhanKS.SelectedIndex = CType(ds.Tables(0).Rows(0)("NhanKS"), Integer)
            End If
            cbPhuTrachCT.EditValue = ds.Tables(0).Rows(0)("IDPhuTrachCT")
            luePhuTrachTC.EditValue = ds.Tables(0).Rows(0)("IDPhuTrachTC")
            gdvListFile.DataSource = DataSourceDSFile()
            With ds.Tables(4)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    Dim tb2 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThucHien").ToString, , ",")
                    .Rows(i)("NgThucHien") = ""
                    For j As Integer = 0 To tb2.Rows.Count - 1
                        AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                        Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb3 Is Nothing Then
                            .Rows(i)("NgThucHien") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NgThucHien") = .Rows(i)("NgThucHien").ToString.Trim

                    Dim tb9 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThongBao").ToString, , ",")
                    .Rows(i)("NgThongBao") = ""
                    For j As Integer = 0 To tb9.Rows.Count - 1
                        AddParameterWhere("@ID", tb9.Rows(j)("File").ToString)
                        Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb3 Is Nothing Then
                            .Rows(i)("NgThongBao") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NgThongBao") = .Rows(i)("NgThongBao").ToString.Trim

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

            gdvThiCong.DataSource = ds.Tables(4)
            _FileCGKinhDoanh = ""
            If Not ds.Tables(0).Rows(0)("FileDinhKem").ToString Is Nothing Then
                Dim listUrl() As String = ds.Tables(0).Rows(0)("FileDinhKem").ToString.Split(New Char() {";"c})
                For Each _url In listUrl
                    If _url.Trim = "" Then Continue For
                    If _url.Substring(0, 23) = "YC" & SPDatHang & " CG" & SPChaoGia & " KT " Then
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", _url)
                    Else
                        _FileCGKinhDoanh &= _url & ";"
                    End If
                Next
                gdvListFileCT.CloseEditor()
                gdvListFileCT.UpdateCurrentRow()
            End If


        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadDSThoiGianGH()
        AddParameter("@Loai", LoaiTuDien.TGCungUng)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            cbThoiGianGH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

    Public Function KiemTraTrung(ByVal MaVT As Object) As Boolean
        Dim count As Integer = 0
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "Model") = MaVT Then
                count += 1
                If count > 1 Then
                    gdvVTCT.SetRowCellValue(i, "LoiGia", 1)
                    For j As Integer = 0 To i - 1
                        If gdvVTCT.GetRowCellValue(j, "Model") = MaVT Then
                            gdvVTCT.SetRowCellValue(j, "LoiGia", 1)
                        End If
                    Next
                End If


                ' gdvVTCT_RowCellStyle(New Object, New DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs(i, colGLoiGia, DevExpress.XtraGrid.Views.Base.GridRowCellState.Default, New DevExpress.Utils.AppearanceObject))
            End If
        Next
        If count > 1 Then
            Return True
        Else
            Return False
        End If
    End Function

#Region "Lưu lại"
    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()

        'If TrangThaiDaXuLy = 1 Then
        '    ShowCanhBao("Không thể cập nhật nội dung khi đã xác nhận trạng thái công trình ! ")
        '    Exit Sub
        'End If

        Dim _MaTrung As String = "Mã trùng: "
        ' Kiểm tra trùng mã khi xuất kho
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If KiemTraTrung(gdvVTCT.GetRowCellValue(i, "Model")) Then
                _MaTrung &= vbCrLf & " - " & gdvVTCT.GetRowCellValue(i, "Model")
            End If
        Next

        If _MaTrung <> "Mã trùng: " Then
            If Not ShowCauHoi(_MaTrung & vbCrLf & " bạn có muốn tiếp tục không ?") Then
                Exit Sub
            End If
        End If

        btCal_Click(New Object, New EventArgs)
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        gdvThiCongCT.CloseEditor()
        gdvThiCongCT.UpdateCurrentRow()
        gdvChiPhiKhacCT.CloseEditor()
        gdvChiPhiKhacCT.UpdateCurrentRow()
        Try
            Dim NgayXuLy As DateTime = GetServerTime()
            BeginTransaction()
            AddParameter("@XuLy", cbTrangThai.SelectedIndex)
            AddParameter("@IDNgXuLy", TaiKhoan)
            AddParameter("@NgayXuLy", NgayXuLy)
            AddParameter("@TienDo", tbTienDo.EditValue)
            AddParameter("@TGThiCong", tbTGThiCong.EditValue)
            AddParameter("@FileDinhKem", _FileCGKinhDoanh & StrDSFile(gdvListFileCT))
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKyThuat) Then
                AddParameter("@IDPhuTrachCT", cbPhuTrachCT.EditValue)
                AddParameter("@IDPhuTrachTC", luePhuTrachTC.EditValue)
            End If

            If cbNhanKS.SelectedIndex = -1 Then
                AddParameter("@NhanKS", DBNull.Value)
            Else
                AddParameter("@NhanKS", cbNhanKS.SelectedIndex)
            End If

            AddParameterWhere("@SP", SPChaoGia)
            If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            With gdvVTCT
                For i As Integer = 0 To .RowCount - 1
                    AddParameter("@SoPhieu", SPChaoGia)
                    AddParameter("@IDvattu", .GetRowCellValue(i, "IDVatTu"))
                    AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                    AddParameter("@DuToan", .GetRowCellValue(i, "DuToan"))
                    AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                    AddParameter("@TyGia", .GetRowCellValue(i, "TyGia"))
                    AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                    AddParameter("@Xuatthue", .GetRowCellValue(i, "XuatThue"))
                    AddParameter("@Chietkhau", .GetRowCellValue(i, "ChietKhau"))
                    AddParameter("@Trangthai", .GetRowCellValue(i, "TrangThai"))
                    AddParameter("@IDYeucau", .GetRowCellValue(i, "IDYeuCau"))
                    AddParameter("@NgayCan", .GetRowCellValue(i, "NgayCan"))
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))

                    If IsDBNull(.GetRowCellValue(i, "IDCG")) Or .GetRowCellValue(i, "IDCG") Is Nothing Then
                        AddParameter("@IdNguoiLap", TaiKhoan)
                        Dim _IDCG As Object = doInsert("CHAOGIA")
                        If _IDCG Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDCG", _IDCG)
                        .SetRowCellValue(i, "NguoiNhap", NguoiDung)
                    Else
                        AddParameterWhere("@IDCG", .GetRowCellValue(i, "IDCG"))
                        If doUpdate("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If


                Next
            End With
            With gdvChiPhiKhacCT
                For i As Integer = 0 To .RowCount - 2
                    AddParameter("@Sophieu", SPChaoGia)
                    AddParameter("@Noidung", .GetRowCellValue(i, "NoiDung"))
                    AddParameter("@Donvi", .GetRowCellValue(i, "DVT"))
                    AddParameter("@Soluong", .GetRowCellValue(i, "SoLuong"))
                    AddParameter("@Dongia", .GetRowCellValue(i, "DonGia"))
                    AddParameter("@Mucthue", .GetRowCellValue(i, "MucThue"))
                    AddParameter("@Xuatthue", .GetRowCellValue(i, "XuatThue"))
                    AddParameter("@Chietkhau", .GetRowCellValue(i, "ChietKhau"))
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                    If IsDBNull(.GetRowCellValue(i, "IDCGAUX")) Or .GetRowCellValue(i, "IDCGAUX") Is Nothing Then
                        Dim _IDCGAUX As Object = doInsert("CHAOGIAAUX")
                        If _IDCGAUX Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDCGAUX", _IDCGAUX)
                    Else
                        AddParameterWhere("@IDCGAUX", .GetRowCellValue(i, "IDCGAUX"))
                        If doUpdate("CHAOGIAAUX", "ID=@IDCGAUX") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next
            End With

            With gdvThiCongCT
                For i As Integer = 0 To .RowCount - 2
                    AddParameter("@SoYC", SPYeuCau)
                    AddParameter("@IDNoiDung", .GetRowCellValue(i, "IDNoiDung"))
                    AddParameter("@NgayBatDau", .GetRowCellValue(i, "NgayBatDau"))
                    AddParameter("@NgayKetThuc", .GetRowCellValue(i, "NgayKetThuc"))
                    AddParameter("@IDNgThucHien", .GetRowCellValue(i, "IDNgThucHien"))
                    AddParameter("@IDNgThongBao", .GetRowCellValue(i, "IDNgThongBao"))
                    AddParameter("@IDNgKiemDuyet1", .GetRowCellValue(i, "IDNgKiemDuyet1"))
                    AddParameter("@IDNgKiemDuyet2", .GetRowCellValue(i, "IDNgKiemDuyet2"))
                    AddParameter("@GiaoViec", 1)
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                    If IsDBNull(.GetRowCellValue(i, "ID")) Or .GetRowCellValue(i, "ID") Is Nothing Then
                        AddParameter("@IDNgNhap", TaiKhoan)
                        AddParameter("@NgayNhap", NgayXuLy)
                        Dim _ID As Object = doInsert("tblBaoCaoLichThiCong")
                        If _ID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "ID", _ID)
                    Else
                        AddParameterWhere("@ID", .GetRowCellValue(i, "ID"))
                        If doUpdate("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next
            End With
            ComitTransaction()

            AddParameter("@IDLoaiYeuCau", cbLoaiYeuCau.EditValue)
            AddParameterWhere("@ID", objIDYC)
            If doUpdate("BANGYEUCAU", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If

            Dim sqlUpdate As String = "UPDATE CHAOGIAAUX SET Tiente = (SELECT DISTINCT BANGCHAOGIA.Tiente FROM BANGCHAOGIA WHERE BANGCHAOGIA.Sophieu=N'" & SPChaoGia & "') WHERE CHAOGIAAUX.Sophieu=N'" & SPChaoGia & "'"
            sqlUpdate &= " UPDATE CHAOGIA SET Tiente = (SELECT BANGCHAOGIA.Tiente FROM BANGCHAOGIA WHERE BANGCHAOGIA.Sophieu=N'" & SPChaoGia & "') WHERE CHAOGIA.Sophieu=N'" & SPChaoGia & "'"
            If ExecuteSQLNonQuery(sqlUpdate) Is Nothing Then
                ShowCanhBao(LoiNgoaiLe)
            End If
            ' Update số lượng cần xuất
            For i As Integer = 0 To gdvVTCT.RowCount - 1
                Dim sql As String = ""
                If Not IsDBNull(gdvVTCT.GetRowCellValue(i, "IDCG")) Or Not gdvVTCT.GetRowCellValue(i, "IDCG") Is Nothing Then
                    If Convert.ToByte(gdvVTCT.GetRowCellValue(i, "TrangThai")) = TrangThaiChaoGia.DaXacNhan Then
                        sql = " Update CHAOGIA Set CanXuat = (Soluong - (select (isnull(sum(soluong),0)) from XUATKHO where IDChaogia = " & gdvVTCT.GetRowCellValue(i, "IDCG") & ")) Where ID = " & gdvVTCT.GetRowCellValue(i, "IDCG")
                    Else
                        sql = " Update CHAOGIA Set CanXuat = 0 Where ID = " & gdvVTCT.GetRowCellValue(i, "IDCG")
                    End If

                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        ShowBaoLoi(sql)
                    End If
                End If

            Next

            ShowAlert("Đã lưu lại !")
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

#End Region


#Region "Các sự kiện của grid vật tư chào giá gdvVTCT"

    Private Sub gdvVTCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvVTCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            Dim f As New frmTinhTrangVT
            f.Tag = Me.Tag
            f._IDVatTu = gdvVTCT.GetFocusedRowCellValue("IDVatTu")
            f._HienThongTinNX = False
            f.ShowDialog()
        ElseIf e.Control AndAlso e.KeyCode = Keys.Q Then
            splitChiTiet.Collapsed = False
        ElseIf e.Control AndAlso e.KeyCode = Keys.W Then
            splitChiTiet.Collapsed = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            If ShowCauHoi("Xoá dòng hiện tại ?") Then

                Dim sql As String = "select count(id) from xuatkhotam where SoCG = @SoCG and IdVatTu = @IdVatTu"
                AddParameter("@SoCG", SPChaoGia)
                AddParameter("@IdVatTu", gdvVTCT.GetFocusedRowCellValue("IDVatTu"))
                Dim dtX As DataTable = ExecuteSQLDataTable(sql)
                If dtX Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
                If dtX.Rows(0)(0) > 0 Then
                    ShowCanhBao("Vật tư này đã xuất kho tạm " & dtX.Rows(0)(0) & " lần nên không thể xóa!")
                    Exit Sub
                End If

                Try

                    BeginTransaction()

                    If Not gdvVTCT.GetFocusedRowCellValue("IDCG") Is Nothing Then
                        AddParameterWhere("@IDCG", gdvVTCT.GetFocusedRowCellValue("IDCG"))
                        If doDelete("CHAOGIA", "ID=@IDCG") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                    ComitTransaction()

                    CType(gdvVT.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvVTCT.GetFocusedDataSourceRowIndex)

                Catch ex As Exception
                    RollBackTransaction()
                    ShowCanhBao(ex.Message)
                End Try

            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvVTCT.FocusedRowHandle = 0 Then Exit Sub
            Dim index = gdvVTCT.FocusedRowHandle

            Dim _tmp As Object = gdvVTCT.GetFocusedRowCellValue("AZ")
            gdvVTCT.SetFocusedRowCellValue("AZ", gdvVTCT.GetRowCellValue(gdvVTCT.FocusedRowHandle - 1, "AZ"))

            gdvVTCT.SetRowCellValue(gdvVTCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvVTCT.FocusedRowHandle += 1

        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvVTCT.FocusedRowHandle = gdvVTCT.RowCount - 1 Then Exit Sub
            Dim index = gdvVTCT.FocusedRowHandle
            Dim _tmp As Object = gdvVTCT.GetFocusedRowCellValue("AZ")
            gdvVTCT.SetFocusedRowCellValue("AZ", gdvVTCT.GetRowCellValue(gdvVTCT.FocusedRowHandle + 1, "AZ"))
            gdvVTCT.SetRowCellValue(gdvVTCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvVTCT.FocusedRowHandle -= 1

        End If
    End Sub

    Private Sub gdvVTCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvVTCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "ChietKhau", "ChietKhauPT"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
            Case "SoLuong", "DonGia"
                If Not IsNumeric(e.Value) Then
                    gdvVTCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                gdvVTCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvVTCT.GetRowCellValue(e.RowHandle, "DonGia") * gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong"), 2))

                gdvVTCT.SetRowCellValue(e.RowHandle, "TTGiaNhap", Math.Round(gdvVTCT.GetRowCellValue(e.RowHandle, "DonGiaGoc") * gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong"), 2))
            Case "ThanhTien"
                If _exit Then Exit Select
                TinhTienDieuChinh()
        End Select

    End Sub

    Private Sub TinhTienDieuChinh()
        tbTongTienDieuChinh.EditValue = 0
        tbChietKhauDC.EditValue = 0
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            tbTongTienDieuChinh.EditValue += gdvVTCT.GetRowCellValue(i, "ThanhTien")
            tbChietKhauDC.EditValue += gdvVTCT.GetRowCellValue(i, "ChietKhau") * gdvVTCT.GetRowCellValue(i, "SoLuong")
        Next
        For i As Integer = 0 To gdvChiPhiKhacCT.RowCount - 2
            tbTongTienDieuChinh.EditValue += gdvChiPhiKhacCT.GetRowCellValue(i, "ThanhTien")
            tbChietKhauDC.EditValue += gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhau") * gdvChiPhiKhacCT.GetRowCellValue(i, "SoLuong")
        Next
    End Sub

#End Region

#Region "Chi phí khác"
    Private Sub gdvChiPhiKhacCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChiPhiKhacCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            If gdvChiPhiKhacCT.FocusedRowHandle < 0 Then Exit Sub
            If ShowCauHoi("Xoá dòng hiện tại ?") Then
                If Not gdvChiPhiKhacCT.GetFocusedRowCellValue("IDCGAUX") Is Nothing Then
                    AddParameterWhere("@IDCGAUX", gdvChiPhiKhacCT.GetFocusedRowCellValue("IDCGAUX"))
                    If doDelete("CHAOGIAAUX", "ID=@IDCGAUX") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        CType(gdvChiPhiKhac.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvChiPhiKhacCT.GetFocusedDataSourceRowIndex)
                    End If

                End If

            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvChiPhiKhacCT.FocusedRowHandle = 0 Or gdvChiPhiKhacCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvChiPhiKhacCT.GetFocusedRowCellValue("AZ")
            gdvChiPhiKhacCT.SetFocusedRowCellValue("AZ", gdvChiPhiKhacCT.GetRowCellValue(gdvChiPhiKhacCT.FocusedRowHandle - 1, "AZ"))
            gdvChiPhiKhacCT.SetRowCellValue(gdvChiPhiKhacCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvChiPhiKhacCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvChiPhiKhacCT.FocusedRowHandle = gdvChiPhiKhacCT.RowCount - 2 Or gdvChiPhiKhacCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvChiPhiKhacCT.GetFocusedRowCellValue("AZ")
            gdvChiPhiKhacCT.SetFocusedRowCellValue("AZ", gdvChiPhiKhacCT.GetRowCellValue(gdvChiPhiKhacCT.FocusedRowHandle + 1, "AZ"))
            gdvChiPhiKhacCT.SetRowCellValue(gdvChiPhiKhacCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvChiPhiKhacCT.FocusedRowHandle -= 1
        End If
    End Sub

    Private Sub rPopUpDSThiCong_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rPopUpDSThiCong.Closed
        Dim _NV As String = ","
        Dim _NV2 As String = ""
        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    _NV &= CType(nod2(0), Utils.ItemObject).Value & ","
                    _NV2 &= "- " & CType(nod2(0), Utils.ItemObject).Name & vbCrLf
                End If

            Next
        Next
        Dim _C1 As String = ""
        Dim _C2 As String = ""

        Select Case gdvThiCongCT.FocusedColumn.FieldName
            Case "IDNgThucHien"
                _C1 = "IDNgThucHien"
                _C2 = "NgThucHien"
            Case "IDNgThongBao"
                _C1 = "IDNgThongBao"
                _C2 = "NgThongBao"
            Case "IDNgKiemDuyet1"
                _C1 = "IDNgKiemDuyet1"
                _C2 = "NVKiemDuyetLan1"
            Case "IDNgKiemDuyet2"
                _C1 = "IDNgKiemDuyet2"
                _C2 = "NVKiemDuyetLan2"
        End Select

        If _NV <> "," Then
            gdvThiCongCT.SetFocusedRowCellValue(_C1, _NV)
            gdvThiCongCT.SetFocusedRowCellValue(_C2, _NV2.Trim)
        Else
            gdvThiCongCT.SetFocusedRowCellValue(_C1, Nothing)
            gdvThiCongCT.SetFocusedRowCellValue(_C2, Nothing)
        End If

    End Sub

    Private Sub treeNV_AfterCheckNode(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeNV.AfterCheckNode
        If e.Node.Nodes.Count > 0 Then
            For i As Integer = 0 To e.Node.Nodes.Count - 1
                e.Node.Nodes(i).Checked = e.Node.Checked
            Next
        Else
            Dim _count As Integer = 0
            If e.Node.ParentNode Is Nothing Then Exit Sub
            For i As Integer = 0 To e.Node.ParentNode.Nodes.Count - 1
                If e.Node.ParentNode.Nodes(i).Checked Then _count += 1
            Next
            If _count > 0 Then
                e.Node.ParentNode.CheckState = CheckState.Checked
            Else
                e.Node.ParentNode.CheckState = CheckState.Indeterminate
            End If
        End If
    End Sub

    Private Sub gdvChiPhiKhacCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvChiPhiKhacCT.InitNewRow
        gdvChiPhiKhacCT.SetFocusedRowCellValue("SoLuong", 1)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("DonGia", 0)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("MucThue", 0)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("ChietKhau", 0)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("ChietKhauPT", 0)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("XuatThue", False)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("ThanhTien", 0)
        gdvChiPhiKhacCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvChiPhiKhacCT.GetRowCellValue(gdvChiPhiKhacCT.RowCount - 2, "AZ")) + 1)
    End Sub


#End Region

#Region "Chuyển mã"
    Private Sub chkLocVatTu_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkDongLocDuLieu.CheckedChanged
        gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = chkDongLocDuLieu.Checked
    End Sub

    Private Sub cbTenVatTu_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTenVatTu.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTenVT.EditValue = Nothing
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            btFilterHangSX.EditValue = Nothing
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        If Not btfilterTenVT.EditValue Is Nothing Then
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
        ' End If
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged, btFilterNhomVT.EditValueChanged
        If Not btFilterHangSX.EditValue Is Nothing Then
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        If Not btFilterNhomVT.EditValue Is Nothing Then
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub chkThongDung_CheckedChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkThongDung.CheckedChanged
        If chkThongDung.Checked Then
            Dim FilterString, FilterDisplayText As String
            Dim FilterThongDung As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("ThongDung", True, BinaryOperatorType.Equal))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "Thông dụng = 1"
            FilterThongDung = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvChuyenMaCT.Columns("ThongDung").FilterInfo = FilterThongDung
        Else
            gdvChuyenMaCT.Columns("ThongDung").ClearFilter()
        End If

    End Sub

    Private Sub chkLocTon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocTon.CheckedChanged
        On Error Resume Next
        If chkLocTon.Checked Then
            Dim FilterString, FilterDisplayText As String
            Dim FilterSLTon As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("slTon", 0, BinaryOperatorType.Greater))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "SL tồn > 0"
            FilterSLTon = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvChuyenMaCT.Columns("slTon").FilterInfo = FilterSLTon
        Else
            gdvChuyenMaCT.Columns("slTon").ClearFilter()
        End If
    End Sub

    Private Sub chkLocKhoBan_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocKhoBan.CheckedChanged
        If chkLocKhoBan.Checked Then
            Dim FilterString, FilterDisplayText As String
            Dim FilterKhoBan As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("HangTon", True, BinaryOperatorType.Equal))
            FilterString = BinaryFilter.ToString()
            FilterDisplayText = "Hàng tồn = 1"
            FilterKhoBan = New ColumnFilterInfo(FilterString, FilterDisplayText)
            gdvChuyenMaCT.Columns("HangTon").FilterInfo = FilterKhoBan
        Else
            gdvChuyenMaCT.Columns("HangTon").ClearFilter()
        End If
    End Sub

    Private Sub btChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChuyenMa.ItemClick
        'If TrangThaiCT = False Then Exit Sub
        gdvChuyenMaCT.CloseEditor()
        gdvChuyenMaCT.UpdateCurrentRow()

        For i As Integer = 0 To gdvChuyenMaCT.RowCount - 1
            If gdvChuyenMaCT.GetRowCellValue(i, "SLYC") > 0 Then
                If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                    gdvVTCT.AddNewRow()
                Else
                    If Not ShowCauHoi("Thay thế mã: " & gdvVTCT.GetFocusedRowCellValue("Model") & " bằng " & gdvChuyenMaCT.GetRowCellValue(i, "Model") & " ?") Then
                        Exit Sub
                    End If
                End If
                Dim GiaNhapGanNhat As Double = 0
                AddParameterWhere("@IDVatTu", gdvChuyenMaCT.GetFocusedRowCellValue("ID"))
                AddParameterWhere("@ThoiGian", ThoiGian)
                Dim tb As DataTable = ExecuteSQLDataTable("SET DATEFORMAT DMY SELECT dbo.LayGiaNhap(@IDVatTu,@ThoiGian)")
                If Not tb Is Nothing Then
                    GiaNhapGanNhat = tb.Rows(0)(0)
                End If

                gdvVTCT.SetFocusedRowCellValue("DonGiaGoc", GiaNhapGanNhat)
                gdvVTCT.SetFocusedRowCellValue("TrangThai", TrangThaiChaoGia.ChoXacNhan)
                gdvVTCT.SetFocusedRowCellValue("IDVatTu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                    gdvVTCT.SetFocusedRowCellValue("DonGia", 0)
                    gdvVTCT.SetFocusedRowCellValue("GiaBanPT", 0)
                    gdvVTCT.SetFocusedRowCellValue("ChietKhau", 0)
                    gdvVTCT.SetFocusedRowCellValue("ChietKhauPT", 0)
                    gdvVTCT.SetFocusedRowCellValue("ThanhTien", 0)
                    gdvVTCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 2, "AZ")) + 1)
                End If
                gdvVTCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetRowCellValue(i, "TenVT"))
                gdvVTCT.SetFocusedRowCellValue("ThongSo", gdvChuyenMaCT.GetRowCellValue(i, "Thongso"))
                gdvVTCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetRowCellValue(i, "Model"))
                gdvVTCT.SetFocusedRowCellValue("SoLuong", gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                gdvVTCT.SetFocusedRowCellValue("TenDVT", gdvChuyenMaCT.GetRowCellValue(i, "DVT"))
                gdvVTCT.SetFocusedRowCellValue("TenHang", gdvChuyenMaCT.GetRowCellValue(i, "HangSX"))
                gdvVTCT.SetFocusedRowCellValue("slTon", gdvChuyenMaCT.GetRowCellValue(i, "slTon"))
                gdvVTCT.SetFocusedRowCellValue("DangVe", gdvChuyenMaCT.GetRowCellValue(i, "Dangve"))
                gdvVTCT.SetFocusedRowCellValue("NgayVe", gdvChuyenMaCT.GetRowCellValue(i, "Ngayve"))
                gdvVTCT.SetFocusedRowCellValue("CanXuat", gdvChuyenMaCT.GetRowCellValue(i, "Canxuat"))
                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "GiaList")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaList", 0)
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaList", gdvChuyenMaCT.GetRowCellValue(i, "GiaList"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "GiaBanBuon")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaBanBuon", gdvVTCT.GetFocusedRowCellValue("GiaList"))
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaBanBuon", gdvChuyenMaCT.GetRowCellValue(i, "GiaBanBuon"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "GiaBanLe")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaBanLe", gdvVTCT.GetFocusedRowCellValue("GiaList"))
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaBanLe", gdvChuyenMaCT.GetRowCellValue(i, "GiaBanLe"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Gianhap")) Then
                    gdvVTCT.SetFocusedRowCellValue("GiaNhap", 100)
                Else
                    gdvVTCT.SetFocusedRowCellValue("GiaNhap", gdvChuyenMaCT.GetRowCellValue(i, "Gianhap"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "TienTe")) Then
                    gdvVTCT.SetFocusedRowCellValue("TienTe", TienTe.VND)
                Else
                    gdvVTCT.SetFocusedRowCellValue("TienTe", gdvChuyenMaCT.GetRowCellValue(i, "TienTe"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "TienTe")) Then
                    gdvVTCT.SetFocusedRowCellValue("TienTe", TienTe.VND)
                Else
                    gdvVTCT.SetFocusedRowCellValue("TienTe", gdvChuyenMaCT.GetRowCellValue(i, "TienTe"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "TyGia")) Then
                    gdvVTCT.SetFocusedRowCellValue("TyGia", 1)
                Else
                    gdvVTCT.SetFocusedRowCellValue("TyGia", gdvChuyenMaCT.GetRowCellValue(i, "TyGia"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Xuatthue")) Then
                    gdvVTCT.SetFocusedRowCellValue("XuatThue", False)
                Else
                    gdvVTCT.SetFocusedRowCellValue("XuatThue", gdvChuyenMaCT.GetRowCellValue(i, "Xuatthue"))
                End If

                If IsDBNull(gdvChuyenMaCT.GetRowCellValue(i, "Mucthue")) Then
                    gdvVTCT.SetFocusedRowCellValue("MucThue", 10)
                Else
                    gdvVTCT.SetFocusedRowCellValue("MucThue", gdvChuyenMaCT.GetRowCellValue(i, "Mucthue"))
                End If

                gdvVTCT.SetFocusedRowCellValue("HangTon", gdvChuyenMaCT.GetRowCellValue(i, "HangTon"))
                gdvVTCT.SetFocusedRowCellValue("LoiGia", 0)
                gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
            End If

        Next

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        ShowAlert("Đã thêm vật tư, hàng hóa !")
    End Sub


    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then
            e.Handled = True
            pNhapSoLuong.Visible = True
            pNhapSoLuong.Focus()
            If gdvVTCT.FocusedRowHandle >= 0 Then
                tbSL.EditValue = Convert.ToDouble(gdvVTCT.GetFocusedRowCellValue("SoLuong"))
            Else
                tbSL.EditValue = 1.0
            End If
            tbSL.Focus()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then

            Dim f As New frmTinhTrangVT
            f.Tag = Me.Tag
            f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
            f._HienThongTinNX = False
            f.ShowDialog()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = Not gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
            chkDongLocDuLieu.Checked = gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub
#End Region

#Region "Xuất Excel"
    Private Sub btXuat_Click(sender As System.Object, e As System.EventArgs) Handles btXuat.Click
        XuatExcel.CreateExcelFileChaoGia(SPChaoGia, chkXuatHangSX.Checked, chkXuatMaVT.Checked, chkXuatThongSo.Checked, chkXuatTinhTrangHang.Checked, chkVIE.Checked, chkN0.Checked, chkXuatKH.Checked, True)
    End Sub
#End Region

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        'btfilterTenVT.EditValue = 
        If chkTaiAnh.Checked Then
            colHinhAnh.Visible = True
        Else
            colHinhAnh.Visible = False
        End If
        LoadDSVatTuDungChuyenMa()
    End Sub

    Private Sub tbSL_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbSL.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            gdvChuyenMaCT.SetFocusedRowCellValue("SLYC", Convert.ToDouble(tbSL.EditValue))
            btChuyenMa.PerformClick()
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        ElseIf e.KeyChar = Convert.ToChar(Keys.Escape) Then
            pNhapSoLuong.Visible = False
            gdvChuyenMaCT.Focus()
        End If
    End Sub

    Private Sub btChuyen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChuyen.ItemClick

        'Dim tb As DataTable = CType(gdvThamChieu.Views.Item(0).DataSource, DataView).Table
        Dim tb As DataTable = CType(gdvVT.Views.Item(0).DataSource, DataView).Table
        Dim az As Integer = Convert.ToInt32(gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 1, "AZ"))
        With gdvThamChieuCT
            For i As Integer = 0 To .SelectedRowsCount - 1
                If Not .GetRowCellValue(.GetSelectedRows(i), "IDVatTu") Is Nothing And Not IsDBNull(.GetRowCellValue(.GetSelectedRows(i), "IDVatTu")) Then

                    'Cảnh báo trùng vật tư
                    For j As Integer = 0 To tb.Rows.Count - 1 Step 1
                        If tb.Rows(j)("IDVatTu") = .GetRowCellValue(.GetSelectedRows(i), "IDVatTu") Then
                            ShowCanhBao("Vật tư """ & .GetRowCellValue(.GetSelectedRows(i), "TenVT") & """ đã tồn tại, vui lòng cập nhật lại số lượng !")
                            gdvVT.Focus()
                            gdvVTCT.Focus()
                            'gdvVTCT.FocusedRowHandle = tb.Rows(j)("AZ")
                            'gdvVTCT.FocusedColumn = gdvVTCT.Columns("SoLuong")
                            Exit Sub
                        End If
                    Next

                    az += 1
                    tb.Rows.Add(tb.NewRow)
                    tb.Rows(tb.Rows.Count - 1)("TrangThai") = TrangThaiChaoGia.ChoXacNhan
                    tb.Rows(tb.Rows.Count - 1)("IDVatTu") = .GetRowCellValue(.GetSelectedRows(i), "IDVatTu")
                    tb.Rows(tb.Rows.Count - 1)("DonGiaGoc") = .GetRowCellValue(.GetSelectedRows(i), "DonGiaGoc")
                    tb.Rows(tb.Rows.Count - 1)("TTGiaNhap") = .GetRowCellValue(.GetSelectedRows(i), "DonGiaGoc") * .GetRowCellValue(.GetSelectedRows(i), "SoLuong")
                    tb.Rows(tb.Rows.Count - 1)("DonGia") = 0
                    tb.Rows(tb.Rows.Count - 1)("GiaBanPT") = 0
                    tb.Rows(tb.Rows.Count - 1)("ChietKhau") = .GetRowCellValue(.GetSelectedRows(i), "ChietKhau")
                    tb.Rows(tb.Rows.Count - 1)("ChietKhauPT") = .GetRowCellValue(.GetSelectedRows(i), "ChietKhauPT")
                    tb.Rows(tb.Rows.Count - 1)("ThanhTien") = 0
                    tb.Rows(tb.Rows.Count - 1)("TenVT") = .GetRowCellValue(.GetSelectedRows(i), "TenVT")
                    tb.Rows(tb.Rows.Count - 1)("ThongSo") = .GetRowCellValue(.GetSelectedRows(i), "ThongSo")
                    tb.Rows(tb.Rows.Count - 1)("Model") = .GetRowCellValue(.GetSelectedRows(i), "Model")
                    tb.Rows(tb.Rows.Count - 1)("SoLuong") = .GetRowCellValue(.GetSelectedRows(i), "SoLuong")
                    tb.Rows(tb.Rows.Count - 1)("TenDVT") = .GetRowCellValue(.GetSelectedRows(i), "TenDVT")
                    tb.Rows(tb.Rows.Count - 1)("TenHang") = .GetRowCellValue(.GetSelectedRows(i), "TenHang")
                    tb.Rows(tb.Rows.Count - 1)("slTon") = .GetRowCellValue(.GetSelectedRows(i), "slTon")
                    tb.Rows(tb.Rows.Count - 1)("DangVe") = .GetRowCellValue(.GetSelectedRows(i), "DangVe")
                    tb.Rows(tb.Rows.Count - 1)("NgayVe") = .GetRowCellValue(.GetSelectedRows(i), "NgayVe")
                    tb.Rows(tb.Rows.Count - 1)("CanXuat") = .GetRowCellValue(.GetSelectedRows(i), "CanXuat")
                    tb.Rows(tb.Rows.Count - 1)("GiaList") = .GetRowCellValue(.GetSelectedRows(i), "GiaList")
                    tb.Rows(tb.Rows.Count - 1)("GiaBanBuon") = .GetRowCellValue(.GetSelectedRows(i), "GiaBanBuon")
                    tb.Rows(tb.Rows.Count - 1)("GiaBanLe") = .GetRowCellValue(.GetSelectedRows(i), "GiaBanLe")
                    tb.Rows(tb.Rows.Count - 1)("GiaNhap") = .GetRowCellValue(.GetSelectedRows(i), "GiaNhap")
                    tb.Rows(tb.Rows.Count - 1)("TienTe") = .GetRowCellValue(.GetSelectedRows(i), "TienTe")
                    tb.Rows(tb.Rows.Count - 1)("TyGia") = .GetRowCellValue(.GetSelectedRows(i), "TyGia")
                    tb.Rows(tb.Rows.Count - 1)("XuatThue") = .GetRowCellValue(.GetSelectedRows(i), "XuatThue")
                    tb.Rows(tb.Rows.Count - 1)("MucThue") = .GetRowCellValue(.GetSelectedRows(i), "MucThue")
                    tb.Rows(tb.Rows.Count - 1)("HangTon") = .GetRowCellValue(.GetSelectedRows(i), "HangTon")
                    tb.Rows(tb.Rows.Count - 1)("IDYeuCau") = .GetRowCellValue(.GetSelectedRows(i), "IDYeuCau")
                    tb.Rows(tb.Rows.Count - 1)("LoiGia") = 0
                    tb.Rows(tb.Rows.Count - 1)("AZ") = az

                Else

                    gdvChiPhiKhacCT.AddNewRow()
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("NoiDung", .GetRowCellValue(.GetSelectedRows(i), "TenVT"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("DonGia", .GetRowCellValue(.GetSelectedRows(i), "DonGia"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("ChietKhau", .GetRowCellValue(.GetSelectedRows(i), "ChietKhau"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("ChietKhauPT", .GetRowCellValue(.GetSelectedRows(i), "ChietKhauPT"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("ThanhTien", .GetRowCellValue(.GetSelectedRows(i), "ThanhTien"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("SoLuong", .GetRowCellValue(.GetSelectedRows(i), "SoLuong"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("DVT", .GetRowCellValue(.GetSelectedRows(i), "TenDVT"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("HangSX", .GetRowCellValue(.GetSelectedRows(i), "TenHang"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("XuatThue", .GetRowCellValue(.GetSelectedRows(i), "XuatThue"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("MucThue", .GetRowCellValue(.GetSelectedRows(i), "MucThue"))
                    gdvChiPhiKhacCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvChiPhiKhacCT.GetRowCellValue(gdvChiPhiKhacCT.RowCount - 2, "AZ")) + 1)
                    gdvChiPhiKhacCT.CloseEditor()
                    gdvChiPhiKhacCT.UpdateCurrentRow()
                End If
            Next
        End With
        gdvVT.DataSource = tb
        TinhTienDieuChinh()

    End Sub

    Private Sub btDuyet_Click(sender As System.Object, e As System.EventArgs) Handles btDuyet.Click

        If Not KiemTraQuyenSuDung("Menu", "mCongTrinhCanXuLy", DanhMucQuyen.KiemDuyet) Then Exit Sub

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            If gdvVTCT.GetRowCellValue(i, "IDCG") Is DBNull.Value Then
                ShowCanhBao("Có dòng vật tư mới vẫn chưa bấm ghi lại ! ")
                Exit Sub
            End If
        Next

        If TrangThaiDaXuLy = 1 And cbTrangThai.SelectedIndex = 1 Then
            Exit Sub
        End If


        If cbTrangThai.SelectedIndex = 1 Then
            If tbTongTienCG.EditValue <> tbTongTienDieuChinh.EditValue Then
                ShowCanhBao("Số tiền chưa khớp không thể xác nhận cho xử lý này !")
                Exit Sub
            End If
        End If


        If cbTrangThai.SelectedIndex = 1 Then
            If ShowCauHoi("Xác nhận hoàn thành ?") Then

                Dim tg As DateTime = GetServerTime()
                Dim sql As String = " UPDATE BANGCHAOGIA SET XuLy=1, NgayDuyet=@NgayDuyet, IDNgDuyet=@IDNgDuyet WHERE Sophieu=@SP"
                AddParameterWhere("@NgayDuyet", tg)
                AddParameterWhere("@IDNgDuyet", TaiKhoan)
                AddParameterWhere("@SP", SPChaoGia)
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
                Dim sql2 As String = " UPDATE CHAOGIA SET Canxuat=0 WHERE Sophieu=@SP"
                AddParameterWhere("@SP", SPChaoGia & "CT")
                If ExecuteSQLNonQuery(sql2) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If

                cbTrangThai.SelectedIndex = 1


                'chuyển sang duyệt vật tư đã xác nhận
                For i As Integer = 0 To gdvVTCT.RowCount - 1

                    sql = "select IdVatTu,isnull(SUM(SlXuatKho),0)SoLuong from xuatkhotam where SoCG = @SoCG  group by IdVatTu; "
                    sql &= "select IdVatTu,isnull(SUM(SlNhapKho),0)SoLuong from nhapkhotam where SoCG = @SoCG  group by IdVatTu; "
                    AddParameter("@SoCG", SPChaoGia)
                    Dim ds As DataSet = ExecuteSQLDataSet(sql)
                    If ds Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    End If
                    Dim dtXuat As DataTable = ds.Tables(0)
                    Dim dtNhap As DataTable = ds.Tables(1)
                    'thay số lượng bằng số lượng xuất kho tạm
                    Dim maVT As Object = gdvVTCT.GetRowCellValue(i, "IDVatTu")
                    Dim rowXuat = dtXuat.Select("IdVatTu=" & maVT)
                    Dim soluong As Double = 0
                    If rowXuat.Length > 0 Then
                        soluong = rowXuat(0)("SoLuong")
                        Dim rowNhap = dtNhap.Select("IdVatTu=" & maVT)
                        If rowNhap.Length > 0 Then
                            soluong = soluong - rowNhap(0)("SoLuong")
                        End If
                    End If

                    'lưu số lượng gốc lại để có thể khôi phục
                    If gdvVTCT.GetRowCellValue(i, "TrangThai") = 2 Then
                        If gdvVTCT.GetRowCellValue(i, "DuToan") Is DBNull.Value Then
                            AddParameter("@DuToan", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                            gdvVTCT.SetRowCellValue(i, "DuToan", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                        End If
                    End If
                    AddParameter("@SoLuongGoc", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                    AddParameter("@SoLuong", soluong)
                    AddParameterWhere("@ID", gdvVTCT.GetRowCellValue(i, "IDCG"))

                    doUpdate("CHAOGIA", "ID=@ID")

                    gdvVTCT.SetRowCellValue(i, "SoLuongGoc", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                    gdvVTCT.SetRowCellValue(i, "SoLuong", soluong)
                Next

                ShowAlert("Đã xác nhận !")
            End If
        Else
            If ShowCauHoi("Bạn có chắc là muốn chuyển sang trạng thái chờ xử lý hay không ?") Then
                Dim tg As DateTime = GetServerTime()
                Dim sql As String = " UPDATE BANGCHAOGIA SET XuLy=0, NgayDuyet=NULL, IDNgDuyet=NULL WHERE Sophieu=@SP"
                AddParameterWhere("@SP", SPChaoGia)
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If

                'chuyển sang duyệt vật tư đã xác nhận
                For i As Integer = 0 To gdvVTCT.RowCount - 1
                    AddParameter("@SoLuongGoc", DBNull.Value)
                    AddParameter("@SoLuong", gdvVTCT.GetRowCellValue(i, "SoLuongGoc"))
                    AddParameterWhere("@ID", gdvVTCT.GetRowCellValue(i, "IDCG"))
                    doUpdate("CHAOGIA", "ID=@ID")
                    gdvVTCT.SetRowCellValue(i, "SoLuong", gdvVTCT.GetRowCellValue(i, "SoLuongGoc"))
                    gdvVTCT.SetRowCellValue(i, "SoLuongGoc", DBNull.Value)
                Next

                ShowAlert("Đã xác nhận !")
            End If
        End If

        TrangThaiDaXuLy = cbTrangThai.SelectedIndex


        ' Update số lượng cần xuất
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            Dim sql As String = ""
            If Not IsDBNull(gdvVTCT.GetRowCellValue(i, "IDCG")) Or Not gdvVTCT.GetRowCellValue(i, "IDCG") Is Nothing Then
                If Convert.ToByte(gdvVTCT.GetRowCellValue(i, "TrangThai")) = TrangThaiChaoGia.DaXacNhan Then
                    sql = " Update CHAOGIA Set CanXuat = (Soluong - (select (isnull(sum(soluong),0)) from XUATKHO where IDChaogia = " & gdvVTCT.GetRowCellValue(i, "IDCG") & ")) Where ID = " & gdvVTCT.GetRowCellValue(i, "IDCG")
                Else
                    sql = " Update CHAOGIA Set CanXuat = 0 Where ID = " & gdvVTCT.GetRowCellValue(i, "IDCG")
                End If

                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    ShowBaoLoi(sql)
                End If
            End If

        Next


    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If Not KiemTraQuyenSuDung("Menu", "mCongTrinhCanXuLy", DanhMucQuyen.QuyenThem) Then Exit Sub
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\") Then
                System.IO.Directory.CreateDirectory(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH)
            End If
            For Each file In openFile.FileNames

                Try

                    path = "YC" & SPYeuCau & " CG" & SPChaoGia & " KT " & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\" & path)
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

        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        Try
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            System.IO.File.Delete(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
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
            'Dim psi As New ProcessStartInfo()
            'With psi
            '    .FileName = RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\" & e.CellValue
            '    .UseShellExecute = True
            'End With
            'Try
            '    Process.Start(psi)
            'Catch ex As Exception
            '    ShowBaoLoi(ex.Message)
            'End Try
            OpenFileOnLocal(RootUrlOld & ThoiGian.Year.ToString & "\" & UrlKyThuat & _MaKH & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub lbAn_Click(sender As System.Object, e As System.EventArgs)
        gDSFileDinhKem.Visible = False
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next

        btFileDinhKem.Tag = _File

    End Sub

    Private Sub btFileDinhKem_Click(sender As System.Object, e As System.EventArgs) Handles btFileDinhKem.Click
        gDSFileDinhKem.Visible = True
    End Sub

    Private Sub gdvChiPhiKhacCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvChiPhiKhacCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "ChietKhau", "ChietKhauPT"
                If Not IsNumeric(e.Value) Then
                    gdvChiPhiKhacCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
            Case "SoLuong", "DonGia"
                If Not IsNumeric(e.Value) Then
                    gdvChiPhiKhacCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, 0)
                End If
                gdvChiPhiKhacCT.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(gdvChiPhiKhacCT.GetRowCellValue(e.RowHandle, "DonGia") * gdvChiPhiKhacCT.GetRowCellValue(e.RowHandle, "SoLuong"), 2))
            Case "ThanhTien"
                TinhTienDieuChinh()
        End Select
    End Sub

    Private Sub btCal_Click(sender As System.Object, e As System.EventArgs) Handles btCal.Click
        gdvVT.Refresh()
        gdvVT.RefreshDataSource()
        Dim sql As String = ""
        ' _exit = True
        AddParameterWhere("@SoCG", SPChaoGia)
        If doDelete("CHAOGIAAUX", "Sophieu=@SoCG") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
        sql &= " SELECT ID AS IDCGAUX,(0)AZ,SoPhieu,NoiDung, Donvi AS DVT,SoLuong, DonGia,HangSX, ChietKhau,(0.0)ChietKhauPT, IDNoiDungThiCong, "
        sql &= " (Soluong*Dongia)ThanhTien,MucThue,XuatThue"
        sql &= " FROM CHAOGIAAUX"
        sql &= " WHERE Sophieu=@SoCG"
        sql &= " ORDER BY AZ "

        AddParameterWhere("@SoCG", SPChaoGia)
        Dim tb2 As DataTable = ExecuteSQLDataTable(sql)

        If tb2 Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvChiPhiKhac.DataSource = tb2
        End If
        gdvVTCT.BeginUpdate()

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            Dim _DonGia As Double = 0
            Dim _GiaBanPT As Double = 0

            If gdvVTCT.GetRowCellValue(i, "DonGia") = 0 Then
                _DonGia = gdvVTCT.GetRowCellValue(i, "DonGiaGoc")
                '_DonGia = 1.15 * gdvVTCT.GetRowCellValue(i, "DonGiaGoc")
                If cbTienTe.EditValue > TienTe.VND Then
                    _DonGia /= tbTyGia.EditValue
                Else
                    If chkLamTron.Checked Then
                        If _DonGia < 1000 And _DonGia >= 100 Then
                            _DonGia = Math.Ceiling(_DonGia / 100) * 100
                        ElseIf _DonGia < 100 Then
                            _DonGia = 100
                        Else
                            _DonGia = Math.Ceiling(_DonGia / 1000) * 1000
                        End If
                        '_DonGia = Math.Ceiling(_DonGia * 1000) / 1000
                    End If
                End If
                If gdvVTCT.GetRowCellValue(i, "DonGiaGoc") <> 0 Then
                    _GiaBanPT = (_DonGia / gdvVTCT.GetRowCellValue(i, "DonGiaGoc")) * 100
                    If _DonGia < gdvVTCT.GetRowCellValue(i, "DonGiaGoc") Or gdvVTCT.GetRowCellValue(i, "DonGiaGoc") = 0 Then
                        gdvVTCT.SetRowCellValue(i, "LoiGia", 1)
                    Else
                        gdvVTCT.SetRowCellValue(i, "LoiGia", 0)
                    End If
                Else
                    _GiaBanPT = 0
                    gdvVTCT.SetRowCellValue(i, "LoiGia", 0)
                End If

                gdvVTCT.SetRowCellValue(i, "DonGia", Math.Round(_DonGia, 2))
                gdvVTCT.SetRowCellValue(i, "GiaBanPT", Math.Round(_GiaBanPT, 2))

            Else
                If gdvVTCT.GetRowCellValue(i, "DonGiaGoc") = 0 Then
                    gdvVTCT.SetRowCellValue(i, "GiaBanPT", 0)
                Else
                    gdvVTCT.SetRowCellValue(i, "GiaBanPT", (gdvVTCT.GetRowCellValue(i, "DonGia") / gdvVTCT.GetRowCellValue(i, "DonGiaGoc")) * 100)
                End If

                If gdvVTCT.GetRowCellValue(i, "DonGia") < gdvVTCT.GetRowCellValue(i, "DonGiaGoc") Or gdvVTCT.GetRowCellValue(i, "DonGiaGoc") = 0 Then
                    gdvVTCT.SetRowCellValue(i, "LoiGia", 1)
                Else
                    gdvVTCT.SetRowCellValue(i, "LoiGia", 0)
                End If
            End If

            gdvVTCT.SetRowCellValue(i, "ThanhTien", gdvVTCT.GetRowCellValue(i, "DonGia") * gdvVTCT.GetRowCellValue(i, "SoLuong"))

            If gdvVTCT.GetRowCellValue(i, "ChietKhauPT") = 0 And gdvVTCT.GetRowCellValue(i, "ChietKhau") = 0 Then Continue For

            If gdvVTCT.GetRowCellValue(i, "ChietKhauPT") = 0 And gdvVTCT.GetRowCellValue(i, "ChietKhau") > 0 Then
                gdvVTCT.SetRowCellValue(i, "ChietKhauPT", (gdvVTCT.GetRowCellValue(i, "ChietKhau") / gdvVTCT.GetRowCellValue(i, "DonGia")) * 100)
            ElseIf gdvVTCT.GetRowCellValue(i, "ChietKhauPT") > 0 And gdvVTCT.GetRowCellValue(i, "ChietKhau") = 0 Then
                gdvVTCT.SetRowCellValue(i, "ChietKhau", (gdvVTCT.GetRowCellValue(i, "ChietKhauPT") / 100) * gdvVTCT.GetRowCellValue(i, "DonGia"))
            Else
                gdvVTCT.SetRowCellValue(i, "ChietKhauPT", (gdvVTCT.GetRowCellValue(i, "ChietKhau") / gdvVTCT.GetRowCellValue(i, "DonGia")) * 100)
            End If

        Next
        gdvVTCT.EndUpdate()
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()

        For i As Integer = 0 To gdvChiPhiKhacCT.RowCount - 2
            If gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhauPT") = 0 And gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhau") = 0 Then Continue For
            If gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhauPT") = 0 And gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhau") > 0 Then
                gdvChiPhiKhacCT.SetRowCellValue(i, "ChietKhauPT", (gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhau") / gdvChiPhiKhacCT.GetRowCellValue(i, "DonGia")) * 100)
            ElseIf gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhauPT") > 0 And gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhau") = 0 Then
                gdvChiPhiKhacCT.SetRowCellValue(i, "ChietKhau", (gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhauPT") / 100) * gdvChiPhiKhacCT.GetRowCellValue(i, "DonGia"))
            Else
                gdvChiPhiKhacCT.SetRowCellValue(i, "ChietKhauPT", (gdvChiPhiKhacCT.GetRowCellValue(i, "ChietKhau") / gdvChiPhiKhacCT.GetRowCellValue(i, "DonGia")) * 100)
            End If
        Next


        TinhTienDieuChinh()
        _exit = False

        If chkLoiNhuan.Checked Then
            Dim _loiNhuan As Double = tbTongTienCG.EditValue - tbTongTienDieuChinh.EditValue
            Dim _chietKhau As Double = tbChietKhauCG.EditValue - tbChietKhauDC.EditValue
            gdvChiPhiKhacCT.AddNewRow()
            gdvChiPhiKhacCT.SetFocusedRowCellValue("NoiDung", "Lợi nhuận, chi phí khác")
            gdvChiPhiKhacCT.SetFocusedRowCellValue("DonGia", _loiNhuan)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("ChietKhau", _chietKhau)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("ChietKhauPT", 0)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("ThanhTien", _loiNhuan)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("SoLuong", 1)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("DVT", 34)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("HangSX", "BAC")
            gdvChiPhiKhacCT.SetFocusedRowCellValue("XuatThue", False)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("MucThue", 0)
            gdvChiPhiKhacCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvChiPhiKhacCT.GetRowCellValue(gdvChiPhiKhacCT.RowCount - 2, "AZ")) + 1)
            gdvChiPhiKhacCT.CloseEditor()
            gdvChiPhiKhacCT.UpdateCurrentRow()
            TinhTienDieuChinh()
            ' End If
        End If

    End Sub

    Private Sub rThongSo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rThongSo.ButtonClick
        btFilterThongSo.EditValue = Nothing
    End Sub

    Private Sub rcbMa_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMa.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub gdvTaiLieuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvTaiLieuCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(UrlTaiLieuVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub LoadDSTaiLieu(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvTaiLieu.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvTaiLieuCT.AddNewRow()
                gdvTaiLieuCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvTaiLieuCT.CloseEditor()
        gdvTaiLieuCT.UpdateCurrentRow()

    End Sub

    Private Sub rcbTaiLieu_Popup(sender As System.Object, e As System.EventArgs) Handles rcbTaiLieu.Popup
        LoadDSTaiLieu(CType(sender, PopupContainerEdit).EditValue)
    End Sub


    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvVTCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "LoiGia" Then
            If e.CellValue = 1 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.Admin) Then Exit Sub
        colGDonGiaNhap.VisibleIndex = 12
        colGDonGiaNhap.Visible = chkHienGia.Checked
        colTTGiaNhap.VisibleIndex = 16
        colTTGiaNhap.Visible = chkHienGia.Checked

        colGiaBanPT.Visible = chkHienGia.Checked
        colDonGiaGoc.Visible = chkHienGia.Checked
        colDonGia.Visible = chkHienGia.Checked
        colChietKhauPT.Visible = chkHienGia.Checked
        colChietKhauTT.Visible = chkHienGia.Checked
        colThanhTien.Visible = chkHienGia.Checked
        colVATPT.Visible = chkHienGia.Checked
        colVAT.Visible = chkHienGia.Checked

        colGGiaBanPT.Visible = chkHienGia.Checked
        colGGiaBanTT.Visible = chkHienGia.Checked
        colGDonGiaNhap.Visible = chkHienGia.Checked
        colGChietKhauPT.Visible = chkHienGia.Checked
        colGChietKhauTT.Visible = chkHienGia.Checked
        colGThanhTien.Visible = chkHienGia.Checked
        colTTGiaNhap.Visible = chkHienGia.Checked
        colGVAPT.Visible = chkHienGia.Checked
        colGVAT.Visible = chkHienGia.Checked
        colGGiaList.Visible = chkHienGia.Checked
        colGGiaBanLe.Visible = chkHienGia.Checked
        colGGiaBanBuon.Visible = chkHienGia.Checked
        colGGiaNhap.Visible = chkHienGia.Checked
        colGDonGiaCP.Visible = chkHienGia.Checked
        colGChietKhauPTCP.Visible = chkHienGia.Checked
        colGChietKhauCP.Visible = chkHienGia.Checked
        colGThanhTienCP.Visible = chkHienGia.Checked
        colGVATPTCP.Visible = chkHienGia.Checked
        colGVATCP.Visible = chkHienGia.Checked

    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs)
        printFile.CongTrinh.BangKeVatTu(SPChaoGia)
    End Sub

    Private Sub btXacNhanIn_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhanIn.Click
        If chkInBangKe.Checked And chkInKeHoachThiCong.Checked = False Then
            printFile.CongTrinh.BangKeVatTu(SPChaoGia)
        ElseIf chkInBangKe.Checked = False And chkInKeHoachThiCong.Checked Then
            printFile.CongTrinh.KeHoachThiCong(SPChaoGia, SPYeuCau)
        Else
            printFile.CongTrinh.BangKeVatTuVaKeHoachThiCong(SPChaoGia, SPYeuCau)
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        pIn.Visible = False
    End Sub

    Private Sub btIn_Click(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        pIn.Visible = True
    End Sub

    Private Sub btAnDSFile_Click(sender As System.Object, e As System.EventArgs) Handles btAnDSFile.Click
        gDSFileDinhKem.Visible = False
    End Sub

    Private Sub btFileLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btFileLienQuan.Click
        Dim f As New frmFileLienQuan
        f.Tag = Me.Tag
        f.SoChaoGia = SPChaoGia
        f.SoYeuCau = SPDatHang
        f.MaKH = _MaKH
        f.ShowDialog()
    End Sub

    Private Sub gdvThamChieuCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvThamChieuCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then

            Dim f As New frmTinhTrangVT
            f.Tag = Me.Tag
            f._IDVatTu = gdvThamChieuCT.GetFocusedRowCellValue("IDVatTu")
            f._HienThongTinNX = False
            f.ShowDialog()
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            btChuyen.PerformClick()
        End If
    End Sub

    Private Sub rcbMa_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles rcbMa.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' rcbMa.
            btTaiLai.PerformClick()
        End If
    End Sub


    Private Sub mTaiTaiLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiTaiLieu.ItemClick
        If gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File").ToString <> "" Then
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File"))
            saveFile.FileName = gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File")
            If saveFile.ShowDialog = DialogResult.OK Then
                Try
                    ShowWaiting("Đang tải file về máy ...")
                    System.IO.File.Copy(UrlTaiLieuVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvTaiLieuCT.GetRowCellValue(gdvTaiLieuCT.FocusedRowHandle, "File"), saveFile.FileName, True)
                    CloseWaiting()
                    If ShowCauHoi("Đã tải file về máy, bạn có muốn mở file vừa tải không ?") Then
                        Dim psi As New ProcessStartInfo()
                        With psi
                            .FileName = saveFile.FileName
                            .UseShellExecute = True
                        End With
                        Try
                            Process.Start(psi)
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try
                    End If
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub mXemAnhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemAnhLon.ItemClick
        If gdvChuyenMaCT.GetFocusedRowCellValue("HinhAnh").ToString = "" Then Exit Sub
        Dim f As New frmXemAnh
        f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HinhAnh").ToString)
        f.Text = "Ảnh: " & gdvChuyenMaCT.GetFocusedRowCellValue("Model").ToString
        f.ShowDialog()
    End Sub

    Private Sub mTaiAnhVeMay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiAnhVeMay.ItemClick
        If gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString = "" Then Exit Sub
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "File Type|*." & System.IO.Path.GetExtension(gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString)
        saveFile.FileName = gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString
        If saveFile.ShowDialog = DialogResult.OK Then
            Try
                ShowWaiting("Đang tải file về máy ...")
                System.IO.File.Copy(UrlAnhVatTu & gdvChuyenMaCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvChuyenMaCT.GetFocusedRowCellValue("HangSX") & "\" & gdvChuyenMaCT.GetRowCellValue(gdvChuyenMaCT.FocusedRowHandle, "HinhAnh").ToString, saveFile.FileName, True)
                CloseWaiting()
                If ShowCauHoi("Đã tải file về máy, bạn có muốn mở file vừa tải không ?") Then
                    Dim psi As New ProcessStartInfo()
                    With psi
                        .FileName = saveFile.FileName
                        .UseShellExecute = True
                    End With
                    Try
                        Process.Start(psi)
                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub


    Private Sub gdvThiCongCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvThiCongCT.InitNewRow
        gdvThiCongCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvThiCongCT.GetRowCellValue(gdvThiCongCT.RowCount - 2, "AZ")) + 1)
        gdvThiCongCT.SetFocusedRowCellValue("NgayBatDau", New DateTime(Today.Year, Today.Month, Today.Day, 7, 30, 0))
        gdvThiCongCT.SetFocusedRowCellValue("NgayKetThuc", New DateTime(Today.Year, Today.Month, Today.Day, 17, 30, 0))
    End Sub

    Private Sub gdvThiCongCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvThiCongCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            If gdvThiCongCT.FocusedRowHandle < 0 Then Exit Sub
            If ShowCauHoi("Xoá dòng hiện tại ?") Then
                If Not gdvThiCongCT.GetFocusedRowCellValue("ID") Is Nothing Then
                    AddParameterWhere("@ID", gdvThiCongCT.GetFocusedRowCellValue("ID"))
                    If doDelete("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        CType(gdvThiCong.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvThiCongCT.GetFocusedDataSourceRowIndex)
                    End If
                End If
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvThiCongCT.FocusedRowHandle = 0 Or gdvThiCongCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvThiCongCT.GetFocusedRowCellValue("AZ")
            gdvThiCongCT.SetFocusedRowCellValue("AZ", gdvThiCongCT.GetRowCellValue(gdvThiCongCT.FocusedRowHandle - 1, "AZ"))
            gdvThiCongCT.SetRowCellValue(gdvThiCongCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvThiCongCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvThiCongCT.FocusedRowHandle = gdvThiCongCT.RowCount - 2 Or gdvThiCongCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvThiCongCT.GetFocusedRowCellValue("AZ")
            gdvThiCongCT.SetFocusedRowCellValue("AZ", gdvThiCongCT.GetRowCellValue(gdvThiCongCT.FocusedRowHandle + 1, "AZ"))
            gdvThiCongCT.SetRowCellValue(gdvThiCongCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvThiCongCT.FocusedRowHandle -= 1
        End If
    End Sub

    Private Sub btNangCao_ShowDropDownControl(sender As System.Object, e As DevExpress.XtraEditors.ShowDropDownControlEventArgs) Handles btNangCao.ShowDropDownControl
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.BanQuanTri) Then
            ShowCanhBao("Bạn cần có quyền admin hoặc ban quản trị để sử dụng tính năng này !")
            e.Allow = False
        End If
    End Sub

    Private Sub gdvThiCongCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvThiCongCT.CellValueChanged
        If e.Column.FieldName = "NgayBatDau" Then
            If _exit Then Exit Sub
            If Convert.ToDateTime(e.Value).Hour = 0 And Convert.ToDateTime(e.Value).Minute = 0 Then
                _exit = True
                gdvThiCongCT.SetFocusedRowCellValue("NgayBatDau", Convert.ToDateTime(e.Value).AddHours(7).AddMinutes(30))
                _exit = False
            End If
        ElseIf e.Column.FieldName = "NgayKetThuc" Then
            If _exit Then Exit Sub
            If Convert.ToDateTime(e.Value).Hour = 0 And Convert.ToDateTime(e.Value).Minute = 0 Then
                _exit = True
                gdvThiCongCT.SetFocusedRowCellValue("NgayKetThuc", Convert.ToDateTime(e.Value).AddHours(7).AddMinutes(30))
                _exit = False
            End If
        End If
    End Sub


    Private Sub mLapYCVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLapYCVatTu.ItemClick
        If gdvThamChieuCT.SelectedRowsCount < 0 Then

        End If
    End Sub

    Private Sub btTinhTrangVTXL_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVTXL.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        f._IDVatTu = gdvVTCT.GetFocusedRowCellValue("IDVatTu")
        f._HienThongTinNX = False
        f.ShowDialog()
    End Sub

    Private Sub btTinhTrangVTCM_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTinhTrangVTCM.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
        f._HienThongTinNX = False
        f.ShowDialog()
    End Sub

    Private Sub gdvChuyenMaCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvChuyenMaCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "CanhBao" Then
            If Not gdvChuyenMaCT.GetRowCellValue(e.RowHandle, "ConSX") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub


    Private Sub mPasteVatTuDaSaoChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPasteVatTuDaSaoChep.ItemClick
        AddParameterWhere("@SP", _SPDaSaoChep)
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT NULL AS IDCG, CHAOGIA.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,Soluong AS SoLuong,CHAOGIA.IDYeucau AS IDYeuCau,CHAOGIA.NgayCan,"
        sql &= " (CHAOGIA.Dongia * CHAOGIA.Soluong) AS ThanhTien,"
        sql &= " ISNULL(ISNULL("
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap "
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "            ORDER BY Ngaythang DESC),"
        sql &= "        (SELECT     TOP (1) Gianhap"
        sql &= "            FROM V_GiaNhap"
        sql &= "            WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,ISNULL((SELECT TOP 1 NgayThang FROM PHIEUXUATKHO WHERE SoPhieuCG=BANGCHAOGIA.SoPhieu ORDER BY NgayThang DESC ),getdate()),103),103)"
        sql &= "                 ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)*tblTienTe.TyGia) AS DonGiaGoc,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"
        sql &= " (SELECT 0.0)TTGiaNhap,"
        sql &= " CHAOGIA.Xuatthue AS XuatThue,CHAOGIA.Mucthue AS MucThue,VATTU.Gianhap1 AS GiaNhap,IDTGGiaoHang,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"

        sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = xuatkhotam.SoCG),'')),0) - "
        sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVattu AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = nhapkhotam.SoCG),'')),0) "
        sql &= " as XuatTam, "

        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA CG where CG.IDVattu= CHAOGIA.IDVattu) AS CanXuat,CHAOGIA.TrangThai,CHAOGIA.Dongia AS DonGia,(0.0)GiaBanPT,"
        sql &= " CHAOGIA.Chietkhau AS ChietKhau,(0.0)ChietKhauPT,"
        sql &= " ISNULL(VATTU.Tiente1,0) AS TienTe,ISNULL(ISNULL(CHAOGIA.TyGia,tblTienTe.TyGia),1)TyGia, VATTU.HangTon , ISNULL(CHAOGIA.AZ,0)AZ, (SELECT 0) LoiGia "
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=CHAOGIA.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " WHERE CHAOGIA.Sophieu=@SP"
        sql &= " ORDER BY  AZ "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            With tb
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1

                    Dim _GiaBanPT As Double = 0
                    If Not .Rows(i)("GiaList") Is Nothing And Not IsDBNull(.Rows(i)("GiaList")) Then
                        If .Rows(i)("GiaList") <> 0 Then
                            If Convert.ToByte(.Rows(i)("TienTe")) > Convert.ToByte(cbTienTe.EditValue) Then
                                _GiaBanPT = .Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)
                            Else
                                _GiaBanPT = .Rows(i)("DonGia") / .Rows(i)("GiaList")
                            End If
                        End If
                        .Rows(i)("GiaBanPT") = Math.Round(_GiaBanPT * 100, 2)
                    End If

                    If .Rows(i)("DonGia") > 0 Then
                        .Rows(i)("ChietKhauPT") = (.Rows(i)("ChietKhau") / .Rows(i)("DonGia")) * 100
                    Else
                        .Rows(i)("ChietKhauPT") = 0
                    End If

                    If IsDBNull(.Rows(i)("DonGiaGoc")) Then .Rows(i)("DonGiaGoc") = 0
                    If .Rows(i)("DonGiaGoc") = 0 Then
                        .Rows(i)("DonGiaGoc") = (.Rows(i)("GiaList") * (.Rows(i)("GiaNhap") / 100) * .Rows(i)("TyGia")) / tbTyGia.EditValue
                    End If
                    .Rows(i)("TTGiaNhap") = .Rows(i)("SoLuong") * .Rows(i)("DonGiaGoc")
                    If .Rows(i)("DonGia") < .Rows(i)("DonGiaGoc") Then .Rows(i)("LoiGia") = 1

                Next
            End With

            If ShowCauHoi("Bạn có muốn xóa các vật tư, hàng hóa hiện đã xử lý không ?") Then
                AddParameterWhere("@SP", SPChaoGia)
                If doDelete("CHAOGIA", "SoPhieu=@SP") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
                gdvVT.DataSource = tb

            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)

        End If
    End Sub

    Private Sub pMenuVT_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuVT.BeforePopup
        If _SPDaSaoChep Is Nothing Then
            mPasteVatTuDaSaoChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            mPasteVatTuDaSaoChep.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub btTop10_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTop10.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sqlWhere As String = " WHERE Maloi=0 "

        Dim sql As String = " Select TOP 10 NULL AS CanhBao,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= " (Round((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID),4)-Round((select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID),4)) AS slTon, "

        'sql &= " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = VATTU.ID AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = xuatkhotam.SoCG),'')),0) - "
        'sql &= " isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = VATTU.ID AND SoCG <> isnull((SELECT TOP 1 SophieuCG FROM phieuxuatkho WHERE SophieuCG = nhapkhotam.SoCG),'')),0) "
        'sql &= " as XuatTam, "

        sql &= "  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu =  VATTU.ID),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu =  VATTU.ID),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu =  VATTU.ID AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu =  VATTU.ID and SlXuatKho > 0)),0) "
        sql &= " as XuatTam,"

        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
        sql &= " Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
        sql &= " Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaBan1/100),2) END)  ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaBan1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanLe,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),-2) ELSE ROUND((VATTU.DonGia1*VATTU.GiaNCC1/100),2) END) ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND(( (VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND(((VATTU.DonGia1*VATTU.GiaNCC1/100)/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaBanBuon,"

        sql &= " VATTU.Gianhap1 AS Gianhap, tblTienTe.Ten AS Tiente,TonNCC,"
        sql &= " TENNUOC.Ten AS Xuatxu,convert(float,0) AS SLYC, VATTU.ThongDung,VATTU.HangTon,VATTU.HinhAnh,(convert(image,NULL))HienThi,VATTU.TaiLieu,VATTU.ConSX "
        sql &= " from VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "

        'If Not btFilterMaKH.EditValue Is Nothing Then
        '    sql &= " INNER JOIN (SELECT XUATKHO.ID,XUATKHO.IDVattu,PHIEUXUATKHO.IDKhachhang FROM XUATKHO INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu)tbXK ON VATTU.ID=tbXK.IDVattu AND tbXK.IDKhachhang = " & btFilterMaKH.EditValue
        'End If


        sql &= sqlWhere
        sql &= " ORDER BY ID DESC "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        Application.DoEvents()
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("HangSX") & "\thumb\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception

                            End Try
                        End If
                    Next
                End With
            End If

            gdvChuyenMa.DataSource = tb

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub rcbMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            cbMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub tbTGThiCong_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbTGThiCong.Properties.ButtonClick
        If e.Button.Index = 1 Then
            tbTGThiCong.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub cbNhanKS_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhanKS.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanKS.SelectedIndex = -1
        End If
    End Sub

    Private Sub cbPhuTrachCT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbPhuTrachCT.ButtonClick
        If e.Button.Index = 1 Then
            cbPhuTrachCT.EditValue = Nothing
        End If
    End Sub
    Private Sub luePhuTrachTC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles luePhuTrachTC.ButtonClick
        If e.Button.Index = 1 Then
            luePhuTrachTC.EditValue = Nothing
        End If
    End Sub
    Private Sub mChuyenSangDaXN_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenSangDaXN.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này")
        Else
            If ShowCauHoi("Chuyển những mục được chọn sang trạng thái đã xác nhận ? ") Then
                gdvVTCT.BeginUpdate()
                For i As Integer = 0 To gdvVTCT.SelectedRowsCount - 1
                    gdvVTCT.SetRowCellValue(gdvVTCT.GetSelectedRows(i), "TrangThai", TrangThaiChaoGia.DaXacNhan)

                Next
                gdvVTCT.EndUpdate()
            End If

        End If
    End Sub


    Private Sub btnChotVatTuCT_Click(sender As System.Object, e As System.EventArgs) Handles btnChotVatTuCT.Click

        If Not ShowCauHoi("Thực hiện thao tác chốt vật tư công trình theo xuất kho tạm?") Then Exit Sub

        Dim sql As String = "select IdVatTu,isnull(SUM(SlXuatKho),0)SoLuong from xuatkhotam where SoCG = @SoCG  group by IdVatTu; "
        sql &= "select IdVatTu,isnull(SUM(SlNhapKho),0)SoLuong from nhapkhotam where SoCG = @SoCG  group by IdVatTu; "

        AddParameter("@SoCG", SPChaoGia)

        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        Dim dtXuat As DataTable = ds.Tables(0)
        Dim dtNhap As DataTable = ds.Tables(1)

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "DuToan", gdvVTCT.GetRowCellValue(i, "SoLuong"))
            Dim maVT As Object = gdvVTCT.GetRowCellValue(i, "IDVatTu")
            Dim rowXuat = dtXuat.Select("IdVatTu=" & maVT)
            If rowXuat.Length > 0 Then
                Dim soluong As Double = rowXuat(0)("SoLuong")
                Dim rowNhap = dtNhap.Select("IdVatTu=" & maVT)
                If rowNhap.Length > 0 Then
                    soluong = soluong - rowNhap(0)("SoLuong")
                End If
                gdvVTCT.SetRowCellValue(i, "SoLuong", soluong)
            Else
                gdvVTCT.SetRowCellValue(i, "SoLuong", 0)
            End If

        Next

        cbTrangThai.EditValue = "Đã xử lý"

    End Sub



    Private Sub gdvVTCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvVTCT.CustomDrawCell
        If e.RowHandle < 0 Then Exit Sub
        Try
            If gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong") Is DBNull.Value OrElse gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong") = 0 Then
                e.Appearance.BackColor = Color.Yellow
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCapNhatTTchung_Click(sender As System.Object, e As System.EventArgs) Handles btnCapNhatTTchung.Click
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()


        AddParameter("@IDLoaiYeuCau", cbLoaiYeuCau.EditValue)
        AddParameterWhere("@ID", objIDYC)
        If doUpdate("BANGYEUCAU", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If


        AddParameter("@IDPhuTrachCT", cbPhuTrachCT.EditValue)
        AddParameter("@IDPhuTrachTC", luePhuTrachTC.EditValue)


        If cbNhanKS.SelectedIndex = -1 Then
            AddParameter("@NhanKS", DBNull.Value)
        Else
            AddParameter("@NhanKS", cbNhanKS.SelectedIndex)
        End If

        AddParameterWhere("@SP", SPChaoGia)
        If doUpdate("BANGCHAOGIA", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)


        ShowAlert("Đã cập nhật thông tin chung!")

    End Sub

    Private Sub mnuChuyenCot_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChuyenCot.ItemClick, BarButtonItem1.ItemClick
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKyThuat) Then
            For i As Integer = 0 To gdvVTCT.RowCount - 1
                gdvVTCT.SetRowCellValue(i, "SoLuong", gdvVTCT.GetRowCellValue(i, "XuatTam"))
            Next
        Else
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
        End If
    End Sub
End Class