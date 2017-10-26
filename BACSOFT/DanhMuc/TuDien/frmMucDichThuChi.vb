Imports BACSOFT.Db.SqlHelper

Public Class frmMucDichThuChi

    Private Sub frmMucDichThuChi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ID AS ID2,Ten,ChiPhiMatDi,( CASE LEFT(ID,1) WHEN 1 THEN N'Mục đích thu' ELSE N'Mục đích chi' END)Loai FROM MUCDICHTHUCHI ORDER BY ID")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("ChiPhiMatDi", False)
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xóa mục đích thu chi ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID2"))
                If Not doDelete("MUCDICHTHUCHI", "Id=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        ElseIf e.KeyCode = Keys.F5 Then
            LoadDS()
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or IsDBNull(gdvCT.GetFocusedRowCellValue("Ten")) Then Exit Sub
        Try
            AddParameter("@id", gdvCT.GetFocusedRowCellValue("ID"))
            AddParameter("@Ten", gdvCT.GetFocusedRowCellValue("Ten"))
            AddParameter("@ChiPhiMatDi", gdvCT.GetFocusedRowCellValue("ChiPhiMatDi"))

            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID2")) Then
                If doInsert("MUCDICHTHUCHI") is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                gdvCT.SetFocusedRowCellValue("ID2", gdvCT.GetFocusedRowCellValue("ID"))
            Else
                AddParameterWhere("@ID2", gdvCT.GetFocusedRowCellValue("ID2"))
                If doUpdate("MUCDICHTHUCHI", "Id=@ID2") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If


        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "ChiPhiMatDi" Then

            gdvCT.SetFocusedRowCellValue("ChiPhiMatDi", Not e.CellValue)

        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "ID" Then
            If e.Value.ToString.Substring(0, 1) = 1 Then
                gdvCT.SetRowCellValue(e.RowHandle, "Loai", "Mục đích thu")
            Else
                gdvCT.SetRowCellValue(e.RowHandle, "Loai", "Mục đích chi")
            End If

        End If
    End Sub
End Class
