Imports BACSOFT.Db.SqlHelper

Public Class frmCNNhomDaoTao

    Private Sub frmCNNhomMH_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.Tag = "CNMonHoc" Then
            fCNMonHoc.loadDSTuDien()
        End If
    End Sub

    Private Sub frmCNNhomMH_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDS()
    End Sub

    Public Sub loadDS()
        AddParameterWhere("@Loai", LoaiTuDien.NhomMonHoc)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID as ID2,ID,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY NoiDung")
        If Not dt Is Nothing Then
            gdvNhomMH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSMonHoc()
        AddParameterWhere("@IDNhom", gdvNhomMHCT.GetFocusedRowCellValue("ID"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblHTNhomMH WHERE IDNhomMH=@IDNhom ORDER BY TenMH")
        If Not tb Is Nothing Then

        End If
    End Sub


    Private Sub gdvNhomMHCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvNhomMHCT.CellValueChanged
        If e.Column.FieldName = "ID" Then Exit Sub
        AddParameter("@NoiDung", gdvNhomMHCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@Loai", LoaiTuDien.NhomMonHoc)
        If Not IsDBNull(gdvNhomMHCT.GetFocusedRowCellValue("ID")) Or gdvNhomMHCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvNhomMHCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else
                ShowAlert("Đã cập nhật nhóm môn học !")
            End If
        Else
            objID = doInsert("tblTuDien")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvNhomMHCT.DeleteSelectedRows()
                loadDS()
            Else
                ShowAlert("Đã thêm nhóm môn học !")
                gdvNhomMHCT.SetFocusedRowCellValue("ID", objID)
            End If
        End If
    End Sub


    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNhomMHCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa nhóm môn học: " & gdvNhomMHCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvNhomMHCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvNhomMHCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub
End Class