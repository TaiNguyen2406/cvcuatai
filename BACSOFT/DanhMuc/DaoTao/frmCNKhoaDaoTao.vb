Imports BACSOFT.Db.SqlHelper

Public Class frmCNKhoaDaoTao

    Private Sub frmCNKhoaDaoTao_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.Tag = "CNLichDaoTao" Then
            fCNLichDaoTao.loadDSKhoaDT()
        End If
    End Sub

    Private Sub frmCNNhomMH_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDS()
    End Sub

    Public Sub loadDS()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,TuNgay,DenNgay FROM tblKhoaDaoTao ORDER BY Ten")
        If Not dt Is Nothing Then
            gdvKhoaDT.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvNhomMHCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvKhoaDTCT.CellValueChanged
        If e.Column.FieldName = "ID" Then Exit Sub
        AddParameter("@Ten", gdvKhoaDTCT.GetFocusedRowCellValue("Ten"))
        AddParameter("@TuNgay", gdvKhoaDTCT.GetFocusedRowCellValue("TuNgay"))
        AddParameter("@DenNgay", gdvKhoaDTCT.GetFocusedRowCellValue("DenNgay"))
        If Not IsDBNull(gdvKhoaDTCT.GetFocusedRowCellValue("ID")) Or gdvKhoaDTCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvKhoaDTCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblKhoaDaoTao", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else
                ShowAlert("Đã cập nhật khoá đào tạo !")
            End If
        Else
            objID = doInsert("tblKhoaDaoTao")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvKhoaDTCT.DeleteSelectedRows()
                loadDS()
            Else
                ShowAlert("Đã thêm khoá đào tạo !")
                gdvKhoaDTCT.SetFocusedRowCellValue("ID", objID)
            End If
        End If
    End Sub


    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvKhoaDTCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa nhóm môn học: " & gdvKhoaDTCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvKhoaDTCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvKhoaDTCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub
End Class