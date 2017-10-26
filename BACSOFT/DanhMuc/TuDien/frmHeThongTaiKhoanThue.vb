Imports BACSOFT.Db.SqlHelper
Public Class frmHeThongTaiKhoanThue

    Private Sub frmHeThongTaiKhoanThue_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        LoadDsTaiKhoan()

    End Sub

    Private Sub LoadDsTaiKhoan()

        Dim sql As String = "SELECT TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE ORDER BY TaiKhoan "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()
       
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next


        gdv.DataSource = tb2


    End Sub

    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub

    Private Sub mnuThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThem.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isAddNew = True
        Dim f As New frmUpdateHeThongTkThue
        f.ShowDialog()
        LoadDsTaiKhoan()

        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "TaiKhoan") = MaTuDien Then
                gdvData.FocusedRowHandle = i
                Exit For
            End If
        Next

    End Sub

    Private Sub mnuSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSua.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isUpdate = True
        MaTuDien = gdvData.GetFocusedRowCellValue("TaiKhoan")
        Dim f As New frmUpdateHeThongTkThue
        f.ShowDialog()
        LoadDsTaiKhoan()

        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "TaiKhoan") = MaTuDien Then
                gdvData.FocusedRowHandle = i
                Exit For
            End If
        Next

    End Sub

    Private Sub mnuXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoa.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        Dim index As Integer = gdvData.FocusedRowHandle

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        Dim tk As String = gdvData.GetFocusedRowCellValue("TaiKhoan")

        If Not ShowCauHoi("Xóa tài khoản thuế " & tk & " bạn vừa chọn?") Then Exit Sub



        Dim sql As String = "select count(id) from chungtuchitiet where TaiKhoanNo = '" & tk & "' or TaiKhoanCo = '" & tk & "'"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt.Rows(0)(0) > 0 Then
            ShowCanhBao("Tài khoản " & tk & " đã được sử dụng nên không thể xóa!")
            Exit Sub
        End If

        AddParameterWhere("@TaiKhoan", tk)
        If doDelete("TAIKHOANTHUE", "TaiKhoan=@TaiKhoan") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        LoadDsTaiKhoan()

        ShowAlert("Đã xóa tài khoản " & tk & " khỏi hệ thống!")

        If index > 0 Then
            gdvData.FocusedRowHandle = index - 1
        End If

    End Sub



    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            mnuSua.Enabled = True
            mnuXoa.Enabled = True
        Else
            mnuSua.Enabled = False
            mnuXoa.Enabled = False
        End If
        pMnu.ShowPopup(gdv.PointToScreen(e.Location))
    End Sub


End Class
