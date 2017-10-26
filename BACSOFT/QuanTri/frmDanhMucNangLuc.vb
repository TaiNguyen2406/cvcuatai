Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Imports DevExpress.Utils.HorzAlignment

Public Class frmDanhMucNangLuc
    Public _exit = False

    Private Sub frmDanhMucNangLuc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDSNangLuc()
        LoadDSKyNang()
    End Sub

    Public Sub loadDSNangLuc()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NLNHOM ORDER BY Ten "
        sql &= " SELECT ID,Ten FROM NLTen ORDER BY Ten "

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvNhomNL.DataSource = ds.Tables(0)
            gdvTenNL.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSKyNang()
        Dim sql As String = ""
        AddParameterWhere("@LoaiNL", LoaiNangLuc.KyNang)
        AddParameterWhere("@Loai", LoaiTuDien.NhomNoiDungThiCong)
        sql &= " SELECT NLDanhSach.*,NLTen.Ten AS TenKN,tblTuDien.NoiDung AS NhomKyNang FROM NLDanhSach "
        sql &= " INNER JOIN NLTen ON NLDanhSach.IDTen=NLTen.ID"
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=NLDanhSach.IDNhomKN AND tblTuDien.Loai=@Loai"
        sql &= " WHERE NLDanhSach.Loai=@LoaiNL "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvKyNang.DataSource = tb
            gdvKyNangCT.ColumnPanelRowHeight = 60
            gdvKyNangCT.Columns("IDNhomKN").Visible = False
            gdvKyNangCT.Columns("ID").Visible = False
            gdvKyNangCT.Columns("IDPhong").Visible = False
            gdvKyNangCT.Columns("IDNhom").Visible = False
            gdvKyNangCT.Columns("Loai").Visible = False
            gdvKyNangCT.Columns("IDTen").Visible = False
            gdvKyNangCT.Columns("TenKN").OptionsColumn.ReadOnly = True
            gdvKyNangCT.Columns("TenKN").VisibleIndex = 0
            gdvKyNangCT.Columns("TenKN").Width = 250
            gdvKyNangCT.Columns("TenKN").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            gdvKyNangCT.Columns("TenKN").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvKyNangCT.Columns("TenKN").Caption = "Tên kỹ năng"
            gdvKyNangCT.Columns("Diem").Caption = "Điểm"
            gdvKyNangCT.Columns("Diem").Width = 70
            gdvKyNangCT.Columns("Diem").VisibleIndex = 1
            gdvKyNangCT.Columns("Diem").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvKyNangCT.Columns("Mota").Caption = "Mô tả"
            gdvKyNangCT.Columns("Mota").Width = 250
            gdvKyNangCT.Columns("Mota").VisibleIndex = 2
            gdvKyNangCT.Columns("Mota").AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            gdvKyNangCT.Columns("Mota").AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            gdvKyNangCT.Columns("Mota").ColumnEdit = rMemoText

            gdvKyNangCT.Columns("Mota").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvKyNangCT.Columns("NhomKyNang").Caption = "Nhóm kỹ năng"
            gdvKyNangCT.Columns("NhomKyNang").Width = 250
            gdvKyNangCT.Columns("NhomKyNang").GroupIndex = 0


            AddParameterWhere("@Loai", LoaiTuDien.NoiDungCongViec)
            Dim tbCv As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT")
            If Not tbCv Is Nothing Then
                For i As Integer = 0 To tbCv.Rows.Count - 1
                    For j As Integer = 0 To gdvKyNangCT.Columns.Count - 1
                        If gdvKyNangCT.Columns(j).FieldName = "P" & tbCv.Rows(i)("ID") Then
                            gdvKyNangCT.Columns(j).Caption = tbCv.Rows(i)("Ten").ToString
                            gdvKyNangCT.Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                            gdvKyNangCT.Columns(j).VisibleIndex = j + 3
                        End If
                    Next
                Next
            Else
                ShowCanhBao(LoiNgoaiLe)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvChiTiet_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvNhomNLCT.RowUpdated, gdvTenNLCT.RowUpdated
        If _exit = True Then Exit Sub

        Dim _TenBang As String = ""
        Select Case CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).Name
            Case "gdvDVTCT"
                _TenBang = "TENDONVITINH"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
                AddParameter("@Ten_ENG", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten_ENG"))
            Case "gdvHangSXCT"
                _TenBang = "TENHANGSANXUAT"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
            Case "gdvNuocSXCT"
                _TenBang = "TENNUOC"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
            Case "gdvNhomNLCT"
                _TenBang = "NLNhom"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
            Case "gdvTenNLCT"
                _TenBang = "NLTen"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
        End Select


        If IsDBNull(CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")) Or CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID") Is Nothing Then
            objID = doInsert(_TenBang)
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteSelectedRows()
                'loadDS()
            Else
                _exit = True
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).SetFocusedRowCellValue("ID", objID)
                ShowAlert("Đã thêm !")
                _exit = False
            End If
        Else
            AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
            If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                'loadDS()
            Else
                ShowAlert("Đã cập nhật !")
            End If
        End If
    End Sub

    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNhomNLCT.KeyDown, gdvTenNLCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            'If ShowCauHoi("Xóa dòng được chọn ?") Then
            '    Dim _TenBang As String = ""
            '    Dim _TenCot As String = ""
            '    Select Case CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).Name
            '        Case "gdvDVTCT"
            '            _TenBang = "TENDONVITINH"
            '            _TenCot = "IDDonvitinh"
            '        Case "gdvHangSXCT"
            '            _TenBang = "TENHANGSANXUAT"
            '            _TenCot = "IDHangsanxuat"
            '        Case "gdvNuocSXCT"
            '            _TenBang = "TENNUOC"
            '            _TenCot = "IDTennuoc"
            '            'Case "gdvNhomNL"
            '            '    _TenBang = "NLNHOM"
            '            '    _TenCot = "IDTennhom"
            '            'Case "gdvTenNL"
            '            '    _TenBang = "TENVATTU"
            '            '    _TenCot = "IDTenvattu"
            '    End Select

            '    AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
            '    Dim tb As DataTable = ExecuteSQLDataTable("SELECT COUNT(ID) FROM VATTU WHERE " & _TenCot & "=@ID")
            '    If tb Is Nothing Then
            '        ShowBaoLoi(LoiNgoaiLe)
            '    Else
            '        If Convert.ToInt32(tb.Rows(0)(0)) > 0 Then
            '            If ShowCauHoi("Đang có vật tư sử dụng thông tin này bạn phải thay thế nó bằng một ID khác trước khi xoá, Thay thế ?") Then
            '                _exit = False
            '                Dim f As New frmDoiID
            '                f._oldID = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
            '                f._TenCot = _TenCot
            '                f.ShowDialog()
            '                If _exit = True Then Exit Sub
            '            Else
            '                Exit Sub
            '            End If
            '        End If
            '    End If

            '    AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
            '    If doDelete(_TenBang, "ID=@ID") Is Nothing Then
            '        ShowBaoLoi(LoiNgoaiLe)
            '    Else
            '        CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteSelectedRows()
            '        ShowAlert("Đã xoá!")
            '    End If
            'End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow = Not CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow
        End If
    End Sub

    Private Sub gdvChiTiet_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvNhomNLCT.DoubleClick, gdvTenNLCT.DoubleClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle < 0 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        End If

        CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")

    End Sub

    Private Sub gdvChiTiet_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gdvNhomNLCT.HiddenEditor, gdvTenNLCT.HiddenEditor
        CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsBehavior.Editable = False
    End Sub

    Private Sub lbTaiLaiNhomNL_Click(sender As System.Object, e As System.EventArgs) Handles lbTaiLaiNhomNL.Click, lbTaiLaiTenNL.Click
        loadDSNangLuc()
    End Sub

    Private Sub btCapNhatNhomKyNang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatNhomKyNang.ItemClick
        Dim f As New frmCNNhomKyNang
        f.ShowDialog()
    End Sub

    Private Sub btThemKyNang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThemKyNang.ItemClick, mThemKyNang.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNKyNang
        f.ShowDialog()
    End Sub

    Private Sub btSuaKyNang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaKyNang.ItemClick, mSuaKN.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvKyNangCT.FocusedRowHandle < 0 Then Exit Sub
        Dim index As Integer = gdvKyNangCT.FocusedRowHandle
        TrangThai.isUpdate = True
        Dim f As New frmCNKyNang
        objID = gdvKyNangCT.GetFocusedRowCellValue("ID")
        f.ShowDialog()
        gdvKyNangCT.FocusedRowHandle = index
    End Sub

    Private Sub btXoaKyNang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoaKyNang.ItemClick, mXoaKN.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvKyNangCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá nội dung đã chọn ?") Then
            AddParameterWhere("@ID", gdvKyNangCT.GetFocusedRowCellValue("ID"))
            If doDelete("NLDanhSach", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDSKyNang()
                ShowAlert("Đã xoá !")
            End If
        End If
    End Sub

    Private Sub gdvKyNangCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvKyNangCT.RowCellClick
        If gdvKyNangCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If e.Column.FieldName <> "IDNhomKN" And e.Column.FieldName <> "ID" And e.Column.FieldName <> "IDPhong" And e.Column.FieldName <> "IDNhom" And e.Column.FieldName <> "Loai" And e.Column.FieldName <> "TenKN" And e.Column.FieldName <> "Diem" And e.Column.FieldName <> "Mota" And e.Column.FieldName <> "NhomKyNang" Then
            Dim val As Integer = 1
            If gdvKyNangCT.GetRowCellValue(e.RowHandle, e.Column.FieldName) Then
                val = 0
            Else
                val = 1
            End If
       

            If ExecuteSQLNonQuery("UPDATE NLDanhSach SET " & e.Column.FieldName & "=" & val & " WHERE ID=" & gdvKyNangCT.GetRowCellValue(e.RowHandle, "ID")) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvKyNangCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, val)
                gdvKyNangCT.CloseEditor()
                gdvKyNangCT.UpdateCurrentRow()
            End If
        End If
    End Sub
End Class
