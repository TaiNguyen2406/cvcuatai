Imports BACSOFT.Db.SqlHelper

Public Class frmTGGiaoHang


    Private Sub frmTGGiaoHang_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDS()
    End Sub

    Private Sub LoadDS()
        AddParameter("@Loai", LoaiTuDien.TGCungUng)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa thời gian giao hàng được chọn ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Else
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        End If

        Try
            AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
            AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
            AddParameter("@Loai", LoaiTuDien.TGCungUng)
            If gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                Dim objID As Object = doInsert("tblTuDien")
                If objID Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                gdvCT.SetFocusedRowCellValue("ID", objID)
                ShowAlert("Đã thêm thời gian giao hàng !")
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ShowAlert("Đã sửa thông tin thời gian giao hàng !")
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class
