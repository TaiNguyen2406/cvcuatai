Imports BACSOFT.Db.SqlHelper

Public Class frmCongViec
    Public _exit As Boolean = False

    Private Sub frmNhomVanDe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDSNhomCongViec()
        loadDS()
    End Sub

    Public Sub loadDS()
        Dim sql As String = ""
        If Not cbNhomCongViec.EditValue Is Nothing Then
            AddParameterWhere("@IDCha", cbNhomCongViec.EditValue)
            sql = "SELECT ID,NoiDung,Ma,MoTa,Diem,ISNULL(HeSo1,0)HeSo1 FROM tblTuDien WHERE Loai=@Loai AND IDP=@IDCha ORDER BY Ma"
        Else
            sql = "SELECT ID,NoiDung,Ma,MoTa,Diem,ISNULL(HeSo1,0)HeSo1 FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma"
        End If
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungCongViec)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSNhomCongViec()
        AddParameterWhere("@Loai", LoaiTuDien.NhomCongViec)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If Not dt Is Nothing Then
            rcbNhomCongViec.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa: " & gdvCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    Dim sql As String = "ALTER TABLE tblDinhMucDiem DROP Column C" & gdvCT.GetFocusedRowCellValue("ID")
                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        ShowCanhBao(LoiNgoaiLe)
                    End If
                    CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub

    Private Sub rcbNhomCongViec_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomCongViec.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmNhomCongViec
            f.Tag = Me.Tag
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbNhomCongViec.EditValue = Nothing
        End If
       
    End Sub

    Private Sub cbNhomCongViec_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNhomCongViec.EditValueChanged
        loadDS()
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If _exit Then Exit Sub
        AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@IDP", cbNhomCongViec.EditValue)
        AddParameter("@Loai", LoaiTuDien.NoiDungCongViec)
        AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
        AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
        AddParameter("@Diem", gdvCT.GetFocusedRowCellValue("Diem"))
        AddParameter("@HeSo1", gdvCT.GetFocusedRowCellValue("HeSo1"))
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else

                ShowAlert("Đã cập nhật hạng mục công việc !")
            End If
        Else
            objID = doInsert("tblTuDien")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDS()
            Else
                Dim sql As String = "ALTER TABLE tblDinhMucDiem ADD C" & objID.ToString & " INT NOT NULL DEFAULT 0"
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowCanhBao(LoiNgoaiLe)
                End If
                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", objID)
                _exit = False

                ShowAlert("Đã thêm hạng mục công việc !")

            End If
        End If
        loadDS()
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("Ma", gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "Ma") + 1)
    End Sub
End Class