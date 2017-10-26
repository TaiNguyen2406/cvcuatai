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

Public Class frmCNXuatKhoCT

    Public tmpTrangThai As New Utils.TrangThai
    Public tmpMaTuDien As Object
    Private EndSelect As Boolean
    Private Move_Next As Boolean
    Public SoPhieuPXKCT As Object = 1
    Public SoPhieuCG As Object
    Private _exit As Boolean
    Public _MaKH As String
    Public _FileCGKinhDoanh As String
    Public NVThamGia As New ArrayList

    Private Sub frmCNCongTrinhCanXuLy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDSThoiGianGH()
        loadGdvVatTuChaoGia()
        loadNhanVien()
        loadKhachHang()
        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        If TrangThai.isAddNew Then
            tbNgay.EditValue = GetServerTime()

            cbNgLap.EditValue = Convert.ToInt32(TaiKhoan)
            cbNgNhan.EditValue = Convert.ToInt32(TaiKhoan)
            gdvMaKH.EditValue = _MaKH
            gdvPhieuCG.EditValue = SoPhieuCG
            'tbSoPhieu.EditValue = LaySoPhieu("tblPhieuXKCT")
            'tbNgay.EditValue = GetServerTime()
        Else
            'Dim sql As String = ""
            'sql &= " SELECT * FROM "
            'AddParameterWhere("@SoPhieu", SoPhieuPXKCT)
            'Dim ds As DataSet = ExecuteSQLDataSet("")

        End If


    End Sub

    Private Sub frmCNCongTrinhCanXuLy_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmCongTrinhCanXuLy).LoadDS()
    End Sub

