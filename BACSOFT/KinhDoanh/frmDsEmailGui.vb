Imports BACSOFT.Db.SqlHelper
Public Class frmDsEmailGui



    Private Sub frmDsEmailGui_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        gdvData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        LoadDuLieu()
    End Sub

    Private Sub btnDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDong.ItemClick
        Me.Close()
    End Sub

    Private Sub LoadDuLieu()
        Dim sql As String = "SELECT Id,Ten,Email,MatKhau FROM DM_EMAIL ORDER BY Ten"
        gdv.DataSource = ExecuteSQLDataTable(sql)
    End Sub
    Private Sub btnThemMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemMoi.ItemClick
        gdvData.AddNewRow()
    End Sub

    Private Sub gdvData_CustomRowCellEditForEditing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles gdvData.CustomRowCellEditForEditing
        If e.Column.FieldName = "MatKhau" Then
            e.RepositoryItem = txtMatKhau2
        End If
    End Sub

    Private Sub btnCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhat.ItemClick
        Try
            For i As Integer = 0 To gdvData.RowCount - 1
                If gdvData.GetRowCellValue(i, "Email").ToString.Trim = "" Or gdvData.GetRowCellValue(i, "MatKhau").ToString.Trim = "" Then Continue For
                AddParameter("@Ten", gdvData.GetRowCellValue(i, "Ten"))
                AddParameter("@Email", gdvData.GetRowCellValue(i, "Email"))
                AddParameter("@MatKhau", gdvData.GetRowCellValue(i, "MatKhau"))
                If gdvData.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    If doInsert("DM_EMAIL") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else
                    AddParameterWhere("@dk_Id", gdvData.GetRowCellValue(i, "Id"))
                    If doUpdate("DM_EMAIL", "Id = @dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next
            ShowThongBao("Cập nhật danh sách gửi email thành công!")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gdvData.FocusedRowHandle < 0 Then
            ShowBaoLoi("Chưa chọn dòng cần xóa !")
            Exit Sub
        End If
        If gdvData.GetFocusedRowCellValue("Id") Is DBNull.Value Then
            gdvData.DeleteRow(gdvData.FocusedRowHandle)
        Else
            If Not ShowCauHoi("Bạn có chắc muốn xóa địa chỉ email đã chọn không?") Then Exit Sub
            AddParameterWhere("@dk_Id", gdvData.GetFocusedRowCellValue("Id"))
            If doDelete("DM_EMAIL", "Id = @dk_Id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvData.DeleteRow(gdvData.FocusedRowHandle)
            End If
        End If

    End Sub

End Class