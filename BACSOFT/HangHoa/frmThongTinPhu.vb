Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Imports DevExpress.Utils.HorzAlignment

Public Class frmThongTinPhu
    Public _exit = False

    Private Sub frmThongTinPhu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDS()
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gdvDVTCT.OptionsBehavior.Editable = True
            gdvNuocSXCT.OptionsBehavior.Editable = True
            gdvHangSXCT.OptionsBehavior.Editable = True
            gdvNhomVTCT.OptionsBehavior.Editable = True
            gdvTenVTCT.OptionsBehavior.Editable = True
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
                gdvDVTCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
                gdvNuocSXCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
                gdvHangSXCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
                gdvNhomVTCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
                gdvTenVTCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
            End If
        End If
    End Sub

    Public Sub loadDS()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten, Ten_ENG FROM TENDONVITINH ORDER BY Ten "
        sql &= " SELECT ID,Ten, MaNuoc FROM TENNUOC ORDER BY Ten "
        sql &= " SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten "
        sql &= " SELECT ID,Ten,Ten_ENG FROM TENNHOM ORDER BY Ten "
        sql &= " SELECT ID,Ten,Ten_ENG FROM TENVATTU ORDER BY Ten "

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvDVT.DataSource = ds.Tables(0)
            gdvNuocSX.DataSource = ds.Tables(1)
            gdvHangSX.DataSource = ds.Tables(2)
            gdvNhomVT.DataSource = ds.Tables(3)
            gdvTenVT.DataSource = ds.Tables(4)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvChiTiet_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvDVTCT.RowUpdated, gdvHangSXCT.RowUpdated, gdvNuocSXCT.RowUpdated, gdvNhomVTCT.RowUpdated, gdvTenVTCT.RowUpdated
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
                AddParameter("@MaNuoc", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("MaNuoc"))
            Case "gdvNhomVTCT"
                _TenBang = "TENNHOM"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
                AddParameter("@Ten_ENG", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten_ENG"))
            Case "gdvTenVTCT"
                _TenBang = "TENVATTU"
                AddParameter("@Ten", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten"))
                AddParameter("@Ten_ENG", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("Ten_ENG"))
        End Select


        If IsDBNull(CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")) Or CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID") Is Nothing Then
            objID = doInsert(_TenBang)
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                'CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteSelectedRows()
                'loadDS()
            Else
                _exit = True
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).SetFocusedRowCellValue("ID", objID)
                _exit = False
            End If

        Else

            AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
            If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                'loadDS()
            End If
            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = deskTop.mThongSo.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThongSo).LoadDSTuDien()
                End If
            Next
        End If
    End Sub

    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvDVTCT.KeyDown, gdvHangSXCT.KeyDown, gdvNuocSXCT.KeyDown, gdvNhomVTCT.KeyDown, gdvTenVTCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xóa dòng được chọn ?") Then
                Dim _TenBang As String = ""
                Dim _TenCot As String = ""
                Select Case CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).Name
                    Case "gdvDVTCT"
                        _TenBang = "TENDONVITINH"
                        _TenCot = "IDDonvitinh"
                    Case "gdvHangSXCT"
                        _TenBang = "TENHANGSANXUAT"
                        _TenCot = "IDHangsanxuat"
                    Case "gdvNuocSXCT"
                        _TenBang = "TENNUOC"
                        _TenCot = "IDTennuoc"
                    Case "gdvNhomVTCT"
                        _TenBang = "TENNHOM"
                        _TenCot = "IDTennhom"
                    Case "gdvTenVTCT"
                        _TenBang = "TENVATTU"
                        _TenCot = "IDTenvattu"
                End Select

                AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT COUNT(ID) FROM VATTU WHERE " & _TenCot & "=@ID")
                If tb Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    If Convert.ToInt32(tb.Rows(0)(0)) > 0 Then
                        If ShowCauHoi("Đang có mặt hàng sử dụng thông tin này bạn phải thay thế nó bằng một ID khác trước khi xoá, Thay thế ?") Then
                            _exit = False
                            Dim f As New frmDoiID
                            f._oldID = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
                            f._TenCot = _TenCot
                            f.ShowDialog()
                            If _exit = True Then Exit Sub
                        Else
                            Exit Sub
                        End If
                    End If
                End If

                AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
                If doDelete(_TenBang, "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
            CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow = Not CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow
        End If
    End Sub

    'Private Sub gdvChiTiet_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvDVTCT.DoubleClick, gdvHangSXCT.DoubleClick, gdvNuocSXCT.DoubleClick, gdvNhomVTCT.DoubleClick, gdvTenVTCT.DoubleClick
    '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
    '    If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle < 0 Then
    '        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
    '    End If

    '    CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsBehavior.Editable = True
    '    SendKeys.Send("{Enter}")

    'End Sub

    'Private Sub gdvChiTiet_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gdvDVTCT.HiddenEditor, gdvHangSXCT.HiddenEditor, gdvNuocSXCT.HiddenEditor, gdvNhomVTCT.HiddenEditor, gdvTenVTCT.HiddenEditor
    '    CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsBehavior.Editable = False
    'End Sub

End Class