#Region "Load dữ liệu cho các combobox"

    Public Sub loadKhachHang()
        Dim sql As String = "SELECT ID,ttcMa,Ten FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvMaKH.Properties.DataSource = tb
        End If
    End Sub

    Public Sub loadDSChaoGia()
        On Error Resume Next
        AddParameterWhere("@MaKH", gdvMaKH.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,CongTrinh,TenDuAn FROM BANGCHAOGIA WHERE IDKhachhang=@MaKH ORDER BY SoPhieu")
        If Not tb Is Nothing Then
            gdvPhieuCG.Properties.DataSource = tb
        End If
    End Sub

    Public Sub loadNhanVien()
        Dim sql As String = "SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74"
        If TrangThai.isUpdate Then
            sql &= " AND TrangThai=1"
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbNgLap.Properties.DataSource = tb
            cbNgNhan.Properties.DataSource = tb
            cbNgXacNhan.Properties.DataSource = tb
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
        ShowWaiting("Đang tải danh sách vật tư dùng chuyển mã ...")
        Dim sqlWhere As String = " WHERE Maloi=0 "

        Dim sql As String = "Select TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS slTon, "
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= Vattu.ID) AS Dangve, "
        sql &= " Ngayve = (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= Vattu.ID), "
        sql &= " Canxuat=(select isnull(SUM(canxuat),0) from Chaogia where IDVattu= Vattu.ID), "
        sql &= " VATTU.Dongia1 as GiaList,VATTU.Giaban1 GiaBanLe, VATTU.Xuatthue1 AS Xuatthue,VATTU.Mucthue1 AS Mucthue, "
        sql &= " VATTU.GiaNCC1 GiaBanBuon,VATTU.Gianhap1 AS Gianhap, tblTienTe.Ten AS TenTienTe,tblTienTe.TyGia,VATTU.Tiente1 AS TienTe, "
        sql &= " TENNUOC.Ten AS Xuatxu, VATTU.TaiLieu,convert(float,0) AS SLYC, VATTU.HangTon,(convert(image,NULL))HienThi,VATTU.HinhAnh, VATTU.ThongDung, TENNHOM.Ten_ENG AS TenNhom_ENG "
        sql &= " FROM VATTU LEFT OUTER JOIN TENVATTU ON VATTU.IDTENVATTU=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENNHOM ON VATTU.IDTennhom=TENNHOM.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID LEFT OUTER JOIN TENNUOC ON VATTU.IDTennuoc=TENNUOC.ID "
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "

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
            gdvChuyenMa.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub


    Public Sub loadGdvVatTuChaoGia()
        Dim sql As String = " SET DATEFORMAT DMY"
        sql &= " SELECT ID,NgayThang,SoPhieu,IDKH,SoPhieuCG,LyDoXuat,IDNgLap,IDNgNhan,IDNgXacNhan,XacNhan,ThoiGianXN FROM tblPhieuXKCT "
        sql &= " WHERE SoPhieu=@SPXuatCT"

        sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SoLuong,CHAOGIA.IDYeuCau,IDTGGiaoHang,CHAOGIA.CanXuat,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe, VATTU.HangTon, ISNULL(CHAOGIA.AZ,0)AZ"
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=CHAOGIA.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " WHERE CHAOGIA.Sophieu=@SP"
        sql &= " ORDER BY AZ "

        sql &= " SELECT tblXKCT.ID AS IDXKCT, tblXKCT.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,SLYC,SLXuatKho,tblXKCT.NgayCan,"
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=tblXKCT.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=tblXKCT.IDVattu)) AS slTon,"
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= tblXKCT.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= tblXKCT.IDVattu) AS NgayVe,"
        sql &= " (select isnull(SUM(canxuat),0) from CHAOGIA where CHAOGIA.IDVattu= tblXKCT.IDVattu) AS CanXuat, VATTU.HangTon , "
        sql &= " ISNULL(tblXKCT.AZ,0)AZ "
        sql &= " FROM tblXKCT LEFT OUTER JOIN VATTU ON tblXKCT.IDVatTu=VATTU.ID"
        sql &= " INNER JOIN tblPhieuXKCT ON tblPhieuXKCT.SoPhieu=tblXKCT.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " LEFT OUTER JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID"
        sql &= " WHERE tblXKCT.SoPhieu=@SPXuatCT"
        sql &= " ORDER BY AZ "

        AddParameter("@SP", SoPhieuCG)
        AddParameter("@SPXuatCT", SoPhieuPXKCT)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            With ds.Tables(1)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                Next
            End With

            With ds.Tables(2)
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    If TrangThai.isCopy Then
                        .Rows(i)("IDXKCT") = DBNull.Value
                        .Rows(i)("SLXuatKho") = 0
                        .Rows(i)("NgayCan") = DBNull.Value
                    End If
                Next
            End With

            gdvThamChieu.DataSource = ds.Tables(1)
            gdvVT.DataSource = ds.Tables(2)
            If TrangThai.isUpdate Then
                tbSoPhieu.EditValue = ds.Tables(0).Rows(0)("SoPhieu")
                tbNgay.EditValue = ds.Tables(0).Rows(0)("NgayThang")
                cbNgLap.EditValue = ds.Tables(0).Rows(0)("IDNgLap")
                cbNgNhan.EditValue = ds.Tables(0).Rows(0)("IDNgNhan")
                gdvMaKH.EditValue = ds.Tables(0).Rows(0)("IDKH")
                gdvPhieuCG.EditValue = ds.Tables(0).Rows(0)("SoPhieuCG")
                tbLyDoXuat.EditValue = ds.Tables(0).Rows(0)("LyDoXuat")
                chkXacNhan.EditValue = ds.Tables(0).Rows(0)("XacNhan")
                cbNgXacNhan.EditValue = ds.Tables(0).Rows(0)("IDNgXacNhan")
                tbThoiGianXN.EditValue = ds.Tables(0).Rows(0)("ThoiGianXN")
            ElseIf TrangThai.isCopy Then
                cbNgLap.EditValue = Convert.ToInt32(TaiKhoan)
                cbNgNhan.EditValue = Convert.ToInt32(TaiKhoan)
                gdvMaKH.EditValue = ds.Tables(0).Rows(0)("IDKH")
                gdvPhieuCG.EditValue = ds.Tables(0).Rows(0)("SoPhieuCG")
                tbLyDoXuat.EditValue = ds.Tables(0).Rows(0)("LyDoXuat")

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

