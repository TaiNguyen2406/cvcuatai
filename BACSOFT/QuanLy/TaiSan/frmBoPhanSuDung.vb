Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Imports DevExpress.Utils.HorzAlignment

Public Class frmBoPhanSuDung

    Private Sub frmPhongBan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDS()
    End Sub

    'Private Sub BarManager1_HighlightedLinkChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.HighlightedLinkChangedEventArgs) Handles BarManager1.HighlightedLinkChanged
    '    If Not e.Link Is Nothing AndAlso TypeOf e.Link.Item Is BarButtonItem Then
    '        Cursor = Cursors.Hand
    '    Else
    '        Cursor = Cursors.Default
    '    End If
    'End Sub

    Public Sub loadDS()

        Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM TAISAN_BOPHAN ORDER BY Id")
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXoa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoa.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If ShowCauHoi("Xóa bộ phận " & gdvChiTiet.GetFocusedRowCellValue("TenBoPhan") & " ?") Then
            AddParameterWhere("@Id", gdvChiTiet.GetFocusedRowCellValue("Id"))
            If doDelete("TAISAN_BOPHAN", "Id=@Id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvChiTiet.DeleteSelectedRows()
                ShowAlert("Đã xoá!")
            End If
        End If
    End Sub

    Private Sub gdvChiTiet_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvChiTiet.RowUpdated
        AddParameter("@TenBoPhan", gdvChiTiet.GetFocusedRowCellValue("TenBoPhan"))
        If Not IsDBNull(gdvChiTiet.GetFocusedRowCellValue("Id")) Then
            AddParameterWhere("@Id", gdvChiTiet.GetFocusedRowCellValue("Id"))
            If doUpdate("TAISAN_BOPHAN", "ID=@ID2") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()

            End If
        Else
            If doInsert("TAISAN_BOPHAN") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvChiTiet.DeleteSelectedRows()
                loadDS()
            Else
                ShowAlert("Đã thêm bộ phận !")
                gdvChiTiet.SetFocusedRowCellValue("Id", gdvChiTiet.GetFocusedRowCellValue("Id"))
            End If
        End If
    End Sub

    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvChiTiet.KeyDown
        If e.KeyCode = Keys.Delete Then
            mXoa.PerformClick()
        End If
    End Sub

    Private Sub gdvChiTiet_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvChiTiet.DoubleClick
        '   If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvChiTiet.FocusedRowHandle < 0 Then
            '   If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        End If

        gdvChiTiet.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")

    End Sub

    Private Sub gdvChiTiet_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gdvChiTiet.HiddenEditor
        gdvChiTiet.OptionsBehavior.Editable = False
    End Sub
End Class
