Imports BACSOFT.Db.SqlHelper

Public Class frmThongTinDuyetLuong

    Private Sub frmThongTinDuyetLuong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblDuyetLuong ORDER BY ID DESC")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        Try
            AddParameter("@DSNhomMua", gdvCT.GetFocusedRowCellValue("DSNhomMua"))
            AddParameter("@HSKyNang", gdvCT.GetFocusedRowCellValue("HSKyNang"))

            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblDuyetLuong", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ShowAlert("Đã cập nhật")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class