#Region "Lưu lại"
    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()

        Try
            Dim _ThoiGian As DateTime = GetServerTime()
            Dim _SoPhieu As Object = LaySoPhieu("tblPhieuXKCT")
            BeginTransaction()
            AddParameter("@NgayThang", _ThoiGian)
            AddParameter("@IDKH", gdvMaKH.EditValue)
            AddParameter("@SoPhieuCG", gdvPhieuCG.EditValue)
            AddParameter("@LyDoXuat", tbLyDoXuat.EditValue)
            AddParameter("@IDNgLap", cbNgLap.EditValue)
            AddParameter("@IDNgNhan", cbNgNhan.EditValue)

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                tbSoPhieu.EditValue = _SoPhieu
                tbNgay.EditValue = _ThoiGian
                AddParameter("@SoPhieu", tbSoPhieu.EditValue)
                If doInsert("tblPhieuXKCT") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@SP", tbSoPhieu.EditValue)
                If doUpdate("tblPhieuXKCT", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            With gdvVTCT
                For i As Integer = 0 To .RowCount - 1
                    AddParameter("@SoPhieu", tbSoPhieu.EditValue)
                    AddParameter("@IDVatTu", .GetRowCellValue(i, "IDVatTu"))
                    AddParameter("@SLYC", .GetRowCellValue(i, "SLYC"))
                    AddParameter("@SLXuatKho", .GetRowCellValue(i, "SLXuatKho"))
                    AddParameter("@NgayCan", .GetRowCellValue(i, "NgayCan"))
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))

                    If IsDBNull(.GetRowCellValue(i, "IDXKCT")) Or .GetRowCellValue(i, "IDXKCT") Is Nothing Then
                        Dim _IDCG As Object = doInsert("tblXKCT")
                        If _IDCG Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "IDXKCT", _IDCG)
                    Else
                        AddParameterWhere("@ID", .GetRowCellValue(i, "IDXKCT"))
                        If doUpdate("tblXKCT", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                Next
            End With

            ComitTransaction()

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mXuatKhoCongTrinh" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKhoCT).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKhoCT).gdvCT.FocusedRowHandle = CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuatKhoCT).index
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
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvVTCT.GetFocusedRowCellValue("IDVatTu"), gdvVTCT.GetFocusedRowCellValue("Sophieu"), gdvVTCT.GetFocusedRowCellValue("IDYeuCau"))
            If Not ds Is Nothing Then
                Dim f As New frmThongTinGiaNhap
                f.lbVatTu.Text &= gdvVTCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvVTCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvVTCT.GetFocusedRowCellValue("TenHang")
                f.lbGiaCungUng.Text &= Convert.ToDouble(ds.Tables(0).Rows(0)(0)).ToString("N2")
                f.gdvGiaNhap.DataSource = ds.Tables(1)
                f.gdvChaoGia.DataSource = ds.Tables(2)
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        ElseIf e.Control AndAlso e.KeyCode = Keys.Q Then
            splitChiTiet.Collapsed = False
        ElseIf e.Control AndAlso e.KeyCode = Keys.W Then
            splitChiTiet.Collapsed = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            If ShowCauHoi("Xoá dòng hiện tại ?") Then

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
        'If   = False Then Exit Sub
        gdvChuyenMaCT.CloseEditor()
        gdvChuyenMaCT.UpdateCurrentRow()

        For i As Integer = 0 To gdvChuyenMaCT.RowCount - 1
            If gdvChuyenMaCT.GetRowCellValue(i, "SLYC") > 0 Then
                If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                    gdvVTCT.AddNewRow()
                Else
                    If Not ShowCauHoi("Thay thế vật tư " & gdvVTCT.GetFocusedRowCellValue("Model") & " bằng " & gdvChuyenMaCT.GetRowCellValue(i, "Model") & " ?") Then
                        Exit Sub
                    End If
                End If

                gdvVTCT.SetFocusedRowCellValue("IDVatTu", gdvChuyenMaCT.GetRowCellValue(i, "ID"))
                If Not Convert.ToBoolean(chkThayThe.EditValue) Then
                    gdvVTCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 2, "AZ")) + 1)
                End If
                gdvVTCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetRowCellValue(i, "TenVT"))
                gdvVTCT.SetFocusedRowCellValue("ThongSo", gdvChuyenMaCT.GetRowCellValue(i, "Thongso"))
                gdvVTCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetRowCellValue(i, "Model"))
                gdvVTCT.SetFocusedRowCellValue("SLYC", gdvChuyenMaCT.GetRowCellValue(i, "SLYC"))
                gdvVTCT.SetFocusedRowCellValue("TenDVT", gdvChuyenMaCT.GetRowCellValue(i, "DVT"))
                gdvVTCT.SetFocusedRowCellValue("TenHang", gdvChuyenMaCT.GetRowCellValue(i, "HangSX"))
                gdvVTCT.SetFocusedRowCellValue("slTon", gdvChuyenMaCT.GetRowCellValue(i, "slTon"))
                gdvVTCT.SetFocusedRowCellValue("DangVe", gdvChuyenMaCT.GetRowCellValue(i, "Dangve"))
                gdvVTCT.SetFocusedRowCellValue("NgayVe", gdvChuyenMaCT.GetRowCellValue(i, "Ngayve"))
                gdvVTCT.SetFocusedRowCellValue("CanXuat", gdvChuyenMaCT.GetRowCellValue(i, "Canxuat"))
                gdvVTCT.SetFocusedRowCellValue("SLXuatKho", 0)
                gdvVTCT.SetFocusedRowCellValue("HangTon", gdvChuyenMaCT.GetRowCellValue(i, "HangTon"))
                gdvChuyenMaCT.SetRowCellValue(i, "SLYC", 0)
            End If

        Next

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
        ShowAlert("Đã thêm vật tư chào giá !")
    End Sub


    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then
            pNhapSoLuong.Visible = True
            pNhapSoLuong.Focus()
            If gdvVTCT.FocusedRowHandle >= 0 Then
                tbSL.EditValue = Convert.ToDouble(gdvVTCT.GetFocusedRowCellValue("SoLuong"))
            Else
                tbSL.EditValue = 1.0
            End If
            tbSL.Focus()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvChuyenMaCT.GetFocusedRowCellValue("ID"), -1, -1)
            If Not ds Is Nothing Then
                Dim f As New frmThongTinGiaNhap
                f.lbVatTu.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvChuyenMaCT.GetFocusedRowCellValue("HangSX")
                f.lbGiaCungUng.Text &= Convert.ToDouble(ds.Tables(0).Rows(0)(0)).ToString("N2")
                f.gdvGiaNhap.DataSource = ds.Tables(1)
                f.gdvChaoGia.DataSource = ds.Tables(2)
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            gdvChuyenMaCT.OptionsView.ShowAutoFilterRow = Not gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
            chkDongLocDuLieu.Checked = gdvChuyenMaCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub
