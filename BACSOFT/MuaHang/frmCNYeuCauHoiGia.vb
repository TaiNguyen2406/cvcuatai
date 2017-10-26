Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports SpreadsheetGear
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Xml
Imports System.Globalization
Imports BACSOFT.Utils

Public Class frmCNYeuCauHoiGia
    Public TrangThai As New Utils.TrangThai
    Public tmpTrangThai As New Utils.TrangThai
    Public tmpMaTuDien As Object
    Public SPHoiGia As Object
    Public tbtmpYC As DataTable
    Private EndSelect As Boolean
    Private Move_Next As Boolean
    Private _strIDNCC As String = ""
    Private _strIDNoiDung As String = ""
    Public IDYC As String = ""
    Private _exit As Boolean
    Public _MatKhauEmailGui As String = ""

    Private Sub frmCNChaoGia_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        loadrcbNgd()
        loadChamSoc()
        LoadCbTrangThai()
        LoadCbPhu()

        loadDSTenVT(Nothing, Nothing)
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)

        'loadGdvVatTuChaoGia()

        If TrangThai.isAddNew Then
            Me.Text = "Lập yêu cầu gửi cho nhà cung cấp "
            SPHoiGia = -1
            cbPhuTrach.EditValue = CType(TaiKhoan, Int32)
            gdvListFile.DataSource = DataSourceDSFile2("")
            loadYCCT(-1)
            gdvCT.BeginUpdate()
            For i As Integer = 0 To tbtmpYC.Rows.Count - 1
                gdvCT.AddNewRow()
                gdvCT.SetFocusedRowCellValue("AZ", i + 1)
                gdvCT.SetFocusedRowCellValue("NoiDung", tbtmpYC.Rows(i)("NoiDung"))
                gdvCT.SetFocusedRowCellValue("IDVatTu", tbtmpYC.Rows(i)("IDVatTu"))
                gdvCT.SetFocusedRowCellValue("TenVT", tbtmpYC.Rows(i)("TenVT"))
                gdvCT.SetFocusedRowCellValue("HangSX", tbtmpYC.Rows(i)("HangSX"))
                gdvCT.SetFocusedRowCellValue("Model", tbtmpYC.Rows(i)("Model"))
                gdvCT.SetFocusedRowCellValue("SoLuong", tbtmpYC.Rows(i)("SoLuong"))
                gdvCT.SetFocusedRowCellValue("DVT", tbtmpYC.Rows(i)("DVT"))
                gdvCT.SetFocusedRowCellValue("IDYeuCau", tbtmpYC.Rows(i)("IDYC"))
                gdvCT.SetFocusedRowCellValue("SoPhieuYC", tbtmpYC.Rows(i)("SoPhieu"))
                gdvCT.SetFocusedRowCellValue("ttcMa", tbtmpYC.Rows(i)("ttcMa"))
                gdvCT.SetFocusedRowCellValue("IDTakeCare", tbtmpYC.Rows(i)("IDTakeCare"))
                gdvCT.SetFocusedRowCellValue("Modify", True)
                gdvCT.SetFocusedRowCellValue("ModifyIDVT", False)
            Next
            gdvCT.EndUpdate()
        ElseIf TrangThai.isUpdate Then
            Me.Text = "Cập nhật yêu cầu gửi cho nhà cung cấp "
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,NgayLap,IDPhuTrach,NgayGui,NgayPhanHoi,NgayHuy,TrangThai,DaGui,TieuDe,NoiDung,FileDinhKem,IDNguoiLap,IDNguoiSua FROM tblYeuCauHoiGia WHERE SoPhieu=" & SPHoiGia)
            If Not tb Is Nothing Then
                tbSoPhieu.EditValue = tb.Rows(0)("SoPhieu")
                tbNgay.EditValue = tb.Rows(0)("NgayLap")
                tbNgayGui.EditValue = tb.Rows(0)("NgayGui")
                tbNgayHuy.EditValue = tb.Rows(0)("NgayHuy")
                tbNgayPhanHoi.EditValue = tb.Rows(0)("NgayPhanHoi")
                cbPhuTrach.EditValue = tb.Rows(0)("IDPhuTrach")
                tbTieuDe.EditValue = tb.Rows(0)("TieuDe")
                tbNoiDung.EditValue = tb.Rows(0)("NoiDung")
                cbTrangThai.EditValue = Convert.ToByte(tb.Rows(0)("TrangThai"))
                gdvListFile.DataSource = DataSourceDSFile2(tb.Rows(0)("FileDinhKem"))
                loadYCCT(tb.Rows(0)("SoPhieu"))
            End If
        End If

        loadgdvNCC()

    End Sub

