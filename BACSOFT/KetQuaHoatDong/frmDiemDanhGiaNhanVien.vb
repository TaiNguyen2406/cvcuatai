Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress

Public Class frmDiemDanhGiaNhanVien
    Dim _exit As Boolean = False

    Private Sub frmDiemDanhGiaNhanVien_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        _exit = True
        tbThang.EditValue = New DateTime(tg.Year, tg.Month, 1)
        _exit = False
        ' LoadCbBoPhan()
        ' btTaiBaoCao.PerformClick()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            '  tbDiemDieuChinh.Enabled = False
            btDuyetHSNangLuc.Visibility = XtraBars.BarItemVisibility.Never
        End If
    End Sub

    'Public Sub LoadCbBoPhan()
    '    AddParameterWhere("@Loai", LoaiTuDien.NhomNoiDungThiCong)
    '    Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
    '    If Not tb Is Nothing Then
    '        rcbBoPhan.DataSource = tb
    '        If tb.Rows.Count > 0 Then
    '            cbBoPhan.EditValue = tb.Rows(0)(0)
    '        End If
    '    Else
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub

    Private Sub btThemDanhGiaMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatDiemDanhGia.ItemClick
        Dim f As New frmCNDiemDanhGiaNV
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
    End Sub

    Private Sub btTaiBaoCao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiBaoCao.ItemClick
        gdvCT.Bands.Clear()
        gdvCT.Columns.Clear()

        Dim sql As String = ""

        sql &= " DECLARE  @tbND table"
        sql &= " ("
        sql &= " 	ID int,"
        sql &= " 	NoiDung nvarchar(250)"
        sql &= " )"

        sql &= " INSERT INTO @tbND(ID,NoiDung)"
        sql &= " VALUES(1,N'Tính chủ động hoàn thành công việc')"
        sql &= " INSERT INTO @tbND(ID,NoiDung)"
        sql &= " VALUES(2,N'Khả năng xử lý công việc')"
        sql &= " INSERT INTO @tbND(ID,NoiDung)"
        sql &= " VALUES(3,N'Tính chủ động tìm việc để làm')"
        sql &= " INSERT INTO @tbND(ID,NoiDung)"
        sql &= " VALUES(4,N'Khối lượng công việc hoàn thành')"
        '============tb0
        ''sql &= " SELECT * FROM ("
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
            sql &= " SELECT DISTINCT  Convert(int,null) AS STT,KTDiemDanhGiaNV.IDNguoiDanhGia,N'' as TenNguoiDanhGia,"
        Else
            sql &= " SELECT DISTINCT  Convert(int,null) AS STT,KTDiemDanhGiaNV.IDNguoiDanhGia,NGUOIDANHGIA.Ten as TenNguoiDanhGia,"
        End If

        sql &= " [@tbND].NoiDung ,[@tbND].ID as IDNoiDung,convert(bit,0) as ChungMau"
        sql &= " FROM KTDiemDanhGiaNV"
        sql &= " LEFT JOIN @tbND ON 1=1"
        sql &= " INNER JOIN NHANSU as NGUOIDANHGIA ON NGUOIDANHGIA.ID=KTDiemDanhGiaNV.IDNguoiDanhGia"
        sql &= " WHERE KTDiemDanhGiaNV.Thang=@Thang AND CapQuanLy=0 AND NhomHoTro=1"
        sql &= " ORDER BY IDNguoiDanhGia,IDNoiDung"
        '========== tb1
        sql &= " SELECT KTDiemDanhGiaNV.*,NHANVIEN.Ten as TenNhanVien,NhanSu_BoPhan.MaBP"
        sql &= " FROM KTDiemDanhGiaNV"
        sql &= " INNER JOIN NHANSU as NHANVIEN ON NHANVIEN.ID=KTDiemDanhGiaNV.IDNhanVien"
        sql &= " LEFT JOIN LUONG ON KTDiemDanhGiaNV.IDNhanVien=LUONG.IDNhanVien AND LUONG.[Month]=@Thang"
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=LUONG.IDBoPhan"
        sql &= " WHERE KTDiemDanhGiaNV.Thang=@Thang AND NhomHoTro=1"
        '=========== tb2
        sql &= " SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
        sql &= " avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam,avg(MucDoHTCongViec)MucDoHTCongViec"
        sql &= " FROM KTDiemDanhGiaNV"
        sql &= " WHERE KTDiemDanhGiaNV.Thang=@Thang AND NhomHoTro=1 AND CapQuanLy=0"
        sql &= " GROUP BY IDNhanVien"
        '=========== tb3
        sql &= " SELECT ISNULL(tbNV.IDNhanVien,isnull(tb1.IDNhanVien,tb2.IDNhanVien))IDNhanVien,"
        sql &= " (ISNULL(tb1.ChuDongHTCongViec,0) * 0.4 + ISNULL(tb2.ChuDongHTCongViec,0)*0.6)ChuDongHTCongViec,"
        sql &= " (ISNULL(tb1.ChuDongTimViecDeLam ,0)* 0.4 + ISNULL(tb2.ChuDongTimViecDeLam,0)*0.6)ChuDongTimViecDeLam,"
        sql &= " (ISNULL(tb1.LinhHoatBietViecDeLam,0) * 0.4 + ISNULL(tb2.LinhHoatBietViecDeLam,0)*0.6)LinhHoatBietViecDeLam,"
        sql &= " (ISNULL(tb1.MucDoHTCongViec,0) * 0.4 + ISNULL(tb2.MucDoHTCongViec,0)*0.6)MucDoHTCongViec"
        sql &= " FROM"
        sql &= " (SELECT DISTINCT IDNhanVien FROM KTDiemDanhGiaNV WHERE Thang=@Thang AND NhomHoTro=1 )tbNV"
        sql &= " LEFT JOIN "
        sql &= " ( "
        sql &= " SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
        sql &= "  avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam, avg(MucDoHTCongViec)MucDoHTCongViec"
        sql &= "  FROM KTDiemDanhGiaNV"
        sql &= "  WHERE KTDiemDanhGiaNV.Thang=@Thang "
        sql &= " AND NhomHoTro=1 AND CapQuanLy=0"
        sql &= "  GROUP BY IDNhanVien)tb1 ON tbNV.IDNhanVien=tb1.IDNhanVien"
        sql &= " left JOIN "
        sql &= " ("
        sql &= "  SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
        sql &= "  avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam, avg(MucDoHTCongViec)MucDoHTCongViec"
        sql &= "  FROM KTDiemDanhGiaNV"
        sql &= "  WHERE KTDiemDanhGiaNV.Thang=@Thang "
        sql &= " AND NhomHoTro=1 AND CapQuanLy=1  AND IDNguoiDanhGia<>IDNhanVien"
        sql &= "  GROUP BY IDNhanVien)tb2"
        sql &= " ON tbNV.IDNhanVien =tb2.IDNhanVien"
        'sql &= " UNION ALL"

        '=========== tb4
        'sql &= " SELECT IDNhanVien,LUONG.IDBoPhan,NhanSu_BoPhan_TrongSo.TrongSo"
        'sql &= " FROM LUONG"
        'sql &= " LEFT JOIN NhanSu_BoPhan_TrongSo"
        'sql &= " ON NhanSu_BoPhan_TrongSo.Ma=LUONG.IDBoPhan AND NhanSu_BoPhan_TrongSo.Thang=LUONG.Month"
        'sql &= " WHERE LUONG.Month=@Thang AND LUONG.IDBoPhan IN (SELECT Ma FROM NhanSu_BoPhan WHERE IDNhom=2)"

        '=========== tb4 Điểm hiệu chỉnh
        'sql &= " IF NOT EXISTS(SELECT * FROM DanhGiaNV_DiemHieuChinh WHERE Thang=@Thang AND IDNhom=@BP AND IDPhong=2)"
        'sql &= " BEGIN"
        'sql &= " DECLARE @DiemHC float"
        'sql &= " DECLARE @HSDieuChinh float"
        'sql &= " SET @DiemHC= ISNULL((SELECT TOP 1 DiemHieuChinh FROM DanhGiaNV_DiemHieuChinh WHERE IDNhom=@BP AND IDPhong=2 ORDER BY ID DESC),1)"
        'sql &= " 	INSERT INTO DanhGiaNV_DiemHieuChinh(Thang,DiemHieuChinh,IDPhong,IDNhom)"
        'sql &= " 	VALUES(@Thang,@DiemHC,2,@BP)"
        'sql &= " End"
        'sql &= " SELECT DiemHieuChinh FROM DanhGiaNV_DiemHieuChinh WHERE Thang=@Thang AND IDNhom=@BP "


        AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
        ' AddParameterWhere("@BP", cbBoPhan.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        gdvCT.BeginUpdate()
        Dim bSTT As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
        bSTT.Fixed = XtraGrid.Columns.FixedStyle.Left
        gdvCT.Bands.Add(bSTT)


        Dim cSTT As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
        cSTT.FieldName = "STT"
        cSTT.Caption = "STT"
        cSTT.OwnerBand = bSTT
        cSTT.VisibleIndex = 0
        cSTT.Width = 40

        Dim bNguoiDanhGia As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
        bNguoiDanhGia.Fixed = XtraGrid.Columns.FixedStyle.Left
        gdvCT.Bands.Add(bNguoiDanhGia)

        Dim cNguoiDanhGia As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
        cNguoiDanhGia.FieldName = "TenNguoiDanhGia"
        cNguoiDanhGia.Caption = "Người đánh giá"
        cNguoiDanhGia.OwnerBand = bNguoiDanhGia
        cNguoiDanhGia.VisibleIndex = 1
        cNguoiDanhGia.Width = 180

        Dim cIDNguoiDanhGia As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
        cIDNguoiDanhGia.FieldName = "IDNguoiDanhGia"
        cIDNguoiDanhGia.OwnerBand = bNguoiDanhGia
        cIDNguoiDanhGia.Visible = False



        Dim bNoiDung As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
        bNoiDung.Fixed = XtraGrid.Columns.FixedStyle.Left
        gdvCT.Bands.Add(bNoiDung)
        Dim cNoiDung As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
        cNoiDung.FieldName = "NoiDung"
        cNoiDung.Caption = "Nội dung đánh giá"
        cNoiDung.OwnerBand = bNoiDung
        cNoiDung.VisibleIndex = 2
        cNoiDung.Width = 190

        Dim cChungMau As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
        cChungMau.FieldName = "ChungMau"
        cChungMau.OwnerBand = bNoiDung
        cChungMau.Visible = False


        Dim cIDNoiDung As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
        cIDNoiDung.FieldName = "IDNoiDung"
        cIDNoiDung.OwnerBand = bNoiDung
        cIDNoiDung.Visible = False

        Dim bNhanVien As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
        bNhanVien.Caption = "Người được đánh giá"
        gdvCT.Bands.Add(bNhanVien)
        gdvCT.EndUpdate()
        If Not ds Is Nothing Then
            ' tbDiemDieuChinh.EditValue = ds.Tables(4).Rows(0)(0)

            If ds.Tables(0).Rows.Count > 0 Then
                gdvCT.BeginUpdate()
                Dim dv As DataView = ds.Tables(1).DefaultView
                dv.Sort = ("MaBP")
                Dim dsNV As DataTable = dv.ToTable(True, "IDNhanVien", "TenNhanVien")
                For i As Integer = 0 To dsNV.Rows.Count - 1
                    Dim c As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                    c.FieldName = "Diem" & dsNV.Rows(i)("IDNhanVien")
                    c.Tag = dsNV.Rows(i)("IDNhanVien")
                    c.Caption = dsNV.Rows(i)("TenNhanVien")
                    c.OwnerBand = bNhanVien
                    c.VisibleIndex = i + 2
                    c.Width = 160
                    gdvCT.Columns.Add(c)
                    ' b.Columns.Add(c)

                    ds.Tables(0).Columns.Add("Diem" & dsNV.Rows(i)("IDNhanVien"), GetType(Double))
                Next
                With ds.Tables(0)
                    Dim _STT As Integer = 1
                    Dim _count As Integer = 0
                    If .Rows.Count > 0 Then
                        .Rows(0)("STT") = _STT
                        _count = 1
                    End If
                    For i As Integer = 1 To .Rows.Count - 1
                        If _count = 4 Then
                            _STT += 1
                            .Rows(i)("STT") = _STT
                            _count = 1
                            .Rows(i)("ChungMau") = Not .Rows(i - 1)("ChungMau")
                        Else
                            _count += 1
                            .Rows(i)("TenNguoiDanhGia") = ""
                            .Rows(i)("ChungMau") = .Rows(i - 1)("ChungMau")
                        End If
                    Next
                End With
                gdvCT.EndUpdate()

                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 1, "Trung bình tự đánh giá", "Tính chủ động hoàn thành công việc", Not ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 2, "", "Khả năng xử lý công việc", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 3, "", "Tính chủ động tìm việc để làm", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 4, "", "Khối lượng công việc hoàn thành", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))

                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 1, "Cấp quản lý đánh giá", "Tính chủ động hoàn thành công việc", Not ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 2, "", "Khả năng xử lý công việc", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 3, "", "Tính chủ động tìm việc để làm", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 4, "", "Khối lượng công việc hoàn thành", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))

                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 1, "Điểm", "Tính chủ động hoàn thành công việc", Not ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 2, "", "Khả năng xử lý công việc", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 3, "", "Tính chủ động tìm việc để làm", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))
                ds.Tables(0).Rows.Add(CreateRow(ds.Tables(0), 4, "", "Khối lượng công việc hoàn thành", ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)("ChungMau")))

                gdv.DataSource = ds.Tables(0)

                gdvCT.BeginUpdate()
                Dim tbDiem As DataTable = ds.Tables(1)
                For i As Integer = 0 To gdvCT.RowCount - 13
                    For j As Integer = 6 To gdvCT.Columns.Count - 1
                        Dim dr() As DataRow = tbDiem.Select("IDNguoiDanhGia=" & gdvCT.GetRowCellValue(i, "IDNguoiDanhGia") & " AND IDNhanVien=" & gdvCT.Columns(j).Tag)
                        If dr.Length > 0 Then

                            Select Case gdvCT.GetRowCellValue(i, "IDNoiDung")
                                Case 1
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("ChuDongHTCongViec"))
                                Case 2
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("ChuDongTimViecDeLam"))
                                Case 3
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("LinhHoatBietViecDeLam"))
                                Case 4
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("MucDoHTCongViec"))
                            End Select
                        End If
                    Next
                Next
                Dim tbDiemTB As DataTable = ds.Tables(2)
                For i As Integer = 6 To gdvCT.Columns.Count - 1
                    'For j As Integer = gdvCT.RowCount - 3 To gdvCT.RowCount - 1
                    Dim dr() As DataRow = tbDiemTB.Select("IDNhanVien=" & gdvCT.Columns(i).Tag)
                    If dr.Length > 0 Then
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 12, gdvCT.Columns(i).FieldName, dr(0)("ChuDongHTCongViec"))
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 11, gdvCT.Columns(i).FieldName, dr(0)("ChuDongTimViecDeLam"))
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 10, gdvCT.Columns(i).FieldName, dr(0)("LinhHoatBietViecDeLam"))
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 9, gdvCT.Columns(i).FieldName, dr(0)("MucDoHTCongViec"))
                    End If
                    'Next
                Next

                For i As Integer = gdvCT.RowCount - 8 To gdvCT.RowCount - 5
                    For j As Integer = 6 To gdvCT.Columns.Count - 1
                        Dim dr() As DataRow = tbDiem.Select("CapQuanLy=1 AND IDNhanVien=" & gdvCT.Columns(j).Tag)
                        If dr.Length > 0 Then

                            Select Case gdvCT.GetRowCellValue(i, "IDNoiDung")
                                Case 1
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("ChuDongHTCongViec"))
                                Case 2
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("ChuDongTimViecDeLam"))
                                Case 3
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("LinhHoatBietViecDeLam"))
                                Case 4
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, dr(0)("MucDoHTCongViec"))
                            End Select
                        End If
                    Next
                Next

                'For i As Integer = 6 To gdvCT.Columns.Count - 1
                '    'For j As Integer = gdvCT.RowCount - 3 To gdvCT.RowCount - 1
                '    Dim dr() As DataRow = tbDiem.Select("CapQuanLy=2 AND IDNhanVien=" & gdvCT.Columns(i).Tag)
                '    If dr.Length > 0 Then
                '        gdvCT.SetRowCellValue(gdvCT.RowCount - 6, gdvCT.Columns(i).FieldName, dr(0)("ChuDongHTCongViec"))
                '        gdvCT.SetRowCellValue(gdvCT.RowCount - 5, gdvCT.Columns(i).FieldName, dr(0)("ChuDongTimViecDeLam"))
                '        gdvCT.SetRowCellValue(gdvCT.RowCount - 4, gdvCT.Columns(i).FieldName, dr(0)("LinhHoatBietViecDeLam"))
                '    End If
                '    'Next
                'Next
                Dim tbTong As DataTable = ds.Tables(3)
                For i As Integer = 6 To gdvCT.Columns.Count - 1
                    'For j As Integer = gdvCT.RowCount - 3 To gdvCT.RowCount - 1
                    Dim dr() As DataRow = tbTong.Select("IDNhanVien=" & gdvCT.Columns(i).Tag)
                    If dr.Length > 0 Then
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 4, gdvCT.Columns(i).FieldName, dr(0)("ChuDongHTCongViec"))
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 3, gdvCT.Columns(i).FieldName, dr(0)("ChuDongTimViecDeLam"))
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 2, gdvCT.Columns(i).FieldName, dr(0)("LinhHoatBietViecDeLam"))
                        gdvCT.SetRowCellValue(gdvCT.RowCount - 1, gdvCT.Columns(i).FieldName, dr(0)("MucDoHTCongViec"))
                    End If
                    'Next
                Next

                'gdvCT.AddNewRow()
                'gdvCT.CloseEditor()
                'gdvCT.UpdateCurrentRow()
                'gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "TenNguoiDanhGia", "Trọng số BP")
                'gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "ChungMau", False)
                'Dim tbTrongSo As DataTable = ds.Tables(4)
                'For i As Integer = 6 To gdvCT.Columns.Count - 1
                '    'For j As Integer = gdvCT.RowCount - 3 To gdvCT.RowCount - 1
                '    Dim dr() As DataRow = tbTrongSo.Select("IDNhanVien=" & gdvCT.Columns(i).Tag)
                '    If dr.Length > 0 Then
                '        gdvCT.SetRowCellValue(gdvCT.RowCount - 1, gdvCT.Columns(i).FieldName, dr(0)("TrongSo"))
                '    End If
                '    'Next
                'Next

                gdvCT.AddNewRow()
                gdvCT.CloseEditor()
                gdvCT.UpdateCurrentRow()
                gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "TenNguoiDanhGia", "Điểm")
                gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "ChungMau", False)

                For i As Integer = 0 To gdvCT.Bands(bNhanVien.Name).Columns.Count - 1
                    '  Dim A As Double = checkValue(gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName))
                    Dim B As Double = (checkValue(gdvCT.GetRowCellValue(gdvCT.RowCount - 3, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName)) + checkValue(gdvCT.GetRowCellValue(gdvCT.RowCount - 4, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName)) + checkValue(gdvCT.GetRowCellValue(gdvCT.RowCount - 5, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName))) / 3
                    Dim C As Double = checkValue(gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName))
                    gdvCT.SetRowCellValue(gdvCT.RowCount - 1, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName, Math.Round(B * C, 2))
                    'Dim str As String = gdvCT.Bands(bNhanVien.Name).Columns(i).Caption + " " + Math.Round(B * C, 2).ToString

                    'gdvCT.Bands(bNhanVien.Name).Columns(i).Caption = str

                    'gdvCT.Columns(i).Caption = " " + str
                Next

                'For i As Integer = 6 To gdvCT.Columns.Count - 1
                '    For j As Integer = gdvCT.RowCount - 4 To gdvCT.RowCount - 2
                '        Dim T As Double = 0
                '        Dim T1 As Double = 0
                '        Dim T2 As Double = 0
                '        Dim T3 As Double = 0
                '        If Not IsDBNull(gdvCT.GetRowCellValue(gdvCT.RowCount - 4, gdvCT.Columns(i).FieldName)) Then
                '            T1 = gdvCT.GetRowCellValue(gdvCT.RowCount - 4, gdvCT.Columns(i).FieldName)
                '        End If

                '        If Not IsDBNull(gdvCT.GetRowCellValue(gdvCT.RowCount - 3, gdvCT.Columns(i).FieldName)) Then
                '            T2 = gdvCT.GetRowCellValue(gdvCT.RowCount - 3, gdvCT.Columns(i).FieldName)
                '        End If

                '        If Not IsDBNull(gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Columns(i).FieldName)) Then
                '            T3 = gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Columns(i).FieldName)
                '        End If

                '        T = T1 + T2 + T3
                '        gdvCT.SetRowCellValue(gdvCT.RowCount - 1, gdvCT.Columns(i).FieldName, T)
                '    Next
                'Next

                'Dim _DiemHC As Double = tbDiemDieuChinh.EditValue

                'gdvCT.AddNewRow()
                'gdvCT.CloseEditor()
                'gdvCT.UpdateCurrentRow()
                'gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "TenNguoiDanhGia", "Điểm đánh giá sau hiệu chỉnh")
                'gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "ChungMau", Not gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "ChungMau"))
                'For i As Integer = 6 To gdvCT.Columns.Count - 1
                '    gdvCT.SetRowCellValue(gdvCT.RowCount - 1, gdvCT.Columns(i).FieldName, (gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Columns(i).FieldName) + _DiemHC * 3) / 3)
                'Next
                'Next

                'gdvCT.AddNewRow()
                'gdvCT.CloseEditor()
                'gdvCT.UpdateCurrentRow()
                'gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "TenNguoiDanhGia", "Hệ số năng lực")
                'gdvCT.SetRowCellValue(gdvCT.RowCount - 1, "ChungMau", Not gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "ChungMau"))


                'For i As Integer = 0 To gdvCT.Bands(bNhanVien.Name).Columns.Count - 1

                '    gdvCT.SetRowCellValue(gdvCT.RowCount - 1, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName, Math.Round(gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName) / (4 + _DiemHC), 2))
                '    Dim str As String = gdvCT.Bands(bNhanVien.Name).Columns(i).Caption + " " + Math.Round(gdvCT.GetRowCellValue(gdvCT.RowCount - 2, gdvCT.Bands(bNhanVien.Name).Columns(i).FieldName) / (4 + _DiemHC), 2).ToString

                '    gdvCT.Bands(bNhanVien.Name).Columns(i).Caption = str

                '    gdvCT.Columns(i).Caption = " " + str
                'Next

                gdvCT.EndUpdate()

            End If


        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function checkValue(ByVal _obj As Object) As Double
        If IsDBNull(_obj) Or _obj Is Nothing Then
            Return 0
        Else
            Return _obj
        End If
    End Function

    Function CreateRow(ByVal _tb As DataTable, ByVal _idNoiDung As Object, ByVal _TenNguoiDanhGia As String, ByVal _NoiDung As String, ByVal _ChungMau As Boolean) As DataRow
        Dim r As DataRow = _tb.NewRow
        r("IDNoiDung") = _idNoiDung
        r("TenNguoiDanhGia") = _TenNguoiDanhGia
        r("NoiDung") = _NoiDung
        r("ChungMau") = _ChungMau
        Return r
    End Function

    Private Sub gdvCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvCT.RowStyle
        On Error Resume Next
        If gdvCT.GetRowCellValue(e.RowHandle, "ChungMau") Then
            e.Appearance.BackColor = Color.AliceBlue
        ElseIf gdvCT.GetRowCellValue(e.RowHandle, "TenNguoiDanhGia") = "Trọng số BP" Then
            e.Appearance.Font = New Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold)
            e.Appearance.BackColor = Color.LightYellow

        ElseIf gdvCT.GetRowCellValue(e.RowHandle, "TenNguoiDanhGia") = "Điểm" Then
            e.Appearance.BackColor = Color.LightGreen
            e.Appearance.Font = New Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold)
        End If
    End Sub




    Private Sub btDuyetHSNangLuc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDuyetHSNangLuc.ItemClick

        If ShowCauHoi("Duyệt điểm đánh giá ?") Then
            Try
                BeginTransaction()

                For i As Integer = 6 To gdvCT.Columns.Count - 1
                    AddParameter("@HSDanhGia", gdvCT.GetRowCellValue(gdvCT.RowCount - 1, gdvCT.Columns(i).FieldName))
                    AddParameterWhere("@IDNV", gdvCT.Columns(i).Tag)
                    AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
                    If doUpdate("tblTHChamCong", "IDNhanVien=@IDNV AND [Month]=@Thang") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Next
                AddParameter("@Duyet", True)
                AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
                '    AddParameterWhere("@BP", cbBoPhan.EditValue)
                ' If doUpdate("DanhGiaNV_DiemHieuChinh", "Thang=@Thang AND IDNhom=@BP AND IDPhong=2") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ComitTransaction()
                ShowAlert("Đã duyệt !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try

        End If


    End Sub
End Class