#End Region


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        'btfilterTenVT.EditValue = 
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

        Dim tb As DataTable = CType(gdvVT.Views.Item(0).DataSource, DataView).Table
        Dim az As Integer = Convert.ToInt32(gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 1, "AZ"))
        With gdvThamChieuCT
            For i As Integer = 0 To .SelectedRowsCount - 1
                If Not .GetRowCellValue(i, "IDVatTu") Is Nothing And Not IsDBNull(.GetRowCellValue(.GetSelectedRows(i), "IDVatTu")) Then
                    az += 1
                    tb.Rows.Add(tb.NewRow)
                    tb.Rows(tb.Rows.Count - 1)("IDVatTu") = .GetRowCellValue(.GetSelectedRows(i), "IDVatTu")
                    tb.Rows(tb.Rows.Count - 1)("TenVT") = .GetRowCellValue(.GetSelectedRows(i), "TenVT")
                    tb.Rows(tb.Rows.Count - 1)("ThongSo") = .GetRowCellValue(.GetSelectedRows(i), "ThongSo")
                    tb.Rows(tb.Rows.Count - 1)("Model") = .GetRowCellValue(.GetSelectedRows(i), "Model")
                    tb.Rows(tb.Rows.Count - 1)("SLYC") = .GetRowCellValue(.GetSelectedRows(i), "SoLuong")
                    tb.Rows(tb.Rows.Count - 1)("SLXuatKho") = 0
                    tb.Rows(tb.Rows.Count - 1)("TenDVT") = .GetRowCellValue(.GetSelectedRows(i), "TenDVT")
                    tb.Rows(tb.Rows.Count - 1)("TenHang") = .GetRowCellValue(.GetSelectedRows(i), "TenHang")
                    tb.Rows(tb.Rows.Count - 1)("slTon") = .GetRowCellValue(.GetSelectedRows(i), "slTon")
                    tb.Rows(tb.Rows.Count - 1)("DangVe") = .GetRowCellValue(.GetSelectedRows(i), "DangVe")
                    tb.Rows(tb.Rows.Count - 1)("NgayVe") = .GetRowCellValue(.GetSelectedRows(i), "NgayVe")
                    tb.Rows(tb.Rows.Count - 1)("CanXuat") = .GetRowCellValue(.GetSelectedRows(i), "CanXuat")
                    tb.Rows(tb.Rows.Count - 1)("HangTon") = .GetRowCellValue(.GetSelectedRows(i), "HangTon")
                    tb.Rows(tb.Rows.Count - 1)("AZ") = az

                End If
            Next
        End With
        gdvVT.DataSource = tb

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

    Private Sub gdvThamChieuCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvThamChieuCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.N Then
            If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
            Dim ds As DataSet = SqlSelect.Select_ThongTinNhapHang(gdvThamChieuCT.GetFocusedRowCellValue("IDVatTu"), -1, -1)
            If Not ds Is Nothing Then
                Dim f As New frmThongTinGiaNhap
                f.lbVatTu.Text &= gdvThamChieuCT.GetFocusedRowCellValue("TenVT")
                f.lbMaVT.Text &= gdvThamChieuCT.GetFocusedRowCellValue("Model")
                f.lbHang.Text &= gdvThamChieuCT.GetFocusedRowCellValue("TenHang")
                f.lbGiaCungUng.Text &= Convert.ToDouble(ds.Tables(0).Rows(0)(0)).ToString("N2")
                f.gdvGiaNhap.DataSource = ds.Tables(1)
                f.gdvChaoGia.DataSource = ds.Tables(2)
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
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


    Private Sub gdvMaKH_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        On Error Resume Next
        If gdvMaKH.IsPopupOpen Then Exit Sub
        loadDSChaoGia()
    End Sub

    Private Sub gdvMaKH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvMaKH.KeyPress
        If gdvMaKH.IsPopupOpen Then
            Exit Sub
        Else
            gdvMaKH.ShowPopup()
        End If
    End Sub

    Private Sub mLapYCVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLapYCVatTu.ItemClick
        If gdvThamChieuCT.SelectedRowsCount < 0 Then

        End If
    End Sub
End Class