#Region "Chuyển mã"
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

    Private Sub rtbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub rtbThongSo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThongSo.ButtonClick
        btFilterThongSo.EditValue = Nothing
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    'Private Sub btTaiLai_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
    '    If chkTaiAnh.Checked Then
    '        colHinhAnh.Visible = True
    '    Else
    '        colHinhAnh.Visible = False
    '    End If
    '    LoadDSVatTuDungChuyenMa()
    'End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub LoadDSVatTuDungChuyenMa()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sqlWhere As String = " WHERE Maloi=0 "

        Dim sql As String = " Select NULL AS CanhBao,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.Model,VATTU.Thongso,VATTU.ID,VATTU.IDDonvitinh AS IDDVT,TENDONVITINH.Ten AS DVT,TENNHOM.Ten AS NhomVT,TENNHOM.Ten_ENG AS TenNhom_ENG, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS slTon, "
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

        If Not btFilterMaVT.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
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

        If Not btFilterThongSo.EditValue Is Nothing Then
            sqlWhere &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If

        sql &= sqlWhere
        sql &= " ORDER BY TenVT,HangSX,Model"

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

            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb)"
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
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb)"
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
            sqltb &= " SELECT TOP 1 IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btTaiDSMaVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadDSVatTuDungChuyenMa()
    End Sub


#End Region

