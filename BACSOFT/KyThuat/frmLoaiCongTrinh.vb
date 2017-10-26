Imports BACSOFT.Db.SqlHelper

Public Class frmLoaiCongTrinh

    Public _exit As Boolean = False
    Public _exit2 As Boolean = False
    Private Sub frmLoaiCongTrinh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbThang.EditValue = Today.Month
        tbNam.EditValue = Today.Year
        loadDS()
    End Sub

    Public Sub loadDS()
        Dim sql As String = ""
        Dim _Thang As String = ""
        If tbThang.EditValue.ToString.Length = 1 Then
            _Thang = "0" + tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        Else
            _Thang = tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        End If
        AddParameterWhere("@Thang", _Thang)
        sql = "SELECT * FROM LoaiCongTrinh "
        sql &= " LEFT JOIN LoaiCongTrinh_LNThietKe ON LoaiCongTrinh.ID=LoaiCongTrinh_LNThietKe.IDLoaiCT AND Thang=@Thang"
        sql &= " ORDER BY STT"
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
                If doDelete("LoaiCongTrinh", "ID=@ID") Is Nothing Then
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

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        loadDS()
    End Sub

    Dim _IDMain As Integer = -1
    Dim _IDCT As Integer = -1

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        ' If _exit Then Exit Sub


        ' AddParameter("@LNThietKe", gdvCT.GetFocusedRowCellValue("LNThietKe"))
        If Not IsDBNull(gdvCT.GetRowCellValue(e.RowHandle, "ID")) Or gdvCT.GetRowCellValue(e.RowHandle, "ID") Is Nothing Then
            If _exit And _IDMain > -1 Then
                _IDMain = -1
                _exit = False
            Else
                AddParameter("@STT", gdvCT.GetRowCellValue(e.RowHandle, "STT"))
                AddParameter("@NoiDung", gdvCT.GetRowCellValue(e.RowHandle, "NoiDung"))
                AddParameter("@MoTa", gdvCT.GetRowCellValue(e.RowHandle, "MoTa"))
                AddParameterWhere("@ID", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
                If doUpdate("LoaiCongTrinh", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)

                    loadDS()
                Else

                    'ShowAlert("Đã cập nhật!")
                End If
            End If

        Else
            AddParameter("@STT", gdvCT.GetRowCellValue(e.RowHandle, "STT"))
            AddParameter("@NoiDung", gdvCT.GetRowCellValue(e.RowHandle, "NoiDung"))
            AddParameter("@MoTa", gdvCT.GetRowCellValue(e.RowHandle, "MoTa"))
            objID = doInsert("LoaiCongTrinh")
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
                _IDMain = objID
                gdvCT.SetRowCellValue(e.RowHandle, "ID", objID)
                ' _exit = False

                ' ShowAlert("Đã thêm !")

            End If
        End If

        Dim sql As String = ""
        Dim _Thang As String = ""
        If tbThang.EditValue.ToString.Length = 1 Then
            _Thang = "0" + tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        Else
            _Thang = tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        End If

        If Not IsDBNull(gdvCT.GetRowCellValue(e.RowHandle, "IDLoaiCT")) Or gdvCT.GetRowCellValue(e.RowHandle, "IDLoaiCT") Is Nothing Then
            If _exit2 And _IDCT > -1 Then
                _IDCT = -1
                _exit2 = False
            Else
                AddParameter("@LNThietKe", gdvCT.GetRowCellValue(e.RowHandle, "LNThietKe"))
                AddParameterWhere("@Thang", _Thang)
                AddParameterWhere("@ID", gdvCT.GetRowCellValue(e.RowHandle, "IDLoaiCT"))
                If doUpdate("LoaiCongTrinh_LNThietKe", "IDLoaiCT=@ID AND Thang=@Thang") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    loadDS()
                Else
                    ShowAlert("Đã cập nhật!")
                End If
            End If

        Else
            AddParameter("@IDLoaiCT", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
            AddParameter("@LNThietKe", gdvCT.GetRowCellValue(e.RowHandle, "LNThietKe"))
            AddParameter("@Thang", _Thang)
            ' objID = 
            If doInsert("LoaiCongTrinh_LNThietKe") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDS()
            Else
                'Dim sql As String = "ALTER TABLE tblDinhMucDiem ADD C" & objID.ToString & " INT NOT NULL DEFAULT 0"
                'If ExecuteSQLNonQuery(sql) Is Nothing Then
                '    ShowCanhBao(LoiNgoaiLe)
                'End If
                _exit2 = True
                _IDCT = 0
                gdvCT.SetRowCellValue(e.RowHandle, "IDLoaiCT", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
                '_exit = False

                ShowAlert("Đã thêm !")

            End If
        End If
            ' loadDS()
            '  CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("STT", gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "STT") + 1)
    End Sub



End Class