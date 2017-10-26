Imports BACSOFT.Db.SqlHelper

Public Class frmCNHeSoThuong

    Private Sub frmCNHeSoThuong_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
    End Sub

    Private Sub frmCNHeSoThuong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Nam As ID,* FROM tblHeSoThuongNam ORDER BY Nam")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng được chọn ?") Then
                AddParameterWhere("@Nam", gdvCT.GetFocusedRowCellValue("Nam"))
                If Not doDelete("tblHeSoThuongNam", "Nam=@Nam") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        Try
            AddParameter("@HSThuongDiemKD", gdvCT.GetFocusedRowCellValue("HSThuongDiemKD"))
            AddParameter("@QuyThuongDiem", gdvCT.GetFocusedRowCellValue("QuyThuongDiem"))
            AddParameter("@Nam", gdvCT.GetFocusedRowCellValue("Nam"))
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then

                If doInsert("tblHeSoThuongNam") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                gdvCT.SetFocusedRowCellValue("ID", gdvCT.GetFocusedRowCellValue("Nam"))
            Else
                AddParameterWhere("@Id", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("tblHeSoThuongNam", "Nam=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

End Class