#Region "Load dữ liệu cho các combobox"

    Public Sub LoadCbPhu()
        Dim sql As String = ""
        sql &= " SELECT Ma,NoiDung FROm tblTuDien WHERE Loai=@ThoiGianCungUng "
        sql &= " SELECT ID,Ten FROM tblTienTe"
        AddParameterWhere("@ThoiGianCungUng", LoaiTuDien.TGCungUng)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbThoiGianGH.DataSource = ds.Tables(0)
            rcbTienTe.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Public Sub loadrcbNgd()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT NHANSU.ID,NHANSU.Ten,KHACHHANG.ttcMa,KHACHHANG.ID AS IDKH,NHANSU.Email FROM NHANSU INNER JOIN KHACHHANG ON NHANSU.Noictac=KHACHHANG.ID AND KHACHHANG.ID <>74 AND (KHACHHANG.ttcKhachHang = 0 OR KHACHHANG.ttcKhachHang =2)")
        If Not tb Is Nothing Then
            rcbNgd.View.Columns.Clear()
            rcbNgd.DataSource = tb
            With rcbNgd.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                colID.Width = 80

                Dim colIDKD = .AddField("IDKH")
                colIDKD.Caption = "IDKD"
                colIDKD.Visible = False
                colIDKD.Width = 80

                Dim colTenKH = .AddField("ttcMa")
                colTenKH.Caption = "Mã KH"
                colTenKH.GroupIndex = 0
                colTenKH.VisibleIndex = 1
                colTenKH.Width = 120
                colTenKH.OptionsColumn.FixedWidth = True
                colTenKH.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colTen = .AddField("Ten")
                colTen.Caption = "Họ tên"
                colTen.VisibleIndex = 2
                colTen.Width = 170
                colTen.OptionsColumn.FixedWidth = True
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

                Dim colEmail = .AddField("Email")
                colEmail.Caption = "Email"
                colEmail.VisibleIndex = 3
                colEmail.Width = 170
                colEmail.OptionsColumn.FixedWidth = True
                colEmail.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

            End With
            rcbNgd.View.ExpandAllGroups()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadgdvNCC()
        AddParameterWhere("@SP", SPHoiGia)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,IDNCC,IDNgd,(SELECT Email FROM NHANSU WHERE NHANSU.ID=tblNCCHoiGia.IDNgd) Email,(SELECT ttcMa FROM KHACHHANG WHERE KHACHHANG.ID=tblNCCHoiGia.IDNCC)ttcMa FROM tblNCCHoiGia WHERE SoPhieu = @SP ")
        If Not tb Is Nothing Then
            gdvNCC.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadChamSoc()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 AND TrangThai=1")
        If Not tb Is Nothing Then
            cbPhuTrach.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadCbTrangThai()
        AddParameter("@Loai", LoaiTuDien.YeuCauHoiGia)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If Not tb Is Nothing Then
            cbTrangThai.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadYCCT(ByVal SoPhieu As Object)
        Dim sql As String = ""
        sql &= " SELECT tblYeuCauHoiGiaCT.ID, tblYeuCauHoiGiaCT.SoPhieu, tblYeuCauHoiGiaCT.NoiDung, tblYeuCauHoiGiaCT.SoLuong, tblYeuCauHoiGiaCT.IDVatTu, tblYeuCauHoiGiaCT.AZ,"
        sql &= " VATTU.Model, TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, TENDONVITINH.Ten AS DVT,tblYeuCauHoiGiaCT.IDYeuCau, YEUCAUDEN.GiaCungUng,YEUCAUDEN.IDTienTeCungUng,YEUCAUDEN.TGCungUng,YEUCAUDEN.HoiThongTin,convert(bit,0)Modify,YEUCAUDEN.SoPhieu AS SoPhieuYC,KHACHHANG.ttcMa,BANGYEUCAU.IDTakeCare,convert(bit,0)ModifyIDVT"
        sql &= " FROM tblYeuCauHoiGiaCT"
        sql &= " LEFT JOIN YEUCAUDEN ON YEUCAUDEN.ID=tblYeuCauHoiGiaCT.IDYeuCau "
        sql &= " LEFT JOIN BANGYEUCAU ON YEUCAUDEN.SoPhieu=BANGYEUCAU.SoPhieu "
        sql &= " LEFT JOIN KHACHHANG ON BANGYEUCAU.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN VATTU ON tblYeuCauHoiGiaCT.IDVatTu = VATTU.ID "
        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenvattu = TENVATTU.ID "
        sql &= " LEFT JOIN TENDONVITINH ON VATTU.IDDonViTinh = TENDONVITINH.ID "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat = TENHANGSANXUAT.ID"
        sql &= " WHERE tblYeuCauHoiGiaCT.SoPhieu=" & SoPhieu
        sql &= " ORDER BY tblYeuCauHoiGiaCT.AZ "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

