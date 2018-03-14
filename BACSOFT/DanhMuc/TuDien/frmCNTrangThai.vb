Imports BACSOFT.Db.SqlHelper

Public Class frmCNTrangThai


    Private Sub frmTGGiaoHang_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDataCbTrangThai()
        LoadDS()
    End Sub

    Private Sub loadDataCbTrangThai()
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Byte"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r As DataRow = tb.NewRow
        r("ID") = 0
        r("Ten") = "Yêu cầu đến"
        tb.Rows.Add(r)

        Dim r1 As DataRow = tb.NewRow
        r1("ID") = 2
        r1("Ten") = "Chào giá"
        tb.Rows.Add(r1)

        Dim r2 As DataRow = tb.NewRow
        r2("ID") = 4
        r2("Ten") = "Thời gian cung ứng"
        tb.Rows.Add(r2)

        Dim r3 As DataRow = tb.NewRow
        r3("ID") = 38
        r3("Ten") = "Yêu cầu hỏi giá"
        tb.Rows.Add(r3)

        Dim r4 As DataRow = tb.NewRow
        r4("ID") = 40
        r4("Ten") = "Nhóm yêu cầu nội bộ"
        tb.Rows.Add(r4)

        Dim r5 As DataRow = tb.NewRow
        r5("ID") = 42
        r5("Ten") = "Nguồn khách mới"
        tb.Rows.Add(r5)

        Dim r6 As DataRow = tb.NewRow
        r6("ID") = 43
        r6("Ten") = "Loại tài sản"
        tb.Rows.Add(r6)

        Dim r7 As DataRow = tb.NewRow
        r7("ID") = 44
        r7("Ten") = "Mục đích xuất cho BAC"
        tb.Rows.Add(r7)
        rcbTrangThai.DataSource = tb
        cbTrangThai.EditValue = Convert.ToByte(0)

    End Sub

    Private Sub LoadDS()
        AddParameter("@Loai", cbTrangThai.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblTuDien WHERE Loai=@Loai ORDER By Ma")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown

        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng được chọn ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("tblTuDien", "ID=@ID") Is Nothing Then
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
            AddParameter("@Ma", gdvCT.GetFocusedRowCellValue("Ma"))
            AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
            AddParameter("@Loai", cbTrangThai.EditValue)
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                Dim objID As Object = doInsert("tblTuDien")
                If objID Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                gdvCT.SetFocusedRowCellValue("ID", objID)
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("tblTuDien", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub cbTrangThai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTrangThai.EditValueChanged
        LoadDS()
    End Sub
End Class
