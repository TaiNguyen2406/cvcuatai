Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.TAI

Public Class frmHinhThucTT2

    Private Sub frmHinhThucTT2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        barChkHuy.Checked = False
        loadData()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gv.OptionsBehavior.ReadOnly = True
        End If

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
        '    gv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False
        '    gv.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None

        'End If

    End Sub

    Private Sub loadData()
        riLueNhom.DataSource = tableNhomHinhThucTT()
        loadGV()
    End Sub

    Private Sub loadGV()
        Dim query = "SELECT * FROM DM_HINH_THUC_TT "
        If barChkHuy.Checked = False Then
            query &= " where TrangThai = 1"
        End If
        query &= " ORDER BY Nhom asc, SoTT, GiaiThich asc "
        Dim dt = ExecuteSQLDataTable(query)
        If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
        gc.DataSource = dt
    End Sub

    Private Sub gv_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv.RowUpdated
        gv.CloseEditor()
        gv.UpdateCurrentRow()
        'If Not IsDBNull(gv.GetFocusedRowCellValue("ID")) Then
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        'Else
        '    If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        'End If

        AddParameter("@TraTruoc1", gv.GetFocusedRowCellValue("TraTruoc1"))
        AddParameter("@TraTruoc2", gv.GetFocusedRowCellValue("TraTruoc2"))
        AddParameter("@TraSau1", gv.GetFocusedRowCellValue("TraSau1"))
        AddParameter("@TraSau2", gv.GetFocusedRowCellValue("TraSau2"))
        AddParameter("@Nhom", gv.GetFocusedRowCellValue("Nhom"))
        AddParameter("@SoNgayHT", gv.GetFocusedRowCellValue("SoNgayHT"))
        AddParameter("@GiaiThich", gv.GetFocusedRowCellValue("GiaiThich"))
        AddParameter("@TrangThai", gv.GetFocusedRowCellValue("TrangThai"))
        AddParameter("@SoTT", gv.GetFocusedRowCellValue("SoTT"))

        Dim row As New Object
        If Not IsDBNull(gv.GetFocusedRowCellValue("ID")) Then

            AddParameterWhere("@ID", gv.GetFocusedRowCellValue("ID"))
            row = doUpdate("DM_HINH_THUC_TT", "ID=@ID")
            ShowAlert("Đã cập nhật hình thức thanh toán!")
            If row Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                row = gv.FocusedRowHandle
                loadGV()
                gv.FocusedRowHandle = row
            End If
        Else

            'row = doInsert("DM_HINH_THUC_TT")
            'If row Is Nothing Then
            '    ShowBaoLoi(LoiNgoaiLe)
            'Else
            '    loadGV()
            '    ShowAlert("Đã thêm hình thức thanh toán mới!")
            '    For i As Integer = 0 To gv.RowCount - 1
            '        If gv.GetRowCellValue(i, "ID") = row Then
            '            gv.FocusedRowHandle = i
            '            Exit Sub
            '        End If
            '    Next
            'End If
        End If

    End Sub

    Private Sub btXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoa.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If ShowCauHoi("Có muốn xóa không ?") Then
            AddParameterWhere("@ID", gv.GetFocusedRowCellValue("ID"))
            If doDelete("DM_HINH_THUC_TT", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gv.DeleteSelectedRows()
                loadGV()
                ShowAlert("Đã xóa dữ liệu!")
            End If
        End If
    End Sub

    Private Sub mnu_Xoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Xoa.ItemClick
        btXoa_ItemClick(Nothing, Nothing)
    End Sub

    Private Sub gv_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.Delete Then
            btXoa_ItemClick(Nothing, Nothing)
        End If
    End Sub
    Private Sub gv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gv.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gv.CalcHitInfo(gc.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right And gv.RowCount > 0 Then
            PopupMenu1.ShowPopup(gc.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub barChkHuy_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barChkHuy.CheckedChanged
        If barChkHuy.Checked = True Then
            barChkHuy.Glyph = My.Resources.Checked
        Else
            barChkHuy.Glyph = My.Resources.UnCheck
        End If
        loadGV()
    End Sub

    
    Private Sub btnTai_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTai.ItemClick
        loadGV()
    End Sub

    Private Sub btnThem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
        Dim f As frmCNHinhThucTT2 = New frmCNHinhThucTT2
        TrangThai.isAddNew = True
        f.ShowDialog()
        loadGV()
    End Sub

    Private Sub btnSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        Dim row = gv.FocusedRowHandle
        Dim f As frmCNHinhThucTT2 = New frmCNHinhThucTT2
        TrangThai.isUpdate = True
        f.idhinhthucTT = gv.GetFocusedRowCellValue("ID")
        f.ShowDialog()
        loadGV()
        gv.FocusedRowHandle = row
    End Sub

    Private Sub mnu_Them_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Them.ItemClick
        btnThem.PerformClick()
    End Sub
    Private Sub mnu_Sua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Sua.ItemClick
        btnSua.PerformClick()
    End Sub
End Class
