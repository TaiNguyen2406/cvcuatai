Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors


Public Class frmCNDatHang
    Public _SoPhieu As Object
    Public _exit As Boolean = False

    Private Sub frmCNDatHang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadTienTe()
        loadLoaiDH()
        loadKhachHang()
        loadTakeCare()
        ' loadCbNgd()
        loadCbHinhThucTT()

        loadDSVatTuDatHang(_SoPhieu)
    End Sub


    Public Sub loadTienTe()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TyGia FROM tblTienTe")
        If Not tb Is Nothing Then
            cbTienTe.Properties.DataSource = tb
            If cbTienTe.EditValue Is Nothing Then
                cbTienTe.EditValue = Convert.ToByte(TienTe.VND)
            End If
            cbTienTeNK.Properties.DataSource = tb
            If cbTienTeNK.EditValue Is Nothing Then
                cbTienTeNK.EditValue = Convert.ToByte(TienTe.USD)
            End If
            rcbTienTeDH.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadLoaiDH()
        Dim sql As String = " SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=" & LoaiTuDien.LoaiDatHang & " ORDER BY Ma"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            cbLoaiDH.Properties.DataSource = dt
            If dt.Rows.Count > 0 Then
                cbLoaiDH.EditValue = dt.Rows(0)(0)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadKhachHang()
        Dim sql As String = " SELECT ID,ttcMa,Ten,IDTakeCare,IDHinhThucTT FROM KHACHHANG WHERE ttcKhachhang <> 1 "
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '    sql &= " AND IDTakeCare=@IDNV"
        '    AddParameterWhere("@IDNV", TaiKhoan)
        'End If
        sql &= " ORDER BY ttcMa"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvNhaCCDatHang.Properties.DataSource = dt
            ' rcbNCC.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadTakeCare()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 ")
        If Not tb Is Nothing Then
            cbTakeCareDatHang.Properties.DataSource = tb
            cbNguoiLap.Properties.DataSource = tb
            cbNguoiLap.EditValue = Convert.ToInt32(TaiKhoan)
            '  rCbNhanVien.DataSource = tb
            '  btNhanVien.EditValue = Convert.ToInt32(TaiKhoan)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadCbNgd(ByVal cbNgd As LookUpEdit, ByVal IDKhachHang As Object)
        AddParameterWhere("@MaNCC", IDKhachHang)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,ChamSoc as IDTakeCare FROM NHANSU WHERE Noictac=@MaNCC order by Ten")
        If Not tb Is Nothing Then
            cbNgd.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadCbHinhThucTT()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,HinhThucTT_VIE FROM tblHinhThucTTKH")
        If Not tb Is Nothing Then
            cbHinhThucTT.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbHinhThucTT.EditValue = tb.Rows(0)("ID")
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvNhaCCDatHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvNhaCCDatHang.EditValueChanged
        On Error Resume Next
        loadCbNgd(cbDaiDienDatHang, gdvNhaCCDatHang.EditValue)

        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbHinhThucTT.EditValue = dr("IDHinhThucTT")
        cbTakeCareDatHang.EditValue = dr("IDTakecare")
    End Sub

    Private Sub cbLoaiDH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbLoaiDH.EditValueChanged

        If cbLoaiDH.EditValue = 1 Then
            bTTVatTu.Visible = False
            bTTNhapKhau.Visible = True
            colDonGia.Visible = False
            colFOB.Visible = True
            colVAT.Visible = False
            colNhapVAT.Visible = False
            colTNKDH.Visible = True
            colSLVe.Visible = False
            lbTienTeNK.Visible = False
            cbTienTeNK.Visible = False
            btChiPhiHQ.Visible = False
            bSoPhieuPhu.Visible = False
            colSoLuong.OptionsColumn.ReadOnly = False
            gdvDatHangCT.OptionsSelection.MultiSelect = False
            '  mVTVe.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btBacklog.Visible = False
            lbTongFOB.Visible = False
            lbTongFOBChuaVe.Visible = False
            tbTongFOBVe.Visible = False
            tbTongFOBChuaVe.Visible = False
            'If tbSoPhieuDH.EditValue Is Nothing Then
            '    For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            '        gdvDatHangCT.BeginUpdate()
            '        gdvDatHangCT.DeleteRow(i)
            '        gdvDatHangCT.EndUpdate()
            '    Next
            'Else
            '    _exit = True
            '    loadDSVatTuDatHang(tbSoPhieuDH.EditValue, 1)
            '    _exit = False
            'End If
            bSTT.Visible = True
        ElseIf cbLoaiDH.EditValue = 0 Then
            bTTVatTu.Visible = True
            bTTNhapKhau.Visible = False
            colDonGia.Visible = True
            colFOB.Visible = False
            colVAT.Visible = True
            colNhapVAT.Visible = True
            colTNKDH.Visible = False
            lbTienTeNK.Visible = False
            cbTienTeNK.Visible = False
            btChiPhiHQ.Visible = False
            colSLVe.Visible = False
            bSoPhieuPhu.Visible = False
            colSoLuong.OptionsColumn.ReadOnly = False
            gdvDatHangCT.OptionsSelection.MultiSelect = False
            ' mVTVe.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btBacklog.Visible = False
            lbTongFOB.Visible = False
            lbTongFOBChuaVe.Visible = False
            tbTongFOBVe.Visible = False
            tbTongFOBChuaVe.Visible = False
            'If tbSoPhieuDH.EditValue Is Nothing Then
            '    For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            '        gdvDatHangCT.BeginUpdate()
            '        gdvDatHangCT.DeleteRow(i)
            '        gdvDatHangCT.EndUpdate()
            '    Next
            'Else
            '    _exit = True
            '    loadDSVatTuDatHang(tbSoPhieuDH.EditValue, 0)
            '    _exit = False
            'End If
            bSTT.Visible = True
        Else
            bTTVatTu.Visible = True
            bTTNhapKhau.Visible = True
            colDonGia.Visible = True
            colFOB.Visible = True
            colVAT.Visible = True
            colNhapVAT.Visible = True
            colTNKDH.Visible = True
            lbTienTeNK.Visible = True
            cbTienTeNK.Visible = True
            btChiPhiHQ.Visible = True
            colSLVe.Visible = True
            bSoPhieuPhu.Visible = True
            gdvDatHangCT.OptionsSelection.MultiSelect = True
            colSoLuong.OptionsColumn.ReadOnly = True
            '  mVTVe.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            'If TrangThaiDH.isAddNew Then
            '    LoadDSHangCanVe()
            'Else
            '    LoadDSHangCanVe(tbSoPhieuDH.EditValue)
            'End If

            chkDuyetDatHang.Checked = True
            btBacklog.Visible = True
            lbTongFOB.Visible = True
            lbTongFOBChuaVe.Visible = True
            tbTongFOBVe.Visible = True
            tbTongFOBChuaVe.Visible = True
            bSTT.Visible = False
        End If
    End Sub

    Public Sub loadDSVatTuDatHang(ByVal SoPhieu As Object)
        If SoPhieu = Nothing Then SoPhieu = "-1"
        Dim sql As String = ""
        sql &= " SELECT NgayDat,NgayNhan,IDKhachHang,SoPhieu,DieuKienKhac,TyGia,IDUser,TienTe, IDNgd,IDTakeCare,"
        sql &= " 		TienTruocThue,TienThue,PheDuyet,IDNguoiDuyet,FileDinhKem,IDHinhThucTT,LoaiDH,SoPhieuO"
        sql &= " FROM PHIEUDATHANG"
        sql &= " WHERE SoPhieu=" & SoPhieu

        sql &= " Select DATHANG.AZ, TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= "     VATTU.ThongSo, VATTU.ID AS IDVatTu,DATHANG.Soluong AS SoLuong,DATHANG.Dongia AS DonGia,DATHANG.FOB,"
        sql &= "     (CASE PHIEUDATHANG.LoaiDH WHEN 1 THEN DATHANG.Soluong*DATHANG.FOB ELSE DATHANG.Soluong*DATHANG.Dongia END )ThanhTien ,Convert(float,0) GiaNhapPT,"
        sql &= "     DATHANG.MucThue,DATHANG.NhapThue,DATHANG.ID AS IDDatHang,"
        sql &= " (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList, "
        sql &= " VATTU.Tiente1 AS TienTe,ISNULL(VATTU.Gianhap1,0) AS GiaNhapDG,"
        sql &= "     VATTU.Mucthue1 AS MucThueDG, VATTU.Xuatthue1 AS NhapThueDG,VATTU.Khoiluong1 AS KhoiLuong,"
        sql &= "     ISNULL(DATHANG.TyGia,tblTienTe.TyGia)TyGia,ISNULL(VATTU.GiaNK,0)GiaNK,VATTU.DMTon,DATHANG.ChiPhi,"
        sql &= "     DATHANG.FOB,VATTU.TNK1,VATTU.TienTeNK,DATHANG.TNK,ISNULL(DATHANG.CO,Convert(bit,0))CO,ISNULL(DATHANG.CQ,Convert(bit,0))CQ,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan)NgayVe,ISNULL(NgayVe2,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan))NgayVe2 "
        sql &= " from DATHANG"

        sql &= "     LEFT JOIN VATTU on DATHANG.IDvattu =VATTU.ID "
        sql &= "     LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= "     LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= "     LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "     LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        'If LoaiDH = 1 Then
        '    sql &= "     INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu "
        '    sql &= " WHERE DATHANG.SoPhieuPhu = " & SoPhieu
        'Else
        sql &= "     INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieu "
        sql &= " WHERE DATHANG.SoPhieu = " & SoPhieu
        ' End If
        sql &= " ORDER BY DATHANG.AZ, DATHANG.ID"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            'If ds.Tables(1).Rows Then
            If ds.Tables(0).Rows.Count = 0 Then Exit Sub
            If ds.Tables(0).Rows(0)("LoaiDH") = 2 Then
                bSTT.Visible = False
                LoadDSHangCanVe(SoPhieu)
            Else
                bSTT.Visible = True
                With ds.Tables(1)

                    'If LoaiDH = 1 Then
                    '    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    '        .Rows(i)("AZ") = i + 1
                    '        If .Rows(i)("GiaNK") = 0 Then
                    '            .Rows(i)("GiaNhapPT") = 0
                    '        Else
                    '            .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("FOB") / .Rows(i)("GiaNK")) * 100, 2)
                    '        End If

                    '    Next
                    'Else
                    For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                        .Rows(i)("AZ") = i + 1
                        If .Rows(i)("GiaList") = 0 Then
                            .Rows(i)("GiaNhapPT") = 0
                        Else
                            If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                                .Rows(i)("GiaNhapPT") = 0
                            Else
                                If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(ds.Tables(0).Rows(0)("Tiente")) Then
                                    .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / ds.Tables(0).Rows(0)("TyGia"))) * 100, 2)
                                Else
                                    .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)

                                End If
                            End If

                        End If

                    Next
                    '   End If

                End With
                gdvDatHang.DataSource = ds.Tables(1)
            End If
            If _exit = False Then
                If ds.Tables(0).Rows.Count > 0 Then
                    tbSoPhieuDH.EditValue = ds.Tables(0).Rows(0)("SoPhieu")
                    gdvNhaCCDatHang.EditValue = ds.Tables(0).Rows(0)("IDKhachHang")
                    cbDaiDienDatHang.EditValue = ds.Tables(0).Rows(0)("IDNgd")
                    cbTakeCareDatHang.EditValue = ds.Tables(0).Rows(0)("IDTakeCare")
                    tbNgayDatHang.EditValue = ds.Tables(0).Rows(0)("NgayDat")
                    _exit = True
                    tbNgayNhanDatHang.EditValue = ds.Tables(0).Rows(0)("NgayNhan")
                    _exit = False
                    cbTienTe.EditValue = ds.Tables(0).Rows(0)("TienTe")
                    tbTyGia.EditValue = ds.Tables(0).Rows(0)("TyGia")
                    cbNguoiLap.EditValue = ds.Tables(0).Rows(0)("IDUser")
                    chkDuyetDatHang.Checked = ds.Tables(0).Rows(0)("PheDuyet")
                    tbSoPhieuDH.Tag = ds.Tables(0).Rows(0)("SoPhieuO")
                    tbTienTruocThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue")
                    tbTienThue.EditValue = ds.Tables(0).Rows(0)("TienThue")
                    cbNguoiLap.EditValue = ds.Tables(0).Rows(0)("IDUser")
                    'tbTienSauThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue") + ds.Tables(0).Rows(0)("TienThue")
                    gdvListFileDH.DataSource = DataSourceDSFile(ds.Tables(0).Rows(0)("FileDinhKem").ToString)
                    cbLoaiDH.EditValue = ds.Tables(0).Rows(0)("LoaiDH")
                Else

                    tbSoPhieuDH.EditValue = ""
                    gdvNhaCCDatHang.EditValue = Nothing
                    cbDaiDienDatHang.EditValue = Nothing
                    cbTakeCareDatHang.EditValue = Nothing
                    tbNgayDatHang.EditValue = GetServerTime()
                    tbNgayNhanDatHang.EditValue = tbNgayDatHang.EditValue
                    cbTienTe.EditValue = 0
                    tbTyGia.EditValue = 1
                    cbNguoiLap.EditValue = Convert.ToUInt32(TaiKhoan)
                    chkDuyetDatHang.Checked = False
                    tbTienTruocThue.EditValue = 0
                    tbTienThue.EditValue = 0
                    'tbTienSauThue.EditValue = ds.Tables(0).Rows(0)("TienTruocThue") + ds.Tables(0).Rows(0)("TienThue")
                    gdvListFileDH.DataSource = DataSourceDSFile()
                    cbLoaiDH.EditValue = 0
                    tbSoPhieuDH.Tag = ""
                End If
            End If

            'If TrangThaiDH.isAddNew Or TrangThaiDH.isCopy Then
            '    cbLoaiDH.Enabled = True
            'Else
            '    cbLoaiDH.Enabled = False
            'End If

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadDSHangCanVe(Optional ByVal SoDH As Object = -1)
        ShowWaiting("Đang tải ds hàng cần nhập !")
        Dim sql As String = ""
        sql &= " Select (CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN NULL ELSE DATHANG.SoPhieu END)SoPhieu,"
        sql &= " 	DATHANG.SoPhieuPhu,TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,TENDONVITINH.Ten AS DVT,"
        sql &= "     VATTU.ThongSo, VATTU.ID AS IDVatTu,DATHANG.Soluong AS SoLuong,"
        sql &= " 	(CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN 0 ELSE DATHANG.SoLuong END)SLVe,"
        sql &= " 	DATHANG.FOB,Convert(float,0) GiaNhapPT,"
        sql &= " 	(CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN 0 ELSE DATHANG.DonGia END)DonGia,"
        sql &= " 	(CASE WHEN DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) THEN 0 ELSE DATHANG.DonGia * DATHANG.SoLuong END)ThanhTien,"
        sql &= "     DATHANG.MucThue,DATHANG.NhapThue,DATHANG.ID AS IDDatHang,"
        sql &= "    (CASE VATTU.XuatThue1 WHEN 0 THEN VATTU.DonGia1 ELSE (CASE VATTU.TienTe1 WHEN 0 THEN ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,-2) ELSE ROUND((VATTU.DonGia1/(100 + VATTU.MucThue1))*100,2) END) END) AS GiaList,"
        sql &= "     VATTU.Tiente1 AS TienTe,ISNULL(VATTU.Gianhap1,0) AS GiaNhapDG,"
        sql &= "     VATTU.Mucthue1 AS MucThueDG, VATTU.Xuatthue1 AS NhapThueDG,VATTU.Khoiluong1 AS KhoiLuong,"
        sql &= "     ISNULL(DATHANG.TyGia,tblTienTe.TyGia)TyGia,ISNULL(VATTU.GiaNK,0)GiaNK,VATTU.DMTon,DATHANG.ChiPhi,"
        sql &= "     DATHANG.FOB,VATTU.TNK1,VATTU.TienTeNK,DATHANG.TNK,ISNULL(DATHANG.CO,Convert(bit,0))CO,ISNULL(DATHANG.CQ,Convert(bit,0))CQ,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan)NgayVe,ISNULL(NgayVe2,ISNULL(NgayVe,PHIEUDATHANG.NgayNhan))NgayVe2 "
        sql &= " from DATHANG"
        sql &= " 	INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieuPhu AND PHIEUDATHANG.PheDuyet=1"
        sql &= "     INNER JOIN VATTU on DATHANG.IDvattu =VATTU.ID "
        sql &= "     LEFT JOIN tblTienTe ON VATTU.Tiente1=tblTienTe.ID "
        sql &= "     LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= "     LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        sql &= "     LEFT JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        sql &= " WHERE DATHANG.SoPhieuPhu=ISNULL(DATHANG.SoPhieu,DATHANG.SoPhieuPhu) OR DATHANG.SoPhieu= '" & SoDH & "'"
        sql &= " ORDER BY DATHANG.SoPhieu DESC,DATHANG.SoPhieuPhu,DATHANG.AZ,HangSX,Model"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            With tb
                For i As Integer = 0 To .Rows.Count - 1
                    If .Rows(i)("GiaList") = 0 Then
                        .Rows(i)("GiaNhapPT") = 0
                    Else
                        If IsDBNull(.Rows(i)("TienTe")) Or .Rows(i)("TienTe") Is Nothing Then
                            .Rows(i)("GiaNhapPT") = 0
                        Else
                            If Convert.ToInt32(.Rows(i)("TienTe")) > Convert.ToInt32(cbTienTe.EditValue) Then
                                .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / (.Rows(i)("GiaList") * .Rows(i)("TyGia") / tbTyGia.EditValue)) * 100, 2)
                            Else
                                .Rows(i)("GiaNhapPT") = Math.Round((.Rows(i)("DonGia") / .Rows(i)("GiaList")) * 100, 2)
                            End If
                        End If

                    End If

                Next
            End With
            gdvDatHang.DataSource = tb
            TinhFoB()
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TinhFoB()
        tbTongFOBVe.EditValue = 0
        tbTongFOBChuaVe.EditValue = 0
        gdvDatHangCT.BeginUpdate()
        For i As Integer = 0 To gdvDatHangCT.RowCount - 1
            tbTongFOBChuaVe.EditValue += gdvDatHangCT.GetRowCellValue(i, "FOB") * (gdvDatHangCT.GetRowCellValue(i, "SoLuong") - gdvDatHangCT.GetRowCellValue(i, "SLVe"))
            tbTongFOBVe.EditValue += gdvDatHangCT.GetRowCellValue(i, "FOB") * gdvDatHangCT.GetRowCellValue(i, "SLVe")
        Next
        gdvDatHangCT.EndUpdate()
    End Sub

End Class