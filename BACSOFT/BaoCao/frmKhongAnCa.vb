Imports BACSOFT.Db.SqlHelper

Public Class frmKhongAnCa
    Private _propertyName As Rectangle
    Private LuongThangKT As String = ""
    Private _exit As Boolean = False

    Private Sub frmAnCa_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbThang.EditValue = Now.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadcbPhong()
    End Sub

    Private Sub LoadcbPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten from DEPATMENT ")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDuLieu()
        'sql &= " SELECT * FROM ##tb"
        AddParameterWhere("@NgayBatDau", New DateTime(tbNam.EditValue, cbThang.EditValue, 1))
        AddParameterWhere("@NgayKetThuc", New DateTime(tbNam.EditValue, cbThang.EditValue, DateTime.DaysInMonth(tbNam.EditValue, cbThang.EditValue)))
        Dim tb As DataTable = ExecutePrcDataTable("prc_AnCa")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            gdvCT.BeginUpdate()
            ' gdvCT.Columns("Modify").Visible = False
            gdvCT.Columns("IDNV").Visible = False
            gdvCT.Columns("Ten").Caption = "Nhân viên"
            gdvCT.Columns("Ten").Width = 150
            gdvCT.Columns("Ten").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("Ten").OptionsColumn.ReadOnly = True
            'gdvCT.Columns("Ten").SortOrder =
            gdvCT.Columns("IDPhong").Caption = "Phòng"
            gdvCT.Columns("IDPhong").ColumnEdit = rcbPhong
            gdvCT.Columns("IDPhong").GroupIndex = 0
            gdvCT.Columns("IDPhong").SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
            For i As Integer = 0 To gdvCT.Columns.Count - 1
                If gdvCT.Columns(i).FieldName <> "Ten" And gdvCT.Columns(i).FieldName <> "IDPhong" And gdvCT.Columns(i).Visible = True Then
                    gdvCT.Columns(i).Width = 40
                    gdvCT.Columns(i).Tag = New DateTime(tbNam.EditValue, cbThang.EditValue, gdvCT.Columns(i).FieldName)
                End If

            Next
            gdvCT.EndUpdate()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub


    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDuLieu()
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        Dim sql As String = ""
        Try
            gdvCT.BeginUpdate()

            For i As Integer = 0 To gdvCT.RowCount - 1
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    sql = ""
                    If gdvCT.Columns(j).FieldName = "Ten" Or gdvCT.Columns(j).FieldName = "IDNV" Or gdvCT.Columns(j).FieldName = "IDPhong" Or gdvCT.Columns(j).Visible = False Then Continue For
                    AddParameter("@KAnCa", gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName))
                    AddParameterWhere("@Ngay", gdvCT.Columns(j).Tag)
                    AddParameterWhere("@IDNV", gdvCT.GetRowCellValue(i, "IDNV"))
                    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Then
                        AddParameterWhere("@QuyenXem", 1)
                    Else
                        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                            AddParameterWhere("@QuyenXem", 2)
                        Else
                            AddParameterWhere("@QuyenXem", 0)
                        End If
                    End If
                    sql &= " IF exists(SELECT ID FROM ANCA WHERE Ngay=@Ngay AND IDNhanVien=@IDNV)"
                    sql &= "    BEGIN"
                    sql &= "        UPDATE ANCA SET KhongAnCa=@KAnCa WHERE ID= (SELECT ID FROM ANCA WHERE Ngay=@Ngay AND IDNhanVien=@IDNV)"
                    sql &= "    End"
                    sql &= " Else"
                    sql &= "    BEGIN"
                    sql &= "        INSERT INTO ANCA(IDNhanVien,Ngay,KhongAnCa)"
                    sql &= "        VALUES (@IDNV,@Ngay,@KAnCa)"
                    sql &= "    End"
                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                Next
            Next
            ShowAlert("ĐÃ thực hiện !")
        Catch ex As Exception

        Finally
            gdvCT.EndUpdate()
        End Try
    End Sub


End Class