Imports BACSOFT.Db.SqlHelper

Public Class frmTHDiemQuyTrinhCu

    Private Sub frmQuyTrinh_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbDiem.EditValue = 0
        loadCb()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gdvCT.OptionsBehavior.ReadOnly = True
        End If
    End Sub

    Public Sub loadCb()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM DEPATMENT "
        sql &= " SELECT ID,Ten FROM NLNhom ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM NLTen ORDER BY Ten"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbPhong.DataSource = ds.Tables(0)
            rcbNhom.DataSource = ds.Tables(1)
            rcbTen.DataSource = ds.Tables(2)
        End If
    End Sub


    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        Dim strNV As String = ""
        Dim sqlStr As String = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        If Not cbPhong.EditValue Is Nothing Then
            sqlStr &= " AND IDDepatment = " & cbPhong.EditValue
        End If
        sqlStr &= " ORDER BY ID"

        Dim tbNV As DataTable = ExecuteSQLDataTable(sqlStr)
        If Not tbNV Is Nothing Then
            For i As Integer = 0 To tbNV.Rows.Count - 1
                strNV &= ",ISNULL(C" & tbNV.Rows(i)("ID").ToString & ",0)AS C" & tbNV.Rows(i)("ID").ToString
            Next
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

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

        sql &= " SELECT (0) AS STT, NLNhom.Ten AS NhomNL,NLTen.Ten AS TenNL,NLDanhSach.ID AS IDNangLuc,NLDanhSach.Diem,NLDanhSach.MoTa, NLNhanVienE.ID " & strNV & " FROM NLDanhSach"
        sql &= " LEFT JOIN NLNhanVienE ON NLNhanVienE.IDNangLuc=NLDanhSach.ID"
        sql &= " INNER JOIN NLTen ON NLDanhSach.IDTen=NLTen.ID"
        sql &= " INNER JOIN NLNhom ON NLNhom.ID=NLDanhSach.IDNhom"
        sql &= " WHERE 1=1 "
        If Not cbPhong.EditValue Is Nothing Then
            sql &= " AND NLDanhSach.ID IN (SELECT DISTINCT ID FROM ##tbNLDanhSach WHERE IDPhong = 'P" & cbPhong.EditValue & "')"
        End If
        If Not cbNhom.EditValue Is Nothing Then
            sql &= " AND NLDanhSach.IDNhom=" & cbNhom.EditValue
        End If
        If Not cbTen.EditValue Is Nothing Then
            sql &= " AND NLDanhSach.IDTen=" & cbTen.EditValue
        End If
        If tbDiem.EditValue <> 0 Then
            sql &= " AND NLDanhSach.Diem=" & tbDiem.EditValue
        End If
        sql &= " ORDER BY NhomNL,TenNL"
        sql &= " DROP table ##tbNLDanhSach"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next
            gdvCT.BeginUpdate()
            gdvCT.Columns.Clear()
            gdv.DataSource = tb
            gdvCT.ColumnPanelRowHeight = 60
            gdvCT.Columns("ID").Visible = False
            gdvCT.Columns("IDNangLuc").Visible = False
            gdvCT.Columns("STT").OptionsColumn.ReadOnly = True
            gdvCT.Columns("STT").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gdvCT.Columns("STT").VisibleIndex = 0
            gdvCT.Columns("STT").Width = 40
            gdvCT.Columns("STT").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("STT").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NhomNL").VisibleIndex = 1
            gdvCT.Columns("NhomNL").Width = 100
            gdvCT.Columns("NhomNL").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("NhomNL").Caption = "Nhóm"
            gdvCT.Columns("NhomNL").OptionsColumn.ReadOnly = True
            gdvCT.Columns("TenNL").OptionsColumn.ReadOnly = True
            gdvCT.Columns("TenNL").VisibleIndex = 2
            gdvCT.Columns("TenNL").Width = 250
            gdvCT.Columns("TenNL").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            gdvCT.Columns("TenNL").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("TenNL").Caption = "Tên quy trình"
            gdvCT.Columns("Diem").Caption = "Điểm"
            gdvCT.Columns("Diem").OptionsColumn.ReadOnly = True
            gdvCT.Columns("Diem").Width = 50
            gdvCT.Columns("Diem").VisibleIndex = 3
            gdvCT.Columns("Diem").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("MoTa").Caption = "Mô tả"
            gdvCT.Columns("MoTa").OptionsColumn.ReadOnly = True
            gdvCT.Columns("MoTa").Width = 250
            gdvCT.Columns("MoTa").VisibleIndex = 4
            gdvCT.Columns("MoTa").AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            gdvCT.Columns("MoTa").AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            gdvCT.Columns("MoTa").ColumnEdit = rMemoText

            If Not tbNV Is Nothing Then
                For i As Integer = 0 To tbNV.Rows.Count - 1
                    For j As Integer = 0 To gdvCT.Columns.Count - 1
                        If gdvCT.Columns(j).FieldName = "C" & tbNV.Rows(i)("ID") Then
                            gdvCT.Columns(j).Caption = tbNV.Rows(i)("Ten").ToString
                            gdvCT.Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                            gdvCT.Columns(j).VisibleIndex = j + 5
                        End If
                    Next
                Next
            Else
                ShowCanhBao(LoiNgoaiLe)
            End If
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

    Private Sub rcbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhom.ButtonClick
        If e.Button.Index = 1 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbTen_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTen.ButtonClick
        If e.Button.Index = 1 Then
            cbTen.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "ID" Then Exit Sub

        AddParameter("@" & e.Column.FieldName, e.Value)
        If IsDBNull(gdvCT.GetRowCellValue(e.RowHandle, "ID")) Then
            AddParameter("@IDNangLuc", gdvCT.GetRowCellValue(e.RowHandle, "IDNangLuc"))
            Dim obj As Object = doInsert("NLNhanVienE")
            If obj Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvCT.SetRowCellValue(e.RowHandle, "ID", obj)
                ShowAlert("Đã cập nhật")
            End If
        Else
            AddParameterWhere("@ID", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
            If doUpdate("NLNhanVienE", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật")
            End If
        End If


    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub
End Class