#Region "Lưu lại"

    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click

        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        gdvNCCCT.CloseEditor()
        gdvNCCCT.UpdateCurrentRow()
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
        Dim tg As DateTime = GetServerTime()

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            tbSoPhieu.EditValue = LaySoPhieu("tblYeuCauHoiGia")
            tbNgay.EditValue = tg
        End If

        Try
            BeginTransaction()
            AddParameter("@SoPhieu", tbSoPhieu.EditValue)
            AddParameter("@NgayLap", tbNgay.EditValue)
            AddParameter("@IDPhuTrach", cbPhuTrach.EditValue)
            AddParameter("@Trangthai", cbTrangThai.EditValue)
            AddParameter("@FileDinhKem", gdv2String(gdvListFileCT))
            AddParameter("@TieuDe", tbTieuDe.EditValue)
            AddParameter("@NoiDung", tbNoiDung.EditValue)

            If cbTrangThai.EditValue = TrangThaiYeuCauHoiGia.ChoPhanHoi Then
                AddParameter("@NgayPhanHoi", DBNull.Value)
                AddParameter("@NgayHuy", DBNull.Value)
            ElseIf cbTrangThai.EditValue = TrangThaiYeuCauHoiGia.DaBaoGia Then
                AddParameter("@NgayPhanHoi", tbNgayPhanHoi.EditValue)
                AddParameter("@NgayHuy", DBNull.Value)
            ElseIf cbTrangThai.EditValue > TrangThaiYeuCauHoiGia.DaBaoGia Then
                AddParameter("@NgayPhanHoi", tbNgayPhanHoi.EditValue)
                AddParameter("@NgayHuy", DBNull.Value)
            End If

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                AddParameter("@IDNguoiLap", Convert.ToInt32(TaiKhoan))
                'AddParameter("@Masodathang", SPHoiGia)
                If doInsert("tblYeuCauHoiGia") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Else
                AddParameter("@IDNguoiSua", Convert.ToInt32(TaiKhoan))
                AddParameterWhere("@SP", tbSoPhieu.EditValue)
                If doUpdate("tblYeuCauHoiGia", "Sophieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            With gdvNCCCT
                For i As Integer = 0 To .RowCount - 2

                    AddParameter("@IDNCC", .GetRowCellValue(i, "IDNCC"))
                    AddParameter("@IDNgd", .GetRowCellValue(i, "IDNgd"))
                    AddParameter("@SoPhieu", tbSoPhieu.EditValue)

                    If IsDBNull(.GetRowCellValue(i, "ID")) Or .GetRowCellValue(i, "ID") Is Nothing Then
                        Dim _ID As Object = doInsert("tblNCCHoiGia")
                        If _ID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "ID", _ID)
                    Else
                        AddParameterWhere("@ID", .GetRowCellValue(i, "ID"))
                        If doUpdate("tblNCCHoiGia", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next
            End With

            If _strIDNCC <> "" Then
                If doDelete("tblNCCHoiGia", "ID IN (" & _strIDNCC.Substring(0, _strIDNCC.Length - 1) & ")") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _strIDNCC = ""
            End If

            With gdvCT
                For i As Integer = 0 To .RowCount - 1
                    AddParameter("@SoPhieu", tbSoPhieu.EditValue)
                    AddParameter("@NoiDung", .GetRowCellValue(i, "NoiDung"))
                    AddParameter("@IDVatTu", .GetRowCellValue(i, "IDVatTu"))
                    AddParameter("@SoLuong", .GetRowCellValue(i, "SoLuong"))
                    AddParameter("@IDYeuCau", .GetRowCellValue(i, "IDYeuCau"))
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))

                    If IsDBNull(.GetRowCellValue(i, "ID")) Or .GetRowCellValue(i, "ID") Is Nothing Then
                        Dim _ID As Object = doInsert("tblYeuCauHoiGiaCT")
                        If _ID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "ID", _ID)
                    Else
                        AddParameterWhere("@ID", .GetRowCellValue(i, "ID"))
                        If doUpdate("tblYeuCauHoiGiaCT", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                    If .GetRowCellValue(i, "ModifyIDVT") Then
                        AddParameter("@IDVatTu", .GetRowCellValue(i, "IDVatTu"))
                        AddParameter("@IDChuyenMa", TaiKhoan)
                        AddParameter("@NgayChuyenMa", tg)
                        AddParameterWhere("@IDYC", .GetRowCellValue(i, "IDYeuCau"))
                        If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            .SetRowCellValue(i, "ModifyIDVT", False)
                            AddParameter("@NoiDung", "Đã chuyển mã YC: " & .GetRowCellValue(i, "SoPhieuYC") & " KH: " & .GetRowCellValue(i, "ttcMa") & vbCrLf & " - nội dung yêu cầu: " & .GetRowCellValue(i, "NoiDung"))
                            AddParameter("@ThoiGian", tg)
                            AddParameter("@IDNhanVien", .GetRowCellValue(i, "IDTakeCare"))
                            If doInsert("tblThongBao") Is Nothing Then
                                ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
                            End If
                        End If

                    End If

                Next
            End With

            If _strIDNoiDung <> "" Then
                If doDelete("tblYeuCauHoiGiaCT", "ID IN (" & _strIDNoiDung.Substring(0, _strIDNoiDung.Length - 1) & ")") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _strIDNoiDung = ""
            End If

            ComitTransaction()

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = deskTop.mYeuCauHoiGiaNCC.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).gdvCT.FocusedRowHandle = CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).index
                End If
            Next

            ShowAlert(Me.Text & " thành công!")
            TrangThai.isUpdate = True
            Me.Text = "Cập nhật yêu cầu gửi cho nhà cung cấp "
        Catch ex As Exception
            RollBackTransaction()
            If TrangThai.isAddNew Then
                tbSoPhieu.EditValue = ""
                tbNgay.EditValue = DBNull.Value
                gdvNCCCT.BeginUpdate()
                For i As Integer = 0 To gdvNCCCT.RowCount - 2
                    gdvNCCCT.SetRowCellValue(i, "ID", DBNull.Value)
                Next
                gdvNCCCT.EndUpdate()
            End If
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

