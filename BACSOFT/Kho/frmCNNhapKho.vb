Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmCNNhapKho
    Public PhieuNK As Object
    Public strID As String = ""

    Private Sub frmCNNhapKho_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadKhachHang()
        loadNgNhap()
        loadTienTe()
        If TrangThai.isAddNew Then
            Dim sql As String = ""
            sql &= " SELECT row_number() over (order by NHAPKHO.ID) AS AZ,TENNUOC.Ten AS XuatXu, NHAPKHO.ID,NHAPKHO.SoPhieu,IDVatTu,VATTU.Model,VATTU.HangTon,"
            sql &= " TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.ThongSo,TENDONVITINH.Ten AS DVT,SoLuong,DonGia,(SoLuong*DonGia)ThanhTien,NHAPKHO.TienTe,"
            sql &= " NHAPKHO.MucThue,NHAPKHO.NhapThue,IDDatHang,NHAPKHO.ModifyID,NHAPKHO.ModifyDate,NHAPKHO.SoHDGTGT,NHAPKHO.NgayHDGTGT,NHAPKHO.ChiPhi "
            sql &= " FROM NHAPKHO "
            sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
            sql &= " INNER JOIN VATTU ON VATTU.ID=NHAPKHO.IDVatTu "
            sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu "
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
            sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonViTinh"
            sql &= " LEFT JOIN TENNUOC ON TENNUOC.ID=VATTU.IDTenNuoc"
            sql &= " WHERE NHAPKHO.SoPhieu=-1"

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing Then
                gdvVT.DataSource = dt
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

            tabXK.SelectedTabPage = tabThamChieuDH
            cbNguoiXuat.EditValue = Convert.ToInt32(TaiKhoan)
            tbNgay.EditValue = GetServerTime()
            If strID <> "" Then
                Dim _s() As String = strID.Split(",")
                gdvThamChieuCT.BeginUpdate()
                For i As Integer = 0 To _s.Length - 1
                    For j As Integer = 0 To gdvThamChieuCT.RowCount - 1
                        If gdvThamChieuCT.GetRowCellValue(j, "IDDH") = _s(i) Then
                            gdvThamChieuCT.SetRowCellValue(j, "Chon", True)
                        End If
                    Next
                Next
                gdvThamChieuCT.EndUpdate()
                ' btChuyenXK.PerformClick()
            End If



            ' tbNguoiLap.EditValue = 
        Else
            Dim sql As String = " SET DATEFORMAT DMY"
            sql &= " SELECT NgayThang,PHIEUNHAPKHO.SoPhieu,Kho,IDKhachHang,SoPhieuDH,IDUser,TienTe,TyGia,TienTruocThue,TienThue,IDNguoiDat FROM PHIEUNHAPKHO "
            sql &= " WHERE SoPhieu=@PhieuNK"

            sql &= " SELECT ISNULL(NHAPKHO.AZ,row_number() over (order by NHAPKHO.ID)) AS AZ,TENNUOC.Ten AS XuatXu, NHAPKHO.ID,NHAPKHO.SoPhieu,NHAPKHO.IDVatTu,"
            sql &= " VATTU.Model,VATTU.HangTon,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.ThongSo,TENDONVITINH.Ten AS DVT,NHAPKHO.SoLuong,"
            sql &= " NHAPKHO.DonGia,(NHAPKHO.SoLuong*NHAPKHO.DonGia)ThanhTien,NHAPKHO.ChiPhi,NHAPKHO.TienTe,NHAPKHO.MucThue,NHAPKHO.NhapThue,IDDatHang,NHAPKHO.ModifyID,NHAPKHO.ModifyDate,NHAPKHO.SoHDGTGT,NHAPKHO.NgayHDGTGT"
            sql &= " FROM NHAPKHO "
            sql &= " INNER JOIN VATTU ON VATTU.ID=NHAPKHO.IDVatTu "
            sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu "
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
            sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonViTinh"
            sql &= " LEFT JOIN TENNUOC ON TENNUOC.ID=VATTU.IDTenNuoc"
            sql &= " WHERE NHAPKHO.SoPhieu=@PhieuNK ORDER BY AZ,ID"

            sql &= " SELECT PHIEUDATHANG.SoPhieu,PHIEUDATHANG.TienTruocThue,PHIEUDATHANG.TienThue,PHIEUDATHANG.TienTe,PHIEUDATHANG.TyGia,KHACHHANG.ttcMa"
            sql &= " FROM PHIEUDATHANG INNER JOIN KHACHHANG ON KHACHHANG.ID=PHIEUDATHANG.IDKhachHang "
            sql &= " WHERE PHIEUDATHANG.SoPhieu=(SELECT DISTINCT SoPhieuDH FROM PHIEUNHAPKHO WHERE PHIEUNHAPKHO.SoPhieu=@PhieuNK)"
            AddParameterWhere("@PhieuNK", PhieuNK)
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                gdvVT.DataSource = ds.Tables(1)
                gdvMaKH.EditValue = ds.Tables(0).Rows(0)("IDKhachHang")

                tbKhoXuat.EditValue = ds.Tables(0).Rows(0)("Kho")
                tbSoPhieu.EditValue = ds.Tables(0).Rows(0)("SoPhieu")
                tbNgay.EditValue = ds.Tables(0).Rows(0)("NgayThang")

                tbTienTruocThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue")
                tbTienThue.EditValue = ds.Tables(0).Rows(0)("TienThue")
                gdvPhieuDH.Properties.DataSource = ds.Tables(2)
                gdvPhieuDH.EditValue = ds.Tables(0).Rows(0)("SoPhieuDH")

                cbTienTe.EditValue = ds.Tables(0).Rows(0)("TienTe")
                tbTyGia.EditValue = ds.Tables(0).Rows(0)("TyGia")
                cbNguoiXuat.EditValue = ds.Tables(0).Rows(0)("IDUser")
                cbNguoiDat.EditValue = ds.Tables(0).Rows(0)("IDNguoiDat")

                LoadThamChieu(ds.Tables(0).Rows(0)("SoPhieuDH"))

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If


            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                colSoLuong.OptionsColumn.ReadOnly = True
                gdvPhieuDH.Enabled = False
                gdvMaKH.Enabled = False
                btCapNhatGia.Visible = False
            Else
                colDonGia.OptionsColumn.ReadOnly = False
            End If

        End If

    End Sub

    Public Sub loadNgNhap()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not tb Is Nothing Then
            cbNguoiXuat.Properties.DataSource = tb
            cbNguoiDat.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadPhieuCG(ByVal IDKhachHang As Object)
        AddParameterWhere("@IDKH", IDKhachHang)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,TienTruocThue,TienThue,TienTe,TyGia,NHANSU.Ten AS TakeCare,PHIEUDATHANG.IDTakeCare FROM PHIEUDATHANG LEFT JOIN NHANSU ON NHANSU.ID=PHIEUDATHANG.IDTakeCare WHERE IDKhachHang=@IDKH AND PHIEUDATHANG.PheDuyet=1 ORDER BY SoPhieu DESC")
        If Not tb Is Nothing Then
            gdvPhieuDH.Properties.DataSource = tb
        End If
    End Sub

    Public Sub LoadThamChieu(ByVal SoPhieuCG As Object)
        Dim sql As String = ""
        AddParameterWhere("@SP", SoPhieuCG)
        sql &= "  SELECT DATHANG.ID AS IDDH,Convert(bit,0)Chon, DATHANG.IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT,CanNhap AS SoLuong,DATHANG.SoLuong AS SLDat,DATHANG.DonGia,DATHANG.MucThue,DATHANG.NhapThue,"
        sql &= " VATTU.IDDonViTinh,TENNUOC.Ten AS XuatXu,0 AS AZ,ISNULL(DATHANG.ChiPhi,0)ChiPhi"
        sql &= " FROM DATHANG "
        sql &= " LEFT OUTER JOIN VATTU ON DATHANG.IDvattu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENNUOC ON VATTU.IDTenNuoc=TENNUOC.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE DATHANG.Sophieu=@SP"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("AZ") = i + 1
            Next
            gdvThamChieu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub

    Public Sub loadNhapKho(ByVal _PhieuNK As Object)
        Dim sql As String = " SET DATEFORMAT DMY"
        sql &= " SELECT ISNULL(NHAPKHO.AZ,row_number() over (order by NHAPKHO.ID)) AS AZ,TENNUOC.Ten AS XuatXu, NHAPKHO.ID,NHAPKHO.SoPhieu,NHAPKHO.IDVatTu,"
        sql &= " VATTU.Model,VATTU.HangTon,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,VATTU.ThongSo,TENDONVITINH.Ten AS DVT,"
        sql &= " NHAPKHO.SoLuong,NHAPKHO.DonGia,(NHAPKHO.SoLuong*NHAPKHO.DonGia)ThanhTien,NHAPKHO.ChiPhi,NHAPKHO.TienTe,NHAPKHO.MucThue,NHAPKHO.NhapThue,"
        sql &= " IDDatHang, NHAPKHO.ModifyID, NHAPKHO.ModifyDate,NHAPKHO.SoHDGTGT,NHAPKHO.NgayHDGTGT "
        sql &= " FROM NHAPKHO "
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
        sql &= " INNER JOIN VATTU ON VATTU.ID=NHAPKHO.IDVatTu "
        sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu "
        sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonViTinh"
        sql &= " LEFT JOIN TENNUOC ON TENNUOC.ID=VATTU.IDTenNuoc"
        sql &= " WHERE NHAPKHO.SoPhieu=@PhieuNK  ORDER BY AZ"
        AddParameterWhere("@PhieuNK", _PhieuNK)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvVT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            rcbTienTe.DataSource = tb
            cbTienTe.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                '     _exit = True
                cbTienTe.EditValue = Convert.ToByte(TienTe.VND)
                '      _exit = False
                tbTyGia.EditValue = 1
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Public Sub loadKhachHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,IDHinhThucTT,IDTakecare FROM KHACHHANG")
        If Not tb Is Nothing Then
            gdvMaKH.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub gdvMaKH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvMaKH.KeyPress
        If gdvMaKH.IsPopupOpen Then
            Exit Sub
        Else
            gdvMaKH.ShowPopup()
        End If
    End Sub

    Private Sub gdvPhieuCG_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvPhieuDH.KeyPress
        If gdvPhieuDH.IsPopupOpen Then
            Exit Sub
        Else
            gdvPhieuDH.ShowPopup()
        End If
    End Sub

    Private Sub gdvMaKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        On Error Resume Next
        LoadPhieuCG(gdvMaKH.EditValue)
    End Sub

    'Private Sub cbNguoiGD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNguoiGD.EditValueChanged
    '    On Error Resume Next
    '    'If _Exit Then Exit Sub
    '    Dim edit As LookUpEdit = CType(sender, LookUpEdit)
    '    Dim dr As DataRowView = edit.GetSelectedDataRow
    '    cbTakeCare.EditValue = dr("Chamsoc")
    'End Sub

    Private Sub btIn_Click(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If tbSoPhieu.EditValue Is Nothing Then Exit Sub
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

            AddParameterWhere("@SP", tbSoPhieu.EditValue)
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
            pIn.Visible = False
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btInPhieu_Click(sender As System.Object, e As System.EventArgs) Handles btInPhieu.Click
        pIn.Visible = True
    End Sub

    Private Sub gdvPhieuCG_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhieuDH.EditValueChanged
        On Error Resume Next
        LoadThamChieu(gdvPhieuDH.EditValue)
        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow

        cbTienTe.EditValue = dr("TienTe")
        tbTyGia.EditValue = dr("TyGia")
        tbTruocThueDH.EditValue = dr("TienTruocThue")
        tbTienThueDH.EditValue = dr("TienThue")
        cbNguoiDat.EditValue = Convert.ToInt32(dr("IDTakeCare"))

        'lbThongTinCG.Text = "*Thông tin chào giá: " & String.Format("Trước thuế: {0:N2}, tiền thuế: {1:N2}, chiết khấu: {2:N2}", dr("TienTruocThue"), dr("TienThue"), dr("TienChietKhau"))
    End Sub

    Private Sub chkChonHet_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChonHet.CheckedChanged
        gdvThamChieuCT.BeginUpdate()
        For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
            gdvThamChieuCT.SetRowCellValue(i, "Chon", chkChonHet.Checked)
        Next
        gdvThamChieuCT.EndUpdate()
    End Sub

    Private Sub gdvThamChieuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvThamChieuCT.RowCellClick
        If e.Column.FieldName = "Chon" Then
            gdvThamChieuCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
        End If
    End Sub


    Private Sub XtraTabControl1_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabXK.SelectedPageChanged
        If e.Page Is tabThamChieuDH Then
            btChuyenXK.Visible = True
        Else
            btChuyenXK.Visible = False
        End If
    End Sub

    Private Sub btChuyenXK_Click(sender As System.Object, e As System.EventArgs) Handles btChuyenXK.Click, mNhapKho.ItemClick
        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
            If gdvThamChieuCT.GetRowCellValue(i, "Chon") Then
                If gdvThamChieuCT.GetRowCellValue(i, "SoLuong") > 0 Then
                    gdvVTCT.AddNewRow()
                    gdvVTCT.SetFocusedRowCellValue("IDVatTu", gdvThamChieuCT.GetRowCellValue(i, "IDVatTu"))
                    gdvVTCT.SetFocusedRowCellValue("TenVT", gdvThamChieuCT.GetRowCellValue(i, "TenVT"))
                    gdvVTCT.SetFocusedRowCellValue("HangSX", gdvThamChieuCT.GetRowCellValue(i, "TenHang"))
                    gdvVTCT.SetFocusedRowCellValue("Model", gdvThamChieuCT.GetRowCellValue(i, "Model"))
                    gdvVTCT.SetFocusedRowCellValue("ThongSo", gdvThamChieuCT.GetRowCellValue(i, "ThongSo"))
                    gdvVTCT.SetFocusedRowCellValue("DVT", gdvThamChieuCT.GetRowCellValue(i, "TenDVT"))
                    gdvVTCT.SetFocusedRowCellValue("SoLuong", gdvThamChieuCT.GetRowCellValue(i, "SoLuong"))
                    gdvVTCT.SetFocusedRowCellValue("DonGia", gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                    gdvVTCT.SetFocusedRowCellValue("ChiPhi", gdvThamChieuCT.GetRowCellValue(i, "ChiPhi"))
                    gdvVTCT.SetFocusedRowCellValue("ThanhTien", gdvThamChieuCT.GetRowCellValue(i, "SoLuong") * gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                    gdvVTCT.SetFocusedRowCellValue("MucThue", gdvThamChieuCT.GetRowCellValue(i, "MucThue"))
                    gdvVTCT.SetFocusedRowCellValue("NhapThue", False)
                    gdvVTCT.SetFocusedRowCellValue("XuatXu", gdvThamChieuCT.GetRowCellValue(i, "XuatXu"))
                    gdvVTCT.SetFocusedRowCellValue("IDDatHang", gdvThamChieuCT.GetRowCellValue(i, "IDDH"))
                    gdvVTCT.CloseEditor()
                    gdvVTCT.UpdateCurrentRow()



                End If
            End If
        Next
        gdvVTCT.EndUpdate()
        tabXK.SelectedTabPage = tabNhapKho

    End Sub

    Private Sub gdvVTCT_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvVTCT.InitNewRow
        gdvVTCT.SetRowCellValue(e.RowHandle, "AZ", gdvVTCT.GetRowCellValue(gdvVTCT.RowCount - 2, "AZ") + 1)
    End Sub


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
            End If
        Next
        If count > 1 Then

            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btCal_Click(sender As System.Object, e As System.EventArgs) Handles btCal.Click
        If gdvPhieuDH.EditValue Is Nothing Then
            ShowCanhBao("Chưa có thông tin đặt hàng !")
            Exit Sub
        End If
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()

        'Dim _MaTrung As String = "Mã trùng: "
        '' Kiểm tra trùng mã khi xuất kho
        'For i As Integer = 0 To gdvVTCT.RowCount - 1
        '    If KiemTraTrung(gdvVTCT.GetRowCellValue(i, "Model")) Then
        '        _MaTrung &= vbCrLf & " - " & gdvVTCT.GetRowCellValue(i, "Model")
        '    End If
        'Next

        'If _MaTrung <> "Mã trùng: " Then
        '    If Not ShowCauHoi(_MaTrung & vbCrLf & " bạn có muốn tiếp tục không ?") Then
        '        Exit Sub
        '    End If
        'End If

        tbTienTruocThue.EditValue = 0
        tbTienThue.EditValue = 0

        For i As Integer = 0 To gdvVTCT.RowCount - 1
            tbTienTruocThue.EditValue += gdvVTCT.GetRowCellValue(i, "ThanhTien")
            If gdvVTCT.GetRowCellValue(i, "NhapThue") Then
                tbTienThue.EditValue += Math.Round((gdvVTCT.GetRowCellValue(i, "ThanhTien") * gdvVTCT.GetRowCellValue(i, "MucThue")) / 100, 2)
            End If

        Next




    End Sub

    Private Sub gdvVTCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvVTCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "SoLuong", "DonGia"
                gdvVTCT.SetRowCellValue(e.RowHandle, "ThanhTien", gdvVTCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvVTCT.GetRowCellValue(e.RowHandle, "DonGia"))
        End Select
    End Sub

    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click
        btCal.PerformClick()
        If tbTienTruocThue.EditValue <> tbTruocThueDH.EditValue Or tbTienThue.EditValue <> tbTienThueDH.EditValue Then
            If Not ShowCauHoi("Số tiền nhập kho chưa khớp đặt hàng, bạn có muốn tiếp tục lưu lại ?") Then Exit Sub
        End If

        Try
            BeginTransaction()

            If TrangThai.isAddNew Then
                tbSoPhieu.EditValue = LaySoPhieu("PHIEUNHAPKHO")
                tbNgay.EditValue = GetServerTime()
            End If
            Dim modifyDate As DateTime = GetServerTime()

            AddParameter("@Ngaythang", tbNgay.EditValue)

            AddParameter("@Kho", tbKhoXuat.EditValue)
            AddParameter("@IDkhachhang", gdvMaKH.EditValue)
            AddParameter("@SophieuDH", gdvPhieuDH.EditValue)
            AddParameter("@IDUser", cbNguoiXuat.EditValue)
            AddParameter("@Tiente", cbTienTe.EditValue)
            AddParameter("@Tygia", tbTyGia.EditValue)
            AddParameter("@Tientruocthue", tbTienTruocThue.EditValue)
            AddParameter("@Tienthue", tbTienThue.EditValue)
            AddParameter("@ModifyID", TaiKhoan)
            AddParameter("@ModifyDate", modifyDate)
            AddParameter("@IDNguoiDat", cbNguoiDat.EditValue)
            If TrangThai.isAddNew Then
                AddParameter("@Sophieu", tbSoPhieu.EditValue)
                If doInsert("PHIEUNHAPKHO") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@SoPhieu", tbSoPhieu.EditValue)
                If doUpdate("PHIEUNHAPKHO", "Sophieu=@SoPhieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                AddParameter("@Sophieu", tbSoPhieu.EditValue)
                AddParameter("@IDvattu", gdvVTCT.GetRowCellValue(i, "IDVatTu"))
                AddParameter("@Soluong", gdvVTCT.GetRowCellValue(i, "SoLuong"))
                AddParameter("@Dongia", gdvVTCT.GetRowCellValue(i, "DonGia"))
                AddParameter("@ChiPhi", gdvVTCT.GetRowCellValue(i, "ChiPhi"))
                AddParameter("@Tiente", cbTienTe.EditValue)
                AddParameter("@Mucthue", gdvVTCT.GetRowCellValue(i, "MucThue"))
                AddParameter("@Nhapthue", gdvVTCT.GetRowCellValue(i, "NhapThue"))
                AddParameter("@IDDathang", gdvVTCT.GetRowCellValue(i, "IDDatHang"))
                AddParameter("@ModifyID", TaiKhoan)
                AddParameter("@ModifyDate", modifyDate)
                AddParameter("@AZ", gdvVTCT.GetRowCellValue(i, "AZ"))
                AddParameter("@SoHDGTGT", gdvVTCT.GetRowCellValue(i, "SoHDGTGT"))
                AddParameter("@NgayHDGTGT", gdvVTCT.GetRowCellValue(i, "NgayHDGTGT"))
                If IsDBNull(gdvVTCT.GetRowCellValue(i, "ID")) Or gdvVTCT.GetRowCellValue(i, "ID") Is Nothing Then
                    Dim IDNK As Integer = doInsert("NHAPKHO")
                    If IDNK = Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvVTCT.SetRowCellValue(i, "ID", IDNK)
                    gdvVTCT.CloseEditor()
                    gdvVTCT.UpdateCurrentRow()
                Else
                    AddParameterWhere("@ID", gdvVTCT.GetRowCellValue(i, "ID"))
                    If doUpdate("NHAPKHO", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next


            If ExecuteSQLNonQuery("DELETE FROM NHAPKHO WHERE SoLuong=0 AND SoPhieu='" & tbSoPhieu.EditValue & "'") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            If ExecuteSQLNonQuery("UPDATE UNC SET TamUng=0 WHERE UNC.MucDich=210 AND UNC.PhieuTC0='" & gdvPhieuDH.EditValue & "' UPDATE CHI SET TamUng=0 WHERE CHI.MucDich=210 AND CHI.PhieuTC0='" & gdvPhieuDH.EditValue & "'") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            ComitTransaction()




            TrangThai.isUpdate = True
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                gdvPhieuDH.Enabled = False
                gdvMaKH.Enabled = False
            Else
                colDonGia.OptionsColumn.ReadOnly = False
            End If

            For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
                Dim sql As String = ""

                sql = " Update DATHANG Set CanNhap = (Soluong - (select (isnull(sum(soluong),0)) from NHAPKHO where IDDathang = " & gdvThamChieuCT.GetRowCellValue(i, "IDDH") & ")) Where ID = " & gdvThamChieuCT.GetRowCellValue(i, "IDDH")

                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    ShowBaoLoi(sql)
                End If

            Next

            ShowAlert("Đã cập nhật phiếu nhập kho !")

            ShowWaiting("Phân bổ chi phí nhập")
            PhanBoChiPhiNhap(tbSoPhieu.EditValue)
            CloseWaiting()


            Dim tinhphanbo As String = CapNhatPhanBoTamUng(gdvPhieuDH.EditValue)
            If tinhphanbo <> "OK" Then
                ShowBaoLoi(tinhphanbo)
            End If


            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mNhapKho" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmNhapKho).LoadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmNhapKho).gdvCT.FocusedRowHandle = CType(deskTop.tabMain.TabPages(i).Controls(0), frmNhapKho).index
                ElseIf deskTop.tabMain.TabPages(i).Tag = "mDHCanNhap" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmDHCanNhapKho).LoadDS()
                End If
            Next
            LoadThamChieu(gdvPhieuDH.EditValue)
            loadNhapKho(tbSoPhieu.EditValue)
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
            If TrangThai.isAddNew Then
                tbSoPhieu.EditValue = Nothing
            End If

        End Try

    End Sub


    Private Sub btTichThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTichThue.ItemClick
        Dim check As Boolean = gdvVTCT.GetRowCellValue(0, "NhapThue")
        gdvVTCT.BeginUpdate()
        For i As Integer = 0 To gdvVTCT.RowCount - 1
            gdvVTCT.SetRowCellValue(i, "NhapThue", Not check)
        Next
        gdvVTCT.EndUpdate()
        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()
    End Sub

    Private Sub mChonBoChon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChonBoChon.ItemClick
        gdvThamChieuCT.BeginUpdate()
        chkChonHet.Checked = Not chkChonHet.Checked
        For i As Integer = 0 To gdvThamChieuCT.RowCount - 1
            gdvThamChieuCT.SetRowCellValue(i, "Chon", chkChonHet.Checked)
        Next
        gdvThamChieuCT.EndUpdate()
    End Sub

    Private Sub gdvVTCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvVTCT.RowCellClick
        If e.Column.FieldName = "NhapThue" Then
            gdvVTCT.SetRowCellValue(e.RowHandle, "NhapThue", Not e.CellValue)
        End If
    End Sub

    Private Sub tbTienThueCG_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTienThueDH.EditValueChanged, tbTienThueDH.EditValueChanged
        tbTongDH.EditValue = tbTienThueDH.EditValue + tbTruocThueDH.EditValue
    End Sub

    Private Sub tbTienThue_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTienThue.EditValueChanged, tbTienTruocThue.EditValueChanged
        tbTongNK.EditValue = tbTienThue.EditValue + tbTienTruocThue.EditValue
    End Sub

    Private Sub btCapNhatGia_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatGia.Click

        If ShowCauHoi("Cập nhật giá nhập kho ? ") Then
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()
            gdvVTCT.BeginUpdate()
            For i As Integer = 0 To gdvThamChieuCT.DataRowCount - 1
                For j As Integer = 0 To gdvVTCT.DataRowCount - 1
                    If gdvThamChieuCT.GetRowCellValue(i, "IDVatTu") = gdvVTCT.GetRowCellValue(j, "IDVatTu") And gdvThamChieuCT.GetRowCellValue(i, "IDDH") = gdvVTCT.GetRowCellValue(j, "IDDatHang") Then
                        gdvVTCT.SetRowCellValue(j, "DonGia", gdvThamChieuCT.GetRowCellValue(i, "DonGia"))
                    End If
                Next
            Next
            gdvVTCT.EndUpdate()
            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()

            btCal.PerformClick()
            ShowAlert("Đã cập nhật!")
        End If
    End Sub

    Private Sub mnuSaoChepDongHangHoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSaoChepDongHangHoa.ItemClick

        If gdvVTCT.FocusedRowHandle < 0 Then Exit Sub
        'If Not ShowCauHoi("Tách số lượng hàng hóa này?") Then Exit Sub
        If gdvVTCT.GetFocusedRowCellValue("SoLuong") <= 1 Then
            ShowCanhBao("Số lượng không đủ tách!")
            Exit Sub
        End If


        Dim str As String = XtraInputBox.Show("Nhập số lượng cần tách:", "Chú ý!", "")
        If str = "" Then Exit Sub

        Dim sl As Double = 0
        Try
            sl = Convert.ToDouble(str)
        Catch ex As Exception
            ShowCanhBao("Số lượng không hợp lệ!")
            Exit Sub
        End Try

        If sl < 1 Or sl >= gdvVTCT.GetFocusedRowCellValue("SoLuong") Then
            ShowCanhBao("Số lượng phải >=1 và < " & gdvVTCT.GetFocusedRowCellValue("SoLuong"))
            Exit Sub
        End If

        Dim dt As DataTable = CType(gdvVT.DataSource, DataTable)
        Dim index As Integer = gdvVTCT.FocusedRowHandle
        gdvVTCT.AddNewRow()
        For i As Integer = 0 To gdvVTCT.Columns.Count - 1
            If gdvVTCT.Columns(i).FieldName = "ID" Or gdvVTCT.Columns(i).FieldName = "AZ" Then Continue For
            gdvVTCT.SetFocusedRowCellValue(gdvVTCT.Columns(i), gdvVTCT.GetRowCellValue(index, gdvVTCT.Columns(i)))
        Next
        gdvVTCT.SetFocusedRowCellValue("SoLuong", sl)
        gdvVTCT.SetRowCellValue(index, "SoLuong", gdvVTCT.GetRowCellValue(index, "SoLuong") - sl)

    End Sub



    Private Sub frmCNNhapKho_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If strID <> "" Then
            btChuyenXK.PerformClick()
            btCal.PerformClick()
        End If
    End Sub



    ''' <summary>
    ''' Tính giá trị phân bổ tạm ứng cho xuất kho
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CapNhatPhanBoTamUng(SoDatHang As String) As String

        Dim kq As String = "OK"
        Dim sql As String = ""

        Try

            'Lấy mức phần trăm phân bổ
            sql = "select TraTruoc1,TraTruoc2,TraSau1 FROM DM_HINH_THUC_TT WHERE ID = (select IDHinhThucTT2 from phieudathang where SoPhieu = @SoDH) "
            AddParameter("@SoDH", SoDatHang)
            Dim dtMucPhanBo As DataTable = ExecuteSQLDataTable(sql)
            If dtMucPhanBo Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim TyLePhanBo As Double = 1

            If dtMucPhanBo.Rows.Count > 0 Then
                Dim r As DataRow = dtMucPhanBo.Rows(0)
                If r("TraTruoc1") > 0 And r("TraTruoc2") > 0 And r("TraSau1") > 0 Then
                    TyLePhanBo = (r("TraTruoc1") + r("TraTruoc2")) / 100.0
                Else
                    TyLePhanBo = r("TraTruoc1") / 100.0
                End If
            End If

            'Lấy tổng tiền tạm ứng cho đặt hàng
            'Mục đích 210 là mua hàng
            sql = "select "
            sql &= "(select isnull(sum(Sotien),0) FROM CHI where MucDich = 210 AND PhieuTC0 = @SoDH) "
            sql &= " + "
            sql &= "(select isnull(sum(sotien),0) from UNC WHERE MucDich = 210 AND PhieuTC0 = @SoDH) "
            AddParameter("@SoDH", SoDatHang)
            Dim TienTamUng As Double = ExecuteSQLDataTable(sql).Rows(0)(0)

            'Lấy danh sách xuất kho tương ứng với chào giá này
            AddParameter("@SoDH", SoDatHang)
            Dim dtXuatKho As DataTable = ExecuteSQLDataTable("SELECT SoPhieu, (Tientruocthue+Tienthue)ThanhTien FROM PHIEUNHAPKHO WHERE SophieuDH = @SoDH ORDER BY NgayThang ASC")

            Dim SoDuTamUng As Double = TienTamUng

            For Each r As DataRow In dtXuatKho.Rows

                Dim tienPhanBo As Double = r("ThanhTien") * TyLePhanBo

                If TienTamUng <> 0 Then
                    SoDuTamUng = SoDuTamUng - tienPhanBo
                    AddParameter("@PhanBoTamUng", tienPhanBo)
                    AddParameter("@SoDuTamUng", SoDuTamUng)
                    AddParameterWhere("@Sophieu", r("SoPhieu"))
                    If doUpdate("PHIEUNHAPKHO", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else

                    AddParameter("@PhanBoTamUng", 0)
                    AddParameter("@SoDuTamUng", 0)
                    AddParameterWhere("@Sophieu", r("SoPhieu"))
                    If doUpdate("PHIEUNHAPKHO", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If

            Next

            AddParameter("@DaTamUng", TienTamUng)
            AddParameter("@TamUngConLai", SoDuTamUng)
            AddParameterWhere("@Sophieu", SoDatHang)
            If doUpdate("PHIEUDATHANG", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Return "OK"

        Catch ex As Exception
            kq = ex.Message
        End Try

        Return kq

    End Function

End Class

#Region "-- CLASS INPUTBOX --"
Public Class XtraInputBox

    Private Shared f As XtraForm
    Private Shared WithEvents cmdAccept As DevExpress.XtraEditors.SimpleButton
    Private Shared txtResults As DevExpress.XtraEditors.TextEdit
    Private Shared lblPrompt As DevExpress.XtraEditors.LabelControl
    Private Shared WithEvents cmdCancel As DevExpress.XtraEditors.SimpleButton


    Private Shared Sub InitializeComponent()
        f = New XtraForm
        txtResults = New DevExpress.XtraEditors.TextEdit()
        cmdAccept = New DevExpress.XtraEditors.SimpleButton()
        lblPrompt = New DevExpress.XtraEditors.LabelControl()
        cmdCancel = New DevExpress.XtraEditors.SimpleButton()
        DirectCast(txtResults.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        f.SuspendLayout()
        '
        'txtResults
        '
        txtResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        txtResults.Location = New System.Drawing.Point(12, 31)
        txtResults.Name = "txtResults"
        txtResults.Size = New System.Drawing.Size(375, 20)
        txtResults.TabIndex = 0
        txtResults.Text = String.Empty
        '
        'cmdAccept
        '
        cmdAccept.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        cmdAccept.Location = New System.Drawing.Point(231, 57)
        cmdAccept.Name = "cmdAccept"
        cmdAccept.Size = New System.Drawing.Size(75, 23)
        cmdAccept.TabIndex = 1
        cmdAccept.Text = "&Thực hiện"

        AddHandler cmdAccept.Click, AddressOf cmdAccept_Click
        '
        'lblPrompt
        '
        lblPrompt.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold)
        lblPrompt.Location = New System.Drawing.Point(12, 12)
        lblPrompt.Name = "lblPrompt"
        lblPrompt.Size = New System.Drawing.Size(42, 13)
        lblPrompt.TabIndex = 2
        lblPrompt.Text = "prompt"
        '
        'cmdCancel
        '
        cmdCancel.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        cmdCancel.Location = New System.Drawing.Point(312, 57)
        cmdCancel.Name = "cmdCancel"
        cmdCancel.Size = New System.Drawing.Size(75, 23)
        cmdCancel.TabIndex = 3
        cmdCancel.Text = "Đó&ng lại"
        '
        'XtraInputBox
        '
        f.AcceptButton = cmdAccept
        f.CancelButton = cmdCancel
        f.ClientSize = New System.Drawing.Size(399, 91)
        f.Controls.Add(lblPrompt)
        f.Controls.Add(cmdCancel)
        f.Controls.Add(cmdAccept)
        f.Controls.Add(txtResults)
        f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        f.HelpButton = True
        f.MaximizeBox = False
        f.MinimizeBox = False
        f.Name = "XtraInputBox"
        f.ShowInTaskbar = False
        f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        DirectCast(txtResults.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        f.ResumeLayout(False)
        f.PerformLayout()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Overloads Shared Sub cmdAccept_Click(sender As Object, e As System.EventArgs) Handles cmdAccept.Click
        f.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Overloads Shared Sub cmdCancel_click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
        f.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Public Overloads Shared Function Show(Prompt As String) As String
        Return Show(Prompt, Application.ProductName).ToString()
    End Function

    Public Overloads Shared Function Show(Prompt As String, Title As String) As Object
        Return Show(Prompt, Title, "")
    End Function

    Public Overloads Shared Function Show(Prompt As String, Title As String, DefaultResponse As String) As Object
        Return Show(Prompt, Title, DefaultResponse, -1)
    End Function

    Public Overloads Shared Function Show(Prompt As String, Title As String, DefaulResponse As String, XPos As Integer) As Object
        Return Show(Prompt, Title, DefaulResponse, XPos, -1)
    End Function

    Public Overloads Shared Function Show(Prompt As String, Title As String, DefaultResponse As String, XPos As Integer, YPos As Integer) As String
        InitializeComponent()
        lblPrompt.Text = Prompt
        f.Text = Title
        f.Top = XPos
        f.Left = YPos
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Return txtResults.Text
        Else
            Return DefaultResponse
        End If
    End Function
End Class
#End Region