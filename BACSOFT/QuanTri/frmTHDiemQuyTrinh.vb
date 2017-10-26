Imports BACSOFT.Db.SqlHelper

Public Class frmTHDiemQuyTrinh

    Private Sub frmQuyTrinh_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbThoiGian.EditValue = Today.Date
        loadCbPhong()
        ''gdvCT.PopulateColumns()
        'Dim col As New DevExpress.XtraGrid.Columns.GridColumn
        'col.Caption = "abc"
        'col.Visible = True
        'col.VisibleIndex = 1
        'gdvCT.Columns.Add(col)


    End Sub

    Public Sub loadCbPhong()

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            rcbPhongBan.DataSource = tb
        End If
    End Sub


    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick


        Dim sql As String = ""
        sql &= " DECLARE @table2 sysname "
        sql &= " SET @table2 = 'NLDanhSach'"
        sql &= " DECLARE @id_field2 sysname "
        sql &= " SET @id_field2 = 'ID'"
        sql &= " DECLARE @sql2 varchar(MAX)"
        sql &= " SET @sql2 = 'SELECT * INTO ##tbNLDanhSach FROM ( SELECT TOP 0 CONVERT(int,0) AS [ID],CONVERT(float,0) AS [Diem], '"
        sql &= "  +'CAST(0 AS nvarchar(10)) AS [IDPhong],'"
        sql &= "  +' CONVERT(sql_variant,N'''') AS [TrangThai] WHERE 1=0 '+CHAR(10)"
        sql &= " SELECT @sql2 = @sql2 + 'UNION ALL SELECT '+@id_field2+',Diem, N'''"
        sql &= "  +COLUMN_NAME+''',CONVERT(sql_variant, '"
        sql &= "   +'['+COLUMN_NAME+']) FROM ['+@table2+'] WHERE ['+COLUMN_NAME+'] IS NOT NULL '"
        sql &= "   +CHAR(10)"
        sql &= " FROM "
        sql &= "   INFORMATION_SCHEMA.COLUMNS"
        sql &= " WHERE "
        sql &= "   TABLE_NAME = @table2 "
        sql &= "   AND COLUMN_NAME <> @id_field2  AND COLUMN_NAME <> convert(sysname,'IDPhong') AND COLUMN_NAME <> convert(sysname,'IDNhom')"
        sql &= " 	 AND COLUMN_NAME <> convert(sysname,'IDTen')  AND COLUMN_NAME <> convert(sysname,'MoTa') AND COLUMN_NAME <> convert(sysname,'Diem')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'Loai') AND COLUMN_NAME <> convert(sysname,'IDNhomKN')"
        sql &= " ORDER BY COLUMN_NAME"
        sql &= " SELECT @sql2 = @sql2 + ')tb WHERE tb.TrangThai=1'"
        sql &= " EXEC(@sql2)"

        sql &= "  DECLARE @sql NVARCHAR(Max);"
        sql &= "  SET @sql = N'';"
        sql &= "  SELECT @sql = @sql + N'"
        sql &= "                     ' + QUOTENAME(f.ID)"
        sql &= "      + ' = MAX(CASE WHEN d.IDQuyTrinh = ' "
        sql &= "      + RTRIM(f.ID) + ' THEN d.Diem ELSE 0 END),'"
        sql &= "  FROM"
        sql &= "  ("
        sql &= "      SELECT f.ID"
        sql &= "      FROM tblDiemQuyTrinh AS d"
        sql &= "      INNER JOIN ##tbNLDanhSach AS f"
        sql &= " 	 ON f.ID = d.IDQuyTrinh "
        If Not cbPhong.EditValue Is Nothing Then
            sql &= "           AND f.IDPhong = 'P" & cbPhong.EditValue & "'"
        End If
        sql &= "      GROUP BY f.ID"
        sql &= "  ) AS f;"

        sql &= "  IF @sql = N''"
        sql &= "    BEGIN"
        sql &= "      SELECT ID, Ten,IDDepatment, "
        sql &= "          Result = N'Không có dữ liệu tblDiemQuyTrinh'"
        sql &= "          FROM NHANSU"
        sql &= "    End"
        sql &= "  ELSE"
        sql &= "    BEGIN"
        sql &= "      SELECT @sql = N'SELECT p.ID, p.Ten,IDDepatment, ' + "
        sql &= "          LEFT(@sql, LEN(@sql)-1) + '"
        sql &= "              FROM "
        sql &= "                 (SELECT tb2.* FROM ("
        sql &= "                 select"
        sql &= "                     tb.IDNhanVien,tb.IDQuyTrinh,"
        sql &= "                     max(tb.NgayThi) as Ngay"
        sql &= "                 from"
        sql &= "                     tblDiemQuyTrinh tb"
        sql &= "                 group by IDNhanVien,IDQuyTrinh) tmpTb  "
        sql &= "                 INNER JOIN tblDiemQuyTrinh tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDQuyTrinh=tb2.IDQuyTrinh AND tmpTb.Ngay=tb2.NgayThi) AS d"
        sql &= "              INNER JOIN NHANSU AS p"
        sql &= "              ON p.ID = d.IDNhanVien"
        If Not cbPhong.EditValue Is Nothing Then
            sql &= "           AND p.IDDepatment = " & cbPhong.EditValue
        End If


        sql &= "   '+'  GROUP BY p.ID, p.Ten,p.IDDepatment"
        sql &= "            ORDER BY p.ID;';"
        sql &= "    EXEC sp_executeSQL @sql;"
        sql &= " End"

        sql &= " DROP table ##tbNLDanhSach"


        sql &= " SELECT NLDanhSach.ID,NLTen.Ten FROM NLDanhSach LEFT JOIN NLTen ON NLDanhSach.IDTen=NLTen.ID WHERE NLDanhSach.Loai=0 ORDER BY Ten"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCT.BeginUpdate()
            gdvCT.Columns.Clear()
            gdv.DataSource = ds.Tables(0)
            gdvCT.ColumnPanelRowHeight = 60
            gdvCT.Columns("ID").Visible = False
            gdvCT.Columns("IDDepatment").OptionsColumn.ReadOnly = True
            gdvCT.Columns("IDDepatment").GroupIndex = 0
            gdvCT.Columns("IDDepatment").ColumnEdit = rcbPhongBan
            gdvCT.Columns("IDDepatment").Caption = "Phòng"
            gdvCT.Columns("Ten").OptionsColumn.ReadOnly = True
            gdvCT.Columns("Ten").VisibleIndex = 1
            gdvCT.Columns("Ten").Width = 250
            gdvCT.Columns("Ten").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            gdvCT.Columns("Ten").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("Ten").Caption = "Nhân viên"


            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                For j As Integer = 3 To gdvCT.Columns.Count - 1
                    If gdvCT.Columns(j).FieldName = ds.Tables(1).Rows(i)("ID") Then
                        gdvCT.Columns(j).Caption = ds.Tables(1).Rows(i)("Ten").ToString
                        gdvCT.Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
                        gdvCT.Columns(j).SummaryItem.DisplayFormat = "{0:N2}"
                        gdvCT.Columns(j).VisibleIndex = j + 3
                    End If
                Next
            Next
            gdvCT.EndUpdate()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub


End Class
