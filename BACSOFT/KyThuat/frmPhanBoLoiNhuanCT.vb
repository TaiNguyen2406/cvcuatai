Imports BACSOFT.Db.SqlHelper

Public Class frmPhanBoLoiNhuanCT

    Public _exit As Boolean = False

    Private Sub frmPhanBoLoiNhuanCT_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        loadDS()
    End Sub

    Public Sub loadDS()
        Dim sql As String = ""

        sql = "SELECT ID,STT,NoiDung,MoTa,PhuTrachHopDong,PhuTrachChaoGia,PhuTrachQuanLyCT,TongConLai,ThietKe FROM KTPhanBoLoiNhuanCT ORDER BY STT"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa: " & gdvCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doDelete("KTPhanBoLoiNhuanCT", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    'Dim sql As String = "ALTER TABLE tblDinhMucDiem DROP Column C" & gdvCT.GetFocusedRowCellValue("ID")
                    'If ExecuteSQLNonQuery(sql) Is Nothing Then
                    '    ShowCanhBao(LoiNgoaiLe)
                    'End If
                    'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub


    Private Sub cbNhomCongViec_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadDS()
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If _exit Then Exit Sub
        AddParameter("@STT", gdvCT.GetFocusedRowCellValue("STT"))
        AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
        AddParameter("@PhuTrachHopDong", gdvCT.GetFocusedRowCellValue("PhuTrachHopDong"))
        AddParameter("@PhuTrachChaoGia", gdvCT.GetFocusedRowCellValue("PhuTrachChaoGia"))
        AddParameter("@PhuTrachQuanLyCT", gdvCT.GetFocusedRowCellValue("PhuTrachQuanLyCT"))
        AddParameter("@TongConLai", gdvCT.GetFocusedRowCellValue("TongConLai"))
        AddParameter("@ThietKe", gdvCT.GetFocusedRowCellValue("ThietKe"))
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doUpdate("KTPhanBoLoiNhuanCT", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else

                ShowAlert("Đã cập nhật!")
            End If
        Else
            objID = doInsert("KTPhanBoLoiNhuanCT")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDS()
            Else
                'Dim sql As String = "ALTER TABLE tblDinhMucDiem ADD C" & objID.ToString & " INT NOT NULL DEFAULT 0"
                'If ExecuteSQLNonQuery(sql) Is Nothing Then
                '    ShowCanhBao(LoiNgoaiLe)
                'End If
                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", objID)
                _exit = False

                ShowAlert("Đã thêm !")

            End If
        End If
        loadDS()
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs)
        gdvCT.SetFocusedRowCellValue("STT", gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "STT") + 1)
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "PhuTrachHopDong", "PhuTrachChaoGia", "PhuTrachQuanLyCT"
                gdvCT.SetRowCellValue(e.RowHandle, "TongConLai", 100 - (gdvCT.GetRowCellValue(e.RowHandle, "PhuTrachHopDong") + gdvCT.GetRowCellValue(e.RowHandle, "PhuTrachChaoGia") + gdvCT.GetRowCellValue(e.RowHandle, "PhuTrachQuanLyCT")))
        End Select
    End Sub
End Class