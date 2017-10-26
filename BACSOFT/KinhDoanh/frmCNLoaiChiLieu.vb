Imports BACSOFT.Db.SqlHelper

Public Class frmCNLoaiChiTieu

    Private Sub frmCNLoaiChiTieu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDS()
    End Sub

    Public Sub loadDS()
        AddParameterWhere("@Loai", LoaiTuDien.LoaiChiTieu)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY NoiDung")
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "ID" Then Exit Sub
        AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@Loai", LoaiTuDien.LoaiChiTieu)
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else
                ShowAlert("Đã cập nhật loại chỉ tiêu !")
            End If
        Else
            objID = doInsert("tblTuDien")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDS()
            Else
                ShowAlert("Đã thêm chỉ tiêu !")
                gdvCT.SetFocusedRowCellValue("ID", objID)
            End If
        End If
    End Sub


    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa chỉ tiêu: " & gdvCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub
End Class