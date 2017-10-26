Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraPrinting
Imports System.IO
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmTongHopThemGioThemCongKT
    Private _exit As Boolean = False
    Private tbTongHop As DataTable
    Private tbTongHopDiLai As DataTable
    Private Sub frmBaoCaoLichThiCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        _exit = True
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
        tbDenNgay.EditValue = tg
        _exit = False
        LoadDSPhongBan()
        cbPhong.EditValue = 2
        LoadDSBoPhan()
        LoadDSNhanVien()
        LoadrcbNV()
        LoadDSSoYC()
        LoadDSSoCG()
        LoadDSNoiDungCV()
        'btTaiBaoCao.PerformClick()
        taobang()
    End Sub

    Public Sub LoadDSPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT ID,Ten FROM DEPATMENT ")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
    Public Sub LoadDSBoPhan()
        Dim tb As DataTable = ExecuteSQLDataTable(" select * from NhanSu_BoPhan ")
        If Not tb Is Nothing Then
            riLueBoPhan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
    Public Sub LoadDSNhanVien()
        On Error Resume Next
        Dim sql As String = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and TrangThai=1 and 1=1 "
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " and IDDepatment= " & cbPhong.EditValue
        End If
        If Not lueBoPhan.EditValue Is Nothing Then
            sql &= " and IDBoPhan= " & lueBoPhan.EditValue
        End If
        sql &= " order by IDBoPhan,NhanSu.ChucVu, ID"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
            ' cbNhanVien.EditValue = TaiKhoan

            ' rcbNV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadrcbNV()
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT ID,Ten FROM NHANSU WHERE Noictac=74 order by Ten asc")
        If Not tb Is Nothing Then
            rcbNV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNoiDungCV()
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        AddParameterWhere("@Loai2", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT tblTuDien.ID,tblTuDien.NoiDung,tbTmp.NoiDung AS Nhom FROM tblTuDien LEFT JOIN tblTuDien as tbTmp ON tblTuDien.IDP=tbTmp.ID and tbTmp.Loai=@loai2 WHERE tblTuDien.Loai=@Loai ORDER BY tbTmp.Ma,tblTuDien.Ma ")
        If Not tb Is Nothing Then
            rgdvNoiDung.DataSource = tb

            With rgdvNoiDung.View.Columns
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

    Public Sub LoadDSSoYC()
        Dim sql As String = " SET DATEFORMAT DMY SELECT Sophieu,KHACHHANG.ttcMa,BANGYEUCAU.NoiDung "
        sql &= " FROM BANGYEUCAU INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
        Dim sqlWhere As String = " WHERE IDLoaiYeuCau Is not null "
        Dim sqlOrder As String = " ORDER BY SoPhieu DESC "
        'If tbTuNgay.EditValue Is Nothing And Not tbDenNgay.EditValue Is Nothing Then
        '    sqlWhere &= " AND BANGYEUCAU.Ngaythang <= @DenNgay "
        'ElseIf Not tbTuNgay.EditValue Is Nothing And tbDenNgay.EditValue Is Nothing Then
        '    sqlWhere &= " AND BANGYEUCAU.Ngaythang >= @TuNgay "
        'ElseIf Not tbTuNgay.EditValue Is Nothing And Not tbDenNgay Is Nothing Then
        '    sqlWhere &= " AND BANGYEUCAU.Ngaythang Between @TuNgay And @DenNgay "
        'End If
        'AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        'AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql & sqlWhere & sqlOrder)
        If Not tb Is Nothing Then
            rcbSoYC.View.Columns.Clear()
            rcbSoYC.DataSource = tb
            With rcbSoYC.View.Columns
                Dim colSP = .AddField("Sophieu")
                colSP.Caption = "Số phiếu"
                colSP.VisibleIndex = 0
                colSP.Width = 80
                colSP.OptionsColumn.FixedWidth = True
                colSP.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
                Dim colTenHang = .AddField("ttcMa")
                colTenHang.Caption = "Mã KH"
                colTenHang.VisibleIndex = 1
                colTenHang.Width = 120
                colTenHang.OptionsColumn.FixedWidth = True
                colTenHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Tên công trình"
                colNoiDung.VisibleIndex = 2
                colNoiDung.Width = 250
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

            End With
        End If
    End Sub
    Public Sub LoadDSSoCG()
        Dim sql As String = " SET DATEFORMAT DMY SELECT Sophieu,KHACHHANG.ttcMa "
        sql &= " FROM BANGCHAOGIA INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID "
        Dim sqlWhere As String = " "
        Dim sqlOrder As String = " ORDER BY SoPhieu DESC "
        'If tbTuNgay.EditValue Is Nothing And Not tbDenNgay.EditValue Is Nothing Then
        '    sqlWhere &= " AND BANGCHAOGIA.Ngaythang <= @DenNgay "
        'ElseIf Not tbTuNgay.EditValue Is Nothing And tbDenNgay.EditValue Is Nothing Then
        '    sqlWhere &= " AND BANGCHAOGIA.Ngaythang >= @TuNgay "
        'ElseIf Not tbTuNgay.EditValue Is Nothing And Not tbDenNgay Is Nothing Then
        '    sqlWhere &= " AND BANGCHAOGIA.Ngaythang Between @TuNgay And @DenNgay "
        'End If
        'AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        'AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql & sqlWhere & sqlOrder)
        If Not tb Is Nothing Then
            riGlueSoCG.View.Columns.Clear()
            riGlueSoCG.DataSource = tb
            With rcbSoYC.View.Columns
                Dim colSP = .AddField("Sophieu")
                colSP.Caption = "Số phiếu"
                colSP.VisibleIndex = 0
                colSP.Width = 80
                colSP.OptionsColumn.FixedWidth = True
                colSP.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
                Dim colTenHang = .AddField("ttcMa")
                colTenHang.Caption = "Mã KH"
                colTenHang.VisibleIndex = 1
                colTenHang.Width = 120
                colTenHang.OptionsColumn.FixedWidth = True
                colTenHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
            End With
        End If
    End Sub
    Private Function createBand(_Ten As String, _TieuDe As String, Optional _HienTieuDe As Boolean = True) As GridBand
        Dim b As New GridBand
        b.Caption = _TieuDe
        b.Name = _Ten
        b.OptionsBand.ShowCaption = _HienTieuDe

        Return b
    End Function
    Private Function taoCot(_Ten As String, _fieldName As String, _ownerBand As GridBand, Optional _Visible As Boolean = True, Optional DoRong As Double = 75, Optional _AllowMerge As DevExpress.Utils.DefaultBoolean = DevExpress.Utils.DefaultBoolean.False) As BandedGridColumn
        Dim col As New BandedGridColumn
        col.Caption = _Ten
        col.FieldName = _fieldName
        col.Visible = _Visible
        col.Width = DoRong

        col.OptionsColumn.AllowMerge = _AllowMerge
        If _Ten = "Thêm giờ" Then
            col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            col.SummaryItem.FieldName = _fieldName
            col.SummaryItem.DisplayFormat = "{0:N2}"
        End If
        If _Ten = "Chấm công" Then
            col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            col.SummaryItem.FieldName = _fieldName
            col.SummaryItem.DisplayFormat = "{0:N2}"
        End If
        gv.Columns.Add(col)
        If _ownerBand IsNot Nothing Then
            col.OwnerBand = _ownerBand
        End If

        Return col
    End Function

    Private Sub taobang()
        gv.Columns.Clear()
        gv.Bands.Clear()
        gv.OptionsView.ColumnAutoWidth = False
        ' gv.BeginUpdate()
        Dim bandnv = gv.Bands.Add(createBand("DSNV", "NV", True))
        bandnv.Fixed = FixedStyle.Left
        Dim c As BandedGridColumn = New BandedGridColumn()
        c.Caption = "Ngày"
        c.FieldName = "Ngay"
        c.Visible = True
        c.Width = 80
        c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
        gv.Columns.Add(c)
        c.OwnerBand = bandnv
        c = New BandedGridColumn()
        c.Caption = "Thứ"
        c.FieldName = "Thu"
        c.Visible = False
        c.Width = 80
        c.OwnerBand = bandnv
        'c = New BandedGridColumn()
        'c.Caption = "Ngay2"
        'c.FieldName = "Ngay2"
        'c.Visible = False
        'c.Width = 80
        'c.OwnerBand = bandnv
        Dim tbnv As DataTable = CType(rcbNhanVien.DataSource, DataTable)
        Dim dt As New DataTable
        Dim tb As New DataTable
        If cbNhanVien.EditValue Is Nothing Then
            For i As Integer = 0 To tbnv.Rows.Count - 1
                gv.BeginUpdate()
                Dim band = createBand(tbnv.Rows(i)("ID").ToString(), tbnv.Rows(i)("Ten"), True)
                gv.Bands.Add(band)
                taoCot("CT", "CT" + band.Name, band, True, 100)
                tb.Columns.Add(New DataColumn("CT" + band.Name, System.Type.GetType("System.String")))
                taoCot("Số YC", "SoYC" + band.Name, band, True)
                tb.Columns.Add(New DataColumn("SoYC" + band.Name, System.Type.GetType("System.String")))
                taoCot("Số CG", "SoCG" + band.Name, band, True)
                tb.Columns.Add(New DataColumn("SoCG" + band.Name, System.Type.GetType("System.String")))
                taoCot("Thêm giờ", "ThemGio" + band.Name, band, True)
                tb.Columns.Add(New DataColumn("ThemGio" + band.Name, System.Type.GetType("System.Int32")))
                taoCot("Chấm công", "ThemCong" + band.Name, band, True, 75, DevExpress.Utils.DefaultBoolean.True)
                tb.Columns.Add(New DataColumn("ThemCong" + band.Name, System.Type.GetType("System.Decimal")))
                taoCot("Lương CB", "LuongCB" + band.Name, band, False)
                taoCot("Số tiền", "SoTien" + band.Name, band, False)
                gv.EndUpdate()
            Next
        Else
            gv.BeginUpdate()
            Dim band = createBand(cbNhanVien.EditValue, rcbNhanVien.GetDisplayText(cbNhanVien.EditValue), True)
            gv.Bands.Add(band)
            taoCot("CT", "CT" + band.Name, band, True)
            tb.Columns.Add(New DataColumn("CT" + band.Name, System.Type.GetType("System.String")))
            taoCot("Số YC", "SoYC" + band.Name, band, True)
            tb.Columns.Add(New DataColumn("SoYC" + band.Name, System.Type.GetType("System.String")))
            taoCot("Số CG", "SoCG" + band.Name, band, True)
            tb.Columns.Add(New DataColumn("SoCG" + band.Name, System.Type.GetType("System.String")))
            taoCot("Thêm giờ", "ThemGio" + band.Name, band, True)
            tb.Columns.Add(New DataColumn("ThemGio" + band.Name, System.Type.GetType("System.Int32")))
            taoCot("Chấm công", "ThemCong" + band.Name, band, True, 75, DevExpress.Utils.DefaultBoolean.True)
            tb.Columns.Add(New DataColumn("ThemCong" + band.Name, System.Type.GetType("System.Decimal")))
            taoCot("Lương CB", "LuongCB" + band.Name, band, False)
            taoCot("Số tiền", "SoTien" + band.Name, band, False)
            gv.EndUpdate()
        End If

        Dim tu As DateTime = tbTuNgay.EditValue
        Dim den As DateTime = tbDenNgay.EditValue
        Dim d As Integer = 0

        tb.Columns.Add(New DataColumn("Ngay", System.Type.GetType("System.DateTime")))
        tb.Columns.Add(New DataColumn("Ngay2", System.Type.GetType("System.Int32")))
        tb.Columns.Add(New DataColumn("Thu", System.Type.GetType("System.Int32")))
        Dim sql As String = meSQL.Text
        If cbSoYC.EditValue IsNot Nothing Then
            AddParameterWhere("@SoYC", CType(cbSoYC.EditValue, DataRowView).Item(0))
        Else
            sql = sql.Replace("and SoYC=@SoYC", "")
        End If
        If glueSoCG.EditValue IsNot Nothing Then
            AddParameterWhere("@SoCG", CType(glueSoCG.EditValue, DataRowView).Item(0))
        Else
            sql = sql.Replace("and SoCG=@SoCG", "")
        End If
        ' sql &= " group by Ngay,  SoYC,SoCG ,IDNgThucHien, IDkhachhang   order by Ngay desc"
        dt = ExecuteSQLDataTable(sql)
        Dim ngay As DateTime
        Dim cuoithang As Integer = den.Subtract(tu).Days
        Dim themcong1 As Object
        Dim themcong2 As Object
        Dim dongbatdau As Integer
        Dim dongcuoi As Integer = 0
        Dim max = 0
        For j = 0 To cuoithang
            '  gv.AddNewRow()
            max = 0
            ngay = tu.AddDays(j)
            tb.Rows.Add()
            tb.Rows(d)("Ngay") = ngay
            tb.Rows(d)("Ngay2") = ngay.Day
            tb.Rows(d)("Thu") = ngay.DayOfWeek()
            '   gv.SetFocusedRowCellValue("Ngay", ngay)

            For t As Integer = 1 To gv.Bands.Count - 1
                Dim band As GridBand = gv.Bands(t)

                If t = 1 Then
                    dongbatdau = d
                    dongcuoi = 0
                End If
                d = dongbatdau
                Dim idNguoiThucHien As String = gv.Bands(t).Name
                Dim row As DataRow() = dt.Select(String.Format("IDNgThucHien='{0}' and Ngay='{1}'", idNguoiThucHien.ToString, ngay))
                'For k As Integer = 1 To band.Columns.Count - 1

                'Next
                If row.Length > 0 Then
                    tb.Rows(d)("CT" & idNguoiThucHien) = row(0)("TenCongTrinh")
                    tb.Rows(d)("SoYC" & idNguoiThucHien) = row(0)("SoYC")
                    tb.Rows(d)("SoCG" & idNguoiThucHien) = row(0)("SoCG")
                    tb.Rows(d)("ThemGio" & idNguoiThucHien) = row(0)("ThemGio")

                End If
                themcong1 = tryObj2Double(dt.Compute("sum(ThemCong1)", String.Format("IDNgThucHien='{0}' and Ngay='{1}'", idNguoiThucHien.ToString, ngay)))
                themcong2 = tryObj2Double(dt.Compute("sum(ThemCong2)", String.Format("IDNgThucHien='{0}' and Ngay='{1}'", idNguoiThucHien.ToString, ngay)))
                tb.Rows(d)("ThemCong" & idNguoiThucHien) = DBNull.Value

                If themcong1 >= 4 Then
                    tb.Rows(d)("ThemGio" & idNguoiThucHien) = tb.Rows(d)("ThemGio" & idNguoiThucHien) - row(0)("ThemCong1")

                End If
                If themcong2 >= 4 Then
                    tb.Rows(d)("ThemGio" & idNguoiThucHien) = tb.Rows(d)("ThemGio" & idNguoiThucHien) - row(0)("ThemCong2")

                End If
                If ngay.DayOfWeek() = DayOfWeek.Sunday Then
                    If themcong1 >= 4 Then
                        tb.Rows(d)("ThemCong" & idNguoiThucHien) = tryObj2Double(tb.Rows(d)("ThemCong" & idNguoiThucHien)) + 0.5
                    End If
                    If themcong2 >= 4 Then
                        tb.Rows(d)("ThemCong" & idNguoiThucHien) = tryObj2Double(tb.Rows(d)("ThemCong" & idNguoiThucHien)) + 0.5
                    End If
                Else
                    tb.Rows(d)("ThemCong" & idNguoiThucHien) = 1
                End If

                Dim themcong = tb.Rows(d)("ThemCong" & idNguoiThucHien)
                Dim dem As Integer = 0

                If row.Length > 1 Then
                    Do While max < row.Length - 1 'And t <> 1
                        max += 1
                        tb.Rows.Add()
                    Loop
                    'If max < row.Length And t <> 1 Then
                    '    max = row.Length
                    '    tb.Rows.Add()
                    'End If
                    Do While dem < row.Length - 1
                       
                        'If t = 1 Then
                        '    max = row.Length
                        '    tb.Rows.Add()
                        'End If
                        d += 1
                        tb.Rows(d)("Ngay") = DBNull.Value
                        tb.Rows(d)("Ngay2") = ngay.Day
                        tb.Rows(d)("Thu") = ngay.DayOfWeek()
                        tb.Rows(d)("CT" & idNguoiThucHien) = row(dem + 1)("TenCongTrinh")
                        tb.Rows(d)("SoYC" & idNguoiThucHien) = row(dem + 1)("SoYC")
                        tb.Rows(d)("SoCG" & idNguoiThucHien) = row(dem + 1)("SoCG")
                        tb.Rows(d)("ThemGio" & idNguoiThucHien) = row(dem + 1)("ThemGio")
                        '  tb.Rows(d)("ThemCong" & idNguoiThucHien) = row(dem)("ThemCong")

                        '   themcong1 = tryObj2Double(dt.Compute("sum(ThemCong1)", String.Format("IDNgThucHien='{0}' and Ngay='{1}'", idNguoiThucHien.ToString, ngay)))
                        ' themcong2 = tryObj2Double(dt.Compute("sum(ThemCong2)", String.Format("IDNgThucHien='{0}' and Ngay='{1}'", idNguoiThucHien.ToString, ngay)))
                        '    tb.Rows(d)("ThemCong" & idNguoiThucHien) = DBNull.Value
                        If ngay.DayOfWeek() = 0 Then

                        End If

                        If themcong1 >= 4 Then
                            tb.Rows(d)("ThemGio" & idNguoiThucHien) = tb.Rows(d)("ThemGio" & idNguoiThucHien) - row(dem + 1)("ThemCong1")
                            'tb.Rows(d)("ThemCong" & idNguoiThucHien) = 0 'tb.Rows(0)("ThemCong" & idNguoiThucHien) 'tryObj2Double(tb.Rows(d)("ThemCong" & idNguoiThucHien)) + 0.5
                        End If
                        If themcong2 >= 4 Then
                            tb.Rows(d)("ThemGio" & idNguoiThucHien) = tb.Rows(d)("ThemGio" & idNguoiThucHien) - row(dem + 1)("ThemCong2")
                            '  tb.Rows(d)("ThemCong" & idNguoiThucHien) = 0 'tb.Rows(0)("ThemCong" & idNguoiThucHien) ' tryObj2Double(tb.Rows(d)("ThemCong" & idNguoiThucHien)) + 0.5
                        End If
                        tb.Rows(d)("ThemCong" & idNguoiThucHien) = 0
                        dem += 1
                    Loop
                    If dem > dongcuoi Then
                        dongcuoi = dem
                        'If t <> 1 Then
                        '    tb.Rows.Add()
                        'End If

                    End If

                End If
                If t = gv.Bands.Count - 1 Then
                    d = dongbatdau + dongcuoi
                End If
            Next

            d += 1
        Next
        gc.DataSource = tb
        gv.OptionsSelection.EnableAppearanceFocusedRow = True
        tbTongHop = tb
        '      gc.RefreshDataSource()
    End Sub
    Private Function taoCotDiLai(_Ten As String, _fieldName As String, _ownerBand As GridBand, Optional _Visible As Boolean = True, Optional DoRong As Double = 75, Optional _AllowMerge As DevExpress.Utils.DefaultBoolean = DevExpress.Utils.DefaultBoolean.False) As BandedGridColumn
        Dim col As New BandedGridColumn
        col.Caption = _Ten
        col.FieldName = _fieldName
        col.Visible = _Visible
        col.Width = DoRong
        col.OptionsColumn.AllowMerge = _AllowMerge
        If _Ten = "Km" Then
            col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            col.SummaryItem.FieldName = _fieldName
            col.SummaryItem.DisplayFormat = "{0:N2}"
        End If
        gvDiLai.Columns.Add(col)
        If _ownerBand IsNot Nothing Then
            col.OwnerBand = _ownerBand
        End If

        Return col
    End Function
    Private Sub taobangDiLai()
        gvDiLai.Columns.Clear()
        gvDiLai.Bands.Clear()
        gvDiLai.OptionsView.ColumnAutoWidth = False
        ' gvDiLai.BeginUpdate()
        Dim bandnv = createBand("DSNV", "NV", True)
        gvDiLai.Bands.Add(bandnv)
        bandnv.Fixed = FixedStyle.Left
        Dim c As BandedGridColumn = New BandedGridColumn()
        c.Caption = "Ngày"
        c.FieldName = "Ngay"
        c.Visible = True
        c.Width = 80
        c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
        gvDiLai.Columns.Add(c)
        c.OwnerBand = bandnv
        c = New BandedGridColumn()
        c.Caption = "Thứ"
        c.FieldName = "Thu"
        c.Visible = False
        c.Width = 80
        c.OwnerBand = bandnv
        Dim tbnv As DataTable = CType(rcbNhanVien.DataSource, DataTable)
        Dim dt As New DataTable
        Dim tb As New DataTable
        If cbNhanVien.EditValue Is Nothing Then
            For i As Integer = 0 To tbnv.Rows.Count - 1
                gvDiLai.BeginUpdate()
                Dim band = createBand(tbnv.Rows(i)("ID").ToString(), tbnv.Rows(i)("Ten"), True)
                gvDiLai.Bands.Add(band)
                taoCotDiLai("CT", "CT" + band.Name, band, True, 100)
                tb.Columns.Add(New DataColumn("CT" + band.Name, System.Type.GetType("System.String")))
                taoCotDiLai("IdKH", "IdKH" + band.Name, band, False, 100)
                tb.Columns.Add(New DataColumn("IdKH" + band.Name, System.Type.GetType("System.String")))
                taoCotDiLai("Km", "SoKm" + band.Name, band, True)
                tb.Columns.Add(New DataColumn("SoKm" + band.Name, System.Type.GetType("System.Decimal")))
                taoCotDiLai("IdSoKM", "IdSoKM" + band.Name, band, False)
                tb.Columns.Add(New DataColumn("IdSoKM" + band.Name, System.Type.GetType("System.Int32")))
                gvDiLai.EndUpdate()
            Next
        Else
            gvDiLai.BeginUpdate()
            Dim band = createBand(cbNhanVien.EditValue, rcbNhanVien.GetDisplayText(cbNhanVien.EditValue), True)
            gvDiLai.Bands.Add(band)
            taoCotDiLai("CT", "CT" + band.Name, band, True)
            tb.Columns.Add(New DataColumn("CT" + band.Name, System.Type.GetType("System.String")))
            taoCotDiLai("IdKH", "IdKH" + band.Name, band, False, 100)
            tb.Columns.Add(New DataColumn("IdKH" + band.Name, System.Type.GetType("System.String")))
            taoCotDiLai("Km", "SoKm" + band.Name, band, True)
            tb.Columns.Add(New DataColumn("SoKm" + band.Name, System.Type.GetType("System.Decimal")))
            taoCotDiLai("IdSoKM", "IdSoKM" + band.Name, band, False)
            tb.Columns.Add(New DataColumn("IdSoKM" + band.Name, System.Type.GetType("System.Int32")))
            gvDiLai.EndUpdate()
        End If

        Dim tu As DateTime = tbTuNgay.EditValue
        Dim den As DateTime = tbDenNgay.EditValue
        Dim d As Integer = 0

        tb.Columns.Add(New DataColumn("Ngay", System.Type.GetType("System.DateTime")))
        tb.Columns.Add(New DataColumn("Ngay2", System.Type.GetType("System.Int32")))
        tb.Columns.Add(New DataColumn("Thu", System.Type.GetType("System.Int32")))
        Dim sql As String = meSqlDiLai.Text
 
        ' sql &= " group by Ngay,  SoYC,SoCG ,IDNgThucHien, IDkhachhang   order by Ngay desc"
        dt = ExecuteSQLDataTable(sql)
        Dim ngay As DateTime
        Dim cuoithang As Integer = den.Subtract(tu).Days
        Dim dongbatdau As Integer
        Dim dongcuoi As Integer = 0
        Dim max = 0
        For j = 0 To cuoithang
            max = 0
            gvDiLai.AddNewRow()
            ngay = tu.AddDays(j)
            tb.Rows.Add()
            tb.Rows(d)("Ngay") = ngay
            tb.Rows(d)("Ngay2") = ngay.Day
            tb.Rows(d)("Thu") = ngay.DayOfWeek()
            '   gvDiLai.SetFocusedRowCellValue("Ngay", ngay)
            For t As Integer = 1 To gvDiLai.Bands.Count - 1
                If t = 1 Then
                    dongbatdau = d
                    dongcuoi = 0
                End If
                d = dongbatdau
                Dim idNguoiThucHien As String = gvDiLai.Bands(t).Name
                Dim row As DataRow() = dt.Select(String.Format("IDNgThucHien='{0}' and Ngay='{1}'", idNguoiThucHien.ToString, ngay))
                If row.Length > 0 Then
                    tb.Rows(d)("CT" & idNguoiThucHien) = row(0)("TenCongTrinh")
                    tb.Rows(d)("IdKH" & idNguoiThucHien) = row(0)("IDkhachhang")
                    tb.Rows(d)("SoKm" & idNguoiThucHien) = row(0)("SoKm")
                    tb.Rows(d)("IdSoKm" & idNguoiThucHien) = row(0)("IdSoKm")
                End If

                Dim dem As Integer = 0
                If row.Length > 1 Then
                    Do While max < row.Length - 1
                        max += 1
                        tb.Rows.Add()
                    Loop
                    Do While dem < row.Length - 1

                        'If t = 1 Then
                        '    max = row.Length
                        '    tb.Rows.Add()
                        'End If
                        d += 1
                        tb.Rows(d)("Ngay") = ngay
                        tb.Rows(d)("Ngay2") = ngay.Day
                        tb.Rows(d)("Thu") = ngay.DayOfWeek()
                        tb.Rows(d)("CT" & idNguoiThucHien) = row(dem + 1)("TenCongTrinh")
                        tb.Rows(d)("IdKH" & idNguoiThucHien) = row(0)("IDkhachhang")
                        tb.Rows(d)("SoKm" & idNguoiThucHien) = row(dem + 1)("SoKm")
                        tb.Rows(d)("IdSoKm" & idNguoiThucHien) = row(0)("IdSoKm")
                        dem += 1
                    Loop
                    If dem > dongcuoi Then
                        dongcuoi = dem
                       
                    End If
                End If
                If t = gv.Bands.Count - 1 Then
                    d = dongbatdau + dongcuoi
                End If
            Next

            d += 1
        Next
        gcDiLai.DataSource = tb
        gvDiLai.OptionsSelection.EnableAppearanceFocusedRow = True
        tbTongHopDiLai = tb
        '      gc.RefreshDataSource()
    End Sub
    Private Function taoCotTH(_Ten As String, _fieldName As String, Optional _Visible As Boolean = True, Optional DoRong As Double = 75, Optional _AllowMerge As DevExpress.Utils.DefaultBoolean = DevExpress.Utils.DefaultBoolean.False) As GridColumn
        Dim col As New GridColumn
        col.Caption = _Ten
        col.FieldName = _fieldName
        col.Visible = _Visible
        col.Width = DoRong

        col.OptionsColumn.AllowMerge = _AllowMerge
        gvTH.Columns.Add(col)

        Return col
    End Function
    Private Sub taobangTH()
        gvTH.Columns.Clear()
        Dim tu As DateTime = tbTuNgay.EditValue
        Dim den As DateTime = tbDenNgay.EditValue
        Dim cuoithang As Integer = den.Subtract(tu).Days + 1
        Dim tb As New DataTable
        Dim tbnv As DataTable = CType(rcbNhanVien.DataSource, DataTable)
        Dim col As GridColumn
        col = taoCotTH("Stt", "Stt", True, 40)
        col.VisibleIndex = 0
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("Stt", System.Type.GetType("System.Int32")))
        taoCotTH("Họ và tên", "HoTen", True, 150).VisibleIndex = 1
        tb.Columns.Add(New DataColumn("HoTen", System.Type.GetType("System.String")))
        col = taoCotTH("CV", "CV", True, 30)
        col.VisibleIndex = 2
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("CV", System.Type.GetType("System.String")))
        taoCotTH("IDNV", "IDNV", False, 30)
        tb.Columns.Add(New DataColumn("IDNV", System.Type.GetType("System.Int32")))
        For i = 1 To cuoithang
            gvTH.BeginUpdate()
            col = taoCotTH(i.ToString(), "Ngay" + i.ToString(), True, 25)
            col.VisibleIndex = i + 2
            If tu.AddDays(i - 1).DayOfWeek = DayOfWeek.Sunday Then
                col.AppearanceCell.BackColor = Color.LightGreen
                col.AppearanceHeader.BackColor = Color.LightGreen
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            End If
            tb.Columns.Add(New DataColumn("Ngay" + i.ToString(), System.Type.GetType("System.Int32")))
            gvTH.EndUpdate()
        Next
        Dim vitri = cuoithang + 2
        'giờ x1
        col = taoCotTH("Giờ x1", "GioX1", False, 40)
        col.VisibleIndex = vitri + 1
        col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col.SummaryItem.FieldName = "GioX1"
        col.SummaryItem.DisplayFormat = "{0:N0}"
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("GioX1", System.Type.GetType("System.Int32")))
        'giờ x2
        col = taoCotTH("Giờ x2", "GioX2", False, 40)
        col.VisibleIndex = vitri + 2
        col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col.SummaryItem.FieldName = "GioX2"
        col.SummaryItem.DisplayFormat = "{0:N0}"
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("GioX2", System.Type.GetType("System.Int32")))
        'tổng
        col = taoCotTH("T", "Tong", False, 40)
        col.VisibleIndex = vitri + 3
        col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col.SummaryItem.FieldName = "Tong"
        col.SummaryItem.DisplayFormat = "{0:N0}"
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("Tong", System.Type.GetType("System.Int32")))
        'công chủ nhật
        col = taoCotTH("Công CN", "CongCN", False, 40)
        col.VisibleIndex = vitri + 4
        col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col.SummaryItem.FieldName = "CongCN"
        col.SummaryItem.DisplayFormat = "{0:N0}"
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("CongCN", System.Type.GetType("System.Int32")))
        'xác nhận
        col = taoCotTH("Xác nhận", "XacNhan", False, 100)
        col.VisibleIndex = vitri + 5
        If cbNhanVien.EditValue Is Nothing Then
            For i = 0 To tbnv.Rows.Count - 1
                tb.Rows.Add()
                tb.Rows(i)("Stt") = i + 1
                tb.Rows(i)("HoTen") = tbnv.Rows(i)("Ten")
                tb.Rows(i)("IDNV") = tbnv.Rows(i)("ID")
                tb.Rows(i)("CV") = "NV"
                Dim idnv As String = tb.Rows(i)("IDNV").ToString()
                For j = 1 To cuoithang
                    tb.Rows(i)("Ngay" + j.ToString()) = tryObj2Double(tbTongHop.Compute("sum(ThemGio" & idnv & ")", String.Format("Ngay2={0}", j)))
                    If tb.Rows(i)("Ngay" + j.ToString()) = 0 Then
                        tb.Rows(i)("Ngay" + j.ToString()) = DBNull.Value
                    End If
                Next
                tb.Rows(i)("GioX1") = tbTongHop.Compute("sum(ThemGio" & idnv & ")", "")
                tb.Rows(i)("Tong") = tryObj2Double(tb.Rows(i)("GioX1")) + tryObj2Double(tb.Rows(i)("GioX2"))
                tb.Rows(i)("CongCN") = tbTongHop.Compute("count(ThemCong" & idnv & ")", "Thu=0 and ThemCong" & idnv & "=1")
            Next
        Else
            tb.Rows.Add()
            tb.Rows(0)("Stt") = 1
            tb.Rows(0)("IDNV") = cbNhanVien.EditValue
            tb.Rows(0)("HoTen") = rcbNhanVien.GetDisplayText(cbNhanVien.EditValue)
            tb.Rows(0)("CV") = "NV"
            Dim idnv As String = cbNhanVien.EditValue
            For j = 1 To cuoithang
                tb.Rows(0)("Ngay" + j.ToString()) = tryObj2Double(tbTongHop.Compute("sum(ThemGio" & idnv & ")", String.Format("Ngay2={0}", j)))
                If tb.Rows(0)("Ngay" + j.ToString()) = 0 Then
                    tb.Rows(0)("Ngay" + j.ToString()) = DBNull.Value
                End If
            Next
            tb.Rows(0)("GioX1") = tbTongHop.Compute("sum(ThemGio" & idnv & ")", "")
            tb.Rows(0)("Tong") = tryObj2Double(tb.Rows(0)("GioX1")) + tryObj2Double(tb.Rows(0)("GioX2"))
        End If
      
        gcTH.DataSource = tb
        gvTH.OptionsSelection.EnableAppearanceFocusedRow = True
    End Sub
    Private Function taoCotTHChamCong(_Ten As String, _fieldName As String, Optional _Visible As Boolean = True, Optional DoRong As Double = 75, Optional _AllowMerge As DevExpress.Utils.DefaultBoolean = DevExpress.Utils.DefaultBoolean.False) As GridColumn
        Dim col As New GridColumn
        col.Caption = _Ten
        col.FieldName = _fieldName
        col.Visible = _Visible
        col.Width = DoRong

        col.OptionsColumn.AllowMerge = _AllowMerge
        gvTHChamCong.Columns.Add(col)

        Return col
    End Function
    Private Sub taobangTHChamCong()
        gvTHChamCong.Columns.Clear()
        Dim tu As DateTime = tbTuNgay.EditValue
        Dim den As DateTime = tbDenNgay.EditValue
        Dim cuoithang As Integer = den.Subtract(tu).Days + 1
        Dim tb As New DataTable
        Dim tbnv As DataTable = CType(rcbNhanVien.DataSource, DataTable)
        Dim col As GridColumn
        col = taoCotTHChamCong("Stt", "Stt", True, 40)
        col.VisibleIndex = 0
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("Stt", System.Type.GetType("System.Int32")))
        taoCotTHChamCong("Họ và tên", "HoTen", True, 150).VisibleIndex = 1
        tb.Columns.Add(New DataColumn("HoTen", System.Type.GetType("System.String")))
        col = taoCotTHChamCong("CV", "CV", True, 30)
        col.VisibleIndex = 2
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("CV", System.Type.GetType("System.String")))
        taoCotTHChamCong("IDNV", "IDNV", False, 30)
        tb.Columns.Add(New DataColumn("IDNV", System.Type.GetType("System.Int32")))
        For i = 1 To cuoithang
            gvTHChamCong.BeginUpdate()
            col = taoCotTHChamCong(i.ToString(), "Ngay" + i.ToString(), True, 30)
            col.VisibleIndex = i + 2
            If tu.AddDays(i - 1).DayOfWeek = DayOfWeek.Sunday Then
                col.AppearanceCell.BackColor = Color.LightGreen
                col.AppearanceHeader.BackColor = Color.LightGreen
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            End If
            tb.Columns.Add(New DataColumn("Ngay" + i.ToString(), System.Type.GetType("System.Decimal")))
            gvTHChamCong.EndUpdate()
        Next
        Dim vitri = cuoithang + 2

        'tổng
        col = taoCotTHChamCong("T", "Tong", False, 40)
        col.VisibleIndex = vitri + 1
        col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col.SummaryItem.FieldName = "Tong"
        col.SummaryItem.DisplayFormat = "{0:N0}"
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("Tong", System.Type.GetType("System.Decimal")))
        'xác nhận
        col = taoCotTHChamCong("Xác nhận", "XacNhan", False, 100)
        col.VisibleIndex = vitri + 2
        If cbNhanVien.EditValue Is Nothing Then
            For i = 0 To tbnv.Rows.Count - 1
                tb.Rows.Add()
                tb.Rows(i)("Stt") = i + 1
                tb.Rows(i)("HoTen") = tbnv.Rows(i)("Ten")
                tb.Rows(i)("IDNV") = tbnv.Rows(i)("ID")
                Dim idnv As String = tb.Rows(i)("IDNV").ToString()
                tb.Rows(i)("CV") = "NV"
                For j = 1 To cuoithang
                    tb.Rows(i)("Ngay" + j.ToString()) = tryObj2Double(tbTongHop.Compute("sum(ThemCong" & idnv & ")", String.Format("Ngay2={0}", j)))
                    If tb.Rows(i)("Ngay" + j.ToString()) = 0 Then
                        tb.Rows(i)("Ngay" + j.ToString()) = DBNull.Value
                    End If
                Next
                '    tb.Rows(i)("GioX1") = tbTongHop.Compute("sum(themcong" & idnv & ")", "")
                tb.Rows(i)("Tong") = tbTongHop.Compute("sum(ThemCong" & idnv & ")", "")

            Next
        Else
            tb.Rows.Add()
            tb.Rows(0)("Stt") = 1
            tb.Rows(0)("IDNV") = cbNhanVien.EditValue
            tb.Rows(0)("HoTen") = rcbNhanVien.GetDisplayText(cbNhanVien.EditValue)
            tb.Rows(0)("CV") = "NV"
            Dim idnv As String = cbNhanVien.EditValue
            For j = 1 To cuoithang
                tb.Rows(0)("Ngay" + j.ToString()) = tryObj2Double(tbTongHop.Compute("sum(ThemCong" & idnv & ")", String.Format("Ngay2={0}", j)))
                If tb.Rows(0)("Ngay" + j.ToString()) = 0 Then
                    tb.Rows(0)("Ngay" + j.ToString()) = DBNull.Value
                End If
            Next
            tb.Rows(0)("Tong") = tbTongHop.Compute("sum(ThemCong" & idnv & ")", "")
        End If
       
        gcTHChamCong.DataSource = tb
        gvTHChamCong.OptionsSelection.EnableAppearanceFocusedRow = True
    End Sub
    Private Function taoCotTHDiLai(_Ten As String, _fieldName As String, Optional _Visible As Boolean = True, Optional DoRong As Double = 75, Optional _AllowMerge As DevExpress.Utils.DefaultBoolean = DevExpress.Utils.DefaultBoolean.False) As GridColumn
        Dim col As New GridColumn
        col.Caption = _Ten
        col.FieldName = _fieldName
        col.Visible = _Visible
        col.Width = DoRong

        col.OptionsColumn.AllowMerge = _AllowMerge
        gvDiLaiTH.Columns.Add(col)

        Return col
    End Function
    Private Sub taobangDiLaiTH()
        gvDiLaiTH.Columns.Clear()
        Dim tu As DateTime = tbTuNgay.EditValue
        Dim den As DateTime = tbDenNgay.EditValue
        Dim cuoithang As Integer = den.Subtract(tu).Days + 1
        Dim tb As New DataTable
        Dim tbnv As DataTable = CType(rcbNhanVien.DataSource, DataTable)
        Dim col As GridColumn
        col = taoCotTHDiLai("Stt", "Stt", True, 40)
        col.VisibleIndex = 0
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("Stt", System.Type.GetType("System.Int32")))
        taoCotTHDiLai("Họ và tên", "HoTen", True, 150).VisibleIndex = 1
        tb.Columns.Add(New DataColumn("HoTen", System.Type.GetType("System.String")))
        col = taoCotTHDiLai("CV", "CV", True, 30)
        col.VisibleIndex = 2
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("CV", System.Type.GetType("System.String")))
        taoCotTHDiLai("IDNV", "IDNV", False, 30)
        tb.Columns.Add(New DataColumn("IDNV", System.Type.GetType("System.Int32")))
        For i = 1 To cuoithang
            gvDiLaiTH.BeginUpdate()
            col = taoCotTHDiLai(i.ToString(), "Ngay" + i.ToString(), True, 30)
            col.VisibleIndex = i + 2
            If tu.AddDays(i - 1).DayOfWeek = DayOfWeek.Sunday Then
                col.AppearanceCell.BackColor = Color.LightGreen
                col.AppearanceHeader.BackColor = Color.LightGreen
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            End If
            tb.Columns.Add(New DataColumn("Ngay" + i.ToString(), System.Type.GetType("System.Decimal")))
            gvDiLaiTH.EndUpdate()
        Next
        Dim vitri = cuoithang + 2

        'tổng
        col = taoCotTHDiLai("T", "Tong", False, 40)
        col.VisibleIndex = vitri + 1
        col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col.SummaryItem.FieldName = "Tong"
        col.SummaryItem.DisplayFormat = "{0:N0}"
        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.Columns.Add(New DataColumn("Tong", System.Type.GetType("System.Int32")))
        'xác nhận
        col = taoCotTHDiLai("Xác nhận", "XacNhan", False, 100)
        col.VisibleIndex = vitri + 2
        If cbNhanVien.EditValue Is Nothing Then
            For i = 0 To tbnv.Rows.Count - 1
                tb.Rows.Add()
                tb.Rows(i)("Stt") = i + 1
                tb.Rows(i)("HoTen") = tbnv.Rows(i)("Ten")
                tb.Rows(i)("IDNV") = tbnv.Rows(i)("ID")
                Dim idnv As String = tb.Rows(i)("IDNV").ToString()
                tb.Rows(i)("CV") = "NV"
                For j = 1 To cuoithang
                    tb.Rows(i)("Ngay" + j.ToString()) = tryObj2Double(tbTongHopDiLai.Compute("sum(SoKM" & idnv & ")", String.Format("Ngay2={0}", j)))
                    If tb.Rows(i)("Ngay" + j.ToString()) = 0 Then
                        tb.Rows(i)("Ngay" + j.ToString()) = DBNull.Value
                    End If
                Next
                tb.Rows(i)("Tong") = tbTongHopDiLai.Compute("sum(SoKM" & idnv & ")", "")

            Next
        Else
            tb.Rows.Add()
            tb.Rows(0)("Stt") = 1
            tb.Rows(0)("HoTen") = rcbNhanVien.GetDisplayText(cbNhanVien.EditValue)
            tb.Rows(0)("IDNV") = cbNhanVien.EditValue
            Dim idnv As String = cbNhanVien.EditValue
            tb.Rows(0)("CV") = "NV"
            For j = 1 To cuoithang
                tb.Rows(0)("Ngay" + j.ToString()) = tryObj2Double(tbTongHopDiLai.Compute("sum(SoKM" & idnv & ")", String.Format("Ngay2={0}", j)))
                If tb.Rows(0)("Ngay" + j.ToString()) = 0 Then
                    tb.Rows(0)("Ngay" + j.ToString()) = DBNull.Value
                End If
            Next
            tb.Rows(0)("Tong") = tbTongHopDiLai.Compute("sum(SoKM" & idnv & ")", "")
        End If
     
        gcDiLaiTH.DataSource = tb
        gvDiLaiTH.OptionsSelection.EnableAppearanceFocusedRow = True
    End Sub
    Private Sub tbTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTuNgay.EditValueChanged, tbDenNgay.EditValueChanged
        If _exit Then Exit Sub
        LoadDSSoYC()
        If CType(sender, DevExpress.XtraBars.BarEditItem).Name = tbDenNgay.Name Then
            tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
        End If
    End Sub

    Private Sub rtbTuNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbTuNgay.ButtonClick
        If e.Button.Index = 1 Then
            tbTuNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub rtbDenNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbDenNgay.ButtonClick
        If e.Button.Index = 1 Then
            tbDenNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbSoYC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbSoYC.ButtonClick
        If e.Button.Index = 1 Then
            cbSoYC.EditValue = Nothing
        End If
    End Sub
    Private Sub riGlueSoCG_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riGlueSoCG.ButtonClick
        If e.Button.Index = 1 Then
            glueSoCG.EditValue = Nothing
        End If
    End Sub
    Private Sub btTaiBaoCao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiBaoCao.ItemClick
        If tabThemGio.SelectedTabPage.Name = "pageTongHop" Then
            taobang()
            taobangTH()
            taobangTHChamCong()
        Else
            If tabThemGio.SelectedTabPage.Name = "pageDiLai" Then
                taobangDiLai()
                taobangDiLaiTH()
            Else
                If tabThemGio.SelectedTabPage.Name = "pageDiLaiTH" Then
                    taobangDiLai()
                    taobangDiLaiTH()
                Else
                    taobang()
                    taobangTH()
                    taobangTHChamCong()
                End If
            End If

        End If
    End Sub

    Private Sub rgdvNoiDung_Popup(sender As System.Object, e As System.EventArgs) Handles rgdvNoiDung.Popup
        CType(sender, GridLookUpEdit).Properties.View.ExpandAllGroups()
    End Sub



    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueBoPhan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueBoPhan.ButtonClick
        If e.Button.Index = 1 Then
            lueBoPhan.EditValue = Nothing
        End If
    End Sub
    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadDSNhanVien()
        cbNhanVien.EditValue = Nothing
    End Sub
    Private Sub lueBoPhan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueBoPhan.EditValueChanged
        LoadDSNhanVien()
        cbNhanVien.EditValue = Nothing
    End Sub
    Private Sub gv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gv.RowStyle
        Dim view As BandedGridView = CType(sender, BandedGridView)
        Dim viewInfo As GridViewInfo = CType(view.GetViewInfo(), GridViewInfo)
        If e.RowHandle = View.FocusedRowHandle Then
            e.Appearance.Assign(viewInfo.PaintAppearance.FocusedRow)
        End If
    End Sub

    Private Sub gv_RowCellStyle(sender As Object, e As Views.Grid.RowCellStyleEventArgs) Handles gv.RowCellStyle
        If gv.GetRowCellValue(e.RowHandle, "Thu") = 0 Then
            e.Appearance.BackColor = Color.LightGreen
        End If
        If e.Column.FieldName = "Ngay" Then
            e.Appearance.Font = New Font(Me.Font.Name, 8, FontStyle.Bold)
        End If
        Dim view As BandedGridView = CType(sender, BandedGridView)
        Dim viewInfo As GridViewInfo = CType(view.GetViewInfo(), GridViewInfo)


    End Sub
    Private Sub gvDiLai_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gvDiLai.RowStyle
        Dim view As BandedGridView = CType(sender, BandedGridView)
        Dim viewInfo As GridViewInfo = CType(view.GetViewInfo(), GridViewInfo)
        If e.RowHandle = view.FocusedRowHandle Then
            e.Appearance.Assign(viewInfo.PaintAppearance.FocusedRow)
        End If
    End Sub
    Private Sub gvDiLai_RowCellStyle(sender As Object, e As Views.Grid.RowCellStyleEventArgs) Handles gvDiLai.RowCellStyle
        If gvDiLai.GetRowCellValue(e.RowHandle, "Thu") = 0 Then
            e.Appearance.BackColor = Color.LightGreen
        End If
        If e.Column.FieldName = "Ngay" Then
            e.Appearance.Font = New Font(Me.Font.Name, 8, FontStyle.Bold)
        End If
        Dim view As BandedGridView = CType(sender, BandedGridView)
        Dim viewInfo As GridViewInfo = CType(view.GetViewInfo(), GridViewInfo)


    End Sub
    Private Sub btnKetXuat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick
        Dim gvexprort As Object = gv
        Dim tu As DateTime = tbTuNgay.EditValue
        Dim den As DateTime = tbDenNgay.EditValue
        Dim filename As String = ""
        If tabThemGio.SelectedTabPage.Name = "pageTongHop" Then
            gvexprort = New GridView
            gvexprort = gvTH
            filename = "Tổng hợp thêm giờ tháng " & tu.Month & "-" & tu.Year

            With gvexprort
                .OptionsView.ShowViewCaption = True
                .ViewCaption = filename
            End With

        Else
            If tabThemGio.SelectedTabPage.Name = "pageChiTiet" Then
                gvexprort = New BandedGridView
                gvexprort = gv
                For t As Integer = 1 To gvexprort.Bands.Count - 1
                    Dim idNguoiThucHien As String = gvexprort.Bands(t).Name
                    gvexprort.Columns("LuongCB" + idNguoiThucHien).Visible = True
                    gvexprort.Columns("SoTien" + idNguoiThucHien).Visible = True
                    gvexprort.Columns("ThemCong" + idNguoiThucHien).Visible = False
                Next
                filename = "Chi tiết thêm giờ tháng " & tu.Month & "-" & tu.Year
                With gvexprort
                    .OptionsView.ShowViewCaption = True
                    .ViewCaption = filename
                End With
            Else
                If tabThemGio.SelectedTabPage.Name = "pageDiLai" Then
                    gvexprort = New BandedGridView
                    gvexprort = gvDiLai
                    filename = "Chi tiết hỗ trợ đi lại " & tu.Month & "-" & tu.Year

                    With gvexprort
                        .OptionsView.ShowViewCaption = True
                        .ViewCaption = filename
                    End With
                Else
                    If tabThemGio.SelectedTabPage.Name = "pageDiLaiTH" Then
                        gvexprort = New GridView
                        gvexprort = gvDiLaiTH
                        filename = "Tổng hợp hỗ trợ đi lại tháng " & tu.Month & "-" & tu.Year
                        With gvexprort
                            .OptionsView.ShowViewCaption = True
                            .ViewCaption = filename
                        End With
                    Else
                        If tabThemGio.SelectedTabPage.Name = "pageTHChamCong" Then
                            gvexprort = New GridView
                            gvexprort = gvTHChamCong
                            filename = "Tổng hợp chấm công tháng " & tu.Month & "-" & tu.Year
                            With gvexprort
                                .OptionsView.ShowViewCaption = True
                                .ViewCaption = filename
                            End With
                        End If
                    End If
                End If
            End If
        End If
        Dim saveDialog As SaveFileDialog = New SaveFileDialog()
        Try
            saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx"
            saveDialog.FileName = filename
            If saveDialog.ShowDialog() = DialogResult.OK Then
                ShowWaiting("Đang kết xuất ...")
                Dim exportFilePath As String = saveDialog.FileName
                Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
                Dim str As String
                Dim tuychon As XlsExportOptions = New XlsExportOptions
                Dim tuychonx As XlsxExportOptions = New XlsxExportOptions

                tuychon.ShowGridLines() = True
                tuychonx.ShowGridLines() = True
                Select Case fileExtenstion
                    Case ".xls"
                        Try
                            gvexprort.ExportToXls(exportFilePath, tuychon)
                        Catch ex As Exception
                            ShowBaoLoi(LoiNgoaiLe)
                        End Try

                    Case (".xlsx")
                        Try
                            gvexprort.ExportToXlsx(exportFilePath, tuychonx)
                        Catch ex As Exception
                            ShowBaoLoi(LoiNgoaiLe)
                        End Try

                End Select

                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    CloseWaiting()
                    If File.Exists(exportFilePath) Then
                        Try
                            System.Diagnostics.Process.Start(exportFilePath)
                        Catch ex As Exception
                            str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                            ShowBaoLoi(str)
                        End Try
                    Else
                        str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                        ShowBaoLoi(str)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
            CloseWaiting()
        End Try
        If tabThemGio.SelectedTabPage.Name = "pageTongHop" Then
            gvTH.OptionsView.ShowViewCaption = False
        Else
            If tabThemGio.SelectedTabPage.Name = "pageDiLai" Then
                gvDiLai.OptionsView.ShowViewCaption = False
            Else
                If tabThemGio.SelectedTabPage.Name = "pageDiLaiTH" Then
                    gvDiLaiTH.OptionsView.ShowViewCaption = False
                Else
                    If tabThemGio.SelectedTabPage.Name = "pageChiTiet" Then
                        For t As Integer = 1 To gv.Bands.Count - 1
                            Dim idNguoiThucHien As String = gv.Bands(t).Name
                            gvexprort.Columns("LuongCB" + idNguoiThucHien).Visible = False
                            gvexprort.Columns("SoTien" + idNguoiThucHien).Visible = False
                            gvexprort.Columns("ThemCong" + idNguoiThucHien).Visible = True
                        Next
                        gv.OptionsView.ShowViewCaption = False
                    Else
                        If tabThemGio.SelectedTabPage.Name = "pageTHChamCong" Then
                            gvTHChamCong.OptionsView.ShowViewCaption = False
                        End If
                    End If
                 
                End If

            End If
        End If

        CloseWaiting()
        '  gv.OptionsView.ShowViewCaption = False
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Alt Or Keys.O) Then
            If meSQL.Visible = True Then
                meSQL.Visible = False
                meSqlDiLai.Visible = False
            Else
                meSQL.Visible = True
                meSqlDiLai.Visible = True
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub gv_CellMerge(sender As Object, e As Views.Grid.CellMergeEventArgs) Handles gv.CellMerge
        If e.Column.Caption = "Chấm công" Then
            '   e.Column.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            Dim view As GridView = CType(sender, GridView)
            Dim val1 As Object = (view.GetRowCellValue(e.RowHandle1, e.Column.FieldName))
            Dim val2 As Object = (view.GetRowCellValue(e.RowHandle2, e.Column.FieldName))
            Dim ngay1 As Object
            Dim ngay2 As Object

            If Not IsDBNull((view.GetRowCellValue(e.RowHandle1, "Ngay2"))) And (view.GetRowCellValue(e.RowHandle1, "Ngay2")) IsNot Nothing And Not IsDBNull((view.GetRowCellValue(e.RowHandle2, "Ngay2"))) And (view.GetRowCellValue(e.RowHandle2, "Ngay2")) IsNot Nothing Then
                ngay1 = (view.GetRowCellValue(e.RowHandle1, "Ngay2"))
                ngay2 = (view.GetRowCellValue(e.RowHandle2, "Ngay2"))
                If ngay1 = ngay2 Then
                    If Not IsDBNull(val1) And Not IsDBNull(val2) Then
                        If val1 = 0 And val2 <> 0 Then
                            val1 = val2
                            e.Merge = (val1 = val2)
                        Else
                            If val1 <> 0 And val2 = 0 Then
                                val2 = val1
                                e.Merge = (val1 = val2)
                            Else
                                If val1 = val2 And val1 <> 0 And val2 <> 0 Then
                                    e.Merge = (val1 = val2)
                                End If
                            End If
                        End If
                    End If

                End If
            End If

          

            e.Handled = True
        Else
            Exit Sub
        End If

    End Sub
    Private Function tryObj2Double(obj As Object) As Double
        Try
            If obj Is DBNull.Value Then Return 0
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub gv_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gv.CustomSummaryCalculate
        Dim summaryID As String = Convert.ToString(TryCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).Tag)
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
            Dim dt As DataTable = CType(gv.DataSource, DataView).ToTable

        End If
    End Sub

    Private Sub tabThemGio_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabThemGio.SelectedPageChanged
        'If tabThemGio.SelectedTabPage.Name = "pageTongHop" Then
        '    taobang()
        '    taobangTH()
        'Else
        '    If tabThemGio.SelectedTabPage.Name = "pageDiLai" Then
        '        taobangDiLai()
        '    Else
        '        If tabThemGio.SelectedTabPage.Name = "pageDiLaiTH" Then
        '            taobangDiLai()
        '            taobangDiLaiTH()
        '        End If

        '    End If
        'End If
    End Sub
    Private Sub gvDiLai_MouseDown(sender As Object, e As MouseEventArgs) Handles gvDiLai.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gvDiLai.CalcHitInfo(gcDiLai.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menuDiLai.ShowPopup(gcDiLai.PointToScreen(e.Location))
            vitri = e.Location

        End If
    End Sub
    Private Shared vitri As Object

    Private Sub btnDong_Click(sender As Object, e As EventArgs) Handles btnDong.Click
        pcCNSoKM.Visible = False
        pcCNSoKM.Enabled = False
    End Sub
    Private Sub btnXacNhan_Click(sender As Object, e As EventArgs) Handles btnXacNhan.Click
        AddParameter("@IdKhachHang", lueKhachHang.EditValue)
        AddParameter("@SoKM", seSoKm.EditValue)
        AddParameter("@NgayApDung", deNgayApDung.EditValue)
        If doInsert("KHACHHANG_SoKM") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("cập nhật thành công")
        End If
    End Sub

    Private Sub gvDiLai_RowUpdated(sender As Object, e As Views.Base.RowObjectEventArgs) Handles gvDiLai.RowUpdated
        If IsDBNull(gvDiLai.GetFocusedRowCellValue("SoKM")) Then
            AddParameter("@IdKhachHang", gvDiLai.GetFocusedRowCellValue("CT"))
            AddParameter("@SoKM", gvDiLai.GetFocusedRowCellValue("SoKM"))
            '  AddParameter("@NgayApDung")
            '  Dim id = ExecuteSQLScalar("select top 1 Id from KHACHHANG_SoKM where  ")
        End If
    End Sub

   
    Private Sub mThemMoiSoKm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemMoiSoKm.ItemClick
        Dim f As frmCNSoKM = New frmCNSoKM
        TrangThai.isAddNew = True
        f.ShowDialog()
        Dim col = gvDiLai.FocusedColumn
        Dim row = gvDiLai.FocusedRowHandle
        taobangDiLai()
        gvDiLai.FocusedColumn = col
        gvDiLai.FocusedRowHandle = row
    End Sub
    Private Sub mSuaSoKm_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaSoKm.ItemClick
        Dim col As BandedGridColumn = gvDiLai.FocusedColumn
        If IsDBNull(gvDiLai.GetFocusedRowCellValue("IdSoKM" + col.OwnerBand.Name)) Then Exit Sub
        Dim f As frmCNSoKM = New frmCNSoKM
        f.idsokm = gvDiLai.GetFocusedRowCellValue("IdSoKM" + col.OwnerBand.Name)
        TrangThai.isUpdate = True
        f.ShowDialog()
        col = gvDiLai.FocusedColumn
        Dim row = gvDiLai.FocusedRowHandle
        taobangDiLai()
        gvDiLai.FocusedColumn = col
        gvDiLai.FocusedRowHandle = row
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim f As New frmKetXuatExeclTGTCKT
        f.ShowDialog()
    End Sub
End Class
