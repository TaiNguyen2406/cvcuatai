Imports BACSOFT.Db.SqlHelper

Public Class frmQuyDoi

    Private Sub frmQuyDoi2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ThangNam AS ID,*,Convert(bit,0)Modify FROM tblQuyDoi ORDER BY Convert(datetime,'01/'+ThangNam,103)")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        Try
            BeginTransaction()
            For i As Integer = 0 To gdvCT.RowCount - 2
                If gdvCT.GetRowCellValue(i, "Modify") Then
                    AddParameter("@TyLeQuyDoiDiem", gdvCT.GetRowCellValue(i, "TyLeQuyDoiDiem"))
                    AddParameter("@TyLeChuyenMa", gdvCT.GetRowCellValue(i, "TyLeChuyenMa"))
                    AddParameter("@HeSoLoiNhuan", gdvCT.GetRowCellValue(i, "HeSoLoiNhuan"))
                    AddParameter("@HSThuCK", gdvCT.GetRowCellValue(i, "HSThuCK"))
                    AddParameter("@HSLNThuongMaiToMH", gdvCT.GetRowCellValue(i, "HSLNThuongMaiToMH"))
                    AddParameter("@HSLNCongTrinhToMH", gdvCT.GetRowCellValue(i, "HSLNCongTrinhToMH"))
                    AddParameter("@ThangNam", gdvCT.GetRowCellValue(i, "ThangNam"))
                    If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Or gdvCT.GetRowCellValue(i, "ID") Is Nothing Then
                        If doInsert("tblQuyDoi") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        gdvCT.SetRowCellValue(i, "ID", gdvCT.GetRowCellValue(i, "ThangNam"))
                    Else
                        AddParameterWhere("@ID", gdvCT.GetRowCellValue(i, "ID"))
                        If doUpdate("tblQuyDoi", "ThangNam=@ThangNam") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                    gdvCT.SetRowCellValue(i, "Modify", False)
                End If

            Next
            ComitTransaction()
            ShowAlert("Đã lưu !")
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(LoiNgoaiLe)
        End Try


    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If Not e.Column.FieldName = "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub
End Class