#End Region


#Region "Các sự kiện của grid vật tư chào giá gdvVTCT"

    Private Sub gdvVTCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvCT.FocusedRowHandle = 0 Then Exit Sub
            Dim _tmp As Object = gdvCT.GetFocusedRowCellValue("AZ")
            gdvCT.SetFocusedRowCellValue("AZ", gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle - 1, "AZ"))
            gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvCT.FocusedRowHandle = gdvCT.RowCount - 1 Then Exit Sub
            Dim _tmp As Object = gdvCT.GetFocusedRowCellValue("AZ")
            gdvCT.SetFocusedRowCellValue("AZ", gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle + 1, "AZ"))
            gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvCT.FocusedRowHandle -= 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng được chọn ?") Then
                If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) And Not gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                    _strIDNoiDung &= gdvCT.GetFocusedRowCellValue("ID") & ","
                End If
                gdvCT.DeleteSelectedRows()
                gdvCT.BeginUpdate()
                For i As Integer = 0 To gdvCT.RowCount - 1
                    gdvCT.SetRowCellValue(i, "AZ", i + 1)
                Next

                gdvCT.EndUpdate()

            End If

        End If
    End Sub



#End Region


#Region "Thay đổi trạng thái"
    Private Sub cbTrangThai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTrangThai.EditValueChanged
        If cbTrangThai.EditValue = TrangThaiYeuCauHoiGia.ChoPhanHoi Then
            tbNgayPhanHoi.Enabled = False
            tbNgayHuy.Enabled = False
        ElseIf cbTrangThai.EditValue = TrangThaiYeuCauHoiGia.DaBaoGia Then
            tbNgayPhanHoi.Enabled = True
            tbNgayHuy.Enabled = False
            tbNgayPhanHoi.EditValue = GetServerTime()
        ElseIf cbTrangThai.EditValue > TrangThaiYeuCauHoiGia.DaBaoGia Then
            tbNgayHuy.Enabled = True
            tbNgayPhanHoi.Enabled = False
            tbNgayHuy.EditValue = GetServerTime()
        End If

    End Sub


