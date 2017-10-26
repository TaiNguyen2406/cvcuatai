Imports BACSOFT.Db.SqlHelper

Public Class frmNoiDungThiCong
    Dim _exit As Boolean = False

    Private Sub frmNoiDungThiCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDSNhomCongViec()
        loadDS()
    End Sub

    Public Sub loadDS()
        Dim sql As String = ""
        If Not cbNhomCongViec.EditValue Is Nothing Then
            AddParameterWhere("@IDCha", cbNhomCongViec.EditValue)
            sql = "SELECT ID,NoiDung,Ma,MoTa,IDP FROM tblTuDien WHERE Loai=@Loai AND IDP=@IDCha ORDER BY Ma"
        Else
            sql = "SELECT ID,NoiDung,Ma,MoTa,IDP FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma"
        End If
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSNhomCongViec()
        AddParameterWhere("@Loai", LoaiTuDien.NhomNoiDungThiCong)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If Not dt Is Nothing Then
            rcbNhomCongViec.DataSource = dt
            rcbNhom.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    'Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
    '    If e.Column.FieldName = "ID" Then Exit Sub
    '    AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
    '    AddParameter("@Loai", LoaiTuDien.NoiDungThiCong)
    '    AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
    '    AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
    '    AddParameter("@IDP", gdvCT.GetFocusedRowCellValue("IDP"))
    '    If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
    '        AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
    '        If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then
    '            ShowBaoLoi(LoiNgoaiLe)
    '            loadDS()
    '        Else
    '            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
    '            ShowAlert("Đã cập nhật nội dung thi công !")
    '        End If
    '    Else
    '        objID = doInsert("tblTuDien")
    '        If objID Is Nothing Then
    '            ShowBaoLoi(LoiNgoaiLe)
    '            gdvCT.DeleteSelectedRows()
    '            loadDS()
    '        Else


    '            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
    '            ShowAlert("Đã thêm nội dung thi công !")
    '            gdvCT.SetFocusedRowCellValue("ID", objID)
    '        End If
    '    End If
    'End Sub


    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xóa: " & gdvCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    AddParameterWhere("@IDND", gdvCT.GetFocusedRowCellValue("ID"))
                    If doDelete("tblDinhMucDiem", "IDNoiDungCV=@IDND") Is Nothing Then
                        ShowBaoLoi("Lỗi xoá dòng tblDinhMucDiem: " & LoiNgoaiLe)
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
            Dim f As New frmNhomNoiDungThiCong
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
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
            If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Else
            If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        End If
        AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
        AddParameter("@Loai", LoaiTuDien.NoiDungThiCong)
        AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
        AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
        AddParameter("@IDP", gdvCT.GetFocusedRowCellValue("IDP"))
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                loadDS()
            Else
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
                ShowAlert("Đã cập nhật nội dung thi công !")
            End If
        Else
            objID = doInsert("tblTuDien")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDS()
            Else
                AddParameter("@IDNoiDungCV", objID)
                If doInsert("tblDinhMucDiem") Is Nothing Then
                    ShowBaoLoi("Lỗi thêm dòng tblDinhMucDiem: " & LoiNgoaiLe)
                End If
                ShowAlert("Đã thêm nội dung thi công !")

                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", objID)
                _exit = False
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
            End If
        End If
    End Sub

    Private Sub frmNoiDungThiCong_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("Ma", gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "Ma") + 1)
        gdvCT.SetFocusedRowCellValue("IDP", cbNhomCongViec.EditValue)
    End Sub
End Class