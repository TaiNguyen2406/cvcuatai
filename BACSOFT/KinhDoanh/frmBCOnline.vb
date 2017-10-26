Imports BACSOFT.Db.SqlHelper

Public Class frmBCOnline

    Private Sub frmBCOnline_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbThoiGian.EditValue = Today.Date
        loadDSPhong()
        LoadDS()
    End Sub

    Public Sub loadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            rcbPhongCT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDS()
        Dim sql As String = ""

        sql &= " SELECT tmpTb.IDNhanVien,SUM(tb2.Diem)Tong INTO #tbDiemTong FROM ("
        sql &= "  select"
        sql &= "      tb.IDNhanVien,tb.IDKyNang,"
        sql &= "      max(tb.NgayThi) as Ngay"
        sql &= "  from"
        sql &= "      tblDiemThiKyNang tb"
        sql &= "  where tb.thoigian>0 and Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & Convert.ToDateTime(tbThoiGian.EditValue).ToString("dd/MM/yyyy") & "',103) "
        sql &= "  group by IDNhanVien,IDKyNang) tmpTb  "
        sql &= "  INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi AND tb2.thoigian > 0"
        ' sql &= "  INNER JOIN NLDanhSach ON NLDanhSach.ID=tb2.IDKyNang"
        sql &= " group by tmpTb.IDNhanVien"

        sql &= " DECLARE @sql NVARCHAR(Max);"
        sql &= " SET @sql = N'';"
        sql &= " SELECT @sql = @sql + N'"
        sql &= " ' + QUOTENAME(Convert(nvarchar,f.ID)+'t')"
        sql &= "   + ' = MAX(CASE WHEN d.IDKyNang = ' "
        sql &= "   + RTRIM(f.ID) + ' THEN d.ThoiGian ELSE 0 END),'"
        sql &= "   + N'"
        sql &= " ' + QUOTENAME(Convert(nvarchar,f.ID)+'d')"
        sql &= "   + ' = MAX(CASE WHEN d.IDKyNang = ' "
        sql &= "   + RTRIM(f.ID) + ' THEN d.Diem ELSE 0 END),'"
        sql &= "   + N'"
        sql &= " ' + QUOTENAME(f.Ten )"
        sql &= "   + ' = convert(float,0),'"
        sql &= " FROM"
        sql &= " ("
        sql &= "     SELECT f.ID, f.Ten"
        sql &= "     FROM dbo.tblDiemThiKyNang AS d"
        sql &= "     INNER JOIN (SELECT NLDanhSach.ID,NLTen.Ten FROM NLDanhSach LEFT JOIN NLTen ON NLDanhSach.IDTen=NLTen.ID WHERE NLDanhSach.Loai=1) AS f"
        sql &= "     ON f.ID = d.IDKyNang"
        sql &= "     GROUP BY f.ID, f.Ten"
        sql &= " ) AS f"
        sql &= " ORDER BY f.Ten;"

        sql &= " IF @sql = N''"
        sql &= " BEGIN"
        sql &= "     SELECT ID, Ten,IDDepatment, "
        sql &= "         Result = N'Không có dữ liệu tblDiemThiKyNang'"
        sql &= "         FROM NHANSU"
        sql &= " End"
        sql &= " ELSE"
        sql &= " BEGIN"
        sql &= "     SELECT @sql = N'SELECT p.ID, p.Ten,IDDepatment,#tbDiemTong.Tong, ' + "
        sql &= "         LEFT(@sql, LEN(@sql)-1) + '"
        sql &= "             FROM "
        sql &= "                (SELECT tb2.* FROM ("
        sql &= "                select"
        sql &= "                    tb.IDNhanVien,tb.IDKyNang,"
        sql &= "                    max(tb.NgayThi) as Ngay"
        sql &= "                from"
        sql &= "                    tblDiemThiKyNang tb"
        sql &= "                where ThoiGian>0 AND Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,''" & Convert.ToDateTime(tbThoiGian.EditValue).ToString("dd/MM/yyyy") & "'',103) "
        sql &= "                group by IDNhanVien,IDKyNang) tmpTb  "
        sql &= "                INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi AND tb2.ThoiGian>0) AS d"
        sql &= " 			INNER JOIN #tbDiemTong ON #tbDiemTong.IDNhanVien=d.IDNhanVien"
        sql &= "             INNER JOIN NHANSU AS p"
        sql &= "             ON p.ID = d.IDNhanVien"
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " AND p.IDDepatment = " & cbPhong.EditValue
        End If
        sql &= "    WHERE Convert(datetime,Convert(nvarchar,d.NgayThi,103),103) <= convert(datetime,''" & Convert.ToDateTime(tbThoiGian.EditValue).ToString("dd/MM/yyyy") & "'',103) "
        sql &= "       '+'  GROUP BY p.ID, p.Ten,p.IDDepatment,#tbDiemTong.Tong"
        sql &= "             ORDER BY p.ID;';"
        sql &= "     EXEC sp_executeSQL @sql;"
        sql &= " End"
        sql &= " DROP table #tbDiemTong"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdvCT.Bands.Clear()
        gdvCT.Columns.Clear()
        If Not dt Is Nothing Then

            Dim colTen As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            colTen.FieldName = "Ten"
            colTen.Caption = "Nhân viên"
            gdvCT.Columns.Add(colTen)


            Dim colPhong As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            colPhong.FieldName = "IDDepatment"
            colPhong.Caption = "Phòng"
            gdvCT.Columns.Add(colPhong)
            gdvCT.Columns("IDDepatment").ColumnEdit = rcbPhongCT
            gdvCT.Columns("IDDepatment").SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
            gdvCT.Columns("IDDepatment").GroupIndex = 0

            Dim colTong As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
            colTong.FieldName = "Tong"
            colTong.Caption = "Tổng"
            gdvCT.Columns.Add(colTong)

            Dim bandTen As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
            bandTen.Caption = " "
            bandTen.Width = 140
            bandTen.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            bandTen.Name = "bTen"
            gdvCT.Bands.Add(bandTen)

            gdvCT.Columns("Ten").OwnerBand = gdvCT.Bands("bTen")
            gdvCT.Columns("Ten").Visible = True
            gdvCT.Columns("Ten").VisibleIndex = 0
            gdvCT.Columns("Ten").Width = 140

            gdvCT.Columns("Tong").OwnerBand = gdvCT.Bands("bTen")
            gdvCT.Columns("Tong").Visible = True
            gdvCT.Columns("Tong").VisibleIndex = 1
            gdvCT.Columns("Tong").Width = 50

            'gdvCT.Bands("bTen").Columns.Add(gdvCT.Columns("Ten"))
            'gdvCT.Columns("")
            With dt
                Dim strIndex As String = ""
                For i As Integer = 4 To .Columns.Count - 1
                    If Not IsNumeric(.Columns(i).Caption.Substring(0, .Columns(i).Caption.Length - 1)) Then
                        Dim bandCT As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        bandCT.Caption = .Columns(i).Caption
                        bandCT.Name = .Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1)
                        gdvCT.Bands.Add(bandCT)
                        gdvCT.Bands(.Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1)).Columns.Add(gdvCT.Columns(.Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1) & "t"))
                        gdvCT.Bands(.Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1)).Columns.Add(gdvCT.Columns(.Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1) & "d"))
                        gdvCT.Columns(.Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1) & "t").Visible = True
                        gdvCT.Columns(.Columns(i - 1).Caption.Substring(0, .Columns(i - 1).Caption.Length - 1) & "d").Visible = True
                    Else
                        Dim col As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                        col.FieldName = .Columns(i).Caption
                        If .Columns(i).Caption.Substring(.Columns(i).Caption.Length - 1, 1) = "t" Then
                            col.Caption = "Phút"
                        Else
                            col.Caption = "Điểm"
                        End If

                        gdvCT.Columns.Add(col)

                    End If
                Next
            End With
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Dim f As New frmCNDiemThiKyNang
        TrangThai.isAddNew = True
        f.ShowDialog()
    End Sub

    'Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
    '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
    '    If gdvCT.FocusedRowHandle < 0 Then Exit Sub
    '    Dim index As Integer = gdvCT.FocusedRowHandle
    '    TrangThai.isUpdate = True
    '    objID = gdvCT.GetFocusedRowCellValue("ID")
    '    Dim f As New frmCNDiemThiKyNang
    '    f.ShowDialog()
    '    gdvCT.FocusedRowHandle = index
    'End Sub

    'Private Sub btXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoa.ItemClick, mXoa.ItemClick
    '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
    '    If gdvCT.FocusedRowHandle < 0 Then Exit Sub
    '    If ShowCauHoi("Xoá mục được chọn ?") Then
    '        AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
    '        If doDelete("tblDiemThiKyNang", "ID=@ID") Is Nothing Then
    '            ShowBaoLoi(LoiNgoaiLe)
    '        Else
    '            ShowAlert("Đã xoá !")
    '            LoadDS()
    '        End If
    '    End If


    'End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub


End Class