#End Region

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

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If tbSoPhieu.EditValue = "" Then
            ShowCanhBao("Cần phải lưu lại trước khi chọn file đính kèm")
            Exit Sub
        End If
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang) Then
                System.IO.Directory.CreateDirectory(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang)
            End If
            For Each file In openFile.FileNames
                Try
                    path = tbSoPhieu.EditValue & " " & TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                            gdvListFileCT.SetFocusedRowCellValue("Check", False)
                        End If
                    Else
                        System.IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                        gdvListFileCT.SetFocusedRowCellValue("Check", False)
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
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        Try
            If ShowCauHoi("Xoá file được chọn ?") Then
                Try
                    Dim Impersonator As New Impersonator()
                    Impersonator.BeginImpersonation()
                    System.IO.File.Delete(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang & gdvListFileCT.GetFocusedRowCellValue("File"))
                    Impersonator.EndImpersonation()
                Catch ex As Exception
                    ShowCanhBao(ex.Message)
                End Try

                gdvListFileCT.DeleteSelectedRows()
            End If

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub btFileDinhKem_Click(sender As System.Object, e As System.EventArgs) Handles btFileDinhKem.Click
        gListFileCT.Visible = True
    End Sub


    Private Sub gdvVTCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        On Error Resume Next
        If e.Column.FieldName = "LoiGia" Then
            If e.CellValue = 1 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub


    Private Sub btAnDSFile_Click(sender As System.Object, e As System.EventArgs) Handles btAnDSFile.Click
        gListFileCT.Visible = False
    End Sub

    'Private Sub btFileLienQuan_Click(sender As System.Object, e As System.EventArgs) Handles btFileLienQuan.Click
    '    Dim f As New frmFileLienQuan
    '    f.Tag = Me.Tag
    '    f.SoChaoGia = SPChaoGia
    '    f.SoYeuCau = SPHoiGia
    '    f.MaKH = gdvMaNCC.Text
    '    f.ShowDialog()
    'End Sub

    Private Sub btTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub


    Private Sub rcbNgd_Popup(sender As System.Object, e As System.EventArgs) Handles rcbNgd.Popup
        rcbNgd.View.ExpandAllGroups()
    End Sub

    Private Sub rcbNgd_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles rcbNgd.EditValueChanged
        Dim edit As DevExpress.XtraEditors.GridLookUpEdit = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)
        Dim row As DataRowView = edit.GetSelectedDataRow()

        gdvNCCCT.SetFocusedRowCellValue("ttcMa", row("ttcMa"))
        gdvNCCCT.SetFocusedRowCellValue("Email", row("Email"))
        gdvNCCCT.SetFocusedRowCellValue("IDNCC", row("IDKH"))
    End Sub

    Private Sub gdvNCCCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvNCCCT.RowCellClick
        If e.Column.FieldName = "IDNgd" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub gdvNCCCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNCCCT.KeyDown
        If gdvNCCCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xóa dòng được chọn ?") Then
            If Not IsDBNull(gdvNCCCT.GetFocusedRowCellValue("ID")) And Not gdvNCCCT.GetFocusedRowCellValue("ID") Is Nothing Then
                _strIDNCC &= gdvNCCCT.GetFocusedRowCellValue("ID") & ","
            End If
            gdvNCCCT.DeleteSelectedRows()
        End If
    End Sub

    Private Sub btGuiMail_Click(sender As System.Object, e As System.EventArgs) Handles btGuiMail.Click
        btGhi.PerformClick()
        Dim sql As String = ""
        sql &= " select KHACHHANG.ttcMa,KHACHHANG.Ten AS TenNCC, KHACHHANG.ttcDienThoai,KHACHHANG.ttcFax,"
        sql &= " 	NHANSU.Ten AS TenNgd,NHANSU.Email,NHANSU.Mobile,NHANSU.Email"
        sql &= " FROM tblNCCHoiGia"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=tblNCCHoiGia.IDNCC"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblNCCHoiGia.IDNgd"
        sql &= " WHERE tblNCCHoiGia.SoPhieu=" & tbSoPhieu.EditValue
        sql &= " SELECT Ten, Mobile,Email FROM NHANSU WHERE ID=" & cbPhuTrach.EditValue
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        If ds.Tables(0).Rows.Count = 0 Then
            ShowCanhBao("Chưa chọn nhà cung cấp nào !")
            Exit Sub
        End If

        Dim f As New frmXacNhanMatKhauEmail
        f.Text = "Nhập mật khẩu email: " & ds.Tables(1).Rows(0)("Email").ToString
        If f.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Try
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dsFile As DataTable = DataSourceDSFile(Utils.XuatExcel.CreateExcelFileYeuCauGuiNCC(tbSoPhieu.EditValue.ToString, CType(gdv.DataSource, DataTable), ds.Tables(0).Rows(i), RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang, ds.Tables(1).Rows(0)("Ten").ToString, ds.Tables(1).Rows(0)("Mobile").ToString, ds.Tables(1).Rows(0)("Email").ToString))
                    For j As Integer = 0 To gdvListFileCT.DataRowCount - 1
                        If gdvListFileCT.GetRowCellValue(j, "Check") Then
                            Dim r As DataRow = dsFile.NewRow
                            r("File") = RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlHoiHang & gdvListFileCT.GetRowCellValue(j, "File")
                            dsFile.Rows.Add(r)
                        End If
                    Next

                    Utils.Email.Send(ds.Tables(0).Rows(i)("Email").ToString, tbTieuDe.EditValue.ToString, tbNoiDung.EditValue.ToString.Replace(Chr(10), "<br/>"), dsFile, ds.Tables(1).Rows(0)("Email").ToString, _MatKhauEmailGui)
                Next
                Dim tg As DateTime = GetServerTime()
                AddParameter("@DaGui", True)
                AddParameter("@NgayGui", tg)
                AddParameter("@TrangThai", 0)
                AddParameterWhere("@SP", tbSoPhieu.EditValue)
                If doUpdate("tblYeuCauHoiGia", "SoPhieu=@SP") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Dim _strIDNoiDungYC As String = ""
                For i As Integer = 0 To gdvCT.DataRowCount - 1
                    _strIDNoiDungYC &= gdvCT.GetRowCellValue(i, "IDYeuCau") & ","
                Next
                _strIDNoiDungYC = _strIDNoiDungYC.Substring(0, _strIDNoiDungYC.Length - 1)


                AddParameter("@IDNhanBaoGia", TaiKhoan)
                AddParameter("@NgayNhanHoiGia", tg)
                'AddParameterWhere("@IDYC", "(" & _strIDYeuCau & ")")
                If doUpdate("YEUCAUDEN", "ID IN (" & _strIDNoiDungYC & ")") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                With gdvCT
                    For i As Integer = 0 To .RowCount - 1
                        AddParameter("@NoiDung", "Đã gửi mail hỏi giá NCC cho YC: " & .GetRowCellValue(i, "SoPhieuYC") & " KH: " & .GetRowCellValue(i, "ttcMa") & vbCrLf & " - nội dung yêu cầu: " & .GetRowCellValue(i, "NoiDung"))
                        AddParameter("@ThoiGian", tg)
                        AddParameter("@IDNhanVien", .GetRowCellValue(i, "IDTakeCare"))
                        If doInsert("tblThongBao") Is Nothing Then
                            ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
                        End If
                    Next
                End With

                tbNgayGui.EditValue = tg
                cbTrangThai.EditValue = Convert.ToByte(0)

                For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                    If deskTop.tabMain.TabPages(i).Tag = deskTop.mYeuCauHoiGiaNCC.Name Then
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).LoadDS()
                        If CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).index <> Nothing Then
                            CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).gdvCT.FocusedRowHandle = CType(deskTop.tabMain.TabPages(i).Controls(0), frmYeuCauHoiGia).index
                        End If
                    End If
                Next
                Impersonator.EndImpersonation()
            Catch ex As Exception
                'Impersonator.EndImpersonation()
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub

    Private Sub btCapNhatGiaChoKD_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatGiaChoKD.Click
        If ShowCauHoi("Xác nhận cập nhật giá báo cho kinh doanh ?") Then
            Dim _count = 0
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            Dim tg As DateTime = GetServerTime()
            With gdvCT
                For i As Integer = 0 To .RowCount - 1


                    If .GetRowCellValue(i, "Modify") Then
                        If (Not IsDBNull(.GetRowCellValue(i, "ID")) And Not .GetRowCellValue(i, "ID") Is Nothing) And (.GetRowCellValue(i, "HoiThongTin").ToString.Trim <> "" Or .GetRowCellValue(i, "GiaCungUng").ToString.Trim <> "" Or .GetRowCellValue(i, "TGCungUng").ToString.Trim <> "") Then
                            AddParameter("@GiaCungUng", .GetRowCellValue(i, "GiaCungUng"))
                            AddParameter("@IDTienTeCungUng", .GetRowCellValue(i, "IDTienTeCungUng"))
                            AddParameter("@TGCungUng", .GetRowCellValue(i, "TGCungUng"))
                            AddParameter("@HoiThongTin", .GetRowCellValue(i, "HoiThongTin"))
                            AddParameter("@NgayHoiGia", tg)
                            AddParameter("@IDHoiGia", TaiKhoan)
                            AddParameter("@TrangThai", TrangThaiYeuCau.CanChaoGia)
                            AddParameterWhere("@ID", .GetRowCellValue(i, "IDYeuCau"))
                            If doUpdate("YEUCAUDEN", "ID=@ID") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            Else
                                _count += 1
                                .SetRowCellValue(i, "Modify", False)
                                AddParameter("@NoiDung", "Đã báo giá yêu cầu: " & .GetRowCellValue(i, "SoPhieuYC") & " KH: " & .GetRowCellValue(i, "ttcMa") & vbCrLf & " - nội dung yêu cầu: " & .GetRowCellValue(i, "NoiDung"))
                                AddParameter("@ThoiGian", tg)
                                AddParameter("@IDNhanVien", .GetRowCellValue(i, "IDTakeCare"))
                                If doInsert("tblThongBao") Is Nothing Then
                                    ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
                                End If
                            End If
                        End If
                    End If
                Next
            End With
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            If _count > 0 Then
                ShowThongBao("Đã cập nhật giá cho " & _count.ToString & " mục hỏi giá !")
                For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                    If deskTop.tabMain.TabPages(i).Tag = deskTop.mXuLyYeuCauHoiGia.Name Then
                        CType(deskTop.tabMain.TabPages(i).Controls(0), frmXuLyYeuCau).LoadYeuCau()
                    End If
                Next
            Else
                ShowThongBao("Chưa có báo giá nào!")
            End If

        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        Select Case e.Column.FieldName
            Case "GiaCungUng", "TGCungUng", "IDTGCungUng", "HoiThongTin"
                gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
                gdvCT.CloseEditor()
                gdvCT.UpdateCurrentRow()
        End Select
    End Sub

    Private Sub gdvChuyenMaCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChuyenMaCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Enter Then

        End If
    End Sub

    Private Sub gdvChuyenMaCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvChuyenMaCT.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdvChuyenMa.PointToScreen(e.Location))
            mChuyenMa.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If gdv.Focus Then
            HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        Else
            HitInfo = gdvChuyenMaCT.CalcHitInfo(gdvChuyenMa.PointToClient(Cursor.Position))
        End If


        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub mChuyenMa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenMa.ItemClick
        If ShowCauHoi("Chuyển mã vật tư " & gdvChuyenMaCT.GetFocusedRowCellValue("Model") & " cho yêu cầu đang được chọn ?") Then

            'Dim tg As DateTime = GetServerTime()
            'AddParameter("@IDVatTu", gdvChuyenMaCT.GetFocusedRowCellValue("ID"))
            'AddParameter("@IDChuyenMa", TaiKhoan)
            'AddParameter("@NgayChuyenMa", tg)
            'AddParameterWhere("@IDYC", gdvYCCT.GetFocusedRowCellValue("IDYC"))
            'If doUpdate("YEUCAUDEN", "ID=@IDYC") Is Nothing Then
            '    ShowBaoLoi(LoiNgoaiLe)
            'Else
            '    gdvYCCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetFocusedRowCellValue("TenVT"))
            '    gdvYCCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetFocusedRowCellValue("Model"))
            '    gdvYCCT.SetFocusedRowCellValue("HangSX", gdvChuyenMaCT.GetFocusedRowCellValue("HangSX"))
            '    gdvYCCT.SetFocusedRowCellValue("NgayChuyenma", tg)
            '    gdvYCCT.SetFocusedRowCellValue("NguoiXuLy", NguoiDung)
            '    gdvYCCT.CloseEditor()
            '    gdvYCCT.UpdateCurrentRow()
            '    ShowAlert("Đã thực hiện !")
            'End If
            gdvCT.SetFocusedRowCellValue("IDVattu", gdvChuyenMaCT.GetFocusedRowCellValue("ID"))
            gdvCT.SetFocusedRowCellValue("TenVT", gdvChuyenMaCT.GetFocusedRowCellValue("TenVT"))
            gdvCT.SetFocusedRowCellValue("Model", gdvChuyenMaCT.GetFocusedRowCellValue("Model"))
            gdvCT.SetFocusedRowCellValue("DVT", gdvChuyenMaCT.GetFocusedRowCellValue("DVT"))
            gdvCT.SetFocusedRowCellValue("HangSX", gdvChuyenMaCT.GetFocusedRowCellValue("HangSX"))
            gdvCT.SetFocusedRowCellValue("ModifyIDVT", True)
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            ShowAlert("Đã chuyển mã")
        End If
    End Sub

    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Tag
        If gdv.Focus Then
            f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        Else
            f._IDVatTu = gdvChuyenMaCT.GetFocusedRowCellValue("ID")
        End If


        f.ShowDialog()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
            mChuyenMa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub
End Class