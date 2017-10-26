Imports BACSOFT.Db.SqlHelper

Public Class frmTaiKhoanNganHang

    Private Sub frmTaiKhoanNganHang_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cbLoaiTK.DataSource = LoaiTaiKhoan()
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,MaSo,Ten,Ten_ENG,LoaiTK FROM TAIKHOAN")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function LoaiTaiKhoan() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("Ma", Type.GetType("System.String"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))

        Dim r As DataRow = tb.NewRow
        r(0) = "CN"
        r(1) = "Cá nhân"
        tb.Rows.Add(r)
        Dim r1 As DataRow = tb.NewRow
        r1(0) = "CT"
        r1(1) = "Công ty"
        tb.Rows.Add(r1)
        Return tb
    End Function

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xóa tài khoản ngân hàng được chọn ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("TAIKHOAN", "ID=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Else
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        End If
        Try
            AddParameter("@MaSo", gdvCT.GetFocusedRowCellValue("MaSo"))
            AddParameter("@Ten", gdvCT.GetFocusedRowCellValue("Ten"))
            AddParameter("@Ten_ENG", gdvCT.GetFocusedRowCellValue("Ten_ENG"))
            AddParameter("@LoaiTK", gdvCT.GetFocusedRowCellValue("LoaiTK"))

            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then

                objID = doInsert("TAIKHOAN")
                If objID Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    Throw New Exception(LoiNgoaiLe)
                End If
                gdvCT.SetFocusedRowCellValue("ID", objID)
            Else

                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("TAIKHOAN", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

End Class
