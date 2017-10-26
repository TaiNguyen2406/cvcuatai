Imports BACSOFT.Db.SqlHelper

Public Class frmCNNhomKyNang

    Private Sub frmCNNhomKyNang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDS()
    End Sub

    Public Sub loadDS()
        AddParameterWhere("@Loai", LoaiTuDien.NhomNoiDungThiCong)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung,Ma,MoTa FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "ID" Then Exit Sub
        AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@Loai", LoaiTuDien.NhomNoiDungThiCong)
        AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
        AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else
                ShowAlert("Đã cập nhật nhóm kỹ năng !")
            End If
        Else
            objID = doInsert("tblTuDien")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDS()
            Else
                ShowAlert("Đã thêm nhóm kỹ năng !")
                gdvCT.SetFocusedRowCellValue("ID", objID)
            End If
        End If
    End Sub


    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa: " & gdvCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
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

    Private Sub frmNhomCongViec_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'fCongViec.loadDSNhomCongViec()
        If deskTop.tabMain.SelectedTabPage.Controls(0).Tag = deskTop.mTongHopDiemKyNang.Name Then
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmTongHopDiemKyNang).LoadDSNhomKN()
        End If
    End Sub
End Class